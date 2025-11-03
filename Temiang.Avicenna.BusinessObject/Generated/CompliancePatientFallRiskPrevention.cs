/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/29/2023 10:23:20 AM
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
    abstract public class esCompliancePatientFallRiskPreventionCollection : esEntityCollectionWAuditLog
    {
        public esCompliancePatientFallRiskPreventionCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "CompliancePatientFallRiskPreventionCollection";
        }

        #region Query Logic
        protected void InitQuery(esCompliancePatientFallRiskPreventionQuery query)
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
            this.InitQuery(query as esCompliancePatientFallRiskPreventionQuery);
        }
        #endregion

        virtual public CompliancePatientFallRiskPrevention DetachEntity(CompliancePatientFallRiskPrevention entity)
        {
            return base.DetachEntity(entity) as CompliancePatientFallRiskPrevention;
        }

        virtual public CompliancePatientFallRiskPrevention AttachEntity(CompliancePatientFallRiskPrevention entity)
        {
            return base.AttachEntity(entity) as CompliancePatientFallRiskPrevention;
        }

        virtual public void Combine(CompliancePatientFallRiskPreventionCollection collection)
        {
            base.Combine(collection);
        }

        new public CompliancePatientFallRiskPrevention this[int index]
        {
            get
            {
                return base[index] as CompliancePatientFallRiskPrevention;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(CompliancePatientFallRiskPrevention);
        }
    }

    [Serializable]
    abstract public class esCompliancePatientFallRiskPrevention : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esCompliancePatientFallRiskPreventionQuery GetDynamicQuery()
        {
            return null;
        }

        public esCompliancePatientFallRiskPrevention()
        {
        }

        public esCompliancePatientFallRiskPrevention(DataRow row)
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
            esCompliancePatientFallRiskPreventionQuery query = this.GetDynamicQuery();
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
                        case "ObserverID": this.str.ObserverID = (string)value; break;
                        case "EmployeeID": this.str.EmployeeID = (string)value; break;
                        case "DepartmentID": this.str.DepartmentID = (string)value; break;
                        case "DivisionID": this.str.DivisionID = (string)value; break;
                        case "SubDivisionID": this.str.SubDivisionID = (string)value; break;
                        case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
                        case "IsApproved": this.str.IsApproved = (string)value; break;
                        case "ApprovedDateTime": this.str.ApprovedDateTime = (string)value; break;
                        case "ApprovedByUserID": this.str.ApprovedByUserID = (string)value; break;
                        case "IsVoid": this.str.IsVoid = (string)value; break;
                        case "VoidDateTime": this.str.VoidDateTime = (string)value; break;
                        case "VoidByUserID": this.str.VoidByUserID = (string)value; break;
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
                        case "TransactionDate":

                            if (value == null || value is System.DateTime)
                                this.TransactionDate = (System.DateTime?)value;
                            break;
                        case "ObserverID":

                            if (value == null || value is System.Int32)
                                this.ObserverID = (System.Int32?)value;
                            break;
                        case "EmployeeID":

                            if (value == null || value is System.Int32)
                                this.EmployeeID = (System.Int32?)value;
                            break;
                        case "DepartmentID":

                            if (value == null || value is System.Int32)
                                this.DepartmentID = (System.Int32?)value;
                            break;
                        case "DivisionID":

                            if (value == null || value is System.Int32)
                                this.DivisionID = (System.Int32?)value;
                            break;
                        case "SubDivisionID":

                            if (value == null || value is System.Int32)
                                this.SubDivisionID = (System.Int32?)value;
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
        /// Maps to CompliancePatientFallRiskPrevention.TransactionNo
        /// </summary>
        virtual public System.String TransactionNo
        {
            get
            {
                return base.GetSystemString(CompliancePatientFallRiskPreventionMetadata.ColumnNames.TransactionNo);
            }

            set
            {
                base.SetSystemString(CompliancePatientFallRiskPreventionMetadata.ColumnNames.TransactionNo, value);
            }
        }
        /// <summary>
        /// Maps to CompliancePatientFallRiskPrevention.TransactionDate
        /// </summary>
        virtual public System.DateTime? TransactionDate
        {
            get
            {
                return base.GetSystemDateTime(CompliancePatientFallRiskPreventionMetadata.ColumnNames.TransactionDate);
            }

            set
            {
                base.SetSystemDateTime(CompliancePatientFallRiskPreventionMetadata.ColumnNames.TransactionDate, value);
            }
        }
        /// <summary>
        /// Maps to CompliancePatientFallRiskPrevention.ObserverID
        /// </summary>
        virtual public System.Int32? ObserverID
        {
            get
            {
                return base.GetSystemInt32(CompliancePatientFallRiskPreventionMetadata.ColumnNames.ObserverID);
            }

            set
            {
                base.SetSystemInt32(CompliancePatientFallRiskPreventionMetadata.ColumnNames.ObserverID, value);
            }
        }
        /// <summary>
        /// Maps to CompliancePatientFallRiskPrevention.EmployeeID
        /// </summary>
        virtual public System.Int32? EmployeeID
        {
            get
            {
                return base.GetSystemInt32(CompliancePatientFallRiskPreventionMetadata.ColumnNames.EmployeeID);
            }

            set
            {
                base.SetSystemInt32(CompliancePatientFallRiskPreventionMetadata.ColumnNames.EmployeeID, value);
            }
        }
        /// <summary>
        /// Maps to CompliancePatientFallRiskPrevention.DepartmentID
        /// </summary>
        virtual public System.Int32? DepartmentID
        {
            get
            {
                return base.GetSystemInt32(CompliancePatientFallRiskPreventionMetadata.ColumnNames.DepartmentID);
            }

            set
            {
                base.SetSystemInt32(CompliancePatientFallRiskPreventionMetadata.ColumnNames.DepartmentID, value);
            }
        }
        /// <summary>
        /// Maps to CompliancePatientFallRiskPrevention.DivisionID
        /// </summary>
        virtual public System.Int32? DivisionID
        {
            get
            {
                return base.GetSystemInt32(CompliancePatientFallRiskPreventionMetadata.ColumnNames.DivisionID);
            }

            set
            {
                base.SetSystemInt32(CompliancePatientFallRiskPreventionMetadata.ColumnNames.DivisionID, value);
            }
        }
        /// <summary>
        /// Maps to CompliancePatientFallRiskPrevention.SubDivisionID
        /// </summary>
        virtual public System.Int32? SubDivisionID
        {
            get
            {
                return base.GetSystemInt32(CompliancePatientFallRiskPreventionMetadata.ColumnNames.SubDivisionID);
            }

            set
            {
                base.SetSystemInt32(CompliancePatientFallRiskPreventionMetadata.ColumnNames.SubDivisionID, value);
            }
        }
        /// <summary>
        /// Maps to CompliancePatientFallRiskPrevention.ServiceUnitID
        /// </summary>
        virtual public System.String ServiceUnitID
        {
            get
            {
                return base.GetSystemString(CompliancePatientFallRiskPreventionMetadata.ColumnNames.ServiceUnitID);
            }

            set
            {
                base.SetSystemString(CompliancePatientFallRiskPreventionMetadata.ColumnNames.ServiceUnitID, value);
            }
        }
        /// <summary>
        /// Maps to CompliancePatientFallRiskPrevention.IsApproved
        /// </summary>
        virtual public System.Boolean? IsApproved
        {
            get
            {
                return base.GetSystemBoolean(CompliancePatientFallRiskPreventionMetadata.ColumnNames.IsApproved);
            }

            set
            {
                base.SetSystemBoolean(CompliancePatientFallRiskPreventionMetadata.ColumnNames.IsApproved, value);
            }
        }
        /// <summary>
        /// Maps to CompliancePatientFallRiskPrevention.ApprovedDateTime
        /// </summary>
        virtual public System.DateTime? ApprovedDateTime
        {
            get
            {
                return base.GetSystemDateTime(CompliancePatientFallRiskPreventionMetadata.ColumnNames.ApprovedDateTime);
            }

            set
            {
                base.SetSystemDateTime(CompliancePatientFallRiskPreventionMetadata.ColumnNames.ApprovedDateTime, value);
            }
        }
        /// <summary>
        /// Maps to CompliancePatientFallRiskPrevention.ApprovedByUserID
        /// </summary>
        virtual public System.String ApprovedByUserID
        {
            get
            {
                return base.GetSystemString(CompliancePatientFallRiskPreventionMetadata.ColumnNames.ApprovedByUserID);
            }

            set
            {
                base.SetSystemString(CompliancePatientFallRiskPreventionMetadata.ColumnNames.ApprovedByUserID, value);
            }
        }
        /// <summary>
        /// Maps to CompliancePatientFallRiskPrevention.IsVoid
        /// </summary>
        virtual public System.Boolean? IsVoid
        {
            get
            {
                return base.GetSystemBoolean(CompliancePatientFallRiskPreventionMetadata.ColumnNames.IsVoid);
            }

            set
            {
                base.SetSystemBoolean(CompliancePatientFallRiskPreventionMetadata.ColumnNames.IsVoid, value);
            }
        }
        /// <summary>
        /// Maps to CompliancePatientFallRiskPrevention.VoidDateTime
        /// </summary>
        virtual public System.DateTime? VoidDateTime
        {
            get
            {
                return base.GetSystemDateTime(CompliancePatientFallRiskPreventionMetadata.ColumnNames.VoidDateTime);
            }

            set
            {
                base.SetSystemDateTime(CompliancePatientFallRiskPreventionMetadata.ColumnNames.VoidDateTime, value);
            }
        }
        /// <summary>
        /// Maps to CompliancePatientFallRiskPrevention.VoidByUserID
        /// </summary>
        virtual public System.String VoidByUserID
        {
            get
            {
                return base.GetSystemString(CompliancePatientFallRiskPreventionMetadata.ColumnNames.VoidByUserID);
            }

            set
            {
                base.SetSystemString(CompliancePatientFallRiskPreventionMetadata.ColumnNames.VoidByUserID, value);
            }
        }
        /// <summary>
        /// Maps to CompliancePatientFallRiskPrevention.CreatedDateTime
        /// </summary>
        virtual public System.DateTime? CreatedDateTime
        {
            get
            {
                return base.GetSystemDateTime(CompliancePatientFallRiskPreventionMetadata.ColumnNames.CreatedDateTime);
            }

            set
            {
                base.SetSystemDateTime(CompliancePatientFallRiskPreventionMetadata.ColumnNames.CreatedDateTime, value);
            }
        }
        /// <summary>
        /// Maps to CompliancePatientFallRiskPrevention.CreatedByUserID
        /// </summary>
        virtual public System.String CreatedByUserID
        {
            get
            {
                return base.GetSystemString(CompliancePatientFallRiskPreventionMetadata.ColumnNames.CreatedByUserID);
            }

            set
            {
                base.SetSystemString(CompliancePatientFallRiskPreventionMetadata.ColumnNames.CreatedByUserID, value);
            }
        }
        /// <summary>
        /// Maps to CompliancePatientFallRiskPrevention.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(CompliancePatientFallRiskPreventionMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(CompliancePatientFallRiskPreventionMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to CompliancePatientFallRiskPrevention.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(CompliancePatientFallRiskPreventionMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(CompliancePatientFallRiskPreventionMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esCompliancePatientFallRiskPrevention entity)
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
            public System.String ObserverID
            {
                get
                {
                    System.Int32? data = entity.ObserverID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ObserverID = null;
                    else entity.ObserverID = Convert.ToInt32(value);
                }
            }
            public System.String EmployeeID
            {
                get
                {
                    System.Int32? data = entity.EmployeeID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.EmployeeID = null;
                    else entity.EmployeeID = Convert.ToInt32(value);
                }
            }
            public System.String DepartmentID
            {
                get
                {
                    System.Int32? data = entity.DepartmentID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DepartmentID = null;
                    else entity.DepartmentID = Convert.ToInt32(value);
                }
            }
            public System.String DivisionID
            {
                get
                {
                    System.Int32? data = entity.DivisionID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DivisionID = null;
                    else entity.DivisionID = Convert.ToInt32(value);
                }
            }
            public System.String SubDivisionID
            {
                get
                {
                    System.Int32? data = entity.SubDivisionID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SubDivisionID = null;
                    else entity.SubDivisionID = Convert.ToInt32(value);
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
            private esCompliancePatientFallRiskPrevention entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esCompliancePatientFallRiskPreventionQuery query)
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
                throw new Exception("esCompliancePatientFallRiskPrevention can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class CompliancePatientFallRiskPrevention : esCompliancePatientFallRiskPrevention
    {
    }

    [Serializable]
    abstract public class esCompliancePatientFallRiskPreventionQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return CompliancePatientFallRiskPreventionMetadata.Meta();
            }
        }

        public esQueryItem TransactionNo
        {
            get
            {
                return new esQueryItem(this, CompliancePatientFallRiskPreventionMetadata.ColumnNames.TransactionNo, esSystemType.String);
            }
        }

        public esQueryItem TransactionDate
        {
            get
            {
                return new esQueryItem(this, CompliancePatientFallRiskPreventionMetadata.ColumnNames.TransactionDate, esSystemType.DateTime);
            }
        }

        public esQueryItem ObserverID
        {
            get
            {
                return new esQueryItem(this, CompliancePatientFallRiskPreventionMetadata.ColumnNames.ObserverID, esSystemType.Int32);
            }
        }

        public esQueryItem EmployeeID
        {
            get
            {
                return new esQueryItem(this, CompliancePatientFallRiskPreventionMetadata.ColumnNames.EmployeeID, esSystemType.Int32);
            }
        }

        public esQueryItem DepartmentID
        {
            get
            {
                return new esQueryItem(this, CompliancePatientFallRiskPreventionMetadata.ColumnNames.DepartmentID, esSystemType.Int32);
            }
        }

        public esQueryItem DivisionID
        {
            get
            {
                return new esQueryItem(this, CompliancePatientFallRiskPreventionMetadata.ColumnNames.DivisionID, esSystemType.Int32);
            }
        }

        public esQueryItem SubDivisionID
        {
            get
            {
                return new esQueryItem(this, CompliancePatientFallRiskPreventionMetadata.ColumnNames.SubDivisionID, esSystemType.Int32);
            }
        }

        public esQueryItem ServiceUnitID
        {
            get
            {
                return new esQueryItem(this, CompliancePatientFallRiskPreventionMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
            }
        }

        public esQueryItem IsApproved
        {
            get
            {
                return new esQueryItem(this, CompliancePatientFallRiskPreventionMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
            }
        }

        public esQueryItem ApprovedDateTime
        {
            get
            {
                return new esQueryItem(this, CompliancePatientFallRiskPreventionMetadata.ColumnNames.ApprovedDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem ApprovedByUserID
        {
            get
            {
                return new esQueryItem(this, CompliancePatientFallRiskPreventionMetadata.ColumnNames.ApprovedByUserID, esSystemType.String);
            }
        }

        public esQueryItem IsVoid
        {
            get
            {
                return new esQueryItem(this, CompliancePatientFallRiskPreventionMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
            }
        }

        public esQueryItem VoidDateTime
        {
            get
            {
                return new esQueryItem(this, CompliancePatientFallRiskPreventionMetadata.ColumnNames.VoidDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem VoidByUserID
        {
            get
            {
                return new esQueryItem(this, CompliancePatientFallRiskPreventionMetadata.ColumnNames.VoidByUserID, esSystemType.String);
            }
        }

        public esQueryItem CreatedDateTime
        {
            get
            {
                return new esQueryItem(this, CompliancePatientFallRiskPreventionMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem CreatedByUserID
        {
            get
            {
                return new esQueryItem(this, CompliancePatientFallRiskPreventionMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, CompliancePatientFallRiskPreventionMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, CompliancePatientFallRiskPreventionMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("CompliancePatientFallRiskPreventionCollection")]
    public partial class CompliancePatientFallRiskPreventionCollection : esCompliancePatientFallRiskPreventionCollection, IEnumerable<CompliancePatientFallRiskPrevention>
    {
        public CompliancePatientFallRiskPreventionCollection()
        {

        }

        public static implicit operator List<CompliancePatientFallRiskPrevention>(CompliancePatientFallRiskPreventionCollection coll)
        {
            List<CompliancePatientFallRiskPrevention> list = new List<CompliancePatientFallRiskPrevention>();

            foreach (CompliancePatientFallRiskPrevention emp in coll)
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
                return CompliancePatientFallRiskPreventionMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new CompliancePatientFallRiskPreventionQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new CompliancePatientFallRiskPrevention(row);
        }

        override protected esEntity CreateEntity()
        {
            return new CompliancePatientFallRiskPrevention();
        }

        #endregion

        [BrowsableAttribute(false)]
        public CompliancePatientFallRiskPreventionQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new CompliancePatientFallRiskPreventionQuery();
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
        public bool Load(CompliancePatientFallRiskPreventionQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public CompliancePatientFallRiskPrevention AddNew()
        {
            CompliancePatientFallRiskPrevention entity = base.AddNewEntity() as CompliancePatientFallRiskPrevention;

            return entity;
        }
        public CompliancePatientFallRiskPrevention FindByPrimaryKey(String transactionNo)
        {
            return base.FindByPrimaryKey(transactionNo) as CompliancePatientFallRiskPrevention;
        }

        #region IEnumerable< CompliancePatientFallRiskPrevention> Members

        IEnumerator<CompliancePatientFallRiskPrevention> IEnumerable<CompliancePatientFallRiskPrevention>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as CompliancePatientFallRiskPrevention;
            }
        }

        #endregion

        private CompliancePatientFallRiskPreventionQuery query;
    }


    /// <summary>
    /// Encapsulates the 'CompliancePatientFallRiskPrevention' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("CompliancePatientFallRiskPrevention ({TransactionNo})")]
    [Serializable]
    public partial class CompliancePatientFallRiskPrevention : esCompliancePatientFallRiskPrevention
    {
        public CompliancePatientFallRiskPrevention()
        {
        }

        public CompliancePatientFallRiskPrevention(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return CompliancePatientFallRiskPreventionMetadata.Meta();
            }
        }

        override protected esCompliancePatientFallRiskPreventionQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new CompliancePatientFallRiskPreventionQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public CompliancePatientFallRiskPreventionQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new CompliancePatientFallRiskPreventionQuery();
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
        public bool Load(CompliancePatientFallRiskPreventionQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private CompliancePatientFallRiskPreventionQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class CompliancePatientFallRiskPreventionQuery : esCompliancePatientFallRiskPreventionQuery
    {
        public CompliancePatientFallRiskPreventionQuery()
        {

        }

        public CompliancePatientFallRiskPreventionQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "CompliancePatientFallRiskPreventionQuery";
        }
    }

    [Serializable]
    public partial class CompliancePatientFallRiskPreventionMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected CompliancePatientFallRiskPreventionMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(CompliancePatientFallRiskPreventionMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = CompliancePatientFallRiskPreventionMetadata.PropertyNames.TransactionNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(CompliancePatientFallRiskPreventionMetadata.ColumnNames.TransactionDate, 1, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = CompliancePatientFallRiskPreventionMetadata.PropertyNames.TransactionDate;
            _columns.Add(c);

            c = new esColumnMetadata(CompliancePatientFallRiskPreventionMetadata.ColumnNames.ObserverID, 2, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = CompliancePatientFallRiskPreventionMetadata.PropertyNames.ObserverID;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(CompliancePatientFallRiskPreventionMetadata.ColumnNames.EmployeeID, 3, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = CompliancePatientFallRiskPreventionMetadata.PropertyNames.EmployeeID;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(CompliancePatientFallRiskPreventionMetadata.ColumnNames.DepartmentID, 4, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = CompliancePatientFallRiskPreventionMetadata.PropertyNames.DepartmentID;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(CompliancePatientFallRiskPreventionMetadata.ColumnNames.DivisionID, 5, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = CompliancePatientFallRiskPreventionMetadata.PropertyNames.DivisionID;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(CompliancePatientFallRiskPreventionMetadata.ColumnNames.SubDivisionID, 6, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = CompliancePatientFallRiskPreventionMetadata.PropertyNames.SubDivisionID;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(CompliancePatientFallRiskPreventionMetadata.ColumnNames.ServiceUnitID, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = CompliancePatientFallRiskPreventionMetadata.PropertyNames.ServiceUnitID;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(CompliancePatientFallRiskPreventionMetadata.ColumnNames.IsApproved, 8, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = CompliancePatientFallRiskPreventionMetadata.PropertyNames.IsApproved;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(CompliancePatientFallRiskPreventionMetadata.ColumnNames.ApprovedDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = CompliancePatientFallRiskPreventionMetadata.PropertyNames.ApprovedDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(CompliancePatientFallRiskPreventionMetadata.ColumnNames.ApprovedByUserID, 10, typeof(System.String), esSystemType.String);
            c.PropertyName = CompliancePatientFallRiskPreventionMetadata.PropertyNames.ApprovedByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(CompliancePatientFallRiskPreventionMetadata.ColumnNames.IsVoid, 11, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = CompliancePatientFallRiskPreventionMetadata.PropertyNames.IsVoid;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(CompliancePatientFallRiskPreventionMetadata.ColumnNames.VoidDateTime, 12, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = CompliancePatientFallRiskPreventionMetadata.PropertyNames.VoidDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(CompliancePatientFallRiskPreventionMetadata.ColumnNames.VoidByUserID, 13, typeof(System.String), esSystemType.String);
            c.PropertyName = CompliancePatientFallRiskPreventionMetadata.PropertyNames.VoidByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(CompliancePatientFallRiskPreventionMetadata.ColumnNames.CreatedDateTime, 14, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = CompliancePatientFallRiskPreventionMetadata.PropertyNames.CreatedDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(CompliancePatientFallRiskPreventionMetadata.ColumnNames.CreatedByUserID, 15, typeof(System.String), esSystemType.String);
            c.PropertyName = CompliancePatientFallRiskPreventionMetadata.PropertyNames.CreatedByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(CompliancePatientFallRiskPreventionMetadata.ColumnNames.LastUpdateDateTime, 16, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = CompliancePatientFallRiskPreventionMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(CompliancePatientFallRiskPreventionMetadata.ColumnNames.LastUpdateByUserID, 17, typeof(System.String), esSystemType.String);
            c.PropertyName = CompliancePatientFallRiskPreventionMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public CompliancePatientFallRiskPreventionMetadata Meta()
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
            public const string ObserverID = "ObserverID";
            public const string EmployeeID = "EmployeeID";
            public const string DepartmentID = "DepartmentID";
            public const string DivisionID = "DivisionID";
            public const string SubDivisionID = "SubDivisionID";
            public const string ServiceUnitID = "ServiceUnitID";
            public const string IsApproved = "IsApproved";
            public const string ApprovedDateTime = "ApprovedDateTime";
            public const string ApprovedByUserID = "ApprovedByUserID";
            public const string IsVoid = "IsVoid";
            public const string VoidDateTime = "VoidDateTime";
            public const string VoidByUserID = "VoidByUserID";
            public const string CreatedDateTime = "CreatedDateTime";
            public const string CreatedByUserID = "CreatedByUserID";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string TransactionNo = "TransactionNo";
            public const string TransactionDate = "TransactionDate";
            public const string ObserverID = "ObserverID";
            public const string EmployeeID = "EmployeeID";
            public const string DepartmentID = "DepartmentID";
            public const string DivisionID = "DivisionID";
            public const string SubDivisionID = "SubDivisionID";
            public const string ServiceUnitID = "ServiceUnitID";
            public const string IsApproved = "IsApproved";
            public const string ApprovedDateTime = "ApprovedDateTime";
            public const string ApprovedByUserID = "ApprovedByUserID";
            public const string IsVoid = "IsVoid";
            public const string VoidDateTime = "VoidDateTime";
            public const string VoidByUserID = "VoidByUserID";
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
            lock (typeof(CompliancePatientFallRiskPreventionMetadata))
            {
                if (CompliancePatientFallRiskPreventionMetadata.mapDelegates == null)
                {
                    CompliancePatientFallRiskPreventionMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (CompliancePatientFallRiskPreventionMetadata.meta == null)
                {
                    CompliancePatientFallRiskPreventionMetadata.meta = new CompliancePatientFallRiskPreventionMetadata();
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
                meta.AddTypeMap("ObserverID", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("EmployeeID", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("DepartmentID", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("DivisionID", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("SubDivisionID", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("ApprovedDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("ApprovedByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("VoidDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("VoidByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "CompliancePatientFallRiskPrevention";
                meta.Destination = "CompliancePatientFallRiskPrevention";
                meta.spInsert = "proc_CompliancePatientFallRiskPreventionInsert";
                meta.spUpdate = "proc_CompliancePatientFallRiskPreventionUpdate";
                meta.spDelete = "proc_CompliancePatientFallRiskPreventionDelete";
                meta.spLoadAll = "proc_CompliancePatientFallRiskPreventionLoadAll";
                meta.spLoadByPrimaryKey = "proc_CompliancePatientFallRiskPreventionLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private CompliancePatientFallRiskPreventionMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
