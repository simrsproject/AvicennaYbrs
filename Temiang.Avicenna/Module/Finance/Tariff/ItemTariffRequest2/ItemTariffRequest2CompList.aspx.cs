using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Tariff
{
    public partial class ItemTariffRequest2CompList : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ItemServiceTariffRequest2;

            if (!IsPostBack)
            {
                var item = new Item();
                item.LoadByPrimaryKey(Request.QueryString["itemId"].ToString());

                var cls = new Class();
                cls.LoadByPrimaryKey(Request.QueryString["classId"].ToString());

                this.Title = "Item : " + item.ItemName + " [" + item.ItemID + "] | Class : " + cls.ClassName;

                (Helper.FindControlRecursive(this, "btnCancel") as Button).Visible = false;
            }

        }

        private ItemTariffRequest2ItemCompCollection ItemTariffRequestItemComps
        {
            get
            {
                object obj = Session["ItemTariffRequest2ItemComps" + Request.UserHostName];
                if (obj != null)
                {
                    return ((ItemTariffRequest2ItemCompCollection)(obj));
                }
                var qa = new ItemTariffRequest2ItemCompQuery("a");
                var qb = new TariffComponentQuery("b");

                qa.InnerJoin(qb).On(qa.TariffComponentID == qb.TariffComponentID);
                qa.Select(qa.SelectAllExcept(), qb.TariffComponentName.As("refToTariffComponent_TariffComponentName"));
                qa.Where(qa.TariffRequestNo == Request.QueryString["trn"].ToString());
                var coll = new ItemTariffRequest2ItemCompCollection();
                coll.Load(qa);
                Session["ItemTariffRequest2ItemComps" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["ItemTariffRequest2ItemComps" + Request.UserHostName] = value; }
        }

        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var ds = from d in ItemTariffRequestItemComps
                     where d.TariffRequestNo == Request.QueryString["trn"].ToString() && d.ItemID == Request.QueryString["itemId"].ToString() && d.ClassID == Request.QueryString["classId"].ToString()
                     select d;
            grdItem.DataSource = ds;
        }
    }
}