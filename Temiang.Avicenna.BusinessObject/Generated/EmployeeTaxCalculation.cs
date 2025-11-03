/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 2/12/2024 11:27:40 AM
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
    abstract public class esEmployeeTaxCalculationCollection : esEntityCollectionWAuditLog
    {
        public esEmployeeTaxCalculationCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "EmployeeTaxCalculationCollection";
        }

        #region Query Logic
        protected void InitQuery(esEmployeeTaxCalculationQuery query)
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
            this.InitQuery(query as esEmployeeTaxCalculationQuery);
        }
        #endregion

        virtual public EmployeeTaxCalculation DetachEntity(EmployeeTaxCalculation entity)
        {
            return base.DetachEntity(entity) as EmployeeTaxCalculation;
        }

        virtual public EmployeeTaxCalculation AttachEntity(EmployeeTaxCalculation entity)
        {
            return base.AttachEntity(entity) as EmployeeTaxCalculation;
        }

        virtual public void Combine(EmployeeTaxCalculationCollection collection)
        {
            base.Combine(collection);
        }

        new public EmployeeTaxCalculation this[int index]
        {
            get
            {
                return base[index] as EmployeeTaxCalculation;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(EmployeeTaxCalculation);
        }
    }

    [Serializable]
    abstract public class esEmployeeTaxCalculation : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esEmployeeTaxCalculationQuery GetDynamicQuery()
        {
            return null;
        }

        public esEmployeeTaxCalculation()
        {
        }

        public esEmployeeTaxCalculation(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(Int32 personID, Int32 wageProcessTypeID, Int32 sPTYear, Int32 sPTMonth)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(personID, wageProcessTypeID, sPTYear, sPTMonth);
            else
                return LoadByPrimaryKeyStoredProcedure(personID, wageProcessTypeID, sPTYear, sPTMonth);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 personID, Int32 wageProcessTypeID, Int32 sPTYear, Int32 sPTMonth)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(personID, wageProcessTypeID, sPTYear, sPTMonth);
            else
                return LoadByPrimaryKeyStoredProcedure(personID, wageProcessTypeID, sPTYear, sPTMonth);
        }

        private bool LoadByPrimaryKeyDynamic(Int32 personID, Int32 wageProcessTypeID, Int32 sPTYear, Int32 sPTMonth)
        {
            esEmployeeTaxCalculationQuery query = this.GetDynamicQuery();
            query.Where(query.PersonID == personID, query.WageProcessTypeID == wageProcessTypeID, query.SPTYear == sPTYear, query.SPTMonth == sPTMonth);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(Int32 personID, Int32 wageProcessTypeID, Int32 sPTYear, Int32 sPTMonth)
        {
            esParameters parms = new esParameters();
            parms.Add("PersonID", personID);
            parms.Add("WageProcessTypeID", wageProcessTypeID);
            parms.Add("SPTYear", sPTYear);
            parms.Add("SPTMonth", sPTMonth);
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
                        case "PersonID": this.str.PersonID = (string)value; break;
                        case "WageProcessTypeID": this.str.WageProcessTypeID = (string)value; break;
                        case "SPTYear": this.str.SPTYear = (string)value; break;
                        case "SPTMonth": this.str.SPTMonth = (string)value; break;
                        case "GrossIncome": this.str.GrossIncome = (string)value; break;
                        case "TaxRate": this.str.TaxRate = (string)value; break;
                        case "TaxAmount": this.str.TaxAmount = (string)value; break;
                        case "Deduction": this.str.Deduction = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "PersonID":

                            if (value == null || value is System.Int32)
                                this.PersonID = (System.Int32?)value;
                            break;
                        case "WageProcessTypeID":

                            if (value == null || value is System.Int32)
                                this.WageProcessTypeID = (System.Int32?)value;
                            break;
                        case "SPTYear":

                            if (value == null || value is System.Int32)
                                this.SPTYear = (System.Int32?)value;
                            break;
                        case "SPTMonth":

                            if (value == null || value is System.Int32)
                                this.SPTMonth = (System.Int32?)value;
                            break;
                        case "GrossIncome":

                            if (value == null || value is System.Decimal)
                                this.GrossIncome = (System.Decimal?)value;
                            break;
                        case "TaxRate":

                            if (value == null || value is System.Decimal)
                                this.TaxRate = (System.Decimal?)value;
                            break;
                        case "TaxAmount":

                            if (value == null || value is System.Decimal)
                                this.TaxAmount = (System.Decimal?)value;
                            break;
                        case "Deduction":

                            if (value == null || value is System.Decimal)
                                this.Deduction = (System.Decimal?)value;
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
        /// Maps to EmployeeTaxCalculation.PersonID
        /// </summary>
        virtual public System.Int32? PersonID
        {
            get
            {
                return base.GetSystemInt32(EmployeeTaxCalculationMetadata.ColumnNames.PersonID);
            }

            set
            {
                base.SetSystemInt32(EmployeeTaxCalculationMetadata.ColumnNames.PersonID, value);
            }
        }
        /// <summary>
        /// Maps to EmployeeTaxCalculation.WageProcessTypeID
        /// </summary>
        virtual public System.Int32? WageProcessTypeID
        {
            get
            {
                return base.GetSystemInt32(EmployeeTaxCalculationMetadata.ColumnNames.WageProcessTypeID);
            }

            set
            {
                base.SetSystemInt32(EmployeeTaxCalculationMetadata.ColumnNames.WageProcessTypeID, value);
            }
        }
        /// <summary>
        /// Maps to EmployeeTaxCalculation.SPTYear
        /// </summary>
        virtual public System.Int32? SPTYear
        {
            get
            {
                return base.GetSystemInt32(EmployeeTaxCalculationMetadata.ColumnNames.SPTYear);
            }

            set
            {
                base.SetSystemInt32(EmployeeTaxCalculationMetadata.ColumnNames.SPTYear, value);
            }
        }
        /// <summary>
        /// Maps to EmployeeTaxCalculation.SPTMonth
        /// </summary>
        virtual public System.Int32? SPTMonth
        {
            get
            {
                return base.GetSystemInt32(EmployeeTaxCalculationMetadata.ColumnNames.SPTMonth);
            }

            set
            {
                base.SetSystemInt32(EmployeeTaxCalculationMetadata.ColumnNames.SPTMonth, value);
            }
        }
        /// <summary>
        /// Maps to EmployeeTaxCalculation.GrossIncome
        /// </summary>
        virtual public System.Decimal? GrossIncome
        {
            get
            {
                return base.GetSystemDecimal(EmployeeTaxCalculationMetadata.ColumnNames.GrossIncome);
            }

            set
            {
                base.SetSystemDecimal(EmployeeTaxCalculationMetadata.ColumnNames.GrossIncome, value);
            }
        }
        /// <summary>
        /// Maps to EmployeeTaxCalculation.TaxRate
        /// </summary>
        virtual public System.Decimal? TaxRate
        {
            get
            {
                return base.GetSystemDecimal(EmployeeTaxCalculationMetadata.ColumnNames.TaxRate);
            }

            set
            {
                base.SetSystemDecimal(EmployeeTaxCalculationMetadata.ColumnNames.TaxRate, value);
            }
        }
        /// <summary>
        /// Maps to EmployeeTaxCalculation.TaxAmount
        /// </summary>
        virtual public System.Decimal? TaxAmount
        {
            get
            {
                return base.GetSystemDecimal(EmployeeTaxCalculationMetadata.ColumnNames.TaxAmount);
            }

            set
            {
                base.SetSystemDecimal(EmployeeTaxCalculationMetadata.ColumnNames.TaxAmount, value);
            }
        }
        /// <summary>
        /// Maps to EmployeeTaxCalculation.Deduction
        /// </summary>
        virtual public System.Decimal? Deduction
        {
            get
            {
                return base.GetSystemDecimal(EmployeeTaxCalculationMetadata.ColumnNames.Deduction);
            }

            set
            {
                base.SetSystemDecimal(EmployeeTaxCalculationMetadata.ColumnNames.Deduction, value);
            }
        }
        /// <summary>
        /// Maps to EmployeeTaxCalculation.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(EmployeeTaxCalculationMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(EmployeeTaxCalculationMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to EmployeeTaxCalculation.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(EmployeeTaxCalculationMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(EmployeeTaxCalculationMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esEmployeeTaxCalculation entity)
            {
                this.entity = entity;
            }
            public System.String PersonID
            {
                get
                {
                    System.Int32? data = entity.PersonID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PersonID = null;
                    else entity.PersonID = Convert.ToInt32(value);
                }
            }
            public System.String WageProcessTypeID
            {
                get
                {
                    System.Int32? data = entity.WageProcessTypeID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.WageProcessTypeID = null;
                    else entity.WageProcessTypeID = Convert.ToInt32(value);
                }
            }
            public System.String SPTYear
            {
                get
                {
                    System.Int32? data = entity.SPTYear;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SPTYear = null;
                    else entity.SPTYear = Convert.ToInt32(value);
                }
            }
            public System.String SPTMonth
            {
                get
                {
                    System.Int32? data = entity.SPTMonth;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SPTMonth = null;
                    else entity.SPTMonth = Convert.ToInt32(value);
                }
            }
            public System.String GrossIncome
            {
                get
                {
                    System.Decimal? data = entity.GrossIncome;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.GrossIncome = null;
                    else entity.GrossIncome = Convert.ToDecimal(value);
                }
            }
            public System.String TaxRate
            {
                get
                {
                    System.Decimal? data = entity.TaxRate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TaxRate = null;
                    else entity.TaxRate = Convert.ToDecimal(value);
                }
            }
            public System.String TaxAmount
            {
                get
                {
                    System.Decimal? data = entity.TaxAmount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TaxAmount = null;
                    else entity.TaxAmount = Convert.ToDecimal(value);
                }
            }
            public System.String Deduction
            {
                get
                {
                    System.Decimal? data = entity.Deduction;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Deduction = null;
                    else entity.Deduction = Convert.ToDecimal(value);
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
            private esEmployeeTaxCalculation entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esEmployeeTaxCalculationQuery query)
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
                throw new Exception("esEmployeeTaxCalculation can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class EmployeeTaxCalculation : esEmployeeTaxCalculation
    {
    }

    [Serializable]
    abstract public class esEmployeeTaxCalculationQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return EmployeeTaxCalculationMetadata.Meta();
            }
        }

        public esQueryItem PersonID
        {
            get
            {
                return new esQueryItem(this, EmployeeTaxCalculationMetadata.ColumnNames.PersonID, esSystemType.Int32);
            }
        }

        public esQueryItem WageProcessTypeID
        {
            get
            {
                return new esQueryItem(this, EmployeeTaxCalculationMetadata.ColumnNames.WageProcessTypeID, esSystemType.Int32);
            }
        }

        public esQueryItem SPTYear
        {
            get
            {
                return new esQueryItem(this, EmployeeTaxCalculationMetadata.ColumnNames.SPTYear, esSystemType.Int32);
            }
        }

        public esQueryItem SPTMonth
        {
            get
            {
                return new esQueryItem(this, EmployeeTaxCalculationMetadata.ColumnNames.SPTMonth, esSystemType.Int32);
            }
        }

        public esQueryItem GrossIncome
        {
            get
            {
                return new esQueryItem(this, EmployeeTaxCalculationMetadata.ColumnNames.GrossIncome, esSystemType.Decimal);
            }
        }

        public esQueryItem TaxRate
        {
            get
            {
                return new esQueryItem(this, EmployeeTaxCalculationMetadata.ColumnNames.TaxRate, esSystemType.Decimal);
            }
        }

        public esQueryItem TaxAmount
        {
            get
            {
                return new esQueryItem(this, EmployeeTaxCalculationMetadata.ColumnNames.TaxAmount, esSystemType.Decimal);
            }
        }

        public esQueryItem Deduction
        {
            get
            {
                return new esQueryItem(this, EmployeeTaxCalculationMetadata.ColumnNames.Deduction, esSystemType.Decimal);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, EmployeeTaxCalculationMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, EmployeeTaxCalculationMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("EmployeeTaxCalculationCollection")]
    public partial class EmployeeTaxCalculationCollection : esEmployeeTaxCalculationCollection, IEnumerable<EmployeeTaxCalculation>
    {
        public EmployeeTaxCalculationCollection()
        {

        }

        public static implicit operator List<EmployeeTaxCalculation>(EmployeeTaxCalculationCollection coll)
        {
            List<EmployeeTaxCalculation> list = new List<EmployeeTaxCalculation>();

            foreach (EmployeeTaxCalculation emp in coll)
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
                return EmployeeTaxCalculationMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new EmployeeTaxCalculationQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new EmployeeTaxCalculation(row);
        }

        override protected esEntity CreateEntity()
        {
            return new EmployeeTaxCalculation();
        }

        #endregion

        [BrowsableAttribute(false)]
        public EmployeeTaxCalculationQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new EmployeeTaxCalculationQuery();
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
        public bool Load(EmployeeTaxCalculationQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public EmployeeTaxCalculation AddNew()
        {
            EmployeeTaxCalculation entity = base.AddNewEntity() as EmployeeTaxCalculation;

            return entity;
        }
        public EmployeeTaxCalculation FindByPrimaryKey(Int32 personID, Int32 wageProcessTypeID, Int32 sPTYear, Int32 sPTMonth)
        {
            return base.FindByPrimaryKey(personID, wageProcessTypeID, sPTYear, sPTMonth) as EmployeeTaxCalculation;
        }

        #region IEnumerable< EmployeeTaxCalculation> Members

        IEnumerator<EmployeeTaxCalculation> IEnumerable<EmployeeTaxCalculation>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as EmployeeTaxCalculation;
            }
        }

        #endregion

        private EmployeeTaxCalculationQuery query;
    }


    /// <summary>
    /// Encapsulates the 'EmployeeTaxCalculation' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("EmployeeTaxCalculation ({PersonID, WageProcessTypeID, SPTYear, SPTMonth})")]
    [Serializable]
    public partial class EmployeeTaxCalculation : esEmployeeTaxCalculation
    {
        public EmployeeTaxCalculation()
        {
        }

        public EmployeeTaxCalculation(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return EmployeeTaxCalculationMetadata.Meta();
            }
        }

        override protected esEmployeeTaxCalculationQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new EmployeeTaxCalculationQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public EmployeeTaxCalculationQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new EmployeeTaxCalculationQuery();
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
        public bool Load(EmployeeTaxCalculationQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private EmployeeTaxCalculationQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class EmployeeTaxCalculationQuery : esEmployeeTaxCalculationQuery
    {
        public EmployeeTaxCalculationQuery()
        {

        }

        public EmployeeTaxCalculationQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "EmployeeTaxCalculationQuery";
        }
    }

    [Serializable]
    public partial class EmployeeTaxCalculationMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected EmployeeTaxCalculationMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(EmployeeTaxCalculationMetadata.ColumnNames.PersonID, 0, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = EmployeeTaxCalculationMetadata.PropertyNames.PersonID;
            c.IsInPrimaryKey = true;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(EmployeeTaxCalculationMetadata.ColumnNames.WageProcessTypeID, 1, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = EmployeeTaxCalculationMetadata.PropertyNames.WageProcessTypeID;
            c.IsInPrimaryKey = true;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(EmployeeTaxCalculationMetadata.ColumnNames.SPTYear, 2, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = EmployeeTaxCalculationMetadata.PropertyNames.SPTYear;
            c.IsInPrimaryKey = true;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(EmployeeTaxCalculationMetadata.ColumnNames.SPTMonth, 3, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = EmployeeTaxCalculationMetadata.PropertyNames.SPTMonth;
            c.IsInPrimaryKey = true;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(EmployeeTaxCalculationMetadata.ColumnNames.GrossIncome, 4, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = EmployeeTaxCalculationMetadata.PropertyNames.GrossIncome;
            c.NumericPrecision = 18;
            c.NumericScale = 4;
            _columns.Add(c);

            c = new esColumnMetadata(EmployeeTaxCalculationMetadata.ColumnNames.TaxRate, 5, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = EmployeeTaxCalculationMetadata.PropertyNames.TaxRate;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(EmployeeTaxCalculationMetadata.ColumnNames.TaxAmount, 6, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = EmployeeTaxCalculationMetadata.PropertyNames.TaxAmount;
            c.NumericPrecision = 18;
            c.NumericScale = 4;
            _columns.Add(c);

            c = new esColumnMetadata(EmployeeTaxCalculationMetadata.ColumnNames.Deduction, 7, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = EmployeeTaxCalculationMetadata.PropertyNames.Deduction;
            c.NumericPrecision = 18;
            c.NumericScale = 4;
            _columns.Add(c);

            c = new esColumnMetadata(EmployeeTaxCalculationMetadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = EmployeeTaxCalculationMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(EmployeeTaxCalculationMetadata.ColumnNames.LastUpdateByUserID, 9, typeof(System.String), esSystemType.String);
            c.PropertyName = EmployeeTaxCalculationMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public EmployeeTaxCalculationMetadata Meta()
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
            public const string PersonID = "PersonID";
            public const string WageProcessTypeID = "WageProcessTypeID";
            public const string SPTYear = "SPTYear";
            public const string SPTMonth = "SPTMonth";
            public const string GrossIncome = "GrossIncome";
            public const string TaxRate = "TaxRate";
            public const string TaxAmount = "TaxAmount";
            public const string Deduction = "Deduction";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string PersonID = "PersonID";
            public const string WageProcessTypeID = "WageProcessTypeID";
            public const string SPTYear = "SPTYear";
            public const string SPTMonth = "SPTMonth";
            public const string GrossIncome = "GrossIncome";
            public const string TaxRate = "TaxRate";
            public const string TaxAmount = "TaxAmount";
            public const string Deduction = "Deduction";
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
            lock (typeof(EmployeeTaxCalculationMetadata))
            {
                if (EmployeeTaxCalculationMetadata.mapDelegates == null)
                {
                    EmployeeTaxCalculationMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (EmployeeTaxCalculationMetadata.meta == null)
                {
                    EmployeeTaxCalculationMetadata.meta = new EmployeeTaxCalculationMetadata();
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

                meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("WageProcessTypeID", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("SPTYear", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("SPTMonth", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("GrossIncome", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("TaxRate", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("TaxAmount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("Deduction", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "EmployeeTaxCalculation";
                meta.Destination = "EmployeeTaxCalculation";
                meta.spInsert = "proc_EmployeeTaxCalculationInsert";
                meta.spUpdate = "proc_EmployeeTaxCalculationUpdate";
                meta.spDelete = "proc_EmployeeTaxCalculationDelete";
                meta.spLoadAll = "proc_EmployeeTaxCalculationLoadAll";
                meta.spLoadByPrimaryKey = "proc_EmployeeTaxCalculationLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private EmployeeTaxCalculationMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
