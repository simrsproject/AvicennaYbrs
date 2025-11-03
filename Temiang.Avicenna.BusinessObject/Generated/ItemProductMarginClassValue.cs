/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 11/29/2017 4:11:18 PM
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
    abstract public class esItemProductMarginClassValueCollection : esEntityCollectionWAuditLog
    {
        public esItemProductMarginClassValueCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "ItemProductMarginClassValueCollection";
        }

        #region Query Logic
        protected void InitQuery(esItemProductMarginClassValueQuery query)
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
            this.InitQuery(query as esItemProductMarginClassValueQuery);
        }
        #endregion

        virtual public ItemProductMarginClassValue DetachEntity(ItemProductMarginClassValue entity)
        {
            return base.DetachEntity(entity) as ItemProductMarginClassValue;
        }

        virtual public ItemProductMarginClassValue AttachEntity(ItemProductMarginClassValue entity)
        {
            return base.AttachEntity(entity) as ItemProductMarginClassValue;
        }

        virtual public void Combine(ItemProductMarginClassValueCollection collection)
        {
            base.Combine(collection);
        }

        new public ItemProductMarginClassValue this[int index]
        {
            get
            {
                return base[index] as ItemProductMarginClassValue;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(ItemProductMarginClassValue);
        }
    }



    [Serializable]
    abstract public class esItemProductMarginClassValue : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esItemProductMarginClassValueQuery GetDynamicQuery()
        {
            return null;
        }

        public esItemProductMarginClassValue()
        {

        }

        public esItemProductMarginClassValue(DataRow row)
            : base(row)
        {

        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(System.String classID, System.String marginID, System.String sequenceNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(classID, marginID, sequenceNo);
            else
                return LoadByPrimaryKeyStoredProcedure(classID, marginID, sequenceNo);
        }

        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String classID, System.String marginID, System.String sequenceNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(classID, marginID, sequenceNo);
            else
                return LoadByPrimaryKeyStoredProcedure(classID, marginID, sequenceNo);
        }

        private bool LoadByPrimaryKeyDynamic(System.String classID, System.String marginID, System.String sequenceNo)
        {
            esItemProductMarginClassValueQuery query = this.GetDynamicQuery();
            query.Where(query.ClassID == classID, query.MarginID == marginID, query.SequenceNo == sequenceNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(System.String classID, System.String marginID, System.String sequenceNo)
        {
            esParameters parms = new esParameters();
            parms.Add("ClassID", classID); parms.Add("MarginID", marginID); parms.Add("SequenceNo", sequenceNo);
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
                        case "MarginID": this.str.MarginID = (string)value; break;
                        case "SequenceNo": this.str.SequenceNo = (string)value; break;
                        case "ClassID": this.str.ClassID = (string)value; break;
                        case "MarginValuePercentage": this.str.MarginValuePercentage = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "MarginValuePercentage":

                            if (value == null || value is System.Decimal)
                                this.MarginValuePercentage = (System.Decimal?)value;
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
        /// Maps to ItemProductMarginClassValue.MarginID
        /// </summary>
        virtual public System.String MarginID
        {
            get
            {
                return base.GetSystemString(ItemProductMarginClassValueMetadata.ColumnNames.MarginID);
            }

            set
            {
                base.SetSystemString(ItemProductMarginClassValueMetadata.ColumnNames.MarginID, value);
            }
        }

        /// <summary>
        /// Maps to ItemProductMarginClassValue.SequenceNo
        /// </summary>
        virtual public System.String SequenceNo
        {
            get
            {
                return base.GetSystemString(ItemProductMarginClassValueMetadata.ColumnNames.SequenceNo);
            }

            set
            {
                base.SetSystemString(ItemProductMarginClassValueMetadata.ColumnNames.SequenceNo, value);
            }
        }

        /// <summary>
        /// Maps to ItemProductMarginClassValue.ClassID
        /// </summary>
        virtual public System.String ClassID
        {
            get
            {
                return base.GetSystemString(ItemProductMarginClassValueMetadata.ColumnNames.ClassID);
            }

            set
            {
                base.SetSystemString(ItemProductMarginClassValueMetadata.ColumnNames.ClassID, value);
            }
        }

        /// <summary>
        /// Maps to ItemProductMarginClassValue.MarginValuePercentage
        /// </summary>
        virtual public System.Decimal? MarginValuePercentage
        {
            get
            {
                return base.GetSystemDecimal(ItemProductMarginClassValueMetadata.ColumnNames.MarginValuePercentage);
            }

            set
            {
                base.SetSystemDecimal(ItemProductMarginClassValueMetadata.ColumnNames.MarginValuePercentage, value);
            }
        }

        /// <summary>
        /// Maps to ItemProductMarginClassValue.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(ItemProductMarginClassValueMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(ItemProductMarginClassValueMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to ItemProductMarginClassValue.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(ItemProductMarginClassValueMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(ItemProductMarginClassValueMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esItemProductMarginClassValue entity)
            {
                this.entity = entity;
            }


            public System.String MarginID
            {
                get
                {
                    System.String data = entity.MarginID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.MarginID = null;
                    else entity.MarginID = Convert.ToString(value);
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

            public System.String ClassID
            {
                get
                {
                    System.String data = entity.ClassID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ClassID = null;
                    else entity.ClassID = Convert.ToString(value);
                }
            }

            public System.String MarginValuePercentage
            {
                get
                {
                    System.Decimal? data = entity.MarginValuePercentage;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.MarginValuePercentage = null;
                    else entity.MarginValuePercentage = Convert.ToDecimal(value);
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


            private esItemProductMarginClassValue entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esItemProductMarginClassValueQuery query)
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
                throw new Exception("esItemProductMarginClassValue can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    public partial class ItemProductMarginClassValue : esItemProductMarginClassValue
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
    abstract public class esItemProductMarginClassValueQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return ItemProductMarginClassValueMetadata.Meta();
            }
        }


        public esQueryItem MarginID
        {
            get
            {
                return new esQueryItem(this, ItemProductMarginClassValueMetadata.ColumnNames.MarginID, esSystemType.String);
            }
        }

        public esQueryItem SequenceNo
        {
            get
            {
                return new esQueryItem(this, ItemProductMarginClassValueMetadata.ColumnNames.SequenceNo, esSystemType.String);
            }
        }

        public esQueryItem ClassID
        {
            get
            {
                return new esQueryItem(this, ItemProductMarginClassValueMetadata.ColumnNames.ClassID, esSystemType.String);
            }
        }

        public esQueryItem MarginValuePercentage
        {
            get
            {
                return new esQueryItem(this, ItemProductMarginClassValueMetadata.ColumnNames.MarginValuePercentage, esSystemType.Decimal);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, ItemProductMarginClassValueMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, ItemProductMarginClassValueMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("ItemProductMarginClassValueCollection")]
    public partial class ItemProductMarginClassValueCollection : esItemProductMarginClassValueCollection, IEnumerable<ItemProductMarginClassValue>
    {
        public ItemProductMarginClassValueCollection()
        {

        }

        public static implicit operator List<ItemProductMarginClassValue>(ItemProductMarginClassValueCollection coll)
        {
            List<ItemProductMarginClassValue> list = new List<ItemProductMarginClassValue>();

            foreach (ItemProductMarginClassValue emp in coll)
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
                return ItemProductMarginClassValueMetadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ItemProductMarginClassValueQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new ItemProductMarginClassValue(row);
        }

        override protected esEntity CreateEntity()
        {
            return new ItemProductMarginClassValue();
        }


        #endregion


        [BrowsableAttribute(false)]
        public ItemProductMarginClassValueQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ItemProductMarginClassValueQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(ItemProductMarginClassValueQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public ItemProductMarginClassValue AddNew()
        {
            ItemProductMarginClassValue entity = base.AddNewEntity() as ItemProductMarginClassValue;

            return entity;
        }

        public ItemProductMarginClassValue FindByPrimaryKey(System.String classID, System.String marginID, System.String sequenceNo)
        {
            return base.FindByPrimaryKey(classID, marginID, sequenceNo) as ItemProductMarginClassValue;
        }


        #region IEnumerable<ItemProductMarginClassValue> Members

        IEnumerator<ItemProductMarginClassValue> IEnumerable<ItemProductMarginClassValue>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as ItemProductMarginClassValue;
            }
        }

        #endregion

        private ItemProductMarginClassValueQuery query;
    }


    /// <summary>
    /// Encapsulates the 'ItemProductMarginClassValue' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("ItemProductMarginClassValue ({MarginID},{SequenceNo},{ClassID})")]
    [Serializable]
    public partial class ItemProductMarginClassValue : esItemProductMarginClassValue
    {
        public ItemProductMarginClassValue()
        {

        }

        public ItemProductMarginClassValue(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return ItemProductMarginClassValueMetadata.Meta();
            }
        }



        override protected esItemProductMarginClassValueQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ItemProductMarginClassValueQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public ItemProductMarginClassValueQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ItemProductMarginClassValueQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(ItemProductMarginClassValueQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private ItemProductMarginClassValueQuery query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class ItemProductMarginClassValueQuery : esItemProductMarginClassValueQuery
    {
        public ItemProductMarginClassValueQuery()
        {

        }

        public ItemProductMarginClassValueQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "ItemProductMarginClassValueQuery";
        }


    }


    [Serializable]
    public partial class ItemProductMarginClassValueMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected ItemProductMarginClassValueMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(ItemProductMarginClassValueMetadata.ColumnNames.MarginID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemProductMarginClassValueMetadata.PropertyNames.MarginID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(ItemProductMarginClassValueMetadata.ColumnNames.SequenceNo, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemProductMarginClassValueMetadata.PropertyNames.SequenceNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 3;
            c.HasDefault = true;
            c.Default = @"('000')";
            _columns.Add(c);

            c = new esColumnMetadata(ItemProductMarginClassValueMetadata.ColumnNames.ClassID, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemProductMarginClassValueMetadata.PropertyNames.ClassID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(ItemProductMarginClassValueMetadata.ColumnNames.MarginValuePercentage, 3, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ItemProductMarginClassValueMetadata.PropertyNames.MarginValuePercentage;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(ItemProductMarginClassValueMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ItemProductMarginClassValueMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemProductMarginClassValueMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemProductMarginClassValueMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 40;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public ItemProductMarginClassValueMetadata Meta()
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
            public const string MarginID = "MarginID";
            public const string SequenceNo = "SequenceNo";
            public const string ClassID = "ClassID";
            public const string MarginValuePercentage = "MarginValuePercentage";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string MarginID = "MarginID";
            public const string SequenceNo = "SequenceNo";
            public const string ClassID = "ClassID";
            public const string MarginValuePercentage = "MarginValuePercentage";
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
            lock (typeof(ItemProductMarginClassValueMetadata))
            {
                if (ItemProductMarginClassValueMetadata.mapDelegates == null)
                {
                    ItemProductMarginClassValueMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (ItemProductMarginClassValueMetadata.meta == null)
                {
                    ItemProductMarginClassValueMetadata.meta = new ItemProductMarginClassValueMetadata();
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


                meta.AddTypeMap("MarginID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SequenceNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ClassID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("MarginValuePercentage", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));



                meta.Source = "ItemProductMarginClassValue";
                meta.Destination = "ItemProductMarginClassValue";

                meta.spInsert = "proc_ItemProductMarginClassValueInsert";
                meta.spUpdate = "proc_ItemProductMarginClassValueUpdate";
                meta.spDelete = "proc_ItemProductMarginClassValueDelete";
                meta.spLoadAll = "proc_ItemProductMarginClassValueLoadAll";
                meta.spLoadByPrimaryKey = "proc_ItemProductMarginClassValueLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private ItemProductMarginClassValueMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
