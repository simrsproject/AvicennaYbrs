/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 3/29/2017 1:17:05 PM
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
    abstract public class esPatientEmergencyContactCollection : esEntityCollectionWAuditLog
    {
        public esPatientEmergencyContactCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "PatientEmergencyContactCollection";
        }

        #region Query Logic
        protected void InitQuery(esPatientEmergencyContactQuery query)
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
            this.InitQuery(query as esPatientEmergencyContactQuery);
        }
        #endregion

        virtual public PatientEmergencyContact DetachEntity(PatientEmergencyContact entity)
        {
            return base.DetachEntity(entity) as PatientEmergencyContact;
        }

        virtual public PatientEmergencyContact AttachEntity(PatientEmergencyContact entity)
        {
            return base.AttachEntity(entity) as PatientEmergencyContact;
        }

        virtual public void Combine(PatientEmergencyContactCollection collection)
        {
            base.Combine(collection);
        }

        new public PatientEmergencyContact this[int index]
        {
            get
            {
                return base[index] as PatientEmergencyContact;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(PatientEmergencyContact);
        }
    }

    [Serializable]
    abstract public class esPatientEmergencyContact : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esPatientEmergencyContactQuery GetDynamicQuery()
        {
            return null;
        }

        public esPatientEmergencyContact()
        {
        }

        public esPatientEmergencyContact(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String patientID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(patientID);
            else
                return LoadByPrimaryKeyStoredProcedure(patientID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String patientID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(patientID);
            else
                return LoadByPrimaryKeyStoredProcedure(patientID);
        }

        private bool LoadByPrimaryKeyDynamic(String patientID)
        {
            esPatientEmergencyContactQuery query = this.GetDynamicQuery();
            query.Where(query.PatientID == patientID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String patientID)
        {
            esParameters parms = new esParameters();
            parms.Add("PatientID", patientID);
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
                        case "PatientID": this.str.PatientID = (string)value; break;
                        case "ContactName": this.str.ContactName = (string)value; break;
                        case "SRRelationship": this.str.SRRelationship = (string)value; break;
                        case "StreetName": this.str.StreetName = (string)value; break;
                        case "District": this.str.District = (string)value; break;
                        case "City": this.str.City = (string)value; break;
                        case "County": this.str.County = (string)value; break;
                        case "State": this.str.State = (string)value; break;
                        case "ZipCode": this.str.ZipCode = (string)value; break;
                        case "FaxNo": this.str.FaxNo = (string)value; break;
                        case "Email": this.str.Email = (string)value; break;
                        case "PhoneNo": this.str.PhoneNo = (string)value; break;
                        case "MobilePhoneNo": this.str.MobilePhoneNo = (string)value; break;
                        case "Notes": this.str.Notes = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "SROccupation": this.str.SROccupation = (string)value; break;
                        case "Ssn": this.str.Ssn = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
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
        /// Maps to PatientEmergencyContact.PatientID
        /// </summary>
        virtual public System.String PatientID
        {
            get
            {
                return base.GetSystemString(PatientEmergencyContactMetadata.ColumnNames.PatientID);
            }

            set
            {
                base.SetSystemString(PatientEmergencyContactMetadata.ColumnNames.PatientID, value);
            }
        }
        /// <summary>
        /// Maps to PatientEmergencyContact.ContactName
        /// </summary>
        virtual public System.String ContactName
        {
            get
            {
                return base.GetSystemString(PatientEmergencyContactMetadata.ColumnNames.ContactName);
            }

            set
            {
                base.SetSystemString(PatientEmergencyContactMetadata.ColumnNames.ContactName, value);
            }
        }
        /// <summary>
        /// Maps to PatientEmergencyContact.SRRelationship
        /// </summary>
        virtual public System.String SRRelationship
        {
            get
            {
                return base.GetSystemString(PatientEmergencyContactMetadata.ColumnNames.SRRelationship);
            }

            set
            {
                base.SetSystemString(PatientEmergencyContactMetadata.ColumnNames.SRRelationship, value);
            }
        }
        /// <summary>
        /// Maps to PatientEmergencyContact.StreetName
        /// </summary>
        virtual public System.String StreetName
        {
            get
            {
                return base.GetSystemString(PatientEmergencyContactMetadata.ColumnNames.StreetName);
            }

            set
            {
                base.SetSystemString(PatientEmergencyContactMetadata.ColumnNames.StreetName, value);
            }
        }
        /// <summary>
        /// Maps to PatientEmergencyContact.District
        /// </summary>
        virtual public System.String District
        {
            get
            {
                return base.GetSystemString(PatientEmergencyContactMetadata.ColumnNames.District);
            }

            set
            {
                base.SetSystemString(PatientEmergencyContactMetadata.ColumnNames.District, value);
            }
        }
        /// <summary>
        /// Maps to PatientEmergencyContact.City
        /// </summary>
        virtual public System.String City
        {
            get
            {
                return base.GetSystemString(PatientEmergencyContactMetadata.ColumnNames.City);
            }

            set
            {
                base.SetSystemString(PatientEmergencyContactMetadata.ColumnNames.City, value);
            }
        }
        /// <summary>
        /// Maps to PatientEmergencyContact.County
        /// </summary>
        virtual public System.String County
        {
            get
            {
                return base.GetSystemString(PatientEmergencyContactMetadata.ColumnNames.County);
            }

            set
            {
                base.SetSystemString(PatientEmergencyContactMetadata.ColumnNames.County, value);
            }
        }
        /// <summary>
        /// Maps to PatientEmergencyContact.State
        /// </summary>
        virtual public System.String State
        {
            get
            {
                return base.GetSystemString(PatientEmergencyContactMetadata.ColumnNames.State);
            }

            set
            {
                base.SetSystemString(PatientEmergencyContactMetadata.ColumnNames.State, value);
            }
        }
        /// <summary>
        /// Maps to PatientEmergencyContact.ZipCode
        /// </summary>
        virtual public System.String ZipCode
        {
            get
            {
                return base.GetSystemString(PatientEmergencyContactMetadata.ColumnNames.ZipCode);
            }

            set
            {
                base.SetSystemString(PatientEmergencyContactMetadata.ColumnNames.ZipCode, value);
            }
        }
        /// <summary>
        /// Maps to PatientEmergencyContact.FaxNo
        /// </summary>
        virtual public System.String FaxNo
        {
            get
            {
                return base.GetSystemString(PatientEmergencyContactMetadata.ColumnNames.FaxNo);
            }

            set
            {
                base.SetSystemString(PatientEmergencyContactMetadata.ColumnNames.FaxNo, value);
            }
        }
        /// <summary>
        /// Maps to PatientEmergencyContact.Email
        /// </summary>
        virtual public System.String Email
        {
            get
            {
                return base.GetSystemString(PatientEmergencyContactMetadata.ColumnNames.Email);
            }

            set
            {
                base.SetSystemString(PatientEmergencyContactMetadata.ColumnNames.Email, value);
            }
        }
        /// <summary>
        /// Maps to PatientEmergencyContact.PhoneNo
        /// </summary>
        virtual public System.String PhoneNo
        {
            get
            {
                return base.GetSystemString(PatientEmergencyContactMetadata.ColumnNames.PhoneNo);
            }

            set
            {
                base.SetSystemString(PatientEmergencyContactMetadata.ColumnNames.PhoneNo, value);
            }
        }
        /// <summary>
        /// Maps to PatientEmergencyContact.MobilePhoneNo
        /// </summary>
        virtual public System.String MobilePhoneNo
        {
            get
            {
                return base.GetSystemString(PatientEmergencyContactMetadata.ColumnNames.MobilePhoneNo);
            }

            set
            {
                base.SetSystemString(PatientEmergencyContactMetadata.ColumnNames.MobilePhoneNo, value);
            }
        }
        /// <summary>
        /// Maps to PatientEmergencyContact.Notes
        /// </summary>
        virtual public System.String Notes
        {
            get
            {
                return base.GetSystemString(PatientEmergencyContactMetadata.ColumnNames.Notes);
            }

            set
            {
                base.SetSystemString(PatientEmergencyContactMetadata.ColumnNames.Notes, value);
            }
        }
        /// <summary>
        /// Maps to PatientEmergencyContact.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(PatientEmergencyContactMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(PatientEmergencyContactMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to PatientEmergencyContact.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(PatientEmergencyContactMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(PatientEmergencyContactMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to PatientEmergencyContact.SROccupation
        /// </summary>
        virtual public System.String SROccupation
        {
            get
            {
                return base.GetSystemString(PatientEmergencyContactMetadata.ColumnNames.SROccupation);
            }

            set
            {
                base.SetSystemString(PatientEmergencyContactMetadata.ColumnNames.SROccupation, value);
            }
        }
        /// <summary>
        /// Maps to PatientEmergencyContact.Ssn
        /// </summary>
        virtual public System.String Ssn
        {
            get
            {
                return base.GetSystemString(PatientEmergencyContactMetadata.ColumnNames.Ssn);
            }

            set
            {
                base.SetSystemString(PatientEmergencyContactMetadata.ColumnNames.Ssn, value);
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
            public esStrings(esPatientEmergencyContact entity)
            {
                this.entity = entity;
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
            public System.String ContactName
            {
                get
                {
                    System.String data = entity.ContactName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ContactName = null;
                    else entity.ContactName = Convert.ToString(value);
                }
            }
            public System.String SRRelationship
            {
                get
                {
                    System.String data = entity.SRRelationship;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRRelationship = null;
                    else entity.SRRelationship = Convert.ToString(value);
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
            public System.String SROccupation
            {
                get
                {
                    System.String data = entity.SROccupation;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SROccupation = null;
                    else entity.SROccupation = Convert.ToString(value);
                }
            }
            public System.String Ssn
            {
                get
                {
                    System.String data = entity.Ssn;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Ssn = null;
                    else entity.Ssn = Convert.ToString(value);
                }
            }
            private esPatientEmergencyContact entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esPatientEmergencyContactQuery query)
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
                throw new Exception("esPatientEmergencyContact can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class PatientEmergencyContact : esPatientEmergencyContact
    {
    }

    [Serializable]
    abstract public class esPatientEmergencyContactQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return PatientEmergencyContactMetadata.Meta();
            }
        }

        public esQueryItem PatientID
        {
            get
            {
                return new esQueryItem(this, PatientEmergencyContactMetadata.ColumnNames.PatientID, esSystemType.String);
            }
        }

        public esQueryItem ContactName
        {
            get
            {
                return new esQueryItem(this, PatientEmergencyContactMetadata.ColumnNames.ContactName, esSystemType.String);
            }
        }

        public esQueryItem SRRelationship
        {
            get
            {
                return new esQueryItem(this, PatientEmergencyContactMetadata.ColumnNames.SRRelationship, esSystemType.String);
            }
        }

        public esQueryItem StreetName
        {
            get
            {
                return new esQueryItem(this, PatientEmergencyContactMetadata.ColumnNames.StreetName, esSystemType.String);
            }
        }

        public esQueryItem District
        {
            get
            {
                return new esQueryItem(this, PatientEmergencyContactMetadata.ColumnNames.District, esSystemType.String);
            }
        }

        public esQueryItem City
        {
            get
            {
                return new esQueryItem(this, PatientEmergencyContactMetadata.ColumnNames.City, esSystemType.String);
            }
        }

        public esQueryItem County
        {
            get
            {
                return new esQueryItem(this, PatientEmergencyContactMetadata.ColumnNames.County, esSystemType.String);
            }
        }

        public esQueryItem State
        {
            get
            {
                return new esQueryItem(this, PatientEmergencyContactMetadata.ColumnNames.State, esSystemType.String);
            }
        }

        public esQueryItem ZipCode
        {
            get
            {
                return new esQueryItem(this, PatientEmergencyContactMetadata.ColumnNames.ZipCode, esSystemType.String);
            }
        }

        public esQueryItem FaxNo
        {
            get
            {
                return new esQueryItem(this, PatientEmergencyContactMetadata.ColumnNames.FaxNo, esSystemType.String);
            }
        }

        public esQueryItem Email
        {
            get
            {
                return new esQueryItem(this, PatientEmergencyContactMetadata.ColumnNames.Email, esSystemType.String);
            }
        }

        public esQueryItem PhoneNo
        {
            get
            {
                return new esQueryItem(this, PatientEmergencyContactMetadata.ColumnNames.PhoneNo, esSystemType.String);
            }
        }

        public esQueryItem MobilePhoneNo
        {
            get
            {
                return new esQueryItem(this, PatientEmergencyContactMetadata.ColumnNames.MobilePhoneNo, esSystemType.String);
            }
        }

        public esQueryItem Notes
        {
            get
            {
                return new esQueryItem(this, PatientEmergencyContactMetadata.ColumnNames.Notes, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, PatientEmergencyContactMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, PatientEmergencyContactMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem SROccupation
        {
            get
            {
                return new esQueryItem(this, PatientEmergencyContactMetadata.ColumnNames.SROccupation, esSystemType.String);
            }
        }

        public esQueryItem Ssn
        {
            get
            {
                return new esQueryItem(this, PatientEmergencyContactMetadata.ColumnNames.Ssn, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("PatientEmergencyContactCollection")]
    public partial class PatientEmergencyContactCollection : esPatientEmergencyContactCollection, IEnumerable<PatientEmergencyContact>
    {
        public PatientEmergencyContactCollection()
        {

        }

        public static implicit operator List<PatientEmergencyContact>(PatientEmergencyContactCollection coll)
        {
            List<PatientEmergencyContact> list = new List<PatientEmergencyContact>();

            foreach (PatientEmergencyContact emp in coll)
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
                return PatientEmergencyContactMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PatientEmergencyContactQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new PatientEmergencyContact(row);
        }

        override protected esEntity CreateEntity()
        {
            return new PatientEmergencyContact();
        }

        #endregion

        [BrowsableAttribute(false)]
        public PatientEmergencyContactQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PatientEmergencyContactQuery();
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
        public bool Load(PatientEmergencyContactQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public PatientEmergencyContact AddNew()
        {
            PatientEmergencyContact entity = base.AddNewEntity() as PatientEmergencyContact;

            return entity;
        }
        public PatientEmergencyContact FindByPrimaryKey(String patientID)
        {
            return base.FindByPrimaryKey(patientID) as PatientEmergencyContact;
        }

        #region IEnumerable< PatientEmergencyContact> Members

        IEnumerator<PatientEmergencyContact> IEnumerable<PatientEmergencyContact>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as PatientEmergencyContact;
            }
        }

        #endregion

        private PatientEmergencyContactQuery query;
    }


    /// <summary>
    /// Encapsulates the 'PatientEmergencyContact' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("PatientEmergencyContact ({PatientID})")]
    [Serializable]
    public partial class PatientEmergencyContact : esPatientEmergencyContact
    {
        public PatientEmergencyContact()
        {
        }

        public PatientEmergencyContact(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return PatientEmergencyContactMetadata.Meta();
            }
        }

        override protected esPatientEmergencyContactQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PatientEmergencyContactQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public PatientEmergencyContactQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PatientEmergencyContactQuery();
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
        public bool Load(PatientEmergencyContactQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private PatientEmergencyContactQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class PatientEmergencyContactQuery : esPatientEmergencyContactQuery
    {
        public PatientEmergencyContactQuery()
        {

        }

        public PatientEmergencyContactQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "PatientEmergencyContactQuery";
        }
    }

    [Serializable]
    public partial class PatientEmergencyContactMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected PatientEmergencyContactMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(PatientEmergencyContactMetadata.ColumnNames.PatientID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientEmergencyContactMetadata.PropertyNames.PatientID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 15;
            c.HasDefault = true;
            c.Default = @"(' ')";
            _columns.Add(c);

            c = new esColumnMetadata(PatientEmergencyContactMetadata.ColumnNames.ContactName, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientEmergencyContactMetadata.PropertyNames.ContactName;
            c.CharacterMaxLength = 100;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(PatientEmergencyContactMetadata.ColumnNames.SRRelationship, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientEmergencyContactMetadata.PropertyNames.SRRelationship;
            c.CharacterMaxLength = 20;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(PatientEmergencyContactMetadata.ColumnNames.StreetName, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientEmergencyContactMetadata.PropertyNames.StreetName;
            c.CharacterMaxLength = 250;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(PatientEmergencyContactMetadata.ColumnNames.District, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientEmergencyContactMetadata.PropertyNames.District;
            c.CharacterMaxLength = 50;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(PatientEmergencyContactMetadata.ColumnNames.City, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientEmergencyContactMetadata.PropertyNames.City;
            c.CharacterMaxLength = 50;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(PatientEmergencyContactMetadata.ColumnNames.County, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientEmergencyContactMetadata.PropertyNames.County;
            c.CharacterMaxLength = 50;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(PatientEmergencyContactMetadata.ColumnNames.State, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientEmergencyContactMetadata.PropertyNames.State;
            c.CharacterMaxLength = 50;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(PatientEmergencyContactMetadata.ColumnNames.ZipCode, 8, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientEmergencyContactMetadata.PropertyNames.ZipCode;
            c.CharacterMaxLength = 15;
            c.HasDefault = true;
            c.Default = @"('')";
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientEmergencyContactMetadata.ColumnNames.FaxNo, 9, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientEmergencyContactMetadata.PropertyNames.FaxNo;
            c.CharacterMaxLength = 50;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(PatientEmergencyContactMetadata.ColumnNames.Email, 10, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientEmergencyContactMetadata.PropertyNames.Email;
            c.CharacterMaxLength = 50;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(PatientEmergencyContactMetadata.ColumnNames.PhoneNo, 11, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientEmergencyContactMetadata.PropertyNames.PhoneNo;
            c.CharacterMaxLength = 255;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(PatientEmergencyContactMetadata.ColumnNames.MobilePhoneNo, 12, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientEmergencyContactMetadata.PropertyNames.MobilePhoneNo;
            c.CharacterMaxLength = 255;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(PatientEmergencyContactMetadata.ColumnNames.Notes, 13, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientEmergencyContactMetadata.PropertyNames.Notes;
            c.CharacterMaxLength = 4000;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(PatientEmergencyContactMetadata.ColumnNames.LastUpdateDateTime, 14, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PatientEmergencyContactMetadata.PropertyNames.LastUpdateDateTime;
            _columns.Add(c);

            c = new esColumnMetadata(PatientEmergencyContactMetadata.ColumnNames.LastUpdateByUserID, 15, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientEmergencyContactMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 40;
            _columns.Add(c);

            c = new esColumnMetadata(PatientEmergencyContactMetadata.ColumnNames.SROccupation, 16, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientEmergencyContactMetadata.PropertyNames.SROccupation;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientEmergencyContactMetadata.ColumnNames.Ssn, 17, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientEmergencyContactMetadata.PropertyNames.Ssn;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public PatientEmergencyContactMetadata Meta()
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
            public const string PatientID = "PatientID";
            public const string ContactName = "ContactName";
            public const string SRRelationship = "SRRelationship";
            public const string StreetName = "StreetName";
            public const string District = "District";
            public const string City = "City";
            public const string County = "County";
            public const string State = "State";
            public const string ZipCode = "ZipCode";
            public const string FaxNo = "FaxNo";
            public const string Email = "Email";
            public const string PhoneNo = "PhoneNo";
            public const string MobilePhoneNo = "MobilePhoneNo";
            public const string Notes = "Notes";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string SROccupation = "SROccupation";
            public const string Ssn = "Ssn";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string PatientID = "PatientID";
            public const string ContactName = "ContactName";
            public const string SRRelationship = "SRRelationship";
            public const string StreetName = "StreetName";
            public const string District = "District";
            public const string City = "City";
            public const string County = "County";
            public const string State = "State";
            public const string ZipCode = "ZipCode";
            public const string FaxNo = "FaxNo";
            public const string Email = "Email";
            public const string PhoneNo = "PhoneNo";
            public const string MobilePhoneNo = "MobilePhoneNo";
            public const string Notes = "Notes";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string SROccupation = "SROccupation";
            public const string Ssn = "Ssn";
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
            lock (typeof(PatientEmergencyContactMetadata))
            {
                if (PatientEmergencyContactMetadata.mapDelegates == null)
                {
                    PatientEmergencyContactMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (PatientEmergencyContactMetadata.meta == null)
                {
                    PatientEmergencyContactMetadata.meta = new PatientEmergencyContactMetadata();
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

                meta.AddTypeMap("PatientID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ContactName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRRelationship", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("StreetName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("District", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("City", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("County", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("State", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ZipCode", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("FaxNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Email", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("PhoneNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("MobilePhoneNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SROccupation", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Ssn", new esTypeMap("varchar", "System.String"));


                meta.Source = "PatientEmergencyContact";
                meta.Destination = "PatientEmergencyContact";
                meta.spInsert = "proc_PatientEmergencyContactInsert";
                meta.spUpdate = "proc_PatientEmergencyContactUpdate";
                meta.spDelete = "proc_PatientEmergencyContactDelete";
                meta.spLoadAll = "proc_PatientEmergencyContactLoadAll";
                meta.spLoadByPrimaryKey = "proc_PatientEmergencyContactLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private PatientEmergencyContactMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
