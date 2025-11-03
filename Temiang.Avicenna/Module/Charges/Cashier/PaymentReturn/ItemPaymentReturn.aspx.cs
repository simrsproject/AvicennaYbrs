using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI;
using System.Collections.Generic;

namespace Temiang.Avicenna.Module.Charges.Cashier.PaymentReturn
{
    public partial class ItemPaymentReturn : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.PaymentReturn;
            Title = "Payment Detail : " + Request.QueryString["paymentNo"];
        }

        private DataTable TransPaymentItems
        {
            get
            {
                TransPaymentItemQuery query = new TransPaymentItemQuery("a");
                AppStandardReferenceItemQuery srQuery = new AppStandardReferenceItemQuery("b");
                AppStandardReferenceItemQuery srQuery2 = new AppStandardReferenceItemQuery("c");

                query.Select
                    (
                        query,
                        srQuery2.ItemName.As("PaymentTypeName"),
                        srQuery.ItemName.As("PaymentMethodName"),
                        query.Amount
                    );
                query.InnerJoin(srQuery2).On(query.SRPaymentType == srQuery2.ItemID);
                query.InnerJoin(srQuery).On(query.SRPaymentMethod == srQuery.ItemID);
                query.Where(query.PaymentNo == Request.QueryString["paymentNo"]);
                query.OrderBy(query.SequenceNo.Ascending);

                return query.LoadDataTable();
            }
        }

        protected void grdTransPaymentItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdTransPaymentItem.DataSource = TransPaymentItems;
        }
    }
}
