/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 11/13/2012 11:09:52 AM
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
    abstract public class esRegistrationOpenCloseHistoryCollection : esEntityCollectionWAuditLog
    {
        public esRegistrationOpenCloseHistoryCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "RegistrationOpenCloseHistoryCollection";
        }

        #region Query Logic
        protected void InitQuery(esRegistrationOpenCloseHistoryQuery query)
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
            this.InitQuery(query as esRegistrationOpenCloseHistoryQuery);
        }
        #endregion

        virtual public RegistrationOpenCloseHistory DetachEntity(RegistrationOpenCloseHistory entity)
        {
            return base.DetachEntity(entity) as RegistrationOpenCloseHistory;
        }

        virtual public RegistrationOpenCloseHistory AttachEntity(RegistrationOpenCloseHistory entity)
        {
            return base.AttachEntity(entity) as RegistrationOpenCloseHistory;
        }

        virtual public void Combine(RegistrationOpenCloseHistoryCollection collection)
        {
            base.Combine(collection);
        }

        new public RegistrationOpenCloseHistory this[int index]
        {
            get
            {
                return base[index] as RegistrationOpenCloseHistory;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(RegistrationOpenCloseHistory);
        }
    }



    [Serializable]
    abstract public class esRegistrationOpenCloseHistory : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esRegistrationOpenCloseHistoryQuery GetDynamicQuery()
        {
            return null;
        }

        public esRegistrationOpenCloseHistory()
        {

        }

        public esRegistrationOpenCloseHistory(DataRow row)
            : base(row)
        {

        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(System.String registrationNo, System.DateTime lastUpdateDateTime, System.String lastUpdateByUserID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(registrationNo, lastUpdateDateTime, lastUpdateByUserID);
            else
                return LoadByPrimaryKeyStoredProcedure(registrationNo, lastUpdateDateTime, lastUpdateByUserID);
        }

        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String registrationNo, System.DateTime lastUpdateDateTime, System.String lastUpdateByUserID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(registrationNo, lastUpdateDateTime, lastUpdateByUserID);
            else
                return LoadByPrimaryKeyStoredProcedure(registrationNo, lastUpdateDateTime, lastUpdateByUserID);
        }

        private bool LoadByPrimaryKeyDynamic(System.String registrationNo, System.DateTime lastUpdateDateTime, System.String lastUpdateByUserID)
        {
            esRegistrationOpenCloseHistoryQuery query = this.GetDynamicQuery();
            query.Where(query.RegistrationNo == registrationNo, query.LastUpdateDateTime == lastUpdateDateTime, query.LastUpdateByUserID == lastUpdateByUserID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(System.String registrationNo, System.DateTime lastUpdateDateTime, System.String lastUpdateByUserID)
        {
            esParameters parms = new esParameters();
            parms.Add("RegistrationNo", registrationNo); parms.Add("LastUpdateDateTime", lastUpdateDateTime); parms.Add("LastUpdateByUserID", lastUpdateByUserID);
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
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "IsClosed": this.str.IsClosed = (string)value; break;
                        case "Notes": this.str.Notes = (string)value; break;
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

                        case "IsClosed":

                            if (value == null || value is System.Boolean)
                                this.IsClosed = (System.Boolean?)value;
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
        /// Maps to RegistrationOpenCloseHistory.RegistrationNo
        /// </summary>
        virtual public System.String RegistrationNo
        {
            get
            {
                return base.GetSystemString(RegistrationOpenCloseHistoryMetadata.ColumnNames.RegistrationNo);
            }

            set
            {
                base.SetSystemString(RegistrationOpenCloseHistoryMetadata.ColumnNames.RegistrationNo, value);
            }
        }

        /// <summary>
        /// Maps to RegistrationOpenCloseHistory.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(RegistrationOpenCloseHistoryMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(RegistrationOpenCloseHistoryMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to RegistrationOpenCloseHistory.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(RegistrationOpenCloseHistoryMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(RegistrationOpenCloseHistoryMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }

        /// <summary>
        /// Maps to RegistrationOpenCloseHistory.IsClosed
        /// </summary>
        virtual public System.Boolean? IsClosed
        {
            get
            {
                return base.GetSystemBoolean(RegistrationOpenCloseHistoryMetadata.ColumnNames.IsClosed);
            }

            set
            {
                base.SetSystemBoolean(RegistrationOpenCloseHistoryMetadata.ColumnNames.IsClosed, value);
            }
        }

        /// <summary>
        /// Maps to RegistrationOpenCloseHistory.Notes
        /// </summary>
        virtual public System.String Notes
        {
            get
            {
                return base.GetSystemString(RegistrationOpenCloseHistoryMetadata.ColumnNames.Notes);
            }

            set
            {
                base.SetSystemString(RegistrationOpenCloseHistoryMetadata.ColumnNames.Notes, value);
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
            public esStrings(esRegistrationOpenCloseHistory entity)
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

            public System.String IsClosed
            {
                get
                {
                    System.Boolean? data = entity.IsClosed;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsClosed = null;
                    else entity.IsClosed = Convert.ToBoolean(value);
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


            private esRegistrationOpenCloseHistory entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esRegistrationOpenCloseHistoryQuery query)
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
                throw new Exception("esRegistrationOpenCloseHistory can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    public partial class RegistrationOpenCloseHistory : esRegistrationOpenCloseHistory
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
    abstract public class esRegistrationOpenCloseHistoryQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return RegistrationOpenCloseHistoryMetadata.Meta();
            }
        }


        public esQueryItem RegistrationNo
        {
            get
            {
                return new esQueryItem(this, RegistrationOpenCloseHistoryMetadata.ColumnNames.RegistrationNo, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, RegistrationOpenCloseHistoryMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, RegistrationOpenCloseHistoryMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem IsClosed
        {
            get
            {
                return new esQueryItem(this, RegistrationOpenCloseHistoryMetadata.ColumnNames.IsClosed, esSystemType.Boolean);
            }
        }

        public esQueryItem Notes
        {
            get
            {
                return new esQueryItem(this, RegistrationOpenCloseHistoryMetadata.ColumnNames.Notes, esSystemType.String);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("RegistrationOpenCloseHistoryCollection")]
    public partial class RegistrationOpenCloseHistoryCollection : esRegistrationOpenCloseHistoryCollection, IEnumerable<RegistrationOpenCloseHistory>
    {
        public RegistrationOpenCloseHistoryCollection()
        {

        }

        public static implicit operator List<RegistrationOpenCloseHistory>(RegistrationOpenCloseHistoryCollection coll)
        {
            List<RegistrationOpenCloseHistory> list = new List<RegistrationOpenCloseHistory>();

            foreach (RegistrationOpenCloseHistory emp in coll)
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
                return RegistrationOpenCloseHistoryMetadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new RegistrationOpenCloseHistoryQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new RegistrationOpenCloseHistory(row);
        }

        override protected esEntity CreateEntity()
        {
            return new RegistrationOpenCloseHistory();
        }


        #endregion


        [BrowsableAttribute(false)]
        public RegistrationOpenCloseHistoryQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new RegistrationOpenCloseHistoryQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(RegistrationOpenCloseHistoryQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public RegistrationOpenCloseHistory AddNew()
        {
            RegistrationOpenCloseHistory entity = base.AddNewEntity() as RegistrationOpenCloseHistory;

            return entity;
        }

        public RegistrationOpenCloseHistory FindByPrimaryKey(System.String registrationNo, System.DateTime lastUpdateDateTime, System.String lastUpdateByUserID)
        {
            return base.FindByPrimaryKey(registrationNo, lastUpdateDateTime, lastUpdateByUserID) as RegistrationOpenCloseHistory;
        }


        #region IEnumerable<RegistrationOpenCloseHistory> Members

        IEnumerator<RegistrationOpenCloseHistory> IEnumerable<RegistrationOpenCloseHistory>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as RegistrationOpenCloseHistory;
            }
        }

        #endregion

        private RegistrationOpenCloseHistoryQuery query;
    }


    /// <summary>
    /// Encapsulates the 'RegistrationOpenCloseHistory' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("RegistrationOpenCloseHistory ({RegistrationNo},{LastUpdateDateTime},{LastUpdateByUserID})")]
    [Serializable]
    public partial class RegistrationOpenCloseHistory : esRegistrationOpenCloseHistory
    {
        public RegistrationOpenCloseHistory()
        {

        }

        public RegistrationOpenCloseHistory(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return RegistrationOpenCloseHistoryMetadata.Meta();
            }
        }



        override protected esRegistrationOpenCloseHistoryQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new RegistrationOpenCloseHistoryQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public RegistrationOpenCloseHistoryQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new RegistrationOpenCloseHistoryQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(RegistrationOpenCloseHistoryQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private RegistrationOpenCloseHistoryQuery query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class RegistrationOpenCloseHistoryQuery : esRegistrationOpenCloseHistoryQuery
    {
        public RegistrationOpenCloseHistoryQuery()
        {

        }

        public RegistrationOpenCloseHistoryQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "RegistrationOpenCloseHistoryQuery";
        }


    }


    [Serializable]
    public partial class RegistrationOpenCloseHistoryMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected RegistrationOpenCloseHistoryMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(RegistrationOpenCloseHistoryMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = RegistrationOpenCloseHistoryMetadata.PropertyNames.RegistrationNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationOpenCloseHistoryMetadata.ColumnNames.LastUpdateDateTime, 1, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = RegistrationOpenCloseHistoryMetadata.PropertyNames.LastUpdateDateTime;
            c.IsInPrimaryKey = true;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationOpenCloseHistoryMetadata.ColumnNames.LastUpdateByUserID, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = RegistrationOpenCloseHistoryMetadata.PropertyNames.LastUpdateByUserID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 15;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationOpenCloseHistoryMetadata.ColumnNames.IsClosed, 3, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = RegistrationOpenCloseHistoryMetadata.PropertyNames.IsClosed;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationOpenCloseHistoryMetadata.ColumnNames.Notes, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = RegistrationOpenCloseHistoryMetadata.PropertyNames.Notes;
            c.CharacterMaxLength = 50;
            _columns.Add(c);

        }
        #endregion

        static public RegistrationOpenCloseHistoryMetadata Meta()
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
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string IsClosed = "IsClosed";
            public const string Notes = "Notes";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string RegistrationNo = "RegistrationNo";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string IsClosed = "IsClosed";
            public const string Notes = "Notes";
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
            lock (typeof(RegistrationOpenCloseHistoryMetadata))
            {
                if (RegistrationOpenCloseHistoryMetadata.mapDelegates == null)
                {
                    RegistrationOpenCloseHistoryMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (RegistrationOpenCloseHistoryMetadata.meta == null)
                {
                    RegistrationOpenCloseHistoryMetadata.meta = new RegistrationOpenCloseHistoryMetadata();
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
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsClosed", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));



                meta.Source = "RegistrationOpenCloseHistory";
                meta.Destination = "RegistrationOpenCloseHistory";

                meta.spInsert = "proc_RegistrationOpenCloseHistoryInsert";
                meta.spUpdate = "proc_RegistrationOpenCloseHistoryUpdate";
                meta.spDelete = "proc_RegistrationOpenCloseHistoryDelete";
                meta.spLoadAll = "proc_RegistrationOpenCloseHistoryLoadAll";
                meta.spLoadByPrimaryKey = "proc_RegistrationOpenCloseHistoryLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private RegistrationOpenCloseHistoryMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
