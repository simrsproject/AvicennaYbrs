/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 2/14/2013 8:03:30 PM
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
    abstract public class esCostCalculationIntermBillTempCollection : esEntityCollectionWAuditLog
    {
        public esCostCalculationIntermBillTempCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "CostCalculationIntermBillTempCollection";
        }

        #region Query Logic
        protected void InitQuery(esCostCalculationIntermBillTempQuery query)
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
            this.InitQuery(query as esCostCalculationIntermBillTempQuery);
        }
        #endregion

        virtual public CostCalculationIntermBillTemp DetachEntity(CostCalculationIntermBillTemp entity)
        {
            return base.DetachEntity(entity) as CostCalculationIntermBillTemp;
        }

        virtual public CostCalculationIntermBillTemp AttachEntity(CostCalculationIntermBillTemp entity)
        {
            return base.AttachEntity(entity) as CostCalculationIntermBillTemp;
        }

        virtual public void Combine(CostCalculationIntermBillTempCollection collection)
        {
            base.Combine(collection);
        }

        new public CostCalculationIntermBillTemp this[int index]
        {
            get
            {
                return base[index] as CostCalculationIntermBillTemp;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(CostCalculationIntermBillTemp);
        }
    }



    [Serializable]
    abstract public class esCostCalculationIntermBillTemp : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esCostCalculationIntermBillTempQuery GetDynamicQuery()
        {
            return null;
        }

        public esCostCalculationIntermBillTemp()
        {

        }

        public esCostCalculationIntermBillTemp(DataRow row)
            : base(row)
        {

        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(System.String registrationNo, System.String transactionNo, System.String sequenceNo, System.String intermBillNo, System.String paymentNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(registrationNo, transactionNo, sequenceNo, intermBillNo, paymentNo);
            else
                return LoadByPrimaryKeyStoredProcedure(registrationNo, transactionNo, sequenceNo, intermBillNo, paymentNo);
        }

        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String registrationNo, System.String transactionNo, System.String sequenceNo, System.String intermBillNo, System.String paymentNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(registrationNo, transactionNo, sequenceNo, intermBillNo, paymentNo);
            else
                return LoadByPrimaryKeyStoredProcedure(registrationNo, transactionNo, sequenceNo, intermBillNo, paymentNo);
        }

        private bool LoadByPrimaryKeyDynamic(System.String registrationNo, System.String transactionNo, System.String sequenceNo, System.String intermBillNo, System.String paymentNo)
        {
            esCostCalculationIntermBillTempQuery query = this.GetDynamicQuery();
            query.Where(query.RegistrationNo == registrationNo, query.TransactionNo == transactionNo, query.SequenceNo == sequenceNo, query.IntermBillNo == intermBillNo, query.PaymentNo == paymentNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(System.String registrationNo, System.String transactionNo, System.String sequenceNo, System.String intermBillNo, System.String paymentNo)
        {
            esParameters parms = new esParameters();
            parms.Add("RegistrationNo", registrationNo); parms.Add("TransactionNo", transactionNo); parms.Add("SequenceNo", sequenceNo); parms.Add("IntermBillNo", intermBillNo); parms.Add("PaymentNo", paymentNo);
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
                        case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
                        case "TransactionNo": this.str.TransactionNo = (string)value; break;
                        case "SequenceNo": this.str.SequenceNo = (string)value; break;
                        case "IntermBillNo": this.str.IntermBillNo = (string)value; break;
                        case "PaymentNo": this.str.PaymentNo = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {

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
        /// Maps to CostCalculationIntermBillTemp.RegistrationNo
        /// </summary>
        virtual public System.String RegistrationNo
        {
            get
            {
                return base.GetSystemString(CostCalculationIntermBillTempMetadata.ColumnNames.RegistrationNo);
            }

            set
            {
                base.SetSystemString(CostCalculationIntermBillTempMetadata.ColumnNames.RegistrationNo, value);
            }
        }

        /// <summary>
        /// Maps to CostCalculationIntermBillTemp.TransactionNo
        /// </summary>
        virtual public System.String TransactionNo
        {
            get
            {
                return base.GetSystemString(CostCalculationIntermBillTempMetadata.ColumnNames.TransactionNo);
            }

            set
            {
                base.SetSystemString(CostCalculationIntermBillTempMetadata.ColumnNames.TransactionNo, value);
            }
        }

        /// <summary>
        /// Maps to CostCalculationIntermBillTemp.SequenceNo
        /// </summary>
        virtual public System.String SequenceNo
        {
            get
            {
                return base.GetSystemString(CostCalculationIntermBillTempMetadata.ColumnNames.SequenceNo);
            }

            set
            {
                base.SetSystemString(CostCalculationIntermBillTempMetadata.ColumnNames.SequenceNo, value);
            }
        }

        /// <summary>
        /// Maps to CostCalculationIntermBillTemp.IntermBillNo
        /// </summary>
        virtual public System.String IntermBillNo
        {
            get
            {
                return base.GetSystemString(CostCalculationIntermBillTempMetadata.ColumnNames.IntermBillNo);
            }

            set
            {
                base.SetSystemString(CostCalculationIntermBillTempMetadata.ColumnNames.IntermBillNo, value);
            }
        }

        /// <summary>
        /// Maps to CostCalculationIntermBillTemp.PaymentNo
        /// </summary>
        virtual public System.String PaymentNo
        {
            get
            {
                return base.GetSystemString(CostCalculationIntermBillTempMetadata.ColumnNames.PaymentNo);
            }

            set
            {
                base.SetSystemString(CostCalculationIntermBillTempMetadata.ColumnNames.PaymentNo, value);
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
            public esStrings(esCostCalculationIntermBillTemp entity)
            {
                this.entity = entity;
            }


            public System.String RegistrationNo
            {
                get
                {
                    System.String data = entity.RegistrationNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RegistrationNo = null;
                    else entity.RegistrationNo = Convert.ToString(value);
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


            private esCostCalculationIntermBillTemp entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esCostCalculationIntermBillTempQuery query)
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
                throw new Exception("esCostCalculationIntermBillTemp can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    public partial class CostCalculationIntermBillTemp : esCostCalculationIntermBillTemp
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
    abstract public class esCostCalculationIntermBillTempQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return CostCalculationIntermBillTempMetadata.Meta();
            }
        }


        public esQueryItem RegistrationNo
        {
            get
            {
                return new esQueryItem(this, CostCalculationIntermBillTempMetadata.ColumnNames.RegistrationNo, esSystemType.String);
            }
        }

        public esQueryItem TransactionNo
        {
            get
            {
                return new esQueryItem(this, CostCalculationIntermBillTempMetadata.ColumnNames.TransactionNo, esSystemType.String);
            }
        }

        public esQueryItem SequenceNo
        {
            get
            {
                return new esQueryItem(this, CostCalculationIntermBillTempMetadata.ColumnNames.SequenceNo, esSystemType.String);
            }
        }

        public esQueryItem IntermBillNo
        {
            get
            {
                return new esQueryItem(this, CostCalculationIntermBillTempMetadata.ColumnNames.IntermBillNo, esSystemType.String);
            }
        }

        public esQueryItem PaymentNo
        {
            get
            {
                return new esQueryItem(this, CostCalculationIntermBillTempMetadata.ColumnNames.PaymentNo, esSystemType.String);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("CostCalculationIntermBillTempCollection")]
    public partial class CostCalculationIntermBillTempCollection : esCostCalculationIntermBillTempCollection, IEnumerable<CostCalculationIntermBillTemp>
    {
        public CostCalculationIntermBillTempCollection()
        {

        }

        public static implicit operator List<CostCalculationIntermBillTemp>(CostCalculationIntermBillTempCollection coll)
        {
            List<CostCalculationIntermBillTemp> list = new List<CostCalculationIntermBillTemp>();

            foreach (CostCalculationIntermBillTemp emp in coll)
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
                return CostCalculationIntermBillTempMetadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new CostCalculationIntermBillTempQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new CostCalculationIntermBillTemp(row);
        }

        override protected esEntity CreateEntity()
        {
            return new CostCalculationIntermBillTemp();
        }


        #endregion


        [BrowsableAttribute(false)]
        public CostCalculationIntermBillTempQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new CostCalculationIntermBillTempQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(CostCalculationIntermBillTempQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public CostCalculationIntermBillTemp AddNew()
        {
            CostCalculationIntermBillTemp entity = base.AddNewEntity() as CostCalculationIntermBillTemp;

            return entity;
        }

        public CostCalculationIntermBillTemp FindByPrimaryKey(System.String registrationNo, System.String transactionNo, System.String sequenceNo, System.String intermBillNo, System.String paymentNo)
        {
            return base.FindByPrimaryKey(registrationNo, transactionNo, sequenceNo, intermBillNo, paymentNo) as CostCalculationIntermBillTemp;
        }


        #region IEnumerable<CostCalculationIntermBillTemp> Members

        IEnumerator<CostCalculationIntermBillTemp> IEnumerable<CostCalculationIntermBillTemp>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as CostCalculationIntermBillTemp;
            }
        }

        #endregion

        private CostCalculationIntermBillTempQuery query;
    }


    /// <summary>
    /// Encapsulates the 'CostCalculationIntermBillTemp' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("CostCalculationIntermBillTemp ({RegistrationNo},{TransactionNo},{SequenceNo},{IntermBillNo},{PaymentNo})")]
    [Serializable]
    public partial class CostCalculationIntermBillTemp : esCostCalculationIntermBillTemp
    {
        public CostCalculationIntermBillTemp()
        {

        }

        public CostCalculationIntermBillTemp(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return CostCalculationIntermBillTempMetadata.Meta();
            }
        }



        override protected esCostCalculationIntermBillTempQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new CostCalculationIntermBillTempQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public CostCalculationIntermBillTempQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new CostCalculationIntermBillTempQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(CostCalculationIntermBillTempQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private CostCalculationIntermBillTempQuery query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class CostCalculationIntermBillTempQuery : esCostCalculationIntermBillTempQuery
    {
        public CostCalculationIntermBillTempQuery()
        {

        }

        public CostCalculationIntermBillTempQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "CostCalculationIntermBillTempQuery";
        }


    }


    [Serializable]
    public partial class CostCalculationIntermBillTempMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected CostCalculationIntermBillTempMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(CostCalculationIntermBillTempMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = CostCalculationIntermBillTempMetadata.PropertyNames.RegistrationNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(CostCalculationIntermBillTempMetadata.ColumnNames.TransactionNo, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = CostCalculationIntermBillTempMetadata.PropertyNames.TransactionNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(CostCalculationIntermBillTempMetadata.ColumnNames.SequenceNo, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = CostCalculationIntermBillTempMetadata.PropertyNames.SequenceNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 6;
            _columns.Add(c);

            c = new esColumnMetadata(CostCalculationIntermBillTempMetadata.ColumnNames.IntermBillNo, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = CostCalculationIntermBillTempMetadata.PropertyNames.IntermBillNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(CostCalculationIntermBillTempMetadata.ColumnNames.PaymentNo, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = CostCalculationIntermBillTempMetadata.PropertyNames.PaymentNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

        }
        #endregion

        static public CostCalculationIntermBillTempMetadata Meta()
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
            public const string RegistrationNo = "RegistrationNo";
            public const string TransactionNo = "TransactionNo";
            public const string SequenceNo = "SequenceNo";
            public const string IntermBillNo = "IntermBillNo";
            public const string PaymentNo = "PaymentNo";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string RegistrationNo = "RegistrationNo";
            public const string TransactionNo = "TransactionNo";
            public const string SequenceNo = "SequenceNo";
            public const string IntermBillNo = "IntermBillNo";
            public const string PaymentNo = "PaymentNo";
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
            lock (typeof(CostCalculationIntermBillTempMetadata))
            {
                if (CostCalculationIntermBillTempMetadata.mapDelegates == null)
                {
                    CostCalculationIntermBillTempMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (CostCalculationIntermBillTempMetadata.meta == null)
                {
                    CostCalculationIntermBillTempMetadata.meta = new CostCalculationIntermBillTempMetadata();
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


                meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SequenceNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IntermBillNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("PaymentNo", new esTypeMap("varchar", "System.String"));



                meta.Source = "CostCalculationIntermBillTemp";
                meta.Destination = "CostCalculationIntermBillTemp";

                meta.spInsert = "proc_CostCalculationIntermBillTempInsert";
                meta.spUpdate = "proc_CostCalculationIntermBillTempUpdate";
                meta.spDelete = "proc_CostCalculationIntermBillTempDelete";
                meta.spLoadAll = "proc_CostCalculationIntermBillTempLoadAll";
                meta.spLoadByPrimaryKey = "proc_CostCalculationIntermBillTempLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private CostCalculationIntermBillTempMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
