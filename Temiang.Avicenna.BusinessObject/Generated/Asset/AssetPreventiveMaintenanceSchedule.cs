/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 8/12/2015 10:30:59 AM
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
    abstract public class esAssetPreventiveMaintenanceScheduleCollection : esEntityCollectionWAuditLog
    {
        public esAssetPreventiveMaintenanceScheduleCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "AssetPreventiveMaintenanceScheduleCollection";
        }

        #region Query Logic
        protected void InitQuery(esAssetPreventiveMaintenanceScheduleQuery query)
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
            this.InitQuery(query as esAssetPreventiveMaintenanceScheduleQuery);
        }
        #endregion

        virtual public AssetPreventiveMaintenanceSchedule DetachEntity(AssetPreventiveMaintenanceSchedule entity)
        {
            return base.DetachEntity(entity) as AssetPreventiveMaintenanceSchedule;
        }

        virtual public AssetPreventiveMaintenanceSchedule AttachEntity(AssetPreventiveMaintenanceSchedule entity)
        {
            return base.AttachEntity(entity) as AssetPreventiveMaintenanceSchedule;
        }

        virtual public void Combine(AssetPreventiveMaintenanceScheduleCollection collection)
        {
            base.Combine(collection);
        }

        new public AssetPreventiveMaintenanceSchedule this[int index]
        {
            get
            {
                return base[index] as AssetPreventiveMaintenanceSchedule;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(AssetPreventiveMaintenanceSchedule);
        }
    }



    [Serializable]
    abstract public class esAssetPreventiveMaintenanceSchedule : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esAssetPreventiveMaintenanceScheduleQuery GetDynamicQuery()
        {
            return null;
        }

        public esAssetPreventiveMaintenanceSchedule()
        {

        }

        public esAssetPreventiveMaintenanceSchedule(DataRow row)
            : base(row)
        {

        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(System.String assetID, System.DateTime scheduleDate)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(assetID, scheduleDate);
            else
                return LoadByPrimaryKeyStoredProcedure(assetID, scheduleDate);
        }

        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String assetID, System.DateTime scheduleDate)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(assetID, scheduleDate);
            else
                return LoadByPrimaryKeyStoredProcedure(assetID, scheduleDate);
        }

        private bool LoadByPrimaryKeyDynamic(System.String assetID, System.DateTime scheduleDate)
        {
            esAssetPreventiveMaintenanceScheduleQuery query = this.GetDynamicQuery();
            query.Where(query.AssetID == assetID, query.ScheduleDate == scheduleDate);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(System.String assetID, System.DateTime scheduleDate)
        {
            esParameters parms = new esParameters();
            parms.Add("AssetID", assetID); parms.Add("ScheduleDate", scheduleDate);
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
                        case "AssetID": this.str.AssetID = (string)value; break;
                        case "ScheduleDate": this.str.ScheduleDate = (string)value; break;
                        case "PeriodYear": this.str.PeriodYear = (string)value; break;
                        case "PeriodDate": this.str.PeriodDate = (string)value; break;
                        case "IsProcessed": this.str.IsProcessed = (string)value; break;
                        case "IsVoid": this.str.IsVoid = (string)value; break;
                        case "VoidDateTime": this.str.VoidDateTime = (string)value; break;
                        case "VoidByUserID": this.str.VoidByUserID = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "ScheduleDate":

                            if (value == null || value is System.DateTime)
                                this.ScheduleDate = (System.DateTime?)value;
                            break;

                        case "PeriodDate":

                            if (value == null || value is System.DateTime)
                                this.PeriodDate = (System.DateTime?)value;
                            break;

                        case "IsProcessed":

                            if (value == null || value is System.Boolean)
                                this.IsProcessed = (System.Boolean?)value;
                            break;

                        case "IsVoid":

                            if (value == null || value is System.Boolean)
                                this.IsVoid = (System.Boolean?)value;
                            break;

                        case "VoidDateTime":

                            if (value == null || value is System.DateTime)
                                this.VoidDateTime = (System.DateTime?)value;
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
        /// Maps to AssetPreventiveMaintenanceSchedule.AssetID
        /// </summary>
        virtual public System.String AssetID
        {
            get
            {
                return base.GetSystemString(AssetPreventiveMaintenanceScheduleMetadata.ColumnNames.AssetID);
            }

            set
            {
                base.SetSystemString(AssetPreventiveMaintenanceScheduleMetadata.ColumnNames.AssetID, value);
            }
        }

        /// <summary>
        /// Maps to AssetPreventiveMaintenanceSchedule.ScheduleDate
        /// </summary>
        virtual public System.DateTime? ScheduleDate
        {
            get
            {
                return base.GetSystemDateTime(AssetPreventiveMaintenanceScheduleMetadata.ColumnNames.ScheduleDate);
            }

            set
            {
                base.SetSystemDateTime(AssetPreventiveMaintenanceScheduleMetadata.ColumnNames.ScheduleDate, value);
            }
        }

        /// <summary>
        /// Maps to AssetPreventiveMaintenanceSchedule.PeriodYear
        /// </summary>
        virtual public System.String PeriodYear
        {
            get
            {
                return base.GetSystemString(AssetPreventiveMaintenanceScheduleMetadata.ColumnNames.PeriodYear);
            }

            set
            {
                base.SetSystemString(AssetPreventiveMaintenanceScheduleMetadata.ColumnNames.PeriodYear, value);
            }
        }

        /// <summary>
        /// Maps to AssetPreventiveMaintenanceSchedule.PeriodDate
        /// </summary>
        virtual public System.DateTime? PeriodDate
        {
            get
            {
                return base.GetSystemDateTime(AssetPreventiveMaintenanceScheduleMetadata.ColumnNames.PeriodDate);
            }

            set
            {
                base.SetSystemDateTime(AssetPreventiveMaintenanceScheduleMetadata.ColumnNames.PeriodDate, value);
            }
        }

        /// <summary>
        /// Maps to AssetPreventiveMaintenanceSchedule.IsProcessed
        /// </summary>
        virtual public System.Boolean? IsProcessed
        {
            get
            {
                return base.GetSystemBoolean(AssetPreventiveMaintenanceScheduleMetadata.ColumnNames.IsProcessed);
            }

            set
            {
                base.SetSystemBoolean(AssetPreventiveMaintenanceScheduleMetadata.ColumnNames.IsProcessed, value);
            }
        }

        /// <summary>
        /// Maps to AssetPreventiveMaintenanceSchedule.IsVoid
        /// </summary>
        virtual public System.Boolean? IsVoid
        {
            get
            {
                return base.GetSystemBoolean(AssetPreventiveMaintenanceScheduleMetadata.ColumnNames.IsVoid);
            }

            set
            {
                base.SetSystemBoolean(AssetPreventiveMaintenanceScheduleMetadata.ColumnNames.IsVoid, value);
            }
        }

        /// <summary>
        /// Maps to AssetPreventiveMaintenanceSchedule.VoidDateTime
        /// </summary>
        virtual public System.DateTime? VoidDateTime
        {
            get
            {
                return base.GetSystemDateTime(AssetPreventiveMaintenanceScheduleMetadata.ColumnNames.VoidDateTime);
            }

            set
            {
                base.SetSystemDateTime(AssetPreventiveMaintenanceScheduleMetadata.ColumnNames.VoidDateTime, value);
            }
        }

        /// <summary>
        /// Maps to AssetPreventiveMaintenanceSchedule.VoidByUserID
        /// </summary>
        virtual public System.String VoidByUserID
        {
            get
            {
                return base.GetSystemString(AssetPreventiveMaintenanceScheduleMetadata.ColumnNames.VoidByUserID);
            }

            set
            {
                base.SetSystemString(AssetPreventiveMaintenanceScheduleMetadata.ColumnNames.VoidByUserID, value);
            }
        }

        /// <summary>
        /// Maps to AssetPreventiveMaintenanceSchedule.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(AssetPreventiveMaintenanceScheduleMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(AssetPreventiveMaintenanceScheduleMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to AssetPreventiveMaintenanceSchedule.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(AssetPreventiveMaintenanceScheduleMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(AssetPreventiveMaintenanceScheduleMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esAssetPreventiveMaintenanceSchedule entity)
            {
                this.entity = entity;
            }


            public System.String AssetID
            {
                get
                {
                    System.String data = entity.AssetID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.AssetID = null;
                    else entity.AssetID = Convert.ToString(value);
                }
            }

            public System.String ScheduleDate
            {
                get
                {
                    System.DateTime? data = entity.ScheduleDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ScheduleDate = null;
                    else entity.ScheduleDate = Convert.ToDateTime(value);
                }
            }

            public System.String PeriodYear
            {
                get
                {
                    System.String data = entity.PeriodYear;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PeriodYear = null;
                    else entity.PeriodYear = Convert.ToString(value);
                }
            }

            public System.String PeriodDate
            {
                get
                {
                    System.DateTime? data = entity.PeriodDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PeriodDate = null;
                    else entity.PeriodDate = Convert.ToDateTime(value);
                }
            }

            public System.String IsProcessed
            {
                get
                {
                    System.Boolean? data = entity.IsProcessed;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsProcessed = null;
                    else entity.IsProcessed = Convert.ToBoolean(value);
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

            public System.String VoidDateTime
            {
                get
                {
                    System.DateTime? data = entity.VoidDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.VoidDateTime = null;
                    else entity.VoidDateTime = Convert.ToDateTime(value);
                }
            }

            public System.String VoidByUserID
            {
                get
                {
                    System.String data = entity.VoidByUserID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.VoidByUserID = null;
                    else entity.VoidByUserID = Convert.ToString(value);
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


            private esAssetPreventiveMaintenanceSchedule entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esAssetPreventiveMaintenanceScheduleQuery query)
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
                throw new Exception("esAssetPreventiveMaintenanceSchedule can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    public partial class AssetPreventiveMaintenanceSchedule : esAssetPreventiveMaintenanceSchedule
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
    abstract public class esAssetPreventiveMaintenanceScheduleQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return AssetPreventiveMaintenanceScheduleMetadata.Meta();
            }
        }


        public esQueryItem AssetID
        {
            get
            {
                return new esQueryItem(this, AssetPreventiveMaintenanceScheduleMetadata.ColumnNames.AssetID, esSystemType.String);
            }
        }

        public esQueryItem ScheduleDate
        {
            get
            {
                return new esQueryItem(this, AssetPreventiveMaintenanceScheduleMetadata.ColumnNames.ScheduleDate, esSystemType.DateTime);
            }
        }

        public esQueryItem PeriodYear
        {
            get
            {
                return new esQueryItem(this, AssetPreventiveMaintenanceScheduleMetadata.ColumnNames.PeriodYear, esSystemType.String);
            }
        }

        public esQueryItem PeriodDate
        {
            get
            {
                return new esQueryItem(this, AssetPreventiveMaintenanceScheduleMetadata.ColumnNames.PeriodDate, esSystemType.DateTime);
            }
        }

        public esQueryItem IsProcessed
        {
            get
            {
                return new esQueryItem(this, AssetPreventiveMaintenanceScheduleMetadata.ColumnNames.IsProcessed, esSystemType.Boolean);
            }
        }

        public esQueryItem IsVoid
        {
            get
            {
                return new esQueryItem(this, AssetPreventiveMaintenanceScheduleMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
            }
        }

        public esQueryItem VoidDateTime
        {
            get
            {
                return new esQueryItem(this, AssetPreventiveMaintenanceScheduleMetadata.ColumnNames.VoidDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem VoidByUserID
        {
            get
            {
                return new esQueryItem(this, AssetPreventiveMaintenanceScheduleMetadata.ColumnNames.VoidByUserID, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, AssetPreventiveMaintenanceScheduleMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, AssetPreventiveMaintenanceScheduleMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("AssetPreventiveMaintenanceScheduleCollection")]
    public partial class AssetPreventiveMaintenanceScheduleCollection : esAssetPreventiveMaintenanceScheduleCollection, IEnumerable<AssetPreventiveMaintenanceSchedule>
    {
        public AssetPreventiveMaintenanceScheduleCollection()
        {

        }

        public static implicit operator List<AssetPreventiveMaintenanceSchedule>(AssetPreventiveMaintenanceScheduleCollection coll)
        {
            List<AssetPreventiveMaintenanceSchedule> list = new List<AssetPreventiveMaintenanceSchedule>();

            foreach (AssetPreventiveMaintenanceSchedule emp in coll)
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
                return AssetPreventiveMaintenanceScheduleMetadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new AssetPreventiveMaintenanceScheduleQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new AssetPreventiveMaintenanceSchedule(row);
        }

        override protected esEntity CreateEntity()
        {
            return new AssetPreventiveMaintenanceSchedule();
        }


        #endregion


        [BrowsableAttribute(false)]
        public AssetPreventiveMaintenanceScheduleQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new AssetPreventiveMaintenanceScheduleQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(AssetPreventiveMaintenanceScheduleQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public AssetPreventiveMaintenanceSchedule AddNew()
        {
            AssetPreventiveMaintenanceSchedule entity = base.AddNewEntity() as AssetPreventiveMaintenanceSchedule;

            return entity;
        }

        public AssetPreventiveMaintenanceSchedule FindByPrimaryKey(System.String assetID, System.DateTime scheduleDate)
        {
            return base.FindByPrimaryKey(assetID, scheduleDate) as AssetPreventiveMaintenanceSchedule;
        }


        #region IEnumerable<AssetPreventiveMaintenanceSchedule> Members

        IEnumerator<AssetPreventiveMaintenanceSchedule> IEnumerable<AssetPreventiveMaintenanceSchedule>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as AssetPreventiveMaintenanceSchedule;
            }
        }

        #endregion

        private AssetPreventiveMaintenanceScheduleQuery query;
    }


    /// <summary>
    /// Encapsulates the 'AssetPreventiveMaintenanceSchedule' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("AssetPreventiveMaintenanceSchedule ({AssetID},{ScheduleDate})")]
    [Serializable]
    public partial class AssetPreventiveMaintenanceSchedule : esAssetPreventiveMaintenanceSchedule
    {
        public AssetPreventiveMaintenanceSchedule()
        {

        }

        public AssetPreventiveMaintenanceSchedule(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return AssetPreventiveMaintenanceScheduleMetadata.Meta();
            }
        }



        override protected esAssetPreventiveMaintenanceScheduleQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new AssetPreventiveMaintenanceScheduleQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public AssetPreventiveMaintenanceScheduleQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new AssetPreventiveMaintenanceScheduleQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(AssetPreventiveMaintenanceScheduleQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private AssetPreventiveMaintenanceScheduleQuery query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class AssetPreventiveMaintenanceScheduleQuery : esAssetPreventiveMaintenanceScheduleQuery
    {
        public AssetPreventiveMaintenanceScheduleQuery()
        {

        }

        public AssetPreventiveMaintenanceScheduleQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "AssetPreventiveMaintenanceScheduleQuery";
        }


    }


    [Serializable]
    public partial class AssetPreventiveMaintenanceScheduleMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected AssetPreventiveMaintenanceScheduleMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(AssetPreventiveMaintenanceScheduleMetadata.ColumnNames.AssetID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = AssetPreventiveMaintenanceScheduleMetadata.PropertyNames.AssetID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 30;
            _columns.Add(c);

            c = new esColumnMetadata(AssetPreventiveMaintenanceScheduleMetadata.ColumnNames.ScheduleDate, 1, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = AssetPreventiveMaintenanceScheduleMetadata.PropertyNames.ScheduleDate;
            c.IsInPrimaryKey = true;
            _columns.Add(c);

            c = new esColumnMetadata(AssetPreventiveMaintenanceScheduleMetadata.ColumnNames.PeriodYear, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = AssetPreventiveMaintenanceScheduleMetadata.PropertyNames.PeriodYear;
            c.CharacterMaxLength = 4;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AssetPreventiveMaintenanceScheduleMetadata.ColumnNames.PeriodDate, 3, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = AssetPreventiveMaintenanceScheduleMetadata.PropertyNames.PeriodDate;
            _columns.Add(c);

            c = new esColumnMetadata(AssetPreventiveMaintenanceScheduleMetadata.ColumnNames.IsProcessed, 4, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = AssetPreventiveMaintenanceScheduleMetadata.PropertyNames.IsProcessed;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(AssetPreventiveMaintenanceScheduleMetadata.ColumnNames.IsVoid, 5, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = AssetPreventiveMaintenanceScheduleMetadata.PropertyNames.IsVoid;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(AssetPreventiveMaintenanceScheduleMetadata.ColumnNames.VoidDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = AssetPreventiveMaintenanceScheduleMetadata.PropertyNames.VoidDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AssetPreventiveMaintenanceScheduleMetadata.ColumnNames.VoidByUserID, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = AssetPreventiveMaintenanceScheduleMetadata.PropertyNames.VoidByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AssetPreventiveMaintenanceScheduleMetadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = AssetPreventiveMaintenanceScheduleMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AssetPreventiveMaintenanceScheduleMetadata.ColumnNames.LastUpdateByUserID, 9, typeof(System.String), esSystemType.String);
            c.PropertyName = AssetPreventiveMaintenanceScheduleMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public AssetPreventiveMaintenanceScheduleMetadata Meta()
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
            public const string AssetID = "AssetID";
            public const string ScheduleDate = "ScheduleDate";
            public const string PeriodYear = "PeriodYear";
            public const string PeriodDate = "PeriodDate";
            public const string IsProcessed = "IsProcessed";
            public const string IsVoid = "IsVoid";
            public const string VoidDateTime = "VoidDateTime";
            public const string VoidByUserID = "VoidByUserID";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string AssetID = "AssetID";
            public const string ScheduleDate = "ScheduleDate";
            public const string PeriodYear = "PeriodYear";
            public const string PeriodDate = "PeriodDate";
            public const string IsProcessed = "IsProcessed";
            public const string IsVoid = "IsVoid";
            public const string VoidDateTime = "VoidDateTime";
            public const string VoidByUserID = "VoidByUserID";
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
            lock (typeof(AssetPreventiveMaintenanceScheduleMetadata))
            {
                if (AssetPreventiveMaintenanceScheduleMetadata.mapDelegates == null)
                {
                    AssetPreventiveMaintenanceScheduleMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (AssetPreventiveMaintenanceScheduleMetadata.meta == null)
                {
                    AssetPreventiveMaintenanceScheduleMetadata.meta = new AssetPreventiveMaintenanceScheduleMetadata();
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


                meta.AddTypeMap("AssetID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ScheduleDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("PeriodYear", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("PeriodDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("IsProcessed", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("VoidDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("VoidByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));



                meta.Source = "AssetPreventiveMaintenanceSchedule";
                meta.Destination = "AssetPreventiveMaintenanceSchedule";

                meta.spInsert = "proc_AssetPreventiveMaintenanceScheduleInsert";
                meta.spUpdate = "proc_AssetPreventiveMaintenanceScheduleUpdate";
                meta.spDelete = "proc_AssetPreventiveMaintenanceScheduleDelete";
                meta.spLoadAll = "proc_AssetPreventiveMaintenanceScheduleLoadAll";
                meta.spLoadByPrimaryKey = "proc_AssetPreventiveMaintenanceScheduleLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private AssetPreventiveMaintenanceScheduleMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
