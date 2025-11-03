/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 4/14/2015 11:52:52 AM
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
    abstract public class esDietLiquidPatientItemCollection : esEntityCollectionWAuditLog
    {
        public esDietLiquidPatientItemCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "DietLiquidPatientItemCollection";
        }

        #region Query Logic
        protected void InitQuery(esDietLiquidPatientItemQuery query)
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
            this.InitQuery(query as esDietLiquidPatientItemQuery);
        }
        #endregion

        virtual public DietLiquidPatientItem DetachEntity(DietLiquidPatientItem entity)
        {
            return base.DetachEntity(entity) as DietLiquidPatientItem;
        }

        virtual public DietLiquidPatientItem AttachEntity(DietLiquidPatientItem entity)
        {
            return base.AttachEntity(entity) as DietLiquidPatientItem;
        }

        virtual public void Combine(DietLiquidPatientItemCollection collection)
        {
            base.Combine(collection);
        }

        new public DietLiquidPatientItem this[int index]
        {
            get
            {
                return base[index] as DietLiquidPatientItem;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(DietLiquidPatientItem);
        }
    }



    [Serializable]
    abstract public class esDietLiquidPatientItem : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esDietLiquidPatientItemQuery GetDynamicQuery()
        {
            return null;
        }

        public esDietLiquidPatientItem()
        {

        }

        public esDietLiquidPatientItem(DataRow row)
            : base(row)
        {

        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(System.String transactionNo, System.String dietTime, System.String itemID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(transactionNo, dietTime, itemID);
            else
                return LoadByPrimaryKeyStoredProcedure(transactionNo, dietTime, itemID);
        }

        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String transactionNo, System.String dietTime, System.String itemID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(transactionNo, dietTime, itemID);
            else
                return LoadByPrimaryKeyStoredProcedure(transactionNo, dietTime, itemID);
        }

        private bool LoadByPrimaryKeyDynamic(System.String transactionNo, System.String dietTime, System.String itemID)
        {
            esDietLiquidPatientItemQuery query = this.GetDynamicQuery();
            query.Where(query.TransactionNo == transactionNo, query.DietTime == dietTime, query.ItemID == itemID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(System.String transactionNo, System.String dietTime, System.String itemID)
        {
            esParameters parms = new esParameters();
            parms.Add("TransactionNo", transactionNo); parms.Add("DietTime", dietTime); parms.Add("ItemID", itemID);
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
                        case "DietTime": this.str.DietTime = (string)value; break;
                        case "ItemID": this.str.ItemID = (string)value; break;
                        case "Qty": this.str.Qty = (string)value; break;
                        case "SRItemUnit": this.str.SRItemUnit = (string)value; break;
                        case "Notes": this.str.Notes = (string)value; break;
                        case "FoodID": this.str.FoodID = (string)value; break;
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
        /// Maps to DietLiquidPatientItem.TransactionNo
        /// </summary>
        virtual public System.String TransactionNo
        {
            get
            {
                return base.GetSystemString(DietLiquidPatientItemMetadata.ColumnNames.TransactionNo);
            }

            set
            {
                base.SetSystemString(DietLiquidPatientItemMetadata.ColumnNames.TransactionNo, value);
            }
        }

        /// <summary>
        /// Maps to DietLiquidPatientItem.DietTime
        /// </summary>
        virtual public System.String DietTime
        {
            get
            {
                return base.GetSystemString(DietLiquidPatientItemMetadata.ColumnNames.DietTime);
            }

            set
            {
                base.SetSystemString(DietLiquidPatientItemMetadata.ColumnNames.DietTime, value);
            }
        }

        /// <summary>
        /// Maps to DietLiquidPatientItem.ItemID
        /// </summary>
        virtual public System.String ItemID
        {
            get
            {
                return base.GetSystemString(DietLiquidPatientItemMetadata.ColumnNames.ItemID);
            }

            set
            {
                base.SetSystemString(DietLiquidPatientItemMetadata.ColumnNames.ItemID, value);
            }
        }

        /// <summary>
        /// Maps to DietLiquidPatientItem.Qty
        /// </summary>
        virtual public System.Decimal? Qty
        {
            get
            {
                return base.GetSystemDecimal(DietLiquidPatientItemMetadata.ColumnNames.Qty);
            }

            set
            {
                base.SetSystemDecimal(DietLiquidPatientItemMetadata.ColumnNames.Qty, value);
            }
        }

        /// <summary>
        /// Maps to DietLiquidPatientItem.SRItemUnit
        /// </summary>
        virtual public System.String SRItemUnit
        {
            get
            {
                return base.GetSystemString(DietLiquidPatientItemMetadata.ColumnNames.SRItemUnit);
            }

            set
            {
                base.SetSystemString(DietLiquidPatientItemMetadata.ColumnNames.SRItemUnit, value);
            }
        }

        /// <summary>
        /// Maps to DietLiquidPatientItem.Notes
        /// </summary>
        virtual public System.String Notes
        {
            get
            {
                return base.GetSystemString(DietLiquidPatientItemMetadata.ColumnNames.Notes);
            }

            set
            {
                base.SetSystemString(DietLiquidPatientItemMetadata.ColumnNames.Notes, value);
            }
        }

        /// <summary>
        /// Maps to DietLiquidPatientItem.FoodID
        /// </summary>
        virtual public System.String FoodID
        {
            get
            {
                return base.GetSystemString(DietLiquidPatientItemMetadata.ColumnNames.FoodID);
            }

            set
            {
                base.SetSystemString(DietLiquidPatientItemMetadata.ColumnNames.FoodID, value);
            }
        }

        /// <summary>
        /// Maps to DietLiquidPatientItem.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(DietLiquidPatientItemMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(DietLiquidPatientItemMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to DietLiquidPatientItem.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(DietLiquidPatientItemMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(DietLiquidPatientItemMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esDietLiquidPatientItem entity)
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

            public System.String DietTime
            {
                get
                {
                    System.String data = entity.DietTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DietTime = null;
                    else entity.DietTime = Convert.ToString(value);
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

            public System.String SRItemUnit
            {
                get
                {
                    System.String data = entity.SRItemUnit;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRItemUnit = null;
                    else entity.SRItemUnit = Convert.ToString(value);
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

            public System.String FoodID
            {
                get
                {
                    System.String data = entity.FoodID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FoodID = null;
                    else entity.FoodID = Convert.ToString(value);
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


            private esDietLiquidPatientItem entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esDietLiquidPatientItemQuery query)
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
                throw new Exception("esDietLiquidPatientItem can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    public partial class DietLiquidPatientItem : esDietLiquidPatientItem
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
    abstract public class esDietLiquidPatientItemQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return DietLiquidPatientItemMetadata.Meta();
            }
        }


        public esQueryItem TransactionNo
        {
            get
            {
                return new esQueryItem(this, DietLiquidPatientItemMetadata.ColumnNames.TransactionNo, esSystemType.String);
            }
        }

        public esQueryItem DietTime
        {
            get
            {
                return new esQueryItem(this, DietLiquidPatientItemMetadata.ColumnNames.DietTime, esSystemType.String);
            }
        }

        public esQueryItem ItemID
        {
            get
            {
                return new esQueryItem(this, DietLiquidPatientItemMetadata.ColumnNames.ItemID, esSystemType.String);
            }
        }

        public esQueryItem Qty
        {
            get
            {
                return new esQueryItem(this, DietLiquidPatientItemMetadata.ColumnNames.Qty, esSystemType.Decimal);
            }
        }

        public esQueryItem SRItemUnit
        {
            get
            {
                return new esQueryItem(this, DietLiquidPatientItemMetadata.ColumnNames.SRItemUnit, esSystemType.String);
            }
        }

        public esQueryItem Notes
        {
            get
            {
                return new esQueryItem(this, DietLiquidPatientItemMetadata.ColumnNames.Notes, esSystemType.String);
            }
        }

        public esQueryItem FoodID
        {
            get
            {
                return new esQueryItem(this, DietLiquidPatientItemMetadata.ColumnNames.FoodID, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, DietLiquidPatientItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, DietLiquidPatientItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("DietLiquidPatientItemCollection")]
    public partial class DietLiquidPatientItemCollection : esDietLiquidPatientItemCollection, IEnumerable<DietLiquidPatientItem>
    {
        public DietLiquidPatientItemCollection()
        {

        }

        public static implicit operator List<DietLiquidPatientItem>(DietLiquidPatientItemCollection coll)
        {
            List<DietLiquidPatientItem> list = new List<DietLiquidPatientItem>();

            foreach (DietLiquidPatientItem emp in coll)
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
                return DietLiquidPatientItemMetadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new DietLiquidPatientItemQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new DietLiquidPatientItem(row);
        }

        override protected esEntity CreateEntity()
        {
            return new DietLiquidPatientItem();
        }


        #endregion


        [BrowsableAttribute(false)]
        public DietLiquidPatientItemQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new DietLiquidPatientItemQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(DietLiquidPatientItemQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public DietLiquidPatientItem AddNew()
        {
            DietLiquidPatientItem entity = base.AddNewEntity() as DietLiquidPatientItem;

            return entity;
        }

        public DietLiquidPatientItem FindByPrimaryKey(System.String transactionNo, System.String dietTime, System.String itemID)
        {
            return base.FindByPrimaryKey(transactionNo, dietTime, itemID) as DietLiquidPatientItem;
        }


        #region IEnumerable<DietLiquidPatientItem> Members

        IEnumerator<DietLiquidPatientItem> IEnumerable<DietLiquidPatientItem>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as DietLiquidPatientItem;
            }
        }

        #endregion

        private DietLiquidPatientItemQuery query;
    }


    /// <summary>
    /// Encapsulates the 'DietLiquidPatientItem' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("DietLiquidPatientItem ({TransactionNo},{DietTime},{ItemID})")]
    [Serializable]
    public partial class DietLiquidPatientItem : esDietLiquidPatientItem
    {
        public DietLiquidPatientItem()
        {

        }

        public DietLiquidPatientItem(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return DietLiquidPatientItemMetadata.Meta();
            }
        }



        override protected esDietLiquidPatientItemQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new DietLiquidPatientItemQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public DietLiquidPatientItemQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new DietLiquidPatientItemQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(DietLiquidPatientItemQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private DietLiquidPatientItemQuery query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class DietLiquidPatientItemQuery : esDietLiquidPatientItemQuery
    {
        public DietLiquidPatientItemQuery()
        {

        }

        public DietLiquidPatientItemQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "DietLiquidPatientItemQuery";
        }


    }


    [Serializable]
    public partial class DietLiquidPatientItemMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected DietLiquidPatientItemMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(DietLiquidPatientItemMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = DietLiquidPatientItemMetadata.PropertyNames.TransactionNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(DietLiquidPatientItemMetadata.ColumnNames.DietTime, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = DietLiquidPatientItemMetadata.PropertyNames.DietTime;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 5;
            _columns.Add(c);

            c = new esColumnMetadata(DietLiquidPatientItemMetadata.ColumnNames.ItemID, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = DietLiquidPatientItemMetadata.PropertyNames.ItemID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(DietLiquidPatientItemMetadata.ColumnNames.Qty, 3, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = DietLiquidPatientItemMetadata.PropertyNames.Qty;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(DietLiquidPatientItemMetadata.ColumnNames.SRItemUnit, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = DietLiquidPatientItemMetadata.PropertyNames.SRItemUnit;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(DietLiquidPatientItemMetadata.ColumnNames.Notes, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = DietLiquidPatientItemMetadata.PropertyNames.Notes;
            c.CharacterMaxLength = 250;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(DietLiquidPatientItemMetadata.ColumnNames.FoodID, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = DietLiquidPatientItemMetadata.PropertyNames.FoodID;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(DietLiquidPatientItemMetadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = DietLiquidPatientItemMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(DietLiquidPatientItemMetadata.ColumnNames.LastUpdateByUserID, 8, typeof(System.String), esSystemType.String);
            c.PropertyName = DietLiquidPatientItemMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public DietLiquidPatientItemMetadata Meta()
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
            public const string DietTime = "DietTime";
            public const string ItemID = "ItemID";
            public const string Qty = "Qty";
            public const string SRItemUnit = "SRItemUnit";
            public const string Notes = "Notes";
            public const string FoodID = "FoodID";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string TransactionNo = "TransactionNo";
            public const string DietTime = "DietTime";
            public const string ItemID = "ItemID";
            public const string Qty = "Qty";
            public const string SRItemUnit = "SRItemUnit";
            public const string Notes = "Notes";
            public const string FoodID = "FoodID";
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
            lock (typeof(DietLiquidPatientItemMetadata))
            {
                if (DietLiquidPatientItemMetadata.mapDelegates == null)
                {
                    DietLiquidPatientItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (DietLiquidPatientItemMetadata.meta == null)
                {
                    DietLiquidPatientItemMetadata.meta = new DietLiquidPatientItemMetadata();
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
                meta.AddTypeMap("DietTime", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Qty", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("SRItemUnit", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("FoodID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));



                meta.Source = "DietLiquidPatientItem";
                meta.Destination = "DietLiquidPatientItem";

                meta.spInsert = "proc_DietLiquidPatientItemInsert";
                meta.spUpdate = "proc_DietLiquidPatientItemUpdate";
                meta.spDelete = "proc_DietLiquidPatientItemDelete";
                meta.spLoadAll = "proc_DietLiquidPatientItemLoadAll";
                meta.spLoadByPrimaryKey = "proc_DietLiquidPatientItemLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private DietLiquidPatientItemMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
