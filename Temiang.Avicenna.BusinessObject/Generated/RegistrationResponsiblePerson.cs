/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 3/29/2017 2:08:29 PM
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
    abstract public class esRegistrationResponsiblePersonCollection : esEntityCollectionWAuditLog
    {
        public esRegistrationResponsiblePersonCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "RegistrationResponsiblePersonCollection";
        }

        #region Query Logic
        protected void InitQuery(esRegistrationResponsiblePersonQuery query)
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
            this.InitQuery(query as esRegistrationResponsiblePersonQuery);
        }
        #endregion

        virtual public RegistrationResponsiblePerson DetachEntity(RegistrationResponsiblePerson entity)
        {
            return base.DetachEntity(entity) as RegistrationResponsiblePerson;
        }

        virtual public RegistrationResponsiblePerson AttachEntity(RegistrationResponsiblePerson entity)
        {
            return base.AttachEntity(entity) as RegistrationResponsiblePerson;
        }

        virtual public void Combine(RegistrationResponsiblePersonCollection collection)
        {
            base.Combine(collection);
        }

        new public RegistrationResponsiblePerson this[int index]
        {
            get
            {
                return base[index] as RegistrationResponsiblePerson;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(RegistrationResponsiblePerson);
        }
    }

    [Serializable]
    abstract public class esRegistrationResponsiblePerson : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esRegistrationResponsiblePersonQuery GetDynamicQuery()
        {
            return null;
        }

        public esRegistrationResponsiblePerson()
        {
        }

        public esRegistrationResponsiblePerson(DataRow row)
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
            esRegistrationResponsiblePersonQuery query = this.GetDynamicQuery();
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
                        case "NameOfTheResponsible": this.str.NameOfTheResponsible = (string)value; break;
                        case "SRRelationship": this.str.SRRelationship = (string)value; break;
                        case "SROccupation": this.str.SROccupation = (string)value; break;
                        case "HomeAddress": this.str.HomeAddress = (string)value; break;
                        case "PhoneNo": this.str.PhoneNo = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "JobDescription": this.str.JobDescription = (string)value; break;
                        case "Ssn": this.str.Ssn = (string)value; break;
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
        /// Maps to RegistrationResponsiblePerson.RegistrationNo
        /// </summary>
        virtual public System.String RegistrationNo
        {
            get
            {
                return base.GetSystemString(RegistrationResponsiblePersonMetadata.ColumnNames.RegistrationNo);
            }

            set
            {
                base.SetSystemString(RegistrationResponsiblePersonMetadata.ColumnNames.RegistrationNo, value);
            }
        }
        /// <summary>
        /// Maps to RegistrationResponsiblePerson.NameOfTheResponsible
        /// </summary>
        virtual public System.String NameOfTheResponsible
        {
            get
            {
                return base.GetSystemString(RegistrationResponsiblePersonMetadata.ColumnNames.NameOfTheResponsible);
            }

            set
            {
                base.SetSystemString(RegistrationResponsiblePersonMetadata.ColumnNames.NameOfTheResponsible, value);
            }
        }
        /// <summary>
        /// Maps to RegistrationResponsiblePerson.SRRelationship
        /// </summary>
        virtual public System.String SRRelationship
        {
            get
            {
                return base.GetSystemString(RegistrationResponsiblePersonMetadata.ColumnNames.SRRelationship);
            }

            set
            {
                base.SetSystemString(RegistrationResponsiblePersonMetadata.ColumnNames.SRRelationship, value);
            }
        }
        /// <summary>
        /// Maps to RegistrationResponsiblePerson.SROccupation
        /// </summary>
        virtual public System.String SROccupation
        {
            get
            {
                return base.GetSystemString(RegistrationResponsiblePersonMetadata.ColumnNames.SROccupation);
            }

            set
            {
                base.SetSystemString(RegistrationResponsiblePersonMetadata.ColumnNames.SROccupation, value);
            }
        }
        /// <summary>
        /// Maps to RegistrationResponsiblePerson.HomeAddress
        /// </summary>
        virtual public System.String HomeAddress
        {
            get
            {
                return base.GetSystemString(RegistrationResponsiblePersonMetadata.ColumnNames.HomeAddress);
            }

            set
            {
                base.SetSystemString(RegistrationResponsiblePersonMetadata.ColumnNames.HomeAddress, value);
            }
        }
        /// <summary>
        /// Maps to RegistrationResponsiblePerson.PhoneNo
        /// </summary>
        virtual public System.String PhoneNo
        {
            get
            {
                return base.GetSystemString(RegistrationResponsiblePersonMetadata.ColumnNames.PhoneNo);
            }

            set
            {
                base.SetSystemString(RegistrationResponsiblePersonMetadata.ColumnNames.PhoneNo, value);
            }
        }
        /// <summary>
        /// Maps to RegistrationResponsiblePerson.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(RegistrationResponsiblePersonMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(RegistrationResponsiblePersonMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to RegistrationResponsiblePerson.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(RegistrationResponsiblePersonMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(RegistrationResponsiblePersonMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to RegistrationResponsiblePerson.JobDescription
        /// </summary>
        virtual public System.String JobDescription
        {
            get
            {
                return base.GetSystemString(RegistrationResponsiblePersonMetadata.ColumnNames.JobDescription);
            }

            set
            {
                base.SetSystemString(RegistrationResponsiblePersonMetadata.ColumnNames.JobDescription, value);
            }
        }
        /// <summary>
        /// Maps to RegistrationResponsiblePerson.Ssn
        /// </summary>
        virtual public System.String Ssn
        {
            get
            {
                return base.GetSystemString(RegistrationResponsiblePersonMetadata.ColumnNames.Ssn);
            }

            set
            {
                base.SetSystemString(RegistrationResponsiblePersonMetadata.ColumnNames.Ssn, value);
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
            public esStrings(esRegistrationResponsiblePerson entity)
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
            public System.String NameOfTheResponsible
            {
                get
                {
                    System.String data = entity.NameOfTheResponsible;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.NameOfTheResponsible = null;
                    else entity.NameOfTheResponsible = Convert.ToString(value);
                }
            }
            public System.String SRRelationship
            {
                get
                {
                    System.String data = entity.SRRelationship;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRRelationship = null;
                    else entity.SRRelationship = Convert.ToString(value);
                }
            }
            public System.String SROccupation
            {
                get
                {
                    System.String data = entity.SROccupation;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SROccupation = null;
                    else entity.SROccupation = Convert.ToString(value);
                }
            }
            public System.String HomeAddress
            {
                get
                {
                    System.String data = entity.HomeAddress;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.HomeAddress = null;
                    else entity.HomeAddress = Convert.ToString(value);
                }
            }
            public System.String PhoneNo
            {
                get
                {
                    System.String data = entity.PhoneNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PhoneNo = null;
                    else entity.PhoneNo = Convert.ToString(value);
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
            public System.String JobDescription
            {
                get
                {
                    System.String data = entity.JobDescription;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.JobDescription = null;
                    else entity.JobDescription = Convert.ToString(value);
                }
            }
            public System.String Ssn
            {
                get
                {
                    System.String data = entity.Ssn;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Ssn = null;
                    else entity.Ssn = Convert.ToString(value);
                }
            }
            private esRegistrationResponsiblePerson entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esRegistrationResponsiblePersonQuery query)
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
                throw new Exception("esRegistrationResponsiblePerson can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class RegistrationResponsiblePerson : esRegistrationResponsiblePerson
    {
    }

    [Serializable]
    abstract public class esRegistrationResponsiblePersonQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return RegistrationResponsiblePersonMetadata.Meta();
            }
        }

        public esQueryItem RegistrationNo
        {
            get
            {
                return new esQueryItem(this, RegistrationResponsiblePersonMetadata.ColumnNames.RegistrationNo, esSystemType.String);
            }
        }

        public esQueryItem NameOfTheResponsible
        {
            get
            {
                return new esQueryItem(this, RegistrationResponsiblePersonMetadata.ColumnNames.NameOfTheResponsible, esSystemType.String);
            }
        }

        public esQueryItem SRRelationship
        {
            get
            {
                return new esQueryItem(this, RegistrationResponsiblePersonMetadata.ColumnNames.SRRelationship, esSystemType.String);
            }
        }

        public esQueryItem SROccupation
        {
            get
            {
                return new esQueryItem(this, RegistrationResponsiblePersonMetadata.ColumnNames.SROccupation, esSystemType.String);
            }
        }

        public esQueryItem HomeAddress
        {
            get
            {
                return new esQueryItem(this, RegistrationResponsiblePersonMetadata.ColumnNames.HomeAddress, esSystemType.String);
            }
        }

        public esQueryItem PhoneNo
        {
            get
            {
                return new esQueryItem(this, RegistrationResponsiblePersonMetadata.ColumnNames.PhoneNo, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, RegistrationResponsiblePersonMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, RegistrationResponsiblePersonMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem JobDescription
        {
            get
            {
                return new esQueryItem(this, RegistrationResponsiblePersonMetadata.ColumnNames.JobDescription, esSystemType.String);
            }
        }

        public esQueryItem Ssn
        {
            get
            {
                return new esQueryItem(this, RegistrationResponsiblePersonMetadata.ColumnNames.Ssn, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("RegistrationResponsiblePersonCollection")]
    public partial class RegistrationResponsiblePersonCollection : esRegistrationResponsiblePersonCollection, IEnumerable<RegistrationResponsiblePerson>
    {
        public RegistrationResponsiblePersonCollection()
        {

        }

        public static implicit operator List<RegistrationResponsiblePerson>(RegistrationResponsiblePersonCollection coll)
        {
            List<RegistrationResponsiblePerson> list = new List<RegistrationResponsiblePerson>();

            foreach (RegistrationResponsiblePerson emp in coll)
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
                return RegistrationResponsiblePersonMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new RegistrationResponsiblePersonQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new RegistrationResponsiblePerson(row);
        }

        override protected esEntity CreateEntity()
        {
            return new RegistrationResponsiblePerson();
        }

        #endregion

        [BrowsableAttribute(false)]
        public RegistrationResponsiblePersonQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new RegistrationResponsiblePersonQuery();
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
        public bool Load(RegistrationResponsiblePersonQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public RegistrationResponsiblePerson AddNew()
        {
            RegistrationResponsiblePerson entity = base.AddNewEntity() as RegistrationResponsiblePerson;

            return entity;
        }
        public RegistrationResponsiblePerson FindByPrimaryKey(String registrationNo)
        {
            return base.FindByPrimaryKey(registrationNo) as RegistrationResponsiblePerson;
        }

        #region IEnumerable< RegistrationResponsiblePerson> Members

        IEnumerator<RegistrationResponsiblePerson> IEnumerable<RegistrationResponsiblePerson>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as RegistrationResponsiblePerson;
            }
        }

        #endregion

        private RegistrationResponsiblePersonQuery query;
    }


    /// <summary>
    /// Encapsulates the 'RegistrationResponsiblePerson' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("RegistrationResponsiblePerson ({RegistrationNo})")]
    [Serializable]
    public partial class RegistrationResponsiblePerson : esRegistrationResponsiblePerson
    {
        public RegistrationResponsiblePerson()
        {
        }

        public RegistrationResponsiblePerson(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return RegistrationResponsiblePersonMetadata.Meta();
            }
        }

        override protected esRegistrationResponsiblePersonQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new RegistrationResponsiblePersonQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public RegistrationResponsiblePersonQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new RegistrationResponsiblePersonQuery();
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
        public bool Load(RegistrationResponsiblePersonQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private RegistrationResponsiblePersonQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class RegistrationResponsiblePersonQuery : esRegistrationResponsiblePersonQuery
    {
        public RegistrationResponsiblePersonQuery()
        {

        }

        public RegistrationResponsiblePersonQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "RegistrationResponsiblePersonQuery";
        }
    }

    [Serializable]
    public partial class RegistrationResponsiblePersonMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected RegistrationResponsiblePersonMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(RegistrationResponsiblePersonMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = RegistrationResponsiblePersonMetadata.PropertyNames.RegistrationNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationResponsiblePersonMetadata.ColumnNames.NameOfTheResponsible, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = RegistrationResponsiblePersonMetadata.PropertyNames.NameOfTheResponsible;
            c.CharacterMaxLength = 150;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationResponsiblePersonMetadata.ColumnNames.SRRelationship, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = RegistrationResponsiblePersonMetadata.PropertyNames.SRRelationship;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationResponsiblePersonMetadata.ColumnNames.SROccupation, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = RegistrationResponsiblePersonMetadata.PropertyNames.SROccupation;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationResponsiblePersonMetadata.ColumnNames.HomeAddress, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = RegistrationResponsiblePersonMetadata.PropertyNames.HomeAddress;
            c.CharacterMaxLength = 250;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationResponsiblePersonMetadata.ColumnNames.PhoneNo, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = RegistrationResponsiblePersonMetadata.PropertyNames.PhoneNo;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationResponsiblePersonMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = RegistrationResponsiblePersonMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationResponsiblePersonMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = RegistrationResponsiblePersonMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationResponsiblePersonMetadata.ColumnNames.JobDescription, 8, typeof(System.String), esSystemType.String);
            c.PropertyName = RegistrationResponsiblePersonMetadata.PropertyNames.JobDescription;
            c.CharacterMaxLength = 150;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationResponsiblePersonMetadata.ColumnNames.Ssn, 9, typeof(System.String), esSystemType.String);
            c.PropertyName = RegistrationResponsiblePersonMetadata.PropertyNames.Ssn;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public RegistrationResponsiblePersonMetadata Meta()
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
            public const string NameOfTheResponsible = "NameOfTheResponsible";
            public const string SRRelationship = "SRRelationship";
            public const string SROccupation = "SROccupation";
            public const string HomeAddress = "HomeAddress";
            public const string PhoneNo = "PhoneNo";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string JobDescription = "JobDescription";
            public const string Ssn = "Ssn";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string RegistrationNo = "RegistrationNo";
            public const string NameOfTheResponsible = "NameOfTheResponsible";
            public const string SRRelationship = "SRRelationship";
            public const string SROccupation = "SROccupation";
            public const string HomeAddress = "HomeAddress";
            public const string PhoneNo = "PhoneNo";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string JobDescription = "JobDescription";
            public const string Ssn = "Ssn";
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
            lock (typeof(RegistrationResponsiblePersonMetadata))
            {
                if (RegistrationResponsiblePersonMetadata.mapDelegates == null)
                {
                    RegistrationResponsiblePersonMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (RegistrationResponsiblePersonMetadata.meta == null)
                {
                    RegistrationResponsiblePersonMetadata.meta = new RegistrationResponsiblePersonMetadata();
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
                meta.AddTypeMap("NameOfTheResponsible", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRRelationship", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SROccupation", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("HomeAddress", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("PhoneNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("JobDescription", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Ssn", new esTypeMap("varchar", "System.String"));


                meta.Source = "RegistrationResponsiblePerson";
                meta.Destination = "RegistrationResponsiblePerson";
                meta.spInsert = "proc_RegistrationResponsiblePersonInsert";
                meta.spUpdate = "proc_RegistrationResponsiblePersonUpdate";
                meta.spDelete = "proc_RegistrationResponsiblePersonDelete";
                meta.spLoadAll = "proc_RegistrationResponsiblePersonLoadAll";
                meta.spLoadByPrimaryKey = "proc_RegistrationResponsiblePersonLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private RegistrationResponsiblePersonMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
