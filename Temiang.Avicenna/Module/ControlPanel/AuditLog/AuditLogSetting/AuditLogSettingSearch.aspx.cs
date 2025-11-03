using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.ControlPanel
{
    public partial class AuditLogSettingSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.AuditLogSetting;
        }

        public override bool  OnButtonOkClicked()
        {
            var query = new AuditLogSettingQuery();
            if (!string.IsNullOrEmpty(txtTableName.Text))
            {
                if (cboFilterTableName.SelectedIndex == 1)
                    query.Where(query.TableName == txtTableName.Text);
                else
                {
                    string searchText = string.Format("%{0}%", txtTableName.Text);
                    query.Where(query.TableName.Like(searchText));
                }
            }

            query.Where(query.IsAuditLog == chkIsAuditLog.Checked);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
