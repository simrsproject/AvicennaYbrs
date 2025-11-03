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
    public partial class ItemProductConsumeUnitMatrixCtl : BaseUserControl
    {
        public object DataItem { get; set; }

        protected override void OnDataBinding(EventArgs e)
        {
            txtConversionFactor.Value = 1;

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                return;
            }
            ViewState["IsNewRecord"] = false;

            // Consume Unit
            var query = new AppStandardReferenceItemQuery("a");
            query.Select
                (
                    query.ItemID,
                    "<a.ItemName +'('+ a.ItemID+')' as ItemName>"
                );
            query.Where
                (
                    query.ItemID == DataBinder.Eval(DataItem, ItemProductConsumeUnitMatrixMetadata.ColumnNames.SRConsumeUnit).ToString(),
                    query.StandardReferenceID == AppEnum.StandardReference.DosageUnit.ToString(),
                    query.IsActive == true
                );
            var dtbConUnit = query.LoadDataTable();

            //var emb = new EmbalaceQuery();
            //emb.Where(emb.EmbalaceID == consumeUnit);
            //emb.Select(emb.EmbalaceID.As("ItemID"), emb.EmbalaceName.As("ItemName"));
            //dtb.Merge(emb.LoadDataTable());


            cboSRConsumeUnit.DataSource = dtbConUnit;
            cboSRConsumeUnit.DataBind();

            cboSRConsumeUnit.SelectedValue = DataBinder.Eval(DataItem, ItemProductConsumeUnitMatrixMetadata.ColumnNames.SRConsumeUnit).ToString();
            txtConversionFactor.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ItemProductConsumeUnitMatrixMetadata.ColumnNames.ConversionFactor));
        }

        protected void cboSRConsumeUnit_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
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

            cboSRConsumeUnit.DataSource = query.LoadDataTable();
            cboSRConsumeUnit.DataBind();
        }

        protected void cboSRConsumeUnit_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (BusinessObject.ItemProductConsumeUnitMatrixCollection)Session["collItemProductConsumeUnitMatrix"];

                var conUnit = cboSRConsumeUnit.SelectedValue;
                var isExist = coll.Any(row => row.SRConsumeUnit.Equals(conUnit));
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("ID: {0} has exist", cboSRConsumeUnit.Text);
                }
            }
        }

        public String SRConsumeUnit
        {
            get { return cboSRConsumeUnit.SelectedValue; }
        }

        public String SRConsumeUnitName
        {
            get { return cboSRConsumeUnit.Text; }
        }

        public Decimal ConversionFactor
        {
            get { return Convert.ToDecimal(txtConversionFactor.Value); }
        }
    }
}