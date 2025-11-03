using System;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna
{
    public partial class ChangePassCode : BasePageDialog
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            FooterVisible = false;

            if (!IsPostBack)
            {
                if (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsUsingPasswordPolicy).ToLower() == "yes")
                {
                    trPasswordPolicy1.Visible = true;
                    trPasswordPolicy2.Visible = true;

                    var str = "<ul>";
                    if (AppParameter.GetParameterValue(AppParameter.ParameterItem.PasswordMinimumLength).ToInt() > 0)
                        str += "<li>" + string.Format("New passcode must greater or equal minimum of {0} character", AppParameter.GetParameterValue(AppParameter.ParameterItem.PasswordMinimumLength)) + "</li>";
                    if (AppParameter.GetParameterValue(AppParameter.ParameterItem.PasswordLowerCaseLength).ToInt() > 0)
                        str += "<li>" + string.Format("New passcode must contains minimum of {0} lower case character", AppParameter.GetParameterValue(AppParameter.ParameterItem.PasswordLowerCaseLength)) + "</li>";
                    if (AppParameter.GetParameterValue(AppParameter.ParameterItem.PasswordUpperCaseLength).ToInt() > 0)
                        str += "<li>" + string.Format("New passcode must contains minimum of {0} upper case or capital character", AppParameter.GetParameterValue(AppParameter.ParameterItem.PasswordUpperCaseLength)) + "</li>";
                    if (AppParameter.GetParameterValue(AppParameter.ParameterItem.PasswordNumericLength).ToInt() > 0)
                        str += "<li>" + string.Format("New passcode must contains minimum of {0} numeric character", AppParameter.GetParameterValue(AppParameter.ParameterItem.PasswordNumericLength)) + "</li>";
                    if (AppParameter.GetParameterValue(AppParameter.ParameterItem.PasswordNonAlphaLength).ToInt() > 0)
                        str += "<li>" + string.Format("New passcode must contains minimum of {0} non aplha or symbol character", AppParameter.GetParameterValue(AppParameter.ParameterItem.PasswordNonAlphaLength)) + "</li>";
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
                ShowInformation("Validation passcode must equal with new passcode");
                return;
            }

            var prgSign = new AppProgramSignature();
            if (!prgSign.LoadByPrimaryKey("ALL", "ALL"))
            {
                //Add
                prgSign = new AppProgramSignature();
                prgSign.ProgramID = "ALL";
                prgSign.UserID = "ALL";
                prgSign.Signature = String.Empty;
                prgSign.Save();
            }

            if (!(string.IsNullOrWhiteSpace(prgSign.Signature) && txtPassword.Text == "987"))
            {
                if (!string.IsNullOrWhiteSpace(prgSign.Signature) &&
                    !Encryptor.Encrypt(txtPassword.Text).Equals(prgSign.Signature))
                {
                    ShowInformation("Current passcode is not valid");
                    return;
                }
            }

            if (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsUsingPasswordPolicy).ToLower() == "yes")
            {
                var result = PasswordPolicy.IsValid(txtNewPassword.Text);
                if (result >= 0)
                {
                    switch (result)
                    {
                        case PasswordValidationResult.LessThanMinimum:
                            ShowInformation(string.Format("New passcode less than minimum of {0} character", AppParameter.GetParameterValue(AppParameter.ParameterItem.PasswordMinimumLength)));
                            return;
                        case PasswordValidationResult.LowerCaseNotFound:
                            ShowInformation(string.Format("New passcode must contains minimum of {0} lower case character", AppParameter.GetParameterValue(AppParameter.ParameterItem.PasswordLowerCaseLength)));
                            return;
                        case PasswordValidationResult.UpperCaseNotFound:
                            ShowInformation(string.Format("New passcode must contains minimum of {0} upper case or capital character", AppParameter.GetParameterValue(AppParameter.ParameterItem.PasswordUpperCaseLength)));
                            return;
                        case PasswordValidationResult.NumericNotFound:
                            ShowInformation(string.Format("New passcode must contains minimum of {0} numeric character", AppParameter.GetParameterValue(AppParameter.ParameterItem.PasswordNumericLength)));
                            return;
                        case PasswordValidationResult.NonAlphaNotFound:
                            ShowInformation(string.Format("New passcode must contains minimum of {0} non aplha or symbol character", AppParameter.GetParameterValue(AppParameter.ParameterItem.PasswordNonAlphaLength)));
                            return;
                    }
                }
            }

            prgSign.Signature = Encryptor.Encrypt(txtNewPassword.Text);
            prgSign.Save();

            ShowInformation("New passcode saved");
        }
    }
}
