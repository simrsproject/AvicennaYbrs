/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 9/14/2018 3:11:16 PM
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
    abstract public class esServiceUnitItemServiceCompMappingCollection : esEntityCollectionWAuditLog
    {
        public esServiceUnitItemServiceCompMappingCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "ServiceUnitItemServiceCompMappingCollection";
        }

        #region Query Logic
        protected void InitQuery(esServiceUnitItemServiceCompMappingQuery query)
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
            this.InitQuery(query as esServiceUnitItemServiceCompMappingQuery);
        }
        #endregion

        virtual public ServiceUnitItemServiceCompMapping DetachEntity(ServiceUnitItemServiceCompMapping entity)
        {
            return base.DetachEntity(entity) as ServiceUnitItemServiceCompMapping;
        }

        virtual public ServiceUnitItemServiceCompMapping AttachEntity(ServiceUnitItemServiceCompMapping entity)
        {
            return base.AttachEntity(entity) as ServiceUnitItemServiceCompMapping;
        }

        virtual public void Combine(ServiceUnitItemServiceCompMappingCollection collection)
        {
            base.Combine(collection);
        }

        new public ServiceUnitItemServiceCompMapping this[int index]
        {
            get
            {
                return base[index] as ServiceUnitItemServiceCompMapping;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(ServiceUnitItemServiceCompMapping);
        }
    }

    [Serializable]
    abstract public class esServiceUnitItemServiceCompMapping : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esServiceUnitItemServiceCompMappingQuery GetDynamicQuery()
        {
            return null;
        }

        public esServiceUnitItemServiceCompMapping()
        {
        }

        public esServiceUnitItemServiceCompMapping(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String serviceUnitID, String itemID, String tariffComponentID, String sRRegistrationType, String sRGuarantorIncomeGroup)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(serviceUnitID, itemID, tariffComponentID, sRRegistrationType, sRGuarantorIncomeGroup);
            else
                return LoadByPrimaryKeyStoredProcedure(serviceUnitID, itemID, tariffComponentID, sRRegistrationType, sRGuarantorIncomeGroup);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String serviceUnitID, String itemID, String tariffComponentID, String sRRegistrationType, String sRGuarantorIncomeGroup)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(serviceUnitID, itemID, tariffComponentID, sRRegistrationType, sRGuarantorIncomeGroup);
            else
                return LoadByPrimaryKeyStoredProcedure(serviceUnitID, itemID, tariffComponentID, sRRegistrationType, sRGuarantorIncomeGroup);
        }

        private bool LoadByPrimaryKeyDynamic(String serviceUnitID, String itemID, String tariffComponentID, String sRRegistrationType, String sRGuarantorIncomeGroup)
        {
            esServiceUnitItemServiceCompMappingQuery query = this.GetDynamicQuery();
            query.Where(query.ServiceUnitID == serviceUnitID, query.ItemID == itemID, query.TariffComponentID == tariffComponentID, query.SRRegistrationType == sRRegistrationType, query.SRGuarantorIncomeGroup == sRGuarantorIncomeGroup);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String serviceUnitID, String itemID, String tariffComponentID, String sRRegistrationType, String sRGuarantorIncomeGroup)
        {
            esParameters parms = new esParameters();
            parms.Add("ServiceUnitID", serviceUnitID);
            parms.Add("ItemID", itemID);
            parms.Add("TariffComponentID", tariffComponentID);
            parms.Add("SRRegistrationType", sRRegistrationType);
            parms.Add("SRGuarantorIncomeGroup", sRGuarantorIncomeGroup);
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
                        case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
                        case "ItemID": this.str.ItemID = (string)value; break;
                        case "TariffComponentID": this.str.TariffComponentID = (string)value; break;
                        case "ChartOfAccountIdIncome": this.str.ChartOfAccountIdIncome = (string)value; break;
                        case "SubledgerIdIncome": this.str.SubledgerIdIncome = (string)value; break;
                        case "ChartOfAccountIdDiscount": this.str.ChartOfAccountIdDiscount = (string)value; break;
                        case "SubledgerIdDiscount": this.str.SubledgerIdDiscount = (string)value; break;
                        case "ChartOfAccountIdCost": this.str.ChartOfAccountIdCost = (string)value; break;
                        case "SubledgerIdCost": this.str.SubledgerIdCost = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "SRRegistrationType": this.str.SRRegistrationType = (string)value; break;
                        case "SRGuarantorIncomeGroup": this.str.SRGuarantorIncomeGroup = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "ChartOfAccountIdIncome":

                            if (value == null || value is System.Int32)
                                this.ChartOfAccountIdIncome = (System.Int32?)value;
                            break;
                        case "SubledgerIdIncome":

                            if (value == null || value is System.Int32)
                                this.SubledgerIdIncome = (System.Int32?)value;
                            break;
                        case "ChartOfAccountIdDiscount":

                            if (value == null || value is System.Int32)
                                this.ChartOfAccountIdDiscount = (System.Int32?)value;
                            break;
                        case "SubledgerIdDiscount":

                            if (value == null || value is System.Int32)
                                this.SubledgerIdDiscount = (System.Int32?)value;
                            break;
                        case "ChartOfAccountIdCost":

                            if (value == null || value is System.Int32)
                                this.ChartOfAccountIdCost = (System.Int32?)value;
                            break;
                        case "SubledgerIdCost":

                            if (value == null || value is System.Int32)
                                this.SubledgerIdCost = (System.Int32?)value;
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
        /// Maps to ServiceUnitItemServiceCompMapping.ServiceUnitID
        /// </summary>
        virtual public System.String ServiceUnitID
        {
            get
            {
                return base.GetSystemString(ServiceUnitItemServiceCompMappingMetadata.ColumnNames.ServiceUnitID);
            }

            set
            {
                base.SetSystemString(ServiceUnitItemServiceCompMappingMetadata.ColumnNames.ServiceUnitID, value);
            }
        }
        /// <summary>
        /// Maps to ServiceUnitItemServiceCompMapping.ItemID
        /// </summary>
        virtual public System.String ItemID
        {
            get
            {
                return base.GetSystemString(ServiceUnitItemServiceCompMappingMetadata.ColumnNames.ItemID);
            }

            set
            {
                base.SetSystemString(ServiceUnitItemServiceCompMappingMetadata.ColumnNames.ItemID, value);
            }
        }
        /// <summary>
        /// Maps to ServiceUnitItemServiceCompMapping.TariffComponentID
        /// </summary>
        virtual public System.String TariffComponentID
        {
            get
            {
                return base.GetSystemString(ServiceUnitItemServiceCompMappingMetadata.ColumnNames.TariffComponentID);
            }

            set
            {
                base.SetSystemString(ServiceUnitItemServiceCompMappingMetadata.ColumnNames.TariffComponentID, value);
            }
        }
        /// <summary>
        /// Maps to ServiceUnitItemServiceCompMapping.ChartOfAccountIdIncome
        /// </summary>
        virtual public System.Int32? ChartOfAccountIdIncome
        {
            get
            {
                return base.GetSystemInt32(ServiceUnitItemServiceCompMappingMetadata.ColumnNames.ChartOfAccountIdIncome);
            }

            set
            {
                base.SetSystemInt32(ServiceUnitItemServiceCompMappingMetadata.ColumnNames.ChartOfAccountIdIncome, value);
            }
        }
        /// <summary>
        /// Maps to ServiceUnitItemServiceCompMapping.SubledgerIdIncome
        /// </summary>
        virtual public System.Int32? SubledgerIdIncome
        {
            get
            {
                return base.GetSystemInt32(ServiceUnitItemServiceCompMappingMetadata.ColumnNames.SubledgerIdIncome);
            }

            set
            {
                base.SetSystemInt32(ServiceUnitItemServiceCompMappingMetadata.ColumnNames.SubledgerIdIncome, value);
            }
        }
        /// <summary>
        /// Maps to ServiceUnitItemServiceCompMapping.ChartOfAccountIdDiscount
        /// </summary>
        virtual public System.Int32? ChartOfAccountIdDiscount
        {
            get
            {
                return base.GetSystemInt32(ServiceUnitItemServiceCompMappingMetadata.ColumnNames.ChartOfAccountIdDiscount);
            }

            set
            {
                base.SetSystemInt32(ServiceUnitItemServiceCompMappingMetadata.ColumnNames.ChartOfAccountIdDiscount, value);
            }
        }
        /// <summary>
        /// Maps to ServiceUnitItemServiceCompMapping.SubledgerIdDiscount
        /// </summary>
        virtual public System.Int32? SubledgerIdDiscount
        {
            get
            {
                return base.GetSystemInt32(ServiceUnitItemServiceCompMappingMetadata.ColumnNames.SubledgerIdDiscount);
            }

            set
            {
                base.SetSystemInt32(ServiceUnitItemServiceCompMappingMetadata.ColumnNames.SubledgerIdDiscount, value);
            }
        }
        /// <summary>
        /// Maps to ServiceUnitItemServiceCompMapping.ChartOfAccountIdCost
        /// </summary>
        virtual public System.Int32? ChartOfAccountIdCost
        {
            get
            {
                return base.GetSystemInt32(ServiceUnitItemServiceCompMappingMetadata.ColumnNames.ChartOfAccountIdCost);
            }

            set
            {
                base.SetSystemInt32(ServiceUnitItemServiceCompMappingMetadata.ColumnNames.ChartOfAccountIdCost, value);
            }
        }
        /// <summary>
        /// Maps to ServiceUnitItemServiceCompMapping.SubledgerIdCost
        /// </summary>
        virtual public System.Int32? SubledgerIdCost
        {
            get
            {
                return base.GetSystemInt32(ServiceUnitItemServiceCompMappingMetadata.ColumnNames.SubledgerIdCost);
            }

            set
            {
                base.SetSystemInt32(ServiceUnitItemServiceCompMappingMetadata.ColumnNames.SubledgerIdCost, value);
            }
        }
        /// <summary>
        /// Maps to ServiceUnitItemServiceCompMapping.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(ServiceUnitItemServiceCompMappingMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(ServiceUnitItemServiceCompMappingMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to ServiceUnitItemServiceCompMapping.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(ServiceUnitItemServiceCompMappingMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(ServiceUnitItemServiceCompMappingMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to ServiceUnitItemServiceCompMapping.SRRegistrationType
        /// </summary>
        virtual public System.String SRRegistrationType
        {
            get
            {
                return base.GetSystemString(ServiceUnitItemServiceCompMappingMetadata.ColumnNames.SRRegistrationType);
            }

            set
            {
                base.SetSystemString(ServiceUnitItemServiceCompMappingMetadata.ColumnNames.SRRegistrationType, value);
            }
        }
        /// <summary>
        /// Maps to ServiceUnitItemServiceCompMapping.SRGuarantorIncomeGroup
        /// </summary>
        virtual public System.String SRGuarantorIncomeGroup
        {
            get
            {
                return base.GetSystemString(ServiceUnitItemServiceCompMappingMetadata.ColumnNames.SRGuarantorIncomeGroup);
            }

            set
            {
                base.SetSystemString(ServiceUnitItemServiceCompMappingMetadata.ColumnNames.SRGuarantorIncomeGroup, value);
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
            public esStrings(esServiceUnitItemServiceCompMapping entity)
            {
                this.entity = entity;
            }
            public System.String ServiceUnitID
            {
                get
                {
                    System.String data = entity.ServiceUnitID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ServiceUnitID = null;
                    else entity.ServiceUnitID = Convert.ToString(value);
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
            public System.String TariffComponentID
            {
                get
                {
                    System.String data = entity.TariffComponentID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TariffComponentID = null;
                    else entity.TariffComponentID = Convert.ToString(value);
                }
            }
            public System.String ChartOfAccountIdIncome
            {
                get
                {
                    System.Int32? data = entity.ChartOfAccountIdIncome;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ChartOfAccountIdIncome = null;
                    else entity.ChartOfAccountIdIncome = Convert.ToInt32(value);
                }
            }
            public System.String SubledgerIdIncome
            {
                get
                {
                    System.Int32? data = entity.SubledgerIdIncome;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SubledgerIdIncome = null;
                    else entity.SubledgerIdIncome = Convert.ToInt32(value);
                }
            }
            public System.String ChartOfAccountIdDiscount
            {
                get
                {
                    System.Int32? data = entity.ChartOfAccountIdDiscount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ChartOfAccountIdDiscount = null;
                    else entity.ChartOfAccountIdDiscount = Convert.ToInt32(value);
                }
            }
            public System.String SubledgerIdDiscount
            {
                get
                {
                    System.Int32? data = entity.SubledgerIdDiscount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SubledgerIdDiscount = null;
                    else entity.SubledgerIdDiscount = Convert.ToInt32(value);
                }
            }
            public System.String ChartOfAccountIdCost
            {
                get
                {
                    System.Int32? data = entity.ChartOfAccountIdCost;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ChartOfAccountIdCost = null;
                    else entity.ChartOfAccountIdCost = Convert.ToInt32(value);
                }
            }
            public System.String SubledgerIdCost
            {
                get
                {
                    System.Int32? data = entity.SubledgerIdCost;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SubledgerIdCost = null;
                    else entity.SubledgerIdCost = Convert.ToInt32(value);
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
            public System.String SRRegistrationType
            {
                get
                {
                    System.String data = entity.SRRegistrationType;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRRegistrationType = null;
                    else entity.SRRegistrationType = Convert.ToString(value);
                }
            }
            public System.String SRGuarantorIncomeGroup
            {
                get
                {
                    System.String data = entity.SRGuarantorIncomeGroup;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRGuarantorIncomeGroup = null;
                    else entity.SRGuarantorIncomeGroup = Convert.ToString(value);
                }
            }
            private esServiceUnitItemServiceCompMapping entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esServiceUnitItemServiceCompMappingQuery query)
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
                throw new Exception("esServiceUnitItemServiceCompMapping can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class ServiceUnitItemServiceCompMapping : esServiceUnitItemServiceCompMapping
    {
    }

    [Serializable]
    abstract public class esServiceUnitItemServiceCompMappingQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return ServiceUnitItemServiceCompMappingMetadata.Meta();
            }
        }

        public esQueryItem ServiceUnitID
        {
            get
            {
                return new esQueryItem(this, ServiceUnitItemServiceCompMappingMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
            }
        }

        public esQueryItem ItemID
        {
            get
            {
                return new esQueryItem(this, ServiceUnitItemServiceCompMappingMetadata.ColumnNames.ItemID, esSystemType.String);
            }
        }

        public esQueryItem TariffComponentID
        {
            get
            {
                return new esQueryItem(this, ServiceUnitItemServiceCompMappingMetadata.ColumnNames.TariffComponentID, esSystemType.String);
            }
        }

        public esQueryItem ChartOfAccountIdIncome
        {
            get
            {
                return new esQueryItem(this, ServiceUnitItemServiceCompMappingMetadata.ColumnNames.ChartOfAccountIdIncome, esSystemType.Int32);
            }
        }

        public esQueryItem SubledgerIdIncome
        {
            get
            {
                return new esQueryItem(this, ServiceUnitItemServiceCompMappingMetadata.ColumnNames.SubledgerIdIncome, esSystemType.Int32);
            }
        }

        public esQueryItem ChartOfAccountIdDiscount
        {
            get
            {
                return new esQueryItem(this, ServiceUnitItemServiceCompMappingMetadata.ColumnNames.ChartOfAccountIdDiscount, esSystemType.Int32);
            }
        }

        public esQueryItem SubledgerIdDiscount
        {
            get
            {
                return new esQueryItem(this, ServiceUnitItemServiceCompMappingMetadata.ColumnNames.SubledgerIdDiscount, esSystemType.Int32);
            }
        }

        public esQueryItem ChartOfAccountIdCost
        {
            get
            {
                return new esQueryItem(this, ServiceUnitItemServiceCompMappingMetadata.ColumnNames.ChartOfAccountIdCost, esSystemType.Int32);
            }
        }

        public esQueryItem SubledgerIdCost
        {
            get
            {
                return new esQueryItem(this, ServiceUnitItemServiceCompMappingMetadata.ColumnNames.SubledgerIdCost, esSystemType.Int32);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, ServiceUnitItemServiceCompMappingMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, ServiceUnitItemServiceCompMappingMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem SRRegistrationType
        {
            get
            {
                return new esQueryItem(this, ServiceUnitItemServiceCompMappingMetadata.ColumnNames.SRRegistrationType, esSystemType.String);
            }
        }

        public esQueryItem SRGuarantorIncomeGroup
        {
            get
            {
                return new esQueryItem(this, ServiceUnitItemServiceCompMappingMetadata.ColumnNames.SRGuarantorIncomeGroup, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("ServiceUnitItemServiceCompMappingCollection")]
    public partial class ServiceUnitItemServiceCompMappingCollection : esServiceUnitItemServiceCompMappingCollection, IEnumerable<ServiceUnitItemServiceCompMapping>
    {
        public ServiceUnitItemServiceCompMappingCollection()
        {

        }

        public static implicit operator List<ServiceUnitItemServiceCompMapping>(ServiceUnitItemServiceCompMappingCollection coll)
        {
            List<ServiceUnitItemServiceCompMapping> list = new List<ServiceUnitItemServiceCompMapping>();

            foreach (ServiceUnitItemServiceCompMapping emp in coll)
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
                return ServiceUnitItemServiceCompMappingMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ServiceUnitItemServiceCompMappingQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new ServiceUnitItemServiceCompMapping(row);
        }

        override protected esEntity CreateEntity()
        {
            return new ServiceUnitItemServiceCompMapping();
        }

        #endregion

        [BrowsableAttribute(false)]
        public ServiceUnitItemServiceCompMappingQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ServiceUnitItemServiceCompMappingQuery();
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
        public bool Load(ServiceUnitItemServiceCompMappingQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public ServiceUnitItemServiceCompMapping AddNew()
        {
            ServiceUnitItemServiceCompMapping entity = base.AddNewEntity() as ServiceUnitItemServiceCompMapping;

            return entity;
        }
        public ServiceUnitItemServiceCompMapping FindByPrimaryKey(String serviceUnitID, String itemID, String tariffComponentID, String sRRegistrationType, String sRGuarantorIncomeGroup)
        {
            return base.FindByPrimaryKey(serviceUnitID, itemID, tariffComponentID, sRRegistrationType, sRGuarantorIncomeGroup) as ServiceUnitItemServiceCompMapping;
        }

        #region IEnumerable< ServiceUnitItemServiceCompMapping> Members

        IEnumerator<ServiceUnitItemServiceCompMapping> IEnumerable<ServiceUnitItemServiceCompMapping>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as ServiceUnitItemServiceCompMapping;
            }
        }

        #endregion

        private ServiceUnitItemServiceCompMappingQuery query;
    }


    /// <summary>
    /// Encapsulates the 'ServiceUnitItemServiceCompMapping' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("ServiceUnitItemServiceCompMapping ({ServiceUnitID, ItemID, TariffComponentID, SRRegistrationType, SRGuarantorIncomeGroup})")]
    [Serializable]
    public partial class ServiceUnitItemServiceCompMapping : esServiceUnitItemServiceCompMapping
    {
        public ServiceUnitItemServiceCompMapping()
        {
        }

        public ServiceUnitItemServiceCompMapping(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return ServiceUnitItemServiceCompMappingMetadata.Meta();
            }
        }

        override protected esServiceUnitItemServiceCompMappingQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ServiceUnitItemServiceCompMappingQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public ServiceUnitItemServiceCompMappingQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ServiceUnitItemServiceCompMappingQuery();
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
        public bool Load(ServiceUnitItemServiceCompMappingQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private ServiceUnitItemServiceCompMappingQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class ServiceUnitItemServiceCompMappingQuery : esServiceUnitItemServiceCompMappingQuery
    {
        public ServiceUnitItemServiceCompMappingQuery()
        {

        }

        public ServiceUnitItemServiceCompMappingQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "ServiceUnitItemServiceCompMappingQuery";
        }
    }

    [Serializable]
    public partial class ServiceUnitItemServiceCompMappingMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected ServiceUnitItemServiceCompMappingMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(ServiceUnitItemServiceCompMappingMetadata.ColumnNames.ServiceUnitID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = ServiceUnitItemServiceCompMappingMetadata.PropertyNames.ServiceUnitID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(ServiceUnitItemServiceCompMappingMetadata.ColumnNames.ItemID, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = ServiceUnitItemServiceCompMappingMetadata.PropertyNames.ItemID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(ServiceUnitItemServiceCompMappingMetadata.ColumnNames.TariffComponentID, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = ServiceUnitItemServiceCompMappingMetadata.PropertyNames.TariffComponentID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 2;
            _columns.Add(c);

            c = new esColumnMetadata(ServiceUnitItemServiceCompMappingMetadata.ColumnNames.ChartOfAccountIdIncome, 3, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = ServiceUnitItemServiceCompMappingMetadata.PropertyNames.ChartOfAccountIdIncome;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ServiceUnitItemServiceCompMappingMetadata.ColumnNames.SubledgerIdIncome, 4, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = ServiceUnitItemServiceCompMappingMetadata.PropertyNames.SubledgerIdIncome;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ServiceUnitItemServiceCompMappingMetadata.ColumnNames.ChartOfAccountIdDiscount, 5, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = ServiceUnitItemServiceCompMappingMetadata.PropertyNames.ChartOfAccountIdDiscount;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ServiceUnitItemServiceCompMappingMetadata.ColumnNames.SubledgerIdDiscount, 6, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = ServiceUnitItemServiceCompMappingMetadata.PropertyNames.SubledgerIdDiscount;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ServiceUnitItemServiceCompMappingMetadata.ColumnNames.ChartOfAccountIdCost, 7, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = ServiceUnitItemServiceCompMappingMetadata.PropertyNames.ChartOfAccountIdCost;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ServiceUnitItemServiceCompMappingMetadata.ColumnNames.SubledgerIdCost, 8, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = ServiceUnitItemServiceCompMappingMetadata.PropertyNames.SubledgerIdCost;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ServiceUnitItemServiceCompMappingMetadata.ColumnNames.LastUpdateDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ServiceUnitItemServiceCompMappingMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ServiceUnitItemServiceCompMappingMetadata.ColumnNames.LastUpdateByUserID, 10, typeof(System.String), esSystemType.String);
            c.PropertyName = ServiceUnitItemServiceCompMappingMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 40;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ServiceUnitItemServiceCompMappingMetadata.ColumnNames.SRRegistrationType, 11, typeof(System.String), esSystemType.String);
            c.PropertyName = ServiceUnitItemServiceCompMappingMetadata.PropertyNames.SRRegistrationType;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(ServiceUnitItemServiceCompMappingMetadata.ColumnNames.SRGuarantorIncomeGroup, 12, typeof(System.String), esSystemType.String);
            c.PropertyName = ServiceUnitItemServiceCompMappingMetadata.PropertyNames.SRGuarantorIncomeGroup;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 50;
            c.HasDefault = true;
            c.Default = @"('001')";
            _columns.Add(c);


        }
        #endregion

        static public ServiceUnitItemServiceCompMappingMetadata Meta()
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
            public const string ServiceUnitID = "ServiceUnitID";
            public const string ItemID = "ItemID";
            public const string TariffComponentID = "TariffComponentID";
            public const string ChartOfAccountIdIncome = "ChartOfAccountIdIncome";
            public const string SubledgerIdIncome = "SubledgerIdIncome";
            public const string ChartOfAccountIdDiscount = "ChartOfAccountIdDiscount";
            public const string SubledgerIdDiscount = "SubledgerIdDiscount";
            public const string ChartOfAccountIdCost = "ChartOfAccountIdCost";
            public const string SubledgerIdCost = "SubledgerIdCost";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string SRRegistrationType = "SRRegistrationType";
            public const string SRGuarantorIncomeGroup = "SRGuarantorIncomeGroup";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string ServiceUnitID = "ServiceUnitID";
            public const string ItemID = "ItemID";
            public const string TariffComponentID = "TariffComponentID";
            public const string ChartOfAccountIdIncome = "ChartOfAccountIdIncome";
            public const string SubledgerIdIncome = "SubledgerIdIncome";
            public const string ChartOfAccountIdDiscount = "ChartOfAccountIdDiscount";
            public const string SubledgerIdDiscount = "SubledgerIdDiscount";
            public const string ChartOfAccountIdCost = "ChartOfAccountIdCost";
            public const string SubledgerIdCost = "SubledgerIdCost";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string SRRegistrationType = "SRRegistrationType";
            public const string SRGuarantorIncomeGroup = "SRGuarantorIncomeGroup";
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
            lock (typeof(ServiceUnitItemServiceCompMappingMetadata))
            {
                if (ServiceUnitItemServiceCompMappingMetadata.mapDelegates == null)
                {
                    ServiceUnitItemServiceCompMappingMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (ServiceUnitItemServiceCompMappingMetadata.meta == null)
                {
                    ServiceUnitItemServiceCompMappingMetadata.meta = new ServiceUnitItemServiceCompMappingMetadata();
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

                meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("TariffComponentID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ChartOfAccountIdIncome", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("SubledgerIdIncome", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("ChartOfAccountIdDiscount", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("SubledgerIdDiscount", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("ChartOfAccountIdCost", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("SubledgerIdCost", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRRegistrationType", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRGuarantorIncomeGroup", new esTypeMap("varchar", "System.String"));


                meta.Source = "ServiceUnitItemServiceCompMapping";
                meta.Destination = "ServiceUnitItemServiceCompMapping";
                meta.spInsert = "proc_ServiceUnitItemServiceCompMappingInsert";
                meta.spUpdate = "proc_ServiceUnitItemServiceCompMappingUpdate";
                meta.spDelete = "proc_ServiceUnitItemServiceCompMappingDelete";
                meta.spLoadAll = "proc_ServiceUnitItemServiceCompMappingLoadAll";
                meta.spLoadByPrimaryKey = "proc_ServiceUnitItemServiceCompMappingLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private ServiceUnitItemServiceCompMappingMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
