/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 6/20/2017 10:25:58 AM
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
    abstract public class esPathologyAnatomyImpressionGroupCollection : esEntityCollectionWAuditLog
    {
        public esPathologyAnatomyImpressionGroupCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "PathologyAnatomyImpressionGroupCollection";
        }

        #region Query Logic
        protected void InitQuery(esPathologyAnatomyImpressionGroupQuery query)
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
            this.InitQuery(query as esPathologyAnatomyImpressionGroupQuery);
        }
        #endregion

        virtual public PathologyAnatomyImpressionGroup DetachEntity(PathologyAnatomyImpressionGroup entity)
        {
            return base.DetachEntity(entity) as PathologyAnatomyImpressionGroup;
        }

        virtual public PathologyAnatomyImpressionGroup AttachEntity(PathologyAnatomyImpressionGroup entity)
        {
            return base.AttachEntity(entity) as PathologyAnatomyImpressionGroup;
        }

        virtual public void Combine(PathologyAnatomyImpressionGroupCollection collection)
        {
            base.Combine(collection);
        }

        new public PathologyAnatomyImpressionGroup this[int index]
        {
            get
            {
                return base[index] as PathologyAnatomyImpressionGroup;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(PathologyAnatomyImpressionGroup);
        }
    }

    [Serializable]
    abstract public class esPathologyAnatomyImpressionGroup : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esPathologyAnatomyImpressionGroupQuery GetDynamicQuery()
        {
            return null;
        }

        public esPathologyAnatomyImpressionGroup()
        {
        }

        public esPathologyAnatomyImpressionGroup(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String groupID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(groupID);
            else
                return LoadByPrimaryKeyStoredProcedure(groupID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String groupID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(groupID);
            else
                return LoadByPrimaryKeyStoredProcedure(groupID);
        }

        private bool LoadByPrimaryKeyDynamic(String groupID)
        {
            esPathologyAnatomyImpressionGroupQuery query = this.GetDynamicQuery();
            query.Where(query.GroupID == groupID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String groupID)
        {
            esParameters parms = new esParameters();
            parms.Add("GroupID", groupID);
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
                        case "GroupID": this.str.GroupID = (string)value; break;
                        case "GroupName": this.str.GroupName = (string)value; break;
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
        /// Maps to PathologyAnatomyImpressionGroup.GroupID
        /// </summary>
        virtual public System.String GroupID
        {
            get
            {
                return base.GetSystemString(PathologyAnatomyImpressionGroupMetadata.ColumnNames.GroupID);
            }

            set
            {
                base.SetSystemString(PathologyAnatomyImpressionGroupMetadata.ColumnNames.GroupID, value);
            }
        }
        /// <summary>
        /// Maps to PathologyAnatomyImpressionGroup.GroupName
        /// </summary>
        virtual public System.String GroupName
        {
            get
            {
                return base.GetSystemString(PathologyAnatomyImpressionGroupMetadata.ColumnNames.GroupName);
            }

            set
            {
                base.SetSystemString(PathologyAnatomyImpressionGroupMetadata.ColumnNames.GroupName, value);
            }
        }
        /// <summary>
        /// Maps to PathologyAnatomyImpressionGroup.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(PathologyAnatomyImpressionGroupMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(PathologyAnatomyImpressionGroupMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to PathologyAnatomyImpressionGroup.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(PathologyAnatomyImpressionGroupMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(PathologyAnatomyImpressionGroupMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esPathologyAnatomyImpressionGroup entity)
            {
                this.entity = entity;
            }
            public System.String GroupID
            {
                get
                {
                    System.String data = entity.GroupID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.GroupID = null;
                    else entity.GroupID = Convert.ToString(value);
                }
            }
            public System.String GroupName
            {
                get
                {
                    System.String data = entity.GroupName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.GroupName = null;
                    else entity.GroupName = Convert.ToString(value);
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
            private esPathologyAnatomyImpressionGroup entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esPathologyAnatomyImpressionGroupQuery query)
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
                throw new Exception("esPathologyAnatomyImpressionGroup can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class PathologyAnatomyImpressionGroup : esPathologyAnatomyImpressionGroup
    {
    }

    [Serializable]
    abstract public class esPathologyAnatomyImpressionGroupQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return PathologyAnatomyImpressionGroupMetadata.Meta();
            }
        }

        public esQueryItem GroupID
        {
            get
            {
                return new esQueryItem(this, PathologyAnatomyImpressionGroupMetadata.ColumnNames.GroupID, esSystemType.String);
            }
        }

        public esQueryItem GroupName
        {
            get
            {
                return new esQueryItem(this, PathologyAnatomyImpressionGroupMetadata.ColumnNames.GroupName, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, PathologyAnatomyImpressionGroupMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, PathologyAnatomyImpressionGroupMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("PathologyAnatomyImpressionGroupCollection")]
    public partial class PathologyAnatomyImpressionGroupCollection : esPathologyAnatomyImpressionGroupCollection, IEnumerable<PathologyAnatomyImpressionGroup>
    {
        public PathologyAnatomyImpressionGroupCollection()
        {

        }

        public static implicit operator List<PathologyAnatomyImpressionGroup>(PathologyAnatomyImpressionGroupCollection coll)
        {
            List<PathologyAnatomyImpressionGroup> list = new List<PathologyAnatomyImpressionGroup>();

            foreach (PathologyAnatomyImpressionGroup emp in coll)
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
                return PathologyAnatomyImpressionGroupMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PathologyAnatomyImpressionGroupQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new PathologyAnatomyImpressionGroup(row);
        }

        override protected esEntity CreateEntity()
        {
            return new PathologyAnatomyImpressionGroup();
        }

        #endregion

        [BrowsableAttribute(false)]
        public PathologyAnatomyImpressionGroupQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PathologyAnatomyImpressionGroupQuery();
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
        public bool Load(PathologyAnatomyImpressionGroupQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public PathologyAnatomyImpressionGroup AddNew()
        {
            PathologyAnatomyImpressionGroup entity = base.AddNewEntity() as PathologyAnatomyImpressionGroup;

            return entity;
        }
        public PathologyAnatomyImpressionGroup FindByPrimaryKey(String groupID)
        {
            return base.FindByPrimaryKey(groupID) as PathologyAnatomyImpressionGroup;
        }

        #region IEnumerable< PathologyAnatomyImpressionGroup> Members

        IEnumerator<PathologyAnatomyImpressionGroup> IEnumerable<PathologyAnatomyImpressionGroup>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as PathologyAnatomyImpressionGroup;
            }
        }

        #endregion

        private PathologyAnatomyImpressionGroupQuery query;
    }


    /// <summary>
    /// Encapsulates the 'PathologyAnatomyImpressionGroup' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("PathologyAnatomyImpressionGroup ({GroupID})")]
    [Serializable]
    public partial class PathologyAnatomyImpressionGroup : esPathologyAnatomyImpressionGroup
    {
        public PathologyAnatomyImpressionGroup()
        {
        }

        public PathologyAnatomyImpressionGroup(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return PathologyAnatomyImpressionGroupMetadata.Meta();
            }
        }

        override protected esPathologyAnatomyImpressionGroupQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PathologyAnatomyImpressionGroupQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public PathologyAnatomyImpressionGroupQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PathologyAnatomyImpressionGroupQuery();
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
        public bool Load(PathologyAnatomyImpressionGroupQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private PathologyAnatomyImpressionGroupQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class PathologyAnatomyImpressionGroupQuery : esPathologyAnatomyImpressionGroupQuery
    {
        public PathologyAnatomyImpressionGroupQuery()
        {

        }

        public PathologyAnatomyImpressionGroupQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "PathologyAnatomyImpressionGroupQuery";
        }
    }

    [Serializable]
    public partial class PathologyAnatomyImpressionGroupMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected PathologyAnatomyImpressionGroupMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(PathologyAnatomyImpressionGroupMetadata.ColumnNames.GroupID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = PathologyAnatomyImpressionGroupMetadata.PropertyNames.GroupID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(PathologyAnatomyImpressionGroupMetadata.ColumnNames.GroupName, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = PathologyAnatomyImpressionGroupMetadata.PropertyNames.GroupName;
            c.CharacterMaxLength = 250;
            _columns.Add(c);

            c = new esColumnMetadata(PathologyAnatomyImpressionGroupMetadata.ColumnNames.LastUpdateDateTime, 2, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PathologyAnatomyImpressionGroupMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PathologyAnatomyImpressionGroupMetadata.ColumnNames.LastUpdateByUserID, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = PathologyAnatomyImpressionGroupMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public PathologyAnatomyImpressionGroupMetadata Meta()
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
            public const string GroupID = "GroupID";
            public const string GroupName = "GroupName";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string GroupID = "GroupID";
            public const string GroupName = "GroupName";
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
            lock (typeof(PathologyAnatomyImpressionGroupMetadata))
            {
                if (PathologyAnatomyImpressionGroupMetadata.mapDelegates == null)
                {
                    PathologyAnatomyImpressionGroupMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (PathologyAnatomyImpressionGroupMetadata.meta == null)
                {
                    PathologyAnatomyImpressionGroupMetadata.meta = new PathologyAnatomyImpressionGroupMetadata();
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

                meta.AddTypeMap("GroupID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("GroupName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "PathologyAnatomyImpressionGroup";
                meta.Destination = "PathologyAnatomyImpressionGroup";
                meta.spInsert = "proc_PathologyAnatomyImpressionGroupInsert";
                meta.spUpdate = "proc_PathologyAnatomyImpressionGroupUpdate";
                meta.spDelete = "proc_PathologyAnatomyImpressionGroupDelete";
                meta.spLoadAll = "proc_PathologyAnatomyImpressionGroupLoadAll";
                meta.spLoadByPrimaryKey = "proc_PathologyAnatomyImpressionGroupLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private PathologyAnatomyImpressionGroupMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
