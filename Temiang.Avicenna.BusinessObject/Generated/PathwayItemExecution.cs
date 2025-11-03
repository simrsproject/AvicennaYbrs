/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 06/07/19 9:25:02 AM
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
    abstract public class esPathwayItemExecutionCollection : esEntityCollectionWAuditLog
    {
        public esPathwayItemExecutionCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "PathwayItemExecutionCollection";
        }

        #region Query Logic
        protected void InitQuery(esPathwayItemExecutionQuery query)
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
            this.InitQuery(query as esPathwayItemExecutionQuery);
        }
        #endregion

        virtual public PathwayItemExecution DetachEntity(PathwayItemExecution entity)
        {
            return base.DetachEntity(entity) as PathwayItemExecution;
        }

        virtual public PathwayItemExecution AttachEntity(PathwayItemExecution entity)
        {
            return base.AttachEntity(entity) as PathwayItemExecution;
        }

        virtual public void Combine(PathwayItemExecutionCollection collection)
        {
            base.Combine(collection);
        }

        new public PathwayItemExecution this[int index]
        {
            get
            {
                return base[index] as PathwayItemExecution;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(PathwayItemExecution);
        }
    }

    [Serializable]
    abstract public class esPathwayItemExecution : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esPathwayItemExecutionQuery GetDynamicQuery()
        {
            return null;
        }

        public esPathwayItemExecution()
        {
        }

        public esPathwayItemExecution(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String pathwayID, Int32 pathwayItemSeqNo, Int32 dayNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(pathwayID, pathwayItemSeqNo, dayNo);
            else
                return LoadByPrimaryKeyStoredProcedure(pathwayID, pathwayItemSeqNo, dayNo);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String pathwayID, Int32 pathwayItemSeqNo, Int32 dayNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(pathwayID, pathwayItemSeqNo, dayNo);
            else
                return LoadByPrimaryKeyStoredProcedure(pathwayID, pathwayItemSeqNo, dayNo);
        }

        private bool LoadByPrimaryKeyDynamic(String pathwayID, Int32 pathwayItemSeqNo, Int32 dayNo)
        {
            esPathwayItemExecutionQuery query = this.GetDynamicQuery();
            query.Where(query.PathwayID == pathwayID, query.PathwayItemSeqNo == pathwayItemSeqNo, query.DayNo == dayNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String pathwayID, Int32 pathwayItemSeqNo, Int32 dayNo)
        {
            esParameters parms = new esParameters();
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
                        case "PathwayID": this.str.PathwayID = (string)value; break;
                        case "PathwayItemSeqNo": this.str.PathwayItemSeqNo = (string)value; break;
                        case "DayNo": this.str.DayNo = (string)value; break;
                        case "SRPathwayExecutionType": this.str.SRPathwayExecutionType = (string)value; break;
                        case "IsActive": this.str.IsActive = (string)value; break;
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
                        case "IsActive":

                            if (value == null || value is System.Boolean)
                                this.IsActive = (System.Boolean?)value;
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
        /// Maps to PathwayItemExecution.PathwayID
        /// </summary>
        virtual public System.String PathwayID
        {
            get
            {
                return base.GetSystemString(PathwayItemExecutionMetadata.ColumnNames.PathwayID);
            }

            set
            {
                base.SetSystemString(PathwayItemExecutionMetadata.ColumnNames.PathwayID, value);
            }
        }
        /// <summary>
        /// Maps to PathwayItemExecution.PathwayItemSeqNo
        /// </summary>
        virtual public System.Int32? PathwayItemSeqNo
        {
            get
            {
                return base.GetSystemInt32(PathwayItemExecutionMetadata.ColumnNames.PathwayItemSeqNo);
            }

            set
            {
                base.SetSystemInt32(PathwayItemExecutionMetadata.ColumnNames.PathwayItemSeqNo, value);
            }
        }
        /// <summary>
        /// Maps to PathwayItemExecution.DayNo
        /// </summary>
        virtual public System.Int32? DayNo
        {
            get
            {
                return base.GetSystemInt32(PathwayItemExecutionMetadata.ColumnNames.DayNo);
            }

            set
            {
                base.SetSystemInt32(PathwayItemExecutionMetadata.ColumnNames.DayNo, value);
            }
        }
        /// <summary>
        /// Maps to PathwayItemExecution.SRPathwayExecutionType
        /// </summary>
        virtual public System.String SRPathwayExecutionType
        {
            get
            {
                return base.GetSystemString(PathwayItemExecutionMetadata.ColumnNames.SRPathwayExecutionType);
            }

            set
            {
                base.SetSystemString(PathwayItemExecutionMetadata.ColumnNames.SRPathwayExecutionType, value);
            }
        }
        /// <summary>
        /// Maps to PathwayItemExecution.IsActive
        /// </summary>
        virtual public System.Boolean? IsActive
        {
            get
            {
                return base.GetSystemBoolean(PathwayItemExecutionMetadata.ColumnNames.IsActive);
            }

            set
            {
                base.SetSystemBoolean(PathwayItemExecutionMetadata.ColumnNames.IsActive, value);
            }
        }
        /// <summary>
        /// Maps to PathwayItemExecution.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(PathwayItemExecutionMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(PathwayItemExecutionMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to PathwayItemExecution.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(PathwayItemExecutionMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(PathwayItemExecutionMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esPathwayItemExecution entity)
            {
                this.entity = entity;
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
            public System.String SRPathwayExecutionType
            {
                get
                {
                    System.String data = entity.SRPathwayExecutionType;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRPathwayExecutionType = null;
                    else entity.SRPathwayExecutionType = Convert.ToString(value);
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
            private esPathwayItemExecution entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esPathwayItemExecutionQuery query)
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
                throw new Exception("esPathwayItemExecution can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class PathwayItemExecution : esPathwayItemExecution
    {
    }

    [Serializable]
    abstract public class esPathwayItemExecutionQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return PathwayItemExecutionMetadata.Meta();
            }
        }

        public esQueryItem PathwayID
        {
            get
            {
                return new esQueryItem(this, PathwayItemExecutionMetadata.ColumnNames.PathwayID, esSystemType.String);
            }
        }

        public esQueryItem PathwayItemSeqNo
        {
            get
            {
                return new esQueryItem(this, PathwayItemExecutionMetadata.ColumnNames.PathwayItemSeqNo, esSystemType.Int32);
            }
        }

        public esQueryItem DayNo
        {
            get
            {
                return new esQueryItem(this, PathwayItemExecutionMetadata.ColumnNames.DayNo, esSystemType.Int32);
            }
        }

        public esQueryItem SRPathwayExecutionType
        {
            get
            {
                return new esQueryItem(this, PathwayItemExecutionMetadata.ColumnNames.SRPathwayExecutionType, esSystemType.String);
            }
        }

        public esQueryItem IsActive
        {
            get
            {
                return new esQueryItem(this, PathwayItemExecutionMetadata.ColumnNames.IsActive, esSystemType.Boolean);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, PathwayItemExecutionMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, PathwayItemExecutionMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("PathwayItemExecutionCollection")]
    public partial class PathwayItemExecutionCollection : esPathwayItemExecutionCollection, IEnumerable<PathwayItemExecution>
    {
        public PathwayItemExecutionCollection()
        {

        }

        public static implicit operator List<PathwayItemExecution>(PathwayItemExecutionCollection coll)
        {
            List<PathwayItemExecution> list = new List<PathwayItemExecution>();

            foreach (PathwayItemExecution emp in coll)
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
                return PathwayItemExecutionMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PathwayItemExecutionQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new PathwayItemExecution(row);
        }

        override protected esEntity CreateEntity()
        {
            return new PathwayItemExecution();
        }

        #endregion

        [BrowsableAttribute(false)]
        public PathwayItemExecutionQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PathwayItemExecutionQuery();
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
        public bool Load(PathwayItemExecutionQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public PathwayItemExecution AddNew()
        {
            PathwayItemExecution entity = base.AddNewEntity() as PathwayItemExecution;

            return entity;
        }
        public PathwayItemExecution FindByPrimaryKey(String pathwayID, Int32 pathwayItemSeqNo, Int32 dayNo)
        {
            return base.FindByPrimaryKey(pathwayID, pathwayItemSeqNo, dayNo) as PathwayItemExecution;
        }

        #region IEnumerable< PathwayItemExecution> Members

        IEnumerator<PathwayItemExecution> IEnumerable<PathwayItemExecution>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as PathwayItemExecution;
            }
        }

        #endregion

        private PathwayItemExecutionQuery query;
    }


    /// <summary>
    /// Encapsulates the 'PathwayItemExecution' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("PathwayItemExecution ({PathwayID, PathwayItemSeqNo, DayNo})")]
    [Serializable]
    public partial class PathwayItemExecution : esPathwayItemExecution
    {
        public PathwayItemExecution()
        {
        }

        public PathwayItemExecution(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return PathwayItemExecutionMetadata.Meta();
            }
        }

        override protected esPathwayItemExecutionQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PathwayItemExecutionQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public PathwayItemExecutionQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PathwayItemExecutionQuery();
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
        public bool Load(PathwayItemExecutionQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private PathwayItemExecutionQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class PathwayItemExecutionQuery : esPathwayItemExecutionQuery
    {
        public PathwayItemExecutionQuery()
        {

        }

        public PathwayItemExecutionQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "PathwayItemExecutionQuery";
        }
    }

    [Serializable]
    public partial class PathwayItemExecutionMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected PathwayItemExecutionMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(PathwayItemExecutionMetadata.ColumnNames.PathwayID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = PathwayItemExecutionMetadata.PropertyNames.PathwayID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 50;
            _columns.Add(c);

            c = new esColumnMetadata(PathwayItemExecutionMetadata.ColumnNames.PathwayItemSeqNo, 1, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = PathwayItemExecutionMetadata.PropertyNames.PathwayItemSeqNo;
            c.IsInPrimaryKey = true;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(PathwayItemExecutionMetadata.ColumnNames.DayNo, 2, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = PathwayItemExecutionMetadata.PropertyNames.DayNo;
            c.IsInPrimaryKey = true;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(PathwayItemExecutionMetadata.ColumnNames.SRPathwayExecutionType, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = PathwayItemExecutionMetadata.PropertyNames.SRPathwayExecutionType;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(PathwayItemExecutionMetadata.ColumnNames.IsActive, 4, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = PathwayItemExecutionMetadata.PropertyNames.IsActive;
            _columns.Add(c);

            c = new esColumnMetadata(PathwayItemExecutionMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PathwayItemExecutionMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PathwayItemExecutionMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = PathwayItemExecutionMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public PathwayItemExecutionMetadata Meta()
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
            public const string PathwayID = "PathwayID";
            public const string PathwayItemSeqNo = "PathwayItemSeqNo";
            public const string DayNo = "DayNo";
            public const string SRPathwayExecutionType = "SRPathwayExecutionType";
            public const string IsActive = "IsActive";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string PathwayID = "PathwayID";
            public const string PathwayItemSeqNo = "PathwayItemSeqNo";
            public const string DayNo = "DayNo";
            public const string SRPathwayExecutionType = "SRPathwayExecutionType";
            public const string IsActive = "IsActive";
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
            lock (typeof(PathwayItemExecutionMetadata))
            {
                if (PathwayItemExecutionMetadata.mapDelegates == null)
                {
                    PathwayItemExecutionMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (PathwayItemExecutionMetadata.meta == null)
                {
                    PathwayItemExecutionMetadata.meta = new PathwayItemExecutionMetadata();
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

                meta.AddTypeMap("PathwayID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("PathwayItemSeqNo", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("DayNo", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("SRPathwayExecutionType", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "PathwayItemExecution";
                meta.Destination = "PathwayItemExecution";
                meta.spInsert = "proc_PathwayItemExecutionInsert";
                meta.spUpdate = "proc_PathwayItemExecutionUpdate";
                meta.spDelete = "proc_PathwayItemExecutionDelete";
                meta.spLoadAll = "proc_PathwayItemExecutionLoadAll";
                meta.spLoadByPrimaryKey = "proc_PathwayItemExecutionLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private PathwayItemExecutionMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
