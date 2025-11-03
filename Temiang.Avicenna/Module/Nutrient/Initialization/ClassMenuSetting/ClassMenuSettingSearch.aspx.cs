using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Nutrient.Initialization
{
    public partial class ClassMenuSettingSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.ClassMenuSetting;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                RestoreValueFromCookie();
        }
        public override bool OnButtonOkClicked()
        {
            var query = new ClassQuery("a");
            var detail = new ClassMenuSettingQuery("b");
            query.LeftJoin(detail).On(query.ClassID == detail.ClassID);
            query.Select
                (
                    query.ClassID,
                    query.ClassName,
                    detail.IsOptional
                );
            query.Where(query.IsActive == true, query.IsInPatientClass == true);
            if (!string.IsNullOrEmpty(txtClassID.Text))
            {
                if (cboFilterClassID.SelectedIndex == 1)
                    query.Where(query.ClassID == txtClassID.Text);
                else
                {
                    string searchText = string.Format("%{0}%", txtClassID.Text);
                    query.Where(query.ClassID.Like(searchText));
                }
            }
            if (!string.IsNullOrEmpty(txtClassName.Text))
            {
                if (cboFilterClassName.SelectedIndex == 1)
                    query.Where(query.ClassName == txtClassName.Text);
                else
                {
                    string searchText = string.Format("%{0}%", txtClassName.Text);
                    query.Where(query.ClassName.Like(searchText));
                }
            }

            query.OrderBy(query.ClassID.Ascending);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}
