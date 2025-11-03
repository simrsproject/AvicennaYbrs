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
    abstract public class esRlTxReport312V2025Collection : esEntityCollectionWAuditLog
    {
        public esRlTxReport312V2025Collection()
        {

        }

        protected override string GetCollectionName()
        {
            return "RlTxReport312V2025Collection";
        }

        #region Query Logic
        protected void InitQuery(esRlTxReport312V2025Query query)
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
            this.InitQuery(query as esRlTxReport312V2025Query);
        }
        #endregion

        virtual public RlTxReport312V2025 DetachEntity(RlTxReport312V2025 entity)
        {
            return base.DetachEntity(entity) as RlTxReport312V2025;
        }

        virtual public RlTxReport312V2025 AttachEntity(RlTxReport312V2025 entity)
        {
            return base.AttachEntity(entity) as RlTxReport312V2025;
        }

        virtual public void Combine(RlTxReport312V2025Collection collection)
        {
            base.Combine(collection);
        }

        new public RlTxReport312V2025 this[int index]
        {
            get
            {
                return base[index] as RlTxReport312V2025;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(RlTxReport312V2025);
        }
    }



    [Serializable]
    abstract public class esRlTxReport312V2025 : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esRlTxReport312V2025Query GetDynamicQuery()
        {
            return null;
        }

        public esRlTxReport312V2025()
        {

        }

        public esRlTxReport312V2025(DataRow row)
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
            esRlTxReport312V2025Query query = this.GetDynamicQuery();
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
                        case "Khusus": this.str.Khusus = (string)value; break;
                        case "Besar": this.str.Besar = (string)value; break;
                        case "Sedang": this.str.Sedang = (string)value; break;
                        case "Kecil": this.str.Kecil = (string)value; break;
                        case "Total": this.str.Total = (string)value; break;
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

                        case "Total":

                            if (value == null || value is System.Int32)
                                this.Total = (System.Int32?)value;
                            break;

                        case "Khusus":

                            if (value == null || value is System.Int32)
                                this.Khusus = (System.Int32?)value;
                            break;

                        case "Besar":

                            if (value == null || value is System.Int32)
                                this.Besar = (System.Int32?)value;
                            break;

                        case "Sedang":

                            if (value == null || value is System.Int32)
                                this.Sedang = (System.Int32?)value;
                            break;

                        case "Kecil":

                            if (value == null || value is System.Int32)
                                this.Kecil = (System.Int32?)value;
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
        /// Maps to RlTxReport3_12.RlTxReportNo
        /// </summary>
        virtual public System.String RlTxReportNo
        {
            get
            {
                return base.GetSystemString(RlTxReport312V2025Metadata.ColumnNames.RlTxReportNo);
            }

            set
            {
                base.SetSystemString(RlTxReport312V2025Metadata.ColumnNames.RlTxReportNo, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_12.RlMasterReportItemID
        /// </summary>
        virtual public System.Int32? RlMasterReportItemID
        {
            get
            {
                return base.GetSystemInt32(RlTxReport312V2025Metadata.ColumnNames.RlMasterReportItemID);
            }

            set
            {
                base.SetSystemInt32(RlTxReport312V2025Metadata.ColumnNames.RlMasterReportItemID, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_12.Total
        /// </summary>
        virtual public System.Int32? Total
        {
            get
            {
                return base.GetSystemInt32(RlTxReport312V2025Metadata.ColumnNames.Total);
            }

            set
            {
                base.SetSystemInt32(RlTxReport312V2025Metadata.ColumnNames.Total, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_12.Khusus
        /// </summary>
        virtual public System.Int32? Khusus
        {
            get
            {
                return base.GetSystemInt32(RlTxReport312V2025Metadata.ColumnNames.Khusus);
            }

            set
            {
                base.SetSystemInt32(RlTxReport312V2025Metadata.ColumnNames.Khusus, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_12.Besar
        /// </summary>
        virtual public System.Int32? Besar
        {
            get
            {
                return base.GetSystemInt32(RlTxReport312V2025Metadata.ColumnNames.Besar);
            }

            set
            {
                base.SetSystemInt32(RlTxReport312V2025Metadata.ColumnNames.Besar, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_12.Sedang
        /// </summary>
        virtual public System.Int32? Sedang
        {
            get
            {
                return base.GetSystemInt32(RlTxReport312V2025Metadata.ColumnNames.Sedang);
            }

            set
            {
                base.SetSystemInt32(RlTxReport312V2025Metadata.ColumnNames.Sedang, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_12.Kecil
        /// </summary>
        virtual public System.Int32? Kecil
        {
            get
            {
                return base.GetSystemInt32(RlTxReport312V2025Metadata.ColumnNames.Kecil);
            }

            set
            {
                base.SetSystemInt32(RlTxReport312V2025Metadata.ColumnNames.Kecil, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_12.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(RlTxReport312V2025Metadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(RlTxReport312V2025Metadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_12.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(RlTxReport312V2025Metadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(RlTxReport312V2025Metadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esRlTxReport312V2025 entity)
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

            public System.String Total
            {
                get
                {
                    System.Int32? data = entity.Total;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Total = null;
                    else entity.Total = Convert.ToInt32(value);
                }
            }

            public System.String Khusus
            {
                get
                {
                    System.Int32? data = entity.Khusus;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Khusus = null;
                    else entity.Khusus = Convert.ToInt32(value);
                }
            }

            public System.String Besar
            {
                get
                {
                    System.Int32? data = entity.Besar;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Besar = null;
                    else entity.Besar = Convert.ToInt32(value);
                }
            }

            public System.String Sedang
            {
                get
                {
                    System.Int32? data = entity.Sedang;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Sedang = null;
                    else entity.Sedang = Convert.ToInt32(value);
                }
            }

            public System.String Kecil
            {
                get
                {
                    System.Int32? data = entity.Kecil;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Kecil = null;
                    else entity.Kecil = Convert.ToInt32(value);
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


            private esRlTxReport312V2025 entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esRlTxReport312V2025Query query)
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
                throw new Exception("esRlTxReport312V2025 can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    public partial class RlTxReport312V2025 : esRlTxReport312V2025
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
    abstract public class esRlTxReport312V2025Query : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return RlTxReport312V2025Metadata.Meta();
            }
        }


        public esQueryItem RlTxReportNo
        {
            get
            {
                return new esQueryItem(this, RlTxReport312V2025Metadata.ColumnNames.RlTxReportNo, esSystemType.String);
            }
        }

        public esQueryItem RlMasterReportItemID
        {
            get
            {
                return new esQueryItem(this, RlTxReport312V2025Metadata.ColumnNames.RlMasterReportItemID, esSystemType.Int32);
            }
        }

        public esQueryItem Total
        {
            get
            {
                return new esQueryItem(this, RlTxReport312V2025Metadata.ColumnNames.Total, esSystemType.Int32);
            }
        }

        public esQueryItem Khusus
        {
            get
            {
                return new esQueryItem(this, RlTxReport312V2025Metadata.ColumnNames.Khusus, esSystemType.Int32);
            }
        }

        public esQueryItem Besar
        {
            get
            {
                return new esQueryItem(this, RlTxReport312V2025Metadata.ColumnNames.Besar, esSystemType.Int32);
            }
        }

        public esQueryItem Sedang
        {
            get
            {
                return new esQueryItem(this, RlTxReport312V2025Metadata.ColumnNames.Sedang, esSystemType.Int32);
            }
        }

        public esQueryItem Kecil
        {
            get
            {
                return new esQueryItem(this, RlTxReport312V2025Metadata.ColumnNames.Kecil, esSystemType.Int32);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, RlTxReport312V2025Metadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, RlTxReport312V2025Metadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("RlTxReport312V2025Collection")]
    public partial class RlTxReport312V2025Collection : esRlTxReport312V2025Collection, IEnumerable<RlTxReport312V2025>
    {
        public RlTxReport312V2025Collection()
        {

        }

        public static implicit operator List<RlTxReport312V2025>(RlTxReport312V2025Collection coll)
        {
            List<RlTxReport312V2025> list = new List<RlTxReport312V2025>();

            foreach (RlTxReport312V2025 emp in coll)
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
                return RlTxReport312V2025Metadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new RlTxReport312V2025Query();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new RlTxReport312V2025(row);
        }

        override protected esEntity CreateEntity()
        {
            return new RlTxReport312V2025();
        }


        #endregion


        [BrowsableAttribute(false)]
        public RlTxReport312V2025Query Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new RlTxReport312V2025Query();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(RlTxReport312V2025Query query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public RlTxReport312V2025 AddNew()
        {
            RlTxReport312V2025 entity = base.AddNewEntity() as RlTxReport312V2025;

            return entity;
        }

        public RlTxReport312V2025 FindByPrimaryKey(System.String rlTxReportNo, System.Int32 rlMasterReportItemID)
        {
            return base.FindByPrimaryKey(rlTxReportNo, rlMasterReportItemID) as RlTxReport312V2025;
        }


        #region IEnumerable<RlTxReport312V2025> Members

        IEnumerator<RlTxReport312V2025> IEnumerable<RlTxReport312V2025>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as RlTxReport312V2025;
            }
        }

        #endregion

        private RlTxReport312V2025Query query;
    }


    /// <summary>
    /// Encapsulates the 'RlTxReport3_12' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("RlTxReport312V2025 ({RlTxReportNo},{RlMasterReportItemID})")]
    [Serializable]
    public partial class RlTxReport312V2025 : esRlTxReport312V2025
    {
        public RlTxReport312V2025()
        {

        }

        public RlTxReport312V2025(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return RlTxReport312V2025Metadata.Meta();
            }
        }



        override protected esRlTxReport312V2025Query GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new RlTxReport312V2025Query();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public RlTxReport312V2025Query Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new RlTxReport312V2025Query();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(RlTxReport312V2025Query query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private RlTxReport312V2025Query query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class RlTxReport312V2025Query : esRlTxReport312V2025Query
    {
        public RlTxReport312V2025Query()
        {

        }

        public RlTxReport312V2025Query(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "RlTxReport312V2025Query";
        }


    }


    [Serializable]
    public partial class RlTxReport312V2025Metadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected RlTxReport312V2025Metadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(RlTxReport312V2025Metadata.ColumnNames.RlTxReportNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = RlTxReport312V2025Metadata.PropertyNames.RlTxReportNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport312V2025Metadata.ColumnNames.RlMasterReportItemID, 1, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport312V2025Metadata.PropertyNames.RlMasterReportItemID;
            c.IsInPrimaryKey = true;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport312V2025Metadata.ColumnNames.Total, 2, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport312V2025Metadata.PropertyNames.Total;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport312V2025Metadata.ColumnNames.Khusus, 3, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport312V2025Metadata.PropertyNames.Khusus;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport312V2025Metadata.ColumnNames.Besar, 4, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport312V2025Metadata.PropertyNames.Besar;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport312V2025Metadata.ColumnNames.Sedang, 5, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport312V2025Metadata.PropertyNames.Sedang;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport312V2025Metadata.ColumnNames.Kecil, 6, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport312V2025Metadata.PropertyNames.Kecil;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport312V2025Metadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = RlTxReport312V2025Metadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport312V2025Metadata.ColumnNames.LastUpdateByUserID, 8, typeof(System.String), esSystemType.String);
            c.PropertyName = RlTxReport312V2025Metadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public RlTxReport312V2025Metadata Meta()
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
            public const string Khusus = "Khusus";
            public const string Besar = "Besar";
            public const string Sedang = "Sedang";
            public const string Kecil = "Kecil";
            public const string Total = "Total";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string RlTxReportNo = "RlTxReportNo";
            public const string RlMasterReportItemID = "RlMasterReportItemID";
            public const string Khusus = "Khusus";
            public const string Besar = "Besar";
            public const string Sedang = "Sedang";
            public const string Kecil = "Kecil";
            public const string Total = "Total";
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
            lock (typeof(RlTxReport312V2025Metadata))
            {
                if (RlTxReport312V2025Metadata.mapDelegates == null)
                {
                    RlTxReport312V2025Metadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (RlTxReport312V2025Metadata.meta == null)
                {
                    RlTxReport312V2025Metadata.meta = new RlTxReport312V2025Metadata();
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
                meta.AddTypeMap("Khusus", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("Besar", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("Sedang", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("Kecil", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("Total", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));



                meta.Source = "RlTxReport3_12V2025";
                meta.Destination = "RlTxReport3_12V2025";

                meta.spInsert = "proc_RlTxReport3_12V2025Insert";
                meta.spUpdate = "proc_RlTxReport3_12V2025Update";
                meta.spDelete = "proc_RlTxReport3_12V2025Delete";
                meta.spLoadAll = "proc_RlTxReport3_12V2025LoadAll";
                meta.spLoadByPrimaryKey = "proc_RlTxReport3_12V2025LoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private RlTxReport312V2025Metadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
