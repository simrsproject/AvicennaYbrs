using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject.Reference;
namespace Temiang.Avicenna.Module.AssetManagement.Management.WorkOrderRealization
{
    public partial class ReferToOtherUnitDialog : BasePageDialog
    {
        private AppAutoNumberLast _autoNumber;

        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Title = "Refer to other unit, WO No. : " + Request.QueryString["owo"].ToString();

                ComboBox.PopulateWithServiceUnitForTransaction(cboServiceUnitID, BusinessObject.Reference.TransactionCode.AssetWorkOrderRealization, false);
            }
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'rebind'";
        }

        public override bool OnButtonOkClicked()
        {
            if (!Page.IsValid)
                return false;

            if (string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
            {
                ShowInformationHeader("Refer To Unit is required.");
                return false;
            }
            if (string.IsNullOrEmpty(txtProblemDescription.Text))
            {
                ShowInformationHeader("Problem Description is required.");
                return false;
            }

            var woRef = new AssetWorkOrder();
            woRef.LoadByPrimaryKey(Request.QueryString["owo"]);

            using (esTransactionScope trans = new esTransactionScope())
            {
                var entity = new AssetWorkOrder();
                entity.AddNew();

                var unit = new ServiceUnit();

                _autoNumber = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, TransactionCode.AssetWorkOrder, unit.LoadByPrimaryKey(woRef.ToServiceUnitID) ? unit.DepartmentID : string.Empty);

                entity.OrderNo = _autoNumber.LastCompleteNumber;
                _autoNumber.Save();

                entity.OrderDate = (new DateTime()).NowAtSqlServer().Date;
                entity.FromServiceUnitID = woRef.ToServiceUnitID;
                entity.ToServiceUnitID = cboServiceUnitID.SelectedValue;
                entity.AssetID = woRef.AssetID;
                entity.ItemID = woRef.ItemID;
                entity.Qty = woRef.Qty;
                entity.ProblemDescription = txtProblemDescription.Text;
                entity.SRWorkStatus = woRef.SRWorkStatus;
                entity.SRWorkType = woRef.SRWorkType;
                entity.SRWorkPriority = woRef.SRWorkPriority;
                entity.SRWorkTrade = woRef.SRWorkTrade;
                entity.RequiredDate = (new DateTime()).NowAtSqlServer().Date;
                entity.RequestByUserID = AppSession.UserLogin.UserID;
                entity.IsVoid = false;
                entity.IsApproved = true;
                entity.ApprovedDateTime = (new DateTime()).NowAtSqlServer();
                entity.IsProceed = false;
                entity.IsPreventiveMaintenance = woRef.IsPreventiveMaintenance;
                entity.PMNo = woRef.PMNo;
                entity.ReferenceNo = (string.IsNullOrEmpty(woRef.ReferenceNo) ? woRef.OrderNo : woRef.ReferenceNo);
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                entity.Save();

                trans.Complete();
            }

            return true;
        }
    }
}
