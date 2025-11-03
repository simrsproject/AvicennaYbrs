/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 10/6/2017 3:54:26 PM
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
    abstract public class esPpiRiskFactorsCollection : esEntityCollectionWAuditLog
    {
        public esPpiRiskFactorsCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "PpiRiskFactorsCollection";
        }

        #region Query Logic
        protected void InitQuery(esPpiRiskFactorsQuery query)
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
            this.InitQuery(query as esPpiRiskFactorsQuery);
        }
        #endregion

        virtual public PpiRiskFactors DetachEntity(PpiRiskFactors entity)
        {
            return base.DetachEntity(entity) as PpiRiskFactors;
        }

        virtual public PpiRiskFactors AttachEntity(PpiRiskFactors entity)
        {
            return base.AttachEntity(entity) as PpiRiskFactors;
        }

        virtual public void Combine(PpiRiskFactorsCollection collection)
        {
            base.Combine(collection);
        }

        new public PpiRiskFactors this[int index]
        {
            get
            {
                return base[index] as PpiRiskFactors;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(PpiRiskFactors);
        }
    }

    [Serializable]
    abstract public class esPpiRiskFactors : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esPpiRiskFactorsQuery GetDynamicQuery()
        {
            return null;
        }

        public esPpiRiskFactors()
        {
        }

        public esPpiRiskFactors(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String registrationNo, String sequenceNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(registrationNo, sequenceNo);
            else
                return LoadByPrimaryKeyStoredProcedure(registrationNo, sequenceNo);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String registrationNo, String sequenceNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(registrationNo, sequenceNo);
            else
                return LoadByPrimaryKeyStoredProcedure(registrationNo, sequenceNo);
        }

        private bool LoadByPrimaryKeyDynamic(String registrationNo, String sequenceNo)
        {
            esPpiRiskFactorsQuery query = this.GetDynamicQuery();
            query.Where(query.RegistrationNo == registrationNo, query.SequenceNo == sequenceNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String registrationNo, String sequenceNo)
        {
            esParameters parms = new esParameters();
            parms.Add("RegistrationNo", registrationNo);
            parms.Add("SequenceNo", sequenceNo);
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
                        case "SequenceNo": this.str.SequenceNo = (string)value; break;
                        case "SRRiskFactorsType": this.str.SRRiskFactorsType = (string)value; break;
                        case "RiskFactorsID": this.str.RiskFactorsID = (string)value; break;
                        case "SRRiskFactorsLocation": this.str.SRRiskFactorsLocation = (string)value; break;
                        case "DateOfInitialInstallation": this.str.DateOfInitialInstallation = (string)value; break;
                        case "DateOfFinalInstallation": this.str.DateOfFinalInstallation = (string)value; break;
                        case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;
                        case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;
                        case "IsVoid": this.str.IsVoid = (string)value; break;
                        case "VoidDateTime": this.str.VoidDateTime = (string)value; break;
                        case "VoidByUserID": this.str.VoidByUserID = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "DateOfInitialInstallation":

                            if (value == null || value is System.DateTime)
                                this.DateOfInitialInstallation = (System.DateTime?)value;
                            break;
                        case "DateOfFinalInstallation":

                            if (value == null || value is System.DateTime)
                                this.DateOfFinalInstallation = (System.DateTime?)value;
                            break;
                        case "CreatedDateTime":

                            if (value == null || value is System.DateTime)
                                this.CreatedDateTime = (System.DateTime?)value;
                            break;
                        case "IsVoid":

                            if (value == null || value is System.Boolean)
                                this.IsVoid = (System.Boolean?)value;
                            break;
                        case "VoidDateTime":

                            if (value == null || value is System.DateTime)
                                this.VoidDateTime = (System.DateTime?)value;
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
        /// Maps to PpiRiskFactors.RegistrationNo
        /// </summary>
        virtual public System.String RegistrationNo
        {
            get
            {
                return base.GetSystemString(PpiRiskFactorsMetadata.ColumnNames.RegistrationNo);
            }

            set
            {
                base.SetSystemString(PpiRiskFactorsMetadata.ColumnNames.RegistrationNo, value);
            }
        }
        /// <summary>
        /// Maps to PpiRiskFactors.SequenceNo
        /// </summary>
        virtual public System.String SequenceNo
        {
            get
            {
                return base.GetSystemString(PpiRiskFactorsMetadata.ColumnNames.SequenceNo);
            }

            set
            {
                base.SetSystemString(PpiRiskFactorsMetadata.ColumnNames.SequenceNo, value);
            }
        }
        /// <summary>
        /// Maps to PpiRiskFactors.SRRiskFactorsType
        /// </summary>
        virtual public System.String SRRiskFactorsType
        {
            get
            {
                return base.GetSystemString(PpiRiskFactorsMetadata.ColumnNames.SRRiskFactorsType);
            }

            set
            {
                base.SetSystemString(PpiRiskFactorsMetadata.ColumnNames.SRRiskFactorsType, value);
            }
        }
        /// <summary>
        /// Maps to PpiRiskFactors.RiskFactorsID
        /// </summary>
        virtual public System.String RiskFactorsID
        {
            get
            {
                return base.GetSystemString(PpiRiskFactorsMetadata.ColumnNames.RiskFactorsID);
            }

            set
            {
                base.SetSystemString(PpiRiskFactorsMetadata.ColumnNames.RiskFactorsID, value);
            }
        }
        /// <summary>
        /// Maps to PpiRiskFactors.SRRiskFactorsLocation
        /// </summary>
        virtual public System.String SRRiskFactorsLocation
        {
            get
            {
                return base.GetSystemString(PpiRiskFactorsMetadata.ColumnNames.SRRiskFactorsLocation);
            }

            set
            {
                base.SetSystemString(PpiRiskFactorsMetadata.ColumnNames.SRRiskFactorsLocation, value);
            }
        }
        /// <summary>
        /// Maps to PpiRiskFactors.DateOfInitialInstallation
        /// </summary>
        virtual public System.DateTime? DateOfInitialInstallation
        {
            get
            {
                return base.GetSystemDateTime(PpiRiskFactorsMetadata.ColumnNames.DateOfInitialInstallation);
            }

            set
            {
                base.SetSystemDateTime(PpiRiskFactorsMetadata.ColumnNames.DateOfInitialInstallation, value);
            }
        }
        /// <summary>
        /// Maps to PpiRiskFactors.DateOfFinalInstallation
        /// </summary>
        virtual public System.DateTime? DateOfFinalInstallation
        {
            get
            {
                return base.GetSystemDateTime(PpiRiskFactorsMetadata.ColumnNames.DateOfFinalInstallation);
            }

            set
            {
                base.SetSystemDateTime(PpiRiskFactorsMetadata.ColumnNames.DateOfFinalInstallation, value);
            }
        }
        /// <summary>
        /// Maps to PpiRiskFactors.CreatedDateTime
        /// </summary>
        virtual public System.DateTime? CreatedDateTime
        {
            get
            {
                return base.GetSystemDateTime(PpiRiskFactorsMetadata.ColumnNames.CreatedDateTime);
            }

            set
            {
                base.SetSystemDateTime(PpiRiskFactorsMetadata.ColumnNames.CreatedDateTime, value);
            }
        }
        /// <summary>
        /// Maps to PpiRiskFactors.CreatedByUserID
        /// </summary>
        virtual public System.String CreatedByUserID
        {
            get
            {
                return base.GetSystemString(PpiRiskFactorsMetadata.ColumnNames.CreatedByUserID);
            }

            set
            {
                base.SetSystemString(PpiRiskFactorsMetadata.ColumnNames.CreatedByUserID, value);
            }
        }
        /// <summary>
        /// Maps to PpiRiskFactors.IsVoid
        /// </summary>
        virtual public System.Boolean? IsVoid
        {
            get
            {
                return base.GetSystemBoolean(PpiRiskFactorsMetadata.ColumnNames.IsVoid);
            }

            set
            {
                base.SetSystemBoolean(PpiRiskFactorsMetadata.ColumnNames.IsVoid, value);
            }
        }
        /// <summary>
        /// Maps to PpiRiskFactors.VoidDateTime
        /// </summary>
        virtual public System.DateTime? VoidDateTime
        {
            get
            {
                return base.GetSystemDateTime(PpiRiskFactorsMetadata.ColumnNames.VoidDateTime);
            }

            set
            {
                base.SetSystemDateTime(PpiRiskFactorsMetadata.ColumnNames.VoidDateTime, value);
            }
        }
        /// <summary>
        /// Maps to PpiRiskFactors.VoidByUserID
        /// </summary>
        virtual public System.String VoidByUserID
        {
            get
            {
                return base.GetSystemString(PpiRiskFactorsMetadata.ColumnNames.VoidByUserID);
            }

            set
            {
                base.SetSystemString(PpiRiskFactorsMetadata.ColumnNames.VoidByUserID, value);
            }
        }
        /// <summary>
        /// Maps to PpiRiskFactors.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(PpiRiskFactorsMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(PpiRiskFactorsMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to PpiRiskFactors.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(PpiRiskFactorsMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(PpiRiskFactorsMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esPpiRiskFactors entity)
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
            public System.String SequenceNo
            {
                get
                {
                    System.String data = entity.SequenceNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SequenceNo = null;
                    else entity.SequenceNo = Convert.ToString(value);
                }
            }
            public System.String SRRiskFactorsType
            {
                get
                {
                    System.String data = entity.SRRiskFactorsType;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRRiskFactorsType = null;
                    else entity.SRRiskFactorsType = Convert.ToString(value);
                }
            }
            public System.String RiskFactorsID
            {
                get
                {
                    System.String data = entity.RiskFactorsID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RiskFactorsID = null;
                    else entity.RiskFactorsID = Convert.ToString(value);
                }
            }
            public System.String SRRiskFactorsLocation
            {
                get
                {
                    System.String data = entity.SRRiskFactorsLocation;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRRiskFactorsLocation = null;
                    else entity.SRRiskFactorsLocation = Convert.ToString(value);
                }
            }
            public System.String DateOfInitialInstallation
            {
                get
                {
                    System.DateTime? data = entity.DateOfInitialInstallation;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DateOfInitialInstallation = null;
                    else entity.DateOfInitialInstallation = Convert.ToDateTime(value);
                }
            }
            public System.String DateOfFinalInstallation
            {
                get
                {
                    System.DateTime? data = entity.DateOfFinalInstallation;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DateOfFinalInstallation = null;
                    else entity.DateOfFinalInstallation = Convert.ToDateTime(value);
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
            public System.String IsVoid
            {
                get
                {
                    System.Boolean? data = entity.IsVoid;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsVoid = null;
                    else entity.IsVoid = Convert.ToBoolean(value);
                }
            }
            public System.String VoidDateTime
            {
                get
                {
                    System.DateTime? data = entity.VoidDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.VoidDateTime = null;
                    else entity.VoidDateTime = Convert.ToDateTime(value);
                }
            }
            public System.String VoidByUserID
            {
                get
                {
                    System.String data = entity.VoidByUserID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.VoidByUserID = null;
                    else entity.VoidByUserID = Convert.ToString(value);
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
            private esPpiRiskFactors entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esPpiRiskFactorsQuery query)
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
                throw new Exception("esPpiRiskFactors can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class PpiRiskFactors : esPpiRiskFactors
    {
    }

    [Serializable]
    abstract public class esPpiRiskFactorsQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return PpiRiskFactorsMetadata.Meta();
            }
        }

        public esQueryItem RegistrationNo
        {
            get
            {
                return new esQueryItem(this, PpiRiskFactorsMetadata.ColumnNames.RegistrationNo, esSystemType.String);
            }
        }

        public esQueryItem SequenceNo
        {
            get
            {
                return new esQueryItem(this, PpiRiskFactorsMetadata.ColumnNames.SequenceNo, esSystemType.String);
            }
        }

        public esQueryItem SRRiskFactorsType
        {
            get
            {
                return new esQueryItem(this, PpiRiskFactorsMetadata.ColumnNames.SRRiskFactorsType, esSystemType.String);
            }
        }

        public esQueryItem RiskFactorsID
        {
            get
            {
                return new esQueryItem(this, PpiRiskFactorsMetadata.ColumnNames.RiskFactorsID, esSystemType.String);
            }
        }

        public esQueryItem SRRiskFactorsLocation
        {
            get
            {
                return new esQueryItem(this, PpiRiskFactorsMetadata.ColumnNames.SRRiskFactorsLocation, esSystemType.String);
            }
        }

        public esQueryItem DateOfInitialInstallation
        {
            get
            {
                return new esQueryItem(this, PpiRiskFactorsMetadata.ColumnNames.DateOfInitialInstallation, esSystemType.DateTime);
            }
        }

        public esQueryItem DateOfFinalInstallation
        {
            get
            {
                return new esQueryItem(this, PpiRiskFactorsMetadata.ColumnNames.DateOfFinalInstallation, esSystemType.DateTime);
            }
        }

        public esQueryItem CreatedDateTime
        {
            get
            {
                return new esQueryItem(this, PpiRiskFactorsMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem CreatedByUserID
        {
            get
            {
                return new esQueryItem(this, PpiRiskFactorsMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
            }
        }

        public esQueryItem IsVoid
        {
            get
            {
                return new esQueryItem(this, PpiRiskFactorsMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
            }
        }

        public esQueryItem VoidDateTime
        {
            get
            {
                return new esQueryItem(this, PpiRiskFactorsMetadata.ColumnNames.VoidDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem VoidByUserID
        {
            get
            {
                return new esQueryItem(this, PpiRiskFactorsMetadata.ColumnNames.VoidByUserID, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, PpiRiskFactorsMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, PpiRiskFactorsMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("PpiRiskFactorsCollection")]
    public partial class PpiRiskFactorsCollection : esPpiRiskFactorsCollection, IEnumerable<PpiRiskFactors>
    {
        public PpiRiskFactorsCollection()
        {

        }

        public static implicit operator List<PpiRiskFactors>(PpiRiskFactorsCollection coll)
        {
            List<PpiRiskFactors> list = new List<PpiRiskFactors>();

            foreach (PpiRiskFactors emp in coll)
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
                return PpiRiskFactorsMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PpiRiskFactorsQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new PpiRiskFactors(row);
        }

        override protected esEntity CreateEntity()
        {
            return new PpiRiskFactors();
        }

        #endregion

        [BrowsableAttribute(false)]
        public PpiRiskFactorsQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PpiRiskFactorsQuery();
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
        public bool Load(PpiRiskFactorsQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public PpiRiskFactors AddNew()
        {
            PpiRiskFactors entity = base.AddNewEntity() as PpiRiskFactors;

            return entity;
        }
        public PpiRiskFactors FindByPrimaryKey(String registrationNo, String sequenceNo)
        {
            return base.FindByPrimaryKey(registrationNo, sequenceNo) as PpiRiskFactors;
        }

        #region IEnumerable< PpiRiskFactors> Members

        IEnumerator<PpiRiskFactors> IEnumerable<PpiRiskFactors>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as PpiRiskFactors;
            }
        }

        #endregion

        private PpiRiskFactorsQuery query;
    }


    /// <summary>
    /// Encapsulates the 'PpiRiskFactors' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("PpiRiskFactors ({RegistrationNo, SequenceNo})")]
    [Serializable]
    public partial class PpiRiskFactors : esPpiRiskFactors
    {
        public PpiRiskFactors()
        {
        }

        public PpiRiskFactors(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return PpiRiskFactorsMetadata.Meta();
            }
        }

        override protected esPpiRiskFactorsQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PpiRiskFactorsQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public PpiRiskFactorsQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PpiRiskFactorsQuery();
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
        public bool Load(PpiRiskFactorsQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private PpiRiskFactorsQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class PpiRiskFactorsQuery : esPpiRiskFactorsQuery
    {
        public PpiRiskFactorsQuery()
        {

        }

        public PpiRiskFactorsQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "PpiRiskFactorsQuery";
        }
    }

    [Serializable]
    public partial class PpiRiskFactorsMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected PpiRiskFactorsMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(PpiRiskFactorsMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = PpiRiskFactorsMetadata.PropertyNames.RegistrationNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(PpiRiskFactorsMetadata.ColumnNames.SequenceNo, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = PpiRiskFactorsMetadata.PropertyNames.SequenceNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(PpiRiskFactorsMetadata.ColumnNames.SRRiskFactorsType, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = PpiRiskFactorsMetadata.PropertyNames.SRRiskFactorsType;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(PpiRiskFactorsMetadata.ColumnNames.RiskFactorsID, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = PpiRiskFactorsMetadata.PropertyNames.RiskFactorsID;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(PpiRiskFactorsMetadata.ColumnNames.SRRiskFactorsLocation, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = PpiRiskFactorsMetadata.PropertyNames.SRRiskFactorsLocation;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiRiskFactorsMetadata.ColumnNames.DateOfInitialInstallation, 5, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PpiRiskFactorsMetadata.PropertyNames.DateOfInitialInstallation;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiRiskFactorsMetadata.ColumnNames.DateOfFinalInstallation, 6, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PpiRiskFactorsMetadata.PropertyNames.DateOfFinalInstallation;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiRiskFactorsMetadata.ColumnNames.CreatedDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PpiRiskFactorsMetadata.PropertyNames.CreatedDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiRiskFactorsMetadata.ColumnNames.CreatedByUserID, 8, typeof(System.String), esSystemType.String);
            c.PropertyName = PpiRiskFactorsMetadata.PropertyNames.CreatedByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiRiskFactorsMetadata.ColumnNames.IsVoid, 9, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = PpiRiskFactorsMetadata.PropertyNames.IsVoid;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiRiskFactorsMetadata.ColumnNames.VoidDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PpiRiskFactorsMetadata.PropertyNames.VoidDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiRiskFactorsMetadata.ColumnNames.VoidByUserID, 11, typeof(System.String), esSystemType.String);
            c.PropertyName = PpiRiskFactorsMetadata.PropertyNames.VoidByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiRiskFactorsMetadata.ColumnNames.LastUpdateDateTime, 12, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PpiRiskFactorsMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiRiskFactorsMetadata.ColumnNames.LastUpdateByUserID, 13, typeof(System.String), esSystemType.String);
            c.PropertyName = PpiRiskFactorsMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public PpiRiskFactorsMetadata Meta()
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
            public const string SequenceNo = "SequenceNo";
            public const string SRRiskFactorsType = "SRRiskFactorsType";
            public const string RiskFactorsID = "RiskFactorsID";
            public const string SRRiskFactorsLocation = "SRRiskFactorsLocation";
            public const string DateOfInitialInstallation = "DateOfInitialInstallation";
            public const string DateOfFinalInstallation = "DateOfFinalInstallation";
            public const string CreatedDateTime = "CreatedDateTime";
            public const string CreatedByUserID = "CreatedByUserID";
            public const string IsVoid = "IsVoid";
            public const string VoidDateTime = "VoidDateTime";
            public const string VoidByUserID = "VoidByUserID";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string RegistrationNo = "RegistrationNo";
            public const string SequenceNo = "SequenceNo";
            public const string SRRiskFactorsType = "SRRiskFactorsType";
            public const string RiskFactorsID = "RiskFactorsID";
            public const string SRRiskFactorsLocation = "SRRiskFactorsLocation";
            public const string DateOfInitialInstallation = "DateOfInitialInstallation";
            public const string DateOfFinalInstallation = "DateOfFinalInstallation";
            public const string CreatedDateTime = "CreatedDateTime";
            public const string CreatedByUserID = "CreatedByUserID";
            public const string IsVoid = "IsVoid";
            public const string VoidDateTime = "VoidDateTime";
            public const string VoidByUserID = "VoidByUserID";
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
            lock (typeof(PpiRiskFactorsMetadata))
            {
                if (PpiRiskFactorsMetadata.mapDelegates == null)
                {
                    PpiRiskFactorsMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (PpiRiskFactorsMetadata.meta == null)
                {
                    PpiRiskFactorsMetadata.meta = new PpiRiskFactorsMetadata();
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
                meta.AddTypeMap("SequenceNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRRiskFactorsType", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("RiskFactorsID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRRiskFactorsLocation", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("DateOfInitialInstallation", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("DateOfFinalInstallation", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("VoidDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("VoidByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "PpiRiskFactors";
                meta.Destination = "PpiRiskFactors";
                meta.spInsert = "proc_PpiRiskFactorsInsert";
                meta.spUpdate = "proc_PpiRiskFactorsUpdate";
                meta.spDelete = "proc_PpiRiskFactorsDelete";
                meta.spLoadAll = "proc_PpiRiskFactorsLoadAll";
                meta.spLoadByPrimaryKey = "proc_PpiRiskFactorsLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private PpiRiskFactorsMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
