/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 05/23/19 1:33:33 PM
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
    abstract public class esAppControlCollection : esEntityCollectionWAuditLog
    {
        public esAppControlCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "AppControlCollection";
        }

        #region Query Logic
        protected void InitQuery(esAppControlQuery query)
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
            this.InitQuery(query as esAppControlQuery);
        }
        #endregion

        virtual public AppControl DetachEntity(AppControl entity)
        {
            return base.DetachEntity(entity) as AppControl;
        }

        virtual public AppControl AttachEntity(AppControl entity)
        {
            return base.AttachEntity(entity) as AppControl;
        }

        virtual public void Combine(AppControlCollection collection)
        {
            base.Combine(collection);
        }

        new public AppControl this[int index]
        {
            get
            {
                return base[index] as AppControl;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(AppControl);
        }
    }

    [Serializable]
    abstract public class esAppControl : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esAppControlQuery GetDynamicQuery()
        {
            return null;
        }

        public esAppControl()
        {
        }

        public esAppControl(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String controlID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(controlID);
            else
                return LoadByPrimaryKeyStoredProcedure(controlID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String controlID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(controlID);
            else
                return LoadByPrimaryKeyStoredProcedure(controlID);
        }

        private bool LoadByPrimaryKeyDynamic(String controlID)
        {
            esAppControlQuery query = this.GetDynamicQuery();
            query.Where(query.ControlID == controlID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String controlID)
        {
            esParameters parms = new esParameters();
            parms.Add("ControlID", controlID);
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
                        case "ControlID": this.str.ControlID = (string)value; break;
                        case "ControlType": this.str.ControlType = (string)value; break;
                        case "Description": this.str.Description = (string)value; break;
                        case "ControlUrl": this.str.ControlUrl = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {

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
        /// Maps to AppControl.ControlID
        /// </summary>
        virtual public System.String ControlID
        {
            get
            {
                return base.GetSystemString(AppControlMetadata.ColumnNames.ControlID);
            }

            set
            {
                base.SetSystemString(AppControlMetadata.ColumnNames.ControlID, value);
            }
        }
        /// <summary>
        /// Maps to AppControl.ControlType
        /// </summary>
        virtual public System.String ControlType
        {
            get
            {
                return base.GetSystemString(AppControlMetadata.ColumnNames.ControlType);
            }

            set
            {
                base.SetSystemString(AppControlMetadata.ColumnNames.ControlType, value);
            }
        }
        /// <summary>
        /// Maps to AppControl.Description
        /// </summary>
        virtual public System.String Description
        {
            get
            {
                return base.GetSystemString(AppControlMetadata.ColumnNames.Description);
            }

            set
            {
                base.SetSystemString(AppControlMetadata.ColumnNames.Description, value);
            }
        }
        /// <summary>
        /// Maps to AppControl.ControlUrl
        /// </summary>
        virtual public System.String ControlUrl
        {
            get
            {
                return base.GetSystemString(AppControlMetadata.ColumnNames.ControlUrl);
            }

            set
            {
                base.SetSystemString(AppControlMetadata.ColumnNames.ControlUrl, value);
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
            public esStrings(esAppControl entity)
            {
                this.entity = entity;
            }
            public System.String ControlID
            {
                get
                {
                    System.String data = entity.ControlID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ControlID = null;
                    else entity.ControlID = Convert.ToString(value);
                }
            }
            public System.String ControlType
            {
                get
                {
                    System.String data = entity.ControlType;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ControlType = null;
                    else entity.ControlType = Convert.ToString(value);
                }
            }
            public System.String Description
            {
                get
                {
                    System.String data = entity.Description;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Description = null;
                    else entity.Description = Convert.ToString(value);
                }
            }
            public System.String ControlUrl
            {
                get
                {
                    System.String data = entity.ControlUrl;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ControlUrl = null;
                    else entity.ControlUrl = Convert.ToString(value);
                }
            }
            private esAppControl entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esAppControlQuery query)
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
                throw new Exception("esAppControl can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class AppControl : esAppControl
    {
    }

    [Serializable]
    abstract public class esAppControlQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return AppControlMetadata.Meta();
            }
        }

        public esQueryItem ControlID
        {
            get
            {
                return new esQueryItem(this, AppControlMetadata.ColumnNames.ControlID, esSystemType.String);
            }
        }

        public esQueryItem ControlType
        {
            get
            {
                return new esQueryItem(this, AppControlMetadata.ColumnNames.ControlType, esSystemType.String);
            }
        }

        public esQueryItem Description
        {
            get
            {
                return new esQueryItem(this, AppControlMetadata.ColumnNames.Description, esSystemType.String);
            }
        }

        public esQueryItem ControlUrl
        {
            get
            {
                return new esQueryItem(this, AppControlMetadata.ColumnNames.ControlUrl, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("AppControlCollection")]
    public partial class AppControlCollection : esAppControlCollection, IEnumerable<AppControl>
    {
        public AppControlCollection()
        {

        }

        public static implicit operator List<AppControl>(AppControlCollection coll)
        {
            List<AppControl> list = new List<AppControl>();

            foreach (AppControl emp in coll)
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
                return AppControlMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new AppControlQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new AppControl(row);
        }

        override protected esEntity CreateEntity()
        {
            return new AppControl();
        }

        #endregion

        [BrowsableAttribute(false)]
        public AppControlQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new AppControlQuery();
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
        public bool Load(AppControlQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public AppControl AddNew()
        {
            AppControl entity = base.AddNewEntity() as AppControl;

            return entity;
        }
        public AppControl FindByPrimaryKey(String controlID)
        {
            return base.FindByPrimaryKey(controlID) as AppControl;
        }

        #region IEnumerable< AppControl> Members

        IEnumerator<AppControl> IEnumerable<AppControl>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as AppControl;
            }
        }

        #endregion

        private AppControlQuery query;
    }


    /// <summary>
    /// Encapsulates the 'AppControl' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("AppControl ({ControlID})")]
    [Serializable]
    public partial class AppControl : esAppControl
    {
        public AppControl()
        {
        }

        public AppControl(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return AppControlMetadata.Meta();
            }
        }

        override protected esAppControlQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new AppControlQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public AppControlQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new AppControlQuery();
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
        public bool Load(AppControlQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private AppControlQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class AppControlQuery : esAppControlQuery
    {
        public AppControlQuery()
        {

        }

        public AppControlQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "AppControlQuery";
        }
    }

    [Serializable]
    public partial class AppControlMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected AppControlMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(AppControlMetadata.ColumnNames.ControlID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = AppControlMetadata.PropertyNames.ControlID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 30;
            _columns.Add(c);

            c = new esColumnMetadata(AppControlMetadata.ColumnNames.ControlType, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = AppControlMetadata.PropertyNames.ControlType;
            c.CharacterMaxLength = 30;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AppControlMetadata.ColumnNames.Description, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = AppControlMetadata.PropertyNames.Description;
            c.CharacterMaxLength = 300;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AppControlMetadata.ColumnNames.ControlUrl, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = AppControlMetadata.PropertyNames.ControlUrl;
            c.CharacterMaxLength = 300;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public AppControlMetadata Meta()
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
            public const string ControlID = "ControlID";
            public const string ControlType = "ControlType";
            public const string Description = "Description";
            public const string ControlUrl = "ControlUrl";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string ControlID = "ControlID";
            public const string ControlType = "ControlType";
            public const string Description = "Description";
            public const string ControlUrl = "ControlUrl";
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
            lock (typeof(AppControlMetadata))
            {
                if (AppControlMetadata.mapDelegates == null)
                {
                    AppControlMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (AppControlMetadata.meta == null)
                {
                    AppControlMetadata.meta = new AppControlMetadata();
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

                meta.AddTypeMap("ControlID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ControlType", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Description", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ControlUrl", new esTypeMap("varchar", "System.String"));


                meta.Source = "AppControl";
                meta.Destination = "AppControl";
                meta.spInsert = "proc_AppControlInsert";
                meta.spUpdate = "proc_AppControlUpdate";
                meta.spDelete = "proc_AppControlDelete";
                meta.spLoadAll = "proc_AppControlLoadAll";
                meta.spLoadByPrimaryKey = "proc_AppControlLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private AppControlMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
