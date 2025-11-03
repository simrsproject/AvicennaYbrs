/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 10/17/2018 12:41:19 PM
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
    abstract public class esOvertimeDetailCollection : esEntityCollectionWAuditLog
    {
        public esOvertimeDetailCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "OvertimeDetailCollection";
        }

        #region Query Logic
        protected void InitQuery(esOvertimeDetailQuery query)
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
            this.InitQuery(query as esOvertimeDetailQuery);
        }
        #endregion

        virtual public OvertimeDetail DetachEntity(OvertimeDetail entity)
        {
            return base.DetachEntity(entity) as OvertimeDetail;
        }

        virtual public OvertimeDetail AttachEntity(OvertimeDetail entity)
        {
            return base.AttachEntity(entity) as OvertimeDetail;
        }

        virtual public void Combine(OvertimeDetailCollection collection)
        {
            base.Combine(collection);
        }

        new public OvertimeDetail this[int index]
        {
            get
            {
                return base[index] as OvertimeDetail;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(OvertimeDetail);
        }
    }

    [Serializable]
    abstract public class esOvertimeDetail : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esOvertimeDetailQuery GetDynamicQuery()
        {
            return null;
        }

        public esOvertimeDetail()
        {
        }

        public esOvertimeDetail(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(Int32 overtimeDetailID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(overtimeDetailID);
            else
                return LoadByPrimaryKeyStoredProcedure(overtimeDetailID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 overtimeDetailID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(overtimeDetailID);
            else
                return LoadByPrimaryKeyStoredProcedure(overtimeDetailID);
        }

        private bool LoadByPrimaryKeyDynamic(Int32 overtimeDetailID)
        {
            esOvertimeDetailQuery query = this.GetDynamicQuery();
            query.Where(query.OvertimeDetailID == overtimeDetailID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(Int32 overtimeDetailID)
        {
            esParameters parms = new esParameters();
            parms.Add("OvertimeDetailID", overtimeDetailID);
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
                        case "OvertimeDetailID": this.str.OvertimeDetailID = (string)value; break;
                        case "OvertimeID": this.str.OvertimeID = (string)value; break;
                        case "HourFrom": this.str.HourFrom = (string)value; break;
                        case "HourTo": this.str.HourTo = (string)value; break;
                        case "Value": this.str.Value = (string)value; break;
                        case "Formula": this.str.Formula = (string)value; break;
                        case "Notes": this.str.Notes = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "OvertimeDetailID":

                            if (value == null || value is System.Int32)
                                this.OvertimeDetailID = (System.Int32?)value;
                            break;
                        case "OvertimeID":

                            if (value == null || value is System.Int32)
                                this.OvertimeID = (System.Int32?)value;
                            break;
                        case "HourFrom":

                            if (value == null || value is System.Decimal)
                                this.HourFrom = (System.Decimal?)value;
                            break;
                        case "HourTo":

                            if (value == null || value is System.Decimal)
                                this.HourTo = (System.Decimal?)value;
                            break;
                        case "Value":

                            if (value == null || value is System.Decimal)
                                this.Value = (System.Decimal?)value;
                            break;
                        case "Formula":

                            if (value == null || value is System.Decimal)
                                this.Formula = (System.Decimal?)value;
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
        /// Maps to OvertimeDetail.OvertimeDetailID
        /// </summary>
        virtual public System.Int32? OvertimeDetailID
        {
            get
            {
                return base.GetSystemInt32(OvertimeDetailMetadata.ColumnNames.OvertimeDetailID);
            }

            set
            {
                base.SetSystemInt32(OvertimeDetailMetadata.ColumnNames.OvertimeDetailID, value);
            }
        }
        /// <summary>
        /// Maps to OvertimeDetail.OvertimeID
        /// </summary>
        virtual public System.Int32? OvertimeID
        {
            get
            {
                return base.GetSystemInt32(OvertimeDetailMetadata.ColumnNames.OvertimeID);
            }

            set
            {
                base.SetSystemInt32(OvertimeDetailMetadata.ColumnNames.OvertimeID, value);
            }
        }
        /// <summary>
        /// Maps to OvertimeDetail.HourFrom
        /// </summary>
        virtual public System.Decimal? HourFrom
        {
            get
            {
                return base.GetSystemDecimal(OvertimeDetailMetadata.ColumnNames.HourFrom);
            }

            set
            {
                base.SetSystemDecimal(OvertimeDetailMetadata.ColumnNames.HourFrom, value);
            }
        }
        /// <summary>
        /// Maps to OvertimeDetail.HourTo
        /// </summary>
        virtual public System.Decimal? HourTo
        {
            get
            {
                return base.GetSystemDecimal(OvertimeDetailMetadata.ColumnNames.HourTo);
            }

            set
            {
                base.SetSystemDecimal(OvertimeDetailMetadata.ColumnNames.HourTo, value);
            }
        }
        /// <summary>
        /// Maps to OvertimeDetail.Value
        /// </summary>
        virtual public System.Decimal? Value
        {
            get
            {
                return base.GetSystemDecimal(OvertimeDetailMetadata.ColumnNames.Value);
            }

            set
            {
                base.SetSystemDecimal(OvertimeDetailMetadata.ColumnNames.Value, value);
            }
        }
        /// <summary>
        /// Maps to OvertimeDetail.Formula
        /// </summary>
        virtual public System.Decimal? Formula
        {
            get
            {
                return base.GetSystemDecimal(OvertimeDetailMetadata.ColumnNames.Formula);
            }

            set
            {
                base.SetSystemDecimal(OvertimeDetailMetadata.ColumnNames.Formula, value);
            }
        }
        /// <summary>
        /// Maps to OvertimeDetail.Notes
        /// </summary>
        virtual public System.String Notes
        {
            get
            {
                return base.GetSystemString(OvertimeDetailMetadata.ColumnNames.Notes);
            }

            set
            {
                base.SetSystemString(OvertimeDetailMetadata.ColumnNames.Notes, value);
            }
        }
        /// <summary>
        /// Maps to OvertimeDetail.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(OvertimeDetailMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(OvertimeDetailMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to OvertimeDetail.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(OvertimeDetailMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(OvertimeDetailMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esOvertimeDetail entity)
            {
                this.entity = entity;
            }
            public System.String OvertimeDetailID
            {
                get
                {
                    System.Int32? data = entity.OvertimeDetailID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.OvertimeDetailID = null;
                    else entity.OvertimeDetailID = Convert.ToInt32(value);
                }
            }
            public System.String OvertimeID
            {
                get
                {
                    System.Int32? data = entity.OvertimeID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.OvertimeID = null;
                    else entity.OvertimeID = Convert.ToInt32(value);
                }
            }
            public System.String HourFrom
            {
                get
                {
                    System.Decimal? data = entity.HourFrom;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.HourFrom = null;
                    else entity.HourFrom = Convert.ToDecimal(value);
                }
            }
            public System.String HourTo
            {
                get
                {
                    System.Decimal? data = entity.HourTo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.HourTo = null;
                    else entity.HourTo = Convert.ToDecimal(value);
                }
            }
            public System.String Value
            {
                get
                {
                    System.Decimal? data = entity.Value;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Value = null;
                    else entity.Value = Convert.ToDecimal(value);
                }
            }
            public System.String Formula
            {
                get
                {
                    System.Decimal? data = entity.Formula;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Formula = null;
                    else entity.Formula = Convert.ToDecimal(value);
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
            private esOvertimeDetail entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esOvertimeDetailQuery query)
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
                throw new Exception("esOvertimeDetail can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class OvertimeDetail : esOvertimeDetail
    {
    }

    [Serializable]
    abstract public class esOvertimeDetailQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return OvertimeDetailMetadata.Meta();
            }
        }

        public esQueryItem OvertimeDetailID
        {
            get
            {
                return new esQueryItem(this, OvertimeDetailMetadata.ColumnNames.OvertimeDetailID, esSystemType.Int32);
            }
        }

        public esQueryItem OvertimeID
        {
            get
            {
                return new esQueryItem(this, OvertimeDetailMetadata.ColumnNames.OvertimeID, esSystemType.Int32);
            }
        }

        public esQueryItem HourFrom
        {
            get
            {
                return new esQueryItem(this, OvertimeDetailMetadata.ColumnNames.HourFrom, esSystemType.Decimal);
            }
        }

        public esQueryItem HourTo
        {
            get
            {
                return new esQueryItem(this, OvertimeDetailMetadata.ColumnNames.HourTo, esSystemType.Decimal);
            }
        }

        public esQueryItem Value
        {
            get
            {
                return new esQueryItem(this, OvertimeDetailMetadata.ColumnNames.Value, esSystemType.Decimal);
            }
        }

        public esQueryItem Formula
        {
            get
            {
                return new esQueryItem(this, OvertimeDetailMetadata.ColumnNames.Formula, esSystemType.Decimal);
            }
        }

        public esQueryItem Notes
        {
            get
            {
                return new esQueryItem(this, OvertimeDetailMetadata.ColumnNames.Notes, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, OvertimeDetailMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, OvertimeDetailMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("OvertimeDetailCollection")]
    public partial class OvertimeDetailCollection : esOvertimeDetailCollection, IEnumerable<OvertimeDetail>
    {
        public OvertimeDetailCollection()
        {

        }

        public static implicit operator List<OvertimeDetail>(OvertimeDetailCollection coll)
        {
            List<OvertimeDetail> list = new List<OvertimeDetail>();

            foreach (OvertimeDetail emp in coll)
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
                return OvertimeDetailMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new OvertimeDetailQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new OvertimeDetail(row);
        }

        override protected esEntity CreateEntity()
        {
            return new OvertimeDetail();
        }

        #endregion

        [BrowsableAttribute(false)]
        public OvertimeDetailQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new OvertimeDetailQuery();
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
        public bool Load(OvertimeDetailQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public OvertimeDetail AddNew()
        {
            OvertimeDetail entity = base.AddNewEntity() as OvertimeDetail;

            return entity;
        }
        public OvertimeDetail FindByPrimaryKey(Int32 overtimeDetailID)
        {
            return base.FindByPrimaryKey(overtimeDetailID) as OvertimeDetail;
        }

        #region IEnumerable< OvertimeDetail> Members

        IEnumerator<OvertimeDetail> IEnumerable<OvertimeDetail>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as OvertimeDetail;
            }
        }

        #endregion

        private OvertimeDetailQuery query;
    }


    /// <summary>
    /// Encapsulates the 'OvertimeDetail' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("OvertimeDetail ({OvertimeDetailID})")]
    [Serializable]
    public partial class OvertimeDetail : esOvertimeDetail
    {
        public OvertimeDetail()
        {
        }

        public OvertimeDetail(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return OvertimeDetailMetadata.Meta();
            }
        }

        override protected esOvertimeDetailQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new OvertimeDetailQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public OvertimeDetailQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new OvertimeDetailQuery();
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
        public bool Load(OvertimeDetailQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private OvertimeDetailQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class OvertimeDetailQuery : esOvertimeDetailQuery
    {
        public OvertimeDetailQuery()
        {

        }

        public OvertimeDetailQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "OvertimeDetailQuery";
        }
    }

    [Serializable]
    public partial class OvertimeDetailMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected OvertimeDetailMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(OvertimeDetailMetadata.ColumnNames.OvertimeDetailID, 0, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = OvertimeDetailMetadata.PropertyNames.OvertimeDetailID;
            c.IsInPrimaryKey = true;
            c.IsAutoIncrement = true;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(OvertimeDetailMetadata.ColumnNames.OvertimeID, 1, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = OvertimeDetailMetadata.PropertyNames.OvertimeID;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(OvertimeDetailMetadata.ColumnNames.HourFrom, 2, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = OvertimeDetailMetadata.PropertyNames.HourFrom;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(OvertimeDetailMetadata.ColumnNames.HourTo, 3, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = OvertimeDetailMetadata.PropertyNames.HourTo;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(OvertimeDetailMetadata.ColumnNames.Value, 4, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = OvertimeDetailMetadata.PropertyNames.Value;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(OvertimeDetailMetadata.ColumnNames.Formula, 5, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = OvertimeDetailMetadata.PropertyNames.Formula;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(OvertimeDetailMetadata.ColumnNames.Notes, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = OvertimeDetailMetadata.PropertyNames.Notes;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(OvertimeDetailMetadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = OvertimeDetailMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(OvertimeDetailMetadata.ColumnNames.LastUpdateByUserID, 8, typeof(System.String), esSystemType.String);
            c.PropertyName = OvertimeDetailMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 40;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public OvertimeDetailMetadata Meta()
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
            public const string OvertimeDetailID = "OvertimeDetailID";
            public const string OvertimeID = "OvertimeID";
            public const string HourFrom = "HourFrom";
            public const string HourTo = "HourTo";
            public const string Value = "Value";
            public const string Formula = "Formula";
            public const string Notes = "Notes";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string OvertimeDetailID = "OvertimeDetailID";
            public const string OvertimeID = "OvertimeID";
            public const string HourFrom = "HourFrom";
            public const string HourTo = "HourTo";
            public const string Value = "Value";
            public const string Formula = "Formula";
            public const string Notes = "Notes";
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
            lock (typeof(OvertimeDetailMetadata))
            {
                if (OvertimeDetailMetadata.mapDelegates == null)
                {
                    OvertimeDetailMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (OvertimeDetailMetadata.meta == null)
                {
                    OvertimeDetailMetadata.meta = new OvertimeDetailMetadata();
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

                meta.AddTypeMap("OvertimeDetailID", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("OvertimeID", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("HourFrom", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("HourTo", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("Value", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("Formula", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("Notes", new esTypeMap("nchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "OvertimeDetail";
                meta.Destination = "OvertimeDetail";
                meta.spInsert = "proc_OvertimeDetailInsert";
                meta.spUpdate = "proc_OvertimeDetailUpdate";
                meta.spDelete = "proc_OvertimeDetailDelete";
                meta.spLoadAll = "proc_OvertimeDetailLoadAll";
                meta.spLoadByPrimaryKey = "proc_OvertimeDetailLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private OvertimeDetailMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
