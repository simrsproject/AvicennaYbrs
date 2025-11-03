/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 10/19/2017 11:20:51 AM
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
    abstract public class esAssetSubGroupCollection : esEntityCollectionWAuditLog
    {
        public esAssetSubGroupCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "AssetSubGroupCollection";
        }

        #region Query Logic
        protected void InitQuery(esAssetSubGroupQuery query)
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
            this.InitQuery(query as esAssetSubGroupQuery);
        }
        #endregion

        virtual public AssetSubGroup DetachEntity(AssetSubGroup entity)
        {
            return base.DetachEntity(entity) as AssetSubGroup;
        }

        virtual public AssetSubGroup AttachEntity(AssetSubGroup entity)
        {
            return base.AttachEntity(entity) as AssetSubGroup;
        }

        virtual public void Combine(AssetSubGroupCollection collection)
        {
            base.Combine(collection);
        }

        new public AssetSubGroup this[int index]
        {
            get
            {
                return base[index] as AssetSubGroup;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(AssetSubGroup);
        }
    }

    [Serializable]
    abstract public class esAssetSubGroup : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esAssetSubGroupQuery GetDynamicQuery()
        {
            return null;
        }

        public esAssetSubGroup()
        {
        }

        public esAssetSubGroup(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String assetGroupId, String assetSubGroupId)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(assetGroupId, assetSubGroupId);
            else
                return LoadByPrimaryKeyStoredProcedure(assetGroupId, assetSubGroupId);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String assetGroupId, String assetSubGroupId)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(assetGroupId, assetSubGroupId);
            else
                return LoadByPrimaryKeyStoredProcedure(assetGroupId, assetSubGroupId);
        }

        private bool LoadByPrimaryKeyDynamic(String assetGroupId, String assetSubGroupId)
        {
            esAssetSubGroupQuery query = this.GetDynamicQuery();
            query.Where(query.AssetGroupId == assetGroupId, query.AssetSubGroupId == assetSubGroupId);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String assetGroupId, String assetSubGroupId)
        {
            esParameters parms = new esParameters();
            parms.Add("AssetGroupId", assetGroupId);
            parms.Add("AssetSubGroupId", assetSubGroupId);
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
                        case "AssetGroupId": this.str.AssetGroupId = (string)value; break;
                        case "AssetSubGroupId": this.str.AssetSubGroupId = (string)value; break;
                        case "AssetSubGroupName": this.str.AssetSubGroupName = (string)value; break;
                        case "Initial": this.str.Initial = (string)value; break;
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
        /// Maps to AssetSubGroup.AssetGroupId
        /// </summary>
        virtual public System.String AssetGroupId
        {
            get
            {
                return base.GetSystemString(AssetSubGroupMetadata.ColumnNames.AssetGroupId);
            }

            set
            {
                base.SetSystemString(AssetSubGroupMetadata.ColumnNames.AssetGroupId, value);
            }
        }
        /// <summary>
        /// Maps to AssetSubGroup.AssetSubGroupId
        /// </summary>
        virtual public System.String AssetSubGroupId
        {
            get
            {
                return base.GetSystemString(AssetSubGroupMetadata.ColumnNames.AssetSubGroupId);
            }

            set
            {
                base.SetSystemString(AssetSubGroupMetadata.ColumnNames.AssetSubGroupId, value);
            }
        }
        /// <summary>
        /// Maps to AssetSubGroup.AssetSubGroupName
        /// </summary>
        virtual public System.String AssetSubGroupName
        {
            get
            {
                return base.GetSystemString(AssetSubGroupMetadata.ColumnNames.AssetSubGroupName);
            }

            set
            {
                base.SetSystemString(AssetSubGroupMetadata.ColumnNames.AssetSubGroupName, value);
            }
        }
        /// <summary>
        /// Maps to AssetSubGroup.Initial
        /// </summary>
        virtual public System.String Initial
        {
            get
            {
                return base.GetSystemString(AssetSubGroupMetadata.ColumnNames.Initial);
            }

            set
            {
                base.SetSystemString(AssetSubGroupMetadata.ColumnNames.Initial, value);
            }
        }
        /// <summary>
        /// Maps to AssetSubGroup.IsActive
        /// </summary>
        virtual public System.Boolean? IsActive
        {
            get
            {
                return base.GetSystemBoolean(AssetSubGroupMetadata.ColumnNames.IsActive);
            }

            set
            {
                base.SetSystemBoolean(AssetSubGroupMetadata.ColumnNames.IsActive, value);
            }
        }
        /// <summary>
        /// Maps to AssetSubGroup.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(AssetSubGroupMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(AssetSubGroupMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to AssetSubGroup.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(AssetSubGroupMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(AssetSubGroupMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esAssetSubGroup entity)
            {
                this.entity = entity;
            }
            public System.String AssetGroupId
            {
                get
                {
                    System.String data = entity.AssetGroupId;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.AssetGroupId = null;
                    else entity.AssetGroupId = Convert.ToString(value);
                }
            }
            public System.String AssetSubGroupId
            {
                get
                {
                    System.String data = entity.AssetSubGroupId;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.AssetSubGroupId = null;
                    else entity.AssetSubGroupId = Convert.ToString(value);
                }
            }
            public System.String AssetSubGroupName
            {
                get
                {
                    System.String data = entity.AssetSubGroupName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.AssetSubGroupName = null;
                    else entity.AssetSubGroupName = Convert.ToString(value);
                }
            }
            public System.String Initial
            {
                get
                {
                    System.String data = entity.Initial;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Initial = null;
                    else entity.Initial = Convert.ToString(value);
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
            private esAssetSubGroup entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esAssetSubGroupQuery query)
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
                throw new Exception("esAssetSubGroup can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class AssetSubGroup : esAssetSubGroup
    {
    }

    [Serializable]
    abstract public class esAssetSubGroupQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return AssetSubGroupMetadata.Meta();
            }
        }

        public esQueryItem AssetGroupId
        {
            get
            {
                return new esQueryItem(this, AssetSubGroupMetadata.ColumnNames.AssetGroupId, esSystemType.String);
            }
        }

        public esQueryItem AssetSubGroupId
        {
            get
            {
                return new esQueryItem(this, AssetSubGroupMetadata.ColumnNames.AssetSubGroupId, esSystemType.String);
            }
        }

        public esQueryItem AssetSubGroupName
        {
            get
            {
                return new esQueryItem(this, AssetSubGroupMetadata.ColumnNames.AssetSubGroupName, esSystemType.String);
            }
        }

        public esQueryItem Initial
        {
            get
            {
                return new esQueryItem(this, AssetSubGroupMetadata.ColumnNames.Initial, esSystemType.String);
            }
        }

        public esQueryItem IsActive
        {
            get
            {
                return new esQueryItem(this, AssetSubGroupMetadata.ColumnNames.IsActive, esSystemType.Boolean);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, AssetSubGroupMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, AssetSubGroupMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("AssetSubGroupCollection")]
    public partial class AssetSubGroupCollection : esAssetSubGroupCollection, IEnumerable<AssetSubGroup>
    {
        public AssetSubGroupCollection()
        {

        }

        public static implicit operator List<AssetSubGroup>(AssetSubGroupCollection coll)
        {
            List<AssetSubGroup> list = new List<AssetSubGroup>();

            foreach (AssetSubGroup emp in coll)
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
                return AssetSubGroupMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new AssetSubGroupQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new AssetSubGroup(row);
        }

        override protected esEntity CreateEntity()
        {
            return new AssetSubGroup();
        }

        #endregion

        [BrowsableAttribute(false)]
        public AssetSubGroupQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new AssetSubGroupQuery();
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
        public bool Load(AssetSubGroupQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public AssetSubGroup AddNew()
        {
            AssetSubGroup entity = base.AddNewEntity() as AssetSubGroup;

            return entity;
        }
        public AssetSubGroup FindByPrimaryKey(String assetGroupId, String assetSubGroupId)
        {
            return base.FindByPrimaryKey(assetGroupId, assetSubGroupId) as AssetSubGroup;
        }

        #region IEnumerable< AssetSubGroup> Members

        IEnumerator<AssetSubGroup> IEnumerable<AssetSubGroup>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as AssetSubGroup;
            }
        }

        #endregion

        private AssetSubGroupQuery query;
    }


    /// <summary>
    /// Encapsulates the 'AssetSubGroup' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("AssetSubGroup ({AssetGroupId, AssetSubGroupId})")]
    [Serializable]
    public partial class AssetSubGroup : esAssetSubGroup
    {
        public AssetSubGroup()
        {
        }

        public AssetSubGroup(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return AssetSubGroupMetadata.Meta();
            }
        }

        override protected esAssetSubGroupQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new AssetSubGroupQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public AssetSubGroupQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new AssetSubGroupQuery();
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
        public bool Load(AssetSubGroupQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private AssetSubGroupQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class AssetSubGroupQuery : esAssetSubGroupQuery
    {
        public AssetSubGroupQuery()
        {

        }

        public AssetSubGroupQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "AssetSubGroupQuery";
        }
    }

    [Serializable]
    public partial class AssetSubGroupMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected AssetSubGroupMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(AssetSubGroupMetadata.ColumnNames.AssetGroupId, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = AssetSubGroupMetadata.PropertyNames.AssetGroupId;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 30;
            _columns.Add(c);

            c = new esColumnMetadata(AssetSubGroupMetadata.ColumnNames.AssetSubGroupId, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = AssetSubGroupMetadata.PropertyNames.AssetSubGroupId;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(AssetSubGroupMetadata.ColumnNames.AssetSubGroupName, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = AssetSubGroupMetadata.PropertyNames.AssetSubGroupName;
            c.CharacterMaxLength = 250;
            _columns.Add(c);

            c = new esColumnMetadata(AssetSubGroupMetadata.ColumnNames.Initial, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = AssetSubGroupMetadata.PropertyNames.Initial;
            c.CharacterMaxLength = 5;
            _columns.Add(c);

            c = new esColumnMetadata(AssetSubGroupMetadata.ColumnNames.IsActive, 4, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = AssetSubGroupMetadata.PropertyNames.IsActive;
            _columns.Add(c);

            c = new esColumnMetadata(AssetSubGroupMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = AssetSubGroupMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AssetSubGroupMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = AssetSubGroupMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public AssetSubGroupMetadata Meta()
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
            public const string AssetGroupId = "AssetGroupId";
            public const string AssetSubGroupId = "AssetSubGroupId";
            public const string AssetSubGroupName = "AssetSubGroupName";
            public const string Initial = "Initial";
            public const string IsActive = "IsActive";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string AssetGroupId = "AssetGroupId";
            public const string AssetSubGroupId = "AssetSubGroupId";
            public const string AssetSubGroupName = "AssetSubGroupName";
            public const string Initial = "Initial";
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
            lock (typeof(AssetSubGroupMetadata))
            {
                if (AssetSubGroupMetadata.mapDelegates == null)
                {
                    AssetSubGroupMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (AssetSubGroupMetadata.meta == null)
                {
                    AssetSubGroupMetadata.meta = new AssetSubGroupMetadata();
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

                meta.AddTypeMap("AssetGroupId", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("AssetSubGroupId", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("AssetSubGroupName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Initial", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "AssetSubGroup";
                meta.Destination = "AssetSubGroup";
                meta.spInsert = "proc_AssetSubGroupInsert";
                meta.spUpdate = "proc_AssetSubGroupUpdate";
                meta.spDelete = "proc_AssetSubGroupDelete";
                meta.spLoadAll = "proc_AssetSubGroupLoadAll";
                meta.spLoadByPrimaryKey = "proc_AssetSubGroupLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private AssetSubGroupMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
