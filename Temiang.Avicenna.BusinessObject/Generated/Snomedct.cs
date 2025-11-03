/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/06/2024 09:36:27
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
    abstract public class esSnomedctCollection : esEntityCollectionWAuditLog
    {
        public esSnomedctCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "SnomedctCollection";
        }

        #region Query Logic
        protected void InitQuery(esSnomedctQuery query)
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
            this.InitQuery(query as esSnomedctQuery);
        }
        #endregion

        virtual public Snomedct DetachEntity(Snomedct entity)
        {
            return base.DetachEntity(entity) as Snomedct;
        }

        virtual public Snomedct AttachEntity(Snomedct entity)
        {
            return base.AttachEntity(entity) as Snomedct;
        }

        virtual public void Combine(SnomedctCollection collection)
        {
            base.Combine(collection);
        }

        new public Snomedct this[int index]
        {
            get
            {
                return base[index] as Snomedct;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(Snomedct);
        }
    }

    [Serializable]
    abstract public class esSnomedct : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esSnomedctQuery GetDynamicQuery()
        {
            return null;
        }

        public esSnomedct()
        {
        }

        public esSnomedct(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String sRSnomedct, String code)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(sRSnomedct, code);
            else
                return LoadByPrimaryKeyStoredProcedure(sRSnomedct, code);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String sRSnomedct, String code)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(sRSnomedct, code);
            else
                return LoadByPrimaryKeyStoredProcedure(sRSnomedct, code);
        }

        private bool LoadByPrimaryKeyDynamic(String sRSnomedct, String code)
        {
            esSnomedctQuery query = this.GetDynamicQuery();
            query.Where(query.SRSnomedct == sRSnomedct, query.Code == code);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String sRSnomedct, String code)
        {
            esParameters parms = new esParameters();
            parms.Add("SRSnomedct", sRSnomedct);
            parms.Add("Code", code);
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
                        case "SRSnomedct": this.str.SRSnomedct = (string)value; break;
                        case "Code": this.str.Code = (string)value; break;
                        case "Display": this.str.Display = (string)value; break;
                        case "DisplayNative": this.str.DisplayNative = (string)value; break;
                        case "Note": this.str.Note = (string)value; break;
                        case "IsActive": this.str.IsActive = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "IsActive":

                            if (value == null || value is System.Boolean)
                                this.IsActive = (System.Boolean?)value;
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
        /// Maps to Snomedct.SRSnomedct
        /// </summary>
        virtual public System.String SRSnomedct
        {
            get
            {
                return base.GetSystemString(SnomedctMetadata.ColumnNames.SRSnomedct);
            }

            set
            {
                base.SetSystemString(SnomedctMetadata.ColumnNames.SRSnomedct, value);
            }
        }
        /// <summary>
        /// Maps to Snomedct.Code
        /// </summary>
        virtual public System.String Code
        {
            get
            {
                return base.GetSystemString(SnomedctMetadata.ColumnNames.Code);
            }

            set
            {
                base.SetSystemString(SnomedctMetadata.ColumnNames.Code, value);
            }
        }
        /// <summary>
        /// Maps to Snomedct.Display
        /// </summary>
        virtual public System.String Display
        {
            get
            {
                return base.GetSystemString(SnomedctMetadata.ColumnNames.Display);
            }

            set
            {
                base.SetSystemString(SnomedctMetadata.ColumnNames.Display, value);
            }
        }
        /// <summary>
        /// Maps to Snomedct.DisplayNative
        /// </summary>
        virtual public System.String DisplayNative
        {
            get
            {
                return base.GetSystemString(SnomedctMetadata.ColumnNames.DisplayNative);
            }

            set
            {
                base.SetSystemString(SnomedctMetadata.ColumnNames.DisplayNative, value);
            }
        }
        /// <summary>
        /// Maps to Snomedct.Note
        /// </summary>
        virtual public System.String Note
        {
            get
            {
                return base.GetSystemString(SnomedctMetadata.ColumnNames.Note);
            }

            set
            {
                base.SetSystemString(SnomedctMetadata.ColumnNames.Note, value);
            }
        }
        /// <summary>
        /// Maps to Snomedct.IsActive
        /// </summary>
        virtual public System.Boolean? IsActive
        {
            get
            {
                return base.GetSystemBoolean(SnomedctMetadata.ColumnNames.IsActive);
            }

            set
            {
                base.SetSystemBoolean(SnomedctMetadata.ColumnNames.IsActive, value);
            }
        }
        /// <summary>
        /// Maps to Snomedct.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(SnomedctMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(SnomedctMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to Snomedct.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(SnomedctMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(SnomedctMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esSnomedct entity)
            {
                this.entity = entity;
            }
            public System.String SRSnomedct
            {
                get
                {
                    System.String data = entity.SRSnomedct;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRSnomedct = null;
                    else entity.SRSnomedct = Convert.ToString(value);
                }
            }
            public System.String Code
            {
                get
                {
                    System.String data = entity.Code;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Code = null;
                    else entity.Code = Convert.ToString(value);
                }
            }
            public System.String Display
            {
                get
                {
                    System.String data = entity.Display;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Display = null;
                    else entity.Display = Convert.ToString(value);
                }
            }
            public System.String DisplayNative
            {
                get
                {
                    System.String data = entity.DisplayNative;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DisplayNative = null;
                    else entity.DisplayNative = Convert.ToString(value);
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
            private esSnomedct entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esSnomedctQuery query)
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
                throw new Exception("esSnomedct can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class Snomedct : esSnomedct
    {
    }

    [Serializable]
    abstract public class esSnomedctQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return SnomedctMetadata.Meta();
            }
        }

        public esQueryItem SRSnomedct
        {
            get
            {
                return new esQueryItem(this, SnomedctMetadata.ColumnNames.SRSnomedct, esSystemType.String);
            }
        }

        public esQueryItem Code
        {
            get
            {
                return new esQueryItem(this, SnomedctMetadata.ColumnNames.Code, esSystemType.String);
            }
        }

        public esQueryItem Display
        {
            get
            {
                return new esQueryItem(this, SnomedctMetadata.ColumnNames.Display, esSystemType.String);
            }
        }

        public esQueryItem DisplayNative
        {
            get
            {
                return new esQueryItem(this, SnomedctMetadata.ColumnNames.DisplayNative, esSystemType.String);
            }
        }

        public esQueryItem Note
        {
            get
            {
                return new esQueryItem(this, SnomedctMetadata.ColumnNames.Note, esSystemType.String);
            }
        }

        public esQueryItem IsActive
        {
            get
            {
                return new esQueryItem(this, SnomedctMetadata.ColumnNames.IsActive, esSystemType.Boolean);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, SnomedctMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, SnomedctMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("SnomedctCollection")]
    public partial class SnomedctCollection : esSnomedctCollection, IEnumerable<Snomedct>
    {
        public SnomedctCollection()
        {

        }

        public static implicit operator List<Snomedct>(SnomedctCollection coll)
        {
            List<Snomedct> list = new List<Snomedct>();

            foreach (Snomedct emp in coll)
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
                return SnomedctMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new SnomedctQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new Snomedct(row);
        }

        override protected esEntity CreateEntity()
        {
            return new Snomedct();
        }

        #endregion

        [BrowsableAttribute(false)]
        public SnomedctQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new SnomedctQuery();
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
        public bool Load(SnomedctQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public Snomedct AddNew()
        {
            Snomedct entity = base.AddNewEntity() as Snomedct;

            return entity;
        }
        public Snomedct FindByPrimaryKey(String sRSnomedct, String code)
        {
            return base.FindByPrimaryKey(sRSnomedct, code) as Snomedct;
        }

        #region IEnumerable< Snomedct> Members

        IEnumerator<Snomedct> IEnumerable<Snomedct>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as Snomedct;
            }
        }

        #endregion

        private SnomedctQuery query;
    }


    /// <summary>
    /// Encapsulates the 'Snomedct' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("Snomedct ({SRSnomedct, Code})")]
    [Serializable]
    public partial class Snomedct : esSnomedct
    {
        public Snomedct()
        {
        }

        public Snomedct(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return SnomedctMetadata.Meta();
            }
        }

        override protected esSnomedctQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new SnomedctQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public SnomedctQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new SnomedctQuery();
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
        public bool Load(SnomedctQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private SnomedctQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class SnomedctQuery : esSnomedctQuery
    {
        public SnomedctQuery()
        {

        }

        public SnomedctQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "SnomedctQuery";
        }
    }

    [Serializable]
    public partial class SnomedctMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected SnomedctMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(SnomedctMetadata.ColumnNames.SRSnomedct, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = SnomedctMetadata.PropertyNames.SRSnomedct;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(SnomedctMetadata.ColumnNames.Code, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = SnomedctMetadata.PropertyNames.Code;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(SnomedctMetadata.ColumnNames.Display, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = SnomedctMetadata.PropertyNames.Display;
            c.CharacterMaxLength = 200;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(SnomedctMetadata.ColumnNames.DisplayNative, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = SnomedctMetadata.PropertyNames.DisplayNative;
            c.CharacterMaxLength = 200;
            c.HasDefault = true;
            c.Default = @"('')";
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(SnomedctMetadata.ColumnNames.Note, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = SnomedctMetadata.PropertyNames.Note;
            c.CharacterMaxLength = 2000;
            c.HasDefault = true;
            c.Default = @"('')";
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(SnomedctMetadata.ColumnNames.IsActive, 5, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = SnomedctMetadata.PropertyNames.IsActive;
            c.HasDefault = true;
            c.Default = @"((0))";
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(SnomedctMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = SnomedctMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(SnomedctMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = SnomedctMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public SnomedctMetadata Meta()
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
            public const string SRSnomedct = "SRSnomedct";
            public const string Code = "Code";
            public const string Display = "Display";
            public const string DisplayNative = "DisplayNative";
            public const string Note = "Note";
            public const string IsActive = "IsActive";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string SRSnomedct = "SRSnomedct";
            public const string Code = "Code";
            public const string Display = "Display";
            public const string DisplayNative = "DisplayNative";
            public const string Note = "Note";
            public const string IsActive = "IsActive";
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
            lock (typeof(SnomedctMetadata))
            {
                if (SnomedctMetadata.mapDelegates == null)
                {
                    SnomedctMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (SnomedctMetadata.meta == null)
                {
                    SnomedctMetadata.meta = new SnomedctMetadata();
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

                meta.AddTypeMap("SRSnomedct", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Code", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Display", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("DisplayNative", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Note", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "Snomedct";
                meta.Destination = "Snomedct";
                meta.spInsert = "proc_SnomedctInsert";
                meta.spUpdate = "proc_SnomedctUpdate";
                meta.spDelete = "proc_SnomedctDelete";
                meta.spLoadAll = "proc_SnomedctLoadAll";
                meta.spLoadByPrimaryKey = "proc_SnomedctLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private SnomedctMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
