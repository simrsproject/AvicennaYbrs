///*
//===============================================================================
//                       Persistence Layer and Business Objects  
//===============================================================================
//                       Date Generated       : 07/18/18 10:45:39 AM
//===============================================================================
//*/

//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Data;
//using System.ComponentModel;
//using System.Xml.Serialization;
//using Temiang.Dal.Core;
//using Temiang.Dal.Interfaces;
//using Temiang.Dal.DynamicQuery;

//namespace Temiang.Avicenna.BusinessObject
//{
//    [Serializable]
//    abstract public class esReferInPatientCollection : esEntityCollectionWAuditLog
//    {
//        public esReferInPatientCollection()
//        {

//        }


//        protected override string GetCollectionName()
//        {
//            return "ReferInPatientCollection";
//        }

//        #region Query Logic
//        protected void InitQuery(esReferInPatientQuery query)
//        {
//            query.OnLoadDelegate = this.OnQueryLoaded;
//            query.es2.Connection = ((IEntityCollection)this).Connection;
//        }

//        protected bool OnQueryLoaded(DataTable table)
//        {
//            this.PopulateCollection(table);
//            return (this.RowCount > 0) ? true : false;
//        }

//        protected override void HookupQuery(esDynamicQuery query)
//        {
//            this.InitQuery(query as esReferInPatientQuery);
//        }
//        #endregion

//        virtual public ReferInPatient DetachEntity(ReferInPatient entity)
//        {
//            return base.DetachEntity(entity) as ReferInPatient;
//        }

//        virtual public ReferInPatient AttachEntity(ReferInPatient entity)
//        {
//            return base.AttachEntity(entity) as ReferInPatient;
//        }

//        virtual public void Combine(ReferInPatientCollection collection)
//        {
//            base.Combine(collection);
//        }

//        new public ReferInPatient this[int index]
//        {
//            get
//            {
//                return base[index] as ReferInPatient;
//            }
//        }

//        public override Type GetEntityType()
//        {
//            return typeof(ReferInPatient);
//        }
//    }

//    [Serializable]
//    abstract public class esReferInPatient : esEntityWAuditLog
//    {
//        /// <summary>
//        /// Used internally by the entity's DynamicQuery mechanism.
//        /// </summary>
//        virtual protected esReferInPatientQuery GetDynamicQuery()
//        {
//            return null;
//        }

//        public esReferInPatient()
//        {
//        }

//        public esReferInPatient(DataRow row)
//            : base(row)
//        {
//        }


//        #region LoadByPrimaryKey
//        public virtual bool LoadByPrimaryKey(String registrationNo, Int32 sequenceNo)
//        {
//            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
//                return LoadByPrimaryKeyDynamic(registrationNo, sequenceNo);
//            else
//                return LoadByPrimaryKeyStoredProcedure(registrationNo, sequenceNo);
//        }

//        /// <summary>
//        /// Loads an entity by primary key
//        /// </summary>
//        /// <remarks>
//        /// Requires primary keys be defined on all tables.
//        /// If a table does not have a primary key set,
//        /// this method will not compile.
//        /// </remarks>
//        /// <param name="sqlAccessType">Either esSqlAccessType StoredProcedure or DynamicSQL</param>
//        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String registrationNo, Int32 sequenceNo)
//        {
//            if (sqlAccessType == esSqlAccessType.DynamicSQL)
//                return LoadByPrimaryKeyDynamic(registrationNo, sequenceNo);
//            else
//                return LoadByPrimaryKeyStoredProcedure(registrationNo, sequenceNo);
//        }

//        private bool LoadByPrimaryKeyDynamic(String registrationNo, Int32 sequenceNo)
//        {
//            esReferInPatientQuery query = this.GetDynamicQuery();
//            query.Where(query.RegistrationNo == registrationNo, query.SequenceNo == sequenceNo);
//            return query.Load();
//        }

//        private bool LoadByPrimaryKeyStoredProcedure(String registrationNo, Int32 sequenceNo)
//        {
//            esParameters parms = new esParameters();
//            parms.Add("RegistrationNo", registrationNo);
//            parms.Add("SequenceNo", sequenceNo);
//            return this.Load(esQueryType.StoredProcedure, this.es.spLoadByPrimaryKey, parms);
//        }
//        #endregion

//        #region Properties

//        public override void SetProperties(IDictionary values)
//        {
//            foreach (string propertyName in values.Keys)
//            {
//                this.SetProperty(propertyName, values[propertyName]);
//            }
//        }

//        public override void SetProperty(string name, object value)
//        {
//            if (this.Row == null) this.AddNew();

//            esColumnMetadata col = this.Meta.Columns.FindByPropertyName(name);
//            if (col != null)
//            {
//                if (value == null || value is System.String)
//                {
//                    // Use the strongly typed property
//                    switch (name)
//                    {
//                        case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
//                        case "SequenceNo": this.str.SequenceNo = (string)value; break;
//                        case "ReferDateTime": this.str.ReferDateTime = (string)value; break;
//                        case "FromParamedicID": this.str.FromParamedicID = (string)value; break;
//                        case "ToParamedicID": this.str.ToParamedicID = (string)value; break;
//                        case "ActionExamTreatment": this.str.ActionExamTreatment = (string)value; break;
//                        case "Notes": this.str.Notes = (string)value; break;
//                        case "Answer": this.str.Answer = (string)value; break;
//                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
//                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
//                    }
//                }
//                else
//                {
//                    switch (name)
//                    {
//                        case "SequenceNo":

//                            if (value == null || value is System.Int32)
//                                this.SequenceNo = (System.Int32?)value;
//                            break;
//                        case "ReferDateTime":

//                            if (value == null || value is System.DateTime)
//                                this.ReferDateTime = (System.DateTime?)value;
//                            break;
//                        case "LastUpdateDateTime":

//                            if (value == null || value is System.DateTime)
//                                this.LastUpdateDateTime = (System.DateTime?)value;
//                            break;

//                        default:
//                            break;
//                    }
//                }
//            }
//            else if (this.Row.Table.Columns.Contains(name))
//            {
//                this.Row[name] = value;
//            }
//            else
//            {
//                throw new Exception("SetProperty Error: '" + name + "' not found");
//            }
//        }

//        /// <summary>
//        /// Maps to ReferInPatient.RegistrationNo
//        /// </summary>
//        virtual public System.String RegistrationNo
//        {
//            get
//            {
//                return base.GetSystemString(ReferInPatientMetadata.ColumnNames.RegistrationNo);
//            }

//            set
//            {
//                base.SetSystemString(ReferInPatientMetadata.ColumnNames.RegistrationNo, value);
//            }
//        }
//        /// <summary>
//        /// Maps to ReferInPatient.SequenceNo
//        /// </summary>
//        virtual public System.Int32? SequenceNo
//        {
//            get
//            {
//                return base.GetSystemInt32(ReferInPatientMetadata.ColumnNames.SequenceNo);
//            }

//            set
//            {
//                base.SetSystemInt32(ReferInPatientMetadata.ColumnNames.SequenceNo, value);
//            }
//        }
//        /// <summary>
//        /// Maps to ReferInPatient.ReferDateTime
//        /// </summary>
//        virtual public System.DateTime? ReferDateTime
//        {
//            get
//            {
//                return base.GetSystemDateTime(ReferInPatientMetadata.ColumnNames.ReferDateTime);
//            }

//            set
//            {
//                base.SetSystemDateTime(ReferInPatientMetadata.ColumnNames.ReferDateTime, value);
//            }
//        }
//        /// <summary>
//        /// Maps to ReferInPatient.FromParamedicID
//        /// </summary>
//        virtual public System.String FromParamedicID
//        {
//            get
//            {
//                return base.GetSystemString(ReferInPatientMetadata.ColumnNames.FromParamedicID);
//            }

//            set
//            {
//                base.SetSystemString(ReferInPatientMetadata.ColumnNames.FromParamedicID, value);
//            }
//        }
//        /// <summary>
//        /// Maps to ReferInPatient.ToParamedicID
//        /// </summary>
//        virtual public System.String ToParamedicID
//        {
//            get
//            {
//                return base.GetSystemString(ReferInPatientMetadata.ColumnNames.ToParamedicID);
//            }

//            set
//            {
//                base.SetSystemString(ReferInPatientMetadata.ColumnNames.ToParamedicID, value);
//            }
//        }
//        /// <summary>
//        /// Maps to ReferInPatient.ActionExamTreatment
//        /// </summary>
//        virtual public System.String ActionExamTreatment
//        {
//            get
//            {
//                return base.GetSystemString(ReferInPatientMetadata.ColumnNames.ActionExamTreatment);
//            }

//            set
//            {
//                base.SetSystemString(ReferInPatientMetadata.ColumnNames.ActionExamTreatment, value);
//            }
//        }
//        /// <summary>
//        /// Maps to ReferInPatient.Notes
//        /// </summary>
//        virtual public System.String Notes
//        {
//            get
//            {
//                return base.GetSystemString(ReferInPatientMetadata.ColumnNames.Notes);
//            }

//            set
//            {
//                base.SetSystemString(ReferInPatientMetadata.ColumnNames.Notes, value);
//            }
//        }
//        /// <summary>
//        /// Maps to ReferInPatient.Answer
//        /// </summary>
//        virtual public System.String Answer
//        {
//            get
//            {
//                return base.GetSystemString(ReferInPatientMetadata.ColumnNames.Answer);
//            }

//            set
//            {
//                base.SetSystemString(ReferInPatientMetadata.ColumnNames.Answer, value);
//            }
//        }
//        /// <summary>
//        /// Maps to ReferInPatient.LastUpdateDateTime
//        /// </summary>
//        virtual public System.DateTime? LastUpdateDateTime
//        {
//            get
//            {
//                return base.GetSystemDateTime(ReferInPatientMetadata.ColumnNames.LastUpdateDateTime);
//            }

//            set
//            {
//                base.SetSystemDateTime(ReferInPatientMetadata.ColumnNames.LastUpdateDateTime, value);
//            }
//        }
//        /// <summary>
//        /// Maps to ReferInPatient.LastUpdateByUserID
//        /// </summary>
//        virtual public System.String LastUpdateByUserID
//        {
//            get
//            {
//                return base.GetSystemString(ReferInPatientMetadata.ColumnNames.LastUpdateByUserID);
//            }

//            set
//            {
//                base.SetSystemString(ReferInPatientMetadata.ColumnNames.LastUpdateByUserID, value);
//            }
//        }

//        #endregion

//        #region String Properties

//        /// <summary>
//        /// Converts an entity's properties to
//        /// and from strings.
//        /// </summary>
//        /// <remarks>
//        /// The str properties Get and Set provide easy conversion
//        /// between a string and a property's data type. Not all
//        /// data types will get a str property.
//        /// </remarks>
//        /// <example>
//        /// Set a datetime from a string.
//        /// <code>
//        /// Employees entity = new Employees();
//        /// entity.LoadByPrimaryKey(10);
//        /// entity.str.HireDate = "2007-01-01 00:00:00";
//        /// entity.Save();
//        /// </code>
//        /// Get a datetime as a string.
//        /// <code>
//        /// Employees entity = new Employees();
//        /// entity.LoadByPrimaryKey(10);
//        /// string theDate = entity.str.HireDate;
//        /// </code>
//        /// </example>
//        [BrowsableAttribute(false)]
//        public esStrings str
//        {
//            get
//            {
//                if (esstrings == null)
//                {
//                    esstrings = new esStrings(this);
//                }
//                return esstrings;
//            }
//        }

//        [Serializable]
//        sealed public class esStrings
//        {
//            public esStrings(esReferInPatient entity)
//            {
//                this.entity = entity;
//            }
//            public System.String RegistrationNo
//            {
//                get
//                {
//                    System.String data = entity.RegistrationNo;
//                    return (data == null) ? String.Empty : Convert.ToString(data);
//                }

//                set
//                {
//                    if (value == null || value.Length == 0) entity.RegistrationNo = null;
//                    else entity.RegistrationNo = Convert.ToString(value);
//                }
//            }
//            public System.String SequenceNo
//            {
//                get
//                {
//                    System.Int32? data = entity.SequenceNo;
//                    return (data == null) ? String.Empty : Convert.ToString(data);
//                }

//                set
//                {
//                    if (value == null || value.Length == 0) entity.SequenceNo = null;
//                    else entity.SequenceNo = Convert.ToInt32(value);
//                }
//            }
//            public System.String ReferDateTime
//            {
//                get
//                {
//                    System.DateTime? data = entity.ReferDateTime;
//                    return (data == null) ? String.Empty : Convert.ToString(data);
//                }

//                set
//                {
//                    if (value == null || value.Length == 0) entity.ReferDateTime = null;
//                    else entity.ReferDateTime = Convert.ToDateTime(value);
//                }
//            }
//            public System.String FromParamedicID
//            {
//                get
//                {
//                    System.String data = entity.FromParamedicID;
//                    return (data == null) ? String.Empty : Convert.ToString(data);
//                }

//                set
//                {
//                    if (value == null || value.Length == 0) entity.FromParamedicID = null;
//                    else entity.FromParamedicID = Convert.ToString(value);
//                }
//            }
//            public System.String ToParamedicID
//            {
//                get
//                {
//                    System.String data = entity.ToParamedicID;
//                    return (data == null) ? String.Empty : Convert.ToString(data);
//                }

//                set
//                {
//                    if (value == null || value.Length == 0) entity.ToParamedicID = null;
//                    else entity.ToParamedicID = Convert.ToString(value);
//                }
//            }
//            public System.String ActionExamTreatment
//            {
//                get
//                {
//                    System.String data = entity.ActionExamTreatment;
//                    return (data == null) ? String.Empty : Convert.ToString(data);
//                }

//                set
//                {
//                    if (value == null || value.Length == 0) entity.ActionExamTreatment = null;
//                    else entity.ActionExamTreatment = Convert.ToString(value);
//                }
//            }
//            public System.String Notes
//            {
//                get
//                {
//                    System.String data = entity.Notes;
//                    return (data == null) ? String.Empty : Convert.ToString(data);
//                }

//                set
//                {
//                    if (value == null || value.Length == 0) entity.Notes = null;
//                    else entity.Notes = Convert.ToString(value);
//                }
//            }
//            public System.String Answer
//            {
//                get
//                {
//                    System.String data = entity.Answer;
//                    return (data == null) ? String.Empty : Convert.ToString(data);
//                }

//                set
//                {
//                    if (value == null || value.Length == 0) entity.Answer = null;
//                    else entity.Answer = Convert.ToString(value);
//                }
//            }
//            public System.String LastUpdateDateTime
//            {
//                get
//                {
//                    System.DateTime? data = entity.LastUpdateDateTime;
//                    return (data == null) ? String.Empty : Convert.ToString(data);
//                }

//                set
//                {
//                    if (value == null || value.Length == 0) entity.LastUpdateDateTime = null;
//                    else entity.LastUpdateDateTime = Convert.ToDateTime(value);
//                }
//            }
//            public System.String LastUpdateByUserID
//            {
//                get
//                {
//                    System.String data = entity.LastUpdateByUserID;
//                    return (data == null) ? String.Empty : Convert.ToString(data);
//                }

//                set
//                {
//                    if (value == null || value.Length == 0) entity.LastUpdateByUserID = null;
//                    else entity.LastUpdateByUserID = Convert.ToString(value);
//                }
//            }
//            private esReferInPatient entity;
//        }
//        #endregion

//        #region Query Logic
//        protected void InitQuery(esReferInPatientQuery query)
//        {
//            query.OnLoadDelegate = this.OnQueryLoaded;
//            query.es2.Connection = ((IEntity)this).Connection;
//        }

//        [System.Diagnostics.DebuggerNonUserCode]
//        protected bool OnQueryLoaded(DataTable table)
//        {
//            bool dataFound = this.PopulateEntity(table);

//            if (this.RowCount > 1)
//            {
//                throw new Exception("esReferInPatient can only hold one record of data");
//            }

//            return dataFound;
//        }
//        #endregion

//        [NonSerialized]
//        private esStrings esstrings;
//    }


//    public partial class ReferInPatient : esReferInPatient
//    {
//    }

//    [Serializable]
//    abstract public class esReferInPatientQuery : esDynamicQuery
//    {

//        override protected IMetadata Meta
//        {
//            get
//            {
//                return ReferInPatientMetadata.Meta();
//            }
//        }

//        public esQueryItem RegistrationNo
//        {
//            get
//            {
//                return new esQueryItem(this, ReferInPatientMetadata.ColumnNames.RegistrationNo, esSystemType.String);
//            }
//        }

//        public esQueryItem SequenceNo
//        {
//            get
//            {
//                return new esQueryItem(this, ReferInPatientMetadata.ColumnNames.SequenceNo, esSystemType.Int32);
//            }
//        }

//        public esQueryItem ReferDateTime
//        {
//            get
//            {
//                return new esQueryItem(this, ReferInPatientMetadata.ColumnNames.ReferDateTime, esSystemType.DateTime);
//            }
//        }

//        public esQueryItem FromParamedicID
//        {
//            get
//            {
//                return new esQueryItem(this, ReferInPatientMetadata.ColumnNames.FromParamedicID, esSystemType.String);
//            }
//        }

//        public esQueryItem ToParamedicID
//        {
//            get
//            {
//                return new esQueryItem(this, ReferInPatientMetadata.ColumnNames.ToParamedicID, esSystemType.String);
//            }
//        }

//        public esQueryItem ActionExamTreatment
//        {
//            get
//            {
//                return new esQueryItem(this, ReferInPatientMetadata.ColumnNames.ActionExamTreatment, esSystemType.String);
//            }
//        }

//        public esQueryItem Notes
//        {
//            get
//            {
//                return new esQueryItem(this, ReferInPatientMetadata.ColumnNames.Notes, esSystemType.String);
//            }
//        }

//        public esQueryItem Answer
//        {
//            get
//            {
//                return new esQueryItem(this, ReferInPatientMetadata.ColumnNames.Answer, esSystemType.String);
//            }
//        }

//        public esQueryItem LastUpdateDateTime
//        {
//            get
//            {
//                return new esQueryItem(this, ReferInPatientMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
//            }
//        }

//        public esQueryItem LastUpdateByUserID
//        {
//            get
//            {
//                return new esQueryItem(this, ReferInPatientMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
//            }
//        }

//    }

//    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
//    [Serializable]
//    [XmlType("ReferInPatientCollection")]
//    public partial class ReferInPatientCollection : esReferInPatientCollection, IEnumerable<ReferInPatient>
//    {
//        public ReferInPatientCollection()
//        {

//        }

//        public static implicit operator List<ReferInPatient>(ReferInPatientCollection coll)
//        {
//            List<ReferInPatient> list = new List<ReferInPatient>();

//            foreach (ReferInPatient emp in coll)
//            {
//                list.Add(emp);
//            }

//            return list;
//        }

//        #region Housekeeping methods
//        override protected IMetadata Meta
//        {
//            get
//            {
//                return ReferInPatientMetadata.Meta();
//            }
//        }

//        override protected esDynamicQuery GetDynamicQuery()
//        {
//            if (this.query == null)
//            {
//                this.query = new ReferInPatientQuery();
//                this.InitQuery(query);
//            }
//            return this.query;
//        }

//        override protected esEntity CreateEntityForCollection(DataRow row)
//        {
//            return new ReferInPatient(row);
//        }

//        override protected esEntity CreateEntity()
//        {
//            return new ReferInPatient();
//        }

//        #endregion

//        [BrowsableAttribute(false)]
//        public ReferInPatientQuery Query
//        {
//            get
//            {
//                if (this.query == null)
//                {
//                    this.query = new ReferInPatientQuery();
//                    base.InitQuery(this.query);
//                }

//                return this.query;
//            }
//        }

//        /// <summary>
//        /// Useful for building up conditional queries.
//        /// In most cases, before loading an entity or collection,
//        /// you should instantiate a new one. This method was added
//        /// to handle specialized circumstances, and should not be
//        /// used as a substitute for that.
//        /// </summary>
//        /// <remarks>
//        /// This just sets obj.Query to null/Nothing.
//        /// In most cases, you will 'new' your object before
//        /// loading it, rather than calling this method.
//        /// It only affects obj.Query.Load(), so is not useful
//        /// when Joins are involved, or for many other situations.
//        /// Because it clears out any obj.Query.Where clauses,
//        /// it can be useful for building conditional queries on the fly.
//        /// <code>
//        /// public bool ReQuery(string lastName, string firstName)
//        /// {
//        ///     this.QueryReset();
//        ///     
//        ///     if(!String.IsNullOrEmpty(lastName))
//        ///     {
//        ///         this.Query.Where(
//        ///             this.Query.LastName == lastName);
//        ///     }
//        ///     if(!String.IsNullOrEmpty(firstName))
//        ///     {
//        ///         this.Query.Where(
//        ///             this.Query.FirstName == firstName);
//        ///     }
//        ///     
//        ///     return this.Query.Load();
//        /// }
//        /// </code>
//        /// <code lang="vbnet">
//        /// Public Function ReQuery(ByVal lastName As String, _
//        ///     ByVal firstName As String) As Boolean
//        /// 
//        ///     Me.QueryReset()
//        /// 
//        ///     If Not [String].IsNullOrEmpty(lastName) Then
//        ///         Me.Query.Where(Me.Query.LastName = lastName)
//        ///     End If
//        ///     If Not [String].IsNullOrEmpty(firstName) Then
//        ///         Me.Query.Where(Me.Query.FirstName = firstName)
//        ///     End If
//        /// 
//        ///     Return Me.Query.Load()
//        /// End Function
//        /// </code>
//        /// </remarks>
//        public void QueryReset()
//        {
//            this.query = null;
//        }

//        /// <summary>
//        /// Used to custom load a Join query.
//        /// Returns true if at least one record was loaded.
//        /// </summary>
//        /// <remarks>
//        /// Provides support for InnerJoin, LeftJoin,
//        /// RightJoin, and FullJoin. You must provide an alias
//        /// for each query when instantiating them.
//        /// <code>
//        /// EmployeeCollection collection = new EmployeeCollection();
//        /// 
//        /// EmployeeQuery emp = new EmployeeQuery("eq");
//        /// CustomerQuery cust = new CustomerQuery("cq");
//        /// 
//        /// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName);
//        /// emp.LeftJoin(cust).On(emp.EmployeeID == cust.StaffAssigned);
//        /// 
//        /// collection.Load(emp);
//        /// </code>
//        /// <code lang="vbnet">
//        /// Dim collection As New EmployeeCollection()
//        /// 
//        /// Dim emp As New EmployeeQuery("eq")
//        /// Dim cust As New CustomerQuery("cq")
//        /// 
//        /// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName)
//        /// emp.LeftJoin(cust).On(emp.EmployeeID = cust.StaffAssigned)
//        /// 
//        /// collection.Load(emp)
//        /// </code>
//        /// </remarks>
//        /// <param name="query">The query object instance name.</param>
//        /// <returns>True if at least one record was loaded.</returns>
//        public bool Load(ReferInPatientQuery query)
//        {
//            this.query = query;
//            base.InitQuery(this.query);
//            return this.Query.Load();
//        }

//        /// <summary>
//        /// Adds a new entity to the collection.
//        /// Always calls AddNew() on the entity, in case it is overridden.
//        /// </summary>
//        public ReferInPatient AddNew()
//        {
//            ReferInPatient entity = base.AddNewEntity() as ReferInPatient;

//            return entity;
//        }
//        public ReferInPatient FindByPrimaryKey(String registrationNo, Int32 sequenceNo)
//        {
//            return base.FindByPrimaryKey(registrationNo, sequenceNo) as ReferInPatient;
//        }

//        #region IEnumerable< ReferInPatient> Members

//        IEnumerator<ReferInPatient> IEnumerable<ReferInPatient>.GetEnumerator()
//        {
//            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
//            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

//            while (iterator.MoveNext())
//            {
//                yield return iterator.Current as ReferInPatient;
//            }
//        }

//        #endregion

//        private ReferInPatientQuery query;
//    }


//    /// <summary>
//    /// Encapsulates the 'ReferInPatient' table
//    /// </summary>
//    [System.Diagnostics.DebuggerDisplay("ReferInPatient ({RegistrationNo, SequenceNo})")]
//    [Serializable]
//    public partial class ReferInPatient : esReferInPatient
//    {
//        public ReferInPatient()
//        {
//        }

//        public ReferInPatient(DataRow row)
//            : base(row)
//        {
//        }

//        #region Housekeeping methods
//        override protected IMetadata Meta
//        {
//            get
//            {
//                return ReferInPatientMetadata.Meta();
//            }
//        }

//        override protected esReferInPatientQuery GetDynamicQuery()
//        {
//            if (this.query == null)
//            {
//                this.query = new ReferInPatientQuery();
//                this.InitQuery(query);
//            }
//            return this.query;
//        }
//        #endregion

//        [BrowsableAttribute(false)]
//        public ReferInPatientQuery Query
//        {
//            get
//            {
//                if (this.query == null)
//                {
//                    this.query = new ReferInPatientQuery();
//                    base.InitQuery(this.query);
//                }

//                return this.query;
//            }
//        }

//        /// <summary>
//        /// Useful for building up conditional queries.
//        /// In most cases, before loading an entity or collection,
//        /// you should instantiate a new one. This method was added
//        /// to handle specialized circumstances, and should not be
//        /// used as a substitute for that.
//        /// </summary>
//        /// <remarks>
//        /// This just sets obj.Query to null/Nothing.
//        /// In most cases, you will 'new' your object before
//        /// loading it, rather than calling this method.
//        /// It only affects obj.Query.Load(), so is not useful
//        /// when Joins are involved, or for many other situations.
//        /// Because it clears out any obj.Query.Where clauses,
//        /// it can be useful for building conditional queries on the fly.
//        /// <code>
//        /// public bool ReQuery(string lastName, string firstName)
//        /// {
//        ///     this.QueryReset();
//        ///     
//        ///     if(!String.IsNullOrEmpty(lastName))
//        ///     {
//        ///         this.Query.Where(
//        ///             this.Query.LastName == lastName);
//        ///     }
//        ///     if(!String.IsNullOrEmpty(firstName))
//        ///     {
//        ///         this.Query.Where(
//        ///             this.Query.FirstName == firstName);
//        ///     }
//        ///     
//        ///     return this.Query.Load();
//        /// }
//        /// </code>
//        /// <code lang="vbnet">
//        /// Public Function ReQuery(ByVal lastName As String, _
//        ///     ByVal firstName As String) As Boolean
//        /// 
//        ///     Me.QueryReset()
//        /// 
//        ///     If Not [String].IsNullOrEmpty(lastName) Then
//        ///         Me.Query.Where(Me.Query.LastName = lastName)
//        ///     End If
//        ///     If Not [String].IsNullOrEmpty(firstName) Then
//        ///         Me.Query.Where(Me.Query.FirstName = firstName)
//        ///     End If
//        /// 
//        ///     Return Me.Query.Load()
//        /// End Function
//        /// </code>
//        /// </remarks>
//        public void QueryReset()
//        {
//            this.query = null;
//        }

//        /// <summary>
//        /// Used to custom load a Join query.
//        /// Returns true if at least one row is loaded.
//        /// For an entity, an exception will be thrown
//        /// if more than one row is loaded.
//        /// </summary>
//        /// <remarks>
//        /// Provides support for InnerJoin, LeftJoin,
//        /// RightJoin, and FullJoin. You must provide an alias
//        /// for each query when instantiating them.
//        /// <code>
//        /// EmployeeCollection collection = new EmployeeCollection();
//        /// 
//        /// EmployeeQuery emp = new EmployeeQuery("eq");
//        /// CustomerQuery cust = new CustomerQuery("cq");
//        /// 
//        /// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName);
//        /// emp.LeftJoin(cust).On(emp.EmployeeID == cust.StaffAssigned);
//        /// 
//        /// collection.Load(emp);
//        /// </code>
//        /// <code lang="vbnet">
//        /// Dim collection As New EmployeeCollection()
//        /// 
//        /// Dim emp As New EmployeeQuery("eq")
//        /// Dim cust As New CustomerQuery("cq")
//        /// 
//        /// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName)
//        /// emp.LeftJoin(cust).On(emp.EmployeeID = cust.StaffAssigned)
//        /// 
//        /// collection.Load(emp)
//        /// </code>
//        /// </remarks>
//        /// <param name="query">The query object instance name.</param>
//        /// <returns>True if at least one record was loaded.</returns>
//        public bool Load(ReferInPatientQuery query)
//        {
//            this.query = query;
//            base.InitQuery(this.query);
//            return this.Query.Load();
//        }

//        private ReferInPatientQuery query;
//    }

//    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
//    [Serializable]
//    public partial class ReferInPatientQuery : esReferInPatientQuery
//    {
//        public ReferInPatientQuery()
//        {

//        }

//        public ReferInPatientQuery(string joinAlias)
//        {
//            this.es.JoinAlias = joinAlias;
//        }

//        override protected string GetQueryName()
//        {
//            return "ReferInPatientQuery";
//        }
//    }

//    [Serializable]
//    public partial class ReferInPatientMetadata : esMetadata, IMetadata
//    {
//        #region Protected Constructor
//        protected ReferInPatientMetadata()
//        {
//            _columns = new esColumnMetadataCollection();
//            esColumnMetadata c;

//            c = new esColumnMetadata(ReferInPatientMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
//            c.PropertyName = ReferInPatientMetadata.PropertyNames.RegistrationNo;
//            c.IsInPrimaryKey = true;
//            c.CharacterMaxLength = 20;
//            _columns.Add(c);

//            c = new esColumnMetadata(ReferInPatientMetadata.ColumnNames.SequenceNo, 1, typeof(System.Int32), esSystemType.Int32);
//            c.PropertyName = ReferInPatientMetadata.PropertyNames.SequenceNo;
//            c.IsInPrimaryKey = true;
//            c.NumericPrecision = 10;
//            _columns.Add(c);

//            c = new esColumnMetadata(ReferInPatientMetadata.ColumnNames.ReferDateTime, 2, typeof(System.DateTime), esSystemType.DateTime);
//            c.PropertyName = ReferInPatientMetadata.PropertyNames.ReferDateTime;
//            c.IsNullable = true;
//            _columns.Add(c);

//            c = new esColumnMetadata(ReferInPatientMetadata.ColumnNames.FromParamedicID, 3, typeof(System.String), esSystemType.String);
//            c.PropertyName = ReferInPatientMetadata.PropertyNames.FromParamedicID;
//            c.CharacterMaxLength = 10;
//            _columns.Add(c);

//            c = new esColumnMetadata(ReferInPatientMetadata.ColumnNames.ToParamedicID, 4, typeof(System.String), esSystemType.String);
//            c.PropertyName = ReferInPatientMetadata.PropertyNames.ToParamedicID;
//            c.CharacterMaxLength = 10;
//            _columns.Add(c);

//            c = new esColumnMetadata(ReferInPatientMetadata.ColumnNames.ActionExamTreatment, 5, typeof(System.String), esSystemType.String);
//            c.PropertyName = ReferInPatientMetadata.PropertyNames.ActionExamTreatment;
//            c.CharacterMaxLength = 1000;
//            _columns.Add(c);

//            c = new esColumnMetadata(ReferInPatientMetadata.ColumnNames.Notes, 6, typeof(System.String), esSystemType.String);
//            c.PropertyName = ReferInPatientMetadata.PropertyNames.Notes;
//            c.CharacterMaxLength = 500;
//            c.IsNullable = true;
//            _columns.Add(c);

//            c = new esColumnMetadata(ReferInPatientMetadata.ColumnNames.Answer, 7, typeof(System.String), esSystemType.String);
//            c.PropertyName = ReferInPatientMetadata.PropertyNames.Answer;
//            c.CharacterMaxLength = 1000;
//            c.IsNullable = true;
//            _columns.Add(c);

//            c = new esColumnMetadata(ReferInPatientMetadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
//            c.PropertyName = ReferInPatientMetadata.PropertyNames.LastUpdateDateTime;
//            c.IsNullable = true;
//            _columns.Add(c);

//            c = new esColumnMetadata(ReferInPatientMetadata.ColumnNames.LastUpdateByUserID, 9, typeof(System.String), esSystemType.String);
//            c.PropertyName = ReferInPatientMetadata.PropertyNames.LastUpdateByUserID;
//            c.CharacterMaxLength = 15;
//            c.IsNullable = true;
//            _columns.Add(c);


//        }
//        #endregion

//        static public ReferInPatientMetadata Meta()
//        {
//            return meta;
//        }

//        public Guid DataID
//        {
//            get { return base._dataID; }
//        }

//        public bool MultiProviderMode
//        {
//            get { return false; }
//        }

//        public esColumnMetadataCollection Columns
//        {
//            get { return base._columns; }
//        }

//        #region ColumnNames
//        public class ColumnNames
//        {
//            public const string RegistrationNo = "RegistrationNo";
//            public const string SequenceNo = "SequenceNo";
//            public const string ReferDateTime = "ReferDateTime";
//            public const string FromParamedicID = "FromParamedicID";
//            public const string ToParamedicID = "ToParamedicID";
//            public const string ActionExamTreatment = "ActionExamTreatment";
//            public const string Notes = "Notes";
//            public const string Answer = "Answer";
//            public const string LastUpdateDateTime = "LastUpdateDateTime";
//            public const string LastUpdateByUserID = "LastUpdateByUserID";
//        }
//        #endregion

//        #region PropertyNames
//        public class PropertyNames
//        {
//            public const string RegistrationNo = "RegistrationNo";
//            public const string SequenceNo = "SequenceNo";
//            public const string ReferDateTime = "ReferDateTime";
//            public const string FromParamedicID = "FromParamedicID";
//            public const string ToParamedicID = "ToParamedicID";
//            public const string ActionExamTreatment = "ActionExamTreatment";
//            public const string Notes = "Notes";
//            public const string Answer = "Answer";
//            public const string LastUpdateDateTime = "LastUpdateDateTime";
//            public const string LastUpdateByUserID = "LastUpdateByUserID";
//        }
//        #endregion

//        public esProviderSpecificMetadata GetProviderMetadata(string mapName)
//        {
//            MapToMeta mapMethod = mapDelegates[mapName];

//            if (mapMethod != null)
//                return mapMethod(mapName);
//            else
//                return null;
//        }

//        #region MAP esDefault

//        static private int RegisterDelegateesDefault()
//        {
//            // This is only executed once per the life of the application
//            lock (typeof(ReferInPatientMetadata))
//            {
//                if (ReferInPatientMetadata.mapDelegates == null)
//                {
//                    ReferInPatientMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
//                }

//                if (ReferInPatientMetadata.meta == null)
//                {
//                    ReferInPatientMetadata.meta = new ReferInPatientMetadata();
//                }

//                MapToMeta mapMethod = new MapToMeta(meta.esDefault);
//                mapDelegates.Add("esDefault", mapMethod);
//                mapMethod("esDefault");
//            }
//            return 0;
//        }

//        private esProviderSpecificMetadata esDefault(string mapName)
//        {
//            if (!_providerMetadataMaps.ContainsKey(mapName))
//            {
//                esProviderSpecificMetadata meta = new esProviderSpecificMetadata();

//                meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
//                meta.AddTypeMap("SequenceNo", new esTypeMap("int", "System.Int32"));
//                meta.AddTypeMap("ReferDateTime", new esTypeMap("datetime", "System.DateTime"));
//                meta.AddTypeMap("FromParamedicID", new esTypeMap("varchar", "System.String"));
//                meta.AddTypeMap("ToParamedicID", new esTypeMap("varchar", "System.String"));
//                meta.AddTypeMap("ActionExamTreatment", new esTypeMap("varchar", "System.String"));
//                meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
//                meta.AddTypeMap("Answer", new esTypeMap("varchar", "System.String"));
//                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
//                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


//                meta.Source = "ReferInPatient";
//                meta.Destination = "ReferInPatient";
//                meta.spInsert = "proc_ReferInPatientInsert";
//                meta.spUpdate = "proc_ReferInPatientUpdate";
//                meta.spDelete = "proc_ReferInPatientDelete";
//                meta.spLoadAll = "proc_ReferInPatientLoadAll";
//                meta.spLoadByPrimaryKey = "proc_ReferInPatientLoadByPrimaryKey";

//                this._providerMetadataMaps["esDefault"] = meta;
//            }

//            return this._providerMetadataMaps["esDefault"];
//        }

//        #endregion

//        static private ReferInPatientMetadata meta;
//        static protected Dictionary<string, MapToMeta> mapDelegates;
//        static private int _esDefault = RegisterDelegateesDefault();
//    }

//}
