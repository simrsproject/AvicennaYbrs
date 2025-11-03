/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 2/4/2015 2:31:47 PM
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
    abstract public class esPatientIncidentInvestigationCollection : esEntityCollectionWAuditLog
    {
        public esPatientIncidentInvestigationCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "PatientIncidentInvestigationCollection";
        }

        #region Query Logic
        protected void InitQuery(esPatientIncidentInvestigationQuery query)
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
            this.InitQuery(query as esPatientIncidentInvestigationQuery);
        }
        #endregion

        virtual public PatientIncidentInvestigation DetachEntity(PatientIncidentInvestigation entity)
        {
            return base.DetachEntity(entity) as PatientIncidentInvestigation;
        }

        virtual public PatientIncidentInvestigation AttachEntity(PatientIncidentInvestigation entity)
        {
            return base.AttachEntity(entity) as PatientIncidentInvestigation;
        }

        virtual public void Combine(PatientIncidentInvestigationCollection collection)
        {
            base.Combine(collection);
        }

        new public PatientIncidentInvestigation this[int index]
        {
            get
            {
                return base[index] as PatientIncidentInvestigation;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(PatientIncidentInvestigation);
        }
    }



    [Serializable]
    abstract public class esPatientIncidentInvestigation : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esPatientIncidentInvestigationQuery GetDynamicQuery()
        {
            return null;
        }

        public esPatientIncidentInvestigation()
        {

        }

        public esPatientIncidentInvestigation(DataRow row)
            : base(row)
        {

        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(System.String patientIncidentNo, System.String serviceUnitID, System.String seqNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(patientIncidentNo, serviceUnitID, seqNo);
            else
                return LoadByPrimaryKeyStoredProcedure(patientIncidentNo, serviceUnitID, seqNo);
        }

        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String patientIncidentNo, System.String serviceUnitID, System.String seqNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(patientIncidentNo, serviceUnitID, seqNo);
            else
                return LoadByPrimaryKeyStoredProcedure(patientIncidentNo, serviceUnitID, seqNo);
        }

        private bool LoadByPrimaryKeyDynamic(System.String patientIncidentNo, System.String serviceUnitID, System.String seqNo)
        {
            esPatientIncidentInvestigationQuery query = this.GetDynamicQuery();
            query.Where(query.PatientIncidentNo == patientIncidentNo, query.ServiceUnitID == serviceUnitID, query.SeqNo == seqNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(System.String patientIncidentNo, System.String serviceUnitID, System.String seqNo)
        {
            esParameters parms = new esParameters();
            parms.Add("PatientIncidentNo", patientIncidentNo); parms.Add("ServiceUnitID", serviceUnitID); parms.Add("SeqNo", seqNo);
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
                        case "PatientIncidentNo": this.str.PatientIncidentNo = (string)value; break;
                        case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
                        case "SeqNo": this.str.SeqNo = (string)value; break;
                        case "Recomendation": this.str.Recomendation = (string)value; break;
                        case "RecomendationDateTime": this.str.RecomendationDateTime = (string)value; break;
                        case "PersonInCharge": this.str.PersonInCharge = (string)value; break;
                        case "Implementation": this.str.Implementation = (string)value; break;
                        case "ImplementationDateTime": this.str.ImplementationDateTime = (string)value; break;
                        case "FollowUp": this.str.FollowUp = (string)value; break;
                        case "FollowUpDateTime": this.str.FollowUpDateTime = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "RecomendationDateTime":

                            if (value == null || value is System.DateTime)
                                this.RecomendationDateTime = (System.DateTime?)value;
                            break;

                        case "ImplementationDateTime":

                            if (value == null || value is System.DateTime)
                                this.ImplementationDateTime = (System.DateTime?)value;
                            break;

                        case "FollowUpDateTime":

                            if (value == null || value is System.DateTime)
                                this.FollowUpDateTime = (System.DateTime?)value;
                            break;

                        case "LastUpdateDateTime":

                            if (value == null || value is System.DateTime)
                                this.LastUpdateDateTime = (System.DateTime?)value;
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
        /// Maps to PatientIncidentInvestigation.PatientIncidentNo
        /// </summary>
        virtual public System.String PatientIncidentNo
        {
            get
            {
                return base.GetSystemString(PatientIncidentInvestigationMetadata.ColumnNames.PatientIncidentNo);
            }

            set
            {
                base.SetSystemString(PatientIncidentInvestigationMetadata.ColumnNames.PatientIncidentNo, value);
            }
        }

        /// <summary>
        /// Maps to PatientIncidentInvestigation.ServiceUnitID
        /// </summary>
        virtual public System.String ServiceUnitID
        {
            get
            {
                return base.GetSystemString(PatientIncidentInvestigationMetadata.ColumnNames.ServiceUnitID);
            }

            set
            {
                base.SetSystemString(PatientIncidentInvestigationMetadata.ColumnNames.ServiceUnitID, value);
            }
        }

        /// <summary>
        /// Maps to PatientIncidentInvestigation.SeqNo
        /// </summary>
        virtual public System.String SeqNo
        {
            get
            {
                return base.GetSystemString(PatientIncidentInvestigationMetadata.ColumnNames.SeqNo);
            }

            set
            {
                base.SetSystemString(PatientIncidentInvestigationMetadata.ColumnNames.SeqNo, value);
            }
        }

        /// <summary>
        /// Maps to PatientIncidentInvestigation.Recomendation
        /// </summary>
        virtual public System.String Recomendation
        {
            get
            {
                return base.GetSystemString(PatientIncidentInvestigationMetadata.ColumnNames.Recomendation);
            }

            set
            {
                base.SetSystemString(PatientIncidentInvestigationMetadata.ColumnNames.Recomendation, value);
            }
        }

        /// <summary>
        /// Maps to PatientIncidentInvestigation.RecomendationDateTime
        /// </summary>
        virtual public System.DateTime? RecomendationDateTime
        {
            get
            {
                return base.GetSystemDateTime(PatientIncidentInvestigationMetadata.ColumnNames.RecomendationDateTime);
            }

            set
            {
                base.SetSystemDateTime(PatientIncidentInvestigationMetadata.ColumnNames.RecomendationDateTime, value);
            }
        }

        /// <summary>
        /// Maps to PatientIncidentInvestigation.PersonInCharge
        /// </summary>
        virtual public System.String PersonInCharge
        {
            get
            {
                return base.GetSystemString(PatientIncidentInvestigationMetadata.ColumnNames.PersonInCharge);
            }

            set
            {
                base.SetSystemString(PatientIncidentInvestigationMetadata.ColumnNames.PersonInCharge, value);
            }
        }

        /// <summary>
        /// Maps to PatientIncidentInvestigation.Implementation
        /// </summary>
        virtual public System.String Implementation
        {
            get
            {
                return base.GetSystemString(PatientIncidentInvestigationMetadata.ColumnNames.Implementation);
            }

            set
            {
                base.SetSystemString(PatientIncidentInvestigationMetadata.ColumnNames.Implementation, value);
            }
        }

        /// <summary>
        /// Maps to PatientIncidentInvestigation.ImplementationDateTime
        /// </summary>
        virtual public System.DateTime? ImplementationDateTime
        {
            get
            {
                return base.GetSystemDateTime(PatientIncidentInvestigationMetadata.ColumnNames.ImplementationDateTime);
            }

            set
            {
                base.SetSystemDateTime(PatientIncidentInvestigationMetadata.ColumnNames.ImplementationDateTime, value);
            }
        }

        /// <summary>
        /// Maps to PatientIncidentInvestigation.FollowUp
        /// </summary>
        virtual public System.String FollowUp
        {
            get
            {
                return base.GetSystemString(PatientIncidentInvestigationMetadata.ColumnNames.FollowUp);
            }

            set
            {
                base.SetSystemString(PatientIncidentInvestigationMetadata.ColumnNames.FollowUp, value);
            }
        }

        /// <summary>
        /// Maps to PatientIncidentInvestigation.FollowUpDateTime
        /// </summary>
        virtual public System.DateTime? FollowUpDateTime
        {
            get
            {
                return base.GetSystemDateTime(PatientIncidentInvestigationMetadata.ColumnNames.FollowUpDateTime);
            }

            set
            {
                base.SetSystemDateTime(PatientIncidentInvestigationMetadata.ColumnNames.FollowUpDateTime, value);
            }
        }

        /// <summary>
        /// Maps to PatientIncidentInvestigation.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(PatientIncidentInvestigationMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(PatientIncidentInvestigationMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to PatientIncidentInvestigation.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(PatientIncidentInvestigationMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(PatientIncidentInvestigationMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esPatientIncidentInvestigation entity)
            {
                this.entity = entity;
            }


            public System.String PatientIncidentNo
            {
                get
                {
                    System.String data = entity.PatientIncidentNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PatientIncidentNo = null;
                    else entity.PatientIncidentNo = Convert.ToString(value);
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

            public System.String SeqNo
            {
                get
                {
                    System.String data = entity.SeqNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SeqNo = null;
                    else entity.SeqNo = Convert.ToString(value);
                }
            }

            public System.String Recomendation
            {
                get
                {
                    System.String data = entity.Recomendation;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Recomendation = null;
                    else entity.Recomendation = Convert.ToString(value);
                }
            }

            public System.String RecomendationDateTime
            {
                get
                {
                    System.DateTime? data = entity.RecomendationDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RecomendationDateTime = null;
                    else entity.RecomendationDateTime = Convert.ToDateTime(value);
                }
            }

            public System.String PersonInCharge
            {
                get
                {
                    System.String data = entity.PersonInCharge;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PersonInCharge = null;
                    else entity.PersonInCharge = Convert.ToString(value);
                }
            }

            public System.String Implementation
            {
                get
                {
                    System.String data = entity.Implementation;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Implementation = null;
                    else entity.Implementation = Convert.ToString(value);
                }
            }

            public System.String ImplementationDateTime
            {
                get
                {
                    System.DateTime? data = entity.ImplementationDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ImplementationDateTime = null;
                    else entity.ImplementationDateTime = Convert.ToDateTime(value);
                }
            }

            public System.String FollowUp
            {
                get
                {
                    System.String data = entity.FollowUp;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FollowUp = null;
                    else entity.FollowUp = Convert.ToString(value);
                }
            }

            public System.String FollowUpDateTime
            {
                get
                {
                    System.DateTime? data = entity.FollowUpDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FollowUpDateTime = null;
                    else entity.FollowUpDateTime = Convert.ToDateTime(value);
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


            private esPatientIncidentInvestigation entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esPatientIncidentInvestigationQuery query)
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
                throw new Exception("esPatientIncidentInvestigation can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    public partial class PatientIncidentInvestigation : esPatientIncidentInvestigation
    {


        /// <summary>
        /// Used internally by the entity's hierarchical properties.
        /// </summary>
        protected override List<esPropertyDescriptor> GetHierarchicalProperties()
        {
            List<esPropertyDescriptor> props = new List<esPropertyDescriptor>();


            return props;
        }

        /// <summary>
        /// Used internally for retrieving AutoIncrementing keys
        /// during hierarchical PreSave.
        /// </summary>
        protected override void ApplyPreSaveKeys()
        {
        }

        /// <summary>
        /// Used internally for retrieving AutoIncrementing keys
        /// during hierarchical PostSave.
        /// </summary>
        protected override void ApplyPostSaveKeys()
        {
        }

        /// <summary>
        /// Used internally for retrieving AutoIncrementing keys
        /// during hierarchical PostOneToOneSave.
        /// </summary>
        protected override void ApplyPostOneSaveKeys()
        {
        }

    }



    [Serializable]
    abstract public class esPatientIncidentInvestigationQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return PatientIncidentInvestigationMetadata.Meta();
            }
        }


        public esQueryItem PatientIncidentNo
        {
            get
            {
                return new esQueryItem(this, PatientIncidentInvestigationMetadata.ColumnNames.PatientIncidentNo, esSystemType.String);
            }
        }

        public esQueryItem ServiceUnitID
        {
            get
            {
                return new esQueryItem(this, PatientIncidentInvestigationMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
            }
        }

        public esQueryItem SeqNo
        {
            get
            {
                return new esQueryItem(this, PatientIncidentInvestigationMetadata.ColumnNames.SeqNo, esSystemType.String);
            }
        }

        public esQueryItem Recomendation
        {
            get
            {
                return new esQueryItem(this, PatientIncidentInvestigationMetadata.ColumnNames.Recomendation, esSystemType.String);
            }
        }

        public esQueryItem RecomendationDateTime
        {
            get
            {
                return new esQueryItem(this, PatientIncidentInvestigationMetadata.ColumnNames.RecomendationDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem PersonInCharge
        {
            get
            {
                return new esQueryItem(this, PatientIncidentInvestigationMetadata.ColumnNames.PersonInCharge, esSystemType.String);
            }
        }

        public esQueryItem Implementation
        {
            get
            {
                return new esQueryItem(this, PatientIncidentInvestigationMetadata.ColumnNames.Implementation, esSystemType.String);
            }
        }

        public esQueryItem ImplementationDateTime
        {
            get
            {
                return new esQueryItem(this, PatientIncidentInvestigationMetadata.ColumnNames.ImplementationDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem FollowUp
        {
            get
            {
                return new esQueryItem(this, PatientIncidentInvestigationMetadata.ColumnNames.FollowUp, esSystemType.String);
            }
        }

        public esQueryItem FollowUpDateTime
        {
            get
            {
                return new esQueryItem(this, PatientIncidentInvestigationMetadata.ColumnNames.FollowUpDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, PatientIncidentInvestigationMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, PatientIncidentInvestigationMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("PatientIncidentInvestigationCollection")]
    public partial class PatientIncidentInvestigationCollection : esPatientIncidentInvestigationCollection, IEnumerable<PatientIncidentInvestigation>
    {
        public PatientIncidentInvestigationCollection()
        {

        }

        public static implicit operator List<PatientIncidentInvestigation>(PatientIncidentInvestigationCollection coll)
        {
            List<PatientIncidentInvestigation> list = new List<PatientIncidentInvestigation>();

            foreach (PatientIncidentInvestigation emp in coll)
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
                return PatientIncidentInvestigationMetadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PatientIncidentInvestigationQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new PatientIncidentInvestigation(row);
        }

        override protected esEntity CreateEntity()
        {
            return new PatientIncidentInvestigation();
        }


        #endregion


        [BrowsableAttribute(false)]
        public PatientIncidentInvestigationQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PatientIncidentInvestigationQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(PatientIncidentInvestigationQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public PatientIncidentInvestigation AddNew()
        {
            PatientIncidentInvestigation entity = base.AddNewEntity() as PatientIncidentInvestigation;

            return entity;
        }

        public PatientIncidentInvestigation FindByPrimaryKey(System.String patientIncidentNo, System.String serviceUnitID, System.String seqNo)
        {
            return base.FindByPrimaryKey(patientIncidentNo, serviceUnitID, seqNo) as PatientIncidentInvestigation;
        }


        #region IEnumerable<PatientIncidentInvestigation> Members

        IEnumerator<PatientIncidentInvestigation> IEnumerable<PatientIncidentInvestigation>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as PatientIncidentInvestigation;
            }
        }

        #endregion

        private PatientIncidentInvestigationQuery query;
    }


    /// <summary>
    /// Encapsulates the 'PatientIncidentInvestigation' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("PatientIncidentInvestigation ({PatientIncidentNo},{ServiceUnitID},{SeqNo})")]
    [Serializable]
    public partial class PatientIncidentInvestigation : esPatientIncidentInvestigation
    {
        public PatientIncidentInvestigation()
        {

        }

        public PatientIncidentInvestigation(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return PatientIncidentInvestigationMetadata.Meta();
            }
        }



        override protected esPatientIncidentInvestigationQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PatientIncidentInvestigationQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public PatientIncidentInvestigationQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PatientIncidentInvestigationQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(PatientIncidentInvestigationQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private PatientIncidentInvestigationQuery query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class PatientIncidentInvestigationQuery : esPatientIncidentInvestigationQuery
    {
        public PatientIncidentInvestigationQuery()
        {

        }

        public PatientIncidentInvestigationQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "PatientIncidentInvestigationQuery";
        }


    }


    [Serializable]
    public partial class PatientIncidentInvestigationMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected PatientIncidentInvestigationMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(PatientIncidentInvestigationMetadata.ColumnNames.PatientIncidentNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientIncidentInvestigationMetadata.PropertyNames.PatientIncidentNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(PatientIncidentInvestigationMetadata.ColumnNames.ServiceUnitID, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientIncidentInvestigationMetadata.PropertyNames.ServiceUnitID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(PatientIncidentInvestigationMetadata.ColumnNames.SeqNo, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientIncidentInvestigationMetadata.PropertyNames.SeqNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 5;
            _columns.Add(c);

            c = new esColumnMetadata(PatientIncidentInvestigationMetadata.ColumnNames.Recomendation, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientIncidentInvestigationMetadata.PropertyNames.Recomendation;
            c.CharacterMaxLength = 500;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientIncidentInvestigationMetadata.ColumnNames.RecomendationDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PatientIncidentInvestigationMetadata.PropertyNames.RecomendationDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientIncidentInvestigationMetadata.ColumnNames.PersonInCharge, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientIncidentInvestigationMetadata.PropertyNames.PersonInCharge;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientIncidentInvestigationMetadata.ColumnNames.Implementation, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientIncidentInvestigationMetadata.PropertyNames.Implementation;
            c.CharacterMaxLength = 500;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientIncidentInvestigationMetadata.ColumnNames.ImplementationDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PatientIncidentInvestigationMetadata.PropertyNames.ImplementationDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientIncidentInvestigationMetadata.ColumnNames.FollowUp, 8, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientIncidentInvestigationMetadata.PropertyNames.FollowUp;
            c.CharacterMaxLength = 500;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientIncidentInvestigationMetadata.ColumnNames.FollowUpDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PatientIncidentInvestigationMetadata.PropertyNames.FollowUpDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientIncidentInvestigationMetadata.ColumnNames.LastUpdateDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PatientIncidentInvestigationMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientIncidentInvestigationMetadata.ColumnNames.LastUpdateByUserID, 11, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientIncidentInvestigationMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public PatientIncidentInvestigationMetadata Meta()
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
            public const string PatientIncidentNo = "PatientIncidentNo";
            public const string ServiceUnitID = "ServiceUnitID";
            public const string SeqNo = "SeqNo";
            public const string Recomendation = "Recomendation";
            public const string RecomendationDateTime = "RecomendationDateTime";
            public const string PersonInCharge = "PersonInCharge";
            public const string Implementation = "Implementation";
            public const string ImplementationDateTime = "ImplementationDateTime";
            public const string FollowUp = "FollowUp";
            public const string FollowUpDateTime = "FollowUpDateTime";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string PatientIncidentNo = "PatientIncidentNo";
            public const string ServiceUnitID = "ServiceUnitID";
            public const string SeqNo = "SeqNo";
            public const string Recomendation = "Recomendation";
            public const string RecomendationDateTime = "RecomendationDateTime";
            public const string PersonInCharge = "PersonInCharge";
            public const string Implementation = "Implementation";
            public const string ImplementationDateTime = "ImplementationDateTime";
            public const string FollowUp = "FollowUp";
            public const string FollowUpDateTime = "FollowUpDateTime";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
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
            lock (typeof(PatientIncidentInvestigationMetadata))
            {
                if (PatientIncidentInvestigationMetadata.mapDelegates == null)
                {
                    PatientIncidentInvestigationMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (PatientIncidentInvestigationMetadata.meta == null)
                {
                    PatientIncidentInvestigationMetadata.meta = new PatientIncidentInvestigationMetadata();
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


                meta.AddTypeMap("PatientIncidentNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SeqNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Recomendation", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("RecomendationDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("PersonInCharge", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Implementation", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ImplementationDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("FollowUp", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("FollowUpDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));



                meta.Source = "PatientIncidentInvestigation";
                meta.Destination = "PatientIncidentInvestigation";

                meta.spInsert = "proc_PatientIncidentInvestigationInsert";
                meta.spUpdate = "proc_PatientIncidentInvestigationUpdate";
                meta.spDelete = "proc_PatientIncidentInvestigationDelete";
                meta.spLoadAll = "proc_PatientIncidentInvestigationLoadAll";
                meta.spLoadByPrimaryKey = "proc_PatientIncidentInvestigationLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private PatientIncidentInvestigationMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
