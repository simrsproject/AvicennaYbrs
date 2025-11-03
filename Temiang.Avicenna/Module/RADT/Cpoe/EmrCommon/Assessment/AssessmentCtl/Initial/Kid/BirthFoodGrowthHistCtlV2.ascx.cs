using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.JsonField.Assesment;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl
{
    public partial class BirthFoodGrowthHistCtlV2 : BaseAssessmentCtl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void PopulateDisplay()
        {
            var pbr = new PatientBirthRecord();
            if (pbr.LoadByPrimaryKey(PatientID))
            {
                optBirthMethod.SelectedValue = pbr.BirthMethod;
                txtBirthMethodScIndication.Text = pbr.BirthMethodScIndication;

                txtChildNumber.Value = pbr.ChildNumber;
                txtChildNumberFrom.Value = pbr.ChildNumberFrom;

                txtLength.Value = (double)(pbr.Length ?? 0);
                txtWeight.Value = (double)(pbr.Weight ?? 0);
                txtAsiToMonthAge.Value = pbr.AsiToMonthAge;
                txtCurrentDiet.Text = pbr.CurrentDiet;
                txtSitAtMonthAge.Value = pbr.SitAtMonthAge;
                txtCrawlAtMonthAge.Value = pbr.CrawlAtMonthAge;
                txtStandUpAtMonthAge.Value = pbr.StandUpAtMonthAge;
                txtWalkAtMonthAge.Value = pbr.WalkAtMonthAge;
                txtSpeak3WordAtMonthAge.Value = pbr.Speak3WordAtMonthAge;
                txtSpeak2SentAtMonthAge.Value = pbr.Speak2SentAtMonthAge;
                txtSchoolClass.Text = pbr.SchoolClass;
                txtSchoolAchievement.Text = pbr.SchoolAchievement;
                txtGrowthNotes.Text = pbr.GrowthNotes;

                txtHeadCircum.Value = (double)(pbr.HeadCircumference ?? 0);
                txtFormulaMilkStartAge.Value = (double)(pbr.FormulaMilkStartAge ?? 0);
                txtAddFoodStartAge.Value = (double)(pbr.AddFoodStartAge ?? 0);
                txtRaiseHead.Value = (double)(pbr.RaiseHead ?? 0);
                txtGrabbing.Value = (double)(pbr.Grabbing ?? 0);
                txtHolding.Value = (double)(pbr.Holding ?? 0);

                txtSmile.Value = (double)(pbr.Smile ?? 0);
                txtCooing.Value = (double)(pbr.Cooing ?? 0);
                txtRollToTummy.Value = (double)(pbr.RollToTummy ?? 0);
                txtRollFromTummy.Value = (double)(pbr.RollFromTummy ?? 0);
                txtBabbling.Value = (double)(pbr.Babbling ?? 0);

            }

            // Ovveride dgn data dari BirthRecord
            var qr = new BirthRecordQuery();
            qr.Where(qr.RegistrationNo == RegistrationNo);
            qr.es.Top = 1;
            qr.OrderBy(qr.RegistrationNo.Ascending);
            var br = new BirthRecord();
            if (br.Load(qr))
            {
                optBirthMethod.SelectedValue = br.SRBirthMethod == "07" ? "SC" : "SN";
                txtLength.Value = (double)(br.Length ?? 0);
                txtWeight.Value = (double)(br.Weight ?? 0);
                txtHeadCircum.Value = (double)(br.HeadCircumference ?? 0);
                txtChildNumber.Value = (double)(br.ChildNo ?? 0);
            }
        }
        #region override method

        public override void OnMenuNewClick()
        {
            PopulateDisplay();
        }

        protected override void OnPopulateEntryControl(PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            PopulateDisplay();


        }

        protected override void OnSetEntityValue(ValidateArgs args, PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            SavePatientBirthRecord();
        }

        private void SavePatientBirthRecord()
        {
            var pbr = new PatientBirthRecord();
            if (!pbr.LoadByPrimaryKey(PatientID))
            {
                pbr.PatientID = PatientID;
            }
            pbr.BirthMethod = optBirthMethod.SelectedValue;
            pbr.BirthMethodScIndication = txtBirthMethodScIndication.Text;
            pbr.ChildNumber = (int)(txtChildNumber.Value ?? 0);
            pbr.ChildNumberFrom = (int)(txtChildNumberFrom.Value ?? 0);
            pbr.Length = (decimal)(txtLength.Value ?? 0);
            pbr.Weight = (decimal)(txtWeight.Value ?? 0);

            pbr.AsiToMonthAge = (int?)txtAsiToMonthAge.Value;
            pbr.CurrentDiet = txtCurrentDiet.Text;
            pbr.SitAtMonthAge = (int?)txtSitAtMonthAge.Value;
            pbr.CrawlAtMonthAge = (int?)txtCrawlAtMonthAge.Value;
            pbr.StandUpAtMonthAge = (int?)txtStandUpAtMonthAge.Value;
            pbr.WalkAtMonthAge = (int?)txtWalkAtMonthAge.Value;
            pbr.Speak3WordAtMonthAge = (int?)txtSpeak3WordAtMonthAge.Value;
            pbr.Speak2SentAtMonthAge = (int?)txtSpeak2SentAtMonthAge.Value;
            pbr.SchoolClass = txtSchoolClass.Text;
            pbr.SchoolAchievement = txtSchoolAchievement.Text;
            pbr.GrowthNotes = txtGrowthNotes.Text;
            pbr.HeadCircumference = (int?)txtHeadCircum.Value;
            pbr.FormulaMilkStartAge = (int?)txtFormulaMilkStartAge.Value;
            pbr.AddFoodStartAge = (int?)txtAddFoodStartAge.Value;
            pbr.RaiseHead = (int?)txtRaiseHead.Value;
            pbr.Grabbing = (int?)txtGrabbing.Value;
            pbr.Holding = (int?)txtHolding.Value;

            pbr.Smile = (int?)txtSmile.Value;
            pbr.Cooing = (int?)txtCooing.Value;
            pbr.RollToTummy = (int?)txtRollToTummy.Value;
            pbr.RollFromTummy = (int?)txtRollFromTummy.Value;
            pbr.Babbling = (int?)txtBabbling.Value;


            pbr.Save();

            // Save juga ke BirthRecord
            var qr = new BirthRecordQuery();
            qr.Where(qr.RegistrationNo == RegistrationNo);
            qr.es.Top = 1;
            qr.OrderBy(qr.RegistrationNo.Ascending);
            var br = new BirthRecord();
            if (br.Load(qr))
            {
                if (txtLength.Value != null) br.Length = (decimal)txtLength.Value;
                if (txtWeight.Value != null) br.Weight = (decimal)txtWeight.Value;
                br.Save();
            }
        }

        protected override void OnDataModeChanged(bool isEdited)
        {
        }
        #endregion
    }
}