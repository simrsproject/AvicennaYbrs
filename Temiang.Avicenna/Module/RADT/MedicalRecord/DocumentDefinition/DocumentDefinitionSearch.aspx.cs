using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.MedicalRecord
{
    public partial class DocumentDefinitionSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.DocumentDefinition; //TODO: Isi ProgramID
        }

        public override bool OnButtonOkClicked()
        {
            var query = new DocumentDefinitionQuery("a");
            var files = new AppStandardReferenceItemQuery("c");
            var department = new DepartmentQuery("b");

            query.LeftJoin(department).On(query.DepartmentID == department.DepartmentID);
            query.InnerJoin(files).On
                (
                    query.SRFilesAnalysis == files.ItemID &
                    files.StandardReferenceID == "FilesAnalysis"
                );

            query.es.Top = AppSession.Parameter.MaxResultRecord;
            query.Select(
                            query.DocumentDefinitionID,
                            query.DepartmentID,
                            department.DepartmentName,
                            files.ItemName.As("NameFilesType"),
                            query.LastUpdateDateTime,
                            query.LastUpdateByUserID,
                            query.IsActive
                        );

            if (!string.IsNullOrEmpty(txtDepartmentName.Text))
            {
                if (cboFilterDepartmentName.SelectedIndex == 1)
                    query.Where(department.DepartmentName == txtDepartmentName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtDepartmentName.Text);
                    query.Where(department.DepartmentName.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtFilesType.Text))
            {
                if (cboFilterFilesType.SelectedIndex == 1)
                    query.Where(files.ItemName == txtFilesType.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtFilesType.Text);
                    query.Where(files.ItemName.Like(searchTextContain));
                }
            }
            query.OrderBy(department.DepartmentName.Ascending);
            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
