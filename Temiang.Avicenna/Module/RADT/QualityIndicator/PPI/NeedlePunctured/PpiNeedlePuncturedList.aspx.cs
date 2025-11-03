using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.RADT.QualityIndicator
{
    public partial class PpiNeedlePuncturedList : BasePageList
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

            UrlPageSearch = "PpiNeedlePuncturedSearch.aspx?type=" + FormType;
            UrlPageDetail = "PpiNeedlePuncturedDetail.aspx?type=" + FormType;
            ProgramID = FormType == ""
                            ? AppConstant.Program.PpiNeedlePuncturedSurveillance
                            : AppConstant.Program.PpiNeedlePuncturedSurveillanceVerified;

            if (!IsPostBack)
            {
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
            string id = dataItem.GetDataKeyValue(PpiNeedlePuncturedMetadata.ColumnNames.TransactionNo).ToString();
            string url = string.Format("PpiNeedlePuncturedDetail.aspx?md={0}&id={1}&type={2}", mode, id, FormType);
            Page.Response.Redirect(url, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = PpiNeedlePunctureds;
        }

        private DataTable PpiNeedlePunctureds
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                PpiNeedlePuncturedQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (PpiNeedlePuncturedQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new PpiNeedlePuncturedQuery("a");
                    var usr = new AppUserQuery("b");
                    query.LeftJoin(usr).On(query.VerifiedByUserID == usr.UserID);

                    if (FormType == "")
                        query.Where(query.CreatedByUserID == AppSession.UserLogin.UserID);
                    else 
                        query.Where(query.IsApproved == true);

                    query.OrderBy
                        (
                            query.TransactionNo.Descending
                        );

                    query.Select(
                        query.TransactionNo,
                        query.TransactionDate,
                        query.OfficerName,
                        query.DatePunctured,
                        query.PuncturedAreas,
                        query.CausePunctured,
                        query.FollowUpDate,
                        query.FollowUp,
                        query.FollowUpBy,
                        query.IsApproved,
                        query.IsVoid,
                        query.IsVerified,
                        query.VerifiedDateTime,
                        usr.UserName.As("VerifiedBy")
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
