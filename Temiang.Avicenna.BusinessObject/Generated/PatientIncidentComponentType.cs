/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 10/19/2016 9:20:01 AM
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
    abstract public class esPatientIncidentComponentTypeCollection : esEntityCollectionWAuditLog
    {
        public esPatientIncidentComponentTypeCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "PatientIncidentComponentTypeCollection";
        }

        #region Query Logic
        protected void InitQuery(esPatientIncidentComponentTypeQuery query)
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
            this.InitQuery(query as esPatientIncidentComponentTypeQuery);
        }
        #endregion

        virtual public PatientIncidentComponentType DetachEntity(PatientIncidentComponentType entity)
        {
            return base.DetachEntity(entity) as PatientIncidentComponentType;
        }

        virtual public PatientIncidentComponentType AttachEntity(PatientIncidentComponentType entity)
        {
            return base.AttachEntity(entity) as PatientIncidentComponentType;
        }

        virtual public void Combine(PatientIncidentComponentTypeCollection collection)
        {
            base.Combine(collection);
        }

        new public PatientIncidentComponentType this[int index]
        {
            get
            {
                return base[index] as PatientIncidentComponentType;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(PatientIncidentComponentType);
        }
    }

    [Serializable]
    abstract public class esPatientIncidentComponentType : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esPatientIncidentComponentTypeQuery GetDynamicQuery()
        {
            return null;
        }

        public esPatientIncidentComponentType()
        {
        }

        public esPatientIncidentComponentType(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String patientIncidentNo, String sRIncidentType, String componentID, String subComponentID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(patientIncidentNo, sRIncidentType, componentID, subComponentID);
            else
                return LoadByPrimaryKeyStoredProcedure(patientIncidentNo, sRIncidentType, componentID, subComponentID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String patientIncidentNo, String sRIncidentType, String componentID, String subComponentID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(patientIncidentNo, sRIncidentType, componentID, subComponentID);
            else
                return LoadByPrimaryKeyStoredProcedure(patientIncidentNo, sRIncidentType, componentID, subComponentID);
        }

        private bool LoadByPrimaryKeyDynamic(String patientIncidentNo, String sRIncidentType, String componentID, String subComponentID)
        {
            esPatientIncidentComponentTypeQuery query = this.GetDynamicQuery();
            query.Where(query.PatientIncidentNo == patientIncidentNo, query.SRIncidentType == sRIncidentType, query.ComponentID == componentID, query.SubComponentID == subComponentID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String patientIncidentNo, String sRIncidentType, String componentID, String subComponentID)
        {
            esParameters parms = new esParameters();
            parms.Add("PatientIncidentNo", patientIncidentNo);
            parms.Add("SRIncidentType", sRIncidentType);
            parms.Add("ComponentID", componentID);
            parms.Add("SubComponentID", subComponentID);
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
                        case "PatientIncidentNo": this.str.PatientIncidentNo = (string)value; break;
                        case "SRIncidentType": this.str.SRIncidentType = (string)value; break;
                        case "ComponentID": this.str.ComponentID = (string)value; break;
                        case "SubComponentID": this.str.SubComponentID = (string)value; break;
                        case "SubComponentName": this.str.SubComponentName = (string)value; break;
                        case "Modus": this.str.Modus = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
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
        /// Maps to PatientIncidentComponentType.PatientIncidentNo
        /// </summary>
        virtual public System.String PatientIncidentNo
        {
            get
            {
                return base.GetSystemString(PatientIncidentComponentTypeMetadata.ColumnNames.PatientIncidentNo);
            }

            set
            {
                base.SetSystemString(PatientIncidentComponentTypeMetadata.ColumnNames.PatientIncidentNo, value);
            }
        }
        /// <summary>
        /// Maps to PatientIncidentComponentType.SRIncidentType
        /// </summary>
        virtual public System.String SRIncidentType
        {
            get
            {
                return base.GetSystemString(PatientIncidentComponentTypeMetadata.ColumnNames.SRIncidentType);
            }

            set
            {
                base.SetSystemString(PatientIncidentComponentTypeMetadata.ColumnNames.SRIncidentType, value);
            }
        }
        /// <summary>
        /// Maps to PatientIncidentComponentType.ComponentID
        /// </summary>
        virtual public System.String ComponentID
        {
            get
            {
                return base.GetSystemString(PatientIncidentComponentTypeMetadata.ColumnNames.ComponentID);
            }

            set
            {
                base.SetSystemString(PatientIncidentComponentTypeMetadata.ColumnNames.ComponentID, value);
            }
        }
        /// <summary>
        /// Maps to PatientIncidentComponentType.SubComponentID
        /// </summary>
        virtual public System.String SubComponentID
        {
            get
            {
                return base.GetSystemString(PatientIncidentComponentTypeMetadata.ColumnNames.SubComponentID);
            }

            set
            {
                base.SetSystemString(PatientIncidentComponentTypeMetadata.ColumnNames.SubComponentID, value);
            }
        }
        /// <summary>
        /// Maps to PatientIncidentComponentType.SubComponentName
        /// </summary>
        virtual public System.String SubComponentName
        {
            get
            {
                return base.GetSystemString(PatientIncidentComponentTypeMetadata.ColumnNames.SubComponentName);
            }

            set
            {
                base.SetSystemString(PatientIncidentComponentTypeMetadata.ColumnNames.SubComponentName, value);
            }
        }
        /// <summary>
        /// Maps to PatientIncidentComponentType.Modus
        /// </summary>
        virtual public System.String Modus
        {
            get
            {
                return base.GetSystemString(PatientIncidentComponentTypeMetadata.ColumnNames.Modus);
            }

            set
            {
                base.SetSystemString(PatientIncidentComponentTypeMetadata.ColumnNames.Modus, value);
            }
        }
        /// <summary>
        /// Maps to PatientIncidentComponentType.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(PatientIncidentComponentTypeMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(PatientIncidentComponentTypeMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to PatientIncidentComponentType.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(PatientIncidentComponentTypeMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(PatientIncidentComponentTypeMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esPatientIncidentComponentType entity)
            {
                this.entity = entity;
            }
            public System.String PatientIncidentNo
            {
                get
                {
                    System.String data = entity.PatientIncidentNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PatientIncidentNo = null;
                    else entity.PatientIncidentNo = Convert.ToString(value);
                }
            }
            public System.String SRIncidentType
            {
                get
                {
                    System.String data = entity.SRIncidentType;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRIncidentType = null;
                    else entity.SRIncidentType = Convert.ToString(value);
                }
            }
            public System.String ComponentID
            {
                get
                {
                    System.String data = entity.ComponentID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ComponentID = null;
                    else entity.ComponentID = Convert.ToString(value);
                }
            }
            public System.String SubComponentID
            {
                get
                {
                    System.String data = entity.SubComponentID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SubComponentID = null;
                    else entity.SubComponentID = Convert.ToString(value);
                }
            }
            public System.String SubComponentName
            {
                get
                {
                    System.String data = entity.SubComponentName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SubComponentName = null;
                    else entity.SubComponentName = Convert.ToString(value);
                }
            }
            public System.String Modus
            {
                get
                {
                    System.String data = entity.Modus;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Modus = null;
                    else entity.Modus = Convert.ToString(value);
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
            private esPatientIncidentComponentType entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esPatientIncidentComponentTypeQuery query)
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
                throw new Exception("esPatientIncidentComponentType can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class PatientIncidentComponentType : esPatientIncidentComponentType
    {
    }

    [Serializable]
    abstract public class esPatientIncidentComponentTypeQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return PatientIncidentComponentTypeMetadata.Meta();
            }
        }

        public esQueryItem PatientIncidentNo
        {
            get
            {
                return new esQueryItem(this, PatientIncidentComponentTypeMetadata.ColumnNames.PatientIncidentNo, esSystemType.String);
            }
        }

        public esQueryItem SRIncidentType
        {
            get
            {
                return new esQueryItem(this, PatientIncidentComponentTypeMetadata.ColumnNames.SRIncidentType, esSystemType.String);
            }
        }

        public esQueryItem ComponentID
        {
            get
            {
                return new esQueryItem(this, PatientIncidentComponentTypeMetadata.ColumnNames.ComponentID, esSystemType.String);
            }
        }

        public esQueryItem SubComponentID
        {
            get
            {
                return new esQueryItem(this, PatientIncidentComponentTypeMetadata.ColumnNames.SubComponentID, esSystemType.String);
            }
        }

        public esQueryItem SubComponentName
        {
            get
            {
                return new esQueryItem(this, PatientIncidentComponentTypeMetadata.ColumnNames.SubComponentName, esSystemType.String);
            }
        }

        public esQueryItem Modus
        {
            get
            {
                return new esQueryItem(this, PatientIncidentComponentTypeMetadata.ColumnNames.Modus, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, PatientIncidentComponentTypeMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, PatientIncidentComponentTypeMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("PatientIncidentComponentTypeCollection")]
    public partial class PatientIncidentComponentTypeCollection : esPatientIncidentComponentTypeCollection, IEnumerable<PatientIncidentComponentType>
    {
        public PatientIncidentComponentTypeCollection()
        {

        }

        public static implicit operator List<PatientIncidentComponentType>(PatientIncidentComponentTypeCollection coll)
        {
            List<PatientIncidentComponentType> list = new List<PatientIncidentComponentType>();

            foreach (PatientIncidentComponentType emp in coll)
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
                return PatientIncidentComponentTypeMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PatientIncidentComponentTypeQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new PatientIncidentComponentType(row);
        }

        override protected esEntity CreateEntity()
        {
            return new PatientIncidentComponentType();
        }

        #endregion

        [BrowsableAttribute(false)]
        public PatientIncidentComponentTypeQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PatientIncidentComponentTypeQuery();
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
        public bool Load(PatientIncidentComponentTypeQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public PatientIncidentComponentType AddNew()
        {
            PatientIncidentComponentType entity = base.AddNewEntity() as PatientIncidentComponentType;

            return entity;
        }
        public PatientIncidentComponentType FindByPrimaryKey(String patientIncidentNo, String sRIncidentType, String componentID, String subComponentID)
        {
            return base.FindByPrimaryKey(patientIncidentNo, sRIncidentType, componentID, subComponentID) as PatientIncidentComponentType;
        }

        #region IEnumerable< PatientIncidentComponentType> Members

        IEnumerator<PatientIncidentComponentType> IEnumerable<PatientIncidentComponentType>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as PatientIncidentComponentType;
            }
        }

        #endregion

        private PatientIncidentComponentTypeQuery query;
    }


    /// <summary>
    /// Encapsulates the 'PatientIncidentComponentType' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("PatientIncidentComponentType ({PatientIncidentNo, SRIncidentType, ComponentID, SubComponentID})")]
    [Serializable]
    public partial class PatientIncidentComponentType : esPatientIncidentComponentType
    {
        public PatientIncidentComponentType()
        {
        }

        public PatientIncidentComponentType(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return PatientIncidentComponentTypeMetadata.Meta();
            }
        }

        override protected esPatientIncidentComponentTypeQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PatientIncidentComponentTypeQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public PatientIncidentComponentTypeQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PatientIncidentComponentTypeQuery();
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
        public bool Load(PatientIncidentComponentTypeQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private PatientIncidentComponentTypeQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class PatientIncidentComponentTypeQuery : esPatientIncidentComponentTypeQuery
    {
        public PatientIncidentComponentTypeQuery()
        {

        }

        public PatientIncidentComponentTypeQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "PatientIncidentComponentTypeQuery";
        }
    }

    [Serializable]
    public partial class PatientIncidentComponentTypeMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected PatientIncidentComponentTypeMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(PatientIncidentComponentTypeMetadata.ColumnNames.PatientIncidentNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientIncidentComponentTypeMetadata.PropertyNames.PatientIncidentNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(PatientIncidentComponentTypeMetadata.ColumnNames.SRIncidentType, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientIncidentComponentTypeMetadata.PropertyNames.SRIncidentType;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(PatientIncidentComponentTypeMetadata.ColumnNames.ComponentID, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientIncidentComponentTypeMetadata.PropertyNames.ComponentID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(PatientIncidentComponentTypeMetadata.ColumnNames.SubComponentID, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientIncidentComponentTypeMetadata.PropertyNames.SubComponentID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(PatientIncidentComponentTypeMetadata.ColumnNames.SubComponentName, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientIncidentComponentTypeMetadata.PropertyNames.SubComponentName;
            c.CharacterMaxLength = 250;
            _columns.Add(c);

            c = new esColumnMetadata(PatientIncidentComponentTypeMetadata.ColumnNames.Modus, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientIncidentComponentTypeMetadata.PropertyNames.Modus;
            c.CharacterMaxLength = 500;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientIncidentComponentTypeMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PatientIncidentComponentTypeMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientIncidentComponentTypeMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientIncidentComponentTypeMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public PatientIncidentComponentTypeMetadata Meta()
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
            public const string PatientIncidentNo = "PatientIncidentNo";
            public const string SRIncidentType = "SRIncidentType";
            public const string ComponentID = "ComponentID";
            public const string SubComponentID = "SubComponentID";
            public const string SubComponentName = "SubComponentName";
            public const string Modus = "Modus";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string PatientIncidentNo = "PatientIncidentNo";
            public const string SRIncidentType = "SRIncidentType";
            public const string ComponentID = "ComponentID";
            public const string SubComponentID = "SubComponentID";
            public const string SubComponentName = "SubComponentName";
            public const string Modus = "Modus";
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
            lock (typeof(PatientIncidentComponentTypeMetadata))
            {
                if (PatientIncidentComponentTypeMetadata.mapDelegates == null)
                {
                    PatientIncidentComponentTypeMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (PatientIncidentComponentTypeMetadata.meta == null)
                {
                    PatientIncidentComponentTypeMetadata.meta = new PatientIncidentComponentTypeMetadata();
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

                meta.AddTypeMap("PatientIncidentNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRIncidentType", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ComponentID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SubComponentID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SubComponentName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Modus", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "PatientIncidentComponentType";
                meta.Destination = "PatientIncidentComponentType";
                meta.spInsert = "proc_PatientIncidentComponentTypeInsert";
                meta.spUpdate = "proc_PatientIncidentComponentTypeUpdate";
                meta.spDelete = "proc_PatientIncidentComponentTypeDelete";
                meta.spLoadAll = "proc_PatientIncidentComponentTypeLoadAll";
                meta.spLoadByPrimaryKey = "proc_PatientIncidentComponentTypeLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private PatientIncidentComponentTypeMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
