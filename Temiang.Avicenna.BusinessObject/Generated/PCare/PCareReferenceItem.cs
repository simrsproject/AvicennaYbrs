/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 3/22/2016 8:24:09 AM
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
    abstract public class esPCareReferenceItemCollection : esEntityCollectionWAuditLog
    {
        public esPCareReferenceItemCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "PCareReferenceItemCollection";
        }

        #region Query Logic
        protected void InitQuery(esPCareReferenceItemQuery query)
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
            this.InitQuery(query as esPCareReferenceItemQuery);
        }
        #endregion

        virtual public PCareReferenceItem DetachEntity(PCareReferenceItem entity)
        {
            return base.DetachEntity(entity) as PCareReferenceItem;
        }

        virtual public PCareReferenceItem AttachEntity(PCareReferenceItem entity)
        {
            return base.AttachEntity(entity) as PCareReferenceItem;
        }

        virtual public void Combine(PCareReferenceItemCollection collection)
        {
            base.Combine(collection);
        }

        new public PCareReferenceItem this[int index]
        {
            get
            {
                return base[index] as PCareReferenceItem;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(PCareReferenceItem);
        }
    }

    [Serializable]
    abstract public class esPCareReferenceItem : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esPCareReferenceItemQuery GetDynamicQuery()
        {
            return null;
        }

        public esPCareReferenceItem()
        {
        }

        public esPCareReferenceItem(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String referenceID, String itemID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(referenceID, itemID);
            else
                return LoadByPrimaryKeyStoredProcedure(referenceID, itemID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String referenceID, String itemID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(referenceID, itemID);
            else
                return LoadByPrimaryKeyStoredProcedure(referenceID, itemID);
        }

        private bool LoadByPrimaryKeyDynamic(String referenceID, String itemID)
        {
            esPCareReferenceItemQuery query = this.GetDynamicQuery();
            query.Where(query.ReferenceID == referenceID, query.ItemID == itemID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String referenceID, String itemID)
        {
            esParameters parms = new esParameters();
            parms.Add("ReferenceID", referenceID);
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
                        case "ReferenceID": this.str.ReferenceID = (string)value; break;
                        case "ItemID": this.str.ItemID = (string)value; break;
                        case "ItemName": this.str.ItemName = (string)value; break;
                        case "ResponseData": this.str.ResponseData = (string)value; break;
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
        /// Maps to PCareReferenceItem.ReferenceID
        /// </summary>
        virtual public System.String ReferenceID
        {
            get
            {
                return base.GetSystemString(PCareReferenceItemMetadata.ColumnNames.ReferenceID);
            }

            set
            {
                base.SetSystemString(PCareReferenceItemMetadata.ColumnNames.ReferenceID, value);
            }
        }
        /// <summary>
        /// Maps to PCareReferenceItem.ItemID
        /// </summary>
        virtual public System.String ItemID
        {
            get
            {
                return base.GetSystemString(PCareReferenceItemMetadata.ColumnNames.ItemID);
            }

            set
            {
                base.SetSystemString(PCareReferenceItemMetadata.ColumnNames.ItemID, value);
            }
        }
        /// <summary>
        /// Maps to PCareReferenceItem.ItemName
        /// </summary>
        virtual public System.String ItemName
        {
            get
            {
                return base.GetSystemString(PCareReferenceItemMetadata.ColumnNames.ItemName);
            }

            set
            {
                base.SetSystemString(PCareReferenceItemMetadata.ColumnNames.ItemName, value);
            }
        }
        /// <summary>
        /// Maps to PCareReferenceItem.ResponseData
        /// </summary>
        virtual public System.String ResponseData
        {
            get
            {
                return base.GetSystemString(PCareReferenceItemMetadata.ColumnNames.ResponseData);
            }

            set
            {
                base.SetSystemString(PCareReferenceItemMetadata.ColumnNames.ResponseData, value);
            }
        }
        /// <summary>
        /// Maps to PCareReferenceItem.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(PCareReferenceItemMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(PCareReferenceItemMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to PCareReferenceItem.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(PCareReferenceItemMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(PCareReferenceItemMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esPCareReferenceItem entity)
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
            public System.String ItemName
            {
                get
                {
                    System.String data = entity.ItemName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ItemName = null;
                    else entity.ItemName = Convert.ToString(value);
                }
            }
            public System.String ResponseData
            {
                get
                {
                    System.String data = entity.ResponseData;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ResponseData = null;
                    else entity.ResponseData = Convert.ToString(value);
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
            private esPCareReferenceItem entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esPCareReferenceItemQuery query)
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
                throw new Exception("esPCareReferenceItem can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class PCareReferenceItem : esPCareReferenceItem
    {
    }

    [Serializable]
    abstract public class esPCareReferenceItemQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return PCareReferenceItemMetadata.Meta();
            }
        }

        public esQueryItem ReferenceID
        {
            get
            {
                return new esQueryItem(this, PCareReferenceItemMetadata.ColumnNames.ReferenceID, esSystemType.String);
            }
        }

        public esQueryItem ItemID
        {
            get
            {
                return new esQueryItem(this, PCareReferenceItemMetadata.ColumnNames.ItemID, esSystemType.String);
            }
        }

        public esQueryItem ItemName
        {
            get
            {
                return new esQueryItem(this, PCareReferenceItemMetadata.ColumnNames.ItemName, esSystemType.String);
            }
        }

        public esQueryItem ResponseData
        {
            get
            {
                return new esQueryItem(this, PCareReferenceItemMetadata.ColumnNames.ResponseData, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, PCareReferenceItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, PCareReferenceItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("PCareReferenceItemCollection")]
    public partial class PCareReferenceItemCollection : esPCareReferenceItemCollection, IEnumerable<PCareReferenceItem>
    {
        public PCareReferenceItemCollection()
        {

        }

        public static implicit operator List<PCareReferenceItem>(PCareReferenceItemCollection coll)
        {
            List<PCareReferenceItem> list = new List<PCareReferenceItem>();

            foreach (PCareReferenceItem emp in coll)
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
                return PCareReferenceItemMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PCareReferenceItemQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new PCareReferenceItem(row);
        }

        override protected esEntity CreateEntity()
        {
            return new PCareReferenceItem();
        }

        #endregion

        [BrowsableAttribute(false)]
        public PCareReferenceItemQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PCareReferenceItemQuery();
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
        public bool Load(PCareReferenceItemQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public PCareReferenceItem AddNew()
        {
            PCareReferenceItem entity = base.AddNewEntity() as PCareReferenceItem;

            return entity;
        }
        public PCareReferenceItem FindByPrimaryKey(String referenceID, String itemID)
        {
            return base.FindByPrimaryKey(referenceID, itemID) as PCareReferenceItem;
        }

        #region IEnumerable< PCareReferenceItem> Members

        IEnumerator<PCareReferenceItem> IEnumerable<PCareReferenceItem>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as PCareReferenceItem;
            }
        }

        #endregion

        private PCareReferenceItemQuery query;
    }


    /// <summary>
    /// Encapsulates the 'PCareReferenceItem' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("PCareReferenceItem ({ReferenceID, ItemID})")]
    [Serializable]
    public partial class PCareReferenceItem : esPCareReferenceItem
    {
        public PCareReferenceItem()
        {
        }

        public PCareReferenceItem(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return PCareReferenceItemMetadata.Meta();
            }
        }

        override protected esPCareReferenceItemQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PCareReferenceItemQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public PCareReferenceItemQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PCareReferenceItemQuery();
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
        public bool Load(PCareReferenceItemQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private PCareReferenceItemQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class PCareReferenceItemQuery : esPCareReferenceItemQuery
    {
        public PCareReferenceItemQuery()
        {

        }

        public PCareReferenceItemQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "PCareReferenceItemQuery";
        }
    }

    [Serializable]
    public partial class PCareReferenceItemMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected PCareReferenceItemMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(PCareReferenceItemMetadata.ColumnNames.ReferenceID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = PCareReferenceItemMetadata.PropertyNames.ReferenceID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 15;
            _columns.Add(c);

            c = new esColumnMetadata(PCareReferenceItemMetadata.ColumnNames.ItemID, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = PCareReferenceItemMetadata.PropertyNames.ItemID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(PCareReferenceItemMetadata.ColumnNames.ItemName, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = PCareReferenceItemMetadata.PropertyNames.ItemName;
            c.CharacterMaxLength = 200;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PCareReferenceItemMetadata.ColumnNames.ResponseData, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = PCareReferenceItemMetadata.PropertyNames.ResponseData;
            c.CharacterMaxLength = -1;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PCareReferenceItemMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PCareReferenceItemMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PCareReferenceItemMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = PCareReferenceItemMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 40;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public PCareReferenceItemMetadata Meta()
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
            public const string ItemID = "ItemID";
            public const string ItemName = "ItemName";
            public const string ResponseData = "ResponseData";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string ReferenceID = "ReferenceID";
            public const string ItemID = "ItemID";
            public const string ItemName = "ItemName";
            public const string ResponseData = "ResponseData";
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
            lock (typeof(PCareReferenceItemMetadata))
            {
                if (PCareReferenceItemMetadata.mapDelegates == null)
                {
                    PCareReferenceItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (PCareReferenceItemMetadata.meta == null)
                {
                    PCareReferenceItemMetadata.meta = new PCareReferenceItemMetadata();
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
                meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ItemName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ResponseData", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "PCareReferenceItem";
                meta.Destination = "PCareReferenceItem";
                meta.spInsert = "proc_PCareReferenceItemInsert";
                meta.spUpdate = "proc_PCareReferenceItemUpdate";
                meta.spDelete = "proc_PCareReferenceItemDelete";
                meta.spLoadAll = "proc_PCareReferenceItemLoadAll";
                meta.spLoadByPrimaryKey = "proc_PCareReferenceItemLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private PCareReferenceItemMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
