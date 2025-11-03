/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/2/2019 9:47:04 AM
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
    abstract public class esBirthRecordCollection : esEntityCollectionWAuditLog
    {
        public esBirthRecordCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "BirthRecordCollection";
        }

        #region Query Logic
        protected void InitQuery(esBirthRecordQuery query)
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
            this.InitQuery(query as esBirthRecordQuery);
        }
        #endregion

        virtual public BirthRecord DetachEntity(BirthRecord entity)
        {
            return base.DetachEntity(entity) as BirthRecord;
        }

        virtual public BirthRecord AttachEntity(BirthRecord entity)
        {
            return base.AttachEntity(entity) as BirthRecord;
        }

        virtual public void Combine(BirthRecordCollection collection)
        {
            base.Combine(collection);
        }

        new public BirthRecord this[int index]
        {
            get
            {
                return base[index] as BirthRecord;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(BirthRecord);
        }
    }

    [Serializable]
    abstract public class esBirthRecord : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esBirthRecordQuery GetDynamicQuery()
        {
            return null;
        }

        public esBirthRecord()
        {
        }

        public esBirthRecord(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String registrationNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(registrationNo);
            else
                return LoadByPrimaryKeyStoredProcedure(registrationNo);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String registrationNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(registrationNo);
            else
                return LoadByPrimaryKeyStoredProcedure(registrationNo);
        }

        private bool LoadByPrimaryKeyDynamic(String registrationNo)
        {
            esBirthRecordQuery query = this.GetDynamicQuery();
            query.Where(query.RegistrationNo == registrationNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String registrationNo)
        {
            esParameters parms = new esParameters();
            parms.Add("RegistrationNo", registrationNo);
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
                        case "MotherMedicalNo": this.str.MotherMedicalNo = (string)value; break;
                        case "MotherRegistrationNo": this.str.MotherRegistrationNo = (string)value; break;
                        case "TimeOfBirth": this.str.TimeOfBirth = (string)value; break;
                        case "SRBornAt": this.str.SRBornAt = (string)value; break;
                        case "BornAtDescription": this.str.BornAtDescription = (string)value; break;
                        case "SRSingleTwin": this.str.SRSingleTwin = (string)value; break;
                        case "TwinNo": this.str.TwinNo = (string)value; break;
                        case "SRBirthMethod": this.str.SRBirthMethod = (string)value; break;
                        case "SRCaesarMethod": this.str.SRCaesarMethod = (string)value; break;
                        case "SRBornCondition": this.str.SRBornCondition = (string)value; break;
                        case "SRBirthComplication": this.str.SRBirthComplication = (string)value; break;
                        case "SRDeathCondition": this.str.SRDeathCondition = (string)value; break;
                        case "SRBornDeath": this.str.SRBornDeath = (string)value; break;
                        case "SRBirthIndication": this.str.SRBirthIndication = (string)value; break;
                        case "BirthPregnancyAge": this.str.BirthPregnancyAge = (string)value; break;
                        case "Length": this.str.Length = (string)value; break;
                        case "Weight": this.str.Weight = (string)value; break;
                        case "ApgarScore1": this.str.ApgarScore1 = (string)value; break;
                        case "ApgarScore2": this.str.ApgarScore2 = (string)value; break;
                        case "ApgarScore3": this.str.ApgarScore3 = (string)value; break;
                        case "HeadCircumference": this.str.HeadCircumference = (string)value; break;
                        case "ChestCircumference": this.str.ChestCircumference = (string)value; break;
                        case "CertificateNo": this.str.CertificateNo = (string)value; break;
                        case "FatherName": this.str.FatherName = (string)value; break;
                        case "FatherSsn": this.str.FatherSsn = (string)value; break;
                        case "FatherBirthOfDate": this.str.FatherBirthOfDate = (string)value; break;
                        case "StreetName": this.str.StreetName = (string)value; break;
                        case "District": this.str.District = (string)value; break;
                        case "City": this.str.City = (string)value; break;
                        case "County": this.str.County = (string)value; break;
                        case "State": this.str.State = (string)value; break;
                        case "ZipCode": this.str.ZipCode = (string)value; break;
                        case "SROccupation": this.str.SROccupation = (string)value; break;
                        case "PhoneNo": this.str.PhoneNo = (string)value; break;
                        case "FaxNo": this.str.FaxNo = (string)value; break;
                        case "MobilePhoneNo": this.str.MobilePhoneNo = (string)value; break;
                        case "Email": this.str.Email = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "Notes": this.str.Notes = (string)value; break;
                        case "AbdomenCircumference": this.str.AbdomenCircumference = (string)value; break;
                        case "ChildNo": this.str.ChildNo = (string)value; break;
                        case "IsEyesSmeared": this.str.IsEyesSmeared = (string)value; break;
                        case "EyesSmearedNotes": this.str.EyesSmearedNotes = (string)value; break;
                        case "IsAnusExamined": this.str.IsAnusExamined = (string)value; break;
                        case "AnusExaminedNotes": this.str.AnusExaminedNotes = (string)value; break;
                        case "IsKangarooMethod": this.str.IsKangarooMethod = (string)value; break;
                        case "IsIMD": this.str.IsIMD = (string)value; break;
                        case "IsCongenitalHyperthyroidism": this.str.IsCongenitalHyperthyroidism = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "BirthPregnancyAge":

                            if (value == null || value is System.Decimal)
                                this.BirthPregnancyAge = (System.Decimal?)value;
                            break;
                        case "Length":

                            if (value == null || value is System.Decimal)
                                this.Length = (System.Decimal?)value;
                            break;
                        case "Weight":

                            if (value == null || value is System.Decimal)
                                this.Weight = (System.Decimal?)value;
                            break;
                        case "ApgarScore1":

                            if (value == null || value is System.Decimal)
                                this.ApgarScore1 = (System.Decimal?)value;
                            break;
                        case "ApgarScore2":

                            if (value == null || value is System.Decimal)
                                this.ApgarScore2 = (System.Decimal?)value;
                            break;
                        case "ApgarScore3":

                            if (value == null || value is System.Decimal)
                                this.ApgarScore3 = (System.Decimal?)value;
                            break;
                        case "HeadCircumference":

                            if (value == null || value is System.Decimal)
                                this.HeadCircumference = (System.Decimal?)value;
                            break;
                        case "ChestCircumference":

                            if (value == null || value is System.Decimal)
                                this.ChestCircumference = (System.Decimal?)value;
                            break;
                        case "FatherBirthOfDate":

                            if (value == null || value is System.DateTime)
                                this.FatherBirthOfDate = (System.DateTime?)value;
                            break;
                        case "LastUpdateDateTime":

                            if (value == null || value is System.DateTime)
                                this.LastUpdateDateTime = (System.DateTime?)value;
                            break;
                        case "AbdomenCircumference":

                            if (value == null || value is System.Decimal)
                                this.AbdomenCircumference = (System.Decimal?)value;
                            break;
                        case "ChildNo":

                            if (value == null || value is System.Int16)
                                this.ChildNo = (System.Int16?)value;
                            break;
                        case "IsEyesSmeared":

                            if (value == null || value is System.Boolean)
                                this.IsEyesSmeared = (System.Boolean?)value;
                            break;
                        case "IsAnusExamined":

                            if (value == null || value is System.Boolean)
                                this.IsAnusExamined = (System.Boolean?)value;
                            break;
                        case "IsKangarooMethod":

                            if (value == null || value is System.Boolean)
                                this.IsKangarooMethod = (System.Boolean?)value;
                            break;
                        case "IsIMD":

                            if (value == null || value is System.Boolean)
                                this.IsIMD = (System.Boolean?)value;
                            break;
                        case "IsCongenitalHyperthyroidism":

                            if (value == null || value is System.Boolean)
                                this.IsCongenitalHyperthyroidism = (System.Boolean?)value;
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
        /// Maps to BirthRecord.RegistrationNo
        /// </summary>
        virtual public System.String RegistrationNo
        {
            get
            {
                return base.GetSystemString(BirthRecordMetadata.ColumnNames.RegistrationNo);
            }

            set
            {
                base.SetSystemString(BirthRecordMetadata.ColumnNames.RegistrationNo, value);
            }
        }
        /// <summary>
        /// Maps to BirthRecord.MotherMedicalNo
        /// </summary>
        virtual public System.String MotherMedicalNo
        {
            get
            {
                return base.GetSystemString(BirthRecordMetadata.ColumnNames.MotherMedicalNo);
            }

            set
            {
                base.SetSystemString(BirthRecordMetadata.ColumnNames.MotherMedicalNo, value);
            }
        }
        /// <summary>
        /// Maps to BirthRecord.MotherRegistrationNo
        /// </summary>
        virtual public System.String MotherRegistrationNo
        {
            get
            {
                return base.GetSystemString(BirthRecordMetadata.ColumnNames.MotherRegistrationNo);
            }

            set
            {
                base.SetSystemString(BirthRecordMetadata.ColumnNames.MotherRegistrationNo, value);
            }
        }
        /// <summary>
        /// Maps to BirthRecord.TimeOfBirth
        /// </summary>
        virtual public System.String TimeOfBirth
        {
            get
            {
                return base.GetSystemString(BirthRecordMetadata.ColumnNames.TimeOfBirth);
            }

            set
            {
                base.SetSystemString(BirthRecordMetadata.ColumnNames.TimeOfBirth, value);
            }
        }
        /// <summary>
        /// Maps to BirthRecord.SRBornAt
        /// </summary>
        virtual public System.String SRBornAt
        {
            get
            {
                return base.GetSystemString(BirthRecordMetadata.ColumnNames.SRBornAt);
            }

            set
            {
                base.SetSystemString(BirthRecordMetadata.ColumnNames.SRBornAt, value);
            }
        }
        /// <summary>
        /// Maps to BirthRecord.BornAtDescription
        /// </summary>
        virtual public System.String BornAtDescription
        {
            get
            {
                return base.GetSystemString(BirthRecordMetadata.ColumnNames.BornAtDescription);
            }

            set
            {
                base.SetSystemString(BirthRecordMetadata.ColumnNames.BornAtDescription, value);
            }
        }
        /// <summary>
        /// Maps to BirthRecord.SRSingleTwin
        /// </summary>
        virtual public System.String SRSingleTwin
        {
            get
            {
                return base.GetSystemString(BirthRecordMetadata.ColumnNames.SRSingleTwin);
            }

            set
            {
                base.SetSystemString(BirthRecordMetadata.ColumnNames.SRSingleTwin, value);
            }
        }
        /// <summary>
        /// Maps to BirthRecord.TwinNo
        /// </summary>
        virtual public System.String TwinNo
        {
            get
            {
                return base.GetSystemString(BirthRecordMetadata.ColumnNames.TwinNo);
            }

            set
            {
                base.SetSystemString(BirthRecordMetadata.ColumnNames.TwinNo, value);
            }
        }
        /// <summary>
        /// Maps to BirthRecord.SRBirthMethod
        /// </summary>
        virtual public System.String SRBirthMethod
        {
            get
            {
                return base.GetSystemString(BirthRecordMetadata.ColumnNames.SRBirthMethod);
            }

            set
            {
                base.SetSystemString(BirthRecordMetadata.ColumnNames.SRBirthMethod, value);
            }
        }
        /// <summary>
        /// Maps to BirthRecord.SRCaesarMethod
        /// </summary>
        virtual public System.String SRCaesarMethod
        {
            get
            {
                return base.GetSystemString(BirthRecordMetadata.ColumnNames.SRCaesarMethod);
            }

            set
            {
                base.SetSystemString(BirthRecordMetadata.ColumnNames.SRCaesarMethod, value);
            }
        }
        /// <summary>
        /// Maps to BirthRecord.SRBornCondition
        /// </summary>
        virtual public System.String SRBornCondition
        {
            get
            {
                return base.GetSystemString(BirthRecordMetadata.ColumnNames.SRBornCondition);
            }

            set
            {
                base.SetSystemString(BirthRecordMetadata.ColumnNames.SRBornCondition, value);
            }
        }
        /// <summary>
        /// Maps to BirthRecord.SRBirthComplication
        /// </summary>
        virtual public System.String SRBirthComplication
        {
            get
            {
                return base.GetSystemString(BirthRecordMetadata.ColumnNames.SRBirthComplication);
            }

            set
            {
                base.SetSystemString(BirthRecordMetadata.ColumnNames.SRBirthComplication, value);
            }
        }
        /// <summary>
        /// Maps to BirthRecord.SRDeathCondition
        /// </summary>
        virtual public System.String SRDeathCondition
        {
            get
            {
                return base.GetSystemString(BirthRecordMetadata.ColumnNames.SRDeathCondition);
            }

            set
            {
                base.SetSystemString(BirthRecordMetadata.ColumnNames.SRDeathCondition, value);
            }
        }
        /// <summary>
        /// Maps to BirthRecord.SRBornDeath
        /// </summary>
        virtual public System.String SRBornDeath
        {
            get
            {
                return base.GetSystemString(BirthRecordMetadata.ColumnNames.SRBornDeath);
            }

            set
            {
                base.SetSystemString(BirthRecordMetadata.ColumnNames.SRBornDeath, value);
            }
        }
        /// <summary>
        /// Maps to BirthRecordMetadata.IsKangarooMethod
        /// </summary>
        virtual public System.Boolean? IsKangarooMethod
        {
            get
            {
                return base.GetSystemBoolean(BirthRecordMetadata.ColumnNames.IsKangarooMethod);
            }

            set
            {
                base.SetSystemBoolean(BirthRecordMetadata.ColumnNames.IsKangarooMethod, value);
            }
        }
        /// <summary>
        /// Maps to BirthRecordMetadata.IsIMD
        /// </summary>
        virtual public System.Boolean? IsIMD
        {
            get
            {
                return base.GetSystemBoolean(BirthRecordMetadata.ColumnNames.IsIMD);
            }

            set
            {
                base.SetSystemBoolean(BirthRecordMetadata.ColumnNames.IsIMD, value);
            }
        }
        /// <summary>
        /// Maps to BirthRecordMetadata.IsCongenitalHyperthyroidismv
        /// </summary>
        virtual public System.Boolean? IsCongenitalHyperthyroidism
        {
            get
            {
                return base.GetSystemBoolean(BirthRecordMetadata.ColumnNames.IsCongenitalHyperthyroidism);
            }

            set
            {
                base.SetSystemBoolean(BirthRecordMetadata.ColumnNames.IsCongenitalHyperthyroidism, value);
            }
        }
        /// <summary>
        /// Maps to BirthRecord.SRBirthIndication
        /// </summary>
        virtual public System.String SRBirthIndication
        {
            get
            {
                return base.GetSystemString(BirthRecordMetadata.ColumnNames.SRBirthIndication);
            }

            set
            {
                base.SetSystemString(BirthRecordMetadata.ColumnNames.SRBirthIndication, value);
            }
        }
        /// <summary>
        /// Maps to BirthRecord.BirthPregnancyAge
        /// </summary>
        virtual public System.Decimal? BirthPregnancyAge
        {
            get
            {
                return base.GetSystemDecimal(BirthRecordMetadata.ColumnNames.BirthPregnancyAge);
            }

            set
            {
                base.SetSystemDecimal(BirthRecordMetadata.ColumnNames.BirthPregnancyAge, value);
            }
        }
        /// <summary>
        /// Maps to BirthRecord.Length
        /// </summary>
        virtual public System.Decimal? Length
        {
            get
            {
                return base.GetSystemDecimal(BirthRecordMetadata.ColumnNames.Length);
            }

            set
            {
                base.SetSystemDecimal(BirthRecordMetadata.ColumnNames.Length, value);
            }
        }
        /// <summary>
        /// Maps to BirthRecord.Weight
        /// </summary>
        virtual public System.Decimal? Weight
        {
            get
            {
                return base.GetSystemDecimal(BirthRecordMetadata.ColumnNames.Weight);
            }

            set
            {
                base.SetSystemDecimal(BirthRecordMetadata.ColumnNames.Weight, value);
            }
        }
        /// <summary>
        /// Maps to BirthRecord.ApgarScore1
        /// </summary>
        virtual public System.Decimal? ApgarScore1
        {
            get
            {
                return base.GetSystemDecimal(BirthRecordMetadata.ColumnNames.ApgarScore1);
            }

            set
            {
                base.SetSystemDecimal(BirthRecordMetadata.ColumnNames.ApgarScore1, value);
            }
        }
        /// <summary>
        /// Maps to BirthRecord.ApgarScore2
        /// </summary>
        virtual public System.Decimal? ApgarScore2
        {
            get
            {
                return base.GetSystemDecimal(BirthRecordMetadata.ColumnNames.ApgarScore2);
            }

            set
            {
                base.SetSystemDecimal(BirthRecordMetadata.ColumnNames.ApgarScore2, value);
            }
        }
        /// <summary>
        /// Maps to BirthRecord.ApgarScore3
        /// </summary>
        virtual public System.Decimal? ApgarScore3
        {
            get
            {
                return base.GetSystemDecimal(BirthRecordMetadata.ColumnNames.ApgarScore3);
            }

            set
            {
                base.SetSystemDecimal(BirthRecordMetadata.ColumnNames.ApgarScore3, value);
            }
        }
        /// <summary>
        /// Maps to BirthRecord.HeadCircumference
        /// </summary>
        virtual public System.Decimal? HeadCircumference
        {
            get
            {
                return base.GetSystemDecimal(BirthRecordMetadata.ColumnNames.HeadCircumference);
            }

            set
            {
                base.SetSystemDecimal(BirthRecordMetadata.ColumnNames.HeadCircumference, value);
            }
        }
        /// <summary>
        /// Maps to BirthRecord.ChestCircumference
        /// </summary>
        virtual public System.Decimal? ChestCircumference
        {
            get
            {
                return base.GetSystemDecimal(BirthRecordMetadata.ColumnNames.ChestCircumference);
            }

            set
            {
                base.SetSystemDecimal(BirthRecordMetadata.ColumnNames.ChestCircumference, value);
            }
        }
        /// <summary>
        /// Maps to BirthRecord.CertificateNo
        /// </summary>
        virtual public System.String CertificateNo
        {
            get
            {
                return base.GetSystemString(BirthRecordMetadata.ColumnNames.CertificateNo);
            }

            set
            {
                base.SetSystemString(BirthRecordMetadata.ColumnNames.CertificateNo, value);
            }
        }
        /// <summary>
        /// Maps to BirthRecord.FatherName
        /// </summary>
        virtual public System.String FatherName
        {
            get
            {
                return base.GetSystemString(BirthRecordMetadata.ColumnNames.FatherName);
            }

            set
            {
                base.SetSystemString(BirthRecordMetadata.ColumnNames.FatherName, value);
            }
        }
        /// <summary>
        /// Maps to BirthRecord.FatherSsn
        /// </summary>
        virtual public System.String FatherSsn
        {
            get
            {
                return base.GetSystemString(BirthRecordMetadata.ColumnNames.FatherSsn);
            }

            set
            {
                base.SetSystemString(BirthRecordMetadata.ColumnNames.FatherSsn, value);
            }
        }
        /// <summary>
        /// Maps to BirthRecord.FatherBirthOfDate
        /// </summary>
        virtual public System.DateTime? FatherBirthOfDate
        {
            get
            {
                return base.GetSystemDateTime(BirthRecordMetadata.ColumnNames.FatherBirthOfDate);
            }

            set
            {
                base.SetSystemDateTime(BirthRecordMetadata.ColumnNames.FatherBirthOfDate, value);
            }
        }
        /// <summary>
        /// Maps to BirthRecord.StreetName
        /// </summary>
        virtual public System.String StreetName
        {
            get
            {
                return base.GetSystemString(BirthRecordMetadata.ColumnNames.StreetName);
            }

            set
            {
                base.SetSystemString(BirthRecordMetadata.ColumnNames.StreetName, value);
            }
        }
        /// <summary>
        /// Maps to BirthRecord.District
        /// </summary>
        virtual public System.String District
        {
            get
            {
                return base.GetSystemString(BirthRecordMetadata.ColumnNames.District);
            }

            set
            {
                base.SetSystemString(BirthRecordMetadata.ColumnNames.District, value);
            }
        }
        /// <summary>
        /// Maps to BirthRecord.City
        /// </summary>
        virtual public System.String City
        {
            get
            {
                return base.GetSystemString(BirthRecordMetadata.ColumnNames.City);
            }

            set
            {
                base.SetSystemString(BirthRecordMetadata.ColumnNames.City, value);
            }
        }
        /// <summary>
        /// Maps to BirthRecord.County
        /// </summary>
        virtual public System.String County
        {
            get
            {
                return base.GetSystemString(BirthRecordMetadata.ColumnNames.County);
            }

            set
            {
                base.SetSystemString(BirthRecordMetadata.ColumnNames.County, value);
            }
        }
        /// <summary>
        /// Maps to BirthRecord.State
        /// </summary>
        virtual public System.String State
        {
            get
            {
                return base.GetSystemString(BirthRecordMetadata.ColumnNames.State);
            }

            set
            {
                base.SetSystemString(BirthRecordMetadata.ColumnNames.State, value);
            }
        }
        /// <summary>
        /// Maps to BirthRecord.ZipCode
        /// </summary>
        virtual public System.String ZipCode
        {
            get
            {
                return base.GetSystemString(BirthRecordMetadata.ColumnNames.ZipCode);
            }

            set
            {
                base.SetSystemString(BirthRecordMetadata.ColumnNames.ZipCode, value);
            }
        }
        /// <summary>
        /// Maps to BirthRecord.SROccupation
        /// </summary>
        virtual public System.String SROccupation
        {
            get
            {
                return base.GetSystemString(BirthRecordMetadata.ColumnNames.SROccupation);
            }

            set
            {
                base.SetSystemString(BirthRecordMetadata.ColumnNames.SROccupation, value);
            }
        }
        /// <summary>
        /// Maps to BirthRecord.PhoneNo
        /// </summary>
        virtual public System.String PhoneNo
        {
            get
            {
                return base.GetSystemString(BirthRecordMetadata.ColumnNames.PhoneNo);
            }

            set
            {
                base.SetSystemString(BirthRecordMetadata.ColumnNames.PhoneNo, value);
            }
        }
        /// <summary>
        /// Maps to BirthRecord.FaxNo
        /// </summary>
        virtual public System.String FaxNo
        {
            get
            {
                return base.GetSystemString(BirthRecordMetadata.ColumnNames.FaxNo);
            }

            set
            {
                base.SetSystemString(BirthRecordMetadata.ColumnNames.FaxNo, value);
            }
        }
        /// <summary>
        /// Maps to BirthRecord.MobilePhoneNo
        /// </summary>
        virtual public System.String MobilePhoneNo
        {
            get
            {
                return base.GetSystemString(BirthRecordMetadata.ColumnNames.MobilePhoneNo);
            }

            set
            {
                base.SetSystemString(BirthRecordMetadata.ColumnNames.MobilePhoneNo, value);
            }
        }
        /// <summary>
        /// Maps to BirthRecord.Email
        /// </summary>
        virtual public System.String Email
        {
            get
            {
                return base.GetSystemString(BirthRecordMetadata.ColumnNames.Email);
            }

            set
            {
                base.SetSystemString(BirthRecordMetadata.ColumnNames.Email, value);
            }
        }
        /// <summary>
        /// Maps to BirthRecord.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(BirthRecordMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(BirthRecordMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to BirthRecord.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(BirthRecordMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(BirthRecordMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to BirthRecord.Notes
        /// </summary>
        virtual public System.String Notes
        {
            get
            {
                return base.GetSystemString(BirthRecordMetadata.ColumnNames.Notes);
            }

            set
            {
                base.SetSystemString(BirthRecordMetadata.ColumnNames.Notes, value);
            }
        }
        /// <summary>
        /// Maps to BirthRecord.AbdomenCircumference
        /// </summary>
        virtual public System.Decimal? AbdomenCircumference
        {
            get
            {
                return base.GetSystemDecimal(BirthRecordMetadata.ColumnNames.AbdomenCircumference);
            }

            set
            {
                base.SetSystemDecimal(BirthRecordMetadata.ColumnNames.AbdomenCircumference, value);
            }
        }
        /// <summary>
        /// Maps to BirthRecord.ChildNo
        /// </summary>
        virtual public System.Int16? ChildNo
        {
            get
            {
                return base.GetSystemInt16(BirthRecordMetadata.ColumnNames.ChildNo);
            }

            set
            {
                base.SetSystemInt16(BirthRecordMetadata.ColumnNames.ChildNo, value);
            }
        }
        /// <summary>
        /// Maps to BirthRecord.IsEyesSmeared
        /// </summary>
        virtual public System.Boolean? IsEyesSmeared
        {
            get
            {
                return base.GetSystemBoolean(BirthRecordMetadata.ColumnNames.IsEyesSmeared);
            }

            set
            {
                base.SetSystemBoolean(BirthRecordMetadata.ColumnNames.IsEyesSmeared, value);
            }
        }
        /// <summary>
        /// Maps to BirthRecord.EyesSmearedNotes
        /// </summary>
        virtual public System.String EyesSmearedNotes
        {
            get
            {
                return base.GetSystemString(BirthRecordMetadata.ColumnNames.EyesSmearedNotes);
            }

            set
            {
                base.SetSystemString(BirthRecordMetadata.ColumnNames.EyesSmearedNotes, value);
            }
        }
        /// <summary>
        /// Maps to BirthRecord.IsAnusExamined
        /// </summary>
        virtual public System.Boolean? IsAnusExamined
        {
            get
            {
                return base.GetSystemBoolean(BirthRecordMetadata.ColumnNames.IsAnusExamined);
            }

            set
            {
                base.SetSystemBoolean(BirthRecordMetadata.ColumnNames.IsAnusExamined, value);
            }
        }
        /// <summary>
        /// Maps to BirthRecord.AnusExaminedNotes
        /// </summary>
        virtual public System.String AnusExaminedNotes
        {
            get
            {
                return base.GetSystemString(BirthRecordMetadata.ColumnNames.AnusExaminedNotes);
            }

            set
            {
                base.SetSystemString(BirthRecordMetadata.ColumnNames.AnusExaminedNotes, value);
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
            public esStrings(esBirthRecord entity)
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
            public System.String MotherMedicalNo
            {
                get
                {
                    System.String data = entity.MotherMedicalNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.MotherMedicalNo = null;
                    else entity.MotherMedicalNo = Convert.ToString(value);
                }
            }
            public System.String MotherRegistrationNo
            {
                get
                {
                    System.String data = entity.MotherRegistrationNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.MotherRegistrationNo = null;
                    else entity.MotherRegistrationNo = Convert.ToString(value);
                }
            }
            public System.String TimeOfBirth
            {
                get
                {
                    System.String data = entity.TimeOfBirth;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TimeOfBirth = null;
                    else entity.TimeOfBirth = Convert.ToString(value);
                }
            }
            public System.String SRBornAt
            {
                get
                {
                    System.String data = entity.SRBornAt;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRBornAt = null;
                    else entity.SRBornAt = Convert.ToString(value);
                }
            }
            public System.String BornAtDescription
            {
                get
                {
                    System.String data = entity.BornAtDescription;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.BornAtDescription = null;
                    else entity.BornAtDescription = Convert.ToString(value);
                }
            }
            public System.String SRSingleTwin
            {
                get
                {
                    System.String data = entity.SRSingleTwin;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRSingleTwin = null;
                    else entity.SRSingleTwin = Convert.ToString(value);
                }
            }
            public System.String TwinNo
            {
                get
                {
                    System.String data = entity.TwinNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TwinNo = null;
                    else entity.TwinNo = Convert.ToString(value);
                }
            }
            public System.String SRBirthMethod
            {
                get
                {
                    System.String data = entity.SRBirthMethod;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRBirthMethod = null;
                    else entity.SRBirthMethod = Convert.ToString(value);
                }
            }
            public System.String SRCaesarMethod
            {
                get
                {
                    System.String data = entity.SRCaesarMethod;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRCaesarMethod = null;
                    else entity.SRCaesarMethod = Convert.ToString(value);
                }
            }
            public System.String SRBornCondition
            {
                get
                {
                    System.String data = entity.SRBornCondition;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRBornCondition = null;
                    else entity.SRBornCondition = Convert.ToString(value);
                }
            }
            public System.String SRBirthComplication
            {
                get
                {
                    System.String data = entity.SRBirthComplication;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRBirthComplication = null;
                    else entity.SRBirthComplication = Convert.ToString(value);
                }
            }
            public System.String SRDeathCondition
            {
                get
                {
                    System.String data = entity.SRDeathCondition;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRDeathCondition = null;
                    else entity.SRDeathCondition = Convert.ToString(value);
                }
            }

            public System.String SRBornDeath
            {
                get
                {
                    System.String data = entity.SRBornDeath;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRBornDeath = null;
                    else entity.SRBornDeath = Convert.ToString(value);
                }
            }
            public System.String IsKangarooMethod
            {
                get
                {
                    System.Boolean? data = entity.IsKangarooMethod;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsKangarooMethod = null;
                    else entity.IsKangarooMethod = Convert.ToBoolean(value);
                }
            }

            public System.String IsIMD
            {
                get
                {
                    System.Boolean? data = entity.IsIMD;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsIMD = null;
                    else entity.IsIMD = Convert.ToBoolean(value);
                }
            }

            public System.String IsCongenitalHyperthyroidism
            {
                get
                {
                    System.Boolean? data = entity.IsCongenitalHyperthyroidism;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsCongenitalHyperthyroidism = null;
                    else entity.IsCongenitalHyperthyroidism = Convert.ToBoolean(value);
                }
            }
            
            public System.String SRBirthIndication
            {
                get
                {
                    System.String data = entity.SRBirthIndication;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRBirthIndication = null;
                    else entity.SRBirthIndication = Convert.ToString(value);
                }
            }
            public System.String BirthPregnancyAge
            {
                get
                {
                    System.Decimal? data = entity.BirthPregnancyAge;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.BirthPregnancyAge = null;
                    else entity.BirthPregnancyAge = Convert.ToDecimal(value);
                }
            }
            public System.String Length
            {
                get
                {
                    System.Decimal? data = entity.Length;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Length = null;
                    else entity.Length = Convert.ToDecimal(value);
                }
            }
            public System.String Weight
            {
                get
                {
                    System.Decimal? data = entity.Weight;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Weight = null;
                    else entity.Weight = Convert.ToDecimal(value);
                }
            }
            public System.String ApgarScore1
            {
                get
                {
                    System.Decimal? data = entity.ApgarScore1;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ApgarScore1 = null;
                    else entity.ApgarScore1 = Convert.ToDecimal(value);
                }
            }
            public System.String ApgarScore2
            {
                get
                {
                    System.Decimal? data = entity.ApgarScore2;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ApgarScore2 = null;
                    else entity.ApgarScore2 = Convert.ToDecimal(value);
                }
            }
            public System.String ApgarScore3
            {
                get
                {
                    System.Decimal? data = entity.ApgarScore3;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ApgarScore3 = null;
                    else entity.ApgarScore3 = Convert.ToDecimal(value);
                }
            }
            public System.String HeadCircumference
            {
                get
                {
                    System.Decimal? data = entity.HeadCircumference;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.HeadCircumference = null;
                    else entity.HeadCircumference = Convert.ToDecimal(value);
                }
            }
            public System.String ChestCircumference
            {
                get
                {
                    System.Decimal? data = entity.ChestCircumference;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ChestCircumference = null;
                    else entity.ChestCircumference = Convert.ToDecimal(value);
                }
            }
            public System.String CertificateNo
            {
                get
                {
                    System.String data = entity.CertificateNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CertificateNo = null;
                    else entity.CertificateNo = Convert.ToString(value);
                }
            }
            public System.String FatherName
            {
                get
                {
                    System.String data = entity.FatherName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FatherName = null;
                    else entity.FatherName = Convert.ToString(value);
                }
            }
            public System.String FatherSsn
            {
                get
                {
                    System.String data = entity.FatherSsn;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FatherSsn = null;
                    else entity.FatherSsn = Convert.ToString(value);
                }
            }
            public System.String FatherBirthOfDate
            {
                get
                {
                    System.DateTime? data = entity.FatherBirthOfDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FatherBirthOfDate = null;
                    else entity.FatherBirthOfDate = Convert.ToDateTime(value);
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
            public System.String AbdomenCircumference
            {
                get
                {
                    System.Decimal? data = entity.AbdomenCircumference;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.AbdomenCircumference = null;
                    else entity.AbdomenCircumference = Convert.ToDecimal(value);
                }
            }
            public System.String ChildNo
            {
                get
                {
                    System.Int16? data = entity.ChildNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ChildNo = null;
                    else entity.ChildNo = Convert.ToInt16(value);
                }
            }
            public System.String IsEyesSmeared
            {
                get
                {
                    System.Boolean? data = entity.IsEyesSmeared;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsEyesSmeared = null;
                    else entity.IsEyesSmeared = Convert.ToBoolean(value);
                }
            }
            public System.String EyesSmearedNotes
            {
                get
                {
                    System.String data = entity.EyesSmearedNotes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.EyesSmearedNotes = null;
                    else entity.EyesSmearedNotes = Convert.ToString(value);
                }
            }
            public System.String IsAnusExamined
            {
                get
                {
                    System.Boolean? data = entity.IsAnusExamined;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsAnusExamined = null;
                    else entity.IsAnusExamined = Convert.ToBoolean(value);
                }
            }
            public System.String AnusExaminedNotes
            {
                get
                {
                    System.String data = entity.AnusExaminedNotes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.AnusExaminedNotes = null;
                    else entity.AnusExaminedNotes = Convert.ToString(value);
                }
            }
            private esBirthRecord entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esBirthRecordQuery query)
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
                throw new Exception("esBirthRecord can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class BirthRecord : esBirthRecord
    {
    }

    [Serializable]
    abstract public class esBirthRecordQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return BirthRecordMetadata.Meta();
            }
        }

        public esQueryItem RegistrationNo
        {
            get
            {
                return new esQueryItem(this, BirthRecordMetadata.ColumnNames.RegistrationNo, esSystemType.String);
            }
        }

        public esQueryItem MotherMedicalNo
        {
            get
            {
                return new esQueryItem(this, BirthRecordMetadata.ColumnNames.MotherMedicalNo, esSystemType.String);
            }
        }

        public esQueryItem MotherRegistrationNo
        {
            get
            {
                return new esQueryItem(this, BirthRecordMetadata.ColumnNames.MotherRegistrationNo, esSystemType.String);
            }
        }

        public esQueryItem TimeOfBirth
        {
            get
            {
                return new esQueryItem(this, BirthRecordMetadata.ColumnNames.TimeOfBirth, esSystemType.String);
            }
        }

        public esQueryItem SRBornAt
        {
            get
            {
                return new esQueryItem(this, BirthRecordMetadata.ColumnNames.SRBornAt, esSystemType.String);
            }
        }

        public esQueryItem BornAtDescription
        {
            get
            {
                return new esQueryItem(this, BirthRecordMetadata.ColumnNames.BornAtDescription, esSystemType.String);
            }
        }

        public esQueryItem SRSingleTwin
        {
            get
            {
                return new esQueryItem(this, BirthRecordMetadata.ColumnNames.SRSingleTwin, esSystemType.String);
            }
        }

        public esQueryItem TwinNo
        {
            get
            {
                return new esQueryItem(this, BirthRecordMetadata.ColumnNames.TwinNo, esSystemType.String);
            }
        }

        public esQueryItem SRBirthMethod
        {
            get
            {
                return new esQueryItem(this, BirthRecordMetadata.ColumnNames.SRBirthMethod, esSystemType.String);
            }
        }

        public esQueryItem SRCaesarMethod
        {
            get
            {
                return new esQueryItem(this, BirthRecordMetadata.ColumnNames.SRCaesarMethod, esSystemType.String);
            }
        }

        public esQueryItem SRBornCondition
        {
            get
            {
                return new esQueryItem(this, BirthRecordMetadata.ColumnNames.SRBornCondition, esSystemType.String);
            }
        }

        public esQueryItem SRBirthComplication
        {
            get
            {
                return new esQueryItem(this, BirthRecordMetadata.ColumnNames.SRBirthComplication, esSystemType.String);
            }
        }

        public esQueryItem SRDeathCondition
        {
            get
            {
                return new esQueryItem(this, BirthRecordMetadata.ColumnNames.SRDeathCondition, esSystemType.String);
            }
        }

        public esQueryItem SRBornDeath
        {
            get
            {
                return new esQueryItem(this, BirthRecordMetadata.ColumnNames.SRBornDeath, esSystemType.String);
            }
        }
        public esQueryItem IsKangarooMethod
        {
            get
            {
                return new esQueryItem(this, BirthRecordMetadata.ColumnNames.IsKangarooMethod, esSystemType.Boolean);
            }
        }
        public esQueryItem IsIMD
        {
            get
            {
                return new esQueryItem(this, BirthRecordMetadata.ColumnNames.IsIMD, esSystemType.Boolean);
            }
        }
        public esQueryItem IsCongenitalHyperthyroidism
        {
            get
            {
                return new esQueryItem(this, BirthRecordMetadata.ColumnNames.IsCongenitalHyperthyroidism, esSystemType.Boolean);
            }
        }

        public esQueryItem SRBirthIndication
        {
            get
            {
                return new esQueryItem(this, BirthRecordMetadata.ColumnNames.SRBirthIndication, esSystemType.String);
            }
        }

        public esQueryItem BirthPregnancyAge
        {
            get
            {
                return new esQueryItem(this, BirthRecordMetadata.ColumnNames.BirthPregnancyAge, esSystemType.Decimal);
            }
        }

        public esQueryItem Length
        {
            get
            {
                return new esQueryItem(this, BirthRecordMetadata.ColumnNames.Length, esSystemType.Decimal);
            }
        }

        public esQueryItem Weight
        {
            get
            {
                return new esQueryItem(this, BirthRecordMetadata.ColumnNames.Weight, esSystemType.Decimal);
            }
        }

        public esQueryItem ApgarScore1
        {
            get
            {
                return new esQueryItem(this, BirthRecordMetadata.ColumnNames.ApgarScore1, esSystemType.Decimal);
            }
        }

        public esQueryItem ApgarScore2
        {
            get
            {
                return new esQueryItem(this, BirthRecordMetadata.ColumnNames.ApgarScore2, esSystemType.Decimal);
            }
        }

        public esQueryItem ApgarScore3
        {
            get
            {
                return new esQueryItem(this, BirthRecordMetadata.ColumnNames.ApgarScore3, esSystemType.Decimal);
            }
        }

        public esQueryItem HeadCircumference
        {
            get
            {
                return new esQueryItem(this, BirthRecordMetadata.ColumnNames.HeadCircumference, esSystemType.Decimal);
            }
        }

        public esQueryItem ChestCircumference
        {
            get
            {
                return new esQueryItem(this, BirthRecordMetadata.ColumnNames.ChestCircumference, esSystemType.Decimal);
            }
        }

        public esQueryItem CertificateNo
        {
            get
            {
                return new esQueryItem(this, BirthRecordMetadata.ColumnNames.CertificateNo, esSystemType.String);
            }
        }

        public esQueryItem FatherName
        {
            get
            {
                return new esQueryItem(this, BirthRecordMetadata.ColumnNames.FatherName, esSystemType.String);
            }
        }

        public esQueryItem FatherSsn
        {
            get
            {
                return new esQueryItem(this, BirthRecordMetadata.ColumnNames.FatherSsn, esSystemType.String);
            }
        }

        public esQueryItem FatherBirthOfDate
        {
            get
            {
                return new esQueryItem(this, BirthRecordMetadata.ColumnNames.FatherBirthOfDate, esSystemType.DateTime);
            }
        }

        public esQueryItem StreetName
        {
            get
            {
                return new esQueryItem(this, BirthRecordMetadata.ColumnNames.StreetName, esSystemType.String);
            }
        }

        public esQueryItem District
        {
            get
            {
                return new esQueryItem(this, BirthRecordMetadata.ColumnNames.District, esSystemType.String);
            }
        }

        public esQueryItem City
        {
            get
            {
                return new esQueryItem(this, BirthRecordMetadata.ColumnNames.City, esSystemType.String);
            }
        }

        public esQueryItem County
        {
            get
            {
                return new esQueryItem(this, BirthRecordMetadata.ColumnNames.County, esSystemType.String);
            }
        }

        public esQueryItem State
        {
            get
            {
                return new esQueryItem(this, BirthRecordMetadata.ColumnNames.State, esSystemType.String);
            }
        }

        public esQueryItem ZipCode
        {
            get
            {
                return new esQueryItem(this, BirthRecordMetadata.ColumnNames.ZipCode, esSystemType.String);
            }
        }

        public esQueryItem SROccupation
        {
            get
            {
                return new esQueryItem(this, BirthRecordMetadata.ColumnNames.SROccupation, esSystemType.String);
            }
        }

        public esQueryItem PhoneNo
        {
            get
            {
                return new esQueryItem(this, BirthRecordMetadata.ColumnNames.PhoneNo, esSystemType.String);
            }
        }

        public esQueryItem FaxNo
        {
            get
            {
                return new esQueryItem(this, BirthRecordMetadata.ColumnNames.FaxNo, esSystemType.String);
            }
        }

        public esQueryItem MobilePhoneNo
        {
            get
            {
                return new esQueryItem(this, BirthRecordMetadata.ColumnNames.MobilePhoneNo, esSystemType.String);
            }
        }

        public esQueryItem Email
        {
            get
            {
                return new esQueryItem(this, BirthRecordMetadata.ColumnNames.Email, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, BirthRecordMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, BirthRecordMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem Notes
        {
            get
            {
                return new esQueryItem(this, BirthRecordMetadata.ColumnNames.Notes, esSystemType.String);
            }
        }

        public esQueryItem AbdomenCircumference
        {
            get
            {
                return new esQueryItem(this, BirthRecordMetadata.ColumnNames.AbdomenCircumference, esSystemType.Decimal);
            }
        }

        public esQueryItem ChildNo
        {
            get
            {
                return new esQueryItem(this, BirthRecordMetadata.ColumnNames.ChildNo, esSystemType.Int16);
            }
        }

        public esQueryItem IsEyesSmeared
        {
            get
            {
                return new esQueryItem(this, BirthRecordMetadata.ColumnNames.IsEyesSmeared, esSystemType.Boolean);
            }
        }

        public esQueryItem EyesSmearedNotes
        {
            get
            {
                return new esQueryItem(this, BirthRecordMetadata.ColumnNames.EyesSmearedNotes, esSystemType.String);
            }
        }

        public esQueryItem IsAnusExamined
        {
            get
            {
                return new esQueryItem(this, BirthRecordMetadata.ColumnNames.IsAnusExamined, esSystemType.Boolean);
            }
        }

        public esQueryItem AnusExaminedNotes
        {
            get
            {
                return new esQueryItem(this, BirthRecordMetadata.ColumnNames.AnusExaminedNotes, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("BirthRecordCollection")]
    public partial class BirthRecordCollection : esBirthRecordCollection, IEnumerable<BirthRecord>
    {
        public BirthRecordCollection()
        {

        }

        public static implicit operator List<BirthRecord>(BirthRecordCollection coll)
        {
            List<BirthRecord> list = new List<BirthRecord>();

            foreach (BirthRecord emp in coll)
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
                return BirthRecordMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new BirthRecordQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new BirthRecord(row);
        }

        override protected esEntity CreateEntity()
        {
            return new BirthRecord();
        }

        #endregion

        [BrowsableAttribute(false)]
        public BirthRecordQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new BirthRecordQuery();
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
        public bool Load(BirthRecordQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public BirthRecord AddNew()
        {
            BirthRecord entity = base.AddNewEntity() as BirthRecord;

            return entity;
        }
        public BirthRecord FindByPrimaryKey(String registrationNo)
        {
            return base.FindByPrimaryKey(registrationNo) as BirthRecord;
        }

        #region IEnumerable< BirthRecord> Members

        IEnumerator<BirthRecord> IEnumerable<BirthRecord>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as BirthRecord;
            }
        }

        #endregion

        private BirthRecordQuery query;
    }


    /// <summary>
    /// Encapsulates the 'BirthRecord' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("BirthRecord ({RegistrationNo})")]
    [Serializable]
    public partial class BirthRecord : esBirthRecord
    {
        public BirthRecord()
        {
        }

        public BirthRecord(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return BirthRecordMetadata.Meta();
            }
        }

        override protected esBirthRecordQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new BirthRecordQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public BirthRecordQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new BirthRecordQuery();
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
        public bool Load(BirthRecordQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private BirthRecordQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class BirthRecordQuery : esBirthRecordQuery
    {
        public BirthRecordQuery()
        {

        }

        public BirthRecordQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "BirthRecordQuery";
        }
    }

    [Serializable]
    public partial class BirthRecordMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected BirthRecordMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(BirthRecordMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = BirthRecordMetadata.PropertyNames.RegistrationNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(BirthRecordMetadata.ColumnNames.MotherMedicalNo, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = BirthRecordMetadata.PropertyNames.MotherMedicalNo;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BirthRecordMetadata.ColumnNames.MotherRegistrationNo, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = BirthRecordMetadata.PropertyNames.MotherRegistrationNo;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BirthRecordMetadata.ColumnNames.TimeOfBirth, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = BirthRecordMetadata.PropertyNames.TimeOfBirth;
            c.CharacterMaxLength = 5;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BirthRecordMetadata.ColumnNames.SRBornAt, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = BirthRecordMetadata.PropertyNames.SRBornAt;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BirthRecordMetadata.ColumnNames.BornAtDescription, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = BirthRecordMetadata.PropertyNames.BornAtDescription;
            c.CharacterMaxLength = 150;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BirthRecordMetadata.ColumnNames.SRSingleTwin, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = BirthRecordMetadata.PropertyNames.SRSingleTwin;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BirthRecordMetadata.ColumnNames.TwinNo, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = BirthRecordMetadata.PropertyNames.TwinNo;
            c.CharacterMaxLength = 1;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BirthRecordMetadata.ColumnNames.SRBirthMethod, 8, typeof(System.String), esSystemType.String);
            c.PropertyName = BirthRecordMetadata.PropertyNames.SRBirthMethod;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BirthRecordMetadata.ColumnNames.SRCaesarMethod, 9, typeof(System.String), esSystemType.String);
            c.PropertyName = BirthRecordMetadata.PropertyNames.SRCaesarMethod;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BirthRecordMetadata.ColumnNames.SRBornCondition, 10, typeof(System.String), esSystemType.String);
            c.PropertyName = BirthRecordMetadata.PropertyNames.SRBornCondition;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BirthRecordMetadata.ColumnNames.SRBirthComplication, 11, typeof(System.String), esSystemType.String);
            c.PropertyName = BirthRecordMetadata.PropertyNames.SRBirthComplication;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BirthRecordMetadata.ColumnNames.SRDeathCondition, 12, typeof(System.String), esSystemType.String);
            c.PropertyName = BirthRecordMetadata.PropertyNames.SRDeathCondition;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BirthRecordMetadata.ColumnNames.SRBornDeath, 13, typeof(System.String), esSystemType.String);
            c.PropertyName = BirthRecordMetadata.PropertyNames.SRBornDeath;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BirthRecordMetadata.ColumnNames.SRBirthIndication, 13, typeof(System.String), esSystemType.String);
            c.PropertyName = BirthRecordMetadata.PropertyNames.SRBirthIndication;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BirthRecordMetadata.ColumnNames.BirthPregnancyAge, 14, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = BirthRecordMetadata.PropertyNames.BirthPregnancyAge;
            c.NumericPrecision = 5;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BirthRecordMetadata.ColumnNames.Length, 15, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = BirthRecordMetadata.PropertyNames.Length;
            c.NumericPrecision = 6;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BirthRecordMetadata.ColumnNames.Weight, 16, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = BirthRecordMetadata.PropertyNames.Weight;
            c.NumericPrecision = 6;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BirthRecordMetadata.ColumnNames.ApgarScore1, 17, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = BirthRecordMetadata.PropertyNames.ApgarScore1;
            c.NumericPrecision = 5;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BirthRecordMetadata.ColumnNames.ApgarScore2, 18, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = BirthRecordMetadata.PropertyNames.ApgarScore2;
            c.NumericPrecision = 5;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BirthRecordMetadata.ColumnNames.ApgarScore3, 19, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = BirthRecordMetadata.PropertyNames.ApgarScore3;
            c.NumericPrecision = 5;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BirthRecordMetadata.ColumnNames.HeadCircumference, 20, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = BirthRecordMetadata.PropertyNames.HeadCircumference;
            c.NumericPrecision = 6;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BirthRecordMetadata.ColumnNames.ChestCircumference, 21, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = BirthRecordMetadata.PropertyNames.ChestCircumference;
            c.NumericPrecision = 6;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BirthRecordMetadata.ColumnNames.CertificateNo, 22, typeof(System.String), esSystemType.String);
            c.PropertyName = BirthRecordMetadata.PropertyNames.CertificateNo;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BirthRecordMetadata.ColumnNames.FatherName, 23, typeof(System.String), esSystemType.String);
            c.PropertyName = BirthRecordMetadata.PropertyNames.FatherName;
            c.CharacterMaxLength = 150;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BirthRecordMetadata.ColumnNames.FatherSsn, 24, typeof(System.String), esSystemType.String);
            c.PropertyName = BirthRecordMetadata.PropertyNames.FatherSsn;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BirthRecordMetadata.ColumnNames.FatherBirthOfDate, 25, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = BirthRecordMetadata.PropertyNames.FatherBirthOfDate;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BirthRecordMetadata.ColumnNames.StreetName, 26, typeof(System.String), esSystemType.String);
            c.PropertyName = BirthRecordMetadata.PropertyNames.StreetName;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BirthRecordMetadata.ColumnNames.District, 27, typeof(System.String), esSystemType.String);
            c.PropertyName = BirthRecordMetadata.PropertyNames.District;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BirthRecordMetadata.ColumnNames.City, 28, typeof(System.String), esSystemType.String);
            c.PropertyName = BirthRecordMetadata.PropertyNames.City;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BirthRecordMetadata.ColumnNames.County, 29, typeof(System.String), esSystemType.String);
            c.PropertyName = BirthRecordMetadata.PropertyNames.County;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BirthRecordMetadata.ColumnNames.State, 30, typeof(System.String), esSystemType.String);
            c.PropertyName = BirthRecordMetadata.PropertyNames.State;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BirthRecordMetadata.ColumnNames.ZipCode, 31, typeof(System.String), esSystemType.String);
            c.PropertyName = BirthRecordMetadata.PropertyNames.ZipCode;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BirthRecordMetadata.ColumnNames.SROccupation, 32, typeof(System.String), esSystemType.String);
            c.PropertyName = BirthRecordMetadata.PropertyNames.SROccupation;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BirthRecordMetadata.ColumnNames.PhoneNo, 33, typeof(System.String), esSystemType.String);
            c.PropertyName = BirthRecordMetadata.PropertyNames.PhoneNo;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BirthRecordMetadata.ColumnNames.FaxNo, 34, typeof(System.String), esSystemType.String);
            c.PropertyName = BirthRecordMetadata.PropertyNames.FaxNo;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BirthRecordMetadata.ColumnNames.MobilePhoneNo, 35, typeof(System.String), esSystemType.String);
            c.PropertyName = BirthRecordMetadata.PropertyNames.MobilePhoneNo;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BirthRecordMetadata.ColumnNames.Email, 36, typeof(System.String), esSystemType.String);
            c.PropertyName = BirthRecordMetadata.PropertyNames.Email;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BirthRecordMetadata.ColumnNames.LastUpdateDateTime, 37, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = BirthRecordMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BirthRecordMetadata.ColumnNames.LastUpdateByUserID, 38, typeof(System.String), esSystemType.String);
            c.PropertyName = BirthRecordMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BirthRecordMetadata.ColumnNames.Notes, 39, typeof(System.String), esSystemType.String);
            c.PropertyName = BirthRecordMetadata.PropertyNames.Notes;
            c.CharacterMaxLength = 500;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BirthRecordMetadata.ColumnNames.AbdomenCircumference, 40, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = BirthRecordMetadata.PropertyNames.AbdomenCircumference;
            c.NumericPrecision = 6;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BirthRecordMetadata.ColumnNames.ChildNo, 41, typeof(System.Int16), esSystemType.Int16);
            c.PropertyName = BirthRecordMetadata.PropertyNames.ChildNo;
            c.NumericPrecision = 5;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BirthRecordMetadata.ColumnNames.IsEyesSmeared, 42, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = BirthRecordMetadata.PropertyNames.IsEyesSmeared;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BirthRecordMetadata.ColumnNames.EyesSmearedNotes, 43, typeof(System.String), esSystemType.String);
            c.PropertyName = BirthRecordMetadata.PropertyNames.EyesSmearedNotes;
            c.CharacterMaxLength = 100;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BirthRecordMetadata.ColumnNames.IsAnusExamined, 44, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = BirthRecordMetadata.PropertyNames.IsAnusExamined;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BirthRecordMetadata.ColumnNames.AnusExaminedNotes, 45, typeof(System.String), esSystemType.String);
            c.PropertyName = BirthRecordMetadata.PropertyNames.AnusExaminedNotes;
            c.CharacterMaxLength = 100;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BirthRecordMetadata.ColumnNames.IsKangarooMethod, 46, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = BirthRecordMetadata.PropertyNames.IsKangarooMethod;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BirthRecordMetadata.ColumnNames.IsIMD, 47, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = BirthRecordMetadata.PropertyNames.IsIMD;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BirthRecordMetadata.ColumnNames.IsCongenitalHyperthyroidism, 48, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = BirthRecordMetadata.PropertyNames.IsCongenitalHyperthyroidism;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public BirthRecordMetadata Meta()
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
            public const string MotherMedicalNo = "MotherMedicalNo";
            public const string MotherRegistrationNo = "MotherRegistrationNo";
            public const string TimeOfBirth = "TimeOfBirth";
            public const string SRBornAt = "SRBornAt";
            public const string BornAtDescription = "BornAtDescription";
            public const string SRSingleTwin = "SRSingleTwin";
            public const string TwinNo = "TwinNo";
            public const string SRBirthMethod = "SRBirthMethod";
            public const string SRCaesarMethod = "SRCaesarMethod";
            public const string SRBornCondition = "SRBornCondition";
            public const string SRBirthComplication = "SRBirthComplication";
            public const string SRDeathCondition = "SRDeathCondition";
            public const string SRBornDeath = "SRBornDeath";
            public const string IsKangarooMethod = "IsKangarooMethod";
            public const string IsIMD = "IsIMD";
            public const string IsCongenitalHyperthyroidism = "IsCongenitalHyperthyroidism";
            public const string SRBirthIndication = "SRBirthIndication";
            public const string BirthPregnancyAge = "BirthPregnancyAge";
            public const string Length = "Length";
            public const string Weight = "Weight";
            public const string ApgarScore1 = "ApgarScore1";
            public const string ApgarScore2 = "ApgarScore2";
            public const string ApgarScore3 = "ApgarScore3";
            public const string HeadCircumference = "HeadCircumference";
            public const string ChestCircumference = "ChestCircumference";
            public const string CertificateNo = "CertificateNo";
            public const string FatherName = "FatherName";
            public const string FatherSsn = "FatherSsn";
            public const string FatherBirthOfDate = "FatherBirthOfDate";
            public const string StreetName = "StreetName";
            public const string District = "District";
            public const string City = "City";
            public const string County = "County";
            public const string State = "State";
            public const string ZipCode = "ZipCode";
            public const string SROccupation = "SROccupation";
            public const string PhoneNo = "PhoneNo";
            public const string FaxNo = "FaxNo";
            public const string MobilePhoneNo = "MobilePhoneNo";
            public const string Email = "Email";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string Notes = "Notes";
            public const string AbdomenCircumference = "AbdomenCircumference";
            public const string ChildNo = "ChildNo";
            public const string IsEyesSmeared = "IsEyesSmeared";
            public const string EyesSmearedNotes = "EyesSmearedNotes";
            public const string IsAnusExamined = "IsAnusExamined";
            public const string AnusExaminedNotes = "AnusExaminedNotes";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string RegistrationNo = "RegistrationNo";
            public const string MotherMedicalNo = "MotherMedicalNo";
            public const string MotherRegistrationNo = "MotherRegistrationNo";
            public const string TimeOfBirth = "TimeOfBirth";
            public const string SRBornAt = "SRBornAt";
            public const string BornAtDescription = "BornAtDescription";
            public const string SRSingleTwin = "SRSingleTwin";
            public const string TwinNo = "TwinNo";
            public const string SRBirthMethod = "SRBirthMethod";
            public const string SRCaesarMethod = "SRCaesarMethod";
            public const string SRBornCondition = "SRBornCondition";
            public const string SRBirthComplication = "SRBirthComplication";
            public const string SRDeathCondition = "SRDeathCondition";
            public const string SRBornDeath = "SRBornDeath";
            public const string IsKangarooMethod = "IsKangarooMethod";
            public const string IsIMD = "IsIMD";
            public const string IsCongenitalHyperthyroidism = "IsCongenitalHyperthyroidism";
            public const string SRBirthIndication = "SRBirthIndication";
            public const string BirthPregnancyAge = "BirthPregnancyAge";
            public const string Length = "Length";
            public const string Weight = "Weight";
            public const string ApgarScore1 = "ApgarScore1";
            public const string ApgarScore2 = "ApgarScore2";
            public const string ApgarScore3 = "ApgarScore3";
            public const string HeadCircumference = "HeadCircumference";
            public const string ChestCircumference = "ChestCircumference";
            public const string CertificateNo = "CertificateNo";
            public const string FatherName = "FatherName";
            public const string FatherSsn = "FatherSsn";
            public const string FatherBirthOfDate = "FatherBirthOfDate";
            public const string StreetName = "StreetName";
            public const string District = "District";
            public const string City = "City";
            public const string County = "County";
            public const string State = "State";
            public const string ZipCode = "ZipCode";
            public const string SROccupation = "SROccupation";
            public const string PhoneNo = "PhoneNo";
            public const string FaxNo = "FaxNo";
            public const string MobilePhoneNo = "MobilePhoneNo";
            public const string Email = "Email";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string Notes = "Notes";
            public const string AbdomenCircumference = "AbdomenCircumference";
            public const string ChildNo = "ChildNo";
            public const string IsEyesSmeared = "IsEyesSmeared";
            public const string EyesSmearedNotes = "EyesSmearedNotes";
            public const string IsAnusExamined = "IsAnusExamined";
            public const string AnusExaminedNotes = "AnusExaminedNotes";
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
            lock (typeof(BirthRecordMetadata))
            {
                if (BirthRecordMetadata.mapDelegates == null)
                {
                    BirthRecordMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (BirthRecordMetadata.meta == null)
                {
                    BirthRecordMetadata.meta = new BirthRecordMetadata();
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
                meta.AddTypeMap("MotherMedicalNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("MotherRegistrationNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("TimeOfBirth", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRBornAt", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("BornAtDescription", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRSingleTwin", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("TwinNo", new esTypeMap("char", "System.String"));
                meta.AddTypeMap("SRBirthMethod", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRCaesarMethod", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRBornCondition", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRBirthComplication", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRDeathCondition", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRBornDeath", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsKangarooMethod", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsIMD", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsCongenitalHyperthyroidism", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("SRBirthIndication", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("BirthPregnancyAge", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("Length", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("Weight", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("ApgarScore1", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("ApgarScore2", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("ApgarScore3", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("HeadCircumference", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("ChestCircumference", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("CertificateNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("FatherName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("FatherSsn", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("FatherBirthOfDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("StreetName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("District", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("City", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("County", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("State", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ZipCode", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SROccupation", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("PhoneNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("FaxNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("MobilePhoneNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Email", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("AbdomenCircumference", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("ChildNo", new esTypeMap("smallint", "System.Int16"));
                meta.AddTypeMap("IsEyesSmeared", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("EyesSmearedNotes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsAnusExamined", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("AnusExaminedNotes", new esTypeMap("varchar", "System.String"));


                meta.Source = "BirthRecord";
                meta.Destination = "BirthRecord";
                meta.spInsert = "proc_BirthRecordInsert";
                meta.spUpdate = "proc_BirthRecordUpdate";
                meta.spDelete = "proc_BirthRecordDelete";
                meta.spLoadAll = "proc_BirthRecordLoadAll";
                meta.spLoadByPrimaryKey = "proc_BirthRecordLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private BirthRecordMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
