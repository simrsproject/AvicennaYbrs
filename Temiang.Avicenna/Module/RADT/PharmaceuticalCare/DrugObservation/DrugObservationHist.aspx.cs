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
    public partial class DrugObservationHist : BasePageDialog
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
                this.Title = "Inpatient Pharmacy Observation";
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            Footer.Visible = false;
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = DrugObservations(RegistrationNo);
        }

        private DataTable DrugObservations(string registrationNo)
        {
            var vis = new RegistrationDrugObsQuery("v");
            var usr = new AppUserQuery("u");
            vis.InnerJoin(usr).On(vis.CreatedByUserID == usr.UserID);
            vis.Select(vis, usr.UserName.As("CreatedByUserName"), usr.LicenseNo);
            vis.Where(vis.RegistrationNo == registrationNo);
            vis.OrderBy(vis.DrugObsNo.Descending);
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

            var drugObsNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]["DrugObsNo"]).ToInt();

            var ent = new RegistrationDrugObs();
            if (ent.LoadByPrimaryKey(RegistrationNo, drugObsNo))
            {
                ent.IsDeleted = true;
                ent.Save();
            }

            ((RadGrid)sender).Rebind();
        }

        #region DRPs
        protected string DrpsHtml(GridItem container)
        {
            var no = DataBinder.Eval(container.DataItem, "DrugObsNo").ToInt();

            var dtb = DrpsDataTable(RegistrationNo, no);
            var strb = new StringBuilder();
            strb.AppendLine("<table id='counselingLine'>");
            strb.AppendLine("<tr>");
            strb.AppendFormat("<th style = 'width: 350px'>Criteria</th><th>{0}</th>", Convert.ToDateTime( DataBinder.Eval(container.DataItem, "DrugObsDateTime")).ToString(AppConstant.DisplayFormat.Date));
            strb.AppendLine("</tr>");

            foreach (DataRow row in dtb.Rows)
            {
                strb.AppendFormat("<tr><td>{0}</td><td>{1}Y&nbsp;&nbsp;{2}T</td></tr>", row["ItemName"], true.Equals(row["IsYes"])? "&#9635;" : "&#9723;", false.Equals(row["IsYes"]) ? "&#9635;" : "&#9723;");
            }
            strb.AppendLine("</table>");

            return strb.ToString();
        }
        private DataTable DrpsDataTable(string registrationNo, int drugObsNo)
        {
            var que = new AppStandardReferenceItemQuery("sri");
            var qrLine = new RegistrationDrugObsDrpsQuery("a");

            que.InnerJoin(qrLine)
                .On(que.ItemID == qrLine.SRDrps && qrLine.RegistrationNo == registrationNo && qrLine.DrugObsNo == drugObsNo);

            que.Where(que.StandardReferenceID == "DRPS");
            que.OrderBy(que.LineNumber.Ascending);
            que.Select(que.ItemID, que.ItemName, qrLine.IsYes);
            return que.LoadDataTable();
        }
        #endregion

        #region Drug Item
        protected string DrugItemHtml(GridItem container)
        {
            var no = DataBinder.Eval(container.DataItem, "DrugObsNo").ToInt();

            var dtb = DrugItemDataTable(RegistrationNo, no);
            var strb = new StringBuilder();
            strb.AppendLine("<table id='counselingLine'>");
            strb.AppendLine("<tr>");
            strb.AppendLine("<th style = 'width: 250px'>Drug Item</th><th>Consume Method</th>");
            strb.AppendLine("</tr>");

            foreach (DataRow row in dtb.Rows)
            {
                strb.AppendFormat("<tr><td>{0}</td><td>{1}</td></tr>", row["ItemDescription"], row["ConsumeMethod"]);
            }
            strb.AppendLine("</table>");

            return strb.ToString();
        }
        private DataTable DrugItemDataTable(string registrationNo, int drugObsNo)
        {
            var qr = new RegistrationDrugObsItemQuery("a");
            var qrMr = new MedicationReceiveQuery("b");
            qr.InnerJoin(qrMr).On(qr.MedicationReceiveNo == qrMr.MedicationReceiveNo);
            qr.Where(qr.RegistrationNo == registrationNo, qr.DrugObsNo == drugObsNo);

            qr.OrderBy(qrMr.ItemDescription.Ascending);
            qr.Select(qr.MedicationReceiveNo, qr.FollowUp, qr.Notes, qrMr.ItemDescription, qrMr.RefTransactionNo, qrMr.RefSequenceNo, qrMr.SRConsumeMethod, qrMr.ConsumeQty, qrMr.SRConsumeUnit);

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
