using System;
using System.Data;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna
{
    public partial class ResetPassword : BasePage
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
            {
                var appUser = AppSession.UserLogin;
                cboUserID.Text = appUser.UserID;
                txtUserName.Text = appUser.UserName;

                var usr = new AppUser();
                if (usr.LoadByPrimaryKey(appUser.UserID))
                    chkIsLocked.Checked = usr.IsLocked ?? false;
                else chkIsLocked.Checked = false;
            }

            ProgramID = AppConstant.Program.UserServiceunit;
        }

        private void ShowInformation(string information)
        {
            lblInformation.Text = information;
            pnlInformation.Visible = true;
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {

            if (!Page.IsValid) return;
            if (cboUserID.Text == "sci" && AppSession.UserLogin.UserID != "sci")
            {
                ShowInformation("You don't have authorization to reset this password");
                return;
            }
            if (txtNewPassword.Text != txtValidationNewPassword.Text)
            {
                ShowInformation("Validation password must equal with new password");
                return;
            }

            var entity = new AppUser();
            if (!entity.LoadByPrimaryKey(cboUserID.Text))
            {
                ShowInformation("User not found, please contact system admin");
                return;
            }

            if (txtNewPassword.Text != string.Empty)
                entity.Password = Encryptor.Encrypt(txtNewPassword.Text);

            entity.IsLocked = chkIsLocked.Checked;

            //Last Update Status
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = DateTime.Now;

            using (var trans = new esTransactionScope())
            {
                entity.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }

            //AppSession.UserLogin = entity;
            ShowInformation("New password and/or locked status saved");
        }

        protected void cboUserID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var appUser = new AppUser();
            appUser.LoadByPrimaryKey(cboUserID.SelectedValue);
            txtUserName.Text = appUser.UserName;
            chkIsLocked.Checked = appUser.IsLocked ?? false;

            txtNewPassword.Text = string.Empty;
            txtValidationNewPassword.Text = string.Empty;
        }

        protected void cboUserID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new AppUserQuery();
            query.es.Top = 20;
            query.Select
                (
                    query.UserID,
                    query.UserName
                );
            query.Where
                (
                    query.Or
                        (
                            query.UserID.Like(searchTextContain),
                            query.UserName.Like(searchTextContain)
                        )
                );

            cboUserID.DataSource = query.LoadDataTable();
            cboUserID.DataBind();
        }

        protected void cboUserID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["UserID"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["UserID"].ToString();
        }
    }
}
