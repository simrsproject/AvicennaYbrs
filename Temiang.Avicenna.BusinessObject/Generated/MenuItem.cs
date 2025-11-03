/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 2/13/2014 1:46:15 PM
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
    abstract public class esMenuItemCollection : esEntityCollectionWAuditLog
    {
        public esMenuItemCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "MenuItemCollection";
        }

        #region Query Logic
        protected void InitQuery(esMenuItemQuery query)
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
            this.InitQuery(query as esMenuItemQuery);
        }
        #endregion

        virtual public MenuItem DetachEntity(MenuItem entity)
        {
            return base.DetachEntity(entity) as MenuItem;
        }

        virtual public MenuItem AttachEntity(MenuItem entity)
        {
            return base.AttachEntity(entity) as MenuItem;
        }

        virtual public void Combine(MenuItemCollection collection)
        {
            base.Combine(collection);
        }

        new public MenuItem this[int index]
        {
            get
            {
                return base[index] as MenuItem;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(MenuItem);
        }
    }



    [Serializable]
    abstract public class esMenuItem : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esMenuItemQuery GetDynamicQuery()
        {
            return null;
        }

        public esMenuItem()
        {

        }

        public esMenuItem(DataRow row)
            : base(row)
        {

        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(System.String menuItemID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(menuItemID);
            else
                return LoadByPrimaryKeyStoredProcedure(menuItemID);
        }

        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String menuItemID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(menuItemID);
            else
                return LoadByPrimaryKeyStoredProcedure(menuItemID);
        }

        private bool LoadByPrimaryKeyDynamic(System.String menuItemID)
        {
            esMenuItemQuery query = this.GetDynamicQuery();
            query.Where(query.MenuItemID == menuItemID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(System.String menuItemID)
        {
            esParameters parms = new esParameters();
            parms.Add("MenuItemID", menuItemID);
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
                        case "MenuItemID": this.str.MenuItemID = (string)value; break;
                        case "MenuItemName": this.str.MenuItemName = (string)value; break;
                        case "MenuID": this.str.MenuID = (string)value; break;
                        case "VersionID": this.str.VersionID = (string)value; break;
                        case "SeqNo": this.str.SeqNo = (string)value; break;
                        case "ClassID": this.str.ClassID = (string)value; break;
                        case "Notes": this.str.Notes = (string)value; break;
                        case "IsActive": this.str.IsActive = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
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
        /// Maps to MenuItem.MenuItemID
        /// </summary>
        virtual public System.String MenuItemID
        {
            get
            {
                return base.GetSystemString(MenuItemMetadata.ColumnNames.MenuItemID);
            }

            set
            {
                base.SetSystemString(MenuItemMetadata.ColumnNames.MenuItemID, value);
            }
        }

        /// <summary>
        /// Maps to MenuItem.MenuItemName
        /// </summary>
        virtual public System.String MenuItemName
        {
            get
            {
                return base.GetSystemString(MenuItemMetadata.ColumnNames.MenuItemName);
            }

            set
            {
                base.SetSystemString(MenuItemMetadata.ColumnNames.MenuItemName, value);
            }
        }

        /// <summary>
        /// Maps to MenuItem.MenuID
        /// </summary>
        virtual public System.String MenuID
        {
            get
            {
                return base.GetSystemString(MenuItemMetadata.ColumnNames.MenuID);
            }

            set
            {
                base.SetSystemString(MenuItemMetadata.ColumnNames.MenuID, value);
            }
        }

        /// <summary>
        /// Maps to MenuItem.VersionID
        /// </summary>
        virtual public System.String VersionID
        {
            get
            {
                return base.GetSystemString(MenuItemMetadata.ColumnNames.VersionID);
            }

            set
            {
                base.SetSystemString(MenuItemMetadata.ColumnNames.VersionID, value);
            }
        }

        /// <summary>
        /// Maps to MenuItem.SeqNo
        /// </summary>
        virtual public System.String SeqNo
        {
            get
            {
                return base.GetSystemString(MenuItemMetadata.ColumnNames.SeqNo);
            }

            set
            {
                base.SetSystemString(MenuItemMetadata.ColumnNames.SeqNo, value);
            }
        }

        /// <summary>
        /// Maps to MenuItem.ClassID
        /// </summary>
        virtual public System.String ClassID
        {
            get
            {
                return base.GetSystemString(MenuItemMetadata.ColumnNames.ClassID);
            }

            set
            {
                base.SetSystemString(MenuItemMetadata.ColumnNames.ClassID, value);
            }
        }

        /// <summary>
        /// Maps to MenuItem.Notes
        /// </summary>
        virtual public System.String Notes
        {
            get
            {
                return base.GetSystemString(MenuItemMetadata.ColumnNames.Notes);
            }

            set
            {
                base.SetSystemString(MenuItemMetadata.ColumnNames.Notes, value);
            }
        }

        /// <summary>
        /// Maps to MenuItem.IsActive
        /// </summary>
        virtual public System.Boolean? IsActive
        {
            get
            {
                return base.GetSystemBoolean(MenuItemMetadata.ColumnNames.IsActive);
            }

            set
            {
                base.SetSystemBoolean(MenuItemMetadata.ColumnNames.IsActive, value);
            }
        }

        /// <summary>
        /// Maps to MenuItem.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(MenuItemMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(MenuItemMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to MenuItem.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(MenuItemMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(MenuItemMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esMenuItem entity)
            {
                this.entity = entity;
            }


            public System.String MenuItemID
            {
                get
                {
                    System.String data = entity.MenuItemID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.MenuItemID = null;
                    else entity.MenuItemID = Convert.ToString(value);
                }
            }

            public System.String MenuItemName
            {
                get
                {
                    System.String data = entity.MenuItemName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.MenuItemName = null;
                    else entity.MenuItemName = Convert.ToString(value);
                }
            }

            public System.String MenuID
            {
                get
                {
                    System.String data = entity.MenuID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.MenuID = null;
                    else entity.MenuID = Convert.ToString(value);
                }
            }

            public System.String VersionID
            {
                get
                {
                    System.String data = entity.VersionID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.VersionID = null;
                    else entity.VersionID = Convert.ToString(value);
                }
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


            private esMenuItem entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esMenuItemQuery query)
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
                throw new Exception("esMenuItem can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    public partial class MenuItem : esMenuItem
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
    abstract public class esMenuItemQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return MenuItemMetadata.Meta();
            }
        }


        public esQueryItem MenuItemID
        {
            get
            {
                return new esQueryItem(this, MenuItemMetadata.ColumnNames.MenuItemID, esSystemType.String);
            }
        }

        public esQueryItem MenuItemName
        {
            get
            {
                return new esQueryItem(this, MenuItemMetadata.ColumnNames.MenuItemName, esSystemType.String);
            }
        }

        public esQueryItem MenuID
        {
            get
            {
                return new esQueryItem(this, MenuItemMetadata.ColumnNames.MenuID, esSystemType.String);
            }
        }

        public esQueryItem VersionID
        {
            get
            {
                return new esQueryItem(this, MenuItemMetadata.ColumnNames.VersionID, esSystemType.String);
            }
        }

        public esQueryItem SeqNo
        {
            get
            {
                return new esQueryItem(this, MenuItemMetadata.ColumnNames.SeqNo, esSystemType.String);
            }
        }

        public esQueryItem ClassID
        {
            get
            {
                return new esQueryItem(this, MenuItemMetadata.ColumnNames.ClassID, esSystemType.String);
            }
        }

        public esQueryItem Notes
        {
            get
            {
                return new esQueryItem(this, MenuItemMetadata.ColumnNames.Notes, esSystemType.String);
            }
        }

        public esQueryItem IsActive
        {
            get
            {
                return new esQueryItem(this, MenuItemMetadata.ColumnNames.IsActive, esSystemType.Boolean);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, MenuItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, MenuItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("MenuItemCollection")]
    public partial class MenuItemCollection : esMenuItemCollection, IEnumerable<MenuItem>
    {
        public MenuItemCollection()
        {

        }

        public static implicit operator List<MenuItem>(MenuItemCollection coll)
        {
            List<MenuItem> list = new List<MenuItem>();

            foreach (MenuItem emp in coll)
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
                return MenuItemMetadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new MenuItemQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new MenuItem(row);
        }

        override protected esEntity CreateEntity()
        {
            return new MenuItem();
        }


        #endregion


        [BrowsableAttribute(false)]
        public MenuItemQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new MenuItemQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(MenuItemQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public MenuItem AddNew()
        {
            MenuItem entity = base.AddNewEntity() as MenuItem;

            return entity;
        }

        public MenuItem FindByPrimaryKey(System.String menuItemID)
        {
            return base.FindByPrimaryKey(menuItemID) as MenuItem;
        }


        #region IEnumerable<MenuItem> Members

        IEnumerator<MenuItem> IEnumerable<MenuItem>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as MenuItem;
            }
        }

        #endregion

        private MenuItemQuery query;
    }


    /// <summary>
    /// Encapsulates the 'MenuItem' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("MenuItem ({MenuItemID})")]
    [Serializable]
    public partial class MenuItem : esMenuItem
    {
        public MenuItem()
        {

        }

        public MenuItem(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return MenuItemMetadata.Meta();
            }
        }



        override protected esMenuItemQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new MenuItemQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public MenuItemQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new MenuItemQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(MenuItemQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private MenuItemQuery query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class MenuItemQuery : esMenuItemQuery
    {
        public MenuItemQuery()
        {

        }

        public MenuItemQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "MenuItemQuery";
        }


    }


    [Serializable]
    public partial class MenuItemMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected MenuItemMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(MenuItemMetadata.ColumnNames.MenuItemID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = MenuItemMetadata.PropertyNames.MenuItemID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 50;
            _columns.Add(c);

            c = new esColumnMetadata(MenuItemMetadata.ColumnNames.MenuItemName, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = MenuItemMetadata.PropertyNames.MenuItemName;
            c.CharacterMaxLength = 200;
            _columns.Add(c);

            c = new esColumnMetadata(MenuItemMetadata.ColumnNames.MenuID, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = MenuItemMetadata.PropertyNames.MenuID;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(MenuItemMetadata.ColumnNames.VersionID, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = MenuItemMetadata.PropertyNames.VersionID;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(MenuItemMetadata.ColumnNames.SeqNo, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = MenuItemMetadata.PropertyNames.SeqNo;
            c.CharacterMaxLength = 3;
            _columns.Add(c);

            c = new esColumnMetadata(MenuItemMetadata.ColumnNames.ClassID, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = MenuItemMetadata.PropertyNames.ClassID;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(MenuItemMetadata.ColumnNames.Notes, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = MenuItemMetadata.PropertyNames.Notes;
            c.CharacterMaxLength = 500;
            _columns.Add(c);

            c = new esColumnMetadata(MenuItemMetadata.ColumnNames.IsActive, 7, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = MenuItemMetadata.PropertyNames.IsActive;
            _columns.Add(c);

            c = new esColumnMetadata(MenuItemMetadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = MenuItemMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MenuItemMetadata.ColumnNames.LastUpdateByUserID, 9, typeof(System.String), esSystemType.String);
            c.PropertyName = MenuItemMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public MenuItemMetadata Meta()
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
            public const string MenuItemID = "MenuItemID";
            public const string MenuItemName = "MenuItemName";
            public const string MenuID = "MenuID";
            public const string VersionID = "VersionID";
            public const string SeqNo = "SeqNo";
            public const string ClassID = "ClassID";
            public const string Notes = "Notes";
            public const string IsActive = "IsActive";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string MenuItemID = "MenuItemID";
            public const string MenuItemName = "MenuItemName";
            public const string MenuID = "MenuID";
            public const string VersionID = "VersionID";
            public const string SeqNo = "SeqNo";
            public const string ClassID = "ClassID";
            public const string Notes = "Notes";
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
            lock (typeof(MenuItemMetadata))
            {
                if (MenuItemMetadata.mapDelegates == null)
                {
                    MenuItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (MenuItemMetadata.meta == null)
                {
                    MenuItemMetadata.meta = new MenuItemMetadata();
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


                meta.AddTypeMap("MenuItemID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("MenuItemName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("MenuID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("VersionID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SeqNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ClassID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));



                meta.Source = "MenuItem";
                meta.Destination = "MenuItem";

                meta.spInsert = "proc_MenuItemInsert";
                meta.spUpdate = "proc_MenuItemUpdate";
                meta.spDelete = "proc_MenuItemDelete";
                meta.spLoadAll = "proc_MenuItemLoadAll";
                meta.spLoadByPrimaryKey = "proc_MenuItemLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private MenuItemMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
