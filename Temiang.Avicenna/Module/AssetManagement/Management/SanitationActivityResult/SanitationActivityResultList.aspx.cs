using System;
using System.Data;
using System.Data.Linq;
using System.Linq;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.AssetManagement.Management
{
    public partial class SanitationActivityResultList : BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.SanitationActivityResult;

            IsShowValueFromCookie = true;

            if (!IsPostBack)
            {
                ComboBox.PopulateWithServiceUnitForTransaction(cboFromServiceUnitID, TransactionCode.AssetWorkOrder, false);
                ComboBox.PopulateWorkTradeItemList(cboSRWorkTradeItem, AppSession.Parameter.WorkTradeSanitation, true);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
            {
                //txtOrderDate1.SelectedDate = DateTime.Now.Date;
                //txtOrderDate2.SelectedDate = DateTime.Now.Date;
            }
            
        }

        protected void grdListOutstanding_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdListOutstanding.DataSource = WorkOrderOutstandings;
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
           grdList.DataSource = SanitationActivityResults;
        }

        private DataTable WorkOrderOutstandings
        {
            get
            {
                var query = new AssetWorkOrderQuery("a");
                var fromunit = new ServiceUnitQuery("b");
                var wtype = new AppStandardReferenceItemQuery("c");
                var wtrade = new AppStandardReferenceItemQuery("d");
                var result = new SanitationActivityResultQuery("e");

                query.Select
                    (
                        query.OrderNo,
                        query.OrderDate,
                        query.LastRealizationDateTime,
                        fromunit.ServiceUnitName.As("FromServiceUnit"),
                        wtype.ItemName.As("WorkType"),
                        wtrade.ItemName.As("WorkTradeItem"),
                        query.ProblemDescription,
                        @"<'SanitationActivityResultDetail.aspx?md=view&ono='+a.OrderNo as WoUrl>"
                    );

                query.InnerJoin(fromunit).On(fromunit.ServiceUnitID == query.FromServiceUnitID);
                query.InnerJoin(wtype).On
                    (
                        wtype.ItemID == query.SRWorkType &&
                        wtype.StandardReferenceID == "WorkType"
                    );
                query.InnerJoin(wtrade).On
                   (
                       wtrade.ItemID == query.SRWorkTradeItem &&
                       wtrade.StandardReferenceID == "WorkTradeItem"
                   );
                query.LeftJoin(result).On(result.OrderNo == query.OrderNo);

                query.Where(query.IsProceed == true, query.IsSanitation == true, result.ResultDateTime.IsNull());

                if (!txtOrderDate1.IsEmpty && !txtOrderDate2.IsEmpty)
                    query.Where(query.OrderDate >= txtOrderDate1.SelectedDate, query.OrderDate < txtOrderDate2.SelectedDate.Value.AddDays(1));
                if (!string.IsNullOrEmpty(txtOrderNo.Text))
                    query.Where(query.OrderNo == txtOrderNo.Text);
                if (!string.IsNullOrEmpty(cboFromServiceUnitID.SelectedValue))
                    query.Where(query.FromServiceUnitID == cboFromServiceUnitID.SelectedValue);
                if (!string.IsNullOrEmpty(cboSRWorkTradeItem.SelectedValue))
                    query.Where(query.SRWorkTradeItem == cboSRWorkTradeItem.SelectedValue);

                query.OrderBy(query.OrderDate.Descending, query.OrderNo.Descending);
                query.es.Top = AppSession.Parameter.MaxResultRecord;

                var dtb = query.LoadDataTable();
                return dtb;
            }
        }

        private DataTable SanitationActivityResults
        {
            get
            {
                var query = new AssetWorkOrderQuery("a");
                var fromunit = new ServiceUnitQuery("b");
                var wtype = new AppStandardReferenceItemQuery("c");
                var wtrade = new AppStandardReferenceItemQuery("d");
                var result = new SanitationActivityResultQuery("e");

                query.Select
                    (
                        query.OrderNo,
                        query.OrderDate,
                        query.LastRealizationDateTime,
                        result.ResultDateTime,
                        fromunit.ServiceUnitName.As("FromServiceUnit"),
                        wtype.ItemName.As("WorkType"),
                        wtrade.ItemName.As("WorkTradeItem"),
                        query.ProblemDescription,
                        @"<'SanitationActivityResultDetail.aspx?md=view&ono='+a.OrderNo as WoUrl>"
                    );

                query.InnerJoin(fromunit).On(fromunit.ServiceUnitID == query.FromServiceUnitID);
                query.InnerJoin(wtype).On
                    (
                        wtype.ItemID == query.SRWorkType &&
                        wtype.StandardReferenceID == "WorkType"
                    );
                query.InnerJoin(wtrade).On
                   (
                       wtrade.ItemID == query.SRWorkTradeItem &&
                       wtrade.StandardReferenceID == "WorkTradeItem"
                   );
                query.InnerJoin(result).On(result.OrderNo == query.OrderNo);

                query.Where(query.IsProceed == true, query.IsSanitation == true);

                if (!txtOrderDate1.IsEmpty && !txtOrderDate2.IsEmpty)
                    query.Where(query.OrderDate >= txtOrderDate1.SelectedDate, query.OrderDate < txtOrderDate2.SelectedDate.Value.AddDays(1));
                if (!txtResultDateTime1.IsEmpty && !txtResultDateTime2.IsEmpty)
                    query.Where(result.ResultDateTime >= txtResultDateTime1.SelectedDate, result.ResultDateTime < txtResultDateTime2.SelectedDate.Value.AddDays(1));
                if (!string.IsNullOrEmpty(txtOrderNo.Text))
                    query.Where(query.OrderNo == txtOrderNo.Text);
                if (!string.IsNullOrEmpty(cboFromServiceUnitID.SelectedValue))
                    query.Where(query.FromServiceUnitID == cboFromServiceUnitID.SelectedValue);
                if (!string.IsNullOrEmpty(cboSRWorkTradeItem.SelectedValue))
                    query.Where(query.SRWorkTradeItem == cboSRWorkTradeItem.SelectedValue);

                query.OrderBy(result.ResultDateTime.Descending, query.OrderNo.Descending);
                query.es.Top = AppSession.Parameter.MaxResultRecord;

                var dtb = query.LoadDataTable();
                return dtb;
            }
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            grdListOutstanding.CurrentPageIndex = 0;
            grdListOutstanding.Rebind();

            grdList.CurrentPageIndex = 0;
            grdList.Rebind();
        }
    }
}