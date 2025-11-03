/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 8/7/2017 10:18:23 AM
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
    abstract public class esRegistrationApproximateCoverageDetailCollection : esEntityCollectionWAuditLog
    {
        public esRegistrationApproximateCoverageDetailCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "RegistrationApproximateCoverageDetailCollection";
        }

        #region Query Logic
        protected void InitQuery(esRegistrationApproximateCoverageDetailQuery query)
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
            this.InitQuery(query as esRegistrationApproximateCoverageDetailQuery);
        }
        #endregion

        virtual public RegistrationApproximateCoverageDetail DetachEntity(RegistrationApproximateCoverageDetail entity)
        {
            return base.DetachEntity(entity) as RegistrationApproximateCoverageDetail;
        }

        virtual public RegistrationApproximateCoverageDetail AttachEntity(RegistrationApproximateCoverageDetail entity)
        {
            return base.AttachEntity(entity) as RegistrationApproximateCoverageDetail;
        }

        virtual public void Combine(RegistrationApproximateCoverageDetailCollection collection)
        {
            base.Combine(collection);
        }

        new public RegistrationApproximateCoverageDetail this[int index]
        {
            get
            {
                return base[index] as RegistrationApproximateCoverageDetail;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(RegistrationApproximateCoverageDetail);
        }
    }

    [Serializable]
    abstract public class esRegistrationApproximateCoverageDetail : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esRegistrationApproximateCoverageDetailQuery GetDynamicQuery()
        {
            return null;
        }

        public esRegistrationApproximateCoverageDetail()
        {
        }

        public esRegistrationApproximateCoverageDetail(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String registrationNo, String classID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(registrationNo, classID);
            else
                return LoadByPrimaryKeyStoredProcedure(registrationNo, classID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String registrationNo, String classID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(registrationNo, classID);
            else
                return LoadByPrimaryKeyStoredProcedure(registrationNo, classID);
        }

        private bool LoadByPrimaryKeyDynamic(String registrationNo, String classID)
        {
            esRegistrationApproximateCoverageDetailQuery query = this.GetDynamicQuery();
            query.Where(query.RegistrationNo == registrationNo, query.ClassID == classID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String registrationNo, String classID)
        {
            esParameters parms = new esParameters();
            parms.Add("RegistrationNo", registrationNo);
            parms.Add("ClassID", classID);
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
                        case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
                        case "ClassID": this.str.ClassID = (string)value; break;
                        case "CoverageAmount": this.str.CoverageAmount = (string)value; break;
                        case "CalculatedAmount": this.str.CalculatedAmount = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "CoverageAmount":

                            if (value == null || value is System.Decimal)
                                this.CoverageAmount = (System.Decimal?)value;
                            break;
                        case "CalculatedAmount":

                            if (value == null || value is System.Decimal)
                                this.CalculatedAmount = (System.Decimal?)value;
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
        /// Maps to RegistrationApproximateCoverageDetail.RegistrationNo
        /// </summary>
        virtual public System.String RegistrationNo
        {
            get
            {
                return base.GetSystemString(RegistrationApproximateCoverageDetailMetadata.ColumnNames.RegistrationNo);
            }

            set
            {
                base.SetSystemString(RegistrationApproximateCoverageDetailMetadata.ColumnNames.RegistrationNo, value);
            }
        }
        /// <summary>
        /// Maps to RegistrationApproximateCoverageDetail.ClassID
        /// </summary>
        virtual public System.String ClassID
        {
            get
            {
                return base.GetSystemString(RegistrationApproximateCoverageDetailMetadata.ColumnNames.ClassID);
            }

            set
            {
                base.SetSystemString(RegistrationApproximateCoverageDetailMetadata.ColumnNames.ClassID, value);
            }
        }
        /// <summary>
        /// Maps to RegistrationApproximateCoverageDetail.CoverageAmount
        /// </summary>
        virtual public System.Decimal? CoverageAmount
        {
            get
            {
                return base.GetSystemDecimal(RegistrationApproximateCoverageDetailMetadata.ColumnNames.CoverageAmount);
            }

            set
            {
                base.SetSystemDecimal(RegistrationApproximateCoverageDetailMetadata.ColumnNames.CoverageAmount, value);
            }
        }
        /// <summary>
        /// Maps to RegistrationApproximateCoverageDetail.CalculatedAmount
        /// </summary>
        virtual public System.Decimal? CalculatedAmount
        {
            get
            {
                return base.GetSystemDecimal(RegistrationApproximateCoverageDetailMetadata.ColumnNames.CalculatedAmount);
            }

            set
            {
                base.SetSystemDecimal(RegistrationApproximateCoverageDetailMetadata.ColumnNames.CalculatedAmount, value);
            }
        }
        /// <summary>
        /// Maps to RegistrationApproximateCoverageDetail.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(RegistrationApproximateCoverageDetailMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(RegistrationApproximateCoverageDetailMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to RegistrationApproximateCoverageDetail.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(RegistrationApproximateCoverageDetailMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(RegistrationApproximateCoverageDetailMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esRegistrationApproximateCoverageDetail entity)
            {
                this.entity = entity;
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
            public System.String ClassID
            {
                get
                {
                    System.String data = entity.ClassID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ClassID = null;
                    else entity.ClassID = Convert.ToString(value);
                }
            }
            public System.String CoverageAmount
            {
                get
                {
                    System.Decimal? data = entity.CoverageAmount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CoverageAmount = null;
                    else entity.CoverageAmount = Convert.ToDecimal(value);
                }
            }
            public System.String CalculatedAmount
            {
                get
                {
                    System.Decimal? data = entity.CalculatedAmount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CalculatedAmount = null;
                    else entity.CalculatedAmount = Convert.ToDecimal(value);
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
            private esRegistrationApproximateCoverageDetail entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esRegistrationApproximateCoverageDetailQuery query)
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
                throw new Exception("esRegistrationApproximateCoverageDetail can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class RegistrationApproximateCoverageDetail : esRegistrationApproximateCoverageDetail
    {
    }

    [Serializable]
    abstract public class esRegistrationApproximateCoverageDetailQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return RegistrationApproximateCoverageDetailMetadata.Meta();
            }
        }

        public esQueryItem RegistrationNo
        {
            get
            {
                return new esQueryItem(this, RegistrationApproximateCoverageDetailMetadata.ColumnNames.RegistrationNo, esSystemType.String);
            }
        }

        public esQueryItem ClassID
        {
            get
            {
                return new esQueryItem(this, RegistrationApproximateCoverageDetailMetadata.ColumnNames.ClassID, esSystemType.String);
            }
        }

        public esQueryItem CoverageAmount
        {
            get
            {
                return new esQueryItem(this, RegistrationApproximateCoverageDetailMetadata.ColumnNames.CoverageAmount, esSystemType.Decimal);
            }
        }

        public esQueryItem CalculatedAmount
        {
            get
            {
                return new esQueryItem(this, RegistrationApproximateCoverageDetailMetadata.ColumnNames.CalculatedAmount, esSystemType.Decimal);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, RegistrationApproximateCoverageDetailMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, RegistrationApproximateCoverageDetailMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("RegistrationApproximateCoverageDetailCollection")]
    public partial class RegistrationApproximateCoverageDetailCollection : esRegistrationApproximateCoverageDetailCollection, IEnumerable<RegistrationApproximateCoverageDetail>
    {
        public RegistrationApproximateCoverageDetailCollection()
        {

        }

        public static implicit operator List<RegistrationApproximateCoverageDetail>(RegistrationApproximateCoverageDetailCollection coll)
        {
            List<RegistrationApproximateCoverageDetail> list = new List<RegistrationApproximateCoverageDetail>();

            foreach (RegistrationApproximateCoverageDetail emp in coll)
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
                return RegistrationApproximateCoverageDetailMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new RegistrationApproximateCoverageDetailQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new RegistrationApproximateCoverageDetail(row);
        }

        override protected esEntity CreateEntity()
        {
            return new RegistrationApproximateCoverageDetail();
        }

        #endregion

        [BrowsableAttribute(false)]
        public RegistrationApproximateCoverageDetailQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new RegistrationApproximateCoverageDetailQuery();
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
        public bool Load(RegistrationApproximateCoverageDetailQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public RegistrationApproximateCoverageDetail AddNew()
        {
            RegistrationApproximateCoverageDetail entity = base.AddNewEntity() as RegistrationApproximateCoverageDetail;

            return entity;
        }
        public RegistrationApproximateCoverageDetail FindByPrimaryKey(String registrationNo, String classID)
        {
            return base.FindByPrimaryKey(registrationNo, classID) as RegistrationApproximateCoverageDetail;
        }

        #region IEnumerable< RegistrationApproximateCoverageDetail> Members

        IEnumerator<RegistrationApproximateCoverageDetail> IEnumerable<RegistrationApproximateCoverageDetail>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as RegistrationApproximateCoverageDetail;
            }
        }

        #endregion

        private RegistrationApproximateCoverageDetailQuery query;
    }


    /// <summary>
    /// Encapsulates the 'RegistrationApproximateCoverageDetail' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("RegistrationApproximateCoverageDetail ({RegistrationNo, ClassID})")]
    [Serializable]
    public partial class RegistrationApproximateCoverageDetail : esRegistrationApproximateCoverageDetail
    {
        public RegistrationApproximateCoverageDetail()
        {
        }

        public RegistrationApproximateCoverageDetail(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return RegistrationApproximateCoverageDetailMetadata.Meta();
            }
        }

        override protected esRegistrationApproximateCoverageDetailQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new RegistrationApproximateCoverageDetailQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public RegistrationApproximateCoverageDetailQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new RegistrationApproximateCoverageDetailQuery();
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
        public bool Load(RegistrationApproximateCoverageDetailQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private RegistrationApproximateCoverageDetailQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class RegistrationApproximateCoverageDetailQuery : esRegistrationApproximateCoverageDetailQuery
    {
        public RegistrationApproximateCoverageDetailQuery()
        {

        }

        public RegistrationApproximateCoverageDetailQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "RegistrationApproximateCoverageDetailQuery";
        }
    }

    [Serializable]
    public partial class RegistrationApproximateCoverageDetailMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected RegistrationApproximateCoverageDetailMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(RegistrationApproximateCoverageDetailMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = RegistrationApproximateCoverageDetailMetadata.PropertyNames.RegistrationNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationApproximateCoverageDetailMetadata.ColumnNames.ClassID, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = RegistrationApproximateCoverageDetailMetadata.PropertyNames.ClassID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationApproximateCoverageDetailMetadata.ColumnNames.CoverageAmount, 2, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = RegistrationApproximateCoverageDetailMetadata.PropertyNames.CoverageAmount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationApproximateCoverageDetailMetadata.ColumnNames.CalculatedAmount, 3, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = RegistrationApproximateCoverageDetailMetadata.PropertyNames.CalculatedAmount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationApproximateCoverageDetailMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = RegistrationApproximateCoverageDetailMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationApproximateCoverageDetailMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = RegistrationApproximateCoverageDetailMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public RegistrationApproximateCoverageDetailMetadata Meta()
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
            public const string RegistrationNo = "RegistrationNo";
            public const string ClassID = "ClassID";
            public const string CoverageAmount = "CoverageAmount";
            public const string CalculatedAmount = "CalculatedAmount";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string RegistrationNo = "RegistrationNo";
            public const string ClassID = "ClassID";
            public const string CoverageAmount = "CoverageAmount";
            public const string CalculatedAmount = "CalculatedAmount";
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
            lock (typeof(RegistrationApproximateCoverageDetailMetadata))
            {
                if (RegistrationApproximateCoverageDetailMetadata.mapDelegates == null)
                {
                    RegistrationApproximateCoverageDetailMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (RegistrationApproximateCoverageDetailMetadata.meta == null)
                {
                    RegistrationApproximateCoverageDetailMetadata.meta = new RegistrationApproximateCoverageDetailMetadata();
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

                meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ClassID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("CoverageAmount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("CalculatedAmount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "RegistrationApproximateCoverageDetail";
                meta.Destination = "RegistrationApproximateCoverageDetail";
                meta.spInsert = "proc_RegistrationApproximateCoverageDetailInsert";
                meta.spUpdate = "proc_RegistrationApproximateCoverageDetailUpdate";
                meta.spDelete = "proc_RegistrationApproximateCoverageDetailDelete";
                meta.spLoadAll = "proc_RegistrationApproximateCoverageDetailLoadAll";
                meta.spLoadByPrimaryKey = "proc_RegistrationApproximateCoverageDetailLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private RegistrationApproximateCoverageDetailMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
