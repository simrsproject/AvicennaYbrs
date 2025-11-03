/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 4/2/2017 6:38:34 PM
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
    abstract public class esPpiAntimicrobialApplicationsCollection : esEntityCollectionWAuditLog
    {
        public esPpiAntimicrobialApplicationsCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "PpiAntimicrobialApplicationsCollection";
        }

        #region Query Logic
        protected void InitQuery(esPpiAntimicrobialApplicationsQuery query)
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
            this.InitQuery(query as esPpiAntimicrobialApplicationsQuery);
        }
        #endregion

        virtual public PpiAntimicrobialApplications DetachEntity(PpiAntimicrobialApplications entity)
        {
            return base.DetachEntity(entity) as PpiAntimicrobialApplications;
        }

        virtual public PpiAntimicrobialApplications AttachEntity(PpiAntimicrobialApplications entity)
        {
            return base.AttachEntity(entity) as PpiAntimicrobialApplications;
        }

        virtual public void Combine(PpiAntimicrobialApplicationsCollection collection)
        {
            base.Combine(collection);
        }

        new public PpiAntimicrobialApplications this[int index]
        {
            get
            {
                return base[index] as PpiAntimicrobialApplications;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(PpiAntimicrobialApplications);
        }
    }

    [Serializable]
    abstract public class esPpiAntimicrobialApplications : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esPpiAntimicrobialApplicationsQuery GetDynamicQuery()
        {
            return null;
        }

        public esPpiAntimicrobialApplications()
        {
        }

        public esPpiAntimicrobialApplications(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String registrationNo, String sRTherapyGroup, String therapyID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(registrationNo, sRTherapyGroup, therapyID);
            else
                return LoadByPrimaryKeyStoredProcedure(registrationNo, sRTherapyGroup, therapyID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String registrationNo, String sRTherapyGroup, String therapyID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(registrationNo, sRTherapyGroup, therapyID);
            else
                return LoadByPrimaryKeyStoredProcedure(registrationNo, sRTherapyGroup, therapyID);
        }

        private bool LoadByPrimaryKeyDynamic(String registrationNo, String sRTherapyGroup, String therapyID)
        {
            esPpiAntimicrobialApplicationsQuery query = this.GetDynamicQuery();
            query.Where(query.RegistrationNo == registrationNo, query.SRTherapyGroup == sRTherapyGroup, query.TherapyID == therapyID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String registrationNo, String sRTherapyGroup, String therapyID)
        {
            esParameters parms = new esParameters();
            parms.Add("RegistrationNo", registrationNo);
            parms.Add("SRTherapyGroup", sRTherapyGroup);
            parms.Add("TherapyID", therapyID);
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
                        case "SRTherapyGroup": this.str.SRTherapyGroup = (string)value; break;
                        case "TherapyID": this.str.TherapyID = (string)value; break;
                        case "Dosage": this.str.Dosage = (string)value; break;
                        case "SRDosageUnit": this.str.SRDosageUnit = (string)value; break;
                        case "StartDate": this.str.StartDate = (string)value; break;
                        case "EndDate": this.str.EndDate = (string)value; break;
                        case "SRAntimicrobialApplicationTiming": this.str.SRAntimicrobialApplicationTiming = (string)value; break;
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
                        case "Dosage":

                            if (value == null || value is System.Decimal)
                                this.Dosage = (System.Decimal?)value;
                            break;
                        case "StartDate":

                            if (value == null || value is System.DateTime)
                                this.StartDate = (System.DateTime?)value;
                            break;
                        case "EndDate":

                            if (value == null || value is System.DateTime)
                                this.EndDate = (System.DateTime?)value;
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
        /// Maps to PpiAntimicrobialApplications.RegistrationNo
        /// </summary>
        virtual public System.String RegistrationNo
        {
            get
            {
                return base.GetSystemString(PpiAntimicrobialApplicationsMetadata.ColumnNames.RegistrationNo);
            }

            set
            {
                base.SetSystemString(PpiAntimicrobialApplicationsMetadata.ColumnNames.RegistrationNo, value);
            }
        }
        /// <summary>
        /// Maps to PpiAntimicrobialApplications.SRTherapyGroup
        /// </summary>
        virtual public System.String SRTherapyGroup
        {
            get
            {
                return base.GetSystemString(PpiAntimicrobialApplicationsMetadata.ColumnNames.SRTherapyGroup);
            }

            set
            {
                base.SetSystemString(PpiAntimicrobialApplicationsMetadata.ColumnNames.SRTherapyGroup, value);
            }
        }
        /// <summary>
        /// Maps to PpiAntimicrobialApplications.TherapyID
        /// </summary>
        virtual public System.String TherapyID
        {
            get
            {
                return base.GetSystemString(PpiAntimicrobialApplicationsMetadata.ColumnNames.TherapyID);
            }

            set
            {
                base.SetSystemString(PpiAntimicrobialApplicationsMetadata.ColumnNames.TherapyID, value);
            }
        }
        /// <summary>
        /// Maps to PpiAntimicrobialApplications.Dosage
        /// </summary>
        virtual public System.Decimal? Dosage
        {
            get
            {
                return base.GetSystemDecimal(PpiAntimicrobialApplicationsMetadata.ColumnNames.Dosage);
            }

            set
            {
                base.SetSystemDecimal(PpiAntimicrobialApplicationsMetadata.ColumnNames.Dosage, value);
            }
        }
        /// <summary>
        /// Maps to PpiAntimicrobialApplications.SRDosageUnit
        /// </summary>
        virtual public System.String SRDosageUnit
        {
            get
            {
                return base.GetSystemString(PpiAntimicrobialApplicationsMetadata.ColumnNames.SRDosageUnit);
            }

            set
            {
                base.SetSystemString(PpiAntimicrobialApplicationsMetadata.ColumnNames.SRDosageUnit, value);
            }
        }
        /// <summary>
        /// Maps to PpiAntimicrobialApplications.StartDate
        /// </summary>
        virtual public System.DateTime? StartDate
        {
            get
            {
                return base.GetSystemDateTime(PpiAntimicrobialApplicationsMetadata.ColumnNames.StartDate);
            }

            set
            {
                base.SetSystemDateTime(PpiAntimicrobialApplicationsMetadata.ColumnNames.StartDate, value);
            }
        }
        /// <summary>
        /// Maps to PpiAntimicrobialApplications.EndDate
        /// </summary>
        virtual public System.DateTime? EndDate
        {
            get
            {
                return base.GetSystemDateTime(PpiAntimicrobialApplicationsMetadata.ColumnNames.EndDate);
            }

            set
            {
                base.SetSystemDateTime(PpiAntimicrobialApplicationsMetadata.ColumnNames.EndDate, value);
            }
        }
        /// <summary>
        /// Maps to PpiAntimicrobialApplications.SRAntimicrobialApplicationTiming
        /// </summary>
        virtual public System.String SRAntimicrobialApplicationTiming
        {
            get
            {
                return base.GetSystemString(PpiAntimicrobialApplicationsMetadata.ColumnNames.SRAntimicrobialApplicationTiming);
            }

            set
            {
                base.SetSystemString(PpiAntimicrobialApplicationsMetadata.ColumnNames.SRAntimicrobialApplicationTiming, value);
            }
        }
        /// <summary>
        /// Maps to PpiAntimicrobialApplications.CreatedDateTime
        /// </summary>
        virtual public System.DateTime? CreatedDateTime
        {
            get
            {
                return base.GetSystemDateTime(PpiAntimicrobialApplicationsMetadata.ColumnNames.CreatedDateTime);
            }

            set
            {
                base.SetSystemDateTime(PpiAntimicrobialApplicationsMetadata.ColumnNames.CreatedDateTime, value);
            }
        }
        /// <summary>
        /// Maps to PpiAntimicrobialApplications.CreatedByUserID
        /// </summary>
        virtual public System.String CreatedByUserID
        {
            get
            {
                return base.GetSystemString(PpiAntimicrobialApplicationsMetadata.ColumnNames.CreatedByUserID);
            }

            set
            {
                base.SetSystemString(PpiAntimicrobialApplicationsMetadata.ColumnNames.CreatedByUserID, value);
            }
        }
        /// <summary>
        /// Maps to PpiAntimicrobialApplications.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(PpiAntimicrobialApplicationsMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(PpiAntimicrobialApplicationsMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to PpiAntimicrobialApplications.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(PpiAntimicrobialApplicationsMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(PpiAntimicrobialApplicationsMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esPpiAntimicrobialApplications entity)
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
            public System.String SRTherapyGroup
            {
                get
                {
                    System.String data = entity.SRTherapyGroup;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRTherapyGroup = null;
                    else entity.SRTherapyGroup = Convert.ToString(value);
                }
            }
            public System.String TherapyID
            {
                get
                {
                    System.String data = entity.TherapyID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TherapyID = null;
                    else entity.TherapyID = Convert.ToString(value);
                }
            }
            public System.String Dosage
            {
                get
                {
                    System.Decimal? data = entity.Dosage;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Dosage = null;
                    else entity.Dosage = Convert.ToDecimal(value);
                }
            }
            public System.String SRDosageUnit
            {
                get
                {
                    System.String data = entity.SRDosageUnit;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRDosageUnit = null;
                    else entity.SRDosageUnit = Convert.ToString(value);
                }
            }
            public System.String StartDate
            {
                get
                {
                    System.DateTime? data = entity.StartDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.StartDate = null;
                    else entity.StartDate = Convert.ToDateTime(value);
                }
            }
            public System.String EndDate
            {
                get
                {
                    System.DateTime? data = entity.EndDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.EndDate = null;
                    else entity.EndDate = Convert.ToDateTime(value);
                }
            }
            public System.String SRAntimicrobialApplicationTiming
            {
                get
                {
                    System.String data = entity.SRAntimicrobialApplicationTiming;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRAntimicrobialApplicationTiming = null;
                    else entity.SRAntimicrobialApplicationTiming = Convert.ToString(value);
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
            private esPpiAntimicrobialApplications entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esPpiAntimicrobialApplicationsQuery query)
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
                throw new Exception("esPpiAntimicrobialApplications can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class PpiAntimicrobialApplications : esPpiAntimicrobialApplications
    {
    }

    [Serializable]
    abstract public class esPpiAntimicrobialApplicationsQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return PpiAntimicrobialApplicationsMetadata.Meta();
            }
        }

        public esQueryItem RegistrationNo
        {
            get
            {
                return new esQueryItem(this, PpiAntimicrobialApplicationsMetadata.ColumnNames.RegistrationNo, esSystemType.String);
            }
        }

        public esQueryItem SRTherapyGroup
        {
            get
            {
                return new esQueryItem(this, PpiAntimicrobialApplicationsMetadata.ColumnNames.SRTherapyGroup, esSystemType.String);
            }
        }

        public esQueryItem TherapyID
        {
            get
            {
                return new esQueryItem(this, PpiAntimicrobialApplicationsMetadata.ColumnNames.TherapyID, esSystemType.String);
            }
        }

        public esQueryItem Dosage
        {
            get
            {
                return new esQueryItem(this, PpiAntimicrobialApplicationsMetadata.ColumnNames.Dosage, esSystemType.Decimal);
            }
        }

        public esQueryItem SRDosageUnit
        {
            get
            {
                return new esQueryItem(this, PpiAntimicrobialApplicationsMetadata.ColumnNames.SRDosageUnit, esSystemType.String);
            }
        }

        public esQueryItem StartDate
        {
            get
            {
                return new esQueryItem(this, PpiAntimicrobialApplicationsMetadata.ColumnNames.StartDate, esSystemType.DateTime);
            }
        }

        public esQueryItem EndDate
        {
            get
            {
                return new esQueryItem(this, PpiAntimicrobialApplicationsMetadata.ColumnNames.EndDate, esSystemType.DateTime);
            }
        }

        public esQueryItem SRAntimicrobialApplicationTiming
        {
            get
            {
                return new esQueryItem(this, PpiAntimicrobialApplicationsMetadata.ColumnNames.SRAntimicrobialApplicationTiming, esSystemType.String);
            }
        }

        public esQueryItem CreatedDateTime
        {
            get
            {
                return new esQueryItem(this, PpiAntimicrobialApplicationsMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem CreatedByUserID
        {
            get
            {
                return new esQueryItem(this, PpiAntimicrobialApplicationsMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, PpiAntimicrobialApplicationsMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, PpiAntimicrobialApplicationsMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("PpiAntimicrobialApplicationsCollection")]
    public partial class PpiAntimicrobialApplicationsCollection : esPpiAntimicrobialApplicationsCollection, IEnumerable<PpiAntimicrobialApplications>
    {
        public PpiAntimicrobialApplicationsCollection()
        {

        }

        public static implicit operator List<PpiAntimicrobialApplications>(PpiAntimicrobialApplicationsCollection coll)
        {
            List<PpiAntimicrobialApplications> list = new List<PpiAntimicrobialApplications>();

            foreach (PpiAntimicrobialApplications emp in coll)
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
                return PpiAntimicrobialApplicationsMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PpiAntimicrobialApplicationsQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new PpiAntimicrobialApplications(row);
        }

        override protected esEntity CreateEntity()
        {
            return new PpiAntimicrobialApplications();
        }

        #endregion

        [BrowsableAttribute(false)]
        public PpiAntimicrobialApplicationsQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PpiAntimicrobialApplicationsQuery();
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
        public bool Load(PpiAntimicrobialApplicationsQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public PpiAntimicrobialApplications AddNew()
        {
            PpiAntimicrobialApplications entity = base.AddNewEntity() as PpiAntimicrobialApplications;

            return entity;
        }
        public PpiAntimicrobialApplications FindByPrimaryKey(String registrationNo, String sRTherapyGroup, String therapyID)
        {
            return base.FindByPrimaryKey(registrationNo, sRTherapyGroup, therapyID) as PpiAntimicrobialApplications;
        }

        #region IEnumerable< PpiAntimicrobialApplications> Members

        IEnumerator<PpiAntimicrobialApplications> IEnumerable<PpiAntimicrobialApplications>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as PpiAntimicrobialApplications;
            }
        }

        #endregion

        private PpiAntimicrobialApplicationsQuery query;
    }


    /// <summary>
    /// Encapsulates the 'PpiAntimicrobialApplications' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("PpiAntimicrobialApplications ({RegistrationNo, SRTherapyGroup, TherapyID})")]
    [Serializable]
    public partial class PpiAntimicrobialApplications : esPpiAntimicrobialApplications
    {
        public PpiAntimicrobialApplications()
        {
        }

        public PpiAntimicrobialApplications(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return PpiAntimicrobialApplicationsMetadata.Meta();
            }
        }

        override protected esPpiAntimicrobialApplicationsQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PpiAntimicrobialApplicationsQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public PpiAntimicrobialApplicationsQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PpiAntimicrobialApplicationsQuery();
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
        public bool Load(PpiAntimicrobialApplicationsQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private PpiAntimicrobialApplicationsQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class PpiAntimicrobialApplicationsQuery : esPpiAntimicrobialApplicationsQuery
    {
        public PpiAntimicrobialApplicationsQuery()
        {

        }

        public PpiAntimicrobialApplicationsQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "PpiAntimicrobialApplicationsQuery";
        }
    }

    [Serializable]
    public partial class PpiAntimicrobialApplicationsMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected PpiAntimicrobialApplicationsMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(PpiAntimicrobialApplicationsMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = PpiAntimicrobialApplicationsMetadata.PropertyNames.RegistrationNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(PpiAntimicrobialApplicationsMetadata.ColumnNames.SRTherapyGroup, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = PpiAntimicrobialApplicationsMetadata.PropertyNames.SRTherapyGroup;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(PpiAntimicrobialApplicationsMetadata.ColumnNames.TherapyID, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = PpiAntimicrobialApplicationsMetadata.PropertyNames.TherapyID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 15;
            _columns.Add(c);

            c = new esColumnMetadata(PpiAntimicrobialApplicationsMetadata.ColumnNames.Dosage, 3, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = PpiAntimicrobialApplicationsMetadata.PropertyNames.Dosage;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiAntimicrobialApplicationsMetadata.ColumnNames.SRDosageUnit, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = PpiAntimicrobialApplicationsMetadata.PropertyNames.SRDosageUnit;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiAntimicrobialApplicationsMetadata.ColumnNames.StartDate, 5, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PpiAntimicrobialApplicationsMetadata.PropertyNames.StartDate;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiAntimicrobialApplicationsMetadata.ColumnNames.EndDate, 6, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PpiAntimicrobialApplicationsMetadata.PropertyNames.EndDate;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiAntimicrobialApplicationsMetadata.ColumnNames.SRAntimicrobialApplicationTiming, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = PpiAntimicrobialApplicationsMetadata.PropertyNames.SRAntimicrobialApplicationTiming;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiAntimicrobialApplicationsMetadata.ColumnNames.CreatedDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PpiAntimicrobialApplicationsMetadata.PropertyNames.CreatedDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiAntimicrobialApplicationsMetadata.ColumnNames.CreatedByUserID, 9, typeof(System.String), esSystemType.String);
            c.PropertyName = PpiAntimicrobialApplicationsMetadata.PropertyNames.CreatedByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiAntimicrobialApplicationsMetadata.ColumnNames.LastUpdateDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PpiAntimicrobialApplicationsMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiAntimicrobialApplicationsMetadata.ColumnNames.LastUpdateByUserID, 11, typeof(System.String), esSystemType.String);
            c.PropertyName = PpiAntimicrobialApplicationsMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public PpiAntimicrobialApplicationsMetadata Meta()
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
            public const string SRTherapyGroup = "SRTherapyGroup";
            public const string TherapyID = "TherapyID";
            public const string Dosage = "Dosage";
            public const string SRDosageUnit = "SRDosageUnit";
            public const string StartDate = "StartDate";
            public const string EndDate = "EndDate";
            public const string SRAntimicrobialApplicationTiming = "SRAntimicrobialApplicationTiming";
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
            public const string SRTherapyGroup = "SRTherapyGroup";
            public const string TherapyID = "TherapyID";
            public const string Dosage = "Dosage";
            public const string SRDosageUnit = "SRDosageUnit";
            public const string StartDate = "StartDate";
            public const string EndDate = "EndDate";
            public const string SRAntimicrobialApplicationTiming = "SRAntimicrobialApplicationTiming";
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
            lock (typeof(PpiAntimicrobialApplicationsMetadata))
            {
                if (PpiAntimicrobialApplicationsMetadata.mapDelegates == null)
                {
                    PpiAntimicrobialApplicationsMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (PpiAntimicrobialApplicationsMetadata.meta == null)
                {
                    PpiAntimicrobialApplicationsMetadata.meta = new PpiAntimicrobialApplicationsMetadata();
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
                meta.AddTypeMap("SRTherapyGroup", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("TherapyID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Dosage", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("SRDosageUnit", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("StartDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("EndDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("SRAntimicrobialApplicationTiming", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "PpiAntimicrobialApplications";
                meta.Destination = "PpiAntimicrobialApplications";
                meta.spInsert = "proc_PpiAntimicrobialApplicationsInsert";
                meta.spUpdate = "proc_PpiAntimicrobialApplicationsUpdate";
                meta.spDelete = "proc_PpiAntimicrobialApplicationsDelete";
                meta.spLoadAll = "proc_PpiAntimicrobialApplicationsLoadAll";
                meta.spLoadByPrimaryKey = "proc_PpiAntimicrobialApplicationsLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private PpiAntimicrobialApplicationsMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
