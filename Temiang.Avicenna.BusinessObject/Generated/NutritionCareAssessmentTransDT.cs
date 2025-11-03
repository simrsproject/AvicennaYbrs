/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 7/6/2019 9:17:23 AM
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
    abstract public class esNutritionCareAssessmentTransDTCollection : esEntityCollectionWAuditLog
    {
        public esNutritionCareAssessmentTransDTCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "NutritionCareAssessmentTransDTCollection";
        }

        #region Query Logic
        protected void InitQuery(esNutritionCareAssessmentTransDTQuery query)
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
            this.InitQuery(query as esNutritionCareAssessmentTransDTQuery);
        }
        #endregion

        virtual public NutritionCareAssessmentTransDT DetachEntity(NutritionCareAssessmentTransDT entity)
        {
            return base.DetachEntity(entity) as NutritionCareAssessmentTransDT;
        }

        virtual public NutritionCareAssessmentTransDT AttachEntity(NutritionCareAssessmentTransDT entity)
        {
            return base.AttachEntity(entity) as NutritionCareAssessmentTransDT;
        }

        virtual public void Combine(NutritionCareAssessmentTransDTCollection collection)
        {
            base.Combine(collection);
        }

        new public NutritionCareAssessmentTransDT this[int index]
        {
            get
            {
                return base[index] as NutritionCareAssessmentTransDT;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(NutritionCareAssessmentTransDT);
        }
    }

    [Serializable]
    abstract public class esNutritionCareAssessmentTransDT : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esNutritionCareAssessmentTransDTQuery GetDynamicQuery()
        {
            return null;
        }

        public esNutritionCareAssessmentTransDT()
        {
        }

        public esNutritionCareAssessmentTransDT(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(Int64 iD)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(iD);
            else
                return LoadByPrimaryKeyStoredProcedure(iD);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int64 iD)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(iD);
            else
                return LoadByPrimaryKeyStoredProcedure(iD);
        }

        private bool LoadByPrimaryKeyDynamic(Int64 iD)
        {
            esNutritionCareAssessmentTransDTQuery query = this.GetDynamicQuery();
            query.Where(query.ID == iD);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(Int64 iD)
        {
            esParameters parms = new esParameters();
            parms.Add("ID", iD);
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
                        case "ID": this.str.ID = (string)value; break;
                        case "HDID": this.str.HDID = (string)value; break;
                        case "QuestionID": this.str.QuestionID = (string)value; break;
                        case "QuestionText": this.str.QuestionText = (string)value; break;
                        case "IsSubjective": this.str.IsSubjective = (string)value; break;
                        case "IsObjective": this.str.IsObjective = (string)value; break;
                        case "AnswerPrefix": this.str.AnswerPrefix = (string)value; break;
                        case "AnswerSuffix": this.str.AnswerSuffix = (string)value; break;
                        case "AnswerText": this.str.AnswerText = (string)value; break;
                        case "AnswerNum": this.str.AnswerNum = (string)value; break;
                        case "AnswerSelectionLineID": this.str.AnswerSelectionLineID = (string)value; break;
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
                        case "ID":

                            if (value == null || value is System.Int64)
                                this.ID = (System.Int64?)value;
                            break;
                        case "HDID":

                            if (value == null || value is System.Int64)
                                this.HDID = (System.Int64?)value;
                            break;
                        case "IsSubjective":

                            if (value == null || value is System.Boolean)
                                this.IsSubjective = (System.Boolean?)value;
                            break;
                        case "IsObjective":

                            if (value == null || value is System.Boolean)
                                this.IsObjective = (System.Boolean?)value;
                            break;
                        case "AnswerNum":

                            if (value == null || value is System.Decimal)
                                this.AnswerNum = (System.Decimal?)value;
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
        /// Maps to NutritionCareAssessmentTransDT.ID
        /// </summary>
        virtual public System.Int64? ID
        {
            get
            {
                return base.GetSystemInt64(NutritionCareAssessmentTransDTMetadata.ColumnNames.ID);
            }

            set
            {
                base.SetSystemInt64(NutritionCareAssessmentTransDTMetadata.ColumnNames.ID, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareAssessmentTransDT.HDID
        /// </summary>
        virtual public System.Int64? HDID
        {
            get
            {
                return base.GetSystemInt64(NutritionCareAssessmentTransDTMetadata.ColumnNames.HDID);
            }

            set
            {
                base.SetSystemInt64(NutritionCareAssessmentTransDTMetadata.ColumnNames.HDID, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareAssessmentTransDT.QuestionID
        /// </summary>
        virtual public System.String QuestionID
        {
            get
            {
                return base.GetSystemString(NutritionCareAssessmentTransDTMetadata.ColumnNames.QuestionID);
            }

            set
            {
                base.SetSystemString(NutritionCareAssessmentTransDTMetadata.ColumnNames.QuestionID, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareAssessmentTransDT.QuestionText
        /// </summary>
        virtual public System.String QuestionText
        {
            get
            {
                return base.GetSystemString(NutritionCareAssessmentTransDTMetadata.ColumnNames.QuestionText);
            }

            set
            {
                base.SetSystemString(NutritionCareAssessmentTransDTMetadata.ColumnNames.QuestionText, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareAssessmentTransDT.IsSubjective
        /// </summary>
        virtual public System.Boolean? IsSubjective
        {
            get
            {
                return base.GetSystemBoolean(NutritionCareAssessmentTransDTMetadata.ColumnNames.IsSubjective);
            }

            set
            {
                base.SetSystemBoolean(NutritionCareAssessmentTransDTMetadata.ColumnNames.IsSubjective, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareAssessmentTransDT.IsObjective
        /// </summary>
        virtual public System.Boolean? IsObjective
        {
            get
            {
                return base.GetSystemBoolean(NutritionCareAssessmentTransDTMetadata.ColumnNames.IsObjective);
            }

            set
            {
                base.SetSystemBoolean(NutritionCareAssessmentTransDTMetadata.ColumnNames.IsObjective, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareAssessmentTransDT.AnswerPrefix
        /// </summary>
        virtual public System.String AnswerPrefix
        {
            get
            {
                return base.GetSystemString(NutritionCareAssessmentTransDTMetadata.ColumnNames.AnswerPrefix);
            }

            set
            {
                base.SetSystemString(NutritionCareAssessmentTransDTMetadata.ColumnNames.AnswerPrefix, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareAssessmentTransDT.AnswerSuffix
        /// </summary>
        virtual public System.String AnswerSuffix
        {
            get
            {
                return base.GetSystemString(NutritionCareAssessmentTransDTMetadata.ColumnNames.AnswerSuffix);
            }

            set
            {
                base.SetSystemString(NutritionCareAssessmentTransDTMetadata.ColumnNames.AnswerSuffix, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareAssessmentTransDT.AnswerText
        /// </summary>
        virtual public System.String AnswerText
        {
            get
            {
                return base.GetSystemString(NutritionCareAssessmentTransDTMetadata.ColumnNames.AnswerText);
            }

            set
            {
                base.SetSystemString(NutritionCareAssessmentTransDTMetadata.ColumnNames.AnswerText, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareAssessmentTransDT.AnswerNum
        /// </summary>
        virtual public System.Decimal? AnswerNum
        {
            get
            {
                return base.GetSystemDecimal(NutritionCareAssessmentTransDTMetadata.ColumnNames.AnswerNum);
            }

            set
            {
                base.SetSystemDecimal(NutritionCareAssessmentTransDTMetadata.ColumnNames.AnswerNum, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareAssessmentTransDT.AnswerSelectionLineID
        /// </summary>
        virtual public System.String AnswerSelectionLineID
        {
            get
            {
                return base.GetSystemString(NutritionCareAssessmentTransDTMetadata.ColumnNames.AnswerSelectionLineID);
            }

            set
            {
                base.SetSystemString(NutritionCareAssessmentTransDTMetadata.ColumnNames.AnswerSelectionLineID, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareAssessmentTransDT.CreateByUserID
        /// </summary>
        virtual public System.String CreateByUserID
        {
            get
            {
                return base.GetSystemString(NutritionCareAssessmentTransDTMetadata.ColumnNames.CreateByUserID);
            }

            set
            {
                base.SetSystemString(NutritionCareAssessmentTransDTMetadata.ColumnNames.CreateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareAssessmentTransDT.CreateDateTime
        /// </summary>
        virtual public System.DateTime? CreateDateTime
        {
            get
            {
                return base.GetSystemDateTime(NutritionCareAssessmentTransDTMetadata.ColumnNames.CreateDateTime);
            }

            set
            {
                base.SetSystemDateTime(NutritionCareAssessmentTransDTMetadata.ColumnNames.CreateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareAssessmentTransDT.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(NutritionCareAssessmentTransDTMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(NutritionCareAssessmentTransDTMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareAssessmentTransDT.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(NutritionCareAssessmentTransDTMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(NutritionCareAssessmentTransDTMetadata.ColumnNames.LastUpdateDateTime, value);
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
            public esStrings(esNutritionCareAssessmentTransDT entity)
            {
                this.entity = entity;
            }
            public System.String ID
            {
                get
                {
                    System.Int64? data = entity.ID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ID = null;
                    else entity.ID = Convert.ToInt64(value);
                }
            }
            public System.String HDID
            {
                get
                {
                    System.Int64? data = entity.HDID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.HDID = null;
                    else entity.HDID = Convert.ToInt64(value);
                }
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
            public System.String AnswerText
            {
                get
                {
                    System.String data = entity.AnswerText;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.AnswerText = null;
                    else entity.AnswerText = Convert.ToString(value);
                }
            }
            public System.String AnswerNum
            {
                get
                {
                    System.Decimal? data = entity.AnswerNum;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.AnswerNum = null;
                    else entity.AnswerNum = Convert.ToDecimal(value);
                }
            }
            public System.String AnswerSelectionLineID
            {
                get
                {
                    System.String data = entity.AnswerSelectionLineID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.AnswerSelectionLineID = null;
                    else entity.AnswerSelectionLineID = Convert.ToString(value);
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
            private esNutritionCareAssessmentTransDT entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esNutritionCareAssessmentTransDTQuery query)
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
                throw new Exception("esNutritionCareAssessmentTransDT can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class NutritionCareAssessmentTransDT : esNutritionCareAssessmentTransDT
    {
    }

    [Serializable]
    abstract public class esNutritionCareAssessmentTransDTQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return NutritionCareAssessmentTransDTMetadata.Meta();
            }
        }

        public esQueryItem ID
        {
            get
            {
                return new esQueryItem(this, NutritionCareAssessmentTransDTMetadata.ColumnNames.ID, esSystemType.Int64);
            }
        }

        public esQueryItem HDID
        {
            get
            {
                return new esQueryItem(this, NutritionCareAssessmentTransDTMetadata.ColumnNames.HDID, esSystemType.Int64);
            }
        }

        public esQueryItem QuestionID
        {
            get
            {
                return new esQueryItem(this, NutritionCareAssessmentTransDTMetadata.ColumnNames.QuestionID, esSystemType.String);
            }
        }

        public esQueryItem QuestionText
        {
            get
            {
                return new esQueryItem(this, NutritionCareAssessmentTransDTMetadata.ColumnNames.QuestionText, esSystemType.String);
            }
        }

        public esQueryItem IsSubjective
        {
            get
            {
                return new esQueryItem(this, NutritionCareAssessmentTransDTMetadata.ColumnNames.IsSubjective, esSystemType.Boolean);
            }
        }

        public esQueryItem IsObjective
        {
            get
            {
                return new esQueryItem(this, NutritionCareAssessmentTransDTMetadata.ColumnNames.IsObjective, esSystemType.Boolean);
            }
        }

        public esQueryItem AnswerPrefix
        {
            get
            {
                return new esQueryItem(this, NutritionCareAssessmentTransDTMetadata.ColumnNames.AnswerPrefix, esSystemType.String);
            }
        }

        public esQueryItem AnswerSuffix
        {
            get
            {
                return new esQueryItem(this, NutritionCareAssessmentTransDTMetadata.ColumnNames.AnswerSuffix, esSystemType.String);
            }
        }

        public esQueryItem AnswerText
        {
            get
            {
                return new esQueryItem(this, NutritionCareAssessmentTransDTMetadata.ColumnNames.AnswerText, esSystemType.String);
            }
        }

        public esQueryItem AnswerNum
        {
            get
            {
                return new esQueryItem(this, NutritionCareAssessmentTransDTMetadata.ColumnNames.AnswerNum, esSystemType.Decimal);
            }
        }

        public esQueryItem AnswerSelectionLineID
        {
            get
            {
                return new esQueryItem(this, NutritionCareAssessmentTransDTMetadata.ColumnNames.AnswerSelectionLineID, esSystemType.String);
            }
        }

        public esQueryItem CreateByUserID
        {
            get
            {
                return new esQueryItem(this, NutritionCareAssessmentTransDTMetadata.ColumnNames.CreateByUserID, esSystemType.String);
            }
        }

        public esQueryItem CreateDateTime
        {
            get
            {
                return new esQueryItem(this, NutritionCareAssessmentTransDTMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, NutritionCareAssessmentTransDTMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, NutritionCareAssessmentTransDTMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("NutritionCareAssessmentTransDTCollection")]
    public partial class NutritionCareAssessmentTransDTCollection : esNutritionCareAssessmentTransDTCollection, IEnumerable<NutritionCareAssessmentTransDT>
    {
        public NutritionCareAssessmentTransDTCollection()
        {

        }

        public static implicit operator List<NutritionCareAssessmentTransDT>(NutritionCareAssessmentTransDTCollection coll)
        {
            List<NutritionCareAssessmentTransDT> list = new List<NutritionCareAssessmentTransDT>();

            foreach (NutritionCareAssessmentTransDT emp in coll)
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
                return NutritionCareAssessmentTransDTMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new NutritionCareAssessmentTransDTQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new NutritionCareAssessmentTransDT(row);
        }

        override protected esEntity CreateEntity()
        {
            return new NutritionCareAssessmentTransDT();
        }

        #endregion

        [BrowsableAttribute(false)]
        public NutritionCareAssessmentTransDTQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new NutritionCareAssessmentTransDTQuery();
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
        public bool Load(NutritionCareAssessmentTransDTQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public NutritionCareAssessmentTransDT AddNew()
        {
            NutritionCareAssessmentTransDT entity = base.AddNewEntity() as NutritionCareAssessmentTransDT;

            return entity;
        }
        public NutritionCareAssessmentTransDT FindByPrimaryKey(Int64 iD)
        {
            return base.FindByPrimaryKey(iD) as NutritionCareAssessmentTransDT;
        }

        #region IEnumerable< NutritionCareAssessmentTransDT> Members

        IEnumerator<NutritionCareAssessmentTransDT> IEnumerable<NutritionCareAssessmentTransDT>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as NutritionCareAssessmentTransDT;
            }
        }

        #endregion

        private NutritionCareAssessmentTransDTQuery query;
    }


    /// <summary>
    /// Encapsulates the 'NutritionCareAssessmentTransDT' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("NutritionCareAssessmentTransDT ({ID})")]
    [Serializable]
    public partial class NutritionCareAssessmentTransDT : esNutritionCareAssessmentTransDT
    {
        public NutritionCareAssessmentTransDT()
        {
        }

        public NutritionCareAssessmentTransDT(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return NutritionCareAssessmentTransDTMetadata.Meta();
            }
        }

        override protected esNutritionCareAssessmentTransDTQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new NutritionCareAssessmentTransDTQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public NutritionCareAssessmentTransDTQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new NutritionCareAssessmentTransDTQuery();
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
        public bool Load(NutritionCareAssessmentTransDTQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private NutritionCareAssessmentTransDTQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class NutritionCareAssessmentTransDTQuery : esNutritionCareAssessmentTransDTQuery
    {
        public NutritionCareAssessmentTransDTQuery()
        {

        }

        public NutritionCareAssessmentTransDTQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "NutritionCareAssessmentTransDTQuery";
        }
    }

    [Serializable]
    public partial class NutritionCareAssessmentTransDTMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected NutritionCareAssessmentTransDTMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(NutritionCareAssessmentTransDTMetadata.ColumnNames.ID, 0, typeof(System.Int64), esSystemType.Int64);
            c.PropertyName = NutritionCareAssessmentTransDTMetadata.PropertyNames.ID;
            c.IsInPrimaryKey = true;
            c.IsAutoIncrement = true;
            c.NumericPrecision = 19;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareAssessmentTransDTMetadata.ColumnNames.HDID, 1, typeof(System.Int64), esSystemType.Int64);
            c.PropertyName = NutritionCareAssessmentTransDTMetadata.PropertyNames.HDID;
            c.NumericPrecision = 19;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareAssessmentTransDTMetadata.ColumnNames.QuestionID, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = NutritionCareAssessmentTransDTMetadata.PropertyNames.QuestionID;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareAssessmentTransDTMetadata.ColumnNames.QuestionText, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = NutritionCareAssessmentTransDTMetadata.PropertyNames.QuestionText;
            c.CharacterMaxLength = 255;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareAssessmentTransDTMetadata.ColumnNames.IsSubjective, 4, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = NutritionCareAssessmentTransDTMetadata.PropertyNames.IsSubjective;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareAssessmentTransDTMetadata.ColumnNames.IsObjective, 5, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = NutritionCareAssessmentTransDTMetadata.PropertyNames.IsObjective;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareAssessmentTransDTMetadata.ColumnNames.AnswerPrefix, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = NutritionCareAssessmentTransDTMetadata.PropertyNames.AnswerPrefix;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareAssessmentTransDTMetadata.ColumnNames.AnswerSuffix, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = NutritionCareAssessmentTransDTMetadata.PropertyNames.AnswerSuffix;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareAssessmentTransDTMetadata.ColumnNames.AnswerText, 8, typeof(System.String), esSystemType.String);
            c.PropertyName = NutritionCareAssessmentTransDTMetadata.PropertyNames.AnswerText;
            c.CharacterMaxLength = 1000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareAssessmentTransDTMetadata.ColumnNames.AnswerNum, 9, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = NutritionCareAssessmentTransDTMetadata.PropertyNames.AnswerNum;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareAssessmentTransDTMetadata.ColumnNames.AnswerSelectionLineID, 10, typeof(System.String), esSystemType.String);
            c.PropertyName = NutritionCareAssessmentTransDTMetadata.PropertyNames.AnswerSelectionLineID;
            c.CharacterMaxLength = 100;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareAssessmentTransDTMetadata.ColumnNames.CreateByUserID, 11, typeof(System.String), esSystemType.String);
            c.PropertyName = NutritionCareAssessmentTransDTMetadata.PropertyNames.CreateByUserID;
            c.CharacterMaxLength = 15;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareAssessmentTransDTMetadata.ColumnNames.CreateDateTime, 12, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = NutritionCareAssessmentTransDTMetadata.PropertyNames.CreateDateTime;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareAssessmentTransDTMetadata.ColumnNames.LastUpdateByUserID, 13, typeof(System.String), esSystemType.String);
            c.PropertyName = NutritionCareAssessmentTransDTMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareAssessmentTransDTMetadata.ColumnNames.LastUpdateDateTime, 14, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = NutritionCareAssessmentTransDTMetadata.PropertyNames.LastUpdateDateTime;
            _columns.Add(c);


        }
        #endregion

        static public NutritionCareAssessmentTransDTMetadata Meta()
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
            public const string ID = "ID";
            public const string HDID = "HDID";
            public const string QuestionID = "QuestionID";
            public const string QuestionText = "QuestionText";
            public const string IsSubjective = "IsSubjective";
            public const string IsObjective = "IsObjective";
            public const string AnswerPrefix = "AnswerPrefix";
            public const string AnswerSuffix = "AnswerSuffix";
            public const string AnswerText = "AnswerText";
            public const string AnswerNum = "AnswerNum";
            public const string AnswerSelectionLineID = "AnswerSelectionLineID";
            public const string CreateByUserID = "CreateByUserID";
            public const string CreateDateTime = "CreateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string ID = "ID";
            public const string HDID = "HDID";
            public const string QuestionID = "QuestionID";
            public const string QuestionText = "QuestionText";
            public const string IsSubjective = "IsSubjective";
            public const string IsObjective = "IsObjective";
            public const string AnswerPrefix = "AnswerPrefix";
            public const string AnswerSuffix = "AnswerSuffix";
            public const string AnswerText = "AnswerText";
            public const string AnswerNum = "AnswerNum";
            public const string AnswerSelectionLineID = "AnswerSelectionLineID";
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
            lock (typeof(NutritionCareAssessmentTransDTMetadata))
            {
                if (NutritionCareAssessmentTransDTMetadata.mapDelegates == null)
                {
                    NutritionCareAssessmentTransDTMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (NutritionCareAssessmentTransDTMetadata.meta == null)
                {
                    NutritionCareAssessmentTransDTMetadata.meta = new NutritionCareAssessmentTransDTMetadata();
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

                meta.AddTypeMap("ID", new esTypeMap("bigint", "System.Int64"));
                meta.AddTypeMap("HDID", new esTypeMap("bigint", "System.Int64"));
                meta.AddTypeMap("QuestionID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("QuestionText", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsSubjective", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsObjective", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("AnswerPrefix", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("AnswerSuffix", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("AnswerText", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("AnswerNum", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("AnswerSelectionLineID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));


                meta.Source = "NutritionCareAssessmentTransDT";
                meta.Destination = "NutritionCareAssessmentTransDT";
                meta.spInsert = "proc_NutritionCareAssessmentTransDTInsert";
                meta.spUpdate = "proc_NutritionCareAssessmentTransDTUpdate";
                meta.spDelete = "proc_NutritionCareAssessmentTransDTDelete";
                meta.spLoadAll = "proc_NutritionCareAssessmentTransDTLoadAll";
                meta.spLoadByPrimaryKey = "proc_NutritionCareAssessmentTransDTLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private NutritionCareAssessmentTransDTMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
