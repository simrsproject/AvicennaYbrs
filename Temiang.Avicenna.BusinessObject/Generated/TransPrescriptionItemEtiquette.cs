/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 1/11/2020 2:12:51 PM
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
    abstract public class esTransPrescriptionItemEtiquetteCollection : esEntityCollectionWAuditLog
    {
        public esTransPrescriptionItemEtiquetteCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "TransPrescriptionItemEtiquetteCollection";
        }

        #region Query Logic
        protected void InitQuery(esTransPrescriptionItemEtiquetteQuery query)
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
            this.InitQuery(query as esTransPrescriptionItemEtiquetteQuery);
        }
        #endregion

        virtual public TransPrescriptionItemEtiquette DetachEntity(TransPrescriptionItemEtiquette entity)
        {
            return base.DetachEntity(entity) as TransPrescriptionItemEtiquette;
        }

        virtual public TransPrescriptionItemEtiquette AttachEntity(TransPrescriptionItemEtiquette entity)
        {
            return base.AttachEntity(entity) as TransPrescriptionItemEtiquette;
        }

        virtual public void Combine(TransPrescriptionItemEtiquetteCollection collection)
        {
            base.Combine(collection);
        }

        new public TransPrescriptionItemEtiquette this[int index]
        {
            get
            {
                return base[index] as TransPrescriptionItemEtiquette;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(TransPrescriptionItemEtiquette);
        }
    }

    [Serializable]
    abstract public class esTransPrescriptionItemEtiquette : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esTransPrescriptionItemEtiquetteQuery GetDynamicQuery()
        {
            return null;
        }

        public esTransPrescriptionItemEtiquette()
        {
        }

        public esTransPrescriptionItemEtiquette(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String prescriptionNo, String sequenceNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(prescriptionNo, sequenceNo);
            else
                return LoadByPrimaryKeyStoredProcedure(prescriptionNo, sequenceNo);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String prescriptionNo, String sequenceNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(prescriptionNo, sequenceNo);
            else
                return LoadByPrimaryKeyStoredProcedure(prescriptionNo, sequenceNo);
        }

        private bool LoadByPrimaryKeyDynamic(String prescriptionNo, String sequenceNo)
        {
            esTransPrescriptionItemEtiquetteQuery query = this.GetDynamicQuery();
            query.Where(query.PrescriptionNo == prescriptionNo, query.SequenceNo == sequenceNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String prescriptionNo, String sequenceNo)
        {
            esParameters parms = new esParameters();
            parms.Add("PrescriptionNo", prescriptionNo);
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
                        case "PrescriptionNo": this.str.PrescriptionNo = (string)value; break;
                        case "SequenceNo": this.str.SequenceNo = (string)value; break;
                        case "ItemName": this.str.ItemName = (string)value; break;
                        case "ConsumeMethod": this.str.ConsumeMethod = (string)value; break;
                        case "Keeping": this.str.Keeping = (string)value; break;
                        case "SpecificInfo": this.str.SpecificInfo = (string)value; break;
                        case "ExpiredDate": this.str.ExpiredDate = (string)value; break;
                        case "IsDrugOutside": this.str.IsDrugOutside = (string)value; break;
                        case "CreateUserID": this.str.CreateUserID = (string)value; break;
                        case "CreateDateTime": this.str.CreateDateTime = (string)value; break;
                        case "LastUpdateUserID": this.str.LastUpdateUserID = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "NumberOfCopies": this.str.NumberOfCopies = (string)value; break;
                        case "BatchNumber": this.str.BatchNumber = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "ExpiredDate":

                            if (value == null || value is System.DateTime)
                                this.ExpiredDate = (System.DateTime?)value;
                            break;
                        case "IsDrugOutside":

                            if (value == null || value is System.Boolean)
                                this.IsDrugOutside = (System.Boolean?)value;
                            break;
                        case "CreateDateTime":

                            if (value == null || value is System.DateTime)
                                this.CreateDateTime = (System.DateTime?)value;
                            break;
                        case "LastUpdateDateTime":

                            if (value == null || value is System.DateTime)
                                this.LastUpdateDateTime = (System.DateTime?)value;
                            break;
                        case "NumberOfCopies":

                            if (value == null || value is System.Int32)
                                this.NumberOfCopies = (System.Int32?)value;
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
        /// Maps to TransPrescriptionItemEtiquette.PrescriptionNo
        /// </summary>
        virtual public System.String PrescriptionNo
        {
            get
            {
                return base.GetSystemString(TransPrescriptionItemEtiquetteMetadata.ColumnNames.PrescriptionNo);
            }

            set
            {
                base.SetSystemString(TransPrescriptionItemEtiquetteMetadata.ColumnNames.PrescriptionNo, value);
            }
        }
        /// <summary>
        /// Maps to TransPrescriptionItemEtiquette.SequenceNo
        /// </summary>
        virtual public System.String SequenceNo
        {
            get
            {
                return base.GetSystemString(TransPrescriptionItemEtiquetteMetadata.ColumnNames.SequenceNo);
            }

            set
            {
                base.SetSystemString(TransPrescriptionItemEtiquetteMetadata.ColumnNames.SequenceNo, value);
            }
        }
        /// <summary>
        /// Maps to TransPrescriptionItemEtiquette.ItemName
        /// </summary>
        virtual public System.String ItemName
        {
            get
            {
                return base.GetSystemString(TransPrescriptionItemEtiquetteMetadata.ColumnNames.ItemName);
            }

            set
            {
                base.SetSystemString(TransPrescriptionItemEtiquetteMetadata.ColumnNames.ItemName, value);
            }
        }
        /// <summary>
        /// Maps to TransPrescriptionItemEtiquette.ConsumeMethod
        /// </summary>
        virtual public System.String ConsumeMethod
        {
            get
            {
                return base.GetSystemString(TransPrescriptionItemEtiquetteMetadata.ColumnNames.ConsumeMethod);
            }

            set
            {
                base.SetSystemString(TransPrescriptionItemEtiquetteMetadata.ColumnNames.ConsumeMethod, value);
            }
        }
        /// <summary>
        /// Maps to TransPrescriptionItemEtiquette.Keeping
        /// </summary>
        virtual public System.String Keeping
        {
            get
            {
                return base.GetSystemString(TransPrescriptionItemEtiquetteMetadata.ColumnNames.Keeping);
            }

            set
            {
                base.SetSystemString(TransPrescriptionItemEtiquetteMetadata.ColumnNames.Keeping, value);
            }
        }
        /// <summary>
        /// Maps to TransPrescriptionItemEtiquette.SpecificInfo
        /// </summary>
        virtual public System.String SpecificInfo
        {
            get
            {
                return base.GetSystemString(TransPrescriptionItemEtiquetteMetadata.ColumnNames.SpecificInfo);
            }

            set
            {
                base.SetSystemString(TransPrescriptionItemEtiquetteMetadata.ColumnNames.SpecificInfo, value);
            }
        }
        /// <summary>
        /// Maps to TransPrescriptionItemEtiquette.ExpiredDate
        /// </summary>
        virtual public System.DateTime? ExpiredDate
        {
            get
            {
                return base.GetSystemDateTime(TransPrescriptionItemEtiquetteMetadata.ColumnNames.ExpiredDate);
            }

            set
            {
                base.SetSystemDateTime(TransPrescriptionItemEtiquetteMetadata.ColumnNames.ExpiredDate, value);
            }
        }
        /// <summary>
        /// Maps to TransPrescriptionItemEtiquette.IsDrugOutside
        /// </summary>
        virtual public System.Boolean? IsDrugOutside
        {
            get
            {
                return base.GetSystemBoolean(TransPrescriptionItemEtiquetteMetadata.ColumnNames.IsDrugOutside);
            }

            set
            {
                base.SetSystemBoolean(TransPrescriptionItemEtiquetteMetadata.ColumnNames.IsDrugOutside, value);
            }
        }
        /// <summary>
        /// Maps to TransPrescriptionItemEtiquette.CreateUserID
        /// </summary>
        virtual public System.String CreateUserID
        {
            get
            {
                return base.GetSystemString(TransPrescriptionItemEtiquetteMetadata.ColumnNames.CreateUserID);
            }

            set
            {
                base.SetSystemString(TransPrescriptionItemEtiquetteMetadata.ColumnNames.CreateUserID, value);
            }
        }
        /// <summary>
        /// Maps to TransPrescriptionItemEtiquette.CreateDateTime
        /// </summary>
        virtual public System.DateTime? CreateDateTime
        {
            get
            {
                return base.GetSystemDateTime(TransPrescriptionItemEtiquetteMetadata.ColumnNames.CreateDateTime);
            }

            set
            {
                base.SetSystemDateTime(TransPrescriptionItemEtiquetteMetadata.ColumnNames.CreateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to TransPrescriptionItemEtiquette.LastUpdateUserID
        /// </summary>
        virtual public System.String LastUpdateUserID
        {
            get
            {
                return base.GetSystemString(TransPrescriptionItemEtiquetteMetadata.ColumnNames.LastUpdateUserID);
            }

            set
            {
                base.SetSystemString(TransPrescriptionItemEtiquetteMetadata.ColumnNames.LastUpdateUserID, value);
            }
        }
        /// <summary>
        /// Maps to TransPrescriptionItemEtiquette.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(TransPrescriptionItemEtiquetteMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(TransPrescriptionItemEtiquetteMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to TransPrescriptionItemEtiquette.NumberOfCopies
        /// </summary>
        virtual public System.Int32? NumberOfCopies
        {
            get
            {
                return base.GetSystemInt32(TransPrescriptionItemEtiquetteMetadata.ColumnNames.NumberOfCopies);
            }

            set
            {
                base.SetSystemInt32(TransPrescriptionItemEtiquetteMetadata.ColumnNames.NumberOfCopies, value);
            }
        }
        /// <summary>
        /// Maps to TransPrescriptionItemEtiquette.BatchNumber
        /// </summary>
        virtual public System.String BatchNumber
        {
            get
            {
                return base.GetSystemString(TransPrescriptionItemEtiquetteMetadata.ColumnNames.BatchNumber);
            }

            set
            {
                base.SetSystemString(TransPrescriptionItemEtiquetteMetadata.ColumnNames.BatchNumber, value);
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
            public esStrings(esTransPrescriptionItemEtiquette entity)
            {
                this.entity = entity;
            }
            public System.String PrescriptionNo
            {
                get
                {
                    System.String data = entity.PrescriptionNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PrescriptionNo = null;
                    else entity.PrescriptionNo = Convert.ToString(value);
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
            public System.String ItemName
            {
                get
                {
                    System.String data = entity.ItemName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ItemName = null;
                    else entity.ItemName = Convert.ToString(value);
                }
            }
            public System.String ConsumeMethod
            {
                get
                {
                    System.String data = entity.ConsumeMethod;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ConsumeMethod = null;
                    else entity.ConsumeMethod = Convert.ToString(value);
                }
            }
            public System.String Keeping
            {
                get
                {
                    System.String data = entity.Keeping;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Keeping = null;
                    else entity.Keeping = Convert.ToString(value);
                }
            }
            public System.String SpecificInfo
            {
                get
                {
                    System.String data = entity.SpecificInfo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SpecificInfo = null;
                    else entity.SpecificInfo = Convert.ToString(value);
                }
            }
            public System.String ExpiredDate
            {
                get
                {
                    System.DateTime? data = entity.ExpiredDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ExpiredDate = null;
                    else entity.ExpiredDate = Convert.ToDateTime(value);
                }
            }
            public System.String IsDrugOutside
            {
                get
                {
                    System.Boolean? data = entity.IsDrugOutside;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsDrugOutside = null;
                    else entity.IsDrugOutside = Convert.ToBoolean(value);
                }
            }
            public System.String CreateUserID
            {
                get
                {
                    System.String data = entity.CreateUserID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CreateUserID = null;
                    else entity.CreateUserID = Convert.ToString(value);
                }
            }
            public System.String CreateDateTime
            {
                get
                {
                    System.DateTime? data = entity.CreateDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CreateDateTime = null;
                    else entity.CreateDateTime = Convert.ToDateTime(value);
                }
            }
            public System.String LastUpdateUserID
            {
                get
                {
                    System.String data = entity.LastUpdateUserID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.LastUpdateUserID = null;
                    else entity.LastUpdateUserID = Convert.ToString(value);
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
            public System.String NumberOfCopies
            {
                get
                {
                    System.Int32? data = entity.NumberOfCopies;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.NumberOfCopies = null;
                    else entity.NumberOfCopies = Convert.ToInt32(value);
                }
            }
            public System.String BatchNumber
            {
                get
                {
                    System.String data = entity.BatchNumber;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.BatchNumber = null;
                    else entity.BatchNumber = Convert.ToString(value);
                }
            }
            private esTransPrescriptionItemEtiquette entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esTransPrescriptionItemEtiquetteQuery query)
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
                throw new Exception("esTransPrescriptionItemEtiquette can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class TransPrescriptionItemEtiquette : esTransPrescriptionItemEtiquette
    {
    }

    [Serializable]
    abstract public class esTransPrescriptionItemEtiquetteQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return TransPrescriptionItemEtiquetteMetadata.Meta();
            }
        }

        public esQueryItem PrescriptionNo
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionItemEtiquetteMetadata.ColumnNames.PrescriptionNo, esSystemType.String);
            }
        }

        public esQueryItem SequenceNo
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionItemEtiquetteMetadata.ColumnNames.SequenceNo, esSystemType.String);
            }
        }

        public esQueryItem ItemName
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionItemEtiquetteMetadata.ColumnNames.ItemName, esSystemType.String);
            }
        }

        public esQueryItem ConsumeMethod
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionItemEtiquetteMetadata.ColumnNames.ConsumeMethod, esSystemType.String);
            }
        }

        public esQueryItem Keeping
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionItemEtiquetteMetadata.ColumnNames.Keeping, esSystemType.String);
            }
        }

        public esQueryItem SpecificInfo
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionItemEtiquetteMetadata.ColumnNames.SpecificInfo, esSystemType.String);
            }
        }

        public esQueryItem ExpiredDate
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionItemEtiquetteMetadata.ColumnNames.ExpiredDate, esSystemType.DateTime);
            }
        }

        public esQueryItem IsDrugOutside
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionItemEtiquetteMetadata.ColumnNames.IsDrugOutside, esSystemType.Boolean);
            }
        }

        public esQueryItem CreateUserID
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionItemEtiquetteMetadata.ColumnNames.CreateUserID, esSystemType.String);
            }
        }

        public esQueryItem CreateDateTime
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionItemEtiquetteMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateUserID
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionItemEtiquetteMetadata.ColumnNames.LastUpdateUserID, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionItemEtiquetteMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem NumberOfCopies
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionItemEtiquetteMetadata.ColumnNames.NumberOfCopies, esSystemType.Int32);
            }
        }

        public esQueryItem BatchNumber
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionItemEtiquetteMetadata.ColumnNames.BatchNumber, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("TransPrescriptionItemEtiquetteCollection")]
    public partial class TransPrescriptionItemEtiquetteCollection : esTransPrescriptionItemEtiquetteCollection, IEnumerable<TransPrescriptionItemEtiquette>
    {
        public TransPrescriptionItemEtiquetteCollection()
        {

        }

        public static implicit operator List<TransPrescriptionItemEtiquette>(TransPrescriptionItemEtiquetteCollection coll)
        {
            List<TransPrescriptionItemEtiquette> list = new List<TransPrescriptionItemEtiquette>();

            foreach (TransPrescriptionItemEtiquette emp in coll)
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
                return TransPrescriptionItemEtiquetteMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new TransPrescriptionItemEtiquetteQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new TransPrescriptionItemEtiquette(row);
        }

        override protected esEntity CreateEntity()
        {
            return new TransPrescriptionItemEtiquette();
        }

        #endregion

        [BrowsableAttribute(false)]
        public TransPrescriptionItemEtiquetteQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new TransPrescriptionItemEtiquetteQuery();
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
        public bool Load(TransPrescriptionItemEtiquetteQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public TransPrescriptionItemEtiquette AddNew()
        {
            TransPrescriptionItemEtiquette entity = base.AddNewEntity() as TransPrescriptionItemEtiquette;

            return entity;
        }
        public TransPrescriptionItemEtiquette FindByPrimaryKey(String prescriptionNo, String sequenceNo)
        {
            return base.FindByPrimaryKey(prescriptionNo, sequenceNo) as TransPrescriptionItemEtiquette;
        }

        #region IEnumerable< TransPrescriptionItemEtiquette> Members

        IEnumerator<TransPrescriptionItemEtiquette> IEnumerable<TransPrescriptionItemEtiquette>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as TransPrescriptionItemEtiquette;
            }
        }

        #endregion

        private TransPrescriptionItemEtiquetteQuery query;
    }


    /// <summary>
    /// Encapsulates the 'TransPrescriptionItemEtiquette' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("TransPrescriptionItemEtiquette ({PrescriptionNo, SequenceNo})")]
    [Serializable]
    public partial class TransPrescriptionItemEtiquette : esTransPrescriptionItemEtiquette
    {
        public TransPrescriptionItemEtiquette()
        {
        }

        public TransPrescriptionItemEtiquette(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return TransPrescriptionItemEtiquetteMetadata.Meta();
            }
        }

        override protected esTransPrescriptionItemEtiquetteQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new TransPrescriptionItemEtiquetteQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public TransPrescriptionItemEtiquetteQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new TransPrescriptionItemEtiquetteQuery();
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
        public bool Load(TransPrescriptionItemEtiquetteQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private TransPrescriptionItemEtiquetteQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class TransPrescriptionItemEtiquetteQuery : esTransPrescriptionItemEtiquetteQuery
    {
        public TransPrescriptionItemEtiquetteQuery()
        {

        }

        public TransPrescriptionItemEtiquetteQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "TransPrescriptionItemEtiquetteQuery";
        }
    }

    [Serializable]
    public partial class TransPrescriptionItemEtiquetteMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected TransPrescriptionItemEtiquetteMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(TransPrescriptionItemEtiquetteMetadata.ColumnNames.PrescriptionNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = TransPrescriptionItemEtiquetteMetadata.PropertyNames.PrescriptionNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionItemEtiquetteMetadata.ColumnNames.SequenceNo, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = TransPrescriptionItemEtiquetteMetadata.PropertyNames.SequenceNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 3;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionItemEtiquetteMetadata.ColumnNames.ItemName, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = TransPrescriptionItemEtiquetteMetadata.PropertyNames.ItemName;
            c.CharacterMaxLength = 255;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionItemEtiquetteMetadata.ColumnNames.ConsumeMethod, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = TransPrescriptionItemEtiquetteMetadata.PropertyNames.ConsumeMethod;
            c.CharacterMaxLength = 100;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionItemEtiquetteMetadata.ColumnNames.Keeping, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = TransPrescriptionItemEtiquetteMetadata.PropertyNames.Keeping;
            c.CharacterMaxLength = 100;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionItemEtiquetteMetadata.ColumnNames.SpecificInfo, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = TransPrescriptionItemEtiquetteMetadata.PropertyNames.SpecificInfo;
            c.CharacterMaxLength = 100;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionItemEtiquetteMetadata.ColumnNames.ExpiredDate, 6, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = TransPrescriptionItemEtiquetteMetadata.PropertyNames.ExpiredDate;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionItemEtiquetteMetadata.ColumnNames.IsDrugOutside, 7, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = TransPrescriptionItemEtiquetteMetadata.PropertyNames.IsDrugOutside;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionItemEtiquetteMetadata.ColumnNames.CreateUserID, 8, typeof(System.String), esSystemType.String);
            c.PropertyName = TransPrescriptionItemEtiquetteMetadata.PropertyNames.CreateUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionItemEtiquetteMetadata.ColumnNames.CreateDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = TransPrescriptionItemEtiquetteMetadata.PropertyNames.CreateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionItemEtiquetteMetadata.ColumnNames.LastUpdateUserID, 10, typeof(System.String), esSystemType.String);
            c.PropertyName = TransPrescriptionItemEtiquetteMetadata.PropertyNames.LastUpdateUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionItemEtiquetteMetadata.ColumnNames.LastUpdateDateTime, 11, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = TransPrescriptionItemEtiquetteMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionItemEtiquetteMetadata.ColumnNames.NumberOfCopies, 12, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = TransPrescriptionItemEtiquetteMetadata.PropertyNames.NumberOfCopies;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionItemEtiquetteMetadata.ColumnNames.BatchNumber, 13, typeof(System.String), esSystemType.String);
            c.PropertyName = TransPrescriptionItemEtiquetteMetadata.PropertyNames.BatchNumber;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public TransPrescriptionItemEtiquetteMetadata Meta()
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
            public const string PrescriptionNo = "PrescriptionNo";
            public const string SequenceNo = "SequenceNo";
            public const string ItemName = "ItemName";
            public const string ConsumeMethod = "ConsumeMethod";
            public const string Keeping = "Keeping";
            public const string SpecificInfo = "SpecificInfo";
            public const string ExpiredDate = "ExpiredDate";
            public const string IsDrugOutside = "IsDrugOutside";
            public const string CreateUserID = "CreateUserID";
            public const string CreateDateTime = "CreateDateTime";
            public const string LastUpdateUserID = "LastUpdateUserID";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string NumberOfCopies = "NumberOfCopies";
            public const string BatchNumber = "BatchNumber";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string PrescriptionNo = "PrescriptionNo";
            public const string SequenceNo = "SequenceNo";
            public const string ItemName = "ItemName";
            public const string ConsumeMethod = "ConsumeMethod";
            public const string Keeping = "Keeping";
            public const string SpecificInfo = "SpecificInfo";
            public const string ExpiredDate = "ExpiredDate";
            public const string IsDrugOutside = "IsDrugOutside";
            public const string CreateUserID = "CreateUserID";
            public const string CreateDateTime = "CreateDateTime";
            public const string LastUpdateUserID = "LastUpdateUserID";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string NumberOfCopies = "NumberOfCopies";
            public const string BatchNumber = "BatchNumber";
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
            lock (typeof(TransPrescriptionItemEtiquetteMetadata))
            {
                if (TransPrescriptionItemEtiquetteMetadata.mapDelegates == null)
                {
                    TransPrescriptionItemEtiquetteMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (TransPrescriptionItemEtiquetteMetadata.meta == null)
                {
                    TransPrescriptionItemEtiquetteMetadata.meta = new TransPrescriptionItemEtiquetteMetadata();
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

                meta.AddTypeMap("PrescriptionNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SequenceNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ItemName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ConsumeMethod", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Keeping", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SpecificInfo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ExpiredDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("IsDrugOutside", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("CreateUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("NumberOfCopies", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("BatchNumber", new esTypeMap("varchar", "System.String"));


                meta.Source = "TransPrescriptionItemEtiquette";
                meta.Destination = "TransPrescriptionItemEtiquette";
                meta.spInsert = "proc_TransPrescriptionItemEtiquetteInsert";
                meta.spUpdate = "proc_TransPrescriptionItemEtiquetteUpdate";
                meta.spDelete = "proc_TransPrescriptionItemEtiquetteDelete";
                meta.spLoadAll = "proc_TransPrescriptionItemEtiquetteLoadAll";
                meta.spLoadByPrimaryKey = "proc_TransPrescriptionItemEtiquetteLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private TransPrescriptionItemEtiquetteMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
