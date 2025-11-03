/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 8/10/2012 9:55:59 AM
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
    abstract public class esTransPaymentItemIntermBillGuarantorCollection : esEntityCollectionWAuditLog
    {
        public esTransPaymentItemIntermBillGuarantorCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "TransPaymentItemIntermBillGuarantorCollection";
        }

        #region Query Logic
        protected void InitQuery(esTransPaymentItemIntermBillGuarantorQuery query)
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
            this.InitQuery(query as esTransPaymentItemIntermBillGuarantorQuery);
        }
        #endregion

        virtual public TransPaymentItemIntermBillGuarantor DetachEntity(TransPaymentItemIntermBillGuarantor entity)
        {
            return base.DetachEntity(entity) as TransPaymentItemIntermBillGuarantor;
        }

        virtual public TransPaymentItemIntermBillGuarantor AttachEntity(TransPaymentItemIntermBillGuarantor entity)
        {
            return base.AttachEntity(entity) as TransPaymentItemIntermBillGuarantor;
        }

        virtual public void Combine(TransPaymentItemIntermBillGuarantorCollection collection)
        {
            base.Combine(collection);
        }

        new public TransPaymentItemIntermBillGuarantor this[int index]
        {
            get
            {
                return base[index] as TransPaymentItemIntermBillGuarantor;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(TransPaymentItemIntermBillGuarantor);
        }
    }



    [Serializable]
    abstract public class esTransPaymentItemIntermBillGuarantor : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esTransPaymentItemIntermBillGuarantorQuery GetDynamicQuery()
        {
            return null;
        }

        public esTransPaymentItemIntermBillGuarantor()
        {

        }

        public esTransPaymentItemIntermBillGuarantor(DataRow row)
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
            esTransPaymentItemIntermBillGuarantorQuery query = this.GetDynamicQuery();
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
        /// Maps to TransPaymentItemIntermBillGuarantor.PaymentNo
        /// </summary>
        virtual public System.String PaymentNo
        {
            get
            {
                return base.GetSystemString(TransPaymentItemIntermBillGuarantorMetadata.ColumnNames.PaymentNo);
            }

            set
            {
                base.SetSystemString(TransPaymentItemIntermBillGuarantorMetadata.ColumnNames.PaymentNo, value);
            }
        }

        /// <summary>
        /// Maps to TransPaymentItemIntermBillGuarantor.IntermBillNo
        /// </summary>
        virtual public System.String IntermBillNo
        {
            get
            {
                return base.GetSystemString(TransPaymentItemIntermBillGuarantorMetadata.ColumnNames.IntermBillNo);
            }

            set
            {
                base.SetSystemString(TransPaymentItemIntermBillGuarantorMetadata.ColumnNames.IntermBillNo, value);
            }
        }

        /// <summary>
        /// Maps to TransPaymentItemIntermBillGuarantor.IsPaymentProceed
        /// </summary>
        virtual public System.Boolean? IsPaymentProceed
        {
            get
            {
                return base.GetSystemBoolean(TransPaymentItemIntermBillGuarantorMetadata.ColumnNames.IsPaymentProceed);
            }

            set
            {
                base.SetSystemBoolean(TransPaymentItemIntermBillGuarantorMetadata.ColumnNames.IsPaymentProceed, value);
            }
        }

        /// <summary>
        /// Maps to TransPaymentItemIntermBillGuarantor.IsPaymentReturned
        /// </summary>
        virtual public System.Boolean? IsPaymentReturned
        {
            get
            {
                return base.GetSystemBoolean(TransPaymentItemIntermBillGuarantorMetadata.ColumnNames.IsPaymentReturned);
            }

            set
            {
                base.SetSystemBoolean(TransPaymentItemIntermBillGuarantorMetadata.ColumnNames.IsPaymentReturned, value);
            }
        }

        /// <summary>
        /// Maps to TransPaymentItemIntermBillGuarantor.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(TransPaymentItemIntermBillGuarantorMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(TransPaymentItemIntermBillGuarantorMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to TransPaymentItemIntermBillGuarantor.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(TransPaymentItemIntermBillGuarantorMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(TransPaymentItemIntermBillGuarantorMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esTransPaymentItemIntermBillGuarantor entity)
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


            private esTransPaymentItemIntermBillGuarantor entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esTransPaymentItemIntermBillGuarantorQuery query)
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
                throw new Exception("esTransPaymentItemIntermBillGuarantor can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    public partial class TransPaymentItemIntermBillGuarantor : esTransPaymentItemIntermBillGuarantor
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
    abstract public class esTransPaymentItemIntermBillGuarantorQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return TransPaymentItemIntermBillGuarantorMetadata.Meta();
            }
        }


        public esQueryItem PaymentNo
        {
            get
            {
                return new esQueryItem(this, TransPaymentItemIntermBillGuarantorMetadata.ColumnNames.PaymentNo, esSystemType.String);
            }
        }

        public esQueryItem IntermBillNo
        {
            get
            {
                return new esQueryItem(this, TransPaymentItemIntermBillGuarantorMetadata.ColumnNames.IntermBillNo, esSystemType.String);
            }
        }

        public esQueryItem IsPaymentProceed
        {
            get
            {
                return new esQueryItem(this, TransPaymentItemIntermBillGuarantorMetadata.ColumnNames.IsPaymentProceed, esSystemType.Boolean);
            }
        }

        public esQueryItem IsPaymentReturned
        {
            get
            {
                return new esQueryItem(this, TransPaymentItemIntermBillGuarantorMetadata.ColumnNames.IsPaymentReturned, esSystemType.Boolean);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, TransPaymentItemIntermBillGuarantorMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, TransPaymentItemIntermBillGuarantorMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("TransPaymentItemIntermBillGuarantorCollection")]
    public partial class TransPaymentItemIntermBillGuarantorCollection : esTransPaymentItemIntermBillGuarantorCollection, IEnumerable<TransPaymentItemIntermBillGuarantor>
    {
        public TransPaymentItemIntermBillGuarantorCollection()
        {

        }

        public static implicit operator List<TransPaymentItemIntermBillGuarantor>(TransPaymentItemIntermBillGuarantorCollection coll)
        {
            List<TransPaymentItemIntermBillGuarantor> list = new List<TransPaymentItemIntermBillGuarantor>();

            foreach (TransPaymentItemIntermBillGuarantor emp in coll)
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
                return TransPaymentItemIntermBillGuarantorMetadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new TransPaymentItemIntermBillGuarantorQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new TransPaymentItemIntermBillGuarantor(row);
        }

        override protected esEntity CreateEntity()
        {
            return new TransPaymentItemIntermBillGuarantor();
        }


        #endregion


        [BrowsableAttribute(false)]
        public TransPaymentItemIntermBillGuarantorQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new TransPaymentItemIntermBillGuarantorQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(TransPaymentItemIntermBillGuarantorQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public TransPaymentItemIntermBillGuarantor AddNew()
        {
            TransPaymentItemIntermBillGuarantor entity = base.AddNewEntity() as TransPaymentItemIntermBillGuarantor;

            return entity;
        }

        public TransPaymentItemIntermBillGuarantor FindByPrimaryKey(System.String paymentNo, System.String intermBillNo)
        {
            return base.FindByPrimaryKey(paymentNo, intermBillNo) as TransPaymentItemIntermBillGuarantor;
        }


        #region IEnumerable<TransPaymentItemIntermBillGuarantor> Members

        IEnumerator<TransPaymentItemIntermBillGuarantor> IEnumerable<TransPaymentItemIntermBillGuarantor>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as TransPaymentItemIntermBillGuarantor;
            }
        }

        #endregion

        private TransPaymentItemIntermBillGuarantorQuery query;
    }


    /// <summary>
    /// Encapsulates the 'TransPaymentItemIntermBillGuarantor' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("TransPaymentItemIntermBillGuarantor ({PaymentNo},{IntermBillNo})")]
    [Serializable]
    public partial class TransPaymentItemIntermBillGuarantor : esTransPaymentItemIntermBillGuarantor
    {
        public TransPaymentItemIntermBillGuarantor()
        {

        }

        public TransPaymentItemIntermBillGuarantor(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return TransPaymentItemIntermBillGuarantorMetadata.Meta();
            }
        }



        override protected esTransPaymentItemIntermBillGuarantorQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new TransPaymentItemIntermBillGuarantorQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public TransPaymentItemIntermBillGuarantorQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new TransPaymentItemIntermBillGuarantorQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(TransPaymentItemIntermBillGuarantorQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private TransPaymentItemIntermBillGuarantorQuery query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class TransPaymentItemIntermBillGuarantorQuery : esTransPaymentItemIntermBillGuarantorQuery
    {
        public TransPaymentItemIntermBillGuarantorQuery()
        {

        }

        public TransPaymentItemIntermBillGuarantorQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "TransPaymentItemIntermBillGuarantorQuery";
        }


    }


    [Serializable]
    public partial class TransPaymentItemIntermBillGuarantorMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected TransPaymentItemIntermBillGuarantorMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(TransPaymentItemIntermBillGuarantorMetadata.ColumnNames.PaymentNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = TransPaymentItemIntermBillGuarantorMetadata.PropertyNames.PaymentNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(TransPaymentItemIntermBillGuarantorMetadata.ColumnNames.IntermBillNo, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = TransPaymentItemIntermBillGuarantorMetadata.PropertyNames.IntermBillNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(TransPaymentItemIntermBillGuarantorMetadata.ColumnNames.IsPaymentProceed, 2, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = TransPaymentItemIntermBillGuarantorMetadata.PropertyNames.IsPaymentProceed;
            _columns.Add(c);

            c = new esColumnMetadata(TransPaymentItemIntermBillGuarantorMetadata.ColumnNames.IsPaymentReturned, 3, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = TransPaymentItemIntermBillGuarantorMetadata.PropertyNames.IsPaymentReturned;
            _columns.Add(c);

            c = new esColumnMetadata(TransPaymentItemIntermBillGuarantorMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = TransPaymentItemIntermBillGuarantorMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransPaymentItemIntermBillGuarantorMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = TransPaymentItemIntermBillGuarantorMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public TransPaymentItemIntermBillGuarantorMetadata Meta()
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
            lock (typeof(TransPaymentItemIntermBillGuarantorMetadata))
            {
                if (TransPaymentItemIntermBillGuarantorMetadata.mapDelegates == null)
                {
                    TransPaymentItemIntermBillGuarantorMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (TransPaymentItemIntermBillGuarantorMetadata.meta == null)
                {
                    TransPaymentItemIntermBillGuarantorMetadata.meta = new TransPaymentItemIntermBillGuarantorMetadata();
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



                meta.Source = "TransPaymentItemIntermBillGuarantor";
                meta.Destination = "TransPaymentItemIntermBillGuarantor";

                meta.spInsert = "proc_TransPaymentItemIntermBillGuarantorInsert";
                meta.spUpdate = "proc_TransPaymentItemIntermBillGuarantorUpdate";
                meta.spDelete = "proc_TransPaymentItemIntermBillGuarantorDelete";
                meta.spLoadAll = "proc_TransPaymentItemIntermBillGuarantorLoadAll";
                meta.spLoadByPrimaryKey = "proc_TransPaymentItemIntermBillGuarantorLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private TransPaymentItemIntermBillGuarantorMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
