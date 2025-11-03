using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Telerik.Web.UI;
using Temiang.Avicenna.Bridging.SatuSehat.BusinessObject;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Avicenna.Bridging.SatuSehat;
using Temiang.Avicenna.Bridging.SatuSehat.Common;
using RestSharp;
using System.Text.RegularExpressions;
using System.Configuration;
using MedicationPost = Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.MedicationPost;
using Temiang.Avicenna.Bridging.PCare.Common;


namespace Temiang.Avicenna.Module.RADT
{
    public partial class ProcessDataToSatuSehat : BasePage
    {
        private string _encounterID;
        private const string _dateFormat = "yyyy-MM-ddTHH:mm:ss";
        private string _satuSehatBridgingType = AppParameter.GetParameterValue(AppParameter.ParameterItem.SatuSehatBridgingTypeID); //"BridgingType-008";
        private string _organizationID = ConfigurationManager.AppSettings["SatuSehatOrganizationID"];// "100026631"; //Dev -> "10000208"; //RS Umum Daerah Tamansari
        private string[] _dayNames = { "Minggu", "Senin", "Selasa", "Rabu", "Kamis", "Jumat", "Sabtu" };
        //private string _gmt = string.Format("{0:00}", AppParameter.GetParameterValue(AppParameter.ParameterItem.GMT).ToInt());
        private int _gmt = 0 - AppParameter.GetParameterValue(AppParameter.ParameterItem.GMT).ToInt();
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.ProcessDataToSatuSehat;

            if (!IsPostBack)
            {
                txtDate.SelectedDate = DateTime.Now;
                PopulateServiceUnit();
            }
        }

        private void PopulateServiceUnit()
        {
            // Hanya untuk non rajal
            var coll = new ServiceUnitCollection();
            var query = new ServiceUnitQuery("a");
            query.Where(query.Or(
                query.SRRegistrationType == AppConstant.RegistrationType.OutPatient
                , query.SRRegistrationType == AppConstant.RegistrationType.EmergencyPatient
                , query.SRRegistrationType == AppConstant.RegistrationType.MedicalCheckUp
                , query.SRRegistrationType == AppConstant.RegistrationType.ClusterPatient
                , query.SRRegistrationType == AppConstant.RegistrationType.Ancillary));

            query.OrderBy(query.ServiceUnitName.Ascending);

            if (AppSession.UserLogin.SRUserType == "NRS")
            {
                var qusr = new AppUserServiceUnitQuery("u");
                query.InnerJoin(qusr).On(query.ServiceUnitID == qusr.ServiceUnitID);
                query.Where(qusr.UserID == AppSession.UserLogin.UserID);
            }
            coll.Load(query);

            cboServiceUnitID.Items.Clear();
            cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (ServiceUnit item in coll)
            {
                cboServiceUnitID.Items.Add(new RadComboBoxItem(item.ServiceUnitName, item.ServiceUnitID));
            }
        }
        private DataTable Registrations
        {
            get
            {
                var qr = new RegistrationQuery("r");
                var qp = new PatientQuery("p");
                qr.InnerJoin(qp).On(qr.PatientID == qp.PatientID);

                var qm = new ParamedicQuery("m");
                qr.LeftJoin(qm).On(qr.ParamedicID == qm.ParamedicID);

                var unit = new ServiceUnitQuery("s");
                qr.LeftJoin(unit).On(qr.ServiceUnitID == unit.ServiceUnitID);

                var guar = new GuarantorQuery("g");
                qr.InnerJoin(guar).On(qr.GuarantorID == guar.GuarantorID);

                var satuSehat = new SatuSehatKunjunganQuery("pc");
                qr.LeftJoin(satuSehat).On(qr.RegistrationNo == satuSehat.RegistrationNo);

                var pb = new PatientBridgingQuery("pb");
                qr.LeftJoin(pb).On(qr.PatientID == pb.PatientID);


                qr.es.Top = AppSession.Parameter.MaxResultRecord;

                // Sub Query Check status Prescription 
                var transPresc = new TransPrescriptionQuery("tp");
                transPresc.Select(transPresc.IsApproval);
                transPresc.Where(transPresc.RegistrationNo == qr.RegistrationNo, "<tp.IsApproval = 1>");
                transPresc.es.Top = 1;

                // Sub Query Check status ICD-10
                var icd = new EpisodeDiagnoseQuery("icd");
                icd.Select("<CAST('1' as BIT) as IsIcd10>");
                icd.Where(icd.RegistrationNo == qr.RegistrationNo, "<icd.DiagnoseID > ''>");
                icd.es.Top = 1;

                // Sub Query Check status VitalSign
                var vs = new PatientHealthRecordLineQuery("vs");
                var qs = new QuestionQuery("q");
                vs.InnerJoin(qs).On(vs.QuestionID == qs.QuestionID);
                vs.Select("<CAST('1' as BIT) as IsVitalSign>");
                vs.Where(vs.RegistrationNo == qr.RegistrationNo, "<q.VitalSignID > ''>");
                vs.es.Top = 1;

                // Sub Query Check status SOAP
                var soap = new RegistrationInfoMedicQuery("soap");
                soap.Select("<CAST('1' as BIT) as IsSoap>");
                soap.Where(soap.RegistrationNo == qr.RegistrationNo);
                soap.es.Top = 1;

                // Sub Query Check status ICD-9
                var icd9 = new EpisodeProcedureQuery("icd9");
                icd9.Select("<CAST('1' as BIT) as IsIcd9>");
                icd9.Where(icd9.RegistrationNo == qr.RegistrationNo);
                icd9.es.Top = 1;

                // Sub Query Check status Education
                var edu = new PatientEducationLineQuery("edu");
                edu.Select("<CAST('1' as BIT) as IsEduDiet>");
                edu.Where(edu.RegistrationNo == qr.RegistrationNo, "<edu.SRPatientEducation = '004'>");//PatientEducation	004	Diet dan nutrisi
                edu.es.Top = 1;


                //var paramedicBridging = new ParamedicBridgingQuery("pcmd");
                //qr.LeftJoin(paramedicBridging).On(qr.ParamedicID == paramedicBridging.BridgingID & paramedicBridging.SRBridgingType == SatuSehatBridgingType);

                qr.Select
                    (
                        qp.PatientID,
                        qr.RegistrationNo,
                        qr.RegistrationDate,
                        qr.RegistrationTime,
                        qp.MedicalNo,
                        qp.PatientName,
                        qp.Sex,
                        qp.Ssn,
                        qm.ParamedicName,
                        unit.ServiceUnitName,
                        "<CAST(1 AS BIT) AS 'IsAllowClose'>",
                        guar.GuarantorName,
                        qr.GuarantorCardNo,
                        satuSehat.ErrorResponse,
                        satuSehat.EncounterID,
                        satuSehat.LastUpdateDateTime,
                        string.Format("<IsVitalSign=COALESCE( ({0}),CAST('0' as BIT))>", vs.Parse()),
                        string.Format("<IsPrescription=COALESCE( ({0}),CAST('0' as BIT))>", transPresc.Parse()),
                        string.Format("<IsIcd10=COALESCE( ({0}),CAST('0' as BIT))>", icd.Parse()),
                        string.Format("<IsIcd9=COALESCE( ({0}),CAST('0' as BIT))>", icd9.Parse()),
                        string.Format("<IsSoap=COALESCE( ({0}),CAST('0' as BIT))>", soap.Parse()),
                        string.Format("<IsEduDiet=COALESCE( ({0}),CAST('0' as BIT))>", edu.Parse())
                        , qr.ParamedicID
                        , pb.BridgingID.As("PatientBridgingID")
                    );


                qr.Where(qr.SRRegistrationType != "IPR");

                if (!chkIncludeClosed.Checked)
                    qr.Where(qr.Or(satuSehat.IsClosed.IsNull(), satuSehat.IsClosed == false));

                if (!chkIncludeFailed.Checked)
                    qr.Where(qr.Or(satuSehat.ErrorResponse.IsNull(), satuSehat.ErrorResponse == ""));

                if (chkHideEmptyIcd10.Checked)
                {
                    var icdExist = new EpisodeDiagnoseQuery("icd");
                    icdExist.Select(icdExist.RegistrationNo);
                    icdExist.Where(icd.RegistrationNo == qr.RegistrationNo, "<icd.DiagnoseID > ''>");

                    qr.Where(qr.RegistrationNo.In(icdExist));
                }

                if (chkHideEmptySSN.Checked)
                {
                    qr.Where(qp.Ssn != "");
                }

                if (!txtDate.IsEmpty)
                    qr.Where(qr.RegistrationDate == txtDate.SelectedDate);

                if (!string.IsNullOrWhiteSpace(cboServiceUnitID.SelectedValue))
                    qr.Where(qr.ServiceUnitID == cboServiceUnitID.SelectedValue);

                if (!string.IsNullOrWhiteSpace(cboParamedicID.SelectedValue))
                    qr.Where(qr.ParamedicID == cboParamedicID.SelectedValue);

                if (txtMedicalNo.Text != string.Empty)
                    qr.Where(qp.MedicalNo == txtMedicalNo.Text);

                if (txtRegistrationNo.Text != string.Empty)
                    qr.Where(qr.RegistrationNo == txtRegistrationNo.Text);

                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = "%" + txtPatientName.Text + "%";
                    qr.Where
                         (
                             string.Format("<LTRIM(RTRIM(LTRIM(p.FirstName + ' ' + p.MiddleName)) + ' ' + p.LastName) LIKE '{0}'>", searchPatient)
                         );
                }

                qr.Where
                    (
                        //qr.IsClosed == true,
                        qr.IsVoid == false,
                        qr.IsFromDispensary == false,
                        qr.ServiceUnitID != AppParameter.GetParameterValue(AppParameter.ParameterItem.ServiceUnitIDForCafe)
                    );

                qr.OrderBy(qr.RegistrationNo.Ascending);

                var tbl = qr.LoadDataTable();

                return tbl;
            }
        }

        protected void grdRegisteredList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdRegisteredList.DataSource = Registrations;
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            grdRegisteredList.MasterTableView.CurrentPageIndex = 0;
            grdRegisteredList.Rebind();
        }

        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            SelectedState(((CheckBox)sender).Checked);
        }

        private void SelectedState(bool selected)
        {
            foreach (CheckBox chkBox in grdRegisteredList.MasterTableView.Items.Cast<GridDataItem>().Select(dataItem => (CheckBox)dataItem.FindControl("detailChkbox")).Where(chkBox => chkBox.Visible))
            {
                chkBox.Checked = selected;
            }
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);
            if ((source is RadGrid))
            {
                if (eventArgument == "process")
                {
                    //// Check apakah ada registrasi ke dokter yg tidak terdaftar di PCare 
                    //// Jika ada munculkan popup penggantinya
                    //var emptyParamedicBridgingID = string.Empty;
                    //foreach (
                    //    GridDataItem dataItem in
                    //    grdRegisteredList.MasterTableView.Items.Cast<GridDataItem>()
                    //        .Where(dataItem => ((CheckBox)dataItem.FindControl("detailChkbox")).Checked))
                    //{
                    //    if (dataItem["ParamedicBridgingID"].Text == "&nbsp" || string.IsNullOrEmpty(dataItem["ParamedicBridgingID"].Text))
                    //    {
                    //        emptyParamedicBridgingID = string.Concat(emptyParamedicBridgingID, "_", dataItem["ParamedicID"].Text);
                    //    }
                    //}


                    var accessToken = string.Empty;
                    var util = new Bridging.SatuSehat.Utils();
                    foreach (
                        GridDataItem dataItem in
                            grdRegisteredList.MasterTableView.Items.Cast<GridDataItem>()
                                .Where(dataItem => ((CheckBox)dataItem.FindControl("detailChkbox")).Checked))
                    {
                        util.PostDataToSatuSehat(dataItem["RegistrationNo"].Text, ref accessToken);
                        //PostDataToSatuSehat(dataItem["RegistrationNo"].Text, ref accessToken);
                    }

                    grdRegisteredList.Rebind();
                }
                else if (eventArgument.Contains("closestatus"))
                {
                    var kunjunganLog = new SatuSehatKunjungan();
                    var regno = eventArgument.Split('|')[1];
                    if (!kunjunganLog.LoadByPrimaryKey(regno))
                    {
                        kunjunganLog = new SatuSehatKunjungan();
                        kunjunganLog.RegistrationNo = regno;
                    }
                    kunjunganLog.IsClosed = true;
                    kunjunganLog.Save();
                    grdRegisteredList.Rebind();
                }
            }
        }
        protected void grdRegisteredList_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                _encounterID = Convert.ToString(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["EncounterID"]);
            }

            if (e.Item is GridNestedViewItem)
            {
                if (string.IsNullOrEmpty(_encounterID)) return;

                //
                var result = new SatuSehatResultQuery("r");
                var guid = new Guid(_encounterID);
                result.Where(result.EncounterID == guid);
                result.OrderBy(result.IndexNo.Ascending);
                result.Select(result, string.Format("<'{0}' as HeaderEncounterID>", _encounterID));
                var dtb = result.LoadDataTable();

                // Populate 
                var grdResult = (RadGrid)e.Item.FindControl("grdResult");

                InitializeCultureGrid(grdResult); // Set date  format

                grdResult.DataSource = dtb;
                grdResult.Rebind();
            }
        }

        #region SatuSehat Webservice access
        private void PostDataToSatuSehat(string registrationNo, ref string accessToken)
        {
            var util = new Bridging.SatuSehat.Utils();

            var satuSehatLog = new SatuSehatKunjungan();
            if (satuSehatLog.LoadByPrimaryKey(registrationNo))
            {
                if (satuSehatLog.EncounterID != null) return;
            }
            else
            {
                satuSehatLog.RegistrationNo = registrationNo;
            }

            var reg = new Registration();
            reg.LoadByPrimaryKey(registrationNo);

            var pat = new BusinessObject.Patient();
            if (!pat.LoadByPrimaryKey(reg.PatientID))
            {
                satuSehatLog.ErrorResponse = "Can not find this patient";
                satuSehatLog.Save();
                return;
            }


            if (string.IsNullOrWhiteSpace(pat.Ssn))
            {
                satuSehatLog.ErrorResponse = "SSN can not empty, please complete the SSN on the master patient";
                satuSehatLog.Save();
                return;
            }

            // Check ICD-10
            var ed = new EpisodeDiagnose();
            ed.Query.Where(ed.Query.RegistrationNo == registrationNo, ed.Query.DiagnoseID > string.Empty, ed.Query.IsVoid == false);
            ed.Query.es.Top = 1;

            if (!ed.Query.Load() || string.IsNullOrWhiteSpace(ed.DiagnoseID))
            {
                satuSehatLog.ErrorResponse = "ICD-10 can not empty,  please complete ICD-10 first";
                satuSehatLog.Save();
                return;
            }


            //// Check ICD-9
            //var ep = new EpisodeProcedure();
            //ep.Query.Where(ep.Query.RegistrationNo == registrationNo, ep.Query.ProcedureID > string.Empty, ep.Query.IsVoid == false);
            //ep.Query.es.Top = 1;

            //if (!ep.Query.Load() || string.IsNullOrWhiteSpace(ep.ProcedureID))
            //{
            //    satuSehatLog.ErrorResponse = "ICD-9 can not empty,  please complete ICD-9 first";
            //    satuSehatLog.Save();
            //    return;
            //}

            // Check Start End
            var regis = new Registration();
            var regTimes = reg.RegistrationTime.Split(':');
            var arrivedTime = reg.RegistrationDate.Value;
            arrivedTime = new DateTime(arrivedTime.Year, arrivedTime.Month, arrivedTime.Day, regTimes[0].ToInt(),
                regTimes[1].ToInt(), 0);

            var startInprogressTime = arrivedTime;
            var finishedTime = arrivedTime;

            regis.Query.Where(regis.Query.RegistrationNo == registrationNo);
            regis.Query.es.Top = 1;

            if (arrivedTime > startInprogressTime)
            {
                satuSehatLog.ErrorResponse = "start date is greater than end date";
                satuSehatLog.Save();
                return;
            }

            var patSs = new PatientBridging();
            patSs.LoadByPrimaryKey(pat.PatientID, _satuSehatBridgingType);
            if (string.IsNullOrWhiteSpace(patSs.BridgingID))
            {
                // Retrieve SS Patient ID
                var response = util.RestClientGet("Patient?identifier=https://fhir.kemkes.go.id/id", string.Concat("nik|", pat.Ssn), ref accessToken);
                if (response.StatusCode == System.Net.HttpStatusCode.Created || response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var patientSearchResponse = JsonConvert.DeserializeObject<Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.PatientSearch.PatientSearchResponse>(response.Content);
                    if (patientSearchResponse.Total == 1)
                    {
                        // Add PatientBridging
                        if (string.IsNullOrEmpty(patSs.PatientID))
                        {
                            patSs = new PatientBridging();
                        }

                        patSs.PatientID = pat.PatientID;
                        patSs.BridgingID = patientSearchResponse.Entry[0].Resource.Id;
                        //patSs.BridgingName = patientSearchResponse.Entry[0].Resource.Name[0].Text; //Mulai 2023 Okt 12 sudah tidak bisa
                        patSs.BridgingName = pat.PatientName;
                        patSs.SRBridgingType = _satuSehatBridgingType;
                        patSs.IsActive = true;
                        patSs.Save();
                    }
                    else
                    {
                        satuSehatLog.ErrorResponse = string.Format("SSN {0} not found at fhir.kemkes.go.id", pat.Ssn);
                        satuSehatLog.Save();
                        return;
                    }
                }
                else
                {
                    satuSehatLog.ErrorResponse = response.Content;
                    satuSehatLog.Save();
                    return;
                }
            }


            var pbQr = new ParamedicBridgingQuery("pb");
            pbQr.Where(pbQr.ParamedicID == reg.ParamedicID, pbQr.SRBridgingType == _satuSehatBridgingType);
            pbQr.es.Top = 1;
            var parMedicSs = new ParamedicBridging();
            if (!parMedicSs.Load((pbQr)))
            {
                var par = new Paramedic();
                par.LoadByPrimaryKey(reg.ParamedicID);
                satuSehatLog.ErrorResponse = string.Format("BridgingID for Physician {0} still empty", par.ParamedicName);
                satuSehatLog.Save();
                return;
            }

            var locSsQr = new ServiceUnitBridgingQuery("pb");
            locSsQr.Where(locSsQr.ServiceUnitID == reg.ServiceUnitID, locSsQr.SRBridgingType == _satuSehatBridgingType);
            locSsQr.es.Top = 1;
            var locSs = new ServiceUnitBridging();
            if (!locSs.Load((locSsQr)))
            {
                var su = new ServiceUnit();
                su.LoadByPrimaryKey(reg.ServiceUnitID);
                satuSehatLog.ErrorResponse = string.Format("BridgingID for Service Unit {0} still empty", su.ServiceUnitName);
                satuSehatLog.Save();
                return;
            }


            // A. Use per step
            var encounterId = PostEncounter(reg, patSs, parMedicSs, locSs, util, ref accessToken); // Kunjungan

            if (!string.IsNullOrEmpty(encounterId))
            {
                // Condition / ICD 10 Primary & Secondary
                PostCondition(reg, patSs, util, encounterId, ref accessToken);

                // Vital Sign
                // 01. Hearth Rate
                PostObservation(util, reg, patSs, parMedicSs, encounterId, accessToken, VitalSign.VitalSignEnum.HeartRate);

                // 02. Pernafasan
                PostObservation(util, reg, patSs, parMedicSs, encounterId, accessToken, VitalSign.VitalSignEnum.RespiratoryRate);

                // 03. Sistol
                PostObservation(util, reg, patSs, parMedicSs, encounterId, accessToken, VitalSign.VitalSignEnum.BloodPressureSistolic);

                // 04. Diastol
                PostObservation(util, reg, patSs, parMedicSs, encounterId, accessToken, VitalSign.VitalSignEnum.BloodPressureDiastolic);

                // 05. Suhu
                PostObservation(util, reg, patSs, parMedicSs, encounterId, accessToken, VitalSign.VitalSignEnum.Temperature);

                // Procedure / ICD 9
                PostProcedure(util, reg, patSs, encounterId, ref accessToken);

                // Composition/ Diet
                PostComposition(util, reg, patSs, parMedicSs, encounterId, ref accessToken);

                // #3. Medication, MedicationRequest, MedicationDispense
                PostMedication(reg, patSs, parMedicSs, util, encounterId, ref accessToken);

                // #5. AllergyIntolerance, ClinicalImpression




                // #5b. Kesadaran, Keluhan Utama, Edukasi, Kondisi Saat Pulang
                var pa = new PatientAssessment();
                pa.Query.Where(pa.Query.RegistrationNo == reg.RegistrationNo);
                pa.Query.es.Top = 1;

                if (!pa.Query.Load())
                {
                    // Kesadaran
                    // PostPatientConsciousness(util, reg, pa, patSs, parMedicSs, encounterId, ref accessToken); //Todo: Level kesadaran belum dapat

                    //Keluhan Utama
                    PostPatientChiefComplaint(util, reg, pa, patSs, encounterId, ref accessToken);
                }

                // Edukasi Obat
                PostPatientEducationMedication(util, reg, patSs, parMedicSs, encounterId, ref accessToken); //Todo: Practitioner pengedukasi harus didaftarkan

                // Kondisi Saat Pulang



                // #6. Radiology


                //06. Rencana Rawat Pasien
                PostCarePlanRawatPasien(util, reg, patSs, parMedicSs, pa, encounterId, ref accessToken);

                // Update Finish status
                PostEncounterFinished(reg, patSs, parMedicSs, locSs, util, ref accessToken); // Kunjungan Finish

                // Close
                Close(reg.RegistrationNo);
            }


            ////B. Or Use Bundle
            //PostBundle(reg, pat);
        }

        private bool IsResultExist(string encounterId, string resType, string category, string code)
        {
            var ent = new SatuSehatResult();
            var ssr = new SatuSehatResultQuery("q");
            ssr.Where(ssr.EncounterID == encounterId, ssr.ResourceType == resType, ssr.Category == category, ssr.Code == code);
            return ent.Load(ssr);
        }

        private void UpdateFinish(string registrationNo)
        {
            var util = new Bridging.SatuSehat.Utils();
            var reg = new Registration();
            reg.LoadByPrimaryKey(registrationNo);

            var pat = new BusinessObject.Patient();
            if (!pat.LoadByPrimaryKey(reg.PatientID))
            {
                return;
            }

            var patSs = new PatientBridging();
            patSs.LoadByPrimaryKey(pat.PatientID, _satuSehatBridgingType);

            var pbQr = new ParamedicBridgingQuery("pb");
            pbQr.Where(pbQr.ParamedicID == reg.ParamedicID, pbQr.SRBridgingType == _satuSehatBridgingType);
            pbQr.es.Top = 1;
            var parMedicSs = new ParamedicBridging();
            if (!parMedicSs.Load((pbQr)))
            {
                return;
            }

            var locSsQr = new ServiceUnitBridgingQuery("pb");
            locSsQr.Where(locSsQr.ServiceUnitID == reg.ServiceUnitID, locSsQr.SRBridgingType == _satuSehatBridgingType);
            locSsQr.es.Top = 1;
            var locSs = new ServiceUnitBridging();
            if (!locSs.Load((locSsQr)))
            {
                return;
            }
            string accessToken = string.Empty;
            PostEncounterFinished(reg, patSs, parMedicSs, locSs, util, ref accessToken); // Kunjungan Finish
            return;
        }

        private void Close(string registrationNo)
        {
            var isNoError = true;
            var satuSehatLog = new SatuSehatKunjungan();
            if (satuSehatLog.LoadByPrimaryKey(registrationNo))
            {
                if (string.IsNullOrWhiteSpace(satuSehatLog.ErrorResponse))
                {
                    var ssResults = new SatuSehatResultCollection();
                    ssResults.Query.Where(ssResults.Query.EncounterID == satuSehatLog.EncounterID);
                    ssResults.Query.Select(satuSehatLog.Query.ErrorResponse);
                    ssResults.LoadAll();
                    foreach (var ssResult in ssResults)
                    {
                        if (!string.IsNullOrEmpty(ssResult.ErrorResponse))
                        {
                            isNoError = false;
                            break;
                        }
                    }

                }
            }

            if (isNoError)
            {
                satuSehatLog.IsClosed = true;
                satuSehatLog.Save();
            }
        }

        private BaseResponse RestClientPostAndSaveLog(Utils util, string resourceType, string requestBody, SatuSehatResult ssResult, ref string accessToken)
        {
            BaseResponse conditionResponse = null;

            var response = util.RestClientPost(requestBody, resourceType, ref accessToken);

            if (response.StatusCode == System.Net.HttpStatusCode.Created || response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //switch (resourceType)
                //{
                //    case "Condition":
                //        conditionResponse = JsonConvert.DeserializeObject<ConditionResponse>(response.Content);
                //        break;
                //    case "Procedure":
                //        conditionResponse = JsonConvert.DeserializeObject<ProcedureResponse>(response.Content);
                //        break;
                //    case "Composition":
                //        conditionResponse = JsonConvert.DeserializeObject<CompositionResponse>(response.Content);
                //        break;
                //    case "Medication":
                //        conditionResponse = JsonConvert.DeserializeObject<Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.MedicationResponse.Root>(response.Content);
                //        break;
                //    case "MedicationRequest":
                //        conditionResponse = JsonConvert.DeserializeObject<Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.MedicationRequestResponse.Root>(response.Content);
                //        break;
                //    case "MedicationDispense":
                //        conditionResponse = JsonConvert.DeserializeObject<Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.MedicationDispenseResponse.Root>(response.Content);
                //        break;
                //    case "Observation":
                //        conditionResponse = JsonConvert.DeserializeObject<ObservationResponse>(response.Content);
                //        break;
                //    default:
                //        throw new Exception(string.Format("ResourceType: {0}, not yet defined", resourceType));
                //}
                conditionResponse = JsonConvert.DeserializeObject<BaseResponse>(response.Content);
                if (!string.IsNullOrEmpty(conditionResponse.Id))
                {
                    ssResult.ResultID = new Guid(conditionResponse.Id);
                }
            }
            else
            {
                ssResult.ErrorResponse = response.Content;
            }

            ssResult.ResourceType = resourceType;
            ssResult.PostData = requestBody;

            SetResultIndexNo(ssResult);

            ssResult.Save();

            return conditionResponse;
        }

        private static void SetResultIndexNo(SatuSehatResult ssResult)
        {
            if (ssResult.IndexNo == null || ssResult.IndexNo == 0)
            {
                var srQr = new SatuSehatResultQuery("sr");
                srQr.Where(srQr.EncounterID == ssResult.EncounterID);
                srQr.es.Top = 1;
                srQr.Select(srQr.IndexNo);
                srQr.OrderBy(srQr.IndexNo.Descending);
                var dtb = srQr.LoadDataTable();
                if (dtb.Rows.Count > 0)
                    ssResult.IndexNo = dtb.Rows[0][0].ToInt() + 1;
                else
                    ssResult.IndexNo = 1;
            }
        }


        #region STEP BY STEP
        #region Encounter / Kunjungan
        private string PostEncounter(Registration reg, PatientBridging patSs, ParamedicBridging parSs, ServiceUnitBridging locSs, Utils util, ref string accessToken)
        {
            var encounterId = string.Empty;
            var encounterPostData = EncounterPostData(reg, patSs, parSs, locSs);
            var requestBody = JsonConvert.SerializeObject(encounterPostData);

            var satuSehatLog = new SatuSehatKunjungan();
            if (!satuSehatLog.LoadByPrimaryKey(reg.RegistrationNo))
                satuSehatLog = new SatuSehatKunjungan();

            satuSehatLog.KunjunganPostData = requestBody;
            satuSehatLog.RegistrationNo = reg.RegistrationNo;
            satuSehatLog.str.ErrorResponse = string.Empty;

            var response = util.RestClientPost(requestBody, "Encounter", ref accessToken);

            if (response.StatusCode == System.Net.HttpStatusCode.Created || response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var encounterResponse = JsonConvert.DeserializeObject<EncounterResponse>(response.Content);
                if (!string.IsNullOrEmpty(encounterResponse.Id))
                {
                    encounterId = encounterResponse.Id;
                    satuSehatLog.EncounterID = new Guid(encounterResponse.Id);
                }
            }
            else
            {
                satuSehatLog.ErrorResponse = response.Content;
            }

            satuSehatLog.Save();

            return encounterId;
        }

        private string PostEncounterFinished(Registration reg, PatientBridging patSs, ParamedicBridging parSs, ServiceUnitBridging locSs, Utils util, ref string accessToken)
        {
            // Update status Finish
            var satuSehatLog = new SatuSehatKunjungan();
            if (!satuSehatLog.LoadByPrimaryKey(reg.RegistrationNo))
                return string.Empty;

            var encounterId = satuSehatLog.EncounterID.ToString();
            var encounterPostData = EncounterFinishPutData(reg, patSs, parSs, locSs, encounterId);
            var requestBody = JsonConvert.SerializeObject(encounterPostData);
            satuSehatLog.KunjunganPostData = requestBody;
            satuSehatLog.str.ErrorResponse = string.Empty;

            var response = util.RestClientPut(requestBody, string.Format("Encounter/{0}", encounterId), ref accessToken);

            if (response.StatusCode == System.Net.HttpStatusCode.Created || response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var encounterResponse = JsonConvert.DeserializeObject<EncounterResponse>(response.Content);
                if (!string.IsNullOrEmpty(encounterResponse.Id))
                {
                    encounterId = encounterResponse.Id;
                    satuSehatLog.EncounterID = new Guid(encounterResponse.Id);
                }
            }
            else
            {
                satuSehatLog.ErrorResponse = response.Content;
            }

            satuSehatLog.Save();

            return encounterId;
        }

        private EncounterPost EncounterPostData(Registration reg, PatientBridging patSs, ParamedicBridging parSs, ServiceUnitBridging locSs)
        {
            var postData = new EncounterPost();
            postData.ResourceType = "Encounter";
            postData.Status = "arrived";
            postData.Class = new Bridging.SatuSehat.BusinessObject.Class()
            {
                System = "http://terminology.hl7.org/CodeSystem/v3-ActCode",
                Code = "AMB",
                Display = "ambulatory"
            };
            postData.Subject = new RefAndDisplay()
            {
                Reference = string.Format("Patient/{0}", patSs.BridgingID),
                Display = patSs.BridgingName
            };

            var codings = new List<Coding>() { new Coding()
                            {
                                System = "http://terminology.hl7.org/CodeSystem/v3-ParticipationType",
                                Code = "ATND",
                                Display = "attender"
                            } };
            var types = new List<Code>()
                            {new Code(){ Coding= codings}  };


            var par = new Paramedic();
            par.LoadByPrimaryKey(reg.ParamedicID);
            postData.Participant = new List<Participant>() {
                                    new Participant(){Individual= new Individual(){ Reference= string.Format("Practitioner/{0}",parSs.BridgingID),
                        Display= parSs.BridgingName}, Type = types } };

            postData.Location = new List<Bridging.SatuSehat.BusinessObject.Location>()
            {
                new Bridging.SatuSehat.BusinessObject.Location()
                {
                    LocationItem = new Bridging.SatuSehat.BusinessObject.RefDisplay()
                    {
                        Reference= string.Format("Location/{0}",locSs.BridgingID),
                        Display= locSs.BridgingName
                    },
                    Extension = new List<Bridging.SatuSehat.BusinessObject.ExtensionLoc>()
                    {
                        new ExtensionLoc()
                        {
                            Url = "https://fhir.kemkes.go.id/r4/StructureDefinition/ServiceClass",
                            ExtensionItem = new List<ExtensionItem>()
                                            {
                                                new ExtensionItem()
                                                {
                                                    Url= "value",
                                                    ValueCodeableConcept = new Code()
                                                    {
                                                        Coding = new List<Coding>(){ new Coding()
                                                            {
                                                                System = "http://terminology.kemkes.go.id/CodeSystem/locationServiceClass-Outpatient",
                                                                Code = "reguler",
                                                                Display = "Kelas Reguler"
                                                            }
                                                        }
                                                    }

                                                }
                                            }
                            }
                    }
                }
            };


            // StatusHistory
            postData.StatusHistory = new List<StatusHistory>();
            var regTimes = reg.RegistrationTime.Split(':');
            var arrivedTime = reg.RegistrationDate.Value;
            arrivedTime = new DateTime(arrivedTime.Year, arrivedTime.Month, arrivedTime.Day, regTimes[0].ToInt(),
                regTimes[1].ToInt(), 0);

            var startInprogressTime = arrivedTime;
            var finishedTime = arrivedTime;

            //var startInprogress = string.Empty;

            // Jam dipanggil
            var pa = new PatientAssessment();
            pa.Query.Where(pa.Query.RegistrationNo == reg.RegistrationNo);
            pa.Query.es.Top = 1;
            pa.Query.OrderBy(pa.Query.AssessmentDateTime.Descending);
            if (pa.Query.Load())
            {
                if (arrivedTime > pa.AssessmentDateTime.Value) //Kasus RegistrationTime tidak sesuai dgn jam kedatangan (Contoh dari Appointment)
                    arrivedTime = reg.LastCreateDateTime.Value;

                startInprogressTime = pa.AssessmentDateTime.Value;

                postData.Status = "in-progress"; //Change status
            }
            else
                startInprogressTime = arrivedTime.AddMinutes(5); // tidak diketahui jam dipanggilnya sehingga anggap saja 5 menit

            // selesai ketika diberi resep
            var presc = new TransPrescription();
            presc.Query.Where(presc.Query.RegistrationNo == reg.RegistrationNo, presc.Query.IsApproval == true);
            presc.Query.es.Top = 1;
            presc.Query.OrderBy(presc.Query.PrescriptionDate.Descending);
            if (presc.Query.Load())
            {
                if (startInprogressTime > presc.CreatedDateTime.Value) // Kasus asesmen dientry setelah resep dibuat
                {
                    startInprogressTime = presc.CreatedDateTime.Value.AddMinutes(-1);
                }

                postData.StatusHistory.Add(new StatusHistory()
                {
                    Status = "in-progress",
                    Period = new Period()
                    {
                        Start = string.Format("{0}+00:00", startInprogressTime.AddHours(_gmt).AddHours(_gmt).ToString(_dateFormat)),
                        End = string.Format("{0}+00:00", presc.CreatedDateTime.Value.AddHours(_gmt).AddHours(_gmt).ToString(_dateFormat))
                    }
                });


                // Status finish dipindah ke akhir karena harus ada diagnosa dulu
                //// finished 
                //postData.StatusHistory.Add(new StatusHistory()
                //{
                //    Status = "finished",
                //    Period = new Period()
                //    {
                //        Start = string.Format("{0}+{1}:00", presc.CreatedDateTime.Value.ToString(_dateFormat), _gmt),
                //        End = string.Format("{0}+{1}:00", (presc.DeliverDateTime ?? presc.ApprovalDateTime).Value.ToString(_dateFormat), _gmt)
                //    }
                //});
                //postData.Status = "finished"; //Change status

            }

            // arrived
            postData.StatusHistory.Insert(0, new StatusHistory()
            {
                Status = "arrived",
                Period = new Period()
                {
                    Start = string.Format("{0}+00:00", arrivedTime.AddHours(_gmt).ToString(_dateFormat)),
                    End = string.Format("{0}+00:00", startInprogressTime.AddHours(_gmt).ToString(_dateFormat))
                }
            });


            // Period
            postData.Period = new Period() { Start = string.Format("{0}+00:00", arrivedTime.AddHours(_gmt).ToString(_dateFormat)) }; //"2022-06-14T07:00:00+07:00"

            postData.ServiceProvider = new ServiceProvider()
            {
                Reference = String.Format("Organization/{0}", _organizationID)
            };

            // No kunjungan / registrasi internal
            postData.Identifier = new List<Identifier>()
            {
                new Identifier() { System = string.Format("http://sys-ids.kemkes.go.id/encounter/{0}",_organizationID), Value = reg.RegistrationNo }
            };
            return postData;
        }

        private EncounterFinishPut EncounterFinishPutData(Registration reg, PatientBridging patSs, ParamedicBridging parSs, ServiceUnitBridging locSs, string encounterId)
        {
            var postData = new EncounterFinishPut();
            postData.ResourceType = "Encounter";
            postData.Class = new Bridging.SatuSehat.BusinessObject.Class()
            {
                System = "http://terminology.hl7.org/CodeSystem/v3-ActCode",
                Code = "AMB",
                Display = "ambulatory"
            };

            // Diagnosa
            var ssres = new SatuSehatResultQuery("r");
            ssres.Where(ssres.EncounterID == new Guid(encounterId), ssres.ResourceType == "Condition", ssres.Category == "Diagnosis");
            ssres.Select(ssres.IndexNo, ssres.ResultID, ssres.Code, ssres.PostData);
            var dtbDiag = ssres.LoadDataTable();
            if (dtbDiag.Rows.Count == 0)
                return postData;

            var diags = new List<Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.Diagnosis>();
            foreach (DataRow row in dtbDiag.Rows)
            {
                var jsonDiag = JsonConvert.DeserializeObject<ConditionResponse>(row["PostData"].ToString());
                var diag = new Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.Diagnosis();
                diag.Condition = new Condition() { Display = jsonDiag.Code.Coding[0].Display, Reference = string.Format("Condition/{0}", row["ResultID"].ToString()) };
                diag.Rank = row["IndexNo"].ToInt();
                diag.Use = new Use() { Coding = new List<Coding> { new Coding() { Code = "DD", Display = "Discharge diagnosis", System = "http://terminology.hl7.org/CodeSystem/diagnosis-role" } } };
                diags.Add(diag);
            }
            postData.Diagnosis = diags;

            postData.ID = encounterId;

            postData.Location = new List<Bridging.SatuSehat.BusinessObject.Location>()
            {
                new Bridging.SatuSehat.BusinessObject.Location()
                {
                    LocationItem = new Bridging.SatuSehat.BusinessObject.RefDisplay()
                    {
                        Reference= string.Format("Location/{0}",locSs.BridgingID),
                        Display= locSs.BridgingName
                    },
                    Extension = new List<Bridging.SatuSehat.BusinessObject.ExtensionLoc>()
                    {
                        new ExtensionLoc()
                        {
                            Url = "https://fhir.kemkes.go.id/r4/StructureDefinition/ServiceClass",
                            ExtensionItem = new List<ExtensionItem>()
                                            {
                                                new ExtensionItem()
                                                {
                                                    Url= "value",
                                                    ValueCodeableConcept = new Code()
                                                    {
                                                        Coding = new List<Coding>(){ new Coding()
                                                            {
                                                                System = "http://terminology.kemkes.go.id/CodeSystem/locationServiceClass-Outpatient",
                                                                Code = "reguler",
                                                                Display = "Kelas Reguler"
                                                            }
                                                        }
                                                    }

                                                }
                                            }
                            }
                    }
                }
            };


            postData.Subject = new RefAndDisplay()
            {
                Reference = string.Format("Patient/{0}", patSs.BridgingID),
                Display = patSs.BridgingName
            };

            var codings = new List<Coding>() { new Coding()
                            {
                                System = "http://terminology.hl7.org/CodeSystem/v3-ParticipationType",
                                Code = "ATND",
                                Display = "attender"
                            } };
            var types = new List<Code>()
                            {new Code(){ Coding= codings}  };


            var par = new Paramedic();
            par.LoadByPrimaryKey(reg.ParamedicID);
            postData.Participant = new List<Participant>() {
                                    new Participant(){Individual= new Individual(){ Reference= string.Format("Practitioner/{0}",parSs.BridgingID),
                        Display= parSs.BridgingName}, Type = types } };

            postData.Status = "finished";

            // StatusHistory
            postData.StatusHistory = new List<StatusHistory>();
            var regTimes = reg.RegistrationTime.Split(':');
            var arrivedTime = reg.RegistrationDate.Value;
            arrivedTime = new DateTime(arrivedTime.Year, arrivedTime.Month, arrivedTime.Day, regTimes[0].ToInt(),
                regTimes[1].ToInt(), 0);

            var startInprogressTime = arrivedTime;
            var finishedTime = arrivedTime;

            //var startInprogress = string.Empty;

            // Jam dipanggil
            var pa = new PatientAssessment();
            pa.Query.Where(pa.Query.RegistrationNo == reg.RegistrationNo);
            pa.Query.es.Top = 1;
            pa.Query.OrderBy(pa.Query.AssessmentDateTime.Descending);
            if (pa.Query.Load())
            {
                if (arrivedTime > pa.AssessmentDateTime.Value) //Kasus RegistrationTime tidak sesuai dgn jam kedatangan (Contoh dari Appointment)
                    arrivedTime = reg.LastCreateDateTime.Value;

                startInprogressTime = pa.AssessmentDateTime.Value;

                postData.Status = "in-progress"; //Change status
            }
            else
                startInprogressTime = arrivedTime.AddMinutes(5); // tidak diketahui jam dipanggilnya sehingga anggap saja 5 menit

            // selesai ketika diberi resep
            var presc = new TransPrescription();
            presc.Query.Where(presc.Query.RegistrationNo == reg.RegistrationNo, presc.Query.IsApproval == true);
            presc.Query.es.Top = 1;
            presc.Query.OrderBy(presc.Query.PrescriptionDate.Descending);
            if (presc.Query.Load())
            {
                if (startInprogressTime > presc.CreatedDateTime.Value) // Kasus asesmen dientry setelah resep dibuat
                {
                    startInprogressTime = presc.CreatedDateTime.Value.AddMinutes(-1);
                }

                postData.StatusHistory.Add(new StatusHistory()
                {
                    Status = "in-progress",
                    Period = new Period()
                    {
                        Start = string.Format("{0}+00:00", startInprogressTime.AddHours(_gmt).ToString(_dateFormat)),
                        End = string.Format("{0}+00:00", presc.CreatedDateTime.Value.AddHours(_gmt).ToString(_dateFormat))
                    }
                });


                // finished
                postData.StatusHistory.Add(new StatusHistory()
                {
                    Status = "finished",
                    Period = new Period()
                    {
                        Start = string.Format("{0}+00:00", presc.CreatedDateTime.Value.AddHours(_gmt).ToString(_dateFormat)),
                        End = string.Format("{0}+00:00", (presc.DeliverDateTime ?? presc.ApprovalDateTime).Value.AddHours(_gmt).ToString(_dateFormat))
                    }
                });
                postData.Status = "finished"; //Change status

            }

            // arrived
            postData.StatusHistory.Insert(0, new StatusHistory()
            {
                Status = "arrived",
                Period = new Period()
                {
                    Start = string.Format("{0}+00:00", arrivedTime.AddHours(_gmt).ToString(_dateFormat)),
                    End = string.Format("{0}+00:00", startInprogressTime.AddHours(_gmt).ToString(_dateFormat))
                }
            });


            // Period
            postData.Period = new Period() { Start = string.Format("{0}+00:00", arrivedTime.AddHours(_gmt).ToString(_dateFormat)) }; //"2022-06-14T07:00:00+07:00"

            postData.ServiceProvider = new ServiceProvider()
            {
                Reference = String.Format("Organization/{0}", _organizationID)
            };

            // No kunjungan / registrasi internal
            postData.Identifier = new List<Identifier>()
            {
                new Identifier() { System = string.Format("http://sys-ids.kemkes.go.id/encounter/{0}",_organizationID), Value = reg.RegistrationNo }
            };
            return postData;
        }

        #endregion

        #region Condition / Diagnose
        private void PostCondition(Registration reg, PatientBridging patSs, Utils util, string encounterId, ref string accessToken)
        {
            var epDiags = new EpisodeDiagnoseCollection();
            epDiags.Query.Where(epDiags.Query.RegistrationNo == reg.RegistrationNo, epDiags.Query.IsVoid == false);
            //epDiags.Query.es.Top = 2;
            epDiags.LoadAll();

            var i = 0;
            foreach (var diag in epDiags)
            {
                if (string.IsNullOrWhiteSpace(diag.DiagnoseID)) continue;

                var postData = ConditionPostData(reg, patSs, diag, encounterId);
                if (postData == null)
                {
                    var ssResultFail = new SatuSehatResult()
                    {
                        EncounterID = new Guid(encounterId),
                        Category = "Diagnosis",
                        Code = diag.DiagnoseID,
                        ErrorResponse = string.Format("ICD-10: {0} tidak terdaftar di Satusehat", diag.DiagnoseID),
                        ResourceType = "Condition"
                    };
                    ssResultFail.Save();
                    continue;
                }

                var requestBody = JsonConvert.SerializeObject(postData);

                var ssResult = new SatuSehatResult()
                {
                    EncounterID = new Guid(encounterId),
                    Category = "Diagnosis",
                    Code = diag.DiagnoseID
                };
                RestClientPostAndSaveLog(util, postData.ResourceType, requestBody, ssResult, ref accessToken);
            }
        }

        private ConditionPost ConditionPostData(Registration reg, PatientBridging patSs, EpisodeDiagnose epDiagnose, string encounterId)
        {
            var diagID = epDiagnose.DiagnoseID;
            var diagText = string.Empty;

            // Check exist in SatuSehat ICDX
            var diag = new Diagnose();
            if (diag.LoadByPrimaryKey(diagID))
            {
                if (diag.IsSatuSehat == null || false.Equals(diag.IsSatuSehat)) // Tidak terdaftar di satusehat
                {
                    // Naikan level
                    // Sample:
                    // A09.9+ -> A09.9
                    // A18.0    + ->  A18.0

                    var i = 1;
                    while (true)
                    {
                        if (diagID.Length == 0) break;

                        diagID = diagID.Substring(0, diagID.Length - 1); // Naikan level
                        diag.QueryReset();
                        if (diag.LoadByPrimaryKey(diagID) && true.Equals(diag.IsSatuSehat))
                        {
                            diagID = diag.DiagnoseID;
                            diagText = diag.DiagnoseName;
                            break;
                        }

                        i++;
                    }
                }
                else
                    diagText = diag.DiagnoseName;
            }

            if (string.IsNullOrWhiteSpace(diagText)) return null;

            var postData = new ConditionPost();
            postData.ResourceType = "Condition";
            postData.ClinicalStatus = new ClinicalStatus()
            {
                Coding = new List<Coding>() {
                            new Coding() {
                                System = "http://terminology.hl7.org/CodeSystem/condition-clinical",
                   Code= "active",
                   Display= "Active"}
                    }
            };


            postData.Category = new List<Category>() { new Category()
             {
                            Coding = new List<Coding>() { new Coding() {
                                System = "http://terminology.hl7.org/CodeSystem/condition-category",
                      Code= "encounter-diagnosis",
                      Display= "Encounter Diagnosis"
                               }
                    }
             }
            };


            postData.Code = new Code()
            {
                Coding = new List<Coding>(){ new Coding()
                    {
                        System = "http://hl7.org/fhir/sid/icd-10",
                        Code = diagID,
                        Display = diagText
                    }
             }
            };

            postData.Subject = new RefAndDisplay()
            {
                Reference = string.Format("Patient/{0}", patSs.BridgingID),
                Display = patSs.BridgingName
            };

            postData.Encounter = new RefAndDisplay()
            {
                Reference = String.Format("Encounter/{0}", encounterId),
                Display = string.Format("Kunjungan {0} di hari {1}", patSs.BridgingName, _dayNames[reg.RegistrationDate.Value.DayOfWeek.ToInt()])
            };
            postData.OnsetDateTime = string.Format("{0}+00:00", (epDiagnose.CreateDateTime ?? epDiagnose.LastUpdateDateTime).Value.AddHours(_gmt).ToString(_dateFormat));
            postData.RecordedDate = postData.OnsetDateTime;

            return postData;
        }

        #endregion

        //    private string EncounterAndConditionPutData(string encounterID, string paramedicBridgingID, string paramedicName)
        //    {
        //        var participant = new List<object>() {
        //          new {
        //            type = new List<object>() {
        //                new {
        //                  coding = new List<object>() {
        //                    new {
        //                      system = "http://terminology.hl7.org/CodeSystem/v3-ParticipationType",
        //                        code = "ATND",
        //                        display = "attender"
        //                    }
        //                  }
        //                }
        //              },
        //              individual = new {
        //                reference = string.Format("Practitioner/{0}",paramedicBridgingID),  //"Practitioner/N10000001",
        //                  display = paramedicName
        //              }
        //          }
        //        };

        //        var diagnosis = new List<object>() {
        //    new {
        //        condition = new {
        //            reference = "Condition/f2bc12fe-0ab2-4e5c-a3cd-32c66150cbe9",
        //            display = "Tuberculosis of lung, confirmed by sputum microscopy with or without culture"
        //        },
        //        use = new {
        //            coding = new List<object>() {
        //                new {
        //                    system = "http://terminology.hl7.org/CodeSystem/diagnosis-role",
        //                    code = "DD",
        //                    display = "Discharge diagnosis"
        //                }
        //            }
        //        },
        //        rank = 1
        //    },
        //    new {
        //        condition = new {
        //            reference = "Condition/ba0dd351-c30a-4659-994e-0013797b545b",
        //            display = "Non-insulin-dependent diabetes mellitus without complications"
        //        },
        //        use = new {
        //            coding = new List<object>() {
        //                new {
        //                    system = "http://terminology.hl7.org/CodeSystem/diagnosis-role",
        //                    code = "DD",
        //                    display = "Discharge diagnosis"
        //                }
        //            }
        //        },
        //        rank = 2
        //    }
        //};

        //        var data = new
        //        {
        //            resourceType = "Encounter",
        //            id = "2823ed1d-3e3e-434e-9a5b-9c579d192787",
        //            identifier = new List<object>() {
        //    new {
        //        system = "http://sys-ids.kemkes.go.id/encounter/10000004",
        //        value = "P20240001"
        //    }
        //},
        //            status = "finished",
        //            classx = new
        //            {
        //                system = "http://terminology.hl7.org/CodeSystem/v3-ActCode",
        //                code = "AMB",
        //                display = "ambulatory"
        //            },
        //            subject = new
        //            {
        //                reference = "Patient/100000030009",
        //                display = "Budi Santoso"
        //            },
        //            participant = participant,
        //            period = new
        //            {
        //                start = "2022-06-14T07:00:00+07:00",
        //                end = "2022-06-14T09:00:00+07:00"
        //            },
        //            location = new List<object>() {
        //    new {
        //        location = new {
        //            reference = "Location/ef011065-38c9-46f8-9c35-d1fe68966a3e",
        //            display = "Ruang 1A, Poliklinik Rawat Jalan"
        //        }
        //    }
        //},
        //            diagnosis = diagnosis,
        //            statusHistory = new List<object>() {
        //    new {
        //        status = "arrived",
        //        period = new {
        //            start = "2022-06-14T07:00:00+07:00",
        //            end = "2022-06-14T08:00:00+07:00"
        //        }
        //    },
        //    new {
        //            status = "in-progress",
        //        period = new {
        //                start = "2022-06-14T08:00:00+07:00",
        //        end = "2022-06-14T09:00:00+07:00"
        //    }
        //        },
        //    new {
        //            status = "finished",
        //        period = new {
        //            start = "2022 - 06 - 14T09: 00:00 + 07:00",
        //            end = "2022 - 06 - 14T09: 00:00 + 07:00"
        //        }
        //    }
        //            },
        //            serviceProvider = new
        //            {
        //                reference = "Organization/10000004"
        //            }
        //        };
        //        return "";
        //    }
        #region Observation / Vital Sign
        private void PostObservation(Utils util, Registration reg, PatientBridging patSs, ParamedicBridging parMedicSs, string encounterId, string accessToken, VitalSign.VitalSignEnum vitalSignEnum)
        {
            var observationPostData = ObservationPostData(reg, patSs, parMedicSs, vitalSignEnum, encounterId);
            if (observationPostData == null)
                return;

            var ssResult = new SatuSehatResult();
            ssResult.EncounterID = new Guid(encounterId);
            ssResult.ResourceType = observationPostData.ResourceType;
            ssResult.Category = "vital-signs";

            switch (vitalSignEnum)
            {
                case VitalSign.VitalSignEnum.BodyWeight:
                    break;
                case VitalSign.VitalSignEnum.BodyHeight:
                    break;
                case VitalSign.VitalSignEnum.BloodPressure:
                    break;
                case VitalSign.VitalSignEnum.BloodPressureSistolic:
                    ssResult.Code = "BPS";
                    break;
                case VitalSign.VitalSignEnum.BloodPressureDiastolic:
                    ssResult.Code = "BPD";
                    break;
                case VitalSign.VitalSignEnum.HeartRate:
                    ssResult.Code = "HR";
                    break;
                case VitalSign.VitalSignEnum.RespiratoryRate:
                    ssResult.Code = "Resp";
                    break;
                case VitalSign.VitalSignEnum.Temperature:
                    ssResult.Code = "Temp";
                    break;
                case VitalSign.VitalSignEnum.PainScale:
                    break;
                case VitalSign.VitalSignEnum.SpO2:
                    break;
                default:
                    break;
            }

            var requestBody = JsonConvert.SerializeObject(observationPostData);
            RestClientPostAndSaveLog(util, observationPostData.ResourceType, requestBody, ssResult, ref accessToken);
        }

        private ObservationPost ObservationPostData(Registration reg, PatientBridging patSs, ParamedicBridging parMedicSs, VitalSign.VitalSignEnum vitalSignEnum, string encounterId)
        {
            var vitalSign = VitalSign.LastVitalSignItem(reg.RegistrationNo, reg.FromRegistrationNo, vitalSignEnum, DateTime.Now);
            if (vitalSign.Value == 0)
                return null;

            string vitalSignCode = String.Empty;
            string vitalSignDisplay = String.Empty;
            var valueQuantity = new ValueQuantity();
            var vitalSignDateTime = vitalSign.RecordDateTime;
            List<Interpretation> interpretation = null;


            switch (vitalSignEnum)
            {
                case VitalSign.VitalSignEnum.BodyWeight:
                    break;
                case VitalSign.VitalSignEnum.BodyHeight:
                    break;
                case VitalSign.VitalSignEnum.BloodPressure:
                    break;
                case VitalSign.VitalSignEnum.BloodPressureSistolic:
                    {
                        vitalSignCode = "8480-6";
                        vitalSignDisplay = "Systolic blood pressure";
                        valueQuantity = new ValueQuantity() { Value = vitalSign.Value.ToInt(), Unit = "mm[Hg]", System = "http://unitsofmeasure.org", Code = "mm[Hg]" };

                        if (vitalSign.Value.ToInt() > 199)
                            interpretation = HighObservation();

                        break;
                    }
                case VitalSign.VitalSignEnum.BloodPressureDiastolic:
                    {
                        vitalSignCode = "8462-4";
                        vitalSignDisplay = "Diastolic blood pressure";
                        valueQuantity = new ValueQuantity() { Value = vitalSign.Value.ToInt(), Unit = "mm[Hg]", System = "http://unitsofmeasure.org", Code = "mm[Hg]" };

                        if (vitalSign.Value.ToInt() > 79)
                        {
                            interpretation = HighObservation();
                        }
                        break;
                    }
                case VitalSign.VitalSignEnum.HeartRate:
                    {
                        vitalSignCode = "8867-4";
                        vitalSignDisplay = "Heart rate";
                        valueQuantity = new ValueQuantity() { Value = vitalSign.Value.ToInt(), Unit = "beats/minute", System = "http://unitsofmeasure.org", Code = "/min" };
                        break;
                    }
                case VitalSign.VitalSignEnum.RespiratoryRate:
                    {
                        vitalSignCode = "9279-1";
                        vitalSignDisplay = "Respiratory rate";
                        valueQuantity = new ValueQuantity() { Value = vitalSign.Value.ToInt(), Unit = "breaths/minute", System = "http://unitsofmeasure.org", Code = "/min" };
                        break;
                    }
                case VitalSign.VitalSignEnum.Temperature:
                    {
                        vitalSignCode = "8310-5";
                        vitalSignDisplay = "Body temperature";
                        valueQuantity = new ValueQuantity() { Value = vitalSign.Value.ToDouble(), Unit = "C", System = "http://unitsofmeasure.org", Code = "Cel" };

                        if (vitalSign.Value.ToDouble() > 37.5)
                            interpretation = HighObservation();
                        else if (vitalSign.Value.ToDouble() > 36.5)
                            interpretation = LowObservation();

                        break;
                    }
                case VitalSign.VitalSignEnum.PainScale:
                    break;
                case VitalSign.VitalSignEnum.SpO2:
                    break;
                default:
                    break;
            }

            var postData = new ObservationPost();
            postData.ResourceType = "Observation";
            postData.Status = "final";
            postData.Category = new List<Category>() { new Category()
             {
                            Coding = new List<Coding>() { new Coding() {
                                System = "http://terminology.hl7.org/CodeSystem/observation-category",
                      Code= "vital-signs",
                      Display= "Vital Signs"
                               }
                    }
             }
          };

            postData.Code = new Code()
            {
                Coding = new List<Coding>(){ new Coding()
                    {
                        System = "http://loinc.org",
                        Code = vitalSignCode,
                        Display = vitalSignDisplay
                    }
             }
            };

            postData.Subject = new RefAndDisplay()
            {
                Reference = string.Format("Patient/{0}", patSs.BridgingID),
                Display = patSs.BridgingName
            };

            postData.Performer = new List<RefAndDisplay>(){ new RefAndDisplay()
            {
                Reference = string.Format("Practitioner/{0}", parMedicSs.BridgingID),
            }};

            postData.Encounter = new RefAndDisplay()
            {
                Reference = String.Format("Encounter/{0}", encounterId),
                Display = string.Format("Pemeriksaan Fisik {0} di hari {1}, {2}", patSs.BridgingName, _dayNames[vitalSignDateTime.DayOfWeek.ToInt()], vitalSignDateTime.ToString("dd MMM yyyy"))
            };

            // YYYY-MM-DDThh:mm:ss+00:00
            postData.EffectiveDateTime = string.Format("{0}+00:00", vitalSignDateTime.AddHours(_gmt).ToString(_dateFormat));
            postData.ValueQuantity = valueQuantity;

            if (vitalSignEnum == VitalSign.VitalSignEnum.BloodPressureSistolic || vitalSignEnum == VitalSign.VitalSignEnum.BloodPressureDiastolic)
            {
                postData.BodySite = new Code()
                {
                    Coding = new List<Coding>(){ new Coding()
                    {
                        System = "http://snomed.info/sct",
                        Code = "368209003",
                        Display = "Right arm"
                    }
             }
                };


            }

            if (interpretation != null)
                postData.Interpretation = interpretation;

            return postData;

        }

        private List<Interpretation> HighObservation()
        {
            return new List<Interpretation>()
                            {
                                new Interpretation()
                                {
                                     Coding = new List<Coding>() {
                                         new Coding() {System = "http://terminology.hl7.org/CodeSystem/v3-ObservationInterpretation",Code= "H",Display= "significantly high"}
                                     },
                                     Text="Di atas nilai referensi"
                                }
                            };
        }
        private List<Interpretation> LowObservation()
        {
            return new List<Interpretation>()
                            {
                                new Interpretation()
                                {
                                     Coding = new List<Coding>() { new Coding() {
                                    System = "http://terminology.hl7.org/CodeSystem/v3-ObservationInterpretation",
                          Code= "L",
                          Display= "low"
                                   }
                        },
                                     Text="Di bawah nilai referensi"
                                }
                            };
        }
        #endregion

        #region Procedure / Diagnose
        private void PostProcedure(Utils util, Registration reg, PatientBridging patSs, string encounterId, ref string accessToken)
        {
            var epProcs = new EpisodeProcedureCollection();
            epProcs.Query.Where(epProcs.Query.RegistrationNo == reg.RegistrationNo, epProcs.Query.IsVoid == false);
            epProcs.LoadAll();

            if (epProcs.Count == 0)
                return;

            foreach (var ep in epProcs)
            {
                var postData = ProcedurePostData(reg, patSs, ep, encounterId);
                if (postData != null)
                {
                    if (string.IsNullOrWhiteSpace(ep.ProcedureID)) continue;
                    var requestBody = JsonConvert.SerializeObject(postData);

                    var ssResult = new SatuSehatResult()
                    {
                        EncounterID = new Guid(encounterId),
                        Category = string.Empty,
                        Code = ep.ProcedureID
                    };
                    RestClientPostAndSaveLog(util, postData.ResourceType, requestBody, ssResult, ref accessToken);
                }
            }
        }

        private ProcedurePost ProcedurePostData(Registration reg, PatientBridging patSs, EpisodeProcedure ep, string encounterId)
        {
            var postData = new ProcedurePost();
            postData.ResourceType = "Procedure";

            postData.Status = "completed";
            postData.Category = new Category()
            {
                Coding = new List<Coding>() { new Coding() {
                                System = "http://snomed.info/sct",
                      Code= "103693007",
                      Display= "Diagnostic procedure"
                               }

                    },
                Text = "Diagnostic procedure"
            };

            postData.Code = new Code
            {
                Coding = new List<Coding>()
                        { new Coding()
                            {
                                System = "http://hl7.org/fhir/sid/icd-9-cm",
                                Code = ep.ProcedureID,
                                Display = ep.ProcedureName
                            }
                        }
            };


            postData.Subject = new RefAndDisplay()
            {
                Reference = string.Format("Patient/{0}", patSs.BridgingID),
                Display = patSs.BridgingName
            };

            postData.Encounter = new RefAndDisplay()
            {
                Reference = String.Format("Encounter/{0}", encounterId),
                Display = String.Format("Tindakan untuk patient {0} pada hari {1} tanggal {2}", patSs.BridgingName, _dayNames[ep.ProcedureDate.Value.DayOfWeek.ToInt()], ep.ProcedureDate.Value.ToString("dd MMM yyyy"))
            };

            // 2023-03-21 00:00:00	09:38 yyyy-MM-ddTHH:mm:ss ->2023-03-21T09:38:00+01:00
            postData.PerformedPeriod = new Period()
            {
                Start = string.Format("{0}T{1}:00+{2}:00", ep.ProcedureDate.Value.ToString("yyyy-MM-dd"), ep.ProcedureTime, _gmt),
                End = string.Format("{0}T{1}:00+{2}:00", ep.ProcedureDate2.Value.ToString("yyyy-MM-dd"), ep.ProcedureTime2, _gmt)
            };

            var pbQr = new ParamedicBridgingQuery("pb");
            pbQr.Where(pbQr.ParamedicID == ep.ParamedicID, pbQr.SRBridgingType == _satuSehatBridgingType);
            pbQr.es.Top = 1;
            var parSsBrid = new ParamedicBridging();
            if (parSsBrid.Load((pbQr)))
            {
                postData.Performer = new List<Performer>() { new Performer() {
                        Actor = new Actor() {
                            Reference = string.Format("Practitioner/{0}",parSsBrid.BridgingID),
                        Display= parSsBrid.BridgingName
                        }
                    }
            };
            }

            //postData.ReasonCode = new List<Code>()
            //    {
            //            new Code() {
            //            Coding = new List<Coding>()
            //                { new Coding(){
            //                System= "http://hl7.org/fhir/sid/icd-10",
            //                Code= "A15.0",
            //                Display= "Tuberculosis of lung, confirmed by sputum microscopy with or without culture"
            //                }
            //        }
            //    }
            //};
            //postData.BodySite = new List<BodySite>()
            //    { new BodySite() {
            //            Coding= new List<Coding>()
            //                { new Coding() {
            //                System= "http://snomed.info/sct",
            //                Code= "302551006",
            //                Display= "Entire Thorax"
            //                }
            //        }
            //    }
            //};

            //postData.Note = new List<Note>()
            //    { new Note() {
            //            Text = "Rontgen thorax melihat perluasan infiltrat dan kavitas."
            //    }
            //};

            return postData;
        }

        #endregion

        #region Composition / Diet
        private void PostComposition(Utils util, Registration reg, PatientBridging patSs, ParamedicBridging parMedicSs, string encounterId, ref string accessToken)
        {

            var postData = CompositionPostData(reg, patSs, parMedicSs, encounterId);
            if (postData != null)
            {
                var requestBody = JsonConvert.SerializeObject(postData);

                var ssResult = new SatuSehatResult()
                {
                    EncounterID = new Guid(encounterId),
                    Category = "Diet",
                    Code = String.Empty
                };
                RestClientPostAndSaveLog(util, postData.ResourceType, requestBody, ssResult, ref accessToken);
            }
        }

        private CompositionPost CompositionPostData(Registration reg, PatientBridging patSs, ParamedicBridging parMedicSs, string encounterId)
        {
            // Diet
            var edu = new PatientEducationLine();
            edu.Query.es.Top = 1;
            edu.Query.Where(edu.Query.RegistrationNo == reg.RegistrationNo, edu.Query.SRPatientEducation == "004"); //PatientEducation	004	Diet dan nutrisi
            if (!edu.Query.Load() || string.IsNullOrWhiteSpace(edu.EducationNotes)) return null;

            var postData = new CompositionPost();
            postData.ResourceType = "Composition";
            postData.Status = "final";

            postData.Type = new Bridging.SatuSehat.BusinessObject.Code
            {
                Coding = new List<Coding>()
                            { new Coding() {
                            System = "http://loinc.org",
                        Code= "18842-5",
                        Display= "Discharge summary"
                            }

            }
            };

            postData.Category = new List<Category>() { new Category() { Coding = new List<Coding>()
                    { new Coding(){ System= "http://loinc.org",
                            Code= "LP173421-1",
                            Display= "Report"} } } };


            postData.Subject = new RefAndDisplay()
            {
                Reference = string.Format("Patient/{0}", patSs.BridgingID),
                Display = patSs.BridgingName
            };


            postData.Encounter = new RefAndDisplay()
            {
                Reference = String.Format("Encounter/{0}", encounterId),
                Display = String.Format("Kunjungan patient {0} pada hari {1} tanggal {2}", patSs.BridgingName, _dayNames[reg.RegistrationDate.Value.DayOfWeek.ToInt()], reg.RegistrationDate.Value.ToString("dd MMM yyyy"))
            };


            //postData.Date = reg.RegistrationDate.Value.ToString("yyyy-MM-dd");

            var eduDate = edu.LastUpdateDateTime != null ? edu.LastUpdateDateTime : reg.RegistrationDate;
            postData.Date = string.Format("{0}+00:00", eduDate.Value.AddHours(_gmt).ToString(_dateFormat));


            postData.Author = new List<Author>
                { new Author(){
                        Reference= String.Format("Practitioner/{0}",parMedicSs.BridgingID),
                    Display= parMedicSs.BridgingName
                }};

            postData.Title = "Resume Medis Rawat Jalan";
            postData.Custodian = new Custodian()
            {
                Reference = String.Format("Organization/{0}", _organizationID)
            };

            postData.Section = new List<Section>{
                new Section() {
                        Code = new Code() {
                            Coding= new List<Coding>()
                                { new Coding(){
                                System= "http://loinc.org",
                                Code= "42344-2",
                                Display= "Discharge diet (narrative)"
                                }
                            }
                    }, Text = new Text(){ Status= "additional",Div= edu.EducationNotes} }
                    };

            return postData;
        }

        #endregion

        #region #5b. Kesadaran, Keluhan Utama, Edukasi, Kondisi Saat Pulang
        private void PostPatientConsciousness(Utils util, Registration reg, PatientAssessment pa, PatientBridging patSs, ParamedicBridging parSs, string encounterId, ref string accessToken)
        {

            // Kesadaran
            var postData = new
            {
                resourceType = "Observation",
                status = "final",
                category = new List<object>() {
        new
        {
            coding= new List<object>() {
                new
                {
                    system= "http://terminology.hl7.org/CodeSystem/observation-category",
                    code= "exam",
                    display= "Exam"
                }
                }
            }
                },
                code = new
                {
                    coding = new List<object>() {
                    new
                    {
                        system= "http://loinc.org",
                        code= "67775 - 7",
                        display= "Level of responsiveness"
                    }
                }
                },
                subject = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID),
                    //display = patSs.BridgingName
                },
                performer = new List<object>() {
        new
        {
            reference = string.Format("Practitioner/{0}", parSs.BridgingID)
                    //display = parSs.BridgingName
        }
    },
                encounter = new
                {
                    reference = string.Format("Encounter/{0}", encounterId),
                    display = string.Format("Pemeriksaan Kesadaran {0} di hari {1}", patSs.BridgingName, _dayNames[reg.RegistrationDate.Value.DayOfWeek.ToInt()])
                },
                effectiveDateTime = pa.AssessmentDateTime.Value.ToString("yyyy-MM-dd"), //"2022 - 07 - 14",
                issued = string.Format("{0}+00:00", pa.AssessmentDateTime.Value.AddHours(_gmt).ToString(_dateFormat)),
                valueCodeableConcept = new
                {
                    coding = new List<object>()
                    {
                        new
                        {
                            system= "http://snomed.info/sct",
                            code= "248234008",
                            display= "Mentally alert"
                        }
                    }
                }
            };
            var ssResult = new SatuSehatResult()
            {
                EncounterID = new Guid(encounterId),
                Category = "Consciousness",
                Code = ""
            };
            var requestBody = JsonConvert.SerializeObject(postData);
            RestClientPostAndSaveLog(util, "Observation", requestBody, ssResult, ref accessToken);
        }


        private void PostPatientCondition(Utils util, Registration reg, PatientBridging patSs, string encounterId, ref string accessToken)
        {
            var postData = new
            {
                resourceType = "Condition",
                clinicalStatus = new
                {
                    coding = new List<object>() { new
                    {
                        system= "http://terminology.hl7.org/CodeSystem/condition-clinical",
                        code= "active",
                        display= "Active"
                    }
                }
                },
                category = new List<object>() {
                new {
                    coding= new List<object>() { new
                        {
                            system= "http://terminology.hl7.org/CodeSystem/condition-category",
                            code= "problem-list-item",
                            display= "Problem List Item"
                        }
                    }
                }
                },
                code = new
                {
                    coding = new List<object>() { new
                        {
                            system= "http://snomed.info/sct",
                            code= "359746009",
                            display= "Patient's condition stable"
                        }
                    }
                },
                subject = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID),
                    display = patSs.BridgingName
                },
                encounter = new
                {
                    reference = string.Format("Encounter/{0}", encounterId),
                    display = string.Format("Kunjungan {0} di hari {1}", patSs.BridgingName, _dayNames[reg.RegistrationDate.Value.DayOfWeek.ToInt()])

                }
            };

            var ssResult = new SatuSehatResult()
            {
                EncounterID = new Guid(encounterId),
                Category = "Consciousness",
                Code = ""
            };

            var requestBody = JsonConvert.SerializeObject(postData);
            RestClientPostAndSaveLog(util, "Condition", requestBody, ssResult, ref accessToken);
        }

        private void PostPatientChiefComplaint(Utils util, Registration reg, PatientAssessment pa, PatientBridging patSs, string encounterId, ref string accessToken)
        {
            if (string.IsNullOrWhiteSpace(pa.SCTChiefComplaint)) return;

            var snomedct = new Snomedct();
            if (!snomedct.LoadByPrimaryKey("ChiefComplaint", pa.SCTChiefComplaint)) return;

            var postData = new
            {
                resourceType = "Condition",
                clinicalStatus = new
                {
                    coding = new List<object>() { new
                    {
                        system= "http://terminology.hl7.org/CodeSystem/condition-clinical",
                        code= "active",
                        display= "Active"
                    }
                }
                },
                category = new List<object>() { new
                {
                    coding= new List<object>() { new
                            {
                                system= "http://terminology.hl7.org/CodeSystem/condition-category",
                                code= "problem - list - item",
                                display= "Problem List Item"
                            }
                        }
                    }
                },
                code = new
                {
                    coding = new List<object>() { new
                    {
                    system= "http://snomed.info/sct",
                        code= pa.SCTChiefComplaint,
                                display= snomedct.Display
                            }
                       }
                },
                onsetString = pa.Hpi, // "Ditemukan sejak 1 bulan yang lalu saat musim kemarau",
                recordedDate = string.Format("{0}+00:00", pa.AssessmentDateTime.Value.AddHours(_gmt).ToString(_dateFormat)), //"2022-06-14T08:45:00 + 07:00",
                subject = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID),
                    display = patSs.BridgingName
                },
                encounter = new
                {
                    reference = string.Format("Encounter/{0}", encounterId),
                    display = string.Format("Kunjungan {0} di hari {1}", patSs.BridgingName, _dayNames[reg.RegistrationDate.Value.DayOfWeek.ToInt()])
                }
            };

            var ssResult = new SatuSehatResult()
            {
                EncounterID = new Guid(encounterId),
                Category = "ChiefComplaint",
                Code = ""
            };

            var requestBody = JsonConvert.SerializeObject(postData);
            RestClientPostAndSaveLog(util, "Condition", requestBody, ssResult, ref accessToken);
        }

        private void PostPatientEducationMedication(Utils util, Registration reg, PatientBridging patSs, ParamedicBridging parSs, string encounterId, ref string accessToken)
        {
            var edu = new PatientEducation();
            edu.Query.Where(edu.Query.RegistrationNo == reg.RegistrationNo, edu.Query.EducationType == "RSP");
            edu.Query.es.Top = 1;
            if (!edu.Query.Load()) return;

            // reasonCodes
            var ssres = new SatuSehatResultQuery("r");
            ssres.Where(ssres.EncounterID == new Guid(encounterId), ssres.ResourceType == "Condition", ssres.Category == "Diagnosis");
            ssres.Select(ssres.IndexNo, ssres.ResultID, ssres.Code, ssres.PostData);
            var dtbDiag = ssres.LoadDataTable();

            var reasonCodes = new List<object>();
            foreach (DataRow row in dtbDiag.Rows)
            {
                var jsonDiag = JsonConvert.DeserializeObject<ConditionResponse>(row["PostData"].ToString());
                var diag = new
                {
                    coding = new List<object>() {
                                new {
                                system= "http://hl7.org/fhir/sid/icd-10",
                                code= jsonDiag.Code.Coding[0].Code,
                                display= jsonDiag.Code.Coding[0].Display
                                }
                            }
                };

                reasonCodes.Add(diag);
            }

            var postData = new
            {
                resourceType = "Procedure",
                status = "completed",
                category = new
                {
                    coding = new List<object>() {
            new
            {
                system= "http://snomed.info/sct",
                code= "409073007",
                display= "Education"
            }
    },
                    text = "Education"
                },
                code = new
                {
                    coding = new List<object>() {
            new
            {
                system= "http://snomed.info/sct",
                code= "967006",
                display= "Medication education"
            }
        }
                },
                subject = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID),
                    display = patSs.BridgingName
                },
                encounter = new
                {
                    reference = string.Format("Encounter/{0}", encounterId),
                    display = string.Format("Edukasi minum obat OAT rutin kepada {0} di hari {1}", patSs.BridgingName, _dayNames[reg.RegistrationDate.Value.DayOfWeek.ToInt()])
                },
                performedPeriod = new
                {
                    start = string.Format("{0}+00:00", edu.EducationDateTime.Value.AddHours(_gmt).ToString(_dateFormat)),
                    end = string.Format("{0}+00:00", edu.EducationDateTime.Value.AddMinutes(edu.Duration ?? 2).AddHours(_gmt).ToString(_dateFormat)),
                },
                performer = new List<object>() {
        new
        {
            actor= new {
                    reference = string.Format("Practitioner/{0}", parSs.BridgingID),
                    display = parSs.BridgingName
        }
        }
    },
                reasonCode = reasonCodes,
                note = new List<object>() {
        new
        {
            text= "Edukasi minum OAT teratur."
        }
    }
            };

            var ssResult = new SatuSehatResult()
            {
                EncounterID = new Guid(encounterId),
                Category = "ChiefComplaint",
                Code = ""
            };

            var requestBody = JsonConvert.SerializeObject(postData);
            RestClientPostAndSaveLog(util, "Condition", requestBody, ssResult, ref accessToken);
        }

        //private void PostKondisiPulang(Utils util, Registration reg, PatientBridging patSs, string encounterId, ref string accessToken)
        //{
        //    var postData = new
        //    {
        //        resourceType = "Condition",
        //        clinicalStatus = new
        //        {
        //            coding = new List<object>() { new
        //            {
        //                system= "http://terminology.hl7.org/CodeSystem/condition-clinical",
        //                code= "active",
        //                display= "Active"
        //            }
        //        }
        //        },
        //        category = new List<object>() { new
        //        {
        //        coding= new List<object>() { new
        //                    {
        //            system= "http://terminology.hl7.org/CodeSystem/condition-category",
        //            code= "problem - list - item",
        //                        display= "Problem List Item"
        //                    }
        //                }
        //            }
        //        },
        //        code = new
        //        {
        //            coding = new List<object>() { new
        //            {
        //            system= "http://snomed.info/sct",
        //                code= "49727002",
        //                        display= "Cough"
        //                    }
        //                }
        //        },
        //        onsetString = "Ditemukan sejak 1 bulan yang lalu saat musim kemarau",
        //        recordedDate = "2022-06-14T08:45:00 + 07:00",
        //        subject = new
        //        {
        //            reference = string.Format("Patient/{0}", patSs.BridgingID),
        //            display = patSs.BridgingName
        //        },
        //        encounter = new
        //        {
        //            reference = string.Format("Encounter/{0}", encounterId),
        //            display = string.Format("Kunjungan {0} di hari {1}", patSs.BridgingName, _dayNames[reg.RegistrationDate.Value.DayOfWeek.ToInt()])

        //        }
        //    };

        //    var ssResult = new SatuSehatResult()
        //    {
        //        EncounterID = new Guid(encounterId),
        //        Category = "ChiefComplaint",
        //        Code = ""
        //    };

        //    var requestBody = JsonConvert.SerializeObject(postData);
        //    RestClientPostAndSaveLog(util, "Condition", requestBody, ssResult, ref accessToken);
        //}
        #endregion #5b. Kesadaran, Keluhan Utama, Edukasi, Kondisi Saat Pulang

        #region Medication
        private void PostMedication(Registration reg, PatientBridging patSs, ParamedicBridging parSs, Utils util, string encounterId, ref string accessToken)
        {
            var tpiq = new TransPrescriptionItemQuery("tpi");
            var tpq = new TransPrescriptionQuery("tp");
            tpiq.InnerJoin(tpq).On(tpiq.PrescriptionNo == tpq.PrescriptionNo);
            tpiq.Where(tpq.RegistrationNo == reg.RegistrationNo, tpq.IsApproval == true, tpq.IsVoid == false, tpiq.IsVoid == false);

            tpiq.Select(tpiq.ItemID, tpiq.ItemInterventionID, tpiq.ParentNo, tpiq.SequenceNo, tpiq.IsCompound, tpq.PrescriptionNo, tpq.PrescriptionDate, tpq.InProgressDateTime, tpq.DeliverDateTime, tpiq.SequenceNo, tpq.ServiceUnitID);

            var dtbTpi = tpiq.LoadDataTable();

            //Medication Create
            foreach (DataRow row in dtbTpi.Rows)
            {
                var itemID = row["ItemInterventionID"] != DBNull.Value && !string.IsNullOrEmpty(row["ItemInterventionID"].ToString()) ? row["ItemInterventionID"].ToString() : row["ItemID"].ToString();

                if (false.Equals(row["IsCompound"]))
                {
                    var ssItem = new ItemBridging();
                    ssItem.Query.Where(ssItem.Query.ItemID == itemID, ssItem.Query.SRBridgingType == _satuSehatBridgingType);
                    ssItem.Query.es.Top = 1;
                    if (!ssItem.Query.Load()) continue;

                    var kfaItem = new SatuSehatKfa();
                    kfaItem.Query.Where(kfaItem.Query.SsUuid == ssItem.BridgingID);
                    kfaItem.Query.es.Top = 1;
                    if (!kfaItem.Query.Load()) continue;

                    var kfaInfo = JsonConvert.DeserializeObject<Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.Kfa.Root>(kfaItem.SsResult);

                    //ZatActive
                    var ingredientZas = new List<object>();
                    foreach (var za in kfaInfo.Data.ActiveIngredients)
                    {
                        // ex. kekuatan_zat_aktif	: 5 mg/1 g
                        var zaInfos = new string[2];
                        var numerators = new string[2];
                        var denominators = new string[2];
                        if (za.KekuatanZatAktif.Contains("/"))
                        {
                            zaInfos = za.KekuatanZatAktif.Split('/');
                            numerators = zaInfos[0].Split(' ');
                            //denominators = zaInfos[1].Split(' '); // satuan g tidak dikenal

                        }
                        else
                        {
                            // ex. kekuatan_zat_aktif	:	100 mg
                            numerators = za.KekuatanZatAktif.Split(' ');
                            denominators[0] = "1";
                            denominators[1] = "TAB";
                        }

                        var ingredientZa =
                                new
                                {
                                    itemCodeableConcept = new
                                    {
                                        coding = new List<object>() {
                                   new
                                   {
                                       system= "http://sys-ids.kemkes.go.id/kfa",
                                       code= za.KfaCode,
                                       display= za.ZatAktif
                                   }
                                        }
                                    },
                                    isActive = za.Active,
                                    strength = new
                                    {
                                        numerator = new
                                        {
                                            value = numerators[0].ToInt(),
                                            system = "http://unitsofmeasure.org",
                                            code = numerators[1]
                                        },
                                        denominator = new
                                        {
                                            value = denominators[0].ToInt(),
                                            system = "http://terminology.hl7.org/CodeSystem/v3-orderableDrugForm",
                                            code = denominators[1]
                                        }
                                    }
                                };
                        ingredientZas.Add(ingredientZa);
                    }

                    // 1. Medication for Request
                    var postData = MedicationPraRequestNonCompoundPostData(reg, row["PrescriptionNo"].ToString(), row["SequenceNo"].ToString(), kfaInfo, ssItem, ingredientZas, encounterId);
                    if (postData != null)
                    {
                        var requestBody = JsonConvert.SerializeObject(postData);

                        var ssResult = new SatuSehatResult()
                        {
                            EncounterID = new Guid(encounterId),
                            Category = string.Format("REQ-{0}", row["PrescriptionNo"]),
                            Code = row["SequenceNo"].ToString()
                        };
                        var medRespon = RestClientPostAndSaveLog(util, "Medication", requestBody, ssResult, ref accessToken);

                        if (medRespon != null && !string.IsNullOrEmpty(medRespon.Id))
                        {
                            //2. Medication Request
                            var tpi = new TransPrescriptionItem();
                            tpi.LoadByPrimaryKey(row["PrescriptionNo"].ToString(), row["SequenceNo"].ToString());
                            var postRequestData = MedicationRequestNonCompoundPostData(reg, patSs, parSs, row["PrescriptionNo"].ToString(), Convert.ToDateTime(row["PrescriptionDate"]), ssItem, tpi, medRespon.Id, encounterId);
                            if (postRequestData != null)
                            {
                                requestBody = JsonConvert.SerializeObject(postRequestData);

                                ssResult = new SatuSehatResult()
                                {
                                    EncounterID = new Guid(encounterId),
                                    Category = string.Format("REQ-{0}", row["PrescriptionNo"]),
                                    Code = row["SequenceNo"].ToString()
                                };
                                var medReqRes = RestClientPostAndSaveLog(util, "MedicationRequest", requestBody, ssResult, ref accessToken);

                                if (medReqRes != null && !string.IsNullOrEmpty(medReqRes.Id))
                                {
                                    // 3. Medication for Dispense
                                    postRequestData = MedicationPraDispenseNonCompoundPostData(reg, row["PrescriptionNo"].ToString(), row["SequenceNo"].ToString(), kfaInfo, ssItem, ingredientZas, encounterId);
                                    if (postRequestData != null)
                                    {
                                        requestBody = JsonConvert.SerializeObject(postRequestData);

                                        ssResult = new SatuSehatResult()
                                        {
                                            EncounterID = new Guid(encounterId),
                                            Category = string.Format("DISP-{0}", row["PrescriptionNo"]),
                                            Code = row["SequenceNo"].ToString()
                                        };

                                        var medForDispRes = RestClientPostAndSaveLog(util, "Medication", requestBody, ssResult, ref accessToken);

                                        if (medForDispRes != null && !string.IsNullOrEmpty(medForDispRes.Id))
                                        {
                                            //4. Medication Dispense
                                            if (row["InProgressDateTime"] != DBNull.Value && row["DeliverDateTime"] != DBNull.Value)
                                            {
                                                var postDispenseData = MedicationDispenseNonCompoundPostData(reg, patSs, parSs, row["PrescriptionNo"].ToString(), row["ServiceUnitID"].ToString(), Convert.ToDateTime(row["PrescriptionDate"]), Convert.ToDateTime(row["InProgressDateTime"]), Convert.ToDateTime(row["DeliverDateTime"]), tpi, medForDispRes.Id, medReqRes.Id, ssItem, encounterId);
                                                if (postDispenseData != null)
                                                {
                                                    requestBody = JsonConvert.SerializeObject(postDispenseData);

                                                    ssResult = new SatuSehatResult()
                                                    {
                                                        EncounterID = new Guid(encounterId),
                                                        Category = string.Format("DISP-{0}", row["PrescriptionNo"]),
                                                        Code = row["SequenceNo"].ToString()
                                                    };
                                                    var medDispRes = RestClientPostAndSaveLog(util, "MedicationDispense", requestBody, ssResult, ref accessToken);
                                                }
                                            }
                                            else
                                            {
                                                ssResult = new SatuSehatResult()
                                                {
                                                    EncounterID = new Guid(encounterId),
                                                    Category = string.Format("DISP-{0}", row["PrescriptionNo"]),
                                                    Code = row["SequenceNo"].ToString(),
                                                    ResourceType = "MedicationDispense",
                                                    ErrorResponse = "Deliver status still empty"
                                                };
                                                SetResultIndexNo(ssResult);
                                                ssResult.Save();
                                            }
                                        }
                                    }

                                }
                            }
                        }
                    }
                }
            }

        }

        private object MedicationPraRequestNonCompoundPostData(Registration reg, string prescNo, string seqNo, Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.Kfa.Root kfaInfo, ItemBridging ssItem, List<object> ingredientZas, string encounterId)
        {
            // Dokumentasi: https://satusehat.kemkes.go.id/platform/docs/id/fhir/resources/medication

            var postData = new
            {
                resourceType = "Medication",
                meta = new
                {
                    profile = new List<string>() { "https://fhir.kemkes.go.id/r4/StructureDefinition/Medication" }
                },
                identifier = new List<object>() {
                   new {
                       system= string.Format("http://sys-ids.kemkes.go.id/medication/{0}",_organizationID),
                       use= "official",
                       value= string.Format("{0}-{1}",prescNo, seqNo)
                   }
                },
                code = new
                {
                    coding = new List<object>() {
                           new
                           {
                               system= "http://sys-ids.kemkes.go.id/kfa",
                               code= ssItem.BridgingID,
                               display= ssItem.BridgingName
                           }
                        }
                },
                status = "active",
                manufacturer = new
                {
                    reference = string.Format("Organization/{0}", _organizationID)
                },
                form = new
                {
                    coding = new List<object>() {
               new
               {
                   system= "http://terminology.kemkes.go.id/CodeSystem/medication-form",
                   code= kfaInfo.Data.DosageForm.Code,
                   display= kfaInfo.Data.DosageForm.Name
               }
           }
                },
                ingredient = ingredientZas,
                extension = new List<object>() {
           new
           {
               url= "https://fhir.kemkes.go.id/r4/StructureDefinition/MedicationType",
               valueCodeableConcept= new {
                   coding= new List<object>() {
                       new
                       {
                           system = "http://terminology.kemkes.go.id/CodeSystem/medication-type",
                           code= "NC",
                           display= "Non - compound"
                       }
           }
               }
           }
       }
            };


            return postData;
        }

        private object MedicationRequestNonCompoundPostData(Registration reg, PatientBridging patSs, ParamedicBridging parSs, string prescNo, DateTime prescDate, ItemBridging ssItem, TransPrescriptionItem tpi, string medicationReference, string encounterId)
        {
            // reasonCodes
            var ssres = new SatuSehatResultQuery("r");
            ssres.Where(ssres.EncounterID == new Guid(encounterId), ssres.ResourceType == "Condition", ssres.Category == "Diagnosis");
            ssres.Select(ssres.IndexNo, ssres.ResultID, ssres.Code, ssres.PostData);
            var dtbDiag = ssres.LoadDataTable();

            var reasonCodes = new List<object>();
            foreach (DataRow row in dtbDiag.Rows)
            {
                var jsonDiag = JsonConvert.DeserializeObject<ConditionResponse>(row["PostData"].ToString());
                var diag = new
                {
                    coding = new List<object>() {
                                new {
                                system= "http://hl7.org/fhir/sid/icd-10",
                                code= jsonDiag.Code.Coding[0].Code,
                                display= jsonDiag.Code.Coding[0].Display
                                }
                            }
                };

                reasonCodes.Add(diag);
            }
            // timing
            // TODO: Berapa hari konsumsi obat
            var cm = new ConsumeMethod();
            cm.LoadByPrimaryKey(tpi.SRConsumeMethod);

            var postData = new
            {
                resourceType = "MedicationRequest",
                identifier = new List<object>() {
                    new {
                        system = string.Format("http://sys-ids.kemkes.go.id/prescription/{0}", _organizationID),
                        use = "official",
                        value = prescNo
                    },
                    new
                    {
                        system = string.Format("http://sys-ids.kemkes.go.id/prescription-item/{0}", _organizationID),
                        use = "official",
                        value = string.Format("{0}-{1}", prescNo, tpi.SequenceNo)//"123456788-1"
                    }
                },
                status = "completed",
                intent = "order",
                category = new List<object>() {
                    new {
                        coding = new List<object>() {
                            new {
                                system = "http://terminology.hl7.org/CodeSystem/medicationrequest-category",
                                code = "outpatient",
                                display = "Outpatient"
                            }
                        }
                    }
                },
                priority = "routine",
                medicationReference = new
                {
                    reference = string.Format("Medication/{0}", medicationReference),
                    display = ssItem.BridgingName
                },
                subject = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID),
                    display = patSs.BridgingName
                },
                encounter = new
                {
                    reference = string.Format("Encounter/{0}", encounterId)
                },
                authoredOn = string.Format("{0}+00:00", prescDate.AddHours(_gmt).ToString(_dateFormat)),
                requester = new
                {
                    reference = string.Format("Practitioner/{0}", parSs.BridgingID),
                    display = parSs.BridgingName
                },
                reasonCode = reasonCodes,
                courseOfTherapyType = new
                {
                    coding = new List<object>() {
                        new {
                            system = "http://terminology.hl7.org/CodeSystem/medicationrequest-course-of-therapy",
                            code = "continuous",
                            display = "Continuing long term therapy"
                        }
                    }
                },
                dosageInstruction = new List<object>() {
                    new {
                        sequence = 1,
                        text = tpi.DosageQty, // "4 tablet per hari",
                        additionalInstruction = new List<object>() {
                            new {
                                text = tpi.Notes //"Diminum setiap hari"
                            }
                        },
                        patientInstruction = tpi.Notes, // "4 tablet perhari, diminum setiap hari tanpa jeda sampai prose pengobatan berakhir",
                        timing = new
                        {
                            repeat = new
                            {
                                frequency = cm.IterationQty,
                                period = 1,
                                periodUnit = "d"
                            }
                        },
                        route = new {
                            coding = new List<object> {
                                new {
                                    system = "http://www.whocc.no/atc",
                                    code = "O",
                                    display = "Oral"
                                }
                            }
                        },
                        doseAndRate = new List<object> {
                            new {
                                type = new {
                                    coding = new List<object> {
                                        new {
                                            system = "http://terminology.hl7.org/CodeSystem/dose-rate-type",
                                            code = "ordered",
                                            display = "Ordered"
                                        }
                                    }
                                },
                                doseQuantity = new {
                                    value = Convert.ToDecimal(new Fraction(tpi.DosageQty)) , // 4,
                                    unit = tpi.SRDosageUnit, //"TAB",
                                    system = "http://terminology.hl7.org/CodeSystem/v3-orderableDrugForm",
                                    code = AppStandardReferenceItemBridging.GetBridgingID("DosageUnit", tpi.SRDosageUnit,_satuSehatBridgingType)
                                }
                            }
                        }
                    }
                },
                dispenseRequest = new
                {
                    dispenseInterval = new
                    {
                        value = 1,
                        unit = "days",
                        system = "http://unitsofmeasure.org",
                        code = "d"
                    },
                    validityPeriod = new
                    {
                        start = string.Format("{0}+00:00", prescDate.AddHours(_gmt).ToString(_dateFormat)),
                        end = string.Format("{0}+00:00", prescDate.AddDays(30).AddHours(_gmt).ToString(_dateFormat)),
                    },
                    numberOfRepeatsAllowed = 0,
                    quantity = new
                    {
                        value = tpi.TakenQty, //120,
                        unit = tpi.SRItemUnit, // "TAB",
                        system = "http://terminology.hl7.org/CodeSystem/v3-orderableDrugForm",
                        code = AppStandardReferenceItemBridging.GetBridgingID("ItemUnit", tpi.SRItemUnit, _satuSehatBridgingType)
                    },
                    expectedSupplyDuration = new
                    {
                        value = 30,
                        unit = "days",
                        system = "http://unitsofmeasure.org",
                        code = "d"
                    },
                    performer = new
                    {
                        reference = string.Format("Organization/{0}", _organizationID)
                    }
                }
            };



            return postData;
        }

        #endregion

        #region Medication Dispense

        private object MedicationPraDispenseNonCompoundPostData(Registration reg, string prescNo, string seqNo, Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.Kfa.Root kfaInfo, ItemBridging ssItem, List<object> ingredientZas, string encounterId)
        {
            // Dokumentasi: https://satusehat.kemkes.go.id/platform/docs/id/fhir/resources/medication
            // LotNumber / Batch Number
            var im = new ItemMovement();
            im.Query.Where(im.Query.TransactionNo == prescNo, im.Query.SequenceNo == seqNo, im.Query.TransactionCode == "091");
            im.Query.es.Top = 1;
            if (!im.Query.Load()) return null;

            var postData = new
            {
                resourceType = "Medication",
                meta = new
                {
                    profile = new List<string>() { "https://fhir.kemkes.go.id/r4/StructureDefinition/Medication" }
                },
                identifier = new List<object>() {
                   new {
                       system= string.Format("http://sys-ids.kemkes.go.id/medication/{0}",_organizationID),
                       use= "official",
                       value= string.Format("{0}-{1}",prescNo, seqNo)
                   }
                },
                code = new
                {
                    coding = new List<object>() {
                           new
                           {
                               system= "http://sys-ids.kemkes.go.id/kfa",
                               code= ssItem.BridgingID,
                               display= ssItem.BridgingName
                           }
                        }
                },
                status = "active",
                manufacturer = new
                {
                    reference = string.Format("Organization/{0}", _organizationID)
                },
                form = new
                {
                    coding = new List<object>() {
                       new
                       {
                           system= "http://terminology.kemkes.go.id/CodeSystem/medication-form",
                           code= kfaInfo.Data.DosageForm.Code,
                           display= kfaInfo.Data.DosageForm.Name
                       }
                    }
                },
                ingredient = ingredientZas,
                batch = new
                {
                    lotNumber = im.BatchNumber, //"1625042A",
                    expirationDate = im.ExpiredDate.Value.ToString("yyyy-MM-dd"), //"2025-07-28"
                },
                extension = new List<object>() {
                   new
                   {
                       url= "https://fhir.kemkes.go.id/r4/StructureDefinition/MedicationType",
                       valueCodeableConcept= new {
                           coding= new List<object>() {
                               new
                               {
                                   system = "http://terminology.kemkes.go.id/CodeSystem/medication-type",
                                   code= "NC",
                                   display= "Non - compound"
                               }
                   }
                       }
                   }
               }
            };


            return postData;
        }

        private object MedicationDispenseNonCompoundPostData(Registration reg, PatientBridging patSs, ParamedicBridging parSs, string prescriptionNo, string serviceUnitID, DateTime prescriptionDate, DateTime inProgressDateTime, DateTime deliverDateTime, TransPrescriptionItem tpi, string medicationPraDispenseID, string medicationRequestRef, ItemBridging ssItem, string encounterId)
        {
            //// MedicationRequest EncounterID
            //var medReq = new SatuSehatResult();
            //medReq.Query.Where(medReq.Query.ResourceType == "MedicationRequest", medReq.Query.EncounterID == encounterId, medReq.Query.Category == prescNo, medReq.Query.Code == tpi.SequenceNo);
            //medReq.Query.es.Top = 1;
            //if (!medReq.Query.Load()) return null;

            //var ssItem = new ItemBridging();
            //ssItem.Query.Where(ssItem.Query.ItemID == itemID, ssItem.Query.SRBridgingType == _satuSehatBridgingType);
            //ssItem.Query.es.Top = 1;
            //if (!ssItem.Query.Load()) return null;

            var ssSu = new ServiceUnitBridging();
            ssSu.Query.Where(ssSu.Query.SRBridgingType == _satuSehatBridgingType, ssSu.Query.ServiceUnitID == serviceUnitID);
            if (!ssSu.Query.Load()) return null;

            // reasonCodes
            var ssres = new SatuSehatResultQuery("r");
            ssres.Where(ssres.EncounterID == new Guid(encounterId), ssres.ResourceType == "Condition", ssres.Category == "Diagnosis");
            ssres.Select(ssres.IndexNo, ssres.ResultID, ssres.Code, ssres.PostData);
            var dtbDiag = ssres.LoadDataTable();

            var reasonCodes = new List<object>();
            foreach (DataRow row in dtbDiag.Rows)
            {
                var jsonDiag = JsonConvert.DeserializeObject<ConditionResponse>(row["PostData"].ToString());
                var diag = new
                {
                    coding = new List<object>() {
                                new {
                                system= "http://hl7.org/fhir/sid/icd-10",
                                code= jsonDiag.Code.Coding[0].Code,
                                display= jsonDiag.Code.Coding[0].Display
                                }
                            }
                };

                reasonCodes.Add(diag);
            }
            // timing
            // TODO: Berapa hari konsumsi obat
            // TODO: performer ganti dengan petugas apotek

            var cm = new ConsumeMethod();
            cm.LoadByPrimaryKey(tpi.SRConsumeMethod);

            var postData = new
            {
                resourceType = "MedicationDispense",
                identifier = new List<object>() {
                    new {
                        system = string.Format("http://sys-ids.kemkes.go.id/prescription/{0}", _organizationID),
                        use = "official",
                        value = prescriptionNo
                    },
                    new
                    {
                        system = string.Format("http://sys-ids.kemkes.go.id/prescription-item/{0}", _organizationID),
                        use = "official",
                        value = string.Format("{0}-{1}", prescriptionNo, tpi.SequenceNo)//"123456788-1"
                    }
                },
                status = "completed",
                category = new
                {
                    coding = new List<object>() {
                       new
                       {
                           system = "http://terminology.hl7.org/fhir/CodeSystem/medicationdispense-category",
                           code= "outpatient",
                           display= "Outpatient"
                       }
                   }
                },
                medicationReference = new
                {
                    reference = string.Format("Medication/{0}", medicationPraDispenseID),
                    display = ssItem.BridgingName //"Obat Anti Tuberculosis / Rifampicin 150 mg / Isoniazid 75 mg / Pyrazinamide 400 mg / Ethambutol 275 mg Kaplet Salut Selaput(KIMIA FARMA)"
                },
                subject = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID),
                    display = patSs.BridgingName
                },
                context = new
                {
                    reference = string.Format("Encounter/{0}", encounterId)
                },
                performer = new List<object>() {
                   new
                   {
                       actor= new {
                           reference = string.Format( "Practitioner/{0}",parSs.BridgingID),
                           display= parSs.BridgingName
                        }
                   }
                },
                location = new
                {
                    reference = string.Format("Location/{0}", ssSu.BridgingID),
                    display = ssSu.BridgingName
                },
                authorizingPrescription = new List<object>() {
                   new
                   {
                       reference = string.Format( "MedicationRequest/{0}", medicationRequestRef)
                   }
                },
                quantity = new
                {
                    system = "http://terminology.hl7.org/CodeSystem/v3-orderableDrugForm",
                    code = AppStandardReferenceItemBridging.GetBridgingID("ItemUnit", tpi.SRItemUnit, _satuSehatBridgingType),
                    value = tpi.TakenQty
                },

                daysSupply = new
                {
                    value = 30,
                    unit = "Day",
                    system = "http://unitsofmeasure.org",
                    code = "d"
                },
                whenPrepared = string.Format("{0}+00:00", inProgressDateTime.AddHours(_gmt).ToString(_dateFormat)), //"2022-01-15T10:20:00Z",
                whenHandedOver = string.Format("{0}+00:00", deliverDateTime.AddHours(_gmt).ToString(_dateFormat)), //"2022-01-15T16:20:00Z",
                dosageInstruction = new List<object>() {
                   new
                   {
                       sequence= 1,
                       text= tpi.Notes, //"Diminum 4 tablet sekali dalam sehari",
                       timing= new {
                           repeat= new {
                               frequency= cm.IterationQty,
                               period= 1,
                               periodUnit= "d"
                   }
                },
                doseAndRate= new List<object>() {
               new
               {
                   type= new {
                       coding= new List<object>() {
                           new
                           {
                               system = "http://terminology.hl7.org/CodeSystem/dose-rate-type",
                               code= "ordered",
                               display= "Ordered"
                           }
               }
                   },
                   doseQuantity= new {
                       value = Convert.ToDecimal(new Fraction(tpi.DosageQty)), // 4,
                       unit= tpi.SRDosageUnit, //"TAB",
                       system= "http://terminology.hl7.org/CodeSystem/v3-orderableDrugForm",
                       code= AppStandardReferenceItemBridging.GetBridgingID("DosageUnit", tpi.SRDosageUnit,_satuSehatBridgingType)
                   }
               }
           }
       }
   }
            };

            return postData;
        }

        #endregion  Medication Dispense

        #region Lab
        private void PostServiceRequest(Utils util, Registration reg, PatientBridging patSs, ParamedicBridging parSs, TransCharges tc, TransChargesItem tci, LoincItem loincItem, string encounterId, ref string accessToken)
        {
            var postData = new
            {
                resourceType = "ServiceRequest",
                identifier = new List<object>() {
                    new {
                        system= "http://sys-ids.kemkes.go.id/servicerequest/10000004",
                        value= "00001"
                    }
                },
                status = "active",
                intent = "original-order",
                priority = "routine",
                category = new List<object>() {
                    new {
                        coding= new List<object>() {
                            new {
                                system= "http://snomed.info/sct",
                                code= "108252007",
                                display= "Laboratory procedure"
                            }
                        }
                    }
                },
                code = new
                {
                    coding = new List<object>() { new
                        {
                        system= "http://loinc.org",
                        code= loincItem.Code, // "11477 - 7",
                        display= loincItem.Display // "Microscopic observation[Identifier} in Sputum by Acid fast stain"
                        }
                    },
                    text = tc.Notes// "Pemeriksaan Sputum BTA"
                },
                subject = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID),
                },
                encounter = new
                {
                    reference = string.Format("Encounter/{0}", encounterId),
                    display = string.Format("Permintaan {0} {1} di hari {2} pukul {3}", "BTA Sputum", patSs.BridgingName, _dayNames[reg.RegistrationDate.Value.DayOfWeek.ToInt()], "09:30 WIB")
                },
                occurrenceDateTime = string.Format("{0}+00:00", tc.ApprovedDateTime.Value.AddHours(_gmt).ToString(_dateFormat)), // "2022-06-14T09:30:27+07:00",
                authoredOn = string.Format("{0}+00:00", tc.ApprovedDateTime.Value.AddHours(_gmt).ToString(_dateFormat)), //"2022-06-13T12:30:27+07:00",
                requester = new
                {
                    reference = string.Format("Practitioner/{0}", parSs.BridgingID),
                    display = parSs.BridgingName
                },
                performer = new List<object>() {
                    new {
                        reference= "Practitioner/N10000005",
                        display= "Fatma"
                    }
                },
                reasonCode = new List<object>() {
                    new {
                        text= "Periksa jika ada kemungkinan Tuberculosis"
                    }
                }
            };

            var ssResult = new SatuSehatResult()
            {
                EncounterID = new Guid(encounterId),
                Category = "ServiceRequest",
                Code = ""
            };

            var requestBody = JsonConvert.SerializeObject(postData);
            RestClientPostAndSaveLog(util, "ServiceRequest", requestBody, ssResult, ref accessToken);

        }
        #endregion Lab

        #region CarePlan
        private void PostCarePlanRawatPasien(Utils util, Registration reg, PatientBridging patSs, ParamedicBridging parSs, PatientAssessment pa, string encounterId, ref string accessToken)
        {
            if (pa.FollowUpPlanType != "IP") { return; }

            var postData = new
            {
                resourceType = "CarePlan",
                status = "active",
                intent = "plan",
                title = "Rencana Rawat Pasien",
                description = "Rencana Rawat Pasien",
                category = new List<object>()
                { new
                { coding = new List<object>()
                    { new {
                        system = "http://snomed.info/sct",
                        code = "736271009",
                        display = "Outpatient care plan"
                    } }
                }},
                subject = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID),
                    display = patSs.BridgingName
                },
                encounter = new
                {
                    reference = string.Format("Encounter/{0}", encounterId)
                },
                created = string.Format("{0}+00:00", pa.AssessmentDateTime.Value.AddHours(_gmt).ToString(_dateFormat)), //"2023-08-31T01:20:00+00:00",
                author = new
                {
                    reference = string.Format("Practitioner/{0}", parSs.BridgingID),
                    display = parSs.BridgingName
                }

            };

            var ssResult = new SatuSehatResult()
            {
                EncounterID = new Guid(encounterId),
                Category = "CarePlan",
                Code = ""
            };

            var requestBody = JsonConvert.SerializeObject(postData);
            RestClientPostAndSaveLog(util, "CarePlan", requestBody, ssResult, ref accessToken);
        }

        #endregion CarePlan

        #endregion

        #endregion

        protected void grdRegisteredList_ItemCommand(object sender, GridCommandEventArgs e)
        {
            return; // Masih terkendala dgn method yg memproses beberapa record
            if (e.CommandName == "Resend")
            {
                var accessToken = string.Empty;
                var util = new Bridging.SatuSehat.Utils();


                var args = e.CommandArgument.ToString().Split('|');
                var regNo = args[0];
                var encounterID = args[1];

                var reg = new Registration();
                reg.LoadByPrimaryKey(regNo);

                var pat = new Patient();
                pat.LoadByPrimaryKey(reg.PatientID);

                var patSs = new PatientBridging();
                patSs.LoadByPrimaryKey(reg.PatientID, _satuSehatBridgingType);

                switch (args[2].ToLower())
                {
                    case "Procedure":
                        {
                            util.PostProcedure(reg, patSs, encounterID, ref accessToken);
                            break;
                        }
                    default:
                        break;
                }
            }

        }
    }
}