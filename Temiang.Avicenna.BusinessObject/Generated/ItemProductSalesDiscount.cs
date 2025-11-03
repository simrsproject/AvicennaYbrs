/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 12/13/2013 1:42:39 PM
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
    abstract public class esItemProductSalesDiscountCollection : esEntityCollectionWAuditLog
    {
        public esItemProductSalesDiscountCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "ItemProductSalesDiscountCollection";
        }

        #region Query Logic
        protected void InitQuery(esItemProductSalesDiscountQuery query)
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
            this.InitQuery(query as esItemProductSalesDiscountQuery);
        }
        #endregion

        virtual public ItemProductSalesDiscount DetachEntity(ItemProductSalesDiscount entity)
        {
            return base.DetachEntity(entity) as ItemProductSalesDiscount;
        }

        virtual public ItemProductSalesDiscount AttachEntity(ItemProductSalesDiscount entity)
        {
            return base.AttachEntity(entity) as ItemProductSalesDiscount;
        }

        virtual public void Combine(ItemProductSalesDiscountCollection collection)
        {
            base.Combine(collection);
        }

        new public ItemProductSalesDiscount this[int index]
        {
            get
            {
                return base[index] as ItemProductSalesDiscount;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(ItemProductSalesDiscount);
        }
    }



    [Serializable]
    abstract public class esItemProductSalesDiscount : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esItemProductSalesDiscountQuery GetDynamicQuery()
        {
            return null;
        }

        public esItemProductSalesDiscount()
        {

        }

        public esItemProductSalesDiscount(DataRow row)
            : base(row)
        {

        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(System.String salesDiscID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(salesDiscID);
            else
                return LoadByPrimaryKeyStoredProcedure(salesDiscID);
        }

        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String salesDiscID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(salesDiscID);
            else
                return LoadByPrimaryKeyStoredProcedure(salesDiscID);
        }

        private bool LoadByPrimaryKeyDynamic(System.String salesDiscID)
        {
            esItemProductSalesDiscountQuery query = this.GetDynamicQuery();
            query.Where(query.SalesDiscID == salesDiscID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(System.String salesDiscID)
        {
            esParameters parms = new esParameters();
            parms.Add("SalesDiscID", salesDiscID);
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
                        case "SalesDiscID": this.str.SalesDiscID = (string)value; break;
                        case "SRItemType": this.str.SRItemType = (string)value; break;
                        case "SupplierDiscPercentageFrom": this.str.SupplierDiscPercentageFrom = (string)value; break;
                        case "SupplierDiscPercentageTo": this.str.SupplierDiscPercentageTo = (string)value; break;
                        case "SalesDiscPercentage": this.str.SalesDiscPercentage = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "SupplierDiscPercentageFrom":

                            if (value == null || value is System.Decimal)
                                this.SupplierDiscPercentageFrom = (System.Decimal?)value;
                            break;

                        case "SupplierDiscPercentageTo":

                            if (value == null || value is System.Decimal)
                                this.SupplierDiscPercentageTo = (System.Decimal?)value;
                            break;

                        case "SalesDiscPercentage":

                            if (value == null || value is System.Decimal)
                                this.SalesDiscPercentage = (System.Decimal?)value;
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
        /// Maps to ItemProductSalesDiscount.SalesDiscID
        /// </summary>
        virtual public System.String SalesDiscID
        {
            get
            {
                return base.GetSystemString(ItemProductSalesDiscountMetadata.ColumnNames.SalesDiscID);
            }

            set
            {
                base.SetSystemString(ItemProductSalesDiscountMetadata.ColumnNames.SalesDiscID, value);
            }
        }

        /// <summary>
        /// Maps to ItemProductSalesDiscount.SRItemType
        /// </summary>
        virtual public System.String SRItemType
        {
            get
            {
                return base.GetSystemString(ItemProductSalesDiscountMetadata.ColumnNames.SRItemType);
            }

            set
            {
                base.SetSystemString(ItemProductSalesDiscountMetadata.ColumnNames.SRItemType, value);
            }
        }

        /// <summary>
        /// Maps to ItemProductSalesDiscount.SupplierDiscPercentageFrom
        /// </summary>
        virtual public System.Decimal? SupplierDiscPercentageFrom
        {
            get
            {
                return base.GetSystemDecimal(ItemProductSalesDiscountMetadata.ColumnNames.SupplierDiscPercentageFrom);
            }

            set
            {
                base.SetSystemDecimal(ItemProductSalesDiscountMetadata.ColumnNames.SupplierDiscPercentageFrom, value);
            }
        }

        /// <summary>
        /// Maps to ItemProductSalesDiscount.SupplierDiscPercentageTo
        /// </summary>
        virtual public System.Decimal? SupplierDiscPercentageTo
        {
            get
            {
                return base.GetSystemDecimal(ItemProductSalesDiscountMetadata.ColumnNames.SupplierDiscPercentageTo);
            }

            set
            {
                base.SetSystemDecimal(ItemProductSalesDiscountMetadata.ColumnNames.SupplierDiscPercentageTo, value);
            }
        }

        /// <summary>
        /// Maps to ItemProductSalesDiscount.SalesDiscPercentage
        /// </summary>
        virtual public System.Decimal? SalesDiscPercentage
        {
            get
            {
                return base.GetSystemDecimal(ItemProductSalesDiscountMetadata.ColumnNames.SalesDiscPercentage);
            }

            set
            {
                base.SetSystemDecimal(ItemProductSalesDiscountMetadata.ColumnNames.SalesDiscPercentage, value);
            }
        }

        /// <summary>
        /// Maps to ItemProductSalesDiscount.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(ItemProductSalesDiscountMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(ItemProductSalesDiscountMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to ItemProductSalesDiscount.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(ItemProductSalesDiscountMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(ItemProductSalesDiscountMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esItemProductSalesDiscount entity)
            {
                this.entity = entity;
            }


            public System.String SalesDiscID
            {
                get
                {
                    System.String data = entity.SalesDiscID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SalesDiscID = null;
                    else entity.SalesDiscID = Convert.ToString(value);
                }
            }

            public System.String SRItemType
            {
                get
                {
                    System.String data = entity.SRItemType;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRItemType = null;
                    else entity.SRItemType = Convert.ToString(value);
                }
            }

            public System.String SupplierDiscPercentageFrom
            {
                get
                {
                    System.Decimal? data = entity.SupplierDiscPercentageFrom;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SupplierDiscPercentageFrom = null;
                    else entity.SupplierDiscPercentageFrom = Convert.ToDecimal(value);
                }
            }

            public System.String SupplierDiscPercentageTo
            {
                get
                {
                    System.Decimal? data = entity.SupplierDiscPercentageTo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SupplierDiscPercentageTo = null;
                    else entity.SupplierDiscPercentageTo = Convert.ToDecimal(value);
                }
            }

            public System.String SalesDiscPercentage
            {
                get
                {
                    System.Decimal? data = entity.SalesDiscPercentage;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SalesDiscPercentage = null;
                    else entity.SalesDiscPercentage = Convert.ToDecimal(value);
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


            private esItemProductSalesDiscount entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esItemProductSalesDiscountQuery query)
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
                throw new Exception("esItemProductSalesDiscount can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    public partial class ItemProductSalesDiscount : esItemProductSalesDiscount
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
    abstract public class esItemProductSalesDiscountQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return ItemProductSalesDiscountMetadata.Meta();
            }
        }


        public esQueryItem SalesDiscID
        {
            get
            {
                return new esQueryItem(this, ItemProductSalesDiscountMetadata.ColumnNames.SalesDiscID, esSystemType.String);
            }
        }

        public esQueryItem SRItemType
        {
            get
            {
                return new esQueryItem(this, ItemProductSalesDiscountMetadata.ColumnNames.SRItemType, esSystemType.String);
            }
        }

        public esQueryItem SupplierDiscPercentageFrom
        {
            get
            {
                return new esQueryItem(this, ItemProductSalesDiscountMetadata.ColumnNames.SupplierDiscPercentageFrom, esSystemType.Decimal);
            }
        }

        public esQueryItem SupplierDiscPercentageTo
        {
            get
            {
                return new esQueryItem(this, ItemProductSalesDiscountMetadata.ColumnNames.SupplierDiscPercentageTo, esSystemType.Decimal);
            }
        }

        public esQueryItem SalesDiscPercentage
        {
            get
            {
                return new esQueryItem(this, ItemProductSalesDiscountMetadata.ColumnNames.SalesDiscPercentage, esSystemType.Decimal);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, ItemProductSalesDiscountMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, ItemProductSalesDiscountMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("ItemProductSalesDiscountCollection")]
    public partial class ItemProductSalesDiscountCollection : esItemProductSalesDiscountCollection, IEnumerable<ItemProductSalesDiscount>
    {
        public ItemProductSalesDiscountCollection()
        {

        }

        public static implicit operator List<ItemProductSalesDiscount>(ItemProductSalesDiscountCollection coll)
        {
            List<ItemProductSalesDiscount> list = new List<ItemProductSalesDiscount>();

            foreach (ItemProductSalesDiscount emp in coll)
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
                return ItemProductSalesDiscountMetadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ItemProductSalesDiscountQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new ItemProductSalesDiscount(row);
        }

        override protected esEntity CreateEntity()
        {
            return new ItemProductSalesDiscount();
        }


        #endregion


        [BrowsableAttribute(false)]
        public ItemProductSalesDiscountQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ItemProductSalesDiscountQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(ItemProductSalesDiscountQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public ItemProductSalesDiscount AddNew()
        {
            ItemProductSalesDiscount entity = base.AddNewEntity() as ItemProductSalesDiscount;

            return entity;
        }

        public ItemProductSalesDiscount FindByPrimaryKey(System.String salesDiscID)
        {
            return base.FindByPrimaryKey(salesDiscID) as ItemProductSalesDiscount;
        }


        #region IEnumerable<ItemProductSalesDiscount> Members

        IEnumerator<ItemProductSalesDiscount> IEnumerable<ItemProductSalesDiscount>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as ItemProductSalesDiscount;
            }
        }

        #endregion

        private ItemProductSalesDiscountQuery query;
    }


    /// <summary>
    /// Encapsulates the 'ItemProductSalesDiscount' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("ItemProductSalesDiscount ({SalesDiscID})")]
    [Serializable]
    public partial class ItemProductSalesDiscount : esItemProductSalesDiscount
    {
        public ItemProductSalesDiscount()
        {

        }

        public ItemProductSalesDiscount(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return ItemProductSalesDiscountMetadata.Meta();
            }
        }



        override protected esItemProductSalesDiscountQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ItemProductSalesDiscountQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public ItemProductSalesDiscountQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ItemProductSalesDiscountQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(ItemProductSalesDiscountQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private ItemProductSalesDiscountQuery query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class ItemProductSalesDiscountQuery : esItemProductSalesDiscountQuery
    {
        public ItemProductSalesDiscountQuery()
        {

        }

        public ItemProductSalesDiscountQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "ItemProductSalesDiscountQuery";
        }


    }


    [Serializable]
    public partial class ItemProductSalesDiscountMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected ItemProductSalesDiscountMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(ItemProductSalesDiscountMetadata.ColumnNames.SalesDiscID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemProductSalesDiscountMetadata.PropertyNames.SalesDiscID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(ItemProductSalesDiscountMetadata.ColumnNames.SRItemType, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemProductSalesDiscountMetadata.PropertyNames.SRItemType;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(ItemProductSalesDiscountMetadata.ColumnNames.SupplierDiscPercentageFrom, 2, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ItemProductSalesDiscountMetadata.PropertyNames.SupplierDiscPercentageFrom;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(ItemProductSalesDiscountMetadata.ColumnNames.SupplierDiscPercentageTo, 3, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ItemProductSalesDiscountMetadata.PropertyNames.SupplierDiscPercentageTo;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(ItemProductSalesDiscountMetadata.ColumnNames.SalesDiscPercentage, 4, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ItemProductSalesDiscountMetadata.PropertyNames.SalesDiscPercentage;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(ItemProductSalesDiscountMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ItemProductSalesDiscountMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemProductSalesDiscountMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemProductSalesDiscountMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public ItemProductSalesDiscountMetadata Meta()
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
            public const string SalesDiscID = "SalesDiscID";
            public const string SRItemType = "SRItemType";
            public const string SupplierDiscPercentageFrom = "SupplierDiscPercentageFrom";
            public const string SupplierDiscPercentageTo = "SupplierDiscPercentageTo";
            public const string SalesDiscPercentage = "SalesDiscPercentage";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string SalesDiscID = "SalesDiscID";
            public const string SRItemType = "SRItemType";
            public const string SupplierDiscPercentageFrom = "SupplierDiscPercentageFrom";
            public const string SupplierDiscPercentageTo = "SupplierDiscPercentageTo";
            public const string SalesDiscPercentage = "SalesDiscPercentage";
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
            lock (typeof(ItemProductSalesDiscountMetadata))
            {
                if (ItemProductSalesDiscountMetadata.mapDelegates == null)
                {
                    ItemProductSalesDiscountMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (ItemProductSalesDiscountMetadata.meta == null)
                {
                    ItemProductSalesDiscountMetadata.meta = new ItemProductSalesDiscountMetadata();
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


                meta.AddTypeMap("SalesDiscID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRItemType", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SupplierDiscPercentageFrom", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("SupplierDiscPercentageTo", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("SalesDiscPercentage", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));



                meta.Source = "ItemProductSalesDiscount";
                meta.Destination = "ItemProductSalesDiscount";

                meta.spInsert = "proc_ItemProductSalesDiscountInsert";
                meta.spUpdate = "proc_ItemProductSalesDiscountUpdate";
                meta.spDelete = "proc_ItemProductSalesDiscountDelete";
                meta.spLoadAll = "proc_ItemProductSalesDiscountLoadAll";
                meta.spLoadByPrimaryKey = "proc_ItemProductSalesDiscountLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private ItemProductSalesDiscountMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
