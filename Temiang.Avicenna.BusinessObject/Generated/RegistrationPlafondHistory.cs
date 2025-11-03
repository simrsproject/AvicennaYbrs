/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/9/2016 2:18:50 PM
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
    abstract public class esRegistrationPlafondHistoryCollection : esEntityCollectionWAuditLog
    {
        public esRegistrationPlafondHistoryCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "RegistrationPlafondHistoryCollection";
        }

        #region Query Logic
        protected void InitQuery(esRegistrationPlafondHistoryQuery query)
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
            this.InitQuery(query as esRegistrationPlafondHistoryQuery);
        }
        #endregion

        virtual public RegistrationPlafondHistory DetachEntity(RegistrationPlafondHistory entity)
        {
            return base.DetachEntity(entity) as RegistrationPlafondHistory;
        }

        virtual public RegistrationPlafondHistory AttachEntity(RegistrationPlafondHistory entity)
        {
            return base.AttachEntity(entity) as RegistrationPlafondHistory;
        }

        virtual public void Combine(RegistrationPlafondHistoryCollection collection)
        {
            base.Combine(collection);
        }

        new public RegistrationPlafondHistory this[int index]
        {
            get
            {
                return base[index] as RegistrationPlafondHistory;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(RegistrationPlafondHistory);
        }
    }

    [Serializable]
    abstract public class esRegistrationPlafondHistory : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esRegistrationPlafondHistoryQuery GetDynamicQuery()
        {
            return null;
        }

        public esRegistrationPlafondHistory()
        {
        }

        public esRegistrationPlafondHistory(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(Int64 historyID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(historyID);
            else
                return LoadByPrimaryKeyStoredProcedure(historyID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int64 historyID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(historyID);
            else
                return LoadByPrimaryKeyStoredProcedure(historyID);
        }

        private bool LoadByPrimaryKeyDynamic(Int64 historyID)
        {
            esRegistrationPlafondHistoryQuery query = this.GetDynamicQuery();
            query.Where(query.HistoryID == historyID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(Int64 historyID)
        {
            esParameters parms = new esParameters();
            parms.Add("HistoryID", historyID);
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
                        case "HistoryID": this.str.HistoryID = (string)value; break;
                        case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
                        case "GuarantorID": this.str.GuarantorID = (string)value; break;
                        case "PlafondAmount": this.str.PlafondAmount = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "HistoryID":

                            if (value == null || value is System.Int64)
                                this.HistoryID = (System.Int64?)value;
                            break;
                        case "PlafondAmount":

                            if (value == null || value is System.Decimal)
                                this.PlafondAmount = (System.Decimal?)value;
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
        /// Maps to RegistrationPlafondHistory.HistoryID
        /// </summary>
        virtual public System.Int64? HistoryID
        {
            get
            {
                return base.GetSystemInt64(RegistrationPlafondHistoryMetadata.ColumnNames.HistoryID);
            }

            set
            {
                base.SetSystemInt64(RegistrationPlafondHistoryMetadata.ColumnNames.HistoryID, value);
            }
        }
        /// <summary>
        /// Maps to RegistrationPlafondHistory.RegistrationNo
        /// </summary>
        virtual public System.String RegistrationNo
        {
            get
            {
                return base.GetSystemString(RegistrationPlafondHistoryMetadata.ColumnNames.RegistrationNo);
            }

            set
            {
                base.SetSystemString(RegistrationPlafondHistoryMetadata.ColumnNames.RegistrationNo, value);
            }
        }
        /// <summary>
        /// Maps to RegistrationPlafondHistory.GuarantorID
        /// </summary>
        virtual public System.String GuarantorID
        {
            get
            {
                return base.GetSystemString(RegistrationPlafondHistoryMetadata.ColumnNames.GuarantorID);
            }

            set
            {
                base.SetSystemString(RegistrationPlafondHistoryMetadata.ColumnNames.GuarantorID, value);
            }
        }
        /// <summary>
        /// Maps to RegistrationPlafondHistory.PlafondAmount
        /// </summary>
        virtual public System.Decimal? PlafondAmount
        {
            get
            {
                return base.GetSystemDecimal(RegistrationPlafondHistoryMetadata.ColumnNames.PlafondAmount);
            }

            set
            {
                base.SetSystemDecimal(RegistrationPlafondHistoryMetadata.ColumnNames.PlafondAmount, value);
            }
        }
        /// <summary>
        /// Maps to RegistrationPlafondHistory.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(RegistrationPlafondHistoryMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(RegistrationPlafondHistoryMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to RegistrationPlafondHistory.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(RegistrationPlafondHistoryMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(RegistrationPlafondHistoryMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esRegistrationPlafondHistory entity)
            {
                this.entity = entity;
            }
            public System.String HistoryID
            {
                get
                {
                    System.Int64? data = entity.HistoryID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.HistoryID = null;
                    else entity.HistoryID = Convert.ToInt64(value);
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
            public System.String GuarantorID
            {
                get
                {
                    System.String data = entity.GuarantorID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.GuarantorID = null;
                    else entity.GuarantorID = Convert.ToString(value);
                }
            }
            public System.String PlafondAmount
            {
                get
                {
                    System.Decimal? data = entity.PlafondAmount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PlafondAmount = null;
                    else entity.PlafondAmount = Convert.ToDecimal(value);
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
            private esRegistrationPlafondHistory entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esRegistrationPlafondHistoryQuery query)
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
                throw new Exception("esRegistrationPlafondHistory can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class RegistrationPlafondHistory : esRegistrationPlafondHistory
    {
    }

    [Serializable]
    abstract public class esRegistrationPlafondHistoryQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return RegistrationPlafondHistoryMetadata.Meta();
            }
        }

        public esQueryItem HistoryID
        {
            get
            {
                return new esQueryItem(this, RegistrationPlafondHistoryMetadata.ColumnNames.HistoryID, esSystemType.Int64);
            }
        }

        public esQueryItem RegistrationNo
        {
            get
            {
                return new esQueryItem(this, RegistrationPlafondHistoryMetadata.ColumnNames.RegistrationNo, esSystemType.String);
            }
        }

        public esQueryItem GuarantorID
        {
            get
            {
                return new esQueryItem(this, RegistrationPlafondHistoryMetadata.ColumnNames.GuarantorID, esSystemType.String);
            }
        }

        public esQueryItem PlafondAmount
        {
            get
            {
                return new esQueryItem(this, RegistrationPlafondHistoryMetadata.ColumnNames.PlafondAmount, esSystemType.Decimal);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, RegistrationPlafondHistoryMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, RegistrationPlafondHistoryMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("RegistrationPlafondHistoryCollection")]
    public partial class RegistrationPlafondHistoryCollection : esRegistrationPlafondHistoryCollection, IEnumerable<RegistrationPlafondHistory>
    {
        public RegistrationPlafondHistoryCollection()
        {

        }

        public static implicit operator List<RegistrationPlafondHistory>(RegistrationPlafondHistoryCollection coll)
        {
            List<RegistrationPlafondHistory> list = new List<RegistrationPlafondHistory>();

            foreach (RegistrationPlafondHistory emp in coll)
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
                return RegistrationPlafondHistoryMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new RegistrationPlafondHistoryQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new RegistrationPlafondHistory(row);
        }

        override protected esEntity CreateEntity()
        {
            return new RegistrationPlafondHistory();
        }

        #endregion

        [BrowsableAttribute(false)]
        public RegistrationPlafondHistoryQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new RegistrationPlafondHistoryQuery();
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
        public bool Load(RegistrationPlafondHistoryQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public RegistrationPlafondHistory AddNew()
        {
            RegistrationPlafondHistory entity = base.AddNewEntity() as RegistrationPlafondHistory;

            return entity;
        }
        public RegistrationPlafondHistory FindByPrimaryKey(Int64 historyID)
        {
            return base.FindByPrimaryKey(historyID) as RegistrationPlafondHistory;
        }

        #region IEnumerable< RegistrationPlafondHistory> Members

        IEnumerator<RegistrationPlafondHistory> IEnumerable<RegistrationPlafondHistory>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as RegistrationPlafondHistory;
            }
        }

        #endregion

        private RegistrationPlafondHistoryQuery query;
    }


    /// <summary>
    /// Encapsulates the 'RegistrationPlafondHistory' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("RegistrationPlafondHistory ({HistoryID})")]
    [Serializable]
    public partial class RegistrationPlafondHistory : esRegistrationPlafondHistory
    {
        public RegistrationPlafondHistory()
        {
        }

        public RegistrationPlafondHistory(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return RegistrationPlafondHistoryMetadata.Meta();
            }
        }

        override protected esRegistrationPlafondHistoryQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new RegistrationPlafondHistoryQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public RegistrationPlafondHistoryQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new RegistrationPlafondHistoryQuery();
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
        public bool Load(RegistrationPlafondHistoryQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private RegistrationPlafondHistoryQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class RegistrationPlafondHistoryQuery : esRegistrationPlafondHistoryQuery
    {
        public RegistrationPlafondHistoryQuery()
        {

        }

        public RegistrationPlafondHistoryQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "RegistrationPlafondHistoryQuery";
        }
    }

    [Serializable]
    public partial class RegistrationPlafondHistoryMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected RegistrationPlafondHistoryMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(RegistrationPlafondHistoryMetadata.ColumnNames.HistoryID, 0, typeof(System.Int64), esSystemType.Int64);
            c.PropertyName = RegistrationPlafondHistoryMetadata.PropertyNames.HistoryID;
            c.IsInPrimaryKey = true;
            c.IsAutoIncrement = true;
            c.NumericPrecision = 19;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationPlafondHistoryMetadata.ColumnNames.RegistrationNo, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = RegistrationPlafondHistoryMetadata.PropertyNames.RegistrationNo;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationPlafondHistoryMetadata.ColumnNames.GuarantorID, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = RegistrationPlafondHistoryMetadata.PropertyNames.GuarantorID;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationPlafondHistoryMetadata.ColumnNames.PlafondAmount, 3, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = RegistrationPlafondHistoryMetadata.PropertyNames.PlafondAmount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationPlafondHistoryMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = RegistrationPlafondHistoryMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationPlafondHistoryMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = RegistrationPlafondHistoryMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public RegistrationPlafondHistoryMetadata Meta()
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
            public const string HistoryID = "HistoryID";
            public const string RegistrationNo = "RegistrationNo";
            public const string GuarantorID = "GuarantorID";
            public const string PlafondAmount = "PlafondAmount";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string HistoryID = "HistoryID";
            public const string RegistrationNo = "RegistrationNo";
            public const string GuarantorID = "GuarantorID";
            public const string PlafondAmount = "PlafondAmount";
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
            lock (typeof(RegistrationPlafondHistoryMetadata))
            {
                if (RegistrationPlafondHistoryMetadata.mapDelegates == null)
                {
                    RegistrationPlafondHistoryMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (RegistrationPlafondHistoryMetadata.meta == null)
                {
                    RegistrationPlafondHistoryMetadata.meta = new RegistrationPlafondHistoryMetadata();
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

                meta.AddTypeMap("HistoryID", new esTypeMap("bigint", "System.Int64"));
                meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("GuarantorID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("PlafondAmount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "RegistrationPlafondHistory";
                meta.Destination = "RegistrationPlafondHistory";
                meta.spInsert = "proc_RegistrationPlafondHistoryInsert";
                meta.spUpdate = "proc_RegistrationPlafondHistoryUpdate";
                meta.spDelete = "proc_RegistrationPlafondHistoryDelete";
                meta.spLoadAll = "proc_RegistrationPlafondHistoryLoadAll";
                meta.spLoadByPrimaryKey = "proc_RegistrationPlafondHistoryLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private RegistrationPlafondHistoryMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
