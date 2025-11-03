using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.ControlPanel.PrinterManagement
{
    public partial class UserHostPrinterOtherDetail : BaseUserControl
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
                cboPrinterID.Items.Clear();
                cboPrinterID.Text = string.Empty;
                cboProgramID.Items.Clear();
                cboProgramID.Text = string.Empty;
                return;
            }
            ViewState["IsNewRecord"] = false;
            string programID = (String) DataBinder.Eval(DataItem, UserHostPrinterOtherMetadata.ColumnNames.ProgramID);
            string printerID = (String) DataBinder.Eval(DataItem, UserHostPrinterOtherMetadata.ColumnNames.PrinterID);

            PrinterQuery printerQuery = new PrinterQuery();
            printerQuery.Where(printerQuery.PrinterID == printerID);
            printerQuery.Select(printerQuery.PrinterID, printerQuery.PrinterName);
            cboPrinterID.DataSource = printerQuery.LoadDataTable();
            cboPrinterID.DataBind();
            ComboBox.SelectedValue(cboPrinterID, printerID);

            AppProgramQuery progQuery = new AppProgramQuery();
            progQuery.Where(progQuery.ProgramID == programID);
            progQuery.Select(progQuery.ProgramID, progQuery.ProgramName);
            cboProgramID.DataSource = progQuery.LoadDataTable();
            cboProgramID.DataBind();
            ComboBox.SelectedValue(cboProgramID, programID);


        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                UserHostPrinterOtherCollection coll =
                    (UserHostPrinterOtherCollection)Session["collUserHostPrinterOther"];

                string id = cboProgramID.SelectedValue;
                bool isExist = false;
                foreach (UserHostPrinterOther item in coll)
                {
                    if (item.ProgramID.Equals(id))
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
        public String ProgramID
        {
            get { return cboProgramID.SelectedValue; }
        }
        public String PrinterID
        {
            get { return cboPrinterID.SelectedValue; }
        }
        public String ProgramName
        {
            get { return cboProgramID.Text; }
        }
        public String PrinterName
        {
            get { return cboPrinterID.Text; }
        }
        #endregion
        protected void cboPrinterID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new PrinterQuery();
            query.es.Top = 10;
            query.Select
                (
                    query.PrinterID,
                    query.PrinterName
                );
            query.Where
                (
                    query.Or
                        (
                            query.PrinterID.Like(searchTextContain),
                            query.PrinterName.Like(searchTextContain)
                        )
                );

            cboPrinterID.DataSource = query.LoadDataTable();
            cboPrinterID.DataBind();
        }

        protected void cboPrinterID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["PrinterName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PrinterID"].ToString();
        }

        protected void cboProgramID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new AppProgramQuery();
            query.es.Top = 10;
            query.Select
                (
                    query.ProgramID,
                    query.ProgramName
                );
            query.Where
                (
                    query.Or
                        (
                            query.ProgramID.Like(searchTextContain),
                            query.ProgramName.Like(searchTextContain)
                        ),

                        query.ProgramType.In("RSLIP", "RPT", "XML"),
                        query.IsVisible == true,
                        query.IsDiscontinue == false
                );

            cboProgramID.DataSource = query.LoadDataTable();
            cboProgramID.DataBind();
        }

        protected void cboProgramID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ProgramName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ProgramID"].ToString();
        }
    }
}
