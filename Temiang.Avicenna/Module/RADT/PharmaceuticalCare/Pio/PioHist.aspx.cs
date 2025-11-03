using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Drawing;
using System.Web;
using Telerik.Web.UI;
using Temiang.Dal.DynamicQuery;
using System.Web.UI;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.BusinessObject;
using System.Text;

namespace Temiang.Avicenna.Module.RADT.PharmaceuticalCare
{

    public partial class PioHist : BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.DrugInfService;
            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRPioCategory, AppEnum.StandardReference.PioCategory);
                StandardReference.InitializeIncludeSpace(cboSRPioSource, AppEnum.StandardReference.PioSource);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
            {
                RestoreValueFromCookie();

                if (txtDate.IsEmpty)
                    txtDate.SelectedDate = DateTime.Today;
            }
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var pio = new PioQuery("pio");
            var stdi = new AppStandardReferenceItemQuery("oc");
            pio.LeftJoin(stdi).On(stdi.StandardReferenceID == "Occupation" & stdi.ItemID == pio.SROccupation);

            var crtBy = new AppUserQuery("rb");
            pio.LeftJoin(crtBy).On(pio.CreatedByUserID == crtBy.UserID);

            pio.es.Top = AppSession.Parameter.MaxResultRecord;
            pio.OrderBy(pio.PioNo.Descending);

            pio.Select(pio, stdi.ItemName.As("OccupationName"),
                        crtBy.UserName.As("CreatedByUserName"),
                        crtBy.LicenseNo);

            if (!txtDate.IsEmpty)
                pio.Where(pio.PioDateTime >= txtDate.SelectedDate.Value, pio.PioDateTime < txtDate.SelectedDate.Value.AddDays(1));

            if (!string.IsNullOrWhiteSpace(txtQuestion.Text))
            {
                pio.Where(pio.Question.Like("%"+txtQuestion.Text.Trim()+"%"));
            }

            if (!string.IsNullOrWhiteSpace(cboSRPioCategory.SelectedValue))
            {
                var cat = new PioCategoryLineQuery("c");
                pio.InnerJoin(cat).On(pio.PioNo == cat.PioNo);
                pio.Where(cat.SRPioCategory == cboSRPioCategory.SelectedValue);
            }

            if (!string.IsNullOrWhiteSpace(cboSRPioSource.SelectedValue))
            {
                var src = new PioSourceLineQuery("c");
                pio.InnerJoin(src).On(pio.PioNo == src.PioNo);
                pio.Where(src.SRPioSource == cboSRPioSource.SelectedValue);
            }

            grdList.DataSource = pio.LoadDataTable();
        }

        protected string CategoryLineHtml(GridItem container)
        {
            var no = DataBinder.Eval(container.DataItem, "PioNo").ToInt();

            var dtb = CategoryLineDataTable(no);
            var strb = new StringBuilder();
            strb.AppendLine("<table id='categoryLine'>");
            foreach (DataRow row in dtb.Rows)
            {
                strb.AppendFormat("<tr><td> • {0}</td></tr>", row["ItemName"]);
            }
            var other = DataBinder.Eval(container.DataItem, "OtherCategory");
            if (other != null && !string.IsNullOrWhiteSpace(other.ToString()))
                strb.AppendFormat("<tr><td> • Other: {0}</td></tr>", other);

            strb.AppendLine("</table>");

            return strb.ToString();

        }
        private DataTable CategoryLineDataTable(int pioNo)
        {
            var que = new AppStandardReferenceItemQuery("sri");
            var qrLine = new PioCategoryLineQuery("a");

            que.InnerJoin(qrLine).On(que.ItemID == qrLine.SRPioCategory);

            que.Where(que.StandardReferenceID == "PioCategory", qrLine.PioNo == pioNo);
            que.OrderBy(que.LineNumber.Ascending);
            que.Select(que.ItemID, que.ItemName);

            return que.LoadDataTable();
        }

        protected string SourceLineHtml(GridItem container)
        {
            var no = DataBinder.Eval(container.DataItem, "PioNo").ToInt();

            var dtb = SourceLineDataTable(no);
            var strb = new StringBuilder();
            strb.AppendLine("<table id='sourceLine'>");

            foreach (DataRow row in dtb.Rows)
            {
                strb.AppendFormat("<tr><td> • {0}</td></tr>", row["ItemName"]);
            }
            var other = DataBinder.Eval(container.DataItem, "OtherSources");
            if (other != null && !string.IsNullOrWhiteSpace(other.ToString()))
                strb.AppendFormat("<tr><td> • Other: {0}</td></tr>", other);

            strb.AppendLine("</table>");

            return strb.ToString();

        }
        private DataTable SourceLineDataTable(int pioNo)
        {
            var que = new AppStandardReferenceItemQuery("sri");
            var qrLine = new PioSourceLineQuery("a");

            que.InnerJoin(qrLine).On(que.ItemID == qrLine.SRPioSource);

            que.Where(que.StandardReferenceID == "PioSource", qrLine.PioNo == pioNo);
            que.OrderBy(que.LineNumber.Ascending);
            que.Select(que.ItemID, que.ItemName);
            return que.LoadDataTable();
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            grdList.CurrentPageIndex = 0;
            grdList.Rebind();
            SaveValueToCookie();
        }

    }
}
