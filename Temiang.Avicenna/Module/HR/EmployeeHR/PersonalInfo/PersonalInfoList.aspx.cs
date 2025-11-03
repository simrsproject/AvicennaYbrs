using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.HR.EmployeeHR
{
    public partial class PersonalInfoList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "PersonalInfoSearch.aspx?status=" + Request.QueryString["status"];
            UrlPageDetail = "PersonalInfoDetail.aspx";
            UrlPageDetailNew = string.Format("{0}?md={1}&status={2}", "PersonalInfoDetail.aspx", "new", Request.QueryString["status"]);

            if (Request.QueryString["status"] == "recruit")
            {
                ProgramID = AppConstant.Program.ApplicantPersonalInfo;

                grdList.Columns.FindByUniqueName("EmployeeNumber").HeaderText = "Applicant No";
                grdList.Columns.FindByUniqueName("EmployeeName").HeaderText = "Applicant Name";

                if (AppParameter.IsYes(AppParameter.ParameterItem.IsEnabledEmployeeRecruitmentGoogleForm))
                {
                    // Additional Toolbar item
                    // Add menu Import From Google Form

                    var tbiSplit = new RadToolBarButton();
                    tbiSplit.IsSeparator = true;
                    ToolBarMenu.Items.Add(tbiSplit);

                    var tbi = new RadToolBarButton();
                    tbi.Text = "Import From Google Form";
                    tbi.Value = "import";
                    tbi.ImageUrl = "~/Images/Toolbar/process16.png";
                    ToolBarMenu.Items.Add(tbi);
                }
            }
            else
                ProgramID = AppConstant.Program.PersonalInfo; //TODO: Isi ProgramID

            // Quick Search
            ToolBarMenuQuickSearch.Visible = true;
        }

        #region Additional Toolbar menu item
        public override string OnGetAdditionalMenuScript()
        {
            var script = @"
case ""import"":
openWinGoogleFormImport();
    args.set_cancel(true);
    break; 
";

            return script;
        }

        protected override void OnToolBarMenuDataAdditionalButtonClick(ValidateArgs args, string value)
        {
        }

        #endregion
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
            //TODO: Betulkan parameter nya
            string id = dataItem.GetDataKeyValue(PersonalInfoMetadata.ColumnNames.PersonID).ToString();
            string url = string.Format("{0}?md={1}&id={2}&status={3}", UrlPageDetail, mode, id, Request.QueryString["status"]);
            Page.Response.Redirect(url, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = PersonalInfos;
        }

        private DataTable PersonalInfos
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                PersonalInfoQuery query;

                if (Session[SessionNameForQuery] != null)
                {
                    query = (PersonalInfoQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new PersonalInfoQuery("a");
                    var gender = new AppStandardReferenceItemQuery("b");
                    var religion = new AppStandardReferenceItemQuery("c");
                    //var blood = new AppStandardReferenceItemQuery("d");
                    var marital = new AppStandardReferenceItemQuery("e");
                    var wi = new EmployeeWorkingInfoQuery("f");
                    var cls = new ClassQuery("g");
                    var status = new AppStandardReferenceItemQuery("h");
                    var unit = new OrganizationUnitQuery("x");
                    var info = new VwEmployeeTableQuery("y");

                    query.es.Top = AppSession.Parameter.MaxResultRecord;
                    query.Select(
                                    query.PersonID,
                                    query.EmployeeNumber.As("EmployeeNo"),
                                    query.FirstName,
                                    query.EmployeeName,
                                    query.PlaceBirth,
                                    query.BirthDate,
                                    query.SRGenderType,
                                    gender.ItemName.As("GenderName"),
                                    query.SRReligion,
                                    religion.ItemName.As("ReligionName"),
                                    //query.SRBloodType,
                                    //blood.ItemName.As("BloodType"),
                                    query.SRMaritalStatus,
                                    marital.ItemName.As("MaritalStatus"),
                                    query.LastUpdateDateTime,
                                    query.LastUpdateByUserID,
                                    cls.ClassName, info.Nik
                                );
                    query.LeftJoin(gender).On
                            (
                                query.SRGenderType == gender.ItemID &
                                gender.StandardReferenceID == "GenderType"
                            );
                    query.LeftJoin(religion).On
                            (
                                query.SRReligion == religion.ItemID &
                                religion.StandardReferenceID == "Religion"
                            );
                    //query.LeftJoin(blood).On
                    //        (
                    //            query.SRBloodType == blood.ItemID &
                    //            blood.StandardReferenceID == "BloodType"
                    //        );
                    query.LeftJoin(marital).On
                            (
                                query.SRMaritalStatus == marital.ItemID &
                                marital.StandardReferenceID == "TaxStatus"
                            );

                    query.LeftJoin(cls).On(query.CoverageClass == cls.ClassID);
                    query.LeftJoin(wi).On(query.PersonID == wi.PersonID);
                    query.LeftJoin(status).On
                            (
                                wi.SREmployeeStatus == status.ItemID &
                                status.StandardReferenceID == AppEnum.StandardReference.EmployeeStatus
                            );
                    query.InnerJoin(info).On(query.PersonID == info.PersonID);
                    query.LeftJoin(unit).On(info.ServiceUnitID == unit.OrganizationUnitID);

                    if (Request.QueryString["status"] == "recruit")
                    {
                        var eepq = new EmployeeEmploymentPeriodQuery("eep");
                        query.LeftJoin(eepq).On(eepq.PersonID == query.PersonID && eepq.SREmploymentType == "0");
                        query.Select(@"<ISNULL(eep.EmployeeNumber, a.EmployeeNumber) AS EmployeeNumber>");
                        query.Where(info.SREmploymentType == "0"); // applicant
                    }
                    else
                    {
                        query.Select(query.EmployeeNumber);
                        query.Where(info.SREmploymentType != "0");
                    }

                    query.OrderBy(query.PersonID.Ascending);

                    //Quick Search
                    ApplyQuickSearch(query, "FirstName", "EmployeeNumber");

                }
                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}

