/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 6/27/2016 2:55:39 PM
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
    abstract public class esParamedicLeaveExeptionUnitCollection : esEntityCollectionWAuditLog
    {
        public esParamedicLeaveExeptionUnitCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "ParamedicLeaveExeptionUnitCollection";
        }

        #region Query Logic
        protected void InitQuery(esParamedicLeaveExeptionUnitQuery query)
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
            this.InitQuery(query as esParamedicLeaveExeptionUnitQuery);
        }
        #endregion

        virtual public ParamedicLeaveExeptionUnit DetachEntity(ParamedicLeaveExeptionUnit entity)
        {
            return base.DetachEntity(entity) as ParamedicLeaveExeptionUnit;
        }

        virtual public ParamedicLeaveExeptionUnit AttachEntity(ParamedicLeaveExeptionUnit entity)
        {
            return base.AttachEntity(entity) as ParamedicLeaveExeptionUnit;
        }

        virtual public void Combine(ParamedicLeaveExeptionUnitCollection collection)
        {
            base.Combine(collection);
        }

        new public ParamedicLeaveExeptionUnit this[int index]
        {
            get
            {
                return base[index] as ParamedicLeaveExeptionUnit;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(ParamedicLeaveExeptionUnit);
        }
    }

    [Serializable]
    abstract public class esParamedicLeaveExeptionUnit : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esParamedicLeaveExeptionUnitQuery GetDynamicQuery()
        {
            return null;
        }

        public esParamedicLeaveExeptionUnit()
        {
        }

        public esParamedicLeaveExeptionUnit(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String transactionNo, String serviceUnitID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(transactionNo, serviceUnitID);
            else
                return LoadByPrimaryKeyStoredProcedure(transactionNo, serviceUnitID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String transactionNo, String serviceUnitID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(transactionNo, serviceUnitID);
            else
                return LoadByPrimaryKeyStoredProcedure(transactionNo, serviceUnitID);
        }

        private bool LoadByPrimaryKeyDynamic(String transactionNo, String serviceUnitID)
        {
            esParamedicLeaveExeptionUnitQuery query = this.GetDynamicQuery();
            query.Where(query.TransactionNo == transactionNo, query.ServiceUnitID == serviceUnitID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String transactionNo, String serviceUnitID)
        {
            esParameters parms = new esParameters();
            parms.Add("TransactionNo", transactionNo);
            parms.Add("ServiceUnitID", serviceUnitID);
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
                        case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
                        case "StartTime": this.str.StartTime = (string)value; break;
                        case "EndTime": this.str.EndTime = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
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
        /// Maps to ParamedicLeaveExeptionUnit.TransactionNo
        /// </summary>
        virtual public System.String TransactionNo
        {
            get
            {
                return base.GetSystemString(ParamedicLeaveExeptionUnitMetadata.ColumnNames.TransactionNo);
            }

            set
            {
                base.SetSystemString(ParamedicLeaveExeptionUnitMetadata.ColumnNames.TransactionNo, value);
            }
        }
        /// <summary>
        /// Maps to ParamedicLeaveExeptionUnit.ServiceUnitID
        /// </summary>
        virtual public System.String ServiceUnitID
        {
            get
            {
                return base.GetSystemString(ParamedicLeaveExeptionUnitMetadata.ColumnNames.ServiceUnitID);
            }

            set
            {
                base.SetSystemString(ParamedicLeaveExeptionUnitMetadata.ColumnNames.ServiceUnitID, value);
            }
        }
        /// <summary>
        /// Maps to ParamedicLeaveExeptionUnit.StartTime
        /// </summary>
        virtual public System.String StartTime
        {
            get
            {
                return base.GetSystemString(ParamedicLeaveExeptionUnitMetadata.ColumnNames.StartTime);
            }

            set
            {
                base.SetSystemString(ParamedicLeaveExeptionUnitMetadata.ColumnNames.StartTime, value);
            }
        }
        /// <summary>
        /// Maps to ParamedicLeaveExeptionUnit.EndTime
        /// </summary>
        virtual public System.String EndTime
        {
            get
            {
                return base.GetSystemString(ParamedicLeaveExeptionUnitMetadata.ColumnNames.EndTime);
            }

            set
            {
                base.SetSystemString(ParamedicLeaveExeptionUnitMetadata.ColumnNames.EndTime, value);
            }
        }
        /// <summary>
        /// Maps to ParamedicLeaveExeptionUnit.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(ParamedicLeaveExeptionUnitMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(ParamedicLeaveExeptionUnitMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to ParamedicLeaveExeptionUnit.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(ParamedicLeaveExeptionUnitMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(ParamedicLeaveExeptionUnitMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esParamedicLeaveExeptionUnit entity)
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
            public System.String StartTime
            {
                get
                {
                    System.String data = entity.StartTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.StartTime = null;
                    else entity.StartTime = Convert.ToString(value);
                }
            }
            public System.String EndTime
            {
                get
                {
                    System.String data = entity.EndTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.EndTime = null;
                    else entity.EndTime = Convert.ToString(value);
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
            private esParamedicLeaveExeptionUnit entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esParamedicLeaveExeptionUnitQuery query)
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
                throw new Exception("esParamedicLeaveExeptionUnit can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class ParamedicLeaveExeptionUnit : esParamedicLeaveExeptionUnit
    {
    }

    [Serializable]
    abstract public class esParamedicLeaveExeptionUnitQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return ParamedicLeaveExeptionUnitMetadata.Meta();
            }
        }

        public esQueryItem TransactionNo
        {
            get
            {
                return new esQueryItem(this, ParamedicLeaveExeptionUnitMetadata.ColumnNames.TransactionNo, esSystemType.String);
            }
        }

        public esQueryItem ServiceUnitID
        {
            get
            {
                return new esQueryItem(this, ParamedicLeaveExeptionUnitMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
            }
        }

        public esQueryItem StartTime
        {
            get
            {
                return new esQueryItem(this, ParamedicLeaveExeptionUnitMetadata.ColumnNames.StartTime, esSystemType.String);
            }
        }

        public esQueryItem EndTime
        {
            get
            {
                return new esQueryItem(this, ParamedicLeaveExeptionUnitMetadata.ColumnNames.EndTime, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, ParamedicLeaveExeptionUnitMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, ParamedicLeaveExeptionUnitMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("ParamedicLeaveExeptionUnitCollection")]
    public partial class ParamedicLeaveExeptionUnitCollection : esParamedicLeaveExeptionUnitCollection, IEnumerable<ParamedicLeaveExeptionUnit>
    {
        public ParamedicLeaveExeptionUnitCollection()
        {

        }

        public static implicit operator List<ParamedicLeaveExeptionUnit>(ParamedicLeaveExeptionUnitCollection coll)
        {
            List<ParamedicLeaveExeptionUnit> list = new List<ParamedicLeaveExeptionUnit>();

            foreach (ParamedicLeaveExeptionUnit emp in coll)
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
                return ParamedicLeaveExeptionUnitMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ParamedicLeaveExeptionUnitQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new ParamedicLeaveExeptionUnit(row);
        }

        override protected esEntity CreateEntity()
        {
            return new ParamedicLeaveExeptionUnit();
        }

        #endregion

        [BrowsableAttribute(false)]
        public ParamedicLeaveExeptionUnitQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ParamedicLeaveExeptionUnitQuery();
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
        public bool Load(ParamedicLeaveExeptionUnitQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public ParamedicLeaveExeptionUnit AddNew()
        {
            ParamedicLeaveExeptionUnit entity = base.AddNewEntity() as ParamedicLeaveExeptionUnit;

            return entity;
        }
        public ParamedicLeaveExeptionUnit FindByPrimaryKey(String transactionNo, String serviceUnitID)
        {
            return base.FindByPrimaryKey(transactionNo, serviceUnitID) as ParamedicLeaveExeptionUnit;
        }

        #region IEnumerable< ParamedicLeaveExeptionUnit> Members

        IEnumerator<ParamedicLeaveExeptionUnit> IEnumerable<ParamedicLeaveExeptionUnit>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as ParamedicLeaveExeptionUnit;
            }
        }

        #endregion

        private ParamedicLeaveExeptionUnitQuery query;
    }


    /// <summary>
    /// Encapsulates the 'ParamedicLeaveExeptionUnit' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("ParamedicLeaveExeptionUnit ({TransactionNo, ServiceUnitID})")]
    [Serializable]
    public partial class ParamedicLeaveExeptionUnit : esParamedicLeaveExeptionUnit
    {
        public ParamedicLeaveExeptionUnit()
        {
        }

        public ParamedicLeaveExeptionUnit(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return ParamedicLeaveExeptionUnitMetadata.Meta();
            }
        }

        override protected esParamedicLeaveExeptionUnitQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ParamedicLeaveExeptionUnitQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public ParamedicLeaveExeptionUnitQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ParamedicLeaveExeptionUnitQuery();
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
        public bool Load(ParamedicLeaveExeptionUnitQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private ParamedicLeaveExeptionUnitQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class ParamedicLeaveExeptionUnitQuery : esParamedicLeaveExeptionUnitQuery
    {
        public ParamedicLeaveExeptionUnitQuery()
        {

        }

        public ParamedicLeaveExeptionUnitQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "ParamedicLeaveExeptionUnitQuery";
        }
    }

    [Serializable]
    public partial class ParamedicLeaveExeptionUnitMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected ParamedicLeaveExeptionUnitMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(ParamedicLeaveExeptionUnitMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = ParamedicLeaveExeptionUnitMetadata.PropertyNames.TransactionNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicLeaveExeptionUnitMetadata.ColumnNames.ServiceUnitID, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = ParamedicLeaveExeptionUnitMetadata.PropertyNames.ServiceUnitID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicLeaveExeptionUnitMetadata.ColumnNames.StartTime, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = ParamedicLeaveExeptionUnitMetadata.PropertyNames.StartTime;
            c.CharacterMaxLength = 5;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicLeaveExeptionUnitMetadata.ColumnNames.EndTime, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = ParamedicLeaveExeptionUnitMetadata.PropertyNames.EndTime;
            c.CharacterMaxLength = 5;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicLeaveExeptionUnitMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ParamedicLeaveExeptionUnitMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicLeaveExeptionUnitMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = ParamedicLeaveExeptionUnitMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public ParamedicLeaveExeptionUnitMetadata Meta()
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
            public const string ServiceUnitID = "ServiceUnitID";
            public const string StartTime = "StartTime";
            public const string EndTime = "EndTime";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string TransactionNo = "TransactionNo";
            public const string ServiceUnitID = "ServiceUnitID";
            public const string StartTime = "StartTime";
            public const string EndTime = "EndTime";
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
            lock (typeof(ParamedicLeaveExeptionUnitMetadata))
            {
                if (ParamedicLeaveExeptionUnitMetadata.mapDelegates == null)
                {
                    ParamedicLeaveExeptionUnitMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (ParamedicLeaveExeptionUnitMetadata.meta == null)
                {
                    ParamedicLeaveExeptionUnitMetadata.meta = new ParamedicLeaveExeptionUnitMetadata();
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
                meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("StartTime", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("EndTime", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "ParamedicLeaveExeptionUnit";
                meta.Destination = "ParamedicLeaveExeptionUnit";
                meta.spInsert = "proc_ParamedicLeaveExeptionUnitInsert";
                meta.spUpdate = "proc_ParamedicLeaveExeptionUnitUpdate";
                meta.spDelete = "proc_ParamedicLeaveExeptionUnitDelete";
                meta.spLoadAll = "proc_ParamedicLeaveExeptionUnitLoadAll";
                meta.spLoadByPrimaryKey = "proc_ParamedicLeaveExeptionUnitLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private ParamedicLeaveExeptionUnitMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
