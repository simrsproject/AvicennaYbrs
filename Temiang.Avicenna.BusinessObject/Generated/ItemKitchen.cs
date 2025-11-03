/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/3/2016 2:30:19 PM
===============================================================================
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.ComponentModel;
using System.Xml.Serialization;


using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;



namespace Temiang.Avicenna.BusinessObject
{

    [Serializable]
    abstract public class esItemKitchenCollection : esEntityCollectionWAuditLog
    {
        public esItemKitchenCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "ItemKitchenCollection";
        }

        #region Query Logic
        protected void InitQuery(esItemKitchenQuery query)
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
            this.InitQuery(query as esItemKitchenQuery);
        }
        #endregion

        virtual public ItemKitchen DetachEntity(ItemKitchen entity)
        {
            return base.DetachEntity(entity) as ItemKitchen;
        }

        virtual public ItemKitchen AttachEntity(ItemKitchen entity)
        {
            return base.AttachEntity(entity) as ItemKitchen;
        }

        virtual public void Combine(ItemKitchenCollection collection)
        {
            base.Combine(collection);
        }

        new public ItemKitchen this[int index]
        {
            get
            {
                return base[index] as ItemKitchen;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(ItemKitchen);
        }
    }



    [Serializable]
    abstract public class esItemKitchen : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esItemKitchenQuery GetDynamicQuery()
        {
            return null;
        }

        public esItemKitchen()
        {

        }

        public esItemKitchen(DataRow row)
            : base(row)
        {

        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(System.String itemID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(itemID);
            else
                return LoadByPrimaryKeyStoredProcedure(itemID);
        }

        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String itemID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(itemID);
            else
                return LoadByPrimaryKeyStoredProcedure(itemID);
        }

        private bool LoadByPrimaryKeyDynamic(System.String itemID)
        {
            esItemKitchenQuery query = this.GetDynamicQuery();
            query.Where(query.ItemID == itemID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(System.String itemID)
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
                        case "ABCClass": this.str.ABCClass = (string)value; break;
                        case "BrandName": this.str.BrandName = (string)value; break;
                        case "SRItemUnit": this.str.SRItemUnit = (string)value; break;
                        case "SRPurchaseUnit": this.str.SRPurchaseUnit = (string)value; break;
                        case "ConversionFactor": this.str.ConversionFactor = (string)value; break;
                        case "IsInventoryItem": this.str.IsInventoryItem = (string)value; break;
                        case "IsControlExpired": this.str.IsControlExpired = (string)value; break;
                        case "PriceInPurchaseUnit": this.str.PriceInPurchaseUnit = (string)value; break;
                        case "PriceInBaseUnit": this.str.PriceInBaseUnit = (string)value; break;
                        case "PriceInBasedUnitWVat": this.str.PriceInBasedUnitWVat = (string)value; break;
                        case "HighestPriceInBasedUnit": this.str.HighestPriceInBasedUnit = (string)value; break;
                        case "LastPriceInBaseUnit": this.str.LastPriceInBaseUnit = (string)value; break;
                        case "PriceWVat": this.str.PriceWVat = (string)value; break;
                        case "CostPrice": this.str.CostPrice = (string)value; break;
                        case "PurchaseDiscount1": this.str.PurchaseDiscount1 = (string)value; break;
                        case "PurchaseDiscount2": this.str.PurchaseDiscount2 = (string)value; break;
                        case "SafetyStock": this.str.SafetyStock = (string)value; break;
                        case "SafetyTime": this.str.SafetyTime = (string)value; break;
                        case "LeadTime": this.str.LeadTime = (string)value; break;
                        case "TolerancePercentage": this.str.TolerancePercentage = (string)value; break;
                        case "Barcode": this.str.Barcode = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "IsSalesAvailable": this.str.IsSalesAvailable = (string)value; break;
                        case "SalesFixedPrice": this.str.SalesFixedPrice = (string)value; break;
                        case "MarginPercentage": this.str.MarginPercentage = (string)value; break;
                        case "MarginID": this.str.MarginID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "ConversionFactor":

                            if (value == null || value is System.Decimal)
                                this.ConversionFactor = (System.Decimal?)value;
                            break;

                        case "IsInventoryItem":

                            if (value == null || value is System.Boolean)
                                this.IsInventoryItem = (System.Boolean?)value;
                            break;

                        case "IsControlExpired":

                            if (value == null || value is System.Boolean)
                                this.IsControlExpired = (System.Boolean?)value;
                            break;

                        case "PriceInPurchaseUnit":

                            if (value == null || value is System.Decimal)
                                this.PriceInPurchaseUnit = (System.Decimal?)value;
                            break;

                        case "PriceInBaseUnit":

                            if (value == null || value is System.Decimal)
                                this.PriceInBaseUnit = (System.Decimal?)value;
                            break;

                        case "PriceInBasedUnitWVat":

                            if (value == null || value is System.Decimal)
                                this.PriceInBasedUnitWVat = (System.Decimal?)value;
                            break;

                        case "HighestPriceInBasedUnit":

                            if (value == null || value is System.Decimal)
                                this.HighestPriceInBasedUnit = (System.Decimal?)value;
                            break;

                        case "LastPriceInBaseUnit":

                            if (value == null || value is System.Decimal)
                                this.LastPriceInBaseUnit = (System.Decimal?)value;
                            break;

                        case "PriceWVat":

                            if (value == null || value is System.Decimal)
                                this.PriceWVat = (System.Decimal?)value;
                            break;

                        case "CostPrice":

                            if (value == null || value is System.Decimal)
                                this.CostPrice = (System.Decimal?)value;
                            break;

                        case "PurchaseDiscount1":

                            if (value == null || value is System.Decimal)
                                this.PurchaseDiscount1 = (System.Decimal?)value;
                            break;

                        case "PurchaseDiscount2":

                            if (value == null || value is System.Decimal)
                                this.PurchaseDiscount2 = (System.Decimal?)value;
                            break;

                        case "SafetyStock":

                            if (value == null || value is System.Decimal)
                                this.SafetyStock = (System.Decimal?)value;
                            break;

                        case "SafetyTime":

                            if (value == null || value is System.Byte)
                                this.SafetyTime = (System.Byte?)value;
                            break;

                        case "LeadTime":

                            if (value == null || value is System.Byte)
                                this.LeadTime = (System.Byte?)value;
                            break;

                        case "TolerancePercentage":

                            if (value == null || value is System.Decimal)
                                this.TolerancePercentage = (System.Decimal?)value;
                            break;

                        case "LastUpdateDateTime":

                            if (value == null || value is System.DateTime)
                                this.LastUpdateDateTime = (System.DateTime?)value;
                            break;

                        case "IsSalesAvailable":

                            if (value == null || value is System.Boolean)
                                this.IsSalesAvailable = (System.Boolean?)value;
                            break;

                        case "SalesFixedPrice":

                            if (value == null || value is System.Decimal)
                                this.SalesFixedPrice = (System.Decimal?)value;
                            break;

                        case "MarginPercentage":

                            if (value == null || value is System.Decimal)
                                this.MarginPercentage = (System.Decimal?)value;
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
        /// Maps to ItemKitchen.ItemID
        /// </summary>
        virtual public System.String ItemID
        {
            get
            {
                return base.GetSystemString(ItemKitchenMetadata.ColumnNames.ItemID);
            }

            set
            {
                base.SetSystemString(ItemKitchenMetadata.ColumnNames.ItemID, value);
            }
        }

        /// <summary>
        /// Maps to ItemKitchen.ABCClass
        /// </summary>
        virtual public System.String ABCClass
        {
            get
            {
                return base.GetSystemString(ItemKitchenMetadata.ColumnNames.ABCClass);
            }

            set
            {
                base.SetSystemString(ItemKitchenMetadata.ColumnNames.ABCClass, value);
            }
        }

        /// <summary>
        /// Maps to ItemKitchen.BrandName
        /// </summary>
        virtual public System.String BrandName
        {
            get
            {
                return base.GetSystemString(ItemKitchenMetadata.ColumnNames.BrandName);
            }

            set
            {
                base.SetSystemString(ItemKitchenMetadata.ColumnNames.BrandName, value);
            }
        }

        /// <summary>
        /// Maps to ItemKitchen.SRItemUnit
        /// </summary>
        virtual public System.String SRItemUnit
        {
            get
            {
                return base.GetSystemString(ItemKitchenMetadata.ColumnNames.SRItemUnit);
            }

            set
            {
                base.SetSystemString(ItemKitchenMetadata.ColumnNames.SRItemUnit, value);
            }
        }

        /// <summary>
        /// Maps to ItemKitchen.SRPurchaseUnit
        /// </summary>
        virtual public System.String SRPurchaseUnit
        {
            get
            {
                return base.GetSystemString(ItemKitchenMetadata.ColumnNames.SRPurchaseUnit);
            }

            set
            {
                base.SetSystemString(ItemKitchenMetadata.ColumnNames.SRPurchaseUnit, value);
            }
        }

        /// <summary>
        /// Maps to ItemKitchen.ConversionFactor
        /// </summary>
        virtual public System.Decimal? ConversionFactor
        {
            get
            {
                return base.GetSystemDecimal(ItemKitchenMetadata.ColumnNames.ConversionFactor);
            }

            set
            {
                base.SetSystemDecimal(ItemKitchenMetadata.ColumnNames.ConversionFactor, value);
            }
        }

        /// <summary>
        /// Maps to ItemKitchen.IsInventoryItem
        /// </summary>
        virtual public System.Boolean? IsInventoryItem
        {
            get
            {
                return base.GetSystemBoolean(ItemKitchenMetadata.ColumnNames.IsInventoryItem);
            }

            set
            {
                base.SetSystemBoolean(ItemKitchenMetadata.ColumnNames.IsInventoryItem, value);
            }
        }

        /// <summary>
        /// Maps to ItemKitchen.IsControlExpired
        /// </summary>
        virtual public System.Boolean? IsControlExpired
        {
            get
            {
                return base.GetSystemBoolean(ItemKitchenMetadata.ColumnNames.IsControlExpired);
            }

            set
            {
                base.SetSystemBoolean(ItemKitchenMetadata.ColumnNames.IsControlExpired, value);
            }
        }

        /// <summary>
        /// Maps to ItemKitchen.PriceInPurchaseUnit
        /// </summary>
        virtual public System.Decimal? PriceInPurchaseUnit
        {
            get
            {
                return base.GetSystemDecimal(ItemKitchenMetadata.ColumnNames.PriceInPurchaseUnit);
            }

            set
            {
                base.SetSystemDecimal(ItemKitchenMetadata.ColumnNames.PriceInPurchaseUnit, value);
            }
        }

        /// <summary>
        /// Maps to ItemKitchen.PriceInBaseUnit
        /// </summary>
        virtual public System.Decimal? PriceInBaseUnit
        {
            get
            {
                return base.GetSystemDecimal(ItemKitchenMetadata.ColumnNames.PriceInBaseUnit);
            }

            set
            {
                base.SetSystemDecimal(ItemKitchenMetadata.ColumnNames.PriceInBaseUnit, value);
            }
        }

        /// <summary>
        /// Maps to ItemKitchen.PriceInBasedUnitWVat
        /// </summary>
        virtual public System.Decimal? PriceInBasedUnitWVat
        {
            get
            {
                return base.GetSystemDecimal(ItemKitchenMetadata.ColumnNames.PriceInBasedUnitWVat);
            }

            set
            {
                base.SetSystemDecimal(ItemKitchenMetadata.ColumnNames.PriceInBasedUnitWVat, value);
            }
        }

        /// <summary>
        /// Maps to ItemKitchen.HighestPriceInBasedUnit
        /// </summary>
        virtual public System.Decimal? HighestPriceInBasedUnit
        {
            get
            {
                return base.GetSystemDecimal(ItemKitchenMetadata.ColumnNames.HighestPriceInBasedUnit);
            }

            set
            {
                base.SetSystemDecimal(ItemKitchenMetadata.ColumnNames.HighestPriceInBasedUnit, value);
            }
        }

        /// <summary>
        /// Maps to ItemKitchen.LastPriceInBaseUnit
        /// </summary>
        virtual public System.Decimal? LastPriceInBaseUnit
        {
            get
            {
                return base.GetSystemDecimal(ItemKitchenMetadata.ColumnNames.LastPriceInBaseUnit);
            }

            set
            {
                base.SetSystemDecimal(ItemKitchenMetadata.ColumnNames.LastPriceInBaseUnit, value);
            }
        }

        /// <summary>
        /// Maps to ItemKitchen.PriceWVat
        /// </summary>
        virtual public System.Decimal? PriceWVat
        {
            get
            {
                return base.GetSystemDecimal(ItemKitchenMetadata.ColumnNames.PriceWVat);
            }

            set
            {
                base.SetSystemDecimal(ItemKitchenMetadata.ColumnNames.PriceWVat, value);
            }
        }

        /// <summary>
        /// Maps to ItemKitchen.CostPrice
        /// </summary>
        virtual public System.Decimal? CostPrice
        {
            get
            {
                return base.GetSystemDecimal(ItemKitchenMetadata.ColumnNames.CostPrice);
            }

            set
            {
                base.SetSystemDecimal(ItemKitchenMetadata.ColumnNames.CostPrice, value);
            }
        }

        /// <summary>
        /// Maps to ItemKitchen.PurchaseDiscount1
        /// </summary>
        virtual public System.Decimal? PurchaseDiscount1
        {
            get
            {
                return base.GetSystemDecimal(ItemKitchenMetadata.ColumnNames.PurchaseDiscount1);
            }

            set
            {
                base.SetSystemDecimal(ItemKitchenMetadata.ColumnNames.PurchaseDiscount1, value);
            }
        }

        /// <summary>
        /// Maps to ItemKitchen.PurchaseDiscount2
        /// </summary>
        virtual public System.Decimal? PurchaseDiscount2
        {
            get
            {
                return base.GetSystemDecimal(ItemKitchenMetadata.ColumnNames.PurchaseDiscount2);
            }

            set
            {
                base.SetSystemDecimal(ItemKitchenMetadata.ColumnNames.PurchaseDiscount2, value);
            }
        }

        /// <summary>
        /// Maps to ItemKitchen.SafetyStock
        /// </summary>
        virtual public System.Decimal? SafetyStock
        {
            get
            {
                return base.GetSystemDecimal(ItemKitchenMetadata.ColumnNames.SafetyStock);
            }

            set
            {
                base.SetSystemDecimal(ItemKitchenMetadata.ColumnNames.SafetyStock, value);
            }
        }

        /// <summary>
        /// Maps to ItemKitchen.SafetyTime
        /// </summary>
        virtual public System.Byte? SafetyTime
        {
            get
            {
                return base.GetSystemByte(ItemKitchenMetadata.ColumnNames.SafetyTime);
            }

            set
            {
                base.SetSystemByte(ItemKitchenMetadata.ColumnNames.SafetyTime, value);
            }
        }

        /// <summary>
        /// Maps to ItemKitchen.LeadTime
        /// </summary>
        virtual public System.Byte? LeadTime
        {
            get
            {
                return base.GetSystemByte(ItemKitchenMetadata.ColumnNames.LeadTime);
            }

            set
            {
                base.SetSystemByte(ItemKitchenMetadata.ColumnNames.LeadTime, value);
            }
        }

        /// <summary>
        /// Maps to ItemKitchen.TolerancePercentage
        /// </summary>
        virtual public System.Decimal? TolerancePercentage
        {
            get
            {
                return base.GetSystemDecimal(ItemKitchenMetadata.ColumnNames.TolerancePercentage);
            }

            set
            {
                base.SetSystemDecimal(ItemKitchenMetadata.ColumnNames.TolerancePercentage, value);
            }
        }

        /// <summary>
        /// Maps to ItemKitchen.Barcode
        /// </summary>
        virtual public System.String Barcode
        {
            get
            {
                return base.GetSystemString(ItemKitchenMetadata.ColumnNames.Barcode);
            }

            set
            {
                base.SetSystemString(ItemKitchenMetadata.ColumnNames.Barcode, value);
            }
        }

        /// <summary>
        /// Maps to ItemKitchen.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(ItemKitchenMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(ItemKitchenMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to ItemKitchen.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(ItemKitchenMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(ItemKitchenMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }

        /// <summary>
        /// Maps to ItemKitchen.IsSalesAvailable
        /// </summary>
        virtual public System.Boolean? IsSalesAvailable
        {
            get
            {
                return base.GetSystemBoolean(ItemKitchenMetadata.ColumnNames.IsSalesAvailable);
            }

            set
            {
                base.SetSystemBoolean(ItemKitchenMetadata.ColumnNames.IsSalesAvailable, value);
            }
        }

        /// <summary>
        /// Maps to ItemKitchen.SalesFixedPrice
        /// </summary>
        virtual public System.Decimal? SalesFixedPrice
        {
            get
            {
                return base.GetSystemDecimal(ItemKitchenMetadata.ColumnNames.SalesFixedPrice);
            }

            set
            {
                base.SetSystemDecimal(ItemKitchenMetadata.ColumnNames.SalesFixedPrice, value);
            }
        }

        /// <summary>
        /// Maps to ItemKitchen.MarginPercentage
        /// </summary>
        virtual public System.Decimal? MarginPercentage
        {
            get
            {
                return base.GetSystemDecimal(ItemKitchenMetadata.ColumnNames.MarginPercentage);
            }

            set
            {
                base.SetSystemDecimal(ItemKitchenMetadata.ColumnNames.MarginPercentage, value);
            }
        }

        /// <summary>
        /// Maps to ItemKitchen.MarginID
        /// </summary>
        virtual public System.String MarginID
        {
            get
            {
                return base.GetSystemString(ItemKitchenMetadata.ColumnNames.MarginID);
            }

            set
            {
                base.SetSystemString(ItemKitchenMetadata.ColumnNames.MarginID, value);
            }
        }

        #endregion

        #region String Properties


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
            public esStrings(esItemKitchen entity)
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

            public System.String ABCClass
            {
                get
                {
                    System.String data = entity.ABCClass;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ABCClass = null;
                    else entity.ABCClass = Convert.ToString(value);
                }
            }

            public System.String BrandName
            {
                get
                {
                    System.String data = entity.BrandName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.BrandName = null;
                    else entity.BrandName = Convert.ToString(value);
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

            public System.String SRPurchaseUnit
            {
                get
                {
                    System.String data = entity.SRPurchaseUnit;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRPurchaseUnit = null;
                    else entity.SRPurchaseUnit = Convert.ToString(value);
                }
            }

            public System.String ConversionFactor
            {
                get
                {
                    System.Decimal? data = entity.ConversionFactor;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ConversionFactor = null;
                    else entity.ConversionFactor = Convert.ToDecimal(value);
                }
            }

            public System.String IsInventoryItem
            {
                get
                {
                    System.Boolean? data = entity.IsInventoryItem;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsInventoryItem = null;
                    else entity.IsInventoryItem = Convert.ToBoolean(value);
                }
            }

            public System.String IsControlExpired
            {
                get
                {
                    System.Boolean? data = entity.IsControlExpired;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsControlExpired = null;
                    else entity.IsControlExpired = Convert.ToBoolean(value);
                }
            }

            public System.String PriceInPurchaseUnit
            {
                get
                {
                    System.Decimal? data = entity.PriceInPurchaseUnit;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PriceInPurchaseUnit = null;
                    else entity.PriceInPurchaseUnit = Convert.ToDecimal(value);
                }
            }

            public System.String PriceInBaseUnit
            {
                get
                {
                    System.Decimal? data = entity.PriceInBaseUnit;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PriceInBaseUnit = null;
                    else entity.PriceInBaseUnit = Convert.ToDecimal(value);
                }
            }

            public System.String PriceInBasedUnitWVat
            {
                get
                {
                    System.Decimal? data = entity.PriceInBasedUnitWVat;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PriceInBasedUnitWVat = null;
                    else entity.PriceInBasedUnitWVat = Convert.ToDecimal(value);
                }
            }

            public System.String HighestPriceInBasedUnit
            {
                get
                {
                    System.Decimal? data = entity.HighestPriceInBasedUnit;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.HighestPriceInBasedUnit = null;
                    else entity.HighestPriceInBasedUnit = Convert.ToDecimal(value);
                }
            }

            public System.String LastPriceInBaseUnit
            {
                get
                {
                    System.Decimal? data = entity.LastPriceInBaseUnit;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.LastPriceInBaseUnit = null;
                    else entity.LastPriceInBaseUnit = Convert.ToDecimal(value);
                }
            }

            public System.String PriceWVat
            {
                get
                {
                    System.Decimal? data = entity.PriceWVat;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PriceWVat = null;
                    else entity.PriceWVat = Convert.ToDecimal(value);
                }
            }

            public System.String CostPrice
            {
                get
                {
                    System.Decimal? data = entity.CostPrice;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CostPrice = null;
                    else entity.CostPrice = Convert.ToDecimal(value);
                }
            }

            public System.String PurchaseDiscount1
            {
                get
                {
                    System.Decimal? data = entity.PurchaseDiscount1;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PurchaseDiscount1 = null;
                    else entity.PurchaseDiscount1 = Convert.ToDecimal(value);
                }
            }

            public System.String PurchaseDiscount2
            {
                get
                {
                    System.Decimal? data = entity.PurchaseDiscount2;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PurchaseDiscount2 = null;
                    else entity.PurchaseDiscount2 = Convert.ToDecimal(value);
                }
            }

            public System.String SafetyStock
            {
                get
                {
                    System.Decimal? data = entity.SafetyStock;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SafetyStock = null;
                    else entity.SafetyStock = Convert.ToDecimal(value);
                }
            }

            public System.String SafetyTime
            {
                get
                {
                    System.Byte? data = entity.SafetyTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SafetyTime = null;
                    else entity.SafetyTime = Convert.ToByte(value);
                }
            }

            public System.String LeadTime
            {
                get
                {
                    System.Byte? data = entity.LeadTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.LeadTime = null;
                    else entity.LeadTime = Convert.ToByte(value);
                }
            }

            public System.String TolerancePercentage
            {
                get
                {
                    System.Decimal? data = entity.TolerancePercentage;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TolerancePercentage = null;
                    else entity.TolerancePercentage = Convert.ToDecimal(value);
                }
            }

            public System.String Barcode
            {
                get
                {
                    System.String data = entity.Barcode;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Barcode = null;
                    else entity.Barcode = Convert.ToString(value);
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

            public System.String IsSalesAvailable
            {
                get
                {
                    System.Boolean? data = entity.IsSalesAvailable;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsSalesAvailable = null;
                    else entity.IsSalesAvailable = Convert.ToBoolean(value);
                }
            }

            public System.String SalesFixedPrice
            {
                get
                {
                    System.Decimal? data = entity.SalesFixedPrice;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SalesFixedPrice = null;
                    else entity.SalesFixedPrice = Convert.ToDecimal(value);
                }
            }

            public System.String MarginPercentage
            {
                get
                {
                    System.Decimal? data = entity.MarginPercentage;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.MarginPercentage = null;
                    else entity.MarginPercentage = Convert.ToDecimal(value);
                }
            }

            public System.String MarginID
            {
                get
                {
                    System.String data = entity.MarginID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.MarginID = null;
                    else entity.MarginID = Convert.ToString(value);
                }
            }


            private esItemKitchen entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esItemKitchenQuery query)
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
                throw new Exception("esItemKitchen can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    public partial class ItemKitchen : esItemKitchen
    {

        /// <summary>
        /// Used internally by the entity's hierarchical properties.
        /// </summary>
        protected override List<esPropertyDescriptor> GetHierarchicalProperties()
        {
            List<esPropertyDescriptor> props = new List<esPropertyDescriptor>();


            return props;
        }

        /// <summary>
        /// Used internally for retrieving AutoIncrementing keys
        /// during hierarchical PreSave.
        /// </summary>
        protected override void ApplyPreSaveKeys()
        {
        }

        /// <summary>
        /// Used internally for retrieving AutoIncrementing keys
        /// during hierarchical PostSave.
        /// </summary>
        protected override void ApplyPostSaveKeys()
        {
        }

        /// <summary>
        /// Used internally for retrieving AutoIncrementing keys
        /// during hierarchical PostOneToOneSave.
        /// </summary>
        protected override void ApplyPostOneSaveKeys()
        {
        }

    }



    [Serializable]
    abstract public class esItemKitchenQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return ItemKitchenMetadata.Meta();
            }
        }


        public esQueryItem ItemID
        {
            get
            {
                return new esQueryItem(this, ItemKitchenMetadata.ColumnNames.ItemID, esSystemType.String);
            }
        }

        public esQueryItem ABCClass
        {
            get
            {
                return new esQueryItem(this, ItemKitchenMetadata.ColumnNames.ABCClass, esSystemType.String);
            }
        }

        public esQueryItem BrandName
        {
            get
            {
                return new esQueryItem(this, ItemKitchenMetadata.ColumnNames.BrandName, esSystemType.String);
            }
        }

        public esQueryItem SRItemUnit
        {
            get
            {
                return new esQueryItem(this, ItemKitchenMetadata.ColumnNames.SRItemUnit, esSystemType.String);
            }
        }

        public esQueryItem SRPurchaseUnit
        {
            get
            {
                return new esQueryItem(this, ItemKitchenMetadata.ColumnNames.SRPurchaseUnit, esSystemType.String);
            }
        }

        public esQueryItem ConversionFactor
        {
            get
            {
                return new esQueryItem(this, ItemKitchenMetadata.ColumnNames.ConversionFactor, esSystemType.Decimal);
            }
        }

        public esQueryItem IsInventoryItem
        {
            get
            {
                return new esQueryItem(this, ItemKitchenMetadata.ColumnNames.IsInventoryItem, esSystemType.Boolean);
            }
        }

        public esQueryItem IsControlExpired
        {
            get
            {
                return new esQueryItem(this, ItemKitchenMetadata.ColumnNames.IsControlExpired, esSystemType.Boolean);
            }
        }

        public esQueryItem PriceInPurchaseUnit
        {
            get
            {
                return new esQueryItem(this, ItemKitchenMetadata.ColumnNames.PriceInPurchaseUnit, esSystemType.Decimal);
            }
        }

        public esQueryItem PriceInBaseUnit
        {
            get
            {
                return new esQueryItem(this, ItemKitchenMetadata.ColumnNames.PriceInBaseUnit, esSystemType.Decimal);
            }
        }

        public esQueryItem PriceInBasedUnitWVat
        {
            get
            {
                return new esQueryItem(this, ItemKitchenMetadata.ColumnNames.PriceInBasedUnitWVat, esSystemType.Decimal);
            }
        }

        public esQueryItem HighestPriceInBasedUnit
        {
            get
            {
                return new esQueryItem(this, ItemKitchenMetadata.ColumnNames.HighestPriceInBasedUnit, esSystemType.Decimal);
            }
        }

        public esQueryItem LastPriceInBaseUnit
        {
            get
            {
                return new esQueryItem(this, ItemKitchenMetadata.ColumnNames.LastPriceInBaseUnit, esSystemType.Decimal);
            }
        }

        public esQueryItem PriceWVat
        {
            get
            {
                return new esQueryItem(this, ItemKitchenMetadata.ColumnNames.PriceWVat, esSystemType.Decimal);
            }
        }

        public esQueryItem CostPrice
        {
            get
            {
                return new esQueryItem(this, ItemKitchenMetadata.ColumnNames.CostPrice, esSystemType.Decimal);
            }
        }

        public esQueryItem PurchaseDiscount1
        {
            get
            {
                return new esQueryItem(this, ItemKitchenMetadata.ColumnNames.PurchaseDiscount1, esSystemType.Decimal);
            }
        }

        public esQueryItem PurchaseDiscount2
        {
            get
            {
                return new esQueryItem(this, ItemKitchenMetadata.ColumnNames.PurchaseDiscount2, esSystemType.Decimal);
            }
        }

        public esQueryItem SafetyStock
        {
            get
            {
                return new esQueryItem(this, ItemKitchenMetadata.ColumnNames.SafetyStock, esSystemType.Decimal);
            }
        }

        public esQueryItem SafetyTime
        {
            get
            {
                return new esQueryItem(this, ItemKitchenMetadata.ColumnNames.SafetyTime, esSystemType.Byte);
            }
        }

        public esQueryItem LeadTime
        {
            get
            {
                return new esQueryItem(this, ItemKitchenMetadata.ColumnNames.LeadTime, esSystemType.Byte);
            }
        }

        public esQueryItem TolerancePercentage
        {
            get
            {
                return new esQueryItem(this, ItemKitchenMetadata.ColumnNames.TolerancePercentage, esSystemType.Decimal);
            }
        }

        public esQueryItem Barcode
        {
            get
            {
                return new esQueryItem(this, ItemKitchenMetadata.ColumnNames.Barcode, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, ItemKitchenMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, ItemKitchenMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem IsSalesAvailable
        {
            get
            {
                return new esQueryItem(this, ItemKitchenMetadata.ColumnNames.IsSalesAvailable, esSystemType.Boolean);
            }
        }

        public esQueryItem SalesFixedPrice
        {
            get
            {
                return new esQueryItem(this, ItemKitchenMetadata.ColumnNames.SalesFixedPrice, esSystemType.Decimal);
            }
        }

        public esQueryItem MarginPercentage
        {
            get
            {
                return new esQueryItem(this, ItemKitchenMetadata.ColumnNames.MarginPercentage, esSystemType.Decimal);
            }
        }

        public esQueryItem MarginID
        {
            get
            {
                return new esQueryItem(this, ItemKitchenMetadata.ColumnNames.MarginID, esSystemType.String);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("ItemKitchenCollection")]
    public partial class ItemKitchenCollection : esItemKitchenCollection, IEnumerable<ItemKitchen>
    {
        public ItemKitchenCollection()
        {

        }

        public static implicit operator List<ItemKitchen>(ItemKitchenCollection coll)
        {
            List<ItemKitchen> list = new List<ItemKitchen>();

            foreach (ItemKitchen emp in coll)
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
                return ItemKitchenMetadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ItemKitchenQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new ItemKitchen(row);
        }

        override protected esEntity CreateEntity()
        {
            return new ItemKitchen();
        }


        #endregion


        [BrowsableAttribute(false)]
        public ItemKitchenQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ItemKitchenQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(ItemKitchenQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public ItemKitchen AddNew()
        {
            ItemKitchen entity = base.AddNewEntity() as ItemKitchen;

            return entity;
        }

        public ItemKitchen FindByPrimaryKey(System.String itemID)
        {
            return base.FindByPrimaryKey(itemID) as ItemKitchen;
        }


        #region IEnumerable<ItemKitchen> Members

        IEnumerator<ItemKitchen> IEnumerable<ItemKitchen>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as ItemKitchen;
            }
        }

        #endregion

        private ItemKitchenQuery query;
    }


    /// <summary>
    /// Encapsulates the 'ItemKitchen' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("ItemKitchen ({ItemID})")]
    [Serializable]
    public partial class ItemKitchen : esItemKitchen
    {
        public ItemKitchen()
        {

        }

        public ItemKitchen(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return ItemKitchenMetadata.Meta();
            }
        }



        override protected esItemKitchenQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ItemKitchenQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public ItemKitchenQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ItemKitchenQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(ItemKitchenQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private ItemKitchenQuery query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class ItemKitchenQuery : esItemKitchenQuery
    {
        public ItemKitchenQuery()
        {

        }

        public ItemKitchenQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "ItemKitchenQuery";
        }


    }


    [Serializable]
    public partial class ItemKitchenMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected ItemKitchenMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(ItemKitchenMetadata.ColumnNames.ItemID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemKitchenMetadata.PropertyNames.ItemID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(ItemKitchenMetadata.ColumnNames.ABCClass, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemKitchenMetadata.PropertyNames.ABCClass;
            c.CharacterMaxLength = 1;
            c.HasDefault = true;
            c.Default = @"('A')";
            _columns.Add(c);

            c = new esColumnMetadata(ItemKitchenMetadata.ColumnNames.BrandName, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemKitchenMetadata.PropertyNames.BrandName;
            c.CharacterMaxLength = 50;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(ItemKitchenMetadata.ColumnNames.SRItemUnit, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemKitchenMetadata.PropertyNames.SRItemUnit;
            c.CharacterMaxLength = 20;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(ItemKitchenMetadata.ColumnNames.SRPurchaseUnit, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemKitchenMetadata.PropertyNames.SRPurchaseUnit;
            c.CharacterMaxLength = 20;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(ItemKitchenMetadata.ColumnNames.ConversionFactor, 5, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ItemKitchenMetadata.PropertyNames.ConversionFactor;
            c.NumericPrecision = 7;
            c.NumericScale = 2;
            c.HasDefault = true;
            c.Default = @"((1))";
            _columns.Add(c);

            c = new esColumnMetadata(ItemKitchenMetadata.ColumnNames.IsInventoryItem, 6, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ItemKitchenMetadata.PropertyNames.IsInventoryItem;
            _columns.Add(c);

            c = new esColumnMetadata(ItemKitchenMetadata.ColumnNames.IsControlExpired, 7, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ItemKitchenMetadata.PropertyNames.IsControlExpired;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(ItemKitchenMetadata.ColumnNames.PriceInPurchaseUnit, 8, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ItemKitchenMetadata.PropertyNames.PriceInPurchaseUnit;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(ItemKitchenMetadata.ColumnNames.PriceInBaseUnit, 9, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ItemKitchenMetadata.PropertyNames.PriceInBaseUnit;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(ItemKitchenMetadata.ColumnNames.PriceInBasedUnitWVat, 10, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ItemKitchenMetadata.PropertyNames.PriceInBasedUnitWVat;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(ItemKitchenMetadata.ColumnNames.HighestPriceInBasedUnit, 11, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ItemKitchenMetadata.PropertyNames.HighestPriceInBasedUnit;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(ItemKitchenMetadata.ColumnNames.LastPriceInBaseUnit, 12, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ItemKitchenMetadata.PropertyNames.LastPriceInBaseUnit;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(ItemKitchenMetadata.ColumnNames.PriceWVat, 13, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ItemKitchenMetadata.PropertyNames.PriceWVat;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(ItemKitchenMetadata.ColumnNames.CostPrice, 14, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ItemKitchenMetadata.PropertyNames.CostPrice;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(ItemKitchenMetadata.ColumnNames.PurchaseDiscount1, 15, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ItemKitchenMetadata.PropertyNames.PurchaseDiscount1;
            c.NumericPrecision = 5;
            c.NumericScale = 2;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(ItemKitchenMetadata.ColumnNames.PurchaseDiscount2, 16, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ItemKitchenMetadata.PropertyNames.PurchaseDiscount2;
            c.NumericPrecision = 5;
            c.NumericScale = 2;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(ItemKitchenMetadata.ColumnNames.SafetyStock, 17, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ItemKitchenMetadata.PropertyNames.SafetyStock;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(ItemKitchenMetadata.ColumnNames.SafetyTime, 18, typeof(System.Byte), esSystemType.Byte);
            c.PropertyName = ItemKitchenMetadata.PropertyNames.SafetyTime;
            c.NumericPrecision = 3;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(ItemKitchenMetadata.ColumnNames.LeadTime, 19, typeof(System.Byte), esSystemType.Byte);
            c.PropertyName = ItemKitchenMetadata.PropertyNames.LeadTime;
            c.NumericPrecision = 3;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(ItemKitchenMetadata.ColumnNames.TolerancePercentage, 20, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ItemKitchenMetadata.PropertyNames.TolerancePercentage;
            c.NumericPrecision = 5;
            c.NumericScale = 2;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(ItemKitchenMetadata.ColumnNames.Barcode, 21, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemKitchenMetadata.PropertyNames.Barcode;
            c.CharacterMaxLength = 35;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(ItemKitchenMetadata.ColumnNames.LastUpdateDateTime, 22, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ItemKitchenMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemKitchenMetadata.ColumnNames.LastUpdateByUserID, 23, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemKitchenMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 40;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemKitchenMetadata.ColumnNames.IsSalesAvailable, 24, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ItemKitchenMetadata.PropertyNames.IsSalesAvailable;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemKitchenMetadata.ColumnNames.SalesFixedPrice, 25, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ItemKitchenMetadata.PropertyNames.SalesFixedPrice;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemKitchenMetadata.ColumnNames.MarginPercentage, 26, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ItemKitchenMetadata.PropertyNames.MarginPercentage;
            c.NumericPrecision = 5;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemKitchenMetadata.ColumnNames.MarginID, 27, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemKitchenMetadata.PropertyNames.MarginID;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public ItemKitchenMetadata Meta()
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
            public const string ABCClass = "ABCClass";
            public const string BrandName = "BrandName";
            public const string SRItemUnit = "SRItemUnit";
            public const string SRPurchaseUnit = "SRPurchaseUnit";
            public const string ConversionFactor = "ConversionFactor";
            public const string IsInventoryItem = "IsInventoryItem";
            public const string IsControlExpired = "IsControlExpired";
            public const string PriceInPurchaseUnit = "PriceInPurchaseUnit";
            public const string PriceInBaseUnit = "PriceInBaseUnit";
            public const string PriceInBasedUnitWVat = "PriceInBasedUnitWVat";
            public const string HighestPriceInBasedUnit = "HighestPriceInBasedUnit";
            public const string LastPriceInBaseUnit = "LastPriceInBaseUnit";
            public const string PriceWVat = "PriceWVat";
            public const string CostPrice = "CostPrice";
            public const string PurchaseDiscount1 = "PurchaseDiscount1";
            public const string PurchaseDiscount2 = "PurchaseDiscount2";
            public const string SafetyStock = "SafetyStock";
            public const string SafetyTime = "SafetyTime";
            public const string LeadTime = "LeadTime";
            public const string TolerancePercentage = "TolerancePercentage";
            public const string Barcode = "Barcode";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string IsSalesAvailable = "IsSalesAvailable";
            public const string SalesFixedPrice = "SalesFixedPrice";
            public const string MarginPercentage = "MarginPercentage";
            public const string MarginID = "MarginID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string ItemID = "ItemID";
            public const string ABCClass = "ABCClass";
            public const string BrandName = "BrandName";
            public const string SRItemUnit = "SRItemUnit";
            public const string SRPurchaseUnit = "SRPurchaseUnit";
            public const string ConversionFactor = "ConversionFactor";
            public const string IsInventoryItem = "IsInventoryItem";
            public const string IsControlExpired = "IsControlExpired";
            public const string PriceInPurchaseUnit = "PriceInPurchaseUnit";
            public const string PriceInBaseUnit = "PriceInBaseUnit";
            public const string PriceInBasedUnitWVat = "PriceInBasedUnitWVat";
            public const string HighestPriceInBasedUnit = "HighestPriceInBasedUnit";
            public const string LastPriceInBaseUnit = "LastPriceInBaseUnit";
            public const string PriceWVat = "PriceWVat";
            public const string CostPrice = "CostPrice";
            public const string PurchaseDiscount1 = "PurchaseDiscount1";
            public const string PurchaseDiscount2 = "PurchaseDiscount2";
            public const string SafetyStock = "SafetyStock";
            public const string SafetyTime = "SafetyTime";
            public const string LeadTime = "LeadTime";
            public const string TolerancePercentage = "TolerancePercentage";
            public const string Barcode = "Barcode";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string IsSalesAvailable = "IsSalesAvailable";
            public const string SalesFixedPrice = "SalesFixedPrice";
            public const string MarginPercentage = "MarginPercentage";
            public const string MarginID = "MarginID";
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
            lock (typeof(ItemKitchenMetadata))
            {
                if (ItemKitchenMetadata.mapDelegates == null)
                {
                    ItemKitchenMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (ItemKitchenMetadata.meta == null)
                {
                    ItemKitchenMetadata.meta = new ItemKitchenMetadata();
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
                meta.AddTypeMap("ABCClass", new esTypeMap("char", "System.String"));
                meta.AddTypeMap("BrandName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRItemUnit", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRPurchaseUnit", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ConversionFactor", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("IsInventoryItem", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsControlExpired", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("PriceInPurchaseUnit", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("PriceInBaseUnit", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("PriceInBasedUnitWVat", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("HighestPriceInBasedUnit", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("LastPriceInBaseUnit", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("PriceWVat", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("CostPrice", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("PurchaseDiscount1", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("PurchaseDiscount2", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("SafetyStock", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("SafetyTime", new esTypeMap("tinyint", "System.Byte"));
                meta.AddTypeMap("LeadTime", new esTypeMap("tinyint", "System.Byte"));
                meta.AddTypeMap("TolerancePercentage", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("Barcode", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsSalesAvailable", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("SalesFixedPrice", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("MarginPercentage", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("MarginID", new esTypeMap("varchar", "System.String"));



                meta.Source = "ItemKitchen";
                meta.Destination = "ItemKitchen";

                meta.spInsert = "proc_ItemKitchenInsert";
                meta.spUpdate = "proc_ItemKitchenUpdate";
                meta.spDelete = "proc_ItemKitchenDelete";
                meta.spLoadAll = "proc_ItemKitchenLoadAll";
                meta.spLoadByPrimaryKey = "proc_ItemKitchenLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private ItemKitchenMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
