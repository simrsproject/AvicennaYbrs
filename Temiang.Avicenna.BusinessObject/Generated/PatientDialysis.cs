/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 2/28/2024 5:41:09 PM
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
    abstract public class esPatientDialysisCollection : esEntityCollectionWAuditLog
    {
        public esPatientDialysisCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "PatientDialysisCollection";
        }

        #region Query Logic
        protected void InitQuery(esPatientDialysisQuery query)
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
            this.InitQuery(query as esPatientDialysisQuery);
        }
        #endregion

        virtual public PatientDialysis DetachEntity(PatientDialysis entity)
        {
            return base.DetachEntity(entity) as PatientDialysis;
        }

        virtual public PatientDialysis AttachEntity(PatientDialysis entity)
        {
            return base.AttachEntity(entity) as PatientDialysis;
        }

        virtual public void Combine(PatientDialysisCollection collection)
        {
            base.Combine(collection);
        }

        new public PatientDialysis this[int index]
        {
            get
            {
                return base[index] as PatientDialysis;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(PatientDialysis);
        }
    }

    [Serializable]
    abstract public class esPatientDialysis : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esPatientDialysisQuery GetDynamicQuery()
        {
            return null;
        }

        public esPatientDialysis()
        {
        }

        public esPatientDialysis(DataRow row)
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
            esPatientDialysisQuery query = this.GetDynamicQuery();
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
                        case "InitialDiagnosis": this.str.InitialDiagnosis = (string)value; break;
                        case "RefferingHospital": this.str.RefferingHospital = (string)value; break;
                        case "RefferingPhysician": this.str.RefferingPhysician = (string)value; break;
                        case "Hb": this.str.Hb = (string)value; break;
                        case "HbDate": this.str.HbDate = (string)value; break;
                        case "Urea": this.str.Urea = (string)value; break;
                        case "UreaDate": this.str.UreaDate = (string)value; break;
                        case "Creatinine": this.str.Creatinine = (string)value; break;
                        case "CreatinineDate": this.str.CreatinineDate = (string)value; break;
                        case "HBsAg": this.str.HBsAg = (string)value; break;
                        case "HBsAgDate": this.str.HBsAgDate = (string)value; break;
                        case "AntiHCV": this.str.AntiHCV = (string)value; break;
                        case "AntiHCVDate": this.str.AntiHCVDate = (string)value; break;
                        case "AntiHIV": this.str.AntiHIV = (string)value; break;
                        case "AntiHIVDate": this.str.AntiHIVDate = (string)value; break;
                        case "KidneyUltrasound": this.str.KidneyUltrasound = (string)value; break;
                        case "KidneyUltrasoundDate": this.str.KidneyUltrasoundDate = (string)value; break;
                        case "ECHO": this.str.ECHO = (string)value; break;
                        case "ECHODate": this.str.ECHODate = (string)value; break;
                        case "FirstHDDate": this.str.FirstHDDate = (string)value; break;
                        case "TransferHDDate": this.str.TransferHDDate = (string)value; break;
                        case "FirstPDDate": this.str.FirstPDDate = (string)value; break;
                        case "TransferPDDate": this.str.TransferPDDate = (string)value; break;
                        case "KidneyTransplantDate": this.str.KidneyTransplantDate = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;
                        case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "hbDate":

                            if (value == null || value is System.DateTime)
                                this.HbDate = (System.DateTime?)value;
                            break;
                        case "UreaDate":

                            if (value == null || value is System.DateTime)
                                this.UreaDate = (System.DateTime?)value;
                            break;
                        case "CreatinineDate":

                            if (value == null || value is System.DateTime)
                                this.CreatinineDate = (System.DateTime?)value;
                            break;
                        case "HBsAgDate":

                            if (value == null || value is System.DateTime)
                                this.HBsAgDate = (System.DateTime?)value;
                            break;
                        case "AntiHCVDate":

                            if (value == null || value is System.DateTime)
                                this.AntiHCVDate = (System.DateTime?)value;
                            break;
                        case "AntiHIVDate":

                            if (value == null || value is System.DateTime)
                                this.AntiHIVDate = (System.DateTime?)value;
                            break;
                        case "KidneyUltrasoundDate":

                            if (value == null || value is System.DateTime)
                                this.KidneyUltrasoundDate = (System.DateTime?)value;
                            break;
                        case "ECHODate":

                            if (value == null || value is System.DateTime)
                                this.ECHODate = (System.DateTime?)value;
                            break;
                        case "FirstHDDate":

                            if (value == null || value is System.DateTime)
                                this.FirstHDDate = (System.DateTime?)value;
                            break;
                        case "TransferHDDate":

                            if (value == null || value is System.DateTime)
                                this.TransferHDDate = (System.DateTime?)value;
                            break;
                        case "FirstPDDate":

                            if (value == null || value is System.DateTime)
                                this.FirstPDDate = (System.DateTime?)value;
                            break;
                        case "TransferPDDate":

                            if (value == null || value is System.DateTime)
                                this.TransferPDDate = (System.DateTime?)value;
                            break;
                        case "KidneyTransplantDate":

                            if (value == null || value is System.DateTime)
                                this.KidneyTransplantDate = (System.DateTime?)value;
                            break;
                        case "LastUpdateDateTime":

                            if (value == null || value is System.DateTime)
                                this.LastUpdateDateTime = (System.DateTime?)value;
                            break;
                        case "CreatedDateTime":

                            if (value == null || value is System.DateTime)
                                this.CreatedDateTime = (System.DateTime?)value;
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
        /// Maps to PatientDialysis.PatientID
        /// </summary>
        virtual public System.String PatientID
        {
            get
            {
                return base.GetSystemString(PatientDialysisMetadata.ColumnNames.PatientID);
            }

            set
            {
                base.SetSystemString(PatientDialysisMetadata.ColumnNames.PatientID, value);
            }
        }
        /// <summary>
        /// Maps to PatientDialysis.InitialDiagnosis
        /// </summary>
        virtual public System.String InitialDiagnosis
        {
            get
            {
                return base.GetSystemString(PatientDialysisMetadata.ColumnNames.InitialDiagnosis);
            }

            set
            {
                base.SetSystemString(PatientDialysisMetadata.ColumnNames.InitialDiagnosis, value);
            }
        }
        /// <summary>
        /// Maps to PatientDialysis.RefferingHospital
        /// </summary>
        virtual public System.String RefferingHospital
        {
            get
            {
                return base.GetSystemString(PatientDialysisMetadata.ColumnNames.RefferingHospital);
            }

            set
            {
                base.SetSystemString(PatientDialysisMetadata.ColumnNames.RefferingHospital, value);
            }
        }
        /// <summary>
        /// Maps to PatientDialysis.RefferingPhysician
        /// </summary>
        virtual public System.String RefferingPhysician
        {
            get
            {
                return base.GetSystemString(PatientDialysisMetadata.ColumnNames.RefferingPhysician);
            }

            set
            {
                base.SetSystemString(PatientDialysisMetadata.ColumnNames.RefferingPhysician, value);
            }
        }
        /// <summary>
        /// Maps to PatientDialysis.Hb
        /// </summary>
        virtual public System.String Hb
        {
            get
            {
                return base.GetSystemString(PatientDialysisMetadata.ColumnNames.Hb);
            }

            set
            {
                base.SetSystemString(PatientDialysisMetadata.ColumnNames.Hb, value);
            }
        }
        /// <summary>
        /// Maps to PatientDialysis.hbDate
        /// </summary>
        virtual public System.DateTime? HbDate
        {
            get
            {
                return base.GetSystemDateTime(PatientDialysisMetadata.ColumnNames.HbDate);
            }

            set
            {
                base.SetSystemDateTime(PatientDialysisMetadata.ColumnNames.HbDate, value);
            }
        }
        /// <summary>
        /// Maps to PatientDialysis.Urea
        /// </summary>
        virtual public System.String Urea
        {
            get
            {
                return base.GetSystemString(PatientDialysisMetadata.ColumnNames.Urea);
            }

            set
            {
                base.SetSystemString(PatientDialysisMetadata.ColumnNames.Urea, value);
            }
        }
        /// <summary>
        /// Maps to PatientDialysis.UreaDate
        /// </summary>
        virtual public System.DateTime? UreaDate
        {
            get
            {
                return base.GetSystemDateTime(PatientDialysisMetadata.ColumnNames.UreaDate);
            }

            set
            {
                base.SetSystemDateTime(PatientDialysisMetadata.ColumnNames.UreaDate, value);
            }
        }
        /// <summary>
        /// Maps to PatientDialysis.Creatinine
        /// </summary>
        virtual public System.String Creatinine
        {
            get
            {
                return base.GetSystemString(PatientDialysisMetadata.ColumnNames.Creatinine);
            }

            set
            {
                base.SetSystemString(PatientDialysisMetadata.ColumnNames.Creatinine, value);
            }
        }
        /// <summary>
        /// Maps to PatientDialysis.CreatinineDate
        /// </summary>
        virtual public System.DateTime? CreatinineDate
        {
            get
            {
                return base.GetSystemDateTime(PatientDialysisMetadata.ColumnNames.CreatinineDate);
            }

            set
            {
                base.SetSystemDateTime(PatientDialysisMetadata.ColumnNames.CreatinineDate, value);
            }
        }
        /// <summary>
        /// Maps to PatientDialysis.HBsAg
        /// </summary>
        virtual public System.String HBsAg
        {
            get
            {
                return base.GetSystemString(PatientDialysisMetadata.ColumnNames.HBsAg);
            }

            set
            {
                base.SetSystemString(PatientDialysisMetadata.ColumnNames.HBsAg, value);
            }
        }
        /// <summary>
        /// Maps to PatientDialysis.HBsAgDate
        /// </summary>
        virtual public System.DateTime? HBsAgDate
        {
            get
            {
                return base.GetSystemDateTime(PatientDialysisMetadata.ColumnNames.HBsAgDate);
            }

            set
            {
                base.SetSystemDateTime(PatientDialysisMetadata.ColumnNames.HBsAgDate, value);
            }
        }
        /// <summary>
        /// Maps to PatientDialysis.AntiHCV
        /// </summary>
        virtual public System.String AntiHCV
        {
            get
            {
                return base.GetSystemString(PatientDialysisMetadata.ColumnNames.AntiHCV);
            }

            set
            {
                base.SetSystemString(PatientDialysisMetadata.ColumnNames.AntiHCV, value);
            }
        }
        /// <summary>
        /// Maps to PatientDialysis.AntiHCVDate
        /// </summary>
        virtual public System.DateTime? AntiHCVDate
        {
            get
            {
                return base.GetSystemDateTime(PatientDialysisMetadata.ColumnNames.AntiHCVDate);
            }

            set
            {
                base.SetSystemDateTime(PatientDialysisMetadata.ColumnNames.AntiHCVDate, value);
            }
        }
        /// <summary>
        /// Maps to PatientDialysis.AntiHIV
        /// </summary>
        virtual public System.String AntiHIV
        {
            get
            {
                return base.GetSystemString(PatientDialysisMetadata.ColumnNames.AntiHIV);
            }

            set
            {
                base.SetSystemString(PatientDialysisMetadata.ColumnNames.AntiHIV, value);
            }
        }
        /// <summary>
        /// Maps to PatientDialysis.AntiHIVDate
        /// </summary>
        virtual public System.DateTime? AntiHIVDate
        {
            get
            {
                return base.GetSystemDateTime(PatientDialysisMetadata.ColumnNames.AntiHIVDate);
            }

            set
            {
                base.SetSystemDateTime(PatientDialysisMetadata.ColumnNames.AntiHIVDate, value);
            }
        }
        /// <summary>
        /// Maps to PatientDialysis.KidneyUltrasound
        /// </summary>
        virtual public System.String KidneyUltrasound
        {
            get
            {
                return base.GetSystemString(PatientDialysisMetadata.ColumnNames.KidneyUltrasound);
            }

            set
            {
                base.SetSystemString(PatientDialysisMetadata.ColumnNames.KidneyUltrasound, value);
            }
        }
        /// <summary>
        /// Maps to PatientDialysis.KidneyUltrasoundDate
        /// </summary>
        virtual public System.DateTime? KidneyUltrasoundDate
        {
            get
            {
                return base.GetSystemDateTime(PatientDialysisMetadata.ColumnNames.KidneyUltrasoundDate);
            }

            set
            {
                base.SetSystemDateTime(PatientDialysisMetadata.ColumnNames.KidneyUltrasoundDate, value);
            }
        }
        /// <summary>
        /// Maps to PatientDialysis.ECHO
        /// </summary>
        virtual public System.String ECHO
        {
            get
            {
                return base.GetSystemString(PatientDialysisMetadata.ColumnNames.ECHO);
            }

            set
            {
                base.SetSystemString(PatientDialysisMetadata.ColumnNames.ECHO, value);
            }
        }
        /// <summary>
        /// Maps to PatientDialysis.ECHODate
        /// </summary>
        virtual public System.DateTime? ECHODate
        {
            get
            {
                return base.GetSystemDateTime(PatientDialysisMetadata.ColumnNames.ECHODate);
            }

            set
            {
                base.SetSystemDateTime(PatientDialysisMetadata.ColumnNames.ECHODate, value);
            }
        }
        /// <summary>
        /// Maps to PatientDialysis.FirstHDDate
        /// </summary>
        virtual public System.DateTime? FirstHDDate
        {
            get
            {
                return base.GetSystemDateTime(PatientDialysisMetadata.ColumnNames.FirstHDDate);
            }

            set
            {
                base.SetSystemDateTime(PatientDialysisMetadata.ColumnNames.FirstHDDate, value);
            }
        }
        /// <summary>
        /// Maps to PatientDialysis.TransferHDDate
        /// </summary>
        virtual public System.DateTime? TransferHDDate
        {
            get
            {
                return base.GetSystemDateTime(PatientDialysisMetadata.ColumnNames.TransferHDDate);
            }

            set
            {
                base.SetSystemDateTime(PatientDialysisMetadata.ColumnNames.TransferHDDate, value);
            }
        }
        /// <summary>
        /// Maps to PatientDialysis.FirstPDDate
        /// </summary>
        virtual public System.DateTime? FirstPDDate
        {
            get
            {
                return base.GetSystemDateTime(PatientDialysisMetadata.ColumnNames.FirstPDDate);
            }

            set
            {
                base.SetSystemDateTime(PatientDialysisMetadata.ColumnNames.FirstPDDate, value);
            }
        }
        /// <summary>
        /// Maps to PatientDialysis.TransferPDDate
        /// </summary>
        virtual public System.DateTime? TransferPDDate
        {
            get
            {
                return base.GetSystemDateTime(PatientDialysisMetadata.ColumnNames.TransferPDDate);
            }

            set
            {
                base.SetSystemDateTime(PatientDialysisMetadata.ColumnNames.TransferPDDate, value);
            }
        }
        /// <summary>
        /// Maps to PatientDialysis.KidneyTransplantDate
        /// </summary>
        virtual public System.DateTime? KidneyTransplantDate
        {
            get
            {
                return base.GetSystemDateTime(PatientDialysisMetadata.ColumnNames.KidneyTransplantDate);
            }

            set
            {
                base.SetSystemDateTime(PatientDialysisMetadata.ColumnNames.KidneyTransplantDate, value);
            }
        }
        /// <summary>
        /// Maps to PatientDialysis.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(PatientDialysisMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(PatientDialysisMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to PatientDialysis.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(PatientDialysisMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(PatientDialysisMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to PatientDialysis.CreatedDateTime
        /// </summary>
        virtual public System.DateTime? CreatedDateTime
        {
            get
            {
                return base.GetSystemDateTime(PatientDialysisMetadata.ColumnNames.CreatedDateTime);
            }

            set
            {
                base.SetSystemDateTime(PatientDialysisMetadata.ColumnNames.CreatedDateTime, value);
            }
        }
        /// <summary>
        /// Maps to PatientDialysis.CreatedByUserID
        /// </summary>
        virtual public System.String CreatedByUserID
        {
            get
            {
                return base.GetSystemString(PatientDialysisMetadata.ColumnNames.CreatedByUserID);
            }

            set
            {
                base.SetSystemString(PatientDialysisMetadata.ColumnNames.CreatedByUserID, value);
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
            public esStrings(esPatientDialysis entity)
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
            public System.String InitialDiagnosis
            {
                get
                {
                    System.String data = entity.InitialDiagnosis;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.InitialDiagnosis = null;
                    else entity.InitialDiagnosis = Convert.ToString(value);
                }
            }
            public System.String RefferingHospital
            {
                get
                {
                    System.String data = entity.RefferingHospital;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RefferingHospital = null;
                    else entity.RefferingHospital = Convert.ToString(value);
                }
            }
            public System.String RefferingPhysician
            {
                get
                {
                    System.String data = entity.RefferingPhysician;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RefferingPhysician = null;
                    else entity.RefferingPhysician = Convert.ToString(value);
                }
            }
            public System.String Hb
            {
                get
                {
                    System.String data = entity.Hb;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Hb = null;
                    else entity.Hb = Convert.ToString(value);
                }
            }
            public System.String HbDate
            {
                get
                {
                    System.DateTime? data = entity.HbDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.HbDate = null;
                    else entity.HbDate = Convert.ToDateTime(value);
                }
            }
            public System.String Urea
            {
                get
                {
                    System.String data = entity.Urea;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Urea = null;
                    else entity.Urea = Convert.ToString(value);
                }
            }
            public System.String UreaDate
            {
                get
                {
                    System.DateTime? data = entity.UreaDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.UreaDate = null;
                    else entity.UreaDate = Convert.ToDateTime(value);
                }
            }
            public System.String Creatinine
            {
                get
                {
                    System.String data = entity.Creatinine;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Creatinine = null;
                    else entity.Creatinine = Convert.ToString(value);
                }
            }
            public System.String CreatinineDate
            {
                get
                {
                    System.DateTime? data = entity.CreatinineDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CreatinineDate = null;
                    else entity.CreatinineDate = Convert.ToDateTime(value);
                }
            }
            public System.String HBsAg
            {
                get
                {
                    System.String data = entity.HBsAg;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.HBsAg = null;
                    else entity.HBsAg = Convert.ToString(value);
                }
            }
            public System.String HBsAgDate
            {
                get
                {
                    System.DateTime? data = entity.HBsAgDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.HBsAgDate = null;
                    else entity.HBsAgDate = Convert.ToDateTime(value);
                }
            }
            public System.String AntiHCV
            {
                get
                {
                    System.String data = entity.AntiHCV;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.AntiHCV = null;
                    else entity.AntiHCV = Convert.ToString(value);
                }
            }
            public System.String AntiHCVDate
            {
                get
                {
                    System.DateTime? data = entity.AntiHCVDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.AntiHCVDate = null;
                    else entity.AntiHCVDate = Convert.ToDateTime(value);
                }
            }
            public System.String AntiHIV
            {
                get
                {
                    System.String data = entity.AntiHIV;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.AntiHIV = null;
                    else entity.AntiHIV = Convert.ToString(value);
                }
            }
            public System.String AntiHIVDate
            {
                get
                {
                    System.DateTime? data = entity.AntiHIVDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.AntiHIVDate = null;
                    else entity.AntiHIVDate = Convert.ToDateTime(value);
                }
            }
            public System.String KidneyUltrasound
            {
                get
                {
                    System.String data = entity.KidneyUltrasound;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.KidneyUltrasound = null;
                    else entity.KidneyUltrasound = Convert.ToString(value);
                }
            }
            public System.String KidneyUltrasoundDate
            {
                get
                {
                    System.DateTime? data = entity.KidneyUltrasoundDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.KidneyUltrasoundDate = null;
                    else entity.KidneyUltrasoundDate = Convert.ToDateTime(value);
                }
            }
            public System.String ECHO
            {
                get
                {
                    System.String data = entity.ECHO;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ECHO = null;
                    else entity.ECHO = Convert.ToString(value);
                }
            }
            public System.String ECHODate
            {
                get
                {
                    System.DateTime? data = entity.ECHODate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ECHODate = null;
                    else entity.ECHODate = Convert.ToDateTime(value);
                }
            }
            public System.String FirstHDDate
            {
                get
                {
                    System.DateTime? data = entity.FirstHDDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FirstHDDate = null;
                    else entity.FirstHDDate = Convert.ToDateTime(value);
                }
            }
            public System.String TransferHDDate
            {
                get
                {
                    System.DateTime? data = entity.TransferHDDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TransferHDDate = null;
                    else entity.TransferHDDate = Convert.ToDateTime(value);
                }
            }
            public System.String FirstPDDate
            {
                get
                {
                    System.DateTime? data = entity.FirstPDDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FirstPDDate = null;
                    else entity.FirstPDDate = Convert.ToDateTime(value);
                }
            }
            public System.String TransferPDDate
            {
                get
                {
                    System.DateTime? data = entity.TransferPDDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TransferPDDate = null;
                    else entity.TransferPDDate = Convert.ToDateTime(value);
                }
            }
            public System.String KidneyTransplantDate
            {
                get
                {
                    System.DateTime? data = entity.KidneyTransplantDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.KidneyTransplantDate = null;
                    else entity.KidneyTransplantDate = Convert.ToDateTime(value);
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
            private esPatientDialysis entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esPatientDialysisQuery query)
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
                throw new Exception("esPatientDialysis can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class PatientDialysis : esPatientDialysis
    {
    }

    [Serializable]
    abstract public class esPatientDialysisQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return PatientDialysisMetadata.Meta();
            }
        }

        public esQueryItem PatientID
        {
            get
            {
                return new esQueryItem(this, PatientDialysisMetadata.ColumnNames.PatientID, esSystemType.String);
            }
        }

        public esQueryItem InitialDiagnosis
        {
            get
            {
                return new esQueryItem(this, PatientDialysisMetadata.ColumnNames.InitialDiagnosis, esSystemType.String);
            }
        }

        public esQueryItem RefferingHospital
        {
            get
            {
                return new esQueryItem(this, PatientDialysisMetadata.ColumnNames.RefferingHospital, esSystemType.String);
            }
        }

        public esQueryItem RefferingPhysician
        {
            get
            {
                return new esQueryItem(this, PatientDialysisMetadata.ColumnNames.RefferingPhysician, esSystemType.String);
            }
        }

        public esQueryItem Hb
        {
            get
            {
                return new esQueryItem(this, PatientDialysisMetadata.ColumnNames.Hb, esSystemType.String);
            }
        }

        public esQueryItem HbDate
        {
            get
            {
                return new esQueryItem(this, PatientDialysisMetadata.ColumnNames.HbDate, esSystemType.DateTime);
            }
        }

        public esQueryItem Urea
        {
            get
            {
                return new esQueryItem(this, PatientDialysisMetadata.ColumnNames.Urea, esSystemType.String);
            }
        }

        public esQueryItem UreaDate
        {
            get
            {
                return new esQueryItem(this, PatientDialysisMetadata.ColumnNames.UreaDate, esSystemType.DateTime);
            }
        }

        public esQueryItem Creatinine
        {
            get
            {
                return new esQueryItem(this, PatientDialysisMetadata.ColumnNames.Creatinine, esSystemType.String);
            }
        }

        public esQueryItem CreatinineDate
        {
            get
            {
                return new esQueryItem(this, PatientDialysisMetadata.ColumnNames.CreatinineDate, esSystemType.DateTime);
            }
        }

        public esQueryItem HBsAg
        {
            get
            {
                return new esQueryItem(this, PatientDialysisMetadata.ColumnNames.HBsAg, esSystemType.String);
            }
        }

        public esQueryItem HBsAgDate
        {
            get
            {
                return new esQueryItem(this, PatientDialysisMetadata.ColumnNames.HBsAgDate, esSystemType.DateTime);
            }
        }

        public esQueryItem AntiHCV
        {
            get
            {
                return new esQueryItem(this, PatientDialysisMetadata.ColumnNames.AntiHCV, esSystemType.String);
            }
        }

        public esQueryItem AntiHCVDate
        {
            get
            {
                return new esQueryItem(this, PatientDialysisMetadata.ColumnNames.AntiHCVDate, esSystemType.DateTime);
            }
        }

        public esQueryItem AntiHIV
        {
            get
            {
                return new esQueryItem(this, PatientDialysisMetadata.ColumnNames.AntiHIV, esSystemType.String);
            }
        }

        public esQueryItem AntiHIVDate
        {
            get
            {
                return new esQueryItem(this, PatientDialysisMetadata.ColumnNames.AntiHIVDate, esSystemType.DateTime);
            }
        }

        public esQueryItem KidneyUltrasound
        {
            get
            {
                return new esQueryItem(this, PatientDialysisMetadata.ColumnNames.KidneyUltrasound, esSystemType.String);
            }
        }

        public esQueryItem KidneyUltrasoundDate
        {
            get
            {
                return new esQueryItem(this, PatientDialysisMetadata.ColumnNames.KidneyUltrasoundDate, esSystemType.DateTime);
            }
        }

        public esQueryItem ECHO
        {
            get
            {
                return new esQueryItem(this, PatientDialysisMetadata.ColumnNames.ECHO, esSystemType.String);
            }
        }

        public esQueryItem ECHODate
        {
            get
            {
                return new esQueryItem(this, PatientDialysisMetadata.ColumnNames.ECHODate, esSystemType.DateTime);
            }
        }

        public esQueryItem FirstHDDate
        {
            get
            {
                return new esQueryItem(this, PatientDialysisMetadata.ColumnNames.FirstHDDate, esSystemType.DateTime);
            }
        }

        public esQueryItem TransferHDDate
        {
            get
            {
                return new esQueryItem(this, PatientDialysisMetadata.ColumnNames.TransferHDDate, esSystemType.DateTime);
            }
        }

        public esQueryItem FirstPDDate
        {
            get
            {
                return new esQueryItem(this, PatientDialysisMetadata.ColumnNames.FirstPDDate, esSystemType.DateTime);
            }
        }

        public esQueryItem TransferPDDate
        {
            get
            {
                return new esQueryItem(this, PatientDialysisMetadata.ColumnNames.TransferPDDate, esSystemType.DateTime);
            }
        }

        public esQueryItem KidneyTransplantDate
        {
            get
            {
                return new esQueryItem(this, PatientDialysisMetadata.ColumnNames.KidneyTransplantDate, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, PatientDialysisMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, PatientDialysisMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem CreatedDateTime
        {
            get
            {
                return new esQueryItem(this, PatientDialysisMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem CreatedByUserID
        {
            get
            {
                return new esQueryItem(this, PatientDialysisMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("PatientDialysisCollection")]
    public partial class PatientDialysisCollection : esPatientDialysisCollection, IEnumerable<PatientDialysis>
    {
        public PatientDialysisCollection()
        {

        }

        public static implicit operator List<PatientDialysis>(PatientDialysisCollection coll)
        {
            List<PatientDialysis> list = new List<PatientDialysis>();

            foreach (PatientDialysis emp in coll)
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
                return PatientDialysisMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PatientDialysisQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new PatientDialysis(row);
        }

        override protected esEntity CreateEntity()
        {
            return new PatientDialysis();
        }

        #endregion

        [BrowsableAttribute(false)]
        public PatientDialysisQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PatientDialysisQuery();
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
        public bool Load(PatientDialysisQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public PatientDialysis AddNew()
        {
            PatientDialysis entity = base.AddNewEntity() as PatientDialysis;

            return entity;
        }
        public PatientDialysis FindByPrimaryKey(String patientID)
        {
            return base.FindByPrimaryKey(patientID) as PatientDialysis;
        }

        #region IEnumerable< PatientDialysis> Members

        IEnumerator<PatientDialysis> IEnumerable<PatientDialysis>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as PatientDialysis;
            }
        }

        #endregion

        private PatientDialysisQuery query;
    }


    /// <summary>
    /// Encapsulates the 'PatientDialysis' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("PatientDialysis ({PatientID})")]
    [Serializable]
    public partial class PatientDialysis : esPatientDialysis
    {
        public PatientDialysis()
        {
        }

        public PatientDialysis(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return PatientDialysisMetadata.Meta();
            }
        }

        override protected esPatientDialysisQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PatientDialysisQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public PatientDialysisQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PatientDialysisQuery();
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
        public bool Load(PatientDialysisQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private PatientDialysisQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class PatientDialysisQuery : esPatientDialysisQuery
    {
        public PatientDialysisQuery()
        {

        }

        public PatientDialysisQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "PatientDialysisQuery";
        }
    }

    [Serializable]
    public partial class PatientDialysisMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected PatientDialysisMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(PatientDialysisMetadata.ColumnNames.PatientID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientDialysisMetadata.PropertyNames.PatientID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 15;
            _columns.Add(c);

            c = new esColumnMetadata(PatientDialysisMetadata.ColumnNames.InitialDiagnosis, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientDialysisMetadata.PropertyNames.InitialDiagnosis;
            c.CharacterMaxLength = 4000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientDialysisMetadata.ColumnNames.RefferingHospital, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientDialysisMetadata.PropertyNames.RefferingHospital;
            c.CharacterMaxLength = 4000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientDialysisMetadata.ColumnNames.RefferingPhysician, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientDialysisMetadata.PropertyNames.RefferingPhysician;
            c.CharacterMaxLength = 4000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientDialysisMetadata.ColumnNames.Hb, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientDialysisMetadata.PropertyNames.Hb;
            c.CharacterMaxLength = 4000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientDialysisMetadata.ColumnNames.HbDate, 5, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PatientDialysisMetadata.PropertyNames.HbDate;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientDialysisMetadata.ColumnNames.Urea, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientDialysisMetadata.PropertyNames.Urea;
            c.CharacterMaxLength = 4000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientDialysisMetadata.ColumnNames.UreaDate, 7, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PatientDialysisMetadata.PropertyNames.UreaDate;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientDialysisMetadata.ColumnNames.Creatinine, 8, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientDialysisMetadata.PropertyNames.Creatinine;
            c.CharacterMaxLength = 4000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientDialysisMetadata.ColumnNames.CreatinineDate, 9, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PatientDialysisMetadata.PropertyNames.CreatinineDate;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientDialysisMetadata.ColumnNames.HBsAg, 10, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientDialysisMetadata.PropertyNames.HBsAg;
            c.CharacterMaxLength = 4000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientDialysisMetadata.ColumnNames.HBsAgDate, 11, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PatientDialysisMetadata.PropertyNames.HBsAgDate;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientDialysisMetadata.ColumnNames.AntiHCV, 12, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientDialysisMetadata.PropertyNames.AntiHCV;
            c.CharacterMaxLength = 4000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientDialysisMetadata.ColumnNames.AntiHCVDate, 13, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PatientDialysisMetadata.PropertyNames.AntiHCVDate;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientDialysisMetadata.ColumnNames.AntiHIV, 14, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientDialysisMetadata.PropertyNames.AntiHIV;
            c.CharacterMaxLength = 4000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientDialysisMetadata.ColumnNames.AntiHIVDate, 15, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PatientDialysisMetadata.PropertyNames.AntiHIVDate;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientDialysisMetadata.ColumnNames.KidneyUltrasound, 16, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientDialysisMetadata.PropertyNames.KidneyUltrasound;
            c.CharacterMaxLength = 4000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientDialysisMetadata.ColumnNames.KidneyUltrasoundDate, 17, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PatientDialysisMetadata.PropertyNames.KidneyUltrasoundDate;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientDialysisMetadata.ColumnNames.ECHO, 18, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientDialysisMetadata.PropertyNames.ECHO;
            c.CharacterMaxLength = 4000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientDialysisMetadata.ColumnNames.ECHODate, 19, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PatientDialysisMetadata.PropertyNames.ECHODate;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientDialysisMetadata.ColumnNames.FirstHDDate, 20, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PatientDialysisMetadata.PropertyNames.FirstHDDate;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientDialysisMetadata.ColumnNames.TransferHDDate, 21, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PatientDialysisMetadata.PropertyNames.TransferHDDate;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientDialysisMetadata.ColumnNames.FirstPDDate, 22, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PatientDialysisMetadata.PropertyNames.FirstPDDate;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientDialysisMetadata.ColumnNames.TransferPDDate, 23, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PatientDialysisMetadata.PropertyNames.TransferPDDate;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientDialysisMetadata.ColumnNames.KidneyTransplantDate, 24, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PatientDialysisMetadata.PropertyNames.KidneyTransplantDate;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientDialysisMetadata.ColumnNames.LastUpdateDateTime, 25, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PatientDialysisMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientDialysisMetadata.ColumnNames.LastUpdateByUserID, 26, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientDialysisMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientDialysisMetadata.ColumnNames.CreatedDateTime, 27, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PatientDialysisMetadata.PropertyNames.CreatedDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientDialysisMetadata.ColumnNames.CreatedByUserID, 28, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientDialysisMetadata.PropertyNames.CreatedByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public PatientDialysisMetadata Meta()
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
            public const string InitialDiagnosis = "InitialDiagnosis";
            public const string RefferingHospital = "RefferingHospital";
            public const string RefferingPhysician = "RefferingPhysician";
            public const string Hb = "Hb";
            public const string HbDate = "hbDate";
            public const string Urea = "Urea";
            public const string UreaDate = "UreaDate";
            public const string Creatinine = "Creatinine";
            public const string CreatinineDate = "CreatinineDate";
            public const string HBsAg = "HBsAg";
            public const string HBsAgDate = "HBsAgDate";
            public const string AntiHCV = "AntiHCV";
            public const string AntiHCVDate = "AntiHCVDate";
            public const string AntiHIV = "AntiHIV";
            public const string AntiHIVDate = "AntiHIVDate";
            public const string KidneyUltrasound = "KidneyUltrasound";
            public const string KidneyUltrasoundDate = "KidneyUltrasoundDate";
            public const string ECHO = "ECHO";
            public const string ECHODate = "ECHODate";
            public const string FirstHDDate = "FirstHDDate";
            public const string TransferHDDate = "TransferHDDate";
            public const string FirstPDDate = "FirstPDDate";
            public const string TransferPDDate = "TransferPDDate";
            public const string KidneyTransplantDate = "KidneyTransplantDate";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string CreatedDateTime = "CreatedDateTime";
            public const string CreatedByUserID = "CreatedByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string PatientID = "PatientID";
            public const string InitialDiagnosis = "InitialDiagnosis";
            public const string RefferingHospital = "RefferingHospital";
            public const string RefferingPhysician = "RefferingPhysician";
            public const string Hb = "Hb";
            public const string HbDate = "HbDate";
            public const string Urea = "Urea";
            public const string UreaDate = "UreaDate";
            public const string Creatinine = "Creatinine";
            public const string CreatinineDate = "CreatinineDate";
            public const string HBsAg = "HBsAg";
            public const string HBsAgDate = "HBsAgDate";
            public const string AntiHCV = "AntiHCV";
            public const string AntiHCVDate = "AntiHCVDate";
            public const string AntiHIV = "AntiHIV";
            public const string AntiHIVDate = "AntiHIVDate";
            public const string KidneyUltrasound = "KidneyUltrasound";
            public const string KidneyUltrasoundDate = "KidneyUltrasoundDate";
            public const string ECHO = "ECHO";
            public const string ECHODate = "ECHODate";
            public const string FirstHDDate = "FirstHDDate";
            public const string TransferHDDate = "TransferHDDate";
            public const string FirstPDDate = "FirstPDDate";
            public const string TransferPDDate = "TransferPDDate";
            public const string KidneyTransplantDate = "KidneyTransplantDate";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string CreatedDateTime = "CreatedDateTime";
            public const string CreatedByUserID = "CreatedByUserID";
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
            lock (typeof(PatientDialysisMetadata))
            {
                if (PatientDialysisMetadata.mapDelegates == null)
                {
                    PatientDialysisMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (PatientDialysisMetadata.meta == null)
                {
                    PatientDialysisMetadata.meta = new PatientDialysisMetadata();
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
                meta.AddTypeMap("InitialDiagnosis", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("RefferingHospital", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("RefferingPhysician", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Hb", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("HbDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("Urea", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("UreaDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("Creatinine", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("CreatinineDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("HBsAg", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("HBsAgDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("AntiHCV", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("AntiHCVDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("AntiHIV", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("AntiHIVDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("KidneyUltrasound", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("KidneyUltrasoundDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("ECHO", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ECHODate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("FirstHDDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("TransferHDDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("FirstPDDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("TransferPDDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("KidneyTransplantDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "PatientDialysis";
                meta.Destination = "PatientDialysis";
                meta.spInsert = "proc_PatientDialysisInsert";
                meta.spUpdate = "proc_PatientDialysisUpdate";
                meta.spDelete = "proc_PatientDialysisDelete";
                meta.spLoadAll = "proc_PatientDialysisLoadAll";
                meta.spLoadByPrimaryKey = "proc_PatientDialysisLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private PatientDialysisMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
