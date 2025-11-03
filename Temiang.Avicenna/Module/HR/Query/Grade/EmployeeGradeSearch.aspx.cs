using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.Query
{
    public partial class EmployeeGradeSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.QueryEmployeeGrade;//TODO: Isi ProgramID
        }

        public override bool OnButtonOkClicked()
        {
            PositionGradeQuery grade = new PositionGradeQuery("c");
            PersonalInfoQuery personal = new PersonalInfoQuery("b");
            var query = new EmployeePositionGradeQuery("a");

            query.Select
                (
                   query.EmployeePositionGradeID,
                   query.PersonID,
                   personal.EmployeeNumber,
                   personal.EmployeeName,
                   grade.PositionGradeCode,
                   grade.PositionGradeName,
                   grade.RankName,
                   query.GradeYear,
                   query.ValidFrom,
                   query.LastUpdateByUserID,
                   query.LastUpdateDateTime
                );

            query.InnerJoin(personal).On(query.PersonID == personal.PersonID);
            query.InnerJoin(grade).On(query.PositionGradeID == grade.PositionGradeID);
            query.OrderBy(query.PersonID.Ascending); //TODO: Betulkan ordernya

            if (!string.IsNullOrEmpty(txtEmployeeNo.Text))
            {
                if (cboFilterEmployeeNumber.SelectedIndex == 1)
                    query.Where(personal.EmployeeNumber == txtEmployeeNo.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtEmployeeNo.Text);
                    query.Where(personal.EmployeeNumber.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtFirstName.Text))
            {
                if (cboFirstName.SelectedIndex == 1)
                    query.Where(personal.FirstName == txtFirstName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtFirstName.Text);
                    query.Where(personal.FirstName.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(cboPositionGradeID.SelectedValue))
            {
                query.Where(query.PositionGradeID == cboPositionGradeID.SelectedValue);
            }

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
            var query = new PositionGradeQuery();

            query.Where(
                query.PositionGradeName.Like(searchTextContain));

            query.Select(query.PositionGradeID, query.PositionGradeCode, query.PositionGradeName, query.RankName);

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
            e.Item.Text = ((DataRowView)e.Item.DataItem)["PositionGradeCode"].ToString().Trim() + " (" + ((DataRowView)e.Item.DataItem)["RankName"].ToString() + ")";
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PositionGradeID"].ToString();
        }
    }
}
