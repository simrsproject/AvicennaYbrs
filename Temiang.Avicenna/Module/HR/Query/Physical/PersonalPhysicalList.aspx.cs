using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.HR.Query
{
    public partial class PersonalPhysicalList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "PersonalPhysicalSearch.aspx";
            UrlPageDetail = "PersonalPhysicalDetail.aspx";

            this.WindowSearch.Height = 400;

            ProgramID = AppConstant.Program.QueryPersonalPhysical; //TODO: Isi ProgramID
        }
        public override void OnMenuEditClick(GridDataItem[] dataItems)
        {
            //RedirectToPageDetail(dataItems[0], "edit");
        }
        public override void OnMenuViewClick(GridDataItem[] dataItems)
        {
            //RedirectToPageDetail(dataItems[0], "view");
        }
        private void RedirectToPageDetail(GridDataItem dataItem, string mode)
        {
            //TODO: Betulkan parameter nya
            string id = dataItem.GetDataKeyValue(PersonalPhysicalMetadata.ColumnNames.PersonalPhysicalID).ToString();
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, mode, id);
            Page.Response.Redirect(url, true);
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = PersonalPhysicals;
        }

        private DataTable PersonalPhysicals
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }
				
				PersonalPhysicalQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (PersonalPhysicalQuery)Session[SessionNameForQuery];
                }
                else
                {
                    AppStandardReferenceItemQuery measurement = new AppStandardReferenceItemQuery("d");
                    AppStandardReferenceItemQuery characteristic = new AppStandardReferenceItemQuery("c");
                    PersonalInfoQuery personal = new PersonalInfoQuery("b");
                    query = new PersonalPhysicalQuery("a");

                    query.es.Top = AppSession.Parameter.MaxResultRecord;

                    query.Select
                        (
                           query.PersonalPhysicalID,
                           query.PersonID,
                           personal.EmployeeNumber,
                           personal.EmployeeName,
                           characteristic.ItemName.As("PhysicalCharacteristicName"),
                           query.PhysicalValue,
                           measurement.ItemName.As("MeasurementCodeName")
                        );

                    query.InnerJoin(personal).On(query.PersonID == personal.PersonID);
                    query.LeftJoin(characteristic).On
                            (
                                query.SRPhysicalCharacteristic == characteristic.ItemID &
                                characteristic.StandardReferenceID == "PhysicalCharacteristic"
                            );
                    query.LeftJoin(measurement).On
                            (
                                query.SRMeasurementCode == measurement.ItemID &
                                measurement.StandardReferenceID == "MeasurementCode"
                            );

                    query.OrderBy(query.PersonID.Ascending); //TODO: Betulkan ordernya
                }
				
				DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }

        }

    }
}

