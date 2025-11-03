using System;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Nutrient.Initialization
{
    public partial class MenuInitializationSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = Request.QueryString["ext"] == "0" ? AppConstant.Program.MenuInitialization : AppConstant.Program.MenuExtraInitialization;
        }

        public override bool OnButtonOkClicked()
        {
            var query = new MenuSettingQuery("a");
            var version = new MenuVersionQuery("b");
            query.InnerJoin(version).On(query.VersionID == version.VersionID);
            query.Select
                (
                    query.StartingDate,
                    query.VersionID,
                    version.VersionID,
                    query.SeqNo
                );
            if (Request.QueryString["ext"] == "0")
                query.Where(query.IsExtra == false);
            else
                query.Where(query.IsExtra == true);
            query.OrderBy(query.StartingDate.Descending);

            if (!txtStartingDate.IsEmpty)
                query.Where(query.StartingDate == txtStartingDate.SelectedDate);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
