/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 1/31/2024 10:11:05 AM
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
    abstract public class esPatientChildBirthHistoryCollection : esEntityCollectionWAuditLog
    {
        public esPatientChildBirthHistoryCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "PatientChildBirthHistoryCollection";
        }

        #region Query Logic
        protected void InitQuery(esPatientChildBirthHistoryQuery query)
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
            this.InitQuery(query as esPatientChildBirthHistoryQuery);
        }
        #endregion

        virtual public PatientChildBirthHistory DetachEntity(PatientChildBirthHistory entity)
        {
            return base.DetachEntity(entity) as PatientChildBirthHistory;
        }

        virtual public PatientChildBirthHistory AttachEntity(PatientChildBirthHistory entity)
        {
            return base.AttachEntity(entity) as PatientChildBirthHistory;
        }

        virtual public void Combine(PatientChildBirthHistoryCollection collection)
        {
            base.Combine(collection);
        }

        new public PatientChildBirthHistory this[int index]
        {
            get
            {
                return base[index] as PatientChildBirthHistory;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(PatientChildBirthHistory);
        }
    }

    [Serializable]
    abstract public class esPatientChildBirthHistory : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esPatientChildBirthHistoryQuery GetDynamicQuery()
        {
            return null;
        }

        public esPatientChildBirthHistory()
        {
        }

        public esPatientChildBirthHistory(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String patientID, Int32 sequenceNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(patientID, sequenceNo);
            else
                return LoadByPrimaryKeyStoredProcedure(patientID, sequenceNo);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String patientID, Int32 sequenceNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(patientID, sequenceNo);
            else
                return LoadByPrimaryKeyStoredProcedure(patientID, sequenceNo);
        }

        private bool LoadByPrimaryKeyDynamic(String patientID, Int32 sequenceNo)
        {
            esPatientChildBirthHistoryQuery query = this.GetDynamicQuery();
            query.Where(query.PatientID == patientID, query.SequenceNo == sequenceNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String patientID, Int32 sequenceNo)
        {
            esParameters parms = new esParameters();
            parms.Add("PatientID", patientID);
            parms.Add("SequenceNo", sequenceNo);
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
                        case "SequenceNo": this.str.SequenceNo = (string)value; break;
                        case "ChildBirth": this.str.ChildBirth = (string)value; break;
                        case "Sex": this.str.Sex = (string)value; break;
                        case "Helper": this.str.Helper = (string)value; break;
                        case "Location": this.str.Location = (string)value; break;
                        case "HM": this.str.HM = (string)value; break;
                        case "BBL": this.str.BBL = (string)value; break;
                        case "Complication": this.str.Complication = (string)value; break;
                        case "Notes": this.str.Notes = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "PregnanDurationMonth": this.str.PregnanDurationMonth = (string)value; break;
                        case "PregnanDurationWeek": this.str.PregnanDurationWeek = (string)value; break;
                        case "SRBirthMethod": this.str.SRBirthMethod = (string)value; break;
                        case "PregnanDurationDay": this.str.PregnanDurationDay = (string)value; break;
                        case "SRBirthType": this.str.SRBirthType = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "SequenceNo":

                            if (value == null || value is System.Int32)
                                this.SequenceNo = (System.Int32?)value;
                            break;
                        case "LastUpdateDateTime":

                            if (value == null || value is System.DateTime)
                                this.LastUpdateDateTime = (System.DateTime?)value;
                            break;
                        case "PregnanDurationMonth":

                            if (value == null || value is System.Int32)
                                this.PregnanDurationMonth = (System.Int32?)value;
                            break;
                        case "PregnanDurationWeek":

                            if (value == null || value is System.Int32)
                                this.PregnanDurationWeek = (System.Int32?)value;
                            break;
                        case "PregnanDurationDay":

                            if (value == null || value is System.Int32)
                                this.PregnanDurationDay = (System.Int32?)value;
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
        /// Maps to PatientChildBirthHistory.PatientID
        /// </summary>
        virtual public System.String PatientID
        {
            get
            {
                return base.GetSystemString(PatientChildBirthHistoryMetadata.ColumnNames.PatientID);
            }

            set
            {
                base.SetSystemString(PatientChildBirthHistoryMetadata.ColumnNames.PatientID, value);
            }
        }
        /// <summary>
        /// Maps to PatientChildBirthHistory.SequenceNo
        /// </summary>
        virtual public System.Int32? SequenceNo
        {
            get
            {
                return base.GetSystemInt32(PatientChildBirthHistoryMetadata.ColumnNames.SequenceNo);
            }

            set
            {
                base.SetSystemInt32(PatientChildBirthHistoryMetadata.ColumnNames.SequenceNo, value);
            }
        }
        /// <summary>
        /// Maps to PatientChildBirthHistory.ChildBirth
        /// </summary>
        virtual public System.String ChildBirth
        {
            get
            {
                return base.GetSystemString(PatientChildBirthHistoryMetadata.ColumnNames.ChildBirth);
            }

            set
            {
                base.SetSystemString(PatientChildBirthHistoryMetadata.ColumnNames.ChildBirth, value);
            }
        }
        /// <summary>
        /// Maps to PatientChildBirthHistory.Sex
        /// </summary>
        virtual public System.String Sex
        {
            get
            {
                return base.GetSystemString(PatientChildBirthHistoryMetadata.ColumnNames.Sex);
            }

            set
            {
                base.SetSystemString(PatientChildBirthHistoryMetadata.ColumnNames.Sex, value);
            }
        }
        /// <summary>
        /// Maps to PatientChildBirthHistory.Helper
        /// </summary>
        virtual public System.String Helper
        {
            get
            {
                return base.GetSystemString(PatientChildBirthHistoryMetadata.ColumnNames.Helper);
            }

            set
            {
                base.SetSystemString(PatientChildBirthHistoryMetadata.ColumnNames.Helper, value);
            }
        }
        /// <summary>
        /// Maps to PatientChildBirthHistory.Location
        /// </summary>
        virtual public System.String Location
        {
            get
            {
                return base.GetSystemString(PatientChildBirthHistoryMetadata.ColumnNames.Location);
            }

            set
            {
                base.SetSystemString(PatientChildBirthHistoryMetadata.ColumnNames.Location, value);
            }
        }
        /// <summary>
        /// Maps to PatientChildBirthHistory.HM
        /// </summary>
        virtual public System.String HM
        {
            get
            {
                return base.GetSystemString(PatientChildBirthHistoryMetadata.ColumnNames.HM);
            }

            set
            {
                base.SetSystemString(PatientChildBirthHistoryMetadata.ColumnNames.HM, value);
            }
        }
        /// <summary>
        /// Maps to PatientChildBirthHistory.BBL
        /// </summary>
        virtual public System.String BBL
        {
            get
            {
                return base.GetSystemString(PatientChildBirthHistoryMetadata.ColumnNames.BBL);
            }

            set
            {
                base.SetSystemString(PatientChildBirthHistoryMetadata.ColumnNames.BBL, value);
            }
        }
        /// <summary>
        /// Maps to PatientChildBirthHistory.Complication
        /// </summary>
        virtual public System.String Complication
        {
            get
            {
                return base.GetSystemString(PatientChildBirthHistoryMetadata.ColumnNames.Complication);
            }

            set
            {
                base.SetSystemString(PatientChildBirthHistoryMetadata.ColumnNames.Complication, value);
            }
        }
        /// <summary>
        /// Maps to PatientChildBirthHistory.Notes
        /// </summary>
        virtual public System.String Notes
        {
            get
            {
                return base.GetSystemString(PatientChildBirthHistoryMetadata.ColumnNames.Notes);
            }

            set
            {
                base.SetSystemString(PatientChildBirthHistoryMetadata.ColumnNames.Notes, value);
            }
        }
        /// <summary>
        /// Maps to PatientChildBirthHistory.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(PatientChildBirthHistoryMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(PatientChildBirthHistoryMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to PatientChildBirthHistory.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(PatientChildBirthHistoryMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(PatientChildBirthHistoryMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to PatientChildBirthHistory.PregnanDurationMonth
        /// </summary>
        virtual public System.Int32? PregnanDurationMonth
        {
            get
            {
                return base.GetSystemInt32(PatientChildBirthHistoryMetadata.ColumnNames.PregnanDurationMonth);
            }

            set
            {
                base.SetSystemInt32(PatientChildBirthHistoryMetadata.ColumnNames.PregnanDurationMonth, value);
            }
        }
        /// <summary>
        /// Maps to PatientChildBirthHistory.PregnanDurationWeek
        /// </summary>
        virtual public System.Int32? PregnanDurationWeek
        {
            get
            {
                return base.GetSystemInt32(PatientChildBirthHistoryMetadata.ColumnNames.PregnanDurationWeek);
            }

            set
            {
                base.SetSystemInt32(PatientChildBirthHistoryMetadata.ColumnNames.PregnanDurationWeek, value);
            }
        }
        /// <summary>
        /// Maps to PatientChildBirthHistory.SRBirthMethod
        /// </summary>
        virtual public System.String SRBirthMethod
        {
            get
            {
                return base.GetSystemString(PatientChildBirthHistoryMetadata.ColumnNames.SRBirthMethod);
            }

            set
            {
                base.SetSystemString(PatientChildBirthHistoryMetadata.ColumnNames.SRBirthMethod, value);
            }
        }
        /// <summary>
        /// Maps to PatientChildBirthHistory.PregnanDurationDay
        /// </summary>
        virtual public System.Int32? PregnanDurationDay
        {
            get
            {
                return base.GetSystemInt32(PatientChildBirthHistoryMetadata.ColumnNames.PregnanDurationDay);
            }

            set
            {
                base.SetSystemInt32(PatientChildBirthHistoryMetadata.ColumnNames.PregnanDurationDay, value);
            }
        }
        /// <summary>
        /// Maps to PatientChildBirthHistory.SRBirthType
        /// </summary>
        virtual public System.String SRBirthType
        {
            get
            {
                return base.GetSystemString(PatientChildBirthHistoryMetadata.ColumnNames.SRBirthType);
            }

            set
            {
                base.SetSystemString(PatientChildBirthHistoryMetadata.ColumnNames.SRBirthType, value);
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
            public esStrings(esPatientChildBirthHistory entity)
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
            public System.String SequenceNo
            {
                get
                {
                    System.Int32? data = entity.SequenceNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SequenceNo = null;
                    else entity.SequenceNo = Convert.ToInt32(value);
                }
            }
            public System.String ChildBirth
            {
                get
                {
                    System.String data = entity.ChildBirth;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ChildBirth = null;
                    else entity.ChildBirth = Convert.ToString(value);
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
            public System.String Helper
            {
                get
                {
                    System.String data = entity.Helper;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Helper = null;
                    else entity.Helper = Convert.ToString(value);
                }
            }
            public System.String Location
            {
                get
                {
                    System.String data = entity.Location;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Location = null;
                    else entity.Location = Convert.ToString(value);
                }
            }
            public System.String HM
            {
                get
                {
                    System.String data = entity.HM;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.HM = null;
                    else entity.HM = Convert.ToString(value);
                }
            }
            public System.String BBL
            {
                get
                {
                    System.String data = entity.BBL;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.BBL = null;
                    else entity.BBL = Convert.ToString(value);
                }
            }
            public System.String Complication
            {
                get
                {
                    System.String data = entity.Complication;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Complication = null;
                    else entity.Complication = Convert.ToString(value);
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
            public System.String PregnanDurationMonth
            {
                get
                {
                    System.Int32? data = entity.PregnanDurationMonth;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PregnanDurationMonth = null;
                    else entity.PregnanDurationMonth = Convert.ToInt32(value);
                }
            }
            public System.String PregnanDurationWeek
            {
                get
                {
                    System.Int32? data = entity.PregnanDurationWeek;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PregnanDurationWeek = null;
                    else entity.PregnanDurationWeek = Convert.ToInt32(value);
                }
            }
            public System.String SRBirthMethod
            {
                get
                {
                    System.String data = entity.SRBirthMethod;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRBirthMethod = null;
                    else entity.SRBirthMethod = Convert.ToString(value);
                }
            }
            public System.String PregnanDurationDay
            {
                get
                {
                    System.Int32? data = entity.PregnanDurationDay;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PregnanDurationDay = null;
                    else entity.PregnanDurationDay = Convert.ToInt32(value);
                }
            }
            public System.String SRBirthType
            {
                get
                {
                    System.String data = entity.SRBirthType;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRBirthType = null;
                    else entity.SRBirthType = Convert.ToString(value);
                }
            }
            private esPatientChildBirthHistory entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esPatientChildBirthHistoryQuery query)
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
                throw new Exception("esPatientChildBirthHistory can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class PatientChildBirthHistory : esPatientChildBirthHistory
    {
    }

    [Serializable]
    abstract public class esPatientChildBirthHistoryQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return PatientChildBirthHistoryMetadata.Meta();
            }
        }

        public esQueryItem PatientID
        {
            get
            {
                return new esQueryItem(this, PatientChildBirthHistoryMetadata.ColumnNames.PatientID, esSystemType.String);
            }
        }

        public esQueryItem SequenceNo
        {
            get
            {
                return new esQueryItem(this, PatientChildBirthHistoryMetadata.ColumnNames.SequenceNo, esSystemType.Int32);
            }
        }

        public esQueryItem ChildBirth
        {
            get
            {
                return new esQueryItem(this, PatientChildBirthHistoryMetadata.ColumnNames.ChildBirth, esSystemType.String);
            }
        }

        public esQueryItem Sex
        {
            get
            {
                return new esQueryItem(this, PatientChildBirthHistoryMetadata.ColumnNames.Sex, esSystemType.String);
            }
        }

        public esQueryItem Helper
        {
            get
            {
                return new esQueryItem(this, PatientChildBirthHistoryMetadata.ColumnNames.Helper, esSystemType.String);
            }
        }

        public esQueryItem Location
        {
            get
            {
                return new esQueryItem(this, PatientChildBirthHistoryMetadata.ColumnNames.Location, esSystemType.String);
            }
        }

        public esQueryItem HM
        {
            get
            {
                return new esQueryItem(this, PatientChildBirthHistoryMetadata.ColumnNames.HM, esSystemType.String);
            }
        }

        public esQueryItem BBL
        {
            get
            {
                return new esQueryItem(this, PatientChildBirthHistoryMetadata.ColumnNames.BBL, esSystemType.String);
            }
        }

        public esQueryItem Complication
        {
            get
            {
                return new esQueryItem(this, PatientChildBirthHistoryMetadata.ColumnNames.Complication, esSystemType.String);
            }
        }

        public esQueryItem Notes
        {
            get
            {
                return new esQueryItem(this, PatientChildBirthHistoryMetadata.ColumnNames.Notes, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, PatientChildBirthHistoryMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, PatientChildBirthHistoryMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem PregnanDurationMonth
        {
            get
            {
                return new esQueryItem(this, PatientChildBirthHistoryMetadata.ColumnNames.PregnanDurationMonth, esSystemType.Int32);
            }
        }

        public esQueryItem PregnanDurationWeek
        {
            get
            {
                return new esQueryItem(this, PatientChildBirthHistoryMetadata.ColumnNames.PregnanDurationWeek, esSystemType.Int32);
            }
        }

        public esQueryItem SRBirthMethod
        {
            get
            {
                return new esQueryItem(this, PatientChildBirthHistoryMetadata.ColumnNames.SRBirthMethod, esSystemType.String);
            }
        }

        public esQueryItem PregnanDurationDay
        {
            get
            {
                return new esQueryItem(this, PatientChildBirthHistoryMetadata.ColumnNames.PregnanDurationDay, esSystemType.Int32);
            }
        }

        public esQueryItem SRBirthType
        {
            get
            {
                return new esQueryItem(this, PatientChildBirthHistoryMetadata.ColumnNames.SRBirthType, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("PatientChildBirthHistoryCollection")]
    public partial class PatientChildBirthHistoryCollection : esPatientChildBirthHistoryCollection, IEnumerable<PatientChildBirthHistory>
    {
        public PatientChildBirthHistoryCollection()
        {

        }

        public static implicit operator List<PatientChildBirthHistory>(PatientChildBirthHistoryCollection coll)
        {
            List<PatientChildBirthHistory> list = new List<PatientChildBirthHistory>();

            foreach (PatientChildBirthHistory emp in coll)
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
                return PatientChildBirthHistoryMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PatientChildBirthHistoryQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new PatientChildBirthHistory(row);
        }

        override protected esEntity CreateEntity()
        {
            return new PatientChildBirthHistory();
        }

        #endregion

        [BrowsableAttribute(false)]
        public PatientChildBirthHistoryQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PatientChildBirthHistoryQuery();
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
        public bool Load(PatientChildBirthHistoryQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public PatientChildBirthHistory AddNew()
        {
            PatientChildBirthHistory entity = base.AddNewEntity() as PatientChildBirthHistory;

            return entity;
        }
        public PatientChildBirthHistory FindByPrimaryKey(String patientID, Int32 sequenceNo)
        {
            return base.FindByPrimaryKey(patientID, sequenceNo) as PatientChildBirthHistory;
        }

        #region IEnumerable< PatientChildBirthHistory> Members

        IEnumerator<PatientChildBirthHistory> IEnumerable<PatientChildBirthHistory>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as PatientChildBirthHistory;
            }
        }

        #endregion

        private PatientChildBirthHistoryQuery query;
    }


    /// <summary>
    /// Encapsulates the 'PatientChildBirthHistory' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("PatientChildBirthHistory ({PatientID, SequenceNo})")]
    [Serializable]
    public partial class PatientChildBirthHistory : esPatientChildBirthHistory
    {
        public PatientChildBirthHistory()
        {
        }

        public PatientChildBirthHistory(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return PatientChildBirthHistoryMetadata.Meta();
            }
        }

        override protected esPatientChildBirthHistoryQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PatientChildBirthHistoryQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public PatientChildBirthHistoryQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PatientChildBirthHistoryQuery();
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
        public bool Load(PatientChildBirthHistoryQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private PatientChildBirthHistoryQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class PatientChildBirthHistoryQuery : esPatientChildBirthHistoryQuery
    {
        public PatientChildBirthHistoryQuery()
        {

        }

        public PatientChildBirthHistoryQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "PatientChildBirthHistoryQuery";
        }
    }

    [Serializable]
    public partial class PatientChildBirthHistoryMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected PatientChildBirthHistoryMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(PatientChildBirthHistoryMetadata.ColumnNames.PatientID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientChildBirthHistoryMetadata.PropertyNames.PatientID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 15;
            _columns.Add(c);

            c = new esColumnMetadata(PatientChildBirthHistoryMetadata.ColumnNames.SequenceNo, 1, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = PatientChildBirthHistoryMetadata.PropertyNames.SequenceNo;
            c.IsInPrimaryKey = true;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(PatientChildBirthHistoryMetadata.ColumnNames.ChildBirth, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientChildBirthHistoryMetadata.PropertyNames.ChildBirth;
            c.CharacterMaxLength = 200;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientChildBirthHistoryMetadata.ColumnNames.Sex, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientChildBirthHistoryMetadata.PropertyNames.Sex;
            c.CharacterMaxLength = 1;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientChildBirthHistoryMetadata.ColumnNames.Helper, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientChildBirthHistoryMetadata.PropertyNames.Helper;
            c.CharacterMaxLength = 100;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientChildBirthHistoryMetadata.ColumnNames.Location, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientChildBirthHistoryMetadata.PropertyNames.Location;
            c.CharacterMaxLength = 200;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientChildBirthHistoryMetadata.ColumnNames.HM, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientChildBirthHistoryMetadata.PropertyNames.HM;
            c.CharacterMaxLength = 200;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientChildBirthHistoryMetadata.ColumnNames.BBL, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientChildBirthHistoryMetadata.PropertyNames.BBL;
            c.CharacterMaxLength = 200;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientChildBirthHistoryMetadata.ColumnNames.Complication, 8, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientChildBirthHistoryMetadata.PropertyNames.Complication;
            c.CharacterMaxLength = 200;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientChildBirthHistoryMetadata.ColumnNames.Notes, 9, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientChildBirthHistoryMetadata.PropertyNames.Notes;
            c.CharacterMaxLength = 200;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientChildBirthHistoryMetadata.ColumnNames.LastUpdateDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PatientChildBirthHistoryMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientChildBirthHistoryMetadata.ColumnNames.LastUpdateByUserID, 11, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientChildBirthHistoryMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientChildBirthHistoryMetadata.ColumnNames.PregnanDurationMonth, 12, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = PatientChildBirthHistoryMetadata.PropertyNames.PregnanDurationMonth;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientChildBirthHistoryMetadata.ColumnNames.PregnanDurationWeek, 13, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = PatientChildBirthHistoryMetadata.PropertyNames.PregnanDurationWeek;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientChildBirthHistoryMetadata.ColumnNames.SRBirthMethod, 14, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientChildBirthHistoryMetadata.PropertyNames.SRBirthMethod;
            c.CharacterMaxLength = 5;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientChildBirthHistoryMetadata.ColumnNames.PregnanDurationDay, 15, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = PatientChildBirthHistoryMetadata.PropertyNames.PregnanDurationDay;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientChildBirthHistoryMetadata.ColumnNames.SRBirthType, 16, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientChildBirthHistoryMetadata.PropertyNames.SRBirthType;
            c.CharacterMaxLength = 5;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public PatientChildBirthHistoryMetadata Meta()
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
            public const string SequenceNo = "SequenceNo";
            public const string ChildBirth = "ChildBirth";
            public const string Sex = "Sex";
            public const string Helper = "Helper";
            public const string Location = "Location";
            public const string HM = "HM";
            public const string BBL = "BBL";
            public const string Complication = "Complication";
            public const string Notes = "Notes";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string PregnanDurationMonth = "PregnanDurationMonth";
            public const string PregnanDurationWeek = "PregnanDurationWeek";
            public const string SRBirthMethod = "SRBirthMethod";
            public const string PregnanDurationDay = "PregnanDurationDay";
            public const string SRBirthType = "SRBirthType";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string PatientID = "PatientID";
            public const string SequenceNo = "SequenceNo";
            public const string ChildBirth = "ChildBirth";
            public const string Sex = "Sex";
            public const string Helper = "Helper";
            public const string Location = "Location";
            public const string HM = "HM";
            public const string BBL = "BBL";
            public const string Complication = "Complication";
            public const string Notes = "Notes";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string PregnanDurationMonth = "PregnanDurationMonth";
            public const string PregnanDurationWeek = "PregnanDurationWeek";
            public const string SRBirthMethod = "SRBirthMethod";
            public const string PregnanDurationDay = "PregnanDurationDay";
            public const string SRBirthType = "SRBirthType";
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
            lock (typeof(PatientChildBirthHistoryMetadata))
            {
                if (PatientChildBirthHistoryMetadata.mapDelegates == null)
                {
                    PatientChildBirthHistoryMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (PatientChildBirthHistoryMetadata.meta == null)
                {
                    PatientChildBirthHistoryMetadata.meta = new PatientChildBirthHistoryMetadata();
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
                meta.AddTypeMap("SequenceNo", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("ChildBirth", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Sex", new esTypeMap("char", "System.String"));
                meta.AddTypeMap("Helper", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Location", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("HM", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("BBL", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Complication", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("PregnanDurationMonth", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("PregnanDurationWeek", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("SRBirthMethod", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("PregnanDurationDay", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("SRBirthType", new esTypeMap("varchar", "System.String"));


                meta.Source = "PatientChildBirthHistory";
                meta.Destination = "PatientChildBirthHistory";
                meta.spInsert = "proc_PatientChildBirthHistoryInsert";
                meta.spUpdate = "proc_PatientChildBirthHistoryUpdate";
                meta.spDelete = "proc_PatientChildBirthHistoryDelete";
                meta.spLoadAll = "proc_PatientChildBirthHistoryLoadAll";
                meta.spLoadByPrimaryKey = "proc_PatientChildBirthHistoryLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private PatientChildBirthHistoryMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
