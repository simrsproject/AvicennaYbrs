/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 2/4/2020 4:54:03 PM
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
    abstract public class esPatientIncidentRelatedUnitCollection : esEntityCollectionWAuditLog
    {
        public esPatientIncidentRelatedUnitCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "PatientIncidentRelatedUnitCollection";
        }

        #region Query Logic
        protected void InitQuery(esPatientIncidentRelatedUnitQuery query)
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
            this.InitQuery(query as esPatientIncidentRelatedUnitQuery);
        }
        #endregion

        virtual public PatientIncidentRelatedUnit DetachEntity(PatientIncidentRelatedUnit entity)
        {
            return base.DetachEntity(entity) as PatientIncidentRelatedUnit;
        }

        virtual public PatientIncidentRelatedUnit AttachEntity(PatientIncidentRelatedUnit entity)
        {
            return base.AttachEntity(entity) as PatientIncidentRelatedUnit;
        }

        virtual public void Combine(PatientIncidentRelatedUnitCollection collection)
        {
            base.Combine(collection);
        }

        new public PatientIncidentRelatedUnit this[int index]
        {
            get
            {
                return base[index] as PatientIncidentRelatedUnit;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(PatientIncidentRelatedUnit);
        }
    }

    [Serializable]
    abstract public class esPatientIncidentRelatedUnit : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esPatientIncidentRelatedUnitQuery GetDynamicQuery()
        {
            return null;
        }

        public esPatientIncidentRelatedUnit()
        {
        }

        public esPatientIncidentRelatedUnit(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String patientIncidentNo, String serviceUnitID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(patientIncidentNo, serviceUnitID);
            else
                return LoadByPrimaryKeyStoredProcedure(patientIncidentNo, serviceUnitID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String patientIncidentNo, String serviceUnitID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(patientIncidentNo, serviceUnitID);
            else
                return LoadByPrimaryKeyStoredProcedure(patientIncidentNo, serviceUnitID);
        }

        private bool LoadByPrimaryKeyDynamic(String patientIncidentNo, String serviceUnitID)
        {
            esPatientIncidentRelatedUnitQuery query = this.GetDynamicQuery();
            query.Where(query.PatientIncidentNo == patientIncidentNo, query.ServiceUnitID == serviceUnitID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String patientIncidentNo, String serviceUnitID)
        {
            esParameters parms = new esParameters();
            parms.Add("PatientIncidentNo", patientIncidentNo);
            parms.Add("ServiceUnitID", serviceUnitID);
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
                        case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
                        case "IncidentDirectCause": this.str.IncidentDirectCause = (string)value; break;
                        case "IncidentUnderlyingCauses": this.str.IncidentUnderlyingCauses = (string)value; break;
                        case "InvestigationByUserID": this.str.InvestigationByUserID = (string)value; break;
                        case "InvestigationDateTime": this.str.InvestigationDateTime = (string)value; break;
                        case "IsInvestigationApproved": this.str.IsInvestigationApproved = (string)value; break;
                        case "InvestigationApprovedByUserID": this.str.InvestigationApprovedByUserID = (string)value; break;
                        case "InvestigationApprovedDateTime": this.str.InvestigationApprovedDateTime = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "IncidentChronologyCauses": this.str.IncidentChronologyCauses = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "InvestigationDateTime":

                            if (value == null || value is System.DateTime)
                                this.InvestigationDateTime = (System.DateTime?)value;
                            break;
                        case "IsInvestigationApproved":

                            if (value == null || value is System.Boolean)
                                this.IsInvestigationApproved = (System.Boolean?)value;
                            break;
                        case "InvestigationApprovedDateTime":

                            if (value == null || value is System.DateTime)
                                this.InvestigationApprovedDateTime = (System.DateTime?)value;
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
        /// Maps to PatientIncidentRelatedUnit.PatientIncidentNo
        /// </summary>
        virtual public System.String PatientIncidentNo
        {
            get
            {
                return base.GetSystemString(PatientIncidentRelatedUnitMetadata.ColumnNames.PatientIncidentNo);
            }

            set
            {
                base.SetSystemString(PatientIncidentRelatedUnitMetadata.ColumnNames.PatientIncidentNo, value);
            }
        }
        /// <summary>
        /// Maps to PatientIncidentRelatedUnit.ServiceUnitID
        /// </summary>
        virtual public System.String ServiceUnitID
        {
            get
            {
                return base.GetSystemString(PatientIncidentRelatedUnitMetadata.ColumnNames.ServiceUnitID);
            }

            set
            {
                base.SetSystemString(PatientIncidentRelatedUnitMetadata.ColumnNames.ServiceUnitID, value);
            }
        }
        /// <summary>
        /// Maps to PatientIncidentRelatedUnit.IncidentDirectCause
        /// </summary>
        virtual public System.String IncidentDirectCause
        {
            get
            {
                return base.GetSystemString(PatientIncidentRelatedUnitMetadata.ColumnNames.IncidentDirectCause);
            }

            set
            {
                base.SetSystemString(PatientIncidentRelatedUnitMetadata.ColumnNames.IncidentDirectCause, value);
            }
        }
        /// <summary>
        /// Maps to PatientIncidentRelatedUnit.IncidentUnderlyingCauses
        /// </summary>
        virtual public System.String IncidentUnderlyingCauses
        {
            get
            {
                return base.GetSystemString(PatientIncidentRelatedUnitMetadata.ColumnNames.IncidentUnderlyingCauses);
            }

            set
            {
                base.SetSystemString(PatientIncidentRelatedUnitMetadata.ColumnNames.IncidentUnderlyingCauses, value);
            }
        }
        /// <summary>
        /// Maps to PatientIncidentRelatedUnit.InvestigationByUserID
        /// </summary>
        virtual public System.String InvestigationByUserID
        {
            get
            {
                return base.GetSystemString(PatientIncidentRelatedUnitMetadata.ColumnNames.InvestigationByUserID);
            }

            set
            {
                base.SetSystemString(PatientIncidentRelatedUnitMetadata.ColumnNames.InvestigationByUserID, value);
            }
        }
        /// <summary>
        /// Maps to PatientIncidentRelatedUnit.InvestigationDateTime
        /// </summary>
        virtual public System.DateTime? InvestigationDateTime
        {
            get
            {
                return base.GetSystemDateTime(PatientIncidentRelatedUnitMetadata.ColumnNames.InvestigationDateTime);
            }

            set
            {
                base.SetSystemDateTime(PatientIncidentRelatedUnitMetadata.ColumnNames.InvestigationDateTime, value);
            }
        }
        /// <summary>
        /// Maps to PatientIncidentRelatedUnit.IsInvestigationApproved
        /// </summary>
        virtual public System.Boolean? IsInvestigationApproved
        {
            get
            {
                return base.GetSystemBoolean(PatientIncidentRelatedUnitMetadata.ColumnNames.IsInvestigationApproved);
            }

            set
            {
                base.SetSystemBoolean(PatientIncidentRelatedUnitMetadata.ColumnNames.IsInvestigationApproved, value);
            }
        }
        /// <summary>
        /// Maps to PatientIncidentRelatedUnit.InvestigationApprovedByUserID
        /// </summary>
        virtual public System.String InvestigationApprovedByUserID
        {
            get
            {
                return base.GetSystemString(PatientIncidentRelatedUnitMetadata.ColumnNames.InvestigationApprovedByUserID);
            }

            set
            {
                base.SetSystemString(PatientIncidentRelatedUnitMetadata.ColumnNames.InvestigationApprovedByUserID, value);
            }
        }
        /// <summary>
        /// Maps to PatientIncidentRelatedUnit.InvestigationApprovedDateTime
        /// </summary>
        virtual public System.DateTime? InvestigationApprovedDateTime
        {
            get
            {
                return base.GetSystemDateTime(PatientIncidentRelatedUnitMetadata.ColumnNames.InvestigationApprovedDateTime);
            }

            set
            {
                base.SetSystemDateTime(PatientIncidentRelatedUnitMetadata.ColumnNames.InvestigationApprovedDateTime, value);
            }
        }
        /// <summary>
        /// Maps to PatientIncidentRelatedUnit.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(PatientIncidentRelatedUnitMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(PatientIncidentRelatedUnitMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to PatientIncidentRelatedUnit.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(PatientIncidentRelatedUnitMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(PatientIncidentRelatedUnitMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to PatientIncidentRelatedUnit.IncidentChronologyCauses
        /// </summary>
        virtual public System.String IncidentChronologyCauses
        {
            get
            {
                return base.GetSystemString(PatientIncidentRelatedUnitMetadata.ColumnNames.IncidentChronologyCauses);
            }

            set
            {
                base.SetSystemString(PatientIncidentRelatedUnitMetadata.ColumnNames.IncidentChronologyCauses, value);
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
            public esStrings(esPatientIncidentRelatedUnit entity)
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
            public System.String ServiceUnitID
            {
                get
                {
                    System.String data = entity.ServiceUnitID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ServiceUnitID = null;
                    else entity.ServiceUnitID = Convert.ToString(value);
                }
            }
            public System.String IncidentDirectCause
            {
                get
                {
                    System.String data = entity.IncidentDirectCause;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IncidentDirectCause = null;
                    else entity.IncidentDirectCause = Convert.ToString(value);
                }
            }
            public System.String IncidentUnderlyingCauses
            {
                get
                {
                    System.String data = entity.IncidentUnderlyingCauses;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IncidentUnderlyingCauses = null;
                    else entity.IncidentUnderlyingCauses = Convert.ToString(value);
                }
            }
            public System.String InvestigationByUserID
            {
                get
                {
                    System.String data = entity.InvestigationByUserID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.InvestigationByUserID = null;
                    else entity.InvestigationByUserID = Convert.ToString(value);
                }
            }
            public System.String InvestigationDateTime
            {
                get
                {
                    System.DateTime? data = entity.InvestigationDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.InvestigationDateTime = null;
                    else entity.InvestigationDateTime = Convert.ToDateTime(value);
                }
            }
            public System.String IsInvestigationApproved
            {
                get
                {
                    System.Boolean? data = entity.IsInvestigationApproved;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsInvestigationApproved = null;
                    else entity.IsInvestigationApproved = Convert.ToBoolean(value);
                }
            }
            public System.String InvestigationApprovedByUserID
            {
                get
                {
                    System.String data = entity.InvestigationApprovedByUserID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.InvestigationApprovedByUserID = null;
                    else entity.InvestigationApprovedByUserID = Convert.ToString(value);
                }
            }
            public System.String InvestigationApprovedDateTime
            {
                get
                {
                    System.DateTime? data = entity.InvestigationApprovedDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.InvestigationApprovedDateTime = null;
                    else entity.InvestigationApprovedDateTime = Convert.ToDateTime(value);
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
            public System.String IncidentChronologyCauses
            {
                get
                {
                    System.String data = entity.IncidentChronologyCauses;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IncidentChronologyCauses = null;
                    else entity.IncidentChronologyCauses = Convert.ToString(value);
                }
            }
            private esPatientIncidentRelatedUnit entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esPatientIncidentRelatedUnitQuery query)
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
                throw new Exception("esPatientIncidentRelatedUnit can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class PatientIncidentRelatedUnit : esPatientIncidentRelatedUnit
    {
    }

    [Serializable]
    abstract public class esPatientIncidentRelatedUnitQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return PatientIncidentRelatedUnitMetadata.Meta();
            }
        }

        public esQueryItem PatientIncidentNo
        {
            get
            {
                return new esQueryItem(this, PatientIncidentRelatedUnitMetadata.ColumnNames.PatientIncidentNo, esSystemType.String);
            }
        }

        public esQueryItem ServiceUnitID
        {
            get
            {
                return new esQueryItem(this, PatientIncidentRelatedUnitMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
            }
        }

        public esQueryItem IncidentDirectCause
        {
            get
            {
                return new esQueryItem(this, PatientIncidentRelatedUnitMetadata.ColumnNames.IncidentDirectCause, esSystemType.String);
            }
        }

        public esQueryItem IncidentUnderlyingCauses
        {
            get
            {
                return new esQueryItem(this, PatientIncidentRelatedUnitMetadata.ColumnNames.IncidentUnderlyingCauses, esSystemType.String);
            }
        }

        public esQueryItem InvestigationByUserID
        {
            get
            {
                return new esQueryItem(this, PatientIncidentRelatedUnitMetadata.ColumnNames.InvestigationByUserID, esSystemType.String);
            }
        }

        public esQueryItem InvestigationDateTime
        {
            get
            {
                return new esQueryItem(this, PatientIncidentRelatedUnitMetadata.ColumnNames.InvestigationDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem IsInvestigationApproved
        {
            get
            {
                return new esQueryItem(this, PatientIncidentRelatedUnitMetadata.ColumnNames.IsInvestigationApproved, esSystemType.Boolean);
            }
        }

        public esQueryItem InvestigationApprovedByUserID
        {
            get
            {
                return new esQueryItem(this, PatientIncidentRelatedUnitMetadata.ColumnNames.InvestigationApprovedByUserID, esSystemType.String);
            }
        }

        public esQueryItem InvestigationApprovedDateTime
        {
            get
            {
                return new esQueryItem(this, PatientIncidentRelatedUnitMetadata.ColumnNames.InvestigationApprovedDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, PatientIncidentRelatedUnitMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, PatientIncidentRelatedUnitMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem IncidentChronologyCauses
        {
            get
            {
                return new esQueryItem(this, PatientIncidentRelatedUnitMetadata.ColumnNames.IncidentChronologyCauses, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("PatientIncidentRelatedUnitCollection")]
    public partial class PatientIncidentRelatedUnitCollection : esPatientIncidentRelatedUnitCollection, IEnumerable<PatientIncidentRelatedUnit>
    {
        public PatientIncidentRelatedUnitCollection()
        {

        }

        public static implicit operator List<PatientIncidentRelatedUnit>(PatientIncidentRelatedUnitCollection coll)
        {
            List<PatientIncidentRelatedUnit> list = new List<PatientIncidentRelatedUnit>();

            foreach (PatientIncidentRelatedUnit emp in coll)
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
                return PatientIncidentRelatedUnitMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PatientIncidentRelatedUnitQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new PatientIncidentRelatedUnit(row);
        }

        override protected esEntity CreateEntity()
        {
            return new PatientIncidentRelatedUnit();
        }

        #endregion

        [BrowsableAttribute(false)]
        public PatientIncidentRelatedUnitQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PatientIncidentRelatedUnitQuery();
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
        public bool Load(PatientIncidentRelatedUnitQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public PatientIncidentRelatedUnit AddNew()
        {
            PatientIncidentRelatedUnit entity = base.AddNewEntity() as PatientIncidentRelatedUnit;

            return entity;
        }
        public PatientIncidentRelatedUnit FindByPrimaryKey(String patientIncidentNo, String serviceUnitID)
        {
            return base.FindByPrimaryKey(patientIncidentNo, serviceUnitID) as PatientIncidentRelatedUnit;
        }

        #region IEnumerable< PatientIncidentRelatedUnit> Members

        IEnumerator<PatientIncidentRelatedUnit> IEnumerable<PatientIncidentRelatedUnit>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as PatientIncidentRelatedUnit;
            }
        }

        #endregion

        private PatientIncidentRelatedUnitQuery query;
    }


    /// <summary>
    /// Encapsulates the 'PatientIncidentRelatedUnit' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("PatientIncidentRelatedUnit ({PatientIncidentNo, ServiceUnitID})")]
    [Serializable]
    public partial class PatientIncidentRelatedUnit : esPatientIncidentRelatedUnit
    {
        public PatientIncidentRelatedUnit()
        {
        }

        public PatientIncidentRelatedUnit(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return PatientIncidentRelatedUnitMetadata.Meta();
            }
        }

        override protected esPatientIncidentRelatedUnitQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PatientIncidentRelatedUnitQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public PatientIncidentRelatedUnitQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PatientIncidentRelatedUnitQuery();
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
        public bool Load(PatientIncidentRelatedUnitQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private PatientIncidentRelatedUnitQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class PatientIncidentRelatedUnitQuery : esPatientIncidentRelatedUnitQuery
    {
        public PatientIncidentRelatedUnitQuery()
        {

        }

        public PatientIncidentRelatedUnitQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "PatientIncidentRelatedUnitQuery";
        }
    }

    [Serializable]
    public partial class PatientIncidentRelatedUnitMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected PatientIncidentRelatedUnitMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(PatientIncidentRelatedUnitMetadata.ColumnNames.PatientIncidentNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientIncidentRelatedUnitMetadata.PropertyNames.PatientIncidentNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(PatientIncidentRelatedUnitMetadata.ColumnNames.ServiceUnitID, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientIncidentRelatedUnitMetadata.PropertyNames.ServiceUnitID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(PatientIncidentRelatedUnitMetadata.ColumnNames.IncidentDirectCause, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientIncidentRelatedUnitMetadata.PropertyNames.IncidentDirectCause;
            c.CharacterMaxLength = 500;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientIncidentRelatedUnitMetadata.ColumnNames.IncidentUnderlyingCauses, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientIncidentRelatedUnitMetadata.PropertyNames.IncidentUnderlyingCauses;
            c.CharacterMaxLength = 500;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientIncidentRelatedUnitMetadata.ColumnNames.InvestigationByUserID, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientIncidentRelatedUnitMetadata.PropertyNames.InvestigationByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientIncidentRelatedUnitMetadata.ColumnNames.InvestigationDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PatientIncidentRelatedUnitMetadata.PropertyNames.InvestigationDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientIncidentRelatedUnitMetadata.ColumnNames.IsInvestigationApproved, 6, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = PatientIncidentRelatedUnitMetadata.PropertyNames.IsInvestigationApproved;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientIncidentRelatedUnitMetadata.ColumnNames.InvestigationApprovedByUserID, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientIncidentRelatedUnitMetadata.PropertyNames.InvestigationApprovedByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientIncidentRelatedUnitMetadata.ColumnNames.InvestigationApprovedDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PatientIncidentRelatedUnitMetadata.PropertyNames.InvestigationApprovedDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientIncidentRelatedUnitMetadata.ColumnNames.LastUpdateDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PatientIncidentRelatedUnitMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientIncidentRelatedUnitMetadata.ColumnNames.LastUpdateByUserID, 10, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientIncidentRelatedUnitMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientIncidentRelatedUnitMetadata.ColumnNames.IncidentChronologyCauses, 11, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientIncidentRelatedUnitMetadata.PropertyNames.IncidentChronologyCauses;
            c.CharacterMaxLength = 5000;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public PatientIncidentRelatedUnitMetadata Meta()
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
            public const string ServiceUnitID = "ServiceUnitID";
            public const string IncidentDirectCause = "IncidentDirectCause";
            public const string IncidentUnderlyingCauses = "IncidentUnderlyingCauses";
            public const string InvestigationByUserID = "InvestigationByUserID";
            public const string InvestigationDateTime = "InvestigationDateTime";
            public const string IsInvestigationApproved = "IsInvestigationApproved";
            public const string InvestigationApprovedByUserID = "InvestigationApprovedByUserID";
            public const string InvestigationApprovedDateTime = "InvestigationApprovedDateTime";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string IncidentChronologyCauses = "IncidentChronologyCauses";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string PatientIncidentNo = "PatientIncidentNo";
            public const string ServiceUnitID = "ServiceUnitID";
            public const string IncidentDirectCause = "IncidentDirectCause";
            public const string IncidentUnderlyingCauses = "IncidentUnderlyingCauses";
            public const string InvestigationByUserID = "InvestigationByUserID";
            public const string InvestigationDateTime = "InvestigationDateTime";
            public const string IsInvestigationApproved = "IsInvestigationApproved";
            public const string InvestigationApprovedByUserID = "InvestigationApprovedByUserID";
            public const string InvestigationApprovedDateTime = "InvestigationApprovedDateTime";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string IncidentChronologyCauses = "IncidentChronologyCauses";
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
            lock (typeof(PatientIncidentRelatedUnitMetadata))
            {
                if (PatientIncidentRelatedUnitMetadata.mapDelegates == null)
                {
                    PatientIncidentRelatedUnitMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (PatientIncidentRelatedUnitMetadata.meta == null)
                {
                    PatientIncidentRelatedUnitMetadata.meta = new PatientIncidentRelatedUnitMetadata();
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
                meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IncidentDirectCause", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IncidentUnderlyingCauses", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("InvestigationByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("InvestigationDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("IsInvestigationApproved", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("InvestigationApprovedByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("InvestigationApprovedDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IncidentChronologyCauses", new esTypeMap("varchar", "System.String"));


                meta.Source = "PatientIncidentRelatedUnit";
                meta.Destination = "PatientIncidentRelatedUnit";
                meta.spInsert = "proc_PatientIncidentRelatedUnitInsert";
                meta.spUpdate = "proc_PatientIncidentRelatedUnitUpdate";
                meta.spDelete = "proc_PatientIncidentRelatedUnitDelete";
                meta.spLoadAll = "proc_PatientIncidentRelatedUnitLoadAll";
                meta.spLoadByPrimaryKey = "proc_PatientIncidentRelatedUnitLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private PatientIncidentRelatedUnitMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
