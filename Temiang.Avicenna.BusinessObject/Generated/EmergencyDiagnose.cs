/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 1/3/2013 9:58:29 AM
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
    abstract public class esEmergencyDiagnoseCollection : esEntityCollectionWAuditLog
    {
        public esEmergencyDiagnoseCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "EmergencyDiagnoseCollection";
        }

        #region Query Logic
        protected void InitQuery(esEmergencyDiagnoseQuery query)
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
            this.InitQuery(query as esEmergencyDiagnoseQuery);
        }
        #endregion

        virtual public EmergencyDiagnose DetachEntity(EmergencyDiagnose entity)
        {
            return base.DetachEntity(entity) as EmergencyDiagnose;
        }

        virtual public EmergencyDiagnose AttachEntity(EmergencyDiagnose entity)
        {
            return base.AttachEntity(entity) as EmergencyDiagnose;
        }

        virtual public void Combine(EmergencyDiagnoseCollection collection)
        {
            base.Combine(collection);
        }

        new public EmergencyDiagnose this[int index]
        {
            get
            {
                return base[index] as EmergencyDiagnose;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(EmergencyDiagnose);
        }
    }



    [Serializable]
    abstract public class esEmergencyDiagnose : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esEmergencyDiagnoseQuery GetDynamicQuery()
        {
            return null;
        }

        public esEmergencyDiagnose()
        {

        }

        public esEmergencyDiagnose(DataRow row)
            : base(row)
        {

        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(System.String emrDiagnoseID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(emrDiagnoseID);
            else
                return LoadByPrimaryKeyStoredProcedure(emrDiagnoseID);
        }

        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String emrDiagnoseID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(emrDiagnoseID);
            else
                return LoadByPrimaryKeyStoredProcedure(emrDiagnoseID);
        }

        private bool LoadByPrimaryKeyDynamic(System.String emrDiagnoseID)
        {
            esEmergencyDiagnoseQuery query = this.GetDynamicQuery();
            query.Where(query.EmrDiagnoseID == emrDiagnoseID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(System.String emrDiagnoseID)
        {
            esParameters parms = new esParameters();
            parms.Add("EmrDiagnoseID", emrDiagnoseID);
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
                        case "EmrDiagnoseID": this.str.EmrDiagnoseID = (string)value; break;
                        case "EmrDiagnoseName": this.str.EmrDiagnoseName = (string)value; break;
                        case "SREmrDiagnoseGroupID": this.str.SREmrDiagnoseGroupID = (string)value; break;
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
        /// Maps to EmergencyDiagnose.EmrDiagnoseID
        /// </summary>
        virtual public System.String EmrDiagnoseID
        {
            get
            {
                return base.GetSystemString(EmergencyDiagnoseMetadata.ColumnNames.EmrDiagnoseID);
            }

            set
            {
                base.SetSystemString(EmergencyDiagnoseMetadata.ColumnNames.EmrDiagnoseID, value);
            }
        }

        /// <summary>
        /// Maps to EmergencyDiagnose.EmrDiagnoseName
        /// </summary>
        virtual public System.String EmrDiagnoseName
        {
            get
            {
                return base.GetSystemString(EmergencyDiagnoseMetadata.ColumnNames.EmrDiagnoseName);
            }

            set
            {
                base.SetSystemString(EmergencyDiagnoseMetadata.ColumnNames.EmrDiagnoseName, value);
            }
        }

        /// <summary>
        /// Maps to EmergencyDiagnose.SREmrDiagnoseGroupID
        /// </summary>
        virtual public System.String SREmrDiagnoseGroupID
        {
            get
            {
                return base.GetSystemString(EmergencyDiagnoseMetadata.ColumnNames.SREmrDiagnoseGroupID);
            }

            set
            {
                base.SetSystemString(EmergencyDiagnoseMetadata.ColumnNames.SREmrDiagnoseGroupID, value);
            }
        }

        /// <summary>
        /// Maps to EmergencyDiagnose.IsActive
        /// </summary>
        virtual public System.Boolean? IsActive
        {
            get
            {
                return base.GetSystemBoolean(EmergencyDiagnoseMetadata.ColumnNames.IsActive);
            }

            set
            {
                base.SetSystemBoolean(EmergencyDiagnoseMetadata.ColumnNames.IsActive, value);
            }
        }

        /// <summary>
        /// Maps to EmergencyDiagnose.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(EmergencyDiagnoseMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(EmergencyDiagnoseMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to EmergencyDiagnose.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(EmergencyDiagnoseMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(EmergencyDiagnoseMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esEmergencyDiagnose entity)
            {
                this.entity = entity;
            }


            public System.String EmrDiagnoseID
            {
                get
                {
                    System.String data = entity.EmrDiagnoseID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.EmrDiagnoseID = null;
                    else entity.EmrDiagnoseID = Convert.ToString(value);
                }
            }

            public System.String EmrDiagnoseName
            {
                get
                {
                    System.String data = entity.EmrDiagnoseName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.EmrDiagnoseName = null;
                    else entity.EmrDiagnoseName = Convert.ToString(value);
                }
            }

            public System.String SREmrDiagnoseGroupID
            {
                get
                {
                    System.String data = entity.SREmrDiagnoseGroupID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SREmrDiagnoseGroupID = null;
                    else entity.SREmrDiagnoseGroupID = Convert.ToString(value);
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


            private esEmergencyDiagnose entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esEmergencyDiagnoseQuery query)
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
                throw new Exception("esEmergencyDiagnose can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    public partial class EmergencyDiagnose : esEmergencyDiagnose
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
    abstract public class esEmergencyDiagnoseQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return EmergencyDiagnoseMetadata.Meta();
            }
        }


        public esQueryItem EmrDiagnoseID
        {
            get
            {
                return new esQueryItem(this, EmergencyDiagnoseMetadata.ColumnNames.EmrDiagnoseID, esSystemType.String);
            }
        }

        public esQueryItem EmrDiagnoseName
        {
            get
            {
                return new esQueryItem(this, EmergencyDiagnoseMetadata.ColumnNames.EmrDiagnoseName, esSystemType.String);
            }
        }

        public esQueryItem SREmrDiagnoseGroupID
        {
            get
            {
                return new esQueryItem(this, EmergencyDiagnoseMetadata.ColumnNames.SREmrDiagnoseGroupID, esSystemType.String);
            }
        }

        public esQueryItem IsActive
        {
            get
            {
                return new esQueryItem(this, EmergencyDiagnoseMetadata.ColumnNames.IsActive, esSystemType.Boolean);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, EmergencyDiagnoseMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, EmergencyDiagnoseMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("EmergencyDiagnoseCollection")]
    public partial class EmergencyDiagnoseCollection : esEmergencyDiagnoseCollection, IEnumerable<EmergencyDiagnose>
    {
        public EmergencyDiagnoseCollection()
        {

        }

        public static implicit operator List<EmergencyDiagnose>(EmergencyDiagnoseCollection coll)
        {
            List<EmergencyDiagnose> list = new List<EmergencyDiagnose>();

            foreach (EmergencyDiagnose emp in coll)
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
                return EmergencyDiagnoseMetadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new EmergencyDiagnoseQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new EmergencyDiagnose(row);
        }

        override protected esEntity CreateEntity()
        {
            return new EmergencyDiagnose();
        }


        #endregion


        [BrowsableAttribute(false)]
        public EmergencyDiagnoseQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new EmergencyDiagnoseQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(EmergencyDiagnoseQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public EmergencyDiagnose AddNew()
        {
            EmergencyDiagnose entity = base.AddNewEntity() as EmergencyDiagnose;

            return entity;
        }

        public EmergencyDiagnose FindByPrimaryKey(System.String emrDiagnoseID)
        {
            return base.FindByPrimaryKey(emrDiagnoseID) as EmergencyDiagnose;
        }


        #region IEnumerable<EmergencyDiagnose> Members

        IEnumerator<EmergencyDiagnose> IEnumerable<EmergencyDiagnose>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as EmergencyDiagnose;
            }
        }

        #endregion

        private EmergencyDiagnoseQuery query;
    }


    /// <summary>
    /// Encapsulates the 'EmergencyDiagnose' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("EmergencyDiagnose ({EmrDiagnoseID})")]
    [Serializable]
    public partial class EmergencyDiagnose : esEmergencyDiagnose
    {
        public EmergencyDiagnose()
        {

        }

        public EmergencyDiagnose(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return EmergencyDiagnoseMetadata.Meta();
            }
        }



        override protected esEmergencyDiagnoseQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new EmergencyDiagnoseQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public EmergencyDiagnoseQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new EmergencyDiagnoseQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(EmergencyDiagnoseQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private EmergencyDiagnoseQuery query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class EmergencyDiagnoseQuery : esEmergencyDiagnoseQuery
    {
        public EmergencyDiagnoseQuery()
        {

        }

        public EmergencyDiagnoseQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "EmergencyDiagnoseQuery";
        }


    }


    [Serializable]
    public partial class EmergencyDiagnoseMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected EmergencyDiagnoseMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(EmergencyDiagnoseMetadata.ColumnNames.EmrDiagnoseID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = EmergencyDiagnoseMetadata.PropertyNames.EmrDiagnoseID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(EmergencyDiagnoseMetadata.ColumnNames.EmrDiagnoseName, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = EmergencyDiagnoseMetadata.PropertyNames.EmrDiagnoseName;
            c.CharacterMaxLength = 500;
            _columns.Add(c);

            c = new esColumnMetadata(EmergencyDiagnoseMetadata.ColumnNames.SREmrDiagnoseGroupID, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = EmergencyDiagnoseMetadata.PropertyNames.SREmrDiagnoseGroupID;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(EmergencyDiagnoseMetadata.ColumnNames.IsActive, 3, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = EmergencyDiagnoseMetadata.PropertyNames.IsActive;
            _columns.Add(c);

            c = new esColumnMetadata(EmergencyDiagnoseMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = EmergencyDiagnoseMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(EmergencyDiagnoseMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = EmergencyDiagnoseMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public EmergencyDiagnoseMetadata Meta()
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
            public const string EmrDiagnoseID = "EmrDiagnoseID";
            public const string EmrDiagnoseName = "EmrDiagnoseName";
            public const string SREmrDiagnoseGroupID = "SREmrDiagnoseGroupID";
            public const string IsActive = "IsActive";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string EmrDiagnoseID = "EmrDiagnoseID";
            public const string EmrDiagnoseName = "EmrDiagnoseName";
            public const string SREmrDiagnoseGroupID = "SREmrDiagnoseGroupID";
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
            lock (typeof(EmergencyDiagnoseMetadata))
            {
                if (EmergencyDiagnoseMetadata.mapDelegates == null)
                {
                    EmergencyDiagnoseMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (EmergencyDiagnoseMetadata.meta == null)
                {
                    EmergencyDiagnoseMetadata.meta = new EmergencyDiagnoseMetadata();
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


                meta.AddTypeMap("EmrDiagnoseID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("EmrDiagnoseName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SREmrDiagnoseGroupID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));



                meta.Source = "EmergencyDiagnose";
                meta.Destination = "EmergencyDiagnose";

                meta.spInsert = "proc_EmergencyDiagnoseInsert";
                meta.spUpdate = "proc_EmergencyDiagnoseUpdate";
                meta.spDelete = "proc_EmergencyDiagnoseDelete";
                meta.spLoadAll = "proc_EmergencyDiagnoseLoadAll";
                meta.spLoadByPrimaryKey = "proc_EmergencyDiagnoseLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private EmergencyDiagnoseMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
