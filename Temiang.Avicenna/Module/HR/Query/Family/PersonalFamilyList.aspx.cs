using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.HR.Query
{
    public partial class PersonalFamilyList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "PersonalFamilySearch.aspx";
            UrlPageDetail = "PersonalFamilyDetail.aspx";

            this.WindowSearch.Height = 400;

            ProgramID = AppConstant.Program.QueryPersonalFamily; //TODO: Isi ProgramID
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
            string id = dataItem.GetDataKeyValue(PersonalFamilyMetadata.ColumnNames.PersonalFamilyID).ToString();
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, mode, id);
            Page.Response.Redirect(url, true);
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = PersonalFamilys;
        }

        private DataTable PersonalFamilys
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }
				
				PersonalFamilyQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (PersonalFamilyQuery)Session[SessionNameForQuery];
                }
                else
                {
                    AppStandardReferenceItemQuery edu = new AppStandardReferenceItemQuery("g");
                    PatientQuery patient = new PatientQuery("f");
                    AppStandardReferenceItemQuery gender = new AppStandardReferenceItemQuery("e");
                    AppStandardReferenceItemQuery marital = new AppStandardReferenceItemQuery("d");
                    AppStandardReferenceItemQuery relation = new AppStandardReferenceItemQuery("c");
                    PersonalInfoQuery posquery = new PersonalInfoQuery("b");
                    query = new PersonalFamilyQuery("a");

                    query.es.Top = AppSession.Parameter.MaxResultRecord;
                    query.Select
                        (
                           query.PersonalFamilyID,
                           query.PersonID,
                           posquery.EmployeeNumber,
                           posquery.EmployeeName,
                           query.PatientID,
                           patient.MedicalNo,
                           relation.ItemName.As("FamilyRelationName"),
                           query.FamilyName,
                           query.DateBirth,
                           edu.ItemName.As("EducationLevelName"),
                           query.Address,
                           query.Phone,
                           marital.ItemName.As("MaritalStatusName"),
                           gender.ItemName.As("GenderTypeName"),
                           query.IsGuaranteed,
                           query.LastUpdateByUserID,
                           query.LastUpdateDateTime
                        );

                    query.InnerJoin(posquery).On(query.PersonID == posquery.PersonID);
                    query.LeftJoin(relation).On
                            (
                                query.SRFamilyRelation == relation.ItemID &
                                relation.StandardReferenceID == "FamilyRelation"
                            );
                    query.LeftJoin(marital).On
                            (
                                query.SRMaritalStatus == marital.ItemID &
                                marital.StandardReferenceID == "MaritalStatus"
                            );
                    query.LeftJoin(gender).On
                            (
                                query.SRGenderType == gender.ItemID &
                                gender.StandardReferenceID == "GenderType"
                            );

                    query.LeftJoin(patient).On
                        (
                            query.PatientID == patient.PatientID
                        );
                    query.LeftJoin(edu).On
                            (
                                query.SREducationLevel == edu.ItemID &
                                edu.StandardReferenceID == "EducationLevel"
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

