using System;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Web.UI;
using System.Data;
using System.Linq;

namespace Temiang.Avicenna.Module.Charges.Dispensary
{
    public partial class PrescriptionHistoryDialog : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            //ProgramID = Request.QueryString["rt"] == "opr" ? AppConstant.Program.PrescriptionSalesOpr : AppConstant.Program.PrescriptionSales;

            if (!IsPostBack)
            {
                var patient = new Patient();
                patient.LoadByPrimaryKey(Request.QueryString["pid"]);

                Title = "Prescription List : " + patient.PatientName + "  [MRN : " + patient.MedicalNo + "]";

                txtFromDate.SelectedDate = DateTime.Now.AddDays(-30);
                txtToDate.SelectedDate = DateTime.Now;

                (Helper.FindControlRecursive(this, "btnOk") as Button).Visible = false;
                (Helper.FindControlRecursive(this, "btnCancel") as Button).Visible = false;
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var presc = new TransPrescriptionQuery("a");
            var reg = new RegistrationQuery("b");
            var detail = new TransPrescriptionItemQuery("c");
            var item1 = new ItemQuery("d");
            var item2 = new ItemQuery("e");
            var medic = new ParamedicQuery("f");

            presc.Select(
                presc.RegistrationNo,
                presc.PrescriptionNo,
                @"<a.PrescriptionNo + '  [Prescribed by  : ' + ISNULL(f.ParamedicName, '-') + ']' AS PrescriptionGroup>",
                @"<CONVERT(VARCHAR(8), a.PrescriptionDate, 112) + a.PrescriptionNo AS PrescriptionOrder>",
                presc.PrescriptionDate,
                detail.ItemID,
                item1.ItemName,
                detail.ItemInterventionID,
                item2.ItemName.Coalesce("''").As("ItemInterventionName"),
                detail.ResultQty,
                detail.SRItemUnit,
                detail.IsRFlag,
                medic.ParamedicName.Coalesce("''")
                );
            presc.InnerJoin(reg).On(presc.RegistrationNo == reg.RegistrationNo);
            presc.InnerJoin(detail).On(detail.PrescriptionNo == presc.PrescriptionNo);
            presc.InnerJoin(item1).On(item1.ItemID == detail.ItemID);
            presc.LeftJoin(item2).On(item2.ItemID == detail.ItemInterventionID);
            presc.LeftJoin(medic).On(medic.ParamedicID == presc.ParamedicID);
            presc.Where(
                reg.PatientID == Request.QueryString["pid"],
                reg.IsVoid == false,
                presc.IsApproval == true
                );

            if (Request.QueryString["rt"] == "reg")
            {
                var itmmed = new ItemProductMedicQuery("g");
                presc.InnerJoin(itmmed).On(itmmed.ItemID == detail.ItemID && itmmed.IsMedication == true);
            }

            if (!txtFromDate.IsEmpty && !txtToDate.IsEmpty)
                presc.Where(presc.PrescriptionDate.Date().Between(txtFromDate.SelectedDate.Value.Date,
                                                                  txtToDate.SelectedDate.Value.Date));
            if (!string.IsNullOrEmpty(cboItemID.SelectedValue))
                presc.Where(presc.Or(detail.ItemID == cboItemID.SelectedValue, detail.ItemInterventionID == cboItemID.SelectedValue));

            presc.OrderBy(presc.PrescriptionNo.Descending);

            grdList.DataSource = presc.LoadDataTable();
        }

        protected string GetItemName(object isRFlag, object itemName)
        {
            if (isRFlag.Equals(true))
                return itemName.ToString();
            return "&nbsp;&nbsp;&nbsp;" + itemName.ToString();
        }

        protected void cboItemID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"] + " [" + ((DataRowView)e.Item.DataItem)["ItemID"] + "]";
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboItemID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var tbl = PopulateItem(e.Text);
            cboItemID.DataSource = tbl.Rows.Count == 0 ? PopulateItem(e.Text) : tbl;
            cboItemID.DataBind();
        }

        private DataTable PopulateItem(string parameter)
        {
            string searchTextContain = string.Format("%{0}%", parameter);
            var query = new ItemQuery("a");
            var tciq = new TransPrescriptionItemQuery("b");
            var tcq = new TransPrescriptionQuery("c");
            var rq = new RegistrationQuery("d");

            query.InnerJoin(tciq).On(query.ItemID == tciq.ItemID);
            query.InnerJoin(tcq).On(tciq.PrescriptionNo == tcq.PrescriptionNo);
            query.InnerJoin(rq).On(tcq.RegistrationNo == rq.RegistrationNo);

            query.es.Top = 20;
            query.es.Distinct = true;
            query.Select
                (
                    query.ItemID,
                    query.ItemName
                );

            query.Where(
                    query.Or(
                            query.ItemName.Like(searchTextContain),
                            query.ItemID.Like(searchTextContain)
                        ),
                    rq.PatientID == Request.QueryString["pid"]);
            query.OrderBy(query.ItemName.Ascending);

            var tbl = query.LoadDataTable();

            if (tbl.Rows.Count < 20)
            {
                var coll = new ItemCollection();
                coll.Load(query);

                var items = coll.Select(i => (i.ItemID));

                if (items.Any())
                {
                    query = new ItemQuery("a");
                    tciq = new TransPrescriptionItemQuery("b");
                    tcq = new TransPrescriptionQuery("c");
                    rq = new RegistrationQuery("d");

                    query.InnerJoin(tciq).On(query.ItemID == tciq.ItemInterventionID);
                    query.InnerJoin(tcq).On(tciq.PrescriptionNo == tcq.PrescriptionNo);
                    query.InnerJoin(rq).On(tcq.RegistrationNo == rq.RegistrationNo);

                    query.es.Top = 20 - tbl.Rows.Count;
                    query.es.Distinct = true;
                    query.Select
                     (
                         query.ItemID,
                         query.ItemName
                     );

                    query.Where(
                            query.ItemID.NotIn(items),
                            query.Or(
                                    query.ItemName.Like(searchTextContain),
                                    query.ItemID.Like(searchTextContain)
                                ), 
                            rq.PatientID == Request.QueryString["pid"]);
                    tbl.Merge(query.LoadDataTable());
                }
            }

            return tbl;
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            grdList.Rebind();
        }
    }
}
