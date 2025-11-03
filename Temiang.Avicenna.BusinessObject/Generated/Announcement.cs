/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/6/2023 10:31:13 AM
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
    abstract public class esAnnouncementCollection : esEntityCollectionWAuditLog
    {
        public esAnnouncementCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "AnnouncementCollection";
        }

        #region Query Logic
        protected void InitQuery(esAnnouncementQuery query)
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
            this.InitQuery(query as esAnnouncementQuery);
        }
        #endregion

        virtual public Announcement DetachEntity(Announcement entity)
        {
            return base.DetachEntity(entity) as Announcement;
        }

        virtual public Announcement AttachEntity(Announcement entity)
        {
            return base.AttachEntity(entity) as Announcement;
        }

        virtual public void Combine(AnnouncementCollection collection)
        {
            base.Combine(collection);
        }

        new public Announcement this[int index]
        {
            get
            {
                return base[index] as Announcement;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(Announcement);
        }
    }

    [Serializable]
    abstract public class esAnnouncement : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esAnnouncementQuery GetDynamicQuery()
        {
            return null;
        }

        public esAnnouncement()
        {
        }

        public esAnnouncement(DataRow row)
            : base(row)
        {
        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String announcementNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(announcementNo);
            else
                return LoadByPrimaryKeyStoredProcedure(announcementNo);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String announcementNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(announcementNo);
            else
                return LoadByPrimaryKeyStoredProcedure(announcementNo);
        }

        private bool LoadByPrimaryKeyDynamic(String announcementNo)
        {
            esAnnouncementQuery query = this.GetDynamicQuery();
            query.Where(query.AnnouncementNo == announcementNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String announcementNo)
        {
            esParameters parms = new esParameters();
            parms.Add("AnnouncementNo", announcementNo);
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
                        case "AnnouncementNo": this.str.AnnouncementNo = (string)value; break;
                        case "AnnouncementStartDate": this.str.AnnouncementStartDate = (string)value; break;
                        case "AnnouncementEndDate": this.str.AnnouncementEndDate = (string)value; break;
                        case "AnnouncementTitle": this.str.AnnouncementTitle = (string)value; break;
                        case "AnnouncementDesc": this.str.AnnouncementDesc = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "IsActive": this.str.IsActive = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "AnnouncementStartDate":

                            if (value == null || value is System.DateTime)
                                this.AnnouncementStartDate = (System.DateTime?)value;
                            break;
                        case "AnnouncementEndDate":

                            if (value == null || value is System.DateTime)
                                this.AnnouncementEndDate = (System.DateTime?)value;
                            break;
                        case "LastUpdateDateTime":

                            if (value == null || value is System.DateTime)
                                this.LastUpdateDateTime = (System.DateTime?)value;
                            break;
                        case "IsActive":

                            if (value == null || value is System.Boolean)
                                this.IsActive = (System.Boolean?)value;
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
        /// Maps to Announcement.AnnouncementNo
        /// </summary>
        virtual public System.String AnnouncementNo
        {
            get
            {
                return base.GetSystemString(AnnouncementMetadata.ColumnNames.AnnouncementNo);
            }

            set
            {
                base.SetSystemString(AnnouncementMetadata.ColumnNames.AnnouncementNo, value);
            }
        }
        /// <summary>
        /// Maps to Announcement.AnnouncementStartDate
        /// </summary>
        virtual public System.DateTime? AnnouncementStartDate
        {
            get
            {
                return base.GetSystemDateTime(AnnouncementMetadata.ColumnNames.AnnouncementStartDate);
            }

            set
            {
                base.SetSystemDateTime(AnnouncementMetadata.ColumnNames.AnnouncementStartDate, value);
            }
        }
        /// <summary>
        /// Maps to Announcement.AnnouncementEndDate
        /// </summary>
        virtual public System.DateTime? AnnouncementEndDate
        {
            get
            {
                return base.GetSystemDateTime(AnnouncementMetadata.ColumnNames.AnnouncementEndDate);
            }

            set
            {
                base.SetSystemDateTime(AnnouncementMetadata.ColumnNames.AnnouncementEndDate, value);
            }
        }
        /// <summary>
        /// Maps to Announcement.AnnouncementTitle
        /// </summary>
        virtual public System.String AnnouncementTitle
        {
            get
            {
                return base.GetSystemString(AnnouncementMetadata.ColumnNames.AnnouncementTitle);
            }

            set
            {
                base.SetSystemString(AnnouncementMetadata.ColumnNames.AnnouncementTitle, value);
            }
        }
        /// <summary>
        /// Maps to Announcement.AnnouncementDesc
        /// </summary>
        virtual public System.String AnnouncementDesc
        {
            get
            {
                return base.GetSystemString(AnnouncementMetadata.ColumnNames.AnnouncementDesc);
            }

            set
            {
                base.SetSystemString(AnnouncementMetadata.ColumnNames.AnnouncementDesc, value);
            }
        }
        /// <summary>
        /// Maps to Announcement.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(AnnouncementMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(AnnouncementMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to Announcement.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(AnnouncementMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(AnnouncementMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to Announcement.IsActive
        /// </summary>
        virtual public System.Boolean? IsActive
        {
            get
            {
                return base.GetSystemBoolean(AnnouncementMetadata.ColumnNames.IsActive);
            }

            set
            {
                base.SetSystemBoolean(AnnouncementMetadata.ColumnNames.IsActive, value);
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
            public esStrings(esAnnouncement entity)
            {
                this.entity = entity;
            }
            public System.String AnnouncementNo
            {
                get
                {
                    System.String data = entity.AnnouncementNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.AnnouncementNo = null;
                    else entity.AnnouncementNo = Convert.ToString(value);
                }
            }
            public System.String AnnouncementStartDate
            {
                get
                {
                    System.DateTime? data = entity.AnnouncementStartDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.AnnouncementStartDate = null;
                    else entity.AnnouncementStartDate = Convert.ToDateTime(value);
                }
            }
            public System.String AnnouncementEndDate
            {
                get
                {
                    System.DateTime? data = entity.AnnouncementEndDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.AnnouncementEndDate = null;
                    else entity.AnnouncementEndDate = Convert.ToDateTime(value);
                }
            }
            public System.String AnnouncementTitle
            {
                get
                {
                    System.String data = entity.AnnouncementTitle;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.AnnouncementTitle = null;
                    else entity.AnnouncementTitle = Convert.ToString(value);
                }
            }
            public System.String AnnouncementDesc
            {
                get
                {
                    System.String data = entity.AnnouncementDesc;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.AnnouncementDesc = null;
                    else entity.AnnouncementDesc = Convert.ToString(value);
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
            public System.String IsActive
            {
                get
                {
                    System.Boolean? data = entity.IsActive;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsActive = null;
                    else entity.IsActive = Convert.ToBoolean(value);
                }
            }
            private esAnnouncement entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esAnnouncementQuery query)
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
                throw new Exception("esAnnouncement can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class Announcement : esAnnouncement
    {
    }

    [Serializable]
    abstract public class esAnnouncementQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return AnnouncementMetadata.Meta();
            }
        }

        public esQueryItem AnnouncementNo
        {
            get
            {
                return new esQueryItem(this, AnnouncementMetadata.ColumnNames.AnnouncementNo, esSystemType.String);
            }
        }

        public esQueryItem AnnouncementStartDate
        {
            get
            {
                return new esQueryItem(this, AnnouncementMetadata.ColumnNames.AnnouncementStartDate, esSystemType.DateTime);
            }
        }

        public esQueryItem AnnouncementEndDate
        {
            get
            {
                return new esQueryItem(this, AnnouncementMetadata.ColumnNames.AnnouncementEndDate, esSystemType.DateTime);
            }
        }

        public esQueryItem AnnouncementTitle
        {
            get
            {
                return new esQueryItem(this, AnnouncementMetadata.ColumnNames.AnnouncementTitle, esSystemType.String);
            }
        }

        public esQueryItem AnnouncementDesc
        {
            get
            {
                return new esQueryItem(this, AnnouncementMetadata.ColumnNames.AnnouncementDesc, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, AnnouncementMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, AnnouncementMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem IsActive
        {
            get
            {
                return new esQueryItem(this, AnnouncementMetadata.ColumnNames.IsActive, esSystemType.Boolean);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("AnnouncementCollection")]
    public partial class AnnouncementCollection : esAnnouncementCollection, IEnumerable<Announcement>
    {
        public AnnouncementCollection()
        {

        }

        public static implicit operator List<Announcement>(AnnouncementCollection coll)
        {
            List<Announcement> list = new List<Announcement>();

            foreach (Announcement emp in coll)
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
                return AnnouncementMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new AnnouncementQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new Announcement(row);
        }

        override protected esEntity CreateEntity()
        {
            return new Announcement();
        }

        #endregion

        [BrowsableAttribute(false)]
        public AnnouncementQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new AnnouncementQuery();
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
        public bool Load(AnnouncementQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public Announcement AddNew()
        {
            Announcement entity = base.AddNewEntity() as Announcement;

            return entity;
        }
        public Announcement FindByPrimaryKey(String announcementNo)
        {
            return base.FindByPrimaryKey(announcementNo) as Announcement;
        }

        #region IEnumerable< Announcement> Members

        IEnumerator<Announcement> IEnumerable<Announcement>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as Announcement;
            }
        }

        #endregion

        private AnnouncementQuery query;
    }


    /// <summary>
    /// Encapsulates the 'Announcement' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("Announcement ({AnnouncementNo})")]
    [Serializable]
    public partial class Announcement : esAnnouncement
    {
        public Announcement()
        {
        }

        public Announcement(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return AnnouncementMetadata.Meta();
            }
        }

        override protected esAnnouncementQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new AnnouncementQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public AnnouncementQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new AnnouncementQuery();
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
        public bool Load(AnnouncementQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private AnnouncementQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class AnnouncementQuery : esAnnouncementQuery
    {
        public AnnouncementQuery()
        {

        }

        public AnnouncementQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "AnnouncementQuery";
        }
    }

    [Serializable]
    public partial class AnnouncementMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected AnnouncementMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(AnnouncementMetadata.ColumnNames.AnnouncementNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = AnnouncementMetadata.PropertyNames.AnnouncementNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(AnnouncementMetadata.ColumnNames.AnnouncementStartDate, 1, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = AnnouncementMetadata.PropertyNames.AnnouncementStartDate;
            _columns.Add(c);

            c = new esColumnMetadata(AnnouncementMetadata.ColumnNames.AnnouncementEndDate, 2, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = AnnouncementMetadata.PropertyNames.AnnouncementEndDate;
            _columns.Add(c);

            c = new esColumnMetadata(AnnouncementMetadata.ColumnNames.AnnouncementTitle, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = AnnouncementMetadata.PropertyNames.AnnouncementTitle;
            c.CharacterMaxLength = 50;
            _columns.Add(c);

            c = new esColumnMetadata(AnnouncementMetadata.ColumnNames.AnnouncementDesc, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = AnnouncementMetadata.PropertyNames.AnnouncementDesc;
            c.CharacterMaxLength = 2147483647;
            _columns.Add(c);

            c = new esColumnMetadata(AnnouncementMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = AnnouncementMetadata.PropertyNames.LastUpdateDateTime;
            _columns.Add(c);

            c = new esColumnMetadata(AnnouncementMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = AnnouncementMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            _columns.Add(c);

            c = new esColumnMetadata(AnnouncementMetadata.ColumnNames.IsActive, 7, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = AnnouncementMetadata.PropertyNames.IsActive;
            _columns.Add(c);


        }
        #endregion

        static public AnnouncementMetadata Meta()
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
            public const string AnnouncementNo = "AnnouncementNo";
            public const string AnnouncementStartDate = "AnnouncementStartDate";
            public const string AnnouncementEndDate = "AnnouncementEndDate";
            public const string AnnouncementTitle = "AnnouncementTitle";
            public const string AnnouncementDesc = "AnnouncementDesc";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string IsActive = "IsActive";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string AnnouncementNo = "AnnouncementNo";
            public const string AnnouncementStartDate = "AnnouncementStartDate";
            public const string AnnouncementEndDate = "AnnouncementEndDate";
            public const string AnnouncementTitle = "AnnouncementTitle";
            public const string AnnouncementDesc = "AnnouncementDesc";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string IsActive = "IsActive";
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
            lock (typeof(AnnouncementMetadata))
            {
                if (AnnouncementMetadata.mapDelegates == null)
                {
                    AnnouncementMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (AnnouncementMetadata.meta == null)
                {
                    AnnouncementMetadata.meta = new AnnouncementMetadata();
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

                meta.AddTypeMap("AnnouncementNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("AnnouncementStartDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("AnnouncementEndDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("AnnouncementTitle", new esTypeMap("nvarchar", "System.String"));
                meta.AddTypeMap("AnnouncementDesc", new esTypeMap("nvarchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("nvarchar", "System.String"));
                meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));


                meta.Source = "Announcement";
                meta.Destination = "Announcement";
                meta.spInsert = "proc_AnnouncementInsert";
                meta.spUpdate = "proc_AnnouncementUpdate";
                meta.spDelete = "proc_AnnouncementDelete";
                meta.spLoadAll = "proc_AnnouncementLoadAll";
                meta.spLoadByPrimaryKey = "proc_AnnouncementLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private AnnouncementMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}