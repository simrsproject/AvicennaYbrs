/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 03/01/19 10:52:04 PM
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
    abstract public class esPatientEducationLineCollection : esEntityCollectionWAuditLog
    {
        public esPatientEducationLineCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "PatientEducationLineCollection";
        }

        #region Query Logic
        protected void InitQuery(esPatientEducationLineQuery query)
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
            this.InitQuery(query as esPatientEducationLineQuery);
        }
        #endregion

        virtual public PatientEducationLine DetachEntity(PatientEducationLine entity)
        {
            return base.DetachEntity(entity) as PatientEducationLine;
        }

        virtual public PatientEducationLine AttachEntity(PatientEducationLine entity)
        {
            return base.AttachEntity(entity) as PatientEducationLine;
        }

        virtual public void Combine(PatientEducationLineCollection collection)
        {
            base.Combine(collection);
        }

        new public PatientEducationLine this[int index]
        {
            get
            {
                return base[index] as PatientEducationLine;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(PatientEducationLine);
        }
    }

    [Serializable]
    abstract public class esPatientEducationLine : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esPatientEducationLineQuery GetDynamicQuery()
        {
            return null;
        }

        public esPatientEducationLine()
        {
        }

        public esPatientEducationLine(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String registrationNo, Int32 seqNo, String sRPatientEducation)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(registrationNo, seqNo, sRPatientEducation);
            else
                return LoadByPrimaryKeyStoredProcedure(registrationNo, seqNo, sRPatientEducation);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String registrationNo, Int32 seqNo, String sRPatientEducation)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(registrationNo, seqNo, sRPatientEducation);
            else
                return LoadByPrimaryKeyStoredProcedure(registrationNo, seqNo, sRPatientEducation);
        }

        private bool LoadByPrimaryKeyDynamic(String registrationNo, Int32 seqNo, String sRPatientEducation)
        {
            esPatientEducationLineQuery query = this.GetDynamicQuery();
            query.Where(query.RegistrationNo == registrationNo, query.SeqNo == seqNo, query.SRPatientEducation == sRPatientEducation);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String registrationNo, Int32 seqNo, String sRPatientEducation)
        {
            esParameters parms = new esParameters();
            parms.Add("RegistrationNo", registrationNo);
            parms.Add("SeqNo", seqNo);
            parms.Add("SRPatientEducation", sRPatientEducation);
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
                        case "SeqNo": this.str.SeqNo = (string)value; break;
                        case "SRPatientEducation": this.str.SRPatientEducation = (string)value; break;
                        case "EducationNotes": this.str.EducationNotes = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "SeqNo":

                            if (value == null || value is System.Int32)
                                this.SeqNo = (System.Int32?)value;
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
        /// Maps to PatientEducationLine.RegistrationNo
        /// </summary>
        virtual public System.String RegistrationNo
        {
            get
            {
                return base.GetSystemString(PatientEducationLineMetadata.ColumnNames.RegistrationNo);
            }

            set
            {
                base.SetSystemString(PatientEducationLineMetadata.ColumnNames.RegistrationNo, value);
            }
        }
        /// <summary>
        /// Maps to PatientEducationLine.SeqNo
        /// </summary>
        virtual public System.Int32? SeqNo
        {
            get
            {
                return base.GetSystemInt32(PatientEducationLineMetadata.ColumnNames.SeqNo);
            }

            set
            {
                base.SetSystemInt32(PatientEducationLineMetadata.ColumnNames.SeqNo, value);
            }
        }
        /// <summary>
        /// Maps to PatientEducationLine.SRPatientEducation
        /// </summary>
        virtual public System.String SRPatientEducation
        {
            get
            {
                return base.GetSystemString(PatientEducationLineMetadata.ColumnNames.SRPatientEducation);
            }

            set
            {
                base.SetSystemString(PatientEducationLineMetadata.ColumnNames.SRPatientEducation, value);
            }
        }
        /// <summary>
        /// Maps to PatientEducationLine.EducationNotes
        /// </summary>
        virtual public System.String EducationNotes
        {
            get
            {
                return base.GetSystemString(PatientEducationLineMetadata.ColumnNames.EducationNotes);
            }

            set
            {
                base.SetSystemString(PatientEducationLineMetadata.ColumnNames.EducationNotes, value);
            }
        }
        /// <summary>
        /// Maps to PatientEducationLine.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(PatientEducationLineMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(PatientEducationLineMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to PatientEducationLine.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(PatientEducationLineMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(PatientEducationLineMetadata.ColumnNames.LastUpdateDateTime, value);
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
            public esStrings(esPatientEducationLine entity)
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
            public System.String SeqNo
            {
                get
                {
                    System.Int32? data = entity.SeqNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SeqNo = null;
                    else entity.SeqNo = Convert.ToInt32(value);
                }
            }
            public System.String SRPatientEducation
            {
                get
                {
                    System.String data = entity.SRPatientEducation;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRPatientEducation = null;
                    else entity.SRPatientEducation = Convert.ToString(value);
                }
            }
            public System.String EducationNotes
            {
                get
                {
                    System.String data = entity.EducationNotes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.EducationNotes = null;
                    else entity.EducationNotes = Convert.ToString(value);
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
            private esPatientEducationLine entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esPatientEducationLineQuery query)
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
                throw new Exception("esPatientEducationLine can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class PatientEducationLine : esPatientEducationLine
    {
    }

    [Serializable]
    abstract public class esPatientEducationLineQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return PatientEducationLineMetadata.Meta();
            }
        }

        public esQueryItem RegistrationNo
        {
            get
            {
                return new esQueryItem(this, PatientEducationLineMetadata.ColumnNames.RegistrationNo, esSystemType.String);
            }
        }

        public esQueryItem SeqNo
        {
            get
            {
                return new esQueryItem(this, PatientEducationLineMetadata.ColumnNames.SeqNo, esSystemType.Int32);
            }
        }

        public esQueryItem SRPatientEducation
        {
            get
            {
                return new esQueryItem(this, PatientEducationLineMetadata.ColumnNames.SRPatientEducation, esSystemType.String);
            }
        }

        public esQueryItem EducationNotes
        {
            get
            {
                return new esQueryItem(this, PatientEducationLineMetadata.ColumnNames.EducationNotes, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, PatientEducationLineMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, PatientEducationLineMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("PatientEducationLineCollection")]
    public partial class PatientEducationLineCollection : esPatientEducationLineCollection, IEnumerable<PatientEducationLine>
    {
        public PatientEducationLineCollection()
        {

        }

        public static implicit operator List<PatientEducationLine>(PatientEducationLineCollection coll)
        {
            List<PatientEducationLine> list = new List<PatientEducationLine>();

            foreach (PatientEducationLine emp in coll)
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
                return PatientEducationLineMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PatientEducationLineQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new PatientEducationLine(row);
        }

        override protected esEntity CreateEntity()
        {
            return new PatientEducationLine();
        }

        #endregion

        [BrowsableAttribute(false)]
        public PatientEducationLineQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PatientEducationLineQuery();
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
        public bool Load(PatientEducationLineQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public PatientEducationLine AddNew()
        {
            PatientEducationLine entity = base.AddNewEntity() as PatientEducationLine;

            return entity;
        }
        public PatientEducationLine FindByPrimaryKey(String registrationNo, Int32 seqNo, String sRPatientEducation)
        {
            return base.FindByPrimaryKey(registrationNo, seqNo, sRPatientEducation) as PatientEducationLine;
        }

        #region IEnumerable< PatientEducationLine> Members

        IEnumerator<PatientEducationLine> IEnumerable<PatientEducationLine>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as PatientEducationLine;
            }
        }

        #endregion

        private PatientEducationLineQuery query;
    }


    /// <summary>
    /// Encapsulates the 'PatientEducationLine' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("PatientEducationLine ({RegistrationNo, SeqNo, SRPatientEducation})")]
    [Serializable]
    public partial class PatientEducationLine : esPatientEducationLine
    {
        public PatientEducationLine()
        {
        }

        public PatientEducationLine(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return PatientEducationLineMetadata.Meta();
            }
        }

        override protected esPatientEducationLineQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PatientEducationLineQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public PatientEducationLineQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PatientEducationLineQuery();
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
        public bool Load(PatientEducationLineQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private PatientEducationLineQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class PatientEducationLineQuery : esPatientEducationLineQuery
    {
        public PatientEducationLineQuery()
        {

        }

        public PatientEducationLineQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "PatientEducationLineQuery";
        }
    }

    [Serializable]
    public partial class PatientEducationLineMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected PatientEducationLineMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(PatientEducationLineMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientEducationLineMetadata.PropertyNames.RegistrationNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(PatientEducationLineMetadata.ColumnNames.SeqNo, 1, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = PatientEducationLineMetadata.PropertyNames.SeqNo;
            c.IsInPrimaryKey = true;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(PatientEducationLineMetadata.ColumnNames.SRPatientEducation, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientEducationLineMetadata.PropertyNames.SRPatientEducation;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 5;
            _columns.Add(c);

            c = new esColumnMetadata(PatientEducationLineMetadata.ColumnNames.EducationNotes, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientEducationLineMetadata.PropertyNames.EducationNotes;
            c.CharacterMaxLength = 400;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientEducationLineMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientEducationLineMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientEducationLineMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PatientEducationLineMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public PatientEducationLineMetadata Meta()
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
            public const string SeqNo = "SeqNo";
            public const string SRPatientEducation = "SRPatientEducation";
            public const string EducationNotes = "EducationNotes";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string RegistrationNo = "RegistrationNo";
            public const string SeqNo = "SeqNo";
            public const string SRPatientEducation = "SRPatientEducation";
            public const string EducationNotes = "EducationNotes";
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
            lock (typeof(PatientEducationLineMetadata))
            {
                if (PatientEducationLineMetadata.mapDelegates == null)
                {
                    PatientEducationLineMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (PatientEducationLineMetadata.meta == null)
                {
                    PatientEducationLineMetadata.meta = new PatientEducationLineMetadata();
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
                meta.AddTypeMap("SeqNo", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("SRPatientEducation", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("EducationNotes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));


                meta.Source = "PatientEducationLine";
                meta.Destination = "PatientEducationLine";
                meta.spInsert = "proc_PatientEducationLineInsert";
                meta.spUpdate = "proc_PatientEducationLineUpdate";
                meta.spDelete = "proc_PatientEducationLineDelete";
                meta.spLoadAll = "proc_PatientEducationLineLoadAll";
                meta.spLoadByPrimaryKey = "proc_PatientEducationLineLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private PatientEducationLineMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
