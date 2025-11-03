using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.AssetManagement.Master
{
    public partial class AssetDepreciationEditor : BaseUserControl
    {
        public object DataItem { get; set; }

        protected override void OnDataBinding(EventArgs e)
        {
        }

        #region Method & Event
        protected void Page_Init(object sender, EventArgs e)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
        #endregion

        #region Properties for return entry value
        public string AssetId
        {
            get { return this.lblAssetId.Text; }
            set { this.lblAssetId.Text = value; }
        }
        #endregion
    }
}