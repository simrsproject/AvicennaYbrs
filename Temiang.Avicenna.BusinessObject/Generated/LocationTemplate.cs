/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 2/26/2020 12:04:14 PM
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
    abstract public class esLocationTemplateCollection : esEntityCollectionWAuditLog
    {
        public esLocationTemplateCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "LocationTemplateCollection";
        }

        #region Query Logic
        protected void InitQuery(esLocationTemplateQuery query)
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
            this.InitQuery(query as esLocationTemplateQuery);
        }
        #endregion

        virtual public LocationTemplate DetachEntity(LocationTemplate entity)
        {
            return base.DetachEntity(entity) as LocationTemplate;
        }

        virtual public LocationTemplate AttachEntity(LocationTemplate entity)
        {
            return base.AttachEntity(entity) as LocationTemplate;
        }

        virtual public void Combine(LocationTemplateCollection collection)
        {
            base.Combine(collection);
        }

        new public LocationTemplate this[int index]
        {
            get
            {
                return base[index] as LocationTemplate;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(LocationTemplate);
        }
    }

    [Serializable]
    abstract public class esLocationTemplate : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esLocationTemplateQuery GetDynamicQuery()
        {
            return null;
        }

        public esLocationTemplate()
        {
        }

        public esLocationTemplate(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String templateNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(templateNo);
            else
                return LoadByPrimaryKeyStoredProcedure(templateNo);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String templateNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(templateNo);
            else
                return LoadByPrimaryKeyStoredProcedure(templateNo);
        }

        private bool LoadByPrimaryKeyDynamic(String templateNo)
        {
            esLocationTemplateQuery query = this.GetDynamicQuery();
            query.Where(query.TemplateNo == templateNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String templateNo)
        {
            esParameters parms = new esParameters();
            parms.Add("TemplateNo", templateNo);
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
                        case "TemplateNo": this.str.TemplateNo = (string)value; break;
                        case "TemplateName": this.str.TemplateName = (string)value; break;
                        case "LocationID": this.str.LocationID = (string)value; break;
                        case "IsActive": this.str.IsActive = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
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
        /// Maps to LocationTemplate.TemplateNo
        /// </summary>
        virtual public System.String TemplateNo
        {
            get
            {
                return base.GetSystemString(LocationTemplateMetadata.ColumnNames.TemplateNo);
            }

            set
            {
                base.SetSystemString(LocationTemplateMetadata.ColumnNames.TemplateNo, value);
            }
        }
        /// <summary>
        /// Maps to LocationTemplate.TemplateName
        /// </summary>
        virtual public System.String TemplateName
        {
            get
            {
                return base.GetSystemString(LocationTemplateMetadata.ColumnNames.TemplateName);
            }

            set
            {
                base.SetSystemString(LocationTemplateMetadata.ColumnNames.TemplateName, value);
            }
        }
        /// <summary>
        /// Maps to LocationTemplate.LocationID
        /// </summary>
        virtual public System.String LocationID
        {
            get
            {
                return base.GetSystemString(LocationTemplateMetadata.ColumnNames.LocationID);
            }

            set
            {
                base.SetSystemString(LocationTemplateMetadata.ColumnNames.LocationID, value);
            }
        }
        /// <summary>
        /// Maps to LocationTemplate.IsActive
        /// </summary>
        virtual public System.Boolean? IsActive
        {
            get
            {
                return base.GetSystemBoolean(LocationTemplateMetadata.ColumnNames.IsActive);
            }

            set
            {
                base.SetSystemBoolean(LocationTemplateMetadata.ColumnNames.IsActive, value);
            }
        }
        /// <summary>
        /// Maps to LocationTemplate.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(LocationTemplateMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(LocationTemplateMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to LocationTemplate.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(LocationTemplateMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(LocationTemplateMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esLocationTemplate entity)
            {
                this.entity = entity;
            }
            public System.String TemplateNo
            {
                get
                {
                    System.String data = entity.TemplateNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TemplateNo = null;
                    else entity.TemplateNo = Convert.ToString(value);
                }
            }
            public System.String TemplateName
            {
                get
                {
                    System.String data = entity.TemplateName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TemplateName = null;
                    else entity.TemplateName = Convert.ToString(value);
                }
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
            private esLocationTemplate entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esLocationTemplateQuery query)
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
                throw new Exception("esLocationTemplate can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class LocationTemplate : esLocationTemplate
    {
    }

    [Serializable]
    abstract public class esLocationTemplateQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return LocationTemplateMetadata.Meta();
            }
        }

        public esQueryItem TemplateNo
        {
            get
            {
                return new esQueryItem(this, LocationTemplateMetadata.ColumnNames.TemplateNo, esSystemType.String);
            }
        }

        public esQueryItem TemplateName
        {
            get
            {
                return new esQueryItem(this, LocationTemplateMetadata.ColumnNames.TemplateName, esSystemType.String);
            }
        }

        public esQueryItem LocationID
        {
            get
            {
                return new esQueryItem(this, LocationTemplateMetadata.ColumnNames.LocationID, esSystemType.String);
            }
        }

        public esQueryItem IsActive
        {
            get
            {
                return new esQueryItem(this, LocationTemplateMetadata.ColumnNames.IsActive, esSystemType.Boolean);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, LocationTemplateMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, LocationTemplateMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("LocationTemplateCollection")]
    public partial class LocationTemplateCollection : esLocationTemplateCollection, IEnumerable<LocationTemplate>
    {
        public LocationTemplateCollection()
        {

        }

        public static implicit operator List<LocationTemplate>(LocationTemplateCollection coll)
        {
            List<LocationTemplate> list = new List<LocationTemplate>();

            foreach (LocationTemplate emp in coll)
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
                return LocationTemplateMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new LocationTemplateQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new LocationTemplate(row);
        }

        override protected esEntity CreateEntity()
        {
            return new LocationTemplate();
        }

        #endregion

        [BrowsableAttribute(false)]
        public LocationTemplateQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new LocationTemplateQuery();
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
        public bool Load(LocationTemplateQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public LocationTemplate AddNew()
        {
            LocationTemplate entity = base.AddNewEntity() as LocationTemplate;

            return entity;
        }
        public LocationTemplate FindByPrimaryKey(String templateNo)
        {
            return base.FindByPrimaryKey(templateNo) as LocationTemplate;
        }

        #region IEnumerable< LocationTemplate> Members

        IEnumerator<LocationTemplate> IEnumerable<LocationTemplate>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as LocationTemplate;
            }
        }

        #endregion

        private LocationTemplateQuery query;
    }


    /// <summary>
    /// Encapsulates the 'LocationTemplate' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("LocationTemplate ({TemplateNo})")]
    [Serializable]
    public partial class LocationTemplate : esLocationTemplate
    {
        public LocationTemplate()
        {
        }

        public LocationTemplate(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return LocationTemplateMetadata.Meta();
            }
        }

        override protected esLocationTemplateQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new LocationTemplateQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public LocationTemplateQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new LocationTemplateQuery();
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
        public bool Load(LocationTemplateQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private LocationTemplateQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class LocationTemplateQuery : esLocationTemplateQuery
    {
        public LocationTemplateQuery()
        {

        }

        public LocationTemplateQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "LocationTemplateQuery";
        }
    }

    [Serializable]
    public partial class LocationTemplateMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected LocationTemplateMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(LocationTemplateMetadata.ColumnNames.TemplateNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = LocationTemplateMetadata.PropertyNames.TemplateNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(LocationTemplateMetadata.ColumnNames.TemplateName, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = LocationTemplateMetadata.PropertyNames.TemplateName;
            c.CharacterMaxLength = 150;
            _columns.Add(c);

            c = new esColumnMetadata(LocationTemplateMetadata.ColumnNames.LocationID, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = LocationTemplateMetadata.PropertyNames.LocationID;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(LocationTemplateMetadata.ColumnNames.IsActive, 3, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = LocationTemplateMetadata.PropertyNames.IsActive;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(LocationTemplateMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = LocationTemplateMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(LocationTemplateMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = LocationTemplateMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public LocationTemplateMetadata Meta()
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
            public const string TemplateNo = "TemplateNo";
            public const string TemplateName = "TemplateName";
            public const string LocationID = "LocationID";
            public const string IsActive = "IsActive";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string TemplateNo = "TemplateNo";
            public const string TemplateName = "TemplateName";
            public const string LocationID = "LocationID";
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
            lock (typeof(LocationTemplateMetadata))
            {
                if (LocationTemplateMetadata.mapDelegates == null)
                {
                    LocationTemplateMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (LocationTemplateMetadata.meta == null)
                {
                    LocationTemplateMetadata.meta = new LocationTemplateMetadata();
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

                meta.AddTypeMap("TemplateNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("TemplateName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LocationID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "LocationTemplate";
                meta.Destination = "LocationTemplate";
                meta.spInsert = "proc_LocationTemplateInsert";
                meta.spUpdate = "proc_LocationTemplateUpdate";
                meta.spDelete = "proc_LocationTemplateDelete";
                meta.spLoadAll = "proc_LocationTemplateLoadAll";
                meta.spLoadByPrimaryKey = "proc_LocationTemplateLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private LocationTemplateMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
