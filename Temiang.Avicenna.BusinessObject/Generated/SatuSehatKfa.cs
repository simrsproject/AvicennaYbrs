/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 01/04/2024 14:18:54
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
    abstract public class esSatuSehatKfaCollection : esEntityCollectionWAuditLog
    {
        public esSatuSehatKfaCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "SatuSehatKfaCollection";
        }

        #region Query Logic
        protected void InitQuery(esSatuSehatKfaQuery query)
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
            this.InitQuery(query as esSatuSehatKfaQuery);
        }
        #endregion

        virtual public SatuSehatKfa DetachEntity(SatuSehatKfa entity)
        {
            return base.DetachEntity(entity) as SatuSehatKfa;
        }

        virtual public SatuSehatKfa AttachEntity(SatuSehatKfa entity)
        {
            return base.AttachEntity(entity) as SatuSehatKfa;
        }

        virtual public void Combine(SatuSehatKfaCollection collection)
        {
            base.Combine(collection);
        }

        new public SatuSehatKfa this[int index]
        {
            get
            {
                return base[index] as SatuSehatKfa;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(SatuSehatKfa);
        }
    }

    [Serializable]
    abstract public class esSatuSehatKfa : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esSatuSehatKfaQuery GetDynamicQuery()
        {
            return null;
        }

        public esSatuSehatKfa()
        {
        }

        public esSatuSehatKfa(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(Int64 id)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(id);
            else
                return LoadByPrimaryKeyStoredProcedure(id);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int64 id)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(id);
            else
                return LoadByPrimaryKeyStoredProcedure(id);
        }

        private bool LoadByPrimaryKeyDynamic(Int64 id)
        {
            esSatuSehatKfaQuery query = this.GetDynamicQuery();
            query.Where(query.Id == id);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(Int64 id)
        {
            esParameters parms = new esParameters();
            parms.Add("id", id);
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
                        case "Id": this.str.Id = (string)value; break;
                        case "SsUuid": this.str.SsUuid = (string)value; break;
                        case "SsType": this.str.SsType = (string)value; break;
                        case "SsNama": this.str.SsNama = (string)value; break;
                        case "SsResult": this.str.SsResult = (string)value; break;
                        case "SsKfaTotalData": this.str.SsKfaTotalData = (string)value; break;
                        case "CreatedAt": this.str.CreatedAt = (string)value; break;
                        case "UpdatedAt": this.str.UpdatedAt = (string)value; break;
                        case "DeletedAt": this.str.DeletedAt = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "id":

                            if (value == null || value is System.Int64)
                                this.Id = (System.Int64?)value;
                            break;
                        case "ss_kfa_total_data":

                            if (value == null || value is System.Int32)
                                this.SsKfaTotalData = (System.Int32?)value;
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
        /// Maps to SatuSehatKfa.id
        /// </summary>
        virtual public System.Int64? Id
        {
            get
            {
                return base.GetSystemInt64(SatuSehatKfaMetadata.ColumnNames.Id);
            }

            set
            {
                base.SetSystemInt64(SatuSehatKfaMetadata.ColumnNames.Id, value);
            }
        }
        /// <summary>
        /// Maps to SatuSehatKfa.ss_uuid
        /// </summary>
        virtual public System.String SsUuid
        {
            get
            {
                return base.GetSystemString(SatuSehatKfaMetadata.ColumnNames.SsUuid);
            }

            set
            {
                base.SetSystemString(SatuSehatKfaMetadata.ColumnNames.SsUuid, value);
            }
        }
        /// <summary>
        /// Maps to SatuSehatKfa.ss_type
        /// </summary>
        virtual public System.String SsType
        {
            get
            {
                return base.GetSystemString(SatuSehatKfaMetadata.ColumnNames.SsType);
            }

            set
            {
                base.SetSystemString(SatuSehatKfaMetadata.ColumnNames.SsType, value);
            }
        }
        /// <summary>
        /// Maps to SatuSehatKfa.ss_nama
        /// </summary>
        virtual public System.String SsNama
        {
            get
            {
                return base.GetSystemString(SatuSehatKfaMetadata.ColumnNames.SsNama);
            }

            set
            {
                base.SetSystemString(SatuSehatKfaMetadata.ColumnNames.SsNama, value);
            }
        }
        /// <summary>
        /// Maps to SatuSehatKfa.ss_result
        /// </summary>
        virtual public System.String SsResult
        {
            get
            {
                return base.GetSystemString(SatuSehatKfaMetadata.ColumnNames.SsResult);
            }

            set
            {
                base.SetSystemString(SatuSehatKfaMetadata.ColumnNames.SsResult, value);
            }
        }
        /// <summary>
        /// Maps to SatuSehatKfa.ss_kfa_total_data
        /// </summary>
        virtual public System.Int32? SsKfaTotalData
        {
            get
            {
                return base.GetSystemInt32(SatuSehatKfaMetadata.ColumnNames.SsKfaTotalData);
            }

            set
            {
                base.SetSystemInt32(SatuSehatKfaMetadata.ColumnNames.SsKfaTotalData, value);
            }
        }
        /// <summary>
        /// Maps to SatuSehatKfa.created_at
        /// </summary>
        virtual public System.String CreatedAt
        {
            get
            {
                return base.GetSystemString(SatuSehatKfaMetadata.ColumnNames.CreatedAt);
            }

            set
            {
                base.SetSystemString(SatuSehatKfaMetadata.ColumnNames.CreatedAt, value);
            }
        }
        /// <summary>
        /// Maps to SatuSehatKfa.updated_at
        /// </summary>
        virtual public System.String UpdatedAt
        {
            get
            {
                return base.GetSystemString(SatuSehatKfaMetadata.ColumnNames.UpdatedAt);
            }

            set
            {
                base.SetSystemString(SatuSehatKfaMetadata.ColumnNames.UpdatedAt, value);
            }
        }
        /// <summary>
        /// Maps to SatuSehatKfa.deleted_at
        /// </summary>
        virtual public System.String DeletedAt
        {
            get
            {
                return base.GetSystemString(SatuSehatKfaMetadata.ColumnNames.DeletedAt);
            }

            set
            {
                base.SetSystemString(SatuSehatKfaMetadata.ColumnNames.DeletedAt, value);
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
            public esStrings(esSatuSehatKfa entity)
            {
                this.entity = entity;
            }
            public System.String Id
            {
                get
                {
                    System.Int64? data = entity.Id;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Id = null;
                    else entity.Id = Convert.ToInt64(value);
                }
            }
            public System.String SsUuid
            {
                get
                {
                    System.String data = entity.SsUuid;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SsUuid = null;
                    else entity.SsUuid = Convert.ToString(value);
                }
            }
            public System.String SsType
            {
                get
                {
                    System.String data = entity.SsType;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SsType = null;
                    else entity.SsType = Convert.ToString(value);
                }
            }
            public System.String SsNama
            {
                get
                {
                    System.String data = entity.SsNama;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SsNama = null;
                    else entity.SsNama = Convert.ToString(value);
                }
            }
            public System.String SsResult
            {
                get
                {
                    System.String data = entity.SsResult;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SsResult = null;
                    else entity.SsResult = Convert.ToString(value);
                }
            }
            public System.String SsKfaTotalData
            {
                get
                {
                    System.Int32? data = entity.SsKfaTotalData;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SsKfaTotalData = null;
                    else entity.SsKfaTotalData = Convert.ToInt32(value);
                }
            }
            public System.String CreatedAt
            {
                get
                {
                    System.String data = entity.CreatedAt;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CreatedAt = null;
                    else entity.CreatedAt = Convert.ToString(value);
                }
            }
            public System.String UpdatedAt
            {
                get
                {
                    System.String data = entity.UpdatedAt;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.UpdatedAt = null;
                    else entity.UpdatedAt = Convert.ToString(value);
                }
            }
            public System.String DeletedAt
            {
                get
                {
                    System.String data = entity.DeletedAt;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DeletedAt = null;
                    else entity.DeletedAt = Convert.ToString(value);
                }
            }
            private esSatuSehatKfa entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esSatuSehatKfaQuery query)
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
                throw new Exception("esSatuSehatKfa can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class SatuSehatKfa : esSatuSehatKfa
    {
    }

    [Serializable]
    abstract public class esSatuSehatKfaQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return SatuSehatKfaMetadata.Meta();
            }
        }

        public esQueryItem Id
        {
            get
            {
                return new esQueryItem(this, SatuSehatKfaMetadata.ColumnNames.Id, esSystemType.Int64);
            }
        }

        public esQueryItem SsUuid
        {
            get
            {
                return new esQueryItem(this, SatuSehatKfaMetadata.ColumnNames.SsUuid, esSystemType.String);
            }
        }

        public esQueryItem SsType
        {
            get
            {
                return new esQueryItem(this, SatuSehatKfaMetadata.ColumnNames.SsType, esSystemType.String);
            }
        }

        public esQueryItem SsNama
        {
            get
            {
                return new esQueryItem(this, SatuSehatKfaMetadata.ColumnNames.SsNama, esSystemType.String);
            }
        }

        public esQueryItem SsResult
        {
            get
            {
                return new esQueryItem(this, SatuSehatKfaMetadata.ColumnNames.SsResult, esSystemType.String);
            }
        }

        public esQueryItem SsKfaTotalData
        {
            get
            {
                return new esQueryItem(this, SatuSehatKfaMetadata.ColumnNames.SsKfaTotalData, esSystemType.Int32);
            }
        }

        public esQueryItem CreatedAt
        {
            get
            {
                return new esQueryItem(this, SatuSehatKfaMetadata.ColumnNames.CreatedAt, esSystemType.String);
            }
        }

        public esQueryItem UpdatedAt
        {
            get
            {
                return new esQueryItem(this, SatuSehatKfaMetadata.ColumnNames.UpdatedAt, esSystemType.String);
            }
        }

        public esQueryItem DeletedAt
        {
            get
            {
                return new esQueryItem(this, SatuSehatKfaMetadata.ColumnNames.DeletedAt, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("SatuSehatKfaCollection")]
    public partial class SatuSehatKfaCollection : esSatuSehatKfaCollection, IEnumerable<SatuSehatKfa>
    {
        public SatuSehatKfaCollection()
        {

        }

        public static implicit operator List<SatuSehatKfa>(SatuSehatKfaCollection coll)
        {
            List<SatuSehatKfa> list = new List<SatuSehatKfa>();

            foreach (SatuSehatKfa emp in coll)
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
                return SatuSehatKfaMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new SatuSehatKfaQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new SatuSehatKfa(row);
        }

        override protected esEntity CreateEntity()
        {
            return new SatuSehatKfa();
        }

        #endregion

        [BrowsableAttribute(false)]
        public SatuSehatKfaQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new SatuSehatKfaQuery();
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
        public bool Load(SatuSehatKfaQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public SatuSehatKfa AddNew()
        {
            SatuSehatKfa entity = base.AddNewEntity() as SatuSehatKfa;

            return entity;
        }
        public SatuSehatKfa FindByPrimaryKey(Int64 id)
        {
            return base.FindByPrimaryKey(id) as SatuSehatKfa;
        }

        #region IEnumerable< SatuSehatKfa> Members

        IEnumerator<SatuSehatKfa> IEnumerable<SatuSehatKfa>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as SatuSehatKfa;
            }
        }

        #endregion

        private SatuSehatKfaQuery query;
    }


    /// <summary>
    /// Encapsulates the 'SatuSehatKfa' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("SatuSehatKfa ({id})")]
    [Serializable]
    public partial class SatuSehatKfa : esSatuSehatKfa
    {
        public SatuSehatKfa()
        {
        }

        public SatuSehatKfa(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return SatuSehatKfaMetadata.Meta();
            }
        }

        override protected esSatuSehatKfaQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new SatuSehatKfaQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public SatuSehatKfaQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new SatuSehatKfaQuery();
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
        public bool Load(SatuSehatKfaQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private SatuSehatKfaQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class SatuSehatKfaQuery : esSatuSehatKfaQuery
    {
        public SatuSehatKfaQuery()
        {

        }

        public SatuSehatKfaQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "SatuSehatKfaQuery";
        }
    }

    [Serializable]
    public partial class SatuSehatKfaMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected SatuSehatKfaMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(SatuSehatKfaMetadata.ColumnNames.Id, 0, typeof(System.Int64), esSystemType.Int64);
            c.PropertyName = SatuSehatKfaMetadata.PropertyNames.Id;
            c.IsInPrimaryKey = true;
            c.NumericPrecision = 19;
            _columns.Add(c);

            c = new esColumnMetadata(SatuSehatKfaMetadata.ColumnNames.SsUuid, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = SatuSehatKfaMetadata.PropertyNames.SsUuid;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(SatuSehatKfaMetadata.ColumnNames.SsType, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = SatuSehatKfaMetadata.PropertyNames.SsType;
            c.CharacterMaxLength = 50;
            _columns.Add(c);

            c = new esColumnMetadata(SatuSehatKfaMetadata.ColumnNames.SsNama, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = SatuSehatKfaMetadata.PropertyNames.SsNama;
            c.CharacterMaxLength = 1000;
            _columns.Add(c);

            c = new esColumnMetadata(SatuSehatKfaMetadata.ColumnNames.SsResult, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = SatuSehatKfaMetadata.PropertyNames.SsResult;
            c.CharacterMaxLength = 2147483647;
            _columns.Add(c);

            c = new esColumnMetadata(SatuSehatKfaMetadata.ColumnNames.SsKfaTotalData, 5, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = SatuSehatKfaMetadata.PropertyNames.SsKfaTotalData;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(SatuSehatKfaMetadata.ColumnNames.CreatedAt, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = SatuSehatKfaMetadata.PropertyNames.CreatedAt;
            c.CharacterMaxLength = 50;
            _columns.Add(c);

            c = new esColumnMetadata(SatuSehatKfaMetadata.ColumnNames.UpdatedAt, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = SatuSehatKfaMetadata.PropertyNames.UpdatedAt;
            c.CharacterMaxLength = 50;
            _columns.Add(c);

            c = new esColumnMetadata(SatuSehatKfaMetadata.ColumnNames.DeletedAt, 8, typeof(System.String), esSystemType.String);
            c.PropertyName = SatuSehatKfaMetadata.PropertyNames.DeletedAt;
            c.CharacterMaxLength = 50;
            _columns.Add(c);


        }
        #endregion

        static public SatuSehatKfaMetadata Meta()
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
            public const string Id = "id";
            public const string SsUuid = "ss_uuid";
            public const string SsType = "ss_type";
            public const string SsNama = "ss_nama";
            public const string SsResult = "ss_result";
            public const string SsKfaTotalData = "ss_kfa_total_data";
            public const string CreatedAt = "created_at";
            public const string UpdatedAt = "updated_at";
            public const string DeletedAt = "deleted_at";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string Id = "Id";
            public const string SsUuid = "SsUuid";
            public const string SsType = "SsType";
            public const string SsNama = "SsNama";
            public const string SsResult = "SsResult";
            public const string SsKfaTotalData = "SsKfaTotalData";
            public const string CreatedAt = "CreatedAt";
            public const string UpdatedAt = "UpdatedAt";
            public const string DeletedAt = "DeletedAt";
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
            lock (typeof(SatuSehatKfaMetadata))
            {
                if (SatuSehatKfaMetadata.mapDelegates == null)
                {
                    SatuSehatKfaMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (SatuSehatKfaMetadata.meta == null)
                {
                    SatuSehatKfaMetadata.meta = new SatuSehatKfaMetadata();
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

                meta.AddTypeMap("Id", new esTypeMap("bigint", "System.Int64"));
                meta.AddTypeMap("SsUuid", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SsType", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SsNama", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SsResult", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SsKfaTotalData", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("CreatedAt", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("UpdatedAt", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("DeletedAt", new esTypeMap("varchar", "System.String"));


                meta.Source = "SatuSehatKfa";
                meta.Destination = "SatuSehatKfa";
                meta.spInsert = "proc_SatuSehatKfaInsert";
                meta.spUpdate = "proc_SatuSehatKfaUpdate";
                meta.spDelete = "proc_SatuSehatKfaDelete";
                meta.spLoadAll = "proc_SatuSehatKfaLoadAll";
                meta.spLoadByPrimaryKey = "proc_SatuSehatKfaLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private SatuSehatKfaMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
