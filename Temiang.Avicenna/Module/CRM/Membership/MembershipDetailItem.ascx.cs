using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.CRM
{
    public partial class MembershipDetailItem : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;
                ViewState["MembershipDetailID"] = "-1";

                var coll = (MembershipDetailCollection)Session["collMembershipDetail" + Request.UserHostName];
                if (coll.Count == 0)
                    txtStartDate.SelectedDate = DateTime.Now;
                else
                {
                    var lastEndDate = (coll.OrderByDescending(c => c.EndDate).Select(c => c.EndDate)).Take(1).SingleOrDefault();
                    txtStartDate.SelectedDate = lastEndDate.Value.Date.AddDays(1);
                }

                txtEndDate.SelectedDate = txtStartDate.SelectedDate.Value.Date.AddYears(1).AddDays(-1);
                hdnTotalAmount.Value = "0";
                hdnReedeemAmount.Value = "0";
                hdnBalanceAmount.Value = "0";
                hdnRewardPoint.Value = "0";
                hdnRewardPointRefferal.Value = "0";
                hdnClaimedPoint.Value = "0";
                return;
            }
            ViewState["IsNewRecord"] = false;
            var id = Convert.ToInt64(DataBinder.Eval(DataItem, MembershipDetailMetadata.ColumnNames.MembershipDetailID));
            ViewState["MembershipDetailID"] = id.ToString();
            txtStartDate.SelectedDate = Convert.ToDateTime(DataBinder.Eval(DataItem, MembershipDetailMetadata.ColumnNames.StartDate));
            txtEndDate.SelectedDate = Convert.ToDateTime(DataBinder.Eval(DataItem, MembershipDetailMetadata.ColumnNames.EndDate));

            var ta = Convert.ToDecimal(DataBinder.Eval(DataItem, MembershipDetailMetadata.ColumnNames.TotalAmount));
            hdnTotalAmount.Value = ta.ToString();
            var ra = Convert.ToDecimal(DataBinder.Eval(DataItem, MembershipDetailMetadata.ColumnNames.ReedeemAmount));
            hdnReedeemAmount.Value = ra.ToString();
            var ba = Convert.ToDecimal(DataBinder.Eval(DataItem, MembershipDetailMetadata.ColumnNames.BalanceAmount));
            hdnBalanceAmount.Value = ba.ToString();
            var rp = Convert.ToDecimal(DataBinder.Eval(DataItem, MembershipDetailMetadata.ColumnNames.RewardPoint));
            hdnRewardPoint.Value = rp.ToString();
            var rpr = Convert.ToDecimal(DataBinder.Eval(DataItem, MembershipDetailMetadata.ColumnNames.RewardPointRefferal));
            hdnRewardPointRefferal.Value = rpr.ToString();
            var cp = Convert.ToDecimal(DataBinder.Eval(DataItem, MembershipDetailMetadata.ColumnNames.ClaimedPoint));
            hdnClaimedPoint.Value = cp.ToString();

            txtStartDate.Enabled = (ta == 0 && rp == 0);
            txtEndDate.Enabled = (ta == 0 && rp == 0);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                MembershipDetailCollection coll = (MembershipDetailCollection)Session["collMembershipDetail" + Request.UserHostName];

                DateTime sValue = txtStartDate.SelectedDate.Value.Date;
                bool isExist = false, isBetween = false;
                foreach (BusinessObject.MembershipDetail item in coll)
                {
                    if (item.StartDate.Value.Date.Equals(sValue))
                    {
                        isExist = true;
                        break;
                    }

                    if (item.StartDate.Value.Date <= sValue && item.EndDate.Value.Date >= sValue)
                    {
                        isBetween = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Member Since Date: {0} has exist", sValue);
                }

                if (isBetween)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Member Since Date: {0} is still active in the previous period", sValue);
                }
            }
        }

        #region Properties for return entry value

        public long MembershipDetailID
        {
            get { return Convert.ToInt64(ViewState["MembershipDetailID"]); }
        }

        public DateTime StartDate
        {
            get { return Convert.ToDateTime(txtStartDate.SelectedDate); }
        }

        public DateTime EndDate
        {
            get { return Convert.ToDateTime(txtEndDate.SelectedDate); }
        }

        public decimal TotalAmount
        {
            get { return Convert.ToDecimal(hdnTotalAmount.Value); }
        }

        public decimal ReedeemAmount
        {
            get { return Convert.ToDecimal(hdnReedeemAmount.Value); }
        }

        public decimal BalanceAmount
        {
            get { return Convert.ToDecimal(hdnBalanceAmount.Value); }
        }

        public decimal RewardPoint
        {
            get { return Convert.ToDecimal(hdnRewardPoint.Value); }
        }

        public decimal RewardPointRefferal
        {
            get { return Convert.ToDecimal(hdnRewardPointRefferal.Value); }
        }

        public decimal ClaimedPoint
        {
            get { return Convert.ToDecimal(hdnClaimedPoint.Value); }
        }
        #endregion
    }
}