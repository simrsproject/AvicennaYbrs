/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 7/9/2019 10:18:04 AM
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
    abstract public class esNutritionCareAssessmentQuestionDiagnoseCollection : esEntityCollectionWAuditLog
    {
        public esNutritionCareAssessmentQuestionDiagnoseCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "NutritionCareAssessmentQuestionDiagnoseCollection";
        }

        #region Query Logic
        protected void InitQuery(esNutritionCareAssessmentQuestionDiagnoseQuery query)
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
            this.InitQuery(query as esNutritionCareAssessmentQuestionDiagnoseQuery);
        }
        #endregion

        virtual public NutritionCareAssessmentQuestionDiagnose DetachEntity(NutritionCareAssessmentQuestionDiagnose entity)
        {
            return base.DetachEntity(entity) as NutritionCareAssessmentQuestionDiagnose;
        }

        virtual public NutritionCareAssessmentQuestionDiagnose AttachEntity(NutritionCareAssessmentQuestionDiagnose entity)
        {
            return base.AttachEntity(entity) as NutritionCareAssessmentQuestionDiagnose;
        }

        virtual public void Combine(NutritionCareAssessmentQuestionDiagnoseCollection collection)
        {
            base.Combine(collection);
        }

        new public NutritionCareAssessmentQuestionDiagnose this[int index]
        {
            get
            {
                return base[index] as NutritionCareAssessmentQuestionDiagnose;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(NutritionCareAssessmentQuestionDiagnose);
        }
    }

    [Serializable]
    abstract public class esNutritionCareAssessmentQuestionDiagnose : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esNutritionCareAssessmentQuestionDiagnoseQuery GetDynamicQuery()
        {
            return null;
        }

        public esNutritionCareAssessmentQuestionDiagnose()
        {
        }

        public esNutritionCareAssessmentQuestionDiagnose(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String questionID, String terminologyID, Int32 ageInMonthStart, Int32 ageInMonthEnd)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(questionID, terminologyID, ageInMonthStart, ageInMonthEnd);
            else
                return LoadByPrimaryKeyStoredProcedure(questionID, terminologyID, ageInMonthStart, ageInMonthEnd);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String questionID, String terminologyID, Int32 ageInMonthStart, Int32 ageInMonthEnd)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(questionID, terminologyID, ageInMonthStart, ageInMonthEnd);
            else
                return LoadByPrimaryKeyStoredProcedure(questionID, terminologyID, ageInMonthStart, ageInMonthEnd);
        }

        private bool LoadByPrimaryKeyDynamic(String questionID, String terminologyID, Int32 ageInMonthStart, Int32 ageInMonthEnd)
        {
            esNutritionCareAssessmentQuestionDiagnoseQuery query = this.GetDynamicQuery();
            query.Where(query.QuestionID == questionID, query.TerminologyID == terminologyID, query.AgeInMonthStart == ageInMonthStart, query.AgeInMonthEnd == ageInMonthEnd);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String questionID, String terminologyID, Int32 ageInMonthStart, Int32 ageInMonthEnd)
        {
            esParameters parms = new esParameters();
            parms.Add("QuestionID", questionID);
            parms.Add("TerminologyID", terminologyID);
            parms.Add("AgeInMonthStart", ageInMonthStart);
            parms.Add("AgeInMonthEnd", ageInMonthEnd);
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
                        case "QuestionID": this.str.QuestionID = (string)value; break;
                        case "TerminologyID": this.str.TerminologyID = (string)value; break;
                        case "AgeInMonthStart": this.str.AgeInMonthStart = (string)value; break;
                        case "AgeInMonthEnd": this.str.AgeInMonthEnd = (string)value; break;
                        case "SRAnswerType": this.str.SRAnswerType = (string)value; break;
                        case "Operand": this.str.Operand = (string)value; break;
                        case "AcceptedText": this.str.AcceptedText = (string)value; break;
                        case "AcceptedNum": this.str.AcceptedNum = (string)value; break;
                        case "AcceptedNum2": this.str.AcceptedNum2 = (string)value; break;
                        case "CheckValue": this.str.CheckValue = (string)value; break;
                        case "IsUsingRange": this.str.IsUsingRange = (string)value; break;
                        case "CreateByUserID": this.str.CreateByUserID = (string)value; break;
                        case "CreateDateTime": this.str.CreateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "AgeInMonthStart":

                            if (value == null || value is System.Int32)
                                this.AgeInMonthStart = (System.Int32?)value;
                            break;
                        case "AgeInMonthEnd":

                            if (value == null || value is System.Int32)
                                this.AgeInMonthEnd = (System.Int32?)value;
                            break;
                        case "AcceptedNum":

                            if (value == null || value is System.Decimal)
                                this.AcceptedNum = (System.Decimal?)value;
                            break;
                        case "AcceptedNum2":

                            if (value == null || value is System.Decimal)
                                this.AcceptedNum2 = (System.Decimal?)value;
                            break;
                        case "CheckValue":

                            if (value == null || value is System.Boolean)
                                this.CheckValue = (System.Boolean?)value;
                            break;
                        case "IsUsingRange":

                            if (value == null || value is System.Boolean)
                                this.IsUsingRange = (System.Boolean?)value;
                            break;
                        case "CreateDateTime":

                            if (value == null || value is System.DateTime)
                                this.CreateDateTime = (System.DateTime?)value;
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
        /// Maps to NutritionCareAssessmentQuestionDiagnose.QuestionID
        /// </summary>
        virtual public System.String QuestionID
        {
            get
            {
                return base.GetSystemString(NutritionCareAssessmentQuestionDiagnoseMetadata.ColumnNames.QuestionID);
            }

            set
            {
                base.SetSystemString(NutritionCareAssessmentQuestionDiagnoseMetadata.ColumnNames.QuestionID, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareAssessmentQuestionDiagnose.TerminologyID
        /// </summary>
        virtual public System.String TerminologyID
        {
            get
            {
                return base.GetSystemString(NutritionCareAssessmentQuestionDiagnoseMetadata.ColumnNames.TerminologyID);
            }

            set
            {
                base.SetSystemString(NutritionCareAssessmentQuestionDiagnoseMetadata.ColumnNames.TerminologyID, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareAssessmentQuestionDiagnose.AgeInMonthStart
        /// </summary>
        virtual public System.Int32? AgeInMonthStart
        {
            get
            {
                return base.GetSystemInt32(NutritionCareAssessmentQuestionDiagnoseMetadata.ColumnNames.AgeInMonthStart);
            }

            set
            {
                base.SetSystemInt32(NutritionCareAssessmentQuestionDiagnoseMetadata.ColumnNames.AgeInMonthStart, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareAssessmentQuestionDiagnose.AgeInMonthEnd
        /// </summary>
        virtual public System.Int32? AgeInMonthEnd
        {
            get
            {
                return base.GetSystemInt32(NutritionCareAssessmentQuestionDiagnoseMetadata.ColumnNames.AgeInMonthEnd);
            }

            set
            {
                base.SetSystemInt32(NutritionCareAssessmentQuestionDiagnoseMetadata.ColumnNames.AgeInMonthEnd, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareAssessmentQuestionDiagnose.SRAnswerType
        /// </summary>
        virtual public System.String SRAnswerType
        {
            get
            {
                return base.GetSystemString(NutritionCareAssessmentQuestionDiagnoseMetadata.ColumnNames.SRAnswerType);
            }

            set
            {
                base.SetSystemString(NutritionCareAssessmentQuestionDiagnoseMetadata.ColumnNames.SRAnswerType, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareAssessmentQuestionDiagnose.Operand
        /// </summary>
        virtual public System.String Operand
        {
            get
            {
                return base.GetSystemString(NutritionCareAssessmentQuestionDiagnoseMetadata.ColumnNames.Operand);
            }

            set
            {
                base.SetSystemString(NutritionCareAssessmentQuestionDiagnoseMetadata.ColumnNames.Operand, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareAssessmentQuestionDiagnose.AcceptedText
        /// </summary>
        virtual public System.String AcceptedText
        {
            get
            {
                return base.GetSystemString(NutritionCareAssessmentQuestionDiagnoseMetadata.ColumnNames.AcceptedText);
            }

            set
            {
                base.SetSystemString(NutritionCareAssessmentQuestionDiagnoseMetadata.ColumnNames.AcceptedText, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareAssessmentQuestionDiagnose.AcceptedNum
        /// </summary>
        virtual public System.Decimal? AcceptedNum
        {
            get
            {
                return base.GetSystemDecimal(NutritionCareAssessmentQuestionDiagnoseMetadata.ColumnNames.AcceptedNum);
            }

            set
            {
                base.SetSystemDecimal(NutritionCareAssessmentQuestionDiagnoseMetadata.ColumnNames.AcceptedNum, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareAssessmentQuestionDiagnose.AcceptedNum2
        /// </summary>
        virtual public System.Decimal? AcceptedNum2
        {
            get
            {
                return base.GetSystemDecimal(NutritionCareAssessmentQuestionDiagnoseMetadata.ColumnNames.AcceptedNum2);
            }

            set
            {
                base.SetSystemDecimal(NutritionCareAssessmentQuestionDiagnoseMetadata.ColumnNames.AcceptedNum2, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareAssessmentQuestionDiagnose.CheckValue
        /// </summary>
        virtual public System.Boolean? CheckValue
        {
            get
            {
                return base.GetSystemBoolean(NutritionCareAssessmentQuestionDiagnoseMetadata.ColumnNames.CheckValue);
            }

            set
            {
                base.SetSystemBoolean(NutritionCareAssessmentQuestionDiagnoseMetadata.ColumnNames.CheckValue, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareAssessmentQuestionDiagnose.IsUsingRange
        /// </summary>
        virtual public System.Boolean? IsUsingRange
        {
            get
            {
                return base.GetSystemBoolean(NutritionCareAssessmentQuestionDiagnoseMetadata.ColumnNames.IsUsingRange);
            }

            set
            {
                base.SetSystemBoolean(NutritionCareAssessmentQuestionDiagnoseMetadata.ColumnNames.IsUsingRange, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareAssessmentQuestionDiagnose.CreateByUserID
        /// </summary>
        virtual public System.String CreateByUserID
        {
            get
            {
                return base.GetSystemString(NutritionCareAssessmentQuestionDiagnoseMetadata.ColumnNames.CreateByUserID);
            }

            set
            {
                base.SetSystemString(NutritionCareAssessmentQuestionDiagnoseMetadata.ColumnNames.CreateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareAssessmentQuestionDiagnose.CreateDateTime
        /// </summary>
        virtual public System.DateTime? CreateDateTime
        {
            get
            {
                return base.GetSystemDateTime(NutritionCareAssessmentQuestionDiagnoseMetadata.ColumnNames.CreateDateTime);
            }

            set
            {
                base.SetSystemDateTime(NutritionCareAssessmentQuestionDiagnoseMetadata.ColumnNames.CreateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareAssessmentQuestionDiagnose.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(NutritionCareAssessmentQuestionDiagnoseMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(NutritionCareAssessmentQuestionDiagnoseMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareAssessmentQuestionDiagnose.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(NutritionCareAssessmentQuestionDiagnoseMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(NutritionCareAssessmentQuestionDiagnoseMetadata.ColumnNames.LastUpdateDateTime, value);
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
            public esStrings(esNutritionCareAssessmentQuestionDiagnose entity)
            {
                this.entity = entity;
            }
            public System.String QuestionID
            {
                get
                {
                    System.String data = entity.QuestionID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.QuestionID = null;
                    else entity.QuestionID = Convert.ToString(value);
                }
            }
            public System.String TerminologyID
            {
                get
                {
                    System.String data = entity.TerminologyID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TerminologyID = null;
                    else entity.TerminologyID = Convert.ToString(value);
                }
            }
            public System.String AgeInMonthStart
            {
                get
                {
                    System.Int32? data = entity.AgeInMonthStart;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.AgeInMonthStart = null;
                    else entity.AgeInMonthStart = Convert.ToInt32(value);
                }
            }
            public System.String AgeInMonthEnd
            {
                get
                {
                    System.Int32? data = entity.AgeInMonthEnd;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.AgeInMonthEnd = null;
                    else entity.AgeInMonthEnd = Convert.ToInt32(value);
                }
            }
            public System.String SRAnswerType
            {
                get
                {
                    System.String data = entity.SRAnswerType;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRAnswerType = null;
                    else entity.SRAnswerType = Convert.ToString(value);
                }
            }
            public System.String Operand
            {
                get
                {
                    System.String data = entity.Operand;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Operand = null;
                    else entity.Operand = Convert.ToString(value);
                }
            }
            public System.String AcceptedText
            {
                get
                {
                    System.String data = entity.AcceptedText;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.AcceptedText = null;
                    else entity.AcceptedText = Convert.ToString(value);
                }
            }
            public System.String AcceptedNum
            {
                get
                {
                    System.Decimal? data = entity.AcceptedNum;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.AcceptedNum = null;
                    else entity.AcceptedNum = Convert.ToDecimal(value);
                }
            }
            public System.String AcceptedNum2
            {
                get
                {
                    System.Decimal? data = entity.AcceptedNum2;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.AcceptedNum2 = null;
                    else entity.AcceptedNum2 = Convert.ToDecimal(value);
                }
            }
            public System.String CheckValue
            {
                get
                {
                    System.Boolean? data = entity.CheckValue;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CheckValue = null;
                    else entity.CheckValue = Convert.ToBoolean(value);
                }
            }
            public System.String IsUsingRange
            {
                get
                {
                    System.Boolean? data = entity.IsUsingRange;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsUsingRange = null;
                    else entity.IsUsingRange = Convert.ToBoolean(value);
                }
            }
            public System.String CreateByUserID
            {
                get
                {
                    System.String data = entity.CreateByUserID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CreateByUserID = null;
                    else entity.CreateByUserID = Convert.ToString(value);
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
            private esNutritionCareAssessmentQuestionDiagnose entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esNutritionCareAssessmentQuestionDiagnoseQuery query)
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
                throw new Exception("esNutritionCareAssessmentQuestionDiagnose can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class NutritionCareAssessmentQuestionDiagnose : esNutritionCareAssessmentQuestionDiagnose
    {
    }

    [Serializable]
    abstract public class esNutritionCareAssessmentQuestionDiagnoseQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return NutritionCareAssessmentQuestionDiagnoseMetadata.Meta();
            }
        }

        public esQueryItem QuestionID
        {
            get
            {
                return new esQueryItem(this, NutritionCareAssessmentQuestionDiagnoseMetadata.ColumnNames.QuestionID, esSystemType.String);
            }
        }

        public esQueryItem TerminologyID
        {
            get
            {
                return new esQueryItem(this, NutritionCareAssessmentQuestionDiagnoseMetadata.ColumnNames.TerminologyID, esSystemType.String);
            }
        }

        public esQueryItem AgeInMonthStart
        {
            get
            {
                return new esQueryItem(this, NutritionCareAssessmentQuestionDiagnoseMetadata.ColumnNames.AgeInMonthStart, esSystemType.Int32);
            }
        }

        public esQueryItem AgeInMonthEnd
        {
            get
            {
                return new esQueryItem(this, NutritionCareAssessmentQuestionDiagnoseMetadata.ColumnNames.AgeInMonthEnd, esSystemType.Int32);
            }
        }

        public esQueryItem SRAnswerType
        {
            get
            {
                return new esQueryItem(this, NutritionCareAssessmentQuestionDiagnoseMetadata.ColumnNames.SRAnswerType, esSystemType.String);
            }
        }

        public esQueryItem Operand
        {
            get
            {
                return new esQueryItem(this, NutritionCareAssessmentQuestionDiagnoseMetadata.ColumnNames.Operand, esSystemType.String);
            }
        }

        public esQueryItem AcceptedText
        {
            get
            {
                return new esQueryItem(this, NutritionCareAssessmentQuestionDiagnoseMetadata.ColumnNames.AcceptedText, esSystemType.String);
            }
        }

        public esQueryItem AcceptedNum
        {
            get
            {
                return new esQueryItem(this, NutritionCareAssessmentQuestionDiagnoseMetadata.ColumnNames.AcceptedNum, esSystemType.Decimal);
            }
        }

        public esQueryItem AcceptedNum2
        {
            get
            {
                return new esQueryItem(this, NutritionCareAssessmentQuestionDiagnoseMetadata.ColumnNames.AcceptedNum2, esSystemType.Decimal);
            }
        }

        public esQueryItem CheckValue
        {
            get
            {
                return new esQueryItem(this, NutritionCareAssessmentQuestionDiagnoseMetadata.ColumnNames.CheckValue, esSystemType.Boolean);
            }
        }

        public esQueryItem IsUsingRange
        {
            get
            {
                return new esQueryItem(this, NutritionCareAssessmentQuestionDiagnoseMetadata.ColumnNames.IsUsingRange, esSystemType.Boolean);
            }
        }

        public esQueryItem CreateByUserID
        {
            get
            {
                return new esQueryItem(this, NutritionCareAssessmentQuestionDiagnoseMetadata.ColumnNames.CreateByUserID, esSystemType.String);
            }
        }

        public esQueryItem CreateDateTime
        {
            get
            {
                return new esQueryItem(this, NutritionCareAssessmentQuestionDiagnoseMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, NutritionCareAssessmentQuestionDiagnoseMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, NutritionCareAssessmentQuestionDiagnoseMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("NutritionCareAssessmentQuestionDiagnoseCollection")]
    public partial class NutritionCareAssessmentQuestionDiagnoseCollection : esNutritionCareAssessmentQuestionDiagnoseCollection, IEnumerable<NutritionCareAssessmentQuestionDiagnose>
    {
        public NutritionCareAssessmentQuestionDiagnoseCollection()
        {

        }

        public static implicit operator List<NutritionCareAssessmentQuestionDiagnose>(NutritionCareAssessmentQuestionDiagnoseCollection coll)
        {
            List<NutritionCareAssessmentQuestionDiagnose> list = new List<NutritionCareAssessmentQuestionDiagnose>();

            foreach (NutritionCareAssessmentQuestionDiagnose emp in coll)
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
                return NutritionCareAssessmentQuestionDiagnoseMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new NutritionCareAssessmentQuestionDiagnoseQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new NutritionCareAssessmentQuestionDiagnose(row);
        }

        override protected esEntity CreateEntity()
        {
            return new NutritionCareAssessmentQuestionDiagnose();
        }

        #endregion

        [BrowsableAttribute(false)]
        public NutritionCareAssessmentQuestionDiagnoseQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new NutritionCareAssessmentQuestionDiagnoseQuery();
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
        public bool Load(NutritionCareAssessmentQuestionDiagnoseQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public NutritionCareAssessmentQuestionDiagnose AddNew()
        {
            NutritionCareAssessmentQuestionDiagnose entity = base.AddNewEntity() as NutritionCareAssessmentQuestionDiagnose;

            return entity;
        }
        public NutritionCareAssessmentQuestionDiagnose FindByPrimaryKey(String questionID, String terminologyID, Int32 ageInMonthStart, Int32 ageInMonthEnd)
        {
            return base.FindByPrimaryKey(questionID, terminologyID, ageInMonthStart, ageInMonthEnd) as NutritionCareAssessmentQuestionDiagnose;
        }

        #region IEnumerable< NutritionCareAssessmentQuestionDiagnose> Members

        IEnumerator<NutritionCareAssessmentQuestionDiagnose> IEnumerable<NutritionCareAssessmentQuestionDiagnose>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as NutritionCareAssessmentQuestionDiagnose;
            }
        }

        #endregion

        private NutritionCareAssessmentQuestionDiagnoseQuery query;
    }


    /// <summary>
    /// Encapsulates the 'NutritionCareAssessmentQuestionDiagnose' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("NutritionCareAssessmentQuestionDiagnose ({QuestionID, TerminologyID, AgeInMonthStart, AgeInMonthEnd})")]
    [Serializable]
    public partial class NutritionCareAssessmentQuestionDiagnose : esNutritionCareAssessmentQuestionDiagnose
    {
        public NutritionCareAssessmentQuestionDiagnose()
        {
        }

        public NutritionCareAssessmentQuestionDiagnose(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return NutritionCareAssessmentQuestionDiagnoseMetadata.Meta();
            }
        }

        override protected esNutritionCareAssessmentQuestionDiagnoseQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new NutritionCareAssessmentQuestionDiagnoseQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public NutritionCareAssessmentQuestionDiagnoseQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new NutritionCareAssessmentQuestionDiagnoseQuery();
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
        public bool Load(NutritionCareAssessmentQuestionDiagnoseQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private NutritionCareAssessmentQuestionDiagnoseQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class NutritionCareAssessmentQuestionDiagnoseQuery : esNutritionCareAssessmentQuestionDiagnoseQuery
    {
        public NutritionCareAssessmentQuestionDiagnoseQuery()
        {

        }

        public NutritionCareAssessmentQuestionDiagnoseQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "NutritionCareAssessmentQuestionDiagnoseQuery";
        }
    }

    [Serializable]
    public partial class NutritionCareAssessmentQuestionDiagnoseMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected NutritionCareAssessmentQuestionDiagnoseMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(NutritionCareAssessmentQuestionDiagnoseMetadata.ColumnNames.QuestionID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = NutritionCareAssessmentQuestionDiagnoseMetadata.PropertyNames.QuestionID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareAssessmentQuestionDiagnoseMetadata.ColumnNames.TerminologyID, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = NutritionCareAssessmentQuestionDiagnoseMetadata.PropertyNames.TerminologyID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareAssessmentQuestionDiagnoseMetadata.ColumnNames.AgeInMonthStart, 2, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = NutritionCareAssessmentQuestionDiagnoseMetadata.PropertyNames.AgeInMonthStart;
            c.IsInPrimaryKey = true;
            c.NumericPrecision = 10;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareAssessmentQuestionDiagnoseMetadata.ColumnNames.AgeInMonthEnd, 3, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = NutritionCareAssessmentQuestionDiagnoseMetadata.PropertyNames.AgeInMonthEnd;
            c.IsInPrimaryKey = true;
            c.NumericPrecision = 10;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareAssessmentQuestionDiagnoseMetadata.ColumnNames.SRAnswerType, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = NutritionCareAssessmentQuestionDiagnoseMetadata.PropertyNames.SRAnswerType;
            c.CharacterMaxLength = 5;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareAssessmentQuestionDiagnoseMetadata.ColumnNames.Operand, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = NutritionCareAssessmentQuestionDiagnoseMetadata.PropertyNames.Operand;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareAssessmentQuestionDiagnoseMetadata.ColumnNames.AcceptedText, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = NutritionCareAssessmentQuestionDiagnoseMetadata.PropertyNames.AcceptedText;
            c.CharacterMaxLength = 350;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareAssessmentQuestionDiagnoseMetadata.ColumnNames.AcceptedNum, 7, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = NutritionCareAssessmentQuestionDiagnoseMetadata.PropertyNames.AcceptedNum;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareAssessmentQuestionDiagnoseMetadata.ColumnNames.AcceptedNum2, 8, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = NutritionCareAssessmentQuestionDiagnoseMetadata.PropertyNames.AcceptedNum2;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareAssessmentQuestionDiagnoseMetadata.ColumnNames.CheckValue, 9, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = NutritionCareAssessmentQuestionDiagnoseMetadata.PropertyNames.CheckValue;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareAssessmentQuestionDiagnoseMetadata.ColumnNames.IsUsingRange, 10, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = NutritionCareAssessmentQuestionDiagnoseMetadata.PropertyNames.IsUsingRange;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareAssessmentQuestionDiagnoseMetadata.ColumnNames.CreateByUserID, 11, typeof(System.String), esSystemType.String);
            c.PropertyName = NutritionCareAssessmentQuestionDiagnoseMetadata.PropertyNames.CreateByUserID;
            c.CharacterMaxLength = 15;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareAssessmentQuestionDiagnoseMetadata.ColumnNames.CreateDateTime, 12, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = NutritionCareAssessmentQuestionDiagnoseMetadata.PropertyNames.CreateDateTime;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareAssessmentQuestionDiagnoseMetadata.ColumnNames.LastUpdateByUserID, 13, typeof(System.String), esSystemType.String);
            c.PropertyName = NutritionCareAssessmentQuestionDiagnoseMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareAssessmentQuestionDiagnoseMetadata.ColumnNames.LastUpdateDateTime, 14, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = NutritionCareAssessmentQuestionDiagnoseMetadata.PropertyNames.LastUpdateDateTime;
            _columns.Add(c);


        }
        #endregion

        static public NutritionCareAssessmentQuestionDiagnoseMetadata Meta()
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
            public const string QuestionID = "QuestionID";
            public const string TerminologyID = "TerminologyID";
            public const string AgeInMonthStart = "AgeInMonthStart";
            public const string AgeInMonthEnd = "AgeInMonthEnd";
            public const string SRAnswerType = "SRAnswerType";
            public const string Operand = "Operand";
            public const string AcceptedText = "AcceptedText";
            public const string AcceptedNum = "AcceptedNum";
            public const string AcceptedNum2 = "AcceptedNum2";
            public const string CheckValue = "CheckValue";
            public const string IsUsingRange = "IsUsingRange";
            public const string CreateByUserID = "CreateByUserID";
            public const string CreateDateTime = "CreateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string QuestionID = "QuestionID";
            public const string TerminologyID = "TerminologyID";
            public const string AgeInMonthStart = "AgeInMonthStart";
            public const string AgeInMonthEnd = "AgeInMonthEnd";
            public const string SRAnswerType = "SRAnswerType";
            public const string Operand = "Operand";
            public const string AcceptedText = "AcceptedText";
            public const string AcceptedNum = "AcceptedNum";
            public const string AcceptedNum2 = "AcceptedNum2";
            public const string CheckValue = "CheckValue";
            public const string IsUsingRange = "IsUsingRange";
            public const string CreateByUserID = "CreateByUserID";
            public const string CreateDateTime = "CreateDateTime";
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
            lock (typeof(NutritionCareAssessmentQuestionDiagnoseMetadata))
            {
                if (NutritionCareAssessmentQuestionDiagnoseMetadata.mapDelegates == null)
                {
                    NutritionCareAssessmentQuestionDiagnoseMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (NutritionCareAssessmentQuestionDiagnoseMetadata.meta == null)
                {
                    NutritionCareAssessmentQuestionDiagnoseMetadata.meta = new NutritionCareAssessmentQuestionDiagnoseMetadata();
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

                meta.AddTypeMap("QuestionID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("TerminologyID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("AgeInMonthStart", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("AgeInMonthEnd", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("SRAnswerType", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Operand", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("AcceptedText", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("AcceptedNum", new esTypeMap("decimal", "System.Decimal"));
                meta.AddTypeMap("AcceptedNum2", new esTypeMap("decimal", "System.Decimal"));
                meta.AddTypeMap("CheckValue", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsUsingRange", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));


                meta.Source = "NutritionCareAssessmentQuestionDiagnose";
                meta.Destination = "NutritionCareAssessmentQuestionDiagnose";
                meta.spInsert = "proc_NutritionCareAssessmentQuestionDiagnoseInsert";
                meta.spUpdate = "proc_NutritionCareAssessmentQuestionDiagnoseUpdate";
                meta.spDelete = "proc_NutritionCareAssessmentQuestionDiagnoseDelete";
                meta.spLoadAll = "proc_NutritionCareAssessmentQuestionDiagnoseLoadAll";
                meta.spLoadByPrimaryKey = "proc_NutritionCareAssessmentQuestionDiagnoseLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private NutritionCareAssessmentQuestionDiagnoseMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
