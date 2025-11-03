using System;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Master
{
    public partial class DtdSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.DTD;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                RestoreValueFromCookie();
        }
        public override bool OnButtonOkClicked()
        {
            var query = new DtdQuery("a");
            var diagQ = new DiagnoseQuery("b");
            var std = new AppStandardReferenceItemQuery("c");
            
            query.LeftJoin(diagQ).On(query.DtdNo == diagQ.DtdNo);
            query.LeftJoin(std).On(std.StandardReferenceID == AppEnum.StandardReference.ChronicDisease && std.ItemID == query.SRChronicDisease);

            if (!string.IsNullOrEmpty(txtDtdNo.Text))
            {
                if (cboFilterDtdNo.SelectedIndex == 1)
                    query.Where(query.DtdNo == txtDtdNo.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtDtdNo.Text);
                    query.Where(query.DtdNo.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtDtdName.Text))
            {
                if (cboFilterDtdName.SelectedIndex == 1)
                    query.Where(query.DtdName == txtDtdName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtDtdName.Text);
                    query.Where(query.DtdName.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtDiagnoseID.Text))
            {
                if (cboFilterDiagnoseID.SelectedIndex == 1)
                    query.Where(diagQ.DiagnoseID == txtDiagnoseID.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtDiagnoseID.Text);
                    query.Where(diagQ.DiagnoseID.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtDiagnoseName.Text))
            {
                if (cboFilterDiagnoseName.SelectedIndex == 1)
                    query.Where(diagQ.DiagnoseName == txtDiagnoseName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtDiagnoseName.Text);
                    query.Where(diagQ.DiagnoseName.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(cboSRChronicDisease.SelectedValue))
            {
                query.Where(query.SRChronicDisease == cboSRChronicDisease.SelectedValue);
            }

            query.Select(query.DtdNo, query.DtdName, query.DtdLabel, query.SRChronicDisease, std.ItemName.As("ChronicDisease"), query.IsActive);
            query.GroupBy(query.DtdNo, query.DtdName, query.DtdLabel, query.SRChronicDisease, std.ItemName, query.IsActive);

            query.OrderBy(query.DtdNo.Ascending);
            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }

        protected void cboSRChronicDisease_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.StandardReferenceItemsRequested((RadComboBox)sender, "ChronicDisease", e.Text);
        }

        protected void cboSRChronicDisease_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.StandardReferenceItemDataBound(e);
        }
    }
}
