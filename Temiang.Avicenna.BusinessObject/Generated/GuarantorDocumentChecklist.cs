/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 9/12/2019 9:16:02 AM
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
    abstract public class esGuarantorDocumentChecklistCollection : esEntityCollectionWAuditLog
    {
        public esGuarantorDocumentChecklistCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "GuarantorDocumentChecklistCollection";
        }

        #region Query Logic
        protected void InitQuery(esGuarantorDocumentChecklistQuery query)
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
            this.InitQuery(query as esGuarantorDocumentChecklistQuery);
        }
        #endregion

        virtual public GuarantorDocumentChecklist DetachEntity(GuarantorDocumentChecklist entity)
        {
            return base.DetachEntity(entity) as GuarantorDocumentChecklist;
        }

        virtual public GuarantorDocumentChecklist AttachEntity(GuarantorDocumentChecklist entity)
        {
            return base.AttachEntity(entity) as GuarantorDocumentChecklist;
        }

        virtual public void Combine(GuarantorDocumentChecklistCollection collection)
        {
            base.Combine(collection);
        }

        new public GuarantorDocumentChecklist this[int index]
        {
            get
            {
                return base[index] as GuarantorDocumentChecklist;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(GuarantorDocumentChecklist);
        }
    }

    [Serializable]
    abstract public class esGuarantorDocumentChecklist : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esGuarantorDocumentChecklistQuery GetDynamicQuery()
        {
            return null;
        }

        public esGuarantorDocumentChecklist()
        {
        }

        public esGuarantorDocumentChecklist(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String guarantorID, String sRRegistrationType)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(guarantorID, sRRegistrationType);
            else
                return LoadByPrimaryKeyStoredProcedure(guarantorID, sRRegistrationType);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String guarantorID, String sRRegistrationType)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(guarantorID, sRRegistrationType);
            else
                return LoadByPrimaryKeyStoredProcedure(guarantorID, sRRegistrationType);
        }

        private bool LoadByPrimaryKeyDynamic(String guarantorID, String sRRegistrationType)
        {
            esGuarantorDocumentChecklistQuery query = this.GetDynamicQuery();
            query.Where(query.GuarantorID == guarantorID, query.SRRegistrationType == sRRegistrationType);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String guarantorID, String sRRegistrationType)
        {
            esParameters parms = new esParameters();
            parms.Add("GuarantorID", guarantorID);
            parms.Add("SRRegistrationType", sRRegistrationType);
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
                        case "GuarantorID": this.str.GuarantorID = (string)value; break;
                        case "SRRegistrationType": this.str.SRRegistrationType = (string)value; break;
                        case "SRDocumentChecklist": this.str.SRDocumentChecklist = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
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
        /// Maps to GuarantorDocumentChecklist.GuarantorID
        /// </summary>
        virtual public System.String GuarantorID
        {
            get
            {
                return base.GetSystemString(GuarantorDocumentChecklistMetadata.ColumnNames.GuarantorID);
            }

            set
            {
                base.SetSystemString(GuarantorDocumentChecklistMetadata.ColumnNames.GuarantorID, value);
            }
        }
        /// <summary>
        /// Maps to GuarantorDocumentChecklist.SRRegistrationType
        /// </summary>
        virtual public System.String SRRegistrationType
        {
            get
            {
                return base.GetSystemString(GuarantorDocumentChecklistMetadata.ColumnNames.SRRegistrationType);
            }

            set
            {
                base.SetSystemString(GuarantorDocumentChecklistMetadata.ColumnNames.SRRegistrationType, value);
            }
        }
        /// <summary>
        /// Maps to GuarantorDocumentChecklist.SRDocumentChecklist
        /// </summary>
        virtual public System.String SRDocumentChecklist
        {
            get
            {
                return base.GetSystemString(GuarantorDocumentChecklistMetadata.ColumnNames.SRDocumentChecklist);
            }

            set
            {
                base.SetSystemString(GuarantorDocumentChecklistMetadata.ColumnNames.SRDocumentChecklist, value);
            }
        }
        /// <summary>
        /// Maps to GuarantorDocumentChecklist.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(GuarantorDocumentChecklistMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(GuarantorDocumentChecklistMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to GuarantorDocumentChecklist.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(GuarantorDocumentChecklistMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(GuarantorDocumentChecklistMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esGuarantorDocumentChecklist entity)
            {
                this.entity = entity;
            }
            public System.String GuarantorID
            {
                get
                {
                    System.String data = entity.GuarantorID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.GuarantorID = null;
                    else entity.GuarantorID = Convert.ToString(value);
                }
            }
            public System.String SRRegistrationType
            {
                get
                {
                    System.String data = entity.SRRegistrationType;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRRegistrationType = null;
                    else entity.SRRegistrationType = Convert.ToString(value);
                }
            }
            public System.String SRDocumentChecklist
            {
                get
                {
                    System.String data = entity.SRDocumentChecklist;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRDocumentChecklist = null;
                    else entity.SRDocumentChecklist = Convert.ToString(value);
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
            private esGuarantorDocumentChecklist entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esGuarantorDocumentChecklistQuery query)
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
                throw new Exception("esGuarantorDocumentChecklist can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class GuarantorDocumentChecklist : esGuarantorDocumentChecklist
    {
    }

    [Serializable]
    abstract public class esGuarantorDocumentChecklistQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return GuarantorDocumentChecklistMetadata.Meta();
            }
        }

        public esQueryItem GuarantorID
        {
            get
            {
                return new esQueryItem(this, GuarantorDocumentChecklistMetadata.ColumnNames.GuarantorID, esSystemType.String);
            }
        }

        public esQueryItem SRRegistrationType
        {
            get
            {
                return new esQueryItem(this, GuarantorDocumentChecklistMetadata.ColumnNames.SRRegistrationType, esSystemType.String);
            }
        }

        public esQueryItem SRDocumentChecklist
        {
            get
            {
                return new esQueryItem(this, GuarantorDocumentChecklistMetadata.ColumnNames.SRDocumentChecklist, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, GuarantorDocumentChecklistMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, GuarantorDocumentChecklistMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("GuarantorDocumentChecklistCollection")]
    public partial class GuarantorDocumentChecklistCollection : esGuarantorDocumentChecklistCollection, IEnumerable<GuarantorDocumentChecklist>
    {
        public GuarantorDocumentChecklistCollection()
        {

        }

        public static implicit operator List<GuarantorDocumentChecklist>(GuarantorDocumentChecklistCollection coll)
        {
            List<GuarantorDocumentChecklist> list = new List<GuarantorDocumentChecklist>();

            foreach (GuarantorDocumentChecklist emp in coll)
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
                return GuarantorDocumentChecklistMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new GuarantorDocumentChecklistQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new GuarantorDocumentChecklist(row);
        }

        override protected esEntity CreateEntity()
        {
            return new GuarantorDocumentChecklist();
        }

        #endregion

        [BrowsableAttribute(false)]
        public GuarantorDocumentChecklistQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new GuarantorDocumentChecklistQuery();
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
        public bool Load(GuarantorDocumentChecklistQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public GuarantorDocumentChecklist AddNew()
        {
            GuarantorDocumentChecklist entity = base.AddNewEntity() as GuarantorDocumentChecklist;

            return entity;
        }
        public GuarantorDocumentChecklist FindByPrimaryKey(String guarantorID, String sRRegistrationType)
        {
            return base.FindByPrimaryKey(guarantorID, sRRegistrationType) as GuarantorDocumentChecklist;
        }

        #region IEnumerable< GuarantorDocumentChecklist> Members

        IEnumerator<GuarantorDocumentChecklist> IEnumerable<GuarantorDocumentChecklist>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as GuarantorDocumentChecklist;
            }
        }

        #endregion

        private GuarantorDocumentChecklistQuery query;
    }


    /// <summary>
    /// Encapsulates the 'GuarantorDocumentChecklist' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("GuarantorDocumentChecklist ({GuarantorID, SRRegistrationType})")]
    [Serializable]
    public partial class GuarantorDocumentChecklist : esGuarantorDocumentChecklist
    {
        public GuarantorDocumentChecklist()
        {
        }

        public GuarantorDocumentChecklist(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return GuarantorDocumentChecklistMetadata.Meta();
            }
        }

        override protected esGuarantorDocumentChecklistQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new GuarantorDocumentChecklistQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public GuarantorDocumentChecklistQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new GuarantorDocumentChecklistQuery();
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
        public bool Load(GuarantorDocumentChecklistQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private GuarantorDocumentChecklistQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class GuarantorDocumentChecklistQuery : esGuarantorDocumentChecklistQuery
    {
        public GuarantorDocumentChecklistQuery()
        {

        }

        public GuarantorDocumentChecklistQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "GuarantorDocumentChecklistQuery";
        }
    }

    [Serializable]
    public partial class GuarantorDocumentChecklistMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected GuarantorDocumentChecklistMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(GuarantorDocumentChecklistMetadata.ColumnNames.GuarantorID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = GuarantorDocumentChecklistMetadata.PropertyNames.GuarantorID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(GuarantorDocumentChecklistMetadata.ColumnNames.SRRegistrationType, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = GuarantorDocumentChecklistMetadata.PropertyNames.SRRegistrationType;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(GuarantorDocumentChecklistMetadata.ColumnNames.SRDocumentChecklist, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = GuarantorDocumentChecklistMetadata.PropertyNames.SRDocumentChecklist;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(GuarantorDocumentChecklistMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = GuarantorDocumentChecklistMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(GuarantorDocumentChecklistMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = GuarantorDocumentChecklistMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public GuarantorDocumentChecklistMetadata Meta()
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
            public const string GuarantorID = "GuarantorID";
            public const string SRRegistrationType = "SRRegistrationType";
            public const string SRDocumentChecklist = "SRDocumentChecklist";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string GuarantorID = "GuarantorID";
            public const string SRRegistrationType = "SRRegistrationType";
            public const string SRDocumentChecklist = "SRDocumentChecklist";
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
            lock (typeof(GuarantorDocumentChecklistMetadata))
            {
                if (GuarantorDocumentChecklistMetadata.mapDelegates == null)
                {
                    GuarantorDocumentChecklistMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (GuarantorDocumentChecklistMetadata.meta == null)
                {
                    GuarantorDocumentChecklistMetadata.meta = new GuarantorDocumentChecklistMetadata();
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

                meta.AddTypeMap("GuarantorID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRRegistrationType", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRDocumentChecklist", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "GuarantorDocumentChecklist";
                meta.Destination = "GuarantorDocumentChecklist";
                meta.spInsert = "proc_GuarantorDocumentChecklistInsert";
                meta.spUpdate = "proc_GuarantorDocumentChecklistUpdate";
                meta.spDelete = "proc_GuarantorDocumentChecklistDelete";
                meta.spLoadAll = "proc_GuarantorDocumentChecklistLoadAll";
                meta.spLoadByPrimaryKey = "proc_GuarantorDocumentChecklistLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private GuarantorDocumentChecklistMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
