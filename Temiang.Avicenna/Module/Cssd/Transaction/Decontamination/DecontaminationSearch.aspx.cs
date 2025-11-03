using System;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Cssd.Transaction
{
    public partial class DecontaminationSearch : BasePageDialog
    {
        private string DPhase
        {
            get
            {
                return Request.QueryString["p"];
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = DPhase == "1" ? AppConstant.Program.CssdDecontaminationImmersion : (DPhase == "2" ? AppConstant.Program.CssdDecontaminationAbstersion : AppConstant.Program.CssdDecontaminationDrying);

            if (!IsPostBack)
            {
                trSRAbstersionType.Visible = (DPhase == "2");
                StandardReference.InitializeIncludeSpace(cboSRAbstersionType, AppEnum.StandardReference.AbstersionType);
            }
        }

        public override bool OnButtonOkClicked()
        {
            var query = new CssdDecontaminationQuery("a");
            var phaseq = new AppStandardReferenceItemQuery("b");
            var typeq = new AppStandardReferenceItemQuery("c");
            query.LeftJoin(phaseq).On(phaseq.StandardReferenceID == AppEnum.StandardReference.DecontaminationPhase.ToString() && phaseq.ItemID == query.SRDecontaminationPhase);
            query.LeftJoin(typeq).On(typeq.StandardReferenceID == AppEnum.StandardReference.AbstersionType.ToString() && typeq.ItemID == query.SRAbstersionType);

            query.Select
                (
                    query.DecontaminationNo,
                    query.DecontaminationDate,
                    query.DecontaminationTime,
                    phaseq.ItemName.As("DecontaminationPhase"),
                    typeq.ItemName.As("AbstersionType"),
                    query.Notes,
                    query.IsApproved,
                    query.IsVoid
                );

            if (!string.IsNullOrEmpty(txtDecontaminationNo.Text))
            {
                if (cboFilterDecontaminationNo.SelectedIndex == 1)
                    query.Where(query.DecontaminationNo == txtDecontaminationNo.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtDecontaminationNo.Text);
                    query.Where(query.DecontaminationNo.Like(searchTextContain));
                }
            }
            if (!txtDecontaminationDate.IsEmpty)
                query.Where(query.DecontaminationDate == txtDecontaminationDate.SelectedDate);
            
            query.Where(query.SRDecontaminationPhase == DPhase);

            if (!string.IsNullOrEmpty(cboSRAbstersionType.SelectedValue))
                query.Where(query.SRAbstersionType == cboSRAbstersionType.SelectedValue);


            query.OrderBy(query.DecontaminationDate.Descending, query.DecontaminationNo.Descending);
            query.es.Top = AppSession.Parameter.MaxResultRecord;

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}