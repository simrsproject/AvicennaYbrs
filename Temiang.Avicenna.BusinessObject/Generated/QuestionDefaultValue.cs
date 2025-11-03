/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/09/19 6:04:29 PM
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
    abstract public class esQuestionDefaultValueCollection : esEntityCollectionWAuditLog
    {
        public esQuestionDefaultValueCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "QuestionDefaultValueCollection";
        }

        #region Query Logic
        protected void InitQuery(esQuestionDefaultValueQuery query)
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
            this.InitQuery(query as esQuestionDefaultValueQuery);
        }
        #endregion

        virtual public QuestionDefaultValue DetachEntity(QuestionDefaultValue entity)
        {
            return base.DetachEntity(entity) as QuestionDefaultValue;
        }

        virtual public QuestionDefaultValue AttachEntity(QuestionDefaultValue entity)
        {
            return base.AttachEntity(entity) as QuestionDefaultValue;
        }

        virtual public void Combine(QuestionDefaultValueCollection collection)
        {
            base.Combine(collection);
        }

        new public QuestionDefaultValue this[int index]
        {
            get
            {
                return base[index] as QuestionDefaultValue;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(QuestionDefaultValue);
        }
    }

    [Serializable]
    abstract public class esQuestionDefaultValue : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esQuestionDefaultValueQuery GetDynamicQuery()
        {
            return null;
        }

        public esQuestionDefaultValue()
        {
        }

        public esQuestionDefaultValue(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String questionFormID, String questionGroupID, String questionID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(questionFormID, questionGroupID, questionID);
            else
                return LoadByPrimaryKeyStoredProcedure(questionFormID, questionGroupID, questionID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String questionFormID, String questionGroupID, String questionID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(questionFormID, questionGroupID, questionID);
            else
                return LoadByPrimaryKeyStoredProcedure(questionFormID, questionGroupID, questionID);
        }

        private bool LoadByPrimaryKeyDynamic(String questionFormID, String questionGroupID, String questionID)
        {
            esQuestionDefaultValueQuery query = this.GetDynamicQuery();
            query.Where(query.QuestionFormID == questionFormID, query.QuestionGroupID == questionGroupID, query.QuestionID == questionID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String questionFormID, String questionGroupID, String questionID)
        {
            esParameters parms = new esParameters();
            parms.Add("QuestionFormID", questionFormID);
            parms.Add("QuestionGroupID", questionGroupID);
            parms.Add("QuestionID", questionID);
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
                        case "QuestionFormID": this.str.QuestionFormID = (string)value; break;
                        case "QuestionGroupID": this.str.QuestionGroupID = (string)value; break;
                        case "QuestionID": this.str.QuestionID = (string)value; break;
                        case "FromQuestionFormID": this.str.FromQuestionFormID = (string)value; break;
                        case "FromQuestionGroupID": this.str.FromQuestionGroupID = (string)value; break;
                        case "FromQuestionID": this.str.FromQuestionID = (string)value; break;
                        case "IsFromCurrentRegistration": this.str.IsFromCurrentRegistration = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "IsFromCurrentRegistration":

                            if (value == null || value is System.Boolean)
                                this.IsFromCurrentRegistration = (System.Boolean?)value;
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
        /// Maps to QuestionDefaultValue.QuestionFormID
        /// </summary>
        virtual public System.String QuestionFormID
        {
            get
            {
                return base.GetSystemString(QuestionDefaultValueMetadata.ColumnNames.QuestionFormID);
            }

            set
            {
                base.SetSystemString(QuestionDefaultValueMetadata.ColumnNames.QuestionFormID, value);
            }
        }
        /// <summary>
        /// Maps to QuestionDefaultValue.QuestionGroupID
        /// </summary>
        virtual public System.String QuestionGroupID
        {
            get
            {
                return base.GetSystemString(QuestionDefaultValueMetadata.ColumnNames.QuestionGroupID);
            }

            set
            {
                base.SetSystemString(QuestionDefaultValueMetadata.ColumnNames.QuestionGroupID, value);
            }
        }
        /// <summary>
        /// Maps to QuestionDefaultValue.QuestionID
        /// </summary>
        virtual public System.String QuestionID
        {
            get
            {
                return base.GetSystemString(QuestionDefaultValueMetadata.ColumnNames.QuestionID);
            }

            set
            {
                base.SetSystemString(QuestionDefaultValueMetadata.ColumnNames.QuestionID, value);
            }
        }
        /// <summary>
        /// Maps to QuestionDefaultValue.FromQuestionFormID
        /// </summary>
        virtual public System.String FromQuestionFormID
        {
            get
            {
                return base.GetSystemString(QuestionDefaultValueMetadata.ColumnNames.FromQuestionFormID);
            }

            set
            {
                base.SetSystemString(QuestionDefaultValueMetadata.ColumnNames.FromQuestionFormID, value);
            }
        }
        /// <summary>
        /// Maps to QuestionDefaultValue.FromQuestionGroupID
        /// </summary>
        virtual public System.String FromQuestionGroupID
        {
            get
            {
                return base.GetSystemString(QuestionDefaultValueMetadata.ColumnNames.FromQuestionGroupID);
            }

            set
            {
                base.SetSystemString(QuestionDefaultValueMetadata.ColumnNames.FromQuestionGroupID, value);
            }
        }
        /// <summary>
        /// Maps to QuestionDefaultValue.FromQuestionID
        /// </summary>
        virtual public System.String FromQuestionID
        {
            get
            {
                return base.GetSystemString(QuestionDefaultValueMetadata.ColumnNames.FromQuestionID);
            }

            set
            {
                base.SetSystemString(QuestionDefaultValueMetadata.ColumnNames.FromQuestionID, value);
            }
        }
        /// <summary>
        /// Maps to QuestionDefaultValue.IsFromCurrentRegistration
        /// </summary>
        virtual public System.Boolean? IsFromCurrentRegistration
        {
            get
            {
                return base.GetSystemBoolean(QuestionDefaultValueMetadata.ColumnNames.IsFromCurrentRegistration);
            }

            set
            {
                base.SetSystemBoolean(QuestionDefaultValueMetadata.ColumnNames.IsFromCurrentRegistration, value);
            }
        }
        /// <summary>
        /// Maps to QuestionDefaultValue.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(QuestionDefaultValueMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(QuestionDefaultValueMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to QuestionDefaultValue.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(QuestionDefaultValueMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(QuestionDefaultValueMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esQuestionDefaultValue entity)
            {
                this.entity = entity;
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
            public System.String QuestionGroupID
            {
                get
                {
                    System.String data = entity.QuestionGroupID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.QuestionGroupID = null;
                    else entity.QuestionGroupID = Convert.ToString(value);
                }
            }
            public System.String QuestionID
            {
                get
                {
                    System.String data = entity.QuestionID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.QuestionID = null;
                    else entity.QuestionID = Convert.ToString(value);
                }
            }
            public System.String FromQuestionFormID
            {
                get
                {
                    System.String data = entity.FromQuestionFormID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FromQuestionFormID = null;
                    else entity.FromQuestionFormID = Convert.ToString(value);
                }
            }
            public System.String FromQuestionGroupID
            {
                get
                {
                    System.String data = entity.FromQuestionGroupID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FromQuestionGroupID = null;
                    else entity.FromQuestionGroupID = Convert.ToString(value);
                }
            }
            public System.String FromQuestionID
            {
                get
                {
                    System.String data = entity.FromQuestionID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FromQuestionID = null;
                    else entity.FromQuestionID = Convert.ToString(value);
                }
            }
            public System.String IsFromCurrentRegistration
            {
                get
                {
                    System.Boolean? data = entity.IsFromCurrentRegistration;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsFromCurrentRegistration = null;
                    else entity.IsFromCurrentRegistration = Convert.ToBoolean(value);
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
            private esQuestionDefaultValue entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esQuestionDefaultValueQuery query)
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
                throw new Exception("esQuestionDefaultValue can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class QuestionDefaultValue : esQuestionDefaultValue
    {
    }

    [Serializable]
    abstract public class esQuestionDefaultValueQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return QuestionDefaultValueMetadata.Meta();
            }
        }

        public esQueryItem QuestionFormID
        {
            get
            {
                return new esQueryItem(this, QuestionDefaultValueMetadata.ColumnNames.QuestionFormID, esSystemType.String);
            }
        }

        public esQueryItem QuestionGroupID
        {
            get
            {
                return new esQueryItem(this, QuestionDefaultValueMetadata.ColumnNames.QuestionGroupID, esSystemType.String);
            }
        }

        public esQueryItem QuestionID
        {
            get
            {
                return new esQueryItem(this, QuestionDefaultValueMetadata.ColumnNames.QuestionID, esSystemType.String);
            }
        }

        public esQueryItem FromQuestionFormID
        {
            get
            {
                return new esQueryItem(this, QuestionDefaultValueMetadata.ColumnNames.FromQuestionFormID, esSystemType.String);
            }
        }

        public esQueryItem FromQuestionGroupID
        {
            get
            {
                return new esQueryItem(this, QuestionDefaultValueMetadata.ColumnNames.FromQuestionGroupID, esSystemType.String);
            }
        }

        public esQueryItem FromQuestionID
        {
            get
            {
                return new esQueryItem(this, QuestionDefaultValueMetadata.ColumnNames.FromQuestionID, esSystemType.String);
            }
        }

        public esQueryItem IsFromCurrentRegistration
        {
            get
            {
                return new esQueryItem(this, QuestionDefaultValueMetadata.ColumnNames.IsFromCurrentRegistration, esSystemType.Boolean);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, QuestionDefaultValueMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, QuestionDefaultValueMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("QuestionDefaultValueCollection")]
    public partial class QuestionDefaultValueCollection : esQuestionDefaultValueCollection, IEnumerable<QuestionDefaultValue>
    {
        public QuestionDefaultValueCollection()
        {

        }

        public static implicit operator List<QuestionDefaultValue>(QuestionDefaultValueCollection coll)
        {
            List<QuestionDefaultValue> list = new List<QuestionDefaultValue>();

            foreach (QuestionDefaultValue emp in coll)
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
                return QuestionDefaultValueMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new QuestionDefaultValueQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new QuestionDefaultValue(row);
        }

        override protected esEntity CreateEntity()
        {
            return new QuestionDefaultValue();
        }

        #endregion

        [BrowsableAttribute(false)]
        public QuestionDefaultValueQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new QuestionDefaultValueQuery();
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
        public bool Load(QuestionDefaultValueQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public QuestionDefaultValue AddNew()
        {
            QuestionDefaultValue entity = base.AddNewEntity() as QuestionDefaultValue;

            return entity;
        }
        public QuestionDefaultValue FindByPrimaryKey(String questionFormID, String questionGroupID, String questionID)
        {
            return base.FindByPrimaryKey(questionFormID, questionGroupID, questionID) as QuestionDefaultValue;
        }

        #region IEnumerable< QuestionDefaultValue> Members

        IEnumerator<QuestionDefaultValue> IEnumerable<QuestionDefaultValue>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as QuestionDefaultValue;
            }
        }

        #endregion

        private QuestionDefaultValueQuery query;
    }


    /// <summary>
    /// Encapsulates the 'QuestionDefaultValue' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("QuestionDefaultValue ({QuestionFormID, QuestionGroupID, QuestionID})")]
    [Serializable]
    public partial class QuestionDefaultValue : esQuestionDefaultValue
    {
        public QuestionDefaultValue()
        {
        }

        public QuestionDefaultValue(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return QuestionDefaultValueMetadata.Meta();
            }
        }

        override protected esQuestionDefaultValueQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new QuestionDefaultValueQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public QuestionDefaultValueQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new QuestionDefaultValueQuery();
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
        public bool Load(QuestionDefaultValueQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private QuestionDefaultValueQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class QuestionDefaultValueQuery : esQuestionDefaultValueQuery
    {
        public QuestionDefaultValueQuery()
        {

        }

        public QuestionDefaultValueQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "QuestionDefaultValueQuery";
        }
    }

    [Serializable]
    public partial class QuestionDefaultValueMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected QuestionDefaultValueMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(QuestionDefaultValueMetadata.ColumnNames.QuestionFormID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = QuestionDefaultValueMetadata.PropertyNames.QuestionFormID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(QuestionDefaultValueMetadata.ColumnNames.QuestionGroupID, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = QuestionDefaultValueMetadata.PropertyNames.QuestionGroupID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(QuestionDefaultValueMetadata.ColumnNames.QuestionID, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = QuestionDefaultValueMetadata.PropertyNames.QuestionID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(QuestionDefaultValueMetadata.ColumnNames.FromQuestionFormID, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = QuestionDefaultValueMetadata.PropertyNames.FromQuestionFormID;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(QuestionDefaultValueMetadata.ColumnNames.FromQuestionGroupID, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = QuestionDefaultValueMetadata.PropertyNames.FromQuestionGroupID;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(QuestionDefaultValueMetadata.ColumnNames.FromQuestionID, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = QuestionDefaultValueMetadata.PropertyNames.FromQuestionID;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(QuestionDefaultValueMetadata.ColumnNames.IsFromCurrentRegistration, 6, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = QuestionDefaultValueMetadata.PropertyNames.IsFromCurrentRegistration;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(QuestionDefaultValueMetadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = QuestionDefaultValueMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(QuestionDefaultValueMetadata.ColumnNames.LastUpdateByUserID, 8, typeof(System.String), esSystemType.String);
            c.PropertyName = QuestionDefaultValueMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public QuestionDefaultValueMetadata Meta()
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
            public const string QuestionFormID = "QuestionFormID";
            public const string QuestionGroupID = "QuestionGroupID";
            public const string QuestionID = "QuestionID";
            public const string FromQuestionFormID = "FromQuestionFormID";
            public const string FromQuestionGroupID = "FromQuestionGroupID";
            public const string FromQuestionID = "FromQuestionID";
            public const string IsFromCurrentRegistration = "IsFromCurrentRegistration";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string QuestionFormID = "QuestionFormID";
            public const string QuestionGroupID = "QuestionGroupID";
            public const string QuestionID = "QuestionID";
            public const string FromQuestionFormID = "FromQuestionFormID";
            public const string FromQuestionGroupID = "FromQuestionGroupID";
            public const string FromQuestionID = "FromQuestionID";
            public const string IsFromCurrentRegistration = "IsFromCurrentRegistration";
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
            lock (typeof(QuestionDefaultValueMetadata))
            {
                if (QuestionDefaultValueMetadata.mapDelegates == null)
                {
                    QuestionDefaultValueMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (QuestionDefaultValueMetadata.meta == null)
                {
                    QuestionDefaultValueMetadata.meta = new QuestionDefaultValueMetadata();
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

                meta.AddTypeMap("QuestionFormID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("QuestionGroupID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("QuestionID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("FromQuestionFormID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("FromQuestionGroupID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("FromQuestionID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsFromCurrentRegistration", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "QuestionDefaultValue";
                meta.Destination = "QuestionDefaultValue";
                meta.spInsert = "proc_QuestionDefaultValueInsert";
                meta.spUpdate = "proc_QuestionDefaultValueUpdate";
                meta.spDelete = "proc_QuestionDefaultValueDelete";
                meta.spLoadAll = "proc_QuestionDefaultValueLoadAll";
                meta.spLoadByPrimaryKey = "proc_QuestionDefaultValueLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private QuestionDefaultValueMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
