/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 5/15/2019 9:35:05 AM
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
    abstract public class esDietMenuCollection : esEntityCollectionWAuditLog
    {
        public esDietMenuCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "DietMenuCollection";
        }

        #region Query Logic
        protected void InitQuery(esDietMenuQuery query)
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
            this.InitQuery(query as esDietMenuQuery);
        }
        #endregion

        virtual public DietMenu DetachEntity(DietMenu entity)
        {
            return base.DetachEntity(entity) as DietMenu;
        }

        virtual public DietMenu AttachEntity(DietMenu entity)
        {
            return base.AttachEntity(entity) as DietMenu;
        }

        virtual public void Combine(DietMenuCollection collection)
        {
            base.Combine(collection);
        }

        new public DietMenu this[int index]
        {
            get
            {
                return base[index] as DietMenu;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(DietMenu);
        }
    }

    [Serializable]
    abstract public class esDietMenu : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esDietMenuQuery GetDynamicQuery()
        {
            return null;
        }

        public esDietMenu()
        {
        }

        public esDietMenu(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String dietID, String formOfFood)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(dietID, formOfFood);
            else
                return LoadByPrimaryKeyStoredProcedure(dietID, formOfFood);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String dietID, String formOfFood)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(dietID, formOfFood);
            else
                return LoadByPrimaryKeyStoredProcedure(dietID, formOfFood);
        }

        private bool LoadByPrimaryKeyDynamic(String dietID, String formOfFood)
        {
            esDietMenuQuery query = this.GetDynamicQuery();
            query.Where(query.DietID == dietID, query.FormOfFood == formOfFood);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String dietID, String formOfFood)
        {
            esParameters parms = new esParameters();
            parms.Add("DietID", dietID);
            parms.Add("FormOfFood", formOfFood);
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
                        case "DietID": this.str.DietID = (string)value; break;
                        case "FormOfFood": this.str.FormOfFood = (string)value; break;
                        case "MenuID": this.str.MenuID = (string)value; break;
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
        /// Maps to DietMenu.DietID
        /// </summary>
        virtual public System.String DietID
        {
            get
            {
                return base.GetSystemString(DietMenuMetadata.ColumnNames.DietID);
            }

            set
            {
                base.SetSystemString(DietMenuMetadata.ColumnNames.DietID, value);
            }
        }
        /// <summary>
        /// Maps to DietMenu.FormOfFood
        /// </summary>
        virtual public System.String FormOfFood
        {
            get
            {
                return base.GetSystemString(DietMenuMetadata.ColumnNames.FormOfFood);
            }

            set
            {
                base.SetSystemString(DietMenuMetadata.ColumnNames.FormOfFood, value);
            }
        }
        /// <summary>
        /// Maps to DietMenu.MenuID
        /// </summary>
        virtual public System.String MenuID
        {
            get
            {
                return base.GetSystemString(DietMenuMetadata.ColumnNames.MenuID);
            }

            set
            {
                base.SetSystemString(DietMenuMetadata.ColumnNames.MenuID, value);
            }
        }
        /// <summary>
        /// Maps to DietMenu.IsActive
        /// </summary>
        virtual public System.Boolean? IsActive
        {
            get
            {
                return base.GetSystemBoolean(DietMenuMetadata.ColumnNames.IsActive);
            }

            set
            {
                base.SetSystemBoolean(DietMenuMetadata.ColumnNames.IsActive, value);
            }
        }
        /// <summary>
        /// Maps to DietMenu.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(DietMenuMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(DietMenuMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to DietMenu.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(DietMenuMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(DietMenuMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esDietMenu entity)
            {
                this.entity = entity;
            }
            public System.String DietID
            {
                get
                {
                    System.String data = entity.DietID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DietID = null;
                    else entity.DietID = Convert.ToString(value);
                }
            }
            public System.String FormOfFood
            {
                get
                {
                    System.String data = entity.FormOfFood;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FormOfFood = null;
                    else entity.FormOfFood = Convert.ToString(value);
                }
            }
            public System.String MenuID
            {
                get
                {
                    System.String data = entity.MenuID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.MenuID = null;
                    else entity.MenuID = Convert.ToString(value);
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
            private esDietMenu entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esDietMenuQuery query)
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
                throw new Exception("esDietMenu can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class DietMenu : esDietMenu
    {
    }

    [Serializable]
    abstract public class esDietMenuQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return DietMenuMetadata.Meta();
            }
        }

        public esQueryItem DietID
        {
            get
            {
                return new esQueryItem(this, DietMenuMetadata.ColumnNames.DietID, esSystemType.String);
            }
        }

        public esQueryItem FormOfFood
        {
            get
            {
                return new esQueryItem(this, DietMenuMetadata.ColumnNames.FormOfFood, esSystemType.String);
            }
        }

        public esQueryItem MenuID
        {
            get
            {
                return new esQueryItem(this, DietMenuMetadata.ColumnNames.MenuID, esSystemType.String);
            }
        }

        public esQueryItem IsActive
        {
            get
            {
                return new esQueryItem(this, DietMenuMetadata.ColumnNames.IsActive, esSystemType.Boolean);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, DietMenuMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, DietMenuMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("DietMenuCollection")]
    public partial class DietMenuCollection : esDietMenuCollection, IEnumerable<DietMenu>
    {
        public DietMenuCollection()
        {

        }

        public static implicit operator List<DietMenu>(DietMenuCollection coll)
        {
            List<DietMenu> list = new List<DietMenu>();

            foreach (DietMenu emp in coll)
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
                return DietMenuMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new DietMenuQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new DietMenu(row);
        }

        override protected esEntity CreateEntity()
        {
            return new DietMenu();
        }

        #endregion

        [BrowsableAttribute(false)]
        public DietMenuQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new DietMenuQuery();
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
        public bool Load(DietMenuQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public DietMenu AddNew()
        {
            DietMenu entity = base.AddNewEntity() as DietMenu;

            return entity;
        }
        public DietMenu FindByPrimaryKey(String dietID, String formOfFood)
        {
            return base.FindByPrimaryKey(dietID, formOfFood) as DietMenu;
        }

        #region IEnumerable< DietMenu> Members

        IEnumerator<DietMenu> IEnumerable<DietMenu>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as DietMenu;
            }
        }

        #endregion

        private DietMenuQuery query;
    }


    /// <summary>
    /// Encapsulates the 'DietMenu' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("DietMenu ({DietID, FormOfFood})")]
    [Serializable]
    public partial class DietMenu : esDietMenu
    {
        public DietMenu()
        {
        }

        public DietMenu(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return DietMenuMetadata.Meta();
            }
        }

        override protected esDietMenuQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new DietMenuQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public DietMenuQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new DietMenuQuery();
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
        public bool Load(DietMenuQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private DietMenuQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class DietMenuQuery : esDietMenuQuery
    {
        public DietMenuQuery()
        {

        }

        public DietMenuQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "DietMenuQuery";
        }
    }

    [Serializable]
    public partial class DietMenuMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected DietMenuMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(DietMenuMetadata.ColumnNames.DietID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = DietMenuMetadata.PropertyNames.DietID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(DietMenuMetadata.ColumnNames.FormOfFood, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = DietMenuMetadata.PropertyNames.FormOfFood;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 2;
            _columns.Add(c);

            c = new esColumnMetadata(DietMenuMetadata.ColumnNames.MenuID, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = DietMenuMetadata.PropertyNames.MenuID;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(DietMenuMetadata.ColumnNames.IsActive, 3, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = DietMenuMetadata.PropertyNames.IsActive;
            _columns.Add(c);

            c = new esColumnMetadata(DietMenuMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = DietMenuMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(DietMenuMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = DietMenuMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public DietMenuMetadata Meta()
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
            public const string DietID = "DietID";
            public const string FormOfFood = "FormOfFood";
            public const string MenuID = "MenuID";
            public const string IsActive = "IsActive";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string DietID = "DietID";
            public const string FormOfFood = "FormOfFood";
            public const string MenuID = "MenuID";
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
            lock (typeof(DietMenuMetadata))
            {
                if (DietMenuMetadata.mapDelegates == null)
                {
                    DietMenuMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (DietMenuMetadata.meta == null)
                {
                    DietMenuMetadata.meta = new DietMenuMetadata();
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

                meta.AddTypeMap("DietID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("FormOfFood", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("MenuID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "DietMenu";
                meta.Destination = "DietMenu";
                meta.spInsert = "proc_DietMenuInsert";
                meta.spUpdate = "proc_DietMenuUpdate";
                meta.spDelete = "proc_DietMenuDelete";
                meta.spLoadAll = "proc_DietMenuLoadAll";
                meta.spLoadByPrimaryKey = "proc_DietMenuLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private DietMenuMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
