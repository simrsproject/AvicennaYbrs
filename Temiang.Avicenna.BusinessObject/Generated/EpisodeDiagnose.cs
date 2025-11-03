/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 9/19/2016 2:28:46 PM
===============================================================================
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.ComponentModel;
using System.Xml.Serialization;


using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;



namespace Temiang.Avicenna.BusinessObject
{

    [Serializable]
    abstract public class esEpisodeDiagnoseCollection : esEntityCollectionWAuditLog
    {
        public esEpisodeDiagnoseCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "EpisodeDiagnoseCollection";
        }

        #region Query Logic
        protected void InitQuery(esEpisodeDiagnoseQuery query)
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
            this.InitQuery(query as esEpisodeDiagnoseQuery);
        }
        #endregion

        virtual public EpisodeDiagnose DetachEntity(EpisodeDiagnose entity)
        {
            return base.DetachEntity(entity) as EpisodeDiagnose;
        }

        virtual public EpisodeDiagnose AttachEntity(EpisodeDiagnose entity)
        {
            return base.AttachEntity(entity) as EpisodeDiagnose;
        }

        virtual public void Combine(EpisodeDiagnoseCollection collection)
        {
            base.Combine(collection);
        }

        new public EpisodeDiagnose this[int index]
        {
            get
            {
                return base[index] as EpisodeDiagnose;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(EpisodeDiagnose);
        }
    }



    [Serializable]
    abstract public class esEpisodeDiagnose : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esEpisodeDiagnoseQuery GetDynamicQuery()
        {
            return null;
        }

        public esEpisodeDiagnose()
        {

        }

        public esEpisodeDiagnose(DataRow row)
            : base(row)
        {

        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(System.String registrationNo, System.String sequenceNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(registrationNo, sequenceNo);
            else
                return LoadByPrimaryKeyStoredProcedure(registrationNo, sequenceNo);
        }

        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String registrationNo, System.String sequenceNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(registrationNo, sequenceNo);
            else
                return LoadByPrimaryKeyStoredProcedure(registrationNo, sequenceNo);
        }

        private bool LoadByPrimaryKeyDynamic(System.String registrationNo, System.String sequenceNo)
        {
            esEpisodeDiagnoseQuery query = this.GetDynamicQuery();
            query.Where(query.RegistrationNo == registrationNo, query.SequenceNo == sequenceNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(System.String registrationNo, System.String sequenceNo)
        {
            esParameters parms = new esParameters();
            parms.Add("RegistrationNo", registrationNo); parms.Add("SequenceNo", sequenceNo);
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
                        case "DiagnoseID": this.str.DiagnoseID = (string)value; break;
                        case "SRDiagnoseType": this.str.SRDiagnoseType = (string)value; break;
                        case "DiagnosisText": this.str.DiagnosisText = (string)value; break;
                        case "MorphologyID": this.str.MorphologyID = (string)value; break;
                        case "ParamedicID": this.str.ParamedicID = (string)value; break;
                        case "IsAcuteDisease": this.str.IsAcuteDisease = (string)value; break;
                        case "IsChronicDisease": this.str.IsChronicDisease = (string)value; break;
                        case "IsOldCase": this.str.IsOldCase = (string)value; break;
                        case "IsConfirmed": this.str.IsConfirmed = (string)value; break;
                        case "IsVoid": this.str.IsVoid = (string)value; break;
                        case "Notes": this.str.Notes = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "ExternalCauseID": this.str.ExternalCauseID = (string)value; break;
                        case "CreateByUserID": this.str.CreateByUserID = (string)value; break;
                        case "CreateDateTime": this.str.CreateDateTime = (string)value; break;
                        case "DiagnoseSynonym": this.str.DiagnoseSynonym = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "IsAcuteDisease":

                            if (value == null || value is System.Boolean)
                                this.IsAcuteDisease = (System.Boolean?)value;
                            break;

                        case "IsChronicDisease":

                            if (value == null || value is System.Boolean)
                                this.IsChronicDisease = (System.Boolean?)value;
                            break;

                        case "IsOldCase":

                            if (value == null || value is System.Boolean)
                                this.IsOldCase = (System.Boolean?)value;
                            break;

                        case "IsConfirmed":

                            if (value == null || value is System.Boolean)
                                this.IsConfirmed = (System.Boolean?)value;
                            break;

                        case "IsVoid":

                            if (value == null || value is System.Boolean)
                                this.IsVoid = (System.Boolean?)value;
                            break;

                        case "LastUpdateDateTime":

                            if (value == null || value is System.DateTime)
                                this.LastUpdateDateTime = (System.DateTime?)value;
                            break;

                        case "CreateDateTime":

                            if (value == null || value is System.DateTime)
                                this.CreateDateTime = (System.DateTime?)value;
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
        /// Maps to EpisodeDiagnose.RegistrationNo
        /// </summary>
        virtual public System.String RegistrationNo
        {
            get
            {
                return base.GetSystemString(EpisodeDiagnoseMetadata.ColumnNames.RegistrationNo);
            }

            set
            {
                base.SetSystemString(EpisodeDiagnoseMetadata.ColumnNames.RegistrationNo, value);
            }
        }

        /// <summary>
        /// Maps to EpisodeDiagnose.SequenceNo
        /// </summary>
        virtual public System.String SequenceNo
        {
            get
            {
                return base.GetSystemString(EpisodeDiagnoseMetadata.ColumnNames.SequenceNo);
            }

            set
            {
                base.SetSystemString(EpisodeDiagnoseMetadata.ColumnNames.SequenceNo, value);
            }
        }

        /// <summary>
        /// Maps to EpisodeDiagnose.DiagnoseID
        /// </summary>
        virtual public System.String DiagnoseID
        {
            get
            {
                return base.GetSystemString(EpisodeDiagnoseMetadata.ColumnNames.DiagnoseID);
            }

            set
            {
                base.SetSystemString(EpisodeDiagnoseMetadata.ColumnNames.DiagnoseID, value);
            }
        }

        /// <summary>
        /// Maps to EpisodeDiagnose.SRDiagnoseType
        /// </summary>
        virtual public System.String SRDiagnoseType
        {
            get
            {
                return base.GetSystemString(EpisodeDiagnoseMetadata.ColumnNames.SRDiagnoseType);
            }

            set
            {
                base.SetSystemString(EpisodeDiagnoseMetadata.ColumnNames.SRDiagnoseType, value);
            }
        }

        /// <summary>
        /// Maps to EpisodeDiagnose.DiagnosisText
        /// </summary>
        virtual public System.String DiagnosisText
        {
            get
            {
                return base.GetSystemString(EpisodeDiagnoseMetadata.ColumnNames.DiagnosisText);
            }

            set
            {
                base.SetSystemString(EpisodeDiagnoseMetadata.ColumnNames.DiagnosisText, value);
            }
        }

        /// <summary>
        /// Maps to EpisodeDiagnose.MorphologyID
        /// </summary>
        virtual public System.String MorphologyID
        {
            get
            {
                return base.GetSystemString(EpisodeDiagnoseMetadata.ColumnNames.MorphologyID);
            }

            set
            {
                base.SetSystemString(EpisodeDiagnoseMetadata.ColumnNames.MorphologyID, value);
            }
        }

        /// <summary>
        /// Maps to EpisodeDiagnose.ParamedicID
        /// </summary>
        virtual public System.String ParamedicID
        {
            get
            {
                return base.GetSystemString(EpisodeDiagnoseMetadata.ColumnNames.ParamedicID);
            }

            set
            {
                base.SetSystemString(EpisodeDiagnoseMetadata.ColumnNames.ParamedicID, value);
            }
        }

        /// <summary>
        /// Maps to EpisodeDiagnose.IsAcuteDisease
        /// </summary>
        virtual public System.Boolean? IsAcuteDisease
        {
            get
            {
                return base.GetSystemBoolean(EpisodeDiagnoseMetadata.ColumnNames.IsAcuteDisease);
            }

            set
            {
                base.SetSystemBoolean(EpisodeDiagnoseMetadata.ColumnNames.IsAcuteDisease, value);
            }
        }

        /// <summary>
        /// Maps to EpisodeDiagnose.IsChronicDisease
        /// </summary>
        virtual public System.Boolean? IsChronicDisease
        {
            get
            {
                return base.GetSystemBoolean(EpisodeDiagnoseMetadata.ColumnNames.IsChronicDisease);
            }

            set
            {
                base.SetSystemBoolean(EpisodeDiagnoseMetadata.ColumnNames.IsChronicDisease, value);
            }
        }

        /// <summary>
        /// Maps to EpisodeDiagnose.IsOldCase
        /// </summary>
        virtual public System.Boolean? IsOldCase
        {
            get
            {
                return base.GetSystemBoolean(EpisodeDiagnoseMetadata.ColumnNames.IsOldCase);
            }

            set
            {
                base.SetSystemBoolean(EpisodeDiagnoseMetadata.ColumnNames.IsOldCase, value);
            }
        }

        /// <summary>
        /// Maps to EpisodeDiagnose.IsConfirmed
        /// </summary>
        virtual public System.Boolean? IsConfirmed
        {
            get
            {
                return base.GetSystemBoolean(EpisodeDiagnoseMetadata.ColumnNames.IsConfirmed);
            }

            set
            {
                base.SetSystemBoolean(EpisodeDiagnoseMetadata.ColumnNames.IsConfirmed, value);
            }
        }

        /// <summary>
        /// Maps to EpisodeDiagnose.IsVoid
        /// </summary>
        virtual public System.Boolean? IsVoid
        {
            get
            {
                return base.GetSystemBoolean(EpisodeDiagnoseMetadata.ColumnNames.IsVoid);
            }

            set
            {
                base.SetSystemBoolean(EpisodeDiagnoseMetadata.ColumnNames.IsVoid, value);
            }
        }

        /// <summary>
        /// Maps to EpisodeDiagnose.Notes
        /// </summary>
        virtual public System.String Notes
        {
            get
            {
                return base.GetSystemString(EpisodeDiagnoseMetadata.ColumnNames.Notes);
            }

            set
            {
                base.SetSystemString(EpisodeDiagnoseMetadata.ColumnNames.Notes, value);
            }
        }

        /// <summary>
        /// Maps to EpisodeDiagnose.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(EpisodeDiagnoseMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(EpisodeDiagnoseMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to EpisodeDiagnose.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(EpisodeDiagnoseMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(EpisodeDiagnoseMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }

        /// <summary>
        /// Maps to EpisodeDiagnose.ExternalCauseID
        /// </summary>
        virtual public System.String ExternalCauseID
        {
            get
            {
                return base.GetSystemString(EpisodeDiagnoseMetadata.ColumnNames.ExternalCauseID);
            }

            set
            {
                base.SetSystemString(EpisodeDiagnoseMetadata.ColumnNames.ExternalCauseID, value);
            }
        }

        /// <summary>
        /// Maps to EpisodeDiagnose.CreateByUserID
        /// </summary>
        virtual public System.String CreateByUserID
        {
            get
            {
                return base.GetSystemString(EpisodeDiagnoseMetadata.ColumnNames.CreateByUserID);
            }

            set
            {
                base.SetSystemString(EpisodeDiagnoseMetadata.ColumnNames.CreateByUserID, value);
            }
        }

        /// <summary>
        /// Maps to EpisodeDiagnose.CreateDateTime
        /// </summary>
        virtual public System.DateTime? CreateDateTime
        {
            get
            {
                return base.GetSystemDateTime(EpisodeDiagnoseMetadata.ColumnNames.CreateDateTime);
            }

            set
            {
                base.SetSystemDateTime(EpisodeDiagnoseMetadata.ColumnNames.CreateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to EpisodeDiagnose.DiagnoseSynonym
        /// </summary>
        virtual public System.String DiagnoseSynonym
        {
            get
            {
                return base.GetSystemString(EpisodeDiagnoseMetadata.ColumnNames.DiagnoseSynonym);
            }

            set
            {
                base.SetSystemString(EpisodeDiagnoseMetadata.ColumnNames.DiagnoseSynonym, value);
            }
        }
        #endregion

        #region String Properties


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
            public esStrings(esEpisodeDiagnose entity)
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

            public System.String DiagnoseID
            {
                get
                {
                    System.String data = entity.DiagnoseID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DiagnoseID = null;
                    else entity.DiagnoseID = Convert.ToString(value);
                }
            }

            public System.String SRDiagnoseType
            {
                get
                {
                    System.String data = entity.SRDiagnoseType;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRDiagnoseType = null;
                    else entity.SRDiagnoseType = Convert.ToString(value);
                }
            }

            public System.String DiagnosisText
            {
                get
                {
                    System.String data = entity.DiagnosisText;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DiagnosisText = null;
                    else entity.DiagnosisText = Convert.ToString(value);
                }
            }

            public System.String MorphologyID
            {
                get
                {
                    System.String data = entity.MorphologyID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.MorphologyID = null;
                    else entity.MorphologyID = Convert.ToString(value);
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

            public System.String IsAcuteDisease
            {
                get
                {
                    System.Boolean? data = entity.IsAcuteDisease;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsAcuteDisease = null;
                    else entity.IsAcuteDisease = Convert.ToBoolean(value);
                }
            }

            public System.String IsChronicDisease
            {
                get
                {
                    System.Boolean? data = entity.IsChronicDisease;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsChronicDisease = null;
                    else entity.IsChronicDisease = Convert.ToBoolean(value);
                }
            }

            public System.String IsOldCase
            {
                get
                {
                    System.Boolean? data = entity.IsOldCase;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsOldCase = null;
                    else entity.IsOldCase = Convert.ToBoolean(value);
                }
            }

            public System.String IsConfirmed
            {
                get
                {
                    System.Boolean? data = entity.IsConfirmed;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsConfirmed = null;
                    else entity.IsConfirmed = Convert.ToBoolean(value);
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

            public System.String ExternalCauseID
            {
                get
                {
                    System.String data = entity.ExternalCauseID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ExternalCauseID = null;
                    else entity.ExternalCauseID = Convert.ToString(value);
                }
            }

            public System.String CreateByUserID
            {
                get
                {
                    System.String data = entity.CreateByUserID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CreateByUserID = null;
                    else entity.CreateByUserID = Convert.ToString(value);
                }
            }

            public System.String CreateDateTime
            {
                get
                {
                    System.DateTime? data = entity.CreateDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CreateDateTime = null;
                    else entity.CreateDateTime = Convert.ToDateTime(value);
                }
            }

            public System.String DiagnoseSynonym
            {
                get
                {
                    System.String data = entity.DiagnoseSynonym;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DiagnoseSynonym = null;
                    else entity.DiagnoseSynonym = Convert.ToString(value);
                }
            }

            private esEpisodeDiagnose entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esEpisodeDiagnoseQuery query)
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
                throw new Exception("esEpisodeDiagnose can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    [Serializable]
    abstract public class esEpisodeDiagnoseQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return EpisodeDiagnoseMetadata.Meta();
            }
        }


        public esQueryItem RegistrationNo
        {
            get
            {
                return new esQueryItem(this, EpisodeDiagnoseMetadata.ColumnNames.RegistrationNo, esSystemType.String);
            }
        }

        public esQueryItem SequenceNo
        {
            get
            {
                return new esQueryItem(this, EpisodeDiagnoseMetadata.ColumnNames.SequenceNo, esSystemType.String);
            }
        }

        public esQueryItem DiagnoseID
        {
            get
            {
                return new esQueryItem(this, EpisodeDiagnoseMetadata.ColumnNames.DiagnoseID, esSystemType.String);
            }
        }

        public esQueryItem SRDiagnoseType
        {
            get
            {
                return new esQueryItem(this, EpisodeDiagnoseMetadata.ColumnNames.SRDiagnoseType, esSystemType.String);
            }
        }

        public esQueryItem DiagnosisText
        {
            get
            {
                return new esQueryItem(this, EpisodeDiagnoseMetadata.ColumnNames.DiagnosisText, esSystemType.String);
            }
        }

        public esQueryItem MorphologyID
        {
            get
            {
                return new esQueryItem(this, EpisodeDiagnoseMetadata.ColumnNames.MorphologyID, esSystemType.String);
            }
        }

        public esQueryItem ParamedicID
        {
            get
            {
                return new esQueryItem(this, EpisodeDiagnoseMetadata.ColumnNames.ParamedicID, esSystemType.String);
            }
        }

        public esQueryItem IsAcuteDisease
        {
            get
            {
                return new esQueryItem(this, EpisodeDiagnoseMetadata.ColumnNames.IsAcuteDisease, esSystemType.Boolean);
            }
        }

        public esQueryItem IsChronicDisease
        {
            get
            {
                return new esQueryItem(this, EpisodeDiagnoseMetadata.ColumnNames.IsChronicDisease, esSystemType.Boolean);
            }
        }

        public esQueryItem IsOldCase
        {
            get
            {
                return new esQueryItem(this, EpisodeDiagnoseMetadata.ColumnNames.IsOldCase, esSystemType.Boolean);
            }
        }

        public esQueryItem IsConfirmed
        {
            get
            {
                return new esQueryItem(this, EpisodeDiagnoseMetadata.ColumnNames.IsConfirmed, esSystemType.Boolean);
            }
        }

        public esQueryItem IsVoid
        {
            get
            {
                return new esQueryItem(this, EpisodeDiagnoseMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
            }
        }

        public esQueryItem Notes
        {
            get
            {
                return new esQueryItem(this, EpisodeDiagnoseMetadata.ColumnNames.Notes, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, EpisodeDiagnoseMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, EpisodeDiagnoseMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem ExternalCauseID
        {
            get
            {
                return new esQueryItem(this, EpisodeDiagnoseMetadata.ColumnNames.ExternalCauseID, esSystemType.String);
            }
        }

        public esQueryItem CreateByUserID
        {
            get
            {
                return new esQueryItem(this, EpisodeDiagnoseMetadata.ColumnNames.CreateByUserID, esSystemType.String);
            }
        }

        public esQueryItem CreateDateTime
        {
            get
            {
                return new esQueryItem(this, EpisodeDiagnoseMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem DiagnoseSynonym
        {
            get
            {
                return new esQueryItem(this, EpisodeDiagnoseMetadata.ColumnNames.DiagnoseSynonym, esSystemType.String);
            }
        }
    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("EpisodeDiagnoseCollection")]
    public partial class EpisodeDiagnoseCollection : esEpisodeDiagnoseCollection, IEnumerable<EpisodeDiagnose>
    {
        public EpisodeDiagnoseCollection()
        {

        }

        public static implicit operator List<EpisodeDiagnose>(EpisodeDiagnoseCollection coll)
        {
            List<EpisodeDiagnose> list = new List<EpisodeDiagnose>();

            foreach (EpisodeDiagnose emp in coll)
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
                return EpisodeDiagnoseMetadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new EpisodeDiagnoseQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new EpisodeDiagnose(row);
        }

        override protected esEntity CreateEntity()
        {
            return new EpisodeDiagnose();
        }


        #endregion


        [BrowsableAttribute(false)]
        public EpisodeDiagnoseQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new EpisodeDiagnoseQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(EpisodeDiagnoseQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public EpisodeDiagnose AddNew()
        {
            EpisodeDiagnose entity = base.AddNewEntity() as EpisodeDiagnose;

            return entity;
        }

        public EpisodeDiagnose FindByPrimaryKey(System.String registrationNo, System.String sequenceNo)
        {
            return base.FindByPrimaryKey(registrationNo, sequenceNo) as EpisodeDiagnose;
        }


        #region IEnumerable<EpisodeDiagnose> Members

        IEnumerator<EpisodeDiagnose> IEnumerable<EpisodeDiagnose>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as EpisodeDiagnose;
            }
        }

        #endregion

        private EpisodeDiagnoseQuery query;
    }


    /// <summary>
    /// Encapsulates the 'EpisodeDiagnose' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("EpisodeDiagnose ({RegistrationNo},{SequenceNo})")]
    [Serializable]
    public partial class EpisodeDiagnose : esEpisodeDiagnose
    {
        public EpisodeDiagnose()
        {

        }

        public EpisodeDiagnose(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return EpisodeDiagnoseMetadata.Meta();
            }
        }



        override protected esEpisodeDiagnoseQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new EpisodeDiagnoseQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public EpisodeDiagnoseQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new EpisodeDiagnoseQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(EpisodeDiagnoseQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private EpisodeDiagnoseQuery query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class EpisodeDiagnoseQuery : esEpisodeDiagnoseQuery
    {
        public EpisodeDiagnoseQuery()
        {

        }

        public EpisodeDiagnoseQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "EpisodeDiagnoseQuery";
        }


    }


    [Serializable]
    public partial class EpisodeDiagnoseMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected EpisodeDiagnoseMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(EpisodeDiagnoseMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = EpisodeDiagnoseMetadata.PropertyNames.RegistrationNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(EpisodeDiagnoseMetadata.ColumnNames.SequenceNo, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = EpisodeDiagnoseMetadata.PropertyNames.SequenceNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 3;
            c.HasDefault = true;
            c.Default = @"('000')";
            _columns.Add(c);

            c = new esColumnMetadata(EpisodeDiagnoseMetadata.ColumnNames.DiagnoseID, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = EpisodeDiagnoseMetadata.PropertyNames.DiagnoseID;
            c.CharacterMaxLength = 20;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(EpisodeDiagnoseMetadata.ColumnNames.SRDiagnoseType, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = EpisodeDiagnoseMetadata.PropertyNames.SRDiagnoseType;
            c.CharacterMaxLength = 20;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(EpisodeDiagnoseMetadata.ColumnNames.DiagnosisText, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = EpisodeDiagnoseMetadata.PropertyNames.DiagnosisText;
            c.CharacterMaxLength = 4000;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(EpisodeDiagnoseMetadata.ColumnNames.MorphologyID, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = EpisodeDiagnoseMetadata.PropertyNames.MorphologyID;
            c.CharacterMaxLength = 10;
            c.HasDefault = true;
            c.Default = @"('')";
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(EpisodeDiagnoseMetadata.ColumnNames.ParamedicID, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = EpisodeDiagnoseMetadata.PropertyNames.ParamedicID;
            c.CharacterMaxLength = 10;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(EpisodeDiagnoseMetadata.ColumnNames.IsAcuteDisease, 7, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = EpisodeDiagnoseMetadata.PropertyNames.IsAcuteDisease;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(EpisodeDiagnoseMetadata.ColumnNames.IsChronicDisease, 8, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = EpisodeDiagnoseMetadata.PropertyNames.IsChronicDisease;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(EpisodeDiagnoseMetadata.ColumnNames.IsOldCase, 9, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = EpisodeDiagnoseMetadata.PropertyNames.IsOldCase;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(EpisodeDiagnoseMetadata.ColumnNames.IsConfirmed, 10, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = EpisodeDiagnoseMetadata.PropertyNames.IsConfirmed;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(EpisodeDiagnoseMetadata.ColumnNames.IsVoid, 11, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = EpisodeDiagnoseMetadata.PropertyNames.IsVoid;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(EpisodeDiagnoseMetadata.ColumnNames.Notes, 12, typeof(System.String), esSystemType.String);
            c.PropertyName = EpisodeDiagnoseMetadata.PropertyNames.Notes;
            c.CharacterMaxLength = 4000;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(EpisodeDiagnoseMetadata.ColumnNames.LastUpdateDateTime, 13, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = EpisodeDiagnoseMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(EpisodeDiagnoseMetadata.ColumnNames.LastUpdateByUserID, 14, typeof(System.String), esSystemType.String);
            c.PropertyName = EpisodeDiagnoseMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 40;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(EpisodeDiagnoseMetadata.ColumnNames.ExternalCauseID, 15, typeof(System.String), esSystemType.String);
            c.PropertyName = EpisodeDiagnoseMetadata.PropertyNames.ExternalCauseID;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(EpisodeDiagnoseMetadata.ColumnNames.CreateByUserID, 16, typeof(System.String), esSystemType.String);
            c.PropertyName = EpisodeDiagnoseMetadata.PropertyNames.CreateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(EpisodeDiagnoseMetadata.ColumnNames.CreateDateTime, 17, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = EpisodeDiagnoseMetadata.PropertyNames.CreateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(EpisodeDiagnoseMetadata.ColumnNames.DiagnoseSynonym, 18, typeof(System.String), esSystemType.String);
            c.PropertyName = EpisodeDiagnoseMetadata.PropertyNames.DiagnoseSynonym;
            c.CharacterMaxLength = 200;
            c.HasDefault = true;
            _columns.Add(c);
        }
        #endregion

        static public EpisodeDiagnoseMetadata Meta()
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
            public const string DiagnoseID = "DiagnoseID";
            public const string SRDiagnoseType = "SRDiagnoseType";
            public const string DiagnosisText = "DiagnosisText";
            public const string MorphologyID = "MorphologyID";
            public const string ParamedicID = "ParamedicID";
            public const string IsAcuteDisease = "IsAcuteDisease";
            public const string IsChronicDisease = "IsChronicDisease";
            public const string IsOldCase = "IsOldCase";
            public const string IsConfirmed = "IsConfirmed";
            public const string IsVoid = "IsVoid";
            public const string Notes = "Notes";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string ExternalCauseID = "ExternalCauseID";
            public const string CreateByUserID = "CreateByUserID";
            public const string CreateDateTime = "CreateDateTime";
            public const string DiagnoseSynonym = "DiagnoseSynonym";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string RegistrationNo = "RegistrationNo";
            public const string SequenceNo = "SequenceNo";
            public const string DiagnoseID = "DiagnoseID";
            public const string SRDiagnoseType = "SRDiagnoseType";
            public const string DiagnosisText = "DiagnosisText";
            public const string MorphologyID = "MorphologyID";
            public const string ParamedicID = "ParamedicID";
            public const string IsAcuteDisease = "IsAcuteDisease";
            public const string IsChronicDisease = "IsChronicDisease";
            public const string IsOldCase = "IsOldCase";
            public const string IsConfirmed = "IsConfirmed";
            public const string IsVoid = "IsVoid";
            public const string Notes = "Notes";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string ExternalCauseID = "ExternalCauseID";
            public const string CreateByUserID = "CreateByUserID";
            public const string CreateDateTime = "CreateDateTime";
            public const string DiagnoseSynonym = "DiagnoseSynonym";
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
            lock (typeof(EpisodeDiagnoseMetadata))
            {
                if (EpisodeDiagnoseMetadata.mapDelegates == null)
                {
                    EpisodeDiagnoseMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (EpisodeDiagnoseMetadata.meta == null)
                {
                    EpisodeDiagnoseMetadata.meta = new EpisodeDiagnoseMetadata();
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
                meta.AddTypeMap("DiagnoseID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRDiagnoseType", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("DiagnosisText", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("MorphologyID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsAcuteDisease", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsChronicDisease", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsOldCase", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsConfirmed", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ExternalCauseID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("DiagnoseSynonym", new esTypeMap("varchar", "System.String"));



                meta.Source = "EpisodeDiagnose";
                meta.Destination = "EpisodeDiagnose";

                meta.spInsert = "proc_EpisodeDiagnoseInsert";
                meta.spUpdate = "proc_EpisodeDiagnoseUpdate";
                meta.spDelete = "proc_EpisodeDiagnoseDelete";
                meta.spLoadAll = "proc_EpisodeDiagnoseLoadAll";
                meta.spLoadByPrimaryKey = "proc_EpisodeDiagnoseLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private EpisodeDiagnoseMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
