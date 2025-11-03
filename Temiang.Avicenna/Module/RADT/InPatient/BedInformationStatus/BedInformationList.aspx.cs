using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;
using System.Web.UI;
using System.Drawing;

namespace Temiang.Avicenna.Module.RADT.InPatient
{
    public partial class BedInformationList : System.Web.UI.Page
    {
        Color bgc = Color.Black;
        Color fgcm = Color.DeepSkyBlue;
        Color fgcf = Color.HotPink;
        Color fgcmf = Color.Lime;
        Color hbgc = Color.LimeGreen;
        Color hfgc = Color.Black;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        protected override void OnInitComplete(EventArgs e)
        {
            //Apply Skin 
            var radSkinManager = Helper.FindFirstRadSkinManager(Page);
            if (radSkinManager != null)
            {
                radSkinManager.Skin = "WebBlue";
                radSkinManager.TargetControls.Add(grdList.ID, "WebBlue");
                radSkinManager.ApplySkin(grdList, "WebBlue");
            }
            grdList.HeaderStyle.BackColor = bgc;
            grdList.HeaderStyle.ForeColor = fgcm;
            foreach (GridColumn col in grdList.Columns)
            {
                col.HeaderStyle.CssClass = "";
                col.HeaderStyle.BorderStyle = System.Web.UI.WebControls.BorderStyle.Solid;
                col.HeaderStyle.BorderWidth = 1;
                col.HeaderStyle.BorderColor = Color.White;
                col.HeaderStyle.BackColor = hbgc;
                col.HeaderStyle.ForeColor = hfgc;
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
                grdList.DataSource = Beds;
        }


        protected void grdList_ItemDataBound(object sender, GridItemEventArgs e)
        {
            string gen = "M";
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                item["ServiceUnitName"].BackColor = bgc;
                item["ClassName"].BackColor = bgc;
                item["Gender"].BackColor = bgc;
                item["Jumlah"].BackColor = bgc;
                item["Occupied"].BackColor = bgc;
                item["Occupied"].BackColor = bgc;
                item["Ready"].BackColor = bgc;

                gen = item["Gen"].Text;

                item["ServiceUnitName"].ForeColor = fgcmf;
                item["ClassName"].ForeColor = fgcmf;
                item["Gender"].ForeColor = gen.Equals("M") ? fgcm : (gen.Equals("F") ? fgcf : fgcmf);
                item["Jumlah"].ForeColor = gen.Equals("M") ? fgcm : (gen.Equals("F") ? fgcf : fgcmf);
                item["Occupied"].ForeColor = gen.Equals("M") ? fgcm : (gen.Equals("F") ? fgcf : fgcmf);
                item["Occupied"].ForeColor = gen.Equals("M") ? fgcm : (gen.Equals("F") ? fgcf : fgcmf);
                item["Ready"].ForeColor = gen.Equals("M") ? fgcm : (gen.Equals("F") ? fgcf : fgcmf);
            }
        }


        private DataTable Beds
        {
            get
            {
                //var b = new BedQuery("b");
                //var sr = new ServiceRoomQuery("sr");
                //var su = new ServiceUnitQuery("su");
                //var c = new ClassQuery("c");
                //var bs = new AppStandardReferenceItemQuery("bs");
                //var gen = new AppStandardReferenceItemQuery("gen");

                //b.InnerJoin(sr).On(b.RoomID.Equal(sr.RoomID))
                //    .InnerJoin(su).On(sr.ServiceUnitID.Equal(su.ServiceUnitID))
                //    .InnerJoin(c).On(b.ClassID.Equal(c.ClassID))
                //    .InnerJoin(bs).On(bs.Equals("BedStatus") && b.SRBedStatus.Equal(bs.ItemID))
                //    .LeftJoin(gen).On(
                return (new BedCollection()).GetBedInformationSummary(Request.UserHostAddress);
            }
        }
    }
}
