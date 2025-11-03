/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/9/2017 12:39:51 PM
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
    abstract public class esEmployeePeriodicStructuralBenefitsCollection : esEntityCollectionWAuditLog
    {
        public esEmployeePeriodicStructuralBenefitsCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "EmployeePeriodicStructuralBenefitsCollection";
        }

        #region Query Logic
        protected void InitQuery(esEmployeePeriodicStructuralBenefitsQuery query)
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
            this.InitQuery(query as esEmployeePeriodicStructuralBenefitsQuery);
        }
        #endregion

        virtual public EmployeePeriodicStructuralBenefits DetachEntity(EmployeePeriodicStructuralBenefits entity)
        {
            return base.DetachEntity(entity) as EmployeePeriodicStructuralBenefits;
        }

        virtual public EmployeePeriodicStructuralBenefits AttachEntity(EmployeePeriodicStructuralBenefits entity)
        {
            return base.AttachEntity(entity) as EmployeePeriodicStructuralBenefits;
        }

        virtual public void Combine(EmployeePeriodicStructuralBenefitsCollection collection)
        {
            base.Combine(collection);
        }

        new public EmployeePeriodicStructuralBenefits this[int index]
        {
            get
            {
                return base[index] as EmployeePeriodicStructuralBenefits;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(EmployeePeriodicStructuralBenefits);
        }
    }

    [Serializable]
    abstract public class esEmployeePeriodicStructuralBenefits : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esEmployeePeriodicStructuralBenefitsQuery GetDynamicQuery()
        {
            return null;
        }

        public esEmployeePeriodicStructuralBenefits()
        {
        }

        public esEmployeePeriodicStructuralBenefits(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(Int32 counterID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(counterID);
            else
                return LoadByPrimaryKeyStoredProcedure(counterID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 counterID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(counterID);
            else
                return LoadByPrimaryKeyStoredProcedure(counterID);
        }

        private bool LoadByPrimaryKeyDynamic(Int32 counterID)
        {
            esEmployeePeriodicStructuralBenefitsQuery query = this.GetDynamicQuery();
            query.Where(query.CounterID == counterID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(Int32 counterID)
        {
            esParameters parms = new esParameters();
            parms.Add("CounterID", counterID);
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
                        case "CounterID": this.str.CounterID = (string)value; break;
                        case "PayrollPeriodID": this.str.PayrollPeriodID = (string)value; break;
                        case "SalaryComponentID": this.str.SalaryComponentID = (string)value; break;
                        case "PersonID": this.str.PersonID = (string)value; break;
                        case "OrganizationUnitID": this.str.OrganizationUnitID = (string)value; break;
                        case "PositionID": this.str.PositionID = (string)value; break;
                        case "SRStructuralBenefitsType": this.str.SRStructuralBenefitsType = (string)value; break;
                        case "Amount": this.str.Amount = (string)value; break;
                        case "IsApproved": this.str.IsApproved = (string)value; break;
                        case "ApprovedDateTime": this.str.ApprovedDateTime = (string)value; break;
                        case "ApprovedByUserID": this.str.ApprovedByUserID = (string)value; break;
                        case "IsVoid": this.str.IsVoid = (string)value; break;
                        case "VoidDateTime": this.str.VoidDateTime = (string)value; break;
                        case "VoidByUserID": this.str.VoidByUserID = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "NumberOfDays": this.str.NumberOfDays = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "CounterID":

                            if (value == null || value is System.Int32)
                                this.CounterID = (System.Int32?)value;
                            break;
                        case "PayrollPeriodID":

                            if (value == null || value is System.Int32)
                                this.PayrollPeriodID = (System.Int32?)value;
                            break;
                        case "SalaryComponentID":

                            if (value == null || value is System.Int32)
                                this.SalaryComponentID = (System.Int32?)value;
                            break;
                        case "PersonID":

                            if (value == null || value is System.Int32)
                                this.PersonID = (System.Int32?)value;
                            break;
                        case "OrganizationUnitID":

                            if (value == null || value is System.Int32)
                                this.OrganizationUnitID = (System.Int32?)value;
                            break;
                        case "PositionID":

                            if (value == null || value is System.Int32)
                                this.PositionID = (System.Int32?)value;
                            break;
                        case "Amount":

                            if (value == null || value is System.Decimal)
                                this.Amount = (System.Decimal?)value;
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
                        case "NumberOfDays":

                            if (value == null || value is System.Decimal)
                                this.NumberOfDays = (System.Decimal?)value;
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
        /// Maps to EmployeePeriodicStructuralBenefits.CounterID
        /// </summary>
        virtual public System.Int32? CounterID
        {
            get
            {
                return base.GetSystemInt32(EmployeePeriodicStructuralBenefitsMetadata.ColumnNames.CounterID);
            }

            set
            {
                base.SetSystemInt32(EmployeePeriodicStructuralBenefitsMetadata.ColumnNames.CounterID, value);
            }
        }
        /// <summary>
        /// Maps to EmployeePeriodicStructuralBenefits.PayrollPeriodID
        /// </summary>
        virtual public System.Int32? PayrollPeriodID
        {
            get
            {
                return base.GetSystemInt32(EmployeePeriodicStructuralBenefitsMetadata.ColumnNames.PayrollPeriodID);
            }

            set
            {
                base.SetSystemInt32(EmployeePeriodicStructuralBenefitsMetadata.ColumnNames.PayrollPeriodID, value);
            }
        }
        /// <summary>
        /// Maps to EmployeePeriodicStructuralBenefits.SalaryComponentID
        /// </summary>
        virtual public System.Int32? SalaryComponentID
        {
            get
            {
                return base.GetSystemInt32(EmployeePeriodicStructuralBenefitsMetadata.ColumnNames.SalaryComponentID);
            }

            set
            {
                base.SetSystemInt32(EmployeePeriodicStructuralBenefitsMetadata.ColumnNames.SalaryComponentID, value);
            }
        }
        /// <summary>
        /// Maps to EmployeePeriodicStructuralBenefits.PersonID
        /// </summary>
        virtual public System.Int32? PersonID
        {
            get
            {
                return base.GetSystemInt32(EmployeePeriodicStructuralBenefitsMetadata.ColumnNames.PersonID);
            }

            set
            {
                base.SetSystemInt32(EmployeePeriodicStructuralBenefitsMetadata.ColumnNames.PersonID, value);
            }
        }
        /// <summary>
        /// Maps to EmployeePeriodicStructuralBenefits.OrganizationUnitID
        /// </summary>
        virtual public System.Int32? OrganizationUnitID
        {
            get
            {
                return base.GetSystemInt32(EmployeePeriodicStructuralBenefitsMetadata.ColumnNames.OrganizationUnitID);
            }

            set
            {
                base.SetSystemInt32(EmployeePeriodicStructuralBenefitsMetadata.ColumnNames.OrganizationUnitID, value);
            }
        }
        /// <summary>
        /// Maps to EmployeePeriodicStructuralBenefits.PositionID
        /// </summary>
        virtual public System.Int32? PositionID
        {
            get
            {
                return base.GetSystemInt32(EmployeePeriodicStructuralBenefitsMetadata.ColumnNames.PositionID);
            }

            set
            {
                base.SetSystemInt32(EmployeePeriodicStructuralBenefitsMetadata.ColumnNames.PositionID, value);
            }
        }
        /// <summary>
        /// Maps to EmployeePeriodicStructuralBenefits.SRStructuralBenefitsType
        /// </summary>
        virtual public System.String SRStructuralBenefitsType
        {
            get
            {
                return base.GetSystemString(EmployeePeriodicStructuralBenefitsMetadata.ColumnNames.SRStructuralBenefitsType);
            }

            set
            {
                base.SetSystemString(EmployeePeriodicStructuralBenefitsMetadata.ColumnNames.SRStructuralBenefitsType, value);
            }
        }
        /// <summary>
        /// Maps to EmployeePeriodicStructuralBenefits.Amount
        /// </summary>
        virtual public System.Decimal? Amount
        {
            get
            {
                return base.GetSystemDecimal(EmployeePeriodicStructuralBenefitsMetadata.ColumnNames.Amount);
            }

            set
            {
                base.SetSystemDecimal(EmployeePeriodicStructuralBenefitsMetadata.ColumnNames.Amount, value);
            }
        }
        /// <summary>
        /// Maps to EmployeePeriodicStructuralBenefits.IsApproved
        /// </summary>
        virtual public System.Boolean? IsApproved
        {
            get
            {
                return base.GetSystemBoolean(EmployeePeriodicStructuralBenefitsMetadata.ColumnNames.IsApproved);
            }

            set
            {
                base.SetSystemBoolean(EmployeePeriodicStructuralBenefitsMetadata.ColumnNames.IsApproved, value);
            }
        }
        /// <summary>
        /// Maps to EmployeePeriodicStructuralBenefits.ApprovedDateTime
        /// </summary>
        virtual public System.DateTime? ApprovedDateTime
        {
            get
            {
                return base.GetSystemDateTime(EmployeePeriodicStructuralBenefitsMetadata.ColumnNames.ApprovedDateTime);
            }

            set
            {
                base.SetSystemDateTime(EmployeePeriodicStructuralBenefitsMetadata.ColumnNames.ApprovedDateTime, value);
            }
        }
        /// <summary>
        /// Maps to EmployeePeriodicStructuralBenefits.ApprovedByUserID
        /// </summary>
        virtual public System.String ApprovedByUserID
        {
            get
            {
                return base.GetSystemString(EmployeePeriodicStructuralBenefitsMetadata.ColumnNames.ApprovedByUserID);
            }

            set
            {
                base.SetSystemString(EmployeePeriodicStructuralBenefitsMetadata.ColumnNames.ApprovedByUserID, value);
            }
        }
        /// <summary>
        /// Maps to EmployeePeriodicStructuralBenefits.IsVoid
        /// </summary>
        virtual public System.Boolean? IsVoid
        {
            get
            {
                return base.GetSystemBoolean(EmployeePeriodicStructuralBenefitsMetadata.ColumnNames.IsVoid);
            }

            set
            {
                base.SetSystemBoolean(EmployeePeriodicStructuralBenefitsMetadata.ColumnNames.IsVoid, value);
            }
        }
        /// <summary>
        /// Maps to EmployeePeriodicStructuralBenefits.VoidDateTime
        /// </summary>
        virtual public System.DateTime? VoidDateTime
        {
            get
            {
                return base.GetSystemDateTime(EmployeePeriodicStructuralBenefitsMetadata.ColumnNames.VoidDateTime);
            }

            set
            {
                base.SetSystemDateTime(EmployeePeriodicStructuralBenefitsMetadata.ColumnNames.VoidDateTime, value);
            }
        }
        /// <summary>
        /// Maps to EmployeePeriodicStructuralBenefits.VoidByUserID
        /// </summary>
        virtual public System.String VoidByUserID
        {
            get
            {
                return base.GetSystemString(EmployeePeriodicStructuralBenefitsMetadata.ColumnNames.VoidByUserID);
            }

            set
            {
                base.SetSystemString(EmployeePeriodicStructuralBenefitsMetadata.ColumnNames.VoidByUserID, value);
            }
        }
        /// <summary>
        /// Maps to EmployeePeriodicStructuralBenefits.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(EmployeePeriodicStructuralBenefitsMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(EmployeePeriodicStructuralBenefitsMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to EmployeePeriodicStructuralBenefits.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(EmployeePeriodicStructuralBenefitsMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(EmployeePeriodicStructuralBenefitsMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to EmployeePeriodicStructuralBenefits.NumberOfDays
        /// </summary>
        virtual public System.Decimal? NumberOfDays
        {
            get
            {
                return base.GetSystemDecimal(EmployeePeriodicStructuralBenefitsMetadata.ColumnNames.NumberOfDays);
            }

            set
            {
                base.SetSystemDecimal(EmployeePeriodicStructuralBenefitsMetadata.ColumnNames.NumberOfDays, value);
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
            public esStrings(esEmployeePeriodicStructuralBenefits entity)
            {
                this.entity = entity;
            }
            public System.String CounterID
            {
                get
                {
                    System.Int32? data = entity.CounterID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CounterID = null;
                    else entity.CounterID = Convert.ToInt32(value);
                }
            }
            public System.String PayrollPeriodID
            {
                get
                {
                    System.Int32? data = entity.PayrollPeriodID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PayrollPeriodID = null;
                    else entity.PayrollPeriodID = Convert.ToInt32(value);
                }
            }
            public System.String SalaryComponentID
            {
                get
                {
                    System.Int32? data = entity.SalaryComponentID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SalaryComponentID = null;
                    else entity.SalaryComponentID = Convert.ToInt32(value);
                }
            }
            public System.String PersonID
            {
                get
                {
                    System.Int32? data = entity.PersonID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PersonID = null;
                    else entity.PersonID = Convert.ToInt32(value);
                }
            }
            public System.String OrganizationUnitID
            {
                get
                {
                    System.Int32? data = entity.OrganizationUnitID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.OrganizationUnitID = null;
                    else entity.OrganizationUnitID = Convert.ToInt32(value);
                }
            }
            public System.String PositionID
            {
                get
                {
                    System.Int32? data = entity.PositionID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PositionID = null;
                    else entity.PositionID = Convert.ToInt32(value);
                }
            }
            public System.String SRStructuralBenefitsType
            {
                get
                {
                    System.String data = entity.SRStructuralBenefitsType;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRStructuralBenefitsType = null;
                    else entity.SRStructuralBenefitsType = Convert.ToString(value);
                }
            }
            public System.String Amount
            {
                get
                {
                    System.Decimal? data = entity.Amount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Amount = null;
                    else entity.Amount = Convert.ToDecimal(value);
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
            public System.String NumberOfDays
            {
                get
                {
                    System.Decimal? data = entity.NumberOfDays;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.NumberOfDays = null;
                    else entity.NumberOfDays = Convert.ToDecimal(value);
                }
            }
            private esEmployeePeriodicStructuralBenefits entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esEmployeePeriodicStructuralBenefitsQuery query)
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
                throw new Exception("esEmployeePeriodicStructuralBenefits can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class EmployeePeriodicStructuralBenefits : esEmployeePeriodicStructuralBenefits
    {
    }

    [Serializable]
    abstract public class esEmployeePeriodicStructuralBenefitsQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return EmployeePeriodicStructuralBenefitsMetadata.Meta();
            }
        }

        public esQueryItem CounterID
        {
            get
            {
                return new esQueryItem(this, EmployeePeriodicStructuralBenefitsMetadata.ColumnNames.CounterID, esSystemType.Int32);
            }
        }

        public esQueryItem PayrollPeriodID
        {
            get
            {
                return new esQueryItem(this, EmployeePeriodicStructuralBenefitsMetadata.ColumnNames.PayrollPeriodID, esSystemType.Int32);
            }
        }

        public esQueryItem SalaryComponentID
        {
            get
            {
                return new esQueryItem(this, EmployeePeriodicStructuralBenefitsMetadata.ColumnNames.SalaryComponentID, esSystemType.Int32);
            }
        }

        public esQueryItem PersonID
        {
            get
            {
                return new esQueryItem(this, EmployeePeriodicStructuralBenefitsMetadata.ColumnNames.PersonID, esSystemType.Int32);
            }
        }

        public esQueryItem OrganizationUnitID
        {
            get
            {
                return new esQueryItem(this, EmployeePeriodicStructuralBenefitsMetadata.ColumnNames.OrganizationUnitID, esSystemType.Int32);
            }
        }

        public esQueryItem PositionID
        {
            get
            {
                return new esQueryItem(this, EmployeePeriodicStructuralBenefitsMetadata.ColumnNames.PositionID, esSystemType.Int32);
            }
        }

        public esQueryItem SRStructuralBenefitsType
        {
            get
            {
                return new esQueryItem(this, EmployeePeriodicStructuralBenefitsMetadata.ColumnNames.SRStructuralBenefitsType, esSystemType.String);
            }
        }

        public esQueryItem Amount
        {
            get
            {
                return new esQueryItem(this, EmployeePeriodicStructuralBenefitsMetadata.ColumnNames.Amount, esSystemType.Decimal);
            }
        }

        public esQueryItem IsApproved
        {
            get
            {
                return new esQueryItem(this, EmployeePeriodicStructuralBenefitsMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
            }
        }

        public esQueryItem ApprovedDateTime
        {
            get
            {
                return new esQueryItem(this, EmployeePeriodicStructuralBenefitsMetadata.ColumnNames.ApprovedDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem ApprovedByUserID
        {
            get
            {
                return new esQueryItem(this, EmployeePeriodicStructuralBenefitsMetadata.ColumnNames.ApprovedByUserID, esSystemType.String);
            }
        }

        public esQueryItem IsVoid
        {
            get
            {
                return new esQueryItem(this, EmployeePeriodicStructuralBenefitsMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
            }
        }

        public esQueryItem VoidDateTime
        {
            get
            {
                return new esQueryItem(this, EmployeePeriodicStructuralBenefitsMetadata.ColumnNames.VoidDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem VoidByUserID
        {
            get
            {
                return new esQueryItem(this, EmployeePeriodicStructuralBenefitsMetadata.ColumnNames.VoidByUserID, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, EmployeePeriodicStructuralBenefitsMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, EmployeePeriodicStructuralBenefitsMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem NumberOfDays
        {
            get
            {
                return new esQueryItem(this, EmployeePeriodicStructuralBenefitsMetadata.ColumnNames.NumberOfDays, esSystemType.Decimal);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("EmployeePeriodicStructuralBenefitsCollection")]
    public partial class EmployeePeriodicStructuralBenefitsCollection : esEmployeePeriodicStructuralBenefitsCollection, IEnumerable<EmployeePeriodicStructuralBenefits>
    {
        public EmployeePeriodicStructuralBenefitsCollection()
        {

        }

        public static implicit operator List<EmployeePeriodicStructuralBenefits>(EmployeePeriodicStructuralBenefitsCollection coll)
        {
            List<EmployeePeriodicStructuralBenefits> list = new List<EmployeePeriodicStructuralBenefits>();

            foreach (EmployeePeriodicStructuralBenefits emp in coll)
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
                return EmployeePeriodicStructuralBenefitsMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new EmployeePeriodicStructuralBenefitsQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new EmployeePeriodicStructuralBenefits(row);
        }

        override protected esEntity CreateEntity()
        {
            return new EmployeePeriodicStructuralBenefits();
        }

        #endregion

        [BrowsableAttribute(false)]
        public EmployeePeriodicStructuralBenefitsQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new EmployeePeriodicStructuralBenefitsQuery();
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
        public bool Load(EmployeePeriodicStructuralBenefitsQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public EmployeePeriodicStructuralBenefits AddNew()
        {
            EmployeePeriodicStructuralBenefits entity = base.AddNewEntity() as EmployeePeriodicStructuralBenefits;

            return entity;
        }
        public EmployeePeriodicStructuralBenefits FindByPrimaryKey(Int32 counterID)
        {
            return base.FindByPrimaryKey(counterID) as EmployeePeriodicStructuralBenefits;
        }

        #region IEnumerable< EmployeePeriodicStructuralBenefits> Members

        IEnumerator<EmployeePeriodicStructuralBenefits> IEnumerable<EmployeePeriodicStructuralBenefits>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as EmployeePeriodicStructuralBenefits;
            }
        }

        #endregion

        private EmployeePeriodicStructuralBenefitsQuery query;
    }


    /// <summary>
    /// Encapsulates the 'EmployeePeriodicStructuralBenefits' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("EmployeePeriodicStructuralBenefits ({CounterID})")]
    [Serializable]
    public partial class EmployeePeriodicStructuralBenefits : esEmployeePeriodicStructuralBenefits
    {
        public EmployeePeriodicStructuralBenefits()
        {
        }

        public EmployeePeriodicStructuralBenefits(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return EmployeePeriodicStructuralBenefitsMetadata.Meta();
            }
        }

        override protected esEmployeePeriodicStructuralBenefitsQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new EmployeePeriodicStructuralBenefitsQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public EmployeePeriodicStructuralBenefitsQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new EmployeePeriodicStructuralBenefitsQuery();
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
        public bool Load(EmployeePeriodicStructuralBenefitsQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private EmployeePeriodicStructuralBenefitsQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class EmployeePeriodicStructuralBenefitsQuery : esEmployeePeriodicStructuralBenefitsQuery
    {
        public EmployeePeriodicStructuralBenefitsQuery()
        {

        }

        public EmployeePeriodicStructuralBenefitsQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "EmployeePeriodicStructuralBenefitsQuery";
        }
    }

    [Serializable]
    public partial class EmployeePeriodicStructuralBenefitsMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected EmployeePeriodicStructuralBenefitsMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(EmployeePeriodicStructuralBenefitsMetadata.ColumnNames.CounterID, 0, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = EmployeePeriodicStructuralBenefitsMetadata.PropertyNames.CounterID;
            c.IsInPrimaryKey = true;
            c.IsAutoIncrement = true;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(EmployeePeriodicStructuralBenefitsMetadata.ColumnNames.PayrollPeriodID, 1, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = EmployeePeriodicStructuralBenefitsMetadata.PropertyNames.PayrollPeriodID;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(EmployeePeriodicStructuralBenefitsMetadata.ColumnNames.SalaryComponentID, 2, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = EmployeePeriodicStructuralBenefitsMetadata.PropertyNames.SalaryComponentID;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(EmployeePeriodicStructuralBenefitsMetadata.ColumnNames.PersonID, 3, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = EmployeePeriodicStructuralBenefitsMetadata.PropertyNames.PersonID;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(EmployeePeriodicStructuralBenefitsMetadata.ColumnNames.OrganizationUnitID, 4, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = EmployeePeriodicStructuralBenefitsMetadata.PropertyNames.OrganizationUnitID;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(EmployeePeriodicStructuralBenefitsMetadata.ColumnNames.PositionID, 5, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = EmployeePeriodicStructuralBenefitsMetadata.PropertyNames.PositionID;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(EmployeePeriodicStructuralBenefitsMetadata.ColumnNames.SRStructuralBenefitsType, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = EmployeePeriodicStructuralBenefitsMetadata.PropertyNames.SRStructuralBenefitsType;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(EmployeePeriodicStructuralBenefitsMetadata.ColumnNames.Amount, 7, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = EmployeePeriodicStructuralBenefitsMetadata.PropertyNames.Amount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(EmployeePeriodicStructuralBenefitsMetadata.ColumnNames.IsApproved, 8, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = EmployeePeriodicStructuralBenefitsMetadata.PropertyNames.IsApproved;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(EmployeePeriodicStructuralBenefitsMetadata.ColumnNames.ApprovedDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = EmployeePeriodicStructuralBenefitsMetadata.PropertyNames.ApprovedDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(EmployeePeriodicStructuralBenefitsMetadata.ColumnNames.ApprovedByUserID, 10, typeof(System.String), esSystemType.String);
            c.PropertyName = EmployeePeriodicStructuralBenefitsMetadata.PropertyNames.ApprovedByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(EmployeePeriodicStructuralBenefitsMetadata.ColumnNames.IsVoid, 11, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = EmployeePeriodicStructuralBenefitsMetadata.PropertyNames.IsVoid;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(EmployeePeriodicStructuralBenefitsMetadata.ColumnNames.VoidDateTime, 12, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = EmployeePeriodicStructuralBenefitsMetadata.PropertyNames.VoidDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(EmployeePeriodicStructuralBenefitsMetadata.ColumnNames.VoidByUserID, 13, typeof(System.String), esSystemType.String);
            c.PropertyName = EmployeePeriodicStructuralBenefitsMetadata.PropertyNames.VoidByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(EmployeePeriodicStructuralBenefitsMetadata.ColumnNames.LastUpdateDateTime, 14, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = EmployeePeriodicStructuralBenefitsMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(EmployeePeriodicStructuralBenefitsMetadata.ColumnNames.LastUpdateByUserID, 15, typeof(System.String), esSystemType.String);
            c.PropertyName = EmployeePeriodicStructuralBenefitsMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(EmployeePeriodicStructuralBenefitsMetadata.ColumnNames.NumberOfDays, 16, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = EmployeePeriodicStructuralBenefitsMetadata.PropertyNames.NumberOfDays;
            c.NumericPrecision = 5;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public EmployeePeriodicStructuralBenefitsMetadata Meta()
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
            public const string CounterID = "CounterID";
            public const string PayrollPeriodID = "PayrollPeriodID";
            public const string SalaryComponentID = "SalaryComponentID";
            public const string PersonID = "PersonID";
            public const string OrganizationUnitID = "OrganizationUnitID";
            public const string PositionID = "PositionID";
            public const string SRStructuralBenefitsType = "SRStructuralBenefitsType";
            public const string Amount = "Amount";
            public const string IsApproved = "IsApproved";
            public const string ApprovedDateTime = "ApprovedDateTime";
            public const string ApprovedByUserID = "ApprovedByUserID";
            public const string IsVoid = "IsVoid";
            public const string VoidDateTime = "VoidDateTime";
            public const string VoidByUserID = "VoidByUserID";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string NumberOfDays = "NumberOfDays";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string CounterID = "CounterID";
            public const string PayrollPeriodID = "PayrollPeriodID";
            public const string SalaryComponentID = "SalaryComponentID";
            public const string PersonID = "PersonID";
            public const string OrganizationUnitID = "OrganizationUnitID";
            public const string PositionID = "PositionID";
            public const string SRStructuralBenefitsType = "SRStructuralBenefitsType";
            public const string Amount = "Amount";
            public const string IsApproved = "IsApproved";
            public const string ApprovedDateTime = "ApprovedDateTime";
            public const string ApprovedByUserID = "ApprovedByUserID";
            public const string IsVoid = "IsVoid";
            public const string VoidDateTime = "VoidDateTime";
            public const string VoidByUserID = "VoidByUserID";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string NumberOfDays = "NumberOfDays";
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
            lock (typeof(EmployeePeriodicStructuralBenefitsMetadata))
            {
                if (EmployeePeriodicStructuralBenefitsMetadata.mapDelegates == null)
                {
                    EmployeePeriodicStructuralBenefitsMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (EmployeePeriodicStructuralBenefitsMetadata.meta == null)
                {
                    EmployeePeriodicStructuralBenefitsMetadata.meta = new EmployeePeriodicStructuralBenefitsMetadata();
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

                meta.AddTypeMap("CounterID", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("PayrollPeriodID", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("SalaryComponentID", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("OrganizationUnitID", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("PositionID", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("SRStructuralBenefitsType", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Amount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("ApprovedDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("ApprovedByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("VoidDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("VoidByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("NumberOfDays", new esTypeMap("numeric", "System.Decimal"));


                meta.Source = "EmployeePeriodicStructuralBenefits";
                meta.Destination = "EmployeePeriodicStructuralBenefits";
                meta.spInsert = "proc_EmployeePeriodicStructuralBenefitsInsert";
                meta.spUpdate = "proc_EmployeePeriodicStructuralBenefitsUpdate";
                meta.spDelete = "proc_EmployeePeriodicStructuralBenefitsDelete";
                meta.spLoadAll = "proc_EmployeePeriodicStructuralBenefitsLoadAll";
                meta.spLoadByPrimaryKey = "proc_EmployeePeriodicStructuralBenefitsLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private EmployeePeriodicStructuralBenefitsMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
