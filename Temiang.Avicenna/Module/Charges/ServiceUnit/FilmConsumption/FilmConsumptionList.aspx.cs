using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;
using System.Web.UI;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class FilmConsumptionList : BasePage
    {
        private bool _isHideEmptySearchMessage = false;

        protected void Page_Init(object sender, EventArgs e)
        {
            _isHideEmptySearchMessage = false;
            if (Page.IsPostBack)
            {
                if (Request["__EVENTTARGET"].Contains("grd") &&
                    Request["__EVENTARGUMENT"].Contains("rebind"))
                {
                    _isHideEmptySearchMessage = true;
                }
            }

            ProgramID = AppConstant.Program.FilmConsumptionEntry;

            if (!IsPostBack)
            {
                txtOrderDate1.SelectedDate = DateTime.Now;
                txtOrderDate2.SelectedDate = DateTime.Now;

                grdList.Columns[grdList.Columns.Count - 1].Visible = AppSession.Parameter.IsRadiologyNoAutoCreate;
            }
        }

        private bool ValidateSearch(bool isEmptyFilter, string searchingLabel)
        {
            if (!IsListLoadRecordIfFiltered) return true;
            if (!IsPostBack) return false;
            if (!isEmptyFilter) return true;
            if (!_isHideEmptySearchMessage)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "invalid",
                    string.Format("alert('Please entry {0} searching criteria');", searchingLabel), true);
            }
            return false;
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var grd = (RadGrid)source;
            if (!IsPostBack && !IsListLoadRecordOnInit) {
                grd.DataSource = new String[] { };
                return; 
            } 
            
            var dataSource = TransCharges;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                grd.DataSource = dataSource;
            }
        }

        private DataTable TransCharges
        {
            get
            {
                var isEmptyFilter = txtOrderDate1.IsEmpty && txtOrderDate2.IsEmpty && string.IsNullOrEmpty(txtTransactionNo.Text) && string.IsNullOrEmpty(txtRegistrationNo.Text) && string.IsNullOrEmpty(txtPatientName.Text);
                if (!ValidateSearch(isEmptyFilter, "Patient")) return null;

                var query = new TransChargesQuery("a");
                var reg = new RegistrationQuery("b");
                var patient = new PatientQuery("c");
                var unit = new ServiceUnitQuery("d");
                var unit2 = new ServiceUnitQuery("e");
                var room = new ServiceRoomQuery("f");
                var py = new TransPaymentItemOrderQuery("g");
                //var sutcQ = new ServiceUnitTransactionCodeQuery("h");
                var usr = new AppUserServiceUnitQuery("i");

                var tci = new TransChargesItemQuery("tci");
                var tcic = new TransChargesItemCompQuery("tcic");
                var tc = new TariffComponentQuery("tc");

                var tciCr = new TransChargesItemQuery("tciCr");

                var jm = new ParamedicFeeTransChargesItemCompSettledQuery("m");
                var sal = new AppStandardReferenceItemQuery("sal");

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                query.es.Distinct = true;

                query.Select
                    (
                        query.TransactionNo,
                        query.ReferenceNo,
                        query.TransactionDate,
                        unit.ServiceUnitName,
                        query.RegistrationNo,
                        patient.MedicalNo,
                        patient.PatientName,
                        unit2.ServiceUnitName.As("ClusterName"),
                        room.RoomName,
                        query.BedID,
                        @"<CAST(CONVERT(VARCHAR(10), a.TransactionDate, 101) AS DATETIME) AS [Group]>",
                        py.PaymentNo,
                        sal.ItemName.As("SalutationName"), 
                        patient.DiagnosticNo
                    );

                query.InnerJoin(tci).On(query.TransactionNo == tci.TransactionNo);
                query.LeftJoin(tciCr).On(tci.TransactionNo == tciCr.ReferenceNo && tci.SequenceNo == tciCr.ReferenceSequenceNo);
                query.InnerJoin(tcic).On(tci.TransactionNo == tcic.TransactionNo && tci.SequenceNo == tcic.SequenceNo);
                query.InnerJoin(tc).On(tcic.TariffComponentID == tc.TariffComponentID);
                query.InnerJoin(reg).On(query.RegistrationNo == reg.RegistrationNo);
                query.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
                query.InnerJoin(unit).On(query.ToServiceUnitID == unit.ServiceUnitID);
                query.InnerJoin(unit2).On(query.FromServiceUnitID == unit2.ServiceUnitID);
                query.LeftJoin(room).On(query.RoomID == room.RoomID);
                query.LeftJoin(py).On(query.TransactionNo == py.TransactionNo && py.IsPaymentProceed == true &&
                                      py.IsPaymentReturned == false);
                //query.InnerJoin(sutcQ).On(query.ToServiceUnitID == sutcQ.ServiceUnitID && sutcQ.SRTransactionCode == "005");
                query.InnerJoin(usr).On(query.ToServiceUnitID == usr.ServiceUnitID &&
                                        usr.UserID == AppSession.UserLogin.UserID);
                query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & patient.SRSalutation == sal.ItemID);
                query.Where(tci.ChargeQuantity > 0, tciCr.TransactionNo.IsNull());
                query.Where(tc.IsTariffParamedic == true);

                if (!txtOrderDate1.IsEmpty && !txtOrderDate2.IsEmpty)
                    query.Where(query.TransactionDate >= txtOrderDate1.SelectedDate, query.TransactionDate < txtOrderDate2.SelectedDate.Value.AddDays(1));

                if (txtRegistrationNo.Text != string.Empty)
                {
                    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                    var reverseMedNoSearch = string.Format("{0}%", searchReg.Replace("-", "").Reverse());

                    query.Where(
                        query.Or(
                            query.RegistrationNo == searchReg,
                            patient.ReverseMedicalNo.Like(reverseMedNoSearch),
                            patient.ReverseOldMedicalNo.Like(reverseMedNoSearch)
                            )
                        );


                    //if (AppSession.Parameter.IsMedicalNoContainStrip)
                    //    query.Where(
                    //        query.Or(
                    //            query.RegistrationNo == txtRegistrationNo.Text,
                    //            patient.MedicalNo == txtRegistrationNo.Text,
                    //            patient.OldMedicalNo == txtRegistrationNo.Text,
                    //            string.Format("< OR REPLACE(c.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg),
                    //            string.Format("< OR REPLACE(c.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchReg)
                    //            )
                    //        );
                    //else
                    //    query.Where(
                    //        query.Or(
                    //            query.RegistrationNo == txtRegistrationNo.Text,
                    //            patient.MedicalNo == txtRegistrationNo.Text,
                    //            patient.OldMedicalNo == txtRegistrationNo.Text,
                    //            string.Format("< OR c.MedicalNo LIKE '%{0}%'>", searchReg),
                    //            string.Format("< OR c.OldMedicalNo LIKE '%{0}%'>", searchReg)
                    //            )
                    //        );
                }
                if (txtTransactionNo.Text != string.Empty)
                {
                    string searchTransactionNo = "%" + txtTransactionNo.Text + "%";
                    query.Where(string.Format("<a.TransactionNo LIKE '{0}'>", searchTransactionNo));
                }
                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
                    query.Where(patient.FullName.Like(searchPatient));
                    //query.Where
                    //    (
                    //      string.Format("<LTRIM(RTRIM(LTRIM(c.FirstName + ' ' + c.MiddleName)) + ' ' + c.LastName) LIKE '{0}'>", searchPatient)
                    //    );
                }

                query.Where
                    (
                        //query.IsOrder == true,
                        query.IsApproved == true
                        
                    );

                if (!AppSession.Parameter.IsUpdatePhysicianLookingPhysicianFeeVerification)
                {
                    query.Where(
                        reg.IsHoldTransactionEntry == false,
                        reg.IsClosed == false
                    );
                }
                else {
                    query.LeftJoin(jm).On(py.TransactionNo == jm.TransactionNo && py.SequenceNo == jm.SequenceNo)
                        .Where(jm.VerificationNo.Coalesce("''") == string.Empty);
                }

                query.OrderBy(query.TransactionDate.Descending);

                DataTable tbl = query.LoadDataTable();

                if (!AppSession.Parameter.IsUpdatePhysicianLookingPhysicianFeeVerification)
                {
                    foreach (DataRow row in tbl.Rows)
                    {
                        var detail = new TransChargesItemQuery();
                        detail.Where
                            (
                                detail.TransactionNo == (string)row["TransactionNo"],
                                detail.IsBillProceed == true,
                                detail.IsVoid == false
                            );

                        if (detail.LoadDataTable().Rows.Count == 0)
                            row.Delete();
                    }

                    tbl.AcceptChanges();
                }

                return tbl;
            }
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            grdList.Rebind();
        }

        protected override void RaisePostBackEvent(System.Web.UI.IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (!(sourceControl is RadGrid))
                return;
            if (eventArgument.Contains("!"))
            {
                var usr = new AppUserServiceUnitCollection();
                usr.Query.Where(usr.Query.UserID == AppSession.UserLogin.UserID,
                                usr.Query.ServiceUnitID == AppSession.Parameter.ServiceUnitRadiologyID);
                usr.LoadAll();
                if (usr.Count > 0)
                {
                    var param = eventArgument.Split('!');
                    var regno = param[1];
                    var diagno = param[2];
                    var reg = new Registration();
                    if (reg.LoadByPrimaryKey(regno))
                    {
                        var pat = new Patient();
                        if (pat.LoadByPrimaryKey(reg.PatientID))
                        {
                            pat.DiagnosticNo = diagno;
                            pat.Save();
                            grdList.Rebind();
                        }
                    }
                }
            }
            else if (eventArgument == "rebind")
                grdList.Rebind();
        }
    }
}
