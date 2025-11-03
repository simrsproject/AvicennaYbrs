using System;

namespace Temiang.Avicenna.CustomControl
{
    public partial class ItemCtl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public String ItemID
        {
            get { return txtItemID.Text; }
            set { txtItemID.Text = value; }
        }

        public String ItemSubGroupID
        {
            get { return cboItemSubGroupID.SelectedValue; }
            set { cboItemSubGroupID.SelectedValue = value; }
        }

        public String ItemName1
        {
            get { return txtItemName1.Text; }
            set { txtItemName1.Text = value; }
        }
        public String ItemName2
        {
            get { return txtItemName2.Text; }
            set { txtItemName2.Text = value; }
        }

        public String AssetBookID
        {
            get { return txtAssetBookID.Text; }
            set { txtAssetBookID.Text = value; }
        }

        public Boolean IsActive
        {
            get { return chkIsActive.Checked; }
            set { chkIsActive.Checked = value; }
        }

        public String Notes
        {
            get { return txtNotes.Text; }
            set { txtNotes.Text = value; }
        } 
    }
}