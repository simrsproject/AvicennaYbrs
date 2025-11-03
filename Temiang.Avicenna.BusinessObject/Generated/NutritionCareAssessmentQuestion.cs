/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 7/6/2019 10:47:35 AM
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
    abstract public class esNutritionCareAssessmentQuestionCollection : esEntityCollectionWAuditLog
    {
        public esNutritionCareAssessmentQuestionCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "NutritionCareAssessmentQuestionCollection";
        }

        #region Query Logic
        protected void InitQuery(esNutritionCareAssessmentQuestionQuery query)
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
            this.InitQuery(query as esNutritionCareAssessmentQuestionQuery);
        }
        #endregion

        virtual public NutritionCareAssessmentQuestion DetachEntity(NutritionCareAssessmentQuestion entity)
        {
            return base.DetachEntity(entity) as NutritionCareAssessmentQuestion;
        }

        virtual public NutritionCareAssessmentQuestion AttachEntity(NutritionCareAssessmentQuestion entity)
        {
            return base.AttachEntity(entity) as NutritionCareAssessmentQuestion;
        }

        virtual public void Combine(NutritionCareAssessmentQuestionCollection collection)
        {
            base.Combine(collection);
        }

        new public NutritionCareAssessmentQuestion this[int index]
        {
            get
            {
                return base[index] as NutritionCareAssessmentQuestion;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(NutritionCareAssessmentQuestion);
        }
    }

    [Serializable]
    abstract public class esNutritionCareAssessmentQuestion : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esNutritionCareAssessmentQuestionQuery GetDynamicQuery()
        {
            return null;
        }

        public esNutritionCareAssessmentQuestion()
        {
        }

        public esNutritionCareAssessmentQuestion(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String questionID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(questionID);
            else
                return LoadByPrimaryKeyStoredProcedure(questionID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String questionID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(questionID);
            else
                return LoadByPrimaryKeyStoredProcedure(questionID);
        }

        private bool LoadByPrimaryKeyDynamic(String questionID)
        {
            esNutritionCareAssessmentQuestionQuery query = this.GetDynamicQuery();
            query.Where(query.QuestionID == questionID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String questionID)
        {
            esParameters parms = new esParameters();
            parms.Add("QuestionID", questionID);
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
                        case "ParentQuestionID": this.str.ParentQuestionID = (string)value; break;
                        case "IndexNo": this.str.IndexNo = (string)value; break;
                        case "QuestionLevel": this.str.QuestionLevel = (string)value; break;
                        case "QuestionText": this.str.QuestionText = (string)value; break;
                        case "QuestionShortText": this.str.QuestionShortText = (string)value; break;
                        case "SRAnswerType": this.str.SRAnswerType = (string)value; break;
                        case "AnswerDecimalDigit": this.str.AnswerDecimalDigit = (string)value; break;
                        case "AnswerPrefix": this.str.AnswerPrefix = (string)value; break;
                        case "AnswerSuffix": this.str.AnswerSuffix = (string)value; break;
                        case "AnswerWidth": this.str.AnswerWidth = (string)value; break;
                        case "AnswerWidth2": this.str.AnswerWidth2 = (string)value; break;
                        case "QuestionAnswerSelectionID": this.str.QuestionAnswerSelectionID = (string)value; break;
                        case "QuestionAnswerDefaultSelectionID": this.str.QuestionAnswerDefaultSelectionID = (string)value; break;
                        case "QuestionAnswerSelectionID2": this.str.QuestionAnswerSelectionID2 = (string)value; break;
                        case "QuestionAnswerDefaultSelectionID2": this.str.QuestionAnswerDefaultSelectionID2 = (string)value; break;
                        case "Formula": this.str.Formula = (string)value; break;
                        case "IsAlwaysPrint": this.str.IsAlwaysPrint = (string)value; break;
                        case "IsMandatory": this.str.IsMandatory = (string)value; break;
                        case "IsSubjective": this.str.IsSubjective = (string)value; break;
                        case "IsObjective": this.str.IsObjective = (string)value; break;
                        case "RelatedQuestionID": this.str.RelatedQuestionID = (string)value; break;
                        case "IsActive": this.str.IsActive = (string)value; break;
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
                        case "IndexNo":

                            if (value == null || value is System.Int32)
                                this.IndexNo = (System.Int32?)value;
                            break;
                        case "QuestionLevel":

                            if (value == null || value is System.Int32)
                                this.QuestionLevel = (System.Int32?)value;
                            break;
                        case "AnswerDecimalDigit":

                            if (value == null || value is System.Int32)
                                this.AnswerDecimalDigit = (System.Int32?)value;
                            break;
                        case "AnswerWidth":

                            if (value == null || value is System.Int32)
                                this.AnswerWidth = (System.Int32?)value;
                            break;
                        case "AnswerWidth2":

                            if (value == null || value is System.Int32)
                                this.AnswerWidth2 = (System.Int32?)value;
                            break;
                        case "IsAlwaysPrint":

                            if (value == null || value is System.Boolean)
                                this.IsAlwaysPrint = (System.Boolean?)value;
                            break;
                        case "IsMandatory":

                            if (value == null || value is System.Boolean)
                                this.IsMandatory = (System.Boolean?)value;
                            break;
                        case "IsSubjective":

                            if (value == null || value is System.Boolean)
                                this.IsSubjective = (System.Boolean?)value;
                            break;
                        case "IsObjective":

                            if (value == null || value is System.Boolean)
                                this.IsObjective = (System.Boolean?)value;
                            break;
                        case "IsActive":

                            if (value == null || value is System.Boolean)
                                this.IsActive = (System.Boolean?)value;
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
        /// Maps to NutritionCareAssessmentQuestion.QuestionID
        /// </summary>
        virtual public System.String QuestionID
        {
            get
            {
                return base.GetSystemString(NutritionCareAssessmentQuestionMetadata.ColumnNames.QuestionID);
            }

            set
            {
                base.SetSystemString(NutritionCareAssessmentQuestionMetadata.ColumnNames.QuestionID, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareAssessmentQuestion.ParentQuestionID
        /// </summary>
        virtual public System.String ParentQuestionID
        {
            get
            {
                return base.GetSystemString(NutritionCareAssessmentQuestionMetadata.ColumnNames.ParentQuestionID);
            }

            set
            {
                base.SetSystemString(NutritionCareAssessmentQuestionMetadata.ColumnNames.ParentQuestionID, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareAssessmentQuestion.IndexNo
        /// </summary>
        virtual public System.Int32? IndexNo
        {
            get
            {
                return base.GetSystemInt32(NutritionCareAssessmentQuestionMetadata.ColumnNames.IndexNo);
            }

            set
            {
                base.SetSystemInt32(NutritionCareAssessmentQuestionMetadata.ColumnNames.IndexNo, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareAssessmentQuestion.QuestionLevel
        /// </summary>
        virtual public System.Int32? QuestionLevel
        {
            get
            {
                return base.GetSystemInt32(NutritionCareAssessmentQuestionMetadata.ColumnNames.QuestionLevel);
            }

            set
            {
                base.SetSystemInt32(NutritionCareAssessmentQuestionMetadata.ColumnNames.QuestionLevel, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareAssessmentQuestion.QuestionText
        /// </summary>
        virtual public System.String QuestionText
        {
            get
            {
                return base.GetSystemString(NutritionCareAssessmentQuestionMetadata.ColumnNames.QuestionText);
            }

            set
            {
                base.SetSystemString(NutritionCareAssessmentQuestionMetadata.ColumnNames.QuestionText, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareAssessmentQuestion.QuestionShortText
        /// </summary>
        virtual public System.String QuestionShortText
        {
            get
            {
                return base.GetSystemString(NutritionCareAssessmentQuestionMetadata.ColumnNames.QuestionShortText);
            }

            set
            {
                base.SetSystemString(NutritionCareAssessmentQuestionMetadata.ColumnNames.QuestionShortText, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareAssessmentQuestion.SRAnswerType
        /// </summary>
        virtual public System.String SRAnswerType
        {
            get
            {
                return base.GetSystemString(NutritionCareAssessmentQuestionMetadata.ColumnNames.SRAnswerType);
            }

            set
            {
                base.SetSystemString(NutritionCareAssessmentQuestionMetadata.ColumnNames.SRAnswerType, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareAssessmentQuestion.AnswerDecimalDigit
        /// </summary>
        virtual public System.Int32? AnswerDecimalDigit
        {
            get
            {
                return base.GetSystemInt32(NutritionCareAssessmentQuestionMetadata.ColumnNames.AnswerDecimalDigit);
            }

            set
            {
                base.SetSystemInt32(NutritionCareAssessmentQuestionMetadata.ColumnNames.AnswerDecimalDigit, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareAssessmentQuestion.AnswerPrefix
        /// </summary>
        virtual public System.String AnswerPrefix
        {
            get
            {
                return base.GetSystemString(NutritionCareAssessmentQuestionMetadata.ColumnNames.AnswerPrefix);
            }

            set
            {
                base.SetSystemString(NutritionCareAssessmentQuestionMetadata.ColumnNames.AnswerPrefix, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareAssessmentQuestion.AnswerSuffix
        /// </summary>
        virtual public System.String AnswerSuffix
        {
            get
            {
                return base.GetSystemString(NutritionCareAssessmentQuestionMetadata.ColumnNames.AnswerSuffix);
            }

            set
            {
                base.SetSystemString(NutritionCareAssessmentQuestionMetadata.ColumnNames.AnswerSuffix, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareAssessmentQuestion.AnswerWidth
        /// </summary>
        virtual public System.Int32? AnswerWidth
        {
            get
            {
                return base.GetSystemInt32(NutritionCareAssessmentQuestionMetadata.ColumnNames.AnswerWidth);
            }

            set
            {
                base.SetSystemInt32(NutritionCareAssessmentQuestionMetadata.ColumnNames.AnswerWidth, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareAssessmentQuestion.AnswerWidth2
        /// </summary>
        virtual public System.Int32? AnswerWidth2
        {
            get
            {
                return base.GetSystemInt32(NutritionCareAssessmentQuestionMetadata.ColumnNames.AnswerWidth2);
            }

            set
            {
                base.SetSystemInt32(NutritionCareAssessmentQuestionMetadata.ColumnNames.AnswerWidth2, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareAssessmentQuestion.QuestionAnswerSelectionID
        /// </summary>
        virtual public System.String QuestionAnswerSelectionID
        {
            get
            {
                return base.GetSystemString(NutritionCareAssessmentQuestionMetadata.ColumnNames.QuestionAnswerSelectionID);
            }

            set
            {
                base.SetSystemString(NutritionCareAssessmentQuestionMetadata.ColumnNames.QuestionAnswerSelectionID, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareAssessmentQuestion.QuestionAnswerDefaultSelectionID
        /// </summary>
        virtual public System.String QuestionAnswerDefaultSelectionID
        {
            get
            {
                return base.GetSystemString(NutritionCareAssessmentQuestionMetadata.ColumnNames.QuestionAnswerDefaultSelectionID);
            }

            set
            {
                base.SetSystemString(NutritionCareAssessmentQuestionMetadata.ColumnNames.QuestionAnswerDefaultSelectionID, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareAssessmentQuestion.QuestionAnswerSelectionID2
        /// </summary>
        virtual public System.String QuestionAnswerSelectionID2
        {
            get
            {
                return base.GetSystemString(NutritionCareAssessmentQuestionMetadata.ColumnNames.QuestionAnswerSelectionID2);
            }

            set
            {
                base.SetSystemString(NutritionCareAssessmentQuestionMetadata.ColumnNames.QuestionAnswerSelectionID2, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareAssessmentQuestion.QuestionAnswerDefaultSelectionID2
        /// </summary>
        virtual public System.String QuestionAnswerDefaultSelectionID2
        {
            get
            {
                return base.GetSystemString(NutritionCareAssessmentQuestionMetadata.ColumnNames.QuestionAnswerDefaultSelectionID2);
            }

            set
            {
                base.SetSystemString(NutritionCareAssessmentQuestionMetadata.ColumnNames.QuestionAnswerDefaultSelectionID2, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareAssessmentQuestion.Formula
        /// </summary>
        virtual public System.String Formula
        {
            get
            {
                return base.GetSystemString(NutritionCareAssessmentQuestionMetadata.ColumnNames.Formula);
            }

            set
            {
                base.SetSystemString(NutritionCareAssessmentQuestionMetadata.ColumnNames.Formula, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareAssessmentQuestion.IsAlwaysPrint
        /// </summary>
        virtual public System.Boolean? IsAlwaysPrint
        {
            get
            {
                return base.GetSystemBoolean(NutritionCareAssessmentQuestionMetadata.ColumnNames.IsAlwaysPrint);
            }

            set
            {
                base.SetSystemBoolean(NutritionCareAssessmentQuestionMetadata.ColumnNames.IsAlwaysPrint, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareAssessmentQuestion.IsMandatory
        /// </summary>
        virtual public System.Boolean? IsMandatory
        {
            get
            {
                return base.GetSystemBoolean(NutritionCareAssessmentQuestionMetadata.ColumnNames.IsMandatory);
            }

            set
            {
                base.SetSystemBoolean(NutritionCareAssessmentQuestionMetadata.ColumnNames.IsMandatory, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareAssessmentQuestion.IsSubjective
        /// </summary>
        virtual public System.Boolean? IsSubjective
        {
            get
            {
                return base.GetSystemBoolean(NutritionCareAssessmentQuestionMetadata.ColumnNames.IsSubjective);
            }

            set
            {
                base.SetSystemBoolean(NutritionCareAssessmentQuestionMetadata.ColumnNames.IsSubjective, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareAssessmentQuestion.IsObjective
        /// </summary>
        virtual public System.Boolean? IsObjective
        {
            get
            {
                return base.GetSystemBoolean(NutritionCareAssessmentQuestionMetadata.ColumnNames.IsObjective);
            }

            set
            {
                base.SetSystemBoolean(NutritionCareAssessmentQuestionMetadata.ColumnNames.IsObjective, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareAssessmentQuestion.RelatedQuestionID
        /// </summary>
        virtual public System.String RelatedQuestionID
        {
            get
            {
                return base.GetSystemString(NutritionCareAssessmentQuestionMetadata.ColumnNames.RelatedQuestionID);
            }

            set
            {
                base.SetSystemString(NutritionCareAssessmentQuestionMetadata.ColumnNames.RelatedQuestionID, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareAssessmentQuestion.IsActive
        /// </summary>
        virtual public System.Boolean? IsActive
        {
            get
            {
                return base.GetSystemBoolean(NutritionCareAssessmentQuestionMetadata.ColumnNames.IsActive);
            }

            set
            {
                base.SetSystemBoolean(NutritionCareAssessmentQuestionMetadata.ColumnNames.IsActive, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareAssessmentQuestion.CreateByUserID
        /// </summary>
        virtual public System.String CreateByUserID
        {
            get
            {
                return base.GetSystemString(NutritionCareAssessmentQuestionMetadata.ColumnNames.CreateByUserID);
            }

            set
            {
                base.SetSystemString(NutritionCareAssessmentQuestionMetadata.ColumnNames.CreateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareAssessmentQuestion.CreateDateTime
        /// </summary>
        virtual public System.DateTime? CreateDateTime
        {
            get
            {
                return base.GetSystemDateTime(NutritionCareAssessmentQuestionMetadata.ColumnNames.CreateDateTime);
            }

            set
            {
                base.SetSystemDateTime(NutritionCareAssessmentQuestionMetadata.ColumnNames.CreateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareAssessmentQuestion.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(NutritionCareAssessmentQuestionMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(NutritionCareAssessmentQuestionMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareAssessmentQuestion.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(NutritionCareAssessmentQuestionMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(NutritionCareAssessmentQuestionMetadata.ColumnNames.LastUpdateDateTime, value);
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
            public esStrings(esNutritionCareAssessmentQuestion entity)
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
            public System.String ParentQuestionID
            {
                get
                {
                    System.String data = entity.ParentQuestionID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ParentQuestionID = null;
                    else entity.ParentQuestionID = Convert.ToString(value);
                }
            }
            public System.String IndexNo
            {
                get
                {
                    System.Int32? data = entity.IndexNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IndexNo = null;
                    else entity.IndexNo = Convert.ToInt32(value);
                }
            }
            public System.String QuestionLevel
            {
                get
                {
                    System.Int32? data = entity.QuestionLevel;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.QuestionLevel = null;
                    else entity.QuestionLevel = Convert.ToInt32(value);
                }
            }
            public System.String QuestionText
            {
                get
                {
                    System.String data = entity.QuestionText;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.QuestionText = null;
                    else entity.QuestionText = Convert.ToString(value);
                }
            }
            public System.String QuestionShortText
            {
                get
                {
                    System.String data = entity.QuestionShortText;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.QuestionShortText = null;
                    else entity.QuestionShortText = Convert.ToString(value);
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
            public System.String AnswerDecimalDigit
            {
                get
                {
                    System.Int32? data = entity.AnswerDecimalDigit;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.AnswerDecimalDigit = null;
                    else entity.AnswerDecimalDigit = Convert.ToInt32(value);
                }
            }
            public System.String AnswerPrefix
            {
                get
                {
                    System.String data = entity.AnswerPrefix;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.AnswerPrefix = null;
                    else entity.AnswerPrefix = Convert.ToString(value);
                }
            }
            public System.String AnswerSuffix
            {
                get
                {
                    System.String data = entity.AnswerSuffix;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.AnswerSuffix = null;
                    else entity.AnswerSuffix = Convert.ToString(value);
                }
            }
            public System.String AnswerWidth
            {
                get
                {
                    System.Int32? data = entity.AnswerWidth;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.AnswerWidth = null;
                    else entity.AnswerWidth = Convert.ToInt32(value);
                }
            }
            public System.String AnswerWidth2
            {
                get
                {
                    System.Int32? data = entity.AnswerWidth2;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.AnswerWidth2 = null;
                    else entity.AnswerWidth2 = Convert.ToInt32(value);
                }
            }
            public System.String QuestionAnswerSelectionID
            {
                get
                {
                    System.String data = entity.QuestionAnswerSelectionID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.QuestionAnswerSelectionID = null;
                    else entity.QuestionAnswerSelectionID = Convert.ToString(value);
                }
            }
            public System.String QuestionAnswerDefaultSelectionID
            {
                get
                {
                    System.String data = entity.QuestionAnswerDefaultSelectionID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.QuestionAnswerDefaultSelectionID = null;
                    else entity.QuestionAnswerDefaultSelectionID = Convert.ToString(value);
                }
            }
            public System.String QuestionAnswerSelectionID2
            {
                get
                {
                    System.String data = entity.QuestionAnswerSelectionID2;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.QuestionAnswerSelectionID2 = null;
                    else entity.QuestionAnswerSelectionID2 = Convert.ToString(value);
                }
            }
            public System.String QuestionAnswerDefaultSelectionID2
            {
                get
                {
                    System.String data = entity.QuestionAnswerDefaultSelectionID2;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.QuestionAnswerDefaultSelectionID2 = null;
                    else entity.QuestionAnswerDefaultSelectionID2 = Convert.ToString(value);
                }
            }
            public System.String Formula
            {
                get
                {
                    System.String data = entity.Formula;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Formula = null;
                    else entity.Formula = Convert.ToString(value);
                }
            }
            public System.String IsAlwaysPrint
            {
                get
                {
                    System.Boolean? data = entity.IsAlwaysPrint;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsAlwaysPrint = null;
                    else entity.IsAlwaysPrint = Convert.ToBoolean(value);
                }
            }
            public System.String IsMandatory
            {
                get
                {
                    System.Boolean? data = entity.IsMandatory;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsMandatory = null;
                    else entity.IsMandatory = Convert.ToBoolean(value);
                }
            }
            public System.String IsSubjective
            {
                get
                {
                    System.Boolean? data = entity.IsSubjective;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsSubjective = null;
                    else entity.IsSubjective = Convert.ToBoolean(value);
                }
            }
            public System.String IsObjective
            {
                get
                {
                    System.Boolean? data = entity.IsObjective;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsObjective = null;
                    else entity.IsObjective = Convert.ToBoolean(value);
                }
            }
            public System.String RelatedQuestionID
            {
                get
                {
                    System.String data = entity.RelatedQuestionID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RelatedQuestionID = null;
                    else entity.RelatedQuestionID = Convert.ToString(value);
                }
            }
            public System.String IsActive
            {
                get
                {
                    System.Boolean? data = entity.IsActive;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsActive = null;
                    else entity.IsActive = Convert.ToBoolean(value);
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
            private esNutritionCareAssessmentQuestion entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esNutritionCareAssessmentQuestionQuery query)
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
                throw new Exception("esNutritionCareAssessmentQuestion can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class NutritionCareAssessmentQuestion : esNutritionCareAssessmentQuestion
    {
    }

    [Serializable]
    abstract public class esNutritionCareAssessmentQuestionQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return NutritionCareAssessmentQuestionMetadata.Meta();
            }
        }

        public esQueryItem QuestionID
        {
            get
            {
                return new esQueryItem(this, NutritionCareAssessmentQuestionMetadata.ColumnNames.QuestionID, esSystemType.String);
            }
        }

        public esQueryItem ParentQuestionID
        {
            get
            {
                return new esQueryItem(this, NutritionCareAssessmentQuestionMetadata.ColumnNames.ParentQuestionID, esSystemType.String);
            }
        }

        public esQueryItem IndexNo
        {
            get
            {
                return new esQueryItem(this, NutritionCareAssessmentQuestionMetadata.ColumnNames.IndexNo, esSystemType.Int32);
            }
        }

        public esQueryItem QuestionLevel
        {
            get
            {
                return new esQueryItem(this, NutritionCareAssessmentQuestionMetadata.ColumnNames.QuestionLevel, esSystemType.Int32);
            }
        }

        public esQueryItem QuestionText
        {
            get
            {
                return new esQueryItem(this, NutritionCareAssessmentQuestionMetadata.ColumnNames.QuestionText, esSystemType.String);
            }
        }

        public esQueryItem QuestionShortText
        {
            get
            {
                return new esQueryItem(this, NutritionCareAssessmentQuestionMetadata.ColumnNames.QuestionShortText, esSystemType.String);
            }
        }

        public esQueryItem SRAnswerType
        {
            get
            {
                return new esQueryItem(this, NutritionCareAssessmentQuestionMetadata.ColumnNames.SRAnswerType, esSystemType.String);
            }
        }

        public esQueryItem AnswerDecimalDigit
        {
            get
            {
                return new esQueryItem(this, NutritionCareAssessmentQuestionMetadata.ColumnNames.AnswerDecimalDigit, esSystemType.Int32);
            }
        }

        public esQueryItem AnswerPrefix
        {
            get
            {
                return new esQueryItem(this, NutritionCareAssessmentQuestionMetadata.ColumnNames.AnswerPrefix, esSystemType.String);
            }
        }

        public esQueryItem AnswerSuffix
        {
            get
            {
                return new esQueryItem(this, NutritionCareAssessmentQuestionMetadata.ColumnNames.AnswerSuffix, esSystemType.String);
            }
        }

        public esQueryItem AnswerWidth
        {
            get
            {
                return new esQueryItem(this, NutritionCareAssessmentQuestionMetadata.ColumnNames.AnswerWidth, esSystemType.Int32);
            }
        }

        public esQueryItem AnswerWidth2
        {
            get
            {
                return new esQueryItem(this, NutritionCareAssessmentQuestionMetadata.ColumnNames.AnswerWidth2, esSystemType.Int32);
            }
        }

        public esQueryItem QuestionAnswerSelectionID
        {
            get
            {
                return new esQueryItem(this, NutritionCareAssessmentQuestionMetadata.ColumnNames.QuestionAnswerSelectionID, esSystemType.String);
            }
        }

        public esQueryItem QuestionAnswerDefaultSelectionID
        {
            get
            {
                return new esQueryItem(this, NutritionCareAssessmentQuestionMetadata.ColumnNames.QuestionAnswerDefaultSelectionID, esSystemType.String);
            }
        }

        public esQueryItem QuestionAnswerSelectionID2
        {
            get
            {
                return new esQueryItem(this, NutritionCareAssessmentQuestionMetadata.ColumnNames.QuestionAnswerSelectionID2, esSystemType.String);
            }
        }

        public esQueryItem QuestionAnswerDefaultSelectionID2
        {
            get
            {
                return new esQueryItem(this, NutritionCareAssessmentQuestionMetadata.ColumnNames.QuestionAnswerDefaultSelectionID2, esSystemType.String);
            }
        }

        public esQueryItem Formula
        {
            get
            {
                return new esQueryItem(this, NutritionCareAssessmentQuestionMetadata.ColumnNames.Formula, esSystemType.String);
            }
        }

        public esQueryItem IsAlwaysPrint
        {
            get
            {
                return new esQueryItem(this, NutritionCareAssessmentQuestionMetadata.ColumnNames.IsAlwaysPrint, esSystemType.Boolean);
            }
        }

        public esQueryItem IsMandatory
        {
            get
            {
                return new esQueryItem(this, NutritionCareAssessmentQuestionMetadata.ColumnNames.IsMandatory, esSystemType.Boolean);
            }
        }

        public esQueryItem IsSubjective
        {
            get
            {
                return new esQueryItem(this, NutritionCareAssessmentQuestionMetadata.ColumnNames.IsSubjective, esSystemType.Boolean);
            }
        }

        public esQueryItem IsObjective
        {
            get
            {
                return new esQueryItem(this, NutritionCareAssessmentQuestionMetadata.ColumnNames.IsObjective, esSystemType.Boolean);
            }
        }

        public esQueryItem RelatedQuestionID
        {
            get
            {
                return new esQueryItem(this, NutritionCareAssessmentQuestionMetadata.ColumnNames.RelatedQuestionID, esSystemType.String);
            }
        }

        public esQueryItem IsActive
        {
            get
            {
                return new esQueryItem(this, NutritionCareAssessmentQuestionMetadata.ColumnNames.IsActive, esSystemType.Boolean);
            }
        }

        public esQueryItem CreateByUserID
        {
            get
            {
                return new esQueryItem(this, NutritionCareAssessmentQuestionMetadata.ColumnNames.CreateByUserID, esSystemType.String);
            }
        }

        public esQueryItem CreateDateTime
        {
            get
            {
                return new esQueryItem(this, NutritionCareAssessmentQuestionMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, NutritionCareAssessmentQuestionMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, NutritionCareAssessmentQuestionMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("NutritionCareAssessmentQuestionCollection")]
    public partial class NutritionCareAssessmentQuestionCollection : esNutritionCareAssessmentQuestionCollection, IEnumerable<NutritionCareAssessmentQuestion>
    {
        public NutritionCareAssessmentQuestionCollection()
        {

        }

        public static implicit operator List<NutritionCareAssessmentQuestion>(NutritionCareAssessmentQuestionCollection coll)
        {
            List<NutritionCareAssessmentQuestion> list = new List<NutritionCareAssessmentQuestion>();

            foreach (NutritionCareAssessmentQuestion emp in coll)
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
                return NutritionCareAssessmentQuestionMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new NutritionCareAssessmentQuestionQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new NutritionCareAssessmentQuestion(row);
        }

        override protected esEntity CreateEntity()
        {
            return new NutritionCareAssessmentQuestion();
        }

        #endregion

        [BrowsableAttribute(false)]
        public NutritionCareAssessmentQuestionQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new NutritionCareAssessmentQuestionQuery();
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
        public bool Load(NutritionCareAssessmentQuestionQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public NutritionCareAssessmentQuestion AddNew()
        {
            NutritionCareAssessmentQuestion entity = base.AddNewEntity() as NutritionCareAssessmentQuestion;

            return entity;
        }
        public NutritionCareAssessmentQuestion FindByPrimaryKey(String questionID)
        {
            return base.FindByPrimaryKey(questionID) as NutritionCareAssessmentQuestion;
        }

        #region IEnumerable< NutritionCareAssessmentQuestion> Members

        IEnumerator<NutritionCareAssessmentQuestion> IEnumerable<NutritionCareAssessmentQuestion>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as NutritionCareAssessmentQuestion;
            }
        }

        #endregion

        private NutritionCareAssessmentQuestionQuery query;
    }


    /// <summary>
    /// Encapsulates the 'NutritionCareAssessmentQuestion' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("NutritionCareAssessmentQuestion ({QuestionID})")]
    [Serializable]
    public partial class NutritionCareAssessmentQuestion : esNutritionCareAssessmentQuestion
    {
        public NutritionCareAssessmentQuestion()
        {
        }

        public NutritionCareAssessmentQuestion(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return NutritionCareAssessmentQuestionMetadata.Meta();
            }
        }

        override protected esNutritionCareAssessmentQuestionQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new NutritionCareAssessmentQuestionQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public NutritionCareAssessmentQuestionQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new NutritionCareAssessmentQuestionQuery();
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
        public bool Load(NutritionCareAssessmentQuestionQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private NutritionCareAssessmentQuestionQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class NutritionCareAssessmentQuestionQuery : esNutritionCareAssessmentQuestionQuery
    {
        public NutritionCareAssessmentQuestionQuery()
        {

        }

        public NutritionCareAssessmentQuestionQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "NutritionCareAssessmentQuestionQuery";
        }
    }

    [Serializable]
    public partial class NutritionCareAssessmentQuestionMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected NutritionCareAssessmentQuestionMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(NutritionCareAssessmentQuestionMetadata.ColumnNames.QuestionID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = NutritionCareAssessmentQuestionMetadata.PropertyNames.QuestionID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareAssessmentQuestionMetadata.ColumnNames.ParentQuestionID, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = NutritionCareAssessmentQuestionMetadata.PropertyNames.ParentQuestionID;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareAssessmentQuestionMetadata.ColumnNames.IndexNo, 2, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = NutritionCareAssessmentQuestionMetadata.PropertyNames.IndexNo;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareAssessmentQuestionMetadata.ColumnNames.QuestionLevel, 3, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = NutritionCareAssessmentQuestionMetadata.PropertyNames.QuestionLevel;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareAssessmentQuestionMetadata.ColumnNames.QuestionText, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = NutritionCareAssessmentQuestionMetadata.PropertyNames.QuestionText;
            c.CharacterMaxLength = 500;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareAssessmentQuestionMetadata.ColumnNames.QuestionShortText, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = NutritionCareAssessmentQuestionMetadata.PropertyNames.QuestionShortText;
            c.CharacterMaxLength = 200;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareAssessmentQuestionMetadata.ColumnNames.SRAnswerType, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = NutritionCareAssessmentQuestionMetadata.PropertyNames.SRAnswerType;
            c.CharacterMaxLength = 3;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareAssessmentQuestionMetadata.ColumnNames.AnswerDecimalDigit, 7, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = NutritionCareAssessmentQuestionMetadata.PropertyNames.AnswerDecimalDigit;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareAssessmentQuestionMetadata.ColumnNames.AnswerPrefix, 8, typeof(System.String), esSystemType.String);
            c.PropertyName = NutritionCareAssessmentQuestionMetadata.PropertyNames.AnswerPrefix;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareAssessmentQuestionMetadata.ColumnNames.AnswerSuffix, 9, typeof(System.String), esSystemType.String);
            c.PropertyName = NutritionCareAssessmentQuestionMetadata.PropertyNames.AnswerSuffix;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareAssessmentQuestionMetadata.ColumnNames.AnswerWidth, 10, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = NutritionCareAssessmentQuestionMetadata.PropertyNames.AnswerWidth;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareAssessmentQuestionMetadata.ColumnNames.AnswerWidth2, 11, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = NutritionCareAssessmentQuestionMetadata.PropertyNames.AnswerWidth2;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareAssessmentQuestionMetadata.ColumnNames.QuestionAnswerSelectionID, 12, typeof(System.String), esSystemType.String);
            c.PropertyName = NutritionCareAssessmentQuestionMetadata.PropertyNames.QuestionAnswerSelectionID;
            c.CharacterMaxLength = 100;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareAssessmentQuestionMetadata.ColumnNames.QuestionAnswerDefaultSelectionID, 13, typeof(System.String), esSystemType.String);
            c.PropertyName = NutritionCareAssessmentQuestionMetadata.PropertyNames.QuestionAnswerDefaultSelectionID;
            c.CharacterMaxLength = 100;
            c.HasDefault = true;
            c.Default = @"('')";
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareAssessmentQuestionMetadata.ColumnNames.QuestionAnswerSelectionID2, 14, typeof(System.String), esSystemType.String);
            c.PropertyName = NutritionCareAssessmentQuestionMetadata.PropertyNames.QuestionAnswerSelectionID2;
            c.CharacterMaxLength = 100;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareAssessmentQuestionMetadata.ColumnNames.QuestionAnswerDefaultSelectionID2, 15, typeof(System.String), esSystemType.String);
            c.PropertyName = NutritionCareAssessmentQuestionMetadata.PropertyNames.QuestionAnswerDefaultSelectionID2;
            c.CharacterMaxLength = 100;
            c.HasDefault = true;
            c.Default = @"('')";
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareAssessmentQuestionMetadata.ColumnNames.Formula, 16, typeof(System.String), esSystemType.String);
            c.PropertyName = NutritionCareAssessmentQuestionMetadata.PropertyNames.Formula;
            c.CharacterMaxLength = 300;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareAssessmentQuestionMetadata.ColumnNames.IsAlwaysPrint, 17, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = NutritionCareAssessmentQuestionMetadata.PropertyNames.IsAlwaysPrint;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareAssessmentQuestionMetadata.ColumnNames.IsMandatory, 18, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = NutritionCareAssessmentQuestionMetadata.PropertyNames.IsMandatory;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareAssessmentQuestionMetadata.ColumnNames.IsSubjective, 19, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = NutritionCareAssessmentQuestionMetadata.PropertyNames.IsSubjective;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareAssessmentQuestionMetadata.ColumnNames.IsObjective, 20, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = NutritionCareAssessmentQuestionMetadata.PropertyNames.IsObjective;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareAssessmentQuestionMetadata.ColumnNames.RelatedQuestionID, 21, typeof(System.String), esSystemType.String);
            c.PropertyName = NutritionCareAssessmentQuestionMetadata.PropertyNames.RelatedQuestionID;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareAssessmentQuestionMetadata.ColumnNames.IsActive, 22, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = NutritionCareAssessmentQuestionMetadata.PropertyNames.IsActive;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareAssessmentQuestionMetadata.ColumnNames.CreateByUserID, 23, typeof(System.String), esSystemType.String);
            c.PropertyName = NutritionCareAssessmentQuestionMetadata.PropertyNames.CreateByUserID;
            c.CharacterMaxLength = 15;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareAssessmentQuestionMetadata.ColumnNames.CreateDateTime, 24, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = NutritionCareAssessmentQuestionMetadata.PropertyNames.CreateDateTime;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareAssessmentQuestionMetadata.ColumnNames.LastUpdateByUserID, 25, typeof(System.String), esSystemType.String);
            c.PropertyName = NutritionCareAssessmentQuestionMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareAssessmentQuestionMetadata.ColumnNames.LastUpdateDateTime, 26, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = NutritionCareAssessmentQuestionMetadata.PropertyNames.LastUpdateDateTime;
            _columns.Add(c);


        }
        #endregion

        static public NutritionCareAssessmentQuestionMetadata Meta()
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
            public const string ParentQuestionID = "ParentQuestionID";
            public const string IndexNo = "IndexNo";
            public const string QuestionLevel = "QuestionLevel";
            public const string QuestionText = "QuestionText";
            public const string QuestionShortText = "QuestionShortText";
            public const string SRAnswerType = "SRAnswerType";
            public const string AnswerDecimalDigit = "AnswerDecimalDigit";
            public const string AnswerPrefix = "AnswerPrefix";
            public const string AnswerSuffix = "AnswerSuffix";
            public const string AnswerWidth = "AnswerWidth";
            public const string AnswerWidth2 = "AnswerWidth2";
            public const string QuestionAnswerSelectionID = "QuestionAnswerSelectionID";
            public const string QuestionAnswerDefaultSelectionID = "QuestionAnswerDefaultSelectionID";
            public const string QuestionAnswerSelectionID2 = "QuestionAnswerSelectionID2";
            public const string QuestionAnswerDefaultSelectionID2 = "QuestionAnswerDefaultSelectionID2";
            public const string Formula = "Formula";
            public const string IsAlwaysPrint = "IsAlwaysPrint";
            public const string IsMandatory = "IsMandatory";
            public const string IsSubjective = "IsSubjective";
            public const string IsObjective = "IsObjective";
            public const string RelatedQuestionID = "RelatedQuestionID";
            public const string IsActive = "IsActive";
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
            public const string ParentQuestionID = "ParentQuestionID";
            public const string IndexNo = "IndexNo";
            public const string QuestionLevel = "QuestionLevel";
            public const string QuestionText = "QuestionText";
            public const string QuestionShortText = "QuestionShortText";
            public const string SRAnswerType = "SRAnswerType";
            public const string AnswerDecimalDigit = "AnswerDecimalDigit";
            public const string AnswerPrefix = "AnswerPrefix";
            public const string AnswerSuffix = "AnswerSuffix";
            public const string AnswerWidth = "AnswerWidth";
            public const string AnswerWidth2 = "AnswerWidth2";
            public const string QuestionAnswerSelectionID = "QuestionAnswerSelectionID";
            public const string QuestionAnswerDefaultSelectionID = "QuestionAnswerDefaultSelectionID";
            public const string QuestionAnswerSelectionID2 = "QuestionAnswerSelectionID2";
            public const string QuestionAnswerDefaultSelectionID2 = "QuestionAnswerDefaultSelectionID2";
            public const string Formula = "Formula";
            public const string IsAlwaysPrint = "IsAlwaysPrint";
            public const string IsMandatory = "IsMandatory";
            public const string IsSubjective = "IsSubjective";
            public const string IsObjective = "IsObjective";
            public const string RelatedQuestionID = "RelatedQuestionID";
            public const string IsActive = "IsActive";
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
            lock (typeof(NutritionCareAssessmentQuestionMetadata))
            {
                if (NutritionCareAssessmentQuestionMetadata.mapDelegates == null)
                {
                    NutritionCareAssessmentQuestionMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (NutritionCareAssessmentQuestionMetadata.meta == null)
                {
                    NutritionCareAssessmentQuestionMetadata.meta = new NutritionCareAssessmentQuestionMetadata();
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
                meta.AddTypeMap("ParentQuestionID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IndexNo", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("QuestionLevel", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("QuestionText", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("QuestionShortText", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRAnswerType", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("AnswerDecimalDigit", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("AnswerPrefix", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("AnswerSuffix", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("AnswerWidth", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("AnswerWidth2", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("QuestionAnswerSelectionID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("QuestionAnswerDefaultSelectionID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("QuestionAnswerSelectionID2", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("QuestionAnswerDefaultSelectionID2", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Formula", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsAlwaysPrint", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsMandatory", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsSubjective", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsObjective", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("RelatedQuestionID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));


                meta.Source = "NutritionCareAssessmentQuestion";
                meta.Destination = "NutritionCareAssessmentQuestion";
                meta.spInsert = "proc_NutritionCareAssessmentQuestionInsert";
                meta.spUpdate = "proc_NutritionCareAssessmentQuestionUpdate";
                meta.spDelete = "proc_NutritionCareAssessmentQuestionDelete";
                meta.spLoadAll = "proc_NutritionCareAssessmentQuestionLoadAll";
                meta.spLoadByPrimaryKey = "proc_NutritionCareAssessmentQuestionLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private NutritionCareAssessmentQuestionMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
