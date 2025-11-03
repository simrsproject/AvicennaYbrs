/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 12/19/2019 9:27:01 AM
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
    abstract public class esTransPrescriptionReviewCollection : esEntityCollectionWAuditLog
    {
        public esTransPrescriptionReviewCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "TransPrescriptionReviewCollection";
        }

        #region Query Logic
        protected void InitQuery(esTransPrescriptionReviewQuery query)
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
            this.InitQuery(query as esTransPrescriptionReviewQuery);
        }
        #endregion

        virtual public TransPrescriptionReview DetachEntity(TransPrescriptionReview entity)
        {
            return base.DetachEntity(entity) as TransPrescriptionReview;
        }

        virtual public TransPrescriptionReview AttachEntity(TransPrescriptionReview entity)
        {
            return base.AttachEntity(entity) as TransPrescriptionReview;
        }

        virtual public void Combine(TransPrescriptionReviewCollection collection)
        {
            base.Combine(collection);
        }

        new public TransPrescriptionReview this[int index]
        {
            get
            {
                return base[index] as TransPrescriptionReview;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(TransPrescriptionReview);
        }
    }

    [Serializable]
    abstract public class esTransPrescriptionReview : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esTransPrescriptionReviewQuery GetDynamicQuery()
        {
            return null;
        }

        public esTransPrescriptionReview()
        {
        }

        public esTransPrescriptionReview(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String prescriptionNo, String sRPrescriptionReview)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(prescriptionNo, sRPrescriptionReview);
            else
                return LoadByPrimaryKeyStoredProcedure(prescriptionNo, sRPrescriptionReview);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String prescriptionNo, String sRPrescriptionReview)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(prescriptionNo, sRPrescriptionReview);
            else
                return LoadByPrimaryKeyStoredProcedure(prescriptionNo, sRPrescriptionReview);
        }

        private bool LoadByPrimaryKeyDynamic(String prescriptionNo, String sRPrescriptionReview)
        {
            esTransPrescriptionReviewQuery query = this.GetDynamicQuery();
            query.Where(query.PrescriptionNo == prescriptionNo, query.SRPrescriptionReview == sRPrescriptionReview);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String prescriptionNo, String sRPrescriptionReview)
        {
            esParameters parms = new esParameters();
            parms.Add("PrescriptionNo", prescriptionNo);
            parms.Add("SRPrescriptionReview", sRPrescriptionReview);
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
                        case "PrescriptionNo": this.str.PrescriptionNo = (string)value; break;
                        case "SRPrescriptionReview": this.str.SRPrescriptionReview = (string)value; break;
                        case "IsPrescriptionReview": this.str.IsPrescriptionReview = (string)value; break;
                        case "IsDrugReview": this.str.IsDrugReview = (string)value; break;
                        case "Note": this.str.Note = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "PrescriptionReviewDateTime": this.str.PrescriptionReviewDateTime = (string)value; break;
                        case "PrescriptionReviewByUserID": this.str.PrescriptionReviewByUserID = (string)value; break;
                        case "DrugReviewDateTime": this.str.DrugReviewDateTime = (string)value; break;
                        case "DrugReviewByUserID": this.str.DrugReviewByUserID = (string)value; break;
                        case "NoteDateTime": this.str.NoteDateTime = (string)value; break;
                        case "NoteByUserID": this.str.NoteByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "IsPrescriptionReview":

                            if (value == null || value is System.Boolean)
                                this.IsPrescriptionReview = (System.Boolean?)value;
                            break;
                        case "IsDrugReview":

                            if (value == null || value is System.Boolean)
                                this.IsDrugReview = (System.Boolean?)value;
                            break;
                        case "LastUpdateDateTime":

                            if (value == null || value is System.DateTime)
                                this.LastUpdateDateTime = (System.DateTime?)value;
                            break;
                        case "PrescriptionReviewDateTime":

                            if (value == null || value is System.DateTime)
                                this.PrescriptionReviewDateTime = (System.DateTime?)value;
                            break;
                        case "DrugReviewDateTime":

                            if (value == null || value is System.DateTime)
                                this.DrugReviewDateTime = (System.DateTime?)value;
                            break;
                        case "NoteDateTime":

                            if (value == null || value is System.DateTime)
                                this.NoteDateTime = (System.DateTime?)value;
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
        /// Maps to TransPrescriptionReview.PrescriptionNo
        /// </summary>
        virtual public System.String PrescriptionNo
        {
            get
            {
                return base.GetSystemString(TransPrescriptionReviewMetadata.ColumnNames.PrescriptionNo);
            }

            set
            {
                base.SetSystemString(TransPrescriptionReviewMetadata.ColumnNames.PrescriptionNo, value);
            }
        }
        /// <summary>
        /// Maps to TransPrescriptionReview.SRPrescriptionReview
        /// </summary>
        virtual public System.String SRPrescriptionReview
        {
            get
            {
                return base.GetSystemString(TransPrescriptionReviewMetadata.ColumnNames.SRPrescriptionReview);
            }

            set
            {
                base.SetSystemString(TransPrescriptionReviewMetadata.ColumnNames.SRPrescriptionReview, value);
            }
        }
        /// <summary>
        /// Maps to TransPrescriptionReview.IsPrescriptionReview
        /// </summary>
        virtual public System.Boolean? IsPrescriptionReview
        {
            get
            {
                return base.GetSystemBoolean(TransPrescriptionReviewMetadata.ColumnNames.IsPrescriptionReview);
            }

            set
            {
                base.SetSystemBoolean(TransPrescriptionReviewMetadata.ColumnNames.IsPrescriptionReview, value);
            }
        }
        /// <summary>
        /// Maps to TransPrescriptionReview.IsDrugReview
        /// </summary>
        virtual public System.Boolean? IsDrugReview
        {
            get
            {
                return base.GetSystemBoolean(TransPrescriptionReviewMetadata.ColumnNames.IsDrugReview);
            }

            set
            {
                base.SetSystemBoolean(TransPrescriptionReviewMetadata.ColumnNames.IsDrugReview, value);
            }
        }
        /// <summary>
        /// Maps to TransPrescriptionReview.Note
        /// </summary>
        virtual public System.String Note
        {
            get
            {
                return base.GetSystemString(TransPrescriptionReviewMetadata.ColumnNames.Note);
            }

            set
            {
                base.SetSystemString(TransPrescriptionReviewMetadata.ColumnNames.Note, value);
            }
        }
        /// <summary>
        /// Maps to TransPrescriptionReview.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(TransPrescriptionReviewMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(TransPrescriptionReviewMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to TransPrescriptionReview.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(TransPrescriptionReviewMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(TransPrescriptionReviewMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to TransPrescriptionReview.PrescriptionReviewDateTime
        /// </summary>
        virtual public System.DateTime? PrescriptionReviewDateTime
        {
            get
            {
                return base.GetSystemDateTime(TransPrescriptionReviewMetadata.ColumnNames.PrescriptionReviewDateTime);
            }

            set
            {
                base.SetSystemDateTime(TransPrescriptionReviewMetadata.ColumnNames.PrescriptionReviewDateTime, value);
            }
        }
        /// <summary>
        /// Maps to TransPrescriptionReview.PrescriptionReviewByUserID
        /// </summary>
        virtual public System.String PrescriptionReviewByUserID
        {
            get
            {
                return base.GetSystemString(TransPrescriptionReviewMetadata.ColumnNames.PrescriptionReviewByUserID);
            }

            set
            {
                base.SetSystemString(TransPrescriptionReviewMetadata.ColumnNames.PrescriptionReviewByUserID, value);
            }
        }
        /// <summary>
        /// Maps to TransPrescriptionReview.DrugReviewDateTime
        /// </summary>
        virtual public System.DateTime? DrugReviewDateTime
        {
            get
            {
                return base.GetSystemDateTime(TransPrescriptionReviewMetadata.ColumnNames.DrugReviewDateTime);
            }

            set
            {
                base.SetSystemDateTime(TransPrescriptionReviewMetadata.ColumnNames.DrugReviewDateTime, value);
            }
        }
        /// <summary>
        /// Maps to TransPrescriptionReview.DrugReviewByUserID
        /// </summary>
        virtual public System.String DrugReviewByUserID
        {
            get
            {
                return base.GetSystemString(TransPrescriptionReviewMetadata.ColumnNames.DrugReviewByUserID);
            }

            set
            {
                base.SetSystemString(TransPrescriptionReviewMetadata.ColumnNames.DrugReviewByUserID, value);
            }
        }
        /// <summary>
        /// Maps to TransPrescriptionReview.NoteDateTime
        /// </summary>
        virtual public System.DateTime? NoteDateTime
        {
            get
            {
                return base.GetSystemDateTime(TransPrescriptionReviewMetadata.ColumnNames.NoteDateTime);
            }

            set
            {
                base.SetSystemDateTime(TransPrescriptionReviewMetadata.ColumnNames.NoteDateTime, value);
            }
        }
        /// <summary>
        /// Maps to TransPrescriptionReview.NoteByUserID
        /// </summary>
        virtual public System.String NoteByUserID
        {
            get
            {
                return base.GetSystemString(TransPrescriptionReviewMetadata.ColumnNames.NoteByUserID);
            }

            set
            {
                base.SetSystemString(TransPrescriptionReviewMetadata.ColumnNames.NoteByUserID, value);
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
            public esStrings(esTransPrescriptionReview entity)
            {
                this.entity = entity;
            }
            public System.String PrescriptionNo
            {
                get
                {
                    System.String data = entity.PrescriptionNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PrescriptionNo = null;
                    else entity.PrescriptionNo = Convert.ToString(value);
                }
            }
            public System.String SRPrescriptionReview
            {
                get
                {
                    System.String data = entity.SRPrescriptionReview;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRPrescriptionReview = null;
                    else entity.SRPrescriptionReview = Convert.ToString(value);
                }
            }
            public System.String IsPrescriptionReview
            {
                get
                {
                    System.Boolean? data = entity.IsPrescriptionReview;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsPrescriptionReview = null;
                    else entity.IsPrescriptionReview = Convert.ToBoolean(value);
                }
            }
            public System.String IsDrugReview
            {
                get
                {
                    System.Boolean? data = entity.IsDrugReview;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsDrugReview = null;
                    else entity.IsDrugReview = Convert.ToBoolean(value);
                }
            }
            public System.String Note
            {
                get
                {
                    System.String data = entity.Note;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Note = null;
                    else entity.Note = Convert.ToString(value);
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
            public System.String PrescriptionReviewDateTime
            {
                get
                {
                    System.DateTime? data = entity.PrescriptionReviewDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PrescriptionReviewDateTime = null;
                    else entity.PrescriptionReviewDateTime = Convert.ToDateTime(value);
                }
            }
            public System.String PrescriptionReviewByUserID
            {
                get
                {
                    System.String data = entity.PrescriptionReviewByUserID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PrescriptionReviewByUserID = null;
                    else entity.PrescriptionReviewByUserID = Convert.ToString(value);
                }
            }
            public System.String DrugReviewDateTime
            {
                get
                {
                    System.DateTime? data = entity.DrugReviewDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DrugReviewDateTime = null;
                    else entity.DrugReviewDateTime = Convert.ToDateTime(value);
                }
            }
            public System.String DrugReviewByUserID
            {
                get
                {
                    System.String data = entity.DrugReviewByUserID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DrugReviewByUserID = null;
                    else entity.DrugReviewByUserID = Convert.ToString(value);
                }
            }
            public System.String NoteDateTime
            {
                get
                {
                    System.DateTime? data = entity.NoteDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.NoteDateTime = null;
                    else entity.NoteDateTime = Convert.ToDateTime(value);
                }
            }
            public System.String NoteByUserID
            {
                get
                {
                    System.String data = entity.NoteByUserID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.NoteByUserID = null;
                    else entity.NoteByUserID = Convert.ToString(value);
                }
            }
            private esTransPrescriptionReview entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esTransPrescriptionReviewQuery query)
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
                throw new Exception("esTransPrescriptionReview can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class TransPrescriptionReview : esTransPrescriptionReview
    {
    }

    [Serializable]
    abstract public class esTransPrescriptionReviewQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return TransPrescriptionReviewMetadata.Meta();
            }
        }

        public esQueryItem PrescriptionNo
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionReviewMetadata.ColumnNames.PrescriptionNo, esSystemType.String);
            }
        }

        public esQueryItem SRPrescriptionReview
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionReviewMetadata.ColumnNames.SRPrescriptionReview, esSystemType.String);
            }
        }

        public esQueryItem IsPrescriptionReview
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionReviewMetadata.ColumnNames.IsPrescriptionReview, esSystemType.Boolean);
            }
        }

        public esQueryItem IsDrugReview
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionReviewMetadata.ColumnNames.IsDrugReview, esSystemType.Boolean);
            }
        }

        public esQueryItem Note
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionReviewMetadata.ColumnNames.Note, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionReviewMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionReviewMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem PrescriptionReviewDateTime
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionReviewMetadata.ColumnNames.PrescriptionReviewDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem PrescriptionReviewByUserID
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionReviewMetadata.ColumnNames.PrescriptionReviewByUserID, esSystemType.String);
            }
        }

        public esQueryItem DrugReviewDateTime
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionReviewMetadata.ColumnNames.DrugReviewDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem DrugReviewByUserID
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionReviewMetadata.ColumnNames.DrugReviewByUserID, esSystemType.String);
            }
        }

        public esQueryItem NoteDateTime
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionReviewMetadata.ColumnNames.NoteDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem NoteByUserID
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionReviewMetadata.ColumnNames.NoteByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("TransPrescriptionReviewCollection")]
    public partial class TransPrescriptionReviewCollection : esTransPrescriptionReviewCollection, IEnumerable<TransPrescriptionReview>
    {
        public TransPrescriptionReviewCollection()
        {

        }

        public static implicit operator List<TransPrescriptionReview>(TransPrescriptionReviewCollection coll)
        {
            List<TransPrescriptionReview> list = new List<TransPrescriptionReview>();

            foreach (TransPrescriptionReview emp in coll)
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
                return TransPrescriptionReviewMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new TransPrescriptionReviewQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new TransPrescriptionReview(row);
        }

        override protected esEntity CreateEntity()
        {
            return new TransPrescriptionReview();
        }

        #endregion

        [BrowsableAttribute(false)]
        public TransPrescriptionReviewQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new TransPrescriptionReviewQuery();
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
        public bool Load(TransPrescriptionReviewQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public TransPrescriptionReview AddNew()
        {
            TransPrescriptionReview entity = base.AddNewEntity() as TransPrescriptionReview;

            return entity;
        }
        public TransPrescriptionReview FindByPrimaryKey(String prescriptionNo, String sRPrescriptionReview)
        {
            return base.FindByPrimaryKey(prescriptionNo, sRPrescriptionReview) as TransPrescriptionReview;
        }

        #region IEnumerable< TransPrescriptionReview> Members

        IEnumerator<TransPrescriptionReview> IEnumerable<TransPrescriptionReview>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as TransPrescriptionReview;
            }
        }

        #endregion

        private TransPrescriptionReviewQuery query;
    }


    /// <summary>
    /// Encapsulates the 'TransPrescriptionReview' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("TransPrescriptionReview ({PrescriptionNo, SRPrescriptionReview})")]
    [Serializable]
    public partial class TransPrescriptionReview : esTransPrescriptionReview
    {
        public TransPrescriptionReview()
        {
        }

        public TransPrescriptionReview(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return TransPrescriptionReviewMetadata.Meta();
            }
        }

        override protected esTransPrescriptionReviewQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new TransPrescriptionReviewQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public TransPrescriptionReviewQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new TransPrescriptionReviewQuery();
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
        public bool Load(TransPrescriptionReviewQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private TransPrescriptionReviewQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class TransPrescriptionReviewQuery : esTransPrescriptionReviewQuery
    {
        public TransPrescriptionReviewQuery()
        {

        }

        public TransPrescriptionReviewQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "TransPrescriptionReviewQuery";
        }
    }

    [Serializable]
    public partial class TransPrescriptionReviewMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected TransPrescriptionReviewMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(TransPrescriptionReviewMetadata.ColumnNames.PrescriptionNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = TransPrescriptionReviewMetadata.PropertyNames.PrescriptionNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionReviewMetadata.ColumnNames.SRPrescriptionReview, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = TransPrescriptionReviewMetadata.PropertyNames.SRPrescriptionReview;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 5;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionReviewMetadata.ColumnNames.IsPrescriptionReview, 2, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = TransPrescriptionReviewMetadata.PropertyNames.IsPrescriptionReview;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionReviewMetadata.ColumnNames.IsDrugReview, 3, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = TransPrescriptionReviewMetadata.PropertyNames.IsDrugReview;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionReviewMetadata.ColumnNames.Note, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = TransPrescriptionReviewMetadata.PropertyNames.Note;
            c.CharacterMaxLength = 200;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionReviewMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = TransPrescriptionReviewMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionReviewMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = TransPrescriptionReviewMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionReviewMetadata.ColumnNames.PrescriptionReviewDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = TransPrescriptionReviewMetadata.PropertyNames.PrescriptionReviewDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionReviewMetadata.ColumnNames.PrescriptionReviewByUserID, 8, typeof(System.String), esSystemType.String);
            c.PropertyName = TransPrescriptionReviewMetadata.PropertyNames.PrescriptionReviewByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionReviewMetadata.ColumnNames.DrugReviewDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = TransPrescriptionReviewMetadata.PropertyNames.DrugReviewDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionReviewMetadata.ColumnNames.DrugReviewByUserID, 10, typeof(System.String), esSystemType.String);
            c.PropertyName = TransPrescriptionReviewMetadata.PropertyNames.DrugReviewByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionReviewMetadata.ColumnNames.NoteDateTime, 11, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = TransPrescriptionReviewMetadata.PropertyNames.NoteDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionReviewMetadata.ColumnNames.NoteByUserID, 12, typeof(System.String), esSystemType.String);
            c.PropertyName = TransPrescriptionReviewMetadata.PropertyNames.NoteByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public TransPrescriptionReviewMetadata Meta()
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
            public const string PrescriptionNo = "PrescriptionNo";
            public const string SRPrescriptionReview = "SRPrescriptionReview";
            public const string IsPrescriptionReview = "IsPrescriptionReview";
            public const string IsDrugReview = "IsDrugReview";
            public const string Note = "Note";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string PrescriptionReviewDateTime = "PrescriptionReviewDateTime";
            public const string PrescriptionReviewByUserID = "PrescriptionReviewByUserID";
            public const string DrugReviewDateTime = "DrugReviewDateTime";
            public const string DrugReviewByUserID = "DrugReviewByUserID";
            public const string NoteDateTime = "NoteDateTime";
            public const string NoteByUserID = "NoteByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string PrescriptionNo = "PrescriptionNo";
            public const string SRPrescriptionReview = "SRPrescriptionReview";
            public const string IsPrescriptionReview = "IsPrescriptionReview";
            public const string IsDrugReview = "IsDrugReview";
            public const string Note = "Note";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string PrescriptionReviewDateTime = "PrescriptionReviewDateTime";
            public const string PrescriptionReviewByUserID = "PrescriptionReviewByUserID";
            public const string DrugReviewDateTime = "DrugReviewDateTime";
            public const string DrugReviewByUserID = "DrugReviewByUserID";
            public const string NoteDateTime = "NoteDateTime";
            public const string NoteByUserID = "NoteByUserID";
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
            lock (typeof(TransPrescriptionReviewMetadata))
            {
                if (TransPrescriptionReviewMetadata.mapDelegates == null)
                {
                    TransPrescriptionReviewMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (TransPrescriptionReviewMetadata.meta == null)
                {
                    TransPrescriptionReviewMetadata.meta = new TransPrescriptionReviewMetadata();
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

                meta.AddTypeMap("PrescriptionNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRPrescriptionReview", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsPrescriptionReview", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsDrugReview", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("Note", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("PrescriptionReviewDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("PrescriptionReviewByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("DrugReviewDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("DrugReviewByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("NoteDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("NoteByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "TransPrescriptionReview";
                meta.Destination = "TransPrescriptionReview";
                meta.spInsert = "proc_TransPrescriptionReviewInsert";
                meta.spUpdate = "proc_TransPrescriptionReviewUpdate";
                meta.spDelete = "proc_TransPrescriptionReviewDelete";
                meta.spLoadAll = "proc_TransPrescriptionReviewLoadAll";
                meta.spLoadByPrimaryKey = "proc_TransPrescriptionReviewLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private TransPrescriptionReviewMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
