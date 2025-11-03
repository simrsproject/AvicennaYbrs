/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 6/6/2016 1:08:18 PM
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
    abstract public class esPathologyAnatomySourceOfTissueCollection : esEntityCollectionWAuditLog
    {
        public esPathologyAnatomySourceOfTissueCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "PathologyAnatomySourceOfTissueCollection";
        }

        #region Query Logic
        protected void InitQuery(esPathologyAnatomySourceOfTissueQuery query)
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
            this.InitQuery(query as esPathologyAnatomySourceOfTissueQuery);
        }
        #endregion

        virtual public PathologyAnatomySourceOfTissue DetachEntity(PathologyAnatomySourceOfTissue entity)
        {
            return base.DetachEntity(entity) as PathologyAnatomySourceOfTissue;
        }

        virtual public PathologyAnatomySourceOfTissue AttachEntity(PathologyAnatomySourceOfTissue entity)
        {
            return base.AttachEntity(entity) as PathologyAnatomySourceOfTissue;
        }

        virtual public void Combine(PathologyAnatomySourceOfTissueCollection collection)
        {
            base.Combine(collection);
        }

        new public PathologyAnatomySourceOfTissue this[int index]
        {
            get
            {
                return base[index] as PathologyAnatomySourceOfTissue;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(PathologyAnatomySourceOfTissue);
        }
    }

    [Serializable]
    abstract public class esPathologyAnatomySourceOfTissue : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esPathologyAnatomySourceOfTissueQuery GetDynamicQuery()
        {
            return null;
        }

        public esPathologyAnatomySourceOfTissue()
        {
        }

        public esPathologyAnatomySourceOfTissue(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String sourceOfTissueID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(sourceOfTissueID);
            else
                return LoadByPrimaryKeyStoredProcedure(sourceOfTissueID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String sourceOfTissueID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(sourceOfTissueID);
            else
                return LoadByPrimaryKeyStoredProcedure(sourceOfTissueID);
        }

        private bool LoadByPrimaryKeyDynamic(String sourceOfTissueID)
        {
            esPathologyAnatomySourceOfTissueQuery query = this.GetDynamicQuery();
            query.Where(query.SourceOfTissueID == sourceOfTissueID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String sourceOfTissueID)
        {
            esParameters parms = new esParameters();
            parms.Add("SourceOfTissueID", sourceOfTissueID);
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
                        case "SourceOfTissueID": this.str.SourceOfTissueID = (string)value; break;
                        case "SourceOfTissueName": this.str.SourceOfTissueName = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
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
        /// Maps to PathologyAnatomySourceOfTissue.SourceOfTissueID
        /// </summary>
        virtual public System.String SourceOfTissueID
        {
            get
            {
                return base.GetSystemString(PathologyAnatomySourceOfTissueMetadata.ColumnNames.SourceOfTissueID);
            }

            set
            {
                base.SetSystemString(PathologyAnatomySourceOfTissueMetadata.ColumnNames.SourceOfTissueID, value);
            }
        }
        /// <summary>
        /// Maps to PathologyAnatomySourceOfTissue.SourceOfTissueName
        /// </summary>
        virtual public System.String SourceOfTissueName
        {
            get
            {
                return base.GetSystemString(PathologyAnatomySourceOfTissueMetadata.ColumnNames.SourceOfTissueName);
            }

            set
            {
                base.SetSystemString(PathologyAnatomySourceOfTissueMetadata.ColumnNames.SourceOfTissueName, value);
            }
        }
        /// <summary>
        /// Maps to PathologyAnatomySourceOfTissue.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(PathologyAnatomySourceOfTissueMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(PathologyAnatomySourceOfTissueMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to PathologyAnatomySourceOfTissue.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(PathologyAnatomySourceOfTissueMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(PathologyAnatomySourceOfTissueMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esPathologyAnatomySourceOfTissue entity)
            {
                this.entity = entity;
            }
            public System.String SourceOfTissueID
            {
                get
                {
                    System.String data = entity.SourceOfTissueID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SourceOfTissueID = null;
                    else entity.SourceOfTissueID = Convert.ToString(value);
                }
            }
            public System.String SourceOfTissueName
            {
                get
                {
                    System.String data = entity.SourceOfTissueName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SourceOfTissueName = null;
                    else entity.SourceOfTissueName = Convert.ToString(value);
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
            private esPathologyAnatomySourceOfTissue entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esPathologyAnatomySourceOfTissueQuery query)
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
                throw new Exception("esPathologyAnatomySourceOfTissue can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class PathologyAnatomySourceOfTissue : esPathologyAnatomySourceOfTissue
    {
    }

    [Serializable]
    abstract public class esPathologyAnatomySourceOfTissueQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return PathologyAnatomySourceOfTissueMetadata.Meta();
            }
        }

        public esQueryItem SourceOfTissueID
        {
            get
            {
                return new esQueryItem(this, PathologyAnatomySourceOfTissueMetadata.ColumnNames.SourceOfTissueID, esSystemType.String);
            }
        }

        public esQueryItem SourceOfTissueName
        {
            get
            {
                return new esQueryItem(this, PathologyAnatomySourceOfTissueMetadata.ColumnNames.SourceOfTissueName, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, PathologyAnatomySourceOfTissueMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, PathologyAnatomySourceOfTissueMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("PathologyAnatomySourceOfTissueCollection")]
    public partial class PathologyAnatomySourceOfTissueCollection : esPathologyAnatomySourceOfTissueCollection, IEnumerable<PathologyAnatomySourceOfTissue>
    {
        public PathologyAnatomySourceOfTissueCollection()
        {

        }

        public static implicit operator List<PathologyAnatomySourceOfTissue>(PathologyAnatomySourceOfTissueCollection coll)
        {
            List<PathologyAnatomySourceOfTissue> list = new List<PathologyAnatomySourceOfTissue>();

            foreach (PathologyAnatomySourceOfTissue emp in coll)
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
                return PathologyAnatomySourceOfTissueMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PathologyAnatomySourceOfTissueQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new PathologyAnatomySourceOfTissue(row);
        }

        override protected esEntity CreateEntity()
        {
            return new PathologyAnatomySourceOfTissue();
        }

        #endregion

        [BrowsableAttribute(false)]
        public PathologyAnatomySourceOfTissueQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PathologyAnatomySourceOfTissueQuery();
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
        public bool Load(PathologyAnatomySourceOfTissueQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public PathologyAnatomySourceOfTissue AddNew()
        {
            PathologyAnatomySourceOfTissue entity = base.AddNewEntity() as PathologyAnatomySourceOfTissue;

            return entity;
        }
        public PathologyAnatomySourceOfTissue FindByPrimaryKey(String sourceOfTissueID)
        {
            return base.FindByPrimaryKey(sourceOfTissueID) as PathologyAnatomySourceOfTissue;
        }

        #region IEnumerable< PathologyAnatomySourceOfTissue> Members

        IEnumerator<PathologyAnatomySourceOfTissue> IEnumerable<PathologyAnatomySourceOfTissue>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as PathologyAnatomySourceOfTissue;
            }
        }

        #endregion

        private PathologyAnatomySourceOfTissueQuery query;
    }


    /// <summary>
    /// Encapsulates the 'PathologyAnatomySourceOfTissue' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("PathologyAnatomySourceOfTissue ({SourceOfTissueID})")]
    [Serializable]
    public partial class PathologyAnatomySourceOfTissue : esPathologyAnatomySourceOfTissue
    {
        public PathologyAnatomySourceOfTissue()
        {
        }

        public PathologyAnatomySourceOfTissue(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return PathologyAnatomySourceOfTissueMetadata.Meta();
            }
        }

        override protected esPathologyAnatomySourceOfTissueQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PathologyAnatomySourceOfTissueQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public PathologyAnatomySourceOfTissueQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PathologyAnatomySourceOfTissueQuery();
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
        public bool Load(PathologyAnatomySourceOfTissueQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private PathologyAnatomySourceOfTissueQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class PathologyAnatomySourceOfTissueQuery : esPathologyAnatomySourceOfTissueQuery
    {
        public PathologyAnatomySourceOfTissueQuery()
        {

        }

        public PathologyAnatomySourceOfTissueQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "PathologyAnatomySourceOfTissueQuery";
        }
    }

    [Serializable]
    public partial class PathologyAnatomySourceOfTissueMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected PathologyAnatomySourceOfTissueMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(PathologyAnatomySourceOfTissueMetadata.ColumnNames.SourceOfTissueID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = PathologyAnatomySourceOfTissueMetadata.PropertyNames.SourceOfTissueID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(PathologyAnatomySourceOfTissueMetadata.ColumnNames.SourceOfTissueName, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = PathologyAnatomySourceOfTissueMetadata.PropertyNames.SourceOfTissueName;
            c.CharacterMaxLength = 500;
            _columns.Add(c);

            c = new esColumnMetadata(PathologyAnatomySourceOfTissueMetadata.ColumnNames.LastUpdateDateTime, 2, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PathologyAnatomySourceOfTissueMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PathologyAnatomySourceOfTissueMetadata.ColumnNames.LastUpdateByUserID, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = PathologyAnatomySourceOfTissueMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public PathologyAnatomySourceOfTissueMetadata Meta()
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
            public const string SourceOfTissueID = "SourceOfTissueID";
            public const string SourceOfTissueName = "SourceOfTissueName";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string SourceOfTissueID = "SourceOfTissueID";
            public const string SourceOfTissueName = "SourceOfTissueName";
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
            lock (typeof(PathologyAnatomySourceOfTissueMetadata))
            {
                if (PathologyAnatomySourceOfTissueMetadata.mapDelegates == null)
                {
                    PathologyAnatomySourceOfTissueMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (PathologyAnatomySourceOfTissueMetadata.meta == null)
                {
                    PathologyAnatomySourceOfTissueMetadata.meta = new PathologyAnatomySourceOfTissueMetadata();
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

                meta.AddTypeMap("SourceOfTissueID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SourceOfTissueName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "PathologyAnatomySourceOfTissue";
                meta.Destination = "PathologyAnatomySourceOfTissue";
                meta.spInsert = "proc_PathologyAnatomySourceOfTissueInsert";
                meta.spUpdate = "proc_PathologyAnatomySourceOfTissueUpdate";
                meta.spDelete = "proc_PathologyAnatomySourceOfTissueDelete";
                meta.spLoadAll = "proc_PathologyAnatomySourceOfTissueLoadAll";
                meta.spLoadByPrimaryKey = "proc_PathologyAnatomySourceOfTissueLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private PathologyAnatomySourceOfTissueMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
