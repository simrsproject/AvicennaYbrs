using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ClassSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.ServiceClass;
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
            var item = new AppStandardReferenceItemQuery("b");
            query.InnerJoin(item).On(query.SRClassRL == item.ItemID);
            query.Select
                (
                    query.ClassID,
                    query.ClassName,
                    query.ShortName,
                    item.ItemName,
                    query.MarginPercentage,
                    query.Margin2Percentage,
                    query.DepositAmount,
                    query.ClassSeq,
                    query.IsInPatientClass,
                    query.IsTariffClass,
                    query.IsActive
                );
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