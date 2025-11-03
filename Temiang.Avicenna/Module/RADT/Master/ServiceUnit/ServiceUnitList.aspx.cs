using System;
using System.Collections.Generic;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ServiceUnitList : BasePageList
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ToolBarMenuQuickSearch.Enabled = true;
        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            WindowSearch.Height = 140;

            UrlPageSearch = "ServiceUnitSearch.aspx";
            UrlPageDetail = "ServiceUnitDetail.aspx";

            ProgramID = AppConstant.Program.ServiceUnit;

            // Quick Search
            ToolBarMenuQuickSearch.Visible = true;
        }

        public override void OnMenuEditClick(GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "edit");
        }

        private void RedirectToPageDetail(GridDataItem dataItem, string modus)
        {
            string id = dataItem.GetDataKeyValue(ServiceUnitMetadata.ColumnNames.ServiceUnitID).ToString();
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, modus, id);
            Page.Response.Redirect(url, true);
        }

        public override void OnMenuViewClick(GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "view");
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = ServiceUnits;
        }

        private DataTable ServiceUnits
        {
            get
            {
                object obj = Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                ServiceUnitQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (ServiceUnitQuery)Session[SessionNameForQuery];
                else
                {
                    query = new ServiceUnitQuery("su");

                    DepartmentQuery qDept = new DepartmentQuery("d");
                    query.InnerJoin(qDept).On(query.DepartmentID == qDept.DepartmentID);

                    //LocationQuery qLoc = new LocationQuery("c");
                    //query.LeftJoin(qLoc).On(query.LocationID == qLoc.LocationID);

                    AppStandardReferenceItemQuery qSr = new AppStandardReferenceItemQuery("sr");
                    query.LeftJoin(qSr).On
                        (
                            query.SRRegistrationType == qSr.ItemID & 
                            qSr.StandardReferenceID == string.Format("'{0}'", AppEnum.StandardReference.RegistrationType)
                        );

                    query.Select
                        (
                            query.DepartmentID,
                            qDept.DepartmentName,
                            query.ServiceUnitID,
                            query.ServiceUnitName,
                            query.ShortName,
                            query.ServiceUnitOfficer,
                            "<'' AS LocationName>",
                            query.SRRegistrationType,
                            qSr.ItemName.As("refToAppStandardReferenceItem_RegistrationType"),
                            query.IsUsingJobOrder,
                            query.IsPatientTransaction,
                            query.IsTransactionEntry,
                            query.IsActive
                        );
                    query.OrderBy(query.ServiceUnitID.Ascending);

                    //Quick Search
                    ApplyQuickSearch(query, "ServiceUnitName", "ServiceUnitID");
                }

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                DataTable dtb = query.LoadDataTable();

                foreach (DataRow row in dtb.Rows)
                {
                    var loc = new Location();
                    if (loc.LoadByPrimaryKey((new ServiceUnit()).GetMainLocationId(row["ServiceUnitID"].ToString())))
                        row["LocationName"] = loc.LocationName;
                }
                dtb.AcceptChanges();

                Session[SessionNameForList] = dtb;

                return dtb;
            }
        }


    }
}