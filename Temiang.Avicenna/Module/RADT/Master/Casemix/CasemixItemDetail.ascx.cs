using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class CasemixItemDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            PopUpSearch.InitializeOnButtonClick(AppEnum.PopUpSearch.Item, txtAssesmentID);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                chkIsActive.Checked = true;
                return;
            }
            ViewState["IsNewRecord"] = false;
            txtAssesmentID.Text = (String)DataBinder.Eval(DataItem, PathwayItemMetadata.ColumnNames.ItemID);
            txtAssesmentName.Text = (String)DataBinder.Eval(DataItem, PathwayItemMetadata.ColumnNames.AssesmentName);
            txtNotes.Text = (String)DataBinder.Eval(DataItem, PathwayItemMetadata.ColumnNames.Notes);
            chkIsActive.Checked = Convert.ToBoolean(DataBinder.Eval(DataItem, PathwayItemMetadata.ColumnNames.IsActive));
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                string sessName = "collCasemixPathwayItemCollection";
                var coll = (CasemixPathwayItemCollection)Session[sessName];

                bool isExist = coll.Any(c => c.ItemID == txtAssesmentID.Text);
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Procedure Name : {0} already exist", txtAssesmentName.Text);
                }
            }
        }

        public String ItemID
        {
            get { return txtAssesmentID.Text; }
        }

        public String AssesmentName
        {
            get { return txtAssesmentName.Text; }
        }

        public String Notes
        {
            get { return txtNotes.Text; }
        }

        public Boolean IsActive
        {
            get { return chkIsActive.Checked; }
        }

        protected void txtAssesmentID_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAssesmentID.Text)) txtAssesmentName.Text = string.Empty;
            else
            {
                var item = new Item();
                item.LoadByPrimaryKey(txtAssesmentID.Text);
                txtAssesmentName.Text = item.ItemName;
            }
        }
    }
}