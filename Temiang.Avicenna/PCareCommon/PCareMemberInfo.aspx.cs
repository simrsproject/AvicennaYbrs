using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.PCareCommon
{
    public partial class PCareMemberInfo : BasePageDialog
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "BPJS Member Info";
            ButtonOk.Text = "Apply";
            ButtonCancel.Text = "Cancel";
            if (!IsPostBack)
            {
                BpjsMember.Populate(Page.Request.QueryString["bpjsno"], Page.Request.QueryString["regno"], Page.Request.QueryString["patientid"], Page.Request.QueryString["nik"]);
            }
        }
        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            var strb = new StringBuilder();
            strb.AppendFormat("oArg.Nama = '{0}';",BpjsMember.Nama);
            strb.AppendFormat("oArg.NoKartu = '{0}';",BpjsMember.NoKartu);
            strb.AppendFormat("oArg.NoKTP = '{0}';",BpjsMember.NoKTP);
            strb.AppendFormat("oArg.TglLahir = '{0}';",BpjsMember.TglLahir);
            strb.AppendFormat("oArg.TglMulaiAktif = '{0}';",BpjsMember.TglMulaiAktif);
            strb.AppendFormat("oArg.TglAkhirBerlaku = '{0}';",BpjsMember.TglAkhirBerlaku);
            strb.AppendFormat("oArg.Aktif = '{0}';",BpjsMember.Aktif);
            strb.AppendFormat("oArg.KetAktif = '{0}';",BpjsMember.KetAktif);
            strb.AppendFormat("oArg.NoHP = '{0}';",BpjsMember.NoHP);
            strb.AppendFormat("oArg.Sex = '{0}';",BpjsMember.Sex);
            return strb.ToString();
        }
        protected override void OnButtonOkClicked(ValidateArgs args)
        {
            //// Save GuarantorNo
            //if (!string.IsNullOrEmpty(PatientID))
            //{
            //    var patient = new Patient();
            //    if (patient.LoadByPrimaryKey(PatientID))
            //    {
            //        patient.GuarantorCardNo = BpjsMember.NoKartu;
            //        patient.Save();
            //    }
            //}

            //if (!string.IsNullOrEmpty(RegistrationNo))
            //{
            //    var reg = new Registration();
            //    if (reg.LoadByPrimaryKey(RegistrationNo))
            //    {
            //        if (string.IsNullOrEmpty(PatientID))
            //        {
            //            var patient = new Patient();
            //            patient.LoadByPrimaryKey(reg.PatientID);
            //            patient.GuarantorCardNo = BpjsMember.NoKartu;
            //            patient.Save();
            //        }
            //    }
            //}
        }
    }
}