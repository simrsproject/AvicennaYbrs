/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 4/25/2016 9:19:11 AM
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
    abstract public class esRlMasterReportCollection : esEntityCollectionWAuditLog
    {
        public esRlMasterReportCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "RlMasterReportCollection";
        }

        #region Query Logic
        protected void InitQuery(esRlMasterReportQuery query)
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
            this.InitQuery(query as esRlMasterReportQuery);
        }
        #endregion

        virtual public RlMasterReport DetachEntity(RlMasterReport entity)
        {
            return base.DetachEntity(entity) as RlMasterReport;
        }

        virtual public RlMasterReport AttachEntity(RlMasterReport entity)
        {
            return base.AttachEntity(entity) as RlMasterReport;
        }

        virtual public void Combine(RlMasterReportCollection collection)
        {
            base.Combine(collection);
        }

        new public RlMasterReport this[int index]
        {
            get
            {
                return base[index] as RlMasterReport;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(RlMasterReport);
        }
    }



    [Serializable]
    abstract public class esRlMasterReport : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esRlMasterReportQuery GetDynamicQuery()
        {
            return null;
        }

        public esRlMasterReport()
        {

        }

        public esRlMasterReport(DataRow row)
            : base(row)
        {

        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(System.Int32 rlMasterReportID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(rlMasterReportID);
            else
                return LoadByPrimaryKeyStoredProcedure(rlMasterReportID);
        }

        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 rlMasterReportID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(rlMasterReportID);
            else
                return LoadByPrimaryKeyStoredProcedure(rlMasterReportID);
        }

        private bool LoadByPrimaryKeyDynamic(System.Int32 rlMasterReportID)
        {
            esRlMasterReportQuery query = this.GetDynamicQuery();
            query.Where(query.RlMasterReportID == rlMasterReportID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(System.Int32 rlMasterReportID)
        {
            esParameters parms = new esParameters();
            parms.Add("RlMasterReportID", rlMasterReportID);
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
                        case "RlMasterReportID": this.str.RlMasterReportID = (string)value; break;
                        case "RlMasterReportNo": this.str.RlMasterReportNo = (string)value; break;
                        case "RlMasterReportName": this.str.RlMasterReportName = (string)value; break;
                        case "IsActive": this.str.IsActive = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "Notes": this.str.Notes = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "RlMasterReportID":

                            if (value == null || value is System.Int32)
                                this.RlMasterReportID = (System.Int32?)value;
                            break;

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
        /// Maps to RlMasterReport.RlMasterReportID
        /// </summary>
        virtual public System.Int32? RlMasterReportID
        {
            get
            {
                return base.GetSystemInt32(RlMasterReportMetadata.ColumnNames.RlMasterReportID);
            }

            set
            {
                base.SetSystemInt32(RlMasterReportMetadata.ColumnNames.RlMasterReportID, value);
            }
        }

        /// <summary>
        /// Maps to RlMasterReport.RlMasterReportNo
        /// </summary>
        virtual public System.String RlMasterReportNo
        {
            get
            {
                return base.GetSystemString(RlMasterReportMetadata.ColumnNames.RlMasterReportNo);
            }

            set
            {
                base.SetSystemString(RlMasterReportMetadata.ColumnNames.RlMasterReportNo, value);
            }
        }

        /// <summary>
        /// Maps to RlMasterReport.RlMasterReportName
        /// </summary>
        virtual public System.String RlMasterReportName
        {
            get
            {
                return base.GetSystemString(RlMasterReportMetadata.ColumnNames.RlMasterReportName);
            }

            set
            {
                base.SetSystemString(RlMasterReportMetadata.ColumnNames.RlMasterReportName, value);
            }
        }

        /// <summary>
        /// Maps to RlMasterReport.IsActive
        /// </summary>
        virtual public System.Boolean? IsActive
        {
            get
            {
                return base.GetSystemBoolean(RlMasterReportMetadata.ColumnNames.IsActive);
            }

            set
            {
                base.SetSystemBoolean(RlMasterReportMetadata.ColumnNames.IsActive, value);
            }
        }

        /// <summary>
        /// Maps to RlMasterReport.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(RlMasterReportMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(RlMasterReportMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to RlMasterReport.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(RlMasterReportMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(RlMasterReportMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }

        /// <summary>
        /// Maps to RlMasterReport.Notes
        /// </summary>
        virtual public System.String Notes
        {
            get
            {
                return base.GetSystemString(RlMasterReportMetadata.ColumnNames.Notes);
            }

            set
            {
                base.SetSystemString(RlMasterReportMetadata.ColumnNames.Notes, value);
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
            public esStrings(esRlMasterReport entity)
            {
                this.entity = entity;
            }


            public System.String RlMasterReportID
            {
                get
                {
                    System.Int32? data = entity.RlMasterReportID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RlMasterReportID = null;
                    else entity.RlMasterReportID = Convert.ToInt32(value);
                }
            }

            public System.String RlMasterReportNo
            {
                get
                {
                    System.String data = entity.RlMasterReportNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RlMasterReportNo = null;
                    else entity.RlMasterReportNo = Convert.ToString(value);
                }
            }

            public System.String RlMasterReportName
            {
                get
                {
                    System.String data = entity.RlMasterReportName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RlMasterReportName = null;
                    else entity.RlMasterReportName = Convert.ToString(value);
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


            private esRlMasterReport entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esRlMasterReportQuery query)
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
                throw new Exception("esRlMasterReport can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    public partial class RlMasterReport : esRlMasterReport
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
    abstract public class esRlMasterReportQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return RlMasterReportMetadata.Meta();
            }
        }


        public esQueryItem RlMasterReportID
        {
            get
            {
                return new esQueryItem(this, RlMasterReportMetadata.ColumnNames.RlMasterReportID, esSystemType.Int32);
            }
        }

        public esQueryItem RlMasterReportNo
        {
            get
            {
                return new esQueryItem(this, RlMasterReportMetadata.ColumnNames.RlMasterReportNo, esSystemType.String);
            }
        }

        public esQueryItem RlMasterReportName
        {
            get
            {
                return new esQueryItem(this, RlMasterReportMetadata.ColumnNames.RlMasterReportName, esSystemType.String);
            }
        }

        public esQueryItem IsActive
        {
            get
            {
                return new esQueryItem(this, RlMasterReportMetadata.ColumnNames.IsActive, esSystemType.Boolean);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, RlMasterReportMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, RlMasterReportMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem Notes
        {
            get
            {
                return new esQueryItem(this, RlMasterReportMetadata.ColumnNames.Notes, esSystemType.String);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("RlMasterReportCollection")]
    public partial class RlMasterReportCollection : esRlMasterReportCollection, IEnumerable<RlMasterReport>
    {
        public RlMasterReportCollection()
        {

        }

        public static implicit operator List<RlMasterReport>(RlMasterReportCollection coll)
        {
            List<RlMasterReport> list = new List<RlMasterReport>();

            foreach (RlMasterReport emp in coll)
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
                return RlMasterReportMetadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new RlMasterReportQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new RlMasterReport(row);
        }

        override protected esEntity CreateEntity()
        {
            return new RlMasterReport();
        }


        #endregion


        [BrowsableAttribute(false)]
        public RlMasterReportQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new RlMasterReportQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(RlMasterReportQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public RlMasterReport AddNew()
        {
            RlMasterReport entity = base.AddNewEntity() as RlMasterReport;

            return entity;
        }

        public RlMasterReport FindByPrimaryKey(System.Int32 rlMasterReportID)
        {
            return base.FindByPrimaryKey(rlMasterReportID) as RlMasterReport;
        }


        #region IEnumerable<RlMasterReport> Members

        IEnumerator<RlMasterReport> IEnumerable<RlMasterReport>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as RlMasterReport;
            }
        }

        #endregion

        private RlMasterReportQuery query;
    }


    /// <summary>
    /// Encapsulates the 'RlMasterReport' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("RlMasterReport ({RlMasterReportID})")]
    [Serializable]
    public partial class RlMasterReport : esRlMasterReport
    {
        public RlMasterReport()
        {

        }

        public RlMasterReport(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return RlMasterReportMetadata.Meta();
            }
        }



        override protected esRlMasterReportQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new RlMasterReportQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public RlMasterReportQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new RlMasterReportQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(RlMasterReportQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private RlMasterReportQuery query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class RlMasterReportQuery : esRlMasterReportQuery
    {
        public RlMasterReportQuery()
        {

        }

        public RlMasterReportQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "RlMasterReportQuery";
        }


    }


    [Serializable]
    public partial class RlMasterReportMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected RlMasterReportMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(RlMasterReportMetadata.ColumnNames.RlMasterReportID, 0, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlMasterReportMetadata.PropertyNames.RlMasterReportID;
            c.IsInPrimaryKey = true;
            c.IsAutoIncrement = true;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(RlMasterReportMetadata.ColumnNames.RlMasterReportNo, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = RlMasterReportMetadata.PropertyNames.RlMasterReportNo;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(RlMasterReportMetadata.ColumnNames.RlMasterReportName, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = RlMasterReportMetadata.PropertyNames.RlMasterReportName;
            c.CharacterMaxLength = 300;
            _columns.Add(c);

            c = new esColumnMetadata(RlMasterReportMetadata.ColumnNames.IsActive, 3, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = RlMasterReportMetadata.PropertyNames.IsActive;
            _columns.Add(c);

            c = new esColumnMetadata(RlMasterReportMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = RlMasterReportMetadata.PropertyNames.LastUpdateDateTime;
            _columns.Add(c);

            c = new esColumnMetadata(RlMasterReportMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = RlMasterReportMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            _columns.Add(c);

            c = new esColumnMetadata(RlMasterReportMetadata.ColumnNames.Notes, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = RlMasterReportMetadata.PropertyNames.Notes;
            c.CharacterMaxLength = 250;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public RlMasterReportMetadata Meta()
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
            public const string RlMasterReportID = "RlMasterReportID";
            public const string RlMasterReportNo = "RlMasterReportNo";
            public const string RlMasterReportName = "RlMasterReportName";
            public const string IsActive = "IsActive";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string Notes = "Notes";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string RlMasterReportID = "RlMasterReportID";
            public const string RlMasterReportNo = "RlMasterReportNo";
            public const string RlMasterReportName = "RlMasterReportName";
            public const string IsActive = "IsActive";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
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
            lock (typeof(RlMasterReportMetadata))
            {
                if (RlMasterReportMetadata.mapDelegates == null)
                {
                    RlMasterReportMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (RlMasterReportMetadata.meta == null)
                {
                    RlMasterReportMetadata.meta = new RlMasterReportMetadata();
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


                meta.AddTypeMap("RlMasterReportID", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("RlMasterReportNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("RlMasterReportName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));



                meta.Source = "RlMasterReport";
                meta.Destination = "RlMasterReport";

                meta.spInsert = "proc_RlMasterReportInsert";
                meta.spUpdate = "proc_RlMasterReportUpdate";
                meta.spDelete = "proc_RlMasterReportDelete";
                meta.spLoadAll = "proc_RlMasterReportLoadAll";
                meta.spLoadByPrimaryKey = "proc_RlMasterReportLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private RlMasterReportMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
