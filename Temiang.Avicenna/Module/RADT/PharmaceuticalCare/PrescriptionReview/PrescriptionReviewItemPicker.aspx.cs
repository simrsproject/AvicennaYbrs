using System;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using System.Data;
using Temiang.Dal.DynamicQuery;
using System.Text;

namespace Temiang.Avicenna.Module.RADT.PharmaceuticalCare
{
    public partial class PrescriptionReviewItemPicker : BasePageDialog
    {
        public override string RegistrationNo => Request.QueryString["regno"];
        public override string PatientID => Request.QueryString["patid"];
        private string PrescriptionNo => Request.QueryString["pn"];
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
                Title = string.Format("Review Of {0}", Uri.UnescapeDataString(Request.QueryString["rv"]));
        }

        #region grdPrescriptionItem
        protected void grdPrescriptionItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
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
            query.Where(query.PrescriptionNo == PrescriptionNo);
            query.OrderBy("ORDERKEY", esOrderByDirection.Ascending);

            grdPrescriptionItem.DataSource = query.LoadDataTable();
        }

        protected string GetItemName(object isRFlag, object itemName)
        {
            if (isRFlag.Equals(true))
                return itemName.ToString();
            else
                return "&nbsp;&nbsp;&nbsp;" + itemName.ToString();
        }

        #endregion


        private string _retVal;
        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oArg.retval= '" + Uri.EscapeUriString(_retVal ?? string.Empty) + "'";
        }

        public override bool OnButtonOkClicked()
        {
            var strb = new StringBuilder();
            foreach (GridDataItem item in grdPrescriptionItem.MasterTableView.Items)
            {
                var txtInfo = ((RadTextBox)item.FindControl("txtInformation"));
                if (item.Selected || !string.IsNullOrWhiteSpace(txtInfo.Text))
                {
                    strb.AppendFormat("{0}: {1}", item["ItemName"].Text, txtInfo.Text);
                    strb.AppendLine(string.Empty);
                }
            }
            _retVal = strb.ToString();
            return true;
        }


    }
}
