/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 06/07/19 10:03:10 AM
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
    abstract public class esClinicalPathwayCollection : esEntityCollectionWAuditLog
    {
        public esClinicalPathwayCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "ClinicalPathwayCollection";
        }

        #region Query Logic
        protected void InitQuery(esClinicalPathwayQuery query)
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
            this.InitQuery(query as esClinicalPathwayQuery);
        }
        #endregion

        virtual public ClinicalPathway DetachEntity(ClinicalPathway entity)
        {
            return base.DetachEntity(entity) as ClinicalPathway;
        }

        virtual public ClinicalPathway AttachEntity(ClinicalPathway entity)
        {
            return base.AttachEntity(entity) as ClinicalPathway;
        }

        virtual public void Combine(ClinicalPathwayCollection collection)
        {
            base.Combine(collection);
        }

        new public ClinicalPathway this[int index]
        {
            get
            {
                return base[index] as ClinicalPathway;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(ClinicalPathway);
        }
    }

    [Serializable]
    abstract public class esClinicalPathway : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esClinicalPathwayQuery GetDynamicQuery()
        {
            return null;
        }

        public esClinicalPathway()
        {
        }

        public esClinicalPathway(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String registrationNo, String pathwayID, Int32 pathwayItemSeqNo, Int32 dayNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(registrationNo, pathwayID, pathwayItemSeqNo, dayNo);
            else
                return LoadByPrimaryKeyStoredProcedure(registrationNo, pathwayID, pathwayItemSeqNo, dayNo);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String registrationNo, String pathwayID, Int32 pathwayItemSeqNo, Int32 dayNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(registrationNo, pathwayID, pathwayItemSeqNo, dayNo);
            else
                return LoadByPrimaryKeyStoredProcedure(registrationNo, pathwayID, pathwayItemSeqNo, dayNo);
        }

        private bool LoadByPrimaryKeyDynamic(String registrationNo, String pathwayID, Int32 pathwayItemSeqNo, Int32 dayNo)
        {
            esClinicalPathwayQuery query = this.GetDynamicQuery();
            query.Where(query.RegistrationNo == registrationNo, query.PathwayID == pathwayID, query.PathwayItemSeqNo == pathwayItemSeqNo, query.DayNo == dayNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String registrationNo, String pathwayID, Int32 pathwayItemSeqNo, Int32 dayNo)
        {
            esParameters parms = new esParameters();
            parms.Add("RegistrationNo", registrationNo);
            parms.Add("PathwayID", pathwayID);
            parms.Add("PathwayItemSeqNo", pathwayItemSeqNo);
            parms.Add("DayNo", dayNo);
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
                        case "PathwayID": this.str.PathwayID = (string)value; break;
                        case "PathwayItemSeqNo": this.str.PathwayItemSeqNo = (string)value; break;
                        case "DayNo": this.str.DayNo = (string)value; break;
                        case "IsRealized": this.str.IsRealized = (string)value; break;
                        case "RealizedDateTime": this.str.RealizedDateTime = (string)value; break;
                        case "ReferenceNo": this.str.ReferenceNo = (string)value; break;
                        case "SRTransactionCode": this.str.SRTransactionCode = (string)value; break;
                        case "ItemID": this.str.ItemID = (string)value; break;
                        case "InterventionItemID": this.str.InterventionItemID = (string)value; break;
                        case "Notes": this.str.Notes = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "PathwayItemSeqNo":

                            if (value == null || value is System.Int32)
                                this.PathwayItemSeqNo = (System.Int32?)value;
                            break;
                        case "DayNo":

                            if (value == null || value is System.Int32)
                                this.DayNo = (System.Int32?)value;
                            break;
                        case "IsRealized":

                            if (value == null || value is System.Boolean)
                                this.IsRealized = (System.Boolean?)value;
                            break;
                        case "RealizedDateTime":

                            if (value == null || value is System.DateTime)
                                this.RealizedDateTime = (System.DateTime?)value;
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
        /// Maps to ClinicalPathway.RegistrationNo
        /// </summary>
        virtual public System.String RegistrationNo
        {
            get
            {
                return base.GetSystemString(ClinicalPathwayMetadata.ColumnNames.RegistrationNo);
            }

            set
            {
                base.SetSystemString(ClinicalPathwayMetadata.ColumnNames.RegistrationNo, value);
            }
        }
        /// <summary>
        /// Maps to ClinicalPathway.PathwayID
        /// </summary>
        virtual public System.String PathwayID
        {
            get
            {
                return base.GetSystemString(ClinicalPathwayMetadata.ColumnNames.PathwayID);
            }

            set
            {
                base.SetSystemString(ClinicalPathwayMetadata.ColumnNames.PathwayID, value);
            }
        }
        /// <summary>
        /// Maps to ClinicalPathway.PathwayItemSeqNo
        /// </summary>
        virtual public System.Int32? PathwayItemSeqNo
        {
            get
            {
                return base.GetSystemInt32(ClinicalPathwayMetadata.ColumnNames.PathwayItemSeqNo);
            }

            set
            {
                base.SetSystemInt32(ClinicalPathwayMetadata.ColumnNames.PathwayItemSeqNo, value);
            }
        }
        /// <summary>
        /// Maps to ClinicalPathway.DayNo
        /// </summary>
        virtual public System.Int32? DayNo
        {
            get
            {
                return base.GetSystemInt32(ClinicalPathwayMetadata.ColumnNames.DayNo);
            }

            set
            {
                base.SetSystemInt32(ClinicalPathwayMetadata.ColumnNames.DayNo, value);
            }
        }
        /// <summary>
        /// Maps to ClinicalPathway.IsRealized
        /// </summary>
        virtual public System.Boolean? IsRealized
        {
            get
            {
                return base.GetSystemBoolean(ClinicalPathwayMetadata.ColumnNames.IsRealized);
            }

            set
            {
                base.SetSystemBoolean(ClinicalPathwayMetadata.ColumnNames.IsRealized, value);
            }
        }
        /// <summary>
        /// Maps to ClinicalPathway.RealizedDateTime
        /// </summary>
        virtual public System.DateTime? RealizedDateTime
        {
            get
            {
                return base.GetSystemDateTime(ClinicalPathwayMetadata.ColumnNames.RealizedDateTime);
            }

            set
            {
                base.SetSystemDateTime(ClinicalPathwayMetadata.ColumnNames.RealizedDateTime, value);
            }
        }
        /// <summary>
        /// Maps to ClinicalPathway.ReferenceNo
        /// </summary>
        virtual public System.String ReferenceNo
        {
            get
            {
                return base.GetSystemString(ClinicalPathwayMetadata.ColumnNames.ReferenceNo);
            }

            set
            {
                base.SetSystemString(ClinicalPathwayMetadata.ColumnNames.ReferenceNo, value);
            }
        }
        /// <summary>
        /// Maps to ClinicalPathway.SRTransactionCode
        /// </summary>
        virtual public System.String SRTransactionCode
        {
            get
            {
                return base.GetSystemString(ClinicalPathwayMetadata.ColumnNames.SRTransactionCode);
            }

            set
            {
                base.SetSystemString(ClinicalPathwayMetadata.ColumnNames.SRTransactionCode, value);
            }
        }
        /// <summary>
        /// Maps to ClinicalPathway.ItemID
        /// </summary>
        virtual public System.String ItemID
        {
            get
            {
                return base.GetSystemString(ClinicalPathwayMetadata.ColumnNames.ItemID);
            }

            set
            {
                base.SetSystemString(ClinicalPathwayMetadata.ColumnNames.ItemID, value);
            }
        }
        /// <summary>
        /// Maps to ClinicalPathway.InterventionItemID
        /// </summary>
        virtual public System.String InterventionItemID
        {
            get
            {
                return base.GetSystemString(ClinicalPathwayMetadata.ColumnNames.InterventionItemID);
            }

            set
            {
                base.SetSystemString(ClinicalPathwayMetadata.ColumnNames.InterventionItemID, value);
            }
        }
        /// <summary>
        /// Maps to ClinicalPathway.Notes
        /// </summary>
        virtual public System.String Notes
        {
            get
            {
                return base.GetSystemString(ClinicalPathwayMetadata.ColumnNames.Notes);
            }

            set
            {
                base.SetSystemString(ClinicalPathwayMetadata.ColumnNames.Notes, value);
            }
        }
        /// <summary>
        /// Maps to ClinicalPathway.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(ClinicalPathwayMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(ClinicalPathwayMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to ClinicalPathway.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(ClinicalPathwayMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(ClinicalPathwayMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esClinicalPathway entity)
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
            public System.String PathwayID
            {
                get
                {
                    System.String data = entity.PathwayID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PathwayID = null;
                    else entity.PathwayID = Convert.ToString(value);
                }
            }
            public System.String PathwayItemSeqNo
            {
                get
                {
                    System.Int32? data = entity.PathwayItemSeqNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PathwayItemSeqNo = null;
                    else entity.PathwayItemSeqNo = Convert.ToInt32(value);
                }
            }
            public System.String DayNo
            {
                get
                {
                    System.Int32? data = entity.DayNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DayNo = null;
                    else entity.DayNo = Convert.ToInt32(value);
                }
            }
            public System.String IsRealized
            {
                get
                {
                    System.Boolean? data = entity.IsRealized;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsRealized = null;
                    else entity.IsRealized = Convert.ToBoolean(value);
                }
            }
            public System.String RealizedDateTime
            {
                get
                {
                    System.DateTime? data = entity.RealizedDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RealizedDateTime = null;
                    else entity.RealizedDateTime = Convert.ToDateTime(value);
                }
            }
            public System.String ReferenceNo
            {
                get
                {
                    System.String data = entity.ReferenceNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ReferenceNo = null;
                    else entity.ReferenceNo = Convert.ToString(value);
                }
            }
            public System.String SRTransactionCode
            {
                get
                {
                    System.String data = entity.SRTransactionCode;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRTransactionCode = null;
                    else entity.SRTransactionCode = Convert.ToString(value);
                }
            }
            public System.String ItemID
            {
                get
                {
                    System.String data = entity.ItemID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ItemID = null;
                    else entity.ItemID = Convert.ToString(value);
                }
            }
            public System.String InterventionItemID
            {
                get
                {
                    System.String data = entity.InterventionItemID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.InterventionItemID = null;
                    else entity.InterventionItemID = Convert.ToString(value);
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
            private esClinicalPathway entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esClinicalPathwayQuery query)
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
                throw new Exception("esClinicalPathway can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class ClinicalPathway : esClinicalPathway
    {
    }

    [Serializable]
    abstract public class esClinicalPathwayQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return ClinicalPathwayMetadata.Meta();
            }
        }

        public esQueryItem RegistrationNo
        {
            get
            {
                return new esQueryItem(this, ClinicalPathwayMetadata.ColumnNames.RegistrationNo, esSystemType.String);
            }
        }

        public esQueryItem PathwayID
        {
            get
            {
                return new esQueryItem(this, ClinicalPathwayMetadata.ColumnNames.PathwayID, esSystemType.String);
            }
        }

        public esQueryItem PathwayItemSeqNo
        {
            get
            {
                return new esQueryItem(this, ClinicalPathwayMetadata.ColumnNames.PathwayItemSeqNo, esSystemType.Int32);
            }
        }

        public esQueryItem DayNo
        {
            get
            {
                return new esQueryItem(this, ClinicalPathwayMetadata.ColumnNames.DayNo, esSystemType.Int32);
            }
        }

        public esQueryItem IsRealized
        {
            get
            {
                return new esQueryItem(this, ClinicalPathwayMetadata.ColumnNames.IsRealized, esSystemType.Boolean);
            }
        }

        public esQueryItem RealizedDateTime
        {
            get
            {
                return new esQueryItem(this, ClinicalPathwayMetadata.ColumnNames.RealizedDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem ReferenceNo
        {
            get
            {
                return new esQueryItem(this, ClinicalPathwayMetadata.ColumnNames.ReferenceNo, esSystemType.String);
            }
        }

        public esQueryItem SRTransactionCode
        {
            get
            {
                return new esQueryItem(this, ClinicalPathwayMetadata.ColumnNames.SRTransactionCode, esSystemType.String);
            }
        }

        public esQueryItem ItemID
        {
            get
            {
                return new esQueryItem(this, ClinicalPathwayMetadata.ColumnNames.ItemID, esSystemType.String);
            }
        }

        public esQueryItem InterventionItemID
        {
            get
            {
                return new esQueryItem(this, ClinicalPathwayMetadata.ColumnNames.InterventionItemID, esSystemType.String);
            }
        }

        public esQueryItem Notes
        {
            get
            {
                return new esQueryItem(this, ClinicalPathwayMetadata.ColumnNames.Notes, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, ClinicalPathwayMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, ClinicalPathwayMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("ClinicalPathwayCollection")]
    public partial class ClinicalPathwayCollection : esClinicalPathwayCollection, IEnumerable<ClinicalPathway>
    {
        public ClinicalPathwayCollection()
        {

        }

        public static implicit operator List<ClinicalPathway>(ClinicalPathwayCollection coll)
        {
            List<ClinicalPathway> list = new List<ClinicalPathway>();

            foreach (ClinicalPathway emp in coll)
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
                return ClinicalPathwayMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ClinicalPathwayQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new ClinicalPathway(row);
        }

        override protected esEntity CreateEntity()
        {
            return new ClinicalPathway();
        }

        #endregion

        [BrowsableAttribute(false)]
        public ClinicalPathwayQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ClinicalPathwayQuery();
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
        public bool Load(ClinicalPathwayQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public ClinicalPathway AddNew()
        {
            ClinicalPathway entity = base.AddNewEntity() as ClinicalPathway;

            return entity;
        }
        public ClinicalPathway FindByPrimaryKey(String registrationNo, String pathwayID, Int32 pathwayItemSeqNo, Int32 dayNo)
        {
            return base.FindByPrimaryKey(registrationNo, pathwayID, pathwayItemSeqNo, dayNo) as ClinicalPathway;
        }

        #region IEnumerable< ClinicalPathway> Members

        IEnumerator<ClinicalPathway> IEnumerable<ClinicalPathway>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as ClinicalPathway;
            }
        }

        #endregion

        private ClinicalPathwayQuery query;
    }


    /// <summary>
    /// Encapsulates the 'ClinicalPathway' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("ClinicalPathway ({RegistrationNo, PathwayID, PathwayItemSeqNo, DayNo})")]
    [Serializable]
    public partial class ClinicalPathway : esClinicalPathway
    {
        public ClinicalPathway()
        {
        }

        public ClinicalPathway(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return ClinicalPathwayMetadata.Meta();
            }
        }

        override protected esClinicalPathwayQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ClinicalPathwayQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public ClinicalPathwayQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ClinicalPathwayQuery();
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
        public bool Load(ClinicalPathwayQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private ClinicalPathwayQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class ClinicalPathwayQuery : esClinicalPathwayQuery
    {
        public ClinicalPathwayQuery()
        {

        }

        public ClinicalPathwayQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "ClinicalPathwayQuery";
        }
    }

    [Serializable]
    public partial class ClinicalPathwayMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected ClinicalPathwayMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(ClinicalPathwayMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = ClinicalPathwayMetadata.PropertyNames.RegistrationNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(ClinicalPathwayMetadata.ColumnNames.PathwayID, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = ClinicalPathwayMetadata.PropertyNames.PathwayID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(ClinicalPathwayMetadata.ColumnNames.PathwayItemSeqNo, 2, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = ClinicalPathwayMetadata.PropertyNames.PathwayItemSeqNo;
            c.IsInPrimaryKey = true;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(ClinicalPathwayMetadata.ColumnNames.DayNo, 3, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = ClinicalPathwayMetadata.PropertyNames.DayNo;
            c.IsInPrimaryKey = true;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(ClinicalPathwayMetadata.ColumnNames.IsRealized, 4, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ClinicalPathwayMetadata.PropertyNames.IsRealized;
            _columns.Add(c);

            c = new esColumnMetadata(ClinicalPathwayMetadata.ColumnNames.RealizedDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ClinicalPathwayMetadata.PropertyNames.RealizedDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ClinicalPathwayMetadata.ColumnNames.ReferenceNo, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = ClinicalPathwayMetadata.PropertyNames.ReferenceNo;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ClinicalPathwayMetadata.ColumnNames.SRTransactionCode, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = ClinicalPathwayMetadata.PropertyNames.SRTransactionCode;
            c.CharacterMaxLength = 5;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ClinicalPathwayMetadata.ColumnNames.ItemID, 8, typeof(System.String), esSystemType.String);
            c.PropertyName = ClinicalPathwayMetadata.PropertyNames.ItemID;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ClinicalPathwayMetadata.ColumnNames.InterventionItemID, 9, typeof(System.String), esSystemType.String);
            c.PropertyName = ClinicalPathwayMetadata.PropertyNames.InterventionItemID;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ClinicalPathwayMetadata.ColumnNames.Notes, 10, typeof(System.String), esSystemType.String);
            c.PropertyName = ClinicalPathwayMetadata.PropertyNames.Notes;
            c.CharacterMaxLength = 500;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ClinicalPathwayMetadata.ColumnNames.LastUpdateDateTime, 11, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ClinicalPathwayMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ClinicalPathwayMetadata.ColumnNames.LastUpdateByUserID, 12, typeof(System.String), esSystemType.String);
            c.PropertyName = ClinicalPathwayMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public ClinicalPathwayMetadata Meta()
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
            public const string PathwayID = "PathwayID";
            public const string PathwayItemSeqNo = "PathwayItemSeqNo";
            public const string DayNo = "DayNo";
            public const string IsRealized = "IsRealized";
            public const string RealizedDateTime = "RealizedDateTime";
            public const string ReferenceNo = "ReferenceNo";
            public const string SRTransactionCode = "SRTransactionCode";
            public const string ItemID = "ItemID";
            public const string InterventionItemID = "InterventionItemID";
            public const string Notes = "Notes";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string RegistrationNo = "RegistrationNo";
            public const string PathwayID = "PathwayID";
            public const string PathwayItemSeqNo = "PathwayItemSeqNo";
            public const string DayNo = "DayNo";
            public const string IsRealized = "IsRealized";
            public const string RealizedDateTime = "RealizedDateTime";
            public const string ReferenceNo = "ReferenceNo";
            public const string SRTransactionCode = "SRTransactionCode";
            public const string ItemID = "ItemID";
            public const string InterventionItemID = "InterventionItemID";
            public const string Notes = "Notes";
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
            lock (typeof(ClinicalPathwayMetadata))
            {
                if (ClinicalPathwayMetadata.mapDelegates == null)
                {
                    ClinicalPathwayMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (ClinicalPathwayMetadata.meta == null)
                {
                    ClinicalPathwayMetadata.meta = new ClinicalPathwayMetadata();
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
                meta.AddTypeMap("PathwayID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("PathwayItemSeqNo", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("DayNo", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("IsRealized", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("RealizedDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("ReferenceNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRTransactionCode", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("InterventionItemID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "ClinicalPathway";
                meta.Destination = "ClinicalPathway";
                meta.spInsert = "proc_ClinicalPathwayInsert";
                meta.spUpdate = "proc_ClinicalPathwayUpdate";
                meta.spDelete = "proc_ClinicalPathwayDelete";
                meta.spLoadAll = "proc_ClinicalPathwayLoadAll";
                meta.spLoadByPrimaryKey = "proc_ClinicalPathwayLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private ClinicalPathwayMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
