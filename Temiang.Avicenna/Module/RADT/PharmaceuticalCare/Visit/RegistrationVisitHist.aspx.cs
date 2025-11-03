using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;


namespace Temiang.Avicenna.Module.RADT.PharmaceuticalCare
{
    public partial class RegistrationVisitHist : BasePageDialog
    {
        public override string RegistrationNo => Request.QueryString["regno"];
        public override string PatientID => Request.QueryString["patid"];

        protected void Page_Init(object sender, EventArgs e)
        {
            if (AppConstant.Program.PharmaceuticalCare.Equals(Request.QueryString["prgid"]))
                ProgramID = AppConstant.Program.PharmaceuticalCare;
            else
                ProgramID = AppConstant.Program.ElectronicMedicalRecord;

            if (!IsPostBack)
            {
                this.Title = "Visit Notes";
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            Footer.Visible = false;
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = VisitNotes(RegistrationNo);
        }

        private DataTable VisitNotes(string registrationNo)
        {
            var vis = new RegistrationVisitQuery("v");
            var usr = new AppUserQuery("u");
            vis.InnerJoin(usr).On(vis.CreatedByUserID == usr.UserID);
            vis.Select(vis.VisitNo, vis.VisitNotes.Coalesce("''").As("VisitNotes"),
                vis.VisitDateTime, vis.CreatedByUserID, vis.CreatedDateTime, vis.IsDeleted, usr.UserName.As("CreatedByUserName"), usr.LicenseNo);
            vis.Where(vis.RegistrationNo == registrationNo);
            vis.OrderBy(vis.VisitNo.Descending);
            return vis.LoadDataTable();
        }

        protected void grdList_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                if (item.GetDataKeyValue("IsDeleted") == DBNull.Value) return;
                var isVoid = Convert.ToBoolean(item.GetDataKeyValue("IsDeleted"));
                //TableCell cell = item["ItemName"];
                if (isVoid)
                {
                    item.Style.Add(HtmlTextWriterStyle.TextDecoration, "line-through");
                    item.Style.Add(HtmlTextWriterStyle.Color, "gray");
                }
            }
        }

        protected void grdList_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            var visitNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]["VisitNo"]).ToInt();

            var ent = new RegistrationVisit();
            if (ent.LoadByPrimaryKey(RegistrationNo, visitNo))
            {
                ent.IsDeleted = true;
                ent.Save();
            }

            ((RadGrid)sender).Rebind();
        }
    }
}
