/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 6/12/2012 11:17:35 AM
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
    abstract public class esSupplierContractItemCollection : esEntityCollectionWAuditLog
    {
        public esSupplierContractItemCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "SupplierContractItemCollection";
        }

        #region Query Logic
        protected void InitQuery(esSupplierContractItemQuery query)
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
            this.InitQuery(query as esSupplierContractItemQuery);
        }
        #endregion

        virtual public SupplierContractItem DetachEntity(SupplierContractItem entity)
        {
            return base.DetachEntity(entity) as SupplierContractItem;
        }

        virtual public SupplierContractItem AttachEntity(SupplierContractItem entity)
        {
            return base.AttachEntity(entity) as SupplierContractItem;
        }

        virtual public void Combine(SupplierContractItemCollection collection)
        {
            base.Combine(collection);
        }

        new public SupplierContractItem this[int index]
        {
            get
            {
                return base[index] as SupplierContractItem;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(SupplierContractItem);
        }
    }



    [Serializable]
    abstract public class esSupplierContractItem : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esSupplierContractItemQuery GetDynamicQuery()
        {
            return null;
        }

        public esSupplierContractItem()
        {

        }

        public esSupplierContractItem(DataRow row)
            : base(row)
        {

        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(System.String transactionNo, System.String itemID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(transactionNo, itemID);
            else
                return LoadByPrimaryKeyStoredProcedure(transactionNo, itemID);
        }

        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String transactionNo, System.String itemID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(transactionNo, itemID);
            else
                return LoadByPrimaryKeyStoredProcedure(transactionNo, itemID);
        }

        private bool LoadByPrimaryKeyDynamic(System.String transactionNo, System.String itemID)
        {
            esSupplierContractItemQuery query = this.GetDynamicQuery();
            query.Where(query.TransactionNo == transactionNo, query.ItemID == itemID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(System.String transactionNo, System.String itemID)
        {
            esParameters parms = new esParameters();
            parms.Add("TransactionNo", transactionNo); parms.Add("ItemID", itemID);
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
                        case "TransactionNo": this.str.TransactionNo = (string)value; break;
                        case "ItemID": this.str.ItemID = (string)value; break;
                        case "SRPurchaseUnit": this.str.SRPurchaseUnit = (string)value; break;
                        case "PriceInPurchaseUnit": this.str.PriceInPurchaseUnit = (string)value; break;
                        case "PurchaseDiscount1": this.str.PurchaseDiscount1 = (string)value; break;
                        case "PurchaseDiscount2": this.str.PurchaseDiscount2 = (string)value; break;
                        case "IsActive": this.str.IsActive = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "PriceInPurchaseUnit":

                            if (value == null || value is System.Decimal)
                                this.PriceInPurchaseUnit = (System.Decimal?)value;
                            break;

                        case "PurchaseDiscount1":

                            if (value == null || value is System.Decimal)
                                this.PurchaseDiscount1 = (System.Decimal?)value;
                            break;

                        case "PurchaseDiscount2":

                            if (value == null || value is System.Decimal)
                                this.PurchaseDiscount2 = (System.Decimal?)value;
                            break;

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
        /// Maps to SupplierContractItem.TransactionNo
        /// </summary>
        virtual public System.String TransactionNo
        {
            get
            {
                return base.GetSystemString(SupplierContractItemMetadata.ColumnNames.TransactionNo);
            }

            set
            {
                base.SetSystemString(SupplierContractItemMetadata.ColumnNames.TransactionNo, value);
            }
        }

        /// <summary>
        /// Maps to SupplierContractItem.ItemID
        /// </summary>
        virtual public System.String ItemID
        {
            get
            {
                return base.GetSystemString(SupplierContractItemMetadata.ColumnNames.ItemID);
            }

            set
            {
                base.SetSystemString(SupplierContractItemMetadata.ColumnNames.ItemID, value);
            }
        }

        /// <summary>
        /// Maps to SupplierContractItem.SRPurchaseUnit
        /// </summary>
        virtual public System.String SRPurchaseUnit
        {
            get
            {
                return base.GetSystemString(SupplierContractItemMetadata.ColumnNames.SRPurchaseUnit);
            }

            set
            {
                base.SetSystemString(SupplierContractItemMetadata.ColumnNames.SRPurchaseUnit, value);
            }
        }

        /// <summary>
        /// Maps to SupplierContractItem.PriceInPurchaseUnit
        /// </summary>
        virtual public System.Decimal? PriceInPurchaseUnit
        {
            get
            {
                return base.GetSystemDecimal(SupplierContractItemMetadata.ColumnNames.PriceInPurchaseUnit);
            }

            set
            {
                base.SetSystemDecimal(SupplierContractItemMetadata.ColumnNames.PriceInPurchaseUnit, value);
            }
        }

        /// <summary>
        /// Maps to SupplierContractItem.PurchaseDiscount1
        /// </summary>
        virtual public System.Decimal? PurchaseDiscount1
        {
            get
            {
                return base.GetSystemDecimal(SupplierContractItemMetadata.ColumnNames.PurchaseDiscount1);
            }

            set
            {
                base.SetSystemDecimal(SupplierContractItemMetadata.ColumnNames.PurchaseDiscount1, value);
            }
        }

        /// <summary>
        /// Maps to SupplierContractItem.PurchaseDiscount2
        /// </summary>
        virtual public System.Decimal? PurchaseDiscount2
        {
            get
            {
                return base.GetSystemDecimal(SupplierContractItemMetadata.ColumnNames.PurchaseDiscount2);
            }

            set
            {
                base.SetSystemDecimal(SupplierContractItemMetadata.ColumnNames.PurchaseDiscount2, value);
            }
        }

        /// <summary>
        /// Maps to SupplierContractItem.IsActive
        /// </summary>
        virtual public System.Boolean? IsActive
        {
            get
            {
                return base.GetSystemBoolean(SupplierContractItemMetadata.ColumnNames.IsActive);
            }

            set
            {
                base.SetSystemBoolean(SupplierContractItemMetadata.ColumnNames.IsActive, value);
            }
        }

        /// <summary>
        /// Maps to SupplierContractItem.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(SupplierContractItemMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(SupplierContractItemMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to SupplierContractItem.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(SupplierContractItemMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(SupplierContractItemMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esSupplierContractItem entity)
            {
                this.entity = entity;
            }


            public System.String TransactionNo
            {
                get
                {
                    System.String data = entity.TransactionNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TransactionNo = null;
                    else entity.TransactionNo = Convert.ToString(value);
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


            private esSupplierContractItem entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esSupplierContractItemQuery query)
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
                throw new Exception("esSupplierContractItem can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    public partial class SupplierContractItem : esSupplierContractItem
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
    abstract public class esSupplierContractItemQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return SupplierContractItemMetadata.Meta();
            }
        }


        public esQueryItem TransactionNo
        {
            get
            {
                return new esQueryItem(this, SupplierContractItemMetadata.ColumnNames.TransactionNo, esSystemType.String);
            }
        }

        public esQueryItem ItemID
        {
            get
            {
                return new esQueryItem(this, SupplierContractItemMetadata.ColumnNames.ItemID, esSystemType.String);
            }
        }

        public esQueryItem SRPurchaseUnit
        {
            get
            {
                return new esQueryItem(this, SupplierContractItemMetadata.ColumnNames.SRPurchaseUnit, esSystemType.String);
            }
        }

        public esQueryItem PriceInPurchaseUnit
        {
            get
            {
                return new esQueryItem(this, SupplierContractItemMetadata.ColumnNames.PriceInPurchaseUnit, esSystemType.Decimal);
            }
        }

        public esQueryItem PurchaseDiscount1
        {
            get
            {
                return new esQueryItem(this, SupplierContractItemMetadata.ColumnNames.PurchaseDiscount1, esSystemType.Decimal);
            }
        }

        public esQueryItem PurchaseDiscount2
        {
            get
            {
                return new esQueryItem(this, SupplierContractItemMetadata.ColumnNames.PurchaseDiscount2, esSystemType.Decimal);
            }
        }

        public esQueryItem IsActive
        {
            get
            {
                return new esQueryItem(this, SupplierContractItemMetadata.ColumnNames.IsActive, esSystemType.Boolean);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, SupplierContractItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, SupplierContractItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("SupplierContractItemCollection")]
    public partial class SupplierContractItemCollection : esSupplierContractItemCollection, IEnumerable<SupplierContractItem>
    {
        public SupplierContractItemCollection()
        {

        }

        public static implicit operator List<SupplierContractItem>(SupplierContractItemCollection coll)
        {
            List<SupplierContractItem> list = new List<SupplierContractItem>();

            foreach (SupplierContractItem emp in coll)
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
                return SupplierContractItemMetadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new SupplierContractItemQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new SupplierContractItem(row);
        }

        override protected esEntity CreateEntity()
        {
            return new SupplierContractItem();
        }


        #endregion


        [BrowsableAttribute(false)]
        public SupplierContractItemQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new SupplierContractItemQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(SupplierContractItemQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public SupplierContractItem AddNew()
        {
            SupplierContractItem entity = base.AddNewEntity() as SupplierContractItem;

            return entity;
        }

        public SupplierContractItem FindByPrimaryKey(System.String transactionNo, System.String itemID)
        {
            return base.FindByPrimaryKey(transactionNo, itemID) as SupplierContractItem;
        }


        #region IEnumerable<SupplierContractItem> Members

        IEnumerator<SupplierContractItem> IEnumerable<SupplierContractItem>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as SupplierContractItem;
            }
        }

        #endregion

        private SupplierContractItemQuery query;
    }


    /// <summary>
    /// Encapsulates the 'SupplierContractItem' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("SupplierContractItem ({TransactionNo},{ItemID})")]
    [Serializable]
    public partial class SupplierContractItem : esSupplierContractItem
    {
        public SupplierContractItem()
        {

        }

        public SupplierContractItem(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return SupplierContractItemMetadata.Meta();
            }
        }



        override protected esSupplierContractItemQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new SupplierContractItemQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public SupplierContractItemQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new SupplierContractItemQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(SupplierContractItemQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private SupplierContractItemQuery query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class SupplierContractItemQuery : esSupplierContractItemQuery
    {
        public SupplierContractItemQuery()
        {

        }

        public SupplierContractItemQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "SupplierContractItemQuery";
        }


    }


    [Serializable]
    public partial class SupplierContractItemMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected SupplierContractItemMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(SupplierContractItemMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = SupplierContractItemMetadata.PropertyNames.TransactionNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(SupplierContractItemMetadata.ColumnNames.ItemID, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = SupplierContractItemMetadata.PropertyNames.ItemID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(SupplierContractItemMetadata.ColumnNames.SRPurchaseUnit, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = SupplierContractItemMetadata.PropertyNames.SRPurchaseUnit;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(SupplierContractItemMetadata.ColumnNames.PriceInPurchaseUnit, 3, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = SupplierContractItemMetadata.PropertyNames.PriceInPurchaseUnit;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(SupplierContractItemMetadata.ColumnNames.PurchaseDiscount1, 4, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = SupplierContractItemMetadata.PropertyNames.PurchaseDiscount1;
            c.NumericPrecision = 5;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(SupplierContractItemMetadata.ColumnNames.PurchaseDiscount2, 5, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = SupplierContractItemMetadata.PropertyNames.PurchaseDiscount2;
            c.NumericPrecision = 5;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(SupplierContractItemMetadata.ColumnNames.IsActive, 6, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = SupplierContractItemMetadata.PropertyNames.IsActive;
            _columns.Add(c);

            c = new esColumnMetadata(SupplierContractItemMetadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = SupplierContractItemMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(SupplierContractItemMetadata.ColumnNames.LastUpdateByUserID, 8, typeof(System.String), esSystemType.String);
            c.PropertyName = SupplierContractItemMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public SupplierContractItemMetadata Meta()
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
            public const string TransactionNo = "TransactionNo";
            public const string ItemID = "ItemID";
            public const string SRPurchaseUnit = "SRPurchaseUnit";
            public const string PriceInPurchaseUnit = "PriceInPurchaseUnit";
            public const string PurchaseDiscount1 = "PurchaseDiscount1";
            public const string PurchaseDiscount2 = "PurchaseDiscount2";
            public const string IsActive = "IsActive";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string TransactionNo = "TransactionNo";
            public const string ItemID = "ItemID";
            public const string SRPurchaseUnit = "SRPurchaseUnit";
            public const string PriceInPurchaseUnit = "PriceInPurchaseUnit";
            public const string PurchaseDiscount1 = "PurchaseDiscount1";
            public const string PurchaseDiscount2 = "PurchaseDiscount2";
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
            lock (typeof(SupplierContractItemMetadata))
            {
                if (SupplierContractItemMetadata.mapDelegates == null)
                {
                    SupplierContractItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (SupplierContractItemMetadata.meta == null)
                {
                    SupplierContractItemMetadata.meta = new SupplierContractItemMetadata();
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


                meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRPurchaseUnit", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("PriceInPurchaseUnit", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("PurchaseDiscount1", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("PurchaseDiscount2", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));



                meta.Source = "SupplierContractItem";
                meta.Destination = "SupplierContractItem";

                meta.spInsert = "proc_SupplierContractItemInsert";
                meta.spUpdate = "proc_SupplierContractItemUpdate";
                meta.spDelete = "proc_SupplierContractItemDelete";
                meta.spLoadAll = "proc_SupplierContractItemLoadAll";
                meta.spLoadByPrimaryKey = "proc_SupplierContractItemLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private SupplierContractItemMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
