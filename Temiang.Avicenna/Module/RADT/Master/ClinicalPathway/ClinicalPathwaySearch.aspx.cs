using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ClinicalPathwaySearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ClinicalPathway;
        }

        public override bool OnButtonOkClicked()
        {
            var query = new PathwayQuery("a");
            query.Select(query);
            query.OrderBy(query.StartingDate.Ascending, query.PathwayID.Ascending);

            if (!txtStartingDate.IsEmpty)
                query.Where(query.StartingDate == txtStartingDate.SelectedDate);
            if (!string.IsNullOrEmpty(txtPathwayID.Text))
            {
                if (cboFilterPathwayID.SelectedIndex == 1)
                    query.Where(query.PathwayID == txtPathwayID.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtPathwayID.Text);
                    query.Where(query.PathwayID.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtPathwayName.Text))
            {
                if (cboFilterPathwayName.SelectedIndex == 1)
                    query.Where(query.PathwayName == txtPathwayName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtPathwayName.Text);
                    query.Where(query.PathwayName.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtNotes.Text))
            {
                if (cboFilterNotes.SelectedIndex == 1)
                    query.Where(query.Notes == txtNotes.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtNotes.Text);
                    query.Where(query.Notes.Like(searchTextContain));
                }
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
