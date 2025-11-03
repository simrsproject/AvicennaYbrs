using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class OperationalTimeSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.OperationalTime;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                RestoreValueFromCookie();
        }
        public override bool  OnButtonOkClicked()
        {
            var query = new OperationalTimeQuery();
            query.Select
                (
                    query.OperationalTimeID, 
                    query.OperationalTimeName, 
                    query.OperationalTimeBackcolor,
                    (query.StartTime1 + " - " + query.EndTime1).As("Time1"),
                    (query.StartTime2 + " - " + query.EndTime2).As("Time2"),
                    (query.StartTime3 + " - " + query.EndTime3).As("Time3"),
                    (query.StartTime4 + " - " + query.EndTime4).As("Time4"),
                    (query.StartTime5 + " - " + query.EndTime5).As("Time5")
                );
            if (!string.IsNullOrEmpty(txtOperationalTimeID.Text))
            {
                if (cboFilterOperationalTimeID.SelectedIndex == 1)
                    query.Where(query.OperationalTimeID == txtOperationalTimeID.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtOperationalTimeID.Text);
                    query.Where(query.OperationalTimeID.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtOperationalTimeName.Text))
            {
                if (cboFilterOperationalTimeName.SelectedIndex == 1)
                    query.Where(query.OperationalTimeName == txtOperationalTimeName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtOperationalTimeName.Text);
                    query.Where(query.OperationalTimeName.Like(searchTextContain));
                }
            }
            query.OrderBy(query.OperationalTimeID.Ascending);
            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}
