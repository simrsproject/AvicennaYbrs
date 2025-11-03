using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Inventory.Master
{
    public partial class ItemProductDosageDetail : BaseUserControl
    {
        public object DataItem { get; set; }

        protected override void OnDataBinding(EventArgs e)
        {
            txtDosage.Value = 1;

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                return;
            }
            ViewState["IsNewRecord"] = false;

            var query = new AppStandardReferenceItemQuery();
            query.Select
                (
                    query.ItemID,
                    query.ItemName
                );
            query.Where
                (
                    query.ItemID == DataBinder.Eval(DataItem, ItemProductDosageDetailMetadata.ColumnNames.SRDosageUnit).ToString(),
                    query.StandardReferenceID == AppEnum.StandardReference.DosageUnit.ToString(),
                    query.IsActive == true
                );

            cboSRDosageUnit.DataSource = query.LoadDataTable();
            cboSRDosageUnit.DataBind();

            cboSRDosageUnit.SelectedValue = DataBinder.Eval(DataItem, ItemProductDosageDetailMetadata.ColumnNames.SRDosageUnit).ToString();
            txtDosage.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ItemProductDosageDetailMetadata.ColumnNames.Dosage));
        }

        protected void cboSRDosageUnit_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new AppStandardReferenceItemQuery();
            query.es.Top = 10;
            query.Select
                (
                    query.ItemID,
                    query.ItemName
                );
            query.Where
                (
                    query.Or
                        (
                            query.ItemID.Like(searchTextContain),
                            query.ItemName.Like(searchTextContain)
                        ),
                        query.StandardReferenceID == AppEnum.StandardReference.DosageUnit.ToString(),
                        query.IsActive == true
                );

            cboSRDosageUnit.DataSource = query.LoadDataTable();
            cboSRDosageUnit.DataBind();
        }

        protected void cboSRDosageUnit_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (BusinessObject.ItemProductDosageDetailCollection)Session["collItemProductDosageDetail"];

                var id = cboSRDosageUnit.SelectedValue;
                var isExist = coll.Any(row => row.ItemID.Equals(id));
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("ID: {0} has exist", id);
                }
            }
        }

        public String SRDosageUnit
        {
            get { return cboSRDosageUnit.SelectedValue; }
        }

        public String SRDosageUnitName
        {
            get { return cboSRDosageUnit.Text; }
        }

        public Decimal Dosage
        {
            get { return Convert.ToDecimal(txtDosage.Value); }
        }
    }
}