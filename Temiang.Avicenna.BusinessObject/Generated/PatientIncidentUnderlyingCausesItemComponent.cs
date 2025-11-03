/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 8/8/2016 3:12:31 PM
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
    abstract public class esPatientIncidentUnderlyingCausesItemComponentCollection : esEntityCollectionWAuditLog
    {
        public esPatientIncidentUnderlyingCausesItemComponentCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "PatientIncidentUnderlyingCausesItemComponentCollection";
        }

        #region Query Logic
        protected void InitQuery(esPatientIncidentUnderlyingCausesItemComponentQuery query)
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
            this.InitQuery(query as esPatientIncidentUnderlyingCausesItemComponentQuery);
        }
        #endregion

        virtual public PatientIncidentUnderlyingCausesItemComponent DetachEntity(PatientIncidentUnderlyingCausesItemComponent entity)
        {
            return base.DetachEntity(entity) as PatientIncidentUnderlyingCausesItemComponent;
        }

        virtual public PatientIncidentUnderlyingCausesItemComponent AttachEntity(PatientIncidentUnderlyingCausesItemComponent entity)
        {
            return base.AttachEntity(entity) as PatientIncidentUnderlyingCausesItemComponent;
        }

        virtual public void Combine(PatientIncidentUnderlyingCausesItemComponentCollection collection)
        {
            base.Combine(collection);
        }

        new public PatientIncidentUnderlyingCausesItemComponent this[int index]
        {
            get
            {
                return base[index] as PatientIncidentUnderlyingCausesItemComponent;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(PatientIncidentUnderlyingCausesItemComponent);
        }
    }

    [Serializable]
    abstract public class esPatientIncidentUnderlyingCausesItemComponent : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esPatientIncidentUnderlyingCausesItemComponentQuery GetDynamicQuery()
        {
            return null;
        }

        public esPatientIncidentUnderlyingCausesItemComponent()
        {
        }

        public esPatientIncidentUnderlyingCausesItemComponent(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String patientIncidentNo, String serviceUnitID, String factorID, String factorItemID, String componentID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(patientIncidentNo, serviceUnitID, factorID, factorItemID, componentID);
            else
                return LoadByPrimaryKeyStoredProcedure(patientIncidentNo, serviceUnitID, factorID, factorItemID, componentID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String patientIncidentNo, String serviceUnitID, String factorID, String factorItemID, String componentID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(patientIncidentNo, serviceUnitID, factorID, factorItemID, componentID);
            else
                return LoadByPrimaryKeyStoredProcedure(patientIncidentNo, serviceUnitID, factorID, factorItemID, componentID);
        }

        private bool LoadByPrimaryKeyDynamic(String patientIncidentNo, String serviceUnitID, String factorID, String factorItemID, String componentID)
        {
            esPatientIncidentUnderlyingCausesItemComponentQuery query = this.GetDynamicQuery();
            query.Where(query.PatientIncidentNo == patientIncidentNo, query.ServiceUnitID == serviceUnitID, query.FactorID == factorID, query.FactorItemID == factorItemID, query.ComponentID == componentID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String patientIncidentNo, String serviceUnitID, String factorID, String factorItemID, String componentID)
        {
            esParameters parms = new esParameters();
            parms.Add("PatientIncidentNo", patientIncidentNo);
            parms.Add("ServiceUnitID", serviceUnitID);
            parms.Add("FactorID", factorID);
            parms.Add("FactorItemID", factorItemID);
            parms.Add("ComponentID", componentID);
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
                        case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
                        case "FactorID": this.str.FactorID = (string)value; break;
                        case "FactorItemID": this.str.FactorItemID = (string)value; break;
                        case "ComponentID": this.str.ComponentID = (string)value; break;
                        case "ComponentName": this.str.ComponentName = (string)value; break;
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
        /// Maps to PatientIncidentUnderlyingCausesItemComponent.PatientIncidentNo
        /// </summary>
        virtual public System.String PatientIncidentNo
        {
            get
            {
                return base.GetSystemString(PatientIncidentUnderlyingCausesItemComponentMetadata.ColumnNames.PatientIncidentNo);
            }

            set
            {
                base.SetSystemString(PatientIncidentUnderlyingCausesItemComponentMetadata.ColumnNames.PatientIncidentNo, value);
            }
        }
        /// <summary>
        /// Maps to PatientIncidentUnderlyingCausesItemComponent.ServiceUnitID
        /// </summary>
        virtual public System.String ServiceUnitID
        {
            get
            {
                return base.GetSystemString(PatientIncidentUnderlyingCausesItemComponentMetadata.ColumnNames.ServiceUnitID);
            }

            set
            {
                base.SetSystemString(PatientIncidentUnderlyingCausesItemComponentMetadata.ColumnNames.ServiceUnitID, value);
            }
        }
        /// <summary>
        /// Maps to PatientIncidentUnderlyingCausesItemComponent.FactorID
        /// </summary>
        virtual public System.String FactorID
        {
            get
            {
                return base.GetSystemString(PatientIncidentUnderlyingCausesItemComponentMetadata.ColumnNames.FactorID);
            }

            set
            {
                base.SetSystemString(PatientIncidentUnderlyingCausesItemComponentMetadata.ColumnNames.FactorID, value);
            }
        }
        /// <summary>
        /// Maps to PatientIncidentUnderlyingCausesItemComponent.FactorItemID
        /// </summary>
        virtual public System.String FactorItemID
        {
            get
            {
                return base.GetSystemString(PatientIncidentUnderlyingCausesItemComponentMetadata.ColumnNames.FactorItemID);
            }

            set
            {
                base.SetSystemString(PatientIncidentUnderlyingCausesItemComponentMetadata.ColumnNames.FactorItemID, value);
            }
        }
        /// <summary>
        /// Maps to PatientIncidentUnderlyingCausesItemComponent.ComponentID
        /// </summary>
        virtual public System.String ComponentID
        {
            get
            {
                return base.GetSystemString(PatientIncidentUnderlyingCausesItemComponentMetadata.ColumnNames.ComponentID);
            }

            set
            {
                base.SetSystemString(PatientIncidentUnderlyingCausesItemComponentMetadata.ColumnNames.ComponentID, value);
            }
        }
        /// <summary>
        /// Maps to PatientIncidentUnderlyingCausesItemComponent.ComponentName
        /// </summary>
        virtual public System.String ComponentName
        {
            get
            {
                return base.GetSystemString(PatientIncidentUnderlyingCausesItemComponentMetadata.ColumnNames.ComponentName);
            }

            set
            {
                base.SetSystemString(PatientIncidentUnderlyingCausesItemComponentMetadata.ColumnNames.ComponentName, value);
            }
        }
        /// <summary>
        /// Maps to PatientIncidentUnderlyingCausesItemComponent.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(PatientIncidentUnderlyingCausesItemComponentMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(PatientIncidentUnderlyingCausesItemComponentMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to PatientIncidentUnderlyingCausesItemComponent.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(PatientIncidentUnderlyingCausesItemComponentMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(PatientIncidentUnderlyingCausesItemComponentMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esPatientIncidentUnderlyingCausesItemComponent entity)
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
            public System.String ServiceUnitID
            {
                get
                {
                    System.String data = entity.ServiceUnitID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ServiceUnitID = null;
                    else entity.ServiceUnitID = Convert.ToString(value);
                }
            }
            public System.String FactorID
            {
                get
                {
                    System.String data = entity.FactorID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FactorID = null;
                    else entity.FactorID = Convert.ToString(value);
                }
            }
            public System.String FactorItemID
            {
                get
                {
                    System.String data = entity.FactorItemID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FactorItemID = null;
                    else entity.FactorItemID = Convert.ToString(value);
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
            public System.String ComponentName
            {
                get
                {
                    System.String data = entity.ComponentName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ComponentName = null;
                    else entity.ComponentName = Convert.ToString(value);
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
            private esPatientIncidentUnderlyingCausesItemComponent entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esPatientIncidentUnderlyingCausesItemComponentQuery query)
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
                throw new Exception("esPatientIncidentUnderlyingCausesItemComponent can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class PatientIncidentUnderlyingCausesItemComponent : esPatientIncidentUnderlyingCausesItemComponent
    {
    }

    [Serializable]
    abstract public class esPatientIncidentUnderlyingCausesItemComponentQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return PatientIncidentUnderlyingCausesItemComponentMetadata.Meta();
            }
        }

        public esQueryItem PatientIncidentNo
        {
            get
            {
                return new esQueryItem(this, PatientIncidentUnderlyingCausesItemComponentMetadata.ColumnNames.PatientIncidentNo, esSystemType.String);
            }
        }

        public esQueryItem ServiceUnitID
        {
            get
            {
                return new esQueryItem(this, PatientIncidentUnderlyingCausesItemComponentMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
            }
        }

        public esQueryItem FactorID
        {
            get
            {
                return new esQueryItem(this, PatientIncidentUnderlyingCausesItemComponentMetadata.ColumnNames.FactorID, esSystemType.String);
            }
        }

        public esQueryItem FactorItemID
        {
            get
            {
                return new esQueryItem(this, PatientIncidentUnderlyingCausesItemComponentMetadata.ColumnNames.FactorItemID, esSystemType.String);
            }
        }

        public esQueryItem ComponentID
        {
            get
            {
                return new esQueryItem(this, PatientIncidentUnderlyingCausesItemComponentMetadata.ColumnNames.ComponentID, esSystemType.String);
            }
        }

        public esQueryItem ComponentName
        {
            get
            {
                return new esQueryItem(this, PatientIncidentUnderlyingCausesItemComponentMetadata.ColumnNames.ComponentName, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, PatientIncidentUnderlyingCausesItemComponentMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, PatientIncidentUnderlyingCausesItemComponentMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("PatientIncidentUnderlyingCausesItemComponentCollection")]
    public partial class PatientIncidentUnderlyingCausesItemComponentCollection : esPatientIncidentUnderlyingCausesItemComponentCollection, IEnumerable<PatientIncidentUnderlyingCausesItemComponent>
    {
        public PatientIncidentUnderlyingCausesItemComponentCollection()
        {

        }

        public static implicit operator List<PatientIncidentUnderlyingCausesItemComponent>(PatientIncidentUnderlyingCausesItemComponentCollection coll)
        {
            List<PatientIncidentUnderlyingCausesItemComponent> list = new List<PatientIncidentUnderlyingCausesItemComponent>();

            foreach (PatientIncidentUnderlyingCausesItemComponent emp in coll)
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
                return PatientIncidentUnderlyingCausesItemComponentMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PatientIncidentUnderlyingCausesItemComponentQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new PatientIncidentUnderlyingCausesItemComponent(row);
        }

        override protected esEntity CreateEntity()
        {
            return new PatientIncidentUnderlyingCausesItemComponent();
        }

        #endregion

        [BrowsableAttribute(false)]
        public PatientIncidentUnderlyingCausesItemComponentQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PatientIncidentUnderlyingCausesItemComponentQuery();
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
        public bool Load(PatientIncidentUnderlyingCausesItemComponentQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public PatientIncidentUnderlyingCausesItemComponent AddNew()
        {
            PatientIncidentUnderlyingCausesItemComponent entity = base.AddNewEntity() as PatientIncidentUnderlyingCausesItemComponent;

            return entity;
        }
        public PatientIncidentUnderlyingCausesItemComponent FindByPrimaryKey(String patientIncidentNo, String serviceUnitID, String factorID, String factorItemID, String componentID)
        {
            return base.FindByPrimaryKey(patientIncidentNo, serviceUnitID, factorID, factorItemID, componentID) as PatientIncidentUnderlyingCausesItemComponent;
        }

        #region IEnumerable< PatientIncidentUnderlyingCausesItemComponent> Members

        IEnumerator<PatientIncidentUnderlyingCausesItemComponent> IEnumerable<PatientIncidentUnderlyingCausesItemComponent>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as PatientIncidentUnderlyingCausesItemComponent;
            }
        }

        #endregion

        private PatientIncidentUnderlyingCausesItemComponentQuery query;
    }


    /// <summary>
    /// Encapsulates the 'PatientIncidentUnderlyingCausesItemComponent' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("PatientIncidentUnderlyingCausesItemComponent ({PatientIncidentNo, ServiceUnitID, FactorID, FactorItemID, ComponentID})")]
    [Serializable]
    public partial class PatientIncidentUnderlyingCausesItemComponent : esPatientIncidentUnderlyingCausesItemComponent
    {
        public PatientIncidentUnderlyingCausesItemComponent()
        {
        }

        public PatientIncidentUnderlyingCausesItemComponent(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return PatientIncidentUnderlyingCausesItemComponentMetadata.Meta();
            }
        }

        override protected esPatientIncidentUnderlyingCausesItemComponentQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PatientIncidentUnderlyingCausesItemComponentQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public PatientIncidentUnderlyingCausesItemComponentQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PatientIncidentUnderlyingCausesItemComponentQuery();
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
        public bool Load(PatientIncidentUnderlyingCausesItemComponentQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private PatientIncidentUnderlyingCausesItemComponentQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class PatientIncidentUnderlyingCausesItemComponentQuery : esPatientIncidentUnderlyingCausesItemComponentQuery
    {
        public PatientIncidentUnderlyingCausesItemComponentQuery()
        {

        }

        public PatientIncidentUnderlyingCausesItemComponentQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "PatientIncidentUnderlyingCausesItemComponentQuery";
        }
    }

    [Serializable]
    public partial class PatientIncidentUnderlyingCausesItemComponentMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected PatientIncidentUnderlyingCausesItemComponentMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(PatientIncidentUnderlyingCausesItemComponentMetadata.ColumnNames.PatientIncidentNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientIncidentUnderlyingCausesItemComponentMetadata.PropertyNames.PatientIncidentNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(PatientIncidentUnderlyingCausesItemComponentMetadata.ColumnNames.ServiceUnitID, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientIncidentUnderlyingCausesItemComponentMetadata.PropertyNames.ServiceUnitID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(PatientIncidentUnderlyingCausesItemComponentMetadata.ColumnNames.FactorID, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientIncidentUnderlyingCausesItemComponentMetadata.PropertyNames.FactorID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(PatientIncidentUnderlyingCausesItemComponentMetadata.ColumnNames.FactorItemID, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientIncidentUnderlyingCausesItemComponentMetadata.PropertyNames.FactorItemID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(PatientIncidentUnderlyingCausesItemComponentMetadata.ColumnNames.ComponentID, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientIncidentUnderlyingCausesItemComponentMetadata.PropertyNames.ComponentID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(PatientIncidentUnderlyingCausesItemComponentMetadata.ColumnNames.ComponentName, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientIncidentUnderlyingCausesItemComponentMetadata.PropertyNames.ComponentName;
            c.CharacterMaxLength = 250;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientIncidentUnderlyingCausesItemComponentMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PatientIncidentUnderlyingCausesItemComponentMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientIncidentUnderlyingCausesItemComponentMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientIncidentUnderlyingCausesItemComponentMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public PatientIncidentUnderlyingCausesItemComponentMetadata Meta()
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
            public const string ServiceUnitID = "ServiceUnitID";
            public const string FactorID = "FactorID";
            public const string FactorItemID = "FactorItemID";
            public const string ComponentID = "ComponentID";
            public const string ComponentName = "ComponentName";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string PatientIncidentNo = "PatientIncidentNo";
            public const string ServiceUnitID = "ServiceUnitID";
            public const string FactorID = "FactorID";
            public const string FactorItemID = "FactorItemID";
            public const string ComponentID = "ComponentID";
            public const string ComponentName = "ComponentName";
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
            lock (typeof(PatientIncidentUnderlyingCausesItemComponentMetadata))
            {
                if (PatientIncidentUnderlyingCausesItemComponentMetadata.mapDelegates == null)
                {
                    PatientIncidentUnderlyingCausesItemComponentMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (PatientIncidentUnderlyingCausesItemComponentMetadata.meta == null)
                {
                    PatientIncidentUnderlyingCausesItemComponentMetadata.meta = new PatientIncidentUnderlyingCausesItemComponentMetadata();
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
                meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("FactorID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("FactorItemID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ComponentID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ComponentName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "PatientIncidentUnderlyingCausesItemComponent";
                meta.Destination = "PatientIncidentUnderlyingCausesItemComponent";
                meta.spInsert = "proc_PatientIncidentUnderlyingCausesItemComponentInsert";
                meta.spUpdate = "proc_PatientIncidentUnderlyingCausesItemComponentUpdate";
                meta.spDelete = "proc_PatientIncidentUnderlyingCausesItemComponentDelete";
                meta.spLoadAll = "proc_PatientIncidentUnderlyingCausesItemComponentLoadAll";
                meta.spLoadByPrimaryKey = "proc_PatientIncidentUnderlyingCausesItemComponentLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private PatientIncidentUnderlyingCausesItemComponentMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
