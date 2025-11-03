/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 8/8/2016 11:23:14 AM
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
    abstract public class esPatientIncidentSafetyGoalsCollection : esEntityCollectionWAuditLog
    {
        public esPatientIncidentSafetyGoalsCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "PatientIncidentSafetyGoalsCollection";
        }

        #region Query Logic
        protected void InitQuery(esPatientIncidentSafetyGoalsQuery query)
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
            this.InitQuery(query as esPatientIncidentSafetyGoalsQuery);
        }
        #endregion

        virtual public PatientIncidentSafetyGoals DetachEntity(PatientIncidentSafetyGoals entity)
        {
            return base.DetachEntity(entity) as PatientIncidentSafetyGoals;
        }

        virtual public PatientIncidentSafetyGoals AttachEntity(PatientIncidentSafetyGoals entity)
        {
            return base.AttachEntity(entity) as PatientIncidentSafetyGoals;
        }

        virtual public void Combine(PatientIncidentSafetyGoalsCollection collection)
        {
            base.Combine(collection);
        }

        new public PatientIncidentSafetyGoals this[int index]
        {
            get
            {
                return base[index] as PatientIncidentSafetyGoals;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(PatientIncidentSafetyGoals);
        }
    }

    [Serializable]
    abstract public class esPatientIncidentSafetyGoals : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esPatientIncidentSafetyGoalsQuery GetDynamicQuery()
        {
            return null;
        }

        public esPatientIncidentSafetyGoals()
        {
        }

        public esPatientIncidentSafetyGoals(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String patientIncidentNo, String sRSafetyGoals)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(patientIncidentNo, sRSafetyGoals);
            else
                return LoadByPrimaryKeyStoredProcedure(patientIncidentNo, sRSafetyGoals);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String patientIncidentNo, String sRSafetyGoals)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(patientIncidentNo, sRSafetyGoals);
            else
                return LoadByPrimaryKeyStoredProcedure(patientIncidentNo, sRSafetyGoals);
        }

        private bool LoadByPrimaryKeyDynamic(String patientIncidentNo, String sRSafetyGoals)
        {
            esPatientIncidentSafetyGoalsQuery query = this.GetDynamicQuery();
            query.Where(query.PatientIncidentNo == patientIncidentNo, query.SRSafetyGoals == sRSafetyGoals);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String patientIncidentNo, String sRSafetyGoals)
        {
            esParameters parms = new esParameters();
            parms.Add("PatientIncidentNo", patientIncidentNo);
            parms.Add("SRSafetyGoals", sRSafetyGoals);
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
                        case "PatientIncidentNo": this.str.PatientIncidentNo = (string)value; break;
                        case "SRSafetyGoals": this.str.SRSafetyGoals = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "Recommendation": this.str.Recommendation = (string)value; break;
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
        /// Maps to PatientIncidentSafetyGoals.PatientIncidentNo
        /// </summary>
        virtual public System.String PatientIncidentNo
        {
            get
            {
                return base.GetSystemString(PatientIncidentSafetyGoalsMetadata.ColumnNames.PatientIncidentNo);
            }

            set
            {
                base.SetSystemString(PatientIncidentSafetyGoalsMetadata.ColumnNames.PatientIncidentNo, value);
            }
        }
        /// <summary>
        /// Maps to PatientIncidentSafetyGoals.SRSafetyGoals
        /// </summary>
        virtual public System.String SRSafetyGoals
        {
            get
            {
                return base.GetSystemString(PatientIncidentSafetyGoalsMetadata.ColumnNames.SRSafetyGoals);
            }

            set
            {
                base.SetSystemString(PatientIncidentSafetyGoalsMetadata.ColumnNames.SRSafetyGoals, value);
            }
        }
        /// <summary>
        /// Maps to PatientIncidentSafetyGoals.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(PatientIncidentSafetyGoalsMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(PatientIncidentSafetyGoalsMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to PatientIncidentSafetyGoals.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(PatientIncidentSafetyGoalsMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(PatientIncidentSafetyGoalsMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }

        /// <summary>
        /// Maps to PatientIncidentSafetyGoals.Modus
        /// </summary>
        virtual public System.String Recommendation
        {
            get
            {
                return base.GetSystemString(PatientIncidentSafetyGoalsMetadata.ColumnNames.Recommendation);
            }

            set
            {
                base.SetSystemString(PatientIncidentSafetyGoalsMetadata.ColumnNames.Recommendation, value);
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
            public esStrings(esPatientIncidentSafetyGoals entity)
            {
                this.entity = entity;
            }
            public System.String PatientIncidentNo
            {
                get
                {
                    System.String data = entity.PatientIncidentNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PatientIncidentNo = null;
                    else entity.PatientIncidentNo = Convert.ToString(value);
                }
            }
            public System.String SRSafetyGoals
            {
                get
                {
                    System.String data = entity.SRSafetyGoals;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRSafetyGoals = null;
                    else entity.SRSafetyGoals = Convert.ToString(value);
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

            public System.String Recommendation
            {
                get
                {
                    System.String data = entity.Recommendation;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Recommendation = null;
                    else entity.Recommendation = Convert.ToString(value);
                }
            }
            private esPatientIncidentSafetyGoals entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esPatientIncidentSafetyGoalsQuery query)
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
                throw new Exception("esPatientIncidentSafetyGoals can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class PatientIncidentSafetyGoals : esPatientIncidentSafetyGoals
    {
    }

    [Serializable]
    abstract public class esPatientIncidentSafetyGoalsQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return PatientIncidentSafetyGoalsMetadata.Meta();
            }
        }

        public esQueryItem PatientIncidentNo
        {
            get
            {
                return new esQueryItem(this, PatientIncidentSafetyGoalsMetadata.ColumnNames.PatientIncidentNo, esSystemType.String);
            }
        }

        public esQueryItem SRSafetyGoals
        {
            get
            {
                return new esQueryItem(this, PatientIncidentSafetyGoalsMetadata.ColumnNames.SRSafetyGoals, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, PatientIncidentSafetyGoalsMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, PatientIncidentSafetyGoalsMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem Recommendation
        {
            get
            {
                return new esQueryItem(this, PatientIncidentSafetyGoalsMetadata.ColumnNames.Recommendation, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("PatientIncidentSafetyGoalsCollection")]
    public partial class PatientIncidentSafetyGoalsCollection : esPatientIncidentSafetyGoalsCollection, IEnumerable<PatientIncidentSafetyGoals>
    {
        public PatientIncidentSafetyGoalsCollection()
        {

        }

        public static implicit operator List<PatientIncidentSafetyGoals>(PatientIncidentSafetyGoalsCollection coll)
        {
            List<PatientIncidentSafetyGoals> list = new List<PatientIncidentSafetyGoals>();

            foreach (PatientIncidentSafetyGoals emp in coll)
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
                return PatientIncidentSafetyGoalsMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PatientIncidentSafetyGoalsQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new PatientIncidentSafetyGoals(row);
        }

        override protected esEntity CreateEntity()
        {
            return new PatientIncidentSafetyGoals();
        }

        #endregion

        [BrowsableAttribute(false)]
        public PatientIncidentSafetyGoalsQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PatientIncidentSafetyGoalsQuery();
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
        public bool Load(PatientIncidentSafetyGoalsQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public PatientIncidentSafetyGoals AddNew()
        {
            PatientIncidentSafetyGoals entity = base.AddNewEntity() as PatientIncidentSafetyGoals;

            return entity;
        }
        public PatientIncidentSafetyGoals FindByPrimaryKey(String patientIncidentNo, String sRSafetyGoals)
        {
            return base.FindByPrimaryKey(patientIncidentNo, sRSafetyGoals) as PatientIncidentSafetyGoals;
        }

        #region IEnumerable< PatientIncidentSafetyGoals> Members

        IEnumerator<PatientIncidentSafetyGoals> IEnumerable<PatientIncidentSafetyGoals>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as PatientIncidentSafetyGoals;
            }
        }

        #endregion

        private PatientIncidentSafetyGoalsQuery query;
    }


    /// <summary>
    /// Encapsulates the 'PatientIncidentSafetyGoals' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("PatientIncidentSafetyGoals ({PatientIncidentNo, SRSafetyGoals})")]
    [Serializable]
    public partial class PatientIncidentSafetyGoals : esPatientIncidentSafetyGoals
    {
        public PatientIncidentSafetyGoals()
        {
        }

        public PatientIncidentSafetyGoals(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return PatientIncidentSafetyGoalsMetadata.Meta();
            }
        }

        override protected esPatientIncidentSafetyGoalsQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PatientIncidentSafetyGoalsQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public PatientIncidentSafetyGoalsQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PatientIncidentSafetyGoalsQuery();
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
        public bool Load(PatientIncidentSafetyGoalsQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private PatientIncidentSafetyGoalsQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class PatientIncidentSafetyGoalsQuery : esPatientIncidentSafetyGoalsQuery
    {
        public PatientIncidentSafetyGoalsQuery()
        {

        }

        public PatientIncidentSafetyGoalsQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "PatientIncidentSafetyGoalsQuery";
        }
    }

    [Serializable]
    public partial class PatientIncidentSafetyGoalsMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected PatientIncidentSafetyGoalsMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(PatientIncidentSafetyGoalsMetadata.ColumnNames.PatientIncidentNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientIncidentSafetyGoalsMetadata.PropertyNames.PatientIncidentNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(PatientIncidentSafetyGoalsMetadata.ColumnNames.SRSafetyGoals, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientIncidentSafetyGoalsMetadata.PropertyNames.SRSafetyGoals;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(PatientIncidentSafetyGoalsMetadata.ColumnNames.LastUpdateDateTime, 2, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PatientIncidentSafetyGoalsMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientIncidentSafetyGoalsMetadata.ColumnNames.LastUpdateByUserID, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientIncidentSafetyGoalsMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientIncidentSafetyGoalsMetadata.ColumnNames.Recommendation, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientIncidentSafetyGoalsMetadata.PropertyNames.Recommendation;
            c.CharacterMaxLength = 500;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public PatientIncidentSafetyGoalsMetadata Meta()
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
            public const string PatientIncidentNo = "PatientIncidentNo";
            public const string SRSafetyGoals = "SRSafetyGoals";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string Recommendation = "Recommendation";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string PatientIncidentNo = "PatientIncidentNo";
            public const string SRSafetyGoals = "SRSafetyGoals";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string Recommendation = "Recommendation";
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
            lock (typeof(PatientIncidentSafetyGoalsMetadata))
            {
                if (PatientIncidentSafetyGoalsMetadata.mapDelegates == null)
                {
                    PatientIncidentSafetyGoalsMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (PatientIncidentSafetyGoalsMetadata.meta == null)
                {
                    PatientIncidentSafetyGoalsMetadata.meta = new PatientIncidentSafetyGoalsMetadata();
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

                meta.AddTypeMap("PatientIncidentNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRSafetyGoals", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Recommendation", new esTypeMap("varchar", "System.String"));


                meta.Source = "PatientIncidentSafetyGoals";
                meta.Destination = "PatientIncidentSafetyGoals";
                meta.spInsert = "proc_PatientIncidentSafetyGoalsInsert";
                meta.spUpdate = "proc_PatientIncidentSafetyGoalsUpdate";
                meta.spDelete = "proc_PatientIncidentSafetyGoalsDelete";
                meta.spLoadAll = "proc_PatientIncidentSafetyGoalsLoadAll";
                meta.spLoadByPrimaryKey = "proc_PatientIncidentSafetyGoalsLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private PatientIncidentSafetyGoalsMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
