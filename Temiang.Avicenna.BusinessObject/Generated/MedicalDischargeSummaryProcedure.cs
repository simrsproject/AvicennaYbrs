/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 3/5/2025 8:28:57 PM
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
    abstract public class esMedicalDischargeSummaryProcedureCollection : esEntityCollectionWAuditLog
    {
        public esMedicalDischargeSummaryProcedureCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "MedicalDischargeSummaryProcedureCollection";
        }

        #region Query Logic
        protected void InitQuery(esMedicalDischargeSummaryProcedureQuery query)
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
            this.InitQuery(query as esMedicalDischargeSummaryProcedureQuery);
        }
        #endregion

        virtual public MedicalDischargeSummaryProcedure DetachEntity(MedicalDischargeSummaryProcedure entity)
        {
            return base.DetachEntity(entity) as MedicalDischargeSummaryProcedure;
        }

        virtual public MedicalDischargeSummaryProcedure AttachEntity(MedicalDischargeSummaryProcedure entity)
        {
            return base.AttachEntity(entity) as MedicalDischargeSummaryProcedure;
        }

        virtual public void Combine(MedicalDischargeSummaryProcedureCollection collection)
        {
            base.Combine(collection);
        }

        new public MedicalDischargeSummaryProcedure this[int index]
        {
            get
            {
                return base[index] as MedicalDischargeSummaryProcedure;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(MedicalDischargeSummaryProcedure);
        }
    }

    [Serializable]
    abstract public class esMedicalDischargeSummaryProcedure : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esMedicalDischargeSummaryProcedureQuery GetDynamicQuery()
        {
            return null;
        }

        public esMedicalDischargeSummaryProcedure()
        {
        }

        public esMedicalDischargeSummaryProcedure(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String registrationNo, String sequenceNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(registrationNo, sequenceNo);
            else
                return LoadByPrimaryKeyStoredProcedure(registrationNo, sequenceNo);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String registrationNo, String sequenceNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(registrationNo, sequenceNo);
            else
                return LoadByPrimaryKeyStoredProcedure(registrationNo, sequenceNo);
        }

        private bool LoadByPrimaryKeyDynamic(String registrationNo, String sequenceNo)
        {
            esMedicalDischargeSummaryProcedureQuery query = this.GetDynamicQuery();
            query.Where(query.RegistrationNo == registrationNo, query.SequenceNo == sequenceNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String registrationNo, String sequenceNo)
        {
            esParameters parms = new esParameters();
            parms.Add("RegistrationNo", registrationNo);
            parms.Add("SequenceNo", sequenceNo);
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
                        case "SequenceNo": this.str.SequenceNo = (string)value; break;
                        case "ProcedureID": this.str.ProcedureID = (string)value; break;
                        case "ProcedureName": this.str.ProcedureName = (string)value; break;
                        case "ParamedicID": this.str.ParamedicID = (string)value; break;
                        case "IsVoid": this.str.IsVoid = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "ProcedureSynonym": this.str.ProcedureSynonym = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
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
        /// Maps to MedicalDischargeSummaryProcedure.RegistrationNo
        /// </summary>
        virtual public System.String RegistrationNo
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryProcedureMetadata.ColumnNames.RegistrationNo);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryProcedureMetadata.ColumnNames.RegistrationNo, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryProcedure.SequenceNo
        /// </summary>
        virtual public System.String SequenceNo
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryProcedureMetadata.ColumnNames.SequenceNo);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryProcedureMetadata.ColumnNames.SequenceNo, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryProcedure.ProcedureID
        /// </summary>
        virtual public System.String ProcedureID
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryProcedureMetadata.ColumnNames.ProcedureID);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryProcedureMetadata.ColumnNames.ProcedureID, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryProcedure.ProcedureName
        /// </summary>
        virtual public System.String ProcedureName
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryProcedureMetadata.ColumnNames.ProcedureName);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryProcedureMetadata.ColumnNames.ProcedureName, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryProcedure.ParamedicID
        /// </summary>
        virtual public System.String ParamedicID
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryProcedureMetadata.ColumnNames.ParamedicID);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryProcedureMetadata.ColumnNames.ParamedicID, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryProcedure.IsVoid
        /// </summary>
        virtual public System.Boolean? IsVoid
        {
            get
            {
                return base.GetSystemBoolean(MedicalDischargeSummaryProcedureMetadata.ColumnNames.IsVoid);
            }

            set
            {
                base.SetSystemBoolean(MedicalDischargeSummaryProcedureMetadata.ColumnNames.IsVoid, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryProcedure.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(MedicalDischargeSummaryProcedureMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(MedicalDischargeSummaryProcedureMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryProcedure.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryProcedureMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryProcedureMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryProcedure.ProcedureSynonym
        /// </summary>
        virtual public System.String ProcedureSynonym
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryProcedureMetadata.ColumnNames.ProcedureSynonym);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryProcedureMetadata.ColumnNames.ProcedureSynonym, value);
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
            public esStrings(esMedicalDischargeSummaryProcedure entity)
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
            public System.String SequenceNo
            {
                get
                {
                    System.String data = entity.SequenceNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SequenceNo = null;
                    else entity.SequenceNo = Convert.ToString(value);
                }
            }
            public System.String ProcedureID
            {
                get
                {
                    System.String data = entity.ProcedureID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ProcedureID = null;
                    else entity.ProcedureID = Convert.ToString(value);
                }
            }
            public System.String ProcedureName
            {
                get
                {
                    System.String data = entity.ProcedureName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ProcedureName = null;
                    else entity.ProcedureName = Convert.ToString(value);
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
            public System.String ProcedureSynonym
            {
                get
                {
                    System.String data = entity.ProcedureSynonym;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ProcedureSynonym = null;
                    else entity.ProcedureSynonym = Convert.ToString(value);
                }
            }
            private esMedicalDischargeSummaryProcedure entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esMedicalDischargeSummaryProcedureQuery query)
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
                throw new Exception("esMedicalDischargeSummaryProcedure can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class MedicalDischargeSummaryProcedure : esMedicalDischargeSummaryProcedure
    {
    }

    [Serializable]
    abstract public class esMedicalDischargeSummaryProcedureQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return MedicalDischargeSummaryProcedureMetadata.Meta();
            }
        }

        public esQueryItem RegistrationNo
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryProcedureMetadata.ColumnNames.RegistrationNo, esSystemType.String);
            }
        }

        public esQueryItem SequenceNo
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryProcedureMetadata.ColumnNames.SequenceNo, esSystemType.String);
            }
        }

        public esQueryItem ProcedureID
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryProcedureMetadata.ColumnNames.ProcedureID, esSystemType.String);
            }
        }

        public esQueryItem ProcedureName
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryProcedureMetadata.ColumnNames.ProcedureName, esSystemType.String);
            }
        }

        public esQueryItem ParamedicID
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryProcedureMetadata.ColumnNames.ParamedicID, esSystemType.String);
            }
        }

        public esQueryItem IsVoid
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryProcedureMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryProcedureMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryProcedureMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem ProcedureSynonym
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryProcedureMetadata.ColumnNames.ProcedureSynonym, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("MedicalDischargeSummaryProcedureCollection")]
    public partial class MedicalDischargeSummaryProcedureCollection : esMedicalDischargeSummaryProcedureCollection, IEnumerable<MedicalDischargeSummaryProcedure>
    {
        public MedicalDischargeSummaryProcedureCollection()
        {

        }

        public static implicit operator List<MedicalDischargeSummaryProcedure>(MedicalDischargeSummaryProcedureCollection coll)
        {
            List<MedicalDischargeSummaryProcedure> list = new List<MedicalDischargeSummaryProcedure>();

            foreach (MedicalDischargeSummaryProcedure emp in coll)
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
                return MedicalDischargeSummaryProcedureMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new MedicalDischargeSummaryProcedureQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new MedicalDischargeSummaryProcedure(row);
        }

        override protected esEntity CreateEntity()
        {
            return new MedicalDischargeSummaryProcedure();
        }

        #endregion

        [BrowsableAttribute(false)]
        public MedicalDischargeSummaryProcedureQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new MedicalDischargeSummaryProcedureQuery();
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
        public bool Load(MedicalDischargeSummaryProcedureQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public MedicalDischargeSummaryProcedure AddNew()
        {
            MedicalDischargeSummaryProcedure entity = base.AddNewEntity() as MedicalDischargeSummaryProcedure;

            return entity;
        }
        public MedicalDischargeSummaryProcedure FindByPrimaryKey(String registrationNo, String sequenceNo)
        {
            return base.FindByPrimaryKey(registrationNo, sequenceNo) as MedicalDischargeSummaryProcedure;
        }

        #region IEnumerable< MedicalDischargeSummaryProcedure> Members

        IEnumerator<MedicalDischargeSummaryProcedure> IEnumerable<MedicalDischargeSummaryProcedure>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as MedicalDischargeSummaryProcedure;
            }
        }

        #endregion

        private MedicalDischargeSummaryProcedureQuery query;
    }


    /// <summary>
    /// Encapsulates the 'MedicalDischargeSummaryProcedure' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("MedicalDischargeSummaryProcedure ({RegistrationNo, SequenceNo})")]
    [Serializable]
    public partial class MedicalDischargeSummaryProcedure : esMedicalDischargeSummaryProcedure
    {
        public MedicalDischargeSummaryProcedure()
        {
        }

        public MedicalDischargeSummaryProcedure(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return MedicalDischargeSummaryProcedureMetadata.Meta();
            }
        }

        override protected esMedicalDischargeSummaryProcedureQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new MedicalDischargeSummaryProcedureQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public MedicalDischargeSummaryProcedureQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new MedicalDischargeSummaryProcedureQuery();
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
        public bool Load(MedicalDischargeSummaryProcedureQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private MedicalDischargeSummaryProcedureQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class MedicalDischargeSummaryProcedureQuery : esMedicalDischargeSummaryProcedureQuery
    {
        public MedicalDischargeSummaryProcedureQuery()
        {

        }

        public MedicalDischargeSummaryProcedureQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "MedicalDischargeSummaryProcedureQuery";
        }
    }

    [Serializable]
    public partial class MedicalDischargeSummaryProcedureMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected MedicalDischargeSummaryProcedureMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(MedicalDischargeSummaryProcedureMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryProcedureMetadata.PropertyNames.RegistrationNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryProcedureMetadata.ColumnNames.SequenceNo, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryProcedureMetadata.PropertyNames.SequenceNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 3;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryProcedureMetadata.ColumnNames.ProcedureID, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryProcedureMetadata.PropertyNames.ProcedureID;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryProcedureMetadata.ColumnNames.ProcedureName, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryProcedureMetadata.PropertyNames.ProcedureName;
            c.CharacterMaxLength = 1000;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryProcedureMetadata.ColumnNames.ParamedicID, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryProcedureMetadata.PropertyNames.ParamedicID;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryProcedureMetadata.ColumnNames.IsVoid, 5, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = MedicalDischargeSummaryProcedureMetadata.PropertyNames.IsVoid;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryProcedureMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = MedicalDischargeSummaryProcedureMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryProcedureMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryProcedureMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 40;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryProcedureMetadata.ColumnNames.ProcedureSynonym, 8, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryProcedureMetadata.PropertyNames.ProcedureSynonym;
            c.CharacterMaxLength = 200;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public MedicalDischargeSummaryProcedureMetadata Meta()
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
            public const string SequenceNo = "SequenceNo";
            public const string ProcedureID = "ProcedureID";
            public const string ProcedureName = "ProcedureName";
            public const string ParamedicID = "ParamedicID";
            public const string IsVoid = "IsVoid";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string ProcedureSynonym = "ProcedureSynonym";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string RegistrationNo = "RegistrationNo";
            public const string SequenceNo = "SequenceNo";
            public const string ProcedureID = "ProcedureID";
            public const string ProcedureName = "ProcedureName";
            public const string ParamedicID = "ParamedicID";
            public const string IsVoid = "IsVoid";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string ProcedureSynonym = "ProcedureSynonym";
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
            lock (typeof(MedicalDischargeSummaryProcedureMetadata))
            {
                if (MedicalDischargeSummaryProcedureMetadata.mapDelegates == null)
                {
                    MedicalDischargeSummaryProcedureMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (MedicalDischargeSummaryProcedureMetadata.meta == null)
                {
                    MedicalDischargeSummaryProcedureMetadata.meta = new MedicalDischargeSummaryProcedureMetadata();
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
                meta.AddTypeMap("SequenceNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ProcedureID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ProcedureName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ProcedureSynonym", new esTypeMap("varchar", "System.String"));


                meta.Source = "MedicalDischargeSummaryProcedure";
                meta.Destination = "MedicalDischargeSummaryProcedure";
                meta.spInsert = "proc_MedicalDischargeSummaryProcedureInsert";
                meta.spUpdate = "proc_MedicalDischargeSummaryProcedureUpdate";
                meta.spDelete = "proc_MedicalDischargeSummaryProcedureDelete";
                meta.spLoadAll = "proc_MedicalDischargeSummaryProcedureLoadAll";
                meta.spLoadByPrimaryKey = "proc_MedicalDischargeSummaryProcedureLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private MedicalDischargeSummaryProcedureMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
