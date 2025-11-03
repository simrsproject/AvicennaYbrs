using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.Cssd.Master
{
    public partial class ItemServiceUnitDetail : BaseUserControl
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
                return;
            }
            ViewState["IsNewRecord"] = false;
            cboServiceUnitID.Enabled = false;
            PopulateCboServiceUnitID(cboServiceUnitID, (String)DataBinder.Eval(DataItem, "ServiceUnitName"));
        }

        #region ComboBox ServiceUnitID
        protected void cboServiceUnitID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulateCboServiceUnitID((RadComboBox)sender, e.Text);
        }

        private void PopulateCboServiceUnitID(RadComboBox comboBox, string textSearch)
        {
            string searchTextContain = string.Format("%{0}%", textSearch);
            var query = new ServiceUnitQuery("a");
            query.Where(
                query.SRRegistrationType.In(AppConstant.RegistrationType.InPatient,
                                            AppConstant.RegistrationType.OutPatient,
                                            AppConstant.RegistrationType.EmergencyPatient,
                                            AppConstant.RegistrationType.ClusterPatient,
                                            AppConstant.RegistrationType.MedicalCheckUp), 
                query.IsActive == true,
                query.ServiceUnitName.Like(searchTextContain));
            query.Select(query.ServiceUnitID, query.ServiceUnitName);

            query.es.Top = 10;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.SelectedValue = dtb.Rows[0]["ServiceUnitID"].ToString();
            }
        }

        protected void cboServiceUnitID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ServiceUnitName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ServiceUnitID"].ToString();
        }

        #endregion

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (CssdItemBalanceCollection)Session["collCssdItemBalance"];

                string id = cboServiceUnitID.SelectedValue;
                bool isExist = false;
                foreach (CssdItemBalance row in coll)
                {
                    if (row.ServiceUnitID.Equals(id))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("ID: {0} has exist", id);
                }
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
        #endregion
    }
}