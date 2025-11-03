/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 9/24/2012 11:16:26 AM
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
    abstract public class esCostCalculationBufferCollection : esEntityCollectionWAuditLog
    {
        public esCostCalculationBufferCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "CostCalculationBufferCollection";
        }

        #region Query Logic
        protected void InitQuery(esCostCalculationBufferQuery query)
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
            this.InitQuery(query as esCostCalculationBufferQuery);
        }
        #endregion

        virtual public CostCalculationBuffer DetachEntity(CostCalculationBuffer entity)
        {
            return base.DetachEntity(entity) as CostCalculationBuffer;
        }

        virtual public CostCalculationBuffer AttachEntity(CostCalculationBuffer entity)
        {
            return base.AttachEntity(entity) as CostCalculationBuffer;
        }

        virtual public void Combine(CostCalculationBufferCollection collection)
        {
            base.Combine(collection);
        }

        new public CostCalculationBuffer this[int index]
        {
            get
            {
                return base[index] as CostCalculationBuffer;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(CostCalculationBuffer);
        }
    }



    [Serializable]
    abstract public class esCostCalculationBuffer : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esCostCalculationBufferQuery GetDynamicQuery()
        {
            return null;
        }

        public esCostCalculationBuffer()
        {

        }

        public esCostCalculationBuffer(DataRow row)
            : base(row)
        {

        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(System.String registrationNo, System.String guarantorID, System.String transactionNo, System.String sequenceNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(registrationNo, guarantorID, transactionNo, sequenceNo);
            else
                return LoadByPrimaryKeyStoredProcedure(registrationNo, guarantorID, transactionNo, sequenceNo);
        }

        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String registrationNo, System.String guarantorID, System.String transactionNo, System.String sequenceNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(registrationNo, guarantorID, transactionNo, sequenceNo);
            else
                return LoadByPrimaryKeyStoredProcedure(registrationNo, guarantorID, transactionNo, sequenceNo);
        }

        private bool LoadByPrimaryKeyDynamic(System.String registrationNo, System.String guarantorID, System.String transactionNo, System.String sequenceNo)
        {
            esCostCalculationBufferQuery query = this.GetDynamicQuery();
            query.Where(query.RegistrationNo == registrationNo, query.GuarantorID == guarantorID, query.TransactionNo == transactionNo, query.SequenceNo == sequenceNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(System.String registrationNo, System.String guarantorID, System.String transactionNo, System.String sequenceNo)
        {
            esParameters parms = new esParameters();
            parms.Add("RegistrationNo", registrationNo); parms.Add("GuarantorID", guarantorID); parms.Add("TransactionNo", transactionNo); parms.Add("SequenceNo", sequenceNo);
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
                        case "GuarantorID": this.str.GuarantorID = (string)value; break;
                        case "TransactionNo": this.str.TransactionNo = (string)value; break;
                        case "SequenceNo": this.str.SequenceNo = (string)value; break;
                        case "ItemID": this.str.ItemID = (string)value; break;
                        case "PatientAmount": this.str.PatientAmount = (string)value; break;
                        case "GuarantorAmount": this.str.GuarantorAmount = (string)value; break;
                        case "DiscountAmount": this.str.DiscountAmount = (string)value; break;
                        case "ParamedicAmount": this.str.ParamedicAmount = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "PaymentNo": this.str.PaymentNo = (string)value; break;
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

                        case "ParamedicAmount":

                            if (value == null || value is System.Decimal)
                                this.ParamedicAmount = (System.Decimal?)value;
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
        /// Maps to CostCalculationBuffer.RegistrationNo
        /// </summary>
        virtual public System.String RegistrationNo
        {
            get
            {
                return base.GetSystemString(CostCalculationBufferMetadata.ColumnNames.RegistrationNo);
            }

            set
            {
                base.SetSystemString(CostCalculationBufferMetadata.ColumnNames.RegistrationNo, value);
            }
        }

        /// <summary>
        /// Maps to CostCalculationBuffer.GuarantorID
        /// </summary>
        virtual public System.String GuarantorID
        {
            get
            {
                return base.GetSystemString(CostCalculationBufferMetadata.ColumnNames.GuarantorID);
            }

            set
            {
                base.SetSystemString(CostCalculationBufferMetadata.ColumnNames.GuarantorID, value);
            }
        }

        /// <summary>
        /// Maps to CostCalculationBuffer.TransactionNo
        /// </summary>
        virtual public System.String TransactionNo
        {
            get
            {
                return base.GetSystemString(CostCalculationBufferMetadata.ColumnNames.TransactionNo);
            }

            set
            {
                base.SetSystemString(CostCalculationBufferMetadata.ColumnNames.TransactionNo, value);
            }
        }

        /// <summary>
        /// Maps to CostCalculationBuffer.SequenceNo
        /// </summary>
        virtual public System.String SequenceNo
        {
            get
            {
                return base.GetSystemString(CostCalculationBufferMetadata.ColumnNames.SequenceNo);
            }

            set
            {
                base.SetSystemString(CostCalculationBufferMetadata.ColumnNames.SequenceNo, value);
            }
        }

        /// <summary>
        /// Maps to CostCalculationBuffer.ItemID
        /// </summary>
        virtual public System.String ItemID
        {
            get
            {
                return base.GetSystemString(CostCalculationBufferMetadata.ColumnNames.ItemID);
            }

            set
            {
                base.SetSystemString(CostCalculationBufferMetadata.ColumnNames.ItemID, value);
            }
        }

        /// <summary>
        /// Maps to CostCalculationBuffer.PatientAmount
        /// </summary>
        virtual public System.Decimal? PatientAmount
        {
            get
            {
                return base.GetSystemDecimal(CostCalculationBufferMetadata.ColumnNames.PatientAmount);
            }

            set
            {
                base.SetSystemDecimal(CostCalculationBufferMetadata.ColumnNames.PatientAmount, value);
            }
        }

        /// <summary>
        /// Maps to CostCalculationBuffer.GuarantorAmount
        /// </summary>
        virtual public System.Decimal? GuarantorAmount
        {
            get
            {
                return base.GetSystemDecimal(CostCalculationBufferMetadata.ColumnNames.GuarantorAmount);
            }

            set
            {
                base.SetSystemDecimal(CostCalculationBufferMetadata.ColumnNames.GuarantorAmount, value);
            }
        }

        /// <summary>
        /// Maps to CostCalculationBuffer.DiscountAmount
        /// </summary>
        virtual public System.Decimal? DiscountAmount
        {
            get
            {
                return base.GetSystemDecimal(CostCalculationBufferMetadata.ColumnNames.DiscountAmount);
            }

            set
            {
                base.SetSystemDecimal(CostCalculationBufferMetadata.ColumnNames.DiscountAmount, value);
            }
        }

        /// <summary>
        /// Maps to CostCalculationBuffer.ParamedicAmount
        /// </summary>
        virtual public System.Decimal? ParamedicAmount
        {
            get
            {
                return base.GetSystemDecimal(CostCalculationBufferMetadata.ColumnNames.ParamedicAmount);
            }

            set
            {
                base.SetSystemDecimal(CostCalculationBufferMetadata.ColumnNames.ParamedicAmount, value);
            }
        }

        /// <summary>
        /// Maps to CostCalculationBuffer.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(CostCalculationBufferMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(CostCalculationBufferMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to CostCalculationBuffer.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(CostCalculationBufferMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(CostCalculationBufferMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }

        /// <summary>
        /// Maps to CostCalculationBuffer.PaymentNo
        /// </summary>
        virtual public System.String PaymentNo
        {
            get
            {
                return base.GetSystemString(CostCalculationBufferMetadata.ColumnNames.PaymentNo);
            }

            set
            {
                base.SetSystemString(CostCalculationBufferMetadata.ColumnNames.PaymentNo, value);
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
            public esStrings(esCostCalculationBuffer entity)
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

            public System.String GuarantorID
            {
                get
                {
                    System.String data = entity.GuarantorID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.GuarantorID = null;
                    else entity.GuarantorID = Convert.ToString(value);
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

            public System.String ParamedicAmount
            {
                get
                {
                    System.Decimal? data = entity.ParamedicAmount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ParamedicAmount = null;
                    else entity.ParamedicAmount = Convert.ToDecimal(value);
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


            private esCostCalculationBuffer entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esCostCalculationBufferQuery query)
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
                throw new Exception("esCostCalculationBuffer can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    public partial class CostCalculationBuffer : esCostCalculationBuffer
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
    abstract public class esCostCalculationBufferQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return CostCalculationBufferMetadata.Meta();
            }
        }


        public esQueryItem RegistrationNo
        {
            get
            {
                return new esQueryItem(this, CostCalculationBufferMetadata.ColumnNames.RegistrationNo, esSystemType.String);
            }
        }

        public esQueryItem GuarantorID
        {
            get
            {
                return new esQueryItem(this, CostCalculationBufferMetadata.ColumnNames.GuarantorID, esSystemType.String);
            }
        }

        public esQueryItem TransactionNo
        {
            get
            {
                return new esQueryItem(this, CostCalculationBufferMetadata.ColumnNames.TransactionNo, esSystemType.String);
            }
        }

        public esQueryItem SequenceNo
        {
            get
            {
                return new esQueryItem(this, CostCalculationBufferMetadata.ColumnNames.SequenceNo, esSystemType.String);
            }
        }

        public esQueryItem ItemID
        {
            get
            {
                return new esQueryItem(this, CostCalculationBufferMetadata.ColumnNames.ItemID, esSystemType.String);
            }
        }

        public esQueryItem PatientAmount
        {
            get
            {
                return new esQueryItem(this, CostCalculationBufferMetadata.ColumnNames.PatientAmount, esSystemType.Decimal);
            }
        }

        public esQueryItem GuarantorAmount
        {
            get
            {
                return new esQueryItem(this, CostCalculationBufferMetadata.ColumnNames.GuarantorAmount, esSystemType.Decimal);
            }
        }

        public esQueryItem DiscountAmount
        {
            get
            {
                return new esQueryItem(this, CostCalculationBufferMetadata.ColumnNames.DiscountAmount, esSystemType.Decimal);
            }
        }

        public esQueryItem ParamedicAmount
        {
            get
            {
                return new esQueryItem(this, CostCalculationBufferMetadata.ColumnNames.ParamedicAmount, esSystemType.Decimal);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, CostCalculationBufferMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, CostCalculationBufferMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem PaymentNo
        {
            get
            {
                return new esQueryItem(this, CostCalculationBufferMetadata.ColumnNames.PaymentNo, esSystemType.String);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("CostCalculationBufferCollection")]
    public partial class CostCalculationBufferCollection : esCostCalculationBufferCollection, IEnumerable<CostCalculationBuffer>
    {
        public CostCalculationBufferCollection()
        {

        }

        public static implicit operator List<CostCalculationBuffer>(CostCalculationBufferCollection coll)
        {
            List<CostCalculationBuffer> list = new List<CostCalculationBuffer>();

            foreach (CostCalculationBuffer emp in coll)
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
                return CostCalculationBufferMetadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new CostCalculationBufferQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new CostCalculationBuffer(row);
        }

        override protected esEntity CreateEntity()
        {
            return new CostCalculationBuffer();
        }


        #endregion


        [BrowsableAttribute(false)]
        public CostCalculationBufferQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new CostCalculationBufferQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(CostCalculationBufferQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public CostCalculationBuffer AddNew()
        {
            CostCalculationBuffer entity = base.AddNewEntity() as CostCalculationBuffer;

            return entity;
        }

        public CostCalculationBuffer FindByPrimaryKey(System.String registrationNo, System.String guarantorID, System.String transactionNo, System.String sequenceNo)
        {
            return base.FindByPrimaryKey(registrationNo, guarantorID, transactionNo, sequenceNo) as CostCalculationBuffer;
        }


        #region IEnumerable<CostCalculationBuffer> Members

        IEnumerator<CostCalculationBuffer> IEnumerable<CostCalculationBuffer>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as CostCalculationBuffer;
            }
        }

        #endregion

        private CostCalculationBufferQuery query;
    }


    /// <summary>
    /// Encapsulates the 'CostCalculationBuffer' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("CostCalculationBuffer ({RegistrationNo},{GuarantorID},{TransactionNo},{SequenceNo})")]
    [Serializable]
    public partial class CostCalculationBuffer : esCostCalculationBuffer
    {
        public CostCalculationBuffer()
        {

        }

        public CostCalculationBuffer(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return CostCalculationBufferMetadata.Meta();
            }
        }



        override protected esCostCalculationBufferQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new CostCalculationBufferQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public CostCalculationBufferQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new CostCalculationBufferQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(CostCalculationBufferQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private CostCalculationBufferQuery query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class CostCalculationBufferQuery : esCostCalculationBufferQuery
    {
        public CostCalculationBufferQuery()
        {

        }

        public CostCalculationBufferQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "CostCalculationBufferQuery";
        }


    }


    [Serializable]
    public partial class CostCalculationBufferMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected CostCalculationBufferMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(CostCalculationBufferMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = CostCalculationBufferMetadata.PropertyNames.RegistrationNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(CostCalculationBufferMetadata.ColumnNames.GuarantorID, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = CostCalculationBufferMetadata.PropertyNames.GuarantorID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(CostCalculationBufferMetadata.ColumnNames.TransactionNo, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = CostCalculationBufferMetadata.PropertyNames.TransactionNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(CostCalculationBufferMetadata.ColumnNames.SequenceNo, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = CostCalculationBufferMetadata.PropertyNames.SequenceNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 6;
            _columns.Add(c);

            c = new esColumnMetadata(CostCalculationBufferMetadata.ColumnNames.ItemID, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = CostCalculationBufferMetadata.PropertyNames.ItemID;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(CostCalculationBufferMetadata.ColumnNames.PatientAmount, 5, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = CostCalculationBufferMetadata.PropertyNames.PatientAmount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(CostCalculationBufferMetadata.ColumnNames.GuarantorAmount, 6, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = CostCalculationBufferMetadata.PropertyNames.GuarantorAmount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(CostCalculationBufferMetadata.ColumnNames.DiscountAmount, 7, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = CostCalculationBufferMetadata.PropertyNames.DiscountAmount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(CostCalculationBufferMetadata.ColumnNames.ParamedicAmount, 8, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = CostCalculationBufferMetadata.PropertyNames.ParamedicAmount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(CostCalculationBufferMetadata.ColumnNames.LastUpdateDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = CostCalculationBufferMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(CostCalculationBufferMetadata.ColumnNames.LastUpdateByUserID, 10, typeof(System.String), esSystemType.String);
            c.PropertyName = CostCalculationBufferMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(CostCalculationBufferMetadata.ColumnNames.PaymentNo, 11, typeof(System.String), esSystemType.String);
            c.PropertyName = CostCalculationBufferMetadata.PropertyNames.PaymentNo;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public CostCalculationBufferMetadata Meta()
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
            public const string GuarantorID = "GuarantorID";
            public const string TransactionNo = "TransactionNo";
            public const string SequenceNo = "SequenceNo";
            public const string ItemID = "ItemID";
            public const string PatientAmount = "PatientAmount";
            public const string GuarantorAmount = "GuarantorAmount";
            public const string DiscountAmount = "DiscountAmount";
            public const string ParamedicAmount = "ParamedicAmount";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string PaymentNo = "PaymentNo";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string RegistrationNo = "RegistrationNo";
            public const string GuarantorID = "GuarantorID";
            public const string TransactionNo = "TransactionNo";
            public const string SequenceNo = "SequenceNo";
            public const string ItemID = "ItemID";
            public const string PatientAmount = "PatientAmount";
            public const string GuarantorAmount = "GuarantorAmount";
            public const string DiscountAmount = "DiscountAmount";
            public const string ParamedicAmount = "ParamedicAmount";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
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
            lock (typeof(CostCalculationBufferMetadata))
            {
                if (CostCalculationBufferMetadata.mapDelegates == null)
                {
                    CostCalculationBufferMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (CostCalculationBufferMetadata.meta == null)
                {
                    CostCalculationBufferMetadata.meta = new CostCalculationBufferMetadata();
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
                meta.AddTypeMap("GuarantorID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SequenceNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("PatientAmount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("GuarantorAmount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("DiscountAmount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("ParamedicAmount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("PaymentNo", new esTypeMap("varchar", "System.String"));



                meta.Source = "CostCalculationBuffer";
                meta.Destination = "CostCalculationBuffer";

                meta.spInsert = "proc_CostCalculationBufferInsert";
                meta.spUpdate = "proc_CostCalculationBufferUpdate";
                meta.spDelete = "proc_CostCalculationBufferDelete";
                meta.spLoadAll = "proc_CostCalculationBufferLoadAll";
                meta.spLoadByPrimaryKey = "proc_CostCalculationBufferLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private CostCalculationBufferMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
