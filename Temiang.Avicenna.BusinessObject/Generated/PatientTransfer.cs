/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 8/8/2018 7:56:50 PM
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
    abstract public class esPatientTransferCollection : esEntityCollectionWAuditLog
    {
        public esPatientTransferCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "PatientTransferCollection";
        }

        #region Query Logic
        protected void InitQuery(esPatientTransferQuery query)
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
            this.InitQuery(query as esPatientTransferQuery);
        }
        #endregion

        virtual public PatientTransfer DetachEntity(PatientTransfer entity)
        {
            return base.DetachEntity(entity) as PatientTransfer;
        }

        virtual public PatientTransfer AttachEntity(PatientTransfer entity)
        {
            return base.AttachEntity(entity) as PatientTransfer;
        }

        virtual public void Combine(PatientTransferCollection collection)
        {
            base.Combine(collection);
        }

        new public PatientTransfer this[int index]
        {
            get
            {
                return base[index] as PatientTransfer;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(PatientTransfer);
        }
    }

    [Serializable]
    abstract public class esPatientTransfer : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esPatientTransferQuery GetDynamicQuery()
        {
            return null;
        }

        public esPatientTransfer()
        {
        }

        public esPatientTransfer(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String transferNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(transferNo);
            else
                return LoadByPrimaryKeyStoredProcedure(transferNo);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String transferNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(transferNo);
            else
                return LoadByPrimaryKeyStoredProcedure(transferNo);
        }

        private bool LoadByPrimaryKeyDynamic(String transferNo)
        {
            esPatientTransferQuery query = this.GetDynamicQuery();
            query.Where(query.TransferNo == transferNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String transferNo)
        {
            esParameters parms = new esParameters();
            parms.Add("TransferNo", transferNo);
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
                        case "TransferNo": this.str.TransferNo = (string)value; break;
                        case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
                        case "DepartmentID": this.str.DepartmentID = (string)value; break;
                        case "TransactionCode": this.str.TransactionCode = (string)value; break;
                        case "TransferDate": this.str.TransferDate = (string)value; break;
                        case "TransferTime": this.str.TransferTime = (string)value; break;
                        case "FromServiceUnitID": this.str.FromServiceUnitID = (string)value; break;
                        case "FromClassID": this.str.FromClassID = (string)value; break;
                        case "FromRoomID": this.str.FromRoomID = (string)value; break;
                        case "FromBedID": this.str.FromBedID = (string)value; break;
                        case "FromChargeClassID": this.str.FromChargeClassID = (string)value; break;
                        case "FromSpecialtyID": this.str.FromSpecialtyID = (string)value; break;
                        case "ToServiceUnitID": this.str.ToServiceUnitID = (string)value; break;
                        case "ToClassID": this.str.ToClassID = (string)value; break;
                        case "ToRoomID": this.str.ToRoomID = (string)value; break;
                        case "ToBedID": this.str.ToBedID = (string)value; break;
                        case "ToChargeClassID": this.str.ToChargeClassID = (string)value; break;
                        case "ToSpecialtyID": this.str.ToSpecialtyID = (string)value; break;
                        case "IsApprove": this.str.IsApprove = (string)value; break;
                        case "IsVoid": this.str.IsVoid = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "IsRoomInFrom": this.str.IsRoomInFrom = (string)value; break;
                        case "IsRoomInTo": this.str.IsRoomInTo = (string)value; break;
                        case "IsValidated": this.str.IsValidated = (string)value; break;
                        case "ValidatedByUserID": this.str.ValidatedByUserID = (string)value; break;
                        case "ValidatedDateTime": this.str.ValidatedDateTime = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "TransferDate":

                            if (value == null || value is System.DateTime)
                                this.TransferDate = (System.DateTime?)value;
                            break;
                        case "IsApprove":

                            if (value == null || value is System.Boolean)
                                this.IsApprove = (System.Boolean?)value;
                            break;
                        case "IsVoid":

                            if (value == null || value is System.Boolean)
                                this.IsVoid = (System.Boolean?)value;
                            break;
                        case "LastUpdateDateTime":

                            if (value == null || value is System.DateTime)
                                this.LastUpdateDateTime = (System.DateTime?)value;
                            break;
                        case "IsRoomInFrom":

                            if (value == null || value is System.Boolean)
                                this.IsRoomInFrom = (System.Boolean?)value;
                            break;
                        case "IsRoomInTo":

                            if (value == null || value is System.Boolean)
                                this.IsRoomInTo = (System.Boolean?)value;
                            break;
                        case "IsValidated":

                            if (value == null || value is System.Boolean)
                                this.IsValidated = (System.Boolean?)value;
                            break;
                        case "ValidatedDateTime":

                            if (value == null || value is System.DateTime)
                                this.ValidatedDateTime = (System.DateTime?)value;
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
        /// Maps to PatientTransfer.TransferNo
        /// </summary>
        virtual public System.String TransferNo
        {
            get
            {
                return base.GetSystemString(PatientTransferMetadata.ColumnNames.TransferNo);
            }

            set
            {
                base.SetSystemString(PatientTransferMetadata.ColumnNames.TransferNo, value);
            }
        }
        /// <summary>
        /// Maps to PatientTransfer.RegistrationNo
        /// </summary>
        virtual public System.String RegistrationNo
        {
            get
            {
                return base.GetSystemString(PatientTransferMetadata.ColumnNames.RegistrationNo);
            }

            set
            {
                base.SetSystemString(PatientTransferMetadata.ColumnNames.RegistrationNo, value);
            }
        }
        /// <summary>
        /// Maps to PatientTransfer.DepartmentID
        /// </summary>
        virtual public System.String DepartmentID
        {
            get
            {
                return base.GetSystemString(PatientTransferMetadata.ColumnNames.DepartmentID);
            }

            set
            {
                base.SetSystemString(PatientTransferMetadata.ColumnNames.DepartmentID, value);
            }
        }
        /// <summary>
        /// Maps to PatientTransfer.TransactionCode
        /// </summary>
        virtual public System.String TransactionCode
        {
            get
            {
                return base.GetSystemString(PatientTransferMetadata.ColumnNames.TransactionCode);
            }

            set
            {
                base.SetSystemString(PatientTransferMetadata.ColumnNames.TransactionCode, value);
            }
        }
        /// <summary>
        /// Maps to PatientTransfer.TransferDate
        /// </summary>
        virtual public System.DateTime? TransferDate
        {
            get
            {
                return base.GetSystemDateTime(PatientTransferMetadata.ColumnNames.TransferDate);
            }

            set
            {
                base.SetSystemDateTime(PatientTransferMetadata.ColumnNames.TransferDate, value);
            }
        }
        /// <summary>
        /// Maps to PatientTransfer.TransferTime
        /// </summary>
        virtual public System.String TransferTime
        {
            get
            {
                return base.GetSystemString(PatientTransferMetadata.ColumnNames.TransferTime);
            }

            set
            {
                base.SetSystemString(PatientTransferMetadata.ColumnNames.TransferTime, value);
            }
        }
        /// <summary>
        /// Maps to PatientTransfer.FromServiceUnitID
        /// </summary>
        virtual public System.String FromServiceUnitID
        {
            get
            {
                return base.GetSystemString(PatientTransferMetadata.ColumnNames.FromServiceUnitID);
            }

            set
            {
                base.SetSystemString(PatientTransferMetadata.ColumnNames.FromServiceUnitID, value);
            }
        }
        /// <summary>
        /// Maps to PatientTransfer.FromClassID
        /// </summary>
        virtual public System.String FromClassID
        {
            get
            {
                return base.GetSystemString(PatientTransferMetadata.ColumnNames.FromClassID);
            }

            set
            {
                base.SetSystemString(PatientTransferMetadata.ColumnNames.FromClassID, value);
            }
        }
        /// <summary>
        /// Maps to PatientTransfer.FromRoomID
        /// </summary>
        virtual public System.String FromRoomID
        {
            get
            {
                return base.GetSystemString(PatientTransferMetadata.ColumnNames.FromRoomID);
            }

            set
            {
                base.SetSystemString(PatientTransferMetadata.ColumnNames.FromRoomID, value);
            }
        }
        /// <summary>
        /// Maps to PatientTransfer.FromBedID
        /// </summary>
        virtual public System.String FromBedID
        {
            get
            {
                return base.GetSystemString(PatientTransferMetadata.ColumnNames.FromBedID);
            }

            set
            {
                base.SetSystemString(PatientTransferMetadata.ColumnNames.FromBedID, value);
            }
        }
        /// <summary>
        /// Maps to PatientTransfer.FromChargeClassID
        /// </summary>
        virtual public System.String FromChargeClassID
        {
            get
            {
                return base.GetSystemString(PatientTransferMetadata.ColumnNames.FromChargeClassID);
            }

            set
            {
                base.SetSystemString(PatientTransferMetadata.ColumnNames.FromChargeClassID, value);
            }
        }
        /// <summary>
        /// Maps to PatientTransfer.FromSpecialtyID
        /// </summary>
        virtual public System.String FromSpecialtyID
        {
            get
            {
                return base.GetSystemString(PatientTransferMetadata.ColumnNames.FromSpecialtyID);
            }

            set
            {
                base.SetSystemString(PatientTransferMetadata.ColumnNames.FromSpecialtyID, value);
            }
        }
        /// <summary>
        /// Maps to PatientTransfer.ToServiceUnitID
        /// </summary>
        virtual public System.String ToServiceUnitID
        {
            get
            {
                return base.GetSystemString(PatientTransferMetadata.ColumnNames.ToServiceUnitID);
            }

            set
            {
                base.SetSystemString(PatientTransferMetadata.ColumnNames.ToServiceUnitID, value);
            }
        }
        /// <summary>
        /// Maps to PatientTransfer.ToClassID
        /// </summary>
        virtual public System.String ToClassID
        {
            get
            {
                return base.GetSystemString(PatientTransferMetadata.ColumnNames.ToClassID);
            }

            set
            {
                base.SetSystemString(PatientTransferMetadata.ColumnNames.ToClassID, value);
            }
        }
        /// <summary>
        /// Maps to PatientTransfer.ToRoomID
        /// </summary>
        virtual public System.String ToRoomID
        {
            get
            {
                return base.GetSystemString(PatientTransferMetadata.ColumnNames.ToRoomID);
            }

            set
            {
                base.SetSystemString(PatientTransferMetadata.ColumnNames.ToRoomID, value);
            }
        }
        /// <summary>
        /// Maps to PatientTransfer.ToBedID
        /// </summary>
        virtual public System.String ToBedID
        {
            get
            {
                return base.GetSystemString(PatientTransferMetadata.ColumnNames.ToBedID);
            }

            set
            {
                base.SetSystemString(PatientTransferMetadata.ColumnNames.ToBedID, value);
            }
        }
        /// <summary>
        /// Maps to PatientTransfer.ToChargeClassID
        /// </summary>
        virtual public System.String ToChargeClassID
        {
            get
            {
                return base.GetSystemString(PatientTransferMetadata.ColumnNames.ToChargeClassID);
            }

            set
            {
                base.SetSystemString(PatientTransferMetadata.ColumnNames.ToChargeClassID, value);
            }
        }
        /// <summary>
        /// Maps to PatientTransfer.ToSpecialtyID
        /// </summary>
        virtual public System.String ToSpecialtyID
        {
            get
            {
                return base.GetSystemString(PatientTransferMetadata.ColumnNames.ToSpecialtyID);
            }

            set
            {
                base.SetSystemString(PatientTransferMetadata.ColumnNames.ToSpecialtyID, value);
            }
        }
        /// <summary>
        /// Maps to PatientTransfer.IsApprove
        /// </summary>
        virtual public System.Boolean? IsApprove
        {
            get
            {
                return base.GetSystemBoolean(PatientTransferMetadata.ColumnNames.IsApprove);
            }

            set
            {
                base.SetSystemBoolean(PatientTransferMetadata.ColumnNames.IsApprove, value);
            }
        }
        /// <summary>
        /// Maps to PatientTransfer.IsVoid
        /// </summary>
        virtual public System.Boolean? IsVoid
        {
            get
            {
                return base.GetSystemBoolean(PatientTransferMetadata.ColumnNames.IsVoid);
            }

            set
            {
                base.SetSystemBoolean(PatientTransferMetadata.ColumnNames.IsVoid, value);
            }
        }
        /// <summary>
        /// Maps to PatientTransfer.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(PatientTransferMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(PatientTransferMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to PatientTransfer.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(PatientTransferMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(PatientTransferMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to PatientTransfer.IsRoomInFrom
        /// </summary>
        virtual public System.Boolean? IsRoomInFrom
        {
            get
            {
                return base.GetSystemBoolean(PatientTransferMetadata.ColumnNames.IsRoomInFrom);
            }

            set
            {
                base.SetSystemBoolean(PatientTransferMetadata.ColumnNames.IsRoomInFrom, value);
            }
        }
        /// <summary>
        /// Maps to PatientTransfer.IsRoomInTo
        /// </summary>
        virtual public System.Boolean? IsRoomInTo
        {
            get
            {
                return base.GetSystemBoolean(PatientTransferMetadata.ColumnNames.IsRoomInTo);
            }

            set
            {
                base.SetSystemBoolean(PatientTransferMetadata.ColumnNames.IsRoomInTo, value);
            }
        }
        /// <summary>
        /// Maps to PatientTransfer.IsValidated
        /// </summary>
        virtual public System.Boolean? IsValidated
        {
            get
            {
                return base.GetSystemBoolean(PatientTransferMetadata.ColumnNames.IsValidated);
            }

            set
            {
                base.SetSystemBoolean(PatientTransferMetadata.ColumnNames.IsValidated, value);
            }
        }
        /// <summary>
        /// Maps to PatientTransfer.ValidatedByUserID
        /// </summary>
        virtual public System.String ValidatedByUserID
        {
            get
            {
                return base.GetSystemString(PatientTransferMetadata.ColumnNames.ValidatedByUserID);
            }

            set
            {
                base.SetSystemString(PatientTransferMetadata.ColumnNames.ValidatedByUserID, value);
            }
        }
        /// <summary>
        /// Maps to PatientTransfer.ValidatedDateTime
        /// </summary>
        virtual public System.DateTime? ValidatedDateTime
        {
            get
            {
                return base.GetSystemDateTime(PatientTransferMetadata.ColumnNames.ValidatedDateTime);
            }

            set
            {
                base.SetSystemDateTime(PatientTransferMetadata.ColumnNames.ValidatedDateTime, value);
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
            public esStrings(esPatientTransfer entity)
            {
                this.entity = entity;
            }
            public System.String TransferNo
            {
                get
                {
                    System.String data = entity.TransferNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TransferNo = null;
                    else entity.TransferNo = Convert.ToString(value);
                }
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
            public System.String DepartmentID
            {
                get
                {
                    System.String data = entity.DepartmentID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DepartmentID = null;
                    else entity.DepartmentID = Convert.ToString(value);
                }
            }
            public System.String TransactionCode
            {
                get
                {
                    System.String data = entity.TransactionCode;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TransactionCode = null;
                    else entity.TransactionCode = Convert.ToString(value);
                }
            }
            public System.String TransferDate
            {
                get
                {
                    System.DateTime? data = entity.TransferDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TransferDate = null;
                    else entity.TransferDate = Convert.ToDateTime(value);
                }
            }
            public System.String TransferTime
            {
                get
                {
                    System.String data = entity.TransferTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TransferTime = null;
                    else entity.TransferTime = Convert.ToString(value);
                }
            }
            public System.String FromServiceUnitID
            {
                get
                {
                    System.String data = entity.FromServiceUnitID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FromServiceUnitID = null;
                    else entity.FromServiceUnitID = Convert.ToString(value);
                }
            }
            public System.String FromClassID
            {
                get
                {
                    System.String data = entity.FromClassID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FromClassID = null;
                    else entity.FromClassID = Convert.ToString(value);
                }
            }
            public System.String FromRoomID
            {
                get
                {
                    System.String data = entity.FromRoomID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FromRoomID = null;
                    else entity.FromRoomID = Convert.ToString(value);
                }
            }
            public System.String FromBedID
            {
                get
                {
                    System.String data = entity.FromBedID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FromBedID = null;
                    else entity.FromBedID = Convert.ToString(value);
                }
            }
            public System.String FromChargeClassID
            {
                get
                {
                    System.String data = entity.FromChargeClassID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FromChargeClassID = null;
                    else entity.FromChargeClassID = Convert.ToString(value);
                }
            }
            public System.String FromSpecialtyID
            {
                get
                {
                    System.String data = entity.FromSpecialtyID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FromSpecialtyID = null;
                    else entity.FromSpecialtyID = Convert.ToString(value);
                }
            }
            public System.String ToServiceUnitID
            {
                get
                {
                    System.String data = entity.ToServiceUnitID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ToServiceUnitID = null;
                    else entity.ToServiceUnitID = Convert.ToString(value);
                }
            }
            public System.String ToClassID
            {
                get
                {
                    System.String data = entity.ToClassID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ToClassID = null;
                    else entity.ToClassID = Convert.ToString(value);
                }
            }
            public System.String ToRoomID
            {
                get
                {
                    System.String data = entity.ToRoomID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ToRoomID = null;
                    else entity.ToRoomID = Convert.ToString(value);
                }
            }
            public System.String ToBedID
            {
                get
                {
                    System.String data = entity.ToBedID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ToBedID = null;
                    else entity.ToBedID = Convert.ToString(value);
                }
            }
            public System.String ToChargeClassID
            {
                get
                {
                    System.String data = entity.ToChargeClassID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ToChargeClassID = null;
                    else entity.ToChargeClassID = Convert.ToString(value);
                }
            }
            public System.String ToSpecialtyID
            {
                get
                {
                    System.String data = entity.ToSpecialtyID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ToSpecialtyID = null;
                    else entity.ToSpecialtyID = Convert.ToString(value);
                }
            }
            public System.String IsApprove
            {
                get
                {
                    System.Boolean? data = entity.IsApprove;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsApprove = null;
                    else entity.IsApprove = Convert.ToBoolean(value);
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
            public System.String IsRoomInFrom
            {
                get
                {
                    System.Boolean? data = entity.IsRoomInFrom;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsRoomInFrom = null;
                    else entity.IsRoomInFrom = Convert.ToBoolean(value);
                }
            }
            public System.String IsRoomInTo
            {
                get
                {
                    System.Boolean? data = entity.IsRoomInTo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsRoomInTo = null;
                    else entity.IsRoomInTo = Convert.ToBoolean(value);
                }
            }
            public System.String IsValidated
            {
                get
                {
                    System.Boolean? data = entity.IsValidated;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsValidated = null;
                    else entity.IsValidated = Convert.ToBoolean(value);
                }
            }
            public System.String ValidatedByUserID
            {
                get
                {
                    System.String data = entity.ValidatedByUserID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ValidatedByUserID = null;
                    else entity.ValidatedByUserID = Convert.ToString(value);
                }
            }
            public System.String ValidatedDateTime
            {
                get
                {
                    System.DateTime? data = entity.ValidatedDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ValidatedDateTime = null;
                    else entity.ValidatedDateTime = Convert.ToDateTime(value);
                }
            }
            private esPatientTransfer entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esPatientTransferQuery query)
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
                throw new Exception("esPatientTransfer can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class PatientTransfer : esPatientTransfer
    {
    }

    [Serializable]
    abstract public class esPatientTransferQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return PatientTransferMetadata.Meta();
            }
        }

        public esQueryItem TransferNo
        {
            get
            {
                return new esQueryItem(this, PatientTransferMetadata.ColumnNames.TransferNo, esSystemType.String);
            }
        }

        public esQueryItem RegistrationNo
        {
            get
            {
                return new esQueryItem(this, PatientTransferMetadata.ColumnNames.RegistrationNo, esSystemType.String);
            }
        }

        public esQueryItem DepartmentID
        {
            get
            {
                return new esQueryItem(this, PatientTransferMetadata.ColumnNames.DepartmentID, esSystemType.String);
            }
        }

        public esQueryItem TransactionCode
        {
            get
            {
                return new esQueryItem(this, PatientTransferMetadata.ColumnNames.TransactionCode, esSystemType.String);
            }
        }

        public esQueryItem TransferDate
        {
            get
            {
                return new esQueryItem(this, PatientTransferMetadata.ColumnNames.TransferDate, esSystemType.DateTime);
            }
        }

        public esQueryItem TransferTime
        {
            get
            {
                return new esQueryItem(this, PatientTransferMetadata.ColumnNames.TransferTime, esSystemType.String);
            }
        }

        public esQueryItem FromServiceUnitID
        {
            get
            {
                return new esQueryItem(this, PatientTransferMetadata.ColumnNames.FromServiceUnitID, esSystemType.String);
            }
        }

        public esQueryItem FromClassID
        {
            get
            {
                return new esQueryItem(this, PatientTransferMetadata.ColumnNames.FromClassID, esSystemType.String);
            }
        }

        public esQueryItem FromRoomID
        {
            get
            {
                return new esQueryItem(this, PatientTransferMetadata.ColumnNames.FromRoomID, esSystemType.String);
            }
        }

        public esQueryItem FromBedID
        {
            get
            {
                return new esQueryItem(this, PatientTransferMetadata.ColumnNames.FromBedID, esSystemType.String);
            }
        }

        public esQueryItem FromChargeClassID
        {
            get
            {
                return new esQueryItem(this, PatientTransferMetadata.ColumnNames.FromChargeClassID, esSystemType.String);
            }
        }

        public esQueryItem FromSpecialtyID
        {
            get
            {
                return new esQueryItem(this, PatientTransferMetadata.ColumnNames.FromSpecialtyID, esSystemType.String);
            }
        }

        public esQueryItem ToServiceUnitID
        {
            get
            {
                return new esQueryItem(this, PatientTransferMetadata.ColumnNames.ToServiceUnitID, esSystemType.String);
            }
        }

        public esQueryItem ToClassID
        {
            get
            {
                return new esQueryItem(this, PatientTransferMetadata.ColumnNames.ToClassID, esSystemType.String);
            }
        }

        public esQueryItem ToRoomID
        {
            get
            {
                return new esQueryItem(this, PatientTransferMetadata.ColumnNames.ToRoomID, esSystemType.String);
            }
        }

        public esQueryItem ToBedID
        {
            get
            {
                return new esQueryItem(this, PatientTransferMetadata.ColumnNames.ToBedID, esSystemType.String);
            }
        }

        public esQueryItem ToChargeClassID
        {
            get
            {
                return new esQueryItem(this, PatientTransferMetadata.ColumnNames.ToChargeClassID, esSystemType.String);
            }
        }

        public esQueryItem ToSpecialtyID
        {
            get
            {
                return new esQueryItem(this, PatientTransferMetadata.ColumnNames.ToSpecialtyID, esSystemType.String);
            }
        }

        public esQueryItem IsApprove
        {
            get
            {
                return new esQueryItem(this, PatientTransferMetadata.ColumnNames.IsApprove, esSystemType.Boolean);
            }
        }

        public esQueryItem IsVoid
        {
            get
            {
                return new esQueryItem(this, PatientTransferMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, PatientTransferMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, PatientTransferMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem IsRoomInFrom
        {
            get
            {
                return new esQueryItem(this, PatientTransferMetadata.ColumnNames.IsRoomInFrom, esSystemType.Boolean);
            }
        }

        public esQueryItem IsRoomInTo
        {
            get
            {
                return new esQueryItem(this, PatientTransferMetadata.ColumnNames.IsRoomInTo, esSystemType.Boolean);
            }
        }

        public esQueryItem IsValidated
        {
            get
            {
                return new esQueryItem(this, PatientTransferMetadata.ColumnNames.IsValidated, esSystemType.Boolean);
            }
        }

        public esQueryItem ValidatedByUserID
        {
            get
            {
                return new esQueryItem(this, PatientTransferMetadata.ColumnNames.ValidatedByUserID, esSystemType.String);
            }
        }

        public esQueryItem ValidatedDateTime
        {
            get
            {
                return new esQueryItem(this, PatientTransferMetadata.ColumnNames.ValidatedDateTime, esSystemType.DateTime);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("PatientTransferCollection")]
    public partial class PatientTransferCollection : esPatientTransferCollection, IEnumerable<PatientTransfer>
    {
        public PatientTransferCollection()
        {

        }

        public static implicit operator List<PatientTransfer>(PatientTransferCollection coll)
        {
            List<PatientTransfer> list = new List<PatientTransfer>();

            foreach (PatientTransfer emp in coll)
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
                return PatientTransferMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PatientTransferQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new PatientTransfer(row);
        }

        override protected esEntity CreateEntity()
        {
            return new PatientTransfer();
        }

        #endregion

        [BrowsableAttribute(false)]
        public PatientTransferQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PatientTransferQuery();
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
        public bool Load(PatientTransferQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public PatientTransfer AddNew()
        {
            PatientTransfer entity = base.AddNewEntity() as PatientTransfer;

            return entity;
        }
        public PatientTransfer FindByPrimaryKey(String transferNo)
        {
            return base.FindByPrimaryKey(transferNo) as PatientTransfer;
        }

        #region IEnumerable< PatientTransfer> Members

        IEnumerator<PatientTransfer> IEnumerable<PatientTransfer>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as PatientTransfer;
            }
        }

        #endregion

        private PatientTransferQuery query;
    }


    /// <summary>
    /// Encapsulates the 'PatientTransfer' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("PatientTransfer ({TransferNo})")]
    [Serializable]
    public partial class PatientTransfer : esPatientTransfer
    {
        public PatientTransfer()
        {
        }

        public PatientTransfer(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return PatientTransferMetadata.Meta();
            }
        }

        override protected esPatientTransferQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PatientTransferQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public PatientTransferQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PatientTransferQuery();
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
        public bool Load(PatientTransferQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private PatientTransferQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class PatientTransferQuery : esPatientTransferQuery
    {
        public PatientTransferQuery()
        {

        }

        public PatientTransferQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "PatientTransferQuery";
        }
    }

    [Serializable]
    public partial class PatientTransferMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected PatientTransferMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(PatientTransferMetadata.ColumnNames.TransferNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientTransferMetadata.PropertyNames.TransferNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(PatientTransferMetadata.ColumnNames.RegistrationNo, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientTransferMetadata.PropertyNames.RegistrationNo;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(PatientTransferMetadata.ColumnNames.DepartmentID, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientTransferMetadata.PropertyNames.DepartmentID;
            c.CharacterMaxLength = 10;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(PatientTransferMetadata.ColumnNames.TransactionCode, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientTransferMetadata.PropertyNames.TransactionCode;
            c.CharacterMaxLength = 3;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(PatientTransferMetadata.ColumnNames.TransferDate, 4, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PatientTransferMetadata.PropertyNames.TransferDate;
            c.HasDefault = true;
            c.Default = @"(getdate())";
            _columns.Add(c);

            c = new esColumnMetadata(PatientTransferMetadata.ColumnNames.TransferTime, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientTransferMetadata.PropertyNames.TransferTime;
            c.CharacterMaxLength = 5;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(PatientTransferMetadata.ColumnNames.FromServiceUnitID, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientTransferMetadata.PropertyNames.FromServiceUnitID;
            c.CharacterMaxLength = 10;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(PatientTransferMetadata.ColumnNames.FromClassID, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientTransferMetadata.PropertyNames.FromClassID;
            c.CharacterMaxLength = 10;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(PatientTransferMetadata.ColumnNames.FromRoomID, 8, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientTransferMetadata.PropertyNames.FromRoomID;
            c.CharacterMaxLength = 10;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(PatientTransferMetadata.ColumnNames.FromBedID, 9, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientTransferMetadata.PropertyNames.FromBedID;
            c.CharacterMaxLength = 10;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(PatientTransferMetadata.ColumnNames.FromChargeClassID, 10, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientTransferMetadata.PropertyNames.FromChargeClassID;
            c.CharacterMaxLength = 10;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(PatientTransferMetadata.ColumnNames.FromSpecialtyID, 11, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientTransferMetadata.PropertyNames.FromSpecialtyID;
            c.CharacterMaxLength = 10;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(PatientTransferMetadata.ColumnNames.ToServiceUnitID, 12, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientTransferMetadata.PropertyNames.ToServiceUnitID;
            c.CharacterMaxLength = 10;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(PatientTransferMetadata.ColumnNames.ToClassID, 13, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientTransferMetadata.PropertyNames.ToClassID;
            c.CharacterMaxLength = 10;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(PatientTransferMetadata.ColumnNames.ToRoomID, 14, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientTransferMetadata.PropertyNames.ToRoomID;
            c.CharacterMaxLength = 10;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(PatientTransferMetadata.ColumnNames.ToBedID, 15, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientTransferMetadata.PropertyNames.ToBedID;
            c.CharacterMaxLength = 10;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(PatientTransferMetadata.ColumnNames.ToChargeClassID, 16, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientTransferMetadata.PropertyNames.ToChargeClassID;
            c.CharacterMaxLength = 10;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(PatientTransferMetadata.ColumnNames.ToSpecialtyID, 17, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientTransferMetadata.PropertyNames.ToSpecialtyID;
            c.CharacterMaxLength = 10;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(PatientTransferMetadata.ColumnNames.IsApprove, 18, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = PatientTransferMetadata.PropertyNames.IsApprove;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(PatientTransferMetadata.ColumnNames.IsVoid, 19, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = PatientTransferMetadata.PropertyNames.IsVoid;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(PatientTransferMetadata.ColumnNames.LastUpdateDateTime, 20, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PatientTransferMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientTransferMetadata.ColumnNames.LastUpdateByUserID, 21, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientTransferMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 40;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientTransferMetadata.ColumnNames.IsRoomInFrom, 22, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = PatientTransferMetadata.PropertyNames.IsRoomInFrom;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientTransferMetadata.ColumnNames.IsRoomInTo, 23, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = PatientTransferMetadata.PropertyNames.IsRoomInTo;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientTransferMetadata.ColumnNames.IsValidated, 24, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = PatientTransferMetadata.PropertyNames.IsValidated;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientTransferMetadata.ColumnNames.ValidatedByUserID, 25, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientTransferMetadata.PropertyNames.ValidatedByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientTransferMetadata.ColumnNames.ValidatedDateTime, 26, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PatientTransferMetadata.PropertyNames.ValidatedDateTime;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public PatientTransferMetadata Meta()
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
            public const string TransferNo = "TransferNo";
            public const string RegistrationNo = "RegistrationNo";
            public const string DepartmentID = "DepartmentID";
            public const string TransactionCode = "TransactionCode";
            public const string TransferDate = "TransferDate";
            public const string TransferTime = "TransferTime";
            public const string FromServiceUnitID = "FromServiceUnitID";
            public const string FromClassID = "FromClassID";
            public const string FromRoomID = "FromRoomID";
            public const string FromBedID = "FromBedID";
            public const string FromChargeClassID = "FromChargeClassID";
            public const string FromSpecialtyID = "FromSpecialtyID";
            public const string ToServiceUnitID = "ToServiceUnitID";
            public const string ToClassID = "ToClassID";
            public const string ToRoomID = "ToRoomID";
            public const string ToBedID = "ToBedID";
            public const string ToChargeClassID = "ToChargeClassID";
            public const string ToSpecialtyID = "ToSpecialtyID";
            public const string IsApprove = "IsApprove";
            public const string IsVoid = "IsVoid";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string IsRoomInFrom = "IsRoomInFrom";
            public const string IsRoomInTo = "IsRoomInTo";
            public const string IsValidated = "IsValidated";
            public const string ValidatedByUserID = "ValidatedByUserID";
            public const string ValidatedDateTime = "ValidatedDateTime";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string TransferNo = "TransferNo";
            public const string RegistrationNo = "RegistrationNo";
            public const string DepartmentID = "DepartmentID";
            public const string TransactionCode = "TransactionCode";
            public const string TransferDate = "TransferDate";
            public const string TransferTime = "TransferTime";
            public const string FromServiceUnitID = "FromServiceUnitID";
            public const string FromClassID = "FromClassID";
            public const string FromRoomID = "FromRoomID";
            public const string FromBedID = "FromBedID";
            public const string FromChargeClassID = "FromChargeClassID";
            public const string FromSpecialtyID = "FromSpecialtyID";
            public const string ToServiceUnitID = "ToServiceUnitID";
            public const string ToClassID = "ToClassID";
            public const string ToRoomID = "ToRoomID";
            public const string ToBedID = "ToBedID";
            public const string ToChargeClassID = "ToChargeClassID";
            public const string ToSpecialtyID = "ToSpecialtyID";
            public const string IsApprove = "IsApprove";
            public const string IsVoid = "IsVoid";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string IsRoomInFrom = "IsRoomInFrom";
            public const string IsRoomInTo = "IsRoomInTo";
            public const string IsValidated = "IsValidated";
            public const string ValidatedByUserID = "ValidatedByUserID";
            public const string ValidatedDateTime = "ValidatedDateTime";
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
            lock (typeof(PatientTransferMetadata))
            {
                if (PatientTransferMetadata.mapDelegates == null)
                {
                    PatientTransferMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (PatientTransferMetadata.meta == null)
                {
                    PatientTransferMetadata.meta = new PatientTransferMetadata();
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

                meta.AddTypeMap("TransferNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("DepartmentID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("TransactionCode", new esTypeMap("char", "System.String"));
                meta.AddTypeMap("TransferDate", new esTypeMap("smalldatetime", "System.DateTime"));
                meta.AddTypeMap("TransferTime", new esTypeMap("char", "System.String"));
                meta.AddTypeMap("FromServiceUnitID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("FromClassID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("FromRoomID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("FromBedID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("FromChargeClassID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("FromSpecialtyID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ToServiceUnitID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ToClassID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ToRoomID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ToBedID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ToChargeClassID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ToSpecialtyID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsApprove", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsRoomInFrom", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsRoomInTo", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsValidated", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("ValidatedByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ValidatedDateTime", new esTypeMap("datetime", "System.DateTime"));


                meta.Source = "PatientTransfer";
                meta.Destination = "PatientTransfer";
                meta.spInsert = "proc_PatientTransferInsert";
                meta.spUpdate = "proc_PatientTransferUpdate";
                meta.spDelete = "proc_PatientTransferDelete";
                meta.spLoadAll = "proc_PatientTransferLoadAll";
                meta.spLoadByPrimaryKey = "proc_PatientTransferLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private PatientTransferMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
