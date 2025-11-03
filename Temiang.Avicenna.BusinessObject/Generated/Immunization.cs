/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 6/26/2016 3:03:34 AM
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
    abstract public class esImmunizationCollection : esEntityCollectionWAuditLog
    {
        public esImmunizationCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "ImmunizationCollection";
        }

        #region Query Logic
        protected void InitQuery(esImmunizationQuery query)
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
            this.InitQuery(query as esImmunizationQuery);
        }
        #endregion

        virtual public Immunization DetachEntity(Immunization entity)
        {
            return base.DetachEntity(entity) as Immunization;
        }

        virtual public Immunization AttachEntity(Immunization entity)
        {
            return base.AttachEntity(entity) as Immunization;
        }

        virtual public void Combine(ImmunizationCollection collection)
        {
            base.Combine(collection);
        }

        new public Immunization this[int index]
        {
            get
            {
                return base[index] as Immunization;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(Immunization);
        }
    }

    [Serializable]
    abstract public class esImmunization : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esImmunizationQuery GetDynamicQuery()
        {
            return null;
        }

        public esImmunization()
        {
        }

        public esImmunization(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String immunizationID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(immunizationID);
            else
                return LoadByPrimaryKeyStoredProcedure(immunizationID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String immunizationID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(immunizationID);
            else
                return LoadByPrimaryKeyStoredProcedure(immunizationID);
        }

        private bool LoadByPrimaryKeyDynamic(String immunizationID)
        {
            esImmunizationQuery query = this.GetDynamicQuery();
            query.Where(query.ImmunizationID == immunizationID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String immunizationID)
        {
            esParameters parms = new esParameters();
            parms.Add("ImmunizationID", immunizationID);
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
                        case "ImmunizationID": this.str.ImmunizationID = (string)value; break;
                        case "ImmunizationName": this.str.ImmunizationName = (string)value; break;
                        case "MaxCount": this.str.MaxCount = (string)value; break;
                        case "IndexNo": this.str.IndexNo = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "MaxCount":

                            if (value == null || value is System.Int32)
                                this.MaxCount = (System.Int32?)value;
                            break;
                        case "IndexNo":

                            if (value == null || value is System.Int32)
                                this.IndexNo = (System.Int32?)value;
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
        /// Maps to Immunization.ImmunizationID
        /// </summary>
        virtual public System.String ImmunizationID
        {
            get
            {
                return base.GetSystemString(ImmunizationMetadata.ColumnNames.ImmunizationID);
            }

            set
            {
                base.SetSystemString(ImmunizationMetadata.ColumnNames.ImmunizationID, value);
            }
        }
        /// <summary>
        /// Maps to Immunization.ImmunizationName
        /// </summary>
        virtual public System.String ImmunizationName
        {
            get
            {
                return base.GetSystemString(ImmunizationMetadata.ColumnNames.ImmunizationName);
            }

            set
            {
                base.SetSystemString(ImmunizationMetadata.ColumnNames.ImmunizationName, value);
            }
        }
        /// <summary>
        /// Maps to Immunization.MaxCount
        /// </summary>
        virtual public System.Int32? MaxCount
        {
            get
            {
                return base.GetSystemInt32(ImmunizationMetadata.ColumnNames.MaxCount);
            }

            set
            {
                base.SetSystemInt32(ImmunizationMetadata.ColumnNames.MaxCount, value);
            }
        }
        /// <summary>
        /// Maps to Immunization.IndexNo
        /// </summary>
        virtual public System.Int32? IndexNo
        {
            get
            {
                return base.GetSystemInt32(ImmunizationMetadata.ColumnNames.IndexNo);
            }

            set
            {
                base.SetSystemInt32(ImmunizationMetadata.ColumnNames.IndexNo, value);
            }
        }
        /// <summary>
        /// Maps to Immunization.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(ImmunizationMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(ImmunizationMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to Immunization.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(ImmunizationMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(ImmunizationMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esImmunization entity)
            {
                this.entity = entity;
            }
            public System.String ImmunizationID
            {
                get
                {
                    System.String data = entity.ImmunizationID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ImmunizationID = null;
                    else entity.ImmunizationID = Convert.ToString(value);
                }
            }
            public System.String ImmunizationName
            {
                get
                {
                    System.String data = entity.ImmunizationName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ImmunizationName = null;
                    else entity.ImmunizationName = Convert.ToString(value);
                }
            }
            public System.String MaxCount
            {
                get
                {
                    System.Int32? data = entity.MaxCount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.MaxCount = null;
                    else entity.MaxCount = Convert.ToInt32(value);
                }
            }
            public System.String IndexNo
            {
                get
                {
                    System.Int32? data = entity.IndexNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IndexNo = null;
                    else entity.IndexNo = Convert.ToInt32(value);
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
            private esImmunization entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esImmunizationQuery query)
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
                throw new Exception("esImmunization can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class Immunization : esImmunization
    {
    }

    [Serializable]
    abstract public class esImmunizationQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return ImmunizationMetadata.Meta();
            }
        }

        public esQueryItem ImmunizationID
        {
            get
            {
                return new esQueryItem(this, ImmunizationMetadata.ColumnNames.ImmunizationID, esSystemType.String);
            }
        }

        public esQueryItem ImmunizationName
        {
            get
            {
                return new esQueryItem(this, ImmunizationMetadata.ColumnNames.ImmunizationName, esSystemType.String);
            }
        }

        public esQueryItem MaxCount
        {
            get
            {
                return new esQueryItem(this, ImmunizationMetadata.ColumnNames.MaxCount, esSystemType.Int32);
            }
        }

        public esQueryItem IndexNo
        {
            get
            {
                return new esQueryItem(this, ImmunizationMetadata.ColumnNames.IndexNo, esSystemType.Int32);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, ImmunizationMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, ImmunizationMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("ImmunizationCollection")]
    public partial class ImmunizationCollection : esImmunizationCollection, IEnumerable<Immunization>
    {
        public ImmunizationCollection()
        {

        }

        public static implicit operator List<Immunization>(ImmunizationCollection coll)
        {
            List<Immunization> list = new List<Immunization>();

            foreach (Immunization emp in coll)
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
                return ImmunizationMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ImmunizationQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new Immunization(row);
        }

        override protected esEntity CreateEntity()
        {
            return new Immunization();
        }

        #endregion

        [BrowsableAttribute(false)]
        public ImmunizationQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ImmunizationQuery();
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
        public bool Load(ImmunizationQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public Immunization AddNew()
        {
            Immunization entity = base.AddNewEntity() as Immunization;

            return entity;
        }
        public Immunization FindByPrimaryKey(String immunizationID)
        {
            return base.FindByPrimaryKey(immunizationID) as Immunization;
        }

        #region IEnumerable< Immunization> Members

        IEnumerator<Immunization> IEnumerable<Immunization>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as Immunization;
            }
        }

        #endregion

        private ImmunizationQuery query;
    }


    /// <summary>
    /// Encapsulates the 'Immunization' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("Immunization ({ImmunizationID})")]
    [Serializable]
    public partial class Immunization : esImmunization
    {
        public Immunization()
        {
        }

        public Immunization(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return ImmunizationMetadata.Meta();
            }
        }

        override protected esImmunizationQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ImmunizationQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public ImmunizationQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ImmunizationQuery();
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
        public bool Load(ImmunizationQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private ImmunizationQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class ImmunizationQuery : esImmunizationQuery
    {
        public ImmunizationQuery()
        {

        }

        public ImmunizationQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "ImmunizationQuery";
        }
    }

    [Serializable]
    public partial class ImmunizationMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected ImmunizationMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(ImmunizationMetadata.ColumnNames.ImmunizationID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = ImmunizationMetadata.PropertyNames.ImmunizationID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 3;
            _columns.Add(c);

            c = new esColumnMetadata(ImmunizationMetadata.ColumnNames.ImmunizationName, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = ImmunizationMetadata.PropertyNames.ImmunizationName;
            c.CharacterMaxLength = 150;
            _columns.Add(c);

            c = new esColumnMetadata(ImmunizationMetadata.ColumnNames.MaxCount, 2, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = ImmunizationMetadata.PropertyNames.MaxCount;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(ImmunizationMetadata.ColumnNames.IndexNo, 3, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = ImmunizationMetadata.PropertyNames.IndexNo;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ImmunizationMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ImmunizationMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ImmunizationMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = ImmunizationMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public ImmunizationMetadata Meta()
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
            public const string ImmunizationID = "ImmunizationID";
            public const string ImmunizationName = "ImmunizationName";
            public const string MaxCount = "MaxCount";
            public const string IndexNo = "IndexNo";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string ImmunizationID = "ImmunizationID";
            public const string ImmunizationName = "ImmunizationName";
            public const string MaxCount = "MaxCount";
            public const string IndexNo = "IndexNo";
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
            lock (typeof(ImmunizationMetadata))
            {
                if (ImmunizationMetadata.mapDelegates == null)
                {
                    ImmunizationMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (ImmunizationMetadata.meta == null)
                {
                    ImmunizationMetadata.meta = new ImmunizationMetadata();
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

                meta.AddTypeMap("ImmunizationID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ImmunizationName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("MaxCount", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("IndexNo", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "Immunization";
                meta.Destination = "Immunization";
                meta.spInsert = "proc_ImmunizationInsert";
                meta.spUpdate = "proc_ImmunizationUpdate";
                meta.spDelete = "proc_ImmunizationDelete";
                meta.spLoadAll = "proc_ImmunizationLoadAll";
                meta.spLoadByPrimaryKey = "proc_ImmunizationLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private ImmunizationMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
