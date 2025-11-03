using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class DiagnosisSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.Diagnosis;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                RestoreValueFromCookie();
        }
        public override bool OnButtonOkClicked()
        {
            var query = new DiagnoseQuery("a");
            var dtdq = new DtdQuery("b");
            query.InnerJoin(dtdq).On(query.DtdNo == dtdq.DtdNo);
            query.Select(query.DiagnoseID, query.DiagnoseName, query.DtdNo, dtdq.DtdName, query.IsChronicDisease,
                         query.IsDisease, query.IsActive);
            if (!string.IsNullOrEmpty(txtDiagnoseID.Text))
            {
                if (cboFilterDiagnoseID.SelectedIndex == 1)
                    query.Where(query.DiagnoseID == txtDiagnoseID.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtDiagnoseID.Text);
                    query.Where(query.DiagnoseID.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtDiagnoseName.Text))
            {
                if (cboFilterDiagnoseName.SelectedIndex == 1)
                    query.Where(query.DiagnoseName == txtDiagnoseName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtDiagnoseName.Text);
                    query.Where(query.DiagnoseName.Like(searchTextContain));
                }
            }
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
                    query.Where(dtdq.DtdName == txtDtdName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtDtdName.Text);
                    query.Where(dtdq.DtdName.Like(searchTextContain));
                }
            }
            
            query.OrderBy(query.DiagnoseID.Ascending);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}
