/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 6/20/2017 5:24:09 PM
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
    abstract public class esAppMessageCollection : esEntityCollectionWAuditLog
    {
        public esAppMessageCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "AppMessageCollection";
        }

        #region Query Logic
        protected void InitQuery(esAppMessageQuery query)
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
            this.InitQuery(query as esAppMessageQuery);
        }
        #endregion

        virtual public AppMessage DetachEntity(AppMessage entity)
        {
            return base.DetachEntity(entity) as AppMessage;
        }

        virtual public AppMessage AttachEntity(AppMessage entity)
        {
            return base.AttachEntity(entity) as AppMessage;
        }

        virtual public void Combine(AppMessageCollection collection)
        {
            base.Combine(collection);
        }

        new public AppMessage this[int index]
        {
            get
            {
                return base[index] as AppMessage;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(AppMessage);
        }
    }

    [Serializable]
    abstract public class esAppMessage : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esAppMessageQuery GetDynamicQuery()
        {
            return null;
        }

        public esAppMessage()
        {
        }

        public esAppMessage(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String messageID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(messageID);
            else
                return LoadByPrimaryKeyStoredProcedure(messageID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String messageID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(messageID);
            else
                return LoadByPrimaryKeyStoredProcedure(messageID);
        }

        private bool LoadByPrimaryKeyDynamic(String messageID)
        {
            esAppMessageQuery query = this.GetDynamicQuery();
            query.Where(query.MessageID == messageID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String messageID)
        {
            esParameters parms = new esParameters();
            parms.Add("MessageID", messageID);
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
                        case "MessageID": this.str.MessageID = (string)value; break;
                        case "MessageText": this.str.MessageText = (string)value; break;
                        case "MessageTextCustom": this.str.MessageTextCustom = (string)value; break;
                        case "IsError": this.str.IsError = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "IsError":

                            if (value == null || value is System.Boolean)
                                this.IsError = (System.Boolean?)value;
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
        /// Maps to AppMessage.MessageID
        /// </summary>
        virtual public System.String MessageID
        {
            get
            {
                return base.GetSystemString(AppMessageMetadata.ColumnNames.MessageID);
            }

            set
            {
                base.SetSystemString(AppMessageMetadata.ColumnNames.MessageID, value);
            }
        }
        /// <summary>
        /// Maps to AppMessage.MessageText
        /// </summary>
        virtual public System.String MessageText
        {
            get
            {
                return base.GetSystemString(AppMessageMetadata.ColumnNames.MessageText);
            }

            set
            {
                base.SetSystemString(AppMessageMetadata.ColumnNames.MessageText, value);
            }
        }
        /// <summary>
        /// Maps to AppMessage.MessageTextCustom
        /// </summary>
        virtual public System.String MessageTextCustom
        {
            get
            {
                return base.GetSystemString(AppMessageMetadata.ColumnNames.MessageTextCustom);
            }

            set
            {
                base.SetSystemString(AppMessageMetadata.ColumnNames.MessageTextCustom, value);
            }
        }
        /// <summary>
        /// Maps to AppMessage.IsError
        /// </summary>
        virtual public System.Boolean? IsError
        {
            get
            {
                return base.GetSystemBoolean(AppMessageMetadata.ColumnNames.IsError);
            }

            set
            {
                base.SetSystemBoolean(AppMessageMetadata.ColumnNames.IsError, value);
            }
        }
        /// <summary>
        /// Maps to AppMessage.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(AppMessageMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(AppMessageMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to AppMessage.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(AppMessageMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(AppMessageMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esAppMessage entity)
            {
                this.entity = entity;
            }
            public System.String MessageID
            {
                get
                {
                    System.String data = entity.MessageID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.MessageID = null;
                    else entity.MessageID = Convert.ToString(value);
                }
            }
            public System.String MessageText
            {
                get
                {
                    System.String data = entity.MessageText;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.MessageText = null;
                    else entity.MessageText = Convert.ToString(value);
                }
            }
            public System.String MessageTextCustom
            {
                get
                {
                    System.String data = entity.MessageTextCustom;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.MessageTextCustom = null;
                    else entity.MessageTextCustom = Convert.ToString(value);
                }
            }
            public System.String IsError
            {
                get
                {
                    System.Boolean? data = entity.IsError;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsError = null;
                    else entity.IsError = Convert.ToBoolean(value);
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
            private esAppMessage entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esAppMessageQuery query)
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
                throw new Exception("esAppMessage can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class AppMessage : esAppMessage
    {
    }

    [Serializable]
    abstract public class esAppMessageQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return AppMessageMetadata.Meta();
            }
        }

        public esQueryItem MessageID
        {
            get
            {
                return new esQueryItem(this, AppMessageMetadata.ColumnNames.MessageID, esSystemType.String);
            }
        }

        public esQueryItem MessageText
        {
            get
            {
                return new esQueryItem(this, AppMessageMetadata.ColumnNames.MessageText, esSystemType.String);
            }
        }

        public esQueryItem MessageTextCustom
        {
            get
            {
                return new esQueryItem(this, AppMessageMetadata.ColumnNames.MessageTextCustom, esSystemType.String);
            }
        }

        public esQueryItem IsError
        {
            get
            {
                return new esQueryItem(this, AppMessageMetadata.ColumnNames.IsError, esSystemType.Boolean);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, AppMessageMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, AppMessageMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("AppMessageCollection")]
    public partial class AppMessageCollection : esAppMessageCollection, IEnumerable<AppMessage>
    {
        public AppMessageCollection()
        {

        }

        public static implicit operator List<AppMessage>(AppMessageCollection coll)
        {
            List<AppMessage> list = new List<AppMessage>();

            foreach (AppMessage emp in coll)
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
                return AppMessageMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new AppMessageQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new AppMessage(row);
        }

        override protected esEntity CreateEntity()
        {
            return new AppMessage();
        }

        #endregion

        [BrowsableAttribute(false)]
        public AppMessageQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new AppMessageQuery();
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
        public bool Load(AppMessageQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public AppMessage AddNew()
        {
            AppMessage entity = base.AddNewEntity() as AppMessage;

            return entity;
        }
        public AppMessage FindByPrimaryKey(String messageID)
        {
            return base.FindByPrimaryKey(messageID) as AppMessage;
        }

        #region IEnumerable< AppMessage> Members

        IEnumerator<AppMessage> IEnumerable<AppMessage>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as AppMessage;
            }
        }

        #endregion

        private AppMessageQuery query;
    }


    /// <summary>
    /// Encapsulates the 'AppMessage' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("AppMessage ({MessageID})")]
    [Serializable]
    public partial class AppMessage : esAppMessage
    {
        public AppMessage()
        {
        }

        public AppMessage(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return AppMessageMetadata.Meta();
            }
        }

        override protected esAppMessageQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new AppMessageQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public AppMessageQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new AppMessageQuery();
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
        public bool Load(AppMessageQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private AppMessageQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class AppMessageQuery : esAppMessageQuery
    {
        public AppMessageQuery()
        {

        }

        public AppMessageQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "AppMessageQuery";
        }
    }

    [Serializable]
    public partial class AppMessageMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected AppMessageMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(AppMessageMetadata.ColumnNames.MessageID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = AppMessageMetadata.PropertyNames.MessageID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 50;
            _columns.Add(c);

            c = new esColumnMetadata(AppMessageMetadata.ColumnNames.MessageText, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = AppMessageMetadata.PropertyNames.MessageText;
            c.CharacterMaxLength = 500;
            _columns.Add(c);

            c = new esColumnMetadata(AppMessageMetadata.ColumnNames.MessageTextCustom, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = AppMessageMetadata.PropertyNames.MessageTextCustom;
            c.CharacterMaxLength = 500;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AppMessageMetadata.ColumnNames.IsError, 3, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = AppMessageMetadata.PropertyNames.IsError;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AppMessageMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = AppMessageMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AppMessageMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = AppMessageMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public AppMessageMetadata Meta()
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
            public const string MessageID = "MessageID";
            public const string MessageText = "MessageText";
            public const string MessageTextCustom = "MessageTextCustom";
            public const string IsError = "IsError";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string MessageID = "MessageID";
            public const string MessageText = "MessageText";
            public const string MessageTextCustom = "MessageTextCustom";
            public const string IsError = "IsError";
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
            lock (typeof(AppMessageMetadata))
            {
                if (AppMessageMetadata.mapDelegates == null)
                {
                    AppMessageMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (AppMessageMetadata.meta == null)
                {
                    AppMessageMetadata.meta = new AppMessageMetadata();
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

                meta.AddTypeMap("MessageID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("MessageText", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("MessageTextCustom", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsError", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "AppMessage";
                meta.Destination = "AppMessage";
                meta.spInsert = "proc_AppMessageInsert";
                meta.spUpdate = "proc_AppMessageUpdate";
                meta.spDelete = "proc_AppMessageDelete";
                meta.spLoadAll = "proc_AppMessageLoadAll";
                meta.spLoadByPrimaryKey = "proc_AppMessageLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private AppMessageMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
