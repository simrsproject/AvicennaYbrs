using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class NonPatientCustomerChargesList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "NonPatientCustomerChargesSearch.aspx";
            UrlPageDetail = "NonPatientCustomerChargesDetail.aspx";

            ProgramID = AppConstant.Program.NonPatientCustomerCharges;

            this.WindowSearch.Height = 400;
        }

        public override void OnMenuEditClick(GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "edit");
        }

        public override void OnMenuViewClick(GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "view");
        }

        private void RedirectToPageDetail(GridDataItem dataItem, string mode)
        {
            string id = dataItem.GetDataKeyValue(ItemTransactionMetadata.ColumnNames.TransactionNo).ToString();
            string url = string.Format("NonPatientCustomerChargesDetail.aspx?md={0}&id={1}", mode, id);
            Page.Response.Redirect(url, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
                grdList.DataSource = TransCharges;
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            //Load record
            TransChargesItemQuery query = new TransChargesItemQuery("a");
            ItemQuery iq = new ItemQuery("b");
            TransChargesQuery tc = new TransChargesQuery("c");
            var cc = new CostCalculationQuery("d");

            query.Select
                (
                    query.TransactionNo,
                    query.ItemID,                
                    query.SequenceNo,
                    query.ChargeQuantity,
                    query.Price,
                    query.LastUpdateByUserID,
                    query.CitoAmount,
                    query.DiscountAmount,
                    tc.IsApproved,
                    tc.IsVoid,               
                    tc.IsCorrection,
                    tc.IsAutoBillTransaction, 
                    cc.IntermBillNo,
                    iq.ItemName.As("ItemName")
                );
            query.InnerJoin(iq).On(query.ItemID == iq.ItemID);
            query.InnerJoin(tc).On(tc.TransactionNo == query.TransactionNo);
            query.InnerJoin(cc).On(cc.TransactionNo == query.TransactionNo && cc.SequenceNo == query.SequenceNo);
            query.Where(query.TransactionNo == e.DetailTableView.ParentItem.GetDataKeyValue("TransactionNo").ToString(),tc.IsNonPatient == true);
            query.OrderBy(query.ItemID.Ascending);

            //Apply
            e.DetailTableView.DataSource = query.LoadDataTable();
        }

        private DataTable TransCharges
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                TransChargesQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (TransChargesQuery)Session[SessionNameForQuery];
                else
                {

                    query = new TransChargesQuery("a");
                    var unit = new ServiceUnitQuery("b");
                    var reg = new RegistrationQuery("c");
                    var patient = new PatientQuery("d");
                    var sal = new AppStandardReferenceItemQuery("e");

                    query.Select
                        (
                            query.RegistrationNo,
                            reg.RegistrationDate,
                            query.TransactionNo,
                            unit.ServiceUnitName,
                            patient.PatientName,
                            query.FromServiceUnitID,
                            sal.ItemName.As("SalutationName")
                        );

                    query.InnerJoin(reg).On(reg.RegistrationNo == query.RegistrationNo);
                    query.InnerJoin(unit).On(query.FromServiceUnitID == unit.ServiceUnitID);
                    query.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
                    query.LeftJoin(sal).On(sal.StandardReferenceID == AppEnum.StandardReference.Salutation.ToString() && sal.ItemID == patient.SRSalutation);
                    query.Where
                   (
                       reg.IsClosed == false,
                       reg.SRRegistrationType == AppConstant.RegistrationType.OutPatient,
                       // reg.IsHoldTransactionEntry == false,
                       reg.IsVoid == false,
                       reg.IsFromDispensary == false,
                       reg.IsNonPatient == true
                   );

                    //query = new ItemTransactionQuery("a");
                    //ServiceUnitQuery qryserviceunit = new ServiceUnitQuery("b");
                    //ServiceUnitQuery qryserviceunitto = new ServiceUnitQuery("c");
                    //AppStandardReferenceItemQuery itemtype = new AppStandardReferenceItemQuery("d");
                    //var user = new AppUserServiceUnitQuery("e");

                    //query.Select
                    //    (
                    //        query.TransactionNo,
                    //        query.TransactionDate,
                    //        qryserviceunit.ServiceUnitName.As("FServiceUnitID"),
                    //        qryserviceunitto.ServiceUnitName.As("TServiceUnitID"),
                    //        itemtype.ItemName,
                    //        query.IsApproved,
                    //        query.IsClosed,
                    //        query.Notes,
                    //        query.IsVoid,
                    //        "<'RequestOrderDetail.aspx?md=view&id='+a.TransactionNo as PrUrl>"
                    //    );

                    //query.InnerJoin(qryserviceunit).On(qryserviceunit.ServiceUnitID == query.FromServiceUnitID);
                    //query.InnerJoin(qryserviceunitto).On(qryserviceunitto.ServiceUnitID == query.ToServiceUnitID);
                    //query.LeftJoin(itemtype).On
                    //    (
                    //        itemtype.ItemID == query.SRItemType && 
                    //        itemtype.StandardReferenceID == "ItemType"
                    //    );
                    //query.InnerJoin(user).On(user.ServiceUnitID == query.FromServiceUnitID);
                    //query.Where
                    //    (
                    //        query.IsClosed == false, 
                    //        query.TransactionCode == BusinessObject.Reference.TransactionCode.PurchaseRequest,
                    //        user.UserID == AppSession.UserLogin.UserID
                    //    );
                    //query.OrderBy(query.TransactionDate.Descending);
                }

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;

                return dtb;
            }
        }
    }
}