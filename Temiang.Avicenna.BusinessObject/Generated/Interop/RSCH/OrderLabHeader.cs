/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 8/3/2016 9:33:12 AM
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
    abstract public class esOrderLabHeaderCollection : esEntityCollectionWAuditLog
    {
        public esOrderLabHeaderCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "OrderLabHeaderCollection";
        }

        #region Query Logic
        protected void InitQuery(esOrderLabHeaderQuery query)
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
            this.InitQuery(query as esOrderLabHeaderQuery);
        }
        #endregion

        virtual public OrderLabHeader DetachEntity(OrderLabHeader entity)
        {
            return base.DetachEntity(entity) as OrderLabHeader;
        }

        virtual public OrderLabHeader AttachEntity(OrderLabHeader entity)
        {
            return base.AttachEntity(entity) as OrderLabHeader;
        }

        virtual public void Combine(OrderLabHeaderCollection collection)
        {
            base.Combine(collection);
        }

        new public OrderLabHeader this[int index]
        {
            get
            {
                return base[index] as OrderLabHeader;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(OrderLabHeader);
        }
    }



    [Serializable]
    abstract public class esOrderLabHeader : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esOrderLabHeaderQuery GetDynamicQuery()
        {
            return null;
        }

        public esOrderLabHeader()
        {

        }

        public esOrderLabHeader(DataRow row)
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

        //public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, )
        //{
        //    if (sqlAccessType == esSqlAccessType.DynamicSQL)
        //        return LoadByPrimaryKeyDynamic();
        //    else
        //        return LoadByPrimaryKeyStoredProcedure();
        //}

        private bool LoadByPrimaryKeyDynamic()
        {
            esOrderLabHeaderQuery query = this.GetDynamicQuery();
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
                        case "OrderLabNamaPengirim": this.str.OrderLabNamaPengirim = (string)value; break;
                        case "OrderLabKdPoli": this.str.OrderLabKdPoli = (string)value; break;
                        case "OrderLabBirthdate": this.str.OrderLabBirthdate = (string)value; break;
                        case "OrderLabAgeYear": this.str.OrderLabAgeYear = (string)value; break;
                        case "OrderLabAgeMonth": this.str.OrderLabAgeMonth = (string)value; break;
                        case "OrderLabAgeDay": this.str.OrderLabAgeDay = (string)value; break;
                        case "OrderLabSex": this.str.OrderLabSex = (string)value; break;
                        case "OrderLabKdPengirim": this.str.OrderLabKdPengirim = (string)value; break;
                        case "OrderlabNamaPoli": this.str.OrderlabNamaPoli = (string)value; break;
                        case "OrderLabJamOrder": this.str.OrderLabJamOrder = (string)value; break;
                        case "OrderLabStatus": this.str.OrderLabStatus = (string)value; break;
                        case "OrderLabNoBed": this.str.OrderLabNoBed = (string)value; break;
                        case "GuarantorName": this.str.GuarantorName = (string)value; break;
                        case "DiagnoseText": this.str.DiagnoseText = (string)value; break;
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

                        case "OrderLabBirthdate":

                            if (value == null || value is System.DateTime)
                                this.OrderLabBirthdate = (System.DateTime?)value;
                            break;

                        case "OrderLabAgeYear":

                            if (value == null || value is System.Int32)
                                this.OrderLabAgeYear = (System.Int32?)value;
                            break;

                        case "OrderLabAgeMonth":

                            if (value == null || value is System.Int32)
                                this.OrderLabAgeMonth = (System.Int32?)value;
                            break;

                        case "OrderLabAgeDay":

                            if (value == null || value is System.Int32)
                                this.OrderLabAgeDay = (System.Int32?)value;
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
        /// Maps to OrderLabHeader.OrderLabNo
        /// </summary>
        virtual public System.String OrderLabNo
        {
            get
            {
                return base.GetSystemString(OrderLabHeaderMetadata.ColumnNames.OrderLabNo);
            }

            set
            {
                base.SetSystemString(OrderLabHeaderMetadata.ColumnNames.OrderLabNo, value);
            }
        }

        /// <summary>
        /// Maps to OrderLabHeader.OrderLabTglOrder
        /// </summary>
        virtual public System.DateTime? OrderLabTglOrder
        {
            get
            {
                return base.GetSystemDateTime(OrderLabHeaderMetadata.ColumnNames.OrderLabTglOrder);
            }

            set
            {
                base.SetSystemDateTime(OrderLabHeaderMetadata.ColumnNames.OrderLabTglOrder, value);
            }
        }

        /// <summary>
        /// Maps to OrderLabHeader.OrderLabNoMR
        /// </summary>
        virtual public System.String OrderLabNoMR
        {
            get
            {
                return base.GetSystemString(OrderLabHeaderMetadata.ColumnNames.OrderLabNoMR);
            }

            set
            {
                base.SetSystemString(OrderLabHeaderMetadata.ColumnNames.OrderLabNoMR, value);
            }
        }

        /// <summary>
        /// Maps to OrderLabHeader.OrderLabNama
        /// </summary>
        virtual public System.String OrderLabNama
        {
            get
            {
                return base.GetSystemString(OrderLabHeaderMetadata.ColumnNames.OrderLabNama);
            }

            set
            {
                base.SetSystemString(OrderLabHeaderMetadata.ColumnNames.OrderLabNama, value);
            }
        }

        /// <summary>
        /// Maps to OrderLabHeader.OrderLabNamaPengirim
        /// </summary>
        virtual public System.String OrderLabNamaPengirim
        {
            get
            {
                return base.GetSystemString(OrderLabHeaderMetadata.ColumnNames.OrderLabNamaPengirim);
            }

            set
            {
                base.SetSystemString(OrderLabHeaderMetadata.ColumnNames.OrderLabNamaPengirim, value);
            }
        }

        /// <summary>
        /// Maps to OrderLabHeader.OrderLabKdPoli
        /// </summary>
        virtual public System.String OrderLabKdPoli
        {
            get
            {
                return base.GetSystemString(OrderLabHeaderMetadata.ColumnNames.OrderLabKdPoli);
            }

            set
            {
                base.SetSystemString(OrderLabHeaderMetadata.ColumnNames.OrderLabKdPoli, value);
            }
        }

        /// <summary>
        /// Maps to OrderLabHeader.OrderLabBirthdate
        /// </summary>
        virtual public System.DateTime? OrderLabBirthdate
        {
            get
            {
                return base.GetSystemDateTime(OrderLabHeaderMetadata.ColumnNames.OrderLabBirthdate);
            }

            set
            {
                base.SetSystemDateTime(OrderLabHeaderMetadata.ColumnNames.OrderLabBirthdate, value);
            }
        }

        /// <summary>
        /// Maps to OrderLabHeader.OrderLabAgeYear
        /// </summary>
        virtual public System.Int32? OrderLabAgeYear
        {
            get
            {
                return base.GetSystemInt32(OrderLabHeaderMetadata.ColumnNames.OrderLabAgeYear);
            }

            set
            {
                base.SetSystemInt32(OrderLabHeaderMetadata.ColumnNames.OrderLabAgeYear, value);
            }
        }

        /// <summary>
        /// Maps to OrderLabHeader.OrderLabAgeMonth
        /// </summary>
        virtual public System.Int32? OrderLabAgeMonth
        {
            get
            {
                return base.GetSystemInt32(OrderLabHeaderMetadata.ColumnNames.OrderLabAgeMonth);
            }

            set
            {
                base.SetSystemInt32(OrderLabHeaderMetadata.ColumnNames.OrderLabAgeMonth, value);
            }
        }

        /// <summary>
        /// Maps to OrderLabHeader.OrderLabAgeDay
        /// </summary>
        virtual public System.Int32? OrderLabAgeDay
        {
            get
            {
                return base.GetSystemInt32(OrderLabHeaderMetadata.ColumnNames.OrderLabAgeDay);
            }

            set
            {
                base.SetSystemInt32(OrderLabHeaderMetadata.ColumnNames.OrderLabAgeDay, value);
            }
        }

        /// <summary>
        /// Maps to OrderLabHeader.OrderLabSex
        /// </summary>
        virtual public System.String OrderLabSex
        {
            get
            {
                return base.GetSystemString(OrderLabHeaderMetadata.ColumnNames.OrderLabSex);
            }

            set
            {
                base.SetSystemString(OrderLabHeaderMetadata.ColumnNames.OrderLabSex, value);
            }
        }

        /// <summary>
        /// Maps to OrderLabHeader.OrderLabKdPengirim
        /// </summary>
        virtual public System.String OrderLabKdPengirim
        {
            get
            {
                return base.GetSystemString(OrderLabHeaderMetadata.ColumnNames.OrderLabKdPengirim);
            }

            set
            {
                base.SetSystemString(OrderLabHeaderMetadata.ColumnNames.OrderLabKdPengirim, value);
            }
        }

        /// <summary>
        /// Maps to OrderLabHeader.orderlabNamaPoli
        /// </summary>
        virtual public System.String OrderlabNamaPoli
        {
            get
            {
                return base.GetSystemString(OrderLabHeaderMetadata.ColumnNames.OrderlabNamaPoli);
            }

            set
            {
                base.SetSystemString(OrderLabHeaderMetadata.ColumnNames.OrderlabNamaPoli, value);
            }
        }

        /// <summary>
        /// Maps to OrderLabHeader.OrderLabJamOrder
        /// </summary>
        virtual public System.String OrderLabJamOrder
        {
            get
            {
                return base.GetSystemString(OrderLabHeaderMetadata.ColumnNames.OrderLabJamOrder);
            }

            set
            {
                base.SetSystemString(OrderLabHeaderMetadata.ColumnNames.OrderLabJamOrder, value);
            }
        }

        /// <summary>
        /// Maps to OrderLabHeader.OrderLabStatus
        /// </summary>
        virtual public System.String OrderLabStatus
        {
            get
            {
                return base.GetSystemString(OrderLabHeaderMetadata.ColumnNames.OrderLabStatus);
            }

            set
            {
                base.SetSystemString(OrderLabHeaderMetadata.ColumnNames.OrderLabStatus, value);
            }
        }

        /// <summary>
        /// Maps to OrderLabHeader.OrderLabNoBed
        /// </summary>
        virtual public System.String OrderLabNoBed
        {
            get
            {
                return base.GetSystemString(OrderLabHeaderMetadata.ColumnNames.OrderLabNoBed);
            }

            set
            {
                base.SetSystemString(OrderLabHeaderMetadata.ColumnNames.OrderLabNoBed, value);
            }
        }

        /// <summary>
        /// Maps to OrderLabHeader.GuarantorName
        /// </summary>
        virtual public System.String GuarantorName
        {
            get
            {
                return base.GetSystemString(OrderLabHeaderMetadata.ColumnNames.GuarantorName);
            }

            set
            {
                base.SetSystemString(OrderLabHeaderMetadata.ColumnNames.GuarantorName, value);
            }
        }

        /// <summary>
        /// Maps to OrderLabHeader.DiagnoseText
        /// </summary>
        virtual public System.String DiagnoseText
        {
            get
            {
                return base.GetSystemString(OrderLabHeaderMetadata.ColumnNames.DiagnoseText);
            }

            set
            {
                base.SetSystemString(OrderLabHeaderMetadata.ColumnNames.DiagnoseText, value);
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
            public esStrings(esOrderLabHeader entity)
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

            public System.String OrderLabNamaPengirim
            {
                get
                {
                    System.String data = entity.OrderLabNamaPengirim;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.OrderLabNamaPengirim = null;
                    else entity.OrderLabNamaPengirim = Convert.ToString(value);
                }
            }

            public System.String OrderLabKdPoli
            {
                get
                {
                    System.String data = entity.OrderLabKdPoli;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.OrderLabKdPoli = null;
                    else entity.OrderLabKdPoli = Convert.ToString(value);
                }
            }

            public System.String OrderLabBirthdate
            {
                get
                {
                    System.DateTime? data = entity.OrderLabBirthdate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.OrderLabBirthdate = null;
                    else entity.OrderLabBirthdate = Convert.ToDateTime(value);
                }
            }

            public System.String OrderLabAgeYear
            {
                get
                {
                    System.Int32? data = entity.OrderLabAgeYear;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.OrderLabAgeYear = null;
                    else entity.OrderLabAgeYear = Convert.ToInt32(value);
                }
            }

            public System.String OrderLabAgeMonth
            {
                get
                {
                    System.Int32? data = entity.OrderLabAgeMonth;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.OrderLabAgeMonth = null;
                    else entity.OrderLabAgeMonth = Convert.ToInt32(value);
                }
            }

            public System.String OrderLabAgeDay
            {
                get
                {
                    System.Int32? data = entity.OrderLabAgeDay;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.OrderLabAgeDay = null;
                    else entity.OrderLabAgeDay = Convert.ToInt32(value);
                }
            }

            public System.String OrderLabSex
            {
                get
                {
                    System.String data = entity.OrderLabSex;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.OrderLabSex = null;
                    else entity.OrderLabSex = Convert.ToString(value);
                }
            }

            public System.String OrderLabKdPengirim
            {
                get
                {
                    System.String data = entity.OrderLabKdPengirim;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.OrderLabKdPengirim = null;
                    else entity.OrderLabKdPengirim = Convert.ToString(value);
                }
            }

            public System.String OrderlabNamaPoli
            {
                get
                {
                    System.String data = entity.OrderlabNamaPoli;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.OrderlabNamaPoli = null;
                    else entity.OrderlabNamaPoli = Convert.ToString(value);
                }
            }

            public System.String OrderLabJamOrder
            {
                get
                {
                    System.String data = entity.OrderLabJamOrder;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.OrderLabJamOrder = null;
                    else entity.OrderLabJamOrder = Convert.ToString(value);
                }
            }

            public System.String OrderLabStatus
            {
                get
                {
                    System.String data = entity.OrderLabStatus;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.OrderLabStatus = null;
                    else entity.OrderLabStatus = Convert.ToString(value);
                }
            }

            public System.String OrderLabNoBed
            {
                get
                {
                    System.String data = entity.OrderLabNoBed;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.OrderLabNoBed = null;
                    else entity.OrderLabNoBed = Convert.ToString(value);
                }
            }

            public System.String GuarantorName
            {
                get
                {
                    System.String data = entity.GuarantorName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.GuarantorName = null;
                    else entity.GuarantorName = Convert.ToString(value);
                }
            }

            public System.String DiagnoseText
            {
                get
                {
                    System.String data = entity.DiagnoseText;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DiagnoseText = null;
                    else entity.DiagnoseText = Convert.ToString(value);
                }
            }


            private esOrderLabHeader entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esOrderLabHeaderQuery query)
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
                throw new Exception("esOrderLabHeader can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    public partial class OrderLabHeader : esOrderLabHeader
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
    abstract public class esOrderLabHeaderQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return OrderLabHeaderMetadata.Meta();
            }
        }


        public esQueryItem OrderLabNo
        {
            get
            {
                return new esQueryItem(this, OrderLabHeaderMetadata.ColumnNames.OrderLabNo, esSystemType.String);
            }
        }

        public esQueryItem OrderLabTglOrder
        {
            get
            {
                return new esQueryItem(this, OrderLabHeaderMetadata.ColumnNames.OrderLabTglOrder, esSystemType.DateTime);
            }
        }

        public esQueryItem OrderLabNoMR
        {
            get
            {
                return new esQueryItem(this, OrderLabHeaderMetadata.ColumnNames.OrderLabNoMR, esSystemType.String);
            }
        }

        public esQueryItem OrderLabNama
        {
            get
            {
                return new esQueryItem(this, OrderLabHeaderMetadata.ColumnNames.OrderLabNama, esSystemType.String);
            }
        }

        public esQueryItem OrderLabNamaPengirim
        {
            get
            {
                return new esQueryItem(this, OrderLabHeaderMetadata.ColumnNames.OrderLabNamaPengirim, esSystemType.String);
            }
        }

        public esQueryItem OrderLabKdPoli
        {
            get
            {
                return new esQueryItem(this, OrderLabHeaderMetadata.ColumnNames.OrderLabKdPoli, esSystemType.String);
            }
        }

        public esQueryItem OrderLabBirthdate
        {
            get
            {
                return new esQueryItem(this, OrderLabHeaderMetadata.ColumnNames.OrderLabBirthdate, esSystemType.DateTime);
            }
        }

        public esQueryItem OrderLabAgeYear
        {
            get
            {
                return new esQueryItem(this, OrderLabHeaderMetadata.ColumnNames.OrderLabAgeYear, esSystemType.Int32);
            }
        }

        public esQueryItem OrderLabAgeMonth
        {
            get
            {
                return new esQueryItem(this, OrderLabHeaderMetadata.ColumnNames.OrderLabAgeMonth, esSystemType.Int32);
            }
        }

        public esQueryItem OrderLabAgeDay
        {
            get
            {
                return new esQueryItem(this, OrderLabHeaderMetadata.ColumnNames.OrderLabAgeDay, esSystemType.Int32);
            }
        }

        public esQueryItem OrderLabSex
        {
            get
            {
                return new esQueryItem(this, OrderLabHeaderMetadata.ColumnNames.OrderLabSex, esSystemType.String);
            }
        }

        public esQueryItem OrderLabKdPengirim
        {
            get
            {
                return new esQueryItem(this, OrderLabHeaderMetadata.ColumnNames.OrderLabKdPengirim, esSystemType.String);
            }
        }

        public esQueryItem OrderlabNamaPoli
        {
            get
            {
                return new esQueryItem(this, OrderLabHeaderMetadata.ColumnNames.OrderlabNamaPoli, esSystemType.String);
            }
        }

        public esQueryItem OrderLabJamOrder
        {
            get
            {
                return new esQueryItem(this, OrderLabHeaderMetadata.ColumnNames.OrderLabJamOrder, esSystemType.String);
            }
        }

        public esQueryItem OrderLabStatus
        {
            get
            {
                return new esQueryItem(this, OrderLabHeaderMetadata.ColumnNames.OrderLabStatus, esSystemType.String);
            }
        }

        public esQueryItem OrderLabNoBed
        {
            get
            {
                return new esQueryItem(this, OrderLabHeaderMetadata.ColumnNames.OrderLabNoBed, esSystemType.String);
            }
        }

        public esQueryItem GuarantorName
        {
            get
            {
                return new esQueryItem(this, OrderLabHeaderMetadata.ColumnNames.GuarantorName, esSystemType.String);
            }
        }

        public esQueryItem DiagnoseText
        {
            get
            {
                return new esQueryItem(this, OrderLabHeaderMetadata.ColumnNames.DiagnoseText, esSystemType.String);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("OrderLabHeaderCollection")]
    public partial class OrderLabHeaderCollection : esOrderLabHeaderCollection, IEnumerable<OrderLabHeader>
    {
        public OrderLabHeaderCollection()
        {

        }

        public static implicit operator List<OrderLabHeader>(OrderLabHeaderCollection coll)
        {
            List<OrderLabHeader> list = new List<OrderLabHeader>();

            foreach (OrderLabHeader emp in coll)
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
                return OrderLabHeaderMetadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new OrderLabHeaderQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new OrderLabHeader(row);
        }

        override protected esEntity CreateEntity()
        {
            return new OrderLabHeader();
        }


        #endregion


        [BrowsableAttribute(false)]
        public OrderLabHeaderQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new OrderLabHeaderQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(OrderLabHeaderQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public OrderLabHeader AddNew()
        {
            OrderLabHeader entity = base.AddNewEntity() as OrderLabHeader;

            return entity;
        }

        public OrderLabHeader FindByPrimaryKey()
        {
            return base.FindByPrimaryKey() as OrderLabHeader;
        }


        #region IEnumerable<OrderLabHeader> Members

        IEnumerator<OrderLabHeader> IEnumerable<OrderLabHeader>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as OrderLabHeader;
            }
        }

        #endregion

        private OrderLabHeaderQuery query;
    }


    /// <summary>
    /// Encapsulates the 'OrderLabHeader' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("OrderLabHeader ()")]
    [Serializable]
    public partial class OrderLabHeader : esOrderLabHeader
    {
        public OrderLabHeader()
        {

        }

        public OrderLabHeader(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return OrderLabHeaderMetadata.Meta();
            }
        }



        override protected esOrderLabHeaderQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new OrderLabHeaderQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public OrderLabHeaderQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new OrderLabHeaderQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(OrderLabHeaderQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private OrderLabHeaderQuery query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class OrderLabHeaderQuery : esOrderLabHeaderQuery
    {
        public OrderLabHeaderQuery()
        {

        }

        public OrderLabHeaderQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "OrderLabHeaderQuery";
        }


    }


    [Serializable]
    public partial class OrderLabHeaderMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected OrderLabHeaderMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(OrderLabHeaderMetadata.ColumnNames.OrderLabNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = OrderLabHeaderMetadata.PropertyNames.OrderLabNo;
            c.CharacterMaxLength = 15;
            _columns.Add(c);

            c = new esColumnMetadata(OrderLabHeaderMetadata.ColumnNames.OrderLabTglOrder, 1, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = OrderLabHeaderMetadata.PropertyNames.OrderLabTglOrder;
            _columns.Add(c);

            c = new esColumnMetadata(OrderLabHeaderMetadata.ColumnNames.OrderLabNoMR, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = OrderLabHeaderMetadata.PropertyNames.OrderLabNoMR;
            c.CharacterMaxLength = 15;
            _columns.Add(c);

            c = new esColumnMetadata(OrderLabHeaderMetadata.ColumnNames.OrderLabNama, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = OrderLabHeaderMetadata.PropertyNames.OrderLabNama;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(OrderLabHeaderMetadata.ColumnNames.OrderLabNamaPengirim, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = OrderLabHeaderMetadata.PropertyNames.OrderLabNamaPengirim;
            c.CharacterMaxLength = 50;
            _columns.Add(c);

            c = new esColumnMetadata(OrderLabHeaderMetadata.ColumnNames.OrderLabKdPoli, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = OrderLabHeaderMetadata.PropertyNames.OrderLabKdPoli;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(OrderLabHeaderMetadata.ColumnNames.OrderLabBirthdate, 6, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = OrderLabHeaderMetadata.PropertyNames.OrderLabBirthdate;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(OrderLabHeaderMetadata.ColumnNames.OrderLabAgeYear, 7, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = OrderLabHeaderMetadata.PropertyNames.OrderLabAgeYear;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(OrderLabHeaderMetadata.ColumnNames.OrderLabAgeMonth, 8, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = OrderLabHeaderMetadata.PropertyNames.OrderLabAgeMonth;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(OrderLabHeaderMetadata.ColumnNames.OrderLabAgeDay, 9, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = OrderLabHeaderMetadata.PropertyNames.OrderLabAgeDay;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(OrderLabHeaderMetadata.ColumnNames.OrderLabSex, 10, typeof(System.String), esSystemType.String);
            c.PropertyName = OrderLabHeaderMetadata.PropertyNames.OrderLabSex;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(OrderLabHeaderMetadata.ColumnNames.OrderLabKdPengirim, 11, typeof(System.String), esSystemType.String);
            c.PropertyName = OrderLabHeaderMetadata.PropertyNames.OrderLabKdPengirim;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(OrderLabHeaderMetadata.ColumnNames.OrderlabNamaPoli, 12, typeof(System.String), esSystemType.String);
            c.PropertyName = OrderLabHeaderMetadata.PropertyNames.OrderlabNamaPoli;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(OrderLabHeaderMetadata.ColumnNames.OrderLabJamOrder, 13, typeof(System.String), esSystemType.String);
            c.PropertyName = OrderLabHeaderMetadata.PropertyNames.OrderLabJamOrder;
            c.CharacterMaxLength = 5;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(OrderLabHeaderMetadata.ColumnNames.OrderLabStatus, 14, typeof(System.String), esSystemType.String);
            c.PropertyName = OrderLabHeaderMetadata.PropertyNames.OrderLabStatus;
            c.CharacterMaxLength = 1;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(OrderLabHeaderMetadata.ColumnNames.OrderLabNoBed, 15, typeof(System.String), esSystemType.String);
            c.PropertyName = OrderLabHeaderMetadata.PropertyNames.OrderLabNoBed;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(OrderLabHeaderMetadata.ColumnNames.GuarantorName, 16, typeof(System.String), esSystemType.String);
            c.PropertyName = OrderLabHeaderMetadata.PropertyNames.GuarantorName;
            c.CharacterMaxLength = 255;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(OrderLabHeaderMetadata.ColumnNames.DiagnoseText, 17, typeof(System.String), esSystemType.String);
            c.PropertyName = OrderLabHeaderMetadata.PropertyNames.DiagnoseText;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public OrderLabHeaderMetadata Meta()
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
            public const string OrderLabNamaPengirim = "OrderLabNamaPengirim";
            public const string OrderLabKdPoli = "OrderLabKdPoli";
            public const string OrderLabBirthdate = "OrderLabBirthdate";
            public const string OrderLabAgeYear = "OrderLabAgeYear";
            public const string OrderLabAgeMonth = "OrderLabAgeMonth";
            public const string OrderLabAgeDay = "OrderLabAgeDay";
            public const string OrderLabSex = "OrderLabSex";
            public const string OrderLabKdPengirim = "OrderLabKdPengirim";
            public const string OrderlabNamaPoli = "orderlabNamaPoli";
            public const string OrderLabJamOrder = "OrderLabJamOrder";
            public const string OrderLabStatus = "OrderLabStatus";
            public const string OrderLabNoBed = "OrderLabNoBed";
            public const string GuarantorName = "GuarantorName";
            public const string DiagnoseText = "DiagnoseText";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string OrderLabNo = "OrderLabNo";
            public const string OrderLabTglOrder = "OrderLabTglOrder";
            public const string OrderLabNoMR = "OrderLabNoMR";
            public const string OrderLabNama = "OrderLabNama";
            public const string OrderLabNamaPengirim = "OrderLabNamaPengirim";
            public const string OrderLabKdPoli = "OrderLabKdPoli";
            public const string OrderLabBirthdate = "OrderLabBirthdate";
            public const string OrderLabAgeYear = "OrderLabAgeYear";
            public const string OrderLabAgeMonth = "OrderLabAgeMonth";
            public const string OrderLabAgeDay = "OrderLabAgeDay";
            public const string OrderLabSex = "OrderLabSex";
            public const string OrderLabKdPengirim = "OrderLabKdPengirim";
            public const string OrderlabNamaPoli = "OrderlabNamaPoli";
            public const string OrderLabJamOrder = "OrderLabJamOrder";
            public const string OrderLabStatus = "OrderLabStatus";
            public const string OrderLabNoBed = "OrderLabNoBed";
            public const string GuarantorName = "GuarantorName";
            public const string DiagnoseText = "DiagnoseText";
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
            lock (typeof(OrderLabHeaderMetadata))
            {
                if (OrderLabHeaderMetadata.mapDelegates == null)
                {
                    OrderLabHeaderMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (OrderLabHeaderMetadata.meta == null)
                {
                    OrderLabHeaderMetadata.meta = new OrderLabHeaderMetadata();
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
                meta.AddTypeMap("OrderLabNamaPengirim", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("OrderLabKdPoli", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("OrderLabBirthdate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("OrderLabAgeYear", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("OrderLabAgeMonth", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("OrderLabAgeDay", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("OrderLabSex", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("OrderLabKdPengirim", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("OrderlabNamaPoli", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("OrderLabJamOrder", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("OrderLabStatus", new esTypeMap("char", "System.String"));
                meta.AddTypeMap("OrderLabNoBed", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("GuarantorName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("DiagnoseText", new esTypeMap("varchar", "System.String"));



                meta.Source = "OrderLabHeader";
                meta.Destination = "OrderLabHeader";

                meta.spInsert = "proc_OrderLabHeaderInsert";
                meta.spUpdate = "proc_OrderLabHeaderUpdate";
                meta.spDelete = "proc_OrderLabHeaderDelete";
                meta.spLoadAll = "proc_OrderLabHeaderLoadAll";
                meta.spLoadByPrimaryKey = "proc_OrderLabHeaderLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private OrderLabHeaderMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
