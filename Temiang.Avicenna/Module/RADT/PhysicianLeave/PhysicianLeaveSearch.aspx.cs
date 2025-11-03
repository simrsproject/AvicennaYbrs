using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT
{
    public partial class PhysicianLeaveSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.PhysicianLeave;

            if (!IsPostBack)
            {
                var parColl = new ParamedicCollection();
                parColl.Query.Where(parColl.Query.IsActive == true);
                parColl.LoadAll();

                cboParamedicID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (var par in parColl)
                {
                    cboParamedicID.Items.Add(new RadComboBoxItem(par.ParamedicName, par.ParamedicID));
                }
            }
        }

        public override bool OnButtonOkClicked()
        {
            var query = new ParamedicLeaveQuery("a");
            var item = new AppStandardReferenceItemQuery("b");
            var par = new ParamedicQuery("c");
            var parIp = new ParamedicQuery("d");
            var parOp = new ParamedicQuery("e");
            var parEr = new ParamedicQuery("f");

            query.LeftJoin(item).On(query.SRPhysicianLeaveReason == item.ItemID &&
                                     item.StandardReferenceID == AppEnum.StandardReference.PhysicianLeaveReason);
            query.InnerJoin(par).On(query.ParamedicID == par.ParamedicID);
            query.LeftJoin(parIp).On(query.SubsParamedicIP == parIp.ParamedicID);
            query.LeftJoin(parOp).On(query.SubsParamedicOP == parOp.ParamedicID);
            query.LeftJoin(parEr).On(query.SubsParamedicEMR == parEr.ParamedicID);

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
                query.Where(query.TransactionDate == txtTransactionDate.SelectedDate);
            
            if (!string.IsNullOrEmpty(cboParamedicID.SelectedValue))
                query.Where(query.ParamedicID == cboParamedicID.SelectedValue);

            query.Select
                        (
                            query.TransactionNo,
                            query.TransactionDate,
                            par.ParamedicName,
                            item.ItemName,
                            query.StartDate,
                            query.EndDate,
                            query.Notes,
                            parIp.ParamedicName.As("SubsParamedicIP"),
                            parOp.ParamedicName.As("SubsParamedicOP"),
                            parEr.ParamedicName.As("SubsParamedicEMR"),
                            query.IsApproved
                        );
            query.OrderBy(query.TransactionNo.Descending);
            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }

        #region ComboBox ParamedicID

        protected void cboParamedicID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ParamedicName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ParamedicID"].ToString();
        }

        protected void cboParamedicID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ParamedicQuery();
            query.es.Top = 20;
            query.Select(
                query.ParamedicID,
                query.ParamedicName
                );
            query.Where(
                query.ParamedicName.Like(searchTextContain),
                query.IsActive == true
                );
            cboParamedicID.DataSource = query.LoadDataTable();
            cboParamedicID.DataBind();
        }

        #endregion
    }
}
