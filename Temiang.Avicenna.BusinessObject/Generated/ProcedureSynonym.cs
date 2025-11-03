/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 2/27/2025 9:39:04 AM
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
    abstract public class esProcedureSynonymCollection : esEntityCollectionWAuditLog
    {
        public esProcedureSynonymCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "ProcedureSynonymCollection";
        }

        #region Query Logic
        protected void InitQuery(esProcedureSynonymQuery query)
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
            this.InitQuery(query as esProcedureSynonymQuery);
        }
        #endregion

        virtual public ProcedureSynonym DetachEntity(ProcedureSynonym entity)
        {
            return base.DetachEntity(entity) as ProcedureSynonym;
        }

        virtual public ProcedureSynonym AttachEntity(ProcedureSynonym entity)
        {
            return base.AttachEntity(entity) as ProcedureSynonym;
        }

        virtual public void Combine(ProcedureSynonymCollection collection)
        {
            base.Combine(collection);
        }

        new public ProcedureSynonym this[int index]
        {
            get
            {
                return base[index] as ProcedureSynonym;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(ProcedureSynonym);
        }
    }

    [Serializable]
    abstract public class esProcedureSynonym : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esProcedureSynonymQuery GetDynamicQuery()
        {
            return null;
        }

        public esProcedureSynonym()
        {
        }

        public esProcedureSynonym(DataRow row)
            : base(row)
        {
        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String procedureID, String sequenceNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(procedureID, sequenceNo);
            else
                return LoadByPrimaryKeyStoredProcedure(procedureID, sequenceNo);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String procedureID, String sequenceNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(procedureID, sequenceNo);
            else
                return LoadByPrimaryKeyStoredProcedure(procedureID, sequenceNo);
        }

        private bool LoadByPrimaryKeyDynamic(String procedureID, String sequenceNo)
        {
            esProcedureSynonymQuery query = this.GetDynamicQuery();
            query.Where(query.ProcedureID == procedureID, query.SequenceNo == sequenceNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String procedureID, String sequenceNo)
        {
            esParameters parms = new esParameters();
            parms.Add("ProcedureID", procedureID);
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
                        case "ProcedureID": this.str.ProcedureID = (string)value; break;
                        case "SequenceNo": this.str.SequenceNo = (string)value; break;
                        case "SynonymText": this.str.SynonymText = (string)value; break;
                        case "CreateDateTime": this.str.CreateDateTime = (string)value; break;
                        case "CreateByUserID": this.str.CreateByUserID = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "CreateDateTime":

                            if (value == null || value is System.DateTime)
                                this.CreateDateTime = (System.DateTime?)value;
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
        /// Maps to ProcedureSynonym.ProcedureID
        /// </summary>
        virtual public System.String ProcedureID
        {
            get
            {
                return base.GetSystemString(ProcedureSynonymMetadata.ColumnNames.ProcedureID);
            }

            set
            {
                base.SetSystemString(ProcedureSynonymMetadata.ColumnNames.ProcedureID, value);
            }
        }
        /// <summary>
        /// Maps to ProcedureSynonym.SequenceNo
        /// </summary>
        virtual public System.String SequenceNo
        {
            get
            {
                return base.GetSystemString(ProcedureSynonymMetadata.ColumnNames.SequenceNo);
            }

            set
            {
                base.SetSystemString(ProcedureSynonymMetadata.ColumnNames.SequenceNo, value);
            }
        }
        /// <summary>
        /// Maps to ProcedureSynonym.SynonymText
        /// </summary>
        virtual public System.String SynonymText
        {
            get
            {
                return base.GetSystemString(ProcedureSynonymMetadata.ColumnNames.SynonymText);
            }

            set
            {
                base.SetSystemString(ProcedureSynonymMetadata.ColumnNames.SynonymText, value);
            }
        }
        /// <summary>
        /// Maps to ProcedureSynonym.CreateDateTime
        /// </summary>
        virtual public System.DateTime? CreateDateTime
        {
            get
            {
                return base.GetSystemDateTime(ProcedureSynonymMetadata.ColumnNames.CreateDateTime);
            }

            set
            {
                base.SetSystemDateTime(ProcedureSynonymMetadata.ColumnNames.CreateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to ProcedureSynonym.CreateByUserID
        /// </summary>
        virtual public System.String CreateByUserID
        {
            get
            {
                return base.GetSystemString(ProcedureSynonymMetadata.ColumnNames.CreateByUserID);
            }

            set
            {
                base.SetSystemString(ProcedureSynonymMetadata.ColumnNames.CreateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to ProcedureSynonym.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(ProcedureSynonymMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(ProcedureSynonymMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to ProcedureSynonym.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(ProcedureSynonymMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(ProcedureSynonymMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esProcedureSynonym entity)
            {
                this.entity = entity;
            }
            public System.String ProcedureID
            {
                get
                {
                    System.String data = entity.ProcedureID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ProcedureID = null;
                    else entity.ProcedureID = Convert.ToString(value);
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
            public System.String SynonymText
            {
                get
                {
                    System.String data = entity.SynonymText;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SynonymText = null;
                    else entity.SynonymText = Convert.ToString(value);
                }
            }
            public System.String CreateDateTime
            {
                get
                {
                    System.DateTime? data = entity.CreateDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CreateDateTime = null;
                    else entity.CreateDateTime = Convert.ToDateTime(value);
                }
            }
            public System.String CreateByUserID
            {
                get
                {
                    System.String data = entity.CreateByUserID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CreateByUserID = null;
                    else entity.CreateByUserID = Convert.ToString(value);
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
            private esProcedureSynonym entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esProcedureSynonymQuery query)
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
                throw new Exception("esProcedureSynonym can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class ProcedureSynonym : esProcedureSynonym
    {
    }

    [Serializable]
    abstract public class esProcedureSynonymQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return ProcedureSynonymMetadata.Meta();
            }
        }

        public esQueryItem ProcedureID
        {
            get
            {
                return new esQueryItem(this, ProcedureSynonymMetadata.ColumnNames.ProcedureID, esSystemType.String);
            }
        }

        public esQueryItem SequenceNo
        {
            get
            {
                return new esQueryItem(this, ProcedureSynonymMetadata.ColumnNames.SequenceNo, esSystemType.String);
            }
        }

        public esQueryItem SynonymText
        {
            get
            {
                return new esQueryItem(this, ProcedureSynonymMetadata.ColumnNames.SynonymText, esSystemType.String);
            }
        }

        public esQueryItem CreateDateTime
        {
            get
            {
                return new esQueryItem(this, ProcedureSynonymMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem CreateByUserID
        {
            get
            {
                return new esQueryItem(this, ProcedureSynonymMetadata.ColumnNames.CreateByUserID, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, ProcedureSynonymMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, ProcedureSynonymMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("ProcedureSynonymCollection")]
    public partial class ProcedureSynonymCollection : esProcedureSynonymCollection, IEnumerable<ProcedureSynonym>
    {
        public ProcedureSynonymCollection()
        {

        }

        public static implicit operator List<ProcedureSynonym>(ProcedureSynonymCollection coll)
        {
            List<ProcedureSynonym> list = new List<ProcedureSynonym>();

            foreach (ProcedureSynonym emp in coll)
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
                return ProcedureSynonymMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ProcedureSynonymQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new ProcedureSynonym(row);
        }

        override protected esEntity CreateEntity()
        {
            return new ProcedureSynonym();
        }

        #endregion

        [BrowsableAttribute(false)]
        public ProcedureSynonymQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ProcedureSynonymQuery();
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
        public bool Load(ProcedureSynonymQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public ProcedureSynonym AddNew()
        {
            ProcedureSynonym entity = base.AddNewEntity() as ProcedureSynonym;

            return entity;
        }
        public ProcedureSynonym FindByPrimaryKey(String procedureID, String sequenceNo)
        {
            return base.FindByPrimaryKey(procedureID, sequenceNo) as ProcedureSynonym;
        }

        #region IEnumerable< ProcedureSynonym> Members

        IEnumerator<ProcedureSynonym> IEnumerable<ProcedureSynonym>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as ProcedureSynonym;
            }
        }

        #endregion

        private ProcedureSynonymQuery query;
    }


    /// <summary>
    /// Encapsulates the 'ProcedureSynonym' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("ProcedureSynonym ({ProcedureID, SequenceNo})")]
    [Serializable]
    public partial class ProcedureSynonym : esProcedureSynonym
    {
        public ProcedureSynonym()
        {
        }

        public ProcedureSynonym(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return ProcedureSynonymMetadata.Meta();
            }
        }

        override protected esProcedureSynonymQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ProcedureSynonymQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public ProcedureSynonymQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ProcedureSynonymQuery();
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
        public bool Load(ProcedureSynonymQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private ProcedureSynonymQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class ProcedureSynonymQuery : esProcedureSynonymQuery
    {
        public ProcedureSynonymQuery()
        {

        }

        public ProcedureSynonymQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "ProcedureSynonymQuery";
        }
    }

    [Serializable]
    public partial class ProcedureSynonymMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected ProcedureSynonymMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(ProcedureSynonymMetadata.ColumnNames.ProcedureID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = ProcedureSynonymMetadata.PropertyNames.ProcedureID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(ProcedureSynonymMetadata.ColumnNames.SequenceNo, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = ProcedureSynonymMetadata.PropertyNames.SequenceNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 4;
            _columns.Add(c);

            c = new esColumnMetadata(ProcedureSynonymMetadata.ColumnNames.SynonymText, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = ProcedureSynonymMetadata.PropertyNames.SynonymText;
            c.CharacterMaxLength = 200;
            _columns.Add(c);

            c = new esColumnMetadata(ProcedureSynonymMetadata.ColumnNames.CreateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ProcedureSynonymMetadata.PropertyNames.CreateDateTime;
            _columns.Add(c);

            c = new esColumnMetadata(ProcedureSynonymMetadata.ColumnNames.CreateByUserID, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = ProcedureSynonymMetadata.PropertyNames.CreateByUserID;
            c.CharacterMaxLength = 40;
            _columns.Add(c);

            c = new esColumnMetadata(ProcedureSynonymMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ProcedureSynonymMetadata.PropertyNames.LastUpdateDateTime;
            _columns.Add(c);

            c = new esColumnMetadata(ProcedureSynonymMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = ProcedureSynonymMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 40;
            _columns.Add(c);


        }
        #endregion

        static public ProcedureSynonymMetadata Meta()
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
            public const string ProcedureID = "ProcedureID";
            public const string SequenceNo = "SequenceNo";
            public const string SynonymText = "SynonymText";
            public const string CreateDateTime = "CreateDateTime";
            public const string CreateByUserID = "CreateByUserID";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string ProcedureID = "ProcedureID";
            public const string SequenceNo = "SequenceNo";
            public const string SynonymText = "SynonymText";
            public const string CreateDateTime = "CreateDateTime";
            public const string CreateByUserID = "CreateByUserID";
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
            lock (typeof(ProcedureSynonymMetadata))
            {
                if (ProcedureSynonymMetadata.mapDelegates == null)
                {
                    ProcedureSynonymMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (ProcedureSynonymMetadata.meta == null)
                {
                    ProcedureSynonymMetadata.meta = new ProcedureSynonymMetadata();
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

                meta.AddTypeMap("ProcedureID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SequenceNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SynonymText", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "ProcedureSynonym";
                meta.Destination = "ProcedureSynonym";
                meta.spInsert = "proc_ProcedureSynonymInsert";
                meta.spUpdate = "proc_ProcedureSynonymUpdate";
                meta.spDelete = "proc_ProcedureSynonymDelete";
                meta.spLoadAll = "proc_ProcedureSynonymLoadAll";
                meta.spLoadByPrimaryKey = "proc_ProcedureSynonymLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private ProcedureSynonymMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}