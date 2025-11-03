using System;
using System.Text;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Text.RegularExpressions;

namespace Temiang.Avicenna.Module.RADT
{
    /// <summary>
    /// Layar untuk keperluan perawat melihat status resep yg sudah complete tetapi belum diambil
    /// Dipanggil dari layar EMR List
    /// </summary>
    public partial class SatuSehatResultInfo : BasePageDialog
    {
        private string ResourceType => Request.QueryString["rtype"];
        private string ResultId => Request.QueryString["rid"];

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ButtonOk.Visible = false;
            ButtonCancel.Text = "Close";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var util = new Bridging.SatuSehat.Utils();
                var accessToken = string.Empty;
                switch (ResourceType)
                {
                    case "Encounter":
                        {
                            Title = "Kunjungan Post Response";
                            var resp = util.RestClientGet("Encounter", ResultId,ref accessToken);
                            txtContent.Text = resp.Content;
                            break;
                        }
                    case "Condition":
                        {
                            Title = "Diagnosis Post Response";
                            var resp = util.RestClientGet("Condition", ResultId,ref accessToken);
                            txtContent.Text = resp.Content;
                            break;
                        }
                    default:
                        {
                            Title = String.Format("{0} Post Response", ResourceType);
                            var resp = util.RestClientGet(ResourceType, ResultId,ref accessToken);
                            txtContent.Text = resp.Content;
                            break;
                        }
                }

            }
        }

    }
}