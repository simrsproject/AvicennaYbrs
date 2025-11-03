/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 6/17/2017 10:34:31 AM
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
    abstract public class esBloodExterminationCollection : esEntityCollectionWAuditLog
    {
        public esBloodExterminationCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "BloodExterminationCollection";
        }

        #region Query Logic
        protected void InitQuery(esBloodExterminationQuery query)
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
            this.InitQuery(query as esBloodExterminationQuery);
        }
        #endregion

        virtual public BloodExtermination DetachEntity(BloodExtermination entity)
        {
            return base.DetachEntity(entity) as BloodExtermination;
        }

        virtual public BloodExtermination AttachEntity(BloodExtermination entity)
        {
            return base.AttachEntity(entity) as BloodExtermination;
        }

        virtual public void Combine(BloodExterminationCollection collection)
        {
            base.Combine(collection);
        }

        new public BloodExtermination this[int index]
        {
            get
            {
                return base[index] as BloodExtermination;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(BloodExtermination);
        }
    }

    [Serializable]
    abstract public class esBloodExtermination : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esBloodExterminationQuery GetDynamicQuery()
        {
            return null;
        }

        public esBloodExtermination()
        {
        }

        public esBloodExtermination(DataRow row)
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
            esBloodExterminationQuery query = this.GetDynamicQuery();
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
                        case "TransactionDate": this.str.TransactionDate = (string)value; break;
                        case "SRReasonsForExtermination": this.str.SRReasonsForExtermination = (string)value; break;
                        case "Weight": this.str.Weight = (string)value; break;
                        case "BloodBankOfficerByUserID": this.str.BloodBankOfficerByUserID = (string)value; break;
                        case "IncineratorOperator": this.str.IncineratorOperator = (string)value; break;
                        case "KnownBy": this.str.KnownBy = (string)value; break;
                        case "Notes": this.str.Notes = (string)value; break;
                        case "IsApproved": this.str.IsApproved = (string)value; break;
                        case "ApprovedDateTime": this.str.ApprovedDateTime = (string)value; break;
                        case "ApprovedByUserID": this.str.ApprovedByUserID = (string)value; break;
                        case "IsVoid": this.str.IsVoid = (string)value; break;
                        case "VoidDateTime": this.str.VoidDateTime = (string)value; break;
                        case "VoidByUserID": this.str.VoidByUserID = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "TransactionDate":

                            if (value == null || value is System.DateTime)
                                this.TransactionDate = (System.DateTime?)value;
                            break;
                        case "Weight":

                            if (value == null || value is System.Decimal)
                                this.Weight = (System.Decimal?)value;
                            break;
                        case "IsApproved":

                            if (value == null || value is System.Boolean)
                                this.IsApproved = (System.Boolean?)value;
                            break;
                        case "ApprovedDateTime":

                            if (value == null || value is System.DateTime)
                                this.ApprovedDateTime = (System.DateTime?)value;
                            break;
                        case "IsVoid":

                            if (value == null || value is System.Boolean)
                                this.IsVoid = (System.Boolean?)value;
                            break;
                        case "VoidDateTime":

                            if (value == null || value is System.DateTime)
                                this.VoidDateTime = (System.DateTime?)value;
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
        /// Maps to BloodExtermination.TransactionNo
        /// </summary>
        virtual public System.String TransactionNo
        {
            get
            {
                return base.GetSystemString(BloodExterminationMetadata.ColumnNames.TransactionNo);
            }

            set
            {
                base.SetSystemString(BloodExterminationMetadata.ColumnNames.TransactionNo, value);
            }
        }
        /// <summary>
        /// Maps to BloodExtermination.TransactionDate
        /// </summary>
        virtual public System.DateTime? TransactionDate
        {
            get
            {
                return base.GetSystemDateTime(BloodExterminationMetadata.ColumnNames.TransactionDate);
            }

            set
            {
                base.SetSystemDateTime(BloodExterminationMetadata.ColumnNames.TransactionDate, value);
            }
        }
        /// <summary>
        /// Maps to BloodExtermination.SRReasonsForExtermination
        /// </summary>
        virtual public System.String SRReasonsForExtermination
        {
            get
            {
                return base.GetSystemString(BloodExterminationMetadata.ColumnNames.SRReasonsForExtermination);
            }

            set
            {
                base.SetSystemString(BloodExterminationMetadata.ColumnNames.SRReasonsForExtermination, value);
            }
        }
        /// <summary>
        /// Maps to BloodExtermination.Weight
        /// </summary>
        virtual public System.Decimal? Weight
        {
            get
            {
                return base.GetSystemDecimal(BloodExterminationMetadata.ColumnNames.Weight);
            }

            set
            {
                base.SetSystemDecimal(BloodExterminationMetadata.ColumnNames.Weight, value);
            }
        }
        /// <summary>
        /// Maps to BloodExtermination.BloodBankOfficerByUserID
        /// </summary>
        virtual public System.String BloodBankOfficerByUserID
        {
            get
            {
                return base.GetSystemString(BloodExterminationMetadata.ColumnNames.BloodBankOfficerByUserID);
            }

            set
            {
                base.SetSystemString(BloodExterminationMetadata.ColumnNames.BloodBankOfficerByUserID, value);
            }
        }
        /// <summary>
        /// Maps to BloodExtermination.IncineratorOperator
        /// </summary>
        virtual public System.String IncineratorOperator
        {
            get
            {
                return base.GetSystemString(BloodExterminationMetadata.ColumnNames.IncineratorOperator);
            }

            set
            {
                base.SetSystemString(BloodExterminationMetadata.ColumnNames.IncineratorOperator, value);
            }
        }
        /// <summary>
        /// Maps to BloodExtermination.KnownBy
        /// </summary>
        virtual public System.String KnownBy
        {
            get
            {
                return base.GetSystemString(BloodExterminationMetadata.ColumnNames.KnownBy);
            }

            set
            {
                base.SetSystemString(BloodExterminationMetadata.ColumnNames.KnownBy, value);
            }
        }
        /// <summary>
        /// Maps to BloodExtermination.Notes
        /// </summary>
        virtual public System.String Notes
        {
            get
            {
                return base.GetSystemString(BloodExterminationMetadata.ColumnNames.Notes);
            }

            set
            {
                base.SetSystemString(BloodExterminationMetadata.ColumnNames.Notes, value);
            }
        }
        /// <summary>
        /// Maps to BloodExtermination.IsApproved
        /// </summary>
        virtual public System.Boolean? IsApproved
        {
            get
            {
                return base.GetSystemBoolean(BloodExterminationMetadata.ColumnNames.IsApproved);
            }

            set
            {
                base.SetSystemBoolean(BloodExterminationMetadata.ColumnNames.IsApproved, value);
            }
        }
        /// <summary>
        /// Maps to BloodExtermination.ApprovedDateTime
        /// </summary>
        virtual public System.DateTime? ApprovedDateTime
        {
            get
            {
                return base.GetSystemDateTime(BloodExterminationMetadata.ColumnNames.ApprovedDateTime);
            }

            set
            {
                base.SetSystemDateTime(BloodExterminationMetadata.ColumnNames.ApprovedDateTime, value);
            }
        }
        /// <summary>
        /// Maps to BloodExtermination.ApprovedByUserID
        /// </summary>
        virtual public System.String ApprovedByUserID
        {
            get
            {
                return base.GetSystemString(BloodExterminationMetadata.ColumnNames.ApprovedByUserID);
            }

            set
            {
                base.SetSystemString(BloodExterminationMetadata.ColumnNames.ApprovedByUserID, value);
            }
        }
        /// <summary>
        /// Maps to BloodExtermination.IsVoid
        /// </summary>
        virtual public System.Boolean? IsVoid
        {
            get
            {
                return base.GetSystemBoolean(BloodExterminationMetadata.ColumnNames.IsVoid);
            }

            set
            {
                base.SetSystemBoolean(BloodExterminationMetadata.ColumnNames.IsVoid, value);
            }
        }
        /// <summary>
        /// Maps to BloodExtermination.VoidDateTime
        /// </summary>
        virtual public System.DateTime? VoidDateTime
        {
            get
            {
                return base.GetSystemDateTime(BloodExterminationMetadata.ColumnNames.VoidDateTime);
            }

            set
            {
                base.SetSystemDateTime(BloodExterminationMetadata.ColumnNames.VoidDateTime, value);
            }
        }
        /// <summary>
        /// Maps to BloodExtermination.VoidByUserID
        /// </summary>
        virtual public System.String VoidByUserID
        {
            get
            {
                return base.GetSystemString(BloodExterminationMetadata.ColumnNames.VoidByUserID);
            }

            set
            {
                base.SetSystemString(BloodExterminationMetadata.ColumnNames.VoidByUserID, value);
            }
        }
        /// <summary>
        /// Maps to BloodExtermination.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(BloodExterminationMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(BloodExterminationMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to BloodExtermination.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(BloodExterminationMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(BloodExterminationMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esBloodExtermination entity)
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
            public System.String TransactionDate
            {
                get
                {
                    System.DateTime? data = entity.TransactionDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TransactionDate = null;
                    else entity.TransactionDate = Convert.ToDateTime(value);
                }
            }
            public System.String SRReasonsForExtermination
            {
                get
                {
                    System.String data = entity.SRReasonsForExtermination;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRReasonsForExtermination = null;
                    else entity.SRReasonsForExtermination = Convert.ToString(value);
                }
            }
            public System.String Weight
            {
                get
                {
                    System.Decimal? data = entity.Weight;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Weight = null;
                    else entity.Weight = Convert.ToDecimal(value);
                }
            }
            public System.String BloodBankOfficerByUserID
            {
                get
                {
                    System.String data = entity.BloodBankOfficerByUserID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.BloodBankOfficerByUserID = null;
                    else entity.BloodBankOfficerByUserID = Convert.ToString(value);
                }
            }
            public System.String IncineratorOperator
            {
                get
                {
                    System.String data = entity.IncineratorOperator;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IncineratorOperator = null;
                    else entity.IncineratorOperator = Convert.ToString(value);
                }
            }
            public System.String KnownBy
            {
                get
                {
                    System.String data = entity.KnownBy;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.KnownBy = null;
                    else entity.KnownBy = Convert.ToString(value);
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
            public System.String IsApproved
            {
                get
                {
                    System.Boolean? data = entity.IsApproved;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsApproved = null;
                    else entity.IsApproved = Convert.ToBoolean(value);
                }
            }
            public System.String ApprovedDateTime
            {
                get
                {
                    System.DateTime? data = entity.ApprovedDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ApprovedDateTime = null;
                    else entity.ApprovedDateTime = Convert.ToDateTime(value);
                }
            }
            public System.String ApprovedByUserID
            {
                get
                {
                    System.String data = entity.ApprovedByUserID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ApprovedByUserID = null;
                    else entity.ApprovedByUserID = Convert.ToString(value);
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
            public System.String VoidDateTime
            {
                get
                {
                    System.DateTime? data = entity.VoidDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.VoidDateTime = null;
                    else entity.VoidDateTime = Convert.ToDateTime(value);
                }
            }
            public System.String VoidByUserID
            {
                get
                {
                    System.String data = entity.VoidByUserID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.VoidByUserID = null;
                    else entity.VoidByUserID = Convert.ToString(value);
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
            private esBloodExtermination entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esBloodExterminationQuery query)
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
                throw new Exception("esBloodExtermination can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class BloodExtermination : esBloodExtermination
    {
    }

    [Serializable]
    abstract public class esBloodExterminationQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return BloodExterminationMetadata.Meta();
            }
        }

        public esQueryItem TransactionNo
        {
            get
            {
                return new esQueryItem(this, BloodExterminationMetadata.ColumnNames.TransactionNo, esSystemType.String);
            }
        }

        public esQueryItem TransactionDate
        {
            get
            {
                return new esQueryItem(this, BloodExterminationMetadata.ColumnNames.TransactionDate, esSystemType.DateTime);
            }
        }

        public esQueryItem SRReasonsForExtermination
        {
            get
            {
                return new esQueryItem(this, BloodExterminationMetadata.ColumnNames.SRReasonsForExtermination, esSystemType.String);
            }
        }

        public esQueryItem Weight
        {
            get
            {
                return new esQueryItem(this, BloodExterminationMetadata.ColumnNames.Weight, esSystemType.Decimal);
            }
        }

        public esQueryItem BloodBankOfficerByUserID
        {
            get
            {
                return new esQueryItem(this, BloodExterminationMetadata.ColumnNames.BloodBankOfficerByUserID, esSystemType.String);
            }
        }

        public esQueryItem IncineratorOperator
        {
            get
            {
                return new esQueryItem(this, BloodExterminationMetadata.ColumnNames.IncineratorOperator, esSystemType.String);
            }
        }

        public esQueryItem KnownBy
        {
            get
            {
                return new esQueryItem(this, BloodExterminationMetadata.ColumnNames.KnownBy, esSystemType.String);
            }
        }

        public esQueryItem Notes
        {
            get
            {
                return new esQueryItem(this, BloodExterminationMetadata.ColumnNames.Notes, esSystemType.String);
            }
        }

        public esQueryItem IsApproved
        {
            get
            {
                return new esQueryItem(this, BloodExterminationMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
            }
        }

        public esQueryItem ApprovedDateTime
        {
            get
            {
                return new esQueryItem(this, BloodExterminationMetadata.ColumnNames.ApprovedDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem ApprovedByUserID
        {
            get
            {
                return new esQueryItem(this, BloodExterminationMetadata.ColumnNames.ApprovedByUserID, esSystemType.String);
            }
        }

        public esQueryItem IsVoid
        {
            get
            {
                return new esQueryItem(this, BloodExterminationMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
            }
        }

        public esQueryItem VoidDateTime
        {
            get
            {
                return new esQueryItem(this, BloodExterminationMetadata.ColumnNames.VoidDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem VoidByUserID
        {
            get
            {
                return new esQueryItem(this, BloodExterminationMetadata.ColumnNames.VoidByUserID, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, BloodExterminationMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, BloodExterminationMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("BloodExterminationCollection")]
    public partial class BloodExterminationCollection : esBloodExterminationCollection, IEnumerable<BloodExtermination>
    {
        public BloodExterminationCollection()
        {

        }

        public static implicit operator List<BloodExtermination>(BloodExterminationCollection coll)
        {
            List<BloodExtermination> list = new List<BloodExtermination>();

            foreach (BloodExtermination emp in coll)
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
                return BloodExterminationMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new BloodExterminationQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new BloodExtermination(row);
        }

        override protected esEntity CreateEntity()
        {
            return new BloodExtermination();
        }

        #endregion

        [BrowsableAttribute(false)]
        public BloodExterminationQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new BloodExterminationQuery();
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
        public bool Load(BloodExterminationQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public BloodExtermination AddNew()
        {
            BloodExtermination entity = base.AddNewEntity() as BloodExtermination;

            return entity;
        }
        public BloodExtermination FindByPrimaryKey(String transactionNo)
        {
            return base.FindByPrimaryKey(transactionNo) as BloodExtermination;
        }

        #region IEnumerable< BloodExtermination> Members

        IEnumerator<BloodExtermination> IEnumerable<BloodExtermination>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as BloodExtermination;
            }
        }

        #endregion

        private BloodExterminationQuery query;
    }


    /// <summary>
    /// Encapsulates the 'BloodExtermination' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("BloodExtermination ({TransactionNo})")]
    [Serializable]
    public partial class BloodExtermination : esBloodExtermination
    {
        public BloodExtermination()
        {
        }

        public BloodExtermination(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return BloodExterminationMetadata.Meta();
            }
        }

        override protected esBloodExterminationQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new BloodExterminationQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public BloodExterminationQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new BloodExterminationQuery();
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
        public bool Load(BloodExterminationQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private BloodExterminationQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class BloodExterminationQuery : esBloodExterminationQuery
    {
        public BloodExterminationQuery()
        {

        }

        public BloodExterminationQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "BloodExterminationQuery";
        }
    }

    [Serializable]
    public partial class BloodExterminationMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected BloodExterminationMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(BloodExterminationMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = BloodExterminationMetadata.PropertyNames.TransactionNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(BloodExterminationMetadata.ColumnNames.TransactionDate, 1, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = BloodExterminationMetadata.PropertyNames.TransactionDate;
            _columns.Add(c);

            c = new esColumnMetadata(BloodExterminationMetadata.ColumnNames.SRReasonsForExtermination, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = BloodExterminationMetadata.PropertyNames.SRReasonsForExtermination;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(BloodExterminationMetadata.ColumnNames.Weight, 3, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = BloodExterminationMetadata.PropertyNames.Weight;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(BloodExterminationMetadata.ColumnNames.BloodBankOfficerByUserID, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = BloodExterminationMetadata.PropertyNames.BloodBankOfficerByUserID;
            c.CharacterMaxLength = 15;
            _columns.Add(c);

            c = new esColumnMetadata(BloodExterminationMetadata.ColumnNames.IncineratorOperator, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = BloodExterminationMetadata.PropertyNames.IncineratorOperator;
            c.CharacterMaxLength = 50;
            _columns.Add(c);

            c = new esColumnMetadata(BloodExterminationMetadata.ColumnNames.KnownBy, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = BloodExterminationMetadata.PropertyNames.KnownBy;
            c.CharacterMaxLength = 50;
            _columns.Add(c);

            c = new esColumnMetadata(BloodExterminationMetadata.ColumnNames.Notes, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = BloodExterminationMetadata.PropertyNames.Notes;
            c.CharacterMaxLength = 500;
            _columns.Add(c);

            c = new esColumnMetadata(BloodExterminationMetadata.ColumnNames.IsApproved, 8, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = BloodExterminationMetadata.PropertyNames.IsApproved;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BloodExterminationMetadata.ColumnNames.ApprovedDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = BloodExterminationMetadata.PropertyNames.ApprovedDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BloodExterminationMetadata.ColumnNames.ApprovedByUserID, 10, typeof(System.String), esSystemType.String);
            c.PropertyName = BloodExterminationMetadata.PropertyNames.ApprovedByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BloodExterminationMetadata.ColumnNames.IsVoid, 11, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = BloodExterminationMetadata.PropertyNames.IsVoid;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BloodExterminationMetadata.ColumnNames.VoidDateTime, 12, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = BloodExterminationMetadata.PropertyNames.VoidDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BloodExterminationMetadata.ColumnNames.VoidByUserID, 13, typeof(System.String), esSystemType.String);
            c.PropertyName = BloodExterminationMetadata.PropertyNames.VoidByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BloodExterminationMetadata.ColumnNames.LastUpdateDateTime, 14, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = BloodExterminationMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BloodExterminationMetadata.ColumnNames.LastUpdateByUserID, 15, typeof(System.String), esSystemType.String);
            c.PropertyName = BloodExterminationMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public BloodExterminationMetadata Meta()
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
            public const string TransactionDate = "TransactionDate";
            public const string SRReasonsForExtermination = "SRReasonsForExtermination";
            public const string Weight = "Weight";
            public const string BloodBankOfficerByUserID = "BloodBankOfficerByUserID";
            public const string IncineratorOperator = "IncineratorOperator";
            public const string KnownBy = "KnownBy";
            public const string Notes = "Notes";
            public const string IsApproved = "IsApproved";
            public const string ApprovedDateTime = "ApprovedDateTime";
            public const string ApprovedByUserID = "ApprovedByUserID";
            public const string IsVoid = "IsVoid";
            public const string VoidDateTime = "VoidDateTime";
            public const string VoidByUserID = "VoidByUserID";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string TransactionNo = "TransactionNo";
            public const string TransactionDate = "TransactionDate";
            public const string SRReasonsForExtermination = "SRReasonsForExtermination";
            public const string Weight = "Weight";
            public const string BloodBankOfficerByUserID = "BloodBankOfficerByUserID";
            public const string IncineratorOperator = "IncineratorOperator";
            public const string KnownBy = "KnownBy";
            public const string Notes = "Notes";
            public const string IsApproved = "IsApproved";
            public const string ApprovedDateTime = "ApprovedDateTime";
            public const string ApprovedByUserID = "ApprovedByUserID";
            public const string IsVoid = "IsVoid";
            public const string VoidDateTime = "VoidDateTime";
            public const string VoidByUserID = "VoidByUserID";
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
            lock (typeof(BloodExterminationMetadata))
            {
                if (BloodExterminationMetadata.mapDelegates == null)
                {
                    BloodExterminationMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (BloodExterminationMetadata.meta == null)
                {
                    BloodExterminationMetadata.meta = new BloodExterminationMetadata();
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
                meta.AddTypeMap("TransactionDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("SRReasonsForExtermination", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Weight", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("BloodBankOfficerByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IncineratorOperator", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("KnownBy", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("ApprovedDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("ApprovedByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("VoidDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("VoidByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "BloodExtermination";
                meta.Destination = "BloodExtermination";
                meta.spInsert = "proc_BloodExterminationInsert";
                meta.spUpdate = "proc_BloodExterminationUpdate";
                meta.spDelete = "proc_BloodExterminationDelete";
                meta.spLoadAll = "proc_BloodExterminationLoadAll";
                meta.spLoadByPrimaryKey = "proc_BloodExterminationLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private BloodExterminationMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
