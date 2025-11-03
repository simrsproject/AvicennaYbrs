/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 4/3/2017 9:54:07 AM
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
    abstract public class esPpiDiseaseFactorsCollection : esEntityCollectionWAuditLog
    {
        public esPpiDiseaseFactorsCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "PpiDiseaseFactorsCollection";
        }

        #region Query Logic
        protected void InitQuery(esPpiDiseaseFactorsQuery query)
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
            this.InitQuery(query as esPpiDiseaseFactorsQuery);
        }
        #endregion

        virtual public PpiDiseaseFactors DetachEntity(PpiDiseaseFactors entity)
        {
            return base.DetachEntity(entity) as PpiDiseaseFactors;
        }

        virtual public PpiDiseaseFactors AttachEntity(PpiDiseaseFactors entity)
        {
            return base.AttachEntity(entity) as PpiDiseaseFactors;
        }

        virtual public void Combine(PpiDiseaseFactorsCollection collection)
        {
            base.Combine(collection);
        }

        new public PpiDiseaseFactors this[int index]
        {
            get
            {
                return base[index] as PpiDiseaseFactors;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(PpiDiseaseFactors);
        }
    }

    [Serializable]
    abstract public class esPpiDiseaseFactors : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esPpiDiseaseFactorsQuery GetDynamicQuery()
        {
            return null;
        }

        public esPpiDiseaseFactors()
        {
        }

        public esPpiDiseaseFactors(DataRow row)
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
            esPpiDiseaseFactorsQuery query = this.GetDynamicQuery();
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
                        case "SRDiseaseFactorsHbsAg": this.str.SRDiseaseFactorsHbsAg = (string)value; break;
                        case "SRDiseaseFactorsAntiHcv": this.str.SRDiseaseFactorsAntiHcv = (string)value; break;
                        case "SRDiseaseFactorsAntiHiv": this.str.SRDiseaseFactorsAntiHiv = (string)value; break;
                        case "OtherDiseaseFactors": this.str.OtherDiseaseFactors = (string)value; break;
                        case "Leukocyte": this.str.Leukocyte = (string)value; break;
                        case "Led": this.str.Led = (string)value; break;
                        case "Gds": this.str.Gds = (string)value; break;
                        case "RadiologyResult": this.str.RadiologyResult = (string)value; break;
                        case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;
                        case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "CreatedDateTime":

                            if (value == null || value is System.DateTime)
                                this.CreatedDateTime = (System.DateTime?)value;
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
        /// Maps to PpiDiseaseFactors.RegistrationNo
        /// </summary>
        virtual public System.String RegistrationNo
        {
            get
            {
                return base.GetSystemString(PpiDiseaseFactorsMetadata.ColumnNames.RegistrationNo);
            }

            set
            {
                base.SetSystemString(PpiDiseaseFactorsMetadata.ColumnNames.RegistrationNo, value);
            }
        }
        /// <summary>
        /// Maps to PpiDiseaseFactors.SRDiseaseFactorsHbsAg
        /// </summary>
        virtual public System.String SRDiseaseFactorsHbsAg
        {
            get
            {
                return base.GetSystemString(PpiDiseaseFactorsMetadata.ColumnNames.SRDiseaseFactorsHbsAg);
            }

            set
            {
                base.SetSystemString(PpiDiseaseFactorsMetadata.ColumnNames.SRDiseaseFactorsHbsAg, value);
            }
        }
        /// <summary>
        /// Maps to PpiDiseaseFactors.SRDiseaseFactorsAntiHcv
        /// </summary>
        virtual public System.String SRDiseaseFactorsAntiHcv
        {
            get
            {
                return base.GetSystemString(PpiDiseaseFactorsMetadata.ColumnNames.SRDiseaseFactorsAntiHcv);
            }

            set
            {
                base.SetSystemString(PpiDiseaseFactorsMetadata.ColumnNames.SRDiseaseFactorsAntiHcv, value);
            }
        }
        /// <summary>
        /// Maps to PpiDiseaseFactors.SRDiseaseFactorsAntiHiv
        /// </summary>
        virtual public System.String SRDiseaseFactorsAntiHiv
        {
            get
            {
                return base.GetSystemString(PpiDiseaseFactorsMetadata.ColumnNames.SRDiseaseFactorsAntiHiv);
            }

            set
            {
                base.SetSystemString(PpiDiseaseFactorsMetadata.ColumnNames.SRDiseaseFactorsAntiHiv, value);
            }
        }
        /// <summary>
        /// Maps to PpiDiseaseFactors.OtherDiseaseFactors
        /// </summary>
        virtual public System.String OtherDiseaseFactors
        {
            get
            {
                return base.GetSystemString(PpiDiseaseFactorsMetadata.ColumnNames.OtherDiseaseFactors);
            }

            set
            {
                base.SetSystemString(PpiDiseaseFactorsMetadata.ColumnNames.OtherDiseaseFactors, value);
            }
        }
        /// <summary>
        /// Maps to PpiDiseaseFactors.Leukocyte
        /// </summary>
        virtual public System.String Leukocyte
        {
            get
            {
                return base.GetSystemString(PpiDiseaseFactorsMetadata.ColumnNames.Leukocyte);
            }

            set
            {
                base.SetSystemString(PpiDiseaseFactorsMetadata.ColumnNames.Leukocyte, value);
            }
        }
        /// <summary>
        /// Maps to PpiDiseaseFactors.Led
        /// </summary>
        virtual public System.String Led
        {
            get
            {
                return base.GetSystemString(PpiDiseaseFactorsMetadata.ColumnNames.Led);
            }

            set
            {
                base.SetSystemString(PpiDiseaseFactorsMetadata.ColumnNames.Led, value);
            }
        }
        /// <summary>
        /// Maps to PpiDiseaseFactors.Gds
        /// </summary>
        virtual public System.String Gds
        {
            get
            {
                return base.GetSystemString(PpiDiseaseFactorsMetadata.ColumnNames.Gds);
            }

            set
            {
                base.SetSystemString(PpiDiseaseFactorsMetadata.ColumnNames.Gds, value);
            }
        }
        /// <summary>
        /// Maps to PpiDiseaseFactors.RadiologyResult
        /// </summary>
        virtual public System.String RadiologyResult
        {
            get
            {
                return base.GetSystemString(PpiDiseaseFactorsMetadata.ColumnNames.RadiologyResult);
            }

            set
            {
                base.SetSystemString(PpiDiseaseFactorsMetadata.ColumnNames.RadiologyResult, value);
            }
        }
        /// <summary>
        /// Maps to PpiDiseaseFactors.CreatedDateTime
        /// </summary>
        virtual public System.DateTime? CreatedDateTime
        {
            get
            {
                return base.GetSystemDateTime(PpiDiseaseFactorsMetadata.ColumnNames.CreatedDateTime);
            }

            set
            {
                base.SetSystemDateTime(PpiDiseaseFactorsMetadata.ColumnNames.CreatedDateTime, value);
            }
        }
        /// <summary>
        /// Maps to PpiDiseaseFactors.CreatedByUserID
        /// </summary>
        virtual public System.String CreatedByUserID
        {
            get
            {
                return base.GetSystemString(PpiDiseaseFactorsMetadata.ColumnNames.CreatedByUserID);
            }

            set
            {
                base.SetSystemString(PpiDiseaseFactorsMetadata.ColumnNames.CreatedByUserID, value);
            }
        }
        /// <summary>
        /// Maps to PpiDiseaseFactors.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(PpiDiseaseFactorsMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(PpiDiseaseFactorsMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to PpiDiseaseFactors.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(PpiDiseaseFactorsMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(PpiDiseaseFactorsMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esPpiDiseaseFactors entity)
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
            public System.String SRDiseaseFactorsHbsAg
            {
                get
                {
                    System.String data = entity.SRDiseaseFactorsHbsAg;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRDiseaseFactorsHbsAg = null;
                    else entity.SRDiseaseFactorsHbsAg = Convert.ToString(value);
                }
            }
            public System.String SRDiseaseFactorsAntiHcv
            {
                get
                {
                    System.String data = entity.SRDiseaseFactorsAntiHcv;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRDiseaseFactorsAntiHcv = null;
                    else entity.SRDiseaseFactorsAntiHcv = Convert.ToString(value);
                }
            }
            public System.String SRDiseaseFactorsAntiHiv
            {
                get
                {
                    System.String data = entity.SRDiseaseFactorsAntiHiv;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRDiseaseFactorsAntiHiv = null;
                    else entity.SRDiseaseFactorsAntiHiv = Convert.ToString(value);
                }
            }
            public System.String OtherDiseaseFactors
            {
                get
                {
                    System.String data = entity.OtherDiseaseFactors;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.OtherDiseaseFactors = null;
                    else entity.OtherDiseaseFactors = Convert.ToString(value);
                }
            }
            public System.String Leukocyte
            {
                get
                {
                    System.String data = entity.Leukocyte;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Leukocyte = null;
                    else entity.Leukocyte = Convert.ToString(value);
                }
            }
            public System.String Led
            {
                get
                {
                    System.String data = entity.Led;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Led = null;
                    else entity.Led = Convert.ToString(value);
                }
            }
            public System.String Gds
            {
                get
                {
                    System.String data = entity.Gds;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Gds = null;
                    else entity.Gds = Convert.ToString(value);
                }
            }
            public System.String RadiologyResult
            {
                get
                {
                    System.String data = entity.RadiologyResult;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RadiologyResult = null;
                    else entity.RadiologyResult = Convert.ToString(value);
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
            private esPpiDiseaseFactors entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esPpiDiseaseFactorsQuery query)
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
                throw new Exception("esPpiDiseaseFactors can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class PpiDiseaseFactors : esPpiDiseaseFactors
    {
    }

    [Serializable]
    abstract public class esPpiDiseaseFactorsQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return PpiDiseaseFactorsMetadata.Meta();
            }
        }

        public esQueryItem RegistrationNo
        {
            get
            {
                return new esQueryItem(this, PpiDiseaseFactorsMetadata.ColumnNames.RegistrationNo, esSystemType.String);
            }
        }

        public esQueryItem SRDiseaseFactorsHbsAg
        {
            get
            {
                return new esQueryItem(this, PpiDiseaseFactorsMetadata.ColumnNames.SRDiseaseFactorsHbsAg, esSystemType.String);
            }
        }

        public esQueryItem SRDiseaseFactorsAntiHcv
        {
            get
            {
                return new esQueryItem(this, PpiDiseaseFactorsMetadata.ColumnNames.SRDiseaseFactorsAntiHcv, esSystemType.String);
            }
        }

        public esQueryItem SRDiseaseFactorsAntiHiv
        {
            get
            {
                return new esQueryItem(this, PpiDiseaseFactorsMetadata.ColumnNames.SRDiseaseFactorsAntiHiv, esSystemType.String);
            }
        }

        public esQueryItem OtherDiseaseFactors
        {
            get
            {
                return new esQueryItem(this, PpiDiseaseFactorsMetadata.ColumnNames.OtherDiseaseFactors, esSystemType.String);
            }
        }

        public esQueryItem Leukocyte
        {
            get
            {
                return new esQueryItem(this, PpiDiseaseFactorsMetadata.ColumnNames.Leukocyte, esSystemType.String);
            }
        }

        public esQueryItem Led
        {
            get
            {
                return new esQueryItem(this, PpiDiseaseFactorsMetadata.ColumnNames.Led, esSystemType.String);
            }
        }

        public esQueryItem Gds
        {
            get
            {
                return new esQueryItem(this, PpiDiseaseFactorsMetadata.ColumnNames.Gds, esSystemType.String);
            }
        }

        public esQueryItem RadiologyResult
        {
            get
            {
                return new esQueryItem(this, PpiDiseaseFactorsMetadata.ColumnNames.RadiologyResult, esSystemType.String);
            }
        }

        public esQueryItem CreatedDateTime
        {
            get
            {
                return new esQueryItem(this, PpiDiseaseFactorsMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem CreatedByUserID
        {
            get
            {
                return new esQueryItem(this, PpiDiseaseFactorsMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, PpiDiseaseFactorsMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, PpiDiseaseFactorsMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("PpiDiseaseFactorsCollection")]
    public partial class PpiDiseaseFactorsCollection : esPpiDiseaseFactorsCollection, IEnumerable<PpiDiseaseFactors>
    {
        public PpiDiseaseFactorsCollection()
        {

        }

        public static implicit operator List<PpiDiseaseFactors>(PpiDiseaseFactorsCollection coll)
        {
            List<PpiDiseaseFactors> list = new List<PpiDiseaseFactors>();

            foreach (PpiDiseaseFactors emp in coll)
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
                return PpiDiseaseFactorsMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PpiDiseaseFactorsQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new PpiDiseaseFactors(row);
        }

        override protected esEntity CreateEntity()
        {
            return new PpiDiseaseFactors();
        }

        #endregion

        [BrowsableAttribute(false)]
        public PpiDiseaseFactorsQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PpiDiseaseFactorsQuery();
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
        public bool Load(PpiDiseaseFactorsQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public PpiDiseaseFactors AddNew()
        {
            PpiDiseaseFactors entity = base.AddNewEntity() as PpiDiseaseFactors;

            return entity;
        }
        public PpiDiseaseFactors FindByPrimaryKey(String registrationNo)
        {
            return base.FindByPrimaryKey(registrationNo) as PpiDiseaseFactors;
        }

        #region IEnumerable< PpiDiseaseFactors> Members

        IEnumerator<PpiDiseaseFactors> IEnumerable<PpiDiseaseFactors>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as PpiDiseaseFactors;
            }
        }

        #endregion

        private PpiDiseaseFactorsQuery query;
    }


    /// <summary>
    /// Encapsulates the 'PpiDiseaseFactors' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("PpiDiseaseFactors ({RegistrationNo})")]
    [Serializable]
    public partial class PpiDiseaseFactors : esPpiDiseaseFactors
    {
        public PpiDiseaseFactors()
        {
        }

        public PpiDiseaseFactors(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return PpiDiseaseFactorsMetadata.Meta();
            }
        }

        override protected esPpiDiseaseFactorsQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PpiDiseaseFactorsQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public PpiDiseaseFactorsQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PpiDiseaseFactorsQuery();
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
        public bool Load(PpiDiseaseFactorsQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private PpiDiseaseFactorsQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class PpiDiseaseFactorsQuery : esPpiDiseaseFactorsQuery
    {
        public PpiDiseaseFactorsQuery()
        {

        }

        public PpiDiseaseFactorsQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "PpiDiseaseFactorsQuery";
        }
    }

    [Serializable]
    public partial class PpiDiseaseFactorsMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected PpiDiseaseFactorsMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(PpiDiseaseFactorsMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = PpiDiseaseFactorsMetadata.PropertyNames.RegistrationNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(PpiDiseaseFactorsMetadata.ColumnNames.SRDiseaseFactorsHbsAg, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = PpiDiseaseFactorsMetadata.PropertyNames.SRDiseaseFactorsHbsAg;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiDiseaseFactorsMetadata.ColumnNames.SRDiseaseFactorsAntiHcv, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = PpiDiseaseFactorsMetadata.PropertyNames.SRDiseaseFactorsAntiHcv;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiDiseaseFactorsMetadata.ColumnNames.SRDiseaseFactorsAntiHiv, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = PpiDiseaseFactorsMetadata.PropertyNames.SRDiseaseFactorsAntiHiv;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiDiseaseFactorsMetadata.ColumnNames.OtherDiseaseFactors, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = PpiDiseaseFactorsMetadata.PropertyNames.OtherDiseaseFactors;
            c.CharacterMaxLength = 500;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiDiseaseFactorsMetadata.ColumnNames.Leukocyte, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = PpiDiseaseFactorsMetadata.PropertyNames.Leukocyte;
            c.CharacterMaxLength = 500;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiDiseaseFactorsMetadata.ColumnNames.Led, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = PpiDiseaseFactorsMetadata.PropertyNames.Led;
            c.CharacterMaxLength = 500;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiDiseaseFactorsMetadata.ColumnNames.Gds, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = PpiDiseaseFactorsMetadata.PropertyNames.Gds;
            c.CharacterMaxLength = 500;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiDiseaseFactorsMetadata.ColumnNames.RadiologyResult, 8, typeof(System.String), esSystemType.String);
            c.PropertyName = PpiDiseaseFactorsMetadata.PropertyNames.RadiologyResult;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiDiseaseFactorsMetadata.ColumnNames.CreatedDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PpiDiseaseFactorsMetadata.PropertyNames.CreatedDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiDiseaseFactorsMetadata.ColumnNames.CreatedByUserID, 10, typeof(System.String), esSystemType.String);
            c.PropertyName = PpiDiseaseFactorsMetadata.PropertyNames.CreatedByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiDiseaseFactorsMetadata.ColumnNames.LastUpdateDateTime, 11, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PpiDiseaseFactorsMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiDiseaseFactorsMetadata.ColumnNames.LastUpdateByUserID, 12, typeof(System.String), esSystemType.String);
            c.PropertyName = PpiDiseaseFactorsMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public PpiDiseaseFactorsMetadata Meta()
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
            public const string SRDiseaseFactorsHbsAg = "SRDiseaseFactorsHbsAg";
            public const string SRDiseaseFactorsAntiHcv = "SRDiseaseFactorsAntiHcv";
            public const string SRDiseaseFactorsAntiHiv = "SRDiseaseFactorsAntiHiv";
            public const string OtherDiseaseFactors = "OtherDiseaseFactors";
            public const string Leukocyte = "Leukocyte";
            public const string Led = "Led";
            public const string Gds = "Gds";
            public const string RadiologyResult = "RadiologyResult";
            public const string CreatedDateTime = "CreatedDateTime";
            public const string CreatedByUserID = "CreatedByUserID";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string RegistrationNo = "RegistrationNo";
            public const string SRDiseaseFactorsHbsAg = "SRDiseaseFactorsHbsAg";
            public const string SRDiseaseFactorsAntiHcv = "SRDiseaseFactorsAntiHcv";
            public const string SRDiseaseFactorsAntiHiv = "SRDiseaseFactorsAntiHiv";
            public const string OtherDiseaseFactors = "OtherDiseaseFactors";
            public const string Leukocyte = "Leukocyte";
            public const string Led = "Led";
            public const string Gds = "Gds";
            public const string RadiologyResult = "RadiologyResult";
            public const string CreatedDateTime = "CreatedDateTime";
            public const string CreatedByUserID = "CreatedByUserID";
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
            lock (typeof(PpiDiseaseFactorsMetadata))
            {
                if (PpiDiseaseFactorsMetadata.mapDelegates == null)
                {
                    PpiDiseaseFactorsMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (PpiDiseaseFactorsMetadata.meta == null)
                {
                    PpiDiseaseFactorsMetadata.meta = new PpiDiseaseFactorsMetadata();
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
                meta.AddTypeMap("SRDiseaseFactorsHbsAg", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRDiseaseFactorsAntiHcv", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRDiseaseFactorsAntiHiv", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("OtherDiseaseFactors", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Leukocyte", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Led", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Gds", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("RadiologyResult", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "PpiDiseaseFactors";
                meta.Destination = "PpiDiseaseFactors";
                meta.spInsert = "proc_PpiDiseaseFactorsInsert";
                meta.spUpdate = "proc_PpiDiseaseFactorsUpdate";
                meta.spDelete = "proc_PpiDiseaseFactorsDelete";
                meta.spLoadAll = "proc_PpiDiseaseFactorsLoadAll";
                meta.spLoadByPrimaryKey = "proc_PpiDiseaseFactorsLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private PpiDiseaseFactorsMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
