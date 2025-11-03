using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Finance.ParamedicFee.ParamedicFeePaymentV3
{
    public partial class ParamedicFeePaymentDetail : BasePageDialog
    {
        public bool IsFeeCalculateProporsionalOnPayment() {
            return AppSession.Parameter.IsFeeCalculateProporsionalOnPayment;
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'rebind'";
        }

        public List<string> SetSelected() {
            Dictionary<string, string> NewSelected = new Dictionary<string, string>();
            foreach (GridDataItem gdi in grdList.MasterTableView.Items.Cast<GridDataItem>())
            {
                var chkBox = ((System.Web.UI.WebControls.CheckBox)gdi.FindControl("detailChkbox"));
                if (chkBox != null)
                {
                    if (chkBox.Checked)
                    {
                        NewSelected.Add(gdi.GetDataKeyValue("Id").ToString(), Request.QueryString["parid"]);
                    }
                }
            }

            Dictionary<string, string> ls = SelectedKeys;

            // remove unselected
            var lx = ls.Where(x => !NewSelected.Select(y => y.Key).Contains(x.Key) && 
                x.Value == Request.QueryString["parid"]);
            for (var i = lx.Count() - 1; i >= 0; i--) {
                ls.Remove(lx.ElementAt(i).Key);
            }

            // add new
            foreach (var n in NewSelected) {
                if (!ls.ContainsKey(n.Key)) {
                    ls.Add(n.Key, n.Value);
                }
            }

            SelectedKeys = ls;

            return ls.Select(x => x.Key).ToList();
        }

        public Dictionary<string, string> SelectedKeys
        {
            get
            {
                Dictionary<string, string> ls = new Dictionary<string, string>();
                if (Session["SelectedFeeTransPayment"] != null)
                {
                    return (Dictionary<string, string>)(Session["SelectedFeeTransPayment"]);
                }
                else
                {
                    return new Dictionary<string, string>();
                }
            }

            set
            {
                Session["SelectedFeeTransPayment"] = value;
            }
        }

        public override bool OnButtonOkClicked()
        {
            SetSelected();

            return true;
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var ds = OutstandingPayment();
            //ds.DefaultView.Sort = "ParamedicID asc";
            //ds = ds.DefaultView.ToTable();

            grdList.DataSource = ds;

            if (!IsFeeCalculateProporsionalOnPayment()) {
                grdList.MasterTableView.GetColumnSafe("Price").Visible = false;
                grdList.MasterTableView.GetColumnSafe("PctgFee").Visible = false;
                grdList.MasterTableView.GetColumnSafe("PaymentRefNoRecal").Visible = false;
                grdList.MasterTableView.GetColumnSafe("PaymentRefDate").Visible = false;
            }
        }

        protected void grdList_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var di = e.Item as GridDataItem;
                var id = di.GetDataKeyValue("Id").ToString();
                var chk = (e.Item.FindControl("detailChkbox") as CheckBox);
                if (chk != null)
                {
                    chk.Checked = SelectedKeys.Where(x => x.Key == id).Any(); //.ContainsKey(id);
                }
            }
        }

        private DataTable OutstandingPayment()
        {
            string ParID = Request.QueryString["parid"];
            DateTime? StartDate = null, EndDate= null;
            if (DateTime.TryParse(Request.QueryString["startdate"], out DateTime oStartDate)) StartDate = oStartDate;
            if (DateTime.TryParse(Request.QueryString["enddate"], out DateTime oEndDate)) EndDate = oEndDate;

            DateTime? StartDateDc = null, EndDateDc = null;
            if (DateTime.TryParse(Request.QueryString["startdatedc"], out DateTime oStartDateDc)) StartDateDc = oStartDateDc;
            if (DateTime.TryParse(Request.QueryString["enddatedc"], out DateTime oEndDateDc)) EndDateDc = oEndDateDc;

            DateTime? PlanningDate = null;
            if (DateTime.TryParse(Request.QueryString["planningdate"], out DateTime oPlanningDate)) PlanningDate = oPlanningDate;

            string RegistrationNo = Request.QueryString["regno"];
            string MedicalNo = Request.QueryString["mr"];
            string PatientName = Request.QueryString["name"];
            string GuarantorID = Request.QueryString["guarid"];
            string SrGuarantorType = Request.QueryString["srguartype"];
            string PaymentNoDraft = Request.QueryString["payno"];

            return (new ParamedicFeeTransChargesItemCompByDischargeDateCollection())
                .GetParamedicFeeProrataBayarDetail(
                    StartDate, EndDate, ParID, StartDateDc, EndDateDc, PlanningDate,
                    RegistrationNo, MedicalNo, PatientName, GuarantorID, SrGuarantorType, PaymentNoDraft
                );
        }

        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            var gtv = ((GridTableView)(((CheckBox)sender).Parent.Parent.Parent.Parent.Parent));
            foreach (CheckBox chkBox in gtv.Items.Cast<GridDataItem>().Select(dataItem => (CheckBox)dataItem.FindControl("detailChkbox")).Where(chkBox => chkBox.Visible))
            {
                chkBox.Checked = ((CheckBox)sender).Checked;
            }
            ChkChanged(sender, e);
        }

        protected void ChkChanged(object sender, EventArgs e)
        {
            decimal sum = 0;
            foreach (GridDataItem gdi in grdList.MasterTableView.Items.Cast<GridDataItem>())
            {
                var chkBox = ((CheckBox)gdi.FindControl("detailChkbox"));
                if (chkBox != null)
                {
                    if (chkBox.Checked)
                    {
                        var FeeAmount = System.Convert.ToDecimal(gdi["FeeAmount"].Text);
                        sum += FeeAmount;// - TaxAmount;
                    }
                }
            }
            //txtPaymentAmount.Value = System.Convert.ToDouble(sum);
        }

        //protected void grdList_ItemCommand(object sender, GridCommandEventArgs e)
        //{
        //    switch (e.CommandName)
        //    {
        //        case "recalFeePercInvPay":
        //            string InvoicePaymentNo = e.CommandArgument.ToString();
        //            string errMsg = "";
        //            try
        //            {
        //                using (esTransactionScope trans = new esTransactionScope())
        //                {
        //                    var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
        //                    feeColl.SetInvoicePayment(InvoicePaymentNo, AppSession.UserLogin.UserID);
        //                    feeColl.Save();

        //                    //Commit if success, Rollback if failed
        //                    trans.Complete();
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                errMsg = ex.Message;
        //            }
        //            if (string.IsNullOrEmpty(errMsg)) errMsg = string.Format("Calculation completed for invoice payment {0}", InvoicePaymentNo);
        //            ShowInformationHeader(errMsg);
        //            grdList.Rebind();

        //            break;
        //    }
        //}
    }
}
