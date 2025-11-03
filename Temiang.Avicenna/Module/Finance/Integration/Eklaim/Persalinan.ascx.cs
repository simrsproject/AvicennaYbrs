using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Integration.Eklaim
{
    public partial class Persalinan : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;
                hdnSequence.Value = ((Session["deliveryEklaim"] as List<Delivery>).Count + 1).ToString();
                return;
            }
            ViewState["IsNewRecord"] = false;

            hdnSequence.Value = (string)DataBinder.Eval(DataItem, "DeliverySequence");
            cboDeliveryMethod.SelectedValue = (string)DataBinder.Eval(DataItem, "DeliveryMethod");

            var waktu = ((string)DataBinder.Eval(DataItem, "DeliveryDttm")).Split(' ');

            DateTime.TryParseExact(waktu[0].Trim(), "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out var tanggal);
            txtTanggalKelahiran.SelectedDate = tanggal;

            var jam = TimeSpan.ParseExact(waktu[1].Trim(), "hh\\:mm\\:ss", null);
            txtJamKelahiran.SelectedTime = jam;

            cboLetakJanin.SelectedValue = (string)DataBinder.Eval(DataItem, "LetakJanin");
            cboKondisi.SelectedValue = (string)DataBinder.Eval(DataItem, "Kondisi");
            chkBantuanManual.Checked = (string)DataBinder.Eval(DataItem, "UseManual") == "1" ? true : false;
            chkForcep.Checked = (string)DataBinder.Eval(DataItem, "UseForcep") == "1" ? true : false;
            chkVacuum.Checked = (string)DataBinder.Eval(DataItem, "UseVacuum") == "1" ? true : false;
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {

        }

        public string DeliverySequence
        {
            get { return hdnSequence.Value; }
        }

        public string DeliveryMethod
        {
            get { return cboDeliveryMethod.SelectedValue; }
        }

        public string DeliveryDttm
        {
            get { return $"{txtTanggalKelahiran.SelectedDate.Value.ToString("yyyy-MM-dd")} {txtJamKelahiran.SelectedTime.Value.ToString("hh\\:mm\\:ss")}"; }
        }

        public string LetakJanin
        {
            get { return cboLetakJanin.SelectedValue; }
        }

        public string Kondisi
        {
            get { return cboKondisi.SelectedValue; }
        }

        public string UseManual
        {
            get { return chkBantuanManual.Checked ? "1" : "0"; }
        }

        public string UseForcep
        {
            get { return chkForcep.Checked ? "1" : "0"; }
        }

        public string UseVacuum
        {
            get { return chkVacuum.Checked ? "1" : "0"; }
        }
    }
}