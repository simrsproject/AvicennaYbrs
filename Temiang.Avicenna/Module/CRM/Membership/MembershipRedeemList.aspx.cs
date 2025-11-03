using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI.WebControls;
using System.Linq;

namespace Temiang.Avicenna.Module.CRM
{
    public partial class MembershipRedeemList : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var pmd = new BusinessObject.MembershipDetail();
                pmd.LoadByPrimaryKey(Request.QueryString["mid"].ToInt());

                var pm = new Membership();
                pm.LoadByPrimaryKey(pmd.MembershipNo);

                var pat = new Patient();
                pat.LoadByPrimaryKey(pm.PatientID);

                Page.Title = "Redeem List for Membership#: " + pm.MembershipNo + " [" + pat.PatientName + " / MRN: " + pat.MedicalNo + "]";

                (Helper.FindControlRecursive(this, "btnOk") as Button).Visible = false;
                (Helper.FindControlRecursive(this, "btnCancel") as Button).Visible = false;
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
                ((RadGrid)source).DataSource = ItemRedemptionHistorys();
        }

        private DataTable ItemRedemptionHistorys()
        {
            var query = new MembershipItemRedemptionDetailQuery("a");
            var hd = new MembershipItemRedemptionQuery("b");
            var pat = new PatientQuery("c");

            query.InnerJoin(hd).On(hd.TransactionNo == query.TransactionNo);
            query.InnerJoin(pat).On(pat.PatientID == hd.PatientID);
            query.Select
                (
                    query.TransactionNo,
                    hd.TransactionDate,
                    query.ClaimedPoint.As("TotalPointsUses"),
                    pat.PatientName,
                    pat.Address,
                    pat.PhoneNo,
                    pat.MobilePhoneNo
                );
            query.Where(query.MembershipDetailID == Request.QueryString["mid"].ToInt(), hd.IsApproved == true);
            query.OrderBy(hd.TransactionDate.Descending);

            var dtb = query.LoadDataTable();

            return dtb;
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = e.DetailTableView.ParentItem;
            string id = dataItem.GetDataKeyValue("TransactionNo").ToString();

            var query = new MembershipItemRedemptionItemQuery("a");
            var itm = new MembershipItemRedeemQuery("b");
            var grp = new AppStandardReferenceItemQuery("c");

            query.InnerJoin(itm).On(itm.ItemReedemID == query.ItemReedemID);
            query.InnerJoin(grp).On(grp.StandardReferenceID == "ItemReedemGroup" && grp.ItemID == itm.SRItemReedemGroup);

            query.Select(query,
                itm.ItemReedemName,
                grp.ItemName.As("ItemReedemGroup")
                );

            query.Where(query.TransactionNo == id);
            query.OrderBy(itm.SRItemReedemGroup.Ascending, itm.ItemReedemName.Ascending);

            e.DetailTableView.DataSource = query.LoadDataTable();
        }

        public override bool OnButtonOkClicked()
        {
            Validate();

            if (!IsValid)
                return false;

            return true;
        }
    }
}