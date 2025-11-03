using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Inventory.Master
{
    public partial class LocationTemplateSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.LocationTemplate;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                RestoreValueFromCookie();
        }
        public override bool OnButtonOkClicked()
        {
            var query = new LocationTemplateQuery("a");
            var location = new LocationQuery("b");
            var unitLocation = new ServiceUnitLocationQuery("c");
            var usrUnit = new AppUserServiceUnitQuery("d");

            query.InnerJoin(location).On(location.LocationID == query.LocationID);
            query.InnerJoin(unitLocation).On(unitLocation.LocationID == query.LocationID);
            query.InnerJoin(usrUnit).On(usrUnit.UserID == AppSession.UserLogin.UserID &&
                                        usrUnit.ServiceUnitID == unitLocation.ServiceUnitID);

            query.Select(query.TemplateNo, query.TemplateName, query.LocationID, location.LocationName,
                query.IsActive);

            query.OrderBy(query.TemplateNo.Ascending);

            if (!string.IsNullOrEmpty(txtTemplateName.Text))
            {
                if (cboFilterTemplateName.SelectedIndex == 1)
                    query.Where(query.TemplateName == txtTemplateName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtTemplateName.Text);
                    query.Where(query.TemplateName.Like(searchTextContain));
                }
            }
            
            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}