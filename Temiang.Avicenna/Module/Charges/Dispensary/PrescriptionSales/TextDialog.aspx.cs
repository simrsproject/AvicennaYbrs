using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.Module.Charges.Dispensary.PrescriptionSales
{
    public partial class TextDialog : BasePageDialog
    {
        TransPrescriptionItem _dt;
        
        protected void Page_Init(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            _dt = new TransPrescriptionItem(); 
            _dt.LoadByPrimaryKey(Request.QueryString["id"], Request.QueryString["seq"]);
            txtText1.Text = _dt.OrderText;
            txtText2.Text = _dt.IterText;
        }

        public override bool OnButtonOkClicked()
        {
            if (_dt == null)
            {
                _dt = new TransPrescriptionItem();
                _dt.LoadByPrimaryKey(Request.QueryString["id"], Request.QueryString["seq"]);
            }
            if (_dt.IsVoid == false)
            {
                _dt.OrderText = txtText1.Text;
                _dt.IterText = txtText2.Text;
                _dt.Save();

            }
            return true;
        }
    }
}
