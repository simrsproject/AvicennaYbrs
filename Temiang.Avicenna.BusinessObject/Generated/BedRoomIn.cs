/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 5/8/2017 2:36:20 PM
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
    abstract public class esBedRoomInCollection : esEntityCollectionWAuditLog
    {
        public esBedRoomInCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "BedRoomInCollection";
        }

        #region Query Logic
        protected void InitQuery(esBedRoomInQuery query)
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
            this.InitQuery(query as esBedRoomInQuery);
        }
        #endregion

        virtual public BedRoomIn DetachEntity(BedRoomIn entity)
        {
            return base.DetachEntity(entity) as BedRoomIn;
        }

        virtual public BedRoomIn AttachEntity(BedRoomIn entity)
        {
            return base.AttachEntity(entity) as BedRoomIn;
        }

        virtual public void Combine(BedRoomInCollection collection)
        {
            base.Combine(collection);
        }

        new public BedRoomIn this[int index]
        {
            get
            {
                return base[index] as BedRoomIn;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(BedRoomIn);
        }
    }

    [Serializable]
    abstract public class esBedRoomIn : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esBedRoomInQuery GetDynamicQuery()
        {
            return null;
        }

        public esBedRoomIn()
        {
        }

        public esBedRoomIn(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String bedID, String registrationNo, DateTime dateOfEntry, String timeOfEntry)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(bedID, registrationNo, dateOfEntry, timeOfEntry);
            else
                return LoadByPrimaryKeyStoredProcedure(bedID, registrationNo, dateOfEntry, timeOfEntry);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String bedID, String registrationNo, DateTime dateOfEntry, String timeOfEntry)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(bedID, registrationNo, dateOfEntry, timeOfEntry);
            else
                return LoadByPrimaryKeyStoredProcedure(bedID, registrationNo, dateOfEntry, timeOfEntry);
        }

        private bool LoadByPrimaryKeyDynamic(String bedID, String registrationNo, DateTime dateOfEntry, String timeOfEntry)
        {
            esBedRoomInQuery query = this.GetDynamicQuery();
            query.Where(query.BedID == bedID, query.RegistrationNo == registrationNo, query.DateOfEntry == dateOfEntry, query.TimeOfEntry == timeOfEntry);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String bedID, String registrationNo, DateTime dateOfEntry, String timeOfEntry)
        {
            esParameters parms = new esParameters();
            parms.Add("BedID", bedID);
            parms.Add("RegistrationNo", registrationNo);
            parms.Add("DateOfEntry", dateOfEntry);
            parms.Add("TimeOfEntry", timeOfEntry);
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
                        case "BedID": this.str.BedID = (string)value; break;
                        case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
                        case "DateOfEntry": this.str.DateOfEntry = (string)value; break;
                        case "TimeOfEntry": this.str.TimeOfEntry = (string)value; break;
                        case "DateOfExit": this.str.DateOfExit = (string)value; break;
                        case "TimeOfExit": this.str.TimeOfExit = (string)value; break;
                        case "IsVoid": this.str.IsVoid = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "SRBedStatus": this.str.SRBedStatus = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "DateOfEntry":

                            if (value == null || value is System.DateTime)
                                this.DateOfEntry = (System.DateTime?)value;
                            break;
                        case "DateOfExit":

                            if (value == null || value is System.DateTime)
                                this.DateOfExit = (System.DateTime?)value;
                            break;
                        case "IsVoid":

                            if (value == null || value is System.Boolean)
                                this.IsVoid = (System.Boolean?)value;
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
        /// Maps to BedRoomIn.BedID
        /// </summary>
        virtual public System.String BedID
        {
            get
            {
                return base.GetSystemString(BedRoomInMetadata.ColumnNames.BedID);
            }

            set
            {
                base.SetSystemString(BedRoomInMetadata.ColumnNames.BedID, value);
            }
        }
        /// <summary>
        /// Maps to BedRoomIn.RegistrationNo
        /// </summary>
        virtual public System.String RegistrationNo
        {
            get
            {
                return base.GetSystemString(BedRoomInMetadata.ColumnNames.RegistrationNo);
            }

            set
            {
                base.SetSystemString(BedRoomInMetadata.ColumnNames.RegistrationNo, value);
            }
        }
        /// <summary>
        /// Maps to BedRoomIn.DateOfEntry
        /// </summary>
        virtual public System.DateTime? DateOfEntry
        {
            get
            {
                return base.GetSystemDateTime(BedRoomInMetadata.ColumnNames.DateOfEntry);
            }

            set
            {
                base.SetSystemDateTime(BedRoomInMetadata.ColumnNames.DateOfEntry, value);
            }
        }
        /// <summary>
        /// Maps to BedRoomIn.TimeOfEntry
        /// </summary>
        virtual public System.String TimeOfEntry
        {
            get
            {
                return base.GetSystemString(BedRoomInMetadata.ColumnNames.TimeOfEntry);
            }

            set
            {
                base.SetSystemString(BedRoomInMetadata.ColumnNames.TimeOfEntry, value);
            }
        }
        /// <summary>
        /// Maps to BedRoomIn.DateOfExit
        /// </summary>
        virtual public System.DateTime? DateOfExit
        {
            get
            {
                return base.GetSystemDateTime(BedRoomInMetadata.ColumnNames.DateOfExit);
            }

            set
            {
                base.SetSystemDateTime(BedRoomInMetadata.ColumnNames.DateOfExit, value);
            }
        }
        /// <summary>
        /// Maps to BedRoomIn.TimeOfExit
        /// </summary>
        virtual public System.String TimeOfExit
        {
            get
            {
                return base.GetSystemString(BedRoomInMetadata.ColumnNames.TimeOfExit);
            }

            set
            {
                base.SetSystemString(BedRoomInMetadata.ColumnNames.TimeOfExit, value);
            }
        }
        /// <summary>
        /// Maps to BedRoomIn.IsVoid
        /// </summary>
        virtual public System.Boolean? IsVoid
        {
            get
            {
                return base.GetSystemBoolean(BedRoomInMetadata.ColumnNames.IsVoid);
            }

            set
            {
                base.SetSystemBoolean(BedRoomInMetadata.ColumnNames.IsVoid, value);
            }
        }
        /// <summary>
        /// Maps to BedRoomIn.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(BedRoomInMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(BedRoomInMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to BedRoomIn.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(BedRoomInMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(BedRoomInMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to BedRoomIn.SRBedStatus
        /// </summary>
        virtual public System.String SRBedStatus
        {
            get
            {
                return base.GetSystemString(BedRoomInMetadata.ColumnNames.SRBedStatus);
            }

            set
            {
                base.SetSystemString(BedRoomInMetadata.ColumnNames.SRBedStatus, value);
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
            public esStrings(esBedRoomIn entity)
            {
                this.entity = entity;
            }
            public System.String BedID
            {
                get
                {
                    System.String data = entity.BedID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.BedID = null;
                    else entity.BedID = Convert.ToString(value);
                }
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
            public System.String DateOfEntry
            {
                get
                {
                    System.DateTime? data = entity.DateOfEntry;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DateOfEntry = null;
                    else entity.DateOfEntry = Convert.ToDateTime(value);
                }
            }
            public System.String TimeOfEntry
            {
                get
                {
                    System.String data = entity.TimeOfEntry;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TimeOfEntry = null;
                    else entity.TimeOfEntry = Convert.ToString(value);
                }
            }
            public System.String DateOfExit
            {
                get
                {
                    System.DateTime? data = entity.DateOfExit;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DateOfExit = null;
                    else entity.DateOfExit = Convert.ToDateTime(value);
                }
            }
            public System.String TimeOfExit
            {
                get
                {
                    System.String data = entity.TimeOfExit;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TimeOfExit = null;
                    else entity.TimeOfExit = Convert.ToString(value);
                }
            }
            public System.String IsVoid
            {
                get
                {
                    System.Boolean? data = entity.IsVoid;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsVoid = null;
                    else entity.IsVoid = Convert.ToBoolean(value);
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
            public System.String SRBedStatus
            {
                get
                {
                    System.String data = entity.SRBedStatus;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRBedStatus = null;
                    else entity.SRBedStatus = Convert.ToString(value);
                }
            }
            private esBedRoomIn entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esBedRoomInQuery query)
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
                throw new Exception("esBedRoomIn can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class BedRoomIn : esBedRoomIn
    {
    }

    [Serializable]
    abstract public class esBedRoomInQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return BedRoomInMetadata.Meta();
            }
        }

        public esQueryItem BedID
        {
            get
            {
                return new esQueryItem(this, BedRoomInMetadata.ColumnNames.BedID, esSystemType.String);
            }
        }

        public esQueryItem RegistrationNo
        {
            get
            {
                return new esQueryItem(this, BedRoomInMetadata.ColumnNames.RegistrationNo, esSystemType.String);
            }
        }

        public esQueryItem DateOfEntry
        {
            get
            {
                return new esQueryItem(this, BedRoomInMetadata.ColumnNames.DateOfEntry, esSystemType.DateTime);
            }
        }

        public esQueryItem TimeOfEntry
        {
            get
            {
                return new esQueryItem(this, BedRoomInMetadata.ColumnNames.TimeOfEntry, esSystemType.String);
            }
        }

        public esQueryItem DateOfExit
        {
            get
            {
                return new esQueryItem(this, BedRoomInMetadata.ColumnNames.DateOfExit, esSystemType.DateTime);
            }
        }

        public esQueryItem TimeOfExit
        {
            get
            {
                return new esQueryItem(this, BedRoomInMetadata.ColumnNames.TimeOfExit, esSystemType.String);
            }
        }

        public esQueryItem IsVoid
        {
            get
            {
                return new esQueryItem(this, BedRoomInMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, BedRoomInMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, BedRoomInMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem SRBedStatus
        {
            get
            {
                return new esQueryItem(this, BedRoomInMetadata.ColumnNames.SRBedStatus, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("BedRoomInCollection")]
    public partial class BedRoomInCollection : esBedRoomInCollection, IEnumerable<BedRoomIn>
    {
        public BedRoomInCollection()
        {

        }

        public static implicit operator List<BedRoomIn>(BedRoomInCollection coll)
        {
            List<BedRoomIn> list = new List<BedRoomIn>();

            foreach (BedRoomIn emp in coll)
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
                return BedRoomInMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new BedRoomInQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new BedRoomIn(row);
        }

        override protected esEntity CreateEntity()
        {
            return new BedRoomIn();
        }

        #endregion

        [BrowsableAttribute(false)]
        public BedRoomInQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new BedRoomInQuery();
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
        public bool Load(BedRoomInQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public BedRoomIn AddNew()
        {
            BedRoomIn entity = base.AddNewEntity() as BedRoomIn;

            return entity;
        }
        public BedRoomIn FindByPrimaryKey(String bedID, String registrationNo, DateTime dateOfEntry, String timeOfEntry)
        {
            return base.FindByPrimaryKey(bedID, registrationNo, dateOfEntry, timeOfEntry) as BedRoomIn;
        }

        #region IEnumerable< BedRoomIn> Members

        IEnumerator<BedRoomIn> IEnumerable<BedRoomIn>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as BedRoomIn;
            }
        }

        #endregion

        private BedRoomInQuery query;
    }


    /// <summary>
    /// Encapsulates the 'BedRoomIn' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("BedRoomIn ({BedID, RegistrationNo, DateOfEntry, TimeOfEntry})")]
    [Serializable]
    public partial class BedRoomIn : esBedRoomIn
    {
        public BedRoomIn()
        {
        }

        public BedRoomIn(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return BedRoomInMetadata.Meta();
            }
        }

        override protected esBedRoomInQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new BedRoomInQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public BedRoomInQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new BedRoomInQuery();
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
        public bool Load(BedRoomInQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private BedRoomInQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class BedRoomInQuery : esBedRoomInQuery
    {
        public BedRoomInQuery()
        {

        }

        public BedRoomInQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "BedRoomInQuery";
        }
    }

    [Serializable]
    public partial class BedRoomInMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected BedRoomInMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(BedRoomInMetadata.ColumnNames.BedID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = BedRoomInMetadata.PropertyNames.BedID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(BedRoomInMetadata.ColumnNames.RegistrationNo, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = BedRoomInMetadata.PropertyNames.RegistrationNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(BedRoomInMetadata.ColumnNames.DateOfEntry, 2, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = BedRoomInMetadata.PropertyNames.DateOfEntry;
            c.IsInPrimaryKey = true;
            _columns.Add(c);

            c = new esColumnMetadata(BedRoomInMetadata.ColumnNames.TimeOfEntry, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = BedRoomInMetadata.PropertyNames.TimeOfEntry;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 5;
            _columns.Add(c);

            c = new esColumnMetadata(BedRoomInMetadata.ColumnNames.DateOfExit, 4, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = BedRoomInMetadata.PropertyNames.DateOfExit;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BedRoomInMetadata.ColumnNames.TimeOfExit, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = BedRoomInMetadata.PropertyNames.TimeOfExit;
            c.CharacterMaxLength = 5;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BedRoomInMetadata.ColumnNames.IsVoid, 6, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = BedRoomInMetadata.PropertyNames.IsVoid;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BedRoomInMetadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = BedRoomInMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BedRoomInMetadata.ColumnNames.LastUpdateByUserID, 8, typeof(System.String), esSystemType.String);
            c.PropertyName = BedRoomInMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BedRoomInMetadata.ColumnNames.SRBedStatus, 9, typeof(System.String), esSystemType.String);
            c.PropertyName = BedRoomInMetadata.PropertyNames.SRBedStatus;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public BedRoomInMetadata Meta()
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
            public const string BedID = "BedID";
            public const string RegistrationNo = "RegistrationNo";
            public const string DateOfEntry = "DateOfEntry";
            public const string TimeOfEntry = "TimeOfEntry";
            public const string DateOfExit = "DateOfExit";
            public const string TimeOfExit = "TimeOfExit";
            public const string IsVoid = "IsVoid";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string SRBedStatus = "SRBedStatus";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string BedID = "BedID";
            public const string RegistrationNo = "RegistrationNo";
            public const string DateOfEntry = "DateOfEntry";
            public const string TimeOfEntry = "TimeOfEntry";
            public const string DateOfExit = "DateOfExit";
            public const string TimeOfExit = "TimeOfExit";
            public const string IsVoid = "IsVoid";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string SRBedStatus = "SRBedStatus";
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
            lock (typeof(BedRoomInMetadata))
            {
                if (BedRoomInMetadata.mapDelegates == null)
                {
                    BedRoomInMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (BedRoomInMetadata.meta == null)
                {
                    BedRoomInMetadata.meta = new BedRoomInMetadata();
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

                meta.AddTypeMap("BedID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("DateOfEntry", new esTypeMap("smalldatetime", "System.DateTime"));
                meta.AddTypeMap("TimeOfEntry", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("DateOfExit", new esTypeMap("smalldatetime", "System.DateTime"));
                meta.AddTypeMap("TimeOfExit", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRBedStatus", new esTypeMap("varchar", "System.String"));


                meta.Source = "BedRoomIn";
                meta.Destination = "BedRoomIn";
                meta.spInsert = "proc_BedRoomInInsert";
                meta.spUpdate = "proc_BedRoomInUpdate";
                meta.spDelete = "proc_BedRoomInDelete";
                meta.spLoadAll = "proc_BedRoomInLoadAll";
                meta.spLoadByPrimaryKey = "proc_BedRoomInLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private BedRoomInMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
