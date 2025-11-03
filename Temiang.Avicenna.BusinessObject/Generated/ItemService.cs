/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 1/9/2019 2:28:14 PM
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
    abstract public class esItemServiceCollection : esEntityCollectionWAuditLog
    {
        public esItemServiceCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "ItemServiceCollection";
        }

        #region Query Logic
        protected void InitQuery(esItemServiceQuery query)
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
            this.InitQuery(query as esItemServiceQuery);
        }
        #endregion

        virtual public ItemService DetachEntity(ItemService entity)
        {
            return base.DetachEntity(entity) as ItemService;
        }

        virtual public ItemService AttachEntity(ItemService entity)
        {
            return base.AttachEntity(entity) as ItemService;
        }

        virtual public void Combine(ItemServiceCollection collection)
        {
            base.Combine(collection);
        }

        new public ItemService this[int index]
        {
            get
            {
                return base[index] as ItemService;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(ItemService);
        }
    }

    [Serializable]
    abstract public class esItemService : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esItemServiceQuery GetDynamicQuery()
        {
            return null;
        }

        public esItemService()
        {
        }

        public esItemService(DataRow row)
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
            esItemServiceQuery query = this.GetDynamicQuery();
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
                        case "SRItemUnit": this.str.SRItemUnit = (string)value; break;
                        case "IsPrimaryService": this.str.IsPrimaryService = (string)value; break;
                        case "IsAdminCalculation": this.str.IsAdminCalculation = (string)value; break;
                        case "IsAllowVariable": this.str.IsAllowVariable = (string)value; break;
                        case "IsAllowCito": this.str.IsAllowCito = (string)value; break;
                        case "IsAllowDiscount": this.str.IsAllowDiscount = (string)value; break;
                        case "IsPrintWithDoctorName": this.str.IsPrintWithDoctorName = (string)value; break;
                        case "IsAssetUtilization": this.str.IsAssetUtilization = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "PremiAmount": this.str.PremiAmount = (string)value; break;
                        case "Premi2Amount": this.str.Premi2Amount = (string)value; break;
                        case "ProductionServicesPercentage": this.str.ProductionServicesPercentage = (string)value; break;
                        case "TogethernessPercentage": this.str.TogethernessPercentage = (string)value; break;
                        case "ProductionServicesPercentage2": this.str.ProductionServicesPercentage2 = (string)value; break;
                        case "ItemRelatedID": this.str.ItemRelatedID = (string)value; break;
                        case "QtyDivider": this.str.QtyDivider = (string)value; break;
                        case "RlMasterReportItemID": this.str.RlMasterReportItemID = (string)value; break;
                        case "IsCitoFromStandardReference": this.str.IsCitoFromStandardReference = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "IsPrimaryService":

                            if (value == null || value is System.Boolean)
                                this.IsPrimaryService = (System.Boolean?)value;
                            break;
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
                        case "PremiAmount":

                            if (value == null || value is System.Decimal)
                                this.PremiAmount = (System.Decimal?)value;
                            break;
                        case "Premi2Amount":

                            if (value == null || value is System.Decimal)
                                this.Premi2Amount = (System.Decimal?)value;
                            break;
                        case "ProductionServicesPercentage":

                            if (value == null || value is System.Decimal)
                                this.ProductionServicesPercentage = (System.Decimal?)value;
                            break;
                        case "TogethernessPercentage":

                            if (value == null || value is System.Decimal)
                                this.TogethernessPercentage = (System.Decimal?)value;
                            break;
                        case "ProductionServicesPercentage2":

                            if (value == null || value is System.Decimal)
                                this.ProductionServicesPercentage2 = (System.Decimal?)value;
                            break;
                        case "QtyDivider":

                            if (value == null || value is System.Decimal)
                                this.QtyDivider = (System.Decimal?)value;
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
        /// Maps to ItemService.ItemID
        /// </summary>
        virtual public System.String ItemID
        {
            get
            {
                return base.GetSystemString(ItemServiceMetadata.ColumnNames.ItemID);
            }

            set
            {
                base.SetSystemString(ItemServiceMetadata.ColumnNames.ItemID, value);
            }
        }
        /// <summary>
        /// Maps to ItemService.ReportRLID
        /// </summary>
        virtual public System.String ReportRLID
        {
            get
            {
                return base.GetSystemString(ItemServiceMetadata.ColumnNames.ReportRLID);
            }

            set
            {
                base.SetSystemString(ItemServiceMetadata.ColumnNames.ReportRLID, value);
            }
        }
        /// <summary>
        /// Maps to ItemService.SRItemUnit
        /// </summary>
        virtual public System.String SRItemUnit
        {
            get
            {
                return base.GetSystemString(ItemServiceMetadata.ColumnNames.SRItemUnit);
            }

            set
            {
                base.SetSystemString(ItemServiceMetadata.ColumnNames.SRItemUnit, value);
            }
        }
        /// <summary>
        /// Maps to ItemService.IsPrimaryService
        /// </summary>
        virtual public System.Boolean? IsPrimaryService
        {
            get
            {
                return base.GetSystemBoolean(ItemServiceMetadata.ColumnNames.IsPrimaryService);
            }

            set
            {
                base.SetSystemBoolean(ItemServiceMetadata.ColumnNames.IsPrimaryService, value);
            }
        }
        /// <summary>
        /// Maps to ItemService.IsAdminCalculation
        /// </summary>
        virtual public System.Boolean? IsAdminCalculation
        {
            get
            {
                return base.GetSystemBoolean(ItemServiceMetadata.ColumnNames.IsAdminCalculation);
            }

            set
            {
                base.SetSystemBoolean(ItemServiceMetadata.ColumnNames.IsAdminCalculation, value);
            }
        }
        /// <summary>
        /// Maps to ItemService.IsAllowVariable
        /// </summary>
        virtual public System.Boolean? IsAllowVariable
        {
            get
            {
                return base.GetSystemBoolean(ItemServiceMetadata.ColumnNames.IsAllowVariable);
            }

            set
            {
                base.SetSystemBoolean(ItemServiceMetadata.ColumnNames.IsAllowVariable, value);
            }
        }
        /// <summary>
        /// Maps to ItemService.IsAllowCito
        /// </summary>
        virtual public System.Boolean? IsAllowCito
        {
            get
            {
                return base.GetSystemBoolean(ItemServiceMetadata.ColumnNames.IsAllowCito);
            }

            set
            {
                base.SetSystemBoolean(ItemServiceMetadata.ColumnNames.IsAllowCito, value);
            }
        }
        /// <summary>
        /// Maps to ItemService.IsAllowDiscount
        /// </summary>
        virtual public System.Boolean? IsAllowDiscount
        {
            get
            {
                return base.GetSystemBoolean(ItemServiceMetadata.ColumnNames.IsAllowDiscount);
            }

            set
            {
                base.SetSystemBoolean(ItemServiceMetadata.ColumnNames.IsAllowDiscount, value);
            }
        }
        /// <summary>
        /// Maps to ItemService.IsPrintWithDoctorName
        /// </summary>
        virtual public System.Boolean? IsPrintWithDoctorName
        {
            get
            {
                return base.GetSystemBoolean(ItemServiceMetadata.ColumnNames.IsPrintWithDoctorName);
            }

            set
            {
                base.SetSystemBoolean(ItemServiceMetadata.ColumnNames.IsPrintWithDoctorName, value);
            }
        }
        /// <summary>
        /// Maps to ItemService.IsAssetUtilization
        /// </summary>
        virtual public System.Boolean? IsAssetUtilization
        {
            get
            {
                return base.GetSystemBoolean(ItemServiceMetadata.ColumnNames.IsAssetUtilization);
            }

            set
            {
                base.SetSystemBoolean(ItemServiceMetadata.ColumnNames.IsAssetUtilization, value);
            }
        }
        /// <summary>
        /// Maps to ItemService.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(ItemServiceMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(ItemServiceMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to ItemService.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(ItemServiceMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(ItemServiceMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to ItemService.PremiAmount
        /// </summary>
        virtual public System.Decimal? PremiAmount
        {
            get
            {
                return base.GetSystemDecimal(ItemServiceMetadata.ColumnNames.PremiAmount);
            }

            set
            {
                base.SetSystemDecimal(ItemServiceMetadata.ColumnNames.PremiAmount, value);
            }
        }
        /// <summary>
        /// Maps to ItemService.Premi2Amount
        /// </summary>
        virtual public System.Decimal? Premi2Amount
        {
            get
            {
                return base.GetSystemDecimal(ItemServiceMetadata.ColumnNames.Premi2Amount);
            }

            set
            {
                base.SetSystemDecimal(ItemServiceMetadata.ColumnNames.Premi2Amount, value);
            }
        }
        /// <summary>
        /// Maps to ItemService.ProductionServicesPercentage
        /// </summary>
        virtual public System.Decimal? ProductionServicesPercentage
        {
            get
            {
                return base.GetSystemDecimal(ItemServiceMetadata.ColumnNames.ProductionServicesPercentage);
            }

            set
            {
                base.SetSystemDecimal(ItemServiceMetadata.ColumnNames.ProductionServicesPercentage, value);
            }
        }
        /// <summary>
        /// Maps to ItemService.TogethernessPercentage
        /// </summary>
        virtual public System.Decimal? TogethernessPercentage
        {
            get
            {
                return base.GetSystemDecimal(ItemServiceMetadata.ColumnNames.TogethernessPercentage);
            }

            set
            {
                base.SetSystemDecimal(ItemServiceMetadata.ColumnNames.TogethernessPercentage, value);
            }
        }
        /// <summary>
        /// Maps to ItemService.ProductionServicesPercentage2
        /// </summary>
        virtual public System.Decimal? ProductionServicesPercentage2
        {
            get
            {
                return base.GetSystemDecimal(ItemServiceMetadata.ColumnNames.ProductionServicesPercentage2);
            }

            set
            {
                base.SetSystemDecimal(ItemServiceMetadata.ColumnNames.ProductionServicesPercentage2, value);
            }
        }
        /// <summary>
        /// Maps to ItemService.ItemRelatedID
        /// </summary>
        virtual public System.String ItemRelatedID
        {
            get
            {
                return base.GetSystemString(ItemServiceMetadata.ColumnNames.ItemRelatedID);
            }

            set
            {
                base.SetSystemString(ItemServiceMetadata.ColumnNames.ItemRelatedID, value);
            }
        }
        /// <summary>
        /// Maps to ItemService.QtyDivider
        /// </summary>
        virtual public System.Decimal? QtyDivider
        {
            get
            {
                return base.GetSystemDecimal(ItemServiceMetadata.ColumnNames.QtyDivider);
            }

            set
            {
                base.SetSystemDecimal(ItemServiceMetadata.ColumnNames.QtyDivider, value);
            }
        }
        /// <summary>
        /// Maps to ItemService.RlMasterReportItemID
        /// </summary>
        virtual public System.Int32? RlMasterReportItemID
        {
            get
            {
                return base.GetSystemInt32(ItemServiceMetadata.ColumnNames.RlMasterReportItemID);
            }

            set
            {
                base.SetSystemInt32(ItemServiceMetadata.ColumnNames.RlMasterReportItemID, value);
            }
        }
        /// <summary>
        /// Maps to ItemService.IsCitoFromStandardReference
        /// </summary>
        virtual public System.Boolean? IsCitoFromStandardReference
        {
            get
            {
                return base.GetSystemBoolean(ItemServiceMetadata.ColumnNames.IsCitoFromStandardReference);
            }

            set
            {
                base.SetSystemBoolean(ItemServiceMetadata.ColumnNames.IsCitoFromStandardReference, value);
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
            public esStrings(esItemService entity)
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
            public System.String SRItemUnit
            {
                get
                {
                    System.String data = entity.SRItemUnit;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRItemUnit = null;
                    else entity.SRItemUnit = Convert.ToString(value);
                }
            }
            public System.String IsPrimaryService
            {
                get
                {
                    System.Boolean? data = entity.IsPrimaryService;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsPrimaryService = null;
                    else entity.IsPrimaryService = Convert.ToBoolean(value);
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
            public System.String PremiAmount
            {
                get
                {
                    System.Decimal? data = entity.PremiAmount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PremiAmount = null;
                    else entity.PremiAmount = Convert.ToDecimal(value);
                }
            }
            public System.String Premi2Amount
            {
                get
                {
                    System.Decimal? data = entity.Premi2Amount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Premi2Amount = null;
                    else entity.Premi2Amount = Convert.ToDecimal(value);
                }
            }
            public System.String ProductionServicesPercentage
            {
                get
                {
                    System.Decimal? data = entity.ProductionServicesPercentage;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ProductionServicesPercentage = null;
                    else entity.ProductionServicesPercentage = Convert.ToDecimal(value);
                }
            }
            public System.String TogethernessPercentage
            {
                get
                {
                    System.Decimal? data = entity.TogethernessPercentage;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TogethernessPercentage = null;
                    else entity.TogethernessPercentage = Convert.ToDecimal(value);
                }
            }
            public System.String ProductionServicesPercentage2
            {
                get
                {
                    System.Decimal? data = entity.ProductionServicesPercentage2;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ProductionServicesPercentage2 = null;
                    else entity.ProductionServicesPercentage2 = Convert.ToDecimal(value);
                }
            }
            public System.String ItemRelatedID
            {
                get
                {
                    System.String data = entity.ItemRelatedID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ItemRelatedID = null;
                    else entity.ItemRelatedID = Convert.ToString(value);
                }
            }
            public System.String QtyDivider
            {
                get
                {
                    System.Decimal? data = entity.QtyDivider;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.QtyDivider = null;
                    else entity.QtyDivider = Convert.ToDecimal(value);
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
            private esItemService entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esItemServiceQuery query)
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
                throw new Exception("esItemService can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class ItemService : esItemService
    {
    }

    [Serializable]
    abstract public class esItemServiceQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return ItemServiceMetadata.Meta();
            }
        }

        public esQueryItem ItemID
        {
            get
            {
                return new esQueryItem(this, ItemServiceMetadata.ColumnNames.ItemID, esSystemType.String);
            }
        }

        public esQueryItem ReportRLID
        {
            get
            {
                return new esQueryItem(this, ItemServiceMetadata.ColumnNames.ReportRLID, esSystemType.String);
            }
        }

        public esQueryItem SRItemUnit
        {
            get
            {
                return new esQueryItem(this, ItemServiceMetadata.ColumnNames.SRItemUnit, esSystemType.String);
            }
        }

        public esQueryItem IsPrimaryService
        {
            get
            {
                return new esQueryItem(this, ItemServiceMetadata.ColumnNames.IsPrimaryService, esSystemType.Boolean);
            }
        }

        public esQueryItem IsAdminCalculation
        {
            get
            {
                return new esQueryItem(this, ItemServiceMetadata.ColumnNames.IsAdminCalculation, esSystemType.Boolean);
            }
        }

        public esQueryItem IsAllowVariable
        {
            get
            {
                return new esQueryItem(this, ItemServiceMetadata.ColumnNames.IsAllowVariable, esSystemType.Boolean);
            }
        }

        public esQueryItem IsAllowCito
        {
            get
            {
                return new esQueryItem(this, ItemServiceMetadata.ColumnNames.IsAllowCito, esSystemType.Boolean);
            }
        }

        public esQueryItem IsAllowDiscount
        {
            get
            {
                return new esQueryItem(this, ItemServiceMetadata.ColumnNames.IsAllowDiscount, esSystemType.Boolean);
            }
        }

        public esQueryItem IsPrintWithDoctorName
        {
            get
            {
                return new esQueryItem(this, ItemServiceMetadata.ColumnNames.IsPrintWithDoctorName, esSystemType.Boolean);
            }
        }

        public esQueryItem IsAssetUtilization
        {
            get
            {
                return new esQueryItem(this, ItemServiceMetadata.ColumnNames.IsAssetUtilization, esSystemType.Boolean);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, ItemServiceMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, ItemServiceMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem PremiAmount
        {
            get
            {
                return new esQueryItem(this, ItemServiceMetadata.ColumnNames.PremiAmount, esSystemType.Decimal);
            }
        }

        public esQueryItem Premi2Amount
        {
            get
            {
                return new esQueryItem(this, ItemServiceMetadata.ColumnNames.Premi2Amount, esSystemType.Decimal);
            }
        }

        public esQueryItem ProductionServicesPercentage
        {
            get
            {
                return new esQueryItem(this, ItemServiceMetadata.ColumnNames.ProductionServicesPercentage, esSystemType.Decimal);
            }
        }

        public esQueryItem TogethernessPercentage
        {
            get
            {
                return new esQueryItem(this, ItemServiceMetadata.ColumnNames.TogethernessPercentage, esSystemType.Decimal);
            }
        }

        public esQueryItem ProductionServicesPercentage2
        {
            get
            {
                return new esQueryItem(this, ItemServiceMetadata.ColumnNames.ProductionServicesPercentage2, esSystemType.Decimal);
            }
        }

        public esQueryItem ItemRelatedID
        {
            get
            {
                return new esQueryItem(this, ItemServiceMetadata.ColumnNames.ItemRelatedID, esSystemType.String);
            }
        }

        public esQueryItem QtyDivider
        {
            get
            {
                return new esQueryItem(this, ItemServiceMetadata.ColumnNames.QtyDivider, esSystemType.Decimal);
            }
        }

        public esQueryItem RlMasterReportItemID
        {
            get
            {
                return new esQueryItem(this, ItemServiceMetadata.ColumnNames.RlMasterReportItemID, esSystemType.Int32);
            }
        }

        public esQueryItem IsCitoFromStandardReference
        {
            get
            {
                return new esQueryItem(this, ItemServiceMetadata.ColumnNames.IsCitoFromStandardReference, esSystemType.Boolean);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("ItemServiceCollection")]
    public partial class ItemServiceCollection : esItemServiceCollection, IEnumerable<ItemService>
    {
        public ItemServiceCollection()
        {

        }

        public static implicit operator List<ItemService>(ItemServiceCollection coll)
        {
            List<ItemService> list = new List<ItemService>();

            foreach (ItemService emp in coll)
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
                return ItemServiceMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ItemServiceQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new ItemService(row);
        }

        override protected esEntity CreateEntity()
        {
            return new ItemService();
        }

        #endregion

        [BrowsableAttribute(false)]
        public ItemServiceQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ItemServiceQuery();
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
        public bool Load(ItemServiceQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public ItemService AddNew()
        {
            ItemService entity = base.AddNewEntity() as ItemService;

            return entity;
        }
        public ItemService FindByPrimaryKey(String itemID)
        {
            return base.FindByPrimaryKey(itemID) as ItemService;
        }

        #region IEnumerable< ItemService> Members

        IEnumerator<ItemService> IEnumerable<ItemService>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as ItemService;
            }
        }

        #endregion

        private ItemServiceQuery query;
    }


    /// <summary>
    /// Encapsulates the 'ItemService' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("ItemService ({ItemID})")]
    [Serializable]
    public partial class ItemService : esItemService
    {
        public ItemService()
        {
        }

        public ItemService(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return ItemServiceMetadata.Meta();
            }
        }

        override protected esItemServiceQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ItemServiceQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public ItemServiceQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ItemServiceQuery();
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
        public bool Load(ItemServiceQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private ItemServiceQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class ItemServiceQuery : esItemServiceQuery
    {
        public ItemServiceQuery()
        {

        }

        public ItemServiceQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "ItemServiceQuery";
        }
    }

    [Serializable]
    public partial class ItemServiceMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected ItemServiceMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(ItemServiceMetadata.ColumnNames.ItemID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemServiceMetadata.PropertyNames.ItemID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(ItemServiceMetadata.ColumnNames.ReportRLID, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemServiceMetadata.PropertyNames.ReportRLID;
            c.CharacterMaxLength = 10;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(ItemServiceMetadata.ColumnNames.SRItemUnit, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemServiceMetadata.PropertyNames.SRItemUnit;
            c.CharacterMaxLength = 20;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(ItemServiceMetadata.ColumnNames.IsPrimaryService, 3, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ItemServiceMetadata.PropertyNames.IsPrimaryService;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemServiceMetadata.ColumnNames.IsAdminCalculation, 4, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ItemServiceMetadata.PropertyNames.IsAdminCalculation;
            c.HasDefault = true;
            c.Default = @"((1))";
            _columns.Add(c);

            c = new esColumnMetadata(ItemServiceMetadata.ColumnNames.IsAllowVariable, 5, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ItemServiceMetadata.PropertyNames.IsAllowVariable;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(ItemServiceMetadata.ColumnNames.IsAllowCito, 6, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ItemServiceMetadata.PropertyNames.IsAllowCito;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(ItemServiceMetadata.ColumnNames.IsAllowDiscount, 7, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ItemServiceMetadata.PropertyNames.IsAllowDiscount;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(ItemServiceMetadata.ColumnNames.IsPrintWithDoctorName, 8, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ItemServiceMetadata.PropertyNames.IsPrintWithDoctorName;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(ItemServiceMetadata.ColumnNames.IsAssetUtilization, 9, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ItemServiceMetadata.PropertyNames.IsAssetUtilization;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(ItemServiceMetadata.ColumnNames.LastUpdateDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ItemServiceMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemServiceMetadata.ColumnNames.LastUpdateByUserID, 11, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemServiceMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 40;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemServiceMetadata.ColumnNames.PremiAmount, 12, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ItemServiceMetadata.PropertyNames.PremiAmount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemServiceMetadata.ColumnNames.Premi2Amount, 13, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ItemServiceMetadata.PropertyNames.Premi2Amount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemServiceMetadata.ColumnNames.ProductionServicesPercentage, 14, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ItemServiceMetadata.PropertyNames.ProductionServicesPercentage;
            c.NumericPrecision = 6;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemServiceMetadata.ColumnNames.TogethernessPercentage, 15, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ItemServiceMetadata.PropertyNames.TogethernessPercentage;
            c.NumericPrecision = 6;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemServiceMetadata.ColumnNames.ProductionServicesPercentage2, 16, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ItemServiceMetadata.PropertyNames.ProductionServicesPercentage2;
            c.NumericPrecision = 6;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemServiceMetadata.ColumnNames.ItemRelatedID, 17, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemServiceMetadata.PropertyNames.ItemRelatedID;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemServiceMetadata.ColumnNames.QtyDivider, 18, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ItemServiceMetadata.PropertyNames.QtyDivider;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemServiceMetadata.ColumnNames.RlMasterReportItemID, 19, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = ItemServiceMetadata.PropertyNames.RlMasterReportItemID;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemServiceMetadata.ColumnNames.IsCitoFromStandardReference, 20, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ItemServiceMetadata.PropertyNames.IsCitoFromStandardReference;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public ItemServiceMetadata Meta()
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
            public const string SRItemUnit = "SRItemUnit";
            public const string IsPrimaryService = "IsPrimaryService";
            public const string IsAdminCalculation = "IsAdminCalculation";
            public const string IsAllowVariable = "IsAllowVariable";
            public const string IsAllowCito = "IsAllowCito";
            public const string IsAllowDiscount = "IsAllowDiscount";
            public const string IsPrintWithDoctorName = "IsPrintWithDoctorName";
            public const string IsAssetUtilization = "IsAssetUtilization";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string PremiAmount = "PremiAmount";
            public const string Premi2Amount = "Premi2Amount";
            public const string ProductionServicesPercentage = "ProductionServicesPercentage";
            public const string TogethernessPercentage = "TogethernessPercentage";
            public const string ProductionServicesPercentage2 = "ProductionServicesPercentage2";
            public const string ItemRelatedID = "ItemRelatedID";
            public const string QtyDivider = "QtyDivider";
            public const string RlMasterReportItemID = "RlMasterReportItemID";
            public const string IsCitoFromStandardReference = "IsCitoFromStandardReference";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string ItemID = "ItemID";
            public const string ReportRLID = "ReportRLID";
            public const string SRItemUnit = "SRItemUnit";
            public const string IsPrimaryService = "IsPrimaryService";
            public const string IsAdminCalculation = "IsAdminCalculation";
            public const string IsAllowVariable = "IsAllowVariable";
            public const string IsAllowCito = "IsAllowCito";
            public const string IsAllowDiscount = "IsAllowDiscount";
            public const string IsPrintWithDoctorName = "IsPrintWithDoctorName";
            public const string IsAssetUtilization = "IsAssetUtilization";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string PremiAmount = "PremiAmount";
            public const string Premi2Amount = "Premi2Amount";
            public const string ProductionServicesPercentage = "ProductionServicesPercentage";
            public const string TogethernessPercentage = "TogethernessPercentage";
            public const string ProductionServicesPercentage2 = "ProductionServicesPercentage2";
            public const string ItemRelatedID = "ItemRelatedID";
            public const string QtyDivider = "QtyDivider";
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
            lock (typeof(ItemServiceMetadata))
            {
                if (ItemServiceMetadata.mapDelegates == null)
                {
                    ItemServiceMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (ItemServiceMetadata.meta == null)
                {
                    ItemServiceMetadata.meta = new ItemServiceMetadata();
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
                meta.AddTypeMap("SRItemUnit", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsPrimaryService", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsAdminCalculation", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsAllowVariable", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsAllowCito", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsAllowDiscount", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsPrintWithDoctorName", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsAssetUtilization", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("PremiAmount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("Premi2Amount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("ProductionServicesPercentage", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("TogethernessPercentage", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("ProductionServicesPercentage2", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("ItemRelatedID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("QtyDivider", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("RlMasterReportItemID", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("IsCitoFromStandardReference", new esTypeMap("bit", "System.Boolean"));


                meta.Source = "ItemService";
                meta.Destination = "ItemService";
                meta.spInsert = "proc_ItemServiceInsert";
                meta.spUpdate = "proc_ItemServiceUpdate";
                meta.spDelete = "proc_ItemServiceDelete";
                meta.spLoadAll = "proc_ItemServiceLoadAll";
                meta.spLoadByPrimaryKey = "proc_ItemServiceLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private ItemServiceMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
