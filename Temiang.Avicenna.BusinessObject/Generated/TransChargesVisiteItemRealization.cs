/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 6/23/2017 1:27:37 PM
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
    abstract public class esTransChargesVisiteItemRealizationCollection : esEntityCollectionWAuditLog
    {
        public esTransChargesVisiteItemRealizationCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "TransChargesVisiteItemRealizationCollection";
        }

        #region Query Logic
        protected void InitQuery(esTransChargesVisiteItemRealizationQuery query)
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
            this.InitQuery(query as esTransChargesVisiteItemRealizationQuery);
        }
        #endregion

        virtual public TransChargesVisiteItemRealization DetachEntity(TransChargesVisiteItemRealization entity)
        {
            return base.DetachEntity(entity) as TransChargesVisiteItemRealization;
        }

        virtual public TransChargesVisiteItemRealization AttachEntity(TransChargesVisiteItemRealization entity)
        {
            return base.AttachEntity(entity) as TransChargesVisiteItemRealization;
        }

        virtual public void Combine(TransChargesVisiteItemRealizationCollection collection)
        {
            base.Combine(collection);
        }

        new public TransChargesVisiteItemRealization this[int index]
        {
            get
            {
                return base[index] as TransChargesVisiteItemRealization;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(TransChargesVisiteItemRealization);
        }
    }

    [Serializable]
    abstract public class esTransChargesVisiteItemRealization : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esTransChargesVisiteItemRealizationQuery GetDynamicQuery()
        {
            return null;
        }

        public esTransChargesVisiteItemRealization()
        {
        }

        public esTransChargesVisiteItemRealization(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String transactionNo, String itemID, String registrationNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(transactionNo, itemID, registrationNo);
            else
                return LoadByPrimaryKeyStoredProcedure(transactionNo, itemID, registrationNo);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String transactionNo, String itemID, String registrationNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(transactionNo, itemID, registrationNo);
            else
                return LoadByPrimaryKeyStoredProcedure(transactionNo, itemID, registrationNo);
        }

        private bool LoadByPrimaryKeyDynamic(String transactionNo, String itemID, String registrationNo)
        {
            esTransChargesVisiteItemRealizationQuery query = this.GetDynamicQuery();
            query.Where(query.TransactionNo == transactionNo, query.ItemID == itemID, query.RegistrationNo == registrationNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String transactionNo, String itemID, String registrationNo)
        {
            esParameters parms = new esParameters();
            parms.Add("TransactionNo", transactionNo);
            parms.Add("ItemID", itemID);
            parms.Add("RegistrationNo", registrationNo);
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
                        case "ItemID": this.str.ItemID = (string)value; break;
                        case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
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
        /// Maps to TransChargesVisiteItemRealization.TransactionNo
        /// </summary>
        virtual public System.String TransactionNo
        {
            get
            {
                return base.GetSystemString(TransChargesVisiteItemRealizationMetadata.ColumnNames.TransactionNo);
            }

            set
            {
                base.SetSystemString(TransChargesVisiteItemRealizationMetadata.ColumnNames.TransactionNo, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesVisiteItemRealization.ItemID
        /// </summary>
        virtual public System.String ItemID
        {
            get
            {
                return base.GetSystemString(TransChargesVisiteItemRealizationMetadata.ColumnNames.ItemID);
            }

            set
            {
                base.SetSystemString(TransChargesVisiteItemRealizationMetadata.ColumnNames.ItemID, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesVisiteItemRealization.RegistrationNo
        /// </summary>
        virtual public System.String RegistrationNo
        {
            get
            {
                return base.GetSystemString(TransChargesVisiteItemRealizationMetadata.ColumnNames.RegistrationNo);
            }

            set
            {
                base.SetSystemString(TransChargesVisiteItemRealizationMetadata.ColumnNames.RegistrationNo, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesVisiteItemRealization.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(TransChargesVisiteItemRealizationMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(TransChargesVisiteItemRealizationMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesVisiteItemRealization.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(TransChargesVisiteItemRealizationMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(TransChargesVisiteItemRealizationMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esTransChargesVisiteItemRealization entity)
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
            private esTransChargesVisiteItemRealization entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esTransChargesVisiteItemRealizationQuery query)
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
                throw new Exception("esTransChargesVisiteItemRealization can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class TransChargesVisiteItemRealization : esTransChargesVisiteItemRealization
    {
    }

    [Serializable]
    abstract public class esTransChargesVisiteItemRealizationQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return TransChargesVisiteItemRealizationMetadata.Meta();
            }
        }

        public esQueryItem TransactionNo
        {
            get
            {
                return new esQueryItem(this, TransChargesVisiteItemRealizationMetadata.ColumnNames.TransactionNo, esSystemType.String);
            }
        }

        public esQueryItem ItemID
        {
            get
            {
                return new esQueryItem(this, TransChargesVisiteItemRealizationMetadata.ColumnNames.ItemID, esSystemType.String);
            }
        }

        public esQueryItem RegistrationNo
        {
            get
            {
                return new esQueryItem(this, TransChargesVisiteItemRealizationMetadata.ColumnNames.RegistrationNo, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, TransChargesVisiteItemRealizationMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, TransChargesVisiteItemRealizationMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("TransChargesVisiteItemRealizationCollection")]
    public partial class TransChargesVisiteItemRealizationCollection : esTransChargesVisiteItemRealizationCollection, IEnumerable<TransChargesVisiteItemRealization>
    {
        public TransChargesVisiteItemRealizationCollection()
        {

        }

        public static implicit operator List<TransChargesVisiteItemRealization>(TransChargesVisiteItemRealizationCollection coll)
        {
            List<TransChargesVisiteItemRealization> list = new List<TransChargesVisiteItemRealization>();

            foreach (TransChargesVisiteItemRealization emp in coll)
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
                return TransChargesVisiteItemRealizationMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new TransChargesVisiteItemRealizationQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new TransChargesVisiteItemRealization(row);
        }

        override protected esEntity CreateEntity()
        {
            return new TransChargesVisiteItemRealization();
        }

        #endregion

        [BrowsableAttribute(false)]
        public TransChargesVisiteItemRealizationQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new TransChargesVisiteItemRealizationQuery();
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
        public bool Load(TransChargesVisiteItemRealizationQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public TransChargesVisiteItemRealization AddNew()
        {
            TransChargesVisiteItemRealization entity = base.AddNewEntity() as TransChargesVisiteItemRealization;

            return entity;
        }
        public TransChargesVisiteItemRealization FindByPrimaryKey(String transactionNo, String itemID, String registrationNo)
        {
            return base.FindByPrimaryKey(transactionNo, itemID, registrationNo) as TransChargesVisiteItemRealization;
        }

        #region IEnumerable< TransChargesVisiteItemRealization> Members

        IEnumerator<TransChargesVisiteItemRealization> IEnumerable<TransChargesVisiteItemRealization>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as TransChargesVisiteItemRealization;
            }
        }

        #endregion

        private TransChargesVisiteItemRealizationQuery query;
    }


    /// <summary>
    /// Encapsulates the 'TransChargesVisiteItemRealization' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("TransChargesVisiteItemRealization ({TransactionNo, ItemID, RegistrationNo})")]
    [Serializable]
    public partial class TransChargesVisiteItemRealization : esTransChargesVisiteItemRealization
    {
        public TransChargesVisiteItemRealization()
        {
        }

        public TransChargesVisiteItemRealization(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return TransChargesVisiteItemRealizationMetadata.Meta();
            }
        }

        override protected esTransChargesVisiteItemRealizationQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new TransChargesVisiteItemRealizationQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public TransChargesVisiteItemRealizationQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new TransChargesVisiteItemRealizationQuery();
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
        public bool Load(TransChargesVisiteItemRealizationQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private TransChargesVisiteItemRealizationQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class TransChargesVisiteItemRealizationQuery : esTransChargesVisiteItemRealizationQuery
    {
        public TransChargesVisiteItemRealizationQuery()
        {

        }

        public TransChargesVisiteItemRealizationQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "TransChargesVisiteItemRealizationQuery";
        }
    }

    [Serializable]
    public partial class TransChargesVisiteItemRealizationMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected TransChargesVisiteItemRealizationMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(TransChargesVisiteItemRealizationMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = TransChargesVisiteItemRealizationMetadata.PropertyNames.TransactionNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesVisiteItemRealizationMetadata.ColumnNames.ItemID, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = TransChargesVisiteItemRealizationMetadata.PropertyNames.ItemID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesVisiteItemRealizationMetadata.ColumnNames.RegistrationNo, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = TransChargesVisiteItemRealizationMetadata.PropertyNames.RegistrationNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesVisiteItemRealizationMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = TransChargesVisiteItemRealizationMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesVisiteItemRealizationMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = TransChargesVisiteItemRealizationMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public TransChargesVisiteItemRealizationMetadata Meta()
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
            public const string ItemID = "ItemID";
            public const string RegistrationNo = "RegistrationNo";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string TransactionNo = "TransactionNo";
            public const string ItemID = "ItemID";
            public const string RegistrationNo = "RegistrationNo";
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
            lock (typeof(TransChargesVisiteItemRealizationMetadata))
            {
                if (TransChargesVisiteItemRealizationMetadata.mapDelegates == null)
                {
                    TransChargesVisiteItemRealizationMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (TransChargesVisiteItemRealizationMetadata.meta == null)
                {
                    TransChargesVisiteItemRealizationMetadata.meta = new TransChargesVisiteItemRealizationMetadata();
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
                meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "TransChargesVisiteItemRealization";
                meta.Destination = "TransChargesVisiteItemRealization";
                meta.spInsert = "proc_TransChargesVisiteItemRealizationInsert";
                meta.spUpdate = "proc_TransChargesVisiteItemRealizationUpdate";
                meta.spDelete = "proc_TransChargesVisiteItemRealizationDelete";
                meta.spLoadAll = "proc_TransChargesVisiteItemRealizationLoadAll";
                meta.spLoadByPrimaryKey = "proc_TransChargesVisiteItemRealizationLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private TransChargesVisiteItemRealizationMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
