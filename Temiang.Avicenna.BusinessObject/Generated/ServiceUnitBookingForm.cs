///*
//===============================================================================
//                       Persistence Layer and Business Objects  
//===============================================================================
//                       Date Generated       : 07/19/19 11:04:24 AM
//===============================================================================
//*/

//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Data;
//using System.ComponentModel;
//using System.Xml.Serialization;
//using Temiang.Dal.Core;
//using Temiang.Dal.Interfaces;
//using Temiang.Dal.DynamicQuery;

//namespace Temiang.Avicenna.BusinessObject
//{
//    [Serializable]
//    abstract public class esServiceUnitBookingFormCollection : esEntityCollectionWAuditLog
//    {
//        public esServiceUnitBookingFormCollection()
//        {

//        }


//        protected override string GetCollectionName()
//        {
//            return "ServiceUnitBookingFormCollection";
//        }

//        #region Query Logic
//        protected void InitQuery(esServiceUnitBookingFormQuery query)
//        {
//            query.OnLoadDelegate = this.OnQueryLoaded;
//            query.es2.Connection = ((IEntityCollection)this).Connection;
//        }

//        protected bool OnQueryLoaded(DataTable table)
//        {
//            this.PopulateCollection(table);
//            return (this.RowCount > 0) ? true : false;
//        }

//        protected override void HookupQuery(esDynamicQuery query)
//        {
//            this.InitQuery(query as esServiceUnitBookingFormQuery);
//        }
//        #endregion

//        virtual public ServiceUnitBookingForm DetachEntity(ServiceUnitBookingForm entity)
//        {
//            return base.DetachEntity(entity) as ServiceUnitBookingForm;
//        }

//        virtual public ServiceUnitBookingForm AttachEntity(ServiceUnitBookingForm entity)
//        {
//            return base.AttachEntity(entity) as ServiceUnitBookingForm;
//        }

//        virtual public void Combine(ServiceUnitBookingFormCollection collection)
//        {
//            base.Combine(collection);
//        }

//        new public ServiceUnitBookingForm this[int index]
//        {
//            get
//            {
//                return base[index] as ServiceUnitBookingForm;
//            }
//        }

//        public override Type GetEntityType()
//        {
//            return typeof(ServiceUnitBookingForm);
//        }
//    }

//    [Serializable]
//    abstract public class esServiceUnitBookingForm : esEntityWAuditLog
//    {
//        /// <summary>
//        /// Used internally by the entity's DynamicQuery mechanism.
//        /// </summary>
//        virtual protected esServiceUnitBookingFormQuery GetDynamicQuery()
//        {
//            return null;
//        }

//        public esServiceUnitBookingForm()
//        {
//        }

//        public esServiceUnitBookingForm(DataRow row)
//            : base(row)
//        {
//        }


//        #region LoadByPrimaryKey
//        public virtual bool LoadByPrimaryKey(String bookingNo, String questionFormID)
//        {
//            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
//                return LoadByPrimaryKeyDynamic(bookingNo, questionFormID);
//            else
//                return LoadByPrimaryKeyStoredProcedure(bookingNo, questionFormID);
//        }

//        /// <summary>
//        /// Loads an entity by primary key
//        /// </summary>
//        /// <remarks>
//        /// Requires primary keys be defined on all tables.
//        /// If a table does not have a primary key set,
//        /// this method will not compile.
//        /// </remarks>
//        /// <param name="sqlAccessType">Either esSqlAccessType StoredProcedure or DynamicSQL</param>
//        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String bookingNo, String questionFormID)
//        {
//            if (sqlAccessType == esSqlAccessType.DynamicSQL)
//                return LoadByPrimaryKeyDynamic(bookingNo, questionFormID);
//            else
//                return LoadByPrimaryKeyStoredProcedure(bookingNo, questionFormID);
//        }

//        private bool LoadByPrimaryKeyDynamic(String bookingNo, String questionFormID)
//        {
//            esServiceUnitBookingFormQuery query = this.GetDynamicQuery();
//            query.Where(query.BookingNo == bookingNo, query.QuestionFormID == questionFormID);
//            return query.Load();
//        }

//        private bool LoadByPrimaryKeyStoredProcedure(String bookingNo, String questionFormID)
//        {
//            esParameters parms = new esParameters();
//            parms.Add("BookingNo", bookingNo);
//            parms.Add("QuestionFormID", questionFormID);
//            return this.Load(esQueryType.StoredProcedure, this.es.spLoadByPrimaryKey, parms);
//        }
//        #endregion

//        #region Properties

//        public override void SetProperties(IDictionary values)
//        {
//            foreach (string propertyName in values.Keys)
//            {
//                this.SetProperty(propertyName, values[propertyName]);
//            }
//        }

//        public override void SetProperty(string name, object value)
//        {
//            if (this.Row == null) this.AddNew();

//            esColumnMetadata col = this.Meta.Columns.FindByPropertyName(name);
//            if (col != null)
//            {
//                if (value == null || value is System.String)
//                {
//                    // Use the strongly typed property
//                    switch (name)
//                    {
//                        case "BookingNo": this.str.BookingNo = (string)value; break;
//                        case "QuestionFormID": this.str.QuestionFormID = (string)value; break;
//                        case "TransactionNo": this.str.TransactionNo = (string)value; break;
//                        case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;
//                        case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;
//                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
//                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
//                    }
//                }
//                else
//                {
//                    switch (name)
//                    {
//                        case "CreatedDateTime":

//                            if (value == null || value is System.DateTime)
//                                this.CreatedDateTime = (System.DateTime?)value;
//                            break;
//                        case "LastUpdateDateTime":

//                            if (value == null || value is System.DateTime)
//                                this.LastUpdateDateTime = (System.DateTime?)value;
//                            break;

//                        default:
//                            break;
//                    }
//                }
//            }
//            else if (this.Row.Table.Columns.Contains(name))
//            {
//                this.Row[name] = value;
//            }
//            else
//            {
//                throw new Exception("SetProperty Error: '" + name + "' not found");
//            }
//        }

//        /// <summary>
//        /// Maps to ServiceUnitBookingForm.BookingNo
//        /// </summary>
//        virtual public System.String BookingNo
//        {
//            get
//            {
//                return base.GetSystemString(ServiceUnitBookingFormMetadata.ColumnNames.BookingNo);
//            }

//            set
//            {
//                base.SetSystemString(ServiceUnitBookingFormMetadata.ColumnNames.BookingNo, value);
//            }
//        }
//        /// <summary>
//        /// Maps to ServiceUnitBookingForm.QuestionFormID
//        /// </summary>
//        virtual public System.String QuestionFormID
//        {
//            get
//            {
//                return base.GetSystemString(ServiceUnitBookingFormMetadata.ColumnNames.QuestionFormID);
//            }

//            set
//            {
//                base.SetSystemString(ServiceUnitBookingFormMetadata.ColumnNames.QuestionFormID, value);
//            }
//        }
//        /// <summary>
//        /// Maps to ServiceUnitBookingForm.TransactionNo
//        /// </summary>
//        virtual public System.String TransactionNo
//        {
//            get
//            {
//                return base.GetSystemString(ServiceUnitBookingFormMetadata.ColumnNames.TransactionNo);
//            }

//            set
//            {
//                base.SetSystemString(ServiceUnitBookingFormMetadata.ColumnNames.TransactionNo, value);
//            }
//        }
//        /// <summary>
//        /// Maps to ServiceUnitBookingForm.CreatedDateTime
//        /// </summary>
//        virtual public System.DateTime? CreatedDateTime
//        {
//            get
//            {
//                return base.GetSystemDateTime(ServiceUnitBookingFormMetadata.ColumnNames.CreatedDateTime);
//            }

//            set
//            {
//                base.SetSystemDateTime(ServiceUnitBookingFormMetadata.ColumnNames.CreatedDateTime, value);
//            }
//        }
//        /// <summary>
//        /// Maps to ServiceUnitBookingForm.CreatedByUserID
//        /// </summary>
//        virtual public System.String CreatedByUserID
//        {
//            get
//            {
//                return base.GetSystemString(ServiceUnitBookingFormMetadata.ColumnNames.CreatedByUserID);
//            }

//            set
//            {
//                base.SetSystemString(ServiceUnitBookingFormMetadata.ColumnNames.CreatedByUserID, value);
//            }
//        }
//        /// <summary>
//        /// Maps to ServiceUnitBookingForm.LastUpdateDateTime
//        /// </summary>
//        virtual public System.DateTime? LastUpdateDateTime
//        {
//            get
//            {
//                return base.GetSystemDateTime(ServiceUnitBookingFormMetadata.ColumnNames.LastUpdateDateTime);
//            }

//            set
//            {
//                base.SetSystemDateTime(ServiceUnitBookingFormMetadata.ColumnNames.LastUpdateDateTime, value);
//            }
//        }
//        /// <summary>
//        /// Maps to ServiceUnitBookingForm.LastUpdateByUserID
//        /// </summary>
//        virtual public System.String LastUpdateByUserID
//        {
//            get
//            {
//                return base.GetSystemString(ServiceUnitBookingFormMetadata.ColumnNames.LastUpdateByUserID);
//            }

//            set
//            {
//                base.SetSystemString(ServiceUnitBookingFormMetadata.ColumnNames.LastUpdateByUserID, value);
//            }
//        }

//        #endregion

//        #region String Properties

//        /// <summary>
//        /// Converts an entity's properties to
//        /// and from strings.
//        /// </summary>
//        /// <remarks>
//        /// The str properties Get and Set provide easy conversion
//        /// between a string and a property's data type. Not all
//        /// data types will get a str property.
//        /// </remarks>
//        /// <example>
//        /// Set a datetime from a string.
//        /// <code>
//        /// Employees entity = new Employees();
//        /// entity.LoadByPrimaryKey(10);
//        /// entity.str.HireDate = "2007-01-01 00:00:00";
//        /// entity.Save();
//        /// </code>
//        /// Get a datetime as a string.
//        /// <code>
//        /// Employees entity = new Employees();
//        /// entity.LoadByPrimaryKey(10);
//        /// string theDate = entity.str.HireDate;
//        /// </code>
//        /// </example>
//        [BrowsableAttribute(false)]
//        public esStrings str
//        {
//            get
//            {
//                if (esstrings == null)
//                {
//                    esstrings = new esStrings(this);
//                }
//                return esstrings;
//            }
//        }

//        [Serializable]
//        sealed public class esStrings
//        {
//            public esStrings(esServiceUnitBookingForm entity)
//            {
//                this.entity = entity;
//            }
//            public System.String BookingNo
//            {
//                get
//                {
//                    System.String data = entity.BookingNo;
//                    return (data == null) ? String.Empty : Convert.ToString(data);
//                }

//                set
//                {
//                    if (value == null || value.Length == 0) entity.BookingNo = null;
//                    else entity.BookingNo = Convert.ToString(value);
//                }
//            }
//            public System.String QuestionFormID
//            {
//                get
//                {
//                    System.String data = entity.QuestionFormID;
//                    return (data == null) ? String.Empty : Convert.ToString(data);
//                }

//                set
//                {
//                    if (value == null || value.Length == 0) entity.QuestionFormID = null;
//                    else entity.QuestionFormID = Convert.ToString(value);
//                }
//            }
//            public System.String TransactionNo
//            {
//                get
//                {
//                    System.String data = entity.TransactionNo;
//                    return (data == null) ? String.Empty : Convert.ToString(data);
//                }

//                set
//                {
//                    if (value == null || value.Length == 0) entity.TransactionNo = null;
//                    else entity.TransactionNo = Convert.ToString(value);
//                }
//            }
//            public System.String CreatedDateTime
//            {
//                get
//                {
//                    System.DateTime? data = entity.CreatedDateTime;
//                    return (data == null) ? String.Empty : Convert.ToString(data);
//                }

//                set
//                {
//                    if (value == null || value.Length == 0) entity.CreatedDateTime = null;
//                    else entity.CreatedDateTime = Convert.ToDateTime(value);
//                }
//            }
//            public System.String CreatedByUserID
//            {
//                get
//                {
//                    System.String data = entity.CreatedByUserID;
//                    return (data == null) ? String.Empty : Convert.ToString(data);
//                }

//                set
//                {
//                    if (value == null || value.Length == 0) entity.CreatedByUserID = null;
//                    else entity.CreatedByUserID = Convert.ToString(value);
//                }
//            }
//            public System.String LastUpdateDateTime
//            {
//                get
//                {
//                    System.DateTime? data = entity.LastUpdateDateTime;
//                    return (data == null) ? String.Empty : Convert.ToString(data);
//                }

//                set
//                {
//                    if (value == null || value.Length == 0) entity.LastUpdateDateTime = null;
//                    else entity.LastUpdateDateTime = Convert.ToDateTime(value);
//                }
//            }
//            public System.String LastUpdateByUserID
//            {
//                get
//                {
//                    System.String data = entity.LastUpdateByUserID;
//                    return (data == null) ? String.Empty : Convert.ToString(data);
//                }

//                set
//                {
//                    if (value == null || value.Length == 0) entity.LastUpdateByUserID = null;
//                    else entity.LastUpdateByUserID = Convert.ToString(value);
//                }
//            }
//            private esServiceUnitBookingForm entity;
//        }
//        #endregion

//        #region Query Logic
//        protected void InitQuery(esServiceUnitBookingFormQuery query)
//        {
//            query.OnLoadDelegate = this.OnQueryLoaded;
//            query.es2.Connection = ((IEntity)this).Connection;
//        }

//        [System.Diagnostics.DebuggerNonUserCode]
//        protected bool OnQueryLoaded(DataTable table)
//        {
//            bool dataFound = this.PopulateEntity(table);

//            if (this.RowCount > 1)
//            {
//                throw new Exception("esServiceUnitBookingForm can only hold one record of data");
//            }

//            return dataFound;
//        }
//        #endregion

//        [NonSerialized]
//        private esStrings esstrings;
//    }


//    public partial class ServiceUnitBookingForm : esServiceUnitBookingForm
//    {
//    }

//    [Serializable]
//    abstract public class esServiceUnitBookingFormQuery : esDynamicQuery
//    {

//        override protected IMetadata Meta
//        {
//            get
//            {
//                return ServiceUnitBookingFormMetadata.Meta();
//            }
//        }

//        public esQueryItem BookingNo
//        {
//            get
//            {
//                return new esQueryItem(this, ServiceUnitBookingFormMetadata.ColumnNames.BookingNo, esSystemType.String);
//            }
//        }

//        public esQueryItem QuestionFormID
//        {
//            get
//            {
//                return new esQueryItem(this, ServiceUnitBookingFormMetadata.ColumnNames.QuestionFormID, esSystemType.String);
//            }
//        }

//        public esQueryItem TransactionNo
//        {
//            get
//            {
//                return new esQueryItem(this, ServiceUnitBookingFormMetadata.ColumnNames.TransactionNo, esSystemType.String);
//            }
//        }

//        public esQueryItem CreatedDateTime
//        {
//            get
//            {
//                return new esQueryItem(this, ServiceUnitBookingFormMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
//            }
//        }

//        public esQueryItem CreatedByUserID
//        {
//            get
//            {
//                return new esQueryItem(this, ServiceUnitBookingFormMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
//            }
//        }

//        public esQueryItem LastUpdateDateTime
//        {
//            get
//            {
//                return new esQueryItem(this, ServiceUnitBookingFormMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
//            }
//        }

//        public esQueryItem LastUpdateByUserID
//        {
//            get
//            {
//                return new esQueryItem(this, ServiceUnitBookingFormMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
//            }
//        }

//    }

//    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
//    [Serializable]
//    [XmlType("ServiceUnitBookingFormCollection")]
//    public partial class ServiceUnitBookingFormCollection : esServiceUnitBookingFormCollection, IEnumerable<ServiceUnitBookingForm>
//    {
//        public ServiceUnitBookingFormCollection()
//        {

//        }

//        public static implicit operator List<ServiceUnitBookingForm>(ServiceUnitBookingFormCollection coll)
//        {
//            List<ServiceUnitBookingForm> list = new List<ServiceUnitBookingForm>();

//            foreach (ServiceUnitBookingForm emp in coll)
//            {
//                list.Add(emp);
//            }

//            return list;
//        }

//        #region Housekeeping methods
//        override protected IMetadata Meta
//        {
//            get
//            {
//                return ServiceUnitBookingFormMetadata.Meta();
//            }
//        }

//        override protected esDynamicQuery GetDynamicQuery()
//        {
//            if (this.query == null)
//            {
//                this.query = new ServiceUnitBookingFormQuery();
//                this.InitQuery(query);
//            }
//            return this.query;
//        }

//        override protected esEntity CreateEntityForCollection(DataRow row)
//        {
//            return new ServiceUnitBookingForm(row);
//        }

//        override protected esEntity CreateEntity()
//        {
//            return new ServiceUnitBookingForm();
//        }

//        #endregion

//        [BrowsableAttribute(false)]
//        public ServiceUnitBookingFormQuery Query
//        {
//            get
//            {
//                if (this.query == null)
//                {
//                    this.query = new ServiceUnitBookingFormQuery();
//                    base.InitQuery(this.query);
//                }

//                return this.query;
//            }
//        }

//        /// <summary>
//        /// Useful for building up conditional queries.
//        /// In most cases, before loading an entity or collection,
//        /// you should instantiate a new one. This method was added
//        /// to handle specialized circumstances, and should not be
//        /// used as a substitute for that.
//        /// </summary>
//        /// <remarks>
//        /// This just sets obj.Query to null/Nothing.
//        /// In most cases, you will 'new' your object before
//        /// loading it, rather than calling this method.
//        /// It only affects obj.Query.Load(), so is not useful
//        /// when Joins are involved, or for many other situations.
//        /// Because it clears out any obj.Query.Where clauses,
//        /// it can be useful for building conditional queries on the fly.
//        /// <code>
//        /// public bool ReQuery(string lastName, string firstName)
//        /// {
//        ///     this.QueryReset();
//        ///     
//        ///     if(!String.IsNullOrEmpty(lastName))
//        ///     {
//        ///         this.Query.Where(
//        ///             this.Query.LastName == lastName);
//        ///     }
//        ///     if(!String.IsNullOrEmpty(firstName))
//        ///     {
//        ///         this.Query.Where(
//        ///             this.Query.FirstName == firstName);
//        ///     }
//        ///     
//        ///     return this.Query.Load();
//        /// }
//        /// </code>
//        /// <code lang="vbnet">
//        /// Public Function ReQuery(ByVal lastName As String, _
//        ///     ByVal firstName As String) As Boolean
//        /// 
//        ///     Me.QueryReset()
//        /// 
//        ///     If Not [String].IsNullOrEmpty(lastName) Then
//        ///         Me.Query.Where(Me.Query.LastName = lastName)
//        ///     End If
//        ///     If Not [String].IsNullOrEmpty(firstName) Then
//        ///         Me.Query.Where(Me.Query.FirstName = firstName)
//        ///     End If
//        /// 
//        ///     Return Me.Query.Load()
//        /// End Function
//        /// </code>
//        /// </remarks>
//        public void QueryReset()
//        {
//            this.query = null;
//        }

//        /// <summary>
//        /// Used to custom load a Join query.
//        /// Returns true if at least one record was loaded.
//        /// </summary>
//        /// <remarks>
//        /// Provides support for InnerJoin, LeftJoin,
//        /// RightJoin, and FullJoin. You must provide an alias
//        /// for each query when instantiating them.
//        /// <code>
//        /// EmployeeCollection collection = new EmployeeCollection();
//        /// 
//        /// EmployeeQuery emp = new EmployeeQuery("eq");
//        /// CustomerQuery cust = new CustomerQuery("cq");
//        /// 
//        /// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName);
//        /// emp.LeftJoin(cust).On(emp.EmployeeID == cust.StaffAssigned);
//        /// 
//        /// collection.Load(emp);
//        /// </code>
//        /// <code lang="vbnet">
//        /// Dim collection As New EmployeeCollection()
//        /// 
//        /// Dim emp As New EmployeeQuery("eq")
//        /// Dim cust As New CustomerQuery("cq")
//        /// 
//        /// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName)
//        /// emp.LeftJoin(cust).On(emp.EmployeeID = cust.StaffAssigned)
//        /// 
//        /// collection.Load(emp)
//        /// </code>
//        /// </remarks>
//        /// <param name="query">The query object instance name.</param>
//        /// <returns>True if at least one record was loaded.</returns>
//        public bool Load(ServiceUnitBookingFormQuery query)
//        {
//            this.query = query;
//            base.InitQuery(this.query);
//            return this.Query.Load();
//        }

//        /// <summary>
//        /// Adds a new entity to the collection.
//        /// Always calls AddNew() on the entity, in case it is overridden.
//        /// </summary>
//        public ServiceUnitBookingForm AddNew()
//        {
//            ServiceUnitBookingForm entity = base.AddNewEntity() as ServiceUnitBookingForm;

//            return entity;
//        }
//        public ServiceUnitBookingForm FindByPrimaryKey(String bookingNo, String questionFormID)
//        {
//            return base.FindByPrimaryKey(bookingNo, questionFormID) as ServiceUnitBookingForm;
//        }

//        #region IEnumerable< ServiceUnitBookingForm> Members

//        IEnumerator<ServiceUnitBookingForm> IEnumerable<ServiceUnitBookingForm>.GetEnumerator()
//        {
//            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
//            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

//            while (iterator.MoveNext())
//            {
//                yield return iterator.Current as ServiceUnitBookingForm;
//            }
//        }

//        #endregion

//        private ServiceUnitBookingFormQuery query;
//    }


//    /// <summary>
//    /// Encapsulates the 'ServiceUnitBookingForm' table
//    /// </summary>
//    [System.Diagnostics.DebuggerDisplay("ServiceUnitBookingForm ({BookingNo, QuestionFormID})")]
//    [Serializable]
//    public partial class ServiceUnitBookingForm : esServiceUnitBookingForm
//    {
//        public ServiceUnitBookingForm()
//        {
//        }

//        public ServiceUnitBookingForm(DataRow row)
//            : base(row)
//        {
//        }

//        #region Housekeeping methods
//        override protected IMetadata Meta
//        {
//            get
//            {
//                return ServiceUnitBookingFormMetadata.Meta();
//            }
//        }

//        override protected esServiceUnitBookingFormQuery GetDynamicQuery()
//        {
//            if (this.query == null)
//            {
//                this.query = new ServiceUnitBookingFormQuery();
//                this.InitQuery(query);
//            }
//            return this.query;
//        }
//        #endregion

//        [BrowsableAttribute(false)]
//        public ServiceUnitBookingFormQuery Query
//        {
//            get
//            {
//                if (this.query == null)
//                {
//                    this.query = new ServiceUnitBookingFormQuery();
//                    base.InitQuery(this.query);
//                }

//                return this.query;
//            }
//        }

//        /// <summary>
//        /// Useful for building up conditional queries.
//        /// In most cases, before loading an entity or collection,
//        /// you should instantiate a new one. This method was added
//        /// to handle specialized circumstances, and should not be
//        /// used as a substitute for that.
//        /// </summary>
//        /// <remarks>
//        /// This just sets obj.Query to null/Nothing.
//        /// In most cases, you will 'new' your object before
//        /// loading it, rather than calling this method.
//        /// It only affects obj.Query.Load(), so is not useful
//        /// when Joins are involved, or for many other situations.
//        /// Because it clears out any obj.Query.Where clauses,
//        /// it can be useful for building conditional queries on the fly.
//        /// <code>
//        /// public bool ReQuery(string lastName, string firstName)
//        /// {
//        ///     this.QueryReset();
//        ///     
//        ///     if(!String.IsNullOrEmpty(lastName))
//        ///     {
//        ///         this.Query.Where(
//        ///             this.Query.LastName == lastName);
//        ///     }
//        ///     if(!String.IsNullOrEmpty(firstName))
//        ///     {
//        ///         this.Query.Where(
//        ///             this.Query.FirstName == firstName);
//        ///     }
//        ///     
//        ///     return this.Query.Load();
//        /// }
//        /// </code>
//        /// <code lang="vbnet">
//        /// Public Function ReQuery(ByVal lastName As String, _
//        ///     ByVal firstName As String) As Boolean
//        /// 
//        ///     Me.QueryReset()
//        /// 
//        ///     If Not [String].IsNullOrEmpty(lastName) Then
//        ///         Me.Query.Where(Me.Query.LastName = lastName)
//        ///     End If
//        ///     If Not [String].IsNullOrEmpty(firstName) Then
//        ///         Me.Query.Where(Me.Query.FirstName = firstName)
//        ///     End If
//        /// 
//        ///     Return Me.Query.Load()
//        /// End Function
//        /// </code>
//        /// </remarks>
//        public void QueryReset()
//        {
//            this.query = null;
//        }

//        /// <summary>
//        /// Used to custom load a Join query.
//        /// Returns true if at least one row is loaded.
//        /// For an entity, an exception will be thrown
//        /// if more than one row is loaded.
//        /// </summary>
//        /// <remarks>
//        /// Provides support for InnerJoin, LeftJoin,
//        /// RightJoin, and FullJoin. You must provide an alias
//        /// for each query when instantiating them.
//        /// <code>
//        /// EmployeeCollection collection = new EmployeeCollection();
//        /// 
//        /// EmployeeQuery emp = new EmployeeQuery("eq");
//        /// CustomerQuery cust = new CustomerQuery("cq");
//        /// 
//        /// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName);
//        /// emp.LeftJoin(cust).On(emp.EmployeeID == cust.StaffAssigned);
//        /// 
//        /// collection.Load(emp);
//        /// </code>
//        /// <code lang="vbnet">
//        /// Dim collection As New EmployeeCollection()
//        /// 
//        /// Dim emp As New EmployeeQuery("eq")
//        /// Dim cust As New CustomerQuery("cq")
//        /// 
//        /// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName)
//        /// emp.LeftJoin(cust).On(emp.EmployeeID = cust.StaffAssigned)
//        /// 
//        /// collection.Load(emp)
//        /// </code>
//        /// </remarks>
//        /// <param name="query">The query object instance name.</param>
//        /// <returns>True if at least one record was loaded.</returns>
//        public bool Load(ServiceUnitBookingFormQuery query)
//        {
//            this.query = query;
//            base.InitQuery(this.query);
//            return this.Query.Load();
//        }

//        private ServiceUnitBookingFormQuery query;
//    }

//    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
//    [Serializable]
//    public partial class ServiceUnitBookingFormQuery : esServiceUnitBookingFormQuery
//    {
//        public ServiceUnitBookingFormQuery()
//        {

//        }

//        public ServiceUnitBookingFormQuery(string joinAlias)
//        {
//            this.es.JoinAlias = joinAlias;
//        }

//        override protected string GetQueryName()
//        {
//            return "ServiceUnitBookingFormQuery";
//        }
//    }

//    [Serializable]
//    public partial class ServiceUnitBookingFormMetadata : esMetadata, IMetadata
//    {
//        #region Protected Constructor
//        protected ServiceUnitBookingFormMetadata()
//        {
//            _columns = new esColumnMetadataCollection();
//            esColumnMetadata c;

//            c = new esColumnMetadata(ServiceUnitBookingFormMetadata.ColumnNames.BookingNo, 0, typeof(System.String), esSystemType.String);
//            c.PropertyName = ServiceUnitBookingFormMetadata.PropertyNames.BookingNo;
//            c.IsInPrimaryKey = true;
//            c.CharacterMaxLength = 20;
//            _columns.Add(c);

//            c = new esColumnMetadata(ServiceUnitBookingFormMetadata.ColumnNames.QuestionFormID, 1, typeof(System.String), esSystemType.String);
//            c.PropertyName = ServiceUnitBookingFormMetadata.PropertyNames.QuestionFormID;
//            c.IsInPrimaryKey = true;
//            c.CharacterMaxLength = 10;
//            _columns.Add(c);

//            c = new esColumnMetadata(ServiceUnitBookingFormMetadata.ColumnNames.TransactionNo, 2, typeof(System.String), esSystemType.String);
//            c.PropertyName = ServiceUnitBookingFormMetadata.PropertyNames.TransactionNo;
//            c.CharacterMaxLength = 20;
//            _columns.Add(c);

//            c = new esColumnMetadata(ServiceUnitBookingFormMetadata.ColumnNames.CreatedDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
//            c.PropertyName = ServiceUnitBookingFormMetadata.PropertyNames.CreatedDateTime;
//            c.IsNullable = true;
//            _columns.Add(c);

//            c = new esColumnMetadata(ServiceUnitBookingFormMetadata.ColumnNames.CreatedByUserID, 4, typeof(System.String), esSystemType.String);
//            c.PropertyName = ServiceUnitBookingFormMetadata.PropertyNames.CreatedByUserID;
//            c.CharacterMaxLength = 40;
//            c.IsNullable = true;
//            _columns.Add(c);

//            c = new esColumnMetadata(ServiceUnitBookingFormMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
//            c.PropertyName = ServiceUnitBookingFormMetadata.PropertyNames.LastUpdateDateTime;
//            c.IsNullable = true;
//            _columns.Add(c);

//            c = new esColumnMetadata(ServiceUnitBookingFormMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
//            c.PropertyName = ServiceUnitBookingFormMetadata.PropertyNames.LastUpdateByUserID;
//            c.CharacterMaxLength = 40;
//            c.IsNullable = true;
//            _columns.Add(c);


//        }
//        #endregion

//        static public ServiceUnitBookingFormMetadata Meta()
//        {
//            return meta;
//        }

//        public Guid DataID
//        {
//            get { return base._dataID; }
//        }

//        public bool MultiProviderMode
//        {
//            get { return false; }
//        }

//        public esColumnMetadataCollection Columns
//        {
//            get { return base._columns; }
//        }

//        #region ColumnNames
//        public class ColumnNames
//        {
//            public const string BookingNo = "BookingNo";
//            public const string QuestionFormID = "QuestionFormID";
//            public const string TransactionNo = "TransactionNo";
//            public const string CreatedDateTime = "CreatedDateTime";
//            public const string CreatedByUserID = "CreatedByUserID";
//            public const string LastUpdateDateTime = "LastUpdateDateTime";
//            public const string LastUpdateByUserID = "LastUpdateByUserID";
//        }
//        #endregion

//        #region PropertyNames
//        public class PropertyNames
//        {
//            public const string BookingNo = "BookingNo";
//            public const string QuestionFormID = "QuestionFormID";
//            public const string TransactionNo = "TransactionNo";
//            public const string CreatedDateTime = "CreatedDateTime";
//            public const string CreatedByUserID = "CreatedByUserID";
//            public const string LastUpdateDateTime = "LastUpdateDateTime";
//            public const string LastUpdateByUserID = "LastUpdateByUserID";
//        }
//        #endregion

//        public esProviderSpecificMetadata GetProviderMetadata(string mapName)
//        {
//            MapToMeta mapMethod = mapDelegates[mapName];

//            if (mapMethod != null)
//                return mapMethod(mapName);
//            else
//                return null;
//        }

//        #region MAP esDefault

//        static private int RegisterDelegateesDefault()
//        {
//            // This is only executed once per the life of the application
//            lock (typeof(ServiceUnitBookingFormMetadata))
//            {
//                if (ServiceUnitBookingFormMetadata.mapDelegates == null)
//                {
//                    ServiceUnitBookingFormMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
//                }

//                if (ServiceUnitBookingFormMetadata.meta == null)
//                {
//                    ServiceUnitBookingFormMetadata.meta = new ServiceUnitBookingFormMetadata();
//                }

//                MapToMeta mapMethod = new MapToMeta(meta.esDefault);
//                mapDelegates.Add("esDefault", mapMethod);
//                mapMethod("esDefault");
//            }
//            return 0;
//        }

//        private esProviderSpecificMetadata esDefault(string mapName)
//        {
//            if (!_providerMetadataMaps.ContainsKey(mapName))
//            {
//                esProviderSpecificMetadata meta = new esProviderSpecificMetadata();

//                meta.AddTypeMap("BookingNo", new esTypeMap("varchar", "System.String"));
//                meta.AddTypeMap("QuestionFormID", new esTypeMap("varchar", "System.String"));
//                meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
//                meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
//                meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
//                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
//                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


//                meta.Source = "ServiceUnitBookingForm";
//                meta.Destination = "ServiceUnitBookingForm";
//                meta.spInsert = "proc_ServiceUnitBookingFormInsert";
//                meta.spUpdate = "proc_ServiceUnitBookingFormUpdate";
//                meta.spDelete = "proc_ServiceUnitBookingFormDelete";
//                meta.spLoadAll = "proc_ServiceUnitBookingFormLoadAll";
//                meta.spLoadByPrimaryKey = "proc_ServiceUnitBookingFormLoadByPrimaryKey";

//                this._providerMetadataMaps["esDefault"] = meta;
//            }

//            return this._providerMetadataMaps["esDefault"];
//        }

//        #endregion

//        static private ServiceUnitBookingFormMetadata meta;
//        static protected Dictionary<string, MapToMeta> mapDelegates;
//        static private int _esDefault = RegisterDelegateesDefault();
//    }

//}
