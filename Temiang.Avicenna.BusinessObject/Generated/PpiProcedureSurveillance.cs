/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 10/9/2017 11:01:52 AM
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
    abstract public class esPpiProcedureSurveillanceCollection : esEntityCollectionWAuditLog
    {
        public esPpiProcedureSurveillanceCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "PpiProcedureSurveillanceCollection";
        }

        #region Query Logic
        protected void InitQuery(esPpiProcedureSurveillanceQuery query)
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
            this.InitQuery(query as esPpiProcedureSurveillanceQuery);
        }
        #endregion

        virtual public PpiProcedureSurveillance DetachEntity(PpiProcedureSurveillance entity)
        {
            return base.DetachEntity(entity) as PpiProcedureSurveillance;
        }

        virtual public PpiProcedureSurveillance AttachEntity(PpiProcedureSurveillance entity)
        {
            return base.AttachEntity(entity) as PpiProcedureSurveillance;
        }

        virtual public void Combine(PpiProcedureSurveillanceCollection collection)
        {
            base.Combine(collection);
        }

        new public PpiProcedureSurveillance this[int index]
        {
            get
            {
                return base[index] as PpiProcedureSurveillance;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(PpiProcedureSurveillance);
        }
    }

    [Serializable]
    abstract public class esPpiProcedureSurveillance : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esPpiProcedureSurveillanceQuery GetDynamicQuery()
        {
            return null;
        }

        public esPpiProcedureSurveillance()
        {
        }

        public esPpiProcedureSurveillance(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String bookingNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(bookingNo);
            else
                return LoadByPrimaryKeyStoredProcedure(bookingNo);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String bookingNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(bookingNo);
            else
                return LoadByPrimaryKeyStoredProcedure(bookingNo);
        }

        private bool LoadByPrimaryKeyDynamic(String bookingNo)
        {
            esPpiProcedureSurveillanceQuery query = this.GetDynamicQuery();
            query.Where(query.BookingNo == bookingNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String bookingNo)
        {
            esParameters parms = new esParameters();
            parms.Add("BookingNo", bookingNo);
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
                        case "BookingNo": this.str.BookingNo = (string)value; break;
                        case "IsRiskFactorAge": this.str.IsRiskFactorAge = (string)value; break;
                        case "IsRiskFactorNutrient": this.str.IsRiskFactorNutrient = (string)value; break;
                        case "IsRiskFactorObesity": this.str.IsRiskFactorObesity = (string)value; break;
                        case "IsDiabetes": this.str.IsDiabetes = (string)value; break;
                        case "IsHypertension": this.str.IsHypertension = (string)value; break;
                        case "IsHiv": this.str.IsHiv = (string)value; break;
                        case "IsHbv": this.str.IsHbv = (string)value; break;
                        case "IsHcv": this.str.IsHcv = (string)value; break;
                        case "SRProcedureClassification": this.str.SRProcedureClassification = (string)value; break;
                        case "SRTypesOfSurgery": this.str.SRTypesOfSurgery = (string)value; break;
                        case "SRRiskCategory": this.str.SRRiskCategory = (string)value; break;
                        case "SRWoundClassification": this.str.SRWoundClassification = (string)value; break;
                        case "SRAsaScore": this.str.SRAsaScore = (string)value; break;
                        case "SRTTime": this.str.SRTTime = (string)value; break;
                        case "Culturs": this.str.Culturs = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "IsRiskFactorAge":

                            if (value == null || value is System.Boolean)
                                this.IsRiskFactorAge = (System.Boolean?)value;
                            break;
                        case "IsRiskFactorNutrient":

                            if (value == null || value is System.Boolean)
                                this.IsRiskFactorNutrient = (System.Boolean?)value;
                            break;
                        case "IsRiskFactorObesity":

                            if (value == null || value is System.Boolean)
                                this.IsRiskFactorObesity = (System.Boolean?)value;
                            break;
                        case "IsDiabetes":

                            if (value == null || value is System.Boolean)
                                this.IsDiabetes = (System.Boolean?)value;
                            break;
                        case "IsHypertension":

                            if (value == null || value is System.Boolean)
                                this.IsHypertension = (System.Boolean?)value;
                            break;
                        case "IsHiv":

                            if (value == null || value is System.Boolean)
                                this.IsHiv = (System.Boolean?)value;
                            break;
                        case "IsHbv":

                            if (value == null || value is System.Boolean)
                                this.IsHbv = (System.Boolean?)value;
                            break;
                        case "IsHcv":

                            if (value == null || value is System.Boolean)
                                this.IsHcv = (System.Boolean?)value;
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
        /// Maps to PpiProcedureSurveillance.BookingNo
        /// </summary>
        virtual public System.String BookingNo
        {
            get
            {
                return base.GetSystemString(PpiProcedureSurveillanceMetadata.ColumnNames.BookingNo);
            }

            set
            {
                base.SetSystemString(PpiProcedureSurveillanceMetadata.ColumnNames.BookingNo, value);
            }
        }
        /// <summary>
        /// Maps to PpiProcedureSurveillance.IsRiskFactorAge
        /// </summary>
        virtual public System.Boolean? IsRiskFactorAge
        {
            get
            {
                return base.GetSystemBoolean(PpiProcedureSurveillanceMetadata.ColumnNames.IsRiskFactorAge);
            }

            set
            {
                base.SetSystemBoolean(PpiProcedureSurveillanceMetadata.ColumnNames.IsRiskFactorAge, value);
            }
        }
        /// <summary>
        /// Maps to PpiProcedureSurveillance.IsRiskFactorNutrient
        /// </summary>
        virtual public System.Boolean? IsRiskFactorNutrient
        {
            get
            {
                return base.GetSystemBoolean(PpiProcedureSurveillanceMetadata.ColumnNames.IsRiskFactorNutrient);
            }

            set
            {
                base.SetSystemBoolean(PpiProcedureSurveillanceMetadata.ColumnNames.IsRiskFactorNutrient, value);
            }
        }
        /// <summary>
        /// Maps to PpiProcedureSurveillance.IsRiskFactorObesity
        /// </summary>
        virtual public System.Boolean? IsRiskFactorObesity
        {
            get
            {
                return base.GetSystemBoolean(PpiProcedureSurveillanceMetadata.ColumnNames.IsRiskFactorObesity);
            }

            set
            {
                base.SetSystemBoolean(PpiProcedureSurveillanceMetadata.ColumnNames.IsRiskFactorObesity, value);
            }
        }
        /// <summary>
        /// Maps to PpiProcedureSurveillance.IsDiabetes
        /// </summary>
        virtual public System.Boolean? IsDiabetes
        {
            get
            {
                return base.GetSystemBoolean(PpiProcedureSurveillanceMetadata.ColumnNames.IsDiabetes);
            }

            set
            {
                base.SetSystemBoolean(PpiProcedureSurveillanceMetadata.ColumnNames.IsDiabetes, value);
            }
        }
        /// <summary>
        /// Maps to PpiProcedureSurveillance.IsHypertension
        /// </summary>
        virtual public System.Boolean? IsHypertension
        {
            get
            {
                return base.GetSystemBoolean(PpiProcedureSurveillanceMetadata.ColumnNames.IsHypertension);
            }

            set
            {
                base.SetSystemBoolean(PpiProcedureSurveillanceMetadata.ColumnNames.IsHypertension, value);
            }
        }
        /// <summary>
        /// Maps to PpiProcedureSurveillance.IsHiv
        /// </summary>
        virtual public System.Boolean? IsHiv
        {
            get
            {
                return base.GetSystemBoolean(PpiProcedureSurveillanceMetadata.ColumnNames.IsHiv);
            }

            set
            {
                base.SetSystemBoolean(PpiProcedureSurveillanceMetadata.ColumnNames.IsHiv, value);
            }
        }
        /// <summary>
        /// Maps to PpiProcedureSurveillance.IsHbv
        /// </summary>
        virtual public System.Boolean? IsHbv
        {
            get
            {
                return base.GetSystemBoolean(PpiProcedureSurveillanceMetadata.ColumnNames.IsHbv);
            }

            set
            {
                base.SetSystemBoolean(PpiProcedureSurveillanceMetadata.ColumnNames.IsHbv, value);
            }
        }
        /// <summary>
        /// Maps to PpiProcedureSurveillance.IsHcv
        /// </summary>
        virtual public System.Boolean? IsHcv
        {
            get
            {
                return base.GetSystemBoolean(PpiProcedureSurveillanceMetadata.ColumnNames.IsHcv);
            }

            set
            {
                base.SetSystemBoolean(PpiProcedureSurveillanceMetadata.ColumnNames.IsHcv, value);
            }
        }
        /// <summary>
        /// Maps to PpiProcedureSurveillance.SRProcedureClassification
        /// </summary>
        virtual public System.String SRProcedureClassification
        {
            get
            {
                return base.GetSystemString(PpiProcedureSurveillanceMetadata.ColumnNames.SRProcedureClassification);
            }

            set
            {
                base.SetSystemString(PpiProcedureSurveillanceMetadata.ColumnNames.SRProcedureClassification, value);
            }
        }
        /// <summary>
        /// Maps to PpiProcedureSurveillance.SRTypesOfSurgery
        /// </summary>
        virtual public System.String SRTypesOfSurgery
        {
            get
            {
                return base.GetSystemString(PpiProcedureSurveillanceMetadata.ColumnNames.SRTypesOfSurgery);
            }

            set
            {
                base.SetSystemString(PpiProcedureSurveillanceMetadata.ColumnNames.SRTypesOfSurgery, value);
            }
        }
        /// <summary>
        /// Maps to PpiProcedureSurveillance.SRRiskCategory
        /// </summary>
        virtual public System.String SRRiskCategory
        {
            get
            {
                return base.GetSystemString(PpiProcedureSurveillanceMetadata.ColumnNames.SRRiskCategory);
            }

            set
            {
                base.SetSystemString(PpiProcedureSurveillanceMetadata.ColumnNames.SRRiskCategory, value);
            }
        }
        /// <summary>
        /// Maps to PpiProcedureSurveillance.SRWoundClassification
        /// </summary>
        virtual public System.String SRWoundClassification
        {
            get
            {
                return base.GetSystemString(PpiProcedureSurveillanceMetadata.ColumnNames.SRWoundClassification);
            }

            set
            {
                base.SetSystemString(PpiProcedureSurveillanceMetadata.ColumnNames.SRWoundClassification, value);
            }
        }
        /// <summary>
        /// Maps to PpiProcedureSurveillance.SRAsaScore
        /// </summary>
        virtual public System.String SRAsaScore
        {
            get
            {
                return base.GetSystemString(PpiProcedureSurveillanceMetadata.ColumnNames.SRAsaScore);
            }

            set
            {
                base.SetSystemString(PpiProcedureSurveillanceMetadata.ColumnNames.SRAsaScore, value);
            }
        }
        /// <summary>
        /// Maps to PpiProcedureSurveillance.SRTTime
        /// </summary>
        virtual public System.String SRTTime
        {
            get
            {
                return base.GetSystemString(PpiProcedureSurveillanceMetadata.ColumnNames.SRTTime);
            }

            set
            {
                base.SetSystemString(PpiProcedureSurveillanceMetadata.ColumnNames.SRTTime, value);
            }
        }
        /// <summary>
        /// Maps to PpiProcedureSurveillance.Culturs
        /// </summary>
        virtual public System.String Culturs
        {
            get
            {
                return base.GetSystemString(PpiProcedureSurveillanceMetadata.ColumnNames.Culturs);
            }

            set
            {
                base.SetSystemString(PpiProcedureSurveillanceMetadata.ColumnNames.Culturs, value);
            }
        }
        /// <summary>
        /// Maps to PpiProcedureSurveillance.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(PpiProcedureSurveillanceMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(PpiProcedureSurveillanceMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to PpiProcedureSurveillance.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(PpiProcedureSurveillanceMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(PpiProcedureSurveillanceMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esPpiProcedureSurveillance entity)
            {
                this.entity = entity;
            }
            public System.String BookingNo
            {
                get
                {
                    System.String data = entity.BookingNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.BookingNo = null;
                    else entity.BookingNo = Convert.ToString(value);
                }
            }
            public System.String IsRiskFactorAge
            {
                get
                {
                    System.Boolean? data = entity.IsRiskFactorAge;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsRiskFactorAge = null;
                    else entity.IsRiskFactorAge = Convert.ToBoolean(value);
                }
            }
            public System.String IsRiskFactorNutrient
            {
                get
                {
                    System.Boolean? data = entity.IsRiskFactorNutrient;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsRiskFactorNutrient = null;
                    else entity.IsRiskFactorNutrient = Convert.ToBoolean(value);
                }
            }
            public System.String IsRiskFactorObesity
            {
                get
                {
                    System.Boolean? data = entity.IsRiskFactorObesity;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsRiskFactorObesity = null;
                    else entity.IsRiskFactorObesity = Convert.ToBoolean(value);
                }
            }
            public System.String IsDiabetes
            {
                get
                {
                    System.Boolean? data = entity.IsDiabetes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsDiabetes = null;
                    else entity.IsDiabetes = Convert.ToBoolean(value);
                }
            }
            public System.String IsHypertension
            {
                get
                {
                    System.Boolean? data = entity.IsHypertension;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsHypertension = null;
                    else entity.IsHypertension = Convert.ToBoolean(value);
                }
            }
            public System.String IsHiv
            {
                get
                {
                    System.Boolean? data = entity.IsHiv;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsHiv = null;
                    else entity.IsHiv = Convert.ToBoolean(value);
                }
            }
            public System.String IsHbv
            {
                get
                {
                    System.Boolean? data = entity.IsHbv;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsHbv = null;
                    else entity.IsHbv = Convert.ToBoolean(value);
                }
            }
            public System.String IsHcv
            {
                get
                {
                    System.Boolean? data = entity.IsHcv;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsHcv = null;
                    else entity.IsHcv = Convert.ToBoolean(value);
                }
            }
            public System.String SRProcedureClassification
            {
                get
                {
                    System.String data = entity.SRProcedureClassification;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRProcedureClassification = null;
                    else entity.SRProcedureClassification = Convert.ToString(value);
                }
            }
            public System.String SRTypesOfSurgery
            {
                get
                {
                    System.String data = entity.SRTypesOfSurgery;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRTypesOfSurgery = null;
                    else entity.SRTypesOfSurgery = Convert.ToString(value);
                }
            }
            public System.String SRRiskCategory
            {
                get
                {
                    System.String data = entity.SRRiskCategory;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRRiskCategory = null;
                    else entity.SRRiskCategory = Convert.ToString(value);
                }
            }
            public System.String SRWoundClassification
            {
                get
                {
                    System.String data = entity.SRWoundClassification;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRWoundClassification = null;
                    else entity.SRWoundClassification = Convert.ToString(value);
                }
            }
            public System.String SRAsaScore
            {
                get
                {
                    System.String data = entity.SRAsaScore;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRAsaScore = null;
                    else entity.SRAsaScore = Convert.ToString(value);
                }
            }
            public System.String SRTTime
            {
                get
                {
                    System.String data = entity.SRTTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRTTime = null;
                    else entity.SRTTime = Convert.ToString(value);
                }
            }
            public System.String Culturs
            {
                get
                {
                    System.String data = entity.Culturs;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Culturs = null;
                    else entity.Culturs = Convert.ToString(value);
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
            private esPpiProcedureSurveillance entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esPpiProcedureSurveillanceQuery query)
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
                throw new Exception("esPpiProcedureSurveillance can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class PpiProcedureSurveillance : esPpiProcedureSurveillance
    {
    }

    [Serializable]
    abstract public class esPpiProcedureSurveillanceQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return PpiProcedureSurveillanceMetadata.Meta();
            }
        }

        public esQueryItem BookingNo
        {
            get
            {
                return new esQueryItem(this, PpiProcedureSurveillanceMetadata.ColumnNames.BookingNo, esSystemType.String);
            }
        }

        public esQueryItem IsRiskFactorAge
        {
            get
            {
                return new esQueryItem(this, PpiProcedureSurveillanceMetadata.ColumnNames.IsRiskFactorAge, esSystemType.Boolean);
            }
        }

        public esQueryItem IsRiskFactorNutrient
        {
            get
            {
                return new esQueryItem(this, PpiProcedureSurveillanceMetadata.ColumnNames.IsRiskFactorNutrient, esSystemType.Boolean);
            }
        }

        public esQueryItem IsRiskFactorObesity
        {
            get
            {
                return new esQueryItem(this, PpiProcedureSurveillanceMetadata.ColumnNames.IsRiskFactorObesity, esSystemType.Boolean);
            }
        }

        public esQueryItem IsDiabetes
        {
            get
            {
                return new esQueryItem(this, PpiProcedureSurveillanceMetadata.ColumnNames.IsDiabetes, esSystemType.Boolean);
            }
        }

        public esQueryItem IsHypertension
        {
            get
            {
                return new esQueryItem(this, PpiProcedureSurveillanceMetadata.ColumnNames.IsHypertension, esSystemType.Boolean);
            }
        }

        public esQueryItem IsHiv
        {
            get
            {
                return new esQueryItem(this, PpiProcedureSurveillanceMetadata.ColumnNames.IsHiv, esSystemType.Boolean);
            }
        }

        public esQueryItem IsHbv
        {
            get
            {
                return new esQueryItem(this, PpiProcedureSurveillanceMetadata.ColumnNames.IsHbv, esSystemType.Boolean);
            }
        }

        public esQueryItem IsHcv
        {
            get
            {
                return new esQueryItem(this, PpiProcedureSurveillanceMetadata.ColumnNames.IsHcv, esSystemType.Boolean);
            }
        }

        public esQueryItem SRProcedureClassification
        {
            get
            {
                return new esQueryItem(this, PpiProcedureSurveillanceMetadata.ColumnNames.SRProcedureClassification, esSystemType.String);
            }
        }

        public esQueryItem SRTypesOfSurgery
        {
            get
            {
                return new esQueryItem(this, PpiProcedureSurveillanceMetadata.ColumnNames.SRTypesOfSurgery, esSystemType.String);
            }
        }

        public esQueryItem SRRiskCategory
        {
            get
            {
                return new esQueryItem(this, PpiProcedureSurveillanceMetadata.ColumnNames.SRRiskCategory, esSystemType.String);
            }
        }

        public esQueryItem SRWoundClassification
        {
            get
            {
                return new esQueryItem(this, PpiProcedureSurveillanceMetadata.ColumnNames.SRWoundClassification, esSystemType.String);
            }
        }

        public esQueryItem SRAsaScore
        {
            get
            {
                return new esQueryItem(this, PpiProcedureSurveillanceMetadata.ColumnNames.SRAsaScore, esSystemType.String);
            }
        }

        public esQueryItem SRTTime
        {
            get
            {
                return new esQueryItem(this, PpiProcedureSurveillanceMetadata.ColumnNames.SRTTime, esSystemType.String);
            }
        }

        public esQueryItem Culturs
        {
            get
            {
                return new esQueryItem(this, PpiProcedureSurveillanceMetadata.ColumnNames.Culturs, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, PpiProcedureSurveillanceMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, PpiProcedureSurveillanceMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("PpiProcedureSurveillanceCollection")]
    public partial class PpiProcedureSurveillanceCollection : esPpiProcedureSurveillanceCollection, IEnumerable<PpiProcedureSurveillance>
    {
        public PpiProcedureSurveillanceCollection()
        {

        }

        public static implicit operator List<PpiProcedureSurveillance>(PpiProcedureSurveillanceCollection coll)
        {
            List<PpiProcedureSurveillance> list = new List<PpiProcedureSurveillance>();

            foreach (PpiProcedureSurveillance emp in coll)
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
                return PpiProcedureSurveillanceMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PpiProcedureSurveillanceQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new PpiProcedureSurveillance(row);
        }

        override protected esEntity CreateEntity()
        {
            return new PpiProcedureSurveillance();
        }

        #endregion

        [BrowsableAttribute(false)]
        public PpiProcedureSurveillanceQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PpiProcedureSurveillanceQuery();
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
        public bool Load(PpiProcedureSurveillanceQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public PpiProcedureSurveillance AddNew()
        {
            PpiProcedureSurveillance entity = base.AddNewEntity() as PpiProcedureSurveillance;

            return entity;
        }
        public PpiProcedureSurveillance FindByPrimaryKey(String bookingNo)
        {
            return base.FindByPrimaryKey(bookingNo) as PpiProcedureSurveillance;
        }

        #region IEnumerable< PpiProcedureSurveillance> Members

        IEnumerator<PpiProcedureSurveillance> IEnumerable<PpiProcedureSurveillance>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as PpiProcedureSurveillance;
            }
        }

        #endregion

        private PpiProcedureSurveillanceQuery query;
    }


    /// <summary>
    /// Encapsulates the 'PpiProcedureSurveillance' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("PpiProcedureSurveillance ({BookingNo})")]
    [Serializable]
    public partial class PpiProcedureSurveillance : esPpiProcedureSurveillance
    {
        public PpiProcedureSurveillance()
        {
        }

        public PpiProcedureSurveillance(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return PpiProcedureSurveillanceMetadata.Meta();
            }
        }

        override protected esPpiProcedureSurveillanceQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PpiProcedureSurveillanceQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public PpiProcedureSurveillanceQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PpiProcedureSurveillanceQuery();
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
        public bool Load(PpiProcedureSurveillanceQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private PpiProcedureSurveillanceQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class PpiProcedureSurveillanceQuery : esPpiProcedureSurveillanceQuery
    {
        public PpiProcedureSurveillanceQuery()
        {

        }

        public PpiProcedureSurveillanceQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "PpiProcedureSurveillanceQuery";
        }
    }

    [Serializable]
    public partial class PpiProcedureSurveillanceMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected PpiProcedureSurveillanceMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(PpiProcedureSurveillanceMetadata.ColumnNames.BookingNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = PpiProcedureSurveillanceMetadata.PropertyNames.BookingNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(PpiProcedureSurveillanceMetadata.ColumnNames.IsRiskFactorAge, 1, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = PpiProcedureSurveillanceMetadata.PropertyNames.IsRiskFactorAge;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiProcedureSurveillanceMetadata.ColumnNames.IsRiskFactorNutrient, 2, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = PpiProcedureSurveillanceMetadata.PropertyNames.IsRiskFactorNutrient;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiProcedureSurveillanceMetadata.ColumnNames.IsRiskFactorObesity, 3, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = PpiProcedureSurveillanceMetadata.PropertyNames.IsRiskFactorObesity;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiProcedureSurveillanceMetadata.ColumnNames.IsDiabetes, 4, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = PpiProcedureSurveillanceMetadata.PropertyNames.IsDiabetes;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiProcedureSurveillanceMetadata.ColumnNames.IsHypertension, 5, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = PpiProcedureSurveillanceMetadata.PropertyNames.IsHypertension;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiProcedureSurveillanceMetadata.ColumnNames.IsHiv, 6, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = PpiProcedureSurveillanceMetadata.PropertyNames.IsHiv;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiProcedureSurveillanceMetadata.ColumnNames.IsHbv, 7, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = PpiProcedureSurveillanceMetadata.PropertyNames.IsHbv;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiProcedureSurveillanceMetadata.ColumnNames.IsHcv, 8, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = PpiProcedureSurveillanceMetadata.PropertyNames.IsHcv;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiProcedureSurveillanceMetadata.ColumnNames.SRProcedureClassification, 9, typeof(System.String), esSystemType.String);
            c.PropertyName = PpiProcedureSurveillanceMetadata.PropertyNames.SRProcedureClassification;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiProcedureSurveillanceMetadata.ColumnNames.SRTypesOfSurgery, 10, typeof(System.String), esSystemType.String);
            c.PropertyName = PpiProcedureSurveillanceMetadata.PropertyNames.SRTypesOfSurgery;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiProcedureSurveillanceMetadata.ColumnNames.SRRiskCategory, 11, typeof(System.String), esSystemType.String);
            c.PropertyName = PpiProcedureSurveillanceMetadata.PropertyNames.SRRiskCategory;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiProcedureSurveillanceMetadata.ColumnNames.SRWoundClassification, 12, typeof(System.String), esSystemType.String);
            c.PropertyName = PpiProcedureSurveillanceMetadata.PropertyNames.SRWoundClassification;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiProcedureSurveillanceMetadata.ColumnNames.SRAsaScore, 13, typeof(System.String), esSystemType.String);
            c.PropertyName = PpiProcedureSurveillanceMetadata.PropertyNames.SRAsaScore;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiProcedureSurveillanceMetadata.ColumnNames.SRTTime, 14, typeof(System.String), esSystemType.String);
            c.PropertyName = PpiProcedureSurveillanceMetadata.PropertyNames.SRTTime;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiProcedureSurveillanceMetadata.ColumnNames.Culturs, 15, typeof(System.String), esSystemType.String);
            c.PropertyName = PpiProcedureSurveillanceMetadata.PropertyNames.Culturs;
            c.CharacterMaxLength = 500;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiProcedureSurveillanceMetadata.ColumnNames.LastUpdateDateTime, 16, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PpiProcedureSurveillanceMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiProcedureSurveillanceMetadata.ColumnNames.LastUpdateByUserID, 17, typeof(System.String), esSystemType.String);
            c.PropertyName = PpiProcedureSurveillanceMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public PpiProcedureSurveillanceMetadata Meta()
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
            public const string BookingNo = "BookingNo";
            public const string IsRiskFactorAge = "IsRiskFactorAge";
            public const string IsRiskFactorNutrient = "IsRiskFactorNutrient";
            public const string IsRiskFactorObesity = "IsRiskFactorObesity";
            public const string IsDiabetes = "IsDiabetes";
            public const string IsHypertension = "IsHypertension";
            public const string IsHiv = "IsHiv";
            public const string IsHbv = "IsHbv";
            public const string IsHcv = "IsHcv";
            public const string SRProcedureClassification = "SRProcedureClassification";
            public const string SRTypesOfSurgery = "SRTypesOfSurgery";
            public const string SRRiskCategory = "SRRiskCategory";
            public const string SRWoundClassification = "SRWoundClassification";
            public const string SRAsaScore = "SRAsaScore";
            public const string SRTTime = "SRTTime";
            public const string Culturs = "Culturs";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string BookingNo = "BookingNo";
            public const string IsRiskFactorAge = "IsRiskFactorAge";
            public const string IsRiskFactorNutrient = "IsRiskFactorNutrient";
            public const string IsRiskFactorObesity = "IsRiskFactorObesity";
            public const string IsDiabetes = "IsDiabetes";
            public const string IsHypertension = "IsHypertension";
            public const string IsHiv = "IsHiv";
            public const string IsHbv = "IsHbv";
            public const string IsHcv = "IsHcv";
            public const string SRProcedureClassification = "SRProcedureClassification";
            public const string SRTypesOfSurgery = "SRTypesOfSurgery";
            public const string SRRiskCategory = "SRRiskCategory";
            public const string SRWoundClassification = "SRWoundClassification";
            public const string SRAsaScore = "SRAsaScore";
            public const string SRTTime = "SRTTime";
            public const string Culturs = "Culturs";
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
            lock (typeof(PpiProcedureSurveillanceMetadata))
            {
                if (PpiProcedureSurveillanceMetadata.mapDelegates == null)
                {
                    PpiProcedureSurveillanceMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (PpiProcedureSurveillanceMetadata.meta == null)
                {
                    PpiProcedureSurveillanceMetadata.meta = new PpiProcedureSurveillanceMetadata();
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

                meta.AddTypeMap("BookingNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsRiskFactorAge", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsRiskFactorNutrient", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsRiskFactorObesity", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsDiabetes", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsHypertension", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsHiv", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsHbv", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsHcv", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("SRProcedureClassification", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRTypesOfSurgery", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRRiskCategory", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRWoundClassification", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRAsaScore", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRTTime", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Culturs", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "PpiProcedureSurveillance";
                meta.Destination = "PpiProcedureSurveillance";
                meta.spInsert = "proc_PpiProcedureSurveillanceInsert";
                meta.spUpdate = "proc_PpiProcedureSurveillanceUpdate";
                meta.spDelete = "proc_PpiProcedureSurveillanceDelete";
                meta.spLoadAll = "proc_PpiProcedureSurveillanceLoadAll";
                meta.spLoadByPrimaryKey = "proc_PpiProcedureSurveillanceLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private PpiProcedureSurveillanceMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
