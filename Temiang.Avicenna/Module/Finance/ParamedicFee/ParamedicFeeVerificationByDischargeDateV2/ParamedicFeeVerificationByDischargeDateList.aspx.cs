using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.BusinessObject.Reference;
using System.Globalization;
using Telerik.Web.UI.ButtonRendering;

namespace Temiang.Avicenna.Module.Finance.ParamedicFee.V2
{
    public partial class ParamedicFeeVerificationByDischargeDateList : BasePage
    {
        private AppAutoNumberLast _autoNumber;

        private List<string> keys = new List<string>();

        public bool IsUnapprovable()
        {
            UserAccess access = this.UserAccess;
            if (access.IsExist)
            {
                return access.IsUnApprovalAble;
            }
            return false;
        }

        public static bool IsFeeCalculatedOnTransaction()
        {
            return AppSession.Parameter.IsFeeCalculatedOnTransaction;
        }

        private TariffComponentCollection TariffComponentCollections
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = Session["collTariffComponentCollection"];
                    if (obj != null)
                        return ((TariffComponentCollection)(obj));
                }

                var coll = new TariffComponentCollection();
                coll.LoadAll();

                Session["collTariffComponentCollection"] = coll;
                return coll;
            }
            set { Session["collTariffComponentCollection"] = value; }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ParamedicFeeVerification;

            if (!IsPostBack)
            {
                // 
                var cColl = new TariffComponentCollection();
                cColl.Query.Where(cColl.Query.IsTariffParamedic == true);
                cColl.LoadAll();
                foreach (var c in cColl)
                {
                    var li = new ListItem(c.TariffComponentName, c.TariffComponentID);
                    li.Selected = c.IsFeeVerificationDefaultSelected ?? true;
                    cblComp.Items.Add(li);
                }
                //
                var phyty = new AppStandardReferenceItemCollection();
                //phyty.LoadByStandardReferenceID("ParamedicType");
                phyty.Query.Where(phyty.Query.IsActive == true,
                    phyty.Query.StandardReferenceID == AppEnum.StandardReference.ParamedicType.ToString());
                phyty.LoadAll();
                foreach (var c in phyty)
                {
                    var lis = new ListItem(c.ItemName, c.ItemID);
                    lis.Selected = true; // c.IsUsedBySystem ?? true;
                    cblPhy.Items.Add(lis);
                }
                var parColl = new ParamedicCollection();
                parColl.Query.Where
                    (
                        parColl.Query.IsActive == true
                    );
                parColl.Query.OrderBy(parColl.Query.ParamedicName.Ascending);
                parColl.LoadAll();

                cboParamedicID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (Paramedic entity in parColl)
                {
                    cboParamedicID.Items.Add(new RadComboBoxItem(entity.ParamedicName, entity.ParamedicID));
                }


                var suColl = new ServiceUnitCollection();
                suColl.Query.Where
                    (
                        suColl.Query.IsActive == true,
                        suColl.Query.SRRegistrationType.In(
                        AppConstant.RegistrationType.ClusterPatient,
                        AppConstant.RegistrationType.EmergencyPatient,
                        AppConstant.RegistrationType.InPatient,
                        AppConstant.RegistrationType.OutPatient,
                        AppConstant.RegistrationType.MedicalCheckUp
                        )

                    );
                suColl.Query.OrderBy(suColl.Query.ServiceUnitName.Ascending);
                suColl.LoadAll();

                cboServiceUnit.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (ServiceUnit entity in suColl)
                {
                    cboServiceUnit.Items.Add(new RadComboBoxItem(entity.ServiceUnitName, entity.ServiceUnitID));
                }

                // guarantor type
                StandardReference.InitializeIncludeSpace(cboGuarantorType, AppEnum.StandardReference.GuarantorType);

                if (AppSession.Parameter.IsParamedicFeeVerifPaymentFilterByClosingBilling)
                {
                    lblPaymentReceive.Text = "Payment Date (Closing Billing)";
                }
                else
                {
                    lblPaymentReceive.Text = "Payment Date (Payment Receive)";
                }
            }
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            SaveValueToCookie();

            grdList.Rebind();
            grdListV.Rebind();
        }

        private DataTable ParamedicFeeTransChargesItemCompByDischargeDateMergeBilling()
        {
            // tarif component
            bool isAllComponentSelected = true;
            List<string> tcs = new List<string>();
            foreach (ListItem li in cblComp.Items)
            {
                if (!li.Selected) isAllComponentSelected = false;
                else tcs.Add(li.Value);
            }
            // physician type
            bool isPhysiciantype = true;
            List<string> pts = new List<string>();
            foreach (ListItem lis in cblPhy.Items)
            {
                if (!lis.Selected) isPhysiciantype = false;
                else pts.Add(lis.Value);
            }

            return ParamedicFeeTransChargesItemCompByDischargeDateCollection.GetParamedicFee(txtDischargeDateFrom.SelectedDate, txtDischargeDateTo.SelectedDate,
                txtPaymentDateFrom.SelectedDate, txtPaymentDateTo.SelectedDate,
                txtRegistrationNo.Text, cboServiceUnit.SelectedValue, cboParamedicID.SelectedValue,
                txtNamaLayanan.Text, cblRegType.Items[0].Selected, cblRegType.Items[1].Selected,
                tcs, isAllComponentSelected, pts, isPhysiciantype,
                AppSession.Parameter.IsPhysicianFeeVerificationPaidOnly,
                AppSession.Parameter.IsPhysicianFeeShowProcedureNote,
                txtInvoiceNo.Text, txtPaymentNo.Text, cboGuarantorID.SelectedValue, cboGuarantorType.SelectedValue,
                txtInvoiceDateFrom.SelectedDate, txtInvoiceDateTo.SelectedDate, cboItemGroupID.SelectedValue
                );
        }

        protected void grdList_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var item = ((GridDataItem)e.Item);
                CheckBox chkCorrected = item["Corrected"].Controls[0] as CheckBox;
                if (chkCorrected.Checked)
                {
                    item.ForeColor = Color.LimeGreen;
                    item.Font.Bold = true;

                    var tcid = item.GetDataKeyValue("TariffComponentID").ToString();
                    var tc = TariffComponentCollections.Where(x => x.TariffComponentID == tcid).FirstOrDefault();
                    if (tc != null)
                    {
                        if (tc.IsAutoChecklistCorrectedFeeVerification ?? false)
                        {
                            var chkd = item.FindControl("detailChkbox") as CheckBox;
                            if (chkd != null)
                            {
                                chkd.Checked = true;
                            }
                        }
                    }
                }

                CheckBox chk = item["IsModified"].Controls[0] as CheckBox;
                if (chk.Checked)
                {
                    item.ForeColor = Color.Red;
                    item.Font.Bold = true;
                    //celltoVerify1.Font.Bold = true;
                    //celltoVerify1.BackColor = Color.Yellow;
                }

                // test dynamic formating for share
                //Check the formatting condition
                if (item["IsCalculatedInPercent"].Text == true.ToString())
                {
                    item["CalculatedAmount"].Text = item["CalculatedAmount"].Text + "%";
                }

                // if change note
                if (DataBinder.Eval(e.Item.DataItem, "ChangeNote").ToString().Length > 0)
                {
                    item["FeeAmountTemplate"].ToolTip = "Change Note: " + DataBinder.Eval(e.Item.DataItem, "ChangeNote").ToString();
                }

                //foreach (var xx in items) { 
                //    grdList.MasterTableView.Items.Cast<GridDataItem>().Where(dataItem => 
                //        dataItem.GetDataKeyValue("TransactionNo") == xx.TransactionNo && 
                //        dataItem.GetDataKeyValue("SequenceNo") == xx.SequenceNo && 
                //        dataItem.GetDataKeyValue("TariffComponentID") == xx.TariffComponentID)
                //}
                if (keys.Contains(string.Format("{0}{1}{2}",
                    item.GetDataKeyValue("TransactionNo"),
                    item.GetDataKeyValue("SequenceNo"),
                    item.GetDataKeyValue("TariffComponentID"))))
                {
                    var chkd = item.FindControl("detailChkbox") as CheckBox;
                    if (chkd != null)
                    {
                        chkd.Checked = true;
                    }
                }

                //if (System.Convert.ToInt32((item.DataItem as DataRowView)["SRPhysicianFeeCategory"].ToString()) >= 6) {
                //    HyperLink linkFeeAmount = new HyperLink();
                //    linkFeeAmount.Text = ((decimal)((item.DataItem as DataRowView)["FeeAmount"])).ToString("N2");
                //    linkFeeAmount.NavigateUrl = "";
                //    linkFeeAmount.Style.Add(HtmlTextWriterStyle.Cursor, "pointer");
                //    //linkFeeAmount.ToolTip = "Click here to view detail info";
                //    linkFeeAmount.Attributes.Add("onclick",
                //        string.Format("javascript:OpenFeeInfo('{0}','{1}','{2}'); return false;",
                //            item.GetDataKeyValue("TransactionNo"),
                //            item.GetDataKeyValue("SequenceNo"),
                //            item.GetDataKeyValue("TariffComponentID")
                //        )
                //    );

                //    var oo = item["FeeAmount"];
                //    oo.Controls.Clear();
                //    oo.Controls.Add(linkFeeAmount);
                //}   
            }
        }

        public static void SetGridColumnVisible(GridTableView grdList)
        {
            if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSJKT")
            {
                // LOS
                //grdList.Columns[7].Visible = false;
                grdList.GetColumn("refToRegistration_LOS").Visible = false;
                // execute date
                //grdList.Columns[11].Visible = false;
                grdList.GetColumn("ExecutionDate").Visible = false;
                // execute date / payment date
                //grdList.Columns[12].Visible = true;
                grdList.GetColumn("ExecuteDatePaymentDate").Visible = true;
                // paramedic
                //grdList.Columns[14].Visible = true;
                grdList.GetColumn("ParamedicNameMix").Visible = true;
                //grdList.Columns[15].Visible = false;
                grdList.GetColumn("ParamedicName").Visible = false;
                // Price Jasa Medis
                //grdList.Columns[27].Visible = true;
                grdList.GetColumn("Price").Visible = true;
                // paid
                //grdList.Columns[16].Visible = true;
                grdList.GetColumn("IsPaidOff").Visible = true;
                // guarantor verified
                //grdList.Columns[17].Visible = false;
                grdList.GetColumn("IsGuarantorVerified").Visible = false;
                // sum deduction
                //grdList.Columns[31].Visible = false;
                grdList.GetColumn("SumDeductionAmount").Visible = false;
            }
            else
            {
                // LOS
                //grdList.Columns[7].Visible = false;
                grdList.GetColumn("refToRegistration_LOS").Visible = false;
                // execute date
                //grdList.Columns[11].Visible = false;
                grdList.GetColumn("ExecutionDate").Visible = false;
                // execute date / payment date
                //grdList.Columns[12].Visible = true;
                grdList.GetColumn("ExecuteDatePaymentDate").Visible = true;
                // paramedic
                //grdList.Columns[14].Visible = true;
                grdList.GetColumn("ParamedicNameMix").Visible = true;
                //grdList.Columns[15].Visible = false;
                grdList.GetColumn("ParamedicName").Visible = false;
                // Price Jasa Medis
                //grdList.Columns[27].Visible = false;
                grdList.GetColumn("Price").Visible = false;
                // paid
                //grdList.Columns[16].Visible = true;
                grdList.GetColumn("IsPaidOff").Visible = true;
                // guarantor verified
                //grdList.Columns[17].Visible = false;
                grdList.GetColumn("IsGuarantorVerified").Visible = false;
                // sum deduction
                //grdList.Columns[31].Visible = false;
                grdList.GetColumn("SumDeductionAmount").Visible = false;
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if ((txtDischargeDateFrom.SelectedDate.HasValue && txtDischargeDateTo.SelectedDate.HasValue) ||
                (txtPaymentDateFrom.SelectedDate.HasValue && txtPaymentDateTo.SelectedDate.HasValue) || 
                (txtInvoiceDateFrom.SelectedDate.HasValue && txtInvoiceDateTo.SelectedDate.HasValue))
            {
                //
            }
            else if (string.IsNullOrEmpty(txtRegistrationNo.Text.Trim()) &&
              string.IsNullOrEmpty(txtInvoiceNo.Text.Trim()) && string.IsNullOrEmpty(txtPaymentNo.Text.Trim()))
            {
                return;
            }

            grdList.DataSource = ParamedicFeeTransChargesItemCompByDischargeDateMergeBilling();

            SetGridColumnVisible(grdList.MasterTableView);
        }

        protected void grdListV_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdListV.DataSource = ParamedicFeeVerifications;
        }

        private DataTable ParamedicFeeVerifications
        {
            get
            {
                var query = new ParamedicFeeVerificationQuery("a");
                var parQ = new ParamedicQuery("b");


                query.InnerJoin(parQ).On(query.ParamedicID == parQ.ParamedicID);

                query.Select(
                       "<'../ParamedicFeeVerificationByDischargeDateV2/ParamedicFeeVerificationByDischargeDateDetail.aspx?md=view&id=' + a.VerificationNo + '&type=4' as VerUrl>",
                       query.VerificationNo,
                       query.VerificationDate,
                       query.ApprovedDate,
                       query.PlanningPaymentDate,
                       parQ.ParamedicName,
                       parQ.Bank,
                       parQ.BankAccount,
                       query.VerificationAmount,
                       query.TaxAmount,
                       query.IsApproved,
                       query.SumDeductionAmountAfterTax,
                       "<LEN(a.VerificationNo) pjng>",
                       "<LEFT(a.VerificationNo, 10) Order1>",
                       "<CAST(SUBSTRING(a.VerificationNo, 12, 10) AS INT) Order2>"
                   );
                //query.Where(query.Or(query.RegistrationNo == txtRegistrationNo.Text,
                //                     query.PaymentNo == txtPaymentNo.Text));

                //if (!string.IsNullOrEmpty(txtRegistrationNo.Text))
                //{
                //    query.Where(query.RegistrationNo == txtRegistrationNo.Text);
                //}
                //if (!string.IsNullOrEmpty(txtPaymentNo.Text))
                //{
                //    query.Where(query.PaymentNo == txtPaymentNo.Text);
                //}
                if (!string.IsNullOrEmpty(cboParamedicID.SelectedValue))
                {
                    query.Where(query.ParamedicID == cboParamedicID.SelectedValue);
                }
                //if (!string.IsNullOrEmpty(txtRegistrationNo.Text)) {
                //    var pmbydc = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("c");
                //    query.InnerJoin(pmbydc).On(query.VerificationNo == pmbydc.VerificationNo);
                //    query.Where(pmbydc.RegistrationNo == txtRegistrationNo.Text);

                //    query.es.Distinct = true;
                //}

                string PaymentNoPM = "";
                string PaymentNoPAR = "";
                if (txtPaymentNo.Text.Contains("PM"))
                {
                    PaymentNoPM = txtPaymentNo.Text;
                }
                if (txtPaymentNo.Text.Contains("PAR"))
                {
                    PaymentNoPAR = txtPaymentNo.Text;
                }

                bool isAllComponentSelected = true;
                List<string> tcs = new List<string>();
                foreach (ListItem li in cblComp.Items)
                {
                    if (!li.Selected) isAllComponentSelected = false;
                    else tcs.Add(li.Value);
                }
                // physician type
                bool isPhysiciantype = true;
                List<string> pts = new List<string>();
                foreach (ListItem lis in cblPhy.Items)
                {
                    if (!lis.Selected) isPhysiciantype = false;
                    else pts.Add(lis.Value);
                }
                if (!isAllComponentSelected || !isPhysiciantype || !string.IsNullOrEmpty(txtRegistrationNo.Text) ||
                    !string.IsNullOrEmpty(txtInvoiceNo.Text) ||
                    !string.IsNullOrEmpty(PaymentNoPAR) ||
                    !string.IsNullOrEmpty(cboGuarantorID.SelectedValue) ||
                    (!txtInvoiceDateFrom.IsEmpty && !txtInvoiceDateTo.IsEmpty))
                {
                    var pmbydc = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("c");
                    query.InnerJoin(pmbydc).On(query.VerificationNo == pmbydc.VerificationNo);
                    if (!string.IsNullOrEmpty(txtRegistrationNo.Text) || !string.IsNullOrEmpty(cboGuarantorID.SelectedValue) ||
                        !string.IsNullOrEmpty(cboGuarantorType.SelectedValue))
                    {
                        var regQ = new RegistrationQuery("reg");
                        var patQ = new PatientQuery("pat");
                        query.LeftJoin(regQ).On(pmbydc.RegistrationNo == regQ.RegistrationNo);
                        query.InnerJoin(patQ).On(regQ.PatientID == patQ.PatientID);
                        if (!string.IsNullOrEmpty(txtRegistrationNo.Text))
                        {
                            if (txtRegistrationNo.Text.Contains("REG"))
                            {
                                query.Where(
                                    pmbydc.Or(
                                        pmbydc.RegistrationNo == txtRegistrationNo.Text,
                                        pmbydc.RegistrationNoMergeTo == txtRegistrationNo.Text));
                            }
                            else {
                                query.Where(patQ.MedicalNo == txtRegistrationNo.Text);
                            }
                            
                        }
                        if (!string.IsNullOrEmpty(cboGuarantorID.SelectedValue))
                        {
                            query.Where(regQ.GuarantorID == cboGuarantorID.SelectedValue);
                        }
                        if (!string.IsNullOrEmpty(cboGuarantorType.SelectedValue))
                        {
                            var guarQ = new GuarantorQuery("guar");
                            query.InnerJoin(guarQ).On(regQ.GuarantorID == guarQ.GuarantorID);
                            query.Where(guarQ.SRGuarantorType == cboGuarantorType.SelectedValue);
                        }
                    }
                    if (!isAllComponentSelected)
                    {
                        query.Where(pmbydc.TariffComponentID.In(tcs));
                    }
                    if (!isPhysiciantype)
                    {
                        var psr = new AppStandardReferenceItemQuery("spr");
                        //parQ.InnerJoin(psr).On(parQ.SRParamedicType == psr.ItemID &&
                        //    psr.StandardReferenceID == AppEnum.StandardReference.ParamedicType);
                        query.InnerJoin(psr).On(parQ.SRParamedicType == psr.ItemID &&
                            psr.StandardReferenceID == AppEnum.StandardReference.ParamedicType);
                        //query.LeftJoin(psr).On(psr.StandardReferenceID == AppEnum.StandardReference.ParamedicType.ToString() &&
                        //    psr.ItemID == parQ.SRParamedicType);
                        query.Where(psr.ItemID.In(pts));
                    }
                    if (!string.IsNullOrEmpty(txtInvoiceNo.Text) || !string.IsNullOrEmpty(PaymentNoPAR) || (!txtInvoiceDateFrom.IsEmpty && !txtInvoiceDateTo.IsEmpty))
                    {
                        var iv = new InvoicesQuery("iv");
                        var ivi = new InvoicesItemQuery("ivi");
                        query.InnerJoin(ivi).On(pmbydc.RegistrationNoMergeTo == ivi.RegistrationNo)
                            .InnerJoin(iv).On(ivi.InvoiceNo == iv.InvoiceNo && iv.IsApproved == true &&
                            iv.IsVoid == false);
                        if (!string.IsNullOrEmpty(txtInvoiceNo.Text))
                        {
                            query.Where(iv.InvoiceNo == txtInvoiceNo.Text);
                        }
                        if (!txtInvoiceDateFrom.IsEmpty && !txtInvoiceDateTo.IsEmpty)
                        {
                            query.Where(iv.InvoiceDate.Between(txtInvoiceDateFrom.SelectedDate, txtInvoiceDateTo.SelectedDate));
                        }
                        if (!string.IsNullOrEmpty(PaymentNoPAR))
                        {
                            var ivp = new InvoicesQuery("ivp");
                            var ivip = new InvoicesItemQuery("ivip");
                            query.InnerJoin(ivip).On(ivip.InvoiceReferenceNo == ivi.InvoiceNo && ivip.PaymentNo == ivi.PaymentNo)
                                .InnerJoin(ivp).On(ivip.InvoiceNo == ivp.InvoiceNo && ivp.IsVoid == false && ivp.IsApproved == true)
                                .Where(ivp.InvoiceNo == PaymentNoPAR);
                        }

                    }
                    if (!string.IsNullOrEmpty(PaymentNoPM))
                    {
                        query.Where(pmbydc.PaymentNoCash == PaymentNoPM);
                    }
                    //if (!string.IsNullOrEmpty(cboGuarantorID.SelectedValue)) {
                    //    var regQ = new RegistrationQuery("reg");
                    //    query.InnerJoin(regQ).On(pmbydc.RegistrationNo == regQ.RegistrationNo);
                    //    query.Where(regQ.GuarantorID == cboGuarantorID.SelectedValue);
                    //}

                    query.es.Distinct = true;
                }

                if (chkUnapprovedOnly.Checked)
                {
                    query.Where(query.IsApproved == false);
                }
                if (!string.IsNullOrEmpty(txtVerificationNo.Text.Trim()))
                {
                    string searchTextContain = string.Format("%{0}%", txtVerificationNo.Text.Trim());
                    query.Where(query.VerificationNo.Like(searchTextContain));
                }

                if (!string.IsNullOrEmpty(cboParamedicID.SelectedValue))
                {
                    query.Where(query.ParamedicID == cboParamedicID.SelectedValue);
                }

                if (!string.IsNullOrEmpty(cboPhysicianUnapproved.SelectedValue))
                {
                    query.Where(query.ParamedicID == cboPhysicianUnapproved.SelectedValue);
                }

                query.OrderBy("<LEFT(a.VerificationNo, 10) DESC>", "<CAST(SUBSTRING(a.VerificationNo, 12,10) AS INT) DESC>");
                query.es.Top = 500;

                DataTable dtb = query.LoadDataTable();

                return dtb;
            }
        }

        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            foreach (CheckBox chkBox in grdList.MasterTableView.Items.Cast<GridDataItem>().Select(dataItem => (CheckBox)dataItem.FindControl("detailChkbox")).Where(chkBox => chkBox.Visible))
            {
                chkBox.Checked = ((CheckBox)sender).Checked;
            }
        }

        protected void ToggleSelectedStateChk(object sender, EventArgs e)
        {
            foreach (CheckBox chkBox in grdListV.MasterTableView.Items.Cast<GridDataItem>().Select(dataItem => (CheckBox)dataItem.FindControl("Chk")).Where(chkBox => chkBox.Visible))
            {
                chkBox.Checked = ((CheckBox)sender).Checked;
            }
        }

        protected void ToggleSelectedPaidState(object sender, EventArgs e)
        {
            foreach (GridDataItem i in grdList.MasterTableView.Items)
            {
                var chk = (CheckBox)i.FindControl("detailChkbox");
                var chkPaid = (CheckBox)i.FindControl("paidChkbox");

                chk.Checked = chkPaid.Checked && ((CheckBox)sender).Checked;
            }
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            pnlInfo.Visible = false;

            if (string.IsNullOrEmpty(eventArgument))
                return;

            if (!(sourceControl is RadGrid))
                return;

            if (eventArgument == "generate")
            {
                pnlInfo.Visible = false;
                //bool isValid = true;
                //if (string.IsNullOrEmpty(txtRegistrationNo.Text) && string.IsNullOrEmpty(txtPaymentNo.Text))
                //{
                //    pnlInfo.Visible = true;
                //    lblInfo.Text = "Registration or Payment No required.";
                //    isValid = false;
                //}

                //if (!isValid) return;

                var transNos = string.Empty;

                /*(0:calculated fee, 1:tariff component fee)*/
                string BaseCalculateTax = AppParameter.GetParameterValue(AppParameter.ParameterItem.pphFeeBase);

                using (var trans = new esTransactionScope())
                {
                    var items = grdList.MasterTableView.Items.Cast<GridDataItem>().Where(dataItem => ((CheckBox)dataItem.FindControl("detailChkbox")).Checked)
                                                                                  .Select(dataItem => new
                                                                                  {
                                                                                      ParamedicID = dataItem["ParamedicID"].Text,
                                                                                      //DischargeDate = Convert.ToDateTime(dataItem["DischargeDate"].Text),
                                                                                      DischargeDate = DateTime.ParseExact(dataItem["DischargeDate"].Text, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                                                                                      TransactionNo = dataItem["TransactionNo"].Text.Replace("&nbsp;", string.Empty),
                                                                                      SequenceNo = dataItem["SequenceNo"].Text.Replace("&nbsp;", string.Empty),
                                                                                      TariffComponentID = dataItem["TariffComponentID"].Text.Replace("&nbsp;", string.Empty),
                                                                                      IsPhysicianMember = (dataItem["IsPhysicianMember"].Controls[0] as CheckBox).Checked
                                                                                  });

                    var tcColl = new TariffComponentCollection();
                    tcColl.LoadAll();

                    int iVerified = 0;

                    foreach (var group in (from g in items
                                           group g by new
                                           {
                                               g.ParamedicID
                                           }
                                               into grp
                                           orderby grp.Key.ParamedicID
                                           select new
                                           {
                                               ParamedicID = grp.Key.ParamedicID
                                           }))
                    {
                        _autoNumber = Helper.GetNewAutoNumber(DateTime.Now.Date, AppEnum.AutoNumber.FeeVerificationNo);
                        decimal verificationAmt = 0;
                        decimal verificationTaxAmt = 0;

                        var entity = new ParamedicFeeVerification();

                        #region header

                        entity.VerificationNo = _autoNumber.LastCompleteNumber;
                        entity.VerificationDate = DateTime.Now.Date;
                        entity.ParamedicID = group.ParamedicID;

                        entity.TaxAmount = 0;
                        entity.IsVoid = false;
                        entity.IsApproved = false;
                        entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        entity.LastUpdateDateTime = DateTime.Now;
                        //entity.RegistrationNo = txtRegistrationNo.Text;
                        //entity.PaymentNo = txtPaymentNo.Text;

                        #endregion

                        DateTime startDate = DateTime.Now.Date;
                        DateTime endDate = DateTime.Now.Date.AddDays(-100);

                        int iAborted = 0;
                        foreach (var i in items.Where(i => i.ParamedicID == group.ParamedicID))
                        {
                            #region detail

                            if (i.DischargeDate < startDate)
                                startDate = i.DischargeDate;
                            if (i.DischargeDate > endDate)
                                endDate = i.DischargeDate;

                            if (!i.IsPhysicianMember)
                            {
                                var c = new ParamedicFeeTransChargesItemCompByDischargeDate();
                                c.Query.Where(c.Query.TransactionNo.Equal(i.TransactionNo),
                                    c.Query.SequenceNo.Equal(i.SequenceNo),
                                    c.Query.TariffComponentID.Equal(i.TariffComponentID));
                                //if (c.LoadByPrimaryKey(i.SequenceNo, i.TariffComponentID, i.TransactionNo))
                                if (c.Load(c.Query))
                                {
                                    if (!string.IsNullOrEmpty(c.VerificationNo))
                                    {
                                        // data ini sudah pernah diverifikasi, sudah ada nomor verif lain,
                                        // abort!!!
                                        var rows = grdList.MasterTableView.Items.Cast<GridDataItem>()
                                            .Where(x => x.GetDataKeyValue("TransactionNo").Equals(i.TransactionNo) &&
                                                x.GetDataKeyValue("SequenceNo").Equals(i.SequenceNo) &&
                                                x.GetDataKeyValue("TariffComponentID").Equals(i.TariffComponentID));
                                        foreach (var row in rows)
                                        {
                                            //row.ForeColor = Color.Red;
                                            //row.Font.Bold = true;
                                            row.Font.Bold = true;
                                            row.BackColor = Color.Yellow;
                                        }
                                        iAborted += 1;
                                    }
                                    c.VerificationNo = entity.VerificationNo;
                                    c.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                    c.LastUpdateDateTime = DateTime.Now;

                                    c.Save();

                                    // update verification no on deductions
                                    var decs = new ParamedicFeeDeductionsCollection();
                                    decs.Query.Where(decs.Query.TransactionNo.Equal(i.TransactionNo),
                                        decs.Query.SequenceNo.Equal(i.SequenceNo),
                                        decs.Query.TariffComponentID.Equal(i.TariffComponentID));
                                    if (decs.LoadAll())
                                    {
                                        foreach (var dec in decs)
                                        {
                                            dec.VerificationNo = entity.VerificationNo;
                                        }
                                    }
                                    decs.Save();

                                    verificationAmt += (c.FeeAmount ?? 0) - (c.SumDeductionAmount ?? 0);

                                    var SRPphType = "01"; /*default kena pph21*/
                                    if (!string.IsNullOrEmpty(i.TariffComponentID))
                                    {
                                        SRPphType = tcColl.Where(x => x.TariffComponentID == i.TariffComponentID).Select(x => x.SRPphType).FirstOrDefault();
                                    }
                                    if (!string.IsNullOrEmpty(SRPphType))
                                        verificationTaxAmt += ((BaseCalculateTax == "1" && (
                                            c.SRPhysicianFeeCategory == "01" ||
                                            System.Convert.ToInt32(string.IsNullOrEmpty(c.SRPhysicianFeeCategory) ? "0" : c.SRPhysicianFeeCategory) >= 4
                                            )) ? (c.Price ?? 0) : (c.FeeAmount ?? 0)); // pajak dihitung sebelum potongan bro!! -(c.SumDeductionAmount ?? 0);

                                    iVerified++;
                                }
                            }
                            else {
                                // fee by team
                                var c = new ParamedicFeeTransChargesItemCompByTeam();
                                c.Query.Where(c.Query.TransactionNo.Equal(i.TransactionNo),
                                    c.Query.SequenceNo.Equal(i.SequenceNo),
                                    c.Query.TariffComponentID.Equal(i.TariffComponentID),
                                    c.Query.ParamedicID.Equal(i.ParamedicID));
                                if (c.Load(c.Query))
                                {
                                    if (!string.IsNullOrEmpty(c.VerificationNo))
                                    {
                                        // data ini sudah pernah diverifikasi, sudah ada nomor verif lain,
                                        // abort!!!
                                        var rows = grdList.MasterTableView.Items.Cast<GridDataItem>()
                                            .Where(x => x.GetDataKeyValue("TransactionNo").Equals(i.TransactionNo) &&
                                                x.GetDataKeyValue("SequenceNo").Equals(i.SequenceNo) &&
                                                x.GetDataKeyValue("TariffComponentID").Equals(i.TariffComponentID) &&
                                                x.GetDataKeyValue("ParamedicID").Equals(i.ParamedicID));
                                        foreach (var row in rows)
                                        {
                                            //row.ForeColor = Color.Red;
                                            //row.Font.Bold = true;
                                            row.Font.Bold = true;
                                            row.BackColor = Color.Yellow;
                                        }
                                        iAborted += 1;
                                    }
                                    c.VerificationNo = entity.VerificationNo;
                                    c.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                    c.LastUpdateDateTime = DateTime.Now;

                                    c.Save();

                                    //// update verification no on deductions
                                    //var decs = new ParamedicFeeDeductionsCollection();
                                    //decs.Query.Where(decs.Query.TransactionNo.Equal(i.TransactionNo),
                                    //    decs.Query.SequenceNo.Equal(i.SequenceNo),
                                    //    decs.Query.TariffComponentID.Equal(i.TariffComponentID));
                                    //if (decs.LoadAll())
                                    //{
                                    //    foreach (var dec in decs)
                                    //    {
                                    //        dec.VerificationNo = entity.VerificationNo;
                                    //    }
                                    //}
                                    //decs.Save();

                                    verificationAmt += (c.FeeAmount ?? 0);// - (c.SumDeductionAmount ?? 0);

                                    var SRPphType = "01"; /*default kena pph21*/
                                    if (!string.IsNullOrEmpty(i.TariffComponentID))
                                    {
                                        SRPphType = tcColl.Where(x => x.TariffComponentID == i.TariffComponentID).Select(x => x.SRPphType).FirstOrDefault();
                                    }
                                    if (!string.IsNullOrEmpty(SRPphType))
                                        verificationTaxAmt += ((BaseCalculateTax == "1") ? (c.Price ?? 0) : (c.FeeAmount ?? 0)); // pajak dihitung sebelum potongan bro!! -(c.SumDeductionAmount ?? 0);

                                    iVerified++;
                                }
                            }

                            var addDec = new ParamedicFeeAddDeduc();
                            if (addDec.LoadByPrimaryKey(i.TransactionNo))
                            {
                                if (!string.IsNullOrEmpty(addDec.VerificationNo))
                                {
                                    // data ini sudah pernah diverifikasi, sudah ada nomor verif lain,
                                    // abort!!!
                                    var rows = grdList.MasterTableView.Items.Cast<GridDataItem>()
                                        .Where(x => x.GetDataKeyValue("TransactionNo").Equals(i.TransactionNo) &&
                                            x.GetDataKeyValue("SequenceNo").Equals(i.SequenceNo) &&
                                            x.GetDataKeyValue("TariffComponentID").Equals(i.TariffComponentID));
                                    foreach (var row in rows)
                                    {
                                        //row.ForeColor = Color.Red;
                                        //row.Font.Bold = true;
                                        row.Font.Bold = true;
                                        row.BackColor = Color.Yellow;
                                    }
                                    iAborted += 1;
                                }
                                addDec.VerificationNo = entity.VerificationNo;
                                addDec.LastUpdatedByUserID = AppSession.UserLogin.UserID;
                                addDec.LastUpdateDateTime = DateTime.Now;

                                addDec.Save();

                                verificationAmt += (addDec.Amount ?? 0) * (addDec.SRParamedicFeeAdjustType.Equals("02") ? -1 : 1);

                                var SRPphType = "01"; /*default kena pph21*/
                                if (!string.IsNullOrEmpty(i.TariffComponentID))
                                {
                                    SRPphType = tcColl.Where(x => x.TariffComponentID == i.TariffComponentID).Select(x => x.SRPphType).FirstOrDefault();
                                }
                                if (!string.IsNullOrEmpty(SRPphType))
                                    verificationTaxAmt += (addDec.Amount ?? 0) * (addDec.SRParamedicFeeAdjustType.Equals("02") ? -1 : 1);

                                iVerified++;
                            }
                            #endregion
                        }

                        if (iAborted > 0)
                        {
                            pnlInfo.Visible = true;
                            lblInfo.Text = "Some data have been verified, verification process failed!";
                            return;
                        }

                        entity.StartDate = startDate;
                        entity.EndDate = endDate;
                        entity.TaxPeriod = Convert.ToInt16(endDate.Year.ToString());
                        entity.VerificationAmount = verificationAmt;
                        entity.VerificationTaxAmount = verificationTaxAmt;

                        _autoNumber.Save();
                        entity.Save();

                        transNos = string.IsNullOrEmpty(transNos) ? entity.VerificationNo : transNos + ", " + entity.VerificationNo;
                    }
                    if (iVerified == 0)
                    {
                        pnlInfo.Visible = true;
                        lblInfo.Text = "No data selected to be verified.";
                        return;
                    }


                    trans.Complete();
                }

                pnlInfo.Visible = true;
                lblInfo.Text = "Fee Verification Succeed with No. : " + transNos;

                grdList.Rebind();
                grdListV.Rebind();
            }
            else if (eventArgument.Contains("approved"))
            {
                var param = eventArgument.Split('|');
                switch (param[0])
                {
                    case "approved":
                        {
                            if (param[1].Equals("page"))
                            {
                                // looping grid
                                foreach (GridDataItem r in grdListV.MasterTableView.Items)
                                {
                                    CheckBox chk = (r["IsApproved"].Controls[0] as CheckBox);
                                    if (!chk.Checked)
                                    {
                                        HyperLink ln = (r["VerificationNo"].Controls[0] as HyperLink);
                                        string verNo = ln.Text;
                                        var msg = (new ParamedicFeeVerification()).Approv(verNo, AppSession.UserLogin.UserID);
                                        if (!string.IsNullOrEmpty(msg))
                                        {
                                            pnlInfo.Visible = true;
                                            lblInfo.Text = msg;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                var msg = (new ParamedicFeeVerification()).Approv(param[1], AppSession.UserLogin.UserID);
                                if (!string.IsNullOrEmpty(msg))
                                {
                                    pnlInfo.Visible = true;
                                    lblInfo.Text = msg;
                                }
                            }
                            break;
                        }
                    case "unapproved":
                        {
                            var msg = (new ParamedicFeeVerification()).UnApprov(param[1], AppSession.UserLogin.UserID);
                            if (!string.IsNullOrEmpty(msg))
                            {
                                pnlInfo.Visible = true;
                                lblInfo.Text = msg;
                            }
                            break;
                        }
                }

                grdListV.Rebind();
            }
            else if (eventArgument.Contains("deleteFeeAll"))
            {
                var parts = eventArgument.Split('|');
                var reason = parts.Length > 1 ? parts[1] : string.Empty;

                if (string.IsNullOrWhiteSpace(reason))
                {
                    return;
                }

                var items = grdList.MasterTableView.Items.Cast<GridDataItem>()
                               .Where(dataItem => ((CheckBox)dataItem.FindControl("detailChkbox")).Checked)
                               .Select(dataItem => new
                               {
                                   TransactionNo = dataItem["TransactionNo"].Text.Replace("&nbsp;", string.Empty),
                                   SequenceNo = dataItem["SequenceNo"].Text.Replace("&nbsp;", string.Empty),
                                   TariffComponentID = dataItem["TariffComponentID"].Text.Replace("&nbsp;", string.Empty),
                                   ParamedicID = dataItem["ParamedicID"].Text.Replace("&nbsp;", string.Empty),
                                   IsPhysicianMember = (dataItem["IsPhysicianMember"].Controls[0] as CheckBox).Checked
                               });

                foreach (var item in items)
                {
                    if (item.IsPhysicianMember)
                    {
                        var fee = new ParamedicFeeTransChargesItemCompByTeam();
                        fee.Query.Where(fee.Query.TransactionNo == item.TransactionNo &&
                                        fee.Query.SequenceNo == item.SequenceNo &&
                                        fee.Query.TariffComponentID == item.TariffComponentID &&
                                        fee.Query.ParamedicID == item.ParamedicID);

                        if (fee.Load(fee.Query))
                        {
                            if (AppSession.Parameter.IsFeeCalculatedOnTransaction)
                            {
                                if (fee.VerificationNo == null)
                                {
                                    fee.IsWriteOff = true;
                                    fee.ChangeNote = reason;
                                    fee.Save();
                                }
                            }
                            else
                            {
                                if (fee.VerificationNo == null)
                                {
                                    fee.MarkAsDeleted();
                                    fee.Save();
                                }
                            }
                        }
                    }
                    else
                    {
                        var fee = new ParamedicFeeTransChargesItemCompByDischargeDate();
                        fee.Query.Where(fee.Query.TransactionNo == item.TransactionNo &&
                                        fee.Query.SequenceNo == item.SequenceNo &&
                                        fee.Query.TariffComponentID == item.TariffComponentID);

                        if (fee.Load(fee.Query))
                        {
                            if (AppSession.Parameter.IsFeeCalculatedOnTransaction)
                            {
                                if (fee.VerificationNo == null)
                                {
                                    fee.IsWriteOff = true;
                                    fee.ChangeNote = reason;
                                    fee.Save();
                                }
                            }
                            else
                            {
                                if (fee.VerificationNo == null)
                                {
                                    fee.MarkAsDeleted();
                                    fee.Save();
                                }
                            }
                        }
                    }
                }

                grdList.Rebind();
            }
            else if (eventArgument.Contains("deleteFee"))
            {
                var param = eventArgument.Split('|');
                {
                    var TransactionNo = param[1];
                    var SequenceNo = param[2];
                    var TariffComponentID = param[3];
                    var ParamedicID = param[4];
                    var IsPhysicianMember = param[5];

                    if (System.Convert.ToBoolean(IsPhysicianMember))
                    {
                        var fee = new ParamedicFeeTransChargesItemCompByTeam();
                        fee.Query.Where(fee.Query.TransactionNo == TransactionNo && 
                            fee.Query.SequenceNo == SequenceNo && fee.Query.TariffComponentID == TariffComponentID &&
                            fee.Query.ParamedicID == ParamedicID);
                        if (fee.Load(fee.Query))
                        {
                            if (AppSession.Parameter.IsFeeCalculatedOnTransaction)
                            {
                                // set write off 
                                if (fee.VerificationNo == null)
                                {
                                    fee.IsWriteOff = true;
                                    fee.ChangeNote = param[6];
                                    fee.Save();
                                    grdList.Rebind();
                                }
                            }
                            else
                            {
                                // delete for re-calculation
                                if (fee.VerificationNo == null)
                                {
                                    fee.MarkAsDeleted();
                                    fee.Save();
                                    grdList.Rebind();
                                }
                            }
                        }
                    }
                    else {
                        var fee = new ParamedicFeeTransChargesItemCompByDischargeDate();
                        fee.Query.Where(fee.Query.TransactionNo == TransactionNo && 
                            fee.Query.SequenceNo == SequenceNo && fee.Query.TariffComponentID == TariffComponentID);
                        if (fee.Load(fee.Query))
                        {
                            if (AppSession.Parameter.IsFeeCalculatedOnTransaction)
                            {
                                // set write off 
                                if (fee.VerificationNo == null)
                                {
                                    fee.IsWriteOff = true;
                                    fee.ChangeNote = param[6];
                                    fee.Save();
                                    grdList.Rebind();
                                }
                            }
                            else
                            {
                                // delete for re-calculation
                                if (fee.VerificationNo == null)
                                {
                                    fee.MarkAsDeleted();
                                    fee.Save();
                                    grdList.Rebind();
                                }
                            }
                        }
                    }
                }
            }            
            else if (eventArgument == "rebind")
            {
                // get selected rows
                keys = grdList.MasterTableView.Items.Cast<GridDataItem>()
                    .Where(dataItem => ((CheckBox)dataItem.FindControl("detailChkbox")).Checked)
                    .Select(dataItem =>
                        string.Format("{0}{1}{2}",
                        dataItem["TransactionNo"].Text.Replace("&nbsp;", string.Empty),
                        dataItem["SequenceNo"].Text.Replace("&nbsp;", string.Empty),
                        dataItem["TariffComponentID"].Text.Replace("&nbsp;", string.Empty)
                        )
                    ).ToList();
                grdList.Rebind();
                keys.Clear();
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack) RestoreValueFromCookie();
        }

        //protected void CalcPercPayAR_OnClick(object sender, ImageClickEventArgs e)
        //{
        //    // calculate payment AR
        //    var ivpColl = new InvoicesCollection();
        //    ivpColl.Query.Where(
        //        ivpColl.Query.IsInvoicePayment == true,
        //        ivpColl.Query.IsApproved == true,
        //        ivpColl.Query.IsVoid == false);
        //    ivpColl.LoadAll();

        //    foreach (var ivp in ivpColl)
        //    {
        //        var ivipColl = new InvoicesItemCollection();
        //        ivipColl.Query.Where(ivipColl.Query.InvoiceNo == ivp.InvoiceNo);
        //        ivipColl.LoadAll();

        //        var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
        //        feeColl.SetInvoicePayment(ivp, ivipColl, AppSession.UserLogin.UserID);

        //        feeColl.Save();
        //    }
        //}

        protected void cboItemGroupID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var igroup = new ItemGroupQuery("a");
            igroup.es.Top = 20;
            igroup.Where(
                igroup.SRItemType.In(ItemType.Service, ItemType.Laboratory, ItemType.Radiology),
                igroup.ItemGroupName.Like(searchTextContain),
                igroup.IsActive == true
                );

            cboItemGroupID.DataSource = igroup.LoadDataTable();
            cboItemGroupID.DataBind();
        }

        protected void cboItemGroupID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemGroupName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemGroupID"].ToString();
        }

        protected void cboGuarantorID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var guar = new GuarantorQuery("a");
            guar.es.Top = 20;
            guar.Where(
                guar.GuarantorName.Like(searchTextContain),
                guar.IsActive == true
                );

            cboGuarantorID.DataSource = guar.LoadDataTable();
            cboGuarantorID.DataBind();
        }

        protected void cboGuarantorID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["GuarantorName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["GuarantorID"].ToString();
        }

        protected void btnPlanningPaymentDate_Click(object sender, ImageClickEventArgs e)
        {
            if (txtPlanningPaymentDate.SelectedDate.HasValue)
            {
                List<string> lVerifNo = new List<string>();
                foreach (GridDataItem gdi in grdListV.MasterTableView.Items)
                {
                    var chk = gdi.FindControl("chk") as CheckBox;
                    if (chk != null)
                    {
                        if (chk.Checked)
                        {
                            lVerifNo.Add(gdi.GetDataKeyValue("VerificationNo").ToString());
                        }
                    }
                }

                if (lVerifNo.Count > 0)
                {
                    ParamedicFeeVerification.SetPlanningPaymentDate(lVerifNo, txtPlanningPaymentDate.SelectedDate.Value);
                    grdListV.Rebind();
                }
            }
        }

        protected void chkUnapprovedOnly_CheckedChanged(object sender, EventArgs e)
        {
            LoadCboPhysicianUnapproved();
            cboPhysicianUnapproved.Enabled = chkUnapprovedOnly.Checked;
        }

        private void LoadCboPhysicianUnapproved()
        {
            var par = new ParamedicQuery("par");
            var ff = new ParamedicFeeVerificationQuery("ff");
            ff.InnerJoin(par).On(ff.ParamedicID == par.ParamedicID)
                .Where(ff.IsApproved == false, ff.IsVoid == false)
                .Select(par.ParamedicID, par.ParamedicName)
                .OrderBy(par.ParamedicName.Ascending);
            ff.es.Distinct = true;

            var dtb = ff.LoadDataTable();

            cboPhysicianUnapproved.Items.Clear();
            cboPhysicianUnapproved.Items.Add("");
            foreach (DataRow dr in dtb.Rows)
            {
                cboPhysicianUnapproved.Items.Add(new RadComboBoxItem(dr["ParamedicName"].ToString(), dr["ParamedicID"].ToString()));
            }
            cboPhysicianUnapproved.SelectedIndex = 0;
        }
    }
}
