/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 6/19/2015 12:14:08 PM
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
    abstract public class esVwTransactionItemCollection : esEntityCollectionWAuditLog
    {
        public esVwTransactionItemCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "VwTransactionItemCollection";
        }

        #region Query Logic
        protected void InitQuery(esVwTransactionItemQuery query)
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
            this.InitQuery(query as esVwTransactionItemQuery);
        }
        #endregion

        virtual public VwTransactionItem DetachEntity(VwTransactionItem entity)
        {
            return base.DetachEntity(entity) as VwTransactionItem;
        }

        virtual public VwTransactionItem AttachEntity(VwTransactionItem entity)
        {
            return base.AttachEntity(entity) as VwTransactionItem;
        }

        virtual public void Combine(VwTransactionItemCollection collection)
        {
            base.Combine(collection);
        }

        new public VwTransactionItem this[int index]
        {
            get
            {
                return base[index] as VwTransactionItem;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(VwTransactionItem);
        }
    }



    [Serializable]
    abstract public class esVwTransactionItem : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esVwTransactionItemQuery GetDynamicQuery()
        {
            return null;
        }

        public esVwTransactionItem()
        {

        }

        public esVwTransactionItem(DataRow row)
            : base(row)
        {

        }

        #region LoadByPrimaryKey

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
                        case "TxType": this.str.TxType = (string)value; break;
                        case "TransactionNo": this.str.TransactionNo = (string)value; break;
                        case "SequenceNo": this.str.SequenceNo = (string)value; break;
                        case "ParamedicCollectionName": this.str.ParamedicCollectionName = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "TxType":

                            if (value == null || value is System.Int32)
                                this.TxType = (System.Int32?)value;
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
        /// Maps to vw_TransactionItem.TxType
        /// </summary>
        virtual public System.Int32? TxType
        {
            get
            {
                return base.GetSystemInt32(VwTransactionItemMetadata.ColumnNames.TxType);
            }

            set
            {
                base.SetSystemInt32(VwTransactionItemMetadata.ColumnNames.TxType, value);
            }
        }

        /// <summary>
        /// Maps to vw_TransactionItem.TransactionNo
        /// </summary>
        virtual public System.String TransactionNo
        {
            get
            {
                return base.GetSystemString(VwTransactionItemMetadata.ColumnNames.TransactionNo);
            }

            set
            {
                base.SetSystemString(VwTransactionItemMetadata.ColumnNames.TransactionNo, value);
            }
        }

        /// <summary>
        /// Maps to vw_TransactionItem.SequenceNo
        /// </summary>
        virtual public System.String SequenceNo
        {
            get
            {
                return base.GetSystemString(VwTransactionItemMetadata.ColumnNames.SequenceNo);
            }

            set
            {
                base.SetSystemString(VwTransactionItemMetadata.ColumnNames.SequenceNo, value);
            }
        }

        /// <summary>
        /// Maps to vw_TransactionItem.ParamedicCollectionName
        /// </summary>
        virtual public System.String ParamedicCollectionName
        {
            get
            {
                return base.GetSystemString(VwTransactionItemMetadata.ColumnNames.ParamedicCollectionName);
            }

            set
            {
                base.SetSystemString(VwTransactionItemMetadata.ColumnNames.ParamedicCollectionName, value);
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
            public esStrings(esVwTransactionItem entity)
            {
                this.entity = entity;
            }


            public System.String TxType
            {
                get
                {
                    System.Int32? data = entity.TxType;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TxType = null;
                    else entity.TxType = Convert.ToInt32(value);
                }
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

            public System.String SequenceNo
            {
                get
                {
                    System.String data = entity.SequenceNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SequenceNo = null;
                    else entity.SequenceNo = Convert.ToString(value);
                }
            }

            public System.String ParamedicCollectionName
            {
                get
                {
                    System.String data = entity.ParamedicCollectionName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ParamedicCollectionName = null;
                    else entity.ParamedicCollectionName = Convert.ToString(value);
                }
            }


            private esVwTransactionItem entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esVwTransactionItemQuery query)
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
                throw new Exception("esVwTransactionItem can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    [Serializable]
    abstract public class esVwTransactionItemQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return VwTransactionItemMetadata.Meta();
            }
        }


        public esQueryItem TxType
        {
            get
            {
                return new esQueryItem(this, VwTransactionItemMetadata.ColumnNames.TxType, esSystemType.Int32);
            }
        }

        public esQueryItem TransactionNo
        {
            get
            {
                return new esQueryItem(this, VwTransactionItemMetadata.ColumnNames.TransactionNo, esSystemType.String);
            }
        }

        public esQueryItem SequenceNo
        {
            get
            {
                return new esQueryItem(this, VwTransactionItemMetadata.ColumnNames.SequenceNo, esSystemType.String);
            }
        }

        public esQueryItem ParamedicCollectionName
        {
            get
            {
                return new esQueryItem(this, VwTransactionItemMetadata.ColumnNames.ParamedicCollectionName, esSystemType.String);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("VwTransactionItemCollection")]
    public partial class VwTransactionItemCollection : esVwTransactionItemCollection, IEnumerable<VwTransactionItem>
    {
        public VwTransactionItemCollection()
        {

        }

        public static implicit operator List<VwTransactionItem>(VwTransactionItemCollection coll)
        {
            List<VwTransactionItem> list = new List<VwTransactionItem>();

            foreach (VwTransactionItem emp in coll)
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
                return VwTransactionItemMetadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new VwTransactionItemQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new VwTransactionItem(row);
        }

        override protected esEntity CreateEntity()
        {
            return new VwTransactionItem();
        }


        override public bool LoadAll()
        {
            return base.LoadAll(esSqlAccessType.DynamicSQL);
        }

        #endregion


        [BrowsableAttribute(false)]
        public VwTransactionItemQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new VwTransactionItemQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(VwTransactionItemQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public VwTransactionItem AddNew()
        {
            VwTransactionItem entity = base.AddNewEntity() as VwTransactionItem;

            return entity;
        }


        #region IEnumerable<VwTransactionItem> Members

        IEnumerator<VwTransactionItem> IEnumerable<VwTransactionItem>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as VwTransactionItem;
            }
        }

        #endregion

        private VwTransactionItemQuery query;
    }


    /// <summary>
    /// Encapsulates the 'vw_TransactionItem' view
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("VwTransactionItem ()")]
    [Serializable]
    public partial class VwTransactionItem : esVwTransactionItem
    {
        public VwTransactionItem()
        {

        }

        public VwTransactionItem(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return VwTransactionItemMetadata.Meta();
            }
        }



        override protected esVwTransactionItemQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new VwTransactionItemQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public VwTransactionItemQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new VwTransactionItemQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(VwTransactionItemQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private VwTransactionItemQuery query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class VwTransactionItemQuery : esVwTransactionItemQuery
    {
        public VwTransactionItemQuery()
        {

        }

        public VwTransactionItemQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "VwTransactionItemQuery";
        }


    }


    [Serializable]
    public partial class VwTransactionItemMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected VwTransactionItemMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(VwTransactionItemMetadata.ColumnNames.TxType, 0, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = VwTransactionItemMetadata.PropertyNames.TxType;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(VwTransactionItemMetadata.ColumnNames.TransactionNo, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = VwTransactionItemMetadata.PropertyNames.TransactionNo;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(VwTransactionItemMetadata.ColumnNames.SequenceNo, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = VwTransactionItemMetadata.PropertyNames.SequenceNo;
            c.CharacterMaxLength = 7;
            _columns.Add(c);

            c = new esColumnMetadata(VwTransactionItemMetadata.ColumnNames.ParamedicCollectionName, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = VwTransactionItemMetadata.PropertyNames.ParamedicCollectionName;
            c.CharacterMaxLength = 250;
            _columns.Add(c);

        }
        #endregion

        static public VwTransactionItemMetadata Meta()
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
            public const string TxType = "TxType";
            public const string TransactionNo = "TransactionNo";
            public const string SequenceNo = "SequenceNo";
            public const string ParamedicCollectionName = "ParamedicCollectionName";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string TxType = "TxType";
            public const string TransactionNo = "TransactionNo";
            public const string SequenceNo = "SequenceNo";
            public const string ParamedicCollectionName = "ParamedicCollectionName";
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
            lock (typeof(VwTransactionItemMetadata))
            {
                if (VwTransactionItemMetadata.mapDelegates == null)
                {
                    VwTransactionItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (VwTransactionItemMetadata.meta == null)
                {
                    VwTransactionItemMetadata.meta = new VwTransactionItemMetadata();
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


                meta.AddTypeMap("TxType", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SequenceNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ParamedicCollectionName", new esTypeMap("varchar", "System.String"));



                meta.Source = "vw_TransactionItem";
                meta.Destination = "vw_TransactionItem";

                meta.spInsert = "proc_vw_TransactionItemInsert";
                meta.spUpdate = "proc_vw_TransactionItemUpdate";
                meta.spDelete = "proc_vw_TransactionItemDelete";
                meta.spLoadAll = "proc_vw_TransactionItemLoadAll";
                meta.spLoadByPrimaryKey = "proc_vw_TransactionItemLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private VwTransactionItemMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
