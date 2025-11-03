/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 1/15/2015 5:07:54 PM
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



namespace Temiang.Avicenna.BusinessObject.Interop.RSCH
{

    [Serializable]
    abstract public class esHasilPasienCollection : esEntityCollectionWAuditLog
    {
        public esHasilPasienCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "HasilPasienCollection";
        }

        #region Query Logic
        protected void InitQuery(esHasilPasienQuery query)
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
            this.InitQuery(query as esHasilPasienQuery);
        }
        #endregion

        virtual public HasilPasien DetachEntity(HasilPasien entity)
        {
            return base.DetachEntity(entity) as HasilPasien;
        }

        virtual public HasilPasien AttachEntity(HasilPasien entity)
        {
            return base.AttachEntity(entity) as HasilPasien;
        }

        virtual public void Combine(HasilPasienCollection collection)
        {
            base.Combine(collection);
        }

        new public HasilPasien this[int index]
        {
            get
            {
                return base[index] as HasilPasien;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(HasilPasien);
        }
    }



    [Serializable]
    abstract public class esHasilPasien : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esHasilPasienQuery GetDynamicQuery()
        {
            return null;
        }

        public esHasilPasien()
        {

        }

        public esHasilPasien(DataRow row)
            : base(row)
        {

        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey()
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic();
            else
                return LoadByPrimaryKeyStoredProcedure();
        }

        // public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, )
        // {
        // if (sqlAccessType == esSqlAccessType.DynamicSQL)
        // return LoadByPrimaryKeyDynamic();
        // else
        // return LoadByPrimaryKeyStoredProcedure();
        // }

        private bool LoadByPrimaryKeyDynamic()
        {
            esHasilPasienQuery query = this.GetDynamicQuery();
            query.Where();
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure()
        {
            esParameters parms = new esParameters();

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
                        case "OrderLabNo": this.str.OrderLabNo = (string)value; break;
                        case "OrderLabTglOrder": this.str.OrderLabTglOrder = (string)value; break;
                        case "OrderLabNoMR": this.str.OrderLabNoMR = (string)value; break;
                        case "OrderLabNama": this.str.OrderLabNama = (string)value; break;
                        case "CheckupResultGroupCode": this.str.CheckupResultGroupCode = (string)value; break;
                        case "CheckupResultGroupName": this.str.CheckupResultGroupName = (string)value; break;
                        case "CheckupResultTestCode": this.str.CheckupResultTestCode = (string)value; break;
                        case "CheckupResultTestName": this.str.CheckupResultTestName = (string)value; break;
                        case "CheckupResultFractionCode": this.str.CheckupResultFractionCode = (string)value; break;
                        case "CheckupResultFractionName": this.str.CheckupResultFractionName = (string)value; break;
                        case "WithinRange": this.str.WithinRange = (string)value; break;
                        case "OutRange": this.str.OutRange = (string)value; break;
                        case "Satuan": this.str.Satuan = (string)value; break;
                        case "StandarValue": this.str.StandarValue = (string)value; break;
                        case "OrderLabCritical": this.str.OrderLabCritical = (string)value; break;
                        case "Seq": this.str.Seq = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "OrderLabTglOrder":

                            if (value == null || value is System.DateTime)
                                this.OrderLabTglOrder = (System.DateTime?)value;
                            break;

                        case "CheckupResultGroupCode":

                            if (value == null || value is System.Int32)
                                this.CheckupResultGroupCode = (System.Int32?)value;
                            break;

                        case "Seq":

                            if (value == null || value is System.Int32)
                                this.Seq = (System.Int32?)value;
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
        /// Maps to HasilPasien.OrderLabNo
        /// </summary>
        virtual public System.String OrderLabNo
        {
            get
            {
                return base.GetSystemString(HasilPasienMetadata.ColumnNames.OrderLabNo);
            }

            set
            {
                base.SetSystemString(HasilPasienMetadata.ColumnNames.OrderLabNo, value);
            }
        }

        /// <summary>
        /// Maps to HasilPasien.OrderLabTglOrder
        /// </summary>
        virtual public System.DateTime? OrderLabTglOrder
        {
            get
            {
                return base.GetSystemDateTime(HasilPasienMetadata.ColumnNames.OrderLabTglOrder);
            }

            set
            {
                base.SetSystemDateTime(HasilPasienMetadata.ColumnNames.OrderLabTglOrder, value);
            }
        }

        /// <summary>
        /// Maps to HasilPasien.OrderLabNoMR
        /// </summary>
        virtual public System.String OrderLabNoMR
        {
            get
            {
                return base.GetSystemString(HasilPasienMetadata.ColumnNames.OrderLabNoMR);
            }

            set
            {
                base.SetSystemString(HasilPasienMetadata.ColumnNames.OrderLabNoMR, value);
            }
        }

        /// <summary>
        /// Maps to HasilPasien.OrderLabNama
        /// </summary>
        virtual public System.String OrderLabNama
        {
            get
            {
                return base.GetSystemString(HasilPasienMetadata.ColumnNames.OrderLabNama);
            }

            set
            {
                base.SetSystemString(HasilPasienMetadata.ColumnNames.OrderLabNama, value);
            }
        }

        /// <summary>
        /// Maps to HasilPasien.CheckupResultGroupCode
        /// </summary>
        virtual public System.Int32? CheckupResultGroupCode
        {
            get
            {
                return base.GetSystemInt32(HasilPasienMetadata.ColumnNames.CheckupResultGroupCode);
            }

            set
            {
                base.SetSystemInt32(HasilPasienMetadata.ColumnNames.CheckupResultGroupCode, value);
            }
        }

        /// <summary>
        /// Maps to HasilPasien.CheckupResultGroupName
        /// </summary>
        virtual public System.String CheckupResultGroupName
        {
            get
            {
                return base.GetSystemString(HasilPasienMetadata.ColumnNames.CheckupResultGroupName);
            }

            set
            {
                base.SetSystemString(HasilPasienMetadata.ColumnNames.CheckupResultGroupName, value);
            }
        }

        /// <summary>
        /// Maps to HasilPasien.CheckupResultTestCode
        /// </summary>
        virtual public System.String CheckupResultTestCode
        {
            get
            {
                return base.GetSystemString(HasilPasienMetadata.ColumnNames.CheckupResultTestCode);
            }

            set
            {
                base.SetSystemString(HasilPasienMetadata.ColumnNames.CheckupResultTestCode, value);
            }
        }

        /// <summary>
        /// Maps to HasilPasien.CheckupResultTestName
        /// </summary>
        virtual public System.String CheckupResultTestName
        {
            get
            {
                return base.GetSystemString(HasilPasienMetadata.ColumnNames.CheckupResultTestName);
            }

            set
            {
                base.SetSystemString(HasilPasienMetadata.ColumnNames.CheckupResultTestName, value);
            }
        }

        /// <summary>
        /// Maps to HasilPasien.CheckupResultFractionCode
        /// </summary>
        virtual public System.String CheckupResultFractionCode
        {
            get
            {
                return base.GetSystemString(HasilPasienMetadata.ColumnNames.CheckupResultFractionCode);
            }

            set
            {
                base.SetSystemString(HasilPasienMetadata.ColumnNames.CheckupResultFractionCode, value);
            }
        }

        /// <summary>
        /// Maps to HasilPasien.CheckupResultFractionName
        /// </summary>
        virtual public System.String CheckupResultFractionName
        {
            get
            {
                return base.GetSystemString(HasilPasienMetadata.ColumnNames.CheckupResultFractionName);
            }

            set
            {
                base.SetSystemString(HasilPasienMetadata.ColumnNames.CheckupResultFractionName, value);
            }
        }

        /// <summary>
        /// Maps to HasilPasien.WithinRange
        /// </summary>
        virtual public System.String WithinRange
        {
            get
            {
                return base.GetSystemString(HasilPasienMetadata.ColumnNames.WithinRange);
            }

            set
            {
                base.SetSystemString(HasilPasienMetadata.ColumnNames.WithinRange, value);
            }
        }

        /// <summary>
        /// Maps to HasilPasien.OutRange
        /// </summary>
        virtual public System.String OutRange
        {
            get
            {
                return base.GetSystemString(HasilPasienMetadata.ColumnNames.OutRange);
            }

            set
            {
                base.SetSystemString(HasilPasienMetadata.ColumnNames.OutRange, value);
            }
        }

        /// <summary>
        /// Maps to HasilPasien.Satuan
        /// </summary>
        virtual public System.String Satuan
        {
            get
            {
                return base.GetSystemString(HasilPasienMetadata.ColumnNames.Satuan);
            }

            set
            {
                base.SetSystemString(HasilPasienMetadata.ColumnNames.Satuan, value);
            }
        }

        /// <summary>
        /// Maps to HasilPasien.StandarValue
        /// </summary>
        virtual public System.String StandarValue
        {
            get
            {
                return base.GetSystemString(HasilPasienMetadata.ColumnNames.StandarValue);
            }

            set
            {
                base.SetSystemString(HasilPasienMetadata.ColumnNames.StandarValue, value);
            }
        }

        /// <summary>
        /// Maps to HasilPasien.OrderLabCritical
        /// </summary>
        virtual public System.String OrderLabCritical
        {
            get
            {
                return base.GetSystemString(HasilPasienMetadata.ColumnNames.OrderLabCritical);
            }

            set
            {
                base.SetSystemString(HasilPasienMetadata.ColumnNames.OrderLabCritical, value);
            }
        }

        /// <summary>
        /// Maps to HasilPasien.Seq
        /// </summary>
        virtual public System.Int32? Seq
        {
            get
            {
                return base.GetSystemInt32(HasilPasienMetadata.ColumnNames.Seq);
            }

            set
            {
                base.SetSystemInt32(HasilPasienMetadata.ColumnNames.Seq, value);
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
            public esStrings(esHasilPasien entity)
            {
                this.entity = entity;
            }


            public System.String OrderLabNo
            {
                get
                {
                    System.String data = entity.OrderLabNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.OrderLabNo = null;
                    else entity.OrderLabNo = Convert.ToString(value);
                }
            }

            public System.String OrderLabTglOrder
            {
                get
                {
                    System.DateTime? data = entity.OrderLabTglOrder;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.OrderLabTglOrder = null;
                    else entity.OrderLabTglOrder = Convert.ToDateTime(value);
                }
            }

            public System.String OrderLabNoMR
            {
                get
                {
                    System.String data = entity.OrderLabNoMR;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.OrderLabNoMR = null;
                    else entity.OrderLabNoMR = Convert.ToString(value);
                }
            }

            public System.String OrderLabNama
            {
                get
                {
                    System.String data = entity.OrderLabNama;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.OrderLabNama = null;
                    else entity.OrderLabNama = Convert.ToString(value);
                }
            }

            public System.String CheckupResultGroupCode
            {
                get
                {
                    System.Int32? data = entity.CheckupResultGroupCode;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CheckupResultGroupCode = null;
                    else entity.CheckupResultGroupCode = Convert.ToInt32(value);
                }
            }

            public System.String CheckupResultGroupName
            {
                get
                {
                    System.String data = entity.CheckupResultGroupName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CheckupResultGroupName = null;
                    else entity.CheckupResultGroupName = Convert.ToString(value);
                }
            }

            public System.String CheckupResultTestCode
            {
                get
                {
                    System.String data = entity.CheckupResultTestCode;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CheckupResultTestCode = null;
                    else entity.CheckupResultTestCode = Convert.ToString(value);
                }
            }

            public System.String CheckupResultTestName
            {
                get
                {
                    System.String data = entity.CheckupResultTestName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CheckupResultTestName = null;
                    else entity.CheckupResultTestName = Convert.ToString(value);
                }
            }

            public System.String CheckupResultFractionCode
            {
                get
                {
                    System.String data = entity.CheckupResultFractionCode;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CheckupResultFractionCode = null;
                    else entity.CheckupResultFractionCode = Convert.ToString(value);
                }
            }

            public System.String CheckupResultFractionName
            {
                get
                {
                    System.String data = entity.CheckupResultFractionName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CheckupResultFractionName = null;
                    else entity.CheckupResultFractionName = Convert.ToString(value);
                }
            }

            public System.String WithinRange
            {
                get
                {
                    System.String data = entity.WithinRange;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.WithinRange = null;
                    else entity.WithinRange = Convert.ToString(value);
                }
            }

            public System.String OutRange
            {
                get
                {
                    System.String data = entity.OutRange;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.OutRange = null;
                    else entity.OutRange = Convert.ToString(value);
                }
            }

            public System.String Satuan
            {
                get
                {
                    System.String data = entity.Satuan;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Satuan = null;
                    else entity.Satuan = Convert.ToString(value);
                }
            }

            public System.String StandarValue
            {
                get
                {
                    System.String data = entity.StandarValue;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.StandarValue = null;
                    else entity.StandarValue = Convert.ToString(value);
                }
            }

            public System.String OrderLabCritical
            {
                get
                {
                    System.String data = entity.OrderLabCritical;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.OrderLabCritical = null;
                    else entity.OrderLabCritical = Convert.ToString(value);
                }
            }

            public System.String Seq
            {
                get
                {
                    System.Int32? data = entity.Seq;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Seq = null;
                    else entity.Seq = Convert.ToInt32(value);
                }
            }


            private esHasilPasien entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esHasilPasienQuery query)
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
                throw new Exception("esHasilPasien can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    [Serializable]
    abstract public class esHasilPasienQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return HasilPasienMetadata.Meta();
            }
        }


        public esQueryItem OrderLabNo
        {
            get
            {
                return new esQueryItem(this, HasilPasienMetadata.ColumnNames.OrderLabNo, esSystemType.String);
            }
        }

        public esQueryItem OrderLabTglOrder
        {
            get
            {
                return new esQueryItem(this, HasilPasienMetadata.ColumnNames.OrderLabTglOrder, esSystemType.DateTime);
            }
        }

        public esQueryItem OrderLabNoMR
        {
            get
            {
                return new esQueryItem(this, HasilPasienMetadata.ColumnNames.OrderLabNoMR, esSystemType.String);
            }
        }

        public esQueryItem OrderLabNama
        {
            get
            {
                return new esQueryItem(this, HasilPasienMetadata.ColumnNames.OrderLabNama, esSystemType.String);
            }
        }

        public esQueryItem CheckupResultGroupCode
        {
            get
            {
                return new esQueryItem(this, HasilPasienMetadata.ColumnNames.CheckupResultGroupCode, esSystemType.Int32);
            }
        }

        public esQueryItem CheckupResultGroupName
        {
            get
            {
                return new esQueryItem(this, HasilPasienMetadata.ColumnNames.CheckupResultGroupName, esSystemType.String);
            }
        }

        public esQueryItem CheckupResultTestCode
        {
            get
            {
                return new esQueryItem(this, HasilPasienMetadata.ColumnNames.CheckupResultTestCode, esSystemType.String);
            }
        }

        public esQueryItem CheckupResultTestName
        {
            get
            {
                return new esQueryItem(this, HasilPasienMetadata.ColumnNames.CheckupResultTestName, esSystemType.String);
            }
        }

        public esQueryItem CheckupResultFractionCode
        {
            get
            {
                return new esQueryItem(this, HasilPasienMetadata.ColumnNames.CheckupResultFractionCode, esSystemType.String);
            }
        }

        public esQueryItem CheckupResultFractionName
        {
            get
            {
                return new esQueryItem(this, HasilPasienMetadata.ColumnNames.CheckupResultFractionName, esSystemType.String);
            }
        }

        public esQueryItem WithinRange
        {
            get
            {
                return new esQueryItem(this, HasilPasienMetadata.ColumnNames.WithinRange, esSystemType.String);
            }
        }

        public esQueryItem OutRange
        {
            get
            {
                return new esQueryItem(this, HasilPasienMetadata.ColumnNames.OutRange, esSystemType.String);
            }
        }

        public esQueryItem Satuan
        {
            get
            {
                return new esQueryItem(this, HasilPasienMetadata.ColumnNames.Satuan, esSystemType.String);
            }
        }

        public esQueryItem StandarValue
        {
            get
            {
                return new esQueryItem(this, HasilPasienMetadata.ColumnNames.StandarValue, esSystemType.String);
            }
        }

        public esQueryItem OrderLabCritical
        {
            get
            {
                return new esQueryItem(this, HasilPasienMetadata.ColumnNames.OrderLabCritical, esSystemType.String);
            }
        }

        public esQueryItem Seq
        {
            get
            {
                return new esQueryItem(this, HasilPasienMetadata.ColumnNames.Seq, esSystemType.Int32);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("HasilPasienCollection")]
    public partial class HasilPasienCollection : esHasilPasienCollection, IEnumerable<HasilPasien>
    {
        public HasilPasienCollection()
        {

        }

        public static implicit operator List<HasilPasien>(HasilPasienCollection coll)
        {
            List<HasilPasien> list = new List<HasilPasien>();

            foreach (HasilPasien emp in coll)
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
                return HasilPasienMetadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new HasilPasienQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new HasilPasien(row);
        }

        override protected esEntity CreateEntity()
        {
            return new HasilPasien();
        }


        #endregion


        [BrowsableAttribute(false)]
        public HasilPasienQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new HasilPasienQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(HasilPasienQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public HasilPasien AddNew()
        {
            HasilPasien entity = base.AddNewEntity() as HasilPasien;

            return entity;
        }

        public HasilPasien FindByPrimaryKey()
        {
            return base.FindByPrimaryKey() as HasilPasien;
        }


        #region IEnumerable<HasilPasien> Members

        IEnumerator<HasilPasien> IEnumerable<HasilPasien>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as HasilPasien;
            }
        }

        #endregion

        private HasilPasienQuery query;
    }


    /// <summary>
    /// Encapsulates the 'HasilPasien' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("HasilPasien ()")]
    [Serializable]
    public partial class HasilPasien : esHasilPasien
    {
        public HasilPasien()
        {

        }

        public HasilPasien(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return HasilPasienMetadata.Meta();
            }
        }



        override protected esHasilPasienQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new HasilPasienQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public HasilPasienQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new HasilPasienQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(HasilPasienQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private HasilPasienQuery query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class HasilPasienQuery : esHasilPasienQuery
    {
        public HasilPasienQuery()
        {

        }

        public HasilPasienQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "HasilPasienQuery";
        }


    }


    [Serializable]
    public partial class HasilPasienMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected HasilPasienMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(HasilPasienMetadata.ColumnNames.OrderLabNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = HasilPasienMetadata.PropertyNames.OrderLabNo;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(HasilPasienMetadata.ColumnNames.OrderLabTglOrder, 1, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = HasilPasienMetadata.PropertyNames.OrderLabTglOrder;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(HasilPasienMetadata.ColumnNames.OrderLabNoMR, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = HasilPasienMetadata.PropertyNames.OrderLabNoMR;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(HasilPasienMetadata.ColumnNames.OrderLabNama, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = HasilPasienMetadata.PropertyNames.OrderLabNama;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(HasilPasienMetadata.ColumnNames.CheckupResultGroupCode, 4, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = HasilPasienMetadata.PropertyNames.CheckupResultGroupCode;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(HasilPasienMetadata.ColumnNames.CheckupResultGroupName, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = HasilPasienMetadata.PropertyNames.CheckupResultGroupName;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(HasilPasienMetadata.ColumnNames.CheckupResultTestCode, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = HasilPasienMetadata.PropertyNames.CheckupResultTestCode;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(HasilPasienMetadata.ColumnNames.CheckupResultTestName, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = HasilPasienMetadata.PropertyNames.CheckupResultTestName;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(HasilPasienMetadata.ColumnNames.CheckupResultFractionCode, 8, typeof(System.String), esSystemType.String);
            c.PropertyName = HasilPasienMetadata.PropertyNames.CheckupResultFractionCode;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(HasilPasienMetadata.ColumnNames.CheckupResultFractionName, 9, typeof(System.String), esSystemType.String);
            c.PropertyName = HasilPasienMetadata.PropertyNames.CheckupResultFractionName;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(HasilPasienMetadata.ColumnNames.WithinRange, 10, typeof(System.String), esSystemType.String);
            c.PropertyName = HasilPasienMetadata.PropertyNames.WithinRange;
            c.CharacterMaxLength = 100;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(HasilPasienMetadata.ColumnNames.OutRange, 11, typeof(System.String), esSystemType.String);
            c.PropertyName = HasilPasienMetadata.PropertyNames.OutRange;
            c.CharacterMaxLength = 100;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(HasilPasienMetadata.ColumnNames.Satuan, 12, typeof(System.String), esSystemType.String);
            c.PropertyName = HasilPasienMetadata.PropertyNames.Satuan;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(HasilPasienMetadata.ColumnNames.StandarValue, 13, typeof(System.String), esSystemType.String);
            c.PropertyName = HasilPasienMetadata.PropertyNames.StandarValue;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(HasilPasienMetadata.ColumnNames.OrderLabCritical, 14, typeof(System.String), esSystemType.String);
            c.PropertyName = HasilPasienMetadata.PropertyNames.OrderLabCritical;
            c.CharacterMaxLength = 1;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(HasilPasienMetadata.ColumnNames.Seq, 15, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = HasilPasienMetadata.PropertyNames.Seq;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public HasilPasienMetadata Meta()
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
            public const string OrderLabNo = "OrderLabNo";
            public const string OrderLabTglOrder = "OrderLabTglOrder";
            public const string OrderLabNoMR = "OrderLabNoMR";
            public const string OrderLabNama = "OrderLabNama";
            public const string CheckupResultGroupCode = "CheckupResultGroupCode";
            public const string CheckupResultGroupName = "CheckupResultGroupName";
            public const string CheckupResultTestCode = "CheckupResultTestCode";
            public const string CheckupResultTestName = "CheckupResultTestName";
            public const string CheckupResultFractionCode = "CheckupResultFractionCode";
            public const string CheckupResultFractionName = "CheckupResultFractionName";
            public const string WithinRange = "WithinRange";
            public const string OutRange = "OutRange";
            public const string Satuan = "Satuan";
            public const string StandarValue = "StandarValue";
            public const string OrderLabCritical = "OrderLabCritical";
            public const string Seq = "Seq";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string OrderLabNo = "OrderLabNo";
            public const string OrderLabTglOrder = "OrderLabTglOrder";
            public const string OrderLabNoMR = "OrderLabNoMR";
            public const string OrderLabNama = "OrderLabNama";
            public const string CheckupResultGroupCode = "CheckupResultGroupCode";
            public const string CheckupResultGroupName = "CheckupResultGroupName";
            public const string CheckupResultTestCode = "CheckupResultTestCode";
            public const string CheckupResultTestName = "CheckupResultTestName";
            public const string CheckupResultFractionCode = "CheckupResultFractionCode";
            public const string CheckupResultFractionName = "CheckupResultFractionName";
            public const string WithinRange = "WithinRange";
            public const string OutRange = "OutRange";
            public const string Satuan = "Satuan";
            public const string StandarValue = "StandarValue";
            public const string OrderLabCritical = "OrderLabCritical";
            public const string Seq = "Seq";
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
            lock (typeof(HasilPasienMetadata))
            {
                if (HasilPasienMetadata.mapDelegates == null)
                {
                    HasilPasienMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (HasilPasienMetadata.meta == null)
                {
                    HasilPasienMetadata.meta = new HasilPasienMetadata();
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


                meta.AddTypeMap("OrderLabNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("OrderLabTglOrder", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("OrderLabNoMR", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("OrderLabNama", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("CheckupResultGroupCode", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("CheckupResultGroupName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("CheckupResultTestCode", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("CheckupResultTestName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("CheckupResultFractionCode", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("CheckupResultFractionName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("WithinRange", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("OutRange", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Satuan", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("StandarValue", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("OrderLabCritical", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Seq", new esTypeMap("int", "System.Int32"));



                meta.Source = "HasilPasien";
                meta.Destination = "HasilPasien";

                meta.spInsert = "proc_HasilPasienInsert";
                meta.spUpdate = "proc_HasilPasienUpdate";
                meta.spDelete = "proc_HasilPasienDelete";
                meta.spLoadAll = "proc_HasilPasienLoadAll";
                meta.spLoadByPrimaryKey = "proc_HasilPasienLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private HasilPasienMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
