/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 11/7/2020 2:51:32 PM
===============================================================================
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.ComponentModel;
using System.Xml.Serialization;


using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;



namespace Temiang.Avicenna.BusinessObject
{

    [Serializable]
    abstract public class esAppraisalScoresheetCollection : esEntityCollectionWAuditLog
    {
        public esAppraisalScoresheetCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "AppraisalScoresheetCollection";
        }

        #region Query Logic
        protected void InitQuery(esAppraisalScoresheetQuery query)
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
            this.InitQuery(query as esAppraisalScoresheetQuery);
        }
        #endregion

        virtual public AppraisalScoresheet DetachEntity(AppraisalScoresheet entity)
        {
            return base.DetachEntity(entity) as AppraisalScoresheet;
        }

        virtual public AppraisalScoresheet AttachEntity(AppraisalScoresheet entity)
        {
            return base.AttachEntity(entity) as AppraisalScoresheet;
        }

        virtual public void Combine(AppraisalScoresheetCollection collection)
        {
            base.Combine(collection);
        }

        new public AppraisalScoresheet this[int index]
        {
            get
            {
                return base[index] as AppraisalScoresheet;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(AppraisalScoresheet);
        }
    }



    [Serializable]
    abstract public class esAppraisalScoresheet : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esAppraisalScoresheetQuery GetDynamicQuery()
        {
            return null;
        }

        public esAppraisalScoresheet()
        {

        }

        public esAppraisalScoresheet(DataRow row)
            : base(row)
        {

        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(System.Int32 scoresheetID, System.Int32 participantItemID, System.Int32 evaluatorID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(scoresheetID, participantItemID, evaluatorID);
            else
                return LoadByPrimaryKeyStoredProcedure(scoresheetID, participantItemID, evaluatorID);
        }

        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 scoresheetID, System.Int32 participantItemID, System.Int32 evaluatorID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(scoresheetID, participantItemID, evaluatorID);
            else
                return LoadByPrimaryKeyStoredProcedure(scoresheetID, participantItemID, evaluatorID);
        }

        private bool LoadByPrimaryKeyDynamic(System.Int32 scoresheetID, System.Int32 participantItemID, System.Int32 evaluatorID)
        {
            esAppraisalScoresheetQuery query = this.GetDynamicQuery();
            query.Where(query.ScoresheetID == scoresheetID, query.ParticipantItemID == participantItemID, query.EvaluatorID == evaluatorID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(System.Int32 evaluatorID, System.Int32 participantItemID, System.Int32 scoresheetID)
        {
            esParameters parms = new esParameters();
            parms.Add("EvaluatorID", evaluatorID); parms.Add("ParticipantItemID", participantItemID); parms.Add("ScoresheetID", scoresheetID);
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
                        case "ScoresheetID": this.str.ScoresheetID = (string)value; break;
                        case "ParticipantItemID": this.str.ParticipantItemID = (string)value; break;
                        case "EvaluatorID": this.str.EvaluatorID = (string)value; break;
                        case "ReferenceID": this.str.ReferenceID = (string)value; break;
                        case "PeriodYear": this.str.PeriodYear = (string)value; break;
                        case "ScoringDate": this.str.ScoringDate = (string)value; break;
                        case "SREvaluatorType": this.str.SREvaluatorType = (string)value; break;
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
                        case "ScoresheetID":

                            if (value == null || value is System.Int32)
                                this.ScoresheetID = (System.Int32?)value;
                            break;

                        case "ParticipantItemID":

                            if (value == null || value is System.Int32)
                                this.ParticipantItemID = (System.Int32?)value;
                            break;

                        case "EvaluatorID":

                            if (value == null || value is System.Int32)
                                this.EvaluatorID = (System.Int32?)value;
                            break;

                        case "ReferenceID":

                            if (value == null || value is System.Int32)
                                this.ReferenceID = (System.Int32?)value;
                            break;

                        case "ScoringDate":

                            if (value == null || value is System.DateTime)
                                this.ScoringDate = (System.DateTime?)value;
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
        /// Maps to AppraisalScoresheet.ScoresheetID
        /// </summary>
        virtual public System.Int32? ScoresheetID
        {
            get
            {
                return base.GetSystemInt32(AppraisalScoresheetMetadata.ColumnNames.ScoresheetID);
            }

            set
            {
                base.SetSystemInt32(AppraisalScoresheetMetadata.ColumnNames.ScoresheetID, value);
            }
        }

        /// <summary>
        /// Maps to AppraisalScoresheet.ParticipantItemID
        /// </summary>
        virtual public System.Int32? ParticipantItemID
        {
            get
            {
                return base.GetSystemInt32(AppraisalScoresheetMetadata.ColumnNames.ParticipantItemID);
            }

            set
            {
                base.SetSystemInt32(AppraisalScoresheetMetadata.ColumnNames.ParticipantItemID, value);
            }
        }

        /// <summary>
        /// Maps to AppraisalScoresheet.EvaluatorID
        /// </summary>
        virtual public System.Int32? EvaluatorID
        {
            get
            {
                return base.GetSystemInt32(AppraisalScoresheetMetadata.ColumnNames.EvaluatorID);
            }

            set
            {
                base.SetSystemInt32(AppraisalScoresheetMetadata.ColumnNames.EvaluatorID, value);
            }
        }

        /// <summary>
        /// Maps to AppraisalScoresheet.ReferenceID
        /// </summary>
        virtual public System.Int32? ReferenceID
        {
            get
            {
                return base.GetSystemInt32(AppraisalScoresheetMetadata.ColumnNames.ReferenceID);
            }

            set
            {
                base.SetSystemInt32(AppraisalScoresheetMetadata.ColumnNames.ReferenceID, value);
            }
        }

        /// <summary>
        /// Maps to AppraisalScoresheet.PeriodYear
        /// </summary>
        virtual public System.String PeriodYear
        {
            get
            {
                return base.GetSystemString(AppraisalScoresheetMetadata.ColumnNames.PeriodYear);
            }

            set
            {
                base.SetSystemString(AppraisalScoresheetMetadata.ColumnNames.PeriodYear, value);
            }
        }

        /// <summary>
        /// Maps to AppraisalScoresheet.ScoringDate
        /// </summary>
        virtual public System.DateTime? ScoringDate
        {
            get
            {
                return base.GetSystemDateTime(AppraisalScoresheetMetadata.ColumnNames.ScoringDate);
            }

            set
            {
                base.SetSystemDateTime(AppraisalScoresheetMetadata.ColumnNames.ScoringDate, value);
            }
        }

        /// <summary>
        /// Maps to AppraisalScoresheet.SREvaluatorType
        /// </summary>
        virtual public System.String SREvaluatorType
        {
            get
            {
                return base.GetSystemString(AppraisalScoresheetMetadata.ColumnNames.SREvaluatorType);
            }

            set
            {
                base.SetSystemString(AppraisalScoresheetMetadata.ColumnNames.SREvaluatorType, value);
            }
        }

        /// <summary>
        /// Maps to AppraisalScoresheet.IsApproved
        /// </summary>
        virtual public System.Boolean? IsApproved
        {
            get
            {
                return base.GetSystemBoolean(AppraisalScoresheetMetadata.ColumnNames.IsApproved);
            }

            set
            {
                base.SetSystemBoolean(AppraisalScoresheetMetadata.ColumnNames.IsApproved, value);
            }
        }

        /// <summary>
        /// Maps to AppraisalScoresheet.ApprovedDateTime
        /// </summary>
        virtual public System.DateTime? ApprovedDateTime
        {
            get
            {
                return base.GetSystemDateTime(AppraisalScoresheetMetadata.ColumnNames.ApprovedDateTime);
            }

            set
            {
                base.SetSystemDateTime(AppraisalScoresheetMetadata.ColumnNames.ApprovedDateTime, value);
            }
        }

        /// <summary>
        /// Maps to AppraisalScoresheet.ApprovedByUserID
        /// </summary>
        virtual public System.String ApprovedByUserID
        {
            get
            {
                return base.GetSystemString(AppraisalScoresheetMetadata.ColumnNames.ApprovedByUserID);
            }

            set
            {
                base.SetSystemString(AppraisalScoresheetMetadata.ColumnNames.ApprovedByUserID, value);
            }
        }

        /// <summary>
        /// Maps to AppraisalScoresheet.IsVoid
        /// </summary>
        virtual public System.Boolean? IsVoid
        {
            get
            {
                return base.GetSystemBoolean(AppraisalScoresheetMetadata.ColumnNames.IsVoid);
            }

            set
            {
                base.SetSystemBoolean(AppraisalScoresheetMetadata.ColumnNames.IsVoid, value);
            }
        }

        /// <summary>
        /// Maps to AppraisalScoresheet.VoidDateTime
        /// </summary>
        virtual public System.DateTime? VoidDateTime
        {
            get
            {
                return base.GetSystemDateTime(AppraisalScoresheetMetadata.ColumnNames.VoidDateTime);
            }

            set
            {
                base.SetSystemDateTime(AppraisalScoresheetMetadata.ColumnNames.VoidDateTime, value);
            }
        }

        /// <summary>
        /// Maps to AppraisalScoresheet.VoidByUserID
        /// </summary>
        virtual public System.String VoidByUserID
        {
            get
            {
                return base.GetSystemString(AppraisalScoresheetMetadata.ColumnNames.VoidByUserID);
            }

            set
            {
                base.SetSystemString(AppraisalScoresheetMetadata.ColumnNames.VoidByUserID, value);
            }
        }

        /// <summary>
        /// Maps to AppraisalScoresheet.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(AppraisalScoresheetMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(AppraisalScoresheetMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to AppraisalScoresheet.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(AppraisalScoresheetMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(AppraisalScoresheetMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }

        #endregion

        #region String Properties


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
            public esStrings(esAppraisalScoresheet entity)
            {
                this.entity = entity;
            }


            public System.String ScoresheetID
            {
                get
                {
                    System.Int32? data = entity.ScoresheetID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ScoresheetID = null;
                    else entity.ScoresheetID = Convert.ToInt32(value);
                }
            }

            public System.String ParticipantItemID
            {
                get
                {
                    System.Int32? data = entity.ParticipantItemID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ParticipantItemID = null;
                    else entity.ParticipantItemID = Convert.ToInt32(value);
                }
            }

            public System.String EvaluatorID
            {
                get
                {
                    System.Int32? data = entity.EvaluatorID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.EvaluatorID = null;
                    else entity.EvaluatorID = Convert.ToInt32(value);
                }
            }

            public System.String ReferenceID
            {
                get
                {
                    System.Int32? data = entity.ReferenceID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ReferenceID = null;
                    else entity.ReferenceID = Convert.ToInt32(value);
                }
            }

            public System.String PeriodYear
            {
                get
                {
                    System.String data = entity.PeriodYear;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PeriodYear = null;
                    else entity.PeriodYear = Convert.ToString(value);
                }
            }

            public System.String ScoringDate
            {
                get
                {
                    System.DateTime? data = entity.ScoringDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ScoringDate = null;
                    else entity.ScoringDate = Convert.ToDateTime(value);
                }
            }

            public System.String SREvaluatorType
            {
                get
                {
                    System.String data = entity.SREvaluatorType;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SREvaluatorType = null;
                    else entity.SREvaluatorType = Convert.ToString(value);
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


            private esAppraisalScoresheet entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esAppraisalScoresheetQuery query)
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
                throw new Exception("esAppraisalScoresheet can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    [Serializable]
    abstract public class esAppraisalScoresheetQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return AppraisalScoresheetMetadata.Meta();
            }
        }


        public esQueryItem ScoresheetID
        {
            get
            {
                return new esQueryItem(this, AppraisalScoresheetMetadata.ColumnNames.ScoresheetID, esSystemType.Int32);
            }
        }

        public esQueryItem ParticipantItemID
        {
            get
            {
                return new esQueryItem(this, AppraisalScoresheetMetadata.ColumnNames.ParticipantItemID, esSystemType.Int32);
            }
        }

        public esQueryItem EvaluatorID
        {
            get
            {
                return new esQueryItem(this, AppraisalScoresheetMetadata.ColumnNames.EvaluatorID, esSystemType.Int32);
            }
        }

        public esQueryItem ReferenceID
        {
            get
            {
                return new esQueryItem(this, AppraisalScoresheetMetadata.ColumnNames.ReferenceID, esSystemType.Int32);
            }
        }

        public esQueryItem PeriodYear
        {
            get
            {
                return new esQueryItem(this, AppraisalScoresheetMetadata.ColumnNames.PeriodYear, esSystemType.String);
            }
        }

        public esQueryItem ScoringDate
        {
            get
            {
                return new esQueryItem(this, AppraisalScoresheetMetadata.ColumnNames.ScoringDate, esSystemType.DateTime);
            }
        }

        public esQueryItem SREvaluatorType
        {
            get
            {
                return new esQueryItem(this, AppraisalScoresheetMetadata.ColumnNames.SREvaluatorType, esSystemType.String);
            }
        }

        public esQueryItem IsApproved
        {
            get
            {
                return new esQueryItem(this, AppraisalScoresheetMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
            }
        }

        public esQueryItem ApprovedDateTime
        {
            get
            {
                return new esQueryItem(this, AppraisalScoresheetMetadata.ColumnNames.ApprovedDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem ApprovedByUserID
        {
            get
            {
                return new esQueryItem(this, AppraisalScoresheetMetadata.ColumnNames.ApprovedByUserID, esSystemType.String);
            }
        }

        public esQueryItem IsVoid
        {
            get
            {
                return new esQueryItem(this, AppraisalScoresheetMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
            }
        }

        public esQueryItem VoidDateTime
        {
            get
            {
                return new esQueryItem(this, AppraisalScoresheetMetadata.ColumnNames.VoidDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem VoidByUserID
        {
            get
            {
                return new esQueryItem(this, AppraisalScoresheetMetadata.ColumnNames.VoidByUserID, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, AppraisalScoresheetMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, AppraisalScoresheetMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("AppraisalScoresheetCollection")]
    public partial class AppraisalScoresheetCollection : esAppraisalScoresheetCollection, IEnumerable<AppraisalScoresheet>
    {
        public AppraisalScoresheetCollection()
        {

        }

        public static implicit operator List<AppraisalScoresheet>(AppraisalScoresheetCollection coll)
        {
            List<AppraisalScoresheet> list = new List<AppraisalScoresheet>();

            foreach (AppraisalScoresheet emp in coll)
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
                return AppraisalScoresheetMetadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new AppraisalScoresheetQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new AppraisalScoresheet(row);
        }

        override protected esEntity CreateEntity()
        {
            return new AppraisalScoresheet();
        }


        #endregion


        [BrowsableAttribute(false)]
        public AppraisalScoresheetQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new AppraisalScoresheetQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(AppraisalScoresheetQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public AppraisalScoresheet AddNew()
        {
            AppraisalScoresheet entity = base.AddNewEntity() as AppraisalScoresheet;

            return entity;
        }

        public AppraisalScoresheet FindByPrimaryKey(System.Int32 evaluatorID, System.Int32 participantItemID, System.Int32 scoresheetID)
        {
            return base.FindByPrimaryKey(evaluatorID, participantItemID, scoresheetID) as AppraisalScoresheet;
        }


        #region IEnumerable<AppraisalScoresheet> Members

        IEnumerator<AppraisalScoresheet> IEnumerable<AppraisalScoresheet>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as AppraisalScoresheet;
            }
        }

        #endregion

        private AppraisalScoresheetQuery query;
    }


    /// <summary>
    /// Encapsulates the 'AppraisalScoresheet' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("AppraisalScoresheet ({ScoresheetID},{ParticipantItemID},{EvaluatorID})")]
    [Serializable]
    public partial class AppraisalScoresheet : esAppraisalScoresheet
    {
        public AppraisalScoresheet()
        {

        }

        public AppraisalScoresheet(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return AppraisalScoresheetMetadata.Meta();
            }
        }



        override protected esAppraisalScoresheetQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new AppraisalScoresheetQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public AppraisalScoresheetQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new AppraisalScoresheetQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(AppraisalScoresheetQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private AppraisalScoresheetQuery query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class AppraisalScoresheetQuery : esAppraisalScoresheetQuery
    {
        public AppraisalScoresheetQuery()
        {

        }

        public AppraisalScoresheetQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "AppraisalScoresheetQuery";
        }


    }


    [Serializable]
    public partial class AppraisalScoresheetMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected AppraisalScoresheetMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(AppraisalScoresheetMetadata.ColumnNames.ScoresheetID, 0, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = AppraisalScoresheetMetadata.PropertyNames.ScoresheetID;
            c.IsInPrimaryKey = true;
            c.IsAutoIncrement = true;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(AppraisalScoresheetMetadata.ColumnNames.ParticipantItemID, 1, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = AppraisalScoresheetMetadata.PropertyNames.ParticipantItemID;
            c.IsInPrimaryKey = true;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(AppraisalScoresheetMetadata.ColumnNames.EvaluatorID, 2, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = AppraisalScoresheetMetadata.PropertyNames.EvaluatorID;
            c.IsInPrimaryKey = true;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(AppraisalScoresheetMetadata.ColumnNames.ReferenceID, 3, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = AppraisalScoresheetMetadata.PropertyNames.ReferenceID;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AppraisalScoresheetMetadata.ColumnNames.PeriodYear, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = AppraisalScoresheetMetadata.PropertyNames.PeriodYear;
            c.CharacterMaxLength = 4;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AppraisalScoresheetMetadata.ColumnNames.ScoringDate, 5, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = AppraisalScoresheetMetadata.PropertyNames.ScoringDate;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AppraisalScoresheetMetadata.ColumnNames.SREvaluatorType, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = AppraisalScoresheetMetadata.PropertyNames.SREvaluatorType;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AppraisalScoresheetMetadata.ColumnNames.IsApproved, 7, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = AppraisalScoresheetMetadata.PropertyNames.IsApproved;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AppraisalScoresheetMetadata.ColumnNames.ApprovedDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = AppraisalScoresheetMetadata.PropertyNames.ApprovedDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AppraisalScoresheetMetadata.ColumnNames.ApprovedByUserID, 9, typeof(System.String), esSystemType.String);
            c.PropertyName = AppraisalScoresheetMetadata.PropertyNames.ApprovedByUserID;
            c.CharacterMaxLength = 40;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AppraisalScoresheetMetadata.ColumnNames.IsVoid, 10, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = AppraisalScoresheetMetadata.PropertyNames.IsVoid;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AppraisalScoresheetMetadata.ColumnNames.VoidDateTime, 11, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = AppraisalScoresheetMetadata.PropertyNames.VoidDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AppraisalScoresheetMetadata.ColumnNames.VoidByUserID, 12, typeof(System.String), esSystemType.String);
            c.PropertyName = AppraisalScoresheetMetadata.PropertyNames.VoidByUserID;
            c.CharacterMaxLength = 40;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AppraisalScoresheetMetadata.ColumnNames.LastUpdateDateTime, 13, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = AppraisalScoresheetMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AppraisalScoresheetMetadata.ColumnNames.LastUpdateByUserID, 14, typeof(System.String), esSystemType.String);
            c.PropertyName = AppraisalScoresheetMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 40;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public AppraisalScoresheetMetadata Meta()
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
            public const string ScoresheetID = "ScoresheetID";
            public const string ParticipantItemID = "ParticipantItemID";
            public const string EvaluatorID = "EvaluatorID";
            public const string ReferenceID = "ReferenceID";
            public const string PeriodYear = "PeriodYear";
            public const string ScoringDate = "ScoringDate";
            public const string SREvaluatorType = "SREvaluatorType";
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
            public const string ScoresheetID = "ScoresheetID";
            public const string ParticipantItemID = "ParticipantItemID";
            public const string EvaluatorID = "EvaluatorID";
            public const string ReferenceID = "ReferenceID";
            public const string PeriodYear = "PeriodYear";
            public const string ScoringDate = "ScoringDate";
            public const string SREvaluatorType = "SREvaluatorType";
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
            lock (typeof(AppraisalScoresheetMetadata))
            {
                if (AppraisalScoresheetMetadata.mapDelegates == null)
                {
                    AppraisalScoresheetMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (AppraisalScoresheetMetadata.meta == null)
                {
                    AppraisalScoresheetMetadata.meta = new AppraisalScoresheetMetadata();
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


                meta.AddTypeMap("ScoresheetID", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("ParticipantItemID", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("EvaluatorID", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("ReferenceID", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("PeriodYear", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ScoringDate", new esTypeMap("smalldatetime", "System.DateTime"));
                meta.AddTypeMap("SREvaluatorType", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("ApprovedDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("ApprovedByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("VoidDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("VoidByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));



                meta.Source = "AppraisalScoresheet";
                meta.Destination = "AppraisalScoresheet";

                meta.spInsert = "proc_AppraisalScoresheetInsert";
                meta.spUpdate = "proc_AppraisalScoresheetUpdate";
                meta.spDelete = "proc_AppraisalScoresheetDelete";
                meta.spLoadAll = "proc_AppraisalScoresheetLoadAll";
                meta.spLoadByPrimaryKey = "proc_AppraisalScoresheetLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private AppraisalScoresheetMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
