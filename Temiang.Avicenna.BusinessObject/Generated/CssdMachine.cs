/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 10/24/2016 9:52:08 AM
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
    abstract public class esCssdMachineCollection : esEntityCollectionWAuditLog
    {
        public esCssdMachineCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "CssdMachineCollection";
        }

        #region Query Logic
        protected void InitQuery(esCssdMachineQuery query)
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
            this.InitQuery(query as esCssdMachineQuery);
        }
        #endregion

        virtual public CssdMachine DetachEntity(CssdMachine entity)
        {
            return base.DetachEntity(entity) as CssdMachine;
        }

        virtual public CssdMachine AttachEntity(CssdMachine entity)
        {
            return base.AttachEntity(entity) as CssdMachine;
        }

        virtual public void Combine(CssdMachineCollection collection)
        {
            base.Combine(collection);
        }

        new public CssdMachine this[int index]
        {
            get
            {
                return base[index] as CssdMachine;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(CssdMachine);
        }
    }

    [Serializable]
    abstract public class esCssdMachine : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esCssdMachineQuery GetDynamicQuery()
        {
            return null;
        }

        public esCssdMachine()
        {
        }

        public esCssdMachine(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String machineID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(machineID);
            else
                return LoadByPrimaryKeyStoredProcedure(machineID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String machineID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(machineID);
            else
                return LoadByPrimaryKeyStoredProcedure(machineID);
        }

        private bool LoadByPrimaryKeyDynamic(String machineID)
        {
            esCssdMachineQuery query = this.GetDynamicQuery();
            query.Where(query.MachineID == machineID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String machineID)
        {
            esParameters parms = new esParameters();
            parms.Add("MachineID", machineID);
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
                        case "MachineID": this.str.MachineID = (string)value; break;
                        case "MachineName": this.str.MachineName = (string)value; break;
                        case "StartUsingDate": this.str.StartUsingDate = (string)value; break;
                        case "Volume": this.str.Volume = (string)value; break;
                        case "Notes": this.str.Notes = (string)value; break;
                        case "IsActive": this.str.IsActive = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "StartUsingDate":

                            if (value == null || value is System.DateTime)
                                this.StartUsingDate = (System.DateTime?)value;
                            break;
                        case "Volume":

                            if (value == null || value is System.Decimal)
                                this.Volume = (System.Decimal?)value;
                            break;
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
        /// Maps to CssdMachine.MachineID
        /// </summary>
        virtual public System.String MachineID
        {
            get
            {
                return base.GetSystemString(CssdMachineMetadata.ColumnNames.MachineID);
            }

            set
            {
                base.SetSystemString(CssdMachineMetadata.ColumnNames.MachineID, value);
            }
        }
        /// <summary>
        /// Maps to CssdMachine.MachineName
        /// </summary>
        virtual public System.String MachineName
        {
            get
            {
                return base.GetSystemString(CssdMachineMetadata.ColumnNames.MachineName);
            }

            set
            {
                base.SetSystemString(CssdMachineMetadata.ColumnNames.MachineName, value);
            }
        }
        /// <summary>
        /// Maps to CssdMachine.StartUsingDate
        /// </summary>
        virtual public System.DateTime? StartUsingDate
        {
            get
            {
                return base.GetSystemDateTime(CssdMachineMetadata.ColumnNames.StartUsingDate);
            }

            set
            {
                base.SetSystemDateTime(CssdMachineMetadata.ColumnNames.StartUsingDate, value);
            }
        }
        /// <summary>
        /// Maps to CssdMachine.Volume
        /// </summary>
        virtual public System.Decimal? Volume
        {
            get
            {
                return base.GetSystemDecimal(CssdMachineMetadata.ColumnNames.Volume);
            }

            set
            {
                base.SetSystemDecimal(CssdMachineMetadata.ColumnNames.Volume, value);
            }
        }
        /// <summary>
        /// Maps to CssdMachine.Notes
        /// </summary>
        virtual public System.String Notes
        {
            get
            {
                return base.GetSystemString(CssdMachineMetadata.ColumnNames.Notes);
            }

            set
            {
                base.SetSystemString(CssdMachineMetadata.ColumnNames.Notes, value);
            }
        }
        /// <summary>
        /// Maps to CssdMachine.IsActive
        /// </summary>
        virtual public System.Boolean? IsActive
        {
            get
            {
                return base.GetSystemBoolean(CssdMachineMetadata.ColumnNames.IsActive);
            }

            set
            {
                base.SetSystemBoolean(CssdMachineMetadata.ColumnNames.IsActive, value);
            }
        }
        /// <summary>
        /// Maps to CssdMachine.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(CssdMachineMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(CssdMachineMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to CssdMachine.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(CssdMachineMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(CssdMachineMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esCssdMachine entity)
            {
                this.entity = entity;
            }
            public System.String MachineID
            {
                get
                {
                    System.String data = entity.MachineID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.MachineID = null;
                    else entity.MachineID = Convert.ToString(value);
                }
            }
            public System.String MachineName
            {
                get
                {
                    System.String data = entity.MachineName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.MachineName = null;
                    else entity.MachineName = Convert.ToString(value);
                }
            }
            public System.String StartUsingDate
            {
                get
                {
                    System.DateTime? data = entity.StartUsingDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.StartUsingDate = null;
                    else entity.StartUsingDate = Convert.ToDateTime(value);
                }
            }
            public System.String Volume
            {
                get
                {
                    System.Decimal? data = entity.Volume;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Volume = null;
                    else entity.Volume = Convert.ToDecimal(value);
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
            private esCssdMachine entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esCssdMachineQuery query)
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
                throw new Exception("esCssdMachine can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class CssdMachine : esCssdMachine
    {
    }

    [Serializable]
    abstract public class esCssdMachineQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return CssdMachineMetadata.Meta();
            }
        }

        public esQueryItem MachineID
        {
            get
            {
                return new esQueryItem(this, CssdMachineMetadata.ColumnNames.MachineID, esSystemType.String);
            }
        }

        public esQueryItem MachineName
        {
            get
            {
                return new esQueryItem(this, CssdMachineMetadata.ColumnNames.MachineName, esSystemType.String);
            }
        }

        public esQueryItem StartUsingDate
        {
            get
            {
                return new esQueryItem(this, CssdMachineMetadata.ColumnNames.StartUsingDate, esSystemType.DateTime);
            }
        }

        public esQueryItem Volume
        {
            get
            {
                return new esQueryItem(this, CssdMachineMetadata.ColumnNames.Volume, esSystemType.Decimal);
            }
        }

        public esQueryItem Notes
        {
            get
            {
                return new esQueryItem(this, CssdMachineMetadata.ColumnNames.Notes, esSystemType.String);
            }
        }

        public esQueryItem IsActive
        {
            get
            {
                return new esQueryItem(this, CssdMachineMetadata.ColumnNames.IsActive, esSystemType.Boolean);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, CssdMachineMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, CssdMachineMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("CssdMachineCollection")]
    public partial class CssdMachineCollection : esCssdMachineCollection, IEnumerable<CssdMachine>
    {
        public CssdMachineCollection()
        {

        }

        public static implicit operator List<CssdMachine>(CssdMachineCollection coll)
        {
            List<CssdMachine> list = new List<CssdMachine>();

            foreach (CssdMachine emp in coll)
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
                return CssdMachineMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new CssdMachineQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new CssdMachine(row);
        }

        override protected esEntity CreateEntity()
        {
            return new CssdMachine();
        }

        #endregion

        [BrowsableAttribute(false)]
        public CssdMachineQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new CssdMachineQuery();
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
        public bool Load(CssdMachineQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public CssdMachine AddNew()
        {
            CssdMachine entity = base.AddNewEntity() as CssdMachine;

            return entity;
        }
        public CssdMachine FindByPrimaryKey(String machineID)
        {
            return base.FindByPrimaryKey(machineID) as CssdMachine;
        }

        #region IEnumerable< CssdMachine> Members

        IEnumerator<CssdMachine> IEnumerable<CssdMachine>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as CssdMachine;
            }
        }

        #endregion

        private CssdMachineQuery query;
    }


    /// <summary>
    /// Encapsulates the 'CssdMachine' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("CssdMachine ({MachineID})")]
    [Serializable]
    public partial class CssdMachine : esCssdMachine
    {
        public CssdMachine()
        {
        }

        public CssdMachine(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return CssdMachineMetadata.Meta();
            }
        }

        override protected esCssdMachineQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new CssdMachineQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public CssdMachineQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new CssdMachineQuery();
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
        public bool Load(CssdMachineQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private CssdMachineQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class CssdMachineQuery : esCssdMachineQuery
    {
        public CssdMachineQuery()
        {

        }

        public CssdMachineQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "CssdMachineQuery";
        }
    }

    [Serializable]
    public partial class CssdMachineMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected CssdMachineMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(CssdMachineMetadata.ColumnNames.MachineID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = CssdMachineMetadata.PropertyNames.MachineID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(CssdMachineMetadata.ColumnNames.MachineName, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = CssdMachineMetadata.PropertyNames.MachineName;
            c.CharacterMaxLength = 200;
            _columns.Add(c);

            c = new esColumnMetadata(CssdMachineMetadata.ColumnNames.StartUsingDate, 2, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = CssdMachineMetadata.PropertyNames.StartUsingDate;
            _columns.Add(c);

            c = new esColumnMetadata(CssdMachineMetadata.ColumnNames.Volume, 3, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = CssdMachineMetadata.PropertyNames.Volume;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(CssdMachineMetadata.ColumnNames.Notes, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = CssdMachineMetadata.PropertyNames.Notes;
            c.CharacterMaxLength = 500;
            _columns.Add(c);

            c = new esColumnMetadata(CssdMachineMetadata.ColumnNames.IsActive, 5, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = CssdMachineMetadata.PropertyNames.IsActive;
            _columns.Add(c);

            c = new esColumnMetadata(CssdMachineMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = CssdMachineMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(CssdMachineMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = CssdMachineMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public CssdMachineMetadata Meta()
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
            public const string MachineID = "MachineID";
            public const string MachineName = "MachineName";
            public const string StartUsingDate = "StartUsingDate";
            public const string Volume = "Volume";
            public const string Notes = "Notes";
            public const string IsActive = "IsActive";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string MachineID = "MachineID";
            public const string MachineName = "MachineName";
            public const string StartUsingDate = "StartUsingDate";
            public const string Volume = "Volume";
            public const string Notes = "Notes";
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
            lock (typeof(CssdMachineMetadata))
            {
                if (CssdMachineMetadata.mapDelegates == null)
                {
                    CssdMachineMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (CssdMachineMetadata.meta == null)
                {
                    CssdMachineMetadata.meta = new CssdMachineMetadata();
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

                meta.AddTypeMap("MachineID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("MachineName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("StartUsingDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("Volume", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "CssdMachine";
                meta.Destination = "CssdMachine";
                meta.spInsert = "proc_CssdMachineInsert";
                meta.spUpdate = "proc_CssdMachineUpdate";
                meta.spDelete = "proc_CssdMachineDelete";
                meta.spLoadAll = "proc_CssdMachineLoadAll";
                meta.spLoadByPrimaryKey = "proc_CssdMachineLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private CssdMachineMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
