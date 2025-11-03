using System;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna
{
    public partial class ChangePassword : BasePage
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            var appUser = AppSession.UserLogin;
            txtUserID.Text = appUser.UserID;
            txtUserName.Text = appUser.UserName;

            ProgramID = AppConstant.Program.ChangePassword;

            if (!string.IsNullOrEmpty(Page.Request.QueryString["msg"]))
            {
                ShowInformation("Your password is still the default password. Please change your password first.");
            }

            if (!IsPostBack)
            {
                if (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsUsingPasswordPolicy).ToLower() == "yes")
                {
                    trPasswordPolicy1.Visible = true;
                    trPasswordPolicy2.Visible = true;

                    var str = "<ul>";
                    if (AppParameter.GetParameterValue(AppParameter.ParameterItem.PasswordMinimumLength).ToInt() > 0)
                        str += "<li>" + string.Format("New password must greater or equal minimum of {0} character", AppParameter.GetParameterValue(AppParameter.ParameterItem.PasswordMinimumLength)) + "</li>";
                    if (AppParameter.GetParameterValue(AppParameter.ParameterItem.PasswordLowerCaseLength).ToInt() > 0)
                        str += "<li>" + string.Format("New password must contains minimum of {0} lower case character", AppParameter.GetParameterValue(AppParameter.ParameterItem.PasswordLowerCaseLength)) + "</li>";
                    if (AppParameter.GetParameterValue(AppParameter.ParameterItem.PasswordUpperCaseLength).ToInt() > 0)
                        str += "<li>" + string.Format("New password must contains minimum of {0} upper case or capital character", AppParameter.GetParameterValue(AppParameter.ParameterItem.PasswordUpperCaseLength)) + "</li>";
                    if (AppParameter.GetParameterValue(AppParameter.ParameterItem.PasswordNumericLength).ToInt() > 0)
                        str += "<li>" + string.Format("New password must contains minimum of {0} numeric character", AppParameter.GetParameterValue(AppParameter.ParameterItem.PasswordNumericLength)) + "</li>";
                    if (AppParameter.GetParameterValue(AppParameter.ParameterItem.PasswordNonAlphaLength).ToInt() > 0)
                        str += "<li>" + string.Format("New password must contains minimum of {0} non aplha or symbol character", AppParameter.GetParameterValue(AppParameter.ParameterItem.PasswordNonAlphaLength)) + "</li>";
                    lblPolicyInfo.Text = str + "</ul>";
                }
            }
        }

        private void ShowInformation(string information)
        {
            lblInformation.Text = information;
            pnlInformation.Visible = true;
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            if (txtNewPassword.Text != txtValidationNewPassword.Text)
            {
                ShowInformation("Validation password must equal with new password");
                return;
            }

            var entity = new AppUser();
            if (!entity.LoadByPrimaryKey(txtUserID.Text))
            {
                ShowInformation("User not found, please contact system admin");
                return;
            }

            if (!Encryptor.Encrypt(txtPassword.Text).Equals(entity.Password))
            {
                ShowInformation("Current password is not valid");
                return;
            }

            if (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsUsingPasswordPolicy).ToLower() == "yes")
            {
                var result = PasswordPolicy.IsValid(txtNewPassword.Text);
                if (result >= 0)
                {
                    switch (result)
                    {
                        case PasswordValidationResult.LessThanMinimum:
                            ShowInformation(string.Format("New password less than minimum of {0} character", AppParameter.GetParameterValue(AppParameter.ParameterItem.PasswordMinimumLength)));
                            return;
                        case PasswordValidationResult.LowerCaseNotFound:
                            ShowInformation(string.Format("New password must contains minimum of {0} lower case character", AppParameter.GetParameterValue(AppParameter.ParameterItem.PasswordLowerCaseLength)));
                            return;
                        case PasswordValidationResult.UpperCaseNotFound:
                            ShowInformation(string.Format("New password must contains minimum of {0} upper case or capital character", AppParameter.GetParameterValue(AppParameter.ParameterItem.PasswordUpperCaseLength)));
                            return;
                        case PasswordValidationResult.NumericNotFound:
                            ShowInformation(string.Format("New password must contains minimum of {0} numeric character", AppParameter.GetParameterValue(AppParameter.ParameterItem.PasswordNumericLength)));
                            return;
                        case PasswordValidationResult.NonAlphaNotFound:
                            ShowInformation(string.Format("New password must contains minimum of {0} non aplha or symbol character", AppParameter.GetParameterValue(AppParameter.ParameterItem.PasswordNonAlphaLength)));
                            return;
                    }
                }
            }

            if (!string.IsNullOrEmpty(txtPassword.Text)) entity.Password = Encryptor.Encrypt(txtNewPassword.Text);

            //Last Update Status
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = DateTime.Now;

            using (var trans = new esTransactionScope())
            {
                entity.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }

            AppSession.UserLogin.Password = entity.Password;

            ShowInformation("New password saved");
        }
    }
}
