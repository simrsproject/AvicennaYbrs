/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 11/5/2012 4:50:29 PM
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
    abstract public class esCostCalculationTempCollection : esEntityCollectionWAuditLog
    {
        public esCostCalculationTempCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "CostCalculationTempCollection";
        }

        #region Query Logic
        protected void InitQuery(esCostCalculationTempQuery query)
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
            this.InitQuery(query as esCostCalculationTempQuery);
        }
        #endregion

        virtual public CostCalculationTemp DetachEntity(CostCalculationTemp entity)
        {
            return base.DetachEntity(entity) as CostCalculationTemp;
        }

        virtual public CostCalculationTemp AttachEntity(CostCalculationTemp entity)
        {
            return base.AttachEntity(entity) as CostCalculationTemp;
        }

        virtual public void Combine(CostCalculationTempCollection collection)
        {
            base.Combine(collection);
        }

        new public CostCalculationTemp this[int index]
        {
            get
            {
                return base[index] as CostCalculationTemp;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(CostCalculationTemp);
        }
    }



    [Serializable]
    abstract public class esCostCalculationTemp : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esCostCalculationTempQuery GetDynamicQuery()
        {
            return null;
        }

        public esCostCalculationTemp()
        {

        }

        public esCostCalculationTemp(DataRow row)
            : base(row)
        {

        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(System.String registrationNo, System.String transactionNo, System.String sequenceNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(registrationNo, transactionNo, sequenceNo);
            else
                return LoadByPrimaryKeyStoredProcedure(registrationNo, transactionNo, sequenceNo);
        }

        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String registrationNo, System.String transactionNo, System.String sequenceNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(registrationNo, transactionNo, sequenceNo);
            else
                return LoadByPrimaryKeyStoredProcedure(registrationNo, transactionNo, sequenceNo);
        }

        private bool LoadByPrimaryKeyDynamic(System.String registrationNo, System.String transactionNo, System.String sequenceNo)
        {
            esCostCalculationTempQuery query = this.GetDynamicQuery();
            query.Where(query.RegistrationNo == registrationNo, query.TransactionNo == transactionNo, query.SequenceNo == sequenceNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(System.String registrationNo, System.String transactionNo, System.String sequenceNo)
        {
            esParameters parms = new esParameters();
            parms.Add("RegistrationNo", registrationNo); parms.Add("TransactionNo", transactionNo); parms.Add("SequenceNo", sequenceNo);
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
                        case "ItemID": this.str.ItemID = (string)value; break;
                        case "PatientAmount": this.str.PatientAmount = (string)value; break;
                        case "GuarantorAmount": this.str.GuarantorAmount = (string)value; break;
                        case "DiscountAmount": this.str.DiscountAmount = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "PatientAmount":

                            if (value == null || value is System.Decimal)
                                this.PatientAmount = (System.Decimal?)value;
                            break;

                        case "GuarantorAmount":

                            if (value == null || value is System.Decimal)
                                this.GuarantorAmount = (System.Decimal?)value;
                            break;

                        case "DiscountAmount":

                            if (value == null || value is System.Decimal)
                                this.DiscountAmount = (System.Decimal?)value;
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
        /// Maps to CostCalculationTemp.RegistrationNo
        /// </summary>
        virtual public System.String RegistrationNo
        {
            get
            {
                return base.GetSystemString(CostCalculationTempMetadata.ColumnNames.RegistrationNo);
            }

            set
            {
                base.SetSystemString(CostCalculationTempMetadata.ColumnNames.RegistrationNo, value);
            }
        }

        /// <summary>
        /// Maps to CostCalculationTemp.TransactionNo
        /// </summary>
        virtual public System.String TransactionNo
        {
            get
            {
                return base.GetSystemString(CostCalculationTempMetadata.ColumnNames.TransactionNo);
            }

            set
            {
                base.SetSystemString(CostCalculationTempMetadata.ColumnNames.TransactionNo, value);
            }
        }

        /// <summary>
        /// Maps to CostCalculationTemp.SequenceNo
        /// </summary>
        virtual public System.String SequenceNo
        {
            get
            {
                return base.GetSystemString(CostCalculationTempMetadata.ColumnNames.SequenceNo);
            }

            set
            {
                base.SetSystemString(CostCalculationTempMetadata.ColumnNames.SequenceNo, value);
            }
        }

        /// <summary>
        /// Maps to CostCalculationTemp.ItemID
        /// </summary>
        virtual public System.String ItemID
        {
            get
            {
                return base.GetSystemString(CostCalculationTempMetadata.ColumnNames.ItemID);
            }

            set
            {
                base.SetSystemString(CostCalculationTempMetadata.ColumnNames.ItemID, value);
            }
        }

        /// <summary>
        /// Maps to CostCalculationTemp.PatientAmount
        /// </summary>
        virtual public System.Decimal? PatientAmount
        {
            get
            {
                return base.GetSystemDecimal(CostCalculationTempMetadata.ColumnNames.PatientAmount);
            }

            set
            {
                base.SetSystemDecimal(CostCalculationTempMetadata.ColumnNames.PatientAmount, value);
            }
        }

        /// <summary>
        /// Maps to CostCalculationTemp.GuarantorAmount
        /// </summary>
        virtual public System.Decimal? GuarantorAmount
        {
            get
            {
                return base.GetSystemDecimal(CostCalculationTempMetadata.ColumnNames.GuarantorAmount);
            }

            set
            {
                base.SetSystemDecimal(CostCalculationTempMetadata.ColumnNames.GuarantorAmount, value);
            }
        }

        /// <summary>
        /// Maps to CostCalculationTemp.DiscountAmount
        /// </summary>
        virtual public System.Decimal? DiscountAmount
        {
            get
            {
                return base.GetSystemDecimal(CostCalculationTempMetadata.ColumnNames.DiscountAmount);
            }

            set
            {
                base.SetSystemDecimal(CostCalculationTempMetadata.ColumnNames.DiscountAmount, value);
            }
        }

        /// <summary>
        /// Maps to CostCalculationTemp.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(CostCalculationTempMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(CostCalculationTempMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to CostCalculationTemp.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(CostCalculationTempMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(CostCalculationTempMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esCostCalculationTemp entity)
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

            public System.String PatientAmount
            {
                get
                {
                    System.Decimal? data = entity.PatientAmount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PatientAmount = null;
                    else entity.PatientAmount = Convert.ToDecimal(value);
                }
            }

            public System.String GuarantorAmount
            {
                get
                {
                    System.Decimal? data = entity.GuarantorAmount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.GuarantorAmount = null;
                    else entity.GuarantorAmount = Convert.ToDecimal(value);
                }
            }

            public System.String DiscountAmount
            {
                get
                {
                    System.Decimal? data = entity.DiscountAmount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DiscountAmount = null;
                    else entity.DiscountAmount = Convert.ToDecimal(value);
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


            private esCostCalculationTemp entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esCostCalculationTempQuery query)
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
                throw new Exception("esCostCalculationTemp can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    public partial class CostCalculationTemp : esCostCalculationTemp
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
    abstract public class esCostCalculationTempQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return CostCalculationTempMetadata.Meta();
            }
        }


        public esQueryItem RegistrationNo
        {
            get
            {
                return new esQueryItem(this, CostCalculationTempMetadata.ColumnNames.RegistrationNo, esSystemType.String);
            }
        }

        public esQueryItem TransactionNo
        {
            get
            {
                return new esQueryItem(this, CostCalculationTempMetadata.ColumnNames.TransactionNo, esSystemType.String);
            }
        }

        public esQueryItem SequenceNo
        {
            get
            {
                return new esQueryItem(this, CostCalculationTempMetadata.ColumnNames.SequenceNo, esSystemType.String);
            }
        }

        public esQueryItem ItemID
        {
            get
            {
                return new esQueryItem(this, CostCalculationTempMetadata.ColumnNames.ItemID, esSystemType.String);
            }
        }

        public esQueryItem PatientAmount
        {
            get
            {
                return new esQueryItem(this, CostCalculationTempMetadata.ColumnNames.PatientAmount, esSystemType.Decimal);
            }
        }

        public esQueryItem GuarantorAmount
        {
            get
            {
                return new esQueryItem(this, CostCalculationTempMetadata.ColumnNames.GuarantorAmount, esSystemType.Decimal);
            }
        }

        public esQueryItem DiscountAmount
        {
            get
            {
                return new esQueryItem(this, CostCalculationTempMetadata.ColumnNames.DiscountAmount, esSystemType.Decimal);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, CostCalculationTempMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, CostCalculationTempMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("CostCalculationTempCollection")]
    public partial class CostCalculationTempCollection : esCostCalculationTempCollection, IEnumerable<CostCalculationTemp>
    {
        public CostCalculationTempCollection()
        {

        }

        public static implicit operator List<CostCalculationTemp>(CostCalculationTempCollection coll)
        {
            List<CostCalculationTemp> list = new List<CostCalculationTemp>();

            foreach (CostCalculationTemp emp in coll)
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
                return CostCalculationTempMetadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new CostCalculationTempQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new CostCalculationTemp(row);
        }

        override protected esEntity CreateEntity()
        {
            return new CostCalculationTemp();
        }


        #endregion


        [BrowsableAttribute(false)]
        public CostCalculationTempQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new CostCalculationTempQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(CostCalculationTempQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public CostCalculationTemp AddNew()
        {
            CostCalculationTemp entity = base.AddNewEntity() as CostCalculationTemp;

            return entity;
        }

        public CostCalculationTemp FindByPrimaryKey(System.String registrationNo, System.String transactionNo, System.String sequenceNo)
        {
            return base.FindByPrimaryKey(registrationNo, transactionNo, sequenceNo) as CostCalculationTemp;
        }


        #region IEnumerable<CostCalculationTemp> Members

        IEnumerator<CostCalculationTemp> IEnumerable<CostCalculationTemp>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as CostCalculationTemp;
            }
        }

        #endregion

        private CostCalculationTempQuery query;
    }


    /// <summary>
    /// Encapsulates the 'CostCalculationTemp' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("CostCalculationTemp ({RegistrationNo},{TransactionNo},{SequenceNo})")]
    [Serializable]
    public partial class CostCalculationTemp : esCostCalculationTemp
    {
        public CostCalculationTemp()
        {

        }

        public CostCalculationTemp(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return CostCalculationTempMetadata.Meta();
            }
        }



        override protected esCostCalculationTempQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new CostCalculationTempQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public CostCalculationTempQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new CostCalculationTempQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(CostCalculationTempQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private CostCalculationTempQuery query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class CostCalculationTempQuery : esCostCalculationTempQuery
    {
        public CostCalculationTempQuery()
        {

        }

        public CostCalculationTempQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "CostCalculationTempQuery";
        }


    }


    [Serializable]
    public partial class CostCalculationTempMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected CostCalculationTempMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(CostCalculationTempMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = CostCalculationTempMetadata.PropertyNames.RegistrationNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(CostCalculationTempMetadata.ColumnNames.TransactionNo, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = CostCalculationTempMetadata.PropertyNames.TransactionNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(CostCalculationTempMetadata.ColumnNames.SequenceNo, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = CostCalculationTempMetadata.PropertyNames.SequenceNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 6;
            _columns.Add(c);

            c = new esColumnMetadata(CostCalculationTempMetadata.ColumnNames.ItemID, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = CostCalculationTempMetadata.PropertyNames.ItemID;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(CostCalculationTempMetadata.ColumnNames.PatientAmount, 4, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = CostCalculationTempMetadata.PropertyNames.PatientAmount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(CostCalculationTempMetadata.ColumnNames.GuarantorAmount, 5, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = CostCalculationTempMetadata.PropertyNames.GuarantorAmount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(CostCalculationTempMetadata.ColumnNames.DiscountAmount, 6, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = CostCalculationTempMetadata.PropertyNames.DiscountAmount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(CostCalculationTempMetadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = CostCalculationTempMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(CostCalculationTempMetadata.ColumnNames.LastUpdateByUserID, 8, typeof(System.String), esSystemType.String);
            c.PropertyName = CostCalculationTempMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public CostCalculationTempMetadata Meta()
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
            public const string ItemID = "ItemID";
            public const string PatientAmount = "PatientAmount";
            public const string GuarantorAmount = "GuarantorAmount";
            public const string DiscountAmount = "DiscountAmount";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string RegistrationNo = "RegistrationNo";
            public const string TransactionNo = "TransactionNo";
            public const string SequenceNo = "SequenceNo";
            public const string ItemID = "ItemID";
            public const string PatientAmount = "PatientAmount";
            public const string GuarantorAmount = "GuarantorAmount";
            public const string DiscountAmount = "DiscountAmount";
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
            lock (typeof(CostCalculationTempMetadata))
            {
                if (CostCalculationTempMetadata.mapDelegates == null)
                {
                    CostCalculationTempMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (CostCalculationTempMetadata.meta == null)
                {
                    CostCalculationTempMetadata.meta = new CostCalculationTempMetadata();
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
                meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("PatientAmount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("GuarantorAmount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("DiscountAmount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));



                meta.Source = "CostCalculationTemp";
                meta.Destination = "CostCalculationTemp";

                meta.spInsert = "proc_CostCalculationTempInsert";
                meta.spUpdate = "proc_CostCalculationTempUpdate";
                meta.spDelete = "proc_CostCalculationTempDelete";
                meta.spLoadAll = "proc_CostCalculationTempLoadAll";
                meta.spLoadByPrimaryKey = "proc_CostCalculationTempLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private CostCalculationTempMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
