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
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl
{
    public partial class GcsCtl : BaseAssessmentCtl
    {
        #region Field
        //public PainScale PainScale
        //{
        //    get
        //    {
        //        var ps = new PainScale();
        //        ps.PainScaleType = ddlSRPainScaleType.SelectedValue;
        //        ps.PainScaleValue = txtPainScale.Value.ToInt();
        //        return ps;
        //    }
        //    set
        //    {
        //        if (value == null) value = new PainScale();

        //        txtPainScale.Value = value.PainScaleValue;
        //        ddlSRPainScaleType.SelectedValue = value.PainScaleType;
        //    }
        //}
        public Gcs Gcs
        {
            get
            {
                var gcs = new Gcs();
                gcs.Eye.SetValue(Eye);
                gcs.Motor.SetValue(Motor);
                gcs.Verbal.SetValue(Verbal);
                gcs.ConsciousnessNote = txtConsciousnessNote.Text;
                gcs.PainScale = optPainScale.SelectedValue;
                if (AppSession.Parameter.IsUsingSplitPainScaleAndFlaccBasedOnAge) //RSSTJ : tambah flacc untuk diterapkan show & hide sesuai umur (Fajri - 2023/10/26)
                {
                    gcs.Flacc.Face = Face;
                    gcs.Flacc.Legs = Legs;
                    gcs.Flacc.Activity = Activity;
                    gcs.Flacc.Cry = Cry;
                    gcs.Flacc.Consolability = Consolability;
                }
                return gcs;
            }
            set
            {
                if (value == null) value = new Gcs();

                // Set Selectedvalue
                Eye = value.Eye.Code;
                Motor = value.Motor.Code;
                Verbal = value.Verbal.Code;
                ConsciousnessNote = value.ConsciousnessNote;
                PainScale = value.PainScale;
                // Populate Consciousness
                Consciousness = string.Format("{0} [{1}]", value.ConsciousnessDescription, value.ConsciousnessValue);
                if (AppSession.Parameter.IsUsingSplitPainScaleAndFlaccBasedOnAge) //RSSTJ : tambah flacc untuk diterapkan show & hide sesuai umur (Fajri - 2023/10/26)
                {
                    Face = value.Flacc.Face;
                    Legs = value.Flacc.Legs;
                    Activity = value.Flacc.Activity;
                    Cry = value.Flacc.Cry;
                    Consolability = value.Flacc.Consolability;
                }
            }
        }
        public string Condition
        {
            get { return optCondition.SelectedValue; }
            set
            {
                optCondition.SelectedValue = value;
            }
        }
        public string Consciousness
        {
            get { return txtConsciousness.Text; }
            set { txtConsciousness.Text = value; }
        }

        public string ConsciousnessNote
        {
            get { return txtConsciousnessNote.Text; }
            set { txtConsciousnessNote.Text = value; }
        }

        public string PainScale
        {
            get { return optPainScale.SelectedValue; }
            set { optPainScale.SelectedValue = value; }
        }

        public string Eye
        {
            get { return ddlGcsEye.SelectedValue; }
            set
            {
                SetSelectedGcsList(ddlGcsEye, value);
            }
        }
        public string Verbal
        {
            get { return ddlGcsVerbal.SelectedValue; }
            set
            {
                SetSelectedGcsList(ddlGcsVerbal, value);
            }
        }
        public string Motor
        {
            get { return ddlGcsMotor.SelectedValue; }
            set
            {
                SetSelectedGcsList(ddlGcsMotor, value);
            }
        }

        //RSSTJ : tambah flacc untuk diterapkan show & hide sesuai umur (Fajri - 2023/10/26)
        public string Face
        {
            get { return ddlFlaccFace.SelectedValue; }
            set { ddlFlaccFace.SelectedValue = value; }
        }
        public string Legs
        {
            get { return ddlFlaccLegs.SelectedValue; }
            set { ddlFlaccLegs.SelectedValue = value; }
        }
        public string Activity
        {
            get { return ddlFlaccActivity.SelectedValue; }
            set { ddlFlaccActivity.SelectedValue = value; }
        }
        public string Cry
        {
            get { return ddlFlaccCry.SelectedValue; }
            set { ddlFlaccCry.SelectedValue = value; }
        }
        public string Consolability
        {
            get { return ddlFlaccConsolability.SelectedValue; }
            set { ddlFlaccConsolability.SelectedValue = value; }
        }
        #endregion

        protected override void OnInit(EventArgs e)
        {


            base.OnInit(e);
            if (!IsPostBack)
            {
                PopulateGcsList(ddlGcsEye, "GcsEye");
                PopulateGcsList(ddlGcsVerbal, "GcsVerbal");
                PopulateGcsList(ddlGcsMotor, "GcsMotor");

                if (AppSession.Parameter.IsUsingSplitPainScaleAndFlaccBasedOnAge)  //RSSTJ : tambah flacc untuk diterapkan show & hide sesuai umur (Fajri - 2023/10/26)
                {
                    PopulateFlaccList(ddlFlaccFace, "Flacc", "FCE");
                    PopulateFlaccList(ddlFlaccLegs, "Flacc", "LEG");
                    PopulateFlaccList(ddlFlaccActivity, "Flacc", "ACT");
                    PopulateFlaccList(ddlFlaccCry, "Flacc", "CRY");
                    PopulateFlaccList(ddlFlaccConsolability, "Flacc", "CON");

                    var pat = new Patient();
                    if (pat.LoadByPrimaryKey(PatientID))
                    {
                        var ageInYear = pat.IsAlive == true ? Helper.GetAgeInYear(pat.DateOfBirth.Value) : Helper.GetAgeInYear(pat.DateOfBirth.Value, pat.DeceasedDateTime ?? DateTime.Now);

                        if (ageInYear <= AppSession.Parameter.SplitPainScaleAndFlaccAgeValue)
                        {
                            trPainScale.Visible = false;
                            trFlacc.Visible = true;
                        }
                        else
                        {
                            trPainScale.Visible = true;
                            trFlacc.Visible = false;
                        }
                    }
                }
            }


            if (AppParameter.IsYes(AppParameter.ParameterItem.isUsingDefultGcs))
            {
                ddlGcsEye.SelectedIndex = 1;
                ddlGcsVerbal.SelectedIndex = 1;
                ddlGcsMotor.SelectedIndex = 1;
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void PopulateGcsList(RadDropDownList ddl, string standardReferenceID)
        {
            var dtb = GetStandardReference(standardReferenceID);
            ddl.Items.Clear();
            //ddl.Items.Add(new DropDownListItem(string.Empty, "_99"));
            ddl.Items.Add(new DropDownListItem(string.Empty, "_0"));

            foreach (System.Data.DataRow row in dtb.Rows)
            {
                ddl.Items.Add(new DropDownListItem(string.Format("{0} - {1}", row["Note"], row["ItemName"]), string.Format("{0}_{1}", row["ItemID"], row["Note"])));
            }
        }
        private void PopulateFlaccList(RadDropDownList ddl, string standardReferenceID, string refId)
        {
            var dtb = GetStandardReferenceFlacc(standardReferenceID, refId);
            ddl.Items.Clear();
            ddl.Items.Add(new DropDownListItem(string.Empty, string.Empty));

            foreach (System.Data.DataRow row in dtb.Rows)
            {
                ddl.Items.Add(new DropDownListItem(string.Format("{0}", row["ItemName"]), string.Format("{0}", row["ItemID"])));
            }
        }
        private void SetSelectedGcsList(RadDropDownList ddl, string code)
        {
            if (!string.IsNullOrEmpty(code))
            {
                code = code + "_";
                foreach (DropDownListItem item in ddl.Items)
                {
                    if (item.Value.Contains(code))
                    {
                        ddl.SelectedValue = item.Value;
                        return;
                    }
                }
            }
            ddl.SelectedIndex = -1;
        }
        private DataTable GetStandardReference(string standardReferenceID)
        {
            // Note tempat menyimpan score untuk GCS
            var query = new AppStandardReferenceItemQuery();
            query.Select(query.ItemID, query.ItemName, query.Note);
            query.Where
                (
                    query.StandardReferenceID == standardReferenceID
                );
            query.OrderBy(query.ItemID.Ascending);
            var dtb = query.LoadDataTable();
            return dtb;
        }
        private DataTable GetStandardReferenceFlacc(string standardReferenceID, string refId)
        {
            // Note tempat menyimpan score untuk GCS
            var query = new AppStandardReferenceItemQuery();
            query.Select(query.ItemID, query.ItemName, query.Note);
            query.Where
                (
                    query.StandardReferenceID == standardReferenceID,
                    query.ReferenceID == refId
                );
            query.OrderBy(query.ItemID.Ascending);
            var dtb = query.LoadDataTable();
            return dtb;
        }

        protected void ddlGcs_SelectedIndexChanged(object sender, DropDownListEventArgs e)
        {
            PopulateConsciousness();
        }

        private void PopulateConsciousness()
        {
            var gcs = new Gcs();
            gcs.Eye.SetValue(Eye);
            gcs.Motor.SetValue(Motor);
            gcs.Verbal.SetValue(Verbal);
            txtConsciousnessNote.Text = gcs.ConsciousnessNote;
            txtConsciousness.Text = gcs.ConsciousnessValue > 20 ? gcs.ConsciousnessDescription : string.Format("{0} [{1}]", gcs.ConsciousnessDescription, gcs.ConsciousnessValue);
        }

        protected void optCondition_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (optCondition.SelectedValue == "DOA")
            {
                ddlGcsEye.SelectedIndex = 0;
                ddlGcsMotor.SelectedIndex = 0;
                ddlGcsVerbal.SelectedIndex = 0;
                PopulateConsciousness();
            }
        }
    }
}