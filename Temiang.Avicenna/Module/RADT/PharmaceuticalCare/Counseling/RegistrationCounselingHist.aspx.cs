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
    public partial class RegistrationCounselingHist : BasePageDialog
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
                this.Title = "Counseling";
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            Footer.Visible = false;
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = Counseling(RegistrationNo);
        }

        private DataTable Counseling(string registrationNo)
        {
            var vis = new RegistrationCounselingQuery("v");
            var usr = new AppUserQuery("u");
            vis.InnerJoin(usr).On(vis.CreatedByUserID == usr.UserID);
            vis.Select(vis, usr.UserName.As("CreatedByUserName"), usr.LicenseNo);
            vis.Where(vis.RegistrationNo == registrationNo);
            vis.OrderBy(vis.CounselingNo.Descending);
            return vis.LoadDataTable();
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

            var counselingNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]["CounselingNo"]).ToInt();

            var ent = new RegistrationCounseling();
            if (ent.LoadByPrimaryKey(RegistrationNo, counselingNo))
            {
                ent.IsDeleted = true;
                ent.Save();
            }

            ((RadGrid)sender).Rebind();
        }

        protected string CounselingLineHtml(GridItem container)
        {
            var no = DataBinder.Eval(container.DataItem, "CounselingNo").ToInt();

            var dtb = CounselingLineDataTable(RegistrationNo, no);
            var strb = new StringBuilder();
            strb.AppendLine("<table id='counselingLine'>");
            strb.AppendLine("<tr>");
            strb.AppendLine("<th style = 'width: 250px'>Counseling</th><th>Notes</th>");
            strb.AppendLine("</tr>");

            foreach (DataRow row in dtb.Rows)
            {
                var itemName = row["ReferenceID"] != DBNull.Value
                    && !string.IsNullOrEmpty(row["ReferenceID"].ToString())
                    ? string.Format("&nbsp;&nbsp;&nbsp;&nbsp;{0}", row["ItemName"])
                    : row["ItemName"].ToString();
                strb.AppendFormat("<tr><td>{0}</td><td>{1}</td></tr>", itemName, row["Notes"]);
            }
            strb.AppendLine("</table>");

            return strb.ToString();

        }
        private DataTable CounselingLineDataTable(string registrationNo, int counselingNo)
        {
            var que = new AppStandardReferenceItemQuery("sri");
            var qrLine = new RegistrationCounselingLineQuery("a");

            que.InnerJoin(qrLine)
                .On(que.ItemID == qrLine.SRDrugCounseling && qrLine.RegistrationNo == registrationNo && qrLine.CounselingNo == counselingNo);

            que.Where(que.StandardReferenceID == "DrugCounseling");
            que.OrderBy(que.LineNumber.Ascending);
            que.Select(que.ItemID, que.ItemName, qrLine.Notes, que.ReferenceID, "<CONVERT(BIT,CASE WHEN a.SRDrugCounseling IS NULL THEN 0 ELSE 1 END) as IsSelected>");
            return que.LoadDataTable();
        }

    }
}
