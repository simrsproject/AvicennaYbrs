/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 6/16/2017 9:24:05 AM
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
    abstract public class esBloodBalanceCollection : esEntityCollectionWAuditLog
    {
        public esBloodBalanceCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "BloodBalanceCollection";
        }

        #region Query Logic
        protected void InitQuery(esBloodBalanceQuery query)
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
            this.InitQuery(query as esBloodBalanceQuery);
        }
        #endregion

        virtual public BloodBalance DetachEntity(BloodBalance entity)
        {
            return base.DetachEntity(entity) as BloodBalance;
        }

        virtual public BloodBalance AttachEntity(BloodBalance entity)
        {
            return base.AttachEntity(entity) as BloodBalance;
        }

        virtual public void Combine(BloodBalanceCollection collection)
        {
            base.Combine(collection);
        }

        new public BloodBalance this[int index]
        {
            get
            {
                return base[index] as BloodBalance;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(BloodBalance);
        }
    }

    [Serializable]
    abstract public class esBloodBalance : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esBloodBalanceQuery GetDynamicQuery()
        {
            return null;
        }

        public esBloodBalance()
        {
        }

        public esBloodBalance(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String sRBloodSource, String sRBloodSourceFrom, String bagNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(sRBloodSource, sRBloodSourceFrom, bagNo);
            else
                return LoadByPrimaryKeyStoredProcedure(sRBloodSource, sRBloodSourceFrom, bagNo);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String sRBloodSource, String sRBloodSourceFrom, String bagNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(sRBloodSource, sRBloodSourceFrom, bagNo);
            else
                return LoadByPrimaryKeyStoredProcedure(sRBloodSource, sRBloodSourceFrom, bagNo);
        }

        private bool LoadByPrimaryKeyDynamic(String sRBloodSource, String sRBloodSourceFrom, String bagNo)
        {
            esBloodBalanceQuery query = this.GetDynamicQuery();
            query.Where(query.SRBloodSource == sRBloodSource, query.SRBloodSourceFrom == sRBloodSourceFrom, query.BagNo == bagNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String sRBloodSource, String sRBloodSourceFrom, String bagNo)
        {
            esParameters parms = new esParameters();
            parms.Add("SRBloodSource", sRBloodSource);
            parms.Add("SRBloodSourceFrom", sRBloodSourceFrom);
            parms.Add("BagNo", bagNo);
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
                        case "SRBloodSource": this.str.SRBloodSource = (string)value; break;
                        case "SRBloodSourceFrom": this.str.SRBloodSourceFrom = (string)value; break;
                        case "BagNo": this.str.BagNo = (string)value; break;
                        case "Balance": this.str.Balance = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "Balance":

                            if (value == null || value is System.Decimal)
                                this.Balance = (System.Decimal?)value;
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
        /// Maps to BloodBalance.SRBloodSource
        /// </summary>
        virtual public System.String SRBloodSource
        {
            get
            {
                return base.GetSystemString(BloodBalanceMetadata.ColumnNames.SRBloodSource);
            }

            set
            {
                base.SetSystemString(BloodBalanceMetadata.ColumnNames.SRBloodSource, value);
            }
        }
        /// <summary>
        /// Maps to BloodBalance.SRBloodSourceFrom
        /// </summary>
        virtual public System.String SRBloodSourceFrom
        {
            get
            {
                return base.GetSystemString(BloodBalanceMetadata.ColumnNames.SRBloodSourceFrom);
            }

            set
            {
                base.SetSystemString(BloodBalanceMetadata.ColumnNames.SRBloodSourceFrom, value);
            }
        }
        /// <summary>
        /// Maps to BloodBalance.BagNo
        /// </summary>
        virtual public System.String BagNo
        {
            get
            {
                return base.GetSystemString(BloodBalanceMetadata.ColumnNames.BagNo);
            }

            set
            {
                base.SetSystemString(BloodBalanceMetadata.ColumnNames.BagNo, value);
            }
        }
        /// <summary>
        /// Maps to BloodBalance.Balance
        /// </summary>
        virtual public System.Decimal? Balance
        {
            get
            {
                return base.GetSystemDecimal(BloodBalanceMetadata.ColumnNames.Balance);
            }

            set
            {
                base.SetSystemDecimal(BloodBalanceMetadata.ColumnNames.Balance, value);
            }
        }
        /// <summary>
        /// Maps to BloodBalance.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(BloodBalanceMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(BloodBalanceMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to BloodBalance.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(BloodBalanceMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(BloodBalanceMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esBloodBalance entity)
            {
                this.entity = entity;
            }
            public System.String SRBloodSource
            {
                get
                {
                    System.String data = entity.SRBloodSource;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRBloodSource = null;
                    else entity.SRBloodSource = Convert.ToString(value);
                }
            }
            public System.String SRBloodSourceFrom
            {
                get
                {
                    System.String data = entity.SRBloodSourceFrom;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRBloodSourceFrom = null;
                    else entity.SRBloodSourceFrom = Convert.ToString(value);
                }
            }
            public System.String BagNo
            {
                get
                {
                    System.String data = entity.BagNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.BagNo = null;
                    else entity.BagNo = Convert.ToString(value);
                }
            }
            public System.String Balance
            {
                get
                {
                    System.Decimal? data = entity.Balance;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Balance = null;
                    else entity.Balance = Convert.ToDecimal(value);
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
            private esBloodBalance entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esBloodBalanceQuery query)
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
                throw new Exception("esBloodBalance can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class BloodBalance : esBloodBalance
    {
    }

    [Serializable]
    abstract public class esBloodBalanceQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return BloodBalanceMetadata.Meta();
            }
        }

        public esQueryItem SRBloodSource
        {
            get
            {
                return new esQueryItem(this, BloodBalanceMetadata.ColumnNames.SRBloodSource, esSystemType.String);
            }
        }

        public esQueryItem SRBloodSourceFrom
        {
            get
            {
                return new esQueryItem(this, BloodBalanceMetadata.ColumnNames.SRBloodSourceFrom, esSystemType.String);
            }
        }

        public esQueryItem BagNo
        {
            get
            {
                return new esQueryItem(this, BloodBalanceMetadata.ColumnNames.BagNo, esSystemType.String);
            }
        }

        public esQueryItem Balance
        {
            get
            {
                return new esQueryItem(this, BloodBalanceMetadata.ColumnNames.Balance, esSystemType.Decimal);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, BloodBalanceMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, BloodBalanceMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("BloodBalanceCollection")]
    public partial class BloodBalanceCollection : esBloodBalanceCollection, IEnumerable<BloodBalance>
    {
        public BloodBalanceCollection()
        {

        }

        public static implicit operator List<BloodBalance>(BloodBalanceCollection coll)
        {
            List<BloodBalance> list = new List<BloodBalance>();

            foreach (BloodBalance emp in coll)
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
                return BloodBalanceMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new BloodBalanceQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new BloodBalance(row);
        }

        override protected esEntity CreateEntity()
        {
            return new BloodBalance();
        }

        #endregion

        [BrowsableAttribute(false)]
        public BloodBalanceQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new BloodBalanceQuery();
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
        public bool Load(BloodBalanceQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public BloodBalance AddNew()
        {
            BloodBalance entity = base.AddNewEntity() as BloodBalance;

            return entity;
        }
        public BloodBalance FindByPrimaryKey(String sRBloodSource, String sRBloodSourceFrom, String bagNo)
        {
            return base.FindByPrimaryKey(sRBloodSource, sRBloodSourceFrom, bagNo) as BloodBalance;
        }

        #region IEnumerable< BloodBalance> Members

        IEnumerator<BloodBalance> IEnumerable<BloodBalance>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as BloodBalance;
            }
        }

        #endregion

        private BloodBalanceQuery query;
    }


    /// <summary>
    /// Encapsulates the 'BloodBalance' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("BloodBalance ({SRBloodSource, SRBloodSourceFrom, BagNo})")]
    [Serializable]
    public partial class BloodBalance : esBloodBalance
    {
        public BloodBalance()
        {
        }

        public BloodBalance(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return BloodBalanceMetadata.Meta();
            }
        }

        override protected esBloodBalanceQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new BloodBalanceQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public BloodBalanceQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new BloodBalanceQuery();
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
        public bool Load(BloodBalanceQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private BloodBalanceQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class BloodBalanceQuery : esBloodBalanceQuery
    {
        public BloodBalanceQuery()
        {

        }

        public BloodBalanceQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "BloodBalanceQuery";
        }
    }

    [Serializable]
    public partial class BloodBalanceMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected BloodBalanceMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(BloodBalanceMetadata.ColumnNames.SRBloodSource, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = BloodBalanceMetadata.PropertyNames.SRBloodSource;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(BloodBalanceMetadata.ColumnNames.SRBloodSourceFrom, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = BloodBalanceMetadata.PropertyNames.SRBloodSourceFrom;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(BloodBalanceMetadata.ColumnNames.BagNo, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = BloodBalanceMetadata.PropertyNames.BagNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 50;
            _columns.Add(c);

            c = new esColumnMetadata(BloodBalanceMetadata.ColumnNames.Balance, 3, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = BloodBalanceMetadata.PropertyNames.Balance;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(BloodBalanceMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = BloodBalanceMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BloodBalanceMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = BloodBalanceMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public BloodBalanceMetadata Meta()
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
            public const string SRBloodSource = "SRBloodSource";
            public const string SRBloodSourceFrom = "SRBloodSourceFrom";
            public const string BagNo = "BagNo";
            public const string Balance = "Balance";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string SRBloodSource = "SRBloodSource";
            public const string SRBloodSourceFrom = "SRBloodSourceFrom";
            public const string BagNo = "BagNo";
            public const string Balance = "Balance";
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
            lock (typeof(BloodBalanceMetadata))
            {
                if (BloodBalanceMetadata.mapDelegates == null)
                {
                    BloodBalanceMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (BloodBalanceMetadata.meta == null)
                {
                    BloodBalanceMetadata.meta = new BloodBalanceMetadata();
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

                meta.AddTypeMap("SRBloodSource", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRBloodSourceFrom", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("BagNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Balance", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "BloodBalance";
                meta.Destination = "BloodBalance";
                meta.spInsert = "proc_BloodBalanceInsert";
                meta.spUpdate = "proc_BloodBalanceUpdate";
                meta.spDelete = "proc_BloodBalanceDelete";
                meta.spLoadAll = "proc_BloodBalanceLoadAll";
                meta.spLoadByPrimaryKey = "proc_BloodBalanceLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private BloodBalanceMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
