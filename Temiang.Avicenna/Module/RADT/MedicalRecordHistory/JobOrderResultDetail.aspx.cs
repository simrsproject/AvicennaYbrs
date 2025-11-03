using System;
using Temiang.Avicenna.Common;
using System.Data;
using Temiang.Avicenna.BusinessObject;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.RADT
{
    public partial class JobOrderResultDetail : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.MedicalRecordHistory;

            if (!IsPostBack)
            {
                Session["pdfViewer"] = null;

                Helper.FindControlRecursive(Page, "btnOk").Visible = false;
                Helper.FindControlRecursive(Page, "btnCancel").Visible = false;
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
                grdList.DataSource = Registrations;
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            var dataItem = e.DetailTableView.ParentItem;
            var transactionNo = dataItem.GetDataKeyValue("TransactionNo").ToString();

            //Load record
            var query = new TransChargesItemQuery("a");
            var item = new ItemQuery("b");

            query.Select
                (
                    query.TransactionNo,
                    query.SequenceNo,
                    item.ItemName,
                    query.ChargeQuantity,
                    query.IsCito
                );
            query.InnerJoin(item).On(query.ItemID == item.ItemID);
            query.Where
                (
                    query.TransactionNo == transactionNo,
                    query.IsApprove == true
                );

            e.DetailTableView.DataSource = query.LoadDataTable();
        }

        private DataTable Registrations
        {
            get
            {
                var patientID = Page.Request.QueryString["patientID"];
                var reg = new RegistrationCollection();
                var dtdRegistration = reg.RegistrationHistoryForJobOrder(patientID, AppSession.Parameter.ServiceUnitLaboratoryID);
                var dtdRegRad = reg.RegistrationHistoryForJobOrder(patientID, AppSession.Parameter.ServiceUnitRadiologyID);

                dtdRegistration.Merge(dtdRegRad);
                
                return dtdRegistration;
            }
        }
    }
}
