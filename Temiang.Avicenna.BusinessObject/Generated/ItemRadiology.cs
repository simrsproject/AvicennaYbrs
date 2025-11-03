/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 1/9/2019 2:28:30 PM
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
    abstract public class esItemRadiologyCollection : esEntityCollectionWAuditLog
    {
        public esItemRadiologyCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "ItemRadiologyCollection";
        }

        #region Query Logic
        protected void InitQuery(esItemRadiologyQuery query)
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
            this.InitQuery(query as esItemRadiologyQuery);
        }
        #endregion

        virtual public ItemRadiology DetachEntity(ItemRadiology entity)
        {
            return base.DetachEntity(entity) as ItemRadiology;
        }

        virtual public ItemRadiology AttachEntity(ItemRadiology entity)
        {
            return base.AttachEntity(entity) as ItemRadiology;
        }

        virtual public void Combine(ItemRadiologyCollection collection)
        {
            base.Combine(collection);
        }

        new public ItemRadiology this[int index]
        {
            get
            {
                return base[index] as ItemRadiology;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(ItemRadiology);
        }
    }

    [Serializable]
    abstract public class esItemRadiology : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esItemRadiologyQuery GetDynamicQuery()
        {
            return null;
        }

        public esItemRadiology()
        {
        }

        public esItemRadiology(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String itemID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(itemID);
            else
                return LoadByPrimaryKeyStoredProcedure(itemID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String itemID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(itemID);
            else
                return LoadByPrimaryKeyStoredProcedure(itemID);
        }

        private bool LoadByPrimaryKeyDynamic(String itemID)
        {
            esItemRadiologyQuery query = this.GetDynamicQuery();
            query.Where(query.ItemID == itemID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String itemID)
        {
            esParameters parms = new esParameters();
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
                        case "ItemID": this.str.ItemID = (string)value; break;
                        case "ReportRLID": this.str.ReportRLID = (string)value; break;
                        case "IsAdminCalculation": this.str.IsAdminCalculation = (string)value; break;
                        case "IsAllowVariable": this.str.IsAllowVariable = (string)value; break;
                        case "IsAllowCito": this.str.IsAllowCito = (string)value; break;
                        case "IsAllowDiscount": this.str.IsAllowDiscount = (string)value; break;
                        case "IsPrintWithDoctorName": this.str.IsPrintWithDoctorName = (string)value; break;
                        case "IsAssetUtilization": this.str.IsAssetUtilization = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "RlMasterReportItemID": this.str.RlMasterReportItemID = (string)value; break;
                        case "IsCitoFromStandardReference": this.str.IsCitoFromStandardReference = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "IsAdminCalculation":

                            if (value == null || value is System.Boolean)
                                this.IsAdminCalculation = (System.Boolean?)value;
                            break;
                        case "IsAllowVariable":

                            if (value == null || value is System.Boolean)
                                this.IsAllowVariable = (System.Boolean?)value;
                            break;
                        case "IsAllowCito":

                            if (value == null || value is System.Boolean)
                                this.IsAllowCito = (System.Boolean?)value;
                            break;
                        case "IsAllowDiscount":

                            if (value == null || value is System.Boolean)
                                this.IsAllowDiscount = (System.Boolean?)value;
                            break;
                        case "IsPrintWithDoctorName":

                            if (value == null || value is System.Boolean)
                                this.IsPrintWithDoctorName = (System.Boolean?)value;
                            break;
                        case "IsAssetUtilization":

                            if (value == null || value is System.Boolean)
                                this.IsAssetUtilization = (System.Boolean?)value;
                            break;
                        case "LastUpdateDateTime":

                            if (value == null || value is System.DateTime)
                                this.LastUpdateDateTime = (System.DateTime?)value;
                            break;
                        case "RlMasterReportItemID":

                            if (value == null || value is System.Int32)
                                this.RlMasterReportItemID = (System.Int32?)value;
                            break;
                        case "IsCitoFromStandardReference":

                            if (value == null || value is System.Boolean)
                                this.IsCitoFromStandardReference = (System.Boolean?)value;
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
        /// Maps to ItemRadiology.ItemID
        /// </summary>
        virtual public System.String ItemID
        {
            get
            {
                return base.GetSystemString(ItemRadiologyMetadata.ColumnNames.ItemID);
            }

            set
            {
                base.SetSystemString(ItemRadiologyMetadata.ColumnNames.ItemID, value);
            }
        }
        /// <summary>
        /// Maps to ItemRadiology.ReportRLID
        /// </summary>
        virtual public System.String ReportRLID
        {
            get
            {
                return base.GetSystemString(ItemRadiologyMetadata.ColumnNames.ReportRLID);
            }

            set
            {
                base.SetSystemString(ItemRadiologyMetadata.ColumnNames.ReportRLID, value);
            }
        }
        /// <summary>
        /// Maps to ItemRadiology.IsAdminCalculation
        /// </summary>
        virtual public System.Boolean? IsAdminCalculation
        {
            get
            {
                return base.GetSystemBoolean(ItemRadiologyMetadata.ColumnNames.IsAdminCalculation);
            }

            set
            {
                base.SetSystemBoolean(ItemRadiologyMetadata.ColumnNames.IsAdminCalculation, value);
            }
        }
        /// <summary>
        /// Maps to ItemRadiology.IsAllowVariable
        /// </summary>
        virtual public System.Boolean? IsAllowVariable
        {
            get
            {
                return base.GetSystemBoolean(ItemRadiologyMetadata.ColumnNames.IsAllowVariable);
            }

            set
            {
                base.SetSystemBoolean(ItemRadiologyMetadata.ColumnNames.IsAllowVariable, value);
            }
        }
        /// <summary>
        /// Maps to ItemRadiology.IsAllowCito
        /// </summary>
        virtual public System.Boolean? IsAllowCito
        {
            get
            {
                return base.GetSystemBoolean(ItemRadiologyMetadata.ColumnNames.IsAllowCito);
            }

            set
            {
                base.SetSystemBoolean(ItemRadiologyMetadata.ColumnNames.IsAllowCito, value);
            }
        }
        /// <summary>
        /// Maps to ItemRadiology.IsAllowDiscount
        /// </summary>
        virtual public System.Boolean? IsAllowDiscount
        {
            get
            {
                return base.GetSystemBoolean(ItemRadiologyMetadata.ColumnNames.IsAllowDiscount);
            }

            set
            {
                base.SetSystemBoolean(ItemRadiologyMetadata.ColumnNames.IsAllowDiscount, value);
            }
        }
        /// <summary>
        /// Maps to ItemRadiology.IsPrintWithDoctorName
        /// </summary>
        virtual public System.Boolean? IsPrintWithDoctorName
        {
            get
            {
                return base.GetSystemBoolean(ItemRadiologyMetadata.ColumnNames.IsPrintWithDoctorName);
            }

            set
            {
                base.SetSystemBoolean(ItemRadiologyMetadata.ColumnNames.IsPrintWithDoctorName, value);
            }
        }
        /// <summary>
        /// Maps to ItemRadiology.IsAssetUtilization
        /// </summary>
        virtual public System.Boolean? IsAssetUtilization
        {
            get
            {
                return base.GetSystemBoolean(ItemRadiologyMetadata.ColumnNames.IsAssetUtilization);
            }

            set
            {
                base.SetSystemBoolean(ItemRadiologyMetadata.ColumnNames.IsAssetUtilization, value);
            }
        }
        /// <summary>
        /// Maps to ItemRadiology.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(ItemRadiologyMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(ItemRadiologyMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to ItemRadiology.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(ItemRadiologyMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(ItemRadiologyMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to ItemRadiology.RlMasterReportItemID
        /// </summary>
        virtual public System.Int32? RlMasterReportItemID
        {
            get
            {
                return base.GetSystemInt32(ItemRadiologyMetadata.ColumnNames.RlMasterReportItemID);
            }

            set
            {
                base.SetSystemInt32(ItemRadiologyMetadata.ColumnNames.RlMasterReportItemID, value);
            }
        }
        /// <summary>
        /// Maps to ItemRadiology.IsCitoFromStandardReference
        /// </summary>
        virtual public System.Boolean? IsCitoFromStandardReference
        {
            get
            {
                return base.GetSystemBoolean(ItemRadiologyMetadata.ColumnNames.IsCitoFromStandardReference);
            }

            set
            {
                base.SetSystemBoolean(ItemRadiologyMetadata.ColumnNames.IsCitoFromStandardReference, value);
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
            public esStrings(esItemRadiology entity)
            {
                this.entity = entity;
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
            public System.String ReportRLID
            {
                get
                {
                    System.String data = entity.ReportRLID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ReportRLID = null;
                    else entity.ReportRLID = Convert.ToString(value);
                }
            }
            public System.String IsAdminCalculation
            {
                get
                {
                    System.Boolean? data = entity.IsAdminCalculation;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsAdminCalculation = null;
                    else entity.IsAdminCalculation = Convert.ToBoolean(value);
                }
            }
            public System.String IsAllowVariable
            {
                get
                {
                    System.Boolean? data = entity.IsAllowVariable;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsAllowVariable = null;
                    else entity.IsAllowVariable = Convert.ToBoolean(value);
                }
            }
            public System.String IsAllowCito
            {
                get
                {
                    System.Boolean? data = entity.IsAllowCito;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsAllowCito = null;
                    else entity.IsAllowCito = Convert.ToBoolean(value);
                }
            }
            public System.String IsAllowDiscount
            {
                get
                {
                    System.Boolean? data = entity.IsAllowDiscount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsAllowDiscount = null;
                    else entity.IsAllowDiscount = Convert.ToBoolean(value);
                }
            }
            public System.String IsPrintWithDoctorName
            {
                get
                {
                    System.Boolean? data = entity.IsPrintWithDoctorName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsPrintWithDoctorName = null;
                    else entity.IsPrintWithDoctorName = Convert.ToBoolean(value);
                }
            }
            public System.String IsAssetUtilization
            {
                get
                {
                    System.Boolean? data = entity.IsAssetUtilization;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsAssetUtilization = null;
                    else entity.IsAssetUtilization = Convert.ToBoolean(value);
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
            public System.String RlMasterReportItemID
            {
                get
                {
                    System.Int32? data = entity.RlMasterReportItemID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RlMasterReportItemID = null;
                    else entity.RlMasterReportItemID = Convert.ToInt32(value);
                }
            }
            public System.String IsCitoFromStandardReference
            {
                get
                {
                    System.Boolean? data = entity.IsCitoFromStandardReference;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsCitoFromStandardReference = null;
                    else entity.IsCitoFromStandardReference = Convert.ToBoolean(value);
                }
            }
            private esItemRadiology entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esItemRadiologyQuery query)
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
                throw new Exception("esItemRadiology can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class ItemRadiology : esItemRadiology
    {
    }

    [Serializable]
    abstract public class esItemRadiologyQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return ItemRadiologyMetadata.Meta();
            }
        }

        public esQueryItem ItemID
        {
            get
            {
                return new esQueryItem(this, ItemRadiologyMetadata.ColumnNames.ItemID, esSystemType.String);
            }
        }

        public esQueryItem ReportRLID
        {
            get
            {
                return new esQueryItem(this, ItemRadiologyMetadata.ColumnNames.ReportRLID, esSystemType.String);
            }
        }

        public esQueryItem IsAdminCalculation
        {
            get
            {
                return new esQueryItem(this, ItemRadiologyMetadata.ColumnNames.IsAdminCalculation, esSystemType.Boolean);
            }
        }

        public esQueryItem IsAllowVariable
        {
            get
            {
                return new esQueryItem(this, ItemRadiologyMetadata.ColumnNames.IsAllowVariable, esSystemType.Boolean);
            }
        }

        public esQueryItem IsAllowCito
        {
            get
            {
                return new esQueryItem(this, ItemRadiologyMetadata.ColumnNames.IsAllowCito, esSystemType.Boolean);
            }
        }

        public esQueryItem IsAllowDiscount
        {
            get
            {
                return new esQueryItem(this, ItemRadiologyMetadata.ColumnNames.IsAllowDiscount, esSystemType.Boolean);
            }
        }

        public esQueryItem IsPrintWithDoctorName
        {
            get
            {
                return new esQueryItem(this, ItemRadiologyMetadata.ColumnNames.IsPrintWithDoctorName, esSystemType.Boolean);
            }
        }

        public esQueryItem IsAssetUtilization
        {
            get
            {
                return new esQueryItem(this, ItemRadiologyMetadata.ColumnNames.IsAssetUtilization, esSystemType.Boolean);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, ItemRadiologyMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, ItemRadiologyMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem RlMasterReportItemID
        {
            get
            {
                return new esQueryItem(this, ItemRadiologyMetadata.ColumnNames.RlMasterReportItemID, esSystemType.Int32);
            }
        }

        public esQueryItem IsCitoFromStandardReference
        {
            get
            {
                return new esQueryItem(this, ItemRadiologyMetadata.ColumnNames.IsCitoFromStandardReference, esSystemType.Boolean);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("ItemRadiologyCollection")]
    public partial class ItemRadiologyCollection : esItemRadiologyCollection, IEnumerable<ItemRadiology>
    {
        public ItemRadiologyCollection()
        {

        }

        public static implicit operator List<ItemRadiology>(ItemRadiologyCollection coll)
        {
            List<ItemRadiology> list = new List<ItemRadiology>();

            foreach (ItemRadiology emp in coll)
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
                return ItemRadiologyMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ItemRadiologyQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new ItemRadiology(row);
        }

        override protected esEntity CreateEntity()
        {
            return new ItemRadiology();
        }

        #endregion

        [BrowsableAttribute(false)]
        public ItemRadiologyQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ItemRadiologyQuery();
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
        public bool Load(ItemRadiologyQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public ItemRadiology AddNew()
        {
            ItemRadiology entity = base.AddNewEntity() as ItemRadiology;

            return entity;
        }
        public ItemRadiology FindByPrimaryKey(String itemID)
        {
            return base.FindByPrimaryKey(itemID) as ItemRadiology;
        }

        #region IEnumerable< ItemRadiology> Members

        IEnumerator<ItemRadiology> IEnumerable<ItemRadiology>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as ItemRadiology;
            }
        }

        #endregion

        private ItemRadiologyQuery query;
    }


    /// <summary>
    /// Encapsulates the 'ItemRadiology' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("ItemRadiology ({ItemID})")]
    [Serializable]
    public partial class ItemRadiology : esItemRadiology
    {
        public ItemRadiology()
        {
        }

        public ItemRadiology(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return ItemRadiologyMetadata.Meta();
            }
        }

        override protected esItemRadiologyQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ItemRadiologyQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public ItemRadiologyQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ItemRadiologyQuery();
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
        public bool Load(ItemRadiologyQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private ItemRadiologyQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class ItemRadiologyQuery : esItemRadiologyQuery
    {
        public ItemRadiologyQuery()
        {

        }

        public ItemRadiologyQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "ItemRadiologyQuery";
        }
    }

    [Serializable]
    public partial class ItemRadiologyMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected ItemRadiologyMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(ItemRadiologyMetadata.ColumnNames.ItemID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemRadiologyMetadata.PropertyNames.ItemID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(ItemRadiologyMetadata.ColumnNames.ReportRLID, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemRadiologyMetadata.PropertyNames.ReportRLID;
            c.CharacterMaxLength = 10;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(ItemRadiologyMetadata.ColumnNames.IsAdminCalculation, 2, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ItemRadiologyMetadata.PropertyNames.IsAdminCalculation;
            c.HasDefault = true;
            c.Default = @"((1))";
            _columns.Add(c);

            c = new esColumnMetadata(ItemRadiologyMetadata.ColumnNames.IsAllowVariable, 3, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ItemRadiologyMetadata.PropertyNames.IsAllowVariable;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(ItemRadiologyMetadata.ColumnNames.IsAllowCito, 4, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ItemRadiologyMetadata.PropertyNames.IsAllowCito;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(ItemRadiologyMetadata.ColumnNames.IsAllowDiscount, 5, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ItemRadiologyMetadata.PropertyNames.IsAllowDiscount;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(ItemRadiologyMetadata.ColumnNames.IsPrintWithDoctorName, 6, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ItemRadiologyMetadata.PropertyNames.IsPrintWithDoctorName;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(ItemRadiologyMetadata.ColumnNames.IsAssetUtilization, 7, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ItemRadiologyMetadata.PropertyNames.IsAssetUtilization;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(ItemRadiologyMetadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ItemRadiologyMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemRadiologyMetadata.ColumnNames.LastUpdateByUserID, 9, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemRadiologyMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 40;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemRadiologyMetadata.ColumnNames.RlMasterReportItemID, 10, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = ItemRadiologyMetadata.PropertyNames.RlMasterReportItemID;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemRadiologyMetadata.ColumnNames.IsCitoFromStandardReference, 11, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ItemRadiologyMetadata.PropertyNames.IsCitoFromStandardReference;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public ItemRadiologyMetadata Meta()
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
            public const string ItemID = "ItemID";
            public const string ReportRLID = "ReportRLID";
            public const string IsAdminCalculation = "IsAdminCalculation";
            public const string IsAllowVariable = "IsAllowVariable";
            public const string IsAllowCito = "IsAllowCito";
            public const string IsAllowDiscount = "IsAllowDiscount";
            public const string IsPrintWithDoctorName = "IsPrintWithDoctorName";
            public const string IsAssetUtilization = "IsAssetUtilization";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string RlMasterReportItemID = "RlMasterReportItemID";
            public const string IsCitoFromStandardReference = "IsCitoFromStandardReference";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string ItemID = "ItemID";
            public const string ReportRLID = "ReportRLID";
            public const string IsAdminCalculation = "IsAdminCalculation";
            public const string IsAllowVariable = "IsAllowVariable";
            public const string IsAllowCito = "IsAllowCito";
            public const string IsAllowDiscount = "IsAllowDiscount";
            public const string IsPrintWithDoctorName = "IsPrintWithDoctorName";
            public const string IsAssetUtilization = "IsAssetUtilization";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string RlMasterReportItemID = "RlMasterReportItemID";
            public const string IsCitoFromStandardReference = "IsCitoFromStandardReference";
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
            lock (typeof(ItemRadiologyMetadata))
            {
                if (ItemRadiologyMetadata.mapDelegates == null)
                {
                    ItemRadiologyMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (ItemRadiologyMetadata.meta == null)
                {
                    ItemRadiologyMetadata.meta = new ItemRadiologyMetadata();
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

                meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ReportRLID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsAdminCalculation", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsAllowVariable", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsAllowCito", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsAllowDiscount", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsPrintWithDoctorName", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsAssetUtilization", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("RlMasterReportItemID", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("IsCitoFromStandardReference", new esTypeMap("bit", "System.Boolean"));


                meta.Source = "ItemRadiology";
                meta.Destination = "ItemRadiology";
                meta.spInsert = "proc_ItemRadiologyInsert";
                meta.spUpdate = "proc_ItemRadiologyUpdate";
                meta.spDelete = "proc_ItemRadiologyDelete";
                meta.spLoadAll = "proc_ItemRadiologyLoadAll";
                meta.spLoadByPrimaryKey = "proc_ItemRadiologyLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private ItemRadiologyMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
