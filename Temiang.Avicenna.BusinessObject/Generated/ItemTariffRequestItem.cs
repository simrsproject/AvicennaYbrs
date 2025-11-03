/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 3/2/2013 9:03:51 AM
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
    abstract public class esItemTariffRequestItemCollection : esEntityCollectionWAuditLog
    {
        public esItemTariffRequestItemCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "ItemTariffRequestItemCollection";
        }

        #region Query Logic
        protected void InitQuery(esItemTariffRequestItemQuery query)
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
            this.InitQuery(query as esItemTariffRequestItemQuery);
        }
        #endregion

        virtual public ItemTariffRequestItem DetachEntity(ItemTariffRequestItem entity)
        {
            return base.DetachEntity(entity) as ItemTariffRequestItem;
        }

        virtual public ItemTariffRequestItem AttachEntity(ItemTariffRequestItem entity)
        {
            return base.AttachEntity(entity) as ItemTariffRequestItem;
        }

        virtual public void Combine(ItemTariffRequestItemCollection collection)
        {
            base.Combine(collection);
        }

        new public ItemTariffRequestItem this[int index]
        {
            get
            {
                return base[index] as ItemTariffRequestItem;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(ItemTariffRequestItem);
        }
    }



    [Serializable]
    abstract public class esItemTariffRequestItem : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esItemTariffRequestItemQuery GetDynamicQuery()
        {
            return null;
        }

        public esItemTariffRequestItem()
        {

        }

        public esItemTariffRequestItem(DataRow row)
            : base(row)
        {

        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(System.String tariffRequestNo, System.String itemID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(tariffRequestNo, itemID);
            else
                return LoadByPrimaryKeyStoredProcedure(tariffRequestNo, itemID);
        }

        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String tariffRequestNo, System.String itemID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(tariffRequestNo, itemID);
            else
                return LoadByPrimaryKeyStoredProcedure(tariffRequestNo, itemID);
        }

        private bool LoadByPrimaryKeyDynamic(System.String tariffRequestNo, System.String itemID)
        {
            esItemTariffRequestItemQuery query = this.GetDynamicQuery();
            query.Where(query.TariffRequestNo == tariffRequestNo, query.ItemID == itemID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(System.String tariffRequestNo, System.String itemID)
        {
            esParameters parms = new esParameters();
            parms.Add("TariffRequestNo", tariffRequestNo); parms.Add("ItemID", itemID);
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
        /// Maps to ItemTariffRequestItem.TariffRequestNo
        /// </summary>
        virtual public System.String TariffRequestNo
        {
            get
            {
                return base.GetSystemString(ItemTariffRequestItemMetadata.ColumnNames.TariffRequestNo);
            }

            set
            {
                base.SetSystemString(ItemTariffRequestItemMetadata.ColumnNames.TariffRequestNo, value);
            }
        }

        /// <summary>
        /// Maps to ItemTariffRequestItem.ItemID
        /// </summary>
        virtual public System.String ItemID
        {
            get
            {
                return base.GetSystemString(ItemTariffRequestItemMetadata.ColumnNames.ItemID);
            }

            set
            {
                base.SetSystemString(ItemTariffRequestItemMetadata.ColumnNames.ItemID, value);
            }
        }

        /// <summary>
        /// Maps to ItemTariffRequestItem.Price
        /// </summary>
        virtual public System.Decimal? Price
        {
            get
            {
                return base.GetSystemDecimal(ItemTariffRequestItemMetadata.ColumnNames.Price);
            }

            set
            {
                base.SetSystemDecimal(ItemTariffRequestItemMetadata.ColumnNames.Price, value);
            }
        }

        /// <summary>
        /// Maps to ItemTariffRequestItem.PriceInBaseUnit
        /// </summary>
        virtual public System.Decimal? PriceInBaseUnit
        {
            get
            {
                return base.GetSystemDecimal(ItemTariffRequestItemMetadata.ColumnNames.PriceInBaseUnit);
            }

            set
            {
                base.SetSystemDecimal(ItemTariffRequestItemMetadata.ColumnNames.PriceInBaseUnit, value);
            }
        }

        /// <summary>
        /// Maps to ItemTariffRequestItem.PriceInBaseUnitWVat
        /// </summary>
        virtual public System.Decimal? PriceInBaseUnitWVat
        {
            get
            {
                return base.GetSystemDecimal(ItemTariffRequestItemMetadata.ColumnNames.PriceInBaseUnitWVat);
            }

            set
            {
                base.SetSystemDecimal(ItemTariffRequestItemMetadata.ColumnNames.PriceInBaseUnitWVat, value);
            }
        }

        /// <summary>
        /// Maps to ItemTariffRequestItem.PriceInPurchaseUnit
        /// </summary>
        virtual public System.Decimal? PriceInPurchaseUnit
        {
            get
            {
                return base.GetSystemDecimal(ItemTariffRequestItemMetadata.ColumnNames.PriceInPurchaseUnit);
            }

            set
            {
                base.SetSystemDecimal(ItemTariffRequestItemMetadata.ColumnNames.PriceInPurchaseUnit, value);
            }
        }

        /// <summary>
        /// Maps to ItemTariffRequestItem.CostPrice
        /// </summary>
        virtual public System.Decimal? CostPrice
        {
            get
            {
                return base.GetSystemDecimal(ItemTariffRequestItemMetadata.ColumnNames.CostPrice);
            }

            set
            {
                base.SetSystemDecimal(ItemTariffRequestItemMetadata.ColumnNames.CostPrice, value);
            }
        }

        /// <summary>
        /// Maps to ItemTariffRequestItem.IsAllowCito
        /// </summary>
        virtual public System.Boolean? IsAllowCito
        {
            get
            {
                return base.GetSystemBoolean(ItemTariffRequestItemMetadata.ColumnNames.IsAllowCito);
            }

            set
            {
                base.SetSystemBoolean(ItemTariffRequestItemMetadata.ColumnNames.IsAllowCito, value);
            }
        }

        /// <summary>
        /// Maps to ItemTariffRequestItem.IsCitoInPercent
        /// </summary>
        virtual public System.Boolean? IsCitoInPercent
        {
            get
            {
                return base.GetSystemBoolean(ItemTariffRequestItemMetadata.ColumnNames.IsCitoInPercent);
            }

            set
            {
                base.SetSystemBoolean(ItemTariffRequestItemMetadata.ColumnNames.IsCitoInPercent, value);
            }
        }

        /// <summary>
        /// Maps to ItemTariffRequestItem.CitoValue
        /// </summary>
        virtual public System.Decimal? CitoValue
        {
            get
            {
                return base.GetSystemDecimal(ItemTariffRequestItemMetadata.ColumnNames.CitoValue);
            }

            set
            {
                base.SetSystemDecimal(ItemTariffRequestItemMetadata.ColumnNames.CitoValue, value);
            }
        }

        /// <summary>
        /// Maps to ItemTariffRequestItem.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(ItemTariffRequestItemMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(ItemTariffRequestItemMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to ItemTariffRequestItem.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(ItemTariffRequestItemMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(ItemTariffRequestItemMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }

        /// <summary>
        /// Maps to ItemTariffRequestItem.DiscPercentage
        /// </summary>
        virtual public System.Decimal? DiscPercentage
        {
            get
            {
                return base.GetSystemDecimal(ItemTariffRequestItemMetadata.ColumnNames.DiscPercentage);
            }

            set
            {
                base.SetSystemDecimal(ItemTariffRequestItemMetadata.ColumnNames.DiscPercentage, value);
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
            public esStrings(esItemTariffRequestItem entity)
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


            private esItemTariffRequestItem entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esItemTariffRequestItemQuery query)
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
                throw new Exception("esItemTariffRequestItem can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    public partial class ItemTariffRequestItem : esItemTariffRequestItem
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
    abstract public class esItemTariffRequestItemQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return ItemTariffRequestItemMetadata.Meta();
            }
        }


        public esQueryItem TariffRequestNo
        {
            get
            {
                return new esQueryItem(this, ItemTariffRequestItemMetadata.ColumnNames.TariffRequestNo, esSystemType.String);
            }
        }

        public esQueryItem ItemID
        {
            get
            {
                return new esQueryItem(this, ItemTariffRequestItemMetadata.ColumnNames.ItemID, esSystemType.String);
            }
        }

        public esQueryItem Price
        {
            get
            {
                return new esQueryItem(this, ItemTariffRequestItemMetadata.ColumnNames.Price, esSystemType.Decimal);
            }
        }

        public esQueryItem PriceInBaseUnit
        {
            get
            {
                return new esQueryItem(this, ItemTariffRequestItemMetadata.ColumnNames.PriceInBaseUnit, esSystemType.Decimal);
            }
        }

        public esQueryItem PriceInBaseUnitWVat
        {
            get
            {
                return new esQueryItem(this, ItemTariffRequestItemMetadata.ColumnNames.PriceInBaseUnitWVat, esSystemType.Decimal);
            }
        }

        public esQueryItem PriceInPurchaseUnit
        {
            get
            {
                return new esQueryItem(this, ItemTariffRequestItemMetadata.ColumnNames.PriceInPurchaseUnit, esSystemType.Decimal);
            }
        }

        public esQueryItem CostPrice
        {
            get
            {
                return new esQueryItem(this, ItemTariffRequestItemMetadata.ColumnNames.CostPrice, esSystemType.Decimal);
            }
        }

        public esQueryItem IsAllowCito
        {
            get
            {
                return new esQueryItem(this, ItemTariffRequestItemMetadata.ColumnNames.IsAllowCito, esSystemType.Boolean);
            }
        }

        public esQueryItem IsCitoInPercent
        {
            get
            {
                return new esQueryItem(this, ItemTariffRequestItemMetadata.ColumnNames.IsCitoInPercent, esSystemType.Boolean);
            }
        }

        public esQueryItem CitoValue
        {
            get
            {
                return new esQueryItem(this, ItemTariffRequestItemMetadata.ColumnNames.CitoValue, esSystemType.Decimal);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, ItemTariffRequestItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, ItemTariffRequestItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem DiscPercentage
        {
            get
            {
                return new esQueryItem(this, ItemTariffRequestItemMetadata.ColumnNames.DiscPercentage, esSystemType.Decimal);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("ItemTariffRequestItemCollection")]
    public partial class ItemTariffRequestItemCollection : esItemTariffRequestItemCollection, IEnumerable<ItemTariffRequestItem>
    {
        public ItemTariffRequestItemCollection()
        {

        }

        public static implicit operator List<ItemTariffRequestItem>(ItemTariffRequestItemCollection coll)
        {
            List<ItemTariffRequestItem> list = new List<ItemTariffRequestItem>();

            foreach (ItemTariffRequestItem emp in coll)
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
                return ItemTariffRequestItemMetadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ItemTariffRequestItemQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new ItemTariffRequestItem(row);
        }

        override protected esEntity CreateEntity()
        {
            return new ItemTariffRequestItem();
        }


        #endregion


        [BrowsableAttribute(false)]
        public ItemTariffRequestItemQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ItemTariffRequestItemQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(ItemTariffRequestItemQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public ItemTariffRequestItem AddNew()
        {
            ItemTariffRequestItem entity = base.AddNewEntity() as ItemTariffRequestItem;

            return entity;
        }

        public ItemTariffRequestItem FindByPrimaryKey(System.String tariffRequestNo, System.String itemID)
        {
            return base.FindByPrimaryKey(tariffRequestNo, itemID) as ItemTariffRequestItem;
        }


        #region IEnumerable<ItemTariffRequestItem> Members

        IEnumerator<ItemTariffRequestItem> IEnumerable<ItemTariffRequestItem>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as ItemTariffRequestItem;
            }
        }

        #endregion

        private ItemTariffRequestItemQuery query;
    }


    /// <summary>
    /// Encapsulates the 'ItemTariffRequestItem' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("ItemTariffRequestItem ({TariffRequestNo},{ItemID})")]
    [Serializable]
    public partial class ItemTariffRequestItem : esItemTariffRequestItem
    {
        public ItemTariffRequestItem()
        {

        }

        public ItemTariffRequestItem(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return ItemTariffRequestItemMetadata.Meta();
            }
        }



        override protected esItemTariffRequestItemQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ItemTariffRequestItemQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public ItemTariffRequestItemQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ItemTariffRequestItemQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(ItemTariffRequestItemQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private ItemTariffRequestItemQuery query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class ItemTariffRequestItemQuery : esItemTariffRequestItemQuery
    {
        public ItemTariffRequestItemQuery()
        {

        }

        public ItemTariffRequestItemQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "ItemTariffRequestItemQuery";
        }


    }


    [Serializable]
    public partial class ItemTariffRequestItemMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected ItemTariffRequestItemMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(ItemTariffRequestItemMetadata.ColumnNames.TariffRequestNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemTariffRequestItemMetadata.PropertyNames.TariffRequestNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(ItemTariffRequestItemMetadata.ColumnNames.ItemID, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemTariffRequestItemMetadata.PropertyNames.ItemID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(ItemTariffRequestItemMetadata.ColumnNames.Price, 2, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ItemTariffRequestItemMetadata.PropertyNames.Price;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(ItemTariffRequestItemMetadata.ColumnNames.PriceInBaseUnit, 3, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ItemTariffRequestItemMetadata.PropertyNames.PriceInBaseUnit;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.HasDefault = true;
            c.Default = @"((0))";
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemTariffRequestItemMetadata.ColumnNames.PriceInBaseUnitWVat, 4, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ItemTariffRequestItemMetadata.PropertyNames.PriceInBaseUnitWVat;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.HasDefault = true;
            c.Default = @"((0))";
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemTariffRequestItemMetadata.ColumnNames.PriceInPurchaseUnit, 5, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ItemTariffRequestItemMetadata.PropertyNames.PriceInPurchaseUnit;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.HasDefault = true;
            c.Default = @"((0))";
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemTariffRequestItemMetadata.ColumnNames.CostPrice, 6, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ItemTariffRequestItemMetadata.PropertyNames.CostPrice;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.HasDefault = true;
            c.Default = @"((0))";
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemTariffRequestItemMetadata.ColumnNames.IsAllowCito, 7, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ItemTariffRequestItemMetadata.PropertyNames.IsAllowCito;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemTariffRequestItemMetadata.ColumnNames.IsCitoInPercent, 8, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ItemTariffRequestItemMetadata.PropertyNames.IsCitoInPercent;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemTariffRequestItemMetadata.ColumnNames.CitoValue, 9, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ItemTariffRequestItemMetadata.PropertyNames.CitoValue;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemTariffRequestItemMetadata.ColumnNames.LastUpdateDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ItemTariffRequestItemMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemTariffRequestItemMetadata.ColumnNames.LastUpdateByUserID, 11, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemTariffRequestItemMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 40;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemTariffRequestItemMetadata.ColumnNames.DiscPercentage, 12, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ItemTariffRequestItemMetadata.PropertyNames.DiscPercentage;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public ItemTariffRequestItemMetadata Meta()
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
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string TariffRequestNo = "TariffRequestNo";
            public const string ItemID = "ItemID";
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
            lock (typeof(ItemTariffRequestItemMetadata))
            {
                if (ItemTariffRequestItemMetadata.mapDelegates == null)
                {
                    ItemTariffRequestItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (ItemTariffRequestItemMetadata.meta == null)
                {
                    ItemTariffRequestItemMetadata.meta = new ItemTariffRequestItemMetadata();
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



                meta.Source = "ItemTariffRequestItem";
                meta.Destination = "ItemTariffRequestItem";

                meta.spInsert = "proc_ItemTariffRequestItemInsert";
                meta.spUpdate = "proc_ItemTariffRequestItemUpdate";
                meta.spDelete = "proc_ItemTariffRequestItemDelete";
                meta.spLoadAll = "proc_ItemTariffRequestItemLoadAll";
                meta.spLoadByPrimaryKey = "proc_ItemTariffRequestItemLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private ItemTariffRequestItemMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
