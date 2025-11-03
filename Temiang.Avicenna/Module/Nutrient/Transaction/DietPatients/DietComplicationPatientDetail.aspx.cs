using System;
using System.Linq;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.Nutrient.Transaction
{
    public partial class DietComplicationPatientDetail : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.DietPatients;

            if (!IsPostBack)
            {
                txtTransactionNo.Text = Request.QueryString["transNo"];
                txtDietID.Text = Request.QueryString["dietId"];
                var diet = new Diet();
                if (diet.LoadByPrimaryKey(txtDietID.Text))
                    lblDietName.Text = diet.DietName;
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (ViewState["list" + Request.UserHostName] == null)
            {
                var query = new DietComplicationQuery("a");
                var dietQ = new DietQuery("b");
                var dietCompQ = new DietComplicationPatientQuery("c");
                query.Select(query.DietComplicationID, dietQ.DietName.As("DietComplicationName"));
                query.InnerJoin(dietQ).On(query.DietComplicationID == dietQ.DietID);
                query.LeftJoin(dietCompQ).On(query.DietID == dietCompQ.DietID &
                                             query.DietComplicationID == dietCompQ.DietComplicationID &
                                             dietCompQ.TransactionNo == txtTransactionNo.Text);
                query.Where(query.DietID == txtDietID.Text, dietCompQ.DietComplicationID.IsNull());
                query.OrderBy(query.DietComplicationID.Ascending);

                DataTable tbl = query.LoadDataTable();

                ViewState["list" + Request.UserHostName] = tbl;
                grdList.DataSource = tbl;
            }
            else
                grdList.DataSource = (DataTable)ViewState["list" + Request.UserHostName];
        }

        protected void grdList_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "add")
            {
                var src = ((DataTable)ViewState["list" + Request.UserHostName]).Rows[e.Item.DataSetIndex];

                var row = ((DataTable)ViewState["selected" + Request.UserHostName]).NewRow();
                row["DietComplicationID"] = src["DietComplicationID"];
                row["DietComplicationName"] = src["DietComplicationName"];

                var dst = ((DataTable)ViewState["selected" + Request.UserHostName]);

                bool exist = dst.Rows.Cast<DataRow>().Where(bar => bar["DietComplicationID"].ToString() == src["DietComplicationID"].ToString()).Any();

                if (!exist)
                    dst.Rows.Add(row);
                else
                {
                    dst.AcceptChanges();
                    ViewState["selected" + Request.UserHostName] = dst;
                }
                
                grdSelected.Rebind();

                //-------------------------
                ((DataTable)ViewState["list" + Request.UserHostName]).Rows.Remove(src);
                grdList.Rebind();
            }
        }

        protected void grdSelected_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "remove")
            {
                var row = ((DataTable)ViewState["selected" + Request.UserHostName]).Rows[e.Item.DataSetIndex];

                var scr = ((DataTable)ViewState["list" + Request.UserHostName]).NewRow();
                scr["DietComplicationID"] = row["DietComplicationID"];
                scr["DietComplicationName"] = row["DietComplicationName"];

                var dst = ((DataTable)ViewState["list" + Request.UserHostName]);

                bool exist = dst.Rows.Cast<DataRow>().Where(bar => bar["DietComplicationID"].ToString() == row["DietComplicationID"].ToString()).Any();

                if (!exist)
                    dst.Rows.Add(scr);
                else
                {
                    dst.AcceptChanges();
                    ViewState["list" + Request.UserHostName] = dst;
                }

                grdList.Rebind();

                //-------------------------
                ((DataTable)ViewState["selected" + Request.UserHostName]).Rows.Remove(row);
                grdSelected.Rebind();
            }
        }

        protected void grdSelected_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (ViewState["selected" + Request.UserHostName] == null)
            {
                DataTable tbl;

                var coll = new DietComplicationPatientCollection();
                coll.Query.Where(coll.Query.TransactionNo == txtTransactionNo.Text, coll.Query.DietID == txtDietID.Text);
                coll.LoadAll();
                if (coll.Count == 0)
                    SetDataColumnSelected(out tbl);
                else
                {
                    var query = new DietComplicationPatientQuery("a");
                    var dietQ = new DietQuery("b");
                    query.Select(query.DietComplicationID, dietQ.DietName.As("DietComplicationName"));
                    query.InnerJoin(dietQ).On(query.DietComplicationID == dietQ.DietID);
                    query.Where(query.DietID == txtDietID.Text, query.TransactionNo == txtTransactionNo.Text);
                    query.OrderBy(query.DietComplicationID.Ascending);

                    tbl = query.LoadDataTable();
                }

                ViewState["selected" + Request.UserHostName] = tbl;
                grdSelected.DataSource = tbl;
            }
            else
                grdSelected.DataSource = (DataTable)ViewState["selected" + Request.UserHostName];
        }

        private static void SetDataColumnSelected(out DataTable dataTable)
        {
            var tbl = new DataTable();

            var col = new DataColumn("DietComplicationID", typeof(string));
            tbl.Columns.Add(col);

            col = new DataColumn("DietComplicationName", typeof(string));
            tbl.Columns.Add(col);

            dataTable = tbl;
        }

        public override bool OnButtonOkClicked()
        {
            var coll = (DietComplicationPatientCollection)Session["collDietComplicationPatient" + Request.UserHostName];

            var tbl = (DataTable)ViewState["selected" + Request.UserHostName];
            var tbldel = (DataTable)ViewState["list" + Request.UserHostName];

            foreach (DataRow row in tbldel.Rows)
            {
                var entity = FindDietComplication(row["DietComplicationID"].ToString());
                if (entity != null)
                    entity.MarkAsDeleted();
            }

            foreach (DataRow row in tbl.Rows)
            {
                var entity = FindDietComplication(row["DietComplicationID"].ToString()) ?? coll.AddNew();

                entity.TransactionNo = txtTransactionNo.Text;
                entity.DietID = txtDietID.Text;
                entity.DietComplicationID = row["DietComplicationID"].ToString();
                entity.DietComplicationName = row["DietComplicationName"].ToString();
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
            return true;
        }

        private DietComplicationPatient FindDietComplication(string dietId)
        {
            var coll = (DietComplicationPatientCollection)Session["collDietComplicationPatient" + Request.UserHostName];
            return
                coll.FirstOrDefault(
                    detail =>
                    detail.DietComplicationID == dietId & detail.DietID == txtDietID.Text &
                    detail.TransactionNo == txtTransactionNo.Text);
        }
    }
}
