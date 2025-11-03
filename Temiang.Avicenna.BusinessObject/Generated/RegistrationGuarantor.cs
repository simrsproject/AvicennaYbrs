/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 5/9/2017 1:44:22 PM
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
    abstract public class esRegistrationGuarantorCollection : esEntityCollectionWAuditLog
    {
        public esRegistrationGuarantorCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "RegistrationGuarantorCollection";
        }

        #region Query Logic
        protected void InitQuery(esRegistrationGuarantorQuery query)
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
            this.InitQuery(query as esRegistrationGuarantorQuery);
        }
        #endregion

        virtual public RegistrationGuarantor DetachEntity(RegistrationGuarantor entity)
        {
            return base.DetachEntity(entity) as RegistrationGuarantor;
        }

        virtual public RegistrationGuarantor AttachEntity(RegistrationGuarantor entity)
        {
            return base.AttachEntity(entity) as RegistrationGuarantor;
        }

        virtual public void Combine(RegistrationGuarantorCollection collection)
        {
            base.Combine(collection);
        }

        new public RegistrationGuarantor this[int index]
        {
            get
            {
                return base[index] as RegistrationGuarantor;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(RegistrationGuarantor);
        }
    }

    [Serializable]
    abstract public class esRegistrationGuarantor : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esRegistrationGuarantorQuery GetDynamicQuery()
        {
            return null;
        }

        public esRegistrationGuarantor()
        {
        }

        public esRegistrationGuarantor(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String registrationNo, String guarantorID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(registrationNo, guarantorID);
            else
                return LoadByPrimaryKeyStoredProcedure(registrationNo, guarantorID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String registrationNo, String guarantorID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(registrationNo, guarantorID);
            else
                return LoadByPrimaryKeyStoredProcedure(registrationNo, guarantorID);
        }

        private bool LoadByPrimaryKeyDynamic(String registrationNo, String guarantorID)
        {
            esRegistrationGuarantorQuery query = this.GetDynamicQuery();
            query.Where(query.RegistrationNo == registrationNo, query.GuarantorID == guarantorID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String registrationNo, String guarantorID)
        {
            esParameters parms = new esParameters();
            parms.Add("RegistrationNo", registrationNo);
            parms.Add("GuarantorID", guarantorID);
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
                        case "GuarantorID": this.str.GuarantorID = (string)value; break;
                        case "PlafondAmount": this.str.PlafondAmount = (string)value; break;
                        case "Notes": this.str.Notes = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
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
        /// Maps to RegistrationGuarantor.RegistrationNo
        /// </summary>
        virtual public System.String RegistrationNo
        {
            get
            {
                return base.GetSystemString(RegistrationGuarantorMetadata.ColumnNames.RegistrationNo);
            }

            set
            {
                base.SetSystemString(RegistrationGuarantorMetadata.ColumnNames.RegistrationNo, value);
            }
        }
        /// <summary>
        /// Maps to RegistrationGuarantor.GuarantorID
        /// </summary>
        virtual public System.String GuarantorID
        {
            get
            {
                return base.GetSystemString(RegistrationGuarantorMetadata.ColumnNames.GuarantorID);
            }

            set
            {
                base.SetSystemString(RegistrationGuarantorMetadata.ColumnNames.GuarantorID, value);
            }
        }
        /// <summary>
        /// Maps to RegistrationGuarantor.PlafondAmount
        /// </summary>
        virtual public System.Decimal? PlafondAmount
        {
            get
            {
                return base.GetSystemDecimal(RegistrationGuarantorMetadata.ColumnNames.PlafondAmount);
            }

            set
            {
                base.SetSystemDecimal(RegistrationGuarantorMetadata.ColumnNames.PlafondAmount, value);
            }
        }
        /// <summary>
        /// Maps to RegistrationGuarantor.Notes
        /// </summary>
        virtual public System.String Notes
        {
            get
            {
                return base.GetSystemString(RegistrationGuarantorMetadata.ColumnNames.Notes);
            }

            set
            {
                base.SetSystemString(RegistrationGuarantorMetadata.ColumnNames.Notes, value);
            }
        }
        /// <summary>
        /// Maps to RegistrationGuarantor.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(RegistrationGuarantorMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(RegistrationGuarantorMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to RegistrationGuarantor.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(RegistrationGuarantorMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(RegistrationGuarantorMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esRegistrationGuarantor entity)
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
            private esRegistrationGuarantor entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esRegistrationGuarantorQuery query)
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
                throw new Exception("esRegistrationGuarantor can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class RegistrationGuarantor : esRegistrationGuarantor
    {
    }

    [Serializable]
    abstract public class esRegistrationGuarantorQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return RegistrationGuarantorMetadata.Meta();
            }
        }

        public esQueryItem RegistrationNo
        {
            get
            {
                return new esQueryItem(this, RegistrationGuarantorMetadata.ColumnNames.RegistrationNo, esSystemType.String);
            }
        }

        public esQueryItem GuarantorID
        {
            get
            {
                return new esQueryItem(this, RegistrationGuarantorMetadata.ColumnNames.GuarantorID, esSystemType.String);
            }
        }

        public esQueryItem PlafondAmount
        {
            get
            {
                return new esQueryItem(this, RegistrationGuarantorMetadata.ColumnNames.PlafondAmount, esSystemType.Decimal);
            }
        }

        public esQueryItem Notes
        {
            get
            {
                return new esQueryItem(this, RegistrationGuarantorMetadata.ColumnNames.Notes, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, RegistrationGuarantorMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, RegistrationGuarantorMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("RegistrationGuarantorCollection")]
    public partial class RegistrationGuarantorCollection : esRegistrationGuarantorCollection, IEnumerable<RegistrationGuarantor>
    {
        public RegistrationGuarantorCollection()
        {

        }

        public static implicit operator List<RegistrationGuarantor>(RegistrationGuarantorCollection coll)
        {
            List<RegistrationGuarantor> list = new List<RegistrationGuarantor>();

            foreach (RegistrationGuarantor emp in coll)
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
                return RegistrationGuarantorMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new RegistrationGuarantorQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new RegistrationGuarantor(row);
        }

        override protected esEntity CreateEntity()
        {
            return new RegistrationGuarantor();
        }

        #endregion

        [BrowsableAttribute(false)]
        public RegistrationGuarantorQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new RegistrationGuarantorQuery();
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
        public bool Load(RegistrationGuarantorQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public RegistrationGuarantor AddNew()
        {
            RegistrationGuarantor entity = base.AddNewEntity() as RegistrationGuarantor;

            return entity;
        }
        public RegistrationGuarantor FindByPrimaryKey(String registrationNo, String guarantorID)
        {
            return base.FindByPrimaryKey(registrationNo, guarantorID) as RegistrationGuarantor;
        }

        #region IEnumerable< RegistrationGuarantor> Members

        IEnumerator<RegistrationGuarantor> IEnumerable<RegistrationGuarantor>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as RegistrationGuarantor;
            }
        }

        #endregion

        private RegistrationGuarantorQuery query;
    }


    /// <summary>
    /// Encapsulates the 'RegistrationGuarantor' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("RegistrationGuarantor ({RegistrationNo, GuarantorID})")]
    [Serializable]
    public partial class RegistrationGuarantor : esRegistrationGuarantor
    {
        public RegistrationGuarantor()
        {
        }

        public RegistrationGuarantor(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return RegistrationGuarantorMetadata.Meta();
            }
        }

        override protected esRegistrationGuarantorQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new RegistrationGuarantorQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public RegistrationGuarantorQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new RegistrationGuarantorQuery();
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
        public bool Load(RegistrationGuarantorQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private RegistrationGuarantorQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class RegistrationGuarantorQuery : esRegistrationGuarantorQuery
    {
        public RegistrationGuarantorQuery()
        {

        }

        public RegistrationGuarantorQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "RegistrationGuarantorQuery";
        }
    }

    [Serializable]
    public partial class RegistrationGuarantorMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected RegistrationGuarantorMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(RegistrationGuarantorMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = RegistrationGuarantorMetadata.PropertyNames.RegistrationNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationGuarantorMetadata.ColumnNames.GuarantorID, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = RegistrationGuarantorMetadata.PropertyNames.GuarantorID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationGuarantorMetadata.ColumnNames.PlafondAmount, 2, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = RegistrationGuarantorMetadata.PropertyNames.PlafondAmount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationGuarantorMetadata.ColumnNames.Notes, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = RegistrationGuarantorMetadata.PropertyNames.Notes;
            c.CharacterMaxLength = 500;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationGuarantorMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = RegistrationGuarantorMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationGuarantorMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = RegistrationGuarantorMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public RegistrationGuarantorMetadata Meta()
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
            public const string GuarantorID = "GuarantorID";
            public const string PlafondAmount = "PlafondAmount";
            public const string Notes = "Notes";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string RegistrationNo = "RegistrationNo";
            public const string GuarantorID = "GuarantorID";
            public const string PlafondAmount = "PlafondAmount";
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
            lock (typeof(RegistrationGuarantorMetadata))
            {
                if (RegistrationGuarantorMetadata.mapDelegates == null)
                {
                    RegistrationGuarantorMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (RegistrationGuarantorMetadata.meta == null)
                {
                    RegistrationGuarantorMetadata.meta = new RegistrationGuarantorMetadata();
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
                meta.AddTypeMap("GuarantorID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("PlafondAmount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "RegistrationGuarantor";
                meta.Destination = "RegistrationGuarantor";
                meta.spInsert = "proc_RegistrationGuarantorInsert";
                meta.spUpdate = "proc_RegistrationGuarantorUpdate";
                meta.spDelete = "proc_RegistrationGuarantorDelete";
                meta.spLoadAll = "proc_RegistrationGuarantorLoadAll";
                meta.spLoadByPrimaryKey = "proc_RegistrationGuarantorLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private RegistrationGuarantorMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
