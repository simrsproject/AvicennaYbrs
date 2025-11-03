/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 9/8/2020 11:30:34 AM
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

namespace Temiang.Avicenna.BusinessObject.Interop.Wynakom
{
    [Serializable]
    abstract public class esOrderedResultsCollection : esEntityCollectionWAuditLog
    {
        public esOrderedResultsCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "OrderedResultsCollection";
        }

        #region Query Logic
        protected void InitQuery(esOrderedResultsQuery query)
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
            this.InitQuery(query as esOrderedResultsQuery);
        }
        #endregion

        virtual public OrderedResults DetachEntity(OrderedResults entity)
        {
            return base.DetachEntity(entity) as OrderedResults;
        }

        virtual public OrderedResults AttachEntity(OrderedResults entity)
        {
            return base.AttachEntity(entity) as OrderedResults;
        }

        virtual public void Combine(OrderedResultsCollection collection)
        {
            base.Combine(collection);
        }

        new public OrderedResults this[int index]
        {
            get
            {
                return base[index] as OrderedResults;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(OrderedResults);
        }
    }

    [Serializable]
    abstract public class esOrderedResults : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esOrderedResultsQuery GetDynamicQuery()
        {
            return null;
        }

        public esOrderedResults()
        {
        }

        public esOrderedResults(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String lisRegNo, String lisTestId, String hisRegNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(lisRegNo, lisTestId, hisRegNo);
            else
                return LoadByPrimaryKeyStoredProcedure(lisRegNo, lisTestId, hisRegNo);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String lisRegNo, String lisTestId, String hisRegNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(lisRegNo, lisTestId, hisRegNo);
            else
                return LoadByPrimaryKeyStoredProcedure(lisRegNo, lisTestId, hisRegNo);
        }

        private bool LoadByPrimaryKeyDynamic(String lisRegNo, String lisTestId, String hisRegNo)
        {
            esOrderedResultsQuery query = this.GetDynamicQuery();
            query.Where(query.LisRegNo == lisRegNo, query.LisTestId == lisTestId, query.HisRegNo == hisRegNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String lisRegNo, String lisTestId, String hisRegNo)
        {
            esParameters parms = new esParameters();
            parms.Add("lis_reg_no", lisRegNo);
            parms.Add("lis_test_id", lisTestId);
            parms.Add("his_reg_no", hisRegNo);
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
                        case "LisRegNo": this.str.LisRegNo = (string)value; break;
                        case "LisTestId": this.str.LisTestId = (string)value; break;
                        case "HisRegNo": this.str.HisRegNo = (string)value; break;
                        case "TestName": this.str.TestName = (string)value; break;
                        case "Result": this.str.Result = (string)value; break;
                        case "ResultComment": this.str.ResultComment = (string)value; break;
                        case "ReferenceValue": this.str.ReferenceValue = (string)value; break;
                        case "ReferenceNote": this.str.ReferenceNote = (string)value; break;
                        case "TestFlagSign": this.str.TestFlagSign = (string)value; break;
                        case "TestUnitsName": this.str.TestUnitsName = (string)value; break;
                        case "InstrumentName": this.str.InstrumentName = (string)value; break;
                        case "AuthorizationDate": this.str.AuthorizationDate = (string)value; break;
                        case "AuthorizationUser": this.str.AuthorizationUser = (string)value; break;
                        case "GreaterthanValue": this.str.GreaterthanValue = (string)value; break;
                        case "LessthanValue": this.str.LessthanValue = (string)value; break;
                        case "AgeYear": this.str.AgeYear = (string)value; break;
                        case "AgeMonth": this.str.AgeMonth = (string)value; break;
                        case "AgeDays": this.str.AgeDays = (string)value; break;
                        case "HisTestId": this.str.HisTestId = (string)value; break;
                        case "Sequence": this.str.Sequence = (string)value; break;
                        case "TransferFlag": this.str.TransferFlag = (string)value; break;
                        case "TestGroup": this.str.TestGroup = (string)value; break;
                        case "Flagrs": this.str.Flagrs = (string)value; break;
                        case "ResultNote": this.str.ResultNote = (string)value; break;
                        case "HeaderFlag": this.str.HeaderFlag = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "authorization_date":

                            if (value == null || value is System.DateTime)
                                this.AuthorizationDate = (System.DateTime?)value;
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
        /// Maps to Ordered_Results.lis_reg_no
        /// </summary>
        virtual public System.String LisRegNo
        {
            get
            {
                return base.GetSystemString(OrderedResultsMetadata.ColumnNames.LisRegNo);
            }

            set
            {
                base.SetSystemString(OrderedResultsMetadata.ColumnNames.LisRegNo, value);
            }
        }
        /// <summary>
        /// Maps to Ordered_Results.lis_test_id
        /// </summary>
        virtual public System.String LisTestId
        {
            get
            {
                return base.GetSystemString(OrderedResultsMetadata.ColumnNames.LisTestId);
            }

            set
            {
                base.SetSystemString(OrderedResultsMetadata.ColumnNames.LisTestId, value);
            }
        }
        /// <summary>
        /// Maps to Ordered_Results.his_reg_no
        /// </summary>
        virtual public System.String HisRegNo
        {
            get
            {
                return base.GetSystemString(OrderedResultsMetadata.ColumnNames.HisRegNo);
            }

            set
            {
                base.SetSystemString(OrderedResultsMetadata.ColumnNames.HisRegNo, value);
            }
        }
        /// <summary>
        /// Maps to Ordered_Results.test_name
        /// </summary>
        virtual public System.String TestName
        {
            get
            {
                return base.GetSystemString(OrderedResultsMetadata.ColumnNames.TestName);
            }

            set
            {
                base.SetSystemString(OrderedResultsMetadata.ColumnNames.TestName, value);
            }
        }
        /// <summary>
        /// Maps to Ordered_Results.result
        /// </summary>
        virtual public System.String Result
        {
            get
            {
                return base.GetSystemString(OrderedResultsMetadata.ColumnNames.Result);
            }

            set
            {
                base.SetSystemString(OrderedResultsMetadata.ColumnNames.Result, value);
            }
        }
        /// <summary>
        /// Maps to Ordered_Results.result_comment
        /// </summary>
        virtual public System.String ResultComment
        {
            get
            {
                return base.GetSystemString(OrderedResultsMetadata.ColumnNames.ResultComment);
            }

            set
            {
                base.SetSystemString(OrderedResultsMetadata.ColumnNames.ResultComment, value);
            }
        }
        /// <summary>
        /// Maps to Ordered_Results.reference_value
        /// </summary>
        virtual public System.String ReferenceValue
        {
            get
            {
                return base.GetSystemString(OrderedResultsMetadata.ColumnNames.ReferenceValue);
            }

            set
            {
                base.SetSystemString(OrderedResultsMetadata.ColumnNames.ReferenceValue, value);
            }
        }
        /// <summary>
        /// Maps to Ordered_Results.reference_note
        /// </summary>
        virtual public System.String ReferenceNote
        {
            get
            {
                return base.GetSystemString(OrderedResultsMetadata.ColumnNames.ReferenceNote);
            }

            set
            {
                base.SetSystemString(OrderedResultsMetadata.ColumnNames.ReferenceNote, value);
            }
        }
        /// <summary>
        /// Maps to Ordered_Results.test_flag_sign
        /// </summary>
        virtual public System.String TestFlagSign
        {
            get
            {
                return base.GetSystemString(OrderedResultsMetadata.ColumnNames.TestFlagSign);
            }

            set
            {
                base.SetSystemString(OrderedResultsMetadata.ColumnNames.TestFlagSign, value);
            }
        }
        /// <summary>
        /// Maps to Ordered_Results.test_units_name
        /// </summary>
        virtual public System.String TestUnitsName
        {
            get
            {
                return base.GetSystemString(OrderedResultsMetadata.ColumnNames.TestUnitsName);
            }

            set
            {
                base.SetSystemString(OrderedResultsMetadata.ColumnNames.TestUnitsName, value);
            }
        }
        /// <summary>
        /// Maps to Ordered_Results.instrument_name
        /// </summary>
        virtual public System.String InstrumentName
        {
            get
            {
                return base.GetSystemString(OrderedResultsMetadata.ColumnNames.InstrumentName);
            }

            set
            {
                base.SetSystemString(OrderedResultsMetadata.ColumnNames.InstrumentName, value);
            }
        }
        /// <summary>
        /// Maps to Ordered_Results.authorization_date
        /// </summary>
        virtual public System.DateTime? AuthorizationDate
        {
            get
            {
                return base.GetSystemDateTime(OrderedResultsMetadata.ColumnNames.AuthorizationDate);
            }

            set
            {
                base.SetSystemDateTime(OrderedResultsMetadata.ColumnNames.AuthorizationDate, value);
            }
        }
        /// <summary>
        /// Maps to Ordered_Results.authorization_user
        /// </summary>
        virtual public System.String AuthorizationUser
        {
            get
            {
                return base.GetSystemString(OrderedResultsMetadata.ColumnNames.AuthorizationUser);
            }

            set
            {
                base.SetSystemString(OrderedResultsMetadata.ColumnNames.AuthorizationUser, value);
            }
        }
        /// <summary>
        /// Maps to Ordered_Results.greaterthan_value
        /// </summary>
        virtual public System.String GreaterthanValue
        {
            get
            {
                return base.GetSystemString(OrderedResultsMetadata.ColumnNames.GreaterthanValue);
            }

            set
            {
                base.SetSystemString(OrderedResultsMetadata.ColumnNames.GreaterthanValue, value);
            }
        }
        /// <summary>
        /// Maps to Ordered_Results.lessthan_value
        /// </summary>
        virtual public System.String LessthanValue
        {
            get
            {
                return base.GetSystemString(OrderedResultsMetadata.ColumnNames.LessthanValue);
            }

            set
            {
                base.SetSystemString(OrderedResultsMetadata.ColumnNames.LessthanValue, value);
            }
        }
        /// <summary>
        /// Maps to Ordered_Results.AGE_YEAR
        /// </summary>
        virtual public System.String AgeYear
        {
            get
            {
                return base.GetSystemString(OrderedResultsMetadata.ColumnNames.AgeYear);
            }

            set
            {
                base.SetSystemString(OrderedResultsMetadata.ColumnNames.AgeYear, value);
            }
        }
        /// <summary>
        /// Maps to Ordered_Results.AGE_MONTH
        /// </summary>
        virtual public System.String AgeMonth
        {
            get
            {
                return base.GetSystemString(OrderedResultsMetadata.ColumnNames.AgeMonth);
            }

            set
            {
                base.SetSystemString(OrderedResultsMetadata.ColumnNames.AgeMonth, value);
            }
        }
        /// <summary>
        /// Maps to Ordered_Results.AGE_DAYS
        /// </summary>
        virtual public System.String AgeDays
        {
            get
            {
                return base.GetSystemString(OrderedResultsMetadata.ColumnNames.AgeDays);
            }

            set
            {
                base.SetSystemString(OrderedResultsMetadata.ColumnNames.AgeDays, value);
            }
        }
        /// <summary>
        /// Maps to Ordered_Results.his_test_id
        /// </summary>
        virtual public System.String HisTestId
        {
            get
            {
                return base.GetSystemString(OrderedResultsMetadata.ColumnNames.HisTestId);
            }

            set
            {
                base.SetSystemString(OrderedResultsMetadata.ColumnNames.HisTestId, value);
            }
        }
        /// <summary>
        /// Maps to Ordered_Results.sequence
        /// </summary>
        virtual public System.String Sequence
        {
            get
            {
                return base.GetSystemString(OrderedResultsMetadata.ColumnNames.Sequence);
            }

            set
            {
                base.SetSystemString(OrderedResultsMetadata.ColumnNames.Sequence, value);
            }
        }
        /// <summary>
        /// Maps to Ordered_Results.transfer_flag
        /// </summary>
        virtual public System.String TransferFlag
        {
            get
            {
                return base.GetSystemString(OrderedResultsMetadata.ColumnNames.TransferFlag);
            }

            set
            {
                base.SetSystemString(OrderedResultsMetadata.ColumnNames.TransferFlag, value);
            }
        }
        /// <summary>
        /// Maps to Ordered_Results.test_group
        /// </summary>
        virtual public System.String TestGroup
        {
            get
            {
                return base.GetSystemString(OrderedResultsMetadata.ColumnNames.TestGroup);
            }

            set
            {
                base.SetSystemString(OrderedResultsMetadata.ColumnNames.TestGroup, value);
            }
        }
        /// <summary>
        /// Maps to Ordered_Results.flagrs
        /// </summary>
        virtual public System.String Flagrs
        {
            get
            {
                return base.GetSystemString(OrderedResultsMetadata.ColumnNames.Flagrs);
            }

            set
            {
                base.SetSystemString(OrderedResultsMetadata.ColumnNames.Flagrs, value);
            }
        }
        /// <summary>
        /// Maps to Ordered_Results.Result_note
        /// </summary>
        virtual public System.String ResultNote
        {
            get
            {
                return base.GetSystemString(OrderedResultsMetadata.ColumnNames.ResultNote);
            }

            set
            {
                base.SetSystemString(OrderedResultsMetadata.ColumnNames.ResultNote, value);
            }
        }
        /// <summary>
        /// Maps to Ordered_Results.header_flag
        /// </summary>
        virtual public System.String HeaderFlag
        {
            get
            {
                return base.GetSystemString(OrderedResultsMetadata.ColumnNames.HeaderFlag);
            }

            set
            {
                base.SetSystemString(OrderedResultsMetadata.ColumnNames.HeaderFlag, value);
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
            public esStrings(esOrderedResults entity)
            {
                this.entity = entity;
            }
            public System.String LisRegNo
            {
                get
                {
                    System.String data = entity.LisRegNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.LisRegNo = null;
                    else entity.LisRegNo = Convert.ToString(value);
                }
            }
            public System.String LisTestId
            {
                get
                {
                    System.String data = entity.LisTestId;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.LisTestId = null;
                    else entity.LisTestId = Convert.ToString(value);
                }
            }
            public System.String HisRegNo
            {
                get
                {
                    System.String data = entity.HisRegNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.HisRegNo = null;
                    else entity.HisRegNo = Convert.ToString(value);
                }
            }
            public System.String TestName
            {
                get
                {
                    System.String data = entity.TestName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TestName = null;
                    else entity.TestName = Convert.ToString(value);
                }
            }
            public System.String Result
            {
                get
                {
                    System.String data = entity.Result;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Result = null;
                    else entity.Result = Convert.ToString(value);
                }
            }
            public System.String ResultComment
            {
                get
                {
                    System.String data = entity.ResultComment;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ResultComment = null;
                    else entity.ResultComment = Convert.ToString(value);
                }
            }
            public System.String ReferenceValue
            {
                get
                {
                    System.String data = entity.ReferenceValue;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ReferenceValue = null;
                    else entity.ReferenceValue = Convert.ToString(value);
                }
            }
            public System.String ReferenceNote
            {
                get
                {
                    System.String data = entity.ReferenceNote;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ReferenceNote = null;
                    else entity.ReferenceNote = Convert.ToString(value);
                }
            }
            public System.String TestFlagSign
            {
                get
                {
                    System.String data = entity.TestFlagSign;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TestFlagSign = null;
                    else entity.TestFlagSign = Convert.ToString(value);
                }
            }
            public System.String TestUnitsName
            {
                get
                {
                    System.String data = entity.TestUnitsName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TestUnitsName = null;
                    else entity.TestUnitsName = Convert.ToString(value);
                }
            }
            public System.String InstrumentName
            {
                get
                {
                    System.String data = entity.InstrumentName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.InstrumentName = null;
                    else entity.InstrumentName = Convert.ToString(value);
                }
            }
            public System.String AuthorizationDate
            {
                get
                {
                    System.DateTime? data = entity.AuthorizationDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.AuthorizationDate = null;
                    else entity.AuthorizationDate = Convert.ToDateTime(value);
                }
            }
            public System.String AuthorizationUser
            {
                get
                {
                    System.String data = entity.AuthorizationUser;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.AuthorizationUser = null;
                    else entity.AuthorizationUser = Convert.ToString(value);
                }
            }
            public System.String GreaterthanValue
            {
                get
                {
                    System.String data = entity.GreaterthanValue;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.GreaterthanValue = null;
                    else entity.GreaterthanValue = Convert.ToString(value);
                }
            }
            public System.String LessthanValue
            {
                get
                {
                    System.String data = entity.LessthanValue;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.LessthanValue = null;
                    else entity.LessthanValue = Convert.ToString(value);
                }
            }
            public System.String AgeYear
            {
                get
                {
                    System.String data = entity.AgeYear;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.AgeYear = null;
                    else entity.AgeYear = Convert.ToString(value);
                }
            }
            public System.String AgeMonth
            {
                get
                {
                    System.String data = entity.AgeMonth;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.AgeMonth = null;
                    else entity.AgeMonth = Convert.ToString(value);
                }
            }
            public System.String AgeDays
            {
                get
                {
                    System.String data = entity.AgeDays;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.AgeDays = null;
                    else entity.AgeDays = Convert.ToString(value);
                }
            }
            public System.String HisTestId
            {
                get
                {
                    System.String data = entity.HisTestId;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.HisTestId = null;
                    else entity.HisTestId = Convert.ToString(value);
                }
            }
            public System.String Sequence
            {
                get
                {
                    System.String data = entity.Sequence;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Sequence = null;
                    else entity.Sequence = Convert.ToString(value);
                }
            }
            public System.String TransferFlag
            {
                get
                {
                    System.String data = entity.TransferFlag;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TransferFlag = null;
                    else entity.TransferFlag = Convert.ToString(value);
                }
            }
            public System.String TestGroup
            {
                get
                {
                    System.String data = entity.TestGroup;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TestGroup = null;
                    else entity.TestGroup = Convert.ToString(value);
                }
            }
            public System.String Flagrs
            {
                get
                {
                    System.String data = entity.Flagrs;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Flagrs = null;
                    else entity.Flagrs = Convert.ToString(value);
                }
            }
            public System.String ResultNote
            {
                get
                {
                    System.String data = entity.ResultNote;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ResultNote = null;
                    else entity.ResultNote = Convert.ToString(value);
                }
            }
            public System.String HeaderFlag
            {
                get
                {
                    System.String data = entity.HeaderFlag;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.HeaderFlag = null;
                    else entity.HeaderFlag = Convert.ToString(value);
                }
            }
            private esOrderedResults entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esOrderedResultsQuery query)
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
                throw new Exception("esOrderedResults can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class OrderedResults : esOrderedResults
    {
    }

    [Serializable]
    abstract public class esOrderedResultsQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return OrderedResultsMetadata.Meta();
            }
        }

        public esQueryItem LisRegNo
        {
            get
            {
                return new esQueryItem(this, OrderedResultsMetadata.ColumnNames.LisRegNo, esSystemType.String);
            }
        }

        public esQueryItem LisTestId
        {
            get
            {
                return new esQueryItem(this, OrderedResultsMetadata.ColumnNames.LisTestId, esSystemType.String);
            }
        }

        public esQueryItem HisRegNo
        {
            get
            {
                return new esQueryItem(this, OrderedResultsMetadata.ColumnNames.HisRegNo, esSystemType.String);
            }
        }

        public esQueryItem TestName
        {
            get
            {
                return new esQueryItem(this, OrderedResultsMetadata.ColumnNames.TestName, esSystemType.String);
            }
        }

        public esQueryItem Result
        {
            get
            {
                return new esQueryItem(this, OrderedResultsMetadata.ColumnNames.Result, esSystemType.String);
            }
        }

        public esQueryItem ResultComment
        {
            get
            {
                return new esQueryItem(this, OrderedResultsMetadata.ColumnNames.ResultComment, esSystemType.String);
            }
        }

        public esQueryItem ReferenceValue
        {
            get
            {
                return new esQueryItem(this, OrderedResultsMetadata.ColumnNames.ReferenceValue, esSystemType.String);
            }
        }

        public esQueryItem ReferenceNote
        {
            get
            {
                return new esQueryItem(this, OrderedResultsMetadata.ColumnNames.ReferenceNote, esSystemType.String);
            }
        }

        public esQueryItem TestFlagSign
        {
            get
            {
                return new esQueryItem(this, OrderedResultsMetadata.ColumnNames.TestFlagSign, esSystemType.String);
            }
        }

        public esQueryItem TestUnitsName
        {
            get
            {
                return new esQueryItem(this, OrderedResultsMetadata.ColumnNames.TestUnitsName, esSystemType.String);
            }
        }

        public esQueryItem InstrumentName
        {
            get
            {
                return new esQueryItem(this, OrderedResultsMetadata.ColumnNames.InstrumentName, esSystemType.String);
            }
        }

        public esQueryItem AuthorizationDate
        {
            get
            {
                return new esQueryItem(this, OrderedResultsMetadata.ColumnNames.AuthorizationDate, esSystemType.DateTime);
            }
        }

        public esQueryItem AuthorizationUser
        {
            get
            {
                return new esQueryItem(this, OrderedResultsMetadata.ColumnNames.AuthorizationUser, esSystemType.String);
            }
        }

        public esQueryItem GreaterthanValue
        {
            get
            {
                return new esQueryItem(this, OrderedResultsMetadata.ColumnNames.GreaterthanValue, esSystemType.String);
            }
        }

        public esQueryItem LessthanValue
        {
            get
            {
                return new esQueryItem(this, OrderedResultsMetadata.ColumnNames.LessthanValue, esSystemType.String);
            }
        }

        public esQueryItem AgeYear
        {
            get
            {
                return new esQueryItem(this, OrderedResultsMetadata.ColumnNames.AgeYear, esSystemType.String);
            }
        }

        public esQueryItem AgeMonth
        {
            get
            {
                return new esQueryItem(this, OrderedResultsMetadata.ColumnNames.AgeMonth, esSystemType.String);
            }
        }

        public esQueryItem AgeDays
        {
            get
            {
                return new esQueryItem(this, OrderedResultsMetadata.ColumnNames.AgeDays, esSystemType.String);
            }
        }

        public esQueryItem HisTestId
        {
            get
            {
                return new esQueryItem(this, OrderedResultsMetadata.ColumnNames.HisTestId, esSystemType.String);
            }
        }

        public esQueryItem Sequence
        {
            get
            {
                return new esQueryItem(this, OrderedResultsMetadata.ColumnNames.Sequence, esSystemType.String);
            }
        }

        public esQueryItem TransferFlag
        {
            get
            {
                return new esQueryItem(this, OrderedResultsMetadata.ColumnNames.TransferFlag, esSystemType.String);
            }
        }

        public esQueryItem TestGroup
        {
            get
            {
                return new esQueryItem(this, OrderedResultsMetadata.ColumnNames.TestGroup, esSystemType.String);
            }
        }

        public esQueryItem Flagrs
        {
            get
            {
                return new esQueryItem(this, OrderedResultsMetadata.ColumnNames.Flagrs, esSystemType.String);
            }
        }

        public esQueryItem ResultNote
        {
            get
            {
                return new esQueryItem(this, OrderedResultsMetadata.ColumnNames.ResultNote, esSystemType.String);
            }
        }

        public esQueryItem HeaderFlag
        {
            get
            {
                return new esQueryItem(this, OrderedResultsMetadata.ColumnNames.HeaderFlag, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("OrderedResultsCollection")]
    public partial class OrderedResultsCollection : esOrderedResultsCollection, IEnumerable<OrderedResults>
    {
        public OrderedResultsCollection()
        {

        }

        public static implicit operator List<OrderedResults>(OrderedResultsCollection coll)
        {
            List<OrderedResults> list = new List<OrderedResults>();

            foreach (OrderedResults emp in coll)
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
                return OrderedResultsMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new OrderedResultsQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new OrderedResults(row);
        }

        override protected esEntity CreateEntity()
        {
            return new OrderedResults();
        }

        #endregion

        [BrowsableAttribute(false)]
        public OrderedResultsQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new OrderedResultsQuery();
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
        public bool Load(OrderedResultsQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public OrderedResults AddNew()
        {
            OrderedResults entity = base.AddNewEntity() as OrderedResults;

            return entity;
        }
        public OrderedResults FindByPrimaryKey(String lisRegNo, String lisTestId, String hisRegNo)
        {
            return base.FindByPrimaryKey(lisRegNo, lisTestId, hisRegNo) as OrderedResults;
        }

        #region IEnumerable< OrderedResults> Members

        IEnumerator<OrderedResults> IEnumerable<OrderedResults>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as OrderedResults;
            }
        }

        #endregion

        private OrderedResultsQuery query;
    }


    /// <summary>
    /// Encapsulates the 'OrderedResults' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("OrderedResults ({lis_reg_no, lis_test_id, his_reg_no})")]
    [Serializable]
    public partial class OrderedResults : esOrderedResults
    {
        public OrderedResults()
        {
        }

        public OrderedResults(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return OrderedResultsMetadata.Meta();
            }
        }

        override protected esOrderedResultsQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new OrderedResultsQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public OrderedResultsQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new OrderedResultsQuery();
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
        public bool Load(OrderedResultsQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private OrderedResultsQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class OrderedResultsQuery : esOrderedResultsQuery
    {
        public OrderedResultsQuery()
        {

        }

        public OrderedResultsQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "OrderedResultsQuery";
        }
    }

    [Serializable]
    public partial class OrderedResultsMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected OrderedResultsMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(OrderedResultsMetadata.ColumnNames.LisRegNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = OrderedResultsMetadata.PropertyNames.LisRegNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(OrderedResultsMetadata.ColumnNames.LisTestId, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = OrderedResultsMetadata.PropertyNames.LisTestId;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 100;
            _columns.Add(c);

            c = new esColumnMetadata(OrderedResultsMetadata.ColumnNames.HisRegNo, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = OrderedResultsMetadata.PropertyNames.HisRegNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(OrderedResultsMetadata.ColumnNames.TestName, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = OrderedResultsMetadata.PropertyNames.TestName;
            c.CharacterMaxLength = 255;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(OrderedResultsMetadata.ColumnNames.Result, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = OrderedResultsMetadata.PropertyNames.Result;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(OrderedResultsMetadata.ColumnNames.ResultComment, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = OrderedResultsMetadata.PropertyNames.ResultComment;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(OrderedResultsMetadata.ColumnNames.ReferenceValue, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = OrderedResultsMetadata.PropertyNames.ReferenceValue;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(OrderedResultsMetadata.ColumnNames.ReferenceNote, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = OrderedResultsMetadata.PropertyNames.ReferenceNote;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(OrderedResultsMetadata.ColumnNames.TestFlagSign, 8, typeof(System.String), esSystemType.String);
            c.PropertyName = OrderedResultsMetadata.PropertyNames.TestFlagSign;
            c.CharacterMaxLength = 5;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(OrderedResultsMetadata.ColumnNames.TestUnitsName, 9, typeof(System.String), esSystemType.String);
            c.PropertyName = OrderedResultsMetadata.PropertyNames.TestUnitsName;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(OrderedResultsMetadata.ColumnNames.InstrumentName, 10, typeof(System.String), esSystemType.String);
            c.PropertyName = OrderedResultsMetadata.PropertyNames.InstrumentName;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(OrderedResultsMetadata.ColumnNames.AuthorizationDate, 11, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = OrderedResultsMetadata.PropertyNames.AuthorizationDate;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(OrderedResultsMetadata.ColumnNames.AuthorizationUser, 12, typeof(System.String), esSystemType.String);
            c.PropertyName = OrderedResultsMetadata.PropertyNames.AuthorizationUser;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(OrderedResultsMetadata.ColumnNames.GreaterthanValue, 13, typeof(System.String), esSystemType.String);
            c.PropertyName = OrderedResultsMetadata.PropertyNames.GreaterthanValue;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(OrderedResultsMetadata.ColumnNames.LessthanValue, 14, typeof(System.String), esSystemType.String);
            c.PropertyName = OrderedResultsMetadata.PropertyNames.LessthanValue;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(OrderedResultsMetadata.ColumnNames.AgeYear, 15, typeof(System.String), esSystemType.String);
            c.PropertyName = OrderedResultsMetadata.PropertyNames.AgeYear;
            c.CharacterMaxLength = 3;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(OrderedResultsMetadata.ColumnNames.AgeMonth, 16, typeof(System.String), esSystemType.String);
            c.PropertyName = OrderedResultsMetadata.PropertyNames.AgeMonth;
            c.CharacterMaxLength = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(OrderedResultsMetadata.ColumnNames.AgeDays, 17, typeof(System.String), esSystemType.String);
            c.PropertyName = OrderedResultsMetadata.PropertyNames.AgeDays;
            c.CharacterMaxLength = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(OrderedResultsMetadata.ColumnNames.HisTestId, 18, typeof(System.String), esSystemType.String);
            c.PropertyName = OrderedResultsMetadata.PropertyNames.HisTestId;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(OrderedResultsMetadata.ColumnNames.Sequence, 19, typeof(System.String), esSystemType.String);
            c.PropertyName = OrderedResultsMetadata.PropertyNames.Sequence;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(OrderedResultsMetadata.ColumnNames.TransferFlag, 20, typeof(System.String), esSystemType.String);
            c.PropertyName = OrderedResultsMetadata.PropertyNames.TransferFlag;
            c.CharacterMaxLength = 1;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(OrderedResultsMetadata.ColumnNames.TestGroup, 21, typeof(System.String), esSystemType.String);
            c.PropertyName = OrderedResultsMetadata.PropertyNames.TestGroup;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(OrderedResultsMetadata.ColumnNames.Flagrs, 22, typeof(System.String), esSystemType.String);
            c.PropertyName = OrderedResultsMetadata.PropertyNames.Flagrs;
            c.CharacterMaxLength = 1;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(OrderedResultsMetadata.ColumnNames.ResultNote, 23, typeof(System.String), esSystemType.String);
            c.PropertyName = OrderedResultsMetadata.PropertyNames.ResultNote;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(OrderedResultsMetadata.ColumnNames.HeaderFlag, 24, typeof(System.String), esSystemType.String);
            c.PropertyName = OrderedResultsMetadata.PropertyNames.HeaderFlag;
            c.CharacterMaxLength = 1;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public OrderedResultsMetadata Meta()
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
            public const string LisRegNo = "lis_reg_no";
            public const string LisTestId = "lis_test_id";
            public const string HisRegNo = "his_reg_no";
            public const string TestName = "test_name";
            public const string Result = "result";
            public const string ResultComment = "result_comment";
            public const string ReferenceValue = "reference_value";
            public const string ReferenceNote = "reference_note";
            public const string TestFlagSign = "test_flag_sign";
            public const string TestUnitsName = "test_units_name";
            public const string InstrumentName = "instrument_name";
            public const string AuthorizationDate = "authorization_date";
            public const string AuthorizationUser = "authorization_user";
            public const string GreaterthanValue = "greaterthan_value";
            public const string LessthanValue = "lessthan_value";
            public const string AgeYear = "AGE_YEAR";
            public const string AgeMonth = "AGE_MONTH";
            public const string AgeDays = "AGE_DAYS";
            public const string HisTestId = "his_test_id";
            public const string Sequence = "sequence";
            public const string TransferFlag = "transfer_flag";
            public const string TestGroup = "test_group";
            public const string Flagrs = "flagrs";
            public const string ResultNote = "Result_note";
            public const string HeaderFlag = "header_flag";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string LisRegNo = "LisRegNo";
            public const string LisTestId = "LisTestId";
            public const string HisRegNo = "HisRegNo";
            public const string TestName = "TestName";
            public const string Result = "Result";
            public const string ResultComment = "ResultComment";
            public const string ReferenceValue = "ReferenceValue";
            public const string ReferenceNote = "ReferenceNote";
            public const string TestFlagSign = "TestFlagSign";
            public const string TestUnitsName = "TestUnitsName";
            public const string InstrumentName = "InstrumentName";
            public const string AuthorizationDate = "AuthorizationDate";
            public const string AuthorizationUser = "AuthorizationUser";
            public const string GreaterthanValue = "GreaterthanValue";
            public const string LessthanValue = "LessthanValue";
            public const string AgeYear = "AgeYear";
            public const string AgeMonth = "AgeMonth";
            public const string AgeDays = "AgeDays";
            public const string HisTestId = "HisTestId";
            public const string Sequence = "Sequence";
            public const string TransferFlag = "TransferFlag";
            public const string TestGroup = "TestGroup";
            public const string Flagrs = "Flagrs";
            public const string ResultNote = "ResultNote";
            public const string HeaderFlag = "HeaderFlag";
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
            lock (typeof(OrderedResultsMetadata))
            {
                if (OrderedResultsMetadata.mapDelegates == null)
                {
                    OrderedResultsMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (OrderedResultsMetadata.meta == null)
                {
                    OrderedResultsMetadata.meta = new OrderedResultsMetadata();
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

                meta.AddTypeMap("LisRegNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LisTestId", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("HisRegNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("TestName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Result", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ResultComment", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ReferenceValue", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ReferenceNote", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("TestFlagSign", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("TestUnitsName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("InstrumentName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("AuthorizationDate", new esTypeMap("smalldatetime", "System.DateTime"));
                meta.AddTypeMap("AuthorizationUser", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("GreaterthanValue", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LessthanValue", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("AgeYear", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("AgeMonth", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("AgeDays", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("HisTestId", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Sequence", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("TransferFlag", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("TestGroup", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Flagrs", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ResultNote", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("HeaderFlag", new esTypeMap("varchar", "System.String"));


                meta.Source = "Ordered_Results";
                meta.Destination = "Ordered_Results";
                meta.spInsert = "proc_OrderedResultsInsert";
                meta.spUpdate = "proc_OrderedResultsUpdate";
                meta.spDelete = "proc_OrderedResultsDelete";
                meta.spLoadAll = "proc_OrderedResultsLoadAll";
                meta.spLoadByPrimaryKey = "proc_OrderedResultsLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private OrderedResultsMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
