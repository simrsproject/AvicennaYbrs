using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.RADT
{
    public partial class PatientRelatedList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "PatientRelatedSearch.aspx";
            UrlPageDetail = "PatientRelatedDetail.aspx";

            ProgramID = AppConstant.Program.PatientRelated;

            // Quick Search
            ToolBarMenuQuickSearch.Visible = true;
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
            string patientID = dataItem.GetDataKeyValue(PatientMetadata.ColumnNames.PatientID).ToString();

            string url = string.Format("{0}?md={1}&patID={2}", UrlPageDetail, mode, patientID);
            Page.Response.Redirect(url, true);
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = PatientRelateds;
        }

        private DataTable PatientRelateds
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                PatientQuery qPatient;
                if (Session[SessionNameForQuery] != null)
                {
                    qPatient = (PatientQuery)Session[SessionNameForQuery];
                }
                else
                {
                    qPatient = new PatientQuery("a");
                    var qRel = new PatientRelatedQuery("b");
                    var sal = new AppStandardReferenceItemQuery("sal");

                    qPatient.LeftJoin(qRel).On(qPatient.PatientID == qRel.RelatedPatientID);
                    qPatient.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & qPatient.SRSalutation == sal.ItemID);

                    qPatient.es.Top = AppSession.Parameter.MaxResultRecord;
                    qPatient.Where(qPatient.IsActive == true, qRel.RelatedPatientID.IsNull());
                    qPatient.Select
                        (
                            qPatient.PatientID,
                            qPatient.MedicalNo,
                            @"<LTRIM(RTRIM(LTRIM(a.FirstName + ' ' + a.MiddleName)) + ' ' + a.LastName) AS 'PatientName'>",
                            qPatient.Sex,
                            qPatient.DateOfBirth,
                            @"<a.StreetName+' '+a.City+' '+a.County +' '+ISNULL(a.ZipCode, '') AS 'Address'>",
                            sal.ItemName.As("SalutationName")
                         );
                    qPatient.OrderBy(qPatient.PatientID.Ascending);

                    //Quick Search
                    ApplyQuickSearch(qPatient, "FirstName", "MedicalNo");
                }

                qPatient.es.Top = AppSession.Parameter.MaxResultRecord;

                DataTable dtb = qPatient.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}
