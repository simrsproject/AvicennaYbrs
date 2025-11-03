/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 27/06/2024 15:02:58
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
    abstract public class esLoincItemCollection : esEntityCollectionWAuditLog
    {
        public esLoincItemCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "LoincItemCollection";
        }

        #region Query Logic
        protected void InitQuery(esLoincItemQuery query)
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
            this.InitQuery(query as esLoincItemQuery);
        }
        #endregion

        virtual public LoincItem DetachEntity(LoincItem entity)
        {
            return base.DetachEntity(entity) as LoincItem;
        }

        virtual public LoincItem AttachEntity(LoincItem entity)
        {
            return base.AttachEntity(entity) as LoincItem;
        }

        virtual public void Combine(LoincItemCollection collection)
        {
            base.Combine(collection);
        }

        new public LoincItem this[int index]
        {
            get
            {
                return base[index] as LoincItem;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(LoincItem);
        }
    }

    [Serializable]
    abstract public class esLoincItem : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esLoincItemQuery GetDynamicQuery()
        {
            return null;
        }

        public esLoincItem()
        {
        }

        public esLoincItem(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String sRLoinc, String code)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(sRLoinc, code);
            else
                return LoadByPrimaryKeyStoredProcedure(sRLoinc, code);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String sRLoinc, String code)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(sRLoinc, code);
            else
                return LoadByPrimaryKeyStoredProcedure(sRLoinc, code);
        }

        private bool LoadByPrimaryKeyDynamic(String sRLoinc, String code)
        {
            esLoincItemQuery query = this.GetDynamicQuery();
            query.Where(query.SRLoinc == sRLoinc, query.Code == code);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String sRLoinc, String code)
        {
            esParameters parms = new esParameters();
            parms.Add("SRLoinc", sRLoinc);
            parms.Add("Code", code);
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
                        case "SRLoinc": this.str.SRLoinc = (string)value; break;
                        case "Code": this.str.Code = (string)value; break;
                        case "Display": this.str.Display = (string)value; break;
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
        /// Maps to LoincItem.SRLoinc
        /// </summary>
        virtual public System.String SRLoinc
        {
            get
            {
                return base.GetSystemString(LoincItemMetadata.ColumnNames.SRLoinc);
            }

            set
            {
                base.SetSystemString(LoincItemMetadata.ColumnNames.SRLoinc, value);
            }
        }
        /// <summary>
        /// Maps to LoincItem.Code
        /// </summary>
        virtual public System.String Code
        {
            get
            {
                return base.GetSystemString(LoincItemMetadata.ColumnNames.Code);
            }

            set
            {
                base.SetSystemString(LoincItemMetadata.ColumnNames.Code, value);
            }
        }
        /// <summary>
        /// Maps to LoincItem.Display
        /// </summary>
        virtual public System.String Display
        {
            get
            {
                return base.GetSystemString(LoincItemMetadata.ColumnNames.Display);
            }

            set
            {
                base.SetSystemString(LoincItemMetadata.ColumnNames.Display, value);
            }
        }
        /// <summary>
        /// Maps to LoincItem.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(LoincItemMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(LoincItemMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to LoincItem.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(LoincItemMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(LoincItemMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esLoincItem entity)
            {
                this.entity = entity;
            }
            public System.String SRLoinc
            {
                get
                {
                    System.String data = entity.SRLoinc;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRLoinc = null;
                    else entity.SRLoinc = Convert.ToString(value);
                }
            }
            public System.String Code
            {
                get
                {
                    System.String data = entity.Code;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Code = null;
                    else entity.Code = Convert.ToString(value);
                }
            }
            public System.String Display
            {
                get
                {
                    System.String data = entity.Display;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Display = null;
                    else entity.Display = Convert.ToString(value);
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
            private esLoincItem entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esLoincItemQuery query)
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
                throw new Exception("esLoincItem can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class LoincItem : esLoincItem
    {
    }

    [Serializable]
    abstract public class esLoincItemQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return LoincItemMetadata.Meta();
            }
        }

        public esQueryItem SRLoinc
        {
            get
            {
                return new esQueryItem(this, LoincItemMetadata.ColumnNames.SRLoinc, esSystemType.String);
            }
        }

        public esQueryItem Code
        {
            get
            {
                return new esQueryItem(this, LoincItemMetadata.ColumnNames.Code, esSystemType.String);
            }
        }

        public esQueryItem Display
        {
            get
            {
                return new esQueryItem(this, LoincItemMetadata.ColumnNames.Display, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, LoincItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, LoincItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("LoincItemCollection")]
    public partial class LoincItemCollection : esLoincItemCollection, IEnumerable<LoincItem>
    {
        public LoincItemCollection()
        {

        }

        public static implicit operator List<LoincItem>(LoincItemCollection coll)
        {
            List<LoincItem> list = new List<LoincItem>();

            foreach (LoincItem emp in coll)
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
                return LoincItemMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new LoincItemQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new LoincItem(row);
        }

        override protected esEntity CreateEntity()
        {
            return new LoincItem();
        }

        #endregion

        [BrowsableAttribute(false)]
        public LoincItemQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new LoincItemQuery();
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
        public bool Load(LoincItemQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public LoincItem AddNew()
        {
            LoincItem entity = base.AddNewEntity() as LoincItem;

            return entity;
        }
        public LoincItem FindByPrimaryKey(String sRLoinc, String code)
        {
            return base.FindByPrimaryKey(sRLoinc, code) as LoincItem;
        }

        #region IEnumerable< LoincItem> Members

        IEnumerator<LoincItem> IEnumerable<LoincItem>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as LoincItem;
            }
        }

        #endregion

        private LoincItemQuery query;
    }


    /// <summary>
    /// Encapsulates the 'LoincItem' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("LoincItem ({SRLoinc, Code})")]
    [Serializable]
    public partial class LoincItem : esLoincItem
    {
        public LoincItem()
        {
        }

        public LoincItem(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return LoincItemMetadata.Meta();
            }
        }

        override protected esLoincItemQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new LoincItemQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public LoincItemQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new LoincItemQuery();
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
        public bool Load(LoincItemQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private LoincItemQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class LoincItemQuery : esLoincItemQuery
    {
        public LoincItemQuery()
        {

        }

        public LoincItemQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "LoincItemQuery";
        }
    }

    [Serializable]
    public partial class LoincItemMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected LoincItemMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(LoincItemMetadata.ColumnNames.SRLoinc, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = LoincItemMetadata.PropertyNames.SRLoinc;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(LoincItemMetadata.ColumnNames.Code, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = LoincItemMetadata.PropertyNames.Code;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(LoincItemMetadata.ColumnNames.Display, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = LoincItemMetadata.PropertyNames.Display;
            c.CharacterMaxLength = 200;
            _columns.Add(c);

            c = new esColumnMetadata(LoincItemMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = LoincItemMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(LoincItemMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = LoincItemMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public LoincItemMetadata Meta()
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
            public const string SRLoinc = "SRLoinc";
            public const string Code = "Code";
            public const string Display = "Display";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string SRLoinc = "SRLoinc";
            public const string Code = "Code";
            public const string Display = "Display";
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
            lock (typeof(LoincItemMetadata))
            {
                if (LoincItemMetadata.mapDelegates == null)
                {
                    LoincItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (LoincItemMetadata.meta == null)
                {
                    LoincItemMetadata.meta = new LoincItemMetadata();
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

                meta.AddTypeMap("SRLoinc", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Code", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Display", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "LoincItem";
                meta.Destination = "LoincItem";
                meta.spInsert = "proc_LoincItemInsert";
                meta.spUpdate = "proc_LoincItemUpdate";
                meta.spDelete = "proc_LoincItemDelete";
                meta.spLoadAll = "proc_LoincItemLoadAll";
                meta.spLoadByPrimaryKey = "proc_LoincItemLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private LoincItemMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
