/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 1/30/2013 9:14:53 AM
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
    abstract public class esTransPrescriptionItemTempCoverageCollection : esEntityCollectionWAuditLog
    {
        public esTransPrescriptionItemTempCoverageCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "TransPrescriptionItemTempCoverageCollection";
        }

        #region Query Logic
        protected void InitQuery(esTransPrescriptionItemTempCoverageQuery query)
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
            this.InitQuery(query as esTransPrescriptionItemTempCoverageQuery);
        }
        #endregion

        virtual public TransPrescriptionItemTempCoverage DetachEntity(TransPrescriptionItemTempCoverage entity)
        {
            return base.DetachEntity(entity) as TransPrescriptionItemTempCoverage;
        }

        virtual public TransPrescriptionItemTempCoverage AttachEntity(TransPrescriptionItemTempCoverage entity)
        {
            return base.AttachEntity(entity) as TransPrescriptionItemTempCoverage;
        }

        virtual public void Combine(TransPrescriptionItemTempCoverageCollection collection)
        {
            base.Combine(collection);
        }

        new public TransPrescriptionItemTempCoverage this[int index]
        {
            get
            {
                return base[index] as TransPrescriptionItemTempCoverage;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(TransPrescriptionItemTempCoverage);
        }
    }



    [Serializable]
    abstract public class esTransPrescriptionItemTempCoverage : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esTransPrescriptionItemTempCoverageQuery GetDynamicQuery()
        {
            return null;
        }

        public esTransPrescriptionItemTempCoverage()
        {

        }

        public esTransPrescriptionItemTempCoverage(DataRow row)
            : base(row)
        {

        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(System.String registrationNo, System.String prescriptionNo, System.String sequenceNo, System.String chargeClassID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(registrationNo, prescriptionNo, sequenceNo, chargeClassID);
            else
                return LoadByPrimaryKeyStoredProcedure(registrationNo, prescriptionNo, sequenceNo, chargeClassID);
        }

        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String registrationNo, System.String prescriptionNo, System.String sequenceNo, System.String chargeClassID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(registrationNo, prescriptionNo, sequenceNo, chargeClassID);
            else
                return LoadByPrimaryKeyStoredProcedure(registrationNo, prescriptionNo, sequenceNo, chargeClassID);
        }

        private bool LoadByPrimaryKeyDynamic(System.String registrationNo, System.String prescriptionNo, System.String sequenceNo, System.String chargeClassID)
        {
            esTransPrescriptionItemTempCoverageQuery query = this.GetDynamicQuery();
            query.Where(query.RegistrationNo == registrationNo, query.PrescriptionNo == prescriptionNo, query.SequenceNo == sequenceNo, query.ChargeClassID == chargeClassID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(System.String registrationNo, System.String prescriptionNo, System.String sequenceNo, System.String chargeClassID)
        {
            esParameters parms = new esParameters();
            parms.Add("RegistrationNo", registrationNo); parms.Add("PrescriptionNo", prescriptionNo); parms.Add("SequenceNo", sequenceNo); parms.Add("ChargeClassID", chargeClassID);
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
                        case "PrescriptionNo": this.str.PrescriptionNo = (string)value; break;
                        case "SequenceNo": this.str.SequenceNo = (string)value; break;
                        case "ItemID": this.str.ItemID = (string)value; break;
                        case "ChargeClassID": this.str.ChargeClassID = (string)value; break;
                        case "ResultQty": this.str.ResultQty = (string)value; break;
                        case "Price": this.str.Price = (string)value; break;
                        case "LineAmount": this.str.LineAmount = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "ResultQty":

                            if (value == null || value is System.Decimal)
                                this.ResultQty = (System.Decimal?)value;
                            break;

                        case "Price":

                            if (value == null || value is System.Decimal)
                                this.Price = (System.Decimal?)value;
                            break;

                        case "LineAmount":

                            if (value == null || value is System.Decimal)
                                this.LineAmount = (System.Decimal?)value;
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
        /// Maps to TransPrescriptionItemTempCoverage.RegistrationNo
        /// </summary>
        virtual public System.String RegistrationNo
        {
            get
            {
                return base.GetSystemString(TransPrescriptionItemTempCoverageMetadata.ColumnNames.RegistrationNo);
            }

            set
            {
                base.SetSystemString(TransPrescriptionItemTempCoverageMetadata.ColumnNames.RegistrationNo, value);
            }
        }

        /// <summary>
        /// Maps to TransPrescriptionItemTempCoverage.PrescriptionNo
        /// </summary>
        virtual public System.String PrescriptionNo
        {
            get
            {
                return base.GetSystemString(TransPrescriptionItemTempCoverageMetadata.ColumnNames.PrescriptionNo);
            }

            set
            {
                base.SetSystemString(TransPrescriptionItemTempCoverageMetadata.ColumnNames.PrescriptionNo, value);
            }
        }

        /// <summary>
        /// Maps to TransPrescriptionItemTempCoverage.SequenceNo
        /// </summary>
        virtual public System.String SequenceNo
        {
            get
            {
                return base.GetSystemString(TransPrescriptionItemTempCoverageMetadata.ColumnNames.SequenceNo);
            }

            set
            {
                base.SetSystemString(TransPrescriptionItemTempCoverageMetadata.ColumnNames.SequenceNo, value);
            }
        }

        /// <summary>
        /// Maps to TransPrescriptionItemTempCoverage.ItemID
        /// </summary>
        virtual public System.String ItemID
        {
            get
            {
                return base.GetSystemString(TransPrescriptionItemTempCoverageMetadata.ColumnNames.ItemID);
            }

            set
            {
                base.SetSystemString(TransPrescriptionItemTempCoverageMetadata.ColumnNames.ItemID, value);
            }
        }

        /// <summary>
        /// Maps to TransPrescriptionItemTempCoverage.ChargeClassID
        /// </summary>
        virtual public System.String ChargeClassID
        {
            get
            {
                return base.GetSystemString(TransPrescriptionItemTempCoverageMetadata.ColumnNames.ChargeClassID);
            }

            set
            {
                base.SetSystemString(TransPrescriptionItemTempCoverageMetadata.ColumnNames.ChargeClassID, value);
            }
        }

        /// <summary>
        /// Maps to TransPrescriptionItemTempCoverage.ResultQty
        /// </summary>
        virtual public System.Decimal? ResultQty
        {
            get
            {
                return base.GetSystemDecimal(TransPrescriptionItemTempCoverageMetadata.ColumnNames.ResultQty);
            }

            set
            {
                base.SetSystemDecimal(TransPrescriptionItemTempCoverageMetadata.ColumnNames.ResultQty, value);
            }
        }

        /// <summary>
        /// Maps to TransPrescriptionItemTempCoverage.Price
        /// </summary>
        virtual public System.Decimal? Price
        {
            get
            {
                return base.GetSystemDecimal(TransPrescriptionItemTempCoverageMetadata.ColumnNames.Price);
            }

            set
            {
                base.SetSystemDecimal(TransPrescriptionItemTempCoverageMetadata.ColumnNames.Price, value);
            }
        }

        /// <summary>
        /// Maps to TransPrescriptionItemTempCoverage.LineAmount
        /// </summary>
        virtual public System.Decimal? LineAmount
        {
            get
            {
                return base.GetSystemDecimal(TransPrescriptionItemTempCoverageMetadata.ColumnNames.LineAmount);
            }

            set
            {
                base.SetSystemDecimal(TransPrescriptionItemTempCoverageMetadata.ColumnNames.LineAmount, value);
            }
        }

        /// <summary>
        /// Maps to TransPrescriptionItemTempCoverage.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(TransPrescriptionItemTempCoverageMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(TransPrescriptionItemTempCoverageMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to TransPrescriptionItemTempCoverage.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(TransPrescriptionItemTempCoverageMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(TransPrescriptionItemTempCoverageMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esTransPrescriptionItemTempCoverage entity)
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

            public System.String PrescriptionNo
            {
                get
                {
                    System.String data = entity.PrescriptionNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PrescriptionNo = null;
                    else entity.PrescriptionNo = Convert.ToString(value);
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

            public System.String ResultQty
            {
                get
                {
                    System.Decimal? data = entity.ResultQty;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ResultQty = null;
                    else entity.ResultQty = Convert.ToDecimal(value);
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

            public System.String LineAmount
            {
                get
                {
                    System.Decimal? data = entity.LineAmount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.LineAmount = null;
                    else entity.LineAmount = Convert.ToDecimal(value);
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


            private esTransPrescriptionItemTempCoverage entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esTransPrescriptionItemTempCoverageQuery query)
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
                throw new Exception("esTransPrescriptionItemTempCoverage can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    public partial class TransPrescriptionItemTempCoverage : esTransPrescriptionItemTempCoverage
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
    abstract public class esTransPrescriptionItemTempCoverageQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return TransPrescriptionItemTempCoverageMetadata.Meta();
            }
        }


        public esQueryItem RegistrationNo
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionItemTempCoverageMetadata.ColumnNames.RegistrationNo, esSystemType.String);
            }
        }

        public esQueryItem PrescriptionNo
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionItemTempCoverageMetadata.ColumnNames.PrescriptionNo, esSystemType.String);
            }
        }

        public esQueryItem SequenceNo
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionItemTempCoverageMetadata.ColumnNames.SequenceNo, esSystemType.String);
            }
        }

        public esQueryItem ItemID
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionItemTempCoverageMetadata.ColumnNames.ItemID, esSystemType.String);
            }
        }

        public esQueryItem ChargeClassID
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionItemTempCoverageMetadata.ColumnNames.ChargeClassID, esSystemType.String);
            }
        }

        public esQueryItem ResultQty
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionItemTempCoverageMetadata.ColumnNames.ResultQty, esSystemType.Decimal);
            }
        }

        public esQueryItem Price
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionItemTempCoverageMetadata.ColumnNames.Price, esSystemType.Decimal);
            }
        }

        public esQueryItem LineAmount
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionItemTempCoverageMetadata.ColumnNames.LineAmount, esSystemType.Decimal);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionItemTempCoverageMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionItemTempCoverageMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("TransPrescriptionItemTempCoverageCollection")]
    public partial class TransPrescriptionItemTempCoverageCollection : esTransPrescriptionItemTempCoverageCollection, IEnumerable<TransPrescriptionItemTempCoverage>
    {
        public TransPrescriptionItemTempCoverageCollection()
        {

        }

        public static implicit operator List<TransPrescriptionItemTempCoverage>(TransPrescriptionItemTempCoverageCollection coll)
        {
            List<TransPrescriptionItemTempCoverage> list = new List<TransPrescriptionItemTempCoverage>();

            foreach (TransPrescriptionItemTempCoverage emp in coll)
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
                return TransPrescriptionItemTempCoverageMetadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new TransPrescriptionItemTempCoverageQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new TransPrescriptionItemTempCoverage(row);
        }

        override protected esEntity CreateEntity()
        {
            return new TransPrescriptionItemTempCoverage();
        }


        #endregion


        [BrowsableAttribute(false)]
        public TransPrescriptionItemTempCoverageQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new TransPrescriptionItemTempCoverageQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(TransPrescriptionItemTempCoverageQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public TransPrescriptionItemTempCoverage AddNew()
        {
            TransPrescriptionItemTempCoverage entity = base.AddNewEntity() as TransPrescriptionItemTempCoverage;

            return entity;
        }

        public TransPrescriptionItemTempCoverage FindByPrimaryKey(System.String registrationNo, System.String prescriptionNo, System.String sequenceNo, System.String chargeClassID)
        {
            return base.FindByPrimaryKey(registrationNo, prescriptionNo, sequenceNo, chargeClassID) as TransPrescriptionItemTempCoverage;
        }


        #region IEnumerable<TransPrescriptionItemTempCoverage> Members

        IEnumerator<TransPrescriptionItemTempCoverage> IEnumerable<TransPrescriptionItemTempCoverage>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as TransPrescriptionItemTempCoverage;
            }
        }

        #endregion

        private TransPrescriptionItemTempCoverageQuery query;
    }


    /// <summary>
    /// Encapsulates the 'TransPrescriptionItemTempCoverage' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("TransPrescriptionItemTempCoverage ({RegistrationNo},{PrescriptionNo},{SequenceNo},{ChargeClassID})")]
    [Serializable]
    public partial class TransPrescriptionItemTempCoverage : esTransPrescriptionItemTempCoverage
    {
        public TransPrescriptionItemTempCoverage()
        {

        }

        public TransPrescriptionItemTempCoverage(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return TransPrescriptionItemTempCoverageMetadata.Meta();
            }
        }



        override protected esTransPrescriptionItemTempCoverageQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new TransPrescriptionItemTempCoverageQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public TransPrescriptionItemTempCoverageQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new TransPrescriptionItemTempCoverageQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(TransPrescriptionItemTempCoverageQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private TransPrescriptionItemTempCoverageQuery query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class TransPrescriptionItemTempCoverageQuery : esTransPrescriptionItemTempCoverageQuery
    {
        public TransPrescriptionItemTempCoverageQuery()
        {

        }

        public TransPrescriptionItemTempCoverageQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "TransPrescriptionItemTempCoverageQuery";
        }


    }


    [Serializable]
    public partial class TransPrescriptionItemTempCoverageMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected TransPrescriptionItemTempCoverageMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(TransPrescriptionItemTempCoverageMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = TransPrescriptionItemTempCoverageMetadata.PropertyNames.RegistrationNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionItemTempCoverageMetadata.ColumnNames.PrescriptionNo, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = TransPrescriptionItemTempCoverageMetadata.PropertyNames.PrescriptionNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionItemTempCoverageMetadata.ColumnNames.SequenceNo, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = TransPrescriptionItemTempCoverageMetadata.PropertyNames.SequenceNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 3;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionItemTempCoverageMetadata.ColumnNames.ItemID, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = TransPrescriptionItemTempCoverageMetadata.PropertyNames.ItemID;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionItemTempCoverageMetadata.ColumnNames.ChargeClassID, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = TransPrescriptionItemTempCoverageMetadata.PropertyNames.ChargeClassID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionItemTempCoverageMetadata.ColumnNames.ResultQty, 5, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = TransPrescriptionItemTempCoverageMetadata.PropertyNames.ResultQty;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionItemTempCoverageMetadata.ColumnNames.Price, 6, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = TransPrescriptionItemTempCoverageMetadata.PropertyNames.Price;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionItemTempCoverageMetadata.ColumnNames.LineAmount, 7, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = TransPrescriptionItemTempCoverageMetadata.PropertyNames.LineAmount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionItemTempCoverageMetadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = TransPrescriptionItemTempCoverageMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionItemTempCoverageMetadata.ColumnNames.LastUpdateByUserID, 9, typeof(System.String), esSystemType.String);
            c.PropertyName = TransPrescriptionItemTempCoverageMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public TransPrescriptionItemTempCoverageMetadata Meta()
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
            public const string PrescriptionNo = "PrescriptionNo";
            public const string SequenceNo = "SequenceNo";
            public const string ItemID = "ItemID";
            public const string ChargeClassID = "ChargeClassID";
            public const string ResultQty = "ResultQty";
            public const string Price = "Price";
            public const string LineAmount = "LineAmount";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string RegistrationNo = "RegistrationNo";
            public const string PrescriptionNo = "PrescriptionNo";
            public const string SequenceNo = "SequenceNo";
            public const string ItemID = "ItemID";
            public const string ChargeClassID = "ChargeClassID";
            public const string ResultQty = "ResultQty";
            public const string Price = "Price";
            public const string LineAmount = "LineAmount";
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
            lock (typeof(TransPrescriptionItemTempCoverageMetadata))
            {
                if (TransPrescriptionItemTempCoverageMetadata.mapDelegates == null)
                {
                    TransPrescriptionItemTempCoverageMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (TransPrescriptionItemTempCoverageMetadata.meta == null)
                {
                    TransPrescriptionItemTempCoverageMetadata.meta = new TransPrescriptionItemTempCoverageMetadata();
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
                meta.AddTypeMap("PrescriptionNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SequenceNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ChargeClassID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ResultQty", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("Price", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("LineAmount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));



                meta.Source = "TransPrescriptionItemTempCoverage";
                meta.Destination = "TransPrescriptionItemTempCoverage";

                meta.spInsert = "proc_TransPrescriptionItemTempCoverageInsert";
                meta.spUpdate = "proc_TransPrescriptionItemTempCoverageUpdate";
                meta.spDelete = "proc_TransPrescriptionItemTempCoverageDelete";
                meta.spLoadAll = "proc_TransPrescriptionItemTempCoverageLoadAll";
                meta.spLoadByPrimaryKey = "proc_TransPrescriptionItemTempCoverageLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private TransPrescriptionItemTempCoverageMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
