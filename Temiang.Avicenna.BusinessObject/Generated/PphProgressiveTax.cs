/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 9/26/2017 3:32:42 PM
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
    abstract public class esPphProgressiveTaxCollection : esEntityCollectionWAuditLog
    {
        public esPphProgressiveTaxCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "PphProgressiveTaxCollection";
        }

        #region Query Logic
        protected void InitQuery(esPphProgressiveTaxQuery query)
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
            this.InitQuery(query as esPphProgressiveTaxQuery);
        }
        #endregion

        virtual public PphProgressiveTax DetachEntity(PphProgressiveTax entity)
        {
            return base.DetachEntity(entity) as PphProgressiveTax;
        }

        virtual public PphProgressiveTax AttachEntity(PphProgressiveTax entity)
        {
            return base.AttachEntity(entity) as PphProgressiveTax;
        }

        virtual public void Combine(PphProgressiveTaxCollection collection)
        {
            base.Combine(collection);
        }

        new public PphProgressiveTax this[int index]
        {
            get
            {
                return base[index] as PphProgressiveTax;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(PphProgressiveTax);
        }
    }

    [Serializable]
    abstract public class esPphProgressiveTax : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esPphProgressiveTaxQuery GetDynamicQuery()
        {
            return null;
        }

        public esPphProgressiveTax()
        {
        }

        public esPphProgressiveTax(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(Int32 counterID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(counterID);
            else
                return LoadByPrimaryKeyStoredProcedure(counterID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 counterID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(counterID);
            else
                return LoadByPrimaryKeyStoredProcedure(counterID);
        }

        private bool LoadByPrimaryKeyDynamic(Int32 counterID)
        {
            esPphProgressiveTaxQuery query = this.GetDynamicQuery();
            query.Where(query.CounterID == counterID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(Int32 counterID)
        {
            esParameters parms = new esParameters();
            parms.Add("CounterID", counterID);
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
                        case "CounterID": this.str.CounterID = (string)value; break;
                        case "MinAmount": this.str.MinAmount = (string)value; break;
                        case "MaxAmount": this.str.MaxAmount = (string)value; break;
                        case "Percentage": this.str.Percentage = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "CounterID":

                            if (value == null || value is System.Int32)
                                this.CounterID = (System.Int32?)value;
                            break;
                        case "MinAmount":

                            if (value == null || value is System.Decimal)
                                this.MinAmount = (System.Decimal?)value;
                            break;
                        case "MaxAmount":

                            if (value == null || value is System.Decimal)
                                this.MaxAmount = (System.Decimal?)value;
                            break;
                        case "Percentage":

                            if (value == null || value is System.Decimal)
                                this.Percentage = (System.Decimal?)value;
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
        /// Maps to PphProgressiveTax.CounterID
        /// </summary>
        virtual public System.Int32? CounterID
        {
            get
            {
                return base.GetSystemInt32(PphProgressiveTaxMetadata.ColumnNames.CounterID);
            }

            set
            {
                base.SetSystemInt32(PphProgressiveTaxMetadata.ColumnNames.CounterID, value);
            }
        }
        /// <summary>
        /// Maps to PphProgressiveTax.MinAmount
        /// </summary>
        virtual public System.Decimal? MinAmount
        {
            get
            {
                return base.GetSystemDecimal(PphProgressiveTaxMetadata.ColumnNames.MinAmount);
            }

            set
            {
                base.SetSystemDecimal(PphProgressiveTaxMetadata.ColumnNames.MinAmount, value);
            }
        }
        /// <summary>
        /// Maps to PphProgressiveTax.MaxAmount
        /// </summary>
        virtual public System.Decimal? MaxAmount
        {
            get
            {
                return base.GetSystemDecimal(PphProgressiveTaxMetadata.ColumnNames.MaxAmount);
            }

            set
            {
                base.SetSystemDecimal(PphProgressiveTaxMetadata.ColumnNames.MaxAmount, value);
            }
        }
        /// <summary>
        /// Maps to PphProgressiveTax.Percentage
        /// </summary>
        virtual public System.Decimal? Percentage
        {
            get
            {
                return base.GetSystemDecimal(PphProgressiveTaxMetadata.ColumnNames.Percentage);
            }

            set
            {
                base.SetSystemDecimal(PphProgressiveTaxMetadata.ColumnNames.Percentage, value);
            }
        }
        /// <summary>
        /// Maps to PphProgressiveTax.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(PphProgressiveTaxMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(PphProgressiveTaxMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to PphProgressiveTax.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(PphProgressiveTaxMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(PphProgressiveTaxMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esPphProgressiveTax entity)
            {
                this.entity = entity;
            }
            public System.String CounterID
            {
                get
                {
                    System.Int32? data = entity.CounterID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CounterID = null;
                    else entity.CounterID = Convert.ToInt32(value);
                }
            }
            public System.String MinAmount
            {
                get
                {
                    System.Decimal? data = entity.MinAmount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.MinAmount = null;
                    else entity.MinAmount = Convert.ToDecimal(value);
                }
            }
            public System.String MaxAmount
            {
                get
                {
                    System.Decimal? data = entity.MaxAmount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.MaxAmount = null;
                    else entity.MaxAmount = Convert.ToDecimal(value);
                }
            }
            public System.String Percentage
            {
                get
                {
                    System.Decimal? data = entity.Percentage;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Percentage = null;
                    else entity.Percentage = Convert.ToDecimal(value);
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
            private esPphProgressiveTax entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esPphProgressiveTaxQuery query)
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
                throw new Exception("esPphProgressiveTax can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class PphProgressiveTax : esPphProgressiveTax
    {
    }

    [Serializable]
    abstract public class esPphProgressiveTaxQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return PphProgressiveTaxMetadata.Meta();
            }
        }

        public esQueryItem CounterID
        {
            get
            {
                return new esQueryItem(this, PphProgressiveTaxMetadata.ColumnNames.CounterID, esSystemType.Int32);
            }
        }

        public esQueryItem MinAmount
        {
            get
            {
                return new esQueryItem(this, PphProgressiveTaxMetadata.ColumnNames.MinAmount, esSystemType.Decimal);
            }
        }

        public esQueryItem MaxAmount
        {
            get
            {
                return new esQueryItem(this, PphProgressiveTaxMetadata.ColumnNames.MaxAmount, esSystemType.Decimal);
            }
        }

        public esQueryItem Percentage
        {
            get
            {
                return new esQueryItem(this, PphProgressiveTaxMetadata.ColumnNames.Percentage, esSystemType.Decimal);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, PphProgressiveTaxMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, PphProgressiveTaxMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("PphProgressiveTaxCollection")]
    public partial class PphProgressiveTaxCollection : esPphProgressiveTaxCollection, IEnumerable<PphProgressiveTax>
    {
        public PphProgressiveTaxCollection()
        {

        }

        public static implicit operator List<PphProgressiveTax>(PphProgressiveTaxCollection coll)
        {
            List<PphProgressiveTax> list = new List<PphProgressiveTax>();

            foreach (PphProgressiveTax emp in coll)
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
                return PphProgressiveTaxMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PphProgressiveTaxQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new PphProgressiveTax(row);
        }

        override protected esEntity CreateEntity()
        {
            return new PphProgressiveTax();
        }

        #endregion

        [BrowsableAttribute(false)]
        public PphProgressiveTaxQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PphProgressiveTaxQuery();
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
        public bool Load(PphProgressiveTaxQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public PphProgressiveTax AddNew()
        {
            PphProgressiveTax entity = base.AddNewEntity() as PphProgressiveTax;

            return entity;
        }
        public PphProgressiveTax FindByPrimaryKey(Int32 counterID)
        {
            return base.FindByPrimaryKey(counterID) as PphProgressiveTax;
        }

        #region IEnumerable< PphProgressiveTax> Members

        IEnumerator<PphProgressiveTax> IEnumerable<PphProgressiveTax>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as PphProgressiveTax;
            }
        }

        #endregion

        private PphProgressiveTaxQuery query;
    }


    /// <summary>
    /// Encapsulates the 'PphProgressiveTax' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("PphProgressiveTax ({CounterID})")]
    [Serializable]
    public partial class PphProgressiveTax : esPphProgressiveTax
    {
        public PphProgressiveTax()
        {
        }

        public PphProgressiveTax(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return PphProgressiveTaxMetadata.Meta();
            }
        }

        override protected esPphProgressiveTaxQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PphProgressiveTaxQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public PphProgressiveTaxQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PphProgressiveTaxQuery();
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
        public bool Load(PphProgressiveTaxQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private PphProgressiveTaxQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class PphProgressiveTaxQuery : esPphProgressiveTaxQuery
    {
        public PphProgressiveTaxQuery()
        {

        }

        public PphProgressiveTaxQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "PphProgressiveTaxQuery";
        }
    }

    [Serializable]
    public partial class PphProgressiveTaxMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected PphProgressiveTaxMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(PphProgressiveTaxMetadata.ColumnNames.CounterID, 0, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = PphProgressiveTaxMetadata.PropertyNames.CounterID;
            c.IsInPrimaryKey = true;
            c.IsAutoIncrement = true;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(PphProgressiveTaxMetadata.ColumnNames.MinAmount, 1, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = PphProgressiveTaxMetadata.PropertyNames.MinAmount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PphProgressiveTaxMetadata.ColumnNames.MaxAmount, 2, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = PphProgressiveTaxMetadata.PropertyNames.MaxAmount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PphProgressiveTaxMetadata.ColumnNames.Percentage, 3, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = PphProgressiveTaxMetadata.PropertyNames.Percentage;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PphProgressiveTaxMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PphProgressiveTaxMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PphProgressiveTaxMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = PphProgressiveTaxMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public PphProgressiveTaxMetadata Meta()
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
            public const string CounterID = "CounterID";
            public const string MinAmount = "MinAmount";
            public const string MaxAmount = "MaxAmount";
            public const string Percentage = "Percentage";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string CounterID = "CounterID";
            public const string MinAmount = "MinAmount";
            public const string MaxAmount = "MaxAmount";
            public const string Percentage = "Percentage";
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
            lock (typeof(PphProgressiveTaxMetadata))
            {
                if (PphProgressiveTaxMetadata.mapDelegates == null)
                {
                    PphProgressiveTaxMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (PphProgressiveTaxMetadata.meta == null)
                {
                    PphProgressiveTaxMetadata.meta = new PphProgressiveTaxMetadata();
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

                meta.AddTypeMap("CounterID", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("MinAmount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("MaxAmount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("Percentage", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "PphProgressiveTax";
                meta.Destination = "PphProgressiveTax";
                meta.spInsert = "proc_PphProgressiveTaxInsert";
                meta.spUpdate = "proc_PphProgressiveTaxUpdate";
                meta.spDelete = "proc_PphProgressiveTaxDelete";
                meta.spLoadAll = "proc_PphProgressiveTaxLoadAll";
                meta.spLoadByPrimaryKey = "proc_PphProgressiveTaxLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private PphProgressiveTaxMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
