/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 1/30/2013 9:14:49 AM
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
    abstract public class esTransChargesItemTempCoverageCollection : esEntityCollectionWAuditLog
    {
        public esTransChargesItemTempCoverageCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "TransChargesItemTempCoverageCollection";
        }

        #region Query Logic
        protected void InitQuery(esTransChargesItemTempCoverageQuery query)
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
            this.InitQuery(query as esTransChargesItemTempCoverageQuery);
        }
        #endregion

        virtual public TransChargesItemTempCoverage DetachEntity(TransChargesItemTempCoverage entity)
        {
            return base.DetachEntity(entity) as TransChargesItemTempCoverage;
        }

        virtual public TransChargesItemTempCoverage AttachEntity(TransChargesItemTempCoverage entity)
        {
            return base.AttachEntity(entity) as TransChargesItemTempCoverage;
        }

        virtual public void Combine(TransChargesItemTempCoverageCollection collection)
        {
            base.Combine(collection);
        }

        new public TransChargesItemTempCoverage this[int index]
        {
            get
            {
                return base[index] as TransChargesItemTempCoverage;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(TransChargesItemTempCoverage);
        }
    }



    [Serializable]
    abstract public class esTransChargesItemTempCoverage : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esTransChargesItemTempCoverageQuery GetDynamicQuery()
        {
            return null;
        }

        public esTransChargesItemTempCoverage()
        {

        }

        public esTransChargesItemTempCoverage(DataRow row)
            : base(row)
        {

        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(System.String registrationNo, System.String transactionNo, System.String sequenceNo, System.String chargeClassID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(registrationNo, transactionNo, sequenceNo, chargeClassID);
            else
                return LoadByPrimaryKeyStoredProcedure(registrationNo, transactionNo, sequenceNo, chargeClassID);
        }

        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String registrationNo, System.String transactionNo, System.String sequenceNo, System.String chargeClassID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(registrationNo, transactionNo, sequenceNo, chargeClassID);
            else
                return LoadByPrimaryKeyStoredProcedure(registrationNo, transactionNo, sequenceNo, chargeClassID);
        }

        private bool LoadByPrimaryKeyDynamic(System.String registrationNo, System.String transactionNo, System.String sequenceNo, System.String chargeClassID)
        {
            esTransChargesItemTempCoverageQuery query = this.GetDynamicQuery();
            query.Where(query.RegistrationNo == registrationNo, query.TransactionNo == transactionNo, query.SequenceNo == sequenceNo, query.ChargeClassID == chargeClassID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(System.String registrationNo, System.String transactionNo, System.String sequenceNo, System.String chargeClassID)
        {
            esParameters parms = new esParameters();
            parms.Add("RegistrationNo", registrationNo); parms.Add("TransactionNo", transactionNo); parms.Add("SequenceNo", sequenceNo); parms.Add("ChargeClassID", chargeClassID);
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
                        case "ReferenceNo": this.str.ReferenceNo = (string)value; break;
                        case "ReferenceSequenceNo": this.str.ReferenceSequenceNo = (string)value; break;
                        case "ItemID": this.str.ItemID = (string)value; break;
                        case "ChargeClassID": this.str.ChargeClassID = (string)value; break;
                        case "ChargeQuantity": this.str.ChargeQuantity = (string)value; break;
                        case "Price": this.str.Price = (string)value; break;
                        case "ParamedicCollectionName": this.str.ParamedicCollectionName = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "ChargeQuantity":

                            if (value == null || value is System.Decimal)
                                this.ChargeQuantity = (System.Decimal?)value;
                            break;

                        case "Price":

                            if (value == null || value is System.Decimal)
                                this.Price = (System.Decimal?)value;
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
        /// Maps to TransChargesItemTempCoverage.RegistrationNo
        /// </summary>
        virtual public System.String RegistrationNo
        {
            get
            {
                return base.GetSystemString(TransChargesItemTempCoverageMetadata.ColumnNames.RegistrationNo);
            }

            set
            {
                base.SetSystemString(TransChargesItemTempCoverageMetadata.ColumnNames.RegistrationNo, value);
            }
        }

        /// <summary>
        /// Maps to TransChargesItemTempCoverage.TransactionNo
        /// </summary>
        virtual public System.String TransactionNo
        {
            get
            {
                return base.GetSystemString(TransChargesItemTempCoverageMetadata.ColumnNames.TransactionNo);
            }

            set
            {
                base.SetSystemString(TransChargesItemTempCoverageMetadata.ColumnNames.TransactionNo, value);
            }
        }

        /// <summary>
        /// Maps to TransChargesItemTempCoverage.SequenceNo
        /// </summary>
        virtual public System.String SequenceNo
        {
            get
            {
                return base.GetSystemString(TransChargesItemTempCoverageMetadata.ColumnNames.SequenceNo);
            }

            set
            {
                base.SetSystemString(TransChargesItemTempCoverageMetadata.ColumnNames.SequenceNo, value);
            }
        }

        /// <summary>
        /// Maps to TransChargesItemTempCoverage.ReferenceNo
        /// </summary>
        virtual public System.String ReferenceNo
        {
            get
            {
                return base.GetSystemString(TransChargesItemTempCoverageMetadata.ColumnNames.ReferenceNo);
            }

            set
            {
                base.SetSystemString(TransChargesItemTempCoverageMetadata.ColumnNames.ReferenceNo, value);
            }
        }

        /// <summary>
        /// Maps to TransChargesItemTempCoverage.ReferenceSequenceNo
        /// </summary>
        virtual public System.String ReferenceSequenceNo
        {
            get
            {
                return base.GetSystemString(TransChargesItemTempCoverageMetadata.ColumnNames.ReferenceSequenceNo);
            }

            set
            {
                base.SetSystemString(TransChargesItemTempCoverageMetadata.ColumnNames.ReferenceSequenceNo, value);
            }
        }

        /// <summary>
        /// Maps to TransChargesItemTempCoverage.ItemID
        /// </summary>
        virtual public System.String ItemID
        {
            get
            {
                return base.GetSystemString(TransChargesItemTempCoverageMetadata.ColumnNames.ItemID);
            }

            set
            {
                base.SetSystemString(TransChargesItemTempCoverageMetadata.ColumnNames.ItemID, value);
            }
        }

        /// <summary>
        /// Maps to TransChargesItemTempCoverage.ChargeClassID
        /// </summary>
        virtual public System.String ChargeClassID
        {
            get
            {
                return base.GetSystemString(TransChargesItemTempCoverageMetadata.ColumnNames.ChargeClassID);
            }

            set
            {
                base.SetSystemString(TransChargesItemTempCoverageMetadata.ColumnNames.ChargeClassID, value);
            }
        }

        /// <summary>
        /// Maps to TransChargesItemTempCoverage.ChargeQuantity
        /// </summary>
        virtual public System.Decimal? ChargeQuantity
        {
            get
            {
                return base.GetSystemDecimal(TransChargesItemTempCoverageMetadata.ColumnNames.ChargeQuantity);
            }

            set
            {
                base.SetSystemDecimal(TransChargesItemTempCoverageMetadata.ColumnNames.ChargeQuantity, value);
            }
        }

        /// <summary>
        /// Maps to TransChargesItemTempCoverage.Price
        /// </summary>
        virtual public System.Decimal? Price
        {
            get
            {
                return base.GetSystemDecimal(TransChargesItemTempCoverageMetadata.ColumnNames.Price);
            }

            set
            {
                base.SetSystemDecimal(TransChargesItemTempCoverageMetadata.ColumnNames.Price, value);
            }
        }

        /// <summary>
        /// Maps to TransChargesItemTempCoverage.ParamedicCollectionName
        /// </summary>
        virtual public System.String ParamedicCollectionName
        {
            get
            {
                return base.GetSystemString(TransChargesItemTempCoverageMetadata.ColumnNames.ParamedicCollectionName);
            }

            set
            {
                base.SetSystemString(TransChargesItemTempCoverageMetadata.ColumnNames.ParamedicCollectionName, value);
            }
        }

        /// <summary>
        /// Maps to TransChargesItemTempCoverage.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(TransChargesItemTempCoverageMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(TransChargesItemTempCoverageMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to TransChargesItemTempCoverage.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(TransChargesItemTempCoverageMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(TransChargesItemTempCoverageMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esTransChargesItemTempCoverage entity)
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

            public System.String ReferenceNo
            {
                get
                {
                    System.String data = entity.ReferenceNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ReferenceNo = null;
                    else entity.ReferenceNo = Convert.ToString(value);
                }
            }

            public System.String ReferenceSequenceNo
            {
                get
                {
                    System.String data = entity.ReferenceSequenceNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ReferenceSequenceNo = null;
                    else entity.ReferenceSequenceNo = Convert.ToString(value);
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

            public System.String ChargeClassID
            {
                get
                {
                    System.String data = entity.ChargeClassID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ChargeClassID = null;
                    else entity.ChargeClassID = Convert.ToString(value);
                }
            }

            public System.String ChargeQuantity
            {
                get
                {
                    System.Decimal? data = entity.ChargeQuantity;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ChargeQuantity = null;
                    else entity.ChargeQuantity = Convert.ToDecimal(value);
                }
            }

            public System.String Price
            {
                get
                {
                    System.Decimal? data = entity.Price;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Price = null;
                    else entity.Price = Convert.ToDecimal(value);
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


            private esTransChargesItemTempCoverage entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esTransChargesItemTempCoverageQuery query)
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
                throw new Exception("esTransChargesItemTempCoverage can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    public partial class TransChargesItemTempCoverage : esTransChargesItemTempCoverage
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
    abstract public class esTransChargesItemTempCoverageQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return TransChargesItemTempCoverageMetadata.Meta();
            }
        }


        public esQueryItem RegistrationNo
        {
            get
            {
                return new esQueryItem(this, TransChargesItemTempCoverageMetadata.ColumnNames.RegistrationNo, esSystemType.String);
            }
        }

        public esQueryItem TransactionNo
        {
            get
            {
                return new esQueryItem(this, TransChargesItemTempCoverageMetadata.ColumnNames.TransactionNo, esSystemType.String);
            }
        }

        public esQueryItem SequenceNo
        {
            get
            {
                return new esQueryItem(this, TransChargesItemTempCoverageMetadata.ColumnNames.SequenceNo, esSystemType.String);
            }
        }

        public esQueryItem ReferenceNo
        {
            get
            {
                return new esQueryItem(this, TransChargesItemTempCoverageMetadata.ColumnNames.ReferenceNo, esSystemType.String);
            }
        }

        public esQueryItem ReferenceSequenceNo
        {
            get
            {
                return new esQueryItem(this, TransChargesItemTempCoverageMetadata.ColumnNames.ReferenceSequenceNo, esSystemType.String);
            }
        }

        public esQueryItem ItemID
        {
            get
            {
                return new esQueryItem(this, TransChargesItemTempCoverageMetadata.ColumnNames.ItemID, esSystemType.String);
            }
        }

        public esQueryItem ChargeClassID
        {
            get
            {
                return new esQueryItem(this, TransChargesItemTempCoverageMetadata.ColumnNames.ChargeClassID, esSystemType.String);
            }
        }

        public esQueryItem ChargeQuantity
        {
            get
            {
                return new esQueryItem(this, TransChargesItemTempCoverageMetadata.ColumnNames.ChargeQuantity, esSystemType.Decimal);
            }
        }

        public esQueryItem Price
        {
            get
            {
                return new esQueryItem(this, TransChargesItemTempCoverageMetadata.ColumnNames.Price, esSystemType.Decimal);
            }
        }

        public esQueryItem ParamedicCollectionName
        {
            get
            {
                return new esQueryItem(this, TransChargesItemTempCoverageMetadata.ColumnNames.ParamedicCollectionName, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, TransChargesItemTempCoverageMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, TransChargesItemTempCoverageMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("TransChargesItemTempCoverageCollection")]
    public partial class TransChargesItemTempCoverageCollection : esTransChargesItemTempCoverageCollection, IEnumerable<TransChargesItemTempCoverage>
    {
        public TransChargesItemTempCoverageCollection()
        {

        }

        public static implicit operator List<TransChargesItemTempCoverage>(TransChargesItemTempCoverageCollection coll)
        {
            List<TransChargesItemTempCoverage> list = new List<TransChargesItemTempCoverage>();

            foreach (TransChargesItemTempCoverage emp in coll)
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
                return TransChargesItemTempCoverageMetadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new TransChargesItemTempCoverageQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new TransChargesItemTempCoverage(row);
        }

        override protected esEntity CreateEntity()
        {
            return new TransChargesItemTempCoverage();
        }


        #endregion


        [BrowsableAttribute(false)]
        public TransChargesItemTempCoverageQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new TransChargesItemTempCoverageQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(TransChargesItemTempCoverageQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public TransChargesItemTempCoverage AddNew()
        {
            TransChargesItemTempCoverage entity = base.AddNewEntity() as TransChargesItemTempCoverage;

            return entity;
        }

        public TransChargesItemTempCoverage FindByPrimaryKey(System.String registrationNo, System.String transactionNo, System.String sequenceNo, System.String chargeClassID)
        {
            return base.FindByPrimaryKey(registrationNo, transactionNo, sequenceNo, chargeClassID) as TransChargesItemTempCoverage;
        }


        #region IEnumerable<TransChargesItemTempCoverage> Members

        IEnumerator<TransChargesItemTempCoverage> IEnumerable<TransChargesItemTempCoverage>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as TransChargesItemTempCoverage;
            }
        }

        #endregion

        private TransChargesItemTempCoverageQuery query;
    }


    /// <summary>
    /// Encapsulates the 'TransChargesItemTempCoverage' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("TransChargesItemTempCoverage ({RegistrationNo},{TransactionNo},{SequenceNo},{ChargeClassID})")]
    [Serializable]
    public partial class TransChargesItemTempCoverage : esTransChargesItemTempCoverage
    {
        public TransChargesItemTempCoverage()
        {

        }

        public TransChargesItemTempCoverage(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return TransChargesItemTempCoverageMetadata.Meta();
            }
        }



        override protected esTransChargesItemTempCoverageQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new TransChargesItemTempCoverageQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public TransChargesItemTempCoverageQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new TransChargesItemTempCoverageQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(TransChargesItemTempCoverageQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private TransChargesItemTempCoverageQuery query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class TransChargesItemTempCoverageQuery : esTransChargesItemTempCoverageQuery
    {
        public TransChargesItemTempCoverageQuery()
        {

        }

        public TransChargesItemTempCoverageQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "TransChargesItemTempCoverageQuery";
        }


    }


    [Serializable]
    public partial class TransChargesItemTempCoverageMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected TransChargesItemTempCoverageMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(TransChargesItemTempCoverageMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = TransChargesItemTempCoverageMetadata.PropertyNames.RegistrationNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemTempCoverageMetadata.ColumnNames.TransactionNo, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = TransChargesItemTempCoverageMetadata.PropertyNames.TransactionNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemTempCoverageMetadata.ColumnNames.SequenceNo, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = TransChargesItemTempCoverageMetadata.PropertyNames.SequenceNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 7;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemTempCoverageMetadata.ColumnNames.ReferenceNo, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = TransChargesItemTempCoverageMetadata.PropertyNames.ReferenceNo;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemTempCoverageMetadata.ColumnNames.ReferenceSequenceNo, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = TransChargesItemTempCoverageMetadata.PropertyNames.ReferenceSequenceNo;
            c.CharacterMaxLength = 7;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemTempCoverageMetadata.ColumnNames.ItemID, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = TransChargesItemTempCoverageMetadata.PropertyNames.ItemID;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemTempCoverageMetadata.ColumnNames.ChargeClassID, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = TransChargesItemTempCoverageMetadata.PropertyNames.ChargeClassID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemTempCoverageMetadata.ColumnNames.ChargeQuantity, 7, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = TransChargesItemTempCoverageMetadata.PropertyNames.ChargeQuantity;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemTempCoverageMetadata.ColumnNames.Price, 8, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = TransChargesItemTempCoverageMetadata.PropertyNames.Price;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemTempCoverageMetadata.ColumnNames.ParamedicCollectionName, 9, typeof(System.String), esSystemType.String);
            c.PropertyName = TransChargesItemTempCoverageMetadata.PropertyNames.ParamedicCollectionName;
            c.CharacterMaxLength = 250;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemTempCoverageMetadata.ColumnNames.LastUpdateDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = TransChargesItemTempCoverageMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemTempCoverageMetadata.ColumnNames.LastUpdateByUserID, 11, typeof(System.String), esSystemType.String);
            c.PropertyName = TransChargesItemTempCoverageMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public TransChargesItemTempCoverageMetadata Meta()
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
            public const string ReferenceNo = "ReferenceNo";
            public const string ReferenceSequenceNo = "ReferenceSequenceNo";
            public const string ItemID = "ItemID";
            public const string ChargeClassID = "ChargeClassID";
            public const string ChargeQuantity = "ChargeQuantity";
            public const string Price = "Price";
            public const string ParamedicCollectionName = "ParamedicCollectionName";
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
            public const string ReferenceNo = "ReferenceNo";
            public const string ReferenceSequenceNo = "ReferenceSequenceNo";
            public const string ItemID = "ItemID";
            public const string ChargeClassID = "ChargeClassID";
            public const string ChargeQuantity = "ChargeQuantity";
            public const string Price = "Price";
            public const string ParamedicCollectionName = "ParamedicCollectionName";
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
            lock (typeof(TransChargesItemTempCoverageMetadata))
            {
                if (TransChargesItemTempCoverageMetadata.mapDelegates == null)
                {
                    TransChargesItemTempCoverageMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (TransChargesItemTempCoverageMetadata.meta == null)
                {
                    TransChargesItemTempCoverageMetadata.meta = new TransChargesItemTempCoverageMetadata();
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
                meta.AddTypeMap("ReferenceNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ReferenceSequenceNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ChargeClassID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ChargeQuantity", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("Price", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("ParamedicCollectionName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));



                meta.Source = "TransChargesItemTempCoverage";
                meta.Destination = "TransChargesItemTempCoverage";

                meta.spInsert = "proc_TransChargesItemTempCoverageInsert";
                meta.spUpdate = "proc_TransChargesItemTempCoverageUpdate";
                meta.spDelete = "proc_TransChargesItemTempCoverageDelete";
                meta.spLoadAll = "proc_TransChargesItemTempCoverageLoadAll";
                meta.spLoadByPrimaryKey = "proc_TransChargesItemTempCoverageLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private TransChargesItemTempCoverageMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
