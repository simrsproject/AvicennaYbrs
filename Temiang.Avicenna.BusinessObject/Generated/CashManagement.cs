/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 12/7/2018 11:42:08 AM
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
    abstract public class esCashManagementCollection : esEntityCollectionWAuditLog
    {
        public esCashManagementCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "CashManagementCollection";
        }

        #region Query Logic
        protected void InitQuery(esCashManagementQuery query)
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
            this.InitQuery(query as esCashManagementQuery);
        }
        #endregion

        virtual public CashManagement DetachEntity(CashManagement entity)
        {
            return base.DetachEntity(entity) as CashManagement;
        }

        virtual public CashManagement AttachEntity(CashManagement entity)
        {
            return base.AttachEntity(entity) as CashManagement;
        }

        virtual public void Combine(CashManagementCollection collection)
        {
            base.Combine(collection);
        }

        new public CashManagement this[int index]
        {
            get
            {
                return base[index] as CashManagement;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(CashManagement);
        }
    }

    [Serializable]
    abstract public class esCashManagement : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esCashManagementQuery GetDynamicQuery()
        {
            return null;
        }

        public esCashManagement()
        {
        }

        public esCashManagement(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String transactionNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(transactionNo);
            else
                return LoadByPrimaryKeyStoredProcedure(transactionNo);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String transactionNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(transactionNo);
            else
                return LoadByPrimaryKeyStoredProcedure(transactionNo);
        }

        private bool LoadByPrimaryKeyDynamic(String transactionNo)
        {
            esCashManagementQuery query = this.GetDynamicQuery();
            query.Where(query.TransactionNo == transactionNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String transactionNo)
        {
            esParameters parms = new esParameters();
            parms.Add("TransactionNo", transactionNo);
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
                        case "OpeningDate": this.str.OpeningDate = (string)value; break;
                        case "SRShift": this.str.SRShift = (string)value; break;
                        case "SRCashierCounter": this.str.SRCashierCounter = (string)value; break;
                        case "OpeningBalance": this.str.OpeningBalance = (string)value; break;
                        case "CashPayment": this.str.CashPayment = (string)value; break;
                        case "CashAmount": this.str.CashAmount = (string)value; break;
                        case "ClosingBalance": this.str.ClosingBalance = (string)value; break;
                        case "ClosingDate": this.str.ClosingDate = (string)value; break;
                        case "ClosingByUserID": this.str.ClosingByUserID = (string)value; break;
                        case "IsApproved": this.str.IsApproved = (string)value; break;
                        case "ApprovedByUserID": this.str.ApprovedByUserID = (string)value; break;
                        case "ApprovedDateTime": this.str.ApprovedDateTime = (string)value; break;
                        case "IsVoid": this.str.IsVoid = (string)value; break;
                        case "VoidByUserID": this.str.VoidByUserID = (string)value; break;
                        case "VoidDateTime": this.str.VoidDateTime = (string)value; break;
                        case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;
                        case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "OpeningDate":

                            if (value == null || value is System.DateTime)
                                this.OpeningDate = (System.DateTime?)value;
                            break;
                        case "OpeningBalance":

                            if (value == null || value is System.Decimal)
                                this.OpeningBalance = (System.Decimal?)value;
                            break;
                        case "CashPayment":

                            if (value == null || value is System.Decimal)
                                this.CashPayment = (System.Decimal?)value;
                            break;
                        case "CashAmount":

                            if (value == null || value is System.Decimal)
                                this.CashAmount = (System.Decimal?)value;
                            break;
                        case "ClosingBalance":

                            if (value == null || value is System.Decimal)
                                this.ClosingBalance = (System.Decimal?)value;
                            break;
                        case "ClosingDate":

                            if (value == null || value is System.DateTime)
                                this.ClosingDate = (System.DateTime?)value;
                            break;
                        case "IsApproved":

                            if (value == null || value is System.Boolean)
                                this.IsApproved = (System.Boolean?)value;
                            break;
                        case "ApprovedDateTime":

                            if (value == null || value is System.DateTime)
                                this.ApprovedDateTime = (System.DateTime?)value;
                            break;
                        case "IsVoid":

                            if (value == null || value is System.Boolean)
                                this.IsVoid = (System.Boolean?)value;
                            break;
                        case "VoidDateTime":

                            if (value == null || value is System.DateTime)
                                this.VoidDateTime = (System.DateTime?)value;
                            break;
                        case "CreatedDateTime":

                            if (value == null || value is System.DateTime)
                                this.CreatedDateTime = (System.DateTime?)value;
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
        /// Maps to CashManagement.TransactionNo
        /// </summary>
        virtual public System.String TransactionNo
        {
            get
            {
                return base.GetSystemString(CashManagementMetadata.ColumnNames.TransactionNo);
            }

            set
            {
                base.SetSystemString(CashManagementMetadata.ColumnNames.TransactionNo, value);
            }
        }
        /// <summary>
        /// Maps to CashManagement.OpeningDate
        /// </summary>
        virtual public System.DateTime? OpeningDate
        {
            get
            {
                return base.GetSystemDateTime(CashManagementMetadata.ColumnNames.OpeningDate);
            }

            set
            {
                base.SetSystemDateTime(CashManagementMetadata.ColumnNames.OpeningDate, value);
            }
        }
        /// <summary>
        /// Maps to CashManagement.SRShift
        /// </summary>
        virtual public System.String SRShift
        {
            get
            {
                return base.GetSystemString(CashManagementMetadata.ColumnNames.SRShift);
            }

            set
            {
                base.SetSystemString(CashManagementMetadata.ColumnNames.SRShift, value);
            }
        }
        /// <summary>
        /// Maps to CashManagement.SRCashierCounter
        /// </summary>
        virtual public System.String SRCashierCounter
        {
            get
            {
                return base.GetSystemString(CashManagementMetadata.ColumnNames.SRCashierCounter);
            }

            set
            {
                base.SetSystemString(CashManagementMetadata.ColumnNames.SRCashierCounter, value);
            }
        }
        /// <summary>
        /// Maps to CashManagement.OpeningBalance
        /// </summary>
        virtual public System.Decimal? OpeningBalance
        {
            get
            {
                return base.GetSystemDecimal(CashManagementMetadata.ColumnNames.OpeningBalance);
            }

            set
            {
                base.SetSystemDecimal(CashManagementMetadata.ColumnNames.OpeningBalance, value);
            }
        }
        /// <summary>
        /// Maps to CashManagement.CashPayment
        /// </summary>
        virtual public System.Decimal? CashPayment
        {
            get
            {
                return base.GetSystemDecimal(CashManagementMetadata.ColumnNames.CashPayment);
            }

            set
            {
                base.SetSystemDecimal(CashManagementMetadata.ColumnNames.CashPayment, value);
            }
        }
        /// <summary>
        /// Maps to CashManagement.CashAmount
        /// </summary>
        virtual public System.Decimal? CashAmount
        {
            get
            {
                return base.GetSystemDecimal(CashManagementMetadata.ColumnNames.CashAmount);
            }

            set
            {
                base.SetSystemDecimal(CashManagementMetadata.ColumnNames.CashAmount, value);
            }
        }
        /// <summary>
        /// Maps to CashManagement.ClosingBalance
        /// </summary>
        virtual public System.Decimal? ClosingBalance
        {
            get
            {
                return base.GetSystemDecimal(CashManagementMetadata.ColumnNames.ClosingBalance);
            }

            set
            {
                base.SetSystemDecimal(CashManagementMetadata.ColumnNames.ClosingBalance, value);
            }
        }
        /// <summary>
        /// Maps to CashManagement.ClosingDate
        /// </summary>
        virtual public System.DateTime? ClosingDate
        {
            get
            {
                return base.GetSystemDateTime(CashManagementMetadata.ColumnNames.ClosingDate);
            }

            set
            {
                base.SetSystemDateTime(CashManagementMetadata.ColumnNames.ClosingDate, value);
            }
        }
        /// <summary>
        /// Maps to CashManagement.ClosingByUserID
        /// </summary>
        virtual public System.String ClosingByUserID
        {
            get
            {
                return base.GetSystemString(CashManagementMetadata.ColumnNames.ClosingByUserID);
            }

            set
            {
                base.SetSystemString(CashManagementMetadata.ColumnNames.ClosingByUserID, value);
            }
        }
        /// <summary>
        /// Maps to CashManagement.IsApproved
        /// </summary>
        virtual public System.Boolean? IsApproved
        {
            get
            {
                return base.GetSystemBoolean(CashManagementMetadata.ColumnNames.IsApproved);
            }

            set
            {
                base.SetSystemBoolean(CashManagementMetadata.ColumnNames.IsApproved, value);
            }
        }
        /// <summary>
        /// Maps to CashManagement.ApprovedByUserID
        /// </summary>
        virtual public System.String ApprovedByUserID
        {
            get
            {
                return base.GetSystemString(CashManagementMetadata.ColumnNames.ApprovedByUserID);
            }

            set
            {
                base.SetSystemString(CashManagementMetadata.ColumnNames.ApprovedByUserID, value);
            }
        }
        /// <summary>
        /// Maps to CashManagement.ApprovedDateTime
        /// </summary>
        virtual public System.DateTime? ApprovedDateTime
        {
            get
            {
                return base.GetSystemDateTime(CashManagementMetadata.ColumnNames.ApprovedDateTime);
            }

            set
            {
                base.SetSystemDateTime(CashManagementMetadata.ColumnNames.ApprovedDateTime, value);
            }
        }
        /// <summary>
        /// Maps to CashManagement.IsVoid
        /// </summary>
        virtual public System.Boolean? IsVoid
        {
            get
            {
                return base.GetSystemBoolean(CashManagementMetadata.ColumnNames.IsVoid);
            }

            set
            {
                base.SetSystemBoolean(CashManagementMetadata.ColumnNames.IsVoid, value);
            }
        }
        /// <summary>
        /// Maps to CashManagement.VoidByUserID
        /// </summary>
        virtual public System.String VoidByUserID
        {
            get
            {
                return base.GetSystemString(CashManagementMetadata.ColumnNames.VoidByUserID);
            }

            set
            {
                base.SetSystemString(CashManagementMetadata.ColumnNames.VoidByUserID, value);
            }
        }
        /// <summary>
        /// Maps to CashManagement.VoidDateTime
        /// </summary>
        virtual public System.DateTime? VoidDateTime
        {
            get
            {
                return base.GetSystemDateTime(CashManagementMetadata.ColumnNames.VoidDateTime);
            }

            set
            {
                base.SetSystemDateTime(CashManagementMetadata.ColumnNames.VoidDateTime, value);
            }
        }
        /// <summary>
        /// Maps to CashManagement.CreatedDateTime
        /// </summary>
        virtual public System.DateTime? CreatedDateTime
        {
            get
            {
                return base.GetSystemDateTime(CashManagementMetadata.ColumnNames.CreatedDateTime);
            }

            set
            {
                base.SetSystemDateTime(CashManagementMetadata.ColumnNames.CreatedDateTime, value);
            }
        }
        /// <summary>
        /// Maps to CashManagement.CreatedByUserID
        /// </summary>
        virtual public System.String CreatedByUserID
        {
            get
            {
                return base.GetSystemString(CashManagementMetadata.ColumnNames.CreatedByUserID);
            }

            set
            {
                base.SetSystemString(CashManagementMetadata.ColumnNames.CreatedByUserID, value);
            }
        }
        /// <summary>
        /// Maps to CashManagement.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(CashManagementMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(CashManagementMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to CashManagement.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(CashManagementMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(CashManagementMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esCashManagement entity)
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
            public System.String OpeningDate
            {
                get
                {
                    System.DateTime? data = entity.OpeningDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.OpeningDate = null;
                    else entity.OpeningDate = Convert.ToDateTime(value);
                }
            }
            public System.String SRShift
            {
                get
                {
                    System.String data = entity.SRShift;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRShift = null;
                    else entity.SRShift = Convert.ToString(value);
                }
            }
            public System.String SRCashierCounter
            {
                get
                {
                    System.String data = entity.SRCashierCounter;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRCashierCounter = null;
                    else entity.SRCashierCounter = Convert.ToString(value);
                }
            }
            public System.String OpeningBalance
            {
                get
                {
                    System.Decimal? data = entity.OpeningBalance;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.OpeningBalance = null;
                    else entity.OpeningBalance = Convert.ToDecimal(value);
                }
            }
            public System.String CashPayment
            {
                get
                {
                    System.Decimal? data = entity.CashPayment;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CashPayment = null;
                    else entity.CashPayment = Convert.ToDecimal(value);
                }
            }
            public System.String CashAmount
            {
                get
                {
                    System.Decimal? data = entity.CashAmount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CashAmount = null;
                    else entity.CashAmount = Convert.ToDecimal(value);
                }
            }
            public System.String ClosingBalance
            {
                get
                {
                    System.Decimal? data = entity.ClosingBalance;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ClosingBalance = null;
                    else entity.ClosingBalance = Convert.ToDecimal(value);
                }
            }
            public System.String ClosingDate
            {
                get
                {
                    System.DateTime? data = entity.ClosingDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ClosingDate = null;
                    else entity.ClosingDate = Convert.ToDateTime(value);
                }
            }
            public System.String ClosingByUserID
            {
                get
                {
                    System.String data = entity.ClosingByUserID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ClosingByUserID = null;
                    else entity.ClosingByUserID = Convert.ToString(value);
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
            public System.String ApprovedByUserID
            {
                get
                {
                    System.String data = entity.ApprovedByUserID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ApprovedByUserID = null;
                    else entity.ApprovedByUserID = Convert.ToString(value);
                }
            }
            public System.String ApprovedDateTime
            {
                get
                {
                    System.DateTime? data = entity.ApprovedDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ApprovedDateTime = null;
                    else entity.ApprovedDateTime = Convert.ToDateTime(value);
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
            public System.String CreatedDateTime
            {
                get
                {
                    System.DateTime? data = entity.CreatedDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CreatedDateTime = null;
                    else entity.CreatedDateTime = Convert.ToDateTime(value);
                }
            }
            public System.String CreatedByUserID
            {
                get
                {
                    System.String data = entity.CreatedByUserID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CreatedByUserID = null;
                    else entity.CreatedByUserID = Convert.ToString(value);
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
            private esCashManagement entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esCashManagementQuery query)
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
                throw new Exception("esCashManagement can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class CashManagement : esCashManagement
    {
    }

    [Serializable]
    abstract public class esCashManagementQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return CashManagementMetadata.Meta();
            }
        }

        public esQueryItem TransactionNo
        {
            get
            {
                return new esQueryItem(this, CashManagementMetadata.ColumnNames.TransactionNo, esSystemType.String);
            }
        }

        public esQueryItem OpeningDate
        {
            get
            {
                return new esQueryItem(this, CashManagementMetadata.ColumnNames.OpeningDate, esSystemType.DateTime);
            }
        }

        public esQueryItem SRShift
        {
            get
            {
                return new esQueryItem(this, CashManagementMetadata.ColumnNames.SRShift, esSystemType.String);
            }
        }

        public esQueryItem SRCashierCounter
        {
            get
            {
                return new esQueryItem(this, CashManagementMetadata.ColumnNames.SRCashierCounter, esSystemType.String);
            }
        }

        public esQueryItem OpeningBalance
        {
            get
            {
                return new esQueryItem(this, CashManagementMetadata.ColumnNames.OpeningBalance, esSystemType.Decimal);
            }
        }

        public esQueryItem CashPayment
        {
            get
            {
                return new esQueryItem(this, CashManagementMetadata.ColumnNames.CashPayment, esSystemType.Decimal);
            }
        }

        public esQueryItem CashAmount
        {
            get
            {
                return new esQueryItem(this, CashManagementMetadata.ColumnNames.CashAmount, esSystemType.Decimal);
            }
        }

        public esQueryItem ClosingBalance
        {
            get
            {
                return new esQueryItem(this, CashManagementMetadata.ColumnNames.ClosingBalance, esSystemType.Decimal);
            }
        }

        public esQueryItem ClosingDate
        {
            get
            {
                return new esQueryItem(this, CashManagementMetadata.ColumnNames.ClosingDate, esSystemType.DateTime);
            }
        }

        public esQueryItem ClosingByUserID
        {
            get
            {
                return new esQueryItem(this, CashManagementMetadata.ColumnNames.ClosingByUserID, esSystemType.String);
            }
        }

        public esQueryItem IsApproved
        {
            get
            {
                return new esQueryItem(this, CashManagementMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
            }
        }

        public esQueryItem ApprovedByUserID
        {
            get
            {
                return new esQueryItem(this, CashManagementMetadata.ColumnNames.ApprovedByUserID, esSystemType.String);
            }
        }

        public esQueryItem ApprovedDateTime
        {
            get
            {
                return new esQueryItem(this, CashManagementMetadata.ColumnNames.ApprovedDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem IsVoid
        {
            get
            {
                return new esQueryItem(this, CashManagementMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
            }
        }

        public esQueryItem VoidByUserID
        {
            get
            {
                return new esQueryItem(this, CashManagementMetadata.ColumnNames.VoidByUserID, esSystemType.String);
            }
        }

        public esQueryItem VoidDateTime
        {
            get
            {
                return new esQueryItem(this, CashManagementMetadata.ColumnNames.VoidDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem CreatedDateTime
        {
            get
            {
                return new esQueryItem(this, CashManagementMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem CreatedByUserID
        {
            get
            {
                return new esQueryItem(this, CashManagementMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, CashManagementMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, CashManagementMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("CashManagementCollection")]
    public partial class CashManagementCollection : esCashManagementCollection, IEnumerable<CashManagement>
    {
        public CashManagementCollection()
        {

        }

        public static implicit operator List<CashManagement>(CashManagementCollection coll)
        {
            List<CashManagement> list = new List<CashManagement>();

            foreach (CashManagement emp in coll)
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
                return CashManagementMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new CashManagementQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new CashManagement(row);
        }

        override protected esEntity CreateEntity()
        {
            return new CashManagement();
        }

        #endregion

        [BrowsableAttribute(false)]
        public CashManagementQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new CashManagementQuery();
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
        public bool Load(CashManagementQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public CashManagement AddNew()
        {
            CashManagement entity = base.AddNewEntity() as CashManagement;

            return entity;
        }
        public CashManagement FindByPrimaryKey(String transactionNo)
        {
            return base.FindByPrimaryKey(transactionNo) as CashManagement;
        }

        #region IEnumerable< CashManagement> Members

        IEnumerator<CashManagement> IEnumerable<CashManagement>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as CashManagement;
            }
        }

        #endregion

        private CashManagementQuery query;
    }


    /// <summary>
    /// Encapsulates the 'CashManagement' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("CashManagement ({TransactionNo})")]
    [Serializable]
    public partial class CashManagement : esCashManagement
    {
        public CashManagement()
        {
        }

        public CashManagement(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return CashManagementMetadata.Meta();
            }
        }

        override protected esCashManagementQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new CashManagementQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public CashManagementQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new CashManagementQuery();
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
        public bool Load(CashManagementQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private CashManagementQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class CashManagementQuery : esCashManagementQuery
    {
        public CashManagementQuery()
        {

        }

        public CashManagementQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "CashManagementQuery";
        }
    }

    [Serializable]
    public partial class CashManagementMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected CashManagementMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(CashManagementMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = CashManagementMetadata.PropertyNames.TransactionNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(CashManagementMetadata.ColumnNames.OpeningDate, 1, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = CashManagementMetadata.PropertyNames.OpeningDate;
            _columns.Add(c);

            c = new esColumnMetadata(CashManagementMetadata.ColumnNames.SRShift, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = CashManagementMetadata.PropertyNames.SRShift;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(CashManagementMetadata.ColumnNames.SRCashierCounter, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = CashManagementMetadata.PropertyNames.SRCashierCounter;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(CashManagementMetadata.ColumnNames.OpeningBalance, 4, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = CashManagementMetadata.PropertyNames.OpeningBalance;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(CashManagementMetadata.ColumnNames.CashPayment, 5, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = CashManagementMetadata.PropertyNames.CashPayment;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(CashManagementMetadata.ColumnNames.CashAmount, 6, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = CashManagementMetadata.PropertyNames.CashAmount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(CashManagementMetadata.ColumnNames.ClosingBalance, 7, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = CashManagementMetadata.PropertyNames.ClosingBalance;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(CashManagementMetadata.ColumnNames.ClosingDate, 8, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = CashManagementMetadata.PropertyNames.ClosingDate;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(CashManagementMetadata.ColumnNames.ClosingByUserID, 9, typeof(System.String), esSystemType.String);
            c.PropertyName = CashManagementMetadata.PropertyNames.ClosingByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(CashManagementMetadata.ColumnNames.IsApproved, 10, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = CashManagementMetadata.PropertyNames.IsApproved;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(CashManagementMetadata.ColumnNames.ApprovedByUserID, 11, typeof(System.String), esSystemType.String);
            c.PropertyName = CashManagementMetadata.PropertyNames.ApprovedByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(CashManagementMetadata.ColumnNames.ApprovedDateTime, 12, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = CashManagementMetadata.PropertyNames.ApprovedDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(CashManagementMetadata.ColumnNames.IsVoid, 13, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = CashManagementMetadata.PropertyNames.IsVoid;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(CashManagementMetadata.ColumnNames.VoidByUserID, 14, typeof(System.String), esSystemType.String);
            c.PropertyName = CashManagementMetadata.PropertyNames.VoidByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(CashManagementMetadata.ColumnNames.VoidDateTime, 15, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = CashManagementMetadata.PropertyNames.VoidDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(CashManagementMetadata.ColumnNames.CreatedDateTime, 16, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = CashManagementMetadata.PropertyNames.CreatedDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(CashManagementMetadata.ColumnNames.CreatedByUserID, 17, typeof(System.String), esSystemType.String);
            c.PropertyName = CashManagementMetadata.PropertyNames.CreatedByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(CashManagementMetadata.ColumnNames.LastUpdateDateTime, 18, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = CashManagementMetadata.PropertyNames.LastUpdateDateTime;
            _columns.Add(c);

            c = new esColumnMetadata(CashManagementMetadata.ColumnNames.LastUpdateByUserID, 19, typeof(System.String), esSystemType.String);
            c.PropertyName = CashManagementMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public CashManagementMetadata Meta()
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
            public const string OpeningDate = "OpeningDate";
            public const string SRShift = "SRShift";
            public const string SRCashierCounter = "SRCashierCounter";
            public const string OpeningBalance = "OpeningBalance";
            public const string CashPayment = "CashPayment";
            public const string CashAmount = "CashAmount";
            public const string ClosingBalance = "ClosingBalance";
            public const string ClosingDate = "ClosingDate";
            public const string ClosingByUserID = "ClosingByUserID";
            public const string IsApproved = "IsApproved";
            public const string ApprovedByUserID = "ApprovedByUserID";
            public const string ApprovedDateTime = "ApprovedDateTime";
            public const string IsVoid = "IsVoid";
            public const string VoidByUserID = "VoidByUserID";
            public const string VoidDateTime = "VoidDateTime";
            public const string CreatedDateTime = "CreatedDateTime";
            public const string CreatedByUserID = "CreatedByUserID";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string TransactionNo = "TransactionNo";
            public const string OpeningDate = "OpeningDate";
            public const string SRShift = "SRShift";
            public const string SRCashierCounter = "SRCashierCounter";
            public const string OpeningBalance = "OpeningBalance";
            public const string CashPayment = "CashPayment";
            public const string CashAmount = "CashAmount";
            public const string ClosingBalance = "ClosingBalance";
            public const string ClosingDate = "ClosingDate";
            public const string ClosingByUserID = "ClosingByUserID";
            public const string IsApproved = "IsApproved";
            public const string ApprovedByUserID = "ApprovedByUserID";
            public const string ApprovedDateTime = "ApprovedDateTime";
            public const string IsVoid = "IsVoid";
            public const string VoidByUserID = "VoidByUserID";
            public const string VoidDateTime = "VoidDateTime";
            public const string CreatedDateTime = "CreatedDateTime";
            public const string CreatedByUserID = "CreatedByUserID";
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
            lock (typeof(CashManagementMetadata))
            {
                if (CashManagementMetadata.mapDelegates == null)
                {
                    CashManagementMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (CashManagementMetadata.meta == null)
                {
                    CashManagementMetadata.meta = new CashManagementMetadata();
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
                meta.AddTypeMap("OpeningDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("SRShift", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRCashierCounter", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("OpeningBalance", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("CashPayment", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("CashAmount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("ClosingBalance", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("ClosingDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("ClosingByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("ApprovedByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ApprovedDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("VoidByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("VoidDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "CashManagement";
                meta.Destination = "CashManagement";
                meta.spInsert = "proc_CashManagementInsert";
                meta.spUpdate = "proc_CashManagementUpdate";
                meta.spDelete = "proc_CashManagementDelete";
                meta.spLoadAll = "proc_CashManagementLoadAll";
                meta.spLoadByPrimaryKey = "proc_CashManagementLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private CashManagementMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
