/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 3/2/2020 5:31:59 PM
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
    abstract public class esMedicalRecordFileBorrowedCollection : esEntityCollectionWAuditLog
    {
        public esMedicalRecordFileBorrowedCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "MedicalRecordFileBorrowedCollection";
        }

        #region Query Logic
        protected void InitQuery(esMedicalRecordFileBorrowedQuery query)
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
            this.InitQuery(query as esMedicalRecordFileBorrowedQuery);
        }
        #endregion

        virtual public MedicalRecordFileBorrowed DetachEntity(MedicalRecordFileBorrowed entity)
        {
            return base.DetachEntity(entity) as MedicalRecordFileBorrowed;
        }

        virtual public MedicalRecordFileBorrowed AttachEntity(MedicalRecordFileBorrowed entity)
        {
            return base.AttachEntity(entity) as MedicalRecordFileBorrowed;
        }

        virtual public void Combine(MedicalRecordFileBorrowedCollection collection)
        {
            base.Combine(collection);
        }

        new public MedicalRecordFileBorrowed this[int index]
        {
            get
            {
                return base[index] as MedicalRecordFileBorrowed;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(MedicalRecordFileBorrowed);
        }
    }

    [Serializable]
    abstract public class esMedicalRecordFileBorrowed : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esMedicalRecordFileBorrowedQuery GetDynamicQuery()
        {
            return null;
        }

        public esMedicalRecordFileBorrowed()
        {
        }

        public esMedicalRecordFileBorrowed(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String transactionNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(transactionNo);
            else
                return LoadByPrimaryKeyStoredProcedure(transactionNo);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String transactionNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(transactionNo);
            else
                return LoadByPrimaryKeyStoredProcedure(transactionNo);
        }

        private bool LoadByPrimaryKeyDynamic(String transactionNo)
        {
            esMedicalRecordFileBorrowedQuery query = this.GetDynamicQuery();
            query.Where(query.TransactionNo == transactionNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String transactionNo)
        {
            esParameters parms = new esParameters();
            parms.Add("TransactionNo", transactionNo);
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
                        case "TransactionNo": this.str.TransactionNo = (string)value; break;
                        case "PatientID": this.str.PatientID = (string)value; break;
                        case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
                        case "DateOfBorrowing": this.str.DateOfBorrowing = (string)value; break;
                        case "DateOfReturn": this.str.DateOfReturn = (string)value; break;
                        case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
                        case "NameOfTheBorrower": this.str.NameOfTheBorrower = (string)value; break;
                        case "SRForThePurposesOf": this.str.SRForThePurposesOf = (string)value; break;
                        case "Notes": this.str.Notes = (string)value; break;
                        case "NameOfRecipientID": this.str.NameOfRecipientID = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "NameOfGivenID": this.str.NameOfGivenID = (string)value; break;
                        case "ReturnByName": this.str.ReturnByName = (string)value; break;
                        case "Duration": this.str.Duration = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "DateOfBorrowing":

                            if (value == null || value is System.DateTime)
                                this.DateOfBorrowing = (System.DateTime?)value;
                            break;
                        case "DateOfReturn":

                            if (value == null || value is System.DateTime)
                                this.DateOfReturn = (System.DateTime?)value;
                            break;
                        case "LastUpdateDateTime":

                            if (value == null || value is System.DateTime)
                                this.LastUpdateDateTime = (System.DateTime?)value;
                            break;
                        case "Duration":

                            if (value == null || value is System.Int16)
                                this.Duration = (System.Int16?)value;
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
        /// Maps to MedicalRecordFileBorrowed.TransactionNo
        /// </summary>
        virtual public System.String TransactionNo
        {
            get
            {
                return base.GetSystemString(MedicalRecordFileBorrowedMetadata.ColumnNames.TransactionNo);
            }

            set
            {
                base.SetSystemString(MedicalRecordFileBorrowedMetadata.ColumnNames.TransactionNo, value);
            }
        }
        /// <summary>
        /// Maps to MedicalRecordFileBorrowed.PatientID
        /// </summary>
        virtual public System.String PatientID
        {
            get
            {
                return base.GetSystemString(MedicalRecordFileBorrowedMetadata.ColumnNames.PatientID);
            }

            set
            {
                base.SetSystemString(MedicalRecordFileBorrowedMetadata.ColumnNames.PatientID, value);
            }
        }
        /// <summary>
        /// Maps to MedicalRecordFileBorrowed.RegistrationNo
        /// </summary>
        virtual public System.String RegistrationNo
        {
            get
            {
                return base.GetSystemString(MedicalRecordFileBorrowedMetadata.ColumnNames.RegistrationNo);
            }

            set
            {
                base.SetSystemString(MedicalRecordFileBorrowedMetadata.ColumnNames.RegistrationNo, value);
            }
        }
        /// <summary>
        /// Maps to MedicalRecordFileBorrowed.DateOfBorrowing
        /// </summary>
        virtual public System.DateTime? DateOfBorrowing
        {
            get
            {
                return base.GetSystemDateTime(MedicalRecordFileBorrowedMetadata.ColumnNames.DateOfBorrowing);
            }

            set
            {
                base.SetSystemDateTime(MedicalRecordFileBorrowedMetadata.ColumnNames.DateOfBorrowing, value);
            }
        }
        /// <summary>
        /// Maps to MedicalRecordFileBorrowed.DateOfReturn
        /// </summary>
        virtual public System.DateTime? DateOfReturn
        {
            get
            {
                return base.GetSystemDateTime(MedicalRecordFileBorrowedMetadata.ColumnNames.DateOfReturn);
            }

            set
            {
                base.SetSystemDateTime(MedicalRecordFileBorrowedMetadata.ColumnNames.DateOfReturn, value);
            }
        }
        /// <summary>
        /// Maps to MedicalRecordFileBorrowed.ServiceUnitID
        /// </summary>
        virtual public System.String ServiceUnitID
        {
            get
            {
                return base.GetSystemString(MedicalRecordFileBorrowedMetadata.ColumnNames.ServiceUnitID);
            }

            set
            {
                base.SetSystemString(MedicalRecordFileBorrowedMetadata.ColumnNames.ServiceUnitID, value);
            }
        }
        /// <summary>
        /// Maps to MedicalRecordFileBorrowed.NameOfTheBorrower
        /// </summary>
        virtual public System.String NameOfTheBorrower
        {
            get
            {
                return base.GetSystemString(MedicalRecordFileBorrowedMetadata.ColumnNames.NameOfTheBorrower);
            }

            set
            {
                base.SetSystemString(MedicalRecordFileBorrowedMetadata.ColumnNames.NameOfTheBorrower, value);
            }
        }
        /// <summary>
        /// Maps to MedicalRecordFileBorrowed.SRForThePurposesOf
        /// </summary>
        virtual public System.String SRForThePurposesOf
        {
            get
            {
                return base.GetSystemString(MedicalRecordFileBorrowedMetadata.ColumnNames.SRForThePurposesOf);
            }

            set
            {
                base.SetSystemString(MedicalRecordFileBorrowedMetadata.ColumnNames.SRForThePurposesOf, value);
            }
        }
        /// <summary>
        /// Maps to MedicalRecordFileBorrowed.Notes
        /// </summary>
        virtual public System.String Notes
        {
            get
            {
                return base.GetSystemString(MedicalRecordFileBorrowedMetadata.ColumnNames.Notes);
            }

            set
            {
                base.SetSystemString(MedicalRecordFileBorrowedMetadata.ColumnNames.Notes, value);
            }
        }
        /// <summary>
        /// Maps to MedicalRecordFileBorrowed.NameOfRecipientID
        /// </summary>
        virtual public System.String NameOfRecipientID
        {
            get
            {
                return base.GetSystemString(MedicalRecordFileBorrowedMetadata.ColumnNames.NameOfRecipientID);
            }

            set
            {
                base.SetSystemString(MedicalRecordFileBorrowedMetadata.ColumnNames.NameOfRecipientID, value);
            }
        }
        /// <summary>
        /// Maps to MedicalRecordFileBorrowed.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(MedicalRecordFileBorrowedMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(MedicalRecordFileBorrowedMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to MedicalRecordFileBorrowed.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(MedicalRecordFileBorrowedMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(MedicalRecordFileBorrowedMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to MedicalRecordFileBorrowed.NameOfGivenID
        /// </summary>
        virtual public System.String NameOfGivenID
        {
            get
            {
                return base.GetSystemString(MedicalRecordFileBorrowedMetadata.ColumnNames.NameOfGivenID);
            }

            set
            {
                base.SetSystemString(MedicalRecordFileBorrowedMetadata.ColumnNames.NameOfGivenID, value);
            }
        }
        /// <summary>
        /// Maps to MedicalRecordFileBorrowed.ReturnByName
        /// </summary>
        virtual public System.String ReturnByName
        {
            get
            {
                return base.GetSystemString(MedicalRecordFileBorrowedMetadata.ColumnNames.ReturnByName);
            }

            set
            {
                base.SetSystemString(MedicalRecordFileBorrowedMetadata.ColumnNames.ReturnByName, value);
            }
        }
        /// <summary>
        /// Maps to MedicalRecordFileBorrowed.Duration
        /// </summary>
        virtual public System.Int16? Duration
        {
            get
            {
                return base.GetSystemInt16(MedicalRecordFileBorrowedMetadata.ColumnNames.Duration);
            }

            set
            {
                base.SetSystemInt16(MedicalRecordFileBorrowedMetadata.ColumnNames.Duration, value);
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
            public esStrings(esMedicalRecordFileBorrowed entity)
            {
                this.entity = entity;
            }
            public System.String TransactionNo
            {
                get
                {
                    System.String data = entity.TransactionNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TransactionNo = null;
                    else entity.TransactionNo = Convert.ToString(value);
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
            public System.String DateOfBorrowing
            {
                get
                {
                    System.DateTime? data = entity.DateOfBorrowing;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DateOfBorrowing = null;
                    else entity.DateOfBorrowing = Convert.ToDateTime(value);
                }
            }
            public System.String DateOfReturn
            {
                get
                {
                    System.DateTime? data = entity.DateOfReturn;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DateOfReturn = null;
                    else entity.DateOfReturn = Convert.ToDateTime(value);
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
            public System.String NameOfTheBorrower
            {
                get
                {
                    System.String data = entity.NameOfTheBorrower;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.NameOfTheBorrower = null;
                    else entity.NameOfTheBorrower = Convert.ToString(value);
                }
            }
            public System.String SRForThePurposesOf
            {
                get
                {
                    System.String data = entity.SRForThePurposesOf;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRForThePurposesOf = null;
                    else entity.SRForThePurposesOf = Convert.ToString(value);
                }
            }
            public System.String Notes
            {
                get
                {
                    System.String data = entity.Notes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Notes = null;
                    else entity.Notes = Convert.ToString(value);
                }
            }
            public System.String NameOfRecipientID
            {
                get
                {
                    System.String data = entity.NameOfRecipientID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.NameOfRecipientID = null;
                    else entity.NameOfRecipientID = Convert.ToString(value);
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
            public System.String NameOfGivenID
            {
                get
                {
                    System.String data = entity.NameOfGivenID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.NameOfGivenID = null;
                    else entity.NameOfGivenID = Convert.ToString(value);
                }
            }
            public System.String ReturnByName
            {
                get
                {
                    System.String data = entity.ReturnByName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ReturnByName = null;
                    else entity.ReturnByName = Convert.ToString(value);
                }
            }
            public System.String Duration
            {
                get
                {
                    System.Int16? data = entity.Duration;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Duration = null;
                    else entity.Duration = Convert.ToInt16(value);
                }
            }
            private esMedicalRecordFileBorrowed entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esMedicalRecordFileBorrowedQuery query)
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
                throw new Exception("esMedicalRecordFileBorrowed can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class MedicalRecordFileBorrowed : esMedicalRecordFileBorrowed
    {
    }

    [Serializable]
    abstract public class esMedicalRecordFileBorrowedQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return MedicalRecordFileBorrowedMetadata.Meta();
            }
        }

        public esQueryItem TransactionNo
        {
            get
            {
                return new esQueryItem(this, MedicalRecordFileBorrowedMetadata.ColumnNames.TransactionNo, esSystemType.String);
            }
        }

        public esQueryItem PatientID
        {
            get
            {
                return new esQueryItem(this, MedicalRecordFileBorrowedMetadata.ColumnNames.PatientID, esSystemType.String);
            }
        }

        public esQueryItem RegistrationNo
        {
            get
            {
                return new esQueryItem(this, MedicalRecordFileBorrowedMetadata.ColumnNames.RegistrationNo, esSystemType.String);
            }
        }

        public esQueryItem DateOfBorrowing
        {
            get
            {
                return new esQueryItem(this, MedicalRecordFileBorrowedMetadata.ColumnNames.DateOfBorrowing, esSystemType.DateTime);
            }
        }

        public esQueryItem DateOfReturn
        {
            get
            {
                return new esQueryItem(this, MedicalRecordFileBorrowedMetadata.ColumnNames.DateOfReturn, esSystemType.DateTime);
            }
        }

        public esQueryItem ServiceUnitID
        {
            get
            {
                return new esQueryItem(this, MedicalRecordFileBorrowedMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
            }
        }

        public esQueryItem NameOfTheBorrower
        {
            get
            {
                return new esQueryItem(this, MedicalRecordFileBorrowedMetadata.ColumnNames.NameOfTheBorrower, esSystemType.String);
            }
        }

        public esQueryItem SRForThePurposesOf
        {
            get
            {
                return new esQueryItem(this, MedicalRecordFileBorrowedMetadata.ColumnNames.SRForThePurposesOf, esSystemType.String);
            }
        }

        public esQueryItem Notes
        {
            get
            {
                return new esQueryItem(this, MedicalRecordFileBorrowedMetadata.ColumnNames.Notes, esSystemType.String);
            }
        }

        public esQueryItem NameOfRecipientID
        {
            get
            {
                return new esQueryItem(this, MedicalRecordFileBorrowedMetadata.ColumnNames.NameOfRecipientID, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, MedicalRecordFileBorrowedMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, MedicalRecordFileBorrowedMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem NameOfGivenID
        {
            get
            {
                return new esQueryItem(this, MedicalRecordFileBorrowedMetadata.ColumnNames.NameOfGivenID, esSystemType.String);
            }
        }

        public esQueryItem ReturnByName
        {
            get
            {
                return new esQueryItem(this, MedicalRecordFileBorrowedMetadata.ColumnNames.ReturnByName, esSystemType.String);
            }
        }

        public esQueryItem Duration
        {
            get
            {
                return new esQueryItem(this, MedicalRecordFileBorrowedMetadata.ColumnNames.Duration, esSystemType.Int16);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("MedicalRecordFileBorrowedCollection")]
    public partial class MedicalRecordFileBorrowedCollection : esMedicalRecordFileBorrowedCollection, IEnumerable<MedicalRecordFileBorrowed>
    {
        public MedicalRecordFileBorrowedCollection()
        {

        }

        public static implicit operator List<MedicalRecordFileBorrowed>(MedicalRecordFileBorrowedCollection coll)
        {
            List<MedicalRecordFileBorrowed> list = new List<MedicalRecordFileBorrowed>();

            foreach (MedicalRecordFileBorrowed emp in coll)
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
                return MedicalRecordFileBorrowedMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new MedicalRecordFileBorrowedQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new MedicalRecordFileBorrowed(row);
        }

        override protected esEntity CreateEntity()
        {
            return new MedicalRecordFileBorrowed();
        }

        #endregion

        [BrowsableAttribute(false)]
        public MedicalRecordFileBorrowedQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new MedicalRecordFileBorrowedQuery();
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
        public bool Load(MedicalRecordFileBorrowedQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public MedicalRecordFileBorrowed AddNew()
        {
            MedicalRecordFileBorrowed entity = base.AddNewEntity() as MedicalRecordFileBorrowed;

            return entity;
        }
        public MedicalRecordFileBorrowed FindByPrimaryKey(String transactionNo)
        {
            return base.FindByPrimaryKey(transactionNo) as MedicalRecordFileBorrowed;
        }

        #region IEnumerable< MedicalRecordFileBorrowed> Members

        IEnumerator<MedicalRecordFileBorrowed> IEnumerable<MedicalRecordFileBorrowed>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as MedicalRecordFileBorrowed;
            }
        }

        #endregion

        private MedicalRecordFileBorrowedQuery query;
    }


    /// <summary>
    /// Encapsulates the 'MedicalRecordFileBorrowed' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("MedicalRecordFileBorrowed ({TransactionNo})")]
    [Serializable]
    public partial class MedicalRecordFileBorrowed : esMedicalRecordFileBorrowed
    {
        public MedicalRecordFileBorrowed()
        {
        }

        public MedicalRecordFileBorrowed(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return MedicalRecordFileBorrowedMetadata.Meta();
            }
        }

        override protected esMedicalRecordFileBorrowedQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new MedicalRecordFileBorrowedQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public MedicalRecordFileBorrowedQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new MedicalRecordFileBorrowedQuery();
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
        public bool Load(MedicalRecordFileBorrowedQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private MedicalRecordFileBorrowedQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class MedicalRecordFileBorrowedQuery : esMedicalRecordFileBorrowedQuery
    {
        public MedicalRecordFileBorrowedQuery()
        {

        }

        public MedicalRecordFileBorrowedQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "MedicalRecordFileBorrowedQuery";
        }
    }

    [Serializable]
    public partial class MedicalRecordFileBorrowedMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected MedicalRecordFileBorrowedMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(MedicalRecordFileBorrowedMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalRecordFileBorrowedMetadata.PropertyNames.TransactionNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalRecordFileBorrowedMetadata.ColumnNames.PatientID, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalRecordFileBorrowedMetadata.PropertyNames.PatientID;
            c.CharacterMaxLength = 15;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalRecordFileBorrowedMetadata.ColumnNames.RegistrationNo, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalRecordFileBorrowedMetadata.PropertyNames.RegistrationNo;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalRecordFileBorrowedMetadata.ColumnNames.DateOfBorrowing, 3, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = MedicalRecordFileBorrowedMetadata.PropertyNames.DateOfBorrowing;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalRecordFileBorrowedMetadata.ColumnNames.DateOfReturn, 4, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = MedicalRecordFileBorrowedMetadata.PropertyNames.DateOfReturn;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalRecordFileBorrowedMetadata.ColumnNames.ServiceUnitID, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalRecordFileBorrowedMetadata.PropertyNames.ServiceUnitID;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalRecordFileBorrowedMetadata.ColumnNames.NameOfTheBorrower, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalRecordFileBorrowedMetadata.PropertyNames.NameOfTheBorrower;
            c.CharacterMaxLength = 150;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalRecordFileBorrowedMetadata.ColumnNames.SRForThePurposesOf, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalRecordFileBorrowedMetadata.PropertyNames.SRForThePurposesOf;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalRecordFileBorrowedMetadata.ColumnNames.Notes, 8, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalRecordFileBorrowedMetadata.PropertyNames.Notes;
            c.CharacterMaxLength = 250;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalRecordFileBorrowedMetadata.ColumnNames.NameOfRecipientID, 9, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalRecordFileBorrowedMetadata.PropertyNames.NameOfRecipientID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalRecordFileBorrowedMetadata.ColumnNames.LastUpdateDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = MedicalRecordFileBorrowedMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalRecordFileBorrowedMetadata.ColumnNames.LastUpdateByUserID, 11, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalRecordFileBorrowedMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalRecordFileBorrowedMetadata.ColumnNames.NameOfGivenID, 12, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalRecordFileBorrowedMetadata.PropertyNames.NameOfGivenID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalRecordFileBorrowedMetadata.ColumnNames.ReturnByName, 13, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalRecordFileBorrowedMetadata.PropertyNames.ReturnByName;
            c.CharacterMaxLength = 150;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalRecordFileBorrowedMetadata.ColumnNames.Duration, 14, typeof(System.Int16), esSystemType.Int16);
            c.PropertyName = MedicalRecordFileBorrowedMetadata.PropertyNames.Duration;
            c.NumericPrecision = 5;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public MedicalRecordFileBorrowedMetadata Meta()
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
            public const string TransactionNo = "TransactionNo";
            public const string PatientID = "PatientID";
            public const string RegistrationNo = "RegistrationNo";
            public const string DateOfBorrowing = "DateOfBorrowing";
            public const string DateOfReturn = "DateOfReturn";
            public const string ServiceUnitID = "ServiceUnitID";
            public const string NameOfTheBorrower = "NameOfTheBorrower";
            public const string SRForThePurposesOf = "SRForThePurposesOf";
            public const string Notes = "Notes";
            public const string NameOfRecipientID = "NameOfRecipientID";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string NameOfGivenID = "NameOfGivenID";
            public const string ReturnByName = "ReturnByName";
            public const string Duration = "Duration";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string TransactionNo = "TransactionNo";
            public const string PatientID = "PatientID";
            public const string RegistrationNo = "RegistrationNo";
            public const string DateOfBorrowing = "DateOfBorrowing";
            public const string DateOfReturn = "DateOfReturn";
            public const string ServiceUnitID = "ServiceUnitID";
            public const string NameOfTheBorrower = "NameOfTheBorrower";
            public const string SRForThePurposesOf = "SRForThePurposesOf";
            public const string Notes = "Notes";
            public const string NameOfRecipientID = "NameOfRecipientID";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string NameOfGivenID = "NameOfGivenID";
            public const string ReturnByName = "ReturnByName";
            public const string Duration = "Duration";
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
            lock (typeof(MedicalRecordFileBorrowedMetadata))
            {
                if (MedicalRecordFileBorrowedMetadata.mapDelegates == null)
                {
                    MedicalRecordFileBorrowedMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (MedicalRecordFileBorrowedMetadata.meta == null)
                {
                    MedicalRecordFileBorrowedMetadata.meta = new MedicalRecordFileBorrowedMetadata();
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

                meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("PatientID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("DateOfBorrowing", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("DateOfReturn", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("NameOfTheBorrower", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRForThePurposesOf", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("NameOfRecipientID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("NameOfGivenID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ReturnByName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Duration", new esTypeMap("smallint", "System.Int16"));


                meta.Source = "MedicalRecordFileBorrowed";
                meta.Destination = "MedicalRecordFileBorrowed";
                meta.spInsert = "proc_MedicalRecordFileBorrowedInsert";
                meta.spUpdate = "proc_MedicalRecordFileBorrowedUpdate";
                meta.spDelete = "proc_MedicalRecordFileBorrowedDelete";
                meta.spLoadAll = "proc_MedicalRecordFileBorrowedLoadAll";
                meta.spLoadByPrimaryKey = "proc_MedicalRecordFileBorrowedLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private MedicalRecordFileBorrowedMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
