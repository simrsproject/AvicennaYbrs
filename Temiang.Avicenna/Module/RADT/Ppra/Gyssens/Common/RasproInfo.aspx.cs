using System;
using System.Text;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Text.RegularExpressions;
using System.Data;

namespace Temiang.Avicenna.Module.RADT.Ppra.Common
{
    /// <summary>
    /// Layar untuk keperluan perawat melihat status resep yg sudah complete tetapi belum diambil
    /// Dipanggil dari layar EMR List
    /// </summary>
    public partial class RasproInfo : BasePageDialog
    {

        private int SeqNo => Request.QueryString["seqno"].ToInt();

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
                var gys = new RegistrationGyssens();
                if (gys.LoadByPrimaryKey(RegistrationNo, SeqNo))
                {
                    var rr = new RegistrationRaspro();
                    if (rr.LoadByPrimaryKey(RegistrationNo, gys.RasproSeqNo ?? 0))
                    {
                        var userasproSeqNo = 0;
                        litAbSuggestion.Text = AbRestriction.AntibioticSuggestion(rr, ref userasproSeqNo);

                        txtDiagnose.Text = rr.Diagnose;

                        var stdi = StandardReference.LoadStandardReferenceItem(AppEnum.StandardReference.RASPRO, rr.SRRaspro);
                        txtRasproType.Text = stdi.ItemName;

                        var abr = new AbRestriction();
                        abr.LoadByPrimaryKey(rr.AbRestrictionID);
                        txtAbRestriction.Text = abr.AbRestrictionName;
                    }

                    grdRaspro.DataSource = LoadRaspro(gys.RasproSeqNo ?? 0, rr.SRRaspro);
                }
            }

        }

        private DataTable LoadRaspro(int rasproSeqNo, string rasproType)
        {
            // Load datasource
            var qr = new RasproQuery("rpo");
            var rl = new RegistrationRasproLineQuery("rl");
            qr.LeftJoin(rl).On(qr.RasproLineID == rl.RasproLineID & rl.RegistrationNo == RegistrationNo & rl.SeqNo == rasproSeqNo);
            qr.Where(qr.SRRaspro == rasproType);
            qr.OrderBy(qr.SeqNo.Ascending);
            qr.Select(qr, rl.Condition,
                "<CONVERT(BIT,CASE WHEN COALESCE(rl.RasproLineID,'')='' THEN 0 ELSE 1 END) as IsEntryVisible>",
                "<CONVERT(BIT,0) as IsStop>",
                "<'' as ParameterInfo>");
            var dtb = qr.LoadDataTable();


            foreach (DataRow row in dtb.Rows)
            {
                row["IsEntryVisible"] = true;
                break;
            }

            return dtb;

        }


    }
}