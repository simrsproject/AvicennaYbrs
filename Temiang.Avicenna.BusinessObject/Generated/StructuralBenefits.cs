/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 9/28/2017 3:02:36 PM
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
    abstract public class esStructuralBenefitsCollection : esEntityCollectionWAuditLog
    {
        public esStructuralBenefitsCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "StructuralBenefitsCollection";
        }

        #region Query Logic
        protected void InitQuery(esStructuralBenefitsQuery query)
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
            this.InitQuery(query as esStructuralBenefitsQuery);
        }
        #endregion

        virtual public StructuralBenefits DetachEntity(StructuralBenefits entity)
        {
            return base.DetachEntity(entity) as StructuralBenefits;
        }

        virtual public StructuralBenefits AttachEntity(StructuralBenefits entity)
        {
            return base.AttachEntity(entity) as StructuralBenefits;
        }

        virtual public void Combine(StructuralBenefitsCollection collection)
        {
            base.Combine(collection);
        }

        new public StructuralBenefits this[int index]
        {
            get
            {
                return base[index] as StructuralBenefits;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(StructuralBenefits);
        }
    }

    [Serializable]
    abstract public class esStructuralBenefits : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esStructuralBenefitsQuery GetDynamicQuery()
        {
            return null;
        }

        public esStructuralBenefits()
        {
        }

        public esStructuralBenefits(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(Int32 organizationUnitID, Int32 positionID, DateTime validFrom)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(organizationUnitID, positionID, validFrom);
            else
                return LoadByPrimaryKeyStoredProcedure(organizationUnitID, positionID, validFrom);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 organizationUnitID, Int32 positionID, DateTime validFrom)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(organizationUnitID, positionID, validFrom);
            else
                return LoadByPrimaryKeyStoredProcedure(organizationUnitID, positionID, validFrom);
        }

        private bool LoadByPrimaryKeyDynamic(Int32 organizationUnitID, Int32 positionID, DateTime validFrom)
        {
            esStructuralBenefitsQuery query = this.GetDynamicQuery();
            query.Where(query.OrganizationUnitID == organizationUnitID, query.PositionID == positionID, query.ValidFrom == validFrom);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(Int32 organizationUnitID, Int32 positionID, DateTime validFrom)
        {
            esParameters parms = new esParameters();
            parms.Add("OrganizationUnitID", organizationUnitID);
            parms.Add("PositionID", positionID);
            parms.Add("ValidFrom", validFrom);
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
                        case "OrganizationUnitID": this.str.OrganizationUnitID = (string)value; break;
                        case "PositionID": this.str.PositionID = (string)value; break;
                        case "ValidFrom": this.str.ValidFrom = (string)value; break;
                        case "Amount": this.str.Amount = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "OrganizationUnitID":

                            if (value == null || value is System.Int32)
                                this.OrganizationUnitID = (System.Int32?)value;
                            break;
                        case "PositionID":

                            if (value == null || value is System.Int32)
                                this.PositionID = (System.Int32?)value;
                            break;
                        case "ValidFrom":

                            if (value == null || value is System.DateTime)
                                this.ValidFrom = (System.DateTime?)value;
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
        /// Maps to StructuralBenefits.OrganizationUnitID
        /// </summary>
        virtual public System.Int32? OrganizationUnitID
        {
            get
            {
                return base.GetSystemInt32(StructuralBenefitsMetadata.ColumnNames.OrganizationUnitID);
            }

            set
            {
                base.SetSystemInt32(StructuralBenefitsMetadata.ColumnNames.OrganizationUnitID, value);
            }
        }
        /// <summary>
        /// Maps to StructuralBenefits.PositionID
        /// </summary>
        virtual public System.Int32? PositionID
        {
            get
            {
                return base.GetSystemInt32(StructuralBenefitsMetadata.ColumnNames.PositionID);
            }

            set
            {
                base.SetSystemInt32(StructuralBenefitsMetadata.ColumnNames.PositionID, value);
            }
        }
        /// <summary>
        /// Maps to StructuralBenefits.ValidFrom
        /// </summary>
        virtual public System.DateTime? ValidFrom
        {
            get
            {
                return base.GetSystemDateTime(StructuralBenefitsMetadata.ColumnNames.ValidFrom);
            }

            set
            {
                base.SetSystemDateTime(StructuralBenefitsMetadata.ColumnNames.ValidFrom, value);
            }
        }
        /// <summary>
        /// Maps to StructuralBenefits.Amount
        /// </summary>
        virtual public System.Decimal? Amount
        {
            get
            {
                return base.GetSystemDecimal(StructuralBenefitsMetadata.ColumnNames.Amount);
            }

            set
            {
                base.SetSystemDecimal(StructuralBenefitsMetadata.ColumnNames.Amount, value);
            }
        }
        /// <summary>
        /// Maps to StructuralBenefits.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(StructuralBenefitsMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(StructuralBenefitsMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to StructuralBenefits.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(StructuralBenefitsMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(StructuralBenefitsMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esStructuralBenefits entity)
            {
                this.entity = entity;
            }
            public System.String OrganizationUnitID
            {
                get
                {
                    System.Int32? data = entity.OrganizationUnitID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.OrganizationUnitID = null;
                    else entity.OrganizationUnitID = Convert.ToInt32(value);
                }
            }
            public System.String PositionID
            {
                get
                {
                    System.Int32? data = entity.PositionID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PositionID = null;
                    else entity.PositionID = Convert.ToInt32(value);
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
            private esStructuralBenefits entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esStructuralBenefitsQuery query)
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
                throw new Exception("esStructuralBenefits can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class StructuralBenefits : esStructuralBenefits
    {
    }

    [Serializable]
    abstract public class esStructuralBenefitsQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return StructuralBenefitsMetadata.Meta();
            }
        }

        public esQueryItem OrganizationUnitID
        {
            get
            {
                return new esQueryItem(this, StructuralBenefitsMetadata.ColumnNames.OrganizationUnitID, esSystemType.Int32);
            }
        }

        public esQueryItem PositionID
        {
            get
            {
                return new esQueryItem(this, StructuralBenefitsMetadata.ColumnNames.PositionID, esSystemType.Int32);
            }
        }

        public esQueryItem ValidFrom
        {
            get
            {
                return new esQueryItem(this, StructuralBenefitsMetadata.ColumnNames.ValidFrom, esSystemType.DateTime);
            }
        }

        public esQueryItem Amount
        {
            get
            {
                return new esQueryItem(this, StructuralBenefitsMetadata.ColumnNames.Amount, esSystemType.Decimal);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, StructuralBenefitsMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, StructuralBenefitsMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("StructuralBenefitsCollection")]
    public partial class StructuralBenefitsCollection : esStructuralBenefitsCollection, IEnumerable<StructuralBenefits>
    {
        public StructuralBenefitsCollection()
        {

        }

        public static implicit operator List<StructuralBenefits>(StructuralBenefitsCollection coll)
        {
            List<StructuralBenefits> list = new List<StructuralBenefits>();

            foreach (StructuralBenefits emp in coll)
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
                return StructuralBenefitsMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new StructuralBenefitsQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new StructuralBenefits(row);
        }

        override protected esEntity CreateEntity()
        {
            return new StructuralBenefits();
        }

        #endregion

        [BrowsableAttribute(false)]
        public StructuralBenefitsQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new StructuralBenefitsQuery();
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
        public bool Load(StructuralBenefitsQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public StructuralBenefits AddNew()
        {
            StructuralBenefits entity = base.AddNewEntity() as StructuralBenefits;

            return entity;
        }
        public StructuralBenefits FindByPrimaryKey(Int32 organizationUnitID, Int32 positionID, DateTime validFrom)
        {
            return base.FindByPrimaryKey(organizationUnitID, positionID, validFrom) as StructuralBenefits;
        }

        #region IEnumerable< StructuralBenefits> Members

        IEnumerator<StructuralBenefits> IEnumerable<StructuralBenefits>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as StructuralBenefits;
            }
        }

        #endregion

        private StructuralBenefitsQuery query;
    }


    /// <summary>
    /// Encapsulates the 'StructuralBenefits' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("StructuralBenefits ({OrganizationUnitID, PositionID, ValidFrom})")]
    [Serializable]
    public partial class StructuralBenefits : esStructuralBenefits
    {
        public StructuralBenefits()
        {
        }

        public StructuralBenefits(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return StructuralBenefitsMetadata.Meta();
            }
        }

        override protected esStructuralBenefitsQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new StructuralBenefitsQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public StructuralBenefitsQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new StructuralBenefitsQuery();
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
        public bool Load(StructuralBenefitsQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private StructuralBenefitsQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class StructuralBenefitsQuery : esStructuralBenefitsQuery
    {
        public StructuralBenefitsQuery()
        {

        }

        public StructuralBenefitsQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "StructuralBenefitsQuery";
        }
    }

    [Serializable]
    public partial class StructuralBenefitsMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected StructuralBenefitsMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(StructuralBenefitsMetadata.ColumnNames.OrganizationUnitID, 0, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = StructuralBenefitsMetadata.PropertyNames.OrganizationUnitID;
            c.IsInPrimaryKey = true;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(StructuralBenefitsMetadata.ColumnNames.PositionID, 1, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = StructuralBenefitsMetadata.PropertyNames.PositionID;
            c.IsInPrimaryKey = true;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(StructuralBenefitsMetadata.ColumnNames.ValidFrom, 2, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = StructuralBenefitsMetadata.PropertyNames.ValidFrom;
            c.IsInPrimaryKey = true;
            _columns.Add(c);

            c = new esColumnMetadata(StructuralBenefitsMetadata.ColumnNames.Amount, 3, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = StructuralBenefitsMetadata.PropertyNames.Amount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(StructuralBenefitsMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = StructuralBenefitsMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(StructuralBenefitsMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = StructuralBenefitsMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public StructuralBenefitsMetadata Meta()
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
            public const string OrganizationUnitID = "OrganizationUnitID";
            public const string PositionID = "PositionID";
            public const string ValidFrom = "ValidFrom";
            public const string Amount = "Amount";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string OrganizationUnitID = "OrganizationUnitID";
            public const string PositionID = "PositionID";
            public const string ValidFrom = "ValidFrom";
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
            lock (typeof(StructuralBenefitsMetadata))
            {
                if (StructuralBenefitsMetadata.mapDelegates == null)
                {
                    StructuralBenefitsMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (StructuralBenefitsMetadata.meta == null)
                {
                    StructuralBenefitsMetadata.meta = new StructuralBenefitsMetadata();
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

                meta.AddTypeMap("OrganizationUnitID", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("PositionID", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("ValidFrom", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("Amount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "StructuralBenefits";
                meta.Destination = "StructuralBenefits";
                meta.spInsert = "proc_StructuralBenefitsInsert";
                meta.spUpdate = "proc_StructuralBenefitsUpdate";
                meta.spDelete = "proc_StructuralBenefitsDelete";
                meta.spLoadAll = "proc_StructuralBenefitsLoadAll";
                meta.spLoadByPrimaryKey = "proc_StructuralBenefitsLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private StructuralBenefitsMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
