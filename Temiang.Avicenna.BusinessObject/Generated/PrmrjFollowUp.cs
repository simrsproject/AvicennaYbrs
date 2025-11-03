/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/21/18 3:56:53 PM
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
    abstract public class esPrmrjFollowUpCollection : esEntityCollectionWAuditLog
    {
        public esPrmrjFollowUpCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "PrmrjFollowUpCollection";
        }

        #region Query Logic
        protected void InitQuery(esPrmrjFollowUpQuery query)
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
            this.InitQuery(query as esPrmrjFollowUpQuery);
        }
        #endregion

        virtual public PrmrjFollowUp DetachEntity(PrmrjFollowUp entity)
        {
            return base.DetachEntity(entity) as PrmrjFollowUp;
        }

        virtual public PrmrjFollowUp AttachEntity(PrmrjFollowUp entity)
        {
            return base.AttachEntity(entity) as PrmrjFollowUp;
        }

        virtual public void Combine(PrmrjFollowUpCollection collection)
        {
            base.Combine(collection);
        }

        new public PrmrjFollowUp this[int index]
        {
            get
            {
                return base[index] as PrmrjFollowUp;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(PrmrjFollowUp);
        }
    }

    [Serializable]
    abstract public class esPrmrjFollowUp : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esPrmrjFollowUpQuery GetDynamicQuery()
        {
            return null;
        }

        public esPrmrjFollowUp()
        {
        }

        public esPrmrjFollowUp(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String registrationInfoMedicID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(registrationInfoMedicID);
            else
                return LoadByPrimaryKeyStoredProcedure(registrationInfoMedicID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String registrationInfoMedicID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(registrationInfoMedicID);
            else
                return LoadByPrimaryKeyStoredProcedure(registrationInfoMedicID);
        }

        private bool LoadByPrimaryKeyDynamic(String registrationInfoMedicID)
        {
            esPrmrjFollowUpQuery query = this.GetDynamicQuery();
            query.Where(query.RegistrationInfoMedicID == registrationInfoMedicID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String registrationInfoMedicID)
        {
            esParameters parms = new esParameters();
            parms.Add("RegistrationInfoMedicID", registrationInfoMedicID);
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
                        case "RegistrationInfoMedicID": this.str.RegistrationInfoMedicID = (string)value; break;
                        case "ImportantClinicalNotes": this.str.ImportantClinicalNotes = (string)value; break;
                        case "Planning": this.str.Planning = (string)value; break;
                        case "Remark": this.str.Remark = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
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
        /// Maps to PrmrjFollowUp.RegistrationInfoMedicID
        /// </summary>
        virtual public System.String RegistrationInfoMedicID
        {
            get
            {
                return base.GetSystemString(PrmrjFollowUpMetadata.ColumnNames.RegistrationInfoMedicID);
            }

            set
            {
                base.SetSystemString(PrmrjFollowUpMetadata.ColumnNames.RegistrationInfoMedicID, value);
            }
        }
        /// <summary>
        /// Maps to PrmrjFollowUp.ImportantClinicalNotes
        /// </summary>
        virtual public System.String ImportantClinicalNotes
        {
            get
            {
                return base.GetSystemString(PrmrjFollowUpMetadata.ColumnNames.ImportantClinicalNotes);
            }

            set
            {
                base.SetSystemString(PrmrjFollowUpMetadata.ColumnNames.ImportantClinicalNotes, value);
            }
        }
        /// <summary>
        /// Maps to PrmrjFollowUp.Planning
        /// </summary>
        virtual public System.String Planning
        {
            get
            {
                return base.GetSystemString(PrmrjFollowUpMetadata.ColumnNames.Planning);
            }

            set
            {
                base.SetSystemString(PrmrjFollowUpMetadata.ColumnNames.Planning, value);
            }
        }
        /// <summary>
        /// Maps to PrmrjFollowUp.Remark
        /// </summary>
        virtual public System.String Remark
        {
            get
            {
                return base.GetSystemString(PrmrjFollowUpMetadata.ColumnNames.Remark);
            }

            set
            {
                base.SetSystemString(PrmrjFollowUpMetadata.ColumnNames.Remark, value);
            }
        }
        /// <summary>
        /// Maps to PrmrjFollowUp.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(PrmrjFollowUpMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(PrmrjFollowUpMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to PrmrjFollowUp.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(PrmrjFollowUpMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(PrmrjFollowUpMetadata.ColumnNames.LastUpdateDateTime, value);
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
            public esStrings(esPrmrjFollowUp entity)
            {
                this.entity = entity;
            }
            public System.String RegistrationInfoMedicID
            {
                get
                {
                    System.String data = entity.RegistrationInfoMedicID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RegistrationInfoMedicID = null;
                    else entity.RegistrationInfoMedicID = Convert.ToString(value);
                }
            }
            public System.String ImportantClinicalNotes
            {
                get
                {
                    System.String data = entity.ImportantClinicalNotes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ImportantClinicalNotes = null;
                    else entity.ImportantClinicalNotes = Convert.ToString(value);
                }
            }
            public System.String Planning
            {
                get
                {
                    System.String data = entity.Planning;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Planning = null;
                    else entity.Planning = Convert.ToString(value);
                }
            }
            public System.String Remark
            {
                get
                {
                    System.String data = entity.Remark;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Remark = null;
                    else entity.Remark = Convert.ToString(value);
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
            private esPrmrjFollowUp entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esPrmrjFollowUpQuery query)
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
                throw new Exception("esPrmrjFollowUp can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class PrmrjFollowUp : esPrmrjFollowUp
    {
    }

    [Serializable]
    abstract public class esPrmrjFollowUpQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return PrmrjFollowUpMetadata.Meta();
            }
        }

        public esQueryItem RegistrationInfoMedicID
        {
            get
            {
                return new esQueryItem(this, PrmrjFollowUpMetadata.ColumnNames.RegistrationInfoMedicID, esSystemType.String);
            }
        }

        public esQueryItem ImportantClinicalNotes
        {
            get
            {
                return new esQueryItem(this, PrmrjFollowUpMetadata.ColumnNames.ImportantClinicalNotes, esSystemType.String);
            }
        }

        public esQueryItem Planning
        {
            get
            {
                return new esQueryItem(this, PrmrjFollowUpMetadata.ColumnNames.Planning, esSystemType.String);
            }
        }

        public esQueryItem Remark
        {
            get
            {
                return new esQueryItem(this, PrmrjFollowUpMetadata.ColumnNames.Remark, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, PrmrjFollowUpMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, PrmrjFollowUpMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("PrmrjFollowUpCollection")]
    public partial class PrmrjFollowUpCollection : esPrmrjFollowUpCollection, IEnumerable<PrmrjFollowUp>
    {
        public PrmrjFollowUpCollection()
        {

        }

        public static implicit operator List<PrmrjFollowUp>(PrmrjFollowUpCollection coll)
        {
            List<PrmrjFollowUp> list = new List<PrmrjFollowUp>();

            foreach (PrmrjFollowUp emp in coll)
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
                return PrmrjFollowUpMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PrmrjFollowUpQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new PrmrjFollowUp(row);
        }

        override protected esEntity CreateEntity()
        {
            return new PrmrjFollowUp();
        }

        #endregion

        [BrowsableAttribute(false)]
        public PrmrjFollowUpQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PrmrjFollowUpQuery();
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
        public bool Load(PrmrjFollowUpQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public PrmrjFollowUp AddNew()
        {
            PrmrjFollowUp entity = base.AddNewEntity() as PrmrjFollowUp;

            return entity;
        }
        public PrmrjFollowUp FindByPrimaryKey(String registrationInfoMedicID)
        {
            return base.FindByPrimaryKey(registrationInfoMedicID) as PrmrjFollowUp;
        }

        #region IEnumerable< PrmrjFollowUp> Members

        IEnumerator<PrmrjFollowUp> IEnumerable<PrmrjFollowUp>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as PrmrjFollowUp;
            }
        }

        #endregion

        private PrmrjFollowUpQuery query;
    }


    /// <summary>
    /// Encapsulates the 'PrmrjFollowUp' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("PrmrjFollowUp ({RegistrationInfoMedicID})")]
    [Serializable]
    public partial class PrmrjFollowUp : esPrmrjFollowUp
    {
        public PrmrjFollowUp()
        {
        }

        public PrmrjFollowUp(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return PrmrjFollowUpMetadata.Meta();
            }
        }

        override protected esPrmrjFollowUpQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PrmrjFollowUpQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public PrmrjFollowUpQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PrmrjFollowUpQuery();
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
        public bool Load(PrmrjFollowUpQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private PrmrjFollowUpQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class PrmrjFollowUpQuery : esPrmrjFollowUpQuery
    {
        public PrmrjFollowUpQuery()
        {

        }

        public PrmrjFollowUpQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "PrmrjFollowUpQuery";
        }
    }

    [Serializable]
    public partial class PrmrjFollowUpMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected PrmrjFollowUpMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(PrmrjFollowUpMetadata.ColumnNames.RegistrationInfoMedicID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = PrmrjFollowUpMetadata.PropertyNames.RegistrationInfoMedicID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(PrmrjFollowUpMetadata.ColumnNames.ImportantClinicalNotes, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = PrmrjFollowUpMetadata.PropertyNames.ImportantClinicalNotes;
            c.CharacterMaxLength = 4000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PrmrjFollowUpMetadata.ColumnNames.Planning, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = PrmrjFollowUpMetadata.PropertyNames.Planning;
            c.CharacterMaxLength = 4000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PrmrjFollowUpMetadata.ColumnNames.Remark, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = PrmrjFollowUpMetadata.PropertyNames.Remark;
            c.CharacterMaxLength = 4000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PrmrjFollowUpMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = PrmrjFollowUpMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PrmrjFollowUpMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PrmrjFollowUpMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public PrmrjFollowUpMetadata Meta()
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
            public const string RegistrationInfoMedicID = "RegistrationInfoMedicID";
            public const string ImportantClinicalNotes = "ImportantClinicalNotes";
            public const string Planning = "Planning";
            public const string Remark = "Remark";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string RegistrationInfoMedicID = "RegistrationInfoMedicID";
            public const string ImportantClinicalNotes = "ImportantClinicalNotes";
            public const string Planning = "Planning";
            public const string Remark = "Remark";
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
            lock (typeof(PrmrjFollowUpMetadata))
            {
                if (PrmrjFollowUpMetadata.mapDelegates == null)
                {
                    PrmrjFollowUpMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (PrmrjFollowUpMetadata.meta == null)
                {
                    PrmrjFollowUpMetadata.meta = new PrmrjFollowUpMetadata();
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

                meta.AddTypeMap("RegistrationInfoMedicID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ImportantClinicalNotes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Planning", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Remark", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));


                meta.Source = "PrmrjFollowUp";
                meta.Destination = "PrmrjFollowUp";
                meta.spInsert = "proc_PrmrjFollowUpInsert";
                meta.spUpdate = "proc_PrmrjFollowUpUpdate";
                meta.spDelete = "proc_PrmrjFollowUpDelete";
                meta.spLoadAll = "proc_PrmrjFollowUpLoadAll";
                meta.spLoadByPrimaryKey = "proc_PrmrjFollowUpLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private PrmrjFollowUpMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
