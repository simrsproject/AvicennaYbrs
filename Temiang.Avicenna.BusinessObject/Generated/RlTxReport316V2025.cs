/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.16V202514.0
Driver          : SQL
Date Generated  : 3/4/2013 16V2025:20:15 PM
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
    abstract public class esRlTxReport316V2025Collection : esEntityCollectionWAuditLog
    {
        public esRlTxReport316V2025Collection()
        {

        }

        protected override string GetCollectionName()
        {
            return "RlTxReport316V2025Collection";
        }

        #region Query Logic
        protected void InitQuery(esRlTxReport316V2025Query query)
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
            this.InitQuery(query as esRlTxReport316V2025Query);
        }
        #endregion

        virtual public RlTxReport316V2025 DetachEntity(RlTxReport316V2025 entity)
        {
            return base.DetachEntity(entity) as RlTxReport316V2025;
        }

        virtual public RlTxReport316V2025 AttachEntity(RlTxReport316V2025 entity)
        {
            return base.AttachEntity(entity) as RlTxReport316V2025;
        }

        virtual public void Combine(RlTxReport316V2025Collection collection)
        {
            base.Combine(collection);
        }

        new public RlTxReport316V2025 this[int index]
        {
            get
            {
                return base[index] as RlTxReport316V2025;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(RlTxReport316V2025);
        }
    }



    [Serializable]
    abstract public class esRlTxReport316V2025 : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esRlTxReport316V2025Query GetDynamicQuery()
        {
            return null;
        }

        public esRlTxReport316V2025()
        {

        }

        public esRlTxReport316V2025(DataRow row)
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
            esRlTxReport316V2025Query query = this.GetDynamicQuery();
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
                        case "PKBPascaPersalinan": this.str.PKBPascaPersalinan = (string)value; break;
                        case "PKBPascaKeguguran": this.str.PKBPascaKeguguran = (string)value; break;
                        case "PKBInterval": this.str.PKBInterval = (string)value; break;
                        case "PKBTotal": this.str.PKBTotal = (string)value; break;
                        case "KomplikasiKB": this.str.KomplikasiKB = (string)value; break;
                        case "KegagalanKB": this.str.KegagalanKB = (string)value; break;
                        case "EfekSamping": this.str.EfekSamping = (string)value; break;
                        case "DropOut": this.str.DropOut = (string)value; break;
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

                        case "PKBPascaPersalinan":

                            if (value == null || value is System.Int32)
                                this.PKBPascaPersalinan = (System.Int32?)value;
                            break;

                        case "PKBPascaKeguguran":

                            if (value == null || value is System.Int32)
                                this.PKBPascaKeguguran = (System.Int32?)value;
                            break;

                        case "PKBInterval":

                            if (value == null || value is System.Int32)
                                this.PKBInterval = (System.Int32?)value;
                            break;

                        case "PKBTotal":

                            if (value == null || value is System.Int32)
                                this.PKBTotal = (System.Int32?)value;
                            break;

                        case "KomplikasiKB":

                            if (value == null || value is System.Int32)
                                this.KomplikasiKB = (System.Int32?)value;
                            break;

                        case "KegagalanKB":

                            if (value == null || value is System.Int32)
                                this.KegagalanKB = (System.Int32?)value;
                            break;

                        case "EfekSamping":

                            if (value == null || value is System.Int32)
                                this.EfekSamping = (System.Int32?)value;
                            break;

                        case "DropOut":

                            if (value == null || value is System.Int32)
                                this.DropOut = (System.Int32?)value;
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
        /// Maps to RlTxReport3_16V2025.RlTxReportNo
        /// </summary>
        virtual public System.String RlTxReportNo
        {
            get
            {
                return base.GetSystemString(RlTxReport316V2025Metadata.ColumnNames.RlTxReportNo);
            }

            set
            {
                base.SetSystemString(RlTxReport316V2025Metadata.ColumnNames.RlTxReportNo, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_16V2025.RlMasterReportItemID
        /// </summary>
        virtual public System.Int32? RlMasterReportItemID
        {
            get
            {
                return base.GetSystemInt32(RlTxReport316V2025Metadata.ColumnNames.RlMasterReportItemID);
            }

            set
            {
                base.SetSystemInt32(RlTxReport316V2025Metadata.ColumnNames.RlMasterReportItemID, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_16V2025.PKBPascaPersalinan
        /// </summary>
        virtual public System.Int32? PKBPascaPersalinan
        {
            get
            {
                return base.GetSystemInt32(RlTxReport316V2025Metadata.ColumnNames.PKBPascaPersalinan);
            }

            set
            {
                base.SetSystemInt32(RlTxReport316V2025Metadata.ColumnNames.PKBPascaPersalinan, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_16V2025.PKBPascaKeguguran
        /// </summary>
        virtual public System.Int32? PKBPascaKeguguran
        {
            get
            {
                return base.GetSystemInt32(RlTxReport316V2025Metadata.ColumnNames.PKBPascaKeguguran);
            }

            set
            {
                base.SetSystemInt32(RlTxReport316V2025Metadata.ColumnNames.PKBPascaKeguguran, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_16V2025.PKBInterval
        /// </summary>
        virtual public System.Int32? PKBInterval
        {
            get
            {
                return base.GetSystemInt32(RlTxReport316V2025Metadata.ColumnNames.PKBInterval);
            }

            set
            {
                base.SetSystemInt32(RlTxReport316V2025Metadata.ColumnNames.PKBInterval, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_16V2025.PKBTotal
        /// </summary>
        virtual public System.Int32? PKBTotal
        {
            get
            {
                return base.GetSystemInt32(RlTxReport316V2025Metadata.ColumnNames.PKBTotal);
            }

            set
            {
                base.SetSystemInt32(RlTxReport316V2025Metadata.ColumnNames.PKBTotal, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_16V2025.KomplikasiKB
        /// </summary>
        virtual public System.Int32? KomplikasiKB
        {
            get
            {
                return base.GetSystemInt32(RlTxReport316V2025Metadata.ColumnNames.KomplikasiKB);
            }

            set
            {
                base.SetSystemInt32(RlTxReport316V2025Metadata.ColumnNames.KomplikasiKB, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_16V2025.KegagalanKB
        /// </summary>
        virtual public System.Int32? KegagalanKB
        {
            get
            {
                return base.GetSystemInt32(RlTxReport316V2025Metadata.ColumnNames.KegagalanKB);
            }

            set
            {
                base.SetSystemInt32(RlTxReport316V2025Metadata.ColumnNames.KegagalanKB, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_16V2025.EfekSamping
        /// </summary>
        virtual public System.Int32? EfekSamping
        {
            get
            {
                return base.GetSystemInt32(RlTxReport316V2025Metadata.ColumnNames.EfekSamping);
            }

            set
            {
                base.SetSystemInt32(RlTxReport316V2025Metadata.ColumnNames.EfekSamping, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_16V2025.DropOut
        /// </summary>
        virtual public System.Int32? DropOut
        {
            get
            {
                return base.GetSystemInt32(RlTxReport316V2025Metadata.ColumnNames.DropOut);
            }

            set
            {
                base.SetSystemInt32(RlTxReport316V2025Metadata.ColumnNames.DropOut, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_16V2025.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(RlTxReport316V2025Metadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(RlTxReport316V2025Metadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_16V2025.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(RlTxReport316V2025Metadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(RlTxReport316V2025Metadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esRlTxReport316V2025 entity)
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

            public System.String PKBPascaPersalinan
            {
                get
                {
                    System.Int32? data = entity.PKBPascaPersalinan;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PKBPascaPersalinan = null;
                    else entity.PKBPascaPersalinan = Convert.ToInt32(value);
                }
            }

            public System.String PKBPascaKeguguran
            {
                get
                {
                    System.Int32? data = entity.PKBPascaKeguguran;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PKBPascaKeguguran = null;
                    else entity.PKBPascaKeguguran = Convert.ToInt32(value);
                }
            }

            public System.String PKBInterval
            {
                get
                {
                    System.Int32? data = entity.PKBInterval;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PKBInterval = null;
                    else entity.PKBInterval = Convert.ToInt32(value);
                }
            }

            public System.String PKBTotal
            {
                get
                {
                    System.Int32? data = entity.PKBTotal;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PKBTotal = null;
                    else entity.PKBTotal = Convert.ToInt32(value);
                }
            }

            public System.String KomplikasiKB
            {
                get
                {
                    System.Int32? data = entity.KomplikasiKB;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.KomplikasiKB = null;
                    else entity.KomplikasiKB = Convert.ToInt32(value);
                }
            }

            public System.String KegagalanKB
            {
                get
                {
                    System.Int32? data = entity.KegagalanKB;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.KegagalanKB = null;
                    else entity.KegagalanKB = Convert.ToInt32(value);
                }
            }

            public System.String EfekSamping
            {
                get
                {
                    System.Int32? data = entity.EfekSamping;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.EfekSamping = null;
                    else entity.EfekSamping = Convert.ToInt32(value);
                }
            }

            public System.String DropOut
            {
                get
                {
                    System.Int32? data = entity.DropOut;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DropOut = null;
                    else entity.DropOut = Convert.ToInt32(value);
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


            private esRlTxReport316V2025 entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esRlTxReport316V2025Query query)
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
                throw new Exception("esRlTxReport316V2025 can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    public partial class RlTxReport316V2025 : esRlTxReport316V2025
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
    abstract public class esRlTxReport316V2025Query : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return RlTxReport316V2025Metadata.Meta();
            }
        }


        public esQueryItem RlTxReportNo
        {
            get
            {
                return new esQueryItem(this, RlTxReport316V2025Metadata.ColumnNames.RlTxReportNo, esSystemType.String);
            }
        }

        public esQueryItem RlMasterReportItemID
        {
            get
            {
                return new esQueryItem(this, RlTxReport316V2025Metadata.ColumnNames.RlMasterReportItemID, esSystemType.Int32);
            }
        }

        public esQueryItem PKBPascaPersalinan
        {
            get
            {
                return new esQueryItem(this, RlTxReport316V2025Metadata.ColumnNames.PKBPascaPersalinan, esSystemType.Int32);
            }
        }

        public esQueryItem PKBPascaKeguguran
        {
            get
            {
                return new esQueryItem(this, RlTxReport316V2025Metadata.ColumnNames.PKBPascaKeguguran, esSystemType.Int32);
            }
        }

        public esQueryItem PKBInterval
        {
            get
            {
                return new esQueryItem(this, RlTxReport316V2025Metadata.ColumnNames.PKBInterval, esSystemType.Int32);
            }
        }

        public esQueryItem PKBTotal
        {
            get
            {
                return new esQueryItem(this, RlTxReport316V2025Metadata.ColumnNames.PKBTotal, esSystemType.Int32);
            }
        }

        public esQueryItem KomplikasiKB
        {
            get
            {
                return new esQueryItem(this, RlTxReport316V2025Metadata.ColumnNames.KomplikasiKB, esSystemType.Int32);
            }
        }

        public esQueryItem KegagalanKB
        {
            get
            {
                return new esQueryItem(this, RlTxReport316V2025Metadata.ColumnNames.KegagalanKB, esSystemType.Int32);
            }
        }

        public esQueryItem EfekSamping
        {
            get
            {
                return new esQueryItem(this, RlTxReport316V2025Metadata.ColumnNames.EfekSamping, esSystemType.Int32);
            }
        }

        public esQueryItem DropOut
        {
            get
            {
                return new esQueryItem(this, RlTxReport316V2025Metadata.ColumnNames.DropOut, esSystemType.Int32);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, RlTxReport316V2025Metadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, RlTxReport316V2025Metadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("RlTxReport316V2025Collection")]
    public partial class RlTxReport316V2025Collection : esRlTxReport316V2025Collection, IEnumerable<RlTxReport316V2025>
    {
        public RlTxReport316V2025Collection()
        {

        }

        public static implicit operator List<RlTxReport316V2025>(RlTxReport316V2025Collection coll)
        {
            List<RlTxReport316V2025> list = new List<RlTxReport316V2025>();

            foreach (RlTxReport316V2025 emp in coll)
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
                return RlTxReport316V2025Metadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new RlTxReport316V2025Query();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new RlTxReport316V2025(row);
        }

        override protected esEntity CreateEntity()
        {
            return new RlTxReport316V2025();
        }


        #endregion


        [BrowsableAttribute(false)]
        public RlTxReport316V2025Query Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new RlTxReport316V2025Query();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(RlTxReport316V2025Query query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public RlTxReport316V2025 AddNew()
        {
            RlTxReport316V2025 entity = base.AddNewEntity() as RlTxReport316V2025;

            return entity;
        }

        public RlTxReport316V2025 FindByPrimaryKey(System.String rlTxReportNo, System.Int32 rlMasterReportItemID)
        {
            return base.FindByPrimaryKey(rlTxReportNo, rlMasterReportItemID) as RlTxReport316V2025;
        }


        #region IEnumerable<RlTxReport316V2025> Members

        IEnumerator<RlTxReport316V2025> IEnumerable<RlTxReport316V2025>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as RlTxReport316V2025;
            }
        }

        #endregion

        private RlTxReport316V2025Query query;
    }


    /// <summary>
    /// Encapsulates the 'RlTxReport3_16V2025' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("RlTxReport316V2025 ({RlTxReportNo},{RlMasterReportItemID})")]
    [Serializable]
    public partial class RlTxReport316V2025 : esRlTxReport316V2025
    {
        public RlTxReport316V2025()
        {

        }

        public RlTxReport316V2025(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return RlTxReport316V2025Metadata.Meta();
            }
        }



        override protected esRlTxReport316V2025Query GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new RlTxReport316V2025Query();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public RlTxReport316V2025Query Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new RlTxReport316V2025Query();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(RlTxReport316V2025Query query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private RlTxReport316V2025Query query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class RlTxReport316V2025Query : esRlTxReport316V2025Query
    {
        public RlTxReport316V2025Query()
        {

        }

        public RlTxReport316V2025Query(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "RlTxReport316V2025Query";
        }


    }


    [Serializable]
    public partial class RlTxReport316V2025Metadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected RlTxReport316V2025Metadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(RlTxReport316V2025Metadata.ColumnNames.RlTxReportNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = RlTxReport316V2025Metadata.PropertyNames.RlTxReportNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport316V2025Metadata.ColumnNames.RlMasterReportItemID, 1, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport316V2025Metadata.PropertyNames.RlMasterReportItemID;
            c.IsInPrimaryKey = true;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport316V2025Metadata.ColumnNames.PKBPascaPersalinan, 2, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport316V2025Metadata.PropertyNames.PKBPascaPersalinan;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport316V2025Metadata.ColumnNames.PKBPascaKeguguran, 3, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport316V2025Metadata.PropertyNames.PKBPascaKeguguran;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport316V2025Metadata.ColumnNames.PKBInterval, 4, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport316V2025Metadata.PropertyNames.PKBInterval;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport316V2025Metadata.ColumnNames.PKBTotal, 5, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport316V2025Metadata.PropertyNames.PKBTotal;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport316V2025Metadata.ColumnNames.KomplikasiKB, 6, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport316V2025Metadata.PropertyNames.KomplikasiKB;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport316V2025Metadata.ColumnNames.KegagalanKB, 7, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport316V2025Metadata.PropertyNames.KegagalanKB;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport316V2025Metadata.ColumnNames.EfekSamping, 8, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport316V2025Metadata.PropertyNames.EfekSamping;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport316V2025Metadata.ColumnNames.DropOut, 9, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport316V2025Metadata.PropertyNames.DropOut;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport316V2025Metadata.ColumnNames.LastUpdateDateTime, 14, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = RlTxReport316V2025Metadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport316V2025Metadata.ColumnNames.LastUpdateByUserID, 15, typeof(System.String), esSystemType.String);
            c.PropertyName = RlTxReport316V2025Metadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public RlTxReport316V2025Metadata Meta()
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
            public const string PKBPascaPersalinan = "PKBPascaPersalinan";
            public const string PKBPascaKeguguran = "PKBPascaKeguguran";
            public const string PKBInterval = "PKBInterval";
            public const string PKBTotal = "PKBTotal";
            public const string KomplikasiKB = "KomplikasiKB";
            public const string KegagalanKB = "KegagalanKB";
            public const string EfekSamping = "EfekSamping";
            public const string DropOut = "DropOut";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string RlTxReportNo = "RlTxReportNo";
            public const string RlMasterReportItemID = "RlMasterReportItemID";
            public const string PKBPascaPersalinan = "PKBPascaPersalinan";
            public const string PKBPascaKeguguran = "PKBPascaKeguguran";
            public const string PKBInterval = "PKBInterval";
            public const string PKBTotal = "PKBTotal";
            public const string KomplikasiKB = "KomplikasiKB";
            public const string KegagalanKB = "KegagalanKB";
            public const string EfekSamping = "EfekSamping";
            public const string DropOut = "DropOut";
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
            lock (typeof(RlTxReport316V2025Metadata))
            {
                if (RlTxReport316V2025Metadata.mapDelegates == null)
                {
                    RlTxReport316V2025Metadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (RlTxReport316V2025Metadata.meta == null)
                {
                    RlTxReport316V2025Metadata.meta = new RlTxReport316V2025Metadata();
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
                meta.AddTypeMap("PKBPascaPersalinan", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("PKBPascaKeguguran", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("PKBInterval", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("PKBTotal", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("KomplikasiKB", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("KegagalanKB", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("EfekSamping", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("DropOut", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));



                meta.Source = "RlTxReport3_16V2025";
                meta.Destination = "RlTxReport3_16V2025";

                meta.spInsert = "proc_RlTxReport3_16V2025Insert";
                meta.spUpdate = "proc_RlTxReport3_16V2025Update";
                meta.spDelete = "proc_RlTxReport3_16V2025Delete";
                meta.spLoadAll = "proc_RlTxReport3_16V2025LoadAll";
                meta.spLoadByPrimaryKey = "proc_RlTxReport3_16V2025LoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private RlTxReport316V2025Metadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
