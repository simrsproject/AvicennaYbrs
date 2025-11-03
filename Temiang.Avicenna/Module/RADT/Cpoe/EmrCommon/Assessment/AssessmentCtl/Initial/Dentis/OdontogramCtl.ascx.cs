using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl
{
    public partial class OdontogramCtl : BaseAssessmentCtl
    {
        protected PatientOdontogram Odontogram = new PatientOdontogram();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && DataModeCurrent == AppEnum.DataMode.New)
            {
                // Load status Odontogram current Registration
                OnPopulateEntryControl(null, null);
            }
        }

        private static void InsertDentalStatus(string id, string name)
        {
            var stdi = new AppStandardReferenceItem
            {
                StandardReferenceID = "DentalStatus",
                ItemID = id,
                ItemName = name
            };
            stdi.Save();
        }

        private static void InsertDentalStatus()
        {
            InsertDentalStatus("O", "Caries");
            InsertDentalStatus("T", "Tambalan");
            InsertDentalStatus("X", "Gigi sudah tidak ada");
            InsertDentalStatus("H", "Gigi belum tumbuh");
            InsertDentalStatus("E", "Gigi goyang");
            InsertDentalStatus("MM", "Calculus");
            InsertDentalStatus("V", "Radix");
            InsertDentalStatus("P", "Gigi palsu");
            InsertDentalStatus("BW", "Bridge work");
            InsertDentalStatus("MC", "Maloclusion");
            InsertDentalStatus("LC", "Lack Of Cantac");
            InsertDentalStatus("CS", "Calculus");
            InsertDentalStatus("ED", "Edontolous");
            InsertDentalStatus("MB", "Mobility");
            InsertDentalStatus("OT", "Other");

            //Caries
            //Filling
            //Root / Radix
            //Missing tooth
            //Crown / Casting
            //Bridge work
            //Dentures
            //Maloclusion
            //Lack Of Cantac
            //Calculus
            //Edontolous
            //Mobility
            //Others

        }
        private static DataTable DentalStatus()
        {
            var stdi = new AppStandardReferenceItemQuery();
            stdi.Where(stdi.StandardReferenceID == "DentalStatus", stdi.IsActive == 1);
            stdi.Select(stdi.ItemID, stdi.ItemName);
            var dtb = stdi.LoadDataTable();
            return dtb;
        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (!IsPostBack)
            {
                var dtb = DentalStatus();
                if (dtb.Rows == null || dtb.Rows.Count == 0)
                {
                    InsertDentalStatus();
                    dtb = DentalStatus();
                }

                var item = new RadMenuItem { Text = "Good", Value = "" };
                mnuTeeth.Items.Add(item);
                foreach (DataRow row in dtb.Rows)
                {
                    item = new RadMenuItem { Text = row["ItemName"].ToString(), Value = row["ItemID"].ToString() };
                    mnuTeeth.Items.Add(item);
                }
            }

            var teethIds = new string[] {"t11","t51",
"t12",
"t52",
"t13",
"t53",
"t14",
"t54",
"t15",
"t55",
"t16",
"t17",
"t18",
"t61",
"t21",
"t62",
"t22",
"t63",
"t23",
"t64",
"t24",
"t65",
"t25",
"t26",
"t27",
"t28",
"t48",
"t47",
"t46",
"t45",
"t85",
"t44",
"t84",
"t43",
"t83",
"t42",
"t82",
"t41",
"t81",
"t38",
"t37",
"t36",
"t75",
"t35",
"t74",
"t34",
"t73",
"t33",
"t72",
"t32",
"t71",
"t31"};

            foreach (string id in teethIds)
            {
                mnuTeeth.Targets.Add(new ContextMenuControlTarget { ControlID = id });
            }


        }

        #region override method

        protected override void OnPopulateEntryControl(PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            // Get Education
            Odontogram = new PatientOdontogram();
            if (!Odontogram.LoadByPrimaryKey(PatientID, RegistrationNo))
            {
                return;
            }
            txt11.Text = Odontogram.T11;
            txt51.Text = Odontogram.T51;
            txt12.Text = Odontogram.T12;
            txt52.Text = Odontogram.T52;
            txt13.Text = Odontogram.T13;
            txt53.Text = Odontogram.T53;
            txt14.Text = Odontogram.T14;
            txt54.Text = Odontogram.T54;
            txt15.Text = Odontogram.T15;
            txt55.Text = Odontogram.T55;
            txt16.Text = Odontogram.T16;
            txt17.Text = Odontogram.T17;
            txt18.Text = Odontogram.T18;
            txt61.Text = Odontogram.T61;
            txt21.Text = Odontogram.T21;
            txt62.Text = Odontogram.T62;
            txt22.Text = Odontogram.T22;
            txt63.Text = Odontogram.T63;
            txt23.Text = Odontogram.T23;
            txt64.Text = Odontogram.T64;
            txt24.Text = Odontogram.T24;
            txt65.Text = Odontogram.T65;
            txt25.Text = Odontogram.T25;
            txt26.Text = Odontogram.T26;
            txt27.Text = Odontogram.T27;
            txt28.Text = Odontogram.T28;
            txt48.Text = Odontogram.T48;
            txt47.Text = Odontogram.T47;
            txt46.Text = Odontogram.T46;
            txt45.Text = Odontogram.T45;
            txt85.Text = Odontogram.T85;
            txt44.Text = Odontogram.T44;
            txt84.Text = Odontogram.T84;
            txt43.Text = Odontogram.T43;
            txt83.Text = Odontogram.T83;
            txt42.Text = Odontogram.T42;
            txt82.Text = Odontogram.T82;
            txt41.Text = Odontogram.T41;
            txt81.Text = Odontogram.T81;
            txt38.Text = Odontogram.T38;
            txt37.Text = Odontogram.T37;
            txt36.Text = Odontogram.T36;
            txt75.Text = Odontogram.T75;
            txt35.Text = Odontogram.T35;
            txt74.Text = Odontogram.T74;
            txt34.Text = Odontogram.T34;
            txt73.Text = Odontogram.T73;
            txt33.Text = Odontogram.T33;
            txt72.Text = Odontogram.T72;
            txt32.Text = Odontogram.T32;
            txt71.Text = Odontogram.T71;
            txt31.Text = Odontogram.T31;
            txt1151Notes.Text = Odontogram.T1151Notes;
            txt1252Notes.Text = Odontogram.T1252Notes;
            txt1353Notes.Text = Odontogram.T1353Notes;
            txt1454Notes.Text = Odontogram.T1454Notes;
            txt1555Notes.Text = Odontogram.T1555Notes;
            txt16Notes.Text = Odontogram.T16Notes;
            txt17Notes.Text = Odontogram.T17Notes;
            txt18Notes.Text = Odontogram.T18Notes;
            txt6121Notes.Text = Odontogram.T6121Notes;
            txt6222Notes.Text = Odontogram.T6222Notes;
            txt6323Notes.Text = Odontogram.T6323Notes;
            txt6424Notes.Text = Odontogram.T6424Notes;
            txt6525Notes.Text = Odontogram.T6525Notes;
            txt26Notes.Text = Odontogram.T26Notes;
            txt27Notes.Text = Odontogram.T27Notes;
            txt28Notes.Text = Odontogram.T28Notes;
            txt48Notes.Text = Odontogram.T48Notes;
            txt47Notes.Text = Odontogram.T47Notes;
            txt46Notes.Text = Odontogram.T46Notes;
            txt4585Notes.Text = Odontogram.T4585Notes;
            txt4484Notes.Text = Odontogram.T4484Notes;
            txt4383Notes.Text = Odontogram.T4383Notes;
            txt4282Notes.Text = Odontogram.T4282Notes;
            txt4181Notes.Text = Odontogram.T4181Notes;
            txt38Notes.Text = Odontogram.T38Notes;
            txt37Notes.Text = Odontogram.T37Notes;
            txt36Notes.Text = Odontogram.T36Notes;
            txt7535Notes.Text = Odontogram.T7535Notes;
            txt7434Notes.Text = Odontogram.T7434Notes;
            txt7333Notes.Text = Odontogram.T7333Notes;
            txt7232Notes.Text = Odontogram.T7232Notes;
            txt7131Notes.Text = Odontogram.T7131Notes;
        }

        protected override void OnSetEntityValue(ValidateArgs args, PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            // Simpan
            Odontogram = new PatientOdontogram();
            if (!Odontogram.LoadByPrimaryKey(PatientID, RegistrationNo))
            {
                Odontogram.RegistrationNo = RegistrationNo;
                Odontogram.PatientID = PatientID;
                Odontogram.OdontogramDateTime = AssessmentDateTime;
            }

            Odontogram.T11 = txt11.Text;
            Odontogram.T51 = txt51.Text;
            Odontogram.T12 = txt12.Text;
            Odontogram.T52 = txt52.Text;
            Odontogram.T13 = txt13.Text;
            Odontogram.T53 = txt53.Text;
            Odontogram.T14 = txt14.Text;
            Odontogram.T54 = txt54.Text;
            Odontogram.T15 = txt15.Text;
            Odontogram.T55 = txt55.Text;
            Odontogram.T16 = txt16.Text;
            Odontogram.T17 = txt17.Text;
            Odontogram.T18 = txt18.Text;
            Odontogram.T61 = txt61.Text;
            Odontogram.T21 = txt21.Text;
            Odontogram.T62 = txt62.Text;
            Odontogram.T22 = txt22.Text;
            Odontogram.T63 = txt63.Text;
            Odontogram.T23 = txt23.Text;
            Odontogram.T64 = txt64.Text;
            Odontogram.T24 = txt24.Text;
            Odontogram.T65 = txt65.Text;
            Odontogram.T25 = txt25.Text;
            Odontogram.T26 = txt26.Text;
            Odontogram.T27 = txt27.Text;
            Odontogram.T28 = txt28.Text;
            Odontogram.T48 = txt48.Text;
            Odontogram.T47 = txt47.Text;
            Odontogram.T46 = txt46.Text;
            Odontogram.T45 = txt45.Text;
            Odontogram.T85 = txt85.Text;
            Odontogram.T44 = txt44.Text;
            Odontogram.T84 = txt84.Text;
            Odontogram.T43 = txt43.Text;
            Odontogram.T83 = txt83.Text;
            Odontogram.T42 = txt42.Text;
            Odontogram.T82 = txt82.Text;
            Odontogram.T41 = txt41.Text;
            Odontogram.T81 = txt81.Text;
            Odontogram.T38 = txt38.Text;
            Odontogram.T37 = txt37.Text;
            Odontogram.T36 = txt36.Text;
            Odontogram.T75 = txt75.Text;
            Odontogram.T35 = txt35.Text;
            Odontogram.T74 = txt74.Text;
            Odontogram.T34 = txt34.Text;
            Odontogram.T73 = txt73.Text;
            Odontogram.T33 = txt33.Text;
            Odontogram.T72 = txt72.Text;
            Odontogram.T32 = txt32.Text;
            Odontogram.T71 = txt71.Text;
            Odontogram.T31 = txt31.Text;
            Odontogram.T1151Notes = txt1151Notes.Text;
            Odontogram.T1252Notes = txt1252Notes.Text;
            Odontogram.T1353Notes = txt1353Notes.Text;
            Odontogram.T1454Notes = txt1454Notes.Text;
            Odontogram.T1555Notes = txt1555Notes.Text;
            Odontogram.T16Notes = txt16Notes.Text;
            Odontogram.T17Notes = txt17Notes.Text;
            Odontogram.T18Notes = txt18Notes.Text;
            Odontogram.T6121Notes = txt6121Notes.Text;
            Odontogram.T6222Notes = txt6222Notes.Text;
            Odontogram.T6323Notes = txt6323Notes.Text;
            Odontogram.T6424Notes = txt6424Notes.Text;
            Odontogram.T6525Notes = txt6525Notes.Text;
            Odontogram.T26Notes = txt26Notes.Text;
            Odontogram.T27Notes = txt27Notes.Text;
            Odontogram.T28Notes = txt28Notes.Text;
            Odontogram.T48Notes = txt48Notes.Text;
            Odontogram.T47Notes = txt47Notes.Text;
            Odontogram.T46Notes = txt46Notes.Text;
            Odontogram.T4585Notes = txt4585Notes.Text;
            Odontogram.T4484Notes = txt4484Notes.Text;
            Odontogram.T4383Notes = txt4383Notes.Text;
            Odontogram.T4282Notes = txt4282Notes.Text;
            Odontogram.T4181Notes = txt4181Notes.Text;
            Odontogram.T38Notes = txt38Notes.Text;
            Odontogram.T37Notes = txt37Notes.Text;
            Odontogram.T36Notes = txt36Notes.Text;
            Odontogram.T7535Notes = txt7535Notes.Text;
            Odontogram.T7434Notes = txt7434Notes.Text;
            Odontogram.T7333Notes = txt7333Notes.Text;
            Odontogram.T7232Notes = txt7232Notes.Text;
            Odontogram.T7131Notes = txt7131Notes.Text;
            Odontogram.Save();

            UpdateMainStatusPatientOdontogram(Odontogram);
        }

        #endregion

        private void UpdateMainStatusPatientOdontogram(PatientOdontogram odonUpdated)
        {
            // Main Status odon adalah yg Registrasinya diisi kosong
            // Simpan
            var odon = new PatientOdontogram();
            if (!odon.LoadByPrimaryKey(Odontogram.PatientID, string.Empty))
            {
                odon.RegistrationNo = string.Empty;
                odon.PatientID = PatientID;
                odon.OdontogramDateTime = AssessmentDateTime;
            }
            if (!string.IsNullOrEmpty(odonUpdated.T11))
                odon.T11 = odonUpdated.T11;

            if (!string.IsNullOrEmpty(odonUpdated.T51))
                odon.T51 = odonUpdated.T51;

            if (!string.IsNullOrEmpty(odonUpdated.T12))
                odon.T12 = odonUpdated.T12;

            if (!string.IsNullOrEmpty(odonUpdated.T52))
                odon.T52 = odonUpdated.T52;

            if (!string.IsNullOrEmpty(odonUpdated.T13))
                odon.T13 = odonUpdated.T13;

            if (!string.IsNullOrEmpty(odonUpdated.T53))
                odon.T53 = odonUpdated.T53;

            if (!string.IsNullOrEmpty(odonUpdated.T14))
                odon.T14 = odonUpdated.T14;

            if (!string.IsNullOrEmpty(odonUpdated.T54))
                odon.T54 = odonUpdated.T54;

            if (!string.IsNullOrEmpty(odonUpdated.T15))
                odon.T15 = odonUpdated.T15;

            if (!string.IsNullOrEmpty(odonUpdated.T55))
                odon.T55 = odonUpdated.T55;

            if (!string.IsNullOrEmpty(odonUpdated.T16))
                odon.T16 = odonUpdated.T16;

            if (!string.IsNullOrEmpty(odonUpdated.T17))
                odon.T17 = odonUpdated.T17;

            if (!string.IsNullOrEmpty(odonUpdated.T18))
                odon.T18 = odonUpdated.T18;

            if (!string.IsNullOrEmpty(odonUpdated.T61))
                odon.T61 = odonUpdated.T61;

            if (!string.IsNullOrEmpty(odonUpdated.T21))
                odon.T21 = odonUpdated.T21;

            if (!string.IsNullOrEmpty(odonUpdated.T62))
                odon.T62 = odonUpdated.T62;

            if (!string.IsNullOrEmpty(odonUpdated.T22))
                odon.T22 = odonUpdated.T22;

            if (!string.IsNullOrEmpty(odonUpdated.T63))
                odon.T63 = odonUpdated.T63;

            if (!string.IsNullOrEmpty(odonUpdated.T23))
                odon.T23 = odonUpdated.T23;

            if (!string.IsNullOrEmpty(odonUpdated.T64))
                odon.T64 = odonUpdated.T64;

            if (!string.IsNullOrEmpty(odonUpdated.T24))
                odon.T24 = odonUpdated.T24;

            if (!string.IsNullOrEmpty(odonUpdated.T65))
                odon.T65 = odonUpdated.T65;

            if (!string.IsNullOrEmpty(odonUpdated.T25))
                odon.T25 = odonUpdated.T25;

            if (!string.IsNullOrEmpty(odonUpdated.T26))
                odon.T26 = odonUpdated.T26;

            if (!string.IsNullOrEmpty(odonUpdated.T27))
                odon.T27 = odonUpdated.T27;

            if (!string.IsNullOrEmpty(odonUpdated.T28))
                odon.T28 = odonUpdated.T28;

            if (!string.IsNullOrEmpty(odonUpdated.T48))
                odon.T48 = odonUpdated.T48;

            if (!string.IsNullOrEmpty(odonUpdated.T47))
                odon.T47 = odonUpdated.T47;

            if (!string.IsNullOrEmpty(odonUpdated.T46))
                odon.T46 = odonUpdated.T46;

            if (!string.IsNullOrEmpty(odonUpdated.T45))
                odon.T45 = odonUpdated.T45;

            if (!string.IsNullOrEmpty(odonUpdated.T85))
                odon.T85 = odonUpdated.T85;

            if (!string.IsNullOrEmpty(odonUpdated.T44))
                odon.T44 = odonUpdated.T44;

            if (!string.IsNullOrEmpty(odonUpdated.T84))
                odon.T84 = odonUpdated.T84;

            if (!string.IsNullOrEmpty(odonUpdated.T43))
                odon.T43 = odonUpdated.T43;

            if (!string.IsNullOrEmpty(odonUpdated.T83))
                odon.T83 = odonUpdated.T83;

            if (!string.IsNullOrEmpty(odonUpdated.T42))
                odon.T42 = odonUpdated.T42;

            if (!string.IsNullOrEmpty(odonUpdated.T82))
                odon.T82 = odonUpdated.T82;

            if (!string.IsNullOrEmpty(odonUpdated.T41))
                odon.T41 = odonUpdated.T41;

            if (!string.IsNullOrEmpty(odonUpdated.T81))
                odon.T81 = odonUpdated.T81;

            if (!string.IsNullOrEmpty(odonUpdated.T38))
                odon.T38 = odonUpdated.T38;

            if (!string.IsNullOrEmpty(odonUpdated.T37))
                odon.T37 = odonUpdated.T37;

            if (!string.IsNullOrEmpty(odonUpdated.T36))
                odon.T36 = odonUpdated.T36;

            if (!string.IsNullOrEmpty(odonUpdated.T75))
                odon.T75 = odonUpdated.T75;

            if (!string.IsNullOrEmpty(odonUpdated.T35))
                odon.T35 = odonUpdated.T35;

            if (!string.IsNullOrEmpty(odonUpdated.T74))
                odon.T74 = odonUpdated.T74;

            if (!string.IsNullOrEmpty(odonUpdated.T34))
                odon.T34 = odonUpdated.T34;

            if (!string.IsNullOrEmpty(odonUpdated.T73))
                odon.T73 = odonUpdated.T73;

            if (!string.IsNullOrEmpty(odonUpdated.T33))
                odon.T33 = odonUpdated.T33;

            if (!string.IsNullOrEmpty(odonUpdated.T72))
                odon.T72 = odonUpdated.T72;

            if (!string.IsNullOrEmpty(odonUpdated.T32))
                odon.T32 = odonUpdated.T32;

            if (!string.IsNullOrEmpty(odonUpdated.T71))
                odon.T71 = odonUpdated.T71;

            if (!string.IsNullOrEmpty(odonUpdated.T31))
                odon.T31 = odonUpdated.T31;

            if (!string.IsNullOrEmpty(odonUpdated.T1151Notes))
                odon.T1151Notes = odonUpdated.T1151Notes;

            if (!string.IsNullOrEmpty(odonUpdated.T1252Notes))
                odon.T1252Notes = odonUpdated.T1252Notes;

            if (!string.IsNullOrEmpty(odonUpdated.T1353Notes))
                odon.T1353Notes = odonUpdated.T1353Notes;

            if (!string.IsNullOrEmpty(odonUpdated.T1454Notes))
                odon.T1454Notes = odonUpdated.T1454Notes;

            if (!string.IsNullOrEmpty(odonUpdated.T1555Notes))
                odon.T1555Notes = odonUpdated.T1555Notes;

            if (!string.IsNullOrEmpty(odonUpdated.T16Notes))
                odon.T16Notes = odonUpdated.T16Notes;

            if (!string.IsNullOrEmpty(odonUpdated.T17Notes))
                odon.T17Notes = odonUpdated.T17Notes;

            if (!string.IsNullOrEmpty(odonUpdated.T18Notes))
                odon.T18Notes = odonUpdated.T18Notes;

            if (!string.IsNullOrEmpty(odonUpdated.T6121Notes))
                odon.T6121Notes = odonUpdated.T6121Notes;

            if (!string.IsNullOrEmpty(odonUpdated.T6222Notes))
                odon.T6222Notes = odonUpdated.T6222Notes;

            if (!string.IsNullOrEmpty(odonUpdated.T6323Notes))
                odon.T6323Notes = odonUpdated.T6323Notes;

            if (!string.IsNullOrEmpty(odonUpdated.T6424Notes))
                odon.T6424Notes = odonUpdated.T6424Notes;

            if (!string.IsNullOrEmpty(odonUpdated.T6525Notes))
                odon.T6525Notes = odonUpdated.T6525Notes;

            if (!string.IsNullOrEmpty(odonUpdated.T26Notes))
                odon.T26Notes = odonUpdated.T26Notes;

            if (!string.IsNullOrEmpty(odonUpdated.T27Notes))
                odon.T27Notes = odonUpdated.T27Notes;

            if (!string.IsNullOrEmpty(odonUpdated.T28Notes))
                odon.T28Notes = odonUpdated.T28Notes;

            if (!string.IsNullOrEmpty(odonUpdated.T48Notes))
                odon.T48Notes = odonUpdated.T48Notes;

            if (!string.IsNullOrEmpty(odonUpdated.T47Notes))
                odon.T47Notes = odonUpdated.T47Notes;

            if (!string.IsNullOrEmpty(odonUpdated.T46Notes))
                odon.T46Notes = odonUpdated.T46Notes;

            if (!string.IsNullOrEmpty(odonUpdated.T4585Notes))
                odon.T4585Notes = odonUpdated.T4585Notes;

            if (!string.IsNullOrEmpty(odonUpdated.T4484Notes))
                odon.T4484Notes = odonUpdated.T4484Notes;

            if (!string.IsNullOrEmpty(odonUpdated.T4383Notes))
                odon.T4383Notes = odonUpdated.T4383Notes;

            if (!string.IsNullOrEmpty(odonUpdated.T4282Notes))
                odon.T4282Notes = odonUpdated.T4282Notes;

            if (!string.IsNullOrEmpty(odonUpdated.T4181Notes))
                odon.T4181Notes = odonUpdated.T4181Notes;

            if (!string.IsNullOrEmpty(odonUpdated.T38Notes))
                odon.T38Notes = odonUpdated.T38Notes;

            if (!string.IsNullOrEmpty(odonUpdated.T37Notes))
                odon.T37Notes = odonUpdated.T37Notes;

            if (!string.IsNullOrEmpty(odonUpdated.T36Notes))
                odon.T36Notes = odonUpdated.T36Notes;

            if (!string.IsNullOrEmpty(odonUpdated.T7535Notes))
                odon.T7535Notes = odonUpdated.T7535Notes;

            if (!string.IsNullOrEmpty(odonUpdated.T7434Notes))
                odon.T7434Notes = odonUpdated.T7434Notes;

            if (!string.IsNullOrEmpty(odonUpdated.T7333Notes))
                odon.T7333Notes = odonUpdated.T7333Notes;

            if (!string.IsNullOrEmpty(odonUpdated.T7232Notes))
                odon.T7232Notes = odonUpdated.T7232Notes;

            if (!string.IsNullOrEmpty(odonUpdated.T7131Notes))
                odon.T7131Notes = odonUpdated.T7131Notes;

            odon.Save();
        }

    }
}