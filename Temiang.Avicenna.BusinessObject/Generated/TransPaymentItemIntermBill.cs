/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 7/30/2012 9:51:52 AM
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
    abstract public class esTransPaymentItemIntermBillCollection : esEntityCollectionWAuditLog
    {
        public esTransPaymentItemIntermBillCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "TransPaymentItemIntermBillCollection";
        }

        #region Query Logic
        protected void InitQuery(esTransPaymentItemIntermBillQuery query)
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
            this.InitQuery(query as esTransPaymentItemIntermBillQuery);
        }
        #endregion

        virtual public TransPaymentItemIntermBill DetachEntity(TransPaymentItemIntermBill entity)
        {
            return base.DetachEntity(entity) as TransPaymentItemIntermBill;
        }

        virtual public TransPaymentItemIntermBill AttachEntity(TransPaymentItemIntermBill entity)
        {
            return base.AttachEntity(entity) as TransPaymentItemIntermBill;
        }

        virtual public void Combine(TransPaymentItemIntermBillCollection collection)
        {
            base.Combine(collection);
        }

        new public TransPaymentItemIntermBill this[int index]
        {
            get
            {
                return base[index] as TransPaymentItemIntermBill;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(TransPaymentItemIntermBill);
        }
    }



    [Serializable]
    abstract public class esTransPaymentItemIntermBill : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esTransPaymentItemIntermBillQuery GetDynamicQuery()
        {
            return null;
        }

        public esTransPaymentItemIntermBill()
        {

        }

        public esTransPaymentItemIntermBill(DataRow row)
            : base(row)
        {

        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(System.String paymentNo, System.String intermBillNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(paymentNo, intermBillNo);
            else
                return LoadByPrimaryKeyStoredProcedure(paymentNo, intermBillNo);
        }

        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String paymentNo, System.String intermBillNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(paymentNo, intermBillNo);
            else
                return LoadByPrimaryKeyStoredProcedure(paymentNo, intermBillNo);
        }

        private bool LoadByPrimaryKeyDynamic(System.String paymentNo, System.String intermBillNo)
        {
            esTransPaymentItemIntermBillQuery query = this.GetDynamicQuery();
            query.Where(query.PaymentNo == paymentNo, query.IntermBillNo == intermBillNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(System.String paymentNo, System.String intermBillNo)
        {
            esParameters parms = new esParameters();
            parms.Add("PaymentNo", paymentNo); parms.Add("IntermBillNo", intermBillNo);
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
                        case "PaymentNo": this.str.PaymentNo = (string)value; break;
                        case "IntermBillNo": this.str.IntermBillNo = (string)value; break;
                        case "IsPaymentProceed": this.str.IsPaymentProceed = (string)value; break;
                        case "IsPaymentReturned": this.str.IsPaymentReturned = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "IsPaymentProceed":

                            if (value == null || value is System.Boolean)
                                this.IsPaymentProceed = (System.Boolean?)value;
                            break;

                        case "IsPaymentReturned":

                            if (value == null || value is System.Boolean)
                                this.IsPaymentReturned = (System.Boolean?)value;
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
        /// Maps to TransPaymentItemIntermBill.PaymentNo
        /// </summary>
        virtual public System.String PaymentNo
        {
            get
            {
                return base.GetSystemString(TransPaymentItemIntermBillMetadata.ColumnNames.PaymentNo);
            }

            set
            {
                base.SetSystemString(TransPaymentItemIntermBillMetadata.ColumnNames.PaymentNo, value);
            }
        }

        /// <summary>
        /// Maps to TransPaymentItemIntermBill.IntermBillNo
        /// </summary>
        virtual public System.String IntermBillNo
        {
            get
            {
                return base.GetSystemString(TransPaymentItemIntermBillMetadata.ColumnNames.IntermBillNo);
            }

            set
            {
                base.SetSystemString(TransPaymentItemIntermBillMetadata.ColumnNames.IntermBillNo, value);
            }
        }

        /// <summary>
        /// Maps to TransPaymentItemIntermBill.IsPaymentProceed
        /// </summary>
        virtual public System.Boolean? IsPaymentProceed
        {
            get
            {
                return base.GetSystemBoolean(TransPaymentItemIntermBillMetadata.ColumnNames.IsPaymentProceed);
            }

            set
            {
                base.SetSystemBoolean(TransPaymentItemIntermBillMetadata.ColumnNames.IsPaymentProceed, value);
            }
        }

        /// <summary>
        /// Maps to TransPaymentItemIntermBill.IsPaymentReturned
        /// </summary>
        virtual public System.Boolean? IsPaymentReturned
        {
            get
            {
                return base.GetSystemBoolean(TransPaymentItemIntermBillMetadata.ColumnNames.IsPaymentReturned);
            }

            set
            {
                base.SetSystemBoolean(TransPaymentItemIntermBillMetadata.ColumnNames.IsPaymentReturned, value);
            }
        }

        /// <summary>
        /// Maps to TransPaymentItemIntermBill.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(TransPaymentItemIntermBillMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(TransPaymentItemIntermBillMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to TransPaymentItemIntermBill.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(TransPaymentItemIntermBillMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(TransPaymentItemIntermBillMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esTransPaymentItemIntermBill entity)
            {
                this.entity = entity;
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

            public System.String IntermBillNo
            {
                get
                {
                    System.String data = entity.IntermBillNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IntermBillNo = null;
                    else entity.IntermBillNo = Convert.ToString(value);
                }
            }

            public System.String IsPaymentProceed
            {
                get
                {
                    System.Boolean? data = entity.IsPaymentProceed;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsPaymentProceed = null;
                    else entity.IsPaymentProceed = Convert.ToBoolean(value);
                }
            }

            public System.String IsPaymentReturned
            {
                get
                {
                    System.Boolean? data = entity.IsPaymentReturned;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsPaymentReturned = null;
                    else entity.IsPaymentReturned = Convert.ToBoolean(value);
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


            private esTransPaymentItemIntermBill entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esTransPaymentItemIntermBillQuery query)
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
                throw new Exception("esTransPaymentItemIntermBill can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    public partial class TransPaymentItemIntermBill : esTransPaymentItemIntermBill
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
    abstract public class esTransPaymentItemIntermBillQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return TransPaymentItemIntermBillMetadata.Meta();
            }
        }


        public esQueryItem PaymentNo
        {
            get
            {
                return new esQueryItem(this, TransPaymentItemIntermBillMetadata.ColumnNames.PaymentNo, esSystemType.String);
            }
        }

        public esQueryItem IntermBillNo
        {
            get
            {
                return new esQueryItem(this, TransPaymentItemIntermBillMetadata.ColumnNames.IntermBillNo, esSystemType.String);
            }
        }

        public esQueryItem IsPaymentProceed
        {
            get
            {
                return new esQueryItem(this, TransPaymentItemIntermBillMetadata.ColumnNames.IsPaymentProceed, esSystemType.Boolean);
            }
        }

        public esQueryItem IsPaymentReturned
        {
            get
            {
                return new esQueryItem(this, TransPaymentItemIntermBillMetadata.ColumnNames.IsPaymentReturned, esSystemType.Boolean);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, TransPaymentItemIntermBillMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, TransPaymentItemIntermBillMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("TransPaymentItemIntermBillCollection")]
    public partial class TransPaymentItemIntermBillCollection : esTransPaymentItemIntermBillCollection, IEnumerable<TransPaymentItemIntermBill>
    {
        public TransPaymentItemIntermBillCollection()
        {

        }

        public static implicit operator List<TransPaymentItemIntermBill>(TransPaymentItemIntermBillCollection coll)
        {
            List<TransPaymentItemIntermBill> list = new List<TransPaymentItemIntermBill>();

            foreach (TransPaymentItemIntermBill emp in coll)
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
                return TransPaymentItemIntermBillMetadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new TransPaymentItemIntermBillQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new TransPaymentItemIntermBill(row);
        }

        override protected esEntity CreateEntity()
        {
            return new TransPaymentItemIntermBill();
        }


        #endregion


        [BrowsableAttribute(false)]
        public TransPaymentItemIntermBillQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new TransPaymentItemIntermBillQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(TransPaymentItemIntermBillQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public TransPaymentItemIntermBill AddNew()
        {
            TransPaymentItemIntermBill entity = base.AddNewEntity() as TransPaymentItemIntermBill;

            return entity;
        }

        public TransPaymentItemIntermBill FindByPrimaryKey(System.String paymentNo, System.String intermBillNo)
        {
            return base.FindByPrimaryKey(paymentNo, intermBillNo) as TransPaymentItemIntermBill;
        }


        #region IEnumerable<TransPaymentItemIntermBill> Members

        IEnumerator<TransPaymentItemIntermBill> IEnumerable<TransPaymentItemIntermBill>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as TransPaymentItemIntermBill;
            }
        }

        #endregion

        private TransPaymentItemIntermBillQuery query;
    }


    /// <summary>
    /// Encapsulates the 'TransPaymentItemIntermBill' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("TransPaymentItemIntermBill ({PaymentNo},{IntermBillNo})")]
    [Serializable]
    public partial class TransPaymentItemIntermBill : esTransPaymentItemIntermBill
    {
        public TransPaymentItemIntermBill()
        {

        }

        public TransPaymentItemIntermBill(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return TransPaymentItemIntermBillMetadata.Meta();
            }
        }



        override protected esTransPaymentItemIntermBillQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new TransPaymentItemIntermBillQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public TransPaymentItemIntermBillQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new TransPaymentItemIntermBillQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(TransPaymentItemIntermBillQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private TransPaymentItemIntermBillQuery query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class TransPaymentItemIntermBillQuery : esTransPaymentItemIntermBillQuery
    {
        public TransPaymentItemIntermBillQuery()
        {

        }

        public TransPaymentItemIntermBillQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "TransPaymentItemIntermBillQuery";
        }


    }


    [Serializable]
    public partial class TransPaymentItemIntermBillMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected TransPaymentItemIntermBillMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(TransPaymentItemIntermBillMetadata.ColumnNames.PaymentNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = TransPaymentItemIntermBillMetadata.PropertyNames.PaymentNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(TransPaymentItemIntermBillMetadata.ColumnNames.IntermBillNo, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = TransPaymentItemIntermBillMetadata.PropertyNames.IntermBillNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(TransPaymentItemIntermBillMetadata.ColumnNames.IsPaymentProceed, 2, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = TransPaymentItemIntermBillMetadata.PropertyNames.IsPaymentProceed;
            _columns.Add(c);

            c = new esColumnMetadata(TransPaymentItemIntermBillMetadata.ColumnNames.IsPaymentReturned, 3, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = TransPaymentItemIntermBillMetadata.PropertyNames.IsPaymentReturned;
            _columns.Add(c);

            c = new esColumnMetadata(TransPaymentItemIntermBillMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = TransPaymentItemIntermBillMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransPaymentItemIntermBillMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = TransPaymentItemIntermBillMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public TransPaymentItemIntermBillMetadata Meta()
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
            public const string PaymentNo = "PaymentNo";
            public const string IntermBillNo = "IntermBillNo";
            public const string IsPaymentProceed = "IsPaymentProceed";
            public const string IsPaymentReturned = "IsPaymentReturned";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string PaymentNo = "PaymentNo";
            public const string IntermBillNo = "IntermBillNo";
            public const string IsPaymentProceed = "IsPaymentProceed";
            public const string IsPaymentReturned = "IsPaymentReturned";
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
            lock (typeof(TransPaymentItemIntermBillMetadata))
            {
                if (TransPaymentItemIntermBillMetadata.mapDelegates == null)
                {
                    TransPaymentItemIntermBillMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (TransPaymentItemIntermBillMetadata.meta == null)
                {
                    TransPaymentItemIntermBillMetadata.meta = new TransPaymentItemIntermBillMetadata();
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


                meta.AddTypeMap("PaymentNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IntermBillNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsPaymentProceed", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsPaymentReturned", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));



                meta.Source = "TransPaymentItemIntermBill";
                meta.Destination = "TransPaymentItemIntermBill";

                meta.spInsert = "proc_TransPaymentItemIntermBillInsert";
                meta.spUpdate = "proc_TransPaymentItemIntermBillUpdate";
                meta.spDelete = "proc_TransPaymentItemIntermBillDelete";
                meta.spLoadAll = "proc_TransPaymentItemIntermBillLoadAll";
                meta.spLoadByPrimaryKey = "proc_TransPaymentItemIntermBillLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private TransPaymentItemIntermBillMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
