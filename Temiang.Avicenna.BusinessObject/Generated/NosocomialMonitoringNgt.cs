/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/03/19 6:44:27 PM
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
    abstract public class esNosocomialMonitoringNgtCollection : esEntityCollectionWAuditLog
    {
        public esNosocomialMonitoringNgtCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "NosocomialMonitoringNgtCollection";
        }

        #region Query Logic
        protected void InitQuery(esNosocomialMonitoringNgtQuery query)
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
            this.InitQuery(query as esNosocomialMonitoringNgtQuery);
        }
        #endregion

        virtual public NosocomialMonitoringNgt DetachEntity(NosocomialMonitoringNgt entity)
        {
            return base.DetachEntity(entity) as NosocomialMonitoringNgt;
        }

        virtual public NosocomialMonitoringNgt AttachEntity(NosocomialMonitoringNgt entity)
        {
            return base.AttachEntity(entity) as NosocomialMonitoringNgt;
        }

        virtual public void Combine(NosocomialMonitoringNgtCollection collection)
        {
            base.Combine(collection);
        }

        new public NosocomialMonitoringNgt this[int index]
        {
            get
            {
                return base[index] as NosocomialMonitoringNgt;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(NosocomialMonitoringNgt);
        }
    }

    [Serializable]
    abstract public class esNosocomialMonitoringNgt : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esNosocomialMonitoringNgtQuery GetDynamicQuery()
        {
            return null;
        }

        public esNosocomialMonitoringNgt()
        {
        }

        public esNosocomialMonitoringNgt(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String registrationNo, Int32 monitoringNo, Int32 sequenceNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(registrationNo, monitoringNo, sequenceNo);
            else
                return LoadByPrimaryKeyStoredProcedure(registrationNo, monitoringNo, sequenceNo);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String registrationNo, Int32 monitoringNo, Int32 sequenceNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(registrationNo, monitoringNo, sequenceNo);
            else
                return LoadByPrimaryKeyStoredProcedure(registrationNo, monitoringNo, sequenceNo);
        }

        private bool LoadByPrimaryKeyDynamic(String registrationNo, Int32 monitoringNo, Int32 sequenceNo)
        {
            esNosocomialMonitoringNgtQuery query = this.GetDynamicQuery();
            query.Where(query.RegistrationNo == registrationNo, query.MonitoringNo == monitoringNo, query.SequenceNo == sequenceNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String registrationNo, Int32 monitoringNo, Int32 sequenceNo)
        {
            esParameters parms = new esParameters();
            parms.Add("RegistrationNo", registrationNo);
            parms.Add("MonitoringNo", monitoringNo);
            parms.Add("SequenceNo", sequenceNo);
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
                        case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
                        case "MonitoringNo": this.str.MonitoringNo = (string)value; break;
                        case "SequenceNo": this.str.SequenceNo = (string)value; break;
                        case "MonitoringDateTime": this.str.MonitoringDateTime = (string)value; break;
                        case "Replacement": this.str.Replacement = (string)value; break;
                        case "IsTempAbove38": this.str.IsTempAbove38 = (string)value; break;
                        case "IsPus": this.str.IsPus = (string)value; break;
                        case "IsPain": this.str.IsPain = (string)value; break;
                        case "IsHeadache": this.str.IsHeadache = (string)value; break;
                        case "IsNoseClogged": this.str.IsNoseClogged = (string)value; break;
                        case "IsPainSwallow": this.str.IsPainSwallow = (string)value; break;
                        case "IsPharingRedness": this.str.IsPharingRedness = (string)value; break;
                        case "IsCough": this.str.IsCough = (string)value; break;
                        case "IsTransillumination": this.str.IsTransillumination = (string)value; break;
                        case "IsCulture": this.str.IsCulture = (string)value; break;
                        case "Note": this.str.Note = (string)value; break;
                        case "IsDeleted": this.str.IsDeleted = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "MonitoringByUserID": this.str.MonitoringByUserID = (string)value; break;
                        case "IsRelease": this.str.IsRelease = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "MonitoringNo":

                            if (value == null || value is System.Int32)
                                this.MonitoringNo = (System.Int32?)value;
                            break;
                        case "SequenceNo":

                            if (value == null || value is System.Int32)
                                this.SequenceNo = (System.Int32?)value;
                            break;
                        case "MonitoringDateTime":

                            if (value == null || value is System.DateTime)
                                this.MonitoringDateTime = (System.DateTime?)value;
                            break;
                        case "IsTempAbove38":

                            if (value == null || value is System.Boolean)
                                this.IsTempAbove38 = (System.Boolean?)value;
                            break;
                        case "IsPus":

                            if (value == null || value is System.Boolean)
                                this.IsPus = (System.Boolean?)value;
                            break;
                        case "IsPain":

                            if (value == null || value is System.Boolean)
                                this.IsPain = (System.Boolean?)value;
                            break;
                        case "IsHeadache":

                            if (value == null || value is System.Boolean)
                                this.IsHeadache = (System.Boolean?)value;
                            break;
                        case "IsNoseClogged":

                            if (value == null || value is System.Boolean)
                                this.IsNoseClogged = (System.Boolean?)value;
                            break;
                        case "IsPainSwallow":

                            if (value == null || value is System.Boolean)
                                this.IsPainSwallow = (System.Boolean?)value;
                            break;
                        case "IsPharingRedness":

                            if (value == null || value is System.Boolean)
                                this.IsPharingRedness = (System.Boolean?)value;
                            break;
                        case "IsCough":

                            if (value == null || value is System.Boolean)
                                this.IsCough = (System.Boolean?)value;
                            break;
                        case "IsTransillumination":

                            if (value == null || value is System.Boolean)
                                this.IsTransillumination = (System.Boolean?)value;
                            break;
                        case "IsCulture":

                            if (value == null || value is System.Boolean)
                                this.IsCulture = (System.Boolean?)value;
                            break;
                        case "IsDeleted":

                            if (value == null || value is System.Boolean)
                                this.IsDeleted = (System.Boolean?)value;
                            break;
                        case "LastUpdateDateTime":

                            if (value == null || value is System.DateTime)
                                this.LastUpdateDateTime = (System.DateTime?)value;
                            break;
                        case "IsRelease":

                            if (value == null || value is System.Boolean)
                                this.IsRelease = (System.Boolean?)value;
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
        /// Maps to NosocomialMonitoringNgt.RegistrationNo
        /// </summary>
        virtual public System.String RegistrationNo
        {
            get
            {
                return base.GetSystemString(NosocomialMonitoringNgtMetadata.ColumnNames.RegistrationNo);
            }

            set
            {
                base.SetSystemString(NosocomialMonitoringNgtMetadata.ColumnNames.RegistrationNo, value);
            }
        }
        /// <summary>
        /// Maps to NosocomialMonitoringNgt.MonitoringNo
        /// </summary>
        virtual public System.Int32? MonitoringNo
        {
            get
            {
                return base.GetSystemInt32(NosocomialMonitoringNgtMetadata.ColumnNames.MonitoringNo);
            }

            set
            {
                base.SetSystemInt32(NosocomialMonitoringNgtMetadata.ColumnNames.MonitoringNo, value);
            }
        }
        /// <summary>
        /// Maps to NosocomialMonitoringNgt.SequenceNo
        /// </summary>
        virtual public System.Int32? SequenceNo
        {
            get
            {
                return base.GetSystemInt32(NosocomialMonitoringNgtMetadata.ColumnNames.SequenceNo);
            }

            set
            {
                base.SetSystemInt32(NosocomialMonitoringNgtMetadata.ColumnNames.SequenceNo, value);
            }
        }
        /// <summary>
        /// Maps to NosocomialMonitoringNgt.MonitoringDateTime
        /// </summary>
        virtual public System.DateTime? MonitoringDateTime
        {
            get
            {
                return base.GetSystemDateTime(NosocomialMonitoringNgtMetadata.ColumnNames.MonitoringDateTime);
            }

            set
            {
                base.SetSystemDateTime(NosocomialMonitoringNgtMetadata.ColumnNames.MonitoringDateTime, value);
            }
        }
        /// <summary>
        /// Maps to NosocomialMonitoringNgt.Replacement
        /// </summary>
        virtual public System.String Replacement
        {
            get
            {
                return base.GetSystemString(NosocomialMonitoringNgtMetadata.ColumnNames.Replacement);
            }

            set
            {
                base.SetSystemString(NosocomialMonitoringNgtMetadata.ColumnNames.Replacement, value);
            }
        }
        /// <summary>
        /// Maps to NosocomialMonitoringNgt.IsTempAbove38
        /// </summary>
        virtual public System.Boolean? IsTempAbove38
        {
            get
            {
                return base.GetSystemBoolean(NosocomialMonitoringNgtMetadata.ColumnNames.IsTempAbove38);
            }

            set
            {
                base.SetSystemBoolean(NosocomialMonitoringNgtMetadata.ColumnNames.IsTempAbove38, value);
            }
        }
        /// <summary>
        /// Maps to NosocomialMonitoringNgt.IsPus
        /// </summary>
        virtual public System.Boolean? IsPus
        {
            get
            {
                return base.GetSystemBoolean(NosocomialMonitoringNgtMetadata.ColumnNames.IsPus);
            }

            set
            {
                base.SetSystemBoolean(NosocomialMonitoringNgtMetadata.ColumnNames.IsPus, value);
            }
        }
        /// <summary>
        /// Maps to NosocomialMonitoringNgt.IsPain
        /// </summary>
        virtual public System.Boolean? IsPain
        {
            get
            {
                return base.GetSystemBoolean(NosocomialMonitoringNgtMetadata.ColumnNames.IsPain);
            }

            set
            {
                base.SetSystemBoolean(NosocomialMonitoringNgtMetadata.ColumnNames.IsPain, value);
            }
        }
        /// <summary>
        /// Maps to NosocomialMonitoringNgt.IsHeadache
        /// </summary>
        virtual public System.Boolean? IsHeadache
        {
            get
            {
                return base.GetSystemBoolean(NosocomialMonitoringNgtMetadata.ColumnNames.IsHeadache);
            }

            set
            {
                base.SetSystemBoolean(NosocomialMonitoringNgtMetadata.ColumnNames.IsHeadache, value);
            }
        }
        /// <summary>
        /// Maps to NosocomialMonitoringNgt.IsNoseClogged
        /// </summary>
        virtual public System.Boolean? IsNoseClogged
        {
            get
            {
                return base.GetSystemBoolean(NosocomialMonitoringNgtMetadata.ColumnNames.IsNoseClogged);
            }

            set
            {
                base.SetSystemBoolean(NosocomialMonitoringNgtMetadata.ColumnNames.IsNoseClogged, value);
            }
        }
        /// <summary>
        /// Maps to NosocomialMonitoringNgt.IsPainSwallow
        /// </summary>
        virtual public System.Boolean? IsPainSwallow
        {
            get
            {
                return base.GetSystemBoolean(NosocomialMonitoringNgtMetadata.ColumnNames.IsPainSwallow);
            }

            set
            {
                base.SetSystemBoolean(NosocomialMonitoringNgtMetadata.ColumnNames.IsPainSwallow, value);
            }
        }
        /// <summary>
        /// Maps to NosocomialMonitoringNgt.IsPharingRedness
        /// </summary>
        virtual public System.Boolean? IsPharingRedness
        {
            get
            {
                return base.GetSystemBoolean(NosocomialMonitoringNgtMetadata.ColumnNames.IsPharingRedness);
            }

            set
            {
                base.SetSystemBoolean(NosocomialMonitoringNgtMetadata.ColumnNames.IsPharingRedness, value);
            }
        }
        /// <summary>
        /// Maps to NosocomialMonitoringNgt.IsCough
        /// </summary>
        virtual public System.Boolean? IsCough
        {
            get
            {
                return base.GetSystemBoolean(NosocomialMonitoringNgtMetadata.ColumnNames.IsCough);
            }

            set
            {
                base.SetSystemBoolean(NosocomialMonitoringNgtMetadata.ColumnNames.IsCough, value);
            }
        }
        /// <summary>
        /// Maps to NosocomialMonitoringNgt.IsTransillumination
        /// </summary>
        virtual public System.Boolean? IsTransillumination
        {
            get
            {
                return base.GetSystemBoolean(NosocomialMonitoringNgtMetadata.ColumnNames.IsTransillumination);
            }

            set
            {
                base.SetSystemBoolean(NosocomialMonitoringNgtMetadata.ColumnNames.IsTransillumination, value);
            }
        }
        /// <summary>
        /// Maps to NosocomialMonitoringNgt.IsCulture
        /// </summary>
        virtual public System.Boolean? IsCulture
        {
            get
            {
                return base.GetSystemBoolean(NosocomialMonitoringNgtMetadata.ColumnNames.IsCulture);
            }

            set
            {
                base.SetSystemBoolean(NosocomialMonitoringNgtMetadata.ColumnNames.IsCulture, value);
            }
        }
        /// <summary>
        /// Maps to NosocomialMonitoringNgt.Note
        /// </summary>
        virtual public System.String Note
        {
            get
            {
                return base.GetSystemString(NosocomialMonitoringNgtMetadata.ColumnNames.Note);
            }

            set
            {
                base.SetSystemString(NosocomialMonitoringNgtMetadata.ColumnNames.Note, value);
            }
        }
        /// <summary>
        /// Maps to NosocomialMonitoringNgt.IsDeleted
        /// </summary>
        virtual public System.Boolean? IsDeleted
        {
            get
            {
                return base.GetSystemBoolean(NosocomialMonitoringNgtMetadata.ColumnNames.IsDeleted);
            }

            set
            {
                base.SetSystemBoolean(NosocomialMonitoringNgtMetadata.ColumnNames.IsDeleted, value);
            }
        }
        /// <summary>
        /// Maps to NosocomialMonitoringNgt.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(NosocomialMonitoringNgtMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(NosocomialMonitoringNgtMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to NosocomialMonitoringNgt.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(NosocomialMonitoringNgtMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(NosocomialMonitoringNgtMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to NosocomialMonitoringNgt.MonitoringByUserID
        /// </summary>
        virtual public System.String MonitoringByUserID
        {
            get
            {
                return base.GetSystemString(NosocomialMonitoringNgtMetadata.ColumnNames.MonitoringByUserID);
            }

            set
            {
                base.SetSystemString(NosocomialMonitoringNgtMetadata.ColumnNames.MonitoringByUserID, value);
            }
        }
        /// <summary>
        /// Maps to NosocomialMonitoringNgt.IsRelease
        /// </summary>
        virtual public System.Boolean? IsRelease
        {
            get
            {
                return base.GetSystemBoolean(NosocomialMonitoringNgtMetadata.ColumnNames.IsRelease);
            }

            set
            {
                base.SetSystemBoolean(NosocomialMonitoringNgtMetadata.ColumnNames.IsRelease, value);
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
            public esStrings(esNosocomialMonitoringNgt entity)
            {
                this.entity = entity;
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
            public System.String MonitoringNo
            {
                get
                {
                    System.Int32? data = entity.MonitoringNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.MonitoringNo = null;
                    else entity.MonitoringNo = Convert.ToInt32(value);
                }
            }
            public System.String SequenceNo
            {
                get
                {
                    System.Int32? data = entity.SequenceNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SequenceNo = null;
                    else entity.SequenceNo = Convert.ToInt32(value);
                }
            }
            public System.String MonitoringDateTime
            {
                get
                {
                    System.DateTime? data = entity.MonitoringDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.MonitoringDateTime = null;
                    else entity.MonitoringDateTime = Convert.ToDateTime(value);
                }
            }
            public System.String Replacement
            {
                get
                {
                    System.String data = entity.Replacement;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Replacement = null;
                    else entity.Replacement = Convert.ToString(value);
                }
            }
            public System.String IsTempAbove38
            {
                get
                {
                    System.Boolean? data = entity.IsTempAbove38;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsTempAbove38 = null;
                    else entity.IsTempAbove38 = Convert.ToBoolean(value);
                }
            }
            public System.String IsPus
            {
                get
                {
                    System.Boolean? data = entity.IsPus;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsPus = null;
                    else entity.IsPus = Convert.ToBoolean(value);
                }
            }
            public System.String IsPain
            {
                get
                {
                    System.Boolean? data = entity.IsPain;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsPain = null;
                    else entity.IsPain = Convert.ToBoolean(value);
                }
            }
            public System.String IsHeadache
            {
                get
                {
                    System.Boolean? data = entity.IsHeadache;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsHeadache = null;
                    else entity.IsHeadache = Convert.ToBoolean(value);
                }
            }
            public System.String IsNoseClogged
            {
                get
                {
                    System.Boolean? data = entity.IsNoseClogged;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsNoseClogged = null;
                    else entity.IsNoseClogged = Convert.ToBoolean(value);
                }
            }
            public System.String IsPainSwallow
            {
                get
                {
                    System.Boolean? data = entity.IsPainSwallow;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsPainSwallow = null;
                    else entity.IsPainSwallow = Convert.ToBoolean(value);
                }
            }
            public System.String IsPharingRedness
            {
                get
                {
                    System.Boolean? data = entity.IsPharingRedness;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsPharingRedness = null;
                    else entity.IsPharingRedness = Convert.ToBoolean(value);
                }
            }
            public System.String IsCough
            {
                get
                {
                    System.Boolean? data = entity.IsCough;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsCough = null;
                    else entity.IsCough = Convert.ToBoolean(value);
                }
            }
            public System.String IsTransillumination
            {
                get
                {
                    System.Boolean? data = entity.IsTransillumination;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsTransillumination = null;
                    else entity.IsTransillumination = Convert.ToBoolean(value);
                }
            }
            public System.String IsCulture
            {
                get
                {
                    System.Boolean? data = entity.IsCulture;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsCulture = null;
                    else entity.IsCulture = Convert.ToBoolean(value);
                }
            }
            public System.String Note
            {
                get
                {
                    System.String data = entity.Note;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Note = null;
                    else entity.Note = Convert.ToString(value);
                }
            }
            public System.String IsDeleted
            {
                get
                {
                    System.Boolean? data = entity.IsDeleted;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsDeleted = null;
                    else entity.IsDeleted = Convert.ToBoolean(value);
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
            public System.String MonitoringByUserID
            {
                get
                {
                    System.String data = entity.MonitoringByUserID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.MonitoringByUserID = null;
                    else entity.MonitoringByUserID = Convert.ToString(value);
                }
            }
            public System.String IsRelease
            {
                get
                {
                    System.Boolean? data = entity.IsRelease;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsRelease = null;
                    else entity.IsRelease = Convert.ToBoolean(value);
                }
            }
            private esNosocomialMonitoringNgt entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esNosocomialMonitoringNgtQuery query)
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
                throw new Exception("esNosocomialMonitoringNgt can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class NosocomialMonitoringNgt : esNosocomialMonitoringNgt
    {
    }

    [Serializable]
    abstract public class esNosocomialMonitoringNgtQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return NosocomialMonitoringNgtMetadata.Meta();
            }
        }

        public esQueryItem RegistrationNo
        {
            get
            {
                return new esQueryItem(this, NosocomialMonitoringNgtMetadata.ColumnNames.RegistrationNo, esSystemType.String);
            }
        }

        public esQueryItem MonitoringNo
        {
            get
            {
                return new esQueryItem(this, NosocomialMonitoringNgtMetadata.ColumnNames.MonitoringNo, esSystemType.Int32);
            }
        }

        public esQueryItem SequenceNo
        {
            get
            {
                return new esQueryItem(this, NosocomialMonitoringNgtMetadata.ColumnNames.SequenceNo, esSystemType.Int32);
            }
        }

        public esQueryItem MonitoringDateTime
        {
            get
            {
                return new esQueryItem(this, NosocomialMonitoringNgtMetadata.ColumnNames.MonitoringDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem Replacement
        {
            get
            {
                return new esQueryItem(this, NosocomialMonitoringNgtMetadata.ColumnNames.Replacement, esSystemType.String);
            }
        }

        public esQueryItem IsTempAbove38
        {
            get
            {
                return new esQueryItem(this, NosocomialMonitoringNgtMetadata.ColumnNames.IsTempAbove38, esSystemType.Boolean);
            }
        }

        public esQueryItem IsPus
        {
            get
            {
                return new esQueryItem(this, NosocomialMonitoringNgtMetadata.ColumnNames.IsPus, esSystemType.Boolean);
            }
        }

        public esQueryItem IsPain
        {
            get
            {
                return new esQueryItem(this, NosocomialMonitoringNgtMetadata.ColumnNames.IsPain, esSystemType.Boolean);
            }
        }

        public esQueryItem IsHeadache
        {
            get
            {
                return new esQueryItem(this, NosocomialMonitoringNgtMetadata.ColumnNames.IsHeadache, esSystemType.Boolean);
            }
        }

        public esQueryItem IsNoseClogged
        {
            get
            {
                return new esQueryItem(this, NosocomialMonitoringNgtMetadata.ColumnNames.IsNoseClogged, esSystemType.Boolean);
            }
        }

        public esQueryItem IsPainSwallow
        {
            get
            {
                return new esQueryItem(this, NosocomialMonitoringNgtMetadata.ColumnNames.IsPainSwallow, esSystemType.Boolean);
            }
        }

        public esQueryItem IsPharingRedness
        {
            get
            {
                return new esQueryItem(this, NosocomialMonitoringNgtMetadata.ColumnNames.IsPharingRedness, esSystemType.Boolean);
            }
        }

        public esQueryItem IsCough
        {
            get
            {
                return new esQueryItem(this, NosocomialMonitoringNgtMetadata.ColumnNames.IsCough, esSystemType.Boolean);
            }
        }

        public esQueryItem IsTransillumination
        {
            get
            {
                return new esQueryItem(this, NosocomialMonitoringNgtMetadata.ColumnNames.IsTransillumination, esSystemType.Boolean);
            }
        }

        public esQueryItem IsCulture
        {
            get
            {
                return new esQueryItem(this, NosocomialMonitoringNgtMetadata.ColumnNames.IsCulture, esSystemType.Boolean);
            }
        }

        public esQueryItem Note
        {
            get
            {
                return new esQueryItem(this, NosocomialMonitoringNgtMetadata.ColumnNames.Note, esSystemType.String);
            }
        }

        public esQueryItem IsDeleted
        {
            get
            {
                return new esQueryItem(this, NosocomialMonitoringNgtMetadata.ColumnNames.IsDeleted, esSystemType.Boolean);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, NosocomialMonitoringNgtMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, NosocomialMonitoringNgtMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem MonitoringByUserID
        {
            get
            {
                return new esQueryItem(this, NosocomialMonitoringNgtMetadata.ColumnNames.MonitoringByUserID, esSystemType.String);
            }
        }

        public esQueryItem IsRelease
        {
            get
            {
                return new esQueryItem(this, NosocomialMonitoringNgtMetadata.ColumnNames.IsRelease, esSystemType.Boolean);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("NosocomialMonitoringNgtCollection")]
    public partial class NosocomialMonitoringNgtCollection : esNosocomialMonitoringNgtCollection, IEnumerable<NosocomialMonitoringNgt>
    {
        public NosocomialMonitoringNgtCollection()
        {

        }

        public static implicit operator List<NosocomialMonitoringNgt>(NosocomialMonitoringNgtCollection coll)
        {
            List<NosocomialMonitoringNgt> list = new List<NosocomialMonitoringNgt>();

            foreach (NosocomialMonitoringNgt emp in coll)
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
                return NosocomialMonitoringNgtMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new NosocomialMonitoringNgtQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new NosocomialMonitoringNgt(row);
        }

        override protected esEntity CreateEntity()
        {
            return new NosocomialMonitoringNgt();
        }

        #endregion

        [BrowsableAttribute(false)]
        public NosocomialMonitoringNgtQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new NosocomialMonitoringNgtQuery();
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
        public bool Load(NosocomialMonitoringNgtQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public NosocomialMonitoringNgt AddNew()
        {
            NosocomialMonitoringNgt entity = base.AddNewEntity() as NosocomialMonitoringNgt;

            return entity;
        }
        public NosocomialMonitoringNgt FindByPrimaryKey(String registrationNo, Int32 monitoringNo, Int32 sequenceNo)
        {
            return base.FindByPrimaryKey(registrationNo, monitoringNo, sequenceNo) as NosocomialMonitoringNgt;
        }

        #region IEnumerable< NosocomialMonitoringNgt> Members

        IEnumerator<NosocomialMonitoringNgt> IEnumerable<NosocomialMonitoringNgt>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as NosocomialMonitoringNgt;
            }
        }

        #endregion

        private NosocomialMonitoringNgtQuery query;
    }


    /// <summary>
    /// Encapsulates the 'NosocomialMonitoringNgt' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("NosocomialMonitoringNgt ({RegistrationNo, MonitoringNo, SequenceNo})")]
    [Serializable]
    public partial class NosocomialMonitoringNgt : esNosocomialMonitoringNgt
    {
        public NosocomialMonitoringNgt()
        {
        }

        public NosocomialMonitoringNgt(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return NosocomialMonitoringNgtMetadata.Meta();
            }
        }

        override protected esNosocomialMonitoringNgtQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new NosocomialMonitoringNgtQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public NosocomialMonitoringNgtQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new NosocomialMonitoringNgtQuery();
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
        public bool Load(NosocomialMonitoringNgtQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private NosocomialMonitoringNgtQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class NosocomialMonitoringNgtQuery : esNosocomialMonitoringNgtQuery
    {
        public NosocomialMonitoringNgtQuery()
        {

        }

        public NosocomialMonitoringNgtQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "NosocomialMonitoringNgtQuery";
        }
    }

    [Serializable]
    public partial class NosocomialMonitoringNgtMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected NosocomialMonitoringNgtMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(NosocomialMonitoringNgtMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = NosocomialMonitoringNgtMetadata.PropertyNames.RegistrationNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(NosocomialMonitoringNgtMetadata.ColumnNames.MonitoringNo, 1, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = NosocomialMonitoringNgtMetadata.PropertyNames.MonitoringNo;
            c.IsInPrimaryKey = true;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(NosocomialMonitoringNgtMetadata.ColumnNames.SequenceNo, 2, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = NosocomialMonitoringNgtMetadata.PropertyNames.SequenceNo;
            c.IsInPrimaryKey = true;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(NosocomialMonitoringNgtMetadata.ColumnNames.MonitoringDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = NosocomialMonitoringNgtMetadata.PropertyNames.MonitoringDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NosocomialMonitoringNgtMetadata.ColumnNames.Replacement, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = NosocomialMonitoringNgtMetadata.PropertyNames.Replacement;
            c.CharacterMaxLength = 200;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NosocomialMonitoringNgtMetadata.ColumnNames.IsTempAbove38, 5, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = NosocomialMonitoringNgtMetadata.PropertyNames.IsTempAbove38;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NosocomialMonitoringNgtMetadata.ColumnNames.IsPus, 6, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = NosocomialMonitoringNgtMetadata.PropertyNames.IsPus;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NosocomialMonitoringNgtMetadata.ColumnNames.IsPain, 7, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = NosocomialMonitoringNgtMetadata.PropertyNames.IsPain;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NosocomialMonitoringNgtMetadata.ColumnNames.IsHeadache, 8, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = NosocomialMonitoringNgtMetadata.PropertyNames.IsHeadache;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NosocomialMonitoringNgtMetadata.ColumnNames.IsNoseClogged, 9, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = NosocomialMonitoringNgtMetadata.PropertyNames.IsNoseClogged;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NosocomialMonitoringNgtMetadata.ColumnNames.IsPainSwallow, 10, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = NosocomialMonitoringNgtMetadata.PropertyNames.IsPainSwallow;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NosocomialMonitoringNgtMetadata.ColumnNames.IsPharingRedness, 11, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = NosocomialMonitoringNgtMetadata.PropertyNames.IsPharingRedness;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NosocomialMonitoringNgtMetadata.ColumnNames.IsCough, 12, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = NosocomialMonitoringNgtMetadata.PropertyNames.IsCough;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NosocomialMonitoringNgtMetadata.ColumnNames.IsTransillumination, 13, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = NosocomialMonitoringNgtMetadata.PropertyNames.IsTransillumination;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NosocomialMonitoringNgtMetadata.ColumnNames.IsCulture, 14, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = NosocomialMonitoringNgtMetadata.PropertyNames.IsCulture;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NosocomialMonitoringNgtMetadata.ColumnNames.Note, 15, typeof(System.String), esSystemType.String);
            c.PropertyName = NosocomialMonitoringNgtMetadata.PropertyNames.Note;
            c.CharacterMaxLength = 200;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NosocomialMonitoringNgtMetadata.ColumnNames.IsDeleted, 16, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = NosocomialMonitoringNgtMetadata.PropertyNames.IsDeleted;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NosocomialMonitoringNgtMetadata.ColumnNames.LastUpdateDateTime, 17, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = NosocomialMonitoringNgtMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NosocomialMonitoringNgtMetadata.ColumnNames.LastUpdateByUserID, 18, typeof(System.String), esSystemType.String);
            c.PropertyName = NosocomialMonitoringNgtMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NosocomialMonitoringNgtMetadata.ColumnNames.MonitoringByUserID, 19, typeof(System.String), esSystemType.String);
            c.PropertyName = NosocomialMonitoringNgtMetadata.PropertyNames.MonitoringByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NosocomialMonitoringNgtMetadata.ColumnNames.IsRelease, 20, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = NosocomialMonitoringNgtMetadata.PropertyNames.IsRelease;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public NosocomialMonitoringNgtMetadata Meta()
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
            public const string RegistrationNo = "RegistrationNo";
            public const string MonitoringNo = "MonitoringNo";
            public const string SequenceNo = "SequenceNo";
            public const string MonitoringDateTime = "MonitoringDateTime";
            public const string Replacement = "Replacement";
            public const string IsTempAbove38 = "IsTempAbove38";
            public const string IsPus = "IsPus";
            public const string IsPain = "IsPain";
            public const string IsHeadache = "IsHeadache";
            public const string IsNoseClogged = "IsNoseClogged";
            public const string IsPainSwallow = "IsPainSwallow";
            public const string IsPharingRedness = "IsPharingRedness";
            public const string IsCough = "IsCough";
            public const string IsTransillumination = "IsTransillumination";
            public const string IsCulture = "IsCulture";
            public const string Note = "Note";
            public const string IsDeleted = "IsDeleted";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string MonitoringByUserID = "MonitoringByUserID";
            public const string IsRelease = "IsRelease";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string RegistrationNo = "RegistrationNo";
            public const string MonitoringNo = "MonitoringNo";
            public const string SequenceNo = "SequenceNo";
            public const string MonitoringDateTime = "MonitoringDateTime";
            public const string Replacement = "Replacement";
            public const string IsTempAbove38 = "IsTempAbove38";
            public const string IsPus = "IsPus";
            public const string IsPain = "IsPain";
            public const string IsHeadache = "IsHeadache";
            public const string IsNoseClogged = "IsNoseClogged";
            public const string IsPainSwallow = "IsPainSwallow";
            public const string IsPharingRedness = "IsPharingRedness";
            public const string IsCough = "IsCough";
            public const string IsTransillumination = "IsTransillumination";
            public const string IsCulture = "IsCulture";
            public const string Note = "Note";
            public const string IsDeleted = "IsDeleted";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string MonitoringByUserID = "MonitoringByUserID";
            public const string IsRelease = "IsRelease";
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
            lock (typeof(NosocomialMonitoringNgtMetadata))
            {
                if (NosocomialMonitoringNgtMetadata.mapDelegates == null)
                {
                    NosocomialMonitoringNgtMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (NosocomialMonitoringNgtMetadata.meta == null)
                {
                    NosocomialMonitoringNgtMetadata.meta = new NosocomialMonitoringNgtMetadata();
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

                meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("MonitoringNo", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("SequenceNo", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("MonitoringDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("Replacement", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsTempAbove38", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsPus", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsPain", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsHeadache", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsNoseClogged", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsPainSwallow", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsPharingRedness", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsCough", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsTransillumination", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsCulture", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("Note", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsDeleted", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("MonitoringByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsRelease", new esTypeMap("bit", "System.Boolean"));


                meta.Source = "NosocomialMonitoringNgt";
                meta.Destination = "NosocomialMonitoringNgt";
                meta.spInsert = "proc_NosocomialMonitoringNgtInsert";
                meta.spUpdate = "proc_NosocomialMonitoringNgtUpdate";
                meta.spDelete = "proc_NosocomialMonitoringNgtDelete";
                meta.spLoadAll = "proc_NosocomialMonitoringNgtLoadAll";
                meta.spLoadByPrimaryKey = "proc_NosocomialMonitoringNgtLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private NosocomialMonitoringNgtMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
