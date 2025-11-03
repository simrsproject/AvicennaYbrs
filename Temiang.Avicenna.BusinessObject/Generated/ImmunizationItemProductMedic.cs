/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 6/26/2016 3:56:53 AM
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
    abstract public class esImmunizationItemProductMedicCollection : esEntityCollectionWAuditLog
    {
        public esImmunizationItemProductMedicCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "ImmunizationItemProductMedicCollection";
        }

        #region Query Logic
        protected void InitQuery(esImmunizationItemProductMedicQuery query)
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
            this.InitQuery(query as esImmunizationItemProductMedicQuery);
        }
        #endregion

        virtual public ImmunizationItemProductMedic DetachEntity(ImmunizationItemProductMedic entity)
        {
            return base.DetachEntity(entity) as ImmunizationItemProductMedic;
        }

        virtual public ImmunizationItemProductMedic AttachEntity(ImmunizationItemProductMedic entity)
        {
            return base.AttachEntity(entity) as ImmunizationItemProductMedic;
        }

        virtual public void Combine(ImmunizationItemProductMedicCollection collection)
        {
            base.Combine(collection);
        }

        new public ImmunizationItemProductMedic this[int index]
        {
            get
            {
                return base[index] as ImmunizationItemProductMedic;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(ImmunizationItemProductMedic);
        }
    }

    [Serializable]
    abstract public class esImmunizationItemProductMedic : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esImmunizationItemProductMedicQuery GetDynamicQuery()
        {
            return null;
        }

        public esImmunizationItemProductMedic()
        {
        }

        public esImmunizationItemProductMedic(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String immunizationID, String itemID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(immunizationID, itemID);
            else
                return LoadByPrimaryKeyStoredProcedure(immunizationID, itemID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String immunizationID, String itemID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(immunizationID, itemID);
            else
                return LoadByPrimaryKeyStoredProcedure(immunizationID, itemID);
        }

        private bool LoadByPrimaryKeyDynamic(String immunizationID, String itemID)
        {
            esImmunizationItemProductMedicQuery query = this.GetDynamicQuery();
            query.Where(query.ImmunizationID == immunizationID, query.ItemID == itemID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String immunizationID, String itemID)
        {
            esParameters parms = new esParameters();
            parms.Add("ImmunizationID", immunizationID);
            parms.Add("ItemID", itemID);
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
                        case "ImmunizationID": this.str.ImmunizationID = (string)value; break;
                        case "ItemID": this.str.ItemID = (string)value; break;
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
        /// Maps to ImmunizationItemProductMedic.ImmunizationID
        /// </summary>
        virtual public System.String ImmunizationID
        {
            get
            {
                return base.GetSystemString(ImmunizationItemProductMedicMetadata.ColumnNames.ImmunizationID);
            }

            set
            {
                base.SetSystemString(ImmunizationItemProductMedicMetadata.ColumnNames.ImmunizationID, value);
            }
        }
        /// <summary>
        /// Maps to ImmunizationItemProductMedic.ItemID
        /// </summary>
        virtual public System.String ItemID
        {
            get
            {
                return base.GetSystemString(ImmunizationItemProductMedicMetadata.ColumnNames.ItemID);
            }

            set
            {
                base.SetSystemString(ImmunizationItemProductMedicMetadata.ColumnNames.ItemID, value);
            }
        }
        /// <summary>
        /// Maps to ImmunizationItemProductMedic.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(ImmunizationItemProductMedicMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(ImmunizationItemProductMedicMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to ImmunizationItemProductMedic.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(ImmunizationItemProductMedicMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(ImmunizationItemProductMedicMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esImmunizationItemProductMedic entity)
            {
                this.entity = entity;
            }
            public System.String ImmunizationID
            {
                get
                {
                    System.String data = entity.ImmunizationID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ImmunizationID = null;
                    else entity.ImmunizationID = Convert.ToString(value);
                }
            }
            public System.String ItemID
            {
                get
                {
                    System.String data = entity.ItemID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ItemID = null;
                    else entity.ItemID = Convert.ToString(value);
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
            private esImmunizationItemProductMedic entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esImmunizationItemProductMedicQuery query)
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
                throw new Exception("esImmunizationItemProductMedic can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class ImmunizationItemProductMedic : esImmunizationItemProductMedic
    {
    }

    [Serializable]
    abstract public class esImmunizationItemProductMedicQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return ImmunizationItemProductMedicMetadata.Meta();
            }
        }

        public esQueryItem ImmunizationID
        {
            get
            {
                return new esQueryItem(this, ImmunizationItemProductMedicMetadata.ColumnNames.ImmunizationID, esSystemType.String);
            }
        }

        public esQueryItem ItemID
        {
            get
            {
                return new esQueryItem(this, ImmunizationItemProductMedicMetadata.ColumnNames.ItemID, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, ImmunizationItemProductMedicMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, ImmunizationItemProductMedicMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("ImmunizationItemProductMedicCollection")]
    public partial class ImmunizationItemProductMedicCollection : esImmunizationItemProductMedicCollection, IEnumerable<ImmunizationItemProductMedic>
    {
        public ImmunizationItemProductMedicCollection()
        {

        }

        public static implicit operator List<ImmunizationItemProductMedic>(ImmunizationItemProductMedicCollection coll)
        {
            List<ImmunizationItemProductMedic> list = new List<ImmunizationItemProductMedic>();

            foreach (ImmunizationItemProductMedic emp in coll)
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
                return ImmunizationItemProductMedicMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ImmunizationItemProductMedicQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new ImmunizationItemProductMedic(row);
        }

        override protected esEntity CreateEntity()
        {
            return new ImmunizationItemProductMedic();
        }

        #endregion

        [BrowsableAttribute(false)]
        public ImmunizationItemProductMedicQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ImmunizationItemProductMedicQuery();
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
        public bool Load(ImmunizationItemProductMedicQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public ImmunizationItemProductMedic AddNew()
        {
            ImmunizationItemProductMedic entity = base.AddNewEntity() as ImmunizationItemProductMedic;

            return entity;
        }
        public ImmunizationItemProductMedic FindByPrimaryKey(String immunizationID, String itemID)
        {
            return base.FindByPrimaryKey(immunizationID, itemID) as ImmunizationItemProductMedic;
        }

        #region IEnumerable< ImmunizationItemProductMedic> Members

        IEnumerator<ImmunizationItemProductMedic> IEnumerable<ImmunizationItemProductMedic>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as ImmunizationItemProductMedic;
            }
        }

        #endregion

        private ImmunizationItemProductMedicQuery query;
    }


    /// <summary>
    /// Encapsulates the 'ImmunizationItemProductMedic' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("ImmunizationItemProductMedic ({ImmunizationID, ItemID})")]
    [Serializable]
    public partial class ImmunizationItemProductMedic : esImmunizationItemProductMedic
    {
        public ImmunizationItemProductMedic()
        {
        }

        public ImmunizationItemProductMedic(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return ImmunizationItemProductMedicMetadata.Meta();
            }
        }

        override protected esImmunizationItemProductMedicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ImmunizationItemProductMedicQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public ImmunizationItemProductMedicQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ImmunizationItemProductMedicQuery();
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
        public bool Load(ImmunizationItemProductMedicQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private ImmunizationItemProductMedicQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class ImmunizationItemProductMedicQuery : esImmunizationItemProductMedicQuery
    {
        public ImmunizationItemProductMedicQuery()
        {

        }

        public ImmunizationItemProductMedicQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "ImmunizationItemProductMedicQuery";
        }
    }

    [Serializable]
    public partial class ImmunizationItemProductMedicMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected ImmunizationItemProductMedicMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(ImmunizationItemProductMedicMetadata.ColumnNames.ImmunizationID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = ImmunizationItemProductMedicMetadata.PropertyNames.ImmunizationID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 3;
            _columns.Add(c);

            c = new esColumnMetadata(ImmunizationItemProductMedicMetadata.ColumnNames.ItemID, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = ImmunizationItemProductMedicMetadata.PropertyNames.ItemID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(ImmunizationItemProductMedicMetadata.ColumnNames.LastUpdateDateTime, 2, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ImmunizationItemProductMedicMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ImmunizationItemProductMedicMetadata.ColumnNames.LastUpdateByUserID, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = ImmunizationItemProductMedicMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public ImmunizationItemProductMedicMetadata Meta()
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
            public const string ImmunizationID = "ImmunizationID";
            public const string ItemID = "ItemID";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string ImmunizationID = "ImmunizationID";
            public const string ItemID = "ItemID";
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
            lock (typeof(ImmunizationItemProductMedicMetadata))
            {
                if (ImmunizationItemProductMedicMetadata.mapDelegates == null)
                {
                    ImmunizationItemProductMedicMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (ImmunizationItemProductMedicMetadata.meta == null)
                {
                    ImmunizationItemProductMedicMetadata.meta = new ImmunizationItemProductMedicMetadata();
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

                meta.AddTypeMap("ImmunizationID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "ImmunizationItemProductMedic";
                meta.Destination = "ImmunizationItemProductMedic";
                meta.spInsert = "proc_ImmunizationItemProductMedicInsert";
                meta.spUpdate = "proc_ImmunizationItemProductMedicUpdate";
                meta.spDelete = "proc_ImmunizationItemProductMedicDelete";
                meta.spLoadAll = "proc_ImmunizationItemProductMedicLoadAll";
                meta.spLoadByPrimaryKey = "proc_ImmunizationItemProductMedicLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private ImmunizationItemProductMedicMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
