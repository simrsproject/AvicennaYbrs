/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 10/9/2017 11:21:40 AM
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
    abstract public class esPpiProcedureSurveillanceUseOfAntibioticsCollection : esEntityCollectionWAuditLog
    {
        public esPpiProcedureSurveillanceUseOfAntibioticsCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "PpiProcedureSurveillanceUseOfAntibioticsCollection";
        }

        #region Query Logic
        protected void InitQuery(esPpiProcedureSurveillanceUseOfAntibioticsQuery query)
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
            this.InitQuery(query as esPpiProcedureSurveillanceUseOfAntibioticsQuery);
        }
        #endregion

        virtual public PpiProcedureSurveillanceUseOfAntibiotics DetachEntity(PpiProcedureSurveillanceUseOfAntibiotics entity)
        {
            return base.DetachEntity(entity) as PpiProcedureSurveillanceUseOfAntibiotics;
        }

        virtual public PpiProcedureSurveillanceUseOfAntibiotics AttachEntity(PpiProcedureSurveillanceUseOfAntibiotics entity)
        {
            return base.AttachEntity(entity) as PpiProcedureSurveillanceUseOfAntibiotics;
        }

        virtual public void Combine(PpiProcedureSurveillanceUseOfAntibioticsCollection collection)
        {
            base.Combine(collection);
        }

        new public PpiProcedureSurveillanceUseOfAntibiotics this[int index]
        {
            get
            {
                return base[index] as PpiProcedureSurveillanceUseOfAntibiotics;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(PpiProcedureSurveillanceUseOfAntibiotics);
        }
    }

    [Serializable]
    abstract public class esPpiProcedureSurveillanceUseOfAntibiotics : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esPpiProcedureSurveillanceUseOfAntibioticsQuery GetDynamicQuery()
        {
            return null;
        }

        public esPpiProcedureSurveillanceUseOfAntibiotics()
        {
        }

        public esPpiProcedureSurveillanceUseOfAntibiotics(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String bookingNo, String itemID, DateTime startDate)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(bookingNo, itemID, startDate);
            else
                return LoadByPrimaryKeyStoredProcedure(bookingNo, itemID, startDate);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String bookingNo, String itemID, DateTime startDate)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(bookingNo, itemID, startDate);
            else
                return LoadByPrimaryKeyStoredProcedure(bookingNo, itemID, startDate);
        }

        private bool LoadByPrimaryKeyDynamic(String bookingNo, String itemID, DateTime startDate)
        {
            esPpiProcedureSurveillanceUseOfAntibioticsQuery query = this.GetDynamicQuery();
            query.Where(query.BookingNo == bookingNo, query.ItemID == itemID, query.StartDate == startDate);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String bookingNo, String itemID, DateTime startDate)
        {
            esParameters parms = new esParameters();
            parms.Add("BookingNo", bookingNo);
            parms.Add("ItemID", itemID);
            parms.Add("StartDate", startDate);
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
                        case "BookingNo": this.str.BookingNo = (string)value; break;
                        case "ItemID": this.str.ItemID = (string)value; break;
                        case "StartDate": this.str.StartDate = (string)value; break;
                        case "EndDate": this.str.EndDate = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "StartDate":

                            if (value == null || value is System.DateTime)
                                this.StartDate = (System.DateTime?)value;
                            break;
                        case "EndDate":

                            if (value == null || value is System.DateTime)
                                this.EndDate = (System.DateTime?)value;
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
        /// Maps to PpiProcedureSurveillanceUseOfAntibiotics.BookingNo
        /// </summary>
        virtual public System.String BookingNo
        {
            get
            {
                return base.GetSystemString(PpiProcedureSurveillanceUseOfAntibioticsMetadata.ColumnNames.BookingNo);
            }

            set
            {
                base.SetSystemString(PpiProcedureSurveillanceUseOfAntibioticsMetadata.ColumnNames.BookingNo, value);
            }
        }
        /// <summary>
        /// Maps to PpiProcedureSurveillanceUseOfAntibiotics.ItemID
        /// </summary>
        virtual public System.String ItemID
        {
            get
            {
                return base.GetSystemString(PpiProcedureSurveillanceUseOfAntibioticsMetadata.ColumnNames.ItemID);
            }

            set
            {
                base.SetSystemString(PpiProcedureSurveillanceUseOfAntibioticsMetadata.ColumnNames.ItemID, value);
            }
        }
        /// <summary>
        /// Maps to PpiProcedureSurveillanceUseOfAntibiotics.StartDate
        /// </summary>
        virtual public System.DateTime? StartDate
        {
            get
            {
                return base.GetSystemDateTime(PpiProcedureSurveillanceUseOfAntibioticsMetadata.ColumnNames.StartDate);
            }

            set
            {
                base.SetSystemDateTime(PpiProcedureSurveillanceUseOfAntibioticsMetadata.ColumnNames.StartDate, value);
            }
        }
        /// <summary>
        /// Maps to PpiProcedureSurveillanceUseOfAntibiotics.EndDate
        /// </summary>
        virtual public System.DateTime? EndDate
        {
            get
            {
                return base.GetSystemDateTime(PpiProcedureSurveillanceUseOfAntibioticsMetadata.ColumnNames.EndDate);
            }

            set
            {
                base.SetSystemDateTime(PpiProcedureSurveillanceUseOfAntibioticsMetadata.ColumnNames.EndDate, value);
            }
        }
        /// <summary>
        /// Maps to PpiProcedureSurveillanceUseOfAntibiotics.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(PpiProcedureSurveillanceUseOfAntibioticsMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(PpiProcedureSurveillanceUseOfAntibioticsMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to PpiProcedureSurveillanceUseOfAntibiotics.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(PpiProcedureSurveillanceUseOfAntibioticsMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(PpiProcedureSurveillanceUseOfAntibioticsMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esPpiProcedureSurveillanceUseOfAntibiotics entity)
            {
                this.entity = entity;
            }
            public System.String BookingNo
            {
                get
                {
                    System.String data = entity.BookingNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.BookingNo = null;
                    else entity.BookingNo = Convert.ToString(value);
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
            public System.String StartDate
            {
                get
                {
                    System.DateTime? data = entity.StartDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.StartDate = null;
                    else entity.StartDate = Convert.ToDateTime(value);
                }
            }
            public System.String EndDate
            {
                get
                {
                    System.DateTime? data = entity.EndDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.EndDate = null;
                    else entity.EndDate = Convert.ToDateTime(value);
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
            private esPpiProcedureSurveillanceUseOfAntibiotics entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esPpiProcedureSurveillanceUseOfAntibioticsQuery query)
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
                throw new Exception("esPpiProcedureSurveillanceUseOfAntibiotics can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class PpiProcedureSurveillanceUseOfAntibiotics : esPpiProcedureSurveillanceUseOfAntibiotics
    {
    }

    [Serializable]
    abstract public class esPpiProcedureSurveillanceUseOfAntibioticsQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return PpiProcedureSurveillanceUseOfAntibioticsMetadata.Meta();
            }
        }

        public esQueryItem BookingNo
        {
            get
            {
                return new esQueryItem(this, PpiProcedureSurveillanceUseOfAntibioticsMetadata.ColumnNames.BookingNo, esSystemType.String);
            }
        }

        public esQueryItem ItemID
        {
            get
            {
                return new esQueryItem(this, PpiProcedureSurveillanceUseOfAntibioticsMetadata.ColumnNames.ItemID, esSystemType.String);
            }
        }

        public esQueryItem StartDate
        {
            get
            {
                return new esQueryItem(this, PpiProcedureSurveillanceUseOfAntibioticsMetadata.ColumnNames.StartDate, esSystemType.DateTime);
            }
        }

        public esQueryItem EndDate
        {
            get
            {
                return new esQueryItem(this, PpiProcedureSurveillanceUseOfAntibioticsMetadata.ColumnNames.EndDate, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, PpiProcedureSurveillanceUseOfAntibioticsMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, PpiProcedureSurveillanceUseOfAntibioticsMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("PpiProcedureSurveillanceUseOfAntibioticsCollection")]
    public partial class PpiProcedureSurveillanceUseOfAntibioticsCollection : esPpiProcedureSurveillanceUseOfAntibioticsCollection, IEnumerable<PpiProcedureSurveillanceUseOfAntibiotics>
    {
        public PpiProcedureSurveillanceUseOfAntibioticsCollection()
        {

        }

        public static implicit operator List<PpiProcedureSurveillanceUseOfAntibiotics>(PpiProcedureSurveillanceUseOfAntibioticsCollection coll)
        {
            List<PpiProcedureSurveillanceUseOfAntibiotics> list = new List<PpiProcedureSurveillanceUseOfAntibiotics>();

            foreach (PpiProcedureSurveillanceUseOfAntibiotics emp in coll)
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
                return PpiProcedureSurveillanceUseOfAntibioticsMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PpiProcedureSurveillanceUseOfAntibioticsQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new PpiProcedureSurveillanceUseOfAntibiotics(row);
        }

        override protected esEntity CreateEntity()
        {
            return new PpiProcedureSurveillanceUseOfAntibiotics();
        }

        #endregion

        [BrowsableAttribute(false)]
        public PpiProcedureSurveillanceUseOfAntibioticsQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PpiProcedureSurveillanceUseOfAntibioticsQuery();
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
        public bool Load(PpiProcedureSurveillanceUseOfAntibioticsQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public PpiProcedureSurveillanceUseOfAntibiotics AddNew()
        {
            PpiProcedureSurveillanceUseOfAntibiotics entity = base.AddNewEntity() as PpiProcedureSurveillanceUseOfAntibiotics;

            return entity;
        }
        public PpiProcedureSurveillanceUseOfAntibiotics FindByPrimaryKey(String bookingNo, String itemID, DateTime startDate)
        {
            return base.FindByPrimaryKey(bookingNo, itemID, startDate) as PpiProcedureSurveillanceUseOfAntibiotics;
        }

        #region IEnumerable< PpiProcedureSurveillanceUseOfAntibiotics> Members

        IEnumerator<PpiProcedureSurveillanceUseOfAntibiotics> IEnumerable<PpiProcedureSurveillanceUseOfAntibiotics>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as PpiProcedureSurveillanceUseOfAntibiotics;
            }
        }

        #endregion

        private PpiProcedureSurveillanceUseOfAntibioticsQuery query;
    }


    /// <summary>
    /// Encapsulates the 'PpiProcedureSurveillanceUseOfAntibiotics' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("PpiProcedureSurveillanceUseOfAntibiotics ({BookingNo, ItemID, StartDate})")]
    [Serializable]
    public partial class PpiProcedureSurveillanceUseOfAntibiotics : esPpiProcedureSurveillanceUseOfAntibiotics
    {
        public PpiProcedureSurveillanceUseOfAntibiotics()
        {
        }

        public PpiProcedureSurveillanceUseOfAntibiotics(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return PpiProcedureSurveillanceUseOfAntibioticsMetadata.Meta();
            }
        }

        override protected esPpiProcedureSurveillanceUseOfAntibioticsQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PpiProcedureSurveillanceUseOfAntibioticsQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public PpiProcedureSurveillanceUseOfAntibioticsQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PpiProcedureSurveillanceUseOfAntibioticsQuery();
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
        public bool Load(PpiProcedureSurveillanceUseOfAntibioticsQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private PpiProcedureSurveillanceUseOfAntibioticsQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class PpiProcedureSurveillanceUseOfAntibioticsQuery : esPpiProcedureSurveillanceUseOfAntibioticsQuery
    {
        public PpiProcedureSurveillanceUseOfAntibioticsQuery()
        {

        }

        public PpiProcedureSurveillanceUseOfAntibioticsQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "PpiProcedureSurveillanceUseOfAntibioticsQuery";
        }
    }

    [Serializable]
    public partial class PpiProcedureSurveillanceUseOfAntibioticsMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected PpiProcedureSurveillanceUseOfAntibioticsMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(PpiProcedureSurveillanceUseOfAntibioticsMetadata.ColumnNames.BookingNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = PpiProcedureSurveillanceUseOfAntibioticsMetadata.PropertyNames.BookingNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(PpiProcedureSurveillanceUseOfAntibioticsMetadata.ColumnNames.ItemID, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = PpiProcedureSurveillanceUseOfAntibioticsMetadata.PropertyNames.ItemID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(PpiProcedureSurveillanceUseOfAntibioticsMetadata.ColumnNames.StartDate, 2, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PpiProcedureSurveillanceUseOfAntibioticsMetadata.PropertyNames.StartDate;
            c.IsInPrimaryKey = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiProcedureSurveillanceUseOfAntibioticsMetadata.ColumnNames.EndDate, 3, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PpiProcedureSurveillanceUseOfAntibioticsMetadata.PropertyNames.EndDate;
            _columns.Add(c);

            c = new esColumnMetadata(PpiProcedureSurveillanceUseOfAntibioticsMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PpiProcedureSurveillanceUseOfAntibioticsMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiProcedureSurveillanceUseOfAntibioticsMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = PpiProcedureSurveillanceUseOfAntibioticsMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public PpiProcedureSurveillanceUseOfAntibioticsMetadata Meta()
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
            public const string BookingNo = "BookingNo";
            public const string ItemID = "ItemID";
            public const string StartDate = "StartDate";
            public const string EndDate = "EndDate";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string BookingNo = "BookingNo";
            public const string ItemID = "ItemID";
            public const string StartDate = "StartDate";
            public const string EndDate = "EndDate";
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
            lock (typeof(PpiProcedureSurveillanceUseOfAntibioticsMetadata))
            {
                if (PpiProcedureSurveillanceUseOfAntibioticsMetadata.mapDelegates == null)
                {
                    PpiProcedureSurveillanceUseOfAntibioticsMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (PpiProcedureSurveillanceUseOfAntibioticsMetadata.meta == null)
                {
                    PpiProcedureSurveillanceUseOfAntibioticsMetadata.meta = new PpiProcedureSurveillanceUseOfAntibioticsMetadata();
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

                meta.AddTypeMap("BookingNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("StartDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("EndDate", new esTypeMap("date", "System.DateTime"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "PpiProcedureSurveillanceUseOfAntibiotics";
                meta.Destination = "PpiProcedureSurveillanceUseOfAntibiotics";
                meta.spInsert = "proc_PpiProcedureSurveillanceUseOfAntibioticsInsert";
                meta.spUpdate = "proc_PpiProcedureSurveillanceUseOfAntibioticsUpdate";
                meta.spDelete = "proc_PpiProcedureSurveillanceUseOfAntibioticsDelete";
                meta.spLoadAll = "proc_PpiProcedureSurveillanceUseOfAntibioticsLoadAll";
                meta.spLoadByPrimaryKey = "proc_PpiProcedureSurveillanceUseOfAntibioticsLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private PpiProcedureSurveillanceUseOfAntibioticsMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
