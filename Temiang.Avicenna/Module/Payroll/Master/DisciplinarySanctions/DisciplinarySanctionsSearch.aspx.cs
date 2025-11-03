using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Payroll.Master
{
    public partial class DisciplinarySanctionsSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.DisciplinarySanctions; //TODO: Isi ProgramID

            if (!IsPostBack)
                StandardReference.InitializeIncludeSpace(cboSREmploymentType, AppEnum.StandardReference.EmploymentType);
        }

        public override bool OnButtonOkClicked()
        {
            var query = new DisciplinarySanctionsQuery("a");
            var emptype = new AppStandardReferenceItemQuery("b");

            query.es.Top = AppSession.Parameter.MaxResultRecord;
            query.Select(
                query,
                emptype.ItemName.As("EmploymentTypeName")
                );
            query.InnerJoin(emptype).On
                   (
                       query.SREmploymentType == emptype.ItemID &
                       emptype.StandardReferenceID == AppEnum.StandardReference.EmploymentType
                   );
            query.OrderBy(query.SREmploymentType.Ascending, query.StartValue.Ascending, query.ValidFromDate.Ascending);
            if (!string.IsNullOrEmpty(cboSREmploymentType.SelectedValue))
            {
                query.Where(query.SREmploymentType == cboSREmploymentType.SelectedValue);
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}