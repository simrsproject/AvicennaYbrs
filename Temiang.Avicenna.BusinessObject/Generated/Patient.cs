/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 08/11/2024 22:40:15
===============================================================================
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;
using System.Xml.Serialization;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
    [Serializable]
    abstract public class esPatientCollection : esEntityCollectionWAuditLog
    {
        public esPatientCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "PatientCollection";
        }

        #region Query Logic
        protected void InitQuery(esPatientQuery query)
        {
            query.OnLoadDelegate = this.OnQueryLoaded;
            query.es2.Connection = ((IEntityCollection)this).Connection;
        }

        protected bool OnQueryLoaded(DataTable table)
        {
            this.PopulateCollection(table);
            return (this.RowCount > 0) ? true : false;
        }

        protected override void HookupQuery(esDynamicQuery query)
        {
            this.InitQuery(query as esPatientQuery);
        }
        #endregion

        virtual public Patient DetachEntity(Patient entity)
        {
            return base.DetachEntity(entity) as Patient;
        }

        virtual public Patient AttachEntity(Patient entity)
        {
            return base.AttachEntity(entity) as Patient;
        }

        virtual public void Combine(PatientCollection collection)
        {
            base.Combine(collection);
        }

        new public Patient this[int index]
        {
            get
            {
                return base[index] as Patient;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(Patient);
        }
    }

    [Serializable]
    abstract public class esPatient : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esPatientQuery GetDynamicQuery()
        {
            return null;
        }

        public esPatient()
        {
        }

        public esPatient(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String patientID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(patientID);
            else
                return LoadByPrimaryKeyStoredProcedure(patientID);
        }

        /// <summary>
        /// Loads an entity by primary key
        /// </summary>
        /// <remarks>
        /// Requires primary keys be defined on all tables.
        /// If a table does not have a primary key set,
        /// this method will not compile.
        /// </remarks>
        /// <param name="sqlAccessType">Either esSqlAccessType StoredProcedure or DynamicSQL</param>
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String patientID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(patientID);
            else
                return LoadByPrimaryKeyStoredProcedure(patientID);
        }

        private bool LoadByPrimaryKeyDynamic(String patientID)
        {
            esPatientQuery query = this.GetDynamicQuery();
            query.Where(query.PatientID == patientID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String patientID)
        {
            esParameters parms = new esParameters();
            parms.Add("PatientID", patientID);
            return this.Load(esQueryType.StoredProcedure, this.es.spLoadByPrimaryKey, parms);
        }
        #endregion

        #region Properties

        public override void SetProperties(IDictionary values)
        {
            foreach (string propertyName in values.Keys)
            {
                this.SetProperty(propertyName, values[propertyName]);
            }
        }

        public override void SetProperty(string name, object value)
        {
            if (this.Row == null) this.AddNew();

            esColumnMetadata col = this.Meta.Columns.FindByPropertyName(name);
            if (col != null)
            {
                if (value == null || value is System.String)
                {
                    // Use the strongly typed property
                    switch (name)
                    {
                        case "PatientID": this.str.PatientID = (string)value; break;
                        case "MedicalNo": this.str.MedicalNo = (string)value; break;
                        case "Ssn": this.str.Ssn = (string)value; break;
                        case "SRSalutation": this.str.SRSalutation = (string)value; break;
                        case "FirstName": this.str.FirstName = (string)value; break;
                        case "MiddleName": this.str.MiddleName = (string)value; break;
                        case "LastName": this.str.LastName = (string)value; break;
                        case "ParentSpouseName": this.str.ParentSpouseName = (string)value; break;
                        case "CityOfBirth": this.str.CityOfBirth = (string)value; break;
                        case "DateOfBirth": this.str.DateOfBirth = (string)value; break;
                        case "Sex": this.str.Sex = (string)value; break;
                        case "SRBloodType": this.str.SRBloodType = (string)value; break;
                        case "BloodRhesus": this.str.BloodRhesus = (string)value; break;
                        case "SREthnic": this.str.SREthnic = (string)value; break;
                        case "SREducation": this.str.SREducation = (string)value; break;
                        case "SRMaritalStatus": this.str.SRMaritalStatus = (string)value; break;
                        case "SRNationality": this.str.SRNationality = (string)value; break;
                        case "SROccupation": this.str.SROccupation = (string)value; break;
                        case "SRTitle": this.str.SRTitle = (string)value; break;
                        case "SRPatientCategory": this.str.SRPatientCategory = (string)value; break;
                        case "SRReligion": this.str.SRReligion = (string)value; break;
                        case "SRMedicalFileBin": this.str.SRMedicalFileBin = (string)value; break;
                        case "SRMedicalFileStatus": this.str.SRMedicalFileStatus = (string)value; break;
                        case "GuarantorID": this.str.GuarantorID = (string)value; break;
                        case "Company": this.str.Company = (string)value; break;
                        case "StreetName": this.str.StreetName = (string)value; break;
                        case "District": this.str.District = (string)value; break;
                        case "City": this.str.City = (string)value; break;
                        case "County": this.str.County = (string)value; break;
                        case "State": this.str.State = (string)value; break;
                        case "ZipCode": this.str.ZipCode = (string)value; break;
                        case "PhoneNo": this.str.PhoneNo = (string)value; break;
                        case "FaxNo": this.str.FaxNo = (string)value; break;
                        case "Email": this.str.Email = (string)value; break;
                        case "MobilePhoneNo": this.str.MobilePhoneNo = (string)value; break;
                        case "TempAddressStreetName": this.str.TempAddressStreetName = (string)value; break;
                        case "TempAddressDistrict": this.str.TempAddressDistrict = (string)value; break;
                        case "TempAddressCity": this.str.TempAddressCity = (string)value; break;
                        case "TempAddressCounty": this.str.TempAddressCounty = (string)value; break;
                        case "TempAddressState": this.str.TempAddressState = (string)value; break;
                        case "TempAddressZipCode": this.str.TempAddressZipCode = (string)value; break;
                        case "TempAddressPhoneNo": this.str.TempAddressPhoneNo = (string)value; break;
                        case "LastVisitDate": this.str.LastVisitDate = (string)value; break;
                        case "NumberOfVisit": this.str.NumberOfVisit = (string)value; break;
                        case "OldMedicalNo": this.str.OldMedicalNo = (string)value; break;
                        case "AccountNo": this.str.AccountNo = (string)value; break;
                        case "PictureFileName": this.str.PictureFileName = (string)value; break;
                        case "IsDonor": this.str.IsDonor = (string)value; break;
                        case "NumberOfDonor": this.str.NumberOfDonor = (string)value; break;
                        case "LastDonorDate": this.str.LastDonorDate = (string)value; break;
                        case "IsBlackList": this.str.IsBlackList = (string)value; break;
                        case "IsAlive": this.str.IsAlive = (string)value; break;
                        case "IsActive": this.str.IsActive = (string)value; break;
                        case "Notes": this.str.Notes = (string)value; break;
                        case "DiagnosticNo": this.str.DiagnosticNo = (string)value; break;
                        case "MemberID": this.str.MemberID = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "PackageBalance": this.str.PackageBalance = (string)value; break;
                        case "HealthcareID": this.str.HealthcareID = (string)value; break;
                        case "ResponTime": this.str.ResponTime = (string)value; break;
                        case "SRInformationFrom": this.str.SRInformationFrom = (string)value; break;
                        case "SRPatienRelation": this.str.SRPatienRelation = (string)value; break;
                        case "PersonID": this.str.PersonID = (string)value; break;
                        case "EmployeeNumber": this.str.EmployeeNumber = (string)value; break;
                        case "SREmployeeRelationship": this.str.SREmployeeRelationship = (string)value; break;
                        case "GuarantorCardNo": this.str.GuarantorCardNo = (string)value; break;
                        case "IsNonPatient": this.str.IsNonPatient = (string)value; break;
                        case "ParentSpouseAge": this.str.ParentSpouseAge = (string)value; break;
                        case "SRParentSpouseOccupation": this.str.SRParentSpouseOccupation = (string)value; break;
                        case "ParentSpouseOccupationDesc": this.str.ParentSpouseOccupationDesc = (string)value; break;
                        case "SRMotherOccupation": this.str.SRMotherOccupation = (string)value; break;
                        case "MotherOccupationDesc": this.str.MotherOccupationDesc = (string)value; break;
                        case "MotherName": this.str.MotherName = (string)value; break;
                        case "MotherAge": this.str.MotherAge = (string)value; break;
                        case "IsNotPaidOff": this.str.IsNotPaidOff = (string)value; break;
                        case "ParentSpouseMedicalNo": this.str.ParentSpouseMedicalNo = (string)value; break;
                        case "MotherMedicalNo": this.str.MotherMedicalNo = (string)value; break;
                        case "CompanyAddress": this.str.CompanyAddress = (string)value; break;
                        case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;
                        case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;
                        case "SRRelationshipQuality": this.str.SRRelationshipQuality = (string)value; break;
                        case "SRResidentialHome": this.str.SRResidentialHome = (string)value; break;
                        case "IsStoredToLokadok": this.str.IsStoredToLokadok = (string)value; break;
                        case "FatherName": this.str.FatherName = (string)value; break;
                        case "FatherAge": this.str.FatherAge = (string)value; break;
                        case "FatherMedicalNo": this.str.FatherMedicalNo = (string)value; break;
                        case "SRFatherOccupation": this.str.SRFatherOccupation = (string)value; break;
                        case "FatherOccupationDesc": this.str.FatherOccupationDesc = (string)value; break;
                        case "DeathCertificateNo": this.str.DeathCertificateNo = (string)value; break;
                        case "EmployeeNo": this.str.EmployeeNo = (string)value; break;
                        case "EmployeeJobTitleName": this.str.EmployeeJobTitleName = (string)value; break;
                        case "EmployeeJobDepartementName": this.str.EmployeeJobDepartementName = (string)value; break;
                        case "ValuesOfTrust": this.str.ValuesOfTrust = (string)value; break;
                        case "DeceasedDateTime": this.str.DeceasedDateTime = (string)value; break;
                        case "FamilyRegisterNo": this.str.FamilyRegisterNo = (string)value; break;
                        case "IsSyncWithDukcapil": this.str.IsSyncWithDukcapil = (string)value; break;
                        case "IsDisability": this.str.IsDisability = (string)value; break;
                        case "PassportNo": this.str.PassportNo = (string)value; break;
                        case "SRPatientLanguage": this.str.SRPatientLanguage = (string)value; break;
                        case "SpouseSsn": this.str.SpouseSsn = (string)value; break;
                        case "MotherSsn": this.str.MotherSsn = (string)value; break;
                        case "FatherSsn": this.str.FatherSsn = (string)value; break;
                        case "ReverseMedicalNo": this.str.ReverseMedicalNo = (string)value; break;
                        case "ReverseOldMedicalNo": this.str.ReverseOldMedicalNo = (string)value; break;
                        case "FullName": this.str.FullName = (string)value; break;
                        case "IsKYC": this.str.IsKYC = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "DateOfBirth":

                            if (value == null || value is System.DateTime)
                                this.DateOfBirth = (System.DateTime?)value;
                            break;
                        case "LastVisitDate":

                            if (value == null || value is System.DateTime)
                                this.LastVisitDate = (System.DateTime?)value;
                            break;
                        case "NumberOfVisit":

                            if (value == null || value is System.Byte)
                                this.NumberOfVisit = (System.Byte?)value;
                            break;
                        case "IsDonor":

                            if (value == null || value is System.Boolean)
                                this.IsDonor = (System.Boolean?)value;
                            break;
                        case "NumberOfDonor":

                            if (value == null || value is System.Decimal)
                                this.NumberOfDonor = (System.Decimal?)value;
                            break;
                        case "LastDonorDate":

                            if (value == null || value is System.DateTime)
                                this.LastDonorDate = (System.DateTime?)value;
                            break;
                        case "IsBlackList":

                            if (value == null || value is System.Boolean)
                                this.IsBlackList = (System.Boolean?)value;
                            break;
                        case "IsAlive":

                            if (value == null || value is System.Boolean)
                                this.IsAlive = (System.Boolean?)value;
                            break;
                        case "IsActive":

                            if (value == null || value is System.Boolean)
                                this.IsActive = (System.Boolean?)value;
                            break;
                        case "LastUpdateDateTime":

                            if (value == null || value is System.DateTime)
                                this.LastUpdateDateTime = (System.DateTime?)value;
                            break;
                        case "PackageBalance":

                            if (value == null || value is System.Decimal)
                                this.PackageBalance = (System.Decimal?)value;
                            break;
                        case "ResponTime":

                            if (value == null || value is System.TimeSpan)
                                this.ResponTime = (System.TimeSpan?)value;
                            break;
                        case "PersonID":

                            if (value == null || value is System.Int32)
                                this.PersonID = (System.Int32?)value;
                            break;
                        case "IsNonPatient":

                            if (value == null || value is System.Boolean)
                                this.IsNonPatient = (System.Boolean?)value;
                            break;
                        case "ParentSpouseAge":

                            if (value == null || value is System.Int16)
                                this.ParentSpouseAge = (System.Int16?)value;
                            break;
                        case "MotherAge":

                            if (value == null || value is System.Int16)
                                this.MotherAge = (System.Int16?)value;
                            break;
                        case "IsNotPaidOff":

                            if (value == null || value is System.Boolean)
                                this.IsNotPaidOff = (System.Boolean?)value;
                            break;
                        case "CreatedDateTime":

                            if (value == null || value is System.DateTime)
                                this.CreatedDateTime = (System.DateTime?)value;
                            break;
                        case "IsStoredToLokadok":

                            if (value == null || value is System.Boolean)
                                this.IsStoredToLokadok = (System.Boolean?)value;
                            break;
                        case "FatherAge":

                            if (value == null || value is System.Int16)
                                this.FatherAge = (System.Int16?)value;
                            break;
                        case "DeceasedDateTime":

                            if (value == null || value is System.DateTime)
                                this.DeceasedDateTime = (System.DateTime?)value;
                            break;
                        case "IsSyncWithDukcapil":

                            if (value == null || value is System.Boolean)
                                this.IsSyncWithDukcapil = (System.Boolean?)value;
                            break;
                        case "IsDisability":

                            if (value == null || value is System.Boolean)
                                this.IsDisability = (System.Boolean?)value;
                            break;
                        case "IsKYC":

                            if (value == null || value is System.Boolean)
                                this.IsKYC = (System.Boolean?)value;
                            break;

                        default:
                            break;
                    }
                }
            }
            else if (this.Row.Table.Columns.Contains(name))
            {
                this.Row[name] = value;
            }
            else
            {
                throw new Exception("SetProperty Error: '" + name + "' not found");
            }
        }

        /// <summary>
        /// Maps to Patient.PatientID
        /// </summary>
        virtual public System.String PatientID
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.PatientID);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.PatientID, value);
            }
        }
        /// <summary>
        /// Maps to Patient.MedicalNo
        /// </summary>
        virtual public System.String MedicalNo
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.MedicalNo);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.MedicalNo, value);
            }
        }
        /// <summary>
        /// Maps to Patient.Ssn
        /// </summary>
        virtual public System.String Ssn
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.Ssn);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.Ssn, value);
            }
        }
        /// <summary>
        /// Maps to Patient.SRSalutation
        /// </summary>
        virtual public System.String SRSalutation
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.SRSalutation);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.SRSalutation, value);
            }
        }
        /// <summary>
        /// Maps to Patient.FirstName
        /// </summary>
        virtual public System.String FirstName
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.FirstName);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.FirstName, value);
            }
        }
        /// <summary>
        /// Maps to Patient.MiddleName
        /// </summary>
        virtual public System.String MiddleName
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.MiddleName);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.MiddleName, value);
            }
        }
        /// <summary>
        /// Maps to Patient.LastName
        /// </summary>
        virtual public System.String LastName
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.LastName);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.LastName, value);
            }
        }
        /// <summary>
        /// Maps to Patient.ParentSpouseName
        /// </summary>
        virtual public System.String ParentSpouseName
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.ParentSpouseName);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.ParentSpouseName, value);
            }
        }
        /// <summary>
        /// Maps to Patient.CityOfBirth
        /// </summary>
        virtual public System.String CityOfBirth
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.CityOfBirth);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.CityOfBirth, value);
            }
        }
        /// <summary>
        /// Maps to Patient.DateOfBirth
        /// </summary>
        virtual public System.DateTime? DateOfBirth
        {
            get
            {
                return base.GetSystemDateTime(PatientMetadata.ColumnNames.DateOfBirth);
            }

            set
            {
                base.SetSystemDateTime(PatientMetadata.ColumnNames.DateOfBirth, value);
            }
        }
        /// <summary>
        /// Maps to Patient.Sex
        /// </summary>
        virtual public System.String Sex
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.Sex);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.Sex, value);
            }
        }
        /// <summary>
        /// Maps to Patient.SRBloodType
        /// </summary>
        virtual public System.String SRBloodType
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.SRBloodType);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.SRBloodType, value);
            }
        }
        /// <summary>
        /// Maps to Patient.BloodRhesus
        /// </summary>
        virtual public System.String BloodRhesus
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.BloodRhesus);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.BloodRhesus, value);
            }
        }
        /// <summary>
        /// Maps to Patient.SREthnic
        /// </summary>
        virtual public System.String SREthnic
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.SREthnic);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.SREthnic, value);
            }
        }
        /// <summary>
        /// Maps to Patient.SREducation
        /// </summary>
        virtual public System.String SREducation
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.SREducation);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.SREducation, value);
            }
        }
        /// <summary>
        /// Maps to Patient.SRMaritalStatus
        /// </summary>
        virtual public System.String SRMaritalStatus
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.SRMaritalStatus);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.SRMaritalStatus, value);
            }
        }
        /// <summary>
        /// Maps to Patient.SRNationality
        /// </summary>
        virtual public System.String SRNationality
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.SRNationality);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.SRNationality, value);
            }
        }
        /// <summary>
        /// Maps to Patient.SROccupation
        /// </summary>
        virtual public System.String SROccupation
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.SROccupation);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.SROccupation, value);
            }
        }
        /// <summary>
        /// Maps to Patient.SRTitle
        /// </summary>
        virtual public System.String SRTitle
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.SRTitle);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.SRTitle, value);
            }
        }
        /// <summary>
        /// Maps to Patient.SRPatientCategory
        /// </summary>
        virtual public System.String SRPatientCategory
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.SRPatientCategory);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.SRPatientCategory, value);
            }
        }
        /// <summary>
        /// Maps to Patient.SRReligion
        /// </summary>
        virtual public System.String SRReligion
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.SRReligion);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.SRReligion, value);
            }
        }
        /// <summary>
        /// Maps to Patient.SRMedicalFileBin
        /// </summary>
        virtual public System.String SRMedicalFileBin
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.SRMedicalFileBin);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.SRMedicalFileBin, value);
            }
        }
        /// <summary>
        /// Maps to Patient.SRMedicalFileStatus
        /// </summary>
        virtual public System.String SRMedicalFileStatus
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.SRMedicalFileStatus);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.SRMedicalFileStatus, value);
            }
        }
        /// <summary>
        /// Maps to Patient.GuarantorID
        /// </summary>
        virtual public System.String GuarantorID
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.GuarantorID);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.GuarantorID, value);
            }
        }
        /// <summary>
        /// Maps to Patient.Company
        /// </summary>
        virtual public System.String Company
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.Company);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.Company, value);
            }
        }
        /// <summary>
        /// Maps to Patient.StreetName
        /// </summary>
        virtual public System.String StreetName
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.StreetName);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.StreetName, value);
            }
        }
        /// <summary>
        /// Maps to Patient.District
        /// </summary>
        virtual public System.String District
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.District);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.District, value);
            }
        }
        /// <summary>
        /// Maps to Patient.City
        /// </summary>
        virtual public System.String City
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.City);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.City, value);
            }
        }
        /// <summary>
        /// Maps to Patient.County
        /// </summary>
        virtual public System.String County
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.County);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.County, value);
            }
        }
        /// <summary>
        /// Maps to Patient.State
        /// </summary>
        virtual public System.String State
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.State);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.State, value);
            }
        }
        /// <summary>
        /// Maps to Patient.ZipCode
        /// </summary>
        virtual public System.String ZipCode
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.ZipCode);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.ZipCode, value);
            }
        }
        /// <summary>
        /// Maps to Patient.PhoneNo
        /// </summary>
        virtual public System.String PhoneNo
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.PhoneNo);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.PhoneNo, value);
            }
        }
        /// <summary>
        /// Maps to Patient.FaxNo
        /// </summary>
        virtual public System.String FaxNo
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.FaxNo);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.FaxNo, value);
            }
        }
        /// <summary>
        /// Maps to Patient.Email
        /// </summary>
        virtual public System.String Email
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.Email);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.Email, value);
            }
        }
        /// <summary>
        /// Maps to Patient.MobilePhoneNo
        /// </summary>
        virtual public System.String MobilePhoneNo
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.MobilePhoneNo);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.MobilePhoneNo, value);
            }
        }
        /// <summary>
        /// Maps to Patient.TempAddressStreetName
        /// </summary>
        virtual public System.String TempAddressStreetName
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.TempAddressStreetName);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.TempAddressStreetName, value);
            }
        }
        /// <summary>
        /// Maps to Patient.TempAddressDistrict
        /// </summary>
        virtual public System.String TempAddressDistrict
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.TempAddressDistrict);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.TempAddressDistrict, value);
            }
        }
        /// <summary>
        /// Maps to Patient.TempAddressCity
        /// </summary>
        virtual public System.String TempAddressCity
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.TempAddressCity);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.TempAddressCity, value);
            }
        }
        /// <summary>
        /// Maps to Patient.TempAddressCounty
        /// </summary>
        virtual public System.String TempAddressCounty
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.TempAddressCounty);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.TempAddressCounty, value);
            }
        }
        /// <summary>
        /// Maps to Patient.TempAddressState
        /// </summary>
        virtual public System.String TempAddressState
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.TempAddressState);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.TempAddressState, value);
            }
        }
        /// <summary>
        /// Maps to Patient.TempAddressZipCode
        /// </summary>
        virtual public System.String TempAddressZipCode
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.TempAddressZipCode);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.TempAddressZipCode, value);
            }
        }
        /// <summary>
        /// Maps to Patient.TempAddressPhoneNo
        /// </summary>
        virtual public System.String TempAddressPhoneNo
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.TempAddressPhoneNo);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.TempAddressPhoneNo, value);
            }
        }
        /// <summary>
        /// Maps to Patient.LastVisitDate
        /// </summary>
        virtual public System.DateTime? LastVisitDate
        {
            get
            {
                return base.GetSystemDateTime(PatientMetadata.ColumnNames.LastVisitDate);
            }

            set
            {
                base.SetSystemDateTime(PatientMetadata.ColumnNames.LastVisitDate, value);
            }
        }
        /// <summary>
        /// Maps to Patient.NumberOfVisit
        /// </summary>
        virtual public System.Byte? NumberOfVisit
        {
            get
            {
                return base.GetSystemByte(PatientMetadata.ColumnNames.NumberOfVisit);
            }

            set
            {
                base.SetSystemByte(PatientMetadata.ColumnNames.NumberOfVisit, value);
            }
        }
        /// <summary>
        /// Maps to Patient.OldMedicalNo
        /// </summary>
        virtual public System.String OldMedicalNo
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.OldMedicalNo);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.OldMedicalNo, value);
            }
        }
        /// <summary>
        /// Maps to Patient.AccountNo
        /// </summary>
        virtual public System.String AccountNo
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.AccountNo);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.AccountNo, value);
            }
        }
        /// <summary>
        /// Maps to Patient.PictureFileName
        /// </summary>
        virtual public System.String PictureFileName
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.PictureFileName);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.PictureFileName, value);
            }
        }
        /// <summary>
        /// Maps to Patient.IsDonor
        /// </summary>
        virtual public System.Boolean? IsDonor
        {
            get
            {
                return base.GetSystemBoolean(PatientMetadata.ColumnNames.IsDonor);
            }

            set
            {
                base.SetSystemBoolean(PatientMetadata.ColumnNames.IsDonor, value);
            }
        }
        /// <summary>
        /// Maps to Patient.NumberOfDonor
        /// </summary>
        virtual public System.Decimal? NumberOfDonor
        {
            get
            {
                return base.GetSystemDecimal(PatientMetadata.ColumnNames.NumberOfDonor);
            }

            set
            {
                base.SetSystemDecimal(PatientMetadata.ColumnNames.NumberOfDonor, value);
            }
        }
        /// <summary>
        /// Maps to Patient.LastDonorDate
        /// </summary>
        virtual public System.DateTime? LastDonorDate
        {
            get
            {
                return base.GetSystemDateTime(PatientMetadata.ColumnNames.LastDonorDate);
            }

            set
            {
                base.SetSystemDateTime(PatientMetadata.ColumnNames.LastDonorDate, value);
            }
        }
        /// <summary>
        /// Maps to Patient.IsBlackList
        /// </summary>
        virtual public System.Boolean? IsBlackList
        {
            get
            {
                return base.GetSystemBoolean(PatientMetadata.ColumnNames.IsBlackList);
            }

            set
            {
                base.SetSystemBoolean(PatientMetadata.ColumnNames.IsBlackList, value);
            }
        }
        /// <summary>
        /// Maps to Patient.IsAlive
        /// </summary>
        virtual public System.Boolean? IsAlive
        {
            get
            {
                return base.GetSystemBoolean(PatientMetadata.ColumnNames.IsAlive);
            }

            set
            {
                base.SetSystemBoolean(PatientMetadata.ColumnNames.IsAlive, value);
            }
        }
        /// <summary>
        /// Maps to Patient.IsActive
        /// </summary>
        virtual public System.Boolean? IsActive
        {
            get
            {
                return base.GetSystemBoolean(PatientMetadata.ColumnNames.IsActive);
            }

            set
            {
                base.SetSystemBoolean(PatientMetadata.ColumnNames.IsActive, value);
            }
        }
        /// <summary>
        /// Maps to Patient.Notes
        /// </summary>
        virtual public System.String Notes
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.Notes);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.Notes, value);
            }
        }
        /// <summary>
        /// Maps to Patient.DiagnosticNo
        /// </summary>
        virtual public System.String DiagnosticNo
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.DiagnosticNo);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.DiagnosticNo, value);
            }
        }
        /// <summary>
        /// Maps to Patient.MemberID
        /// </summary>
        virtual public System.String MemberID
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.MemberID);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.MemberID, value);
            }
        }
        /// <summary>
        /// Maps to Patient.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(PatientMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(PatientMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to Patient.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to Patient.PackageBalance
        /// </summary>
        virtual public System.Decimal? PackageBalance
        {
            get
            {
                return base.GetSystemDecimal(PatientMetadata.ColumnNames.PackageBalance);
            }

            set
            {
                base.SetSystemDecimal(PatientMetadata.ColumnNames.PackageBalance, value);
            }
        }
        /// <summary>
        /// Maps to Patient.HealthcareID
        /// </summary>
        virtual public System.String HealthcareID
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.HealthcareID);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.HealthcareID, value);
            }
        }
        /// <summary>
        /// Maps to Patient.ResponTime
        /// </summary>
        virtual public System.TimeSpan? ResponTime
        {
            get
            {
                return base.GetSystemTimeSpan(PatientMetadata.ColumnNames.ResponTime);
            }

            set
            {
                base.SetSystemTimeSpan(PatientMetadata.ColumnNames.ResponTime, value);
            }
        }
        /// <summary>
        /// Maps to Patient.SRInformationFrom
        /// </summary>
        virtual public System.String SRInformationFrom
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.SRInformationFrom);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.SRInformationFrom, value);
            }
        }
        /// <summary>
        /// Maps to Patient.SRPatienRelation
        /// </summary>
        virtual public System.String SRPatienRelation
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.SRPatienRelation);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.SRPatienRelation, value);
            }
        }
        /// <summary>
        /// Maps to Patient.PersonID
        /// </summary>
        virtual public System.Int32? PersonID
        {
            get
            {
                return base.GetSystemInt32(PatientMetadata.ColumnNames.PersonID);
            }

            set
            {
                base.SetSystemInt32(PatientMetadata.ColumnNames.PersonID, value);
            }
        }
        /// <summary>
        /// Maps to Patient.EmployeeNumber
        /// </summary>
        virtual public System.String EmployeeNumber
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.EmployeeNumber);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.EmployeeNumber, value);
            }
        }
        /// <summary>
        /// Maps to Patient.SREmployeeRelationship
        /// </summary>
        virtual public System.String SREmployeeRelationship
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.SREmployeeRelationship);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.SREmployeeRelationship, value);
            }
        }
        /// <summary>
        /// Maps to Patient.GuarantorCardNo
        /// </summary>
        virtual public System.String GuarantorCardNo
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.GuarantorCardNo);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.GuarantorCardNo, value);
            }
        }
        /// <summary>
        /// Maps to Patient.IsNonPatient
        /// </summary>
        virtual public System.Boolean? IsNonPatient
        {
            get
            {
                return base.GetSystemBoolean(PatientMetadata.ColumnNames.IsNonPatient);
            }

            set
            {
                base.SetSystemBoolean(PatientMetadata.ColumnNames.IsNonPatient, value);
            }
        }
        /// <summary>
        /// Maps to Patient.ParentSpouseAge
        /// </summary>
        virtual public System.Int16? ParentSpouseAge
        {
            get
            {
                return base.GetSystemInt16(PatientMetadata.ColumnNames.ParentSpouseAge);
            }

            set
            {
                base.SetSystemInt16(PatientMetadata.ColumnNames.ParentSpouseAge, value);
            }
        }
        /// <summary>
        /// Maps to Patient.SRParentSpouseOccupation
        /// </summary>
        virtual public System.String SRParentSpouseOccupation
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.SRParentSpouseOccupation);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.SRParentSpouseOccupation, value);
            }
        }
        /// <summary>
        /// Maps to Patient.ParentSpouseOccupationDesc
        /// </summary>
        virtual public System.String ParentSpouseOccupationDesc
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.ParentSpouseOccupationDesc);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.ParentSpouseOccupationDesc, value);
            }
        }
        /// <summary>
        /// Maps to Patient.SRMotherOccupation
        /// </summary>
        virtual public System.String SRMotherOccupation
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.SRMotherOccupation);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.SRMotherOccupation, value);
            }
        }
        /// <summary>
        /// Maps to Patient.MotherOccupationDesc
        /// </summary>
        virtual public System.String MotherOccupationDesc
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.MotherOccupationDesc);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.MotherOccupationDesc, value);
            }
        }
        /// <summary>
        /// Maps to Patient.MotherName
        /// </summary>
        virtual public System.String MotherName
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.MotherName);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.MotherName, value);
            }
        }
        /// <summary>
        /// Maps to Patient.MotherAge
        /// </summary>
        virtual public System.Int16? MotherAge
        {
            get
            {
                return base.GetSystemInt16(PatientMetadata.ColumnNames.MotherAge);
            }

            set
            {
                base.SetSystemInt16(PatientMetadata.ColumnNames.MotherAge, value);
            }
        }
        /// <summary>
        /// Maps to Patient.IsNotPaidOff
        /// </summary>
        virtual public System.Boolean? IsNotPaidOff
        {
            get
            {
                return base.GetSystemBoolean(PatientMetadata.ColumnNames.IsNotPaidOff);
            }

            set
            {
                base.SetSystemBoolean(PatientMetadata.ColumnNames.IsNotPaidOff, value);
            }
        }
        /// <summary>
        /// Maps to Patient.ParentSpouseMedicalNo
        /// </summary>
        virtual public System.String ParentSpouseMedicalNo
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.ParentSpouseMedicalNo);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.ParentSpouseMedicalNo, value);
            }
        }
        /// <summary>
        /// Maps to Patient.MotherMedicalNo
        /// </summary>
        virtual public System.String MotherMedicalNo
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.MotherMedicalNo);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.MotherMedicalNo, value);
            }
        }
        /// <summary>
        /// Maps to Patient.CompanyAddress
        /// </summary>
        virtual public System.String CompanyAddress
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.CompanyAddress);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.CompanyAddress, value);
            }
        }
        /// <summary>
        /// Maps to Patient.CreatedByUserID
        /// </summary>
        virtual public System.String CreatedByUserID
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.CreatedByUserID);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.CreatedByUserID, value);
            }
        }
        /// <summary>
        /// Maps to Patient.CreatedDateTime
        /// </summary>
        virtual public System.DateTime? CreatedDateTime
        {
            get
            {
                return base.GetSystemDateTime(PatientMetadata.ColumnNames.CreatedDateTime);
            }

            set
            {
                base.SetSystemDateTime(PatientMetadata.ColumnNames.CreatedDateTime, value);
            }
        }
        /// <summary>
        /// Maps to Patient.SRRelationshipQuality
        /// </summary>
        virtual public System.String SRRelationshipQuality
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.SRRelationshipQuality);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.SRRelationshipQuality, value);
            }
        }
        /// <summary>
        /// Maps to Patient.SRResidentialHome
        /// </summary>
        virtual public System.String SRResidentialHome
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.SRResidentialHome);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.SRResidentialHome, value);
            }
        }
        /// <summary>
        /// Maps to Patient.IsStoredToLokadok
        /// </summary>
        virtual public System.Boolean? IsStoredToLokadok
        {
            get
            {
                return base.GetSystemBoolean(PatientMetadata.ColumnNames.IsStoredToLokadok);
            }

            set
            {
                base.SetSystemBoolean(PatientMetadata.ColumnNames.IsStoredToLokadok, value);
            }
        }
        /// <summary>
        /// Maps to Patient.FatherName
        /// </summary>
        virtual public System.String FatherName
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.FatherName);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.FatherName, value);
            }
        }
        /// <summary>
        /// Maps to Patient.FatherAge
        /// </summary>
        virtual public System.Int16? FatherAge
        {
            get
            {
                return base.GetSystemInt16(PatientMetadata.ColumnNames.FatherAge);
            }

            set
            {
                base.SetSystemInt16(PatientMetadata.ColumnNames.FatherAge, value);
            }
        }
        /// <summary>
        /// Maps to Patient.FatherMedicalNo
        /// </summary>
        virtual public System.String FatherMedicalNo
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.FatherMedicalNo);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.FatherMedicalNo, value);
            }
        }
        /// <summary>
        /// Maps to Patient.SRFatherOccupation
        /// </summary>
        virtual public System.String SRFatherOccupation
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.SRFatherOccupation);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.SRFatherOccupation, value);
            }
        }
        /// <summary>
        /// Maps to Patient.FatherOccupationDesc
        /// </summary>
        virtual public System.String FatherOccupationDesc
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.FatherOccupationDesc);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.FatherOccupationDesc, value);
            }
        }
        /// <summary>
        /// Maps to Patient.DeathCertificateNo
        /// </summary>
        virtual public System.String DeathCertificateNo
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.DeathCertificateNo);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.DeathCertificateNo, value);
            }
        }
        /// <summary>
        /// Maps to Patient.EmployeeNo
        /// </summary>
        virtual public System.String EmployeeNo
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.EmployeeNo);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.EmployeeNo, value);
            }
        }
        /// <summary>
        /// Maps to Patient.EmployeeJobTitleName
        /// </summary>
        virtual public System.String EmployeeJobTitleName
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.EmployeeJobTitleName);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.EmployeeJobTitleName, value);
            }
        }
        /// <summary>
        /// Maps to Patient.EmployeeJobDepartementName
        /// </summary>
        virtual public System.String EmployeeJobDepartementName
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.EmployeeJobDepartementName);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.EmployeeJobDepartementName, value);
            }
        }
        /// <summary>
        /// Maps to Patient.ValuesOfTrust
        /// </summary>
        virtual public System.String ValuesOfTrust
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.ValuesOfTrust);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.ValuesOfTrust, value);
            }
        }
        /// <summary>
        /// Maps to Patient.DeceasedDateTime
        /// </summary>
        virtual public System.DateTime? DeceasedDateTime
        {
            get
            {
                return base.GetSystemDateTime(PatientMetadata.ColumnNames.DeceasedDateTime);
            }

            set
            {
                base.SetSystemDateTime(PatientMetadata.ColumnNames.DeceasedDateTime, value);
            }
        }
        /// <summary>
        /// Maps to Patient.FamilyRegisterNo
        /// </summary>
        virtual public System.String FamilyRegisterNo
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.FamilyRegisterNo);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.FamilyRegisterNo, value);
            }
        }
        /// <summary>
        /// Maps to Patient.IsSyncWithDukcapil
        /// </summary>
        virtual public System.Boolean? IsSyncWithDukcapil
        {
            get
            {
                return base.GetSystemBoolean(PatientMetadata.ColumnNames.IsSyncWithDukcapil);
            }

            set
            {
                base.SetSystemBoolean(PatientMetadata.ColumnNames.IsSyncWithDukcapil, value);
            }
        }
        /// <summary>
        /// Maps to Patient.IsDisability
        /// </summary>
        virtual public System.Boolean? IsDisability
        {
            get
            {
                return base.GetSystemBoolean(PatientMetadata.ColumnNames.IsDisability);
            }

            set
            {
                base.SetSystemBoolean(PatientMetadata.ColumnNames.IsDisability, value);
            }
        }
        /// <summary>
        /// Maps to Patient.PassportNo
        /// </summary>
        virtual public System.String PassportNo
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.PassportNo);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.PassportNo, value);
            }
        }
        /// <summary>
        /// Maps to Patient.SRPatientLanguage
        /// </summary>
        virtual public System.String SRPatientLanguage
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.SRPatientLanguage);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.SRPatientLanguage, value);
            }
        }
        /// <summary>
        /// Maps to Patient.SpouseSsn
        /// </summary>
        virtual public System.String SpouseSsn
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.SpouseSsn);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.SpouseSsn, value);
            }
        }
        /// <summary>
        /// Maps to Patient.MotherSsn
        /// </summary>
        virtual public System.String MotherSsn
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.MotherSsn);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.MotherSsn, value);
            }
        }
        /// <summary>
        /// Maps to Patient.FatherSsn
        /// </summary>
        virtual public System.String FatherSsn
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.FatherSsn);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.FatherSsn, value);
            }
        }
        /// <summary>
        /// Maps to Patient.ReverseMedicalNo
        /// </summary>
        virtual public System.String ReverseMedicalNo
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.ReverseMedicalNo);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.ReverseMedicalNo, value);
            }
        }
        /// <summary>
        /// Maps to Patient.ReverseOldMedicalNo
        /// </summary>
        virtual public System.String ReverseOldMedicalNo
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.ReverseOldMedicalNo);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.ReverseOldMedicalNo, value);
            }
        }
        /// <summary>
        /// Maps to Patient.FullName
        /// </summary>
        virtual public System.String FullName
        {
            get
            {
                return base.GetSystemString(PatientMetadata.ColumnNames.FullName);
            }

            set
            {
                base.SetSystemString(PatientMetadata.ColumnNames.FullName, value);
            }
        }
        /// <summary>
        /// Maps to Patient.IsKYC
        /// </summary>
        virtual public System.Boolean? IsKYC
        {
            get
            {
                return base.GetSystemBoolean(PatientMetadata.ColumnNames.IsKYC);
            }

            set
            {
                base.SetSystemBoolean(PatientMetadata.ColumnNames.IsKYC, value);
            }
        }

        #endregion

        #region String Properties

        /// <summary>
        /// Converts an entity's properties to
        /// and from strings.
        /// </summary>
        /// <remarks>
        /// The str properties Get and Set provide easy conversion
        /// between a string and a property's data type. Not all
        /// data types will get a str property.
        /// </remarks>
        /// <example>
        /// Set a datetime from a string.
        /// <code>
        /// Employees entity = new Employees();
        /// entity.LoadByPrimaryKey(10);
        /// entity.str.HireDate = "2007-01-01 00:00:00";
        /// entity.Save();
        /// </code>
        /// Get a datetime as a string.
        /// <code>
        /// Employees entity = new Employees();
        /// entity.LoadByPrimaryKey(10);
        /// string theDate = entity.str.HireDate;
        /// </code>
        /// </example>
        [BrowsableAttribute(false)]
        public esStrings str
        {
            get
            {
                if (esstrings == null)
                {
                    esstrings = new esStrings(this);
                }
                return esstrings;
            }
        }

        [Serializable]
        sealed public class esStrings
        {
            public esStrings(esPatient entity)
            {
                this.entity = entity;
            }
            public System.String PatientID
            {
                get
                {
                    System.String data = entity.PatientID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PatientID = null;
                    else entity.PatientID = Convert.ToString(value);
                }
            }
            public System.String MedicalNo
            {
                get
                {
                    System.String data = entity.MedicalNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.MedicalNo = null;
                    else entity.MedicalNo = Convert.ToString(value);
                }
            }
            public System.String Ssn
            {
                get
                {
                    System.String data = entity.Ssn;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Ssn = null;
                    else entity.Ssn = Convert.ToString(value);
                }
            }
            public System.String SRSalutation
            {
                get
                {
                    System.String data = entity.SRSalutation;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRSalutation = null;
                    else entity.SRSalutation = Convert.ToString(value);
                }
            }
            public System.String FirstName
            {
                get
                {
                    System.String data = entity.FirstName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FirstName = null;
                    else entity.FirstName = Convert.ToString(value);
                }
            }
            public System.String MiddleName
            {
                get
                {
                    System.String data = entity.MiddleName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.MiddleName = null;
                    else entity.MiddleName = Convert.ToString(value);
                }
            }
            public System.String LastName
            {
                get
                {
                    System.String data = entity.LastName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.LastName = null;
                    else entity.LastName = Convert.ToString(value);
                }
            }
            public System.String ParentSpouseName
            {
                get
                {
                    System.String data = entity.ParentSpouseName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ParentSpouseName = null;
                    else entity.ParentSpouseName = Convert.ToString(value);
                }
            }
            public System.String CityOfBirth
            {
                get
                {
                    System.String data = entity.CityOfBirth;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CityOfBirth = null;
                    else entity.CityOfBirth = Convert.ToString(value);
                }
            }
            public System.String DateOfBirth
            {
                get
                {
                    System.DateTime? data = entity.DateOfBirth;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DateOfBirth = null;
                    else entity.DateOfBirth = Convert.ToDateTime(value);
                }
            }
            public System.String Sex
            {
                get
                {
                    System.String data = entity.Sex;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Sex = null;
                    else entity.Sex = Convert.ToString(value);
                }
            }
            public System.String SRBloodType
            {
                get
                {
                    System.String data = entity.SRBloodType;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRBloodType = null;
                    else entity.SRBloodType = Convert.ToString(value);
                }
            }
            public System.String BloodRhesus
            {
                get
                {
                    System.String data = entity.BloodRhesus;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.BloodRhesus = null;
                    else entity.BloodRhesus = Convert.ToString(value);
                }
            }
            public System.String SREthnic
            {
                get
                {
                    System.String data = entity.SREthnic;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SREthnic = null;
                    else entity.SREthnic = Convert.ToString(value);
                }
            }
            public System.String SREducation
            {
                get
                {
                    System.String data = entity.SREducation;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SREducation = null;
                    else entity.SREducation = Convert.ToString(value);
                }
            }
            public System.String SRMaritalStatus
            {
                get
                {
                    System.String data = entity.SRMaritalStatus;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRMaritalStatus = null;
                    else entity.SRMaritalStatus = Convert.ToString(value);
                }
            }
            public System.String SRNationality
            {
                get
                {
                    System.String data = entity.SRNationality;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRNationality = null;
                    else entity.SRNationality = Convert.ToString(value);
                }
            }
            public System.String SROccupation
            {
                get
                {
                    System.String data = entity.SROccupation;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SROccupation = null;
                    else entity.SROccupation = Convert.ToString(value);
                }
            }
            public System.String SRTitle
            {
                get
                {
                    System.String data = entity.SRTitle;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRTitle = null;
                    else entity.SRTitle = Convert.ToString(value);
                }
            }
            public System.String SRPatientCategory
            {
                get
                {
                    System.String data = entity.SRPatientCategory;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRPatientCategory = null;
                    else entity.SRPatientCategory = Convert.ToString(value);
                }
            }
            public System.String SRReligion
            {
                get
                {
                    System.String data = entity.SRReligion;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRReligion = null;
                    else entity.SRReligion = Convert.ToString(value);
                }
            }
            public System.String SRMedicalFileBin
            {
                get
                {
                    System.String data = entity.SRMedicalFileBin;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRMedicalFileBin = null;
                    else entity.SRMedicalFileBin = Convert.ToString(value);
                }
            }
            public System.String SRMedicalFileStatus
            {
                get
                {
                    System.String data = entity.SRMedicalFileStatus;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRMedicalFileStatus = null;
                    else entity.SRMedicalFileStatus = Convert.ToString(value);
                }
            }
            public System.String GuarantorID
            {
                get
                {
                    System.String data = entity.GuarantorID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.GuarantorID = null;
                    else entity.GuarantorID = Convert.ToString(value);
                }
            }
            public System.String Company
            {
                get
                {
                    System.String data = entity.Company;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Company = null;
                    else entity.Company = Convert.ToString(value);
                }
            }
            public System.String StreetName
            {
                get
                {
                    System.String data = entity.StreetName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.StreetName = null;
                    else entity.StreetName = Convert.ToString(value);
                }
            }
            public System.String District
            {
                get
                {
                    System.String data = entity.District;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.District = null;
                    else entity.District = Convert.ToString(value);
                }
            }
            public System.String City
            {
                get
                {
                    System.String data = entity.City;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.City = null;
                    else entity.City = Convert.ToString(value);
                }
            }
            public System.String County
            {
                get
                {
                    System.String data = entity.County;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.County = null;
                    else entity.County = Convert.ToString(value);
                }
            }
            public System.String State
            {
                get
                {
                    System.String data = entity.State;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.State = null;
                    else entity.State = Convert.ToString(value);
                }
            }
            public System.String ZipCode
            {
                get
                {
                    System.String data = entity.ZipCode;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ZipCode = null;
                    else entity.ZipCode = Convert.ToString(value);
                }
            }
            public System.String PhoneNo
            {
                get
                {
                    System.String data = entity.PhoneNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PhoneNo = null;
                    else entity.PhoneNo = Convert.ToString(value);
                }
            }
            public System.String FaxNo
            {
                get
                {
                    System.String data = entity.FaxNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FaxNo = null;
                    else entity.FaxNo = Convert.ToString(value);
                }
            }
            public System.String Email
            {
                get
                {
                    System.String data = entity.Email;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Email = null;
                    else entity.Email = Convert.ToString(value);
                }
            }
            public System.String MobilePhoneNo
            {
                get
                {
                    System.String data = entity.MobilePhoneNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.MobilePhoneNo = null;
                    else entity.MobilePhoneNo = Convert.ToString(value);
                }
            }
            public System.String TempAddressStreetName
            {
                get
                {
                    System.String data = entity.TempAddressStreetName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TempAddressStreetName = null;
                    else entity.TempAddressStreetName = Convert.ToString(value);
                }
            }
            public System.String TempAddressDistrict
            {
                get
                {
                    System.String data = entity.TempAddressDistrict;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TempAddressDistrict = null;
                    else entity.TempAddressDistrict = Convert.ToString(value);
                }
            }
            public System.String TempAddressCity
            {
                get
                {
                    System.String data = entity.TempAddressCity;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TempAddressCity = null;
                    else entity.TempAddressCity = Convert.ToString(value);
                }
            }
            public System.String TempAddressCounty
            {
                get
                {
                    System.String data = entity.TempAddressCounty;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TempAddressCounty = null;
                    else entity.TempAddressCounty = Convert.ToString(value);
                }
            }
            public System.String TempAddressState
            {
                get
                {
                    System.String data = entity.TempAddressState;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TempAddressState = null;
                    else entity.TempAddressState = Convert.ToString(value);
                }
            }
            public System.String TempAddressZipCode
            {
                get
                {
                    System.String data = entity.TempAddressZipCode;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TempAddressZipCode = null;
                    else entity.TempAddressZipCode = Convert.ToString(value);
                }
            }
            public System.String TempAddressPhoneNo
            {
                get
                {
                    System.String data = entity.TempAddressPhoneNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TempAddressPhoneNo = null;
                    else entity.TempAddressPhoneNo = Convert.ToString(value);
                }
            }
            public System.String LastVisitDate
            {
                get
                {
                    System.DateTime? data = entity.LastVisitDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.LastVisitDate = null;
                    else entity.LastVisitDate = Convert.ToDateTime(value);
                }
            }
            public System.String NumberOfVisit
            {
                get
                {
                    System.Byte? data = entity.NumberOfVisit;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.NumberOfVisit = null;
                    else entity.NumberOfVisit = Convert.ToByte(value);
                }
            }
            public System.String OldMedicalNo
            {
                get
                {
                    System.String data = entity.OldMedicalNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.OldMedicalNo = null;
                    else entity.OldMedicalNo = Convert.ToString(value);
                }
            }
            public System.String AccountNo
            {
                get
                {
                    System.String data = entity.AccountNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.AccountNo = null;
                    else entity.AccountNo = Convert.ToString(value);
                }
            }
            public System.String PictureFileName
            {
                get
                {
                    System.String data = entity.PictureFileName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PictureFileName = null;
                    else entity.PictureFileName = Convert.ToString(value);
                }
            }
            public System.String IsDonor
            {
                get
                {
                    System.Boolean? data = entity.IsDonor;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsDonor = null;
                    else entity.IsDonor = Convert.ToBoolean(value);
                }
            }
            public System.String NumberOfDonor
            {
                get
                {
                    System.Decimal? data = entity.NumberOfDonor;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.NumberOfDonor = null;
                    else entity.NumberOfDonor = Convert.ToDecimal(value);
                }
            }
            public System.String LastDonorDate
            {
                get
                {
                    System.DateTime? data = entity.LastDonorDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.LastDonorDate = null;
                    else entity.LastDonorDate = Convert.ToDateTime(value);
                }
            }
            public System.String IsBlackList
            {
                get
                {
                    System.Boolean? data = entity.IsBlackList;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsBlackList = null;
                    else entity.IsBlackList = Convert.ToBoolean(value);
                }
            }
            public System.String IsAlive
            {
                get
                {
                    System.Boolean? data = entity.IsAlive;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsAlive = null;
                    else entity.IsAlive = Convert.ToBoolean(value);
                }
            }
            public System.String IsActive
            {
                get
                {
                    System.Boolean? data = entity.IsActive;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsActive = null;
                    else entity.IsActive = Convert.ToBoolean(value);
                }
            }
            public System.String Notes
            {
                get
                {
                    System.String data = entity.Notes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Notes = null;
                    else entity.Notes = Convert.ToString(value);
                }
            }
            public System.String DiagnosticNo
            {
                get
                {
                    System.String data = entity.DiagnosticNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DiagnosticNo = null;
                    else entity.DiagnosticNo = Convert.ToString(value);
                }
            }
            public System.String MemberID
            {
                get
                {
                    System.String data = entity.MemberID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.MemberID = null;
                    else entity.MemberID = Convert.ToString(value);
                }
            }
            public System.String LastUpdateDateTime
            {
                get
                {
                    System.DateTime? data = entity.LastUpdateDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.LastUpdateDateTime = null;
                    else entity.LastUpdateDateTime = Convert.ToDateTime(value);
                }
            }
            public System.String LastUpdateByUserID
            {
                get
                {
                    System.String data = entity.LastUpdateByUserID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.LastUpdateByUserID = null;
                    else entity.LastUpdateByUserID = Convert.ToString(value);
                }
            }
            public System.String PackageBalance
            {
                get
                {
                    System.Decimal? data = entity.PackageBalance;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PackageBalance = null;
                    else entity.PackageBalance = Convert.ToDecimal(value);
                }
            }
            public System.String HealthcareID
            {
                get
                {
                    System.String data = entity.HealthcareID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.HealthcareID = null;
                    else entity.HealthcareID = Convert.ToString(value);
                }
            }
            public System.String ResponTime
            {
                get
                {
                    System.TimeSpan? data = entity.ResponTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ResponTime = null;
                    else entity.ResponTime = TimeSpan.Parse(value);
                }
            }
            public System.String SRInformationFrom
            {
                get
                {
                    System.String data = entity.SRInformationFrom;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRInformationFrom = null;
                    else entity.SRInformationFrom = Convert.ToString(value);
                }
            }
            public System.String SRPatienRelation
            {
                get
                {
                    System.String data = entity.SRPatienRelation;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRPatienRelation = null;
                    else entity.SRPatienRelation = Convert.ToString(value);
                }
            }
            public System.String PersonID
            {
                get
                {
                    System.Int32? data = entity.PersonID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PersonID = null;
                    else entity.PersonID = Convert.ToInt32(value);
                }
            }
            public System.String EmployeeNumber
            {
                get
                {
                    System.String data = entity.EmployeeNumber;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.EmployeeNumber = null;
                    else entity.EmployeeNumber = Convert.ToString(value);
                }
            }
            public System.String SREmployeeRelationship
            {
                get
                {
                    System.String data = entity.SREmployeeRelationship;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SREmployeeRelationship = null;
                    else entity.SREmployeeRelationship = Convert.ToString(value);
                }
            }
            public System.String GuarantorCardNo
            {
                get
                {
                    System.String data = entity.GuarantorCardNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.GuarantorCardNo = null;
                    else entity.GuarantorCardNo = Convert.ToString(value);
                }
            }
            public System.String IsNonPatient
            {
                get
                {
                    System.Boolean? data = entity.IsNonPatient;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsNonPatient = null;
                    else entity.IsNonPatient = Convert.ToBoolean(value);
                }
            }
            public System.String ParentSpouseAge
            {
                get
                {
                    System.Int16? data = entity.ParentSpouseAge;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ParentSpouseAge = null;
                    else entity.ParentSpouseAge = Convert.ToInt16(value);
                }
            }
            public System.String SRParentSpouseOccupation
            {
                get
                {
                    System.String data = entity.SRParentSpouseOccupation;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRParentSpouseOccupation = null;
                    else entity.SRParentSpouseOccupation = Convert.ToString(value);
                }
            }
            public System.String ParentSpouseOccupationDesc
            {
                get
                {
                    System.String data = entity.ParentSpouseOccupationDesc;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ParentSpouseOccupationDesc = null;
                    else entity.ParentSpouseOccupationDesc = Convert.ToString(value);
                }
            }
            public System.String SRMotherOccupation
            {
                get
                {
                    System.String data = entity.SRMotherOccupation;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRMotherOccupation = null;
                    else entity.SRMotherOccupation = Convert.ToString(value);
                }
            }
            public System.String MotherOccupationDesc
            {
                get
                {
                    System.String data = entity.MotherOccupationDesc;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.MotherOccupationDesc = null;
                    else entity.MotherOccupationDesc = Convert.ToString(value);
                }
            }
            public System.String MotherName
            {
                get
                {
                    System.String data = entity.MotherName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.MotherName = null;
                    else entity.MotherName = Convert.ToString(value);
                }
            }
            public System.String MotherAge
            {
                get
                {
                    System.Int16? data = entity.MotherAge;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.MotherAge = null;
                    else entity.MotherAge = Convert.ToInt16(value);
                }
            }
            public System.String IsNotPaidOff
            {
                get
                {
                    System.Boolean? data = entity.IsNotPaidOff;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsNotPaidOff = null;
                    else entity.IsNotPaidOff = Convert.ToBoolean(value);
                }
            }
            public System.String ParentSpouseMedicalNo
            {
                get
                {
                    System.String data = entity.ParentSpouseMedicalNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ParentSpouseMedicalNo = null;
                    else entity.ParentSpouseMedicalNo = Convert.ToString(value);
                }
            }
            public System.String MotherMedicalNo
            {
                get
                {
                    System.String data = entity.MotherMedicalNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.MotherMedicalNo = null;
                    else entity.MotherMedicalNo = Convert.ToString(value);
                }
            }
            public System.String CompanyAddress
            {
                get
                {
                    System.String data = entity.CompanyAddress;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CompanyAddress = null;
                    else entity.CompanyAddress = Convert.ToString(value);
                }
            }
            public System.String CreatedByUserID
            {
                get
                {
                    System.String data = entity.CreatedByUserID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CreatedByUserID = null;
                    else entity.CreatedByUserID = Convert.ToString(value);
                }
            }
            public System.String CreatedDateTime
            {
                get
                {
                    System.DateTime? data = entity.CreatedDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CreatedDateTime = null;
                    else entity.CreatedDateTime = Convert.ToDateTime(value);
                }
            }
            public System.String SRRelationshipQuality
            {
                get
                {
                    System.String data = entity.SRRelationshipQuality;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRRelationshipQuality = null;
                    else entity.SRRelationshipQuality = Convert.ToString(value);
                }
            }
            public System.String SRResidentialHome
            {
                get
                {
                    System.String data = entity.SRResidentialHome;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRResidentialHome = null;
                    else entity.SRResidentialHome = Convert.ToString(value);
                }
            }
            public System.String IsStoredToLokadok
            {
                get
                {
                    System.Boolean? data = entity.IsStoredToLokadok;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsStoredToLokadok = null;
                    else entity.IsStoredToLokadok = Convert.ToBoolean(value);
                }
            }
            public System.String FatherName
            {
                get
                {
                    System.String data = entity.FatherName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FatherName = null;
                    else entity.FatherName = Convert.ToString(value);
                }
            }
            public System.String FatherAge
            {
                get
                {
                    System.Int16? data = entity.FatherAge;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FatherAge = null;
                    else entity.FatherAge = Convert.ToInt16(value);
                }
            }
            public System.String FatherMedicalNo
            {
                get
                {
                    System.String data = entity.FatherMedicalNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FatherMedicalNo = null;
                    else entity.FatherMedicalNo = Convert.ToString(value);
                }
            }
            public System.String SRFatherOccupation
            {
                get
                {
                    System.String data = entity.SRFatherOccupation;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRFatherOccupation = null;
                    else entity.SRFatherOccupation = Convert.ToString(value);
                }
            }
            public System.String FatherOccupationDesc
            {
                get
                {
                    System.String data = entity.FatherOccupationDesc;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FatherOccupationDesc = null;
                    else entity.FatherOccupationDesc = Convert.ToString(value);
                }
            }
            public System.String DeathCertificateNo
            {
                get
                {
                    System.String data = entity.DeathCertificateNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DeathCertificateNo = null;
                    else entity.DeathCertificateNo = Convert.ToString(value);
                }
            }
            public System.String EmployeeNo
            {
                get
                {
                    System.String data = entity.EmployeeNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.EmployeeNo = null;
                    else entity.EmployeeNo = Convert.ToString(value);
                }
            }
            public System.String EmployeeJobTitleName
            {
                get
                {
                    System.String data = entity.EmployeeJobTitleName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.EmployeeJobTitleName = null;
                    else entity.EmployeeJobTitleName = Convert.ToString(value);
                }
            }
            public System.String EmployeeJobDepartementName
            {
                get
                {
                    System.String data = entity.EmployeeJobDepartementName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.EmployeeJobDepartementName = null;
                    else entity.EmployeeJobDepartementName = Convert.ToString(value);
                }
            }
            public System.String ValuesOfTrust
            {
                get
                {
                    System.String data = entity.ValuesOfTrust;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ValuesOfTrust = null;
                    else entity.ValuesOfTrust = Convert.ToString(value);
                }
            }
            public System.String DeceasedDateTime
            {
                get
                {
                    System.DateTime? data = entity.DeceasedDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DeceasedDateTime = null;
                    else entity.DeceasedDateTime = Convert.ToDateTime(value);
                }
            }
            public System.String FamilyRegisterNo
            {
                get
                {
                    System.String data = entity.FamilyRegisterNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FamilyRegisterNo = null;
                    else entity.FamilyRegisterNo = Convert.ToString(value);
                }
            }
            public System.String IsSyncWithDukcapil
            {
                get
                {
                    System.Boolean? data = entity.IsSyncWithDukcapil;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsSyncWithDukcapil = null;
                    else entity.IsSyncWithDukcapil = Convert.ToBoolean(value);
                }
            }
            public System.String IsDisability
            {
                get
                {
                    System.Boolean? data = entity.IsDisability;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsDisability = null;
                    else entity.IsDisability = Convert.ToBoolean(value);
                }
            }
            public System.String PassportNo
            {
                get
                {
                    System.String data = entity.PassportNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PassportNo = null;
                    else entity.PassportNo = Convert.ToString(value);
                }
            }
            public System.String SRPatientLanguage
            {
                get
                {
                    System.String data = entity.SRPatientLanguage;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRPatientLanguage = null;
                    else entity.SRPatientLanguage = Convert.ToString(value);
                }
            }
            public System.String SpouseSsn
            {
                get
                {
                    System.String data = entity.SpouseSsn;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SpouseSsn = null;
                    else entity.SpouseSsn = Convert.ToString(value);
                }
            }
            public System.String MotherSsn
            {
                get
                {
                    System.String data = entity.MotherSsn;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.MotherSsn = null;
                    else entity.MotherSsn = Convert.ToString(value);
                }
            }
            public System.String FatherSsn
            {
                get
                {
                    System.String data = entity.FatherSsn;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FatherSsn = null;
                    else entity.FatherSsn = Convert.ToString(value);
                }
            }
            public System.String ReverseMedicalNo
            {
                get
                {
                    System.String data = entity.ReverseMedicalNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ReverseMedicalNo = null;
                    else entity.ReverseMedicalNo = Convert.ToString(value);
                }
            }
            public System.String ReverseOldMedicalNo
            {
                get
                {
                    System.String data = entity.ReverseOldMedicalNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ReverseOldMedicalNo = null;
                    else entity.ReverseOldMedicalNo = Convert.ToString(value);
                }
            }
            public System.String FullName
            {
                get
                {
                    System.String data = entity.FullName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FullName = null;
                    else entity.FullName = Convert.ToString(value);
                }
            }
            public System.String IsKYC
            {
                get
                {
                    System.Boolean? data = entity.IsKYC;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsKYC = null;
                    else entity.IsKYC = Convert.ToBoolean(value);
                }
            }
            private esPatient entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esPatientQuery query)
        {
            query.OnLoadDelegate = this.OnQueryLoaded;
            query.es2.Connection = ((IEntity)this).Connection;
        }

        [System.Diagnostics.DebuggerNonUserCode]
        protected bool OnQueryLoaded(DataTable table)
        {
            bool dataFound = this.PopulateEntity(table);

            if (this.RowCount > 1)
            {
                throw new Exception("esPatient can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class Patient : esPatient
    {
    }

    [Serializable]
    abstract public class esPatientQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return PatientMetadata.Meta();
            }
        }

        public esQueryItem PatientID
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.PatientID, esSystemType.String);
            }
        }

        public esQueryItem MedicalNo
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.MedicalNo, esSystemType.String);
            }
        }

        public esQueryItem Ssn
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.Ssn, esSystemType.String);
            }
        }

        public esQueryItem SRSalutation
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.SRSalutation, esSystemType.String);
            }
        }

        public esQueryItem FirstName
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.FirstName, esSystemType.String);
            }
        }

        public esQueryItem MiddleName
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.MiddleName, esSystemType.String);
            }
        }

        public esQueryItem LastName
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.LastName, esSystemType.String);
            }
        }

        public esQueryItem ParentSpouseName
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.ParentSpouseName, esSystemType.String);
            }
        }

        public esQueryItem CityOfBirth
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.CityOfBirth, esSystemType.String);
            }
        }

        public esQueryItem DateOfBirth
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.DateOfBirth, esSystemType.DateTime);
            }
        }

        public esQueryItem Sex
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.Sex, esSystemType.String);
            }
        }

        public esQueryItem SRBloodType
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.SRBloodType, esSystemType.String);
            }
        }

        public esQueryItem BloodRhesus
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.BloodRhesus, esSystemType.String);
            }
        }

        public esQueryItem SREthnic
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.SREthnic, esSystemType.String);
            }
        }

        public esQueryItem SREducation
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.SREducation, esSystemType.String);
            }
        }

        public esQueryItem SRMaritalStatus
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.SRMaritalStatus, esSystemType.String);
            }
        }

        public esQueryItem SRNationality
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.SRNationality, esSystemType.String);
            }
        }

        public esQueryItem SROccupation
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.SROccupation, esSystemType.String);
            }
        }

        public esQueryItem SRTitle
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.SRTitle, esSystemType.String);
            }
        }

        public esQueryItem SRPatientCategory
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.SRPatientCategory, esSystemType.String);
            }
        }

        public esQueryItem SRReligion
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.SRReligion, esSystemType.String);
            }
        }

        public esQueryItem SRMedicalFileBin
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.SRMedicalFileBin, esSystemType.String);
            }
        }

        public esQueryItem SRMedicalFileStatus
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.SRMedicalFileStatus, esSystemType.String);
            }
        }

        public esQueryItem GuarantorID
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.GuarantorID, esSystemType.String);
            }
        }

        public esQueryItem Company
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.Company, esSystemType.String);
            }
        }

        public esQueryItem StreetName
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.StreetName, esSystemType.String);
            }
        }

        public esQueryItem District
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.District, esSystemType.String);
            }
        }

        public esQueryItem City
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.City, esSystemType.String);
            }
        }

        public esQueryItem County
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.County, esSystemType.String);
            }
        }

        public esQueryItem State
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.State, esSystemType.String);
            }
        }

        public esQueryItem ZipCode
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.ZipCode, esSystemType.String);
            }
        }

        public esQueryItem PhoneNo
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.PhoneNo, esSystemType.String);
            }
        }

        public esQueryItem FaxNo
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.FaxNo, esSystemType.String);
            }
        }

        public esQueryItem Email
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.Email, esSystemType.String);
            }
        }

        public esQueryItem MobilePhoneNo
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.MobilePhoneNo, esSystemType.String);
            }
        }

        public esQueryItem TempAddressStreetName
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.TempAddressStreetName, esSystemType.String);
            }
        }

        public esQueryItem TempAddressDistrict
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.TempAddressDistrict, esSystemType.String);
            }
        }

        public esQueryItem TempAddressCity
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.TempAddressCity, esSystemType.String);
            }
        }

        public esQueryItem TempAddressCounty
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.TempAddressCounty, esSystemType.String);
            }
        }

        public esQueryItem TempAddressState
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.TempAddressState, esSystemType.String);
            }
        }

        public esQueryItem TempAddressZipCode
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.TempAddressZipCode, esSystemType.String);
            }
        }

        public esQueryItem TempAddressPhoneNo
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.TempAddressPhoneNo, esSystemType.String);
            }
        }

        public esQueryItem LastVisitDate
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.LastVisitDate, esSystemType.DateTime);
            }
        }

        public esQueryItem NumberOfVisit
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.NumberOfVisit, esSystemType.Byte);
            }
        }

        public esQueryItem OldMedicalNo
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.OldMedicalNo, esSystemType.String);
            }
        }

        public esQueryItem AccountNo
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.AccountNo, esSystemType.String);
            }
        }

        public esQueryItem PictureFileName
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.PictureFileName, esSystemType.String);
            }
        }

        public esQueryItem IsDonor
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.IsDonor, esSystemType.Boolean);
            }
        }

        public esQueryItem NumberOfDonor
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.NumberOfDonor, esSystemType.Decimal);
            }
        }

        public esQueryItem LastDonorDate
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.LastDonorDate, esSystemType.DateTime);
            }
        }

        public esQueryItem IsBlackList
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.IsBlackList, esSystemType.Boolean);
            }
        }

        public esQueryItem IsAlive
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.IsAlive, esSystemType.Boolean);
            }
        }

        public esQueryItem IsActive
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.IsActive, esSystemType.Boolean);
            }
        }

        public esQueryItem Notes
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.Notes, esSystemType.String);
            }
        }

        public esQueryItem DiagnosticNo
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.DiagnosticNo, esSystemType.String);
            }
        }

        public esQueryItem MemberID
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.MemberID, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem PackageBalance
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.PackageBalance, esSystemType.Decimal);
            }
        }

        public esQueryItem HealthcareID
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.HealthcareID, esSystemType.String);
            }
        }

        public esQueryItem ResponTime
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.ResponTime, esSystemType.TimeSpan);
            }
        }

        public esQueryItem SRInformationFrom
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.SRInformationFrom, esSystemType.String);
            }
        }

        public esQueryItem SRPatienRelation
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.SRPatienRelation, esSystemType.String);
            }
        }

        public esQueryItem PersonID
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.PersonID, esSystemType.Int32);
            }
        }

        public esQueryItem EmployeeNumber
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.EmployeeNumber, esSystemType.String);
            }
        }

        public esQueryItem SREmployeeRelationship
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.SREmployeeRelationship, esSystemType.String);
            }
        }

        public esQueryItem GuarantorCardNo
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.GuarantorCardNo, esSystemType.String);
            }
        }

        public esQueryItem IsNonPatient
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.IsNonPatient, esSystemType.Boolean);
            }
        }

        public esQueryItem ParentSpouseAge
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.ParentSpouseAge, esSystemType.Int16);
            }
        }

        public esQueryItem SRParentSpouseOccupation
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.SRParentSpouseOccupation, esSystemType.String);
            }
        }

        public esQueryItem ParentSpouseOccupationDesc
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.ParentSpouseOccupationDesc, esSystemType.String);
            }
        }

        public esQueryItem SRMotherOccupation
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.SRMotherOccupation, esSystemType.String);
            }
        }

        public esQueryItem MotherOccupationDesc
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.MotherOccupationDesc, esSystemType.String);
            }
        }

        public esQueryItem MotherName
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.MotherName, esSystemType.String);
            }
        }

        public esQueryItem MotherAge
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.MotherAge, esSystemType.Int16);
            }
        }

        public esQueryItem IsNotPaidOff
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.IsNotPaidOff, esSystemType.Boolean);
            }
        }

        public esQueryItem ParentSpouseMedicalNo
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.ParentSpouseMedicalNo, esSystemType.String);
            }
        }

        public esQueryItem MotherMedicalNo
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.MotherMedicalNo, esSystemType.String);
            }
        }

        public esQueryItem CompanyAddress
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.CompanyAddress, esSystemType.String);
            }
        }

        public esQueryItem CreatedByUserID
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
            }
        }

        public esQueryItem CreatedDateTime
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem SRRelationshipQuality
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.SRRelationshipQuality, esSystemType.String);
            }
        }

        public esQueryItem SRResidentialHome
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.SRResidentialHome, esSystemType.String);
            }
        }

        public esQueryItem IsStoredToLokadok
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.IsStoredToLokadok, esSystemType.Boolean);
            }
        }

        public esQueryItem FatherName
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.FatherName, esSystemType.String);
            }
        }

        public esQueryItem FatherAge
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.FatherAge, esSystemType.Int16);
            }
        }

        public esQueryItem FatherMedicalNo
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.FatherMedicalNo, esSystemType.String);
            }
        }

        public esQueryItem SRFatherOccupation
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.SRFatherOccupation, esSystemType.String);
            }
        }

        public esQueryItem FatherOccupationDesc
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.FatherOccupationDesc, esSystemType.String);
            }
        }

        public esQueryItem DeathCertificateNo
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.DeathCertificateNo, esSystemType.String);
            }
        }

        public esQueryItem EmployeeNo
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.EmployeeNo, esSystemType.String);
            }
        }

        public esQueryItem EmployeeJobTitleName
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.EmployeeJobTitleName, esSystemType.String);
            }
        }

        public esQueryItem EmployeeJobDepartementName
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.EmployeeJobDepartementName, esSystemType.String);
            }
        }

        public esQueryItem ValuesOfTrust
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.ValuesOfTrust, esSystemType.String);
            }
        }

        public esQueryItem DeceasedDateTime
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.DeceasedDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem FamilyRegisterNo
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.FamilyRegisterNo, esSystemType.String);
            }
        }

        public esQueryItem IsSyncWithDukcapil
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.IsSyncWithDukcapil, esSystemType.Boolean);
            }
        }

        public esQueryItem IsDisability
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.IsDisability, esSystemType.Boolean);
            }
        }

        public esQueryItem PassportNo
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.PassportNo, esSystemType.String);
            }
        }

        public esQueryItem SRPatientLanguage
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.SRPatientLanguage, esSystemType.String);
            }
        }

        public esQueryItem SpouseSsn
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.SpouseSsn, esSystemType.String);
            }
        }

        public esQueryItem MotherSsn
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.MotherSsn, esSystemType.String);
            }
        }

        public esQueryItem FatherSsn
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.FatherSsn, esSystemType.String);
            }
        }

        public esQueryItem ReverseMedicalNo
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.ReverseMedicalNo, esSystemType.String);
            }
        }

        public esQueryItem ReverseOldMedicalNo
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.ReverseOldMedicalNo, esSystemType.String);
            }
        }

        public esQueryItem FullName
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.FullName, esSystemType.String);
            }
        }

        public esQueryItem IsKYC
        {
            get
            {
                return new esQueryItem(this, PatientMetadata.ColumnNames.IsKYC, esSystemType.Boolean);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("PatientCollection")]
    public partial class PatientCollection : esPatientCollection, IEnumerable<Patient>
    {
        public PatientCollection()
        {

        }

        public static implicit operator List<Patient>(PatientCollection coll)
        {
            List<Patient> list = new List<Patient>();

            foreach (Patient emp in coll)
            {
                list.Add(emp);
            }

            return list;
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return PatientMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PatientQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new Patient(row);
        }

        override protected esEntity CreateEntity()
        {
            return new Patient();
        }

        #endregion

        [BrowsableAttribute(false)]
        public PatientQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PatientQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        /// <summary>
        /// Useful for building up conditional queries.
        /// In most cases, before loading an entity or collection,
        /// you should instantiate a new one. This method was added
        /// to handle specialized circumstances, and should not be
        /// used as a substitute for that.
        /// </summary>
        /// <remarks>
        /// This just sets obj.Query to null/Nothing.
        /// In most cases, you will 'new' your object before
        /// loading it, rather than calling this method.
        /// It only affects obj.Query.Load(), so is not useful
        /// when Joins are involved, or for many other situations.
        /// Because it clears out any obj.Query.Where clauses,
        /// it can be useful for building conditional queries on the fly.
        /// <code>
        /// public bool ReQuery(string lastName, string firstName)
        /// {
        ///     this.QueryReset();
        ///     
        ///     if(!String.IsNullOrEmpty(lastName))
        ///     {
        ///         this.Query.Where(
        ///             this.Query.LastName == lastName);
        ///     }
        ///     if(!String.IsNullOrEmpty(firstName))
        ///     {
        ///         this.Query.Where(
        ///             this.Query.FirstName == firstName);
        ///     }
        ///     
        ///     return this.Query.Load();
        /// }
        /// </code>
        /// <code lang="vbnet">
        /// Public Function ReQuery(ByVal lastName As String, _
        ///     ByVal firstName As String) As Boolean
        /// 
        ///     Me.QueryReset()
        /// 
        ///     If Not [String].IsNullOrEmpty(lastName) Then
        ///         Me.Query.Where(Me.Query.LastName = lastName)
        ///     End If
        ///     If Not [String].IsNullOrEmpty(firstName) Then
        ///         Me.Query.Where(Me.Query.FirstName = firstName)
        ///     End If
        /// 
        ///     Return Me.Query.Load()
        /// End Function
        /// </code>
        /// </remarks>
        public void QueryReset()
        {
            this.query = null;
        }

        /// <summary>
        /// Used to custom load a Join query.
        /// Returns true if at least one record was loaded.
        /// </summary>
        /// <remarks>
        /// Provides support for InnerJoin, LeftJoin,
        /// RightJoin, and FullJoin. You must provide an alias
        /// for each query when instantiating them.
        /// <code>
        /// EmployeeCollection collection = new EmployeeCollection();
        /// 
        /// EmployeeQuery emp = new EmployeeQuery("eq");
        /// CustomerQuery cust = new CustomerQuery("cq");
        /// 
        /// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName);
        /// emp.LeftJoin(cust).On(emp.EmployeeID == cust.StaffAssigned);
        /// 
        /// collection.Load(emp);
        /// </code>
        /// <code lang="vbnet">
        /// Dim collection As New EmployeeCollection()
        /// 
        /// Dim emp As New EmployeeQuery("eq")
        /// Dim cust As New CustomerQuery("cq")
        /// 
        /// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName)
        /// emp.LeftJoin(cust).On(emp.EmployeeID = cust.StaffAssigned)
        /// 
        /// collection.Load(emp)
        /// </code>
        /// </remarks>
        /// <param name="query">The query object instance name.</param>
        /// <returns>True if at least one record was loaded.</returns>
        public bool Load(PatientQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public Patient AddNew()
        {
            Patient entity = base.AddNewEntity() as Patient;

            return entity;
        }
        public Patient FindByPrimaryKey(String patientID)
        {
            return base.FindByPrimaryKey(patientID) as Patient;
        }

        #region IEnumerable< Patient> Members

        IEnumerator<Patient> IEnumerable<Patient>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as Patient;
            }
        }

        #endregion

        private PatientQuery query;
    }


    /// <summary>
    /// Encapsulates the 'Patient' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("Patient ({PatientID})")]
    [Serializable]
    public partial class Patient : esPatient
    {
        public Patient()
        {
        }

        public Patient(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return PatientMetadata.Meta();
            }
        }

        override protected esPatientQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PatientQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public PatientQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PatientQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        /// <summary>
        /// Useful for building up conditional queries.
        /// In most cases, before loading an entity or collection,
        /// you should instantiate a new one. This method was added
        /// to handle specialized circumstances, and should not be
        /// used as a substitute for that.
        /// </summary>
        /// <remarks>
        /// This just sets obj.Query to null/Nothing.
        /// In most cases, you will 'new' your object before
        /// loading it, rather than calling this method.
        /// It only affects obj.Query.Load(), so is not useful
        /// when Joins are involved, or for many other situations.
        /// Because it clears out any obj.Query.Where clauses,
        /// it can be useful for building conditional queries on the fly.
        /// <code>
        /// public bool ReQuery(string lastName, string firstName)
        /// {
        ///     this.QueryReset();
        ///     
        ///     if(!String.IsNullOrEmpty(lastName))
        ///     {
        ///         this.Query.Where(
        ///             this.Query.LastName == lastName);
        ///     }
        ///     if(!String.IsNullOrEmpty(firstName))
        ///     {
        ///         this.Query.Where(
        ///             this.Query.FirstName == firstName);
        ///     }
        ///     
        ///     return this.Query.Load();
        /// }
        /// </code>
        /// <code lang="vbnet">
        /// Public Function ReQuery(ByVal lastName As String, _
        ///     ByVal firstName As String) As Boolean
        /// 
        ///     Me.QueryReset()
        /// 
        ///     If Not [String].IsNullOrEmpty(lastName) Then
        ///         Me.Query.Where(Me.Query.LastName = lastName)
        ///     End If
        ///     If Not [String].IsNullOrEmpty(firstName) Then
        ///         Me.Query.Where(Me.Query.FirstName = firstName)
        ///     End If
        /// 
        ///     Return Me.Query.Load()
        /// End Function
        /// </code>
        /// </remarks>
        public void QueryReset()
        {
            this.query = null;
        }

        /// <summary>
        /// Used to custom load a Join query.
        /// Returns true if at least one row is loaded.
        /// For an entity, an exception will be thrown
        /// if more than one row is loaded.
        /// </summary>
        /// <remarks>
        /// Provides support for InnerJoin, LeftJoin,
        /// RightJoin, and FullJoin. You must provide an alias
        /// for each query when instantiating them.
        /// <code>
        /// EmployeeCollection collection = new EmployeeCollection();
        /// 
        /// EmployeeQuery emp = new EmployeeQuery("eq");
        /// CustomerQuery cust = new CustomerQuery("cq");
        /// 
        /// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName);
        /// emp.LeftJoin(cust).On(emp.EmployeeID == cust.StaffAssigned);
        /// 
        /// collection.Load(emp);
        /// </code>
        /// <code lang="vbnet">
        /// Dim collection As New EmployeeCollection()
        /// 
        /// Dim emp As New EmployeeQuery("eq")
        /// Dim cust As New CustomerQuery("cq")
        /// 
        /// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName)
        /// emp.LeftJoin(cust).On(emp.EmployeeID = cust.StaffAssigned)
        /// 
        /// collection.Load(emp)
        /// </code>
        /// </remarks>
        /// <param name="query">The query object instance name.</param>
        /// <returns>True if at least one record was loaded.</returns>
        public bool Load(PatientQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private PatientQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class PatientQuery : esPatientQuery
    {
        public PatientQuery()
        {

        }

        public PatientQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "PatientQuery";
        }
    }

    [Serializable]
    public partial class PatientMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected PatientMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(PatientMetadata.ColumnNames.PatientID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.PatientID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 15;
            c.HasDefault = true;
            c.Default = @"(' ')";
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.MedicalNo, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.MedicalNo;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.Ssn, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.Ssn;
            c.CharacterMaxLength = 50;
            c.HasDefault = true;
            c.Default = @"(' ')";
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.SRSalutation, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.SRSalutation;
            c.CharacterMaxLength = 20;
            c.HasDefault = true;
            c.Default = @"(' ')";
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.FirstName, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.FirstName;
            c.CharacterMaxLength = 50;
            c.HasDefault = true;
            c.Default = @"(' ')";
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.MiddleName, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.MiddleName;
            c.CharacterMaxLength = 50;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.LastName, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.LastName;
            c.CharacterMaxLength = 50;
            c.HasDefault = true;
            c.Default = @"(' ')";
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.ParentSpouseName, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.ParentSpouseName;
            c.CharacterMaxLength = 100;
            c.HasDefault = true;
            c.Default = @"('')";
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.CityOfBirth, 8, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.CityOfBirth;
            c.CharacterMaxLength = 50;
            c.HasDefault = true;
            c.Default = @"(' ')";
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.DateOfBirth, 9, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PatientMetadata.PropertyNames.DateOfBirth;
            c.HasDefault = true;
            c.Default = @"(CONVERT([smalldatetime],'19000101',(0)))";
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.Sex, 10, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.Sex;
            c.CharacterMaxLength = 1;
            c.HasDefault = true;
            c.Default = @"('M')";
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.SRBloodType, 11, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.SRBloodType;
            c.CharacterMaxLength = 20;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.BloodRhesus, 12, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.BloodRhesus;
            c.CharacterMaxLength = 1;
            c.HasDefault = true;
            c.Default = @"('+')";
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.SREthnic, 13, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.SREthnic;
            c.CharacterMaxLength = 20;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.SREducation, 14, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.SREducation;
            c.CharacterMaxLength = 20;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.SRMaritalStatus, 15, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.SRMaritalStatus;
            c.CharacterMaxLength = 20;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.SRNationality, 16, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.SRNationality;
            c.CharacterMaxLength = 20;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.SROccupation, 17, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.SROccupation;
            c.CharacterMaxLength = 20;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.SRTitle, 18, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.SRTitle;
            c.CharacterMaxLength = 20;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.SRPatientCategory, 19, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.SRPatientCategory;
            c.CharacterMaxLength = 20;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.SRReligion, 20, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.SRReligion;
            c.CharacterMaxLength = 20;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.SRMedicalFileBin, 21, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.SRMedicalFileBin;
            c.CharacterMaxLength = 20;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.SRMedicalFileStatus, 22, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.SRMedicalFileStatus;
            c.CharacterMaxLength = 20;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.GuarantorID, 23, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.GuarantorID;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.Company, 24, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.Company;
            c.CharacterMaxLength = 100;
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.StreetName, 25, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.StreetName;
            c.CharacterMaxLength = 250;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.District, 26, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.District;
            c.CharacterMaxLength = 50;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.City, 27, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.City;
            c.CharacterMaxLength = 50;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.County, 28, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.County;
            c.CharacterMaxLength = 50;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.State, 29, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.State;
            c.CharacterMaxLength = 50;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.ZipCode, 30, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.ZipCode;
            c.CharacterMaxLength = 15;
            c.HasDefault = true;
            c.Default = @"('')";
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.PhoneNo, 31, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.PhoneNo;
            c.CharacterMaxLength = 255;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.FaxNo, 32, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.FaxNo;
            c.CharacterMaxLength = 50;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.Email, 33, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.Email;
            c.CharacterMaxLength = 50;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.MobilePhoneNo, 34, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.MobilePhoneNo;
            c.CharacterMaxLength = 255;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.TempAddressStreetName, 35, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.TempAddressStreetName;
            c.CharacterMaxLength = 250;
            c.HasDefault = true;
            c.Default = @"('')";
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.TempAddressDistrict, 36, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.TempAddressDistrict;
            c.CharacterMaxLength = 50;
            c.HasDefault = true;
            c.Default = @"('')";
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.TempAddressCity, 37, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.TempAddressCity;
            c.CharacterMaxLength = 50;
            c.HasDefault = true;
            c.Default = @"('')";
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.TempAddressCounty, 38, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.TempAddressCounty;
            c.CharacterMaxLength = 50;
            c.HasDefault = true;
            c.Default = @"('')";
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.TempAddressState, 39, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.TempAddressState;
            c.CharacterMaxLength = 50;
            c.HasDefault = true;
            c.Default = @"('')";
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.TempAddressZipCode, 40, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.TempAddressZipCode;
            c.CharacterMaxLength = 50;
            c.HasDefault = true;
            c.Default = @"('')";
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.TempAddressPhoneNo, 41, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.TempAddressPhoneNo;
            c.CharacterMaxLength = 255;
            c.HasDefault = true;
            c.Default = @"('')";
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.LastVisitDate, 42, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PatientMetadata.PropertyNames.LastVisitDate;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.NumberOfVisit, 43, typeof(System.Byte), esSystemType.Byte);
            c.PropertyName = PatientMetadata.PropertyNames.NumberOfVisit;
            c.NumericPrecision = 3;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.OldMedicalNo, 44, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.OldMedicalNo;
            c.CharacterMaxLength = 20;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.AccountNo, 45, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.AccountNo;
            c.CharacterMaxLength = 20;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.PictureFileName, 46, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.PictureFileName;
            c.CharacterMaxLength = 50;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.IsDonor, 47, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = PatientMetadata.PropertyNames.IsDonor;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.NumberOfDonor, 48, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = PatientMetadata.PropertyNames.NumberOfDonor;
            c.NumericPrecision = 3;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.LastDonorDate, 49, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PatientMetadata.PropertyNames.LastDonorDate;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.IsBlackList, 50, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = PatientMetadata.PropertyNames.IsBlackList;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.IsAlive, 51, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = PatientMetadata.PropertyNames.IsAlive;
            c.HasDefault = true;
            c.Default = @"((1))";
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.IsActive, 52, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = PatientMetadata.PropertyNames.IsActive;
            c.HasDefault = true;
            c.Default = @"((1))";
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.Notes, 53, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.Notes;
            c.CharacterMaxLength = 4000;
            c.HasDefault = true;
            c.Default = @"(' ')";
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.DiagnosticNo, 54, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.DiagnosticNo;
            c.CharacterMaxLength = 100;
            c.HasDefault = true;
            c.Default = @"('')";
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.MemberID, 55, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.MemberID;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.LastUpdateDateTime, 56, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PatientMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.LastUpdateByUserID, 57, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 40;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.PackageBalance, 58, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = PatientMetadata.PropertyNames.PackageBalance;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.HasDefault = true;
            c.Default = @"((0))";
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.HealthcareID, 59, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.HealthcareID;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.ResponTime, 60, typeof(System.TimeSpan), esSystemType.TimeSpan);
            c.PropertyName = PatientMetadata.PropertyNames.ResponTime;
            c.NumericPrecision = 8;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.SRInformationFrom, 61, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.SRInformationFrom;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.SRPatienRelation, 62, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.SRPatienRelation;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.PersonID, 63, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = PatientMetadata.PropertyNames.PersonID;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.EmployeeNumber, 64, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.EmployeeNumber;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.SREmployeeRelationship, 65, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.SREmployeeRelationship;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.GuarantorCardNo, 66, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.GuarantorCardNo;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.IsNonPatient, 67, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = PatientMetadata.PropertyNames.IsNonPatient;
            c.HasDefault = true;
            c.Default = @"((0))";
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.ParentSpouseAge, 68, typeof(System.Int16), esSystemType.Int16);
            c.PropertyName = PatientMetadata.PropertyNames.ParentSpouseAge;
            c.NumericPrecision = 5;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.SRParentSpouseOccupation, 69, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.SRParentSpouseOccupation;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.ParentSpouseOccupationDesc, 70, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.ParentSpouseOccupationDesc;
            c.CharacterMaxLength = 250;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.SRMotherOccupation, 71, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.SRMotherOccupation;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.MotherOccupationDesc, 72, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.MotherOccupationDesc;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.MotherName, 73, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.MotherName;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.MotherAge, 74, typeof(System.Int16), esSystemType.Int16);
            c.PropertyName = PatientMetadata.PropertyNames.MotherAge;
            c.NumericPrecision = 5;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.IsNotPaidOff, 75, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = PatientMetadata.PropertyNames.IsNotPaidOff;
            c.HasDefault = true;
            c.Default = @"((0))";
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.ParentSpouseMedicalNo, 76, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.ParentSpouseMedicalNo;
            c.CharacterMaxLength = 15;
            c.HasDefault = true;
            c.Default = @"('')";
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.MotherMedicalNo, 77, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.MotherMedicalNo;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.CompanyAddress, 78, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.CompanyAddress;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.CreatedByUserID, 79, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.CreatedByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.CreatedDateTime, 80, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PatientMetadata.PropertyNames.CreatedDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.SRRelationshipQuality, 81, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.SRRelationshipQuality;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.SRResidentialHome, 82, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.SRResidentialHome;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.IsStoredToLokadok, 83, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = PatientMetadata.PropertyNames.IsStoredToLokadok;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.FatherName, 84, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.FatherName;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.FatherAge, 85, typeof(System.Int16), esSystemType.Int16);
            c.PropertyName = PatientMetadata.PropertyNames.FatherAge;
            c.NumericPrecision = 5;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.FatherMedicalNo, 86, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.FatherMedicalNo;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.SRFatherOccupation, 87, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.SRFatherOccupation;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.FatherOccupationDesc, 88, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.FatherOccupationDesc;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.DeathCertificateNo, 89, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.DeathCertificateNo;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.EmployeeNo, 90, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.EmployeeNo;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.EmployeeJobTitleName, 91, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.EmployeeJobTitleName;
            c.CharacterMaxLength = 255;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.EmployeeJobDepartementName, 92, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.EmployeeJobDepartementName;
            c.CharacterMaxLength = 255;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.ValuesOfTrust, 93, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.ValuesOfTrust;
            c.CharacterMaxLength = 1000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.DeceasedDateTime, 94, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PatientMetadata.PropertyNames.DeceasedDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.FamilyRegisterNo, 95, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.FamilyRegisterNo;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.IsSyncWithDukcapil, 96, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = PatientMetadata.PropertyNames.IsSyncWithDukcapil;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.IsDisability, 97, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = PatientMetadata.PropertyNames.IsDisability;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.PassportNo, 98, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.PassportNo;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.SRPatientLanguage, 99, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.SRPatientLanguage;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.SpouseSsn, 100, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.SpouseSsn;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.MotherSsn, 101, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.MotherSsn;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.FatherSsn, 102, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.FatherSsn;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.ReverseMedicalNo, 103, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.ReverseMedicalNo;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.ReverseOldMedicalNo, 104, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.ReverseOldMedicalNo;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.FullName, 105, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientMetadata.PropertyNames.FullName;
            c.CharacterMaxLength = 152;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientMetadata.ColumnNames.IsKYC, 106, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = PatientMetadata.PropertyNames.IsKYC;
            c.HasDefault = true;
            c.Default = @"((1))";
            _columns.Add(c);


        }
        #endregion

        static public PatientMetadata Meta()
        {
            return meta;
        }

        public Guid DataID
        {
            get { return base._dataID; }
        }

        public bool MultiProviderMode
        {
            get { return false; }
        }

        public esColumnMetadataCollection Columns
        {
            get { return base._columns; }
        }

        #region ColumnNames
        public class ColumnNames
        {
            public const string PatientID = "PatientID";
            public const string MedicalNo = "MedicalNo";
            public const string Ssn = "Ssn";
            public const string SRSalutation = "SRSalutation";
            public const string FirstName = "FirstName";
            public const string MiddleName = "MiddleName";
            public const string LastName = "LastName";
            public const string ParentSpouseName = "ParentSpouseName";
            public const string CityOfBirth = "CityOfBirth";
            public const string DateOfBirth = "DateOfBirth";
            public const string Sex = "Sex";
            public const string SRBloodType = "SRBloodType";
            public const string BloodRhesus = "BloodRhesus";
            public const string SREthnic = "SREthnic";
            public const string SREducation = "SREducation";
            public const string SRMaritalStatus = "SRMaritalStatus";
            public const string SRNationality = "SRNationality";
            public const string SROccupation = "SROccupation";
            public const string SRTitle = "SRTitle";
            public const string SRPatientCategory = "SRPatientCategory";
            public const string SRReligion = "SRReligion";
            public const string SRMedicalFileBin = "SRMedicalFileBin";
            public const string SRMedicalFileStatus = "SRMedicalFileStatus";
            public const string GuarantorID = "GuarantorID";
            public const string Company = "Company";
            public const string StreetName = "StreetName";
            public const string District = "District";
            public const string City = "City";
            public const string County = "County";
            public const string State = "State";
            public const string ZipCode = "ZipCode";
            public const string PhoneNo = "PhoneNo";
            public const string FaxNo = "FaxNo";
            public const string Email = "Email";
            public const string MobilePhoneNo = "MobilePhoneNo";
            public const string TempAddressStreetName = "TempAddressStreetName";
            public const string TempAddressDistrict = "TempAddressDistrict";
            public const string TempAddressCity = "TempAddressCity";
            public const string TempAddressCounty = "TempAddressCounty";
            public const string TempAddressState = "TempAddressState";
            public const string TempAddressZipCode = "TempAddressZipCode";
            public const string TempAddressPhoneNo = "TempAddressPhoneNo";
            public const string LastVisitDate = "LastVisitDate";
            public const string NumberOfVisit = "NumberOfVisit";
            public const string OldMedicalNo = "OldMedicalNo";
            public const string AccountNo = "AccountNo";
            public const string PictureFileName = "PictureFileName";
            public const string IsDonor = "IsDonor";
            public const string NumberOfDonor = "NumberOfDonor";
            public const string LastDonorDate = "LastDonorDate";
            public const string IsBlackList = "IsBlackList";
            public const string IsAlive = "IsAlive";
            public const string IsActive = "IsActive";
            public const string Notes = "Notes";
            public const string DiagnosticNo = "DiagnosticNo";
            public const string MemberID = "MemberID";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string PackageBalance = "PackageBalance";
            public const string HealthcareID = "HealthcareID";
            public const string ResponTime = "ResponTime";
            public const string SRInformationFrom = "SRInformationFrom";
            public const string SRPatienRelation = "SRPatienRelation";
            public const string PersonID = "PersonID";
            public const string EmployeeNumber = "EmployeeNumber";
            public const string SREmployeeRelationship = "SREmployeeRelationship";
            public const string GuarantorCardNo = "GuarantorCardNo";
            public const string IsNonPatient = "IsNonPatient";
            public const string ParentSpouseAge = "ParentSpouseAge";
            public const string SRParentSpouseOccupation = "SRParentSpouseOccupation";
            public const string ParentSpouseOccupationDesc = "ParentSpouseOccupationDesc";
            public const string SRMotherOccupation = "SRMotherOccupation";
            public const string MotherOccupationDesc = "MotherOccupationDesc";
            public const string MotherName = "MotherName";
            public const string MotherAge = "MotherAge";
            public const string IsNotPaidOff = "IsNotPaidOff";
            public const string ParentSpouseMedicalNo = "ParentSpouseMedicalNo";
            public const string MotherMedicalNo = "MotherMedicalNo";
            public const string CompanyAddress = "CompanyAddress";
            public const string CreatedByUserID = "CreatedByUserID";
            public const string CreatedDateTime = "CreatedDateTime";
            public const string SRRelationshipQuality = "SRRelationshipQuality";
            public const string SRResidentialHome = "SRResidentialHome";
            public const string IsStoredToLokadok = "IsStoredToLokadok";
            public const string FatherName = "FatherName";
            public const string FatherAge = "FatherAge";
            public const string FatherMedicalNo = "FatherMedicalNo";
            public const string SRFatherOccupation = "SRFatherOccupation";
            public const string FatherOccupationDesc = "FatherOccupationDesc";
            public const string DeathCertificateNo = "DeathCertificateNo";
            public const string EmployeeNo = "EmployeeNo";
            public const string EmployeeJobTitleName = "EmployeeJobTitleName";
            public const string EmployeeJobDepartementName = "EmployeeJobDepartementName";
            public const string ValuesOfTrust = "ValuesOfTrust";
            public const string DeceasedDateTime = "DeceasedDateTime";
            public const string FamilyRegisterNo = "FamilyRegisterNo";
            public const string IsSyncWithDukcapil = "IsSyncWithDukcapil";
            public const string IsDisability = "IsDisability";
            public const string PassportNo = "PassportNo";
            public const string SRPatientLanguage = "SRPatientLanguage";
            public const string SpouseSsn = "SpouseSsn";
            public const string MotherSsn = "MotherSsn";
            public const string FatherSsn = "FatherSsn";
            public const string ReverseMedicalNo = "ReverseMedicalNo";
            public const string ReverseOldMedicalNo = "ReverseOldMedicalNo";
            public const string FullName = "FullName";
            public const string IsKYC = "IsKYC";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string PatientID = "PatientID";
            public const string MedicalNo = "MedicalNo";
            public const string Ssn = "Ssn";
            public const string SRSalutation = "SRSalutation";
            public const string FirstName = "FirstName";
            public const string MiddleName = "MiddleName";
            public const string LastName = "LastName";
            public const string ParentSpouseName = "ParentSpouseName";
            public const string CityOfBirth = "CityOfBirth";
            public const string DateOfBirth = "DateOfBirth";
            public const string Sex = "Sex";
            public const string SRBloodType = "SRBloodType";
            public const string BloodRhesus = "BloodRhesus";
            public const string SREthnic = "SREthnic";
            public const string SREducation = "SREducation";
            public const string SRMaritalStatus = "SRMaritalStatus";
            public const string SRNationality = "SRNationality";
            public const string SROccupation = "SROccupation";
            public const string SRTitle = "SRTitle";
            public const string SRPatientCategory = "SRPatientCategory";
            public const string SRReligion = "SRReligion";
            public const string SRMedicalFileBin = "SRMedicalFileBin";
            public const string SRMedicalFileStatus = "SRMedicalFileStatus";
            public const string GuarantorID = "GuarantorID";
            public const string Company = "Company";
            public const string StreetName = "StreetName";
            public const string District = "District";
            public const string City = "City";
            public const string County = "County";
            public const string State = "State";
            public const string ZipCode = "ZipCode";
            public const string PhoneNo = "PhoneNo";
            public const string FaxNo = "FaxNo";
            public const string Email = "Email";
            public const string MobilePhoneNo = "MobilePhoneNo";
            public const string TempAddressStreetName = "TempAddressStreetName";
            public const string TempAddressDistrict = "TempAddressDistrict";
            public const string TempAddressCity = "TempAddressCity";
            public const string TempAddressCounty = "TempAddressCounty";
            public const string TempAddressState = "TempAddressState";
            public const string TempAddressZipCode = "TempAddressZipCode";
            public const string TempAddressPhoneNo = "TempAddressPhoneNo";
            public const string LastVisitDate = "LastVisitDate";
            public const string NumberOfVisit = "NumberOfVisit";
            public const string OldMedicalNo = "OldMedicalNo";
            public const string AccountNo = "AccountNo";
            public const string PictureFileName = "PictureFileName";
            public const string IsDonor = "IsDonor";
            public const string NumberOfDonor = "NumberOfDonor";
            public const string LastDonorDate = "LastDonorDate";
            public const string IsBlackList = "IsBlackList";
            public const string IsAlive = "IsAlive";
            public const string IsActive = "IsActive";
            public const string Notes = "Notes";
            public const string DiagnosticNo = "DiagnosticNo";
            public const string MemberID = "MemberID";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string PackageBalance = "PackageBalance";
            public const string HealthcareID = "HealthcareID";
            public const string ResponTime = "ResponTime";
            public const string SRInformationFrom = "SRInformationFrom";
            public const string SRPatienRelation = "SRPatienRelation";
            public const string PersonID = "PersonID";
            public const string EmployeeNumber = "EmployeeNumber";
            public const string SREmployeeRelationship = "SREmployeeRelationship";
            public const string GuarantorCardNo = "GuarantorCardNo";
            public const string IsNonPatient = "IsNonPatient";
            public const string ParentSpouseAge = "ParentSpouseAge";
            public const string SRParentSpouseOccupation = "SRParentSpouseOccupation";
            public const string ParentSpouseOccupationDesc = "ParentSpouseOccupationDesc";
            public const string SRMotherOccupation = "SRMotherOccupation";
            public const string MotherOccupationDesc = "MotherOccupationDesc";
            public const string MotherName = "MotherName";
            public const string MotherAge = "MotherAge";
            public const string IsNotPaidOff = "IsNotPaidOff";
            public const string ParentSpouseMedicalNo = "ParentSpouseMedicalNo";
            public const string MotherMedicalNo = "MotherMedicalNo";
            public const string CompanyAddress = "CompanyAddress";
            public const string CreatedByUserID = "CreatedByUserID";
            public const string CreatedDateTime = "CreatedDateTime";
            public const string SRRelationshipQuality = "SRRelationshipQuality";
            public const string SRResidentialHome = "SRResidentialHome";
            public const string IsStoredToLokadok = "IsStoredToLokadok";
            public const string FatherName = "FatherName";
            public const string FatherAge = "FatherAge";
            public const string FatherMedicalNo = "FatherMedicalNo";
            public const string SRFatherOccupation = "SRFatherOccupation";
            public const string FatherOccupationDesc = "FatherOccupationDesc";
            public const string DeathCertificateNo = "DeathCertificateNo";
            public const string EmployeeNo = "EmployeeNo";
            public const string EmployeeJobTitleName = "EmployeeJobTitleName";
            public const string EmployeeJobDepartementName = "EmployeeJobDepartementName";
            public const string ValuesOfTrust = "ValuesOfTrust";
            public const string DeceasedDateTime = "DeceasedDateTime";
            public const string FamilyRegisterNo = "FamilyRegisterNo";
            public const string IsSyncWithDukcapil = "IsSyncWithDukcapil";
            public const string IsDisability = "IsDisability";
            public const string PassportNo = "PassportNo";
            public const string SRPatientLanguage = "SRPatientLanguage";
            public const string SpouseSsn = "SpouseSsn";
            public const string MotherSsn = "MotherSsn";
            public const string FatherSsn = "FatherSsn";
            public const string ReverseMedicalNo = "ReverseMedicalNo";
            public const string ReverseOldMedicalNo = "ReverseOldMedicalNo";
            public const string FullName = "FullName";
            public const string IsKYC = "IsKYC";
        }
        #endregion

        public esProviderSpecificMetadata GetProviderMetadata(string mapName)
        {
            MapToMeta mapMethod = mapDelegates[mapName];

            if (mapMethod != null)
                return mapMethod(mapName);
            else
                return null;
        }

        #region MAP esDefault

        static private int RegisterDelegateesDefault()
        {
            // This is only executed once per the life of the application
            lock (typeof(PatientMetadata))
            {
                if (PatientMetadata.mapDelegates == null)
                {
                    PatientMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (PatientMetadata.meta == null)
                {
                    PatientMetadata.meta = new PatientMetadata();
                }

                MapToMeta mapMethod = new MapToMeta(meta.esDefault);
                mapDelegates.Add("esDefault", mapMethod);
                mapMethod("esDefault");
            }
            return 0;
        }

        private esProviderSpecificMetadata esDefault(string mapName)
        {
            if (!_providerMetadataMaps.ContainsKey(mapName))
            {
                esProviderSpecificMetadata meta = new esProviderSpecificMetadata();

                meta.AddTypeMap("PatientID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("MedicalNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Ssn", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRSalutation", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("FirstName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("MiddleName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ParentSpouseName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("CityOfBirth", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("DateOfBirth", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("Sex", new esTypeMap("char", "System.String"));
                meta.AddTypeMap("SRBloodType", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("BloodRhesus", new esTypeMap("char", "System.String"));
                meta.AddTypeMap("SREthnic", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SREducation", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRMaritalStatus", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRNationality", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SROccupation", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRTitle", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRPatientCategory", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRReligion", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRMedicalFileBin", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRMedicalFileStatus", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("GuarantorID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Company", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("StreetName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("District", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("City", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("County", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("State", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ZipCode", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("PhoneNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("FaxNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Email", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("MobilePhoneNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("TempAddressStreetName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("TempAddressDistrict", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("TempAddressCity", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("TempAddressCounty", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("TempAddressState", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("TempAddressZipCode", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("TempAddressPhoneNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastVisitDate", new esTypeMap("smalldatetime", "System.DateTime"));
                meta.AddTypeMap("NumberOfVisit", new esTypeMap("tinyint", "System.Byte"));
                meta.AddTypeMap("OldMedicalNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("AccountNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("PictureFileName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsDonor", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("NumberOfDonor", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("LastDonorDate", new esTypeMap("smalldatetime", "System.DateTime"));
                meta.AddTypeMap("IsBlackList", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsAlive", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("DiagnosticNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("MemberID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("PackageBalance", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("HealthcareID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ResponTime", new esTypeMap("time", "System.TimeSpan"));
                meta.AddTypeMap("SRInformationFrom", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRPatienRelation", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("EmployeeNumber", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SREmployeeRelationship", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("GuarantorCardNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsNonPatient", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("ParentSpouseAge", new esTypeMap("smallint", "System.Int16"));
                meta.AddTypeMap("SRParentSpouseOccupation", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ParentSpouseOccupationDesc", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRMotherOccupation", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("MotherOccupationDesc", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("MotherName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("MotherAge", new esTypeMap("smallint", "System.Int16"));
                meta.AddTypeMap("IsNotPaidOff", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("ParentSpouseMedicalNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("MotherMedicalNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("CompanyAddress", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("SRRelationshipQuality", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRResidentialHome", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsStoredToLokadok", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("FatherName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("FatherAge", new esTypeMap("smallint", "System.Int16"));
                meta.AddTypeMap("FatherMedicalNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRFatherOccupation", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("FatherOccupationDesc", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("DeathCertificateNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("EmployeeNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("EmployeeJobTitleName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("EmployeeJobDepartementName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ValuesOfTrust", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("DeceasedDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("FamilyRegisterNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsSyncWithDukcapil", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsDisability", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("PassportNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRPatientLanguage", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SpouseSsn", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("MotherSsn", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("FatherSsn", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ReverseMedicalNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ReverseOldMedicalNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("FullName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsKYC", new esTypeMap("bit", "System.Boolean"));


                meta.Source = "Patient";
                meta.Destination = "Patient";
                meta.spInsert = "proc_PatientInsert";
                meta.spUpdate = "proc_PatientUpdate";
                meta.spDelete = "proc_PatientDelete";
                meta.spLoadAll = "proc_PatientLoadAll";
                meta.spLoadByPrimaryKey = "proc_PatientLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private PatientMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
