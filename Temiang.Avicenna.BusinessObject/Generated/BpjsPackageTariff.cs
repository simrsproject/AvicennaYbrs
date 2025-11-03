/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 8/7/2017 9:53:30 AM
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
    abstract public class esBpjsPackageTariffCollection : esEntityCollectionWAuditLog
    {
        public esBpjsPackageTariffCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "BpjsPackageTariffCollection";
        }

        #region Query Logic
        protected void InitQuery(esBpjsPackageTariffQuery query)
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
            this.InitQuery(query as esBpjsPackageTariffQuery);
        }
        #endregion

        virtual public BpjsPackageTariff DetachEntity(BpjsPackageTariff entity)
        {
            return base.DetachEntity(entity) as BpjsPackageTariff;
        }

        virtual public BpjsPackageTariff AttachEntity(BpjsPackageTariff entity)
        {
            return base.AttachEntity(entity) as BpjsPackageTariff;
        }

        virtual public void Combine(BpjsPackageTariffCollection collection)
        {
            base.Combine(collection);
        }

        new public BpjsPackageTariff this[int index]
        {
            get
            {
                return base[index] as BpjsPackageTariff;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(BpjsPackageTariff);
        }
    }

    [Serializable]
    abstract public class esBpjsPackageTariff : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esBpjsPackageTariffQuery GetDynamicQuery()
        {
            return null;
        }

        public esBpjsPackageTariff()
        {
        }

        public esBpjsPackageTariff(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String packageID, DateTime startingDate, String classID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(packageID, startingDate, classID);
            else
                return LoadByPrimaryKeyStoredProcedure(packageID, startingDate, classID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String packageID, DateTime startingDate, String classID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(packageID, startingDate, classID);
            else
                return LoadByPrimaryKeyStoredProcedure(packageID, startingDate, classID);
        }

        private bool LoadByPrimaryKeyDynamic(String packageID, DateTime startingDate, String classID)
        {
            esBpjsPackageTariffQuery query = this.GetDynamicQuery();
            query.Where(query.PackageID == packageID, query.StartingDate == startingDate, query.ClassID == classID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String packageID, DateTime startingDate, String classID)
        {
            esParameters parms = new esParameters();
            parms.Add("PackageID", packageID);
            parms.Add("StartingDate", startingDate);
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
                        case "PackageID": this.str.PackageID = (string)value; break;
                        case "StartingDate": this.str.StartingDate = (string)value; break;
                        case "ClassID": this.str.ClassID = (string)value; break;
                        case "Price": this.str.Price = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "StartingDate":

                            if (value == null || value is System.DateTime)
                                this.StartingDate = (System.DateTime?)value;
                            break;
                        case "Price":

                            if (value == null || value is System.Decimal)
                                this.Price = (System.Decimal?)value;
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
        /// Maps to BpjsPackageTariff.PackageID
        /// </summary>
        virtual public System.String PackageID
        {
            get
            {
                return base.GetSystemString(BpjsPackageTariffMetadata.ColumnNames.PackageID);
            }

            set
            {
                base.SetSystemString(BpjsPackageTariffMetadata.ColumnNames.PackageID, value);
            }
        }
        /// <summary>
        /// Maps to BpjsPackageTariff.StartingDate
        /// </summary>
        virtual public System.DateTime? StartingDate
        {
            get
            {
                return base.GetSystemDateTime(BpjsPackageTariffMetadata.ColumnNames.StartingDate);
            }

            set
            {
                base.SetSystemDateTime(BpjsPackageTariffMetadata.ColumnNames.StartingDate, value);
            }
        }
        /// <summary>
        /// Maps to BpjsPackageTariff.ClassID
        /// </summary>
        virtual public System.String ClassID
        {
            get
            {
                return base.GetSystemString(BpjsPackageTariffMetadata.ColumnNames.ClassID);
            }

            set
            {
                base.SetSystemString(BpjsPackageTariffMetadata.ColumnNames.ClassID, value);
            }
        }
        /// <summary>
        /// Maps to BpjsPackageTariff.Price
        /// </summary>
        virtual public System.Decimal? Price
        {
            get
            {
                return base.GetSystemDecimal(BpjsPackageTariffMetadata.ColumnNames.Price);
            }

            set
            {
                base.SetSystemDecimal(BpjsPackageTariffMetadata.ColumnNames.Price, value);
            }
        }
        /// <summary>
        /// Maps to BpjsPackageTariff.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(BpjsPackageTariffMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(BpjsPackageTariffMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to BpjsPackageTariff.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(BpjsPackageTariffMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(BpjsPackageTariffMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esBpjsPackageTariff entity)
            {
                this.entity = entity;
            }
            public System.String PackageID
            {
                get
                {
                    System.String data = entity.PackageID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PackageID = null;
                    else entity.PackageID = Convert.ToString(value);
                }
            }
            public System.String StartingDate
            {
                get
                {
                    System.DateTime? data = entity.StartingDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.StartingDate = null;
                    else entity.StartingDate = Convert.ToDateTime(value);
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
            private esBpjsPackageTariff entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esBpjsPackageTariffQuery query)
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
                throw new Exception("esBpjsPackageTariff can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class BpjsPackageTariff : esBpjsPackageTariff
    {
    }

    [Serializable]
    abstract public class esBpjsPackageTariffQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return BpjsPackageTariffMetadata.Meta();
            }
        }

        public esQueryItem PackageID
        {
            get
            {
                return new esQueryItem(this, BpjsPackageTariffMetadata.ColumnNames.PackageID, esSystemType.String);
            }
        }

        public esQueryItem StartingDate
        {
            get
            {
                return new esQueryItem(this, BpjsPackageTariffMetadata.ColumnNames.StartingDate, esSystemType.DateTime);
            }
        }

        public esQueryItem ClassID
        {
            get
            {
                return new esQueryItem(this, BpjsPackageTariffMetadata.ColumnNames.ClassID, esSystemType.String);
            }
        }

        public esQueryItem Price
        {
            get
            {
                return new esQueryItem(this, BpjsPackageTariffMetadata.ColumnNames.Price, esSystemType.Decimal);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, BpjsPackageTariffMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, BpjsPackageTariffMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("BpjsPackageTariffCollection")]
    public partial class BpjsPackageTariffCollection : esBpjsPackageTariffCollection, IEnumerable<BpjsPackageTariff>
    {
        public BpjsPackageTariffCollection()
        {

        }

        public static implicit operator List<BpjsPackageTariff>(BpjsPackageTariffCollection coll)
        {
            List<BpjsPackageTariff> list = new List<BpjsPackageTariff>();

            foreach (BpjsPackageTariff emp in coll)
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
                return BpjsPackageTariffMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new BpjsPackageTariffQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new BpjsPackageTariff(row);
        }

        override protected esEntity CreateEntity()
        {
            return new BpjsPackageTariff();
        }

        #endregion

        [BrowsableAttribute(false)]
        public BpjsPackageTariffQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new BpjsPackageTariffQuery();
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
        public bool Load(BpjsPackageTariffQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public BpjsPackageTariff AddNew()
        {
            BpjsPackageTariff entity = base.AddNewEntity() as BpjsPackageTariff;

            return entity;
        }
        public BpjsPackageTariff FindByPrimaryKey(String packageID, DateTime startingDate, String classID)
        {
            return base.FindByPrimaryKey(packageID, startingDate, classID) as BpjsPackageTariff;
        }

        #region IEnumerable< BpjsPackageTariff> Members

        IEnumerator<BpjsPackageTariff> IEnumerable<BpjsPackageTariff>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as BpjsPackageTariff;
            }
        }

        #endregion

        private BpjsPackageTariffQuery query;
    }


    /// <summary>
    /// Encapsulates the 'BpjsPackageTariff' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("BpjsPackageTariff ({PackageID, StartingDate, ClassID})")]
    [Serializable]
    public partial class BpjsPackageTariff : esBpjsPackageTariff
    {
        public BpjsPackageTariff()
        {
        }

        public BpjsPackageTariff(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return BpjsPackageTariffMetadata.Meta();
            }
        }

        override protected esBpjsPackageTariffQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new BpjsPackageTariffQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public BpjsPackageTariffQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new BpjsPackageTariffQuery();
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
        public bool Load(BpjsPackageTariffQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private BpjsPackageTariffQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class BpjsPackageTariffQuery : esBpjsPackageTariffQuery
    {
        public BpjsPackageTariffQuery()
        {

        }

        public BpjsPackageTariffQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "BpjsPackageTariffQuery";
        }
    }

    [Serializable]
    public partial class BpjsPackageTariffMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected BpjsPackageTariffMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(BpjsPackageTariffMetadata.ColumnNames.PackageID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = BpjsPackageTariffMetadata.PropertyNames.PackageID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsPackageTariffMetadata.ColumnNames.StartingDate, 1, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = BpjsPackageTariffMetadata.PropertyNames.StartingDate;
            c.IsInPrimaryKey = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsPackageTariffMetadata.ColumnNames.ClassID, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = BpjsPackageTariffMetadata.PropertyNames.ClassID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsPackageTariffMetadata.ColumnNames.Price, 3, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = BpjsPackageTariffMetadata.PropertyNames.Price;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsPackageTariffMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = BpjsPackageTariffMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsPackageTariffMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = BpjsPackageTariffMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public BpjsPackageTariffMetadata Meta()
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
            public const string PackageID = "PackageID";
            public const string StartingDate = "StartingDate";
            public const string ClassID = "ClassID";
            public const string Price = "Price";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string PackageID = "PackageID";
            public const string StartingDate = "StartingDate";
            public const string ClassID = "ClassID";
            public const string Price = "Price";
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
            lock (typeof(BpjsPackageTariffMetadata))
            {
                if (BpjsPackageTariffMetadata.mapDelegates == null)
                {
                    BpjsPackageTariffMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (BpjsPackageTariffMetadata.meta == null)
                {
                    BpjsPackageTariffMetadata.meta = new BpjsPackageTariffMetadata();
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

                meta.AddTypeMap("PackageID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("StartingDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("ClassID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Price", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "BpjsPackageTariff";
                meta.Destination = "BpjsPackageTariff";
                meta.spInsert = "proc_BpjsPackageTariffInsert";
                meta.spUpdate = "proc_BpjsPackageTariffUpdate";
                meta.spDelete = "proc_BpjsPackageTariffDelete";
                meta.spLoadAll = "proc_BpjsPackageTariffLoadAll";
                meta.spLoadByPrimaryKey = "proc_BpjsPackageTariffLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private BpjsPackageTariffMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
