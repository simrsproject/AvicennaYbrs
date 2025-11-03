/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 6/20/2017 10:26:34 AM
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
    abstract public class esPathologyAnatomyImpressionGroupItemCollection : esEntityCollectionWAuditLog
    {
        public esPathologyAnatomyImpressionGroupItemCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "PathologyAnatomyImpressionGroupItemCollection";
        }

        #region Query Logic
        protected void InitQuery(esPathologyAnatomyImpressionGroupItemQuery query)
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
            this.InitQuery(query as esPathologyAnatomyImpressionGroupItemQuery);
        }
        #endregion

        virtual public PathologyAnatomyImpressionGroupItem DetachEntity(PathologyAnatomyImpressionGroupItem entity)
        {
            return base.DetachEntity(entity) as PathologyAnatomyImpressionGroupItem;
        }

        virtual public PathologyAnatomyImpressionGroupItem AttachEntity(PathologyAnatomyImpressionGroupItem entity)
        {
            return base.AttachEntity(entity) as PathologyAnatomyImpressionGroupItem;
        }

        virtual public void Combine(PathologyAnatomyImpressionGroupItemCollection collection)
        {
            base.Combine(collection);
        }

        new public PathologyAnatomyImpressionGroupItem this[int index]
        {
            get
            {
                return base[index] as PathologyAnatomyImpressionGroupItem;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(PathologyAnatomyImpressionGroupItem);
        }
    }

    [Serializable]
    abstract public class esPathologyAnatomyImpressionGroupItem : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esPathologyAnatomyImpressionGroupItemQuery GetDynamicQuery()
        {
            return null;
        }

        public esPathologyAnatomyImpressionGroupItem()
        {
        }

        public esPathologyAnatomyImpressionGroupItem(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String groupID, String itemID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(groupID, itemID);
            else
                return LoadByPrimaryKeyStoredProcedure(groupID, itemID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String groupID, String itemID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(groupID, itemID);
            else
                return LoadByPrimaryKeyStoredProcedure(groupID, itemID);
        }

        private bool LoadByPrimaryKeyDynamic(String groupID, String itemID)
        {
            esPathologyAnatomyImpressionGroupItemQuery query = this.GetDynamicQuery();
            query.Where(query.GroupID == groupID, query.ItemID == itemID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String groupID, String itemID)
        {
            esParameters parms = new esParameters();
            parms.Add("GroupID", groupID);
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
                        case "GroupID": this.str.GroupID = (string)value; break;
                        case "ItemID": this.str.ItemID = (string)value; break;
                        case "ItemName": this.str.ItemName = (string)value; break;
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
        /// Maps to PathologyAnatomyImpressionGroupItem.GroupID
        /// </summary>
        virtual public System.String GroupID
        {
            get
            {
                return base.GetSystemString(PathologyAnatomyImpressionGroupItemMetadata.ColumnNames.GroupID);
            }

            set
            {
                base.SetSystemString(PathologyAnatomyImpressionGroupItemMetadata.ColumnNames.GroupID, value);
            }
        }
        /// <summary>
        /// Maps to PathologyAnatomyImpressionGroupItem.ItemID
        /// </summary>
        virtual public System.String ItemID
        {
            get
            {
                return base.GetSystemString(PathologyAnatomyImpressionGroupItemMetadata.ColumnNames.ItemID);
            }

            set
            {
                base.SetSystemString(PathologyAnatomyImpressionGroupItemMetadata.ColumnNames.ItemID, value);
            }
        }
        /// <summary>
        /// Maps to PathologyAnatomyImpressionGroupItem.ItemName
        /// </summary>
        virtual public System.String ItemName
        {
            get
            {
                return base.GetSystemString(PathologyAnatomyImpressionGroupItemMetadata.ColumnNames.ItemName);
            }

            set
            {
                base.SetSystemString(PathologyAnatomyImpressionGroupItemMetadata.ColumnNames.ItemName, value);
            }
        }
        /// <summary>
        /// Maps to PathologyAnatomyImpressionGroupItem.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(PathologyAnatomyImpressionGroupItemMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(PathologyAnatomyImpressionGroupItemMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to PathologyAnatomyImpressionGroupItem.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(PathologyAnatomyImpressionGroupItemMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(PathologyAnatomyImpressionGroupItemMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esPathologyAnatomyImpressionGroupItem entity)
            {
                this.entity = entity;
            }
            public System.String GroupID
            {
                get
                {
                    System.String data = entity.GroupID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.GroupID = null;
                    else entity.GroupID = Convert.ToString(value);
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
            private esPathologyAnatomyImpressionGroupItem entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esPathologyAnatomyImpressionGroupItemQuery query)
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
                throw new Exception("esPathologyAnatomyImpressionGroupItem can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class PathologyAnatomyImpressionGroupItem : esPathologyAnatomyImpressionGroupItem
    {
    }

    [Serializable]
    abstract public class esPathologyAnatomyImpressionGroupItemQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return PathologyAnatomyImpressionGroupItemMetadata.Meta();
            }
        }

        public esQueryItem GroupID
        {
            get
            {
                return new esQueryItem(this, PathologyAnatomyImpressionGroupItemMetadata.ColumnNames.GroupID, esSystemType.String);
            }
        }

        public esQueryItem ItemID
        {
            get
            {
                return new esQueryItem(this, PathologyAnatomyImpressionGroupItemMetadata.ColumnNames.ItemID, esSystemType.String);
            }
        }

        public esQueryItem ItemName
        {
            get
            {
                return new esQueryItem(this, PathologyAnatomyImpressionGroupItemMetadata.ColumnNames.ItemName, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, PathologyAnatomyImpressionGroupItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, PathologyAnatomyImpressionGroupItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("PathologyAnatomyImpressionGroupItemCollection")]
    public partial class PathologyAnatomyImpressionGroupItemCollection : esPathologyAnatomyImpressionGroupItemCollection, IEnumerable<PathologyAnatomyImpressionGroupItem>
    {
        public PathologyAnatomyImpressionGroupItemCollection()
        {

        }

        public static implicit operator List<PathologyAnatomyImpressionGroupItem>(PathologyAnatomyImpressionGroupItemCollection coll)
        {
            List<PathologyAnatomyImpressionGroupItem> list = new List<PathologyAnatomyImpressionGroupItem>();

            foreach (PathologyAnatomyImpressionGroupItem emp in coll)
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
                return PathologyAnatomyImpressionGroupItemMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PathologyAnatomyImpressionGroupItemQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new PathologyAnatomyImpressionGroupItem(row);
        }

        override protected esEntity CreateEntity()
        {
            return new PathologyAnatomyImpressionGroupItem();
        }

        #endregion

        [BrowsableAttribute(false)]
        public PathologyAnatomyImpressionGroupItemQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PathologyAnatomyImpressionGroupItemQuery();
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
        public bool Load(PathologyAnatomyImpressionGroupItemQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public PathologyAnatomyImpressionGroupItem AddNew()
        {
            PathologyAnatomyImpressionGroupItem entity = base.AddNewEntity() as PathologyAnatomyImpressionGroupItem;

            return entity;
        }
        public PathologyAnatomyImpressionGroupItem FindByPrimaryKey(String groupID, String itemID)
        {
            return base.FindByPrimaryKey(groupID, itemID) as PathologyAnatomyImpressionGroupItem;
        }

        #region IEnumerable< PathologyAnatomyImpressionGroupItem> Members

        IEnumerator<PathologyAnatomyImpressionGroupItem> IEnumerable<PathologyAnatomyImpressionGroupItem>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as PathologyAnatomyImpressionGroupItem;
            }
        }

        #endregion

        private PathologyAnatomyImpressionGroupItemQuery query;
    }


    /// <summary>
    /// Encapsulates the 'PathologyAnatomyImpressionGroupItem' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("PathologyAnatomyImpressionGroupItem ({GroupID, ItemID})")]
    [Serializable]
    public partial class PathologyAnatomyImpressionGroupItem : esPathologyAnatomyImpressionGroupItem
    {
        public PathologyAnatomyImpressionGroupItem()
        {
        }

        public PathologyAnatomyImpressionGroupItem(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return PathologyAnatomyImpressionGroupItemMetadata.Meta();
            }
        }

        override protected esPathologyAnatomyImpressionGroupItemQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PathologyAnatomyImpressionGroupItemQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public PathologyAnatomyImpressionGroupItemQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PathologyAnatomyImpressionGroupItemQuery();
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
        public bool Load(PathologyAnatomyImpressionGroupItemQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private PathologyAnatomyImpressionGroupItemQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class PathologyAnatomyImpressionGroupItemQuery : esPathologyAnatomyImpressionGroupItemQuery
    {
        public PathologyAnatomyImpressionGroupItemQuery()
        {

        }

        public PathologyAnatomyImpressionGroupItemQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "PathologyAnatomyImpressionGroupItemQuery";
        }
    }

    [Serializable]
    public partial class PathologyAnatomyImpressionGroupItemMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected PathologyAnatomyImpressionGroupItemMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(PathologyAnatomyImpressionGroupItemMetadata.ColumnNames.GroupID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = PathologyAnatomyImpressionGroupItemMetadata.PropertyNames.GroupID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(PathologyAnatomyImpressionGroupItemMetadata.ColumnNames.ItemID, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = PathologyAnatomyImpressionGroupItemMetadata.PropertyNames.ItemID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(PathologyAnatomyImpressionGroupItemMetadata.ColumnNames.ItemName, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = PathologyAnatomyImpressionGroupItemMetadata.PropertyNames.ItemName;
            c.CharacterMaxLength = 250;
            _columns.Add(c);

            c = new esColumnMetadata(PathologyAnatomyImpressionGroupItemMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PathologyAnatomyImpressionGroupItemMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PathologyAnatomyImpressionGroupItemMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = PathologyAnatomyImpressionGroupItemMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public PathologyAnatomyImpressionGroupItemMetadata Meta()
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
            public const string GroupID = "GroupID";
            public const string ItemID = "ItemID";
            public const string ItemName = "ItemName";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string GroupID = "GroupID";
            public const string ItemID = "ItemID";
            public const string ItemName = "ItemName";
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
            lock (typeof(PathologyAnatomyImpressionGroupItemMetadata))
            {
                if (PathologyAnatomyImpressionGroupItemMetadata.mapDelegates == null)
                {
                    PathologyAnatomyImpressionGroupItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (PathologyAnatomyImpressionGroupItemMetadata.meta == null)
                {
                    PathologyAnatomyImpressionGroupItemMetadata.meta = new PathologyAnatomyImpressionGroupItemMetadata();
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

                meta.AddTypeMap("GroupID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ItemName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "PathologyAnatomyImpressionGroupItem";
                meta.Destination = "PathologyAnatomyImpressionGroupItem";
                meta.spInsert = "proc_PathologyAnatomyImpressionGroupItemInsert";
                meta.spUpdate = "proc_PathologyAnatomyImpressionGroupItemUpdate";
                meta.spDelete = "proc_PathologyAnatomyImpressionGroupItemDelete";
                meta.spLoadAll = "proc_PathologyAnatomyImpressionGroupItemLoadAll";
                meta.spLoadByPrimaryKey = "proc_PathologyAnatomyImpressionGroupItemLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private PathologyAnatomyImpressionGroupItemMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
