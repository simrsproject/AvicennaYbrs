/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 3/16/2017 10:18:38 AM
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
    abstract public class esBedManagementCollection : esEntityCollectionWAuditLog
    {
        public esBedManagementCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "BedManagementCollection";
        }

        #region Query Logic
        protected void InitQuery(esBedManagementQuery query)
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
            this.InitQuery(query as esBedManagementQuery);
        }
        #endregion

        virtual public BedManagement DetachEntity(BedManagement entity)
        {
            return base.DetachEntity(entity) as BedManagement;
        }

        virtual public BedManagement AttachEntity(BedManagement entity)
        {
            return base.AttachEntity(entity) as BedManagement;
        }

        virtual public void Combine(BedManagementCollection collection)
        {
            base.Combine(collection);
        }

        new public BedManagement this[int index]
        {
            get
            {
                return base[index] as BedManagement;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(BedManagement);
        }
    }

    [Serializable]
    abstract public class esBedManagement : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esBedManagementQuery GetDynamicQuery()
        {
            return null;
        }

        public esBedManagement()
        {
        }

        public esBedManagement(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(Int64 bedManagementID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(bedManagementID);
            else
                return LoadByPrimaryKeyStoredProcedure(bedManagementID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int64 bedManagementID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(bedManagementID);
            else
                return LoadByPrimaryKeyStoredProcedure(bedManagementID);
        }

        private bool LoadByPrimaryKeyDynamic(Int64 bedManagementID)
        {
            esBedManagementQuery query = this.GetDynamicQuery();
            query.Where(query.BedManagementID == bedManagementID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(Int64 bedManagementID)
        {
            esParameters parms = new esParameters();
            parms.Add("BedManagementID", bedManagementID);
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
                        case "BedManagementID": this.str.BedManagementID = (string)value; break;
                        case "BedID": this.str.BedID = (string)value; break;
                        case "TransactionDate": this.str.TransactionDate = (string)value; break;
                        case "PatientID": this.str.PatientID = (string)value; break;
                        case "FirstName": this.str.FirstName = (string)value; break;
                        case "MiddleName": this.str.MiddleName = (string)value; break;
                        case "LastName": this.str.LastName = (string)value; break;
                        case "StreetName": this.str.StreetName = (string)value; break;
                        case "District": this.str.District = (string)value; break;
                        case "City": this.str.City = (string)value; break;
                        case "County": this.str.County = (string)value; break;
                        case "State": this.str.State = (string)value; break;
                        case "ZipCode": this.str.ZipCode = (string)value; break;
                        case "PhoneNo": this.str.PhoneNo = (string)value; break;
                        case "MobilePhoneNo": this.str.MobilePhoneNo = (string)value; break;
                        case "FaxNo": this.str.FaxNo = (string)value; break;
                        case "Email": this.str.Email = (string)value; break;
                        case "ReservationNo": this.str.ReservationNo = (string)value; break;
                        case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
                        case "RegistrationBedID": this.str.RegistrationBedID = (string)value; break;
                        case "SRBedStatus": this.str.SRBedStatus = (string)value; break;
                        case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;
                        case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;
                        case "IsReleased": this.str.IsReleased = (string)value; break;
                        case "ReleasedDateTime": this.str.ReleasedDateTime = (string)value; break;
                        case "ReleasedByUserID": this.str.ReleasedByUserID = (string)value; break;
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
                        case "BedManagementID":

                            if (value == null || value is System.Int64)
                                this.BedManagementID = (System.Int64?)value;
                            break;
                        case "TransactionDate":

                            if (value == null || value is System.DateTime)
                                this.TransactionDate = (System.DateTime?)value;
                            break;
                        case "CreatedDateTime":

                            if (value == null || value is System.DateTime)
                                this.CreatedDateTime = (System.DateTime?)value;
                            break;
                        case "IsReleased":

                            if (value == null || value is System.Boolean)
                                this.IsReleased = (System.Boolean?)value;
                            break;
                        case "ReleasedDateTime":

                            if (value == null || value is System.DateTime)
                                this.ReleasedDateTime = (System.DateTime?)value;
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
        /// Maps to BedManagement.BedManagementID
        /// </summary>
        virtual public System.Int64? BedManagementID
        {
            get
            {
                return base.GetSystemInt64(BedManagementMetadata.ColumnNames.BedManagementID);
            }

            set
            {
                base.SetSystemInt64(BedManagementMetadata.ColumnNames.BedManagementID, value);
            }
        }
        /// <summary>
        /// Maps to BedManagement.BedID
        /// </summary>
        virtual public System.String BedID
        {
            get
            {
                return base.GetSystemString(BedManagementMetadata.ColumnNames.BedID);
            }

            set
            {
                base.SetSystemString(BedManagementMetadata.ColumnNames.BedID, value);
            }
        }
        /// <summary>
        /// Maps to BedManagement.TransactionDate
        /// </summary>
        virtual public System.DateTime? TransactionDate
        {
            get
            {
                return base.GetSystemDateTime(BedManagementMetadata.ColumnNames.TransactionDate);
            }

            set
            {
                base.SetSystemDateTime(BedManagementMetadata.ColumnNames.TransactionDate, value);
            }
        }
        /// <summary>
        /// Maps to BedManagement.PatientID
        /// </summary>
        virtual public System.String PatientID
        {
            get
            {
                return base.GetSystemString(BedManagementMetadata.ColumnNames.PatientID);
            }

            set
            {
                base.SetSystemString(BedManagementMetadata.ColumnNames.PatientID, value);
            }
        }
        /// <summary>
        /// Maps to BedManagement.FirstName
        /// </summary>
        virtual public System.String FirstName
        {
            get
            {
                return base.GetSystemString(BedManagementMetadata.ColumnNames.FirstName);
            }

            set
            {
                base.SetSystemString(BedManagementMetadata.ColumnNames.FirstName, value);
            }
        }
        /// <summary>
        /// Maps to BedManagement.MiddleName
        /// </summary>
        virtual public System.String MiddleName
        {
            get
            {
                return base.GetSystemString(BedManagementMetadata.ColumnNames.MiddleName);
            }

            set
            {
                base.SetSystemString(BedManagementMetadata.ColumnNames.MiddleName, value);
            }
        }
        /// <summary>
        /// Maps to BedManagement.LastName
        /// </summary>
        virtual public System.String LastName
        {
            get
            {
                return base.GetSystemString(BedManagementMetadata.ColumnNames.LastName);
            }

            set
            {
                base.SetSystemString(BedManagementMetadata.ColumnNames.LastName, value);
            }
        }
        /// <summary>
        /// Maps to BedManagement.StreetName
        /// </summary>
        virtual public System.String StreetName
        {
            get
            {
                return base.GetSystemString(BedManagementMetadata.ColumnNames.StreetName);
            }

            set
            {
                base.SetSystemString(BedManagementMetadata.ColumnNames.StreetName, value);
            }
        }
        /// <summary>
        /// Maps to BedManagement.District
        /// </summary>
        virtual public System.String District
        {
            get
            {
                return base.GetSystemString(BedManagementMetadata.ColumnNames.District);
            }

            set
            {
                base.SetSystemString(BedManagementMetadata.ColumnNames.District, value);
            }
        }
        /// <summary>
        /// Maps to BedManagement.City
        /// </summary>
        virtual public System.String City
        {
            get
            {
                return base.GetSystemString(BedManagementMetadata.ColumnNames.City);
            }

            set
            {
                base.SetSystemString(BedManagementMetadata.ColumnNames.City, value);
            }
        }
        /// <summary>
        /// Maps to BedManagement.County
        /// </summary>
        virtual public System.String County
        {
            get
            {
                return base.GetSystemString(BedManagementMetadata.ColumnNames.County);
            }

            set
            {
                base.SetSystemString(BedManagementMetadata.ColumnNames.County, value);
            }
        }
        /// <summary>
        /// Maps to BedManagement.State
        /// </summary>
        virtual public System.String State
        {
            get
            {
                return base.GetSystemString(BedManagementMetadata.ColumnNames.State);
            }

            set
            {
                base.SetSystemString(BedManagementMetadata.ColumnNames.State, value);
            }
        }
        /// <summary>
        /// Maps to BedManagement.ZipCode
        /// </summary>
        virtual public System.String ZipCode
        {
            get
            {
                return base.GetSystemString(BedManagementMetadata.ColumnNames.ZipCode);
            }

            set
            {
                base.SetSystemString(BedManagementMetadata.ColumnNames.ZipCode, value);
            }
        }
        /// <summary>
        /// Maps to BedManagement.PhoneNo
        /// </summary>
        virtual public System.String PhoneNo
        {
            get
            {
                return base.GetSystemString(BedManagementMetadata.ColumnNames.PhoneNo);
            }

            set
            {
                base.SetSystemString(BedManagementMetadata.ColumnNames.PhoneNo, value);
            }
        }
        /// <summary>
        /// Maps to BedManagement.MobilePhoneNo
        /// </summary>
        virtual public System.String MobilePhoneNo
        {
            get
            {
                return base.GetSystemString(BedManagementMetadata.ColumnNames.MobilePhoneNo);
            }

            set
            {
                base.SetSystemString(BedManagementMetadata.ColumnNames.MobilePhoneNo, value);
            }
        }
        /// <summary>
        /// Maps to BedManagement.FaxNo
        /// </summary>
        virtual public System.String FaxNo
        {
            get
            {
                return base.GetSystemString(BedManagementMetadata.ColumnNames.FaxNo);
            }

            set
            {
                base.SetSystemString(BedManagementMetadata.ColumnNames.FaxNo, value);
            }
        }
        /// <summary>
        /// Maps to BedManagement.Email
        /// </summary>
        virtual public System.String Email
        {
            get
            {
                return base.GetSystemString(BedManagementMetadata.ColumnNames.Email);
            }

            set
            {
                base.SetSystemString(BedManagementMetadata.ColumnNames.Email, value);
            }
        }
        /// <summary>
        /// Maps to BedManagement.ReservationNo
        /// </summary>
        virtual public System.String ReservationNo
        {
            get
            {
                return base.GetSystemString(BedManagementMetadata.ColumnNames.ReservationNo);
            }

            set
            {
                base.SetSystemString(BedManagementMetadata.ColumnNames.ReservationNo, value);
            }
        }
        /// <summary>
        /// Maps to BedManagement.RegistrationNo
        /// </summary>
        virtual public System.String RegistrationNo
        {
            get
            {
                return base.GetSystemString(BedManagementMetadata.ColumnNames.RegistrationNo);
            }

            set
            {
                base.SetSystemString(BedManagementMetadata.ColumnNames.RegistrationNo, value);
            }
        }
        /// <summary>
        /// Maps to BedManagement.RegistrationBedID
        /// </summary>
        virtual public System.String RegistrationBedID
        {
            get
            {
                return base.GetSystemString(BedManagementMetadata.ColumnNames.RegistrationBedID);
            }

            set
            {
                base.SetSystemString(BedManagementMetadata.ColumnNames.RegistrationBedID, value);
            }
        }
        /// <summary>
        /// Maps to BedManagement.SRBedStatus
        /// </summary>
        virtual public System.String SRBedStatus
        {
            get
            {
                return base.GetSystemString(BedManagementMetadata.ColumnNames.SRBedStatus);
            }

            set
            {
                base.SetSystemString(BedManagementMetadata.ColumnNames.SRBedStatus, value);
            }
        }
        /// <summary>
        /// Maps to BedManagement.CreatedDateTime
        /// </summary>
        virtual public System.DateTime? CreatedDateTime
        {
            get
            {
                return base.GetSystemDateTime(BedManagementMetadata.ColumnNames.CreatedDateTime);
            }

            set
            {
                base.SetSystemDateTime(BedManagementMetadata.ColumnNames.CreatedDateTime, value);
            }
        }
        /// <summary>
        /// Maps to BedManagement.CreatedByUserID
        /// </summary>
        virtual public System.String CreatedByUserID
        {
            get
            {
                return base.GetSystemString(BedManagementMetadata.ColumnNames.CreatedByUserID);
            }

            set
            {
                base.SetSystemString(BedManagementMetadata.ColumnNames.CreatedByUserID, value);
            }
        }
        /// <summary>
        /// Maps to BedManagement.IsReleased
        /// </summary>
        virtual public System.Boolean? IsReleased
        {
            get
            {
                return base.GetSystemBoolean(BedManagementMetadata.ColumnNames.IsReleased);
            }

            set
            {
                base.SetSystemBoolean(BedManagementMetadata.ColumnNames.IsReleased, value);
            }
        }
        /// <summary>
        /// Maps to BedManagement.ReleasedDateTime
        /// </summary>
        virtual public System.DateTime? ReleasedDateTime
        {
            get
            {
                return base.GetSystemDateTime(BedManagementMetadata.ColumnNames.ReleasedDateTime);
            }

            set
            {
                base.SetSystemDateTime(BedManagementMetadata.ColumnNames.ReleasedDateTime, value);
            }
        }
        /// <summary>
        /// Maps to BedManagement.ReleasedByUserID
        /// </summary>
        virtual public System.String ReleasedByUserID
        {
            get
            {
                return base.GetSystemString(BedManagementMetadata.ColumnNames.ReleasedByUserID);
            }

            set
            {
                base.SetSystemString(BedManagementMetadata.ColumnNames.ReleasedByUserID, value);
            }
        }
        /// <summary>
        /// Maps to BedManagement.IsVoid
        /// </summary>
        virtual public System.Boolean? IsVoid
        {
            get
            {
                return base.GetSystemBoolean(BedManagementMetadata.ColumnNames.IsVoid);
            }

            set
            {
                base.SetSystemBoolean(BedManagementMetadata.ColumnNames.IsVoid, value);
            }
        }
        /// <summary>
        /// Maps to BedManagement.VoidDateTime
        /// </summary>
        virtual public System.DateTime? VoidDateTime
        {
            get
            {
                return base.GetSystemDateTime(BedManagementMetadata.ColumnNames.VoidDateTime);
            }

            set
            {
                base.SetSystemDateTime(BedManagementMetadata.ColumnNames.VoidDateTime, value);
            }
        }
        /// <summary>
        /// Maps to BedManagement.VoidByUserID
        /// </summary>
        virtual public System.String VoidByUserID
        {
            get
            {
                return base.GetSystemString(BedManagementMetadata.ColumnNames.VoidByUserID);
            }

            set
            {
                base.SetSystemString(BedManagementMetadata.ColumnNames.VoidByUserID, value);
            }
        }
        /// <summary>
        /// Maps to BedManagement.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(BedManagementMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(BedManagementMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to BedManagement.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(BedManagementMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(BedManagementMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esBedManagement entity)
            {
                this.entity = entity;
            }
            public System.String BedManagementID
            {
                get
                {
                    System.Int64? data = entity.BedManagementID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.BedManagementID = null;
                    else entity.BedManagementID = Convert.ToInt64(value);
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
            public System.String TransactionDate
            {
                get
                {
                    System.DateTime? data = entity.TransactionDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TransactionDate = null;
                    else entity.TransactionDate = Convert.ToDateTime(value);
                }
            }
            public System.String PatientID
            {
                get
                {
                    System.String data = entity.PatientID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PatientID = null;
                    else entity.PatientID = Convert.ToString(value);
                }
            }
            public System.String FirstName
            {
                get
                {
                    System.String data = entity.FirstName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FirstName = null;
                    else entity.FirstName = Convert.ToString(value);
                }
            }
            public System.String MiddleName
            {
                get
                {
                    System.String data = entity.MiddleName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.MiddleName = null;
                    else entity.MiddleName = Convert.ToString(value);
                }
            }
            public System.String LastName
            {
                get
                {
                    System.String data = entity.LastName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.LastName = null;
                    else entity.LastName = Convert.ToString(value);
                }
            }
            public System.String StreetName
            {
                get
                {
                    System.String data = entity.StreetName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.StreetName = null;
                    else entity.StreetName = Convert.ToString(value);
                }
            }
            public System.String District
            {
                get
                {
                    System.String data = entity.District;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.District = null;
                    else entity.District = Convert.ToString(value);
                }
            }
            public System.String City
            {
                get
                {
                    System.String data = entity.City;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.City = null;
                    else entity.City = Convert.ToString(value);
                }
            }
            public System.String County
            {
                get
                {
                    System.String data = entity.County;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.County = null;
                    else entity.County = Convert.ToString(value);
                }
            }
            public System.String State
            {
                get
                {
                    System.String data = entity.State;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.State = null;
                    else entity.State = Convert.ToString(value);
                }
            }
            public System.String ZipCode
            {
                get
                {
                    System.String data = entity.ZipCode;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ZipCode = null;
                    else entity.ZipCode = Convert.ToString(value);
                }
            }
            public System.String PhoneNo
            {
                get
                {
                    System.String data = entity.PhoneNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PhoneNo = null;
                    else entity.PhoneNo = Convert.ToString(value);
                }
            }
            public System.String MobilePhoneNo
            {
                get
                {
                    System.String data = entity.MobilePhoneNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.MobilePhoneNo = null;
                    else entity.MobilePhoneNo = Convert.ToString(value);
                }
            }
            public System.String FaxNo
            {
                get
                {
                    System.String data = entity.FaxNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FaxNo = null;
                    else entity.FaxNo = Convert.ToString(value);
                }
            }
            public System.String Email
            {
                get
                {
                    System.String data = entity.Email;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Email = null;
                    else entity.Email = Convert.ToString(value);
                }
            }
            public System.String ReservationNo
            {
                get
                {
                    System.String data = entity.ReservationNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ReservationNo = null;
                    else entity.ReservationNo = Convert.ToString(value);
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
            public System.String RegistrationBedID
            {
                get
                {
                    System.String data = entity.RegistrationBedID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RegistrationBedID = null;
                    else entity.RegistrationBedID = Convert.ToString(value);
                }
            }
            public System.String SRBedStatus
            {
                get
                {
                    System.String data = entity.SRBedStatus;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRBedStatus = null;
                    else entity.SRBedStatus = Convert.ToString(value);
                }
            }
            public System.String CreatedDateTime
            {
                get
                {
                    System.DateTime? data = entity.CreatedDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CreatedDateTime = null;
                    else entity.CreatedDateTime = Convert.ToDateTime(value);
                }
            }
            public System.String CreatedByUserID
            {
                get
                {
                    System.String data = entity.CreatedByUserID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CreatedByUserID = null;
                    else entity.CreatedByUserID = Convert.ToString(value);
                }
            }
            public System.String IsReleased
            {
                get
                {
                    System.Boolean? data = entity.IsReleased;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsReleased = null;
                    else entity.IsReleased = Convert.ToBoolean(value);
                }
            }
            public System.String ReleasedDateTime
            {
                get
                {
                    System.DateTime? data = entity.ReleasedDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ReleasedDateTime = null;
                    else entity.ReleasedDateTime = Convert.ToDateTime(value);
                }
            }
            public System.String ReleasedByUserID
            {
                get
                {
                    System.String data = entity.ReleasedByUserID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ReleasedByUserID = null;
                    else entity.ReleasedByUserID = Convert.ToString(value);
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
            private esBedManagement entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esBedManagementQuery query)
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
                throw new Exception("esBedManagement can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class BedManagement : esBedManagement
    {
    }

    [Serializable]
    abstract public class esBedManagementQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return BedManagementMetadata.Meta();
            }
        }

        public esQueryItem BedManagementID
        {
            get
            {
                return new esQueryItem(this, BedManagementMetadata.ColumnNames.BedManagementID, esSystemType.Int64);
            }
        }

        public esQueryItem BedID
        {
            get
            {
                return new esQueryItem(this, BedManagementMetadata.ColumnNames.BedID, esSystemType.String);
            }
        }

        public esQueryItem TransactionDate
        {
            get
            {
                return new esQueryItem(this, BedManagementMetadata.ColumnNames.TransactionDate, esSystemType.DateTime);
            }
        }

        public esQueryItem PatientID
        {
            get
            {
                return new esQueryItem(this, BedManagementMetadata.ColumnNames.PatientID, esSystemType.String);
            }
        }

        public esQueryItem FirstName
        {
            get
            {
                return new esQueryItem(this, BedManagementMetadata.ColumnNames.FirstName, esSystemType.String);
            }
        }

        public esQueryItem MiddleName
        {
            get
            {
                return new esQueryItem(this, BedManagementMetadata.ColumnNames.MiddleName, esSystemType.String);
            }
        }

        public esQueryItem LastName
        {
            get
            {
                return new esQueryItem(this, BedManagementMetadata.ColumnNames.LastName, esSystemType.String);
            }
        }

        public esQueryItem StreetName
        {
            get
            {
                return new esQueryItem(this, BedManagementMetadata.ColumnNames.StreetName, esSystemType.String);
            }
        }

        public esQueryItem District
        {
            get
            {
                return new esQueryItem(this, BedManagementMetadata.ColumnNames.District, esSystemType.String);
            }
        }

        public esQueryItem City
        {
            get
            {
                return new esQueryItem(this, BedManagementMetadata.ColumnNames.City, esSystemType.String);
            }
        }

        public esQueryItem County
        {
            get
            {
                return new esQueryItem(this, BedManagementMetadata.ColumnNames.County, esSystemType.String);
            }
        }

        public esQueryItem State
        {
            get
            {
                return new esQueryItem(this, BedManagementMetadata.ColumnNames.State, esSystemType.String);
            }
        }

        public esQueryItem ZipCode
        {
            get
            {
                return new esQueryItem(this, BedManagementMetadata.ColumnNames.ZipCode, esSystemType.String);
            }
        }

        public esQueryItem PhoneNo
        {
            get
            {
                return new esQueryItem(this, BedManagementMetadata.ColumnNames.PhoneNo, esSystemType.String);
            }
        }

        public esQueryItem MobilePhoneNo
        {
            get
            {
                return new esQueryItem(this, BedManagementMetadata.ColumnNames.MobilePhoneNo, esSystemType.String);
            }
        }

        public esQueryItem FaxNo
        {
            get
            {
                return new esQueryItem(this, BedManagementMetadata.ColumnNames.FaxNo, esSystemType.String);
            }
        }

        public esQueryItem Email
        {
            get
            {
                return new esQueryItem(this, BedManagementMetadata.ColumnNames.Email, esSystemType.String);
            }
        }

        public esQueryItem ReservationNo
        {
            get
            {
                return new esQueryItem(this, BedManagementMetadata.ColumnNames.ReservationNo, esSystemType.String);
            }
        }

        public esQueryItem RegistrationNo
        {
            get
            {
                return new esQueryItem(this, BedManagementMetadata.ColumnNames.RegistrationNo, esSystemType.String);
            }
        }

        public esQueryItem RegistrationBedID
        {
            get
            {
                return new esQueryItem(this, BedManagementMetadata.ColumnNames.RegistrationBedID, esSystemType.String);
            }
        }

        public esQueryItem SRBedStatus
        {
            get
            {
                return new esQueryItem(this, BedManagementMetadata.ColumnNames.SRBedStatus, esSystemType.String);
            }
        }

        public esQueryItem CreatedDateTime
        {
            get
            {
                return new esQueryItem(this, BedManagementMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem CreatedByUserID
        {
            get
            {
                return new esQueryItem(this, BedManagementMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
            }
        }

        public esQueryItem IsReleased
        {
            get
            {
                return new esQueryItem(this, BedManagementMetadata.ColumnNames.IsReleased, esSystemType.Boolean);
            }
        }

        public esQueryItem ReleasedDateTime
        {
            get
            {
                return new esQueryItem(this, BedManagementMetadata.ColumnNames.ReleasedDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem ReleasedByUserID
        {
            get
            {
                return new esQueryItem(this, BedManagementMetadata.ColumnNames.ReleasedByUserID, esSystemType.String);
            }
        }

        public esQueryItem IsVoid
        {
            get
            {
                return new esQueryItem(this, BedManagementMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
            }
        }

        public esQueryItem VoidDateTime
        {
            get
            {
                return new esQueryItem(this, BedManagementMetadata.ColumnNames.VoidDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem VoidByUserID
        {
            get
            {
                return new esQueryItem(this, BedManagementMetadata.ColumnNames.VoidByUserID, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, BedManagementMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, BedManagementMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("BedManagementCollection")]
    public partial class BedManagementCollection : esBedManagementCollection, IEnumerable<BedManagement>
    {
        public BedManagementCollection()
        {

        }

        public static implicit operator List<BedManagement>(BedManagementCollection coll)
        {
            List<BedManagement> list = new List<BedManagement>();

            foreach (BedManagement emp in coll)
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
                return BedManagementMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new BedManagementQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new BedManagement(row);
        }

        override protected esEntity CreateEntity()
        {
            return new BedManagement();
        }

        #endregion

        [BrowsableAttribute(false)]
        public BedManagementQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new BedManagementQuery();
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
        public bool Load(BedManagementQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public BedManagement AddNew()
        {
            BedManagement entity = base.AddNewEntity() as BedManagement;

            return entity;
        }
        public BedManagement FindByPrimaryKey(Int64 bedManagementID)
        {
            return base.FindByPrimaryKey(bedManagementID) as BedManagement;
        }

        #region IEnumerable< BedManagement> Members

        IEnumerator<BedManagement> IEnumerable<BedManagement>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as BedManagement;
            }
        }

        #endregion

        private BedManagementQuery query;
    }


    /// <summary>
    /// Encapsulates the 'BedManagement' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("BedManagement ({BedManagementID})")]
    [Serializable]
    public partial class BedManagement : esBedManagement
    {
        public BedManagement()
        {
        }

        public BedManagement(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return BedManagementMetadata.Meta();
            }
        }

        override protected esBedManagementQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new BedManagementQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public BedManagementQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new BedManagementQuery();
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
        public bool Load(BedManagementQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private BedManagementQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class BedManagementQuery : esBedManagementQuery
    {
        public BedManagementQuery()
        {

        }

        public BedManagementQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "BedManagementQuery";
        }
    }

    [Serializable]
    public partial class BedManagementMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected BedManagementMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(BedManagementMetadata.ColumnNames.BedManagementID, 0, typeof(System.Int64), esSystemType.Int64);
            c.PropertyName = BedManagementMetadata.PropertyNames.BedManagementID;
            c.IsInPrimaryKey = true;
            c.IsAutoIncrement = true;
            c.NumericPrecision = 19;
            _columns.Add(c);

            c = new esColumnMetadata(BedManagementMetadata.ColumnNames.BedID, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = BedManagementMetadata.PropertyNames.BedID;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(BedManagementMetadata.ColumnNames.TransactionDate, 2, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = BedManagementMetadata.PropertyNames.TransactionDate;
            _columns.Add(c);

            c = new esColumnMetadata(BedManagementMetadata.ColumnNames.PatientID, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = BedManagementMetadata.PropertyNames.PatientID;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BedManagementMetadata.ColumnNames.FirstName, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = BedManagementMetadata.PropertyNames.FirstName;
            c.CharacterMaxLength = 50;
            _columns.Add(c);

            c = new esColumnMetadata(BedManagementMetadata.ColumnNames.MiddleName, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = BedManagementMetadata.PropertyNames.MiddleName;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BedManagementMetadata.ColumnNames.LastName, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = BedManagementMetadata.PropertyNames.LastName;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BedManagementMetadata.ColumnNames.StreetName, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = BedManagementMetadata.PropertyNames.StreetName;
            c.CharacterMaxLength = 250;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BedManagementMetadata.ColumnNames.District, 8, typeof(System.String), esSystemType.String);
            c.PropertyName = BedManagementMetadata.PropertyNames.District;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BedManagementMetadata.ColumnNames.City, 9, typeof(System.String), esSystemType.String);
            c.PropertyName = BedManagementMetadata.PropertyNames.City;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BedManagementMetadata.ColumnNames.County, 10, typeof(System.String), esSystemType.String);
            c.PropertyName = BedManagementMetadata.PropertyNames.County;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BedManagementMetadata.ColumnNames.State, 11, typeof(System.String), esSystemType.String);
            c.PropertyName = BedManagementMetadata.PropertyNames.State;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BedManagementMetadata.ColumnNames.ZipCode, 12, typeof(System.String), esSystemType.String);
            c.PropertyName = BedManagementMetadata.PropertyNames.ZipCode;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BedManagementMetadata.ColumnNames.PhoneNo, 13, typeof(System.String), esSystemType.String);
            c.PropertyName = BedManagementMetadata.PropertyNames.PhoneNo;
            c.CharacterMaxLength = 255;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BedManagementMetadata.ColumnNames.MobilePhoneNo, 14, typeof(System.String), esSystemType.String);
            c.PropertyName = BedManagementMetadata.PropertyNames.MobilePhoneNo;
            c.CharacterMaxLength = 255;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BedManagementMetadata.ColumnNames.FaxNo, 15, typeof(System.String), esSystemType.String);
            c.PropertyName = BedManagementMetadata.PropertyNames.FaxNo;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BedManagementMetadata.ColumnNames.Email, 16, typeof(System.String), esSystemType.String);
            c.PropertyName = BedManagementMetadata.PropertyNames.Email;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BedManagementMetadata.ColumnNames.ReservationNo, 17, typeof(System.String), esSystemType.String);
            c.PropertyName = BedManagementMetadata.PropertyNames.ReservationNo;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BedManagementMetadata.ColumnNames.RegistrationNo, 18, typeof(System.String), esSystemType.String);
            c.PropertyName = BedManagementMetadata.PropertyNames.RegistrationNo;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BedManagementMetadata.ColumnNames.RegistrationBedID, 19, typeof(System.String), esSystemType.String);
            c.PropertyName = BedManagementMetadata.PropertyNames.RegistrationBedID;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BedManagementMetadata.ColumnNames.SRBedStatus, 20, typeof(System.String), esSystemType.String);
            c.PropertyName = BedManagementMetadata.PropertyNames.SRBedStatus;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(BedManagementMetadata.ColumnNames.CreatedDateTime, 21, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = BedManagementMetadata.PropertyNames.CreatedDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BedManagementMetadata.ColumnNames.CreatedByUserID, 22, typeof(System.String), esSystemType.String);
            c.PropertyName = BedManagementMetadata.PropertyNames.CreatedByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BedManagementMetadata.ColumnNames.IsReleased, 23, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = BedManagementMetadata.PropertyNames.IsReleased;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BedManagementMetadata.ColumnNames.ReleasedDateTime, 24, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = BedManagementMetadata.PropertyNames.ReleasedDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BedManagementMetadata.ColumnNames.ReleasedByUserID, 25, typeof(System.String), esSystemType.String);
            c.PropertyName = BedManagementMetadata.PropertyNames.ReleasedByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BedManagementMetadata.ColumnNames.IsVoid, 26, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = BedManagementMetadata.PropertyNames.IsVoid;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BedManagementMetadata.ColumnNames.VoidDateTime, 27, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = BedManagementMetadata.PropertyNames.VoidDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BedManagementMetadata.ColumnNames.VoidByUserID, 28, typeof(System.String), esSystemType.String);
            c.PropertyName = BedManagementMetadata.PropertyNames.VoidByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BedManagementMetadata.ColumnNames.LastUpdateDateTime, 29, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = BedManagementMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BedManagementMetadata.ColumnNames.LastUpdateByUserID, 30, typeof(System.String), esSystemType.String);
            c.PropertyName = BedManagementMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public BedManagementMetadata Meta()
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
            public const string BedManagementID = "BedManagementID";
            public const string BedID = "BedID";
            public const string TransactionDate = "TransactionDate";
            public const string PatientID = "PatientID";
            public const string FirstName = "FirstName";
            public const string MiddleName = "MiddleName";
            public const string LastName = "LastName";
            public const string StreetName = "StreetName";
            public const string District = "District";
            public const string City = "City";
            public const string County = "County";
            public const string State = "State";
            public const string ZipCode = "ZipCode";
            public const string PhoneNo = "PhoneNo";
            public const string MobilePhoneNo = "MobilePhoneNo";
            public const string FaxNo = "FaxNo";
            public const string Email = "Email";
            public const string ReservationNo = "ReservationNo";
            public const string RegistrationNo = "RegistrationNo";
            public const string RegistrationBedID = "RegistrationBedID";
            public const string SRBedStatus = "SRBedStatus";
            public const string CreatedDateTime = "CreatedDateTime";
            public const string CreatedByUserID = "CreatedByUserID";
            public const string IsReleased = "IsReleased";
            public const string ReleasedDateTime = "ReleasedDateTime";
            public const string ReleasedByUserID = "ReleasedByUserID";
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
            public const string BedManagementID = "BedManagementID";
            public const string BedID = "BedID";
            public const string TransactionDate = "TransactionDate";
            public const string PatientID = "PatientID";
            public const string FirstName = "FirstName";
            public const string MiddleName = "MiddleName";
            public const string LastName = "LastName";
            public const string StreetName = "StreetName";
            public const string District = "District";
            public const string City = "City";
            public const string County = "County";
            public const string State = "State";
            public const string ZipCode = "ZipCode";
            public const string PhoneNo = "PhoneNo";
            public const string MobilePhoneNo = "MobilePhoneNo";
            public const string FaxNo = "FaxNo";
            public const string Email = "Email";
            public const string ReservationNo = "ReservationNo";
            public const string RegistrationNo = "RegistrationNo";
            public const string RegistrationBedID = "RegistrationBedID";
            public const string SRBedStatus = "SRBedStatus";
            public const string CreatedDateTime = "CreatedDateTime";
            public const string CreatedByUserID = "CreatedByUserID";
            public const string IsReleased = "IsReleased";
            public const string ReleasedDateTime = "ReleasedDateTime";
            public const string ReleasedByUserID = "ReleasedByUserID";
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
            lock (typeof(BedManagementMetadata))
            {
                if (BedManagementMetadata.mapDelegates == null)
                {
                    BedManagementMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (BedManagementMetadata.meta == null)
                {
                    BedManagementMetadata.meta = new BedManagementMetadata();
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

                meta.AddTypeMap("BedManagementID", new esTypeMap("bigint", "System.Int64"));
                meta.AddTypeMap("BedID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("TransactionDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("PatientID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("FirstName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("MiddleName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("StreetName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("District", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("City", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("County", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("State", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ZipCode", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("PhoneNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("MobilePhoneNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("FaxNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Email", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ReservationNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("RegistrationBedID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRBedStatus", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsReleased", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("ReleasedDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("ReleasedByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("VoidDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("VoidByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "BedManagement";
                meta.Destination = "BedManagement";
                meta.spInsert = "proc_BedManagementInsert";
                meta.spUpdate = "proc_BedManagementUpdate";
                meta.spDelete = "proc_BedManagementDelete";
                meta.spLoadAll = "proc_BedManagementLoadAll";
                meta.spLoadByPrimaryKey = "proc_BedManagementLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private BedManagementMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
