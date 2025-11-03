using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.PCareCommon
{
    public partial class PCareReferenceCtl : System.Web.UI.UserControl
    {
        public Bridging.PCare.Common.Constant.ReferenceType ReferenceType
        {
            get
            {
                try
                {
                    return (Bridging.PCare.Common.Constant.ReferenceType)ViewState["PCareReferenceType"];

                }
                catch
                {

                    return Bridging.PCare.Common.Constant.ReferenceType.PoliFktp;
                }

            }
            set { ViewState["PCareReferenceType"] = value; }
        }

        public bool IsPCareValidation
        {
            get
            {
                if (ViewState["PCareVal"] == null)
                {
                    var pCareValidation = ConfigurationManager.AppSettings["PCareValidation"];
                    ViewState["PCareVal"] = !string.IsNullOrEmpty(pCareValidation) && pCareValidation.ToLower().Equals("yes");
                }
                return (bool)ViewState["PCareVal"];
            }
        }

        public string ItemID
        {
            get { return txtPCareItemID.Text; }
            set
            {
                if (!IsPCareValidation) return;
                txtPCareItemID.Text = value;
                PopulateItemName(txtPCareItemID.Text);
            }
        }
        public void Delete(string mappingWithId)
        {
            if (!IsPCareValidation) return;
            var pcareMap = new PCareReferenceItemMapping();
            if (pcareMap.LoadByPrimaryKey(ReferenceType.ToString(), mappingWithId))
            {
                pcareMap.MarkAsDeleted();
                pcareMap.Save();
            }
        }
        public void Save(string mappingWithId)
        {
            if (!IsPCareValidation) return;
            var pcareMap = new PCareReferenceItemMapping();
            if (!pcareMap.LoadByPrimaryKey(ReferenceType.ToString(), mappingWithId))
            {
                if (string.IsNullOrWhiteSpace(mappingWithId)) return;

                // Add
                pcareMap = new PCareReferenceItemMapping();
                pcareMap.ReferenceID = ReferenceType.ToString();
                pcareMap.MappingWithID = mappingWithId;
            }
            pcareMap.ItemID = ItemID;
            pcareMap.Save();
        }
        public void Populate(string mappingWithId)
        {
            if (!IsPCareValidation) return;

            // Pcare Map
            if (string.IsNullOrEmpty(mappingWithId))
                ItemID = string.Empty;
            else
            {
                var pcareMap = new PCareReferenceItemMapping();
                pcareMap.LoadByPrimaryKey(ReferenceType.ToString(), mappingWithId);
                ItemID = pcareMap.ItemID;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Page_Init(object sender, EventArgs e)
        {
            this.Visible = IsPCareValidation;
        }
        protected void txtPCareItemID_TextChanged(object sender, EventArgs e)
        {
            PopulateItemName(txtPCareItemID.Text);
        }

        private void PopulateItemName(string itemID)
        {
            lblPCareItemName.Text = string.Empty;
            if (string.IsNullOrEmpty(itemID))
                return;

            var stdi = new PCareReferenceItem();
            if (stdi.LoadByPrimaryKey(ReferenceType.ToString(), itemID))
                lblPCareItemName.Text = stdi.ItemName;
            else
                lblPCareItemName.Text = string.Empty;
        }
    }
}