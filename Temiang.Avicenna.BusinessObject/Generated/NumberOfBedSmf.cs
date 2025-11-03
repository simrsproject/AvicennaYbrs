/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 3/11/2020 1:16:33 PM
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
    abstract public class esNumberOfBedSmfCollection : esEntityCollectionWAuditLog
    {
        public esNumberOfBedSmfCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "NumberOfBedSmfCollection";
        }

        #region Query Logic
        protected void InitQuery(esNumberOfBedSmfQuery query)
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
            this.InitQuery(query as esNumberOfBedSmfQuery);
        }
        #endregion

        virtual public NumberOfBedSmf DetachEntity(NumberOfBedSmf entity)
        {
            return base.DetachEntity(entity) as NumberOfBedSmf;
        }

        virtual public NumberOfBedSmf AttachEntity(NumberOfBedSmf entity)
        {
            return base.AttachEntity(entity) as NumberOfBedSmf;
        }

        virtual public void Combine(NumberOfBedSmfCollection collection)
        {
            base.Combine(collection);
        }

        new public NumberOfBedSmf this[int index]
        {
            get
            {
                return base[index] as NumberOfBedSmf;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(NumberOfBedSmf);
        }
    }

    [Serializable]
    abstract public class esNumberOfBedSmf : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esNumberOfBedSmfQuery GetDynamicQuery()
        {
            return null;
        }

        public esNumberOfBedSmf()
        {
        }

        public esNumberOfBedSmf(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(DateTime startingDate, String classID, String smfID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(startingDate, classID, smfID);
            else
                return LoadByPrimaryKeyStoredProcedure(startingDate, classID, smfID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, DateTime startingDate, String classID, String smfID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(startingDate, classID, smfID);
            else
                return LoadByPrimaryKeyStoredProcedure(startingDate, classID, smfID);
        }

        private bool LoadByPrimaryKeyDynamic(DateTime startingDate, String classID, String smfID)
        {
            esNumberOfBedSmfQuery query = this.GetDynamicQuery();
            query.Where(query.StartingDate == startingDate, query.ClassID == classID, query.SmfID == smfID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(DateTime startingDate, String classID, String smfID)
        {
            esParameters parms = new esParameters();
            parms.Add("StartingDate", startingDate);
            parms.Add("ClassID", classID);
            parms.Add("SmfID", smfID);
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
                        case "StartingDate": this.str.StartingDate = (string)value; break;
                        case "ClassID": this.str.ClassID = (string)value; break;
                        case "SmfID": this.str.SmfID = (string)value; break;
                        case "NumberOfBed": this.str.NumberOfBed = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "StartingDate":

                            if (value == null || value is System.DateTime)
                                this.StartingDate = (System.DateTime?)value;
                            break;
                        case "NumberOfBed":

                            if (value == null || value is System.Int32)
                                this.NumberOfBed = (System.Int32?)value;
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
        /// Maps to NumberOfBedSmf.StartingDate
        /// </summary>
        virtual public System.DateTime? StartingDate
        {
            get
            {
                return base.GetSystemDateTime(NumberOfBedSmfMetadata.ColumnNames.StartingDate);
            }

            set
            {
                base.SetSystemDateTime(NumberOfBedSmfMetadata.ColumnNames.StartingDate, value);
            }
        }
        /// <summary>
        /// Maps to NumberOfBedSmf.ClassID
        /// </summary>
        virtual public System.String ClassID
        {
            get
            {
                return base.GetSystemString(NumberOfBedSmfMetadata.ColumnNames.ClassID);
            }

            set
            {
                base.SetSystemString(NumberOfBedSmfMetadata.ColumnNames.ClassID, value);
            }
        }
        /// <summary>
        /// Maps to NumberOfBedSmf.SmfID
        /// </summary>
        virtual public System.String SmfID
        {
            get
            {
                return base.GetSystemString(NumberOfBedSmfMetadata.ColumnNames.SmfID);
            }

            set
            {
                base.SetSystemString(NumberOfBedSmfMetadata.ColumnNames.SmfID, value);
            }
        }
        /// <summary>
        /// Maps to NumberOfBedSmf.NumberOfBed
        /// </summary>
        virtual public System.Int32? NumberOfBed
        {
            get
            {
                return base.GetSystemInt32(NumberOfBedSmfMetadata.ColumnNames.NumberOfBed);
            }

            set
            {
                base.SetSystemInt32(NumberOfBedSmfMetadata.ColumnNames.NumberOfBed, value);
            }
        }
        /// <summary>
        /// Maps to NumberOfBedSmf.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(NumberOfBedSmfMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(NumberOfBedSmfMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to NumberOfBedSmf.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(NumberOfBedSmfMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(NumberOfBedSmfMetadata.ColumnNames.LastUpdateDateTime, value);
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
            public esStrings(esNumberOfBedSmf entity)
            {
                this.entity = entity;
            }
            public System.String StartingDate
            {
                get
                {
                    System.DateTime? data = entity.StartingDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.StartingDate = null;
                    else entity.StartingDate = Convert.ToDateTime(value);
                }
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
            public System.String SmfID
            {
                get
                {
                    System.String data = entity.SmfID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SmfID = null;
                    else entity.SmfID = Convert.ToString(value);
                }
            }
            public System.String NumberOfBed
            {
                get
                {
                    System.Int32? data = entity.NumberOfBed;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.NumberOfBed = null;
                    else entity.NumberOfBed = Convert.ToInt32(value);
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
            private esNumberOfBedSmf entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esNumberOfBedSmfQuery query)
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
                throw new Exception("esNumberOfBedSmf can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class NumberOfBedSmf : esNumberOfBedSmf
    {
    }

    [Serializable]
    abstract public class esNumberOfBedSmfQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return NumberOfBedSmfMetadata.Meta();
            }
        }

        public esQueryItem StartingDate
        {
            get
            {
                return new esQueryItem(this, NumberOfBedSmfMetadata.ColumnNames.StartingDate, esSystemType.DateTime);
            }
        }

        public esQueryItem ClassID
        {
            get
            {
                return new esQueryItem(this, NumberOfBedSmfMetadata.ColumnNames.ClassID, esSystemType.String);
            }
        }

        public esQueryItem SmfID
        {
            get
            {
                return new esQueryItem(this, NumberOfBedSmfMetadata.ColumnNames.SmfID, esSystemType.String);
            }
        }

        public esQueryItem NumberOfBed
        {
            get
            {
                return new esQueryItem(this, NumberOfBedSmfMetadata.ColumnNames.NumberOfBed, esSystemType.Int32);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, NumberOfBedSmfMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, NumberOfBedSmfMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("NumberOfBedSmfCollection")]
    public partial class NumberOfBedSmfCollection : esNumberOfBedSmfCollection, IEnumerable<NumberOfBedSmf>
    {
        public NumberOfBedSmfCollection()
        {

        }

        public static implicit operator List<NumberOfBedSmf>(NumberOfBedSmfCollection coll)
        {
            List<NumberOfBedSmf> list = new List<NumberOfBedSmf>();

            foreach (NumberOfBedSmf emp in coll)
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
                return NumberOfBedSmfMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new NumberOfBedSmfQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new NumberOfBedSmf(row);
        }

        override protected esEntity CreateEntity()
        {
            return new NumberOfBedSmf();
        }

        #endregion

        [BrowsableAttribute(false)]
        public NumberOfBedSmfQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new NumberOfBedSmfQuery();
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
        public bool Load(NumberOfBedSmfQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public NumberOfBedSmf AddNew()
        {
            NumberOfBedSmf entity = base.AddNewEntity() as NumberOfBedSmf;

            return entity;
        }
        public NumberOfBedSmf FindByPrimaryKey(DateTime startingDate, String classID, String smfID)
        {
            return base.FindByPrimaryKey(startingDate, classID, smfID) as NumberOfBedSmf;
        }

        #region IEnumerable< NumberOfBedSmf> Members

        IEnumerator<NumberOfBedSmf> IEnumerable<NumberOfBedSmf>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as NumberOfBedSmf;
            }
        }

        #endregion

        private NumberOfBedSmfQuery query;
    }


    /// <summary>
    /// Encapsulates the 'NumberOfBedSmf' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("NumberOfBedSmf ({StartingDate, ClassID, SmfID})")]
    [Serializable]
    public partial class NumberOfBedSmf : esNumberOfBedSmf
    {
        public NumberOfBedSmf()
        {
        }

        public NumberOfBedSmf(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return NumberOfBedSmfMetadata.Meta();
            }
        }

        override protected esNumberOfBedSmfQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new NumberOfBedSmfQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public NumberOfBedSmfQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new NumberOfBedSmfQuery();
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
        public bool Load(NumberOfBedSmfQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private NumberOfBedSmfQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class NumberOfBedSmfQuery : esNumberOfBedSmfQuery
    {
        public NumberOfBedSmfQuery()
        {

        }

        public NumberOfBedSmfQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "NumberOfBedSmfQuery";
        }
    }

    [Serializable]
    public partial class NumberOfBedSmfMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected NumberOfBedSmfMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(NumberOfBedSmfMetadata.ColumnNames.StartingDate, 0, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = NumberOfBedSmfMetadata.PropertyNames.StartingDate;
            c.IsInPrimaryKey = true;
            _columns.Add(c);

            c = new esColumnMetadata(NumberOfBedSmfMetadata.ColumnNames.ClassID, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = NumberOfBedSmfMetadata.PropertyNames.ClassID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(NumberOfBedSmfMetadata.ColumnNames.SmfID, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = NumberOfBedSmfMetadata.PropertyNames.SmfID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(NumberOfBedSmfMetadata.ColumnNames.NumberOfBed, 3, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = NumberOfBedSmfMetadata.PropertyNames.NumberOfBed;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(NumberOfBedSmfMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = NumberOfBedSmfMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NumberOfBedSmfMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = NumberOfBedSmfMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public NumberOfBedSmfMetadata Meta()
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
            public const string StartingDate = "StartingDate";
            public const string ClassID = "ClassID";
            public const string SmfID = "SmfID";
            public const string NumberOfBed = "NumberOfBed";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string StartingDate = "StartingDate";
            public const string ClassID = "ClassID";
            public const string SmfID = "SmfID";
            public const string NumberOfBed = "NumberOfBed";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
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
            lock (typeof(NumberOfBedSmfMetadata))
            {
                if (NumberOfBedSmfMetadata.mapDelegates == null)
                {
                    NumberOfBedSmfMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (NumberOfBedSmfMetadata.meta == null)
                {
                    NumberOfBedSmfMetadata.meta = new NumberOfBedSmfMetadata();
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

                meta.AddTypeMap("StartingDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("ClassID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SmfID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("NumberOfBed", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));


                meta.Source = "NumberOfBedSmf";
                meta.Destination = "NumberOfBedSmf";
                meta.spInsert = "proc_NumberOfBedSmfInsert";
                meta.spUpdate = "proc_NumberOfBedSmfUpdate";
                meta.spDelete = "proc_NumberOfBedSmfDelete";
                meta.spLoadAll = "proc_NumberOfBedSmfLoadAll";
                meta.spLoadByPrimaryKey = "proc_NumberOfBedSmfLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private NumberOfBedSmfMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
