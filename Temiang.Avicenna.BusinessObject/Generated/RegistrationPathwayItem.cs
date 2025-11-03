/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 1/30/2020 4:50:33 AM
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
    abstract public class esRegistrationPathwayItemCollection : esEntityCollectionWAuditLog
    {
        public esRegistrationPathwayItemCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "RegistrationPathwayItemCollection";
        }

        #region Query Logic
        protected void InitQuery(esRegistrationPathwayItemQuery query)
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
            this.InitQuery(query as esRegistrationPathwayItemQuery);
        }
        #endregion

        virtual public RegistrationPathwayItem DetachEntity(RegistrationPathwayItem entity)
        {
            return base.DetachEntity(entity) as RegistrationPathwayItem;
        }

        virtual public RegistrationPathwayItem AttachEntity(RegistrationPathwayItem entity)
        {
            return base.AttachEntity(entity) as RegistrationPathwayItem;
        }

        virtual public void Combine(RegistrationPathwayItemCollection collection)
        {
            base.Combine(collection);
        }

        new public RegistrationPathwayItem this[int index]
        {
            get
            {
                return base[index] as RegistrationPathwayItem;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(RegistrationPathwayItem);
        }
    }



    [Serializable]
    abstract public class esRegistrationPathwayItem : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esRegistrationPathwayItemQuery GetDynamicQuery()
        {
            return null;
        }

        public esRegistrationPathwayItem()
        {

        }

        public esRegistrationPathwayItem(DataRow row)
            : base(row)
        {

        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(System.String pathwayID, System.Int32 pathwayItemSeqNo, System.String registrationNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(pathwayID, pathwayItemSeqNo, registrationNo);
            else
                return LoadByPrimaryKeyStoredProcedure(pathwayID, pathwayItemSeqNo, registrationNo);
        }

        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String pathwayID, System.Int32 pathwayItemSeqNo, System.String registrationNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(pathwayID, pathwayItemSeqNo, registrationNo);
            else
                return LoadByPrimaryKeyStoredProcedure(pathwayID, pathwayItemSeqNo, registrationNo);
        }

        private bool LoadByPrimaryKeyDynamic(System.String pathwayID, System.Int32 pathwayItemSeqNo, System.String registrationNo)
        {
            esRegistrationPathwayItemQuery query = this.GetDynamicQuery();
            query.Where(query.PathwayID == pathwayID, query.PathwayItemSeqNo == pathwayItemSeqNo, query.RegistrationNo == registrationNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(System.String pathwayID, System.Int32 pathwayItemSeqNo, System.String registrationNo)
        {
            esParameters parms = new esParameters();
            parms.Add("PathwayID", pathwayID); parms.Add("PathwayItemSeqNo", pathwayItemSeqNo); parms.Add("RegistrationNo", registrationNo);
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
                        case "PathwayID": this.str.PathwayID = (string)value; break;
                        case "PathwayItemSeqNo": this.str.PathwayItemSeqNo = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "PathwayItemSeqNo":

                            if (value == null || value is System.Int32)
                                this.PathwayItemSeqNo = (System.Int32?)value;
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
        /// Maps to RegistrationPathwayItem.RegistrationNo
        /// </summary>
        virtual public System.String RegistrationNo
        {
            get
            {
                return base.GetSystemString(RegistrationPathwayItemMetadata.ColumnNames.RegistrationNo);
            }

            set
            {
                base.SetSystemString(RegistrationPathwayItemMetadata.ColumnNames.RegistrationNo, value);
            }
        }

        /// <summary>
        /// Maps to RegistrationPathwayItem.PathwayID
        /// </summary>
        virtual public System.String PathwayID
        {
            get
            {
                return base.GetSystemString(RegistrationPathwayItemMetadata.ColumnNames.PathwayID);
            }

            set
            {
                base.SetSystemString(RegistrationPathwayItemMetadata.ColumnNames.PathwayID, value);
            }
        }

        /// <summary>
        /// Maps to RegistrationPathwayItem.PathwayItemSeqNo
        /// </summary>
        virtual public System.Int32? PathwayItemSeqNo
        {
            get
            {
                return base.GetSystemInt32(RegistrationPathwayItemMetadata.ColumnNames.PathwayItemSeqNo);
            }

            set
            {
                base.SetSystemInt32(RegistrationPathwayItemMetadata.ColumnNames.PathwayItemSeqNo, value);
            }
        }

        /// <summary>
        /// Maps to RegistrationPathwayItem.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(RegistrationPathwayItemMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(RegistrationPathwayItemMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to RegistrationPathwayItem.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(RegistrationPathwayItemMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(RegistrationPathwayItemMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esRegistrationPathwayItem entity)
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

            public System.String PathwayID
            {
                get
                {
                    System.String data = entity.PathwayID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PathwayID = null;
                    else entity.PathwayID = Convert.ToString(value);
                }
            }

            public System.String PathwayItemSeqNo
            {
                get
                {
                    System.Int32? data = entity.PathwayItemSeqNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PathwayItemSeqNo = null;
                    else entity.PathwayItemSeqNo = Convert.ToInt32(value);
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


            private esRegistrationPathwayItem entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esRegistrationPathwayItemQuery query)
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
                throw new Exception("esRegistrationPathwayItem can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    [Serializable]
    abstract public class esRegistrationPathwayItemQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return RegistrationPathwayItemMetadata.Meta();
            }
        }


        public esQueryItem RegistrationNo
        {
            get
            {
                return new esQueryItem(this, RegistrationPathwayItemMetadata.ColumnNames.RegistrationNo, esSystemType.String);
            }
        }

        public esQueryItem PathwayID
        {
            get
            {
                return new esQueryItem(this, RegistrationPathwayItemMetadata.ColumnNames.PathwayID, esSystemType.String);
            }
        }

        public esQueryItem PathwayItemSeqNo
        {
            get
            {
                return new esQueryItem(this, RegistrationPathwayItemMetadata.ColumnNames.PathwayItemSeqNo, esSystemType.Int32);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, RegistrationPathwayItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, RegistrationPathwayItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("RegistrationPathwayItemCollection")]
    public partial class RegistrationPathwayItemCollection : esRegistrationPathwayItemCollection, IEnumerable<RegistrationPathwayItem>
    {
        public RegistrationPathwayItemCollection()
        {

        }

        public static implicit operator List<RegistrationPathwayItem>(RegistrationPathwayItemCollection coll)
        {
            List<RegistrationPathwayItem> list = new List<RegistrationPathwayItem>();

            foreach (RegistrationPathwayItem emp in coll)
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
                return RegistrationPathwayItemMetadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new RegistrationPathwayItemQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new RegistrationPathwayItem(row);
        }

        override protected esEntity CreateEntity()
        {
            return new RegistrationPathwayItem();
        }


        #endregion


        [BrowsableAttribute(false)]
        public RegistrationPathwayItemQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new RegistrationPathwayItemQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(RegistrationPathwayItemQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public RegistrationPathwayItem AddNew()
        {
            RegistrationPathwayItem entity = base.AddNewEntity() as RegistrationPathwayItem;

            return entity;
        }

        public RegistrationPathwayItem FindByPrimaryKey(System.String pathwayID, System.Int32 pathwayItemSeqNo, System.String registrationNo)
        {
            return base.FindByPrimaryKey(pathwayID, pathwayItemSeqNo, registrationNo) as RegistrationPathwayItem;
        }


        #region IEnumerable<RegistrationPathwayItem> Members

        IEnumerator<RegistrationPathwayItem> IEnumerable<RegistrationPathwayItem>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as RegistrationPathwayItem;
            }
        }

        #endregion

        private RegistrationPathwayItemQuery query;
    }


    /// <summary>
    /// Encapsulates the 'RegistrationPathwayItem' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("RegistrationPathwayItem ({RegistrationNo},{PathwayID},{PathwayItemSeqNo})")]
    [Serializable]
    public partial class RegistrationPathwayItem : esRegistrationPathwayItem
    {
        public RegistrationPathwayItem()
        {

        }

        public RegistrationPathwayItem(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return RegistrationPathwayItemMetadata.Meta();
            }
        }



        override protected esRegistrationPathwayItemQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new RegistrationPathwayItemQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public RegistrationPathwayItemQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new RegistrationPathwayItemQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(RegistrationPathwayItemQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private RegistrationPathwayItemQuery query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class RegistrationPathwayItemQuery : esRegistrationPathwayItemQuery
    {
        public RegistrationPathwayItemQuery()
        {

        }

        public RegistrationPathwayItemQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "RegistrationPathwayItemQuery";
        }


    }


    [Serializable]
    public partial class RegistrationPathwayItemMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected RegistrationPathwayItemMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(RegistrationPathwayItemMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = RegistrationPathwayItemMetadata.PropertyNames.RegistrationNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationPathwayItemMetadata.ColumnNames.PathwayID, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = RegistrationPathwayItemMetadata.PropertyNames.PathwayID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationPathwayItemMetadata.ColumnNames.PathwayItemSeqNo, 2, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RegistrationPathwayItemMetadata.PropertyNames.PathwayItemSeqNo;
            c.IsInPrimaryKey = true;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationPathwayItemMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = RegistrationPathwayItemMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationPathwayItemMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = RegistrationPathwayItemMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public RegistrationPathwayItemMetadata Meta()
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
            public const string PathwayID = "PathwayID";
            public const string PathwayItemSeqNo = "PathwayItemSeqNo";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string RegistrationNo = "RegistrationNo";
            public const string PathwayID = "PathwayID";
            public const string PathwayItemSeqNo = "PathwayItemSeqNo";
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
            lock (typeof(RegistrationPathwayItemMetadata))
            {
                if (RegistrationPathwayItemMetadata.mapDelegates == null)
                {
                    RegistrationPathwayItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (RegistrationPathwayItemMetadata.meta == null)
                {
                    RegistrationPathwayItemMetadata.meta = new RegistrationPathwayItemMetadata();
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
                meta.AddTypeMap("PathwayID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("PathwayItemSeqNo", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));



                meta.Source = "RegistrationPathwayItem";
                meta.Destination = "RegistrationPathwayItem";

                meta.spInsert = "proc_RegistrationPathwayItemInsert";
                meta.spUpdate = "proc_RegistrationPathwayItemUpdate";
                meta.spDelete = "proc_RegistrationPathwayItemDelete";
                meta.spLoadAll = "proc_RegistrationPathwayItemLoadAll";
                meta.spLoadByPrimaryKey = "proc_RegistrationPathwayItemLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private RegistrationPathwayItemMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
