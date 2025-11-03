using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class GuarantorServiceUnitPlafondDetail : BaseUserControl
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

                txtPlafondAmount.Value = 0D;
                return;
            }
            ViewState["IsNewRecord"] = false;
            cboServiceUnitID.Enabled = false;

            var unitId = (String)DataBinder.Eval(DataItem, GuarantorServiceUnitPlafondMetadata.ColumnNames.ServiceUnitID);
            var unitq = new ServiceUnitQuery();
            unitq.Where(unitq.ServiceUnitID == unitId);
            cboServiceUnitID.DataSource = unitq.LoadDataTable();
            cboServiceUnitID.DataBind();
            cboServiceUnitID.SelectedValue = unitId;

            txtPlafondAmount.Value = Convert.ToDouble(DataBinder.Eval(DataItem, GuarantorServiceUnitPlafondMetadata.ColumnNames.PlafondAmount));
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (GuarantorServiceUnitPlafondCollection)Session["collGuarantorServiceUnitPlafond"];

                bool isExist = false;
                foreach (GuarantorServiceUnitPlafond item in coll)
                {
                    if (item.ServiceUnitID.Equals(cboServiceUnitID.SelectedValue))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Service Unit : {0} has exist", cboServiceUnitID.Text);
                    return;
                }
            }

            if (txtPlafondAmount.Value <= 0)
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = string.Format("Plafond Amount required.");
                return;
            }
        }

        #region Properties for return entry value

        public String ServiceUnitID
        {
            get { return cboServiceUnitID.SelectedValue; }
        }

        public String ServiceUnitName
        {
            get { return cboServiceUnitID.Text; }
        }

        public Decimal PlafondAmount
        {
            get { return Convert.ToDecimal(txtPlafondAmount.Value); }
        }
        #endregion

        protected void cboServiceUnitID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ServiceUnitQuery();
            query.Select(query.ServiceUnitID, query.ServiceUnitName);
            query.Where
                (query.ServiceUnitName.Like(searchTextContain),
                 query.IsActive == true
                );
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboServiceUnitID.DataSource = dtb;
            cboServiceUnitID.DataBind();
        }

        protected void cboServiceUnitID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ServiceUnitName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ServiceUnitID"].ToString();
        }
    }
}