/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 6/13/2012 2:06:32 PM
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
    abstract public class esSupplierContractCollection : esEntityCollectionWAuditLog
    {
        public esSupplierContractCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "SupplierContractCollection";
        }

        #region Query Logic
        protected void InitQuery(esSupplierContractQuery query)
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
            this.InitQuery(query as esSupplierContractQuery);
        }
        #endregion

        virtual public SupplierContract DetachEntity(SupplierContract entity)
        {
            return base.DetachEntity(entity) as SupplierContract;
        }

        virtual public SupplierContract AttachEntity(SupplierContract entity)
        {
            return base.AttachEntity(entity) as SupplierContract;
        }

        virtual public void Combine(SupplierContractCollection collection)
        {
            base.Combine(collection);
        }

        new public SupplierContract this[int index]
        {
            get
            {
                return base[index] as SupplierContract;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(SupplierContract);
        }
    }



    [Serializable]
    abstract public class esSupplierContract : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esSupplierContractQuery GetDynamicQuery()
        {
            return null;
        }

        public esSupplierContract()
        {

        }

        public esSupplierContract(DataRow row)
            : base(row)
        {

        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(System.String transactionNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(transactionNo);
            else
                return LoadByPrimaryKeyStoredProcedure(transactionNo);
        }

        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String transactionNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(transactionNo);
            else
                return LoadByPrimaryKeyStoredProcedure(transactionNo);
        }

        private bool LoadByPrimaryKeyDynamic(System.String transactionNo)
        {
            esSupplierContractQuery query = this.GetDynamicQuery();
            query.Where(query.TransactionNo == transactionNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(System.String transactionNo)
        {
            esParameters parms = new esParameters();
            parms.Add("TransactionNo", transactionNo);
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
                        case "TransactionNo": this.str.TransactionNo = (string)value; break;
                        case "TransactionDate": this.str.TransactionDate = (string)value; break;
                        case "SupplierID": this.str.SupplierID = (string)value; break;
                        case "ContractNo": this.str.ContractNo = (string)value; break;
                        case "ContractStart": this.str.ContractStart = (string)value; break;
                        case "ContractEnd": this.str.ContractEnd = (string)value; break;
                        case "ContractSummary": this.str.ContractSummary = (string)value; break;
                        case "ContractAmount": this.str.ContractAmount = (string)value; break;
                        case "PurchaseAmount": this.str.PurchaseAmount = (string)value; break;
                        case "DiscountAmount": this.str.DiscountAmount = (string)value; break;
                        case "IsActive": this.str.IsActive = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "TransactionDate":

                            if (value == null || value is System.DateTime)
                                this.TransactionDate = (System.DateTime?)value;
                            break;

                        case "ContractStart":

                            if (value == null || value is System.DateTime)
                                this.ContractStart = (System.DateTime?)value;
                            break;

                        case "ContractEnd":

                            if (value == null || value is System.DateTime)
                                this.ContractEnd = (System.DateTime?)value;
                            break;

                        case "ContractAmount":

                            if (value == null || value is System.Decimal)
                                this.ContractAmount = (System.Decimal?)value;
                            break;

                        case "PurchaseAmount":

                            if (value == null || value is System.Decimal)
                                this.PurchaseAmount = (System.Decimal?)value;
                            break;

                        case "DiscountAmount":

                            if (value == null || value is System.Decimal)
                                this.DiscountAmount = (System.Decimal?)value;
                            break;

                        case "IsActive":

                            if (value == null || value is System.Boolean)
                                this.IsActive = (System.Boolean?)value;
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
        /// Maps to SupplierContract.TransactionNo
        /// </summary>
        virtual public System.String TransactionNo
        {
            get
            {
                return base.GetSystemString(SupplierContractMetadata.ColumnNames.TransactionNo);
            }

            set
            {
                base.SetSystemString(SupplierContractMetadata.ColumnNames.TransactionNo, value);
            }
        }

        /// <summary>
        /// Maps to SupplierContract.TransactionDate
        /// </summary>
        virtual public System.DateTime? TransactionDate
        {
            get
            {
                return base.GetSystemDateTime(SupplierContractMetadata.ColumnNames.TransactionDate);
            }

            set
            {
                base.SetSystemDateTime(SupplierContractMetadata.ColumnNames.TransactionDate, value);
            }
        }

        /// <summary>
        /// Maps to SupplierContract.SupplierID
        /// </summary>
        virtual public System.String SupplierID
        {
            get
            {
                return base.GetSystemString(SupplierContractMetadata.ColumnNames.SupplierID);
            }

            set
            {
                base.SetSystemString(SupplierContractMetadata.ColumnNames.SupplierID, value);
            }
        }

        /// <summary>
        /// Maps to SupplierContract.ContractNo
        /// </summary>
        virtual public System.String ContractNo
        {
            get
            {
                return base.GetSystemString(SupplierContractMetadata.ColumnNames.ContractNo);
            }

            set
            {
                base.SetSystemString(SupplierContractMetadata.ColumnNames.ContractNo, value);
            }
        }

        /// <summary>
        /// Maps to SupplierContract.ContractStart
        /// </summary>
        virtual public System.DateTime? ContractStart
        {
            get
            {
                return base.GetSystemDateTime(SupplierContractMetadata.ColumnNames.ContractStart);
            }

            set
            {
                base.SetSystemDateTime(SupplierContractMetadata.ColumnNames.ContractStart, value);
            }
        }

        /// <summary>
        /// Maps to SupplierContract.ContractEnd
        /// </summary>
        virtual public System.DateTime? ContractEnd
        {
            get
            {
                return base.GetSystemDateTime(SupplierContractMetadata.ColumnNames.ContractEnd);
            }

            set
            {
                base.SetSystemDateTime(SupplierContractMetadata.ColumnNames.ContractEnd, value);
            }
        }

        /// <summary>
        /// Maps to SupplierContract.ContractSummary
        /// </summary>
        virtual public System.String ContractSummary
        {
            get
            {
                return base.GetSystemString(SupplierContractMetadata.ColumnNames.ContractSummary);
            }

            set
            {
                base.SetSystemString(SupplierContractMetadata.ColumnNames.ContractSummary, value);
            }
        }

        /// <summary>
        /// Maps to SupplierContract.ContractAmount
        /// </summary>
        virtual public System.Decimal? ContractAmount
        {
            get
            {
                return base.GetSystemDecimal(SupplierContractMetadata.ColumnNames.ContractAmount);
            }

            set
            {
                base.SetSystemDecimal(SupplierContractMetadata.ColumnNames.ContractAmount, value);
            }
        }

        /// <summary>
        /// Maps to SupplierContract.PurchaseAmount
        /// </summary>
        virtual public System.Decimal? PurchaseAmount
        {
            get
            {
                return base.GetSystemDecimal(SupplierContractMetadata.ColumnNames.PurchaseAmount);
            }

            set
            {
                base.SetSystemDecimal(SupplierContractMetadata.ColumnNames.PurchaseAmount, value);
            }
        }

        /// <summary>
        /// Maps to SupplierContract.DiscountAmount
        /// </summary>
        virtual public System.Decimal? DiscountAmount
        {
            get
            {
                return base.GetSystemDecimal(SupplierContractMetadata.ColumnNames.DiscountAmount);
            }

            set
            {
                base.SetSystemDecimal(SupplierContractMetadata.ColumnNames.DiscountAmount, value);
            }
        }

        /// <summary>
        /// Maps to SupplierContract.IsActive
        /// </summary>
        virtual public System.Boolean? IsActive
        {
            get
            {
                return base.GetSystemBoolean(SupplierContractMetadata.ColumnNames.IsActive);
            }

            set
            {
                base.SetSystemBoolean(SupplierContractMetadata.ColumnNames.IsActive, value);
            }
        }

        /// <summary>
        /// Maps to SupplierContract.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(SupplierContractMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(SupplierContractMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to SupplierContract.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(SupplierContractMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(SupplierContractMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esSupplierContract entity)
            {
                this.entity = entity;
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

            public System.String TransactionDate
            {
                get
                {
                    System.DateTime? data = entity.TransactionDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TransactionDate = null;
                    else entity.TransactionDate = Convert.ToDateTime(value);
                }
            }

            public System.String SupplierID
            {
                get
                {
                    System.String data = entity.SupplierID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SupplierID = null;
                    else entity.SupplierID = Convert.ToString(value);
                }
            }

            public System.String ContractNo
            {
                get
                {
                    System.String data = entity.ContractNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ContractNo = null;
                    else entity.ContractNo = Convert.ToString(value);
                }
            }

            public System.String ContractStart
            {
                get
                {
                    System.DateTime? data = entity.ContractStart;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ContractStart = null;
                    else entity.ContractStart = Convert.ToDateTime(value);
                }
            }

            public System.String ContractEnd
            {
                get
                {
                    System.DateTime? data = entity.ContractEnd;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ContractEnd = null;
                    else entity.ContractEnd = Convert.ToDateTime(value);
                }
            }

            public System.String ContractSummary
            {
                get
                {
                    System.String data = entity.ContractSummary;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ContractSummary = null;
                    else entity.ContractSummary = Convert.ToString(value);
                }
            }

            public System.String ContractAmount
            {
                get
                {
                    System.Decimal? data = entity.ContractAmount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ContractAmount = null;
                    else entity.ContractAmount = Convert.ToDecimal(value);
                }
            }

            public System.String PurchaseAmount
            {
                get
                {
                    System.Decimal? data = entity.PurchaseAmount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PurchaseAmount = null;
                    else entity.PurchaseAmount = Convert.ToDecimal(value);
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

            public System.String IsActive
            {
                get
                {
                    System.Boolean? data = entity.IsActive;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsActive = null;
                    else entity.IsActive = Convert.ToBoolean(value);
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


            private esSupplierContract entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esSupplierContractQuery query)
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
                throw new Exception("esSupplierContract can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    public partial class SupplierContract : esSupplierContract
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
    abstract public class esSupplierContractQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return SupplierContractMetadata.Meta();
            }
        }


        public esQueryItem TransactionNo
        {
            get
            {
                return new esQueryItem(this, SupplierContractMetadata.ColumnNames.TransactionNo, esSystemType.String);
            }
        }

        public esQueryItem TransactionDate
        {
            get
            {
                return new esQueryItem(this, SupplierContractMetadata.ColumnNames.TransactionDate, esSystemType.DateTime);
            }
        }

        public esQueryItem SupplierID
        {
            get
            {
                return new esQueryItem(this, SupplierContractMetadata.ColumnNames.SupplierID, esSystemType.String);
            }
        }

        public esQueryItem ContractNo
        {
            get
            {
                return new esQueryItem(this, SupplierContractMetadata.ColumnNames.ContractNo, esSystemType.String);
            }
        }

        public esQueryItem ContractStart
        {
            get
            {
                return new esQueryItem(this, SupplierContractMetadata.ColumnNames.ContractStart, esSystemType.DateTime);
            }
        }

        public esQueryItem ContractEnd
        {
            get
            {
                return new esQueryItem(this, SupplierContractMetadata.ColumnNames.ContractEnd, esSystemType.DateTime);
            }
        }

        public esQueryItem ContractSummary
        {
            get
            {
                return new esQueryItem(this, SupplierContractMetadata.ColumnNames.ContractSummary, esSystemType.String);
            }
        }

        public esQueryItem ContractAmount
        {
            get
            {
                return new esQueryItem(this, SupplierContractMetadata.ColumnNames.ContractAmount, esSystemType.Decimal);
            }
        }

        public esQueryItem PurchaseAmount
        {
            get
            {
                return new esQueryItem(this, SupplierContractMetadata.ColumnNames.PurchaseAmount, esSystemType.Decimal);
            }
        }

        public esQueryItem DiscountAmount
        {
            get
            {
                return new esQueryItem(this, SupplierContractMetadata.ColumnNames.DiscountAmount, esSystemType.Decimal);
            }
        }

        public esQueryItem IsActive
        {
            get
            {
                return new esQueryItem(this, SupplierContractMetadata.ColumnNames.IsActive, esSystemType.Boolean);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, SupplierContractMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, SupplierContractMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("SupplierContractCollection")]
    public partial class SupplierContractCollection : esSupplierContractCollection, IEnumerable<SupplierContract>
    {
        public SupplierContractCollection()
        {

        }

        public static implicit operator List<SupplierContract>(SupplierContractCollection coll)
        {
            List<SupplierContract> list = new List<SupplierContract>();

            foreach (SupplierContract emp in coll)
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
                return SupplierContractMetadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new SupplierContractQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new SupplierContract(row);
        }

        override protected esEntity CreateEntity()
        {
            return new SupplierContract();
        }


        #endregion


        [BrowsableAttribute(false)]
        public SupplierContractQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new SupplierContractQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(SupplierContractQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public SupplierContract AddNew()
        {
            SupplierContract entity = base.AddNewEntity() as SupplierContract;

            return entity;
        }

        public SupplierContract FindByPrimaryKey(System.String transactionNo)
        {
            return base.FindByPrimaryKey(transactionNo) as SupplierContract;
        }


        #region IEnumerable<SupplierContract> Members

        IEnumerator<SupplierContract> IEnumerable<SupplierContract>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as SupplierContract;
            }
        }

        #endregion

        private SupplierContractQuery query;
    }


    /// <summary>
    /// Encapsulates the 'SupplierContract' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("SupplierContract ({TransactionNo})")]
    [Serializable]
    public partial class SupplierContract : esSupplierContract
    {
        public SupplierContract()
        {

        }

        public SupplierContract(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return SupplierContractMetadata.Meta();
            }
        }



        override protected esSupplierContractQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new SupplierContractQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public SupplierContractQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new SupplierContractQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(SupplierContractQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private SupplierContractQuery query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class SupplierContractQuery : esSupplierContractQuery
    {
        public SupplierContractQuery()
        {

        }

        public SupplierContractQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "SupplierContractQuery";
        }


    }


    [Serializable]
    public partial class SupplierContractMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected SupplierContractMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(SupplierContractMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = SupplierContractMetadata.PropertyNames.TransactionNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(SupplierContractMetadata.ColumnNames.TransactionDate, 1, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = SupplierContractMetadata.PropertyNames.TransactionDate;
            _columns.Add(c);

            c = new esColumnMetadata(SupplierContractMetadata.ColumnNames.SupplierID, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = SupplierContractMetadata.PropertyNames.SupplierID;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(SupplierContractMetadata.ColumnNames.ContractNo, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = SupplierContractMetadata.PropertyNames.ContractNo;
            c.CharacterMaxLength = 50;
            _columns.Add(c);

            c = new esColumnMetadata(SupplierContractMetadata.ColumnNames.ContractStart, 4, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = SupplierContractMetadata.PropertyNames.ContractStart;
            _columns.Add(c);

            c = new esColumnMetadata(SupplierContractMetadata.ColumnNames.ContractEnd, 5, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = SupplierContractMetadata.PropertyNames.ContractEnd;
            _columns.Add(c);

            c = new esColumnMetadata(SupplierContractMetadata.ColumnNames.ContractSummary, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = SupplierContractMetadata.PropertyNames.ContractSummary;
            c.CharacterMaxLength = 2147483647;
            _columns.Add(c);

            c = new esColumnMetadata(SupplierContractMetadata.ColumnNames.ContractAmount, 7, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = SupplierContractMetadata.PropertyNames.ContractAmount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(SupplierContractMetadata.ColumnNames.PurchaseAmount, 8, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = SupplierContractMetadata.PropertyNames.PurchaseAmount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(SupplierContractMetadata.ColumnNames.DiscountAmount, 9, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = SupplierContractMetadata.PropertyNames.DiscountAmount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(SupplierContractMetadata.ColumnNames.IsActive, 10, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = SupplierContractMetadata.PropertyNames.IsActive;
            _columns.Add(c);

            c = new esColumnMetadata(SupplierContractMetadata.ColumnNames.LastUpdateDateTime, 11, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = SupplierContractMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(SupplierContractMetadata.ColumnNames.LastUpdateByUserID, 12, typeof(System.String), esSystemType.String);
            c.PropertyName = SupplierContractMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public SupplierContractMetadata Meta()
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
            public const string TransactionNo = "TransactionNo";
            public const string TransactionDate = "TransactionDate";
            public const string SupplierID = "SupplierID";
            public const string ContractNo = "ContractNo";
            public const string ContractStart = "ContractStart";
            public const string ContractEnd = "ContractEnd";
            public const string ContractSummary = "ContractSummary";
            public const string ContractAmount = "ContractAmount";
            public const string PurchaseAmount = "PurchaseAmount";
            public const string DiscountAmount = "DiscountAmount";
            public const string IsActive = "IsActive";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string TransactionNo = "TransactionNo";
            public const string TransactionDate = "TransactionDate";
            public const string SupplierID = "SupplierID";
            public const string ContractNo = "ContractNo";
            public const string ContractStart = "ContractStart";
            public const string ContractEnd = "ContractEnd";
            public const string ContractSummary = "ContractSummary";
            public const string ContractAmount = "ContractAmount";
            public const string PurchaseAmount = "PurchaseAmount";
            public const string DiscountAmount = "DiscountAmount";
            public const string IsActive = "IsActive";
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
            lock (typeof(SupplierContractMetadata))
            {
                if (SupplierContractMetadata.mapDelegates == null)
                {
                    SupplierContractMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (SupplierContractMetadata.meta == null)
                {
                    SupplierContractMetadata.meta = new SupplierContractMetadata();
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


                meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("TransactionDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("SupplierID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ContractNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ContractStart", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("ContractEnd", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("ContractSummary", new esTypeMap("text", "System.String"));
                meta.AddTypeMap("ContractAmount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("PurchaseAmount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("DiscountAmount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));



                meta.Source = "SupplierContract";
                meta.Destination = "SupplierContract";

                meta.spInsert = "proc_SupplierContractInsert";
                meta.spUpdate = "proc_SupplierContractUpdate";
                meta.spDelete = "proc_SupplierContractDelete";
                meta.spLoadAll = "proc_SupplierContractLoadAll";
                meta.spLoadByPrimaryKey = "proc_SupplierContractLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private SupplierContractMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
