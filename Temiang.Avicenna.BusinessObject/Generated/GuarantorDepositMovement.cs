/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 4/17/2015 2:53:33 PM
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
    abstract public class esGuarantorDepositMovementCollection : esEntityCollectionWAuditLog
    {
        public esGuarantorDepositMovementCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "GuarantorDepositMovementCollection";
        }

        #region Query Logic
        protected void InitQuery(esGuarantorDepositMovementQuery query)
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
            this.InitQuery(query as esGuarantorDepositMovementQuery);
        }
        #endregion

        virtual public GuarantorDepositMovement DetachEntity(GuarantorDepositMovement entity)
        {
            return base.DetachEntity(entity) as GuarantorDepositMovement;
        }

        virtual public GuarantorDepositMovement AttachEntity(GuarantorDepositMovement entity)
        {
            return base.AttachEntity(entity) as GuarantorDepositMovement;
        }

        virtual public void Combine(GuarantorDepositMovementCollection collection)
        {
            base.Combine(collection);
        }

        new public GuarantorDepositMovement this[int index]
        {
            get
            {
                return base[index] as GuarantorDepositMovement;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(GuarantorDepositMovement);
        }
    }



    [Serializable]
    abstract public class esGuarantorDepositMovement : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esGuarantorDepositMovementQuery GetDynamicQuery()
        {
            return null;
        }

        public esGuarantorDepositMovement()
        {

        }

        public esGuarantorDepositMovement(DataRow row)
            : base(row)
        {

        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(System.Guid movementID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(movementID);
            else
                return LoadByPrimaryKeyStoredProcedure(movementID);
        }

        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Guid movementID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(movementID);
            else
                return LoadByPrimaryKeyStoredProcedure(movementID);
        }

        private bool LoadByPrimaryKeyDynamic(System.Guid movementID)
        {
            esGuarantorDepositMovementQuery query = this.GetDynamicQuery();
            query.Where(query.MovementID == movementID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(System.Guid movementID)
        {
            esParameters parms = new esParameters();
            parms.Add("MovementID", movementID);
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
                        case "MovementID": this.str.MovementID = (string)value; break;
                        case "MovementDate": this.str.MovementDate = (string)value; break;
                        case "GuarantorID": this.str.GuarantorID = (string)value; break;
                        case "TransactionCode": this.str.TransactionCode = (string)value; break;
                        case "TransactionNo": this.str.TransactionNo = (string)value; break;
                        case "InitialBalance": this.str.InitialBalance = (string)value; break;
                        case "Debet": this.str.Debet = (string)value; break;
                        case "Credit": this.str.Credit = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "MovementID":

                            if (value == null || value is System.Guid)
                                this.MovementID = (System.Guid?)value;
                            break;

                        case "MovementDate":

                            if (value == null || value is System.DateTime)
                                this.MovementDate = (System.DateTime?)value;
                            break;

                        case "InitialBalance":

                            if (value == null || value is System.Decimal)
                                this.InitialBalance = (System.Decimal?)value;
                            break;

                        case "Debet":

                            if (value == null || value is System.Decimal)
                                this.Debet = (System.Decimal?)value;
                            break;

                        case "Credit":

                            if (value == null || value is System.Decimal)
                                this.Credit = (System.Decimal?)value;
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
        /// Maps to GuarantorDepositMovement.MovementID
        /// </summary>
        virtual public System.Guid? MovementID
        {
            get
            {
                return base.GetSystemGuid(GuarantorDepositMovementMetadata.ColumnNames.MovementID);
            }

            set
            {
                base.SetSystemGuid(GuarantorDepositMovementMetadata.ColumnNames.MovementID, value);
            }
        }

        /// <summary>
        /// Maps to GuarantorDepositMovement.MovementDate
        /// </summary>
        virtual public System.DateTime? MovementDate
        {
            get
            {
                return base.GetSystemDateTime(GuarantorDepositMovementMetadata.ColumnNames.MovementDate);
            }

            set
            {
                base.SetSystemDateTime(GuarantorDepositMovementMetadata.ColumnNames.MovementDate, value);
            }
        }

        /// <summary>
        /// Maps to GuarantorDepositMovement.GuarantorID
        /// </summary>
        virtual public System.String GuarantorID
        {
            get
            {
                return base.GetSystemString(GuarantorDepositMovementMetadata.ColumnNames.GuarantorID);
            }

            set
            {
                base.SetSystemString(GuarantorDepositMovementMetadata.ColumnNames.GuarantorID, value);
            }
        }

        /// <summary>
        /// Maps to GuarantorDepositMovement.TransactionCode
        /// </summary>
        virtual public System.String TransactionCode
        {
            get
            {
                return base.GetSystemString(GuarantorDepositMovementMetadata.ColumnNames.TransactionCode);
            }

            set
            {
                base.SetSystemString(GuarantorDepositMovementMetadata.ColumnNames.TransactionCode, value);
            }
        }

        /// <summary>
        /// Maps to GuarantorDepositMovement.TransactionNo
        /// </summary>
        virtual public System.String TransactionNo
        {
            get
            {
                return base.GetSystemString(GuarantorDepositMovementMetadata.ColumnNames.TransactionNo);
            }

            set
            {
                base.SetSystemString(GuarantorDepositMovementMetadata.ColumnNames.TransactionNo, value);
            }
        }

        /// <summary>
        /// Maps to GuarantorDepositMovement.InitialBalance
        /// </summary>
        virtual public System.Decimal? InitialBalance
        {
            get
            {
                return base.GetSystemDecimal(GuarantorDepositMovementMetadata.ColumnNames.InitialBalance);
            }

            set
            {
                base.SetSystemDecimal(GuarantorDepositMovementMetadata.ColumnNames.InitialBalance, value);
            }
        }

        /// <summary>
        /// Maps to GuarantorDepositMovement.Debet
        /// </summary>
        virtual public System.Decimal? Debet
        {
            get
            {
                return base.GetSystemDecimal(GuarantorDepositMovementMetadata.ColumnNames.Debet);
            }

            set
            {
                base.SetSystemDecimal(GuarantorDepositMovementMetadata.ColumnNames.Debet, value);
            }
        }

        /// <summary>
        /// Maps to GuarantorDepositMovement.Credit
        /// </summary>
        virtual public System.Decimal? Credit
        {
            get
            {
                return base.GetSystemDecimal(GuarantorDepositMovementMetadata.ColumnNames.Credit);
            }

            set
            {
                base.SetSystemDecimal(GuarantorDepositMovementMetadata.ColumnNames.Credit, value);
            }
        }

        /// <summary>
        /// Maps to GuarantorDepositMovement.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(GuarantorDepositMovementMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(GuarantorDepositMovementMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to GuarantorDepositMovement.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(GuarantorDepositMovementMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(GuarantorDepositMovementMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esGuarantorDepositMovement entity)
            {
                this.entity = entity;
            }


            public System.String MovementID
            {
                get
                {
                    System.Guid? data = entity.MovementID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.MovementID = null;
                    else entity.MovementID = new Guid(value);
                }
            }

            public System.String MovementDate
            {
                get
                {
                    System.DateTime? data = entity.MovementDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.MovementDate = null;
                    else entity.MovementDate = Convert.ToDateTime(value);
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

            public System.String TransactionCode
            {
                get
                {
                    System.String data = entity.TransactionCode;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TransactionCode = null;
                    else entity.TransactionCode = Convert.ToString(value);
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

            public System.String InitialBalance
            {
                get
                {
                    System.Decimal? data = entity.InitialBalance;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.InitialBalance = null;
                    else entity.InitialBalance = Convert.ToDecimal(value);
                }
            }

            public System.String Debet
            {
                get
                {
                    System.Decimal? data = entity.Debet;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Debet = null;
                    else entity.Debet = Convert.ToDecimal(value);
                }
            }

            public System.String Credit
            {
                get
                {
                    System.Decimal? data = entity.Credit;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Credit = null;
                    else entity.Credit = Convert.ToDecimal(value);
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


            private esGuarantorDepositMovement entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esGuarantorDepositMovementQuery query)
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
                throw new Exception("esGuarantorDepositMovement can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    public partial class GuarantorDepositMovement : esGuarantorDepositMovement
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
    abstract public class esGuarantorDepositMovementQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return GuarantorDepositMovementMetadata.Meta();
            }
        }


        public esQueryItem MovementID
        {
            get
            {
                return new esQueryItem(this, GuarantorDepositMovementMetadata.ColumnNames.MovementID, esSystemType.Guid);
            }
        }

        public esQueryItem MovementDate
        {
            get
            {
                return new esQueryItem(this, GuarantorDepositMovementMetadata.ColumnNames.MovementDate, esSystemType.DateTime);
            }
        }

        public esQueryItem GuarantorID
        {
            get
            {
                return new esQueryItem(this, GuarantorDepositMovementMetadata.ColumnNames.GuarantorID, esSystemType.String);
            }
        }

        public esQueryItem TransactionCode
        {
            get
            {
                return new esQueryItem(this, GuarantorDepositMovementMetadata.ColumnNames.TransactionCode, esSystemType.String);
            }
        }

        public esQueryItem TransactionNo
        {
            get
            {
                return new esQueryItem(this, GuarantorDepositMovementMetadata.ColumnNames.TransactionNo, esSystemType.String);
            }
        }

        public esQueryItem InitialBalance
        {
            get
            {
                return new esQueryItem(this, GuarantorDepositMovementMetadata.ColumnNames.InitialBalance, esSystemType.Decimal);
            }
        }

        public esQueryItem Debet
        {
            get
            {
                return new esQueryItem(this, GuarantorDepositMovementMetadata.ColumnNames.Debet, esSystemType.Decimal);
            }
        }

        public esQueryItem Credit
        {
            get
            {
                return new esQueryItem(this, GuarantorDepositMovementMetadata.ColumnNames.Credit, esSystemType.Decimal);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, GuarantorDepositMovementMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, GuarantorDepositMovementMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("GuarantorDepositMovementCollection")]
    public partial class GuarantorDepositMovementCollection : esGuarantorDepositMovementCollection, IEnumerable<GuarantorDepositMovement>
    {
        public GuarantorDepositMovementCollection()
        {

        }

        public static implicit operator List<GuarantorDepositMovement>(GuarantorDepositMovementCollection coll)
        {
            List<GuarantorDepositMovement> list = new List<GuarantorDepositMovement>();

            foreach (GuarantorDepositMovement emp in coll)
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
                return GuarantorDepositMovementMetadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new GuarantorDepositMovementQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new GuarantorDepositMovement(row);
        }

        override protected esEntity CreateEntity()
        {
            return new GuarantorDepositMovement();
        }


        #endregion


        [BrowsableAttribute(false)]
        public GuarantorDepositMovementQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new GuarantorDepositMovementQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(GuarantorDepositMovementQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public GuarantorDepositMovement AddNew()
        {
            GuarantorDepositMovement entity = base.AddNewEntity() as GuarantorDepositMovement;

            return entity;
        }

        public GuarantorDepositMovement FindByPrimaryKey(System.Guid movementID)
        {
            return base.FindByPrimaryKey(movementID) as GuarantorDepositMovement;
        }


        #region IEnumerable<GuarantorDepositMovement> Members

        IEnumerator<GuarantorDepositMovement> IEnumerable<GuarantorDepositMovement>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as GuarantorDepositMovement;
            }
        }

        #endregion

        private GuarantorDepositMovementQuery query;
    }


    /// <summary>
    /// Encapsulates the 'GuarantorDepositMovement' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("GuarantorDepositMovement ({MovementID})")]
    [Serializable]
    public partial class GuarantorDepositMovement : esGuarantorDepositMovement
    {
        public GuarantorDepositMovement()
        {

        }

        public GuarantorDepositMovement(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return GuarantorDepositMovementMetadata.Meta();
            }
        }



        override protected esGuarantorDepositMovementQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new GuarantorDepositMovementQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public GuarantorDepositMovementQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new GuarantorDepositMovementQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(GuarantorDepositMovementQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private GuarantorDepositMovementQuery query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class GuarantorDepositMovementQuery : esGuarantorDepositMovementQuery
    {
        public GuarantorDepositMovementQuery()
        {

        }

        public GuarantorDepositMovementQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "GuarantorDepositMovementQuery";
        }


    }


    [Serializable]
    public partial class GuarantorDepositMovementMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected GuarantorDepositMovementMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(GuarantorDepositMovementMetadata.ColumnNames.MovementID, 0, typeof(System.Guid), esSystemType.Guid);
            c.PropertyName = GuarantorDepositMovementMetadata.PropertyNames.MovementID;
            c.IsInPrimaryKey = true;
            c.HasDefault = true;
            c.Default = @"(newid())";
            _columns.Add(c);

            c = new esColumnMetadata(GuarantorDepositMovementMetadata.ColumnNames.MovementDate, 1, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = GuarantorDepositMovementMetadata.PropertyNames.MovementDate;
            _columns.Add(c);

            c = new esColumnMetadata(GuarantorDepositMovementMetadata.ColumnNames.GuarantorID, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = GuarantorDepositMovementMetadata.PropertyNames.GuarantorID;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(GuarantorDepositMovementMetadata.ColumnNames.TransactionCode, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = GuarantorDepositMovementMetadata.PropertyNames.TransactionCode;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(GuarantorDepositMovementMetadata.ColumnNames.TransactionNo, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = GuarantorDepositMovementMetadata.PropertyNames.TransactionNo;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(GuarantorDepositMovementMetadata.ColumnNames.InitialBalance, 5, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = GuarantorDepositMovementMetadata.PropertyNames.InitialBalance;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(GuarantorDepositMovementMetadata.ColumnNames.Debet, 6, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = GuarantorDepositMovementMetadata.PropertyNames.Debet;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(GuarantorDepositMovementMetadata.ColumnNames.Credit, 7, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = GuarantorDepositMovementMetadata.PropertyNames.Credit;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(GuarantorDepositMovementMetadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = GuarantorDepositMovementMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(GuarantorDepositMovementMetadata.ColumnNames.LastUpdateByUserID, 9, typeof(System.String), esSystemType.String);
            c.PropertyName = GuarantorDepositMovementMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public GuarantorDepositMovementMetadata Meta()
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
            public const string MovementID = "MovementID";
            public const string MovementDate = "MovementDate";
            public const string GuarantorID = "GuarantorID";
            public const string TransactionCode = "TransactionCode";
            public const string TransactionNo = "TransactionNo";
            public const string InitialBalance = "InitialBalance";
            public const string Debet = "Debet";
            public const string Credit = "Credit";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string MovementID = "MovementID";
            public const string MovementDate = "MovementDate";
            public const string GuarantorID = "GuarantorID";
            public const string TransactionCode = "TransactionCode";
            public const string TransactionNo = "TransactionNo";
            public const string InitialBalance = "InitialBalance";
            public const string Debet = "Debet";
            public const string Credit = "Credit";
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
            lock (typeof(GuarantorDepositMovementMetadata))
            {
                if (GuarantorDepositMovementMetadata.mapDelegates == null)
                {
                    GuarantorDepositMovementMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (GuarantorDepositMovementMetadata.meta == null)
                {
                    GuarantorDepositMovementMetadata.meta = new GuarantorDepositMovementMetadata();
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


                meta.AddTypeMap("MovementID", new esTypeMap("uniqueidentifier", "System.Guid"));
                meta.AddTypeMap("MovementDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("GuarantorID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("TransactionCode", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("InitialBalance", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("Debet", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("Credit", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));



                meta.Source = "GuarantorDepositMovement";
                meta.Destination = "GuarantorDepositMovement";

                meta.spInsert = "proc_GuarantorDepositMovementInsert";
                meta.spUpdate = "proc_GuarantorDepositMovementUpdate";
                meta.spDelete = "proc_GuarantorDepositMovementDelete";
                meta.spLoadAll = "proc_GuarantorDepositMovementLoadAll";
                meta.spLoadByPrimaryKey = "proc_GuarantorDepositMovementLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private GuarantorDepositMovementMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
