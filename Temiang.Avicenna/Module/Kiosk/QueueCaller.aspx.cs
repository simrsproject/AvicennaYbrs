using System;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna
{
    public partial class QueueCaller : BasePage
    {
        public string getUserID() {
            return AppSession.UserLogin.UserID;
        }

        public string refid {
            get {
                return Request.QueryString["refid"];
            }
        }
        public string[] refidArray {
            get {
                return refid.Split(',');
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ProgramID = AppConstant.Program.QueueList;
            
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
            if (!IsPostBack)
            {
                var u = new AppUser();
                if (u.LoadByPrimaryKey(getUserID())) {
                    txtCounterName.Text = u.LastCounterName;
                }
            }
        }

        public void Page_Load()
        {
            if (!IsPostBack)
            {
                var stdRef = new AppStandardReferenceItemCollection();
                stdRef.LoadByStandardReferenceID("KioskQueueType");
                foreach (var std in stdRef.Where(s => s.IsUsedBySystem ?? false).OrderBy(s => s.ItemID)) {
                    cblQueueGroup.Items.Add(new System.Web.UI.WebControls.ListItem(std.ItemName, std.ItemID));
                }
            }
        }

        protected void grdQueueType_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            //var qtColl = new AppStandardReferenceItemCollection();
            //qtColl.Query.Where(qtColl.Query.StandardReferenceID == AppEnum.StandardReference.KioskQueueType,
            //    qtColl.Query.IsActive == true);
            //qtColl.LoadAll();

            var selectedGroup = cblQueueGroup.Items.Cast<ListItem>()
                .Where(li => li.Selected)
                .Select(li => li.Value)
                .ToList();

            if (selectedGroup.Count == 0)
                selectedGroup.Add("xxxxxxx");

            var a = new AppStandardReferenceItemQuery("a");
            var b = new AppStandardReferenceQuery("b");
            var c = new AppStandardReferenceItemQuery("c");
           
            a.InnerJoin(b).On(a.StandardReferenceID == b.StandardReferenceGroup)
                .InnerJoin(c).On(b.StandardReferenceID == c.StandardReferenceID && a.ItemID == c.ReferenceID)
                .Where(a.StandardReferenceID == "KioskQueueType", a.ItemID.In(selectedGroup), c.ItemID.IsNotNull(), a.IsUsedBySystem == true)
                .Select(c.ItemID, "<a.ItemName + ' ' + c.ItemName as ItemName>", c.ReferenceID);
            var dtb2 = a.LoadDataTable();

            var xxx = dtb2.AsEnumerable().Select(x => x["ReferenceID"].ToString()).Distinct().ToList();

            if (!xxx.Any())
            {
                xxx.Add("xxxxxxx");
            }

            a = new AppStandardReferenceItemQuery("a");
            a.Where(a.StandardReferenceID == "KioskQueueType", a.ItemID.In(selectedGroup), a.ItemID.NotIn(xxx), a.IsUsedBySystem == true)
                .Select(a.ItemID, a.ItemName, a.ReferenceID);
            a.es.Distinct = true;
            var dtb = a.LoadDataTable();

            dtb.Merge(dtb2);

            ((RadGrid)source).DataSource = dtb;
        }

        protected void grdQueueType_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem) {
                var d = (e.Item as GridDataItem);
                d["ItemName"].Text = d["ItemName"].Text.ToString().Replace("\\n", " ").Replace("|", "<br />");
            }
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            grdQueueType.Rebind();
        }
    }
}