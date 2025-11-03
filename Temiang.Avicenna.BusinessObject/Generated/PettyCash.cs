/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 1/2/2014 2:24:37 PM
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
    abstract public class esPettyCashCollection : esEntityCollectionWAuditLog
    {
        public esPettyCashCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "PettyCashCollection";
        }

        #region Query Logic
        protected void InitQuery(esPettyCashQuery query)
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
            this.InitQuery(query as esPettyCashQuery);
        }
        #endregion

        virtual public PettyCash DetachEntity(PettyCash entity)
        {
            return base.DetachEntity(entity) as PettyCash;
        }

        virtual public PettyCash AttachEntity(PettyCash entity)
        {
            return base.AttachEntity(entity) as PettyCash;
        }

        virtual public void Combine(PettyCashCollection collection)
        {
            base.Combine(collection);
        }

        new public PettyCash this[int index]
        {
            get
            {
                return base[index] as PettyCash;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(PettyCash);
        }
    }



    [Serializable]
    abstract public class esPettyCash : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esPettyCashQuery GetDynamicQuery()
        {
            return null;
        }

        public esPettyCash()
        {

        }

        public esPettyCash(DataRow row)
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
            esPettyCashQuery query = this.GetDynamicQuery();
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
                        case "SRPettyCashUnitID": this.str.SRPettyCashUnitID = (string)value; break;
                        case "BankID": this.str.BankID = (string)value; break;
                        case "Notes": this.str.Notes = (string)value; break;
                        case "TotalDebitAmount": this.str.TotalDebitAmount = (string)value; break;
                        case "TotalCreditAmount": this.str.TotalCreditAmount = (string)value; break;
                        case "ReferenceNo": this.str.ReferenceNo = (string)value; break;
                        case "IsApproved": this.str.IsApproved = (string)value; break;
                        case "IsVoid": this.str.IsVoid = (string)value; break;
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

                        case "TotalDebitAmount":

                            if (value == null || value is System.Decimal)
                                this.TotalDebitAmount = (System.Decimal?)value;
                            break;

                        case "TotalCreditAmount":

                            if (value == null || value is System.Decimal)
                                this.TotalCreditAmount = (System.Decimal?)value;
                            break;

                        case "IsApproved":

                            if (value == null || value is System.Boolean)
                                this.IsApproved = (System.Boolean?)value;
                            break;

                        case "IsVoid":

                            if (value == null || value is System.Boolean)
                                this.IsVoid = (System.Boolean?)value;
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
        /// Maps to PettyCash.TransactionNo
        /// </summary>
        virtual public System.String TransactionNo
        {
            get
            {
                return base.GetSystemString(PettyCashMetadata.ColumnNames.TransactionNo);
            }

            set
            {
                base.SetSystemString(PettyCashMetadata.ColumnNames.TransactionNo, value);
            }
        }

        /// <summary>
        /// Maps to PettyCash.TransactionDate
        /// </summary>
        virtual public System.DateTime? TransactionDate
        {
            get
            {
                return base.GetSystemDateTime(PettyCashMetadata.ColumnNames.TransactionDate);
            }

            set
            {
                base.SetSystemDateTime(PettyCashMetadata.ColumnNames.TransactionDate, value);
            }
        }

        /// <summary>
        /// Maps to PettyCash.SRPettyCashUnitID
        /// </summary>
        virtual public System.String SRPettyCashUnitID
        {
            get
            {
                return base.GetSystemString(PettyCashMetadata.ColumnNames.SRPettyCashUnitID);
            }

            set
            {
                base.SetSystemString(PettyCashMetadata.ColumnNames.SRPettyCashUnitID, value);
            }
        }

        /// <summary>
        /// Maps to PettyCash.BankID
        /// </summary>
        virtual public System.String BankID
        {
            get
            {
                return base.GetSystemString(PettyCashMetadata.ColumnNames.BankID);
            }

            set
            {
                base.SetSystemString(PettyCashMetadata.ColumnNames.BankID, value);
            }
        }

        /// <summary>
        /// Maps to PettyCash.Notes
        /// </summary>
        virtual public System.String Notes
        {
            get
            {
                return base.GetSystemString(PettyCashMetadata.ColumnNames.Notes);
            }

            set
            {
                base.SetSystemString(PettyCashMetadata.ColumnNames.Notes, value);
            }
        }

        /// <summary>
        /// Maps to PettyCash.TotalDebitAmount
        /// </summary>
        virtual public System.Decimal? TotalDebitAmount
        {
            get
            {
                return base.GetSystemDecimal(PettyCashMetadata.ColumnNames.TotalDebitAmount);
            }

            set
            {
                base.SetSystemDecimal(PettyCashMetadata.ColumnNames.TotalDebitAmount, value);
            }
        }

        /// <summary>
        /// Maps to PettyCash.TotalCreditAmount
        /// </summary>
        virtual public System.Decimal? TotalCreditAmount
        {
            get
            {
                return base.GetSystemDecimal(PettyCashMetadata.ColumnNames.TotalCreditAmount);
            }

            set
            {
                base.SetSystemDecimal(PettyCashMetadata.ColumnNames.TotalCreditAmount, value);
            }
        }

        /// <summary>
        /// Maps to PettyCash.ReferenceNo
        /// </summary>
        virtual public System.String ReferenceNo
        {
            get
            {
                return base.GetSystemString(PettyCashMetadata.ColumnNames.ReferenceNo);
            }

            set
            {
                base.SetSystemString(PettyCashMetadata.ColumnNames.ReferenceNo, value);
            }
        }

        /// <summary>
        /// Maps to PettyCash.IsApproved
        /// </summary>
        virtual public System.Boolean? IsApproved
        {
            get
            {
                return base.GetSystemBoolean(PettyCashMetadata.ColumnNames.IsApproved);
            }

            set
            {
                base.SetSystemBoolean(PettyCashMetadata.ColumnNames.IsApproved, value);
            }
        }

        /// <summary>
        /// Maps to PettyCash.IsVoid
        /// </summary>
        virtual public System.Boolean? IsVoid
        {
            get
            {
                return base.GetSystemBoolean(PettyCashMetadata.ColumnNames.IsVoid);
            }

            set
            {
                base.SetSystemBoolean(PettyCashMetadata.ColumnNames.IsVoid, value);
            }
        }

        /// <summary>
        /// Maps to PettyCash.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(PettyCashMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(PettyCashMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to PettyCash.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(PettyCashMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(PettyCashMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esPettyCash entity)
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

            public System.String SRPettyCashUnitID
            {
                get
                {
                    System.String data = entity.SRPettyCashUnitID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRPettyCashUnitID = null;
                    else entity.SRPettyCashUnitID = Convert.ToString(value);
                }
            }

            public System.String BankID
            {
                get
                {
                    System.String data = entity.BankID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.BankID = null;
                    else entity.BankID = Convert.ToString(value);
                }
            }

            public System.String Notes
            {
                get
                {
                    System.String data = entity.Notes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Notes = null;
                    else entity.Notes = Convert.ToString(value);
                }
            }

            public System.String TotalDebitAmount
            {
                get
                {
                    System.Decimal? data = entity.TotalDebitAmount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TotalDebitAmount = null;
                    else entity.TotalDebitAmount = Convert.ToDecimal(value);
                }
            }

            public System.String TotalCreditAmount
            {
                get
                {
                    System.Decimal? data = entity.TotalCreditAmount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TotalCreditAmount = null;
                    else entity.TotalCreditAmount = Convert.ToDecimal(value);
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

            public System.String IsApproved
            {
                get
                {
                    System.Boolean? data = entity.IsApproved;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsApproved = null;
                    else entity.IsApproved = Convert.ToBoolean(value);
                }
            }

            public System.String IsVoid
            {
                get
                {
                    System.Boolean? data = entity.IsVoid;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsVoid = null;
                    else entity.IsVoid = Convert.ToBoolean(value);
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


            private esPettyCash entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esPettyCashQuery query)
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
                throw new Exception("esPettyCash can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    public partial class PettyCash : esPettyCash
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
    abstract public class esPettyCashQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return PettyCashMetadata.Meta();
            }
        }


        public esQueryItem TransactionNo
        {
            get
            {
                return new esQueryItem(this, PettyCashMetadata.ColumnNames.TransactionNo, esSystemType.String);
            }
        }

        public esQueryItem TransactionDate
        {
            get
            {
                return new esQueryItem(this, PettyCashMetadata.ColumnNames.TransactionDate, esSystemType.DateTime);
            }
        }

        public esQueryItem SRPettyCashUnitID
        {
            get
            {
                return new esQueryItem(this, PettyCashMetadata.ColumnNames.SRPettyCashUnitID, esSystemType.String);
            }
        }

        public esQueryItem BankID
        {
            get
            {
                return new esQueryItem(this, PettyCashMetadata.ColumnNames.BankID, esSystemType.String);
            }
        }

        public esQueryItem Notes
        {
            get
            {
                return new esQueryItem(this, PettyCashMetadata.ColumnNames.Notes, esSystemType.String);
            }
        }

        public esQueryItem TotalDebitAmount
        {
            get
            {
                return new esQueryItem(this, PettyCashMetadata.ColumnNames.TotalDebitAmount, esSystemType.Decimal);
            }
        }

        public esQueryItem TotalCreditAmount
        {
            get
            {
                return new esQueryItem(this, PettyCashMetadata.ColumnNames.TotalCreditAmount, esSystemType.Decimal);
            }
        }

        public esQueryItem ReferenceNo
        {
            get
            {
                return new esQueryItem(this, PettyCashMetadata.ColumnNames.ReferenceNo, esSystemType.String);
            }
        }

        public esQueryItem IsApproved
        {
            get
            {
                return new esQueryItem(this, PettyCashMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
            }
        }

        public esQueryItem IsVoid
        {
            get
            {
                return new esQueryItem(this, PettyCashMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, PettyCashMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, PettyCashMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("PettyCashCollection")]
    public partial class PettyCashCollection : esPettyCashCollection, IEnumerable<PettyCash>
    {
        public PettyCashCollection()
        {

        }

        public static implicit operator List<PettyCash>(PettyCashCollection coll)
        {
            List<PettyCash> list = new List<PettyCash>();

            foreach (PettyCash emp in coll)
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
                return PettyCashMetadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PettyCashQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new PettyCash(row);
        }

        override protected esEntity CreateEntity()
        {
            return new PettyCash();
        }


        #endregion


        [BrowsableAttribute(false)]
        public PettyCashQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PettyCashQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(PettyCashQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public PettyCash AddNew()
        {
            PettyCash entity = base.AddNewEntity() as PettyCash;

            return entity;
        }

        public PettyCash FindByPrimaryKey(System.String transactionNo)
        {
            return base.FindByPrimaryKey(transactionNo) as PettyCash;
        }


        #region IEnumerable<PettyCash> Members

        IEnumerator<PettyCash> IEnumerable<PettyCash>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as PettyCash;
            }
        }

        #endregion

        private PettyCashQuery query;
    }


    /// <summary>
    /// Encapsulates the 'PettyCash' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("PettyCash ({TransactionNo})")]
    [Serializable]
    public partial class PettyCash : esPettyCash
    {
        public PettyCash()
        {

        }

        public PettyCash(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return PettyCashMetadata.Meta();
            }
        }



        override protected esPettyCashQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PettyCashQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public PettyCashQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PettyCashQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(PettyCashQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private PettyCashQuery query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class PettyCashQuery : esPettyCashQuery
    {
        public PettyCashQuery()
        {

        }

        public PettyCashQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "PettyCashQuery";
        }


    }


    [Serializable]
    public partial class PettyCashMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected PettyCashMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(PettyCashMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = PettyCashMetadata.PropertyNames.TransactionNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(PettyCashMetadata.ColumnNames.TransactionDate, 1, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PettyCashMetadata.PropertyNames.TransactionDate;
            _columns.Add(c);

            c = new esColumnMetadata(PettyCashMetadata.ColumnNames.SRPettyCashUnitID, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = PettyCashMetadata.PropertyNames.SRPettyCashUnitID;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(PettyCashMetadata.ColumnNames.BankID, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = PettyCashMetadata.PropertyNames.BankID;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PettyCashMetadata.ColumnNames.Notes, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = PettyCashMetadata.PropertyNames.Notes;
            c.CharacterMaxLength = 250;
            _columns.Add(c);

            c = new esColumnMetadata(PettyCashMetadata.ColumnNames.TotalDebitAmount, 5, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = PettyCashMetadata.PropertyNames.TotalDebitAmount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PettyCashMetadata.ColumnNames.TotalCreditAmount, 6, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = PettyCashMetadata.PropertyNames.TotalCreditAmount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PettyCashMetadata.ColumnNames.ReferenceNo, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = PettyCashMetadata.PropertyNames.ReferenceNo;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PettyCashMetadata.ColumnNames.IsApproved, 8, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = PettyCashMetadata.PropertyNames.IsApproved;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PettyCashMetadata.ColumnNames.IsVoid, 9, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = PettyCashMetadata.PropertyNames.IsVoid;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PettyCashMetadata.ColumnNames.LastUpdateDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PettyCashMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PettyCashMetadata.ColumnNames.LastUpdateByUserID, 11, typeof(System.String), esSystemType.String);
            c.PropertyName = PettyCashMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public PettyCashMetadata Meta()
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
            public const string SRPettyCashUnitID = "SRPettyCashUnitID";
            public const string BankID = "BankID";
            public const string Notes = "Notes";
            public const string TotalDebitAmount = "TotalDebitAmount";
            public const string TotalCreditAmount = "TotalCreditAmount";
            public const string ReferenceNo = "ReferenceNo";
            public const string IsApproved = "IsApproved";
            public const string IsVoid = "IsVoid";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string TransactionNo = "TransactionNo";
            public const string TransactionDate = "TransactionDate";
            public const string SRPettyCashUnitID = "SRPettyCashUnitID";
            public const string BankID = "BankID";
            public const string Notes = "Notes";
            public const string TotalDebitAmount = "TotalDebitAmount";
            public const string TotalCreditAmount = "TotalCreditAmount";
            public const string ReferenceNo = "ReferenceNo";
            public const string IsApproved = "IsApproved";
            public const string IsVoid = "IsVoid";
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
            lock (typeof(PettyCashMetadata))
            {
                if (PettyCashMetadata.mapDelegates == null)
                {
                    PettyCashMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (PettyCashMetadata.meta == null)
                {
                    PettyCashMetadata.meta = new PettyCashMetadata();
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
                meta.AddTypeMap("SRPettyCashUnitID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("BankID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("TotalDebitAmount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("TotalCreditAmount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("ReferenceNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));



                meta.Source = "PettyCash";
                meta.Destination = "PettyCash";

                meta.spInsert = "proc_PettyCashInsert";
                meta.spUpdate = "proc_PettyCashUpdate";
                meta.spDelete = "proc_PettyCashDelete";
                meta.spLoadAll = "proc_PettyCashLoadAll";
                meta.spLoadByPrimaryKey = "proc_PettyCashLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private PettyCashMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
