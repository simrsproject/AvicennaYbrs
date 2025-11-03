/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 3/4/2013 12:20:16 PM
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
    abstract public class esRlTxReport35V2025Collection : esEntityCollectionWAuditLog
    {
        public esRlTxReport35V2025Collection()
        {

        }

        protected override string GetCollectionName()
        {
            return "RlTxReport35V2025Collection";
        }

        #region Query Logic
        protected void InitQuery(esRlTxReport35V2025Query query)
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
            this.InitQuery(query as esRlTxReport35V2025Query);
        }
        #endregion

        virtual public RlTxReport35V2025 DetachEntity(RlTxReport35V2025 entity)
        {
            return base.DetachEntity(entity) as RlTxReport35V2025;
        }

        virtual public RlTxReport35V2025 AttachEntity(RlTxReport35V2025 entity)
        {
            return base.AttachEntity(entity) as RlTxReport35V2025;
        }

        virtual public void Combine(RlTxReport35V2025Collection collection)
        {
            base.Combine(collection);
        }

        new public RlTxReport35V2025 this[int index]
        {
            get
            {
                return base[index] as RlTxReport35V2025;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(RlTxReport35V2025);
        }
    }



    [Serializable]
    abstract public class esRlTxReport35V2025 : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esRlTxReport35V2025Query GetDynamicQuery()
        {
            return null;
        }

        public esRlTxReport35V2025()
        {

        }

        public esRlTxReport35V2025(DataRow row)
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
            esRlTxReport35V2025Query query = this.GetDynamicQuery();
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
                        case "Jumlah": this.str.Jumlah = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "JumlahLaki": this.str.JumlahLaki = (string)value; break;
                        case "JumlahPerempuan": this.str.JumlahPerempuan = (string)value; break;

                        case "JumlahLaki2": this.str.JumlahLaki2 = (string)value; break;
                        case "JumlahPerempuan2": this.str.JumlahPerempuan2 = (string)value; break;
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



                        case "Jumlah":

                            if (value == null || value is System.Int32)
                                this.Jumlah = (System.Int32?)value;
                            break;

                        case "LastUpdateDateTime":

                            if (value == null || value is System.DateTime)
                                this.LastUpdateDateTime = (System.DateTime?)value;
                            break;

                        case "JumlahLaki":

                            if (value == null || value is System.Int32)
                                this.JumlahLaki = (System.Int32?)value;
                            break;

                        case "JumlahPerempuan":

                            if (value == null || value is System.Int32)
                                this.JumlahPerempuan = (System.Int32?)value;
                            break;

                        case "JumlahLaki2":

                            if (value == null || value is System.Int32)
                                this.JumlahLaki2 = (System.Int32?)value;
                            break;

                        case "JumlahPerempuan2":

                            if (value == null || value is System.Int32)
                                this.JumlahPerempuan2 = (System.Int32?)value;
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
        /// Maps to RlTxReport3_5V2025.RlTxReportNo
        /// </summary>
        virtual public System.String RlTxReportNo
        {
            get
            {
                return base.GetSystemString(RlTxReport35V2025Metadata.ColumnNames.RlTxReportNo);
            }

            set
            {
                base.SetSystemString(RlTxReport35V2025Metadata.ColumnNames.RlTxReportNo, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_5V2025.RlMasterReportItemID
        /// </summary>
        virtual public System.Int32? RlMasterReportItemID
        {
            get
            {
                return base.GetSystemInt32(RlTxReport35V2025Metadata.ColumnNames.RlMasterReportItemID);
            }

            set
            {
                base.SetSystemInt32(RlTxReport35V2025Metadata.ColumnNames.RlMasterReportItemID, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_5V2025.Jumlah
        /// </summary>
        virtual public System.Int32? Jumlah
        {
            get
            {
                return base.GetSystemInt32(RlTxReport35V2025Metadata.ColumnNames.Jumlah);
            }

            set
            {
                base.SetSystemInt32(RlTxReport35V2025Metadata.ColumnNames.Jumlah, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_5V2025.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(RlTxReport35V2025Metadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(RlTxReport35V2025Metadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_5V2025.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(RlTxReport35V2025Metadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(RlTxReport35V2025Metadata.ColumnNames.LastUpdateByUserID, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_5V2025.JumlahLaki
        /// </summary>
        virtual public System.Int32? JumlahLaki
        {
            get
            {
                return base.GetSystemInt32(RlTxReport35V2025Metadata.ColumnNames.JumlahLaki);
            }

            set
            {
                base.SetSystemInt32(RlTxReport35V2025Metadata.ColumnNames.JumlahLaki, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_5V2025.JumlahPerempuan
        /// </summary>
        virtual public System.Int32? JumlahPerempuan
        {
            get
            {
                return base.GetSystemInt32(RlTxReport35V2025Metadata.ColumnNames.JumlahPerempuan);
            }

            set
            {
                base.SetSystemInt32(RlTxReport35V2025Metadata.ColumnNames.JumlahPerempuan, value);
            }
        }



        virtual public System.Int32? JumlahLaki2
        {
            get
            {
                return base.GetSystemInt32(RlTxReport35V2025Metadata.ColumnNames.JumlahLaki2);
            }

            set
            {
                base.SetSystemInt32(RlTxReport35V2025Metadata.ColumnNames.JumlahLaki2, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_5V2025.JumlahPerempuan
        /// </summary>
        virtual public System.Int32? JumlahPerempuan2
        {
            get
            {
                return base.GetSystemInt32(RlTxReport35V2025Metadata.ColumnNames.JumlahPerempuan2);
            }

            set
            {
                base.SetSystemInt32(RlTxReport35V2025Metadata.ColumnNames.JumlahPerempuan2, value);
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
            public esStrings(esRlTxReport35V2025 entity)
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



            public System.String Jumlah
            {
                get
                {
                    System.Int32? data = entity.Jumlah;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Jumlah = null;
                    else entity.Jumlah = Convert.ToInt32(value);
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

            public System.String JumlahLaki
            {
                get
                {
                    System.Int32? data = entity.JumlahLaki;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.JumlahLaki = null;
                    else entity.JumlahLaki = Convert.ToInt32(value);
                }
            }

            public System.String JumlahPerempuan
            {
                get
                {
                    System.Int32? data = entity.JumlahPerempuan;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.JumlahPerempuan = null;
                    else entity.JumlahPerempuan = Convert.ToInt32(value);
                }
            }






            public System.String JumlahLaki2
            {
                get
                {
                    System.Int32? data = entity.JumlahLaki2;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.JumlahLaki2 = null;
                    else entity.JumlahLaki2 = Convert.ToInt32(value);
                }
            }

            public System.String JumlahPerempuan2
            {
                get
                {
                    System.Int32? data = entity.JumlahPerempuan2;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.JumlahPerempuan2 = null;
                    else entity.JumlahPerempuan2 = Convert.ToInt32(value);
                }
            }


            private esRlTxReport35V2025 entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esRlTxReport35V2025Query query)
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
                throw new Exception("esRlTxReport35V2025 can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    public partial class RlTxReport35V2025 : esRlTxReport35V2025
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
    abstract public class esRlTxReport35V2025Query : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return RlTxReport35V2025Metadata.Meta();
            }
        }


        public esQueryItem RlTxReportNo
        {
            get
            {
                return new esQueryItem(this, RlTxReport35V2025Metadata.ColumnNames.RlTxReportNo, esSystemType.String);
            }
        }

        public esQueryItem RlMasterReportItemID
        {
            get
            {
                return new esQueryItem(this, RlTxReport35V2025Metadata.ColumnNames.RlMasterReportItemID, esSystemType.Int32);
            }
        }



        public esQueryItem Jumlah
        {
            get
            {
                return new esQueryItem(this, RlTxReport35V2025Metadata.ColumnNames.Jumlah, esSystemType.Int32);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, RlTxReport35V2025Metadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, RlTxReport35V2025Metadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }
        public esQueryItem JumlahLaki
        {
            get
            {
                return new esQueryItem(this, RlTxReport35V2025Metadata.ColumnNames.JumlahLaki, esSystemType.Int32);
            }
        }

        public esQueryItem JumlahPerempuan
        {
            get
            {
                return new esQueryItem(this, RlTxReport35V2025Metadata.ColumnNames.JumlahPerempuan, esSystemType.Int32);
            }
        }


        public esQueryItem JumlahLaki2
        {
            get
            {
                return new esQueryItem(this, RlTxReport35V2025Metadata.ColumnNames.JumlahLaki2, esSystemType.Int32);
            }
        }

        public esQueryItem JumlahPerempuan2
        {
            get
            {
                return new esQueryItem(this, RlTxReport35V2025Metadata.ColumnNames.JumlahPerempuan2, esSystemType.Int32);
            }
        }



    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("RlTxReport35V2025Collection")]
    public partial class RlTxReport35V2025Collection : esRlTxReport35V2025Collection, IEnumerable<RlTxReport35V2025>
    {
        public RlTxReport35V2025Collection()
        {

        }

        public static implicit operator List<RlTxReport35V2025>(RlTxReport35V2025Collection coll)
        {
            List<RlTxReport35V2025> list = new List<RlTxReport35V2025>();

            foreach (RlTxReport35V2025 emp in coll)
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
                return RlTxReport35V2025Metadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new RlTxReport35V2025Query();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new RlTxReport35V2025(row);
        }

        override protected esEntity CreateEntity()
        {
            return new RlTxReport35V2025();
        }


        #endregion


        [BrowsableAttribute(false)]
        public RlTxReport35V2025Query Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new RlTxReport35V2025Query();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(RlTxReport35V2025Query query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public RlTxReport35V2025 AddNew()
        {
            RlTxReport35V2025 entity = base.AddNewEntity() as RlTxReport35V2025;

            return entity;
        }

        public RlTxReport35V2025 FindByPrimaryKey(System.String rlTxReportNo, System.Int32 rlMasterReportItemID)
        {
            return base.FindByPrimaryKey(rlTxReportNo, rlMasterReportItemID) as RlTxReport35V2025;
        }


        #region IEnumerable<RlTxReport35V2025> Members

        IEnumerator<RlTxReport35V2025> IEnumerable<RlTxReport35V2025>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as RlTxReport35V2025;
            }
        }

        #endregion

        private RlTxReport35V2025Query query;
    }


    /// <summary>
    /// Encapsulates the 'RlTxReport3_5V2025' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("RlTxReport35V2025 ({RlTxReportNo},{RlMasterReportItemID})")]
    [Serializable]
    public partial class RlTxReport35V2025 : esRlTxReport35V2025
    {
        public RlTxReport35V2025()
        {

        }

        public RlTxReport35V2025(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return RlTxReport35V2025Metadata.Meta();
            }
        }



        override protected esRlTxReport35V2025Query GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new RlTxReport35V2025Query();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public RlTxReport35V2025Query Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new RlTxReport35V2025Query();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(RlTxReport35V2025Query query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private RlTxReport35V2025Query query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class RlTxReport35V2025Query : esRlTxReport35V2025Query
    {
        public RlTxReport35V2025Query()
        {

        }

        public RlTxReport35V2025Query(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "RlTxReport35V2025Query";
        }


    }


    [Serializable]
    public partial class RlTxReport35V2025Metadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected RlTxReport35V2025Metadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(RlTxReport35V2025Metadata.ColumnNames.RlTxReportNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = RlTxReport35V2025Metadata.PropertyNames.RlTxReportNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport35V2025Metadata.ColumnNames.RlMasterReportItemID, 1, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport35V2025Metadata.PropertyNames.RlMasterReportItemID;
            c.IsInPrimaryKey = true;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport35V2025Metadata.ColumnNames.Jumlah, 2, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport35V2025Metadata.PropertyNames.Jumlah;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport35V2025Metadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = RlTxReport35V2025Metadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport35V2025Metadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = RlTxReport35V2025Metadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport35V2025Metadata.ColumnNames.JumlahLaki, 5, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport35V2025Metadata.PropertyNames.JumlahLaki;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport35V2025Metadata.ColumnNames.JumlahPerempuan, 6, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport35V2025Metadata.PropertyNames.JumlahPerempuan;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);


            c = new esColumnMetadata(RlTxReport35V2025Metadata.ColumnNames.JumlahLaki2, 7, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport35V2025Metadata.PropertyNames.JumlahLaki2;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport35V2025Metadata.ColumnNames.JumlahPerempuan2, 8, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport35V2025Metadata.PropertyNames.JumlahPerempuan2;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public RlTxReport35V2025Metadata Meta()
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

            public const string Jumlah = "Jumlah";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string JumlahLaki = "JumlahLaki";
            public const string JumlahPerempuan = "JumlahPerempuan";

            public const string JumlahLaki2 = "JumlahLaki2";
            public const string JumlahPerempuan2 = "JumlahPerempuan2";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string RlTxReportNo = "RlTxReportNo";
            public const string RlMasterReportItemID = "RlMasterReportItemID";

            public const string Jumlah = "Jumlah";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string JumlahLaki = "JumlahLaki";
            public const string JumlahPerempuan = "JumlahPerempuan";


            public const string JumlahLaki2 = "JumlahLaki2";
            public const string JumlahPerempuan2 = "JumlahPerempuan2";

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
            lock (typeof(RlTxReport35V2025Metadata))
            {
                if (RlTxReport35V2025Metadata.mapDelegates == null)
                {
                    RlTxReport35V2025Metadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (RlTxReport35V2025Metadata.meta == null)
                {
                    RlTxReport35V2025Metadata.meta = new RlTxReport35V2025Metadata();
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

                meta.AddTypeMap("Jumlah", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("JumlahLaki", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("JumlahPerempuan", new esTypeMap("int", "System.Int32"));

                meta.AddTypeMap("JumlahLaki2", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("JumlahPerempuan2", new esTypeMap("int", "System.Int32"));



                meta.Source = "RlTxReport3_5V2025";
                meta.Destination = "RlTxReport3_5V2025";

                meta.spInsert = "proc_RlTxReport3_5V2025Insert";
                meta.spUpdate = "proc_RlTxReport3_5V2025Update";
                meta.spDelete = "proc_RlTxReport3_5V2025Delete";
                meta.spLoadAll = "proc_RlTxReport3_5V2025LoadAll";
                meta.spLoadByPrimaryKey = "proc_RlTxReport3_5V2025LoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private RlTxReport35V2025Metadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
