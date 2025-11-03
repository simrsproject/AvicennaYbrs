/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 9/15/2017 10:35:41 AM
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
    abstract public class esSupplierBankCollection : esEntityCollectionWAuditLog
    {
        public esSupplierBankCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "SupplierBankCollection";
        }

        #region Query Logic
        protected void InitQuery(esSupplierBankQuery query)
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
            this.InitQuery(query as esSupplierBankQuery);
        }
        #endregion

        virtual public SupplierBank DetachEntity(SupplierBank entity)
        {
            return base.DetachEntity(entity) as SupplierBank;
        }

        virtual public SupplierBank AttachEntity(SupplierBank entity)
        {
            return base.AttachEntity(entity) as SupplierBank;
        }

        virtual public void Combine(SupplierBankCollection collection)
        {
            base.Combine(collection);
        }

        new public SupplierBank this[int index]
        {
            get
            {
                return base[index] as SupplierBank;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(SupplierBank);
        }
    }

    [Serializable]
    abstract public class esSupplierBank : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esSupplierBankQuery GetDynamicQuery()
        {
            return null;
        }

        public esSupplierBank()
        {
        }

        public esSupplierBank(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String supplierID, String bankAccountNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(supplierID, bankAccountNo);
            else
                return LoadByPrimaryKeyStoredProcedure(supplierID, bankAccountNo);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String supplierID, String bankAccountNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(supplierID, bankAccountNo);
            else
                return LoadByPrimaryKeyStoredProcedure(supplierID, bankAccountNo);
        }

        private bool LoadByPrimaryKeyDynamic(String supplierID, String bankAccountNo)
        {
            esSupplierBankQuery query = this.GetDynamicQuery();
            query.Where(query.SupplierID == supplierID, query.BankAccountNo == bankAccountNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String supplierID, String bankAccountNo)
        {
            esParameters parms = new esParameters();
            parms.Add("SupplierID", supplierID);
            parms.Add("BankAccountNo", bankAccountNo);
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
                        case "SupplierID": this.str.SupplierID = (string)value; break;
                        case "BankAccountNo": this.str.BankAccountNo = (string)value; break;
                        case "BankName": this.str.BankName = (string)value; break;
                        case "BankAccountName": this.str.BankAccountName = (string)value; break;
                        case "IsActive": this.str.IsActive = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "IsActive":

                            if (value == null || value is System.Boolean)
                                this.IsActive = (System.Boolean?)value;
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
        /// Maps to SupplierBank.SupplierID
        /// </summary>
        virtual public System.String SupplierID
        {
            get
            {
                return base.GetSystemString(SupplierBankMetadata.ColumnNames.SupplierID);
            }

            set
            {
                base.SetSystemString(SupplierBankMetadata.ColumnNames.SupplierID, value);
            }
        }
        /// <summary>
        /// Maps to SupplierBank.BankAccountNo
        /// </summary>
        virtual public System.String BankAccountNo
        {
            get
            {
                return base.GetSystemString(SupplierBankMetadata.ColumnNames.BankAccountNo);
            }

            set
            {
                base.SetSystemString(SupplierBankMetadata.ColumnNames.BankAccountNo, value);
            }
        }
        /// <summary>
        /// Maps to SupplierBank.BankName
        /// </summary>
        virtual public System.String BankName
        {
            get
            {
                return base.GetSystemString(SupplierBankMetadata.ColumnNames.BankName);
            }

            set
            {
                base.SetSystemString(SupplierBankMetadata.ColumnNames.BankName, value);
            }
        }
        /// <summary>
        /// Maps to SupplierBank.BankAccountName
        /// </summary>
        virtual public System.String BankAccountName
        {
            get
            {
                return base.GetSystemString(SupplierBankMetadata.ColumnNames.BankAccountName);
            }

            set
            {
                base.SetSystemString(SupplierBankMetadata.ColumnNames.BankAccountName, value);
            }
        }
        /// <summary>
        /// Maps to SupplierBank.IsActive
        /// </summary>
        virtual public System.Boolean? IsActive
        {
            get
            {
                return base.GetSystemBoolean(SupplierBankMetadata.ColumnNames.IsActive);
            }

            set
            {
                base.SetSystemBoolean(SupplierBankMetadata.ColumnNames.IsActive, value);
            }
        }
        /// <summary>
        /// Maps to SupplierBank.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(SupplierBankMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(SupplierBankMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to SupplierBank.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(SupplierBankMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(SupplierBankMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esSupplierBank entity)
            {
                this.entity = entity;
            }
            public System.String SupplierID
            {
                get
                {
                    System.String data = entity.SupplierID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SupplierID = null;
                    else entity.SupplierID = Convert.ToString(value);
                }
            }
            public System.String BankAccountNo
            {
                get
                {
                    System.String data = entity.BankAccountNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.BankAccountNo = null;
                    else entity.BankAccountNo = Convert.ToString(value);
                }
            }
            public System.String BankName
            {
                get
                {
                    System.String data = entity.BankName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.BankName = null;
                    else entity.BankName = Convert.ToString(value);
                }
            }
            public System.String BankAccountName
            {
                get
                {
                    System.String data = entity.BankAccountName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.BankAccountName = null;
                    else entity.BankAccountName = Convert.ToString(value);
                }
            }
            public System.String IsActive
            {
                get
                {
                    System.Boolean? data = entity.IsActive;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsActive = null;
                    else entity.IsActive = Convert.ToBoolean(value);
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
            private esSupplierBank entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esSupplierBankQuery query)
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
                throw new Exception("esSupplierBank can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class SupplierBank : esSupplierBank
    {
    }

    [Serializable]
    abstract public class esSupplierBankQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return SupplierBankMetadata.Meta();
            }
        }

        public esQueryItem SupplierID
        {
            get
            {
                return new esQueryItem(this, SupplierBankMetadata.ColumnNames.SupplierID, esSystemType.String);
            }
        }

        public esQueryItem BankAccountNo
        {
            get
            {
                return new esQueryItem(this, SupplierBankMetadata.ColumnNames.BankAccountNo, esSystemType.String);
            }
        }

        public esQueryItem BankName
        {
            get
            {
                return new esQueryItem(this, SupplierBankMetadata.ColumnNames.BankName, esSystemType.String);
            }
        }

        public esQueryItem BankAccountName
        {
            get
            {
                return new esQueryItem(this, SupplierBankMetadata.ColumnNames.BankAccountName, esSystemType.String);
            }
        }

        public esQueryItem IsActive
        {
            get
            {
                return new esQueryItem(this, SupplierBankMetadata.ColumnNames.IsActive, esSystemType.Boolean);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, SupplierBankMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, SupplierBankMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("SupplierBankCollection")]
    public partial class SupplierBankCollection : esSupplierBankCollection, IEnumerable<SupplierBank>
    {
        public SupplierBankCollection()
        {

        }

        public static implicit operator List<SupplierBank>(SupplierBankCollection coll)
        {
            List<SupplierBank> list = new List<SupplierBank>();

            foreach (SupplierBank emp in coll)
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
                return SupplierBankMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new SupplierBankQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new SupplierBank(row);
        }

        override protected esEntity CreateEntity()
        {
            return new SupplierBank();
        }

        #endregion

        [BrowsableAttribute(false)]
        public SupplierBankQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new SupplierBankQuery();
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
        public bool Load(SupplierBankQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public SupplierBank AddNew()
        {
            SupplierBank entity = base.AddNewEntity() as SupplierBank;

            return entity;
        }
        public SupplierBank FindByPrimaryKey(String supplierID, String bankAccountNo)
        {
            return base.FindByPrimaryKey(supplierID, bankAccountNo) as SupplierBank;
        }

        #region IEnumerable< SupplierBank> Members

        IEnumerator<SupplierBank> IEnumerable<SupplierBank>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as SupplierBank;
            }
        }

        #endregion

        private SupplierBankQuery query;
    }


    /// <summary>
    /// Encapsulates the 'SupplierBank' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("SupplierBank ({SupplierID, BankAccountNo})")]
    [Serializable]
    public partial class SupplierBank : esSupplierBank
    {
        public SupplierBank()
        {
        }

        public SupplierBank(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return SupplierBankMetadata.Meta();
            }
        }

        override protected esSupplierBankQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new SupplierBankQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public SupplierBankQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new SupplierBankQuery();
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
        public bool Load(SupplierBankQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private SupplierBankQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class SupplierBankQuery : esSupplierBankQuery
    {
        public SupplierBankQuery()
        {

        }

        public SupplierBankQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "SupplierBankQuery";
        }
    }

    [Serializable]
    public partial class SupplierBankMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected SupplierBankMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(SupplierBankMetadata.ColumnNames.SupplierID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = SupplierBankMetadata.PropertyNames.SupplierID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(SupplierBankMetadata.ColumnNames.BankAccountNo, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = SupplierBankMetadata.PropertyNames.BankAccountNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 50;
            _columns.Add(c);

            c = new esColumnMetadata(SupplierBankMetadata.ColumnNames.BankName, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = SupplierBankMetadata.PropertyNames.BankName;
            c.CharacterMaxLength = 150;
            _columns.Add(c);

            c = new esColumnMetadata(SupplierBankMetadata.ColumnNames.BankAccountName, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = SupplierBankMetadata.PropertyNames.BankAccountName;
            c.CharacterMaxLength = 150;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(SupplierBankMetadata.ColumnNames.IsActive, 4, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = SupplierBankMetadata.PropertyNames.IsActive;
            _columns.Add(c);

            c = new esColumnMetadata(SupplierBankMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = SupplierBankMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(SupplierBankMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = SupplierBankMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public SupplierBankMetadata Meta()
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
            public const string SupplierID = "SupplierID";
            public const string BankAccountNo = "BankAccountNo";
            public const string BankName = "BankName";
            public const string BankAccountName = "BankAccountName";
            public const string IsActive = "IsActive";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string SupplierID = "SupplierID";
            public const string BankAccountNo = "BankAccountNo";
            public const string BankName = "BankName";
            public const string BankAccountName = "BankAccountName";
            public const string IsActive = "IsActive";
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
            lock (typeof(SupplierBankMetadata))
            {
                if (SupplierBankMetadata.mapDelegates == null)
                {
                    SupplierBankMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (SupplierBankMetadata.meta == null)
                {
                    SupplierBankMetadata.meta = new SupplierBankMetadata();
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

                meta.AddTypeMap("SupplierID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("BankAccountNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("BankName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("BankAccountName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "SupplierBank";
                meta.Destination = "SupplierBank";
                meta.spInsert = "proc_SupplierBankInsert";
                meta.spUpdate = "proc_SupplierBankUpdate";
                meta.spDelete = "proc_SupplierBankDelete";
                meta.spLoadAll = "proc_SupplierBankLoadAll";
                meta.spLoadByPrimaryKey = "proc_SupplierBankLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private SupplierBankMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
