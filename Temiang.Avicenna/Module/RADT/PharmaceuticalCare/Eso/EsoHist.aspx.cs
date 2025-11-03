using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using System.Text;

namespace Temiang.Avicenna.Module.RADT.PharmaceuticalCare
{
    public partial class EsoHist : BasePageDialog
    {

        public override string RegistrationNo => Request.QueryString["regno"];
        public override string PatientID => Request.QueryString["patid"];

        protected void Page_Init(object sender, EventArgs e)
        {
            if (AppConstant.Program.PharmaceuticalCare.Equals(Request.QueryString["prgid"]))
                ProgramID = AppConstant.Program.PharmaceuticalCare;
            else
                ProgramID = AppConstant.Program.ElectronicMedicalRecord;

            if (!IsPostBack)
            {
                this.Title = "Drug Side Effects";
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            Footer.Visible = false;
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = EsoDataTable(RegistrationNo);
        }

        private DataTable EsoDataTable(string registrationNo)
        {
            var vis = new RegistrationEsoQuery("v");
            var usr = new AppUserQuery("u");
            vis.InnerJoin(usr).On(vis.CreatedByUserID == usr.UserID);
            vis.Select(vis, usr.UserName.As("CreatedByUserName"), usr.LicenseNo);
            vis.Where(vis.RegistrationNo == registrationNo);
            vis.OrderBy(vis.EsoNo.Descending);

            var dtb = vis.LoadDataTable();
            foreach (DataRow row in dtb.Rows)
            {
                if (row["EsoManifestations"] != DBNull.Value && !string.IsNullOrEmpty(row["EsoManifestations"].ToString()))
                {
                    var itemIds = row["EsoManifestations"].ToString().Split(';');
                    var stdi = new AppStandardReferenceItemQuery("a");
                    stdi.Select("<STRING_AGG( ISNULL(ItemName, ' '), ',') As EsoManifestations>");
                    stdi.Where(stdi.StandardReferenceID == AppEnum.StandardReference.EsoManifestation.ToString(), stdi.ItemID.In(itemIds));
                    var dtbMani = stdi.LoadDataTable();
                    row["EsoManifestations"] = (dtbMani.Rows[0][0]).ToString();
                }

            }
            return dtb;
        }

        protected void grdList_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                if (item.GetDataKeyValue("IsDeleted") == DBNull.Value) return;
                var isVoid = Convert.ToBoolean(item.GetDataKeyValue("IsDeleted"));
                //TableCell cell = item["ItemName"];
                if (isVoid)
                {
                    item.Style.Add(HtmlTextWriterStyle.TextDecoration, "line-through");
                    item.Style.Add(HtmlTextWriterStyle.Color, "gray");
                }
            }
        }

        protected void grdList_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            var esoNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]["EsoNo"]).ToInt();

            var ent = new RegistrationEso();
            if (ent.LoadByPrimaryKey(RegistrationNo, esoNo))
            {
                ent.IsDeleted = true;
                ent.Save();
            }

            ((RadGrid)sender).Rebind();
        }

        #region Drug Item
        protected string DrugItemHtml(GridItem container)
        {
            var no = DataBinder.Eval(container.DataItem, "EsoNo").ToInt();

            var dtb = DrugItemDataTable(RegistrationNo, no);
            var strb = new StringBuilder();
            strb.AppendLine("<table id='counselingLine'>");
            strb.AppendLine("<tr>");
            strb.AppendLine("<th style = 'width: 250px'>Drug Item</th><th>Consume Method</th><th style = 'width: 80px'>Suspect</th>");
            strb.AppendLine("</tr>");

            foreach (DataRow row in dtb.Rows)
            {
                strb.AppendFormat("<tr><td>{0}</td><td>{1}</td><td align=\"center\">{2}</td></tr>", row["ItemDescription"], row["ConsumeMethod"], true.Equals(row["IsSuspect"]) ? "&#9635;" : string.Empty);
            }
            strb.AppendLine("</table>");

            return strb.ToString();
        }
        private DataTable DrugItemDataTable(string registrationNo, int esoNo)
        {
            var qr = new RegistrationEsoItemQuery("a");
            var qrMr = new MedicationReceiveQuery("b");
            qr.InnerJoin(qrMr).On(qr.MedicationReceiveNo == qrMr.MedicationReceiveNo);
            qr.Where(qr.RegistrationNo == registrationNo, qr.EsoNo == esoNo);

            qr.OrderBy(qrMr.ItemDescription.Ascending);
            qr.Select(qr, qrMr.ItemDescription, qrMr.RefTransactionNo, qrMr.RefSequenceNo, qrMr.SRConsumeMethod, qrMr.ConsumeQty, qrMr.SRConsumeUnit);

            var dtb = qr.LoadDataTable();
            dtb.Columns.Add(new DataColumn("ConsumeMethod", typeof(string)));
            dtb.PrimaryKey = new DataColumn[] { dtb.Columns["MedicationReceiveNo"] };

            foreach (DataRow row in dtb.Rows)
            {
                var itemDesc = row["ItemDescription"].ToString();
                if (row["RefTransactionNo"] != DBNull.Value && row["RefSequenceNo"] != DBNull.Value)
                    itemDesc = MedicationReceive.PrescriptionItemDescription(row["RefTransactionNo"].ToString(), row["RefSequenceNo"].ToString(), row["ItemDescription"].ToString(), false, false);

                row["ItemDescription"] = itemDesc;

                var cons = new ConsumeMethod();
                cons.LoadByPrimaryKey(row["SRConsumeMethod"].ToString());
                row["ConsumeMethod"] = string.Format("{0} @{1} {2}", cons.SRConsumeMethodName, row["ConsumeQty"], row["SRConsumeUnit"]);
            }
            return dtb;
        }
        #endregion

    }
}
