/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 10/8/2017 2:19:55 PM
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
    abstract public class esPpiInfectionCollection : esEntityCollectionWAuditLog
    {
        public esPpiInfectionCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "PpiInfectionCollection";
        }

        #region Query Logic
        protected void InitQuery(esPpiInfectionQuery query)
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
            this.InitQuery(query as esPpiInfectionQuery);
        }
        #endregion

        virtual public PpiInfection DetachEntity(PpiInfection entity)
        {
            return base.DetachEntity(entity) as PpiInfection;
        }

        virtual public PpiInfection AttachEntity(PpiInfection entity)
        {
            return base.AttachEntity(entity) as PpiInfection;
        }

        virtual public void Combine(PpiInfectionCollection collection)
        {
            base.Combine(collection);
        }

        new public PpiInfection this[int index]
        {
            get
            {
                return base[index] as PpiInfection;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(PpiInfection);
        }
    }

    [Serializable]
    abstract public class esPpiInfection : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esPpiInfectionQuery GetDynamicQuery()
        {
            return null;
        }

        public esPpiInfection()
        {
        }

        public esPpiInfection(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String registrationNo, String sRInfectionType)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(registrationNo, sRInfectionType);
            else
                return LoadByPrimaryKeyStoredProcedure(registrationNo, sRInfectionType);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String registrationNo, String sRInfectionType)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(registrationNo, sRInfectionType);
            else
                return LoadByPrimaryKeyStoredProcedure(registrationNo, sRInfectionType);
        }

        private bool LoadByPrimaryKeyDynamic(String registrationNo, String sRInfectionType)
        {
            esPpiInfectionQuery query = this.GetDynamicQuery();
            query.Where(query.RegistrationNo == registrationNo, query.SRInfectionType == sRInfectionType);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String registrationNo, String sRInfectionType)
        {
            esParameters parms = new esParameters();
            parms.Add("RegistrationNo", registrationNo);
            parms.Add("SRInfectionType", sRInfectionType);
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
                        case "SRInfectionType": this.str.SRInfectionType = (string)value; break;
                        case "DaysTo": this.str.DaysTo = (string)value; break;
                        case "Cultures": this.str.Cultures = (string)value; break;
                        case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;
                        case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "DaysTo":

                            if (value == null || value is System.Int16)
                                this.DaysTo = (System.Int16?)value;
                            break;
                        case "CreatedDateTime":

                            if (value == null || value is System.DateTime)
                                this.CreatedDateTime = (System.DateTime?)value;
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
        /// Maps to PpiInfection.RegistrationNo
        /// </summary>
        virtual public System.String RegistrationNo
        {
            get
            {
                return base.GetSystemString(PpiInfectionMetadata.ColumnNames.RegistrationNo);
            }

            set
            {
                base.SetSystemString(PpiInfectionMetadata.ColumnNames.RegistrationNo, value);
            }
        }
        /// <summary>
        /// Maps to PpiInfection.SRInfectionType
        /// </summary>
        virtual public System.String SRInfectionType
        {
            get
            {
                return base.GetSystemString(PpiInfectionMetadata.ColumnNames.SRInfectionType);
            }

            set
            {
                base.SetSystemString(PpiInfectionMetadata.ColumnNames.SRInfectionType, value);
            }
        }
        /// <summary>
        /// Maps to PpiInfection.DaysTo
        /// </summary>
        virtual public System.Int16? DaysTo
        {
            get
            {
                return base.GetSystemInt16(PpiInfectionMetadata.ColumnNames.DaysTo);
            }

            set
            {
                base.SetSystemInt16(PpiInfectionMetadata.ColumnNames.DaysTo, value);
            }
        }
        /// <summary>
        /// Maps to PpiInfection.Cultures
        /// </summary>
        virtual public System.String Cultures
        {
            get
            {
                return base.GetSystemString(PpiInfectionMetadata.ColumnNames.Cultures);
            }

            set
            {
                base.SetSystemString(PpiInfectionMetadata.ColumnNames.Cultures, value);
            }
        }
        /// <summary>
        /// Maps to PpiInfection.CreatedDateTime
        /// </summary>
        virtual public System.DateTime? CreatedDateTime
        {
            get
            {
                return base.GetSystemDateTime(PpiInfectionMetadata.ColumnNames.CreatedDateTime);
            }

            set
            {
                base.SetSystemDateTime(PpiInfectionMetadata.ColumnNames.CreatedDateTime, value);
            }
        }
        /// <summary>
        /// Maps to PpiInfection.CreatedByUserID
        /// </summary>
        virtual public System.String CreatedByUserID
        {
            get
            {
                return base.GetSystemString(PpiInfectionMetadata.ColumnNames.CreatedByUserID);
            }

            set
            {
                base.SetSystemString(PpiInfectionMetadata.ColumnNames.CreatedByUserID, value);
            }
        }
        /// <summary>
        /// Maps to PpiInfection.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(PpiInfectionMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(PpiInfectionMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to PpiInfection.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(PpiInfectionMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(PpiInfectionMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esPpiInfection entity)
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
            public System.String SRInfectionType
            {
                get
                {
                    System.String data = entity.SRInfectionType;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRInfectionType = null;
                    else entity.SRInfectionType = Convert.ToString(value);
                }
            }
            public System.String DaysTo
            {
                get
                {
                    System.Int16? data = entity.DaysTo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DaysTo = null;
                    else entity.DaysTo = Convert.ToInt16(value);
                }
            }
            public System.String Cultures
            {
                get
                {
                    System.String data = entity.Cultures;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Cultures = null;
                    else entity.Cultures = Convert.ToString(value);
                }
            }
            public System.String CreatedDateTime
            {
                get
                {
                    System.DateTime? data = entity.CreatedDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CreatedDateTime = null;
                    else entity.CreatedDateTime = Convert.ToDateTime(value);
                }
            }
            public System.String CreatedByUserID
            {
                get
                {
                    System.String data = entity.CreatedByUserID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CreatedByUserID = null;
                    else entity.CreatedByUserID = Convert.ToString(value);
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
            private esPpiInfection entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esPpiInfectionQuery query)
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
                throw new Exception("esPpiInfection can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class PpiInfection : esPpiInfection
    {
    }

    [Serializable]
    abstract public class esPpiInfectionQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return PpiInfectionMetadata.Meta();
            }
        }

        public esQueryItem RegistrationNo
        {
            get
            {
                return new esQueryItem(this, PpiInfectionMetadata.ColumnNames.RegistrationNo, esSystemType.String);
            }
        }

        public esQueryItem SRInfectionType
        {
            get
            {
                return new esQueryItem(this, PpiInfectionMetadata.ColumnNames.SRInfectionType, esSystemType.String);
            }
        }

        public esQueryItem DaysTo
        {
            get
            {
                return new esQueryItem(this, PpiInfectionMetadata.ColumnNames.DaysTo, esSystemType.Int16);
            }
        }

        public esQueryItem Cultures
        {
            get
            {
                return new esQueryItem(this, PpiInfectionMetadata.ColumnNames.Cultures, esSystemType.String);
            }
        }

        public esQueryItem CreatedDateTime
        {
            get
            {
                return new esQueryItem(this, PpiInfectionMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem CreatedByUserID
        {
            get
            {
                return new esQueryItem(this, PpiInfectionMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, PpiInfectionMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, PpiInfectionMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("PpiInfectionCollection")]
    public partial class PpiInfectionCollection : esPpiInfectionCollection, IEnumerable<PpiInfection>
    {
        public PpiInfectionCollection()
        {

        }

        public static implicit operator List<PpiInfection>(PpiInfectionCollection coll)
        {
            List<PpiInfection> list = new List<PpiInfection>();

            foreach (PpiInfection emp in coll)
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
                return PpiInfectionMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PpiInfectionQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new PpiInfection(row);
        }

        override protected esEntity CreateEntity()
        {
            return new PpiInfection();
        }

        #endregion

        [BrowsableAttribute(false)]
        public PpiInfectionQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PpiInfectionQuery();
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
        public bool Load(PpiInfectionQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public PpiInfection AddNew()
        {
            PpiInfection entity = base.AddNewEntity() as PpiInfection;

            return entity;
        }
        public PpiInfection FindByPrimaryKey(String registrationNo, String sRInfectionType)
        {
            return base.FindByPrimaryKey(registrationNo, sRInfectionType) as PpiInfection;
        }

        #region IEnumerable< PpiInfection> Members

        IEnumerator<PpiInfection> IEnumerable<PpiInfection>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as PpiInfection;
            }
        }

        #endregion

        private PpiInfectionQuery query;
    }


    /// <summary>
    /// Encapsulates the 'PpiInfection' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("PpiInfection ({RegistrationNo, SRInfectionType})")]
    [Serializable]
    public partial class PpiInfection : esPpiInfection
    {
        public PpiInfection()
        {
        }

        public PpiInfection(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return PpiInfectionMetadata.Meta();
            }
        }

        override protected esPpiInfectionQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PpiInfectionQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public PpiInfectionQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PpiInfectionQuery();
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
        public bool Load(PpiInfectionQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private PpiInfectionQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class PpiInfectionQuery : esPpiInfectionQuery
    {
        public PpiInfectionQuery()
        {

        }

        public PpiInfectionQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "PpiInfectionQuery";
        }
    }

    [Serializable]
    public partial class PpiInfectionMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected PpiInfectionMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(PpiInfectionMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = PpiInfectionMetadata.PropertyNames.RegistrationNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(PpiInfectionMetadata.ColumnNames.SRInfectionType, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = PpiInfectionMetadata.PropertyNames.SRInfectionType;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(PpiInfectionMetadata.ColumnNames.DaysTo, 2, typeof(System.Int16), esSystemType.Int16);
            c.PropertyName = PpiInfectionMetadata.PropertyNames.DaysTo;
            c.NumericPrecision = 5;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiInfectionMetadata.ColumnNames.Cultures, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = PpiInfectionMetadata.PropertyNames.Cultures;
            c.CharacterMaxLength = 500;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiInfectionMetadata.ColumnNames.CreatedDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PpiInfectionMetadata.PropertyNames.CreatedDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiInfectionMetadata.ColumnNames.CreatedByUserID, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = PpiInfectionMetadata.PropertyNames.CreatedByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiInfectionMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PpiInfectionMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiInfectionMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = PpiInfectionMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public PpiInfectionMetadata Meta()
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
            public const string SRInfectionType = "SRInfectionType";
            public const string DaysTo = "DaysTo";
            public const string Cultures = "Cultures";
            public const string CreatedDateTime = "CreatedDateTime";
            public const string CreatedByUserID = "CreatedByUserID";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string RegistrationNo = "RegistrationNo";
            public const string SRInfectionType = "SRInfectionType";
            public const string DaysTo = "DaysTo";
            public const string Cultures = "Cultures";
            public const string CreatedDateTime = "CreatedDateTime";
            public const string CreatedByUserID = "CreatedByUserID";
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
            lock (typeof(PpiInfectionMetadata))
            {
                if (PpiInfectionMetadata.mapDelegates == null)
                {
                    PpiInfectionMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (PpiInfectionMetadata.meta == null)
                {
                    PpiInfectionMetadata.meta = new PpiInfectionMetadata();
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
                meta.AddTypeMap("SRInfectionType", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("DaysTo", new esTypeMap("smallint", "System.Int16"));
                meta.AddTypeMap("Cultures", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "PpiInfection";
                meta.Destination = "PpiInfection";
                meta.spInsert = "proc_PpiInfectionInsert";
                meta.spUpdate = "proc_PpiInfectionUpdate";
                meta.spDelete = "proc_PpiInfectionDelete";
                meta.spLoadAll = "proc_PpiInfectionLoadAll";
                meta.spLoadByPrimaryKey = "proc_PpiInfectionLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private PpiInfectionMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
