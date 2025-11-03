/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 09/13/19 2:57:01 PM
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
    abstract public class esRegistrationInfoSumaryCollection : esEntityCollectionWAuditLog
    {
        public esRegistrationInfoSumaryCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "RegistrationInfoSumaryCollection";
        }

        #region Query Logic
        protected void InitQuery(esRegistrationInfoSumaryQuery query)
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
            this.InitQuery(query as esRegistrationInfoSumaryQuery);
        }
        #endregion

        virtual public RegistrationInfoSumary DetachEntity(RegistrationInfoSumary entity)
        {
            return base.DetachEntity(entity) as RegistrationInfoSumary;
        }

        virtual public RegistrationInfoSumary AttachEntity(RegistrationInfoSumary entity)
        {
            return base.AttachEntity(entity) as RegistrationInfoSumary;
        }

        virtual public void Combine(RegistrationInfoSumaryCollection collection)
        {
            base.Combine(collection);
        }

        new public RegistrationInfoSumary this[int index]
        {
            get
            {
                return base[index] as RegistrationInfoSumary;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(RegistrationInfoSumary);
        }
    }

    [Serializable]
    abstract public class esRegistrationInfoSumary : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esRegistrationInfoSumaryQuery GetDynamicQuery()
        {
            return null;
        }

        public esRegistrationInfoSumary()
        {
        }

        public esRegistrationInfoSumary(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String registrationNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(registrationNo);
            else
                return LoadByPrimaryKeyStoredProcedure(registrationNo);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String registrationNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(registrationNo);
            else
                return LoadByPrimaryKeyStoredProcedure(registrationNo);
        }

        private bool LoadByPrimaryKeyDynamic(String registrationNo)
        {
            esRegistrationInfoSumaryQuery query = this.GetDynamicQuery();
            query.Where(query.RegistrationNo == registrationNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String registrationNo)
        {
            esParameters parms = new esParameters();
            parms.Add("RegistrationNo", registrationNo);
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
                        case "NoteCount": this.str.NoteCount = (string)value; break;
                        case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;
                        case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "NoteMedicalCount": this.str.NoteMedicalCount = (string)value; break;
                        case "DocumentCheckListCount": this.str.DocumentCheckListCount = (string)value; break;
                        case "DocumentCheckListCountRemains": this.str.DocumentCheckListCountRemains = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "NoteCount":

                            if (value == null || value is System.Int32)
                                this.NoteCount = (System.Int32?)value;
                            break;
                        case "CreatedDateTime":

                            if (value == null || value is System.DateTime)
                                this.CreatedDateTime = (System.DateTime?)value;
                            break;
                        case "LastUpdateDateTime":

                            if (value == null || value is System.DateTime)
                                this.LastUpdateDateTime = (System.DateTime?)value;
                            break;
                        case "NoteMedicalCount":

                            if (value == null || value is System.Int32)
                                this.NoteMedicalCount = (System.Int32?)value;
                            break;
                        case "DocumentCheckListCount":

                            if (value == null || value is System.Int32)
                                this.DocumentCheckListCount = (System.Int32?)value;
                            break;
                        case "DocumentCheckListCountRemains":

                            if (value == null || value is System.Int32)
                                this.DocumentCheckListCountRemains = (System.Int32?)value;
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
        /// Maps to RegistrationInfoSumary.RegistrationNo
        /// </summary>
        virtual public System.String RegistrationNo
        {
            get
            {
                return base.GetSystemString(RegistrationInfoSumaryMetadata.ColumnNames.RegistrationNo);
            }

            set
            {
                base.SetSystemString(RegistrationInfoSumaryMetadata.ColumnNames.RegistrationNo, value);
            }
        }
        /// <summary>
        /// Maps to RegistrationInfoSumary.NoteCount
        /// </summary>
        virtual public System.Int32? NoteCount
        {
            get
            {
                return base.GetSystemInt32(RegistrationInfoSumaryMetadata.ColumnNames.NoteCount);
            }

            set
            {
                base.SetSystemInt32(RegistrationInfoSumaryMetadata.ColumnNames.NoteCount, value);
            }
        }
        /// <summary>
        /// Maps to RegistrationInfoSumary.CreatedByUserID
        /// </summary>
        virtual public System.String CreatedByUserID
        {
            get
            {
                return base.GetSystemString(RegistrationInfoSumaryMetadata.ColumnNames.CreatedByUserID);
            }

            set
            {
                base.SetSystemString(RegistrationInfoSumaryMetadata.ColumnNames.CreatedByUserID, value);
            }
        }
        /// <summary>
        /// Maps to RegistrationInfoSumary.CreatedDateTime
        /// </summary>
        virtual public System.DateTime? CreatedDateTime
        {
            get
            {
                return base.GetSystemDateTime(RegistrationInfoSumaryMetadata.ColumnNames.CreatedDateTime);
            }

            set
            {
                base.SetSystemDateTime(RegistrationInfoSumaryMetadata.ColumnNames.CreatedDateTime, value);
            }
        }
        /// <summary>
        /// Maps to RegistrationInfoSumary.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(RegistrationInfoSumaryMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(RegistrationInfoSumaryMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to RegistrationInfoSumary.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(RegistrationInfoSumaryMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(RegistrationInfoSumaryMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to RegistrationInfoSumary.NoteMedicalCount
        /// </summary>
        virtual public System.Int32? NoteMedicalCount
        {
            get
            {
                return base.GetSystemInt32(RegistrationInfoSumaryMetadata.ColumnNames.NoteMedicalCount);
            }

            set
            {
                base.SetSystemInt32(RegistrationInfoSumaryMetadata.ColumnNames.NoteMedicalCount, value);
            }
        }
        /// <summary>
        /// Maps to RegistrationInfoSumary.DocumentCheckListCount
        /// </summary>
        virtual public System.Int32? DocumentCheckListCount
        {
            get
            {
                return base.GetSystemInt32(RegistrationInfoSumaryMetadata.ColumnNames.DocumentCheckListCount);
            }

            set
            {
                base.SetSystemInt32(RegistrationInfoSumaryMetadata.ColumnNames.DocumentCheckListCount, value);
            }
        }
        /// <summary>
        /// Maps to RegistrationInfoSumary.DocumentCheckListCountRemains
        /// </summary>
        virtual public System.Int32? DocumentCheckListCountRemains
        {
            get
            {
                return base.GetSystemInt32(RegistrationInfoSumaryMetadata.ColumnNames.DocumentCheckListCountRemains);
            }

            set
            {
                base.SetSystemInt32(RegistrationInfoSumaryMetadata.ColumnNames.DocumentCheckListCountRemains, value);
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
            public esStrings(esRegistrationInfoSumary entity)
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
            public System.String NoteCount
            {
                get
                {
                    System.Int32? data = entity.NoteCount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.NoteCount = null;
                    else entity.NoteCount = Convert.ToInt32(value);
                }
            }
            public System.String CreatedByUserID
            {
                get
                {
                    System.String data = entity.CreatedByUserID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CreatedByUserID = null;
                    else entity.CreatedByUserID = Convert.ToString(value);
                }
            }
            public System.String CreatedDateTime
            {
                get
                {
                    System.DateTime? data = entity.CreatedDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CreatedDateTime = null;
                    else entity.CreatedDateTime = Convert.ToDateTime(value);
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
            public System.String NoteMedicalCount
            {
                get
                {
                    System.Int32? data = entity.NoteMedicalCount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.NoteMedicalCount = null;
                    else entity.NoteMedicalCount = Convert.ToInt32(value);
                }
            }
            public System.String DocumentCheckListCount
            {
                get
                {
                    System.Int32? data = entity.DocumentCheckListCount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DocumentCheckListCount = null;
                    else entity.DocumentCheckListCount = Convert.ToInt32(value);
                }
            }
            public System.String DocumentCheckListCountRemains
            {
                get
                {
                    System.Int32? data = entity.DocumentCheckListCountRemains;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DocumentCheckListCountRemains = null;
                    else entity.DocumentCheckListCountRemains = Convert.ToInt32(value);
                }
            }
            private esRegistrationInfoSumary entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esRegistrationInfoSumaryQuery query)
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
                throw new Exception("esRegistrationInfoSumary can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class RegistrationInfoSumary : esRegistrationInfoSumary
    {
    }

    [Serializable]
    abstract public class esRegistrationInfoSumaryQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return RegistrationInfoSumaryMetadata.Meta();
            }
        }

        public esQueryItem RegistrationNo
        {
            get
            {
                return new esQueryItem(this, RegistrationInfoSumaryMetadata.ColumnNames.RegistrationNo, esSystemType.String);
            }
        }

        public esQueryItem NoteCount
        {
            get
            {
                return new esQueryItem(this, RegistrationInfoSumaryMetadata.ColumnNames.NoteCount, esSystemType.Int32);
            }
        }

        public esQueryItem CreatedByUserID
        {
            get
            {
                return new esQueryItem(this, RegistrationInfoSumaryMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
            }
        }

        public esQueryItem CreatedDateTime
        {
            get
            {
                return new esQueryItem(this, RegistrationInfoSumaryMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, RegistrationInfoSumaryMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, RegistrationInfoSumaryMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem NoteMedicalCount
        {
            get
            {
                return new esQueryItem(this, RegistrationInfoSumaryMetadata.ColumnNames.NoteMedicalCount, esSystemType.Int32);
            }
        }

        public esQueryItem DocumentCheckListCount
        {
            get
            {
                return new esQueryItem(this, RegistrationInfoSumaryMetadata.ColumnNames.DocumentCheckListCount, esSystemType.Int32);
            }
        }

        public esQueryItem DocumentCheckListCountRemains
        {
            get
            {
                return new esQueryItem(this, RegistrationInfoSumaryMetadata.ColumnNames.DocumentCheckListCountRemains, esSystemType.Int32);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("RegistrationInfoSumaryCollection")]
    public partial class RegistrationInfoSumaryCollection : esRegistrationInfoSumaryCollection, IEnumerable<RegistrationInfoSumary>
    {
        public RegistrationInfoSumaryCollection()
        {

        }

        public static implicit operator List<RegistrationInfoSumary>(RegistrationInfoSumaryCollection coll)
        {
            List<RegistrationInfoSumary> list = new List<RegistrationInfoSumary>();

            foreach (RegistrationInfoSumary emp in coll)
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
                return RegistrationInfoSumaryMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new RegistrationInfoSumaryQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new RegistrationInfoSumary(row);
        }

        override protected esEntity CreateEntity()
        {
            return new RegistrationInfoSumary();
        }

        #endregion

        [BrowsableAttribute(false)]
        public RegistrationInfoSumaryQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new RegistrationInfoSumaryQuery();
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
        public bool Load(RegistrationInfoSumaryQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public RegistrationInfoSumary AddNew()
        {
            RegistrationInfoSumary entity = base.AddNewEntity() as RegistrationInfoSumary;

            return entity;
        }
        public RegistrationInfoSumary FindByPrimaryKey(String registrationNo)
        {
            return base.FindByPrimaryKey(registrationNo) as RegistrationInfoSumary;
        }

        #region IEnumerable< RegistrationInfoSumary> Members

        IEnumerator<RegistrationInfoSumary> IEnumerable<RegistrationInfoSumary>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as RegistrationInfoSumary;
            }
        }

        #endregion

        private RegistrationInfoSumaryQuery query;
    }


    /// <summary>
    /// Encapsulates the 'RegistrationInfoSumary' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("RegistrationInfoSumary ({RegistrationNo})")]
    [Serializable]
    public partial class RegistrationInfoSumary : esRegistrationInfoSumary
    {
        public RegistrationInfoSumary()
        {
        }

        public RegistrationInfoSumary(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return RegistrationInfoSumaryMetadata.Meta();
            }
        }

        override protected esRegistrationInfoSumaryQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new RegistrationInfoSumaryQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public RegistrationInfoSumaryQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new RegistrationInfoSumaryQuery();
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
        public bool Load(RegistrationInfoSumaryQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private RegistrationInfoSumaryQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class RegistrationInfoSumaryQuery : esRegistrationInfoSumaryQuery
    {
        public RegistrationInfoSumaryQuery()
        {

        }

        public RegistrationInfoSumaryQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "RegistrationInfoSumaryQuery";
        }
    }

    [Serializable]
    public partial class RegistrationInfoSumaryMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected RegistrationInfoSumaryMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(RegistrationInfoSumaryMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = RegistrationInfoSumaryMetadata.PropertyNames.RegistrationNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 50;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationInfoSumaryMetadata.ColumnNames.NoteCount, 1, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RegistrationInfoSumaryMetadata.PropertyNames.NoteCount;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationInfoSumaryMetadata.ColumnNames.CreatedByUserID, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = RegistrationInfoSumaryMetadata.PropertyNames.CreatedByUserID;
            c.CharacterMaxLength = 15;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationInfoSumaryMetadata.ColumnNames.CreatedDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = RegistrationInfoSumaryMetadata.PropertyNames.CreatedDateTime;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationInfoSumaryMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = RegistrationInfoSumaryMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationInfoSumaryMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = RegistrationInfoSumaryMetadata.PropertyNames.LastUpdateDateTime;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationInfoSumaryMetadata.ColumnNames.NoteMedicalCount, 6, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RegistrationInfoSumaryMetadata.PropertyNames.NoteMedicalCount;
            c.NumericPrecision = 10;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationInfoSumaryMetadata.ColumnNames.DocumentCheckListCount, 7, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RegistrationInfoSumaryMetadata.PropertyNames.DocumentCheckListCount;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationInfoSumaryMetadata.ColumnNames.DocumentCheckListCountRemains, 8, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RegistrationInfoSumaryMetadata.PropertyNames.DocumentCheckListCountRemains;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public RegistrationInfoSumaryMetadata Meta()
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
            public const string NoteCount = "NoteCount";
            public const string CreatedByUserID = "CreatedByUserID";
            public const string CreatedDateTime = "CreatedDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string NoteMedicalCount = "NoteMedicalCount";
            public const string DocumentCheckListCount = "DocumentCheckListCount";
            public const string DocumentCheckListCountRemains = "DocumentCheckListCountRemains";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string RegistrationNo = "RegistrationNo";
            public const string NoteCount = "NoteCount";
            public const string CreatedByUserID = "CreatedByUserID";
            public const string CreatedDateTime = "CreatedDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string NoteMedicalCount = "NoteMedicalCount";
            public const string DocumentCheckListCount = "DocumentCheckListCount";
            public const string DocumentCheckListCountRemains = "DocumentCheckListCountRemains";
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
            lock (typeof(RegistrationInfoSumaryMetadata))
            {
                if (RegistrationInfoSumaryMetadata.mapDelegates == null)
                {
                    RegistrationInfoSumaryMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (RegistrationInfoSumaryMetadata.meta == null)
                {
                    RegistrationInfoSumaryMetadata.meta = new RegistrationInfoSumaryMetadata();
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
                meta.AddTypeMap("NoteCount", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("NoteMedicalCount", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("DocumentCheckListCount", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("DocumentCheckListCountRemains", new esTypeMap("int", "System.Int32"));


                meta.Source = "RegistrationInfoSumary";
                meta.Destination = "RegistrationInfoSumary";
                meta.spInsert = "proc_RegistrationInfoSumaryInsert";
                meta.spUpdate = "proc_RegistrationInfoSumaryUpdate";
                meta.spDelete = "proc_RegistrationInfoSumaryDelete";
                meta.spLoadAll = "proc_RegistrationInfoSumaryLoadAll";
                meta.spLoadByPrimaryKey = "proc_RegistrationInfoSumaryLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private RegistrationInfoSumaryMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
