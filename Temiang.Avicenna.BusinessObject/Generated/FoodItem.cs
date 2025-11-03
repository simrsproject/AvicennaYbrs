/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 4/13/2015 10:07:38 AM
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
    abstract public class esFoodItemCollection : esEntityCollectionWAuditLog
    {
        public esFoodItemCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "FoodItemCollection";
        }

        #region Query Logic
        protected void InitQuery(esFoodItemQuery query)
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
            this.InitQuery(query as esFoodItemQuery);
        }
        #endregion

        virtual public FoodItem DetachEntity(FoodItem entity)
        {
            return base.DetachEntity(entity) as FoodItem;
        }

        virtual public FoodItem AttachEntity(FoodItem entity)
        {
            return base.AttachEntity(entity) as FoodItem;
        }

        virtual public void Combine(FoodItemCollection collection)
        {
            base.Combine(collection);
        }

        new public FoodItem this[int index]
        {
            get
            {
                return base[index] as FoodItem;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(FoodItem);
        }
    }



    [Serializable]
    abstract public class esFoodItem : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esFoodItemQuery GetDynamicQuery()
        {
            return null;
        }

        public esFoodItem()
        {

        }

        public esFoodItem(DataRow row)
            : base(row)
        {

        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(System.String foodID, System.String itemID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(foodID, itemID);
            else
                return LoadByPrimaryKeyStoredProcedure(foodID, itemID);
        }

        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String foodID, System.String itemID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(foodID, itemID);
            else
                return LoadByPrimaryKeyStoredProcedure(foodID, itemID);
        }

        private bool LoadByPrimaryKeyDynamic(System.String foodID, System.String itemID)
        {
            esFoodItemQuery query = this.GetDynamicQuery();
            query.Where(query.FoodID == foodID, query.ItemID == itemID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(System.String foodID, System.String itemID)
        {
            esParameters parms = new esParameters();
            parms.Add("FoodID", foodID); parms.Add("ItemID", itemID);
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
                        case "FoodID": this.str.FoodID = (string)value; break;
                        case "ItemID": this.str.ItemID = (string)value; break;
                        case "Qty": this.str.Qty = (string)value; break;
                        case "SRItemUnit": this.str.SRItemUnit = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "Qty":

                            if (value == null || value is System.Decimal)
                                this.Qty = (System.Decimal?)value;
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
        /// Maps to FoodItem.FoodID
        /// </summary>
        virtual public System.String FoodID
        {
            get
            {
                return base.GetSystemString(FoodItemMetadata.ColumnNames.FoodID);
            }

            set
            {
                base.SetSystemString(FoodItemMetadata.ColumnNames.FoodID, value);
            }
        }

        /// <summary>
        /// Maps to FoodItem.ItemID
        /// </summary>
        virtual public System.String ItemID
        {
            get
            {
                return base.GetSystemString(FoodItemMetadata.ColumnNames.ItemID);
            }

            set
            {
                base.SetSystemString(FoodItemMetadata.ColumnNames.ItemID, value);
            }
        }

        /// <summary>
        /// Maps to FoodItem.Qty
        /// </summary>
        virtual public System.Decimal? Qty
        {
            get
            {
                return base.GetSystemDecimal(FoodItemMetadata.ColumnNames.Qty);
            }

            set
            {
                base.SetSystemDecimal(FoodItemMetadata.ColumnNames.Qty, value);
            }
        }

        /// <summary>
        /// Maps to FoodItem.SRItemUnit
        /// </summary>
        virtual public System.String SRItemUnit
        {
            get
            {
                return base.GetSystemString(FoodItemMetadata.ColumnNames.SRItemUnit);
            }

            set
            {
                base.SetSystemString(FoodItemMetadata.ColumnNames.SRItemUnit, value);
            }
        }

        /// <summary>
        /// Maps to FoodItem.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(FoodItemMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(FoodItemMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to FoodItem.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(FoodItemMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(FoodItemMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esFoodItem entity)
            {
                this.entity = entity;
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

            public System.String Qty
            {
                get
                {
                    System.Decimal? data = entity.Qty;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Qty = null;
                    else entity.Qty = Convert.ToDecimal(value);
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


            private esFoodItem entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esFoodItemQuery query)
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
                throw new Exception("esFoodItem can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    public partial class FoodItem : esFoodItem
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
    abstract public class esFoodItemQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return FoodItemMetadata.Meta();
            }
        }


        public esQueryItem FoodID
        {
            get
            {
                return new esQueryItem(this, FoodItemMetadata.ColumnNames.FoodID, esSystemType.String);
            }
        }

        public esQueryItem ItemID
        {
            get
            {
                return new esQueryItem(this, FoodItemMetadata.ColumnNames.ItemID, esSystemType.String);
            }
        }

        public esQueryItem Qty
        {
            get
            {
                return new esQueryItem(this, FoodItemMetadata.ColumnNames.Qty, esSystemType.Decimal);
            }
        }

        public esQueryItem SRItemUnit
        {
            get
            {
                return new esQueryItem(this, FoodItemMetadata.ColumnNames.SRItemUnit, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, FoodItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, FoodItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("FoodItemCollection")]
    public partial class FoodItemCollection : esFoodItemCollection, IEnumerable<FoodItem>
    {
        public FoodItemCollection()
        {

        }

        public static implicit operator List<FoodItem>(FoodItemCollection coll)
        {
            List<FoodItem> list = new List<FoodItem>();

            foreach (FoodItem emp in coll)
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
                return FoodItemMetadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new FoodItemQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new FoodItem(row);
        }

        override protected esEntity CreateEntity()
        {
            return new FoodItem();
        }


        #endregion


        [BrowsableAttribute(false)]
        public FoodItemQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new FoodItemQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(FoodItemQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public FoodItem AddNew()
        {
            FoodItem entity = base.AddNewEntity() as FoodItem;

            return entity;
        }

        public FoodItem FindByPrimaryKey(System.String foodID, System.String itemID)
        {
            return base.FindByPrimaryKey(foodID, itemID) as FoodItem;
        }


        #region IEnumerable<FoodItem> Members

        IEnumerator<FoodItem> IEnumerable<FoodItem>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as FoodItem;
            }
        }

        #endregion

        private FoodItemQuery query;
    }


    /// <summary>
    /// Encapsulates the 'FoodItem' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("FoodItem ({FoodID},{ItemID})")]
    [Serializable]
    public partial class FoodItem : esFoodItem
    {
        public FoodItem()
        {

        }

        public FoodItem(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return FoodItemMetadata.Meta();
            }
        }



        override protected esFoodItemQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new FoodItemQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public FoodItemQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new FoodItemQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(FoodItemQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private FoodItemQuery query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class FoodItemQuery : esFoodItemQuery
    {
        public FoodItemQuery()
        {

        }

        public FoodItemQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "FoodItemQuery";
        }


    }


    [Serializable]
    public partial class FoodItemMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected FoodItemMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(FoodItemMetadata.ColumnNames.FoodID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = FoodItemMetadata.PropertyNames.FoodID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(FoodItemMetadata.ColumnNames.ItemID, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = FoodItemMetadata.PropertyNames.ItemID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(FoodItemMetadata.ColumnNames.Qty, 2, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = FoodItemMetadata.PropertyNames.Qty;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(FoodItemMetadata.ColumnNames.SRItemUnit, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = FoodItemMetadata.PropertyNames.SRItemUnit;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(FoodItemMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = FoodItemMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(FoodItemMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = FoodItemMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public FoodItemMetadata Meta()
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
            public const string FoodID = "FoodID";
            public const string ItemID = "ItemID";
            public const string Qty = "Qty";
            public const string SRItemUnit = "SRItemUnit";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string FoodID = "FoodID";
            public const string ItemID = "ItemID";
            public const string Qty = "Qty";
            public const string SRItemUnit = "SRItemUnit";
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
            lock (typeof(FoodItemMetadata))
            {
                if (FoodItemMetadata.mapDelegates == null)
                {
                    FoodItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (FoodItemMetadata.meta == null)
                {
                    FoodItemMetadata.meta = new FoodItemMetadata();
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


                meta.AddTypeMap("FoodID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Qty", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("SRItemUnit", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));



                meta.Source = "FoodItem";
                meta.Destination = "FoodItem";

                meta.spInsert = "proc_FoodItemInsert";
                meta.spUpdate = "proc_FoodItemUpdate";
                meta.spDelete = "proc_FoodItemDelete";
                meta.spLoadAll = "proc_FoodItemLoadAll";
                meta.spLoadByPrimaryKey = "proc_FoodItemLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private FoodItemMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
