/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 01/31/19 12:49:20 PM
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
    abstract public class esQuestionAnswerSelectionLineCollection : esEntityCollectionWAuditLog
    {
        public esQuestionAnswerSelectionLineCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "QuestionAnswerSelectionLineCollection";
        }

        #region Query Logic
        protected void InitQuery(esQuestionAnswerSelectionLineQuery query)
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
            this.InitQuery(query as esQuestionAnswerSelectionLineQuery);
        }
        #endregion

        virtual public QuestionAnswerSelectionLine DetachEntity(QuestionAnswerSelectionLine entity)
        {
            return base.DetachEntity(entity) as QuestionAnswerSelectionLine;
        }

        virtual public QuestionAnswerSelectionLine AttachEntity(QuestionAnswerSelectionLine entity)
        {
            return base.AttachEntity(entity) as QuestionAnswerSelectionLine;
        }

        virtual public void Combine(QuestionAnswerSelectionLineCollection collection)
        {
            base.Combine(collection);
        }

        new public QuestionAnswerSelectionLine this[int index]
        {
            get
            {
                return base[index] as QuestionAnswerSelectionLine;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(QuestionAnswerSelectionLine);
        }
    }

    [Serializable]
    abstract public class esQuestionAnswerSelectionLine : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esQuestionAnswerSelectionLineQuery GetDynamicQuery()
        {
            return null;
        }

        public esQuestionAnswerSelectionLine()
        {
        }

        public esQuestionAnswerSelectionLine(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String questionAnswerSelectionID, String questionAnswerSelectionLineID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(questionAnswerSelectionID, questionAnswerSelectionLineID);
            else
                return LoadByPrimaryKeyStoredProcedure(questionAnswerSelectionID, questionAnswerSelectionLineID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String questionAnswerSelectionID, String questionAnswerSelectionLineID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(questionAnswerSelectionID, questionAnswerSelectionLineID);
            else
                return LoadByPrimaryKeyStoredProcedure(questionAnswerSelectionID, questionAnswerSelectionLineID);
        }

        private bool LoadByPrimaryKeyDynamic(String questionAnswerSelectionID, String questionAnswerSelectionLineID)
        {
            esQuestionAnswerSelectionLineQuery query = this.GetDynamicQuery();
            query.Where(query.QuestionAnswerSelectionID == questionAnswerSelectionID, query.QuestionAnswerSelectionLineID == questionAnswerSelectionLineID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String questionAnswerSelectionID, String questionAnswerSelectionLineID)
        {
            esParameters parms = new esParameters();
            parms.Add("QuestionAnswerSelectionID", questionAnswerSelectionID);
            parms.Add("QuestionAnswerSelectionLineID", questionAnswerSelectionLineID);
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
                        case "QuestionAnswerSelectionID": this.str.QuestionAnswerSelectionID = (string)value; break;
                        case "QuestionAnswerSelectionLineID": this.str.QuestionAnswerSelectionLineID = (string)value; break;
                        case "QuestionAnswerSelectionLineText": this.str.QuestionAnswerSelectionLineText = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "Score": this.str.Score = (string)value; break;
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
                        case "Score":

                            if (value == null || value is System.Decimal)
                                this.Score = (System.Decimal?)value;
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
        /// Maps to QuestionAnswerSelectionLine.QuestionAnswerSelectionID
        /// </summary>
        virtual public System.String QuestionAnswerSelectionID
        {
            get
            {
                return base.GetSystemString(QuestionAnswerSelectionLineMetadata.ColumnNames.QuestionAnswerSelectionID);
            }

            set
            {
                base.SetSystemString(QuestionAnswerSelectionLineMetadata.ColumnNames.QuestionAnswerSelectionID, value);
            }
        }
        /// <summary>
        /// Maps to QuestionAnswerSelectionLine.QuestionAnswerSelectionLineID
        /// </summary>
        virtual public System.String QuestionAnswerSelectionLineID
        {
            get
            {
                return base.GetSystemString(QuestionAnswerSelectionLineMetadata.ColumnNames.QuestionAnswerSelectionLineID);
            }

            set
            {
                base.SetSystemString(QuestionAnswerSelectionLineMetadata.ColumnNames.QuestionAnswerSelectionLineID, value);
            }
        }
        /// <summary>
        /// Maps to QuestionAnswerSelectionLine.QuestionAnswerSelectionLineText
        /// </summary>
        virtual public System.String QuestionAnswerSelectionLineText
        {
            get
            {
                return base.GetSystemString(QuestionAnswerSelectionLineMetadata.ColumnNames.QuestionAnswerSelectionLineText);
            }

            set
            {
                base.SetSystemString(QuestionAnswerSelectionLineMetadata.ColumnNames.QuestionAnswerSelectionLineText, value);
            }
        }
        /// <summary>
        /// Maps to QuestionAnswerSelectionLine.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(QuestionAnswerSelectionLineMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(QuestionAnswerSelectionLineMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to QuestionAnswerSelectionLine.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(QuestionAnswerSelectionLineMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(QuestionAnswerSelectionLineMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to QuestionAnswerSelectionLine.Score
        /// </summary>
        virtual public System.Decimal? Score
        {
            get
            {
                return base.GetSystemDecimal(QuestionAnswerSelectionLineMetadata.ColumnNames.Score);
            }

            set
            {
                base.SetSystemDecimal(QuestionAnswerSelectionLineMetadata.ColumnNames.Score, value);
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
            public esStrings(esQuestionAnswerSelectionLine entity)
            {
                this.entity = entity;
            }
            public System.String QuestionAnswerSelectionID
            {
                get
                {
                    System.String data = entity.QuestionAnswerSelectionID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.QuestionAnswerSelectionID = null;
                    else entity.QuestionAnswerSelectionID = Convert.ToString(value);
                }
            }
            public System.String QuestionAnswerSelectionLineID
            {
                get
                {
                    System.String data = entity.QuestionAnswerSelectionLineID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.QuestionAnswerSelectionLineID = null;
                    else entity.QuestionAnswerSelectionLineID = Convert.ToString(value);
                }
            }
            public System.String QuestionAnswerSelectionLineText
            {
                get
                {
                    System.String data = entity.QuestionAnswerSelectionLineText;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.QuestionAnswerSelectionLineText = null;
                    else entity.QuestionAnswerSelectionLineText = Convert.ToString(value);
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
            public System.String Score
            {
                get
                {
                    System.Decimal? data = entity.Score;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Score = null;
                    else entity.Score = Convert.ToDecimal(value);
                }
            }
            private esQuestionAnswerSelectionLine entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esQuestionAnswerSelectionLineQuery query)
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
                throw new Exception("esQuestionAnswerSelectionLine can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class QuestionAnswerSelectionLine : esQuestionAnswerSelectionLine
    {
    }

    [Serializable]
    abstract public class esQuestionAnswerSelectionLineQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return QuestionAnswerSelectionLineMetadata.Meta();
            }
        }

        public esQueryItem QuestionAnswerSelectionID
        {
            get
            {
                return new esQueryItem(this, QuestionAnswerSelectionLineMetadata.ColumnNames.QuestionAnswerSelectionID, esSystemType.String);
            }
        }

        public esQueryItem QuestionAnswerSelectionLineID
        {
            get
            {
                return new esQueryItem(this, QuestionAnswerSelectionLineMetadata.ColumnNames.QuestionAnswerSelectionLineID, esSystemType.String);
            }
        }

        public esQueryItem QuestionAnswerSelectionLineText
        {
            get
            {
                return new esQueryItem(this, QuestionAnswerSelectionLineMetadata.ColumnNames.QuestionAnswerSelectionLineText, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, QuestionAnswerSelectionLineMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, QuestionAnswerSelectionLineMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem Score
        {
            get
            {
                return new esQueryItem(this, QuestionAnswerSelectionLineMetadata.ColumnNames.Score, esSystemType.Decimal);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("QuestionAnswerSelectionLineCollection")]
    public partial class QuestionAnswerSelectionLineCollection : esQuestionAnswerSelectionLineCollection, IEnumerable<QuestionAnswerSelectionLine>
    {
        public QuestionAnswerSelectionLineCollection()
        {

        }

        public static implicit operator List<QuestionAnswerSelectionLine>(QuestionAnswerSelectionLineCollection coll)
        {
            List<QuestionAnswerSelectionLine> list = new List<QuestionAnswerSelectionLine>();

            foreach (QuestionAnswerSelectionLine emp in coll)
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
                return QuestionAnswerSelectionLineMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new QuestionAnswerSelectionLineQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new QuestionAnswerSelectionLine(row);
        }

        override protected esEntity CreateEntity()
        {
            return new QuestionAnswerSelectionLine();
        }

        #endregion

        [BrowsableAttribute(false)]
        public QuestionAnswerSelectionLineQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new QuestionAnswerSelectionLineQuery();
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
        public bool Load(QuestionAnswerSelectionLineQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public QuestionAnswerSelectionLine AddNew()
        {
            QuestionAnswerSelectionLine entity = base.AddNewEntity() as QuestionAnswerSelectionLine;

            return entity;
        }
        public QuestionAnswerSelectionLine FindByPrimaryKey(String questionAnswerSelectionID, String questionAnswerSelectionLineID)
        {
            return base.FindByPrimaryKey(questionAnswerSelectionID, questionAnswerSelectionLineID) as QuestionAnswerSelectionLine;
        }

        #region IEnumerable< QuestionAnswerSelectionLine> Members

        IEnumerator<QuestionAnswerSelectionLine> IEnumerable<QuestionAnswerSelectionLine>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as QuestionAnswerSelectionLine;
            }
        }

        #endregion

        private QuestionAnswerSelectionLineQuery query;
    }


    /// <summary>
    /// Encapsulates the 'QuestionAnswerSelectionLine' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("QuestionAnswerSelectionLine ({QuestionAnswerSelectionID, QuestionAnswerSelectionLineID})")]
    [Serializable]
    public partial class QuestionAnswerSelectionLine : esQuestionAnswerSelectionLine
    {
        public QuestionAnswerSelectionLine()
        {
        }

        public QuestionAnswerSelectionLine(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return QuestionAnswerSelectionLineMetadata.Meta();
            }
        }

        override protected esQuestionAnswerSelectionLineQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new QuestionAnswerSelectionLineQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public QuestionAnswerSelectionLineQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new QuestionAnswerSelectionLineQuery();
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
        public bool Load(QuestionAnswerSelectionLineQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private QuestionAnswerSelectionLineQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class QuestionAnswerSelectionLineQuery : esQuestionAnswerSelectionLineQuery
    {
        public QuestionAnswerSelectionLineQuery()
        {

        }

        public QuestionAnswerSelectionLineQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "QuestionAnswerSelectionLineQuery";
        }
    }

    [Serializable]
    public partial class QuestionAnswerSelectionLineMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected QuestionAnswerSelectionLineMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(QuestionAnswerSelectionLineMetadata.ColumnNames.QuestionAnswerSelectionID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = QuestionAnswerSelectionLineMetadata.PropertyNames.QuestionAnswerSelectionID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 100;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(QuestionAnswerSelectionLineMetadata.ColumnNames.QuestionAnswerSelectionLineID, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = QuestionAnswerSelectionLineMetadata.PropertyNames.QuestionAnswerSelectionLineID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 100;
            _columns.Add(c);

            c = new esColumnMetadata(QuestionAnswerSelectionLineMetadata.ColumnNames.QuestionAnswerSelectionLineText, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = QuestionAnswerSelectionLineMetadata.PropertyNames.QuestionAnswerSelectionLineText;
            c.CharacterMaxLength = 200;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(QuestionAnswerSelectionLineMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = QuestionAnswerSelectionLineMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(QuestionAnswerSelectionLineMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = QuestionAnswerSelectionLineMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 40;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(QuestionAnswerSelectionLineMetadata.ColumnNames.Score, 5, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = QuestionAnswerSelectionLineMetadata.PropertyNames.Score;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public QuestionAnswerSelectionLineMetadata Meta()
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
            public const string QuestionAnswerSelectionID = "QuestionAnswerSelectionID";
            public const string QuestionAnswerSelectionLineID = "QuestionAnswerSelectionLineID";
            public const string QuestionAnswerSelectionLineText = "QuestionAnswerSelectionLineText";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string Score = "Score";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string QuestionAnswerSelectionID = "QuestionAnswerSelectionID";
            public const string QuestionAnswerSelectionLineID = "QuestionAnswerSelectionLineID";
            public const string QuestionAnswerSelectionLineText = "QuestionAnswerSelectionLineText";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string Score = "Score";
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
            lock (typeof(QuestionAnswerSelectionLineMetadata))
            {
                if (QuestionAnswerSelectionLineMetadata.mapDelegates == null)
                {
                    QuestionAnswerSelectionLineMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (QuestionAnswerSelectionLineMetadata.meta == null)
                {
                    QuestionAnswerSelectionLineMetadata.meta = new QuestionAnswerSelectionLineMetadata();
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

                meta.AddTypeMap("QuestionAnswerSelectionID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("QuestionAnswerSelectionLineID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("QuestionAnswerSelectionLineText", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Score", new esTypeMap("numeric", "System.Decimal"));


                meta.Source = "QuestionAnswerSelectionLine";
                meta.Destination = "QuestionAnswerSelectionLine";
                meta.spInsert = "proc_QuestionAnswerSelectionLineInsert";
                meta.spUpdate = "proc_QuestionAnswerSelectionLineUpdate";
                meta.spDelete = "proc_QuestionAnswerSelectionLineDelete";
                meta.spLoadAll = "proc_QuestionAnswerSelectionLineLoadAll";
                meta.spLoadByPrimaryKey = "proc_QuestionAnswerSelectionLineLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private QuestionAnswerSelectionLineMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
