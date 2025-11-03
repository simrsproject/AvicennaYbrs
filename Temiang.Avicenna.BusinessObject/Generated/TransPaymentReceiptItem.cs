/*
===============================================================================
                    EntitySpaces 2009 by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2009.2.1214.0
EntitySpaces Driver  : SQL
Date Generated       : 8/6/2015 11:40:06 AM
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
    abstract public class esTransPaymentReceiptItemCollection : esEntityCollection
    {
        public esTransPaymentReceiptItemCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "TransPaymentReceiptItemCollection";
        }

        #region Query Logic
        protected void InitQuery(esTransPaymentReceiptItemQuery query)
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
            this.InitQuery(query as esTransPaymentReceiptItemQuery);
        }
        #endregion

        virtual public TransPaymentReceiptItem DetachEntity(TransPaymentReceiptItem entity)
        {
            return base.DetachEntity(entity) as TransPaymentReceiptItem;
        }

        virtual public TransPaymentReceiptItem AttachEntity(TransPaymentReceiptItem entity)
        {
            return base.AttachEntity(entity) as TransPaymentReceiptItem;
        }

        virtual public void Combine(TransPaymentReceiptItemCollection collection)
        {
            base.Combine(collection);
        }

        new public TransPaymentReceiptItem this[int index]
        {
            get
            {
                return base[index] as TransPaymentReceiptItem;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(TransPaymentReceiptItem);
        }
    }



    [Serializable]
    abstract public class esTransPaymentReceiptItem : esEntity
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esTransPaymentReceiptItemQuery GetDynamicQuery()
        {
            return null;
        }

        public esTransPaymentReceiptItem()
        {

        }

        public esTransPaymentReceiptItem(DataRow row)
            : base(row)
        {

        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(System.String paymentReceiptNo, System.String paymentNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(paymentReceiptNo, paymentNo);
            else
                return LoadByPrimaryKeyStoredProcedure(paymentReceiptNo, paymentNo);
        }

        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String paymentReceiptNo, System.String paymentNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(paymentReceiptNo, paymentNo);
            else
                return LoadByPrimaryKeyStoredProcedure(paymentReceiptNo, paymentNo);
        }

        private bool LoadByPrimaryKeyDynamic(System.String paymentReceiptNo, System.String paymentNo)
        {
            esTransPaymentReceiptItemQuery query = this.GetDynamicQuery();
            query.Where(query.PaymentReceiptNo == paymentReceiptNo, query.PaymentNo == paymentNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(System.String paymentReceiptNo, System.String paymentNo)
        {
            esParameters parms = new esParameters();
            parms.Add("PaymentReceiptNo", paymentReceiptNo); parms.Add("PaymentNo", paymentNo);
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
                        case "PaymentReceiptNo": this.str.PaymentReceiptNo = (string)value; break;
                        case "PaymentNo": this.str.PaymentNo = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "Amount": this.str.Amount = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "LastUpdateDateTime":

                            if (value == null || value is System.DateTime)
                                this.LastUpdateDateTime = (System.DateTime?)value;
                            break;

                        case "Amount":

                            if (value == null || value is System.Decimal)
                                this.Amount = (System.Decimal?)value;
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
        /// Maps to TransPaymentReceiptItem.PaymentReceiptNo
        /// </summary>
        virtual public System.String PaymentReceiptNo
        {
            get
            {
                return base.GetSystemString(TransPaymentReceiptItemMetadata.ColumnNames.PaymentReceiptNo);
            }

            set
            {
                base.SetSystemString(TransPaymentReceiptItemMetadata.ColumnNames.PaymentReceiptNo, value);
            }
        }

        /// <summary>
        /// Maps to TransPaymentReceiptItem.PaymentNo
        /// </summary>
        virtual public System.String PaymentNo
        {
            get
            {
                return base.GetSystemString(TransPaymentReceiptItemMetadata.ColumnNames.PaymentNo);
            }

            set
            {
                base.SetSystemString(TransPaymentReceiptItemMetadata.ColumnNames.PaymentNo, value);
            }
        }

        /// <summary>
        /// Maps to TransPaymentReceiptItem.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(TransPaymentReceiptItemMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(TransPaymentReceiptItemMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to TransPaymentReceiptItem.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(TransPaymentReceiptItemMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(TransPaymentReceiptItemMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }

        /// <summary>
        /// Maps to TransPaymentReceiptItem.Amount
        /// </summary>
        virtual public System.Decimal? Amount
        {
            get
            {
                return base.GetSystemDecimal(TransPaymentReceiptItemMetadata.ColumnNames.Amount);
            }

            set
            {
                base.SetSystemDecimal(TransPaymentReceiptItemMetadata.ColumnNames.Amount, value);
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
            public esStrings(esTransPaymentReceiptItem entity)
            {
                this.entity = entity;
            }


            public System.String PaymentReceiptNo
            {
                get
                {
                    System.String data = entity.PaymentReceiptNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PaymentReceiptNo = null;
                    else entity.PaymentReceiptNo = Convert.ToString(value);
                }
            }

            public System.String PaymentNo
            {
                get
                {
                    System.String data = entity.PaymentNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PaymentNo = null;
                    else entity.PaymentNo = Convert.ToString(value);
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

            public System.String Amount
            {
                get
                {
                    System.Decimal? data = entity.Amount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Amount = null;
                    else entity.Amount = Convert.ToDecimal(value);
                }
            }


            private esTransPaymentReceiptItem entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esTransPaymentReceiptItemQuery query)
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
                throw new Exception("esTransPaymentReceiptItem can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    [Serializable]
    abstract public class esTransPaymentReceiptItemQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return TransPaymentReceiptItemMetadata.Meta();
            }
        }


        public esQueryItem PaymentReceiptNo
        {
            get
            {
                return new esQueryItem(this, TransPaymentReceiptItemMetadata.ColumnNames.PaymentReceiptNo, esSystemType.String);
            }
        }

        public esQueryItem PaymentNo
        {
            get
            {
                return new esQueryItem(this, TransPaymentReceiptItemMetadata.ColumnNames.PaymentNo, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, TransPaymentReceiptItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, TransPaymentReceiptItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem Amount
        {
            get
            {
                return new esQueryItem(this, TransPaymentReceiptItemMetadata.ColumnNames.Amount, esSystemType.Decimal);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("TransPaymentReceiptItemCollection")]
    public partial class TransPaymentReceiptItemCollection : esTransPaymentReceiptItemCollection, IEnumerable<TransPaymentReceiptItem>
    {
        public TransPaymentReceiptItemCollection()
        {

        }

        public static implicit operator List<TransPaymentReceiptItem>(TransPaymentReceiptItemCollection coll)
        {
            List<TransPaymentReceiptItem> list = new List<TransPaymentReceiptItem>();

            foreach (TransPaymentReceiptItem emp in coll)
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
                return TransPaymentReceiptItemMetadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new TransPaymentReceiptItemQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new TransPaymentReceiptItem(row);
        }

        override protected esEntity CreateEntity()
        {
            return new TransPaymentReceiptItem();
        }


        #endregion


        [BrowsableAttribute(false)]
        public TransPaymentReceiptItemQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new TransPaymentReceiptItemQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(TransPaymentReceiptItemQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public TransPaymentReceiptItem AddNew()
        {
            TransPaymentReceiptItem entity = base.AddNewEntity() as TransPaymentReceiptItem;

            return entity;
        }

        public TransPaymentReceiptItem FindByPrimaryKey(System.String paymentReceiptNo, System.String paymentNo)
        {
            return base.FindByPrimaryKey(paymentReceiptNo, paymentNo) as TransPaymentReceiptItem;
        }


        #region IEnumerable<TransPaymentReceiptItem> Members

        IEnumerator<TransPaymentReceiptItem> IEnumerable<TransPaymentReceiptItem>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as TransPaymentReceiptItem;
            }
        }

        #endregion

        private TransPaymentReceiptItemQuery query;
    }


    /// <summary>
    /// Encapsulates the 'TransPaymentReceiptItem' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("TransPaymentReceiptItem ({PaymentReceiptNo},{PaymentNo})")]
    [Serializable]
    public partial class TransPaymentReceiptItem : esTransPaymentReceiptItem
    {
        public TransPaymentReceiptItem()
        {

        }

        public TransPaymentReceiptItem(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return TransPaymentReceiptItemMetadata.Meta();
            }
        }



        override protected esTransPaymentReceiptItemQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new TransPaymentReceiptItemQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public TransPaymentReceiptItemQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new TransPaymentReceiptItemQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(TransPaymentReceiptItemQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private TransPaymentReceiptItemQuery query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class TransPaymentReceiptItemQuery : esTransPaymentReceiptItemQuery
    {
        public TransPaymentReceiptItemQuery()
        {

        }

        public TransPaymentReceiptItemQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "TransPaymentReceiptItemQuery";
        }


    }


    [Serializable]
    public partial class TransPaymentReceiptItemMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected TransPaymentReceiptItemMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(TransPaymentReceiptItemMetadata.ColumnNames.PaymentReceiptNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = TransPaymentReceiptItemMetadata.PropertyNames.PaymentReceiptNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(TransPaymentReceiptItemMetadata.ColumnNames.PaymentNo, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = TransPaymentReceiptItemMetadata.PropertyNames.PaymentNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(TransPaymentReceiptItemMetadata.ColumnNames.LastUpdateDateTime, 2, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = TransPaymentReceiptItemMetadata.PropertyNames.LastUpdateDateTime;
            _columns.Add(c);

            c = new esColumnMetadata(TransPaymentReceiptItemMetadata.ColumnNames.LastUpdateByUserID, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = TransPaymentReceiptItemMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 40;
            _columns.Add(c);

            c = new esColumnMetadata(TransPaymentReceiptItemMetadata.ColumnNames.Amount, 4, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = TransPaymentReceiptItemMetadata.PropertyNames.Amount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public TransPaymentReceiptItemMetadata Meta()
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
            public const string PaymentReceiptNo = "PaymentReceiptNo";
            public const string PaymentNo = "PaymentNo";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string Amount = "Amount";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string PaymentReceiptNo = "PaymentReceiptNo";
            public const string PaymentNo = "PaymentNo";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string Amount = "Amount";
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
            lock (typeof(TransPaymentReceiptItemMetadata))
            {
                if (TransPaymentReceiptItemMetadata.mapDelegates == null)
                {
                    TransPaymentReceiptItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (TransPaymentReceiptItemMetadata.meta == null)
                {
                    TransPaymentReceiptItemMetadata.meta = new TransPaymentReceiptItemMetadata();
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


                meta.AddTypeMap("PaymentReceiptNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("PaymentNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Amount", new esTypeMap("numeric", "System.Decimal"));



                meta.Source = "TransPaymentReceiptItem";
                meta.Destination = "TransPaymentReceiptItem";

                meta.spInsert = "proc_TransPaymentReceiptItemInsert";
                meta.spUpdate = "proc_TransPaymentReceiptItemUpdate";
                meta.spDelete = "proc_TransPaymentReceiptItemDelete";
                meta.spLoadAll = "proc_TransPaymentReceiptItemLoadAll";
                meta.spLoadByPrimaryKey = "proc_TransPaymentReceiptItemLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private TransPaymentReceiptItemMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
