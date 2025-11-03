using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.QualityIndicator
{
    public partial class PpiProcedureSurveillanceUseOfAntibiotictDetail : BaseUserControl
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

            var itemId =
                (String) DataBinder.Eval(DataItem, PpiProcedureSurveillanceUseOfAntibioticsMetadata.ColumnNames.ItemID);
            var iq = new ItemQuery();
            iq.Where(iq.ItemID == itemId);
            cboItemID.DataSource = iq.LoadDataTable();
            cboItemID.DataBind();
            cboItemID.SelectedValue = itemId;

            object startDate = DataBinder.Eval(DataItem, PpiProcedureSurveillanceUseOfAntibioticsMetadata.ColumnNames.StartDate);
            if (startDate != null)
                txtStartDate.SelectedDate = (DateTime)DataBinder.Eval(DataItem, PpiProcedureSurveillanceUseOfAntibioticsMetadata.ColumnNames.StartDate);
            else
                txtStartDate.Clear();
            object endDate = DataBinder.Eval(DataItem, PpiProcedureSurveillanceUseOfAntibioticsMetadata.ColumnNames.StartDate);
            if (endDate != null)
                txtEndDate.SelectedDate = (DateTime)DataBinder.Eval(DataItem, PpiProcedureSurveillanceUseOfAntibioticsMetadata.ColumnNames.EndDate);
            else
                txtEndDate.Clear();
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (PpiProcedureSurveillanceUseOfAntibioticsCollection)Session["collPpiProcedureSurveillanceUseOfAntibiotics"];

                string itemId = cboItemID.SelectedValue;
                DateTime sdate = txtStartDate.SelectedDate ?? DateTime.Now;

                bool isExist = false;

                foreach (PpiProcedureSurveillanceUseOfAntibiotics item in coll)
                {
                    if (item.ItemID.Equals(itemId) && item.StartDate.Value.Date.Equals(sdate.Date))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage =
                        string.Format("Item : {0} with Start Date : {1} already exist", cboItemID.Text, sdate.ToString("dd-MMM-yyyy"));
                }
            }
        }

        protected void cboItemID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            var query = new ItemQuery("a");
            var ipm = new ItemProductMedicQuery("b");
            query.InnerJoin(ipm).On(query.ItemID == ipm.ItemID & ipm.IsAntibiotic == true);
            query.Where(
                query.Or(query.ItemID == e.Text,
                query.ItemName.Like(string.Format("%{0}%", e.Text))),
                query.IsActive == true
                );
            query.Select(query.ItemID, query.ItemName);

            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboItemID.DataSource = dtb;
            cboItemID.DataBind();
        }

        protected void cboItemID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.ItemItemDataBound(e);
        }

        #region Properties for return entry value

        public String ItemID
        {
            get { return cboItemID.SelectedValue; }
        }

        public String ItemName
        {
            get { return cboItemID.Text; }
        }

        public DateTime? StartDate
        {
            get { return txtStartDate.SelectedDate; }
        }

        public DateTime? EndDate
        {
            get { return txtEndDate.SelectedDate; }
        }

        #endregion
    }
}