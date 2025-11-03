/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 03/04/2024 22:29:39
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
    abstract public class esReferExternalCmxCollection : esEntityCollectionWAuditLog
    {
        public esReferExternalCmxCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "ReferExternalCmxCollection";
        }

        #region Query Logic
        protected void InitQuery(esReferExternalCmxQuery query)
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
            this.InitQuery(query as esReferExternalCmxQuery);
        }
        #endregion

        virtual public ReferExternalCmx DetachEntity(ReferExternalCmx entity)
        {
            return base.DetachEntity(entity) as ReferExternalCmx;
        }

        virtual public ReferExternalCmx AttachEntity(ReferExternalCmx entity)
        {
            return base.AttachEntity(entity) as ReferExternalCmx;
        }

        virtual public void Combine(ReferExternalCmxCollection collection)
        {
            base.Combine(collection);
        }

        new public ReferExternalCmx this[int index]
        {
            get
            {
                return base[index] as ReferExternalCmx;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(ReferExternalCmx);
        }
    }

    [Serializable]
    abstract public class esReferExternalCmx : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esReferExternalCmxQuery GetDynamicQuery()
        {
            return null;
        }

        public esReferExternalCmx()
        {
        }

        public esReferExternalCmx(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String registrationNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(registrationNo);
            else
                return LoadByPrimaryKeyStoredProcedure(registrationNo);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String registrationNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(registrationNo);
            else
                return LoadByPrimaryKeyStoredProcedure(registrationNo);
        }

        private bool LoadByPrimaryKeyDynamic(String registrationNo)
        {
            esReferExternalCmxQuery query = this.GetDynamicQuery();
            query.Where(query.RegistrationNo == registrationNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String registrationNo)
        {
            esParameters parms = new esParameters();
            parms.Add("RegistrationNo", registrationNo);
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
                        case "ReferralID": this.str.ReferralID = (string)value; break;
                        case "SRReferReason": this.str.SRReferReason = (string)value; break;
                        case "ReferReasonOther": this.str.ReferReasonOther = (string)value; break;
                        case "OtherInformation": this.str.OtherInformation = (string)value; break;
                        case "ReferralAgreedBy": this.str.ReferralAgreedBy = (string)value; break;
                        case "ReferralAgreedTime": this.str.ReferralAgreedTime = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "ContactOfficer": this.str.ContactOfficer = (string)value; break;
                        case "UnitOfficer": this.str.UnitOfficer = (string)value; break;
                        case "ContactTime": this.str.ContactTime = (string)value; break;
                        case "SRReferralServiceUnit": this.str.SRReferralServiceUnit = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "ReferralAgreedTime":

                            if (value == null || value is System.DateTime)
                                this.ReferralAgreedTime = (System.DateTime?)value;
                            break;
                        case "LastUpdateDateTime":

                            if (value == null || value is System.DateTime)
                                this.LastUpdateDateTime = (System.DateTime?)value;
                            break;
                        case "ContactTime":

                            if (value == null || value is System.DateTime)
                                this.ContactTime = (System.DateTime?)value;
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
        /// Maps to ReferExternalCmx.RegistrationNo
        /// </summary>
        virtual public System.String RegistrationNo
        {
            get
            {
                return base.GetSystemString(ReferExternalCmxMetadata.ColumnNames.RegistrationNo);
            }

            set
            {
                base.SetSystemString(ReferExternalCmxMetadata.ColumnNames.RegistrationNo, value);
            }
        }
        /// <summary>
        /// Maps to ReferExternalCmx.ReferralID
        /// </summary>
        virtual public System.String ReferralID
        {
            get
            {
                return base.GetSystemString(ReferExternalCmxMetadata.ColumnNames.ReferralID);
            }

            set
            {
                base.SetSystemString(ReferExternalCmxMetadata.ColumnNames.ReferralID, value);
            }
        }
        /// <summary>
        /// Maps to ReferExternalCmx.SRReferReason
        /// </summary>
        virtual public System.String SRReferReason
        {
            get
            {
                return base.GetSystemString(ReferExternalCmxMetadata.ColumnNames.SRReferReason);
            }

            set
            {
                base.SetSystemString(ReferExternalCmxMetadata.ColumnNames.SRReferReason, value);
            }
        }
        /// <summary>
        /// Maps to ReferExternalCmx.ReferReasonOther
        /// </summary>
        virtual public System.String ReferReasonOther
        {
            get
            {
                return base.GetSystemString(ReferExternalCmxMetadata.ColumnNames.ReferReasonOther);
            }

            set
            {
                base.SetSystemString(ReferExternalCmxMetadata.ColumnNames.ReferReasonOther, value);
            }
        }
        /// <summary>
        /// Maps to ReferExternalCmx.OtherInformation
        /// </summary>
        virtual public System.String OtherInformation
        {
            get
            {
                return base.GetSystemString(ReferExternalCmxMetadata.ColumnNames.OtherInformation);
            }

            set
            {
                base.SetSystemString(ReferExternalCmxMetadata.ColumnNames.OtherInformation, value);
            }
        }
        /// <summary>
        /// Maps to ReferExternalCmx.ReferralAgreedBy
        /// </summary>
        virtual public System.String ReferralAgreedBy
        {
            get
            {
                return base.GetSystemString(ReferExternalCmxMetadata.ColumnNames.ReferralAgreedBy);
            }

            set
            {
                base.SetSystemString(ReferExternalCmxMetadata.ColumnNames.ReferralAgreedBy, value);
            }
        }
        /// <summary>
        /// Maps to ReferExternalCmx.ReferralAgreedTime
        /// </summary>
        virtual public System.DateTime? ReferralAgreedTime
        {
            get
            {
                return base.GetSystemDateTime(ReferExternalCmxMetadata.ColumnNames.ReferralAgreedTime);
            }

            set
            {
                base.SetSystemDateTime(ReferExternalCmxMetadata.ColumnNames.ReferralAgreedTime, value);
            }
        }
        /// <summary>
        /// Maps to ReferExternalCmx.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(ReferExternalCmxMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(ReferExternalCmxMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to ReferExternalCmx.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(ReferExternalCmxMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(ReferExternalCmxMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to ReferExternalCmx.ContactOfficer
        /// </summary>
        virtual public System.String ContactOfficer
        {
            get
            {
                return base.GetSystemString(ReferExternalCmxMetadata.ColumnNames.ContactOfficer);
            }

            set
            {
                base.SetSystemString(ReferExternalCmxMetadata.ColumnNames.ContactOfficer, value);
            }
        }
        /// <summary>
        /// Maps to ReferExternalCmx.UnitOfficer
        /// </summary>
        virtual public System.String UnitOfficer
        {
            get
            {
                return base.GetSystemString(ReferExternalCmxMetadata.ColumnNames.UnitOfficer);
            }

            set
            {
                base.SetSystemString(ReferExternalCmxMetadata.ColumnNames.UnitOfficer, value);
            }
        }
        /// <summary>
        /// Maps to ReferExternalCmx.ContactTime
        /// </summary>
        virtual public System.DateTime? ContactTime
        {
            get
            {
                return base.GetSystemDateTime(ReferExternalCmxMetadata.ColumnNames.ContactTime);
            }

            set
            {
                base.SetSystemDateTime(ReferExternalCmxMetadata.ColumnNames.ContactTime, value);
            }
        }
        /// <summary>
        /// Maps to ReferExternalCmx.SRReferralServiceUnit
        /// </summary>
        virtual public System.String SRReferralServiceUnit
        {
            get
            {
                return base.GetSystemString(ReferExternalCmxMetadata.ColumnNames.SRReferralServiceUnit);
            }

            set
            {
                base.SetSystemString(ReferExternalCmxMetadata.ColumnNames.SRReferralServiceUnit, value);
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
            public esStrings(esReferExternalCmx entity)
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
            public System.String ReferralID
            {
                get
                {
                    System.String data = entity.ReferralID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ReferralID = null;
                    else entity.ReferralID = Convert.ToString(value);
                }
            }
            public System.String SRReferReason
            {
                get
                {
                    System.String data = entity.SRReferReason;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRReferReason = null;
                    else entity.SRReferReason = Convert.ToString(value);
                }
            }
            public System.String ReferReasonOther
            {
                get
                {
                    System.String data = entity.ReferReasonOther;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ReferReasonOther = null;
                    else entity.ReferReasonOther = Convert.ToString(value);
                }
            }
            public System.String OtherInformation
            {
                get
                {
                    System.String data = entity.OtherInformation;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.OtherInformation = null;
                    else entity.OtherInformation = Convert.ToString(value);
                }
            }
            public System.String ReferralAgreedBy
            {
                get
                {
                    System.String data = entity.ReferralAgreedBy;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ReferralAgreedBy = null;
                    else entity.ReferralAgreedBy = Convert.ToString(value);
                }
            }
            public System.String ReferralAgreedTime
            {
                get
                {
                    System.DateTime? data = entity.ReferralAgreedTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ReferralAgreedTime = null;
                    else entity.ReferralAgreedTime = Convert.ToDateTime(value);
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
            public System.String ContactOfficer
            {
                get
                {
                    System.String data = entity.ContactOfficer;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ContactOfficer = null;
                    else entity.ContactOfficer = Convert.ToString(value);
                }
            }
            public System.String UnitOfficer
            {
                get
                {
                    System.String data = entity.UnitOfficer;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.UnitOfficer = null;
                    else entity.UnitOfficer = Convert.ToString(value);
                }
            }
            public System.String ContactTime
            {
                get
                {
                    System.DateTime? data = entity.ContactTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ContactTime = null;
                    else entity.ContactTime = Convert.ToDateTime(value);
                }
            }
            public System.String SRReferralServiceUnit
            {
                get
                {
                    System.String data = entity.SRReferralServiceUnit;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRReferralServiceUnit = null;
                    else entity.SRReferralServiceUnit = Convert.ToString(value);
                }
            }
            private esReferExternalCmx entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esReferExternalCmxQuery query)
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
                throw new Exception("esReferExternalCmx can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class ReferExternalCmx : esReferExternalCmx
    {
    }

    [Serializable]
    abstract public class esReferExternalCmxQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return ReferExternalCmxMetadata.Meta();
            }
        }

        public esQueryItem RegistrationNo
        {
            get
            {
                return new esQueryItem(this, ReferExternalCmxMetadata.ColumnNames.RegistrationNo, esSystemType.String);
            }
        }

        public esQueryItem ReferralID
        {
            get
            {
                return new esQueryItem(this, ReferExternalCmxMetadata.ColumnNames.ReferralID, esSystemType.String);
            }
        }

        public esQueryItem SRReferReason
        {
            get
            {
                return new esQueryItem(this, ReferExternalCmxMetadata.ColumnNames.SRReferReason, esSystemType.String);
            }
        }

        public esQueryItem ReferReasonOther
        {
            get
            {
                return new esQueryItem(this, ReferExternalCmxMetadata.ColumnNames.ReferReasonOther, esSystemType.String);
            }
        }

        public esQueryItem OtherInformation
        {
            get
            {
                return new esQueryItem(this, ReferExternalCmxMetadata.ColumnNames.OtherInformation, esSystemType.String);
            }
        }

        public esQueryItem ReferralAgreedBy
        {
            get
            {
                return new esQueryItem(this, ReferExternalCmxMetadata.ColumnNames.ReferralAgreedBy, esSystemType.String);
            }
        }

        public esQueryItem ReferralAgreedTime
        {
            get
            {
                return new esQueryItem(this, ReferExternalCmxMetadata.ColumnNames.ReferralAgreedTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, ReferExternalCmxMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, ReferExternalCmxMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem ContactOfficer
        {
            get
            {
                return new esQueryItem(this, ReferExternalCmxMetadata.ColumnNames.ContactOfficer, esSystemType.String);
            }
        }

        public esQueryItem UnitOfficer
        {
            get
            {
                return new esQueryItem(this, ReferExternalCmxMetadata.ColumnNames.UnitOfficer, esSystemType.String);
            }
        }

        public esQueryItem ContactTime
        {
            get
            {
                return new esQueryItem(this, ReferExternalCmxMetadata.ColumnNames.ContactTime, esSystemType.DateTime);
            }
        }

        public esQueryItem SRReferralServiceUnit
        {
            get
            {
                return new esQueryItem(this, ReferExternalCmxMetadata.ColumnNames.SRReferralServiceUnit, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("ReferExternalCmxCollection")]
    public partial class ReferExternalCmxCollection : esReferExternalCmxCollection, IEnumerable<ReferExternalCmx>
    {
        public ReferExternalCmxCollection()
        {

        }

        public static implicit operator List<ReferExternalCmx>(ReferExternalCmxCollection coll)
        {
            List<ReferExternalCmx> list = new List<ReferExternalCmx>();

            foreach (ReferExternalCmx emp in coll)
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
                return ReferExternalCmxMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ReferExternalCmxQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new ReferExternalCmx(row);
        }

        override protected esEntity CreateEntity()
        {
            return new ReferExternalCmx();
        }

        #endregion

        [BrowsableAttribute(false)]
        public ReferExternalCmxQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ReferExternalCmxQuery();
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
        public bool Load(ReferExternalCmxQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public ReferExternalCmx AddNew()
        {
            ReferExternalCmx entity = base.AddNewEntity() as ReferExternalCmx;

            return entity;
        }
        public ReferExternalCmx FindByPrimaryKey(String registrationNo)
        {
            return base.FindByPrimaryKey(registrationNo) as ReferExternalCmx;
        }

        #region IEnumerable< ReferExternalCmx> Members

        IEnumerator<ReferExternalCmx> IEnumerable<ReferExternalCmx>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as ReferExternalCmx;
            }
        }

        #endregion

        private ReferExternalCmxQuery query;
    }


    /// <summary>
    /// Encapsulates the 'ReferExternalCmx' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("ReferExternalCmx ({RegistrationNo})")]
    [Serializable]
    public partial class ReferExternalCmx : esReferExternalCmx
    {
        public ReferExternalCmx()
        {
        }

        public ReferExternalCmx(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return ReferExternalCmxMetadata.Meta();
            }
        }

        override protected esReferExternalCmxQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ReferExternalCmxQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public ReferExternalCmxQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ReferExternalCmxQuery();
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
        public bool Load(ReferExternalCmxQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private ReferExternalCmxQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class ReferExternalCmxQuery : esReferExternalCmxQuery
    {
        public ReferExternalCmxQuery()
        {

        }

        public ReferExternalCmxQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "ReferExternalCmxQuery";
        }
    }

    [Serializable]
    public partial class ReferExternalCmxMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected ReferExternalCmxMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(ReferExternalCmxMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = ReferExternalCmxMetadata.PropertyNames.RegistrationNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(ReferExternalCmxMetadata.ColumnNames.ReferralID, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = ReferExternalCmxMetadata.PropertyNames.ReferralID;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ReferExternalCmxMetadata.ColumnNames.SRReferReason, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = ReferExternalCmxMetadata.PropertyNames.SRReferReason;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ReferExternalCmxMetadata.ColumnNames.ReferReasonOther, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = ReferExternalCmxMetadata.PropertyNames.ReferReasonOther;
            c.CharacterMaxLength = 500;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ReferExternalCmxMetadata.ColumnNames.OtherInformation, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = ReferExternalCmxMetadata.PropertyNames.OtherInformation;
            c.CharacterMaxLength = 1500;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ReferExternalCmxMetadata.ColumnNames.ReferralAgreedBy, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = ReferExternalCmxMetadata.PropertyNames.ReferralAgreedBy;
            c.CharacterMaxLength = 100;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ReferExternalCmxMetadata.ColumnNames.ReferralAgreedTime, 6, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ReferExternalCmxMetadata.PropertyNames.ReferralAgreedTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ReferExternalCmxMetadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ReferExternalCmxMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ReferExternalCmxMetadata.ColumnNames.LastUpdateByUserID, 8, typeof(System.String), esSystemType.String);
            c.PropertyName = ReferExternalCmxMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ReferExternalCmxMetadata.ColumnNames.ContactOfficer, 9, typeof(System.String), esSystemType.String);
            c.PropertyName = ReferExternalCmxMetadata.PropertyNames.ContactOfficer;
            c.CharacterMaxLength = 200;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ReferExternalCmxMetadata.ColumnNames.UnitOfficer, 10, typeof(System.String), esSystemType.String);
            c.PropertyName = ReferExternalCmxMetadata.PropertyNames.UnitOfficer;
            c.CharacterMaxLength = 200;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ReferExternalCmxMetadata.ColumnNames.ContactTime, 11, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ReferExternalCmxMetadata.PropertyNames.ContactTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ReferExternalCmxMetadata.ColumnNames.SRReferralServiceUnit, 12, typeof(System.String), esSystemType.String);
            c.PropertyName = ReferExternalCmxMetadata.PropertyNames.SRReferralServiceUnit;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public ReferExternalCmxMetadata Meta()
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
            public const string ReferralID = "ReferralID";
            public const string SRReferReason = "SRReferReason";
            public const string ReferReasonOther = "ReferReasonOther";
            public const string OtherInformation = "OtherInformation";
            public const string ReferralAgreedBy = "ReferralAgreedBy";
            public const string ReferralAgreedTime = "ReferralAgreedTime";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string ContactOfficer = "ContactOfficer";
            public const string UnitOfficer = "UnitOfficer";
            public const string ContactTime = "ContactTime";
            public const string SRReferralServiceUnit = "SRReferralServiceUnit";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string RegistrationNo = "RegistrationNo";
            public const string ReferralID = "ReferralID";
            public const string SRReferReason = "SRReferReason";
            public const string ReferReasonOther = "ReferReasonOther";
            public const string OtherInformation = "OtherInformation";
            public const string ReferralAgreedBy = "ReferralAgreedBy";
            public const string ReferralAgreedTime = "ReferralAgreedTime";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string ContactOfficer = "ContactOfficer";
            public const string UnitOfficer = "UnitOfficer";
            public const string ContactTime = "ContactTime";
            public const string SRReferralServiceUnit = "SRReferralServiceUnit";
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
            lock (typeof(ReferExternalCmxMetadata))
            {
                if (ReferExternalCmxMetadata.mapDelegates == null)
                {
                    ReferExternalCmxMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (ReferExternalCmxMetadata.meta == null)
                {
                    ReferExternalCmxMetadata.meta = new ReferExternalCmxMetadata();
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
                meta.AddTypeMap("ReferralID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRReferReason", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ReferReasonOther", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("OtherInformation", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ReferralAgreedBy", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ReferralAgreedTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ContactOfficer", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("UnitOfficer", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ContactTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("SRReferralServiceUnit", new esTypeMap("varchar", "System.String"));


                meta.Source = "ReferExternalCmx";
                meta.Destination = "ReferExternalCmx";
                meta.spInsert = "proc_ReferExternalCmxInsert";
                meta.spUpdate = "proc_ReferExternalCmxUpdate";
                meta.spDelete = "proc_ReferExternalCmxDelete";
                meta.spLoadAll = "proc_ReferExternalCmxLoadAll";
                meta.spLoadByPrimaryKey = "proc_ReferExternalCmxLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private ReferExternalCmxMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
