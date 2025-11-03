using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ServiceUnitAutoBillItemDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            trIsUsingGenerateOnSchedule.Visible = !AppSession.Application.IsHisMode;

            PopUpSearch.InitializeOnButtonClick(AppEnum.PopUpSearch.ServiceUnitAutoBillItem, txtItemID);

            if (cboSRItemUnit.Items.Count == 0)
                StandardReference.InitializeIncludeSpace(cboSRItemUnit, AppEnum.StandardReference.ItemUnit);

            // Initialize GenerateOnClassIDs
            var bed = new BedQuery("b");
            var room = new ServiceRoomQuery("r");
            room.InnerJoin(bed).On(room.RoomID == bed.RoomID);
            var classQ = new ClassQuery("c");
            room.InnerJoin(classQ).On(bed.ClassID == classQ.ClassID);

            var suid = (Helper.FindControlRecursive(this.Page, "txtServiceUnitID") as RadTextBox).Text;

            room.Where(room.ServiceUnitID == suid);
            room.es.Distinct = true;
            room.Select(bed.ClassID, classQ.ClassName);
            var dtbGenerateOnClassIDs = room.LoadDataTable();
            dtbGenerateOnClassIDs.Columns.Add("IsSelected", typeof(bool));

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                txtQuantity.Value = 1.00;
                chkIsActive.Checked = true;
                txtGenerateOnDayStart.Value = 0;
                txtGenerateOnDayEnd.Value = 0;
                chklGenerateOnClassIDs.DataSource = dtbGenerateOnClassIDs;
                chklGenerateOnClassIDs.DataBind();
                return;
            }
            ViewState["IsNewRecord"] = false;

            txtItemID.Text = (String)DataBinder.Eval(DataItem, ServiceUnitAutoBillItemMetadata.ColumnNames.ItemID);
            Item i = new Item();
            if (i.LoadByPrimaryKey(txtItemID.Text))
                lblItemName.Text = i.ItemName;

            txtQuantity.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ServiceUnitAutoBillItemMetadata.ColumnNames.Quantity));
            cboSRItemUnit.SelectedValue = (string)DataBinder.Eval(DataItem, ServiceUnitAutoBillItemMetadata.ColumnNames.SRItemUnit);
            chkIsAutoPayment.Checked = (bool)DataBinder.Eval(DataItem, ServiceUnitAutoBillItemMetadata.ColumnNames.IsAutoPayment);
            chkIsActive.Checked = (bool)DataBinder.Eval(DataItem, ServiceUnitAutoBillItemMetadata.ColumnNames.IsActive);
            chkIsGenerateOnRegistration.Checked = (bool)DataBinder.Eval(DataItem, ServiceUnitAutoBillItemMetadata.ColumnNames.IsGenerateOnRegistration);
            chkIsGenerateOnNewRegistration.Checked = (bool)DataBinder.Eval(DataItem, ServiceUnitAutoBillItemMetadata.ColumnNames.IsGenerateOnNewRegistration);
            chkIsGenerateOnReferral.Checked = (bool)DataBinder.Eval(DataItem, ServiceUnitAutoBillItemMetadata.ColumnNames.IsGenerateOnReferral);
            chkIsGenerateOnFirstRegistration.Checked = (bool)DataBinder.Eval(DataItem, ServiceUnitAutoBillItemMetadata.ColumnNames.IsGenerateOnFirstRegistration);

            chkIsGenerateOnSchedule.Checked = (bool)DataBinder.Eval(DataItem, ServiceUnitAutoBillItemMetadata.ColumnNames.IsGenerateOnSchedule);
            txtGenerateOnDayStart.Value = DataBinder.Eval(DataItem, ServiceUnitAutoBillItemMetadata.ColumnNames.GenerateOnDayStart).ToInt();
            txtGenerateOnDayEnd.Value = DataBinder.Eval(DataItem, ServiceUnitAutoBillItemMetadata.ColumnNames.GenerateOnDayEnd).ToInt();

            // Selected GenerateOnClassIDs
            var generateOnClassIDs = (string)DataBinder.Eval(DataItem, ServiceUnitAutoBillItemMetadata.ColumnNames.GenerateOnClassIDs);
            generateOnClassIDs = generateOnClassIDs.Trim() + ";";
            foreach (DataRow row in dtbGenerateOnClassIDs.Rows)
            {
                row["IsSelected"] = generateOnClassIDs.Contains(row["ClassID"].ToString().Trim() + ";");
            }

            chklGenerateOnClassIDs.DataSource = dtbGenerateOnClassIDs;
            chklGenerateOnClassIDs.DataBind();
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                ServiceUnitAutoBillItemCollection coll = (ServiceUnitAutoBillItemCollection)Session["collServiceUnitAutoBillItem"];

                string itemID = txtItemID.Text;
                bool isExist = false;
                foreach (ServiceUnitAutoBillItem item in coll)
                {
                    if (item.ItemID.Equals(itemID))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Item ID : {0} already exist", itemID);
                }
            }
        }

        #region Properties for return entry value

        public String ItemID
        {
            get { return txtItemID.Text; }
        }

        public String ItemName
        {
            get { return lblItemName.Text; }
        }

        public Decimal Quantity
        {
            get { return Convert.ToDecimal(txtQuantity.Value); }
        }

        public string SRItemUnit
        {
            get { return cboSRItemUnit.SelectedValue; }
        }

        public string ItemUnit
        {
            get { return cboSRItemUnit.SelectedItem.Text; }
        }

        public Boolean IsAutoPayment
        {
            get { return chkIsAutoPayment.Checked; }
        }

        public Boolean IsActive
        {
            get { return chkIsActive.Checked; }
        }

        public Boolean IsGenerateOnRegistration
        {
            get { return chkIsGenerateOnRegistration.Checked; }
        }

        public Boolean IsGenerateOnNewRegistration
        {
            get { return chkIsGenerateOnNewRegistration.Checked; }
        }

        public Boolean IsGenerateOnReferral
        {
            get { return chkIsGenerateOnReferral.Checked; }
        }

        public Boolean IsGenerateOnFirstRegistration
        {
            get { return chkIsGenerateOnFirstRegistration.Checked; }
        }

        public Boolean IsGenerateOnSchedule
        {
            get { return chkIsGenerateOnSchedule.Checked; }
        }

        public int GenerateOnDayStart
        {
            get { return txtGenerateOnDayStart.Value.ToInt(); }
        }
        public int GenerateOnDayEnd
        {
            get { return txtGenerateOnDayEnd.Value.ToInt(); }
        }
        public string GenerateOnClassIDs
        {
            get { return string.Join(";", chklGenerateOnClassIDs.SelectedValues); }
        }
        #endregion

        protected void txtItemID_TextChanged(object sender, EventArgs e)
        {
            Item i = new Item();
            if (i.LoadByPrimaryKey(txtItemID.Text))
            {
                lblItemName.Text = i.ItemName;
                if (i.SRItemType == BusinessObject.Reference.ItemType.Medical)
                {
                    ItemProductMedic ipmd = new ItemProductMedic();
                    if (ipmd.LoadByPrimaryKey(txtItemID.Text))
                        cboSRItemUnit.SelectedValue = ipmd.SRItemUnit;
                }
                else if (i.SRItemType == BusinessObject.Reference.ItemType.NonMedical)
                {
                    ItemProductNonMedic ipmd = new ItemProductNonMedic();
                    if (ipmd.LoadByPrimaryKey(txtItemID.Text))
                        cboSRItemUnit.SelectedValue = ipmd.SRItemUnit;
                }
                else if (i.SRItemType == BusinessObject.Reference.ItemType.Service)
                {
                    ItemService ipmd = new ItemService();
                    if (ipmd.LoadByPrimaryKey(txtItemID.Text))
                        cboSRItemUnit.SelectedValue = ipmd.SRItemUnit;
                }
                else if (i.SRItemType == BusinessObject.Reference.ItemType.Diagnostic)
                {
                    ItemDiagnostic ipmd = new ItemDiagnostic();
                    if (ipmd.LoadByPrimaryKey(txtItemID.Text))
                        cboSRItemUnit.SelectedValue = "";
                }
            }
            else
            {
                txtItemID.Text = string.Empty;
                lblItemName.Text = string.Empty;
            }
        }
    }
}
