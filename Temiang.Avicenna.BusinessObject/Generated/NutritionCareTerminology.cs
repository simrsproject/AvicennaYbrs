/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 7/10/2019 11:33:05 AM
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
    abstract public class esNutritionCareTerminologyCollection : esEntityCollectionWAuditLog
    {
        public esNutritionCareTerminologyCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "NutritionCareTerminologyCollection";
        }

        #region Query Logic
        protected void InitQuery(esNutritionCareTerminologyQuery query)
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
            this.InitQuery(query as esNutritionCareTerminologyQuery);
        }
        #endregion

        virtual public NutritionCareTerminology DetachEntity(NutritionCareTerminology entity)
        {
            return base.DetachEntity(entity) as NutritionCareTerminology;
        }

        virtual public NutritionCareTerminology AttachEntity(NutritionCareTerminology entity)
        {
            return base.AttachEntity(entity) as NutritionCareTerminology;
        }

        virtual public void Combine(NutritionCareTerminologyCollection collection)
        {
            base.Combine(collection);
        }

        new public NutritionCareTerminology this[int index]
        {
            get
            {
                return base[index] as NutritionCareTerminology;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(NutritionCareTerminology);
        }
    }

    [Serializable]
    abstract public class esNutritionCareTerminology : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esNutritionCareTerminologyQuery GetDynamicQuery()
        {
            return null;
        }

        public esNutritionCareTerminology()
        {
        }

        public esNutritionCareTerminology(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String terminologyID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(terminologyID);
            else
                return LoadByPrimaryKeyStoredProcedure(terminologyID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String terminologyID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(terminologyID);
            else
                return LoadByPrimaryKeyStoredProcedure(terminologyID);
        }

        private bool LoadByPrimaryKeyDynamic(String terminologyID)
        {
            esNutritionCareTerminologyQuery query = this.GetDynamicQuery();
            query.Where(query.TerminologyID == terminologyID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String terminologyID)
        {
            esParameters parms = new esParameters();
            parms.Add("TerminologyID", terminologyID);
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
                        case "TerminologyID": this.str.TerminologyID = (string)value; break;
                        case "TerminologyName": this.str.TerminologyName = (string)value; break;
                        case "TerminologyDesc": this.str.TerminologyDesc = (string)value; break;
                        case "SRNutritionCareTerminologyLevel": this.str.SRNutritionCareTerminologyLevel = (string)value; break;
                        case "SequenceNo": this.str.SequenceNo = (string)value; break;
                        case "TerminologyParentID": this.str.TerminologyParentID = (string)value; break;
                        case "TerminologyLevel": this.str.TerminologyLevel = (string)value; break;
                        case "DomainID": this.str.DomainID = (string)value; break;
                        case "IsDetail": this.str.IsDetail = (string)value; break;
                        case "RespondTemplate": this.str.RespondTemplate = (string)value; break;
                        case "IsActive": this.str.IsActive = (string)value; break;
                        case "CreateByUserID": this.str.CreateByUserID = (string)value; break;
                        case "CreateDateTime": this.str.CreateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "TerminologyLevel":

                            if (value == null || value is System.Int32)
                                this.TerminologyLevel = (System.Int32?)value;
                            break;
                        case "IsDetail":

                            if (value == null || value is System.Boolean)
                                this.IsDetail = (System.Boolean?)value;
                            break;
                        case "IsActive":

                            if (value == null || value is System.Boolean)
                                this.IsActive = (System.Boolean?)value;
                            break;
                        case "CreateDateTime":

                            if (value == null || value is System.DateTime)
                                this.CreateDateTime = (System.DateTime?)value;
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
        /// Maps to NutritionCareTerminology.TerminologyID
        /// </summary>
        virtual public System.String TerminologyID
        {
            get
            {
                return base.GetSystemString(NutritionCareTerminologyMetadata.ColumnNames.TerminologyID);
            }

            set
            {
                base.SetSystemString(NutritionCareTerminologyMetadata.ColumnNames.TerminologyID, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareTerminology.TerminologyName
        /// </summary>
        virtual public System.String TerminologyName
        {
            get
            {
                return base.GetSystemString(NutritionCareTerminologyMetadata.ColumnNames.TerminologyName);
            }

            set
            {
                base.SetSystemString(NutritionCareTerminologyMetadata.ColumnNames.TerminologyName, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareTerminology.TerminologyDesc
        /// </summary>
        virtual public System.String TerminologyDesc
        {
            get
            {
                return base.GetSystemString(NutritionCareTerminologyMetadata.ColumnNames.TerminologyDesc);
            }

            set
            {
                base.SetSystemString(NutritionCareTerminologyMetadata.ColumnNames.TerminologyDesc, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareTerminology.SRNutritionCareTerminologyLevel
        /// </summary>
        virtual public System.String SRNutritionCareTerminologyLevel
        {
            get
            {
                return base.GetSystemString(NutritionCareTerminologyMetadata.ColumnNames.SRNutritionCareTerminologyLevel);
            }

            set
            {
                base.SetSystemString(NutritionCareTerminologyMetadata.ColumnNames.SRNutritionCareTerminologyLevel, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareTerminology.SequenceNo
        /// </summary>
        virtual public System.String SequenceNo
        {
            get
            {
                return base.GetSystemString(NutritionCareTerminologyMetadata.ColumnNames.SequenceNo);
            }

            set
            {
                base.SetSystemString(NutritionCareTerminologyMetadata.ColumnNames.SequenceNo, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareTerminology.TerminologyParentID
        /// </summary>
        virtual public System.String TerminologyParentID
        {
            get
            {
                return base.GetSystemString(NutritionCareTerminologyMetadata.ColumnNames.TerminologyParentID);
            }

            set
            {
                base.SetSystemString(NutritionCareTerminologyMetadata.ColumnNames.TerminologyParentID, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareTerminology.TerminologyLevel
        /// </summary>
        virtual public System.Int32? TerminologyLevel
        {
            get
            {
                return base.GetSystemInt32(NutritionCareTerminologyMetadata.ColumnNames.TerminologyLevel);
            }

            set
            {
                base.SetSystemInt32(NutritionCareTerminologyMetadata.ColumnNames.TerminologyLevel, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareTerminology.DomainID
        /// </summary>
        virtual public System.String DomainID
        {
            get
            {
                return base.GetSystemString(NutritionCareTerminologyMetadata.ColumnNames.DomainID);
            }

            set
            {
                base.SetSystemString(NutritionCareTerminologyMetadata.ColumnNames.DomainID, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareTerminology.IsDetail
        /// </summary>
        virtual public System.Boolean? IsDetail
        {
            get
            {
                return base.GetSystemBoolean(NutritionCareTerminologyMetadata.ColumnNames.IsDetail);
            }

            set
            {
                base.SetSystemBoolean(NutritionCareTerminologyMetadata.ColumnNames.IsDetail, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareTerminology.RespondTemplate
        /// </summary>
        virtual public System.String RespondTemplate
        {
            get
            {
                return base.GetSystemString(NutritionCareTerminologyMetadata.ColumnNames.RespondTemplate);
            }

            set
            {
                base.SetSystemString(NutritionCareTerminologyMetadata.ColumnNames.RespondTemplate, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareTerminology.IsActive
        /// </summary>
        virtual public System.Boolean? IsActive
        {
            get
            {
                return base.GetSystemBoolean(NutritionCareTerminologyMetadata.ColumnNames.IsActive);
            }

            set
            {
                base.SetSystemBoolean(NutritionCareTerminologyMetadata.ColumnNames.IsActive, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareTerminology.CreateByUserID
        /// </summary>
        virtual public System.String CreateByUserID
        {
            get
            {
                return base.GetSystemString(NutritionCareTerminologyMetadata.ColumnNames.CreateByUserID);
            }

            set
            {
                base.SetSystemString(NutritionCareTerminologyMetadata.ColumnNames.CreateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareTerminology.CreateDateTime
        /// </summary>
        virtual public System.DateTime? CreateDateTime
        {
            get
            {
                return base.GetSystemDateTime(NutritionCareTerminologyMetadata.ColumnNames.CreateDateTime);
            }

            set
            {
                base.SetSystemDateTime(NutritionCareTerminologyMetadata.ColumnNames.CreateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareTerminology.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(NutritionCareTerminologyMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(NutritionCareTerminologyMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareTerminology.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(NutritionCareTerminologyMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(NutritionCareTerminologyMetadata.ColumnNames.LastUpdateDateTime, value);
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
            public esStrings(esNutritionCareTerminology entity)
            {
                this.entity = entity;
            }
            public System.String TerminologyID
            {
                get
                {
                    System.String data = entity.TerminologyID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TerminologyID = null;
                    else entity.TerminologyID = Convert.ToString(value);
                }
            }
            public System.String TerminologyName
            {
                get
                {
                    System.String data = entity.TerminologyName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TerminologyName = null;
                    else entity.TerminologyName = Convert.ToString(value);
                }
            }
            public System.String TerminologyDesc
            {
                get
                {
                    System.String data = entity.TerminologyDesc;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TerminologyDesc = null;
                    else entity.TerminologyDesc = Convert.ToString(value);
                }
            }
            public System.String SRNutritionCareTerminologyLevel
            {
                get
                {
                    System.String data = entity.SRNutritionCareTerminologyLevel;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRNutritionCareTerminologyLevel = null;
                    else entity.SRNutritionCareTerminologyLevel = Convert.ToString(value);
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
            public System.String TerminologyParentID
            {
                get
                {
                    System.String data = entity.TerminologyParentID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TerminologyParentID = null;
                    else entity.TerminologyParentID = Convert.ToString(value);
                }
            }
            public System.String TerminologyLevel
            {
                get
                {
                    System.Int32? data = entity.TerminologyLevel;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TerminologyLevel = null;
                    else entity.TerminologyLevel = Convert.ToInt32(value);
                }
            }
            public System.String DomainID
            {
                get
                {
                    System.String data = entity.DomainID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DomainID = null;
                    else entity.DomainID = Convert.ToString(value);
                }
            }
            public System.String IsDetail
            {
                get
                {
                    System.Boolean? data = entity.IsDetail;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsDetail = null;
                    else entity.IsDetail = Convert.ToBoolean(value);
                }
            }
            public System.String RespondTemplate
            {
                get
                {
                    System.String data = entity.RespondTemplate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RespondTemplate = null;
                    else entity.RespondTemplate = Convert.ToString(value);
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
            private esNutritionCareTerminology entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esNutritionCareTerminologyQuery query)
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
                throw new Exception("esNutritionCareTerminology can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class NutritionCareTerminology : esNutritionCareTerminology
    {
    }

    [Serializable]
    abstract public class esNutritionCareTerminologyQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return NutritionCareTerminologyMetadata.Meta();
            }
        }

        public esQueryItem TerminologyID
        {
            get
            {
                return new esQueryItem(this, NutritionCareTerminologyMetadata.ColumnNames.TerminologyID, esSystemType.String);
            }
        }

        public esQueryItem TerminologyName
        {
            get
            {
                return new esQueryItem(this, NutritionCareTerminologyMetadata.ColumnNames.TerminologyName, esSystemType.String);
            }
        }

        public esQueryItem TerminologyDesc
        {
            get
            {
                return new esQueryItem(this, NutritionCareTerminologyMetadata.ColumnNames.TerminologyDesc, esSystemType.String);
            }
        }

        public esQueryItem SRNutritionCareTerminologyLevel
        {
            get
            {
                return new esQueryItem(this, NutritionCareTerminologyMetadata.ColumnNames.SRNutritionCareTerminologyLevel, esSystemType.String);
            }
        }

        public esQueryItem SequenceNo
        {
            get
            {
                return new esQueryItem(this, NutritionCareTerminologyMetadata.ColumnNames.SequenceNo, esSystemType.String);
            }
        }

        public esQueryItem TerminologyParentID
        {
            get
            {
                return new esQueryItem(this, NutritionCareTerminologyMetadata.ColumnNames.TerminologyParentID, esSystemType.String);
            }
        }

        public esQueryItem TerminologyLevel
        {
            get
            {
                return new esQueryItem(this, NutritionCareTerminologyMetadata.ColumnNames.TerminologyLevel, esSystemType.Int32);
            }
        }

        public esQueryItem DomainID
        {
            get
            {
                return new esQueryItem(this, NutritionCareTerminologyMetadata.ColumnNames.DomainID, esSystemType.String);
            }
        }

        public esQueryItem IsDetail
        {
            get
            {
                return new esQueryItem(this, NutritionCareTerminologyMetadata.ColumnNames.IsDetail, esSystemType.Boolean);
            }
        }

        public esQueryItem RespondTemplate
        {
            get
            {
                return new esQueryItem(this, NutritionCareTerminologyMetadata.ColumnNames.RespondTemplate, esSystemType.String);
            }
        }

        public esQueryItem IsActive
        {
            get
            {
                return new esQueryItem(this, NutritionCareTerminologyMetadata.ColumnNames.IsActive, esSystemType.Boolean);
            }
        }

        public esQueryItem CreateByUserID
        {
            get
            {
                return new esQueryItem(this, NutritionCareTerminologyMetadata.ColumnNames.CreateByUserID, esSystemType.String);
            }
        }

        public esQueryItem CreateDateTime
        {
            get
            {
                return new esQueryItem(this, NutritionCareTerminologyMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, NutritionCareTerminologyMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, NutritionCareTerminologyMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("NutritionCareTerminologyCollection")]
    public partial class NutritionCareTerminologyCollection : esNutritionCareTerminologyCollection, IEnumerable<NutritionCareTerminology>
    {
        public NutritionCareTerminologyCollection()
        {

        }

        public static implicit operator List<NutritionCareTerminology>(NutritionCareTerminologyCollection coll)
        {
            List<NutritionCareTerminology> list = new List<NutritionCareTerminology>();

            foreach (NutritionCareTerminology emp in coll)
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
                return NutritionCareTerminologyMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new NutritionCareTerminologyQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new NutritionCareTerminology(row);
        }

        override protected esEntity CreateEntity()
        {
            return new NutritionCareTerminology();
        }

        #endregion

        [BrowsableAttribute(false)]
        public NutritionCareTerminologyQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new NutritionCareTerminologyQuery();
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
        public bool Load(NutritionCareTerminologyQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public NutritionCareTerminology AddNew()
        {
            NutritionCareTerminology entity = base.AddNewEntity() as NutritionCareTerminology;

            return entity;
        }
        public NutritionCareTerminology FindByPrimaryKey(String terminologyID)
        {
            return base.FindByPrimaryKey(terminologyID) as NutritionCareTerminology;
        }

        #region IEnumerable< NutritionCareTerminology> Members

        IEnumerator<NutritionCareTerminology> IEnumerable<NutritionCareTerminology>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as NutritionCareTerminology;
            }
        }

        #endregion

        private NutritionCareTerminologyQuery query;
    }


    /// <summary>
    /// Encapsulates the 'NutritionCareTerminology' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("NutritionCareTerminology ({TerminologyID})")]
    [Serializable]
    public partial class NutritionCareTerminology : esNutritionCareTerminology
    {
        public NutritionCareTerminology()
        {
        }

        public NutritionCareTerminology(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return NutritionCareTerminologyMetadata.Meta();
            }
        }

        override protected esNutritionCareTerminologyQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new NutritionCareTerminologyQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public NutritionCareTerminologyQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new NutritionCareTerminologyQuery();
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
        public bool Load(NutritionCareTerminologyQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private NutritionCareTerminologyQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class NutritionCareTerminologyQuery : esNutritionCareTerminologyQuery
    {
        public NutritionCareTerminologyQuery()
        {

        }

        public NutritionCareTerminologyQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "NutritionCareTerminologyQuery";
        }
    }

    [Serializable]
    public partial class NutritionCareTerminologyMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected NutritionCareTerminologyMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(NutritionCareTerminologyMetadata.ColumnNames.TerminologyID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = NutritionCareTerminologyMetadata.PropertyNames.TerminologyID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareTerminologyMetadata.ColumnNames.TerminologyName, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = NutritionCareTerminologyMetadata.PropertyNames.TerminologyName;
            c.CharacterMaxLength = 450;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareTerminologyMetadata.ColumnNames.TerminologyDesc, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = NutritionCareTerminologyMetadata.PropertyNames.TerminologyDesc;
            c.CharacterMaxLength = 500;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareTerminologyMetadata.ColumnNames.SRNutritionCareTerminologyLevel, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = NutritionCareTerminologyMetadata.PropertyNames.SRNutritionCareTerminologyLevel;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareTerminologyMetadata.ColumnNames.SequenceNo, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = NutritionCareTerminologyMetadata.PropertyNames.SequenceNo;
            c.CharacterMaxLength = 5;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareTerminologyMetadata.ColumnNames.TerminologyParentID, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = NutritionCareTerminologyMetadata.PropertyNames.TerminologyParentID;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareTerminologyMetadata.ColumnNames.TerminologyLevel, 6, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = NutritionCareTerminologyMetadata.PropertyNames.TerminologyLevel;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareTerminologyMetadata.ColumnNames.DomainID, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = NutritionCareTerminologyMetadata.PropertyNames.DomainID;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareTerminologyMetadata.ColumnNames.IsDetail, 8, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = NutritionCareTerminologyMetadata.PropertyNames.IsDetail;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareTerminologyMetadata.ColumnNames.RespondTemplate, 9, typeof(System.String), esSystemType.String);
            c.PropertyName = NutritionCareTerminologyMetadata.PropertyNames.RespondTemplate;
            c.CharacterMaxLength = 255;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareTerminologyMetadata.ColumnNames.IsActive, 10, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = NutritionCareTerminologyMetadata.PropertyNames.IsActive;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareTerminologyMetadata.ColumnNames.CreateByUserID, 11, typeof(System.String), esSystemType.String);
            c.PropertyName = NutritionCareTerminologyMetadata.PropertyNames.CreateByUserID;
            c.CharacterMaxLength = 15;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareTerminologyMetadata.ColumnNames.CreateDateTime, 12, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = NutritionCareTerminologyMetadata.PropertyNames.CreateDateTime;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareTerminologyMetadata.ColumnNames.LastUpdateByUserID, 13, typeof(System.String), esSystemType.String);
            c.PropertyName = NutritionCareTerminologyMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareTerminologyMetadata.ColumnNames.LastUpdateDateTime, 14, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = NutritionCareTerminologyMetadata.PropertyNames.LastUpdateDateTime;
            _columns.Add(c);


        }
        #endregion

        static public NutritionCareTerminologyMetadata Meta()
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
            public const string TerminologyID = "TerminologyID";
            public const string TerminologyName = "TerminologyName";
            public const string TerminologyDesc = "TerminologyDesc";
            public const string SRNutritionCareTerminologyLevel = "SRNutritionCareTerminologyLevel";
            public const string SequenceNo = "SequenceNo";
            public const string TerminologyParentID = "TerminologyParentID";
            public const string TerminologyLevel = "TerminologyLevel";
            public const string DomainID = "DomainID";
            public const string IsDetail = "IsDetail";
            public const string RespondTemplate = "RespondTemplate";
            public const string IsActive = "IsActive";
            public const string CreateByUserID = "CreateByUserID";
            public const string CreateDateTime = "CreateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string TerminologyID = "TerminologyID";
            public const string TerminologyName = "TerminologyName";
            public const string TerminologyDesc = "TerminologyDesc";
            public const string SRNutritionCareTerminologyLevel = "SRNutritionCareTerminologyLevel";
            public const string SequenceNo = "SequenceNo";
            public const string TerminologyParentID = "TerminologyParentID";
            public const string TerminologyLevel = "TerminologyLevel";
            public const string DomainID = "DomainID";
            public const string IsDetail = "IsDetail";
            public const string RespondTemplate = "RespondTemplate";
            public const string IsActive = "IsActive";
            public const string CreateByUserID = "CreateByUserID";
            public const string CreateDateTime = "CreateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
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
            lock (typeof(NutritionCareTerminologyMetadata))
            {
                if (NutritionCareTerminologyMetadata.mapDelegates == null)
                {
                    NutritionCareTerminologyMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (NutritionCareTerminologyMetadata.meta == null)
                {
                    NutritionCareTerminologyMetadata.meta = new NutritionCareTerminologyMetadata();
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

                meta.AddTypeMap("TerminologyID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("TerminologyName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("TerminologyDesc", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRNutritionCareTerminologyLevel", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SequenceNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("TerminologyParentID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("TerminologyLevel", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("DomainID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsDetail", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("RespondTemplate", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));


                meta.Source = "NutritionCareTerminology";
                meta.Destination = "NutritionCareTerminology";
                meta.spInsert = "proc_NutritionCareTerminologyInsert";
                meta.spUpdate = "proc_NutritionCareTerminologyUpdate";
                meta.spDelete = "proc_NutritionCareTerminologyDelete";
                meta.spLoadAll = "proc_NutritionCareTerminologyLoadAll";
                meta.spLoadByPrimaryKey = "proc_NutritionCareTerminologyLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private NutritionCareTerminologyMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
