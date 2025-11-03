using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Linq;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;
using System.Web.UI.WebControls;

namespace Temiang.Avicenna.Module.Finance.ParamedicFee.V2
{
    public partial class ParamedicFeeVerificationByPaymentAddDeducPickList : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["type"] == "2")
            {
                ProgramID = AppConstant.Program.ParamedicFeeVerificationPerRegNo;
            }
            else if (Request.QueryString["type"] == "3")
            {
                ProgramID = AppConstant.Program.ParamedicFeeVerificationPerFilter;
            }
            else
            {
                ProgramID = AppConstant.Program.ParamedicFeeVerification;
            }
        }

        private DataTable ParamedicFeeAddDeducs
        {
            get
            {
                string paramedicID = Request.QueryString["pid"];

                var query = new ParamedicFeeAddDeducQuery("a");
                var stdQuery = new AppStandardReferenceItemQuery("b");
                query.InnerJoin(stdQuery).On(query.SRParamedicFeeAdjustType == stdQuery.ItemID &&
                                             stdQuery.StandardReferenceID == "ParamedicFeeAdjustType");

                query.Select
                    (
                    query,
                    stdQuery.ItemName.As("ParamedicFeeAdjustType")
                    );

                query.Where
                    (
                    query.ParamedicID == paramedicID,
                    query.IsApproved == true,
                    query.VerificationNo.IsNull());
                
                query.OrderBy(query.TransactionNo.Ascending);

                return query.LoadDataTable();
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            ((RadGrid)source).DataSource = ParamedicFeeAddDeducs;
        }

        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            bool selected = ((CheckBox)sender).Checked;

            foreach (GridDataItem dataItem in grdList.MasterTableView.Items)
            {
                ((CheckBox)dataItem.FindControl("detailChkbox")).Checked = selected;
            }
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument.command = 'rebind'";
        }

        public override bool OnButtonOkClicked()
        {
            var addDeduc = (ParamedicFeeAddDeducCollection)Session["collParamedicFeeAddDeduc"];

            foreach (GridDataItem dataItem in grdList.MasterTableView.Items)
            {
                if (((CheckBox)dataItem.FindControl("detailChkbox")).Checked)
                {
                    {
                        if (addDeduc.Where(x => x.TransactionNo.Equals(dataItem["TransactionNo"].Text)).Count() == 0)
                        {
                            var entity = new ParamedicFeeAddDeduc();
                            entity.LoadByPrimaryKey(dataItem["TransactionNo"].Text);
                            entity.VerificationNo = Request.QueryString["ver"];
                            addDeduc.AttachEntity(entity);
                        }
                    }
                }
            }

            return true;
        }
    }
}
