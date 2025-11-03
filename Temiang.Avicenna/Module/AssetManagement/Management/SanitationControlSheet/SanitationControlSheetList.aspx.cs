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

namespace Temiang.Avicenna.Module.AssetManagement.Management
{
    public partial class SanitationControlSheetList : BasePage
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e); // !!Jika tidak dipanggil, tampilan jadi tidak rapih

            ProgramID = AppConstant.Program.SanitationControlSheet;

            if (!IsPostBack)
            {
                var sheetColl = new QuestionFormCollection();
                var sheetq = new QuestionFormQuery("a");
                var unitq = new QuestionFormInServiceUnitQuery("b");
                sheetq.InnerJoin(unitq).On(unitq.QuestionFormID == sheetq.QuestionFormID && unitq.ServiceUnitID == AppSession.Parameter.ServiceUnitSanitationId.ToString());
                sheetq.OrderBy(sheetq.QuestionFormID.Ascending);
                sheetColl.Load(sheetq);

                cboQuestionFormID.Items.Clear();
                cboQuestionFormID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (var item in sheetColl)
                {
                    cboQuestionFormID.Items.Add(new RadComboBoxItem(item.QuestionFormName, item.QuestionFormID));
                }
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (IsPostBack) return;

            RestoreValueFromCookie();
        }

        protected void grdControlSheet_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdControlSheet.DataSource = QuestionForms;
        }

        private DataTable QuestionForms
        {
            get
            {
                var query = new QuestionFormQuery("a");
                var unitq = new QuestionFormInServiceUnitQuery("b");

                query.InnerJoin(unitq).On(unitq.QuestionFormID == query.QuestionFormID && unitq.ServiceUnitID == AppSession.Parameter.ServiceUnitSanitationId.ToString());

                query.OrderBy(query.QuestionFormID.Ascending);

                query.Select(
                    query.QuestionFormID,
                    query.QuestionFormName);
               
                return query.LoadDataTable();
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = SanitationControlSheets;
        }

        private DataTable SanitationControlSheets
        {
            get
            {
                var query = new SanitationControlSheetQuery("a");
                var sheetq = new QuestionFormQuery("b");

                query.InnerJoin(sheetq).On(sheetq.QuestionFormID == query.QuestionFormID);

                query.Select(
                    query.ControlSheetNo,
                    query.QuestionFormID,
                    sheetq.QuestionFormName,
                    query.ControlDate,
                    query.IsApproved,
                    query.IsVoid
                    );
                
                if (!txtControlFromDate.IsEmpty && !txtControlToDate.IsEmpty)
                {
                    query.Where(query.ControlDate >= txtControlFromDate.SelectedDate, query.ControlDate <= txtControlToDate.SelectedDate);
                }
                if (!string.IsNullOrEmpty(cboQuestionFormID.SelectedValue))
                {
                    query.Where(query.QuestionFormID == cboQuestionFormID.SelectedValue);
                }

                query.OrderBy(query.ControlDate.Descending);

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                return query.LoadDataTable();
            }
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            SaveValueToCookie();
            grdList.Rebind();
        }
    }
}