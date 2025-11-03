using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI.WebControls;
using System.Linq;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class ExposureFactorDialog : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();

                if (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsUsingItemConsAndExpFactorOnJORealizationList) == "Yes")
                {
                    (Helper.FindControlRecursive(this, "btnOk") as Button).Visible = false;
                    (Helper.FindControlRecursive(this, "btnCancel") as Button).Text = "Close";
                }
            }
        }

        private void LoadData()
        {
            txtItemID.Text = Request.QueryString["itemID"];
            var i = new Item();
            i.LoadByPrimaryKey(txtItemID.Text);
            lblItemName.Text = i.ItemName;

            var entity = new TransChargesItemFilmConsumption();
            if (entity.LoadByPrimaryKey(Request.QueryString["joNo"].ToString(), Request.QueryString["seqNo"].ToString(), string.Empty))
            {
                txtKvF.Value = Convert.ToDouble(entity.Kv);
                txtMaF.Value = Convert.ToDouble(entity.Ma);
                txtSF.Value = Convert.ToDouble(entity.S);
                txtMasF.Value = Convert.ToDouble(entity.Mas);
                txtKvC.Value = Convert.ToDouble(entity.KvC);
                txtMaC.Value = Convert.ToDouble(entity.MaC);
                txtSC.Value = Convert.ToDouble(entity.SC);
                txtMasC.Value = Convert.ToDouble(entity.MasC);
                txtFfd.Value = Convert.ToDouble(entity.Ffd);
                txtScreeningTime.Value = Convert.ToDouble(entity.ScreeningTime);
                txtCineTime.Value = Convert.ToDouble(entity.CineTime);
                txtNotes.Text = entity.Notes;
            }
            else {
                txtKvF.Value = 0;
                txtMaF.Value = 0;
                txtSF.Value = 0;
                txtMasF.Value = 0;
                txtKvC.Value = 0;
                txtMaC.Value = 0;
                txtSC.Value = 0;
                txtMasC.Value = 0;
                txtFfd.Value = 0;
                txtScreeningTime.Value = 0;
                txtCineTime.Value = 0;
                txtNotes.Text = string.Empty;
            }
        }

        public override bool OnButtonOkClicked()
        {
            var entity = new TransChargesItemFilmConsumption();
            if (!entity.LoadByPrimaryKey(Request.QueryString["joNo"].ToString(), Request.QueryString["seqNo"].ToString(), string.Empty))
                entity.AddNew();

            entity.TransactionNo = Request.QueryString["joNo"];
            entity.SequenceNo = Request.QueryString["seqNo"];
            entity.SRFilmID = string.Empty;
            entity.FilmName = string.Empty;
            entity.Qty = 0;

            entity.Kv = Convert.ToDecimal(txtKvF.Value);
            entity.Ma = Convert.ToDecimal(txtMaF.Value); ;
            entity.S = Convert.ToDecimal(txtSF.Value); ;
            entity.Mas = Convert.ToDecimal(txtMasF.Value); ;
            entity.KvC = Convert.ToDecimal(txtKvC.Value); ;
            entity.MaC = Convert.ToDecimal(txtMaC.Value); ;
            entity.SC = Convert.ToDecimal(txtSC.Value); ;
            entity.MasC = Convert.ToDecimal(txtMasC.Value); ;
            entity.Ffd = Convert.ToDecimal(txtFfd.Value); ;
            entity.ScreeningTime = Convert.ToDecimal(txtScreeningTime.Value); ;
            entity.CineTime = Convert.ToDecimal(txtCineTime.Value); ;
            entity.Notes = txtNotes.Text;

            entity.LastUpdateDateTime = DateTime.Now;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;

            entity.Save();

            return true;
        }
    }
}