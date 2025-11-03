/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 06/07/19 9:24:40 AM
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
    abstract public class esPathwayDiagnoseItemCollection : esEntityCollectionWAuditLog
    {
        public esPathwayDiagnoseItemCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "PathwayDiagnoseItemCollection";
        }

        #region Query Logic
        protected void InitQuery(esPathwayDiagnoseItemQuery query)
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
            this.InitQuery(query as esPathwayDiagnoseItemQuery);
        }
        #endregion

        virtual public PathwayDiagnoseItem DetachEntity(PathwayDiagnoseItem entity)
        {
            return base.DetachEntity(entity) as PathwayDiagnoseItem;
        }

        virtual public PathwayDiagnoseItem AttachEntity(PathwayDiagnoseItem entity)
        {
            return base.AttachEntity(entity) as PathwayDiagnoseItem;
        }

        virtual public void Combine(PathwayDiagnoseItemCollection collection)
        {
            base.Combine(collection);
        }

        new public PathwayDiagnoseItem this[int index]
        {
            get
            {
                return base[index] as PathwayDiagnoseItem;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(PathwayDiagnoseItem);
        }
    }

    [Serializable]
    abstract public class esPathwayDiagnoseItem : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esPathwayDiagnoseItemQuery GetDynamicQuery()
        {
            return null;
        }

        public esPathwayDiagnoseItem()
        {
        }

        public esPathwayDiagnoseItem(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String pathwayID, String diagnoseID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(pathwayID, diagnoseID);
            else
                return LoadByPrimaryKeyStoredProcedure(pathwayID, diagnoseID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String pathwayID, String diagnoseID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(pathwayID, diagnoseID);
            else
                return LoadByPrimaryKeyStoredProcedure(pathwayID, diagnoseID);
        }

        private bool LoadByPrimaryKeyDynamic(String pathwayID, String diagnoseID)
        {
            esPathwayDiagnoseItemQuery query = this.GetDynamicQuery();
            query.Where(query.PathwayID == pathwayID, query.DiagnoseID == diagnoseID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String pathwayID, String diagnoseID)
        {
            esParameters parms = new esParameters();
            parms.Add("PathwayID", pathwayID);
            parms.Add("DiagnoseID", diagnoseID);
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
                        case "DiagnoseID": this.str.DiagnoseID = (string)value; break;
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
        /// Maps to PathwayDiagnoseItem.PathwayID
        /// </summary>
        virtual public System.String PathwayID
        {
            get
            {
                return base.GetSystemString(PathwayDiagnoseItemMetadata.ColumnNames.PathwayID);
            }

            set
            {
                base.SetSystemString(PathwayDiagnoseItemMetadata.ColumnNames.PathwayID, value);
            }
        }
        /// <summary>
        /// Maps to PathwayDiagnoseItem.DiagnoseID
        /// </summary>
        virtual public System.String DiagnoseID
        {
            get
            {
                return base.GetSystemString(PathwayDiagnoseItemMetadata.ColumnNames.DiagnoseID);
            }

            set
            {
                base.SetSystemString(PathwayDiagnoseItemMetadata.ColumnNames.DiagnoseID, value);
            }
        }
        /// <summary>
        /// Maps to PathwayDiagnoseItem.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(PathwayDiagnoseItemMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(PathwayDiagnoseItemMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to PathwayDiagnoseItem.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(PathwayDiagnoseItemMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(PathwayDiagnoseItemMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esPathwayDiagnoseItem entity)
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
            private esPathwayDiagnoseItem entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esPathwayDiagnoseItemQuery query)
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
                throw new Exception("esPathwayDiagnoseItem can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class PathwayDiagnoseItem : esPathwayDiagnoseItem
    {
    }

    [Serializable]
    abstract public class esPathwayDiagnoseItemQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return PathwayDiagnoseItemMetadata.Meta();
            }
        }

        public esQueryItem PathwayID
        {
            get
            {
                return new esQueryItem(this, PathwayDiagnoseItemMetadata.ColumnNames.PathwayID, esSystemType.String);
            }
        }

        public esQueryItem DiagnoseID
        {
            get
            {
                return new esQueryItem(this, PathwayDiagnoseItemMetadata.ColumnNames.DiagnoseID, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, PathwayDiagnoseItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, PathwayDiagnoseItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("PathwayDiagnoseItemCollection")]
    public partial class PathwayDiagnoseItemCollection : esPathwayDiagnoseItemCollection, IEnumerable<PathwayDiagnoseItem>
    {
        public PathwayDiagnoseItemCollection()
        {

        }

        public static implicit operator List<PathwayDiagnoseItem>(PathwayDiagnoseItemCollection coll)
        {
            List<PathwayDiagnoseItem> list = new List<PathwayDiagnoseItem>();

            foreach (PathwayDiagnoseItem emp in coll)
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
                return PathwayDiagnoseItemMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PathwayDiagnoseItemQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new PathwayDiagnoseItem(row);
        }

        override protected esEntity CreateEntity()
        {
            return new PathwayDiagnoseItem();
        }

        #endregion

        [BrowsableAttribute(false)]
        public PathwayDiagnoseItemQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PathwayDiagnoseItemQuery();
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
        public bool Load(PathwayDiagnoseItemQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public PathwayDiagnoseItem AddNew()
        {
            PathwayDiagnoseItem entity = base.AddNewEntity() as PathwayDiagnoseItem;

            return entity;
        }
        public PathwayDiagnoseItem FindByPrimaryKey(String pathwayID, String diagnoseID)
        {
            return base.FindByPrimaryKey(pathwayID, diagnoseID) as PathwayDiagnoseItem;
        }

        #region IEnumerable< PathwayDiagnoseItem> Members

        IEnumerator<PathwayDiagnoseItem> IEnumerable<PathwayDiagnoseItem>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as PathwayDiagnoseItem;
            }
        }

        #endregion

        private PathwayDiagnoseItemQuery query;
    }


    /// <summary>
    /// Encapsulates the 'PathwayDiagnoseItem' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("PathwayDiagnoseItem ({PathwayID, DiagnoseID})")]
    [Serializable]
    public partial class PathwayDiagnoseItem : esPathwayDiagnoseItem
    {
        public PathwayDiagnoseItem()
        {
        }

        public PathwayDiagnoseItem(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return PathwayDiagnoseItemMetadata.Meta();
            }
        }

        override protected esPathwayDiagnoseItemQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PathwayDiagnoseItemQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public PathwayDiagnoseItemQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PathwayDiagnoseItemQuery();
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
        public bool Load(PathwayDiagnoseItemQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private PathwayDiagnoseItemQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class PathwayDiagnoseItemQuery : esPathwayDiagnoseItemQuery
    {
        public PathwayDiagnoseItemQuery()
        {

        }

        public PathwayDiagnoseItemQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "PathwayDiagnoseItemQuery";
        }
    }

    [Serializable]
    public partial class PathwayDiagnoseItemMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected PathwayDiagnoseItemMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(PathwayDiagnoseItemMetadata.ColumnNames.PathwayID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = PathwayDiagnoseItemMetadata.PropertyNames.PathwayID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 50;
            _columns.Add(c);

            c = new esColumnMetadata(PathwayDiagnoseItemMetadata.ColumnNames.DiagnoseID, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = PathwayDiagnoseItemMetadata.PropertyNames.DiagnoseID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(PathwayDiagnoseItemMetadata.ColumnNames.LastUpdateDateTime, 2, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PathwayDiagnoseItemMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PathwayDiagnoseItemMetadata.ColumnNames.LastUpdateByUserID, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = PathwayDiagnoseItemMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public PathwayDiagnoseItemMetadata Meta()
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
            public const string DiagnoseID = "DiagnoseID";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string PathwayID = "PathwayID";
            public const string DiagnoseID = "DiagnoseID";
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
            lock (typeof(PathwayDiagnoseItemMetadata))
            {
                if (PathwayDiagnoseItemMetadata.mapDelegates == null)
                {
                    PathwayDiagnoseItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (PathwayDiagnoseItemMetadata.meta == null)
                {
                    PathwayDiagnoseItemMetadata.meta = new PathwayDiagnoseItemMetadata();
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
                meta.AddTypeMap("DiagnoseID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "PathwayDiagnoseItem";
                meta.Destination = "PathwayDiagnoseItem";
                meta.spInsert = "proc_PathwayDiagnoseItemInsert";
                meta.spUpdate = "proc_PathwayDiagnoseItemUpdate";
                meta.spDelete = "proc_PathwayDiagnoseItemDelete";
                meta.spLoadAll = "proc_PathwayDiagnoseItemLoadAll";
                meta.spLoadByPrimaryKey = "proc_PathwayDiagnoseItemLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private PathwayDiagnoseItemMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
