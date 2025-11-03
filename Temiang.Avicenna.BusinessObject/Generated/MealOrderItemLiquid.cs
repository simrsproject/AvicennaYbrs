/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/29/2015 10:32:25 AM
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
    abstract public class esMealOrderItemLiquidCollection : esEntityCollectionWAuditLog
    {
        public esMealOrderItemLiquidCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "MealOrderItemLiquidCollection";
        }

        #region Query Logic
        protected void InitQuery(esMealOrderItemLiquidQuery query)
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
            this.InitQuery(query as esMealOrderItemLiquidQuery);
        }
        #endregion

        virtual public MealOrderItemLiquid DetachEntity(MealOrderItemLiquid entity)
        {
            return base.DetachEntity(entity) as MealOrderItemLiquid;
        }

        virtual public MealOrderItemLiquid AttachEntity(MealOrderItemLiquid entity)
        {
            return base.AttachEntity(entity) as MealOrderItemLiquid;
        }

        virtual public void Combine(MealOrderItemLiquidCollection collection)
        {
            base.Combine(collection);
        }

        new public MealOrderItemLiquid this[int index]
        {
            get
            {
                return base[index] as MealOrderItemLiquid;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(MealOrderItemLiquid);
        }
    }



    [Serializable]
    abstract public class esMealOrderItemLiquid : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esMealOrderItemLiquidQuery GetDynamicQuery()
        {
            return null;
        }

        public esMealOrderItemLiquid()
        {

        }

        public esMealOrderItemLiquid(DataRow row)
            : base(row)
        {

        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(System.String mealTime, System.String orderNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(mealTime, orderNo);
            else
                return LoadByPrimaryKeyStoredProcedure(mealTime, orderNo);
        }

        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String mealTime, System.String orderNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(mealTime, orderNo);
            else
                return LoadByPrimaryKeyStoredProcedure(mealTime, orderNo);
        }

        private bool LoadByPrimaryKeyDynamic(System.String mealTime, System.String orderNo)
        {
            esMealOrderItemLiquidQuery query = this.GetDynamicQuery();
            query.Where(query.MealTime == mealTime, query.OrderNo == orderNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(System.String mealTime, System.String orderNo)
        {
            esParameters parms = new esParameters();
            parms.Add("MealTime", mealTime); parms.Add("OrderNo", orderNo);
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
                        case "OrderNo": this.str.OrderNo = (string)value; break;
                        case "MealTime": this.str.MealTime = (string)value; break;
                        case "FoodID": this.str.FoodID = (string)value; break;
                        case "DietLiquidTransNo": this.str.DietLiquidTransNo = (string)value; break;
                        case "IsDistributed": this.str.IsDistributed = (string)value; break;
                        case "IsVoidDistributed": this.str.IsVoidDistributed = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "IsDistributed":

                            if (value == null || value is System.Boolean)
                                this.IsDistributed = (System.Boolean?)value;
                            break;

                        case "IsVoidDistributed":

                            if (value == null || value is System.Boolean)
                                this.IsVoidDistributed = (System.Boolean?)value;
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
        /// Maps to MealOrderItemLiquid.OrderNo
        /// </summary>
        virtual public System.String OrderNo
        {
            get
            {
                return base.GetSystemString(MealOrderItemLiquidMetadata.ColumnNames.OrderNo);
            }

            set
            {
                base.SetSystemString(MealOrderItemLiquidMetadata.ColumnNames.OrderNo, value);
            }
        }

        /// <summary>
        /// Maps to MealOrderItemLiquid.MealTime
        /// </summary>
        virtual public System.String MealTime
        {
            get
            {
                return base.GetSystemString(MealOrderItemLiquidMetadata.ColumnNames.MealTime);
            }

            set
            {
                base.SetSystemString(MealOrderItemLiquidMetadata.ColumnNames.MealTime, value);
            }
        }

        /// <summary>
        /// Maps to MealOrderItemLiquid.FoodID
        /// </summary>
        virtual public System.String FoodID
        {
            get
            {
                return base.GetSystemString(MealOrderItemLiquidMetadata.ColumnNames.FoodID);
            }

            set
            {
                base.SetSystemString(MealOrderItemLiquidMetadata.ColumnNames.FoodID, value);
            }
        }

        /// <summary>
        /// Maps to MealOrderItemLiquid.DietLiquidTransNo
        /// </summary>
        virtual public System.String DietLiquidTransNo
        {
            get
            {
                return base.GetSystemString(MealOrderItemLiquidMetadata.ColumnNames.DietLiquidTransNo);
            }

            set
            {
                base.SetSystemString(MealOrderItemLiquidMetadata.ColumnNames.DietLiquidTransNo, value);
            }
        }

        /// <summary>
        /// Maps to MealOrderItemLiquid.IsDistributed
        /// </summary>
        virtual public System.Boolean? IsDistributed
        {
            get
            {
                return base.GetSystemBoolean(MealOrderItemLiquidMetadata.ColumnNames.IsDistributed);
            }

            set
            {
                base.SetSystemBoolean(MealOrderItemLiquidMetadata.ColumnNames.IsDistributed, value);
            }
        }

        /// <summary>
        /// Maps to MealOrderItemLiquid.IsVoidDistributed
        /// </summary>
        virtual public System.Boolean? IsVoidDistributed
        {
            get
            {
                return base.GetSystemBoolean(MealOrderItemLiquidMetadata.ColumnNames.IsVoidDistributed);
            }

            set
            {
                base.SetSystemBoolean(MealOrderItemLiquidMetadata.ColumnNames.IsVoidDistributed, value);
            }
        }

        /// <summary>
        /// Maps to MealOrderItemLiquid.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(MealOrderItemLiquidMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(MealOrderItemLiquidMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to MealOrderItemLiquid.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(MealOrderItemLiquidMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(MealOrderItemLiquidMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esMealOrderItemLiquid entity)
            {
                this.entity = entity;
            }


            public System.String OrderNo
            {
                get
                {
                    System.String data = entity.OrderNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.OrderNo = null;
                    else entity.OrderNo = Convert.ToString(value);
                }
            }

            public System.String MealTime
            {
                get
                {
                    System.String data = entity.MealTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.MealTime = null;
                    else entity.MealTime = Convert.ToString(value);
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

            public System.String DietLiquidTransNo
            {
                get
                {
                    System.String data = entity.DietLiquidTransNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DietLiquidTransNo = null;
                    else entity.DietLiquidTransNo = Convert.ToString(value);
                }
            }

            public System.String IsDistributed
            {
                get
                {
                    System.Boolean? data = entity.IsDistributed;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsDistributed = null;
                    else entity.IsDistributed = Convert.ToBoolean(value);
                }
            }

            public System.String IsVoidDistributed
            {
                get
                {
                    System.Boolean? data = entity.IsVoidDistributed;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsVoidDistributed = null;
                    else entity.IsVoidDistributed = Convert.ToBoolean(value);
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


            private esMealOrderItemLiquid entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esMealOrderItemLiquidQuery query)
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
                throw new Exception("esMealOrderItemLiquid can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    public partial class MealOrderItemLiquid : esMealOrderItemLiquid
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
    abstract public class esMealOrderItemLiquidQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return MealOrderItemLiquidMetadata.Meta();
            }
        }


        public esQueryItem OrderNo
        {
            get
            {
                return new esQueryItem(this, MealOrderItemLiquidMetadata.ColumnNames.OrderNo, esSystemType.String);
            }
        }

        public esQueryItem MealTime
        {
            get
            {
                return new esQueryItem(this, MealOrderItemLiquidMetadata.ColumnNames.MealTime, esSystemType.String);
            }
        }

        public esQueryItem FoodID
        {
            get
            {
                return new esQueryItem(this, MealOrderItemLiquidMetadata.ColumnNames.FoodID, esSystemType.String);
            }
        }

        public esQueryItem DietLiquidTransNo
        {
            get
            {
                return new esQueryItem(this, MealOrderItemLiquidMetadata.ColumnNames.DietLiquidTransNo, esSystemType.String);
            }
        }

        public esQueryItem IsDistributed
        {
            get
            {
                return new esQueryItem(this, MealOrderItemLiquidMetadata.ColumnNames.IsDistributed, esSystemType.Boolean);
            }
        }

        public esQueryItem IsVoidDistributed
        {
            get
            {
                return new esQueryItem(this, MealOrderItemLiquidMetadata.ColumnNames.IsVoidDistributed, esSystemType.Boolean);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, MealOrderItemLiquidMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, MealOrderItemLiquidMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("MealOrderItemLiquidCollection")]
    public partial class MealOrderItemLiquidCollection : esMealOrderItemLiquidCollection, IEnumerable<MealOrderItemLiquid>
    {
        public MealOrderItemLiquidCollection()
        {

        }

        public static implicit operator List<MealOrderItemLiquid>(MealOrderItemLiquidCollection coll)
        {
            List<MealOrderItemLiquid> list = new List<MealOrderItemLiquid>();

            foreach (MealOrderItemLiquid emp in coll)
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
                return MealOrderItemLiquidMetadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new MealOrderItemLiquidQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new MealOrderItemLiquid(row);
        }

        override protected esEntity CreateEntity()
        {
            return new MealOrderItemLiquid();
        }


        #endregion


        [BrowsableAttribute(false)]
        public MealOrderItemLiquidQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new MealOrderItemLiquidQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(MealOrderItemLiquidQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public MealOrderItemLiquid AddNew()
        {
            MealOrderItemLiquid entity = base.AddNewEntity() as MealOrderItemLiquid;

            return entity;
        }

        public MealOrderItemLiquid FindByPrimaryKey(System.String mealTime, System.String orderNo)
        {
            return base.FindByPrimaryKey(mealTime, orderNo) as MealOrderItemLiquid;
        }


        #region IEnumerable<MealOrderItemLiquid> Members

        IEnumerator<MealOrderItemLiquid> IEnumerable<MealOrderItemLiquid>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as MealOrderItemLiquid;
            }
        }

        #endregion

        private MealOrderItemLiquidQuery query;
    }


    /// <summary>
    /// Encapsulates the 'MealOrderItemLiquid' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("MealOrderItemLiquid ({OrderNo},{MealTime})")]
    [Serializable]
    public partial class MealOrderItemLiquid : esMealOrderItemLiquid
    {
        public MealOrderItemLiquid()
        {

        }

        public MealOrderItemLiquid(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return MealOrderItemLiquidMetadata.Meta();
            }
        }



        override protected esMealOrderItemLiquidQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new MealOrderItemLiquidQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public MealOrderItemLiquidQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new MealOrderItemLiquidQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(MealOrderItemLiquidQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private MealOrderItemLiquidQuery query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class MealOrderItemLiquidQuery : esMealOrderItemLiquidQuery
    {
        public MealOrderItemLiquidQuery()
        {

        }

        public MealOrderItemLiquidQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "MealOrderItemLiquidQuery";
        }


    }


    [Serializable]
    public partial class MealOrderItemLiquidMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected MealOrderItemLiquidMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(MealOrderItemLiquidMetadata.ColumnNames.OrderNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = MealOrderItemLiquidMetadata.PropertyNames.OrderNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(MealOrderItemLiquidMetadata.ColumnNames.MealTime, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = MealOrderItemLiquidMetadata.PropertyNames.MealTime;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 5;
            _columns.Add(c);

            c = new esColumnMetadata(MealOrderItemLiquidMetadata.ColumnNames.FoodID, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = MealOrderItemLiquidMetadata.PropertyNames.FoodID;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(MealOrderItemLiquidMetadata.ColumnNames.DietLiquidTransNo, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = MealOrderItemLiquidMetadata.PropertyNames.DietLiquidTransNo;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MealOrderItemLiquidMetadata.ColumnNames.IsDistributed, 4, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = MealOrderItemLiquidMetadata.PropertyNames.IsDistributed;
            c.HasDefault = true;
            c.Default = @"((0))";
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MealOrderItemLiquidMetadata.ColumnNames.IsVoidDistributed, 5, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = MealOrderItemLiquidMetadata.PropertyNames.IsVoidDistributed;
            c.HasDefault = true;
            c.Default = @"((0))";
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MealOrderItemLiquidMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = MealOrderItemLiquidMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MealOrderItemLiquidMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = MealOrderItemLiquidMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public MealOrderItemLiquidMetadata Meta()
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
            public const string OrderNo = "OrderNo";
            public const string MealTime = "MealTime";
            public const string FoodID = "FoodID";
            public const string DietLiquidTransNo = "DietLiquidTransNo";
            public const string IsDistributed = "IsDistributed";
            public const string IsVoidDistributed = "IsVoidDistributed";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string OrderNo = "OrderNo";
            public const string MealTime = "MealTime";
            public const string FoodID = "FoodID";
            public const string DietLiquidTransNo = "DietLiquidTransNo";
            public const string IsDistributed = "IsDistributed";
            public const string IsVoidDistributed = "IsVoidDistributed";
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
            lock (typeof(MealOrderItemLiquidMetadata))
            {
                if (MealOrderItemLiquidMetadata.mapDelegates == null)
                {
                    MealOrderItemLiquidMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (MealOrderItemLiquidMetadata.meta == null)
                {
                    MealOrderItemLiquidMetadata.meta = new MealOrderItemLiquidMetadata();
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


                meta.AddTypeMap("OrderNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("MealTime", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("FoodID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("DietLiquidTransNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsDistributed", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsVoidDistributed", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));



                meta.Source = "MealOrderItemLiquid";
                meta.Destination = "MealOrderItemLiquid";

                meta.spInsert = "proc_MealOrderItemLiquidInsert";
                meta.spUpdate = "proc_MealOrderItemLiquidUpdate";
                meta.spDelete = "proc_MealOrderItemLiquidDelete";
                meta.spLoadAll = "proc_MealOrderItemLiquidLoadAll";
                meta.spLoadByPrimaryKey = "proc_MealOrderItemLiquidLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private MealOrderItemLiquidMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
