/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 24/07/2024 20:18:45
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
    abstract public class esJsonBridgingRujukBalikValueTempCollection : esEntityCollectionWAuditLog
    {
        public esJsonBridgingRujukBalikValueTempCollection()
        {

        }

        protected override string GetConnectionName()
        {
            return "sci";
        }

        protected override string GetCollectionName()
        {
            return "JsonBridgingRujukBalikValueTempCollection";
        }

        #region Query Logic
        protected void InitQuery(esJsonBridgingRujukBalikValueTempQuery query)
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
            this.InitQuery(query as esJsonBridgingRujukBalikValueTempQuery);
        }
        #endregion

        virtual public JsonBridgingRujukBalikValueTemp DetachEntity(JsonBridgingRujukBalikValueTemp entity)
        {
            return base.DetachEntity(entity) as JsonBridgingRujukBalikValueTemp;
        }

        virtual public JsonBridgingRujukBalikValueTemp AttachEntity(JsonBridgingRujukBalikValueTemp entity)
        {
            return base.AttachEntity(entity) as JsonBridgingRujukBalikValueTemp;
        }

        virtual public void Combine(JsonBridgingRujukBalikValueTempCollection collection)
        {
            base.Combine(collection);
        }

        new public JsonBridgingRujukBalikValueTemp this[int index]
        {
            get
            {
                return base[index] as JsonBridgingRujukBalikValueTemp;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(JsonBridgingRujukBalikValueTemp);
        }
    }

    [Serializable]
    abstract public class esJsonBridgingRujukBalikValueTemp : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esJsonBridgingRujukBalikValueTempQuery GetDynamicQuery()
        {
            return null;
        }

        public esJsonBridgingRujukBalikValueTemp()
        {
        }

        public esJsonBridgingRujukBalikValueTemp(DataRow row)
            : base(row)
        {
        }

        protected override string GetConnectionName()
        {
            return "sci";
        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String noSRB, String noSep)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(noSRB, noSep);
            else
                return LoadByPrimaryKeyStoredProcedure(noSRB, noSep);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String noSRB, String noSep)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(noSRB, noSep);
            else
                return LoadByPrimaryKeyStoredProcedure(noSRB, noSep);
        }

        private bool LoadByPrimaryKeyDynamic(String noSRB, String noSep)
        {
            esJsonBridgingRujukBalikValueTempQuery query = this.GetDynamicQuery();
            query.Where(query.NoSRB == noSRB, query.NoSep == noSep);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String noSRB, String noSep)
        {
            esParameters parms = new esParameters();
            parms.Add("NoSRB", noSRB);
            parms.Add("NoSep", noSep);
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
                        case "NoSRB": this.str.NoSRB = (string)value; break;
                        case "NoSep": this.str.NoSep = (string)value; break;
                        case "JsonValue": this.str.JsonValue = (string)value; break;
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
        /// Maps to JsonBridgingRujukBalikValueTemp.NoSRB
        /// </summary>
        virtual public System.String NoSRB
        {
            get
            {
                return base.GetSystemString(JsonBridgingRujukBalikValueTempMetadata.ColumnNames.NoSRB);
            }

            set
            {
                base.SetSystemString(JsonBridgingRujukBalikValueTempMetadata.ColumnNames.NoSRB, value);
            }
        }
        /// <summary>
        /// Maps to JsonBridgingRujukBalikValueTemp.NoSep
        /// </summary>
        virtual public System.String NoSep
        {
            get
            {
                return base.GetSystemString(JsonBridgingRujukBalikValueTempMetadata.ColumnNames.NoSep);
            }

            set
            {
                base.SetSystemString(JsonBridgingRujukBalikValueTempMetadata.ColumnNames.NoSep, value);
            }
        }
        /// <summary>
        /// Maps to JsonBridgingRujukBalikValueTemp.JsonValue
        /// </summary>
        virtual public System.String JsonValue
        {
            get
            {
                return base.GetSystemString(JsonBridgingRujukBalikValueTempMetadata.ColumnNames.JsonValue);
            }

            set
            {
                base.SetSystemString(JsonBridgingRujukBalikValueTempMetadata.ColumnNames.JsonValue, value);
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
            public esStrings(esJsonBridgingRujukBalikValueTemp entity)
            {
                this.entity = entity;
            }
            public System.String NoSRB
            {
                get
                {
                    System.String data = entity.NoSRB;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.NoSRB = null;
                    else entity.NoSRB = Convert.ToString(value);
                }
            }
            public System.String NoSep
            {
                get
                {
                    System.String data = entity.NoSep;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.NoSep = null;
                    else entity.NoSep = Convert.ToString(value);
                }
            }
            public System.String JsonValue
            {
                get
                {
                    System.String data = entity.JsonValue;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.JsonValue = null;
                    else entity.JsonValue = Convert.ToString(value);
                }
            }
            private esJsonBridgingRujukBalikValueTemp entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esJsonBridgingRujukBalikValueTempQuery query)
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
                throw new Exception("esJsonBridgingRujukBalikValueTemp can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class JsonBridgingRujukBalikValueTemp : esJsonBridgingRujukBalikValueTemp
    {
    }

    [Serializable]
    abstract public class esJsonBridgingRujukBalikValueTempQuery : esDynamicQuery
    {
        protected override string GetConnectionName()
        {
            return "sci";
        }

        override protected IMetadata Meta
        {
            get
            {
                return JsonBridgingRujukBalikValueTempMetadata.Meta();
            }
        }

        public esQueryItem NoSRB
        {
            get
            {
                return new esQueryItem(this, JsonBridgingRujukBalikValueTempMetadata.ColumnNames.NoSRB, esSystemType.String);
            }
        }

        public esQueryItem NoSep
        {
            get
            {
                return new esQueryItem(this, JsonBridgingRujukBalikValueTempMetadata.ColumnNames.NoSep, esSystemType.String);
            }
        }

        public esQueryItem JsonValue
        {
            get
            {
                return new esQueryItem(this, JsonBridgingRujukBalikValueTempMetadata.ColumnNames.JsonValue, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("JsonBridgingRujukBalikValueTempCollection")]
    public partial class JsonBridgingRujukBalikValueTempCollection : esJsonBridgingRujukBalikValueTempCollection, IEnumerable<JsonBridgingRujukBalikValueTemp>
    {
        public JsonBridgingRujukBalikValueTempCollection()
        {

        }

        public static implicit operator List<JsonBridgingRujukBalikValueTemp>(JsonBridgingRujukBalikValueTempCollection coll)
        {
            List<JsonBridgingRujukBalikValueTemp> list = new List<JsonBridgingRujukBalikValueTemp>();

            foreach (JsonBridgingRujukBalikValueTemp emp in coll)
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
                return JsonBridgingRujukBalikValueTempMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new JsonBridgingRujukBalikValueTempQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new JsonBridgingRujukBalikValueTemp(row);
        }

        override protected esEntity CreateEntity()
        {
            return new JsonBridgingRujukBalikValueTemp();
        }

        #endregion

        [BrowsableAttribute(false)]
        public JsonBridgingRujukBalikValueTempQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new JsonBridgingRujukBalikValueTempQuery();
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
        public bool Load(JsonBridgingRujukBalikValueTempQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public JsonBridgingRujukBalikValueTemp AddNew()
        {
            JsonBridgingRujukBalikValueTemp entity = base.AddNewEntity() as JsonBridgingRujukBalikValueTemp;

            return entity;
        }
        public JsonBridgingRujukBalikValueTemp FindByPrimaryKey(String noSRB, String noSep)
        {
            return base.FindByPrimaryKey(noSRB, noSep) as JsonBridgingRujukBalikValueTemp;
        }

        #region IEnumerable< JsonBridgingRujukBalikValueTemp> Members

        IEnumerator<JsonBridgingRujukBalikValueTemp> IEnumerable<JsonBridgingRujukBalikValueTemp>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as JsonBridgingRujukBalikValueTemp;
            }
        }

        #endregion

        private JsonBridgingRujukBalikValueTempQuery query;
    }


    /// <summary>
    /// Encapsulates the 'JsonBridgingRujukBalikValueTemp' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("JsonBridgingRujukBalikValueTemp ({NoSRB, NoSep})")]
    [Serializable]
    public partial class JsonBridgingRujukBalikValueTemp : esJsonBridgingRujukBalikValueTemp
    {
        public JsonBridgingRujukBalikValueTemp()
        {
        }

        public JsonBridgingRujukBalikValueTemp(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return JsonBridgingRujukBalikValueTempMetadata.Meta();
            }
        }

        override protected esJsonBridgingRujukBalikValueTempQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new JsonBridgingRujukBalikValueTempQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public JsonBridgingRujukBalikValueTempQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new JsonBridgingRujukBalikValueTempQuery();
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
        public bool Load(JsonBridgingRujukBalikValueTempQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private JsonBridgingRujukBalikValueTempQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class JsonBridgingRujukBalikValueTempQuery : esJsonBridgingRujukBalikValueTempQuery
    {
        public JsonBridgingRujukBalikValueTempQuery()
        {

        }

        public JsonBridgingRujukBalikValueTempQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "JsonBridgingRujukBalikValueTempQuery";
        }
    }

    [Serializable]
    public partial class JsonBridgingRujukBalikValueTempMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected JsonBridgingRujukBalikValueTempMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(JsonBridgingRujukBalikValueTempMetadata.ColumnNames.NoSRB, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = JsonBridgingRujukBalikValueTempMetadata.PropertyNames.NoSRB;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 50;
            _columns.Add(c);

            c = new esColumnMetadata(JsonBridgingRujukBalikValueTempMetadata.ColumnNames.NoSep, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = JsonBridgingRujukBalikValueTempMetadata.PropertyNames.NoSep;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 50;
            _columns.Add(c);

            c = new esColumnMetadata(JsonBridgingRujukBalikValueTempMetadata.ColumnNames.JsonValue, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = JsonBridgingRujukBalikValueTempMetadata.PropertyNames.JsonValue;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public JsonBridgingRujukBalikValueTempMetadata Meta()
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
            public const string NoSRB = "NoSRB";
            public const string NoSep = "NoSep";
            public const string JsonValue = "JsonValue";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string NoSRB = "NoSRB";
            public const string NoSep = "NoSep";
            public const string JsonValue = "JsonValue";
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
            lock (typeof(JsonBridgingRujukBalikValueTempMetadata))
            {
                if (JsonBridgingRujukBalikValueTempMetadata.mapDelegates == null)
                {
                    JsonBridgingRujukBalikValueTempMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (JsonBridgingRujukBalikValueTempMetadata.meta == null)
                {
                    JsonBridgingRujukBalikValueTempMetadata.meta = new JsonBridgingRujukBalikValueTempMetadata();
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

                meta.AddTypeMap("NoSRB", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("NoSep", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("JsonValue", new esTypeMap("varchar", "System.String"));


                meta.Source = "JsonBridgingRujukBalikValueTemp";
                meta.Destination = "JsonBridgingRujukBalikValueTemp";
                meta.spInsert = "proc_JsonBridgingRujukBalikValueTempInsert";
                meta.spUpdate = "proc_JsonBridgingRujukBalikValueTempUpdate";
                meta.spDelete = "proc_JsonBridgingRujukBalikValueTempDelete";
                meta.spLoadAll = "proc_JsonBridgingRujukBalikValueTempLoadAll";
                meta.spLoadByPrimaryKey = "proc_JsonBridgingRujukBalikValueTempLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private JsonBridgingRujukBalikValueTempMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
