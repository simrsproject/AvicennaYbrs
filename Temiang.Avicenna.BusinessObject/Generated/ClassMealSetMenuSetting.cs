/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 12/6/2017 10:26:07 AM
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
    abstract public class esClassMealSetMenuSettingCollection : esEntityCollectionWAuditLog
    {
        public esClassMealSetMenuSettingCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "ClassMealSetMenuSettingCollection";
        }

        #region Query Logic
        protected void InitQuery(esClassMealSetMenuSettingQuery query)
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
            this.InitQuery(query as esClassMealSetMenuSettingQuery);
        }
        #endregion

        virtual public ClassMealSetMenuSetting DetachEntity(ClassMealSetMenuSetting entity)
        {
            return base.DetachEntity(entity) as ClassMealSetMenuSetting;
        }

        virtual public ClassMealSetMenuSetting AttachEntity(ClassMealSetMenuSetting entity)
        {
            return base.AttachEntity(entity) as ClassMealSetMenuSetting;
        }

        virtual public void Combine(ClassMealSetMenuSettingCollection collection)
        {
            base.Combine(collection);
        }

        new public ClassMealSetMenuSetting this[int index]
        {
            get
            {
                return base[index] as ClassMealSetMenuSetting;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(ClassMealSetMenuSetting);
        }
    }

    [Serializable]
    abstract public class esClassMealSetMenuSetting : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esClassMealSetMenuSettingQuery GetDynamicQuery()
        {
            return null;
        }

        public esClassMealSetMenuSetting()
        {
        }

        public esClassMealSetMenuSetting(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String classID, String sRMealSet)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(classID, sRMealSet);
            else
                return LoadByPrimaryKeyStoredProcedure(classID, sRMealSet);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String classID, String sRMealSet)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(classID, sRMealSet);
            else
                return LoadByPrimaryKeyStoredProcedure(classID, sRMealSet);
        }

        private bool LoadByPrimaryKeyDynamic(String classID, String sRMealSet)
        {
            esClassMealSetMenuSettingQuery query = this.GetDynamicQuery();
            query.Where(query.ClassID == classID, query.SRMealSet == sRMealSet);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String classID, String sRMealSet)
        {
            esParameters parms = new esParameters();
            parms.Add("ClassID", classID);
            parms.Add("SRMealSet", sRMealSet);
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
                        case "ClassID": this.str.ClassID = (string)value; break;
                        case "SRMealSet": this.str.SRMealSet = (string)value; break;
                        case "IsOptional": this.str.IsOptional = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "IsOptional":

                            if (value == null || value is System.Boolean)
                                this.IsOptional = (System.Boolean?)value;
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
        /// Maps to ClassMealSetMenuSetting.ClassID
        /// </summary>
        virtual public System.String ClassID
        {
            get
            {
                return base.GetSystemString(ClassMealSetMenuSettingMetadata.ColumnNames.ClassID);
            }

            set
            {
                base.SetSystemString(ClassMealSetMenuSettingMetadata.ColumnNames.ClassID, value);
            }
        }
        /// <summary>
        /// Maps to ClassMealSetMenuSetting.SRMealSet
        /// </summary>
        virtual public System.String SRMealSet
        {
            get
            {
                return base.GetSystemString(ClassMealSetMenuSettingMetadata.ColumnNames.SRMealSet);
            }

            set
            {
                base.SetSystemString(ClassMealSetMenuSettingMetadata.ColumnNames.SRMealSet, value);
            }
        }
        /// <summary>
        /// Maps to ClassMealSetMenuSetting.IsOptional
        /// </summary>
        virtual public System.Boolean? IsOptional
        {
            get
            {
                return base.GetSystemBoolean(ClassMealSetMenuSettingMetadata.ColumnNames.IsOptional);
            }

            set
            {
                base.SetSystemBoolean(ClassMealSetMenuSettingMetadata.ColumnNames.IsOptional, value);
            }
        }
        /// <summary>
        /// Maps to ClassMealSetMenuSetting.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(ClassMealSetMenuSettingMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(ClassMealSetMenuSettingMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to ClassMealSetMenuSetting.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(ClassMealSetMenuSettingMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(ClassMealSetMenuSettingMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esClassMealSetMenuSetting entity)
            {
                this.entity = entity;
            }
            public System.String ClassID
            {
                get
                {
                    System.String data = entity.ClassID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ClassID = null;
                    else entity.ClassID = Convert.ToString(value);
                }
            }
            public System.String SRMealSet
            {
                get
                {
                    System.String data = entity.SRMealSet;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRMealSet = null;
                    else entity.SRMealSet = Convert.ToString(value);
                }
            }
            public System.String IsOptional
            {
                get
                {
                    System.Boolean? data = entity.IsOptional;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsOptional = null;
                    else entity.IsOptional = Convert.ToBoolean(value);
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
            private esClassMealSetMenuSetting entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esClassMealSetMenuSettingQuery query)
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
                throw new Exception("esClassMealSetMenuSetting can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class ClassMealSetMenuSetting : esClassMealSetMenuSetting
    {
    }

    [Serializable]
    abstract public class esClassMealSetMenuSettingQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return ClassMealSetMenuSettingMetadata.Meta();
            }
        }

        public esQueryItem ClassID
        {
            get
            {
                return new esQueryItem(this, ClassMealSetMenuSettingMetadata.ColumnNames.ClassID, esSystemType.String);
            }
        }

        public esQueryItem SRMealSet
        {
            get
            {
                return new esQueryItem(this, ClassMealSetMenuSettingMetadata.ColumnNames.SRMealSet, esSystemType.String);
            }
        }

        public esQueryItem IsOptional
        {
            get
            {
                return new esQueryItem(this, ClassMealSetMenuSettingMetadata.ColumnNames.IsOptional, esSystemType.Boolean);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, ClassMealSetMenuSettingMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, ClassMealSetMenuSettingMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("ClassMealSetMenuSettingCollection")]
    public partial class ClassMealSetMenuSettingCollection : esClassMealSetMenuSettingCollection, IEnumerable<ClassMealSetMenuSetting>
    {
        public ClassMealSetMenuSettingCollection()
        {

        }

        public static implicit operator List<ClassMealSetMenuSetting>(ClassMealSetMenuSettingCollection coll)
        {
            List<ClassMealSetMenuSetting> list = new List<ClassMealSetMenuSetting>();

            foreach (ClassMealSetMenuSetting emp in coll)
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
                return ClassMealSetMenuSettingMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ClassMealSetMenuSettingQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new ClassMealSetMenuSetting(row);
        }

        override protected esEntity CreateEntity()
        {
            return new ClassMealSetMenuSetting();
        }

        #endregion

        [BrowsableAttribute(false)]
        public ClassMealSetMenuSettingQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ClassMealSetMenuSettingQuery();
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
        public bool Load(ClassMealSetMenuSettingQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public ClassMealSetMenuSetting AddNew()
        {
            ClassMealSetMenuSetting entity = base.AddNewEntity() as ClassMealSetMenuSetting;

            return entity;
        }
        public ClassMealSetMenuSetting FindByPrimaryKey(String classID, String sRMealSet)
        {
            return base.FindByPrimaryKey(classID, sRMealSet) as ClassMealSetMenuSetting;
        }

        #region IEnumerable< ClassMealSetMenuSetting> Members

        IEnumerator<ClassMealSetMenuSetting> IEnumerable<ClassMealSetMenuSetting>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as ClassMealSetMenuSetting;
            }
        }

        #endregion

        private ClassMealSetMenuSettingQuery query;
    }


    /// <summary>
    /// Encapsulates the 'ClassMealSetMenuSetting' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("ClassMealSetMenuSetting ({ClassID, SRMealSet})")]
    [Serializable]
    public partial class ClassMealSetMenuSetting : esClassMealSetMenuSetting
    {
        public ClassMealSetMenuSetting()
        {
        }

        public ClassMealSetMenuSetting(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return ClassMealSetMenuSettingMetadata.Meta();
            }
        }

        override protected esClassMealSetMenuSettingQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ClassMealSetMenuSettingQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public ClassMealSetMenuSettingQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ClassMealSetMenuSettingQuery();
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
        public bool Load(ClassMealSetMenuSettingQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private ClassMealSetMenuSettingQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class ClassMealSetMenuSettingQuery : esClassMealSetMenuSettingQuery
    {
        public ClassMealSetMenuSettingQuery()
        {

        }

        public ClassMealSetMenuSettingQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "ClassMealSetMenuSettingQuery";
        }
    }

    [Serializable]
    public partial class ClassMealSetMenuSettingMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected ClassMealSetMenuSettingMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(ClassMealSetMenuSettingMetadata.ColumnNames.ClassID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = ClassMealSetMenuSettingMetadata.PropertyNames.ClassID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(ClassMealSetMenuSettingMetadata.ColumnNames.SRMealSet, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = ClassMealSetMenuSettingMetadata.PropertyNames.SRMealSet;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(ClassMealSetMenuSettingMetadata.ColumnNames.IsOptional, 2, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ClassMealSetMenuSettingMetadata.PropertyNames.IsOptional;
            _columns.Add(c);

            c = new esColumnMetadata(ClassMealSetMenuSettingMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ClassMealSetMenuSettingMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ClassMealSetMenuSettingMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = ClassMealSetMenuSettingMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public ClassMealSetMenuSettingMetadata Meta()
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
            public const string ClassID = "ClassID";
            public const string SRMealSet = "SRMealSet";
            public const string IsOptional = "IsOptional";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string ClassID = "ClassID";
            public const string SRMealSet = "SRMealSet";
            public const string IsOptional = "IsOptional";
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
            lock (typeof(ClassMealSetMenuSettingMetadata))
            {
                if (ClassMealSetMenuSettingMetadata.mapDelegates == null)
                {
                    ClassMealSetMenuSettingMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (ClassMealSetMenuSettingMetadata.meta == null)
                {
                    ClassMealSetMenuSettingMetadata.meta = new ClassMealSetMenuSettingMetadata();
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

                meta.AddTypeMap("ClassID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRMealSet", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsOptional", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "ClassMealSetMenuSetting";
                meta.Destination = "ClassMealSetMenuSetting";
                meta.spInsert = "proc_ClassMealSetMenuSettingInsert";
                meta.spUpdate = "proc_ClassMealSetMenuSettingUpdate";
                meta.spDelete = "proc_ClassMealSetMenuSettingDelete";
                meta.spLoadAll = "proc_ClassMealSetMenuSettingLoadAll";
                meta.spLoadByPrimaryKey = "proc_ClassMealSetMenuSettingLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private ClassMealSetMenuSettingMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
