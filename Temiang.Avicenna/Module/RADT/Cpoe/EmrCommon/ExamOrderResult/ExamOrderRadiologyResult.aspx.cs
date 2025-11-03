using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
using Telerik.Web.UI;
using System.Data;

namespace Temiang.Avicenna.Module.Emr
{
    public partial class ExamOrderRadiologyResult : BasePageDialog
    {
        private string TransactionNo
        {
            get { return Request.QueryString["trno"]; }
        }
        protected void Page_Init(object sender, EventArgs e)
        {
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ButtonCancel.Text = "Close";
            ButtonOk.Visible = false;
        }
        #region Imaging Result

        protected void grdImagingResult_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!IsPostBack || (IsPostBack && (Session["imgresult"] == null || !Equals(Session["imgresult_trno"], TransactionNo))))
            {
                Session["imgresult"] = ImagingResultCollections();
                Session["imgresult_trno"] = TransactionNo;
            }
            grdImagingResult.DataSource = Session["imgresult"];
        }

        private DataTable ImagingResultCollections()
        {
            var test = new TestResultQuery("a");
            var tci = new TransChargesItemQuery("b");
            test.InnerJoin(tci).On(test.TransactionNo == tci.TransactionNo && test.ItemID == tci.ItemID && tci.IsVoid == false);

            var med = new ParamedicQuery("c");
            test.InnerJoin(med).On(test.ParamedicID == med.ParamedicID);

            var item = new ItemQuery("d");
            test.InnerJoin(item).On(test.ItemID == item.ItemID);

            var tc = new TransChargesQuery("e");
            test.InnerJoin(tc).On(test.TransactionNo == tc.TransactionNo && tc.IsVoid == false);

            test.Where(test.TransactionNo == TransactionNo);
            test.Select
                (
                    test.TransactionNo,
                    test.TestResultDateTime,
                    med.ParamedicName,
                    test.ItemID,
                    item.ItemName,
                    test.TestResult
                );
            test.OrderBy(test.TestResultDateTime.Descending);

            return test.LoadDataTable();
        }

        #endregion
    }
}
