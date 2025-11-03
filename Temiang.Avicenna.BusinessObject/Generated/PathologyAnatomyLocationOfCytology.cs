/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 6/6/2016 1:39:09 PM
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
    abstract public class esPathologyAnatomyLocationOfCytologyCollection : esEntityCollectionWAuditLog
    {
        public esPathologyAnatomyLocationOfCytologyCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "PathologyAnatomyLocationOfCytologyCollection";
        }

        #region Query Logic
        protected void InitQuery(esPathologyAnatomyLocationOfCytologyQuery query)
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
            this.InitQuery(query as esPathologyAnatomyLocationOfCytologyQuery);
        }
        #endregion

        virtual public PathologyAnatomyLocationOfCytology DetachEntity(PathologyAnatomyLocationOfCytology entity)
        {
            return base.DetachEntity(entity) as PathologyAnatomyLocationOfCytology;
        }

        virtual public PathologyAnatomyLocationOfCytology AttachEntity(PathologyAnatomyLocationOfCytology entity)
        {
            return base.AttachEntity(entity) as PathologyAnatomyLocationOfCytology;
        }

        virtual public void Combine(PathologyAnatomyLocationOfCytologyCollection collection)
        {
            base.Combine(collection);
        }

        new public PathologyAnatomyLocationOfCytology this[int index]
        {
            get
            {
                return base[index] as PathologyAnatomyLocationOfCytology;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(PathologyAnatomyLocationOfCytology);
        }
    }

    [Serializable]
    abstract public class esPathologyAnatomyLocationOfCytology : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esPathologyAnatomyLocationOfCytologyQuery GetDynamicQuery()
        {
            return null;
        }

        public esPathologyAnatomyLocationOfCytology()
        {
        }

        public esPathologyAnatomyLocationOfCytology(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String locationID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(locationID);
            else
                return LoadByPrimaryKeyStoredProcedure(locationID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String locationID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(locationID);
            else
                return LoadByPrimaryKeyStoredProcedure(locationID);
        }

        private bool LoadByPrimaryKeyDynamic(String locationID)
        {
            esPathologyAnatomyLocationOfCytologyQuery query = this.GetDynamicQuery();
            query.Where(query.LocationID == locationID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String locationID)
        {
            esParameters parms = new esParameters();
            parms.Add("LocationID", locationID);
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
                        case "LocationID": this.str.LocationID = (string)value; break;
                        case "LocationName": this.str.LocationName = (string)value; break;
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
        /// Maps to PathologyAnatomyLocationOfCytology.LocationID
        /// </summary>
        virtual public System.String LocationID
        {
            get
            {
                return base.GetSystemString(PathologyAnatomyLocationOfCytologyMetadata.ColumnNames.LocationID);
            }

            set
            {
                base.SetSystemString(PathologyAnatomyLocationOfCytologyMetadata.ColumnNames.LocationID, value);
            }
        }
        /// <summary>
        /// Maps to PathologyAnatomyLocationOfCytology.LocationName
        /// </summary>
        virtual public System.String LocationName
        {
            get
            {
                return base.GetSystemString(PathologyAnatomyLocationOfCytologyMetadata.ColumnNames.LocationName);
            }

            set
            {
                base.SetSystemString(PathologyAnatomyLocationOfCytologyMetadata.ColumnNames.LocationName, value);
            }
        }
        /// <summary>
        /// Maps to PathologyAnatomyLocationOfCytology.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(PathologyAnatomyLocationOfCytologyMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(PathologyAnatomyLocationOfCytologyMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to PathologyAnatomyLocationOfCytology.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(PathologyAnatomyLocationOfCytologyMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(PathologyAnatomyLocationOfCytologyMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esPathologyAnatomyLocationOfCytology entity)
            {
                this.entity = entity;
            }
            public System.String LocationID
            {
                get
                {
                    System.String data = entity.LocationID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.LocationID = null;
                    else entity.LocationID = Convert.ToString(value);
                }
            }
            public System.String LocationName
            {
                get
                {
                    System.String data = entity.LocationName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.LocationName = null;
                    else entity.LocationName = Convert.ToString(value);
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
            private esPathologyAnatomyLocationOfCytology entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esPathologyAnatomyLocationOfCytologyQuery query)
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
                throw new Exception("esPathologyAnatomyLocationOfCytology can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class PathologyAnatomyLocationOfCytology : esPathologyAnatomyLocationOfCytology
    {
    }

    [Serializable]
    abstract public class esPathologyAnatomyLocationOfCytologyQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return PathologyAnatomyLocationOfCytologyMetadata.Meta();
            }
        }

        public esQueryItem LocationID
        {
            get
            {
                return new esQueryItem(this, PathologyAnatomyLocationOfCytologyMetadata.ColumnNames.LocationID, esSystemType.String);
            }
        }

        public esQueryItem LocationName
        {
            get
            {
                return new esQueryItem(this, PathologyAnatomyLocationOfCytologyMetadata.ColumnNames.LocationName, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, PathologyAnatomyLocationOfCytologyMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, PathologyAnatomyLocationOfCytologyMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("PathologyAnatomyLocationOfCytologyCollection")]
    public partial class PathologyAnatomyLocationOfCytologyCollection : esPathologyAnatomyLocationOfCytologyCollection, IEnumerable<PathologyAnatomyLocationOfCytology>
    {
        public PathologyAnatomyLocationOfCytologyCollection()
        {

        }

        public static implicit operator List<PathologyAnatomyLocationOfCytology>(PathologyAnatomyLocationOfCytologyCollection coll)
        {
            List<PathologyAnatomyLocationOfCytology> list = new List<PathologyAnatomyLocationOfCytology>();

            foreach (PathologyAnatomyLocationOfCytology emp in coll)
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
                return PathologyAnatomyLocationOfCytologyMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PathologyAnatomyLocationOfCytologyQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new PathologyAnatomyLocationOfCytology(row);
        }

        override protected esEntity CreateEntity()
        {
            return new PathologyAnatomyLocationOfCytology();
        }

        #endregion

        [BrowsableAttribute(false)]
        public PathologyAnatomyLocationOfCytologyQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PathologyAnatomyLocationOfCytologyQuery();
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
        public bool Load(PathologyAnatomyLocationOfCytologyQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public PathologyAnatomyLocationOfCytology AddNew()
        {
            PathologyAnatomyLocationOfCytology entity = base.AddNewEntity() as PathologyAnatomyLocationOfCytology;

            return entity;
        }
        public PathologyAnatomyLocationOfCytology FindByPrimaryKey(String locationID)
        {
            return base.FindByPrimaryKey(locationID) as PathologyAnatomyLocationOfCytology;
        }

        #region IEnumerable< PathologyAnatomyLocationOfCytology> Members

        IEnumerator<PathologyAnatomyLocationOfCytology> IEnumerable<PathologyAnatomyLocationOfCytology>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as PathologyAnatomyLocationOfCytology;
            }
        }

        #endregion

        private PathologyAnatomyLocationOfCytologyQuery query;
    }


    /// <summary>
    /// Encapsulates the 'PathologyAnatomyLocationOfCytology' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("PathologyAnatomyLocationOfCytology ({LocationID})")]
    [Serializable]
    public partial class PathologyAnatomyLocationOfCytology : esPathologyAnatomyLocationOfCytology
    {
        public PathologyAnatomyLocationOfCytology()
        {
        }

        public PathologyAnatomyLocationOfCytology(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return PathologyAnatomyLocationOfCytologyMetadata.Meta();
            }
        }

        override protected esPathologyAnatomyLocationOfCytologyQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PathologyAnatomyLocationOfCytologyQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public PathologyAnatomyLocationOfCytologyQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PathologyAnatomyLocationOfCytologyQuery();
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
        public bool Load(PathologyAnatomyLocationOfCytologyQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private PathologyAnatomyLocationOfCytologyQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class PathologyAnatomyLocationOfCytologyQuery : esPathologyAnatomyLocationOfCytologyQuery
    {
        public PathologyAnatomyLocationOfCytologyQuery()
        {

        }

        public PathologyAnatomyLocationOfCytologyQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "PathologyAnatomyLocationOfCytologyQuery";
        }
    }

    [Serializable]
    public partial class PathologyAnatomyLocationOfCytologyMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected PathologyAnatomyLocationOfCytologyMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(PathologyAnatomyLocationOfCytologyMetadata.ColumnNames.LocationID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = PathologyAnatomyLocationOfCytologyMetadata.PropertyNames.LocationID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(PathologyAnatomyLocationOfCytologyMetadata.ColumnNames.LocationName, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = PathologyAnatomyLocationOfCytologyMetadata.PropertyNames.LocationName;
            c.CharacterMaxLength = 500;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PathologyAnatomyLocationOfCytologyMetadata.ColumnNames.LastUpdateDateTime, 2, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PathologyAnatomyLocationOfCytologyMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PathologyAnatomyLocationOfCytologyMetadata.ColumnNames.LastUpdateByUserID, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = PathologyAnatomyLocationOfCytologyMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public PathologyAnatomyLocationOfCytologyMetadata Meta()
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
            public const string LocationID = "LocationID";
            public const string LocationName = "LocationName";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string LocationID = "LocationID";
            public const string LocationName = "LocationName";
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
            lock (typeof(PathologyAnatomyLocationOfCytologyMetadata))
            {
                if (PathologyAnatomyLocationOfCytologyMetadata.mapDelegates == null)
                {
                    PathologyAnatomyLocationOfCytologyMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (PathologyAnatomyLocationOfCytologyMetadata.meta == null)
                {
                    PathologyAnatomyLocationOfCytologyMetadata.meta = new PathologyAnatomyLocationOfCytologyMetadata();
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

                meta.AddTypeMap("LocationID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LocationName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "PathologyAnatomyLocationOfCytology";
                meta.Destination = "PathologyAnatomyLocationOfCytology";
                meta.spInsert = "proc_PathologyAnatomyLocationOfCytologyInsert";
                meta.spUpdate = "proc_PathologyAnatomyLocationOfCytologyUpdate";
                meta.spDelete = "proc_PathologyAnatomyLocationOfCytologyDelete";
                meta.spLoadAll = "proc_PathologyAnatomyLocationOfCytologyLoadAll";
                meta.spLoadByPrimaryKey = "proc_PathologyAnatomyLocationOfCytologyLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private PathologyAnatomyLocationOfCytologyMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
