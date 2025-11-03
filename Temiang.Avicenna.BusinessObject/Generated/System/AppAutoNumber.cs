/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 1/31/2017 11:57:26 AM
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
    abstract public class esAppAutoNumberCollection : esEntityCollectionWAuditLog
    {
        public esAppAutoNumberCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "AppAutoNumberCollection";
        }

        #region Query Logic
        protected void InitQuery(esAppAutoNumberQuery query)
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
            this.InitQuery(query as esAppAutoNumberQuery);
        }
        #endregion

        virtual public AppAutoNumber DetachEntity(AppAutoNumber entity)
        {
            return base.DetachEntity(entity) as AppAutoNumber;
        }

        virtual public AppAutoNumber AttachEntity(AppAutoNumber entity)
        {
            return base.AttachEntity(entity) as AppAutoNumber;
        }

        virtual public void Combine(AppAutoNumberCollection collection)
        {
            base.Combine(collection);
        }

        new public AppAutoNumber this[int index]
        {
            get
            {
                return base[index] as AppAutoNumber;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(AppAutoNumber);
        }
    }

    [Serializable]
    abstract public class esAppAutoNumber : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esAppAutoNumberQuery GetDynamicQuery()
        {
            return null;
        }

        public esAppAutoNumber()
        {
        }

        public esAppAutoNumber(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String sRAutoNumber, DateTime effectiveDate)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(sRAutoNumber, effectiveDate);
            else
                return LoadByPrimaryKeyStoredProcedure(sRAutoNumber, effectiveDate);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String sRAutoNumber, DateTime effectiveDate)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(sRAutoNumber, effectiveDate);
            else
                return LoadByPrimaryKeyStoredProcedure(sRAutoNumber, effectiveDate);
        }

        private bool LoadByPrimaryKeyDynamic(String sRAutoNumber, DateTime effectiveDate)
        {
            esAppAutoNumberQuery query = this.GetDynamicQuery();
            query.Where(query.SRAutoNumber == sRAutoNumber, query.EffectiveDate == effectiveDate);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String sRAutoNumber, DateTime effectiveDate)
        {
            esParameters parms = new esParameters();
            parms.Add("SRAutoNumber", sRAutoNumber);
            parms.Add("EffectiveDate", effectiveDate);
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
                        case "SRAutoNumber": this.str.SRAutoNumber = (string)value; break;
                        case "EffectiveDate": this.str.EffectiveDate = (string)value; break;
                        case "Prefik": this.str.Prefik = (string)value; break;
                        case "SeparatorAfterPrefik": this.str.SeparatorAfterPrefik = (string)value; break;
                        case "IsUsedDepartment": this.str.IsUsedDepartment = (string)value; break;
                        case "SeparatorAfterDept": this.str.SeparatorAfterDept = (string)value; break;
                        case "IsUsedYear": this.str.IsUsedYear = (string)value; break;
                        case "YearDigit": this.str.YearDigit = (string)value; break;
                        case "SeparatorAfterYear": this.str.SeparatorAfterYear = (string)value; break;
                        case "IsUsedMonth": this.str.IsUsedMonth = (string)value; break;
                        case "IsMonthInRomawi": this.str.IsMonthInRomawi = (string)value; break;
                        case "SeparatorAfterMonth": this.str.SeparatorAfterMonth = (string)value; break;
                        case "IsUsedDay": this.str.IsUsedDay = (string)value; break;
                        case "SeparatorAfterDay": this.str.SeparatorAfterDay = (string)value; break;
                        case "NumberLength": this.str.NumberLength = (string)value; break;
                        case "NumberGroupLength": this.str.NumberGroupLength = (string)value; break;
                        case "NumberGroupSeparator": this.str.NumberGroupSeparator = (string)value; break;
                        case "NumberFormat": this.str.NumberFormat = (string)value; break;
                        case "SeparatorAfterNumber": this.str.SeparatorAfterNumber = (string)value; break;
                        case "IsUsedYearToDateOrder": this.str.IsUsedYearToDateOrder = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "EffectiveDate":

                            if (value == null || value is System.DateTime)
                                this.EffectiveDate = (System.DateTime?)value;
                            break;
                        case "IsUsedDepartment":

                            if (value == null || value is System.Boolean)
                                this.IsUsedDepartment = (System.Boolean?)value;
                            break;
                        case "IsUsedYear":

                            if (value == null || value is System.Boolean)
                                this.IsUsedYear = (System.Boolean?)value;
                            break;
                        case "YearDigit":

                            if (value == null || value is System.Byte)
                                this.YearDigit = (System.Byte?)value;
                            break;
                        case "IsUsedMonth":

                            if (value == null || value is System.Boolean)
                                this.IsUsedMonth = (System.Boolean?)value;
                            break;
                        case "IsMonthInRomawi":

                            if (value == null || value is System.Boolean)
                                this.IsMonthInRomawi = (System.Boolean?)value;
                            break;
                        case "IsUsedDay":

                            if (value == null || value is System.Boolean)
                                this.IsUsedDay = (System.Boolean?)value;
                            break;
                        case "NumberLength":

                            if (value == null || value is System.Byte)
                                this.NumberLength = (System.Byte?)value;
                            break;
                        case "NumberGroupLength":

                            if (value == null || value is System.Byte)
                                this.NumberGroupLength = (System.Byte?)value;
                            break;
                        case "IsUsedYearToDateOrder":

                            if (value == null || value is System.Boolean)
                                this.IsUsedYearToDateOrder = (System.Boolean?)value;
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
        /// Maps to AppAutoNumber.SRAutoNumber
        /// </summary>
        virtual public System.String SRAutoNumber
        {
            get
            {
                return base.GetSystemString(AppAutoNumberMetadata.ColumnNames.SRAutoNumber);
            }

            set
            {
                base.SetSystemString(AppAutoNumberMetadata.ColumnNames.SRAutoNumber, value);
            }
        }
        /// <summary>
        /// Maps to AppAutoNumber.EffectiveDate
        /// </summary>
        virtual public System.DateTime? EffectiveDate
        {
            get
            {
                return base.GetSystemDateTime(AppAutoNumberMetadata.ColumnNames.EffectiveDate);
            }

            set
            {
                base.SetSystemDateTime(AppAutoNumberMetadata.ColumnNames.EffectiveDate, value);
            }
        }
        /// <summary>
        /// Maps to AppAutoNumber.Prefik
        /// </summary>
        virtual public System.String Prefik
        {
            get
            {
                return base.GetSystemString(AppAutoNumberMetadata.ColumnNames.Prefik);
            }

            set
            {
                base.SetSystemString(AppAutoNumberMetadata.ColumnNames.Prefik, value);
            }
        }
        /// <summary>
        /// Maps to AppAutoNumber.SeparatorAfterPrefik
        /// </summary>
        virtual public System.String SeparatorAfterPrefik
        {
            get
            {
                return base.GetSystemString(AppAutoNumberMetadata.ColumnNames.SeparatorAfterPrefik);
            }

            set
            {
                base.SetSystemString(AppAutoNumberMetadata.ColumnNames.SeparatorAfterPrefik, value);
            }
        }
        /// <summary>
        /// Maps to AppAutoNumber.IsUsedDepartment
        /// </summary>
        virtual public System.Boolean? IsUsedDepartment
        {
            get
            {
                return base.GetSystemBoolean(AppAutoNumberMetadata.ColumnNames.IsUsedDepartment);
            }

            set
            {
                base.SetSystemBoolean(AppAutoNumberMetadata.ColumnNames.IsUsedDepartment, value);
            }
        }
        /// <summary>
        /// Maps to AppAutoNumber.SeparatorAfterDept
        /// </summary>
        virtual public System.String SeparatorAfterDept
        {
            get
            {
                return base.GetSystemString(AppAutoNumberMetadata.ColumnNames.SeparatorAfterDept);
            }

            set
            {
                base.SetSystemString(AppAutoNumberMetadata.ColumnNames.SeparatorAfterDept, value);
            }
        }
        /// <summary>
        /// Maps to AppAutoNumber.IsUsedYear
        /// </summary>
        virtual public System.Boolean? IsUsedYear
        {
            get
            {
                return base.GetSystemBoolean(AppAutoNumberMetadata.ColumnNames.IsUsedYear);
            }

            set
            {
                base.SetSystemBoolean(AppAutoNumberMetadata.ColumnNames.IsUsedYear, value);
            }
        }
        /// <summary>
        /// Maps to AppAutoNumber.YearDigit
        /// </summary>
        virtual public System.Byte? YearDigit
        {
            get
            {
                return base.GetSystemByte(AppAutoNumberMetadata.ColumnNames.YearDigit);
            }

            set
            {
                base.SetSystemByte(AppAutoNumberMetadata.ColumnNames.YearDigit, value);
            }
        }
        /// <summary>
        /// Maps to AppAutoNumber.SeparatorAfterYear
        /// </summary>
        virtual public System.String SeparatorAfterYear
        {
            get
            {
                return base.GetSystemString(AppAutoNumberMetadata.ColumnNames.SeparatorAfterYear);
            }

            set
            {
                base.SetSystemString(AppAutoNumberMetadata.ColumnNames.SeparatorAfterYear, value);
            }
        }
        /// <summary>
        /// Maps to AppAutoNumber.IsUsedMonth
        /// </summary>
        virtual public System.Boolean? IsUsedMonth
        {
            get
            {
                return base.GetSystemBoolean(AppAutoNumberMetadata.ColumnNames.IsUsedMonth);
            }

            set
            {
                base.SetSystemBoolean(AppAutoNumberMetadata.ColumnNames.IsUsedMonth, value);
            }
        }
        /// <summary>
        /// Maps to AppAutoNumber.IsMonthInRomawi
        /// </summary>
        virtual public System.Boolean? IsMonthInRomawi
        {
            get
            {
                return base.GetSystemBoolean(AppAutoNumberMetadata.ColumnNames.IsMonthInRomawi);
            }

            set
            {
                base.SetSystemBoolean(AppAutoNumberMetadata.ColumnNames.IsMonthInRomawi, value);
            }
        }
        /// <summary>
        /// Maps to AppAutoNumber.SeparatorAfterMonth
        /// </summary>
        virtual public System.String SeparatorAfterMonth
        {
            get
            {
                return base.GetSystemString(AppAutoNumberMetadata.ColumnNames.SeparatorAfterMonth);
            }

            set
            {
                base.SetSystemString(AppAutoNumberMetadata.ColumnNames.SeparatorAfterMonth, value);
            }
        }
        /// <summary>
        /// Maps to AppAutoNumber.IsUsedDay
        /// </summary>
        virtual public System.Boolean? IsUsedDay
        {
            get
            {
                return base.GetSystemBoolean(AppAutoNumberMetadata.ColumnNames.IsUsedDay);
            }

            set
            {
                base.SetSystemBoolean(AppAutoNumberMetadata.ColumnNames.IsUsedDay, value);
            }
        }
        /// <summary>
        /// Maps to AppAutoNumber.SeparatorAfterDay
        /// </summary>
        virtual public System.String SeparatorAfterDay
        {
            get
            {
                return base.GetSystemString(AppAutoNumberMetadata.ColumnNames.SeparatorAfterDay);
            }

            set
            {
                base.SetSystemString(AppAutoNumberMetadata.ColumnNames.SeparatorAfterDay, value);
            }
        }
        /// <summary>
        /// Maps to AppAutoNumber.NumberLength
        /// </summary>
        virtual public System.Byte? NumberLength
        {
            get
            {
                return base.GetSystemByte(AppAutoNumberMetadata.ColumnNames.NumberLength);
            }

            set
            {
                base.SetSystemByte(AppAutoNumberMetadata.ColumnNames.NumberLength, value);
            }
        }
        /// <summary>
        /// Maps to AppAutoNumber.NumberGroupLength
        /// </summary>
        virtual public System.Byte? NumberGroupLength
        {
            get
            {
                return base.GetSystemByte(AppAutoNumberMetadata.ColumnNames.NumberGroupLength);
            }

            set
            {
                base.SetSystemByte(AppAutoNumberMetadata.ColumnNames.NumberGroupLength, value);
            }
        }
        /// <summary>
        /// Maps to AppAutoNumber.NumberGroupSeparator
        /// </summary>
        virtual public System.String NumberGroupSeparator
        {
            get
            {
                return base.GetSystemString(AppAutoNumberMetadata.ColumnNames.NumberGroupSeparator);
            }

            set
            {
                base.SetSystemString(AppAutoNumberMetadata.ColumnNames.NumberGroupSeparator, value);
            }
        }
        /// <summary>
        /// Maps to AppAutoNumber.NumberFormat
        /// </summary>
        virtual public System.String NumberFormat
        {
            get
            {
                return base.GetSystemString(AppAutoNumberMetadata.ColumnNames.NumberFormat);
            }

            set
            {
                base.SetSystemString(AppAutoNumberMetadata.ColumnNames.NumberFormat, value);
            }
        }
        /// <summary>
        /// Maps to AppAutoNumber.SeparatorAfterNumber
        /// </summary>
        virtual public System.String SeparatorAfterNumber
        {
            get
            {
                return base.GetSystemString(AppAutoNumberMetadata.ColumnNames.SeparatorAfterNumber);
            }

            set
            {
                base.SetSystemString(AppAutoNumberMetadata.ColumnNames.SeparatorAfterNumber, value);
            }
        }
        /// <summary>
        /// Maps to AppAutoNumber.IsUsedYearToDateOrder
        /// </summary>
        virtual public System.Boolean? IsUsedYearToDateOrder
        {
            get
            {
                return base.GetSystemBoolean(AppAutoNumberMetadata.ColumnNames.IsUsedYearToDateOrder);
            }

            set
            {
                base.SetSystemBoolean(AppAutoNumberMetadata.ColumnNames.IsUsedYearToDateOrder, value);
            }
        }
        /// <summary>
        /// Maps to AppAutoNumber.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(AppAutoNumberMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(AppAutoNumberMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to AppAutoNumber.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(AppAutoNumberMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(AppAutoNumberMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esAppAutoNumber entity)
            {
                this.entity = entity;
            }
            public System.String SRAutoNumber
            {
                get
                {
                    System.String data = entity.SRAutoNumber;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRAutoNumber = null;
                    else entity.SRAutoNumber = Convert.ToString(value);
                }
            }
            public System.String EffectiveDate
            {
                get
                {
                    System.DateTime? data = entity.EffectiveDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.EffectiveDate = null;
                    else entity.EffectiveDate = Convert.ToDateTime(value);
                }
            }
            public System.String Prefik
            {
                get
                {
                    System.String data = entity.Prefik;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Prefik = null;
                    else entity.Prefik = Convert.ToString(value);
                }
            }
            public System.String SeparatorAfterPrefik
            {
                get
                {
                    System.String data = entity.SeparatorAfterPrefik;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SeparatorAfterPrefik = null;
                    else entity.SeparatorAfterPrefik = Convert.ToString(value);
                }
            }
            public System.String IsUsedDepartment
            {
                get
                {
                    System.Boolean? data = entity.IsUsedDepartment;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsUsedDepartment = null;
                    else entity.IsUsedDepartment = Convert.ToBoolean(value);
                }
            }
            public System.String SeparatorAfterDept
            {
                get
                {
                    System.String data = entity.SeparatorAfterDept;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SeparatorAfterDept = null;
                    else entity.SeparatorAfterDept = Convert.ToString(value);
                }
            }
            public System.String IsUsedYear
            {
                get
                {
                    System.Boolean? data = entity.IsUsedYear;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsUsedYear = null;
                    else entity.IsUsedYear = Convert.ToBoolean(value);
                }
            }
            public System.String YearDigit
            {
                get
                {
                    System.Byte? data = entity.YearDigit;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.YearDigit = null;
                    else entity.YearDigit = Convert.ToByte(value);
                }
            }
            public System.String SeparatorAfterYear
            {
                get
                {
                    System.String data = entity.SeparatorAfterYear;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SeparatorAfterYear = null;
                    else entity.SeparatorAfterYear = Convert.ToString(value);
                }
            }
            public System.String IsUsedMonth
            {
                get
                {
                    System.Boolean? data = entity.IsUsedMonth;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsUsedMonth = null;
                    else entity.IsUsedMonth = Convert.ToBoolean(value);
                }
            }
            public System.String IsMonthInRomawi
            {
                get
                {
                    System.Boolean? data = entity.IsMonthInRomawi;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsMonthInRomawi = null;
                    else entity.IsMonthInRomawi = Convert.ToBoolean(value);
                }
            }
            public System.String SeparatorAfterMonth
            {
                get
                {
                    System.String data = entity.SeparatorAfterMonth;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SeparatorAfterMonth = null;
                    else entity.SeparatorAfterMonth = Convert.ToString(value);
                }
            }
            public System.String IsUsedDay
            {
                get
                {
                    System.Boolean? data = entity.IsUsedDay;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsUsedDay = null;
                    else entity.IsUsedDay = Convert.ToBoolean(value);
                }
            }
            public System.String SeparatorAfterDay
            {
                get
                {
                    System.String data = entity.SeparatorAfterDay;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SeparatorAfterDay = null;
                    else entity.SeparatorAfterDay = Convert.ToString(value);
                }
            }
            public System.String NumberLength
            {
                get
                {
                    System.Byte? data = entity.NumberLength;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.NumberLength = null;
                    else entity.NumberLength = Convert.ToByte(value);
                }
            }
            public System.String NumberGroupLength
            {
                get
                {
                    System.Byte? data = entity.NumberGroupLength;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.NumberGroupLength = null;
                    else entity.NumberGroupLength = Convert.ToByte(value);
                }
            }
            public System.String NumberGroupSeparator
            {
                get
                {
                    System.String data = entity.NumberGroupSeparator;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.NumberGroupSeparator = null;
                    else entity.NumberGroupSeparator = Convert.ToString(value);
                }
            }
            public System.String NumberFormat
            {
                get
                {
                    System.String data = entity.NumberFormat;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.NumberFormat = null;
                    else entity.NumberFormat = Convert.ToString(value);
                }
            }
            public System.String SeparatorAfterNumber
            {
                get
                {
                    System.String data = entity.SeparatorAfterNumber;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SeparatorAfterNumber = null;
                    else entity.SeparatorAfterNumber = Convert.ToString(value);
                }
            }
            public System.String IsUsedYearToDateOrder
            {
                get
                {
                    System.Boolean? data = entity.IsUsedYearToDateOrder;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsUsedYearToDateOrder = null;
                    else entity.IsUsedYearToDateOrder = Convert.ToBoolean(value);
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
            private esAppAutoNumber entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esAppAutoNumberQuery query)
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
                throw new Exception("esAppAutoNumber can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class AppAutoNumber : esAppAutoNumber
    {
    }

    [Serializable]
    abstract public class esAppAutoNumberQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return AppAutoNumberMetadata.Meta();
            }
        }

        public esQueryItem SRAutoNumber
        {
            get
            {
                return new esQueryItem(this, AppAutoNumberMetadata.ColumnNames.SRAutoNumber, esSystemType.String);
            }
        }

        public esQueryItem EffectiveDate
        {
            get
            {
                return new esQueryItem(this, AppAutoNumberMetadata.ColumnNames.EffectiveDate, esSystemType.DateTime);
            }
        }

        public esQueryItem Prefik
        {
            get
            {
                return new esQueryItem(this, AppAutoNumberMetadata.ColumnNames.Prefik, esSystemType.String);
            }
        }

        public esQueryItem SeparatorAfterPrefik
        {
            get
            {
                return new esQueryItem(this, AppAutoNumberMetadata.ColumnNames.SeparatorAfterPrefik, esSystemType.String);
            }
        }

        public esQueryItem IsUsedDepartment
        {
            get
            {
                return new esQueryItem(this, AppAutoNumberMetadata.ColumnNames.IsUsedDepartment, esSystemType.Boolean);
            }
        }

        public esQueryItem SeparatorAfterDept
        {
            get
            {
                return new esQueryItem(this, AppAutoNumberMetadata.ColumnNames.SeparatorAfterDept, esSystemType.String);
            }
        }

        public esQueryItem IsUsedYear
        {
            get
            {
                return new esQueryItem(this, AppAutoNumberMetadata.ColumnNames.IsUsedYear, esSystemType.Boolean);
            }
        }

        public esQueryItem YearDigit
        {
            get
            {
                return new esQueryItem(this, AppAutoNumberMetadata.ColumnNames.YearDigit, esSystemType.Byte);
            }
        }

        public esQueryItem SeparatorAfterYear
        {
            get
            {
                return new esQueryItem(this, AppAutoNumberMetadata.ColumnNames.SeparatorAfterYear, esSystemType.String);
            }
        }

        public esQueryItem IsUsedMonth
        {
            get
            {
                return new esQueryItem(this, AppAutoNumberMetadata.ColumnNames.IsUsedMonth, esSystemType.Boolean);
            }
        }

        public esQueryItem IsMonthInRomawi
        {
            get
            {
                return new esQueryItem(this, AppAutoNumberMetadata.ColumnNames.IsMonthInRomawi, esSystemType.Boolean);
            }
        }

        public esQueryItem SeparatorAfterMonth
        {
            get
            {
                return new esQueryItem(this, AppAutoNumberMetadata.ColumnNames.SeparatorAfterMonth, esSystemType.String);
            }
        }

        public esQueryItem IsUsedDay
        {
            get
            {
                return new esQueryItem(this, AppAutoNumberMetadata.ColumnNames.IsUsedDay, esSystemType.Boolean);
            }
        }

        public esQueryItem SeparatorAfterDay
        {
            get
            {
                return new esQueryItem(this, AppAutoNumberMetadata.ColumnNames.SeparatorAfterDay, esSystemType.String);
            }
        }

        public esQueryItem NumberLength
        {
            get
            {
                return new esQueryItem(this, AppAutoNumberMetadata.ColumnNames.NumberLength, esSystemType.Byte);
            }
        }

        public esQueryItem NumberGroupLength
        {
            get
            {
                return new esQueryItem(this, AppAutoNumberMetadata.ColumnNames.NumberGroupLength, esSystemType.Byte);
            }
        }

        public esQueryItem NumberGroupSeparator
        {
            get
            {
                return new esQueryItem(this, AppAutoNumberMetadata.ColumnNames.NumberGroupSeparator, esSystemType.String);
            }
        }

        public esQueryItem NumberFormat
        {
            get
            {
                return new esQueryItem(this, AppAutoNumberMetadata.ColumnNames.NumberFormat, esSystemType.String);
            }
        }

        public esQueryItem SeparatorAfterNumber
        {
            get
            {
                return new esQueryItem(this, AppAutoNumberMetadata.ColumnNames.SeparatorAfterNumber, esSystemType.String);
            }
        }

        public esQueryItem IsUsedYearToDateOrder
        {
            get
            {
                return new esQueryItem(this, AppAutoNumberMetadata.ColumnNames.IsUsedYearToDateOrder, esSystemType.Boolean);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, AppAutoNumberMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, AppAutoNumberMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("AppAutoNumberCollection")]
    public partial class AppAutoNumberCollection : esAppAutoNumberCollection, IEnumerable<AppAutoNumber>
    {
        public AppAutoNumberCollection()
        {

        }

        public static implicit operator List<AppAutoNumber>(AppAutoNumberCollection coll)
        {
            List<AppAutoNumber> list = new List<AppAutoNumber>();

            foreach (AppAutoNumber emp in coll)
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
                return AppAutoNumberMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new AppAutoNumberQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new AppAutoNumber(row);
        }

        override protected esEntity CreateEntity()
        {
            return new AppAutoNumber();
        }

        #endregion

        [BrowsableAttribute(false)]
        public AppAutoNumberQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new AppAutoNumberQuery();
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
        public bool Load(AppAutoNumberQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public AppAutoNumber AddNew()
        {
            AppAutoNumber entity = base.AddNewEntity() as AppAutoNumber;

            return entity;
        }
        public AppAutoNumber FindByPrimaryKey(String sRAutoNumber, DateTime effectiveDate)
        {
            return base.FindByPrimaryKey(sRAutoNumber, effectiveDate) as AppAutoNumber;
        }

        #region IEnumerable< AppAutoNumber> Members

        IEnumerator<AppAutoNumber> IEnumerable<AppAutoNumber>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as AppAutoNumber;
            }
        }

        #endregion

        private AppAutoNumberQuery query;
    }


    /// <summary>
    /// Encapsulates the 'AppAutoNumber' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("AppAutoNumber ({SRAutoNumber, EffectiveDate})")]
    [Serializable]
    public partial class AppAutoNumber : esAppAutoNumber
    {
        public AppAutoNumber()
        {
        }

        public AppAutoNumber(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return AppAutoNumberMetadata.Meta();
            }
        }

        override protected esAppAutoNumberQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new AppAutoNumberQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public AppAutoNumberQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new AppAutoNumberQuery();
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
        public bool Load(AppAutoNumberQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private AppAutoNumberQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class AppAutoNumberQuery : esAppAutoNumberQuery
    {
        public AppAutoNumberQuery()
        {

        }

        public AppAutoNumberQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "AppAutoNumberQuery";
        }
    }

    [Serializable]
    public partial class AppAutoNumberMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected AppAutoNumberMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(AppAutoNumberMetadata.ColumnNames.SRAutoNumber, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = AppAutoNumberMetadata.PropertyNames.SRAutoNumber;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(AppAutoNumberMetadata.ColumnNames.EffectiveDate, 1, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = AppAutoNumberMetadata.PropertyNames.EffectiveDate;
            c.IsInPrimaryKey = true;
            _columns.Add(c);

            c = new esColumnMetadata(AppAutoNumberMetadata.ColumnNames.Prefik, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = AppAutoNumberMetadata.PropertyNames.Prefik;
            c.CharacterMaxLength = 5;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AppAutoNumberMetadata.ColumnNames.SeparatorAfterPrefik, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = AppAutoNumberMetadata.PropertyNames.SeparatorAfterPrefik;
            c.CharacterMaxLength = 1;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AppAutoNumberMetadata.ColumnNames.IsUsedDepartment, 4, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = AppAutoNumberMetadata.PropertyNames.IsUsedDepartment;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AppAutoNumberMetadata.ColumnNames.SeparatorAfterDept, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = AppAutoNumberMetadata.PropertyNames.SeparatorAfterDept;
            c.CharacterMaxLength = 1;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AppAutoNumberMetadata.ColumnNames.IsUsedYear, 6, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = AppAutoNumberMetadata.PropertyNames.IsUsedYear;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AppAutoNumberMetadata.ColumnNames.YearDigit, 7, typeof(System.Byte), esSystemType.Byte);
            c.PropertyName = AppAutoNumberMetadata.PropertyNames.YearDigit;
            c.NumericPrecision = 3;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AppAutoNumberMetadata.ColumnNames.SeparatorAfterYear, 8, typeof(System.String), esSystemType.String);
            c.PropertyName = AppAutoNumberMetadata.PropertyNames.SeparatorAfterYear;
            c.CharacterMaxLength = 1;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AppAutoNumberMetadata.ColumnNames.IsUsedMonth, 9, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = AppAutoNumberMetadata.PropertyNames.IsUsedMonth;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AppAutoNumberMetadata.ColumnNames.IsMonthInRomawi, 10, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = AppAutoNumberMetadata.PropertyNames.IsMonthInRomawi;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AppAutoNumberMetadata.ColumnNames.SeparatorAfterMonth, 11, typeof(System.String), esSystemType.String);
            c.PropertyName = AppAutoNumberMetadata.PropertyNames.SeparatorAfterMonth;
            c.CharacterMaxLength = 1;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AppAutoNumberMetadata.ColumnNames.IsUsedDay, 12, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = AppAutoNumberMetadata.PropertyNames.IsUsedDay;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AppAutoNumberMetadata.ColumnNames.SeparatorAfterDay, 13, typeof(System.String), esSystemType.String);
            c.PropertyName = AppAutoNumberMetadata.PropertyNames.SeparatorAfterDay;
            c.CharacterMaxLength = 1;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AppAutoNumberMetadata.ColumnNames.NumberLength, 14, typeof(System.Byte), esSystemType.Byte);
            c.PropertyName = AppAutoNumberMetadata.PropertyNames.NumberLength;
            c.NumericPrecision = 3;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AppAutoNumberMetadata.ColumnNames.NumberGroupLength, 15, typeof(System.Byte), esSystemType.Byte);
            c.PropertyName = AppAutoNumberMetadata.PropertyNames.NumberGroupLength;
            c.NumericPrecision = 3;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AppAutoNumberMetadata.ColumnNames.NumberGroupSeparator, 16, typeof(System.String), esSystemType.String);
            c.PropertyName = AppAutoNumberMetadata.PropertyNames.NumberGroupSeparator;
            c.CharacterMaxLength = 1;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AppAutoNumberMetadata.ColumnNames.NumberFormat, 17, typeof(System.String), esSystemType.String);
            c.PropertyName = AppAutoNumberMetadata.PropertyNames.NumberFormat;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AppAutoNumberMetadata.ColumnNames.SeparatorAfterNumber, 18, typeof(System.String), esSystemType.String);
            c.PropertyName = AppAutoNumberMetadata.PropertyNames.SeparatorAfterNumber;
            c.CharacterMaxLength = 1;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AppAutoNumberMetadata.ColumnNames.IsUsedYearToDateOrder, 19, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = AppAutoNumberMetadata.PropertyNames.IsUsedYearToDateOrder;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AppAutoNumberMetadata.ColumnNames.LastUpdateDateTime, 20, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = AppAutoNumberMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AppAutoNumberMetadata.ColumnNames.LastUpdateByUserID, 21, typeof(System.String), esSystemType.String);
            c.PropertyName = AppAutoNumberMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 40;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public AppAutoNumberMetadata Meta()
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
            public const string SRAutoNumber = "SRAutoNumber";
            public const string EffectiveDate = "EffectiveDate";
            public const string Prefik = "Prefik";
            public const string SeparatorAfterPrefik = "SeparatorAfterPrefik";
            public const string IsUsedDepartment = "IsUsedDepartment";
            public const string SeparatorAfterDept = "SeparatorAfterDept";
            public const string IsUsedYear = "IsUsedYear";
            public const string YearDigit = "YearDigit";
            public const string SeparatorAfterYear = "SeparatorAfterYear";
            public const string IsUsedMonth = "IsUsedMonth";
            public const string IsMonthInRomawi = "IsMonthInRomawi";
            public const string SeparatorAfterMonth = "SeparatorAfterMonth";
            public const string IsUsedDay = "IsUsedDay";
            public const string SeparatorAfterDay = "SeparatorAfterDay";
            public const string NumberLength = "NumberLength";
            public const string NumberGroupLength = "NumberGroupLength";
            public const string NumberGroupSeparator = "NumberGroupSeparator";
            public const string NumberFormat = "NumberFormat";
            public const string SeparatorAfterNumber = "SeparatorAfterNumber";
            public const string IsUsedYearToDateOrder = "IsUsedYearToDateOrder";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string SRAutoNumber = "SRAutoNumber";
            public const string EffectiveDate = "EffectiveDate";
            public const string Prefik = "Prefik";
            public const string SeparatorAfterPrefik = "SeparatorAfterPrefik";
            public const string IsUsedDepartment = "IsUsedDepartment";
            public const string SeparatorAfterDept = "SeparatorAfterDept";
            public const string IsUsedYear = "IsUsedYear";
            public const string YearDigit = "YearDigit";
            public const string SeparatorAfterYear = "SeparatorAfterYear";
            public const string IsUsedMonth = "IsUsedMonth";
            public const string IsMonthInRomawi = "IsMonthInRomawi";
            public const string SeparatorAfterMonth = "SeparatorAfterMonth";
            public const string IsUsedDay = "IsUsedDay";
            public const string SeparatorAfterDay = "SeparatorAfterDay";
            public const string NumberLength = "NumberLength";
            public const string NumberGroupLength = "NumberGroupLength";
            public const string NumberGroupSeparator = "NumberGroupSeparator";
            public const string NumberFormat = "NumberFormat";
            public const string SeparatorAfterNumber = "SeparatorAfterNumber";
            public const string IsUsedYearToDateOrder = "IsUsedYearToDateOrder";
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
            lock (typeof(AppAutoNumberMetadata))
            {
                if (AppAutoNumberMetadata.mapDelegates == null)
                {
                    AppAutoNumberMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (AppAutoNumberMetadata.meta == null)
                {
                    AppAutoNumberMetadata.meta = new AppAutoNumberMetadata();
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

                meta.AddTypeMap("SRAutoNumber", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("EffectiveDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("Prefik", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SeparatorAfterPrefik", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsUsedDepartment", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("SeparatorAfterDept", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsUsedYear", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("YearDigit", new esTypeMap("tinyint", "System.Byte"));
                meta.AddTypeMap("SeparatorAfterYear", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsUsedMonth", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsMonthInRomawi", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("SeparatorAfterMonth", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsUsedDay", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("SeparatorAfterDay", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("NumberLength", new esTypeMap("tinyint", "System.Byte"));
                meta.AddTypeMap("NumberGroupLength", new esTypeMap("tinyint", "System.Byte"));
                meta.AddTypeMap("NumberGroupSeparator", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("NumberFormat", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SeparatorAfterNumber", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsUsedYearToDateOrder", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "AppAutoNumber";
                meta.Destination = "AppAutoNumber";
                meta.spInsert = "proc_AppAutoNumberInsert";
                meta.spUpdate = "proc_AppAutoNumberUpdate";
                meta.spDelete = "proc_AppAutoNumberDelete";
                meta.spLoadAll = "proc_AppAutoNumberLoadAll";
                meta.spLoadByPrimaryKey = "proc_AppAutoNumberLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private AppAutoNumberMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
