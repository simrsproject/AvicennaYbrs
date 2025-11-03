using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using System.Web.UI;
using Temiang.Dal.DynamicQuery;
using System.Collections.Generic;
using System.Web;

namespace Temiang.Avicenna.Module.Laundry.Transaction
{
    public partial class LinenItemsRepairingList : BasePage
    {       
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e); // !!Jika tidak dipanggil, tampilan jadi tidak rapih
          

            ProgramID = AppConstant.Program.LinenItemsRepairing;

            if (!IsPostBack)
            {
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (IsPostBack) return;

            RestoreValueFromCookie();
        }
       
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grdList.DataSource = new String[] { };
                return;
            }

            var grd = (RadGrid)source;
            var dataSource = LaundryRepairingProcesses;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }          
        }

        private DataTable LaundryRepairingProcesses
        {
            get
            {
                var query = new LaundryRepairingProcessQuery("a");
                query.Select
                    (
                        query.TransactionNo,
                        query.TransactionDate,
                        query.Notes,
                        query.IsApproved,
                        query.IsVoid,
                        "<'LinenItemsRepairingDetail.aspx?md=view&id=' + a.TransactionNo AS RUrl>"
                    );

                query.Where(query.Or(query.IsClosed.IsNull(), query.IsClosed == false));
                

                if (!txtFromTransactionDate.IsEmpty && !txtToTransactionDate.IsEmpty)
                    query.Where(query.TransactionDate.Between(txtFromTransactionDate.SelectedDate, txtToTransactionDate.SelectedDate));
                if (!string.IsNullOrEmpty(txtTransactionNo.Text))
                    query.Where(query.TransactionNo == txtTransactionNo.Text);

                query.OrderBy(query.TransactionNo.Descending);
                query.es.Top = AppSession.Parameter.MaxResultRecord;

                return query.LoadDataTable();
            }
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = e.DetailTableView.ParentItem;
            string trn = dataItem.GetDataKeyValue("TransactionNo").ToString();

            var query = new LaundryRepairingProcessItemQuery("a");
            var itemq = new ItemQuery("b");
            
            query.Select
                (
                    query.ItemID,
                    itemq.ItemName.As("ItemName"),
                    query.Qty,
                    query.SRItemUnit

                );
            query.InnerJoin(itemq).On(itemq.ItemID == query.ItemID);
            
            query.Where(query.TransactionNo == trn);
            
            query.OrderBy(query.ItemID.Ascending);

            e.DetailTableView.DataSource = query.LoadDataTable();
        }

        protected void grdClosingList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grdList.DataSource = new String[] { };
                return;
            }

            var grd = (RadGrid)source;
            var dataSource = LaundryRepairingProcessClose;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }           
        }

        private DataTable LaundryRepairingProcessClose
        {
            get
            {              
                var query = new LaundryRepairingProcessQuery("a");
                query.Select
                    (
                        query.TransactionNo,
                        query.TransactionDate,
                        query.ClosedDateTime,
                        query.Notes,
                        query.IsApproved,
                        query.IsVoid
                    );

                query.Where(query.IsClosed == true);


                if (!txtFromTransactionDate.IsEmpty && !txtToTransactionDate.IsEmpty)
                    query.Where(query.TransactionDate.Between(txtFromTransactionDate.SelectedDate, txtToTransactionDate.SelectedDate));
                if (!string.IsNullOrEmpty(txtTransactionNo.Text))
                    query.Where(query.TransactionNo == txtTransactionNo.Text);
                if (!txtFromClosingDate.IsEmpty && !txtToClosingDate.IsEmpty)
                    query.Where(query.ClosedDateTime >= txtFromClosingDate.SelectedDate, query.ClosedDateTime < txtToClosingDate.SelectedDate.Value.AddDays(1));

                query.OrderBy(query.TransactionNo.Descending);
                query.es.Top = AppSession.Parameter.MaxResultRecord;

                return query.LoadDataTable();
            }
        }

        protected void grdClosingList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = e.DetailTableView.ParentItem;
            string trn = dataItem.GetDataKeyValue("TransactionNo").ToString();

            var query = new LaundryRepairingProcessItemQuery("a");
            var itemq = new ItemQuery("b");

            query.Select
                (
                    query.ItemID,
                    itemq.ItemName.As("ItemName"),
                    query.Qty,
                    query.SRItemUnit

                );
            query.InnerJoin(itemq).On(itemq.ItemID == query.ItemID);

            query.Where(query.TransactionNo == trn);

            query.OrderBy(query.ItemID.Ascending);

            e.DetailTableView.DataSource = query.LoadDataTable();
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            SaveValueToCookie();
            grdList.Rebind();
            grdClosingList.Rebind();
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (string.IsNullOrEmpty(eventArgument))
                return;

            if (!(sourceControl is RadGrid))
                return;

            if (eventArgument.Contains("|"))
            {
                var param = eventArgument.Split('|');

                var x = new LaundryRepairingProcess();
                x.LoadByPrimaryKey(param[1]);
                
                x.IsClosed = true;
                x.ClosedDateTime= (new DateTime()).NowAtSqlServer();
                x.ClosedByUserID = AppSession.UserLogin.UserID;
                x.LastUpdateDateTime= (new DateTime()).NowAtSqlServer();
                x.LastUpdateByUserID= AppSession.UserLogin.UserID;
                
                x.Save();

                grdList.Rebind();
                grdClosingList.Rebind();
            }
        }
    }
}