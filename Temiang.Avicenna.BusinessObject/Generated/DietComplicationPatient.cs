/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 4/8/2014 11:19:36 AM
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
    abstract public class esDietComplicationPatientCollection : esEntityCollectionWAuditLog
    {
        public esDietComplicationPatientCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "DietComplicationPatientCollection";
        }

        #region Query Logic
        protected void InitQuery(esDietComplicationPatientQuery query)
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
            this.InitQuery(query as esDietComplicationPatientQuery);
        }
        #endregion

        virtual public DietComplicationPatient DetachEntity(DietComplicationPatient entity)
        {
            return base.DetachEntity(entity) as DietComplicationPatient;
        }

        virtual public DietComplicationPatient AttachEntity(DietComplicationPatient entity)
        {
            return base.AttachEntity(entity) as DietComplicationPatient;
        }

        virtual public void Combine(DietComplicationPatientCollection collection)
        {
            base.Combine(collection);
        }

        new public DietComplicationPatient this[int index]
        {
            get
            {
                return base[index] as DietComplicationPatient;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(DietComplicationPatient);
        }
    }



    [Serializable]
    abstract public class esDietComplicationPatient : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esDietComplicationPatientQuery GetDynamicQuery()
        {
            return null;
        }

        public esDietComplicationPatient()
        {

        }

        public esDietComplicationPatient(DataRow row)
            : base(row)
        {

        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(System.String transactionNo, System.String dietID, System.String dietComplicationID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(transactionNo, dietID, dietComplicationID);
            else
                return LoadByPrimaryKeyStoredProcedure(transactionNo, dietID, dietComplicationID);
        }

        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String transactionNo, System.String dietID, System.String dietComplicationID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(transactionNo, dietID, dietComplicationID);
            else
                return LoadByPrimaryKeyStoredProcedure(transactionNo, dietID, dietComplicationID);
        }

        private bool LoadByPrimaryKeyDynamic(System.String transactionNo, System.String dietID, System.String dietComplicationID)
        {
            esDietComplicationPatientQuery query = this.GetDynamicQuery();
            query.Where(query.TransactionNo == transactionNo, query.DietID == dietID, query.DietComplicationID == dietComplicationID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(System.String transactionNo, System.String dietID, System.String dietComplicationID)
        {
            esParameters parms = new esParameters();
            parms.Add("TransactionNo", transactionNo); parms.Add("DietID", dietID); parms.Add("DietComplicationID", dietComplicationID);
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
                        case "TransactionNo": this.str.TransactionNo = (string)value; break;
                        case "DietID": this.str.DietID = (string)value; break;
                        case "DietComplicationID": this.str.DietComplicationID = (string)value; break;
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
        /// Maps to DietComplicationPatient.TransactionNo
        /// </summary>
        virtual public System.String TransactionNo
        {
            get
            {
                return base.GetSystemString(DietComplicationPatientMetadata.ColumnNames.TransactionNo);
            }

            set
            {
                base.SetSystemString(DietComplicationPatientMetadata.ColumnNames.TransactionNo, value);
            }
        }

        /// <summary>
        /// Maps to DietComplicationPatient.DietID
        /// </summary>
        virtual public System.String DietID
        {
            get
            {
                return base.GetSystemString(DietComplicationPatientMetadata.ColumnNames.DietID);
            }

            set
            {
                base.SetSystemString(DietComplicationPatientMetadata.ColumnNames.DietID, value);
            }
        }

        /// <summary>
        /// Maps to DietComplicationPatient.DietComplicationID
        /// </summary>
        virtual public System.String DietComplicationID
        {
            get
            {
                return base.GetSystemString(DietComplicationPatientMetadata.ColumnNames.DietComplicationID);
            }

            set
            {
                base.SetSystemString(DietComplicationPatientMetadata.ColumnNames.DietComplicationID, value);
            }
        }

        /// <summary>
        /// Maps to DietComplicationPatient.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(DietComplicationPatientMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(DietComplicationPatientMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to DietComplicationPatient.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(DietComplicationPatientMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(DietComplicationPatientMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esDietComplicationPatient entity)
            {
                this.entity = entity;
            }


            public System.String TransactionNo
            {
                get
                {
                    System.String data = entity.TransactionNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TransactionNo = null;
                    else entity.TransactionNo = Convert.ToString(value);
                }
            }

            public System.String DietID
            {
                get
                {
                    System.String data = entity.DietID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DietID = null;
                    else entity.DietID = Convert.ToString(value);
                }
            }

            public System.String DietComplicationID
            {
                get
                {
                    System.String data = entity.DietComplicationID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DietComplicationID = null;
                    else entity.DietComplicationID = Convert.ToString(value);
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


            private esDietComplicationPatient entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esDietComplicationPatientQuery query)
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
                throw new Exception("esDietComplicationPatient can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    public partial class DietComplicationPatient : esDietComplicationPatient
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
    abstract public class esDietComplicationPatientQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return DietComplicationPatientMetadata.Meta();
            }
        }


        public esQueryItem TransactionNo
        {
            get
            {
                return new esQueryItem(this, DietComplicationPatientMetadata.ColumnNames.TransactionNo, esSystemType.String);
            }
        }

        public esQueryItem DietID
        {
            get
            {
                return new esQueryItem(this, DietComplicationPatientMetadata.ColumnNames.DietID, esSystemType.String);
            }
        }

        public esQueryItem DietComplicationID
        {
            get
            {
                return new esQueryItem(this, DietComplicationPatientMetadata.ColumnNames.DietComplicationID, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, DietComplicationPatientMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, DietComplicationPatientMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("DietComplicationPatientCollection")]
    public partial class DietComplicationPatientCollection : esDietComplicationPatientCollection, IEnumerable<DietComplicationPatient>
    {
        public DietComplicationPatientCollection()
        {

        }

        public static implicit operator List<DietComplicationPatient>(DietComplicationPatientCollection coll)
        {
            List<DietComplicationPatient> list = new List<DietComplicationPatient>();

            foreach (DietComplicationPatient emp in coll)
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
                return DietComplicationPatientMetadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new DietComplicationPatientQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new DietComplicationPatient(row);
        }

        override protected esEntity CreateEntity()
        {
            return new DietComplicationPatient();
        }


        #endregion


        [BrowsableAttribute(false)]
        public DietComplicationPatientQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new DietComplicationPatientQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(DietComplicationPatientQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public DietComplicationPatient AddNew()
        {
            DietComplicationPatient entity = base.AddNewEntity() as DietComplicationPatient;

            return entity;
        }

        public DietComplicationPatient FindByPrimaryKey(System.String transactionNo, System.String dietID, System.String dietComplicationID)
        {
            return base.FindByPrimaryKey(transactionNo, dietID, dietComplicationID) as DietComplicationPatient;
        }


        #region IEnumerable<DietComplicationPatient> Members

        IEnumerator<DietComplicationPatient> IEnumerable<DietComplicationPatient>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as DietComplicationPatient;
            }
        }

        #endregion

        private DietComplicationPatientQuery query;
    }


    /// <summary>
    /// Encapsulates the 'DietComplicationPatient' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("DietComplicationPatient ({TransactionNo},{DietID},{DietComplicationID})")]
    [Serializable]
    public partial class DietComplicationPatient : esDietComplicationPatient
    {
        public DietComplicationPatient()
        {

        }

        public DietComplicationPatient(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return DietComplicationPatientMetadata.Meta();
            }
        }



        override protected esDietComplicationPatientQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new DietComplicationPatientQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public DietComplicationPatientQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new DietComplicationPatientQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(DietComplicationPatientQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private DietComplicationPatientQuery query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class DietComplicationPatientQuery : esDietComplicationPatientQuery
    {
        public DietComplicationPatientQuery()
        {

        }

        public DietComplicationPatientQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "DietComplicationPatientQuery";
        }


    }


    [Serializable]
    public partial class DietComplicationPatientMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected DietComplicationPatientMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(DietComplicationPatientMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = DietComplicationPatientMetadata.PropertyNames.TransactionNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(DietComplicationPatientMetadata.ColumnNames.DietID, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = DietComplicationPatientMetadata.PropertyNames.DietID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(DietComplicationPatientMetadata.ColumnNames.DietComplicationID, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = DietComplicationPatientMetadata.PropertyNames.DietComplicationID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(DietComplicationPatientMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = DietComplicationPatientMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(DietComplicationPatientMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = DietComplicationPatientMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public DietComplicationPatientMetadata Meta()
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
            public const string TransactionNo = "TransactionNo";
            public const string DietID = "DietID";
            public const string DietComplicationID = "DietComplicationID";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string TransactionNo = "TransactionNo";
            public const string DietID = "DietID";
            public const string DietComplicationID = "DietComplicationID";
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
            lock (typeof(DietComplicationPatientMetadata))
            {
                if (DietComplicationPatientMetadata.mapDelegates == null)
                {
                    DietComplicationPatientMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (DietComplicationPatientMetadata.meta == null)
                {
                    DietComplicationPatientMetadata.meta = new DietComplicationPatientMetadata();
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


                meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("DietID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("DietComplicationID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));



                meta.Source = "DietComplicationPatient";
                meta.Destination = "DietComplicationPatient";

                meta.spInsert = "proc_DietComplicationPatientInsert";
                meta.spUpdate = "proc_DietComplicationPatientUpdate";
                meta.spDelete = "proc_DietComplicationPatientDelete";
                meta.spLoadAll = "proc_DietComplicationPatientLoadAll";
                meta.spLoadByPrimaryKey = "proc_DietComplicationPatientLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private DietComplicationPatientMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
