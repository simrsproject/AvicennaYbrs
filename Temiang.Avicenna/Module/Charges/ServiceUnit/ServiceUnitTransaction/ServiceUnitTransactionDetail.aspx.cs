using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web;
using Telerik.Web.Data.Extensions;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Avicenna.Common;
using Temiang.Dal.Core;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class ServiceUnitTransactionDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber, _amplopFilmAutoNumber;
        private string _healthcareInitial, _paramedicIdDokterLuar;

        private string OrderType
        {
            get
            {
                // LAB, RAD, OTH, PAT
                return Request.QueryString["ordertype"];
            }
        }

        private bool IsCalledFromEmr
        {
            get
            {
                if (Request.QueryString["emr"] == null)
                    return false;

                return Request.QueryString["emr"].Equals("1");
            }
        }

        public override string OnGetCloseScript()
        {
            // Refresh grid after close for edit or not edit
            return "CloseAndApply();args.set_cancel(true);";
        }

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            if (IsCalledFromEmr)
            {
                IsSingleRecordMode = true;
                ProgramID = AppConstant.Program.ElectronicMedicalRecord;

                // ProgramReferenceID untuk filter report list
                switch (Request.QueryString["type"])
                {
                    case "tr":
                        ProgramReferenceID = Request.QueryString["disch"] == "0" ? AppConstant.Program.ServiceUnitTransaction : AppConstant.Program.ServiceUnitTransactionVerification;
                        break;
                    case "jo":
                        ProgramReferenceID = Request.QueryString["disch"] == "0" ? AppConstant.Program.JobOrderTransaction : AppConstant.Program.JobOrderTransactionForCashier;
                        break;
                    case "ds":
                        ProgramReferenceID = Request.QueryString["disch"] == "0" ? AppConstant.Program.DiagnosticSupportTransaction : AppConstant.Program.DiagnosticSupportTransactionVerification;
                        break;
                    case "mcu":
                        ProgramReferenceID = AppConstant.Program.HealthScreeningTransaction;
                        break;
                }
            }
            else
            {
                // Url Search & List 
                UrlPageSearch = "ServiceUnitTransactionSearch.aspx?fp=dt&type=" + Request.QueryString["type"];
                if (Request.QueryString["verif"] == "0")
                    UrlPageList = "ServiceUnitRegistrationList.aspx?type=" + Request.QueryString["type"] + "&pid=" +
                                  Request.QueryString["pid"] + "&cid=" + Request.QueryString["cid"] + "&resp=" +
                                  Request.QueryString["resp"] + "&disch=" + Request.QueryString["disch"];
                else
                    UrlPageList = "../../Billing/FinalizeBilling/FinalizeBillingVerification.aspx?regNo=" + Request.QueryString["regno"] + "&regType=" + Request.QueryString["type"] + "&md=new&from=1";


                switch (Request.QueryString["type"])
                {
                    case "tr":
                        ProgramID = Request.QueryString["disch"] == "0" ? AppConstant.Program.ServiceUnitTransaction : AppConstant.Program.ServiceUnitTransactionVerification;
                        break;
                    case "jo":
                        ProgramID = Request.QueryString["disch"] == "0" ? AppConstant.Program.JobOrderTransaction : AppConstant.Program.JobOrderTransactionForCashier;
                        break;
                    case "ds":
                        ProgramID = Request.QueryString["disch"] == "0" ? AppConstant.Program.DiagnosticSupportTransaction : AppConstant.Program.DiagnosticSupportTransactionVerification;
                        break;
                    case "mcu":
                        ProgramID = AppConstant.Program.HealthScreeningTransaction;
                        break;
                }
            }

            WindowSearch.Height = 330;

            _healthcareInitial = AppSession.Parameter.HealthcareInitialAppsVersion;
            _paramedicIdDokterLuar = AppSession.Parameter.ParamedicIdDokterLuar;

            if (!IsPostBack)
            {
                hdnPageId.Value = PageID;

                TransChargesItems = null; // Reset record detail untuk menghindari akibat PageID masih sama dgn yg sebelumnya (Handono 231109)

                var reg = new Registration();
                reg.LoadByPrimaryKey(Request.QueryString["regno"]);

                tdLengthOfStay.Visible = reg.SRRegistrationType == AppConstant.RegistrationType.InPatient;
                txtLengthOfStay.Visible = tdLengthOfStay.Visible;
                lblLengthOfStayDays.Visible = tdLengthOfStay.Visible;
                if (tdLengthOfStay.Visible)
                {
                    var x = reg.DischargeDate != null ? reg.DischargeDate.Value.Date : (new DateTime()).NowAtSqlServer().Date;
                    var y = reg.RegistrationDate.Value.Date;
                    txtLengthOfStay.Value = (x - y).TotalDays == 0 ? 1 : (x - y).TotalDays + 1;
                }

                pnlResponUnit.Visible = Request.QueryString["resp"] == "1";

                if (pnlResponUnit.Visible)
                    ComboBox.PopulateWithServiceUnitForTransaction(cboResponUnit, true);

                trClinicalDiagnosis.Visible = Request.QueryString["type"] == "ds" && AppSession.Parameter.IsVisibleClinicalDiagnosisOnJobOrderRealization;

                if (Request.QueryString["type"] == "jo" || Request.QueryString["type"] == "ds")
                {
                    ComboBox.PopulateWithServiceUnitForTransactionJO(cboFromServiceUnitID, TransactionCode.JobOrder, true);

                    if (Request.QueryString["type"] == "jo")
                    {
                        if (_healthcareInitial == "RSSA" && Request.QueryString["disch"] != "1")
                            ComboBox.PopulateWithServiceUnitForTransactionJOLab(cboToServiceUnitID);
                        else if (OrderType == "LAB" || OrderType == "RAD" || OrderType == "OTH" || OrderType == "PAT")
                        {
                            var query = new ServiceUnitQuery("a");
                            query.Select(query.ServiceUnitID, query.ServiceUnitName);
                            switch (OrderType)
                            {
                                case "LAB":
                                    {
                                        query.Where(query.ServiceUnitID == AppSession.Parameter.ServiceUnitLaboratoryID);
                                        break;
                                    }
                                case "RAD":
                                    {
                                        var radOthers = AppSession.Parameter.ServiceUnitRadiologyIDs;
                                        if (radOthers.Contains(";"))
                                            query.Where(query.Or(query.ServiceUnitID == AppSession.Parameter.ServiceUnitRadiologyID,
                                                query.ServiceUnitID == AppSession.Parameter.ServiceUnitRadiologyID2,
                                                query.ServiceUnitID.In(radOthers.Split(';')),
                                                query.ServiceUnitID.In(AppSession.Parameter.ServiceUnitRadiologyIdArray)));
                                        else
                                            query.Where(query.Or(query.ServiceUnitID == AppSession.Parameter.ServiceUnitRadiologyID,
                                                query.ServiceUnitID == AppSession.Parameter.ServiceUnitRadiologyID2,
                                                query.ServiceUnitID == radOthers,
                                                query.ServiceUnitID.In(AppSession.Parameter.ServiceUnitRadiologyIdArray)));

                                        break;
                                    }
                                case "PAT":
                                    {
                                        query.Where(query.ServiceUnitID == AppSession.Parameter.ServiceUnitPathologyAnatomyID);
                                        break;
                                    }
                                case "OTH":
                                    {
                                        var tc = new ServiceUnitTransactionCodeQuery("tc");
                                        query.InnerJoin(tc).On(query.ServiceUnitID == tc.ServiceUnitID);
                                        query.Where
                                        (
                                            tc.SRTransactionCode == TransactionCode.JobOrder,
                                            query.IsActive == true,
                                            query.IsUsingJobOrder == true
                                        );

                                        query.Where(query.ServiceUnitID != AppSession.Parameter.ServiceUnitLaboratoryID, query.ServiceUnitID != AppSession.Parameter.ServiceUnitPathologyAnatomyID);
                                        var radOthers = AppSession.Parameter.ServiceUnitRadiologyIDs;
                                        if (radOthers.Contains(";"))
                                            query.Where(query.ServiceUnitID != AppSession.Parameter.ServiceUnitRadiologyID,
                                                query.ServiceUnitID != AppSession.Parameter.ServiceUnitRadiologyID2,
                                                query.ServiceUnitID.NotIn(radOthers.Split(';')));
                                        else
                                            query.Where(query.ServiceUnitID != AppSession.Parameter.ServiceUnitRadiologyID,
                                                query.ServiceUnitID != AppSession.Parameter.ServiceUnitRadiologyID2,
                                                query.ServiceUnitID != radOthers);

                                        break;
                                    }
                            }
                            var dtb = query.LoadDataTable();
                            cboToServiceUnitID.Items.Clear();
                            foreach (DataRow item in dtb.Rows)
                                cboToServiceUnitID.Items.Add(new RadComboBoxItem(item["ServiceUnitName"].ToString(), item["ServiceUnitID"].ToString()));
                        }
                        else
                            ComboBox.PopulateWithServiceUnitForTransactionJO(cboToServiceUnitID, TransactionCode.JobOrder, false);
                    }
                    else
                    {
                        //if (_healthcareInitial == "RSSA")
                        //    ComboBox.PopulateWithServiceUnitForTransaction(cboToServiceUnitID, TransactionCode.JobOrder, Request.QueryString["disch"] != "1");
                        //else
                        //    ComboBox.PopulateWithServiceUnitForTransactionJO(cboToServiceUnitID, TransactionCode.JobOrder, false);
                        ComboBox.PopulateWithServiceUnitForTransaction(cboToServiceUnitID, TransactionCode.JobOrder, Request.QueryString["disch"] == "0");
                    }

                    pnlJobOrder.Visible = true;
                    pnlJobOrder2.Visible = true;
                    pnlJobOrder3.Visible = Request.QueryString["type"] == "jo";
                    TrBloodSampleTakenBy.Visible = Request.QueryString["type"] == "jo" && AppSession.Parameter.IsNeedSpecimenOnJo;
                    pnlSurgeryPackage.Visible = false;
                    trPhysicianSenders.Visible = true;
                    if (Request.QueryString["type"] == "ds")
                    {
                        cboPhysicianSender.Visible = !AppSession.Parameter.IsAncillaryServicePhysicianSenderFreeText;
                        rfvPhysicianSenders.Visible = cboPhysicianSender.Visible;
                        txtPhysicianSenders.Visible = AppSession.Parameter.IsAncillaryServicePhysicianSenderFreeText;
                        rfvTxtPhysicianSenders.Visible = txtPhysicianSenders.Visible;
                    }
                    else
                    {
                        cboPhysicianSender.Visible = true;
                        rfvPhysicianSenders.Visible = true;
                        txtPhysicianSenders.Visible = false;
                        rfvTxtPhysicianSenders.Visible = false;
                    }

                    if (Request.QueryString["type"] == "ds" && !AppSession.Parameter.IsRadiologyNoAutoCreate)
                        TrDiagNo.Visible = true;
                    else TrDiagNo.Visible = false;
                }
                else
                {
                    pnlJobOrder.Visible = false;
                    pnlJobOrder2.Visible = false;
                    pnlJobOrder3.Visible = false;
                    TrBloodSampleTakenBy.Visible = false;
                    pnlSurgeryPackage.Visible = false;

                    if (AppSession.Parameter.IsDisplayServiceUnitBookingNoOnTransactionEntry)
                    {
                        var rooms2 = new ServiceRoomCollection();
                        rooms2.Query.es.Distinct = true;
                        rooms2.Query.Select(rooms2.Query.ServiceUnitID);
                        rooms2.Query.Where(
                            rooms2.Query.IsOperatingRoom == true,
                            rooms2.Query.IsShowOnBookingOT == true,
                            rooms2.Query.IsActive == true,
                            rooms2.Query.ServiceUnitID == Request.QueryString["cid"]
                        );
                        rooms2.LoadAll();

                        pnlServiceUnitBookingNo.Visible = rooms2.Count() > 0;
                        hdnIsMandatoryBookingNo.Value = pnlServiceUnitBookingNo.Visible && AppSession.Parameter.IsVisibleTrProcedureOnBookingRealization &&
                            AppSession.Parameter.IsUsingMappingServiceUnitProcedure ? "y" : "n";
                        if (hdnIsMandatoryBookingNo.Value.ToString() == "y")
                        {
                            lblServiceUnitBookingNo.Visible = false;
                            lblServiceUnitBookingNoMandatory.Visible = true;
                        }

                    }

                    if (AppSession.Parameter.IsDisplayKiaCaseAndObstetricTypeOnTransactionEntry)
                    {
                        pnlKiaCaseType.Visible = (Request.QueryString["cid"] ==
                                                  AppSession.Parameter.ServiceUnitKiaId ||
                                                  Request.QueryString["cid"] ==
                                                  AppSession.Parameter.ServiceUnitImmunizationId);
                        pnlObstetricType.Visible = Request.QueryString["cid"] ==
                                                   AppSession.Parameter.ServiceUnitObstetricsId;
                    }

                    TrDiagNo.Visible = false;
                }

                var unit = new ServiceUnit();
                unit.LoadByPrimaryKey(Request.QueryString["cid"]);
                cboFromServiceUnitID.Items.Add(new RadComboBoxItem(unit.ServiceUnitName, unit.ServiceUnitID));
                if (cboFromServiceUnitID.Items.Count > 1)
                    cboFromServiceUnitID.Items.Remove(cboFromServiceUnitID.Items.Single(c => string.IsNullOrEmpty(c.Value)));

                if (Request.QueryString["type"] == "tr" || Request.QueryString["type"] == "mcu" || Request.QueryString["type"] == "npc")
                {
                    if (!string.IsNullOrEmpty(cboFromServiceUnitID.SelectedValue))
                    {
                        ComboBox.PopulateWithServiceUnitForLocation(cboLocationID, cboFromServiceUnitID.SelectedValue);
                        cboLocationID.SelectedIndex = 1;
                    }
                }

                TransChargesItems = null;
                TransChargesItemComps = null;
                TransChargesItemConsumptions = null;

                TransChargesItemTemporaries = null;
                var tciTemps = TransChargesItemTemporaries;
                TransPrescriptionItemTemporaries = null;
                var tpiTemps = TransPrescriptionItemTemporaries;

                //if ((Request.QueryString["type"] == "tr" || 
                //     Request.QueryString["type"] == "mcu") && 
                //    !pnlResponUnit.Visible && 
                //    Request.QueryString["disch"] == "0" &&
                //    DataModeCurrent == DataMode.New)
                //{
                //    var cstext1 = new StringBuilder();
                //    cstext1.Append("<script type=text/javascript> __doPostBack('ctl00$ContentPlaceHolder1$grdTransChargesItem$ctl00$ctl02$ctl00$lbInsert','') </script>");

                //    Page.ClientScript.RegisterStartupScript(GetType(), "OpenAddNewRecordGrid", cstext1.ToString());
                //}

                if (Request.QueryString["disch"] == "0") cboBedID.Enabled = false;

                if (_healthcareInitial == "RSCH" && Request.QueryString["type"] == "jo") cboFromServiceUnitID.Enabled = true;
                else cboFromServiceUnitID.Enabled = false;

                //if (Request.QueryString["type"] == "ds" &&
                //AppSession.Parameter.IsUsingHisInterop == "Yes" &&
                //AppSession.Parameter.HisInteropConfigName == AppConstant.HIS_INTEROP.PRODIA_LIS_INTEROP_CONNECTION_NAME)
                //{
                //    pnlProdia.Visible = true;
                //}
                //else
                //{
                //    pnlProdia.Visible = false;
                //}

                StandardReference.InitializeIncludeSpace(cboSRProdiaContractID, AppEnum.StandardReference.ProdiaContractID);

                var booking = new ServiceUnitBookingQuery("a");
                var parBooking = new ParamedicQuery("b");
                booking.InnerJoin(parBooking).On(booking.ParamedicID == parBooking.ParamedicID);
                booking.Select(booking.BookingNo, booking.RealizationDateTimeFrom, parBooking.ParamedicName);
                booking.Where(
                    booking.RegistrationNo == Request.QueryString["regno"],
                    booking.IsApproved == true,
                    booking.IsVoid == false);
                booking.OrderBy(booking.BookingNo.Ascending);
                DataTable dtbBooking = booking.LoadDataTable();

                foreach (DataRow item in dtbBooking.Rows)
                {
                    cboServiceUnitBookingNo.Items.Add(new RadComboBoxItem(item["BookingNo"] + " [" +
                        (string.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(item["RealizationDateTimeFrom"]))) +
                          "] " + item["ParamedicName"], item["BookingNo"].ToString()));
                }

                if (AppSession.Parameter.IsTariffPriceVisibleOnlyForAdm)
                {
                    if (string.IsNullOrEmpty(Request.QueryString["verif"]) || (Request.QueryString["verif"] == "0"))
                    {
                        grdTransChargesItem.Columns.FindByUniqueName("Price").Visible = false;
                        grdTransChargesItem.Columns.FindByUniqueName("DiscountAmount").Visible = false;
                        grdTransChargesItem.Columns.FindByUniqueName("CitoAmount").Visible = false;
                        grdTransChargesItem.Columns.FindByUniqueName("Total").Visible = false;
                    }
                }

                if ((Request.QueryString["type"] == "tr" || Request.QueryString["type"] == "mcu") &&
                    !pnlResponUnit.Visible && Request.QueryString["disch"] == "0" &&
                    Request.QueryString["md"] == "new")
                {
                    if (!(Request.QueryString["type"] == "tr" && pnlServiceUnitBookingNo.Visible && AppSession.Parameter.IsVisibleTrProcedureOnBookingRealization && AppSession.Parameter.IsUsingMappingServiceUnitProcedure))
                    {
                        var cstext1 = new StringBuilder();
                        cstext1.Append(@"<script type=text/javascript>
                        function OpenAddNewRecordGrid(){ 
                            __doPostBack('ctl00$ContentPlaceHolder1$grdTransChargesItem$ctl00$ctl02$ctl00$lbInsert',''); 
                            Sys.Application.remove_load(OpenAddNewRecordGrid);
                        } 

                        Sys.Application.add_load(OpenAddNewRecordGrid); 
                        </script>");

                        Page.ClientScript.RegisterStartupScript(GetType(), "OpenAddNewRecordGrid", cstext1.ToString());
                    }
                }
            }

            if (Request.QueryString["type"] == "ds")
            {
                if (AppSession.Parameter.IsUsingHisInterop && AppSession.Parameter.HisInteropConfigName == AppConstant.HIS_INTEROP.LINK_LIS_INTEROP_CONNECTION_NAME) pnlLinkLis.Visible = true;
                else pnlLinkLis.Visible = false;
            }
            else pnlLinkLis.Visible = false;
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            if (Request.QueryString["resp"] == "1")
            {
                ajax.AddAjaxSetting(cboResponUnit, grdTransChargesItem);
                ajax.AddAjaxSetting(grdTransChargesItem, cboResponUnit);
                ajax.AddAjaxSetting(cboResponUnit, cboResponUnit);
                ajax.AddAjaxSetting(cboResponUnit, cboLocationID);
            }

            if (Request.QueryString["type"] == "jo" || Request.QueryString["type"] == "ds")
            {
                ajax.AddAjaxSetting(cboToServiceUnitID, grdTransChargesItem);
                ajax.AddAjaxSetting(grdTransChargesItem, cboToServiceUnitID);
                ajax.AddAjaxSetting(cboToServiceUnitID, cboToServiceUnitID);
                ajax.AddAjaxSetting(cboToServiceUnitID, cboLocationID);

                if (Request.QueryString["type"] == "ds" && !AppSession.Parameter.IsRadiologyNoAutoCreate)
                {
                    ajax.AddAjaxSetting(cboToServiceUnitID, txtDiagnosticNo);
                }

                //if (Request.QueryString["type"] == "ds" && pnlProdia.Visible)
                //{
                //    ajax.AddAjaxSetting(cboToServiceUnitID, cboSRProdiaContractID);
                //    ajax.AddAjaxSetting(cboToServiceUnitID, rfvSRProdiaContractID);
                //}

                ajax.AddAjaxSetting(cboToServiceUnitID, rfvNotes);
            }

            ajax.AddAjaxSetting(grdTransChargesItem, cboBedID);
            ajax.AddAjaxSetting(cboBedID, grdTransChargesItem);
            ajax.AddAjaxSetting(cboBedID, cboFromServiceUnitID);
            ajax.AddAjaxSetting(cboBedID, txtClassID);
            ajax.AddAjaxSetting(cboBedID, lblClassName);
            ajax.AddAjaxSetting(cboBedID, txtRoomID);
            ajax.AddAjaxSetting(cboBedID, lblRoomName);
            ajax.AddAjaxSetting(cboBedID, txtTariffDiscForRoomIn);
            ajax.AddAjaxSetting(cboBedID, chkIsRoomIn);
            ajax.AddAjaxSetting(cboBedID, cboBedID);
            ajax.AddAjaxSetting(cboBedID, cboLocationID);
            ajax.AddAjaxSetting(cboBedID, txtRegistrationNo);
            ajax.AddAjaxSetting(cboBedID, cboParamedicID);
            ajax.AddAjaxSetting(cboBedID, lblRegistrationInfo2);
            ajax.AddAjaxSetting(cboBedID, txtRegistrationDate);
            ajax.AddAjaxSetting(cboBedID, txtRegistrationTime);
            ajax.AddAjaxSetting(cboBedID, trPASUS);
            ajax.AddAjaxSetting(cboBedID, lblPASUS);

            ajax.AddAjaxSetting(grdTransChargesItem, grdTransChargesItem);
            //ajax.AddAjaxSetting(txtBarcodeEntry, txtBarcodeEntry);
            //ajax.AddAjaxSetting(txtBarcodeEntry, grdTransChargesItem);

            if (Request.QueryString["type"] == "ds" && pnlLinkLis.Visible) // Jika Visible false akan error di javascript nya
            {
                ajax.AddAjaxSetting(cboToServiceUnitID, cboPhysicianIDPathology);
                ajax.AddAjaxSetting(cboToServiceUnitID, cboAnalystID);
            }

            if (tblTemporaryBill.Visible)
            {
                ajax.AddAjaxSetting(grdTransChargesItem, txtTemporaryBillTotal);
            }
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            switch (programID)
            {
                case AppConstant.Report.JobOrderNotes:
                    printJobParameters.AddNew("TransactionNo", txtTransactionNo.Text);
                    break;
                case AppConstant.Report.JobOrderNotes2:
                    printJobParameters.AddNew("TransactionNo", txtTransactionNo.Text);
                    break;
                case AppConstant.Report.TestReceipt:
                    printJobParameters.AddNew("TransactionNo", txtTransactionNo.Text);
                    break;
                case AppConstant.Report.AncillaryStruk:
                    printJobParameters.AddNew("TransactionNo", txtTransactionNo.Text);
                    break;
                case AppConstant.Report.TransactionReceipt:
                    printJobParameters.AddNew("TransactionNo", txtTransactionNo.Text);

                    var usr = new AppUser();
                    usr.LoadByPrimaryKey(AppSession.UserLogin.UserID);

                    printJobParameters.AddNew("UserName", usr.UserName);
                    break;
                case AppConstant.Report.BillingStatementOutpatient:
                    printJobParameters.AddNew("p_RegistrationNo", txtRegistrationNo.Text);
                    printJobParameters.AddNew("p_TransactionNo", txtTransactionNo.Text);

                    var u = new AppUser();
                    u.LoadByPrimaryKey(AppSession.UserLogin.UserID);

                    printJobParameters.AddNew("p_UserName", u.UserName);
                    break;
                case AppConstant.Report.CetakAmplopOrLabelDiagnostik:
                case AppConstant.Report.CetakAmplopAudiometri:
                case AppConstant.Report.CetakAmplopSpirometri:
                    if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSCH")
                    {
                        foreach (var transChargesItem in TransChargesItems)
                        {
                            if (transChargesItem.IsPackage == true)
                            {
                                var detailQ = new TransChargesItemQuery("a");
                                var headerQ = new TransChargesQuery("b");
                                var itemQ = new ItemQuery("c");

                                detailQ.Select(detailQ.TransactionNo, detailQ.SequenceNo, itemQ.Notes);
                                detailQ.InnerJoin(headerQ).On(detailQ.TransactionNo == headerQ.TransactionNo);
                                detailQ.InnerJoin(itemQ).On(detailQ.ItemID == itemQ.ItemID && itemQ.Notes.Length() > 0);
                                detailQ.Where(headerQ.PackageReferenceNo == transChargesItem.TransactionNo,
                                              detailQ.IsPackage == false);
                                DataTable detailDt = detailQ.LoadDataTable();
                                foreach (DataRow row in detailDt.Rows)
                                {
                                    var tci = new TransChargesItem();
                                    if (tci.LoadByPrimaryKey(row["TransactionNo"].ToString(), row["SequenceNo"].ToString()))
                                    {
                                        if (string.IsNullOrEmpty(tci.FilmNo))
                                        {
                                            _amplopFilmAutoNumber = Helper.GetNewAutoNumber(txtTransactionDate.SelectedDate.Value.Date,
                                                     AppEnum.AutoNumber.AmplopFilmNo,
                                                     row["Notes"].ToString().Length >= 3 ? row["Notes"].ToString().Substring(0, 3).ToUpper() : row["Notes"].ToString().ToUpper(),
                                                     AppSession.UserLogin.UserID);

                                            var filmNo = _amplopFilmAutoNumber.LastCompleteNumber;
                                            _amplopFilmAutoNumber.Save();

                                            tci.FilmNo = filmNo;
                                            tci.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                            tci.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                                            tci.Save();
                                        }
                                    }
                                }
                            }
                            else if (string.IsNullOrEmpty(transChargesItem.FilmNo))
                            {
                                var item = new Item();
                                item.LoadByPrimaryKey(transChargesItem.ItemID);
                                if (item.Notes.Length > 0 && item.SRItemType != ItemType.Medical && item.SRItemType != ItemType.NonMedical && item.SRItemType != ItemType.Kitchen)
                                {
                                    _amplopFilmAutoNumber = Helper.GetNewAutoNumber(txtTransactionDate.SelectedDate.Value.Date,
                                                     AppEnum.AutoNumber.AmplopFilmNo,
                                                     item.Notes.Length >= 3 ? item.Notes.Substring(0, 3).ToUpper() : item.Notes.ToUpper(),
                                                     AppSession.UserLogin.UserID);

                                    var filmNo = _amplopFilmAutoNumber.LastCompleteNumber;
                                    _amplopFilmAutoNumber.Save();

                                    transChargesItem.FilmNo = filmNo;
                                }
                            }
                        }

                        TransChargesItems.Save();
                        //coll.Save();
                    }

                    printJobParameters.AddNew("TransactionNo", txtTransactionNo.Text);
                    break;

                case AppConstant.Report.CetakMateriMcu:
                    printJobParameters.AddNew("TransactionNo", txtTransactionNo.Text);
                    printJobParameters.AddNew("UserID", AppSession.UserLogin.UserID);

                    break;
                case AppConstant.Report.JobOrderLabelDiagnostic:
                    printJobParameters.AddNew("p_TransactionNo", txtTransactionNo.Text);
                    printJobParameters.AddNew("p_Notes", string.Empty);
                    break;
                default:
                    printJobParameters.AddNew("TransactionNo", txtTransactionNo.Text);
                    break;
            }
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new TransCharges());

            txtTransactionDate.SelectedDate = (new DateTime()).NowAtSqlServer();
            txtTransactionTime.Text = (new DateTime()).NowAtSqlServer().ToString("HH:mm");
            txtTransactionNo.Text = GetNewTransactionNo(Request.QueryString["type"], ref _autoNumber, txtTransactionDate.SelectedDate.Value);

            txtExecutionDate.SelectedDate = (new DateTime()).NowAtSqlServer();
            txtExecutionTime.Text = (new DateTime()).NowAtSqlServer().ToString("HH:mm");

            txtRegistrationNo.Text = Request.QueryString["regno"];

            var reg = new Registration();
            reg.LoadByPrimaryKey(txtRegistrationNo.Text);

            txtRegistrationDate.SelectedDate = reg.RegistrationDate;
            txtRegistrationTime.Text = reg.RegistrationTime;

            PopulatePASUS(reg.SRReferralGroup, reg.ReferralID, reg.ReferralName);

            cboFromServiceUnitID.SelectedValue = Request.QueryString["cid"];
            if (Request.QueryString["type"] == "tr" || Request.QueryString["type"] == "mcu" || Request.QueryString["type"] == "npc")
            {
                ComboBox.PopulateWithServiceUnitForLocation(cboLocationID, cboFromServiceUnitID.SelectedValue);
                cboLocationID.SelectedIndex = 1;
            }

            if (_healthcareInitial == "RSCH" && Request.QueryString["type"] == "jo")
                cboFromServiceUnitID.Enabled = true;
            else
                cboFromServiceUnitID.Enabled = false;

            cboBedID.Enabled = Request.QueryString["disch"] == "1";

            if (Request.QueryString["type"] == "jo" || Request.QueryString["type"] == "ds")
            {
                var defValTakenBy = AppParameter.GetParameterValue(AppParameter.ParameterItem.DefaultValueSpecimenTakenBy);
                if (!string.IsNullOrWhiteSpace(defValTakenBy))
                {
                    ComboBox.PopulateWithOneStandardReference(cboSRBloodSampleTakenBy, "BloodSampleTakenBy", defValTakenBy);
                    ComboBox.SelectedValue(cboSRBloodSampleTakenBy, defValTakenBy);
                }

                cboToServiceUnitID.SelectedValue = string.Empty;
                cboToServiceUnitID.Text = string.Empty;
                cboLocationID.Items.Clear();
                cboLocationID.Text = string.Empty;

                switch (OrderType)
                {
                    case "LAB":
                        ComboBox.SelectedValue(cboToServiceUnitID, AppSession.Parameter.ServiceUnitLaboratoryID);
                        break;
                    case "RAD":
                        ComboBox.SelectedValue(cboToServiceUnitID, AppSession.Parameter.ServiceUnitRadiologyID);
                        break;
                    case "PAT":
                        ComboBox.SelectedValue(cboToServiceUnitID, AppSession.Parameter.ServiceUnitPathologyAnatomyID);
                        break;
                    case "OTH":
                        cboToServiceUnitID.SelectedIndex = 0;
                        break;
                }

                ApplyToServiceUnitID(cboToServiceUnitID.SelectedValue);

            }

            if (pnlKiaCaseType.Visible && !string.IsNullOrEmpty(reg.SRKiaCaseType))
            {
                var kiaCase = new AppStandardReferenceItemQuery();
                kiaCase.Where(kiaCase.ItemID == reg.SRKiaCaseType,
                              kiaCase.StandardReferenceID == AppEnum.StandardReference.KiaCaseType);

                cboSRKiaCaseType.DataSource = kiaCase.LoadDataTable();
                cboSRKiaCaseType.DataBind();
                cboSRKiaCaseType.SelectedValue = reg.SRKiaCaseType;
            }

            if (pnlObstetricType.Visible && !string.IsNullOrEmpty(reg.SRObstetricType))
            {
                var obstetricType = new AppStandardReferenceItemQuery();
                obstetricType.Where(obstetricType.ItemID == reg.SRObstetricType,
                              obstetricType.StandardReferenceID == AppEnum.StandardReference.ObstetricType);

                cboSRObstetricType.DataSource = obstetricType.LoadDataTable();
                cboSRObstetricType.DataBind();
                cboSRObstetricType.SelectedValue = reg.SRObstetricType;
            }

            var par = new ParamedicQuery();
            par.Where(par.ParamedicID == Request.QueryString["pid"]);

            cboParamedicID.DataSource = par.LoadDataTable();
            cboParamedicID.DataBind();
            cboParamedicID.SelectedValue = Request.QueryString["pid"];

            var patient = new Patient();
            patient.LoadByPrimaryKey(reg.PatientID);
            txtMedicalNo.Text = patient.MedicalNo;
            txtDiagnosticNo.Text = patient.DiagnosticNo;

            var std = new AppStandardReferenceItem();
            txtSalutation.Text = std.LoadByPrimaryKey("Salutation", patient.SRSalutation) ? std.ItemName : string.Empty;
            txtPatientName.Text = patient.PatientName;
            txtGender.Text = patient.Sex;
            txtPlaceDOB.Text = string.Format("{0}, {1}", patient.CityOfBirth, Convert.ToDateTime(patient.DateOfBirth).ToString("dd-MMM-yyyy"));
            txtAgeInYear.Text = Convert.ToString(reg.AgeInYear);
            txtAgeInMonth.Text = Convert.ToString(reg.AgeInMonth);
            txtAgeInDay.Text = Convert.ToString(reg.AgeInDay);

            PopulatePatientImage(patient.PatientID);

            var rooms = new ServiceRoomCollection();
            rooms.Query.Where(rooms.Query.ServiceUnitID == cboFromServiceUnitID.SelectedValue && rooms.Query.IsOperatingRoom == true && rooms.Query.IsShowOnBookingOT == true);
            rooms.LoadAll();

            txtRoomID.Text = string.IsNullOrEmpty(Request.QueryString["roomid"]) ? reg.RoomID : Request.QueryString["roomid"];
            var room = new ServiceRoom();
            room.LoadByPrimaryKey(txtRoomID.Text);
            lblRoomName.Text = room.RoomName;
            txtTariffDiscForRoomIn.Value = Convert.ToDouble(room.TariffDiscountForRoomIn);

            if (rooms.Count > 0)
            {
                txtClassID.Text = string.IsNullOrEmpty(reg.ProcedureChargeClassID)
                                      ? reg.ChargeClassID
                                      : reg.ProcedureChargeClassID;
            }
            else
                txtClassID.Text = reg.ChargeClassID;
            var c = new Class();
            c.LoadByPrimaryKey(txtClassID.Text);
            lblClassName.Text = c.ClassName;

            PopulateBedCollection(reg);
            //cboBedID.SelectedValue = reg.ServiceUnitID + ", " + reg.RoomID + ", " + reg.ChargeClassID + ", " + reg.BedID;
            cboBedID.SelectedValue = cboFromServiceUnitID.SelectedValue + ", " + txtRoomID.Text + ", " + txtClassID.Text + ", " + reg.BedID + ", " + reg.RegistrationNo;
            chkIsRoomIn.Checked = reg.IsRoomIn ?? false;

            var guar = new GuarantorQuery();
            guar.Where(guar.GuarantorID == (string.IsNullOrEmpty(patient.str.MemberID) ? reg.GuarantorID : patient.MemberID));
            cboGuarantorID.DataSource = guar.LoadDataTable();
            cboGuarantorID.DataBind();
            cboGuarantorID.SelectedValue = string.IsNullOrEmpty(patient.str.MemberID) ? reg.GuarantorID : patient.MemberID;

            var grr = new Guarantor();
            if (grr.LoadByPrimaryKey(cboGuarantorID.SelectedValue))
                trBpjsSepNo.Visible = grr.SRGuarantorType == AppSession.Parameter.GuarantorTypeBPJS;
            else 
                trBpjsSepNo.Visible = false;
            txtBpjsSepNo.Text = reg.BpjsSepNo;

            var parId = !string.IsNullOrEmpty(reg.ParamedicID)
                                   ? reg.ParamedicID
                                   : Request.QueryString["pid"];
            if (parId == _paramedicIdDokterLuar)
            {
                cboPhysicianSender_ItemsRequested(cboPhysicianSender, new RadComboBoxItemsRequestedEventArgs() { Text = reg.PhysicianSenders });
                cboPhysicianSender.Text = reg.PhysicianSenders;
                txtPhysicianSenders.Text = reg.PhysicianSenders;
            }
            else
            {
                bool isDefaultFromReg = false;
                if (_healthcareInitial == "RSCH")
                {
                    var query = new ServiceUnitTransactionCodeQuery("a");
                    var qrServ = new ServiceUnitQuery("c");

                    query.es.Distinct = true;
                    query.Select(qrServ.ServiceUnitID, qrServ.ServiceUnitName);
                    query.InnerJoin(qrServ).On(query.ServiceUnitID == qrServ.ServiceUnitID);
                    query.Where
                        (
                            query.SRTransactionCode == TransactionCode.JobOrder,
                            qrServ.IsActive == true,
                            qrServ.IsUsingJobOrder == true,
                            qrServ.ServiceUnitID == cboFromServiceUnitID.SelectedValue
                        );

                    DataTable dtb = query.LoadDataTable();
                    if (dtb.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(reg.ReferralID))
                        {
                            var r = new Referral();
                            if (r.LoadByPrimaryKey(reg.ReferralID))
                            {
                                cboPhysicianSender_ItemsRequested(cboPhysicianSender, new RadComboBoxItemsRequestedEventArgs() { Text = r.ReferralName });
                                cboPhysicianSender.Text = r.ReferralName;
                                txtPhysicianSenders.Text = r.ReferralName;
                            }
                            else
                                isDefaultFromReg = true;
                        }
                        else
                            isDefaultFromReg = true;
                    }
                    else
                        isDefaultFromReg = true;
                }
                else
                    isDefaultFromReg = true;

                if (isDefaultFromReg)
                {
                    var p = new Paramedic();
                    p.LoadByPrimaryKey(parId);
                    {
                        cboPhysicianSender_ItemsRequested(cboPhysicianSender, new RadComboBoxItemsRequestedEventArgs() { Text = p.ParamedicName });
                        cboPhysicianSender.Text = p.ParamedicName;
                        txtPhysicianSenders.Text = p.ParamedicName;
                    }
                }
            }

            chkIsProceed.Checked = Request.QueryString["type"] == "jo";

            lblRegistrationInfo2.Text = RegistrationInfoSumary.GetDocumentCheckListCountRemains(txtRegistrationNo.Text);

            tblTemporaryBill.Visible = reg.SRRegistrationType == AppConstant.RegistrationType.OutPatient && trBpjsSepNo.Visible;
            if (tblTemporaryBill.Visible)
            {
                txtTemporaryBillPlafond.Value = Convert.ToDouble(reg.PlavonAmount);
                txtTemporaryBillTotal.Value = Convert.ToDouble(GetTotalTemporaryBill());
            }

            ViewState["IsApproved"] = false;
            ViewState["IsVoid"] = false;
        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var entity = new TransCharges();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                if ((entity.IsApproved ?? false))
                {
                    args.IsCancel = true;
                    args.MessageText = AppConstant.Message.RecordHasApproved;
                }
                else if ((entity.IsVoid ?? false))
                {
                    args.IsCancel = true;
                    args.MessageText = AppConstant.Message.RecordHasVoided;
                }
            }
            else
            {
                args.IsCancel = true;
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
        }
        protected override void OnMenuEditClick()
        {
            if (Request.QueryString["type"] == "jo" || Request.QueryString["type"] == "ds")
                cboToServiceUnitID.Enabled = (TransChargesItems.Count == 0);
            cboLocationID.Enabled = (TransChargesItems.Count == 0);
            cboBedID.Enabled = Request.QueryString["disch"] != "0" && (TransChargesItems.Count == 0);
            if (_healthcareInitial == "RSCH" && Request.QueryString["type"] == "jo")
                cboFromServiceUnitID.Enabled = true;
            else
                cboFromServiceUnitID.Enabled = false;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            //var entity = new TransCharges();
            //if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            //{
            //    entity.MarkAsDeleted();
            //    SaveEntity(entity);
            //}
            //else
            //    args.MessageText = AppConstant.Message.RecordNotExist;

            // Di AppProgram di set tidak bisa delete jadi saya remark saja krn layar ini dipakai juga di EMR (Handono 2020-08-25)
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            if (TransChargesItems.Count == 0)
            {
                args.MessageText = "Detail transaction is not defined.";
                args.IsCancel = true;
                return;
            }
            if (txtExecutionDate.SelectedDate.Value.Date < txtRegistrationDate.SelectedDate.Value.Date)
            {
                args.MessageText = string.Format("Execution Date should not be less than Registration Date.");
                args.IsCancel = true;
                return;
            }
            if (Request.QueryString["type"] != "jo")
            {
                foreach (var comp in TransChargesItemComps.Where(c => TransChargesItems.Where(t => string.IsNullOrEmpty(t.ParentNo)).Select(t => t.SequenceNo).Contains(c.SequenceNo)))
                {
                    var tc = new TariffComponent();
                    tc.LoadByPrimaryKey(comp.TariffComponentID);
                    if ((tc.IsTariffParamedic ?? false) && string.IsNullOrEmpty(comp.ParamedicID))
                    {
                        var item = TransChargesItems.FindByPrimaryKey(comp.TransactionNo, comp.SequenceNo);

                        args.MessageText = string.Format("Physician ID in {0} is not defined.", item.GetColumn("refToItem_ItemName"));
                        args.IsCancel = true;
                        return;
                    }
                }

                if (txtExecutionDate.SelectedDate.Value.Date > txtTransactionDate.SelectedDate.Value.Date && !AppSession.Parameter.IsAllowExecutionDateForward)
                {
                    args.MessageText = string.Format("Execution Date should not be greater than Transaction Date.");
                    args.IsCancel = true;
                    return;
                }
            }
            else
            {
                if (AppSession.Parameter.IsNeedSpecimenOnJo && cboToServiceUnitID.SelectedValue == AppSession.Parameter.ServiceUnitLaboratoryID && string.IsNullOrEmpty(cboSRBloodSampleTakenBy.SelectedValue))
                {
                    args.MessageText = string.Format("Specimen Taken By is required.");
                    args.IsCancel = true;
                    return;
                }
            }

            if (Request.QueryString["type"] == "ds")
            {
                if (AppSession.Parameter.IsUsingHisInterop && AppSession.Parameter.HisInteropConfigName == AppConstant.HIS_INTEROP.LINK_LIS_INTEROP_CONNECTION_NAME)
                {
                    if (cboToServiceUnitID.SelectedValue == AppSession.Parameter.ServiceUnitLaboratoryID)
                    {
                        if (string.IsNullOrEmpty(cboPhysicianIDPathology.SelectedValue))
                        {
                            args.MessageText = string.Format("Clinical Pathologist is required.");
                            args.IsCancel = true;
                            return;
                        }

                        if (string.IsNullOrEmpty(cboAnalystID.SelectedValue))
                        {
                            args.MessageText = string.Format("Laboratory Analyst is required.");
                            args.IsCancel = true;
                            return;
                        }
                    }
                }
            }

            var entity = new TransCharges();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);

            if (IsCalledFromEmr)
            {
                if (Request.QueryString["type"] == "jo" && AppParameter.IsYes(AppParameter.ParameterItem.IsAutoApprovedExamOrderFromEmr))
                    OnMenuApprovalClick(args);
                else if (AppParameter.IsYes(AppParameter.ParameterItem.IsAutoApprovedServiceUnitTxFromEmr))
                    OnMenuApprovalClick(args);
            }
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            if (TransChargesItems.Count == 0)
            {
                args.MessageText = "Detail transaction is not defined.";
                args.IsCancel = true;
                return;
            }
            if (txtExecutionDate.SelectedDate.Value.Date < txtRegistrationDate.SelectedDate.Value.Date)
            {
                args.MessageText = string.Format("Execution Date should not be less than Registration Date.");
                args.IsCancel = true;
                return;
            }
            if (Request.QueryString["type"] != "jo")
            {
                if (txtExecutionDate.SelectedDate.Value.Date > txtTransactionDate.SelectedDate.Value.Date && !AppSession.Parameter.IsAllowExecutionDateForward)
                {
                    args.MessageText = string.Format("Execution Date should not be greater than Transaction Date.");
                    args.IsCancel = true;
                    return;
                }

            }
            else
            {
                if (AppSession.Parameter.IsNeedBloodSample && cboToServiceUnitID.SelectedValue == AppSession.Parameter.ServiceUnitLaboratoryID && string.IsNullOrEmpty(cboSRBloodSampleTakenBy.SelectedValue))
                {
                    args.MessageText = string.Format("Blood Samples Taken By is required.");
                    args.IsCancel = true;
                    return;
                }
            }

            if (Request.QueryString["type"] == "ds")
            {
                if (AppSession.Parameter.IsUsingHisInterop && AppSession.Parameter.HisInteropConfigName == AppConstant.HIS_INTEROP.LINK_LIS_INTEROP_CONNECTION_NAME)
                {
                    if (cboToServiceUnitID.SelectedValue == AppSession.Parameter.ServiceUnitLaboratoryID)
                    {
                        if (string.IsNullOrEmpty(cboPhysicianIDPathology.SelectedValue))
                        {
                            args.MessageText = string.Format("Clinical Pathologist is required.");
                            args.IsCancel = true;
                            return;
                        }

                        if (string.IsNullOrEmpty(cboAnalystID.SelectedValue))
                        {
                            args.MessageText = string.Format("Laboratory Analyst is required.");
                            args.IsCancel = true;
                            return;
                        }
                    }
                }
            }

            var entity = new TransCharges();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);

                if (IsCalledFromEmr && AppParameter.IsYes(AppParameter.ParameterItem.IsAutoApprovedServiceUnitTxFromEmr))
                    OnMenuApprovalClick(args);
            }
        }

        protected override void OnMenuMoveNextClick(ValidateArgs args)
        {
            MoveRecord(true);
        }

        protected override void OnMenuMovePrevClick(ValidateArgs args)
        {
            MoveRecord(false);
        }

        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            auditLogFilter.PrimaryKeyData = string.Format("TransactionNo='{0}'", txtTransactionNo.Text.Trim());
            auditLogFilter.TableName = "TransCharges";
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            var entity = new TransCharges();
            if (!entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
            if (entity.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return;
            }

            if (Helper.GuarantorBpjsCasemix.Contains(cboGuarantorID.SelectedValue))
            {
                var reg = new Registration();
                reg.LoadByPrimaryKey(txtRegistrationNo.Text);
                if (AppSession.Parameter.CasemixValidationRegistrationType.Contains(reg.SRRegistrationType))
                {
                    if (Request.QueryString["type"] != "jo")
                    {
                        if (TransChargesItems.Any(t => !(t.IsVoid ?? false) && !(t.IsCasemixApproved ?? false)))
                        {
                            var tpi = TransChargesItems.Where(t => !(t.IsCasemixApproved ?? false)).Take(1).SingleOrDefault();
                            args.MessageText = "Some item(s) need validation by Casemix : " + tpi.GetColumn("refToItem_ItemName").ToString();
                            args.IsCancel = true;
                            return;
                        }
                    }
                }
            }

            //try
            //{
            string retMsg = SetApproval(entity, true,
                TransChargesItems, TransChargesItemComps, TransChargesItemConsumptions,
                txtClassID.Text, CostCalculations,
                Request.QueryString["type"], cboFromServiceUnitID.SelectedValue, cboToServiceUnitID.SelectedValue,
                txtTransactionDate.SelectedDate.Value, _amplopFilmAutoNumber, args);
            if (!retMsg.Equals(string.Empty))
            {
                args.MessageText = retMsg;
                args.IsCancel = true;
                return;
            }
            //}
            //catch (Exception e)
            //{
            //    // lagi cari penyebab approve header tapi detailnya gak approve
            //    LogError(e);
            //}

            if (IsCalledFromEmr) // Close after approve
                ScriptManager.RegisterStartupScript(this, this.GetType(), "closeAndApply", "CloseAndApply();", true);
        }

        private static string ValidateApproval(TransCharges entity,
            Registration reg, string QueryString_type,
            TransChargesItemCollection TransChargesItems, TransChargesItemCompCollection TransChargesItemComps,
            ref bool detailApproved)
        {
            detailApproved = false;

            //if (!entity.LoadByPrimaryKey(TransactionNo))
            //{
            //    return AppConstant.Message.RecordNotExist;
            //}

            if (entity.IsApproved ?? false)
            {
                // requery lagi memastikan transchargesdetail belum diapprove oleh thread yang lain
                // atau user yang lain
                var tcds = new TransChargesItemCollection();
                tcds.Query.Where(
                    tcds.Query.TransactionNo.Equal(entity.TransactionNo),
                    tcds.Query.IsVoid.Equal(false),
                    tcds.Query.ParentNo.Equal(string.Empty));
                if (tcds.LoadAll())
                {
                    foreach (var tcd in tcds)
                    {
                        detailApproved = detailApproved || (tcd.IsApprove ?? false);
                    }
                }

                if (detailApproved)
                {
                    return AppConstant.Message.RecordHasApproved;
                }
            }
            if (entity.IsVoid ?? false)
            {
                return AppConstant.Message.RecordHasVoided;
            }

            //bool isClosed = true, isLocked = true;
            //var mergebilling = new MergeBilling();
            //if (mergebilling.LoadByPrimaryKey(entity.RegistrationNo) && !string.IsNullOrEmpty(mergebilling.FromRegistrationNo))
            //{
            //    var regmb = new Registration();
            //    if (regmb.LoadByPrimaryKey(mergebilling.FromRegistrationNo))
            //    {
            //        isClosed = regmb.IsClosed ?? false;
            //        isLocked = regmb.IsHoldTransactionEntry ?? false;
            //    }
            //}

            //reg.LoadByPrimaryKey(entity.RegistrationNo);
            //if (isClosed && (reg.IsClosed ?? false))
            //{
            //    return string.Format("Registration has been closed.");
            //}

            //if (isLocked && (reg.IsHoldTransactionEntry ?? false))
            //{
            //    return string.Format("Transaction is locked.");
            //}

            reg.LoadByPrimaryKey(entity.RegistrationNo);
            if (reg.IsHoldTransactionEntry ?? false)
                return string.Format("Transaction is locked.");

            if (reg.IsClosed ?? false)
                return string.Format("Registration has closed.");

            bool isClosed = false, isLocked = false;
            var mergebilling = new MergeBilling();
            if (mergebilling.LoadByPrimaryKey(entity.RegistrationNo) && !string.IsNullOrEmpty(mergebilling.FromRegistrationNo))
            {
                var regmb = new Registration();
                if (regmb.LoadByPrimaryKey(mergebilling.FromRegistrationNo))
                {
                    isClosed = regmb.IsClosed ?? false;
                    isLocked = regmb.IsHoldTransactionEntry ?? false;
                }
            }
            if (isClosed)
                return string.Format("Parent Registration has closed.");

            if (isLocked)
                return string.Format("Parent Transaction is locked.");


            if (QueryString_type.Equals(string.Empty)) QueryString_type = "tr";

            if (QueryString_type != "jo")
            {
                foreach (var comp in TransChargesItemComps.Where(c => TransChargesItems.Where(t => string.IsNullOrEmpty(t.ParentNo)).Select(t => t.SequenceNo).Contains(c.SequenceNo)))
                {
                    var tc = new TariffComponent();
                    tc.LoadByPrimaryKey(comp.TariffComponentID);
                    if ((tc.IsTariffParamedic ?? false) && string.IsNullOrEmpty(comp.ParamedicID))
                    {
                        var item = TransChargesItems.FindByPrimaryKey(comp.TransactionNo, comp.SequenceNo);

                        return string.Format("Physician ID for {0} is not defined.", item.GetColumn("refToItem_ItemName"));
                    }
                }

                if (AppSession.Parameter.IsAutoApprovePackage)
                {
                    // validate approve header paket, harus cek detail paket ada yang autoapprove atau tidak, trus yang auto approve harus sudah ada dokternya
                    foreach (var comp in TransChargesItemComps.Where(c => TransChargesItems.Where(t => !string.IsNullOrEmpty(t.ParentNo)).Select(t => t.SequenceNo).Contains(c.SequenceNo)))
                    {
                        var tc = new TariffComponent();
                        tc.LoadByPrimaryKey(comp.TariffComponentID);
                        if ((tc.IsTariffParamedic ?? false) && string.IsNullOrEmpty(comp.ParamedicID))
                        {
                            var item = TransChargesItems.FindByPrimaryKey(comp.TransactionNo, comp.SequenceNo);
                            // jika auto approve

                            var itemPkg = TransChargesItems.Where(x => x.TransactionNo == item.TransactionNo && x.IsPackage == true).FirstOrDefault();
                            if (itemPkg != null)
                            {
                                var ipColl = new ItemPackageCollection();
                                ipColl.Query.Where(ipColl.Query.ItemID == itemPkg.ItemID,
                                    ipColl.Query.DetailItemID == item.ItemID,
                                    ipColl.Query.ServiceUnitID == item.ToServiceUnitID,
                                    ipColl.Query.IsAutoApprove == true);
                                if (ipColl.LoadAll())
                                {
                                    return string.Format("Physician ID for {0} in detail package for auto approve is not defined.", item.GetColumn("refToItem_ItemName"));
                                }
                            }
                        }
                    }
                }
            }

            //foreach (var tci in TransChargesItems)
            //{
            //    if (tci.Price != 0 && tci.CostPrice == 0)
            //    {
            //        args.MessageText = string.Format("Cost Price for item {0} is zero.", tci.GetColumn("refToItem_ItemName"));
            //        args.IsCancel = true;
            //        return;
            //    }
            //}

            return string.Empty;
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            // For Package Transaction Only
            var entity = new TransCharges();

            if (!entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }

            if (Request.QueryString["type"] != "jo" && entity.IsPackage == false)
            {
                args.MessageText = "This transaction is not a item package transaction. Please do data correction through the correction transaction menu.";
                args.IsCancel = true;
                return;
            }

            if (Request.QueryString["type"] == "jo" && entity.IsPackage == false)
            {
                var detail = new TransChargesItemQuery();
                detail.Where
                    (
                        detail.TransactionNo == txtTransactionNo.Text,
                        detail.IsOrderRealization == true,
                        detail.IsVoid == false
                    );

                if (detail.LoadDataTable().Rows.Count > 0)
                {
                    args.MessageText = "This transaction has been proceed. Data can't be canceled";
                    args.IsCancel = true;
                    return;
                }
            }

            var reg = new Registration();
            reg.LoadByPrimaryKey(txtRegistrationNo.Text);

            var pay = new TransPaymentItemOrderCollection();
            var qtpio = new TransPaymentItemOrderQuery("a");
            var qtp = new TransPaymentQuery("b");
            qtpio.InnerJoin(qtp).On(qtpio.PaymentNo.Equal(qtp.PaymentNo))
                .Where(qtpio.TransactionNo == txtTransactionNo.Text, qtpio.IsPaymentReturned == false,
                qtp.IsVoid.Equal(false), qtp.IsApproved.Equal(true));
            pay.Load(qtpio);
            if (pay.Count > 0)
            {
                args.MessageText = "This transaction has been paid. Data can't be canceled";
                args.IsCancel = true;
                return;
            }

            var cc = new CostCalculationCollection();
            cc.Query.Where(cc.Query.TransactionNo == txtTransactionNo.Text, cc.Query.IntermBillNo.IsNotNull());
            cc.LoadAll();
            if (cc.Count > 0)
            {
                args.MessageText = "Transaction is already on interm bill. Data can't be canceled";
                args.IsCancel = true;
                return;
            }
            var dateInServer = (new DateTime()).NowAtSqlServer();
            using (var trans = new esTransactionScope())
            {
                var headers = new TransChargesCollection();
                headers.Query.Where(
                    headers.Query.Or(
                        headers.Query.TransactionNo == txtTransactionNo.Text,
                        headers.Query.PackageReferenceNo == txtTransactionNo.Text
                        )
                    );
                headers.LoadAll();

                var items = new TransChargesItemCollection();
                items.Query.Where(items.Query.TransactionNo.In(headers.Select(h => h.TransactionNo)));
                items.LoadAll();

                foreach (var item in items)
                {
                    item.IsApprove = false;
                    item.IsBillProceed = false;
                    item.IsVoid = true;
                    item.VoidByUserID = AppSession.UserLogin.UserID;
                    item.VoidDateTime = (new DateTime()).NowAtSqlServer();
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = dateInServer;
                }

                items.Save();

                // cek jasa medis
                var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
                feeColl.Query.Where(feeColl.Query.TransactionNo.In(headers.Select(h => h.TransactionNo)));
                feeColl.LoadAll();

                if (feeColl.Where(x => (x.VerificationNo ?? string.Empty) != string.Empty).Count() > 0)
                {
                    // abort
                    args.MessageText = "Paramedic fee has been verified";
                    args.IsCancel = true;
                    return;
                }
                feeColl.MarkAllAsDeleted();
                feeColl.Save();
                // end cek jasa medis

                ItemBalance.PrepareItemBalancesForMCUCorrection(headers, AppSession.UserLogin.UserID, AppSession.Parameter.IsEnabledStockWithEdControl);

                foreach (var header in headers)
                {
                    var comps = new TransChargesItemCompCollection();
                    var cost = new CostCalculationCollection();

                    var unit = new ServiceUnit();
                    unit.LoadByPrimaryKey(header.ToServiceUnitID);

                    if ((!(header.IsOrder ?? false)) && (header.IsApproved ?? false) && (!(header.IsPackage ?? false)))
                    {
                        comps.Query.Where(comps.Query.TransactionNo == header.TransactionNo);
                        comps.LoadAll();

                        cost.Query.Where(cost.Query.TransactionNo == header.TransactionNo);
                        cost.LoadAll();

                        if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsJournalCashBased) == "No")
                        {
                            if (AppParameter.IsYes(AppParameter.ParameterItem.acc_IsJournalAccualNoTemporary))
                            {
                                JournalTransactions.AddNewPatientIncomeAccrualUnapproval(header.TransactionNo, dateInServer.Date, AppSession.UserLogin.UserID, 0);
                            }
                            else
                            {
                                var isClosingPeriod = PostingStatus.IsPeriodeClosed(header.TransactionDate.Value.Date);
                                if (isClosingPeriod)
                                {
                                    args.MessageText = "Financial statements for period: " +
                                                       string.Format("{0:MMMM-yyyy}", header.TransactionDate.Value.Date) +
                                                       " have been closed. Please contact the authorities.";
                                    args.IsCancel = true;
                                    return;
                                }

                                int? journalId = JournalTransactions.AddNewIncomeCorrectionJournal(header, comps, reg, unit, cost, "SC", AppSession.UserLogin.UserID, false, 0);
                            }

                        }
                    }
                    else if ((header.IsOrder ?? false) && (header.IsBillProceed ?? false))
                    {
                        var cuery = new TransChargesItemCompQuery("a");
                        var tuery = new TransChargesItemQuery("b");

                        cuery.InnerJoin(tuery).On(
                            cuery.TransactionNo == tuery.TransactionNo &&
                            cuery.SequenceNo == tuery.SequenceNo &&
                            tuery.IsOrderRealization == true
                            );
                        cuery.Where(cuery.TransactionNo == header.TransactionNo);

                        comps.Load(cuery);

                        var cst = new CostCalculationQuery("a");
                        tuery = new TransChargesItemQuery("b");

                        cst.InnerJoin(tuery).On(
                            cst.TransactionNo == tuery.TransactionNo &&
                            cst.SequenceNo == tuery.SequenceNo &&
                            tuery.IsOrderRealization == true
                            );
                        cst.Where(cst.TransactionNo == header.TransactionNo);

                        cost.Load(cst);

                        if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsJournalCashBased) == "No")
                        {
                            if (AppParameter.IsYes(AppParameter.ParameterItem.acc_IsJournalAccualNoTemporary))
                            {
                                JournalTransactions.AddNewPatientIncomeAccrualUnapproval(header.TransactionNo, dateInServer.Date, AppSession.UserLogin.UserID, 0);
                            }
                            else
                            {
                                var isClosingPeriod = PostingStatus.IsPeriodeClosed(header.TransactionDate.Value.Date);
                                if (isClosingPeriod)
                                {
                                    args.MessageText = "Financial statements for period: " +
                                                       string.Format("{0:MMMM-yyyy}", header.TransactionDate.Value.Date) +
                                                       " have been closed. Please contact the authorities.";
                                    args.IsCancel = true;
                                    return;
                                }

                                int? journalId = JournalTransactions.AddNewIncomeCorrectionJournal(header, comps, reg, unit, cost, "SC", AppSession.UserLogin.UserID, false, 0);
                            }
                        }
                    }
                    else
                    {
                        cost.Query.Where(cost.Query.TransactionNo == header.TransactionNo);
                        cost.LoadAll();
                    }

                    cost.MarkAllAsDeleted();
                    cost.Save();

                    header.IsApproved = false;
                    header.IsBillProceed = false;
                    header.IsProceed = false;
                    header.IsVoid = true;
                    header.VoidDateTime = dateInServer;
                    header.VoidByUserID = AppSession.UserLogin.UserID;
                    header.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    header.LastUpdateDateTime = dateInServer;
                }

                headers.Save();

                trans.Complete();
            }
        }

        private static bool IsServiceUnitOrder(string serviceUnitID)
        {
            var unit = new ServiceUnit();
            unit.LoadByPrimaryKey(serviceUnitID);
            return unit.IsUsingJobOrder ?? false;
        }

        public static string SetApproval(TransCharges entity, bool isApproval,
            TransChargesItemCollection TransChargesItems, TransChargesItemCompCollection TransChargesItemComps,
            TransChargesItemConsumptionCollection TransChargesItemConsumptions, string ClassID,
            CostCalculationCollection CostCalculations,
            string QueryString_type, string FromServiceUnitID, string ToServiceUnitID, DateTime TransactionDate,
            AppAutoNumberLast amplopFilmAutoNumber,
            ValidateArgs args)
        {
            bool detailApproved = false;
            var reg = new Registration();
            var unit = new ServiceUnit();

            string valMsg = ValidateApproval(entity, reg, QueryString_type,
                TransChargesItems, TransChargesItemComps, ref detailApproved);
            if (!valMsg.Equals(string.Empty)) return valMsg;

            if (ClassID.Equals(string.Empty)) ClassID = entity.ClassID;
            if (FromServiceUnitID.Equals(string.Empty)) FromServiceUnitID = entity.FromServiceUnitID;
            if (ToServiceUnitID.Equals(string.Empty)) ToServiceUnitID = entity.ToServiceUnitID;

            using (var trans = new esTransactionScope())
            {
                if (string.IsNullOrEmpty(entity.LocationID))
                    entity.LocationID = unit.GetMainLocationId(entity.ToServiceUnitID);

                if (!(entity.IsApproved ?? false) && detailApproved)
                {
                    entity.IsApproved = true;
                    entity.ApprovedDateTime = (new DateTime()).NowAtSqlServer();
                    entity.ApprovedByUserID = AppSession.UserLogin.UserID;

                    entity.Save();
                }
                else if ((entity.IsApproved ?? false) && !detailApproved)
                {
                    foreach (var tci in TransChargesItems)
                    {
                        if (!(tci.IsVoid ?? false))
                            tci.IsApprove = true;
                    }
                    TransChargesItems.Save();
                }
                else
                {
                    //package manipulation
                    //hanya paket dari mcu yang di pecah per unit tujuan
                    //paket non mcu tidak dipecah
                    //if ((QueryString_type == "mcu") && (entity.IsPackage ?? false))
                    if (entity.IsPackage ?? false)
                    {
                        var headers = new TransChargesCollection();
                        var details = new TransChargesItemCollection();
                        var components = new TransChargesItemCompCollection();
                        var consumptions = new TransChargesItemConsumptionCollection();

                        var pacs = (TransChargesItems.Where(i => !string.IsNullOrEmpty(i.ParentNo) && (i.ParentNo.Length == 3))
                                                     .GroupBy(i => new
                                                     {
                                                         i.ParentNo,
                                                         i.ToServiceUnitID
                                                     })
                                                     .Select(g => new
                                                     {
                                                         g.Key.ParentNo,
                                                         g.Key.ToServiceUnitID,
                                                         IsOrder = IsServiceUnitOrder(g.Key.ToServiceUnitID) &&
                                                            g.Key.ToServiceUnitID !=
                                                            ((QueryString_type == "tr" || QueryString_type == "npc") ? FromServiceUnitID : ToServiceUnitID)
                                                     })).Distinct();

                        foreach (var pac in pacs)
                        {
                            var autoNumber = Helper.GetNewAutoNumber(TransactionDate.Date,
                                pac.IsOrder ? AppEnum.AutoNumber.JobOrderNo : AppEnum.AutoNumber.TransactionNo);
                            var transactionNo = autoNumber.LastCompleteNumber;
                            autoNumber.Save();

                            //header
                            #region header
                            var header = headers.AddNew();
                            header.TransactionNo = transactionNo;
                            header.RegistrationNo = entity.RegistrationNo;
                            header.TransactionDate = entity.TransactionDate;
                            header.ExecutionDate = entity.ExecutionDate;
                            header.ReferenceNo = string.Empty;
                            header.ResponUnitID = String.Empty;
                            header.FromServiceUnitID = (pac.IsOrder) ? entity.FromServiceUnitID : pac.ToServiceUnitID;
                            header.IsBillProceed = false;
                            header.IsApproved = pac.IsOrder;
                            header.ToServiceUnitID = pac.ToServiceUnitID;
                            header.ClassID = entity.ClassID;
                            header.RoomID = entity.RoomID;
                            header.BedID = entity.BedID;
                            header.DueDate = entity.DueDate;
                            header.SRShift = entity.SRShift;
                            header.SRItemType = string.Empty;

                            header.IsVoid = false;
                            header.IsAutoBillTransaction = false;
                            header.IsOrder = pac.IsOrder;
                            header.IsCorrection = false;
                            header.Notes = string.Empty;
                            header.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            header.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                            header.IsPackage = false;
                            header.PackageReferenceNo = entity.TransactionNo;
                            header.SurgicalPackageID = String.Empty;
                            header.LocationID = unit.GetMainLocationId(pac.ToServiceUnitID);
                            if (pac.IsOrder)
                            {
                                header.SROrderStatus = "1";
                                header.IsProceed = true;
                            }
                            else
                                header.IsProceed = false;

                            #endregion

                            var tcis = TransChargesItems.Where(t => t.ParentNo == pac.ParentNo &&
                                                                    t.ToServiceUnitID == pac.ToServiceUnitID)
                                                        .OrderBy(t => t.SequenceNo);

                            foreach (var tci in tcis)
                            {
                                //detail
                                #region detail
                                var detail = details.AddNew();
                                detail.TransactionNo = header.TransactionNo;
                                detail.SequenceNo = tci.SequenceNo;
                                detail.ReferenceNo = tci.ReferenceNo;
                                detail.ReferenceSequenceNo = tci.ReferenceSequenceNo;
                                detail.ItemID = tci.ItemID;
                                detail.ChargeClassID = tci.ChargeClassID;
                                detail.ParamedicID = tci.ParamedicID;
                                detail.SecondParamedicID = tci.SecondParamedicID;
                                detail.IsAdminCalculation = tci.IsAdminCalculation;
                                detail.IsVariable = tci.IsVariable;
                                detail.IsCito = tci.IsCito;
                                detail.ChargeQuantity = tci.ChargeQuantity;
                                detail.StockQuantity = tci.StockQuantity;
                                detail.SRItemUnit = tci.SRItemUnit;
                                detail.CostPrice = tci.CostPrice;
                                detail.Price = tci.Price;
                                detail.DiscountAmount = tci.DiscountAmount;
                                detail.CitoAmount = tci.CitoAmount;
                                detail.RoundingAmount = tci.RoundingAmount;
                                detail.SRDiscountReason = tci.SRDiscountReason;
                                detail.IsAssetUtilization = tci.IsAssetUtilization;
                                detail.AssetID = tci.AssetID;
                                detail.IsBillProceed = false;// (tci.IsVoid ?? false) ? false : pac.IsOrder;
                                //detail.IsBillProceed = AppParameter.GetParameterValue(AppParameter.ParameterItem.IsJobOrderRealizationNeedConfirm).ToLower() == "yes" ? false : pac.IsOrder;
                                detail.IsOrderRealization = tci.IsOrderRealization;
                                detail.IsPaymentConfirmed = tci.IsPaymentConfirmed;
                                detail.IsPackage = tci.IsPackage;
                                detail.IsApprove = (tci.IsVoid ?? false) ? false : pac.IsOrder;
                                detail.IsVoid = tci.IsVoid;
                                detail.Notes = tci.Notes;
                                detail.FilmNo = tci.FilmNo;
                                detail.TariffDate = tci.TariffDate;

                                var item = new Item();
                                item.LoadByPrimaryKey(detail.ItemID);
                                if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSCH" && string.IsNullOrEmpty(detail.FilmNo))
                                {
                                    if (item.Notes.Length > 0 && item.SRItemType != ItemType.Medical && item.SRItemType != ItemType.NonMedical && item.SRItemType != ItemType.Kitchen)
                                    {
                                        amplopFilmAutoNumber =
                                            Helper.GetNewAutoNumber(TransactionDate.Date,
                                                                    AppEnum.AutoNumber.AmplopFilmNo,
                                                                    item.Notes.Length >= 3
                                                                        ? item.Notes.Substring(0, 3).ToUpper()
                                                                        : item.Notes.ToUpper(),
                                                                    AppSession.UserLogin.UserID);

                                        var filmNo = amplopFilmAutoNumber.LastCompleteNumber;
                                        amplopFilmAutoNumber.Save();

                                        detail.FilmNo = filmNo;
                                    }
                                }

                                detail.LastUpdateDateTime = tci.LastUpdateDateTime;
                                detail.LastUpdateByUserID = tci.LastUpdateByUserID;
                                detail.ParentNo = string.Empty;
                                detail.SRCenterID = tci.SRCenterID;
                                detail.AutoProcessCalculation = tci.AutoProcessCalculation;
                                detail.ParamedicCollectionName = tci.ParamedicCollectionName;
                                detail.ToServiceUnitID = tci.ToServiceUnitID;
                                detail.IsCitoInPercent = tci.IsCitoInPercent;
                                detail.BasicCitoAmount = tci.BasicCitoAmount;
                                detail.IsItemRoom = tci.IsItemRoom;
                                detail.IsItemRoom = false;

                                detail.SRCitoPercentage = tci.SRCitoPercentage;
                                detail.ItemConditionRuleID = tci.ItemConditionRuleID;

                                if (pac.IsOrder)
                                    detail.IsOrderConfirmed = true;

                                if (tci.IsExtraItem ?? false)
                                {
                                    detail.IsExtraItem = tci.IsExtraItem;
                                    detail.IsSelectedExtraItem = tci.IsSelectedExtraItem;
                                }

                                // cek mapping serviceunit item service, jika belum ada mapping
                                // maka harus dimapping, mapping dibutuhkan untuk edit (untuk isi dokter per detail paket)
                                // dan jurnal
                                var suisColl = new ServiceUnitItemServiceCollection();
                                suisColl.Query.Where(suisColl.Query.ItemID.Equal(detail.ItemID),
                                    suisColl.Query.ServiceUnitID.Equal(header.ToServiceUnitID));
                                if (!suisColl.LoadAll())
                                {
                                    var nSuis = suisColl.AddNew();
                                    nSuis.ServiceUnitID = header.ToServiceUnitID;
                                    nSuis.ItemID = detail.ItemID;
                                    nSuis.ChartOfAccountId = 0;
                                    nSuis.SubledgerId = 0;
                                    nSuis.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                                    nSuis.LastUpdateByUserID = "system";
                                    nSuis.IsAllowEditByUserVerificated = true;
                                    nSuis.IsVisible = true;
                                    suisColl.Save();
                                }

                                #endregion

                                var tcis2 = TransChargesItems.Where(t => t.ParentNo == detail.SequenceNo)
                                                             .OrderBy(t => t.SequenceNo);
                                foreach (var tci2 in tcis2)
                                {
                                    var detail2 = details.AddNew();
                                    detail2.TransactionNo = header.TransactionNo;
                                    detail2.SequenceNo = tci2.SequenceNo;
                                    detail2.ReferenceNo = tci2.ReferenceNo;
                                    detail2.ReferenceSequenceNo = tci2.ReferenceSequenceNo;
                                    detail2.ItemID = tci2.ItemID;
                                    detail2.ChargeClassID = tci2.ChargeClassID;
                                    detail2.ParamedicID = tci2.ParamedicID;
                                    detail2.SecondParamedicID = tci2.SecondParamedicID;
                                    detail2.IsAdminCalculation = tci2.IsAdminCalculation;
                                    detail2.IsVariable = tci2.IsVariable;
                                    detail2.IsCito = tci2.IsCito;
                                    detail2.ChargeQuantity = tci2.ChargeQuantity;
                                    detail2.StockQuantity = tci2.StockQuantity;
                                    detail2.SRItemUnit = tci2.SRItemUnit;
                                    detail2.CostPrice = tci2.CostPrice;
                                    detail2.Price = tci2.Price;
                                    detail2.DiscountAmount = tci2.DiscountAmount;
                                    detail2.CitoAmount = tci2.CitoAmount;
                                    detail2.RoundingAmount = tci2.RoundingAmount;
                                    detail2.SRDiscountReason = tci2.SRDiscountReason;
                                    detail2.IsAssetUtilization = tci2.IsAssetUtilization;
                                    detail2.AssetID = tci2.AssetID;
                                    detail2.IsBillProceed = tci2.IsBillProceed;
                                    detail2.IsOrderRealization = tci2.IsOrderRealization;
                                    detail2.IsPaymentConfirmed = tci2.IsPaymentConfirmed;
                                    detail2.IsPackage = tci2.IsPackage;
                                    detail2.IsApprove = tci2.IsApprove;
                                    detail2.IsVoid = tci2.IsVoid;
                                    detail2.Notes = tci2.Notes;
                                    detail2.FilmNo = tci2.FilmNo;
                                    detail2.TariffDate = tci2.TariffDate;

                                    detail2.LastUpdateDateTime = tci2.LastUpdateDateTime;
                                    detail2.LastUpdateByUserID = tci2.LastUpdateByUserID;
                                    detail2.ParentNo = tci2.ParentNo;
                                    detail2.SRCenterID = tci2.SRCenterID;
                                    detail2.AutoProcessCalculation = tci2.AutoProcessCalculation;
                                    detail2.ParamedicCollectionName = tci2.ParamedicCollectionName;
                                    detail2.ToServiceUnitID = tci2.ToServiceUnitID;
                                    detail2.IsCitoInPercent = tci2.IsCitoInPercent;
                                    detail2.BasicCitoAmount = tci2.BasicCitoAmount;
                                    detail2.IsItemRoom = tci2.IsItemRoom;
                                    detail2.IsItemRoom = tci2.IsItemRoom;

                                    detail2.SRCitoPercentage = tci2.SRCitoPercentage;
                                    detail2.ItemConditionRuleID = tci2.ItemConditionRuleID;

                                    if (tci2.IsExtraItem ?? false)
                                    {
                                        detail2.IsExtraItem = tci2.IsExtraItem;
                                        detail2.IsSelectedExtraItem = tci2.IsSelectedExtraItem;
                                    }

                                    var tcis3 = TransChargesItems.Where(t => t.ParentNo == tci2.SequenceNo)
                                                                 .OrderBy(t => t.SequenceNo);
                                    foreach (var tci3 in tcis3)
                                    {
                                        var detail3 = details.AddNew();
                                        detail3.TransactionNo = header.TransactionNo;
                                        detail3.SequenceNo = tci3.SequenceNo;
                                        detail3.ReferenceNo = tci3.ReferenceNo;
                                        detail3.ReferenceSequenceNo = tci3.ReferenceSequenceNo;
                                        detail3.ItemID = tci3.ItemID;
                                        detail3.ChargeClassID = tci3.ChargeClassID;
                                        detail3.ParamedicID = tci3.ParamedicID;
                                        detail3.SecondParamedicID = tci3.SecondParamedicID;
                                        detail3.IsAdminCalculation = tci3.IsAdminCalculation;
                                        detail3.IsVariable = tci3.IsVariable;
                                        detail3.IsCito = tci3.IsCito;
                                        detail3.ChargeQuantity = tci3.ChargeQuantity;
                                        detail3.StockQuantity = tci3.StockQuantity;
                                        detail3.SRItemUnit = tci3.SRItemUnit;
                                        detail3.CostPrice = tci3.CostPrice;
                                        detail3.Price = tci3.Price;
                                        detail3.DiscountAmount = tci3.DiscountAmount;
                                        detail3.CitoAmount = tci3.CitoAmount;
                                        detail3.RoundingAmount = tci3.RoundingAmount;
                                        detail3.SRDiscountReason = tci3.SRDiscountReason;
                                        detail3.IsAssetUtilization = tci3.IsAssetUtilization;
                                        detail3.AssetID = tci3.AssetID;
                                        detail3.IsBillProceed = tci3.IsBillProceed;
                                        detail3.IsOrderRealization = tci3.IsOrderRealization;
                                        detail3.IsPaymentConfirmed = tci3.IsPaymentConfirmed;
                                        detail3.IsPackage = tci3.IsPackage;
                                        detail3.IsApprove = tci3.IsApprove;
                                        detail3.IsVoid = tci3.IsVoid;
                                        detail3.Notes = tci3.Notes;
                                        detail3.FilmNo = tci3.FilmNo;
                                        detail3.TariffDate = tci3.TariffDate;

                                        detail3.LastUpdateDateTime = tci3.LastUpdateDateTime;
                                        detail3.LastUpdateByUserID = tci3.LastUpdateByUserID;
                                        detail3.ParentNo = tci3.ParentNo;
                                        detail3.SRCenterID = tci3.SRCenterID;
                                        detail3.AutoProcessCalculation = tci3.AutoProcessCalculation;
                                        detail3.ParamedicCollectionName = tci3.ParamedicCollectionName;
                                        detail3.ToServiceUnitID = tci3.ToServiceUnitID;
                                        detail3.IsCitoInPercent = tci3.IsCitoInPercent;
                                        detail3.BasicCitoAmount = tci3.BasicCitoAmount;
                                        detail3.IsItemRoom = tci3.IsItemRoom;
                                        detail3.IsItemRoom = tci3.IsItemRoom;

                                        detail3.SRCitoPercentage = tci3.SRCitoPercentage;
                                        detail3.ItemConditionRuleID = tci3.ItemConditionRuleID;

                                        if (tci3.IsExtraItem ?? false)
                                        {
                                            detail3.IsExtraItem = tci3.IsExtraItem;
                                            detail3.IsSelectedExtraItem = tci3.IsSelectedExtraItem;
                                        }
                                    }
                                }

                                var tcics = TransChargesItemComps.Where(t => t.SequenceNo == tci.SequenceNo)
                                                                 .OrderBy(t => t.TariffComponentID);
                                //component
                                #region component
                                foreach (var tcic in tcics)
                                {
                                    var component = TransChargesItemComps.AddNew();
                                    component.TransactionNo = detail.TransactionNo;
                                    component.SequenceNo = detail.SequenceNo;
                                    component.TariffComponentID = tcic.TariffComponentID;
                                    component.Price = tcic.Price;
                                    component.DiscountAmount = tcic.DiscountAmount;
                                    component.ParamedicID = tcic.ParamedicID;
                                    component.LastUpdateDateTime = tcic.LastUpdateDateTime;
                                    component.LastUpdateByUserID = tcic.LastUpdateByUserID;
                                    component.IsPackage = tcic.IsPackage;
                                    component.AutoProcessCalculation = tcic.AutoProcessCalculation;
                                    component.CitoAmount = tcic.CitoAmount;

                                    tcic.MarkAsDeleted();
                                }
                                #endregion

                                var cons = TransChargesItemConsumptions.Where(t => t.SequenceNo == tci.SequenceNo)
                                                                       .OrderBy(t => t.DetailItemID);
                                //consumption
                                #region consumption
                                foreach (var con in cons)
                                {
                                    var consumption = consumptions.AddNew();
                                    consumption.TransactionNo = detail.TransactionNo;
                                    consumption.SequenceNo = detail.SequenceNo;
                                    consumption.DetailItemID = con.DetailItemID;
                                    consumption.Qty = con.Qty;
                                    consumption.QtyRealization = con.QtyRealization;
                                    consumption.SRItemUnit = con.SRItemUnit;
                                    consumption.Price = con.Price;
                                    consumption.AveragePrice = con.AveragePrice;
                                    consumption.FifoPrice = con.FifoPrice;
                                    consumption.LastUpdateDateTime = con.LastUpdateDateTime;
                                    consumption.LastUpdateByUserID = con.LastUpdateByUserID;
                                    consumption.IsPackage = con.IsPackage;

                                    con.MarkAsDeleted();
                                }
                                #endregion

                                tci.MarkAsDeleted();
                            }
                        }

                        headers.Save();
                        details.Save();
                        components.Save();
                        consumptions.Save();

                        TransChargesItems.Save();
                        TransChargesItemComps.Save();
                        TransChargesItemConsumptions.Save();
                    }

                    //header
                    entity.IsApproved = isApproval;
                    entity.ApprovedDateTime = (new DateTime()).NowAtSqlServer();
                    entity.ApprovedByUserID = AppSession.UserLogin.UserID;

                    if (QueryString_type != "jo") entity.IsBillProceed = isApproval;
                    else
                    {
                        if (AppSession.Parameter.IsUsingHisInterop)
                        {
                            if (AppSession.Parameter.HealthcareInitialAppsVersion == "YBRSGKP")
                            {
                                if (AppSession.Parameter.HealthcareInitial == "RSI")
                                {
                                    if (entity.ToServiceUnitID == AppSession.Parameter.ServiceUnitLaboratoryID)
                                    {
                                        bool poctExist = false;
                                        //foreach (var entity2 in TransChargesItems.Where(t => t.IsOrderRealization ?? false && t.IsVoid == false))
                                        foreach (var entity2 in TransChargesItems.Where(t => t.IsVoid == false))
                                        {
                                            var item = new Item();
                                            item.LoadByPrimaryKey(entity2.ItemID);

                                            if (!string.IsNullOrEmpty(item.ItemIDExternal))
                                            {
                                                poctExist = true;
                                                break;
                                            }
                                        }
                                        if (poctExist) entity.IsBillProceed = isApproval;
                                    }
                                }
                            }
                        }
                    }

                    if (QueryString_type == "jo")
                        entity.SROrderStatus = "1";
                    else if (QueryString_type == "ds")
                        entity.SROrderStatus = "2";

                    entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                    var grrID = reg.GuarantorID;

                    var pat = new Patient();
                    pat.LoadByPrimaryKey(reg.PatientID);

                    if (grrID == AppSession.Parameter.SelfGuarantor)
                    {
                        if (!string.IsNullOrEmpty(pat.MemberID)) grrID = pat.MemberID;
                    }

                    var grr = new Guarantor();
                    grr.LoadByPrimaryKey(reg.GuarantorID);
                    var tariffDate = grr.TariffCalculationMethod == 1
                        ? reg.RegistrationDate.Value.Date
                        : entity.TransactionDate.Value;

                    //var unit = new ServiceUnit();
                    //unit.LoadByPrimaryKey(entity.ToServiceUnitID);

                    var tblCovered = new DataTable();
                    if ((QueryString_type != "jo") && isApproval)
                    {
                        tblCovered = Helper.GetCoveredItems(reg.RegistrationNo, grrID, ClassID, reg.CoverageClassID, (TransChargesItems.Where(t => !(t.IsVoid ?? false)).Select(t => t.ItemID)).ToArray(),
                            tariffDate, false);
                    }
                    else
                    {
                        if (AppSession.Parameter.IsUsingHisInterop)
                        {
                            if (AppSession.Parameter.HealthcareInitialAppsVersion == "YBRSGKP")
                            {
                                if (AppSession.Parameter.HealthcareInitial == "RSI")
                                {
                                    if (entity.ToServiceUnitID == AppSession.Parameter.ServiceUnitLaboratoryID)
                                    {
                                        tblCovered = Helper.GetCoveredItems(reg.RegistrationNo, grrID, ClassID, reg.CoverageClassID, (TransChargesItems.Where(t => !(t.IsVoid ?? false)).Select(t => t.ItemID)).ToArray(), tariffDate, false);
                                    }
                                }
                            }
                        }

                    }

                    //cost calculation
                    if (QueryString_type != "jo")
                    {
                        if (isApproval)
                        {
                            foreach (TransChargesItem detail in TransChargesItems.Where(t => string.IsNullOrEmpty(t.ParentNo) && !(t.IsVoid ?? false)))
                            {
                                //--- untuk item detail paket, covered diambil dari header item paket
                                string itemId = detail.ItemID;
                                if (!string.IsNullOrEmpty(entity.PackageReferenceNo))
                                {
                                    var tciPackageRef = new TransChargesItem();
                                    if (tciPackageRef.LoadByPrimaryKey(entity.PackageReferenceNo, detail.SequenceNo.Substring(0, 3))) itemId = tciPackageRef.ItemID;

                                    tblCovered = Helper.GetCoveredItems(reg.RegistrationNo, grrID, reg.CoverageClassID, itemId, tariffDate, false);
                                }

                                //var rowCovered = tblCovered.AsEnumerable().SingleOrDefault(t => t.Field<string>("ItemID") == detail.ItemID &&
                                //                                                                t.Field<bool>("IsInclude"));

                                var rowCovered = tblCovered.AsEnumerable().SingleOrDefault(t => t.Field<string>("ItemID") == itemId && t.Field<bool>("IsInclude"));
                                bool isTransChargesItemComps = false;
                                //TransChargesItemComps
                                if (rowCovered != null)
                                {
                                    decimal? discount = 0;
                                    bool isDiscount = false, isMargin = false;

                                    foreach (var comp in TransChargesItemComps.Where(t => t.TransactionNo == detail.TransactionNo && t.SequenceNo == detail.SequenceNo)
                                                                              .OrderBy(t => t.TariffComponentID))
                                    {
                                        decimal? amountValue = 0;
                                        decimal? basicPrice = 0;
                                        decimal? coveragePrice = 0;

                                        if (Convert.ToBoolean(rowCovered["IsByTariffComponent"]))
                                        {
                                            var array = rowCovered["TariffComponentValue"].ToString().Split(';').Where(l => l.Split('/')[2] == comp.TariffComponentID).SingleOrDefault();
                                            if (array == null)
                                            {
                                                amountValue = (decimal?)rowCovered["AmountValue"];
                                                basicPrice = (decimal?)rowCovered["BasicPrice"];
                                                coveragePrice = (decimal?)rowCovered["CoveragePrice"];
                                            }
                                            else
                                            {
                                                var list = array.Split('/');
                                                if (list == null || list.Count() == 0)
                                                {
                                                    amountValue = (decimal?)rowCovered["AmountValue"];
                                                    basicPrice = (decimal?)rowCovered["BasicPrice"];
                                                    coveragePrice = (decimal?)rowCovered["CoveragePrice"];
                                                }
                                                else
                                                {
                                                    amountValue = Convert.ToDecimal(list[3]);
                                                    basicPrice = Convert.ToDecimal(list[0]);
                                                    coveragePrice = Convert.ToDecimal(list[1]);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            amountValue = (decimal?)rowCovered["AmountValue"];
                                            basicPrice = (decimal?)rowCovered["BasicPrice"];
                                            coveragePrice = (decimal?)rowCovered["CoveragePrice"];
                                        }

                                        isTransChargesItemComps = true;

                                        basicPrice = Helper.Tariff.GetItemConditionRuleTariff(basicPrice ?? 0, detail.ItemConditionRuleID, detail.TariffDate ?? tariffDate);
                                        coveragePrice = Helper.Tariff.GetItemConditionRuleTariff(coveragePrice ?? 0, detail.ItemConditionRuleID, detail.TariffDate ?? tariffDate);

                                        if (rowCovered["SRGuarantorRuleType"].ToString().Equals(AppSession.Parameter.GuarantorRuleTypeDiscount))
                                        {
                                            if (comp.DiscountAmount > 0) continue;
                                            if ((comp.Price - comp.DiscountAmount) <= 0) continue;

                                            var compPrice = comp.Price;
                                            if (basicPrice > coveragePrice)
                                            {
                                                var tcomp = Helper.Tariff.GetItemTariffComponent(detail.TariffDate ?? tariffDate, grr.SRTariffType, reg.CoverageClassID, comp.TariffComponentID, detail.ItemID);
                                                if (!tcomp.AsEnumerable().Any())
                                                    tcomp = Helper.Tariff.GetItemTariffComponent(detail.TariffDate ?? tariffDate, grr.SRTariffType, AppSession.Parameter.DefaultTariffClass, comp.TariffComponentID,
                                                        detail.ItemID);
                                                if (!tcomp.AsEnumerable().Any())
                                                    tcomp = Helper.Tariff.GetItemTariffComponent(detail.TariffDate ?? tariffDate, AppSession.Parameter.DefaultTariffType, reg.CoverageClassID, comp.TariffComponentID,
                                                        detail.ItemID);
                                                if (!tcomp.AsEnumerable().Any())
                                                    tcomp = Helper.Tariff.GetItemTariffComponent(detail.TariffDate ?? tariffDate, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass,
                                                        comp.TariffComponentID, detail.ItemID);

                                                if (!tcomp.AsEnumerable().Any()) continue;

                                                compPrice = tcomp.AsEnumerable().Select(c => c.Field<decimal>("Price")).Single();
                                                if (!string.IsNullOrEmpty(detail.ItemConditionRuleID)) compPrice = Helper.Tariff.GetItemConditionRuleTariff(compPrice ?? 0, detail.ItemConditionRuleID,
                                                    tariffDate);
                                            }

                                            decimal basicCitoAmount = detail.BasicCitoAmount ?? 0;
                                            decimal compCitoAmt = (compPrice ?? 0) * basicCitoAmount / 100;

                                            if ((bool)rowCovered["IsValueInPercent"])
                                            {
                                                var discountRule = (amountValue / 100) * (compPrice + compCitoAmt);
                                                var fee = comp.CalculateParamedicPercentDiscount(AppSession.Parameter.IsTarifCompPhysicianDiscountMaxByShare, entity.RegistrationNo, detail.ItemID, discountRule,
                                                    AppSession.UserLogin.UserID, entity.ClassID, entity.ToServiceUnitID);
                                                comp.AutoProcessCalculation = 0 - comp.DiscountAmount;

                                                //comp.DiscountAmount = (amountValue / 100) * compPrice;
                                                //comp.AutoProcessCalculation = 0 - (amountValue / 100) * compPrice;
                                            }
                                            else
                                            {
                                                //if (!isDiscount)
                                                //{
                                                //    if (discount == 0)
                                                //    {
                                                if (detail.Price > compPrice) amountValue = ((compPrice + compCitoAmt) / (detail.Price + (detail.CitoAmount / Math.Abs(detail.ChargeQuantity ?? 0)))) * amountValue;

                                                if (compPrice + compCitoAmt >= amountValue)
                                                {
                                                    var discountRule = amountValue;
                                                    var fee = comp.CalculateParamedicPercentDiscount(AppSession.Parameter.IsTarifCompPhysicianDiscountMaxByShare, entity.RegistrationNo, detail.ItemID,
                                                        discountRule, AppSession.UserLogin.UserID, entity.ClassID, entity.ToServiceUnitID);
                                                    comp.AutoProcessCalculation = 0 - comp.DiscountAmount;

                                                    //comp.DiscountAmount = amountValue;
                                                    //comp.AutoProcessCalculation = 0 - amountValue;
                                                    //isDiscount = true;
                                                }
                                                else
                                                {
                                                    var discountRule = compPrice + compCitoAmt;
                                                    var fee = comp.CalculateParamedicPercentDiscount(AppSession.Parameter.IsTarifCompPhysicianDiscountMaxByShare, entity.RegistrationNo, detail.ItemID,
                                                        discountRule, AppSession.UserLogin.UserID, entity.ClassID, entity.ToServiceUnitID);
                                                    comp.AutoProcessCalculation = 0 - comp.DiscountAmount;

                                                    //comp.DiscountAmount = compPrice;
                                                    //comp.AutoProcessCalculation = 0 - compPrice;
                                                    //discount = amountValue - compPrice;
                                                }
                                                //    }
                                                //    else
                                                //    {
                                                //        if (compPrice >= discount)
                                                //        {
                                                //            comp.DiscountAmount = discount;
                                                //            comp.AutoProcessCalculation = 0 - discount;
                                                //            isDiscount = true;
                                                //        }
                                                //        else
                                                //        {
                                                //            comp.DiscountAmount = compPrice;
                                                //            comp.AutoProcessCalculation = 0 - compPrice;
                                                //            discount -= compPrice;
                                                //        }
                                                //    }
                                                //}
                                            }
                                        }
                                        else if (rowCovered["SRGuarantorRuleType"].ToString().Equals(AppSession.Parameter.GuarantorRuleTypeMargin))
                                        {
                                            decimal basicCitoAmount = detail.BasicCitoAmount ?? 0;

                                            if ((bool)rowCovered["IsValueInPercent"])
                                            {
                                                comp.AutoProcessCalculation = (amountValue / 100) * (comp.Price + comp.CitoAmount);
                                                comp.Price += (amountValue / 100) * comp.Price;
                                                comp.CitoAmount += (amountValue / 100) * comp.CitoAmount;

                                                var discountRule = 0;
                                                var fee = comp.CalculateParamedicPercentDiscount(AppSession.Parameter.IsTarifCompPhysicianDiscountMaxByShare, entity.RegistrationNo, detail.ItemID,
                                                    discountRule, AppSession.UserLogin.UserID, entity.ClassID, entity.ToServiceUnitID);
                                                comp.AutoProcessCalculation = comp.AutoProcessCalculation - comp.DiscountAmount;
                                            }
                                            else
                                            {
                                                if (!isMargin)
                                                {
                                                    comp.Price += amountValue;
                                                    comp.CitoAmount = comp.Price * basicCitoAmount / 100;
                                                    comp.AutoProcessCalculation = amountValue + comp.CitoAmount;
                                                    isMargin = true;

                                                    var discountRule = 0;
                                                    var fee = comp.CalculateParamedicPercentDiscount(AppSession.Parameter.IsTarifCompPhysicianDiscountMaxByShare, entity.RegistrationNo, detail.ItemID,
                                                        discountRule, AppSession.UserLogin.UserID, entity.ClassID, entity.ToServiceUnitID);
                                                    comp.AutoProcessCalculation = amountValue + comp.CitoAmount - comp.DiscountAmount;
                                                }
                                            }
                                        }
                                        comp.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                        comp.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                                    }
                                }

                                //TransChargesItems
                                detail.IsApprove = isApproval;
                                detail.IsBillProceed = isApproval;
                                //jangan di remark, kalo mo remark, tanya deby dulu
                                if (QueryString_type == "ds")
                                {
                                    detail.IsOrderRealization = true;
                                    detail.IsPaymentConfirmed = false;
                                    detail.RealizationDateTime = (new DateTime()).NowAtSqlServer();
                                    detail.RealizationUserID = AppSession.UserLogin.UserID;
                                    //detail.IsSendToLIS = entity.ToServiceUnitID == AppSession.Parameter.ServiceUnitLaboratoryID;
                                }
                                //end jangan di remark
                                //if (TransChargesItemComps.Count > 0)
                                if (isTransChargesItemComps)
                                {
                                    detail.AutoProcessCalculation = TransChargesItemComps.Where(t => t.TransactionNo == detail.TransactionNo && t.SequenceNo == detail.SequenceNo)
                                                                                         .Sum(t => t.AutoProcessCalculation);
                                    if (detail.AutoProcessCalculation < 0)
                                    {
                                        //detail.DiscountAmount += detail.ChargeQuantity * Math.Abs(detail.AutoProcessCalculation ?? 0);
                                        detail.DiscountAmount = detail.ChargeQuantity * Math.Abs(detail.AutoProcessCalculation ?? 0);

                                        if (detail.DiscountAmount > (detail.Price * Math.Abs(detail.ChargeQuantity ?? 0)) + detail.CitoAmount)
                                        {
                                            detail.DiscountAmount = (detail.Price * Math.Abs(detail.ChargeQuantity ?? 0)) + detail.CitoAmount;
                                            detail.AutoProcessCalculation = 0 - (detail.Price + (detail.CitoAmount / Math.Abs(detail.ChargeQuantity ?? 0)));
                                        }
                                    }
                                    else if (detail.AutoProcessCalculation > 0) detail.Price += detail.AutoProcessCalculation;
                                }
                                else
                                {
                                    if (rowCovered != null)
                                    {
                                        if (rowCovered["SRGuarantorRuleType"].ToString().Equals(AppSession.Parameter.GuarantorRuleTypeDiscount))
                                        {
                                            var basicPrice = (decimal?)rowCovered["BasicPrice"];
                                            var coveragePrice = (decimal?)rowCovered["CoveragePrice"];
                                            if (!string.IsNullOrEmpty(detail.ItemConditionRuleID))
                                            {
                                                basicPrice = Helper.Tariff.GetItemConditionRuleTariff(basicPrice ?? 0, detail.ItemConditionRuleID, detail.TariffDate ?? tariffDate);
                                                coveragePrice = Helper.Tariff.GetItemConditionRuleTariff(coveragePrice ?? 0, detail.ItemConditionRuleID, detail.TariffDate ?? tariffDate);
                                            }

                                            var detailPrice = detail.Price ?? 0;
                                            if (basicPrice > coveragePrice)
                                            {
                                                ItemTariff tariff = (Helper.Tariff.GetItemTariff(detail.TariffDate ?? tariffDate, grr.SRTariffType, reg.CoverageClassID, reg.CoverageClassID, detail.ItemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
                                                         Helper.Tariff.GetItemTariff(detail.TariffDate ?? tariffDate, grr.SRTariffType, AppSession.Parameter.DefaultTariffClass, reg.CoverageClassID, detail.ItemID, reg.GuarantorID, false, reg.SRRegistrationType)) ??
                                                        (Helper.Tariff.GetItemTariff(detail.TariffDate ?? tariffDate, AppSession.Parameter.DefaultTariffType, reg.CoverageClassID, reg.CoverageClassID, detail.ItemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
                                                         Helper.Tariff.GetItemTariff(detail.TariffDate ?? tariffDate, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, reg.CoverageClassID, detail.ItemID, reg.GuarantorID, false, reg.SRRegistrationType));
                                                if (tariff != null)
                                                {
                                                    //detailPrice = tariff.Price ?? 0;
                                                    detailPrice = Helper.Tariff.GetItemConditionRuleTariff(tariff.Price ?? 0, detail.ItemConditionRuleID, detail.TariffDate ?? tariffDate);
                                                }
                                            }

                                            if ((bool)rowCovered["IsValueInPercent"])
                                            {
                                                detail.DiscountAmount = (detail.ChargeQuantity ?? 0) * (((decimal)rowCovered["AmountValue"] / 100) * detailPrice);
                                                detail.AutoProcessCalculation = 0 - (((decimal)rowCovered["AmountValue"] / 100) * detailPrice);
                                            }
                                            else
                                            {
                                                detail.DiscountAmount = (detail.ChargeQuantity ?? 0) * (decimal)rowCovered["AmountValue"];
                                                detail.AutoProcessCalculation = 0 - (decimal)rowCovered["AmountValue"];
                                            }

                                            if (detail.DiscountAmount > (detailPrice * detail.ChargeQuantity))
                                                detail.DiscountAmount = detailPrice * detail.ChargeQuantity;
                                        }
                                        else if (rowCovered["SRGuarantorRuleType"].ToString().Equals(AppSession.Parameter.GuarantorRuleTypeMargin))
                                        {
                                            if ((bool)rowCovered["IsValueInPercent"])
                                            {
                                                detail.AutoProcessCalculation = ((decimal)rowCovered["AmountValue"] / 100) * detail.Price;
                                                detail.Price += ((decimal)rowCovered["AmountValue"] / 100) * detail.Price;
                                            }
                                            else
                                            {
                                                detail.Price += (decimal)rowCovered["AmountValue"];
                                                detail.AutoProcessCalculation = (decimal)rowCovered["AmountValue"];
                                            }
                                        }
                                    }
                                }

                                if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSCH" && string.IsNullOrEmpty(detail.FilmNo))
                                {
                                    var item = new Item();
                                    item.LoadByPrimaryKey(detail.ItemID);
                                    if (item.Notes.Length > 0 && item.SRItemType != ItemType.Medical && item.SRItemType != ItemType.NonMedical && item.SRItemType != ItemType.Kitchen)
                                    {
                                        amplopFilmAutoNumber = Helper.GetNewAutoNumber(TransactionDate.Date, AppEnum.AutoNumber.AmplopFilmNo,
                                            item.Notes.Length >= 3 ? item.Notes.Substring(0, 3).ToUpper() : item.Notes.ToUpper(), AppSession.UserLogin.UserID);

                                        var filmNo = amplopFilmAutoNumber.LastCompleteNumber;
                                        amplopFilmAutoNumber.Save();

                                        detail.FilmNo = filmNo;
                                    }
                                }
                                else if (AppSession.Parameter.HealthcareInitialAppsVersion == "YBRSGKP" && AppSession.Parameter.HealthcareInitial == "RSI")
                                {
                                    //if (string.IsNullOrEmpty(detail.FilmNo))
                                    {
                                        var i = new Item();
                                        i.LoadByPrimaryKey(detail.ItemID);
                                        if (i.SRItemType == ItemType.Radiology)
                                        {
                                            if (i.ItemIDExternal == "CR")
                                            {
                                                amplopFilmAutoNumber = Helper.GetNewAutoNumber(DateTime.Now.Date, AppEnum.AutoNumber.CRRadiologyFilmNo);
                                                detail.FilmNo = amplopFilmAutoNumber.LastCompleteNumber;
                                                amplopFilmAutoNumber.Save();
                                            }
                                            else if (i.ItemIDExternal == "CT")
                                            {
                                                amplopFilmAutoNumber = Helper.GetNewAutoNumber(DateTime.Now.Date, AppEnum.AutoNumber.CTRadiologyFilmNo);
                                                detail.FilmNo = amplopFilmAutoNumber.LastCompleteNumber;
                                                amplopFilmAutoNumber.Save();
                                            }
                                        }
                                    }
                                }

                                detail.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                detail.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                                //post
                                decimal? total = ((detail.ChargeQuantity * detail.Price) - detail.DiscountAmount) + detail.CitoAmount;
                                decimal? qty = detail.ChargeQuantity;

                                //var calc = new Helper.CostCalculation(grrID, detail.ItemID, total ?? 0, tblCovered, qty ?? 0,
                                //                                      detail.IsCito ?? false,
                                //                                      detail.IsCitoInPercent ?? false,
                                //                                      detail.BasicCitoAmount ?? 0, detail.Price ?? 0,
                                //                                      entity.IsRoomIn ?? false, detail.IsItemRoom ?? false,
                                //                                      entity.TariffDiscountForRoomIn ?? 0, detail.DiscountAmount ?? 0, false,
                                //                                      detail.ItemConditionRuleID, detail.TariffDate ?? tariffDate, detail.IsVariable ?? false);

                                var calc = new Helper.CostCalculation(grrID, itemId, total ?? 0, tblCovered, qty ?? 0,
                                                                      detail.IsCito ?? false,
                                                                      detail.IsCitoInPercent ?? false,
                                                                      detail.BasicCitoAmount ?? 0, detail.Price ?? 0,
                                                                      entity.IsRoomIn ?? false, detail.IsItemRoom ?? false,
                                                                      entity.TariffDiscountForRoomIn ?? 0, detail.DiscountAmount ?? 0, false,
                                                                      detail.ItemConditionRuleID, detail.TariffDate ?? tariffDate, detail.IsVariable ?? false);

                                var package = new GuarantorSurgicalPackageCoveredItem();
                                if (package.LoadByPrimaryKey(grrID, entity.SurgicalPackageID, detail.ItemID))
                                {
                                    if (calc.PatientAmount + calc.GuarantorAmount <= package.CoveredAmount)
                                    {
                                        calc.GuarantorAmount = calc.PatientAmount + calc.GuarantorAmount;
                                        calc.PatientAmount = 0;
                                    }
                                    else
                                    {
                                        calc.PatientAmount = calc.PatientAmount + calc.GuarantorAmount - package.CoveredAmount ?? 0;
                                        calc.GuarantorAmount = package.CoveredAmount ?? 0;
                                    }
                                }

                                //CostCalculation

                                // 20160912 terjadi duplikasi CostCalculation padahal transaksinya (cth RSMP: SU161205-0192) belum approve
                                var costs = CostCalculations.Where(cc => cc.TransactionNo == detail.TransactionNo && cc.SequenceNo == detail.SequenceNo);
                                CostCalculation cost;
                                if (costs.Count() == 1)
                                {
                                    cost = costs.First();
                                    new BasePage().LogError(new Exception(string.Format("Dev Warning: duplication of cost calculation on TransactionNo {0}, SequenceNo {1}, ItemID {2}. Potential of duplicate stock taking!!",
                                        cost.TransactionNo, cost.SequenceNo, cost.ItemID)));
                                }
                                else cost = CostCalculations.AddNew();

                                cost.RegistrationNo = entity.RegistrationNo;
                                cost.TransactionNo = detail.TransactionNo;
                                cost.SequenceNo = detail.SequenceNo;
                                cost.ItemID = detail.ItemID;

                                //start here
                                decimal? totaltrans = calc.GuarantorAmount + calc.PatientAmount + (detail.DiscountAmount ?? 0);
                                decimal? totaldisc = detail.DiscountAmount ?? 0;

                                if (reg.SRBussinesMethod == AppSession.Parameter.BusinessMethodFlavon)
                                {
                                    if (totaldisc >= totaltrans)
                                    {
                                        cost.GuarantorAmount = 0;
                                        cost.PatientAmount = 0;
                                    }
                                    else
                                    {
                                        cost.GuarantorAmount = totaltrans - totaldisc;
                                        cost.PatientAmount = 0;
                                    }
                                    cost.DiscountAmount = totaldisc;
                                }
                                else
                                {
                                    if (calc.GuarantorAmount > 0)
                                    {
                                        cost.DiscountAmount = totaldisc > (calc.GuarantorAmount + detail.DiscountAmount)
                                                                   ? (calc.GuarantorAmount + detail.DiscountAmount)
                                                                   : totaldisc;

                                        cost.GuarantorAmount = totaldisc > (calc.GuarantorAmount + detail.DiscountAmount)
                                                                   ? 0
                                                                   : (calc.GuarantorAmount + detail.DiscountAmount) - totaldisc;
                                        cost.PatientAmount = calc.PatientAmount;

                                    }
                                    else
                                    {
                                        cost.DiscountAmount = totaldisc > calc.PatientAmount + detail.DiscountAmount
                                                                  ? calc.PatientAmount + detail.DiscountAmount
                                                                  : totaldisc;

                                        cost.PatientAmount = totaldisc > calc.PatientAmount + detail.DiscountAmount
                                                                 ? 0
                                                                 : calc.PatientAmount + detail.DiscountAmount - totaldisc;
                                        cost.GuarantorAmount = calc.GuarantorAmount;
                                    }

                                    if (totaldisc > cost.DiscountAmount)
                                    {
                                        //hitung ulang diskon di TransChargesItem & TransChargesItemComp
                                        if (isTransChargesItemComps)
                                        {
                                            var compColl = TransChargesItemComps.Where(
                                                t =>
                                                t.TransactionNo == detail.TransactionNo &&
                                                t.SequenceNo == detail.SequenceNo)
                                                .OrderBy(t => t.TariffComponentID);
                                            var i = compColl.Count();

                                            foreach (var compEntity in compColl)
                                            {
                                                compEntity.DiscountAmount = i == 1
                                                                           ? (cost.DiscountAmount / Math.Abs(detail.ChargeQuantity ?? 0))
                                                                           : (compEntity.Price + compEntity.CitoAmount) * (cost.DiscountAmount / detail.DiscountAmount);

                                                var fee = compEntity.CalculateParamedicPercentDiscount(
                                                    AppSession.Parameter.IsTarifCompPhysicianDiscountMaxByShare,
                                                    cost.RegistrationNo, detail.ItemID, (compEntity.DiscountAmount ?? 0),
                                                    AppSession.UserLogin.UserID, entity.ClassID, entity.ToServiceUnitID);

                                            }

                                            TransChargesItemComps.Save();
                                        }

                                        detail.DiscountAmount = cost.DiscountAmount;
                                        detail.Save();
                                    }
                                }
                                //end

                                cost.IsPackage = detail.IsPackage;
                                cost.ParentNo = detail.ParentNo;
                                cost.ParamedicAmount = detail.ChargeQuantity * TransChargesItemComps.Where(comp => comp.TransactionNo == detail.TransactionNo && comp.SequenceNo == detail.SequenceNo &&
                                                                                                                   !string.IsNullOrEmpty(comp.ParamedicID))
                                                                                                    .Sum(comp => comp.Price - comp.DiscountAmount + comp.CitoAmount);
                                cost.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                                cost.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            }

                            if (!string.IsNullOrEmpty(entity.PackageReferenceNo))
                            {
                                foreach (TransChargesItem detail in TransChargesItems.Where(t => string.IsNullOrEmpty(t.ParentNo) && (t.IsVoid ?? false)))
                                {
                                    foreach (var cons in TransChargesItemConsumptions.Where(cons => cons.SequenceNo == detail.SequenceNo))
                                    {
                                        cons.MarkAsDeleted();
                                    }

                                    if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSCH")
                                    {
                                        var parent = new TransChargesItem();
                                        if (parent.LoadByPrimaryKey(entity.PackageReferenceNo, detail.SequenceNo.Substring(0, 3)))
                                        {
                                            var package = new ItemPackage();
                                            package.Query.Where(package.Query.ItemID == parent.ItemID && package.Query.DetailItemID == detail.ItemID);
                                            if (package.Query.Load())
                                            {
                                                var comp = new ItemPackageTariffComponent();
                                                comp.Query.Select(comp.Query.Price.Sum());
                                                comp.Query.Where(comp.Query.ItemID == parent.ItemID && comp.Query.DetailItemID == detail.ItemID);
                                                if (comp.Query.Load())
                                                {
                                                    parent.DiscountAmount += (comp.Price - ((package.IsDiscountInPercent ?? false) ? (((package.DiscountValue ?? 0) / 100) * comp.Price) : (package.DiscountValue ?? 0)));
                                                    parent.AutoProcessCalculation += 0 - (comp.Price - ((package.IsDiscountInPercent ?? false) ? (((package.DiscountValue ?? 0) / 100) * comp.Price) : (package.DiscountValue ?? 0)));
                                                    parent.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                                    parent.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                                                    parent.Save();
                                                }
                                            }
                                        }

                                        var cmp = new TransChargesItemComp();
                                        if (cmp.LoadByPrimaryKey(entity.PackageReferenceNo, detail.SequenceNo.Substring(0, 3), AppSession.Parameter.TariffComponentJasaSaranaID))
                                        {
                                            cmp.DiscountAmount += parent.DiscountAmount;
                                            cmp.AutoProcessCalculation += 0 - parent.DiscountAmount;
                                            cmp.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                            cmp.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                                            cmp.Save();
                                        }

                                        var cc = new CostCalculation();
                                        if (cc.LoadByPrimaryKey(entity.RegistrationNo, entity.PackageReferenceNo, detail.SequenceNo.Substring(0, 3)))
                                        {
                                            cc.PatientAmount = cc.PatientAmount == 0 ? 0 : (parent.ChargeQuantity * parent.Price) - parent.DiscountAmount;
                                            cc.GuarantorAmount = cc.GuarantorAmount == 0 ? 0 : (parent.ChargeQuantity * parent.Price) - parent.DiscountAmount;
                                            cc.DiscountAmount += parent.DiscountAmount;
                                            cc.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                            cc.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                                            cc.Save();
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            CostCalculations.MarkAllAsDeleted();
                        }
                    }
                    else if (QueryString_type == "jo")
                    {
                        if (AppSession.Parameter.IsUsingHisInterop)
                        {
                            if (AppSession.Parameter.HealthcareInitialAppsVersion == "YBRSGKP")
                            {
                                if (AppSession.Parameter.HealthcareInitial == "RSI")
                                {
                                    if (entity.ToServiceUnitID == AppSession.Parameter.ServiceUnitLaboratoryID)
                                    {
                                        foreach (TransChargesItem detail in TransChargesItems.Where(t => string.IsNullOrEmpty(t.ParentNo) && !(t.IsVoid ?? false)))
                                        {
                                            var item = new Item();
                                            item.LoadByPrimaryKey(detail.ItemID);

                                            if (string.IsNullOrEmpty(item.ItemIDExternal)) continue;

                                            //--- untuk item detail paket, covered diambil dari header item paket
                                            string itemId = detail.ItemID;
                                            if (!string.IsNullOrEmpty(entity.PackageReferenceNo))
                                            {
                                                var tciPackageRef = new TransChargesItem();
                                                if (tciPackageRef.LoadByPrimaryKey(entity.PackageReferenceNo, detail.SequenceNo.Substring(0, 3))) itemId = tciPackageRef.ItemID;

                                                tblCovered = Helper.GetCoveredItems(reg.RegistrationNo, grrID, reg.CoverageClassID, itemId, tariffDate, false);
                                            }

                                            //var rowCovered = tblCovered.AsEnumerable().SingleOrDefault(t => t.Field<string>("ItemID") == detail.ItemID &&
                                            //                                                                t.Field<bool>("IsInclude"));

                                            var rowCovered = tblCovered.AsEnumerable().SingleOrDefault(t => t.Field<string>("ItemID") == itemId && t.Field<bool>("IsInclude"));
                                            bool isTransChargesItemComps = false;
                                            //TransChargesItemComps
                                            if (rowCovered != null)
                                            {
                                                decimal? discount = 0;
                                                bool isDiscount = false, isMargin = false;

                                                foreach (var comp in TransChargesItemComps.Where(t => t.TransactionNo == detail.TransactionNo && t.SequenceNo == detail.SequenceNo)
                                                                                          .OrderBy(t => t.TariffComponentID))
                                                {
                                                    decimal? amountValue = 0;
                                                    decimal? basicPrice = 0;
                                                    decimal? coveragePrice = 0;

                                                    if (Convert.ToBoolean(rowCovered["IsByTariffComponent"]))
                                                    {
                                                        var array = rowCovered["TariffComponentValue"].ToString().Split(';').Where(l => l.Split('/')[2] == comp.TariffComponentID).SingleOrDefault();
                                                        if (array == null)
                                                        {
                                                            amountValue = (decimal?)rowCovered["AmountValue"];
                                                            basicPrice = (decimal?)rowCovered["BasicPrice"];
                                                            coveragePrice = (decimal?)rowCovered["CoveragePrice"];
                                                        }
                                                        else
                                                        {
                                                            var list = array.Split('/');
                                                            if (list == null || list.Count() == 0)
                                                            {
                                                                amountValue = (decimal?)rowCovered["AmountValue"];
                                                                basicPrice = (decimal?)rowCovered["BasicPrice"];
                                                                coveragePrice = (decimal?)rowCovered["CoveragePrice"];
                                                            }
                                                            else
                                                            {
                                                                amountValue = Convert.ToDecimal(list[3]);
                                                                basicPrice = Convert.ToDecimal(list[0]);
                                                                coveragePrice = Convert.ToDecimal(list[1]);
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        amountValue = (decimal?)rowCovered["AmountValue"];
                                                        basicPrice = (decimal?)rowCovered["BasicPrice"];
                                                        coveragePrice = (decimal?)rowCovered["CoveragePrice"];
                                                    }

                                                    isTransChargesItemComps = true;

                                                    basicPrice = Helper.Tariff.GetItemConditionRuleTariff(basicPrice ?? 0, detail.ItemConditionRuleID, detail.TariffDate ?? tariffDate);
                                                    coveragePrice = Helper.Tariff.GetItemConditionRuleTariff(coveragePrice ?? 0, detail.ItemConditionRuleID, detail.TariffDate ?? tariffDate);

                                                    if (rowCovered["SRGuarantorRuleType"].ToString().Equals(AppSession.Parameter.GuarantorRuleTypeDiscount))
                                                    {
                                                        if (comp.DiscountAmount > 0) continue;
                                                        if ((comp.Price - comp.DiscountAmount) <= 0) continue;

                                                        var compPrice = comp.Price;
                                                        if (basicPrice > coveragePrice)
                                                        {
                                                            var tcomp = Helper.Tariff.GetItemTariffComponent(detail.TariffDate ?? tariffDate, grr.SRTariffType, reg.CoverageClassID, comp.TariffComponentID, detail.ItemID);
                                                            if (!tcomp.AsEnumerable().Any())
                                                                tcomp = Helper.Tariff.GetItemTariffComponent(detail.TariffDate ?? tariffDate, grr.SRTariffType, AppSession.Parameter.DefaultTariffClass, comp.TariffComponentID,
                                                                    detail.ItemID);
                                                            if (!tcomp.AsEnumerable().Any())
                                                                tcomp = Helper.Tariff.GetItemTariffComponent(detail.TariffDate ?? tariffDate, AppSession.Parameter.DefaultTariffType, reg.CoverageClassID, comp.TariffComponentID,
                                                                    detail.ItemID);
                                                            if (!tcomp.AsEnumerable().Any())
                                                                tcomp = Helper.Tariff.GetItemTariffComponent(detail.TariffDate ?? tariffDate, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass,
                                                                    comp.TariffComponentID, detail.ItemID);

                                                            if (!tcomp.AsEnumerable().Any()) continue;

                                                            compPrice = tcomp.AsEnumerable().Select(c => c.Field<decimal>("Price")).Single();
                                                            if (!string.IsNullOrEmpty(detail.ItemConditionRuleID)) compPrice = Helper.Tariff.GetItemConditionRuleTariff(compPrice ?? 0, detail.ItemConditionRuleID,
                                                                tariffDate);
                                                        }

                                                        decimal basicCitoAmount = detail.BasicCitoAmount ?? 0;
                                                        decimal compCitoAmt = (compPrice ?? 0) * basicCitoAmount / 100;

                                                        if ((bool)rowCovered["IsValueInPercent"])
                                                        {
                                                            var discountRule = (amountValue / 100) * (compPrice + compCitoAmt);
                                                            var fee = comp.CalculateParamedicPercentDiscount(AppSession.Parameter.IsTarifCompPhysicianDiscountMaxByShare, entity.RegistrationNo, detail.ItemID, discountRule,
                                                                AppSession.UserLogin.UserID, entity.ClassID, entity.ToServiceUnitID);
                                                            comp.AutoProcessCalculation = 0 - comp.DiscountAmount;

                                                            //comp.DiscountAmount = (amountValue / 100) * compPrice;
                                                            //comp.AutoProcessCalculation = 0 - (amountValue / 100) * compPrice;
                                                        }
                                                        else
                                                        {
                                                            //if (!isDiscount)
                                                            //{
                                                            //    if (discount == 0)
                                                            //    {
                                                            if (detail.Price > compPrice) amountValue = ((compPrice + compCitoAmt) / (detail.Price + (detail.CitoAmount / Math.Abs(detail.ChargeQuantity ?? 0)))) * amountValue;

                                                            if (compPrice + compCitoAmt >= amountValue)
                                                            {
                                                                var discountRule = amountValue;
                                                                var fee = comp.CalculateParamedicPercentDiscount(AppSession.Parameter.IsTarifCompPhysicianDiscountMaxByShare, entity.RegistrationNo, detail.ItemID,
                                                                    discountRule, AppSession.UserLogin.UserID, entity.ClassID, entity.ToServiceUnitID);
                                                                comp.AutoProcessCalculation = 0 - comp.DiscountAmount;

                                                                //comp.DiscountAmount = amountValue;
                                                                //comp.AutoProcessCalculation = 0 - amountValue;
                                                                //isDiscount = true;
                                                            }
                                                            else
                                                            {
                                                                var discountRule = compPrice + compCitoAmt;
                                                                var fee = comp.CalculateParamedicPercentDiscount(AppSession.Parameter.IsTarifCompPhysicianDiscountMaxByShare, entity.RegistrationNo, detail.ItemID,
                                                                    discountRule, AppSession.UserLogin.UserID, entity.ClassID, entity.ToServiceUnitID);
                                                                comp.AutoProcessCalculation = 0 - comp.DiscountAmount;

                                                                //comp.DiscountAmount = compPrice;
                                                                //comp.AutoProcessCalculation = 0 - compPrice;
                                                                //discount = amountValue - compPrice;
                                                            }
                                                            //    }
                                                            //    else
                                                            //    {
                                                            //        if (compPrice >= discount)
                                                            //        {
                                                            //            comp.DiscountAmount = discount;
                                                            //            comp.AutoProcessCalculation = 0 - discount;
                                                            //            isDiscount = true;
                                                            //        }
                                                            //        else
                                                            //        {
                                                            //            comp.DiscountAmount = compPrice;
                                                            //            comp.AutoProcessCalculation = 0 - compPrice;
                                                            //            discount -= compPrice;
                                                            //        }
                                                            //    }
                                                            //}
                                                        }
                                                    }
                                                    else if (rowCovered["SRGuarantorRuleType"].ToString().Equals(AppSession.Parameter.GuarantorRuleTypeMargin))
                                                    {
                                                        decimal basicCitoAmount = detail.BasicCitoAmount ?? 0;

                                                        if ((bool)rowCovered["IsValueInPercent"])
                                                        {
                                                            comp.AutoProcessCalculation = (amountValue / 100) * (comp.Price + comp.CitoAmount);
                                                            comp.Price += (amountValue / 100) * comp.Price;
                                                            comp.CitoAmount += (amountValue / 100) * comp.CitoAmount;

                                                            var discountRule = 0;
                                                            var fee = comp.CalculateParamedicPercentDiscount(AppSession.Parameter.IsTarifCompPhysicianDiscountMaxByShare, entity.RegistrationNo, detail.ItemID,
                                                                discountRule, AppSession.UserLogin.UserID, entity.ClassID, entity.ToServiceUnitID);
                                                            comp.AutoProcessCalculation = comp.AutoProcessCalculation - comp.DiscountAmount;
                                                        }
                                                        else
                                                        {
                                                            if (!isMargin)
                                                            {
                                                                comp.Price += amountValue;
                                                                comp.CitoAmount = comp.Price * basicCitoAmount / 100;
                                                                comp.AutoProcessCalculation = amountValue + comp.CitoAmount;
                                                                isMargin = true;

                                                                var discountRule = 0;
                                                                var fee = comp.CalculateParamedicPercentDiscount(AppSession.Parameter.IsTarifCompPhysicianDiscountMaxByShare, entity.RegistrationNo, detail.ItemID,
                                                                    discountRule, AppSession.UserLogin.UserID, entity.ClassID, entity.ToServiceUnitID);
                                                                comp.AutoProcessCalculation = amountValue + comp.CitoAmount - comp.DiscountAmount;
                                                            }
                                                        }
                                                    }
                                                    comp.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                                    comp.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                                                }
                                            }

                                            //TransChargesItems
                                            detail.IsApprove = isApproval;
                                            detail.IsBillProceed = isApproval;

                                            //jangan di remark, kalo mo remark, tanya deby dulu
                                            if (new string[] { "ds", "jo" }.Contains(QueryString_type))
                                            {
                                                detail.IsOrderRealization = true;
                                                detail.IsPaymentConfirmed = false;
                                                detail.RealizationDateTime = (new DateTime()).NowAtSqlServer();
                                                detail.RealizationUserID = AppSession.UserLogin.UserID;
                                                //detail.IsSendToLIS = entity.ToServiceUnitID == AppSession.Parameter.ServiceUnitLaboratoryID;
                                            }
                                            //end jangan di remark
                                            //if (TransChargesItemComps.Count > 0)
                                            if (isTransChargesItemComps)
                                            {
                                                detail.AutoProcessCalculation = TransChargesItemComps.Where(t => t.TransactionNo == detail.TransactionNo && t.SequenceNo == detail.SequenceNo)
                                                                                                     .Sum(t => t.AutoProcessCalculation);
                                                if (detail.AutoProcessCalculation < 0)
                                                {
                                                    //detail.DiscountAmount += detail.ChargeQuantity * Math.Abs(detail.AutoProcessCalculation ?? 0);
                                                    detail.DiscountAmount = detail.ChargeQuantity * Math.Abs(detail.AutoProcessCalculation ?? 0);

                                                    if (detail.DiscountAmount > (detail.Price * Math.Abs(detail.ChargeQuantity ?? 0)) + detail.CitoAmount)
                                                    {
                                                        detail.DiscountAmount = (detail.Price * Math.Abs(detail.ChargeQuantity ?? 0)) + detail.CitoAmount;
                                                        detail.AutoProcessCalculation = 0 - (detail.Price + (detail.CitoAmount / Math.Abs(detail.ChargeQuantity ?? 0)));
                                                    }
                                                }
                                                else if (detail.AutoProcessCalculation > 0) detail.Price += detail.AutoProcessCalculation;
                                            }
                                            else
                                            {
                                                if (rowCovered != null)
                                                {
                                                    if (rowCovered["SRGuarantorRuleType"].ToString().Equals(AppSession.Parameter.GuarantorRuleTypeDiscount))
                                                    {
                                                        var basicPrice = (decimal?)rowCovered["BasicPrice"];
                                                        var coveragePrice = (decimal?)rowCovered["CoveragePrice"];
                                                        if (!string.IsNullOrEmpty(detail.ItemConditionRuleID))
                                                        {
                                                            basicPrice = Helper.Tariff.GetItemConditionRuleTariff(basicPrice ?? 0, detail.ItemConditionRuleID, detail.TariffDate ?? tariffDate);
                                                            coveragePrice = Helper.Tariff.GetItemConditionRuleTariff(coveragePrice ?? 0, detail.ItemConditionRuleID, detail.TariffDate ?? tariffDate);
                                                        }

                                                        var detailPrice = detail.Price ?? 0;
                                                        if (basicPrice > coveragePrice)
                                                        {
                                                            ItemTariff tariff = (Helper.Tariff.GetItemTariff(detail.TariffDate ?? tariffDate, grr.SRTariffType, reg.CoverageClassID, reg.CoverageClassID, detail.ItemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
                                                                     Helper.Tariff.GetItemTariff(detail.TariffDate ?? tariffDate, grr.SRTariffType, AppSession.Parameter.DefaultTariffClass, reg.CoverageClassID, detail.ItemID, reg.GuarantorID, false, reg.SRRegistrationType)) ??
                                                                    (Helper.Tariff.GetItemTariff(detail.TariffDate ?? tariffDate, AppSession.Parameter.DefaultTariffType, reg.CoverageClassID, reg.CoverageClassID, detail.ItemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
                                                                     Helper.Tariff.GetItemTariff(detail.TariffDate ?? tariffDate, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, reg.CoverageClassID, detail.ItemID, reg.GuarantorID, false, reg.SRRegistrationType));
                                                            if (tariff != null)
                                                            {
                                                                //detailPrice = tariff.Price ?? 0;
                                                                detailPrice = Helper.Tariff.GetItemConditionRuleTariff(tariff.Price ?? 0, detail.ItemConditionRuleID, detail.TariffDate ?? tariffDate);
                                                            }
                                                        }

                                                        if ((bool)rowCovered["IsValueInPercent"])
                                                        {
                                                            detail.DiscountAmount = (detail.ChargeQuantity ?? 0) * (((decimal)rowCovered["AmountValue"] / 100) * detailPrice);
                                                            detail.AutoProcessCalculation = 0 - (((decimal)rowCovered["AmountValue"] / 100) * detailPrice);
                                                        }
                                                        else
                                                        {
                                                            detail.DiscountAmount = (detail.ChargeQuantity ?? 0) * (decimal)rowCovered["AmountValue"];
                                                            detail.AutoProcessCalculation = 0 - (decimal)rowCovered["AmountValue"];
                                                        }

                                                        if (detail.DiscountAmount > (detailPrice * detail.ChargeQuantity))
                                                            detail.DiscountAmount = detailPrice * detail.ChargeQuantity;
                                                    }
                                                    else if (rowCovered["SRGuarantorRuleType"].ToString().Equals(AppSession.Parameter.GuarantorRuleTypeMargin))
                                                    {
                                                        if ((bool)rowCovered["IsValueInPercent"])
                                                        {
                                                            detail.AutoProcessCalculation = ((decimal)rowCovered["AmountValue"] / 100) * detail.Price;
                                                            detail.Price += ((decimal)rowCovered["AmountValue"] / 100) * detail.Price;
                                                        }
                                                        else
                                                        {
                                                            detail.Price += (decimal)rowCovered["AmountValue"];
                                                            detail.AutoProcessCalculation = (decimal)rowCovered["AmountValue"];
                                                        }
                                                    }
                                                }
                                            }

                                            if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSCH" && string.IsNullOrEmpty(detail.FilmNo))
                                            {
                                                item = new Item();
                                                item.LoadByPrimaryKey(detail.ItemID);
                                                if (item.Notes.Length > 0 && item.SRItemType != ItemType.Medical && item.SRItemType != ItemType.NonMedical && item.SRItemType != ItemType.Kitchen)
                                                {
                                                    amplopFilmAutoNumber = Helper.GetNewAutoNumber(TransactionDate.Date, AppEnum.AutoNumber.AmplopFilmNo,
                                                        item.Notes.Length >= 3 ? item.Notes.Substring(0, 3).ToUpper() : item.Notes.ToUpper(), AppSession.UserLogin.UserID);

                                                    var filmNo = amplopFilmAutoNumber.LastCompleteNumber;
                                                    amplopFilmAutoNumber.Save();

                                                    detail.FilmNo = filmNo;
                                                }
                                            }
                                            else if (AppSession.Parameter.HealthcareInitialAppsVersion == "YBRSGKP" && AppSession.Parameter.HealthcareInitial == "RSI")
                                            {
                                                //if (string.IsNullOrEmpty(detail.FilmNo))
                                                {
                                                    var i = new Item();
                                                    i.LoadByPrimaryKey(detail.ItemID);
                                                    if (i.SRItemType == ItemType.Radiology)
                                                    {
                                                        if (i.ItemIDExternal == "CR")
                                                        {
                                                            amplopFilmAutoNumber = Helper.GetNewAutoNumber(DateTime.Now.Date, AppEnum.AutoNumber.CRRadiologyFilmNo);
                                                            detail.FilmNo = amplopFilmAutoNumber.LastCompleteNumber;
                                                            amplopFilmAutoNumber.Save();
                                                        }
                                                        else if (i.ItemIDExternal == "CT")
                                                        {
                                                            amplopFilmAutoNumber = Helper.GetNewAutoNumber(DateTime.Now.Date, AppEnum.AutoNumber.CTRadiologyFilmNo);
                                                            detail.FilmNo = amplopFilmAutoNumber.LastCompleteNumber;
                                                            amplopFilmAutoNumber.Save();
                                                        }
                                                    }
                                                }
                                            }

                                            detail.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                            detail.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                                            //post
                                            decimal? total = ((detail.ChargeQuantity * detail.Price) - detail.DiscountAmount) + detail.CitoAmount;
                                            decimal? qty = detail.ChargeQuantity;

                                            //var calc = new Helper.CostCalculation(grrID, detail.ItemID, total ?? 0, tblCovered, qty ?? 0,
                                            //                                      detail.IsCito ?? false,
                                            //                                      detail.IsCitoInPercent ?? false,
                                            //                                      detail.BasicCitoAmount ?? 0, detail.Price ?? 0,
                                            //                                      entity.IsRoomIn ?? false, detail.IsItemRoom ?? false,
                                            //                                      entity.TariffDiscountForRoomIn ?? 0, detail.DiscountAmount ?? 0, false,
                                            //                                      detail.ItemConditionRuleID, detail.TariffDate ?? tariffDate, detail.IsVariable ?? false);

                                            var calc = new Helper.CostCalculation(grrID, itemId, total ?? 0, tblCovered, qty ?? 0,
                                                                                  detail.IsCito ?? false,
                                                                                  detail.IsCitoInPercent ?? false,
                                                                                  detail.BasicCitoAmount ?? 0, detail.Price ?? 0,
                                                                                  entity.IsRoomIn ?? false, detail.IsItemRoom ?? false,
                                                                                  entity.TariffDiscountForRoomIn ?? 0, detail.DiscountAmount ?? 0, false,
                                                                                  detail.ItemConditionRuleID, detail.TariffDate ?? tariffDate, detail.IsVariable ?? false);

                                            var package = new GuarantorSurgicalPackageCoveredItem();
                                            if (package.LoadByPrimaryKey(grrID, entity.SurgicalPackageID, detail.ItemID))
                                            {
                                                if (calc.PatientAmount + calc.GuarantorAmount <= package.CoveredAmount)
                                                {
                                                    calc.GuarantorAmount = calc.PatientAmount + calc.GuarantorAmount;
                                                    calc.PatientAmount = 0;
                                                }
                                                else
                                                {
                                                    calc.PatientAmount = calc.PatientAmount + calc.GuarantorAmount - package.CoveredAmount ?? 0;
                                                    calc.GuarantorAmount = package.CoveredAmount ?? 0;
                                                }
                                            }

                                            //CostCalculation

                                            // 20160912 terjadi duplikasi CostCalculation padahal transaksinya (cth RSMP: SU161205-0192) belum approve
                                            var costs = CostCalculations.Where(cc => cc.TransactionNo == detail.TransactionNo && cc.SequenceNo == detail.SequenceNo);
                                            CostCalculation cost;
                                            if (costs.Count() == 1)
                                            {
                                                cost = costs.First();
                                                new BasePage().LogError(new Exception(string.Format("Dev Warning: duplication of cost calculation on TransactionNo {0}, SequenceNo {1}, ItemID {2}. Potential of duplicate stock taking!!",
                                                    cost.TransactionNo, cost.SequenceNo, cost.ItemID)));
                                            }
                                            else cost = CostCalculations.AddNew();

                                            cost.RegistrationNo = entity.RegistrationNo;
                                            cost.TransactionNo = detail.TransactionNo;
                                            cost.SequenceNo = detail.SequenceNo;
                                            cost.ItemID = detail.ItemID;

                                            //start here
                                            decimal? totaltrans = calc.GuarantorAmount + calc.PatientAmount + (detail.DiscountAmount ?? 0);
                                            decimal? totaldisc = detail.DiscountAmount ?? 0;

                                            if (reg.SRBussinesMethod == AppSession.Parameter.BusinessMethodFlavon)
                                            {
                                                if (totaldisc >= totaltrans)
                                                {
                                                    cost.GuarantorAmount = 0;
                                                    cost.PatientAmount = 0;
                                                }
                                                else
                                                {
                                                    cost.GuarantorAmount = totaltrans - totaldisc;
                                                    cost.PatientAmount = 0;
                                                }
                                                cost.DiscountAmount = totaldisc;
                                            }
                                            else
                                            {
                                                if (calc.GuarantorAmount > 0)
                                                {
                                                    cost.DiscountAmount = totaldisc > (calc.GuarantorAmount + detail.DiscountAmount)
                                                                               ? (calc.GuarantorAmount + detail.DiscountAmount)
                                                                               : totaldisc;

                                                    cost.GuarantorAmount = totaldisc > (calc.GuarantorAmount + detail.DiscountAmount)
                                                                               ? 0
                                                                               : (calc.GuarantorAmount + detail.DiscountAmount) - totaldisc;
                                                    cost.PatientAmount = calc.PatientAmount;

                                                }
                                                else
                                                {
                                                    cost.DiscountAmount = totaldisc > calc.PatientAmount + detail.DiscountAmount
                                                                              ? calc.PatientAmount + detail.DiscountAmount
                                                                              : totaldisc;

                                                    cost.PatientAmount = totaldisc > calc.PatientAmount + detail.DiscountAmount
                                                                             ? 0
                                                                             : calc.PatientAmount + detail.DiscountAmount - totaldisc;
                                                    cost.GuarantorAmount = calc.GuarantorAmount;
                                                }

                                                if (totaldisc > cost.DiscountAmount)
                                                {
                                                    //hitung ulang diskon di TransChargesItem & TransChargesItemComp
                                                    if (isTransChargesItemComps)
                                                    {
                                                        var compColl = TransChargesItemComps.Where(
                                                            t =>
                                                            t.TransactionNo == detail.TransactionNo &&
                                                            t.SequenceNo == detail.SequenceNo)
                                                            .OrderBy(t => t.TariffComponentID);
                                                        var i = compColl.Count();

                                                        foreach (var compEntity in compColl)
                                                        {
                                                            compEntity.DiscountAmount = i == 1
                                                                                       ? (cost.DiscountAmount / Math.Abs(detail.ChargeQuantity ?? 0))
                                                                                       : (compEntity.Price + compEntity.CitoAmount) * (cost.DiscountAmount / detail.DiscountAmount);

                                                            var fee = compEntity.CalculateParamedicPercentDiscount(
                                                                AppSession.Parameter.IsTarifCompPhysicianDiscountMaxByShare,
                                                                cost.RegistrationNo, detail.ItemID, (compEntity.DiscountAmount ?? 0),
                                                                AppSession.UserLogin.UserID, entity.ClassID, entity.ToServiceUnitID);

                                                        }

                                                        TransChargesItemComps.Save();
                                                    }

                                                    detail.DiscountAmount = cost.DiscountAmount;
                                                    detail.Save();
                                                }
                                            }
                                            //end

                                            cost.IsPackage = detail.IsPackage;
                                            cost.ParentNo = detail.ParentNo;
                                            cost.ParamedicAmount = detail.ChargeQuantity * TransChargesItemComps.Where(comp => comp.TransactionNo == detail.TransactionNo && comp.SequenceNo == detail.SequenceNo &&
                                                                                                                               !string.IsNullOrEmpty(comp.ParamedicID))
                                                                                                                .Sum(comp => comp.Price - comp.DiscountAmount + comp.CitoAmount);
                                            cost.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                                            cost.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                        }
                                    }
                                }
                            }
                        }
                        if (!isApproval) CostCalculations.MarkAllAsDeleted();
                    }

                    if (QueryString_type == "jo" || QueryString_type == "ds")
                    {
                        if (string.IsNullOrEmpty(pat.DiagnosticNo) && isApproval && AppSession.Parameter.IsRadiologyNoAutoCreate)
                        {
                            pat.DiagnosticNo = (new DateTime()).NowAtSqlServer().ToString(AppSession.Parameter.RadiologyNoFormat);
                            pat.Save();
                        }

                        if (QueryString_type == "jo")
                        {
                            if (AppSession.Parameter.IsUsingHisInterop)
                            {
                                if (entity.ToServiceUnitID == AppSession.Parameter.ServiceUnitLaboratoryID)
                                {
                                    var patient = new Patient();
                                    patient.LoadByPrimaryKey(reg.PatientID);

                                    if (!AppSession.Parameter.IsUsingHisInteropWithMultipleConnection)
                                    {
                                        #region with single connection (default)
                                        switch (AppSession.Parameter.HisInteropConfigName)
                                        {
                                            case AppConstant.HIS_INTEROP.WYNAKOM_LIS_INTEROP_CONNECTION_NAME:
                                                if (entity.ToServiceUnitID == AppSession.Parameter.ServiceUnitLaboratoryID)
                                                {
                                                    var demographics = new BusinessObject.Interop.Wynakom.Demographics();
                                                    demographics.es.Connection.Name = AppConstant.HIS_INTEROP.WYNAKOM_LIS_INTEROP_CONNECTION_NAME;

                                                    if (!demographics.LoadByPrimaryKey(string.IsNullOrEmpty(patient.MedicalNo) ? patient.PatientID : patient.MedicalNo)) demographics = new BusinessObject.Interop.Wynakom.Demographics();
                                                    demographics.PatientId = string.IsNullOrEmpty(patient.MedicalNo) ? patient.PatientID : patient.MedicalNo;
                                                    demographics.GenderId = patient.Sex;
                                                    demographics.DateOfBirth = patient.DateOfBirth;

                                                    var std = new AppStandardReferenceItem();
                                                    if (std.LoadByPrimaryKey(AppEnum.StandardReference.Salutation.ToString(), patient.SRSalutation))
                                                        demographics.PatientName = string.Format("{0} {1}", std.ItemName, patient.PatientName);
                                                    else
                                                        demographics.PatientName = string.Format("{0}", patient.PatientName);//string.Format("{0} {1}", string.Empty, patient.PatientName);

                                                    demographics.PatientAddress = patient.Address;
                                                    demographics.CityName = patient.City;
                                                    demographics.PhoneNumber = patient.PhoneNo;
                                                    demographics.FaxNumber = patient.FaxNo;
                                                    demographics.MobileNumber = patient.MobilePhoneNo;
                                                    demographics.Email = patient.Email;
                                                    demographics.KtpNumber = patient.Ssn;

                                                    var registration = new BusinessObject.Interop.Wynakom.Registration();
                                                    registration.es.Connection.Name = AppConstant.HIS_INTEROP.WYNAKOM_LIS_INTEROP_CONNECTION_NAME;

                                                    registration.PatientId = demographics.PatientId;
                                                    registration.VisitNumber = reg.RegistrationNo;
                                                    registration.OrderNumber = entity.TransactionNo;
                                                    registration.OrderDateTime = entity.ExecutionDate;
                                                    registration.DiagnoseName = "";
                                                    registration.Cito = TransChargesItems.Any(t => t.IsCito ?? false);
                                                    registration.ServiceUnitID = unit.ServiceUnitID;
                                                    registration.ServiceUnitName = unit.ServiceUnitName;
                                                    registration.GuarantorID = grr.GuarantorID;
                                                    registration.GuarantorName = grr.GuarantorName;

                                                    var pmedic = new Paramedic();
                                                    pmedic.LoadByPrimaryKey(reg.ParamedicID);
                                                    registration.DoctorID = pmedic.ParamedicID;
                                                    registration.DoctorName = pmedic.ParamedicName;

                                                    var cls = new Class();
                                                    cls.LoadByPrimaryKey(entity.ClassID);
                                                    registration.ClassID = cls.ClassID;
                                                    registration.ClassName = cls.ClassName;
                                                    registration.AgreementID = cls.ClassID;
                                                    registration.AgreementName = cls.ClassName;

                                                    registration.WardID = unit.ServiceUnitID;
                                                    registration.WardName = unit.ServiceUnitName;

                                                    var room = new ServiceRoom();
                                                    room.LoadByPrimaryKey(entity.RoomID);
                                                    registration.RoomID = room.RoomID;
                                                    registration.RoomName = room.RoomName;

                                                    registration.BedID = entity.BedID;
                                                    registration.BedName = entity.BedID;
                                                    registration.RegUserName = AppSession.UserLogin.UserName;
                                                    registration.Notes = reg.Notes;

                                                    if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSJKT")
                                                    {
                                                        //registration.PhysicianSenders = reg.PhysicianSenders;
                                                    }

                                                    var ordered_item = new BusinessObject.Interop.Wynakom.OrderedItems();
                                                    ordered_item.es.Connection.Name = AppConstant.HIS_INTEROP.WYNAKOM_LIS_INTEROP_CONNECTION_NAME;
                                                    ordered_item.OrderNumber = registration.OrderNumber;
                                                    ordered_item.OrderItemDate = registration.OrderDateTime;

                                                    bool isInsert = false;
                                                    string maxSequence = string.Empty;
                                                    foreach (var entity2 in TransChargesItems.Where(t => t.IsOrderRealization ?? false && t.IsVoid == false))
                                                    {
                                                        var item = new Item();
                                                        item.LoadByPrimaryKey(entity2.ItemID);

                                                        if (!string.IsNullOrEmpty(item.ItemIDExternal))
                                                        {
                                                            if (AppSession.Parameter.HealthcareInitialAppsVersion == "YBRSGKP")
                                                            {
                                                                if (AppSession.Parameter.HealthcareInitial == "RSI")
                                                                {
                                                                    var pa = new BusinessObject.Interop.ROCHE.PocAdt();
                                                                    pa.es.Connection.Name = AppConstant.HIS_INTEROP.ROCHE_LIS_INTEROP_CONNECTION_NAME;
                                                                    pa.Action = "ADD";
                                                                    pa.Pid = patient.MedicalNo;
                                                                    pa.Dob = patient.DateOfBirth;
                                                                    pa.Sex = patient.Sex;
                                                                    pa.VisitNum = entity.TransactionNo + $"^{entity2.SequenceNo}";

                                                                    var pc = string.Empty;
                                                                    if (reg.SRRegistrationType == AppConstant.RegistrationType.InPatient) pc = "1";
                                                                    else if (reg.SRRegistrationType == AppConstant.RegistrationType.OutPatient) pc = "2";
                                                                    else if (reg.SRRegistrationType == AppConstant.RegistrationType.EmergencyPatient) pc = "3";
                                                                    else pc = "2";
                                                                    pa.PatientClass = pc;

                                                                    var fUnit = new ServiceUnit();
                                                                    fUnit.LoadByPrimaryKey(entity.FromServiceUnitID);

                                                                    pa.LocationCode = fUnit.ServiceUnitID;
                                                                    pa.LocationName = fUnit.ServiceUnitName;
                                                                    pa.DoctorCode = pmedic.ParamedicID;
                                                                    pa.DoctorName = pmedic.ParamedicName;
                                                                    pa.AdmitDt = entity.ExecutionDate;
                                                                    pa.PatientName = patient.PatientName;

                                                                    var fRoom = new ServiceRoom();
                                                                    fRoom.LoadByPrimaryKey(reg.RoomID);

                                                                    pa.RoomCode = fRoom.RoomID;
                                                                    pa.RoomName = fRoom.RoomName;

                                                                    pa.Save();
                                                                }
                                                            }
                                                        }

                                                        if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSPP")
                                                        {
                                                            //ordered_item.OrderItemID += (string.IsNullOrEmpty(item.ItemIDExternal) ? item.ItemID : item.ItemIDExternal) + "~";
                                                            ordered_item.OrderItemID += item.ItemID + "~";
                                                            ordered_item.OrderItemName += item.ItemName + "~";
                                                            isInsert = true;
                                                        }
                                                        else
                                                        {
                                                            ordered_item.OrderItemID += item.ItemID + "~";
                                                            ordered_item.OrderItemName += item.ItemName + "~";
                                                            isInsert = true;
                                                        }

                                                        maxSequence = entity2.SequenceNo;
                                                    }

                                                    if (AppSession.Parameter.HealthcareInitialAppsVersion == "YBRSGKP")
                                                    {
                                                        if (AppSession.Parameter.HealthcareInitial == "RSI")
                                                        {
                                                            registration.OrderNumber += $"^{maxSequence}";
                                                            ordered_item.OrderNumber += $"^{maxSequence}";
                                                        }
                                                    }

                                                    if (isInsert)
                                                    {
                                                        if (!string.IsNullOrEmpty(ordered_item.OrderItemID) && !string.IsNullOrEmpty(ordered_item.OrderItemName))
                                                        {
                                                            ordered_item.OrderItemID = ordered_item.OrderItemID.Remove(ordered_item.OrderItemID.Length - 1);
                                                            ordered_item.OrderItemName = ordered_item.OrderItemName.Remove(ordered_item.OrderItemName.Length - 1);
                                                        }

                                                        demographics.es.Connection.Name = AppConstant.HIS_INTEROP.WYNAKOM_LIS_INTEROP_CONNECTION_NAME;
                                                        demographics.Save();

                                                        registration.es.Connection.Name = AppConstant.HIS_INTEROP.WYNAKOM_LIS_INTEROP_CONNECTION_NAME;
                                                        registration.Save();

                                                        ordered_item.es.Connection.Name = AppConstant.HIS_INTEROP.WYNAKOM_LIS_INTEROP_CONNECTION_NAME;
                                                        ordered_item.Save();
                                                    }
                                                }
                                                break;
                                        }
                                        #endregion
                                    }
                                    else
                                    {
                                        #region with multiple connection
                                        var listItem = TransChargesItems.Where(t => t.IsOrderRealization ?? false && t.IsVoid == false).Select(q => new
                                        {
                                            ItemId = q.ItemID
                                        });

                                        if (listItem.Any())
                                        {
                                            var itemBridgings = new ItemBridgingCollection();
                                            var ibq = new ItemBridgingQuery("a");
                                            var connq = new AppStandardReferenceItemQuery("b");
                                            ibq.Select(ibq, connq.ReferenceID.As("refToAppStandardReferenceItem_BridgingTypeReferenceID"));
                                            ibq.InnerJoin(connq).On(connq.StandardReferenceID == AppEnum.StandardReference.BridgingType.ToString() && connq.ItemID == ibq.SRBridgingType);
                                            ibq.Where(ibq.ItemID.In(listItem.Select(l => l.ItemId)), ibq.IsActive == true,
                                                connq.ReferenceID.In(AppConstant.HIS_INTEROP.WYNAKOM_LIS_INTEROP_CONNECTION_NAME, AppConstant.HIS_INTEROP.ROCHE_LIS_INTEROP_CONNECTION_NAME));
                                            ibq.OrderBy(connq.ReferenceID.Ascending, ibq.ItemID.Ascending);
                                            itemBridgings.Load(ibq);

                                            var pmedic = new Paramedic();
                                            pmedic.LoadByPrimaryKey(reg.ParamedicID);

                                            foreach (var group in (from g in itemBridgings
                                                                   group g by new
                                                                   {
                                                                       g.ConnectionName
                                                                   }
                                                       into grp
                                                                   orderby grp.Key.ConnectionName
                                                                   select new
                                                                   {
                                                                       ConnectionName = grp.Key.ConnectionName
                                                                   }))
                                            {
                                                switch (group.ConnectionName)
                                                {
                                                    case AppConstant.HIS_INTEROP.WYNAKOM_LIS_INTEROP_CONNECTION_NAME:
                                                        if (entity.ToServiceUnitID == AppSession.Parameter.ServiceUnitLaboratoryID)
                                                        {
                                                            var demographics = new BusinessObject.Interop.Wynakom.Demographics();
                                                            demographics.es.Connection.Name = AppConstant.HIS_INTEROP.WYNAKOM_LIS_INTEROP_CONNECTION_NAME;

                                                            if (!demographics.LoadByPrimaryKey(string.IsNullOrEmpty(patient.MedicalNo) ? patient.PatientID : patient.MedicalNo)) demographics = new BusinessObject.Interop.Wynakom.Demographics();
                                                            demographics.PatientId = string.IsNullOrEmpty(patient.MedicalNo) ? patient.PatientID : patient.MedicalNo;
                                                            demographics.GenderId = patient.Sex;
                                                            demographics.DateOfBirth = patient.DateOfBirth;

                                                            var std = new AppStandardReferenceItem();
                                                            if (std.LoadByPrimaryKey(AppEnum.StandardReference.Salutation.ToString(), patient.SRSalutation))
                                                                demographics.PatientName = string.Format("{0} {1}", std.ItemName, patient.PatientName);
                                                            else
                                                                demographics.PatientName = string.Format("{0}", patient.PatientName);//string.Format("{0} {1}", string.Empty, patient.PatientName);

                                                            demographics.PatientAddress = patient.Address;
                                                            demographics.CityName = patient.City;
                                                            demographics.PhoneNumber = patient.PhoneNo;
                                                            demographics.FaxNumber = patient.FaxNo;
                                                            demographics.MobileNumber = patient.MobilePhoneNo;
                                                            demographics.Email = patient.Email;
                                                            demographics.KtpNumber = patient.Ssn;

                                                            var registration = new BusinessObject.Interop.Wynakom.Registration();
                                                            registration.es.Connection.Name = AppConstant.HIS_INTEROP.WYNAKOM_LIS_INTEROP_CONNECTION_NAME;

                                                            registration.PatientId = demographics.PatientId;
                                                            registration.VisitNumber = reg.RegistrationNo;
                                                            registration.OrderNumber = entity.TransactionNo;
                                                            registration.OrderDateTime = entity.ExecutionDate;
                                                            registration.DiagnoseName = "";
                                                            registration.Cito = TransChargesItems.Any(t => t.IsCito ?? false);
                                                            registration.ServiceUnitID = unit.ServiceUnitID;
                                                            registration.ServiceUnitName = unit.ServiceUnitName;
                                                            registration.GuarantorID = grr.GuarantorID;
                                                            registration.GuarantorName = grr.GuarantorName;

                                                            registration.DoctorID = pmedic.ParamedicID;
                                                            registration.DoctorName = pmedic.ParamedicName;

                                                            var cls = new Class();
                                                            cls.LoadByPrimaryKey(entity.ClassID);
                                                            registration.ClassID = cls.ClassID;
                                                            registration.ClassName = cls.ClassName;
                                                            registration.AgreementID = cls.ClassID;
                                                            registration.AgreementName = cls.ClassName;

                                                            registration.WardID = unit.ServiceUnitID;
                                                            registration.WardName = unit.ServiceUnitName;

                                                            var room = new ServiceRoom();
                                                            room.LoadByPrimaryKey(entity.RoomID);
                                                            registration.RoomID = room.RoomID;
                                                            registration.RoomName = room.RoomName;

                                                            registration.BedID = entity.BedID;
                                                            registration.BedName = entity.BedID;
                                                            registration.RegUserName = AppSession.UserLogin.UserName;
                                                            registration.Notes = reg.Notes;

                                                            var ordered_item = new BusinessObject.Interop.Wynakom.OrderedItems();
                                                            ordered_item.es.Connection.Name = AppConstant.HIS_INTEROP.WYNAKOM_LIS_INTEROP_CONNECTION_NAME;
                                                            ordered_item.OrderNumber = registration.OrderNumber;
                                                            ordered_item.OrderItemDate = registration.OrderDateTime;

                                                            bool isInsert = false;
                                                            foreach (var i in itemBridgings.Where(i => i.ConnectionName == group.ConnectionName))
                                                            {
                                                                var item = new Item();
                                                                item.LoadByPrimaryKey(i.ItemID);

                                                                //ordered_item.OrderItemID += (string.IsNullOrEmpty(item.ItemIDExternal) ? item.ItemID : item.ItemIDExternal) + "~";
                                                                ordered_item.OrderItemID += item.ItemID + "~";
                                                                ordered_item.OrderItemName += item.ItemName + "~";
                                                                isInsert = true;
                                                            }

                                                            if (isInsert)
                                                            {
                                                                if (!string.IsNullOrEmpty(ordered_item.OrderItemID) && !string.IsNullOrEmpty(ordered_item.OrderItemName))
                                                                {
                                                                    ordered_item.OrderItemID = ordered_item.OrderItemID.Remove(ordered_item.OrderItemID.Length - 1);
                                                                    ordered_item.OrderItemName = ordered_item.OrderItemName.Remove(ordered_item.OrderItemName.Length - 1);
                                                                }

                                                                demographics.es.Connection.Name = AppConstant.HIS_INTEROP.WYNAKOM_LIS_INTEROP_CONNECTION_NAME;
                                                                demographics.Save();

                                                                registration.es.Connection.Name = AppConstant.HIS_INTEROP.WYNAKOM_LIS_INTEROP_CONNECTION_NAME;
                                                                registration.Save();

                                                                ordered_item.es.Connection.Name = AppConstant.HIS_INTEROP.WYNAKOM_LIS_INTEROP_CONNECTION_NAME;
                                                                ordered_item.Save();
                                                            }
                                                        }
                                                        break;
                                                    case AppConstant.HIS_INTEROP.ROCHE_LIS_INTEROP_CONNECTION_NAME:
                                                        if (entity.ToServiceUnitID == AppSession.Parameter.ServiceUnitLaboratoryID)
                                                        {
                                                            foreach (var i in itemBridgings.Where(i => i.ConnectionName == group.ConnectionName))
                                                            {
                                                                var item = new Item();
                                                                item.LoadByPrimaryKey(i.ItemID);

                                                                if (!string.IsNullOrEmpty(i.ItemIdExternal))
                                                                {
                                                                    var pa = new BusinessObject.Interop.ROCHE.PocAdt();
                                                                    pa.es.Connection.Name = AppConstant.HIS_INTEROP.ROCHE_LIS_INTEROP_CONNECTION_NAME;
                                                                    pa.Action = "ADD";
                                                                    pa.Pid = patient.MedicalNo;
                                                                    pa.Dob = patient.DateOfBirth;
                                                                    pa.Sex = patient.Sex;
                                                                    pa.VisitNum = entity.TransactionNo;

                                                                    var pc = string.Empty;
                                                                    if (reg.SRRegistrationType == AppConstant.RegistrationType.InPatient) pc = "1";
                                                                    else if (reg.SRRegistrationType == AppConstant.RegistrationType.OutPatient) pc = "2";
                                                                    else if (reg.SRRegistrationType == AppConstant.RegistrationType.EmergencyPatient) pc = "3";
                                                                    else pc = "2";
                                                                    pa.PatientClass = pc;

                                                                    var fUnit = new ServiceUnit();
                                                                    fUnit.LoadByPrimaryKey(entity.FromServiceUnitID);

                                                                    pa.LocationCode = fUnit.ServiceUnitID;
                                                                    pa.LocationName = fUnit.ServiceUnitName;
                                                                    pa.DoctorCode = pmedic.ParamedicID;
                                                                    pa.DoctorName = pmedic.ParamedicName;
                                                                    pa.AdmitDt = entity.ExecutionDate;
                                                                    pa.PatientName = patient.PatientName;

                                                                    var fRoom = new ServiceRoom();
                                                                    fRoom.LoadByPrimaryKey(reg.RoomID);

                                                                    pa.RoomCode = fRoom.RoomID;
                                                                    pa.RoomName = fRoom.RoomName;

                                                                    pa.Save();
                                                                }
                                                            }
                                                        }
                                                        break;
                                                }
                                            }
                                        }
                                        #endregion
                                    }
                                }
                            }
                        }
                    }

                    entity.Save();

                    if (isApproval)
                    {
                        if (QueryString_type != "jo")
                        {
                            // stock calculation
                            // charges
                            var chargesBalances = new ItemBalanceCollection();
                            var chargesDetailBalances = new ItemBalanceDetailCollection();
                            var chargesDetailBalanceEds = new ItemBalanceDetailEdCollection();
                            var chargesMovements = new ItemMovementCollection();

                            string itemNoStock;
                            var transChargesItems = TransChargesItems;

                            ItemBalance.PrepareItemBalances(transChargesItems, entity.ToServiceUnitID, entity.LocationID, AppSession.UserLogin.UserID, isApproval, ref chargesBalances, ref chargesDetailBalances,
                                ref chargesMovements, ref chargesDetailBalanceEds, AppSession.Parameter.IsEnabledStockWithEdControl, out itemNoStock);

                            if (!string.IsNullOrEmpty(itemNoStock))
                            {
                                if (itemNoStock.Length >= 5 && itemNoStock.Substring(0, 5) == "Zero|") return "Zero cost price of item : " + itemNoStock.Replace("Zero|", "");
                                return "Insufficient balance of item : " + itemNoStock;
                            }

                            transChargesItems.Save();
                            TransChargesItemComps.Save();
                            CostCalculations.Save();

                            if (AppSession.Parameter.IsFeeCalculatedOnTransaction)
                            {
                                // extract fee
                                var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
                                feeColl.SetFeeByTCIC(TransChargesItemComps, AppSession.UserLogin.UserID);
                                feeColl.Save();
                                //feeColl.SetPaymentAndInvoicePaymentAfterSave(AppSession.UserLogin.UserID);
                                //feeColl.Save();
                            }

                            if (chargesBalances != null) chargesBalances.Save();
                            if (chargesDetailBalances != null) chargesDetailBalances.Save();
                            if (chargesDetailBalanceEds != null) chargesDetailBalanceEds.Save();
                            if (chargesMovements != null) chargesMovements.Save();

                            // consumption
                            var consumptionBalances = new ItemBalanceCollection();
                            var consumptionDetailBalances = new ItemBalanceDetailCollection();
                            var consumptionDetailBalanceEds = new ItemBalanceDetailEdCollection();
                            var consumptionMovements = new ItemMovementCollection();

                            var transChargesItemConsumptions = TransChargesItemConsumptions;

                            ItemBalance.PrepareItemBalances(transChargesItemConsumptions, entity.ToServiceUnitID, entity.LocationID, AppSession.UserLogin.UserID, ref consumptionBalances, ref consumptionDetailBalances,
                                ref consumptionMovements, ref consumptionDetailBalanceEds, AppSession.Parameter.IsEnabledStockWithEdControl, out itemNoStock);

                            if (!string.IsNullOrEmpty(itemNoStock))
                            {
                                if (itemNoStock.Length >= 5 && itemNoStock.Substring(0, 5) == "Zero|") return "Zero cost price of item : " + itemNoStock.Replace("Zero|", "");
                                return "Insufficient balance of item : " + itemNoStock;
                            }

                            if ((chargesBalances != null && chargesBalances.Count > 0) || (consumptionBalances != null && consumptionBalances.Count > 0))
                            {
                                var loc = new Location();
                                if (loc.LoadByPrimaryKey(entity.LocationID) && loc.IsHoldForTransaction == true)
                                {
                                    return "Location: " + loc.LocationName + " in Hold For Transaction status. Transaction is not allowed.";
                                }
                            }

                            transChargesItemConsumptions.Save();

                            if (consumptionBalances != null) consumptionBalances.Save();
                            if (consumptionDetailBalances != null) consumptionDetailBalances.Save();
                            if (consumptionDetailBalanceEds != null) consumptionDetailBalanceEds.Save();
                            if (consumptionMovements != null) consumptionMovements.Save();

                            unit = new ServiceUnit();
                            unit.LoadByPrimaryKey(entity.ToServiceUnitID);
                            if (QueryString_type == "jo")
                            {
                                /* Automatic Journal Testing Start */
                                if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsJournalCashBased) == "No")
                                {
                                    if (AppParameter.IsYes(AppParameter.ParameterItem.acc_IsJournalAccualNoTemporary))
                                    {
                                        JournalTransactions.AddNewPatientIncomeAccrual(BusinessObject.JournalType.Income, entity.TransactionNo, AppSession.UserLogin.UserID, 0);
                                    }
                                    else
                                    {
                                        var type = AppParameter.GetParameterValue(AppParameter.ParameterItem.RegistrationTypeForAccrualJournal).Split(',');

                                        var mb = new MergeBilling();
                                        mb.LoadByPrimaryKey(reg.RegistrationNo);
                                        if (string.IsNullOrEmpty(mb.FromRegistrationNo))
                                        {
                                            if (type.Contains(reg.SRRegistrationType))
                                            {
                                                var isClosingPeriod = PostingStatus.IsPeriodeClosed(entity.TransactionDate.Value.Date);
                                                if (isClosingPeriod)
                                                {
                                                    return "Financial statements for period: " +
                                                           string.Format("{0:MMMM-yyyy}", entity.TransactionDate.Value.Date) +
                                                           " have been closed. Please contact the authorities.";
                                                }

                                                int? journalId = JournalTransactions.AddNewIncomeJournalTemporary(entity, TransChargesItemComps, reg, unit, CostCalculations, "JO", AppSession.UserLogin.UserID, 0);
                                            }
                                        }
                                        else
                                        {
                                            var freg = new Registration();
                                            freg.LoadByPrimaryKey(mb.FromRegistrationNo);
                                            if (type.Contains(reg.SRRegistrationType))
                                            {
                                                var isClosingPeriod = PostingStatus.IsPeriodeClosed(entity.TransactionDate.Value.Date);
                                                if (isClosingPeriod)
                                                {
                                                    return "Financial statements for period: " +
                                                           string.Format("{0:MMMM-yyyy}", entity.TransactionDate.Value.Date) +
                                                           " have been closed. Please contact the authorities.";
                                                }

                                                int? journalId = JournalTransactions.AddNewIncomeJournalTemporary(entity, TransChargesItemComps, reg, unit, CostCalculations, "JO", AppSession.UserLogin.UserID, 0);
                                            }
                                        }
                                    }
                                }

                                /* Automatic Journal Testing End */
                            }
                            else if (QueryString_type != "mcu")
                            {
                                if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsJournalCashBased) == "No")
                                {
                                    if (AppParameter.IsYes(AppParameter.ParameterItem.acc_IsJournalAccualNoTemporary))
                                    {
                                        JournalTransactions.AddNewPatientIncomeAccrual(BusinessObject.JournalType.Income, entity.TransactionNo, AppSession.UserLogin.UserID, 0);
                                    }
                                    else
                                    {
                                        var type = AppParameter.GetParameterValue(AppParameter.ParameterItem.RegistrationTypeForAccrualJournal).Split(',');

                                        var mb = new MergeBilling();
                                        mb.LoadByPrimaryKey(reg.RegistrationNo);
                                        if (string.IsNullOrEmpty(mb.FromRegistrationNo))
                                        {
                                            if (type.Contains(reg.SRRegistrationType))
                                            {
                                                var isClosingPeriod = PostingStatus.IsPeriodeClosed(entity.TransactionDate.Value.Date);
                                                if (isClosingPeriod)
                                                {
                                                    return "Financial statements for period: " +
                                                           string.Format("{0:MMMM-yyyy}", entity.TransactionDate.Value.Date) +
                                                           " have been closed. Please contact the authorities.";
                                                }

                                                int? journalId = JournalTransactions.AddNewIncomeJournalTemporary(entity, TransChargesItemComps, reg, unit, CostCalculations, "SU", AppSession.UserLogin.UserID, 0);
                                            }
                                        }
                                        else
                                        {
                                            var freg = new Registration();
                                            freg.LoadByPrimaryKey(mb.FromRegistrationNo);
                                            if (type.Contains(freg.SRRegistrationType))
                                            {
                                                var isClosingPeriod = PostingStatus.IsPeriodeClosed(entity.TransactionDate.Value.Date);
                                                if (isClosingPeriod)
                                                {
                                                    return "Financial statements for period: " +
                                                           string.Format("{0:MMMM-yyyy}", entity.TransactionDate.Value.Date) +
                                                           " have been closed. Please contact the authorities.";
                                                }

                                                int? journalId = JournalTransactions.AddNewIncomeJournalTemporary(entity, TransChargesItemComps, reg, unit, CostCalculations, "SU", AppSession.UserLogin.UserID, 0);
                                            }
                                        }
                                    }
                                }
                                if (QueryString_type == "ds")
                                {
                                    //var charges = new TransCharges();
                                    //charges.LoadByPrimaryKey(txtTransactionNo.Text);

                                    #region Interop
                                    if (AppSession.Parameter.IsUsingHisInterop)
                                    {
                                        var patient = new Patient();
                                        patient.LoadByPrimaryKey(reg.PatientID);

                                        var salutation = string.Empty;
                                        var apstd = new AppStandardReferenceItem();
                                        if (apstd.LoadByPrimaryKey("Salutation", pat.SRSalutation))
                                        { salutation = apstd.ItemName; }

                                        var paramedic = new Paramedic();
                                        paramedic.LoadByPrimaryKey(reg.ParamedicID);

                                        //interop lab
                                        if (entity.ToServiceUnitID == AppSession.Parameter.ServiceUnitLaboratoryID || AppSession.Parameter.ServiceUnitLaboratoryIdArray.Contains(entity.ToServiceUnitID))
                                        {
                                            if (!AppSession.Parameter.IsUsingHisInteropWithMultipleConnection)
                                            {
                                                #region with single connection (default)
                                                switch (AppSession.Parameter.HisInteropConfigName)
                                                {
                                                    #region PAC_HIS_INTEROP_CONNECTION_NAME
                                                    case "PAC_HIS_INTEROP_CONNECTION_NAME":
                                                        if (entity.ToServiceUnitID == AppSession.Parameter.ServiceUnitLaboratoryID)
                                                        {
                                                            var lto = new BusinessObject.Interop.PAC.LabTestOrder
                                                            {
                                                                TransactionNo = entity.TransactionNo,
                                                                TransactionDate = entity.TransactionDate,
                                                                RegistrationNo = entity.RegistrationNo
                                                            };

                                                            lto.MedicalNo = patient.MedicalNo;
                                                            lto.FirstName = patient.FirstName;
                                                            lto.MiddleName = patient.MiddleName;
                                                            lto.LastName = patient.LastName;
                                                            lto.Sex = patient.Sex;
                                                            lto.FromServiceUnitID = reg.ServiceUnitID;
                                                            lto.FromServiceUnitName = unit.ServiceUnitName;
                                                            lto.ClassID = entity.ClassID;

                                                            var cls = new Class();
                                                            cls.LoadByPrimaryKey(entity.ClassID);
                                                            lto.ClassName = cls.ClassName;

                                                            lto.CityOfBirth = patient.CityOfBirth;
                                                            lto.DateOfBirth = patient.DateOfBirth;
                                                            lto.ParamedicID = reg.ParamedicID;

                                                            var param = new Paramedic();
                                                            param.LoadByPrimaryKey(reg.ParamedicID);
                                                            lto.ParamedicName = param.ParamedicName;

                                                            lto.StreetName = patient.StreetName;
                                                            lto.District = patient.District;
                                                            lto.City = patient.City;
                                                            lto.County = patient.County;
                                                            lto.State = patient.State;
                                                            lto.ZipCode = patient.ZipCode;
                                                            lto.PhoneNo = patient.PhoneNo;
                                                            lto.FaxNo = patient.FaxNo;
                                                            lto.Email = patient.Email;
                                                            lto.MobilePhoneNo = patient.MobilePhoneNo;
                                                            lto.Company = patient.Company;

                                                            lto.GuarantorName = grr.GuarantorName;

                                                            foreach (var entity2 in transChargesItems)
                                                            {
                                                                var item = new Item();
                                                                item.LoadByPrimaryKey(entity2.ItemID);

                                                                lto.TestOrderID += item.ItemIDExternal + "^";
                                                                lto.TestOrderName += item.ItemName + "^";
                                                            }

                                                            lto.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                                                            lto.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                                            lto.IsConfirm = false;

                                                            lto.es.Connection.Name = AppConstant.HIS_INTEROP.PAC_HIS_INTEROP_CONNECTION_NAME;
                                                            lto.Save();
                                                        }
                                                        break;
                                                    #endregion
                                                    case "RSSA_HIS_INTEROP_CONNECTION_NAME":
                                                        break;
                                                    #region RSCH_LIS_INTEROP_CONNECTION_NAME
                                                    case AppConstant.HIS_INTEROP.RSCH_LIS_INTEROP_CONNECTION_NAME:
                                                        if (entity.ToServiceUnitID == AppSession.Parameter.ServiceUnitLaboratoryID)
                                                        {
                                                            var olh = new BusinessObject.Interop.RSCH.OrderLabHeader();
                                                            olh.es.Connection.Name = AppConstant.HIS_INTEROP.RSCH_LIS_INTEROP_CONNECTION_NAME;
                                                            olh.OrderLabNo = entity.TransactionNo;
                                                            olh.OrderLabTglOrder = (new DateTime()).NowAtSqlServer().Date;
                                                            olh.OrderLabNoMR = patient.MedicalNo;
                                                            olh.OrderLabNama = ((patient.FirstName + " " + patient.MiddleName).Trim() + " " + patient.LastName).Trim();

                                                            if (!string.IsNullOrEmpty(reg.PhysicianSenders)) olh.OrderLabNamaPengirim = reg.PhysicianSenders;
                                                            else
                                                            {
                                                                var medic = new Paramedic();
                                                                medic.LoadByPrimaryKey(reg.ParamedicID);
                                                                olh.OrderLabNamaPengirim = medic.ParamedicName;
                                                            }

                                                            olh.OrderLabKdPoli = entity.FromServiceUnitID;
                                                            olh.OrderLabBirthdate = patient.DateOfBirth;
                                                            olh.OrderLabAgeYear = reg.AgeInYear;
                                                            olh.OrderLabAgeMonth = reg.AgeInMonth;
                                                            olh.OrderLabAgeDay = reg.AgeInDay;
                                                            olh.OrderLabSex = patient.Sex;
                                                            olh.OrderLabKdPengirim = string.Empty;

                                                            unit = new ServiceUnit();
                                                            unit.LoadByPrimaryKey(entity.FromServiceUnitID);
                                                            olh.OrderlabNamaPoli = unit.ServiceUnitName;

                                                            olh.OrderLabJamOrder = (new DateTime()).NowAtSqlServer().ToString("HH:mm");
                                                            olh.OrderLabStatus = string.Empty;
                                                            olh.OrderLabNoBed = reg.BedID;

                                                            var guar = new Guarantor();
                                                            if (guar.LoadByPrimaryKey(reg.GuarantorID))
                                                                olh.GuarantorName = guar.GuarantorName;

                                                            if (reg.SRRegistrationType == AppConstant.RegistrationType.OutPatient)
                                                            {
                                                                var soap = new RegistrationInfoMedic();
                                                                soap.Query.es.Top = 1;
                                                                soap.Query.Where(soap.Query.RegistrationNo == reg.RegistrationNo, soap.Query.SRMedicalNotesInputType == "SOAP");
                                                                soap.Query.OrderBy(soap.Query.RegistrationInfoMedicID.Descending);
                                                                if (soap.Query.Load()) olh.DiagnoseText = soap.Info3;
                                                                else olh.DiagnoseText = string.Empty;
                                                            }
                                                            else if (reg.SRRegistrationType == AppConstant.RegistrationType.InPatient)
                                                            {
                                                                var nhd = new NursingTransHD();
                                                                nhd.Query.es.Top = 1;
                                                                nhd.Query.Where(nhd.Query.RegistrationNo == reg.RegistrationNo);
                                                                if (nhd.Query.Load())
                                                                {
                                                                    var ndt = new NursingDiagnosaTransDT();
                                                                    ndt.Query.es.Top = 1;
                                                                    ndt.Query.Where(ndt.Query.TransactionNo == nhd.TransactionNo);
                                                                    ndt.Query.OrderBy(ndt.Query.Priority.Ascending);
                                                                    if (ndt.Query.Load()) olh.DiagnoseText = ndt.NursingDiagnosaName;
                                                                    else olh.DiagnoseText = string.Empty;
                                                                }
                                                                else olh.DiagnoseText = string.Empty;
                                                            }

                                                            olh.Save();

                                                            var details = new BusinessObject.Interop.RSCH.OrderLabDetailCollection();
                                                            details.es.Connection.Name = AppConstant.HIS_INTEROP.RSCH_LIS_INTEROP_CONNECTION_NAME;

                                                            foreach (var entity2 in transChargesItems)
                                                            {
                                                                var old = details.AddNew();

                                                                var item = new Item();
                                                                item.LoadByPrimaryKey(entity2.ItemID);

                                                                old.OrderLabNo = entity.TransactionNo;
                                                                old.OrderLabTglOrder = olh.OrderLabTglOrder;
                                                                old.CheckupResultTestCode = item.ItemIDExternal;
                                                                old.OrderLabJamOrder = olh.OrderLabJamOrder;
                                                                old.OrderLabStatus = string.Empty;
                                                                old.OrderLabCito = (entity2.IsCito ?? false) ? "C" : string.Empty;
                                                            }

                                                            if (details.Any()) details.Save();
                                                        }
                                                        break;
                                                    #endregion
                                                    #region SYSMEX_LIS_INTEROP_CONNECTION_NAME
                                                    case AppConstant.HIS_INTEROP.MEDICLAB_LIS_INTEROP_CONNECTION_NAME:
                                                    case AppConstant.HIS_INTEROP.VANSLAB_LIS_INTEROP_CONNECTION_NAME:
                                                    case AppConstant.HIS_INTEROP.SYSMEX_LIS_INTEROP_CONNECTION_NAME:
                                                        if (entity.ToServiceUnitID == AppSession.Parameter.ServiceUnitLaboratoryID)
                                                        {
                                                            var lo = new BusinessObject.Interop.SYSMEX.LisOrder();
                                                            if (AppSession.Parameter.HisInteropConfigName == AppConstant.HIS_INTEROP.VANSLAB_LIS_INTEROP_CONNECTION_NAME) lo.es.Connection.Name = AppConstant.HIS_INTEROP.VANSLAB_LIS_INTEROP_CONNECTION_NAME;
                                                            else if (AppSession.Parameter.HisInteropConfigName == AppConstant.HIS_INTEROP.MEDICLAB_LIS_INTEROP_CONNECTION_NAME) lo.es.Connection.Name = AppConstant.HIS_INTEROP.MEDICLAB_LIS_INTEROP_CONNECTION_NAME;
                                                            else
                                                                lo.es.Connection.Name = AppConstant.HIS_INTEROP.SYSMEX_LIS_INTEROP_CONNECTION_NAME;

                                                            lo.MessageDt = (entity.LastUpdateDateTime ?? (new DateTime()).NowAtSqlServer()).ToString("yyyyMMddHHmmss");
                                                            lo.OrderControl = "NW";
                                                            if (AppSession.Parameter.HealthcareInitialAppsVersion == "GRHA") lo.Pid = string.IsNullOrEmpty(patient.MedicalNo) ? patient.PatientID : patient.MedicalNo;
                                                            else lo.Pid = patient.MedicalNo;
                                                            lo.Pname = patient.PatientName;
                                                            lo.Address1 = patient.StreetName.Trim();

                                                            switch (AppSession.Parameter.HealthcareInitialAppsVersion)
                                                            {
                                                                case "RSTJ":
                                                                    lo.Pname = (patient.PatientName + " " + salutation).Trim();
                                                                    lo.Address2 = patient.District;
                                                                    lo.Address3 = patient.County;
                                                                    lo.Address4 = patient.State;
                                                                    break;

                                                                case "RSUTAMA":
                                                                case "KLUTAMA":
                                                                    lo.Address2 = patient.District;
                                                                    lo.Address3 = patient.County + " " + patient.State;
                                                                    lo.Address4 = patient.MobilePhoneNo;
                                                                    break;

                                                                case "RSMP":
                                                                case "GRHA":
                                                                    if (!string.IsNullOrEmpty(reg.ReferralID))
                                                                    {
                                                                        var refral = new Referral();
                                                                        refral.LoadByPrimaryKey(reg.ReferralID);

                                                                        lo.Address3 = "";
                                                                    }

                                                                    lo.Address2 = patient.District;
                                                                    lo.Address3 = "";
                                                                    lo.Address4 = grr.GuarantorName;
                                                                    break;
                                                                case "RSSMCB":
                                                                    lo.Address2 = grr.GuarantorName;
                                                                    lo.Address3 = patient.District.Trim() + " " + patient.County.Trim();
                                                                    if (AppSession.Parameter.IsUsingHisInterop)
                                                                    {
                                                                        lo.Address4 = patient.MobilePhoneNo;
                                                                    }
                                                                    else
                                                                    {
                                                                        if (AppSession.Parameter.HealthcareInitial == "RSSMHB")
                                                                        {
                                                                            lo.Address3 += " " + patient.State.Trim();

                                                                            var mb = new MergeBilling();
                                                                            if (mb.LoadByPrimaryKey(reg.RegistrationNo))
                                                                            {
                                                                                if (!string.IsNullOrEmpty(mb.FromRegistrationNo))
                                                                                {
                                                                                    var freg = new Registration();
                                                                                    freg.LoadByPrimaryKey(mb.FromRegistrationNo);
                                                                                    var funit = new ServiceUnit();
                                                                                    funit.LoadByPrimaryKey(freg.ServiceUnitID);
                                                                                    lo.Address4 = funit.ServiceUnitID + "^" + funit.ServiceUnitName;
                                                                                }
                                                                                else lo.Address4 = unit.ServiceUnitID + "^" + unit.ServiceUnitName; ;
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            lo.Address4 = patient.State;
                                                                        }
                                                                    }

                                                                    break;
                                                                case "YBRSGKP":
                                                                    lo.Address2 = "";
                                                                    switch (grr.SRGuarantorType)
                                                                    {
                                                                        case "09":
                                                                            lo.Address2 = "BPJS";
                                                                            break;
                                                                        case "00":
                                                                            lo.Address2 = "PRIBADI";
                                                                            break;
                                                                        default:
                                                                            lo.Address2 = "MITRA";
                                                                            break;

                                                                    }

                                                                    lo.Address3 = grr.GuarantorName;
                                                                    lo.Address4 = ClassID;

                                                                    var cls = new Class();
                                                                    cls.LoadByPrimaryKey(ClassID);
                                                                    lo.Address4 = reg.SRRegistrationType == "IPR" ? cls.ClassName : string.Empty;
                                                                    break;
                                                                case "RSGPI":

                                                                    var cl = new Class();
                                                                    cl.LoadByPrimaryKey(ClassID);
                                                                    lo.Address2 = patient.District;
                                                                    lo.Address3 = grr.GuarantorName;
                                                                    lo.Address4 = cl.ClassName;
                                                                    break;
                                                                default:
                                                                    lo.Pname = patient.PatientName;
                                                                    lo.Address2 = patient.District;
                                                                    lo.Address3 = patient.County;
                                                                    lo.Address4 = patient.State;
                                                                    break;
                                                            }

                                                            lo.Ptype = reg.SRRegistrationType == AppConstant.RegistrationType.InPatient ? "IN" : "OP";
                                                            lo.BirthDt = (patient.DateOfBirth ?? (new DateTime()).NowAtSqlServer()).ToString("yyyyMMddHHmmss");
                                                            lo.Sex = patient.Sex == "M" ? "1" : "0";
                                                            lo.Ono = entity.TransactionNo;
                                                            lo.RequestDt = (entity.LastUpdateDateTime ?? (new DateTime()).NowAtSqlServer()).ToString("yyyyMMddHHmmss");

                                                            //switch (AppSession.Parameter.HealthcareInitialAppsVersion)
                                                            //{
                                                            //    case "RSTJ":
                                                            //        lo.RequestDt = (entity.CreatedDateTime ?? (new DateTime()).NowAtSqlServer()).ToString("yyyyMMddHHmmss");
                                                            //        break;
                                                            //    default:
                                                            //        lo.RequestDt = (entity.LastUpdateDateTime ?? (new DateTime()).NowAtSqlServer()).ToString("yyyyMMddHHmmss");
                                                            //        break;
                                                            //}

                                                            unit = new ServiceUnit();
                                                            unit.LoadByPrimaryKey(entity.FromServiceUnitID);

                                                            lo.Source = unit.ServiceUnitID + "^" + unit.ServiceUnitName;

                                                            var param = new Paramedic();

                                                            switch (AppSession.Parameter.HealthcareInitialAppsVersion)
                                                            {
                                                                case "RSUTAMA":
                                                                    if (!string.IsNullOrEmpty(reg.ReferralID))
                                                                    {
                                                                        var refer = new Referral();
                                                                        refer.LoadByPrimaryKey(reg.ReferralID);

                                                                        lo.Clinician = reg.ReferralID + "^" + refer.ReferralName;
                                                                    }
                                                                    else if (!string.IsNullOrEmpty(reg.PhysicianSenders))
                                                                    {
                                                                        lo.Clinician = reg.ParamedicID + "^" + reg.PhysicianSenders;
                                                                    }
                                                                    else
                                                                    {
                                                                        param.LoadByPrimaryKey(reg.ParamedicID);

                                                                        lo.Clinician = reg.ParamedicID + "^" + param.ParamedicName;
                                                                    }
                                                                    break;
                                                                default:
                                                                    param.LoadByPrimaryKey(reg.ParamedicID);

                                                                    lo.Clinician = reg.ParamedicID + "^" + param.ParamedicName;
                                                                    break;
                                                            }

                                                            lo.RoomNo = reg.RoomID;
                                                            lo.Priority = transChargesItems.Any(t => (t.IsCito ?? false)) ? "U" : "R";

                                                            if (AppSession.Parameter.HisInteropConfigName == AppConstant.HIS_INTEROP.VANSLAB_LIS_INTEROP_CONNECTION_NAME) lo.Cmt = grr.GuarantorName;
                                                            else
                                                            {
                                                                if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSSMCB" && AppSession.Parameter.IsUsingHisInteropToHcLab)
                                                                    lo.Cmt = patient.Ssn;
                                                                else
                                                                    lo.Cmt = entity.Notes; //string.Empty;
                                                            }

                                                            lo.Visitno = entity.RegistrationNo;

                                                            var prefixCode = AppSession.Parameter.PrefixOnoSysmexInterop;
                                                            if (!string.IsNullOrWhiteSpace(prefixCode))
                                                            {
                                                                lo.HealthcareCode = prefixCode;
                                                                lo.Pid = prefixCode + lo.Pid;
                                                                lo.Ono = prefixCode + lo.Ono;
                                                                lo.Source = prefixCode + lo.Source;
                                                                lo.Clinician = prefixCode + lo.Clinician;
                                                            }

                                                            //switch (AppSession.Parameter.HealthcareInitialAppsVersion)
                                                            //{
                                                            //    case "RSUI":
                                                            //        lo.HealthcareCode = "TG";
                                                            //        lo.Pid = "TG" + lo.Pid;
                                                            //        lo.Ono = "TG" + lo.Ono;
                                                            //        lo.Source = "TG" + lo.Source;
                                                            //        lo.Clinician = "TG" + lo.Clinician;
                                                            //        break;
                                                            //    case "RSPM":
                                                            //        lo.HealthcareCode = "ST";
                                                            //        lo.Pid = "ST" + lo.Pid;
                                                            //        lo.Ono = "ST" + lo.Ono;
                                                            //        lo.Source = "ST" + lo.Source;
                                                            //        lo.Clinician = "ST" + lo.Clinician;
                                                            //        break;
                                                            //}

                                                            var items = new ItemCollection();
                                                            items.Query.Where(items.Query.ItemID.In(transChargesItems.Where(t => string.IsNullOrEmpty(t.ParentNo)).Select(t => t.ItemID)));
                                                            items.Query.Load();

                                                            foreach (var item in items)
                                                            {
                                                                if (AppSession.Parameter.HisInteropConfigName == AppConstant.HIS_INTEROP.VANSLAB_LIS_INTEROP_CONNECTION_NAME) lo.OrderTestid += item.ItemID + "~";
                                                                else lo.OrderTestid += item.ItemIDExternal + "~";

                                                                //lo.OrderTestid += item.ItemIDExternal + "~";
                                                            }

                                                            lo.Save();
                                                        }
                                                        break;
                                                    #endregion
                                                    #region PRODIA_LIS_INTEROP_CONNECTION_NAME
                                                    case AppConstant.HIS_INTEROP.PRODIA_LIS_INTEROP_CONNECTION_NAME:
                                                        break;
                                                    #endregion
                                                    #region ELIMS_LIS_INTEROP_CONNECTION_NAME
                                                    case AppConstant.HIS_INTEROP.ELIMS_LIS_INTEROP_CONNECTION_NAME:
                                                        if (entity.ToServiceUnitID == AppSession.Parameter.ServiceUnitLaboratoryID || AppSession.Parameter.ServiceUnitLaboratoryIdArray.Contains(entity.ToServiceUnitID))
                                                        {

                                                            //var kl = new BusinessObject.Interop.ELIMS.KirimLIS();
                                                            //kl.es.Connection.Name = AppConstant.HIS_INTEROP.ELIMS_LIS_INTEROP_CONNECTION_NAME;

                                                            var kls = new BusinessObject.Interop.ELIMS.KirimLISCollection();

                                                            switch (AppSession.Parameter.HealthcareInitialAppsVersion)
                                                            {
                                                                case "RSISB":
                                                                    {
                                                                        foreach (var detail in transChargesItems.Where(t => t.IsOrderRealization ?? false))
                                                                        {
                                                                            var kl = kls.AddNew();
                                                                            kl.ModifiedDate = entity.ExecutionDate;
                                                                            kl.NoPasien = patient.MedicalNo;
                                                                            kl.KodeKunjungan = entity.TransactionNo;
                                                                            kl.Nama = patient.PatientName + " " + apstd.ItemName;
                                                                            kl.Email = patient.Email;
                                                                            kl.DateOfBirth = patient.DateOfBirth;
                                                                            kl.UmurTahun = reg.AgeInYear;
                                                                            kl.UmurBulan = reg.AgeInMonth;
                                                                            kl.UmurHari = reg.AgeInDay;
                                                                            kl.Gender = patient.Sex == "M" ? "L" : "P";
                                                                            kl.Alamat = patient.Address;
                                                                            kl.Diagnosa = reg.Anamnesis;
                                                                            kl.TglPeriksa = entity.ExecutionDate;
                                                                            kl.Pengirim = reg.ParamedicID;

                                                                            if (!string.IsNullOrEmpty(reg.PhysicianSenders)) kl.PengirimName = reg.PhysicianSenders;
                                                                            else
                                                                            {
                                                                                var medic = new Paramedic();
                                                                                medic.LoadByPrimaryKey(reg.ParamedicID);
                                                                                kl.PengirimName = medic.ParamedicName;
                                                                            }

                                                                            var cls = new Class();
                                                                            cls.LoadByPrimaryKey(entity.ClassID);
                                                                            kl.Kelas = cls.ClassID;
                                                                            kl.KelasName = cls.ClassID;

                                                                            var room = new ServiceRoom();
                                                                            room.LoadByPrimaryKey(entity.RoomID);
                                                                            kl.Ruang = room.RoomID;
                                                                            kl.RuangName = room.RoomName;

                                                                            kl.CaraBayar = grr.GuarantorID;
                                                                            kl.CaraBayarName = grr.GuarantorName;



                                                                            kl.KodeTarif = detail.ItemID;
                                                                            kl.Update = detail.IsCorrection == false ? "N" : "D";


                                                                            kl.ISInap = reg.SRRegistrationType == "IPR" ? "1" : "0";
                                                                            kl.Status = "1";

                                                                            kl.NIK = patient.Ssn;



                                                                            kl.IS_CITO = detail.IsCito == false ? "0" : "1";



                                                                        }

                                                                    }
                                                                    break;
                                                                default:
                                                                    foreach (var detail in transChargesItems.Where(t => t.IsOrderRealization ?? false))
                                                                    {
                                                                        var kl = kls.AddNew();
                                                                        kl.ModifiedDate = entity.ExecutionDate;
                                                                        kl.NoPasien = patient.MedicalNo;
                                                                        kl.KodeKunjungan = entity.TransactionNo;
                                                                        kl.Nama = patient.PatientName + " " + apstd.ItemName;
                                                                        kl.Email = patient.Email;
                                                                        kl.DateOfBirth = patient.DateOfBirth;
                                                                        kl.UmurTahun = reg.AgeInYear;
                                                                        kl.UmurBulan = reg.AgeInMonth;
                                                                        kl.UmurHari = reg.AgeInDay;
                                                                        kl.Gender = patient.Sex == "M" ? "L" : "P";
                                                                        kl.Alamat = patient.Address;
                                                                        kl.Diagnosa = reg.Anamnesis;
                                                                        kl.TglPeriksa = entity.ExecutionDate;
                                                                        kl.Pengirim = reg.ParamedicID;

                                                                        if (!string.IsNullOrEmpty(reg.PhysicianSenders)) kl.PengirimName = reg.PhysicianSenders;
                                                                        else
                                                                        {
                                                                            var medic = new Paramedic();
                                                                            medic.LoadByPrimaryKey(reg.ParamedicID);
                                                                            kl.PengirimName = medic.ParamedicName;
                                                                        }

                                                                        var cls = new Class();
                                                                        cls.LoadByPrimaryKey(entity.ClassID);
                                                                        kl.Kelas = cls.ClassID;
                                                                        kl.KelasName = cls.ClassID;

                                                                        var room = new ServiceRoom();
                                                                        room.LoadByPrimaryKey(entity.RoomID);
                                                                        kl.Ruang = room.RoomID;
                                                                        kl.RuangName = room.RoomName;

                                                                        kl.CaraBayar = grr.GuarantorID;
                                                                        kl.CaraBayarName = grr.GuarantorName;



                                                                        kl.KodeTarif = detail.ItemID;
                                                                        kl.Update = detail.IsCorrection == false ? "N" : "D";


                                                                        kl.ISInap = reg.SRRegistrationType == "IPR" ? "1" : "0";
                                                                        kl.Status = "1";

                                                                        kl.NIK = patient.Ssn;



                                                                        kl.IS_CITO = detail.IsCito == false ? "0" : "1";

                                                                    }


                                                                    break;
                                                            }

                                                            kls.es.Connection.Name = AppConstant.HIS_INTEROP.ELIMS_LIS_INTEROP_CONNECTION_NAME;
                                                            kls.Save();

                                                        }
                                                        break;
                                                    #endregion
                                                    case AppConstant.HIS_INTEROP.WYNAKOM_LIS_INTEROP_CONNECTION_NAME:
                                                        if (entity.ToServiceUnitID == AppSession.Parameter.ServiceUnitLaboratoryID)
                                                        {
                                                            var demographics = new BusinessObject.Interop.Wynakom.Demographics();
                                                            demographics.es.Connection.Name = AppConstant.HIS_INTEROP.WYNAKOM_LIS_INTEROP_CONNECTION_NAME;

                                                            if (!demographics.LoadByPrimaryKey(string.IsNullOrEmpty(patient.MedicalNo) ? patient.PatientID : patient.MedicalNo)) demographics = new BusinessObject.Interop.Wynakom.Demographics();
                                                            demographics.PatientId = string.IsNullOrEmpty(patient.MedicalNo) ? patient.PatientID : patient.MedicalNo;
                                                            demographics.GenderId = patient.Sex;
                                                            demographics.DateOfBirth = patient.DateOfBirth;

                                                            var std = new AppStandardReferenceItem();
                                                            if (std.LoadByPrimaryKey(AppEnum.StandardReference.Salutation.ToString(), patient.SRSalutation))
                                                                demographics.PatientName = string.Format("{0} {1}", std.ItemName, patient.PatientName);
                                                            else
                                                                demographics.PatientName = string.Format("{0}", patient.PatientName);//string.Format("{0} {1}", string.Empty, patient.PatientName);

                                                            demographics.PatientAddress = patient.Address;
                                                            demographics.CityName = patient.City;
                                                            demographics.PhoneNumber = patient.PhoneNo;
                                                            demographics.FaxNumber = patient.FaxNo;
                                                            demographics.MobileNumber = patient.MobilePhoneNo;
                                                            demographics.Email = patient.Email;
                                                            demographics.KtpNumber = patient.Ssn;

                                                            var registration = new BusinessObject.Interop.Wynakom.Registration();
                                                            registration.es.Connection.Name = AppConstant.HIS_INTEROP.WYNAKOM_LIS_INTEROP_CONNECTION_NAME;

                                                            registration.PatientId = demographics.PatientId;
                                                            registration.VisitNumber = reg.RegistrationNo;
                                                            registration.OrderNumber = entity.TransactionNo;
                                                            registration.OrderDateTime = entity.ExecutionDate;
                                                            registration.DiagnoseName = "";
                                                            registration.Cito = transChargesItems.Any(t => t.IsCito ?? false);
                                                            registration.ServiceUnitID = unit.ServiceUnitID;
                                                            registration.ServiceUnitName = unit.ServiceUnitName;
                                                            registration.GuarantorID = grr.GuarantorID;
                                                            registration.GuarantorName = grr.GuarantorName;

                                                            var pmedic = new Paramedic();
                                                            pmedic.LoadByPrimaryKey(reg.ParamedicID);
                                                            registration.DoctorID = pmedic.ParamedicID;
                                                            registration.DoctorName = pmedic.ParamedicName;

                                                            var cls = new Class();
                                                            cls.LoadByPrimaryKey(entity.ClassID);
                                                            registration.ClassID = cls.ClassID;
                                                            registration.ClassName = cls.ClassName;
                                                            registration.AgreementID = cls.ClassID;
                                                            registration.AgreementName = cls.ClassName;

                                                            registration.WardID = unit.ServiceUnitID;
                                                            registration.WardName = unit.ServiceUnitName;

                                                            var room = new ServiceRoom();
                                                            room.LoadByPrimaryKey(entity.RoomID);
                                                            registration.RoomID = room.RoomID;
                                                            registration.RoomName = room.RoomName;

                                                            registration.BedID = entity.BedID;
                                                            registration.BedName = entity.BedID;
                                                            registration.RegUserName = AppSession.UserLogin.UserName;
                                                            registration.Notes = reg.Notes;

                                                            var ordered_item = new BusinessObject.Interop.Wynakom.OrderedItems();
                                                            ordered_item.es.Connection.Name = AppConstant.HIS_INTEROP.WYNAKOM_LIS_INTEROP_CONNECTION_NAME;
                                                            ordered_item.OrderNumber = registration.OrderNumber;
                                                            ordered_item.OrderItemDate = registration.OrderDateTime;

                                                            bool isInsert = false;
                                                            foreach (var entity2 in transChargesItems.Where(t => t.IsOrderRealization ?? false && t.IsVoid == false))
                                                            {
                                                                var item = new Item();
                                                                item.LoadByPrimaryKey(entity2.ItemID);

                                                                if (!string.IsNullOrEmpty(item.ItemIDExternal))
                                                                {
                                                                    if (AppSession.Parameter.HealthcareInitialAppsVersion == "YBRSGKP")
                                                                    {
                                                                        if (AppSession.Parameter.HealthcareInitial == "RSI")
                                                                        {
                                                                            var pa = new BusinessObject.Interop.ROCHE.PocAdt();
                                                                            pa.es.Connection.Name = AppConstant.HIS_INTEROP.ROCHE_LIS_INTEROP_CONNECTION_NAME;
                                                                            pa.Action = "ADD";
                                                                            pa.Pid = patient.MedicalNo;
                                                                            pa.Dob = patient.DateOfBirth;
                                                                            pa.Sex = patient.Sex;
                                                                            pa.VisitNum = entity.TransactionNo;

                                                                            var pc = string.Empty;
                                                                            if (reg.SRRegistrationType == AppConstant.RegistrationType.InPatient) pc = "1";
                                                                            else if (reg.SRRegistrationType == AppConstant.RegistrationType.OutPatient) pc = "2";
                                                                            else if (reg.SRRegistrationType == AppConstant.RegistrationType.EmergencyPatient) pc = "3";
                                                                            else pc = "2";
                                                                            pa.PatientClass = pc;

                                                                            var fUnit = new ServiceUnit();
                                                                            fUnit.LoadByPrimaryKey(entity.FromServiceUnitID);

                                                                            pa.LocationCode = fUnit.ServiceUnitID;
                                                                            pa.LocationName = fUnit.ServiceUnitName;
                                                                            pa.DoctorCode = pmedic.ParamedicID;
                                                                            pa.DoctorName = pmedic.ParamedicName;
                                                                            pa.AdmitDt = entity.ExecutionDate;
                                                                            pa.PatientName = patient.PatientName;

                                                                            var fRoom = new ServiceRoom();
                                                                            fRoom.LoadByPrimaryKey(reg.RoomID);

                                                                            pa.RoomCode = fRoom.RoomID;
                                                                            pa.RoomName = fRoom.RoomName;

                                                                            pa.Save();
                                                                        }
                                                                    }
                                                                }

                                                                //ordered_item.OrderItemID += (string.IsNullOrEmpty(item.ItemIDExternal) ? item.ItemID : item.ItemIDExternal) + "~";
                                                                ordered_item.OrderItemID += item.ItemID + "~";
                                                                ordered_item.OrderItemName += item.ItemName + "~";
                                                                isInsert = true;
                                                            }

                                                            if (isInsert)
                                                            {
                                                                if (!string.IsNullOrEmpty(ordered_item.OrderItemID) && !string.IsNullOrEmpty(ordered_item.OrderItemName))
                                                                {
                                                                    ordered_item.OrderItemID = ordered_item.OrderItemID.Remove(ordered_item.OrderItemID.Length - 1);
                                                                    ordered_item.OrderItemName = ordered_item.OrderItemName.Remove(ordered_item.OrderItemName.Length - 1);
                                                                }

                                                                demographics.es.Connection.Name = AppConstant.HIS_INTEROP.WYNAKOM_LIS_INTEROP_CONNECTION_NAME;
                                                                demographics.Save();

                                                                registration.es.Connection.Name = AppConstant.HIS_INTEROP.WYNAKOM_LIS_INTEROP_CONNECTION_NAME;
                                                                registration.Save();

                                                                ordered_item.es.Connection.Name = AppConstant.HIS_INTEROP.WYNAKOM_LIS_INTEROP_CONNECTION_NAME;
                                                                ordered_item.Save();
                                                            }
                                                        }
                                                        break;
                                                    case AppConstant.HIS_INTEROP.LINK_LIS_INTEROP_CONNECTION_NAME:
                                                        var id_ruangan = new ServiceUnitBridging();
                                                        id_ruangan.Query.Where(id_ruangan.Query.ServiceUnitID == reg.ServiceUnitID, id_ruangan.Query.SRBridgingType == AppEnum.BridgingType.LINK_LIS.ToString());
                                                        if (!id_ruangan.Query.Load())
                                                        {
                                                            var ruangan = new ServiceUnit();
                                                            ruangan.LoadByPrimaryKey(reg.ServiceUnitID);
                                                            return "Service Unit : " + ruangan.ServiceUnitName + " not mapped to Link Lis.";
                                                        }

                                                        var id_dokter = new ParamedicBridging();
                                                        id_dokter.Query.Where(id_dokter.Query.ParamedicID == reg.ParamedicID, id_dokter.Query.SRBridgingType == AppEnum.BridgingType.LINK_LIS.ToString());
                                                        if (!id_dokter.Query.Load())
                                                        {
                                                            var ruangan = new Paramedic();
                                                            ruangan.LoadByPrimaryKey(reg.ParamedicID);
                                                            return "Physician : " + ruangan.ParamedicName + " not mapped to Link Lis.";

                                                        }

                                                        var id_status = new GuarantorBridging();
                                                        id_status.Query.Where(id_status.Query.GuarantorID == reg.GuarantorID, id_status.Query.SRBridgingType == AppEnum.BridgingType.LINK_LIS.ToString());
                                                        if (!id_status.Query.Load())
                                                        {
                                                            var ruangan = new Guarantor();
                                                            ruangan.LoadByPrimaryKey(reg.GuarantorID);
                                                            return "Guarantor : " + ruangan.GuarantorName + " not mapped to Link Lis.";
                                                        }

                                                        foreach (var detail in transChargesItems.Where(t => t.IsOrderRealization ?? false))
                                                        {
                                                            var ib = new ItemBridging();
                                                            ib.Query.Where(ib.Query.ItemID == detail.ItemID, ib.Query.SRBridgingType == AppEnum.BridgingType.LINK_LIS.ToString());
                                                            if (!ib.Query.Load())
                                                            {
                                                                var ruangan = new Item();
                                                                ruangan.LoadByPrimaryKey(detail.ItemID);
                                                                return "Item : " + ruangan.ItemName + " not mapped to Link Lis.";
                                                            }
                                                        }

                                                        var svc = new Common.LinkLis.Service();
                                                        var response = svc.InsertRegistrasiPasien(new Common.LinkLis.Object.RegistrasiPasien()
                                                        {
                                                            no_rm = pat.MedicalNo,
                                                            nama = pat.PatientName,
                                                            alamat = pat.Address,
                                                            tgl_lahir = pat.DateOfBirth.Value.ToString("dd-MM-yyyy"),
                                                            jenis_kelamin = pat.Sex == "F" ? "Perempuan" : "Laki-laki",
                                                            status = id_status.BridgingID
                                                        }, true);

                                                        svc = new Common.LinkLis.Service();
                                                        var kode = svc.GetKodePemeriksaan();

                                                        svc = new Common.LinkLis.Service();
                                                        response = svc.InsertRegistrasiPemeriksaan(new Common.LinkLis.Object.RegistrasiPemeriksaan()
                                                        {
                                                            kode_pemeriksaan = kode.KodePemeriksaan,
                                                            no_rm = pat.MedicalNo,
                                                            id_ruangan = id_ruangan.BridgingID,
                                                            id_dokter = id_dokter.BridgingID.Split('-')[1],
                                                            id_analis = entity.AnalystID.Split('-')[1],
                                                            id_dokterpk = entity.LaboratoryParamedicID.Split('-')[1],
                                                            id_status = id_status.BridgingID
                                                        }, true);

                                                        var list_pemeriksaan = new List<Common.LinkLis.Object.ListPemeriksaan>();
                                                        var list_parameter = new List<Common.LinkLis.Object.ListParameter>();
                                                        foreach (var detail in transChargesItems.Where(t => t.IsOrderRealization ?? false))
                                                        {
                                                            var ib = new ItemBridging();
                                                            ib.Query.Where(ib.Query.ItemID == detail.ItemID, ib.Query.SRBridgingType == AppEnum.BridgingType.LINK_LIS.ToString());
                                                            ib.Query.Load();

                                                            if (!list_pemeriksaan.Any(p => p.list_pemeriksaan == ib.BridgingGroupID)) list_pemeriksaan.Add(new Common.LinkLis.Object.ListPemeriksaan() { list_pemeriksaan = ib.BridgingGroupID });
                                                            list_parameter.Add(new Common.LinkLis.Object.ListParameter() { list_pemeriksaan = ib.BridgingGroupID, list_parameter = ib.BridgingID });
                                                            detail.ResultValue = kode.KodePemeriksaan;
                                                        }

                                                        svc = new Common.LinkLis.Service();
                                                        response = svc.InsertRegistrasiParameterPemeriksaan(new Common.LinkLis.Object.ParameterPemeriksaan()
                                                        {
                                                            kode_pemeriksaan = kode.KodePemeriksaan,
                                                            list_pemeriksaan = list_pemeriksaan,
                                                            list_parameter = list_parameter
                                                        });

                                                        transChargesItems.Save();
                                                        break;
                                                    case AppConstant.HIS_INTEROP.VANSLITE_LIS_INTEROP_CONNECTION_NAME:
                                                        {
                                                            var pmedic = new Paramedic();
                                                            pmedic.LoadByPrimaryKey(reg.ParamedicID);

                                                            var tunit = new ServiceUnit();
                                                            tunit.LoadByPrimaryKey(entity.FromServiceUnitID);

                                                            var vlites = new BusinessObject.Interop.VANSLITE.OrderLabCollection();

                                                            foreach (var detail in transChargesItems.Where(t => t.IsOrderRealization ?? false))
                                                            {
                                                                var vlite = vlites.AddNew();
                                                                vlite.AsalLab = tunit.ServiceUnitName;
                                                                vlite.NoLab = detail.TransactionNo;
                                                                vlite.NoLabDtl = detail.SequenceNo;
                                                                vlite.NoRegistrasi = reg.RegistrationNo;
                                                                vlite.NoRm = patient.MedicalNo;
                                                                vlite.TglOrder = Convert.ToDateTime(entity.ExecutionDate.Value.ToString("MM/dd/yyyy"));
                                                                vlite.NamaPas = patient.PatientName;
                                                                vlite.JenisKel = patient.Sex == "M" ? "L" : "P";
                                                                vlite.TglLahir = patient.DateOfBirth;
                                                                vlite.Usia = Helper.GetAgeInYear(patient.DateOfBirth.Value, entity.ExecutionDate.Value).ToString();
                                                                vlite.Alamat = patient.Address;
                                                                vlite.KodeDokKirim = reg.ParamedicID;
                                                                vlite.NamaDokKirim = pmedic.ParamedicName;
                                                                vlite.KodeRuang = entity.FromServiceUnitID;
                                                                vlite.NamaRuang = tunit.ServiceUnitName;
                                                                vlite.KodeCaraBayar = reg.GuarantorID;
                                                                vlite.CaraBayar = grr.GuarantorName;
                                                                vlite.KetKlinis = reg.Complaint;
                                                                vlite.KodeTest = detail.ItemID;
                                                                var item = new Item();
                                                                item.LoadByPrimaryKey(detail.ItemID);
                                                                vlite.Test = item.ItemName;
                                                                vlite.Harga = (detail.Price ?? 0).ToInt();
                                                                vlite.WaktuKirim = Convert.ToDateTime(entity.ExecutionDate.Value.ToString("HH:mm:ss"));
                                                                vlite.Prioritas = (detail.IsCito ?? false) ? "C" : "R";
                                                                vlite.JnsRawat = reg.SRRegistrationType == "IPR" ? "Rawat Inap" : "Rawat Jalan";
                                                                if (string.IsNullOrWhiteSpace(detail.ParamedicID)) vlite.DokJaga = "";
                                                                else
                                                                {
                                                                    var tmedic = new Paramedic();
                                                                    tmedic.LoadByPrimaryKey(detail.ParamedicID);
                                                                    vlite.DokJaga = tmedic.ParamedicName;
                                                                }
                                                                vlite.Status = 0;
                                                                vlite.Batal = 0;
                                                                vlite.JumlahTest = (detail.ChargeQuantity ?? 0).ToInt();

                                                                detail.IsSendToLIS = true;
                                                            }

                                                            transChargesItems.Save();

                                                            vlites.es.Connection.Name = AppConstant.HIS_INTEROP.VANSLITE_LIS_INTEROP_CONNECTION_NAME;
                                                            vlites.Save();
                                                        }
                                                        break;
                                                }
                                                #endregion
                                            }
                                            else
                                            {
                                                #region with multiple connection
                                                var listItem = transChargesItems.Select(q => new
                                                {
                                                    ItemId = q.ItemID
                                                });

                                                var itemBridgings = new ItemBridgingCollection();
                                                var ibq = new ItemBridgingQuery("a");
                                                var connq = new AppStandardReferenceItemQuery("b");
                                                ibq.Select(ibq, connq.ReferenceID.As("refToAppStandardReferenceItem_BridgingTypeReferenceID"));
                                                ibq.InnerJoin(connq).On(connq.StandardReferenceID == AppEnum.StandardReference.BridgingType.ToString() && connq.ItemID == ibq.SRBridgingType);
                                                ibq.Where(ibq.ItemID.In(listItem.Select(l => l.ItemId)), ibq.IsActive == true,
                                                    connq.ReferenceID.IsNotNull(), connq.ReferenceID != string.Empty);
                                                ibq.OrderBy(connq.ReferenceID.Ascending, ibq.ItemID.Ascending);
                                                itemBridgings.Load(ibq);

                                                foreach (var group in (from g in itemBridgings
                                                                       group g by new
                                                                       {
                                                                           g.ConnectionName
                                                                       }
                                                   into grp
                                                                       orderby grp.Key.ConnectionName
                                                                       select new
                                                                       {
                                                                           ConnectionName = grp.Key.ConnectionName
                                                                       }))
                                                {
                                                    switch (group.ConnectionName)
                                                    {
                                                        #region PAC_HIS_INTEROP_CONNECTION_NAME
                                                        case AppConstant.HIS_INTEROP.PAC_HIS_INTEROP_CONNECTION_NAME:
                                                            if (entity.ToServiceUnitID == AppSession.Parameter.ServiceUnitLaboratoryID)
                                                            {
                                                                var lto = new BusinessObject.Interop.PAC.LabTestOrder
                                                                {
                                                                    TransactionNo = entity.TransactionNo,
                                                                    TransactionDate = entity.TransactionDate,
                                                                    RegistrationNo = entity.RegistrationNo
                                                                };

                                                                lto.MedicalNo = patient.MedicalNo;
                                                                lto.FirstName = patient.FirstName;
                                                                lto.MiddleName = patient.MiddleName;
                                                                lto.LastName = patient.LastName;
                                                                lto.Sex = patient.Sex;
                                                                lto.FromServiceUnitID = reg.ServiceUnitID;
                                                                lto.FromServiceUnitName = unit.ServiceUnitName;
                                                                lto.ClassID = entity.ClassID;

                                                                var cls = new Class();
                                                                cls.LoadByPrimaryKey(entity.ClassID);
                                                                lto.ClassName = cls.ClassName;

                                                                lto.CityOfBirth = patient.CityOfBirth;
                                                                lto.DateOfBirth = patient.DateOfBirth;
                                                                lto.ParamedicID = reg.ParamedicID;

                                                                var param = new Paramedic();
                                                                param.LoadByPrimaryKey(reg.ParamedicID);
                                                                lto.ParamedicName = param.ParamedicName;

                                                                lto.StreetName = patient.StreetName;
                                                                lto.District = patient.District;
                                                                lto.City = patient.City;
                                                                lto.County = patient.County;
                                                                lto.State = patient.State;
                                                                lto.ZipCode = patient.ZipCode;
                                                                lto.PhoneNo = patient.PhoneNo;
                                                                lto.FaxNo = patient.FaxNo;
                                                                lto.Email = patient.Email;
                                                                lto.MobilePhoneNo = patient.MobilePhoneNo;
                                                                lto.Company = patient.Company;

                                                                lto.GuarantorName = grr.GuarantorName;

                                                                foreach (var i in itemBridgings.Where(i => i.ConnectionName == group.ConnectionName))
                                                                {
                                                                    var item = new Item();
                                                                    item.LoadByPrimaryKey(i.ItemID);

                                                                    lto.TestOrderID += i.ItemIdExternal + "^";
                                                                    lto.TestOrderName += item.ItemName + "^";
                                                                }

                                                                lto.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                                                                lto.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                                                lto.IsConfirm = false;

                                                                lto.es.Connection.Name = AppConstant.HIS_INTEROP.PAC_HIS_INTEROP_CONNECTION_NAME;
                                                                lto.Save();
                                                            }
                                                            break;
                                                        #endregion
                                                        case "RSSA_HIS_INTEROP_CONNECTION_NAME":
                                                            break;
                                                        #region RSCH_LIS_INTEROP_CONNECTION_NAME
                                                        case AppConstant.HIS_INTEROP.RSCH_LIS_INTEROP_CONNECTION_NAME:
                                                            if (entity.ToServiceUnitID == AppSession.Parameter.ServiceUnitLaboratoryID)
                                                            {
                                                                var olh = new BusinessObject.Interop.RSCH.OrderLabHeader();
                                                                olh.es.Connection.Name = AppConstant.HIS_INTEROP.RSCH_LIS_INTEROP_CONNECTION_NAME;
                                                                olh.OrderLabNo = entity.TransactionNo;
                                                                olh.OrderLabTglOrder = (new DateTime()).NowAtSqlServer().Date;
                                                                olh.OrderLabNoMR = patient.MedicalNo;
                                                                olh.OrderLabNama = ((patient.FirstName + " " + patient.MiddleName).Trim() + " " + patient.LastName).Trim();

                                                                if (!string.IsNullOrEmpty(reg.PhysicianSenders)) olh.OrderLabNamaPengirim = reg.PhysicianSenders;
                                                                else
                                                                {
                                                                    var medic = new Paramedic();
                                                                    medic.LoadByPrimaryKey(reg.ParamedicID);
                                                                    olh.OrderLabNamaPengirim = medic.ParamedicName;
                                                                }

                                                                olh.OrderLabKdPoli = entity.FromServiceUnitID;
                                                                olh.OrderLabBirthdate = patient.DateOfBirth;
                                                                olh.OrderLabAgeYear = reg.AgeInYear;
                                                                olh.OrderLabAgeMonth = reg.AgeInMonth;
                                                                olh.OrderLabAgeDay = reg.AgeInDay;
                                                                olh.OrderLabSex = patient.Sex;
                                                                olh.OrderLabKdPengirim = string.Empty;

                                                                unit = new ServiceUnit();
                                                                unit.LoadByPrimaryKey(entity.FromServiceUnitID);
                                                                olh.OrderlabNamaPoli = unit.ServiceUnitName;

                                                                olh.OrderLabJamOrder = (new DateTime()).NowAtSqlServer().ToString("HH:mm");
                                                                olh.OrderLabStatus = string.Empty;
                                                                olh.OrderLabNoBed = reg.BedID;

                                                                var guar = new Guarantor();
                                                                if (guar.LoadByPrimaryKey(reg.GuarantorID))
                                                                    olh.GuarantorName = guar.GuarantorName;

                                                                if (reg.SRRegistrationType == AppConstant.RegistrationType.OutPatient)
                                                                {
                                                                    var soap = new RegistrationInfoMedic();
                                                                    soap.Query.es.Top = 1;
                                                                    soap.Query.Where(soap.Query.RegistrationNo == reg.RegistrationNo, soap.Query.SRMedicalNotesInputType == "SOAP");
                                                                    soap.Query.OrderBy(soap.Query.RegistrationInfoMedicID.Descending);
                                                                    if (soap.Query.Load()) olh.DiagnoseText = soap.Info3;
                                                                    else olh.DiagnoseText = string.Empty;
                                                                }
                                                                else if (reg.SRRegistrationType == AppConstant.RegistrationType.InPatient)
                                                                {
                                                                    var nhd = new NursingTransHD();
                                                                    nhd.Query.es.Top = 1;
                                                                    nhd.Query.Where(nhd.Query.RegistrationNo == reg.RegistrationNo);
                                                                    if (nhd.Query.Load())
                                                                    {
                                                                        var ndt = new NursingDiagnosaTransDT();
                                                                        ndt.Query.es.Top = 1;
                                                                        ndt.Query.Where(ndt.Query.TransactionNo == nhd.TransactionNo);
                                                                        ndt.Query.OrderBy(ndt.Query.Priority.Ascending);
                                                                        if (ndt.Query.Load()) olh.DiagnoseText = ndt.NursingDiagnosaName;
                                                                        else olh.DiagnoseText = string.Empty;
                                                                    }
                                                                    else olh.DiagnoseText = string.Empty;
                                                                }

                                                                olh.Save();

                                                                var details = new BusinessObject.Interop.RSCH.OrderLabDetailCollection();
                                                                details.es.Connection.Name = AppConstant.HIS_INTEROP.RSCH_LIS_INTEROP_CONNECTION_NAME;

                                                                foreach (var i in itemBridgings.Where(i => i.ConnectionName == group.ConnectionName))
                                                                {
                                                                    var old = details.AddNew();

                                                                    var item = new Item();
                                                                    item.LoadByPrimaryKey(i.ItemID);

                                                                    old.OrderLabNo = entity.TransactionNo;
                                                                    old.OrderLabTglOrder = olh.OrderLabTglOrder;
                                                                    old.CheckupResultTestCode = i.ItemIdExternal;
                                                                    old.OrderLabJamOrder = olh.OrderLabJamOrder;
                                                                    old.OrderLabStatus = string.Empty;

                                                                    var isCito = false;
                                                                    foreach (var findCito in transChargesItems.Where(findCito => findCito.ItemID.Equals(i.ItemID) && findCito.IsCito == true))
                                                                    {
                                                                        isCito = true;
                                                                        break;
                                                                    }

                                                                    old.OrderLabCito = isCito ? "C" : string.Empty;
                                                                }

                                                                if (details.Any()) details.Save();
                                                            }
                                                            break;
                                                        #endregion
                                                        #region SYSMEX_LIS_INTEROP_CONNECTION_NAME
                                                        case AppConstant.HIS_INTEROP.VANSLAB_LIS_INTEROP_CONNECTION_NAME:
                                                        case AppConstant.HIS_INTEROP.SYSMEX_LIS_INTEROP_CONNECTION_NAME:
                                                            if (entity.ToServiceUnitID == AppSession.Parameter.ServiceUnitLaboratoryID)
                                                            {
                                                                var lo = new BusinessObject.Interop.SYSMEX.LisOrder();
                                                                if (AppSession.Parameter.HisInteropConfigName == AppConstant.HIS_INTEROP.VANSLAB_LIS_INTEROP_CONNECTION_NAME) lo.es.Connection.Name = AppConstant.HIS_INTEROP.VANSLAB_LIS_INTEROP_CONNECTION_NAME;
                                                                else lo.es.Connection.Name = AppConstant.HIS_INTEROP.SYSMEX_LIS_INTEROP_CONNECTION_NAME;

                                                                lo.MessageDt = (entity.LastUpdateDateTime ?? (new DateTime()).NowAtSqlServer()).ToString("yyyyMMddHHmmss");
                                                                lo.OrderControl = "NW";
                                                                if (AppSession.Parameter.HealthcareInitialAppsVersion == "GRHA") lo.Pid = string.IsNullOrEmpty(patient.MedicalNo) ? patient.PatientID : patient.MedicalNo;
                                                                else lo.Pid = patient.MedicalNo;
                                                                lo.Pname = patient.PatientName;
                                                                lo.Address1 = patient.StreetName.Trim();

                                                                switch (AppSession.Parameter.HealthcareInitialAppsVersion)
                                                                {
                                                                    case "RSTJ":
                                                                        lo.Pname = (patient.PatientName + " " + salutation).Trim();
                                                                        lo.Address2 = patient.District;
                                                                        lo.Address3 = patient.County;
                                                                        lo.Address4 = patient.State;
                                                                        break;

                                                                    case "RSUTAMA":
                                                                    case "KLUTAMA":
                                                                        lo.Address2 = patient.District;
                                                                        lo.Address3 = patient.County + " " + patient.State;
                                                                        lo.Address4 = patient.MobilePhoneNo;
                                                                        break;

                                                                    case "RSMP":
                                                                    case "GRHA":
                                                                        var refral = new Referral();
                                                                        refral.LoadByPrimaryKey(reg.ReferralID);

                                                                        lo.Address2 = patient.District;
                                                                        lo.Address3 = string.IsNullOrEmpty(reg.ReferralID) ? reg.ReferralName : refral.ReferralName;
                                                                        lo.Address4 = grr.GuarantorName;
                                                                        break;
                                                                    case "RSSMCB":
                                                                        lo.Address2 = grr.GuarantorName;
                                                                        lo.Address3 = patient.District.Trim() + " " + patient.County.Trim();
                                                                        if (AppSession.Parameter.IsUsingHisInterop)
                                                                        {
                                                                            lo.Address4 = patient.MobilePhoneNo;
                                                                        }
                                                                        else
                                                                        {
                                                                            if (AppSession.Parameter.HealthcareInitial == "RSSMHB")
                                                                            {
                                                                                lo.Address3 += " " + patient.State.Trim();

                                                                                var mb = new MergeBilling();
                                                                                if (mb.LoadByPrimaryKey(reg.RegistrationNo))
                                                                                {
                                                                                    if (!string.IsNullOrEmpty(mb.FromRegistrationNo))
                                                                                    {
                                                                                        var freg = new Registration();
                                                                                        freg.LoadByPrimaryKey(mb.FromRegistrationNo);
                                                                                        var funit = new ServiceUnit();
                                                                                        funit.LoadByPrimaryKey(freg.ServiceUnitID);
                                                                                        lo.Address4 = funit.ServiceUnitID + "^" + funit.ServiceUnitName;
                                                                                    }
                                                                                    else lo.Address4 = unit.ServiceUnitID + "^" + unit.ServiceUnitName; ;
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                lo.Address4 = patient.State;
                                                                            }
                                                                        }

                                                                        break;
                                                                    case "YBRSGKP":
                                                                        lo.Address2 = "";
                                                                        switch (grr.SRGuarantorType)
                                                                        {
                                                                            case "09":
                                                                                lo.Address2 = "BPJS";
                                                                                break;
                                                                            case "00":
                                                                                lo.Address2 = "PRIBADI";
                                                                                break;
                                                                            default:
                                                                                lo.Address2 = "MITRA";
                                                                                break;

                                                                        }

                                                                        lo.Address3 = grr.GuarantorName;
                                                                        lo.Address4 = ClassID;

                                                                        var cls = new Class();
                                                                        cls.LoadByPrimaryKey(ClassID);
                                                                        lo.Address4 = reg.SRRegistrationType == "IPR" ? cls.ClassName : string.Empty;
                                                                        break;
                                                                    default:
                                                                        lo.Pname = patient.PatientName;
                                                                        lo.Address2 = patient.District;
                                                                        lo.Address3 = patient.County;
                                                                        lo.Address4 = patient.State;
                                                                        break;
                                                                }

                                                                lo.Ptype = reg.SRRegistrationType == AppConstant.RegistrationType.InPatient ? "IN" : "OP";
                                                                lo.BirthDt = (patient.DateOfBirth ?? (new DateTime()).NowAtSqlServer()).ToString("yyyyMMddHHmmss");
                                                                lo.Sex = patient.Sex == "M" ? "1" : "0";
                                                                lo.Ono = entity.TransactionNo;
                                                                lo.RequestDt = (entity.LastUpdateDateTime ?? (new DateTime()).NowAtSqlServer()).ToString("yyyyMMddHHmmss");

                                                                //switch (AppSession.Parameter.HealthcareInitialAppsVersion)
                                                                //{
                                                                //    case "RSTJ":
                                                                //        lo.RequestDt = (entity.CreatedDateTime ?? (new DateTime()).NowAtSqlServer()).ToString("yyyyMMddHHmmss");
                                                                //        break;
                                                                //    default:
                                                                //        lo.RequestDt = (entity.LastUpdateDateTime ?? (new DateTime()).NowAtSqlServer()).ToString("yyyyMMddHHmmss");
                                                                //        break;
                                                                //}

                                                                unit = new ServiceUnit();
                                                                unit.LoadByPrimaryKey(entity.FromServiceUnitID);

                                                                lo.Source = unit.ServiceUnitID + "^" + unit.ServiceUnitName;

                                                                var param = new Paramedic();

                                                                switch (AppSession.Parameter.HealthcareInitialAppsVersion)
                                                                {
                                                                    case "RSUTAMA":
                                                                        if (!string.IsNullOrEmpty(reg.ReferralID))
                                                                        {
                                                                            var refer = new Referral();
                                                                            refer.LoadByPrimaryKey(reg.ReferralID);

                                                                            lo.Clinician = reg.ReferralID + "^" + refer.ReferralName;
                                                                        }
                                                                        else if (!string.IsNullOrEmpty(reg.PhysicianSenders))
                                                                        {
                                                                            lo.Clinician = reg.ParamedicID + "^" + reg.PhysicianSenders;
                                                                        }
                                                                        else
                                                                        {
                                                                            param.LoadByPrimaryKey(reg.ParamedicID);

                                                                            lo.Clinician = reg.ParamedicID + "^" + param.ParamedicName;
                                                                        }
                                                                        break;
                                                                    default:
                                                                        param.LoadByPrimaryKey(reg.ParamedicID);

                                                                        lo.Clinician = reg.ParamedicID + "^" + param.ParamedicName;
                                                                        break;
                                                                }

                                                                lo.RoomNo = reg.RoomID;
                                                                lo.Priority = transChargesItems.Any(t => (t.IsCito ?? false)) ? "U" : "R";

                                                                if (AppSession.Parameter.HisInteropConfigName == AppConstant.HIS_INTEROP.VANSLAB_LIS_INTEROP_CONNECTION_NAME) lo.Cmt = grr.GuarantorName;
                                                                else
                                                                {
                                                                    if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSSMCB" && AppSession.Parameter.IsUsingHisInteropToHcLab)
                                                                        lo.Cmt = patient.Ssn;
                                                                    else
                                                                        lo.Cmt = entity.Notes; //string.Empty;
                                                                }

                                                                lo.Visitno = entity.RegistrationNo;

                                                                var prefixCode = AppSession.Parameter.PrefixOnoSysmexInterop;
                                                                if (!string.IsNullOrWhiteSpace(prefixCode))
                                                                {
                                                                    lo.HealthcareCode = prefixCode;
                                                                    lo.Pid = prefixCode + lo.Pid;
                                                                    lo.Ono = prefixCode + lo.Ono;
                                                                    lo.Source = prefixCode + lo.Source;
                                                                    lo.Clinician = prefixCode + lo.Clinician;
                                                                }

                                                                //switch (AppSession.Parameter.HealthcareInitialAppsVersion)
                                                                //{
                                                                //    case "RSUI":
                                                                //        lo.HealthcareCode = "TG";
                                                                //        lo.Pid = "TG" + lo.Pid;
                                                                //        lo.Ono = "TG" + lo.Ono;
                                                                //        lo.Source = "TG" + lo.Source;
                                                                //        lo.Clinician = "TG" + lo.Clinician;
                                                                //        break;
                                                                //    case "RSPM":
                                                                //        lo.HealthcareCode = "ST";
                                                                //        lo.Pid = "ST" + lo.Pid;
                                                                //        lo.Ono = "ST" + lo.Ono;
                                                                //        lo.Source = "ST" + lo.Source;
                                                                //        lo.Clinician = "ST" + lo.Clinician;
                                                                //        break;
                                                                //}

                                                                foreach (var i in itemBridgings.Where(i => i.ConnectionName == group.ConnectionName))
                                                                {
                                                                    if (AppSession.Parameter.HisInteropConfigName == AppConstant.HIS_INTEROP.VANSLAB_LIS_INTEROP_CONNECTION_NAME)
                                                                        lo.OrderTestid += i.ItemID + "~";
                                                                    else
                                                                        lo.OrderTestid += i.ItemIdExternal + "~";
                                                                }

                                                                lo.Save();
                                                            }
                                                            break;
                                                        #endregion
                                                        #region PRODIA_LIS_INTEROP_CONNECTION_NAME
                                                        case AppConstant.HIS_INTEROP.PRODIA_LIS_INTEROP_CONNECTION_NAME:
                                                            break;
                                                        #endregion
                                                        #region ELIMS_LIS_INTEROP_CONNECTION_NAME
                                                        case AppConstant.HIS_INTEROP.ELIMS_LIS_INTEROP_CONNECTION_NAME:
                                                            if (entity.ToServiceUnitID == AppSession.Parameter.ServiceUnitLaboratoryID || AppSession.Parameter.ServiceUnitLaboratoryIdArray.Contains(entity.ToServiceUnitID))
                                                            {

                                                                //var kl = new BusinessObject.Interop.ELIMS.KirimLIS();
                                                                //kl.es.Connection.Name = AppConstant.HIS_INTEROP.ELIMS_LIS_INTEROP_CONNECTION_NAME;

                                                                var kls = new BusinessObject.Interop.ELIMS.KirimLISCollection();

                                                                switch (AppSession.Parameter.HealthcareInitialAppsVersion)
                                                                {
                                                                    case "RSISB":
                                                                        {
                                                                            foreach (var detail in transChargesItems.Where(t => t.IsOrderRealization ?? false))
                                                                            {
                                                                                var kl = kls.AddNew();
                                                                                kl.ModifiedDate = entity.ExecutionDate;
                                                                                kl.NoPasien = patient.MedicalNo;
                                                                                kl.KodeKunjungan = entity.TransactionNo;
                                                                                kl.Nama = patient.PatientName + " " + apstd.ItemName;
                                                                                kl.Email = patient.Email;
                                                                                kl.DateOfBirth = patient.DateOfBirth;
                                                                                kl.UmurTahun = reg.AgeInYear;
                                                                                kl.UmurBulan = reg.AgeInMonth;
                                                                                kl.UmurHari = reg.AgeInDay;
                                                                                kl.Gender = patient.Sex == "M" ? "L" : "P";
                                                                                kl.Alamat = patient.Address;
                                                                                kl.Diagnosa = reg.Anamnesis;
                                                                                kl.TglPeriksa = entity.ExecutionDate;
                                                                                kl.Pengirim = reg.ParamedicID;

                                                                                if (!string.IsNullOrEmpty(reg.PhysicianSenders)) kl.PengirimName = reg.PhysicianSenders;
                                                                                else
                                                                                {
                                                                                    var medic = new Paramedic();
                                                                                    medic.LoadByPrimaryKey(reg.ParamedicID);
                                                                                    kl.PengirimName = medic.ParamedicName;
                                                                                }

                                                                                var cls = new Class();
                                                                                cls.LoadByPrimaryKey(entity.ClassID);
                                                                                kl.Kelas = cls.ClassID;
                                                                                kl.KelasName = cls.ClassID;

                                                                                var room = new ServiceRoom();
                                                                                room.LoadByPrimaryKey(entity.RoomID);
                                                                                kl.Ruang = room.RoomID;
                                                                                kl.RuangName = room.RoomName;

                                                                                kl.CaraBayar = grr.GuarantorID;
                                                                                kl.CaraBayarName = grr.GuarantorName;



                                                                                kl.KodeTarif = detail.ItemID;
                                                                                kl.Update = detail.IsCorrection == false ? "N" : "D";


                                                                                kl.ISInap = reg.SRRegistrationType == "IPR" ? "1" : "0";
                                                                                kl.Status = "1";

                                                                                kl.NIK = patient.Ssn;



                                                                                kl.IS_CITO = detail.IsCito == false ? "0" : "1";



                                                                            }

                                                                        }
                                                                        break;
                                                                    default:
                                                                        foreach (var detail in transChargesItems.Where(t => t.IsOrderRealization ?? false))
                                                                        {
                                                                            var kl = kls.AddNew();
                                                                            kl.ModifiedDate = entity.ExecutionDate;
                                                                            kl.NoPasien = patient.MedicalNo;
                                                                            kl.KodeKunjungan = entity.TransactionNo;
                                                                            kl.Nama = patient.PatientName + " " + apstd.ItemName;
                                                                            kl.Email = patient.Email;
                                                                            kl.DateOfBirth = patient.DateOfBirth;
                                                                            kl.UmurTahun = reg.AgeInYear;
                                                                            kl.UmurBulan = reg.AgeInMonth;
                                                                            kl.UmurHari = reg.AgeInDay;
                                                                            kl.Gender = patient.Sex == "M" ? "L" : "P";
                                                                            kl.Alamat = patient.Address;
                                                                            kl.Diagnosa = reg.Anamnesis;
                                                                            kl.TglPeriksa = entity.ExecutionDate;
                                                                            kl.Pengirim = reg.ParamedicID;

                                                                            if (!string.IsNullOrEmpty(reg.PhysicianSenders)) kl.PengirimName = reg.PhysicianSenders;
                                                                            else
                                                                            {
                                                                                var medic = new Paramedic();
                                                                                medic.LoadByPrimaryKey(reg.ParamedicID);
                                                                                kl.PengirimName = medic.ParamedicName;
                                                                            }

                                                                            var cls = new Class();
                                                                            cls.LoadByPrimaryKey(entity.ClassID);
                                                                            kl.Kelas = cls.ClassID;
                                                                            kl.KelasName = cls.ClassID;

                                                                            var room = new ServiceRoom();
                                                                            room.LoadByPrimaryKey(entity.RoomID);
                                                                            kl.Ruang = room.RoomID;
                                                                            kl.RuangName = room.RoomName;

                                                                            kl.CaraBayar = grr.GuarantorID;
                                                                            kl.CaraBayarName = grr.GuarantorName;



                                                                            kl.KodeTarif = detail.ItemID;
                                                                            kl.Update = detail.IsCorrection == false ? "N" : "D";


                                                                            kl.ISInap = reg.SRRegistrationType == "IPR" ? "1" : "0";
                                                                            kl.Status = "1";

                                                                        }


                                                                        break;
                                                                }

                                                                kls.es.Connection.Name = AppConstant.HIS_INTEROP.ELIMS_LIS_INTEROP_CONNECTION_NAME;
                                                                kls.Save();

                                                            }
                                                            break;
                                                        #endregion
                                                        #region WYNAKOM_LIS_INTEROP_CONNECTION_NAME
                                                        case AppConstant.HIS_INTEROP.WYNAKOM_LIS_INTEROP_CONNECTION_NAME:
                                                            if (entity.ToServiceUnitID == AppSession.Parameter.ServiceUnitLaboratoryID)
                                                            {
                                                                var demographics = new BusinessObject.Interop.Wynakom.Demographics();
                                                                demographics.es.Connection.Name = AppConstant.HIS_INTEROP.WYNAKOM_LIS_INTEROP_CONNECTION_NAME;

                                                                if (!demographics.LoadByPrimaryKey(string.IsNullOrEmpty(patient.MedicalNo) ? patient.PatientID : patient.MedicalNo)) demographics = new BusinessObject.Interop.Wynakom.Demographics();
                                                                demographics.PatientId = string.IsNullOrEmpty(patient.MedicalNo) ? patient.PatientID : patient.MedicalNo;
                                                                demographics.GenderId = patient.Sex;
                                                                demographics.DateOfBirth = patient.DateOfBirth;

                                                                var std = new AppStandardReferenceItem();
                                                                if (std.LoadByPrimaryKey(AppEnum.StandardReference.Salutation.ToString(), patient.SRSalutation))
                                                                    demographics.PatientName = string.Format("{0} {1}", std.ItemName, patient.PatientName);
                                                                else
                                                                    demographics.PatientName = string.Format("{0}", patient.PatientName);//string.Format("{0} {1}", string.Empty, patient.PatientName);

                                                                demographics.PatientAddress = patient.Address;
                                                                demographics.CityName = patient.City;
                                                                demographics.PhoneNumber = patient.PhoneNo;
                                                                demographics.FaxNumber = patient.FaxNo;
                                                                demographics.MobileNumber = patient.MobilePhoneNo;
                                                                demographics.Email = patient.Email;
                                                                demographics.KtpNumber = patient.Ssn;

                                                                var registration = new BusinessObject.Interop.Wynakom.Registration();
                                                                registration.es.Connection.Name = AppConstant.HIS_INTEROP.WYNAKOM_LIS_INTEROP_CONNECTION_NAME;

                                                                registration.PatientId = demographics.PatientId;
                                                                registration.VisitNumber = reg.RegistrationNo;
                                                                registration.OrderNumber = entity.TransactionNo;
                                                                registration.OrderDateTime = entity.ExecutionDate;
                                                                registration.DiagnoseName = "";
                                                                registration.Cito = transChargesItems.Any(t => t.IsCito ?? false);
                                                                registration.ServiceUnitID = unit.ServiceUnitID;
                                                                registration.ServiceUnitName = unit.ServiceUnitName;
                                                                registration.GuarantorID = grr.GuarantorID;
                                                                registration.GuarantorName = grr.GuarantorName;

                                                                var pmedic = new Paramedic();
                                                                pmedic.LoadByPrimaryKey(reg.ParamedicID);
                                                                registration.DoctorID = pmedic.ParamedicID;
                                                                registration.DoctorName = pmedic.ParamedicName;

                                                                var cls = new Class();
                                                                cls.LoadByPrimaryKey(entity.ClassID);
                                                                registration.ClassID = cls.ClassID;
                                                                registration.ClassName = cls.ClassName;
                                                                registration.AgreementID = cls.ClassID;
                                                                registration.AgreementName = cls.ClassName;

                                                                registration.WardID = unit.ServiceUnitID;
                                                                registration.WardName = unit.ServiceUnitName;

                                                                var room = new ServiceRoom();
                                                                room.LoadByPrimaryKey(entity.RoomID);
                                                                registration.RoomID = room.RoomID;
                                                                registration.RoomName = room.RoomName;

                                                                registration.BedID = entity.BedID;
                                                                registration.BedName = entity.BedID;
                                                                registration.RegUserName = AppSession.UserLogin.UserName;
                                                                registration.Notes = reg.Notes;

                                                                var ordered_item = new BusinessObject.Interop.Wynakom.OrderedItems();
                                                                ordered_item.es.Connection.Name = AppConstant.HIS_INTEROP.WYNAKOM_LIS_INTEROP_CONNECTION_NAME;
                                                                ordered_item.OrderNumber = registration.OrderNumber;
                                                                ordered_item.OrderItemDate = registration.OrderDateTime;

                                                                bool isInsert = false;
                                                                foreach (var i in itemBridgings.Where(i => i.ConnectionName == group.ConnectionName))
                                                                {
                                                                    var item = new Item();
                                                                    item.LoadByPrimaryKey(i.ItemID);

                                                                    //if (!string.IsNullOrEmpty(item.ItemIDExternal))
                                                                    //{
                                                                    //    if (AppSession.Parameter.HealthcareInitialAppsVersion == "YBRSGKP")
                                                                    //    {
                                                                    //        if (AppSession.Parameter.HealthcareInitial == "RSI")
                                                                    //        {
                                                                    //            var pa = new BusinessObject.Interop.ROCHE.PocAdt();
                                                                    //            pa.es.Connection.Name = AppConstant.HIS_INTEROP.ROCHE_LIS_INTEROP_CONNECTION_NAME;
                                                                    //            pa.Action = "ADD";
                                                                    //            pa.Pid = patient.MedicalNo;
                                                                    //            pa.Dob = patient.DateOfBirth;
                                                                    //            pa.Sex = patient.Sex;
                                                                    //            pa.VisitNum = entity.TransactionNo;

                                                                    //            var pc = string.Empty;
                                                                    //            if (reg.SRRegistrationType == AppConstant.RegistrationType.InPatient) pc = "1";
                                                                    //            else if (reg.SRRegistrationType == AppConstant.RegistrationType.OutPatient) pc = "2";
                                                                    //            else if (reg.SRRegistrationType == AppConstant.RegistrationType.EmergencyPatient) pc = "3";
                                                                    //            else pc = "2";
                                                                    //            pa.PatientClass = pc;

                                                                    //            var fUnit = new ServiceUnit();
                                                                    //            fUnit.LoadByPrimaryKey(entity.FromServiceUnitID);

                                                                    //            pa.LocationCode = fUnit.ServiceUnitID;
                                                                    //            pa.LocationName = fUnit.ServiceUnitName;
                                                                    //            pa.DoctorCode = pmedic.ParamedicID;
                                                                    //            pa.DoctorName = pmedic.ParamedicName;
                                                                    //            pa.AdmitDt = entity.ExecutionDate;
                                                                    //            pa.PatientName = patient.PatientName;

                                                                    //            var fRoom = new ServiceRoom();
                                                                    //            fRoom.LoadByPrimaryKey(reg.RoomID);

                                                                    //            pa.RoomCode = fRoom.RoomID;
                                                                    //            pa.RoomName = fRoom.RoomName;

                                                                    //            pa.Save();
                                                                    //        }
                                                                    //    }
                                                                    //}

                                                                    //ordered_item.OrderItemID += (string.IsNullOrEmpty(item.ItemIDExternal) ? item.ItemID : item.ItemIDExternal) + "~";
                                                                    ordered_item.OrderItemID += item.ItemID + "~";
                                                                    ordered_item.OrderItemName += item.ItemName + "~";
                                                                    isInsert = true;
                                                                }

                                                                if (isInsert)
                                                                {
                                                                    if (!string.IsNullOrEmpty(ordered_item.OrderItemID) && !string.IsNullOrEmpty(ordered_item.OrderItemName))
                                                                    {
                                                                        ordered_item.OrderItemID = ordered_item.OrderItemID.Remove(ordered_item.OrderItemID.Length - 1);
                                                                        ordered_item.OrderItemName = ordered_item.OrderItemName.Remove(ordered_item.OrderItemName.Length - 1);
                                                                    }

                                                                    demographics.es.Connection.Name = AppConstant.HIS_INTEROP.WYNAKOM_LIS_INTEROP_CONNECTION_NAME;
                                                                    demographics.Save();

                                                                    registration.es.Connection.Name = AppConstant.HIS_INTEROP.WYNAKOM_LIS_INTEROP_CONNECTION_NAME;
                                                                    registration.Save();

                                                                    ordered_item.es.Connection.Name = AppConstant.HIS_INTEROP.WYNAKOM_LIS_INTEROP_CONNECTION_NAME;
                                                                    ordered_item.Save();
                                                                }
                                                            }
                                                            break;
                                                        #endregion
                                                        #region ROCHE_LIS_INTEROP_CONNECTION_NAME
                                                        case AppConstant.HIS_INTEROP.ROCHE_LIS_INTEROP_CONNECTION_NAME:
                                                            if (entity.ToServiceUnitID == AppSession.Parameter.ServiceUnitLaboratoryID)
                                                            {
                                                                var pmedic = new Paramedic();
                                                                pmedic.LoadByPrimaryKey(reg.ParamedicID);

                                                                foreach (var i in itemBridgings.Where(i => i.ConnectionName == group.ConnectionName))
                                                                {
                                                                    var pa = new BusinessObject.Interop.ROCHE.PocAdt();
                                                                    pa.es.Connection.Name = AppConstant.HIS_INTEROP.ROCHE_LIS_INTEROP_CONNECTION_NAME;
                                                                    pa.Action = "ADD";
                                                                    pa.Pid = patient.MedicalNo;
                                                                    pa.Dob = patient.DateOfBirth;
                                                                    pa.Sex = patient.Sex;
                                                                    pa.VisitNum = entity.TransactionNo;

                                                                    var pc = string.Empty;
                                                                    if (reg.SRRegistrationType == AppConstant.RegistrationType.InPatient) pc = "1";
                                                                    else if (reg.SRRegistrationType == AppConstant.RegistrationType.OutPatient) pc = "2";
                                                                    else if (reg.SRRegistrationType == AppConstant.RegistrationType.EmergencyPatient) pc = "3";
                                                                    else pc = "2";
                                                                    pa.PatientClass = pc;

                                                                    var fUnit = new ServiceUnit();
                                                                    fUnit.LoadByPrimaryKey(entity.FromServiceUnitID);

                                                                    pa.LocationCode = fUnit.ServiceUnitID;
                                                                    pa.LocationName = fUnit.ServiceUnitName;
                                                                    pa.DoctorCode = pmedic.ParamedicID;
                                                                    pa.DoctorName = pmedic.ParamedicName;
                                                                    pa.AdmitDt = entity.ExecutionDate;
                                                                    pa.PatientName = patient.PatientName;

                                                                    var fRoom = new ServiceRoom();
                                                                    fRoom.LoadByPrimaryKey(reg.RoomID);

                                                                    pa.RoomCode = fRoom.RoomID;
                                                                    pa.RoomName = fRoom.RoomName;

                                                                    pa.Save();
                                                                }
                                                            }
                                                            break;
                                                        #endregion
                                                        #region LINK_LIS_INTEROP_CONNECTION_NAME
                                                        case AppConstant.HIS_INTEROP.LINK_LIS_INTEROP_CONNECTION_NAME:
                                                            var id_ruangan = new ServiceUnitBridging();
                                                            id_ruangan.Query.Where(id_ruangan.Query.ServiceUnitID == reg.ServiceUnitID, id_ruangan.Query.SRBridgingType == AppEnum.BridgingType.LINK_LIS.ToString());
                                                            if (!id_ruangan.Query.Load())
                                                            {
                                                                var ruangan = new ServiceUnit();
                                                                ruangan.LoadByPrimaryKey(reg.ServiceUnitID);
                                                                return "Service Unit : " + ruangan.ServiceUnitName + " not mapped to Link Lis.";
                                                            }

                                                            var id_dokter = new ParamedicBridging();
                                                            id_dokter.Query.Where(id_dokter.Query.ParamedicID == reg.ParamedicID, id_dokter.Query.SRBridgingType == AppEnum.BridgingType.LINK_LIS.ToString());
                                                            if (!id_dokter.Query.Load())
                                                            {
                                                                var ruangan = new Paramedic();
                                                                ruangan.LoadByPrimaryKey(reg.ParamedicID);
                                                                return "Physician : " + ruangan.ParamedicName + " not mapped to Link Lis.";

                                                            }

                                                            var id_status = new GuarantorBridging();
                                                            id_status.Query.Where(id_status.Query.GuarantorID == reg.GuarantorID, id_status.Query.SRBridgingType == AppEnum.BridgingType.LINK_LIS.ToString());
                                                            if (!id_status.Query.Load())
                                                            {
                                                                var ruangan = new Guarantor();
                                                                ruangan.LoadByPrimaryKey(reg.GuarantorID);
                                                                return "Guarantor : " + ruangan.GuarantorName + " not mapped to Link Lis.";
                                                            }

                                                            if (itemBridgings.Any(t => t.ConnectionName == group.ConnectionName))
                                                            {
                                                                var svc = new Common.LinkLis.Service();
                                                                var response = svc.InsertRegistrasiPasien(new Common.LinkLis.Object.RegistrasiPasien()
                                                                {
                                                                    no_rm = pat.MedicalNo,
                                                                    nama = pat.PatientName,
                                                                    alamat = pat.Address,
                                                                    tgl_lahir = pat.DateOfBirth.Value.ToString("dd-MM-yyyy"),
                                                                    jenis_kelamin = pat.Sex == "F" ? "Perempuan" : "Laki-laki",
                                                                    status = id_status.BridgingID
                                                                }, true);

                                                                svc = new Common.LinkLis.Service();
                                                                var kode = svc.GetKodePemeriksaan();

                                                                svc = new Common.LinkLis.Service();
                                                                response = svc.InsertRegistrasiPemeriksaan(new Common.LinkLis.Object.RegistrasiPemeriksaan()
                                                                {
                                                                    kode_pemeriksaan = kode.KodePemeriksaan,
                                                                    no_rm = pat.MedicalNo,
                                                                    id_ruangan = id_ruangan.BridgingID,
                                                                    id_dokter = id_dokter.BridgingID.Split('-')[1],
                                                                    id_analis = entity.AnalystID.Split('-')[1],
                                                                    id_dokterpk = entity.LaboratoryParamedicID.Split('-')[1],
                                                                    id_status = id_status.BridgingID
                                                                }, true);

                                                                var list_pemeriksaan = new List<Common.LinkLis.Object.ListPemeriksaan>();
                                                                var list_parameter = new List<Common.LinkLis.Object.ListParameter>();
                                                                foreach (var i in itemBridgings.Where(i => i.ConnectionName == group.ConnectionName))
                                                                {
                                                                    if (!list_pemeriksaan.Any(p => p.list_pemeriksaan == i.BridgingGroupID)) list_pemeriksaan.Add(new Common.LinkLis.Object.ListPemeriksaan() { list_pemeriksaan = i.BridgingGroupID });
                                                                    list_parameter.Add(new Common.LinkLis.Object.ListParameter() { list_pemeriksaan = i.BridgingGroupID, list_parameter = i.BridgingID });

                                                                    foreach (var detail in transChargesItems.Where(t => (t.IsOrderRealization ?? false) && t.ItemID == i.ItemID))
                                                                    {
                                                                        detail.ResultValue = kode.KodePemeriksaan;
                                                                    }
                                                                }

                                                                svc = new Common.LinkLis.Service();
                                                                response = svc.InsertRegistrasiParameterPemeriksaan(new Common.LinkLis.Object.ParameterPemeriksaan()
                                                                {
                                                                    kode_pemeriksaan = kode.KodePemeriksaan,
                                                                    list_pemeriksaan = list_pemeriksaan,
                                                                    list_parameter = list_parameter
                                                                });

                                                                transChargesItems.Save();
                                                            }

                                                            break;
                                                        #endregion
                                                        #region VANSLITE_LIS_INTEROP_CONNECTION_NAME
                                                        case AppConstant.HIS_INTEROP.VANSLITE_LIS_INTEROP_CONNECTION_NAME:
                                                            {
                                                                var pmedic = new Paramedic();
                                                                pmedic.LoadByPrimaryKey(reg.ParamedicID);

                                                                var tunit = new ServiceUnit();
                                                                tunit.LoadByPrimaryKey(entity.ToServiceUnitID);

                                                                var vlites = new BusinessObject.Interop.VANSLITE.OrderLabCollection();

                                                                foreach (var i in itemBridgings.Where(i => i.ConnectionName == group.ConnectionName))
                                                                {
                                                                    var seqNo = string.Empty;
                                                                    var parId = string.Empty;
                                                                    decimal price = 0;
                                                                    decimal qty = 0;
                                                                    bool isCito = false;
                                                                    foreach (var detail in transChargesItems.Where(t => (t.IsOrderRealization ?? false) && t.ItemID == i.ItemID))
                                                                    {
                                                                        seqNo = detail.SequenceNo;
                                                                        price = detail.Price ?? 0;
                                                                        qty = detail.ChargeQuantity ?? 0;
                                                                        isCito = detail.IsCito ?? false;
                                                                        parId = string.IsNullOrEmpty(detail.ParamedicID) ? string.Empty : detail.ParamedicID;

                                                                        detail.IsSendToLIS = true;

                                                                        break;
                                                                    }

                                                                    var vlite = vlites.AddNew();
                                                                    vlite.AsalLab = tunit.ServiceUnitName;
                                                                    vlite.NoLab = entity.TransactionNo;
                                                                    vlite.NoLabDtl = seqNo;
                                                                    vlite.NoRegistrasi = reg.RegistrationNo;
                                                                    vlite.NoRm = patient.MedicalNo;
                                                                    vlite.TglOrder = Convert.ToDateTime(entity.ExecutionDate.Value.ToString("MM/dd/yyyy"));
                                                                    vlite.NamaPas = patient.PatientName;
                                                                    vlite.JenisKel = patient.Sex == "M" ? "L" : "P";
                                                                    vlite.TglLahir = patient.DateOfBirth;
                                                                    vlite.Usia = Helper.GetAgeInYear(patient.DateOfBirth.Value, entity.ExecutionDate.Value).ToString();
                                                                    vlite.Alamat = patient.Address;
                                                                    vlite.KodeDokKirim = reg.ParamedicID;
                                                                    vlite.NamaDokKirim = pmedic.ParamedicName;
                                                                    vlite.KodeRuang = entity.FromServiceUnitID;
                                                                    vlite.NamaRuang = unit.ServiceUnitName;
                                                                    vlite.KodeCaraBayar = reg.GuarantorID;
                                                                    vlite.CaraBayar = grr.GuarantorName;
                                                                    vlite.KetKlinis = reg.Complaint;
                                                                    vlite.KodeTest = i.ItemID;
                                                                    var item = new Item();
                                                                    item.LoadByPrimaryKey(i.ItemID);
                                                                    vlite.Test = item.ItemName;
                                                                    vlite.Harga = price.ToInt();
                                                                    vlite.WaktuKirim = Convert.ToDateTime(entity.ExecutionDate.Value.ToString("HH:mm:ss"));
                                                                    vlite.Prioritas = isCito ? "C" : "R";
                                                                    vlite.JnsRawat = reg.SRRegistrationType == "IPR" ? "Rawat Inap" : "Rawat Jalan";
                                                                    if (string.IsNullOrWhiteSpace(parId)) vlite.DokJaga = "";
                                                                    else
                                                                    {
                                                                        var tmedic = new Paramedic();
                                                                        tmedic.LoadByPrimaryKey(parId);
                                                                        vlite.DokJaga = tmedic.ParamedicName;
                                                                    }
                                                                    vlite.Status = 0;
                                                                    vlite.Batal = 0;
                                                                    vlite.JumlahTest = qty.ToInt();
                                                                }

                                                                transChargesItems.Save();

                                                                vlites.es.Connection.Name = AppConstant.HIS_INTEROP.VANSLITE_LIS_INTEROP_CONNECTION_NAME;
                                                                vlites.Save();
                                                            }
                                                            break;
                                                            #endregion
                                                    }
                                                }
                                                #endregion
                                            }
                                        }
                                    }
                                    #endregion

                                    #region Interop RIS/PACS
                                    if (AppSession.Parameter.IsUsingRisPacsInterop)
                                    {
                                        //interop radiologi
                                        if (entity.ToServiceUnitID == AppSession.Parameter.ServiceUnitRadiologyID || entity.ToServiceUnitID == AppSession.Parameter.ServiceUnitRadiologyID2)
                                        {
                                            var patient = new Patient();
                                            patient.LoadByPrimaryKey(reg.PatientID);

                                            switch (AppSession.Parameter.HealthcareInitialAppsVersion)
                                            {
                                                case "GRHA":
                                                    {
                                                        var svc = new Common.Worklist.MM2100.Service();
                                                        svc.NewWorklist(entity.TransactionNo, transChargesItems);
                                                        break;
                                                    }
                                                case "YBRSGKP":
                                                    {
                                                        if (AppSession.Parameter.HealthcareInitial == "RSI")
                                                        {
                                                            foreach (var detail in transChargesItems.Where(t => t.IsOrderRealization ?? false))
                                                            {
                                                                var root = new Common.Worklist.RSI.Json.Order.Root();

                                                                var orderno = string.Empty;
                                                                foreach (var c in detail.TransactionNo.ToCharArray())
                                                                {
                                                                    if (!int.TryParse(c.ToString(), out int number)) continue;
                                                                    if (number == 0) continue;
                                                                    orderno += number.ToString();
                                                                }

                                                                root.acc = orderno;

                                                                orderno += detail.SequenceNo.ToInt().ToString();
                                                                foreach (var c in detail.ItemID.ToCharArray())
                                                                {
                                                                    if (!int.TryParse(c.ToString(), out int number)) continue;
                                                                    if (number == 0) continue;
                                                                    orderno += number.ToString();
                                                                }

                                                                var irad = new Item();
                                                                irad.LoadByPrimaryKey(detail.ItemID);
                                                                if (string.IsNullOrEmpty(irad.ItemIDExternal)) continue;

                                                                root.uid = string.Format("{0}.{1}.{2}{3}.{4}", "1.2.40.0.13.1", patient.MedicalNo.ToInt().ToString(), entity.ExecutionDate.Value.ToString("yyMd"), entity.ExecutionDate.Value.ToString("Hms"), orderno);
                                                                //if (string.IsNullOrEmpty(entity.FilmNo))
                                                                //{
                                                                //    _filmNo = Helper.GetNewAutoNumber(DateTime.Now.Date, AppEnum.AutoNumber.RadiologyFilmNo, irad.ItemIDExternal, string.Empty, AppSession.UserLogin.UserID);
                                                                //    var filmNo = _filmNo.LastCompleteNumber;
                                                                //    root.patientid = filmNo;
                                                                //    _filmNo.Save();
                                                                //}
                                                                //else 
                                                                root.patientid = detail.FilmNo;
                                                                root.mrn = patient.MedicalNo;
                                                                root.name = patient.PatientName;
                                                                root.address = patient.Address;
                                                                root.sex = patient.Sex;
                                                                root.birth_date = patient.DateOfBirth.Value.ToString("yyyyMMdd");
                                                                root.weight = "null";

                                                                var unitf = new ServiceUnit();
                                                                unitf.LoadByPrimaryKey(entity.FromServiceUnitID);
                                                                root.name_dep = unitf.ServiceUnitName;

                                                                root.xray_type_code = irad.ItemIDExternal;
                                                                root.typename = string.Empty;
                                                                root.prosedur = irad.ItemName;

                                                                var drp = new Paramedic();
                                                                drp.LoadByPrimaryKey(reg.ParamedicID);
                                                                root.dokterid = reg.ParamedicID;
                                                                root.named = drp.ParamedicName;

                                                                var drr = new Paramedic();
                                                                drr.LoadByPrimaryKey(detail.ParamedicID);
                                                                root.dokradid = detail.ParamedicID;
                                                                root.dokrad_name = drr.ParamedicName;
                                                                root.create_time = string.Format("{0}{1}00", entity.ExecutionDate.Value.ToString("yyyyMMdd"), entity.ExecutionDate.Value.ToString("HH:mm:ss").Replace(":", string.Empty));

                                                                var date = entity.ExecutionDate.Value.AddMinutes(10);
                                                                root.schedule_date = date.ToString("yyyyMMdd");
                                                                root.schedule_time = date.ToString("HH:mm:ss").Replace(":", string.Empty);

                                                                root.priority = (detail.IsCito ?? false) ? "CITO" : "NORMAL";
                                                                root.pat_state = reg.SRRegistrationType == AppConstant.RegistrationType.InPatient ? "RAWAT INAP" :
                                                                    reg.SRRegistrationType == AppConstant.RegistrationType.OutPatient ? "RAWAT JALAN" : "IGD";

                                                                var diag = new EpisodeDiagnose();
                                                                diag.Query.es.Top = 1;
                                                                diag.Query.Where(diag.Query.RegistrationNo == entity.RegistrationNo, diag.Query.DiagnosisText != string.Empty);
                                                                diag.Query.OrderBy(diag.Query.LastUpdateDateTime.Descending);
                                                                if (diag.Query.Load()) root.spc_needs = diag.DiagnosisText;
                                                                else root.spc_needs = string.Empty;

                                                                root.payment = reg.GuarantorID == AppParameter.GetParameterValue(AppParameter.ParameterItem.SelfGuarantorID) ? "TUNAI" : "ASURANSI/PERUSAHAAN";
                                                                root.arrive_date = entity.ExecutionDate.Value.ToString("yyyyMMdd");
                                                                root.arrive_time = entity.ExecutionDate.Value.ToString("HH:mm:ss").Replace(":", string.Empty);

                                                                var service = new Common.Worklist.RSI.Service();
                                                                var response = service.CreateJsonOrder(root);
                                                                if (!response.data.status) return response.data.hasil;

                                                                //xml
                                                                service = new Common.Worklist.RSI.Service();
                                                                var sps = service.GetSpsLastCode();

                                                                var ds = new Common.Worklist.RSI.Xml.dataset();

                                                                Common.Worklist.RSI.Xml.datasetAttr[] datasetAttr_array = new Common.Worklist.RSI.Xml.datasetAttr[24];

                                                                ///
                                                                var datasetAttr = new Common.Worklist.RSI.Xml.datasetAttr
                                                                {
                                                                    tag = "00080005",
                                                                    Text = "ISO_IR 192"
                                                                };
                                                                datasetAttr_array.SetValue(datasetAttr, 0);

                                                                ///
                                                                datasetAttr = new Common.Worklist.RSI.Xml.datasetAttr
                                                                {
                                                                    tag = "00400100"
                                                                };

                                                                var datasetAttrAttr_array = new Common.Worklist.RSI.Xml.datasetAttrAttr[12];

                                                                var datasetAttrAttr = new Common.Worklist.RSI.Xml.datasetAttrAttr
                                                                {
                                                                    tag = "00400001",
                                                                    Text = "DCMPACS"
                                                                };
                                                                datasetAttrAttr_array.SetValue(datasetAttrAttr, 0);

                                                                datasetAttrAttr = new Common.Worklist.RSI.Xml.datasetAttrAttr
                                                                {
                                                                    tag = "00400002",
                                                                    Text = root.schedule_date
                                                                };
                                                                datasetAttrAttr_array.SetValue(datasetAttrAttr, 1);

                                                                datasetAttrAttr = new Common.Worklist.RSI.Xml.datasetAttrAttr
                                                                {
                                                                    tag = "00400003",
                                                                    Text = root.schedule_time
                                                                };
                                                                datasetAttrAttr_array.SetValue(datasetAttrAttr, 2);

                                                                datasetAttrAttr = new Common.Worklist.RSI.Xml.datasetAttrAttr
                                                                {
                                                                    tag = "00080060",
                                                                    Text = root.xray_type_code
                                                                };
                                                                datasetAttrAttr_array.SetValue(datasetAttrAttr, 3);

                                                                datasetAttrAttr = new Common.Worklist.RSI.Xml.datasetAttrAttr
                                                                {
                                                                    tag = "00400006",
                                                                    Text = "NULL"
                                                                };
                                                                datasetAttrAttr_array.SetValue(datasetAttrAttr, 4);

                                                                datasetAttrAttr = new Common.Worklist.RSI.Xml.datasetAttrAttr
                                                                {
                                                                    tag = "00400007",
                                                                    Text = root.prosedur
                                                                };
                                                                datasetAttrAttr_array.SetValue(datasetAttrAttr, 5);

                                                                datasetAttrAttr = new Common.Worklist.RSI.Xml.datasetAttrAttr
                                                                {
                                                                    tag = "00400011",
                                                                    Text = "Scheduled Procedure Step Location"
                                                                };
                                                                datasetAttrAttr_array.SetValue(datasetAttrAttr, 6);

                                                                datasetAttrAttr = new Common.Worklist.RSI.Xml.datasetAttrAttr
                                                                {
                                                                    tag = "00400008"
                                                                };

                                                                var datasetAttrAttrAttr_array = new Common.Worklist.RSI.Xml.datasetAttrAttrAttr[3];

                                                                var datasetAttrAttrAttr = new Common.Worklist.RSI.Xml.datasetAttrAttrAttr
                                                                {
                                                                    tag = "00080100",
                                                                    Value = "PROT-1205"
                                                                };
                                                                datasetAttrAttrAttr_array.SetValue(datasetAttrAttrAttr, 0);

                                                                datasetAttrAttrAttr = new Common.Worklist.RSI.Xml.datasetAttrAttrAttr
                                                                {
                                                                    tag = "00080102",
                                                                    Value = "DCM"
                                                                };
                                                                datasetAttrAttrAttr_array.SetValue(datasetAttrAttrAttr, 1);

                                                                datasetAttrAttrAttr = new Common.Worklist.RSI.Xml.datasetAttrAttrAttr
                                                                {
                                                                    tag = "00080104",
                                                                    Value = "NA"
                                                                };
                                                                datasetAttrAttrAttr_array.SetValue(datasetAttrAttrAttr, 2);

                                                                datasetAttrAttr.item = datasetAttrAttrAttr_array;

                                                                datasetAttrAttr_array.SetValue(datasetAttrAttr, 7);

                                                                datasetAttrAttr = new Common.Worklist.RSI.Xml.datasetAttrAttr
                                                                {
                                                                    tag = "00400012",
                                                                    Text = "Pre-Medication"
                                                                };
                                                                datasetAttrAttr_array.SetValue(datasetAttrAttr, 8);

                                                                datasetAttrAttr = new Common.Worklist.RSI.Xml.datasetAttrAttr
                                                                {
                                                                    tag = "00400009",
                                                                    Text = sps[0]
                                                                };
                                                                datasetAttrAttr_array.SetValue(datasetAttrAttr, 9);

                                                                datasetAttrAttr = new Common.Worklist.RSI.Xml.datasetAttrAttr
                                                                {
                                                                    tag = "00321070",
                                                                    Text = "NULL"
                                                                };
                                                                datasetAttrAttr_array.SetValue(datasetAttrAttr, 10);

                                                                datasetAttrAttr = new Common.Worklist.RSI.Xml.datasetAttrAttr
                                                                {
                                                                    tag = "00400020",
                                                                    Text = "SCHEDULED"
                                                                };
                                                                datasetAttrAttr_array.SetValue(datasetAttrAttr, 11);

                                                                datasetAttr.item = datasetAttrAttr_array;
                                                                datasetAttr_array.SetValue(datasetAttr, 1);

                                                                ///
                                                                datasetAttr = new Common.Worklist.RSI.Xml.datasetAttr
                                                                {
                                                                    tag = "00401001",
                                                                    Text = sps[1]
                                                                };
                                                                datasetAttr_array.SetValue(datasetAttr, 2);

                                                                ///
                                                                datasetAttr = new Common.Worklist.RSI.Xml.datasetAttr
                                                                {
                                                                    tag = "00321060",
                                                                    Text = irad.ItemName
                                                                };
                                                                datasetAttr_array.SetValue(datasetAttr, 3);

                                                                ///
                                                                datasetAttr = new Common.Worklist.RSI.Xml.datasetAttr
                                                                {
                                                                    tag = "00321064"
                                                                };

                                                                datasetAttrAttr_array = new Common.Worklist.RSI.Xml.datasetAttrAttr[3];

                                                                datasetAttrAttr = new Common.Worklist.RSI.Xml.datasetAttrAttr
                                                                {
                                                                    tag = "00080100",
                                                                    Text = "PROC-1205"
                                                                };
                                                                datasetAttrAttr_array.SetValue(datasetAttrAttr, 0);

                                                                datasetAttrAttr = new Common.Worklist.RSI.Xml.datasetAttrAttr
                                                                {
                                                                    tag = "00080102",
                                                                    Text = "DCM"
                                                                };
                                                                datasetAttrAttr_array.SetValue(datasetAttrAttr, 1);

                                                                datasetAttrAttr = new Common.Worklist.RSI.Xml.datasetAttrAttr
                                                                {
                                                                    tag = "00080104",
                                                                    Text = irad.ItemName
                                                                };
                                                                datasetAttrAttr_array.SetValue(datasetAttrAttr, 2);

                                                                datasetAttr.item = datasetAttrAttr_array;
                                                                datasetAttr_array.SetValue(datasetAttr, 4);

                                                                ///
                                                                datasetAttr = new Common.Worklist.RSI.Xml.datasetAttr
                                                                {
                                                                    tag = "0020000D",
                                                                    Text = root.uid
                                                                };
                                                                datasetAttr_array.SetValue(datasetAttr, 5);

                                                                ///
                                                                datasetAttr = new Common.Worklist.RSI.Xml.datasetAttr
                                                                {
                                                                    tag = "00401003",
                                                                    Text = root.priority
                                                                };
                                                                datasetAttr_array.SetValue(datasetAttr, 6);

                                                                ///
                                                                datasetAttr = new Common.Worklist.RSI.Xml.datasetAttr
                                                                {
                                                                    tag = "00080050",
                                                                    Text = root.acc
                                                                };
                                                                datasetAttr_array.SetValue(datasetAttr, 7);

                                                                ///
                                                                datasetAttr = new Common.Worklist.RSI.Xml.datasetAttr
                                                                {
                                                                    tag = "00321032",
                                                                    Text = root.dokrad_name
                                                                };
                                                                datasetAttr_array.SetValue(datasetAttr, 8);

                                                                ///
                                                                datasetAttr = new Common.Worklist.RSI.Xml.datasetAttr
                                                                {
                                                                    tag = "00321033",
                                                                    Text = root.name_dep
                                                                };
                                                                datasetAttr_array.SetValue(datasetAttr, 9);

                                                                ///
                                                                datasetAttr = new Common.Worklist.RSI.Xml.datasetAttr
                                                                {
                                                                    tag = "00080090",
                                                                    Text = root.named
                                                                };
                                                                datasetAttr_array.SetValue(datasetAttr, 10);

                                                                ///
                                                                datasetAttr = new Common.Worklist.RSI.Xml.datasetAttr
                                                                {
                                                                    tag = "00380010",
                                                                    Text = "ADM-1234"
                                                                };
                                                                datasetAttr_array.SetValue(datasetAttr, 11);

                                                                ///
                                                                datasetAttr = new Common.Worklist.RSI.Xml.datasetAttr
                                                                {
                                                                    tag = "00380300",
                                                                    Text = root.name_dep
                                                                };
                                                                datasetAttr_array.SetValue(datasetAttr, 12);

                                                                ///
                                                                datasetAttr = new Common.Worklist.RSI.Xml.datasetAttr
                                                                {
                                                                    tag = "00100010",
                                                                    Text = root.name
                                                                };
                                                                datasetAttr_array.SetValue(datasetAttr, 13);

                                                                ///
                                                                datasetAttr = new Common.Worklist.RSI.Xml.datasetAttr
                                                                {
                                                                    tag = "00100020",
                                                                    Text = root.patientid
                                                                };
                                                                datasetAttr_array.SetValue(datasetAttr, 14);

                                                                ///
                                                                datasetAttr = new Common.Worklist.RSI.Xml.datasetAttr
                                                                {
                                                                    tag = "00100030",
                                                                    Text = root.birth_date
                                                                };
                                                                datasetAttr_array.SetValue(datasetAttr, 15);

                                                                ///
                                                                datasetAttr = new Common.Worklist.RSI.Xml.datasetAttr
                                                                {
                                                                    tag = "00100040",
                                                                    Text = root.sex
                                                                };
                                                                datasetAttr_array.SetValue(datasetAttr, 16);

                                                                ///
                                                                datasetAttr = new Common.Worklist.RSI.Xml.datasetAttr
                                                                {
                                                                    tag = "00101030",
                                                                    Text = "NULL"
                                                                };
                                                                datasetAttr_array.SetValue(datasetAttr, 17);

                                                                ///
                                                                datasetAttr = new Common.Worklist.RSI.Xml.datasetAttr
                                                                {
                                                                    tag = "00403001",
                                                                    Text = "V"
                                                                };
                                                                datasetAttr_array.SetValue(datasetAttr, 18);

                                                                ///
                                                                datasetAttr = new Common.Worklist.RSI.Xml.datasetAttr
                                                                {
                                                                    tag = "00380500",
                                                                    Text = root.pat_state
                                                                };
                                                                datasetAttr_array.SetValue(datasetAttr, 19);

                                                                ///
                                                                datasetAttr = new Common.Worklist.RSI.Xml.datasetAttr
                                                                {
                                                                    tag = "001021C0",
                                                                    Text = "000"
                                                                };
                                                                datasetAttr_array.SetValue(datasetAttr, 20);

                                                                ///
                                                                datasetAttr = new Common.Worklist.RSI.Xml.datasetAttr
                                                                {
                                                                    tag = "00102000",
                                                                    Text = "Medical Alerts"
                                                                };
                                                                datasetAttr_array.SetValue(datasetAttr, 21);

                                                                ///
                                                                datasetAttr = new Common.Worklist.RSI.Xml.datasetAttr
                                                                {
                                                                    tag = "00102110",
                                                                    Text = "NULL"
                                                                };
                                                                datasetAttr_array.SetValue(datasetAttr, 22);

                                                                ///
                                                                datasetAttr = new Common.Worklist.RSI.Xml.datasetAttr
                                                                {
                                                                    tag = "00380050"
                                                                };
                                                                datasetAttr_array.SetValue(datasetAttr, 23);

                                                                ds.attr = datasetAttr_array;

                                                                service = new Common.Worklist.RSI.Service();
                                                                service.CreateXmlWorklist(ds);

                                                                detail.ResultValue = root.uid;
                                                            }
                                                        }
                                                        else if (AppSession.Parameter.HealthcareInitial == "RSBK")
                                                        {
                                                            var pref = new Paramedic();
                                                            pref.LoadByPrimaryKey(reg.ParamedicID);

                                                            var uref = new ServiceUnit();
                                                            uref.LoadByPrimaryKey(entity.FromServiceUnitID);

                                                            var epsdiag = new EpisodeDiagnose();
                                                            epsdiag.Query.es.Top = 1;
                                                            epsdiag.Query.Where(epsdiag.Query.RegistrationNo == reg.RegistrationNo, epsdiag.Query.SRDiagnoseType.In("DiagnoseType-001", "DiagnoseType-006"), epsdiag.Query.IsVoid == false);
                                                            epsdiag.Query.OrderBy(epsdiag.Query.CreateDateTime.Descending);
                                                            var isEpsDiag = epsdiag.Query.Load();

                                                            string diagId = string.Empty;
                                                            string diagnoseName = string.Empty;
                                                            string patasdiagnose = string.Empty;

                                                            var patas = new PatientAssessment();
                                                            patas.Query.es.Top = 1;
                                                            patas.Query.Where(patas.Query.RegistrationNo == reg.RegistrationNo);
                                                            patas.Query.OrderBy(patas.Query.CreatedDateTime.Descending);
                                                            var patasdiag = patas.Query.Load();

                                                            diagId = string.IsNullOrWhiteSpace(epsdiag.DiagnoseID) ? string.Empty : $"({epsdiag.DiagnoseID}) ";
                                                            diagnoseName = epsdiag.DiagnosisText ?? string.Empty;
                                                            patasdiagnose = patas.Diagnose ?? string.Empty;

                                                            if (transChargesItems.Any(t => (t.IsOrderRealization ?? false) && !(t.IsVoid ?? false)))
                                                            {
                                                                var list = transChargesItems.Where(t => (t.IsOrderRealization ?? false) && !(t.IsVoid ?? false)).Select(t =>
                                                                {
                                                                    var it = new Item();
                                                                    it.LoadByPrimaryKey(t.ItemID);

                                                                    if (it.IsHasTestResults == false)
                                                                    {
                                                                        return null;
                                                                    }

                                                                    var itg = new ItemGroup();
                                                                    itg.LoadByPrimaryKey(it.ItemGroupID);

                                                                    var refdoc = entity.PhysicianSenders ?? string.Empty;

                                                                    var tcic = new TransChargesItemComp();
                                                                    tcic.LoadByPrimaryKey(t.TransactionNo, t.SequenceNo, "05");

                                                                    var opername = string.Empty;
                                                                    opername = tcic.ParamedicID ?? string.Empty;

                                                                    var sero = new ServiceRoom();
                                                                    sero.LoadByPrimaryKey(reg.RoomID);

                                                                    var seru = new ServiceUnit();
                                                                    seru.LoadByPrimaryKey(entity.FromServiceUnitID);

                                                                    var sal = new AppStandardReferenceItem();
                                                                    sal.LoadByPrimaryKey("Salutation", patient.SRSalutation);

                                                                    return new Common.Worklist.RSBK.DataExamOrder()
                                                                    {
                                                                        patient_id = patient.MedicalNo,
                                                                        patient_name = $"{sal.ItemName} {patient.FirstName} {patient.MiddleName} {patient.LastName}",
                                                                        patient_sex = patient.Sex == "M" ? "M" : (patient.Sex == "F" ? "F" : (patient.Sex == "O" ? "O" : "U")),
                                                                        patient_birthday = patient.DateOfBirth.Value.ToString("yyyy-MM-dd"),
                                                                        patient_weight = string.Empty,
                                                                        patient_class = reg.SRRegistrationType == AppConstant.RegistrationType.InPatient ? "I" : reg.SRRegistrationType == AppConstant.RegistrationType.OutPatient ? "O" : "E",
                                                                        ward = sero.RoomName,
                                                                        attending_doctor = t.ParamedicCollectionName,
                                                                        referring_doctor = refdoc,
                                                                        order_control = "NW",
                                                                        order_department = reg.DepartmentID,
                                                                        accession_number = $"{t.TransactionNo}" + $"{t.SequenceNo.Substring(t.SequenceNo.Length - 2)}", //co:JO240424-00003 + 001 > JO240424-00003 + 01 > JO240424-0000301
                                                                        study_code = t.ItemID,
                                                                        study_name = GetItemName(t.ItemID),
                                                                        order_datetime = entity.TransactionDate.Value.ToString("yyyy-MM-dd HH:mm:ss"),
                                                                        scheduled_datetime = entity.ExecutionDate.Value.ToString("yyyy-MM-dd HH:mm:ss"),
                                                                        clinic_comments = $"{diagId}, {diagnoseName} ~~ {entity.Notes} ~~ {patasdiagnose}",
                                                                        sickness_name = t.Notes,
                                                                        reason_for_study = string.Empty,
                                                                        body_part = string.Empty,
                                                                        ordering_doctor = t.ParamedicCollectionName,
                                                                        exam_room = seru.ServiceUnitName,
                                                                        modality = itg.Initial.Substring(itg.Initial.Length - 2),
                                                                        operator_name = Paramedic.GetParamedicName(opername),
                                                                        exam_urgent = t.IsCito.HasValue ? (t.IsCito.Value ? "1" : "0") : "0",
                                                                        issuer = "H",
                                                                        if_flag = 0,
                                                                        result = 0,
                                                                        urllink = string.Empty
                                                                    };
                                                                })
                                                                .Where(order => order != null)
                                                                .ToList();

                                                                var ris = new Common.Worklist.RSBK.Service();
                                                                if (!ris.InsertExamOrder(list))
                                                                {
                                                                    //ShowInformationHeader($"Send order failed, please try again.");
                                                                    //return false;
                                                                }
                                                                foreach (var tci in transChargesItems.Where(t => (t.IsOrderRealization ?? false) && !(t.IsVoid ?? false)))
                                                                {
                                                                    tci.IsSendToLIS = true;
                                                                }
                                                            }
                                                        }
                                                    }
                                                    transChargesItems.Save();
                                                    break;
                                                case "RSTJ":
                                                    //if (!HttpContext.Current.IsDebuggingEnabled)
                                                    {
                                                        var funit = new ServiceUnit();
                                                        funit.LoadByPrimaryKey(entity.FromServiceUnitID);

                                                        var pmedic = new Paramedic();
                                                        pmedic.LoadByPrimaryKey(reg.ParamedicID);

                                                        foreach (var detail in transChargesItems.Where(t => (t.IsOrderRealization ?? false) && !(t.IsVoid ?? false)))
                                                        {
                                                            var tmedic = new Paramedic();
                                                            //if (!tmedic.LoadByPrimaryKey(detail.ParamedicID))
                                                            {
                                                                var comp = TransChargesItemComps.Where(c => c.TransactionNo == detail.TransactionNo && c.SequenceNo == detail.SequenceNo && c.TariffComponentID == "01" && !string.IsNullOrEmpty(c.ParamedicID)).SingleOrDefault();
                                                                if (comp != null)
                                                                {
                                                                    //tmedic = new Paramedic();
                                                                    tmedic.LoadByPrimaryKey(comp.ParamedicID);
                                                                }
                                                            }

                                                            var item = new Item();
                                                            item.LoadByPrimaryKey(detail.ItemID);

                                                            var svc = new Common.Worklist.RSTJ.Service();
                                                            var request = new Common.Worklist.RSTJ.Json.Request.Root()
                                                            {
                                                                Norm = patient.MedicalNo,
                                                                Nama = patient.PatientName,
                                                                Alamat = patient.Address,
                                                                Kota = patient.City,
                                                                Tgllahir = patient.DateOfBirth.Value.ToString("yyyy-MM-dd"),
                                                                Nohap = patient.MobilePhoneNo,
                                                                Kelamin = patient.Sex == "M" ? "L" : "P",
                                                                Drpeminta = pmedic.ParamedicName,
                                                                Asalpasien = funit.ServiceUnitName,
                                                                Layanan = item.ItemName,
                                                                Notagihan = "",
                                                                Statusbayar = "",
                                                                NoTransaksi = detail.TransactionNo + "-" + detail.SequenceNo,
                                                                Asuransi = grr.GuarantorName,
                                                                DokterRadiologi = tmedic.ParamedicName,
                                                                DokterId = tmedic.ParamedicID
                                                            };
                                                            var response = svc.SendJsonOrder(request);
                                                            if (!string.IsNullOrEmpty(response)) detail.IsSendToLIS = true;

                                                            var log = new WebServiceAPILog()
                                                            {
                                                                DateRequest = DateTime.Now,
                                                                IPAddress = "10.10.10.38",
                                                                UrlAddress = "http://10.10.10.38/reqpasien",
                                                                Params = Newtonsoft.Json.JsonConvert.SerializeObject(request),
                                                                Response = response,
                                                                Totalms = 0
                                                            };
                                                            log.Save();
                                                        }

                                                        transChargesItems.Save();
                                                    }
                                                    break;
                                                case "RSMMP":
                                                    {
                                                        var pref = new Paramedic();
                                                        pref.LoadByPrimaryKey(reg.ParamedicID);

                                                        var uref = new ServiceUnit();
                                                        uref.LoadByPrimaryKey(entity.FromServiceUnitID);

                                                        var diag = new EpisodeDiagnose();
                                                        diag.Query.es.Top = 1;
                                                        diag.Query.Where(diag.Query.RegistrationNo == reg.RegistrationNo, diag.Query.SRDiagnoseType.In(AppSession.Parameter.DiagnoseTypeMain), diag.Query.IsVoid == false);
                                                        diag.Query.OrderBy(diag.Query.LastUpdateDateTime.Descending);
                                                        var isDiag = diag.Query.Load();

                                                        var diagId = !isDiag ? string.Empty : $"({diag.DiagnoseID}) ";

                                                        var tc = new TariffComponentCollection();
                                                        tc.Query.Where(tc.Query.IsTariffParamedic == true);
                                                        tc.Query.Load();

                                                        var tcics = TransChargesItemComps.Where(t => t.TransactionNo == entity.TransactionNo && tc.Select(c => c.TariffComponentID).Contains(t.TariffComponentID) && !string.IsNullOrWhiteSpace(t.ParamedicID))
                                                            .Select(t => t.SequenceNo)
                                                            .Distinct();

                                                        if (tcics.Any())
                                                        {
                                                            if (transChargesItems.Any(t => tcics.Contains(t.SequenceNo) && (t.IsOrderRealization ?? false) && !(t.IsVoid ?? false)))
                                                            {
                                                                var root = new Common.Worklist.RSMMP.Json.Order.Request.Root
                                                                {
                                                                    Patient = new Common.Worklist.RSMMP.Json.Order.Request.Patient()
                                                                    {
                                                                        Address = patient.Address,
                                                                        Alerts = string.Empty,
                                                                        Allergies = string.Empty,
                                                                        BirthDate = patient.DateOfBirth.Value.ToString("yyyyMMdd"),
                                                                        BirthPlace = patient.CityOfBirth,
                                                                        Diagnosis = isDiag ? $"{diagId}{diag.DiagnosisText}" : string.Empty,
                                                                        Email = patient.Email,
                                                                        FirstName = patient.FirstName,
                                                                        Gender = patient.Sex,
                                                                        Height = 0,
                                                                        LastName = patient.LastName,
                                                                        MedicalNo = patient.MedicalNo,
                                                                        MiddleName = patient.MiddleName,
                                                                        Phone = patient.MobilePhoneNo,
                                                                        Pregnancy = null,
                                                                        Ssn = patient.Ssn,
                                                                        Weight = 0
                                                                    },
                                                                    Exams = transChargesItems.Where(t => tcics.Contains(t.SequenceNo) && (t.IsOrderRealization ?? false) && !(t.IsVoid ?? false))
                                                                                         .Select(tci => new Common.Worklist.RSMMP.Json.Order.Request.Exam()
                                                                                         {
                                                                                             Datetime = tci.RealizationDateTime.Value.ToString("yyyyMMdd HHmmss"),
                                                                                             HisRefId = $"{tci.TransactionNo}#{tci.SequenceNo}",
                                                                                             Payor = new Common.Worklist.RSMMP.Json.Order.Request.Payor()
                                                                                             {
                                                                                                 Code = grr.GuarantorID,
                                                                                                 Name = grr.GuarantorName,
                                                                                             },
                                                                                             ParamedicPic = new Common.Worklist.RSMMP.Json.Order.Request.ParamedicPic()
                                                                                             {
                                                                                                 Code = TransChargesItemComps.Where(x => x.TransactionNo == tci.TransactionNo && x.SequenceNo == tci.SequenceNo && !string.IsNullOrWhiteSpace(x.ParamedicID))
                                                                                                    .OrderBy(x => x.TariffComponentID)
                                                                                                    .First()
                                                                                                    .ParamedicID
                                                                                             },
                                                                                             ParamedicRef = new Common.Worklist.RSMMP.Json.Order.Request.ParamedicRef()
                                                                                             {
                                                                                                 Code = reg.ParamedicID,
                                                                                                 Name = pref.ParamedicName
                                                                                             },
                                                                                             Study = new Common.Worklist.RSMMP.Json.Order.Request.Study()
                                                                                             {
                                                                                                 Code = tci.ItemID
                                                                                             },
                                                                                             CitoStat = tci.IsCito ?? false,
                                                                                             UnitPic = new Common.Worklist.RSMMP.Json.Order.Request.UnitPic()
                                                                                             {
                                                                                                 Code = entity.ToServiceUnitID
                                                                                             },
                                                                                             UnitRef = new Common.Worklist.RSMMP.Json.Order.Request.UnitRef()
                                                                                             {
                                                                                                 Code = entity.FromServiceUnitID,
                                                                                                 Name = uref.ServiceUnitName
                                                                                             },
                                                                                             UserId = tci.RealizationUserID
                                                                                         }).ToList()
                                                                };

                                                                var svc = new Common.Worklist.RSMMP.Service();
                                                                var response = svc.SendOrder(root);
                                                                if (response.Metadata.Code == 200)
                                                                {
                                                                    foreach (var tci in transChargesItems.Where(t => (t.IsOrderRealization ?? false) && !(t.IsVoid ?? false)))
                                                                    {
                                                                        var data = response.Exams.SingleOrDefault(r => r.HisRefId == $"{tci.TransactionNo}#{tci.SequenceNo}");
                                                                        if (data == null) continue;
                                                                        tci.IsSendToLIS = true;
                                                                        tci.ResultValue = data.PacsStudyUid;
                                                                        tci.FilmNo = data.FilmNo;
                                                                    }
                                                                    transChargesItems.Save();
                                                                }
                                                                else
                                                                {
                                                                    return $"Send order failed, please try again. {response.Metadata.Message}";
                                                                }
                                                            }
                                                        }
                                                    }
                                                    break;
                                                case "RSCDR":
                                                    {
                                                        var pref = new Paramedic();
                                                        pref.LoadByPrimaryKey(reg.ParamedicID);

                                                        var uref = new ServiceUnit();
                                                        uref.LoadByPrimaryKey(entity.FromServiceUnitID);

                                                        foreach (var detail in transChargesItems.Where(t => (t.IsOrderRealization ?? false) && !(t.IsVoid ?? false)))
                                                        {
                                                            var item = new Item();
                                                            item.LoadByPrimaryKey(detail.ItemID);

                                                            var root = new Common.Worklist.RSCDR.Json.Order.Root()
                                                            {
                                                                Order = new Common.Worklist.RSCDR.Json.Order.JOrder()
                                                                {
                                                                    Order2 = new Common.Worklist.RSCDR.Json.Order.JOrder2()
                                                                    {
                                                                        Id = $"{detail.TransactionNo}-{detail.SequenceNo.ToInt()}",
                                                                        ServiceCode = detail.ItemID,
                                                                        ServiceName = item.ItemName,
                                                                        Status = "NEW",
                                                                        OrderDate = entity.ExecutionDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                                                        Doctor = pref.ParamedicName,
                                                                        Modality = "CT",
                                                                        ClinicalDiagnosis = ""
                                                                    },
                                                                    Patient = new Common.Worklist.RSCDR.Json.Order.Patient()
                                                                    {
                                                                        Id = patient.MedicalNo,
                                                                        FirstName = string.Empty,
                                                                        MiddleName = string.Empty,
                                                                        LastName = patient.PatientName,
                                                                        Sex = patient.Sex,
                                                                        BirthDate = patient.DateOfBirth?.ToString("yyyy-MM-dd"),
                                                                        Phone = patient.MobilePhoneNo,
                                                                        Address = patient.Address,
                                                                        Height = "",
                                                                        Weight = "",
                                                                        Priority = "",
                                                                        Department = uref.ServiceUnitName
                                                                    }
                                                                }
                                                            };

                                                            detail.IsSendToLIS = true;

                                                            var svc = new Common.Worklist.RSCDR.Service();
                                                            var response = svc.PostOrder(root);

                                                            var log = new WebServiceAPILog()
                                                            {
                                                                DateRequest = DateTime.Now,
                                                                IPAddress = "10.10.10.38",
                                                                UrlAddress = "http://121.121.121.4:10110/pacs/putOrder/",
                                                                Params = Newtonsoft.Json.JsonConvert.SerializeObject(root),
                                                                Response = response,
                                                                Totalms = 0
                                                            };
                                                            log.Save();
                                                        }

                                                        transChargesItems.Save();
                                                    }
                                                    break;
                                                case "RSUTS":
                                                    {
                                                        var pref = new Paramedic();
                                                        pref.LoadByPrimaryKey(reg.ParamedicID);

                                                        var uref = new ServiceUnit();
                                                        uref.LoadByPrimaryKey(entity.FromServiceUnitID);

                                                        var diag = new RegistrationInfoMedicDiagnose();
                                                        diag.Query.es.Top = 1;
                                                        diag.Query.Where(diag.Query.RegistrationNo == reg.RegistrationNo, diag.Query.SRDiagnoseType.In("DiagnoseType-001", "DiagnoseType-006"), diag.Query.IsVoid == false);
                                                        diag.Query.OrderBy(diag.Query.DiagnoseDateTime.Descending);
                                                        var isDiag = diag.Query.Load();

                                                        var diagId = string.IsNullOrWhiteSpace(diag.DiagnoseID) ? string.Empty : $"({diag.DiagnoseID}) ";

                                                        if (transChargesItems.Any(t => (t.IsOrderRealization ?? false) && !(t.IsVoid ?? false)))
                                                        {
                                                            var list = transChargesItems.Where(t => (t.IsOrderRealization ?? false) && !(t.IsVoid ?? false)).Select(t => new Common.Worklist.RSTS.Data()
                                                            {
                                                                ordercode = $"{t.TransactionNo}_{t.SequenceNo}",
                                                                trancode = $"{t.TransactionNo}_{t.SequenceNo}",
                                                                patientid = patient.MedicalNo,
                                                                patientname = patient.PatientName,
                                                                patientbirth = patient.DateOfBirth.Value.ToString("yyyy-MM-dd"),
                                                                orderdate = entity.ExecutionDate.Value.ToString("yyyy-MM-dd"),
                                                                ordertime = entity.ExecutionDate.Value.ToString("HH:mm:ss"),
                                                                doctorid = reg.ParamedicID,
                                                                doctorname = pref.ParamedicName,
                                                                iostatus = reg.SRRegistrationType == AppConstant.RegistrationType.InPatient ? "I" : "O",
                                                                examinationid = t.ItemID,
                                                                examinationname = GetItemName(t.ItemID),
                                                                clinicdiag = isDiag ? $"{diagId}, {diag.DiagnoseName}" : string.Empty,
                                                                additional = string.Empty,
                                                                flag = string.Empty,
                                                                orderstatus = "1", // order
                                                                unitcode = entity.FromServiceUnitID,
                                                                unitname = uref.ServiceUnitName,
                                                                patientsex = patient.Sex == "M" ? "L" : "P",
                                                                patientaddress = patient.Address,
                                                                no_hp = patient.MobilePhoneNo,
                                                                branch = "RSUD Tamansari Jakarta"

                                                            }).ToList();
                                                            var ris = new Common.Worklist.RSTS.Service();
                                                            if (!ris.Insert(list))
                                                            {
                                                                //this.sho($"Send order failed, please try again.");
                                                                //return false;
                                                            }
                                                            var tci = transChargesItems.Single(t => (t.IsOrderRealization ?? false) && !(t.IsVoid ?? false));
                                                            tci.IsSendToLIS = true;
                                                        }
                                                        transChargesItems.Save();
                                                    }
                                                    break;
                                            }
                                        }
                                    }
                                    #endregion
                                }
                            }

                            //if (!string.IsNullOrEmpty(entity.PackageReferenceNo))
                            //{
                            //    int? x = ParamedicFeeTransChargesItemCompSettled.AddNewSettled(entity, TransChargesItemComps, AppSession.UserLogin.UserID, false);
                            //}

                            if (QueryString_type == "tr")
                            {
                                if (AppSession.Parameter.HealthcareInitial == "RSMMP")
                                {
                                    if (AppSession.Parameter.IsUsingRisPacsInterop)
                                    {
                                        var pref = new Paramedic();
                                        pref.LoadByPrimaryKey(reg.ParamedicID);

                                        var uref = new ServiceUnit();
                                        uref.LoadByPrimaryKey(entity.FromServiceUnitID);

                                        var diag = new RegistrationInfoMedicDiagnose();
                                        diag.Query.es.Top = 1;
                                        diag.Query.Where(diag.Query.RegistrationNo == reg.RegistrationNo, diag.Query.SRDiagnoseType.In("DiagnoseType-001", "DiagnoseType-006"), diag.Query.IsVoid == false);
                                        diag.Query.OrderBy(diag.Query.DiagnoseDateTime.Descending);
                                        var isDiag = diag.Query.Load();

                                        var diagId = string.IsNullOrWhiteSpace(diag.DiagnoseID) ? string.Empty : $"({diag.DiagnoseID}) ";

                                        var tc = new TariffComponentCollection();
                                        tc.Query.Where(tc.Query.IsTariffParamedic == true);
                                        tc.Query.Load();

                                        var tcics = TransChargesItemComps.Where(t => t.TransactionNo == entity.TransactionNo && tc.Select(c => c.TariffComponentID).Contains(t.TariffComponentID) && !string.IsNullOrWhiteSpace(t.ParamedicID))
                                            .Select(t => t.SequenceNo)
                                            .Distinct();

                                        if (tcics.Any())
                                        {
                                            var exams = new List<Common.Worklist.RSMMP.Json.Order.Request.Exam>();
                                            foreach (var tci in transChargesItems.Where(t => tcics.Contains(t.SequenceNo) && !(t.IsVoid ?? false)))
                                            {
                                                var item = new Item();
                                                item.LoadByPrimaryKey(tci.ItemID);
                                                if (item.SRItemType != ItemType.Service) continue;
                                                if (string.IsNullOrWhiteSpace(item.ItemIDExternal)) continue;
                                                // hanya item service di unit cathlab yg di mapping

                                                exams.Add(new Common.Worklist.RSMMP.Json.Order.Request.Exam()
                                                {
                                                    Datetime = entity.ExecutionDate.Value.ToString("yyyyMMdd HHmmss"),
                                                    HisRefId = $"{tci.TransactionNo}#{tci.SequenceNo}",
                                                    Payor = new Common.Worklist.RSMMP.Json.Order.Request.Payor()
                                                    {
                                                        Code = grr.GuarantorID,
                                                        Name = grr.GuarantorName,
                                                    },
                                                    ParamedicPic = new Common.Worklist.RSMMP.Json.Order.Request.ParamedicPic()
                                                    {
                                                        Code = TransChargesItemComps.Where(x => x.TransactionNo == tci.TransactionNo && x.SequenceNo == tci.SequenceNo && !string.IsNullOrWhiteSpace(x.ParamedicID))
                                                                                    .OrderBy(x => x.TariffComponentID)
                                                                                    .First()
                                                                                    .ParamedicID
                                                    },
                                                    ParamedicRef = new Common.Worklist.RSMMP.Json.Order.Request.ParamedicRef()
                                                    {
                                                        Code = reg.ParamedicID,
                                                        Name = pref.ParamedicName
                                                    },
                                                    Study = new Common.Worklist.RSMMP.Json.Order.Request.Study()
                                                    {
                                                        Code = tci.ItemID
                                                    },
                                                    CitoStat = tci.IsCito ?? false,
                                                    UnitPic = new Common.Worklist.RSMMP.Json.Order.Request.UnitPic()
                                                    {
                                                        Code = entity.ToServiceUnitID
                                                    },
                                                    UnitRef = new Common.Worklist.RSMMP.Json.Order.Request.UnitRef()
                                                    {
                                                        Code = entity.FromServiceUnitID,
                                                        Name = uref.ServiceUnitName
                                                    },
                                                    UserId = tci.CreatedByUserID
                                                });
                                            }

                                            if (exams.Any())
                                            {
                                                var root = new Common.Worklist.RSMMP.Json.Order.Request.Root
                                                {
                                                    Patient = new Common.Worklist.RSMMP.Json.Order.Request.Patient()
                                                    {
                                                        Address = pat.Address,
                                                        Alerts = string.Empty,
                                                        Allergies = string.Empty,
                                                        BirthDate = pat.DateOfBirth.Value.ToString("yyyyMMdd"),
                                                        BirthPlace = pat.CityOfBirth,
                                                        Diagnosis = isDiag ? $"{diagId}{diag.DiagnosisText}" : string.Empty,
                                                        Email = pat.Email,
                                                        FirstName = pat.FirstName,
                                                        Gender = pat.Sex,
                                                        Height = null,
                                                        LastName = pat.LastName,
                                                        MedicalNo = pat.MedicalNo,
                                                        MiddleName = pat.MiddleName,
                                                        Phone = pat.MobilePhoneNo,
                                                        Pregnancy = null,
                                                        Ssn = pat.Ssn,
                                                        Weight = null
                                                    },
                                                    Exams = exams
                                                };

                                                var svc = new Common.Worklist.RSMMP.Service();
                                                var response = svc.SendOrder(root);
                                                if (response.Metadata.Code == 200)
                                                {
                                                    foreach (var tci in transChargesItems.Where(t => !(t.IsVoid ?? false)))
                                                    {
                                                        var data = response.Exams.SingleOrDefault(r => r.HisRefId == $"{tci.TransactionNo}#{tci.SequenceNo}");
                                                        if (data == null) continue;
                                                        tci.IsSendToLIS = true;
                                                        tci.ResultValue = data.PacsStudyUid;
                                                        tci.FilmNo = data.FilmNo;
                                                    }
                                                    transChargesItems.Save();
                                                }
                                                else
                                                {
                                                    return $"Send order failed, please try again. {response.Metadata.Message}";
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (AppSession.Parameter.IsUsingHisInterop)
                            {
                                if (AppSession.Parameter.HealthcareInitialAppsVersion == "YBRSGKP")
                                {
                                    if (AppSession.Parameter.HealthcareInitial == "RSI")
                                    {
                                        if (entity.ToServiceUnitID == AppSession.Parameter.ServiceUnitLaboratoryID)
                                        {
                                            TransChargesItems.Save();
                                            TransChargesItemComps.Save();
                                            CostCalculations.Save();
                                            TransChargesItemConsumptions.Save();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                //Commit if success, Rollback if failed
                trans.Complete();

                return string.Empty;
            }
        }

        private static string GetItemName(string itemId)
        {
            var item = new Item();
            return item.LoadByPrimaryKey(itemId) ? item.ItemName : string.Empty;
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            var entity = new TransCharges();
            if (!entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
            if (entity.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return;
            }
            if (entity.IsApproved ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasApproved;
                args.IsCancel = true;
                return;
            }
            if (entity.IsBillProceed ?? false)
            {
                args.MessageText = "This data has been proceed to billing.";
                args.IsCancel = true;
                return;
            }

            SetVoid(entity, true);
        }

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {
            var entity = new TransCharges();
            if (!entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }

            SetVoid(entity, false);
        }

        private void SetVoid(TransCharges entity, bool isVoid)
        {
            //header
            entity.IsVoid = isVoid;
            if (isVoid)
            {
                entity.IsApproved = false;
                entity.VoidByUserID = AppSession.UserLogin.UserID;
                entity.VoidDateTime = (new DateTime()).NowAtSqlServer();
            }
            else
            {
                entity.VoidByUserID = null;
                entity.VoidDateTime = null;
            }

            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            //detail
            foreach (TransChargesItem item in TransChargesItems)
            {
                item.IsVoid = isVoid;
                if (isVoid)
                {
                    item.IsApprove = false;
                    item.VoidByUserID = AppSession.UserLogin.UserID;
                    item.VoidDateTime = (new DateTime()).NowAtSqlServer();
                }
                else
                {
                    item.VoidByUserID = null;
                    item.VoidDateTime = null;
                }

                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }

            using (var trans = new esTransactionScope())
            {
                entity.Save();
                TransChargesItems.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        #endregion

        #region ToolBar Menu Support
        public override bool OnGetStatusMenuDelete()
        {
            // hardcode krn dari program tidak ada fitur delete
            return false;
        }

        public override bool OnGetStatusMenuEdit()
        {
            return txtTransactionNo.Text != string.Empty;
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return !(bool)ViewState["IsApproved"];
        }

        public override bool OnGetStatusMenuVoid()
        {
            return !(bool)ViewState["IsVoid"];
        }

        protected override void OnDataModeChanged(Temiang.Avicenna.Common.AppEnum.DataMode oldVal, Temiang.Avicenna.Common.AppEnum.DataMode newVal)
        {
            RefreshCommandItemTransChargesItem(oldVal, newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new TransCharges();
            if (parameters.Length > 0)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                    entity.LoadByPrimaryKey(Request.QueryString["id"]);
            }
            else
                entity.LoadByPrimaryKey(txtTransactionNo.Text);
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var transCharges = (TransCharges)entity;
            txtTransactionNo.Text = transCharges.TransactionNo;

            var dummyLoad = TransChargesItemImages; //Just load TransChargesItemImages for edit after txtTransactionNo.Text set

            txtPackageReferenceNo.Text = transCharges.PackageReferenceNo;

            ViewState["IsApproved"] = transCharges.IsApproved ?? false;
            // cek approve detail
            if ((bool)ViewState["IsApproved"] && Request.QueryString["type"] != "jo")
            {
                foreach (var r in TransChargesItems.Where(x => (x.IsVoid ?? false) == false &&
                    x.ParentNo == string.Empty))
                    ViewState["IsApproved"] = (bool)ViewState["IsApproved"] && (r.IsApprove ?? false);

                //foreach (var r in TransChargesItems.Where(x => (x.IsVoid ?? false) == false &&
                //    (transCharges.IsPackage == false || (transCharges.IsPackage == true && x.IsPackage == true))))
                //    ViewState["IsApproved"] = (bool)ViewState["IsApproved"] && (r.IsApprove ?? false);
            }

            ViewState["IsVoid"] = transCharges.IsVoid ?? false;
            ViewState["IsPackage"] = transCharges.IsPackage ?? false;

            txtRegistrationNo.Text = transCharges.RegistrationNo;
            var reg = new Registration();
            if (reg.LoadByPrimaryKey(txtRegistrationNo.Text))
            {
                txtRegistrationDate.SelectedDate = reg.RegistrationDate;
                txtRegistrationTime.Text = reg.RegistrationTime;

                PopulatePASUS(reg.SRReferralGroup, reg.ReferralID, reg.ReferralName);

                var pat = new Patient();
                pat.LoadByPrimaryKey(reg.PatientID);
                txtMedicalNo.Text = pat.MedicalNo;
                txtDiagnosticNo.Text = pat.DiagnosticNo;
                var std = new AppStandardReferenceItem();
                txtSalutation.Text = std.LoadByPrimaryKey("Salutation", string.IsNullOrEmpty(pat.SRSalutation) ? string.Empty : pat.SRSalutation) ? std.ItemName : string.Empty;
                txtPatientName.Text = pat.PatientName;
                txtPlaceDOB.Text = string.Format("{0}, {1}", pat.CityOfBirth, Convert.ToDateTime(pat.DateOfBirth).ToString("dd-MMM-yyyy"));
                txtGender.Text = pat.Sex;
                txtAgeInYear.Text = Convert.ToString(reg.AgeInYear);
                txtAgeInMonth.Text = Convert.ToString(reg.AgeInMonth);
                txtAgeInDay.Text = Convert.ToString(reg.AgeInDay);

                PopulatePatientImage(pat.PatientID);

                var parID = string.IsNullOrEmpty(reg.str.ParamedicID) ? Request.QueryString["pid"] : reg.ParamedicID;
                ComboBox.PopulateWithOneParamedic(cboParamedicID, parID);

                var guarID = string.IsNullOrEmpty(pat.str.MemberID) ? reg.GuarantorID : pat.MemberID;
                ComboBox.PopulateWithOneRow(cboGuarantorID, guarID, BusinessObject.Common.Enums.EntityClassName.Guarantor);
                var guar = new Guarantor();
                if (guar.LoadByPrimaryKey(cboGuarantorID.SelectedValue))
                    trBpjsSepNo.Visible = guar.SRGuarantorType == AppSession.Parameter.GuarantorTypeBPJS;
                else trBpjsSepNo.Visible = false;
                txtBpjsSepNo.Text = reg.BpjsSepNo;

                PopulateBedCollection(reg);

                if (pnlKiaCaseType.Visible && !string.IsNullOrEmpty(reg.SRKiaCaseType))
                {
                    var kiaCase = new AppStandardReferenceItemQuery();
                    kiaCase.Where(kiaCase.ItemID == reg.SRKiaCaseType,
                                  kiaCase.StandardReferenceID == AppEnum.StandardReference.KiaCaseType);

                    cboSRKiaCaseType.DataSource = kiaCase.LoadDataTable();
                    cboSRKiaCaseType.DataBind();
                    cboSRKiaCaseType.SelectedValue = reg.SRKiaCaseType;
                }
                if (pnlObstetricType.Visible && !string.IsNullOrEmpty(reg.SRObstetricType))
                {
                    var obstetricType = new AppStandardReferenceItemQuery();
                    obstetricType.Where(obstetricType.ItemID == reg.SRObstetricType,
                                  obstetricType.StandardReferenceID == AppEnum.StandardReference.ObstetricType);

                    cboSRObstetricType.DataSource = obstetricType.LoadDataTable();
                    cboSRObstetricType.DataBind();
                    cboSRObstetricType.SelectedValue = reg.SRObstetricType;
                }

                lblRegistrationInfo2.Text = RegistrationInfoSumary.GetDocumentCheckListCountRemains(txtRegistrationNo.Text);

                tblTemporaryBill.Visible = reg.SRRegistrationType == AppConstant.RegistrationType.OutPatient && trBpjsSepNo.Visible;
                if (tblTemporaryBill.Visible)
                {
                    txtTemporaryBillPlafond.Value = Convert.ToDouble(reg.PlavonAmount);
                    txtTemporaryBillTotal.Value = Convert.ToDouble(GetTotalTemporaryBill());
                }
            }

            if (transCharges.TransactionDate.HasValue)
            {
                txtTransactionDate.SelectedDate = transCharges.TransactionDate.Value.Date;
                txtTransactionTime.Text = transCharges.TransactionDate.Value.ToString("HH:mm");
            }

            if (transCharges.ExecutionDate.HasValue)
            {
                txtExecutionDate.SelectedDate = transCharges.ExecutionDate.Value.Date;
                txtExecutionTime.Text = transCharges.ExecutionDate.Value.ToString("HH:mm");
            }

            cboFromServiceUnitID.SelectedValue = transCharges.FromServiceUnitID;

            if (pnlResponUnit.Visible)
                cboResponUnit.SelectedValue = transCharges.str.ResponUnitID;

            if (Request.QueryString["type"] == "jo" || Request.QueryString["type"] == "ds")
            {
                cboToServiceUnitID.SelectedValue = transCharges.ToServiceUnitID;
                //cboToServiceUnitID_SelectedIndexChanged(null, new RadComboBoxSelectedIndexChangedEventArgs(string.Empty, string.Empty, transCharges.ToServiceUnitID, string.Empty));
                ApplyToServiceUnitID(transCharges.ToServiceUnitID);

                cboPhysicianIDPathology.SelectedValue = transCharges.LaboratoryParamedicID;
                cboAnalystID.SelectedValue = transCharges.AnalystID;

                if (TrDiagNo.Visible && cboToServiceUnitID.SelectedValue == AppSession.Parameter.ServiceUnitRadiologyID)
                    txtDiagnosticNo.ReadOnly = false;
                else
                    txtDiagnosticNo.ReadOnly = true;

                ComboBox.PopulateWithOneStandardReference(cboTypeResult, "TypeResult", transCharges.SRTypeResult);
                cboTypeResult.SelectedValue = transCharges.SRTypeResult;
                if (!string.IsNullOrEmpty(transCharges.SRBloodSampleTakenBy))
                {
                    ComboBox.PopulateWithOneStandardReference(cboSRBloodSampleTakenBy, "BloodSampleTakenBy", transCharges.SRBloodSampleTakenBy);
                    cboSRBloodSampleTakenBy.SelectedValue = transCharges.SRBloodSampleTakenBy;
                }
                else
                {
                    cboSRBloodSampleTakenBy.Items.Clear();
                    cboSRBloodSampleTakenBy.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(transCharges.PhysicianSenders))
                {
                    cboPhysicianSender_ItemsRequested(cboPhysicianSender, new RadComboBoxItemsRequestedEventArgs() { Text = transCharges.PhysicianSenders });
                    cboPhysicianSender.Text = transCharges.PhysicianSenders;
                    txtPhysicianSenders.Text = transCharges.PhysicianSenders;
                }
                else
                {
                    var parId = !string.IsNullOrEmpty(reg.ParamedicID)
                                      ? reg.ParamedicID
                                      : Request.QueryString["pid"];
                    if (parId == _paramedicIdDokterLuar)
                    {
                        cboPhysicianSender_ItemsRequested(cboPhysicianSender, new RadComboBoxItemsRequestedEventArgs() { Text = reg.PhysicianSenders });
                        cboPhysicianSender.Text = reg.PhysicianSenders;
                        txtPhysicianSenders.Text = reg.PhysicianSenders;
                    }
                    else
                    {
                        var par = new Paramedic();
                        par.LoadByPrimaryKey(parId);
                        {
                            cboPhysicianSender_ItemsRequested(cboPhysicianSender, new RadComboBoxItemsRequestedEventArgs() { Text = par.ParamedicName });
                            cboPhysicianSender.Text = par.ParamedicName;
                            txtPhysicianSenders.Text = par.ParamedicName;
                        }
                    }
                }
            }

            string serviceUnitId;
            if (pnlResponUnit.Visible)
            {
                serviceUnitId = cboResponUnit.SelectedValue;
            }
            else if (Request.QueryString["type"] == "jo" || Request.QueryString["type"] == "ds")
            {
                serviceUnitId = cboToServiceUnitID.SelectedValue;
            }
            else
                serviceUnitId = cboFromServiceUnitID.SelectedValue;

            if (!string.IsNullOrEmpty(serviceUnitId))
            {
                ComboBox.PopulateWithServiceUnitForLocation(cboLocationID, serviceUnitId);
                if (!string.IsNullOrEmpty(transCharges.LocationID))
                    cboLocationID.SelectedValue = transCharges.LocationID;
                else
                    cboLocationID.SelectedIndex = 1;
            }

            txtNotes.Text = transCharges.Notes;
            txtClinicalDiagnosis.Text = transCharges.ClinicalDiagnosis;
            chkIsProceed.Checked = transCharges.IsProceed ?? false;

            txtRoomID.Text = transCharges.RoomID;
            var room = new ServiceRoom();
            if (room.LoadByPrimaryKey(txtRoomID.Text))
            {
                lblRoomName.Text = room.RoomName;
                txtTariffDiscForRoomIn.Value = Convert.ToDouble(room.TariffDiscountForRoomIn);
            }
            else
                txtTariffDiscForRoomIn.Value = 0;

            txtClassID.Text = transCharges.ClassID;
            var c = new Class();
            if (c.LoadByPrimaryKey(txtClassID.Text))
                lblClassName.Text = c.ClassName;

            if (!string.IsNullOrEmpty(transCharges.BedID))
            {
                cboBedID.SelectedValue = transCharges.ToServiceUnitID + ", " + transCharges.RoomID + ", " + transCharges.ClassID + ", " + transCharges.BedID + ", " + transCharges.RegistrationNo;
                chkIsRoomIn.Checked = transCharges.IsRoomIn ?? false;
            }
            else
                chkIsRoomIn.Checked = false;

            if (pnlSurgeryPackage.Visible && !string.IsNullOrEmpty(transCharges.SurgicalPackageID))
            {
                var query = new SurgicalPackageQuery();
                query.Select(query.PackageID, query.PackageName);
                query.Where(query.PackageID == transCharges.SurgicalPackageID);
                DataTable dtb = query.LoadDataTable();
                cboSurgeryPackageID.DataSource = dtb;
                cboSurgeryPackageID.DataBind();
                cboSurgeryPackageID.SelectedValue = transCharges.SurgicalPackageID;
            }
            if (pnlServiceUnitBookingNo.Visible && !string.IsNullOrEmpty(transCharges.ServiceUnitBookingNo))
            {
                //var query = new ServiceUnitBookingQuery("a");
                //var par = new ParamedicQuery("b");
                //query.InnerJoin(par).On(query.ParamedicID == par.ParamedicID);
                //query.Select(query.BookingNo, query.BookingDateTimeFrom, par.ParamedicName);
                //query.Where(query.BookingNo == transCharges.ServiceUnitBookingNo);
                //DataTable dtb = query.LoadDataTable();
                //cboServiceUnitBookingNo.DataSource = dtb;
                //cboServiceUnitBookingNo.DataBind();
                cboServiceUnitBookingNo.SelectedValue = transCharges.ServiceUnitBookingNo;
            }
            cboSRProdiaContractID.SelectedValue = transCharges.SRProdiaContractID;

            //Display Data Detail
            PopulateTransChargesItemGrid();
            if (Request.QueryString["type"] == "jo" || Request.QueryString["type"] == "ds")
                cboToServiceUnitID.Enabled = (TransChargesItems.Count == 0);
            cboLocationID.Enabled = (TransChargesItems.Count == 0);
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(TransCharges entity)
        {
            var physicianSenders = cboPhysicianSender.Text;
            if (Request.QueryString["type"] == "ds" && AppSession.Parameter.IsAncillaryServicePhysicianSenderFreeText)
                physicianSenders = txtPhysicianSenders.Text;

            txtTransactionNo.Text = SetEntityValue(entity, (DataModeCurrent == AppEnum.DataMode.New), txtTransactionNo.Text,
                txtRegistrationNo.Text,
                Request.QueryString["type"], _autoNumber,
                txtTransactionDate.SelectedDate.Value, txtTransactionTime.TextWithLiterals,
                txtExecutionDate.SelectedDate.Value, txtExecutionTime.TextWithLiterals,
                cboResponUnit.SelectedValue, pnlResponUnit.Visible,
                cboFromServiceUnitID.SelectedValue, cboToServiceUnitID.SelectedValue, cboLocationID.SelectedValue,
                cboTypeResult.SelectedValue, txtClassID.Text, txtRoomID.Text,
                cboBedID.SelectedValue == string.Empty ? string.Empty : cboBedID.SelectedValue.Split(',')[3].Trim(),
                chkIsRoomIn.Checked, txtTariffDiscForRoomIn.Value, chkIsProceed.Checked,
                (Request.QueryString["type"] == "ds" || Request.QueryString["type"] == "jo"),
                txtNotes.Text, cboSurgeryPackageID.SelectedValue, cboServiceUnitBookingNo.SelectedValue,
                TransChargesItems, TransChargesItemComps, TransChargesItemConsumptions,
                physicianSenders/*cboPhysicianSender.Text*//*txtPhysicianSenders.Text*/,
                cboSRProdiaContractID.SelectedValue, cboGuarantorID.SelectedValue,
                cboSRBloodSampleTakenBy.SelectedValue, cboPhysicianIDPathology.SelectedValue, cboAnalystID.SelectedValue, txtClinicalDiagnosis.Text);
        }

        public static string SetEntityValue(TransCharges entity, bool IsDataModeNew, string TransactionNo,
            string RegistrationNo,
            string QueryString_type, AppAutoNumberLast _autoNumber, DateTime TransactionDate,
            string TransactionTime, DateTime ExecutionDate, string ExecutionTime,
            string ResponUnitID, bool IsPnlResponVisible,
            string FromServiceUnitID, string ToServiceUnitID, string LocationID, string SRTypeResult,
            string ClassID, string RoomID, string BedID, bool IsRoomIn, double? TariffDiscForRoomIn,
            bool IsProceed, bool IsOrder, string Notes, string SurgeryPackageID, string ServiceUnitBookingNo,
            TransChargesItemCollection TransChargesItems,
            TransChargesItemCompCollection TransChargesItemComps,
            TransChargesItemConsumptionCollection TransChargesItemConsumptions,
            string PhysicianSenders, string SRProdiaContractID, string GuarantorID, string SRBloodSampleTakenBy, string LaboratoryParamedicID, string AnalystID, string clinicalDiagnosis)
        {
            if (IsDataModeNew)
            {
                TransactionNo = GetNewTransactionNo(QueryString_type, ref _autoNumber, TransactionDate);
                // save autonumber immediately to decrease time gap between create and save
                _autoNumber.Save();
            }

            entity.TransactionNo = TransactionNo;
            entity.RegistrationNo = RegistrationNo;
            entity.TransactionDate = DateTime.Parse(TransactionDate.ToShortDateString() + " " + TransactionTime);
            entity.ExecutionDate = DateTime.Parse(ExecutionDate.ToShortDateString() + " " + ExecutionTime);
            entity.ReferenceNo = string.Empty;
            entity.ResponUnitID = ResponUnitID;
            entity.FromServiceUnitID = FromServiceUnitID;
            entity.IsBillProceed = entity.IsApproved ?? false;
            entity.IsApproved = entity.IsApproved ?? false;

            if (QueryString_type == "tr" || QueryString_type == "mcu" || QueryString_type == "npc")
                entity.ToServiceUnitID = IsPnlResponVisible ? ResponUnitID : FromServiceUnitID;
            else
            {
                entity.ToServiceUnitID = ToServiceUnitID;
                entity.SRTypeResult = SRTypeResult;
                if (ToServiceUnitID == AppSession.Parameter.ServiceUnitLaboratoryID)
                {
                    if (QueryString_type == "jo")
                        entity.SRBloodSampleTakenBy = SRBloodSampleTakenBy;
                    else entity.SRBloodSampleTakenBy = "lab";
                }
            }
            entity.LocationID = LocationID;

            entity.ClassID = ClassID;
            entity.RoomID = RoomID;
            entity.BedID = BedID;
            entity.IsRoomIn = IsRoomIn;
            entity.TariffDiscountForRoomIn = Convert.ToDecimal(TariffDiscForRoomIn);
            entity.DueDate = TransactionDate;
            entity.SRShift = Registration.GetShiftID();
            entity.SRItemType = string.Empty;
            entity.IsProceed = IsProceed;
            entity.IsVoid = false;
            entity.IsAutoBillTransaction = false;
            entity.IsOrder = IsOrder;
            entity.IsCorrection = false;
            entity.Notes = Notes;
            entity.ClinicalDiagnosis = clinicalDiagnosis;

            entity.IsNonPatient = QueryString_type == "npc" ? true : false;

            entity.SurgicalPackageID = SurgeryPackageID;
            entity.ServiceUnitBookingNo = ServiceUnitBookingNo;

            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            entity.LaboratoryParamedicID = LaboratoryParamedicID;
            entity.AnalystID = AnalystID;

            if (IsDataModeNew)
            {
                entity.CreatedByUserID = AppSession.UserLogin.UserID;
                entity.CreatedDateTime = (new DateTime()).NowAtSqlServer();
            }

            // pastikan di TransChargesItems tidak ada mixing item package dan non-package dulu ya
            entity.IsPackage = TransChargesItems.HasPackage;//(Request.QueryString["type"] == "mcu");

            if (QueryString_type == "ds" || QueryString_type == "jo")
                entity.PhysicianSenders = PhysicianSenders;
            else
                entity.PhysicianSenders = string.Empty;

            entity.SRProdiaContractID = SRProdiaContractID;

            //Last Update Status Detail
            foreach (var item in TransChargesItems)
            {
                item.TransactionNo = entity.TransactionNo;
                item.IsBillProceed = false;
                item.IsApprove = false;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }

            if (TransChargesItemComps.Count > 0)
            {
                foreach (var comp in TransChargesItemComps)
                {
                    comp.TransactionNo = entity.TransactionNo;
                    comp.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    comp.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                }
            }
            else
            {
                #region Cek Comp

                //var reg = new Registration();
                //reg.LoadByPrimaryKey(RegistrationNo);

                var grr = new Guarantor();
                grr.LoadByPrimaryKey(GuarantorID);

                var _defaultTariffClass = AppSession.Parameter.DefaultTariffClass;
                var _defaultTariffType = AppSession.Parameter.DefaultTariffType;

                foreach (var item in TransChargesItems)
                {
                    var itm = new Item();
                    itm.LoadByPrimaryKey(item.ItemID);

                    if (itm.SRItemType != ItemType.Medical && itm.SRItemType != ItemType.NonMedical && itm.SRItemType != ItemType.Kitchen)
                    {
                        #region insert into TransChargesItemComp

                        var compColl = Helper.Tariff.GetItemTariffComponentCollection(entity.TransactionDate.Value.Date,
                                                                                      grr.SRTariffType, entity.ClassID,
                                                                                      item.ItemID);
                        if (!compColl.Any())
                            compColl = Helper.Tariff.GetItemTariffComponentCollection(entity.TransactionDate.Value.Date,
                                                                                      grr.SRTariffType,
                                                                                      _defaultTariffClass,
                                                                                      item.ItemID);
                        if (!compColl.Any())
                            compColl = Helper.Tariff.GetItemTariffComponentCollection(entity.TransactionDate.Value.Date,
                                                                                      _defaultTariffType,
                                                                                      entity.ClassID, item.ItemID);
                        if (!compColl.Any())
                            compColl = Helper.Tariff.GetItemTariffComponentCollection(entity.TransactionDate.Value.Date,
                                                                                      _defaultTariffType,
                                                                                      _defaultTariffClass,
                                                                                      item.ItemID);

                        var i = 0;
                        foreach (var comp in compColl)
                        {
                            var compCharges = TransChargesItemComps.AddNew();
                            compCharges.TransactionNo = item.TransactionNo;
                            compCharges.SequenceNo = item.SequenceNo;
                            compCharges.TariffComponentID = comp.TariffComponentID;

                            //if (comp.IsAllowVariable ?? false)
                            //{
                            //    item.IsVariable = true;
                            //    if (i == 0)
                            //        compCharges.Price = (item.Price / item.ChargeQuantity);
                            //    else
                            //        compCharges.Price = 0;
                            //}
                            //else
                            //{
                            var compPrice = Helper.Tariff.GetItemConditionRuleTariff(comp.Price ?? 0, item.ItemConditionRuleID, entity.TransactionDate.Value.Date);
                            if (entity.IsRoomIn == true && item.IsItemRoom == true)
                                compCharges.Price = compPrice - (compPrice * entity.TariffDiscountForRoomIn / 100);
                            else
                                compCharges.Price = compPrice;
                            //}

                            compCharges.DiscountAmount = (decimal)0D;
                            if (!(item.IsCito ?? false)) compCharges.CitoAmount = 0;
                            else compCharges.CitoAmount = (!item.IsCitoInPercent ?? false) ? item.BasicCitoAmount : ((item.BasicCitoAmount / 100) * compCharges.Price);

                            var tcomp = new TariffComponent();
                            tcomp.LoadByPrimaryKey(comp.TariffComponentID);
                            if (tcomp.IsTariffParamedic ?? false)
                            {
                                if (!string.IsNullOrEmpty(item.ParamedicCollectionName))
                                {
                                    var p = new ParamedicQuery();
                                    p.Where(p.ParamedicName == item.ParamedicCollectionName);
                                    p.Select(p.ParamedicID);
                                    p.es.Top = 1;

                                    DataTable dtp = p.LoadDataTable();
                                    if (dtp.Rows.Count > 0)
                                        compCharges.ParamedicID = dtp.Rows[0]["ParamedicID"].ToString();
                                    else
                                        compCharges.ParamedicID = string.Empty;
                                }
                                else
                                    compCharges.ParamedicID = string.Empty;
                            }
                            else
                                compCharges.ParamedicID = string.Empty;

                            compCharges.FeeDiscountPercentage = 0;

                            var fee = compCharges.CalculateParamedicPercentDiscount(AppSession.Parameter.IsTarifCompPhysicianDiscountMaxByShare,
                                RegistrationNo, item.ItemID, compCharges.DiscountAmount, AppSession.UserLogin.UserID,
                                entity.ClassID, entity.ToServiceUnitID);

                            compCharges.IsPackage = itm.SRItemType == ItemType.Package;
                            compCharges.LastUpdateByUserID = item.LastUpdateByUserID;
                            compCharges.LastUpdateDateTime = item.LastUpdateDateTime;

                            i += 1;
                        }
                        #endregion
                    }
                }
                #endregion
            }

            if (TransChargesItemConsumptions.Count > 0)
            {
                foreach (var cons in TransChargesItemConsumptions)
                {
                    cons.TransactionNo = entity.TransactionNo;
                    cons.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    cons.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                    if (string.IsNullOrEmpty(cons.LocationID))
                    {
                        // set pakai lokasi default
                        cons.LocationID = LocationID;
                    }
                }
            }

            return TransactionNo;
        }

        private void SaveEntity(TransCharges entity)
        {
            using (var trans = new esTransactionScope())
            {
                //autonumber has been saved on SetEntity
                //if (DataModeCurrent == DataMode.New)
                //    _autoNumber.Save();

                entity.Save();

                TransChargesItems.Save();
                TransChargesItemComps.Save();
                TransChargesItemConsumptions.Save();

                if (TransChargesItemImages != null)
                {
                    // Update ulang TransactionNo di TransChargesItemImages krn belum dilakukan di SetEntity
                    foreach (var item in TransChargesItemImages)
                    {
                        item.TransactionNo = entity.TransactionNo;
                    }
                    TransChargesItemImages.Save();
                }

                var reg = new Registration();
                reg.LoadByPrimaryKey(txtRegistrationNo.Text);
                if (pnlKiaCaseType.Visible || pnlObstetricType.Visible)
                {
                    if (pnlKiaCaseType.Visible)
                        reg.SRKiaCaseType = cboSRKiaCaseType.SelectedValue;
                    if (pnlObstetricType.Visible)
                        reg.SRObstetricType = cboSRObstetricType.SelectedValue;
                    reg.Save();
                }

                var patient = new Patient();
                patient.LoadByPrimaryKey(reg.PatientID);

                if (Request.QueryString["type"] == "ds" || Request.QueryString["type"] == "jo")
                {
                    if (cboToServiceUnitID.SelectedValue == AppSession.Parameter.ServiceUnitRadiologyID)
                    {
                        if (AppSession.Parameter.IsRadiologyNoAutoCreate & string.IsNullOrEmpty(patient.str.DiagnosticNo))
                        {
                            patient.DiagnosticNo = (new DateTime()).NowAtSqlServer().ToString(AppSession.Parameter.RadiologyNoFormat);
                            patient.Save();
                        }
                        else if (TrDiagNo.Visible)
                        {
                            patient.DiagnosticNo = txtDiagnosticNo.Text;
                            patient.Save();
                        }
                    }
                }

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new TransChargesQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where
                    (
                        que.TransactionNo > txtTransactionNo.Text,
                        que.IsOrder == (Request.QueryString["type"] == "ds" || Request.QueryString["type"] == "jo"),
                        que.IsPackage == (Request.QueryString["type"] == "mcu")
                    );
                que.OrderBy(que.TransactionNo.Descending);
            }
            else
            {
                que.Where
                    (
                        que.TransactionNo < txtTransactionNo.Text,
                        que.IsOrder == (Request.QueryString["type"] == "jo" || Request.QueryString["type"] == "jo"),
                        que.IsPackage == (Request.QueryString["type"] == "mcu")
                    );
                que.OrderBy(que.TransactionNo.Descending);
            }

            var entity = new TransCharges();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        private static string GetNewTransactionNo(string transactionType, ref AppAutoNumberLast _autoNumber, DateTime TransactionDate)
        {
            switch (transactionType)
            {
                case "tr":
                    _autoNumber = Helper.GetNewAutoNumber(TransactionDate.Date, AppEnum.AutoNumber.TransactionNo);
                    break;
                case "ds":
                    _autoNumber = Helper.GetNewAutoNumber(TransactionDate.Date, AppEnum.AutoNumber.AncillaryServiceNo);
                    break;
                case "jo":
                    _autoNumber = Helper.GetNewAutoNumber(TransactionDate.Date, AppEnum.AutoNumber.JobOrderNo);
                    break;
                case "mcu":
                    _autoNumber = Helper.GetNewAutoNumber(TransactionDate.Date, AppEnum.AutoNumber.HealthScreeningNo);
                    break;
                case "npc":
                    _autoNumber = Helper.GetNewAutoNumber(TransactionDate.Date, AppEnum.AutoNumber.NonPatientCharges);
                    break;
            }
            return _autoNumber.LastCompleteNumber;
        }

        #endregion

        #region Record Detail Method Function TransChargesItem

        private ServiceUnitCollection ServiceUnitsJobOrder
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = Session["collSUJobOrder"];
                    if (obj != null)
                        return ((ServiceUnitCollection)(obj));
                }

                var sUnits = new ServiceUnitCollection();
                sUnits.Query.Where(sUnits.Query.IsUsingJobOrder == true,
                    sUnits.Query.IsActive == true);
                sUnits.LoadAll();

                Session["collSUJobOrder"] = sUnits;
                return sUnits;
            }
            set { Session["collSUJobOrder"] = value; }
        }

        private TransChargesItemCollection TransChargesItems
        {
            get
            {
                // Diganti reset TransChargesItems pada saat page init !IsPostBack  (Handono 231109)
                //if (IsPostBack) <- Kondisi ini mengakibatkan query dibawah akan berulang kali dijalankan saat pertama kali page diload (Handono 231109)
                //{
                //    var obj = Session["collTransChargesItem" + Request.UserHostName + hdnPageId.Value];
                //    if (obj != null)
                //        return ((TransChargesItemCollection)(obj));
                //}

                var obj = Session["collTransChargesItem" + Request.UserHostName + hdnPageId.Value];
                if (obj != null)
                    return ((TransChargesItemCollection)(obj));

                var coll = new TransChargesItemCollection();
                var query = new TransChargesItemQuery("a");
                var item = new ItemQuery("b");
                var param = new ParamedicQuery("c");
                var tci = new TransChargesItemQuery("d");
                var tounit = new ServiceUnitQuery("e");
                var group = new ItemGroupQuery("f");
                var cond = new ItemConditionRuleQuery("g");

                tci.Select(tci.TransactionNo, tci.SequenceNo);
                tci.Where(tci.TransactionNo == query.TransactionNo, tci.SequenceNo == query.SequenceNo,
                          tci.IsExtraItem == true,
                          tci.IsSelectedExtraItem == false);

                var total = new esQueryItem(query, "Total", esSystemType.Decimal);
                total = ((query.ChargeQuantity * query.Price) - query.DiscountAmount) + query.CitoAmount;

                query.Select
                    (
                        query,
                        total.As("refToTransChargesItem_Total"),
                        @"<CASE WHEN ISNULL(a.FilmNo, '') = '' THEN b.ItemName ELSE b.ItemName + ' [' + a.FilmNo + ']' END AS refToItem_ItemName>",
                        //item.ItemName.As("refToItem_ItemName"),
                        param.ParamedicName.As("refToParamedic_ParamedicName"),
                        tounit.ServiceUnitName.As("refToServiceUnit_ServiceUnitName"),
                        "<CAST((CASE WHEN b.SRItemType IN ('" + ItemType.Medical + "', '" + ItemType.NonMedical + "', '" + ItemType.Kitchen + "') THEN 0 ELSE 1 END) AS BIT) AS refTo_IsItemTypeService>",
                        "<'' as refTo_ParentNoByTransactionNo>",
                        group.ItemGroupName.As("refToItemGroup_ItemGroupName"),
                        @"<CASE WHEN ISNULL(g.ItemConditionRuleName, '') = '' THEN '' ELSE '~ ' + g.ItemConditionRuleName END AS 'refToItemConditionRule_ItemConditionRuleName'>",
                        //cond.ItemConditionRuleName.As("refToItemConditionRule_ItemConditionRuleName"),
                        "<'' as refTo_PrevOrder>",
                        @"<'' AS refTo_SpecimenTypeName>",
                        @"<CASE WHEN ISNULL(a.CasemixNotes, '') = '' THEN '' ELSE ' (Casemix: ' + a.CasemixNotes +')' END AS 'refTo_CombinedNotes'>"
                    );

                query.InnerJoin(item).On(query.ItemID == item.ItemID);
                query.LeftJoin(param).On(query.ParamedicID == param.ParamedicID);
                query.LeftJoin(tounit).On(query.ToServiceUnitID == tounit.ServiceUnitID);
                query.LeftJoin(group).On(item.ItemGroupID == group.ItemGroupID);
                query.LeftJoin(cond).On(query.ItemConditionRuleID == cond.ItemConditionRuleID);

                if (Request.QueryString["type"] == "mcu")
                    query.Where(query.TransactionNo == txtTransactionNo.Text);
                else
                    query.Where(query.TransactionNo == txtTransactionNo.Text, query.NotExists(tci));

                query.OrderBy(query.SequenceNo.Ascending);
                //DataTable dtb = query.LoadDataTable();
                coll.Load(query);

                /*belum berhasil*/
                coll.SetParentNoByTransactionNo();
                coll.SetPrevOrder(RegistrationNo, AppSession.Parameter.IntervalOrderWarning);

                Session["collTransChargesItem" + Request.UserHostName + hdnPageId.Value] = coll;
                return coll;
            }
            set { Session["collTransChargesItem" + Request.UserHostName + hdnPageId.Value] = value; }
        }

        private TransChargesItemImageCollection TransChargesItemImages
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = Session["collTransChargesItemImg" + Request.UserHostName + hdnPageId.Value];
                    if (obj != null)
                        return ((TransChargesItemImageCollection)(obj));
                }

                var coll = new TransChargesItemImageCollection();
                var query = new TransChargesItemImageQuery("a");

                query.Where(query.TransactionNo == txtTransactionNo.Text);
                coll.Load(query);

                Session["collTransChargesItemImg" + Request.UserHostName + hdnPageId.Value] = coll;
                return coll;
            }
            set { Session["collTransChargesItemImg" + Request.UserHostName + hdnPageId.Value] = value; }
        }

        private void RefreshCommandItemTransChargesItem(Temiang.Avicenna.Common.AppEnum.DataMode oldVal, Temiang.Avicenna.Common.AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != Temiang.Avicenna.Common.AppEnum.DataMode.Read);
            grdTransChargesItem.Columns[0].Visible = isVisible;
            grdTransChargesItem.Columns[1].Visible = isVisible;
            grdTransChargesItem.Columns[2].Visible = isVisible;
            grdTransChargesItem.Columns[3].Visible = Request.QueryString["type"] != "jo" && isVisible;

            grdTransChargesItem.Columns[grdTransChargesItem.Columns.Count - 1].Visible = isVisible;

            if (newVal == AppEnum.DataMode.Read)
            {
                // Close item detail entry
                grdTransChargesItem.EditIndexes.Clear();
                grdTransChargesItem.MasterTableView.IsItemInserted = false;
                grdTransChargesItem.MasterTableView.ClearEditItems();
            }

            if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSSA" && !string.IsNullOrEmpty(txtPackageReferenceNo.Text))
                grdTransChargesItem.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
            else
            {
                if (Request.QueryString["type"] != "tr")
                    grdTransChargesItem.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;
                else
                {
                    var reg = new Registration();
                    reg.LoadByPrimaryKey(Request.QueryString["regno"]);

                    if (reg.SRRegistrationType == AppConstant.RegistrationType.OutPatient && Request.QueryString["resp"] == "1")
                        grdTransChargesItem.MasterTableView.CommandItemDisplay = (isVisible && !string.IsNullOrEmpty(cboResponUnit.SelectedValue)) ?
                            GridCommandItemDisplay.Top : GridCommandItemDisplay.None;
                    else
                        grdTransChargesItem.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;
                }
            }

            if (Request.QueryString["type"] == "jo" || Request.QueryString["type"] == "ds")
                cboToServiceUnitID.Enabled = (TransChargesItems.Count == 0);
            cboLocationID.Enabled = (TransChargesItems.Count == 0);

            if (oldVal != AppEnum.DataMode.Read)
            {
                TransChargesItems = null;
                TransChargesItemComps = null;
                TransChargesItemConsumptions = null;
                CostCalculations = null;
            }

            //Perbaharui tampilan dan data
            if (IsPostBack)
            {
                grdTransChargesItem.Rebind();
                Session["cboItemGroupID" + Request.UserHostName + hdnPageId.Value] = null;
            }
        }

        private void PopulateTransChargesItemGrid()
        {
            //Display Data Detail
            TransChargesItems = null; //Reset Record Detail
            grdTransChargesItem.DataSource = TransChargesItems; //Requery
            grdTransChargesItem.MasterTableView.IsItemInserted = false;
            grdTransChargesItem.MasterTableView.ClearEditItems();
            grdTransChargesItem.DataBind();

            TransChargesItemComps = null;
            var comps = TransChargesItemComps;

            TransChargesItemConsumptions = null;
            var cons = TransChargesItemConsumptions;

            CostCalculations = null;
            var cost = CostCalculations;
        }

        protected void grdTransChargesItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdTransChargesItem.DataSource = TransChargesItems;
        }

        protected void grdTransChargesItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null)
                return;

            var sequenceNo = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][TransChargesItemMetadata.ColumnNames.SequenceNo]);
            var entity = FindTransChargesItem(sequenceNo);
            if (entity != null)
            {
                SetTransChargesItemValue(entity, e, false);

                if (tblTemporaryBill.Visible)
                {
                    txtTemporaryBillTotal.Value = Convert.ToDouble(GetTotalTemporaryBill());
                }
            }

            var entityImg = FindTransChargesItemImage(sequenceNo, 1);
            SetTransChargesItemImageValue(entityImg, e);

            if (pnlResponUnit.Visible)
                cboResponUnit.Enabled = (TransChargesItems.Count == 0);
            if (Request.QueryString["type"] == "jo" || Request.QueryString["type"] == "ds")
                cboToServiceUnitID.Enabled = (TransChargesItems.Count == 0);

            cboLocationID.Enabled = (TransChargesItems.Count == 0);
            cboBedID.Enabled = Request.QueryString["disch"] != "0" && (TransChargesItems.Count == 0);
        }

        protected void grdTransChargesItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null)
                return;

            var sequenceNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][TransChargesItemMetadata.ColumnNames.SequenceNo]);
            var entity = FindTransChargesItem(sequenceNo);
            if (entity != null)
            {
                if (DataModeCurrent == AppEnum.DataMode.New)
                {
                    if (!(entity.IsPackage ?? false))
                    {
                        foreach (var detail in TransChargesItems.Where(d => d.ParentNo == entity.SequenceNo))
                        {
                            detail.MarkAsDeleted();
                        }

                        foreach (var comps in TransChargesItemComps.Where(comps => comps.SequenceNo == sequenceNo))
                        {
                            comps.MarkAsDeleted();
                        }

                        foreach (var consm in TransChargesItemConsumptions.Where(consm => consm.SequenceNo == sequenceNo))
                        {
                            consm.MarkAsDeleted();
                        }
                        //entity.MarkAsDeleted();
                    }
                    else
                    {
                        foreach (TransChargesItem pac in TransChargesItems.Where(pac => pac.ParentNo == sequenceNo || pac.SequenceNo == sequenceNo))
                        {
                            foreach (var comp in TransChargesItemComps.Where(comp => comp.SequenceNo == pac.SequenceNo))
                            {
                                comp.MarkAsDeleted();
                            }

                            foreach (var cons in TransChargesItemConsumptions.Where(cons => cons.SequenceNo == pac.SequenceNo))
                            {
                                cons.MarkAsDeleted();
                            }

                            pac.MarkAsDeleted();
                        }

                    }
                    entity.MarkAsDeleted();
                }
                else
                {
                    var transactionNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][TransChargesItemMetadata.ColumnNames.TransactionNo]);
                    var hd = new TransCharges();
                    if (hd.LoadByPrimaryKey(transactionNo))
                    {
                        if (string.IsNullOrEmpty(hd.PackageReferenceNo))
                        {
                            foreach (var detail in TransChargesItems.Where(d => d.ParentNo == entity.SequenceNo))
                            {
                                detail.MarkAsDeleted();
                            }

                            foreach (var comps in TransChargesItemComps.Where(comps => comps.SequenceNo == sequenceNo))
                            {
                                comps.MarkAsDeleted();
                            }

                            foreach (var consm in TransChargesItemConsumptions.Where(consm => consm.SequenceNo == sequenceNo))
                            {
                                consm.MarkAsDeleted();
                            }

                            entity.MarkAsDeleted();
                        }
                        else
                        {
                            entity.IsVoid = true;
                            entity.VoidByUserID = AppSession.UserLogin.UserID;
                            entity.VoidDateTime = (new DateTime()).NowAtSqlServer();
                        }
                    }
                }

                if (tblTemporaryBill.Visible)
                {
                    txtTemporaryBillTotal.Value = Convert.ToDouble(GetTotalTemporaryBill());
                }
            }

            var entityImg = FindTransChargesItemImage(sequenceNo, 1);
            if (entityImg != null)
            {
                entityImg.MarkAsDeleted();
            }

            if (pnlResponUnit.Visible)
                cboResponUnit.Enabled = (TransChargesItems.Count == 0);
            if (Request.QueryString["type"] == "jo" || Request.QueryString["type"] == "ds")
                cboToServiceUnitID.Enabled = (TransChargesItems.Count == 0);
            cboLocationID.Enabled = (TransChargesItems.Count == 0);
            cboBedID.Enabled = Request.QueryString["disch"] != "0" && (TransChargesItems.Count == 0);
        }

        protected void grdTransChargesItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = TransChargesItems.AddNew();
            SetTransChargesItemValue(entity, e, true);

            if (tblTemporaryBill.Visible)
            {
                txtTemporaryBillTotal.Value = Convert.ToDouble(GetTotalTemporaryBill());
            }

            SetTransChargesItemImageValue(null, e); // Add entitiy didalam fungsinya

            e.Canceled = true;
            grdTransChargesItem.Rebind();

            if (pnlResponUnit.Visible)
                cboResponUnit.Enabled = (TransChargesItems.Count == 0);
            if (Request.QueryString["type"] == "jo" || Request.QueryString["type"] == "ds")
                cboToServiceUnitID.Enabled = (TransChargesItems.Count == 0);
            cboLocationID.Enabled = (TransChargesItems.Count == 0);
            cboBedID.Enabled = Request.QueryString["disch"] != "0" && (TransChargesItems.Count == 0);
        }
        private TransChargesItemImage FindTransChargesItemImage(string sequenceNo, int imgNo)
        {
            return TransChargesItemImages.FirstOrDefault(rec => rec.SequenceNo.Equals(sequenceNo) && rec.ImageNo.Equals(imgNo));
        }

        private void SetTransChargesItemImageValue(TransChargesItemImage entity, GridCommandEventArgs e)
        {
            var userControl = (ItemTransactionDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                if (!string.IsNullOrWhiteSpace(userControl.ImageCaptureInString))
                {
                    // Contoh data 
                    //  - dari JCrop  -> data:image/jpeg;base64,/9j/4AAQSkZJRgABAQAAAQABAAD...
                    //  - dari CropIt -> data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAA...
                    var imgHelper = new ImageHelper();
                    var dataImage = imgHelper.ConvertBase64StringToImage(userControl.ImageCaptureInString.Split(',')[1]);

                    var resizedImg = imgHelper.ResizeImage(dataImage, new System.Drawing.Size(240, 240), true, System.Drawing.Drawing2D.InterpolationMode.Default);
                    var compressedImg = imgHelper.CompressImageToArray(resizedImg, 100); // 115KB to 14KB 
                    if (compressedImg != null)
                    {
                        if (entity == null)
                            entity = TransChargesItemImages.AddNew();

                        entity.TransactionNo = entity.TransactionNo;
                        entity.SequenceNo = userControl.SequenceNo;
                        entity.ImageNo = 1; // Krn hanya untuk 1 record image
                        entity.DocumentName = "Pictures when ordering";
                        entity.DocumentImage = compressedImg;
                    }
                }
                else if (entity != null && !string.IsNullOrWhiteSpace(entity.SequenceNo))
                {
                    entity.DocumentImage = null;
                }
            }
        }

        private TransChargesItem FindTransChargesItem(String sequenceNo)
        {
            return TransChargesItems.FirstOrDefault(rec => rec.SequenceNo.Equals(sequenceNo));
        }

        private void SetTransChargesItemValue(TransChargesItem entity, GridCommandEventArgs e, bool isInsertCommand)
        {
            var userControl = (ItemTransactionDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                SetEntityDetail(entity, userControl.SequenceNo, userControl.ItemID, userControl.ItemName, userControl.ParamedicID,
                    userControl.ParamedicName, userControl.IsAdminCalculation, userControl.IsVariable, userControl.IsCito,
                    userControl.ChargeQuantity, userControl.StockQuantity, userControl.SRItemUnit, 0,
                    userControl.Price, userControl.DiscountAmount, userControl.SRDiscountReason, userControl.IsAssetUtilization,
                    userControl.AssetID, userControl.IsPackage, userControl.IsVoid, userControl.Notes, userControl.CenterID,
                    userControl.TariffComponent, userControl.IsNewRecord, userControl.IsItemRoom, userControl.FilmNo,
                    txtTransactionNo.Text, txtRegistrationNo.Text, txtClassID.Text, cboToServiceUnitID.SelectedValue,
                    TransChargesItemComps, userControl.TariffDate ?? (new DateTime()).NowAtSqlServer().Date,
                    TransChargesItems, TransChargesItemConsumptions, string.Empty,
                    userControl.SRCitoPercentage, userControl.ItemConditionRuleID, userControl.ItemConditionRuleName, userControl.IsItemTypeService, Request.QueryString["type"], cboToServiceUnitID.SelectedValue);
            }
        }

        public static void SetEntityDetail(TransChargesItem entity, string sequenceNo, string itemID, string itemName, string paramedicID,
            string paramedicName, bool? isAdminCalculation, bool? isVariable, bool? isCito, decimal? chargeQuantity,
            decimal? stockQuantity, string srItemUnit, decimal? costPrice, decimal? price, decimal? discountAmount,
            string srDiscountReason, bool? isAssetUtilization, string assetID, bool? isPackage, bool? isVoid, string notes, string centerID,
            IEnumerable<TransChargesItemComp> tariffComponents, bool isNewRecord, bool? isItemRoom, string filmNo,
            string TransactionNo, string RegistrationNo, string ClassID, string ToServiceUnitID,
            TransChargesItemCompCollection TransChargesItemComps, DateTime TransactionDate,
            TransChargesItemCollection TransChargesItems,
            TransChargesItemConsumptionCollection TransChargesItemConsumptions, string GuarantorID,
            string SRCitoPercentage, string itemConditionRuleId, string itemConditionRuleName, bool isItemTypeService, string transType, string toServiceUnitID)
        {
            entity.TransactionNo = TransactionNo;
            entity.SequenceNo = sequenceNo;
            entity.ParentNo = string.Empty;
            entity.ReferenceNo = string.Empty;
            entity.ReferenceSequenceNo = string.Empty;
            entity.ItemID = itemID;
            entity.ItemName = itemName;

            var reg = new Registration();
            reg.LoadByPrimaryKey(RegistrationNo);

            entity.ChargeClassID = ClassID;
            entity.ParamedicID = paramedicID;
            entity.ParamedicName = paramedicName;
            entity.IsAdminCalculation = isAdminCalculation;
            entity.IsVariable = isVariable;
            entity.IsCito = isCito;
            entity.ChargeQuantity = chargeQuantity;
            entity.StockQuantity = stockQuantity;
            entity.SRItemUnit = srItemUnit;
            entity.CostPrice = costPrice;
            //entity.Price = price;
            entity.Price = Helper.Tariff.GetItemConditionRuleTariff(price ?? 0, itemConditionRuleId, TransactionDate);

            var grr = new Guarantor();
            grr.LoadByPrimaryKey(reg.GuarantorID ?? GuarantorID);
            //if (grr.TariffCalculationMethod == 1) TransactionDate = reg.RegistrationDate.Value.Date;
            entity.TariffDate = TransactionDate.Date;

            if (!(entity.IsCito ?? false))
            {
                entity.CitoAmount = 0;
                entity.IsCitoInPercent = false;
                entity.BasicCitoAmount = 0;
                entity.SRCitoPercentage = string.Empty;
            }
            else
            {
                var tariff = (Helper.Tariff.GetItemTariff(TransactionDate, grr.SRTariffType, entity.ChargeClassID, entity.ChargeClassID, entity.ItemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
                              Helper.Tariff.GetItemTariff(TransactionDate, grr.SRTariffType, AppSession.Parameter.DefaultTariffClass, entity.ChargeClassID, entity.ItemID, reg.GuarantorID, false, reg.SRRegistrationType)) ??
                             (Helper.Tariff.GetItemTariff(TransactionDate, AppSession.Parameter.DefaultTariffType, entity.ChargeClassID, entity.ChargeClassID, entity.ItemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
                              Helper.Tariff.GetItemTariff(TransactionDate, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, entity.ChargeClassID, entity.ItemID, reg.GuarantorID, false, reg.SRRegistrationType));

                tariff.UpdateCitoFromStdRef(SRCitoPercentage);

                entity.CitoAmount = (!tariff.IsCitoInPercent ?? false)
                                        ? (chargeQuantity * tariff.CitoValue)
                                        : (chargeQuantity * ((tariff.CitoValue / 100) * entity.Price));
                entity.IsCitoInPercent = tariff.IsCitoInPercent ?? false;
                entity.BasicCitoAmount = tariff.CitoValue;
                entity.SRCitoPercentage = (tariff.IsCitoFromStandardReference ?? false) ? SRCitoPercentage : string.Empty;
            }

            entity.RoundingAmount = Helper.RoundingDiff;
            entity.SRDiscountReason = srDiscountReason;
            entity.IsAssetUtilization = isAssetUtilization;
            entity.AssetID = assetID;
            entity.IsBillProceed = false;
            entity.IsOrderRealization = false;
            entity.IsPaymentConfirmed = false;
            entity.IsPackage = isPackage;
            entity.IsVoid = isVoid;
            entity.Notes = notes;
            entity.IsItemTypeService = isItemTypeService;
            entity.SRCenterID = centerID;
            entity.IsApprove = false;
            entity.IsItemRoom = isItemRoom;
            entity.FilmNo = filmNo;
            entity.ItemConditionRuleID = itemConditionRuleId;
            entity.ItemConditionRuleName = !string.IsNullOrEmpty(itemConditionRuleName) ? "~ " + itemConditionRuleName : itemConditionRuleName;

            if (isNewRecord)
            {
                entity.CreatedByUserID = AppSession.UserLogin.UserID;
                entity.CreatedDateTime = (new DateTime()).NowAtSqlServer();
            }

            if (Helper.GuarantorBpjsCasemix.Contains(reg.GuarantorID) && AppSession.Parameter.CasemixValidationRegistrationType.Contains(reg.SRRegistrationType))
                entity.IsCasemixApproved = Helper.IsCasemixApproved(entity.ItemID, entity.ChargeQuantity ?? 0, reg.RegistrationNo, entity.TransactionNo, reg.GuarantorID, false);
            else
                entity.IsCasemixApproved = true;


            string p = string.Empty;

            //Item Tariff Component
            if (tariffComponents != null)
            {
                if (tariffComponents.Any())
                {
                    // sum ulang diskon
                    discountAmount = 0;

                    foreach (var comp in tariffComponents)
                    {
                        TransChargesItemComp item = FindTransChargesItemComp(TransChargesItemComps, comp.SequenceNo, comp.TariffComponentID);
                        if (item == null)
                        {
                            item = TransChargesItemComps.AddNew();
                            item.TransactionNo = TransactionNo;
                            item.SequenceNo = comp.SequenceNo;
                        }

                        item.TariffComponentID = comp.TariffComponentID;
                        //item.Price = comp.Price ?? 0;
                        //item.DiscountAmount = comp.DiscountAmount;
                        item.Price = Helper.Tariff.GetItemConditionRuleTariff(comp.Price ?? 0, itemConditionRuleId, TransactionDate);
                        item.DiscountAmount = Helper.Tariff.GetItemConditionRuleTariff(comp.DiscountAmount ?? 0, itemConditionRuleId, TransactionDate); ;

                        if (!(entity.IsCito ?? false)) item.CitoAmount = 0;
                        else
                        {
                            decimal citoamt = 0;
                            if (entity.IsCitoInPercent ?? false)
                            {
                                item.CitoAmount = (entity.BasicCitoAmount / 100) * item.Price;
                            }
                            else
                            {
                                item.CitoAmount = (item.Price) / (entity.Price) * entity.BasicCitoAmount;
                            }
                        }
                        item.ParamedicID = comp.ParamedicID;

                        item.FeeDiscountPercentage = comp.FeeDiscountPercentage ?? 0;

                        var fee = item.CalculateParamedicPercentDiscount(AppSession.Parameter.IsTarifCompPhysicianDiscountMaxByShare,
                            reg.RegistrationNo, entity.ItemID, item.DiscountAmount, AppSession.UserLogin.UserID,
                            ClassID, ToServiceUnitID);

                        item.IsPackage = false;

                        if (!string.IsNullOrEmpty(item.ParamedicID))
                        {
                            var tComp = new TariffComponent();
                            if (tComp.LoadByPrimaryKey(item.TariffComponentID))
                            {
                                if (tComp.IsPrintParamedicInSlip ?? false)
                                {
                                    var par = new Paramedic();
                                    par.LoadByPrimaryKey(item.ParamedicID);
                                    if (par.IsPrintInSlip ?? true)
                                    {
                                        if (p.Length == 0) p = par.ParamedicName;
                                        else if (!p.Contains(par.ParamedicName))
                                            p = p + "; " + par.ParamedicName;
                                    }
                                }
                            }
                        }
                        discountAmount += item.DiscountAmount ?? 0;
                    }
                    discountAmount = entity.ChargeQuantity * discountAmount;
                }
                else
                {
                    var comps = Helper.Tariff.GetItemTariffComponentCollection(TransactionDate, grr.SRTariffType,
                                        entity.ChargeClassID, itemID);
                    if (!comps.Any())
                        comps = Helper.Tariff.GetItemTariffComponentCollection(TransactionDate,
                            grr.SRTariffType, AppSession.Parameter.DefaultTariffClass, itemID);
                    if (!comps.Any())
                        comps = Helper.Tariff.GetItemTariffComponentCollection(TransactionDate,
                            AppSession.Parameter.DefaultTariffType, entity.ChargeClassID, itemID);
                    if (!comps.Any())
                        comps = Helper.Tariff.GetItemTariffComponentCollection(TransactionDate,
                            AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, itemID);

                    foreach (var comp in comps)
                    {
                        //var tcomp = TransChargesItemComps.AddNew();
                        //tcomp.TransactionNo = entity.TransactionNo;
                        //tcomp.SequenceNo = entity.SequenceNo;
                        //tcomp.TariffComponentID = comp.TariffComponentID;

                        TransChargesItemComp tcomp = FindTransChargesItemComp(TransChargesItemComps, entity.SequenceNo, comp.TariffComponentID);
                        if (tcomp == null)
                        {
                            tcomp = TransChargesItemComps.AddNew();
                            tcomp.TransactionNo = entity.TransactionNo;
                            tcomp.SequenceNo = entity.SequenceNo;
                            tcomp.TariffComponentID = comp.TariffComponentID;
                        }

                        var tc = new TariffComponent();
                        tc.LoadByPrimaryKey(tcomp.TariffComponentID);
                        tcomp.TariffComponentName = tc.TariffComponentName;
                        tcomp.Price = Helper.Tariff.GetItemConditionRuleTariff(comp.Price ?? 0, itemConditionRuleId, TransactionDate);
                        tcomp.DiscountAmount = 0;
                        tcomp.CitoAmount = 0;
                        tcomp.ParamedicID = string.Empty;
                        tcomp.IsPackage = false;
                    }
                }
            }

            entity.ParamedicCollectionName = p;
            entity.DiscountAmount = discountAmount;
            decimal tot = (entity.ChargeQuantity.Value * entity.Price.Value) - entity.DiscountAmount.Value + entity.CitoAmount.Value;
            entity.Total = Helper.Rounding(tot, AppEnum.RoundingType.Transaction);

            if (isNewRecord)
            {
                TransChargesItemConsumption consItem;

                if (entity.IsPackage ?? false)
                {
                    var pacs = new ItemPackageCollection();
                    pacs.Query.Where(
                        pacs.Query.ItemID == entity.ItemID &&
                        pacs.Query.IsExtraItem == false &&
                        pacs.Query.IsActive == true
                        );
                    pacs.LoadAll();

                    bool isFromItemPackComp = false;
                    var collItemPackageComp = new ItemPackageTariffComponentCollection();
                    collItemPackageComp.Query.Where(collItemPackageComp.Query.ItemID == entity.ItemID);
                    collItemPackageComp.LoadAll();
                    if (collItemPackageComp.Count > 0)
                        isFromItemPackComp = true;

                    foreach (var pac in pacs)
                    {
                        var ent = TransChargesItems.AddNew();
                        ent.TransactionNo = entity.TransactionNo;

                        var seq = (TransChargesItems.Where(c => c.ParentNo == entity.SequenceNo)
                                                    .OrderByDescending(c => c.SequenceNo)
                                                    .Select(c => c.SequenceNo.Substring(3, 3))).Take(1).SingleOrDefault();

                        ent.SequenceNo = entity.SequenceNo + string.Format("{0:000}", int.Parse((seq ?? "0")) + 1);
                        ent.ParentNo = entity.SequenceNo;
                        ent.ReferenceNo = string.Empty;
                        ent.ReferenceSequenceNo = string.Empty;
                        ent.ItemID = pac.DetailItemID;

                        var i = new Item();
                        i.LoadByPrimaryKey(ent.ItemID);
                        ent.ItemName = i.ItemName;
                        ent.ChargeClassID = entity.ChargeClassID;
                        ent.ParamedicID = string.Empty;
                        ent.IsAdminCalculation = false;
                        ent.IsVariable = false;
                        ent.IsCito = false;
                        ent.ChargeQuantity = chargeQuantity * pac.Quantity;

                        switch (i.SRItemType)
                        {
                            case ItemType.Medical:
                                ent.StockQuantity = ent.ChargeQuantity;

                                var ipm = new ItemProductMedic();
                                ipm.LoadByPrimaryKey(pac.DetailItemID);

                                ent.CostPrice = ipm.CostPrice ?? 0;
                                break;
                            case ItemType.NonMedical:
                                ent.StockQuantity = ent.ChargeQuantity;

                                var ipn = new ItemProductNonMedic();
                                ipn.LoadByPrimaryKey(pac.DetailItemID);

                                ent.CostPrice = ipn.CostPrice ?? 0;
                                break;
                            case ItemType.Kitchen:
                                ent.StockQuantity = ent.ChargeQuantity;

                                var ik = new ItemKitchen();
                                ik.LoadByPrimaryKey(pac.DetailItemID);

                                ent.CostPrice = ik.CostPrice ?? 0;
                                break;
                            default:
                                ent.StockQuantity = 0;
                                ent.CostPrice = 0;
                                break;
                        }

                        ent.SRItemUnit = pac.SRItemUnit;
                        ent.CitoAmount = 0;
                        ent.RoundingAmount = 0;
                        ent.SRDiscountReason = string.Empty;
                        ent.IsAssetUtilization = false;
                        ent.AssetID = string.Empty;
                        ent.IsBillProceed = false;
                        ent.IsOrderRealization = false;
                        ent.IsPaymentConfirmed = false;
                        ent.IsPackage = false;
                        ent.IsVoid = false;
                        ent.Notes = string.Empty;
                        ent.IsItemTypeService = i.SRItemType != ItemType.Medical && i.SRItemType != ItemType.NonMedical && i.SRItemType != ItemType.Kitchen;
                        ent.ToServiceUnitID = pac.ServiceUnitID;

                        var unit = new ServiceUnit();
                        if (unit.LoadByPrimaryKey(ent.ToServiceUnitID))
                            ent.ServiceUnitName = unit.ServiceUnitName;

                        ent.IsCitoInPercent = false;
                        ent.BasicCitoAmount = 0;
                        ent.IsItemRoom = false;
                        ent.SRCitoPercentage = string.Empty;
                        ent.ItemConditionRuleID = itemConditionRuleId;

                        decimal pricePackage = 0;
                        decimal discPackage = 0;
                        decimal packageDiscValue = pac.DiscountValue ?? 0;

                        switch (i.SRItemType)
                        {
                            case ItemType.Medical:
                            case ItemType.NonMedical:
                            case ItemType.Kitchen:
                                if (isFromItemPackComp == true)
                                {
                                    var tariffCompPack = new ItemPackageTariffComponentCollection();
                                    tariffCompPack.Query.Where(tariffCompPack.Query.ItemID == pac.ItemID, tariffCompPack.Query.DetailItemID == pac.DetailItemID);
                                    tariffCompPack.LoadAll();
                                    if (tariffCompPack.Count > 0)
                                    {
                                        var comp = tariffCompPack.First();
                                        //pricePackage = comp.Price ?? 0;
                                        //discPackage = comp.Discount ?? 0;
                                        pricePackage = Helper.Tariff.GetItemConditionRuleTariff(comp.Price ?? 0, itemConditionRuleId, TransactionDate);
                                        discPackage = Helper.Tariff.GetItemConditionRuleTariff(comp.Discount ?? 0, itemConditionRuleId, TransactionDate);
                                    }
                                }
                                else
                                {
                                    var tariff = (Helper.Tariff.GetItemTariff(TransactionDate, grr.SRTariffType, entity.ChargeClassID, entity.ChargeClassID, ent.ItemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
                                      Helper.Tariff.GetItemTariff(TransactionDate, grr.SRTariffType, AppSession.Parameter.DefaultTariffClass, entity.ChargeClassID, ent.ItemID, reg.GuarantorID, false, reg.SRRegistrationType)) ??
                                     (Helper.Tariff.GetItemTariff(TransactionDate, AppSession.Parameter.DefaultTariffType, entity.ChargeClassID, entity.ChargeClassID, ent.ItemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
                                      Helper.Tariff.GetItemTariff(TransactionDate, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, entity.ChargeClassID, ent.ItemID, reg.GuarantorID, false, reg.SRRegistrationType));

                                    //pricePackage = tariff.Price ?? 0;
                                    pricePackage = Helper.Tariff.GetItemConditionRuleTariff(tariff.Price ?? 0, itemConditionRuleId, TransactionDate);
                                }
                                break;
                            case ItemType.Diagnostic:
                            case ItemType.Laboratory:
                            case ItemType.Package:
                            case ItemType.Radiology:
                            case ItemType.Service:
                                //kl ItemPackageTariffComponent ada isinya maka semua ambil dari situ karena komponen yg ada di itemtariff tidak selalu sama
                                // dengan yg sudah disetting di ItemPackageTariffComponent sehingga cara lama gak bisa dipakai karena detail di transchargeitemcomp jadi salah
                                if (isFromItemPackComp == true)
                                {
                                    var tariffCompPack = new ItemPackageTariffComponentCollection();
                                    tariffCompPack.Query.Where(tariffCompPack.Query.ItemID == pac.ItemID, tariffCompPack.Query.DetailItemID == pac.DetailItemID);
                                    tariffCompPack.LoadAll();

                                    if (tariffCompPack.Count > 0)
                                    {
                                        foreach (var comp in tariffCompPack)
                                        {
                                            var tcomp = TransChargesItemComps.AddNew();
                                            tcomp.TransactionNo = entity.TransactionNo;
                                            tcomp.SequenceNo = ent.SequenceNo;
                                            tcomp.TariffComponentID = comp.TariffComponentID;

                                            var tc = new TariffComponent();
                                            tc.LoadByPrimaryKey(tcomp.TariffComponentID);
                                            tcomp.TariffComponentName = tc.TariffComponentName;
                                            //tcomp.Price = comp.Price ?? 0;
                                            //tcomp.DiscountAmount = comp.Discount ?? 0;
                                            tcomp.Price = Helper.Tariff.GetItemConditionRuleTariff(comp.Price ?? 0, itemConditionRuleId, TransactionDate);
                                            tcomp.DiscountAmount = Helper.Tariff.GetItemConditionRuleTariff(comp.Discount ?? 0, itemConditionRuleId, TransactionDate);

                                            tcomp.CitoAmount = 0;
                                            tcomp.ParamedicID = string.Empty;
                                            tcomp.IsPackage = true;

                                            pricePackage += tcomp.Price ?? 0;
                                            discPackage += tcomp.DiscountAmount ?? 0;
                                        }
                                    }
                                }
                                else
                                {
                                    var comps = Helper.Tariff.GetItemTariffComponentCollection(TransactionDate, grr.SRTariffType,
                                        entity.ChargeClassID, ent.ItemID);
                                    if (!comps.Any())
                                        comps = Helper.Tariff.GetItemTariffComponentCollection(TransactionDate,
                                            grr.SRTariffType, AppSession.Parameter.DefaultTariffClass, ent.ItemID);
                                    if (!comps.Any())
                                        comps = Helper.Tariff.GetItemTariffComponentCollection(TransactionDate,
                                            AppSession.Parameter.DefaultTariffType, entity.ChargeClassID, ent.ItemID);
                                    if (!comps.Any())
                                        comps = Helper.Tariff.GetItemTariffComponentCollection(TransactionDate,
                                            AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, ent.ItemID);

                                    foreach (var comp in comps)
                                    {
                                        var tcomp = TransChargesItemComps.AddNew();
                                        tcomp.TransactionNo = entity.TransactionNo;
                                        tcomp.SequenceNo = ent.SequenceNo;
                                        tcomp.TariffComponentID = comp.TariffComponentID;

                                        var tc = new TariffComponent();
                                        tc.LoadByPrimaryKey(tcomp.TariffComponentID);
                                        tcomp.TariffComponentName = tc.TariffComponentName;
                                        //tcomp.Price = comp.Price ?? 0;
                                        tcomp.Price = Helper.Tariff.GetItemConditionRuleTariff(comp.Price ?? 0, itemConditionRuleId, TransactionDate);
                                        tcomp.DiscountAmount = tcomp.Price * packageDiscValue / 100;
                                        tcomp.CitoAmount = 0;
                                        tcomp.ParamedicID = string.Empty;
                                        tcomp.IsPackage = true;

                                        pricePackage += tcomp.Price ?? 0;
                                        discPackage += tcomp.DiscountAmount ?? 0;
                                    }
                                }

                                // consumption
                                var cons = new ItemConsumptionCollection();
                                cons.Query.Where(cons.Query.ItemID == pac.DetailItemID);
                                cons.LoadAll();

                                foreach (var consEntity in cons)
                                {
                                    consItem = TransChargesItemConsumptions.AddNew();
                                    consItem.TransactionNo = entity.TransactionNo;
                                    consItem.SequenceNo = ent.SequenceNo;
                                    consItem.DetailItemID = consEntity.DetailItemID;

                                    i = new Item();
                                    i.LoadByPrimaryKey(consItem.DetailItemID);
                                    consItem.ItemName = i.ItemName;

                                    consItem.Qty = chargeQuantity * consEntity.Qty;
                                    consItem.QtyRealization = consItem.Qty;
                                    consItem.MaxValue = AppSession.Parameter.IsValidateMaxQtyItemConsumptions ? consItem.Qty : 1000;
                                    consItem.SRItemUnit = consEntity.SRItemUnit;

                                    var tariff = (Helper.Tariff.GetItemTariff(TransactionDate, grr.SRTariffType, entity.ChargeClassID, entity.ChargeClassID, consItem.DetailItemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
                                     Helper.Tariff.GetItemTariff(TransactionDate, grr.SRTariffType, AppSession.Parameter.DefaultTariffClass, entity.ChargeClassID, consItem.DetailItemID, reg.GuarantorID, false, reg.SRRegistrationType)) ??
                                    (Helper.Tariff.GetItemTariff(TransactionDate, AppSession.Parameter.DefaultTariffType, entity.ChargeClassID, entity.ChargeClassID, consItem.DetailItemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
                                     Helper.Tariff.GetItemTariff(TransactionDate, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, entity.ChargeClassID, consItem.DetailItemID, reg.GuarantorID, false, reg.SRRegistrationType));

                                    consItem.Price = tariff.Price ?? 0;

                                    switch (i.SRItemType)
                                    {
                                        case ItemType.Medical:
                                            var im = new ItemProductMedic();
                                            im.LoadByPrimaryKey(i.ItemID);
                                            consItem.AveragePrice = im.CostPrice;
                                            consItem.FifoPrice = im.PriceInBaseUnit;
                                            break;
                                        case ItemType.NonMedical:
                                            var inm = new ItemProductNonMedic();
                                            inm.LoadByPrimaryKey(i.ItemID);
                                            consItem.AveragePrice = inm.CostPrice;
                                            consItem.FifoPrice = inm.PriceInBaseUnit;
                                            break;
                                        case ItemType.Kitchen:
                                            var ik = new ItemKitchen();
                                            ik.LoadByPrimaryKey(i.ItemID);
                                            consItem.AveragePrice = ik.CostPrice;
                                            consItem.FifoPrice = ik.PriceInBaseUnit;
                                            break;
                                        default:
                                            consItem.AveragePrice = consItem.Price;
                                            consItem.FifoPrice = consItem.Price;
                                            break;
                                    }

                                    consItem.IsPackage = true;
                                }
                                break;
                            default:
                                if (pac.IsStockControl ?? false)
                                {
                                    consItem = TransChargesItemConsumptions.AddNew();
                                    consItem.TransactionNo = entity.TransactionNo;
                                    consItem.SequenceNo = ent.SequenceNo;
                                    consItem.DetailItemID = pac.DetailItemID;

                                    i = new Item();
                                    i.LoadByPrimaryKey(consItem.DetailItemID);
                                    consItem.ItemName = i.ItemName;

                                    consItem.Qty = chargeQuantity * pac.Quantity;
                                    consItem.QtyRealization = consItem.Qty;
                                    consItem.MaxValue = AppSession.Parameter.IsValidateMaxQtyItemConsumptions ? consItem.Qty : 1000;
                                    consItem.SRItemUnit = pac.SRItemUnit;

                                    var tariff = (Helper.Tariff.GetItemTariff(TransactionDate, grr.SRTariffType, entity.ChargeClassID, entity.ChargeClassID, consItem.DetailItemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
                                     Helper.Tariff.GetItemTariff(TransactionDate, grr.SRTariffType, AppSession.Parameter.DefaultTariffClass, entity.ChargeClassID, consItem.DetailItemID, reg.GuarantorID, false, reg.SRRegistrationType)) ??
                                    (Helper.Tariff.GetItemTariff(TransactionDate, AppSession.Parameter.DefaultTariffType, entity.ChargeClassID, entity.ChargeClassID, consItem.DetailItemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
                                     Helper.Tariff.GetItemTariff(TransactionDate, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, entity.ChargeClassID, consItem.DetailItemID, reg.GuarantorID, false, reg.SRRegistrationType));

                                    consItem.Price = tariff.Price ?? 0;

                                    switch (i.SRItemType)
                                    {
                                        case ItemType.Medical:
                                            var im = new ItemProductMedic();
                                            im.LoadByPrimaryKey(i.ItemID);
                                            consItem.AveragePrice = im.CostPrice;
                                            consItem.FifoPrice = im.PriceInBaseUnit;
                                            break;
                                        case ItemType.NonMedical:
                                            var inm = new ItemProductNonMedic();
                                            inm.LoadByPrimaryKey(i.ItemID);
                                            consItem.AveragePrice = inm.CostPrice;
                                            consItem.FifoPrice = inm.PriceInBaseUnit;
                                            break;
                                        case ItemType.Kitchen:
                                            var ik = new ItemKitchen();
                                            ik.LoadByPrimaryKey(i.ItemID);
                                            consItem.AveragePrice = ik.CostPrice;
                                            consItem.FifoPrice = ik.PriceInBaseUnit;
                                            break;
                                        default:
                                            consItem.AveragePrice = consItem.Price;
                                            consItem.FifoPrice = consItem.Price;
                                            break;
                                    }

                                    consItem.IsPackage = true;
                                }
                                break;
                        }

                        ent.Price = pricePackage;
                        ent.DiscountAmount = discPackage * ent.ChargeQuantity;
                        ent.IsExtraItem = false;
                        ent.IsSelectedExtraItem = false;

                        if (i.SRItemType == ItemType.Laboratory)// && AppSession.Parameter.IsUsingHisInterop == "No")
                        {
                            var labs = new ItemLaboratoryProfileCollection();
                            labs.Query.Where(labs.Query.ParentItemID == ent.ItemID);
                            labs.Query.OrderBy(labs.Query.DisplaySequence.Ascending);
                            if (labs.Query.Load())
                            {
                                foreach (var lab in labs)
                                {
                                    var entityLab = TransChargesItems.AddNew();
                                    entityLab.TransactionNo = TransactionNo;

                                    sequenceNo = (TransChargesItems.Where(c => c.ParentNo == ent.SequenceNo).OrderByDescending(c => c.SequenceNo).Select(c => c.SequenceNo.Substring(c.SequenceNo.Length - 3, 3))).Take(1).SingleOrDefault();
                                    entityLab.SequenceNo = ent.SequenceNo + string.Format("{0:000}", string.IsNullOrEmpty(sequenceNo) ? 1 : int.Parse(sequenceNo) + 1);

                                    entityLab.ParentNo = ent.SequenceNo;
                                    entityLab.ReferenceNo = string.Empty;
                                    entityLab.ReferenceSequenceNo = string.Empty;
                                    entityLab.ItemID = lab.DetailItemID;
                                    entityLab.ItemName = string.Empty;
                                    entityLab.ChargeClassID = ClassID;
                                    entityLab.ParamedicID = paramedicID;
                                    entityLab.ParamedicName = paramedicName;
                                    entityLab.IsAdminCalculation = false;
                                    entityLab.IsVariable = false;
                                    entityLab.IsCito = false;
                                    entityLab.ChargeQuantity = 0;
                                    entityLab.StockQuantity = 0;
                                    entityLab.SRItemUnit = srItemUnit;
                                    entityLab.CostPrice = 0;
                                    entityLab.Price = 0;
                                    entityLab.CitoAmount = 0;
                                    entityLab.IsCitoInPercent = false;
                                    entityLab.BasicCitoAmount = 0;
                                    entityLab.RoundingAmount = 0;
                                    entityLab.SRDiscountReason = string.Empty;
                                    entityLab.IsAssetUtilization = false;
                                    entityLab.AssetID = string.Empty;
                                    entityLab.IsBillProceed = false;
                                    entityLab.IsOrderRealization = false;
                                    entityLab.IsPaymentConfirmed = false;
                                    entityLab.IsPackage = false;
                                    entityLab.IsVoid = isVoid;
                                    entityLab.Notes = string.Empty;
                                    entityLab.IsItemTypeService = true;
                                    entityLab.SRCenterID = string.Empty;
                                    entityLab.IsApprove = false;
                                    entityLab.IsItemRoom = false;
                                    entityLab.ToServiceUnitID = ent.ToServiceUnitID;
                                    entityLab.SRCitoPercentage = string.Empty;

                                    if (isNewRecord)
                                    {
                                        entityLab.CreatedByUserID = AppSession.UserLogin.UserID;
                                        entityLab.CreatedDateTime = (new DateTime()).NowAtSqlServer();
                                    }

                                    //cek anak level 2
                                    var labs2 = new ItemLaboratoryProfileCollection();
                                    labs2.Query.Where(labs2.Query.ParentItemID == entityLab.ItemID);
                                    labs2.Query.OrderBy(labs2.Query.DisplaySequence.Ascending);
                                    if (labs2.Query.Load())
                                    {
                                        foreach (var lab2 in labs2)
                                        {
                                            var entityLab2 = TransChargesItems.AddNew();
                                            entityLab2.TransactionNo = TransactionNo;

                                            sequenceNo = (TransChargesItems.Where(c => c.ParentNo == entityLab.SequenceNo).OrderByDescending(c => c.SequenceNo).Select(c => c.SequenceNo.Substring(c.SequenceNo.Length - 3, 3))).Take(1).SingleOrDefault();
                                            entityLab2.SequenceNo = entityLab.SequenceNo + string.Format("{0:000}", string.IsNullOrEmpty(sequenceNo) ? 1 : int.Parse(sequenceNo) + 1);

                                            entityLab2.ParentNo = entityLab.SequenceNo;
                                            entityLab2.ReferenceNo = string.Empty;
                                            entityLab2.ReferenceSequenceNo = string.Empty;
                                            entityLab2.ItemID = lab2.DetailItemID;
                                            entityLab2.ItemName = string.Empty;
                                            entityLab2.ChargeClassID = ClassID;
                                            entityLab2.ParamedicID = paramedicID;
                                            entityLab2.ParamedicName = paramedicName;
                                            entityLab2.IsAdminCalculation = false;
                                            entityLab2.IsVariable = false;
                                            entityLab2.IsCito = false;
                                            entityLab2.ChargeQuantity = 0;
                                            entityLab2.StockQuantity = 0;
                                            entityLab2.SRItemUnit = srItemUnit;
                                            entityLab2.CostPrice = 0;
                                            entityLab2.Price = 0;
                                            entityLab2.CitoAmount = 0;
                                            entityLab2.IsCitoInPercent = false;
                                            entityLab2.BasicCitoAmount = 0;
                                            entityLab2.RoundingAmount = 0;
                                            entityLab2.SRDiscountReason = string.Empty;
                                            entityLab2.IsAssetUtilization = false;
                                            entityLab2.AssetID = string.Empty;
                                            entityLab2.IsBillProceed = false;
                                            entityLab2.IsOrderRealization = false;
                                            entityLab2.IsPaymentConfirmed = false;
                                            entityLab2.IsPackage = false;
                                            entityLab2.IsVoid = isVoid;
                                            entityLab2.Notes = string.Empty;
                                            entityLab2.IsItemTypeService = true;
                                            entityLab2.SRCenterID = string.Empty;
                                            entityLab2.IsApprove = false;
                                            entityLab2.IsItemRoom = false;
                                            entityLab2.ToServiceUnitID = ent.ToServiceUnitID;
                                            entityLab2.SRCitoPercentage = string.Empty;

                                            if (isNewRecord)
                                            {
                                                entityLab2.CreatedByUserID = AppSession.UserLogin.UserID;
                                                entityLab2.CreatedDateTime = (new DateTime()).NowAtSqlServer();
                                            }

                                            //cek anak level 3
                                            var labs3 = new ItemLaboratoryProfileCollection();
                                            labs3.Query.Where(labs3.Query.ParentItemID == entityLab2.ItemID);
                                            labs3.Query.OrderBy(labs3.Query.DisplaySequence.Ascending);
                                            if (labs3.Query.Load())
                                            {
                                                foreach (var lab3 in labs3)
                                                {
                                                    var entityLab3 = TransChargesItems.AddNew();
                                                    entityLab3.TransactionNo = string.Empty;

                                                    sequenceNo = (TransChargesItems.Where(c => c.ParentNo == entityLab2.SequenceNo).OrderByDescending(c => c.SequenceNo).Select(c => c.SequenceNo.Substring(c.SequenceNo.Length - 3, 3))).Take(1).SingleOrDefault();
                                                    entityLab3.SequenceNo = entityLab2.SequenceNo + string.Format("{0:000}", string.IsNullOrEmpty(sequenceNo) ? 1 : int.Parse(sequenceNo) + 1);

                                                    entityLab3.ParentNo = entityLab2.SequenceNo;
                                                    entityLab3.ReferenceNo = string.Empty;
                                                    entityLab3.ReferenceSequenceNo = string.Empty;
                                                    entityLab3.ItemID = lab3.DetailItemID;
                                                    entityLab3.ItemName = string.Empty;
                                                    entityLab3.ChargeClassID = string.Empty;
                                                    entityLab3.ParamedicID = string.Empty;
                                                    entityLab3.ParamedicName = string.Empty;
                                                    entityLab3.IsAdminCalculation = false;
                                                    entityLab3.IsVariable = false;
                                                    entityLab3.IsCito = false;
                                                    entityLab3.ChargeQuantity = 0;
                                                    entityLab3.StockQuantity = 0;
                                                    entityLab3.SRItemUnit = "x";
                                                    entityLab3.CostPrice = 0;
                                                    entityLab3.Price = 0;
                                                    entityLab3.CitoAmount = 0;
                                                    entityLab3.IsCitoInPercent = false;
                                                    entityLab3.BasicCitoAmount = 0;
                                                    entityLab3.RoundingAmount = 0;
                                                    entityLab3.SRDiscountReason = string.Empty;
                                                    entityLab3.IsAssetUtilization = false;
                                                    entityLab3.AssetID = string.Empty;
                                                    entityLab3.IsBillProceed = false;
                                                    entityLab3.IsOrderRealization = false;
                                                    entityLab3.IsPaymentConfirmed = false;
                                                    entityLab3.IsPackage = false;
                                                    entityLab3.IsVoid = false;
                                                    entityLab3.Notes = string.Empty;
                                                    entityLab3.IsItemTypeService = true;
                                                    entityLab3.SRCenterID = string.Empty;
                                                    entityLab3.IsApprove = false;
                                                    entityLab3.IsItemRoom = false;
                                                    entityLab3.SRCitoPercentage = string.Empty;
                                                    if (isNewRecord)
                                                    {

                                                        entityLab3.CreatedByUserID = AppSession.UserLogin.UserID;
                                                        entityLab3.CreatedDateTime = (new DateTime()).NowAtSqlServer();
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    var consColl = new ItemConsumptionCollection();
                    consColl.Query.Where(consColl.Query.ItemID == itemID);
                    consColl.LoadAll();

                    foreach (var consEntity in consColl)
                    {
                        consItem = TransChargesItemConsumptions.AddNew();
                        consItem.TransactionNo = entity.TransactionNo;
                        consItem.SequenceNo = sequenceNo;
                        consItem.DetailItemID = consEntity.DetailItemID;

                        var i = new Item();
                        i.LoadByPrimaryKey(consItem.DetailItemID);
                        consItem.ItemName = i.ItemName;

                        consItem.Qty = chargeQuantity * consEntity.Qty;
                        consItem.QtyRealization = consItem.Qty;
                        consItem.MaxValue = AppSession.Parameter.IsValidateMaxQtyItemConsumptions ? consItem.Qty : 1000;
                        consItem.SRItemUnit = consEntity.SRItemUnit;

                        var tariff = (Helper.Tariff.GetItemTariff(TransactionDate, grr.SRTariffType, entity.ChargeClassID, entity.ChargeClassID, consItem.DetailItemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
                                     Helper.Tariff.GetItemTariff(TransactionDate, grr.SRTariffType, AppSession.Parameter.DefaultTariffClass, entity.ChargeClassID, consItem.DetailItemID, reg.GuarantorID, false, reg.SRRegistrationType)) ??
                                    (Helper.Tariff.GetItemTariff(TransactionDate, AppSession.Parameter.DefaultTariffType, entity.ChargeClassID, entity.ChargeClassID, consItem.DetailItemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
                                     Helper.Tariff.GetItemTariff(TransactionDate, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, entity.ChargeClassID, consItem.DetailItemID, reg.GuarantorID, false, reg.SRRegistrationType));

                        if (tariff != null)
                            consItem.Price = tariff.Price ?? 0;
                        else
                            consItem.Price = 0;

                        switch (i.SRItemType)
                        {
                            case ItemType.Medical:
                                var im = new ItemProductMedic();
                                im.LoadByPrimaryKey(i.ItemID);
                                consItem.AveragePrice = im.CostPrice;
                                consItem.FifoPrice = im.PriceInBaseUnit;
                                break;
                            case ItemType.NonMedical:
                                var inm = new ItemProductNonMedic();
                                inm.LoadByPrimaryKey(i.ItemID);
                                consItem.AveragePrice = inm.CostPrice;
                                consItem.FifoPrice = inm.PriceInBaseUnit;
                                break;
                            case ItemType.Kitchen:
                                var ik = new ItemKitchen();
                                ik.LoadByPrimaryKey(i.ItemID);
                                consItem.AveragePrice = ik.CostPrice;
                                consItem.FifoPrice = ik.PriceInBaseUnit;
                                break;
                            default:
                                consItem.AveragePrice = consItem.Price;
                                consItem.FifoPrice = consItem.Price;
                                break;
                        }

                        consItem.IsPackage = false;
                    }
                }

                //item lab detail khusus rs pake lis offile avicenna
                var item = new Item();
                item.LoadByPrimaryKey(entity.ItemID);
                if (item.SRItemType == ItemType.Laboratory)// && AppSession.Parameter.IsUsingHisInterop == "No")
                {
                    var labs = new ItemLaboratoryProfileCollection();
                    labs.Query.Where(labs.Query.ParentItemID == entity.ItemID);
                    labs.Query.OrderBy(labs.Query.DisplaySequence.Ascending);
                    if (labs.Query.Load())
                    {
                        foreach (var lab in labs)
                        {
                            var entityLab = TransChargesItems.AddNew();
                            entityLab.TransactionNo = TransactionNo;

                            sequenceNo = (TransChargesItems.Where(c => c.ParentNo == entity.SequenceNo).OrderByDescending(c => c.SequenceNo).Select(c => c.SequenceNo.Substring(c.SequenceNo.Length - 3, 3))).Take(1).SingleOrDefault();
                            entityLab.SequenceNo = entity.SequenceNo + string.Format("{0:000}", string.IsNullOrEmpty(sequenceNo) ? 1 : int.Parse(sequenceNo) + 1);

                            entityLab.ParentNo = entity.SequenceNo;
                            entityLab.ReferenceNo = string.Empty;
                            entityLab.ReferenceSequenceNo = string.Empty;
                            entityLab.ItemID = lab.DetailItemID;
                            entityLab.ItemName = string.Empty;
                            entityLab.ChargeClassID = ClassID;
                            entityLab.ParamedicID = paramedicID;
                            entityLab.ParamedicName = paramedicName;
                            entityLab.IsAdminCalculation = false;
                            entityLab.IsVariable = false;
                            entityLab.IsCito = false;
                            entityLab.ChargeQuantity = 0;
                            entityLab.StockQuantity = 0;
                            entityLab.SRItemUnit = srItemUnit;
                            entityLab.CostPrice = 0;
                            entityLab.Price = 0;
                            entityLab.CitoAmount = 0;
                            entityLab.IsCitoInPercent = false;
                            entityLab.BasicCitoAmount = 0;
                            entityLab.RoundingAmount = 0;
                            entityLab.SRDiscountReason = string.Empty;
                            entityLab.IsAssetUtilization = false;
                            entityLab.AssetID = string.Empty;
                            entityLab.IsBillProceed = false;
                            entityLab.IsOrderRealization = false;
                            entityLab.IsPaymentConfirmed = false;
                            entityLab.IsPackage = false;
                            entityLab.IsVoid = isVoid;
                            entityLab.Notes = string.Empty;
                            entityLab.IsItemTypeService = true;
                            entityLab.SRCenterID = string.Empty;
                            entityLab.IsApprove = false;
                            entityLab.IsItemRoom = false;
                            entityLab.SRCitoPercentage = string.Empty;

                            if (isNewRecord)
                            {
                                entityLab.CreatedByUserID = AppSession.UserLogin.UserID;
                                entityLab.CreatedDateTime = (new DateTime()).NowAtSqlServer();
                            }

                            //cek anak level 2
                            var labs2 = new ItemLaboratoryProfileCollection();
                            labs2.Query.Where(labs2.Query.ParentItemID == entityLab.ItemID);
                            labs2.Query.OrderBy(labs2.Query.DisplaySequence.Ascending);
                            if (labs2.Query.Load())
                            {
                                foreach (var lab2 in labs2)
                                {
                                    var entityLab2 = TransChargesItems.AddNew();
                                    entityLab2.TransactionNo = TransactionNo;

                                    sequenceNo = (TransChargesItems.Where(c => c.ParentNo == entityLab.SequenceNo).OrderByDescending(c => c.SequenceNo).Select(c => c.SequenceNo.Substring(c.SequenceNo.Length - 3, 3))).Take(1).SingleOrDefault();
                                    entityLab2.SequenceNo = entityLab.SequenceNo + string.Format("{0:000}", string.IsNullOrEmpty(sequenceNo) ? 1 : int.Parse(sequenceNo) + 1);

                                    entityLab2.ParentNo = entityLab.SequenceNo;
                                    entityLab2.ReferenceNo = string.Empty;
                                    entityLab2.ReferenceSequenceNo = string.Empty;
                                    entityLab2.ItemID = lab2.DetailItemID;
                                    entityLab2.ItemName = string.Empty;
                                    entityLab2.ChargeClassID = ClassID;
                                    entityLab2.ParamedicID = paramedicID;
                                    entityLab2.ParamedicName = paramedicName;
                                    entityLab2.IsAdminCalculation = false;
                                    entityLab2.IsVariable = false;
                                    entityLab2.IsCito = false;
                                    entityLab2.ChargeQuantity = 0;
                                    entityLab2.StockQuantity = 0;
                                    entityLab2.SRItemUnit = srItemUnit;
                                    entityLab2.CostPrice = 0;
                                    entityLab2.Price = 0;
                                    entityLab2.CitoAmount = 0;
                                    entityLab2.IsCitoInPercent = false;
                                    entityLab2.BasicCitoAmount = 0;
                                    entityLab2.RoundingAmount = 0;
                                    entityLab2.SRDiscountReason = string.Empty;
                                    entityLab2.IsAssetUtilization = false;
                                    entityLab2.AssetID = string.Empty;
                                    entityLab2.IsBillProceed = false;
                                    entityLab2.IsOrderRealization = false;
                                    entityLab2.IsPaymentConfirmed = false;
                                    entityLab2.IsPackage = false;
                                    entityLab2.IsVoid = isVoid;
                                    entityLab2.Notes = string.Empty;
                                    entityLab2.IsItemTypeService = true;
                                    entityLab2.SRCenterID = string.Empty;
                                    entityLab2.IsApprove = false;
                                    entityLab2.IsItemRoom = false;
                                    entityLab2.SRCitoPercentage = string.Empty;

                                    if (isNewRecord)
                                    {
                                        entityLab2.CreatedByUserID = AppSession.UserLogin.UserID;
                                        entityLab2.CreatedDateTime = (new DateTime()).NowAtSqlServer();
                                    }

                                    //cek anak level 3
                                    var labs3 = new ItemLaboratoryProfileCollection();
                                    labs3.Query.Where(labs3.Query.ParentItemID == entityLab2.ItemID);
                                    labs3.Query.OrderBy(labs3.Query.DisplaySequence.Ascending);
                                    if (labs3.Query.Load())
                                    {
                                        foreach (var lab3 in labs3)
                                        {
                                            var entityLab3 = TransChargesItems.AddNew();
                                            entityLab3.TransactionNo = string.Empty;

                                            sequenceNo = (TransChargesItems.Where(c => c.ParentNo == entityLab2.SequenceNo).OrderByDescending(c => c.SequenceNo).Select(c => c.SequenceNo.Substring(c.SequenceNo.Length - 3, 3))).Take(1).SingleOrDefault();
                                            entityLab3.SequenceNo = entityLab2.SequenceNo + string.Format("{0:000}", string.IsNullOrEmpty(sequenceNo) ? 1 : int.Parse(sequenceNo) + 1);

                                            entityLab3.ParentNo = entityLab2.SequenceNo;
                                            entityLab3.ReferenceNo = string.Empty;
                                            entityLab3.ReferenceSequenceNo = string.Empty;
                                            entityLab3.ItemID = lab3.DetailItemID;
                                            entityLab3.ItemName = string.Empty;
                                            entityLab3.ChargeClassID = string.Empty;
                                            entityLab3.ParamedicID = string.Empty;
                                            entityLab3.ParamedicName = string.Empty;
                                            entityLab3.IsAdminCalculation = false;
                                            entityLab3.IsVariable = false;
                                            entityLab3.IsCito = false;
                                            entityLab3.ChargeQuantity = 0;
                                            entityLab3.StockQuantity = 0;
                                            entityLab3.SRItemUnit = "x";
                                            entityLab3.CostPrice = 0;
                                            entityLab3.Price = 0;
                                            entityLab3.CitoAmount = 0;
                                            entityLab3.IsCitoInPercent = false;
                                            entityLab3.BasicCitoAmount = 0;
                                            entityLab3.RoundingAmount = 0;
                                            entityLab3.SRDiscountReason = string.Empty;
                                            entityLab3.IsAssetUtilization = false;
                                            entityLab3.AssetID = string.Empty;
                                            entityLab3.IsBillProceed = false;
                                            entityLab3.IsOrderRealization = false;
                                            entityLab3.IsPaymentConfirmed = false;
                                            entityLab3.IsPackage = false;
                                            entityLab3.IsVoid = false;
                                            entityLab3.Notes = string.Empty;
                                            entityLab3.IsItemTypeService = true;
                                            entityLab3.SRCenterID = string.Empty;
                                            entityLab3.IsApprove = false;
                                            entityLab3.IsItemRoom = false;
                                            entityLab3.SRCitoPercentage = string.Empty;
                                            if (isNewRecord)
                                            {

                                                entityLab3.CreatedByUserID = AppSession.UserLogin.UserID;
                                                entityLab3.CreatedDateTime = (new DateTime()).NowAtSqlServer();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                var group = new ItemGroup();
                group.LoadByPrimaryKey(item.ItemGroupID);
                entity.ItemGroupName = group.ItemGroupName;
            }
            else
            {
                if (TransChargesItemConsumptions.Count > 0)
                {
                    foreach (var x in TransChargesItemConsumptions)
                    {
                        var item = TransChargesItems.FindByPrimaryKey(x.TransactionNo, x.SequenceNo);
                        var itemCons = new ItemConsumption();
                        if (itemCons.LoadByPrimaryKey(item.ItemID, x.DetailItemID))
                        {
                            if (x.Qty != item.ChargeQuantity * itemCons.Qty)
                            {
                                x.Qty = item.ChargeQuantity * itemCons.Qty;
                                x.QtyRealization = x.Qty;
                                x.MaxValue = AppSession.Parameter.IsValidateMaxQtyItemConsumptions ? x.Qty : 1000;
                            }
                        }
                    }
                }
            }

            entity.SetPrevOrder(RegistrationNo, AppSession.Parameter.IntervalOrderWarning);
        }

        #endregion

        private static string[] GuarantorBPJS
        {
            get
            {
                var grr = new GuarantorBridgingCollection();
                grr.Query.es.Distinct = true;
                grr.Query.Where(grr.Query.SRBridgingType.In(AppEnum.BridgingType.BPJS.ToString(), AppEnum.BridgingType.BPJS_TNI_POLRI_PNS.ToString()));
                if (grr.Query.Load()) return grr.Select(g => g.GuarantorID).ToArray();
                else return new string[] { string.Empty };
            }
        }

        private TransChargesItemCompCollection TransChargesItemComps
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = Session["collTransChargesItemComp" + Request.UserHostName + hdnPageId.Value];
                    if (obj != null)
                        return ((TransChargesItemCompCollection)(obj));
                }

                var coll = new TransChargesItemCompCollection();

                var query = new TransChargesItemCompQuery("a");
                var comp = new TariffComponentQuery("b");
                var tci = new TransChargesItemQuery("d");

                tci.Select(tci.TransactionNo, tci.SequenceNo);
                tci.Where(tci.TransactionNo == query.TransactionNo, tci.SequenceNo == query.SequenceNo,
                          tci.IsExtraItem == true,
                          tci.IsSelectedExtraItem == false);

                query.Select(
                    query,
                    comp.TariffComponentName.As("refToTariffComponent_TariffComponentName"),
                    comp.IsTariffParamedic
                    );
                query.InnerJoin(comp).On(query.TariffComponentID == comp.TariffComponentID);

                //query.Where(query.TransactionNo == txtTransactionNo.Text);

                if (Request.QueryString["type"] == "mcu")
                    query.Where(query.TransactionNo == txtTransactionNo.Text, query.NotExists(tci));
                else
                    query.Where(query.TransactionNo == txtTransactionNo.Text);

                query.OrderBy(
                        query.SequenceNo.Ascending,
                        query.TariffComponentID.Ascending
                    );

                coll.Load(query);

                Session["collTransChargesItemComp" + Request.UserHostName + hdnPageId.Value] = coll;
                return coll;
            }
            set { Session["collTransChargesItemComp" + Request.UserHostName + hdnPageId.Value] = value; }
        }

        private TransChargesItemConsumptionCollection TransChargesItemConsumptions
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = Session["collTransChargesItemConsumption" + Request.UserHostName + hdnPageId.Value];
                    if (obj != null)
                        return ((TransChargesItemConsumptionCollection)(obj));
                }

                var coll = new TransChargesItemConsumptionCollection();

                var query = new TransChargesItemConsumptionQuery("a");
                var item = new ItemQuery("b");
                var tci = new TransChargesItemQuery("d");

                tci.Select(tci.TransactionNo, tci.SequenceNo);
                tci.Where(tci.TransactionNo == query.TransactionNo, tci.SequenceNo == query.SequenceNo,
                          tci.IsExtraItem == true,
                          tci.IsSelectedExtraItem == false);

                query.Select(
                    query,
                    item.ItemName.As("refToItem_ItemName")
                    );

                if (AppSession.Parameter.IsValidateMaxQtyItemConsumptions)
                    query.Select(@"<CASE WHEN a.Qty = 0 THEN 10000 ELSE a.Qty END AS 'refTo_MaxQty'>");
                else
                    query.Select(@"<CAST(10000 AS NUMERIC(10,2)) AS 'refTo_MaxQty'>");

                query.InnerJoin(item).On(query.DetailItemID == item.ItemID);

                if (Request.QueryString["type"] == "mcu")
                    query.Where(query.TransactionNo == txtTransactionNo.Text, query.NotExists(tci));
                else
                    query.Where(query.TransactionNo == txtTransactionNo.Text);

                //query.Where(query.TransactionNo == txtTransactionNo.Text);

                coll.Load(query);

                Session["collTransChargesItemConsumption" + Request.UserHostName + hdnPageId.Value] = coll;
                return coll;
            }
            set { Session["collTransChargesItemConsumption" + Request.UserHostName + hdnPageId.Value] = value; }
        }

        private CostCalculationCollection CostCalculations
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = ViewState["collCostCalculation" + Request.UserHostName + hdnPageId.Value];
                    if (obj != null)
                        return ((CostCalculationCollection)(obj));
                }

                var coll = new CostCalculationCollection();

                coll.Query.Where
                    (
                        coll.Query.TransactionNo == txtTransactionNo.Text,
                        coll.Query.RegistrationNo == txtRegistrationNo.Text
                    );
                coll.LoadAll();

                ViewState["collCostCalculation" + Request.UserHostName + hdnPageId.Value] = coll;
                return coll;
            }
            set { ViewState["collCostCalculation" + Request.UserHostName + hdnPageId.Value] = value; }
        }

        protected void grdTransChargesItem_ItemCreated(object sender, GridItemEventArgs e)
        {
            grdTransChargesItem_OnItemCreated(TransChargesItems, sender, e);
        }

        public static void grdTransChargesItem_OnItemCreated(TransChargesItemCollection TransChargesItems,
            object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                if (TransChargesItems.Count < e.Item.DataSetIndex)
                {
                    var item = TransChargesItems[e.Item.DataSetIndex];
                    if (item != null)
                    {
                        if (item.IsVoid ?? false)
                        {
                            for (var i = 0; i < e.Item.Cells.Count; i++)
                            {
                                if (i > 0 && i < e.Item.Cells.Count)
                                    e.Item.Cells[i].Font.Strikeout = true;

                            }
                        }
                    }
                }
            }
        }

        protected void grdTransChargesItem_DataBound(object sender, EventArgs e)
        {
            try
            {
                var lbtnO2Calculation = grdTransChargesItem.MasterTableView.GetItems(GridItemType.CommandItem)[0].FindControl("lbO2Calculation");
                lbtnO2Calculation.Visible = grdTransChargesItem.MasterTableView.IsItemInserted && Request.QueryString["type"] == "tr";

                var lbtnfiO2Calculation = grdTransChargesItem.MasterTableView.GetItems(GridItemType.CommandItem)[0].FindControl("lbfiO2Calculation");
                lbtnfiO2Calculation.Visible = grdTransChargesItem.MasterTableView.IsItemInserted && Request.QueryString["type"] == "tr";

                var lbtnPickFromTemplate = grdTransChargesItem.MasterTableView.GetItems(GridItemType.CommandItem)[0].FindControl("lbPickFromTemplate");
                lbtnPickFromTemplate.Visible = !grdTransChargesItem.MasterTableView.IsItemInserted && !string.IsNullOrEmpty(AppSession.UserLogin.ParamedicID) && Request.QueryString["type"] == "jo" && Request.QueryString["emr"] == "1";

                var lbtnSaveAsNewTemplate = grdTransChargesItem.MasterTableView.GetItems(GridItemType.CommandItem)[0].FindControl("lbSaveAsNewTemplate");
                lbtnSaveAsNewTemplate.Visible = !grdTransChargesItem.MasterTableView.IsItemInserted && !string.IsNullOrEmpty(AppSession.UserLogin.ParamedicID) && Request.QueryString["type"] == "jo" && Request.QueryString["emr"] == "1";
            }
            catch { }
        }


        protected void grdTransChargesItem_ItemCommand(object source, GridCommandEventArgs e)
        {
            grdTransChargesItem_OnItemCommand(TransChargesItems, source, e);
        }

        public static void grdTransChargesItem_OnItemCommand(TransChargesItemCollection tci, object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "Edit" || e.CommandName == "Delete")
            {
                var item = tci[e.Item.DataSetIndex];
                if (item != null)
                {
                    if (item.IsBillProceed.Value)
                        e.Canceled = true;
                }
            }
        }


        private static TransChargesItemComp FindTransChargesItemComp(TransChargesItemCompCollection TransChargesItemComps,
            String sequenceNo, String tariffComponentID)
        {
            var coll = TransChargesItemComps;
            return coll.FirstOrDefault(rec => rec.SequenceNo.Equals(sequenceNo) && rec.TariffComponentID.Equals(tariffComponentID));
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);

            if (string.IsNullOrEmpty(eventArgument))
                return;

            if (eventArgument.Contains("addwithbarcode"))
            {
                var barcode = eventArgument.Split('|')[1];
                if (AddItemDetailWithBarcode(barcode))
                {
                    if (tblTemporaryBill.Visible)
                    {
                        txtTemporaryBillTotal.Value = Convert.ToDouble(GetTotalTemporaryBill());
                    }
                    grdTransChargesItem.Rebind();
                    if (Request.QueryString["type"] == "jo" || Request.QueryString["type"] == "ds")
                        cboToServiceUnitID.Enabled = (TransChargesItems.Count == 0);
                    cboLocationID.Enabled = (TransChargesItems.Count == 0);
                    cboBedID.Enabled = Request.QueryString["disch"] != "0" && (TransChargesItems.Count == 0);
                }
                var txtBarcode = (RadTextBox)source;
                txtBarcode.Text = string.Empty;
                txtBarcode.Focus();
            }
            else
            {
                if (!(source is RadGrid))
                    return;

                var grd = (RadGrid)source;
                if (grd.ID == "grdTransChargesItem")
                {
                    if (pnlResponUnit.Visible)
                        cboResponUnit.Enabled = (TransChargesItems.Count == 0);
                    if (Request.QueryString["type"] == "jo" || Request.QueryString["type"] == "ds")
                        cboToServiceUnitID.Enabled = (TransChargesItems.Count == 0);
                    cboLocationID.Enabled = (TransChargesItems.Count == 0);

                    if (tblTemporaryBill.Visible)
                    {
                        txtTemporaryBillTotal.Value = Convert.ToDouble(GetTotalTemporaryBill());
                    }

                    grd.Rebind();
                }
            }

        }

        #region Barcode entry
        protected virtual void txtBarcodeEntry_OnTextChanged(object sender, System.EventArgs e)
        {
            var txtBarcodeEntry = (RadTextBox)sender;
            if (txtBarcodeEntry.Text == string.Empty)
                return;

            if (AddItemDetailWithBarcode(txtBarcodeEntry.Text))
            {
                grdTransChargesItem.Rebind();
                if (Request.QueryString["type"] == "jo" || Request.QueryString["type"] == "ds")
                    cboToServiceUnitID.Enabled = (TransChargesItems.Count == 0);
                cboLocationID.Enabled = (TransChargesItems.Count == 0);
                cboBedID.Enabled = Request.QueryString["disch"] != "0" && (TransChargesItems.Count == 0);
            }

            txtBarcodeEntry.Text = string.Empty;
            txtBarcodeEntry.Focus();
        }

        private bool AddItemDetailWithBarcode(string barcode)
        {
            //Check hanya untuk type item 11 Medical & 21 Non Medical
            var item = new Item();
            if (!item.LoadByBarcode(barcode))
            {
                // Barcode bisa sbg ItemID
                if (!item.LoadByPrimaryKey(barcode))
                    return false;
            }

            var itemID = item.ItemID;
            bool isItemInventory = false;
            if (item.SRItemType == ItemType.Medical || item.SRItemType == ItemType.NonMedical ||
                item.SRItemType == ItemType.Kitchen)
            {
                if (item.SRItemType == ItemType.Medical)
                {
                    var ipm = new ItemProductMedic();
                    ipm.LoadByPrimaryKey(itemID);
                    isItemInventory = ipm.IsInventoryItem ?? false;
                }
                else if (item.SRItemType == ItemType.NonMedical)
                {
                    var ipnm = new ItemProductNonMedic();
                    ipnm.LoadByPrimaryKey(itemID);
                    isItemInventory = ipnm.IsInventoryItem ?? false;
                }
                else if (item.SRItemType == ItemType.Kitchen)
                {
                    var ik = new ItemKitchen();
                    ik.LoadByPrimaryKey(itemID);
                    isItemInventory = ik.IsInventoryItem ?? false;
                }

                var bal = new ItemBalance();
                if (!bal.LoadByPrimaryKey(cboLocationID.SelectedValue, itemID))
                    return false;

                if (bal.Balance < 0 && isItemInventory)
                    return false;
            }

            var unit = new ServiceUnit();
            if (Request.QueryString["type"] == "tr")
            {
                if (!unit.LoadByPrimaryKey(pnlResponUnit.Visible ? cboResponUnit.SelectedValue : cboFromServiceUnitID.SelectedValue))
                    return false;
            }
            else
            {
                if (!unit.LoadByPrimaryKey(cboToServiceUnitID.SelectedValue))
                    return false;
            }

            string sequenceNo,
                   itemName,
                   paramedicID,
                   paramedicName,
                   srItemUnit = string.Empty,
                   srDiscountReason,
                   assetID,
                   notes,
                   filmNo,
                   SRCitoPercentage = string.Empty,
                   itemConditionRuleId = string.Empty,
                   itemConditionRuleName = string.Empty;

            bool? isAdminCalculation, isVariable, isCito, isAssetUtilization, isPackage, isVoid, isItemRoom, isItemTypeService;
            decimal? chargeQuantity, stockQuantity, costPrice = 0, price, discountAmount;
            DateTime? tariffDate;
            bool isNewRecord;

            //Check bila sudah ada maka tambah di qty nya saja
            var entity = TransChargesItems.FirstOrDefault(rec => rec.ItemID.Equals(itemID));

            if (entity != null)
            {
                sequenceNo = entity.SequenceNo;
                itemName = entity.ItemName;
                paramedicID = entity.ParamedicID;
                paramedicName = entity.ParamedicName;
                isAdminCalculation = entity.IsAdminCalculation;
                isVariable = entity.IsVariable;
                isCito = entity.IsCito;
                chargeQuantity = entity.ChargeQuantity + 1;
                stockQuantity = entity.StockQuantity + 1;
                srItemUnit = entity.SRItemUnit;
                costPrice = entity.CostPrice;
                price = entity.Price;
                discountAmount = entity.DiscountAmount;
                srDiscountReason = entity.SRDiscountReason;
                isAssetUtilization = entity.IsAssetUtilization;
                assetID = entity.AssetID;
                isPackage = entity.IsPackage;
                isVoid = entity.IsVoid;
                notes = entity.Notes;
                isNewRecord = false;
                isItemRoom = entity.IsItemRoom;
                filmNo = entity.FilmNo;
                SRCitoPercentage = entity.SRCitoPercentage;
                itemConditionRuleId = entity.ItemConditionRuleID;
                itemConditionRuleName = entity.ItemConditionRuleName;
                isItemTypeService = entity.IsItemTypeService;
                tariffDate = entity.TariffDate;
            }
            else
            {
                sequenceNo = TransChargesItems.Count > 0 ? string.Format("{0:000}", int.Parse(TransChargesItems[TransChargesItems.Count - 1].SequenceNo) + 1) : "001";

                itemName = item.ItemName;
                paramedicID = string.Empty;
                paramedicName = string.Empty;

                var reg = new Registration();
                reg.LoadByPrimaryKey(txtRegistrationNo.Text);

                var grr = new Guarantor();
                grr.LoadByPrimaryKey(reg.GuarantorID);

                if (item.SRItemType == ItemType.Medical)
                {
                    var itemMedic = new ItemProductMedic();
                    itemMedic.LoadByPrimaryKey(itemID);
                    srItemUnit = itemMedic.SRItemUnit;
                    costPrice = itemMedic.CostPrice;
                    stockQuantity = 1;
                    tariffDate = txtTransactionDate.SelectedDate;
                }
                else if (item.SRItemType == ItemType.NonMedical)
                {
                    var itemNonMedic = new ItemProductNonMedic();
                    itemNonMedic.LoadByPrimaryKey(itemID);
                    srItemUnit = itemNonMedic.SRItemUnit;
                    costPrice = itemNonMedic.CostPrice;
                    stockQuantity = 1;
                    tariffDate = txtTransactionDate.SelectedDate;
                }
                else if (item.SRItemType == ItemType.Kitchen)
                {
                    var itemKitchen = new ItemKitchen();
                    itemKitchen.LoadByPrimaryKey(itemID);
                    srItemUnit = itemKitchen.SRItemUnit;
                    costPrice = itemKitchen.CostPrice;
                    stockQuantity = 1;
                    tariffDate = txtTransactionDate.SelectedDate;
                }
                else if (item.SRItemType == ItemType.Service)
                {
                    var service = new ItemService();
                    service.LoadByPrimaryKey(itemID);
                    srItemUnit = service.SRItemUnit;
                    costPrice = 0;
                    stockQuantity = 0;
                    tariffDate = grr.TariffCalculationMethod == 1
                        ? reg.RegistrationDate.Value.Date
                        : txtTransactionDate.SelectedDate.Value;
                }
                else
                {
                    srItemUnit = "X";
                    costPrice = 0;
                    stockQuantity = 0;
                    tariffDate = grr.TariffCalculationMethod == 1
                        ? reg.RegistrationDate.Value.Date
                        : txtTransactionDate.SelectedDate.Value;
                }

                chargeQuantity = 1;

                ItemTariff tariff = (Helper.Tariff.GetItemTariff(tariffDate ?? (new DateTime()).NowAtSqlServer().Date, grr.SRTariffType, reg.ChargeClassID, reg.ChargeClassID, itemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
                                     Helper.Tariff.GetItemTariff(tariffDate ?? (new DateTime()).NowAtSqlServer().Date, grr.SRTariffType, AppSession.Parameter.DefaultTariffClass, reg.ChargeClassID, itemID, reg.GuarantorID, false, reg.SRRegistrationType)) ??
                                    (Helper.Tariff.GetItemTariff(tariffDate ?? (new DateTime()).NowAtSqlServer().Date, AppSession.Parameter.DefaultTariffType, reg.ChargeClassID, reg.ChargeClassID, itemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
                                    Helper.Tariff.GetItemTariff(tariffDate ?? (new DateTime()).NowAtSqlServer().Date, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, reg.ChargeClassID, itemID, reg.GuarantorID, false, reg.SRRegistrationType));

                tariff.UpdateCitoFromStdRef(SRCitoPercentage);

                price = tariff.Price ?? 0;
                isVariable = tariff.IsAllowVariable ?? false;
                isCito = tariff.IsAllowCito ?? false;
                isAdminCalculation = tariff.IsAdminCalculation ?? false;

                discountAmount = 0;
                srDiscountReason = string.Empty;
                isAssetUtilization = false;
                assetID = string.Empty;
                isPackage = false;
                isVoid = false;
                notes = string.Empty;
                isNewRecord = true;
                filmNo = string.Empty;

                var itemRooms = new AppStandardReferenceItemCollection();
                itemRooms.Query.Where(itemRooms.Query.StandardReferenceID == "ItemTariffRoom",
                                      itemRooms.Query.ItemID == itemID, itemRooms.Query.IsActive == true);
                itemRooms.LoadAll();
                isItemRoom = itemRooms.Count > 0;
                isItemTypeService = false;

                entity = TransChargesItems.AddNew();
            }
            SetEntityDetail(entity, sequenceNo, itemID, itemName, paramedicID, paramedicName, isAdminCalculation, isVariable, isCito, chargeQuantity,
                  stockQuantity, srItemUnit, costPrice, price, discountAmount, srDiscountReason, isAssetUtilization, assetID, isPackage, isVoid,
                  notes, string.Empty, null, isNewRecord, isItemRoom, filmNo,
                  txtTransactionNo.Text, txtRegistrationNo.Text, txtClassID.Text, cboToServiceUnitID.SelectedValue,
                    TransChargesItemComps, tariffDate ?? (new DateTime()).NowAtSqlServer().Date,
                    TransChargesItems, TransChargesItemConsumptions, string.Empty, SRCitoPercentage, itemConditionRuleId, itemConditionRuleName, isItemTypeService ?? false, Request.QueryString["type"], cboToServiceUnitID.SelectedValue);
            return true;
        }
        #endregion

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            ToolBarMenuSearch.Enabled = false;
            //ToolBarMenuEdit.Enabled = !(bool)ViewState["IsApproved"] && !(bool)ViewState["IsVoid"];
            //ToolBarMenuDelete.Enabled = !(bool)ViewState["IsApproved"] && !(bool)ViewState["IsVoid"];

            // khusus transaksi paket bisa unapproval, karena paket tidak ada koreksiannya
            if (Request.QueryString["type"] != "jo")
                ToolBarMenuUnApproval.Enabled = ToolBarMenuUnApproval.Enabled && (bool)ViewState["IsPackage"];

            // Override Title
            switch (OrderType)
            {
                case "LAB":
                    {
                        this.Title = "Exam Order Laboratory";
                        break;
                    }
                case "RAD":
                    {
                        this.Title = "Exam Order Radiology and Imaging";
                        break;
                    }
                case "PAT":
                    {
                        this.Title = "Exam Order Pathology Anatomy";
                        break;
                    }
                case "OTH":
                    {
                        this.Title = "Exam Order Other";
                        break;
                    }
            }
        }

        //public static ItemTariffQuery GetItemTariffQuery(string tariffType, string classID, string itemID)
        //{
        //    var query = new ItemTariffQuery();
        //    query.es.Top = 1;
        //    query.Where
        //        (
        //            query.SRTariffType == tariffType,
        //            query.ClassID == classID,
        //            query.ItemID == itemID,
        //            query.StartingDate <= (new DateTime()).NowAtSqlServer().Date
        //        );
        //    query.OrderBy(query.StartingDate.Descending);

        //    return query;
        //}

        protected void cboLocationID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            // DO NOT REMOVE, UNLESS YOU KNOW WHAT U ARE DOING!!
        }

        protected void cboToServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            grdTransChargesItem.DataSource = TransChargesItems;
            grdTransChargesItem.MasterTableView.IsItemInserted = false;
            grdTransChargesItem.MasterTableView.ClearEditItems();
            grdTransChargesItem.DataBind();

            ApplyToServiceUnitID(e.Value);
        }

        private void ApplyToServiceUnitID(string serviceUnitID)
        {
            rfvNotes.Enabled = false;

            if (!string.IsNullOrEmpty(serviceUnitID))
            {
                ComboBox.PopulateWithServiceUnitForLocation(cboLocationID, serviceUnitID);
                cboLocationID.SelectedIndex = 1;

                // cek validasi notes
                if (Request.QueryString["type"] == "jo" || Request.QueryString["type"] == "ds")
                {
                    if (AppSession.Parameter.IsValidateNoteOnAllJobOrder)
                        rfvNotes.Enabled = true;
                    else if ((AppSession.Parameter.ServiceUnitLaboratoryID == serviceUnitID || AppSession.Parameter.ServiceUnitLaboratoryIdArray.Contains(serviceUnitID))
                        && AppSession.Parameter.IsValidateNoteOnJobOrderLab)
                    {
                        rfvNotes.Enabled = true;
                    }
                }
            }
            else
            {
                cboLocationID.Items.Clear();
                cboLocationID.Text = string.Empty;
            }

            if (TrDiagNo.Visible && serviceUnitID == AppSession.Parameter.ServiceUnitRadiologyID)
                txtDiagnosticNo.ReadOnly = false;
            else
                txtDiagnosticNo.ReadOnly = true;

            //if (Request.QueryString["type"] == "ds" &&
            //    e.Value == AppSession.Parameter.ServiceUnitLaboratoryID &&
            //    AppSession.Parameter.IsUsingHisInterop == "Yes" &&
            //    AppSession.Parameter.HisInteropConfigName == AppConstant.HIS_INTEROP.PRODIA_LIS_INTEROP_CONNECTION_NAME)
            //{
            //    cboSRProdiaContractID.Enabled = true;
            //    rfvSRProdiaContractID.Visible = true;
            //}
            //else
            //{
            //    cboSRProdiaContractID.Enabled = false;
            //    rfvSRProdiaContractID.Visible = false;
            //}
            //cboSRProdiaContractID.SelectedValue = string.Empty;
            //cboSRProdiaContractID.Text = string.Empty;

            if (Request.QueryString["type"] == "ds")
            {
                if (AppSession.Parameter.IsUsingHisInterop && AppSession.Parameter.HisInteropConfigName == AppConstant.HIS_INTEROP.LINK_LIS_INTEROP_CONNECTION_NAME)
                {
                    if (serviceUnitID == AppSession.Parameter.ServiceUnitLaboratoryID)
                    {
                        if (pnlLinkLis.Visible)
                        {
                            var pb = new ParamedicBridgingQuery("a");
                            var p = new ParamedicQuery("b");

                            pb.Select(pb.BridgingID, pb.BridgingName, p.SRParamedicType);
                            pb.InnerJoin(p).On(pb.ParamedicID == p.ParamedicID &&
                                pb.SRBridgingType == AppEnum.BridgingType.LINK_LIS.ToString() &&
                                p.SRParamedicType.In(AppEnum.ParamedicType.ClinicalPathologist.ToString(), AppEnum.ParamedicType.LaboratoryAnalyst.ToString()) &&
                                p.IsActive == true);
                            var dtb = pb.LoadDataTable();

                            cboPhysicianIDPathology.Items.Clear();
                            cboPhysicianIDPathology.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                            foreach (DataRow item in dtb.AsEnumerable().Where(d => d.Field<string>("SRParamedicType") == AppEnum.ParamedicType.ClinicalPathologist.ToString()))
                            {
                                cboPhysicianIDPathology.Items.Add(new RadComboBoxItem(item["BridgingName"].ToString(), item["BridgingID"].ToString()));
                            }

                            cboAnalystID.Items.Clear();
                            cboAnalystID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                            foreach (DataRow item in dtb.AsEnumerable().Where(d => d.Field<string>("SRParamedicType") == AppEnum.ParamedicType.LaboratoryAnalyst.ToString()))
                            {
                                cboAnalystID.Items.Add(new RadComboBoxItem(item["BridgingName"].ToString(), item["BridgingID"].ToString()));
                            }
                        }
                    }
                    else
                    {
                        cboPhysicianIDPathology.Items.Clear();
                        cboAnalystID.Items.Clear();
                    }
                }
            }
        }

        protected void cboResponUnit_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            bool isVisible = (DataModeCurrent != AppEnum.DataMode.Read);
            grdTransChargesItem.MasterTableView.CommandItemDisplay = (isVisible && !string.IsNullOrEmpty(cboResponUnit.SelectedValue))
                                                                         ? GridCommandItemDisplay.Top
                                                                         : GridCommandItemDisplay.None;
            grdTransChargesItem.Rebind();

            ComboBox.PopulateWithServiceUnitForLocation(cboLocationID, e.Value);
            cboLocationID.SelectedIndex = 1;
        }

        protected void cboParamedicID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ParamedicName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ParamedicID"].ToString();
        }

        protected void cboParamedicID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ParamedicQuery("a");
            var unit = new ServiceUnitParamedicQuery("b");

            query.es.Top = 10;
            query.Select(
                query.ParamedicID,
                query.ParamedicName
                );
            query.InnerJoin(unit).On(query.ParamedicID == unit.ParamedicID);
            query.Where
                (
                    query.ParamedicName.Like(searchTextContain),
                    query.IsActive == true,
                    query.IsAvailable == true,
                    unit.ServiceUnitID == cboFromServiceUnitID.SelectedValue
                );
            query.OrderBy(query.ParamedicName.Ascending);

            cboParamedicID.DataSource = query.LoadDataTable();
            cboParamedicID.DataBind();
        }

        protected void cboBedID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboFromServiceUnitID.Items.Clear();
            var uq = new ServiceUnitQuery();
            uq.Where(uq.ServiceUnitID == e.Value.Split(',')[0].Trim());
            cboFromServiceUnitID.DataSource = uq.LoadDataTable();
            cboFromServiceUnitID.DataBind();

            cboFromServiceUnitID.SelectedValue = e.Value.Split(',')[0].Trim();
            var unit = new ServiceUnit();
            unit.LoadByPrimaryKey(cboFromServiceUnitID.SelectedValue);

            if (Request.QueryString["type"] == "tr" || Request.QueryString["type"] == "mcu" || Request.QueryString["type"] == "npc")
            {
                if (!string.IsNullOrEmpty(cboFromServiceUnitID.SelectedValue))
                {
                    ComboBox.PopulateWithServiceUnitForLocation(cboLocationID, cboFromServiceUnitID.SelectedValue);
                    cboLocationID.SelectedIndex = 1;
                }
            }

            txtRoomID.Text = e.Value.Split(',')[1].Trim();
            var room = new ServiceRoom();
            room.LoadByPrimaryKey(txtRoomID.Text);
            lblRoomName.Text = room.RoomName;
            txtTariffDiscForRoomIn.Value = Convert.ToDouble(room.TariffDiscountForRoomIn);

            if (room.IsOperatingRoom == true)
            {
                var reg = new Registration();
                reg.LoadByPrimaryKey(txtRegistrationNo.Text);
                txtClassID.Text = string.IsNullOrEmpty(reg.ProcedureChargeClassID)
                                          ? e.Value.Split(',')[2].Trim()
                                          : reg.ProcedureChargeClassID;
            }
            else
                txtClassID.Text = e.Value.Split(',')[2].Trim();

            var c = new Class();
            c.LoadByPrimaryKey(txtClassID.Text);
            lblClassName.Text = c.ClassName;

            var birColl = new BedRoomInCollection();
            birColl.Query.Where(birColl.Query.BedID == e.Value.Split(',')[3].Trim(),
                                birColl.Query.RegistrationNo == txtRegistrationNo.Text, birColl.Query.IsVoid == false);
            birColl.LoadAll();
            chkIsRoomIn.Checked = birColl.Count > 0;
            txtRegistrationNo.Text = e.Value.Split(',')[4].Trim();

            cboParamedicID.Items.Clear();
            var r = new Registration();
            r.LoadByPrimaryKey(txtRegistrationNo.Text);

            txtRegistrationDate.SelectedDate = r.RegistrationDate;
            txtRegistrationTime.Text = r.RegistrationTime;

            PopulatePASUS(r.SRReferralGroup, r.ReferralID, r.ReferralName);

            var par = new ParamedicQuery("a");
            par.Where(par.ParamedicID == r.ParamedicID);
            cboParamedicID.DataSource = par.LoadDataTable();
            cboParamedicID.DataBind();

            cboParamedicID.SelectedValue = r.ParamedicID;
            var p = new Paramedic();
            p.LoadByPrimaryKey(r.ParamedicID);
            cboParamedicID.Text = p.ParamedicName;

            lblRegistrationInfo2.Text = RegistrationInfoSumary.GetDocumentCheckListCountRemains(txtRegistrationNo.Text);
        }

        private void PopulateBedCollection(Registration reg)
        {
            cboBedID.Items.Clear();
            var refers = Helper.MergeBilling.GetMergeRegistration(txtRegistrationNo.Text).Where(m => !m.Equals(txtRegistrationNo.Text));
            if (refers.Any())
            {
                var regs = new RegistrationCollection();
                regs.Query.Where(regs.Query.RegistrationNo.In(refers));
                regs.LoadAll();

                foreach (var entity in regs)
                {
                    var unit = new ServiceUnit();
                    unit.LoadByPrimaryKey(entity.ServiceUnitID);

                    var room = new ServiceRoom();
                    room.LoadByPrimaryKey(entity.RoomID);

                    var c = new Class();
                    c.LoadByPrimaryKey(entity.ChargeClassID);

                    cboBedID.Items.Add(
                        new RadComboBoxItem(
                            unit.ServiceUnitName + ", " + room.RoomName + ", " + c.ClassName + ", " + entity.BedID + ", " + entity.RegistrationNo,
                            entity.ServiceUnitID + ", " + entity.RoomID + ", " + entity.ChargeClassID + ", " + entity.BedID + ", " + entity.RegistrationNo)
                            );
                }
            }

            var transfers = new PatientTransferCollection();
            transfers.Query.Where(
                transfers.Query.RegistrationNo == txtRegistrationNo.Text,
                transfers.Query.IsApprove == true
                );
            transfers.Query.OrderBy(transfers.Query.TransferDate.Descending, transfers.Query.TransferTime.Descending);
            transfers.LoadAll();

            if (transfers.HasData)
            {
                var array = new string[transfers.Count * 2];
                var i = 0;
                foreach (var transfer in transfers)
                {
                    array.SetValue(transfer.FromServiceUnitID + ", " + transfer.FromRoomID + ", " + transfer.FromChargeClassID + ", " + transfer.FromBedID + ", " + transfer.RegistrationNo, i);
                    i++;

                    array.SetValue(transfer.ToServiceUnitID + ", " + transfer.ToRoomID + ", " + transfer.ToChargeClassID + ", " + transfer.ToBedID + ", " + transfer.RegistrationNo, i);
                    i++;
                }

                foreach (var str in array.Distinct())
                {
                    var unit = new ServiceUnit();
                    unit.LoadByPrimaryKey(str.Split(',')[0].Trim());

                    var room = new ServiceRoom();
                    room.LoadByPrimaryKey(str.Split(',')[1].Trim());

                    var c = new Class();
                    c.LoadByPrimaryKey(str.Split(',')[2].Trim());

                    cboBedID.Items.Add(new RadComboBoxItem(unit.ServiceUnitName + ", " + room.RoomName + ", " + c.ClassName + ", " + str.Split(',')[3].Trim() + ", " + str.Split(',')[4].Trim(), str));
                }
            }
            else
            {
                //if (string.IsNullOrEmpty(reg.BedID))
                //    return;

                var unit = new ServiceUnit();
                unit.LoadByPrimaryKey(reg.ServiceUnitID);

                var room = new ServiceRoom();
                room.LoadByPrimaryKey(reg.RoomID);

                var c = new Class();
                c.LoadByPrimaryKey(reg.ChargeClassID);

                cboBedID.Items.Add(new RadComboBoxItem(unit.ServiceUnitName + ", " + room.RoomName + ", " + c.ClassName + ", " + reg.BedID + ", " + reg.RegistrationNo,
                    reg.ServiceUnitID + ", " + reg.RoomID + ", " + reg.ChargeClassID + ", " + reg.BedID + ", " + reg.RegistrationNo));
            }

            var bookingq = new ServiceUnitBookingQuery("a");
            bookingq.Where(bookingq.RegistrationNo == txtRegistrationNo.Text, bookingq.IsApproved == true);
            bookingq.Select(bookingq.ServiceUnitID, bookingq.RoomID);
            bookingq.es.Distinct = true;

            DataTable dtbbooking = bookingq.LoadDataTable();
            if (dtbbooking.Rows.Count > 0)
            {
                foreach (DataRow row in dtbbooking.Rows)
                {
                    var unit = new ServiceUnit();
                    unit.LoadByPrimaryKey(row["ServiceUnitID"].ToString());

                    var room = new ServiceRoom();
                    room.LoadByPrimaryKey(row["RoomID"].ToString());

                    var c = new Class();
                    c.LoadByPrimaryKey(string.IsNullOrEmpty(reg.ProcedureChargeClassID)
                                       ? reg.ChargeClassID
                                       : reg.ProcedureChargeClassID);

                    cboBedID.Items.Add(
                        new RadComboBoxItem(
                            unit.ServiceUnitName + ", " + room.RoomName + ", " + c.ClassName + ", " + reg.BedID + ", " + reg.RegistrationNo,
                            row["ServiceUnitID"] + ", " + row["RoomID"] + ", " + c.ClassID + ", " + reg.BedID + ", " + reg.RegistrationNo)
                            );

                    if (transfers.HasData)
                    {
                        foreach (var transfer in transfers)
                        {
                            c = new Class();
                            c.LoadByPrimaryKey(string.IsNullOrEmpty(reg.ProcedureChargeClassID)
                                      ? transfer.FromChargeClassID
                                      : reg.ProcedureChargeClassID);

                            cboBedID.Items.Add(
                                new RadComboBoxItem(
                                    unit.ServiceUnitName + ", " + room.RoomName + ", " + c.ClassName + ", " + transfer.FromBedID + ", " + transfer.RegistrationNo,
                                    row["ServiceUnitID"] + ", " + row["RoomID"] + ", " + c.ClassID + ", " + transfer.FromBedID + ", " + transfer.RegistrationNo)
                                    );
                        }
                    }
                }
            }
        }

        protected void cboGuarantorID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["GuarantorName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["GuarantorID"].ToString();
        }

        protected void cboTypeResult_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.StandardReferenceItemDataBound(e);
        }

        protected void cboTypeResult_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.StandardReferenceItemsRequested((RadComboBox)o, "TypeResult", e.Text);
        }

        protected void cboSRBloodSampleTakenBy_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.StandardReferenceItemDataBound(e);
        }

        protected void cboSRBloodSampleTakenBy_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.StandardReferenceItemsRequested((RadComboBox)o, "BloodSampleTakenBy", e.Text);
        }

        protected void cboSurgeryPackageID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["PackageName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PackageID"].ToString();
        }

        protected void cboSurgeryPackageID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new SurgicalPackageQuery();
            query.es.Top = 20;
            query.Select(query.PackageID, query.PackageName);
            query.Where
                (
                    query.PackageName.Like(searchTextContain),
                    query.IsActive == true
                );
            query.OrderBy(query.PackageID.Ascending);
            DataTable dtb = query.LoadDataTable();
            cboSurgeryPackageID.DataSource = dtb;
            cboSurgeryPackageID.DataBind();
        }

        protected void cboFromServiceUnitID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ServiceUnitName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ServiceUnitID"].ToString();
        }

        protected void cboServiceUnitBookingNo_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ServiceUnitBookingQuery("a");
            var par = new ParamedicQuery("b");
            query.InnerJoin(par).On(query.ParamedicID == par.ParamedicID);
            query.es.Top = 20;
            query.Select(query.BookingNo, query.RealizationDateTimeFrom, par.ParamedicName);
            query.Where(
                query.Or(par.ParamedicName.Like(searchTextContain),
                         query.BookingNo.Like(searchTextContain)),
                query.IsApproved == true,
                query.RegistrationNo == txtRegistrationNo.Text);
            query.OrderBy(query.BookingNo.Ascending);
            DataTable dtb = query.LoadDataTable();
            cboServiceUnitBookingNo.DataSource = dtb;
            cboServiceUnitBookingNo.DataBind();
        }

        protected void cboServiceUnitBookingNo_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["BookingNo"] + " [" +
                          (string.Format("{0:dd-MMM-yyyy}",
                                         Convert.ToDateTime(((DataRowView)e.Item.DataItem)["RealizationDateTimeFrom"]))) +
                          "] " + ((DataRowView)e.Item.DataItem)["ParamedicName"];
            e.Item.Value = ((DataRowView)e.Item.DataItem)["BookingNo"].ToString();
        }

        protected void cboSRKiaCaseType_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new AppStandardReferenceItemQuery();
            query.es.Top = 20;
            query.Select(query.ItemID, query.ItemName);
            query.Where(
                query.Or(query.ItemID.Like(searchTextContain),
                         query.ItemName.Like(searchTextContain)),
                query.IsActive == true, query.StandardReferenceID == AppEnum.StandardReference.KiaCaseType);
            query.OrderBy(query.ItemID.Ascending);
            DataTable dtb = query.LoadDataTable();
            cboSRKiaCaseType.DataSource = dtb;
            cboSRKiaCaseType.DataBind();
        }

        protected void cboSRKiaCaseType_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboSRObstetricType_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new AppStandardReferenceItemQuery();
            query.es.Top = 20;
            query.Select(query.ItemID, query.ItemName);
            query.Where(
                query.Or(query.ItemID.Like(searchTextContain),
                         query.ItemName.Like(searchTextContain)),
                query.IsActive == true, query.StandardReferenceID == AppEnum.StandardReference.ObstetricType);
            query.OrderBy(query.ItemID.Ascending);
            DataTable dtb = query.LoadDataTable();
            cboSRObstetricType.DataSource = dtb;
            cboSRObstetricType.DataBind();
        }

        protected void cboSRObstetricType_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboPhysicianSendersID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ParamedicName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ParamedicID"].ToString();
        }

        protected void cboPhysicianSender_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var prm = new ParamedicQuery();
            prm.Where(prm.IsActive == true,
                prm.Or(
                    prm.ParamedicID.Like(searchTextContain),
                    prm.ParamedicName.Like(searchTextContain)
                ))
                .Select(prm.ParamedicID, prm.ParamedicName);
            var dtb = prm.LoadDataTable();

            string appParamRefGroup = AppSession.Parameter.PhysicianSenderReferralGroups;
            string[] referralGroup = appParamRefGroup.Split(',');
            if (referralGroup.Count() > 0)
            {
                var refer = new ReferralQuery();
                refer.Where(
                        refer.SRReferralGroup.In(referralGroup),
                        refer.IsActive == true,
                        refer.ReferralName.Like(searchTextContain))
                    .Select(
                        refer.ReferralID.As("ParamedicID"),
                        refer.ReferralName.As("ParamedicName"));
                dtb.Merge(refer.LoadDataTable());
            }

            DataView view = new DataView(dtb);
            DataTable distinctValues = view.ToTable(true, "ParamedicName");

            distinctValues.DefaultView.Sort = "ParamedicName";
            cboPhysicianSender.DataSource = distinctValues;
            cboPhysicianSender.DataBind();
        }

        protected void cboPhysicianSender_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ParamedicName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ParamedicName"].ToString();
        }

        #region PatientImage
        private void PopulatePatientImage(string patientID)
        {
            // Load from database
            var patientImg = new PatientImage();

            if (patientID == null) patientID = "";

            if (patientImg.LoadByPrimaryKey(patientID))
            {
                // Show Image
                if (patientImg.Photo != null)
                {
                    imgPatientPhoto.ImageUrl = string.Format("data:image/jpg;base64,{0}",
                        Convert.ToBase64String(patientImg.Photo));
                }
                else
                {
                    imgPatientPhoto.ImageUrl = txtGender.Text == "M" ? "~/Images/Asset/Patient/ManVector.png" : (txtGender.Text == "F" ? "~/Images/Asset/Patient/WomanVector.png" : "~/Images/Asset/Patient/HumanVector.png");
                }
            }
            else
                imgPatientPhoto.ImageUrl = txtGender.Text == "M" ? "~/Images/Asset/Patient/ManVector.png" : (txtGender.Text == "F" ? "~/Images/Asset/Patient/WomanVector.png" : "~/Images/Asset/Patient/HumanVector.png");

        }
        #endregion

        #region PASUS
        private void PopulatePASUS(string refGroupId, string refId, string refDesc)
        {
            if (!string.IsNullOrEmpty(refGroupId) && AppSession.Parameter.ReferralGroupPASUS.Contains(refGroupId))
            {
                trPASUS.Visible = true;
                var captionPASUS = AppSession.Parameter.ReferralGroupPASUSLabel;
                if (!string.IsNullOrEmpty(refDesc))
                    captionPASUS = captionPASUS + ": " + refDesc;
                else if (!string.IsNullOrEmpty(refId))
                {
                    var r = new Referral();
                    if (r.LoadByPrimaryKey(refId))
                        captionPASUS = captionPASUS + ": " + r.ReferralName;
                }
                lblPASUS.Text = captionPASUS;
            }
            else
            {
                trPASUS.Visible = false;
                lblPASUS.Text = string.Empty;
            }
        }
        #endregion

        #region Temporary Bill

        private decimal GetTotalTemporaryBill()
        {
            var tciColl = TransChargesItems;
            var tciTemps = TransChargesItemTemporaries;
            var tpiTemps = TransPrescriptionItemTemporaries;
            

            decimal tbilling = 0;

            foreach (var i in tciColl)
            {
                tbilling = tbilling + (((i.ChargeQuantity ?? 0) * (i.Price ?? 0)) - (i.DiscountAmount ?? 0) + (i.CitoAmount ?? 0));
            }

            foreach (var i in tciTemps)
            {
                tbilling = tbilling + (((i.ChargeQuantity ?? 0) * (i.Price ?? 0)) - (i.DiscountAmount ?? 0) + (i.CitoAmount ?? 0));
            }

            foreach (var i in tpiTemps)
            {
                tbilling = tbilling + (i.LineAmount ?? 0);
            }

            return tbilling;
        }


        private TransChargesItemCollection TransChargesItemTemporaries
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = Session["collTransChargesItemTemporaries" + Request.UserHostName + hdnPageId.Value];
                    if (obj != null)
                        return ((TransChargesItemCollection)(obj));
                }

                var regnos = Helper.MergeBilling.GetMergeRegistration(Request.QueryString["regno"].ToString());

                var transNos = new TransChargesQuery();
                transNos.Where(transNos.TransactionNo != txtTransactionNo.Text, transNos.RegistrationNo.In(regnos), 
                    transNos.Or(transNos.PackageReferenceNo == string.Empty, transNos.PackageReferenceNo.IsNull()),
                    transNos.IsVoid == false);
                transNos.Select(transNos.TransactionNo);

                var coll = new TransChargesItemCollection();
                var query = new TransChargesItemQuery("a");

                query.Where(query.TransactionNo.In(transNos), query.IsVoid == false, query.Or(query.ParentNo == string.Empty, query.ParentNo.IsNull()));
                coll.Load(query);

                Session["collTransChargesItemTemporaries" + Request.UserHostName + hdnPageId.Value] = coll;
                return coll;
            }
            set { Session["collTransChargesItemTemporaries" + Request.UserHostName + hdnPageId.Value] = value; }
        }

        private TransPrescriptionItemCollection TransPrescriptionItemTemporaries
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = Session["collTransPrescriptionItemTemporaries" + Request.UserHostName + hdnPageId.Value];
                    if (obj != null)
                        return ((TransPrescriptionItemCollection)(obj));
                }

                var regnos = Helper.MergeBilling.GetMergeRegistration(Request.QueryString["regno"].ToString());

                var prescNos = new TransPrescriptionQuery();
                prescNos.Where(prescNos.RegistrationNo.In(regnos), prescNos.IsVoid == false);
                prescNos.Select(prescNos.PrescriptionNo);

                var coll = new TransPrescriptionItemCollection();
                var query = new TransPrescriptionItemQuery("a");

                query.Where(query.PrescriptionNo.In(prescNos), query.IsVoid == false);
                coll.Load(query);

                Session["collTransPrescriptionItemTemporaries" + Request.UserHostName + hdnPageId.Value] = coll;
                return coll;
            }
            set { Session["collTransPrescriptionItemTemporaries" + Request.UserHostName + hdnPageId.Value] = value; }
        }
        #endregion

    }
}