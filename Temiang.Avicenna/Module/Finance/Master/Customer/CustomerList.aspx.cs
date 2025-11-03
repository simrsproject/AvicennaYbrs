using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class CustomerList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "CustomerSearch.aspx";
            UrlPageDetail = "CustomerDetail.aspx";

            WindowSearch.Height = 200;

            ProgramID = AppConstant.Program.CUSTOMER;
        }

        public override void OnMenuEditClick(Telerik.Web.UI.GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "edit");
        }

        public override void OnMenuViewClick(Telerik.Web.UI.GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "view");
        }

        private void RedirectToPageDetail(GridDataItem dataItem, string mode)
        {
            string id = dataItem.GetDataKeyValue(CustomerMetadata.ColumnNames.CustomerID).ToString();
            Page.Response.Redirect("CustomerDetail.aspx?md=" + mode + "&id=" + id, true);
        }	

        protected void grdList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = Customers;
        }

        private DataTable Customers
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                CustomerQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (CustomerQuery)Session[SessionNameForQuery];
                else
                {
                   
                     query = new CustomerQuery("a");

                    query.Select
                        (
                            query.CustomerID,
                            query.CustomerName,
                            query.ContactPerson,
                            query.StreetName,
                            query.City,
                            query.PhoneNo,
                            query.Email
                        );
                    query.OrderBy(query.CustomerID.Descending);
                }

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}