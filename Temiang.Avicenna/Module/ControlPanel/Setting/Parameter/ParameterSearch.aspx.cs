using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.ControlPanel.Setting
{
    public partial class ParameterSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.ParameterSetting;
        }

        public override bool OnButtonOkClicked()
        {
            var query = new AppParameterQuery();
            if (!string.IsNullOrEmpty(txtParameterID.Text))
            {
                if (cboFilterParameterID.SelectedIndex == 1)
                    query.Where(query.ParameterID == txtParameterID.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtParameterID.Text);
                    query.Where(query.ParameterID.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtParameterName.Text))
            {
                if (cboFilterParameterName.SelectedIndex == 1)
                    query.Where(query.ParameterName == txtParameterName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtParameterName.Text);
                    query.Where(query.ParameterName.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtParameterValue.Text))
            {
                if (cboFilterParameterValue.SelectedIndex == 1)
                    query.Where(query.ParameterValue == txtParameterValue.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtParameterValue.Text);
                    query.Where(query.ParameterValue.Like(searchTextContain));
                }
            }
            query.Where(query.IsUsedBySystem == false);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset
            return true;
        }
    }
}