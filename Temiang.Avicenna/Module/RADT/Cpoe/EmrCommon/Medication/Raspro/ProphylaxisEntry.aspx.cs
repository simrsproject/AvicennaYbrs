using System;
using System.Drawing.Imaging;
using System.Web.UI;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;


namespace Temiang.Avicenna.Module.RADT.Emr
{
    public partial class ProphylaxisEntry : BasePageDialog
    {
        public string PatientID
        {
            get
            {
                return Request.QueryString["patid"];
            }
        }
        public string LocationID
        {
            get
            {
                return Request.QueryString["locid"];
            }
        }

        public int RasproSeqNo
        {
            get
            {
                return Request.QueryString["seqno"].ToInt();
            }
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ElectronicHealthRecord;

            if (!IsPostBack)
            {
                var stdi = new AppStandardReferenceItem();
                stdi.LoadByPrimaryKey(AppEnum.StandardReference.RASPRO.ToString(), AppConstant.RasproType.Prophylaxis);
                lblRasproName.Text = stdi.ItemName;
                lblRasproNote.Text = stdi.Note;


                txtRasproDateTime.SelectedDate = DateTime.Now;

                // TODO: Betulkan ServiceUnit saat isi form Raspro 
                var reg = new Registration();
                reg.LoadByPrimaryKey(RegistrationNo);
                var su = new ServiceUnit();
                su.LoadByPrimaryKey(reg.ServiceUnitID);
                txtServiceUnitName.Text = su.ServiceUnitName;

                txtParamedicName.Text = Paramedic.GetParamedicName(ParamedicTeam.DPJP(RegistrationNo).ParamedicID);

                txtRegistrationNo.Text = RegistrationNo;
                var pat = new Patient();
                if (pat.LoadByPrimaryKey(PatientID))
                {
                    txtPatientName.Text = pat.PatientName;
                    txtMedicalNo.Text = pat.MedicalNo;
                    txtDateOfBirth.SelectedDate = pat.DateOfBirth;
                }

                // Surgery Selection
                var inf = new AbRestrictionQuery();
                inf.Select(inf.AbRestrictionID, inf.ParentID, inf.AbRestrictionName);
                inf.Where(inf.SRAbRestrictionType == "SGR");
                inf.OrderBy(inf.AbRestrictionName.Ascending);
                var dtbInf = inf.LoadDataTable();
                cboAbRestrictionID.DataSource = dtbInf;
                cboAbRestrictionID.DataBind();

                StandardReference.Initialize(cboSRWoundClassification, AppEnum.StandardReference.WoundClassification);

                if (RasproSeqNo > 0) // Edit
                {
                    var rr = new RegistrationRaspro();
                    if (rr.LoadByPrimaryKey(RegistrationNo, RasproSeqNo))
                    {

                        txtRasproDateTime.SelectedDate = rr.RasproDateTime;
                        txtSurgeryName.Text = rr.SurgeryName;
                        if (!string.IsNullOrWhiteSpace(rr.AbRestrictionID))
                            cboAbRestrictionID.SelectedValue = rr.AbRestrictionID;

                        ComboBox.SelectedValue(cboSRWoundClassification, rr.SRWoundClassification);
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Footer.Visible = false;
        }

        private void SaveAndClose()
        {
            var rr = new RegistrationRaspro();
            if (RasproSeqNo > 0)
                rr.LoadByPrimaryKey(RegistrationNo, RasproSeqNo);
            else
            {
                // NewSeqNo
                var newSeqNo = 1;
                var lastrr = new RegistrationRaspro();
                lastrr.Query.Where(lastrr.Query.RegistrationNo == RegistrationNo);
                lastrr.Query.OrderBy(lastrr.Query.SeqNo.Descending);
                lastrr.Query.es.Top = 1;
                if (lastrr.Query.Load())
                    newSeqNo = (lastrr.SeqNo ?? 0) + 1;

                rr = new RegistrationRaspro
                {
                    SRRaspro = AppConstant.RasproType.Prophylaxis,
                    RegistrationNo = RegistrationNo,
                    SeqNo = newSeqNo
                };

                // TODO: Betulkan ServiceUnit saat isi form Raspro 
                var reg = new Registration();
                reg.LoadByPrimaryKey(RegistrationNo);
                rr.ServiceUnitID = reg.ServiceUnitID;
            }

            rr.ParamedicID = ParamedicTeam.DPJP(RegistrationNo).ParamedicID;
            rr.RasproDateTime = txtRasproDateTime.SelectedDate;

            // Save Sign
            if (!string.IsNullOrWhiteSpace(hdnImage.Value))
            {
                var imgHelper = new ImageHelper();
                var resized = imgHelper.ResizeImage(imgHelper.ToImage(hdnImage.Value), new System.Drawing.Size(332, 185));
                rr.SignImage = imgHelper.ToByteArray(resized, ImageFormat.Png);
            }

            rr.SurgeryName = txtSurgeryName.Text;
            rr.AbRestrictionID = cboAbRestrictionID.SelectedValue;
            rr.SRWoundClassification = cboSRWoundClassification.SelectedValue;

            //KATEGORI OPERASI : 01 BERSIH, 02 BERSIH + PROTESA, 03 BERSIH TERCEMAR,05 TERCEMAR, 06 KOTOR
            //D.Kategori Operasi Bersih tidak membutuhkan antibiotik profilaksis
            //E.Kategori Operasi Bersih + Protesa dan Kategori Operasi Bersih Tercemar membutuhkan antibiotik profilaksis
            //F.Kategori Operasi Tercemat dan Kotor membutuhkan antibiotik empirik(harus mengisi formulir RASAL)
            if (rr.SRWoundClassification == "02" || rr.SRWoundClassification == "03")
                rr.AntibioticLevel = 1; // Pakai Profilaskis AB restriction 
            else if (rr.SRWoundClassification == "01")
                rr.AntibioticLevel = AppConstant.AntibioticLevel.NoNeedAntibiotic;
            else
                rr.AntibioticLevel = AppConstant.AntibioticLevel.UseAbNonProphylaxis;

            rr.Save();

            // Utk refresh di PrescriptionEntry
            Session["RasproSeqNo"] = rr.SeqNo;
            Session["SRRaspro"] = rr.SRRaspro;

            var script = "<script type='text/javascript'>CloseAndApply();</script>";
            //Create Startup Javascript for close window              
            Page.ClientScript.RegisterStartupScript(this.GetType(), "closeMe", script);
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (eventArgument == "save")
            {
                SaveAndClose();
            }
        }
    }
}
