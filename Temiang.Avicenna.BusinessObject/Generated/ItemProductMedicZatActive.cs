/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 9/30/2014 9:12:59 AM
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
    abstract public class esItemProductMedicZatActiveCollection : esEntityCollection
    {
        public esItemProductMedicZatActiveCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "ItemProductMedicZatActiveCollection";
        }

        #region Query Logic
        protected void InitQuery(esItemProductMedicZatActiveQuery query)
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
            this.InitQuery(query as esItemProductMedicZatActiveQuery);
        }
        #endregion

        virtual public ItemProductMedicZatActive DetachEntity(ItemProductMedicZatActive entity)
        {
            return base.DetachEntity(entity) as ItemProductMedicZatActive;
        }

        virtual public ItemProductMedicZatActive AttachEntity(ItemProductMedicZatActive entity)
        {
            return base.AttachEntity(entity) as ItemProductMedicZatActive;
        }

        virtual public void Combine(ItemProductMedicZatActiveCollection collection)
        {
            base.Combine(collection);
        }

        new public ItemProductMedicZatActive this[int index]
        {
            get
            {
                return base[index] as ItemProductMedicZatActive;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(ItemProductMedicZatActive);
        }
    }



    [Serializable]
    abstract public class esItemProductMedicZatActive : esEntity
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esItemProductMedicZatActiveQuery GetDynamicQuery()
        {
            return null;
        }

        public esItemProductMedicZatActive()
        {

        }

        public esItemProductMedicZatActive(DataRow row)
            : base(row)
        {

        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(System.String itemID, System.String zatActiveID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(itemID, zatActiveID);
            else
                return LoadByPrimaryKeyStoredProcedure(itemID, zatActiveID);
        }

        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String itemID, System.String zatActiveID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(itemID, zatActiveID);
            else
                return LoadByPrimaryKeyStoredProcedure(itemID, zatActiveID);
        }

        private bool LoadByPrimaryKeyDynamic(System.String itemID, System.String zatActiveID)
        {
            esItemProductMedicZatActiveQuery query = this.GetDynamicQuery();
            query.Where(query.ItemID == itemID, query.ZatActiveID == zatActiveID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(System.String itemID, System.String zatActiveID)
        {
            esParameters parms = new esParameters();
            parms.Add("ItemID", itemID); parms.Add("ZatActiveID", zatActiveID);
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
                        case "ZatActiveID": this.str.ZatActiveID = (string)value; break;
                        case "InsertDateTime": this.str.InsertDateTime = (string)value; break;
                        case "InsertByUserID": this.str.InsertByUserID = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "IsPrinted": this.str.IsPrinted = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "InsertDateTime":

                            if (value == null || value is System.DateTime)
                                this.InsertDateTime = (System.DateTime?)value;
                            break;

                        case "LastUpdateDateTime":

                            if (value == null || value is System.DateTime)
                                this.LastUpdateDateTime = (System.DateTime?)value;
                            break;

                        case "IsPrinted":

                            if (value == null || value is System.Boolean)
                                this.IsPrinted = (System.Boolean?)value;
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
        /// Maps to ItemProductMedicZatActive.ItemID
        /// </summary>
        virtual public System.String ItemID
        {
            get
            {
                return base.GetSystemString(ItemProductMedicZatActiveMetadata.ColumnNames.ItemID);
            }

            set
            {
                base.SetSystemString(ItemProductMedicZatActiveMetadata.ColumnNames.ItemID, value);
            }
        }

        /// <summary>
        /// Maps to ItemProductMedicZatActive.ZatActiveID
        /// </summary>
        virtual public System.String ZatActiveID
        {
            get
            {
                return base.GetSystemString(ItemProductMedicZatActiveMetadata.ColumnNames.ZatActiveID);
            }

            set
            {
                base.SetSystemString(ItemProductMedicZatActiveMetadata.ColumnNames.ZatActiveID, value);
            }
        }

        /// <summary>
        /// Maps to ItemProductMedicZatActive.InsertDateTime
        /// </summary>
        virtual public System.DateTime? InsertDateTime
        {
            get
            {
                return base.GetSystemDateTime(ItemProductMedicZatActiveMetadata.ColumnNames.InsertDateTime);
            }

            set
            {
                base.SetSystemDateTime(ItemProductMedicZatActiveMetadata.ColumnNames.InsertDateTime, value);
            }
        }

        /// <summary>
        /// Maps to ItemProductMedicZatActive.InsertByUserID
        /// </summary>
        virtual public System.String InsertByUserID
        {
            get
            {
                return base.GetSystemString(ItemProductMedicZatActiveMetadata.ColumnNames.InsertByUserID);
            }

            set
            {
                base.SetSystemString(ItemProductMedicZatActiveMetadata.ColumnNames.InsertByUserID, value);
            }
        }

        /// <summary>
        /// Maps to ItemProductMedicZatActive.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(ItemProductMedicZatActiveMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(ItemProductMedicZatActiveMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to ItemProductMedicZatActive.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(ItemProductMedicZatActiveMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(ItemProductMedicZatActiveMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }

        /// <summary>
        /// Maps to ItemProductMedicZatActive.IsPrinted
        /// </summary>
        virtual public System.Boolean? IsPrinted
        {
            get
            {
                return base.GetSystemBoolean(ItemProductMedicZatActiveMetadata.ColumnNames.IsPrinted);
            }

            set
            {
                base.SetSystemBoolean(ItemProductMedicZatActiveMetadata.ColumnNames.IsPrinted, value);
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
            public esStrings(esItemProductMedicZatActive entity)
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

            public System.String ZatActiveID
            {
                get
                {
                    System.String data = entity.ZatActiveID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ZatActiveID = null;
                    else entity.ZatActiveID = Convert.ToString(value);
                }
            }

            public System.String InsertDateTime
            {
                get
                {
                    System.DateTime? data = entity.InsertDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.InsertDateTime = null;
                    else entity.InsertDateTime = Convert.ToDateTime(value);
                }
            }

            public System.String InsertByUserID
            {
                get
                {
                    System.String data = entity.InsertByUserID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.InsertByUserID = null;
                    else entity.InsertByUserID = Convert.ToString(value);
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

            public System.String IsPrinted
            {
                get
                {
                    System.Boolean? data = entity.IsPrinted;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsPrinted = null;
                    else entity.IsPrinted = Convert.ToBoolean(value);
                }
            }


            private esItemProductMedicZatActive entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esItemProductMedicZatActiveQuery query)
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
                throw new Exception("esItemProductMedicZatActive can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    [Serializable]
    abstract public class esItemProductMedicZatActiveQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return ItemProductMedicZatActiveMetadata.Meta();
            }
        }


        public esQueryItem ItemID
        {
            get
            {
                return new esQueryItem(this, ItemProductMedicZatActiveMetadata.ColumnNames.ItemID, esSystemType.String);
            }
        }

        public esQueryItem ZatActiveID
        {
            get
            {
                return new esQueryItem(this, ItemProductMedicZatActiveMetadata.ColumnNames.ZatActiveID, esSystemType.String);
            }
        }

        public esQueryItem InsertDateTime
        {
            get
            {
                return new esQueryItem(this, ItemProductMedicZatActiveMetadata.ColumnNames.InsertDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem InsertByUserID
        {
            get
            {
                return new esQueryItem(this, ItemProductMedicZatActiveMetadata.ColumnNames.InsertByUserID, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, ItemProductMedicZatActiveMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, ItemProductMedicZatActiveMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem IsPrinted
        {
            get
            {
                return new esQueryItem(this, ItemProductMedicZatActiveMetadata.ColumnNames.IsPrinted, esSystemType.Boolean);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("ItemProductMedicZatActiveCollection")]
    public partial class ItemProductMedicZatActiveCollection : esItemProductMedicZatActiveCollection, IEnumerable<ItemProductMedicZatActive>
    {
        public ItemProductMedicZatActiveCollection()
        {

        }

        public static implicit operator List<ItemProductMedicZatActive>(ItemProductMedicZatActiveCollection coll)
        {
            List<ItemProductMedicZatActive> list = new List<ItemProductMedicZatActive>();

            foreach (ItemProductMedicZatActive emp in coll)
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
                return ItemProductMedicZatActiveMetadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ItemProductMedicZatActiveQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new ItemProductMedicZatActive(row);
        }

        override protected esEntity CreateEntity()
        {
            return new ItemProductMedicZatActive();
        }


        #endregion


        [BrowsableAttribute(false)]
        public ItemProductMedicZatActiveQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ItemProductMedicZatActiveQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(ItemProductMedicZatActiveQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public ItemProductMedicZatActive AddNew()
        {
            ItemProductMedicZatActive entity = base.AddNewEntity() as ItemProductMedicZatActive;

            return entity;
        }

        public ItemProductMedicZatActive FindByPrimaryKey(System.String itemID, System.String zatActiveID)
        {
            return base.FindByPrimaryKey(itemID, zatActiveID) as ItemProductMedicZatActive;
        }


        #region IEnumerable<ItemProductMedicZatActive> Members

        IEnumerator<ItemProductMedicZatActive> IEnumerable<ItemProductMedicZatActive>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as ItemProductMedicZatActive;
            }
        }

        #endregion

        private ItemProductMedicZatActiveQuery query;
    }


    /// <summary>
    /// Encapsulates the 'ItemProductMedicZatActive' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("ItemProductMedicZatActive ({ItemID},{ZatActiveID})")]
    [Serializable]
    public partial class ItemProductMedicZatActive : esItemProductMedicZatActive
    {
        public ItemProductMedicZatActive()
        {

        }

        public ItemProductMedicZatActive(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return ItemProductMedicZatActiveMetadata.Meta();
            }
        }



        override protected esItemProductMedicZatActiveQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ItemProductMedicZatActiveQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public ItemProductMedicZatActiveQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ItemProductMedicZatActiveQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(ItemProductMedicZatActiveQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private ItemProductMedicZatActiveQuery query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class ItemProductMedicZatActiveQuery : esItemProductMedicZatActiveQuery
    {
        public ItemProductMedicZatActiveQuery()
        {

        }

        public ItemProductMedicZatActiveQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "ItemProductMedicZatActiveQuery";
        }


    }


    [Serializable]
    public partial class ItemProductMedicZatActiveMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected ItemProductMedicZatActiveMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(ItemProductMedicZatActiveMetadata.ColumnNames.ItemID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemProductMedicZatActiveMetadata.PropertyNames.ItemID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(ItemProductMedicZatActiveMetadata.ColumnNames.ZatActiveID, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemProductMedicZatActiveMetadata.PropertyNames.ZatActiveID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(ItemProductMedicZatActiveMetadata.ColumnNames.InsertDateTime, 2, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ItemProductMedicZatActiveMetadata.PropertyNames.InsertDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemProductMedicZatActiveMetadata.ColumnNames.InsertByUserID, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemProductMedicZatActiveMetadata.PropertyNames.InsertByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemProductMedicZatActiveMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ItemProductMedicZatActiveMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemProductMedicZatActiveMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemProductMedicZatActiveMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemProductMedicZatActiveMetadata.ColumnNames.IsPrinted, 6, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ItemProductMedicZatActiveMetadata.PropertyNames.IsPrinted;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public ItemProductMedicZatActiveMetadata Meta()
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
            public const string ZatActiveID = "ZatActiveID";
            public const string InsertDateTime = "InsertDateTime";
            public const string InsertByUserID = "InsertByUserID";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string IsPrinted = "IsPrinted";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string ItemID = "ItemID";
            public const string ZatActiveID = "ZatActiveID";
            public const string InsertDateTime = "InsertDateTime";
            public const string InsertByUserID = "InsertByUserID";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string IsPrinted = "IsPrinted";
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
            lock (typeof(ItemProductMedicZatActiveMetadata))
            {
                if (ItemProductMedicZatActiveMetadata.mapDelegates == null)
                {
                    ItemProductMedicZatActiveMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (ItemProductMedicZatActiveMetadata.meta == null)
                {
                    ItemProductMedicZatActiveMetadata.meta = new ItemProductMedicZatActiveMetadata();
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
                meta.AddTypeMap("ZatActiveID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("InsertDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("InsertByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsPrinted", new esTypeMap("bit", "System.Boolean"));



                meta.Source = "ItemProductMedicZatActive";
                meta.Destination = "ItemProductMedicZatActive";

                meta.spInsert = "proc_ItemProductMedicZatActiveInsert";
                meta.spUpdate = "proc_ItemProductMedicZatActiveUpdate";
                meta.spDelete = "proc_ItemProductMedicZatActiveDelete";
                meta.spLoadAll = "proc_ItemProductMedicZatActiveLoadAll";
                meta.spLoadByPrimaryKey = "proc_ItemProductMedicZatActiveLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private ItemProductMedicZatActiveMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
