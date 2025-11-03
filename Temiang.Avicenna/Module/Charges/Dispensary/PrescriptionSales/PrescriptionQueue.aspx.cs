using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;
using System.Web.UI;
using System.Drawing;

namespace Temiang.Avicenna.Module.Charges.Dispensary
{
    public partial class PrescriptionQueue : System.Web.UI.Page
    {
        //Color bgc = Color.Black;
        //Color fgc = Color.Lime;

        //Color fg0 = Color.White; //Color.DeepSkyBlue;
        //Color fg1 = Color.WhiteSmoke;
        //Color fg2 = Color.White;
        //Color fg3 = Color.Lime;

        //Color hbgc = Color.LimeGreen;
        //Color hfgc = Color.Black;

        Color bgc = Color.White;
        Color fgc = Color.Maroon;

        Color fg0 = Color.Black; //Color.DeepSkyBlue;
        Color fg1 = Color.Black;
        Color fg2 = Color.Black;
        Color fg3 = Color.Black;

        Color hbgc = Color.Maroon;
        Color hfgc = Color.White;

        private string GetServiceUnitID() {
            return Request.QueryString["suid"];
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (string.IsNullOrEmpty(GetServiceUnitID()))
            {
                pnlSU.Visible = true;
                var suColl = new ServiceUnitCollection();
                suColl.Query.Where(suColl.Query.IsActive == true, suColl.Query.IsDispensaryUnit == true);
                suColl.LoadAll();
                foreach (var su in suColl)
                {
                    cboSU.Items.Add(new RadComboBoxItem(su.ServiceUnitName, su.ServiceUnitID));
                }
            }
            else {
                pnlSU.Visible = false;
            }
        }

        protected void btnSU_Click(object sender, EventArgs e) {
            Page.Response.Redirect("PrescriptionQueue.aspx?suid=" + cboSU.SelectedValue, true);
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
            StylingGrid(grdList);
            StylingGrid(grdListComplete);
        }

        private void StylingGrid(RadGrid rg) {
            rg.HeaderStyle.BackColor = bgc;
            rg.HeaderStyle.ForeColor = fgc;
            foreach (GridColumn col in rg.Columns)
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
            if (string.IsNullOrEmpty(GetServiceUnitID())) return;
            var rg = (RadGrid)source;
            rg.DataSource = rg.ID.Equals("grdList") ? GetPrescriptionQueue(false) : GetPrescriptionQueue(true);
        }

        protected void grdList_ItemDataBound(object sender, GridItemEventArgs e)
        {
            string Status = "0";
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                item["PrescriptionNo"].BackColor = bgc;
                item["PatientName"].BackColor = bgc;
                item["Duration"].BackColor = bgc;
                item["StatusName"].BackColor = bgc;
                item["Flag"].BackColor = bgc;

                Status = item["Status"].Text;
                Color cl;
                switch (Status){
                    case "0": {cl = fg0; break;}
                    case "1": { cl = fg1; break; }
                    case "2": { cl = fg2; break; }
                    case "3": { cl = fg3; break; }
                    default: {cl = Color.White; break;}
                }


                item["PrescriptionNo"].ForeColor = cl;
                item["PatientName"].ForeColor = cl;
                item["Duration"].ForeColor = cl;
                item["StatusName"].ForeColor = cl;
                item["Flag"].ForeColor = cl;
            }
        }

        private DataTable GetPrescriptionQueue(bool IsComplete)
        {
            return (new TransPrescriptionCollection())
                .GetQueueWithPaging(Request.UserHostAddress, GetServiceUnitID(), IsComplete);
        }
    }
}
