/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 3/5/2013 9:19:54 AM
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
    abstract public class esRlTxReport12Collection : esEntityCollectionWAuditLog
    {
        public esRlTxReport12Collection()
        {

        }

        protected override string GetCollectionName()
        {
            return "RlTxReport12Collection";
        }

        #region Query Logic
        protected void InitQuery(esRlTxReport12Query query)
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
            this.InitQuery(query as esRlTxReport12Query);
        }
        #endregion

        virtual public RlTxReport12 DetachEntity(RlTxReport12 entity)
        {
            return base.DetachEntity(entity) as RlTxReport12;
        }

        virtual public RlTxReport12 AttachEntity(RlTxReport12 entity)
        {
            return base.AttachEntity(entity) as RlTxReport12;
        }

        virtual public void Combine(RlTxReport12Collection collection)
        {
            base.Combine(collection);
        }

        new public RlTxReport12 this[int index]
        {
            get
            {
                return base[index] as RlTxReport12;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(RlTxReport12);
        }
    }



    [Serializable]
    abstract public class esRlTxReport12 : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esRlTxReport12Query GetDynamicQuery()
        {
            return null;
        }

        public esRlTxReport12()
        {

        }

        public esRlTxReport12(DataRow row)
            : base(row)
        {

        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(System.String rlTxReportNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(rlTxReportNo);
            else
                return LoadByPrimaryKeyStoredProcedure(rlTxReportNo);
        }

        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String rlTxReportNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(rlTxReportNo);
            else
                return LoadByPrimaryKeyStoredProcedure(rlTxReportNo);
        }

        private bool LoadByPrimaryKeyDynamic(System.String rlTxReportNo)
        {
            esRlTxReport12Query query = this.GetDynamicQuery();
            query.Where(query.RlTxReportNo == rlTxReportNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(System.String rlTxReportNo)
        {
            esParameters parms = new esParameters();
            parms.Add("RlTxReportNo", rlTxReportNo);
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
                        case "Bor": this.str.Bor = (string)value; break;
                        case "Los": this.str.Los = (string)value; break;
                        case "Bto": this.str.Bto = (string)value; break;
                        case "Toi": this.str.Toi = (string)value; break;
                        case "Ndr": this.str.Ndr = (string)value; break;
                        case "Gdr": this.str.Gdr = (string)value; break;
                        case "RataKunjungan": this.str.RataKunjungan = (string)value; break;
                        case "RataRata": this.str.RataRata = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "Bor":

                            if (value == null || value is System.Decimal)
                                this.Bor = (System.Decimal?)value;
                            break;

                        case "Los":

                            if (value == null || value is System.Decimal)
                                this.Los = (System.Decimal?)value;
                            break;

                        case "Bto":

                            if (value == null || value is System.Decimal)
                                this.Bto = (System.Decimal?)value;
                            break;

                        case "Toi":

                            if (value == null || value is System.Decimal)
                                this.Toi = (System.Decimal?)value;
                            break;

                        case "Ndr":

                            if (value == null || value is System.Decimal)
                                this.Ndr = (System.Decimal?)value;
                            break;

                        case "Gdr":

                            if (value == null || value is System.Decimal)
                                this.Gdr = (System.Decimal?)value;
                            break;

                        case "RataKunjungan":

                            if (value == null || value is System.Decimal)
                                this.RataKunjungan = (System.Decimal?)value;
                            break;

                        case "RataRata":

                            if (value == null || value is System.Decimal)
                                this.RataRata = (System.Decimal?)value;
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
        /// Maps to RlTxReport1_2.RlTxReportNo
        /// </summary>
        virtual public System.String RlTxReportNo
        {
            get
            {
                return base.GetSystemString(RlTxReport12Metadata.ColumnNames.RlTxReportNo);
            }

            set
            {
                base.SetSystemString(RlTxReport12Metadata.ColumnNames.RlTxReportNo, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport1_2.Bor
        /// </summary>
        virtual public System.Decimal? Bor
        {
            get
            {
                return base.GetSystemDecimal(RlTxReport12Metadata.ColumnNames.Bor);
            }

            set
            {
                base.SetSystemDecimal(RlTxReport12Metadata.ColumnNames.Bor, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport1_2.Los
        /// </summary>
        virtual public System.Decimal? Los
        {
            get
            {
                return base.GetSystemDecimal(RlTxReport12Metadata.ColumnNames.Los);
            }

            set
            {
                base.SetSystemDecimal(RlTxReport12Metadata.ColumnNames.Los, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport1_2.Bto
        /// </summary>
        virtual public System.Decimal? Bto
        {
            get
            {
                return base.GetSystemDecimal(RlTxReport12Metadata.ColumnNames.Bto);
            }

            set
            {
                base.SetSystemDecimal(RlTxReport12Metadata.ColumnNames.Bto, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport1_2.Toi
        /// </summary>
        virtual public System.Decimal? Toi
        {
            get
            {
                return base.GetSystemDecimal(RlTxReport12Metadata.ColumnNames.Toi);
            }

            set
            {
                base.SetSystemDecimal(RlTxReport12Metadata.ColumnNames.Toi, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport1_2.Ndr
        /// </summary>
        virtual public System.Decimal? Ndr
        {
            get
            {
                return base.GetSystemDecimal(RlTxReport12Metadata.ColumnNames.Ndr);
            }

            set
            {
                base.SetSystemDecimal(RlTxReport12Metadata.ColumnNames.Ndr, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport1_2.Gdr
        /// </summary>
        virtual public System.Decimal? Gdr
        {
            get
            {
                return base.GetSystemDecimal(RlTxReport12Metadata.ColumnNames.Gdr);
            }

            set
            {
                base.SetSystemDecimal(RlTxReport12Metadata.ColumnNames.Gdr, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport1_2.RataKunjungan
        /// </summary>
        virtual public System.Decimal? RataKunjungan
        {
            get
            {
                return base.GetSystemDecimal(RlTxReport12Metadata.ColumnNames.RataKunjungan);
            }

            set
            {
                base.SetSystemDecimal(RlTxReport12Metadata.ColumnNames.RataKunjungan, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport1_2.RataRata
        /// </summary>
        virtual public System.Decimal? RataRata
        {
            get
            {
                return base.GetSystemDecimal(RlTxReport12Metadata.ColumnNames.RataRata);
            }

            set
            {
                base.SetSystemDecimal(RlTxReport12Metadata.ColumnNames.RataRata, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport1_2.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(RlTxReport12Metadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(RlTxReport12Metadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport1_2.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(RlTxReport12Metadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(RlTxReport12Metadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esRlTxReport12 entity)
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

            public System.String Bor
            {
                get
                {
                    System.Decimal? data = entity.Bor;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Bor = null;
                    else entity.Bor = Convert.ToDecimal(value);
                }
            }

            public System.String Los
            {
                get
                {
                    System.Decimal? data = entity.Los;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Los = null;
                    else entity.Los = Convert.ToDecimal(value);
                }
            }

            public System.String Bto
            {
                get
                {
                    System.Decimal? data = entity.Bto;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Bto = null;
                    else entity.Bto = Convert.ToDecimal(value);
                }
            }

            public System.String Toi
            {
                get
                {
                    System.Decimal? data = entity.Toi;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Toi = null;
                    else entity.Toi = Convert.ToDecimal(value);
                }
            }

            public System.String Ndr
            {
                get
                {
                    System.Decimal? data = entity.Ndr;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Ndr = null;
                    else entity.Ndr = Convert.ToDecimal(value);
                }
            }

            public System.String Gdr
            {
                get
                {
                    System.Decimal? data = entity.Gdr;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Gdr = null;
                    else entity.Gdr = Convert.ToDecimal(value);
                }
            }

            public System.String RataKunjungan
            {
                get
                {
                    System.Decimal? data = entity.RataKunjungan;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RataKunjungan = null;
                    else entity.RataKunjungan = Convert.ToDecimal(value);
                }
            }

            public System.String RataRata
            {
                get
                {
                    System.Decimal? data = entity.RataRata;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RataRata = null;
                    else entity.RataRata = Convert.ToDecimal(value);
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


            private esRlTxReport12 entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esRlTxReport12Query query)
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
                throw new Exception("esRlTxReport12 can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    public partial class RlTxReport12 : esRlTxReport12
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
    abstract public class esRlTxReport12Query : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return RlTxReport12Metadata.Meta();
            }
        }


        public esQueryItem RlTxReportNo
        {
            get
            {
                return new esQueryItem(this, RlTxReport12Metadata.ColumnNames.RlTxReportNo, esSystemType.String);
            }
        }

        public esQueryItem Bor
        {
            get
            {
                return new esQueryItem(this, RlTxReport12Metadata.ColumnNames.Bor, esSystemType.Decimal);
            }
        }

        public esQueryItem Los
        {
            get
            {
                return new esQueryItem(this, RlTxReport12Metadata.ColumnNames.Los, esSystemType.Decimal);
            }
        }

        public esQueryItem Bto
        {
            get
            {
                return new esQueryItem(this, RlTxReport12Metadata.ColumnNames.Bto, esSystemType.Decimal);
            }
        }

        public esQueryItem Toi
        {
            get
            {
                return new esQueryItem(this, RlTxReport12Metadata.ColumnNames.Toi, esSystemType.Decimal);
            }
        }

        public esQueryItem Ndr
        {
            get
            {
                return new esQueryItem(this, RlTxReport12Metadata.ColumnNames.Ndr, esSystemType.Decimal);
            }
        }

        public esQueryItem Gdr
        {
            get
            {
                return new esQueryItem(this, RlTxReport12Metadata.ColumnNames.Gdr, esSystemType.Decimal);
            }
        }

        public esQueryItem RataKunjungan
        {
            get
            {
                return new esQueryItem(this, RlTxReport12Metadata.ColumnNames.RataKunjungan, esSystemType.Decimal);
            }
        }

        public esQueryItem RataRata
        {
            get
            {
                return new esQueryItem(this, RlTxReport12Metadata.ColumnNames.RataRata, esSystemType.Decimal);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, RlTxReport12Metadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, RlTxReport12Metadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("RlTxReport12Collection")]
    public partial class RlTxReport12Collection : esRlTxReport12Collection, IEnumerable<RlTxReport12>
    {
        public RlTxReport12Collection()
        {

        }

        public static implicit operator List<RlTxReport12>(RlTxReport12Collection coll)
        {
            List<RlTxReport12> list = new List<RlTxReport12>();

            foreach (RlTxReport12 emp in coll)
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
                return RlTxReport12Metadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new RlTxReport12Query();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new RlTxReport12(row);
        }

        override protected esEntity CreateEntity()
        {
            return new RlTxReport12();
        }


        #endregion


        [BrowsableAttribute(false)]
        public RlTxReport12Query Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new RlTxReport12Query();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(RlTxReport12Query query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public RlTxReport12 AddNew()
        {
            RlTxReport12 entity = base.AddNewEntity() as RlTxReport12;

            return entity;
        }

        public RlTxReport12 FindByPrimaryKey(System.String rlTxReportNo)
        {
            return base.FindByPrimaryKey(rlTxReportNo) as RlTxReport12;
        }


        #region IEnumerable<RlTxReport12> Members

        IEnumerator<RlTxReport12> IEnumerable<RlTxReport12>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as RlTxReport12;
            }
        }

        #endregion

        private RlTxReport12Query query;
    }


    /// <summary>
    /// Encapsulates the 'RlTxReport1_2' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("RlTxReport12 ({RlTxReportNo})")]
    [Serializable]
    public partial class RlTxReport12 : esRlTxReport12
    {
        public RlTxReport12()
        {

        }

        public RlTxReport12(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return RlTxReport12Metadata.Meta();
            }
        }



        override protected esRlTxReport12Query GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new RlTxReport12Query();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public RlTxReport12Query Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new RlTxReport12Query();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(RlTxReport12Query query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private RlTxReport12Query query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class RlTxReport12Query : esRlTxReport12Query
    {
        public RlTxReport12Query()
        {

        }

        public RlTxReport12Query(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "RlTxReport12Query";
        }


    }


    [Serializable]
    public partial class RlTxReport12Metadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected RlTxReport12Metadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(RlTxReport12Metadata.ColumnNames.RlTxReportNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = RlTxReport12Metadata.PropertyNames.RlTxReportNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport12Metadata.ColumnNames.Bor, 1, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = RlTxReport12Metadata.PropertyNames.Bor;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport12Metadata.ColumnNames.Los, 2, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = RlTxReport12Metadata.PropertyNames.Los;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport12Metadata.ColumnNames.Bto, 3, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = RlTxReport12Metadata.PropertyNames.Bto;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport12Metadata.ColumnNames.Toi, 4, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = RlTxReport12Metadata.PropertyNames.Toi;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport12Metadata.ColumnNames.Ndr, 5, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = RlTxReport12Metadata.PropertyNames.Ndr;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport12Metadata.ColumnNames.Gdr, 6, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = RlTxReport12Metadata.PropertyNames.Gdr;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport12Metadata.ColumnNames.RataKunjungan, 7, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = RlTxReport12Metadata.PropertyNames.RataKunjungan;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport12Metadata.ColumnNames.RataRata, 8, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = RlTxReport12Metadata.PropertyNames.RataRata;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport12Metadata.ColumnNames.LastUpdateDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = RlTxReport12Metadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport12Metadata.ColumnNames.LastUpdateByUserID, 10, typeof(System.String), esSystemType.String);
            c.PropertyName = RlTxReport12Metadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public RlTxReport12Metadata Meta()
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
            public const string Bor = "Bor";
            public const string Los = "Los";
            public const string Bto = "Bto";
            public const string Toi = "Toi";
            public const string Ndr = "Ndr";
            public const string Gdr = "Gdr";
            public const string RataKunjungan = "RataKunjungan";
            public const string RataRata = "RataRata";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string RlTxReportNo = "RlTxReportNo";
            public const string Bor = "Bor";
            public const string Los = "Los";
            public const string Bto = "Bto";
            public const string Toi = "Toi";
            public const string Ndr = "Ndr";
            public const string Gdr = "Gdr";
            public const string RataKunjungan = "RataKunjungan";
            public const string RataRata = "RataRata";
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
            lock (typeof(RlTxReport12Metadata))
            {
                if (RlTxReport12Metadata.mapDelegates == null)
                {
                    RlTxReport12Metadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (RlTxReport12Metadata.meta == null)
                {
                    RlTxReport12Metadata.meta = new RlTxReport12Metadata();
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
                meta.AddTypeMap("Bor", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("Los", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("Bto", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("Toi", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("Ndr", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("Gdr", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("RataKunjungan", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("RataRata", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));



                meta.Source = "RlTxReport1_2";
                meta.Destination = "RlTxReport1_2";

                meta.spInsert = "proc_RlTxReport1_2Insert";
                meta.spUpdate = "proc_RlTxReport1_2Update";
                meta.spDelete = "proc_RlTxReport1_2Delete";
                meta.spLoadAll = "proc_RlTxReport1_2LoadAll";
                meta.spLoadByPrimaryKey = "proc_RlTxReport1_2LoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private RlTxReport12Metadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
