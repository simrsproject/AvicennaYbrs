/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 6/2/2014 8:43:29 AM
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
    abstract public class esReasonsForTreatmentCollection : esEntityCollectionWAuditLog
    {
        public esReasonsForTreatmentCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "ReasonsForTreatmentCollection";
        }

        #region Query Logic
        protected void InitQuery(esReasonsForTreatmentQuery query)
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
            this.InitQuery(query as esReasonsForTreatmentQuery);
        }
        #endregion

        virtual public ReasonsForTreatment DetachEntity(ReasonsForTreatment entity)
        {
            return base.DetachEntity(entity) as ReasonsForTreatment;
        }

        virtual public ReasonsForTreatment AttachEntity(ReasonsForTreatment entity)
        {
            return base.AttachEntity(entity) as ReasonsForTreatment;
        }

        virtual public void Combine(ReasonsForTreatmentCollection collection)
        {
            base.Combine(collection);
        }

        new public ReasonsForTreatment this[int index]
        {
            get
            {
                return base[index] as ReasonsForTreatment;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(ReasonsForTreatment);
        }
    }



    [Serializable]
    abstract public class esReasonsForTreatment : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esReasonsForTreatmentQuery GetDynamicQuery()
        {
            return null;
        }

        public esReasonsForTreatment()
        {

        }

        public esReasonsForTreatment(DataRow row)
            : base(row)
        {

        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(System.String sRReasonVisit, System.String reasonsForTreatmentID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(sRReasonVisit, reasonsForTreatmentID);
            else
                return LoadByPrimaryKeyStoredProcedure(sRReasonVisit, reasonsForTreatmentID);
        }

        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String sRReasonVisit, System.String reasonsForTreatmentID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(sRReasonVisit, reasonsForTreatmentID);
            else
                return LoadByPrimaryKeyStoredProcedure(sRReasonVisit, reasonsForTreatmentID);
        }

        private bool LoadByPrimaryKeyDynamic(System.String sRReasonVisit, System.String reasonsForTreatmentID)
        {
            esReasonsForTreatmentQuery query = this.GetDynamicQuery();
            query.Where(query.SRReasonVisit == sRReasonVisit, query.ReasonsForTreatmentID == reasonsForTreatmentID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(System.String sRReasonVisit, System.String reasonsForTreatmentID)
        {
            esParameters parms = new esParameters();
            parms.Add("SRReasonVisit", sRReasonVisit); parms.Add("ReasonsForTreatmentID", reasonsForTreatmentID);
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
                        case "SRReasonVisit": this.str.SRReasonVisit = (string)value; break;
                        case "ReasonsForTreatmentID": this.str.ReasonsForTreatmentID = (string)value; break;
                        case "ReasonsForTreatmentName": this.str.ReasonsForTreatmentName = (string)value; break;
                        case "DiagnoseID": this.str.DiagnoseID = (string)value; break;
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
        /// Maps to ReasonsForTreatment.SRReasonVisit
        /// </summary>
        virtual public System.String SRReasonVisit
        {
            get
            {
                return base.GetSystemString(ReasonsForTreatmentMetadata.ColumnNames.SRReasonVisit);
            }

            set
            {
                base.SetSystemString(ReasonsForTreatmentMetadata.ColumnNames.SRReasonVisit, value);
            }
        }

        /// <summary>
        /// Maps to ReasonsForTreatment.ReasonsForTreatmentID
        /// </summary>
        virtual public System.String ReasonsForTreatmentID
        {
            get
            {
                return base.GetSystemString(ReasonsForTreatmentMetadata.ColumnNames.ReasonsForTreatmentID);
            }

            set
            {
                base.SetSystemString(ReasonsForTreatmentMetadata.ColumnNames.ReasonsForTreatmentID, value);
            }
        }

        /// <summary>
        /// Maps to ReasonsForTreatment.ReasonsForTreatmentName
        /// </summary>
        virtual public System.String ReasonsForTreatmentName
        {
            get
            {
                return base.GetSystemString(ReasonsForTreatmentMetadata.ColumnNames.ReasonsForTreatmentName);
            }

            set
            {
                base.SetSystemString(ReasonsForTreatmentMetadata.ColumnNames.ReasonsForTreatmentName, value);
            }
        }

        /// <summary>
        /// Maps to ReasonsForTreatment.DiagnoseID
        /// </summary>
        virtual public System.String DiagnoseID
        {
            get
            {
                return base.GetSystemString(ReasonsForTreatmentMetadata.ColumnNames.DiagnoseID);
            }

            set
            {
                base.SetSystemString(ReasonsForTreatmentMetadata.ColumnNames.DiagnoseID, value);
            }
        }

        /// <summary>
        /// Maps to ReasonsForTreatment.IsActive
        /// </summary>
        virtual public System.Boolean? IsActive
        {
            get
            {
                return base.GetSystemBoolean(ReasonsForTreatmentMetadata.ColumnNames.IsActive);
            }

            set
            {
                base.SetSystemBoolean(ReasonsForTreatmentMetadata.ColumnNames.IsActive, value);
            }
        }

        /// <summary>
        /// Maps to ReasonsForTreatment.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(ReasonsForTreatmentMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(ReasonsForTreatmentMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to ReasonsForTreatment.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(ReasonsForTreatmentMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(ReasonsForTreatmentMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esReasonsForTreatment entity)
            {
                this.entity = entity;
            }


            public System.String SRReasonVisit
            {
                get
                {
                    System.String data = entity.SRReasonVisit;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRReasonVisit = null;
                    else entity.SRReasonVisit = Convert.ToString(value);
                }
            }

            public System.String ReasonsForTreatmentID
            {
                get
                {
                    System.String data = entity.ReasonsForTreatmentID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ReasonsForTreatmentID = null;
                    else entity.ReasonsForTreatmentID = Convert.ToString(value);
                }
            }

            public System.String ReasonsForTreatmentName
            {
                get
                {
                    System.String data = entity.ReasonsForTreatmentName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ReasonsForTreatmentName = null;
                    else entity.ReasonsForTreatmentName = Convert.ToString(value);
                }
            }

            public System.String DiagnoseID
            {
                get
                {
                    System.String data = entity.DiagnoseID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DiagnoseID = null;
                    else entity.DiagnoseID = Convert.ToString(value);
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


            private esReasonsForTreatment entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esReasonsForTreatmentQuery query)
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
                throw new Exception("esReasonsForTreatment can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    public partial class ReasonsForTreatment : esReasonsForTreatment
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
    abstract public class esReasonsForTreatmentQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return ReasonsForTreatmentMetadata.Meta();
            }
        }


        public esQueryItem SRReasonVisit
        {
            get
            {
                return new esQueryItem(this, ReasonsForTreatmentMetadata.ColumnNames.SRReasonVisit, esSystemType.String);
            }
        }

        public esQueryItem ReasonsForTreatmentID
        {
            get
            {
                return new esQueryItem(this, ReasonsForTreatmentMetadata.ColumnNames.ReasonsForTreatmentID, esSystemType.String);
            }
        }

        public esQueryItem ReasonsForTreatmentName
        {
            get
            {
                return new esQueryItem(this, ReasonsForTreatmentMetadata.ColumnNames.ReasonsForTreatmentName, esSystemType.String);
            }
        }

        public esQueryItem DiagnoseID
        {
            get
            {
                return new esQueryItem(this, ReasonsForTreatmentMetadata.ColumnNames.DiagnoseID, esSystemType.String);
            }
        }

        public esQueryItem IsActive
        {
            get
            {
                return new esQueryItem(this, ReasonsForTreatmentMetadata.ColumnNames.IsActive, esSystemType.Boolean);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, ReasonsForTreatmentMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, ReasonsForTreatmentMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("ReasonsForTreatmentCollection")]
    public partial class ReasonsForTreatmentCollection : esReasonsForTreatmentCollection, IEnumerable<ReasonsForTreatment>
    {
        public ReasonsForTreatmentCollection()
        {

        }

        public static implicit operator List<ReasonsForTreatment>(ReasonsForTreatmentCollection coll)
        {
            List<ReasonsForTreatment> list = new List<ReasonsForTreatment>();

            foreach (ReasonsForTreatment emp in coll)
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
                return ReasonsForTreatmentMetadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ReasonsForTreatmentQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new ReasonsForTreatment(row);
        }

        override protected esEntity CreateEntity()
        {
            return new ReasonsForTreatment();
        }


        #endregion


        [BrowsableAttribute(false)]
        public ReasonsForTreatmentQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ReasonsForTreatmentQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(ReasonsForTreatmentQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public ReasonsForTreatment AddNew()
        {
            ReasonsForTreatment entity = base.AddNewEntity() as ReasonsForTreatment;

            return entity;
        }

        public ReasonsForTreatment FindByPrimaryKey(System.String sRReasonVisit, System.String reasonsForTreatmentID)
        {
            return base.FindByPrimaryKey(sRReasonVisit, reasonsForTreatmentID) as ReasonsForTreatment;
        }


        #region IEnumerable<ReasonsForTreatment> Members

        IEnumerator<ReasonsForTreatment> IEnumerable<ReasonsForTreatment>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as ReasonsForTreatment;
            }
        }

        #endregion

        private ReasonsForTreatmentQuery query;
    }


    /// <summary>
    /// Encapsulates the 'ReasonsForTreatment' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("ReasonsForTreatment ({SRReasonVisit},{ReasonsForTreatmentID})")]
    [Serializable]
    public partial class ReasonsForTreatment : esReasonsForTreatment
    {
        public ReasonsForTreatment()
        {

        }

        public ReasonsForTreatment(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return ReasonsForTreatmentMetadata.Meta();
            }
        }



        override protected esReasonsForTreatmentQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ReasonsForTreatmentQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public ReasonsForTreatmentQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ReasonsForTreatmentQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(ReasonsForTreatmentQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private ReasonsForTreatmentQuery query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class ReasonsForTreatmentQuery : esReasonsForTreatmentQuery
    {
        public ReasonsForTreatmentQuery()
        {

        }

        public ReasonsForTreatmentQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "ReasonsForTreatmentQuery";
        }


    }


    [Serializable]
    public partial class ReasonsForTreatmentMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected ReasonsForTreatmentMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(ReasonsForTreatmentMetadata.ColumnNames.SRReasonVisit, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = ReasonsForTreatmentMetadata.PropertyNames.SRReasonVisit;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(ReasonsForTreatmentMetadata.ColumnNames.ReasonsForTreatmentID, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = ReasonsForTreatmentMetadata.PropertyNames.ReasonsForTreatmentID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(ReasonsForTreatmentMetadata.ColumnNames.ReasonsForTreatmentName, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = ReasonsForTreatmentMetadata.PropertyNames.ReasonsForTreatmentName;
            c.CharacterMaxLength = 250;
            _columns.Add(c);

            c = new esColumnMetadata(ReasonsForTreatmentMetadata.ColumnNames.DiagnoseID, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = ReasonsForTreatmentMetadata.PropertyNames.DiagnoseID;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ReasonsForTreatmentMetadata.ColumnNames.IsActive, 4, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ReasonsForTreatmentMetadata.PropertyNames.IsActive;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ReasonsForTreatmentMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ReasonsForTreatmentMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ReasonsForTreatmentMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = ReasonsForTreatmentMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public ReasonsForTreatmentMetadata Meta()
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
            public const string SRReasonVisit = "SRReasonVisit";
            public const string ReasonsForTreatmentID = "ReasonsForTreatmentID";
            public const string ReasonsForTreatmentName = "ReasonsForTreatmentName";
            public const string DiagnoseID = "DiagnoseID";
            public const string IsActive = "IsActive";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string SRReasonVisit = "SRReasonVisit";
            public const string ReasonsForTreatmentID = "ReasonsForTreatmentID";
            public const string ReasonsForTreatmentName = "ReasonsForTreatmentName";
            public const string DiagnoseID = "DiagnoseID";
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
            lock (typeof(ReasonsForTreatmentMetadata))
            {
                if (ReasonsForTreatmentMetadata.mapDelegates == null)
                {
                    ReasonsForTreatmentMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (ReasonsForTreatmentMetadata.meta == null)
                {
                    ReasonsForTreatmentMetadata.meta = new ReasonsForTreatmentMetadata();
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


                meta.AddTypeMap("SRReasonVisit", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ReasonsForTreatmentID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ReasonsForTreatmentName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("DiagnoseID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));



                meta.Source = "ReasonsForTreatment";
                meta.Destination = "ReasonsForTreatment";

                meta.spInsert = "proc_ReasonsForTreatmentInsert";
                meta.spUpdate = "proc_ReasonsForTreatmentUpdate";
                meta.spDelete = "proc_ReasonsForTreatmentDelete";
                meta.spLoadAll = "proc_ReasonsForTreatmentLoadAll";
                meta.spLoadByPrimaryKey = "proc_ReasonsForTreatmentLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private ReasonsForTreatmentMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
