using System;
using System.Data;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Payroll.Master
{
    public partial class SalaryScaleSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.SalaryScale;

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSREmploymentType, AppEnum.StandardReference.EmploymentType);
                StandardReference.InitializeIncludeSpace(cboSRProfessionGroup, AppEnum.StandardReference.ProfessionGroup);
                StandardReference.InitializeIncludeSpace(cboSREducationGroup, AppEnum.StandardReference.EducationGroup);
            }
        }

        public override bool OnButtonOkClicked()
        {
            var query = new SalaryScaleQuery("a");
            var grade = new PositionGradeQuery("b");
            var etype = new AppStandardReferenceItemQuery("c");
            var pgroup = new AppStandardReferenceItemQuery("d");
            var egroup = new AppStandardReferenceItemQuery("e");

            query.es.Top = AppSession.Parameter.MaxResultRecord;
            query.Select(
                query.SalaryScaleID,
                query.SalaryScaleCode,
                query.SalaryScaleName,
                query.PositionGradeID,
                grade.PositionGradeCode,
                grade.PositionGradeName,
                query.SREmploymentType,
                etype.ItemName.As("EmploymentTypeName"),
                query.SRProfessionGroup,
                pgroup.ItemName.As("ProfessionGroupName"),
                query.SREducationGroup,
                egroup.ItemName.As("EducationGroupName"),
                query.Notes,
                query.IsActive,
                query.LastUpdateDateTime,
                query.LastUpdateByUserID
                        );
            query.InnerJoin(grade).On(query.PositionGradeID == grade.PositionGradeID);
            query.InnerJoin(etype).On(etype.StandardReferenceID == AppEnum.StandardReference.EmploymentType.ToString() && etype.ItemID == query.SREmploymentType);
            query.InnerJoin(pgroup).On(pgroup.StandardReferenceID == AppEnum.StandardReference.ProfessionGroup.ToString() && pgroup.ItemID == query.SRProfessionGroup);
            query.InnerJoin(egroup).On(egroup.StandardReferenceID == AppEnum.StandardReference.EducationGroup.ToString() && egroup.ItemID == query.SREducationGroup);

            if (!string.IsNullOrEmpty(cboPositionGradeID.SelectedValue))
                query.Where(query.PositionGradeID == cboPositionGradeID.SelectedValue.ToInt());
            if (!string.IsNullOrEmpty(cboSREmploymentType.SelectedValue))
                query.Where(query.SREmploymentType == cboSREmploymentType.SelectedValue);
            if (!string.IsNullOrEmpty(cboSRProfessionGroup.SelectedValue))
                query.Where(query.SRProfessionGroup == cboSRProfessionGroup.SelectedValue);
            if (!string.IsNullOrEmpty(cboSREducationGroup.SelectedValue))
                query.Where(query.SREducationGroup == cboSREducationGroup.SelectedValue);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }

        protected void cboPositionGradeID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulatecboPositionGradeID((RadComboBox)sender, e.Text);
        }
        private void PopulatecboPositionGradeID(RadComboBox comboBox, string textSearch)
        {
            string searchTextContain = string.Format("%{0}%", textSearch);
            var query = new PositionGradeQuery("a");

            query.Where(query.PositionGradeName.Like(searchTextContain));

            query.Select(
                query.PositionGradeID,
                query.PositionGradeCode,
                query.PositionGradeName
                );

            query.es.Top = 30;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.SelectedValue = dtb.Rows[0]["PositionGradeID"].ToString();
            }
        }

        protected void cboPositionGradeID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["PositionGradeName"].ToString().Trim();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PositionGradeID"].ToString();
        }
    }
}