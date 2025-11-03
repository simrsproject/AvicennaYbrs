using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class BpjsPackageTariffDetail : BaseUserControl
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
                cboClassID.Enabled = true;
                var coll = new AppStandardReferenceItemCollection();
                coll.Query.Where(coll.Query.StandardReferenceID == "ClassRL",
                                 coll.Query.ItemID.In("ClassRL-002", "ClassRL-003", "ClassRL-004", "ClassRL-005"),
                                 coll.Query.IsActive == true);
                coll.Query.OrderBy(coll.Query.Note.Ascending);
                coll.LoadAll();

                cboClassID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (AppStandardReferenceItem c in coll)
                {
                    cboClassID.Items.Add(new RadComboBoxItem(c.ItemName, c.ItemID));
                }

                ViewState["IsNewRecord"] = true;

                return;
            }
            ViewState["IsNewRecord"] = false;

            cboClassID.Enabled = false;
            var clQ = new AppStandardReferenceItemQuery();
            clQ.Select(clQ.ItemID, clQ.ItemName);
            clQ.Where(clQ.StandardReferenceID == "ClassRL",
                      clQ.ItemID == (String)DataBinder.Eval(DataItem, BpjsPackageTariffMetadata.ColumnNames.ClassID));
            var colll = new AppStandardReferenceItemCollection();
            colll.Load(clQ);
            foreach (AppStandardReferenceItem c in colll)
            {
                cboClassID.Items.Add(new RadComboBoxItem(c.ItemName, c.ItemID));
            }

            txtStartingDate.SelectedDate = Convert.ToDateTime(DataBinder.Eval(DataItem, BpjsPackageTariffMetadata.ColumnNames.StartingDate));
            cboClassID.SelectedValue = (String)DataBinder.Eval(DataItem, BpjsPackageTariffMetadata.ColumnNames.ClassID);
            txtPrice.Value = Convert.ToDouble(DataBinder.Eval(DataItem, BpjsPackageTariffMetadata.ColumnNames.Price));
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (BpjsPackageTariffCollection)Session["collBpjsPackageTariff"];

                DateTime sValue = txtStartingDate.SelectedDate ?? DateTime.Now;
                bool isExist = false;
                foreach (BpjsPackageTariff item in coll)
                {
                    if (item.StartingDate.Equals(sValue) && item.ClassID.Equals(cboClassID.SelectedValue))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Starting Date: {0} and Class: {1} has exist", sValue, cboClassID.Text);
                }
            }
        }
        #region Properties for return entry value

        public DateTime StartingDate
        {
            get { return Convert.ToDateTime(txtStartingDate.SelectedDate); }
        }

        public String ClassId
        {
            get { return cboClassID.SelectedValue; }
        }

        public String ClassName
        {
            get { return cboClassID.Text; }
        }

        public Decimal Price
        {
            get { return Convert.ToDecimal(txtPrice.Value); }
        }

        #endregion
    }
}