/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 3/2/2020 4:34:37 PM
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
    abstract public class esTransPrescriptionOrderCollection : esEntityCollectionWAuditLog
    {
        public esTransPrescriptionOrderCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "TransPrescriptionOrderCollection";
        }

        #region Query Logic
        protected void InitQuery(esTransPrescriptionOrderQuery query)
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
            this.InitQuery(query as esTransPrescriptionOrderQuery);
        }
        #endregion

        virtual public TransPrescriptionOrder DetachEntity(TransPrescriptionOrder entity)
        {
            return base.DetachEntity(entity) as TransPrescriptionOrder;
        }

        virtual public TransPrescriptionOrder AttachEntity(TransPrescriptionOrder entity)
        {
            return base.AttachEntity(entity) as TransPrescriptionOrder;
        }

        virtual public void Combine(TransPrescriptionOrderCollection collection)
        {
            base.Combine(collection);
        }

        new public TransPrescriptionOrder this[int index]
        {
            get
            {
                return base[index] as TransPrescriptionOrder;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(TransPrescriptionOrder);
        }
    }

    [Serializable]
    abstract public class esTransPrescriptionOrder : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esTransPrescriptionOrderQuery GetDynamicQuery()
        {
            return null;
        }

        public esTransPrescriptionOrder()
        {
        }

        public esTransPrescriptionOrder(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String orderNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(orderNo);
            else
                return LoadByPrimaryKeyStoredProcedure(orderNo);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String orderNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(orderNo);
            else
                return LoadByPrimaryKeyStoredProcedure(orderNo);
        }

        private bool LoadByPrimaryKeyDynamic(String orderNo)
        {
            esTransPrescriptionOrderQuery query = this.GetDynamicQuery();
            query.Where(query.OrderNo == orderNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String orderNo)
        {
            esParameters parms = new esParameters();
            parms.Add("OrderNo", orderNo);
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
                        case "OrderNo": this.str.OrderNo = (string)value; break;
                        case "OrderDate": this.str.OrderDate = (string)value; break;
                        case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
                        case "Notes": this.str.Notes = (string)value; break;
                        case "IsApproval": this.str.IsApproval = (string)value; break;
                        case "IsVoid": this.str.IsVoid = (string)value; break;
                        case "ApprovalDate": this.str.ApprovalDate = (string)value; break;
                        case "ApprovalBy": this.str.ApprovalBy = (string)value; break;
                        case "VoidDate": this.str.VoidDate = (string)value; break;
                        case "VoidBy": this.str.VoidBy = (string)value; break;
                        case "IsClosed": this.str.IsClosed = (string)value; break;
                        case "CreateDateTime": this.str.CreateDateTime = (string)value; break;
                        case "CreateBy": this.str.CreateBy = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateBy": this.str.LastUpdateBy = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "OrderDate":

                            if (value == null || value is System.DateTime)
                                this.OrderDate = (System.DateTime?)value;
                            break;
                        case "IsApproval":

                            if (value == null || value is System.Boolean)
                                this.IsApproval = (System.Boolean?)value;
                            break;
                        case "IsVoid":

                            if (value == null || value is System.Boolean)
                                this.IsVoid = (System.Boolean?)value;
                            break;
                        case "ApprovalDate":

                            if (value == null || value is System.DateTime)
                                this.ApprovalDate = (System.DateTime?)value;
                            break;
                        case "VoidDate":

                            if (value == null || value is System.DateTime)
                                this.VoidDate = (System.DateTime?)value;
                            break;
                        case "IsClosed":

                            if (value == null || value is System.Boolean)
                                this.IsClosed = (System.Boolean?)value;
                            break;
                        case "CreateDateTime":

                            if (value == null || value is System.DateTime)
                                this.CreateDateTime = (System.DateTime?)value;
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
        /// Maps to TransPrescriptionOrder.OrderNo
        /// </summary>
        virtual public System.String OrderNo
        {
            get
            {
                return base.GetSystemString(TransPrescriptionOrderMetadata.ColumnNames.OrderNo);
            }

            set
            {
                base.SetSystemString(TransPrescriptionOrderMetadata.ColumnNames.OrderNo, value);
            }
        }
        /// <summary>
        /// Maps to TransPrescriptionOrder.OrderDate
        /// </summary>
        virtual public System.DateTime? OrderDate
        {
            get
            {
                return base.GetSystemDateTime(TransPrescriptionOrderMetadata.ColumnNames.OrderDate);
            }

            set
            {
                base.SetSystemDateTime(TransPrescriptionOrderMetadata.ColumnNames.OrderDate, value);
            }
        }
        /// <summary>
        /// Maps to TransPrescriptionOrder.RegistrationNo
        /// </summary>
        virtual public System.String RegistrationNo
        {
            get
            {
                return base.GetSystemString(TransPrescriptionOrderMetadata.ColumnNames.RegistrationNo);
            }

            set
            {
                base.SetSystemString(TransPrescriptionOrderMetadata.ColumnNames.RegistrationNo, value);
            }
        }
        /// <summary>
        /// Maps to TransPrescriptionOrder.Notes
        /// </summary>
        virtual public System.String Notes
        {
            get
            {
                return base.GetSystemString(TransPrescriptionOrderMetadata.ColumnNames.Notes);
            }

            set
            {
                base.SetSystemString(TransPrescriptionOrderMetadata.ColumnNames.Notes, value);
            }
        }
        /// <summary>
        /// Maps to TransPrescriptionOrder.IsApproval
        /// </summary>
        virtual public System.Boolean? IsApproval
        {
            get
            {
                return base.GetSystemBoolean(TransPrescriptionOrderMetadata.ColumnNames.IsApproval);
            }

            set
            {
                base.SetSystemBoolean(TransPrescriptionOrderMetadata.ColumnNames.IsApproval, value);
            }
        }
        /// <summary>
        /// Maps to TransPrescriptionOrder.IsVoid
        /// </summary>
        virtual public System.Boolean? IsVoid
        {
            get
            {
                return base.GetSystemBoolean(TransPrescriptionOrderMetadata.ColumnNames.IsVoid);
            }

            set
            {
                base.SetSystemBoolean(TransPrescriptionOrderMetadata.ColumnNames.IsVoid, value);
            }
        }
        /// <summary>
        /// Maps to TransPrescriptionOrder.ApprovalDate
        /// </summary>
        virtual public System.DateTime? ApprovalDate
        {
            get
            {
                return base.GetSystemDateTime(TransPrescriptionOrderMetadata.ColumnNames.ApprovalDate);
            }

            set
            {
                base.SetSystemDateTime(TransPrescriptionOrderMetadata.ColumnNames.ApprovalDate, value);
            }
        }
        /// <summary>
        /// Maps to TransPrescriptionOrder.ApprovalBy
        /// </summary>
        virtual public System.String ApprovalBy
        {
            get
            {
                return base.GetSystemString(TransPrescriptionOrderMetadata.ColumnNames.ApprovalBy);
            }

            set
            {
                base.SetSystemString(TransPrescriptionOrderMetadata.ColumnNames.ApprovalBy, value);
            }
        }
        /// <summary>
        /// Maps to TransPrescriptionOrder.VoidDate
        /// </summary>
        virtual public System.DateTime? VoidDate
        {
            get
            {
                return base.GetSystemDateTime(TransPrescriptionOrderMetadata.ColumnNames.VoidDate);
            }

            set
            {
                base.SetSystemDateTime(TransPrescriptionOrderMetadata.ColumnNames.VoidDate, value);
            }
        }
        /// <summary>
        /// Maps to TransPrescriptionOrder.VoidBy
        /// </summary>
        virtual public System.String VoidBy
        {
            get
            {
                return base.GetSystemString(TransPrescriptionOrderMetadata.ColumnNames.VoidBy);
            }

            set
            {
                base.SetSystemString(TransPrescriptionOrderMetadata.ColumnNames.VoidBy, value);
            }
        }
        /// <summary>
        /// Maps to TransPrescriptionOrder.IsClosed
        /// </summary>
        virtual public System.Boolean? IsClosed
        {
            get
            {
                return base.GetSystemBoolean(TransPrescriptionOrderMetadata.ColumnNames.IsClosed);
            }

            set
            {
                base.SetSystemBoolean(TransPrescriptionOrderMetadata.ColumnNames.IsClosed, value);
            }
        }
        /// <summary>
        /// Maps to TransPrescriptionOrder.CreateDateTime
        /// </summary>
        virtual public System.DateTime? CreateDateTime
        {
            get
            {
                return base.GetSystemDateTime(TransPrescriptionOrderMetadata.ColumnNames.CreateDateTime);
            }

            set
            {
                base.SetSystemDateTime(TransPrescriptionOrderMetadata.ColumnNames.CreateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to TransPrescriptionOrder.CreateBy
        /// </summary>
        virtual public System.String CreateBy
        {
            get
            {
                return base.GetSystemString(TransPrescriptionOrderMetadata.ColumnNames.CreateBy);
            }

            set
            {
                base.SetSystemString(TransPrescriptionOrderMetadata.ColumnNames.CreateBy, value);
            }
        }
        /// <summary>
        /// Maps to TransPrescriptionOrder.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(TransPrescriptionOrderMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(TransPrescriptionOrderMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to TransPrescriptionOrder.LastUpdateBy
        /// </summary>
        virtual public System.String LastUpdateBy
        {
            get
            {
                return base.GetSystemString(TransPrescriptionOrderMetadata.ColumnNames.LastUpdateBy);
            }

            set
            {
                base.SetSystemString(TransPrescriptionOrderMetadata.ColumnNames.LastUpdateBy, value);
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
            public esStrings(esTransPrescriptionOrder entity)
            {
                this.entity = entity;
            }
            public System.String OrderNo
            {
                get
                {
                    System.String data = entity.OrderNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.OrderNo = null;
                    else entity.OrderNo = Convert.ToString(value);
                }
            }
            public System.String OrderDate
            {
                get
                {
                    System.DateTime? data = entity.OrderDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.OrderDate = null;
                    else entity.OrderDate = Convert.ToDateTime(value);
                }
            }
            public System.String RegistrationNo
            {
                get
                {
                    System.String data = entity.RegistrationNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RegistrationNo = null;
                    else entity.RegistrationNo = Convert.ToString(value);
                }
            }
            public System.String Notes
            {
                get
                {
                    System.String data = entity.Notes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Notes = null;
                    else entity.Notes = Convert.ToString(value);
                }
            }
            public System.String IsApproval
            {
                get
                {
                    System.Boolean? data = entity.IsApproval;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsApproval = null;
                    else entity.IsApproval = Convert.ToBoolean(value);
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
            public System.String ApprovalDate
            {
                get
                {
                    System.DateTime? data = entity.ApprovalDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ApprovalDate = null;
                    else entity.ApprovalDate = Convert.ToDateTime(value);
                }
            }
            public System.String ApprovalBy
            {
                get
                {
                    System.String data = entity.ApprovalBy;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ApprovalBy = null;
                    else entity.ApprovalBy = Convert.ToString(value);
                }
            }
            public System.String VoidDate
            {
                get
                {
                    System.DateTime? data = entity.VoidDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.VoidDate = null;
                    else entity.VoidDate = Convert.ToDateTime(value);
                }
            }
            public System.String VoidBy
            {
                get
                {
                    System.String data = entity.VoidBy;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.VoidBy = null;
                    else entity.VoidBy = Convert.ToString(value);
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
            public System.String CreateDateTime
            {
                get
                {
                    System.DateTime? data = entity.CreateDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CreateDateTime = null;
                    else entity.CreateDateTime = Convert.ToDateTime(value);
                }
            }
            public System.String CreateBy
            {
                get
                {
                    System.String data = entity.CreateBy;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CreateBy = null;
                    else entity.CreateBy = Convert.ToString(value);
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
            public System.String LastUpdateBy
            {
                get
                {
                    System.String data = entity.LastUpdateBy;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.LastUpdateBy = null;
                    else entity.LastUpdateBy = Convert.ToString(value);
                }
            }
            private esTransPrescriptionOrder entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esTransPrescriptionOrderQuery query)
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
                throw new Exception("esTransPrescriptionOrder can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class TransPrescriptionOrder : esTransPrescriptionOrder
    {
    }

    [Serializable]
    abstract public class esTransPrescriptionOrderQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return TransPrescriptionOrderMetadata.Meta();
            }
        }

        public esQueryItem OrderNo
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionOrderMetadata.ColumnNames.OrderNo, esSystemType.String);
            }
        }

        public esQueryItem OrderDate
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionOrderMetadata.ColumnNames.OrderDate, esSystemType.DateTime);
            }
        }

        public esQueryItem RegistrationNo
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionOrderMetadata.ColumnNames.RegistrationNo, esSystemType.String);
            }
        }

        public esQueryItem Notes
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionOrderMetadata.ColumnNames.Notes, esSystemType.String);
            }
        }

        public esQueryItem IsApproval
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionOrderMetadata.ColumnNames.IsApproval, esSystemType.Boolean);
            }
        }

        public esQueryItem IsVoid
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionOrderMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
            }
        }

        public esQueryItem ApprovalDate
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionOrderMetadata.ColumnNames.ApprovalDate, esSystemType.DateTime);
            }
        }

        public esQueryItem ApprovalBy
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionOrderMetadata.ColumnNames.ApprovalBy, esSystemType.String);
            }
        }

        public esQueryItem VoidDate
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionOrderMetadata.ColumnNames.VoidDate, esSystemType.DateTime);
            }
        }

        public esQueryItem VoidBy
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionOrderMetadata.ColumnNames.VoidBy, esSystemType.String);
            }
        }

        public esQueryItem IsClosed
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionOrderMetadata.ColumnNames.IsClosed, esSystemType.Boolean);
            }
        }

        public esQueryItem CreateDateTime
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionOrderMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem CreateBy
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionOrderMetadata.ColumnNames.CreateBy, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionOrderMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateBy
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionOrderMetadata.ColumnNames.LastUpdateBy, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("TransPrescriptionOrderCollection")]
    public partial class TransPrescriptionOrderCollection : esTransPrescriptionOrderCollection, IEnumerable<TransPrescriptionOrder>
    {
        public TransPrescriptionOrderCollection()
        {

        }

        public static implicit operator List<TransPrescriptionOrder>(TransPrescriptionOrderCollection coll)
        {
            List<TransPrescriptionOrder> list = new List<TransPrescriptionOrder>();

            foreach (TransPrescriptionOrder emp in coll)
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
                return TransPrescriptionOrderMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new TransPrescriptionOrderQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new TransPrescriptionOrder(row);
        }

        override protected esEntity CreateEntity()
        {
            return new TransPrescriptionOrder();
        }

        #endregion

        [BrowsableAttribute(false)]
        public TransPrescriptionOrderQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new TransPrescriptionOrderQuery();
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
        public bool Load(TransPrescriptionOrderQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public TransPrescriptionOrder AddNew()
        {
            TransPrescriptionOrder entity = base.AddNewEntity() as TransPrescriptionOrder;

            return entity;
        }
        public TransPrescriptionOrder FindByPrimaryKey(String orderNo)
        {
            return base.FindByPrimaryKey(orderNo) as TransPrescriptionOrder;
        }

        #region IEnumerable< TransPrescriptionOrder> Members

        IEnumerator<TransPrescriptionOrder> IEnumerable<TransPrescriptionOrder>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as TransPrescriptionOrder;
            }
        }

        #endregion

        private TransPrescriptionOrderQuery query;
    }


    /// <summary>
    /// Encapsulates the 'TransPrescriptionOrder' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("TransPrescriptionOrder ({OrderNo})")]
    [Serializable]
    public partial class TransPrescriptionOrder : esTransPrescriptionOrder
    {
        public TransPrescriptionOrder()
        {
        }

        public TransPrescriptionOrder(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return TransPrescriptionOrderMetadata.Meta();
            }
        }

        override protected esTransPrescriptionOrderQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new TransPrescriptionOrderQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public TransPrescriptionOrderQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new TransPrescriptionOrderQuery();
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
        public bool Load(TransPrescriptionOrderQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private TransPrescriptionOrderQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class TransPrescriptionOrderQuery : esTransPrescriptionOrderQuery
    {
        public TransPrescriptionOrderQuery()
        {

        }

        public TransPrescriptionOrderQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "TransPrescriptionOrderQuery";
        }
    }

    [Serializable]
    public partial class TransPrescriptionOrderMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected TransPrescriptionOrderMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(TransPrescriptionOrderMetadata.ColumnNames.OrderNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = TransPrescriptionOrderMetadata.PropertyNames.OrderNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionOrderMetadata.ColumnNames.OrderDate, 1, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = TransPrescriptionOrderMetadata.PropertyNames.OrderDate;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionOrderMetadata.ColumnNames.RegistrationNo, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = TransPrescriptionOrderMetadata.PropertyNames.RegistrationNo;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionOrderMetadata.ColumnNames.Notes, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = TransPrescriptionOrderMetadata.PropertyNames.Notes;
            c.CharacterMaxLength = 500;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionOrderMetadata.ColumnNames.IsApproval, 4, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = TransPrescriptionOrderMetadata.PropertyNames.IsApproval;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionOrderMetadata.ColumnNames.IsVoid, 5, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = TransPrescriptionOrderMetadata.PropertyNames.IsVoid;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionOrderMetadata.ColumnNames.ApprovalDate, 6, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = TransPrescriptionOrderMetadata.PropertyNames.ApprovalDate;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionOrderMetadata.ColumnNames.ApprovalBy, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = TransPrescriptionOrderMetadata.PropertyNames.ApprovalBy;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionOrderMetadata.ColumnNames.VoidDate, 8, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = TransPrescriptionOrderMetadata.PropertyNames.VoidDate;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionOrderMetadata.ColumnNames.VoidBy, 9, typeof(System.String), esSystemType.String);
            c.PropertyName = TransPrescriptionOrderMetadata.PropertyNames.VoidBy;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionOrderMetadata.ColumnNames.IsClosed, 10, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = TransPrescriptionOrderMetadata.PropertyNames.IsClosed;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionOrderMetadata.ColumnNames.CreateDateTime, 11, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = TransPrescriptionOrderMetadata.PropertyNames.CreateDateTime;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionOrderMetadata.ColumnNames.CreateBy, 12, typeof(System.String), esSystemType.String);
            c.PropertyName = TransPrescriptionOrderMetadata.PropertyNames.CreateBy;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionOrderMetadata.ColumnNames.LastUpdateDateTime, 13, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = TransPrescriptionOrderMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionOrderMetadata.ColumnNames.LastUpdateBy, 14, typeof(System.String), esSystemType.String);
            c.PropertyName = TransPrescriptionOrderMetadata.PropertyNames.LastUpdateBy;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public TransPrescriptionOrderMetadata Meta()
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
            public const string OrderNo = "OrderNo";
            public const string OrderDate = "OrderDate";
            public const string RegistrationNo = "RegistrationNo";
            public const string Notes = "Notes";
            public const string IsApproval = "IsApproval";
            public const string IsVoid = "IsVoid";
            public const string ApprovalDate = "ApprovalDate";
            public const string ApprovalBy = "ApprovalBy";
            public const string VoidDate = "VoidDate";
            public const string VoidBy = "VoidBy";
            public const string IsClosed = "IsClosed";
            public const string CreateDateTime = "CreateDateTime";
            public const string CreateBy = "CreateBy";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateBy = "LastUpdateBy";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string OrderNo = "OrderNo";
            public const string OrderDate = "OrderDate";
            public const string RegistrationNo = "RegistrationNo";
            public const string Notes = "Notes";
            public const string IsApproval = "IsApproval";
            public const string IsVoid = "IsVoid";
            public const string ApprovalDate = "ApprovalDate";
            public const string ApprovalBy = "ApprovalBy";
            public const string VoidDate = "VoidDate";
            public const string VoidBy = "VoidBy";
            public const string IsClosed = "IsClosed";
            public const string CreateDateTime = "CreateDateTime";
            public const string CreateBy = "CreateBy";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateBy = "LastUpdateBy";
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
            lock (typeof(TransPrescriptionOrderMetadata))
            {
                if (TransPrescriptionOrderMetadata.mapDelegates == null)
                {
                    TransPrescriptionOrderMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (TransPrescriptionOrderMetadata.meta == null)
                {
                    TransPrescriptionOrderMetadata.meta = new TransPrescriptionOrderMetadata();
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

                meta.AddTypeMap("OrderNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("OrderDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsApproval", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("ApprovalDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("ApprovalBy", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("VoidDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("VoidBy", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsClosed", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("CreateBy", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateBy", new esTypeMap("varchar", "System.String"));


                meta.Source = "TransPrescriptionOrder";
                meta.Destination = "TransPrescriptionOrder";
                meta.spInsert = "proc_TransPrescriptionOrderInsert";
                meta.spUpdate = "proc_TransPrescriptionOrderUpdate";
                meta.spDelete = "proc_TransPrescriptionOrderDelete";
                meta.spLoadAll = "proc_TransPrescriptionOrderLoadAll";
                meta.spLoadByPrimaryKey = "proc_TransPrescriptionOrderLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private TransPrescriptionOrderMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
