using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using System.Web.UI;
using Temiang.Dal.DynamicQuery;
using System.Collections.Generic;
using System.Web;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class PrescriptionVerificationList : BasePage
    {
        private bool _isHideEmptySearchMessage = false;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e); // !!Jika tidak dipanggil, tampilan jadi tidak rapih
            _isHideEmptySearchMessage = false;
            if (Page.IsPostBack)
            {
                if (Request["__EVENTTARGET"].Contains("grd") &&
                    Request["__EVENTARGUMENT"].Contains("rebind"))
                {
                    _isHideEmptySearchMessage = true;
                }
            }

            ProgramID = AppConstant.Program.PrescriptionVerification;

            if (!IsPostBack)
            {
                //cboSRRegistrationType

                //service unit
                var unit = new ServiceUnitCollection();
                var query = new ServiceUnitQuery();
                query.Where(query.SRRegistrationType.In(AppConstant.RegistrationType.EmergencyPatient,
                                                            AppConstant.RegistrationType.OutPatient,
                                                            AppConstant.RegistrationType.InPatient,
                                                            AppConstant.RegistrationType.MedicalCheckUp), 
                                                            query.IsActive == true);
                query.OrderBy(unit.Query.ServiceUnitName.Ascending);
                unit.Load(query);

                cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (ServiceUnit entity in unit)
                {
                    cboServiceUnitID.Items.Add(new RadComboBoxItem(entity.ServiceUnitName, entity.ServiceUnitID));
                }

                //paramedic
                var param = new ParamedicCollection();
                param.Query.Where
                    (
                        param.Query.IsActive == true,
                        param.Query.IsAvailable == true
                    );
                param.Query.OrderBy(param.Query.ParamedicName.Ascending);
                param.LoadAll();

                cboParamedicID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (Paramedic entity in param)
                {
                    cboParamedicID.Items.Add(new RadComboBoxItem(entity.ParamedicName, entity.ParamedicID));
                }

                cboPrescriptionType.Items.Add(new RadComboBoxItem("", ""));
                cboPrescriptionType.Items.Add(new RadComboBoxItem("Sales Handling", "1"));
                cboPrescriptionType.Items.Add(new RadComboBoxItem("Order Handling", "2"));
                cboPrescriptionType.Items.Add(new RadComboBoxItem("UDD", "3"));
                cboPrescriptionType.Items.Add(new RadComboBoxItem("Prescription Return", "4"));

                cboPrescriptionType2.Items.Add(new RadComboBoxItem("", ""));
                cboPrescriptionType2.Items.Add(new RadComboBoxItem("Sales Handling", "1"));
                cboPrescriptionType2.Items.Add(new RadComboBoxItem("Order Handling", "2"));
                cboPrescriptionType2.Items.Add(new RadComboBoxItem("UDD", "3"));
                cboPrescriptionType2.Items.Add(new RadComboBoxItem("Prescription Return", "4"));

                txtPrescriptionDate.SelectedDate = (new DateTime()).NowAtSqlServer();
                txtVerificationDate.SelectedDate = (new DateTime()).NowAtSqlServer();
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (IsPostBack) return;

            RestoreValueFromCookie();

            ComboBox.PopulateWithServiceUnitForTransaction(cboDispensaryID, TransactionCode.Prescription, true);
            ComboBox.PopulateWithServiceUnitForTransaction(cboDispensaryID2, TransactionCode.Prescription, true);
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

        protected void grdPrescription_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var grd = (RadGrid)source;

            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grd.DataSource = new String[] { };
                return;
            }

            var dataSource = TransPrescriptions;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }
        }

        private DataTable TransPrescriptions
        {
            get
            {
                var isEmptyFilter = string.IsNullOrEmpty(cboRegistrationType.SelectedValue) && string.IsNullOrEmpty(cboDispensaryID.SelectedValue) && 
                    string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) && string.IsNullOrEmpty(cboParamedicID.SelectedValue) && string.IsNullOrEmpty(txtRegistrationNo.Text) && 
                    string.IsNullOrEmpty(txtPatientName.Text) && txtPrescriptionDate.IsEmpty && string.IsNullOrEmpty(txtPrescriptionNo.Text) && string.IsNullOrEmpty(cboPrescriptionType.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Prescription")) return null;

                var presc = new TransPrescriptionQuery("a");
                var reg = new RegistrationQuery("b");
                var patient = new PatientQuery("c");
                var medic = new ParamedicQuery("d");
                var unit = new ServiceUnitQuery("e");
                var sal = new AppStandardReferenceItemQuery("sal");
                var disp = new ServiceUnitQuery("disp");
                var usr2 = new AppUserQuery("appr");
                var guar = new GuarantorQuery("guar");

                presc.es.Distinct = true;

                presc.Select
                    (
                        presc.PrescriptionNo,
                        presc.PrescriptionDate,
                        presc.RegistrationNo,
                        patient.MedicalNo,
                        patient.PatientName,
                        patient.Sex,
                        reg.ServiceUnitID,
                        unit.ServiceUnitName,
                        presc.ParamedicID,
                        medic.ParamedicName,
                        presc.Note,
                        presc.ApprovalDateTime,
                        presc.IsProceedByPharmacist,
                        presc.CompleteDateTime,
                        presc.DeliverDateTime,
                        "<0 AS Status>",
                        sal.ItemName.As("SalutationName"),
                        disp.ServiceUnitName.As("DispensaryName"),
                        usr2.UserName.As("ApprovedByUserName"),
                        @"<CASE WHEN b.SRRegistrationType = 'IPR' THEN 'ipr' ELSE 'opr' END AS rt>",
                        @"<CASE WHEN a.IsPrescriptionReturn = 1 THEN '1' ELSE '0' END AS ret>",
                        guar.GuarantorName
                    );

                presc.InnerJoin(reg).On(reg.RegistrationNo == presc.RegistrationNo);
                presc.InnerJoin(patient).On(patient.PatientID == reg.PatientID);
                presc.InnerJoin(unit).On(unit.ServiceUnitID == reg.ServiceUnitID);
                presc.LeftJoin(medic).On(medic.ParamedicID == presc.ParamedicID);
                presc.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & patient.SRSalutation == sal.ItemID);
                presc.InnerJoin(disp).On(disp.ServiceUnitID == presc.ServiceUnitID); 
                presc.LeftJoin(usr2).On(usr2.UserID == presc.ApprovedByUserID);
                presc.LeftJoin(guar).On(guar.GuarantorID == reg.GuarantorID);

                // Bug fix: Jika dipilih type UDD jadi bentrok dgn filter presc.IsUnitDosePrescription == true yg dibawah (Handono 230405)
                //presc.Where(presc.IsApproval == true, presc.IsVoid == false, 
                //    presc.IsUnitDosePrescription == false, presc.Or(presc.IsPos.IsNull(), presc.IsPos == false),
                //    presc.Or(presc.IsVerified == false, presc.IsVerified.IsNull()));

                presc.Where(presc.IsApproval == true, presc.IsVoid == false, 
                    presc.Or(presc.IsPos.IsNull(), presc.IsPos == false),
                    presc.Or(presc.IsVerified == false, presc.IsVerified.IsNull()));

                bool IsFilterMaxResultRecord = true;
                if (!string.IsNullOrEmpty(cboRegistrationType.SelectedValue))
                    presc.Where(reg.SRRegistrationType == cboRegistrationType.SelectedValue);
                if (!string.IsNullOrEmpty(cboDispensaryID.SelectedValue))
                {
                    presc.Where(presc.ServiceUnitID == cboDispensaryID.SelectedValue);
                    IsFilterMaxResultRecord = false;
                }
                if (cboServiceUnitID.SelectedValue != string.Empty)
                    presc.Where(reg.ServiceUnitID == cboServiceUnitID.SelectedValue);
                if (cboParamedicID.SelectedValue != string.Empty)
                    presc.Where(presc.ParamedicID == cboParamedicID.SelectedValue);
                if (txtRegistrationNo.Text != string.Empty)
                {
                    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                    presc.Where(
                        presc.Or(
                            string.Format("<a.RegistrationNo = '{0}' OR >", searchReg),
                            string.Format("<c.MedicalNo = '{0}' OR >", searchReg),
                            string.Format("<c.OldMedicalNo = '{0}'>", searchReg),
                            string.Format("< OR REPLACE(c.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg),
                            string.Format("< OR REPLACE(c.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchReg)
                            )
                        );
                }

                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
                    presc.Where
                        (
                          string.Format("<LTRIM(RTRIM(LTRIM(c.FirstName + ' ' + c.MiddleName)) + ' ' + c.LastName) LIKE '{0}'>", searchPatient)
                        );
                }
                if (!txtPrescriptionDate.IsEmpty)
                {
                    presc.Where(presc.PrescriptionDate >= txtPrescriptionDate.SelectedDate, presc.PrescriptionDate < txtPrescriptionDate.SelectedDate.Value.AddDays(1));
                    IsFilterMaxResultRecord = false;
                }
                if (txtPrescriptionNo.Text != string.Empty)
                    presc.Where(presc.PrescriptionNo == Helper.EscapeQuery(txtPrescriptionNo.Text));
                if (!string.IsNullOrEmpty(cboPrescriptionType.SelectedValue))
                {
                    switch (cboPrescriptionType.SelectedValue)
                    {
                        case "1":
                            {
                                presc.Where(presc.IsFromSOAP == true, presc.IsPrescriptionReturn == false, presc.IsUnitDosePrescription == false);
                                break;
                            }
                        case "2":
                            {
                                presc.Where(presc.IsFromSOAP == false, presc.IsPrescriptionReturn == false, presc.IsUnitDosePrescription == false);
                                break;
                            }
                        case "3":
                            {
                                presc.Where(presc.IsFromSOAP == false, presc.IsPrescriptionReturn == false, presc.IsUnitDosePrescription == true);
                                break;
                            }
                        case "4":
                            {
                                presc.Where(presc.IsPrescriptionReturn == true);
                                break;
                            }
                    }
                }

                if (IsFilterMaxResultRecord)
                    presc.es.Top = AppSession.Parameter.MaxResultRecord;

                presc.OrderBy(presc.PrescriptionNo.Descending);

                DataTable dtbl = presc.LoadDataTable();

                foreach (DataRow row in dtbl.Rows)
                {
                    row["Status"] = !(row["DeliverDateTime"] is DBNull) ? 3 :
                        (!(row["CompleteDateTime"] is DBNull) ? 2 : (!(row["IsProceedByPharmacist"] is DBNull) ? 1 : 0));
                }
                dtbl.AcceptChanges();

                return dtbl;
            }
        }

        protected void grdPrescription_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            var query = new TransPrescriptionItemQuery("a");
            var qItem = new ItemQuery("b");
            var qItemI = new ItemQuery("c");

            var emb = new EmbalaceQuery("x");
            var cons = new ConsumeMethodQuery("y");

            query.Select
                (
                    query,
                    qItem.ItemName.As("ItemName"),
                    qItemI.ItemName.As("ItemInterventionName"),
                    "<(a.ParentNo + a.SequenceNo) as ORDERKEY>",
                    emb.EmbalaceLabel.As("EmbalaceLabel"),
                    cons.SRConsumeMethodName.As("SRConsumeMethodName")
                );
            query.InnerJoin(qItem).On(query.ItemID == qItem.ItemID);
            query.LeftJoin(qItemI).On(query.ItemInterventionID == qItemI.ItemID);
            query.LeftJoin(emb).On(query.EmbalaceID == emb.EmbalaceID);
            query.LeftJoin(cons).On(query.SRConsumeMethod == cons.SRConsumeMethod);
            query.Where(query.PrescriptionNo == e.DetailTableView.ParentItem.GetDataKeyValue("PrescriptionNo").ToString());
            query.OrderBy("ORDERKEY", esOrderByDirection.Ascending);

            e.DetailTableView.DataSource = query.LoadDataTable();
        }

        protected void grdPrescriptionVerif_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var grd = (RadGrid)source;

            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grd.DataSource = new String[] { };
                return;
            }

            var dataSource = TransPrescriptionVerifs;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }
        }

        private DataTable TransPrescriptionVerifs
        {
            get
            {
                var isEmptyFilter = string.IsNullOrEmpty(cboRegistrationType.SelectedValue) && string.IsNullOrEmpty(cboDispensaryID2.SelectedValue) &&
                    string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) && string.IsNullOrEmpty(cboParamedicID.SelectedValue) && string.IsNullOrEmpty(txtRegistrationNo.Text) &&
                    string.IsNullOrEmpty(txtPatientName.Text) && txtPrescriptionDate2.IsEmpty && txtVerificationDate.IsEmpty && string.IsNullOrEmpty(txtPrescriptionNo2.Text) && 
                    string.IsNullOrEmpty(cboPrescriptionType2.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Prescription")) return null;

                var presc = new TransPrescriptionQuery("a");
                var reg = new RegistrationQuery("b");
                var patient = new PatientQuery("c");
                var medic = new ParamedicQuery("d");
                var unit = new ServiceUnitQuery("e");
                var sal = new AppStandardReferenceItemQuery("sal");
                var disp = new ServiceUnitQuery("disp");
                var usr = new AppUserQuery("usr");
                var usr2 = new AppUserQuery("appr");
                var guar = new GuarantorQuery("guar");

                presc.es.Distinct = true;

                presc.Select
                    (
                        presc.PrescriptionNo,
                        presc.PrescriptionDate,
                        presc.RegistrationNo,
                        patient.MedicalNo,
                        patient.PatientName,
                        patient.Sex,
                        reg.ServiceUnitID,
                        unit.ServiceUnitName,
                        presc.ParamedicID,
                        medic.ParamedicName,
                        presc.Note,
                        presc.ApprovalDateTime,
                        presc.IsProceedByPharmacist,
                        presc.CompleteDateTime,
                        presc.DeliverDateTime,
                        "<0 AS Status>",
                        sal.ItemName.As("SalutationName"),
                        disp.ServiceUnitName.As("DispensaryName"), 
                        usr.UserName.As("VerifiedByUserName"),
                        presc.VerifiedDateTime,
                        usr2.UserName.As("ApprovedByUserName"), 
                        guar.GuarantorName
                    );

                presc.InnerJoin(reg).On(reg.RegistrationNo == presc.RegistrationNo);
                presc.InnerJoin(patient).On(patient.PatientID == reg.PatientID);
                presc.InnerJoin(unit).On(unit.ServiceUnitID == reg.ServiceUnitID);
                presc.LeftJoin(medic).On(medic.ParamedicID == presc.ParamedicID);
                presc.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & patient.SRSalutation == sal.ItemID);
                presc.InnerJoin(disp).On(disp.ServiceUnitID == presc.ServiceUnitID);
                presc.LeftJoin(usr).On(usr.UserID == presc.VerifiedByUserID);
                presc.LeftJoin(usr2).On(usr2.UserID == presc.ApprovedByUserID);
                presc.LeftJoin(guar).On(guar.GuarantorID == reg.GuarantorID);

                // Bug fix: Jika dipilih type UDD jadi bentrok dgn filter presc.IsUnitDosePrescription == true yg dibawah (Handono 230405)
                //presc.Where(presc.IsApproval == true, presc.IsVoid == false,
                //    presc.IsUnitDosePrescription == false, presc.Or(presc.IsPos.IsNull(), presc.IsPos == false),
                //    presc.IsVerified == true);

                presc.Where(presc.IsApproval == true, presc.IsVoid == false,
                    presc.Or(presc.IsPos.IsNull(), presc.IsPos == false),
                    presc.IsVerified == true);

                bool IsFilterMaxResultRecord = true;
                if (!string.IsNullOrEmpty(cboRegistrationType.SelectedValue))
                    presc.Where(reg.SRRegistrationType == cboRegistrationType.SelectedValue);
                if (!string.IsNullOrEmpty(cboDispensaryID2.SelectedValue))
                {
                    presc.Where(presc.ServiceUnitID == cboDispensaryID2.SelectedValue);
                    IsFilterMaxResultRecord = false;
                }
                if (cboServiceUnitID.SelectedValue != string.Empty)
                    presc.Where(reg.ServiceUnitID == cboServiceUnitID.SelectedValue);
                if (cboParamedicID.SelectedValue != string.Empty)
                    presc.Where(presc.ParamedicID == cboParamedicID.SelectedValue);
                if (txtRegistrationNo.Text != string.Empty)
                {
                    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                    presc.Where(
                        presc.Or(
                            string.Format("<a.RegistrationNo = '{0}' OR >", searchReg),
                            string.Format("<c.MedicalNo = '{0}' OR >", searchReg),
                            string.Format("<c.OldMedicalNo = '{0}'>", searchReg),
                            string.Format("< OR REPLACE(c.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg),
                            string.Format("< OR REPLACE(c.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchReg)
                            )
                        );
                }

                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
                    presc.Where
                        (
                          string.Format("<LTRIM(RTRIM(LTRIM(c.FirstName + ' ' + c.MiddleName)) + ' ' + c.LastName) LIKE '{0}'>", searchPatient)
                        );
                }
                if (!txtPrescriptionDate2.IsEmpty)
                {
                    presc.Where(presc.PrescriptionDate >= txtPrescriptionDate2.SelectedDate, presc.PrescriptionDate < txtPrescriptionDate2.SelectedDate.Value.AddDays(1));
                    IsFilterMaxResultRecord = false;
                }
                if (!txtVerificationDate.IsEmpty)
                {
                    presc.Where(presc.VerifiedDateTime >= txtVerificationDate.SelectedDate, presc.VerifiedDateTime < txtVerificationDate.SelectedDate.Value.AddDays(1));
                    IsFilterMaxResultRecord = false;
                }
                if (txtPrescriptionNo2.Text != string.Empty)
                    presc.Where(presc.PrescriptionNo == Helper.EscapeQuery(txtPrescriptionNo2.Text));
                if (!string.IsNullOrEmpty(cboPrescriptionType2.SelectedValue))
                {
                    switch (cboPrescriptionType2.SelectedValue)
                    {
                        case "1":
                            {
                                presc.Where(presc.IsFromSOAP == true, presc.IsPrescriptionReturn == false, presc.IsUnitDosePrescription == false);
                                break;
                            }
                        case "2":
                            {
                                presc.Where(presc.IsFromSOAP == false, presc.IsPrescriptionReturn == false, presc.IsUnitDosePrescription == false);
                                break;
                            }
                        case "3":
                            {
                                presc.Where(presc.IsFromSOAP == false, presc.IsPrescriptionReturn == false, presc.IsUnitDosePrescription == true);
                                break;
                            }
                        case "4":
                            {
                                presc.Where(presc.IsPrescriptionReturn == true);
                                break;
                            }
                    }
                }
                if (IsFilterMaxResultRecord)
                    presc.es.Top = AppSession.Parameter.MaxResultRecord;

                presc.OrderBy(presc.PrescriptionNo.Descending);

                DataTable dtbl = presc.LoadDataTable();

                foreach (DataRow row in dtbl.Rows)
                {
                    row["Status"] = !(row["DeliverDateTime"] is DBNull) ? 3 :
                        (!(row["CompleteDateTime"] is DBNull) ? 2 : (!(row["IsProceedByPharmacist"] is DBNull) ? 1 : 0));
                }
                dtbl.AcceptChanges();

                return dtbl;
            }
        }

        protected void grdPrescriptionVerif_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            var query = new TransPrescriptionItemQuery("a");
            var qItem = new ItemQuery("b");
            var qItemI = new ItemQuery("c");

            var emb = new EmbalaceQuery("x");
            var cons = new ConsumeMethodQuery("y");

            query.Select
                (
                    query,
                    qItem.ItemName.As("ItemName"),
                    qItemI.ItemName.As("ItemInterventionName"),
                    "<(a.ParentNo + a.SequenceNo) as ORDERKEY>",
                    emb.EmbalaceLabel.As("EmbalaceLabel"),
                    cons.SRConsumeMethodName.As("SRConsumeMethodName")
                );
            query.InnerJoin(qItem).On(query.ItemID == qItem.ItemID);
            query.LeftJoin(qItemI).On(query.ItemInterventionID == qItemI.ItemID);
            query.LeftJoin(emb).On(query.EmbalaceID == emb.EmbalaceID);
            query.LeftJoin(cons).On(query.SRConsumeMethod == cons.SRConsumeMethod);
            query.Where(query.PrescriptionNo == e.DetailTableView.ParentItem.GetDataKeyValue("PrescriptionNo").ToString());
            query.OrderBy("ORDERKEY", esOrderByDirection.Ascending);

            e.DetailTableView.DataSource = query.LoadDataTable();
        }

        protected string GetItemName(object isRFlag, object itemName)
        {
            if (isRFlag.Equals(true))
                return itemName.ToString();
            else
                return "&nbsp;&nbsp;&nbsp;" + itemName.ToString();
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            SaveValueToCookie();

            grdPrescription.Rebind();
            grdPrescriptionVerif.Rebind();
        }

        protected void btnFilterVerif_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            SaveValueToCookie();

            grdPrescription.Rebind();
            grdPrescriptionVerif.Rebind();
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (string.IsNullOrEmpty(eventArgument))
                return;

            if (!(sourceControl is RadGrid))
                return;

            if (eventArgument == "rebind")
                grdPrescription.Rebind();

            else if (eventArgument.Contains("|"))
            {
                var param = eventArgument.Split('|');
                var parameter = param[1];

                var pres = new TransPrescription();
                if (pres.LoadByPrimaryKey(parameter))
                {
                    pres.IsVerified = true;
                    pres.VerifiedByUserID = AppSession.UserLogin.UserID;
                    pres.VerifiedDateTime = (new DateTime()).NowAtSqlServer();
                    pres.Save();
                }

                grdPrescription.DataSource = TransPrescriptions;
                grdPrescription.DataBind();
                grdPrescriptionVerif.DataSource = TransPrescriptionVerifs;
                grdPrescriptionVerif.DataBind();
            }
        }

        protected void cboRegistrationType_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.StandardReferenceItemsRequested((RadComboBox)sender, "RegistrationType", e.Text);
        }
        protected void cboRegistrationType_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.StandardReferenceItemDataBound(e);
        }
    }
}