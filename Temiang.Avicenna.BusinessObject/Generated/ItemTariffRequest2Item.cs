/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 3/13/2019 2:52:44 PM
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
    abstract public class esItemTariffRequest2ItemCollection : esEntityCollectionWAuditLog
    {
        public esItemTariffRequest2ItemCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "ItemTariffRequest2ItemCollection";
        }

        #region Query Logic
        protected void InitQuery(esItemTariffRequest2ItemQuery query)
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
            this.InitQuery(query as esItemTariffRequest2ItemQuery);
        }
        #endregion

        virtual public ItemTariffRequest2Item DetachEntity(ItemTariffRequest2Item entity)
        {
            return base.DetachEntity(entity) as ItemTariffRequest2Item;
        }

        virtual public ItemTariffRequest2Item AttachEntity(ItemTariffRequest2Item entity)
        {
            return base.AttachEntity(entity) as ItemTariffRequest2Item;
        }

        virtual public void Combine(ItemTariffRequest2ItemCollection collection)
        {
            base.Combine(collection);
        }

        new public ItemTariffRequest2Item this[int index]
        {
            get
            {
                return base[index] as ItemTariffRequest2Item;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(ItemTariffRequest2Item);
        }
    }

    [Serializable]
    abstract public class esItemTariffRequest2Item : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esItemTariffRequest2ItemQuery GetDynamicQuery()
        {
            return null;
        }

        public esItemTariffRequest2Item()
        {
        }

        public esItemTariffRequest2Item(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String tariffRequestNo, String itemID, String classID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(tariffRequestNo, itemID, classID);
            else
                return LoadByPrimaryKeyStoredProcedure(tariffRequestNo, itemID, classID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String tariffRequestNo, String itemID, String classID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(tariffRequestNo, itemID, classID);
            else
                return LoadByPrimaryKeyStoredProcedure(tariffRequestNo, itemID, classID);
        }

        private bool LoadByPrimaryKeyDynamic(String tariffRequestNo, String itemID, String classID)
        {
            esItemTariffRequest2ItemQuery query = this.GetDynamicQuery();
            query.Where(query.TariffRequestNo == tariffRequestNo, query.ItemID == itemID, query.ClassID == classID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String tariffRequestNo, String itemID, String classID)
        {
            esParameters parms = new esParameters();
            parms.Add("TariffRequestNo", tariffRequestNo);
            parms.Add("ItemID", itemID);
            parms.Add("ClassID", classID);
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
                        case "TariffRequestNo": this.str.TariffRequestNo = (string)value; break;
                        case "ItemID": this.str.ItemID = (string)value; break;
                        case "ClassID": this.str.ClassID = (string)value; break;
                        case "Price": this.str.Price = (string)value; break;
                        case "PriceInBaseUnit": this.str.PriceInBaseUnit = (string)value; break;
                        case "PriceInBaseUnitWVat": this.str.PriceInBaseUnitWVat = (string)value; break;
                        case "PriceInPurchaseUnit": this.str.PriceInPurchaseUnit = (string)value; break;
                        case "CostPrice": this.str.CostPrice = (string)value; break;
                        case "IsAllowCito": this.str.IsAllowCito = (string)value; break;
                        case "IsCitoInPercent": this.str.IsCitoInPercent = (string)value; break;
                        case "CitoValue": this.str.CitoValue = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "DiscPercentage": this.str.DiscPercentage = (string)value; break;
                        case "IsCitoFromStandardReference": this.str.IsCitoFromStandardReference = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "Price":

                            if (value == null || value is System.Decimal)
                                this.Price = (System.Decimal?)value;
                            break;
                        case "PriceInBaseUnit":

                            if (value == null || value is System.Decimal)
                                this.PriceInBaseUnit = (System.Decimal?)value;
                            break;
                        case "PriceInBaseUnitWVat":

                            if (value == null || value is System.Decimal)
                                this.PriceInBaseUnitWVat = (System.Decimal?)value;
                            break;
                        case "PriceInPurchaseUnit":

                            if (value == null || value is System.Decimal)
                                this.PriceInPurchaseUnit = (System.Decimal?)value;
                            break;
                        case "CostPrice":

                            if (value == null || value is System.Decimal)
                                this.CostPrice = (System.Decimal?)value;
                            break;
                        case "IsAllowCito":

                            if (value == null || value is System.Boolean)
                                this.IsAllowCito = (System.Boolean?)value;
                            break;
                        case "IsCitoInPercent":

                            if (value == null || value is System.Boolean)
                                this.IsCitoInPercent = (System.Boolean?)value;
                            break;
                        case "CitoValue":

                            if (value == null || value is System.Decimal)
                                this.CitoValue = (System.Decimal?)value;
                            break;
                        case "LastUpdateDateTime":

                            if (value == null || value is System.DateTime)
                                this.LastUpdateDateTime = (System.DateTime?)value;
                            break;
                        case "DiscPercentage":

                            if (value == null || value is System.Decimal)
                                this.DiscPercentage = (System.Decimal?)value;
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
        /// Maps to ItemTariffRequest2Item.TariffRequestNo
        /// </summary>
        virtual public System.String TariffRequestNo
        {
            get
            {
                return base.GetSystemString(ItemTariffRequest2ItemMetadata.ColumnNames.TariffRequestNo);
            }

            set
            {
                base.SetSystemString(ItemTariffRequest2ItemMetadata.ColumnNames.TariffRequestNo, value);
            }
        }
        /// <summary>
        /// Maps to ItemTariffRequest2Item.ItemID
        /// </summary>
        virtual public System.String ItemID
        {
            get
            {
                return base.GetSystemString(ItemTariffRequest2ItemMetadata.ColumnNames.ItemID);
            }

            set
            {
                base.SetSystemString(ItemTariffRequest2ItemMetadata.ColumnNames.ItemID, value);
            }
        }
        /// <summary>
        /// Maps to ItemTariffRequest2Item.ClassID
        /// </summary>
        virtual public System.String ClassID
        {
            get
            {
                return base.GetSystemString(ItemTariffRequest2ItemMetadata.ColumnNames.ClassID);
            }

            set
            {
                base.SetSystemString(ItemTariffRequest2ItemMetadata.ColumnNames.ClassID, value);
            }
        }
        /// <summary>
        /// Maps to ItemTariffRequest2Item.Price
        /// </summary>
        virtual public System.Decimal? Price
        {
            get
            {
                return base.GetSystemDecimal(ItemTariffRequest2ItemMetadata.ColumnNames.Price);
            }

            set
            {
                base.SetSystemDecimal(ItemTariffRequest2ItemMetadata.ColumnNames.Price, value);
            }
        }
        /// <summary>
        /// Maps to ItemTariffRequest2Item.PriceInBaseUnit
        /// </summary>
        virtual public System.Decimal? PriceInBaseUnit
        {
            get
            {
                return base.GetSystemDecimal(ItemTariffRequest2ItemMetadata.ColumnNames.PriceInBaseUnit);
            }

            set
            {
                base.SetSystemDecimal(ItemTariffRequest2ItemMetadata.ColumnNames.PriceInBaseUnit, value);
            }
        }
        /// <summary>
        /// Maps to ItemTariffRequest2Item.PriceInBaseUnitWVat
        /// </summary>
        virtual public System.Decimal? PriceInBaseUnitWVat
        {
            get
            {
                return base.GetSystemDecimal(ItemTariffRequest2ItemMetadata.ColumnNames.PriceInBaseUnitWVat);
            }

            set
            {
                base.SetSystemDecimal(ItemTariffRequest2ItemMetadata.ColumnNames.PriceInBaseUnitWVat, value);
            }
        }
        /// <summary>
        /// Maps to ItemTariffRequest2Item.PriceInPurchaseUnit
        /// </summary>
        virtual public System.Decimal? PriceInPurchaseUnit
        {
            get
            {
                return base.GetSystemDecimal(ItemTariffRequest2ItemMetadata.ColumnNames.PriceInPurchaseUnit);
            }

            set
            {
                base.SetSystemDecimal(ItemTariffRequest2ItemMetadata.ColumnNames.PriceInPurchaseUnit, value);
            }
        }
        /// <summary>
        /// Maps to ItemTariffRequest2Item.CostPrice
        /// </summary>
        virtual public System.Decimal? CostPrice
        {
            get
            {
                return base.GetSystemDecimal(ItemTariffRequest2ItemMetadata.ColumnNames.CostPrice);
            }

            set
            {
                base.SetSystemDecimal(ItemTariffRequest2ItemMetadata.ColumnNames.CostPrice, value);
            }
        }
        /// <summary>
        /// Maps to ItemTariffRequest2Item.IsAllowCito
        /// </summary>
        virtual public System.Boolean? IsAllowCito
        {
            get
            {
                return base.GetSystemBoolean(ItemTariffRequest2ItemMetadata.ColumnNames.IsAllowCito);
            }

            set
            {
                base.SetSystemBoolean(ItemTariffRequest2ItemMetadata.ColumnNames.IsAllowCito, value);
            }
        }
        /// <summary>
        /// Maps to ItemTariffRequest2Item.IsCitoInPercent
        /// </summary>
        virtual public System.Boolean? IsCitoInPercent
        {
            get
            {
                return base.GetSystemBoolean(ItemTariffRequest2ItemMetadata.ColumnNames.IsCitoInPercent);
            }

            set
            {
                base.SetSystemBoolean(ItemTariffRequest2ItemMetadata.ColumnNames.IsCitoInPercent, value);
            }
        }
        /// <summary>
        /// Maps to ItemTariffRequest2Item.CitoValue
        /// </summary>
        virtual public System.Decimal? CitoValue
        {
            get
            {
                return base.GetSystemDecimal(ItemTariffRequest2ItemMetadata.ColumnNames.CitoValue);
            }

            set
            {
                base.SetSystemDecimal(ItemTariffRequest2ItemMetadata.ColumnNames.CitoValue, value);
            }
        }
        /// <summary>
        /// Maps to ItemTariffRequest2Item.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(ItemTariffRequest2ItemMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(ItemTariffRequest2ItemMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to ItemTariffRequest2Item.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(ItemTariffRequest2ItemMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(ItemTariffRequest2ItemMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to ItemTariffRequest2Item.DiscPercentage
        /// </summary>
        virtual public System.Decimal? DiscPercentage
        {
            get
            {
                return base.GetSystemDecimal(ItemTariffRequest2ItemMetadata.ColumnNames.DiscPercentage);
            }

            set
            {
                base.SetSystemDecimal(ItemTariffRequest2ItemMetadata.ColumnNames.DiscPercentage, value);
            }
        }
        /// <summary>
        /// Maps to ItemTariffRequest2Item.IsCitoFromStandardReference
        /// </summary>
        virtual public System.Boolean? IsCitoFromStandardReference
        {
            get
            {
                return base.GetSystemBoolean(ItemTariffRequest2ItemMetadata.ColumnNames.IsCitoFromStandardReference);
            }

            set
            {
                base.SetSystemBoolean(ItemTariffRequest2ItemMetadata.ColumnNames.IsCitoFromStandardReference, value);
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
            public esStrings(esItemTariffRequest2Item entity)
            {
                this.entity = entity;
            }
            public System.String TariffRequestNo
            {
                get
                {
                    System.String data = entity.TariffRequestNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TariffRequestNo = null;
                    else entity.TariffRequestNo = Convert.ToString(value);
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
            public System.String ClassID
            {
                get
                {
                    System.String data = entity.ClassID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ClassID = null;
                    else entity.ClassID = Convert.ToString(value);
                }
            }
            public System.String Price
            {
                get
                {
                    System.Decimal? data = entity.Price;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Price = null;
                    else entity.Price = Convert.ToDecimal(value);
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
            public System.String PriceInBaseUnitWVat
            {
                get
                {
                    System.Decimal? data = entity.PriceInBaseUnitWVat;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PriceInBaseUnitWVat = null;
                    else entity.PriceInBaseUnitWVat = Convert.ToDecimal(value);
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
            public System.String IsCitoInPercent
            {
                get
                {
                    System.Boolean? data = entity.IsCitoInPercent;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsCitoInPercent = null;
                    else entity.IsCitoInPercent = Convert.ToBoolean(value);
                }
            }
            public System.String CitoValue
            {
                get
                {
                    System.Decimal? data = entity.CitoValue;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CitoValue = null;
                    else entity.CitoValue = Convert.ToDecimal(value);
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
            public System.String DiscPercentage
            {
                get
                {
                    System.Decimal? data = entity.DiscPercentage;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DiscPercentage = null;
                    else entity.DiscPercentage = Convert.ToDecimal(value);
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
            private esItemTariffRequest2Item entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esItemTariffRequest2ItemQuery query)
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
                throw new Exception("esItemTariffRequest2Item can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class ItemTariffRequest2Item : esItemTariffRequest2Item
    {
    }

    [Serializable]
    abstract public class esItemTariffRequest2ItemQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return ItemTariffRequest2ItemMetadata.Meta();
            }
        }

        public esQueryItem TariffRequestNo
        {
            get
            {
                return new esQueryItem(this, ItemTariffRequest2ItemMetadata.ColumnNames.TariffRequestNo, esSystemType.String);
            }
        }

        public esQueryItem ItemID
        {
            get
            {
                return new esQueryItem(this, ItemTariffRequest2ItemMetadata.ColumnNames.ItemID, esSystemType.String);
            }
        }

        public esQueryItem ClassID
        {
            get
            {
                return new esQueryItem(this, ItemTariffRequest2ItemMetadata.ColumnNames.ClassID, esSystemType.String);
            }
        }

        public esQueryItem Price
        {
            get
            {
                return new esQueryItem(this, ItemTariffRequest2ItemMetadata.ColumnNames.Price, esSystemType.Decimal);
            }
        }

        public esQueryItem PriceInBaseUnit
        {
            get
            {
                return new esQueryItem(this, ItemTariffRequest2ItemMetadata.ColumnNames.PriceInBaseUnit, esSystemType.Decimal);
            }
        }

        public esQueryItem PriceInBaseUnitWVat
        {
            get
            {
                return new esQueryItem(this, ItemTariffRequest2ItemMetadata.ColumnNames.PriceInBaseUnitWVat, esSystemType.Decimal);
            }
        }

        public esQueryItem PriceInPurchaseUnit
        {
            get
            {
                return new esQueryItem(this, ItemTariffRequest2ItemMetadata.ColumnNames.PriceInPurchaseUnit, esSystemType.Decimal);
            }
        }

        public esQueryItem CostPrice
        {
            get
            {
                return new esQueryItem(this, ItemTariffRequest2ItemMetadata.ColumnNames.CostPrice, esSystemType.Decimal);
            }
        }

        public esQueryItem IsAllowCito
        {
            get
            {
                return new esQueryItem(this, ItemTariffRequest2ItemMetadata.ColumnNames.IsAllowCito, esSystemType.Boolean);
            }
        }

        public esQueryItem IsCitoInPercent
        {
            get
            {
                return new esQueryItem(this, ItemTariffRequest2ItemMetadata.ColumnNames.IsCitoInPercent, esSystemType.Boolean);
            }
        }

        public esQueryItem CitoValue
        {
            get
            {
                return new esQueryItem(this, ItemTariffRequest2ItemMetadata.ColumnNames.CitoValue, esSystemType.Decimal);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, ItemTariffRequest2ItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, ItemTariffRequest2ItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem DiscPercentage
        {
            get
            {
                return new esQueryItem(this, ItemTariffRequest2ItemMetadata.ColumnNames.DiscPercentage, esSystemType.Decimal);
            }
        }

        public esQueryItem IsCitoFromStandardReference
        {
            get
            {
                return new esQueryItem(this, ItemTariffRequest2ItemMetadata.ColumnNames.IsCitoFromStandardReference, esSystemType.Boolean);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("ItemTariffRequest2ItemCollection")]
    public partial class ItemTariffRequest2ItemCollection : esItemTariffRequest2ItemCollection, IEnumerable<ItemTariffRequest2Item>
    {
        public ItemTariffRequest2ItemCollection()
        {

        }

        public static implicit operator List<ItemTariffRequest2Item>(ItemTariffRequest2ItemCollection coll)
        {
            List<ItemTariffRequest2Item> list = new List<ItemTariffRequest2Item>();

            foreach (ItemTariffRequest2Item emp in coll)
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
                return ItemTariffRequest2ItemMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ItemTariffRequest2ItemQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new ItemTariffRequest2Item(row);
        }

        override protected esEntity CreateEntity()
        {
            return new ItemTariffRequest2Item();
        }

        #endregion

        [BrowsableAttribute(false)]
        public ItemTariffRequest2ItemQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ItemTariffRequest2ItemQuery();
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
        public bool Load(ItemTariffRequest2ItemQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public ItemTariffRequest2Item AddNew()
        {
            ItemTariffRequest2Item entity = base.AddNewEntity() as ItemTariffRequest2Item;

            return entity;
        }
        public ItemTariffRequest2Item FindByPrimaryKey(String tariffRequestNo, String itemID, String classID)
        {
            return base.FindByPrimaryKey(tariffRequestNo, itemID, classID) as ItemTariffRequest2Item;
        }

        #region IEnumerable< ItemTariffRequest2Item> Members

        IEnumerator<ItemTariffRequest2Item> IEnumerable<ItemTariffRequest2Item>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as ItemTariffRequest2Item;
            }
        }

        #endregion

        private ItemTariffRequest2ItemQuery query;
    }


    /// <summary>
    /// Encapsulates the 'ItemTariffRequest2Item' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("ItemTariffRequest2Item ({TariffRequestNo, ItemID, ClassID})")]
    [Serializable]
    public partial class ItemTariffRequest2Item : esItemTariffRequest2Item
    {
        public ItemTariffRequest2Item()
        {
        }

        public ItemTariffRequest2Item(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return ItemTariffRequest2ItemMetadata.Meta();
            }
        }

        override protected esItemTariffRequest2ItemQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ItemTariffRequest2ItemQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public ItemTariffRequest2ItemQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ItemTariffRequest2ItemQuery();
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
        public bool Load(ItemTariffRequest2ItemQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private ItemTariffRequest2ItemQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class ItemTariffRequest2ItemQuery : esItemTariffRequest2ItemQuery
    {
        public ItemTariffRequest2ItemQuery()
        {

        }

        public ItemTariffRequest2ItemQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "ItemTariffRequest2ItemQuery";
        }
    }

    [Serializable]
    public partial class ItemTariffRequest2ItemMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected ItemTariffRequest2ItemMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(ItemTariffRequest2ItemMetadata.ColumnNames.TariffRequestNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemTariffRequest2ItemMetadata.PropertyNames.TariffRequestNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(ItemTariffRequest2ItemMetadata.ColumnNames.ItemID, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemTariffRequest2ItemMetadata.PropertyNames.ItemID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(ItemTariffRequest2ItemMetadata.ColumnNames.ClassID, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemTariffRequest2ItemMetadata.PropertyNames.ClassID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(ItemTariffRequest2ItemMetadata.ColumnNames.Price, 3, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ItemTariffRequest2ItemMetadata.PropertyNames.Price;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(ItemTariffRequest2ItemMetadata.ColumnNames.PriceInBaseUnit, 4, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ItemTariffRequest2ItemMetadata.PropertyNames.PriceInBaseUnit;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.HasDefault = true;
            c.Default = @"((0))";
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemTariffRequest2ItemMetadata.ColumnNames.PriceInBaseUnitWVat, 5, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ItemTariffRequest2ItemMetadata.PropertyNames.PriceInBaseUnitWVat;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.HasDefault = true;
            c.Default = @"((0))";
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemTariffRequest2ItemMetadata.ColumnNames.PriceInPurchaseUnit, 6, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ItemTariffRequest2ItemMetadata.PropertyNames.PriceInPurchaseUnit;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.HasDefault = true;
            c.Default = @"((0))";
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemTariffRequest2ItemMetadata.ColumnNames.CostPrice, 7, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ItemTariffRequest2ItemMetadata.PropertyNames.CostPrice;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.HasDefault = true;
            c.Default = @"((0))";
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemTariffRequest2ItemMetadata.ColumnNames.IsAllowCito, 8, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ItemTariffRequest2ItemMetadata.PropertyNames.IsAllowCito;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemTariffRequest2ItemMetadata.ColumnNames.IsCitoInPercent, 9, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ItemTariffRequest2ItemMetadata.PropertyNames.IsCitoInPercent;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemTariffRequest2ItemMetadata.ColumnNames.CitoValue, 10, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ItemTariffRequest2ItemMetadata.PropertyNames.CitoValue;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemTariffRequest2ItemMetadata.ColumnNames.LastUpdateDateTime, 11, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ItemTariffRequest2ItemMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemTariffRequest2ItemMetadata.ColumnNames.LastUpdateByUserID, 12, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemTariffRequest2ItemMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 40;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemTariffRequest2ItemMetadata.ColumnNames.DiscPercentage, 13, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ItemTariffRequest2ItemMetadata.PropertyNames.DiscPercentage;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemTariffRequest2ItemMetadata.ColumnNames.IsCitoFromStandardReference, 14, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ItemTariffRequest2ItemMetadata.PropertyNames.IsCitoFromStandardReference;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public ItemTariffRequest2ItemMetadata Meta()
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
            public const string TariffRequestNo = "TariffRequestNo";
            public const string ItemID = "ItemID";
            public const string ClassID = "ClassID";
            public const string Price = "Price";
            public const string PriceInBaseUnit = "PriceInBaseUnit";
            public const string PriceInBaseUnitWVat = "PriceInBaseUnitWVat";
            public const string PriceInPurchaseUnit = "PriceInPurchaseUnit";
            public const string CostPrice = "CostPrice";
            public const string IsAllowCito = "IsAllowCito";
            public const string IsCitoInPercent = "IsCitoInPercent";
            public const string CitoValue = "CitoValue";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string DiscPercentage = "DiscPercentage";
            public const string IsCitoFromStandardReference = "IsCitoFromStandardReference";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string TariffRequestNo = "TariffRequestNo";
            public const string ItemID = "ItemID";
            public const string ClassID = "ClassID";
            public const string Price = "Price";
            public const string PriceInBaseUnit = "PriceInBaseUnit";
            public const string PriceInBaseUnitWVat = "PriceInBaseUnitWVat";
            public const string PriceInPurchaseUnit = "PriceInPurchaseUnit";
            public const string CostPrice = "CostPrice";
            public const string IsAllowCito = "IsAllowCito";
            public const string IsCitoInPercent = "IsCitoInPercent";
            public const string CitoValue = "CitoValue";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string DiscPercentage = "DiscPercentage";
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
            lock (typeof(ItemTariffRequest2ItemMetadata))
            {
                if (ItemTariffRequest2ItemMetadata.mapDelegates == null)
                {
                    ItemTariffRequest2ItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (ItemTariffRequest2ItemMetadata.meta == null)
                {
                    ItemTariffRequest2ItemMetadata.meta = new ItemTariffRequest2ItemMetadata();
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

                meta.AddTypeMap("TariffRequestNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ClassID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Price", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("PriceInBaseUnit", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("PriceInBaseUnitWVat", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("PriceInPurchaseUnit", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("CostPrice", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("IsAllowCito", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsCitoInPercent", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("CitoValue", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("DiscPercentage", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("IsCitoFromStandardReference", new esTypeMap("bit", "System.Boolean"));


                meta.Source = "ItemTariffRequest2Item";
                meta.Destination = "ItemTariffRequest2Item";
                meta.spInsert = "proc_ItemTariffRequest2ItemInsert";
                meta.spUpdate = "proc_ItemTariffRequest2ItemUpdate";
                meta.spDelete = "proc_ItemTariffRequest2ItemDelete";
                meta.spLoadAll = "proc_ItemTariffRequest2ItemLoadAll";
                meta.spLoadByPrimaryKey = "proc_ItemTariffRequest2ItemLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private ItemTariffRequest2ItemMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
