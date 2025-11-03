using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using Telerik.Web.UI;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using fastJSON;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class BillingAdjustDetail : BasePage
    {
        //private string RegistrationNo {
        //    get {
        //        return Request.QueryString["regNo"];
        //    }
        //}

        private BillingAdjustment BillAdjust
        {
            get {
                if (Page.IsPostBack)
                {
                    var obj = Session["BillingAdjust:Registration" + RegistrationNo];
                    if (obj != null)
                        return ((BillingAdjustment)(obj));
                }

                var reg = new BillingAdjustment(RegistrationNo, false);
                
                Session["BillingAdjust:Registration" + RegistrationNo] = reg;
                return (BillingAdjustment)Session["BillingAdjust:Registration" + RegistrationNo];
            }
            set {
                Session["BillingAdjust:Registration" + RegistrationNo] = value;
            }
        }

        protected override void OnInitComplete(EventArgs e)
        {
            base.OnInitComplete(e);
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            if (!Page.IsPostBack)
            {
                SetJsonAdjustLog();
            }
        }

        private ParamedicFeeTransChargesItemCompByDischargeDateCollection GetPhysicianFee() {
            var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
            feeColl.Query.Where(
                feeColl.Query.RegistrationNoMergeTo == RegistrationNo, 
                feeColl.Query.VerificationNo.IsNull()
            );
            feeColl.LoadAll();

            return feeColl;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ProgramID = AppConstant.Program.VerificationFinalizeBilling;

            if (!Page.IsPostBack) {
                lblRegistrationNo.Text = BillAdjust.RegistrationNo;

                var pat = new Patient();
                if (pat.LoadByPrimaryKey(BillAdjust.PatientID))
                {
                    lblMedicalNo.Text = pat.MedicalNo;
                    lblPatientName.Text = pat.PatientName;
                    rblSex.SelectedValue = pat.Sex;
                    lblAge.Text = string.Format("{0}Y {1}M {2}D",
                        BillAdjust.AgeInYear.ToString(), BillAdjust.AgeInMonth.ToString(),
                        BillAdjust.AgeInDay.ToString());
                }
            }

            var valMsg = BillAdjust.ValidateFee();
            if (!string.IsNullOrEmpty(valMsg)) ShowInformation(valMsg);
        }

        private void ShowInformation(string information)
        {
            lblInformation.Text = information;
            pnlInformation.Visible = true;
        }

        private void Refresh() {
            // reset registrasi
            //BillAdjust = null;
            //CostCalculations = null;

            grdCostCalculation.Rebind();
            grdFeePreview.Rebind();
        }

        private void CalculateTotal() {
            var transAmount = BillAdjust.CostCalculations.Sum(x => (x.PatientAmount ?? 0) + (x.GuarantorAmount ?? 0));
            lblTransactionAmount.Text = string.Format("{0:N2}", transAmount);

            var ibs = BillAdjust.CostCalculations.Where(x => x.IntermBillNo != null).Select(x => x.IntermBillNo).Distinct();

            decimal admAmount = 0;

            if (ibs.Count() > 0)
            {
                IntermBillCollection ibColl = new IntermBillCollection();
                ibColl.Query.Where(ibColl.Query.IntermBillNo.In(ibs));
                if (ibColl.LoadAll())
                {
                    admAmount = ibColl.Sum(x => (x.AdministrationAmount ?? 0) + (x.GuarantorAdministrationAmount ?? 0) - (x.DiscAdmPatient ?? 0) - (x.DiscAdmGuarantor ?? 0));
                }
            }
            lblAdmAmount.Text = string.Format("{0:N2}", admAmount);

            lblTotalAmount.Text = string.Format("{0:N2}",(transAmount + admAmount));

            var SumAmount = BillAdjust.CostCalculations.Sum(x => (x.AmountAdjusted ?? 0));
            lblSimulationAmount.Text = string.Format("{0:N2}", SumAmount);

            var plafon = BillAdjust.Plavon;
            lblPlafonAmount.Text = string.Format("{0:N2}", plafon);

            lblDifferent.Text = string.Format("{0:N2}", (plafon - SumAmount));
        }

        protected void grdCostCalculation_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdCostCalculation.DataSource = BillAdjust.CostCalculations;
            CalculateTotal();
        }
        protected void grdCostCalculation_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var cc = e.Item.DataItem as CostCalculation;
                //var txtDisc = e.Item.FindControl("txtDisc") as RadNumericTextBox;
                //if (txtDisc != null) {
                //    txtDisc.Value = System.Convert.ToDouble(cc.AdjustedDisc.AdjustedDiscAmount);
                //}

                var rblDisc = e.Item.FindControl("rblDisc") as RadioButtonList;
                if (rblDisc != null)
                {
                    rblDisc.SelectedValue = cc.AdjustedDiscSelection.ToString();//cc.AdjustedDisc.AdjustedDiscSelection.ToString();
                }

                // color
                System.Drawing.Color cl = System.Drawing.Color.WhiteSmoke;
                if (e.Item.ItemType == GridItemType.AlternatingItem) {
                    cl = System.Drawing.Color.LightBlue;
                }
                e.Item.Cells[11].BackColor = cl;
                e.Item.Cells[13].BackColor = cl;
                e.Item.Cells[14].BackColor = cl;

                if (cc.AmountAdjusted.HasValue && cc.es.ModifiedColumns.Count == 0) {
                    e.Item.ForeColor = System.Drawing.Color.Green;
                }
            }
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);

            if (string.IsNullOrEmpty(eventArgument) || !(source is RadGrid))
                return;

            switch (eventArgument)
            {
                
                case "copy":
                    CopyAdjustmentDefault();
                    Refresh();
                    break;
                case "calculate":
                    CalculateAdjustment();
                    Refresh();
                    break;
                case "save":
                    SaveAdjustment();
                    Refresh();
                    break;
                case "delete":
                    DeleteAdjustment();
                    Refresh();
                    break;
                case "refresh":
                    Refresh();
                    break;
                case "printd":
                    Print(AppConstant.Report.BillingStatementDetail2, false, true, true);
                    break;
            }
        }

        private string[] IntermBills
        {
            get
            {
                return BillAdjust.CostCalculations.Where(x => !string.IsNullOrEmpty(x.IntermBillNo))
                    .Select(x => x.IntermBillNo).Distinct().ToArray();
            }
        }

        //private string[] MergeRegistrationList()
        //{
        //    return Helper.MergeBilling.GetMergeRegistration(RegistrationNo);
        //}

        private void Print(string reportName, bool forceUseNoIntermbill,
            bool ShowPatientPaid, bool? IsAdjusted)
        {
            var jobParameters = new PrintJobParameterCollection();

            var jobParameter = jobParameters.AddNew();
            jobParameter.Name = "IntermBillNoList";
            jobParameter.ValueString = string.Empty;

            if (AppSession.Parameter.IsUsingIntermBill && !forceUseNoIntermbill)
            {
                foreach (var str in IntermBills)
                {
                    jobParameter.ValueString += str + ",";
                }

                if (jobParameter.ValueString == string.Empty)
                    return;

                jobParameter.ValueString = jobParameter.ValueString.Substring(0, jobParameter.ValueString.Length - 1);
            }

            string[] registrationNoList = MergeBilling.GetMergeRegistration(RegistrationNo);
            var jobParameter2 = jobParameters.AddNew();
            jobParameter2.Name = "RegistrationNoList";
            jobParameter2.ValueString = string.Empty;

            foreach (var str in registrationNoList)
            {
                jobParameter2.ValueString += str + ",";
            }

            jobParameter2.ValueString = jobParameter2.ValueString.Substring(0, jobParameter2.ValueString.Length - 1);

            var parRegNo = jobParameters.AddNew();
            parRegNo.Name = "RegNo";
            parRegNo.ValueString = RegistrationNo;

            var parUserID = jobParameters.AddNew();
            parUserID.Name = "UserID";
            parUserID.ValueString = AppSession.UserLogin.UserID;

            var parUser = jobParameters.AddNew();
            parUser.Name = "UserName";
            parUser.ValueString = AppSession.UserLogin.UserName;

            var parplafond = jobParameters.AddNew();
            parplafond.Name = "plafond";
            parplafond.ValueString = BillAdjust.Plavon.ToString();

            var parDate1 = jobParameters.AddNew();
            parDate1.Name = "StartDate";
            parDate1.ValueDateTime = Convert.ToDateTime("1900-01-01 00:00:00");

            var parDate2 = jobParameters.AddNew();
            parDate2.Name = "EndDate";
            parDate2.ValueDateTime = (new DateTime()).NowAtSqlServer().AddDays(10);

            var parSelfGuarantor = jobParameters.AddNew();
            parSelfGuarantor.Name = "SelfGuarantor";
            parSelfGuarantor.ValueString = AppSession.Parameter.SelfGuarantor;

            var parAksesGuarantor = jobParameters.AddNew();
            parAksesGuarantor.Name = "AskesGuarantor";
            parAksesGuarantor.ValueString = string.Empty;// _guarantorAskesID;

            if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSSMCB")
            {
                var parShowPatientPaid = jobParameters.AddNew();
                parShowPatientPaid.Name = "ShowPatientPaid";
                parShowPatientPaid.ValueNumeric = ShowPatientPaid ? 1 : 0;// _guarantorAskesID;
            }

            if (IsAdjusted.HasValue) {
                var parIsAdjusted = jobParameters.AddNew();
                parIsAdjusted.Name = "IsAdjusted";
                parIsAdjusted.ValueNumeric = IsAdjusted.Value ? 1 : 0;
            }

            AppSession.PrintJobParameters = jobParameters;
            AppSession.PrintJobReportID = reportName;

            string script = @"var oWnd = $find('" + winPrint.ClientID + "');" +
                            "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
                            "oWnd.Show();" +
                            "oWnd.Maximize();";
            RadAjaxPanel1.ResponseScripts.Add(script);
        }

        private void SetJsonAdjustLog(){
            if (BillAdjust.AdjustLog == string.Empty) return;

            try
            {
                var log = fastJSON.JSON.ToObject<AdjustLog[]>(BillAdjust.AdjustLog);

                foreach (var l in log)
                {
                    switch (l.AdjustType)
                    {
                        case "ItemGroup":
                            {
                                foreach (GridDataItem gdi in radgrdItemGroup.Items)
                                {
                                    if (gdi.GetDataKeyValue("ItemGroupID").ToString() == l.Key)
                                    {
                                        var txtDisc = gdi.FindControl("txtDisc") as RadNumericTextBox;
                                        var rblDisc = gdi.FindControl("rblDisc") as RadioButtonList;
                                        if (txtDisc != null) txtDisc.Value = System.Convert.ToDouble(l.AdjustDisc.AdjustedDiscAmount ?? 0);
                                        if (rblDisc != null) rblDisc.SelectedValue = l.AdjustDisc.AdjustedDiscSelection.ToString();
                                    }
                                }
                                break;
                            }
                        case "ServiceUnit":
                            {
                                foreach (GridDataItem gdi in radgrdServiceUnit.Items)
                                {
                                    if (gdi.GetDataKeyValue("ServiceUnitID").ToString() == l.Key)
                                    {
                                        var txtDisc = gdi.FindControl("txtDisc") as RadNumericTextBox;
                                        var rblDisc = gdi.FindControl("rblDisc") as RadioButtonList;
                                        if (txtDisc != null) txtDisc.Value = System.Convert.ToDouble(l.AdjustDisc.AdjustedDiscAmount ?? 0);
                                        if (rblDisc != null) rblDisc.SelectedValue = l.AdjustDisc.AdjustedDiscSelection.ToString();
                                    }
                                }
                                break;
                            }
                        case "ItemType":
                            {
                                foreach (GridDataItem gdi in radgrdItemType.Items)
                                {
                                    if (gdi.GetDataKeyValue("ItemID").ToString() == l.Key)
                                    {
                                        var txtDisc = gdi.FindControl("txtDisc") as RadNumericTextBox;
                                        var rblDisc = gdi.FindControl("rblDisc") as RadioButtonList;
                                        if (txtDisc != null) txtDisc.Value = System.Convert.ToDouble(l.AdjustDisc.AdjustedDiscAmount ?? 0);
                                        if (rblDisc != null) rblDisc.SelectedValue = l.AdjustDisc.AdjustedDiscSelection.ToString();
                                    }
                                }
                                break;
                            }
                    }
                }
            }
            catch (Exception ex) {
                LogError(ex);
            }
        }

        private void DeleteAdjustment() {
            var saveMsg = BillAdjust.DeleteAdjustment(AppSession.UserLogin.UserID);
            if (!string.IsNullOrEmpty(saveMsg)) ShowInformation(saveMsg);
        }

        private void SaveAdjustment() {
            var saveMsg = BillAdjust.SaveAdjustment(AppSession.UserLogin.UserID);
            if (!string.IsNullOrEmpty(saveMsg)) ShowInformation(saveMsg);
        }

        private void CopyAdjustmentDefault()
        {
            BillAdjust.CopyAdjustmentDefault();
        }

        //private void CalculateAdjustment()
        //{
        //    // item group
        //    decimal plafon = GetPlafon();
        //    // RESET
        //    CopyAdjustment();

        //    #region Adjust by master item
        //    // read default setting per item
        //    var iDefault = new BillingAdjustItemSettingCollection();
        //    iDefault.LoadAll();
        //    foreach (var cc in CostCalculations)
        //    {
        //        // find related setting
        //        var iSet = iDefault.Where(x => x.ItemID == cc.ItemID && x.TariffComponentID != string.Empty);
        //        if (iSet.Any())
        //        {
        //            var tcics = TransChargesItemComps.Where(x => x.TransactionNo == cc.TransactionNo &&
        //                x.SequenceNo == cc.SequenceNo);
        //            foreach (var tcic in tcics)
        //            {
        //                var iSetComp = iSet.Where(x => x.TariffComponentID == tcic.TariffComponentID).FirstOrDefault();
        //                if (iSetComp != null)
        //                {
        //                    if (iSetComp.HasReplacement(CostCalculations))
        //                    {
        //                        tcic.PriceAdjusted = 0;
        //                    }
        //                    else
        //                    {
        //                        tcic.PriceAdjusted = (iSetComp.IsFeeValueInPercent ?? false) ? (iSetComp.FeeValue / 100 * tcic.GetFinalValue()) : iSetComp.FeeValue;
        //                    }
        //                }
        //                else
        //                {
        //                    tcic.PriceAdjusted = 0;
        //                }
        //            }
        //            var discAdj = new AdjustedDisc();
        //            cc.SetAdjustmentDisc(discAdj);
        //            cc.SetAdjustmentValueWithoutTCIC(TransChargesItems, TransChargesItemComps, tcics.Sum(x => x.PriceAdjusted ?? 0));

        //        }
        //        else
        //        {
        //            // find setting without tariffcomponentid
        //            var iSet2 = iDefault.Where(x => x.ItemID == cc.ItemID && x.TariffComponentID == string.Empty).FirstOrDefault();
        //            if (iSet2 != null)
        //            {
        //                var discAdj = new AdjustedDisc();
        //                cc.SetAdjustmentDisc(discAdj);
        //                if (iSet2.HasReplacement(CostCalculations))
        //                {
        //                    cc.SetAdjustmentValue(TransChargesItems, TransChargesItemComps, 0);
        //                }
        //                else
        //                {
        //                    if (iSet2.IsFeeValueInPercent ?? false)
        //                    {
        //                        cc.SetAdjustmentValue(TransChargesItems, TransChargesItemComps, cc.AmountTransaction - (cc.AmountTransaction * (iSet2.FeeValue ?? 0) / 100));
        //                    }
        //                    else
        //                    {
        //                        cc.SetAdjustmentValue(TransChargesItems, TransChargesItemComps, iSet2.FeeValue ?? 0);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    #endregion

        //    #region adjust by master item group
        //    // read default setting
        //    var IGDefault = new BillingAdjustItemGroupSettingCollection();
        //    IGDefault.LoadAll();

        //    // Item Group
        //    foreach (GridDataItem gdi in radgrdItemGroup.Items)
        //    {
        //        var ig = ItemGroups.Where(x => x.ItemGroupID == gdi.GetDataKeyValue("ItemGroupID").ToString()).FirstOrDefault();
        //        if (ig != null)
        //        {
        //            // reset auto adjust
        //            ig.AutoAdjust = false;

        //            var txtDA = gdi.FindControl("txtDisc") as RadNumericTextBox;
        //            var rbSelection = gdi.FindControl("rblDisc") as RadioButtonList;

        //            // load default setting
        //            if (!txtDA.Value.HasValue)
        //            {
        //                var igDef = IGDefault.Where(x => x.ItemGroupID == gdi.GetDataKeyValue("ItemGroupID").ToString()).FirstOrDefault();
        //                if (igDef != null)
        //                {
        //                    if (igDef.DiscValue.HasValue)
        //                    {
        //                        txtDA.Value = System.Convert.ToDouble(igDef.DiscValue ?? 0);
        //                        rbSelection.SelectedIndex = igDef.DiscSelection ?? 0;
        //                    }
        //                }
        //                else
        //                {
        //                    // cek auto adjust
        //                    var chk = gdi.FindControl("chkAutoAdjustItemGroup") as CheckBox;
        //                    if (chk != null)
        //                    {
        //                        ig.AutoAdjust = chk.Checked;
        //                    }
        //                }
        //            }

        //            if (txtDA != null)
        //            {
        //                if (txtDA.Value.HasValue)
        //                {
        //                    ig.AdjustedDisc.AdjustedDiscAmount = System.Convert.ToDecimal(txtDA.Value ?? 0);

        //                    if (rbSelection != null)
        //                    {
        //                        ig.AdjustedDisc.AdjustedDiscSelection = System.Convert.ToInt16(rbSelection.SelectedValue);
        //                    }
        //                }
        //                else
        //                {
        //                    ig.AdjustedDisc.AdjustedDiscAmount = null;
        //                    ig.AdjustedDisc.AdjustedDiscSelection = null;
        //                }
        //            }
        //        }
        //    }

        //    foreach (var ig in ItemGroups)
        //    {
        //        if (ig.AdjustedDisc.AdjustedDiscAmount.HasValue)
        //        {
        //            {
        //                if (ig.AdjustedDisc.AdjustedDiscSelection == 0/*tarif*/)
        //                {
        //                    var ccSelected = CostCalculations.Where(x => x.ItemGroupID == ig.ItemGroupID);
        //                    foreach (var cc in ccSelected)
        //                    {
        //                        cc.SetAdjustmentDisc(ig.AdjustedDisc);
        //                        cc.SetAdjustmentValue(TransChargesItems, TransChargesItemComps, cc.AmountTransaction - (cc.AmountTransaction * (ig.AdjustedDisc.AdjustedDiscAmount ?? 0) / 100));
        //                    }
        //                }
        //                else if (ig.AdjustedDisc.AdjustedDiscSelection == 1/*plafon*/)
        //                {
        //                    var plafonDiscAmount = plafon * (ig.AdjustedDisc.AdjustedDiscAmount ?? 0) / 100;
        //                    var ccSelected = CostCalculations.Where(x => x.ItemGroupID == ig.ItemGroupID);
        //                    var ccSum = ccSelected.Sum(x => x.AmountTransaction);

        //                    foreach (var cc in ccSelected)
        //                    {
        //                        cc.SetAdjustmentDisc(ig.AdjustedDisc);
        //                        cc.SetAdjustmentValue(TransChargesItems, TransChargesItemComps, plafonDiscAmount / ccSum * cc.AmountTransaction);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    #endregion

        //    #region hitung proporsional yang itemgroupnya auto adjust
        //    // hitung proporsional

        //    // cc sudah adjust atau cc belum adjust tapi yang groupnya tidak centang auto adjust
        //    var totalAdjusted = CostCalculations.Where(x => x.IsAdjusted == true || (x.IsAdjusted == false && ItemGroups.Where(ig => ig.AutoAdjust == false).Select(ig => ig.ItemGroupID).Contains(x.ItemGroupID)))
        //        .Sum(x => x.AmountAdjusted ?? 0);

        //    // cc belum adjust dan cc yang groupnya centang auto adjust
        //    var ccUnadjusted = CostCalculations.Where(x => x.IsAdjusted == false && ItemGroups.Where(ig => ig.AutoAdjust == true).Select(ig => ig.ItemGroupID).Contains(x.ItemGroupID));

        //    var totalUnAdjusted = ccUnadjusted.Sum(x => (x.PatientAmount ?? 0) + (x.GuarantorAmount ?? 0));

        //    var sisa = GetPlafon() - totalAdjusted;

        //    decimal newValSum = 0;
        //    if (sisa > 0)
        //    {
        //        int ccUnadjustedCount = ccUnadjusted.Count();
        //        int idx = 0;
        //        foreach (var cc in ccUnadjusted)
        //        {
        //            idx++;
        //            var newVal = sisa * ((cc.PatientAmount ?? 0) + (cc.GuarantorAmount ?? 0)) / totalUnAdjusted;
        //            newVal = Math.Round(newVal, 0);
        //            newValSum += newVal;

        //            if (idx == ccUnadjustedCount)
        //            { // finishing
        //                // tambahkan di cc terakhir jika ada sisa-sisa pembulatan
        //                newVal += sisa - newValSum;
        //            }

        //            var discAdj = new AdjustedDisc();
        //            cc.SetAdjustmentDisc(discAdj);
        //            cc.SetAdjustmentValue(TransChargesItems, TransChargesItemComps, newVal);
        //        }
        //    }else{
        //        ShowInformation("Auto calculate can not be done due to minus result!");
        //    }

        //    #endregion

        //    //// Service Unit
        //    //foreach (GridDataItem gdi in radgrdServiceUnit.Items)
        //    //{
        //    //    var su = ServiceUnits.Where(x => x.ServiceUnitID == gdi.GetDataKeyValue("ServiceUnitID").ToString()).FirstOrDefault();
        //    //    if (su != null)
        //    //    {
        //    //        var txtDA = gdi.FindControl("txtDisc") as RadNumericTextBox;
        //    //        decimal discAmount = 0;
        //    //        int iSelection = 0;
        //    //        if (txtDA != null)
        //    //        {
        //    //            discAmount = System.Convert.ToDecimal(txtDA.Value ?? 0);
        //    //        }

        //    //        var rbSelection = gdi.FindControl("rblDisc") as RadioButtonList;
        //    //        if (rbSelection != null)
        //    //        {
        //    //            iSelection = System.Convert.ToInt16(rbSelection.SelectedValue);
        //    //        }

        //    //        su.AdjustedDisc.AdjustedDiscAmount = discAmount;
        //    //        su.AdjustedDisc.AdjustedDiscSelection = iSelection;
        //    //    }
        //    //}
        //    //foreach (var su in ServiceUnits)
        //    //{
        //    //    if ((su.AdjustedDisc.AdjustedDiscAmount ?? 0) > 0)
        //    //    {
        //    //        if (su.AdjustedDisc.AdjustedDiscSelection == 0/*tarif*/)
        //    //        {
        //    //            var ccSelected = CostCalculations.Where(x => x.ServiceUnitName == su.ServiceUnitName);
        //    //            foreach (var cc in ccSelected)
        //    //            {
        //    //                cc.SetAdjustmentDisc(su.AdjustedDisc);
        //    //                cc.SetAdjustmentValue(TransChargesItems, TransChargesItemComps, cc.AmountTransaction - (cc.AmountTransaction * (su.AdjustedDisc.AdjustedDiscAmount ?? 0) / 100));
        //    //            }
        //    //        }
        //    //        else if (su.AdjustedDisc.AdjustedDiscSelection == 1/*plafon*/)
        //    //        {
        //    //            var plafonDiscAmount = plafon * (su.AdjustedDisc.AdjustedDiscAmount ?? 0) / 100;
        //    //            var ccSelected = CostCalculations.Where(x => x.ServiceUnitName == su.ServiceUnitName);
        //    //            var ccSum = ccSelected.Sum(x => x.AmountTransaction);

        //    //            foreach (var cc in ccSelected)
        //    //            {
        //    //                cc.SetAdjustmentDisc(su.AdjustedDisc);
        //    //                cc.SetAdjustmentValue(TransChargesItems, TransChargesItemComps, plafonDiscAmount / ccSum * cc.AmountTransaction);
        //    //            }
        //    //        }
        //    //    }
        //    //}

        //    //// Item Type
        //    //foreach (GridDataItem gdi in radgrdItemType.Items)
        //    //{
        //    //    var srIt = ItemTypes.Where(x => x.ItemID == gdi.GetDataKeyValue("ItemID").ToString()).FirstOrDefault();
        //    //    if (srIt != null)
        //    //    {
        //    //        var txtDA = gdi.FindControl("txtDisc") as RadNumericTextBox;
        //    //        decimal discAmount = 0;
        //    //        int iSelection = 0;
        //    //        if (txtDA != null)
        //    //        {
        //    //            discAmount = System.Convert.ToDecimal(txtDA.Value ?? 0);
        //    //        }

        //    //        var rbSelection = gdi.FindControl("rblDisc") as RadioButtonList;
        //    //        if (rbSelection != null)
        //    //        {
        //    //            iSelection = System.Convert.ToInt16(rbSelection.SelectedValue);
        //    //        }

        //    //        srIt.AdjustedDisc.AdjustedDiscAmount = discAmount;
        //    //        srIt.AdjustedDisc.AdjustedDiscSelection = iSelection;
        //    //    }
        //    //}
        //    //foreach (var srIt in ItemTypes)
        //    //{
        //    //    if ((srIt.AdjustedDisc.AdjustedDiscAmount ?? 0) > 0)
        //    //    {
        //    //        if (srIt.AdjustedDisc.AdjustedDiscSelection == 0/*tarif*/)
        //    //        {
        //    //            var ccSelected = CostCalculations.Where(x => x.ItemTypeID == srIt.ItemID);
        //    //            foreach (var cc in ccSelected)
        //    //            {
        //    //                cc.SetAdjustmentDisc(srIt.AdjustedDisc);
        //    //                cc.SetAdjustmentValue(TransChargesItems, TransChargesItemComps, cc.AmountTransaction - (cc.AmountTransaction * (srIt.AdjustedDisc.AdjustedDiscAmount ?? 0) / 100));
        //    //            }
        //    //        }
        //    //        else if (srIt.AdjustedDisc.AdjustedDiscSelection == 1/*plafon*/)
        //    //        {
        //    //            var plafonDiscAmount = plafon * (srIt.AdjustedDisc.AdjustedDiscAmount ?? 0) / 100;
        //    //            var ccSelected = CostCalculations.Where(x => x.ItemTypeID == srIt.ItemID);
        //    //            var ccSum = ccSelected.Sum(x => x.AmountTransaction);

        //    //            foreach (var cc in ccSelected)
        //    //            {
        //    //                cc.SetAdjustmentDisc(srIt.AdjustedDisc);
        //    //                cc.SetAdjustmentValue(TransChargesItems, TransChargesItemComps, plafonDiscAmount / ccSum * cc.AmountTransaction);
        //    //            }
        //    //        }
        //    //    }
        //    //}
        //}

        private void CalculateAdjustment()
        {
            #region adjust by master item group
            // read default setting
            var IGDefault = new BillingAdjustItemGroupSettingCollection();
            IGDefault.LoadAll();

            //var BA = BillAdjust;

            // Item Group
            foreach (GridDataItem gdi in radgrdItemGroup.Items)
            {
                var ig = BillAdjust.ItemGroups.Where(x => x.ItemGroupID == gdi.GetDataKeyValue("ItemGroupID").ToString()).FirstOrDefault();
                if (ig != null)
                {
                    // reset auto adjust
                    ig.AutoAdjust = false;

                    var txtDA = gdi.FindControl("txtDisc") as RadNumericTextBox;
                    var rbSelection = gdi.FindControl("rblDisc") as RadioButtonList;

                    // load default setting
                    if (!txtDA.Value.HasValue)
                    {
                        var igDef = IGDefault.Where(x => x.ItemGroupID == gdi.GetDataKeyValue("ItemGroupID").ToString()).FirstOrDefault();
                        if (igDef != null)
                        {
                            if (igDef.DiscValue.HasValue)
                            {
                                txtDA.Value = System.Convert.ToDouble(igDef.DiscValue ?? 0);
                                rbSelection.SelectedIndex = igDef.DiscSelection ?? 0;
                            }
                        }
                        else
                        {
                            // cek auto adjust
                            var chk = gdi.FindControl("chkAutoAdjustItemGroup") as CheckBox;
                            if (chk != null)
                            {
                                ig.AutoAdjust = chk.Checked;
                            }
                        }
                    }

                    if (txtDA != null)
                    {
                        if (txtDA.Value.HasValue)
                        {
                            ig.AdjustedDisc.AdjustedDiscAmount = System.Convert.ToDecimal(txtDA.Value ?? 0);

                            if (rbSelection != null)
                            {
                                ig.AdjustedDisc.AdjustedDiscSelection = System.Convert.ToInt16(rbSelection.SelectedValue);
                            }
                        }
                        else
                        {
                            ig.AdjustedDisc.AdjustedDiscAmount = null;
                            ig.AdjustedDisc.AdjustedDiscSelection = null;
                        }
                    }
                }
            }

            var retMsg = BillAdjust.CalculateAdjustment();

            if (!string.IsNullOrEmpty(retMsg))
            {
                ShowInformation(retMsg);
            }

            //BillAdjust = BA;

            #endregion
        }


        protected void txtAmountAdjusted_TextChanged(object sender, EventArgs e)
        {
            var txtAmountAdjusted = (RadNumericTextBox)sender;
            string TransactionNo = (txtAmountAdjusted.Parent.Parent as GridDataItem).GetDataKeyValue("TransactionNo").ToString();
            string SequenceNo = (txtAmountAdjusted.Parent.Parent as GridDataItem).GetDataKeyValue("SequenceNo").ToString();
            var cc = BillAdjust.CostCalculations.Where(x => x.TransactionNo == TransactionNo && x.SequenceNo == SequenceNo).First();

            cc.SetAdjustmentValue(BillAdjust.TransChargesItems, BillAdjust.TransChargesItemComps, System.Convert.ToDecimal(txtAmountAdjusted.Value));

            // apply rumus yang sama ke item yang sama
            //var ccs = CostCalculations.Where(x => x.ItemID == cc.ItemID);
            //foreach (var cc2 in ccs) {
            //    cc2.SetAdjustmentValue(TransChargesItems, TransChargesItemComps, System.Convert.ToDecimal(txtAmountAdjusted.Value));
            //}
            
            Refresh();
        }

        protected void txtDisc_TextChanged(object sender, EventArgs e)
        {
            var txtDisc = (RadNumericTextBox)sender;
            string TransactionNo = (txtDisc.Parent.Parent as GridDataItem).GetDataKeyValue("TransactionNo").ToString();
            string SequenceNo = (txtDisc.Parent.Parent as GridDataItem).GetDataKeyValue("SequenceNo").ToString();
            var ccEdited = BillAdjust.CostCalculations.Where(x => x.TransactionNo == TransactionNo && x.SequenceNo == SequenceNo).First();
            var rbSelection = (txtDisc.Parent.Parent as GridDataItem).FindControl("rblDisc") as RadioButtonList;
            var discSelection = System.Convert.ToInt32(rbSelection.SelectedValue);

            AdjustedDisc disc = new AdjustedDisc() { AdjustedDiscSelection = discSelection, AdjustedDiscAmount = System.Convert.ToDecimal(txtDisc.Value ?? 0) };

            var plafon = BillAdjust.Plavon;

            var cc = BillAdjust.CostCalculations.Where(x => x.TransactionNo == TransactionNo && x.SequenceNo == SequenceNo).First();
            // apply rumus yang sama ke item yang sama
            var ccSelected = BillAdjust.CostCalculations.Where(x => x.ItemID == ccEdited.ItemID);

            if (disc.AdjustedDiscSelection == 0/*tarif*/)
            {
                //cc.AdjustedDisc = disc;
                cc.SetAdjustmentPercent(BillAdjust.TransChargesItems, BillAdjust.TransChargesItemComps, disc);
                //cc.SetAdjustmentValue(TransChargesItems, TransChargesItemComps, cc.AmountTransaction - (cc.AmountTransaction * (disc.AdjustedDiscAmount ?? 0) / 100));
                //foreach (var cc in ccSelected)
                //{
                //    cc.AdjustedDisc = disc;
                //    cc.SetAdjustmentValue(TransChargesItems, TransChargesItemComps, cc.AmountTransaction - (cc.AmountTransaction * (disc.AdjustedDiscAmount ?? 0) / 100));
                //}
            }
            else if (disc.AdjustedDiscSelection== 1/*plafon*/)
            {
                var plafonDiscAmount = plafon * (disc.AdjustedDiscAmount ?? 0) / 100;
                var ccSum = ccSelected.Sum(x => x.AmountTransaction);

                foreach (var cc1 in ccSelected)
                {
                    cc1.SetAdjustmentDisc(disc);
                    cc1.SetAdjustmentValue(BillAdjust.TransChargesItems, BillAdjust.TransChargesItemComps, plafonDiscAmount / ccSum * cc1.AmountTransaction);
                }
            }

            Refresh();
        }

        protected void ToggleSelectedStateItemType(object sender, EventArgs e)
        {
            var selected = ((CheckBox)sender).Checked;

            foreach (GridDataItem dataItem in radgrdItemType.MasterTableView.Items)
            {
                ((CheckBox)dataItem.FindControl("chkItemType")).Checked = selected;
            }
        }

        protected void radgrdItemType_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            //radgrdItemType.DataSource = ItemTypes;
        }

        protected void ToggleSelectedStateServiceUnit(object sender, EventArgs e)
        {
            var selected = ((CheckBox)sender).Checked;

            foreach (GridDataItem dataItem in radgrdServiceUnit.MasterTableView.Items)
            {
                ((CheckBox)dataItem.FindControl("chkServiceUnit")).Checked = selected;
            }
        }

        protected void radgrdServiceUnit_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            //radgrdServiceUnit.DataSource = ServiceUnits;
        }

        protected void radgrdItemGroup_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            radgrdItemGroup.DataSource = BillAdjust.ItemGroups;
        }
        protected void ToggleSelectedStateItemGroup(object sender, EventArgs e)
        {
            var selected = ((CheckBox)sender).Checked;

            foreach (GridDataItem dataItem in radgrdItemGroup.MasterTableView.Items)
            {
                if (((RadNumericTextBox)dataItem.FindControl("txtDisc")).Value.HasValue)
                {
                    ((CheckBox)dataItem.FindControl("chkAutoAdjustItemGroup")).Checked = false;
                }
                else {
                    ((CheckBox)dataItem.FindControl("chkAutoAdjustItemGroup")).Checked = selected;
                }
            }
        }

        protected void grdFeePreview_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var fees = BillAdjust.RecalculateFeeByAdjustment(AppSession.UserLogin.UserID);

            fees.LoadFieldRef();

            var dtb = new System.Data.DataTable();
            dtb.Columns.Add("TransactionNo", typeof(string));
            dtb.Columns.Add("SequenceNo", typeof(string));
            dtb.Columns.Add("TariffComponentID", typeof(string));
            dtb.Columns.Add("ItemID", typeof(string));
            dtb.Columns.Add("ItemName", typeof(string));
            dtb.Columns.Add("TariffComponentName", typeof(string));
            dtb.Columns.Add("FeeAmount", typeof(decimal));

            foreach (var fee in fees) {
                var r = dtb.NewRow();
                r["TransactionNo"] = fee.TransactionNo;
                r["SequenceNo"] = fee.SequenceNo;
                r["TariffComponentID"] = fee.TariffComponentID;
                r["FeeAmount"] = fee.FeeAmount;

                r["ItemID"] = fee.ItemID;
                r["ItemName"] = fee.ItemName;
                r["TariffComponentName"] = fee.TariffComponentName;

                dtb.Rows.Add(r);
            }

            grdFeePreview.DataSource = dtb;
        }
    }
}
