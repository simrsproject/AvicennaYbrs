/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 1/23/2020 4:58:43 PM
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
    abstract public class esMealOrderNonPatientCollection : esEntityCollectionWAuditLog
    {
        public esMealOrderNonPatientCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "MealOrderNonPatientCollection";
        }

        #region Query Logic
        protected void InitQuery(esMealOrderNonPatientQuery query)
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
            this.InitQuery(query as esMealOrderNonPatientQuery);
        }
        #endregion

        virtual public MealOrderNonPatient DetachEntity(MealOrderNonPatient entity)
        {
            return base.DetachEntity(entity) as MealOrderNonPatient;
        }

        virtual public MealOrderNonPatient AttachEntity(MealOrderNonPatient entity)
        {
            return base.AttachEntity(entity) as MealOrderNonPatient;
        }

        virtual public void Combine(MealOrderNonPatientCollection collection)
        {
            base.Combine(collection);
        }

        new public MealOrderNonPatient this[int index]
        {
            get
            {
                return base[index] as MealOrderNonPatient;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(MealOrderNonPatient);
        }
    }

    [Serializable]
    abstract public class esMealOrderNonPatient : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esMealOrderNonPatientQuery GetDynamicQuery()
        {
            return null;
        }

        public esMealOrderNonPatient()
        {
        }

        public esMealOrderNonPatient(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String transactionNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(transactionNo);
            else
                return LoadByPrimaryKeyStoredProcedure(transactionNo);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String transactionNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(transactionNo);
            else
                return LoadByPrimaryKeyStoredProcedure(transactionNo);
        }

        private bool LoadByPrimaryKeyDynamic(String transactionNo)
        {
            esMealOrderNonPatientQuery query = this.GetDynamicQuery();
            query.Where(query.TransactionNo == transactionNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String transactionNo)
        {
            esParameters parms = new esParameters();
            parms.Add("TransactionNo", transactionNo);
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
                        case "TransactionNo": this.str.TransactionNo = (string)value; break;
                        case "TransactionDate": this.str.TransactionDate = (string)value; break;
                        case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
                        case "IsApproved": this.str.IsApproved = (string)value; break;
                        case "ApprovedDateTime": this.str.ApprovedDateTime = (string)value; break;
                        case "ApprovedByUserID": this.str.ApprovedByUserID = (string)value; break;
                        case "IsVoid": this.str.IsVoid = (string)value; break;
                        case "IsDistributed": this.str.IsDistributed = (string)value; break;
                        case "DistributedDateTime": this.str.DistributedDateTime = (string)value; break;
                        case "DistributedByUserID": this.str.DistributedByUserID = (string)value; break;
                        case "LastCreateDateTime": this.str.LastCreateDateTime = (string)value; break;
                        case "LastCreateByUserID": this.str.LastCreateByUserID = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "TransactionDate":

                            if (value == null || value is System.DateTime)
                                this.TransactionDate = (System.DateTime?)value;
                            break;
                        case "IsApproved":

                            if (value == null || value is System.Boolean)
                                this.IsApproved = (System.Boolean?)value;
                            break;
                        case "ApprovedDateTime":

                            if (value == null || value is System.DateTime)
                                this.ApprovedDateTime = (System.DateTime?)value;
                            break;
                        case "IsVoid":

                            if (value == null || value is System.Boolean)
                                this.IsVoid = (System.Boolean?)value;
                            break;
                        case "IsDistributed":

                            if (value == null || value is System.Boolean)
                                this.IsDistributed = (System.Boolean?)value;
                            break;
                        case "DistributedDateTime":

                            if (value == null || value is System.DateTime)
                                this.DistributedDateTime = (System.DateTime?)value;
                            break;
                        case "LastCreateDateTime":

                            if (value == null || value is System.DateTime)
                                this.LastCreateDateTime = (System.DateTime?)value;
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
        /// Maps to MealOrderNonPatient.TransactionNo
        /// </summary>
        virtual public System.String TransactionNo
        {
            get
            {
                return base.GetSystemString(MealOrderNonPatientMetadata.ColumnNames.TransactionNo);
            }

            set
            {
                base.SetSystemString(MealOrderNonPatientMetadata.ColumnNames.TransactionNo, value);
            }
        }
        /// <summary>
        /// Maps to MealOrderNonPatient.TransactionDate
        /// </summary>
        virtual public System.DateTime? TransactionDate
        {
            get
            {
                return base.GetSystemDateTime(MealOrderNonPatientMetadata.ColumnNames.TransactionDate);
            }

            set
            {
                base.SetSystemDateTime(MealOrderNonPatientMetadata.ColumnNames.TransactionDate, value);
            }
        }
        /// <summary>
        /// Maps to MealOrderNonPatient.RegistrationNo
        /// </summary>
        virtual public System.String RegistrationNo
        {
            get
            {
                return base.GetSystemString(MealOrderNonPatientMetadata.ColumnNames.RegistrationNo);
            }

            set
            {
                base.SetSystemString(MealOrderNonPatientMetadata.ColumnNames.RegistrationNo, value);
            }
        }
        /// <summary>
        /// Maps to MealOrderNonPatient.IsApproved
        /// </summary>
        virtual public System.Boolean? IsApproved
        {
            get
            {
                return base.GetSystemBoolean(MealOrderNonPatientMetadata.ColumnNames.IsApproved);
            }

            set
            {
                base.SetSystemBoolean(MealOrderNonPatientMetadata.ColumnNames.IsApproved, value);
            }
        }
        /// <summary>
        /// Maps to MealOrderNonPatient.ApprovedDateTime
        /// </summary>
        virtual public System.DateTime? ApprovedDateTime
        {
            get
            {
                return base.GetSystemDateTime(MealOrderNonPatientMetadata.ColumnNames.ApprovedDateTime);
            }

            set
            {
                base.SetSystemDateTime(MealOrderNonPatientMetadata.ColumnNames.ApprovedDateTime, value);
            }
        }
        /// <summary>
        /// Maps to MealOrderNonPatient.ApprovedByUserID
        /// </summary>
        virtual public System.String ApprovedByUserID
        {
            get
            {
                return base.GetSystemString(MealOrderNonPatientMetadata.ColumnNames.ApprovedByUserID);
            }

            set
            {
                base.SetSystemString(MealOrderNonPatientMetadata.ColumnNames.ApprovedByUserID, value);
            }
        }
        /// <summary>
        /// Maps to MealOrderNonPatient.IsVoid
        /// </summary>
        virtual public System.Boolean? IsVoid
        {
            get
            {
                return base.GetSystemBoolean(MealOrderNonPatientMetadata.ColumnNames.IsVoid);
            }

            set
            {
                base.SetSystemBoolean(MealOrderNonPatientMetadata.ColumnNames.IsVoid, value);
            }
        }
        /// <summary>
        /// Maps to MealOrderNonPatient.IsDistributed
        /// </summary>
        virtual public System.Boolean? IsDistributed
        {
            get
            {
                return base.GetSystemBoolean(MealOrderNonPatientMetadata.ColumnNames.IsDistributed);
            }

            set
            {
                base.SetSystemBoolean(MealOrderNonPatientMetadata.ColumnNames.IsDistributed, value);
            }
        }
        /// <summary>
        /// Maps to MealOrderNonPatient.DistributedDateTime
        /// </summary>
        virtual public System.DateTime? DistributedDateTime
        {
            get
            {
                return base.GetSystemDateTime(MealOrderNonPatientMetadata.ColumnNames.DistributedDateTime);
            }

            set
            {
                base.SetSystemDateTime(MealOrderNonPatientMetadata.ColumnNames.DistributedDateTime, value);
            }
        }
        /// <summary>
        /// Maps to MealOrderNonPatient.DistributedByUserID
        /// </summary>
        virtual public System.String DistributedByUserID
        {
            get
            {
                return base.GetSystemString(MealOrderNonPatientMetadata.ColumnNames.DistributedByUserID);
            }

            set
            {
                base.SetSystemString(MealOrderNonPatientMetadata.ColumnNames.DistributedByUserID, value);
            }
        }
        /// <summary>
        /// Maps to MealOrderNonPatient.LastCreateDateTime
        /// </summary>
        virtual public System.DateTime? LastCreateDateTime
        {
            get
            {
                return base.GetSystemDateTime(MealOrderNonPatientMetadata.ColumnNames.LastCreateDateTime);
            }

            set
            {
                base.SetSystemDateTime(MealOrderNonPatientMetadata.ColumnNames.LastCreateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to MealOrderNonPatient.LastCreateByUserID
        /// </summary>
        virtual public System.String LastCreateByUserID
        {
            get
            {
                return base.GetSystemString(MealOrderNonPatientMetadata.ColumnNames.LastCreateByUserID);
            }

            set
            {
                base.SetSystemString(MealOrderNonPatientMetadata.ColumnNames.LastCreateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to MealOrderNonPatient.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(MealOrderNonPatientMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(MealOrderNonPatientMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to MealOrderNonPatient.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(MealOrderNonPatientMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(MealOrderNonPatientMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esMealOrderNonPatient entity)
            {
                this.entity = entity;
            }
            public System.String TransactionNo
            {
                get
                {
                    System.String data = entity.TransactionNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TransactionNo = null;
                    else entity.TransactionNo = Convert.ToString(value);
                }
            }
            public System.String TransactionDate
            {
                get
                {
                    System.DateTime? data = entity.TransactionDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TransactionDate = null;
                    else entity.TransactionDate = Convert.ToDateTime(value);
                }
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
            public System.String IsApproved
            {
                get
                {
                    System.Boolean? data = entity.IsApproved;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsApproved = null;
                    else entity.IsApproved = Convert.ToBoolean(value);
                }
            }
            public System.String ApprovedDateTime
            {
                get
                {
                    System.DateTime? data = entity.ApprovedDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ApprovedDateTime = null;
                    else entity.ApprovedDateTime = Convert.ToDateTime(value);
                }
            }
            public System.String ApprovedByUserID
            {
                get
                {
                    System.String data = entity.ApprovedByUserID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ApprovedByUserID = null;
                    else entity.ApprovedByUserID = Convert.ToString(value);
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
            public System.String IsDistributed
            {
                get
                {
                    System.Boolean? data = entity.IsDistributed;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsDistributed = null;
                    else entity.IsDistributed = Convert.ToBoolean(value);
                }
            }
            public System.String DistributedDateTime
            {
                get
                {
                    System.DateTime? data = entity.DistributedDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DistributedDateTime = null;
                    else entity.DistributedDateTime = Convert.ToDateTime(value);
                }
            }
            public System.String DistributedByUserID
            {
                get
                {
                    System.String data = entity.DistributedByUserID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DistributedByUserID = null;
                    else entity.DistributedByUserID = Convert.ToString(value);
                }
            }
            public System.String LastCreateDateTime
            {
                get
                {
                    System.DateTime? data = entity.LastCreateDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.LastCreateDateTime = null;
                    else entity.LastCreateDateTime = Convert.ToDateTime(value);
                }
            }
            public System.String LastCreateByUserID
            {
                get
                {
                    System.String data = entity.LastCreateByUserID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.LastCreateByUserID = null;
                    else entity.LastCreateByUserID = Convert.ToString(value);
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
            private esMealOrderNonPatient entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esMealOrderNonPatientQuery query)
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
                throw new Exception("esMealOrderNonPatient can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class MealOrderNonPatient : esMealOrderNonPatient
    {
    }

    [Serializable]
    abstract public class esMealOrderNonPatientQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return MealOrderNonPatientMetadata.Meta();
            }
        }

        public esQueryItem TransactionNo
        {
            get
            {
                return new esQueryItem(this, MealOrderNonPatientMetadata.ColumnNames.TransactionNo, esSystemType.String);
            }
        }

        public esQueryItem TransactionDate
        {
            get
            {
                return new esQueryItem(this, MealOrderNonPatientMetadata.ColumnNames.TransactionDate, esSystemType.DateTime);
            }
        }

        public esQueryItem RegistrationNo
        {
            get
            {
                return new esQueryItem(this, MealOrderNonPatientMetadata.ColumnNames.RegistrationNo, esSystemType.String);
            }
        }

        public esQueryItem IsApproved
        {
            get
            {
                return new esQueryItem(this, MealOrderNonPatientMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
            }
        }

        public esQueryItem ApprovedDateTime
        {
            get
            {
                return new esQueryItem(this, MealOrderNonPatientMetadata.ColumnNames.ApprovedDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem ApprovedByUserID
        {
            get
            {
                return new esQueryItem(this, MealOrderNonPatientMetadata.ColumnNames.ApprovedByUserID, esSystemType.String);
            }
        }

        public esQueryItem IsVoid
        {
            get
            {
                return new esQueryItem(this, MealOrderNonPatientMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
            }
        }

        public esQueryItem IsDistributed
        {
            get
            {
                return new esQueryItem(this, MealOrderNonPatientMetadata.ColumnNames.IsDistributed, esSystemType.Boolean);
            }
        }

        public esQueryItem DistributedDateTime
        {
            get
            {
                return new esQueryItem(this, MealOrderNonPatientMetadata.ColumnNames.DistributedDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem DistributedByUserID
        {
            get
            {
                return new esQueryItem(this, MealOrderNonPatientMetadata.ColumnNames.DistributedByUserID, esSystemType.String);
            }
        }

        public esQueryItem LastCreateDateTime
        {
            get
            {
                return new esQueryItem(this, MealOrderNonPatientMetadata.ColumnNames.LastCreateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastCreateByUserID
        {
            get
            {
                return new esQueryItem(this, MealOrderNonPatientMetadata.ColumnNames.LastCreateByUserID, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, MealOrderNonPatientMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, MealOrderNonPatientMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("MealOrderNonPatientCollection")]
    public partial class MealOrderNonPatientCollection : esMealOrderNonPatientCollection, IEnumerable<MealOrderNonPatient>
    {
        public MealOrderNonPatientCollection()
        {

        }

        public static implicit operator List<MealOrderNonPatient>(MealOrderNonPatientCollection coll)
        {
            List<MealOrderNonPatient> list = new List<MealOrderNonPatient>();

            foreach (MealOrderNonPatient emp in coll)
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
                return MealOrderNonPatientMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new MealOrderNonPatientQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new MealOrderNonPatient(row);
        }

        override protected esEntity CreateEntity()
        {
            return new MealOrderNonPatient();
        }

        #endregion

        [BrowsableAttribute(false)]
        public MealOrderNonPatientQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new MealOrderNonPatientQuery();
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
        public bool Load(MealOrderNonPatientQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public MealOrderNonPatient AddNew()
        {
            MealOrderNonPatient entity = base.AddNewEntity() as MealOrderNonPatient;

            return entity;
        }
        public MealOrderNonPatient FindByPrimaryKey(String transactionNo)
        {
            return base.FindByPrimaryKey(transactionNo) as MealOrderNonPatient;
        }

        #region IEnumerable< MealOrderNonPatient> Members

        IEnumerator<MealOrderNonPatient> IEnumerable<MealOrderNonPatient>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as MealOrderNonPatient;
            }
        }

        #endregion

        private MealOrderNonPatientQuery query;
    }


    /// <summary>
    /// Encapsulates the 'MealOrderNonPatient' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("MealOrderNonPatient ({TransactionNo})")]
    [Serializable]
    public partial class MealOrderNonPatient : esMealOrderNonPatient
    {
        public MealOrderNonPatient()
        {
        }

        public MealOrderNonPatient(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return MealOrderNonPatientMetadata.Meta();
            }
        }

        override protected esMealOrderNonPatientQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new MealOrderNonPatientQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public MealOrderNonPatientQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new MealOrderNonPatientQuery();
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
        public bool Load(MealOrderNonPatientQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private MealOrderNonPatientQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class MealOrderNonPatientQuery : esMealOrderNonPatientQuery
    {
        public MealOrderNonPatientQuery()
        {

        }

        public MealOrderNonPatientQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "MealOrderNonPatientQuery";
        }
    }

    [Serializable]
    public partial class MealOrderNonPatientMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected MealOrderNonPatientMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(MealOrderNonPatientMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = MealOrderNonPatientMetadata.PropertyNames.TransactionNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(MealOrderNonPatientMetadata.ColumnNames.TransactionDate, 1, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = MealOrderNonPatientMetadata.PropertyNames.TransactionDate;
            _columns.Add(c);

            c = new esColumnMetadata(MealOrderNonPatientMetadata.ColumnNames.RegistrationNo, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = MealOrderNonPatientMetadata.PropertyNames.RegistrationNo;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(MealOrderNonPatientMetadata.ColumnNames.IsApproved, 3, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = MealOrderNonPatientMetadata.PropertyNames.IsApproved;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MealOrderNonPatientMetadata.ColumnNames.ApprovedDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = MealOrderNonPatientMetadata.PropertyNames.ApprovedDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MealOrderNonPatientMetadata.ColumnNames.ApprovedByUserID, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = MealOrderNonPatientMetadata.PropertyNames.ApprovedByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MealOrderNonPatientMetadata.ColumnNames.IsVoid, 6, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = MealOrderNonPatientMetadata.PropertyNames.IsVoid;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MealOrderNonPatientMetadata.ColumnNames.IsDistributed, 7, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = MealOrderNonPatientMetadata.PropertyNames.IsDistributed;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MealOrderNonPatientMetadata.ColumnNames.DistributedDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = MealOrderNonPatientMetadata.PropertyNames.DistributedDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MealOrderNonPatientMetadata.ColumnNames.DistributedByUserID, 9, typeof(System.String), esSystemType.String);
            c.PropertyName = MealOrderNonPatientMetadata.PropertyNames.DistributedByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MealOrderNonPatientMetadata.ColumnNames.LastCreateDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = MealOrderNonPatientMetadata.PropertyNames.LastCreateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MealOrderNonPatientMetadata.ColumnNames.LastCreateByUserID, 11, typeof(System.String), esSystemType.String);
            c.PropertyName = MealOrderNonPatientMetadata.PropertyNames.LastCreateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MealOrderNonPatientMetadata.ColumnNames.LastUpdateDateTime, 12, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = MealOrderNonPatientMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MealOrderNonPatientMetadata.ColumnNames.LastUpdateByUserID, 13, typeof(System.String), esSystemType.String);
            c.PropertyName = MealOrderNonPatientMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public MealOrderNonPatientMetadata Meta()
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
            public const string TransactionNo = "TransactionNo";
            public const string TransactionDate = "TransactionDate";
            public const string RegistrationNo = "RegistrationNo";
            public const string IsApproved = "IsApproved";
            public const string ApprovedDateTime = "ApprovedDateTime";
            public const string ApprovedByUserID = "ApprovedByUserID";
            public const string IsVoid = "IsVoid";
            public const string IsDistributed = "IsDistributed";
            public const string DistributedDateTime = "DistributedDateTime";
            public const string DistributedByUserID = "DistributedByUserID";
            public const string LastCreateDateTime = "LastCreateDateTime";
            public const string LastCreateByUserID = "LastCreateByUserID";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string TransactionNo = "TransactionNo";
            public const string TransactionDate = "TransactionDate";
            public const string RegistrationNo = "RegistrationNo";
            public const string IsApproved = "IsApproved";
            public const string ApprovedDateTime = "ApprovedDateTime";
            public const string ApprovedByUserID = "ApprovedByUserID";
            public const string IsVoid = "IsVoid";
            public const string IsDistributed = "IsDistributed";
            public const string DistributedDateTime = "DistributedDateTime";
            public const string DistributedByUserID = "DistributedByUserID";
            public const string LastCreateDateTime = "LastCreateDateTime";
            public const string LastCreateByUserID = "LastCreateByUserID";
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
            lock (typeof(MealOrderNonPatientMetadata))
            {
                if (MealOrderNonPatientMetadata.mapDelegates == null)
                {
                    MealOrderNonPatientMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (MealOrderNonPatientMetadata.meta == null)
                {
                    MealOrderNonPatientMetadata.meta = new MealOrderNonPatientMetadata();
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

                meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("TransactionDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("ApprovedDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("ApprovedByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsDistributed", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("DistributedDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("DistributedByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastCreateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastCreateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "MealOrderNonPatient";
                meta.Destination = "MealOrderNonPatient";
                meta.spInsert = "proc_MealOrderNonPatientInsert";
                meta.spUpdate = "proc_MealOrderNonPatientUpdate";
                meta.spDelete = "proc_MealOrderNonPatientDelete";
                meta.spLoadAll = "proc_MealOrderNonPatientLoadAll";
                meta.spLoadByPrimaryKey = "proc_MealOrderNonPatientLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private MealOrderNonPatientMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
