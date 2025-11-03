/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 7/26/2023 1:03:37 PM
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
    abstract public class esTransPaymentItemVisiteCollection : esEntityCollectionWAuditLog
    {
        public esTransPaymentItemVisiteCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "TransPaymentItemVisiteCollection";
        }

        #region Query Logic
        protected void InitQuery(esTransPaymentItemVisiteQuery query)
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
            this.InitQuery(query as esTransPaymentItemVisiteQuery);
        }
        #endregion

        virtual public TransPaymentItemVisite DetachEntity(TransPaymentItemVisite entity)
        {
            return base.DetachEntity(entity) as TransPaymentItemVisite;
        }

        virtual public TransPaymentItemVisite AttachEntity(TransPaymentItemVisite entity)
        {
            return base.AttachEntity(entity) as TransPaymentItemVisite;
        }

        virtual public void Combine(TransPaymentItemVisiteCollection collection)
        {
            base.Combine(collection);
        }

        new public TransPaymentItemVisite this[int index]
        {
            get
            {
                return base[index] as TransPaymentItemVisite;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(TransPaymentItemVisite);
        }
    }

    [Serializable]
    abstract public class esTransPaymentItemVisite : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esTransPaymentItemVisiteQuery GetDynamicQuery()
        {
            return null;
        }

        public esTransPaymentItemVisite()
        {
        }

        public esTransPaymentItemVisite(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String paymentNo, String patientID, String itemID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(paymentNo, patientID, itemID);
            else
                return LoadByPrimaryKeyStoredProcedure(paymentNo, patientID, itemID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String paymentNo, String patientID, String itemID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(paymentNo, patientID, itemID);
            else
                return LoadByPrimaryKeyStoredProcedure(paymentNo, patientID, itemID);
        }

        private bool LoadByPrimaryKeyDynamic(String paymentNo, String patientID, String itemID)
        {
            esTransPaymentItemVisiteQuery query = this.GetDynamicQuery();
            query.Where(query.PaymentNo == paymentNo, query.PatientID == patientID, query.ItemID == itemID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String paymentNo, String patientID, String itemID)
        {
            esParameters parms = new esParameters();
            parms.Add("PaymentNo", paymentNo);
            parms.Add("PatientID", patientID);
            parms.Add("ItemID", itemID);
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
                        case "PaymentNo": this.str.PaymentNo = (string)value; break;
                        case "PatientID": this.str.PatientID = (string)value; break;
                        case "ItemID": this.str.ItemID = (string)value; break;
                        case "VisiteQty": this.str.VisiteQty = (string)value; break;
                        case "RealizationQty": this.str.RealizationQty = (string)value; break;
                        case "Price": this.str.Price = (string)value; break;
                        case "Discount": this.str.Discount = (string)value; break;
                        case "IsClosed": this.str.IsClosed = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
                        case "ExpiredDate": this.str.ExpiredDate = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "VisiteQty":

                            if (value == null || value is System.Int32)
                                this.VisiteQty = (System.Int32?)value;
                            break;
                        case "RealizationQty":

                            if (value == null || value is System.Int32)
                                this.RealizationQty = (System.Int32?)value;
                            break;
                        case "Price":

                            if (value == null || value is System.Decimal)
                                this.Price = (System.Decimal?)value;
                            break;
                        case "Discount":

                            if (value == null || value is System.Decimal)
                                this.Discount = (System.Decimal?)value;
                            break;
                        case "IsClosed":

                            if (value == null || value is System.Boolean)
                                this.IsClosed = (System.Boolean?)value;
                            break;
                        case "LastUpdateDateTime":

                            if (value == null || value is System.DateTime)
                                this.LastUpdateDateTime = (System.DateTime?)value;
                            break;
                        case "ExpiredDate":

                            if (value == null || value is System.DateTime)
                                this.ExpiredDate = (System.DateTime?)value;
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
        /// Maps to TransPaymentItemVisite.PaymentNo
        /// </summary>
        virtual public System.String PaymentNo
        {
            get
            {
                return base.GetSystemString(TransPaymentItemVisiteMetadata.ColumnNames.PaymentNo);
            }

            set
            {
                base.SetSystemString(TransPaymentItemVisiteMetadata.ColumnNames.PaymentNo, value);
            }
        }
        /// <summary>
        /// Maps to TransPaymentItemVisite.PatientID
        /// </summary>
        virtual public System.String PatientID
        {
            get
            {
                return base.GetSystemString(TransPaymentItemVisiteMetadata.ColumnNames.PatientID);
            }

            set
            {
                base.SetSystemString(TransPaymentItemVisiteMetadata.ColumnNames.PatientID, value);
            }
        }
        /// <summary>
        /// Maps to TransPaymentItemVisite.ItemID
        /// </summary>
        virtual public System.String ItemID
        {
            get
            {
                return base.GetSystemString(TransPaymentItemVisiteMetadata.ColumnNames.ItemID);
            }

            set
            {
                base.SetSystemString(TransPaymentItemVisiteMetadata.ColumnNames.ItemID, value);
            }
        }
        /// <summary>
        /// Maps to TransPaymentItemVisite.VisiteQty
        /// </summary>
        virtual public System.Int32? VisiteQty
        {
            get
            {
                return base.GetSystemInt32(TransPaymentItemVisiteMetadata.ColumnNames.VisiteQty);
            }

            set
            {
                base.SetSystemInt32(TransPaymentItemVisiteMetadata.ColumnNames.VisiteQty, value);
            }
        }
        /// <summary>
        /// Maps to TransPaymentItemVisite.RealizationQty
        /// </summary>
        virtual public System.Int32? RealizationQty
        {
            get
            {
                return base.GetSystemInt32(TransPaymentItemVisiteMetadata.ColumnNames.RealizationQty);
            }

            set
            {
                base.SetSystemInt32(TransPaymentItemVisiteMetadata.ColumnNames.RealizationQty, value);
            }
        }
        /// <summary>
        /// Maps to TransPaymentItemVisite.Price
        /// </summary>
        virtual public System.Decimal? Price
        {
            get
            {
                return base.GetSystemDecimal(TransPaymentItemVisiteMetadata.ColumnNames.Price);
            }

            set
            {
                base.SetSystemDecimal(TransPaymentItemVisiteMetadata.ColumnNames.Price, value);
            }
        }
        /// <summary>
        /// Maps to TransPaymentItemVisite.Discount
        /// </summary>
        virtual public System.Decimal? Discount
        {
            get
            {
                return base.GetSystemDecimal(TransPaymentItemVisiteMetadata.ColumnNames.Discount);
            }

            set
            {
                base.SetSystemDecimal(TransPaymentItemVisiteMetadata.ColumnNames.Discount, value);
            }
        }
        /// <summary>
        /// Maps to TransPaymentItemVisite.IsClosed
        /// </summary>
        virtual public System.Boolean? IsClosed
        {
            get
            {
                return base.GetSystemBoolean(TransPaymentItemVisiteMetadata.ColumnNames.IsClosed);
            }

            set
            {
                base.SetSystemBoolean(TransPaymentItemVisiteMetadata.ColumnNames.IsClosed, value);
            }
        }
        /// <summary>
        /// Maps to TransPaymentItemVisite.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(TransPaymentItemVisiteMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(TransPaymentItemVisiteMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to TransPaymentItemVisite.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(TransPaymentItemVisiteMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(TransPaymentItemVisiteMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to TransPaymentItemVisite.ServiceUnitID
        /// </summary>
        virtual public System.String ServiceUnitID
        {
            get
            {
                return base.GetSystemString(TransPaymentItemVisiteMetadata.ColumnNames.ServiceUnitID);
            }

            set
            {
                base.SetSystemString(TransPaymentItemVisiteMetadata.ColumnNames.ServiceUnitID, value);
            }
        }
        /// <summary>
        /// Maps to TransPaymentItemVisite.ExpiredDate
        /// </summary>
        virtual public System.DateTime? ExpiredDate
        {
            get
            {
                return base.GetSystemDateTime(TransPaymentItemVisiteMetadata.ColumnNames.ExpiredDate);
            }

            set
            {
                base.SetSystemDateTime(TransPaymentItemVisiteMetadata.ColumnNames.ExpiredDate, value);
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
            public esStrings(esTransPaymentItemVisite entity)
            {
                this.entity = entity;
            }
            public System.String PaymentNo
            {
                get
                {
                    System.String data = entity.PaymentNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PaymentNo = null;
                    else entity.PaymentNo = Convert.ToString(value);
                }
            }
            public System.String PatientID
            {
                get
                {
                    System.String data = entity.PatientID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PatientID = null;
                    else entity.PatientID = Convert.ToString(value);
                }
            }
            public System.String ItemID
            {
                get
                {
                    System.String data = entity.ItemID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ItemID = null;
                    else entity.ItemID = Convert.ToString(value);
                }
            }
            public System.String VisiteQty
            {
                get
                {
                    System.Int32? data = entity.VisiteQty;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.VisiteQty = null;
                    else entity.VisiteQty = Convert.ToInt32(value);
                }
            }
            public System.String RealizationQty
            {
                get
                {
                    System.Int32? data = entity.RealizationQty;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RealizationQty = null;
                    else entity.RealizationQty = Convert.ToInt32(value);
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
            public System.String Discount
            {
                get
                {
                    System.Decimal? data = entity.Discount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Discount = null;
                    else entity.Discount = Convert.ToDecimal(value);
                }
            }
            public System.String IsClosed
            {
                get
                {
                    System.Boolean? data = entity.IsClosed;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsClosed = null;
                    else entity.IsClosed = Convert.ToBoolean(value);
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
            public System.String ExpiredDate
            {
                get
                {
                    System.DateTime? data = entity.ExpiredDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ExpiredDate = null;
                    else entity.ExpiredDate = Convert.ToDateTime(value);
                }
            }
            private esTransPaymentItemVisite entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esTransPaymentItemVisiteQuery query)
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
                throw new Exception("esTransPaymentItemVisite can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class TransPaymentItemVisite : esTransPaymentItemVisite
    {
    }

    [Serializable]
    abstract public class esTransPaymentItemVisiteQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return TransPaymentItemVisiteMetadata.Meta();
            }
        }

        public esQueryItem PaymentNo
        {
            get
            {
                return new esQueryItem(this, TransPaymentItemVisiteMetadata.ColumnNames.PaymentNo, esSystemType.String);
            }
        }

        public esQueryItem PatientID
        {
            get
            {
                return new esQueryItem(this, TransPaymentItemVisiteMetadata.ColumnNames.PatientID, esSystemType.String);
            }
        }

        public esQueryItem ItemID
        {
            get
            {
                return new esQueryItem(this, TransPaymentItemVisiteMetadata.ColumnNames.ItemID, esSystemType.String);
            }
        }

        public esQueryItem VisiteQty
        {
            get
            {
                return new esQueryItem(this, TransPaymentItemVisiteMetadata.ColumnNames.VisiteQty, esSystemType.Int32);
            }
        }

        public esQueryItem RealizationQty
        {
            get
            {
                return new esQueryItem(this, TransPaymentItemVisiteMetadata.ColumnNames.RealizationQty, esSystemType.Int32);
            }
        }

        public esQueryItem Price
        {
            get
            {
                return new esQueryItem(this, TransPaymentItemVisiteMetadata.ColumnNames.Price, esSystemType.Decimal);
            }
        }

        public esQueryItem Discount
        {
            get
            {
                return new esQueryItem(this, TransPaymentItemVisiteMetadata.ColumnNames.Discount, esSystemType.Decimal);
            }
        }

        public esQueryItem IsClosed
        {
            get
            {
                return new esQueryItem(this, TransPaymentItemVisiteMetadata.ColumnNames.IsClosed, esSystemType.Boolean);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, TransPaymentItemVisiteMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, TransPaymentItemVisiteMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem ServiceUnitID
        {
            get
            {
                return new esQueryItem(this, TransPaymentItemVisiteMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
            }
        }

        public esQueryItem ExpiredDate
        {
            get
            {
                return new esQueryItem(this, TransPaymentItemVisiteMetadata.ColumnNames.ExpiredDate, esSystemType.DateTime);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("TransPaymentItemVisiteCollection")]
    public partial class TransPaymentItemVisiteCollection : esTransPaymentItemVisiteCollection, IEnumerable<TransPaymentItemVisite>
    {
        public TransPaymentItemVisiteCollection()
        {

        }

        public static implicit operator List<TransPaymentItemVisite>(TransPaymentItemVisiteCollection coll)
        {
            List<TransPaymentItemVisite> list = new List<TransPaymentItemVisite>();

            foreach (TransPaymentItemVisite emp in coll)
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
                return TransPaymentItemVisiteMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new TransPaymentItemVisiteQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new TransPaymentItemVisite(row);
        }

        override protected esEntity CreateEntity()
        {
            return new TransPaymentItemVisite();
        }

        #endregion

        [BrowsableAttribute(false)]
        public TransPaymentItemVisiteQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new TransPaymentItemVisiteQuery();
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
        public bool Load(TransPaymentItemVisiteQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public TransPaymentItemVisite AddNew()
        {
            TransPaymentItemVisite entity = base.AddNewEntity() as TransPaymentItemVisite;

            return entity;
        }
        public TransPaymentItemVisite FindByPrimaryKey(String paymentNo, String patientID, String itemID)
        {
            return base.FindByPrimaryKey(paymentNo, patientID, itemID) as TransPaymentItemVisite;
        }

        #region IEnumerable< TransPaymentItemVisite> Members

        IEnumerator<TransPaymentItemVisite> IEnumerable<TransPaymentItemVisite>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as TransPaymentItemVisite;
            }
        }

        #endregion

        private TransPaymentItemVisiteQuery query;
    }


    /// <summary>
    /// Encapsulates the 'TransPaymentItemVisite' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("TransPaymentItemVisite ({PaymentNo, PatientID, ItemID})")]
    [Serializable]
    public partial class TransPaymentItemVisite : esTransPaymentItemVisite
    {
        public TransPaymentItemVisite()
        {
        }

        public TransPaymentItemVisite(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return TransPaymentItemVisiteMetadata.Meta();
            }
        }

        override protected esTransPaymentItemVisiteQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new TransPaymentItemVisiteQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public TransPaymentItemVisiteQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new TransPaymentItemVisiteQuery();
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
        public bool Load(TransPaymentItemVisiteQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private TransPaymentItemVisiteQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class TransPaymentItemVisiteQuery : esTransPaymentItemVisiteQuery
    {
        public TransPaymentItemVisiteQuery()
        {

        }

        public TransPaymentItemVisiteQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "TransPaymentItemVisiteQuery";
        }
    }

    [Serializable]
    public partial class TransPaymentItemVisiteMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected TransPaymentItemVisiteMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(TransPaymentItemVisiteMetadata.ColumnNames.PaymentNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = TransPaymentItemVisiteMetadata.PropertyNames.PaymentNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(TransPaymentItemVisiteMetadata.ColumnNames.PatientID, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = TransPaymentItemVisiteMetadata.PropertyNames.PatientID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 15;
            _columns.Add(c);

            c = new esColumnMetadata(TransPaymentItemVisiteMetadata.ColumnNames.ItemID, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = TransPaymentItemVisiteMetadata.PropertyNames.ItemID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(TransPaymentItemVisiteMetadata.ColumnNames.VisiteQty, 3, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = TransPaymentItemVisiteMetadata.PropertyNames.VisiteQty;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(TransPaymentItemVisiteMetadata.ColumnNames.RealizationQty, 4, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = TransPaymentItemVisiteMetadata.PropertyNames.RealizationQty;
            c.NumericPrecision = 10;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(TransPaymentItemVisiteMetadata.ColumnNames.Price, 5, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = TransPaymentItemVisiteMetadata.PropertyNames.Price;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(TransPaymentItemVisiteMetadata.ColumnNames.Discount, 6, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = TransPaymentItemVisiteMetadata.PropertyNames.Discount;
            c.NumericPrecision = 5;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(TransPaymentItemVisiteMetadata.ColumnNames.IsClosed, 7, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = TransPaymentItemVisiteMetadata.PropertyNames.IsClosed;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(TransPaymentItemVisiteMetadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = TransPaymentItemVisiteMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransPaymentItemVisiteMetadata.ColumnNames.LastUpdateByUserID, 9, typeof(System.String), esSystemType.String);
            c.PropertyName = TransPaymentItemVisiteMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 40;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransPaymentItemVisiteMetadata.ColumnNames.ServiceUnitID, 10, typeof(System.String), esSystemType.String);
            c.PropertyName = TransPaymentItemVisiteMetadata.PropertyNames.ServiceUnitID;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransPaymentItemVisiteMetadata.ColumnNames.ExpiredDate, 11, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = TransPaymentItemVisiteMetadata.PropertyNames.ExpiredDate;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public TransPaymentItemVisiteMetadata Meta()
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
            public const string PaymentNo = "PaymentNo";
            public const string PatientID = "PatientID";
            public const string ItemID = "ItemID";
            public const string VisiteQty = "VisiteQty";
            public const string RealizationQty = "RealizationQty";
            public const string Price = "Price";
            public const string Discount = "Discount";
            public const string IsClosed = "IsClosed";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string ServiceUnitID = "ServiceUnitID";
            public const string ExpiredDate = "ExpiredDate";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string PaymentNo = "PaymentNo";
            public const string PatientID = "PatientID";
            public const string ItemID = "ItemID";
            public const string VisiteQty = "VisiteQty";
            public const string RealizationQty = "RealizationQty";
            public const string Price = "Price";
            public const string Discount = "Discount";
            public const string IsClosed = "IsClosed";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string ServiceUnitID = "ServiceUnitID";
            public const string ExpiredDate = "ExpiredDate";
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
            lock (typeof(TransPaymentItemVisiteMetadata))
            {
                if (TransPaymentItemVisiteMetadata.mapDelegates == null)
                {
                    TransPaymentItemVisiteMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (TransPaymentItemVisiteMetadata.meta == null)
                {
                    TransPaymentItemVisiteMetadata.meta = new TransPaymentItemVisiteMetadata();
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

                meta.AddTypeMap("PaymentNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("PatientID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("VisiteQty", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("RealizationQty", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("Price", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("Discount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("IsClosed", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ExpiredDate", new esTypeMap("datetime", "System.DateTime"));


                meta.Source = "TransPaymentItemVisite";
                meta.Destination = "TransPaymentItemVisite";
                meta.spInsert = "proc_TransPaymentItemVisiteInsert";
                meta.spUpdate = "proc_TransPaymentItemVisiteUpdate";
                meta.spDelete = "proc_TransPaymentItemVisiteDelete";
                meta.spLoadAll = "proc_TransPaymentItemVisiteLoadAll";
                meta.spLoadByPrimaryKey = "proc_TransPaymentItemVisiteLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private TransPaymentItemVisiteMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
