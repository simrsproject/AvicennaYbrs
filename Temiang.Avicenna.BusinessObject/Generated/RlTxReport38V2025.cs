/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 3/4/2013 12:20:15 PM
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
    abstract public class esRlTxReport38V2025Collection : esEntityCollectionWAuditLog
    {
        public esRlTxReport38V2025Collection()
        {

        }

        protected override string GetCollectionName()
        {
            return "RlTxReport38V2025Collection";
        }

        #region Query Logic
        protected void InitQuery(esRlTxReport38V2025Query query)
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
            this.InitQuery(query as esRlTxReport38V2025Query);
        }
        #endregion

        virtual public RlTxReport38V2025 DetachEntity(RlTxReport38V2025 entity)
        {
            return base.DetachEntity(entity) as RlTxReport38V2025;
        }

        virtual public RlTxReport38V2025 AttachEntity(RlTxReport38V2025 entity)
        {
            return base.AttachEntity(entity) as RlTxReport38V2025;
        }

        virtual public void Combine(RlTxReport38V2025Collection collection)
        {
            base.Combine(collection);
        }

        new public RlTxReport38V2025 this[int index]
        {
            get
            {
                return base[index] as RlTxReport38V2025;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(RlTxReport38V2025);
        }
    }



    [Serializable]
    abstract public class esRlTxReport38V2025 : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esRlTxReport38V2025Query GetDynamicQuery()
        {
            return null;
        }

        public esRlTxReport38V2025()
        {

        }

        public esRlTxReport38V2025(DataRow row)
            : base(row)
        {

        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(System.String rlTxReportNo, System.Int32 rlMasterReportItemID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(rlTxReportNo, rlMasterReportItemID);
            else
                return LoadByPrimaryKeyStoredProcedure(rlTxReportNo, rlMasterReportItemID);
        }

        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String rlTxReportNo, System.Int32 rlMasterReportItemID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(rlTxReportNo, rlMasterReportItemID);
            else
                return LoadByPrimaryKeyStoredProcedure(rlTxReportNo, rlMasterReportItemID);
        }

        private bool LoadByPrimaryKeyDynamic(System.String rlTxReportNo, System.Int32 rlMasterReportItemID)
        {
            esRlTxReport38V2025Query query = this.GetDynamicQuery();
            query.Where(query.RlTxReportNo == rlTxReportNo, query.RlMasterReportItemID == rlMasterReportItemID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(System.String rlTxReportNo, System.Int32 rlMasterReportItemID)
        {
            esParameters parms = new esParameters();
            parms.Add("RlTxReportNo", rlTxReportNo); parms.Add("RlMasterReportItemID", rlMasterReportItemID);
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
                        case "RlTxReportNo": this.str.RlTxReportNo = (string)value; break;
                        case "RlMasterReportItemID": this.str.RlMasterReportItemID = (string)value; break;
                        case "JumlahPemeriksaanL": this.str.JumlahPemeriksaanL = (string)value; break;
                        case "JumlahPemeriksaanP": this.str.JumlahPemeriksaanP = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "RlMasterReportItemID":

                            if (value == null || value is System.Int32)
                                this.RlMasterReportItemID = (System.Int32?)value;
                            break;

                        case "JumlahPemeriksaanL":

                            if (value == null || value is System.Int32)
                                this.JumlahPemeriksaanL = (System.Int32?)value;
                            break;

                        case "JumlahPemeriksaanP":

                            if (value == null || value is System.Int32)
                                this.JumlahPemeriksaanP = (System.Int32?)value;
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
        /// Maps to RlTxReport3_8.RlTxReportNo
        /// </summary>
        virtual public System.String RlTxReportNo
        {
            get
            {
                return base.GetSystemString(RlTxReport38V2025Metadata.ColumnNames.RlTxReportNo);
            }

            set
            {
                base.SetSystemString(RlTxReport38V2025Metadata.ColumnNames.RlTxReportNo, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_8.RlMasterReportItemID
        /// </summary>
        virtual public System.Int32? RlMasterReportItemID
        {
            get
            {
                return base.GetSystemInt32(RlTxReport38V2025Metadata.ColumnNames.RlMasterReportItemID);
            }

            set
            {
                base.SetSystemInt32(RlTxReport38V2025Metadata.ColumnNames.RlMasterReportItemID, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_8.JumlahPemeriksaanL
        /// </summary>
        virtual public System.Int32? JumlahPemeriksaanL
        {
            get
            {
                return base.GetSystemInt32(RlTxReport38V2025Metadata.ColumnNames.JumlahPemeriksaanL);
            }

            set
            {
                base.SetSystemInt32(RlTxReport38V2025Metadata.ColumnNames.JumlahPemeriksaanL, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_8.JumlahPemeriksaanP
        /// </summary>
        virtual public System.Int32? JumlahPemeriksaanP
        {
            get
            {
                return base.GetSystemInt32(RlTxReport38V2025Metadata.ColumnNames.JumlahPemeriksaanP);
            }

            set
            {
                base.SetSystemInt32(RlTxReport38V2025Metadata.ColumnNames.JumlahPemeriksaanP, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_8.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(RlTxReport38V2025Metadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(RlTxReport38V2025Metadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_8.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(RlTxReport38V2025Metadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(RlTxReport38V2025Metadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esRlTxReport38V2025 entity)
            {
                this.entity = entity;
            }


            public System.String RlTxReportNo
            {
                get
                {
                    System.String data = entity.RlTxReportNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RlTxReportNo = null;
                    else entity.RlTxReportNo = Convert.ToString(value);
                }
            }

            public System.String RlMasterReportItemID
            {
                get
                {
                    System.Int32? data = entity.RlMasterReportItemID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RlMasterReportItemID = null;
                    else entity.RlMasterReportItemID = Convert.ToInt32(value);
                }
            }

            public System.String JumlahPemeriksaanL
            {
                get
                {
                    System.Int32? data = entity.JumlahPemeriksaanL;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.JumlahPemeriksaanL = null;
                    else entity.JumlahPemeriksaanL = Convert.ToInt32(value);
                }
            }

            public System.String JumlahPemeriksaanP
            {
                get
                {
                    System.Int32? data = entity.JumlahPemeriksaanP;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.JumlahPemeriksaanP = null;
                    else entity.JumlahPemeriksaanP = Convert.ToInt32(value);
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


            private esRlTxReport38V2025 entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esRlTxReport38V2025Query query)
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
                throw new Exception("esRlTxReport38V2025 can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    public partial class RlTxReport38V2025 : esRlTxReport38V2025
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
    abstract public class esRlTxReport38V2025Query : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return RlTxReport38V2025Metadata.Meta();
            }
        }


        public esQueryItem RlTxReportNo
        {
            get
            {
                return new esQueryItem(this, RlTxReport38V2025Metadata.ColumnNames.RlTxReportNo, esSystemType.String);
            }
        }

        public esQueryItem RlMasterReportItemID
        {
            get
            {
                return new esQueryItem(this, RlTxReport38V2025Metadata.ColumnNames.RlMasterReportItemID, esSystemType.Int32);
            }
        }

        public esQueryItem JumlahPemeriksaanL
        {
            get
            {
                return new esQueryItem(this, RlTxReport38V2025Metadata.ColumnNames.JumlahPemeriksaanL, esSystemType.Int32);
            }
        }

        public esQueryItem JumlahPemeriksaanP
        {
            get
            {
                return new esQueryItem(this, RlTxReport38V2025Metadata.ColumnNames.JumlahPemeriksaanP, esSystemType.Int32);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, RlTxReport38V2025Metadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, RlTxReport38V2025Metadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("RlTxReport38V2025Collection")]
    public partial class RlTxReport38V2025Collection : esRlTxReport38V2025Collection, IEnumerable<RlTxReport38V2025>
    {
        public RlTxReport38V2025Collection()
        {

        }

        public static implicit operator List<RlTxReport38V2025>(RlTxReport38V2025Collection coll)
        {
            List<RlTxReport38V2025> list = new List<RlTxReport38V2025>();

            foreach (RlTxReport38V2025 emp in coll)
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
                return RlTxReport38V2025Metadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new RlTxReport38V2025Query();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new RlTxReport38V2025(row);
        }

        override protected esEntity CreateEntity()
        {
            return new RlTxReport38V2025();
        }


        #endregion


        [BrowsableAttribute(false)]
        public RlTxReport38V2025Query Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new RlTxReport38V2025Query();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(RlTxReport38V2025Query query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public RlTxReport38V2025 AddNew()
        {
            RlTxReport38V2025 entity = base.AddNewEntity() as RlTxReport38V2025;

            return entity;
        }

        public RlTxReport38V2025 FindByPrimaryKey(System.String rlTxReportNo, System.Int32 rlMasterReportItemID)
        {
            return base.FindByPrimaryKey(rlTxReportNo, rlMasterReportItemID) as RlTxReport38V2025;
        }


        #region IEnumerable<RlTxReport38> Members

        IEnumerator<RlTxReport38V2025> IEnumerable<RlTxReport38V2025>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as RlTxReport38V2025;
            }
        }

        #endregion

        private RlTxReport38V2025Query query;
    }


    /// <summary>
    /// Encapsulates the 'RlTxReport3_8' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("RlTxReport38V2025 ({RlTxReportNo},{RlMasterReportItemID})")]
    [Serializable]
    public partial class RlTxReport38V2025 : esRlTxReport38V2025
    {
        public RlTxReport38V2025()
        {

        }

        public RlTxReport38V2025(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return RlTxReport38V2025Metadata.Meta();
            }
        }



        override protected esRlTxReport38V2025Query GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new RlTxReport38V2025Query();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public RlTxReport38V2025Query Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new RlTxReport38V2025Query();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(RlTxReport38V2025Query query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private RlTxReport38V2025Query query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class RlTxReport38V2025Query : esRlTxReport38V2025Query
    {
        public RlTxReport38V2025Query()
        {

        }

        public RlTxReport38V2025Query(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "RlTxReport38V2025Query";
        }


    }


    [Serializable]
    public partial class RlTxReport38V2025Metadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected RlTxReport38V2025Metadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(RlTxReport38V2025Metadata.ColumnNames.RlTxReportNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = RlTxReport38V2025Metadata.PropertyNames.RlTxReportNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport38V2025Metadata.ColumnNames.RlMasterReportItemID, 1, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport38V2025Metadata.PropertyNames.RlMasterReportItemID;
            c.IsInPrimaryKey = true;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport38V2025Metadata.ColumnNames.JumlahPemeriksaanL, 2, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport38V2025Metadata.PropertyNames.JumlahPemeriksaanL;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport38V2025Metadata.ColumnNames.JumlahPemeriksaanP, 3, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport38V2025Metadata.PropertyNames.JumlahPemeriksaanP;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport38V2025Metadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = RlTxReport38V2025Metadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport38V2025Metadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = RlTxReport38V2025Metadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public RlTxReport38V2025Metadata Meta()
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
            public const string RlTxReportNo = "RlTxReportNo";
            public const string RlMasterReportItemID = "RlMasterReportItemID";
            public const string JumlahPemeriksaanL = "JumlahPemeriksaanL";
            public const string JumlahPemeriksaanP = "JumlahPemeriksaanP";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string RlTxReportNo = "RlTxReportNo";
            public const string RlMasterReportItemID = "RlMasterReportItemID";
            public const string JumlahPemeriksaanL = "JumlahPemeriksaanL";
            public const string JumlahPemeriksaanP = "JumlahPemeriksaanP";
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
            lock (typeof(RlTxReport38V2025Metadata))
            {
                if (RlTxReport38V2025Metadata.mapDelegates == null)
                {
                    RlTxReport38V2025Metadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (RlTxReport38V2025Metadata.meta == null)
                {
                    RlTxReport38V2025Metadata.meta = new RlTxReport38V2025Metadata();
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


                meta.AddTypeMap("RlTxReportNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("RlMasterReportItemID", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("JumlahPemeriksaanL", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("JumlahPemeriksaanP", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));



                meta.Source = "RlTxReport3_8V2025";
                meta.Destination = "RlTxReport3_8V2025";

                meta.spInsert = "proc_RlTxReport3_8V2025Insert";
                meta.spUpdate = "proc_RlTxReport3_8V2025Update";
                meta.spDelete = "proc_RlTxReport3_8V2025Delete";
                meta.spLoadAll = "proc_RlTxReport3_8V2025LoadAll";
                meta.spLoadByPrimaryKey = "proc_RlTxReport3_8V2025LoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private RlTxReport38V2025Metadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
