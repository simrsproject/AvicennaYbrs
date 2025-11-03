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
    abstract public class esRlTxReport53V2025Collection : esEntityCollectionWAuditLog
    {
        public esRlTxReport53V2025Collection()
        {

        }

        protected override string GetCollectionName()
        {
            return "RlTxReport53V2025Collection";
        }

        #region Query Logic
        protected void InitQuery(esRlTxReport53V2025Query query)
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
            this.InitQuery(query as esRlTxReport53V2025Query);
        }
        #endregion

        virtual public RlTxReport53V2025 DetachEntity(RlTxReport53V2025 entity)
        {
            return base.DetachEntity(entity) as RlTxReport53V2025;
        }

        virtual public RlTxReport53V2025 AttachEntity(RlTxReport53V2025 entity)
        {
            return base.AttachEntity(entity) as RlTxReport53V2025;
        }

        virtual public void Combine(RlTxReport53V2025Collection collection)
        {
            base.Combine(collection);
        }

        new public RlTxReport53V2025 this[int index]
        {
            get
            {
                return base[index] as RlTxReport53V2025;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(RlTxReport53V2025);
        }
    }



    [Serializable]
    abstract public class esRlTxReport53V2025 : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esRlTxReport53V2025Query GetDynamicQuery()
        {
            return null;
        }

        public esRlTxReport53V2025()
        {

        }

        public esRlTxReport53V2025(DataRow row)
            : base(row)
        {

        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(System.String rlTxReportNo, System.String diagnosaID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(rlTxReportNo, diagnosaID);
            else
                return LoadByPrimaryKeyStoredProcedure(rlTxReportNo, diagnosaID);
        }

        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String rlTxReportNo, System.String diagnosaID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(rlTxReportNo, diagnosaID);
            else
                return LoadByPrimaryKeyStoredProcedure(rlTxReportNo, diagnosaID);
        }

        private bool LoadByPrimaryKeyDynamic(System.String rlTxReportNo, System.String diagnosaID)
        {
            esRlTxReport53V2025Query query = this.GetDynamicQuery();
            query.Where(query.RlTxReportNo == rlTxReportNo, query.DiagnosaID == diagnosaID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(System.String rlTxReportNo, System.String diagnosaID)
        {
            esParameters parms = new esParameters();
            parms.Add("RlTxReportNo", rlTxReportNo); parms.Add("DiagnosaID", diagnosaID);
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
                        case "DiagnosaID": this.str.DiagnosaID = (string)value; break;
                        case "KasusBaruL": this.str.KasusBaruL = (string)value; break;
                        case "KasusBaruP": this.str.KasusBaruP = (string)value; break;
                        case "JumlahKasusBaru": this.str.JumlahKasusBaru = (string)value; break;
                        case "KunjunganL": this.str.KasusBaruL = (string)value; break;
                        case "KunjunganP": this.str.KasusBaruP = (string)value; break;
                        case "JumlahKunjungan": this.str.JumlahKunjungan = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "KasusBaruL":

                            if (value == null || value is System.Int32)
                                this.KasusBaruL = (System.Int32?)value;
                            break;

                        case "KasusBaruP":

                            if (value == null || value is System.Int32)
                                this.KasusBaruP = (System.Int32?)value;
                            break;

                        case "JumlahKasusBaru":

                            if (value == null || value is System.Int32)
                                this.JumlahKasusBaru = (System.Int32?)value;
                            break;
                        case "KunjunganL":

                            if (value == null || value is System.Int32)
                                this.KasusBaruL = (System.Int32?)value;
                            break;

                        case "KunjunganP":

                            if (value == null || value is System.Int32)
                                this.KasusBaruP = (System.Int32?)value;
                            break;

                        case "JumlahKunjungan":

                            if (value == null || value is System.Int32)
                                this.JumlahKunjungan = (System.Int32?)value;
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
        /// Maps to RlTxReport5_3V2025.RlTxReportNo
        /// </summary>
        virtual public System.String RlTxReportNo
        {
            get
            {
                return base.GetSystemString(RlTxReport53V2025Metadata.ColumnNames.RlTxReportNo);
            }

            set
            {
                base.SetSystemString(RlTxReport53V2025Metadata.ColumnNames.RlTxReportNo, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport5_3V2025.DiagnosaID
        /// </summary>
        virtual public System.String DiagnosaID
        {
            get
            {
                return base.GetSystemString(RlTxReport53V2025Metadata.ColumnNames.DiagnosaID);
            }

            set
            {
                base.SetSystemString(RlTxReport53V2025Metadata.ColumnNames.DiagnosaID, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport5_3V2024.KasusBaruL
        /// </summary>
        virtual public System.Int32? KasusBaruL
        {
            get
            {
                return base.GetSystemInt32(RlTxReport53V2025Metadata.ColumnNames.KasusBaruL);
            }

            set
            {
                base.SetSystemInt32(RlTxReport53V2025Metadata.ColumnNames.KasusBaruL, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport5_3V2025.KasusBaruP
        /// </summary>
        virtual public System.Int32? KasusBaruP
        {
            get
            {
                return base.GetSystemInt32(RlTxReport53V2025Metadata.ColumnNames.KasusBaruP);
            }

            set
            {
                base.SetSystemInt32(RlTxReport53V2025Metadata.ColumnNames.KasusBaruP, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport5_3V2024.KasusBaruL
        /// </summary>
        virtual public System.Int32? KunjunganL
        {
            get
            {
                return base.GetSystemInt32(RlTxReport53V2025Metadata.ColumnNames.KunjunganL);
            }

            set
            {
                base.SetSystemInt32(RlTxReport53V2025Metadata.ColumnNames.KunjunganL, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport5_3V2025.KasusBaruP
        /// </summary>
        virtual public System.Int32? KunjunganP
        {
            get
            {
                return base.GetSystemInt32(RlTxReport53V2025Metadata.ColumnNames.KunjunganP);
            }

            set
            {
                base.SetSystemInt32(RlTxReport53V2025Metadata.ColumnNames.KunjunganP, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport5_3V2025.JumlahKasusBaru
        /// </summary>
        virtual public System.Int32? JumlahKasusBaru
        {
            get
            {
                return base.GetSystemInt32(RlTxReport53V2025Metadata.ColumnNames.JumlahKasusBaru);
            }

            set
            {
                base.SetSystemInt32(RlTxReport53V2025Metadata.ColumnNames.JumlahKasusBaru, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport5_3V2025.JumlahKunjungan
        /// </summary>
        virtual public System.Int32? JumlahKunjungan
        {
            get
            {
                return base.GetSystemInt32(RlTxReport53V2025Metadata.ColumnNames.JumlahKunjungan);
            }

            set
            {
                base.SetSystemInt32(RlTxReport53V2025Metadata.ColumnNames.JumlahKunjungan, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport5_3V2025.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(RlTxReport53V2025Metadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(RlTxReport53V2025Metadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport5_3V2025.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(RlTxReport53V2025Metadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(RlTxReport53V2025Metadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esRlTxReport53V2025 entity)
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

            public System.String DiagnosaID
            {
                get
                {
                    System.String data = entity.DiagnosaID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DiagnosaID = null;
                    else entity.DiagnosaID = Convert.ToString(value);
                }
            }

            public System.String KasusBaruL
            {
                get
                {
                    System.Int32? data = entity.KasusBaruL;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.KasusBaruL = null;
                    else entity.KasusBaruL = Convert.ToInt32(value);
                }
            }

            public System.String KasusBaruP
            {
                get
                {
                    System.Int32? data = entity.KasusBaruP;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.KasusBaruP = null;
                    else entity.KasusBaruP = Convert.ToInt32(value);
                }
            }

            public System.String JumlahKasusBaru
            {
                get
                {
                    System.Int32? data = entity.JumlahKasusBaru;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.JumlahKasusBaru = null;
                    else entity.JumlahKasusBaru = Convert.ToInt32(value);
                }
            }

            public System.String KunjunganL
            {
                get
                {
                    System.Int32? data = entity.KunjunganL;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.KunjunganL = null;
                    else entity.KunjunganL = Convert.ToInt32(value);
                }
            }

            public System.String KunjunganP
            {
                get
                {
                    System.Int32? data = entity.KunjunganP;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.KunjunganP = null;
                    else entity.KunjunganP = Convert.ToInt32(value);
                }
            }

            public System.String JumlahKunjungan
            {
                get
                {
                    System.Int32? data = entity.JumlahKunjungan;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.JumlahKunjungan = null;
                    else entity.JumlahKunjungan = Convert.ToInt32(value);
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


            private esRlTxReport53V2025 entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esRlTxReport53V2025Query query)
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
                throw new Exception("esRlTxReport53V2025 can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    public partial class RlTxReport53V2025 : esRlTxReport53V2025
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
    abstract public class esRlTxReport53V2025Query : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return RlTxReport53V2025Metadata.Meta();
            }
        }


        public esQueryItem RlTxReportNo
        {
            get
            {
                return new esQueryItem(this, RlTxReport53V2025Metadata.ColumnNames.RlTxReportNo, esSystemType.String);
            }
        }

        public esQueryItem DiagnosaID
        {
            get
            {
                return new esQueryItem(this, RlTxReport53V2025Metadata.ColumnNames.DiagnosaID, esSystemType.String);
            }
        }

        public esQueryItem KasusBaruL
        {
            get
            {
                return new esQueryItem(this, RlTxReport53V2025Metadata.ColumnNames.KasusBaruL, esSystemType.Int32);
            }
        }

        public esQueryItem KasusBaruP
        {
            get
            {
                return new esQueryItem(this, RlTxReport53V2025Metadata.ColumnNames.KasusBaruP, esSystemType.Int32);
            }
        }

        public esQueryItem JumlahKasusBaru
        {
            get
            {
                return new esQueryItem(this, RlTxReport53V2025Metadata.ColumnNames.JumlahKasusBaru, esSystemType.Int32);
            }
        }

        public esQueryItem KunjunganL
        {
            get
            {
                return new esQueryItem(this, RlTxReport53V2025Metadata.ColumnNames.KunjunganL, esSystemType.Int32);
            }
        }

        public esQueryItem KunjunganP
        {
            get
            {
                return new esQueryItem(this, RlTxReport53V2025Metadata.ColumnNames.KunjunganP, esSystemType.Int32);
            }
        }

        public esQueryItem JumlahKunjungan
        {
            get
            {
                return new esQueryItem(this, RlTxReport53V2025Metadata.ColumnNames.JumlahKunjungan, esSystemType.Int32);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, RlTxReport53V2025Metadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, RlTxReport53V2025Metadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("RlTxReport53V2025Collection")]
    public partial class RlTxReport53V2025Collection : esRlTxReport53V2025Collection, IEnumerable<RlTxReport53V2025>
    {
        public RlTxReport53V2025Collection()
        {

        }

        public static implicit operator List<RlTxReport53V2025>(RlTxReport53V2025Collection coll)
        {
            List<RlTxReport53V2025> list = new List<RlTxReport53V2025>();

            foreach (RlTxReport53V2025 emp in coll)
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
                return RlTxReport53V2025Metadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new RlTxReport53V2025Query();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new RlTxReport53V2025(row);
        }

        override protected esEntity CreateEntity()
        {
            return new RlTxReport53V2025();
        }


        #endregion


        [BrowsableAttribute(false)]
        public RlTxReport53V2025Query Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new RlTxReport53V2025Query();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(RlTxReport53V2025Query query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public RlTxReport53V2025 AddNew()
        {
            RlTxReport53V2025 entity = base.AddNewEntity() as RlTxReport53V2025;

            return entity;
        }

        public RlTxReport53V2025 FindByPrimaryKey(System.String rlTxReportNo, System.String diagnosaID)
        {
            return base.FindByPrimaryKey(rlTxReportNo, diagnosaID) as RlTxReport53V2025;
        }


        #region IEnumerable<RlTxReport53V2025> Members

        IEnumerator<RlTxReport53V2025> IEnumerable<RlTxReport53V2025>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as RlTxReport53V2025;
            }
        }

        #endregion

        private RlTxReport53V2025Query query;
    }


    /// <summary>
    /// Encapsulates the 'RlTxReport5_3V2025' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("RlTxReport53V2025 ({RlTxReportNo},{DiagnosaID})")]
    [Serializable]
    public partial class RlTxReport53V2025 : esRlTxReport53V2025
    {
        public RlTxReport53V2025()
        {

        }

        public RlTxReport53V2025(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return RlTxReport53V2025Metadata.Meta();
            }
        }



        override protected esRlTxReport53V2025Query GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new RlTxReport53V2025Query();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public RlTxReport53V2025Query Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new RlTxReport53V2025Query();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(RlTxReport53V2025Query query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private RlTxReport53V2025Query query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class RlTxReport53V2025Query : esRlTxReport53V2025Query
    {
        public RlTxReport53V2025Query()
        {

        }

        public RlTxReport53V2025Query(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "RlTxReport53V2025Query";
        }


    }


    [Serializable]
    public partial class RlTxReport53V2025Metadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected RlTxReport53V2025Metadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(RlTxReport53V2025Metadata.ColumnNames.RlTxReportNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = RlTxReport53V2025Metadata.PropertyNames.RlTxReportNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport53V2025Metadata.ColumnNames.DiagnosaID, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = RlTxReport53V2025Metadata.PropertyNames.DiagnosaID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport53V2025Metadata.ColumnNames.KasusBaruL, 2, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport53V2025Metadata.PropertyNames.KasusBaruL;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport53V2025Metadata.ColumnNames.KasusBaruP, 3, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport53V2025Metadata.PropertyNames.KasusBaruP;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport53V2025Metadata.ColumnNames.JumlahKasusBaru, 4, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport53V2025Metadata.PropertyNames.JumlahKasusBaru;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport53V2025Metadata.ColumnNames.KunjunganL, 5, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport53V2025Metadata.PropertyNames.KunjunganL;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport53V2025Metadata.ColumnNames.KunjunganP, 6, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport53V2025Metadata.PropertyNames.KunjunganP;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport53V2025Metadata.ColumnNames.JumlahKunjungan, 7, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport53V2025Metadata.PropertyNames.JumlahKunjungan;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport53V2025Metadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = RlTxReport53V2025Metadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport53V2025Metadata.ColumnNames.LastUpdateByUserID, 9, typeof(System.String), esSystemType.String);
            c.PropertyName = RlTxReport53V2025Metadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public RlTxReport53V2025Metadata Meta()
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
            public const string DiagnosaID = "DiagnosaID";
            public const string KasusBaruL = "KasusBaruL";
            public const string KasusBaruP = "KasusBaruP";
            public const string JumlahKasusBaru = "JumlahKasusBaru";
            public const string KunjunganL = "KunjunganL";
            public const string KunjunganP = "KunjunganP";
            public const string JumlahKunjungan = "JumlahKunjungan";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string RlTxReportNo = "RlTxReportNo";
            public const string DiagnosaID = "DiagnosaID";
            public const string KasusBaruL = "KasusBaruL";
            public const string KasusBaruP = "KasusBaruP";
            public const string JumlahKasusBaru = "JumlahKasusBaru";
            public const string KunjunganL = "KunjunganL";
            public const string KunjunganP = "KunjunganP";
            public const string JumlahKunjungan = "JumlahKunjungan";
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
            lock (typeof(RlTxReport53V2025Metadata))
            {
                if (RlTxReport53V2025Metadata.mapDelegates == null)
                {
                    RlTxReport53V2025Metadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (RlTxReport53V2025Metadata.meta == null)
                {
                    RlTxReport53V2025Metadata.meta = new RlTxReport53V2025Metadata();
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
                meta.AddTypeMap("DiagnosaID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("KasusBaruL", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("KasusBaruP", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("JumlahKasusBaru", new esTypeMap("int", "System.Int32"));

                meta.AddTypeMap("KunjunganL", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("KunjunganP", new esTypeMap("int", "System.Int32"));

                meta.AddTypeMap("JumlahKunjungan", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));



                meta.Source = "RlTxReport5_3V2025";
                meta.Destination = "RlTxReport5_3V2025";

                meta.spInsert = "proc_RlTxReport5_3V2025Insert";
                meta.spUpdate = "proc_RlTxReport5_3V2025Update";
                meta.spDelete = "proc_RlTxReport5_3V2025Delete";
                meta.spLoadAll = "proc_RlTxReport5_3V2025LoadAll";
                meta.spLoadByPrimaryKey = "proc_RlTxReport5_3V2025LoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private RlTxReport53V2025Metadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
