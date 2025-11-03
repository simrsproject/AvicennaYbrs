/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/21/19 7:29:47 PM
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
    abstract public class esParamedicConsultFormCollection : esEntityCollectionWAuditLog
    {
        public esParamedicConsultFormCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "ParamedicConsultFormCollection";
        }

        #region Query Logic
        protected void InitQuery(esParamedicConsultFormQuery query)
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
            this.InitQuery(query as esParamedicConsultFormQuery);
        }
        #endregion

        virtual public ParamedicConsultForm DetachEntity(ParamedicConsultForm entity)
        {
            return base.DetachEntity(entity) as ParamedicConsultForm;
        }

        virtual public ParamedicConsultForm AttachEntity(ParamedicConsultForm entity)
        {
            return base.AttachEntity(entity) as ParamedicConsultForm;
        }

        virtual public void Combine(ParamedicConsultFormCollection collection)
        {
            base.Combine(collection);
        }

        new public ParamedicConsultForm this[int index]
        {
            get
            {
                return base[index] as ParamedicConsultForm;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(ParamedicConsultForm);
        }
    }

    [Serializable]
    abstract public class esParamedicConsultForm : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esParamedicConsultFormQuery GetDynamicQuery()
        {
            return null;
        }

        public esParamedicConsultForm()
        {
        }

        public esParamedicConsultForm(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String consultReferNo, String transactionNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(consultReferNo, transactionNo);
            else
                return LoadByPrimaryKeyStoredProcedure(consultReferNo, transactionNo);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String consultReferNo, String transactionNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(consultReferNo, transactionNo);
            else
                return LoadByPrimaryKeyStoredProcedure(consultReferNo, transactionNo);
        }

        private bool LoadByPrimaryKeyDynamic(String consultReferNo, String transactionNo)
        {
            esParamedicConsultFormQuery query = this.GetDynamicQuery();
            query.Where(query.ConsultReferNo == consultReferNo, query.TransactionNo == transactionNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String consultReferNo, String transactionNo)
        {
            esParameters parms = new esParameters();
            parms.Add("ConsultReferNo", consultReferNo);
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
                        case "ConsultReferNo": this.str.ConsultReferNo = (string)value; break;
                        case "TransactionNo": this.str.TransactionNo = (string)value; break;
                        case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
                        case "QuestionFormID": this.str.QuestionFormID = (string)value; break;
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
        /// Maps to ParamedicConsultForm.ConsultReferNo
        /// </summary>
        virtual public System.String ConsultReferNo
        {
            get
            {
                return base.GetSystemString(ParamedicConsultFormMetadata.ColumnNames.ConsultReferNo);
            }

            set
            {
                base.SetSystemString(ParamedicConsultFormMetadata.ColumnNames.ConsultReferNo, value);
            }
        }
        /// <summary>
        /// Maps to ParamedicConsultForm.TransactionNo
        /// </summary>
        virtual public System.String TransactionNo
        {
            get
            {
                return base.GetSystemString(ParamedicConsultFormMetadata.ColumnNames.TransactionNo);
            }

            set
            {
                base.SetSystemString(ParamedicConsultFormMetadata.ColumnNames.TransactionNo, value);
            }
        }
        /// <summary>
        /// Maps to ParamedicConsultForm.RegistrationNo
        /// </summary>
        virtual public System.String RegistrationNo
        {
            get
            {
                return base.GetSystemString(ParamedicConsultFormMetadata.ColumnNames.RegistrationNo);
            }

            set
            {
                base.SetSystemString(ParamedicConsultFormMetadata.ColumnNames.RegistrationNo, value);
            }
        }
        /// <summary>
        /// Maps to ParamedicConsultForm.QuestionFormID
        /// </summary>
        virtual public System.String QuestionFormID
        {
            get
            {
                return base.GetSystemString(ParamedicConsultFormMetadata.ColumnNames.QuestionFormID);
            }

            set
            {
                base.SetSystemString(ParamedicConsultFormMetadata.ColumnNames.QuestionFormID, value);
            }
        }
        /// <summary>
        /// Maps to ParamedicConsultForm.CreatedDateTime
        /// </summary>
        virtual public System.DateTime? CreatedDateTime
        {
            get
            {
                return base.GetSystemDateTime(ParamedicConsultFormMetadata.ColumnNames.CreatedDateTime);
            }

            set
            {
                base.SetSystemDateTime(ParamedicConsultFormMetadata.ColumnNames.CreatedDateTime, value);
            }
        }
        /// <summary>
        /// Maps to ParamedicConsultForm.CreatedByUserID
        /// </summary>
        virtual public System.String CreatedByUserID
        {
            get
            {
                return base.GetSystemString(ParamedicConsultFormMetadata.ColumnNames.CreatedByUserID);
            }

            set
            {
                base.SetSystemString(ParamedicConsultFormMetadata.ColumnNames.CreatedByUserID, value);
            }
        }
        /// <summary>
        /// Maps to ParamedicConsultForm.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(ParamedicConsultFormMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(ParamedicConsultFormMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to ParamedicConsultForm.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(ParamedicConsultFormMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(ParamedicConsultFormMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esParamedicConsultForm entity)
            {
                this.entity = entity;
            }
            public System.String ConsultReferNo
            {
                get
                {
                    System.String data = entity.ConsultReferNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ConsultReferNo = null;
                    else entity.ConsultReferNo = Convert.ToString(value);
                }
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
            public System.String QuestionFormID
            {
                get
                {
                    System.String data = entity.QuestionFormID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.QuestionFormID = null;
                    else entity.QuestionFormID = Convert.ToString(value);
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
            private esParamedicConsultForm entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esParamedicConsultFormQuery query)
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
                throw new Exception("esParamedicConsultForm can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class ParamedicConsultForm : esParamedicConsultForm
    {
    }

    [Serializable]
    abstract public class esParamedicConsultFormQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return ParamedicConsultFormMetadata.Meta();
            }
        }

        public esQueryItem ConsultReferNo
        {
            get
            {
                return new esQueryItem(this, ParamedicConsultFormMetadata.ColumnNames.ConsultReferNo, esSystemType.String);
            }
        }

        public esQueryItem TransactionNo
        {
            get
            {
                return new esQueryItem(this, ParamedicConsultFormMetadata.ColumnNames.TransactionNo, esSystemType.String);
            }
        }

        public esQueryItem RegistrationNo
        {
            get
            {
                return new esQueryItem(this, ParamedicConsultFormMetadata.ColumnNames.RegistrationNo, esSystemType.String);
            }
        }

        public esQueryItem QuestionFormID
        {
            get
            {
                return new esQueryItem(this, ParamedicConsultFormMetadata.ColumnNames.QuestionFormID, esSystemType.String);
            }
        }

        public esQueryItem CreatedDateTime
        {
            get
            {
                return new esQueryItem(this, ParamedicConsultFormMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem CreatedByUserID
        {
            get
            {
                return new esQueryItem(this, ParamedicConsultFormMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, ParamedicConsultFormMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, ParamedicConsultFormMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("ParamedicConsultFormCollection")]
    public partial class ParamedicConsultFormCollection : esParamedicConsultFormCollection, IEnumerable<ParamedicConsultForm>
    {
        public ParamedicConsultFormCollection()
        {

        }

        public static implicit operator List<ParamedicConsultForm>(ParamedicConsultFormCollection coll)
        {
            List<ParamedicConsultForm> list = new List<ParamedicConsultForm>();

            foreach (ParamedicConsultForm emp in coll)
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
                return ParamedicConsultFormMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ParamedicConsultFormQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new ParamedicConsultForm(row);
        }

        override protected esEntity CreateEntity()
        {
            return new ParamedicConsultForm();
        }

        #endregion

        [BrowsableAttribute(false)]
        public ParamedicConsultFormQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ParamedicConsultFormQuery();
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
        public bool Load(ParamedicConsultFormQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public ParamedicConsultForm AddNew()
        {
            ParamedicConsultForm entity = base.AddNewEntity() as ParamedicConsultForm;

            return entity;
        }
        public ParamedicConsultForm FindByPrimaryKey(String consultReferNo, String transactionNo)
        {
            return base.FindByPrimaryKey(consultReferNo, transactionNo) as ParamedicConsultForm;
        }

        #region IEnumerable< ParamedicConsultForm> Members

        IEnumerator<ParamedicConsultForm> IEnumerable<ParamedicConsultForm>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as ParamedicConsultForm;
            }
        }

        #endregion

        private ParamedicConsultFormQuery query;
    }


    /// <summary>
    /// Encapsulates the 'ParamedicConsultForm' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("ParamedicConsultForm ({ConsultReferNo, TransactionNo})")]
    [Serializable]
    public partial class ParamedicConsultForm : esParamedicConsultForm
    {
        public ParamedicConsultForm()
        {
        }

        public ParamedicConsultForm(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return ParamedicConsultFormMetadata.Meta();
            }
        }

        override protected esParamedicConsultFormQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ParamedicConsultFormQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public ParamedicConsultFormQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ParamedicConsultFormQuery();
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
        public bool Load(ParamedicConsultFormQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private ParamedicConsultFormQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class ParamedicConsultFormQuery : esParamedicConsultFormQuery
    {
        public ParamedicConsultFormQuery()
        {

        }

        public ParamedicConsultFormQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "ParamedicConsultFormQuery";
        }
    }

    [Serializable]
    public partial class ParamedicConsultFormMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected ParamedicConsultFormMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(ParamedicConsultFormMetadata.ColumnNames.ConsultReferNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = ParamedicConsultFormMetadata.PropertyNames.ConsultReferNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 25;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicConsultFormMetadata.ColumnNames.TransactionNo, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = ParamedicConsultFormMetadata.PropertyNames.TransactionNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicConsultFormMetadata.ColumnNames.RegistrationNo, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = ParamedicConsultFormMetadata.PropertyNames.RegistrationNo;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicConsultFormMetadata.ColumnNames.QuestionFormID, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = ParamedicConsultFormMetadata.PropertyNames.QuestionFormID;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicConsultFormMetadata.ColumnNames.CreatedDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ParamedicConsultFormMetadata.PropertyNames.CreatedDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicConsultFormMetadata.ColumnNames.CreatedByUserID, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = ParamedicConsultFormMetadata.PropertyNames.CreatedByUserID;
            c.CharacterMaxLength = 40;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicConsultFormMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ParamedicConsultFormMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicConsultFormMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = ParamedicConsultFormMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 40;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public ParamedicConsultFormMetadata Meta()
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
            public const string ConsultReferNo = "ConsultReferNo";
            public const string TransactionNo = "TransactionNo";
            public const string RegistrationNo = "RegistrationNo";
            public const string QuestionFormID = "QuestionFormID";
            public const string CreatedDateTime = "CreatedDateTime";
            public const string CreatedByUserID = "CreatedByUserID";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string ConsultReferNo = "ConsultReferNo";
            public const string TransactionNo = "TransactionNo";
            public const string RegistrationNo = "RegistrationNo";
            public const string QuestionFormID = "QuestionFormID";
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
            lock (typeof(ParamedicConsultFormMetadata))
            {
                if (ParamedicConsultFormMetadata.mapDelegates == null)
                {
                    ParamedicConsultFormMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (ParamedicConsultFormMetadata.meta == null)
                {
                    ParamedicConsultFormMetadata.meta = new ParamedicConsultFormMetadata();
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

                meta.AddTypeMap("ConsultReferNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("QuestionFormID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "ParamedicConsultForm";
                meta.Destination = "ParamedicConsultForm";
                meta.spInsert = "proc_ParamedicConsultFormInsert";
                meta.spUpdate = "proc_ParamedicConsultFormUpdate";
                meta.spDelete = "proc_ParamedicConsultFormDelete";
                meta.spLoadAll = "proc_ParamedicConsultFormLoadAll";
                meta.spLoadByPrimaryKey = "proc_ParamedicConsultFormLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private ParamedicConsultFormMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
