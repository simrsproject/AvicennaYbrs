/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 6/2/2024 9:09:46 PM
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
    abstract public class esPatientAllergyCollection : esEntityCollectionWAuditLog
    {
        public esPatientAllergyCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "PatientAllergyCollection";
        }

        #region Query Logic
        protected void InitQuery(esPatientAllergyQuery query)
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
            this.InitQuery(query as esPatientAllergyQuery);
        }
        #endregion

        virtual public PatientAllergy DetachEntity(PatientAllergy entity)
        {
            return base.DetachEntity(entity) as PatientAllergy;
        }

        virtual public PatientAllergy AttachEntity(PatientAllergy entity)
        {
            return base.AttachEntity(entity) as PatientAllergy;
        }

        virtual public void Combine(PatientAllergyCollection collection)
        {
            base.Combine(collection);
        }

        new public PatientAllergy this[int index]
        {
            get
            {
                return base[index] as PatientAllergy;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(PatientAllergy);
        }
    }

    [Serializable]
    abstract public class esPatientAllergy : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esPatientAllergyQuery GetDynamicQuery()
        {
            return null;
        }

        public esPatientAllergy()
        {
        }

        public esPatientAllergy(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String allergyGroup, String allergen, String patientID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(allergyGroup, allergen, patientID);
            else
                return LoadByPrimaryKeyStoredProcedure(allergyGroup, allergen, patientID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String allergyGroup, String allergen, String patientID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(allergyGroup, allergen, patientID);
            else
                return LoadByPrimaryKeyStoredProcedure(allergyGroup, allergen, patientID);
        }

        private bool LoadByPrimaryKeyDynamic(String allergyGroup, String allergen, String patientID)
        {
            esPatientAllergyQuery query = this.GetDynamicQuery();
            query.Where(query.AllergyGroup == allergyGroup, query.Allergen == allergen, query.PatientID == patientID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String allergyGroup, String allergen, String patientID)
        {
            esParameters parms = new esParameters();
            parms.Add("AllergyGroup", allergyGroup);
            parms.Add("Allergen", allergen);
            parms.Add("PatientID", patientID);
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
                        case "AllergyGroup": this.str.AllergyGroup = (string)value; break;
                        case "Allergen": this.str.Allergen = (string)value; break;
                        case "AllergenName": this.str.AllergenName = (string)value; break;
                        case "SRAnaphylaxis": this.str.SRAnaphylaxis = (string)value; break;
                        case "Anaphylaxis": this.str.Anaphylaxis = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "PatientID": this.str.PatientID = (string)value; break;
                        case "DescAndReaction": this.str.DescAndReaction = (string)value; break;
                        case "AllergenDate": this.str.AllergenDate = (string)value; break;
                        case "SRAllergyClinicalStatus": this.str.SRAllergyClinicalStatus = (string)value; break;
                        case "SRAllergyVerificationStatus": this.str.SRAllergyVerificationStatus = (string)value; break;
                        case "SRAllergyCategory": this.str.SRAllergyCategory = (string)value; break;
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
                        case "AllergenDate":

                            if (value == null || value is System.DateTime)
                                this.AllergenDate = (System.DateTime?)value;
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
        /// Maps to PatientAllergy.AllergyGroup
        /// </summary>
        virtual public System.String AllergyGroup
        {
            get
            {
                return base.GetSystemString(PatientAllergyMetadata.ColumnNames.AllergyGroup);
            }

            set
            {
                base.SetSystemString(PatientAllergyMetadata.ColumnNames.AllergyGroup, value);
            }
        }
        /// <summary>
        /// Maps to PatientAllergy.Allergen
        /// </summary>
        virtual public System.String Allergen
        {
            get
            {
                return base.GetSystemString(PatientAllergyMetadata.ColumnNames.Allergen);
            }

            set
            {
                base.SetSystemString(PatientAllergyMetadata.ColumnNames.Allergen, value);
            }
        }
        /// <summary>
        /// Maps to PatientAllergy.AllergenName
        /// </summary>
        virtual public System.String AllergenName
        {
            get
            {
                return base.GetSystemString(PatientAllergyMetadata.ColumnNames.AllergenName);
            }

            set
            {
                base.SetSystemString(PatientAllergyMetadata.ColumnNames.AllergenName, value);
            }
        }
        /// <summary>
        /// Maps to PatientAllergy.SRAnaphylaxis
        /// </summary>
        virtual public System.String SRAnaphylaxis
        {
            get
            {
                return base.GetSystemString(PatientAllergyMetadata.ColumnNames.SRAnaphylaxis);
            }

            set
            {
                base.SetSystemString(PatientAllergyMetadata.ColumnNames.SRAnaphylaxis, value);
            }
        }
        /// <summary>
        /// Maps to PatientAllergy.Anaphylaxis
        /// </summary>
        virtual public System.String Anaphylaxis
        {
            get
            {
                return base.GetSystemString(PatientAllergyMetadata.ColumnNames.Anaphylaxis);
            }

            set
            {
                base.SetSystemString(PatientAllergyMetadata.ColumnNames.Anaphylaxis, value);
            }
        }
        /// <summary>
        /// Maps to PatientAllergy.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(PatientAllergyMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(PatientAllergyMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to PatientAllergy.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(PatientAllergyMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(PatientAllergyMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to PatientAllergy.PatientID
        /// </summary>
        virtual public System.String PatientID
        {
            get
            {
                return base.GetSystemString(PatientAllergyMetadata.ColumnNames.PatientID);
            }

            set
            {
                base.SetSystemString(PatientAllergyMetadata.ColumnNames.PatientID, value);
            }
        }
        /// <summary>
        /// Maps to PatientAllergy.DescAndReaction
        /// </summary>
        virtual public System.String DescAndReaction
        {
            get
            {
                return base.GetSystemString(PatientAllergyMetadata.ColumnNames.DescAndReaction);
            }

            set
            {
                base.SetSystemString(PatientAllergyMetadata.ColumnNames.DescAndReaction, value);
            }
        }
        /// <summary>
        /// Maps to PatientAllergy.AllergenDate
        /// </summary>
        virtual public System.DateTime? AllergenDate
        {
            get
            {
                return base.GetSystemDateTime(PatientAllergyMetadata.ColumnNames.AllergenDate);
            }

            set
            {
                base.SetSystemDateTime(PatientAllergyMetadata.ColumnNames.AllergenDate, value);
            }
        }
        /// <summary>
        /// Maps to PatientAllergy.SRAllergyClinicalStatus
        /// </summary>
        virtual public System.String SRAllergyClinicalStatus
        {
            get
            {
                return base.GetSystemString(PatientAllergyMetadata.ColumnNames.SRAllergyClinicalStatus);
            }

            set
            {
                base.SetSystemString(PatientAllergyMetadata.ColumnNames.SRAllergyClinicalStatus, value);
            }
        }
        /// <summary>
        /// Maps to PatientAllergy.SRAllergyVerificationStatus
        /// </summary>
        virtual public System.String SRAllergyVerificationStatus
        {
            get
            {
                return base.GetSystemString(PatientAllergyMetadata.ColumnNames.SRAllergyVerificationStatus);
            }

            set
            {
                base.SetSystemString(PatientAllergyMetadata.ColumnNames.SRAllergyVerificationStatus, value);
            }
        }
        /// <summary>
        /// Maps to PatientAllergy.SRAllergyCategory
        /// </summary>
        virtual public System.String SRAllergyCategory
        {
            get
            {
                return base.GetSystemString(PatientAllergyMetadata.ColumnNames.SRAllergyCategory);
            }

            set
            {
                base.SetSystemString(PatientAllergyMetadata.ColumnNames.SRAllergyCategory, value);
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
            public esStrings(esPatientAllergy entity)
            {
                this.entity = entity;
            }
            public System.String AllergyGroup
            {
                get
                {
                    System.String data = entity.AllergyGroup;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.AllergyGroup = null;
                    else entity.AllergyGroup = Convert.ToString(value);
                }
            }
            public System.String Allergen
            {
                get
                {
                    System.String data = entity.Allergen;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Allergen = null;
                    else entity.Allergen = Convert.ToString(value);
                }
            }
            public System.String AllergenName
            {
                get
                {
                    System.String data = entity.AllergenName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.AllergenName = null;
                    else entity.AllergenName = Convert.ToString(value);
                }
            }
            public System.String SRAnaphylaxis
            {
                get
                {
                    System.String data = entity.SRAnaphylaxis;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRAnaphylaxis = null;
                    else entity.SRAnaphylaxis = Convert.ToString(value);
                }
            }
            public System.String Anaphylaxis
            {
                get
                {
                    System.String data = entity.Anaphylaxis;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Anaphylaxis = null;
                    else entity.Anaphylaxis = Convert.ToString(value);
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
            public System.String PatientID
            {
                get
                {
                    System.String data = entity.PatientID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PatientID = null;
                    else entity.PatientID = Convert.ToString(value);
                }
            }
            public System.String DescAndReaction
            {
                get
                {
                    System.String data = entity.DescAndReaction;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DescAndReaction = null;
                    else entity.DescAndReaction = Convert.ToString(value);
                }
            }
            public System.String AllergenDate
            {
                get
                {
                    System.DateTime? data = entity.AllergenDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.AllergenDate = null;
                    else entity.AllergenDate = Convert.ToDateTime(value);
                }
            }
            public System.String SRAllergyClinicalStatus
            {
                get
                {
                    System.String data = entity.SRAllergyClinicalStatus;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRAllergyClinicalStatus = null;
                    else entity.SRAllergyClinicalStatus = Convert.ToString(value);
                }
            }
            public System.String SRAllergyVerificationStatus
            {
                get
                {
                    System.String data = entity.SRAllergyVerificationStatus;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRAllergyVerificationStatus = null;
                    else entity.SRAllergyVerificationStatus = Convert.ToString(value);
                }
            }
            public System.String SRAllergyCategory
            {
                get
                {
                    System.String data = entity.SRAllergyCategory;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRAllergyCategory = null;
                    else entity.SRAllergyCategory = Convert.ToString(value);
                }
            }
            private esPatientAllergy entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esPatientAllergyQuery query)
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
                throw new Exception("esPatientAllergy can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class PatientAllergy : esPatientAllergy
    {
    }

    [Serializable]
    abstract public class esPatientAllergyQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return PatientAllergyMetadata.Meta();
            }
        }

        public esQueryItem AllergyGroup
        {
            get
            {
                return new esQueryItem(this, PatientAllergyMetadata.ColumnNames.AllergyGroup, esSystemType.String);
            }
        }

        public esQueryItem Allergen
        {
            get
            {
                return new esQueryItem(this, PatientAllergyMetadata.ColumnNames.Allergen, esSystemType.String);
            }
        }

        public esQueryItem AllergenName
        {
            get
            {
                return new esQueryItem(this, PatientAllergyMetadata.ColumnNames.AllergenName, esSystemType.String);
            }
        }

        public esQueryItem SRAnaphylaxis
        {
            get
            {
                return new esQueryItem(this, PatientAllergyMetadata.ColumnNames.SRAnaphylaxis, esSystemType.String);
            }
        }

        public esQueryItem Anaphylaxis
        {
            get
            {
                return new esQueryItem(this, PatientAllergyMetadata.ColumnNames.Anaphylaxis, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, PatientAllergyMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, PatientAllergyMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem PatientID
        {
            get
            {
                return new esQueryItem(this, PatientAllergyMetadata.ColumnNames.PatientID, esSystemType.String);
            }
        }

        public esQueryItem DescAndReaction
        {
            get
            {
                return new esQueryItem(this, PatientAllergyMetadata.ColumnNames.DescAndReaction, esSystemType.String);
            }
        }

        public esQueryItem AllergenDate
        {
            get
            {
                return new esQueryItem(this, PatientAllergyMetadata.ColumnNames.AllergenDate, esSystemType.DateTime);
            }
        }

        public esQueryItem SRAllergyClinicalStatus
        {
            get
            {
                return new esQueryItem(this, PatientAllergyMetadata.ColumnNames.SRAllergyClinicalStatus, esSystemType.String);
            }
        }

        public esQueryItem SRAllergyVerificationStatus
        {
            get
            {
                return new esQueryItem(this, PatientAllergyMetadata.ColumnNames.SRAllergyVerificationStatus, esSystemType.String);
            }
        }

        public esQueryItem SRAllergyCategory
        {
            get
            {
                return new esQueryItem(this, PatientAllergyMetadata.ColumnNames.SRAllergyCategory, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("PatientAllergyCollection")]
    public partial class PatientAllergyCollection : esPatientAllergyCollection, IEnumerable<PatientAllergy>
    {
        public PatientAllergyCollection()
        {

        }

        public static implicit operator List<PatientAllergy>(PatientAllergyCollection coll)
        {
            List<PatientAllergy> list = new List<PatientAllergy>();

            foreach (PatientAllergy emp in coll)
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
                return PatientAllergyMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PatientAllergyQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new PatientAllergy(row);
        }

        override protected esEntity CreateEntity()
        {
            return new PatientAllergy();
        }

        #endregion

        [BrowsableAttribute(false)]
        public PatientAllergyQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PatientAllergyQuery();
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
        public bool Load(PatientAllergyQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public PatientAllergy AddNew()
        {
            PatientAllergy entity = base.AddNewEntity() as PatientAllergy;

            return entity;
        }
        public PatientAllergy FindByPrimaryKey(String allergyGroup, String allergen, String patientID)
        {
            return base.FindByPrimaryKey(allergyGroup, allergen, patientID) as PatientAllergy;
        }

        #region IEnumerable< PatientAllergy> Members

        IEnumerator<PatientAllergy> IEnumerable<PatientAllergy>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as PatientAllergy;
            }
        }

        #endregion

        private PatientAllergyQuery query;
    }


    /// <summary>
    /// Encapsulates the 'PatientAllergy' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("PatientAllergy ({AllergyGroup, Allergen, PatientID})")]
    [Serializable]
    public partial class PatientAllergy : esPatientAllergy
    {
        public PatientAllergy()
        {
        }

        public PatientAllergy(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return PatientAllergyMetadata.Meta();
            }
        }

        override protected esPatientAllergyQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PatientAllergyQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public PatientAllergyQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PatientAllergyQuery();
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
        public bool Load(PatientAllergyQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private PatientAllergyQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class PatientAllergyQuery : esPatientAllergyQuery
    {
        public PatientAllergyQuery()
        {

        }

        public PatientAllergyQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "PatientAllergyQuery";
        }
    }

    [Serializable]
    public partial class PatientAllergyMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected PatientAllergyMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(PatientAllergyMetadata.ColumnNames.AllergyGroup, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientAllergyMetadata.PropertyNames.AllergyGroup;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 30;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(PatientAllergyMetadata.ColumnNames.Allergen, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientAllergyMetadata.PropertyNames.Allergen;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(PatientAllergyMetadata.ColumnNames.AllergenName, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientAllergyMetadata.PropertyNames.AllergenName;
            c.CharacterMaxLength = 4000;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(PatientAllergyMetadata.ColumnNames.SRAnaphylaxis, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientAllergyMetadata.PropertyNames.SRAnaphylaxis;
            c.CharacterMaxLength = 20;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(PatientAllergyMetadata.ColumnNames.Anaphylaxis, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientAllergyMetadata.PropertyNames.Anaphylaxis;
            c.CharacterMaxLength = 100;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(PatientAllergyMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PatientAllergyMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientAllergyMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientAllergyMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 40;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientAllergyMetadata.ColumnNames.PatientID, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientAllergyMetadata.PropertyNames.PatientID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 15;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(PatientAllergyMetadata.ColumnNames.DescAndReaction, 8, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientAllergyMetadata.PropertyNames.DescAndReaction;
            c.CharacterMaxLength = 4000;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(PatientAllergyMetadata.ColumnNames.AllergenDate, 9, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PatientAllergyMetadata.PropertyNames.AllergenDate;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientAllergyMetadata.ColumnNames.SRAllergyClinicalStatus, 10, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientAllergyMetadata.PropertyNames.SRAllergyClinicalStatus;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientAllergyMetadata.ColumnNames.SRAllergyVerificationStatus, 11, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientAllergyMetadata.PropertyNames.SRAllergyVerificationStatus;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientAllergyMetadata.ColumnNames.SRAllergyCategory, 13, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientAllergyMetadata.PropertyNames.SRAllergyCategory;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public PatientAllergyMetadata Meta()
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
            public const string AllergyGroup = "AllergyGroup";
            public const string Allergen = "Allergen";
            public const string AllergenName = "AllergenName";
            public const string SRAnaphylaxis = "SRAnaphylaxis";
            public const string Anaphylaxis = "Anaphylaxis";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string PatientID = "PatientID";
            public const string DescAndReaction = "DescAndReaction";
            public const string AllergenDate = "AllergenDate";
            public const string SRAllergyClinicalStatus = "SRAllergyClinicalStatus";
            public const string SRAllergyVerificationStatus = "SRAllergyVerificationStatus";
            public const string SRAllergyCategory = "SRAllergyCategory";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string AllergyGroup = "AllergyGroup";
            public const string Allergen = "Allergen";
            public const string AllergenName = "AllergenName";
            public const string SRAnaphylaxis = "SRAnaphylaxis";
            public const string Anaphylaxis = "Anaphylaxis";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string PatientID = "PatientID";
            public const string DescAndReaction = "DescAndReaction";
            public const string AllergenDate = "AllergenDate";
            public const string SRAllergyClinicalStatus = "SRAllergyClinicalStatus";
            public const string SRAllergyVerificationStatus = "SRAllergyVerificationStatus";
            public const string SRAllergyCategory = "SRAllergyCategory";
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
            lock (typeof(PatientAllergyMetadata))
            {
                if (PatientAllergyMetadata.mapDelegates == null)
                {
                    PatientAllergyMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (PatientAllergyMetadata.meta == null)
                {
                    PatientAllergyMetadata.meta = new PatientAllergyMetadata();
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

                meta.AddTypeMap("AllergyGroup", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Allergen", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("AllergenName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRAnaphylaxis", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Anaphylaxis", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("PatientID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("DescAndReaction", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("AllergenDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("SRAllergyClinicalStatus", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRAllergyVerificationStatus", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRAllergyCategory", new esTypeMap("varchar", "System.String"));


                meta.Source = "PatientAllergy";
                meta.Destination = "PatientAllergy";
                meta.spInsert = "proc_PatientAllergyInsert";
                meta.spUpdate = "proc_PatientAllergyUpdate";
                meta.spDelete = "proc_PatientAllergyDelete";
                meta.spLoadAll = "proc_PatientAllergyLoadAll";
                meta.spLoadByPrimaryKey = "proc_PatientAllergyLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private PatientAllergyMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
