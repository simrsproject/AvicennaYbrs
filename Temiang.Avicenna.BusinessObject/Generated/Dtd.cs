/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/15/18 11:12:55 AM
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
    abstract public class esDtdCollection : esEntityCollectionWAuditLog
    {
        public esDtdCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "DtdCollection";
        }

        #region Query Logic
        protected void InitQuery(esDtdQuery query)
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
            this.InitQuery(query as esDtdQuery);
        }
        #endregion

        virtual public Dtd DetachEntity(Dtd entity)
        {
            return base.DetachEntity(entity) as Dtd;
        }

        virtual public Dtd AttachEntity(Dtd entity)
        {
            return base.AttachEntity(entity) as Dtd;
        }

        virtual public void Combine(DtdCollection collection)
        {
            base.Combine(collection);
        }

        new public Dtd this[int index]
        {
            get
            {
                return base[index] as Dtd;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(Dtd);
        }
    }

    [Serializable]
    abstract public class esDtd : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esDtdQuery GetDynamicQuery()
        {
            return null;
        }

        public esDtd()
        {
        }

        public esDtd(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String dtdNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(dtdNo);
            else
                return LoadByPrimaryKeyStoredProcedure(dtdNo);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String dtdNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(dtdNo);
            else
                return LoadByPrimaryKeyStoredProcedure(dtdNo);
        }

        private bool LoadByPrimaryKeyDynamic(String dtdNo)
        {
            esDtdQuery query = this.GetDynamicQuery();
            query.Where(query.DtdNo == dtdNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String dtdNo)
        {
            esParameters parms = new esParameters();
            parms.Add("DtdNo", dtdNo);
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
                        case "DtdNo": this.str.DtdNo = (string)value; break;
                        case "DtdName": this.str.DtdName = (string)value; break;
                        case "DtdLabel": this.str.DtdLabel = (string)value; break;
                        case "IsActive": this.str.IsActive = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "SRChronicDisease": this.str.SRChronicDisease = (string)value; break;
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
        /// Maps to Dtd.DtdNo
        /// </summary>
        virtual public System.String DtdNo
        {
            get
            {
                return base.GetSystemString(DtdMetadata.ColumnNames.DtdNo);
            }

            set
            {
                base.SetSystemString(DtdMetadata.ColumnNames.DtdNo, value);
            }
        }
        /// <summary>
        /// Maps to Dtd.DtdName
        /// </summary>
        virtual public System.String DtdName
        {
            get
            {
                return base.GetSystemString(DtdMetadata.ColumnNames.DtdName);
            }

            set
            {
                base.SetSystemString(DtdMetadata.ColumnNames.DtdName, value);
            }
        }
        /// <summary>
        /// Maps to Dtd.DtdLabel
        /// </summary>
        virtual public System.String DtdLabel
        {
            get
            {
                return base.GetSystemString(DtdMetadata.ColumnNames.DtdLabel);
            }

            set
            {
                base.SetSystemString(DtdMetadata.ColumnNames.DtdLabel, value);
            }
        }
        /// <summary>
        /// Maps to Dtd.IsActive
        /// </summary>
        virtual public System.Boolean? IsActive
        {
            get
            {
                return base.GetSystemBoolean(DtdMetadata.ColumnNames.IsActive);
            }

            set
            {
                base.SetSystemBoolean(DtdMetadata.ColumnNames.IsActive, value);
            }
        }
        /// <summary>
        /// Maps to Dtd.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(DtdMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(DtdMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to Dtd.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(DtdMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(DtdMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to Dtd.SRChronicDisease
        /// </summary>
        virtual public System.String SRChronicDisease
        {
            get
            {
                return base.GetSystemString(DtdMetadata.ColumnNames.SRChronicDisease);
            }

            set
            {
                base.SetSystemString(DtdMetadata.ColumnNames.SRChronicDisease, value);
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
            public esStrings(esDtd entity)
            {
                this.entity = entity;
            }
            public System.String DtdNo
            {
                get
                {
                    System.String data = entity.DtdNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DtdNo = null;
                    else entity.DtdNo = Convert.ToString(value);
                }
            }
            public System.String DtdName
            {
                get
                {
                    System.String data = entity.DtdName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DtdName = null;
                    else entity.DtdName = Convert.ToString(value);
                }
            }
            public System.String DtdLabel
            {
                get
                {
                    System.String data = entity.DtdLabel;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DtdLabel = null;
                    else entity.DtdLabel = Convert.ToString(value);
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
            public System.String SRChronicDisease
            {
                get
                {
                    System.String data = entity.SRChronicDisease;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRChronicDisease = null;
                    else entity.SRChronicDisease = Convert.ToString(value);
                }
            }
            private esDtd entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esDtdQuery query)
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
                throw new Exception("esDtd can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class Dtd : esDtd
    {
    }

    [Serializable]
    abstract public class esDtdQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return DtdMetadata.Meta();
            }
        }

        public esQueryItem DtdNo
        {
            get
            {
                return new esQueryItem(this, DtdMetadata.ColumnNames.DtdNo, esSystemType.String);
            }
        }

        public esQueryItem DtdName
        {
            get
            {
                return new esQueryItem(this, DtdMetadata.ColumnNames.DtdName, esSystemType.String);
            }
        }

        public esQueryItem DtdLabel
        {
            get
            {
                return new esQueryItem(this, DtdMetadata.ColumnNames.DtdLabel, esSystemType.String);
            }
        }

        public esQueryItem IsActive
        {
            get
            {
                return new esQueryItem(this, DtdMetadata.ColumnNames.IsActive, esSystemType.Boolean);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, DtdMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, DtdMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem SRChronicDisease
        {
            get
            {
                return new esQueryItem(this, DtdMetadata.ColumnNames.SRChronicDisease, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("DtdCollection")]
    public partial class DtdCollection : esDtdCollection, IEnumerable<Dtd>
    {
        public DtdCollection()
        {

        }

        public static implicit operator List<Dtd>(DtdCollection coll)
        {
            List<Dtd> list = new List<Dtd>();

            foreach (Dtd emp in coll)
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
                return DtdMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new DtdQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new Dtd(row);
        }

        override protected esEntity CreateEntity()
        {
            return new Dtd();
        }

        #endregion

        [BrowsableAttribute(false)]
        public DtdQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new DtdQuery();
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
        public bool Load(DtdQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public Dtd AddNew()
        {
            Dtd entity = base.AddNewEntity() as Dtd;

            return entity;
        }
        public Dtd FindByPrimaryKey(String dtdNo)
        {
            return base.FindByPrimaryKey(dtdNo) as Dtd;
        }

        #region IEnumerable< Dtd> Members

        IEnumerator<Dtd> IEnumerable<Dtd>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as Dtd;
            }
        }

        #endregion

        private DtdQuery query;
    }


    /// <summary>
    /// Encapsulates the 'Dtd' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("Dtd ({DtdNo})")]
    [Serializable]
    public partial class Dtd : esDtd
    {
        public Dtd()
        {
        }

        public Dtd(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return DtdMetadata.Meta();
            }
        }

        override protected esDtdQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new DtdQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public DtdQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new DtdQuery();
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
        public bool Load(DtdQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private DtdQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class DtdQuery : esDtdQuery
    {
        public DtdQuery()
        {

        }

        public DtdQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "DtdQuery";
        }
    }

    [Serializable]
    public partial class DtdMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected DtdMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(DtdMetadata.ColumnNames.DtdNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = DtdMetadata.PropertyNames.DtdNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(DtdMetadata.ColumnNames.DtdName, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = DtdMetadata.PropertyNames.DtdName;
            c.CharacterMaxLength = 500;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(DtdMetadata.ColumnNames.DtdLabel, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = DtdMetadata.PropertyNames.DtdLabel;
            c.CharacterMaxLength = 500;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(DtdMetadata.ColumnNames.IsActive, 3, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = DtdMetadata.PropertyNames.IsActive;
            c.HasDefault = true;
            c.Default = @"((1))";
            _columns.Add(c);

            c = new esColumnMetadata(DtdMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = DtdMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(DtdMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = DtdMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 40;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(DtdMetadata.ColumnNames.SRChronicDisease, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = DtdMetadata.PropertyNames.SRChronicDisease;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public DtdMetadata Meta()
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
            public const string DtdNo = "DtdNo";
            public const string DtdName = "DtdName";
            public const string DtdLabel = "DtdLabel";
            public const string IsActive = "IsActive";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string SRChronicDisease = "SRChronicDisease";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string DtdNo = "DtdNo";
            public const string DtdName = "DtdName";
            public const string DtdLabel = "DtdLabel";
            public const string IsActive = "IsActive";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string SRChronicDisease = "SRChronicDisease";
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
            lock (typeof(DtdMetadata))
            {
                if (DtdMetadata.mapDelegates == null)
                {
                    DtdMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (DtdMetadata.meta == null)
                {
                    DtdMetadata.meta = new DtdMetadata();
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

                meta.AddTypeMap("DtdNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("DtdName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("DtdLabel", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRChronicDisease", new esTypeMap("varchar", "System.String"));


                meta.Source = "Dtd";
                meta.Destination = "Dtd";
                meta.spInsert = "proc_DtdInsert";
                meta.spUpdate = "proc_DtdUpdate";
                meta.spDelete = "proc_DtdDelete";
                meta.spLoadAll = "proc_DtdLoadAll";
                meta.spLoadByPrimaryKey = "proc_DtdLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private DtdMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
