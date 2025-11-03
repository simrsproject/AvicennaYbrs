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
    abstract public class esRegistrationPathwayItemExecutionCollection : esEntityCollectionWAuditLog
    {
        public esRegistrationPathwayItemExecutionCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "RegistrationPathwayItemExecutionCollection";
        }

        #region Query Logic
        protected void InitQuery(esRegistrationPathwayItemExecutionQuery query)
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
            this.InitQuery(query as esRegistrationPathwayItemExecutionQuery);
        }
        #endregion

        virtual public RegistrationPathwayItemExecution DetachEntity(RegistrationPathwayItemExecution entity)
        {
            return base.DetachEntity(entity) as RegistrationPathwayItemExecution;
        }

        virtual public RegistrationPathwayItemExecution AttachEntity(RegistrationPathwayItemExecution entity)
        {
            return base.AttachEntity(entity) as RegistrationPathwayItemExecution;
        }

        virtual public void Combine(RegistrationPathwayItemExecutionCollection collection)
        {
            base.Combine(collection);
        }

        new public RegistrationPathwayItemExecution this[int index]
        {
            get
            {
                return base[index] as RegistrationPathwayItemExecution;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(RegistrationPathwayItemExecution);
        }
    }



    [Serializable]
    abstract public class esRegistrationPathwayItemExecution : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esRegistrationPathwayItemExecutionQuery GetDynamicQuery()
        {
            return null;
        }

        public esRegistrationPathwayItemExecution()
        {

        }

        public esRegistrationPathwayItemExecution(DataRow row)
            : base(row)
        {

        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(System.Int32 dayNo, System.String pathwayID, System.Int32 pathwayItemSeqNo, System.String registrationNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(dayNo, pathwayID, pathwayItemSeqNo, registrationNo);
            else
                return LoadByPrimaryKeyStoredProcedure(dayNo, pathwayID, pathwayItemSeqNo, registrationNo);
        }

        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 dayNo, System.String pathwayID, System.Int32 pathwayItemSeqNo, System.String registrationNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(dayNo, pathwayID, pathwayItemSeqNo, registrationNo);
            else
                return LoadByPrimaryKeyStoredProcedure(dayNo, pathwayID, pathwayItemSeqNo, registrationNo);
        }

        private bool LoadByPrimaryKeyDynamic(System.Int32 dayNo, System.String pathwayID, System.Int32 pathwayItemSeqNo, System.String registrationNo)
        {
            esRegistrationPathwayItemExecutionQuery query = this.GetDynamicQuery();
            query.Where(query.DayNo == dayNo, query.PathwayID == pathwayID, query.PathwayItemSeqNo == pathwayItemSeqNo, query.RegistrationNo == registrationNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(System.Int32 dayNo, System.String pathwayID, System.Int32 pathwayItemSeqNo, System.String registrationNo)
        {
            esParameters parms = new esParameters();
            parms.Add("DayNo", dayNo); parms.Add("PathwayID", pathwayID); parms.Add("PathwayItemSeqNo", pathwayItemSeqNo); parms.Add("RegistrationNo", registrationNo);
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
                        case "DayNo": this.str.DayNo = (string)value; break;
                        case "IsApprove": this.str.IsApprove = (string)value; break;
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

                        case "DayNo":

                            if (value == null || value is System.Int32)
                                this.DayNo = (System.Int32?)value;
                            break;

                        case "IsApprove":

                            if (value == null || value is System.Boolean)
                                this.IsApprove = (System.Boolean?)value;
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
        /// Maps to RegistrationPathwayItemExecution.RegistrationNo
        /// </summary>
        virtual public System.String RegistrationNo
        {
            get
            {
                return base.GetSystemString(RegistrationPathwayItemExecutionMetadata.ColumnNames.RegistrationNo);
            }

            set
            {
                base.SetSystemString(RegistrationPathwayItemExecutionMetadata.ColumnNames.RegistrationNo, value);
            }
        }

        /// <summary>
        /// Maps to RegistrationPathwayItemExecution.PathwayID
        /// </summary>
        virtual public System.String PathwayID
        {
            get
            {
                return base.GetSystemString(RegistrationPathwayItemExecutionMetadata.ColumnNames.PathwayID);
            }

            set
            {
                base.SetSystemString(RegistrationPathwayItemExecutionMetadata.ColumnNames.PathwayID, value);
            }
        }

        /// <summary>
        /// Maps to RegistrationPathwayItemExecution.PathwayItemSeqNo
        /// </summary>
        virtual public System.Int32? PathwayItemSeqNo
        {
            get
            {
                return base.GetSystemInt32(RegistrationPathwayItemExecutionMetadata.ColumnNames.PathwayItemSeqNo);
            }

            set
            {
                base.SetSystemInt32(RegistrationPathwayItemExecutionMetadata.ColumnNames.PathwayItemSeqNo, value);
            }
        }

        /// <summary>
        /// Maps to RegistrationPathwayItemExecution.DayNo
        /// </summary>
        virtual public System.Int32? DayNo
        {
            get
            {
                return base.GetSystemInt32(RegistrationPathwayItemExecutionMetadata.ColumnNames.DayNo);
            }

            set
            {
                base.SetSystemInt32(RegistrationPathwayItemExecutionMetadata.ColumnNames.DayNo, value);
            }
        }

        /// <summary>
        /// Maps to RegistrationPathwayItemExecution.IsApprove
        /// </summary>
        virtual public System.Boolean? IsApprove
        {
            get
            {
                return base.GetSystemBoolean(RegistrationPathwayItemExecutionMetadata.ColumnNames.IsApprove);
            }

            set
            {
                base.SetSystemBoolean(RegistrationPathwayItemExecutionMetadata.ColumnNames.IsApprove, value);
            }
        }

        /// <summary>
        /// Maps to RegistrationPathwayItemExecution.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(RegistrationPathwayItemExecutionMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(RegistrationPathwayItemExecutionMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to RegistrationPathwayItemExecution.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(RegistrationPathwayItemExecutionMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(RegistrationPathwayItemExecutionMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esRegistrationPathwayItemExecution entity)
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

            public System.String DayNo
            {
                get
                {
                    System.Int32? data = entity.DayNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DayNo = null;
                    else entity.DayNo = Convert.ToInt32(value);
                }
            }

            public System.String IsApprove
            {
                get
                {
                    System.Boolean? data = entity.IsApprove;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsApprove = null;
                    else entity.IsApprove = Convert.ToBoolean(value);
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


            private esRegistrationPathwayItemExecution entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esRegistrationPathwayItemExecutionQuery query)
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
                throw new Exception("esRegistrationPathwayItemExecution can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    [Serializable]
    abstract public class esRegistrationPathwayItemExecutionQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return RegistrationPathwayItemExecutionMetadata.Meta();
            }
        }


        public esQueryItem RegistrationNo
        {
            get
            {
                return new esQueryItem(this, RegistrationPathwayItemExecutionMetadata.ColumnNames.RegistrationNo, esSystemType.String);
            }
        }

        public esQueryItem PathwayID
        {
            get
            {
                return new esQueryItem(this, RegistrationPathwayItemExecutionMetadata.ColumnNames.PathwayID, esSystemType.String);
            }
        }

        public esQueryItem PathwayItemSeqNo
        {
            get
            {
                return new esQueryItem(this, RegistrationPathwayItemExecutionMetadata.ColumnNames.PathwayItemSeqNo, esSystemType.Int32);
            }
        }

        public esQueryItem DayNo
        {
            get
            {
                return new esQueryItem(this, RegistrationPathwayItemExecutionMetadata.ColumnNames.DayNo, esSystemType.Int32);
            }
        }

        public esQueryItem IsApprove
        {
            get
            {
                return new esQueryItem(this, RegistrationPathwayItemExecutionMetadata.ColumnNames.IsApprove, esSystemType.Boolean);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, RegistrationPathwayItemExecutionMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, RegistrationPathwayItemExecutionMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("RegistrationPathwayItemExecutionCollection")]
    public partial class RegistrationPathwayItemExecutionCollection : esRegistrationPathwayItemExecutionCollection, IEnumerable<RegistrationPathwayItemExecution>
    {
        public RegistrationPathwayItemExecutionCollection()
        {

        }

        public static implicit operator List<RegistrationPathwayItemExecution>(RegistrationPathwayItemExecutionCollection coll)
        {
            List<RegistrationPathwayItemExecution> list = new List<RegistrationPathwayItemExecution>();

            foreach (RegistrationPathwayItemExecution emp in coll)
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
                return RegistrationPathwayItemExecutionMetadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new RegistrationPathwayItemExecutionQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new RegistrationPathwayItemExecution(row);
        }

        override protected esEntity CreateEntity()
        {
            return new RegistrationPathwayItemExecution();
        }


        #endregion


        [BrowsableAttribute(false)]
        public RegistrationPathwayItemExecutionQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new RegistrationPathwayItemExecutionQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(RegistrationPathwayItemExecutionQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public RegistrationPathwayItemExecution AddNew()
        {
            RegistrationPathwayItemExecution entity = base.AddNewEntity() as RegistrationPathwayItemExecution;

            return entity;
        }

        public RegistrationPathwayItemExecution FindByPrimaryKey(System.Int32 dayNo, System.String pathwayID, System.Int32 pathwayItemSeqNo, System.String registrationNo)
        {
            return base.FindByPrimaryKey(dayNo, pathwayID, pathwayItemSeqNo, registrationNo) as RegistrationPathwayItemExecution;
        }


        #region IEnumerable<RegistrationPathwayItemExecution> Members

        IEnumerator<RegistrationPathwayItemExecution> IEnumerable<RegistrationPathwayItemExecution>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as RegistrationPathwayItemExecution;
            }
        }

        #endregion

        private RegistrationPathwayItemExecutionQuery query;
    }


    /// <summary>
    /// Encapsulates the 'RegistrationPathwayItemExecution' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("RegistrationPathwayItemExecution ({RegistrationNo},{PathwayID},{PathwayItemSeqNo},{DayNo})")]
    [Serializable]
    public partial class RegistrationPathwayItemExecution : esRegistrationPathwayItemExecution
    {
        public RegistrationPathwayItemExecution()
        {

        }

        public RegistrationPathwayItemExecution(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return RegistrationPathwayItemExecutionMetadata.Meta();
            }
        }



        override protected esRegistrationPathwayItemExecutionQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new RegistrationPathwayItemExecutionQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public RegistrationPathwayItemExecutionQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new RegistrationPathwayItemExecutionQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(RegistrationPathwayItemExecutionQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private RegistrationPathwayItemExecutionQuery query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class RegistrationPathwayItemExecutionQuery : esRegistrationPathwayItemExecutionQuery
    {
        public RegistrationPathwayItemExecutionQuery()
        {

        }

        public RegistrationPathwayItemExecutionQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "RegistrationPathwayItemExecutionQuery";
        }


    }


    [Serializable]
    public partial class RegistrationPathwayItemExecutionMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected RegistrationPathwayItemExecutionMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(RegistrationPathwayItemExecutionMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = RegistrationPathwayItemExecutionMetadata.PropertyNames.RegistrationNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationPathwayItemExecutionMetadata.ColumnNames.PathwayID, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = RegistrationPathwayItemExecutionMetadata.PropertyNames.PathwayID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationPathwayItemExecutionMetadata.ColumnNames.PathwayItemSeqNo, 2, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RegistrationPathwayItemExecutionMetadata.PropertyNames.PathwayItemSeqNo;
            c.IsInPrimaryKey = true;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationPathwayItemExecutionMetadata.ColumnNames.DayNo, 3, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RegistrationPathwayItemExecutionMetadata.PropertyNames.DayNo;
            c.IsInPrimaryKey = true;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationPathwayItemExecutionMetadata.ColumnNames.IsApprove, 4, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = RegistrationPathwayItemExecutionMetadata.PropertyNames.IsApprove;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationPathwayItemExecutionMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = RegistrationPathwayItemExecutionMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationPathwayItemExecutionMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = RegistrationPathwayItemExecutionMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public RegistrationPathwayItemExecutionMetadata Meta()
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
            public const string DayNo = "DayNo";
            public const string IsApprove = "IsApprove";
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
            public const string DayNo = "DayNo";
            public const string IsApprove = "IsApprove";
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
            lock (typeof(RegistrationPathwayItemExecutionMetadata))
            {
                if (RegistrationPathwayItemExecutionMetadata.mapDelegates == null)
                {
                    RegistrationPathwayItemExecutionMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (RegistrationPathwayItemExecutionMetadata.meta == null)
                {
                    RegistrationPathwayItemExecutionMetadata.meta = new RegistrationPathwayItemExecutionMetadata();
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
                meta.AddTypeMap("DayNo", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("IsApprove", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));



                meta.Source = "RegistrationPathwayItemExecution";
                meta.Destination = "RegistrationPathwayItemExecution";

                meta.spInsert = "proc_RegistrationPathwayItemExecutionInsert";
                meta.spUpdate = "proc_RegistrationPathwayItemExecutionUpdate";
                meta.spDelete = "proc_RegistrationPathwayItemExecutionDelete";
                meta.spLoadAll = "proc_RegistrationPathwayItemExecutionLoadAll";
                meta.spLoadByPrimaryKey = "proc_RegistrationPathwayItemExecutionLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private RegistrationPathwayItemExecutionMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
