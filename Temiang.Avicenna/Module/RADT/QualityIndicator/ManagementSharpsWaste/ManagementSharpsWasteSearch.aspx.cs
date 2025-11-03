using System;
using System.Data;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.RADT.QualityIndicator
{
    public partial class ManagementSharpsWasteSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.ManagementSharpsWaste;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
            {
            }
        }

        public override bool OnButtonOkClicked()
        {
            OnButtonOkClick();

            return true;
        }

        private void OnButtonOkClick()
        {
            var query = new ManagementSharpsWasteQuery("a");
            var su = new ServiceUnitQuery("b");
            var user = new AppUserServiceUnitQuery("c");


            query.InnerJoin(su).On(query.ServiceUnitID == su.ServiceUnitID);
            query.InnerJoin(user).On(user.ServiceUnitID == query.ServiceUnitID & user.UserID == AppSession.UserLogin.UserID);

            query.OrderBy
                        (
                            query.TransactionDate.Descending, query.TransactionNo.Descending
                        );


            query.Select(
                       query.TransactionNo,
                       query.TransactionDate,
                       query.UserName,
                       su.ServiceUnitName.As("ServiceUnitName"),
                       query.IsApproved,
                       query.IsVoid
                       );

            if (!string.IsNullOrEmpty(txtTransactionNo.Text))
            {
                if (cboFilterTransactionNo.SelectedIndex == 1)
                    query.Where(query.TransactionNo == txtTransactionNo.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtTransactionNo.Text);
                    query.Where(query.TransactionNo.Like(searchTextContain));
                }
            }
            if (!txtTransactionDate.IsEmpty)
            {
                query.Where(query.TransactionDate == txtTransactionDate.SelectedDate);
            }            

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset
        }
    }
}