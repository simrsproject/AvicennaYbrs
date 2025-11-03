/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/28/2019 9:49:47 AM
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
    abstract public class esPatientTransferHistoryCollection : esEntityCollectionWAuditLog
    {
        public esPatientTransferHistoryCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "PatientTransferHistoryCollection";
        }

        #region Query Logic
        protected void InitQuery(esPatientTransferHistoryQuery query)
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
            this.InitQuery(query as esPatientTransferHistoryQuery);
        }
        #endregion

        virtual public PatientTransferHistory DetachEntity(PatientTransferHistory entity)
        {
            return base.DetachEntity(entity) as PatientTransferHistory;
        }

        virtual public PatientTransferHistory AttachEntity(PatientTransferHistory entity)
        {
            return base.AttachEntity(entity) as PatientTransferHistory;
        }

        virtual public void Combine(PatientTransferHistoryCollection collection)
        {
            base.Combine(collection);
        }

        new public PatientTransferHistory this[int index]
        {
            get
            {
                return base[index] as PatientTransferHistory;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(PatientTransferHistory);
        }
    }

    [Serializable]
    abstract public class esPatientTransferHistory : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esPatientTransferHistoryQuery GetDynamicQuery()
        {
            return null;
        }

        public esPatientTransferHistory()
        {
        }

        public esPatientTransferHistory(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String registrationNo, String transferNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(registrationNo, transferNo);
            else
                return LoadByPrimaryKeyStoredProcedure(registrationNo, transferNo);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String registrationNo, String transferNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(registrationNo, transferNo);
            else
                return LoadByPrimaryKeyStoredProcedure(registrationNo, transferNo);
        }

        private bool LoadByPrimaryKeyDynamic(String registrationNo, String transferNo)
        {
            esPatientTransferHistoryQuery query = this.GetDynamicQuery();
            query.Where(query.RegistrationNo == registrationNo, query.TransferNo == transferNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String registrationNo, String transferNo)
        {
            esParameters parms = new esParameters();
            parms.Add("RegistrationNo", registrationNo);
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
                        case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
                        case "TransferNo": this.str.TransferNo = (string)value; break;
                        case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
                        case "ClassID": this.str.ClassID = (string)value; break;
                        case "RoomID": this.str.RoomID = (string)value; break;
                        case "BedID": this.str.BedID = (string)value; break;
                        case "ChargeClassID": this.str.ChargeClassID = (string)value; break;
                        case "DateOfEntry": this.str.DateOfEntry = (string)value; break;
                        case "TimeOfEntry": this.str.TimeOfEntry = (string)value; break;
                        case "DateOfExit": this.str.DateOfExit = (string)value; break;
                        case "TimeOfExit": this.str.TimeOfExit = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "SmfID": this.str.SmfID = (string)value; break;
                        case "SmfIDBefore": this.str.SmfIDBefore = (string)value; break;
                        case "ArrivedDateTime": this.str.ArrivedDateTime = (string)value; break;
                        case "SRTransferredPatientWith": this.str.SRTransferredPatientWith = (string)value; break;
                        case "TransferredByUserID": this.str.TransferredByUserID = (string)value; break;
                        case "ReceivedByUserID": this.str.ReceivedByUserID = (string)value; break;
                        case "FromServiceUnitID": this.str.FromServiceUnitID = (string)value; break;
                        case "FromClassID": this.str.FromClassID = (string)value; break;
                        case "FromRoomID": this.str.FromRoomID = (string)value; break;
                        case "FromBedID": this.str.FromBedID = (string)value; break;
                        case "FromChargeClassID": this.str.FromChargeClassID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "DateOfEntry":

                            if (value == null || value is System.DateTime)
                                this.DateOfEntry = (System.DateTime?)value;
                            break;
                        case "DateOfExit":

                            if (value == null || value is System.DateTime)
                                this.DateOfExit = (System.DateTime?)value;
                            break;
                        case "LastUpdateDateTime":

                            if (value == null || value is System.DateTime)
                                this.LastUpdateDateTime = (System.DateTime?)value;
                            break;
                        case "ArrivedDateTime":

                            if (value == null || value is System.DateTime)
                                this.ArrivedDateTime = (System.DateTime?)value;
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
        /// Maps to PatientTransferHistory.RegistrationNo
        /// </summary>
        virtual public System.String RegistrationNo
        {
            get
            {
                return base.GetSystemString(PatientTransferHistoryMetadata.ColumnNames.RegistrationNo);
            }

            set
            {
                base.SetSystemString(PatientTransferHistoryMetadata.ColumnNames.RegistrationNo, value);
            }
        }
        /// <summary>
        /// Maps to PatientTransferHistory.TransferNo
        /// </summary>
        virtual public System.String TransferNo
        {
            get
            {
                return base.GetSystemString(PatientTransferHistoryMetadata.ColumnNames.TransferNo);
            }

            set
            {
                base.SetSystemString(PatientTransferHistoryMetadata.ColumnNames.TransferNo, value);
            }
        }
        /// <summary>
        /// Maps to PatientTransferHistory.ServiceUnitID
        /// </summary>
        virtual public System.String ServiceUnitID
        {
            get
            {
                return base.GetSystemString(PatientTransferHistoryMetadata.ColumnNames.ServiceUnitID);
            }

            set
            {
                base.SetSystemString(PatientTransferHistoryMetadata.ColumnNames.ServiceUnitID, value);
            }
        }
        /// <summary>
        /// Maps to PatientTransferHistory.ClassID
        /// </summary>
        virtual public System.String ClassID
        {
            get
            {
                return base.GetSystemString(PatientTransferHistoryMetadata.ColumnNames.ClassID);
            }

            set
            {
                base.SetSystemString(PatientTransferHistoryMetadata.ColumnNames.ClassID, value);
            }
        }
        /// <summary>
        /// Maps to PatientTransferHistory.RoomID
        /// </summary>
        virtual public System.String RoomID
        {
            get
            {
                return base.GetSystemString(PatientTransferHistoryMetadata.ColumnNames.RoomID);
            }

            set
            {
                base.SetSystemString(PatientTransferHistoryMetadata.ColumnNames.RoomID, value);
            }
        }
        /// <summary>
        /// Maps to PatientTransferHistory.BedID
        /// </summary>
        virtual public System.String BedID
        {
            get
            {
                return base.GetSystemString(PatientTransferHistoryMetadata.ColumnNames.BedID);
            }

            set
            {
                base.SetSystemString(PatientTransferHistoryMetadata.ColumnNames.BedID, value);
            }
        }
        /// <summary>
        /// Maps to PatientTransferHistory.ChargeClassID
        /// </summary>
        virtual public System.String ChargeClassID
        {
            get
            {
                return base.GetSystemString(PatientTransferHistoryMetadata.ColumnNames.ChargeClassID);
            }

            set
            {
                base.SetSystemString(PatientTransferHistoryMetadata.ColumnNames.ChargeClassID, value);
            }
        }
        /// <summary>
        /// Maps to PatientTransferHistory.DateOfEntry
        /// </summary>
        virtual public System.DateTime? DateOfEntry
        {
            get
            {
                return base.GetSystemDateTime(PatientTransferHistoryMetadata.ColumnNames.DateOfEntry);
            }

            set
            {
                base.SetSystemDateTime(PatientTransferHistoryMetadata.ColumnNames.DateOfEntry, value);
            }
        }
        /// <summary>
        /// Maps to PatientTransferHistory.TimeOfEntry
        /// </summary>
        virtual public System.String TimeOfEntry
        {
            get
            {
                return base.GetSystemString(PatientTransferHistoryMetadata.ColumnNames.TimeOfEntry);
            }

            set
            {
                base.SetSystemString(PatientTransferHistoryMetadata.ColumnNames.TimeOfEntry, value);
            }
        }
        /// <summary>
        /// Maps to PatientTransferHistory.DateOfExit
        /// </summary>
        virtual public System.DateTime? DateOfExit
        {
            get
            {
                return base.GetSystemDateTime(PatientTransferHistoryMetadata.ColumnNames.DateOfExit);
            }

            set
            {
                base.SetSystemDateTime(PatientTransferHistoryMetadata.ColumnNames.DateOfExit, value);
            }
        }
        /// <summary>
        /// Maps to PatientTransferHistory.TimeOfExit
        /// </summary>
        virtual public System.String TimeOfExit
        {
            get
            {
                return base.GetSystemString(PatientTransferHistoryMetadata.ColumnNames.TimeOfExit);
            }

            set
            {
                base.SetSystemString(PatientTransferHistoryMetadata.ColumnNames.TimeOfExit, value);
            }
        }
        /// <summary>
        /// Maps to PatientTransferHistory.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(PatientTransferHistoryMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(PatientTransferHistoryMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to PatientTransferHistory.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(PatientTransferHistoryMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(PatientTransferHistoryMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to PatientTransferHistory.SmfID
        /// </summary>
        virtual public System.String SmfID
        {
            get
            {
                return base.GetSystemString(PatientTransferHistoryMetadata.ColumnNames.SmfID);
            }

            set
            {
                base.SetSystemString(PatientTransferHistoryMetadata.ColumnNames.SmfID, value);
            }
        }
        /// <summary>
        /// Maps to PatientTransferHistory.SmfIDBefore
        /// </summary>
        virtual public System.String SmfIDBefore
        {
            get
            {
                return base.GetSystemString(PatientTransferHistoryMetadata.ColumnNames.SmfIDBefore);
            }

            set
            {
                base.SetSystemString(PatientTransferHistoryMetadata.ColumnNames.SmfIDBefore, value);
            }
        }
        /// <summary>
        /// Maps to PatientTransferHistory.ArrivedDateTime
        /// </summary>
        virtual public System.DateTime? ArrivedDateTime
        {
            get
            {
                return base.GetSystemDateTime(PatientTransferHistoryMetadata.ColumnNames.ArrivedDateTime);
            }

            set
            {
                base.SetSystemDateTime(PatientTransferHistoryMetadata.ColumnNames.ArrivedDateTime, value);
            }
        }
        /// <summary>
        /// Maps to PatientTransferHistory.SRTransferredPatientWith
        /// </summary>
        virtual public System.String SRTransferredPatientWith
        {
            get
            {
                return base.GetSystemString(PatientTransferHistoryMetadata.ColumnNames.SRTransferredPatientWith);
            }

            set
            {
                base.SetSystemString(PatientTransferHistoryMetadata.ColumnNames.SRTransferredPatientWith, value);
            }
        }
        /// <summary>
        /// Maps to PatientTransferHistory.TransferredByUserID
        /// </summary>
        virtual public System.String TransferredByUserID
        {
            get
            {
                return base.GetSystemString(PatientTransferHistoryMetadata.ColumnNames.TransferredByUserID);
            }

            set
            {
                base.SetSystemString(PatientTransferHistoryMetadata.ColumnNames.TransferredByUserID, value);
            }
        }
        /// <summary>
        /// Maps to PatientTransferHistory.ReceivedByUserID
        /// </summary>
        virtual public System.String ReceivedByUserID
        {
            get
            {
                return base.GetSystemString(PatientTransferHistoryMetadata.ColumnNames.ReceivedByUserID);
            }

            set
            {
                base.SetSystemString(PatientTransferHistoryMetadata.ColumnNames.ReceivedByUserID, value);
            }
        }
        /// <summary>
        /// Maps to PatientTransferHistory.FromServiceUnitID
        /// </summary>
        virtual public System.String FromServiceUnitID
        {
            get
            {
                return base.GetSystemString(PatientTransferHistoryMetadata.ColumnNames.FromServiceUnitID);
            }

            set
            {
                base.SetSystemString(PatientTransferHistoryMetadata.ColumnNames.FromServiceUnitID, value);
            }
        }
        /// <summary>
        /// Maps to PatientTransferHistory.FromClassID
        /// </summary>
        virtual public System.String FromClassID
        {
            get
            {
                return base.GetSystemString(PatientTransferHistoryMetadata.ColumnNames.FromClassID);
            }

            set
            {
                base.SetSystemString(PatientTransferHistoryMetadata.ColumnNames.FromClassID, value);
            }
        }
        /// <summary>
        /// Maps to PatientTransferHistory.FromRoomID
        /// </summary>
        virtual public System.String FromRoomID
        {
            get
            {
                return base.GetSystemString(PatientTransferHistoryMetadata.ColumnNames.FromRoomID);
            }

            set
            {
                base.SetSystemString(PatientTransferHistoryMetadata.ColumnNames.FromRoomID, value);
            }
        }
        /// <summary>
        /// Maps to PatientTransferHistory.FromBedID
        /// </summary>
        virtual public System.String FromBedID
        {
            get
            {
                return base.GetSystemString(PatientTransferHistoryMetadata.ColumnNames.FromBedID);
            }

            set
            {
                base.SetSystemString(PatientTransferHistoryMetadata.ColumnNames.FromBedID, value);
            }
        }
        /// <summary>
        /// Maps to PatientTransferHistory.FromChargeClassID
        /// </summary>
        virtual public System.String FromChargeClassID
        {
            get
            {
                return base.GetSystemString(PatientTransferHistoryMetadata.ColumnNames.FromChargeClassID);
            }

            set
            {
                base.SetSystemString(PatientTransferHistoryMetadata.ColumnNames.FromChargeClassID, value);
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
            public esStrings(esPatientTransferHistory entity)
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
            public System.String ServiceUnitID
            {
                get
                {
                    System.String data = entity.ServiceUnitID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ServiceUnitID = null;
                    else entity.ServiceUnitID = Convert.ToString(value);
                }
            }
            public System.String ClassID
            {
                get
                {
                    System.String data = entity.ClassID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ClassID = null;
                    else entity.ClassID = Convert.ToString(value);
                }
            }
            public System.String RoomID
            {
                get
                {
                    System.String data = entity.RoomID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RoomID = null;
                    else entity.RoomID = Convert.ToString(value);
                }
            }
            public System.String BedID
            {
                get
                {
                    System.String data = entity.BedID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.BedID = null;
                    else entity.BedID = Convert.ToString(value);
                }
            }
            public System.String ChargeClassID
            {
                get
                {
                    System.String data = entity.ChargeClassID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ChargeClassID = null;
                    else entity.ChargeClassID = Convert.ToString(value);
                }
            }
            public System.String DateOfEntry
            {
                get
                {
                    System.DateTime? data = entity.DateOfEntry;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DateOfEntry = null;
                    else entity.DateOfEntry = Convert.ToDateTime(value);
                }
            }
            public System.String TimeOfEntry
            {
                get
                {
                    System.String data = entity.TimeOfEntry;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TimeOfEntry = null;
                    else entity.TimeOfEntry = Convert.ToString(value);
                }
            }
            public System.String DateOfExit
            {
                get
                {
                    System.DateTime? data = entity.DateOfExit;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DateOfExit = null;
                    else entity.DateOfExit = Convert.ToDateTime(value);
                }
            }
            public System.String TimeOfExit
            {
                get
                {
                    System.String data = entity.TimeOfExit;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TimeOfExit = null;
                    else entity.TimeOfExit = Convert.ToString(value);
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
            public System.String SmfID
            {
                get
                {
                    System.String data = entity.SmfID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SmfID = null;
                    else entity.SmfID = Convert.ToString(value);
                }
            }
            public System.String SmfIDBefore
            {
                get
                {
                    System.String data = entity.SmfIDBefore;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SmfIDBefore = null;
                    else entity.SmfIDBefore = Convert.ToString(value);
                }
            }
            public System.String ArrivedDateTime
            {
                get
                {
                    System.DateTime? data = entity.ArrivedDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ArrivedDateTime = null;
                    else entity.ArrivedDateTime = Convert.ToDateTime(value);
                }
            }
            public System.String SRTransferredPatientWith
            {
                get
                {
                    System.String data = entity.SRTransferredPatientWith;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRTransferredPatientWith = null;
                    else entity.SRTransferredPatientWith = Convert.ToString(value);
                }
            }
            public System.String TransferredByUserID
            {
                get
                {
                    System.String data = entity.TransferredByUserID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TransferredByUserID = null;
                    else entity.TransferredByUserID = Convert.ToString(value);
                }
            }
            public System.String ReceivedByUserID
            {
                get
                {
                    System.String data = entity.ReceivedByUserID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ReceivedByUserID = null;
                    else entity.ReceivedByUserID = Convert.ToString(value);
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
            private esPatientTransferHistory entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esPatientTransferHistoryQuery query)
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
                throw new Exception("esPatientTransferHistory can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class PatientTransferHistory : esPatientTransferHistory
    {
    }

    [Serializable]
    abstract public class esPatientTransferHistoryQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return PatientTransferHistoryMetadata.Meta();
            }
        }

        public esQueryItem RegistrationNo
        {
            get
            {
                return new esQueryItem(this, PatientTransferHistoryMetadata.ColumnNames.RegistrationNo, esSystemType.String);
            }
        }

        public esQueryItem TransferNo
        {
            get
            {
                return new esQueryItem(this, PatientTransferHistoryMetadata.ColumnNames.TransferNo, esSystemType.String);
            }
        }

        public esQueryItem ServiceUnitID
        {
            get
            {
                return new esQueryItem(this, PatientTransferHistoryMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
            }
        }

        public esQueryItem ClassID
        {
            get
            {
                return new esQueryItem(this, PatientTransferHistoryMetadata.ColumnNames.ClassID, esSystemType.String);
            }
        }

        public esQueryItem RoomID
        {
            get
            {
                return new esQueryItem(this, PatientTransferHistoryMetadata.ColumnNames.RoomID, esSystemType.String);
            }
        }

        public esQueryItem BedID
        {
            get
            {
                return new esQueryItem(this, PatientTransferHistoryMetadata.ColumnNames.BedID, esSystemType.String);
            }
        }

        public esQueryItem ChargeClassID
        {
            get
            {
                return new esQueryItem(this, PatientTransferHistoryMetadata.ColumnNames.ChargeClassID, esSystemType.String);
            }
        }

        public esQueryItem DateOfEntry
        {
            get
            {
                return new esQueryItem(this, PatientTransferHistoryMetadata.ColumnNames.DateOfEntry, esSystemType.DateTime);
            }
        }

        public esQueryItem TimeOfEntry
        {
            get
            {
                return new esQueryItem(this, PatientTransferHistoryMetadata.ColumnNames.TimeOfEntry, esSystemType.String);
            }
        }

        public esQueryItem DateOfExit
        {
            get
            {
                return new esQueryItem(this, PatientTransferHistoryMetadata.ColumnNames.DateOfExit, esSystemType.DateTime);
            }
        }

        public esQueryItem TimeOfExit
        {
            get
            {
                return new esQueryItem(this, PatientTransferHistoryMetadata.ColumnNames.TimeOfExit, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, PatientTransferHistoryMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, PatientTransferHistoryMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem SmfID
        {
            get
            {
                return new esQueryItem(this, PatientTransferHistoryMetadata.ColumnNames.SmfID, esSystemType.String);
            }
        }

        public esQueryItem SmfIDBefore
        {
            get
            {
                return new esQueryItem(this, PatientTransferHistoryMetadata.ColumnNames.SmfIDBefore, esSystemType.String);
            }
        }

        public esQueryItem ArrivedDateTime
        {
            get
            {
                return new esQueryItem(this, PatientTransferHistoryMetadata.ColumnNames.ArrivedDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem SRTransferredPatientWith
        {
            get
            {
                return new esQueryItem(this, PatientTransferHistoryMetadata.ColumnNames.SRTransferredPatientWith, esSystemType.String);
            }
        }

        public esQueryItem TransferredByUserID
        {
            get
            {
                return new esQueryItem(this, PatientTransferHistoryMetadata.ColumnNames.TransferredByUserID, esSystemType.String);
            }
        }

        public esQueryItem ReceivedByUserID
        {
            get
            {
                return new esQueryItem(this, PatientTransferHistoryMetadata.ColumnNames.ReceivedByUserID, esSystemType.String);
            }
        }

        public esQueryItem FromServiceUnitID
        {
            get
            {
                return new esQueryItem(this, PatientTransferHistoryMetadata.ColumnNames.FromServiceUnitID, esSystemType.String);
            }
        }

        public esQueryItem FromClassID
        {
            get
            {
                return new esQueryItem(this, PatientTransferHistoryMetadata.ColumnNames.FromClassID, esSystemType.String);
            }
        }

        public esQueryItem FromRoomID
        {
            get
            {
                return new esQueryItem(this, PatientTransferHistoryMetadata.ColumnNames.FromRoomID, esSystemType.String);
            }
        }

        public esQueryItem FromBedID
        {
            get
            {
                return new esQueryItem(this, PatientTransferHistoryMetadata.ColumnNames.FromBedID, esSystemType.String);
            }
        }

        public esQueryItem FromChargeClassID
        {
            get
            {
                return new esQueryItem(this, PatientTransferHistoryMetadata.ColumnNames.FromChargeClassID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("PatientTransferHistoryCollection")]
    public partial class PatientTransferHistoryCollection : esPatientTransferHistoryCollection, IEnumerable<PatientTransferHistory>
    {
        public PatientTransferHistoryCollection()
        {

        }

        public static implicit operator List<PatientTransferHistory>(PatientTransferHistoryCollection coll)
        {
            List<PatientTransferHistory> list = new List<PatientTransferHistory>();

            foreach (PatientTransferHistory emp in coll)
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
                return PatientTransferHistoryMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PatientTransferHistoryQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new PatientTransferHistory(row);
        }

        override protected esEntity CreateEntity()
        {
            return new PatientTransferHistory();
        }

        #endregion

        [BrowsableAttribute(false)]
        public PatientTransferHistoryQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PatientTransferHistoryQuery();
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
        public bool Load(PatientTransferHistoryQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public PatientTransferHistory AddNew()
        {
            PatientTransferHistory entity = base.AddNewEntity() as PatientTransferHistory;

            return entity;
        }
        public PatientTransferHistory FindByPrimaryKey(String registrationNo, String transferNo)
        {
            return base.FindByPrimaryKey(registrationNo, transferNo) as PatientTransferHistory;
        }

        #region IEnumerable< PatientTransferHistory> Members

        IEnumerator<PatientTransferHistory> IEnumerable<PatientTransferHistory>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as PatientTransferHistory;
            }
        }

        #endregion

        private PatientTransferHistoryQuery query;
    }


    /// <summary>
    /// Encapsulates the 'PatientTransferHistory' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("PatientTransferHistory ({RegistrationNo, TransferNo})")]
    [Serializable]
    public partial class PatientTransferHistory : esPatientTransferHistory
    {
        public PatientTransferHistory()
        {
        }

        public PatientTransferHistory(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return PatientTransferHistoryMetadata.Meta();
            }
        }

        override protected esPatientTransferHistoryQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PatientTransferHistoryQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public PatientTransferHistoryQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PatientTransferHistoryQuery();
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
        public bool Load(PatientTransferHistoryQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private PatientTransferHistoryQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class PatientTransferHistoryQuery : esPatientTransferHistoryQuery
    {
        public PatientTransferHistoryQuery()
        {

        }

        public PatientTransferHistoryQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "PatientTransferHistoryQuery";
        }
    }

    [Serializable]
    public partial class PatientTransferHistoryMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected PatientTransferHistoryMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(PatientTransferHistoryMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientTransferHistoryMetadata.PropertyNames.RegistrationNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(PatientTransferHistoryMetadata.ColumnNames.TransferNo, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientTransferHistoryMetadata.PropertyNames.TransferNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(PatientTransferHistoryMetadata.ColumnNames.ServiceUnitID, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientTransferHistoryMetadata.PropertyNames.ServiceUnitID;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(PatientTransferHistoryMetadata.ColumnNames.ClassID, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientTransferHistoryMetadata.PropertyNames.ClassID;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(PatientTransferHistoryMetadata.ColumnNames.RoomID, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientTransferHistoryMetadata.PropertyNames.RoomID;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(PatientTransferHistoryMetadata.ColumnNames.BedID, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientTransferHistoryMetadata.PropertyNames.BedID;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(PatientTransferHistoryMetadata.ColumnNames.ChargeClassID, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientTransferHistoryMetadata.PropertyNames.ChargeClassID;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(PatientTransferHistoryMetadata.ColumnNames.DateOfEntry, 7, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PatientTransferHistoryMetadata.PropertyNames.DateOfEntry;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientTransferHistoryMetadata.ColumnNames.TimeOfEntry, 8, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientTransferHistoryMetadata.PropertyNames.TimeOfEntry;
            c.CharacterMaxLength = 5;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientTransferHistoryMetadata.ColumnNames.DateOfExit, 9, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PatientTransferHistoryMetadata.PropertyNames.DateOfExit;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientTransferHistoryMetadata.ColumnNames.TimeOfExit, 10, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientTransferHistoryMetadata.PropertyNames.TimeOfExit;
            c.CharacterMaxLength = 5;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientTransferHistoryMetadata.ColumnNames.LastUpdateDateTime, 11, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PatientTransferHistoryMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientTransferHistoryMetadata.ColumnNames.LastUpdateByUserID, 12, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientTransferHistoryMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientTransferHistoryMetadata.ColumnNames.SmfID, 13, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientTransferHistoryMetadata.PropertyNames.SmfID;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientTransferHistoryMetadata.ColumnNames.SmfIDBefore, 14, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientTransferHistoryMetadata.PropertyNames.SmfIDBefore;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientTransferHistoryMetadata.ColumnNames.ArrivedDateTime, 15, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PatientTransferHistoryMetadata.PropertyNames.ArrivedDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientTransferHistoryMetadata.ColumnNames.SRTransferredPatientWith, 16, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientTransferHistoryMetadata.PropertyNames.SRTransferredPatientWith;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientTransferHistoryMetadata.ColumnNames.TransferredByUserID, 17, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientTransferHistoryMetadata.PropertyNames.TransferredByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientTransferHistoryMetadata.ColumnNames.ReceivedByUserID, 18, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientTransferHistoryMetadata.PropertyNames.ReceivedByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientTransferHistoryMetadata.ColumnNames.FromServiceUnitID, 19, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientTransferHistoryMetadata.PropertyNames.FromServiceUnitID;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientTransferHistoryMetadata.ColumnNames.FromClassID, 20, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientTransferHistoryMetadata.PropertyNames.FromClassID;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientTransferHistoryMetadata.ColumnNames.FromRoomID, 21, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientTransferHistoryMetadata.PropertyNames.FromRoomID;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientTransferHistoryMetadata.ColumnNames.FromBedID, 22, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientTransferHistoryMetadata.PropertyNames.FromBedID;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientTransferHistoryMetadata.ColumnNames.FromChargeClassID, 23, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientTransferHistoryMetadata.PropertyNames.FromChargeClassID;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public PatientTransferHistoryMetadata Meta()
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
            public const string TransferNo = "TransferNo";
            public const string ServiceUnitID = "ServiceUnitID";
            public const string ClassID = "ClassID";
            public const string RoomID = "RoomID";
            public const string BedID = "BedID";
            public const string ChargeClassID = "ChargeClassID";
            public const string DateOfEntry = "DateOfEntry";
            public const string TimeOfEntry = "TimeOfEntry";
            public const string DateOfExit = "DateOfExit";
            public const string TimeOfExit = "TimeOfExit";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string SmfID = "SmfID";
            public const string SmfIDBefore = "SmfIDBefore";
            public const string ArrivedDateTime = "ArrivedDateTime";
            public const string SRTransferredPatientWith = "SRTransferredPatientWith";
            public const string TransferredByUserID = "TransferredByUserID";
            public const string ReceivedByUserID = "ReceivedByUserID";
            public const string FromServiceUnitID = "FromServiceUnitID";
            public const string FromClassID = "FromClassID";
            public const string FromRoomID = "FromRoomID";
            public const string FromBedID = "FromBedID";
            public const string FromChargeClassID = "FromChargeClassID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string RegistrationNo = "RegistrationNo";
            public const string TransferNo = "TransferNo";
            public const string ServiceUnitID = "ServiceUnitID";
            public const string ClassID = "ClassID";
            public const string RoomID = "RoomID";
            public const string BedID = "BedID";
            public const string ChargeClassID = "ChargeClassID";
            public const string DateOfEntry = "DateOfEntry";
            public const string TimeOfEntry = "TimeOfEntry";
            public const string DateOfExit = "DateOfExit";
            public const string TimeOfExit = "TimeOfExit";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string SmfID = "SmfID";
            public const string SmfIDBefore = "SmfIDBefore";
            public const string ArrivedDateTime = "ArrivedDateTime";
            public const string SRTransferredPatientWith = "SRTransferredPatientWith";
            public const string TransferredByUserID = "TransferredByUserID";
            public const string ReceivedByUserID = "ReceivedByUserID";
            public const string FromServiceUnitID = "FromServiceUnitID";
            public const string FromClassID = "FromClassID";
            public const string FromRoomID = "FromRoomID";
            public const string FromBedID = "FromBedID";
            public const string FromChargeClassID = "FromChargeClassID";
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
            lock (typeof(PatientTransferHistoryMetadata))
            {
                if (PatientTransferHistoryMetadata.mapDelegates == null)
                {
                    PatientTransferHistoryMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (PatientTransferHistoryMetadata.meta == null)
                {
                    PatientTransferHistoryMetadata.meta = new PatientTransferHistoryMetadata();
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
                meta.AddTypeMap("TransferNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ClassID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("RoomID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("BedID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ChargeClassID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("DateOfEntry", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("TimeOfEntry", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("DateOfExit", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("TimeOfExit", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SmfID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SmfIDBefore", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ArrivedDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("SRTransferredPatientWith", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("TransferredByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ReceivedByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("FromServiceUnitID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("FromClassID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("FromRoomID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("FromBedID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("FromChargeClassID", new esTypeMap("varchar", "System.String"));


                meta.Source = "PatientTransferHistory";
                meta.Destination = "PatientTransferHistory";
                meta.spInsert = "proc_PatientTransferHistoryInsert";
                meta.spUpdate = "proc_PatientTransferHistoryUpdate";
                meta.spDelete = "proc_PatientTransferHistoryDelete";
                meta.spLoadAll = "proc_PatientTransferHistoryLoadAll";
                meta.spLoadByPrimaryKey = "proc_PatientTransferHistoryLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private PatientTransferHistoryMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
