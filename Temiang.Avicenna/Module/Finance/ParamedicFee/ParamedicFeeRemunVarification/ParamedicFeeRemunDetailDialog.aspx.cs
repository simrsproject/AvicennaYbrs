using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Finance.ParamedicFee
{
    public partial class ParamedicFeeRemunDetailDialog : BasePageDialog
    {
        private DataTable feeRemunDetail
        {
            get
            {
                return (DataTable)Session["feeRemunDetail"];
            }
        }
        private string ParamedicID {
            get {
                return Request.QueryString["parid"];
            }
        }

        public string RemunNo {
            get {
                return Request.QueryString["remunno"];
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ButtonOk.Visible = false;
                ButtonCancel.Text = "Close";

                var ds = feeRemunDetail.AsEnumerable().Where(x => x["ParamedicID"].ToString() == ParamedicID).AsDataView().ToTable();
                grdRemunParamedic.DataSource = ds;
                grdRemunParamedic.DataBind();
            }
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'rebind'";
        }

        public override bool OnButtonOkClicked()
        {
            return true;
        }

        protected void grdRemunParamedic_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                item["RvuConversion"].ToolTip = "Rvu / Adjustment Factor";
                item["Coefficient"].ToolTip = "Qty * Score * RvuConversion * Multiplier Factor";
            }
        }
    }
}
