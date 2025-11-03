/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 11/14/2014 10:46:34 AM
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
    abstract public class esAssetPreventiveMaintenanceCollection : esEntityCollectionWAuditLog
    {
        public esAssetPreventiveMaintenanceCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "AssetPreventiveMaintenanceCollection";
        }

        #region Query Logic
        protected void InitQuery(esAssetPreventiveMaintenanceQuery query)
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
            this.InitQuery(query as esAssetPreventiveMaintenanceQuery);
        }
        #endregion

        virtual public AssetPreventiveMaintenance DetachEntity(AssetPreventiveMaintenance entity)
        {
            return base.DetachEntity(entity) as AssetPreventiveMaintenance;
        }

        virtual public AssetPreventiveMaintenance AttachEntity(AssetPreventiveMaintenance entity)
        {
            return base.AttachEntity(entity) as AssetPreventiveMaintenance;
        }

        virtual public void Combine(AssetPreventiveMaintenanceCollection collection)
        {
            base.Combine(collection);
        }

        new public AssetPreventiveMaintenance this[int index]
        {
            get
            {
                return base[index] as AssetPreventiveMaintenance;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(AssetPreventiveMaintenance);
        }
    }



    [Serializable]
    abstract public class esAssetPreventiveMaintenance : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esAssetPreventiveMaintenanceQuery GetDynamicQuery()
        {
            return null;
        }

        public esAssetPreventiveMaintenance()
        {

        }

        public esAssetPreventiveMaintenance(DataRow row)
            : base(row)
        {

        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(System.String pMNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(pMNo);
            else
                return LoadByPrimaryKeyStoredProcedure(pMNo);
        }

        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String pMNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(pMNo);
            else
                return LoadByPrimaryKeyStoredProcedure(pMNo);
        }

        private bool LoadByPrimaryKeyDynamic(System.String pMNo)
        {
            esAssetPreventiveMaintenanceQuery query = this.GetDynamicQuery();
            query.Where(query.PMNo == pMNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(System.String pMNo)
        {
            esParameters parms = new esParameters();
            parms.Add("PMNo", pMNo);
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
                        case "PMNo": this.str.PMNo = (string)value; break;
                        case "PMDate": this.str.PMDate = (string)value; break;
                        case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
                        case "AssetID": this.str.AssetID = (string)value; break;
                        case "SRWorkTrade": this.str.SRWorkTrade = (string)value; break;
                        case "TargetDate": this.str.TargetDate = (string)value; break;
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
                        case "PMDate":

                            if (value == null || value is System.DateTime)
                                this.PMDate = (System.DateTime?)value;
                            break;

                        case "TargetDate":

                            if (value == null || value is System.DateTime)
                                this.TargetDate = (System.DateTime?)value;
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
        /// Maps to AssetPreventiveMaintenance.PMNo
        /// </summary>
        virtual public System.String PMNo
        {
            get
            {
                return base.GetSystemString(AssetPreventiveMaintenanceMetadata.ColumnNames.PMNo);
            }

            set
            {
                base.SetSystemString(AssetPreventiveMaintenanceMetadata.ColumnNames.PMNo, value);
            }
        }

        /// <summary>
        /// Maps to AssetPreventiveMaintenance.PMDate
        /// </summary>
        virtual public System.DateTime? PMDate
        {
            get
            {
                return base.GetSystemDateTime(AssetPreventiveMaintenanceMetadata.ColumnNames.PMDate);
            }

            set
            {
                base.SetSystemDateTime(AssetPreventiveMaintenanceMetadata.ColumnNames.PMDate, value);
            }
        }

        /// <summary>
        /// Maps to AssetPreventiveMaintenance.ServiceUnitID
        /// </summary>
        virtual public System.String ServiceUnitID
        {
            get
            {
                return base.GetSystemString(AssetPreventiveMaintenanceMetadata.ColumnNames.ServiceUnitID);
            }

            set
            {
                base.SetSystemString(AssetPreventiveMaintenanceMetadata.ColumnNames.ServiceUnitID, value);
            }
        }

        /// <summary>
        /// Maps to AssetPreventiveMaintenance.AssetID
        /// </summary>
        virtual public System.String AssetID
        {
            get
            {
                return base.GetSystemString(AssetPreventiveMaintenanceMetadata.ColumnNames.AssetID);
            }

            set
            {
                base.SetSystemString(AssetPreventiveMaintenanceMetadata.ColumnNames.AssetID, value);
            }
        }

        /// <summary>
        /// Maps to AssetPreventiveMaintenance.SRWorkTrade
        /// </summary>
        virtual public System.String SRWorkTrade
        {
            get
            {
                return base.GetSystemString(AssetPreventiveMaintenanceMetadata.ColumnNames.SRWorkTrade);
            }

            set
            {
                base.SetSystemString(AssetPreventiveMaintenanceMetadata.ColumnNames.SRWorkTrade, value);
            }
        }

        /// <summary>
        /// Maps to AssetPreventiveMaintenance.TargetDate
        /// </summary>
        virtual public System.DateTime? TargetDate
        {
            get
            {
                return base.GetSystemDateTime(AssetPreventiveMaintenanceMetadata.ColumnNames.TargetDate);
            }

            set
            {
                base.SetSystemDateTime(AssetPreventiveMaintenanceMetadata.ColumnNames.TargetDate, value);
            }
        }

        /// <summary>
        /// Maps to AssetPreventiveMaintenance.IsApproved
        /// </summary>
        virtual public System.Boolean? IsApproved
        {
            get
            {
                return base.GetSystemBoolean(AssetPreventiveMaintenanceMetadata.ColumnNames.IsApproved);
            }

            set
            {
                base.SetSystemBoolean(AssetPreventiveMaintenanceMetadata.ColumnNames.IsApproved, value);
            }
        }

        /// <summary>
        /// Maps to AssetPreventiveMaintenance.IsVoid
        /// </summary>
        virtual public System.Boolean? IsVoid
        {
            get
            {
                return base.GetSystemBoolean(AssetPreventiveMaintenanceMetadata.ColumnNames.IsVoid);
            }

            set
            {
                base.SetSystemBoolean(AssetPreventiveMaintenanceMetadata.ColumnNames.IsVoid, value);
            }
        }

        /// <summary>
        /// Maps to AssetPreventiveMaintenance.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(AssetPreventiveMaintenanceMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(AssetPreventiveMaintenanceMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to AssetPreventiveMaintenance.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(AssetPreventiveMaintenanceMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(AssetPreventiveMaintenanceMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esAssetPreventiveMaintenance entity)
            {
                this.entity = entity;
            }


            public System.String PMNo
            {
                get
                {
                    System.String data = entity.PMNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PMNo = null;
                    else entity.PMNo = Convert.ToString(value);
                }
            }

            public System.String PMDate
            {
                get
                {
                    System.DateTime? data = entity.PMDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PMDate = null;
                    else entity.PMDate = Convert.ToDateTime(value);
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

            public System.String SRWorkTrade
            {
                get
                {
                    System.String data = entity.SRWorkTrade;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRWorkTrade = null;
                    else entity.SRWorkTrade = Convert.ToString(value);
                }
            }

            public System.String TargetDate
            {
                get
                {
                    System.DateTime? data = entity.TargetDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TargetDate = null;
                    else entity.TargetDate = Convert.ToDateTime(value);
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


            private esAssetPreventiveMaintenance entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esAssetPreventiveMaintenanceQuery query)
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
                throw new Exception("esAssetPreventiveMaintenance can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    public partial class AssetPreventiveMaintenance : esAssetPreventiveMaintenance
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
    abstract public class esAssetPreventiveMaintenanceQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return AssetPreventiveMaintenanceMetadata.Meta();
            }
        }


        public esQueryItem PMNo
        {
            get
            {
                return new esQueryItem(this, AssetPreventiveMaintenanceMetadata.ColumnNames.PMNo, esSystemType.String);
            }
        }

        public esQueryItem PMDate
        {
            get
            {
                return new esQueryItem(this, AssetPreventiveMaintenanceMetadata.ColumnNames.PMDate, esSystemType.DateTime);
            }
        }

        public esQueryItem ServiceUnitID
        {
            get
            {
                return new esQueryItem(this, AssetPreventiveMaintenanceMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
            }
        }

        public esQueryItem AssetID
        {
            get
            {
                return new esQueryItem(this, AssetPreventiveMaintenanceMetadata.ColumnNames.AssetID, esSystemType.String);
            }
        }

        public esQueryItem SRWorkTrade
        {
            get
            {
                return new esQueryItem(this, AssetPreventiveMaintenanceMetadata.ColumnNames.SRWorkTrade, esSystemType.String);
            }
        }

        public esQueryItem TargetDate
        {
            get
            {
                return new esQueryItem(this, AssetPreventiveMaintenanceMetadata.ColumnNames.TargetDate, esSystemType.DateTime);
            }
        }

        public esQueryItem IsApproved
        {
            get
            {
                return new esQueryItem(this, AssetPreventiveMaintenanceMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
            }
        }

        public esQueryItem IsVoid
        {
            get
            {
                return new esQueryItem(this, AssetPreventiveMaintenanceMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, AssetPreventiveMaintenanceMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, AssetPreventiveMaintenanceMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("AssetPreventiveMaintenanceCollection")]
    public partial class AssetPreventiveMaintenanceCollection : esAssetPreventiveMaintenanceCollection, IEnumerable<AssetPreventiveMaintenance>
    {
        public AssetPreventiveMaintenanceCollection()
        {

        }

        public static implicit operator List<AssetPreventiveMaintenance>(AssetPreventiveMaintenanceCollection coll)
        {
            List<AssetPreventiveMaintenance> list = new List<AssetPreventiveMaintenance>();

            foreach (AssetPreventiveMaintenance emp in coll)
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
                return AssetPreventiveMaintenanceMetadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new AssetPreventiveMaintenanceQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new AssetPreventiveMaintenance(row);
        }

        override protected esEntity CreateEntity()
        {
            return new AssetPreventiveMaintenance();
        }


        #endregion


        [BrowsableAttribute(false)]
        public AssetPreventiveMaintenanceQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new AssetPreventiveMaintenanceQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(AssetPreventiveMaintenanceQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public AssetPreventiveMaintenance AddNew()
        {
            AssetPreventiveMaintenance entity = base.AddNewEntity() as AssetPreventiveMaintenance;

            return entity;
        }

        public AssetPreventiveMaintenance FindByPrimaryKey(System.String pMNo)
        {
            return base.FindByPrimaryKey(pMNo) as AssetPreventiveMaintenance;
        }


        #region IEnumerable<AssetPreventiveMaintenance> Members

        IEnumerator<AssetPreventiveMaintenance> IEnumerable<AssetPreventiveMaintenance>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as AssetPreventiveMaintenance;
            }
        }

        #endregion

        private AssetPreventiveMaintenanceQuery query;
    }


    /// <summary>
    /// Encapsulates the 'AssetPreventiveMaintenance' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("AssetPreventiveMaintenance ({PMNo})")]
    [Serializable]
    public partial class AssetPreventiveMaintenance : esAssetPreventiveMaintenance
    {
        public AssetPreventiveMaintenance()
        {

        }

        public AssetPreventiveMaintenance(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return AssetPreventiveMaintenanceMetadata.Meta();
            }
        }



        override protected esAssetPreventiveMaintenanceQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new AssetPreventiveMaintenanceQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public AssetPreventiveMaintenanceQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new AssetPreventiveMaintenanceQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(AssetPreventiveMaintenanceQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private AssetPreventiveMaintenanceQuery query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class AssetPreventiveMaintenanceQuery : esAssetPreventiveMaintenanceQuery
    {
        public AssetPreventiveMaintenanceQuery()
        {

        }

        public AssetPreventiveMaintenanceQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "AssetPreventiveMaintenanceQuery";
        }


    }


    [Serializable]
    public partial class AssetPreventiveMaintenanceMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected AssetPreventiveMaintenanceMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(AssetPreventiveMaintenanceMetadata.ColumnNames.PMNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = AssetPreventiveMaintenanceMetadata.PropertyNames.PMNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(AssetPreventiveMaintenanceMetadata.ColumnNames.PMDate, 1, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = AssetPreventiveMaintenanceMetadata.PropertyNames.PMDate;
            _columns.Add(c);

            c = new esColumnMetadata(AssetPreventiveMaintenanceMetadata.ColumnNames.ServiceUnitID, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = AssetPreventiveMaintenanceMetadata.PropertyNames.ServiceUnitID;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(AssetPreventiveMaintenanceMetadata.ColumnNames.AssetID, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = AssetPreventiveMaintenanceMetadata.PropertyNames.AssetID;
            c.CharacterMaxLength = 30;
            _columns.Add(c);

            c = new esColumnMetadata(AssetPreventiveMaintenanceMetadata.ColumnNames.SRWorkTrade, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = AssetPreventiveMaintenanceMetadata.PropertyNames.SRWorkTrade;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(AssetPreventiveMaintenanceMetadata.ColumnNames.TargetDate, 5, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = AssetPreventiveMaintenanceMetadata.PropertyNames.TargetDate;
            _columns.Add(c);

            c = new esColumnMetadata(AssetPreventiveMaintenanceMetadata.ColumnNames.IsApproved, 6, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = AssetPreventiveMaintenanceMetadata.PropertyNames.IsApproved;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AssetPreventiveMaintenanceMetadata.ColumnNames.IsVoid, 7, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = AssetPreventiveMaintenanceMetadata.PropertyNames.IsVoid;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AssetPreventiveMaintenanceMetadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = AssetPreventiveMaintenanceMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AssetPreventiveMaintenanceMetadata.ColumnNames.LastUpdateByUserID, 9, typeof(System.String), esSystemType.String);
            c.PropertyName = AssetPreventiveMaintenanceMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public AssetPreventiveMaintenanceMetadata Meta()
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
            public const string PMNo = "PMNo";
            public const string PMDate = "PMDate";
            public const string ServiceUnitID = "ServiceUnitID";
            public const string AssetID = "AssetID";
            public const string SRWorkTrade = "SRWorkTrade";
            public const string TargetDate = "TargetDate";
            public const string IsApproved = "IsApproved";
            public const string IsVoid = "IsVoid";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string PMNo = "PMNo";
            public const string PMDate = "PMDate";
            public const string ServiceUnitID = "ServiceUnitID";
            public const string AssetID = "AssetID";
            public const string SRWorkTrade = "SRWorkTrade";
            public const string TargetDate = "TargetDate";
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
            lock (typeof(AssetPreventiveMaintenanceMetadata))
            {
                if (AssetPreventiveMaintenanceMetadata.mapDelegates == null)
                {
                    AssetPreventiveMaintenanceMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (AssetPreventiveMaintenanceMetadata.meta == null)
                {
                    AssetPreventiveMaintenanceMetadata.meta = new AssetPreventiveMaintenanceMetadata();
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


                meta.AddTypeMap("PMNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("PMDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("AssetID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRWorkTrade", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("TargetDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));



                meta.Source = "AssetPreventiveMaintenance";
                meta.Destination = "AssetPreventiveMaintenance";

                meta.spInsert = "proc_AssetPreventiveMaintenanceInsert";
                meta.spUpdate = "proc_AssetPreventiveMaintenanceUpdate";
                meta.spDelete = "proc_AssetPreventiveMaintenanceDelete";
                meta.spLoadAll = "proc_AssetPreventiveMaintenanceLoadAll";
                meta.spLoadByPrimaryKey = "proc_AssetPreventiveMaintenanceLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private AssetPreventiveMaintenanceMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
