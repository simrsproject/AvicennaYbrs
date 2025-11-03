/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 2/8/2013 9:28:21 AM
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
    abstract public class esClosingWageTransactionCollection : esEntityCollectionWAuditLog
    {
        public esClosingWageTransactionCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "ClosingWageTransactionCollection";
        }

        #region Query Logic
        protected void InitQuery(esClosingWageTransactionQuery query)
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
            this.InitQuery(query as esClosingWageTransactionQuery);
        }
        #endregion

        virtual public ClosingWageTransaction DetachEntity(ClosingWageTransaction entity)
        {
            return base.DetachEntity(entity) as ClosingWageTransaction;
        }

        virtual public ClosingWageTransaction AttachEntity(ClosingWageTransaction entity)
        {
            return base.AttachEntity(entity) as ClosingWageTransaction;
        }

        virtual public void Combine(ClosingWageTransactionCollection collection)
        {
            base.Combine(collection);
        }

        new public ClosingWageTransaction this[int index]
        {
            get
            {
                return base[index] as ClosingWageTransaction;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(ClosingWageTransaction);
        }
    }



    [Serializable]
    abstract public class esClosingWageTransaction : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esClosingWageTransactionQuery GetDynamicQuery()
        {
            return null;
        }

        public esClosingWageTransaction()
        {

        }

        public esClosingWageTransaction(DataRow row)
            : base(row)
        {

        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(System.Int32 payrollPeriodID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(payrollPeriodID);
            else
                return LoadByPrimaryKeyStoredProcedure(payrollPeriodID);
        }

        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 payrollPeriodID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(payrollPeriodID);
            else
                return LoadByPrimaryKeyStoredProcedure(payrollPeriodID);
        }

        private bool LoadByPrimaryKeyDynamic(System.Int32 payrollPeriodID)
        {
            esClosingWageTransactionQuery query = this.GetDynamicQuery();
            query.Where(query.PayrollPeriodID == payrollPeriodID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(System.Int32 payrollPeriodID)
        {
            esParameters parms = new esParameters();
            parms.Add("PayrollPeriodID", payrollPeriodID);
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
                        case "PayrollPeriodID": this.str.PayrollPeriodID = (string)value; break;
                        case "IsClosed": this.str.IsClosed = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "PayrollPeriodID":

                            if (value == null || value is System.Int32)
                                this.PayrollPeriodID = (System.Int32?)value;
                            break;

                        case "IsClosed":

                            if (value == null || value is System.Boolean)
                                this.IsClosed = (System.Boolean?)value;
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
        /// Maps to ClosingWageTransaction.PayrollPeriodID
        /// </summary>
        virtual public System.Int32? PayrollPeriodID
        {
            get
            {
                return base.GetSystemInt32(ClosingWageTransactionMetadata.ColumnNames.PayrollPeriodID);
            }

            set
            {
                base.SetSystemInt32(ClosingWageTransactionMetadata.ColumnNames.PayrollPeriodID, value);
            }
        }

        /// <summary>
        /// Maps to ClosingWageTransaction.IsClosed
        /// </summary>
        virtual public System.Boolean? IsClosed
        {
            get
            {
                return base.GetSystemBoolean(ClosingWageTransactionMetadata.ColumnNames.IsClosed);
            }

            set
            {
                base.SetSystemBoolean(ClosingWageTransactionMetadata.ColumnNames.IsClosed, value);
            }
        }

        /// <summary>
        /// Maps to ClosingWageTransaction.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(ClosingWageTransactionMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(ClosingWageTransactionMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to ClosingWageTransaction.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(ClosingWageTransactionMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(ClosingWageTransactionMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esClosingWageTransaction entity)
            {
                this.entity = entity;
            }


            public System.String PayrollPeriodID
            {
                get
                {
                    System.Int32? data = entity.PayrollPeriodID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PayrollPeriodID = null;
                    else entity.PayrollPeriodID = Convert.ToInt32(value);
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


            private esClosingWageTransaction entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esClosingWageTransactionQuery query)
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
                throw new Exception("esClosingWageTransaction can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    public partial class ClosingWageTransaction : esClosingWageTransaction
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
    abstract public class esClosingWageTransactionQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return ClosingWageTransactionMetadata.Meta();
            }
        }


        public esQueryItem PayrollPeriodID
        {
            get
            {
                return new esQueryItem(this, ClosingWageTransactionMetadata.ColumnNames.PayrollPeriodID, esSystemType.Int32);
            }
        }

        public esQueryItem IsClosed
        {
            get
            {
                return new esQueryItem(this, ClosingWageTransactionMetadata.ColumnNames.IsClosed, esSystemType.Boolean);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, ClosingWageTransactionMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, ClosingWageTransactionMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("ClosingWageTransactionCollection")]
    public partial class ClosingWageTransactionCollection : esClosingWageTransactionCollection, IEnumerable<ClosingWageTransaction>
    {
        public ClosingWageTransactionCollection()
        {

        }

        public static implicit operator List<ClosingWageTransaction>(ClosingWageTransactionCollection coll)
        {
            List<ClosingWageTransaction> list = new List<ClosingWageTransaction>();

            foreach (ClosingWageTransaction emp in coll)
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
                return ClosingWageTransactionMetadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ClosingWageTransactionQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new ClosingWageTransaction(row);
        }

        override protected esEntity CreateEntity()
        {
            return new ClosingWageTransaction();
        }


        #endregion


        [BrowsableAttribute(false)]
        public ClosingWageTransactionQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ClosingWageTransactionQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(ClosingWageTransactionQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public ClosingWageTransaction AddNew()
        {
            ClosingWageTransaction entity = base.AddNewEntity() as ClosingWageTransaction;

            return entity;
        }

        public ClosingWageTransaction FindByPrimaryKey(System.Int32 payrollPeriodID)
        {
            return base.FindByPrimaryKey(payrollPeriodID) as ClosingWageTransaction;
        }


        #region IEnumerable<ClosingWageTransaction> Members

        IEnumerator<ClosingWageTransaction> IEnumerable<ClosingWageTransaction>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as ClosingWageTransaction;
            }
        }

        #endregion

        private ClosingWageTransactionQuery query;
    }


    /// <summary>
    /// Encapsulates the 'ClosingWageTransaction' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("ClosingWageTransaction ({PayrollPeriodID})")]
    [Serializable]
    public partial class ClosingWageTransaction : esClosingWageTransaction
    {
        public ClosingWageTransaction()
        {

        }

        public ClosingWageTransaction(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return ClosingWageTransactionMetadata.Meta();
            }
        }



        override protected esClosingWageTransactionQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ClosingWageTransactionQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public ClosingWageTransactionQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ClosingWageTransactionQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(ClosingWageTransactionQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private ClosingWageTransactionQuery query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class ClosingWageTransactionQuery : esClosingWageTransactionQuery
    {
        public ClosingWageTransactionQuery()
        {

        }

        public ClosingWageTransactionQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "ClosingWageTransactionQuery";
        }


    }


    [Serializable]
    public partial class ClosingWageTransactionMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected ClosingWageTransactionMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(ClosingWageTransactionMetadata.ColumnNames.PayrollPeriodID, 0, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = ClosingWageTransactionMetadata.PropertyNames.PayrollPeriodID;
            c.IsInPrimaryKey = true;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(ClosingWageTransactionMetadata.ColumnNames.IsClosed, 1, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ClosingWageTransactionMetadata.PropertyNames.IsClosed;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ClosingWageTransactionMetadata.ColumnNames.LastUpdateDateTime, 2, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ClosingWageTransactionMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ClosingWageTransactionMetadata.ColumnNames.LastUpdateByUserID, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = ClosingWageTransactionMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 40;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public ClosingWageTransactionMetadata Meta()
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
            public const string PayrollPeriodID = "PayrollPeriodID";
            public const string IsClosed = "IsClosed";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string PayrollPeriodID = "PayrollPeriodID";
            public const string IsClosed = "IsClosed";
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
            lock (typeof(ClosingWageTransactionMetadata))
            {
                if (ClosingWageTransactionMetadata.mapDelegates == null)
                {
                    ClosingWageTransactionMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (ClosingWageTransactionMetadata.meta == null)
                {
                    ClosingWageTransactionMetadata.meta = new ClosingWageTransactionMetadata();
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


                meta.AddTypeMap("PayrollPeriodID", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("IsClosed", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));



                meta.Source = "ClosingWageTransaction";
                meta.Destination = "ClosingWageTransaction";

                meta.spInsert = "proc_ClosingWageTransactionInsert";
                meta.spUpdate = "proc_ClosingWageTransactionUpdate";
                meta.spDelete = "proc_ClosingWageTransactionDelete";
                meta.spLoadAll = "proc_ClosingWageTransactionLoadAll";
                meta.spLoadByPrimaryKey = "proc_ClosingWageTransactionLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private ClosingWageTransactionMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
