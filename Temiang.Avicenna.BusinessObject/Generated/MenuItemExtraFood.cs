/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 3/7/2015 3:42:03 PM
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
    abstract public class esMenuItemExtraFoodCollection : esEntityCollectionWAuditLog
    {
        public esMenuItemExtraFoodCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "MenuItemExtraFoodCollection";
        }

        #region Query Logic
        protected void InitQuery(esMenuItemExtraFoodQuery query)
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
            this.InitQuery(query as esMenuItemExtraFoodQuery);
        }
        #endregion

        virtual public MenuItemExtraFood DetachEntity(MenuItemExtraFood entity)
        {
            return base.DetachEntity(entity) as MenuItemExtraFood;
        }

        virtual public MenuItemExtraFood AttachEntity(MenuItemExtraFood entity)
        {
            return base.AttachEntity(entity) as MenuItemExtraFood;
        }

        virtual public void Combine(MenuItemExtraFoodCollection collection)
        {
            base.Combine(collection);
        }

        new public MenuItemExtraFood this[int index]
        {
            get
            {
                return base[index] as MenuItemExtraFood;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(MenuItemExtraFood);
        }
    }



    [Serializable]
    abstract public class esMenuItemExtraFood : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esMenuItemExtraFoodQuery GetDynamicQuery()
        {
            return null;
        }

        public esMenuItemExtraFood()
        {

        }

        public esMenuItemExtraFood(DataRow row)
            : base(row)
        {

        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(System.String seqNo, System.String sRDayName, System.String foodID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(seqNo, sRDayName, foodID);
            else
                return LoadByPrimaryKeyStoredProcedure(seqNo, sRDayName, foodID);
        }

        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String seqNo, System.String sRDayName, System.String foodID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(seqNo, sRDayName, foodID);
            else
                return LoadByPrimaryKeyStoredProcedure(seqNo, sRDayName, foodID);
        }

        private bool LoadByPrimaryKeyDynamic(System.String seqNo, System.String sRDayName, System.String foodID)
        {
            esMenuItemExtraFoodQuery query = this.GetDynamicQuery();
            query.Where(query.SeqNo == seqNo, query.SRDayName == sRDayName, query.FoodID == foodID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(System.String seqNo, System.String sRDayName, System.String foodID)
        {
            esParameters parms = new esParameters();
            parms.Add("SeqNo", seqNo); parms.Add("SRDayName", sRDayName); parms.Add("FoodID", foodID);
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
                        case "SeqNo": this.str.SeqNo = (string)value; break;
                        case "SRDayName": this.str.SRDayName = (string)value; break;
                        case "FoodID": this.str.FoodID = (string)value; break;
                        case "IsActive": this.str.IsActive = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
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
        /// Maps to MenuItemExtraFood.SeqNo
        /// </summary>
        virtual public System.String SeqNo
        {
            get
            {
                return base.GetSystemString(MenuItemExtraFoodMetadata.ColumnNames.SeqNo);
            }

            set
            {
                base.SetSystemString(MenuItemExtraFoodMetadata.ColumnNames.SeqNo, value);
            }
        }

        /// <summary>
        /// Maps to MenuItemExtraFood.SRDayName
        /// </summary>
        virtual public System.String SRDayName
        {
            get
            {
                return base.GetSystemString(MenuItemExtraFoodMetadata.ColumnNames.SRDayName);
            }

            set
            {
                base.SetSystemString(MenuItemExtraFoodMetadata.ColumnNames.SRDayName, value);
            }
        }

        /// <summary>
        /// Maps to MenuItemExtraFood.FoodID
        /// </summary>
        virtual public System.String FoodID
        {
            get
            {
                return base.GetSystemString(MenuItemExtraFoodMetadata.ColumnNames.FoodID);
            }

            set
            {
                base.SetSystemString(MenuItemExtraFoodMetadata.ColumnNames.FoodID, value);
            }
        }

        /// <summary>
        /// Maps to MenuItemExtraFood.IsActive
        /// </summary>
        virtual public System.String IsActive
        {
            get
            {
                return base.GetSystemString(MenuItemExtraFoodMetadata.ColumnNames.IsActive);
            }

            set
            {
                base.SetSystemString(MenuItemExtraFoodMetadata.ColumnNames.IsActive, value);
            }
        }

        /// <summary>
        /// Maps to MenuItemExtraFood.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(MenuItemExtraFoodMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(MenuItemExtraFoodMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to MenuItemExtraFood.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(MenuItemExtraFoodMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(MenuItemExtraFoodMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esMenuItemExtraFood entity)
            {
                this.entity = entity;
            }


            public System.String SeqNo
            {
                get
                {
                    System.String data = entity.SeqNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SeqNo = null;
                    else entity.SeqNo = Convert.ToString(value);
                }
            }

            public System.String SRDayName
            {
                get
                {
                    System.String data = entity.SRDayName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRDayName = null;
                    else entity.SRDayName = Convert.ToString(value);
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

            public System.String IsActive
            {
                get
                {
                    System.String data = entity.IsActive;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsActive = null;
                    else entity.IsActive = Convert.ToString(value);
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


            private esMenuItemExtraFood entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esMenuItemExtraFoodQuery query)
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
                throw new Exception("esMenuItemExtraFood can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    public partial class MenuItemExtraFood : esMenuItemExtraFood
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
    abstract public class esMenuItemExtraFoodQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return MenuItemExtraFoodMetadata.Meta();
            }
        }


        public esQueryItem SeqNo
        {
            get
            {
                return new esQueryItem(this, MenuItemExtraFoodMetadata.ColumnNames.SeqNo, esSystemType.String);
            }
        }

        public esQueryItem SRDayName
        {
            get
            {
                return new esQueryItem(this, MenuItemExtraFoodMetadata.ColumnNames.SRDayName, esSystemType.String);
            }
        }

        public esQueryItem FoodID
        {
            get
            {
                return new esQueryItem(this, MenuItemExtraFoodMetadata.ColumnNames.FoodID, esSystemType.String);
            }
        }

        public esQueryItem IsActive
        {
            get
            {
                return new esQueryItem(this, MenuItemExtraFoodMetadata.ColumnNames.IsActive, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, MenuItemExtraFoodMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, MenuItemExtraFoodMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("MenuItemExtraFoodCollection")]
    public partial class MenuItemExtraFoodCollection : esMenuItemExtraFoodCollection, IEnumerable<MenuItemExtraFood>
    {
        public MenuItemExtraFoodCollection()
        {

        }

        public static implicit operator List<MenuItemExtraFood>(MenuItemExtraFoodCollection coll)
        {
            List<MenuItemExtraFood> list = new List<MenuItemExtraFood>();

            foreach (MenuItemExtraFood emp in coll)
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
                return MenuItemExtraFoodMetadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new MenuItemExtraFoodQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new MenuItemExtraFood(row);
        }

        override protected esEntity CreateEntity()
        {
            return new MenuItemExtraFood();
        }


        #endregion


        [BrowsableAttribute(false)]
        public MenuItemExtraFoodQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new MenuItemExtraFoodQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(MenuItemExtraFoodQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public MenuItemExtraFood AddNew()
        {
            MenuItemExtraFood entity = base.AddNewEntity() as MenuItemExtraFood;

            return entity;
        }

        public MenuItemExtraFood FindByPrimaryKey(System.String seqNo, System.String sRDayName, System.String foodID)
        {
            return base.FindByPrimaryKey(seqNo, sRDayName, foodID) as MenuItemExtraFood;
        }


        #region IEnumerable<MenuItemExtraFood> Members

        IEnumerator<MenuItemExtraFood> IEnumerable<MenuItemExtraFood>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as MenuItemExtraFood;
            }
        }

        #endregion

        private MenuItemExtraFoodQuery query;
    }


    /// <summary>
    /// Encapsulates the 'MenuItemExtraFood' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("MenuItemExtraFood ({SeqNo},{SRDayName},{FoodID})")]
    [Serializable]
    public partial class MenuItemExtraFood : esMenuItemExtraFood
    {
        public MenuItemExtraFood()
        {

        }

        public MenuItemExtraFood(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return MenuItemExtraFoodMetadata.Meta();
            }
        }



        override protected esMenuItemExtraFoodQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new MenuItemExtraFoodQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public MenuItemExtraFoodQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new MenuItemExtraFoodQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(MenuItemExtraFoodQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private MenuItemExtraFoodQuery query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class MenuItemExtraFoodQuery : esMenuItemExtraFoodQuery
    {
        public MenuItemExtraFoodQuery()
        {

        }

        public MenuItemExtraFoodQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "MenuItemExtraFoodQuery";
        }


    }


    [Serializable]
    public partial class MenuItemExtraFoodMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected MenuItemExtraFoodMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(MenuItemExtraFoodMetadata.ColumnNames.SeqNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = MenuItemExtraFoodMetadata.PropertyNames.SeqNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 5;
            _columns.Add(c);

            c = new esColumnMetadata(MenuItemExtraFoodMetadata.ColumnNames.SRDayName, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = MenuItemExtraFoodMetadata.PropertyNames.SRDayName;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(MenuItemExtraFoodMetadata.ColumnNames.FoodID, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = MenuItemExtraFoodMetadata.PropertyNames.FoodID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(MenuItemExtraFoodMetadata.ColumnNames.IsActive, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = MenuItemExtraFoodMetadata.PropertyNames.IsActive;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MenuItemExtraFoodMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = MenuItemExtraFoodMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MenuItemExtraFoodMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = MenuItemExtraFoodMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public MenuItemExtraFoodMetadata Meta()
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
            public const string SeqNo = "SeqNo";
            public const string SRDayName = "SRDayName";
            public const string FoodID = "FoodID";
            public const string IsActive = "IsActive";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string SeqNo = "SeqNo";
            public const string SRDayName = "SRDayName";
            public const string FoodID = "FoodID";
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
            lock (typeof(MenuItemExtraFoodMetadata))
            {
                if (MenuItemExtraFoodMetadata.mapDelegates == null)
                {
                    MenuItemExtraFoodMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (MenuItemExtraFoodMetadata.meta == null)
                {
                    MenuItemExtraFoodMetadata.meta = new MenuItemExtraFoodMetadata();
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


                meta.AddTypeMap("SeqNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRDayName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("FoodID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsActive", new esTypeMap("nchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));



                meta.Source = "MenuItemExtraFood";
                meta.Destination = "MenuItemExtraFood";

                meta.spInsert = "proc_MenuItemExtraFoodInsert";
                meta.spUpdate = "proc_MenuItemExtraFoodUpdate";
                meta.spDelete = "proc_MenuItemExtraFoodDelete";
                meta.spLoadAll = "proc_MenuItemExtraFoodLoadAll";
                meta.spLoadByPrimaryKey = "proc_MenuItemExtraFoodLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private MenuItemExtraFoodMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
