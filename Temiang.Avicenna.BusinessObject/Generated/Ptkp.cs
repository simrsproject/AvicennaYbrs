/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 9/3/2016 9:42:35 AM
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
    abstract public class esPtkpCollection : esEntityCollectionWAuditLog
    {
        public esPtkpCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "PtkpCollection";
        }

        #region Query Logic
        protected void InitQuery(esPtkpQuery query)
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
            this.InitQuery(query as esPtkpQuery);
        }
        #endregion

        virtual public Ptkp DetachEntity(Ptkp entity)
        {
            return base.DetachEntity(entity) as Ptkp;
        }

        virtual public Ptkp AttachEntity(Ptkp entity)
        {
            return base.AttachEntity(entity) as Ptkp;
        }

        virtual public void Combine(PtkpCollection collection)
        {
            base.Combine(collection);
        }

        new public Ptkp this[int index]
        {
            get
            {
                return base[index] as Ptkp;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(Ptkp);
        }
    }

    [Serializable]
    abstract public class esPtkp : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esPtkpQuery GetDynamicQuery()
        {
            return null;
        }

        public esPtkp()
        {
        }

        public esPtkp(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(Int32 ptkpID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(ptkpID);
            else
                return LoadByPrimaryKeyStoredProcedure(ptkpID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 ptkpID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(ptkpID);
            else
                return LoadByPrimaryKeyStoredProcedure(ptkpID);
        }

        private bool LoadByPrimaryKeyDynamic(Int32 ptkpID)
        {
            esPtkpQuery query = this.GetDynamicQuery();
            query.Where(query.PtkpID == ptkpID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(Int32 ptkpID)
        {
            esParameters parms = new esParameters();
            parms.Add("PtkpID", ptkpID);
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
                        case "PtkpID": this.str.PtkpID = (string)value; break;
                        case "ValidFrom": this.str.ValidFrom = (string)value; break;
                        case "ValidTo": this.str.ValidTo = (string)value; break;
                        case "SRTaxStatus": this.str.SRTaxStatus = (string)value; break;
                        case "Amount": this.str.Amount = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "PtkpID":

                            if (value == null || value is System.Int32)
                                this.PtkpID = (System.Int32?)value;
                            break;
                        case "ValidFrom":

                            if (value == null || value is System.DateTime)
                                this.ValidFrom = (System.DateTime?)value;
                            break;
                        case "ValidTo":

                            if (value == null || value is System.DateTime)
                                this.ValidTo = (System.DateTime?)value;
                            break;
                        case "Amount":

                            if (value == null || value is System.Decimal)
                                this.Amount = (System.Decimal?)value;
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
        /// Maps to Ptkp.PtkpID
        /// </summary>
        virtual public System.Int32? PtkpID
        {
            get
            {
                return base.GetSystemInt32(PtkpMetadata.ColumnNames.PtkpID);
            }

            set
            {
                base.SetSystemInt32(PtkpMetadata.ColumnNames.PtkpID, value);
            }
        }
        /// <summary>
        /// Maps to Ptkp.ValidFrom
        /// </summary>
        virtual public System.DateTime? ValidFrom
        {
            get
            {
                return base.GetSystemDateTime(PtkpMetadata.ColumnNames.ValidFrom);
            }

            set
            {
                base.SetSystemDateTime(PtkpMetadata.ColumnNames.ValidFrom, value);
            }
        }
        /// <summary>
        /// Maps to Ptkp.ValidTo
        /// </summary>
        virtual public System.DateTime? ValidTo
        {
            get
            {
                return base.GetSystemDateTime(PtkpMetadata.ColumnNames.ValidTo);
            }

            set
            {
                base.SetSystemDateTime(PtkpMetadata.ColumnNames.ValidTo, value);
            }
        }
        /// <summary>
        /// Maps to Ptkp.SRTaxStatus
        /// </summary>
        virtual public System.String SRTaxStatus
        {
            get
            {
                return base.GetSystemString(PtkpMetadata.ColumnNames.SRTaxStatus);
            }

            set
            {
                base.SetSystemString(PtkpMetadata.ColumnNames.SRTaxStatus, value);
            }
        }
        /// <summary>
        /// Maps to Ptkp.Amount
        /// </summary>
        virtual public System.Decimal? Amount
        {
            get
            {
                return base.GetSystemDecimal(PtkpMetadata.ColumnNames.Amount);
            }

            set
            {
                base.SetSystemDecimal(PtkpMetadata.ColumnNames.Amount, value);
            }
        }
        /// <summary>
        /// Maps to Ptkp.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(PtkpMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(PtkpMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to Ptkp.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(PtkpMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(PtkpMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esPtkp entity)
            {
                this.entity = entity;
            }
            public System.String PtkpID
            {
                get
                {
                    System.Int32? data = entity.PtkpID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PtkpID = null;
                    else entity.PtkpID = Convert.ToInt32(value);
                }
            }
            public System.String ValidFrom
            {
                get
                {
                    System.DateTime? data = entity.ValidFrom;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ValidFrom = null;
                    else entity.ValidFrom = Convert.ToDateTime(value);
                }
            }
            public System.String ValidTo
            {
                get
                {
                    System.DateTime? data = entity.ValidTo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ValidTo = null;
                    else entity.ValidTo = Convert.ToDateTime(value);
                }
            }
            public System.String SRTaxStatus
            {
                get
                {
                    System.String data = entity.SRTaxStatus;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRTaxStatus = null;
                    else entity.SRTaxStatus = Convert.ToString(value);
                }
            }
            public System.String Amount
            {
                get
                {
                    System.Decimal? data = entity.Amount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Amount = null;
                    else entity.Amount = Convert.ToDecimal(value);
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
            private esPtkp entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esPtkpQuery query)
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
                throw new Exception("esPtkp can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class Ptkp : esPtkp
    {
    }

    [Serializable]
    abstract public class esPtkpQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return PtkpMetadata.Meta();
            }
        }

        public esQueryItem PtkpID
        {
            get
            {
                return new esQueryItem(this, PtkpMetadata.ColumnNames.PtkpID, esSystemType.Int32);
            }
        }

        public esQueryItem ValidFrom
        {
            get
            {
                return new esQueryItem(this, PtkpMetadata.ColumnNames.ValidFrom, esSystemType.DateTime);
            }
        }

        public esQueryItem ValidTo
        {
            get
            {
                return new esQueryItem(this, PtkpMetadata.ColumnNames.ValidTo, esSystemType.DateTime);
            }
        }

        public esQueryItem SRTaxStatus
        {
            get
            {
                return new esQueryItem(this, PtkpMetadata.ColumnNames.SRTaxStatus, esSystemType.String);
            }
        }

        public esQueryItem Amount
        {
            get
            {
                return new esQueryItem(this, PtkpMetadata.ColumnNames.Amount, esSystemType.Decimal);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, PtkpMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, PtkpMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("PtkpCollection")]
    public partial class PtkpCollection : esPtkpCollection, IEnumerable<Ptkp>
    {
        public PtkpCollection()
        {

        }

        public static implicit operator List<Ptkp>(PtkpCollection coll)
        {
            List<Ptkp> list = new List<Ptkp>();

            foreach (Ptkp emp in coll)
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
                return PtkpMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PtkpQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new Ptkp(row);
        }

        override protected esEntity CreateEntity()
        {
            return new Ptkp();
        }

        #endregion

        [BrowsableAttribute(false)]
        public PtkpQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PtkpQuery();
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
        public bool Load(PtkpQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public Ptkp AddNew()
        {
            Ptkp entity = base.AddNewEntity() as Ptkp;

            return entity;
        }
        public Ptkp FindByPrimaryKey(Int32 ptkpID)
        {
            return base.FindByPrimaryKey(ptkpID) as Ptkp;
        }

        #region IEnumerable< Ptkp> Members

        IEnumerator<Ptkp> IEnumerable<Ptkp>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as Ptkp;
            }
        }

        #endregion

        private PtkpQuery query;
    }


    /// <summary>
    /// Encapsulates the 'Ptkp' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("Ptkp ({PtkpID})")]
    [Serializable]
    public partial class Ptkp : esPtkp
    {
        public Ptkp()
        {
        }

        public Ptkp(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return PtkpMetadata.Meta();
            }
        }

        override protected esPtkpQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PtkpQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public PtkpQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PtkpQuery();
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
        public bool Load(PtkpQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private PtkpQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class PtkpQuery : esPtkpQuery
    {
        public PtkpQuery()
        {

        }

        public PtkpQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "PtkpQuery";
        }
    }

    [Serializable]
    public partial class PtkpMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected PtkpMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(PtkpMetadata.ColumnNames.PtkpID, 0, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = PtkpMetadata.PropertyNames.PtkpID;
            c.IsInPrimaryKey = true;
            c.IsAutoIncrement = true;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(PtkpMetadata.ColumnNames.ValidFrom, 1, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PtkpMetadata.PropertyNames.ValidFrom;
            _columns.Add(c);

            c = new esColumnMetadata(PtkpMetadata.ColumnNames.ValidTo, 2, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PtkpMetadata.PropertyNames.ValidTo;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PtkpMetadata.ColumnNames.SRTaxStatus, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = PtkpMetadata.PropertyNames.SRTaxStatus;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(PtkpMetadata.ColumnNames.Amount, 4, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = PtkpMetadata.PropertyNames.Amount;
            c.NumericPrecision = 19;
            c.NumericScale = 4;
            _columns.Add(c);

            c = new esColumnMetadata(PtkpMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PtkpMetadata.PropertyNames.LastUpdateDateTime;
            _columns.Add(c);

            c = new esColumnMetadata(PtkpMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = PtkpMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 40;
            _columns.Add(c);


        }
        #endregion

        static public PtkpMetadata Meta()
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
            public const string PtkpID = "PtkpID";
            public const string ValidFrom = "ValidFrom";
            public const string ValidTo = "ValidTo";
            public const string SRTaxStatus = "SRTaxStatus";
            public const string Amount = "Amount";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string PtkpID = "PtkpID";
            public const string ValidFrom = "ValidFrom";
            public const string ValidTo = "ValidTo";
            public const string SRTaxStatus = "SRTaxStatus";
            public const string Amount = "Amount";
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
            lock (typeof(PtkpMetadata))
            {
                if (PtkpMetadata.mapDelegates == null)
                {
                    PtkpMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (PtkpMetadata.meta == null)
                {
                    PtkpMetadata.meta = new PtkpMetadata();
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

                meta.AddTypeMap("PtkpID", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("ValidFrom", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("ValidTo", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("SRTaxStatus", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Amount", new esTypeMap("money", "System.Decimal"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "Ptkp";
                meta.Destination = "Ptkp";
                meta.spInsert = "proc_PtkpInsert";
                meta.spUpdate = "proc_PtkpUpdate";
                meta.spDelete = "proc_PtkpDelete";
                meta.spLoadAll = "proc_PtkpLoadAll";
                meta.spLoadByPrimaryKey = "proc_PtkpLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private PtkpMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
