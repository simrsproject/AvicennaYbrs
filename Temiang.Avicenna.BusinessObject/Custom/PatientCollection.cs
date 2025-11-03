using System;
using System.Linq;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;
using System.Data;
using Temiang.Avicenna.BusinessObject.JsonField.Assesment;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class PatientCollection
    {
        public DataTable PatientRegisterAble(string searchPatient, string dateOfBirth, string phoneNo, string address, byte isValidateByZipCode, string regType, string nik, string ssn, string guarantorCardNo)
        {
            var qb = new HealthcareQuery("b");
            var qr = new PatientQuery("a");
            var qc = new AppStandardReferenceItemQuery("c");
            qr.LeftJoin(qb).On(qr.HealthcareID == qb.HealthcareID);
            qr.LeftJoin(qc).On(qc.StandardReferenceID == "Salutation" && qc.ItemID == qr.SRSalutation);

            qr.es.Top = 500;
            qr.Select(
                qr.PatientID,
                qr.MedicalNo,
                qr.OldMedicalNo,
                qr.PatientName,
                qr.Sex,
                qr.Address,
                qr.PhoneNo,
                qr.MobilePhoneNo,
                qr.DateOfBirth,
                qb.HealthcareName,
                qr.IsBlackList,
                qr.IsNotPaidOff,
                qr.IsAlive,
                qc.ItemName.As("Salutation"),
                @"<IsFromMpi = CONVERT(BIT, 0)>",
                //@"<IsFromMpi = CONVERT(BIT, 0), 
                //   IsRegisterAble = CASE WHEN ((SELECT ap.ParameterValue
                //                                FROM   AppParameter ap
                //                                WHERE  ap.ParameterID = 'IsAllowMultipleRegOp') = 'Yes') THEN CAST(1 AS BIT)
                //                    ELSE CONVERT(BIT, CASE WHEN EXISTS(SELECT RegistrationNo 
                //                                                  FROM   Registration r 
                //                                                  WHERE  r.PatientID = a.PatientID 
                //                                                         AND r.IsClosed = 0 
                //                                                         AND r.IsVoid = 0) THEN 0 
                //                                      ELSE 1 
                //                                      END)
                //                    END>",
                qr.ZipCode.Coalesce("''"), string.Format("<CAST({0} AS BIT) AS IsValidateByZipCode>",
                isValidateByZipCode), qr.Ssn,
                qr.IsStoredToLokadok, qr.IsActive,
                qr.IsKYC
                );

            switch (regType)
            {
                case "IPR":
                    qr.Select(@"<IsRegisterAble = (CASE WHEN EXISTS(SELECT r.RegistrationNo FROM Registration r 
                                   WHERE r.PatientID = a.PatientID  
                                   AND r.SRRegistrationType = 'IPR' 
                                   AND r.DischargeDate IS NULL 
                                   AND r.IsVoid = 0) THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END)>");
                    break;
                case "OPR":
                    qr.Select(@"<IsRegisterAble = (CASE WHEN ((SELECT ap.ParameterValue
                                                FROM   AppParameter ap
                                                WHERE  ap.ParameterID = 'IsAllowMultipleRegOp') = 'Yes') THEN CAST(1 AS BIT)
                                    ELSE CONVERT(BIT, CASE WHEN EXISTS(SELECT RegistrationNo 
                                                                  FROM   Registration r 
                                                                  WHERE  r.PatientID = a.PatientID 
                                                                         AND r.IsClosed = 0 
                                                                         AND r.IsVoid = 0) THEN CAST(0 AS BIT)
                                                      ELSE CAST(1 AS BIT) 
                                                      END)
                                    END)>");
                    break;
                case "EMR":
                    qr.Select(@"<IsRegisterAble = (CASE WHEN EXISTS(SELECT r.RegistrationNo FROM Registration r 
                                   WHERE r.PatientID = a.PatientID  
                                   AND r.SRRegistrationType = 'EMR' 
                                   AND r.IsClosed = 0 
                                   AND r.IsVoid = 0) THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END)>");
                    break;
                case "MCU":
                case "ANC":
                default:
                    qr.Select(@"<IsRegisterAble = CAST(1 AS BIT)>");
                    break;
            }

            qr.Where(//qr.IsActive == 1, 
                qr.IsNonPatient == false);

            if (!string.IsNullOrEmpty(dateOfBirth))
                qr.Where(qr.DateOfBirth == dateOfBirth);

            if (!string.IsNullOrEmpty(phoneNo))
                qr.Where(
                    qr.Or(
                        qr.PhoneNo == phoneNo,
                        qr.MobilePhoneNo == phoneNo
                        )
                    );

            if (searchPatient != string.Empty)
            {
                //string searchPatient = "%" + txtPatientName.Text + "%";

                string sNumber = searchPatient.Replace("-", "").Replace("/", "").Replace(".", "");
                int n;
                bool isNumeric = int.TryParse(sNumber, out n);
                if (isNumeric)
                {
                    //if (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsMedicalNoContainStrip) == "Yes")
                    //    // for fast search: numeric is medical no
                    //    qr.Where(qr.Or(
                    //        string.Format("<REPLACE(REPLACE(REPLACE(a.MedicalNo, '-', ''), '/', ''), '.', '') LIKE '%{0}%'>", sNumber),
                    //        string.Format("< OR REPLACE(REPLACE(REPLACE(a.OldMedicalNo, '-', ''), '/', ''), '.', '') LIKE '%{0}%'>", sNumber)
                    //        )
                    //    );
                    //else
                    //    qr.Where(qr.Or(
                    //        string.Format("< a.MedicalNo LIKE '%{0}%'>", sNumber),
                    //        string.Format("< OR a.OldMedicalNo LIKE '%{0}%'>", sNumber)
                    //        )
                    //    );

                    var reverseMedNoSearch = string.Format("{0}%", searchPatient.Replace("-", "").Reverse());
                    qr.Where(
                        qr.Or(
                            qr.ReverseMedicalNo.Like(reverseMedNoSearch),
                            qr.ReverseOldMedicalNo.Like(reverseMedNoSearch)
                        )
                    );

                }
                else
                {
                    // search by medical no already defined above
                    //string searchTextContain = string.Format("%{0}%", searchPatient);
                    //qr.Where(
                    //    qr.Or(
                    //        qr.PatientID == searchPatient,
                    //        qr.MedicalNo.Like(searchTextContain),
                    //        //qr.OldMedicalNo.Like(searchTextContain),
                    //        string.Format("< OR RTRIM(LTRIM(RTRIM(LTRIM(a.FirstName + ' ' + a.MiddleName)) + ' ' + a.LastName)) LIKE '%{0}%'>", searchPatient)
                    //        //,
                    //        //string.Format("< OR REPLACE(a.MedicalNo, '-', '') LIKE '%{0}%'>", searchPatient),
                    //        //string.Format("< OR REPLACE(a.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchPatient)
                    //    )
                    //);

                    string searchTextContain = string.Format("{0}%", searchPatient);
                    qr.Where(qr.FullName.Like(searchTextContain));
                }
            }

            if (!string.IsNullOrEmpty(address))
            {
                if (address.Trim().Contains(" "))
                {
                    var searchs = address.Split(' ');
                    foreach (var searchLike in searchs.Select(search => "%" + search + "%"))
                    {
                        qr.Where(
                            qr.Or(
                                qr.StreetName.Like(searchLike),
                                qr.City.Like(searchLike),
                                qr.County.Like(searchLike),
                                qr.ZipCode.Like(searchLike)
                                )
                            );
                    }
                }
                else
                {
                    var searchLike = "%" + address + "%";
                    qr.Where(
                        qr.Or(
                                qr.StreetName.Like(searchLike),
                                qr.City.Like(searchLike),
                                qr.County.Like(searchLike),
                                qr.ZipCode.Like(searchLike)
                            )
                        );
                }
            }

            if (!string.IsNullOrWhiteSpace(nik))
            {
                qr.Select(qr.Ssn, "<CAST(ISNULL(a.IsSyncWithDukcapil, 0) AS BIT) AS IsSyncWithDukcapil>");
                qr.Where(qr.Ssn == nik, qr.IsSyncWithDukcapil == true);
            }

            if (!string.IsNullOrEmpty(ssn))
            {
                var searchLike = "%" + ssn + "%";
                qr.Where(qr.Ssn.Like(searchLike));
            }

            if (!string.IsNullOrEmpty(guarantorCardNo))
            {
                var searchLike = "%" + guarantorCardNo + "%";
                qr.Where(qr.GuarantorCardNo.Like(searchLike));
            }

            qr.OrderBy(searchPatient != string.Empty ? qr.FirstName.Ascending : qr.MedicalNo.Descending);


            return qr.LoadDataTable();
        }

        public DataTable PatientRegisterAbleForDifferentRegType(string searchPatient, string dateOfBirth, string phoneNo, string address, string regType)
        {
            var qb = new HealthcareQuery("b");
            var qr = new PatientQuery("a");
            qr.LeftJoin(qb).On(qr.HealthcareID == qb.HealthcareID);

            qr.es.Top = 500;
            qr.Select(
                qr.PatientID,
                qr.MedicalNo,
                qr.OldMedicalNo,
                qr.PatientName,
                qr.Sex,
                qr.Address,
                qr.PhoneNo,
                qr.MobilePhoneNo,
                qr.DateOfBirth,
                qb.HealthcareName,
                @"<IsFromMpi = CONVERT(BIT, 0), 
                   IsRegisterAble = CASE WHEN ((SELECT ap.ParameterValue
                                                FROM   AppParameter ap
                                                WHERE  ap.ParameterID = 'IsAllowMultipleRegOp') = 'Yes') THEN CAST(1 AS BIT)
                                    ELSE 
                                        CASE WHEN ((SELECT ap.ParameterValue
                                                FROM   AppParameter ap
                                                WHERE  ap.ParameterID = 'IsAllowMultipleRegForDifferentRegType') = 'Yes') THEN 
                                                        (CONVERT(BIT, CASE WHEN EXISTS(SELECT RegistrationNo 
                                                                          FROM   Registration r 
                                                                          WHERE  r.PatientID = a.PatientID 
                                                                                 AND r.IsClosed = 0 
                                                                                 AND r.IsVoid = 0 AND SRRegistrationType = '" + regType + "') THEN 0" +
                                                              " ELSE 1 " +
                                                              " END))" +
                                        " ELSE " +
                                        " CONVERT(BIT, CASE WHEN EXISTS(SELECT RegistrationNo " +
                                                                  "FROM   Registration r " +
                                                                  "WHERE  r.PatientID = a.PatientID " +
                                                                         "AND r.IsClosed = 0 " +
                                                                        " AND r.IsVoid = 0) THEN 0 " +
                                                      " ELSE 1 " +
                                                      " END)" +
                                        " END" +
                                    " END>"
                );
            qr.Where(qr.IsActive == 1, qr.IsNonPatient == false);

            if (!string.IsNullOrEmpty(dateOfBirth))
                qr.Where(qr.DateOfBirth == dateOfBirth);

            if (!string.IsNullOrEmpty(phoneNo))
                qr.Where(
                    qr.Or(
                        qr.PhoneNo == phoneNo,
                        qr.MobilePhoneNo == phoneNo
                        )
                    );

            if (searchPatient != string.Empty)
            {
                if (searchPatient.Trim().Contains(" "))
                {
                    var searchs = searchPatient.Split(' ');
                    foreach (var searchLike in searchs.Select(search => "%" + search + "%"))
                    {
                        qr.Where(
                            qr.Or(
                                qr.PatientID.Like(searchLike),
                                qr.FirstName.Like(searchLike),
                                qr.LastName.Like(searchLike),
                                qr.MiddleName.Like(searchLike),
                                qr.MedicalNo.Like(searchLike),
                                qr.OldMedicalNo.Like(searchLike)
                                )
                            );
                    }
                }
                else
                {
                    var searchLike = "%" + searchPatient + "%";
                    qr.Where(
                        qr.Or(
                            qr.PatientID.Like(searchLike),
                            qr.FirstName.Like(searchLike),
                            qr.LastName.Like(searchLike),
                            qr.MiddleName.Like(searchLike),
                            qr.MedicalNo.Like(searchLike),
                            qr.OldMedicalNo.Like(searchLike)
                            )
                        );
                }
            }

            if (!string.IsNullOrEmpty(address))
            {
                if (address.Trim().Contains(" "))
                {
                    var searchs = address.Split(' ');
                    foreach (var searchLike in searchs.Select(search => "%" + search + "%"))
                    {
                        qr.Where(
                            qr.Or(
                                qr.StreetName.Like(searchLike),
                                qr.City.Like(searchLike),
                                qr.County.Like(searchLike),
                                qr.ZipCode.Like(searchLike)
                                )
                            );
                    }
                }
                else
                {
                    var searchLike = "%" + address + "%";
                    qr.Where(
                        qr.Or(
                                qr.StreetName.Like(searchLike),
                                qr.City.Like(searchLike),
                                qr.County.Like(searchLike),
                                qr.ZipCode.Like(searchLike)
                            )
                        );
                }
            }

            qr.OrderBy(searchPatient != string.Empty ? qr.FirstName.Ascending : qr.PatientID.Ascending);

            return qr.LoadDataTable();
        }

        public DataTable PatientRegisterRelatedAble(string searchPatient, string dateOfBirth, string phoneNo, string address)
        {
            var qb = new HealthcareQuery("c");
            var related = new PatientRelatedQuery("b");
            var qr = new PatientQuery("a");
            qr.InnerJoin(related).On(qr.PatientID == related.RelatedPatientID);
            qr.LeftJoin(qb).On(qr.HealthcareID == qb.HealthcareID);
            qr.es.Top = 500;
            qr.Select(
                related.PatientID,
                qr.MedicalNo,
                qr.OldMedicalNo,
                qr.PatientName,
                qr.Sex,
                qr.Address,
                qr.PhoneNo,
                qr.MobilePhoneNo,
                qr.DateOfBirth,
                qb.HealthcareName,
                @"<IsFromMpi = CONVERT(BIT, 0), 
                   IsRegisterAble = CASE WHEN ((SELECT ap.ParameterValue
                                                FROM   AppParameter ap
                                                WHERE  ap.ParameterID = 'IsAllowMultipleRegOp') = 'Yes') THEN CAST(1 AS BIT)
                                    ELSE CONVERT(BIT, CASE WHEN EXISTS(SELECT RegistrationNo 
                                                                  FROM   Registration r 
                                                                  WHERE  r.PatientID = a.PatientID 
                                                                         AND r.IsClosed = 0 
                                                                         AND r.IsVoid = 0) THEN 0 
                                                      ELSE 1 
                                                      END)
                                    END>"
                );
            qr.Where(qr.IsActive == 1, qr.IsNonPatient == false);

            if (!string.IsNullOrEmpty(dateOfBirth))
                qr.Where(qr.DateOfBirth == dateOfBirth);

            if (!string.IsNullOrEmpty(phoneNo))
                qr.Where(
                    qr.Or(
                        qr.PhoneNo == phoneNo,
                        qr.MobilePhoneNo == phoneNo
                        )
                    );

            if (searchPatient != string.Empty)
            {
                if (searchPatient.Trim().Contains(" "))
                {
                    var searchs = searchPatient.Split(' ');
                    foreach (var searchLike in searchs.Select(search => "%" + search + "%"))
                    {
                        qr.Where(
                            qr.Or(
                                qr.PatientID.Like(searchLike),
                                qr.FirstName.Like(searchLike),
                                qr.LastName.Like(searchLike),
                                qr.MiddleName.Like(searchLike),
                                qr.MedicalNo.Like(searchLike),
                                qr.OldMedicalNo.Like(searchLike)
                                )
                            );
                    }
                }
                else
                {
                    var searchLike = "%" + searchPatient + "%";
                    qr.Where(
                        qr.Or(
                            qr.PatientID.Like(searchLike),
                            qr.FirstName.Like(searchLike),
                            qr.LastName.Like(searchLike),
                            qr.MiddleName.Like(searchLike),
                            qr.MedicalNo.Like(searchLike),
                            qr.OldMedicalNo.Like(searchLike)
                            )
                        );
                }
            }

            if (!string.IsNullOrEmpty(address))
            {
                if (address.Trim().Contains(" "))
                {
                    var searchs = address.Split(' ');
                    foreach (var searchLike in searchs.Select(search => "%" + search + "%"))
                    {
                        qr.Where(
                            qr.Or(
                                qr.StreetName.Like(searchLike),
                                qr.City.Like(searchLike),
                                qr.County.Like(searchLike),
                                qr.ZipCode.Like(searchLike)
                                )
                            );
                    }
                }
                else
                {
                    var searchLike = "%" + address + "%";
                    qr.Where(
                        qr.Or(
                                qr.StreetName.Like(searchLike),
                                qr.City.Like(searchLike),
                                qr.County.Like(searchLike),
                                qr.ZipCode.Like(searchLike)
                            )
                        );
                }
            }

            qr.OrderBy(searchPatient != string.Empty ? qr.FirstName.Ascending : qr.PatientID.Ascending);

            return qr.LoadDataTable();
        }

        public DataTable PatientRegisterRelatedAbleForDifferentRegType(string searchPatient, string dateOfBirth, string phoneNo, string address, string regType)
        {
            var qb = new HealthcareQuery("c");
            var related = new PatientRelatedQuery("b");
            var qr = new PatientQuery("a");
            qr.InnerJoin(related).On(qr.PatientID == related.RelatedPatientID);
            qr.LeftJoin(qb).On(qr.HealthcareID == qb.HealthcareID);
            qr.es.Top = 500;
            qr.Select(
                related.PatientID,
                qr.MedicalNo,
                qr.OldMedicalNo,
                qr.PatientName,
                qr.Sex,
                qr.Address,
                qr.PhoneNo,
                qr.MobilePhoneNo,
                qr.DateOfBirth,
                qb.HealthcareName,
                @"<IsFromMpi = CONVERT(BIT, 0), 
                   IsRegisterAble = CASE WHEN ((SELECT ap.ParameterValue
                                                FROM   AppParameter ap
                                                WHERE  ap.ParameterID = 'IsAllowMultipleRegOp') = 'Yes') THEN CAST(1 AS BIT)
                                    ELSE 
                                        CASE WHEN ((SELECT ap.ParameterValue
                                                FROM   AppParameter ap
                                                WHERE  ap.ParameterID = 'IsAllowMultipleRegForDifferentRegType') = 'Yes') THEN 
                                                        (CONVERT(BIT, CASE WHEN EXISTS(SELECT RegistrationNo 
                                                                          FROM   Registration r 
                                                                          WHERE  r.PatientID = a.PatientID 
                                                                                 AND r.IsClosed = 0 
                                                                                 AND r.IsVoid = 0 AND SRRegistrationType = '" + regType + "') THEN 0" +
                                                                      " ELSE 1 " +
                                                                      " END))" +
                                        " ELSE " +
                                        " CONVERT(BIT, CASE WHEN EXISTS(SELECT RegistrationNo " +
                                                                  "FROM   Registration r " +
                                                                  "WHERE  r.PatientID = a.PatientID " +
                                                                         "AND r.IsClosed = 0 " +
                                                                        " AND r.IsVoid = 0) THEN 0 " +
                                                      " ELSE 1 " +
                                                      " END)" +
                                        " END" +
                                    " END>"
                );
            qr.Where(qr.IsNonPatient == false);

            if (!string.IsNullOrEmpty(dateOfBirth))
                qr.Where(qr.DateOfBirth == dateOfBirth);

            if (!string.IsNullOrEmpty(phoneNo))
                qr.Where(
                    qr.Or(
                        qr.PhoneNo == phoneNo,
                        qr.MobilePhoneNo == phoneNo
                        )
                    );

            if (searchPatient != string.Empty)
            {
                if (searchPatient.Trim().Contains(" "))
                {
                    var searchs = searchPatient.Split(' ');
                    foreach (var searchLike in searchs.Select(search => "%" + search + "%"))
                    {
                        qr.Where(
                            qr.Or(
                                qr.PatientID.Like(searchLike),
                                qr.FirstName.Like(searchLike),
                                qr.LastName.Like(searchLike),
                                qr.MiddleName.Like(searchLike),
                                qr.MedicalNo.Like(searchLike),
                                qr.OldMedicalNo.Like(searchLike)
                                )
                            );
                    }
                }
                else
                {
                    var searchLike = "%" + searchPatient + "%";
                    qr.Where(
                        qr.Or(
                            qr.PatientID.Like(searchLike),
                            qr.FirstName.Like(searchLike),
                            qr.LastName.Like(searchLike),
                            qr.MiddleName.Like(searchLike),
                            qr.MedicalNo.Like(searchLike),
                            qr.OldMedicalNo.Like(searchLike)
                            )
                        );
                }
            }

            if (!string.IsNullOrEmpty(address))
            {
                if (address.Trim().Contains(" "))
                {
                    var searchs = address.Split(' ');
                    foreach (var searchLike in searchs.Select(search => "%" + search + "%"))
                    {
                        qr.Where(
                            qr.Or(
                                qr.StreetName.Like(searchLike),
                                qr.City.Like(searchLike),
                                qr.County.Like(searchLike),
                                qr.ZipCode.Like(searchLike)
                                )
                            );
                    }
                }
                else
                {
                    var searchLike = "%" + address + "%";
                    qr.Where(
                        qr.Or(
                                qr.StreetName.Like(searchLike),
                                qr.City.Like(searchLike),
                                qr.County.Like(searchLike),
                                qr.ZipCode.Like(searchLike)
                            )
                        );
                }
            }

            qr.OrderBy(searchPatient != string.Empty ? qr.FirstName.Ascending : qr.PatientID.Ascending);

            return qr.LoadDataTable();
        }

        public DataTable PatientRegisterAble(string searchPatient, string dateOfBirth, string phoneNo, string address, int top)
        {
            return PatientRegisterAble(searchPatient, dateOfBirth, phoneNo, address, top, false);
        }
        public DataTable PatientRegisterAbleByMedicalNo(string medicalno, int top)
        {
            return PatientRegisterAble(medicalno, string.Empty, string.Empty, string.Empty, top, true);
        }
        public DataTable PatientRegisterAble(string searchPatient, string dateOfBirth, string phoneNo, string address, int top, bool exactmatch)
        {
            var qr = new PatientQuery("a");
            qr.es.Top = top == 0 ? int.Parse(AppParameter.GetParameterValue(AppParameter.ParameterItem.MaxResultRecord)) : top;
            qr.Select(
                qr.PatientID,
                qr.MedicalNo,
                qr.OldMedicalNo,
                qr.PatientName,
                qr.Sex,
                qr.Address,
                qr.PhoneNo,
                qr.MobilePhoneNo,
                qr.DateOfBirth,
                @"<IsFromMpi = CONVERT(BIT, 0), 
                   IsRegisterAble = CASE WHEN ((SELECT ap.ParameterValue
                                                FROM   AppParameter ap
                                                WHERE  ap.ParameterID = 'IsAllowMultipleRegOp') = 'Yes') THEN CAST(1 AS BIT)
                                    ELSE CONVERT(BIT, CASE WHEN EXISTS(SELECT RegistrationNo 
                                                                  FROM   Registration r 
                                                                  WHERE  r.PatientID = a.PatientID 
                                                                         AND r.IsClosed = 0 
                                                                         AND r.IsVoid = 0) THEN 0 
                                                      ELSE 1 
                                                      END)
                                    END>"
                );
            qr.Where(qr.IsActive == 1, qr.IsNonPatient == false);

            if (!string.IsNullOrEmpty(dateOfBirth))
                qr.Where(qr.DateOfBirth == dateOfBirth);

            if (!string.IsNullOrEmpty(phoneNo))
                qr.Where(
                    qr.Or(
                        qr.PhoneNo == phoneNo,
                        qr.MobilePhoneNo == phoneNo
                        )
                    );

            if (searchPatient != string.Empty)
            {
                string sNumber = searchPatient.Replace("-", "").Replace("/", "");
                int n;
                bool isNumeric = int.TryParse(sNumber, out n);
                if (isNumeric)
                {
                    // for fast search: numeric removed - and / can be a patient number or birthdate
                    if (searchPatient.Contains('/'))
                    {
                        // probably a date input
                        //DateTime birthDate;
                        //var isDate = DateTime.TryParse(searchPatient, System.Globalization.CultureInfo.CurrentCulture,
                        //    System.Globalization.DateTimeStyles.NoCurrentDateDefault, out birthDate);
                        //if (isDate) {
                        //    qr.Where(qr.DateOfBirth == birthDate);
                        //}
                        searchPatient = searchPatient.Trim();
                        string[] datePart = searchPatient.Split('/');
                        int d, m, y;
                        if (datePart.Length > 0)
                        {
                            isNumeric = int.TryParse(datePart[0], out d);
                            if (isNumeric)
                                qr.Where(string.Format("<DAY(a.DateOfBirth) = {0}>", d));
                        }
                        if (datePart.Length > 1)
                        {
                            isNumeric = int.TryParse(datePart[1], out m);
                            if (isNumeric)
                                qr.Where(string.Format("<MONTH(a.DateOfBirth) = {0}>", m));
                        }
                        if (datePart.Length > 2)
                        {
                            isNumeric = int.TryParse(datePart[2], out y);
                            if (isNumeric)
                                qr.Where(string.Format("<YEAR(a.DateOfBirth) = {0}>", y));
                        }
                    }
                    else
                    {
                        if (exactmatch)
                        {
                            qr.Where(string.Format("<REPLACE(a.MedicalNo, '-', '') LIKE '{0}'>", sNumber));
                        }
                        else
                        {
                            qr.Where(string.Format("<REPLACE(a.MedicalNo, '-', '') LIKE '%{0}%'>", sNumber));
                        }
                    }
                }
                else
                {
                    if (searchPatient.Trim().Contains(" "))
                    {
                        var searchs = searchPatient.Split(' ');
                        foreach (var searchLike in searchs.Select(search => "%" + search + "%"))
                        {
                            qr.Where(
                                qr.Or(
                                    qr.Ssn == searchPatient,
                                    qr.PatientID == searchPatient,
                                    qr.FirstName.Like(searchLike),
                                    qr.LastName.Like(searchLike),
                                    qr.MiddleName.Like(searchLike),
                                    qr.MedicalNo.Like(searchLike),
                                    qr.OldMedicalNo.Like(searchLike),
                                    qr.StreetName.Like(searchLike),
                                    string.Format("< OR REPLACE(a.MedicalNo, '-', '') LIKE '{0}'>", searchLike)
                                    )
                                );
                        }
                    }
                    else
                    {
                        var searchLike = "%" + searchPatient + "%";
                        qr.Where(
                            qr.Or(
                                qr.Ssn == searchPatient,
                                qr.PatientID == searchPatient,
                                qr.FirstName.Like(searchLike),
                                qr.LastName.Like(searchLike),
                                qr.MiddleName.Like(searchLike),
                                qr.MedicalNo.Like(searchLike),
                                qr.OldMedicalNo.Like(searchLike),
                                qr.StreetName.Like(searchLike),
                                string.Format("< OR REPLACE(a.MedicalNo, '-', '') LIKE '{0}'>", searchLike)
                                )
                            );
                    }
                }
            }

            if (!string.IsNullOrEmpty(address))
            {
                if (address.Trim().Contains(" "))
                {
                    var searchs = address.Split(' ');
                    foreach (var searchLike in searchs.Select(search => "%" + search + "%"))
                    {
                        qr.Where(
                            qr.Or(
                                qr.StreetName.Like(searchLike),
                                qr.City.Like(searchLike),
                                qr.County.Like(searchLike),
                                qr.ZipCode.Like(searchLike)
                                )
                            );
                    }
                }
                else
                {
                    var searchLike = "%" + address + "%";
                    qr.Where(
                        qr.Or(
                                qr.StreetName.Like(searchLike),
                                qr.City.Like(searchLike),
                                qr.County.Like(searchLike),
                                qr.ZipCode.Like(searchLike)
                            )
                        );
                }
            }

            qr.OrderBy(searchPatient != string.Empty ? qr.FirstName.Ascending : qr.PatientID.Ascending);

            return qr.LoadDataTable();
        }

        // untuk transaksi non patient customer charges
        public DataTable NonPatientRegisterAble(string searchPatient, string dateOfBirth, string phoneNo, string address, int top)
        {
            var qr = new PatientQuery("a");
            qr.es.Top = top;
            qr.Select(
                qr.PatientID,
                qr.MedicalNo,
                qr.OldMedicalNo,
                qr.PatientName,
                qr.Sex,
                qr.Address,
                qr.PhoneNo,
                qr.MobilePhoneNo,
                qr.DateOfBirth,
                @"<IsFromMpi = CONVERT(BIT, 0), 
                   IsRegisterAble = CASE WHEN ((SELECT ap.ParameterValue
                                                FROM   AppParameter ap
                                                WHERE  ap.ParameterID = 'IsAllowMultipleRegOp') = 'Yes') THEN CAST(1 AS BIT)
                                    ELSE CONVERT(BIT, CASE WHEN EXISTS(SELECT RegistrationNo 
                                                                  FROM   Registration r 
                                                                  WHERE  r.PatientID = a.PatientID 
                                                                         AND r.IsClosed = 0 
                                                                         AND r.IsVoid = 0) THEN 0 
                                                      ELSE 1 
                                                      END)
                                    END>"
                );
            qr.Where(qr.IsActive == 1);

            if (!string.IsNullOrEmpty(dateOfBirth))
                qr.Where(qr.DateOfBirth == dateOfBirth);

            if (!string.IsNullOrEmpty(phoneNo))
                qr.Where(
                    qr.Or(
                        qr.PhoneNo == phoneNo,
                        qr.MobilePhoneNo == phoneNo
                        )
                    );

            if (searchPatient != string.Empty)
            {
                if (searchPatient.Trim().Contains(" "))
                {
                    var searchs = searchPatient.Split(' ');
                    foreach (var searchLike in searchs.Select(search => "%" + search + "%"))
                    {
                        qr.Where(
                            qr.Or(
                                qr.PatientID.Like(searchLike),
                                qr.FirstName.Like(searchLike),
                                qr.LastName.Like(searchLike),
                                qr.MiddleName.Like(searchLike),
                                qr.MedicalNo.Like(searchLike),
                                qr.OldMedicalNo.Like(searchLike),
                                qr.Ssn.Like(searchLike)
                                )
                            );
                    }
                }
                else
                {
                    var searchLike = "%" + searchPatient + "%";
                    qr.Where(
                        qr.Or(
                            qr.PatientID.Like(searchLike),
                            qr.FirstName.Like(searchLike),
                            qr.LastName.Like(searchLike),
                            qr.MiddleName.Like(searchLike),
                            qr.MedicalNo.Like(searchLike),
                            qr.OldMedicalNo.Like(searchLike),
                            qr.Ssn.Like(searchLike)
                            )
                        );
                }
            }

            if (!string.IsNullOrEmpty(address))
            {
                if (address.Trim().Contains(" "))
                {
                    var searchs = address.Split(' ');
                    foreach (var searchLike in searchs.Select(search => "%" + search + "%"))
                    {
                        qr.Where(
                            qr.Or(
                                qr.StreetName.Like(searchLike),
                                qr.City.Like(searchLike),
                                qr.County.Like(searchLike),
                                qr.ZipCode.Like(searchLike)
                                )
                            );
                    }
                }
                else
                {
                    var searchLike = "%" + address + "%";
                    qr.Where(
                        qr.Or(
                                qr.StreetName.Like(searchLike),
                                qr.City.Like(searchLike),
                                qr.County.Like(searchLike),
                                qr.ZipCode.Like(searchLike)
                            )
                        );
                }
            }

            qr.OrderBy(searchPatient != string.Empty ? qr.FirstName.Ascending : qr.PatientID.Ascending);

            return qr.LoadDataTable();
        }

        public DataTable DuplicatePatient(string nik, string firstName, string middleName, string lastName, DateTime? dob, string sex, string address)
        {
            esParameters par = new esParameters();

            string commandText;
            if (!string.IsNullOrEmpty(nik) & nik.Length >= 16)
                commandText = "SELECT p.MedicalNo FROM Patient AS p WITH(NOLOCK) " +
                    "WHERE p.IsActive = 1 AND p.Ssn = '" + nik + "' ";
            else
                commandText = "SELECT p.MedicalNo FROM Patient AS p WITH(NOLOCK) " +
                    "WHERE (p.IsActive = 1 AND p.FirstName = '" + firstName + "' AND p.MiddleName = '" + middleName + "' AND p.LastName = '" + lastName + "' AND p.DateOfBirth = '" + dob + "') " +
                    "OR (p.IsActive = 1 AND p.FirstName = '" + firstName + "' AND p.MiddleName = '" + middleName + "' AND p.LastName = '" + lastName + "' AND p.Sex = '" + sex + "' AND p.StreetName = '" + address + "') ";

            commandText += "ORDER BY p.MedicalNo ";

            return FillDataTable(esQueryType.Text, commandText, par);
        }
    }
}

