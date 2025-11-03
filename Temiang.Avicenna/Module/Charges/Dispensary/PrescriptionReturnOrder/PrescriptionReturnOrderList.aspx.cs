using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using System.Web.UI;
using Temiang.Dal.DynamicQuery;
using System.Web.UI.WebControls;
using System.Linq;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class PrescriptionReturnOrderList : BasePage
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

            ProgramID = AppConstant.Program.PrescriptionReturnOrder;

            if (!IsPostBack)
            {
                //service unit
                ServiceUnitCollection unit = new ServiceUnitCollection();
                ServiceUnitQuery query = new ServiceUnitQuery("a");
                var usr = new AppUserServiceUnitQuery("b");
                query.InnerJoin(usr).On(usr.UserID == AppSession.UserLogin.UserID &&
                                        usr.ServiceUnitID == query.ServiceUnitID);
                query.Where
                    (
                        query.SRRegistrationType.In(
                            AppConstant.RegistrationType.EmergencyPatient,
                            AppConstant.RegistrationType.InPatient,
                            AppConstant.RegistrationType.OutPatient,
                            AppConstant.RegistrationType.MedicalCheckUp
                        ),
                        query.IsActive == true
                    );
                query.OrderBy(unit.Query.ServiceUnitName.Ascending);
                unit.Load(query);

                cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (ServiceUnit entity in unit)
                {
                    cboServiceUnitID.Items.Add(new RadComboBoxItem(entity.ServiceUnitName, entity.ServiceUnitID));
                }

                //paramedic
                ParamedicCollection param = new ParamedicCollection();
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
                txtPrescOrderDate.SelectedDate = DateTime.Now;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack) RestoreValueFromCookie();
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

        protected void grdRegistration_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var grd = (RadGrid)source;

            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grd.DataSource = new String[] { };
                return;
            }

            var dataSource = Registrations;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }
        }

        private DataTable Registrations
        {
            get
            {
                var isEmptyFilter = string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) && string.IsNullOrEmpty(cboParamedicID.SelectedValue) &&
                    string.IsNullOrEmpty(txtRegistrationNo.Text) && string.IsNullOrEmpty(txtPatientName.Text);
                if (!ValidateSearch(isEmptyFilter, "Registration")) return null;

                var query = new RegistrationQuery("e");
                var unit = new ServiceUnitQuery("b");
                var room = new ServiceRoomQuery("c");
                var medic = new ParamedicQuery("d");

                var patient = new PatientQuery("f");
                var grr = new GuarantorQuery("g");
                var sal = new AppStandardReferenceItemQuery("sal");
                var usr = new AppUserServiceUnitQuery("usr");

                query.es.Distinct = true;
                query.es.Top = AppSession.Parameter.MaxResultRecord;

                query.Select
                    (
                        room.RoomName,
                        query.RegistrationDate,
                        "<0 AS QueNo>",
                        medic.ParamedicName,
                        query.RegistrationNo,
                        patient.MedicalNo,
                        patient.PatientName,
                        patient.Sex,
                        query.BedID,
                        unit.ServiceUnitName,
                        query.ServiceUnitID,
                        query.ParamedicID,
                        sal.ItemName.As("SalutationName")
                    );

                query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);
                query.LeftJoin(room).On(query.RoomID == room.RoomID);
                query.LeftJoin(medic).On(query.ParamedicID == medic.ParamedicID);
                query.InnerJoin(patient).On(query.PatientID == patient.PatientID);
                query.InnerJoin(grr).On(query.GuarantorID == grr.GuarantorID);
                query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & patient.SRSalutation == sal.ItemID);
                query.InnerJoin(usr).On(usr.UserID == AppSession.UserLogin.UserID &&
                                        usr.ServiceUnitID == query.ServiceUnitID);

                if (cboServiceUnitID.SelectedValue != string.Empty)
                    query.Where(query.ServiceUnitID == cboServiceUnitID.SelectedValue);
                if (cboParamedicID.SelectedValue != string.Empty)
                    query.Where(query.ParamedicID == cboParamedicID.SelectedValue);
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



                    //query.Where(
                    //    query.Or(
                    //        query.RegistrationNo == searchReg,
                    //        patient.MedicalNo == searchReg,
                    //        patient.OldMedicalNo == searchReg,
                    //        string.Format("< OR REPLACE(f.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg),
                    //        string.Format("< OR REPLACE(f.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchReg)
                    //        )
                    //    );
                    
                    //Helper.AddFilterMedNoOrRegNoOrPatName(query, patient, searchReg, "registration");
                }
                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
                    query.Where
                        (
                            patient.FullName.Like(searchPatient)
                            //string.Format("<LTRIM(RTRIM(LTRIM(f.FirstName + ' ' + f.MiddleName)) + ' ' + f.LastName) LIKE '{0}'>", searchPatient)
                        );
                }

                query.Where
                    (
                        query.IsClosed == false,
                        query.IsHoldTransactionEntry == false,
                        query.IsNonPatient == false
                    );

                query.OrderBy
                    (
                        query.RegistrationDate.Descending,
                        query.ServiceUnitID.Ascending,
                        query.ParamedicID.Ascending
                    );

                return query.LoadDataTable();
            }
        }

        protected void grdOrder_ItemCommand(object source, GridCommandEventArgs e) {
            if (e.CommandName == "PrintOrder")
            {
                PrintJobParameterCollection jobParameters = new PrintJobParameterCollection();
                PrintJobParameter jobParameter;

                jobParameter = jobParameters.AddNew();
                jobParameter.Name = "p_OrderNo";
                jobParameter.ValueString = e.CommandArgument.ToString();

                AppSession.PrintJobReportID = AppSession.Parameter.PrescriptionOrderSlipID;
                AppSession.PrintJobParameters = jobParameters;

                string script = @"var oWnd = $find('" + winPrint.ClientID + "');" +
                "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
                "oWnd.Show();" +
                "oWnd.Maximize();";
                RadAjaxPanel1.ResponseScripts.Add(script);
            }
        }

        protected void grdOrder_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
            {
                grdOrder.DataSource = TransPrescriptionOrders;
            }
        }

        private DataTable TransPrescriptionOrders
        {
            get
            {
                var isEmptyFilter = string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) && string.IsNullOrEmpty(cboParamedicID.SelectedValue) &&
                 string.IsNullOrEmpty(txtRegistrationNo.Text) && string.IsNullOrEmpty(txtPatientName.Text) && txtPrescOrderDate.IsEmpty && string.IsNullOrEmpty(txtPrescOrderNo.Text);
                if (!ValidateSearch(isEmptyFilter, "Patient")) return null;

                var pod = new TransPrescriptionOrderQuery("pod");
                var reg = new RegistrationQuery("reg");
                var pat = new PatientQuery("pat");
                var su = new ServiceUnitQuery("su");
                var par = new ParamedicQuery("par");

                pod.es.Top = AppSession.Parameter.MaxResultRecord;
                pod.InnerJoin(reg).On(pod.RegistrationNo == reg.RegistrationNo)
                    .InnerJoin(pat).On(reg.PatientID == pat.PatientID)
                    .InnerJoin(su).On(reg.ServiceUnitID == su.ServiceUnitID)
                    .InnerJoin(par).On(reg.ParamedicID == par.ParamedicID)
                    .Select(pod,
                        pat.MedicalNo,
                        pat.PatientName,
                        su.ServiceUnitName,
                        par.ParamedicName
                    );

                if (cboServiceUnitID.SelectedValue != string.Empty)
                {
                    pod.Where(reg.ServiceUnitID == cboServiceUnitID.SelectedValue);
                }
                if (cboParamedicID.SelectedValue != string.Empty) {
                    pod.Where(reg.ParamedicID == cboParamedicID.SelectedValue);
                }
                    
                if (txtRegistrationNo.Text != string.Empty)
                {
                    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                    var reverseMedNoSearch = string.Format("{0}%", searchReg.Replace("-", "").Reverse());

                    pod.Where(
                        pod.Or(
                            pod.RegistrationNo == searchReg,
                            pat.ReverseMedicalNo.Like(reverseMedNoSearch),
                            pat.ReverseOldMedicalNo.Like(reverseMedNoSearch)
                            )
                        );



                    //if (Helper.IsNumeric(txtRegistrationNo.Text.Replace("-", "")))
                    //{
                    //    if (AppSession.Parameter.IsMedicalNoContainStrip)
                    //        // norm
                    //        //pod.InnerJoin(pat).On(reg.PatientID == pat.PatientID)
                    //            pod.Where(reg.Or(
                    //                string.Format("< REPLACE(pat.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg),
                    //                string.Format("< OR REPLACE(pat.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchReg)
                    //            )
                    //        );
                    //    else
                    //        pod.Where(reg.Or(
                    //                string.Format("< pat.MedicalNo LIKE '%{0}%'>", searchReg),
                    //                string.Format("< OR pat.OldMedicalNo LIKE '%{0}%'>", searchReg)
                    //            )
                    //        );
                    //}
                    //else {
                    //    // noreg
                    //    pod.Where(pod.RegistrationNo == searchReg);
                    //}
                }
                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
                    pod.Where
                        (
                            pat.FullName.Like(searchPatient)
                            //string.Format("<LTRIM(RTRIM(LTRIM(pat.FirstName + ' ' + pat.MiddleName)) + ' ' + pat.LastName) LIKE '{0}'>", searchPatient)
                        );
                }
                if (!txtPrescOrderDate.IsEmpty)
                    pod.Where(pod.OrderDate.Date() == txtPrescOrderDate.SelectedDate);
                if (txtPrescOrderNo.Text != string.Empty)
                    pod.Where(pod.OrderNo == txtPrescOrderNo.Text);

                pod.OrderBy(pod.OrderNo.Descending);

                DataTable dtbl = pod.LoadDataTable();

                return dtbl;
            }
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

            grdRegistration.Rebind();
            grdOrder.Rebind();
        }

        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            bool selected = ((CheckBox)sender).Checked;

            foreach (GridDataItem dataItem in grdOrder.MasterTableView.Items)
            {
                var chk = ((CheckBox)dataItem.FindControl("detailChkbox"));
                if (chk.Visible) chk.Checked = selected;
            }
        }

        protected void btnPrint_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (grdOrder.MasterTableView.Items.Count == 0) return;

            string param = string.Empty;
            foreach (var item in grdOrder.MasterTableView.Items.Cast<GridDataItem>().Where(i => (i.FindControl("detailChkbox") as CheckBox).Checked))
            {
                param += item["PrescriptionNo"].Text + ",";
            }
            param = param.Remove(param.Length - 1, 1);

            var printJobParameters = new PrintJobParameterCollection();
            printJobParameters.AddNew("p_PrescriptionNo", param);
            printJobParameters.AddNew("p_Label", "");
            printJobParameters.AddNew("temp_TITLE", "RETUR RESEP FARMASI");

            AppSession.PrintJobParameters = printJobParameters;
            AppSession.PrintJobReportID = AppConstant.Report.RSSA_PrescriptionReturnReceiptDetailSlip;

            string script = @"var oWnd = $find('" + winCharges.ClientID + "');" +
            "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
            "oWnd.Show();" +
            "oWnd.Maximize();";
            RadAjaxPanel1.ResponseScripts.Add(script);
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (sourceControl is RadGrid && eventArgument == "rebind")
            {
                grdRegistration.Rebind();
                grdOrder.Rebind();
            }

            if (sourceControl is RadGrid)
            {
                var arg = eventArgument.Split((new string[] { "|" }), StringSplitOptions.RemoveEmptyEntries);
                if (arg.Length == 2)
                {
                    if (arg[0] == "voidOrder")
                    {
                        var odr = new TransPrescriptionOrder();
                        if (odr.LoadByPrimaryKey(arg[1]))
                        {
                            if (!(odr.IsClosed ?? false) && !(odr.IsVoid ?? false))
                            {
                                odr.IsVoid = true;
                                odr.IsClosed = true;
                                odr.VoidDate = DateTime.Now;
                                odr.VoidBy = AppSession.UserLogin.UserID;
                                odr.LastUpdateDateTime = DateTime.Now;
                                odr.LastUpdateBy = AppSession.UserLogin.UserID;
                                odr.Save();
                            }
                        }
                    }
                    grdOrder.Rebind();
                }
            }
        }
    }
}