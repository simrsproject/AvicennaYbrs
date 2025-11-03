/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 05/18/18 8:05:14 AM
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
    abstract public class esSickLetterCollection : esEntityCollectionWAuditLog
    {
        public esSickLetterCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "SickLetterCollection";
        }

        #region Query Logic
        protected void InitQuery(esSickLetterQuery query)
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
            this.InitQuery(query as esSickLetterQuery);
        }
        #endregion

        virtual public SickLetter DetachEntity(SickLetter entity)
        {
            return base.DetachEntity(entity) as SickLetter;
        }

        virtual public SickLetter AttachEntity(SickLetter entity)
        {
            return base.AttachEntity(entity) as SickLetter;
        }

        virtual public void Combine(SickLetterCollection collection)
        {
            base.Combine(collection);
        }

        new public SickLetter this[int index]
        {
            get
            {
                return base[index] as SickLetter;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(SickLetter);
        }
    }

    [Serializable]
    abstract public class esSickLetter : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esSickLetterQuery GetDynamicQuery()
        {
            return null;
        }

        public esSickLetter()
        {
        }

        public esSickLetter(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String registrationNo, String paramedicID, String sRLetterType)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(registrationNo, paramedicID, sRLetterType);
            else
                return LoadByPrimaryKeyStoredProcedure(registrationNo, paramedicID, sRLetterType);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String registrationNo, String paramedicID, String sRLetterType)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(registrationNo, paramedicID, sRLetterType);
            else
                return LoadByPrimaryKeyStoredProcedure(registrationNo, paramedicID, sRLetterType);
        }

        private bool LoadByPrimaryKeyDynamic(String registrationNo, String paramedicID, String sRLetterType)
        {
            esSickLetterQuery query = this.GetDynamicQuery();
            query.Where(query.RegistrationNo == registrationNo, query.ParamedicID == paramedicID, query.SRLetterType == sRLetterType);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String registrationNo, String paramedicID, String sRLetterType)
        {
            esParameters parms = new esParameters();
            parms.Add("RegistrationNo", registrationNo);
            parms.Add("ParamedicID", paramedicID);
            parms.Add("SRLetterType", sRLetterType);
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
                        case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
                        case "ParamedicID": this.str.ParamedicID = (string)value; break;
                        case "SRLetterType": this.str.SRLetterType = (string)value; break;
                        case "StartDate": this.str.StartDate = (string)value; break;
                        case "EndDate": this.str.EndDate = (string)value; break;
                        case "Notes": this.str.Notes = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "StartDate":

                            if (value == null || value is System.DateTime)
                                this.StartDate = (System.DateTime?)value;
                            break;
                        case "EndDate":

                            if (value == null || value is System.DateTime)
                                this.EndDate = (System.DateTime?)value;
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
        /// Maps to SickLetter.RegistrationNo
        /// </summary>
        virtual public System.String RegistrationNo
        {
            get
            {
                return base.GetSystemString(SickLetterMetadata.ColumnNames.RegistrationNo);
            }

            set
            {
                base.SetSystemString(SickLetterMetadata.ColumnNames.RegistrationNo, value);
            }
        }
        /// <summary>
        /// Maps to SickLetter.ParamedicID
        /// </summary>
        virtual public System.String ParamedicID
        {
            get
            {
                return base.GetSystemString(SickLetterMetadata.ColumnNames.ParamedicID);
            }

            set
            {
                base.SetSystemString(SickLetterMetadata.ColumnNames.ParamedicID, value);
            }
        }
        /// <summary>
        /// Maps to SickLetter.SRLetterType
        /// </summary>
        virtual public System.String SRLetterType
        {
            get
            {
                return base.GetSystemString(SickLetterMetadata.ColumnNames.SRLetterType);
            }

            set
            {
                base.SetSystemString(SickLetterMetadata.ColumnNames.SRLetterType, value);
            }
        }
        /// <summary>
        /// Maps to SickLetter.StartDate
        /// </summary>
        virtual public System.DateTime? StartDate
        {
            get
            {
                return base.GetSystemDateTime(SickLetterMetadata.ColumnNames.StartDate);
            }

            set
            {
                base.SetSystemDateTime(SickLetterMetadata.ColumnNames.StartDate, value);
            }
        }
        /// <summary>
        /// Maps to SickLetter.EndDate
        /// </summary>
        virtual public System.DateTime? EndDate
        {
            get
            {
                return base.GetSystemDateTime(SickLetterMetadata.ColumnNames.EndDate);
            }

            set
            {
                base.SetSystemDateTime(SickLetterMetadata.ColumnNames.EndDate, value);
            }
        }
        /// <summary>
        /// Maps to SickLetter.Notes
        /// </summary>
        virtual public System.String Notes
        {
            get
            {
                return base.GetSystemString(SickLetterMetadata.ColumnNames.Notes);
            }

            set
            {
                base.SetSystemString(SickLetterMetadata.ColumnNames.Notes, value);
            }
        }
        /// <summary>
        /// Maps to SickLetter.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(SickLetterMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(SickLetterMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to SickLetter.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(SickLetterMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(SickLetterMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esSickLetter entity)
            {
                this.entity = entity;
            }
            public System.String RegistrationNo
            {
                get
                {
                    System.String data = entity.RegistrationNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RegistrationNo = null;
                    else entity.RegistrationNo = Convert.ToString(value);
                }
            }
            public System.String ParamedicID
            {
                get
                {
                    System.String data = entity.ParamedicID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ParamedicID = null;
                    else entity.ParamedicID = Convert.ToString(value);
                }
            }
            public System.String SRLetterType
            {
                get
                {
                    System.String data = entity.SRLetterType;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRLetterType = null;
                    else entity.SRLetterType = Convert.ToString(value);
                }
            }
            public System.String StartDate
            {
                get
                {
                    System.DateTime? data = entity.StartDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.StartDate = null;
                    else entity.StartDate = Convert.ToDateTime(value);
                }
            }
            public System.String EndDate
            {
                get
                {
                    System.DateTime? data = entity.EndDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.EndDate = null;
                    else entity.EndDate = Convert.ToDateTime(value);
                }
            }
            public System.String Notes
            {
                get
                {
                    System.String data = entity.Notes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Notes = null;
                    else entity.Notes = Convert.ToString(value);
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
            private esSickLetter entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esSickLetterQuery query)
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
                throw new Exception("esSickLetter can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class SickLetter : esSickLetter
    {
    }

    [Serializable]
    abstract public class esSickLetterQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return SickLetterMetadata.Meta();
            }
        }

        public esQueryItem RegistrationNo
        {
            get
            {
                return new esQueryItem(this, SickLetterMetadata.ColumnNames.RegistrationNo, esSystemType.String);
            }
        }

        public esQueryItem ParamedicID
        {
            get
            {
                return new esQueryItem(this, SickLetterMetadata.ColumnNames.ParamedicID, esSystemType.String);
            }
        }

        public esQueryItem SRLetterType
        {
            get
            {
                return new esQueryItem(this, SickLetterMetadata.ColumnNames.SRLetterType, esSystemType.String);
            }
        }

        public esQueryItem StartDate
        {
            get
            {
                return new esQueryItem(this, SickLetterMetadata.ColumnNames.StartDate, esSystemType.DateTime);
            }
        }

        public esQueryItem EndDate
        {
            get
            {
                return new esQueryItem(this, SickLetterMetadata.ColumnNames.EndDate, esSystemType.DateTime);
            }
        }

        public esQueryItem Notes
        {
            get
            {
                return new esQueryItem(this, SickLetterMetadata.ColumnNames.Notes, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, SickLetterMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, SickLetterMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("SickLetterCollection")]
    public partial class SickLetterCollection : esSickLetterCollection, IEnumerable<SickLetter>
    {
        public SickLetterCollection()
        {

        }

        public static implicit operator List<SickLetter>(SickLetterCollection coll)
        {
            List<SickLetter> list = new List<SickLetter>();

            foreach (SickLetter emp in coll)
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
                return SickLetterMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new SickLetterQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new SickLetter(row);
        }

        override protected esEntity CreateEntity()
        {
            return new SickLetter();
        }

        #endregion

        [BrowsableAttribute(false)]
        public SickLetterQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new SickLetterQuery();
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
        public bool Load(SickLetterQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public SickLetter AddNew()
        {
            SickLetter entity = base.AddNewEntity() as SickLetter;

            return entity;
        }
        public SickLetter FindByPrimaryKey(String registrationNo, String paramedicID, String sRLetterType)
        {
            return base.FindByPrimaryKey(registrationNo, paramedicID, sRLetterType) as SickLetter;
        }

        #region IEnumerable< SickLetter> Members

        IEnumerator<SickLetter> IEnumerable<SickLetter>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as SickLetter;
            }
        }

        #endregion

        private SickLetterQuery query;
    }


    /// <summary>
    /// Encapsulates the 'SickLetter' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("SickLetter ({RegistrationNo, ParamedicID, SRLetterType})")]
    [Serializable]
    public partial class SickLetter : esSickLetter
    {
        public SickLetter()
        {
        }

        public SickLetter(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return SickLetterMetadata.Meta();
            }
        }

        override protected esSickLetterQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new SickLetterQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public SickLetterQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new SickLetterQuery();
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
        public bool Load(SickLetterQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private SickLetterQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class SickLetterQuery : esSickLetterQuery
    {
        public SickLetterQuery()
        {

        }

        public SickLetterQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "SickLetterQuery";
        }
    }

    [Serializable]
    public partial class SickLetterMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected SickLetterMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(SickLetterMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = SickLetterMetadata.PropertyNames.RegistrationNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(SickLetterMetadata.ColumnNames.ParamedicID, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = SickLetterMetadata.PropertyNames.ParamedicID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(SickLetterMetadata.ColumnNames.SRLetterType, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = SickLetterMetadata.PropertyNames.SRLetterType;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            c.HasDefault = true;
            c.Default = @"('SL')";
            _columns.Add(c);

            c = new esColumnMetadata(SickLetterMetadata.ColumnNames.StartDate, 3, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = SickLetterMetadata.PropertyNames.StartDate;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(SickLetterMetadata.ColumnNames.EndDate, 4, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = SickLetterMetadata.PropertyNames.EndDate;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(SickLetterMetadata.ColumnNames.Notes, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = SickLetterMetadata.PropertyNames.Notes;
            c.CharacterMaxLength = 500;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(SickLetterMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = SickLetterMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(SickLetterMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = SickLetterMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public SickLetterMetadata Meta()
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
            public const string RegistrationNo = "RegistrationNo";
            public const string ParamedicID = "ParamedicID";
            public const string SRLetterType = "SRLetterType";
            public const string StartDate = "StartDate";
            public const string EndDate = "EndDate";
            public const string Notes = "Notes";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string RegistrationNo = "RegistrationNo";
            public const string ParamedicID = "ParamedicID";
            public const string SRLetterType = "SRLetterType";
            public const string StartDate = "StartDate";
            public const string EndDate = "EndDate";
            public const string Notes = "Notes";
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
            lock (typeof(SickLetterMetadata))
            {
                if (SickLetterMetadata.mapDelegates == null)
                {
                    SickLetterMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (SickLetterMetadata.meta == null)
                {
                    SickLetterMetadata.meta = new SickLetterMetadata();
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

                meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRLetterType", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("StartDate", new esTypeMap("smalldatetime", "System.DateTime"));
                meta.AddTypeMap("EndDate", new esTypeMap("smalldatetime", "System.DateTime"));
                meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "SickLetter";
                meta.Destination = "SickLetter";
                meta.spInsert = "proc_SickLetterInsert";
                meta.spUpdate = "proc_SickLetterUpdate";
                meta.spDelete = "proc_SickLetterDelete";
                meta.spLoadAll = "proc_SickLetterLoadAll";
                meta.spLoadByPrimaryKey = "proc_SickLetterLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private SickLetterMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
