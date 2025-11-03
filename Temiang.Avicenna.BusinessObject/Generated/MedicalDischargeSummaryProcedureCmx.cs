/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 3/5/2025 8:32:30 PM
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
    abstract public class esMedicalDischargeSummaryProcedureCmxCollection : esEntityCollectionWAuditLog
    {
        public esMedicalDischargeSummaryProcedureCmxCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "MedicalDischargeSummaryProcedureCmxCollection";
        }

        #region Query Logic
        protected void InitQuery(esMedicalDischargeSummaryProcedureCmxQuery query)
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
            this.InitQuery(query as esMedicalDischargeSummaryProcedureCmxQuery);
        }
        #endregion

        virtual public MedicalDischargeSummaryProcedureCmx DetachEntity(MedicalDischargeSummaryProcedureCmx entity)
        {
            return base.DetachEntity(entity) as MedicalDischargeSummaryProcedureCmx;
        }

        virtual public MedicalDischargeSummaryProcedureCmx AttachEntity(MedicalDischargeSummaryProcedureCmx entity)
        {
            return base.AttachEntity(entity) as MedicalDischargeSummaryProcedureCmx;
        }

        virtual public void Combine(MedicalDischargeSummaryProcedureCmxCollection collection)
        {
            base.Combine(collection);
        }

        new public MedicalDischargeSummaryProcedureCmx this[int index]
        {
            get
            {
                return base[index] as MedicalDischargeSummaryProcedureCmx;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(MedicalDischargeSummaryProcedureCmx);
        }
    }

    [Serializable]
    abstract public class esMedicalDischargeSummaryProcedureCmx : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esMedicalDischargeSummaryProcedureCmxQuery GetDynamicQuery()
        {
            return null;
        }

        public esMedicalDischargeSummaryProcedureCmx()
        {
        }

        public esMedicalDischargeSummaryProcedureCmx(DataRow row)
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
            esMedicalDischargeSummaryProcedureCmxQuery query = this.GetDynamicQuery();
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
        /// Maps to MedicalDischargeSummaryProcedureCmx.RegistrationNo
        /// </summary>
        virtual public System.String RegistrationNo
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryProcedureCmxMetadata.ColumnNames.RegistrationNo);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryProcedureCmxMetadata.ColumnNames.RegistrationNo, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryProcedureCmx.SequenceNo
        /// </summary>
        virtual public System.String SequenceNo
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryProcedureCmxMetadata.ColumnNames.SequenceNo);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryProcedureCmxMetadata.ColumnNames.SequenceNo, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryProcedureCmx.ProcedureID
        /// </summary>
        virtual public System.String ProcedureID
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryProcedureCmxMetadata.ColumnNames.ProcedureID);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryProcedureCmxMetadata.ColumnNames.ProcedureID, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryProcedureCmx.ProcedureName
        /// </summary>
        virtual public System.String ProcedureName
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryProcedureCmxMetadata.ColumnNames.ProcedureName);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryProcedureCmxMetadata.ColumnNames.ProcedureName, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryProcedureCmx.ParamedicID
        /// </summary>
        virtual public System.String ParamedicID
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryProcedureCmxMetadata.ColumnNames.ParamedicID);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryProcedureCmxMetadata.ColumnNames.ParamedicID, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryProcedureCmx.IsVoid
        /// </summary>
        virtual public System.Boolean? IsVoid
        {
            get
            {
                return base.GetSystemBoolean(MedicalDischargeSummaryProcedureCmxMetadata.ColumnNames.IsVoid);
            }

            set
            {
                base.SetSystemBoolean(MedicalDischargeSummaryProcedureCmxMetadata.ColumnNames.IsVoid, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryProcedureCmx.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(MedicalDischargeSummaryProcedureCmxMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(MedicalDischargeSummaryProcedureCmxMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryProcedureCmx.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryProcedureCmxMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryProcedureCmxMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryProcedureCmx.ProcedureSynonym
        /// </summary>
        virtual public System.String ProcedureSynonym
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryProcedureCmxMetadata.ColumnNames.ProcedureSynonym);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryProcedureCmxMetadata.ColumnNames.ProcedureSynonym, value);
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
            public esStrings(esMedicalDischargeSummaryProcedureCmx entity)
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
            private esMedicalDischargeSummaryProcedureCmx entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esMedicalDischargeSummaryProcedureCmxQuery query)
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
                throw new Exception("esMedicalDischargeSummaryProcedureCmx can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class MedicalDischargeSummaryProcedureCmx : esMedicalDischargeSummaryProcedureCmx
    {
    }

    [Serializable]
    abstract public class esMedicalDischargeSummaryProcedureCmxQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return MedicalDischargeSummaryProcedureCmxMetadata.Meta();
            }
        }

        public esQueryItem RegistrationNo
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryProcedureCmxMetadata.ColumnNames.RegistrationNo, esSystemType.String);
            }
        }

        public esQueryItem SequenceNo
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryProcedureCmxMetadata.ColumnNames.SequenceNo, esSystemType.String);
            }
        }

        public esQueryItem ProcedureID
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryProcedureCmxMetadata.ColumnNames.ProcedureID, esSystemType.String);
            }
        }

        public esQueryItem ProcedureName
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryProcedureCmxMetadata.ColumnNames.ProcedureName, esSystemType.String);
            }
        }

        public esQueryItem ParamedicID
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryProcedureCmxMetadata.ColumnNames.ParamedicID, esSystemType.String);
            }
        }

        public esQueryItem IsVoid
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryProcedureCmxMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryProcedureCmxMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryProcedureCmxMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem ProcedureSynonym
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryProcedureCmxMetadata.ColumnNames.ProcedureSynonym, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("MedicalDischargeSummaryProcedureCmxCollection")]
    public partial class MedicalDischargeSummaryProcedureCmxCollection : esMedicalDischargeSummaryProcedureCmxCollection, IEnumerable<MedicalDischargeSummaryProcedureCmx>
    {
        public MedicalDischargeSummaryProcedureCmxCollection()
        {

        }

        public static implicit operator List<MedicalDischargeSummaryProcedureCmx>(MedicalDischargeSummaryProcedureCmxCollection coll)
        {
            List<MedicalDischargeSummaryProcedureCmx> list = new List<MedicalDischargeSummaryProcedureCmx>();

            foreach (MedicalDischargeSummaryProcedureCmx emp in coll)
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
                return MedicalDischargeSummaryProcedureCmxMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new MedicalDischargeSummaryProcedureCmxQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new MedicalDischargeSummaryProcedureCmx(row);
        }

        override protected esEntity CreateEntity()
        {
            return new MedicalDischargeSummaryProcedureCmx();
        }

        #endregion

        [BrowsableAttribute(false)]
        public MedicalDischargeSummaryProcedureCmxQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new MedicalDischargeSummaryProcedureCmxQuery();
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
        public bool Load(MedicalDischargeSummaryProcedureCmxQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public MedicalDischargeSummaryProcedureCmx AddNew()
        {
            MedicalDischargeSummaryProcedureCmx entity = base.AddNewEntity() as MedicalDischargeSummaryProcedureCmx;

            return entity;
        }
        public MedicalDischargeSummaryProcedureCmx FindByPrimaryKey(String registrationNo, String sequenceNo)
        {
            return base.FindByPrimaryKey(registrationNo, sequenceNo) as MedicalDischargeSummaryProcedureCmx;
        }

        #region IEnumerable< MedicalDischargeSummaryProcedureCmx> Members

        IEnumerator<MedicalDischargeSummaryProcedureCmx> IEnumerable<MedicalDischargeSummaryProcedureCmx>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as MedicalDischargeSummaryProcedureCmx;
            }
        }

        #endregion

        private MedicalDischargeSummaryProcedureCmxQuery query;
    }


    /// <summary>
    /// Encapsulates the 'MedicalDischargeSummaryProcedureCmx' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("MedicalDischargeSummaryProcedureCmx ({RegistrationNo, SequenceNo})")]
    [Serializable]
    public partial class MedicalDischargeSummaryProcedureCmx : esMedicalDischargeSummaryProcedureCmx
    {
        public MedicalDischargeSummaryProcedureCmx()
        {
        }

        public MedicalDischargeSummaryProcedureCmx(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return MedicalDischargeSummaryProcedureCmxMetadata.Meta();
            }
        }

        override protected esMedicalDischargeSummaryProcedureCmxQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new MedicalDischargeSummaryProcedureCmxQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public MedicalDischargeSummaryProcedureCmxQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new MedicalDischargeSummaryProcedureCmxQuery();
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
        public bool Load(MedicalDischargeSummaryProcedureCmxQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private MedicalDischargeSummaryProcedureCmxQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class MedicalDischargeSummaryProcedureCmxQuery : esMedicalDischargeSummaryProcedureCmxQuery
    {
        public MedicalDischargeSummaryProcedureCmxQuery()
        {

        }

        public MedicalDischargeSummaryProcedureCmxQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "MedicalDischargeSummaryProcedureCmxQuery";
        }
    }

    [Serializable]
    public partial class MedicalDischargeSummaryProcedureCmxMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected MedicalDischargeSummaryProcedureCmxMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(MedicalDischargeSummaryProcedureCmxMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryProcedureCmxMetadata.PropertyNames.RegistrationNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryProcedureCmxMetadata.ColumnNames.SequenceNo, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryProcedureCmxMetadata.PropertyNames.SequenceNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 3;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryProcedureCmxMetadata.ColumnNames.ProcedureID, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryProcedureCmxMetadata.PropertyNames.ProcedureID;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryProcedureCmxMetadata.ColumnNames.ProcedureName, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryProcedureCmxMetadata.PropertyNames.ProcedureName;
            c.CharacterMaxLength = 1000;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryProcedureCmxMetadata.ColumnNames.ParamedicID, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryProcedureCmxMetadata.PropertyNames.ParamedicID;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryProcedureCmxMetadata.ColumnNames.IsVoid, 5, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = MedicalDischargeSummaryProcedureCmxMetadata.PropertyNames.IsVoid;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryProcedureCmxMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = MedicalDischargeSummaryProcedureCmxMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryProcedureCmxMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryProcedureCmxMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 40;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryProcedureCmxMetadata.ColumnNames.ProcedureSynonym, 8, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryProcedureCmxMetadata.PropertyNames.ProcedureSynonym;
            c.CharacterMaxLength = 200;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public MedicalDischargeSummaryProcedureCmxMetadata Meta()
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
            lock (typeof(MedicalDischargeSummaryProcedureCmxMetadata))
            {
                if (MedicalDischargeSummaryProcedureCmxMetadata.mapDelegates == null)
                {
                    MedicalDischargeSummaryProcedureCmxMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (MedicalDischargeSummaryProcedureCmxMetadata.meta == null)
                {
                    MedicalDischargeSummaryProcedureCmxMetadata.meta = new MedicalDischargeSummaryProcedureCmxMetadata();
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


                meta.Source = "MedicalDischargeSummaryProcedureCmx";
                meta.Destination = "MedicalDischargeSummaryProcedureCmx";
                meta.spInsert = "proc_MedicalDischargeSummaryProcedureCmxInsert";
                meta.spUpdate = "proc_MedicalDischargeSummaryProcedureCmxUpdate";
                meta.spDelete = "proc_MedicalDischargeSummaryProcedureCmxDelete";
                meta.spLoadAll = "proc_MedicalDischargeSummaryProcedureCmxLoadAll";
                meta.spLoadByPrimaryKey = "proc_MedicalDischargeSummaryProcedureCmxLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private MedicalDischargeSummaryProcedureCmxMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
