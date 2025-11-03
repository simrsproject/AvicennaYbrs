/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 1/22/2020 4:48:57 PM
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
    abstract public class esMealOrderItemCollection : esEntityCollectionWAuditLog
    {
        public esMealOrderItemCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "MealOrderItemCollection";
        }

        #region Query Logic
        protected void InitQuery(esMealOrderItemQuery query)
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
            this.InitQuery(query as esMealOrderItemQuery);
        }
        #endregion

        virtual public MealOrderItem DetachEntity(MealOrderItem entity)
        {
            return base.DetachEntity(entity) as MealOrderItem;
        }

        virtual public MealOrderItem AttachEntity(MealOrderItem entity)
        {
            return base.AttachEntity(entity) as MealOrderItem;
        }

        virtual public void Combine(MealOrderItemCollection collection)
        {
            base.Combine(collection);
        }

        new public MealOrderItem this[int index]
        {
            get
            {
                return base[index] as MealOrderItem;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(MealOrderItem);
        }
    }

    [Serializable]
    abstract public class esMealOrderItem : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esMealOrderItemQuery GetDynamicQuery()
        {
            return null;
        }

        public esMealOrderItem()
        {
        }

        public esMealOrderItem(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String orderNo, String sRMealSet, String foodID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(orderNo, sRMealSet, foodID);
            else
                return LoadByPrimaryKeyStoredProcedure(orderNo, sRMealSet, foodID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String orderNo, String sRMealSet, String foodID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(orderNo, sRMealSet, foodID);
            else
                return LoadByPrimaryKeyStoredProcedure(orderNo, sRMealSet, foodID);
        }

        private bool LoadByPrimaryKeyDynamic(String orderNo, String sRMealSet, String foodID)
        {
            esMealOrderItemQuery query = this.GetDynamicQuery();
            query.Where(query.OrderNo == orderNo, query.SRMealSet == sRMealSet, query.FoodID == foodID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String orderNo, String sRMealSet, String foodID)
        {
            esParameters parms = new esParameters();
            parms.Add("OrderNo", orderNo);
            parms.Add("SRMealSet", sRMealSet);
            parms.Add("FoodID", foodID);
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
                        case "OrderNo": this.str.OrderNo = (string)value; break;
                        case "SRMealSet": this.str.SRMealSet = (string)value; break;
                        case "FoodID": this.str.FoodID = (string)value; break;
                        case "IsOptional": this.str.IsOptional = (string)value; break;
                        case "IsCustom": this.str.IsCustom = (string)value; break;
                        case "DietPatientNo": this.str.DietPatientNo = (string)value; break;
                        case "DietID": this.str.DietID = (string)value; break;
                        case "MenuID": this.str.MenuID = (string)value; break;
                        case "MenuItemID": this.str.MenuItemID = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "IsOptional":

                            if (value == null || value is System.Boolean)
                                this.IsOptional = (System.Boolean?)value;
                            break;
                        case "IsCustom":

                            if (value == null || value is System.Boolean)
                                this.IsCustom = (System.Boolean?)value;
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
        /// Maps to MealOrderItem.OrderNo
        /// </summary>
        virtual public System.String OrderNo
        {
            get
            {
                return base.GetSystemString(MealOrderItemMetadata.ColumnNames.OrderNo);
            }

            set
            {
                base.SetSystemString(MealOrderItemMetadata.ColumnNames.OrderNo, value);
            }
        }
        /// <summary>
        /// Maps to MealOrderItem.SRMealSet
        /// </summary>
        virtual public System.String SRMealSet
        {
            get
            {
                return base.GetSystemString(MealOrderItemMetadata.ColumnNames.SRMealSet);
            }

            set
            {
                base.SetSystemString(MealOrderItemMetadata.ColumnNames.SRMealSet, value);
            }
        }
        /// <summary>
        /// Maps to MealOrderItem.FoodID
        /// </summary>
        virtual public System.String FoodID
        {
            get
            {
                return base.GetSystemString(MealOrderItemMetadata.ColumnNames.FoodID);
            }

            set
            {
                base.SetSystemString(MealOrderItemMetadata.ColumnNames.FoodID, value);
            }
        }
        /// <summary>
        /// Maps to MealOrderItem.IsOptional
        /// </summary>
        virtual public System.Boolean? IsOptional
        {
            get
            {
                return base.GetSystemBoolean(MealOrderItemMetadata.ColumnNames.IsOptional);
            }

            set
            {
                base.SetSystemBoolean(MealOrderItemMetadata.ColumnNames.IsOptional, value);
            }
        }
        /// <summary>
        /// Maps to MealOrderItem.IsCustom
        /// </summary>
        virtual public System.Boolean? IsCustom
        {
            get
            {
                return base.GetSystemBoolean(MealOrderItemMetadata.ColumnNames.IsCustom);
            }

            set
            {
                base.SetSystemBoolean(MealOrderItemMetadata.ColumnNames.IsCustom, value);
            }
        }
        /// <summary>
        /// Maps to MealOrderItem.DietPatientNo
        /// </summary>
        virtual public System.String DietPatientNo
        {
            get
            {
                return base.GetSystemString(MealOrderItemMetadata.ColumnNames.DietPatientNo);
            }

            set
            {
                base.SetSystemString(MealOrderItemMetadata.ColumnNames.DietPatientNo, value);
            }
        }
        /// <summary>
        /// Maps to MealOrderItem.DietID
        /// </summary>
        virtual public System.String DietID
        {
            get
            {
                return base.GetSystemString(MealOrderItemMetadata.ColumnNames.DietID);
            }

            set
            {
                base.SetSystemString(MealOrderItemMetadata.ColumnNames.DietID, value);
            }
        }
        /// <summary>
        /// Maps to MealOrderItem.MenuID
        /// </summary>
        virtual public System.String MenuID
        {
            get
            {
                return base.GetSystemString(MealOrderItemMetadata.ColumnNames.MenuID);
            }

            set
            {
                base.SetSystemString(MealOrderItemMetadata.ColumnNames.MenuID, value);
            }
        }
        /// <summary>
        /// Maps to MealOrderItem.MenuItemID
        /// </summary>
        virtual public System.String MenuItemID
        {
            get
            {
                return base.GetSystemString(MealOrderItemMetadata.ColumnNames.MenuItemID);
            }

            set
            {
                base.SetSystemString(MealOrderItemMetadata.ColumnNames.MenuItemID, value);
            }
        }
        /// <summary>
        /// Maps to MealOrderItem.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(MealOrderItemMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(MealOrderItemMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to MealOrderItem.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(MealOrderItemMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(MealOrderItemMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esMealOrderItem entity)
            {
                this.entity = entity;
            }
            public System.String OrderNo
            {
                get
                {
                    System.String data = entity.OrderNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.OrderNo = null;
                    else entity.OrderNo = Convert.ToString(value);
                }
            }
            public System.String SRMealSet
            {
                get
                {
                    System.String data = entity.SRMealSet;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRMealSet = null;
                    else entity.SRMealSet = Convert.ToString(value);
                }
            }
            public System.String FoodID
            {
                get
                {
                    System.String data = entity.FoodID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FoodID = null;
                    else entity.FoodID = Convert.ToString(value);
                }
            }
            public System.String IsOptional
            {
                get
                {
                    System.Boolean? data = entity.IsOptional;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsOptional = null;
                    else entity.IsOptional = Convert.ToBoolean(value);
                }
            }
            public System.String IsCustom
            {
                get
                {
                    System.Boolean? data = entity.IsCustom;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsCustom = null;
                    else entity.IsCustom = Convert.ToBoolean(value);
                }
            }
            public System.String DietPatientNo
            {
                get
                {
                    System.String data = entity.DietPatientNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DietPatientNo = null;
                    else entity.DietPatientNo = Convert.ToString(value);
                }
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
            public System.String MenuItemID
            {
                get
                {
                    System.String data = entity.MenuItemID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.MenuItemID = null;
                    else entity.MenuItemID = Convert.ToString(value);
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
            private esMealOrderItem entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esMealOrderItemQuery query)
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
                throw new Exception("esMealOrderItem can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class MealOrderItem : esMealOrderItem
    {
    }

    [Serializable]
    abstract public class esMealOrderItemQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return MealOrderItemMetadata.Meta();
            }
        }

        public esQueryItem OrderNo
        {
            get
            {
                return new esQueryItem(this, MealOrderItemMetadata.ColumnNames.OrderNo, esSystemType.String);
            }
        }

        public esQueryItem SRMealSet
        {
            get
            {
                return new esQueryItem(this, MealOrderItemMetadata.ColumnNames.SRMealSet, esSystemType.String);
            }
        }

        public esQueryItem FoodID
        {
            get
            {
                return new esQueryItem(this, MealOrderItemMetadata.ColumnNames.FoodID, esSystemType.String);
            }
        }

        public esQueryItem IsOptional
        {
            get
            {
                return new esQueryItem(this, MealOrderItemMetadata.ColumnNames.IsOptional, esSystemType.Boolean);
            }
        }

        public esQueryItem IsCustom
        {
            get
            {
                return new esQueryItem(this, MealOrderItemMetadata.ColumnNames.IsCustom, esSystemType.Boolean);
            }
        }

        public esQueryItem DietPatientNo
        {
            get
            {
                return new esQueryItem(this, MealOrderItemMetadata.ColumnNames.DietPatientNo, esSystemType.String);
            }
        }

        public esQueryItem DietID
        {
            get
            {
                return new esQueryItem(this, MealOrderItemMetadata.ColumnNames.DietID, esSystemType.String);
            }
        }

        public esQueryItem MenuID
        {
            get
            {
                return new esQueryItem(this, MealOrderItemMetadata.ColumnNames.MenuID, esSystemType.String);
            }
        }

        public esQueryItem MenuItemID
        {
            get
            {
                return new esQueryItem(this, MealOrderItemMetadata.ColumnNames.MenuItemID, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, MealOrderItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, MealOrderItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("MealOrderItemCollection")]
    public partial class MealOrderItemCollection : esMealOrderItemCollection, IEnumerable<MealOrderItem>
    {
        public MealOrderItemCollection()
        {

        }

        public static implicit operator List<MealOrderItem>(MealOrderItemCollection coll)
        {
            List<MealOrderItem> list = new List<MealOrderItem>();

            foreach (MealOrderItem emp in coll)
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
                return MealOrderItemMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new MealOrderItemQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new MealOrderItem(row);
        }

        override protected esEntity CreateEntity()
        {
            return new MealOrderItem();
        }

        #endregion

        [BrowsableAttribute(false)]
        public MealOrderItemQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new MealOrderItemQuery();
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
        public bool Load(MealOrderItemQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public MealOrderItem AddNew()
        {
            MealOrderItem entity = base.AddNewEntity() as MealOrderItem;

            return entity;
        }
        public MealOrderItem FindByPrimaryKey(String orderNo, String sRMealSet, String foodID)
        {
            return base.FindByPrimaryKey(orderNo, sRMealSet, foodID) as MealOrderItem;
        }

        #region IEnumerable< MealOrderItem> Members

        IEnumerator<MealOrderItem> IEnumerable<MealOrderItem>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as MealOrderItem;
            }
        }

        #endregion

        private MealOrderItemQuery query;
    }


    /// <summary>
    /// Encapsulates the 'MealOrderItem' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("MealOrderItem ({OrderNo, SRMealSet, FoodID})")]
    [Serializable]
    public partial class MealOrderItem : esMealOrderItem
    {
        public MealOrderItem()
        {
        }

        public MealOrderItem(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return MealOrderItemMetadata.Meta();
            }
        }

        override protected esMealOrderItemQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new MealOrderItemQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public MealOrderItemQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new MealOrderItemQuery();
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
        public bool Load(MealOrderItemQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private MealOrderItemQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class MealOrderItemQuery : esMealOrderItemQuery
    {
        public MealOrderItemQuery()
        {

        }

        public MealOrderItemQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "MealOrderItemQuery";
        }
    }

    [Serializable]
    public partial class MealOrderItemMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected MealOrderItemMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(MealOrderItemMetadata.ColumnNames.OrderNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = MealOrderItemMetadata.PropertyNames.OrderNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(MealOrderItemMetadata.ColumnNames.SRMealSet, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = MealOrderItemMetadata.PropertyNames.SRMealSet;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(MealOrderItemMetadata.ColumnNames.FoodID, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = MealOrderItemMetadata.PropertyNames.FoodID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(MealOrderItemMetadata.ColumnNames.IsOptional, 3, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = MealOrderItemMetadata.PropertyNames.IsOptional;
            _columns.Add(c);

            c = new esColumnMetadata(MealOrderItemMetadata.ColumnNames.IsCustom, 4, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = MealOrderItemMetadata.PropertyNames.IsCustom;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MealOrderItemMetadata.ColumnNames.DietPatientNo, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = MealOrderItemMetadata.PropertyNames.DietPatientNo;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MealOrderItemMetadata.ColumnNames.DietID, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = MealOrderItemMetadata.PropertyNames.DietID;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MealOrderItemMetadata.ColumnNames.MenuID, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = MealOrderItemMetadata.PropertyNames.MenuID;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MealOrderItemMetadata.ColumnNames.MenuItemID, 8, typeof(System.String), esSystemType.String);
            c.PropertyName = MealOrderItemMetadata.PropertyNames.MenuItemID;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MealOrderItemMetadata.ColumnNames.LastUpdateDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = MealOrderItemMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MealOrderItemMetadata.ColumnNames.LastUpdateByUserID, 10, typeof(System.String), esSystemType.String);
            c.PropertyName = MealOrderItemMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public MealOrderItemMetadata Meta()
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
            public const string OrderNo = "OrderNo";
            public const string SRMealSet = "SRMealSet";
            public const string FoodID = "FoodID";
            public const string IsOptional = "IsOptional";
            public const string IsCustom = "IsCustom";
            public const string DietPatientNo = "DietPatientNo";
            public const string DietID = "DietID";
            public const string MenuID = "MenuID";
            public const string MenuItemID = "MenuItemID";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string OrderNo = "OrderNo";
            public const string SRMealSet = "SRMealSet";
            public const string FoodID = "FoodID";
            public const string IsOptional = "IsOptional";
            public const string IsCustom = "IsCustom";
            public const string DietPatientNo = "DietPatientNo";
            public const string DietID = "DietID";
            public const string MenuID = "MenuID";
            public const string MenuItemID = "MenuItemID";
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
            lock (typeof(MealOrderItemMetadata))
            {
                if (MealOrderItemMetadata.mapDelegates == null)
                {
                    MealOrderItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (MealOrderItemMetadata.meta == null)
                {
                    MealOrderItemMetadata.meta = new MealOrderItemMetadata();
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

                meta.AddTypeMap("OrderNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRMealSet", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("FoodID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsOptional", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsCustom", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("DietPatientNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("DietID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("MenuID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("MenuItemID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "MealOrderItem";
                meta.Destination = "MealOrderItem";
                meta.spInsert = "proc_MealOrderItemInsert";
                meta.spUpdate = "proc_MealOrderItemUpdate";
                meta.spDelete = "proc_MealOrderItemDelete";
                meta.spLoadAll = "proc_MealOrderItemLoadAll";
                meta.spLoadByPrimaryKey = "proc_MealOrderItemLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private MealOrderItemMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
