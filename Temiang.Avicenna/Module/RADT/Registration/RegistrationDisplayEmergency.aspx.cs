using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;
using System.Web.UI;
using System.Drawing;

namespace Temiang.Avicenna.Module.RADT
{
    public partial class RegistrationDisplayEmergency : System.Web.UI.Page
    {
        Color bgc = Color.Black;
        Color fgcm = Color.DeepSkyBlue;
        Color fgcmf = Color.Lime;
        Color hbgc = Color.LimeGreen;
        Color hfgc = Color.Black;

        private string GetServiceUnitID()
        {
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
            else
            {
                pnlSU.Visible = false;
            }
        }

        protected void btnSU_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect("RegistrationDisplayEmergency.aspx?suid=" + cboSU.SelectedValue, true);
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
                grdList.DataSource = Reg;
        }

        protected void grdList_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                item["MedicalNo"].BackColor = bgc;
                item["PatientName"].BackColor = bgc;
                item["RegDate"].BackColor = bgc;
                item["Jam"].BackColor = bgc;


                item["MedicalNo"].ForeColor = fgcmf;
                item["PatientName"].ForeColor = fgcmf;
                item["RegDate"].ForeColor = fgcmf;
                item["Jam"].ForeColor = fgcmf;
            }
        }

        private DataTable Reg
        {
            get
            { 
                return (new RegistrationCollection()).GetRegistrationDisplayEmergency(Request.UserHostAddress, GetServiceUnitID());
            }
        }

    }
}