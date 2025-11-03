/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 10/17/2018 12:40:50 PM
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
    abstract public class esOvertimeCollection : esEntityCollectionWAuditLog
    {
        public esOvertimeCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "OvertimeCollection";
        }

        #region Query Logic
        protected void InitQuery(esOvertimeQuery query)
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
            this.InitQuery(query as esOvertimeQuery);
        }
        #endregion

        virtual public Overtime DetachEntity(Overtime entity)
        {
            return base.DetachEntity(entity) as Overtime;
        }

        virtual public Overtime AttachEntity(Overtime entity)
        {
            return base.AttachEntity(entity) as Overtime;
        }

        virtual public void Combine(OvertimeCollection collection)
        {
            base.Combine(collection);
        }

        new public Overtime this[int index]
        {
            get
            {
                return base[index] as Overtime;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(Overtime);
        }
    }

    [Serializable]
    abstract public class esOvertime : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esOvertimeQuery GetDynamicQuery()
        {
            return null;
        }

        public esOvertime()
        {
        }

        public esOvertime(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(Int32 overtimeID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(overtimeID);
            else
                return LoadByPrimaryKeyStoredProcedure(overtimeID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 overtimeID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(overtimeID);
            else
                return LoadByPrimaryKeyStoredProcedure(overtimeID);
        }

        private bool LoadByPrimaryKeyDynamic(Int32 overtimeID)
        {
            esOvertimeQuery query = this.GetDynamicQuery();
            query.Where(query.OvertimeID == overtimeID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(Int32 overtimeID)
        {
            esParameters parms = new esParameters();
            parms.Add("OvertimeID", overtimeID);
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
                        case "OvertimeID": this.str.OvertimeID = (string)value; break;
                        case "OvertimeName": this.str.OvertimeName = (string)value; break;
                        case "SalaryComponentID": this.str.SalaryComponentID = (string)value; break;
                        case "ValidFrom": this.str.ValidFrom = (string)value; break;
                        case "ValidTo": this.str.ValidTo = (string)value; break;
                        case "Notes": this.str.Notes = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "OvertimeID":

                            if (value == null || value is System.Int32)
                                this.OvertimeID = (System.Int32?)value;
                            break;
                        case "SalaryComponentID":

                            if (value == null || value is System.Int32)
                                this.SalaryComponentID = (System.Int32?)value;
                            break;
                        case "ValidFrom":

                            if (value == null || value is System.DateTime)
                                this.ValidFrom = (System.DateTime?)value;
                            break;
                        case "ValidTo":

                            if (value == null || value is System.DateTime)
                                this.ValidTo = (System.DateTime?)value;
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
        /// Maps to Overtime.OvertimeID
        /// </summary>
        virtual public System.Int32? OvertimeID
        {
            get
            {
                return base.GetSystemInt32(OvertimeMetadata.ColumnNames.OvertimeID);
            }

            set
            {
                base.SetSystemInt32(OvertimeMetadata.ColumnNames.OvertimeID, value);
            }
        }
        /// <summary>
        /// Maps to Overtime.OvertimeName
        /// </summary>
        virtual public System.String OvertimeName
        {
            get
            {
                return base.GetSystemString(OvertimeMetadata.ColumnNames.OvertimeName);
            }

            set
            {
                base.SetSystemString(OvertimeMetadata.ColumnNames.OvertimeName, value);
            }
        }
        /// <summary>
        /// Maps to Overtime.SalaryComponentID
        /// </summary>
        virtual public System.Int32? SalaryComponentID
        {
            get
            {
                return base.GetSystemInt32(OvertimeMetadata.ColumnNames.SalaryComponentID);
            }

            set
            {
                base.SetSystemInt32(OvertimeMetadata.ColumnNames.SalaryComponentID, value);
            }
        }
        /// <summary>
        /// Maps to Overtime.ValidFrom
        /// </summary>
        virtual public System.DateTime? ValidFrom
        {
            get
            {
                return base.GetSystemDateTime(OvertimeMetadata.ColumnNames.ValidFrom);
            }

            set
            {
                base.SetSystemDateTime(OvertimeMetadata.ColumnNames.ValidFrom, value);
            }
        }
        /// <summary>
        /// Maps to Overtime.ValidTo
        /// </summary>
        virtual public System.DateTime? ValidTo
        {
            get
            {
                return base.GetSystemDateTime(OvertimeMetadata.ColumnNames.ValidTo);
            }

            set
            {
                base.SetSystemDateTime(OvertimeMetadata.ColumnNames.ValidTo, value);
            }
        }
        /// <summary>
        /// Maps to Overtime.Notes
        /// </summary>
        virtual public System.String Notes
        {
            get
            {
                return base.GetSystemString(OvertimeMetadata.ColumnNames.Notes);
            }

            set
            {
                base.SetSystemString(OvertimeMetadata.ColumnNames.Notes, value);
            }
        }
        /// <summary>
        /// Maps to Overtime.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(OvertimeMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(OvertimeMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to Overtime.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(OvertimeMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(OvertimeMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esOvertime entity)
            {
                this.entity = entity;
            }
            public System.String OvertimeID
            {
                get
                {
                    System.Int32? data = entity.OvertimeID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.OvertimeID = null;
                    else entity.OvertimeID = Convert.ToInt32(value);
                }
            }
            public System.String OvertimeName
            {
                get
                {
                    System.String data = entity.OvertimeName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.OvertimeName = null;
                    else entity.OvertimeName = Convert.ToString(value);
                }
            }
            public System.String SalaryComponentID
            {
                get
                {
                    System.Int32? data = entity.SalaryComponentID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SalaryComponentID = null;
                    else entity.SalaryComponentID = Convert.ToInt32(value);
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
            private esOvertime entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esOvertimeQuery query)
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
                throw new Exception("esOvertime can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class Overtime : esOvertime
    {
    }

    [Serializable]
    abstract public class esOvertimeQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return OvertimeMetadata.Meta();
            }
        }

        public esQueryItem OvertimeID
        {
            get
            {
                return new esQueryItem(this, OvertimeMetadata.ColumnNames.OvertimeID, esSystemType.Int32);
            }
        }

        public esQueryItem OvertimeName
        {
            get
            {
                return new esQueryItem(this, OvertimeMetadata.ColumnNames.OvertimeName, esSystemType.String);
            }
        }

        public esQueryItem SalaryComponentID
        {
            get
            {
                return new esQueryItem(this, OvertimeMetadata.ColumnNames.SalaryComponentID, esSystemType.Int32);
            }
        }

        public esQueryItem ValidFrom
        {
            get
            {
                return new esQueryItem(this, OvertimeMetadata.ColumnNames.ValidFrom, esSystemType.DateTime);
            }
        }

        public esQueryItem ValidTo
        {
            get
            {
                return new esQueryItem(this, OvertimeMetadata.ColumnNames.ValidTo, esSystemType.DateTime);
            }
        }

        public esQueryItem Notes
        {
            get
            {
                return new esQueryItem(this, OvertimeMetadata.ColumnNames.Notes, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, OvertimeMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, OvertimeMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("OvertimeCollection")]
    public partial class OvertimeCollection : esOvertimeCollection, IEnumerable<Overtime>
    {
        public OvertimeCollection()
        {

        }

        public static implicit operator List<Overtime>(OvertimeCollection coll)
        {
            List<Overtime> list = new List<Overtime>();

            foreach (Overtime emp in coll)
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
                return OvertimeMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new OvertimeQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new Overtime(row);
        }

        override protected esEntity CreateEntity()
        {
            return new Overtime();
        }

        #endregion

        [BrowsableAttribute(false)]
        public OvertimeQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new OvertimeQuery();
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
        public bool Load(OvertimeQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public Overtime AddNew()
        {
            Overtime entity = base.AddNewEntity() as Overtime;

            return entity;
        }
        public Overtime FindByPrimaryKey(Int32 overtimeID)
        {
            return base.FindByPrimaryKey(overtimeID) as Overtime;
        }

        #region IEnumerable< Overtime> Members

        IEnumerator<Overtime> IEnumerable<Overtime>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as Overtime;
            }
        }

        #endregion

        private OvertimeQuery query;
    }


    /// <summary>
    /// Encapsulates the 'Overtime' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("Overtime ({OvertimeID})")]
    [Serializable]
    public partial class Overtime : esOvertime
    {
        public Overtime()
        {
        }

        public Overtime(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return OvertimeMetadata.Meta();
            }
        }

        override protected esOvertimeQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new OvertimeQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public OvertimeQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new OvertimeQuery();
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
        public bool Load(OvertimeQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private OvertimeQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class OvertimeQuery : esOvertimeQuery
    {
        public OvertimeQuery()
        {

        }

        public OvertimeQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "OvertimeQuery";
        }
    }

    [Serializable]
    public partial class OvertimeMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected OvertimeMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(OvertimeMetadata.ColumnNames.OvertimeID, 0, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = OvertimeMetadata.PropertyNames.OvertimeID;
            c.IsInPrimaryKey = true;
            c.IsAutoIncrement = true;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(OvertimeMetadata.ColumnNames.OvertimeName, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = OvertimeMetadata.PropertyNames.OvertimeName;
            c.CharacterMaxLength = 255;
            _columns.Add(c);

            c = new esColumnMetadata(OvertimeMetadata.ColumnNames.SalaryComponentID, 2, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = OvertimeMetadata.PropertyNames.SalaryComponentID;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(OvertimeMetadata.ColumnNames.ValidFrom, 3, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = OvertimeMetadata.PropertyNames.ValidFrom;
            _columns.Add(c);

            c = new esColumnMetadata(OvertimeMetadata.ColumnNames.ValidTo, 4, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = OvertimeMetadata.PropertyNames.ValidTo;
            _columns.Add(c);

            c = new esColumnMetadata(OvertimeMetadata.ColumnNames.Notes, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = OvertimeMetadata.PropertyNames.Notes;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(OvertimeMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = OvertimeMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(OvertimeMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = OvertimeMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 40;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public OvertimeMetadata Meta()
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
            public const string OvertimeID = "OvertimeID";
            public const string OvertimeName = "OvertimeName";
            public const string SalaryComponentID = "SalaryComponentID";
            public const string ValidFrom = "ValidFrom";
            public const string ValidTo = "ValidTo";
            public const string Notes = "Notes";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string OvertimeID = "OvertimeID";
            public const string OvertimeName = "OvertimeName";
            public const string SalaryComponentID = "SalaryComponentID";
            public const string ValidFrom = "ValidFrom";
            public const string ValidTo = "ValidTo";
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
            lock (typeof(OvertimeMetadata))
            {
                if (OvertimeMetadata.mapDelegates == null)
                {
                    OvertimeMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (OvertimeMetadata.meta == null)
                {
                    OvertimeMetadata.meta = new OvertimeMetadata();
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

                meta.AddTypeMap("OvertimeID", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("OvertimeName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SalaryComponentID", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("ValidFrom", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("ValidTo", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("Notes", new esTypeMap("nchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "Overtime";
                meta.Destination = "Overtime";
                meta.spInsert = "proc_OvertimeInsert";
                meta.spUpdate = "proc_OvertimeUpdate";
                meta.spDelete = "proc_OvertimeDelete";
                meta.spLoadAll = "proc_OvertimeLoadAll";
                meta.spLoadByPrimaryKey = "proc_OvertimeLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private OvertimeMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
