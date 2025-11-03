/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 07/09/19 8:25:04 AM
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
    abstract public class esTransPrescriptionItemTemplateCollection : esEntityCollectionWAuditLog
    {
        public esTransPrescriptionItemTemplateCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "TransPrescriptionItemTemplateCollection";
        }

        #region Query Logic
        protected void InitQuery(esTransPrescriptionItemTemplateQuery query)
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
            this.InitQuery(query as esTransPrescriptionItemTemplateQuery);
        }
        #endregion

        virtual public TransPrescriptionItemTemplate DetachEntity(TransPrescriptionItemTemplate entity)
        {
            return base.DetachEntity(entity) as TransPrescriptionItemTemplate;
        }

        virtual public TransPrescriptionItemTemplate AttachEntity(TransPrescriptionItemTemplate entity)
        {
            return base.AttachEntity(entity) as TransPrescriptionItemTemplate;
        }

        virtual public void Combine(TransPrescriptionItemTemplateCollection collection)
        {
            base.Combine(collection);
        }

        new public TransPrescriptionItemTemplate this[int index]
        {
            get
            {
                return base[index] as TransPrescriptionItemTemplate;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(TransPrescriptionItemTemplate);
        }
    }

    [Serializable]
    abstract public class esTransPrescriptionItemTemplate : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esTransPrescriptionItemTemplateQuery GetDynamicQuery()
        {
            return null;
        }

        public esTransPrescriptionItemTemplate()
        {
        }

        public esTransPrescriptionItemTemplate(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String templateNo, String sequenceNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(templateNo, sequenceNo);
            else
                return LoadByPrimaryKeyStoredProcedure(templateNo, sequenceNo);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String templateNo, String sequenceNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(templateNo, sequenceNo);
            else
                return LoadByPrimaryKeyStoredProcedure(templateNo, sequenceNo);
        }

        private bool LoadByPrimaryKeyDynamic(String templateNo, String sequenceNo)
        {
            esTransPrescriptionItemTemplateQuery query = this.GetDynamicQuery();
            query.Where(query.TemplateNo == templateNo, query.SequenceNo == sequenceNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String templateNo, String sequenceNo)
        {
            esParameters parms = new esParameters();
            parms.Add("TemplateNo", templateNo);
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
                        case "TemplateNo": this.str.TemplateNo = (string)value; break;
                        case "SequenceNo": this.str.SequenceNo = (string)value; break;
                        case "ParentNo": this.str.ParentNo = (string)value; break;
                        case "IsRFlag": this.str.IsRFlag = (string)value; break;
                        case "IsCompound": this.str.IsCompound = (string)value; break;
                        case "ItemID": this.str.ItemID = (string)value; break;
                        case "SRItemUnit": this.str.SRItemUnit = (string)value; break;
                        case "ItemQtyInString": this.str.ItemQtyInString = (string)value; break;
                        case "SRDosageUnit": this.str.SRDosageUnit = (string)value; break;
                        case "PrescriptionQty": this.str.PrescriptionQty = (string)value; break;
                        case "TakenQty": this.str.TakenQty = (string)value; break;
                        case "ResultQty": this.str.ResultQty = (string)value; break;
                        case "EmbalaceID": this.str.EmbalaceID = (string)value; break;
                        case "EmbalaceAmount": this.str.EmbalaceAmount = (string)value; break;
                        case "Notes": this.str.Notes = (string)value; break;
                        case "SRConsumeMethod": this.str.SRConsumeMethod = (string)value; break;
                        case "DosageQty": this.str.DosageQty = (string)value; break;
                        case "EmbalaceQty": this.str.EmbalaceQty = (string)value; break;
                        case "ConsumeQty": this.str.ConsumeQty = (string)value; break;
                        case "SRConsumeUnit": this.str.SRConsumeUnit = (string)value; break;
                        case "LastCreateDateTime": this.str.LastCreateDateTime = (string)value; break;
                        case "LastCreateUserID": this.str.LastCreateUserID = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "SRMedicationConsume": this.str.SRMedicationConsume = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "IsRFlag":

                            if (value == null || value is System.Boolean)
                                this.IsRFlag = (System.Boolean?)value;
                            break;
                        case "IsCompound":

                            if (value == null || value is System.Boolean)
                                this.IsCompound = (System.Boolean?)value;
                            break;
                        case "PrescriptionQty":

                            if (value == null || value is System.Decimal)
                                this.PrescriptionQty = (System.Decimal?)value;
                            break;
                        case "TakenQty":

                            if (value == null || value is System.Decimal)
                                this.TakenQty = (System.Decimal?)value;
                            break;
                        case "ResultQty":

                            if (value == null || value is System.Decimal)
                                this.ResultQty = (System.Decimal?)value;
                            break;
                        case "EmbalaceAmount":

                            if (value == null || value is System.Decimal)
                                this.EmbalaceAmount = (System.Decimal?)value;
                            break;
                        case "LastCreateDateTime":

                            if (value == null || value is System.DateTime)
                                this.LastCreateDateTime = (System.DateTime?)value;
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
        /// Maps to TransPrescriptionItemTemplate.TemplateNo
        /// </summary>
        virtual public System.String TemplateNo
        {
            get
            {
                return base.GetSystemString(TransPrescriptionItemTemplateMetadata.ColumnNames.TemplateNo);
            }

            set
            {
                base.SetSystemString(TransPrescriptionItemTemplateMetadata.ColumnNames.TemplateNo, value);
            }
        }
        /// <summary>
        /// Maps to TransPrescriptionItemTemplate.SequenceNo
        /// </summary>
        virtual public System.String SequenceNo
        {
            get
            {
                return base.GetSystemString(TransPrescriptionItemTemplateMetadata.ColumnNames.SequenceNo);
            }

            set
            {
                base.SetSystemString(TransPrescriptionItemTemplateMetadata.ColumnNames.SequenceNo, value);
            }
        }
        /// <summary>
        /// Maps to TransPrescriptionItemTemplate.ParentNo
        /// </summary>
        virtual public System.String ParentNo
        {
            get
            {
                return base.GetSystemString(TransPrescriptionItemTemplateMetadata.ColumnNames.ParentNo);
            }

            set
            {
                base.SetSystemString(TransPrescriptionItemTemplateMetadata.ColumnNames.ParentNo, value);
            }
        }
        /// <summary>
        /// Maps to TransPrescriptionItemTemplate.IsRFlag
        /// </summary>
        virtual public System.Boolean? IsRFlag
        {
            get
            {
                return base.GetSystemBoolean(TransPrescriptionItemTemplateMetadata.ColumnNames.IsRFlag);
            }

            set
            {
                base.SetSystemBoolean(TransPrescriptionItemTemplateMetadata.ColumnNames.IsRFlag, value);
            }
        }
        /// <summary>
        /// Maps to TransPrescriptionItemTemplate.IsCompound
        /// </summary>
        virtual public System.Boolean? IsCompound
        {
            get
            {
                return base.GetSystemBoolean(TransPrescriptionItemTemplateMetadata.ColumnNames.IsCompound);
            }

            set
            {
                base.SetSystemBoolean(TransPrescriptionItemTemplateMetadata.ColumnNames.IsCompound, value);
            }
        }
        /// <summary>
        /// Maps to TransPrescriptionItemTemplate.ItemID
        /// </summary>
        virtual public System.String ItemID
        {
            get
            {
                return base.GetSystemString(TransPrescriptionItemTemplateMetadata.ColumnNames.ItemID);
            }

            set
            {
                base.SetSystemString(TransPrescriptionItemTemplateMetadata.ColumnNames.ItemID, value);
            }
        }
        /// <summary>
        /// Maps to TransPrescriptionItemTemplate.SRItemUnit
        /// </summary>
        virtual public System.String SRItemUnit
        {
            get
            {
                return base.GetSystemString(TransPrescriptionItemTemplateMetadata.ColumnNames.SRItemUnit);
            }

            set
            {
                base.SetSystemString(TransPrescriptionItemTemplateMetadata.ColumnNames.SRItemUnit, value);
            }
        }
        /// <summary>
        /// Maps to TransPrescriptionItemTemplate.ItemQtyInString
        /// </summary>
        virtual public System.String ItemQtyInString
        {
            get
            {
                return base.GetSystemString(TransPrescriptionItemTemplateMetadata.ColumnNames.ItemQtyInString);
            }

            set
            {
                base.SetSystemString(TransPrescriptionItemTemplateMetadata.ColumnNames.ItemQtyInString, value);
            }
        }
        /// <summary>
        /// Maps to TransPrescriptionItemTemplate.SRDosageUnit
        /// </summary>
        virtual public System.String SRDosageUnit
        {
            get
            {
                return base.GetSystemString(TransPrescriptionItemTemplateMetadata.ColumnNames.SRDosageUnit);
            }

            set
            {
                base.SetSystemString(TransPrescriptionItemTemplateMetadata.ColumnNames.SRDosageUnit, value);
            }
        }
        /// <summary>
        /// Maps to TransPrescriptionItemTemplate.PrescriptionQty
        /// </summary>
        virtual public System.Decimal? PrescriptionQty
        {
            get
            {
                return base.GetSystemDecimal(TransPrescriptionItemTemplateMetadata.ColumnNames.PrescriptionQty);
            }

            set
            {
                base.SetSystemDecimal(TransPrescriptionItemTemplateMetadata.ColumnNames.PrescriptionQty, value);
            }
        }
        /// <summary>
        /// Maps to TransPrescriptionItemTemplate.TakenQty
        /// </summary>
        virtual public System.Decimal? TakenQty
        {
            get
            {
                return base.GetSystemDecimal(TransPrescriptionItemTemplateMetadata.ColumnNames.TakenQty);
            }

            set
            {
                base.SetSystemDecimal(TransPrescriptionItemTemplateMetadata.ColumnNames.TakenQty, value);
            }
        }
        /// <summary>
        /// Maps to TransPrescriptionItemTemplate.ResultQty
        /// </summary>
        virtual public System.Decimal? ResultQty
        {
            get
            {
                return base.GetSystemDecimal(TransPrescriptionItemTemplateMetadata.ColumnNames.ResultQty);
            }

            set
            {
                base.SetSystemDecimal(TransPrescriptionItemTemplateMetadata.ColumnNames.ResultQty, value);
            }
        }
        /// <summary>
        /// Maps to TransPrescriptionItemTemplate.EmbalaceID
        /// </summary>
        virtual public System.String EmbalaceID
        {
            get
            {
                return base.GetSystemString(TransPrescriptionItemTemplateMetadata.ColumnNames.EmbalaceID);
            }

            set
            {
                base.SetSystemString(TransPrescriptionItemTemplateMetadata.ColumnNames.EmbalaceID, value);
            }
        }
        /// <summary>
        /// Maps to TransPrescriptionItemTemplate.EmbalaceAmount
        /// </summary>
        virtual public System.Decimal? EmbalaceAmount
        {
            get
            {
                return base.GetSystemDecimal(TransPrescriptionItemTemplateMetadata.ColumnNames.EmbalaceAmount);
            }

            set
            {
                base.SetSystemDecimal(TransPrescriptionItemTemplateMetadata.ColumnNames.EmbalaceAmount, value);
            }
        }
        /// <summary>
        /// Maps to TransPrescriptionItemTemplate.Notes
        /// </summary>
        virtual public System.String Notes
        {
            get
            {
                return base.GetSystemString(TransPrescriptionItemTemplateMetadata.ColumnNames.Notes);
            }

            set
            {
                base.SetSystemString(TransPrescriptionItemTemplateMetadata.ColumnNames.Notes, value);
            }
        }
        /// <summary>
        /// Maps to TransPrescriptionItemTemplate.SRConsumeMethod
        /// </summary>
        virtual public System.String SRConsumeMethod
        {
            get
            {
                return base.GetSystemString(TransPrescriptionItemTemplateMetadata.ColumnNames.SRConsumeMethod);
            }

            set
            {
                base.SetSystemString(TransPrescriptionItemTemplateMetadata.ColumnNames.SRConsumeMethod, value);
            }
        }
        /// <summary>
        /// Maps to TransPrescriptionItemTemplate.DosageQty
        /// </summary>
        virtual public System.String DosageQty
        {
            get
            {
                return base.GetSystemString(TransPrescriptionItemTemplateMetadata.ColumnNames.DosageQty);
            }

            set
            {
                base.SetSystemString(TransPrescriptionItemTemplateMetadata.ColumnNames.DosageQty, value);
            }
        }
        /// <summary>
        /// Maps to TransPrescriptionItemTemplate.EmbalaceQty
        /// </summary>
        virtual public System.String EmbalaceQty
        {
            get
            {
                return base.GetSystemString(TransPrescriptionItemTemplateMetadata.ColumnNames.EmbalaceQty);
            }

            set
            {
                base.SetSystemString(TransPrescriptionItemTemplateMetadata.ColumnNames.EmbalaceQty, value);
            }
        }
        /// <summary>
        /// Maps to TransPrescriptionItemTemplate.ConsumeQty
        /// </summary>
        virtual public System.String ConsumeQty
        {
            get
            {
                return base.GetSystemString(TransPrescriptionItemTemplateMetadata.ColumnNames.ConsumeQty);
            }

            set
            {
                base.SetSystemString(TransPrescriptionItemTemplateMetadata.ColumnNames.ConsumeQty, value);
            }
        }
        /// <summary>
        /// Maps to TransPrescriptionItemTemplate.SRConsumeUnit
        /// </summary>
        virtual public System.String SRConsumeUnit
        {
            get
            {
                return base.GetSystemString(TransPrescriptionItemTemplateMetadata.ColumnNames.SRConsumeUnit);
            }

            set
            {
                base.SetSystemString(TransPrescriptionItemTemplateMetadata.ColumnNames.SRConsumeUnit, value);
            }
        }
        /// <summary>
        /// Maps to TransPrescriptionItemTemplate.LastCreateDateTime
        /// </summary>
        virtual public System.DateTime? LastCreateDateTime
        {
            get
            {
                return base.GetSystemDateTime(TransPrescriptionItemTemplateMetadata.ColumnNames.LastCreateDateTime);
            }

            set
            {
                base.SetSystemDateTime(TransPrescriptionItemTemplateMetadata.ColumnNames.LastCreateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to TransPrescriptionItemTemplate.LastCreateUserID
        /// </summary>
        virtual public System.String LastCreateUserID
        {
            get
            {
                return base.GetSystemString(TransPrescriptionItemTemplateMetadata.ColumnNames.LastCreateUserID);
            }

            set
            {
                base.SetSystemString(TransPrescriptionItemTemplateMetadata.ColumnNames.LastCreateUserID, value);
            }
        }
        /// <summary>
        /// Maps to TransPrescriptionItemTemplate.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(TransPrescriptionItemTemplateMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(TransPrescriptionItemTemplateMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to TransPrescriptionItemTemplate.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(TransPrescriptionItemTemplateMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(TransPrescriptionItemTemplateMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to TransPrescriptionItemTemplate.SRMedicationConsume
        /// </summary>
        virtual public System.String SRMedicationConsume
        {
            get
            {
                return base.GetSystemString(TransPrescriptionItemTemplateMetadata.ColumnNames.SRMedicationConsume);
            }

            set
            {
                base.SetSystemString(TransPrescriptionItemTemplateMetadata.ColumnNames.SRMedicationConsume, value);
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
            public esStrings(esTransPrescriptionItemTemplate entity)
            {
                this.entity = entity;
            }
            public System.String TemplateNo
            {
                get
                {
                    System.String data = entity.TemplateNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TemplateNo = null;
                    else entity.TemplateNo = Convert.ToString(value);
                }
            }
            public System.String SequenceNo
            {
                get
                {
                    System.String data = entity.SequenceNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SequenceNo = null;
                    else entity.SequenceNo = Convert.ToString(value);
                }
            }
            public System.String ParentNo
            {
                get
                {
                    System.String data = entity.ParentNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ParentNo = null;
                    else entity.ParentNo = Convert.ToString(value);
                }
            }
            public System.String IsRFlag
            {
                get
                {
                    System.Boolean? data = entity.IsRFlag;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsRFlag = null;
                    else entity.IsRFlag = Convert.ToBoolean(value);
                }
            }
            public System.String IsCompound
            {
                get
                {
                    System.Boolean? data = entity.IsCompound;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsCompound = null;
                    else entity.IsCompound = Convert.ToBoolean(value);
                }
            }
            public System.String ItemID
            {
                get
                {
                    System.String data = entity.ItemID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ItemID = null;
                    else entity.ItemID = Convert.ToString(value);
                }
            }
            public System.String SRItemUnit
            {
                get
                {
                    System.String data = entity.SRItemUnit;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRItemUnit = null;
                    else entity.SRItemUnit = Convert.ToString(value);
                }
            }
            public System.String ItemQtyInString
            {
                get
                {
                    System.String data = entity.ItemQtyInString;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ItemQtyInString = null;
                    else entity.ItemQtyInString = Convert.ToString(value);
                }
            }
            public System.String SRDosageUnit
            {
                get
                {
                    System.String data = entity.SRDosageUnit;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRDosageUnit = null;
                    else entity.SRDosageUnit = Convert.ToString(value);
                }
            }
            public System.String PrescriptionQty
            {
                get
                {
                    System.Decimal? data = entity.PrescriptionQty;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PrescriptionQty = null;
                    else entity.PrescriptionQty = Convert.ToDecimal(value);
                }
            }
            public System.String TakenQty
            {
                get
                {
                    System.Decimal? data = entity.TakenQty;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TakenQty = null;
                    else entity.TakenQty = Convert.ToDecimal(value);
                }
            }
            public System.String ResultQty
            {
                get
                {
                    System.Decimal? data = entity.ResultQty;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ResultQty = null;
                    else entity.ResultQty = Convert.ToDecimal(value);
                }
            }
            public System.String EmbalaceID
            {
                get
                {
                    System.String data = entity.EmbalaceID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.EmbalaceID = null;
                    else entity.EmbalaceID = Convert.ToString(value);
                }
            }
            public System.String EmbalaceAmount
            {
                get
                {
                    System.Decimal? data = entity.EmbalaceAmount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.EmbalaceAmount = null;
                    else entity.EmbalaceAmount = Convert.ToDecimal(value);
                }
            }
            public System.String Notes
            {
                get
                {
                    System.String data = entity.Notes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Notes = null;
                    else entity.Notes = Convert.ToString(value);
                }
            }
            public System.String SRConsumeMethod
            {
                get
                {
                    System.String data = entity.SRConsumeMethod;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRConsumeMethod = null;
                    else entity.SRConsumeMethod = Convert.ToString(value);
                }
            }
            public System.String DosageQty
            {
                get
                {
                    System.String data = entity.DosageQty;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DosageQty = null;
                    else entity.DosageQty = Convert.ToString(value);
                }
            }
            public System.String EmbalaceQty
            {
                get
                {
                    System.String data = entity.EmbalaceQty;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.EmbalaceQty = null;
                    else entity.EmbalaceQty = Convert.ToString(value);
                }
            }
            public System.String ConsumeQty
            {
                get
                {
                    System.String data = entity.ConsumeQty;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ConsumeQty = null;
                    else entity.ConsumeQty = Convert.ToString(value);
                }
            }
            public System.String SRConsumeUnit
            {
                get
                {
                    System.String data = entity.SRConsumeUnit;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRConsumeUnit = null;
                    else entity.SRConsumeUnit = Convert.ToString(value);
                }
            }
            public System.String LastCreateDateTime
            {
                get
                {
                    System.DateTime? data = entity.LastCreateDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.LastCreateDateTime = null;
                    else entity.LastCreateDateTime = Convert.ToDateTime(value);
                }
            }
            public System.String LastCreateUserID
            {
                get
                {
                    System.String data = entity.LastCreateUserID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.LastCreateUserID = null;
                    else entity.LastCreateUserID = Convert.ToString(value);
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
            public System.String SRMedicationConsume
            {
                get
                {
                    System.String data = entity.SRMedicationConsume;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRMedicationConsume = null;
                    else entity.SRMedicationConsume = Convert.ToString(value);
                }
            }
            private esTransPrescriptionItemTemplate entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esTransPrescriptionItemTemplateQuery query)
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
                throw new Exception("esTransPrescriptionItemTemplate can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class TransPrescriptionItemTemplate : esTransPrescriptionItemTemplate
    {
    }

    [Serializable]
    abstract public class esTransPrescriptionItemTemplateQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return TransPrescriptionItemTemplateMetadata.Meta();
            }
        }

        public esQueryItem TemplateNo
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionItemTemplateMetadata.ColumnNames.TemplateNo, esSystemType.String);
            }
        }

        public esQueryItem SequenceNo
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionItemTemplateMetadata.ColumnNames.SequenceNo, esSystemType.String);
            }
        }

        public esQueryItem ParentNo
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionItemTemplateMetadata.ColumnNames.ParentNo, esSystemType.String);
            }
        }

        public esQueryItem IsRFlag
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionItemTemplateMetadata.ColumnNames.IsRFlag, esSystemType.Boolean);
            }
        }

        public esQueryItem IsCompound
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionItemTemplateMetadata.ColumnNames.IsCompound, esSystemType.Boolean);
            }
        }

        public esQueryItem ItemID
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionItemTemplateMetadata.ColumnNames.ItemID, esSystemType.String);
            }
        }

        public esQueryItem SRItemUnit
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionItemTemplateMetadata.ColumnNames.SRItemUnit, esSystemType.String);
            }
        }

        public esQueryItem ItemQtyInString
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionItemTemplateMetadata.ColumnNames.ItemQtyInString, esSystemType.String);
            }
        }

        public esQueryItem SRDosageUnit
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionItemTemplateMetadata.ColumnNames.SRDosageUnit, esSystemType.String);
            }
        }

        public esQueryItem PrescriptionQty
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionItemTemplateMetadata.ColumnNames.PrescriptionQty, esSystemType.Decimal);
            }
        }

        public esQueryItem TakenQty
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionItemTemplateMetadata.ColumnNames.TakenQty, esSystemType.Decimal);
            }
        }

        public esQueryItem ResultQty
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionItemTemplateMetadata.ColumnNames.ResultQty, esSystemType.Decimal);
            }
        }

        public esQueryItem EmbalaceID
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionItemTemplateMetadata.ColumnNames.EmbalaceID, esSystemType.String);
            }
        }

        public esQueryItem EmbalaceAmount
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionItemTemplateMetadata.ColumnNames.EmbalaceAmount, esSystemType.Decimal);
            }
        }

        public esQueryItem Notes
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionItemTemplateMetadata.ColumnNames.Notes, esSystemType.String);
            }
        }

        public esQueryItem SRConsumeMethod
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionItemTemplateMetadata.ColumnNames.SRConsumeMethod, esSystemType.String);
            }
        }

        public esQueryItem DosageQty
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionItemTemplateMetadata.ColumnNames.DosageQty, esSystemType.String);
            }
        }

        public esQueryItem EmbalaceQty
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionItemTemplateMetadata.ColumnNames.EmbalaceQty, esSystemType.String);
            }
        }

        public esQueryItem ConsumeQty
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionItemTemplateMetadata.ColumnNames.ConsumeQty, esSystemType.String);
            }
        }

        public esQueryItem SRConsumeUnit
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionItemTemplateMetadata.ColumnNames.SRConsumeUnit, esSystemType.String);
            }
        }

        public esQueryItem LastCreateDateTime
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionItemTemplateMetadata.ColumnNames.LastCreateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastCreateUserID
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionItemTemplateMetadata.ColumnNames.LastCreateUserID, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionItemTemplateMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionItemTemplateMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem SRMedicationConsume
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionItemTemplateMetadata.ColumnNames.SRMedicationConsume, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("TransPrescriptionItemTemplateCollection")]
    public partial class TransPrescriptionItemTemplateCollection : esTransPrescriptionItemTemplateCollection, IEnumerable<TransPrescriptionItemTemplate>
    {
        public TransPrescriptionItemTemplateCollection()
        {

        }

        public static implicit operator List<TransPrescriptionItemTemplate>(TransPrescriptionItemTemplateCollection coll)
        {
            List<TransPrescriptionItemTemplate> list = new List<TransPrescriptionItemTemplate>();

            foreach (TransPrescriptionItemTemplate emp in coll)
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
                return TransPrescriptionItemTemplateMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new TransPrescriptionItemTemplateQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new TransPrescriptionItemTemplate(row);
        }

        override protected esEntity CreateEntity()
        {
            return new TransPrescriptionItemTemplate();
        }

        #endregion

        [BrowsableAttribute(false)]
        public TransPrescriptionItemTemplateQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new TransPrescriptionItemTemplateQuery();
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
        public bool Load(TransPrescriptionItemTemplateQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public TransPrescriptionItemTemplate AddNew()
        {
            TransPrescriptionItemTemplate entity = base.AddNewEntity() as TransPrescriptionItemTemplate;

            return entity;
        }
        public TransPrescriptionItemTemplate FindByPrimaryKey(String templateNo, String sequenceNo)
        {
            return base.FindByPrimaryKey(templateNo, sequenceNo) as TransPrescriptionItemTemplate;
        }

        #region IEnumerable< TransPrescriptionItemTemplate> Members

        IEnumerator<TransPrescriptionItemTemplate> IEnumerable<TransPrescriptionItemTemplate>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as TransPrescriptionItemTemplate;
            }
        }

        #endregion

        private TransPrescriptionItemTemplateQuery query;
    }


    /// <summary>
    /// Encapsulates the 'TransPrescriptionItemTemplate' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("TransPrescriptionItemTemplate ({TemplateNo, SequenceNo})")]
    [Serializable]
    public partial class TransPrescriptionItemTemplate : esTransPrescriptionItemTemplate
    {
        public TransPrescriptionItemTemplate()
        {
        }

        public TransPrescriptionItemTemplate(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return TransPrescriptionItemTemplateMetadata.Meta();
            }
        }

        override protected esTransPrescriptionItemTemplateQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new TransPrescriptionItemTemplateQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public TransPrescriptionItemTemplateQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new TransPrescriptionItemTemplateQuery();
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
        public bool Load(TransPrescriptionItemTemplateQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private TransPrescriptionItemTemplateQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class TransPrescriptionItemTemplateQuery : esTransPrescriptionItemTemplateQuery
    {
        public TransPrescriptionItemTemplateQuery()
        {

        }

        public TransPrescriptionItemTemplateQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "TransPrescriptionItemTemplateQuery";
        }
    }

    [Serializable]
    public partial class TransPrescriptionItemTemplateMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected TransPrescriptionItemTemplateMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(TransPrescriptionItemTemplateMetadata.ColumnNames.TemplateNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = TransPrescriptionItemTemplateMetadata.PropertyNames.TemplateNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionItemTemplateMetadata.ColumnNames.SequenceNo, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = TransPrescriptionItemTemplateMetadata.PropertyNames.SequenceNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 3;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionItemTemplateMetadata.ColumnNames.ParentNo, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = TransPrescriptionItemTemplateMetadata.PropertyNames.ParentNo;
            c.CharacterMaxLength = 3;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionItemTemplateMetadata.ColumnNames.IsRFlag, 3, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = TransPrescriptionItemTemplateMetadata.PropertyNames.IsRFlag;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionItemTemplateMetadata.ColumnNames.IsCompound, 4, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = TransPrescriptionItemTemplateMetadata.PropertyNames.IsCompound;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionItemTemplateMetadata.ColumnNames.ItemID, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = TransPrescriptionItemTemplateMetadata.PropertyNames.ItemID;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionItemTemplateMetadata.ColumnNames.SRItemUnit, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = TransPrescriptionItemTemplateMetadata.PropertyNames.SRItemUnit;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionItemTemplateMetadata.ColumnNames.ItemQtyInString, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = TransPrescriptionItemTemplateMetadata.PropertyNames.ItemQtyInString;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionItemTemplateMetadata.ColumnNames.SRDosageUnit, 8, typeof(System.String), esSystemType.String);
            c.PropertyName = TransPrescriptionItemTemplateMetadata.PropertyNames.SRDosageUnit;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionItemTemplateMetadata.ColumnNames.PrescriptionQty, 9, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = TransPrescriptionItemTemplateMetadata.PropertyNames.PrescriptionQty;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionItemTemplateMetadata.ColumnNames.TakenQty, 10, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = TransPrescriptionItemTemplateMetadata.PropertyNames.TakenQty;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionItemTemplateMetadata.ColumnNames.ResultQty, 11, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = TransPrescriptionItemTemplateMetadata.PropertyNames.ResultQty;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionItemTemplateMetadata.ColumnNames.EmbalaceID, 12, typeof(System.String), esSystemType.String);
            c.PropertyName = TransPrescriptionItemTemplateMetadata.PropertyNames.EmbalaceID;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionItemTemplateMetadata.ColumnNames.EmbalaceAmount, 13, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = TransPrescriptionItemTemplateMetadata.PropertyNames.EmbalaceAmount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionItemTemplateMetadata.ColumnNames.Notes, 14, typeof(System.String), esSystemType.String);
            c.PropertyName = TransPrescriptionItemTemplateMetadata.PropertyNames.Notes;
            c.CharacterMaxLength = 500;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionItemTemplateMetadata.ColumnNames.SRConsumeMethod, 15, typeof(System.String), esSystemType.String);
            c.PropertyName = TransPrescriptionItemTemplateMetadata.PropertyNames.SRConsumeMethod;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionItemTemplateMetadata.ColumnNames.DosageQty, 16, typeof(System.String), esSystemType.String);
            c.PropertyName = TransPrescriptionItemTemplateMetadata.PropertyNames.DosageQty;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionItemTemplateMetadata.ColumnNames.EmbalaceQty, 17, typeof(System.String), esSystemType.String);
            c.PropertyName = TransPrescriptionItemTemplateMetadata.PropertyNames.EmbalaceQty;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionItemTemplateMetadata.ColumnNames.ConsumeQty, 18, typeof(System.String), esSystemType.String);
            c.PropertyName = TransPrescriptionItemTemplateMetadata.PropertyNames.ConsumeQty;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionItemTemplateMetadata.ColumnNames.SRConsumeUnit, 19, typeof(System.String), esSystemType.String);
            c.PropertyName = TransPrescriptionItemTemplateMetadata.PropertyNames.SRConsumeUnit;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionItemTemplateMetadata.ColumnNames.LastCreateDateTime, 20, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = TransPrescriptionItemTemplateMetadata.PropertyNames.LastCreateDateTime;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionItemTemplateMetadata.ColumnNames.LastCreateUserID, 21, typeof(System.String), esSystemType.String);
            c.PropertyName = TransPrescriptionItemTemplateMetadata.PropertyNames.LastCreateUserID;
            c.CharacterMaxLength = 15;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionItemTemplateMetadata.ColumnNames.LastUpdateDateTime, 22, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = TransPrescriptionItemTemplateMetadata.PropertyNames.LastUpdateDateTime;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionItemTemplateMetadata.ColumnNames.LastUpdateByUserID, 23, typeof(System.String), esSystemType.String);
            c.PropertyName = TransPrescriptionItemTemplateMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionItemTemplateMetadata.ColumnNames.SRMedicationConsume, 24, typeof(System.String), esSystemType.String);
            c.PropertyName = TransPrescriptionItemTemplateMetadata.PropertyNames.SRMedicationConsume;
            c.CharacterMaxLength = 5;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public TransPrescriptionItemTemplateMetadata Meta()
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
            public const string TemplateNo = "TemplateNo";
            public const string SequenceNo = "SequenceNo";
            public const string ParentNo = "ParentNo";
            public const string IsRFlag = "IsRFlag";
            public const string IsCompound = "IsCompound";
            public const string ItemID = "ItemID";
            public const string SRItemUnit = "SRItemUnit";
            public const string ItemQtyInString = "ItemQtyInString";
            public const string SRDosageUnit = "SRDosageUnit";
            public const string PrescriptionQty = "PrescriptionQty";
            public const string TakenQty = "TakenQty";
            public const string ResultQty = "ResultQty";
            public const string EmbalaceID = "EmbalaceID";
            public const string EmbalaceAmount = "EmbalaceAmount";
            public const string Notes = "Notes";
            public const string SRConsumeMethod = "SRConsumeMethod";
            public const string DosageQty = "DosageQty";
            public const string EmbalaceQty = "EmbalaceQty";
            public const string ConsumeQty = "ConsumeQty";
            public const string SRConsumeUnit = "SRConsumeUnit";
            public const string LastCreateDateTime = "LastCreateDateTime";
            public const string LastCreateUserID = "LastCreateUserID";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string SRMedicationConsume = "SRMedicationConsume";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string TemplateNo = "TemplateNo";
            public const string SequenceNo = "SequenceNo";
            public const string ParentNo = "ParentNo";
            public const string IsRFlag = "IsRFlag";
            public const string IsCompound = "IsCompound";
            public const string ItemID = "ItemID";
            public const string SRItemUnit = "SRItemUnit";
            public const string ItemQtyInString = "ItemQtyInString";
            public const string SRDosageUnit = "SRDosageUnit";
            public const string PrescriptionQty = "PrescriptionQty";
            public const string TakenQty = "TakenQty";
            public const string ResultQty = "ResultQty";
            public const string EmbalaceID = "EmbalaceID";
            public const string EmbalaceAmount = "EmbalaceAmount";
            public const string Notes = "Notes";
            public const string SRConsumeMethod = "SRConsumeMethod";
            public const string DosageQty = "DosageQty";
            public const string EmbalaceQty = "EmbalaceQty";
            public const string ConsumeQty = "ConsumeQty";
            public const string SRConsumeUnit = "SRConsumeUnit";
            public const string LastCreateDateTime = "LastCreateDateTime";
            public const string LastCreateUserID = "LastCreateUserID";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string SRMedicationConsume = "SRMedicationConsume";
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
            lock (typeof(TransPrescriptionItemTemplateMetadata))
            {
                if (TransPrescriptionItemTemplateMetadata.mapDelegates == null)
                {
                    TransPrescriptionItemTemplateMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (TransPrescriptionItemTemplateMetadata.meta == null)
                {
                    TransPrescriptionItemTemplateMetadata.meta = new TransPrescriptionItemTemplateMetadata();
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

                meta.AddTypeMap("TemplateNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SequenceNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ParentNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsRFlag", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsCompound", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRItemUnit", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ItemQtyInString", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRDosageUnit", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("PrescriptionQty", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("TakenQty", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("ResultQty", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("EmbalaceID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("EmbalaceAmount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRConsumeMethod", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("DosageQty", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("EmbalaceQty", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ConsumeQty", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRConsumeUnit", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastCreateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastCreateUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRMedicationConsume", new esTypeMap("varchar", "System.String"));


                meta.Source = "TransPrescriptionItemTemplate";
                meta.Destination = "TransPrescriptionItemTemplate";
                meta.spInsert = "proc_TransPrescriptionItemTemplateInsert";
                meta.spUpdate = "proc_TransPrescriptionItemTemplateUpdate";
                meta.spDelete = "proc_TransPrescriptionItemTemplateDelete";
                meta.spLoadAll = "proc_TransPrescriptionItemTemplateLoadAll";
                meta.spLoadByPrimaryKey = "proc_TransPrescriptionItemTemplateLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private TransPrescriptionItemTemplateMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
