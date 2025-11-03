/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 6/29/2016 11:27:30 PM
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
    abstract public class esEpisodeSOAPECollection : esEntityCollectionWAuditLog
    {
        public esEpisodeSOAPECollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "EpisodeSOAPECollection";
        }

        #region Query Logic
        protected void InitQuery(esEpisodeSOAPEQuery query)
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
            this.InitQuery(query as esEpisodeSOAPEQuery);
        }
        #endregion

        virtual public EpisodeSOAPE DetachEntity(EpisodeSOAPE entity)
        {
            return base.DetachEntity(entity) as EpisodeSOAPE;
        }

        virtual public EpisodeSOAPE AttachEntity(EpisodeSOAPE entity)
        {
            return base.AttachEntity(entity) as EpisodeSOAPE;
        }

        virtual public void Combine(EpisodeSOAPECollection collection)
        {
            base.Combine(collection);
        }

        new public EpisodeSOAPE this[int index]
        {
            get
            {
                return base[index] as EpisodeSOAPE;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(EpisodeSOAPE);
        }
    }

    [Serializable]
    abstract public class esEpisodeSOAPE : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esEpisodeSOAPEQuery GetDynamicQuery()
        {
            return null;
        }

        public esEpisodeSOAPE()
        {
        }

        public esEpisodeSOAPE(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String registrationNo, String sequenceNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(registrationNo, sequenceNo);
            else
                return LoadByPrimaryKeyStoredProcedure(registrationNo, sequenceNo);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String registrationNo, String sequenceNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(registrationNo, sequenceNo);
            else
                return LoadByPrimaryKeyStoredProcedure(registrationNo, sequenceNo);
        }

        private bool LoadByPrimaryKeyDynamic(String registrationNo, String sequenceNo)
        {
            esEpisodeSOAPEQuery query = this.GetDynamicQuery();
            query.Where(query.RegistrationNo == registrationNo, query.SequenceNo == sequenceNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String registrationNo, String sequenceNo)
        {
            esParameters parms = new esParameters();
            parms.Add("RegistrationNo", registrationNo);
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
                        case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
                        case "SequenceNo": this.str.SequenceNo = (string)value; break;
                        case "ParamedicID": this.str.ParamedicID = (string)value; break;
                        case "SOAPEDate": this.str.SOAPEDate = (string)value; break;
                        case "SOAPETime": this.str.SOAPETime = (string)value; break;
                        case "Subjective": this.str.Subjective = (string)value; break;
                        case "Objective": this.str.Objective = (string)value; break;
                        case "Assesment": this.str.Assesment = (string)value; break;
                        case "Planning": this.str.Planning = (string)value; break;
                        case "Evaluation": this.str.Evaluation = (string)value; break;
                        case "AttendingNotes": this.str.AttendingNotes = (string)value; break;
                        case "IsSummary": this.str.IsSummary = (string)value; break;
                        case "IsVoid": this.str.IsVoid = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
                        case "IsInformConcern": this.str.IsInformConcern = (string)value; break;
                        case "Imported": this.str.Imported = (string)value; break;
                        case "ImportedDateTime": this.str.ImportedDateTime = (string)value; break;
                        case "ToRegistrationInfoMedicID": this.str.ToRegistrationInfoMedicID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "SOAPEDate":

                            if (value == null || value is System.DateTime)
                                this.SOAPEDate = (System.DateTime?)value;
                            break;
                        case "IsSummary":

                            if (value == null || value is System.Boolean)
                                this.IsSummary = (System.Boolean?)value;
                            break;
                        case "IsVoid":

                            if (value == null || value is System.Boolean)
                                this.IsVoid = (System.Boolean?)value;
                            break;
                        case "LastUpdateDateTime":

                            if (value == null || value is System.DateTime)
                                this.LastUpdateDateTime = (System.DateTime?)value;
                            break;
                        case "IsInformConcern":

                            if (value == null || value is System.Boolean)
                                this.IsInformConcern = (System.Boolean?)value;
                            break;
                        case "BodyImage":

                            if (value == null || value is System.Byte[])
                                this.BodyImage = (System.Byte[])value;
                            break;
                        case "Imported":

                            if (value == null || value is System.Boolean)
                                this.Imported = (System.Boolean?)value;
                            break;
                        case "ImportedDateTime":

                            if (value == null || value is System.DateTime)
                                this.ImportedDateTime = (System.DateTime?)value;
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
        /// Maps to EpisodeSOAPE.RegistrationNo
        /// </summary>
        virtual public System.String RegistrationNo
        {
            get
            {
                return base.GetSystemString(EpisodeSOAPEMetadata.ColumnNames.RegistrationNo);
            }

            set
            {
                base.SetSystemString(EpisodeSOAPEMetadata.ColumnNames.RegistrationNo, value);
            }
        }
        /// <summary>
        /// Maps to EpisodeSOAPE.SequenceNo
        /// </summary>
        virtual public System.String SequenceNo
        {
            get
            {
                return base.GetSystemString(EpisodeSOAPEMetadata.ColumnNames.SequenceNo);
            }

            set
            {
                base.SetSystemString(EpisodeSOAPEMetadata.ColumnNames.SequenceNo, value);
            }
        }
        /// <summary>
        /// Maps to EpisodeSOAPE.ParamedicID
        /// </summary>
        virtual public System.String ParamedicID
        {
            get
            {
                return base.GetSystemString(EpisodeSOAPEMetadata.ColumnNames.ParamedicID);
            }

            set
            {
                base.SetSystemString(EpisodeSOAPEMetadata.ColumnNames.ParamedicID, value);
            }
        }
        /// <summary>
        /// Maps to EpisodeSOAPE.SOAPEDate
        /// </summary>
        virtual public System.DateTime? SOAPEDate
        {
            get
            {
                return base.GetSystemDateTime(EpisodeSOAPEMetadata.ColumnNames.SOAPEDate);
            }

            set
            {
                base.SetSystemDateTime(EpisodeSOAPEMetadata.ColumnNames.SOAPEDate, value);
            }
        }
        /// <summary>
        /// Maps to EpisodeSOAPE.SOAPETime
        /// </summary>
        virtual public System.String SOAPETime
        {
            get
            {
                return base.GetSystemString(EpisodeSOAPEMetadata.ColumnNames.SOAPETime);
            }

            set
            {
                base.SetSystemString(EpisodeSOAPEMetadata.ColumnNames.SOAPETime, value);
            }
        }
        /// <summary>
        /// Maps to EpisodeSOAPE.Subjective
        /// </summary>
        virtual public System.String Subjective
        {
            get
            {
                return base.GetSystemString(EpisodeSOAPEMetadata.ColumnNames.Subjective);
            }

            set
            {
                base.SetSystemString(EpisodeSOAPEMetadata.ColumnNames.Subjective, value);
            }
        }
        /// <summary>
        /// Maps to EpisodeSOAPE.Objective
        /// </summary>
        virtual public System.String Objective
        {
            get
            {
                return base.GetSystemString(EpisodeSOAPEMetadata.ColumnNames.Objective);
            }

            set
            {
                base.SetSystemString(EpisodeSOAPEMetadata.ColumnNames.Objective, value);
            }
        }
        /// <summary>
        /// Maps to EpisodeSOAPE.Assesment
        /// </summary>
        virtual public System.String Assesment
        {
            get
            {
                return base.GetSystemString(EpisodeSOAPEMetadata.ColumnNames.Assesment);
            }

            set
            {
                base.SetSystemString(EpisodeSOAPEMetadata.ColumnNames.Assesment, value);
            }
        }
        /// <summary>
        /// Maps to EpisodeSOAPE.Planning
        /// </summary>
        virtual public System.String Planning
        {
            get
            {
                return base.GetSystemString(EpisodeSOAPEMetadata.ColumnNames.Planning);
            }

            set
            {
                base.SetSystemString(EpisodeSOAPEMetadata.ColumnNames.Planning, value);
            }
        }
        /// <summary>
        /// Maps to EpisodeSOAPE.Evaluation
        /// </summary>
        virtual public System.String Evaluation
        {
            get
            {
                return base.GetSystemString(EpisodeSOAPEMetadata.ColumnNames.Evaluation);
            }

            set
            {
                base.SetSystemString(EpisodeSOAPEMetadata.ColumnNames.Evaluation, value);
            }
        }
        /// <summary>
        /// Maps to EpisodeSOAPE.AttendingNotes
        /// </summary>
        virtual public System.String AttendingNotes
        {
            get
            {
                return base.GetSystemString(EpisodeSOAPEMetadata.ColumnNames.AttendingNotes);
            }

            set
            {
                base.SetSystemString(EpisodeSOAPEMetadata.ColumnNames.AttendingNotes, value);
            }
        }
        /// <summary>
        /// Maps to EpisodeSOAPE.IsSummary
        /// </summary>
        virtual public System.Boolean? IsSummary
        {
            get
            {
                return base.GetSystemBoolean(EpisodeSOAPEMetadata.ColumnNames.IsSummary);
            }

            set
            {
                base.SetSystemBoolean(EpisodeSOAPEMetadata.ColumnNames.IsSummary, value);
            }
        }
        /// <summary>
        /// Maps to EpisodeSOAPE.IsVoid
        /// </summary>
        virtual public System.Boolean? IsVoid
        {
            get
            {
                return base.GetSystemBoolean(EpisodeSOAPEMetadata.ColumnNames.IsVoid);
            }

            set
            {
                base.SetSystemBoolean(EpisodeSOAPEMetadata.ColumnNames.IsVoid, value);
            }
        }
        /// <summary>
        /// Maps to EpisodeSOAPE.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(EpisodeSOAPEMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(EpisodeSOAPEMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to EpisodeSOAPE.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(EpisodeSOAPEMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(EpisodeSOAPEMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to EpisodeSOAPE.ServiceUnitID
        /// </summary>
        virtual public System.String ServiceUnitID
        {
            get
            {
                return base.GetSystemString(EpisodeSOAPEMetadata.ColumnNames.ServiceUnitID);
            }

            set
            {
                base.SetSystemString(EpisodeSOAPEMetadata.ColumnNames.ServiceUnitID, value);
            }
        }
        /// <summary>
        /// Maps to EpisodeSOAPE.IsInformConcern
        /// </summary>
        virtual public System.Boolean? IsInformConcern
        {
            get
            {
                return base.GetSystemBoolean(EpisodeSOAPEMetadata.ColumnNames.IsInformConcern);
            }

            set
            {
                base.SetSystemBoolean(EpisodeSOAPEMetadata.ColumnNames.IsInformConcern, value);
            }
        }
        /// <summary>
        /// Maps to EpisodeSOAPE.BodyImage
        /// </summary>
        virtual public System.Byte[] BodyImage
        {
            get
            {
                return base.GetSystemByteArray(EpisodeSOAPEMetadata.ColumnNames.BodyImage);
            }

            set
            {
                base.SetSystemByteArray(EpisodeSOAPEMetadata.ColumnNames.BodyImage, value);
            }
        }
        /// <summary>
        /// Maps to EpisodeSOAPE.Imported
        /// </summary>
        virtual public System.Boolean? Imported
        {
            get
            {
                return base.GetSystemBoolean(EpisodeSOAPEMetadata.ColumnNames.Imported);
            }

            set
            {
                base.SetSystemBoolean(EpisodeSOAPEMetadata.ColumnNames.Imported, value);
            }
        }
        /// <summary>
        /// Maps to EpisodeSOAPE.ImportedDateTime
        /// </summary>
        virtual public System.DateTime? ImportedDateTime
        {
            get
            {
                return base.GetSystemDateTime(EpisodeSOAPEMetadata.ColumnNames.ImportedDateTime);
            }

            set
            {
                base.SetSystemDateTime(EpisodeSOAPEMetadata.ColumnNames.ImportedDateTime, value);
            }
        }
        /// <summary>
        /// Maps to EpisodeSOAPE.ToRegistrationInfoMedicID
        /// </summary>
        virtual public System.String ToRegistrationInfoMedicID
        {
            get
            {
                return base.GetSystemString(EpisodeSOAPEMetadata.ColumnNames.ToRegistrationInfoMedicID);
            }

            set
            {
                base.SetSystemString(EpisodeSOAPEMetadata.ColumnNames.ToRegistrationInfoMedicID, value);
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
            public esStrings(esEpisodeSOAPE entity)
            {
                this.entity = entity;
            }
            public System.String RegistrationNo
            {
                get
                {
                    System.String data = entity.RegistrationNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RegistrationNo = null;
                    else entity.RegistrationNo = Convert.ToString(value);
                }
            }
            public System.String SequenceNo
            {
                get
                {
                    System.String data = entity.SequenceNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SequenceNo = null;
                    else entity.SequenceNo = Convert.ToString(value);
                }
            }
            public System.String ParamedicID
            {
                get
                {
                    System.String data = entity.ParamedicID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ParamedicID = null;
                    else entity.ParamedicID = Convert.ToString(value);
                }
            }
            public System.String SOAPEDate
            {
                get
                {
                    System.DateTime? data = entity.SOAPEDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SOAPEDate = null;
                    else entity.SOAPEDate = Convert.ToDateTime(value);
                }
            }
            public System.String SOAPETime
            {
                get
                {
                    System.String data = entity.SOAPETime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SOAPETime = null;
                    else entity.SOAPETime = Convert.ToString(value);
                }
            }
            public System.String Subjective
            {
                get
                {
                    System.String data = entity.Subjective;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Subjective = null;
                    else entity.Subjective = Convert.ToString(value);
                }
            }
            public System.String Objective
            {
                get
                {
                    System.String data = entity.Objective;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Objective = null;
                    else entity.Objective = Convert.ToString(value);
                }
            }
            public System.String Assesment
            {
                get
                {
                    System.String data = entity.Assesment;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Assesment = null;
                    else entity.Assesment = Convert.ToString(value);
                }
            }
            public System.String Planning
            {
                get
                {
                    System.String data = entity.Planning;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Planning = null;
                    else entity.Planning = Convert.ToString(value);
                }
            }
            public System.String Evaluation
            {
                get
                {
                    System.String data = entity.Evaluation;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Evaluation = null;
                    else entity.Evaluation = Convert.ToString(value);
                }
            }
            public System.String AttendingNotes
            {
                get
                {
                    System.String data = entity.AttendingNotes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.AttendingNotes = null;
                    else entity.AttendingNotes = Convert.ToString(value);
                }
            }
            public System.String IsSummary
            {
                get
                {
                    System.Boolean? data = entity.IsSummary;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsSummary = null;
                    else entity.IsSummary = Convert.ToBoolean(value);
                }
            }
            public System.String IsVoid
            {
                get
                {
                    System.Boolean? data = entity.IsVoid;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsVoid = null;
                    else entity.IsVoid = Convert.ToBoolean(value);
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
            public System.String ServiceUnitID
            {
                get
                {
                    System.String data = entity.ServiceUnitID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ServiceUnitID = null;
                    else entity.ServiceUnitID = Convert.ToString(value);
                }
            }
            public System.String IsInformConcern
            {
                get
                {
                    System.Boolean? data = entity.IsInformConcern;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsInformConcern = null;
                    else entity.IsInformConcern = Convert.ToBoolean(value);
                }
            }
            public System.String Imported
            {
                get
                {
                    System.Boolean? data = entity.Imported;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Imported = null;
                    else entity.Imported = Convert.ToBoolean(value);
                }
            }
            public System.String ImportedDateTime
            {
                get
                {
                    System.DateTime? data = entity.ImportedDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ImportedDateTime = null;
                    else entity.ImportedDateTime = Convert.ToDateTime(value);
                }
            }
            public System.String ToRegistrationInfoMedicID
            {
                get
                {
                    System.String data = entity.ToRegistrationInfoMedicID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ToRegistrationInfoMedicID = null;
                    else entity.ToRegistrationInfoMedicID = Convert.ToString(value);
                }
            }
            private esEpisodeSOAPE entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esEpisodeSOAPEQuery query)
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
                throw new Exception("esEpisodeSOAPE can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class EpisodeSOAPE : esEpisodeSOAPE
    {
    }

    [Serializable]
    abstract public class esEpisodeSOAPEQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return EpisodeSOAPEMetadata.Meta();
            }
        }

        public esQueryItem RegistrationNo
        {
            get
            {
                return new esQueryItem(this, EpisodeSOAPEMetadata.ColumnNames.RegistrationNo, esSystemType.String);
            }
        }

        public esQueryItem SequenceNo
        {
            get
            {
                return new esQueryItem(this, EpisodeSOAPEMetadata.ColumnNames.SequenceNo, esSystemType.String);
            }
        }

        public esQueryItem ParamedicID
        {
            get
            {
                return new esQueryItem(this, EpisodeSOAPEMetadata.ColumnNames.ParamedicID, esSystemType.String);
            }
        }

        public esQueryItem SOAPEDate
        {
            get
            {
                return new esQueryItem(this, EpisodeSOAPEMetadata.ColumnNames.SOAPEDate, esSystemType.DateTime);
            }
        }

        public esQueryItem SOAPETime
        {
            get
            {
                return new esQueryItem(this, EpisodeSOAPEMetadata.ColumnNames.SOAPETime, esSystemType.String);
            }
        }

        public esQueryItem Subjective
        {
            get
            {
                return new esQueryItem(this, EpisodeSOAPEMetadata.ColumnNames.Subjective, esSystemType.String);
            }
        }

        public esQueryItem Objective
        {
            get
            {
                return new esQueryItem(this, EpisodeSOAPEMetadata.ColumnNames.Objective, esSystemType.String);
            }
        }

        public esQueryItem Assesment
        {
            get
            {
                return new esQueryItem(this, EpisodeSOAPEMetadata.ColumnNames.Assesment, esSystemType.String);
            }
        }

        public esQueryItem Planning
        {
            get
            {
                return new esQueryItem(this, EpisodeSOAPEMetadata.ColumnNames.Planning, esSystemType.String);
            }
        }

        public esQueryItem Evaluation
        {
            get
            {
                return new esQueryItem(this, EpisodeSOAPEMetadata.ColumnNames.Evaluation, esSystemType.String);
            }
        }

        public esQueryItem AttendingNotes
        {
            get
            {
                return new esQueryItem(this, EpisodeSOAPEMetadata.ColumnNames.AttendingNotes, esSystemType.String);
            }
        }

        public esQueryItem IsSummary
        {
            get
            {
                return new esQueryItem(this, EpisodeSOAPEMetadata.ColumnNames.IsSummary, esSystemType.Boolean);
            }
        }

        public esQueryItem IsVoid
        {
            get
            {
                return new esQueryItem(this, EpisodeSOAPEMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, EpisodeSOAPEMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, EpisodeSOAPEMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem ServiceUnitID
        {
            get
            {
                return new esQueryItem(this, EpisodeSOAPEMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
            }
        }

        public esQueryItem IsInformConcern
        {
            get
            {
                return new esQueryItem(this, EpisodeSOAPEMetadata.ColumnNames.IsInformConcern, esSystemType.Boolean);
            }
        }

        public esQueryItem BodyImage
        {
            get
            {
                return new esQueryItem(this, EpisodeSOAPEMetadata.ColumnNames.BodyImage, esSystemType.ByteArray);
            }
        }

        public esQueryItem Imported
        {
            get
            {
                return new esQueryItem(this, EpisodeSOAPEMetadata.ColumnNames.Imported, esSystemType.Boolean);
            }
        }

        public esQueryItem ImportedDateTime
        {
            get
            {
                return new esQueryItem(this, EpisodeSOAPEMetadata.ColumnNames.ImportedDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem ToRegistrationInfoMedicID
        {
            get
            {
                return new esQueryItem(this, EpisodeSOAPEMetadata.ColumnNames.ToRegistrationInfoMedicID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("EpisodeSOAPECollection")]
    public partial class EpisodeSOAPECollection : esEpisodeSOAPECollection, IEnumerable<EpisodeSOAPE>
    {
        public EpisodeSOAPECollection()
        {

        }

        public static implicit operator List<EpisodeSOAPE>(EpisodeSOAPECollection coll)
        {
            List<EpisodeSOAPE> list = new List<EpisodeSOAPE>();

            foreach (EpisodeSOAPE emp in coll)
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
                return EpisodeSOAPEMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new EpisodeSOAPEQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new EpisodeSOAPE(row);
        }

        override protected esEntity CreateEntity()
        {
            return new EpisodeSOAPE();
        }

        #endregion

        [BrowsableAttribute(false)]
        public EpisodeSOAPEQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new EpisodeSOAPEQuery();
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
        public bool Load(EpisodeSOAPEQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public EpisodeSOAPE AddNew()
        {
            EpisodeSOAPE entity = base.AddNewEntity() as EpisodeSOAPE;

            return entity;
        }
        public EpisodeSOAPE FindByPrimaryKey(String registrationNo, String sequenceNo)
        {
            return base.FindByPrimaryKey(registrationNo, sequenceNo) as EpisodeSOAPE;
        }

        #region IEnumerable< EpisodeSOAPE> Members

        IEnumerator<EpisodeSOAPE> IEnumerable<EpisodeSOAPE>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as EpisodeSOAPE;
            }
        }

        #endregion

        private EpisodeSOAPEQuery query;
    }


    /// <summary>
    /// Encapsulates the 'EpisodeSOAPE' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("EpisodeSOAPE ({RegistrationNo, SequenceNo})")]
    [Serializable]
    public partial class EpisodeSOAPE : esEpisodeSOAPE
    {
        public EpisodeSOAPE()
        {
        }

        public EpisodeSOAPE(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return EpisodeSOAPEMetadata.Meta();
            }
        }

        override protected esEpisodeSOAPEQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new EpisodeSOAPEQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public EpisodeSOAPEQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new EpisodeSOAPEQuery();
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
        public bool Load(EpisodeSOAPEQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private EpisodeSOAPEQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class EpisodeSOAPEQuery : esEpisodeSOAPEQuery
    {
        public EpisodeSOAPEQuery()
        {

        }

        public EpisodeSOAPEQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "EpisodeSOAPEQuery";
        }
    }

    [Serializable]
    public partial class EpisodeSOAPEMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected EpisodeSOAPEMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(EpisodeSOAPEMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = EpisodeSOAPEMetadata.PropertyNames.RegistrationNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(EpisodeSOAPEMetadata.ColumnNames.SequenceNo, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = EpisodeSOAPEMetadata.PropertyNames.SequenceNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 3;
            c.HasDefault = true;
            c.Default = @"('000')";
            _columns.Add(c);

            c = new esColumnMetadata(EpisodeSOAPEMetadata.ColumnNames.ParamedicID, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = EpisodeSOAPEMetadata.PropertyNames.ParamedicID;
            c.CharacterMaxLength = 10;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(EpisodeSOAPEMetadata.ColumnNames.SOAPEDate, 3, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = EpisodeSOAPEMetadata.PropertyNames.SOAPEDate;
            c.HasDefault = true;
            c.Default = @"(getdate())";
            _columns.Add(c);

            c = new esColumnMetadata(EpisodeSOAPEMetadata.ColumnNames.SOAPETime, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = EpisodeSOAPEMetadata.PropertyNames.SOAPETime;
            c.CharacterMaxLength = 5;
            c.HasDefault = true;
            c.Default = @"('00:00')";
            _columns.Add(c);

            c = new esColumnMetadata(EpisodeSOAPEMetadata.ColumnNames.Subjective, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = EpisodeSOAPEMetadata.PropertyNames.Subjective;
            c.CharacterMaxLength = 4000;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(EpisodeSOAPEMetadata.ColumnNames.Objective, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = EpisodeSOAPEMetadata.PropertyNames.Objective;
            c.CharacterMaxLength = 4000;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(EpisodeSOAPEMetadata.ColumnNames.Assesment, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = EpisodeSOAPEMetadata.PropertyNames.Assesment;
            c.CharacterMaxLength = 4000;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(EpisodeSOAPEMetadata.ColumnNames.Planning, 8, typeof(System.String), esSystemType.String);
            c.PropertyName = EpisodeSOAPEMetadata.PropertyNames.Planning;
            c.CharacterMaxLength = 4000;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(EpisodeSOAPEMetadata.ColumnNames.Evaluation, 9, typeof(System.String), esSystemType.String);
            c.PropertyName = EpisodeSOAPEMetadata.PropertyNames.Evaluation;
            c.CharacterMaxLength = 4000;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(EpisodeSOAPEMetadata.ColumnNames.AttendingNotes, 10, typeof(System.String), esSystemType.String);
            c.PropertyName = EpisodeSOAPEMetadata.PropertyNames.AttendingNotes;
            c.CharacterMaxLength = 4000;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(EpisodeSOAPEMetadata.ColumnNames.IsSummary, 11, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = EpisodeSOAPEMetadata.PropertyNames.IsSummary;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(EpisodeSOAPEMetadata.ColumnNames.IsVoid, 12, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = EpisodeSOAPEMetadata.PropertyNames.IsVoid;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(EpisodeSOAPEMetadata.ColumnNames.LastUpdateDateTime, 13, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = EpisodeSOAPEMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(EpisodeSOAPEMetadata.ColumnNames.LastUpdateByUserID, 14, typeof(System.String), esSystemType.String);
            c.PropertyName = EpisodeSOAPEMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 40;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(EpisodeSOAPEMetadata.ColumnNames.ServiceUnitID, 15, typeof(System.String), esSystemType.String);
            c.PropertyName = EpisodeSOAPEMetadata.PropertyNames.ServiceUnitID;
            c.CharacterMaxLength = 10;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(EpisodeSOAPEMetadata.ColumnNames.IsInformConcern, 16, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = EpisodeSOAPEMetadata.PropertyNames.IsInformConcern;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(EpisodeSOAPEMetadata.ColumnNames.BodyImage, 17, typeof(System.Byte[]), esSystemType.ByteArray);
            c.PropertyName = EpisodeSOAPEMetadata.PropertyNames.BodyImage;
            c.NumericPrecision = 0;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(EpisodeSOAPEMetadata.ColumnNames.Imported, 18, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = EpisodeSOAPEMetadata.PropertyNames.Imported;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(EpisodeSOAPEMetadata.ColumnNames.ImportedDateTime, 19, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = EpisodeSOAPEMetadata.PropertyNames.ImportedDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(EpisodeSOAPEMetadata.ColumnNames.ToRegistrationInfoMedicID, 20, typeof(System.String), esSystemType.String);
            c.PropertyName = EpisodeSOAPEMetadata.PropertyNames.ToRegistrationInfoMedicID;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public EpisodeSOAPEMetadata Meta()
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
            public const string RegistrationNo = "RegistrationNo";
            public const string SequenceNo = "SequenceNo";
            public const string ParamedicID = "ParamedicID";
            public const string SOAPEDate = "SOAPEDate";
            public const string SOAPETime = "SOAPETime";
            public const string Subjective = "Subjective";
            public const string Objective = "Objective";
            public const string Assesment = "Assesment";
            public const string Planning = "Planning";
            public const string Evaluation = "Evaluation";
            public const string AttendingNotes = "AttendingNotes";
            public const string IsSummary = "IsSummary";
            public const string IsVoid = "IsVoid";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string ServiceUnitID = "ServiceUnitID";
            public const string IsInformConcern = "IsInformConcern";
            public const string BodyImage = "BodyImage";
            public const string Imported = "Imported";
            public const string ImportedDateTime = "ImportedDateTime";
            public const string ToRegistrationInfoMedicID = "ToRegistrationInfoMedicID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string RegistrationNo = "RegistrationNo";
            public const string SequenceNo = "SequenceNo";
            public const string ParamedicID = "ParamedicID";
            public const string SOAPEDate = "SOAPEDate";
            public const string SOAPETime = "SOAPETime";
            public const string Subjective = "Subjective";
            public const string Objective = "Objective";
            public const string Assesment = "Assesment";
            public const string Planning = "Planning";
            public const string Evaluation = "Evaluation";
            public const string AttendingNotes = "AttendingNotes";
            public const string IsSummary = "IsSummary";
            public const string IsVoid = "IsVoid";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string ServiceUnitID = "ServiceUnitID";
            public const string IsInformConcern = "IsInformConcern";
            public const string BodyImage = "BodyImage";
            public const string Imported = "Imported";
            public const string ImportedDateTime = "ImportedDateTime";
            public const string ToRegistrationInfoMedicID = "ToRegistrationInfoMedicID";
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
            lock (typeof(EpisodeSOAPEMetadata))
            {
                if (EpisodeSOAPEMetadata.mapDelegates == null)
                {
                    EpisodeSOAPEMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (EpisodeSOAPEMetadata.meta == null)
                {
                    EpisodeSOAPEMetadata.meta = new EpisodeSOAPEMetadata();
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

                meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SequenceNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SOAPEDate", new esTypeMap("smalldatetime", "System.DateTime"));
                meta.AddTypeMap("SOAPETime", new esTypeMap("char", "System.String"));
                meta.AddTypeMap("Subjective", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Objective", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Assesment", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Planning", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Evaluation", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("AttendingNotes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsSummary", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsInformConcern", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("BodyImage", new esTypeMap("image", "System.Byte[]"));
                meta.AddTypeMap("Imported", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("ImportedDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("ToRegistrationInfoMedicID", new esTypeMap("varchar", "System.String"));


                meta.Source = "EpisodeSOAPE";
                meta.Destination = "EpisodeSOAPE";
                meta.spInsert = "proc_EpisodeSOAPEInsert";
                meta.spUpdate = "proc_EpisodeSOAPEUpdate";
                meta.spDelete = "proc_EpisodeSOAPEDelete";
                meta.spLoadAll = "proc_EpisodeSOAPELoadAll";
                meta.spLoadByPrimaryKey = "proc_EpisodeSOAPELoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private EpisodeSOAPEMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
