using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.JsonField.Assesment;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl
{
    /// <summary>
    /// Entry control untuk asesmen IGD
    /// Dengan isian FLACC, ESI, Pysical Examination Detail 
    /// </summary>
    /// Create By: Handono
    /// Create Date: 2023-March-07
    /// Client Req: RSYS
    /// Assessment default pakai ProgramID : SLP.01.AIGD
    /// Assessment Esi pakai ProgramID : SLP.01.AIGDESI
    /// ----------------------------------------------------
    public partial class IgdEsiOptPeCtl : BaseAssessmentCtl
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            StandardReference.InitializeIncludeSpace(ddlEsi, "Triage5Level", true);
            StandardReference.InitializeIncludeSpace(ddlFlaccFace, AppEnum.StandardReference.Flacc, true, "FCE");
            StandardReference.InitializeIncludeSpace(ddlFlaccLegs, AppEnum.StandardReference.Flacc, true, "LEG");
            StandardReference.InitializeIncludeSpace(ddlFlaccActivity, AppEnum.StandardReference.Flacc, true, "ACT");
            StandardReference.InitializeIncludeSpace(ddlFlaccCry, AppEnum.StandardReference.Flacc, true, "CRY");
            StandardReference.InitializeIncludeSpace(ddlFlaccConsolability, AppEnum.StandardReference.Flacc, true, "CON");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //RSSTJ : tambah textbox skrinning gizi dan hide & show pain scale dan flacc berdasarkan umur (Fajri - 2023/10/25)
            trNutriSkrinning.Visible = AppSession.Parameter.HealthcareInitialAppsVersion == "RSSTJ";

            if (AppSession.Parameter.IsUsingSplitPainScaleAndFlaccBasedOnAge)
            {
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

        #region override method
        protected override void OnPopulateEntryControl(PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            txtSubjective.Text = assessment.SubjectiveAddNote;

            // Get Education
            var asses = assessment;
            // Convert to class w json
            try
            {
                if (!string.IsNullOrEmpty(asses.PhysicalExam))
                {
                    var igd = JsonConvert.DeserializeObject<Igd>(asses.PhysicalExam);

                    // Triage
                    optPainScale.SelectedValue = igd.Consciousness.PainScale;

                    var flacc = igd.Flacc;
                    ddlFlaccFace.SelectedValue = flacc.Face;
                    ddlFlaccLegs.SelectedValue = flacc.Legs;
                    ddlFlaccActivity.SelectedValue = flacc.Activity;
                    ddlFlaccCry.SelectedValue = flacc.Cry;
                    ddlFlaccConsolability.SelectedValue = flacc.Consolability;

                    var esi = igd.Esi;
                    ddlEsi.SelectedValue = esi.Id;

                    PopulateEsiReason(esi.Id);
                    var condition = string.Join(";", esi.ConditionIds);
                    foreach (Telerik.Web.UI.ButtonListItem item in cblEsiCondition.Items)
                    {
                        item.Selected = condition.Contains(item.Value);
                    }


                    // Subjective
                    txtSubjective.Text = assessment.SubjectiveAddNote;

                    // Objective
                    if (igd.Head != null && igd.Head.IsAbNormal != null)
                    {
                        optHead.SelectedIndex = (igd.Head.IsAbNormal ?? false) ? 1 : 0;
                        txtHead.Text = igd.Head.Notes;
                    }


                    if (igd.Eye != null && igd.Eye.IsAbNormal != null)
                    {
                        optEye.SelectedIndex = (igd.Eye.IsAbNormal ?? false) ? 1 : 0;

                        if (igd.Eye.IsAbNormal ?? false)
                        {
                            var reason = string.Join("|", igd.Eye.Reasons);
                            chkEyesAnemia.Checked = reason.Contains("ANM");
                            chkEyesIkterik.Checked = reason.Contains("IKT");

                            var reasons = igd.Eye.Reasons;
                            var match = reasons.FirstOrDefault(stringToCheck => stringToCheck.Contains("PPL|"));
                            if (match != null)
                            {
                                chkEyesPupil.Checked = true;
                                txtEyesPupil.Text = match.Split('|')[1];
                            }

                            match = reasons.FirstOrDefault(stringToCheck => stringToCheck.Contains("OTH|"));
                            if (match != null)
                            {
                                chkEyesOther.Checked = true;
                                txtEyesOther.Text = match.Split('|')[1];
                            }
                        }

                    }

                    if (igd.Neck.IsAbNormal != null)
                    {
                        optNeck.SelectedIndex = (igd.Neck.IsAbNormal ?? false) ? 1 : 0;
                        txtNeck.Text = igd.Neck.Notes;
                    }


                    if (igd.Pulmo != null && igd.Pulmo.IsAbNormal != null)
                    {
                        optPul.SelectedIndex = (igd.Pulmo.IsAbNormal ?? false) ? 1 : 0;
                        if (igd.Pulmo.IsAbNormal ?? false)
                        {
                            var reason = string.Join("|", igd.Pulmo.Reasons);
                            chkPulRonki.Checked = reason.Contains("RNK");
                            chkPulWheezing.Checked = reason.Contains("WZG");
                            chkPulRetrac.Checked = reason.Contains("RTC");

                            var match = igd.Pulmo.Reasons.FirstOrDefault(stringToCheck => stringToCheck.Contains("OTH|"));
                            if (match != null)
                            {
                                chkPulOther.Checked = true;
                                txtPulOther.Text = match.Split('|')[1];
                            }
                        }
                    }


                    if (igd.Cor != null && igd.Cor.IsAbNormal != null)
                    {
                        optCor.SelectedIndex = (igd.Cor.IsAbNormal ?? false) ? 1 : 0;
                        if (igd.Cor.IsAbNormal ?? false)
                        {
                            var reasons = string.Join("|", igd.Cor.Reasons);
                            chkCorGallop.Checked = reasons.Contains("GLP");
                            chkCorMurmur.Checked = reasons.Contains("MMR");

                            var match = igd.Cor.Reasons.FirstOrDefault(stringToCheck => stringToCheck.Contains("OTH|"));
                            if (match != null)
                            {
                                chkCorOther.Checked = true;
                                txtCorOther.Text = match.Split('|')[1];
                            }
                        }
                    }

                    if (igd.Abdomen != null && igd.Abdomen.IsAbNormal != null)
                    {
                        optAbd.SelectedIndex = (igd.Abdomen.IsAbNormal ?? false) ? 1 : 0;
                        if (igd.Abdomen.IsAbNormal ?? false)
                        {
                            var reasons = string.Join("|", igd.Abdomen.Reasons);
                            chkAbdBiSusUp.Checked = reasons.Contains("BSU");

                            chkAbdBiSusDown.Checked = reasons.Contains("BSD");

                            chkAbdPressPain.Checked = reasons.Contains("PPN");

                            chkAbdHepa.Checked = reasons.Contains("HEP");
                            chkAbdSple.Checked = reasons.Contains("SPL");
                            chkAbdRelPain.Checked = reasons.Contains("RPN");

                            var match = igd.Abdomen.Reasons.FirstOrDefault(stringToCheck => stringToCheck.Contains("OTH|"));
                            if (match != null)
                            {
                                chkAbdOther.Checked = true;
                                txtAbdOther.Text = match.Split('|')[1];
                            }
                        }
                    }


                    if (igd.Extremity != null && igd.Extremity.IsAbNormal != null)
                    {
                        optExt.SelectedIndex = (igd.Extremity.IsAbNormal ?? false) ? 1 : 0;

                        if (igd.Extremity.IsAbNormal ?? false)
                        {
                            var reason = string.Join("|", igd.Extremity.Reasons);
                            chkExtColdAkral.Checked = reason.Contains("CAK");
                            chkExtPitEdem.Checked = reason.Contains("PED");
                            chkExtWeakPul.Checked = reason.Contains("WPL");
                            chkExtPares.Checked = reason.Contains("PRS");
                            chkExtCrt2up.Checked = reason.Contains("C2U");
                            chkExtMenSs.Checked = reason.Contains("MSS");
                            chkExtParesthe.Checked = reason.Contains("PRT");

                            var reasons = igd.Extremity.Reasons;
                            var match = reasons.FirstOrDefault(stringToCheck => stringToCheck.Contains("ARM|"));
                            if (match != null)
                            {
                                chkExtArmMus.Checked = true;
                                txtExtArmMus.Text = match.Split('|')[1];
                            }
                            match = reasons.FirstOrDefault(stringToCheck => stringToCheck.Contains("LGM|"));
                            if (match != null)
                            {
                                chkExtLegMus.Checked = true;
                                txtExtLegMus.Text = match.Split('|')[1];
                            }
                            match = reasons.FirstOrDefault(stringToCheck => stringToCheck.Contains("OTH|"));
                            if (match != null)
                            {
                                optExtOther.Checked = true;
                                txtExtOther.Text = match.Split('|')[1];
                            }
                        }

                        if (igd.Skin.IsAbNormal != null)
                        {
                            optSkin.SelectedIndex = (igd.Skin.IsAbNormal ?? false) ? 1 : 0;
                            txtSkin.Text = igd.Skin.Notes;
                        }
                    }

                    txtObjectiveNotes.Text = igd.Notes;
                    optNutritionSkrinning.SelectedValue = igd.NutritionSkrinning;
                }

            }
            catch (Exception)
            {
                // Nothing 
            }
        }
        protected override void OnSetEntityValue(ValidateArgs args, PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            // Triage
            var igd = new Igd();
            var gcs = new Gcs();
            gcs.PainScale = optPainScale.SelectedValue;
            igd.Consciousness = gcs;

            var flacc = new Flacc();
            flacc.Face = ddlFlaccFace.SelectedValue;
            flacc.Legs = ddlFlaccLegs.SelectedValue;
            flacc.Activity = ddlFlaccActivity.SelectedValue;
            flacc.Cry = ddlFlaccCry.SelectedValue;
            flacc.Consolability = ddlFlaccConsolability.SelectedValue;
            igd.Flacc = flacc;

            var esi = new Esi();
            esi.Id = ddlEsi.SelectedValue;
            esi.ConditionIds = cblEsiCondition.SelectedValues;
            igd.Esi = esi;

            // Subjective
            assessment.SubjectiveAddNote = txtSubjective.Text;

            // Objective
            if (optHead.SelectedIndex > -1)
                igd.Head = new AbNormalAndNotes2 { IsAbNormal = optHead.SelectedIndex == 1, Notes = txtHead.Text };

            if (optEye.SelectedIndex > -1)
            {
                if (optEye.SelectedValue == "A")
                {
                    var reasons = new List<string>();
                    if (chkEyesAnemia.Checked ?? false)
                        reasons.Add("ANM");

                    if (chkEyesIkterik.Checked ?? false)
                        reasons.Add("IKT");

                    if ((chkEyesPupil.Checked ?? false) || !string.IsNullOrWhiteSpace(txtEyesPupil.Text))
                    {
                        reasons.Add("PPL|" + txtEyesPupil.Text);
                    }

                    if ((chkEyesOther.Checked ?? false) || !string.IsNullOrWhiteSpace(txtEyesOther.Text))
                    {
                        reasons.Add("OTH|" + txtEyesOther.Text);
                    }

                    igd.Eye = new AbNormalAndReason { IsAbNormal = true, Reasons = reasons };
                }
                else
                    igd.Eye = new AbNormalAndReason { IsAbNormal = false };
            }

            if (optNeck.SelectedIndex > -1)
                igd.Neck = new AbNormalAndNotes2 { IsAbNormal = optNeck.SelectedIndex == 1, Notes = txtNeck.Text };



            if (optPul.SelectedIndex > -1)
            {
                if (optPul.SelectedValue == "A")
                {
                    var reasons = new List<string>();
                    if (chkPulRonki.Checked ?? false)
                        reasons.Add("RNK");

                    if (chkPulWheezing.Checked ?? false)
                        reasons.Add("WZG");

                    if (chkPulRetrac.Checked ?? false)
                        reasons.Add("RTC");

                    if ((chkPulOther.Checked ?? false) || !string.IsNullOrWhiteSpace(txtPulOther.Text))
                    {
                        reasons.Add("OTH|" + txtPulOther.Text);
                    }

                    igd.Pulmo = new AbNormalAndReason { IsAbNormal = true, Reasons = reasons };
                }
                else
                    igd.Pulmo = new AbNormalAndReason { IsAbNormal = false };
            }


            if (optCor.SelectedIndex > -1)
            {
                if (optCor.SelectedValue == "A")
                {
                    var reasons = new List<string>();
                    if (chkCorGallop.Checked ?? false)
                        reasons.Add("GLP");

                    if (chkCorMurmur.Checked ?? false)
                        reasons.Add("MMR");

                    if ((chkCorOther.Checked ?? false) || !string.IsNullOrWhiteSpace(txtCorOther.Text))
                    {
                        reasons.Add("OTH|" + txtCorOther.Text);
                    }

                    igd.Cor = new AbNormalAndReason { IsAbNormal = true, Reasons = reasons };
                }
                else
                    igd.Cor = new AbNormalAndReason { IsAbNormal = false };
            }

            if (optAbd.SelectedIndex > -1)
            {
                if (optAbd.SelectedValue == "A")
                {
                    var reasons = new List<string>();
                    if (chkAbdBiSusUp.Checked ?? false)
                        reasons.Add("BSU");

                    if (chkAbdBiSusDown.Checked ?? false)
                        reasons.Add("BSD");

                    if (chkAbdPressPain.Checked ?? false)
                        reasons.Add("PPN");

                    if (chkAbdHepa.Checked ?? false)
                        reasons.Add("HEP");

                    if (chkAbdSple.Checked ?? false)
                        reasons.Add("SPL");

                    if (chkAbdRelPain.Checked ?? false)
                        reasons.Add("RPN");

                    if ((chkAbdOther.Checked ?? false) || !string.IsNullOrWhiteSpace(txtAbdOther.Text))
                    {
                        reasons.Add("OTH|" + txtAbdOther.Text);
                    }

                    igd.Abdomen = new AbNormalAndReason { IsAbNormal = true, Reasons = reasons };
                }
                else
                    igd.Abdomen = new AbNormalAndReason { IsAbNormal = false };
            }

            if (optExt.SelectedIndex > -1)
            {
                if (optExt.SelectedValue == "A")
                {
                    var reasons = new List<string>();
                    if (chkExtColdAkral.Checked ?? false)
                        reasons.Add("CAK");

                    if (chkExtPitEdem.Checked ?? false)
                        reasons.Add("PED");

                    if (chkExtWeakPul.Checked ?? false)
                        reasons.Add("WPL");

                    if (chkExtPares.Checked ?? false)
                        reasons.Add("PRS");

                    if (chkExtCrt2up.Checked ?? false)
                        reasons.Add("C2U");

                    if (chkExtMenSs.Checked ?? false)
                        reasons.Add("MSS");

                    if (chkExtParesthe.Checked ?? false)
                        reasons.Add("PRT");

                    if ((chkExtArmMus.Checked ?? false) || !string.IsNullOrWhiteSpace(txtExtArmMus.Text))
                    {
                        reasons.Add("ARM|" + txtExtArmMus.Text);
                    }

                    if ((chkExtLegMus.Checked ?? false) || !string.IsNullOrWhiteSpace(txtExtLegMus.Text))
                    {
                        reasons.Add("LGM|" + txtExtLegMus.Text);
                    }

                    if ((optExtOther.Checked ?? false) || !string.IsNullOrWhiteSpace(txtExtOther.Text))
                    {
                        reasons.Add("OTH|" + txtExtOther.Text);
                    }

                    igd.Extremity = new AbNormalAndReason { IsAbNormal = true, Reasons = reasons };
                }
                else
                    igd.Extremity = new AbNormalAndReason { IsAbNormal = false };
            }

            if (optSkin.SelectedIndex > -1)
                igd.Skin = new AbNormalAndNotes2 { IsAbNormal = optSkin.SelectedIndex == 1, Notes = txtSkin.Text };

            igd.Notes = txtObjectiveNotes.Text;
            igd.NutritionSkrinning = optNutritionSkrinning.SelectedValue;

            assessment.PhysicalExam = JsonConvert.SerializeObject(igd);

            // Objective
            rim.Info2 = GenerateSoapObjective(igd);
        }

        private string GenerateSoapObjective(Igd pe)
        {
            var strBuilder = new StringBuilder();
            strBuilder.AppendLine("TRIAGE:");
            strBuilder.AppendFormat("Pain Scale: {0}", pe.Consciousness.PainScale);
            strBuilder.AppendLine(string.Empty);


            // Flacc
            var strbFlacc = new StringBuilder();
            var flaccScore = 0;
            var flacc = StandardReference.LoadStandardReferenceItem(AppEnum.StandardReference.Flacc, pe.Flacc.Face);
            if (flacc.ItemID != null)
            {
                flaccScore = flaccScore + flacc.NumericValue.ToInt();
                strbFlacc.AppendFormat(" - Face: {0}", string.Format("{0} [{1}]", flacc.ItemName, flacc.NumericValue.ToInt()));
                strbFlacc.AppendLine(string.Empty);
            }

            flacc = StandardReference.LoadStandardReferenceItem(AppEnum.StandardReference.Flacc, pe.Flacc.Legs);
            if (flacc.ItemID != null)
            {
                flaccScore = flaccScore + flacc.NumericValue.ToInt();
                strbFlacc.AppendFormat(" - Legs: {0}", string.Format("{0} [{1}]", flacc.ItemName, flacc.NumericValue.ToInt()));
                strbFlacc.AppendLine(string.Empty);
            }

            flacc = StandardReference.LoadStandardReferenceItem(AppEnum.StandardReference.Flacc, pe.Flacc.Activity);
            if (flacc.ItemID != null)
            {
                flaccScore = flaccScore + flacc.NumericValue.ToInt();
                strbFlacc.AppendFormat(" - Activity: {0}", string.Format("{0} [{1}]", flacc.ItemName, flacc.NumericValue.ToInt()));
                strbFlacc.AppendLine(string.Empty);
            }

            flacc = StandardReference.LoadStandardReferenceItem(AppEnum.StandardReference.Flacc, pe.Flacc.Cry);
            if (flacc.ItemID != null)
            {
                flaccScore = flaccScore + flacc.NumericValue.ToInt();
                strbFlacc.AppendFormat(" - Cry: {0}", string.Format("{0} [{1}]", flacc.ItemName, flacc.NumericValue.ToInt()));
                strbFlacc.AppendLine(string.Empty);
            }

            flacc = StandardReference.LoadStandardReferenceItem(AppEnum.StandardReference.Flacc, pe.Flacc.Consolability);
            if (flacc.ItemID != null)
            {
                flaccScore = flaccScore + flacc.NumericValue.ToInt();
                strbFlacc.AppendFormat(" - Consolability: {0}", string.Format("{0} [{1}]", flacc.ItemName, flacc.NumericValue.ToInt()));
                strbFlacc.AppendLine(string.Empty);
            }

            var flaccStr = strbFlacc.ToString();
            if (!string.IsNullOrWhiteSpace(flaccStr))
            {
                strBuilder.AppendFormat("FLACC ({0}):", flaccScore);
                strbFlacc.AppendLine(string.Empty);
                strBuilder.AppendLine(flaccStr);
            }


            // ESI
            var esi = StandardReference.LoadStandardReferenceItem(AppEnum.StandardReference.Triage5Level, pe.Esi.Id);
            if (esi.ItemID != null)
            {
                strBuilder.AppendFormat("{0}: ", esi.ItemName);

                if (pe.Esi.ConditionIds != null && pe.Esi.ConditionIds.Length > 0)
                {
                    var stdi = new AppStandardReferenceItemCollection();
                    stdi.Query.Where(stdi.Query.StandardReferenceID == pe.Esi.Id, stdi.Query.ItemID.In(pe.Esi.ConditionIds));
                    stdi.LoadAll();
                    foreach (var item in stdi)
                    {
                        strBuilder.AppendFormat("{0} | ", item.ItemName.Trim());
                    }
                }
                strBuilder.AppendLine(string.Empty);
            }

            var isIncludeNormal = AppParameter.IsYes(AppParameter.ParameterItem.IsSoapFromPysicalExamIncludeNormalValue);

            // Head

            if (isIncludeNormal || (pe.Head.IsAbNormal ?? false))
            {
                strBuilder.AppendFormat("Kepala: {1}: {0}", pe.Head.Notes, (pe.Head.IsAbNormal ?? false) ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }

            // Eye
            if (pe.Eye.IsAbNormal ?? false)
            {
                strBuilder.Append("Mata: Abnormal: ");

                var reason = string.Join("|", pe.Eye.Reasons);
                if (reason.Contains("ANM"))
                    strBuilder.Append("Anemia | ");

                if (reason.Contains("IKT"))
                    strBuilder.Append("Ikterik | ");

                var reasons = pe.Eye.Reasons;
                var match = reasons.FirstOrDefault(stringToCheck => stringToCheck.Contains("PPL|"));
                if (match != null)
                    strBuilder.AppendFormat("Pupil Anisokor: {0} | ", match.Split('|')[1]);

                match = reasons.FirstOrDefault(stringToCheck => stringToCheck.Contains("OTH|"));
                if (match != null)
                    strBuilder.AppendFormat("Other: {0} | ", match.Split('|')[1]);
                strBuilder.AppendLine(string.Empty);
            }

            // Neck
            if (isIncludeNormal || (pe.Neck.IsAbNormal ?? false))
            {
                strBuilder.AppendFormat("Leher: {1}: {0}", pe.Neck.Notes, (pe.Head.IsAbNormal ?? false) ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }

            // Pulmo
            if (pe.Pulmo.IsAbNormal ?? false)
            {
                strBuilder.Append("Pulmo: Abnormal: ");

                var reason = string.Join("|", pe.Pulmo.Reasons);
                if (reason.Contains("RNK"))
                    strBuilder.Append("Ronki | ");

                if (reason.Contains("WZG"))
                    strBuilder.Append("Wheezing | ");

                if (reason.Contains("RTC"))
                    strBuilder.Append("Retraction | ");

                var reasons = pe.Pulmo.Reasons;
                var match = reasons.FirstOrDefault(stringToCheck => stringToCheck.Contains("OTH|"));
                if (match != null)
                    strBuilder.AppendFormat("Other: {0} | ", match.Split('|')[1]);
                strBuilder.AppendLine(string.Empty);
            }

            // Cor
            if (pe.Cor.IsAbNormal ?? false)
            {
                strBuilder.Append("Cor: Abnormal: ");

                var reason = string.Join("|", pe.Cor.Reasons);
                if (reason.Contains("GLP"))
                    strBuilder.Append("Gallop | ");

                if (reason.Contains("MMR"))
                    strBuilder.Append("Murmur | ");

                var reasons = pe.Cor.Reasons;
                var match = reasons.FirstOrDefault(stringToCheck => stringToCheck.Contains("OTH|"));
                if (match != null)
                    strBuilder.AppendFormat("Other: {0} | ", match.Split('|')[1]);
                strBuilder.AppendLine(string.Empty);
            }

            // Abdomen
            if (pe.Abdomen.IsAbNormal ?? false)
            {
                strBuilder.Append("Abdomen: Abnormal: ");

                var reason = string.Join("|", pe.Abdomen.Reasons);
                if (reason.Contains("BSU"))
                    strBuilder.Append("Bowel sounds increased | ");

                if (reason.Contains("BSD"))
                    strBuilder.Append("Decreased bowel sounds | ");

                if (reason.Contains("PPN"))
                    strBuilder.Append("Pressure Pain | ");

                if (reason.Contains("HEP"))
                    strBuilder.Append("Hepatomegali | ");

                if (reason.Contains("SPL"))
                    strBuilder.Append("Splenomegali | ");

                if (reason.Contains("RPN"))
                    strBuilder.Append("Release Pain | ");

                var reasons = pe.Abdomen.Reasons;
                var match = reasons.FirstOrDefault(stringToCheck => stringToCheck.Contains("OTH|"));
                if (match != null)
                    strBuilder.AppendFormat("Other: {0} | ", match.Split('|')[1]);
                strBuilder.AppendLine(string.Empty);
            }

            // Extremity
            if (pe.Extremity.IsAbNormal ?? false)
            {
                strBuilder.Append("Extremity: Abnormal: ");

                var reason = string.Join("|", pe.Extremity.Reasons);
                if (reason.Contains("CAK"))
                    strBuilder.Append("Cold Akral | ");

                if (reason.Contains("PED"))
                    strBuilder.Append("Pitting Edema | ");

                if (reason.Contains("WPL"))
                    strBuilder.Append("Weak pulse | ");

                if (reason.Contains("SPL"))
                    strBuilder.Append("Splenomegali | ");

                if (reason.Contains("PRS"))
                    strBuilder.Append("Paresis | ");

                if (reason.Contains("C2U"))
                    strBuilder.Append("CRT >2 seconds | ");

                if (reason.Contains("MSS"))
                    strBuilder.Append("Meningeal Stimulation Sign | ");

                if (reason.Contains("PRT"))
                    strBuilder.Append("Paresthesia | ");

                var reasons = pe.Extremity.Reasons;
                var match = reasons.FirstOrDefault(stringToCheck => stringToCheck.Contains("ARM|"));
                if (match != null)
                    strBuilder.AppendFormat("Arm Muscle Strength: {0} | ", match.Split('|')[1]);

                match = reasons.FirstOrDefault(stringToCheck => stringToCheck.Contains("LGM|"));
                if (match != null)
                    strBuilder.AppendFormat("Leg Muscle Strength: {0} | ", match.Split('|')[1]);

                match = reasons.FirstOrDefault(stringToCheck => stringToCheck.Contains("OTH|"));
                if (match != null)
                    strBuilder.AppendFormat("Other: {0} | ", match.Split('|')[1]);
                strBuilder.AppendLine(string.Empty);
            }

            //Kulit
            if (isIncludeNormal || (pe.Skin.IsAbNormal ?? false))
            {
                strBuilder.AppendFormat("Kulit: {1}: {0}", pe.Skin.Notes, (pe.Skin.IsAbNormal ?? false) ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (!string.IsNullOrEmpty(pe.NutritionSkrinning))
            {
                strBuilder.AppendFormat("Skrinning Gizi: {0}", pe.NutritionSkrinning);
                strBuilder.AppendLine(string.Empty);
            }
            //Notes
            if (!string.IsNullOrEmpty(pe.Notes))
            {
                strBuilder.AppendFormat("Lainnya: {0}", pe.Notes);
                strBuilder.AppendLine(string.Empty);
            }

            return strBuilder.ToString();
        }

        #endregion

        protected void ddlEsi_SelectedIndexChanged(object sender, DropDownListEventArgs e)
        {
            PopulateEsiReason(e.Value);
        }

        private void PopulateEsiReason(string esiId)
        {
            cblEsiCondition.Items.Clear();
            var coll = StandardReference.LoadStandardReferenceItemCollection(esiId, String.Empty, true);
            foreach (var stdi in coll)
            {
                cblEsiCondition.Items.Add(new ButtonListItem(stdi.ItemName, stdi.ItemID));
            }
        }
    }
}