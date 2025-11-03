using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.RADT.QualityIndicator
{
    public partial class PatientIncidentList : BasePageList
    {
        private string FormType
        {
            get
            {
                return Request.QueryString["type"];
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            WindowSearch.Height = 300;

            if (FormType == "entry")
            {
                UrlPageSearch = "PatientIncidentSearch.aspx?type=entry";
                UrlPageDetail = "PatientIncidentDetail.aspx?type=entry";
                ProgramID = AppConstant.Program.PatientIncident;
            }
            else
            {
                UrlPageSearch = "PatientIncidentSearch.aspx?type=verif";
                UrlPageDetail = "PatientIncidentDetail.aspx?type=verif";
                ProgramID = AppConstant.Program.PatientIncidentVerification;
            }

            if (!IsPostBack)
            {
                grdList.Columns[8].Visible = FormType != "verif";
                grdList.Columns[9].Visible = FormType == "verif";
            }
        }

        public override string OnGetScriptToolBarNewClicking()
        {
            string script = string.Format(@"gotoAddUrl('{0}'); args.set_cancel(true);", FormType);
            return script;
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
            string id = dataItem.GetDataKeyValue(PatientIncidentMetadata.ColumnNames.PatientIncidentNo).ToString();
            string url = string.Format("PatientIncidentDetail.aspx?md={0}&id={1}&type={2}", mode, id, FormType);
            Page.Response.Redirect(url, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = PatientIncidents;
        }

        private DataTable PatientIncidents
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                PatientIncidentQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (PatientIncidentQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new PatientIncidentQuery("a");
                    var qsu = new ServiceUnitQuery("b");
                    var qreg = new RegistrationQuery("d");
                    var qpat = new PatientQuery("e");
                    var qrefgr = new AppStandardReferenceItemQuery("f");
                    var sal = new AppStandardReferenceItemQuery("sal");

                    query.InnerJoin(qsu).On(query.ServiceUnitIDInCharge == qsu.ServiceUnitID);
                    query.LeftJoin(qreg).On(query.RegistrationNo == qreg.RegistrationNo);
                    query.LeftJoin(qpat).On(qreg.PatientID == qpat.PatientID);
                    query.LeftJoin(qrefgr).On(query.SRIncidentGroup == qrefgr.ItemID & qrefgr.StandardReferenceID == AppEnum.StandardReference.IncidentGroup.ToString());
                    query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & sal.ItemID == qpat.SRSalutation);

                    if (FormType == "entry")
                    {
                        if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSCH")
                            query.Where(query.InsertByUserID == AppSession.UserLogin.UserID,
                                        query.Or(query.IsVerified == false, query.IsVerified.IsNull()));
                        else
                        {
                            var qusu = new AppUserServiceUnitQuery("c");
                            query.InnerJoin(qusu).On(query.ServiceUnitIDInCharge == qusu.ServiceUnitID);
                            query.Where(qusu.UserID == AppSession.UserLogin.UserID);
                        }
                    }
                    query.Where(query.IsRiskManagement == false);

                    query.OrderBy
                        (
                            query.IncidentDateTime.Descending
                        );

                    query.Select(
                        query.PatientIncidentNo,
                                query.IncidentDateTime,
                                query.RegistrationNo,
                                qpat.MedicalNo,
                                query.PatientName,
                                sal.ItemName.As("SalutationName"),
                                query.ServiceUnitIDInCharge.As("ServiceUnitID"),
                                qsu.ServiceUnitName,

                                query.SRIncidentType,
                                query.SRIncidentGroup,
                                query.SRClinicalImpact,
                                query.SRIncidentFollowUp,
                                query.ReportingDateTime,
                                query.ReportedByUserID,
                                qrefgr.ItemName.As("IncidentGroupName"),
                                query.IsApproved,
                                query.IsVerified
                        );
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}
