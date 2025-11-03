using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI;
using System.Collections.Generic;

namespace Temiang.Avicenna.Module.Charges.PaymentReceive
{
    public partial class ItemPaymentReceive : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.PaymentReceive;
            Title = "Payment Detail : " + Request.QueryString["paymentNo"];
        }

        private DataTable TransPaymentItems
        {
            get
            {
                var query = new TransPaymentItemQuery("a");
                var srQuery = new PaymentMethodQuery("b");
                var srQuery2 = new PaymentTypeQuery("c");

                query.Select
                    (
                        query,
                        srQuery2.PaymentTypeName.As("PaymentTypeName"),
                        srQuery.PaymentMethodName.As("PaymentMethodName"),
                        query.Amount
                    );
                query.InnerJoin(srQuery2).On(query.SRPaymentType == srQuery2.SRPaymentTypeID);
                query.LeftJoin(srQuery).On
                   (
                       srQuery2.SRPaymentTypeID == srQuery.SRPaymentTypeID &
                       query.SRPaymentMethod == srQuery.SRPaymentMethodID
                   );
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
