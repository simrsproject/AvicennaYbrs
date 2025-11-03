/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/20/2013 9:52:56 AM
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
    abstract public class esSnackOrderCollection : esEntityCollectionWAuditLog
    {
        public esSnackOrderCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "SnackOrderCollection";
        }

        #region Query Logic
        protected void InitQuery(esSnackOrderQuery query)
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
            this.InitQuery(query as esSnackOrderQuery);
        }
        #endregion

        virtual public SnackOrder DetachEntity(SnackOrder entity)
        {
            return base.DetachEntity(entity) as SnackOrder;
        }

        virtual public SnackOrder AttachEntity(SnackOrder entity)
        {
            return base.AttachEntity(entity) as SnackOrder;
        }

        virtual public void Combine(SnackOrderCollection collection)
        {
            base.Combine(collection);
        }

        new public SnackOrder this[int index]
        {
            get
            {
                return base[index] as SnackOrder;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(SnackOrder);
        }
    }



    [Serializable]
    abstract public class esSnackOrder : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esSnackOrderQuery GetDynamicQuery()
        {
            return null;
        }

        public esSnackOrder()
        {

        }

        public esSnackOrder(DataRow row)
            : base(row)
        {

        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(System.String snackOrderNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(snackOrderNo);
            else
                return LoadByPrimaryKeyStoredProcedure(snackOrderNo);
        }

        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String snackOrderNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(snackOrderNo);
            else
                return LoadByPrimaryKeyStoredProcedure(snackOrderNo);
        }

        private bool LoadByPrimaryKeyDynamic(System.String snackOrderNo)
        {
            esSnackOrderQuery query = this.GetDynamicQuery();
            query.Where(query.SnackOrderNo == snackOrderNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(System.String snackOrderNo)
        {
            esParameters parms = new esParameters();
            parms.Add("SnackOrderNo", snackOrderNo);
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
                        case "SnackOrderNo": this.str.SnackOrderNo = (string)value; break;
                        case "SnackOrderDate": this.str.SnackOrderDate = (string)value; break;
                        case "SnackOrderForDate": this.str.SnackOrderForDate = (string)value; break;
                        case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
                        case "Notes": this.str.Notes = (string)value; break;
                        case "IsApproved": this.str.IsApproved = (string)value; break;
                        case "IsVoid": this.str.IsVoid = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "SnackOrderDate":

                            if (value == null || value is System.DateTime)
                                this.SnackOrderDate = (System.DateTime?)value;
                            break;

                        case "SnackOrderForDate":

                            if (value == null || value is System.DateTime)
                                this.SnackOrderForDate = (System.DateTime?)value;
                            break;

                        case "IsApproved":

                            if (value == null || value is System.Boolean)
                                this.IsApproved = (System.Boolean?)value;
                            break;

                        case "IsVoid":

                            if (value == null || value is System.Boolean)
                                this.IsVoid = (System.Boolean?)value;
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
        /// Maps to SnackOrder.SnackOrderNo
        /// </summary>
        virtual public System.String SnackOrderNo
        {
            get
            {
                return base.GetSystemString(SnackOrderMetadata.ColumnNames.SnackOrderNo);
            }

            set
            {
                base.SetSystemString(SnackOrderMetadata.ColumnNames.SnackOrderNo, value);
            }
        }

        /// <summary>
        /// Maps to SnackOrder.SnackOrderDate
        /// </summary>
        virtual public System.DateTime? SnackOrderDate
        {
            get
            {
                return base.GetSystemDateTime(SnackOrderMetadata.ColumnNames.SnackOrderDate);
            }

            set
            {
                base.SetSystemDateTime(SnackOrderMetadata.ColumnNames.SnackOrderDate, value);
            }
        }

        /// <summary>
        /// Maps to SnackOrder.SnackOrderForDate
        /// </summary>
        virtual public System.DateTime? SnackOrderForDate
        {
            get
            {
                return base.GetSystemDateTime(SnackOrderMetadata.ColumnNames.SnackOrderForDate);
            }

            set
            {
                base.SetSystemDateTime(SnackOrderMetadata.ColumnNames.SnackOrderForDate, value);
            }
        }

        /// <summary>
        /// Maps to SnackOrder.ServiceUnitID
        /// </summary>
        virtual public System.String ServiceUnitID
        {
            get
            {
                return base.GetSystemString(SnackOrderMetadata.ColumnNames.ServiceUnitID);
            }

            set
            {
                base.SetSystemString(SnackOrderMetadata.ColumnNames.ServiceUnitID, value);
            }
        }

        /// <summary>
        /// Maps to SnackOrder.Notes
        /// </summary>
        virtual public System.String Notes
        {
            get
            {
                return base.GetSystemString(SnackOrderMetadata.ColumnNames.Notes);
            }

            set
            {
                base.SetSystemString(SnackOrderMetadata.ColumnNames.Notes, value);
            }
        }

        /// <summary>
        /// Maps to SnackOrder.IsApproved
        /// </summary>
        virtual public System.Boolean? IsApproved
        {
            get
            {
                return base.GetSystemBoolean(SnackOrderMetadata.ColumnNames.IsApproved);
            }

            set
            {
                base.SetSystemBoolean(SnackOrderMetadata.ColumnNames.IsApproved, value);
            }
        }

        /// <summary>
        /// Maps to SnackOrder.IsVoid
        /// </summary>
        virtual public System.Boolean? IsVoid
        {
            get
            {
                return base.GetSystemBoolean(SnackOrderMetadata.ColumnNames.IsVoid);
            }

            set
            {
                base.SetSystemBoolean(SnackOrderMetadata.ColumnNames.IsVoid, value);
            }
        }

        /// <summary>
        /// Maps to SnackOrder.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(SnackOrderMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(SnackOrderMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to SnackOrder.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(SnackOrderMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(SnackOrderMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esSnackOrder entity)
            {
                this.entity = entity;
            }


            public System.String SnackOrderNo
            {
                get
                {
                    System.String data = entity.SnackOrderNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SnackOrderNo = null;
                    else entity.SnackOrderNo = Convert.ToString(value);
                }
            }

            public System.String SnackOrderDate
            {
                get
                {
                    System.DateTime? data = entity.SnackOrderDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SnackOrderDate = null;
                    else entity.SnackOrderDate = Convert.ToDateTime(value);
                }
            }

            public System.String SnackOrderForDate
            {
                get
                {
                    System.DateTime? data = entity.SnackOrderForDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SnackOrderForDate = null;
                    else entity.SnackOrderForDate = Convert.ToDateTime(value);
                }
            }

            public System.String ServiceUnitID
            {
                get
                {
                    System.String data = entity.ServiceUnitID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ServiceUnitID = null;
                    else entity.ServiceUnitID = Convert.ToString(value);
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

            public System.String IsApproved
            {
                get
                {
                    System.Boolean? data = entity.IsApproved;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsApproved = null;
                    else entity.IsApproved = Convert.ToBoolean(value);
                }
            }

            public System.String IsVoid
            {
                get
                {
                    System.Boolean? data = entity.IsVoid;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsVoid = null;
                    else entity.IsVoid = Convert.ToBoolean(value);
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


            private esSnackOrder entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esSnackOrderQuery query)
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
                throw new Exception("esSnackOrder can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    public partial class SnackOrder : esSnackOrder
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
    abstract public class esSnackOrderQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return SnackOrderMetadata.Meta();
            }
        }


        public esQueryItem SnackOrderNo
        {
            get
            {
                return new esQueryItem(this, SnackOrderMetadata.ColumnNames.SnackOrderNo, esSystemType.String);
            }
        }

        public esQueryItem SnackOrderDate
        {
            get
            {
                return new esQueryItem(this, SnackOrderMetadata.ColumnNames.SnackOrderDate, esSystemType.DateTime);
            }
        }

        public esQueryItem SnackOrderForDate
        {
            get
            {
                return new esQueryItem(this, SnackOrderMetadata.ColumnNames.SnackOrderForDate, esSystemType.DateTime);
            }
        }

        public esQueryItem ServiceUnitID
        {
            get
            {
                return new esQueryItem(this, SnackOrderMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
            }
        }

        public esQueryItem Notes
        {
            get
            {
                return new esQueryItem(this, SnackOrderMetadata.ColumnNames.Notes, esSystemType.String);
            }
        }

        public esQueryItem IsApproved
        {
            get
            {
                return new esQueryItem(this, SnackOrderMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
            }
        }

        public esQueryItem IsVoid
        {
            get
            {
                return new esQueryItem(this, SnackOrderMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, SnackOrderMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, SnackOrderMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("SnackOrderCollection")]
    public partial class SnackOrderCollection : esSnackOrderCollection, IEnumerable<SnackOrder>
    {
        public SnackOrderCollection()
        {

        }

        public static implicit operator List<SnackOrder>(SnackOrderCollection coll)
        {
            List<SnackOrder> list = new List<SnackOrder>();

            foreach (SnackOrder emp in coll)
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
                return SnackOrderMetadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new SnackOrderQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new SnackOrder(row);
        }

        override protected esEntity CreateEntity()
        {
            return new SnackOrder();
        }


        #endregion


        [BrowsableAttribute(false)]
        public SnackOrderQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new SnackOrderQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(SnackOrderQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public SnackOrder AddNew()
        {
            SnackOrder entity = base.AddNewEntity() as SnackOrder;

            return entity;
        }

        public SnackOrder FindByPrimaryKey(System.String snackOrderNo)
        {
            return base.FindByPrimaryKey(snackOrderNo) as SnackOrder;
        }


        #region IEnumerable<SnackOrder> Members

        IEnumerator<SnackOrder> IEnumerable<SnackOrder>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as SnackOrder;
            }
        }

        #endregion

        private SnackOrderQuery query;
    }


    /// <summary>
    /// Encapsulates the 'SnackOrder' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("SnackOrder ({SnackOrderNo})")]
    [Serializable]
    public partial class SnackOrder : esSnackOrder
    {
        public SnackOrder()
        {

        }

        public SnackOrder(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return SnackOrderMetadata.Meta();
            }
        }



        override protected esSnackOrderQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new SnackOrderQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public SnackOrderQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new SnackOrderQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(SnackOrderQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private SnackOrderQuery query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class SnackOrderQuery : esSnackOrderQuery
    {
        public SnackOrderQuery()
        {

        }

        public SnackOrderQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "SnackOrderQuery";
        }


    }


    [Serializable]
    public partial class SnackOrderMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected SnackOrderMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(SnackOrderMetadata.ColumnNames.SnackOrderNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = SnackOrderMetadata.PropertyNames.SnackOrderNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(SnackOrderMetadata.ColumnNames.SnackOrderDate, 1, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = SnackOrderMetadata.PropertyNames.SnackOrderDate;
            _columns.Add(c);

            c = new esColumnMetadata(SnackOrderMetadata.ColumnNames.SnackOrderForDate, 2, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = SnackOrderMetadata.PropertyNames.SnackOrderForDate;
            _columns.Add(c);

            c = new esColumnMetadata(SnackOrderMetadata.ColumnNames.ServiceUnitID, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = SnackOrderMetadata.PropertyNames.ServiceUnitID;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(SnackOrderMetadata.ColumnNames.Notes, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = SnackOrderMetadata.PropertyNames.Notes;
            c.CharacterMaxLength = 500;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(SnackOrderMetadata.ColumnNames.IsApproved, 5, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = SnackOrderMetadata.PropertyNames.IsApproved;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(SnackOrderMetadata.ColumnNames.IsVoid, 6, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = SnackOrderMetadata.PropertyNames.IsVoid;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(SnackOrderMetadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = SnackOrderMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(SnackOrderMetadata.ColumnNames.LastUpdateByUserID, 8, typeof(System.String), esSystemType.String);
            c.PropertyName = SnackOrderMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public SnackOrderMetadata Meta()
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
            public const string SnackOrderNo = "SnackOrderNo";
            public const string SnackOrderDate = "SnackOrderDate";
            public const string SnackOrderForDate = "SnackOrderForDate";
            public const string ServiceUnitID = "ServiceUnitID";
            public const string Notes = "Notes";
            public const string IsApproved = "IsApproved";
            public const string IsVoid = "IsVoid";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string SnackOrderNo = "SnackOrderNo";
            public const string SnackOrderDate = "SnackOrderDate";
            public const string SnackOrderForDate = "SnackOrderForDate";
            public const string ServiceUnitID = "ServiceUnitID";
            public const string Notes = "Notes";
            public const string IsApproved = "IsApproved";
            public const string IsVoid = "IsVoid";
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
            lock (typeof(SnackOrderMetadata))
            {
                if (SnackOrderMetadata.mapDelegates == null)
                {
                    SnackOrderMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (SnackOrderMetadata.meta == null)
                {
                    SnackOrderMetadata.meta = new SnackOrderMetadata();
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


                meta.AddTypeMap("SnackOrderNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SnackOrderDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("SnackOrderForDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));



                meta.Source = "SnackOrder";
                meta.Destination = "SnackOrder";

                meta.spInsert = "proc_SnackOrderInsert";
                meta.spUpdate = "proc_SnackOrderUpdate";
                meta.spDelete = "proc_SnackOrderDelete";
                meta.spLoadAll = "proc_SnackOrderLoadAll";
                meta.spLoadByPrimaryKey = "proc_SnackOrderLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private SnackOrderMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
