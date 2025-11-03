/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 5/30/2018 5:00:28 PM
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
    abstract public class esItemConditionRuleCollection : esEntityCollectionWAuditLog
    {
        public esItemConditionRuleCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "ItemConditionRuleCollection";
        }

        #region Query Logic
        protected void InitQuery(esItemConditionRuleQuery query)
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
            this.InitQuery(query as esItemConditionRuleQuery);
        }
        #endregion

        virtual public ItemConditionRule DetachEntity(ItemConditionRule entity)
        {
            return base.DetachEntity(entity) as ItemConditionRule;
        }

        virtual public ItemConditionRule AttachEntity(ItemConditionRule entity)
        {
            return base.AttachEntity(entity) as ItemConditionRule;
        }

        virtual public void Combine(ItemConditionRuleCollection collection)
        {
            base.Combine(collection);
        }

        new public ItemConditionRule this[int index]
        {
            get
            {
                return base[index] as ItemConditionRule;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(ItemConditionRule);
        }
    }

    [Serializable]
    abstract public class esItemConditionRule : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esItemConditionRuleQuery GetDynamicQuery()
        {
            return null;
        }

        public esItemConditionRule()
        {
        }

        public esItemConditionRule(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String itemConditionRuleID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(itemConditionRuleID);
            else
                return LoadByPrimaryKeyStoredProcedure(itemConditionRuleID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String itemConditionRuleID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(itemConditionRuleID);
            else
                return LoadByPrimaryKeyStoredProcedure(itemConditionRuleID);
        }

        private bool LoadByPrimaryKeyDynamic(String itemConditionRuleID)
        {
            esItemConditionRuleQuery query = this.GetDynamicQuery();
            query.Where(query.ItemConditionRuleID == itemConditionRuleID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String itemConditionRuleID)
        {
            esParameters parms = new esParameters();
            parms.Add("ItemConditionRuleID", itemConditionRuleID);
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
                        case "ItemConditionRuleID": this.str.ItemConditionRuleID = (string)value; break;
                        case "ItemConditionRuleName": this.str.ItemConditionRuleName = (string)value; break;
                        case "StartingDate": this.str.StartingDate = (string)value; break;
                        case "EndingDate": this.str.EndingDate = (string)value; break;
                        case "SRItemConditionRuleType": this.str.SRItemConditionRuleType = (string)value; break;
                        case "IsValueInPercent": this.str.IsValueInPercent = (string)value; break;
                        case "AmountValue": this.str.AmountValue = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "StartingDate":

                            if (value == null || value is System.DateTime)
                                this.StartingDate = (System.DateTime?)value;
                            break;
                        case "EndingDate":

                            if (value == null || value is System.DateTime)
                                this.EndingDate = (System.DateTime?)value;
                            break;
                        case "IsValueInPercent":

                            if (value == null || value is System.Boolean)
                                this.IsValueInPercent = (System.Boolean?)value;
                            break;
                        case "AmountValue":

                            if (value == null || value is System.Decimal)
                                this.AmountValue = (System.Decimal?)value;
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
        /// Maps to ItemConditionRule.ItemConditionRuleID
        /// </summary>
        virtual public System.String ItemConditionRuleID
        {
            get
            {
                return base.GetSystemString(ItemConditionRuleMetadata.ColumnNames.ItemConditionRuleID);
            }

            set
            {
                base.SetSystemString(ItemConditionRuleMetadata.ColumnNames.ItemConditionRuleID, value);
            }
        }
        /// <summary>
        /// Maps to ItemConditionRule.ItemConditionRuleName
        /// </summary>
        virtual public System.String ItemConditionRuleName
        {
            get
            {
                return base.GetSystemString(ItemConditionRuleMetadata.ColumnNames.ItemConditionRuleName);
            }

            set
            {
                base.SetSystemString(ItemConditionRuleMetadata.ColumnNames.ItemConditionRuleName, value);
            }
        }
        /// <summary>
        /// Maps to ItemConditionRule.StartingDate
        /// </summary>
        virtual public System.DateTime? StartingDate
        {
            get
            {
                return base.GetSystemDateTime(ItemConditionRuleMetadata.ColumnNames.StartingDate);
            }

            set
            {
                base.SetSystemDateTime(ItemConditionRuleMetadata.ColumnNames.StartingDate, value);
            }
        }
        /// <summary>
        /// Maps to ItemConditionRule.EndingDate
        /// </summary>
        virtual public System.DateTime? EndingDate
        {
            get
            {
                return base.GetSystemDateTime(ItemConditionRuleMetadata.ColumnNames.EndingDate);
            }

            set
            {
                base.SetSystemDateTime(ItemConditionRuleMetadata.ColumnNames.EndingDate, value);
            }
        }
        /// <summary>
        /// Maps to ItemConditionRule.SRItemConditionRuleType
        /// </summary>
        virtual public System.String SRItemConditionRuleType
        {
            get
            {
                return base.GetSystemString(ItemConditionRuleMetadata.ColumnNames.SRItemConditionRuleType);
            }

            set
            {
                base.SetSystemString(ItemConditionRuleMetadata.ColumnNames.SRItemConditionRuleType, value);
            }
        }
        /// <summary>
        /// Maps to ItemConditionRule.IsValueInPercent
        /// </summary>
        virtual public System.Boolean? IsValueInPercent
        {
            get
            {
                return base.GetSystemBoolean(ItemConditionRuleMetadata.ColumnNames.IsValueInPercent);
            }

            set
            {
                base.SetSystemBoolean(ItemConditionRuleMetadata.ColumnNames.IsValueInPercent, value);
            }
        }
        /// <summary>
        /// Maps to ItemConditionRule.AmountValue
        /// </summary>
        virtual public System.Decimal? AmountValue
        {
            get
            {
                return base.GetSystemDecimal(ItemConditionRuleMetadata.ColumnNames.AmountValue);
            }

            set
            {
                base.SetSystemDecimal(ItemConditionRuleMetadata.ColumnNames.AmountValue, value);
            }
        }
        /// <summary>
        /// Maps to ItemConditionRule.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(ItemConditionRuleMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(ItemConditionRuleMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to ItemConditionRule.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(ItemConditionRuleMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(ItemConditionRuleMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esItemConditionRule entity)
            {
                this.entity = entity;
            }
            public System.String ItemConditionRuleID
            {
                get
                {
                    System.String data = entity.ItemConditionRuleID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ItemConditionRuleID = null;
                    else entity.ItemConditionRuleID = Convert.ToString(value);
                }
            }
            public System.String ItemConditionRuleName
            {
                get
                {
                    System.String data = entity.ItemConditionRuleName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ItemConditionRuleName = null;
                    else entity.ItemConditionRuleName = Convert.ToString(value);
                }
            }
            public System.String StartingDate
            {
                get
                {
                    System.DateTime? data = entity.StartingDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.StartingDate = null;
                    else entity.StartingDate = Convert.ToDateTime(value);
                }
            }
            public System.String EndingDate
            {
                get
                {
                    System.DateTime? data = entity.EndingDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.EndingDate = null;
                    else entity.EndingDate = Convert.ToDateTime(value);
                }
            }
            public System.String SRItemConditionRuleType
            {
                get
                {
                    System.String data = entity.SRItemConditionRuleType;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRItemConditionRuleType = null;
                    else entity.SRItemConditionRuleType = Convert.ToString(value);
                }
            }
            public System.String IsValueInPercent
            {
                get
                {
                    System.Boolean? data = entity.IsValueInPercent;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsValueInPercent = null;
                    else entity.IsValueInPercent = Convert.ToBoolean(value);
                }
            }
            public System.String AmountValue
            {
                get
                {
                    System.Decimal? data = entity.AmountValue;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.AmountValue = null;
                    else entity.AmountValue = Convert.ToDecimal(value);
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
            private esItemConditionRule entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esItemConditionRuleQuery query)
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
                throw new Exception("esItemConditionRule can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class ItemConditionRule : esItemConditionRule
    {
    }

    [Serializable]
    abstract public class esItemConditionRuleQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return ItemConditionRuleMetadata.Meta();
            }
        }

        public esQueryItem ItemConditionRuleID
        {
            get
            {
                return new esQueryItem(this, ItemConditionRuleMetadata.ColumnNames.ItemConditionRuleID, esSystemType.String);
            }
        }

        public esQueryItem ItemConditionRuleName
        {
            get
            {
                return new esQueryItem(this, ItemConditionRuleMetadata.ColumnNames.ItemConditionRuleName, esSystemType.String);
            }
        }

        public esQueryItem StartingDate
        {
            get
            {
                return new esQueryItem(this, ItemConditionRuleMetadata.ColumnNames.StartingDate, esSystemType.DateTime);
            }
        }

        public esQueryItem EndingDate
        {
            get
            {
                return new esQueryItem(this, ItemConditionRuleMetadata.ColumnNames.EndingDate, esSystemType.DateTime);
            }
        }

        public esQueryItem SRItemConditionRuleType
        {
            get
            {
                return new esQueryItem(this, ItemConditionRuleMetadata.ColumnNames.SRItemConditionRuleType, esSystemType.String);
            }
        }

        public esQueryItem IsValueInPercent
        {
            get
            {
                return new esQueryItem(this, ItemConditionRuleMetadata.ColumnNames.IsValueInPercent, esSystemType.Boolean);
            }
        }

        public esQueryItem AmountValue
        {
            get
            {
                return new esQueryItem(this, ItemConditionRuleMetadata.ColumnNames.AmountValue, esSystemType.Decimal);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, ItemConditionRuleMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, ItemConditionRuleMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("ItemConditionRuleCollection")]
    public partial class ItemConditionRuleCollection : esItemConditionRuleCollection, IEnumerable<ItemConditionRule>
    {
        public ItemConditionRuleCollection()
        {

        }

        public static implicit operator List<ItemConditionRule>(ItemConditionRuleCollection coll)
        {
            List<ItemConditionRule> list = new List<ItemConditionRule>();

            foreach (ItemConditionRule emp in coll)
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
                return ItemConditionRuleMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ItemConditionRuleQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new ItemConditionRule(row);
        }

        override protected esEntity CreateEntity()
        {
            return new ItemConditionRule();
        }

        #endregion

        [BrowsableAttribute(false)]
        public ItemConditionRuleQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ItemConditionRuleQuery();
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
        public bool Load(ItemConditionRuleQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public ItemConditionRule AddNew()
        {
            ItemConditionRule entity = base.AddNewEntity() as ItemConditionRule;

            return entity;
        }
        public ItemConditionRule FindByPrimaryKey(String itemConditionRuleID)
        {
            return base.FindByPrimaryKey(itemConditionRuleID) as ItemConditionRule;
        }

        #region IEnumerable< ItemConditionRule> Members

        IEnumerator<ItemConditionRule> IEnumerable<ItemConditionRule>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as ItemConditionRule;
            }
        }

        #endregion

        private ItemConditionRuleQuery query;
    }


    /// <summary>
    /// Encapsulates the 'ItemConditionRule' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("ItemConditionRule ({ItemConditionRuleID})")]
    [Serializable]
    public partial class ItemConditionRule : esItemConditionRule
    {
        public ItemConditionRule()
        {
        }

        public ItemConditionRule(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return ItemConditionRuleMetadata.Meta();
            }
        }

        override protected esItemConditionRuleQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ItemConditionRuleQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public ItemConditionRuleQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ItemConditionRuleQuery();
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
        public bool Load(ItemConditionRuleQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private ItemConditionRuleQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class ItemConditionRuleQuery : esItemConditionRuleQuery
    {
        public ItemConditionRuleQuery()
        {

        }

        public ItemConditionRuleQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "ItemConditionRuleQuery";
        }
    }

    [Serializable]
    public partial class ItemConditionRuleMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected ItemConditionRuleMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(ItemConditionRuleMetadata.ColumnNames.ItemConditionRuleID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemConditionRuleMetadata.PropertyNames.ItemConditionRuleID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(ItemConditionRuleMetadata.ColumnNames.ItemConditionRuleName, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemConditionRuleMetadata.PropertyNames.ItemConditionRuleName;
            c.CharacterMaxLength = 250;
            _columns.Add(c);

            c = new esColumnMetadata(ItemConditionRuleMetadata.ColumnNames.StartingDate, 2, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ItemConditionRuleMetadata.PropertyNames.StartingDate;
            _columns.Add(c);

            c = new esColumnMetadata(ItemConditionRuleMetadata.ColumnNames.EndingDate, 3, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ItemConditionRuleMetadata.PropertyNames.EndingDate;
            _columns.Add(c);

            c = new esColumnMetadata(ItemConditionRuleMetadata.ColumnNames.SRItemConditionRuleType, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemConditionRuleMetadata.PropertyNames.SRItemConditionRuleType;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(ItemConditionRuleMetadata.ColumnNames.IsValueInPercent, 5, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ItemConditionRuleMetadata.PropertyNames.IsValueInPercent;
            _columns.Add(c);

            c = new esColumnMetadata(ItemConditionRuleMetadata.ColumnNames.AmountValue, 6, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ItemConditionRuleMetadata.PropertyNames.AmountValue;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(ItemConditionRuleMetadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ItemConditionRuleMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemConditionRuleMetadata.ColumnNames.LastUpdateByUserID, 8, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemConditionRuleMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public ItemConditionRuleMetadata Meta()
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
            public const string ItemConditionRuleID = "ItemConditionRuleID";
            public const string ItemConditionRuleName = "ItemConditionRuleName";
            public const string StartingDate = "StartingDate";
            public const string EndingDate = "EndingDate";
            public const string SRItemConditionRuleType = "SRItemConditionRuleType";
            public const string IsValueInPercent = "IsValueInPercent";
            public const string AmountValue = "AmountValue";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string ItemConditionRuleID = "ItemConditionRuleID";
            public const string ItemConditionRuleName = "ItemConditionRuleName";
            public const string StartingDate = "StartingDate";
            public const string EndingDate = "EndingDate";
            public const string SRItemConditionRuleType = "SRItemConditionRuleType";
            public const string IsValueInPercent = "IsValueInPercent";
            public const string AmountValue = "AmountValue";
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
            lock (typeof(ItemConditionRuleMetadata))
            {
                if (ItemConditionRuleMetadata.mapDelegates == null)
                {
                    ItemConditionRuleMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (ItemConditionRuleMetadata.meta == null)
                {
                    ItemConditionRuleMetadata.meta = new ItemConditionRuleMetadata();
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

                meta.AddTypeMap("ItemConditionRuleID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ItemConditionRuleName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("StartingDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("EndingDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("SRItemConditionRuleType", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsValueInPercent", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("AmountValue", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "ItemConditionRule";
                meta.Destination = "ItemConditionRule";
                meta.spInsert = "proc_ItemConditionRuleInsert";
                meta.spUpdate = "proc_ItemConditionRuleUpdate";
                meta.spDelete = "proc_ItemConditionRuleDelete";
                meta.spLoadAll = "proc_ItemConditionRuleLoadAll";
                meta.spLoadByPrimaryKey = "proc_ItemConditionRuleLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private ItemConditionRuleMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
