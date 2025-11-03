/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 4/10/2013 10:50:09 AM
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
    abstract public class esInvoiceSupplierItemConsignmentCollection : esEntityCollectionWAuditLog
    {
        public esInvoiceSupplierItemConsignmentCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "InvoiceSupplierItemConsignmentCollection";
        }

        #region Query Logic
        protected void InitQuery(esInvoiceSupplierItemConsignmentQuery query)
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
            this.InitQuery(query as esInvoiceSupplierItemConsignmentQuery);
        }
        #endregion

        virtual public InvoiceSupplierItemConsignment DetachEntity(InvoiceSupplierItemConsignment entity)
        {
            return base.DetachEntity(entity) as InvoiceSupplierItemConsignment;
        }

        virtual public InvoiceSupplierItemConsignment AttachEntity(InvoiceSupplierItemConsignment entity)
        {
            return base.AttachEntity(entity) as InvoiceSupplierItemConsignment;
        }

        virtual public void Combine(InvoiceSupplierItemConsignmentCollection collection)
        {
            base.Combine(collection);
        }

        new public InvoiceSupplierItemConsignment this[int index]
        {
            get
            {
                return base[index] as InvoiceSupplierItemConsignment;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(InvoiceSupplierItemConsignment);
        }
    }



    [Serializable]
    abstract public class esInvoiceSupplierItemConsignment : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esInvoiceSupplierItemConsignmentQuery GetDynamicQuery()
        {
            return null;
        }

        public esInvoiceSupplierItemConsignment()
        {

        }

        public esInvoiceSupplierItemConsignment(DataRow row)
            : base(row)
        {

        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(System.String invoiceNo, System.String transactionNo, System.String sequenceNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(invoiceNo, transactionNo, sequenceNo);
            else
                return LoadByPrimaryKeyStoredProcedure(invoiceNo, transactionNo, sequenceNo);
        }

        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String invoiceNo, System.String transactionNo, System.String sequenceNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(invoiceNo, transactionNo, sequenceNo);
            else
                return LoadByPrimaryKeyStoredProcedure(invoiceNo, transactionNo, sequenceNo);
        }

        private bool LoadByPrimaryKeyDynamic(System.String invoiceNo, System.String transactionNo, System.String sequenceNo)
        {
            esInvoiceSupplierItemConsignmentQuery query = this.GetDynamicQuery();
            query.Where(query.InvoiceNo == invoiceNo, query.TransactionNo == transactionNo, query.SequenceNo == sequenceNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(System.String invoiceNo, System.String transactionNo, System.String sequenceNo)
        {
            esParameters parms = new esParameters();
            parms.Add("InvoiceNo", invoiceNo); parms.Add("TransactionNo", transactionNo); parms.Add("SequenceNo", sequenceNo);
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
                        case "InvoiceNo": this.str.InvoiceNo = (string)value; break;
                        case "TransactionNo": this.str.TransactionNo = (string)value; break;
                        case "SequenceNo": this.str.SequenceNo = (string)value; break;
                        case "ItemID": this.str.ItemID = (string)value; break;
                        case "Qty": this.str.Qty = (string)value; break;
                        case "Price": this.str.Price = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "Qty":

                            if (value == null || value is System.Decimal)
                                this.Qty = (System.Decimal?)value;
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
        /// Maps to InvoiceSupplierItemConsignment.InvoiceNo
        /// </summary>
        virtual public System.String InvoiceNo
        {
            get
            {
                return base.GetSystemString(InvoiceSupplierItemConsignmentMetadata.ColumnNames.InvoiceNo);
            }

            set
            {
                base.SetSystemString(InvoiceSupplierItemConsignmentMetadata.ColumnNames.InvoiceNo, value);
            }
        }

        /// <summary>
        /// Maps to InvoiceSupplierItemConsignment.TransactionNo
        /// </summary>
        virtual public System.String TransactionNo
        {
            get
            {
                return base.GetSystemString(InvoiceSupplierItemConsignmentMetadata.ColumnNames.TransactionNo);
            }

            set
            {
                base.SetSystemString(InvoiceSupplierItemConsignmentMetadata.ColumnNames.TransactionNo, value);
            }
        }

        /// <summary>
        /// Maps to InvoiceSupplierItemConsignment.SequenceNo
        /// </summary>
        virtual public System.String SequenceNo
        {
            get
            {
                return base.GetSystemString(InvoiceSupplierItemConsignmentMetadata.ColumnNames.SequenceNo);
            }

            set
            {
                base.SetSystemString(InvoiceSupplierItemConsignmentMetadata.ColumnNames.SequenceNo, value);
            }
        }

        /// <summary>
        /// Maps to InvoiceSupplierItemConsignment.ItemID
        /// </summary>
        virtual public System.String ItemID
        {
            get
            {
                return base.GetSystemString(InvoiceSupplierItemConsignmentMetadata.ColumnNames.ItemID);
            }

            set
            {
                base.SetSystemString(InvoiceSupplierItemConsignmentMetadata.ColumnNames.ItemID, value);
            }
        }

        /// <summary>
        /// Maps to InvoiceSupplierItemConsignment.Qty
        /// </summary>
        virtual public System.Decimal? Qty
        {
            get
            {
                return base.GetSystemDecimal(InvoiceSupplierItemConsignmentMetadata.ColumnNames.Qty);
            }

            set
            {
                base.SetSystemDecimal(InvoiceSupplierItemConsignmentMetadata.ColumnNames.Qty, value);
            }
        }

        /// <summary>
        /// Maps to InvoiceSupplierItemConsignment.Price
        /// </summary>
        virtual public System.Decimal? Price
        {
            get
            {
                return base.GetSystemDecimal(InvoiceSupplierItemConsignmentMetadata.ColumnNames.Price);
            }

            set
            {
                base.SetSystemDecimal(InvoiceSupplierItemConsignmentMetadata.ColumnNames.Price, value);
            }
        }

        /// <summary>
        /// Maps to InvoiceSupplierItemConsignment.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(InvoiceSupplierItemConsignmentMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(InvoiceSupplierItemConsignmentMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to InvoiceSupplierItemConsignment.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(InvoiceSupplierItemConsignmentMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(InvoiceSupplierItemConsignmentMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esInvoiceSupplierItemConsignment entity)
            {
                this.entity = entity;
            }


            public System.String InvoiceNo
            {
                get
                {
                    System.String data = entity.InvoiceNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.InvoiceNo = null;
                    else entity.InvoiceNo = Convert.ToString(value);
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

            public System.String Qty
            {
                get
                {
                    System.Decimal? data = entity.Qty;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Qty = null;
                    else entity.Qty = Convert.ToDecimal(value);
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


            private esInvoiceSupplierItemConsignment entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esInvoiceSupplierItemConsignmentQuery query)
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
                throw new Exception("esInvoiceSupplierItemConsignment can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    public partial class InvoiceSupplierItemConsignment : esInvoiceSupplierItemConsignment
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
    abstract public class esInvoiceSupplierItemConsignmentQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return InvoiceSupplierItemConsignmentMetadata.Meta();
            }
        }


        public esQueryItem InvoiceNo
        {
            get
            {
                return new esQueryItem(this, InvoiceSupplierItemConsignmentMetadata.ColumnNames.InvoiceNo, esSystemType.String);
            }
        }

        public esQueryItem TransactionNo
        {
            get
            {
                return new esQueryItem(this, InvoiceSupplierItemConsignmentMetadata.ColumnNames.TransactionNo, esSystemType.String);
            }
        }

        public esQueryItem SequenceNo
        {
            get
            {
                return new esQueryItem(this, InvoiceSupplierItemConsignmentMetadata.ColumnNames.SequenceNo, esSystemType.String);
            }
        }

        public esQueryItem ItemID
        {
            get
            {
                return new esQueryItem(this, InvoiceSupplierItemConsignmentMetadata.ColumnNames.ItemID, esSystemType.String);
            }
        }

        public esQueryItem Qty
        {
            get
            {
                return new esQueryItem(this, InvoiceSupplierItemConsignmentMetadata.ColumnNames.Qty, esSystemType.Decimal);
            }
        }

        public esQueryItem Price
        {
            get
            {
                return new esQueryItem(this, InvoiceSupplierItemConsignmentMetadata.ColumnNames.Price, esSystemType.Decimal);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, InvoiceSupplierItemConsignmentMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, InvoiceSupplierItemConsignmentMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("InvoiceSupplierItemConsignmentCollection")]
    public partial class InvoiceSupplierItemConsignmentCollection : esInvoiceSupplierItemConsignmentCollection, IEnumerable<InvoiceSupplierItemConsignment>
    {
        public InvoiceSupplierItemConsignmentCollection()
        {

        }

        public static implicit operator List<InvoiceSupplierItemConsignment>(InvoiceSupplierItemConsignmentCollection coll)
        {
            List<InvoiceSupplierItemConsignment> list = new List<InvoiceSupplierItemConsignment>();

            foreach (InvoiceSupplierItemConsignment emp in coll)
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
                return InvoiceSupplierItemConsignmentMetadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new InvoiceSupplierItemConsignmentQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new InvoiceSupplierItemConsignment(row);
        }

        override protected esEntity CreateEntity()
        {
            return new InvoiceSupplierItemConsignment();
        }


        #endregion


        [BrowsableAttribute(false)]
        public InvoiceSupplierItemConsignmentQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new InvoiceSupplierItemConsignmentQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(InvoiceSupplierItemConsignmentQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public InvoiceSupplierItemConsignment AddNew()
        {
            InvoiceSupplierItemConsignment entity = base.AddNewEntity() as InvoiceSupplierItemConsignment;

            return entity;
        }

        public InvoiceSupplierItemConsignment FindByPrimaryKey(System.String invoiceNo, System.String transactionNo, System.String sequenceNo)
        {
            return base.FindByPrimaryKey(invoiceNo, transactionNo, sequenceNo) as InvoiceSupplierItemConsignment;
        }


        #region IEnumerable<InvoiceSupplierItemConsignment> Members

        IEnumerator<InvoiceSupplierItemConsignment> IEnumerable<InvoiceSupplierItemConsignment>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as InvoiceSupplierItemConsignment;
            }
        }

        #endregion

        private InvoiceSupplierItemConsignmentQuery query;
    }


    /// <summary>
    /// Encapsulates the 'InvoiceSupplierItemConsignment' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("InvoiceSupplierItemConsignment ({InvoiceNo},{TransactionNo},{SequenceNo})")]
    [Serializable]
    public partial class InvoiceSupplierItemConsignment : esInvoiceSupplierItemConsignment
    {
        public InvoiceSupplierItemConsignment()
        {

        }

        public InvoiceSupplierItemConsignment(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return InvoiceSupplierItemConsignmentMetadata.Meta();
            }
        }



        override protected esInvoiceSupplierItemConsignmentQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new InvoiceSupplierItemConsignmentQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public InvoiceSupplierItemConsignmentQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new InvoiceSupplierItemConsignmentQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(InvoiceSupplierItemConsignmentQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private InvoiceSupplierItemConsignmentQuery query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class InvoiceSupplierItemConsignmentQuery : esInvoiceSupplierItemConsignmentQuery
    {
        public InvoiceSupplierItemConsignmentQuery()
        {

        }

        public InvoiceSupplierItemConsignmentQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "InvoiceSupplierItemConsignmentQuery";
        }


    }


    [Serializable]
    public partial class InvoiceSupplierItemConsignmentMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected InvoiceSupplierItemConsignmentMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(InvoiceSupplierItemConsignmentMetadata.ColumnNames.InvoiceNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = InvoiceSupplierItemConsignmentMetadata.PropertyNames.InvoiceNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceSupplierItemConsignmentMetadata.ColumnNames.TransactionNo, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = InvoiceSupplierItemConsignmentMetadata.PropertyNames.TransactionNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceSupplierItemConsignmentMetadata.ColumnNames.SequenceNo, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = InvoiceSupplierItemConsignmentMetadata.PropertyNames.SequenceNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 5;
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceSupplierItemConsignmentMetadata.ColumnNames.ItemID, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = InvoiceSupplierItemConsignmentMetadata.PropertyNames.ItemID;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceSupplierItemConsignmentMetadata.ColumnNames.Qty, 4, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = InvoiceSupplierItemConsignmentMetadata.PropertyNames.Qty;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceSupplierItemConsignmentMetadata.ColumnNames.Price, 5, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = InvoiceSupplierItemConsignmentMetadata.PropertyNames.Price;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceSupplierItemConsignmentMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = InvoiceSupplierItemConsignmentMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceSupplierItemConsignmentMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = InvoiceSupplierItemConsignmentMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public InvoiceSupplierItemConsignmentMetadata Meta()
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
            public const string InvoiceNo = "InvoiceNo";
            public const string TransactionNo = "TransactionNo";
            public const string SequenceNo = "SequenceNo";
            public const string ItemID = "ItemID";
            public const string Qty = "Qty";
            public const string Price = "Price";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string InvoiceNo = "InvoiceNo";
            public const string TransactionNo = "TransactionNo";
            public const string SequenceNo = "SequenceNo";
            public const string ItemID = "ItemID";
            public const string Qty = "Qty";
            public const string Price = "Price";
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
            lock (typeof(InvoiceSupplierItemConsignmentMetadata))
            {
                if (InvoiceSupplierItemConsignmentMetadata.mapDelegates == null)
                {
                    InvoiceSupplierItemConsignmentMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (InvoiceSupplierItemConsignmentMetadata.meta == null)
                {
                    InvoiceSupplierItemConsignmentMetadata.meta = new InvoiceSupplierItemConsignmentMetadata();
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


                meta.AddTypeMap("InvoiceNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SequenceNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Qty", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("Price", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));



                meta.Source = "InvoiceSupplierItemConsignment";
                meta.Destination = "InvoiceSupplierItemConsignment";

                meta.spInsert = "proc_InvoiceSupplierItemConsignmentInsert";
                meta.spUpdate = "proc_InvoiceSupplierItemConsignmentUpdate";
                meta.spDelete = "proc_InvoiceSupplierItemConsignmentDelete";
                meta.spLoadAll = "proc_InvoiceSupplierItemConsignmentLoadAll";
                meta.spLoadByPrimaryKey = "proc_InvoiceSupplierItemConsignmentLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private InvoiceSupplierItemConsignmentMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
