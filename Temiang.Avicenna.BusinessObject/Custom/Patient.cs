using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class Patient
    {
        public void SetEmpty()
        {
            this.FirstName = this.MiddleName = this.LastName = string.Empty;
            this.DateOfBirth = new DateTime(1900, 1, 1);
            this.CityOfBirth = string.Empty;
            this.GuarantorID = string.Empty;
            this.Sex = string.Empty;
            this.StreetName = string.Empty;
            this.District = string.Empty;
            this.City = string.Empty;
            this.County = string.Empty;
            this.State = string.Empty;
            this.ZipCode = string.Empty;
            this.PhoneNo = string.Empty;
            this.Email = string.Empty;
            this.Ssn = string.Empty;
            this.MobilePhoneNo = string.Empty;
            this.Notes = string.Empty;
        }
        public static string GetFullName(string firstName, string middleName, string lastName)
        {
            if (string.IsNullOrEmpty(firstName))
                return string.Empty;

            var name = string.Empty;
            name = name + firstName.Trim();
            if (middleName.Trim() != string.Empty)
                name = name + " " + middleName.Trim();
            if (lastName.Trim() != string.Empty)
                name = name + " " + lastName.Trim();

            return name;
        }

        public string PatientName
        {
            get
            {
                return GetFullName(FirstName, MiddleName, LastName);
            }
        }

        public string Address
        {
            get
            {
                if (string.IsNullOrEmpty(StreetName))
                    return string.Empty;

                var zipPostalCode = string.Empty;

                if (!string.IsNullOrEmpty(ZipCode))
                {
                    var zp = new ZipCode();
                    if (zp.LoadByPrimaryKey(ZipCode))
                    {
                        zipPostalCode = zp.ZipPostalCode;
                    }
                }

                return (StreetName + " " + City.Trim() + " " + County.Trim() + " " + zipPostalCode).Trim();
            }
        }

        public bool LoadByMedicalNo(string medicalNo)
        {
            QueryReset();
            Query.Where(Query.MedicalNo == medicalNo);
            Query.es.Top = 1;
            return Load(Query);
        }

        public bool GetPatientByNorm(string medicalNo)
        {
            PatientCollection ps = new PatientCollection();
            ps.Query.Where(string.Format("<REPLACE(MedicalNo, '-', '') LIKE '{0}'>", medicalNo.Replace("-", ""))); // ps.Query.MedicalNo.Like("%" + norm + "%"));
            ps.LoadAll();
            if (ps.Count != 1)
            {
                return false; //"Please enter a valid Medical Number";
            }
            this.LoadByPrimaryKey(ps[0].PatientID);
            return true;
        }

        public bool GetpatientByGuarantorCardNo(string GuarantorCardNo, string SRGuarantorTypeBPJS)
        {
            var patColl = new PatientCollection();
            var patq = new PatientQuery("patq");
            var guarq = new GuarantorQuery("guarq");
            var pat = new Patient();

            patq.InnerJoin(guarq).On(patq.GuarantorID == guarq.GuarantorID)
                                .Where(patq.GuarantorCardNo == GuarantorCardNo, patq.IsActive == true,
                                    guarq.IsActive == true, guarq.SRGuarantorType == SRGuarantorTypeBPJS /*bpjs*/
                                );
            if (patColl.Load(patq))
            {
                if (patColl.Count > 1)
                {
                    throw new Exception("Invalid card number, found multiple record with the same card number");
                }

                pat = patColl.First();
            }
            else
            {
                //  cari di registrasi
                var regColl = new RegistrationCollection();
                var regq = new RegistrationQuery("regq");
                guarq = new GuarantorQuery("guarq");
                regq.InnerJoin(guarq).On(regq.GuarantorID == guarq.GuarantorID)
                    .Where(regq.GuarantorCardNo == GuarantorCardNo, regq.IsVoid == false,
                        guarq.IsActive == true, guarq.SRGuarantorType == SRGuarantorTypeBPJS /*bpjs*/
                    );


                if (regColl.Load(regq))
                {
                    pat.LoadByPrimaryKey(regColl.First().PatientID);
                    pat.GuarantorID = regColl.First().GuarantorID;
                }
                else
                {
                    return false;
                }
            }

            this.LoadByPrimaryKey(pat.PatientID);
            this.GuarantorID = pat.GuarantorID;
            this.GuarantorCardNo = pat.GuarantorCardNo;
            return true;
        }

        #region Public Static
        public void ValidateCreateNew()
        {
            if (this.es.IsAdded)
            {
                var patColl = new PatientCollection();
                patColl.Query.Where(patColl.Query.FirstName == this.FirstName,
                                    patColl.Query.MiddleName == this.MiddleName,
                                    patColl.Query.LastName == this.LastName,
                                    patColl.Query.DateOfBirth.Date() == this.DateOfBirth.Value.Date,
                                    patColl.Query.Sex == this.Sex,
                                    patColl.Query.MotherName == (this.MotherName ?? ""),
                                    patColl.Query.IsActive == true);
                patColl.LoadAll();
                if (patColl.Count > 0)
                {
                    var mrno = string.Empty;
                    foreach (var pat in patColl)
                    {
                        if (mrno == string.Empty)
                            mrno = pat.MedicalNo;
                        else
                            mrno = mrno + ", " + pat.MedicalNo;
                    }
                    throw new Exception(string.Format("Patient data already exists with MRN: " + mrno + "."));
                }
            }

            if (!string.IsNullOrEmpty(this.MedicalNo))
            {
                ValidateCreateNewDuplicateMRN();
            }
        }

        private void ValidateCreateNewDuplicateMRN()
        {
            PatientCollection pColl = new PatientCollection();
            PatientQuery patientQuery = new PatientQuery();
            patientQuery.es.Top = 1;
            patientQuery.Select(patientQuery.PatientName, patientQuery.PatientID);
            patientQuery.Where(patientQuery.MedicalNo == this.MedicalNo);
            //DataTable dtb = patientQuery.LoadDataTable();
            if (pColl.Load(patientQuery))
            {
                if (pColl[0].PatientID == this.PatientID)
                    throw new Exception(string.Format("MRN: {0} has been used by another patient, please change to other No.",
                                  this.MedicalNo));
            }
        }
        #endregion

        private static List<string> _patientRelateds;
        public static List<string> PatientRelateds(string patientID)
        {

            // Check apakah patientID menjadi relatednya jika ya ambil patientID masternya
            var pr = new PatientRelated();
            pr.Query.Where(pr.Query.RelatedPatientID == patientID);
            pr.Query.es.Top = 1;
            if (pr.Query.Load() && !string.IsNullOrEmpty(pr.PatientID))
            {
                patientID = pr.PatientID;
            }

            _patientRelateds = new List<string> { patientID };

            //Add from merge
            var prs = new PatientRelatedCollection();
            prs.Query.Where(prs.Query.PatientID == patientID);
            if (prs.LoadAll() && prs.Count > 0)
            {
                foreach (var item in prs)
                {
                    _patientRelateds.Add(item.RelatedPatientID);
                }
            }
            return _patientRelateds;
        }

        #region ChronicDisease
        public string ChronicDisease()
        {
            return ChronicDisease(PatientID);
        }

        public static string ChronicDisease(string patientID)
        {
            var chronicDisease = string.Empty;

            // Cek pernah apakah didiagnosa penyakit kronis
            var ep = new EpisodeDiagnoseQuery("e");
            var diag = new DiagnoseQuery("n");
            ep.InnerJoin(diag).On(ep.DiagnoseID == diag.DiagnoseID);

            var dtd = new DtdQuery("d");
            ep.InnerJoin(dtd).On(diag.DtdNo == dtd.DtdNo);

            var stdi = new AppStandardReferenceItemQuery("i");
            ep.InnerJoin(stdi).On(dtd.SRChronicDisease == stdi.ItemID &&
                                  stdi.StandardReferenceID == "ChronicDisease");

            var reg = new RegistrationQuery("r");
            ep.InnerJoin(reg).On(ep.RegistrationNo == reg.RegistrationNo);
            ep.Where(reg.PatientID == patientID, reg.IsVoid == false, ep.IsVoid == false);
            ep.Select(stdi.ItemName);
            ep.es.Distinct = true;

            var dtb = ep.LoadDataTable();
            foreach (DataRow row in dtb.Rows)
            {
                chronicDisease = string.Format("{0}, {1}", chronicDisease, row[0]);
            }

            if (!string.IsNullOrEmpty(chronicDisease))
            {
                chronicDisease = chronicDisease.Substring(2);
            }

            return chronicDisease;
        }

        #endregion

        #region PastMedicalHistory
        public string PastMedicalHistory()
        {
            return PastMedicalHistory(PatientID);
        }

        public static string PastMedicalHistory(string patientID, bool useBrTag = false)
        {
            var result = string.Empty;

            var query = new PastMedicalHistoryQuery("e");
            var stdi = new AppStandardReferenceItemQuery("i");
            query.InnerJoin(stdi).On(query.SRMedicalDisease == stdi.ItemID &&
                                  stdi.StandardReferenceID == "PastMedHist");
            query.Where(query.PatientID == patientID);
            query.Select(stdi.ItemName, query.Notes);
            var dtb = query.LoadDataTable();

            if (useBrTag)
            {
                foreach (DataRow row in dtb.Rows)
                {
                    result = string.Format("{0}{1}: {2}<br />", result, row["ItemName"], row["notes"]);
                }
            }
            else
            {
                foreach (DataRow row in dtb.Rows)
                {
                    result = string.Format("{0}, {1} {2}", result, row["ItemName"], row["notes"]);
                }

                if (!string.IsNullOrEmpty(result))
                {
                    result = result.Substring(2);
                }
            }
           
            return result;
        }

        #endregion

        public Registration LastRegistration()
        {
            return Last.Registration(PatientID);
        }

        public static class Last
        {
            public static Registration Registration(string patientID)
            {
                var qr = new RegistrationQuery();
                qr.Where(qr.PatientID == patientID);
                qr.OrderBy(qr.RegistrationDate.Descending, qr.RegistrationTime.Descending);
                qr.es.Top = 1;

                var reg = new Registration();
                if (reg.Load(qr))
                    return reg;

                return null;
            }
            public static List<string> RegistrationNos(string patientID, int lastCount, string startFromRegistrationNo = null)
            {
                if (lastCount == 0) lastCount = 1;
                if (lastCount < 2 && !string.IsNullOrWhiteSpace(startFromRegistrationNo))
                    return new List<string>() { startFromRegistrationNo };

                var regTime = string.Empty;
                DateTime regDate = DateTime.Today;
                var qr = new RegistrationQuery();
                qr.Select(qr.RegistrationNo, qr.RegistrationDate, qr.RegistrationTime);
                qr.OrderBy(qr.RegistrationDate.Descending, qr.RegistrationTime.Descending);
                qr.es.Top = lastCount;

                var patientRelateds = Patient.PatientRelateds(patientID);
                if (patientRelateds.Count == 1)
                    qr.Where(qr.PatientID == patientID);
                else
                    qr.Where(qr.PatientID.In(patientRelateds));

                if (!string.IsNullOrWhiteSpace(startFromRegistrationNo))
                {
                    var reg = new Registration();
                    if (reg.LoadByPrimaryKey(startFromRegistrationNo))
                    {
                        regTime = reg.RegistrationTime;
                        regDate = reg.RegistrationDate.Value;
                        qr.Where(qr.RegistrationDate <= regDate);
                    }
                    else
                        qr.Where(qr.RegistrationNo <= startFromRegistrationNo);
                }
                qr.Where(qr.IsVoid == false, qr.IsFromDispensary == false, qr.IsDirectPrescriptionReturn == false, qr.IsNonPatient == false);

                var dtb = qr.LoadDataTable();

                var regs = new List<string>();
                regs.Add(startFromRegistrationNo);

                //Add prev reg
                if (dtb != null && dtb.Rows.Count > 0)
                {
                    // Jika ditgl yg sama Filter ambil hanya reg jam sebelumnya (Handono 231018)
                    foreach (DataRow row in dtb.Rows)
                    {
                        if (regDate.Equals(row["RegistrationDate"]))
                        {
                            if (regTime.CompareTo(row["RegistrationTime"]) > 0) // ambil hanya jika RegistrationTime lebih awal dari regTime
                            {
                                regs.Add(row["RegistrationNo"].ToString());
                            }
                        }
                        else
                            regs.Add(row["RegistrationNo"].ToString());
                    }
                }
                return regs;
            }

            public static string PhysicalExamination(string registrationNo, string fromRegistrationNo, bool useBrTag = false)
            {
                // Ambil assessemnt terakhir
                var query = new PatientAssessmentQuery();
                if (!String.IsNullOrWhiteSpace(fromRegistrationNo))
                    query.Where(query.Or(query.RegistrationNo == registrationNo, query.RegistrationNo == fromRegistrationNo));
                else
                    query.Where(query.RegistrationNo == registrationNo);

                //query.Where(query.RegistrationNo.In(mergeRegistrations));
                query.es.Top = 1;
                query.OrderBy(query.RegistrationInfoMedicID.Descending);
                var pas = new PatientAssessment();
                if (pas.Load(query))
                {
                    //Ambil objective 
                    var rim = new RegistrationInfoMedic();
                    if (rim.LoadByPrimaryKey(pas.RegistrationInfoMedicID) && useBrTag)
                        return rim.Info2.Replace("\r\n", "<br/>");
                    else if(rim.LoadByPrimaryKey(pas.RegistrationInfoMedicID))
                        return rim.Info2;
                    else 
                        return String.Empty;
                }

                return String.Empty;
            }

            public static PatientAssessment PatientAssessment(string registrationNo, string fromRegistrationNo)
            {
                var qra = new PatientAssessmentQuery("a");
                if (!String.IsNullOrWhiteSpace(fromRegistrationNo))
                    qra.Where(qra.Or(qra.RegistrationNo == registrationNo, qra.RegistrationNo == fromRegistrationNo));
                else
                    qra.Where(qra.RegistrationNo == registrationNo);
                //qra.Where(qra.RegistrationNo.In(mergeRegistrations));

                qra.OrderBy(qra.RegistrationInfoMedicID.Ascending);
                qra.es.Top = 1;
                var assesment = new PatientAssessment();
                if (assesment.Load(qra))
                {
                    return assesment;
                }
                else
                {
                    return new PatientAssessment();
                }
            }
        }
    }
}