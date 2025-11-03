/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 3/22/2016 8:23:02 AM
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
    abstract public class esPCareReferenceCollection : esEntityCollectionWAuditLog
    {
        public esPCareReferenceCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "PCareReferenceCollection";
        }

        #region Query Logic
        protected void InitQuery(esPCareReferenceQuery query)
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
            this.InitQuery(query as esPCareReferenceQuery);
        }
        #endregion

        virtual public PCareReference DetachEntity(PCareReference entity)
        {
            return base.DetachEntity(entity) as PCareReference;
        }

        virtual public PCareReference AttachEntity(PCareReference entity)
        {
            return base.AttachEntity(entity) as PCareReference;
        }

        virtual public void Combine(PCareReferenceCollection collection)
        {
            base.Combine(collection);
        }

        new public PCareReference this[int index]
        {
            get
            {
                return base[index] as PCareReference;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(PCareReference);
        }
    }

    [Serializable]
    abstract public class esPCareReference : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esPCareReferenceQuery GetDynamicQuery()
        {
            return null;
        }

        public esPCareReference()
        {
        }

        public esPCareReference(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String referenceID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(referenceID);
            else
                return LoadByPrimaryKeyStoredProcedure(referenceID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String referenceID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(referenceID);
            else
                return LoadByPrimaryKeyStoredProcedure(referenceID);
        }

        private bool LoadByPrimaryKeyDynamic(String referenceID)
        {
            esPCareReferenceQuery query = this.GetDynamicQuery();
            query.Where(query.ReferenceID == referenceID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String referenceID)
        {
            esParameters parms = new esParameters();
            parms.Add("ReferenceID", referenceID);
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
                        case "ReferenceID": this.str.ReferenceID = (string)value; break;
                        case "ReferenceName": this.str.ReferenceName = (string)value; break;
                        case "Url": this.str.Url = (string)value; break;
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
        /// Maps to PCareReference.ReferenceID
        /// </summary>
        virtual public System.String ReferenceID
        {
            get
            {
                return base.GetSystemString(PCareReferenceMetadata.ColumnNames.ReferenceID);
            }

            set
            {
                base.SetSystemString(PCareReferenceMetadata.ColumnNames.ReferenceID, value);
            }
        }
        /// <summary>
        /// Maps to PCareReference.ReferenceName
        /// </summary>
        virtual public System.String ReferenceName
        {
            get
            {
                return base.GetSystemString(PCareReferenceMetadata.ColumnNames.ReferenceName);
            }

            set
            {
                base.SetSystemString(PCareReferenceMetadata.ColumnNames.ReferenceName, value);
            }
        }
        /// <summary>
        /// Maps to PCareReference.Url
        /// </summary>
        virtual public System.String Url
        {
            get
            {
                return base.GetSystemString(PCareReferenceMetadata.ColumnNames.Url);
            }

            set
            {
                base.SetSystemString(PCareReferenceMetadata.ColumnNames.Url, value);
            }
        }
        /// <summary>
        /// Maps to PCareReference.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(PCareReferenceMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(PCareReferenceMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to PCareReference.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(PCareReferenceMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(PCareReferenceMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esPCareReference entity)
            {
                this.entity = entity;
            }
            public System.String ReferenceID
            {
                get
                {
                    System.String data = entity.ReferenceID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ReferenceID = null;
                    else entity.ReferenceID = Convert.ToString(value);
                }
            }
            public System.String ReferenceName
            {
                get
                {
                    System.String data = entity.ReferenceName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ReferenceName = null;
                    else entity.ReferenceName = Convert.ToString(value);
                }
            }
            public System.String Url
            {
                get
                {
                    System.String data = entity.Url;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Url = null;
                    else entity.Url = Convert.ToString(value);
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
            private esPCareReference entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esPCareReferenceQuery query)
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
                throw new Exception("esPCareReference can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class PCareReference : esPCareReference
    {
    }

    [Serializable]
    abstract public class esPCareReferenceQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return PCareReferenceMetadata.Meta();
            }
        }

        public esQueryItem ReferenceID
        {
            get
            {
                return new esQueryItem(this, PCareReferenceMetadata.ColumnNames.ReferenceID, esSystemType.String);
            }
        }

        public esQueryItem ReferenceName
        {
            get
            {
                return new esQueryItem(this, PCareReferenceMetadata.ColumnNames.ReferenceName, esSystemType.String);
            }
        }

        public esQueryItem Url
        {
            get
            {
                return new esQueryItem(this, PCareReferenceMetadata.ColumnNames.Url, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, PCareReferenceMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, PCareReferenceMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("PCareReferenceCollection")]
    public partial class PCareReferenceCollection : esPCareReferenceCollection, IEnumerable<PCareReference>
    {
        public PCareReferenceCollection()
        {

        }

        public static implicit operator List<PCareReference>(PCareReferenceCollection coll)
        {
            List<PCareReference> list = new List<PCareReference>();

            foreach (PCareReference emp in coll)
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
                return PCareReferenceMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PCareReferenceQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new PCareReference(row);
        }

        override protected esEntity CreateEntity()
        {
            return new PCareReference();
        }

        #endregion

        [BrowsableAttribute(false)]
        public PCareReferenceQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PCareReferenceQuery();
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
        public bool Load(PCareReferenceQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public PCareReference AddNew()
        {
            PCareReference entity = base.AddNewEntity() as PCareReference;

            return entity;
        }
        public PCareReference FindByPrimaryKey(String referenceID)
        {
            return base.FindByPrimaryKey(referenceID) as PCareReference;
        }

        #region IEnumerable< PCareReference> Members

        IEnumerator<PCareReference> IEnumerable<PCareReference>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as PCareReference;
            }
        }

        #endregion

        private PCareReferenceQuery query;
    }


    /// <summary>
    /// Encapsulates the 'PCareReference' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("PCareReference ({ReferenceID})")]
    [Serializable]
    public partial class PCareReference : esPCareReference
    {
        public PCareReference()
        {
        }

        public PCareReference(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return PCareReferenceMetadata.Meta();
            }
        }

        override protected esPCareReferenceQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PCareReferenceQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public PCareReferenceQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PCareReferenceQuery();
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
        public bool Load(PCareReferenceQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private PCareReferenceQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class PCareReferenceQuery : esPCareReferenceQuery
    {
        public PCareReferenceQuery()
        {

        }

        public PCareReferenceQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "PCareReferenceQuery";
        }
    }

    [Serializable]
    public partial class PCareReferenceMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected PCareReferenceMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(PCareReferenceMetadata.ColumnNames.ReferenceID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = PCareReferenceMetadata.PropertyNames.ReferenceID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 15;
            _columns.Add(c);

            c = new esColumnMetadata(PCareReferenceMetadata.ColumnNames.ReferenceName, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = PCareReferenceMetadata.PropertyNames.ReferenceName;
            c.CharacterMaxLength = 200;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PCareReferenceMetadata.ColumnNames.Url, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = PCareReferenceMetadata.PropertyNames.Url;
            c.CharacterMaxLength = 200;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PCareReferenceMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PCareReferenceMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PCareReferenceMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = PCareReferenceMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 40;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public PCareReferenceMetadata Meta()
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
            public const string ReferenceID = "ReferenceID";
            public const string ReferenceName = "ReferenceName";
            public const string Url = "Url";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string ReferenceID = "ReferenceID";
            public const string ReferenceName = "ReferenceName";
            public const string Url = "Url";
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
            lock (typeof(PCareReferenceMetadata))
            {
                if (PCareReferenceMetadata.mapDelegates == null)
                {
                    PCareReferenceMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (PCareReferenceMetadata.meta == null)
                {
                    PCareReferenceMetadata.meta = new PCareReferenceMetadata();
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

                meta.AddTypeMap("ReferenceID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ReferenceName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Url", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "PCareReference";
                meta.Destination = "PCareReference";
                meta.spInsert = "proc_PCareReferenceInsert";
                meta.spUpdate = "proc_PCareReferenceUpdate";
                meta.spDelete = "proc_PCareReferenceDelete";
                meta.spLoadAll = "proc_PCareReferenceLoadAll";
                meta.spLoadByPrimaryKey = "proc_PCareReferenceLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private PCareReferenceMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
