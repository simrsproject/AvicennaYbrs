/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 10/4/2017 8:45:31 AM
===============================================================================
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;
using System.Xml.Serialization;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
    [Serializable]
    abstract public class esTransChargesItemCompCollection : esEntityCollectionWAuditLog
    {
        public esTransChargesItemCompCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "TransChargesItemCompCollection";
        }

        #region Query Logic
        protected void InitQuery(esTransChargesItemCompQuery query)
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
            this.InitQuery(query as esTransChargesItemCompQuery);
        }
        #endregion

        virtual public TransChargesItemComp DetachEntity(TransChargesItemComp entity)
        {
            return base.DetachEntity(entity) as TransChargesItemComp;
        }

        virtual public TransChargesItemComp AttachEntity(TransChargesItemComp entity)
        {
            return base.AttachEntity(entity) as TransChargesItemComp;
        }

        virtual public void Combine(TransChargesItemCompCollection collection)
        {
            base.Combine(collection);
        }

        new public TransChargesItemComp this[int index]
        {
            get
            {
                return base[index] as TransChargesItemComp;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(TransChargesItemComp);
        }
    }

    [Serializable]
    abstract public class esTransChargesItemComp : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esTransChargesItemCompQuery GetDynamicQuery()
        {
            return null;
        }

        public esTransChargesItemComp()
        {
        }

        public esTransChargesItemComp(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String transactionNo, String sequenceNo, String tariffComponentID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(transactionNo, sequenceNo, tariffComponentID);
            else
                return LoadByPrimaryKeyStoredProcedure(transactionNo, sequenceNo, tariffComponentID);
        }

        /// <summary>
        /// Loads an entity by primary key
        /// </summary>
        /// <remarks>
        /// Requires primary keys be defined on all tables.
        /// If a table does not have a primary key set,
        /// this method will not compile.
        /// </remarks>
        /// <param name="sqlAccessType">Either esSqlAccessType StoredProcedure or DynamicSQL</param>
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String transactionNo, String sequenceNo, String tariffComponentID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(transactionNo, sequenceNo, tariffComponentID);
            else
                return LoadByPrimaryKeyStoredProcedure(transactionNo, sequenceNo, tariffComponentID);
        }

        private bool LoadByPrimaryKeyDynamic(String transactionNo, String sequenceNo, String tariffComponentID)
        {
            esTransChargesItemCompQuery query = this.GetDynamicQuery();
            query.Where(query.TransactionNo == transactionNo, query.SequenceNo == sequenceNo, query.TariffComponentID == tariffComponentID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String transactionNo, String sequenceNo, String tariffComponentID)
        {
            esParameters parms = new esParameters();
            parms.Add("TransactionNo", transactionNo);
            parms.Add("SequenceNo", sequenceNo);
            parms.Add("TariffComponentID", tariffComponentID);
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
                        case "TransactionNo": this.str.TransactionNo = (string)value; break;
                        case "SequenceNo": this.str.SequenceNo = (string)value; break;
                        case "TariffComponentID": this.str.TariffComponentID = (string)value; break;
                        case "Price": this.str.Price = (string)value; break;
                        case "DiscountAmount": this.str.DiscountAmount = (string)value; break;
                        case "ParamedicID": this.str.ParamedicID = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "IsPackage": this.str.IsPackage = (string)value; break;
                        case "AutoProcessCalculation": this.str.AutoProcessCalculation = (string)value; break;
                        case "CitoAmount": this.str.CitoAmount = (string)value; break;
                        case "FeeSettledNo": this.str.FeeSettledNo = (string)value; break;
                        case "FeeCalculated": this.str.FeeCalculated = (string)value; break;
                        case "FeeDiscountPercentage": this.str.FeeDiscountPercentage = (string)value; break;
                        case "FeeDiscount": this.str.FeeDiscount = (string)value; break;
                        case "PriceAdjusted": this.str.PriceAdjusted = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "Price":

                            if (value == null || value is System.Decimal)
                                this.Price = (System.Decimal?)value;
                            break;
                        case "DiscountAmount":

                            if (value == null || value is System.Decimal)
                                this.DiscountAmount = (System.Decimal?)value;
                            break;
                        case "LastUpdateDateTime":

                            if (value == null || value is System.DateTime)
                                this.LastUpdateDateTime = (System.DateTime?)value;
                            break;
                        case "IsPackage":

                            if (value == null || value is System.Boolean)
                                this.IsPackage = (System.Boolean?)value;
                            break;
                        case "AutoProcessCalculation":

                            if (value == null || value is System.Decimal)
                                this.AutoProcessCalculation = (System.Decimal?)value;
                            break;
                        case "CitoAmount":

                            if (value == null || value is System.Decimal)
                                this.CitoAmount = (System.Decimal?)value;
                            break;
                        case "FeeCalculated":

                            if (value == null || value is System.Decimal)
                                this.FeeCalculated = (System.Decimal?)value;
                            break;
                        case "FeeDiscountPercentage":

                            if (value == null || value is System.Decimal)
                                this.FeeDiscountPercentage = (System.Decimal?)value;
                            break;
                        case "FeeDiscount":

                            if (value == null || value is System.Decimal)
                                this.FeeDiscount = (System.Decimal?)value;
                            break;
                        case "PriceAdjusted":

                            if (value == null || value is System.Decimal)
                                this.PriceAdjusted = (System.Decimal?)value;
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
        /// Maps to TransChargesItemComp.TransactionNo
        /// </summary>
        virtual public System.String TransactionNo
        {
            get
            {
                return base.GetSystemString(TransChargesItemCompMetadata.ColumnNames.TransactionNo);
            }

            set
            {
                base.SetSystemString(TransChargesItemCompMetadata.ColumnNames.TransactionNo, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItemComp.SequenceNo
        /// </summary>
        virtual public System.String SequenceNo
        {
            get
            {
                return base.GetSystemString(TransChargesItemCompMetadata.ColumnNames.SequenceNo);
            }

            set
            {
                base.SetSystemString(TransChargesItemCompMetadata.ColumnNames.SequenceNo, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItemComp.TariffComponentID
        /// </summary>
        virtual public System.String TariffComponentID
        {
            get
            {
                return base.GetSystemString(TransChargesItemCompMetadata.ColumnNames.TariffComponentID);
            }

            set
            {
                base.SetSystemString(TransChargesItemCompMetadata.ColumnNames.TariffComponentID, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItemComp.Price
        /// </summary>
        virtual public System.Decimal? Price
        {
            get
            {
                return base.GetSystemDecimal(TransChargesItemCompMetadata.ColumnNames.Price);
            }

            set
            {
                base.SetSystemDecimal(TransChargesItemCompMetadata.ColumnNames.Price, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItemComp.DiscountAmount
        /// </summary>
        virtual public System.Decimal? DiscountAmount
        {
            get
            {
                return base.GetSystemDecimal(TransChargesItemCompMetadata.ColumnNames.DiscountAmount);
            }

            set
            {
                base.SetSystemDecimal(TransChargesItemCompMetadata.ColumnNames.DiscountAmount, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItemComp.ParamedicID
        /// </summary>
        virtual public System.String ParamedicID
        {
            get
            {
                return base.GetSystemString(TransChargesItemCompMetadata.ColumnNames.ParamedicID);
            }

            set
            {
                base.SetSystemString(TransChargesItemCompMetadata.ColumnNames.ParamedicID, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItemComp.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(TransChargesItemCompMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(TransChargesItemCompMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItemComp.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(TransChargesItemCompMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(TransChargesItemCompMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItemComp.IsPackage
        /// </summary>
        virtual public System.Boolean? IsPackage
        {
            get
            {
                return base.GetSystemBoolean(TransChargesItemCompMetadata.ColumnNames.IsPackage);
            }

            set
            {
                base.SetSystemBoolean(TransChargesItemCompMetadata.ColumnNames.IsPackage, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItemComp.AutoProcessCalculation
        /// </summary>
        virtual public System.Decimal? AutoProcessCalculation
        {
            get
            {
                return base.GetSystemDecimal(TransChargesItemCompMetadata.ColumnNames.AutoProcessCalculation);
            }

            set
            {
                base.SetSystemDecimal(TransChargesItemCompMetadata.ColumnNames.AutoProcessCalculation, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItemComp.CitoAmount
        /// </summary>
        virtual public System.Decimal? CitoAmount
        {
            get
            {
                return base.GetSystemDecimal(TransChargesItemCompMetadata.ColumnNames.CitoAmount);
            }

            set
            {
                base.SetSystemDecimal(TransChargesItemCompMetadata.ColumnNames.CitoAmount, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItemComp.FeeSettledNo
        /// </summary>
        virtual public System.String FeeSettledNo
        {
            get
            {
                return base.GetSystemString(TransChargesItemCompMetadata.ColumnNames.FeeSettledNo);
            }

            set
            {
                base.SetSystemString(TransChargesItemCompMetadata.ColumnNames.FeeSettledNo, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItemComp.FeeCalculated
        /// </summary>
        virtual public System.Decimal? FeeCalculated
        {
            get
            {
                return base.GetSystemDecimal(TransChargesItemCompMetadata.ColumnNames.FeeCalculated);
            }

            set
            {
                base.SetSystemDecimal(TransChargesItemCompMetadata.ColumnNames.FeeCalculated, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItemComp.FeeDiscountPercentage
        /// </summary>
        virtual public System.Decimal? FeeDiscountPercentage
        {
            get
            {
                return base.GetSystemDecimal(TransChargesItemCompMetadata.ColumnNames.FeeDiscountPercentage);
            }

            set
            {
                base.SetSystemDecimal(TransChargesItemCompMetadata.ColumnNames.FeeDiscountPercentage, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItemComp.FeeDiscount
        /// </summary>
        virtual public System.Decimal? FeeDiscount
        {
            get
            {
                return base.GetSystemDecimal(TransChargesItemCompMetadata.ColumnNames.FeeDiscount);
            }

            set
            {
                base.SetSystemDecimal(TransChargesItemCompMetadata.ColumnNames.FeeDiscount, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItemComp.PriceAdjusted
        /// </summary>
        virtual public System.Decimal? PriceAdjusted
        {
            get
            {
                return base.GetSystemDecimal(TransChargesItemCompMetadata.ColumnNames.PriceAdjusted);
            }

            set
            {
                base.SetSystemDecimal(TransChargesItemCompMetadata.ColumnNames.PriceAdjusted, value);
            }
        }

        #endregion

        #region String Properties

        /// <summary>
        /// Converts an entity's properties to
        /// and from strings.
        /// </summary>
        /// <remarks>
        /// The str properties Get and Set provide easy conversion
        /// between a string and a property's data type. Not all
        /// data types will get a str property.
        /// </remarks>
        /// <example>
        /// Set a datetime from a string.
        /// <code>
        /// Employees entity = new Employees();
        /// entity.LoadByPrimaryKey(10);
        /// entity.str.HireDate = "2007-01-01 00:00:00";
        /// entity.Save();
        /// </code>
        /// Get a datetime as a string.
        /// <code>
        /// Employees entity = new Employees();
        /// entity.LoadByPrimaryKey(10);
        /// string theDate = entity.str.HireDate;
        /// </code>
        /// </example>
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
            public esStrings(esTransChargesItemComp entity)
            {
                this.entity = entity;
            }
            public System.String TransactionNo
            {
                get
                {
                    System.String data = entity.TransactionNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TransactionNo = null;
                    else entity.TransactionNo = Convert.ToString(value);
                }
            }
            public System.String SequenceNo
            {
                get
                {
                    System.String data = entity.SequenceNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SequenceNo = null;
                    else entity.SequenceNo = Convert.ToString(value);
                }
            }
            public System.String TariffComponentID
            {
                get
                {
                    System.String data = entity.TariffComponentID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TariffComponentID = null;
                    else entity.TariffComponentID = Convert.ToString(value);
                }
            }
            public System.String Price
            {
                get
                {
                    System.Decimal? data = entity.Price;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Price = null;
                    else entity.Price = Convert.ToDecimal(value);
                }
            }
            public System.String DiscountAmount
            {
                get
                {
                    System.Decimal? data = entity.DiscountAmount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DiscountAmount = null;
                    else entity.DiscountAmount = Convert.ToDecimal(value);
                }
            }
            public System.String ParamedicID
            {
                get
                {
                    System.String data = entity.ParamedicID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ParamedicID = null;
                    else entity.ParamedicID = Convert.ToString(value);
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
            public System.String IsPackage
            {
                get
                {
                    System.Boolean? data = entity.IsPackage;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsPackage = null;
                    else entity.IsPackage = Convert.ToBoolean(value);
                }
            }
            public System.String AutoProcessCalculation
            {
                get
                {
                    System.Decimal? data = entity.AutoProcessCalculation;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.AutoProcessCalculation = null;
                    else entity.AutoProcessCalculation = Convert.ToDecimal(value);
                }
            }
            public System.String CitoAmount
            {
                get
                {
                    System.Decimal? data = entity.CitoAmount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CitoAmount = null;
                    else entity.CitoAmount = Convert.ToDecimal(value);
                }
            }
            public System.String FeeSettledNo
            {
                get
                {
                    System.String data = entity.FeeSettledNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FeeSettledNo = null;
                    else entity.FeeSettledNo = Convert.ToString(value);
                }
            }
            public System.String FeeCalculated
            {
                get
                {
                    System.Decimal? data = entity.FeeCalculated;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FeeCalculated = null;
                    else entity.FeeCalculated = Convert.ToDecimal(value);
                }
            }
            public System.String FeeDiscountPercentage
            {
                get
                {
                    System.Decimal? data = entity.FeeDiscountPercentage;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FeeDiscountPercentage = null;
                    else entity.FeeDiscountPercentage = Convert.ToDecimal(value);
                }
            }
            public System.String FeeDiscount
            {
                get
                {
                    System.Decimal? data = entity.FeeDiscount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FeeDiscount = null;
                    else entity.FeeDiscount = Convert.ToDecimal(value);
                }
            }
            public System.String PriceAdjusted
            {
                get
                {
                    System.Decimal? data = entity.PriceAdjusted;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PriceAdjusted = null;
                    else entity.PriceAdjusted = Convert.ToDecimal(value);
                }
            }
            private esTransChargesItemComp entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esTransChargesItemCompQuery query)
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
                throw new Exception("esTransChargesItemComp can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class TransChargesItemComp : esTransChargesItemComp
    {
    }

    [Serializable]
    abstract public class esTransChargesItemCompQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return TransChargesItemCompMetadata.Meta();
            }
        }

        public esQueryItem TransactionNo
        {
            get
            {
                return new esQueryItem(this, TransChargesItemCompMetadata.ColumnNames.TransactionNo, esSystemType.String);
            }
        }

        public esQueryItem SequenceNo
        {
            get
            {
                return new esQueryItem(this, TransChargesItemCompMetadata.ColumnNames.SequenceNo, esSystemType.String);
            }
        }

        public esQueryItem TariffComponentID
        {
            get
            {
                return new esQueryItem(this, TransChargesItemCompMetadata.ColumnNames.TariffComponentID, esSystemType.String);
            }
        }

        public esQueryItem Price
        {
            get
            {
                return new esQueryItem(this, TransChargesItemCompMetadata.ColumnNames.Price, esSystemType.Decimal);
            }
        }

        public esQueryItem DiscountAmount
        {
            get
            {
                return new esQueryItem(this, TransChargesItemCompMetadata.ColumnNames.DiscountAmount, esSystemType.Decimal);
            }
        }

        public esQueryItem ParamedicID
        {
            get
            {
                return new esQueryItem(this, TransChargesItemCompMetadata.ColumnNames.ParamedicID, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, TransChargesItemCompMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, TransChargesItemCompMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem IsPackage
        {
            get
            {
                return new esQueryItem(this, TransChargesItemCompMetadata.ColumnNames.IsPackage, esSystemType.Boolean);
            }
        }

        public esQueryItem AutoProcessCalculation
        {
            get
            {
                return new esQueryItem(this, TransChargesItemCompMetadata.ColumnNames.AutoProcessCalculation, esSystemType.Decimal);
            }
        }

        public esQueryItem CitoAmount
        {
            get
            {
                return new esQueryItem(this, TransChargesItemCompMetadata.ColumnNames.CitoAmount, esSystemType.Decimal);
            }
        }

        public esQueryItem FeeSettledNo
        {
            get
            {
                return new esQueryItem(this, TransChargesItemCompMetadata.ColumnNames.FeeSettledNo, esSystemType.String);
            }
        }

        public esQueryItem FeeCalculated
        {
            get
            {
                return new esQueryItem(this, TransChargesItemCompMetadata.ColumnNames.FeeCalculated, esSystemType.Decimal);
            }
        }

        public esQueryItem FeeDiscountPercentage
        {
            get
            {
                return new esQueryItem(this, TransChargesItemCompMetadata.ColumnNames.FeeDiscountPercentage, esSystemType.Decimal);
            }
        }

        public esQueryItem FeeDiscount
        {
            get
            {
                return new esQueryItem(this, TransChargesItemCompMetadata.ColumnNames.FeeDiscount, esSystemType.Decimal);
            }
        }

        public esQueryItem PriceAdjusted
        {
            get
            {
                return new esQueryItem(this, TransChargesItemCompMetadata.ColumnNames.PriceAdjusted, esSystemType.Decimal);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("TransChargesItemCompCollection")]
    public partial class TransChargesItemCompCollection : esTransChargesItemCompCollection, IEnumerable<TransChargesItemComp>
    {
        public TransChargesItemCompCollection()
        {

        }

        public static implicit operator List<TransChargesItemComp>(TransChargesItemCompCollection coll)
        {
            List<TransChargesItemComp> list = new List<TransChargesItemComp>();

            foreach (TransChargesItemComp emp in coll)
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
                return TransChargesItemCompMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new TransChargesItemCompQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new TransChargesItemComp(row);
        }

        override protected esEntity CreateEntity()
        {
            return new TransChargesItemComp();
        }

        #endregion

        [BrowsableAttribute(false)]
        public TransChargesItemCompQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new TransChargesItemCompQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        /// <summary>
        /// Useful for building up conditional queries.
        /// In most cases, before loading an entity or collection,
        /// you should instantiate a new one. This method was added
        /// to handle specialized circumstances, and should not be
        /// used as a substitute for that.
        /// </summary>
        /// <remarks>
        /// This just sets obj.Query to null/Nothing.
        /// In most cases, you will 'new' your object before
        /// loading it, rather than calling this method.
        /// It only affects obj.Query.Load(), so is not useful
        /// when Joins are involved, or for many other situations.
        /// Because it clears out any obj.Query.Where clauses,
        /// it can be useful for building conditional queries on the fly.
        /// <code>
        /// public bool ReQuery(string lastName, string firstName)
        /// {
        ///     this.QueryReset();
        ///     
        ///     if(!String.IsNullOrEmpty(lastName))
        ///     {
        ///         this.Query.Where(
        ///             this.Query.LastName == lastName);
        ///     }
        ///     if(!String.IsNullOrEmpty(firstName))
        ///     {
        ///         this.Query.Where(
        ///             this.Query.FirstName == firstName);
        ///     }
        ///     
        ///     return this.Query.Load();
        /// }
        /// </code>
        /// <code lang="vbnet">
        /// Public Function ReQuery(ByVal lastName As String, _
        ///     ByVal firstName As String) As Boolean
        /// 
        ///     Me.QueryReset()
        /// 
        ///     If Not [String].IsNullOrEmpty(lastName) Then
        ///         Me.Query.Where(Me.Query.LastName = lastName)
        ///     End If
        ///     If Not [String].IsNullOrEmpty(firstName) Then
        ///         Me.Query.Where(Me.Query.FirstName = firstName)
        ///     End If
        /// 
        ///     Return Me.Query.Load()
        /// End Function
        /// </code>
        /// </remarks>
        public void QueryReset()
        {
            this.query = null;
        }

        /// <summary>
        /// Used to custom load a Join query.
        /// Returns true if at least one record was loaded.
        /// </summary>
        /// <remarks>
        /// Provides support for InnerJoin, LeftJoin,
        /// RightJoin, and FullJoin. You must provide an alias
        /// for each query when instantiating them.
        /// <code>
        /// EmployeeCollection collection = new EmployeeCollection();
        /// 
        /// EmployeeQuery emp = new EmployeeQuery("eq");
        /// CustomerQuery cust = new CustomerQuery("cq");
        /// 
        /// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName);
        /// emp.LeftJoin(cust).On(emp.EmployeeID == cust.StaffAssigned);
        /// 
        /// collection.Load(emp);
        /// </code>
        /// <code lang="vbnet">
        /// Dim collection As New EmployeeCollection()
        /// 
        /// Dim emp As New EmployeeQuery("eq")
        /// Dim cust As New CustomerQuery("cq")
        /// 
        /// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName)
        /// emp.LeftJoin(cust).On(emp.EmployeeID = cust.StaffAssigned)
        /// 
        /// collection.Load(emp)
        /// </code>
        /// </remarks>
        /// <param name="query">The query object instance name.</param>
        /// <returns>True if at least one record was loaded.</returns>
        public bool Load(TransChargesItemCompQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public TransChargesItemComp AddNew()
        {
            TransChargesItemComp entity = base.AddNewEntity() as TransChargesItemComp;

            return entity;
        }
        public TransChargesItemComp FindByPrimaryKey(String transactionNo, String sequenceNo, String tariffComponentID)
        {
            return base.FindByPrimaryKey(transactionNo, sequenceNo, tariffComponentID) as TransChargesItemComp;
        }

        #region IEnumerable< TransChargesItemComp> Members

        IEnumerator<TransChargesItemComp> IEnumerable<TransChargesItemComp>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as TransChargesItemComp;
            }
        }

        #endregion

        private TransChargesItemCompQuery query;
    }


    /// <summary>
    /// Encapsulates the 'TransChargesItemComp' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("TransChargesItemComp ({TransactionNo, SequenceNo, TariffComponentID})")]
    [Serializable]
    public partial class TransChargesItemComp : esTransChargesItemComp
    {
        public TransChargesItemComp()
        {
        }

        public TransChargesItemComp(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return TransChargesItemCompMetadata.Meta();
            }
        }

        override protected esTransChargesItemCompQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new TransChargesItemCompQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public TransChargesItemCompQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new TransChargesItemCompQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        /// <summary>
        /// Useful for building up conditional queries.
        /// In most cases, before loading an entity or collection,
        /// you should instantiate a new one. This method was added
        /// to handle specialized circumstances, and should not be
        /// used as a substitute for that.
        /// </summary>
        /// <remarks>
        /// This just sets obj.Query to null/Nothing.
        /// In most cases, you will 'new' your object before
        /// loading it, rather than calling this method.
        /// It only affects obj.Query.Load(), so is not useful
        /// when Joins are involved, or for many other situations.
        /// Because it clears out any obj.Query.Where clauses,
        /// it can be useful for building conditional queries on the fly.
        /// <code>
        /// public bool ReQuery(string lastName, string firstName)
        /// {
        ///     this.QueryReset();
        ///     
        ///     if(!String.IsNullOrEmpty(lastName))
        ///     {
        ///         this.Query.Where(
        ///             this.Query.LastName == lastName);
        ///     }
        ///     if(!String.IsNullOrEmpty(firstName))
        ///     {
        ///         this.Query.Where(
        ///             this.Query.FirstName == firstName);
        ///     }
        ///     
        ///     return this.Query.Load();
        /// }
        /// </code>
        /// <code lang="vbnet">
        /// Public Function ReQuery(ByVal lastName As String, _
        ///     ByVal firstName As String) As Boolean
        /// 
        ///     Me.QueryReset()
        /// 
        ///     If Not [String].IsNullOrEmpty(lastName) Then
        ///         Me.Query.Where(Me.Query.LastName = lastName)
        ///     End If
        ///     If Not [String].IsNullOrEmpty(firstName) Then
        ///         Me.Query.Where(Me.Query.FirstName = firstName)
        ///     End If
        /// 
        ///     Return Me.Query.Load()
        /// End Function
        /// </code>
        /// </remarks>
        public void QueryReset()
        {
            this.query = null;
        }

        /// <summary>
        /// Used to custom load a Join query.
        /// Returns true if at least one row is loaded.
        /// For an entity, an exception will be thrown
        /// if more than one row is loaded.
        /// </summary>
        /// <remarks>
        /// Provides support for InnerJoin, LeftJoin,
        /// RightJoin, and FullJoin. You must provide an alias
        /// for each query when instantiating them.
        /// <code>
        /// EmployeeCollection collection = new EmployeeCollection();
        /// 
        /// EmployeeQuery emp = new EmployeeQuery("eq");
        /// CustomerQuery cust = new CustomerQuery("cq");
        /// 
        /// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName);
        /// emp.LeftJoin(cust).On(emp.EmployeeID == cust.StaffAssigned);
        /// 
        /// collection.Load(emp);
        /// </code>
        /// <code lang="vbnet">
        /// Dim collection As New EmployeeCollection()
        /// 
        /// Dim emp As New EmployeeQuery("eq")
        /// Dim cust As New CustomerQuery("cq")
        /// 
        /// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName)
        /// emp.LeftJoin(cust).On(emp.EmployeeID = cust.StaffAssigned)
        /// 
        /// collection.Load(emp)
        /// </code>
        /// </remarks>
        /// <param name="query">The query object instance name.</param>
        /// <returns>True if at least one record was loaded.</returns>
        public bool Load(TransChargesItemCompQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private TransChargesItemCompQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class TransChargesItemCompQuery : esTransChargesItemCompQuery
    {
        public TransChargesItemCompQuery()
        {

        }

        public TransChargesItemCompQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "TransChargesItemCompQuery";
        }
    }

    [Serializable]
    public partial class TransChargesItemCompMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected TransChargesItemCompMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(TransChargesItemCompMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = TransChargesItemCompMetadata.PropertyNames.TransactionNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemCompMetadata.ColumnNames.SequenceNo, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = TransChargesItemCompMetadata.PropertyNames.SequenceNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 7;
            c.HasDefault = true;
            c.Default = @"('000')";
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemCompMetadata.ColumnNames.TariffComponentID, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = TransChargesItemCompMetadata.PropertyNames.TariffComponentID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 2;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemCompMetadata.ColumnNames.Price, 3, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = TransChargesItemCompMetadata.PropertyNames.Price;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemCompMetadata.ColumnNames.DiscountAmount, 4, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = TransChargesItemCompMetadata.PropertyNames.DiscountAmount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemCompMetadata.ColumnNames.ParamedicID, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = TransChargesItemCompMetadata.PropertyNames.ParamedicID;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemCompMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = TransChargesItemCompMetadata.PropertyNames.LastUpdateDateTime;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemCompMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = TransChargesItemCompMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 40;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemCompMetadata.ColumnNames.IsPackage, 8, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = TransChargesItemCompMetadata.PropertyNames.IsPackage;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemCompMetadata.ColumnNames.AutoProcessCalculation, 9, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = TransChargesItemCompMetadata.PropertyNames.AutoProcessCalculation;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemCompMetadata.ColumnNames.CitoAmount, 10, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = TransChargesItemCompMetadata.PropertyNames.CitoAmount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemCompMetadata.ColumnNames.FeeSettledNo, 11, typeof(System.String), esSystemType.String);
            c.PropertyName = TransChargesItemCompMetadata.PropertyNames.FeeSettledNo;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemCompMetadata.ColumnNames.FeeCalculated, 12, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = TransChargesItemCompMetadata.PropertyNames.FeeCalculated;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemCompMetadata.ColumnNames.FeeDiscountPercentage, 13, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = TransChargesItemCompMetadata.PropertyNames.FeeDiscountPercentage;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemCompMetadata.ColumnNames.FeeDiscount, 14, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = TransChargesItemCompMetadata.PropertyNames.FeeDiscount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemCompMetadata.ColumnNames.PriceAdjusted, 15, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = TransChargesItemCompMetadata.PropertyNames.PriceAdjusted;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public TransChargesItemCompMetadata Meta()
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
            public const string TransactionNo = "TransactionNo";
            public const string SequenceNo = "SequenceNo";
            public const string TariffComponentID = "TariffComponentID";
            public const string Price = "Price";
            public const string DiscountAmount = "DiscountAmount";
            public const string ParamedicID = "ParamedicID";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string IsPackage = "IsPackage";
            public const string AutoProcessCalculation = "AutoProcessCalculation";
            public const string CitoAmount = "CitoAmount";
            public const string FeeSettledNo = "FeeSettledNo";
            public const string FeeCalculated = "FeeCalculated";
            public const string FeeDiscountPercentage = "FeeDiscountPercentage";
            public const string FeeDiscount = "FeeDiscount";
            public const string PriceAdjusted = "PriceAdjusted";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string TransactionNo = "TransactionNo";
            public const string SequenceNo = "SequenceNo";
            public const string TariffComponentID = "TariffComponentID";
            public const string Price = "Price";
            public const string DiscountAmount = "DiscountAmount";
            public const string ParamedicID = "ParamedicID";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string IsPackage = "IsPackage";
            public const string AutoProcessCalculation = "AutoProcessCalculation";
            public const string CitoAmount = "CitoAmount";
            public const string FeeSettledNo = "FeeSettledNo";
            public const string FeeCalculated = "FeeCalculated";
            public const string FeeDiscountPercentage = "FeeDiscountPercentage";
            public const string FeeDiscount = "FeeDiscount";
            public const string PriceAdjusted = "PriceAdjusted";
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
            lock (typeof(TransChargesItemCompMetadata))
            {
                if (TransChargesItemCompMetadata.mapDelegates == null)
                {
                    TransChargesItemCompMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (TransChargesItemCompMetadata.meta == null)
                {
                    TransChargesItemCompMetadata.meta = new TransChargesItemCompMetadata();
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

                meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SequenceNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("TariffComponentID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Price", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("DiscountAmount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsPackage", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("AutoProcessCalculation", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("CitoAmount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("FeeSettledNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("FeeCalculated", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("FeeDiscountPercentage", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("FeeDiscount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("PriceAdjusted", new esTypeMap("decimal", "System.Decimal"));


                meta.Source = "TransChargesItemComp";
                meta.Destination = "TransChargesItemComp";
                meta.spInsert = "proc_TransChargesItemCompInsert";
                meta.spUpdate = "proc_TransChargesItemCompUpdate";
                meta.spDelete = "proc_TransChargesItemCompDelete";
                meta.spLoadAll = "proc_TransChargesItemCompLoadAll";
                meta.spLoadByPrimaryKey = "proc_TransChargesItemCompLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private TransChargesItemCompMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
