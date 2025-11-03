using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.JsonField.Assesment;

namespace Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl
{
    public partial class PhysicalExamMethodCtl : System.Web.UI.UserControl
    {
        #region Field
        public PhysicalExamMetod PhysicalExamMetod {
            get
            {
                var pem = new PhysicalExamMetod
                {
                    Auskultasi = Auskultasi,
                    Inspeksi = Inspeksi,
                    Palpasi = Palpasi,
                    Perkusi = Perkusi
                };
                return pem;
            }
            set
            {
                if (value==null) value = new PhysicalExamMetod();
                Auskultasi = value.Auskultasi;
                Inspeksi = value.Inspeksi;
                Palpasi = value.Palpasi;
                Perkusi = value.Perkusi;
            } }
        public string Inspeksi
        {
            get { return txtInspeksi.Text; }
            set
            {
                txtInspeksi.Text = value;
            }
        }
        public string Palpasi
        {
            get { return txtPalpasi.Text; }
            set
            {
                txtPalpasi.Text = value;
            }
        }
        public string Perkusi
        {
            get { return txtPerkusi.Text; }
            set
            {
                txtPerkusi.Text = value;
            }
        }
        public string Auskultasi
        {
            get { return txtAuskultasi.Text; }
            set
            {
                txtAuskultasi.Text = value;
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }

    }
}