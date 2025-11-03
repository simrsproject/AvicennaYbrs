using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.RADT.Master.Referralv2
{
    public partial class ReferralSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.Referral;

            if (!IsPostBack)
            {
                cboReferenceID.Items.Add(new RadComboBoxItem("", ""));
                cboReferenceID.Items.Add(new RadComboBoxItem("RS", "RS"));
                cboReferenceID.Items.Add(new RadComboBoxItem("RSLAIN", "RSLAIN"));
                cboReferenceID.Items.Add(new RadComboBoxItem("BIDAN", "BIDAN"));
                cboReferenceID.Items.Add(new RadComboBoxItem("PUSKESMAS", "PUSKESMAS"));
                cboReferenceID.Items.Add(new RadComboBoxItem("FASKES", "FASKES"));
                cboReferenceID.Items.Add(new RadComboBoxItem("FASKESLAIN", "FASKESLAIN"));
                cboReferenceID.Items.Add(new RadComboBoxItem("DUKUN", "DUKUN"));
                cboReferenceID.Items.Add(new RadComboBoxItem("DATANGSENDIRI", "DATANGSENDIRI"));
            }
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                RestoreValueFromCookie();
        }
        public override bool OnButtonOkClicked()
        {
            var query = new AppStandardReferenceItemQuery("a");
            query.Where(query.StandardReferenceID == "ReferralGroup");

            query.Select
                (
                    query.ItemID,
                    query.ItemName,
                    query.Note,
                    query.ReferenceID,
                    query.IsActive
                );

            if (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsCreateReferralGroupIdAutomatic) == "Yes")
                query.OrderBy(query.ItemID.Ascending);
            else
                query.OrderBy(query.ItemID.Descending);

            if (!string.IsNullOrEmpty(txtItemID.Text))
            {
                if (cboFilterItemID.SelectedIndex == 1)
                    query.Where(query.ItemID == txtItemID.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtItemID.Text);
                    query.Where(query.ItemID.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtItemName.Text))
            {
                if (cboFilterItemName.SelectedIndex == 1)
                    query.Where(query.ItemName == txtItemName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtItemName.Text);
                    query.Where(query.ItemName.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtNote.Text))
            {
                if (cboFilterNote.SelectedIndex == 1)
                    query.Where(query.Note == txtNote.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtNote.Text);
                    query.Where(query.Note.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(cboReferenceID.SelectedValue))
                query.Where(query.ReferenceID == cboReferenceID.SelectedValue);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}