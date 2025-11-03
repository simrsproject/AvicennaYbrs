using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class RlMasterReportSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.RlMasterReport;//TODO: Isi ProgramID
        }

        public override bool OnButtonOkClicked()
        {
            var query = new RlMasterReportQuery();
            query.Select(
                query.RlMasterReportID,
                query.RlMasterReportNo,
                query.RlMasterReportName,
                query.IsActive,
                query.LastUpdateDateTime,
                query.LastUpdateByUserID
                );
            if (!string.IsNullOrEmpty(txtRlMasterReportNo.Text))
            {
                if (cboFilterRlMasterReportNo.SelectedIndex == 1)
                    query.Where(query.RlMasterReportNo == txtRlMasterReportNo.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtRlMasterReportNo.Text);
                    query.Where(query.RlMasterReportNo.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtRlMasterReportName.Text))
            {
                if (cboFilterRlMasterReportName.SelectedIndex == 1)
                    query.Where(query.RlMasterReportName == txtRlMasterReportName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtRlMasterReportName.Text);
                    query.Where(query.RlMasterReportName.Like(searchTextContain));
                }
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
